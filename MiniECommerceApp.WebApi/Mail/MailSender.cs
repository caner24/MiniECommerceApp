using Microsoft.AspNetCore.Identity;
using MimeKit;
using MiniECommerceApp.Core.CrosssCuttingConcerns.MailService;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.MailBody;
using System.Collections.Generic;

namespace MiniECommerceApp.WebApi.Mail
{
    public class MailSender : IEmailSender<User>
    {
        private readonly IMessageProducer _messageProducer;
        public MailSender(IMessageProducer messageProducer)
        {
            _messageProducer = messageProducer;
        }
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            var model = new MailModel { Email = email, Header = "E-Posta Adresinizi Doğrulayın", ConfLink = confirmationLink };
            await Task.Run(() => _messageProducer.SendMessage(model));
        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            var model = new MailModel { Email = email, Header = "Şifre sıfırlama kodu", ConfLink = resetCode };
            await Task.Run(() => _messageProducer.SendMessage(model));
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            var model = new MailModel { Email = email, Header = "Şifre sıfırlama linki", ConfLink = resetLink };
            await Task.Run(() => _messageProducer.SendMessage(model));
        }
    }
}
