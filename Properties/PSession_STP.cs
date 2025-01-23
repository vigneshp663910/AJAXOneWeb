using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Properties
{
    public static class PSession_STP
    {
        private static T GetFromSession<T>(string key)
        {
            object obj = HttpContext.Current.Session[key];
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }
       
        private static void SetInSession<T>(string key, T value)
        {
            if (value == null)
            {
                HttpContext.Current.Session.Remove(key);
            }
            else
            {
                HttpContext.Current.Session[key] = value;
            }
        }
      
        public static PSalesTouchPointUser SalesTouchPointUser
        {
            get { return GetFromSession<PSalesTouchPointUser>("SalesTouchPointUser"); }
            set { SetInSession<PSalesTouchPointUser>("SalesTouchPointUser", value); }
        }
        public static string AccessToken
        {
            get { return GetFromSession<string>("AccessToken"); }
            set { SetInSession<string>("AccessToken", value); }
        }
        public static string SessionId
        {
            get { return HttpContext.Current.Session.SessionID; }
        }
        public static string UserBrowser
        {
            get { return HttpContext.Current.Request.Browser.Browser; }
        }
        public static string UserIPAddress
        {
            get { return HttpContext.Current.Request.UserHostAddress; }
        }
       
    }
}
