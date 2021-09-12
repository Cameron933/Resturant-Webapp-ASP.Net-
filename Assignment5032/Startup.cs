using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Assignment5032.Startup))]
namespace Assignment5032
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
