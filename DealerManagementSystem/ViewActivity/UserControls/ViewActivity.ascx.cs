using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewActivity.UserControls
{
    public partial class ViewActivity : System.Web.UI.UserControl
    {
        public PLead Lead
        {
            get
            {
                if (Session["LeadView"] == null)
                {
                    Session["LeadView"] = new PLead();
                }
                return (PLead)Session["LeadView"];
            }
            set
            {
                Session["LeadView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 

        }
        public void fillViewLead(long LeadID)
        {
            Lead = new BLead().GetLeadByID(LeadID);
            lblLeadNumber.Text = Lead.LeadNumber;
            lblLeadDate.Text = Lead.LeadDate.ToLongDateString();
            lblCategory.Text = Lead.Category == null ? "" : Lead.Category.Category;
            lblProgressStatus.Text = Lead.ProgressStatus.ProgressStatus;
            lblQualification.Text = Lead.Qualification.Qualification;
            lblSource.Text = Lead.Source.Source;
            lblStatus.Text = Lead.Status.Status;
            lblType.Text = Lead.Type.Type;
            lblDealer.Text = Lead.Dealer.DealerCode;
            lblRemarks.Text = Lead.Remarks;
            lblCustomer.Text = Lead.Customer.CustomerFullName; 
           
            string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            
            fillAssignSalesEngineer();
      
            ActionControlMange();
       
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Lead")
            {
              //  MPE_Lead.Show();
                
            }
            else if (lbActions.Text == "Convert to Prospect")
            {
                string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=3" + "&UserID=" + PSession.User.UserID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                ShowMessage(Results);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                fillViewLead(Lead.LeadID);
            }
              
        }
        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {

            //MPE_Effort.Show();
            //string Message = UC_Effort.ValidationEffort();
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Visible = true;
            //if (!string.IsNullOrEmpty(Message))
            //{
            //    lblMessageEffort.Text = Message;
            //    return;
            //}
            //PLeadEffort Effort = new PLeadEffort();
            //Effort = UC_Effort.ReadEffort();
            //Effort.LeadEffortID = 0;
            //Effort.LeadID = Lead.LeadID;
            //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Effort", Effort));

            //if (Results.Status == PApplication.Failure)
            //{
            //    lblMessageEffort.Text = Results.Message;
            //    return;
            //}
            //ShowMessage(Results);

            //MPE_Effort.Hide();
            //fillViewLead(Lead.LeadID);
        }
        
        void fillAssignSalesEngineer()
        {
            //gvSalesEngineer.DataSource = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, null);
            //gvSalesEngineer.DataBind();

        }
         
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            //lblMessage.Visible = true;
            //if (fileUpload.PostedFile.FileName.Length == 0)
            //{
            //    lblMessage.Text = "Please select the file";
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            //byte[] buffer = new byte[100];
            //Stream stream = new MemoryStream(buffer);
            //HttpPostedFile file = fileUpload.PostedFile;
            //PAttachedFile F = new PAttachedFile();
            //int size = file.ContentLength;
            //string name = file.FileName;
            //int position = name.LastIndexOf("\\");
            //name = name.Substring(position + 1);

            //byte[] fileData = new byte[size];
            //file.InputStream.Read(fileData, 0, size);

            //F.FileName = name;
            //F.AttachedFile = fileData;
            //F.FileType = file.ContentType;
            //F.FileSize = size;
            //F.AttachedFileID = 0;
            //F.ReferenceID = Lead.LeadID;
            //F.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/AttachedFile", F));
            //ShowMessage(Results);
            //if (Results.Status == PApplication.Failure)
            //{
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            //fillSupportDocument();
        }
        
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

 
        public string ValidationColdVisit()
        {
            string Message = "";
            //txtColdVisitDate.BorderColor = Color.Silver;
            //txtVisitRemark.BorderColor = Color.Silver;
            //ddlActionType.BorderColor = Color.Silver;
            //if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
            //{
            //    Message = "Please enter the Cold Visit Date";
            //    txtColdVisitDate.BorderColor = Color.Red;
            //}
            //else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            //{
            //    Message = Message + "Please enter the Location";
            //    txtLocation.BorderColor = Color.Red;
            //}
            //else if (string.IsNullOrEmpty(txtVisitRemark.Text.Trim()))
            //{
            //    Message = Message + "Please enter the Remark";
            //    txtVisitRemark.BorderColor = Color.Red;
            //} 
            //else if (ddlActionType.SelectedValue == "0")
            //{
            //    Message = Message + "Please select the Action Type";
            //    ddlActionType.BorderColor = Color.Red;
            //}
            return Message;
        }

        void ActionControlMange()
        {
            
        }
    }
}