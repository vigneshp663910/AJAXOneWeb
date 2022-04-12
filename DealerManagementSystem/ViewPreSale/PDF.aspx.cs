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
            //if (!Directory.Exists(ConfigurationManager.AppSettings["BasePath"] + "/Quotation/"))
            //{
            //    Directory.CreateDirectory(ConfigurationManager.AppSettings["BasePath"] + "/Quotation/");
            //}
            //var uploadPath = ConfigurationManager.AppSettings["BasePath"] + "/Quotation/";
            //var tempfilename = "QT_Tax908551" + ".pdf";
            //string FileName = Path.Combine(uploadPath, Path.GetFileName(tempfilename));
            //if (Request.QueryString["FileName"] != null)
            //{
            //    FileName = Request.QueryString["FileName"].ToString();
            //}
            //PdfReader pdfReader = new PdfReader(FileName);
            //int numberOfPages = pdfReader.NumberOfPages;
            ////ifrm_dcbform.Attributes["height"] = numberOfPages * 1140 + "px";
            ////ifrm_dcbform.Attributes["src"] = File;
            //ifrm_dcbform.Attributes["height"] = numberOfPages * 1140 + "px";
            //ifrm_dcbform.Src = Path.Combine(ConfigurationManager.AppSettings["BasePath"] + "/Quotation/", Path.GetFileName(tempfilename)) + "?page=hsn#toolbar=0";





            var uploadPath = Server.MapPath("~/Backup");
            var tempfilename = "QT_Tax908551" + ".pdf";
            string FileName = Path.Combine(uploadPath, Path.GetFileName(tempfilename));
            PdfReader pdfReader = new PdfReader(FileName);
            if (Request.QueryString["FileName"] != null)
            {
                tempfilename = Request.QueryString["FileName"].ToString();
                FileName=Path.Combine(Server.MapPath("~/Backup"), Path.GetFileName(tempfilename));
                pdfReader = new PdfReader(FileName);
            }
            int numberOfPages = pdfReader.NumberOfPages;
            ifrm_dcbform.Attributes["height"] = numberOfPages * 1140 + "px";
            ifrm_dcbform.Src = Path.Combine("~/Backup", Path.GetFileName(tempfilename)) + "?page=hsn#toolbar=0";
        }
    }
}