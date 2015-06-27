using CampervibeBooking.UI.ActionFilters;
using System.Web;
using System.Web.Mvc;

namespace CampervibeBooking.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}