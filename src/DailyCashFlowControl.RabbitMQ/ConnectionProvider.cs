using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCashFlowControl.RabbitMQ
{
    public class ConnectionProvider
    {
        protected ConnectionFactory _connectionFactory;
        private IConnection _connection;

        public ConnectionProvider(string uri)
        {
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(uri)
            };

            _connectionFactory.DispatchConsumersAsync = true;
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
