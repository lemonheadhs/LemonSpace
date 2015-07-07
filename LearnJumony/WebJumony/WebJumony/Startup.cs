using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebJumony.Startup))]
namespace WebJumony
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
