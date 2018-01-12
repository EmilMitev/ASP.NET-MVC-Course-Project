using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackFaceSystem.Web.Startup))]
namespace StackFaceSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
