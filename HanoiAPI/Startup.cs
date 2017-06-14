using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(HanoiAPI.Startup))]

namespace HanoiAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}