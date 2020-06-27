using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperKit.Models
{
    public interface IServiceResponse
    {
        string Message { get; set; }
        bool Error { get; set; }
    }

    public interface IServiceResponse<T> : IServiceResponse where T : class
    {
        T Result { get; set; }
    }
}
