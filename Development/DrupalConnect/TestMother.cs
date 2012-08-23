using System;
using DrupalConnect.Models;

namespace DrupalConnect
{
    public static class TestMother
    {
        public static CreateNodeModel TestNode
        {
            get { return new CreateNodeModel("Test Title", "Test Date Published: " + DateTime.Now); }
        }
    }
}