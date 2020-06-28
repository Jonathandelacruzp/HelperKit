using HelperKit.Mvc.Render;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HelperKit.Mvc.Mail
{
    public abstract class TemplatedEmail<T> : TemplateRender, IDisposable where T : IBaseMail, new()
    {
        protected T Email { get; set; }
        protected string Template { get; set; }
        //protected EncryptHelper _Encrypt { get; set; }

        public TemplatedEmail() : base()
        {
            this.Email = new T();
        }

        public TemplatedEmail(string filePath) : base(filePath)
        {
            this.Email = new T();
        }

        public virtual void SendEmail<C>(List<Tuple<string, string>> emails, string subject, object model) where C : Controller, new()
        {
            var body = Render<C>(this.Template, model);
            this.Email.SendMultipleEmail(emails, subject, body);
        }

        public virtual void SendEmail<C>(string email, string name, string subject, object model) where C : Controller, new()
        {
            var body = Render<C>(this.Template, model);
            this.Email.SendSingleEmail(email, name, subject, body);
        }

        public virtual void SendEmail<C>(List<string> emails, string subject, object model) where C : Controller, new()
        {
            var body = Render<C>(this.Template, model);
            this.Email.SendMultipleEmail(emails, subject, body);
        }

        public virtual void SendEmail<C>(string email, string subject, object model) where C : Controller, new() => this.SendEmail<C>(email, null, subject, model);

        public new void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}