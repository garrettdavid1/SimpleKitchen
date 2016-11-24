using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleKitchen.Startup))]
namespace SimpleKitchen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
