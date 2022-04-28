using Microsoft.AspNet.SignalR.Configuration;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(DealerManagementSystem.Startup))]
//[assembly: OwinStartup("ProductionConfiguration", typeof(DealerManagementSystem.ProductionStartup))]
namespace DealerManagementSystem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888


            var config = new DefaultConfigurationManager();
            config.ConnectionTimeout = new TimeSpan(0, 2, 22);
            app.MapSignalR();

        }
       
    } 
}
