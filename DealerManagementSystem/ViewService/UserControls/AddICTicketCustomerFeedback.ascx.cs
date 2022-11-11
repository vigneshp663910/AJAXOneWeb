using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class AddICTicketCustomerFeedback : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(PICTicketCustomerFeedback Feedback)
        {
            FillCustomerSatisfactionLevel();

            if (Feedback.CustomerSatisfactionLevel != null)
                ddlCustomerSatisfactionLevel.SelectedValue = Feedback.CustomerSatisfactionLevel.CustomerSatisfactionLevelID.ToString();

        }

        void Clear()
        {


        }
        public PICTicketCustomerFeedback Read()
        {
            PICTicketCustomerFeedback Feed = new PICTicketCustomerFeedback();
            Feed.Remarks = txtRemarks.Text;
            Feed.Photo = CreateUploadedFile(fuPhoto.PostedFile);
            Feed.Signature = CreateUploadedFile(fuSignature.PostedFile);
            Feed.Latitude = 0;
            Feed.Longitude = 0;
            Feed.CustomerSatisfactionLevel = new PDMS_CustomerSatisfactionLevel() { CustomerSatisfactionLevelID = Convert.ToInt32(ddlCustomerSatisfactionLevel.SelectedValue) }; 
            return Feed;
        }
        public string Validation()
        {
            string Message = "";
            
            if (string.IsNullOrEmpty(txtRemarks.Text))
            {
                return "Please enter the Remarks";
            }
            return Message;
        }

        private PAttachedFileS CreateUploadedFile(HttpPostedFile file)
        {
            PAttachedFileS AttachedFile = new PAttachedFileS();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);
            AttachedFile.FileName = name;

            AttachedFile.FileType = file.ContentType;

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData; 
            AttachedFile.AttachedFileID = 0; 
            // AttachedFile.ICTicket = new PDMS_ICTicket() { ICTicketID = SDMS_ICTicket.ICTicketID }; 
            return AttachedFile;
        }
        private void FillCustomerSatisfactionLevel()
        {
            ddlCustomerSatisfactionLevel.DataTextField = "CustomerSatisfactionLevel";
            ddlCustomerSatisfactionLevel.DataValueField = "CustomerSatisfactionLevelID";
            ddlCustomerSatisfactionLevel.DataSource = new BDMS_Service().GetCustomerSatisfactionLevel(null, null);
            ddlCustomerSatisfactionLevel.DataBind();
            ddlCustomerSatisfactionLevel.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}