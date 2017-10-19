using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure.Identity.Service
{
    class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            MailMessage mailMessage= new MailMessage
            {
                Subject=message.Subject,
                Body=message.Body,
                IsBodyHtml=true
            };
            mailMessage.To.Add(new MailAddress(message.Destination));
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.SendAsync(mailMessage, null);
            return Task.FromResult(0);
        }
    }
}
