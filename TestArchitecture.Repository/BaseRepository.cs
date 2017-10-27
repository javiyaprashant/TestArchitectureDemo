using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TestArchitecture.Domain;
using TestArchitectureDemo.Context;



namespace TestArchitecture.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected ArchitectureContext Context { get; private set; }

        protected DbSet<T> DbSet { get; private set; }

        public BaseRepository(IUnitOfWork uow)
        {
            Context = uow.Context;
            DbSet = Context.Set<T>();
        }

        public T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public void AddOrUpdate(T item)
        {
            DbSet.Add(item);
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        

        
    }
}
