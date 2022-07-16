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
        //public long LeadID
        //{
        //    get
        //    {
        //        if (Session["LeadID"] == null)
        //        {
        //            Session["LeadID"] = 0;
        //        }
        //        return Convert.ToInt64(Session["LeadID"]);
        //    }
        //    set
        //    {
        //        Session["LeadID"] = value;
        //    }
        //}
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
            lblMessageConversation.Text = "";
            lblMessageAssignEngineer.Text = "";
            lblMessageFinancial.Text = "";
            lblMessageProduct.Text = "";
            lblMessageQuotation.Text = ""; 
        }
        public void fillViewLead(long LeadID)
        { 
            Lead = new BLead().GetLeadByID(LeadID);
            lblLeadNumber.Text = Lead.LeadNumber;
            lblLeadDate.Text = Lead.LeadDate.ToLongDateString();
           // lblCategory.Text = Lead.Category==null?"": Lead.Category.Category;
            lblUrgency.Text = Lead.Urgency==null?"": Lead.Urgency.Urgency;
            lblApplication.Text = Lead.Application == null ? "" : Lead.Application.MainApplication;
            lblQualification.Text = Lead.Qualification == null ? "" : Lead.Qualification.Qualification;
            lblSource.Text = Lead.Source == null ? "" : Lead.Source.Source;
            lblStatus.Text = Lead.Status.Status;
            lblProject.Text = Lead.Project == null ? "" : Lead.Project.ProjectName;
            lblDealer.Text = Lead.Dealer.DealerCode;
            lblCustomerFeedback.Text = Lead.CustomerFeedback;
            lblRemarks.Text = Lead.Remarks;
            lblCustomer.Text = Lead.Customer.CustomerFullName;
            lblContactPerson.Text = Lead.Customer.ContactPerson;
            //lblMobile.Text = Lead.Customer.Mobile;
            lblMobile.Text = "<a href='tel:" + Lead.Customer.Mobile + "'>" + Lead.Customer.Mobile  + "</a>";
            lblEmail.Text = "<a href='mailto:"+ Lead.Customer.Email + "'>" + Lead.Customer.Email + "</a>"; 

            string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            lblLocation.Text = Location;
            lblTotalEffort.Text = Convert.ToString(Lead.TotalEffort);
            lblTotalExpense.Text = Convert.ToString(Lead.TotalExpense);
             

            fillAssignSalesEngineer();
            fillFollowUp();
            fillConversation();
            fillFinancial();
            fillEffort();
            fillExpense();
            fillProduct();
            fillQuestionaries();
            ActionControlMange();
            fillVisit();
            fillEffortConversationVisit();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Lead")
            {
                MPE_Lead.Show();
                UC_AddLead.FillMaster();
                UC_AddLead.fillLead(Lead);
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
                UC_AssignSE.FillMaster(Lead);
            }
            else if (lbActions.Text == "Add Follow-up")
            {
                MPE_FollowUp.Show(); 

                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID,true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind((DropDownList)UC_FollowUp.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID", false);
                UC_FollowUp.FillMaster();
            }
            else if (lbActions.Text == "Customer Conversation")
            {
                MPE_Conversation.Show(); 
                UC_CustomerConversation.FillMaster(Lead.LeadID);

                
            }
            else if (lbActions.Text == "Financial Info")
            {
                MPE_Financial.Show(); 
                UC_Financial.FillMaster();
            }
            else if (lbActions.Text == "Add Effort")
            { 
                DropDownList ddlSalesEngineer = (DropDownList)UC_Effort.FindControl("ddlSalesEngineer");
                DropDownList ddlEffortType = (DropDownList)UC_Effort.FindControl("ddlEffortType");

                new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                ddlSalesEngineer.Enabled = false;

                MPE_Effort.Show(); 

                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID", false);
            }
            else if (lbActions.Text == "Add Expense")
            { 
                DropDownList ddlSalesEngineer = (DropDownList)UC_Expense.FindControl("ddlSalesEngineer");
                DropDownList ddlExpenseType = (DropDownList)UC_Expense.FindControl("ddlExpenseType");
                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
                ddlSalesEngineer.Enabled = false;
                MPE_Expense.Show();
                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID", false);
            }
            else if (lbActions.Text == "Add Product")
            {
                MPE_Product.Show();
                UC_Product.FillMaster(Lead); 
            }
            else if (lbActions.Text == "Convert to Quotation")
            {
                lblMessage.Text = "";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(Lead.Customer.CustomerCode))
                {
                    lblMessage.Text = "Customer Code is Not Create";
                    return;
                }
                if (Lead.LeadProduct  == null)
                {
                    lblMessage.Text = "Please select the Product";
                    return;
                }
                if (Lead.LeadProduct.Count == 0)
                {
                    lblMessage.Text = "Please select the Product";
                    return;
                }
                MPE_Quotation.Show();
                UC_Quotation.FillMaster(Lead);
            }
            else if (lbActions.Text == "Add Questionaries")
            {
                new DDLBind(ddlQuestionariesMain, new BLead().GetLeadQuestionariesMain(null, null), "LeadQuestionariesMain", "LeadQuestionariesMainID");
                MPE_Questionaries.Show(); 
            }
            else if (lbActions.Text == "Add Visit")
            {
                MPE_Visit.Show(); 
                new DDLBind(ddlActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
                new DDLBind(ddlImportance, new BDMS_Master().GetImportance(null, null), "Importance", "ImportanceID");
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
            PLeadEffort Effort = new PLeadEffort();
            Effort = UC_Effort.ReadEffort();
            Effort.LeadEffortID = 0;
            Effort.LeadID = Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Effort", Effort));

            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_Effort.Hide();
            fillViewLead(Lead.LeadID);
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
            PLeadExpense Expense = new PLeadExpense();
            Expense = UC_Expense.ReadExpense();
            Expense.LeadExpenseID = 0;
            Expense.LeadID = Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Expense", Expense));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageExpense.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_Expense.Hide();
            fillViewLead(Lead.LeadID);
        }
        protected void lblFinancialAdd_Click(object sender, EventArgs e)
        {

            DropDownList ddlBankNameF = (DropDownList)gvFinancial.FooterRow.FindControl("ddlBankNameF");
            TextBox txtFinancePercentageF = (TextBox)gvFinancial.FooterRow.FindControl("txtFinancePercentageF");
            TextBox txtRemarkF = (TextBox)gvFinancial.FooterRow.FindControl("txtRemarkF");
            PLeadFinancial Financial = new PLeadFinancial();
            Financial.LeadFinancialID = 0;
            Financial.LeadID = Lead.LeadID;
            Financial.BankName = new PBankName { BankNameID = Convert.ToInt32(ddlBankNameF.SelectedValue) };
            Financial.FinancePercentage = Convert.ToDecimal(txtFinancePercentageF.Text.Trim());
            Financial.Remark = txtRemarkF.Text;
            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

           
           
            fillFinancial();
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
            PLeadFollowUp FollowUp = new PLeadFollowUp();
            FollowUp = UC_FollowUp.ReadFollowUp();
            FollowUp.LeadFollowUpID = 0;
            FollowUp.LeadID = Lead.LeadID;
            PApiResult Results =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/FollowUp", FollowUp));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFollowUp.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_FollowUp.Hide();
            fillFollowUp();
        }
        protected void btnSaveustomerConversation_Click(object sender, EventArgs e)
        {
            MPE_Conversation.Show();
            string Message = UC_CustomerConversation.ValidationConversation();
            lblMessageConversation.ForeColor = Color.Red;
            lblMessageConversation.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageConversation.Text = Message;
                return;
            }
            PLeadConversation Conversation = new PLeadConversation();
            Conversation = UC_CustomerConversation.ReadConversation();
            Conversation.LeadID = Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Conversation", Conversation));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageConversation.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_Conversation.Hide();
            fillConversation();
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
            PLeadSalesEngineer Engineer = new PLeadSalesEngineer();
            Engineer = UC_AssignSE.ReadAssignSE();
            List<PLeadSalesEngineer> EngineerSE = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, true);
            if(EngineerSE.Count !=0)
            {
                if(EngineerSE[0].SalesEngineer.UserID == Engineer.SalesEngineer.UserID)
                {
                    lblMessageAssignEngineer.Text = "Already this engineer assigned";
                    return;
                }
            }

            Engineer.LeadID = Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/SalesEngineer", Engineer));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAssignEngineer.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_AssignSE.Hide();
            tbpCust.ActiveTabIndex = 0;

            fillAssignSalesEngineer();
            fillFollowUp();
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
            PLeadFinancial Financial = new PLeadFinancial();
            Financial = UC_Financial.ReadFinancial();
            Financial.LeadID = Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Financial", Financial));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFinancial.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_Financial.Hide();
            fillFinancial();
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
            PLeadProduct Product = new PLeadProduct();
            Product = UC_Product.ReadProduct();
            Product.LeadID = Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Product", Product));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageProduct.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_Product.Hide();
            fillProduct();
        }
        void fillAssignSalesEngineer()
        { 
            gvSalesEngineer.DataSource = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, null);
            gvSalesEngineer.DataBind();
            
        }
        void fillFollowUp()
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUpByID(Lead.LeadID, null); 
            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();

           
        }
        void fillConversation()
        { 
            gvConversation.DataSource = new BLead().GetLeadConversation(Lead.LeadID, PSession.User.UserID); ;
            gvConversation.DataBind();
        }
        void fillEffortConversationVisit()
        {
            gvEffortConversationVisit.DataSource = new BLead().GetLeadEffortConversationVisit(Lead.LeadID);
            gvEffortConversationVisit.DataBind();
        }
        void fillFinancial()
        { 
            gvFinancial.DataSource = new BLead().GetLeadFinancial(Lead.LeadID, PSession.User.UserID);
            gvFinancial.DataBind();
        }
        void fillEffort()
        {
            gvEffort.DataSource = new BLead().GetLeadEffort(Lead.LeadID, PSession.User.UserID);
            gvEffort.DataBind();


        }
        void fillExpense()
        {
            gvExpense.DataSource = new BLead().GetLeadExpense(Lead.LeadID, PSession.User.UserID);
            gvExpense.DataBind();

         
        }
        void fillProduct()
        {
            Lead.LeadProduct = new BLead().GetLeadProduct(Lead.LeadID, PSession.User.UserID);
            gvProduct.DataSource = Lead.LeadProduct;
            gvProduct.DataBind(); 
        }
      
        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BLead().GetAttachedFileLead(Lead.LeadID);
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
            F.ReferenceID = Lead.LeadID;
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
            F.ReferenceID = Lead.LeadID;
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
            string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=5&Reason=" + txtLostReason.Text.Trim() + "&UserID=" + PSession.User.UserID;
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
            string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=6&Reason=" + txtRejectedBySalesReason.Text.Trim() + "&UserID=" + PSession.User.UserID;
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
            PLead_Insert LeadEdit = new PLead_Insert();
            lblMessageLead.ForeColor = Color.Red;
            lblMessageLead.Visible = true;
            string Message = UC_AddLead.Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageLead.Text = Message;
                return;
            }
            LeadEdit = UC_AddLead.Read();

            LeadEdit.LeadID = Lead.LeadID;
            LeadEdit.Customer = Lead.Customer;  


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
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
       

        protected void ddlQuestionariesMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlQuestionariesSub, new BLead().GetLeadQuestionariesSub(null, Convert.ToInt32(ddlQuestionariesMain.SelectedValue), null), "LeadQuestionariesSub", "LeadQuestionariesSubID");
            MPE_Questionaries.Show();
        }

        public string ValidationQuestionaries()
        {
            string Message = "";
            ddlQuestionariesMain.BorderColor = Color.Silver;
            ddlQuestionariesSub.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver;
            if (ddlQuestionariesMain.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Questionaries Main";
                ddlQuestionariesMain.BorderColor = Color.Red;
            }

            else if (ddlQuestionariesSub.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Questionaries Sub";
                ddlQuestionariesSub.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = "Please enter the Remark";
                txtRemark.BorderColor = Color.Red;
            }
            return Message;
        }
        protected void btnSaveMarketSegment_Click(object sender, EventArgs e)
        {
            lblMessageQuestionaries.Visible = true;
            lblMessageQuestionaries.ForeColor = Color.Red;
            MPE_Questionaries.Show();
            string Message = ValidationQuestionaries();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageQuestionaries.Text = Message;
                return;
            }
            PLeadQuestionaries Questionaries = new PLeadQuestionaries();
            Questionaries.LeadID = Lead.LeadID;
            Questionaries.QuestionariesMain = new PLeadQuestionariesMain() { LeadQuestionariesMainID = Convert.ToInt32(ddlQuestionariesMain.SelectedValue) };
            Questionaries.QuestionariesSub = new PLeadQuestionariesSub() { LeadQuestionariesSubID = Convert.ToInt32(ddlQuestionariesSub.SelectedValue) };
            Questionaries.Remark = txtRemark.Text.Trim();
            Questionaries.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Questionaries", Questionaries));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageQuestionaries.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            ddlQuestionariesMain.Items.Clear();
            ddlQuestionariesSub.Items.Clear();
            txtRemark.Text = "";
            tbpCust.ActiveTabIndex = 0;
            MPE_Questionaries.Hide();
            fillQuestionaries();
        }
        void fillQuestionaries()
        {
            gvQuestionaries.DataSource = new BLead().GetLeadQuestionaries(Lead.LeadID);
            gvQuestionaries.DataBind();
        }
        protected void lbMarketSegmentDelete_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblLeadQuestionariesID = (Label)gvRow.FindControl("lblLeadQuestionariesID");
            PLeadQuestionaries Questionaries = new PLeadQuestionaries();
            Questionaries.LeadQuestionariesID = Convert.ToInt64(lblLeadQuestionariesID.Text);
            Questionaries.LeadID = Lead.LeadID;

            Questionaries.QuestionariesMain = new PLeadQuestionariesMain() { LeadQuestionariesMainID = 0 };
            Questionaries.QuestionariesSub = new PLeadQuestionariesSub() { LeadQuestionariesSubID = 0 };
            Questionaries.Remark = txtRemark.Text.Trim();
            Questionaries.CreatedBy = new PUser() { UserID = PSession.User.UserID };


            Questionaries.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Questionaries", Questionaries));
            lblMessage.Visible = true;
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;

            fillQuestionaries();

        }

        protected void btnSaveVisit_Click(object sender, EventArgs e)
        {
            MPE_Visit.Show();
            PColdVisit ColdVisitList = new PColdVisit();
            lblMessageColdVisit.ForeColor = Color.Red;
            lblMessageColdVisit.Visible = true;
            string Message = "";

            Message = ValidationColdVisit();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageColdVisit.Text = Message;
                return;
            } 
            ColdVisitList.Customer = new PDMS_Customer() { CustomerID = Lead.Customer.CustomerID };
            ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtColdVisitDate.Text.Trim());
            ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
            ColdVisitList.Importance = new PImportance() { ImportanceID = Convert.ToInt32(ddlImportance.SelectedValue) };
            ColdVisitList.Remark = txtVisitRemark.Text.Trim();
            ColdVisitList.Location = txtLocation.Text.Trim();
            ColdVisitList.ReferenceID = Lead.LeadID;
            ColdVisitList.ReferenceTableID = 1;
            ColdVisitList.CreatedBy = new PUser { UserID = PSession.User.UserID }; 

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit", ColdVisitList));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageQuestionaries.Text = Results.Message;
                return;
            }

            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = Results.Message;
            MPE_Visit.Hide();
            fillVisit();
        }
        void fillVisit()
        {
            List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, 1, Lead.LeadID);
            gvVisit.DataSource = Visit;
            gvVisit.DataBind(); 
        }
        public string ValidationColdVisit()
        {
            string Message = "";
            txtColdVisitDate.BorderColor = Color.Silver;
            txtVisitRemark.BorderColor = Color.Silver;
            ddlActionType.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
            {
                Message = "Please enter the Cold Visit Date";
                txtColdVisitDate.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = Message + "Please enter the Location";
                txtLocation.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtVisitRemark.Text.Trim()))
            {
                Message = Message + "Please enter the Remark";
                txtVisitRemark.BorderColor = Color.Red;
            }

            else if (ddlActionType.SelectedValue == "0")
            {
                Message = Message + "Please select the Action Type";
                ddlActionType.BorderColor = Color.Red;
            }
            return Message;
        }

        void ActionControlMange()
        {
            //lbtnEditLead.Visible = false;
            //lbtnConvertToProspect.Visible = false;
            //lbtnLostLead.Visible = false;
            //lbtnCancelLead.Visible = false;
            //lbtnAssign.Visible = false;
            //lbtnAddFollowUp.Visible = false;
            //lbtnCustomerConversation.Visible = false;
            //lbtnAddEffort.Visible = false;
            //lbtnAddExpense.Visible = false;
            //lbtnAddFinancialInfo.Visible = false;
            //lbtnAddProduct.Visible = false;
            //lbtnAddQuotation.Visible = false; 

            lbtnEditLead.Visible = true; 
            lbtnAssign.Visible = true;
            lbtnAddFollowUp.Visible = true;
            lbtnCustomerConversation.Visible = true;
            lbtnAddEffort.Visible = true;
            lbtnAddExpense.Visible = true;
            lbtnAddFinancialInfo.Visible = true;
            lbtnAddProduct.Visible = true;
            lbtnAddQuotation.Visible = true;
            lbtAddQuestionaries.Visible = true;
            lbtnAddVisit.Visible = true;
            lbtnLostLead.Visible = true;
            lbtnCancelLead.Visible = true;

            if (Lead.Status.StatusID == (short)LeadStatus.Cancelled)
            {
                lbtnEditLead.Visible = false;
                lbtnAssign.Visible = false;
                lbtnAddFollowUp.Visible = false;
                lbtnCustomerConversation.Visible = false;
                lbtnAddEffort.Visible = false;
                lbtnAddExpense.Visible = false;
                lbtnAddFinancialInfo.Visible = false;
                lbtnAddProduct.Visible = false;
                lbtnAddQuotation.Visible = false;
                lbtAddQuestionaries.Visible = false;
                lbtnAddVisit.Visible = false;
                lbtnLostLead.Visible = false;
                lbtnCancelLead.Visible = false;
            }

            List<PSalesQuotation>  Quote = new BSalesQuotation().GetSalesQuotationBasic(null, null, Lead.LeadID, null, null, null, null, null, null, null, null);
            if(Quote.Count !=0)
            {
                lbtnAddQuotation.Visible = false;
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditLead).Count() == 0)
            {
                lbtnEditLead.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AssignLead).Count() == 0)
            {
                lbtnAssign.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddFollowUpLead).Count() == 0)
            {
                lbtnAddFollowUp.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CustomerConversationLead).Count() == 0)
            {
                lbtnCustomerConversation.Visible = false;
            }   
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddEffortLead).Count() == 0)
            {
                lbtnAddEffort.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddExpenseLead).Count() == 0)
            {
                lbtnAddExpense.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.FinancialInfoLead).Count() == 0)
            {
                lbtnAddFinancialInfo.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddProductLead).Count() == 0)
            {
                lbtnAddProduct.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ConvertToQuotationLead).Count() == 0)
            {
                lbtnAddQuotation.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddQuestionariesLead).Count() == 0)
            {
                lbtAddQuestionaries.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddVisitLead).Count() == 0)
            {
                lbtnAddVisit.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.LostLead).Count() == 0)
            {
                lbtnLostLead.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CancelLead).Count() == 0)
            {
                lbtnCancelLead.Visible = false;
            }
        }

    }
}