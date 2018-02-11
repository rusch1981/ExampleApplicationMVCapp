using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExampleApplicationMVC.Utilities;

namespace ExampleApplicationMVC.Filters
{
    public class ErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
             EmailUtil emailUtil = new EmailUtil();
            try
            {
                emailUtil.SendEmail(filterContext.Exception, "Error Handler");
            }
            catch (Exception) { }

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}