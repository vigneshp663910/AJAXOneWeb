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

namespace DealerManagementSystem.ViewPreSale
{
    public partial class PDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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