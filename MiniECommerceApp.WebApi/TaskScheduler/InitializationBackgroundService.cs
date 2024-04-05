
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using MiniECommerceApp.Data.Abstract;
using Newtonsoft.Json;
using System.Text;
using MiniECommerceApp.Core.CrosssCuttingConcerns.MailService;
using MiniECommerceApp.WebApi.Mail;
using System.Net.Mail;
using MiniECommerceApp.Entity.MailBody;
using MiniECommerceApp.Entity;

namespace MiniECommerceApp.WebApi.TaskScheduler
{
    public class InitializationBackgroundService : IHostedService
    {
        private readonly IEmailSender _emailSender;
        private IModel _channel;

        public InitializationBackgroundService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory { HostName = "rabbitmq" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.QueueDeclare("tokens", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body;
                var jsonString = Encoding.UTF8.GetString(body.ToArray());
                var message = JsonConvert.DeserializeObject<MailModel>(jsonString);
                var messageSender = new Message(new string[] { message.Email }, message.Header, MailConfirmationModel.EmailConfirmationModel(message.Email.Substring(0, message.Email.IndexOf("@")), "Email Onaylama", "2 hour", message.ConfLink.ToString()), null);
                await _emailSender.SendEmailAsync(messageSender);
                _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue: "tokens", autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _channel?.Dispose();
            return Task.CompletedTask;
        }
    }
}

