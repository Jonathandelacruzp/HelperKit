using HelperKit.Mvc.Render;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HelperKit.Mvc.Mail
{
    public class TemplatedEmail : TemplateRender, IDisposable
    {
        private readonly IBaseMail _email;
        protected string Template { get; set; }
        //protected EncryptHelper _Encrypt { get; set; }

        public TemplatedEmail(IBaseMail mail) : base()
        {
            this._email = mail;
        }

        public TemplatedEmail(IBaseMail mail, string filePath) : base(filePath)
        {
            this._email = mail;
        }

        public virtual void SendEmail<C>(List<Tuple<string, string>> emails, string subject, object model) where C : Controller, new()
        {
            var body = Render<C>(this.Template, model);
            this._email.SendMultipleEmail(emails, subject, body);
        }

        public virtual void SendEmail<C>(string email, string name, string subject, object model) where C : Controller, new()
        {
            var body = Render<C>(this.Template, model);
            this._email.SendSingleEmail(email, name, subject, body);
        }

        public virtual void SendEmail<C>(List<string> emails, string subject, object model) where C : Controller, new()
        {
            var body = Render<C>(this.Template, model);
            this._email.SendMultipleEmail(emails, subject, body);
        }

        public virtual void SendEmail<C>(string email, string subject, object model) where C : Controller, new() => this.SendEmail<C>(email, null, subject, model);

        public new void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}