using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace CampervibeBooking.UI.Security
{
    public class UserProvider : IUserProvider
    {
        public string GetUsername()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public Guid GetId()
        {
            var claim = (HttpContext.Current.User.Identity as ClaimsIdentity)
                .Claims
                .SingleOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier");

            return new Guid(claim.Value);
        }
    }
}