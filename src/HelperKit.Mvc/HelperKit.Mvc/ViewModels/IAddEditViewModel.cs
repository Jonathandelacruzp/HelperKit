using HelperKit.Mvc.Models;
using System.Web.Mvc;

namespace HelperKit.Mvc.ViewModels
{
    public interface IAddEditViewModel<C, T> where C : BaseDataContext where T : class
    {
        void Register(C dataContext, T model, FormCollection frm = null);
    }
}