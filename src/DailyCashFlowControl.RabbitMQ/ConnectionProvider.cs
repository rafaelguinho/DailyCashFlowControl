using RabbitMQ.Client;

namespace DailyCashFlowControl.RabbitMQ
{
    public class ConnectionProvider
    {
        protected ConnectionFactory _connectionFactory;
        private IConnection _connection;

        public ConnectionProvider(string uri)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(uri),
                DispatchConsumersAsync = true
            };
        }

        public IConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            return _connection;
        }
    }
}
