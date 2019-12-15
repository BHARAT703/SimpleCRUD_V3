using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleCRUD.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleCRUD.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : class, new()
    {
        IEnumerable<T> GetAllTheRecords();
        IEnumerable<T> GetAllTheRecords(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAllTheRecords(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T GetSingle(int id);
        T GetSingle(string id);
        T GetSingle(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T Add(T entity);
        T Update(T entity);
        T SoftDelete(T entity);
        T HardDelete(T entity);
        int Count();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly ApplicationContext context;
        public BaseRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<T> GetAllTheRecords()
        {
            return context.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> GetAllTheRecords(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            return query.AsEnumerable();
        }
        public virtual IEnumerable<T> GetAllTheRecords(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            return query.AsEnumerable();
        }

        /// <summary>
        /// This method will return enity itself for given Id
        /// foreign key entities will not be mapped
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetSingle(int id) => context.Set<T>().Find(id);

        public virtual T GetSingle(string id) => context.Set<T>().Find(id);

        /// <summary>
        /// This method will return enity itself for given condition using lamda prediction.
        /// foreign key entities will not be mapped
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> predicate) => context.Set<T>().FirstOrDefault(predicate);
        /// <summary>
        /// This method will return enity for given condition using lamda prediction.
        /// foreign key entities will be mapped, have to pass includeProperties to make it working
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual T Add(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            context.Set<T>().Add(entity);
            dbEntityEntry.State = EntityState.Added;
            return entity;
        }

        public virtual T Update(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            return entity;
        }

        public virtual T SoftDelete(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            return entity;
        }

        public virtual T HardDelete(T entity)
        {
            EntityEntry dbEntityEntry = context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
            return entity;
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public virtual int Count() => context.Set<T>().Count();
        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate) => context.Set<T>().Where(predicate);
    }
}
