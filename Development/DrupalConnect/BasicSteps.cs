﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace DrupalConnect
{
    [Binding]
    public class BasicSteps
    {
        private string nid;
        private DrupalConnector connector;
        private string result;

        [Given("a connection")]
        public void GivenAConnection()
        {
            this.connector = new DrupalConnector();
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

        [When("I authenticate with bibliopedia")]
        public void WhenIAuthenticateWithBibliopedia()
        {
            //TODO: implement act (action) logic

            ScenarioContext.Current.Pending();
        }

        //[Then("")]
        //public void Then()
        //{
        //    //TODO: implement assert (verification) logic

        //    ScenarioContext.Current.Pending();
        //}
        [Then("I get some data back")]
        public void ThenIGetSomeDataBack()
        {
            Assert.IsTrue(result != "", "Result was empty");
        }
    }
}
