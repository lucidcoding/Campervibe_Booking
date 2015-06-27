using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Campervibe.Internal.UI.Startup))]
namespace Campervibe.Internal.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
