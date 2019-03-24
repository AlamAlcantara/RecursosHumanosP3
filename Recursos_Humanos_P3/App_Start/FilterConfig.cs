using System.Web;
using System.Web.Mvc;

namespace Recursos_Humanos_P3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
