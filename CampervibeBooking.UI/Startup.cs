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
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = "f9c75c65-f4cf-4bff-9c3c-630c624ae933",
                    Authority = "https://login.windows.net/paultdhotmail.onmicrosoft.com"
                });
        }
    }
}
