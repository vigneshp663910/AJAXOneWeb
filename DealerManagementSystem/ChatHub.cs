using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerManagementSystem
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            var config = new DefaultConfigurationManager();
            config.ConnectionTimeout = new TimeSpan(0, 2, 22);
            Clients.All.sendMessage(name, message);
        }
    }
}