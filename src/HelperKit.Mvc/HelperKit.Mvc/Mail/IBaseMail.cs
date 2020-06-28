using System;
using System.Collections.Generic;

namespace HelperKit.Mvc.Mail
{
    public interface IBaseMail
    {
        void SendMultipleEmail(List<Tuple<string, string>> emails, string subject, string body);

        void SendSingleEmail(string email, string name, string subject, string body);

        void SendMultipleEmail(List<string> emails, string subject, string body);
    }
}