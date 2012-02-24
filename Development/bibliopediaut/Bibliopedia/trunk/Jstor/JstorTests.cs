﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Serialization;
using Jstor.Domain;
using System.IO;
using System.Xml;
using Data.FluentsNeeds;
using Data;
using System.Net;
using Iesi.Collections.Generic;
using NHibernate;
using FluentNHibernate.Data;
using System.Linq.Expressions;
using System.Data.SqlServerCe;
using System.Threading;

namespace Jstor
{
    public static class SessionExtensions
    {
        public static object TransactedSave<T>(this ISession session, T entity) where T : Entity
        {
            return session.TransactedOperation<T>(
                x => x.Save(entity),
                x => ExceptionRethrow.Rethrow);
        }

        public enum ExceptionRethrow
        {
            Rethrow = 0,
            ReturnNull
        }

        public static object TransactedOperation<TEntity>(
            this ISession session, 
            Func<ISession, object> operation, 
            Func<Exception, ExceptionRethrow> onError) where TEntity : Entity
        {
            using (var trans = session.BeginTransaction())
            {
                try
                {
                    var result = operation(session);
                    trans.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    var result = onError(ex);
                    trans.Rollback();
                    if (result == ExceptionRethrow.Rethrow) throw;
                    return null;
                }
            }
        }
    }


    [TestFixture]
    public class JstorTests
    {
        IAutoDatabase Database;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Database = 
                AutoDatabase<JstorTests>.Create(
                PersistenceConfigurer.FileBasedTempDb("JSTOR"), true);
                    //PersistenceConfigurer.SqlExpress("localhost", "JSTOR"), true);
        }

        ISession Session;
        [SetUp]
        public void SetUp()
        {
            Session = Database.SessionFactory.OpenSession();
        }

        [TearDown]
        public void TearDown()
        {
            Session.Close();
            Session.Dispose();
        }


        [Test]
        public void dc_deserializes_into_multiple_attributes()
        {
            var fileStream = new FileStream("CannedResponse.xml", FileMode.Open);
            var reader = XmlReader.Create(fileStream);
            var serializer = new XmlSerializer(typeof(JstorService.searchRetrieveResponse));
            Assert.True(serializer.CanDeserialize(reader), "Serialization error");

            var response = serializer.Deserialize(reader) as JstorService.searchRetrieveResponse;

            Assert.True(response.records.Length == 1, "Bad record length");
            var records = response.records;

            var firstRecord = records[0];

            var dc = firstRecord.recordData[0];

            Assert.NotNull(dc);
        }

        [Test]
        public void JstorServiceIsUp()
        {
            var processor = new Restful.ParallelProcessor(new SearchResponseCallbacks());
            var stream = processor.InvokePoxWebRequest(@"http://dfr.jstor.org/sru/?operation=searchRetrieve&query=dc.subject+%3D+mandeville&version=1.1");
        }

        [Test]
        public void CanPersistJstorTypes()
        {
            var callbacks = GetCannedCallbacks();

            Save(callbacks.Response);
        }

        public void Save<T>(T item) where T : Entity
        {
            using (var trans = Session.BeginTransaction())
            {
                try
                {
                    Session.Save(item);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Console.WriteLine("Transaction exception: " + ex.Message);
                }
            }
        }

        private static SearchResponseCallbacks GetCannedCallbacks()
        {
            var callbacks = new SearchResponseCallbacks();
            var processor = new Restful.ParallelProcessor(callbacks);
            var file = new FileStream(@"Mandeville.xml", FileMode.Open);
            var reader = XmlReader.Create(file);
            processor.ParallelProcessXml(reader);
            var response = callbacks;
            //processor.Process(@"http://dfr.jstor.org/sru/?operation=searchRetrieve&query=dc.subject+%3D+mandeville&version=1.1");
            //processor.Process(@"http://dfr.jstor.org/sru/?operation=searchRetrieve&recordPacking=xml&version=1.1&startRecord=1&maximumRecords=784&resultSetTTL=&recordSchema=info:srw/schema/1/dc-v1.1&query=dc.subject%20=%20mandeville");
            return callbacks;
        }

        [Test(Description="Tests that the serialization format is still valid as it has changed in the past")]
        // Note: It changed.
        public void CanGetProperlyFormattedXmlFromJstor()
        {
            var callbacks = new SearchResponseCallbacks();
            var processor = new Restful.ParallelProcessor(callbacks);
            processor.ProcessWithCredentials(@"http://dfr.jstor.org/sru/?operation=searchRetrieve&query=dc.subject+%3D+mandeville&version=1.1", Search.Creds);
            Assert.NotNull(callbacks.Response.numberOfRecords, "Serialization error");
        }

        [Test(Description = "Test ensures search is returning results as expected")]
        public void SearchReturnsResults()
        {
            var search = new Search();
            var results = search.SearchAnywhere("mandeville's travels");
            Assert.AreEqual(1, results.Take(1).Count());
        }

        [TestCase(10, 1005)]
        public void CanScourMassiveSubject(int reportEvery, int maxResults)
        {
            var search = new Search();
            var results = search.SearchBySubject("english");

            var count = 0;
            foreach (var result in results)
            {
                Save(result);

                if (count > maxResults) return;
                if (count % reportEvery == 0)
                {
                    Console.WriteLine("Imported " + count);
                }
                count++;
            }
            Assert.Greater(count, maxResults);
        }

        [Test]
        public void GetCitationsXml()
        {
            var article = new Resource(@"10.2307/1618158");
            var results = article.CitationsXml;
            Assert.IsNotEmpty(results);
        }

        [Test]
        public void EscapeQueryWorksProperly()
        {
            var result = Search.EscapeString("dc.subject", "mandeville");
            Assert.IsTrue(result.Contains("%3D"));
        }

        [Test]
        public void ExtensionMethodReturnsCitations()
        {
            var search = new Search();
            var results = search.SearchAnywhere("mandeville's travels");
            var citations = results.First().Citations().ToList();
            Assert.Greater(citations.Count, 0);
        }
    }
}