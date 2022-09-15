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
    public partial class AddFSRAttachments : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(PDMS_ICTicket SDMS_ICTicket)
        {
            ddlFSRAttachedName.DataTextField = "FSRAttachedName";
            ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
            ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, true, null);
            ddlFSRAttachedName.DataBind();
        }

        public PDMS_FSRAttachedFile_M Read()
        {
            PDMS_FSRAttachedFile_M AttachedFile = new PDMS_FSRAttachedFile_M(); 
            AttachedFile = CreateUploadedFileFSR(fu.PostedFile);
            AttachedFile.FSRAttachedName = new PDMS_FSRAttachedName() { FSRAttachedFileNameID = Convert.ToInt32(ddlFSRAttachedName.SelectedValue) };

            return AttachedFile;
        }

        public string Validation()
        {
            string Message = "";
             
            if (fu.PostedFile.FileName.Length == 0)
            {
                return "Please select the file";  
            }
            string ext = System.IO.Path.GetExtension(fu.PostedFile.FileName).ToLower(); 
            List<string> Extension = new List<string>();
            Extension.Add(".jpg");
            Extension.Add(".png");
            Extension.Add(".gif");
            Extension.Add(".jpeg");
            if (!Extension.Contains(ext))
            { 
                return "Please choose only .jpg, .png and .gif image types!"; 
            } 

            return Message;
        }
       
        private PDMS_FSRAttachedFile_M CreateUploadedFileFSR(HttpPostedFile file)
        {
            PDMS_FSRAttachedFile_M AttachedFile = new PDMS_FSRAttachedFile_M();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);
            AttachedFile.FileName = name;

            AttachedFile.FileType = file.ContentType;

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.FileSize = size;
            AttachedFile.AttachedFileID = 0;
            AttachedFile.IsDeleted = false;
           // AttachedFile.ICTicket = new PDMS_ICTicket() { ICTicketID = SDMS_ICTicket.ICTicketID }; 
            return AttachedFile;
        }
    }
}