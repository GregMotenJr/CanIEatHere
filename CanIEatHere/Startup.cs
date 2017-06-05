using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CanIEatHere.Startup))]
namespace CanIEatHere
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
