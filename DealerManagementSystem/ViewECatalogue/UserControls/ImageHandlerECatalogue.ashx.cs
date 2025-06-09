using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DealerManagementSystem.ViewECatalogue.UserControls
{
    /// <summary>
    /// Summary description for ImageHandlerECatalogue
    /// </summary>
    public class ImageHandlerECatalogue : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string filePath = context.Session["filePath"] as string;

            if (string.IsNullOrEmpty(filePath))
            {
                filePath = context.Server.MapPath("~/Ajax/Images/AJAXtLogo.png");
            }
            else
            {
                context.Session["filePath"] = "";
                filePath = context.Server.MapPath("~/ECat/Files/" + filePath);
            }
            context.Response.ContentType = "image/png";
            context.Response.WriteFile(filePath);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}