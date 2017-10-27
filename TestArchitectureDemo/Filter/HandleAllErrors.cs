using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestArchitectureDemo.Filter
{
    public class HandleAllErrors : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            var exceptionType = exception.GetType().Name;
            

            if (!filterContext.ExceptionHandled)
            {
                bool isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest() == true ? true : false;
                if (isAjaxRequest)
                {
                    filterContext.Result = HandleJsonResult(filterContext.Exception, exception.Message);
                    filterContext.ExceptionHandled = true;
                }
            }
            if (!filterContext.ExceptionHandled)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary()
                };
                filterContext.ExceptionHandled = true;
            }
        }

        private JsonResult HandleJsonResult(Exception exception, string errorMessage = "")
        {
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Errors = string.IsNullOrEmpty(errorMessage) ? "Some Issue found" : errorMessage,
                    ResponseStatus = false
                }
            };
        }
    }
}