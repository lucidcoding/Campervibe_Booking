using System.Web;
using System.Web.Mvc;
using CampervibeBooking.Data.Common;
using CampervibeBooking.Data.Core;
using Ninject;

namespace CampervibeBooking.UI.ActionFilters
{
    public class EntityFrameworkWriteContextAttribute : ActionFilterAttribute
    {
    }
}