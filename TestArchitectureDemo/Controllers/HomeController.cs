using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestArchitecture.Service;
using TestArchitectureDemo.Context;
using TestArchitectureDemo.Models;
using TestArchitecture.Domain.DTO;
using TestArchitectureDemo.Filter;

namespace TestArchitectureDemo.Controllers
{
    [HandleAllErrors]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {  
            return View();
        }

        /// <summary>
        /// This method gets the property data
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPropertyData()
        {
            //throw new HttpException(404, "Product not found with given id");

          // throw new Exception("Not able to retrieve the data");
           
            var work = UnitOfWork.Begin();
            var dataService = new DataService(work);
            var lstProperties = dataService.GetAllPropertiesData();
            var lstPropertyViewModels = GetPropertiesViewModel(lstProperties);
            return Json(new { properties = lstPropertyViewModels }, JsonRequestBehavior.AllowGet);
          
        }

        /// <summary>
        /// This method converts the list of propertyInfo to view model list.
        /// </summary>
        /// <param name="lstProperties"></param>
        /// <returns></returns>
        private List<PropertyViewModel> GetPropertiesViewModel(List<PropertyInfo> lstProperties)
        {
            List<PropertyViewModel> listProperties = new List<PropertyViewModel>();

            foreach(var property in lstProperties)
            {
                listProperties.Add(MapPropertyInfoToViewModel(property));
            }
            return listProperties;
        }

        /// <summary>
        /// This method converts the propertyInfo DTO to view model. 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private PropertyViewModel MapPropertyInfoToViewModel(PropertyInfo propertyInfo)
        {
            return new PropertyViewModel
            {
                PropertyId = propertyInfo.PropertyId,
                Address1 = propertyInfo.Address.Address1,
                Address2 = propertyInfo.Address.Address2,
                City = propertyInfo.Address.City,
                Country = propertyInfo.Address.Country,
                County = propertyInfo.Address.County,
                District = propertyInfo.Address.District,
                State = propertyInfo.Address.State,
                ZipCode = propertyInfo.Address.ZipCode,
                ZipPlus4 = propertyInfo.Address.ZipPlus4,
                YearBuilt = propertyInfo.YearBuilt,
                ListPrice = propertyInfo.ListPrice.HasValue ? Convert.ToDecimal(propertyInfo.ListPrice.Value.ToString("###0.00", System.Globalization.CultureInfo.InvariantCulture)): propertyInfo.ListPrice,
                MonthlyRent = propertyInfo.MonthlyRent,
                GrossYield = propertyInfo.GrossYield.HasValue ? Convert.ToDecimal(propertyInfo.GrossYield.Value.ToString("###0.00", System.Globalization.CultureInfo.InvariantCulture)) : propertyInfo.GrossYield,                 
            };
        }
        

        /// <summary>
        /// This method converts the view model to DTO
        /// </summary>
        /// <param name="propertyViewModel"></param>
        /// <returns></returns>
        private PropertyInfo MapViewModelToPropertyInfo(PropertyViewModel propertyViewModel)
        {
            return new PropertyInfo
            {
                PropertyId = propertyViewModel.PropertyId,
                Address = new AddressInfo
                {
                    Address1 = propertyViewModel.Address1,
                    Address2 = propertyViewModel.Address2,
                    City = propertyViewModel.City,
                    Country = propertyViewModel.Country,
                    County = propertyViewModel.County,
                    District = propertyViewModel.District,
                    State = propertyViewModel.State,
                    ZipCode = propertyViewModel.ZipCode,
                    ZipPlus4 = propertyViewModel.ZipPlus4,
                }, 
                YearBuilt = propertyViewModel.YearBuilt,
                ListPrice = propertyViewModel.ListPrice,
                MonthlyRent = propertyViewModel.MonthlyRent,
                GrossYield = propertyViewModel.GrossYield
            };
        }


        /// <summary>
        /// This method saves property data to DB.
        /// </summary>
        /// <param name="modelData">view model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePropertyData(PropertyViewModel modelData)
        {
            var work = UnitOfWork.Begin();
            var propertyService = new PropertyService(work);
            propertyService.AddProperty(MapViewModelToPropertyInfo(modelData));
            work.Commit();

            return Json(new { Success = "success" });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}