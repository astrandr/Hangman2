using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HangmanWeb.Startup))]
namespace HangmanWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
