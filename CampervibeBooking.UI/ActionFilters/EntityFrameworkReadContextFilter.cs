using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampervibeBooking.Data.Common;

namespace CampervibeBooking.UI.ActionFilters
{
    public class EntityFrameworkReadContextFilter : IActionFilter
    {
        private IContextProvider _contextProvider { get; set; }

        public EntityFrameworkReadContextFilter(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _contextProvider.Dispose();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}