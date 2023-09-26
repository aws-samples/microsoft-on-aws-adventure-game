using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(anyo_platform.Startup))]
namespace anyo_platform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
