using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace HelperKit.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Create(T entity);

        Task CreateAsync(T entity, CancellationToken cancellationToken = default);

        void Update(T entity);

        void Delete(T entity);

        void Edit(T entity);

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}