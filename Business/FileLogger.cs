using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class FileLogger: System.Web.UI.Page
    {
        public void LogMessage(string ServiceName, string message, Exception e)
        {
            string LogFolder = Convert.ToString(ConfigurationManager.AppSettings["LogFolder"]);
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
            FileStream fs1;

            //   string LogFilePath = LogFolder + "/Logs" + Session["NewUser"] + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
            string LogFilePath = "";
            if (PSession.User.UserID == null)
            {
                LogFilePath = LogFolder + "/Logs" + DateTime.Now.ToShortDateString() + ".txt";
            }
            else
            {
                LogFilePath = LogFolder + "/Logs" + PSession.User.UserID + ".txt";
            }

            if (!File.Exists(LogFilePath))
                fs1 = new FileStream(LogFilePath, FileMode.Create, FileAccess.Write);
            else
                fs1 = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write);
            using (TextWriter tw = new StreamWriter(fs1))
            {
                tw.WriteLine(DateTime.Now.ToString() + "\t" + ServiceName + "\t" + message + "\t" + Convert.ToString(e));
                tw.Close();
            }
        }
        public void LogMessageService(string ServiceName, string message, Exception e)
        {
            string LogFolder = Convert.ToString(ConfigurationManager.AppSettings["LogFolder"]);
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
            FileStream fs1;

            //   string LogFilePath = LogFolder + "/Logs" + Session["NewUser"] + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
            string LogFilePath = "";
             
                LogFilePath = LogFolder + "/Logs" + DateTime.Now.ToShortDateString().Replace("/","") + ".txt";
           

            if (!File.Exists(LogFilePath))
                fs1 = new FileStream(LogFilePath, FileMode.Create, FileAccess.Write);
            else
                fs1 = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write);
            using (TextWriter tw = new StreamWriter(fs1))
            {
                tw.WriteLine(DateTime.Now.ToString() + "\t" + ServiceName + "\t" + message + "\t" + Convert.ToString(e));
                tw.Close();
            }
        }
        public void LogTrack(string FuntionName, string message)
        {
            if(Convert.ToString(ConfigurationManager.AppSettings["LogTrack"]) =="0")
            {
                return;
            }
            string LogFolder = Convert.ToString(ConfigurationManager.AppSettings["LogFolder"]);
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
            FileStream fs1;

            string LogFilePath = LogFolder + "/LogsLogTrack" + Session["NewUser"] + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";

            if (!File.Exists(LogFilePath))
                fs1 = new FileStream(LogFilePath, FileMode.Create, FileAccess.Write);
            else
                fs1 = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write);
            using (TextWriter tw = new StreamWriter(fs1))
            {
                tw.WriteLine(DateTime.Now.ToString() + "\t" + FuntionName + "\t" + message);
                tw.Close();
            }
        }
    }
}