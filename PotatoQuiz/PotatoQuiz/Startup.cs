using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PotatoQuiz.Startup))]
namespace PotatoQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
