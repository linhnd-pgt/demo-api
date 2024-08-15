using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories.Base
{
    // common repository class where futer repository can implements
    public interface IRepositoryBase<T>
    {

        // IQueryable provides functionality to evaluate queries against a specific datasource
        // wherein the type of the data is known
        IQueryable<T> FindAll();

        // Expression<Func<T, bool>> represents a strongly typed lambda epxression as a data structure
        // in the form of an expression tree

        // Func<T, bool> returns a function that takes an object as the parameter
        // and return bool as the result
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        public readonly RepositoryContext _dbContext;

        public RepositoryBase(RepositoryContext context)
        {
            this._dbContext = context;
        }

        public IQueryable<T> FindAll() => _dbContext.Set<T>().AsNoTracking<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition) => _dbContext.Set<T>().Where(condition).AsNoTracking();

        public void Create(T entity) => _dbContext.Add(entity);

        public void Update(T entity) => _dbContext.Update(entity);

        public void Delete(T entity) => _dbContext.Remove(entity);

      
    }
}
