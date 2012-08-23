using System;
using System.Net;
using DrupalConnect.Models;
using Newtonsoft.Json.Linq;


namespace DrupalConnect.Commands
{
    public class CreateNode : DrupalCommand
    {
        private readonly CreateNodeModel _model;
        private readonly Lazy<JObject> _result; 

        public CreateNode(DrupalConnector connection, CreateNodeModel model)
            : base(connection)
        {
            _model = model;
            _result = new Lazy<JObject>( () => ComputeValue() );
        }

        public JObject ComputeValue()
        {
            var u = BibliopediaUrls.CreateNode;
            var result = Connector.Post(u, _model.ToPostData());
            return result;
        }

        public override HttpStatusCode Execute()
        {
            try
            {
                var doSideEffect = _result.Value;
                return HttpStatusCode.OK;
            }
            catch (WebException e)
            {
                // Side effect for debugging
                if (e == null) return HttpStatusCode.NotAcceptable;
                throw;
            }
        }
    }
}