using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMS.Dashboard._Startup))]
namespace CMS.Dashboard
{
    public partial class _Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
