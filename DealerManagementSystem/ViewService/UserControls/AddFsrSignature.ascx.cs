using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class AddFsrSignature : System.Web.UI.UserControl
    {
        //public int Signature
        //{
        //    get
        //    {
        //        if (ViewState["SDMS_Signature"] == null)
        //        {
        //            ViewState["SDMS_Signature"] = 0;
        //        }
        //        return (int)ViewState["SDMS_Signature"];
        //    }
        //    set
        //    {
        //        ViewState["SDMS_Signature"] = value;
        //    }
        //} 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
        }

        public void FillMaster()
        {
            //Signature = TechSignature;
            lblProcessName.Text = "Engineer Photo";
            lblProcessID.Text = "1";
            //var x = Signature;
            //string dd = "<script type='text/javascript'>";
            //if ((x == 1) || (x == 3))
            //{
            //    dd = dd + "document.getElementById('divWeb').style.display = 'block';";
            //    dd = dd + "document.getElementById('divCapture').style.display = 'None';";
            //    dd = dd + "document.getElementById('divSign').style.display = 'None'";

            //}
            //else if ((x == 2) || (x == 4))
            //{
            //    dd = dd + "document.getElementById('divWeb').style.display = 'None';";
            //    dd = dd + "document.getElementById('divCapture').style.display = 'None';";
            //    dd = dd + "document.getElementById('divSign').style.display = 'block';"; 
            //}
            //dd = dd + "</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", dd);

            string dd = "<script type='text/javascript'>";
            dd = dd + "document.getElementById('divWeb').style.display = 'block';"; 
            dd = dd + "</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", dd);


            string WebCam = "<script type='text/javascript'>";
            WebCam = WebCam + "Webcam.set({width: 320,height: 240,image_format: 'jpeg',jpeg_quality: 90}); ";
            WebCam = WebCam + "Webcam.attach('#webcam');";
            WebCam = WebCam + "</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script2", WebCam);
        }

        protected void Save(object sender, EventArgs e)
        {
            //lblMessage.Visible = true;

            //string Image = "";
            //if ((Signature == 1) || (Signature == 3))
            //{
            //    if (string.IsNullOrEmpty(hfCapture.Value))
            //    {
            //        lblMessage.Text = "Please check the image ";
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    Image = hfCapture.Value;
            //}
            //else if ((Signature == 2) || (Signature == 4))
            //{
            //    if (string.IsNullOrEmpty(hfSign.Value))
            //    {
            //        lblMessage.Text = "Please check the image ";
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(txtNameOfSignature.Text.Trim()))
            //    {
            //        lblMessage.Text = "Please Enter the Name Of Signature";
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    Image = hfSign.Value;
            //}
            //if (new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSRSignature(SDMS_ICTicketFSR.FsrID, Image, PSession.User.UserID, Signature, false, txtNameOfSignature.Text.Trim()) != 0)
            //{
            //    lblMessage.Text = "Image successfully saved";
            //    lblMessage.ForeColor = Color.Green;
            //    txtNameOfSignature.Text = "";
            //}
            //else
            //{
            //    lblMessage.Text = "Image is not successfully saved";
            //    lblMessage.ForeColor = Color.Red;
            //}
        }
        public PApiResult SaveSign(long FsrID)
        {
            PApiResult Results = new PApiResult();
            Results.Status = PApplication.Failure;
            //string Image = "";
            //if ((Signature == 1) || (Signature == 3))
            //{
            //    if (string.IsNullOrEmpty(hfCapture.Value))
            //    {
            //        Results.Message = "Please check the image";
            //        return Results;
            //    }
            //    Image = hfCapture.Value;
            //}
            //else if ((Signature == 2) || (Signature == 4))
            //{
            //    if (string.IsNullOrEmpty(hfSign.Value))
            //    {
            //        Results.Message = "Please check the image ";
            //        return Results;
            //    }
            //    if (string.IsNullOrEmpty(txtNameOfSignature.Text.Trim()))
            //    {
            //        Results.Message = "Please Enter the Name Of Signature";
            //        return Results;
            //    }
            //    Image = hfSign.Value;
            //}
            //if (new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSRSignature(FsrID, Image, PSession.User.UserID, Signature, false, txtNameOfSignature.Text.Trim()) != 0)
            //{
            //    Results.Message = "Image successfully saved";
            //    Results.Status = PApplication.Success;
            //    txtNameOfSignature.Text = "";
            //}
            //else
            //{
            //    Results.Message = "Image is not successfully saved";
            //}

            if (string.IsNullOrEmpty(hfTPhotoCapture.Value))
            {
                Results.Message = "Please check the Engineer Photo";
                return Results;
            }
            if (string.IsNullOrEmpty(hfTSign.Value))
            {
                Results.Message = "Please check the Engineer Sign";
                return Results;
            }
            if (string.IsNullOrEmpty(hfCPhotoCapture.Value))
            {
                Results.Message = "Please check the Customer Photo";
                return Results;
            }
            if (string.IsNullOrEmpty(hfCSign.Value))
            {
                Results.Message = "Please check the Customer Sign";
                return Results;
            }
            if (string.IsNullOrEmpty(txtNameOfSignatureC.Text.Trim()))
            {
                Results.Message = "Please Enter the Name Of Customer Signed";
                return Results;
            }
            if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
            {
                Results.Message = "Please Enable GeoLocation...!";
                return Results;
            }


            PICTicketFSRSignature_Insert FSr = new PICTicketFSRSignature_Insert();
            FSr.FsrID = FsrID;
            FSr.FileType = "png";
            FSr.TPhoto = Convert.FromBase64String(Regex.Match(hfTPhotoCapture.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value);
            FSr.TSignature = Convert.FromBase64String(Regex.Match(hfTSign.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value);
            FSr.CPhoto = Convert.FromBase64String(Regex.Match(hfCPhotoCapture.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value);
            FSr.CSignature = Convert.FromBase64String(Regex.Match(hfCSign.Value, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value);
            FSr.CName = txtNameOfSignatureC.Text;
            FSr.SignatureOn = DateTime.Now;
            FSr.Latitude = Convert.ToDecimal( hfLatitude.Value);
            FSr.Longitude = Convert.ToDecimal(hfLongitude.Value);
            string result = new BAPI().ApiPut("ICTicketFSR/UpdateTicketFSRSignature", FSr);
            Results = JsonConvert.DeserializeObject<PApiResult>(result);
            return Results;
        }
    }
}