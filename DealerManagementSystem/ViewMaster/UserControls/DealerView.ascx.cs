 
using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                //ActionControlMange();
            }
        }

        public void filldealer(int DealerID)
        {
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

            fillDealerOffice();
            fillDealerEmployee();
            fillDealerNotification();
            ActionControlMange();
        }

        protected void lnkBtnActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
               if (lbActions.Text == "Add Dealer Notification")
                {
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

            DealerNotificationList = new BDMS_Dealer().GetDealerNotification(Dealer.DealerID);
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

    }
}