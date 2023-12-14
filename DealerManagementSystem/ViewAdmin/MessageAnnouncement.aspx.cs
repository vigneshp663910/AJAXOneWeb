using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class MessageAnnouncement : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_MessageAnnouncement; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public int? DealerID, DealerDepartmentID, DealerDesignationID, DealerEmployeeID;
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        public List<PMessageAnnouncementHeader> Message
        {
            get
            {
                if (ViewState["PMessageAnnouncement"] == null)
                {
                    ViewState["PMessageAnnouncement"] = new List<PMessageAnnouncementHeader>();
                }
                return (List<PMessageAnnouncementHeader>)ViewState["PMessageAnnouncement"];
            }
            set
            {
                ViewState["PMessageAnnouncement"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Message Announcement');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
                DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
                List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
                new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
                DealerEmployeeID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.MailNotificationCreation).Count() == 0)
                { 
                    DivMessageHeader.Visible = false; 
                }
                Fill();
            }
        }
        void Fill()
        {
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            DealerEmployeeID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);

            if(ChkGetAllMessage.Checked)
            { 
                Message = new BMessageAnnouncement().GetMessageAnnouncementHeader(null, DealerID, DealerDepartmentID, DealerDesignationID, DealerEmployeeID, null, null); 
            }
            else
            {
                Message = new BMessageAnnouncement().GetMessageAnnouncementHeader(null, null, null, null, PSession.User.UserID, false, DateTime.Now.ToString("yyyy-MM-dd"));
            }
            gvMessageAnnouncement.DataSource = Message;
            gvMessageAnnouncement.DataBind();

            if (Message.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((gvMessageAnnouncement.PageIndex) * gvMessageAnnouncement.PageSize) + 1) + " - " + (((gvMessageAnnouncement.PageIndex) * gvMessageAnnouncement.PageSize) + gvMessageAnnouncement.Rows.Count) + " of " + Message.Count;
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerEmployeeID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerDepartmentID = (ddlDepartment.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, DealerDepartmentID, null, null);
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerEmployeeID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                Fill();
            }
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divMessageAnnouncementCreate.Visible = true;
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            //UC_MessageAnnouncementCreate.FillMaster();
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divMessageAnnouncementCreate.Visible = false;
            divMessageAnnouncementView.Visible = false;
        }

        protected void btnViewMessage_Click(object sender, EventArgs e)
        {
            divMessageAnnouncementView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblMessageAnnouncementId = (Label)gvRow.FindControl("lblMessageAnnouncementId");

            UC_MessageAnnouncementView.fillViewMessage(Convert.ToInt64(lblMessageAnnouncementId.Text));
        }

        protected void btnViewBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divMessageAnnouncementCreate.Visible = false;
            divMessageAnnouncementView.Visible = false;
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                Fill();
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerDesignationID = (ddlDesignation.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            List<PUser> user = new BUser().GetUsers(null, null, null, null, DealerID, true, null, DealerDepartmentID, DealerDesignationID);
            new DDLBind(ddlDealerEmployee, user, "ContactName", "UserID");
            DealerEmployeeID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Fill();
        }
    }
}