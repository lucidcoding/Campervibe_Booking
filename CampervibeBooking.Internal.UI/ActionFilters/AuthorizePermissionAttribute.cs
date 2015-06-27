using Campervibe.Domain.RepositoryContracts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Campervibe.Internal.UI.ActionFilters
{
    public class AuthorizePermissionAttribute : AuthorizeAttribute
    {
        [Inject]
        public IUserRepository UserRepository { get; set; }

        private string _permission;

        public AuthorizePermissionAttribute(string permission)
        {
            Order = 1100;
            _permission = permission;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string userName = null;

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                userName = HttpContext.Current.User.Identity.Name;
            }

            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            var user = UserRepository.GetByUsername(userName);

            if (user == null)
            {
                return false;
            }

            if (user.Role.PermissionRoles.Any(x => x.Permission.Description.Equals(_permission)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //filterContext.Result = new HttpUnauthorizedResult(); //Can't use because MVC uses it internally.
            //filterContext.Result = new HttpStatusCodeResult(403); //Can't get to work for some reason.
            filterContext.Result = new RedirectToRouteResult(new
                       RouteValueDictionary (new { controller = "Account", action = "Unauthorized" }));

        }
    }
}