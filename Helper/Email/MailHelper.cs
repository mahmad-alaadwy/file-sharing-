using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace File_Sharing_proj004.Helper.Email
{
    public class MailHelper : IMailService
    {
        private readonly IConfiguration config;

        public MailHelper(IConfiguration config)
        {
            this.config = config;
        }
        public void SendEmail(InputEmailMessage model)
        {
            using (SmtpClient client = new SmtpClient(config.GetValue<string>("Mail:Host"), config.GetValue<int>("Mail:Port")))
            {
                client.EnableSsl = true;
                var msg = new MailMessage();

                msg.From = new MailAddress(config.GetValue<string>("Mail:From"), config.GetValue<string>("Mail:Sender"), System.Text.Encoding.UTF8);
                msg.Subject = model.Subject;
                msg.Body = model.Body;
                msg.To.Add(model.Email);


                client.Credentials = new System.Net.NetworkCredential(config.GetValue<string>("Mail:From"), config.GetValue<string>("Mail:PWD"));
                client.Send(msg);
            }
        }
    }
}
