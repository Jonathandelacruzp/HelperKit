using System.Web.Mvc.Html;

namespace HelperKit.Mvc
{
    public interface IBaseFormBuilder
    {
        MvcForm BeginForm(object obj);
    }
}