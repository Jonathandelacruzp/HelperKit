using HelperKit.Mvc.Models;

namespace HelperKit.Mvc.ViewModels
{
    public interface IPagedListViewModel<C, T> : IFilterViewModel
        where C : BaseDataContext
        where T : IFilterViewModel
    {
        int p { get; set; }

        void Fill(C dataContext, int? p, T model);
    }
}