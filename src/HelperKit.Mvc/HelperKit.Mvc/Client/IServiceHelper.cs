using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelperKit.Mvc
{
    public interface IServiceHelper
    {
        Task<string> GetService(string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null);

        Task<string> PostService(string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null);

        Task<T> CreateObject<T>(FormMethod method, string serviceName, NameValueCollection reqParm, NameValueCollection headerParm = null) where T : class;
    }
}