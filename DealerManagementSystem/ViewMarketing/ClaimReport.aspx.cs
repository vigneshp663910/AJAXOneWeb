using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ClaimReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMarketing_MarketingClaimReport; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Marketing » Claim Report');</script>");

            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                txtFromDate.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");  
            }
        }
 

        protected void Search_Click(object sender, EventArgs e)
        {
            PageCount = 0;
            PageIndex = 1;
            FillClaim();
           
             
        } 
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillClaim();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillClaim();
            }
        }

        void FillClaim()
        {
            PApiResult Result = GetMarketingClaimReport(0);

            gvData.DataSource = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            gvData.DataBind();
            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvData.PageSize - 1) / gvData.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvData.PageSize) + 1) + " - " + (((PageIndex - 1) * gvData.PageSize) + gvData.Rows.Count) + " of " + Result.RowCount;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            PApiResult Result = GetMarketingClaimReport(1);
            new BXcel().ExporttoExcel(JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data)), "Stock Ageing Report");
        }

        PApiResult GetMarketingClaimReport(int Excel)
        {
            int? DealerID = null;
            string ActivityType = ddlActivityType.SelectedValue;
            int? StatusID = null;
            string FromDate = txtFromDate.Text.Trim();
            string ToDate = txtToDate.Text.Trim(); 

            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue); 
            }  
            return new BDMS_Activity().GetMarketingClaimReport(DealerID,  ActivityType, StatusID, FromDate, ToDate, Excel, PageIndex, gvData.PageSize); 
        }
    }
}