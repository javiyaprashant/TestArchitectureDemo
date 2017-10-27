using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestArchitecture.Domain.DTO;

namespace TestArchitecture.Infrastructure
{
    public class ExternalDataService
    {
        /// <summary>
        /// This method gets the properties data by connecting json URL.
        /// </summary>
        /// <returns></returns>
        public List<PropertyInfo> GetAllPropertiesData()
        {
            var rootData = PropertyWebClient.GetData();

            List<PropertyInfo> lstProperties = new List<PropertyInfo>();
            PropertyInfo property;
            foreach (var item in rootData)
            {
                property = new PropertyInfo();

                property.PropertyId = item.id;
                property.Address = new AddressInfo
                {
                    Address1 = item.address.address1,
                    Address2 = item.address.address2,
                    City = item.address.city,
                    Country = item.address.country,
                    County = item.address.county,
                    District = item.address.district != null ? item.address.district.ToString() : string.Empty,
                    State = item.address.state,
                    ZipCode = item.address.zip,
                    ZipPlus4 = item.address.zipPlus4 != null ? item.address.zipPlus4 : string.Empty
                };

                if(item.physical != null)
                {
                    property.YearBuilt = item.physical.yearBuilt;
                }
                if(item.financial != null)
                {
                    property.ListPrice = Convert.ToDecimal(item.financial.listPrice);
                    property.MonthlyRent = Convert.ToDecimal(item.financial.monthlyRent);                    
                }
                lstProperties.Add(property);
            }

            return lstProperties;
        }
    }
}
