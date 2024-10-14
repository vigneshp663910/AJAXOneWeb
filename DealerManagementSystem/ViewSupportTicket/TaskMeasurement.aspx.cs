using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class TaskMeasurement : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_TaskMeasurement; } }
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
        public List<PTicketHeader> GetTicketSupportMeasurement
        {
            get
            {
                if (ViewState["GetTicketSupportMeasurement"] == null)
                {
                    ViewState["GetTicketSupportMeasurement"] = new List<PTicketHeader>();
                }
                return (List<PTicketHeader>)ViewState["GetTicketSupportMeasurement"];
            }
            set
            {
                ViewState["GetTicketSupportMeasurement"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        int? HeaderId;
        int? CategoryID;
        int? SubCategoryID;
        int? CreatedBy;
        int? AssignedTo;
        int? DepartmentID;
        string TicketStatus;
        int? Rating;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Measurement');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    new FillDropDownt().Category(ddlCategory, null, null);
                    ddlCategory_SelectedIndexChanged(null, null);
                    FillStatus();
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlCreatedBy);
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlAssignedTo);
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    //FillTickets();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void FillStatus()
        {
            lbStatus.DataTextField = "Status";
            lbStatus.DataValueField = "StatusID";
            lbStatus.DataSource = JsonConvert.DeserializeObject<List<PStatus>>(JsonConvert.SerializeObject(new BTickets().getTicketStatus(null, null).Data));
            lbStatus.DataBind();
            lbStatus.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            new FillDropDownt().SubCategory(ddlSubcategory, null, null, CategoryID);
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlAssignedTo, DealerUser, "ContactName", "UserID");
            new DDLBind(ddlCreatedBy, DealerUser, "ContactName", "UserID");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageIndex = 1;
                FillTickets();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        void fillter()
        {
            HeaderId = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (int?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
            CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            SubCategoryID = null;
            if (ddlSubcategory.Items.Count > 0)
            {
                SubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            }
            CreatedBy = ddlCreatedBy.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
            TicketStatus = "";
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    TicketStatus = TicketStatus + "," + li.Text;
                }
            }
            TicketStatus = (string.IsNullOrEmpty(TicketStatus) ? null : TicketStatus);
            DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            AssignedTo = ddlAssignedTo.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAssignedTo.SelectedValue);            
            Rating = ddlRating.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRating.SelectedValue);
        }
        void FillTickets()
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                fillter();
                
                PApiResult Result = new BTickets().GetTicketSupportMeasurement(HeaderId, CategoryID, SubCategoryID, CreatedBy, AssignedTo, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, TicketStatus, Rating, PageIndex, gvTickets.PageSize);
                GetTicketSupportMeasurement = JsonConvert.DeserializeObject<List<PTicketHeader>>(JsonConvert.SerializeObject(Result.Data));

                gvTickets.DataSource = GetTicketSupportMeasurement;
                gvTickets.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvTickets.PageSize - 1) / gvTickets.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvTickets.PageSize) + gvTickets.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillTickets();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillTickets();
            }
        }
    }
}