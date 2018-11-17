using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CareMeBotWeb.Startup))]
namespace CareMeBotWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
