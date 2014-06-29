using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HAKCMS.Web.Startup))]
namespace HAKCMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
