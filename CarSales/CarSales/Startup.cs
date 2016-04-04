using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarSales.Startup))]
namespace CarSales
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
