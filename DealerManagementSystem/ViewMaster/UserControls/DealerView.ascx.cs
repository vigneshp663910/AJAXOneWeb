 
using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class DealerView : System.Web.UI.UserControl
    {
        public PDMS_Dealer Dealer
        {
            get
            {
                if (Session["DealerView"] == null)
                {
                    Session["DealerView"] = new PDMS_Dealer();
                }
                return (PDMS_Dealer)Session["DealerView"];
            }
            set
            {
                Session["DealerView"] = value;
            }
        }
        public List<PDMS_DealerOffice> DealerOfficeList
        {
            get
            {
                if (Session["DealerOffice"] == null)
                {
                    Session["DealerOffice"] = new List<PDMS_DealerOffice>();
                }
                return (List<PDMS_DealerOffice>)Session["DealerOffice"];
            }
            set
            {
                Session["DealerOffice"] = value;
            }
        }
        public List<PDMS_DealerEmployee> DealerEmployeeList
        {
            get
            {
                if (Session["DealerEmployee"] == null)
                {
                    Session["DealerEmployee"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["DealerEmployee"];
            }
            set
            {
                Session["DealerEmployee"] = value;
            }
        }
        public List<PDealerNotification> DealerNotificationList
        {
            get
            {
                if (Session["DealerNotification"] == null)
                {
                    Session["DealerNotification"] = new List<PDealerNotification>();
                }
                return (List<PDealerNotification>)Session["DealerNotification"];
            }
            set
            {
                Session["DealerNotification"] = value;
            }
        }
        public List<PDMS_DealerEmployee> DealerResponsibleUserList
        {
            get
            {
                if (Session["DealerResponsibleUser"] == null)
                {
                    Session["DealerResponsibleUser"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["DealerResponsibleUser"];
            }
            set
            {
                Session["DealerResponsibleUser"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                //ActionControlMange();
                HiddenID.Value = "0";
            }
            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["DealerID"])))
            {
                int DealerID = Convert.ToInt32(Convert.ToString(ViewState["DealerID"]));
                if (DealerID != Dealer.DealerID)
                {
                    Dealer = new BDMS_Dealer().GetDealer(DealerID, "", null, null)[0];
                }
            }
        }
        public void filldealer(int DealerID)
        {
            ViewState["DealerID"] = DealerID;
            Dealer = new BDMS_Dealer().GetDealer(DealerID, "", null, null)[0];

            lblDealerCode.Text = Dealer.DealerCode;
            lblDealerName.Text = Dealer.DealerName;
            lblMobile.Text = "<a href='tel:" + Dealer.Mobile + "'>" + Dealer.Mobile + "</a>";
            lblEmail.Text = "<a href='mailto:" + Dealer.Email + "'>" + Dealer.Email + "</a>";
            cbIsActive.Checked = Dealer.IsActive;
            lblDealerCountry.Text = Dealer.Country;
            lblDealerState.Text = Dealer.StateN.State;
            lblDealerRegion.Text = Dealer.Region.Region;
            //lblTeamLead.Text = Dealer.TL.ContactName;
            //lblSerivceManager.Text = Dealer.SM.ContactName;

            List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(DealerID, null, null);
            lblDealerBank.Text = DealerBank[0].DealerBank.BankName;
            lblDealerBankBranch.Text = DealerBank[0].DealerBank.Branch;
            lblIFSCCode.Text = DealerBank[0].DealerBank.IfscCode;
            lblAccountNo.Text = DealerBank[0].DealerBank.AcNumber;
            lblDealerBankID.Text = DealerBank[0].DealerBank.DealerBankID.ToString();

            //new DDLBind().FillDealerAndEngneer(ddlDealerDN, null);
            new DDLBind(ddlDealerDN, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
            new DDLBind(ddlDealerNotificationModuleG, new BDMS_Dealer().GetDealerNotificationModule(), "ModuleName", "DealerNotificationModuleID");

            fillDealerOffice();
            fillDealerEmployee();
            fillDealerNotification();
            fillDealerResponsibleUser();
            ActionControlMange();
        }
        protected void lnkBtnActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.Text == "Add Branch Office")
                {
                    lblMessageAddBranchOffice.Text = string.Empty;
                    cbIsHeadOffice.Checked = false;
                    txtSapLocationCode.Text = string.Empty;
                    txtOfficeCode.Text = string.Empty;
                    txtOfficeName.Text = string.Empty;
                    txtDealerOfficeAddress1.Text = string.Empty;
                    txtDealerOfficeAddress2.Text = string.Empty;
                    txtDealerOfficeAddress3.Text = string.Empty;
                    txtDealerOfficeCity.Text = string.Empty;
                    txtDealerOfficePincode.Text = string.Empty;
                    txtDealerOfficeGSTIN.Text = string.Empty;
                    txtDealerOfficePAN.Text = string.Empty;
                    txtDealerOfficeMobile.Text = string.Empty;
                    txtDealerOfficeEmail.Text = string.Empty;

                    new DDLBind(ddlDealerOfficeState, new BDMS_Address().GetState(Dealer.DealerID, 1, null, null, null), "State", "StateID");
                    
                    ddlDealerOfficeDistrict.Items.Clear();
                    ddlDealerOfficeDistrict.SelectedValue = "0";

                    MPE_AddBranchOffice.Show();
                }
                if (lbActions.Text == "Add Dealer Notification")
                {
                    lblMessageAddNotification.Text = string.Empty;
                    List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Dealer.DealerID, true, null, null,null);
                    new DDLBind(ddlDealerNotificationModule, new BDMS_Dealer().GetDealerNotificationModule(), "ModuleName", "DealerNotificationModuleID");
                    new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");
                    MPE_AddNotification.Show();
                    int AFDealerID = Convert.ToInt32(ConfigurationManager.AppSettings["AjaxDealerID"]);
                    if (Dealer.DealerID == AFDealerID)
                    {
                        lblDealer.Visible = true;
                        ddlDealer.Visible = true;
                    }
                    else
                    {
                        lblDealer.Visible = false;
                        ddlDealer.Visible = false;
                    }
                    new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID", false);
                    ddlDealer.SelectedValue = Convert.ToString(Dealer.DealerID);
                }
                if (lbActions.Text == "Edit Bank Details")
                {
                    lblMessageDealerEdit.Text = string.Empty;
                    txtBank.Text = lblDealerBank.Text;
                    txtBranch.Text = lblDealerBankBranch.Text;
                    txtIFSCCode.Text = lblIFSCCode.Text;
                    txtAccountNo.Text = lblAccountNo.Text;
                    MPE_EditBank.Show();
                }
                if (lbActions.Text == "Edit Dealer Responsible User")
                {
                    ddlDealerResposibleUserType.SelectedValue = "0";
                    //ddlDealerResponsibleUser.SelectedValue = "0";
                    //ddlDealerResposibleUserType.Items.Clear();
                    ddlDealerResponsibleUser.Items.Clear();
                    lblEditDealerResponsibleUserMessage.Text = "";
                    lblEditDealerResponsibleUserMessage.Visible = false;
                    MPE_EditDealerResposibleUser.Show();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        void fillDealerOffice()
        {
            DealerOfficeList = new BDMS_Dealer().GetDealerOffice(Dealer.DealerID, null, null);
            DealerOfficeBind();
            //gvDealerOffice.DataSource = DealerOfficeList;
            //gvDealerOffice.DataBind();
        }
        void fillDealerEmployee()
        {
            DealerEmployeeList = new BDMS_Dealer().GetDealerEmployeeByDealerID(Dealer.DealerID, null, null, null, null);
            DealerEmployeeBind();
            //gvDealerEmployee.DataSource = DealerEmployeeList;
            //gvDealerEmployee.DataBind();
        }
        void fillDealerNotification()
        {
            int AFDealerID = Convert.ToInt32(ConfigurationManager.AppSettings["AjaxDealerID"]);
            if (Dealer.DealerID == AFDealerID)
            {
                divDealerNotification.Visible = true ;
            }
            else
            {
                divDealerNotification.Visible = false;
            }

            int? UsersDealerID = ddlDealerDN.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerDN.SelectedValue);
            int? DealerDepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            int? DealerDesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            int? DealerNotificationModuleID = (ddlDealerNotificationModuleG.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerNotificationModuleG.SelectedValue);

            DealerNotificationList = new BDMS_Dealer().GetDealerNotification(Dealer.DealerID, UsersDealerID, DealerDepartmentID, DealerDesignationID, DealerNotificationModuleID);
            DealerNotificationBind();
            //gvDealerNotification.DataSource = DealerNotificationList;
            //gvDealerNotification.DataBind();
        }
        protected void ibtnDealerOfficeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerOffice.PageIndex > 0)
            {
                gvDealerOffice.PageIndex = gvDealerOffice.PageIndex - 1;
                DealerOfficeBind();
            }
        }
        protected void ibtnDealerOfficeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerOffice.PageCount > gvDealerOffice.PageIndex)
            {
                gvDealerOffice.PageIndex = gvDealerOffice.PageIndex + 1;
                DealerOfficeBind();
            }
        }
        void DealerOfficeBind()
        {
            gvDealerOffice.DataSource = DealerOfficeList;
            gvDealerOffice.DataBind();
            lblRowCountDealerOffice.Text = (((gvDealerOffice.PageIndex) * gvDealerOffice.PageSize) + 1) + " - " + (((gvDealerOffice.PageIndex) * gvDealerOffice.PageSize) + gvDealerOffice.Rows.Count) + " of " + DealerOfficeList.Count;
        }
        protected void gvDealerOffice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerOffice.PageIndex = e.NewPageIndex;
            fillDealerOffice();
        }
        protected void ibtnDealerEmployeeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;
                DealerEmployeeBind();
            }
        }
        protected void ibtnDealerEmployeeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                DealerEmployeeBind();
            }
        }
        void DealerEmployeeBind()
        {
            gvDealerEmployee.DataSource = DealerEmployeeList;
            gvDealerEmployee.DataBind();
            lblRowCountDealerEmployee.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + DealerEmployeeList.Count;
        }
        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            fillDealerEmployee();
        }
        protected void lnkbtnDealerOfficeDelete_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblOfficeCode = (Label)gvRow.FindControl("lblOfficeCode");
            PDMS_DealerOffice DealerOffice = new PDMS_DealerOffice();
            DealerOffice.OfficeCode = lblOfficeCode.Text;
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            DealerOffice.OfficeID = Convert.ToInt32(lblOfficeID.Text);

            //Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = 0 };
            //Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = 0 };
            //Attribute.Remark = txtRemark.Text.Trim();
            //Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };


            //Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            //PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute));
            //lblMessage.Visible = true;
            //if (Result.Status == PApplication.Failure)
            //{
            //    lblMessage.Text = Result.Message;
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            //lblMessage.Text = Result.Message;
            //lblMessage.ForeColor = Color.Green;

            fillDealerOffice();

        }
        protected void lnkBtnNotificationDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblDealerNotificationID = (Label)gvRow.FindControl("lblDealerNotificationID");
            Label lblDealerNotificationModuleID = (Label)gvRow.FindControl("lblDealerNotificationModuleID");
            Label lblUserID = (Label)gvRow.FindControl("lblUserID");
            CheckBox chkbxIsSMS = (CheckBox)gvRow.FindControl("chkbxIsSMS");
            CheckBox chkbxIsMail = (CheckBox)gvRow.FindControl("chkbxIsMail");
            PDealerNotification DealerNotification = new PDealerNotification();
            DealerNotification.DealerNotificationID = Convert.ToInt32(lblDealerNotificationID.Text);

            DealerNotification.Dealer = new PDMS_Dealer() { DealerID = Dealer.DealerID };
            DealerNotification.User = new PUser() { UserID = Convert.ToInt32(lblUserID.Text) };
            DealerNotification.Module = new PDealerNotificationModule() { DealerNotificationModuleID = Convert.ToInt32(lblDealerNotificationModuleID.Text) };
            DealerNotification.IsSMS = Convert.ToBoolean(chkbxIsSMS.Checked);
            DealerNotification.IsMail = Convert.ToBoolean(chkbxIsMail.Checked);
            DealerNotification.IsActive = false;

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/DealerNotification", DealerNotification));
            lblMessage.Visible = true;
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;

            fillDealerNotification();

        }
        void ActionControlMange()
        {
            lnkBtnEditBank.Visible = true;
            lnkBtnAddNotification.Visible = true;
            lnkBtnEditDealerResponsibleUser.Visible = true;

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerNotificationAdd).Count() == 0)
            {
                lnkBtnAddNotification.Visible = false;
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBankDetailsEdit).Count() == 0)
            {
                lnkBtnEditBank.Visible = false;
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerResponsibleUserEdit).Count() == 0)
            {
                lnkBtnEditDealerResponsibleUser.Visible = false;
            }
        }
        protected void btnAddNotification_Click(object sender, EventArgs e)
        {
            MPE_AddNotification.Show();
            lblMessageAddNotification.Visible = true;
            lblMessageAddNotification.ForeColor = Color.Red;
           
            
            string Message = ValidationAddNotification();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageAddNotification.Text = Message;
                return;
            }
            PDealerNotification DealerNotification = new PDealerNotification();
            DealerNotification.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(ddlDealer.SelectedValue) };
            DealerNotification.User = new PUser() { UserID = Convert.ToInt32(ddlEmployee.SelectedValue) };
            DealerNotification.Module = new PDealerNotificationModule() { DealerNotificationModuleID = Convert.ToInt32(ddlDealerNotificationModule.SelectedValue) };
            DealerNotification.IsSMS = cbSendSMS.Checked;
            DealerNotification.IsMail = cbSendEmail.Checked;
            DealerNotification.IsActive = true;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/DealerNotification", DealerNotification));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAddNotification.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            ddlEmployee.Items.Clear();
            tbpDealer.ActiveTabIndex = 3;
            MPE_AddNotification.Hide();
            fillDealerNotification();
        }
        public string ValidationAddNotification()
        {
            string Message = "";
            ddlDealerNotificationModule.BorderColor = Color.Silver;
            if ((ddlDealerNotificationModule.SelectedValue == "0") || (ddlDealerNotificationModule.SelectedValue == ""))
            {
                Message = Message + "<br/>Please select the Module";
                ddlDealerNotificationModule.BorderColor = Color.Red;
                goto msg;
            }
            ddlEmployee.BorderColor = Color.Silver;
            if ((ddlEmployee.SelectedValue == "0") || (ddlEmployee.SelectedValue == ""))
            {
                Message = Message + "<br/>Please select the Employee";
                ddlEmployee.BorderColor = Color.Red;
                goto msg;
            }
            if(cbSendSMS.Checked==false && cbSendEmail.Checked == false)
            {
                Message = Message + "<br/>Please Check Email or SMS";
                cbSendSMS.BorderColor = Color.Red;
                cbSendEmail.BorderColor = Color.Red;
                goto msg;
            }
            msg:
            return Message;
        }
        protected void ibtnDealerNotificationArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerNotification.PageIndex > 0)
            {
                gvDealerNotification.PageIndex = gvDealerEmployee.PageIndex - 1;
                DealerNotificationBind();
            }
        }
        protected void ibtnDealerNotificationArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerNotification.PageCount > gvDealerNotification.PageIndex)
            {
                gvDealerNotification.PageIndex = gvDealerNotification.PageIndex + 1;
                DealerNotificationBind();
            }
        }
        void DealerNotificationBind()
        {
            gvDealerNotification.DataSource = DealerNotificationList;
            gvDealerNotification.DataBind();
            lblRowCountDealerNotification.Text = (((gvDealerNotification.PageIndex) * gvDealerNotification.PageSize) + 1) + " - " + (((gvDealerNotification.PageIndex) * gvDealerNotification.PageSize) + gvDealerNotification.Rows.Count) + " of " + DealerNotificationList.Count;
        }
        protected void gvDealerNotification_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerNotification.PageIndex = e.NewPageIndex;
            fillDealerNotification();
        }
        protected void btnEditBank_Click(object sender, EventArgs e)
        {
            PDealerBankDetails BankDetails = new PDealerBankDetails();
            BankDetails.DealerID = Convert.ToInt32(Dealer.DealerID);
            BankDetails.DealerBankID = Convert.ToInt32(lblDealerBankID.Text);
            BankDetails.BankName = txtBank.Text;
            BankDetails.Branch = txtBranch.Text;
            BankDetails.AcNumber = txtAccountNo.Text;
            BankDetails.IfscCode = txtIFSCCode.Text;

            if (new BDMS_Dealer().InsertOrUpdateDealerBankDetails(BankDetails, 0))
            {
                lblMessageEditBank.Text = "Bank Details updated for the Dealer.";
                lblMessage.ForeColor = Color.Green;
                lblMessageEditBank.Visible = true;
            }
            else
            {
                lblMessage.Text = "Bank Details not updated for the Dealer.";
                lblMessage.ForeColor = Color.Red;
                lblMessageEditBank.Visible = true;
            }
            filldealer(Dealer.DealerID);
        }
        void fillDealerResponsibleUser()
        {
            DealerResponsibleUserList = new BDMS_Dealer().GetDealerResponsibleUser(Dealer.DealerID, Dealer.DealerCode);
            DealerResponsibleUserBind();
            //gvDealerResponsibleUser.DataSource = DealerResponsibleUserList;
            //gvDealerResponsibleUser.DataBind();
        }
        void DealerResponsibleUserBind()
        {
            gvDealerResponsibleUser.DataSource = DealerResponsibleUserList;
            gvDealerResponsibleUser.DataBind();
            lblRowCountDealerResponsibleUser.Text = (((gvDealerResponsibleUser.PageIndex) * gvDealerResponsibleUser.PageSize) + 1) + " - " + (((gvDealerResponsibleUser.PageIndex) * gvDealerResponsibleUser.PageSize) + gvDealerResponsibleUser.Rows.Count) + " of " + DealerResponsibleUserList.Count;
        }
        protected void ibtnDealerResponsibleUserArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerResponsibleUser.PageIndex > 0)
            {
                gvDealerResponsibleUser.PageIndex = gvDealerResponsibleUser.PageIndex - 1;
                DealerResponsibleUserBind();
            }
        }
        protected void ibtnDealerResponsibleUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerResponsibleUser.PageCount > gvDealerResponsibleUser.PageIndex)
            {
                gvDealerResponsibleUser.PageIndex = gvDealerResponsibleUser.PageIndex + 1;
                DealerResponsibleUserBind();
            }
        }
        protected void gvDealerResponsibleUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerResponsibleUser.PageIndex = e.NewPageIndex;
            fillDealerResponsibleUser();
        }
        protected void ddlDealerResposibleUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlDealerResposibleUserType.SelectedValue == "TL" || ddlDealerResposibleUserType.SelectedValue == "SM")
            {
                new DDLBind(ddlDealerResponsibleUser, new BUser().GetUsers(null, null, null, null, 53, true, null, 2, null), "ContactName", "UserID");
            }
            if(ddlDealerResposibleUserType.SelectedValue == "Sales Responsible User")
            {
                new DDLBind(ddlDealerResponsibleUser, new BUser().GetUsers(null, null, null, null, 53, true, null, 1, null), "ContactName", "UserID");
            }
            MPE_EditDealerResposibleUser.Show();
        }
        protected void ddlDealerOfficeState_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_AddBranchOffice.Show();
            new DDLBind(ddlDealerOfficeDistrict, new BDMS_Address().GetDistrict(1, null, Convert.ToInt32(ddlDealerOfficeState.SelectedValue), null, null, Convert.ToInt32(Dealer.DealerID)), "District", "DistrictID");
        }
        protected void btnUpdateDealerResposibleUser_Click(object sender, EventArgs e)
        {
            if (ddlDealerResponsibleUser.SelectedValue == "0")
            {
                lblEditDealerResponsibleUserMessage.Text = "";
                lblEditDealerResponsibleUserMessage.ForeColor = Color.Red;
                lblEditDealerResponsibleUserMessage.Visible = true;
            }

            if (new BDMS_Dealer().UpdateDealerResponsibleUser(Dealer.DealerID, Convert.ToInt32(ddlDealerResponsibleUser.SelectedValue), ddlDealerResposibleUserType.SelectedValue))
            {
                lblMessage.Text = "Dealer Responsible User updated for the Dealer.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Dealer Responsible User not updated for the Dealer.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            filldealer(Dealer.DealerID);
        }
        protected void btnAddUpdateBranchOffice_Click(object sender, EventArgs e)
        {
            MPE_AddBranchOffice.Show();
            lblMessageAddBranchOffice.Visible = true;
            lblMessageAddBranchOffice.ForeColor = Color.Red;
            
            string Message = ValidationAddBranchOffice();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageAddBranchOffice.Text = Message;
                return;
            }
            
            PDMS_DealerOffice_Insert dealerOffice = ReadDealerOffice();

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/DealerOffice", dealerOffice));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAddBranchOffice.Text = Results.Message;
                return;
            }
            HiddenID.Value = "0";
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            tbpDealer.ActiveTabIndex = 1;
            MPE_AddBranchOffice.Hide();
            fillDealerOffice();
        }
        private PDMS_DealerOffice_Insert ReadDealerOffice()
        {
            PDMS_DealerOffice_Insert dealerOffice = new PDMS_DealerOffice_Insert();
            dealerOffice.DealerID = Dealer.DealerID;
            dealerOffice.OfficeID = Convert.ToInt32(HiddenID.Value);

            dealerOffice.SapLocationCode = txtSapLocationCode.Text.Trim();
            dealerOffice.OfficeCode = txtOfficeCode.Text.Trim();
            dealerOffice.OfficeName = txtOfficeName.Text.Trim();
            dealerOffice.Address1 = txtDealerOfficeAddress1.Text.Trim();
            dealerOffice.Address2 = txtDealerOfficeAddress2.Text.Trim();
            dealerOffice.Address3 = txtDealerOfficeAddress3.Text.Trim();
            dealerOffice.City = txtDealerOfficeCity.Text.Trim();
            dealerOffice.Pincode = txtDealerOfficePincode.Text.Trim();
            dealerOffice.DistrictID = Convert.ToInt32(ddlDealerOfficeDistrict.SelectedValue);
            dealerOffice.StateID = Convert.ToInt32(ddlDealerOfficeState.SelectedValue);
            dealerOffice.CountryID = 1;
            dealerOffice.GSTIN = txtDealerOfficeGSTIN.Text.Trim().ToUpper();
            dealerOffice.PAN = txtDealerOfficePAN.Text.Trim().ToUpper();
            dealerOffice.Mobile = txtDealerOfficeMobile.Text.Trim();
            dealerOffice.Email = txtDealerOfficeEmail.Text.Trim();
            dealerOffice.IsHeadOffice = cbIsHeadOffice.Checked;
            dealerOffice.IsActive = true;
            return dealerOffice;
        }
        private string ValidationAddBranchOffice()
        {
            string Message = "";
            txtSapLocationCode.BorderColor = Color.Silver;
            txtOfficeCode.BorderColor = Color.Silver;
            txtOfficeName.BorderColor = Color.Silver;
            txtDealerOfficeAddress1.BorderColor = Color.Silver;
            txtDealerOfficeCity.BorderColor = Color.Silver;
            txtDealerOfficePincode.BorderColor = Color.Silver;
            txtDealerOfficeGSTIN.BorderColor = Color.Silver;
            txtDealerOfficePAN.BorderColor = Color.Silver;
            txtDealerOfficeMobile.BorderColor = Color.Silver;

            Regex regex = new Regex(@"^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[a-zA-Z0-9]{3}$");

            long longCheck;

            if (string.IsNullOrEmpty(txtSapLocationCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter SAP Location Code.";
                txtSapLocationCode.BorderColor = Color.Red;
                goto msg;
            }
            if (string.IsNullOrEmpty(txtOfficeCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter Office Code.";
                txtOfficeCode.BorderColor = Color.Red;
                goto msg;
            }
            if (string.IsNullOrEmpty(txtOfficeName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter Office Name.";
                txtOfficeName.BorderColor = Color.Red;
                goto msg;
            }
            if (string.IsNullOrEmpty(txtDealerOfficeAddress1.Text.Trim()))
            {
                Message = Message + "<br/>Please enter Address1.";
                txtDealerOfficeAddress1.BorderColor = Color.Red;
                goto msg;
            }
            if (string.IsNullOrEmpty(txtDealerOfficeCity.Text.Trim()))
            {
                Message = Message + "<br/>Please enter City.";
                txtDealerOfficeCity.BorderColor = Color.Red;
                goto msg;
            }
            if (ddlDealerOfficeDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please enter District.";
                txtDealerOfficeCity.BorderColor = Color.Red;
                goto msg;
            }
            if (ddlDealerOfficeState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please enter State.";
                txtDealerOfficeCity.BorderColor = Color.Red;
                goto msg;
            }
            if (string.IsNullOrEmpty(txtDealerOfficePincode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter Pincode.";
                txtDealerOfficePincode.BorderColor = Color.Red;
                goto msg;
            }

            if (!string.IsNullOrEmpty(txtDealerOfficeGSTIN.Text.Trim()))
            {
                if ((!regex.Match(txtDealerOfficeGSTIN.Text.Trim()).Success) && (txtDealerOfficeGSTIN.Text.Trim() != "URD"))
                {
                    Message = " GST Number " + txtDealerOfficeGSTIN.Text.Trim() + " is not correct.";
                    txtDealerOfficeGSTIN.BorderColor = Color.Red;
                    goto msg;
                }
                if ((txtDealerOfficeGSTIN.Text.Trim() != "URD") && (!string.IsNullOrEmpty(txtDealerOfficeGSTIN.Text.Trim())))
                {
                    string gst = txtDealerOfficeGSTIN.Text.Trim().Remove(0, 2).Substring(0, 10);
                    if (txtDealerOfficePAN.Text.Trim().ToUpper() != gst.ToUpper())
                    {
                        Message = Message + "<br/>PAN and GSTIN are not matching.";
                        txtDealerOfficePAN.BorderColor = Color.Red;
                        goto msg;
                    }
                }
            }

            if (!string.IsNullOrEmpty(txtDealerOfficeMobile.Text.Trim()))
            {
                if (txtDealerOfficeMobile.Text.Trim().Length != 10)
                {
                    Message = Message + "<br/>Mobile Length should be 10 digit.";
                    txtDealerOfficeMobile.BorderColor = Color.Red;
                }
                else if (!long.TryParse(txtDealerOfficeMobile.Text.Trim(), out longCheck))
                {
                    Message = Message + "<br/>Alternative Mobile should be 10 digit";
                    txtDealerOfficeMobile.BorderColor = Color.Red;
                }
            }
            msg:
                return Message;
        }
        protected void lnkBtnItemAction_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            LinkButton lnkBtnEdit = (LinkButton)gvRow.FindControl("lnkBtnEdit");
            LinkButton lnkBtnDelete = (LinkButton)gvRow.FindControl("lnkBtnDelete");
            Label lblOfficeCodeID = (Label)gvRow.FindControl("lblOfficeCodeID");

            if (lbActions.ID == "lnkBtnEdit")
            {
                //lnkBtnEdit.Visible = false;
                //lnkBtnDelete.Visible = false;

                PopupDialogueAddBranchOffice.InnerText = "Edit Branch Office";

                DealerOfficeList = new BDMS_Dealer().GetDealerOffice(Dealer.DealerID, Convert.ToInt32(lblOfficeCodeID.Text), null);
                
                cbIsHeadOffice.Checked = DealerOfficeList[0].IsHeadOffice;
                txtSapLocationCode.Text = DealerOfficeList[0].SapLocationCode;
                txtOfficeCode.Text = DealerOfficeList[0].OfficeCode;
                txtOfficeName.Text = DealerOfficeList[0].OfficeName;
                txtDealerOfficeAddress1.Text = DealerOfficeList[0].Address1;
                txtDealerOfficeAddress2.Text = DealerOfficeList[0].Address2;
                txtDealerOfficeAddress3.Text = DealerOfficeList[0].Address3;
                txtDealerOfficeCity.Text = DealerOfficeList[0].City;
                ddlDealerOfficeDistrict.SelectedValue = DealerOfficeList[0].District.DistrictID.ToString();
                ddlDealerOfficeState.SelectedValue = DealerOfficeList[0].StateN.StateID.ToString();
                txtDealerOfficePincode.Text = DealerOfficeList[0].Pincode;
                txtDealerOfficeGSTIN.Text = DealerOfficeList[0].GSTIN;
                txtDealerOfficePAN.Text = DealerOfficeList[0].PAN;
                txtDealerOfficeMobile.Text = DealerOfficeList[0].Mobile;
                txtDealerOfficeEmail.Text = DealerOfficeList[0].Email;

                new DDLBind(ddlDealerOfficeState, new BDMS_Address().GetState(null, 1, null, null, null), "State", "StateID");
                ddlDealerOfficeState.SelectedValue = Convert.ToString(DealerOfficeList[0].StateN.StateID);

                new DDLBind(ddlDealerOfficeDistrict, new BDMS_Address().GetDistrict(1, null, Convert.ToInt32(ddlDealerOfficeState.SelectedValue), null, null, null), "District", "DistrictID");
                ddlDealerOfficeDistrict.SelectedValue = Convert.ToString(DealerOfficeList[0].District.DistrictID);
                HiddenID.Value = Convert.ToString(lblOfficeCodeID.Text);

                MPE_AddBranchOffice.Show();
            }
            else if (lbActions.ID == "lnkBtnDelete")
            {
                PDMS_DealerOffice_Insert dealerOffice = new PDMS_DealerOffice_Insert();

                DealerOfficeList = new BDMS_Dealer().GetDealerOffice(Dealer.DealerID, Convert.ToInt32(lblOfficeCodeID.Text), null);
                dealerOffice.OfficeID = DealerOfficeList[0].OfficeID;
                dealerOffice.DealerID = DealerOfficeList[0].DealerID; 
                dealerOffice.SapLocationCode = DealerOfficeList[0].SapLocationCode;
                dealerOffice.OfficeCode = DealerOfficeList[0].OfficeCode;
                dealerOffice.OfficeName = DealerOfficeList[0].OfficeName;
                dealerOffice.Address1 = DealerOfficeList[0].Address1;
                dealerOffice.Address2 = DealerOfficeList[0].Address2;
                dealerOffice.Address3 = DealerOfficeList[0].Address3;
                dealerOffice.City = DealerOfficeList[0].City;
                dealerOffice.DistrictID = DealerOfficeList[0].District.DistrictID;
                dealerOffice.StateID = DealerOfficeList[0].StateN.StateID;
                dealerOffice.CountryID = DealerOfficeList[0].Country == null ? 1 : DealerOfficeList[0].Country.CountryID;
                dealerOffice.Pincode = DealerOfficeList[0].Pincode;
                dealerOffice.GSTIN = DealerOfficeList[0].GSTIN;
                dealerOffice.PAN = DealerOfficeList[0].PAN;
                dealerOffice.Mobile = DealerOfficeList[0].Mobile;
                dealerOffice.Email = DealerOfficeList[0].Email;
                dealerOffice.IsHeadOffice = DealerOfficeList[0].IsHeadOffice; 
                dealerOffice.IsActive = false;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/DealerBranchOffice", dealerOffice));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                fillDealerOffice();
                //lnkBtnEdit.Visible = true;
                //lnkBtnDelete.Visible = true;
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDealerNotification();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}