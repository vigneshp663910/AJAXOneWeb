using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class VisitCoverageReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_VisitCoverageReport; } }
        public DataSet CoverageReport
        {
            get
            {
                if (Session["VisitCoverageReport"] == null)
                {
                    Session["VisitCoverageReport"] = new DataSet();
                }
                return (DataSet)Session["VisitCoverageReport"];
            }
            set
            {
                Session["VisitCoverageReport"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Performance');</script>");

            lblMessage.Text = "";

            if (!IsPostBack)
            {
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
                ddlSCountry.SelectedValue = "1";
                new DDLBind(ddlRegion, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID");
                List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID");
                //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null); 
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillClodVisit();
        }

        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageIndex > 0)
            {
                gvLead.PageIndex = gvLead.PageIndex - 1;
                LeadBind(gvLead, lblRowCount);
            }
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageCount > gvLead.PageIndex)
            {
                gvLead.PageIndex = gvLead.PageIndex + 1;
                LeadBind(gvLead, lblRowCount);
            }
        }

        void LeadBind(GridView gv, Label lbl)
        {
            gv.DataSource = CoverageReport;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + CoverageReport.Tables[0].Rows.Count;
        }

        void FillClodVisit()
        {
            lblMessage.Text = string.Empty;
            PApiResult result = new PApiResult();
            try
            {
                int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
                int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
                int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                result = new BPreSale().GetVisitCoverageReport(CountryID, RegionID, StateID, DealerID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                CoverageReport = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(result.Data));
                gvLead.DataSource = CoverageReport.Tables[0];
                gvLead.DataBind();
                gvRegionCoverage.DataSource = CoverageReport.Tables[1];
                gvRegionCoverage.DataBind();

                if (CoverageReport.Tables[0].Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnLeadArrowLeft.Visible = false;
                    ibtnLeadArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnLeadArrowLeft.Visible = true;
                    ibtnLeadArrowRight.Visible = true;
                    lblRowCount.Text = (((gvLead.PageIndex) * gvLead.PageSize) + 1) + " - " + (((gvLead.PageIndex) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + CoverageReport.Tables[0].Rows.Count;
                }
            }
            catch (Exception e)
            {
                lblMessage.Text = e.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }
        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLead.PageIndex = e.NewPageIndex;
            FillClodVisit();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {  
             new BXcel().ExporttoExcelMultipleTable(CoverageReport, "Coverage Report");
        } 
    }
}