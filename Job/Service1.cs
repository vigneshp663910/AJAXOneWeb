using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Job
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        static void Main(string[] args)
        {
            try
            {
                //  new FileLogger().LogMessage("Start", "Main", null);
                CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["AppCulture"]);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                new BIntegration().Start();
                //   new FileLogger().LogMessage("End", "Main", null);
            }
            catch (Exception ex)
            {
                // new FileLogger().LogMessage("EmpService", "Main", ex);
            }
        }
        protected override void OnStart(string[] args)
        {

        }
        protected override void OnStop()
        {
        }
    }
}
