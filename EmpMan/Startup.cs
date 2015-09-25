using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmpMan.Startup))]
namespace EmpMan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
