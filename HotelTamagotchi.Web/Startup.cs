using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelTamagotchi.Web.Startup))]
namespace HotelTamagotchi.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
