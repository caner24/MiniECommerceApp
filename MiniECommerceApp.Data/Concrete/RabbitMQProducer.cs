using MiniECommerceApp.Core.CrosssCuttingConcerns.MailService;
using MiniECommerceApp.Data.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Concrete
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("tokens", durable: true, exclusive: false, autoDelete: false, arguments: null);
            byte[] bytemessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            channel.BasicPublish(exchange: "", routingKey: "tokens", basicProperties: properties, body: bytemessage);
        }
    }
}
