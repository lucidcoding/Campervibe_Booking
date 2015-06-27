using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

//[assembly: OwinStartupAttribute(typeof(CampervibeBooking.UI.Startup))]
namespace CampervibeBooking.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {

            //app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseOpenIdConnectAuthentication(
            //    new OpenIdConnectAuthenticationOptions
            //    {
            //        ClientId = "a54372b5-7c28-47b3-ae9f-9972ba3b42f5",
            //        Authority = "https://login.windows.net/paultdhotmail.onmicrosoft.com"
            //    });
        }
    }
}
