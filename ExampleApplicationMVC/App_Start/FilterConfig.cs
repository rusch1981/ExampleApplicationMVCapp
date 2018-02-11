using System.Web;
using System.Web.Mvc;
using ExampleApplicationMVC.Filters;

namespace ExampleApplicationMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler());
        }
    }
}
