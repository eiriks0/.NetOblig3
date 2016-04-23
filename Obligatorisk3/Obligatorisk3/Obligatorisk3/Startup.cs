using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Obligatorisk3.Startup))]
namespace Obligatorisk3
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
