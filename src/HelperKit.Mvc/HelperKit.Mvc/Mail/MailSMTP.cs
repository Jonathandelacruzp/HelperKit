using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace HelperKit.Mvc.Mail
{
    public class MailSMTP : IBaseMail, IDisposable
    {
        private readonly string _host;
        private readonly Int32 _port;
        private readonly string _emailFromPwd;
        private readonly SmtpClient _smtpClient;
        private readonly string _emailFrom;
        private readonly string _emailFromName;

        /// <summary>
        /// "SMTP_FROM", "SMTP_FROM_PWD", "SMTP_FROM_NAME", "SMTP_HOST", "SMTP_PORT"
        /// </summary>
        public MailSMTP()
        {
            _emailFrom = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_FROM");
            _emailFromPwd = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_FROM_PWD");
            _emailFromName = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_FROM_NAME");
            _host = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_HOST");
            _port = System.Configuration.ConfigurationManager.AppSettings.Get("SMTP_PORT").ToInteger(25);

            _smtpClient = new SmtpClient(_host)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailFrom, _emailFromPwd),
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Timeout = 10000
            };
        }

        public void SendMultipleEmail(List<Tuple<string, string>> emails, string subject, string body)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailFrom)
                };
                foreach (var email in emails)
                    mailMessage.To.Add(email.Item1);

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                _smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SendSingleEmail(string email, string name, string subject, string body)
        {
            var emails = new List<Tuple<string, string>>();
            emails.Add(new Tuple<string, string>(email, name));
            SendMultipleEmail(emails, subject, body);
        }

        public void SendSingleEmail(string email, string subject, string body) => SendSingleEmail(email, string.Empty, subject, body);

        public void SendMultipleEmail(List<string> emails, string subject, string body) => SendMultipleEmail(emails.Select(x => new Tuple<string, string>(x, string.Empty)).ToList(), subject, body);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}