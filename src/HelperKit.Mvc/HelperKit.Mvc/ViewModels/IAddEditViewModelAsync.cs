using HelperKit.Mvc.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelperKit.Mvc.ViewModels
{
    public interface IAddEditViewModelAsync<C, T> where C : BaseDataContext where T : class
    {
        Task Register(C dataContext, T model, FormCollection frm = null);
    }
}