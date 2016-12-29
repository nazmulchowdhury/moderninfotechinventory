using System;
using System.Linq;
using Data.Helper;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private ModernInfoTechInventoryContext context;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ModernInfoTechInventoryContext Context
        {
            get
            { 
                return context ?? (context = DbFactory.Init());
            }
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = Context.Set<T>();
        }

        public virtual T Add(T entity)
        {
            dbSet.Add(entity);
            context.Commit();
            return entity;
        }

        public virtual T Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.Commit();
            return entity;
        }

        public virtual bool Delete(string id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                context.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                dbSet.Remove(obj);
                context.Commit();
                return true;
            }
            return false;
        }

        public virtual T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual ICollection<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual ICollection<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }    
    }
}
