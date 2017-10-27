using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitectureDemo.Context
{
    public interface IUnitOfWork : IDisposable
    {
        ArchitectureContext Context { get; }
        void Commit();
        void ForceIdentity(string name);
    }
}
