



using System;
using System.Text;
using System.Threading.Tasks;
using Auth.Core.Common;
using Auth.Core.Dao;
using Auth.Core.Services;

namespace Auth.Core.Factory
{

    public class EmailFactory
    {

        public async static Task<SendGrid.Response> AccountVerification(Client client, string email, string fullName, string userName, string code)
        {
            try
            {
                var baseUrl = client != null && string.IsNullOrEmpty(client.BaseUrl)? client.BaseUrl : AppSetting.BaseUrl;

                string url = $"{baseUrl}/auth/verify/{userName}/{code}";

                var body = new StringBuilder();

                body.Append(url);

                var res = await EmailService.Send(new EmailComponent
                {
                    FromEmail = AppSetting.SystemEmail,
                    FromName = AppSetting.SystemAdmin,
                    ToEmail = email,
                    ToName = fullName,
                    Subject = "Verify You Account",
                    TextBody = body.ToString()
                });

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async static Task<SendGrid.Response> ResetPassword(Client client, string code, string fullName, string userName, string email)
        {
            try
            {
                string url = $"{client.BaseUrl??AppSetting.BaseUrl}/auth/resetpassword/{userName}/{code}";

                var body = new StringBuilder();

                body.Append(url);

                var res = await EmailService.Send(new EmailComponent
                {
                    ToName = fullName,
                    ToEmail = email,
                    FromEmail = AppSetting.SystemEmail,
                    FromName = AppSetting.SystemAdmin,
                    Subject = "Reset Password",
                    TextBody = body.ToString()
                });

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class EmailComponent
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string TextBody { get; set; }
        public byte[] Attachment { get; set; }
    }

}