using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMS.Dashboard.Startup))]
namespace CMS.Dashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
