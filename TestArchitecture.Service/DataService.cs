using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestArchitectureDemo.Context;
using TestArchitecture.Infrastructure;
using TestArchitecture.Domain.DTO;

namespace TestArchitecture.Service
{
    public class DataService
    {
        private readonly IUnitOfWork uow;

        public DataService(IUnitOfWork unitOfWork)
        {   
            uow = unitOfWork;
        }

        /// <summary>
        /// This method gets the properties data from infrastructer service
        /// </summary>
        /// <returns>properties</returns>
        public List<PropertyInfo> GetAllPropertiesData()
        {
            var externalDataService = new ExternalDataService();

            var properties = externalDataService.GetAllPropertiesData();

            foreach(var property in properties)
            {
                if (property.MonthlyRent != null && property.ListPrice != null)
                    property.GrossYield =  decimal.Round(Convert.ToDecimal(property.MonthlyRent * 12 / property.ListPrice),2);
            }

            return properties;
        }
    }
}
