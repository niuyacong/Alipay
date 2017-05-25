using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alipay.Startup))]
namespace Alipay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
