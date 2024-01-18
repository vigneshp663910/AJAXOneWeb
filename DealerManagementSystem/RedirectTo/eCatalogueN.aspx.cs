

using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.RedirectTo
{
    public partial class eCatalogueN : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('e-Catalogue » eSpare Parts Catalogue');</script>");
                
            }

            //Response.Redirect("https://ajaxapps.ajax-engg.com:8095/eCatalogue/Home");
        }
    }
}