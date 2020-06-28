using HelperKit.Mvc.Models;
using System.Threading.Tasks;

namespace HelperKit.Mvc.ViewModels
{
    public interface IPagedListViewModelAsync<C, T> : IFilterViewModel
        where C : BaseDataContext
        where T : IFilterViewModel
    {
        int p { get; set; }

        Task Fill(C dataContext, int? p, T model);
    }
}