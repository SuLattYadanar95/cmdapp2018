using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CareMeClient.Startup))]
namespace CareMeClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
