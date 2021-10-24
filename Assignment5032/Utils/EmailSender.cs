using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Assignment5032.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "";

        // bulk emial send by sendgrid
        // Reference:https://stackoverflow.com/questions/68850989/sending-an-email-batch-with-sendgrid-v3?noredirect=1&lq=1
        // Reference:https://microsoft.github.io/AzureTipsAndTricks/blog/tip73.html
        public async Task<Tuple<string, string, string>> SendAsync(string[] toEmails, String subject, String contents)
        {
            try
            {
                var client = new SendGridClient(API_KEY);
                var fileP = "~/Content/Attachment/thx.jpg";
                var messageEmail = new SendGridMessage()
                {
                    From = new EmailAddress("huangchu103@outlook.com", "YuLong Restaurant"),
                    Subject = subject,
                    PlainTextContent = contents,
                    HtmlContent = "<p>" +contents + "</p>",
                };
                foreach (var email in toEmails)
                {
                    messageEmail.AddTo(new EmailAddress(email));
                }
                var bytes = File.ReadAllBytes(fileP);
                var file = Convert.ToBase64String(bytes);
                messageEmail.AddAttachment("attachment-file",file);

                var response = await client.SendEmailAsync(messageEmail);
                return new Tuple<string, string, string>(response.StatusCode.ToString(), response.Headers.ToString(), response.Body.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}