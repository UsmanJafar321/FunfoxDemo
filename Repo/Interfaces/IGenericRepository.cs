using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Int
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int? skip = null, int? take = null);
        IEnumerable<TEntity> GetFromProc(string query);
        TEntity GetByID(object id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);
        int CountRecords(Expression<Func<TEntity, bool>> filter);
        bool CheckExistance(Expression<Func<TEntity, bool>> filter);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
