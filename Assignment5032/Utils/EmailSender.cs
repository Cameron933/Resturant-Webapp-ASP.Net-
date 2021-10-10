using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Assignment5032.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";


        public async Task Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("778286323@qq.com", "FIT5032 Example Email User");
            var to = new EmailAddress(toEmail, "778286323@qq.com");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            msg.AddAttachment(@"~\Content\Attachment\thx.jpg", "Testing", null, null, null);

            var response = await client.SendEmailAsync(msg);
        }
    }
}