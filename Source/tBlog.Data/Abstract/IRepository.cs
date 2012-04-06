using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using tBlog.Domain;

namespace tBlog.Data.Abstract
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(int id, params Expression<Func<T, object>>[] includeExpressions);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);
        
        void Save(T entity);

        void Delete(T entity);

    }
}
