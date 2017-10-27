using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using TestArchitecture.Domain;

namespace TestArchitecture.DomainConfiguration
{
    public class PropertyConfiguration : EntityTypeConfiguration<Property>
    {
        public PropertyConfiguration()
        {
            Property(e => e.ListPrice).HasPrecision(18, 2);
            Property(e => e.MonthlyRent).HasPrecision(18, 2);
            Property(e => e.GrossYield).HasPrecision(18, 2);
        }
    }
}
