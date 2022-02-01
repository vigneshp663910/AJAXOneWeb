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
    public partial class LeadView : System.Web.UI.UserControl
    {
        public long LeadID
        {
            get
            {
                if (Session["LeadID"] == null)
                {
                    Session["LeadID"] = 0;
                }
                return Convert.ToInt64(Session["LeadID"]);
            }
            set
            {
                Session["LeadID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       public void fillViewLead(long LeadID)
        {
            this.LeadID =LeadID;
            PLeadSearch S = new PLeadSearch();
            S.LeadID = LeadID;
            PLead Lead = new BLead().GetLead(S)[0];
            lblLeadNumber.Text = Lead.LeadNumber;
            lblLeadDate.Text = Convert.ToString(Lead.LeadDate);
            lblCategory.Text = Lead.Category.Category;
            lblProgressStatus.Text = Lead.ProgressStatus.ProgressStatus;
            lblQualification.Text = Lead.Qualification.Qualification;
            lblSource.Text = Lead.Source.Source;
            lblStatus.Text = Lead.Status.Status;
            lblType.Text = Lead.Type.Type;
            lblDealer.Text = Lead.Dealer.DealerCode;
            lblRemarks.Text = Lead.Remarks;
            string Customer = Lead.Customer.CustomerCode + " " + Lead.Customer.CustomerName;
            lblCustomer.Text = Customer;
            lblContactPerson.Text = Lead.Customer.ContactPerson;
            lblMobile.Text = Lead.Customer.Mobile;
            lblEmail.Text = Lead.Customer.Email;

            string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            lblLocation.Text = Location;

            fillAssignSalesEngineer(LeadID);
            fillFollowUp(LeadID);
            fillConvocation(LeadID);
            fillFinancial(LeadID);
            fillEffort(LeadID);
            fillExpense(LeadID);
            fillProduct(LeadID);
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Assign")
            {
                MP_AssignSE.Show();
                fillAssignSalesEngineer(LeadID);
            }
            else if (lbActions.Text == "Add Follow-up")
            {
                MPE_FollowUp.Show();
                fillFollowUp(LeadID);
            }
            else if (lbActions.Text == "Customer Convocation")
            {
                MPE_Convocation.Show();
                fillConvocation(LeadID);
            }
            else if (lbActions.Text == "Financial Info")
            {
                MPE_Financial.Show();
                fillFinancial(LeadID);
            }
            else if (lbActions.Text == "Add Effort")
            {
                MPE_Effort.Show();
                fillEffort(LeadID);
            }
            else if (lbActions.Text == "Add Expense")
            {
                MPE_Expense.Show();
                fillExpense(LeadID);
            }
            else if (lbActions.Text == "Add Product")
            {
                MPE_Product.Show();
                fillProduct(LeadID);
            }
            
        }
  
        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {
            MPE_Effort.Show();

            string Message = UC_Effort.ValidationEffort();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadEffort Lead = new PLeadEffort();
            Lead = UC_Effort.ReadEffort();
            Lead.LeadEffortID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Effort", Lead)).Data);
            fillEffort(Lead.LeadID);
        }
        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            MPE_Expense.Show();
            string Message = UC_Expense.ValidationExpense();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            MPE_Expense.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadExpense Lead = new PLeadExpense();
            Lead = UC_Expense.ReadExpense();
            Lead.LeadExpenseID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Expense", Lead)).Data);
            fillExpense(Lead.LeadID);
        }
        protected void lblFinancialAdd_Click(object sender, EventArgs e)
        {

            DropDownList ddlBankNameF = (DropDownList)gvFinancial.FooterRow.FindControl("ddlBankNameF");
            TextBox txtFinancePercentageF = (TextBox)gvFinancial.FooterRow.FindControl("txtFinancePercentageF");
            TextBox txtRemarkF = (TextBox)gvFinancial.FooterRow.FindControl("txtRemarkF");
            PLeadFinancial Lead = new PLeadFinancial();
            Lead.LeadFinancialID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            Lead.BankName = new PBankName { BankNameID = Convert.ToInt32(ddlBankNameF.SelectedValue) };
            Lead.FinancePercentage = Convert.ToDecimal(txtFinancePercentageF.Text.Trim());
            Lead.Remark = txtRemarkF.Text;
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

           
           
            fillFinancial(Lead.LeadID);
        }

        void fillAssignSalesEngineer(long LeadID)
        { 
            gvSalesEngineer.DataSource = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            gvSalesEngineer.DataBind();
            UC_AssignSE.FillMaster();
        }
        void fillFollowUp(long LeadID)
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUpByID(LeadID, null); 
            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();

            //DropDownList ddlSalesEngineer = (DropDownList)gvFollowUp.FooterRow.FindControl("ddlSalesEngineer");

            //List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            //List<PUser> U = new List<PUser>();
            //foreach (PLeadSalesEngineer SE in SalesEngineer)
            //{
            //    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            //}
            UC_FollowUp.FillMaster(LeadID); 
        }
        void fillConvocation(long LeadID)
        { 
            gvConvocation.DataSource = new BLead().GetLeadConvocation(LeadID, PSession.User.UserID); ;
            gvConvocation.DataBind();
            UC_CustomerConvocation.FillMaster(LeadID);
        }
        void fillFinancial(long LeadID)
        { 
            gvFinancial.DataSource = new BLead().GetLeadFinancial(LeadID, PSession.User.UserID);
            gvFinancial.DataBind();
            UC_Financial.FillMaster();
        }
        void fillEffort(long LeadID)
        {
            gvEffort.DataSource = new BLead().GetLeadEffort(LeadID, PSession.User.UserID);
            gvEffort.DataBind();


            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind((DropDownList)UC_Effort.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID");
        }
        void fillExpense(long LeadID)
        {
            gvExpense.DataSource = new BLead().GetLeadExpense(LeadID, PSession.User.UserID);
            gvExpense.DataBind();

            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind((DropDownList)UC_Expense.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID");
        }
        void fillProduct(long LeadID)
        {
            gvProduct.DataSource = new BLead().GetLeadProduct(LeadID, PSession.User.UserID);
            gvProduct.DataBind();
            UC_Product.FillMaster();
           

        }
        protected void btnSaveFollowUp_Click(object sender, EventArgs e)
        { 
            MPE_FollowUp.Show(); 
            string Message = UC_FollowUp.ValidationFollowUp();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadFollowUp Lead = new PLeadFollowUp();
            Lead = UC_FollowUp.ReadFollowUp();
            Lead.LeadFollowUpID = 0;
            Lead.LeadID = LeadID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/FollowUp", Lead)).Data); 
            fillFollowUp(Lead.LeadID); 
        }

        protected void btnSaveustomerConvocation_Click(object sender, EventArgs e)
        {
            MPE_Convocation.Show();
            string Message = UC_CustomerConvocation.ValidationConvocation();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadConvocation Lead = new PLeadConvocation();
            Lead = UC_CustomerConvocation.ReadConvocation();
            Lead.LeadID = LeadID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Convocation", Lead)).Data);
            fillConvocation(Lead.LeadID);
        }

        protected void btnSaveAssignSE_Click(object sender, EventArgs e)
        {

            MP_AssignSE.Show();
            string Message = UC_AssignSE.ValidationAssignSE();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadSalesEngineer Lead = new PLeadSalesEngineer();
            Lead = UC_AssignSE.ReadAssignSE();
            Lead.LeadID = LeadID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/SalesEngineer", Lead)).Data);
            fillAssignSalesEngineer(Lead.LeadID);
        }

        protected void btnSaveFinancial_Click(object sender, EventArgs e)
        { 
            MPE_Financial.Show();
            string Message = UC_Financial.ValidationFinancial();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadFinancial Lead = new PLeadFinancial();
            Lead = UC_Financial.ReadFinancial();
            Lead.LeadID = LeadID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Financial", Lead)).Data);
            fillFinancial(LeadID);
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            MPE_Product.Show();
            string Message = UC_Product.ValidationProduct();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PLeadProduct Lead = new PLeadProduct();
            Lead = UC_Product.ReadProduct();
            Lead.LeadID = LeadID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Product", Lead)).Data);
            fillProduct(LeadID);
        }

        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BLead().GetAttachedFileLead(LeadID);
            gvSupportDocument.DataBind();
        }
        protected void btnAddFile_Click(object sender, EventArgs e)
        {

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
            F.ReferenceID = LeadID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/AttachedFile", F)).Data);
            if (Convert.ToBoolean(s) == true)
            {
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Green;
                fillSupportDocument();
            }
            else
            {
                lblMessage.Text = "Something went wrong try again.";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
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

                PAttachedFile UploadedFile = new BLead().GetAttachedFileLeadForDownload(AttachedFileID + Path.GetExtension(lblFileName.Text));

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
            F.ReferenceID = LeadID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/AttachedFile", F)).Data);
            if (Convert.ToBoolean(s) == true)
            {
                lblMessage.Text = "Removed successfully";
                lblMessage.ForeColor = Color.Green;
                fillSupportDocument();
            }
            else
            {
                lblMessage.Text = "Something went wrong try again.";
                lblMessage.ForeColor = Color.Red;
            }
        }


    }
}