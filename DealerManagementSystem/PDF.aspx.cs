using iTextSharp.text.pdf;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class PDF : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Title"] != null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('" + Request.QueryString["Title"].ToString() + "');</script>");
            }
            PdfReader pdfReader;
            if (Request.QueryString["FileName"] != null)
            {
                var tempfilename = Request.QueryString["FileName"].ToString();
                string FileName = Path.Combine(Server.MapPath("~/Backup"), Path.GetFileName(tempfilename));
                pdfReader = new PdfReader(FileName);
                int numberOfPages = pdfReader.NumberOfPages;
                ifrm_dcbform.Attributes["height"] = numberOfPages * 1140 + "px";
                ifrm_dcbform.Src = Path.Combine("~/Backup", Path.GetFileName(tempfilename));
            }
            
        }
    }
}