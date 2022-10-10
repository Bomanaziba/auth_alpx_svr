


using System;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Factory;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Auth.Core.Services
{

    public class EmailService
    {
        public static async Task<SendGrid.Response> Send(EmailComponent _template)
        {
            try
            {
                string apiKey = AppSetting.EmailAPIKey;

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_template.FromEmail, _template.FromName);
                var to = new EmailAddress(_template.ToEmail, _template.ToName);

                if (string.IsNullOrEmpty(_template.HtmlBody)) _template.HtmlBody = null;

                var msg = MailHelper.CreateSingleEmail(from, to, _template.Subject, _template.TextBody, _template.HtmlBody);

                var response = await client.SendEmailAsync(msg);

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}