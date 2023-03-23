using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MultiShop.Startup))]
namespace MultiShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
