using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewBusinessExcellence
{
    public partial class DealerBusinessExcellenceQuarterWise : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Reports_DealerBusinessExcellenceQuarterWise; } }
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
            if (!IsPostBack)
            {
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind(ddlRegionID, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID", true, "All");
            }
        }
        void FillYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 2023; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(i + 1 - 2023, new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlMonth.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(i, new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
            } 
        }
        void FillGrid()
        {
            int Year =  Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? RegionID = ddlRegionID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegionID.SelectedValue);
            int? Quarter = ddlQuarter.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlQuarter.SelectedValue); 
            PApiResult Result = new BDealerBusiness().GetDealerBusinessExcellenceQuarterWise(Year, Quarter, Month, DealerID, RegionID);
            gvDealerB.DataSource = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
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
    }
}