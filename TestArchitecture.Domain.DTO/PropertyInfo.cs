using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArchitecture.Domain.DTO
{
    public class PropertyInfo
    {
        public long PropertyId { get; set; }

        public AddressInfo Address { get; set; }

        public int? YearBuilt { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? MonthlyRent { get; set; }
        public decimal? GrossYield { get; set; }
        
    }
}
