using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Swas.Client.Startup))]
namespace Swas.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
