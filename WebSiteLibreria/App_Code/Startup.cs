using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteLibreria.Startup))]
namespace WebSiteLibreria
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
