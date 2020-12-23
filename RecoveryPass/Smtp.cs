using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RecoveryPass
{
    public class Smtp
    {

        NameValueCollection collection;

        public SmtpClient Start(string password)
        {
            SmtpClient smtpClient = new SmtpClient();
            collection = ConfigurationManager.AppSettings;
            smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("sashakachkurwwa@gmail.com", password);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = (SmtpDeliveryMethod)Convert.ToInt32(collection["SmtpDeliveryMethod"]);
            smtpClient.Timeout = Convert.ToInt32(collection["SMTPTimeoutInMilliseconds"]);
            return smtpClient;
        }


        public MailMessage InitMailMessage(string receivers, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sashakachkurwwa@gmail.com");
            string[] mails = receivers.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string mail in mails)
            {
                mailMessage.To.Add(new MailAddress(mail));
            }
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            return mailMessage;
        }
    }
}
