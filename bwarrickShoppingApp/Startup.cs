using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bwarrickShoppingApp.Startup))]
namespace bwarrickShoppingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
