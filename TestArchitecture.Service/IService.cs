using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using TestArchitecture.Domain;

namespace TestArchitecture.Service
{
    public interface IService<T> where T: Entity
    {
        T GetById(long id);
        IEnumerable<T> GetAll();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        bool Contains(Expression<Func<T, bool>> predicate);
        void AddOrUpdate(T item);
    }
}
