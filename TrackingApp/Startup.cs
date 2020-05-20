using Microsoft.Owin;
using Owin;
using FluentScheduler;
using TrackingApp.Extension;


[assembly: OwinStartupAttribute(typeof(TrackingApp.Startup))]
namespace TrackingApp
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            JobManager.Initialize(new MyRegistry(20));
            ConfigureAuth(app);
        }
    }
}
