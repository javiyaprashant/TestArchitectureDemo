using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestArchitecture.Domain;

namespace TestArchitecture.Repository
{
    public interface IRepository<T> where T:Entity
    {
        T GetById(long id);
        IQueryable<T> GetAll();        
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        bool Contains(Expression<Func<T, bool>> predicate);
        void AddOrUpdate(T item);
    }
}
