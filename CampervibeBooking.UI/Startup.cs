using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CampervibeBooking.UI.Startup))]
namespace CampervibeBooking.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
