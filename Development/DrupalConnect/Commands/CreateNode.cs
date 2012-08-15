using System.Net;
using DrupalConnect.Models;


namespace DrupalConnect.Commands
{
    public class CreateNode : DrupalCommand
    {
        public CreateNode(DrupalConnector connection, CreateNodeModel model)
            : base(connection)
        {
        }

        public HttpStatusCode Execute()
        {
            Connector.Post()

        }
    }
}