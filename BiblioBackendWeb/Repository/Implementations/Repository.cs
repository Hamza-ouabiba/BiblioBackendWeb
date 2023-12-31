using BiblioBackendWeb.Utils;
using BiblioBackendWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BiblioBackendWeb.Repository.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        private DbSet<TEntity> _dbset;
        public Repository(DbContext _context)
        {
            this._context = _context;
            _dbset = _context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbset.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null,Pagination page=null)
        {
            IQueryable<TEntity> query = _dbset;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(!String.IsNullOrEmpty(includeProperties))
            {
                string[] properties = includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries);

                foreach(string property in properties)
                    query = query.Include(property);
            }

            if(page != null)
            {
                query = query.Skip(page.PageIndex);
            }

            return query.ToList();
        }


        public TEntity Get(int id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbset.ToList();
        }

        public void Remove(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbset.RemoveRange(entities);
        }
    }
}
