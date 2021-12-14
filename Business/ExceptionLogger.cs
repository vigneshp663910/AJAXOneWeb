using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public static class ExceptionLogger
    {
        public static void LogError(string message, Exception ex)
        {
            //ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ILog log = LogManager.GetLogger(typeof(ExceptionLogger));
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Logger.IsEnabledFor(Level.Error);
                log.Error(message, ex);
            }
            catch (Exception exp)
            {
                string s = exp.Message;
            }

        }
        public static void LogMessage(string message)
        {
            //ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            ILog log = LogManager.GetLogger(typeof(ExceptionLogger));
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                log.Logger.IsEnabledFor(Level.Alert);
                log.Info(message);
            }
            catch (Exception exp)
            {
                string s = exp.Message;
            }

        }
    }
}
