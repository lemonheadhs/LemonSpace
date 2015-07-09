using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TinyLibrary.WebApp2.Startup))]
namespace TinyLibrary.WebApp2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
