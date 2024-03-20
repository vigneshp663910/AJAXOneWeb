using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewBusinessExcellence
{
    public partial class DealerBusinessExcellence : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Reports_DealerBusinessExcellence; } }
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Business Excellence » Business Excellence');</script>");

            if (!IsPostBack)
            {
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind(ddlRegionID, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID", true, "All");
                new DDLBind(ddlStatus, new BDMS_Master().GetAjaxOneStatus(1), "Status", "StatusID");
            }
        }
        void FillYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 2023; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(i + 1 - 2023, new ListItem(i.ToString(), i.ToString()));
            }

            ddlMonth.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(i, new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
            }
        }
        void FillGrid()
        {
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? RegionID = ddlRegionID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegionID.SelectedValue);
            int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
            PApiResult Result = new BDealerBusiness().GetDealerBusinessExcellence(Year, Month, DealerID, RegionID, StatusID, PageIndex, gvDealerB.PageSize);
            gvDealerB.DataSource = JsonConvert.DeserializeObject<List<PDealerBusinessExcellenceHeader>>(JsonConvert.SerializeObject(Result.Data)); ;
            gvDealerB.DataBind();
            if (Result.RowCount == 0)
            {
                PageCount = 0;
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvDealerB.PageSize - 1) / gvDealerB.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvDealerB.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerB.PageSize) + gvDealerB.Rows.Count) + " of " + Result.RowCount;
                // lblRowCount.Text = (((gvDealerB.PageIndex) * gvDealerB.PageSize) + 1) + " - " + (((gvDealerB.PageIndex) * gvDealerB.PageSize) + gvDealerB.Rows.Count) + " of " + LeadReport.Count;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillGrid();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillGrid();
            }
        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divDetailsView.Visible = true;
            lblMessage.Text = "";
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblDealerBusinessExcellenceID = (Label)gvRow.FindControl("lblDealerBusinessExcellenceID"); 
            UC_ViewDealerBusinessExcellence.fill(Convert.ToInt64(lblDealerBusinessExcellenceID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }
         
    }
}