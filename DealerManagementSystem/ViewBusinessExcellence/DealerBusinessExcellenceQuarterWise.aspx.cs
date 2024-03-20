using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
       
        public DataTable QuarterWise
        {
            get
            {
                if (ViewState["DealerBusinessExcellenceQuarterWise"] == null)
                {
                    ViewState["DealerBusinessExcellenceQuarterWise"] = new DataTable();
                }
                return (DataTable)ViewState["DealerBusinessExcellenceQuarterWise"];
            }
            set
            {
                ViewState["DealerBusinessExcellenceQuarterWise"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Business Excellence » Report » Quarter wise ');</script>");
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
            QuarterWise = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            gvDealerB.DataSource = QuarterWise;
            gvDealerB.DataBind(); 
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        } 

        protected void gvDealerB_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerB.PageIndex = e.NewPageIndex;
            gvDealerB.DataSource = QuarterWise;
            gvDealerB.DataBind();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    new BXcel().ExporttoExcel(QuarterWise,"BusinessExcellenceQuarterWise");
                }
                catch
                {
                } 
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}