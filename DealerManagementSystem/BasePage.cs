using System;

namespace DealerManagementSystem
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {

            base.OnPreInit(e);
            if (Context.Session != null)
            {
                if (Session.IsNewSession)
                {
                    string cookieHeader = Request.Headers["Cookie"];
                    if (!String.IsNullOrEmpty(cookieHeader) && cookieHeader.IndexOf("ASP.NET_SessionId") >= 0)
                    {
                        Response.Redirect(UIHelper.SessionFailureRedirectionPage);
                    }

                }

            }

        }
    }

}