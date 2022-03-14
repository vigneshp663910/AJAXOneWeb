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
            lblMessageEffort.Text = "";
            lblMessageExpense.Text = "";
            lblMessageFollowUp.Text = "";
            lblMessageConvocation.Text = "";
            lblMessageAssignEngineer.Text = "";
            lblMessageFinancial.Text = "";
            lblMessageProduct.Text = "";
            lblMessageQuotation.Text = ""; 
        }
        public void fillViewLead(long LeadID)
        {
            this.LeadID = LeadID;
            Lead = new BLead().GetLeadByID(LeadID);
            lblLeadNumber.Text = Lead.LeadNumber;
            lblLeadDate.Text = Lead.LeadDate.ToLongDateString();
            lblCategory.Text = Lead.Category.Category;
            lblProgressStatus.Text = Lead.ProgressStatus.ProgressStatus;
            lblQualification.Text = Lead.Qualification.Qualification;
            lblSource.Text = Lead.Source.Source;
            lblStatus.Text = Lead.Status.Status;
            lblType.Text = Lead.Type.Type;
            lblDealer.Text = Lead.Dealer.DealerCode;
            lblRemarks.Text = Lead.Remarks;
            lblCustomer.Text = Lead.Customer.CustomerFullName;
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

            ActionControlMange();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Lead")
            {
                MPE_Lead.Show();
                fillLeadEdit();
            }
            else if (lbActions.Text == "Convert to Prospect")
            {
                string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=4" + "&UserID=" + PSession.User.UserID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)); 
                ShowMessage(Results);
                if (Results.Status == PApplication.Failure)
                { 
                    lblMessage.ForeColor = Color.Red;
                    return;
                } 
                fillViewLead(Lead.LeadID);
            }
            else if (lbActions.Text == "Lost Lead")
            {
                MPE_LostReason.Show();
            }
            else if (lbActions.Text == "Cancel Lead")
            {
                MPE_RejectedBySales.Show();
            }
            else if(lbActions.Text == "Assign")
            {
                MPE_AssignSE.Show();
                fillAssignSalesEngineer(LeadID);
            }
            else if (lbActions.Text == "Add Follow-up")
            {
                MPE_FollowUp.Show(); 

                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID,true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind((DropDownList)UC_FollowUp.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID", false);
                UC_FollowUp.FillMaster();
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
                DropDownList ddlSalesEngineer = (DropDownList)UC_Effort.FindControl("ddlSalesEngineer");
                DropDownList ddlEffortType = (DropDownList)UC_Effort.FindControl("ddlEffortType");

                new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                ddlSalesEngineer.Enabled = false;

                MPE_Effort.Show();
                fillEffort(LeadID);
            }
            else if (lbActions.Text == "Add Expense")
            { 
                DropDownList ddlSalesEngineer = (DropDownList)UC_Expense.FindControl("ddlSalesEngineer");
                DropDownList ddlExpenseType = (DropDownList)UC_Expense.FindControl("ddlExpenseType");
                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
                ddlSalesEngineer.Enabled = false;
                MPE_Expense.Show();
                fillExpense(LeadID);
            }
            else if (lbActions.Text == "Add Product")
            {
                MPE_Product.Show();
                fillProduct(LeadID);
            }
            else if (lbActions.Text == "Add Quotation")
            {
                MPE_Quotation.Show();
                UC_Quotation.FillMaster();
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
                lblMessageEffort.Text = Message;
                return;
            }
            PLeadEffort Lead = new PLeadEffort();
            Lead = UC_Effort.ReadEffort();
            Lead.LeadEffortID = 0;
            Lead.LeadID = LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Effort", Lead));

            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_Effort.Hide();
            fillEffort(Lead.LeadID);
        }
        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            MPE_Expense.Show();
            string Message = UC_Expense.ValidationExpense();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true; 
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageExpense.Text = Message;
                return;
            }
            PLeadExpense Lead = new PLeadExpense();
            Lead = UC_Expense.ReadExpense();
            Lead.LeadExpenseID = 0;
            Lead.LeadID = Convert.ToInt64(ViewState["LeadID"]);
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Expense", Lead));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageExpense.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_Expense.Hide();
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
        protected void btnSaveFollowUp_Click(object sender, EventArgs e)
        {
            MPE_FollowUp.Show();
            string Message = UC_FollowUp.ValidationFollowUp();
            lblMessageFollowUp.ForeColor = Color.Red;
            lblMessageFollowUp.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageFollowUp.Text = Message;
                return;
            }
            PLeadFollowUp Lead = new PLeadFollowUp();
            Lead = UC_FollowUp.ReadFollowUp();
            Lead.LeadFollowUpID = 0;
            Lead.LeadID = LeadID;
            PApiResult Results =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/FollowUp", Lead));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFollowUp.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_FollowUp.Hide();
            fillFollowUp(Lead.LeadID);
        }

        protected void btnSaveustomerConvocation_Click(object sender, EventArgs e)
        {
            MPE_Convocation.Show();
            string Message = UC_CustomerConvocation.ValidationConvocation();
            lblMessageConvocation.ForeColor = Color.Red;
            lblMessageConvocation.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageConvocation.Text = Message;
                return;
            }
            PLeadConvocation Lead = new PLeadConvocation();
            Lead = UC_CustomerConvocation.ReadConvocation();
            Lead.LeadID = LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Convocation", Lead));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageConvocation.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_Convocation.Hide();
            fillConvocation(Lead.LeadID);
        }

        protected void btnSaveAssignSE_Click(object sender, EventArgs e)
        {
            MPE_AssignSE.Show();
            string Message = UC_AssignSE.ValidationAssignSE();
            lblMessageAssignEngineer.ForeColor = Color.Red;
            lblMessageAssignEngineer.Visible = true;

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageAssignEngineer.Text = Message;
                return;
            }
            PLeadSalesEngineer Lead = new PLeadSalesEngineer();
            Lead = UC_AssignSE.ReadAssignSE();
            Lead.LeadID = LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/SalesEngineer", Lead));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAssignEngineer.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_AssignSE.Hide();
            tbpCust.ActiveTabIndex = 0;

            fillAssignSalesEngineer(Lead.LeadID);
        }

        protected void btnSaveFinancial_Click(object sender, EventArgs e)
        {
            MPE_Financial.Show();
            string Message = UC_Financial.ValidationFinancial();
            lblMessageFinancial.ForeColor = Color.Red;
            lblMessageFinancial.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageFinancial.Text = Message;
                return;
            }
            PLeadFinancial Lead = new PLeadFinancial();
            Lead = UC_Financial.ReadFinancial();
            Lead.LeadID = LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Financial", Lead));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFinancial.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_Financial.Hide();
            fillFinancial(LeadID);
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            MPE_Product.Show();
            string Message = UC_Product.ValidationProduct();
            lblMessageProduct.ForeColor = Color.Red;
            lblMessageProduct.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageProduct.Text = Message;
                return;
            }
            PLeadProduct Lead = new PLeadProduct();
            Lead = UC_Product.ReadProduct();
            Lead.LeadID = LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Product", Lead));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageProduct.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_Product.Hide();
            fillProduct(LeadID);
        }

        void fillAssignSalesEngineer(long LeadID)
        { 
            gvSalesEngineer.DataSource = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID, null);
            gvSalesEngineer.DataBind();
            UC_AssignSE.FillMaster();
        }
        void fillFollowUp(long LeadID)
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUpByID(LeadID, null); 
            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();

           
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


            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID,true);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind((DropDownList)UC_Effort.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID",false);
        }
        void fillExpense(long LeadID)
        {
            gvExpense.DataSource = new BLead().GetLeadExpense(LeadID, PSession.User.UserID);
            gvExpense.DataBind();

            List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(LeadID, PSession.User.UserID,true);
            List<PUser> U = new List<PUser>();
            foreach (PLeadSalesEngineer SE in SalesEngineer)
            {
                U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            }
            new DDLBind((DropDownList)UC_Expense.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID", false);
        }
        void fillProduct(long LeadID)
        {
            gvProduct.DataSource = new BLead().GetLeadProduct(LeadID, PSession.User.UserID);
            gvProduct.DataBind();
            UC_Product.FillMaster();
           

        }

        void fillLeadEdit()
        {
            txtLeadDate.Text = Lead.LeadDate.ToString("yyyy-MM-dd");


            List<PLeadCategory> Category = new BLead().GetLeadCategory(null, null);
            new DDLBind(ddlCategory, Category, "Category", "CategoryID"); 

            List<PLeadQualification> Qualification = new BLead().GetLeadQualification(null, null);
            new DDLBind(ddlQualification, Qualification, "Qualification", "QualificationID"); 

            List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
            new DDLBind(ddlSource, Source, "Source", "SourceID"); 

            List<PLeadType> LeadType = new BLead().GetLeadType(null, null);
            new DDLBind(ddlLeadType, LeadType, "Type", "TypeID");  

            List<PLeadProgressStatus> ProgressStatus = new BLead().GetLeadProgressStatus(null, null); 
            new DDLBind(ddlProgressStatus, ProgressStatus, "ProgressStatus", "ProgressStatusID");

            List<PLeadStatus> Status = new BLead().GetLeadStatus(null, null); 
            new DDLBind(ddlStatus, Status, "Status", "StatusID"); 


            ddlStatus.SelectedValue = Convert.ToString(Lead.Status.StatusID);
            ddlProgressStatus.SelectedValue = Convert.ToString(Lead.ProgressStatus.ProgressStatusID);
            ddlCategory.SelectedValue = Convert.ToString(Lead.Category.CategoryID);
            ddlQualification.SelectedValue = Convert.ToString(Lead.Qualification.QualificationID);
            ddlSource.SelectedValue = Convert.ToString(Lead.Source.SourceID);
            ddlLeadType.SelectedValue = Convert.ToString(Lead.Type.TypeID);
            txtRemarks.Text = Lead.Remarks;
        }
        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BLead().GetAttachedFileLead(LeadID);
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
            F.ReferenceID = LeadID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/AttachedFile", F));
            ShowMessage(Results);
            if (Results.Status == PApplication.Failure)
            { 
                lblMessage.ForeColor = Color.Red;
                return;
            } 
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
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/AttachedFile", F));
            ShowMessage(Results);
            if (Results.Status == PApplication.Failure)
            { 
                lblMessage.ForeColor = Color.Red;
                return;
            } 
            fillSupportDocument();
        }

        protected void BtnSaveQuotation_Click(object sender, EventArgs e)
        {
            MPE_Quotation.Show();
            string Message = UC_Quotation.ValidationSalesQuotation();
            lblMessageQuotation.ForeColor = Color.Red;
            lblMessageQuotation.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageQuotation.Text = Message;
                return;
            }
            PSalesQuotation Sq = new PSalesQuotation();
            Sq = UC_Quotation.ReadSalesQuotation();
            Sq.Lead = new PLead { LeadID = Lead.LeadID };
            Sq.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation", Sq));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageQuotation.Text = Results.Message;
                return;
            }
            Response.Redirect("Quotation.aspx?Quotation=" + Results.Data);
            ShowMessage(Results);
            MPE_Quotation.Hide(); 
        }
         
        protected void btnLostReasonUpdate_Click(object sender, EventArgs e)
        {
            string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=6&Reason=" + txtLostReason.Text.Trim() + "&UserID=" + PSession.User.UserID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            ShowMessage(Results); 
            if (Results.Status == PApplication.Failure)
            { 
                lblMessage.ForeColor = Color.Red;
                return;
            } 
            txtLostReason.Text = "";
            fillViewLead(Lead.LeadID);
        }

        protected void btnRejectedBySalesUpdate_Click(object sender, EventArgs e)
        {
            string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=7&Reason=" + txtRejectedBySalesReason.Text.Trim() + "&UserID=" + PSession.User.UserID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            ShowMessage(Results);
            if (Results.Status == PApplication.Failure)
            { 
                lblMessage.ForeColor = Color.Red;
                return;
            } 
            txtRejectedBySalesReason.Text = "";
            fillViewLead(Lead.LeadID);
        }

        protected void btnLeadEdit_Click(object sender, EventArgs e)
        {
            MPE_Lead.Show();
            PLead LeadEdit = new PLead();
            lblMessageLead.ForeColor = Color.Red;
            lblMessageLead.Visible = true;
            string Message =  ValidationLead();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageLead.Text = Message;
                return;
            }
            LeadEdit.LeadID = Lead.LeadID;
            LeadEdit.Customer = Lead.Customer;
            LeadEdit.LeadDate = Convert.ToDateTime(txtLeadDate.Text.Trim());

            LeadEdit.Status = new PLeadStatus() { StatusID = Convert.ToInt32(ddlStatus.SelectedValue) };
            LeadEdit.ProgressStatus = new PLeadProgressStatus() { ProgressStatusID = Convert.ToInt32(ddlProgressStatus.SelectedValue) };

            LeadEdit.Category = new PLeadCategory() { CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) };
            LeadEdit.Qualification = new PLeadQualification() { QualificationID = Convert.ToInt32(ddlQualification.SelectedValue) };
            LeadEdit.Source = new PLeadSource() { SourceID = Convert.ToInt32(ddlSource.SelectedValue) };
            LeadEdit.Type = new PLeadType() { TypeID = Convert.ToInt32(ddlLeadType.SelectedValue) };
            LeadEdit.Remarks = txtRemarks.Text.Trim();
            LeadEdit.CreatedBy = new PUser { UserID = PSession.User.UserID };


            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead", LeadEdit));
            if (Results.Status == PApplication.Failure)
            { 
                lblMessageLead.Text = "Customer is not updated successfully ";
                return;
            } 
            ShowMessage(Results);
            fillViewLead(Lead.LeadID);

            MPE_Lead.Hide();
        }
        public string ValidationLead()
        {
            string Message = "";
            txtLeadDate.BorderColor = Color.Silver;
            ddlStatus.BorderColor = Color.Silver;
            ddlProgressStatus.BorderColor = Color.Silver;
            ddlCategory.BorderColor = Color.Silver;
            ddlQualification.BorderColor = Color.Silver;
            ddlSource.BorderColor = Color.Silver;
            ddlStatus.BorderColor = Color.Silver;
            txtRemarks.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtLeadDate.Text.Trim()))
            {
                Message = "Please enter the Lead Date";
                txtLeadDate.BorderColor = Color.Red;
            }
            else if (ddlStatus.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Status";
                ddlStatus.BorderColor = Color.Red;
            }
            else if (ddlProgressStatus.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Progress Status";
                ddlProgressStatus.BorderColor = Color.Red;
            }
            else if (ddlCategory.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Category";
                ddlCategory.BorderColor = Color.Red;
            }
            else if (ddlQualification.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Qualification";
                ddlQualification.BorderColor = Color.Red;
            }
            else if (ddlSource.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Source";
                ddlSource.BorderColor = Color.Red;
            }
            else if (ddlLeadType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the LeadType";
                ddlStatus.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark";
                txtRemarks.BorderColor = Color.Red;
            }
            return Message;
        }

        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        void ActionControlMange()
        {
            lbtnEditLead.Visible = true;
            lbtnConvertToProspect.Visible = true;
            lbtnLostLead.Visible = true;
            lbtnCancelLead.Visible = true;
            lbtnAssign.Visible = true;
            lbtnAddFollowUp.Visible = true;
            lbtnCustomerConvocation.Visible = true;
            lbtnAddEffort.Visible = true;
            lbtnAddExpense.Visible = true;
            lbtnAddFinancialInfo.Visible = true;
            lbtnAddProduct.Visible = true;
            lbtnAddQuotation.Visible = true;

            //lbtnEditLead.Visible = false;
            //lbtnConvertToProspect.Visible = false;
            //lbtnLostLead.Visible = false;
            //lbtnCancelLead.Visible = false;
            //lbtnAssign.Visible = false;
            //lbtnAddFollowUp.Visible = false;
            //lbtnCustomerConvocation.Visible = false;
            //lbtnAddEffort.Visible = false;
            //lbtnAddExpense.Visible = false;
            //lbtnAddFinancialInfo.Visible = false;
            //lbtnAddProduct.Visible = false;
            //lbtnAddQuotation.Visible = false;

             
        }

    }
}