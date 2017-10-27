using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestArchitectureDemo.Startup))]
namespace TestArchitectureDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
