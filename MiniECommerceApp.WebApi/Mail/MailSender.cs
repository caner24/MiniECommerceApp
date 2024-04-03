using Microsoft.AspNetCore.Identity;
using MiniECommerceApp.Core.CrosssCuttingConcerns.MailService;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.MailBody;

namespace MiniECommerceApp.WebApi.Mail
{
    public class MailSender : IEmailSender<User>
    {
        private readonly MailModels _mailModels;
        private readonly IEmailSender _emailSender;
        public MailSender(IEmailSender emailSender)
        {
            _mailModels = new MailModels();
            _emailSender = emailSender;
        }
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            var message = new Message(new string[] { email }, "E-Posta Adresinizi Doğrulayın", _mailModels.EmailConfirmationModel(email.Substring(0, user.Email.IndexOf("@")), "Email Onaylama", "2 hour", confirmationLink.ToString()), null);
            await _emailSender.SendEmailAsync(message);
        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            var message = new Message(new string[] { email }, "Şifrenizi Sıfırlama Kodu", _mailModels.EmailConfirmationModel(email.Substring(0, user.Email.IndexOf("@")), "Şifre Sıfırlama", "2 hour", resetCode.ToString()), null);
            await _emailSender.SendEmailAsync(message);
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            var message = new Message(new string[] { email }, "Şifre Sıfırlama Linki", _mailModels.EmailConfirmationModel(email.Substring(0, user.Email.IndexOf("@")), "Şifre Sıfırlama", "2 hour", resetLink.ToString()), null);
            await _emailSender.SendEmailAsync(message);
        }
    }
}
