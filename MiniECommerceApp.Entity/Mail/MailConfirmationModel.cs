using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.MailBody
{
    public static class MailConfirmationModel
    {
        public static string EmailConfirmationModel(string username, string header, string exdate, string link)
        {

            var htmlTemplate = @"""
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>{1}</title>
            </head>
            <body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">
                <table style=""max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
                    <tr>
                        <td style=""text-align: center;"">
                            <h2 style=""color: #333;"">{1}</h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>Hello, <strong>{0}</strong>,</p>
                            <p>We received a request to set up your account with the email address <strong>{1}</strong>. To complete the process, please click the button below to confirm your email:</p>
                            <p style=""text-align: center; padding: 20px 0;"">
                                <a href=""{2}"" style=""display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px;"">Confirm Email</a>
                            </p>
                            <p>This link will expire on <strong>{3}</strong>.</p>
                            <p>If you did not make this request, you can safely ignore this email.</p>
                        </td>
                    </tr>
                </table>
            </body>
            </html>""";
            string formattedHtml = string.Format(htmlTemplate, username, header, link, exdate);
            return formattedHtml;
        }
    }
}
