using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitecture.Domain
{
    public abstract class Entity : IEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        protected Entity()
        {
            var today = DateTime.Now;            
            CreatedDate = today;
            CreatedBy = string.Empty;            
        }
    }
}
