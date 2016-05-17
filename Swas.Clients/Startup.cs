using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Swas.Clients.Startup))]
namespace Swas.Clients
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
