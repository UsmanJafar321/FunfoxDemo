using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Manager;
using Entity;
using Repo.Int;

namespace Repo.Int
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal FunfoxEntities Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(FunfoxEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int? skip = null, int? take = null)
        {
            try
            {


                IQueryable<TEntity> query = DbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (
                    var includeProperty in
                        includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (take.HasValue)
                {
                    query = skip.HasValue
                        ? query.Skip(skip.GetValueOrDefault()).Take(take.GetValueOrDefault())
                        : query.Take(take.GetValueOrDefault());
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExecption(ex);
                throw;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExecption(ex);
                throw;
            }
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                return query.FirstOrDefault(filter);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExecption(ex);
                throw;
            }
        }
        public int CountRecords(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                return query.Count(filter);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExecption(ex);
                throw;
            }
        }
        public bool CheckExistance(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                IQueryable<TEntity> query = DbSet;
                return query.Any(filter);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleExecption(ex);
                throw;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }


        public IEnumerable<TEntity> GetFromProc(string query)
        {
            using (DbContext context = new FunfoxEntities())
            {
                return context.Database.SqlQuery<TEntity>(query).ToList();

            }
        }
    }
}