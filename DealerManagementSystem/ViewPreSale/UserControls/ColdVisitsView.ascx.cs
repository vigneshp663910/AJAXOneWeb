using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class ColdVisitsView : System.Web.UI.UserControl
    {
        public long ColdVisitID
        {
            get
            {
                if (Session["ColdVisitID"] == null)
                {
                    Session["ColdVisitID"] = 0;
                }
                return Convert.ToInt64(Session["ColdVisitID"]);
            }
            set
            {
                Session["ColdVisitID"] = value;
            }
        }
        public PColdVisit ColdVisit
        {
            get
            {
                if (Session["ColdVisitsView"] == null)
                {
                    Session["ColdVisitsView"] = new PColdVisit();
                }
                return (PColdVisit)Session["ColdVisitsView"];
            }
            set
            {
                Session["ColdVisitsView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageEffort.Text = "";
            lblMessageExpense.Text = "";
            lblMessage.Text = "";
        }
        public void fillViewColdVisit(long ColdVisitID)
        {
            this.ColdVisitID = ColdVisitID;

            ColdVisit = new BColdVisit().GetColdVisit(ColdVisitID,null, null, null, null, null, null, null, null, null, null, null, null, null)[0];
            lblLeadNumber.Text = ColdVisit.ColdVisitNumber;
            lblLeadDate.Text = ColdVisit.ColdVisitDate.ToShortDateString();// Convert.ToString(Lead.ColdVisitDate); 
                                                                           //  lblRemarks.Text = ColdVisit.Remarks; 

            lblLocation.Text = ColdVisit.Location;
            lblCustomer.Text = ColdVisit.Customer.CustomerFullName;
            lblContactPerson.Text = ColdVisit.Customer.ContactPerson;
            lblMobile.Text = "<a href='tel:" + ColdVisit.Customer.Mobile + "'>" + ColdVisit.Customer.Mobile + "</a>";
            lblEmail.Text = "<a href='mailto:" + ColdVisit.Customer.Email + "'>" + ColdVisit.Customer.Email + "</a>";

            lblStatus.Text = ColdVisit.Status.Status;
            lblImportance.Text = ColdVisit.Importance.Importance;

            string Location = ColdVisit.Customer.Address1 + ", " + ColdVisit.Customer.Address2 + ", " + ColdVisit.Customer.District.District + ", " + ColdVisit.Customer.State.State;
            lblAddress.Text = Location;
            fillEffort();
            fillExpense();

          
            ActionControlMange();

            
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);

            if (lbActions.Text == "Add Effort")
            {
                List<PUser> User = new List<PUser>();
                User.Add(ColdVisit.CreatedBy);
                DropDownList ddlSalesEngineer = (DropDownList)UC_Effort.FindControl("ddlSalesEngineer");
                DropDownList ddlEffortType = (DropDownList)UC_Effort.FindControl("ddlEffortType");

                new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                new DDLBind(ddlSalesEngineer, User, "ContactName", "UserID", false); 
               
                ddlSalesEngineer.Enabled = false;
                MPE_Effort.Show(); 
            }
            else if (lbActions.Text == "Add Expense")
            {
                List<PUser> User = new List<PUser>();
                User.Add(ColdVisit.CreatedBy);
                DropDownList ddlSalesEngineer = (DropDownList)UC_Expense.FindControl("ddlSalesEngineer");
                DropDownList ddlExpenseType = (DropDownList)UC_Expense.FindControl("ddlExpenseType");

                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
                new DDLBind(ddlSalesEngineer, User, "ContactName", "UserID", false);

                ddlSalesEngineer.Enabled = false;

                MPE_Expense.Show(); 
            }
            else if (lbActions.Text == "Status Change to Close")
            {
                string endPoint = "ColdVisit/UpdateColdVisitStatus?ColdVisitID=" + ColdVisitID + "&StatusID=2"  + "&UserID=" + PSession.User.UserID;
                string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                if (Convert.ToBoolean(s) == true)
                {
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillViewColdVisit(ColdVisitID);
                }
                else
                {
                    lblMessage.Text = "Something went wrong try again.";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
            else if (lbActions.Text == "Status Change to Cancel")
            {
                string endPoint = "ColdVisit/UpdateColdVisitStatus?ColdVisitID=" + ColdVisitID + "&StatusID=3" + "&UserID=" + PSession.User.UserID;
                string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                if (Convert.ToBoolean(s) == true)
                {
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillViewColdVisit(ColdVisitID);
                }
                else
                {
                    lblMessage.Text = "Something went wrong try again.";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
        }

        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {
            MPE_Effort.Show();
            string Message = UC_Effort.ValidationEffort();
            lblMessageEffort.ForeColor = Color.Red;
            lblMessageEffort.Visible = true; 
            if (!string.IsNullOrEmpty(Message))
            { 
                lblMessageEffort.Text = Message;
                return;
            }
            PLeadEffort Effort = new PLeadEffort();
            Effort = UC_Effort.ReadEffort();
            Effort.LeadID = ColdVisitID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Effort", Effort));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            MPE_Effort.Hide();
            tbpCust.ActiveTabIndex = 0;
            fillEffort(); 
        }

        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            MPE_Expense.Show();
            string Message = UC_Expense.ValidationExpense();
            lblMessageExpense.ForeColor = Color.Red;
            lblMessageExpense.Visible = true;
            
            if (!string.IsNullOrEmpty(Message))
            {
              
                lblMessageExpense.Text = Message;
                return;
            }
            PLeadExpense Expense = new PLeadExpense();
            Expense = UC_Expense.ReadExpense();
            Expense.LeadID = ColdVisitID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Expense", Expense));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            MPE_Expense.Hide();
            tbpCust.ActiveTabIndex =1;
            fillExpense();
        }
        void fillEffort()
        {
            gvEffort.DataSource = new BColdVisit().GetColdVisitEffort(ColdVisitID, PSession.User.UserID);
            gvEffort.DataBind();

        }
        void fillExpense()
        {

            gvExpense.DataSource = new BColdVisit().GetColdVisitExpense(ColdVisitID, PSession.User.UserID);
            gvExpense.DataBind();

        }
        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BColdVisit().GetAttachedFileColdVisit(ColdVisitID);
            gvSupportDocument.DataBind();
        }
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            if (fileUpload.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            byte[] buffer = new byte[100];
            Stream stream = new MemoryStream(buffer);
            HttpPostedFile file = fileUpload.PostedFile;
            PAttachedFile F = new PAttachedFile();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);

            F.FileName = name;
            F.AttachedFile = fileData;
            F.FileType = file.ContentType;
            F.FileSize = size;
            F.AttachedFileID = 0;
            F.ReferenceID = ColdVisitID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/AttachedFile", F));
            lblMessage.Visible = true;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Results.Message; 
            lblMessage.ForeColor = Color.Green; 
            fillSupportDocument(); 
           
        }

        protected void lbSupportDocumentDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                Label lblFileName = (Label)gvRow.FindControl("lblFileName");
                Label lblFileType = (Label)gvRow.FindControl("lblFileType");

                PAttachedFile UploadedFile = new BColdVisit().GetAttachedFileColdVisitForDownload(AttachedFileID + Path.GetExtension(lblFileName.Text));

                Response.AddHeader("Content-type", lblFileType.Text);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblFileName.Text);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }
        }

        protected void lbSupportDocumentDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
            PAttachedFile F = new PAttachedFile();
            F.AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
            F.ReferenceID = ColdVisitID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/AttachedFile", F));

            lblMessage.Visible = true;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.ForeColor = Color.Green;
            fillSupportDocument();
        }
        void ActionControlMange()
        { 
            lbtnStatusChangeToClose.Visible = true;
            lbtnStatusChangeToCancel.Visible = true;
            if ((ColdVisit.Status.StatusID == 2) || (ColdVisit.Status.StatusID == 3))
            {
                lbtnStatusChangeToClose.Visible = false;
                lbtnStatusChangeToCancel.Visible = false;
            }

            if (ColdVisit.ReferenceID != null)
            {
                lbtnAddEffort.Visible = false;
                lbtnAddExpense.Visible = false;
                tbpCust.Visible = false;
            }
        }
    }
}