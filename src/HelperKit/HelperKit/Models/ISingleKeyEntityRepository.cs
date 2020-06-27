using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperKit.Models
{
    public interface ISingleKeyEntityRepository<T, K> : IRepository<T> where T : class
    {
        T GetSingle(K entityKey);
    }
}
