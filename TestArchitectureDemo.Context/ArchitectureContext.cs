using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.Common;
using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;
using System.Threading;
using TestArchitecture.Domain;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace TestArchitectureDemo.Context
{
    public class ArchitectureContext : DbContext
    {
        const string CONTEXT_NAME = "ArchitectureManagement";
        
        private string identity;

        public ArchitectureContext() : base(CONTEXT_NAME) { }

        public ArchitectureContext(string connectionString) : base(connectionString) { }


        public void ForceIdentity(string name)
        {
            identity = name;
        }

        public DbSet<Property> Properties { get; set; }

        ///// <summary>
        ///// In case you want to check the SQL Statement and Entity Validation Errors
        ///// </summary>
        ///// <returns></returns>
        //public override int SaveChanges()
        //{
        //    var user = identity ?? Thread.CurrentPrincipal.Identity.Name ?? string.Empty;
        //    var now = DateTime.Now;

        //}

        public ObjectContext GetObjectContext()
        {
            return (this as IObjectContextAdapter).ObjectContext;
        }

        public override int SaveChanges()
        {
            var user = identity ?? Thread.CurrentPrincipal.Identity.Name ?? string.Empty;
            var now = DateTime.Now;

            foreach(var item in ChangeTracker.Entries().Where(s =>
                                    s.State == EntityState.Added ||
                                    s.State == EntityState.Deleted ||
                                    s.State == EntityState.Modified))
            {
                var entityState = item.State;
                var entity = (Entity)item.Entity;

                if (item.State == EntityState.Added)
                {
                    entity.CreatedBy = user;
                    entity.CreatedDate = now;
                    
                }                
                item.State = entityState;

            }

            var errorCode = default(int);
            try
            {
                //var sqlStatements = CustomExtensions.ToTraceString(this.GetObjectContext());
                //Debug.WriteLine(sqlStatements);
                errorCode = base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Debug.WriteLine(e.Message);
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }

                    throw;
                }
            }
            return errorCode;
        }

            /*
            public override int SaveChanges()
            {
                var user = identity ?? Thread.CurrentPrincipal.Identity.Name ?? string.Empty;
                var now = DateTime.Now;


                foreach (var item in ChangeTracker.Entries().Where(s =>
                                         s.State == EntityState.Added ||
                                         s.State == EntityState.Deleted ||
                                         s.State == EntityState.Modified))
                {
                    var entityState = item.State;
                    var entity = (Entity)item.Entity;

                    if (entity.IsTransient())
                    {
                        entity.CreatedBy = user;
                        entity.CreatedDate = now;
                        entity.ModifiedDate = now;
                        entity.ModifiedBy = user;
                    }
                    if (entity.RowState == RowState.TransientUpdate)
                    {
                        entity.ModifiedDate = now;
                        entity.ModifiedBy = user;
                        entity.RowState = RowState.Update;
                    }
                    else if (entity.RowState == RowState.TransientDelete)
                    {
                        entity.ModifiedDate = now;
                        entity.ModifiedBy = user;
                        entity.RowState = RowState.Delete;
                    }
                    item.State = entityState;

                }

                var errorCode = default(int);
                try
                {
                    //var sqlStatements = CustomExtensions.ToTraceString(this.GetObjectContext());
                    //Debug.WriteLine(sqlStatements);
                    errorCode = base.SaveChanges();
                }

                catch (DbEntityValidationException e)
                {
                    Debug.WriteLine(e.Message);
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }

                        throw;
                    }
                }
                return errorCode;
            }
            */
        }
}
