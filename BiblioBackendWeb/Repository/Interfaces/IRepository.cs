using BiblioBackendWeb.Utils;
using System.Linq.Expressions;

namespace BiblioBackendWeb.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null,Pagination page=null);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);    
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
