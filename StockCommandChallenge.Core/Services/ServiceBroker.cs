using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockCommandChallenge.Core.Interfaces;
using StockCommandChallenge.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockCommandChallenge.Core.Services
{
    public class ServiceBroker : IServiceBroker
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        private readonly AppSettings settings;

        private const string EXCHANGE_NAME = "stock";

        public ServiceBroker(IOptions<AppSettings> appSettings)
        {
            settings = appSettings.Value;
            InitializeBroker();
        }

        public void InitializeBroker()
        {
            _factory = new ConnectionFactory
            {
                HostName = settings.ServiceBrokerHostName,
                UserName = settings.ServiceBrokerUsername,
                Password = settings.ServiceBrokerPassword
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);
        }

        public void AddConsumer(string queueName, Action<string> onReceived)
        {
            _channel.QueueDeclare(queue: queueName, exclusive: false, autoDelete: true);

            _channel.QueueBind(queue: queueName,
                              exchange: EXCHANGE_NAME,
                              routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                onReceived(message);
            };
            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void SendBroadcast(string message)
        {
            _channel.BasicPublish(exchange: EXCHANGE_NAME,
                          routingKey: "",
                          basicProperties: null,
                          body: Encoding.UTF8.GetBytes(message));
        }

        public void Close()
        {
            if (_connection != null)
                _connection.Close();

            if (_channel != null)
                _channel.Close();
        }

        public void DeleteConsumer(string queueName)
        {
            _channel.QueueDelete(queueName);
        }
    }
}