using Campervibe.Internal.UI.ActionFilters;
using System.Web;
using System.Web.Mvc;

namespace Campervibe.Internal.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}