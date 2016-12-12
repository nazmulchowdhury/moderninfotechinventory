using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);
        // Marks an entity as modified
        T Update(T entity);
        // Marks an entity to be removed
        bool Delete(string id);
        // Marks entity to be removed using delegate
        bool Delete(Expression<Func<T, bool>> where);
        // Get an entity by int id
        T GetById(string id);
        // Get an entity using delegate
        T Get(Expression<Func<T, bool>> where);
        // Gets all entities of type T
        ICollection<T> GetAll();
        // Gets entities using delegate
        ICollection<T> GetMany(Expression<Func<T, bool>> where);
    }
}
