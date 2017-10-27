using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestArchitecture.Domain;
using System.Linq.Expressions;
using TestArchitecture.Repository;
using TestArchitectureDemo.Context;


namespace TestArchitecture.Service
{
    public class BaseService<T> : IService<T> where T: Entity
    {
        readonly BaseRepository<T> repository;

        public BaseService(IUnitOfWork uow)
        {
            repository = new BaseRepository<T>(uow);
        }

        public IEnumerable<T> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public void AddOrUpdate(T item)
        {
            repository.AddOrUpdate(item);
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return repository.Contains(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return repository.FindAll(predicate);
        }

       

        public T GetById(long id)
        {
            return repository.GetById(id);
        }


    }
}
