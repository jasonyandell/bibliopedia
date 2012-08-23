using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using DrupalConnect.Commands;
using DrupalConnect.Models;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace DrupalConnect
{
    [Binding]
    public class BasicSteps
    {
        private string nid;
        private DrupalConnector connector;
        private JObject result;

        [Given("a connection")]
        public void GivenAConnection()
        {
            this.connector = new DrupalConnector();
            Set(connector);
        }

        [Given("the node (.*)")]
        public void GivenTheNode(string nid)
        {
            this.nid = nid;
        }

        [Given("credentials")]
        public void GivenCredentials()
        {
            connector.Credentials("bibliobot", "biblio$Bot0!");
        }

        [When("I fetch that node")]
        public void WhenIFetchThatNode()
        {
            this.result = connector.GetNode(nid);
        }

        [When("I perform a basic get")]
        public void WhenIPerformABasicGet()
        {
            //TODO: implement act (action) logic

            ScenarioContext.Current.Pending();
        }

        [When("I can perform a basic get")]
        public void WhenICanPerformABasicGet()
        {
            this.result = connector.GetNode("224");
        }

        //[When("I authenticate with bibliopedia")]
        //public void WhenIAuthenticateWithBibliopedia()
        //{
        //    //TODO: implement act (action) logic

        //    ScenarioContext.Current.Pending();
        //}

        //[Then("")]
        //public void Then()
        //{
        //    //TODO: implement assert (verification) logic

        //    ScenarioContext.Current.Pending();
        //}

        private void Set<T>(T item)
        {
            ScenarioContext.Current.Set(item);
        }

        private T Get<T>()
        {
            T value;
            var success = ScenarioContext.Current.TryGetValue(out value);
            if (success) return value;
            throw new InstanceNotFoundException(typeof (T).FullName);
        }

        [Given(@"sample data")]
        public void GivenSampleData()
        {
            Set(TestMother.TestNode);
        }

        [Then(@"I can create a new node")]
        public void ThenICanCreateANewNode()
        {
            var connection = Get<DrupalConnector>();

            var command = new CreateNode(connection, Get<CreateNodeModel>());
            Set(command);

            command.Execute();
        }

        [Then("I get some data back")]
        public void ThenIGetSomeDataBack()
        {
            Assert.IsTrue(result != null, "Result was empty");
        }


    }
}
