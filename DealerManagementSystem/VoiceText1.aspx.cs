using Microsoft.AspNet.SignalR.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class VoiceText1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var config = new DefaultConfigurationManager();
            config.ConnectionTimeout = new TimeSpan(0, 2, 22);
        }
    }
}