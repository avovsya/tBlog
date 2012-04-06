using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using tBlog.Data.Abstract;
using tBlog.Domain;

namespace tBlog.Data.Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        private BlogDbContext _dbContext;
        private IDbSet<T> _dbSet;
        private IDatabaseFactory _databaseFactory;

        protected BlogDbContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = _databaseFactory.Get());
            }
        }

        public BaseRepository(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
            _dbSet = DbContext.Set<T>();
        }

        public T GetById(int id, params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = _dbSet.Where(e => e.Id == id);
            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set.SingleOrDefault();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions)
        {
            var set = _dbSet.AsQueryable();
            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }

        public void Save(T entity)
        {
            if (entity.Id == 0)
            {
                SetEntityTimestamps(entity, isNew: true);
                _dbSet.Add(entity);
            }
            else
            {
                SetEntityTimestamps(entity, isNew: false);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        protected void SetEntityTimestamps(T entity, bool isNew)
        {
            if (entity is EntityWithTimestamps)
            {
                (entity as EntityWithTimestamps).UpdatedAt = DateTime.Now;
                if (isNew)
                    (entity as EntityWithTimestamps).CreatedAt = DateTime.Now;
            }
        }
    }
}
