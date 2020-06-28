using HelperKit.Mvc;
using $rootnamespace$.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$.Controllers
{
    public class BaseController : HelperController
    {
        private DataContext _dataContext;
        protected DataContext DataContext { get { return this.SetDataContext(); } }

        public BaseController() : base()
        {
        }

        public override void InvalidateContext()
        {            
        }

        private DataContext SetDataContext()
        {
            if (this._dataContext == null)
                this._dataContext = new DataContext() {
                    Controller = this, CurrentCulture = this._CurrentCulture, HttpContext = HttpContext, Session = Session //, Context = this._Context, Email = this._Email
                };
            return this._dataContext;
        }
    }
}
