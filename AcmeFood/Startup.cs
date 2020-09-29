using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcmeFood.Startup))]
namespace AcmeFood
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
