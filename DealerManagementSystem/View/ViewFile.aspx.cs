using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.View
{
    public partial class ViewFile :  BasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["Title"] != null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('" + Request.QueryString["Title"].ToString() + "');</script>");
            }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FileName"] != null)
            {
                var tempfilename = Request.QueryString["FileName"].ToString();
                ifrm_dcbform.Attributes["src"] = tempfilename;
            }
        }
    }
}