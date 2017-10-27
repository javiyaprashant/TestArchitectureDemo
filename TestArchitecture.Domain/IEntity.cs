using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitecture.Domain
{
    public interface IEntity
    {
        string CreatedBy { get; }
        DateTime CreatedDate { get; }
    }
}
