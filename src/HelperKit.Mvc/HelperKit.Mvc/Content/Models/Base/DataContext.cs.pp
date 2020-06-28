using HelperKit.Mvc.Models;
using $rootnamespace$.Controllers;
using System;
using System.Collections.Generic;
using System.Web;

namespace $rootnamespace$.Models.Base
{
	public class DataContext : BaseDataContext
	{
		public override HttpSessionStateBase Session { get; set; }
		public override HttpContextBase HttpContext { get; set; }
		public override string CurrentCulture { get; set; }
		public new BaseController Controller { get; set; }
		//public new DBContextEntities Context { get; set; }
	}
}
