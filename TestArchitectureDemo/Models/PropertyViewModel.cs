using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestArchitectureDemo.Models
{
    public class PropertyViewModel
    {
        public long PropertyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipPlus4 { get; set; }
        public int? YearBuilt { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? MonthlyRent { get; set; }
        public decimal? GrossYield { get; set; }
    }
}