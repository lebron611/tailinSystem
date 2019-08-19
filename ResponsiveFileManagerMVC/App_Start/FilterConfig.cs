using System.Web;
using System.Web.Mvc;
using ResponsiveFileManagerMVC.Models;
namespace ResponsiveFileManagerMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserTraceLog());
        }
    }
}
