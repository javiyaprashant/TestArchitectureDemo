using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace TestArchitectureDemo.Context
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        ArchitectureContext context;
        bool disposed = false;

        public UnitOfWork(ArchitectureContext context)
        {
            this.context = context;            
        }

        public UnitOfWork()
        {
           context = new ArchitectureContext();               
        }
        public ArchitectureContext Context
        {
            get
            {
                return context;
            }
        }

        public static IUnitOfWork Begin()
        {            
            return new UnitOfWork(new ArchitectureContext());
        }

        public static void Commit(IUnitOfWork work)
        {
            work.Commit();
        }

        public void Commit()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                //saveFailed = true;
                //ex.Entries.Single().Reload();
            }
                       
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void ForceIdentity(string name)
        {
            Context.ForceIdentity(name);
        }

    }
}
