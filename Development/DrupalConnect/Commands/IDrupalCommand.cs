using System.Net;

namespace DrupalConnect.Commands
{
    public interface IDrupalCommand
    {
        DrupalConnector Connector { get; }

        HttpStatusCode Execute();
    }

    public abstract class DrupalCommand : IDrupalCommand
    {
        private readonly DrupalConnector _connector;

        protected DrupalCommand(DrupalConnector connector)
        {
            _connector = connector;
        }

        public DrupalConnector Connector
        {
            get { return _connector; }
        }

        public abstract HttpStatusCode Execute();
    }
}