using System.Collections.Generic;
using System.Text;
using Laobian.Share.Model;
using RabbitMQ.Client;

namespace Laobian.Share.Data.Message
{
    public interface IRabbitMqClient
    {
        void Publish(string queueName, string message);

        List<string> Consume(string queueName);
    }

    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly IModel _channel;

        public RabbitMqClient(Config config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.RabbitMqHostName,
                UserName = config.RabbitMqUserName,
                Password = config.RabbitMqPassword
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void Publish(string queueName, string message)
        {
            _channel.QueueDeclare(queueName, true, false, false);
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(string.Empty, queueName, properties, Encoding.UTF8.GetBytes(message));
        }

        public List<string> Consume(string queueName)
        {
            var results = new List<string>();
            _channel.QueueDeclare(queueName, true, false, false);
            while (true)
            {
                var result = _channel.BasicGet(queueName, true);
                if (result == null)
                {
                    break;
                }

                results.Add(Encoding.UTF8.GetString(result.Body));
            }

            return results;
        }
    }
}
