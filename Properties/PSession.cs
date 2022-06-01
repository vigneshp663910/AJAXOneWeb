using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Properties
{
    public static class PSession
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
        private static List<T> GetListFromSession<T>(string key)
        {
            object obj = HttpContext.Current.Session[key];
            if (obj == null)
            {
                return default(List<T>);
            }
            return (List<T>)obj;
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
        private static void SetListInSession<T>(string key, List<T> value)
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
        private static T GetFromApplication<T>(string key)
        {
            return (T)HttpContext.Current.Application[key];
        }
        private static void SetInApplication<T>(string key, T value)
        {
            if (value == null)
            {
                HttpContext.Current.Application.Remove(key);
            }
            else
            {
                HttpContext.Current.Application[key] = value;
            }
        }
        //public static long UserId
        //{
        //    get { return GetFromSession<long>("UserId"); }
        //    set { SetInSession<long>("UserId", value); }
        //}
        public static PUser User
        {
            get { return GetFromSession<PUser>("User"); }
            set { SetInSession<PUser>("User", value); }
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
        public static long CustomerServiceAdminUserId
        {
            get { return GetFromSession<long>("CustomerServiceAdminUserId"); }
            set { SetInSession<long>("CustomerServiceAdminUserId", value); }
        }
        public static PEmployee Emp
        {
            get { return GetFromSession<PEmployee>("Emp"); }
            set { SetInSession<PEmployee>("Emp", value); }
        }
    }
}
