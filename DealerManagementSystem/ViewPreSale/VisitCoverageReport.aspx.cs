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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Visit Coverage Report');</script>");

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
            //if (gvLead.PageIndex > 0)
            //{
            //    gvLead.PageIndex = gvLead.PageIndex - 1;
            //    LeadBind(gvLead, lblRowCount);
            //}
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            //if (gvLead.PageCount > gvLead.PageIndex)
            //{
            //    gvLead.PageIndex = gvLead.PageIndex + 1;
            //    LeadBind(gvLead, lblRowCount);
            //}
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
                result = new BPreSale().GetVisitCoverageReport(CountryID, RegionID, StateID, DealerID, txtLeadDateFrom.Text.Trim(), txtLeadDateTo.Text.Trim(), txtVisitDateFrom.Text.Trim(), txtVisitDateTo.Text.Trim());
                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                CoverageReport = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(result.Data));

                gvAll.DataSource = CoverageReport.Tables[0];
                gvAll.DataBind();

                gvRegion.DataSource = CoverageReport.Tables[1];
                gvRegion.DataBind();

                gvState.DataSource = CoverageReport.Tables[2];
                gvState.DataBind();
                lblState.Text = "" + CoverageReport.Tables[2].Rows.Count;

                gvDealer.DataSource = CoverageReport.Tables[3];
                gvDealer.DataBind();
                lblDealer.Text = "" + CoverageReport.Tables[3].Rows.Count;

                gvEngg.DataSource = CoverageReport.Tables[4];
                gvEngg.DataBind();
                lblEngineer.Text = "" + CoverageReport.Tables[4].Rows.Count;

                gvDetails.DataSource = CoverageReport.Tables[5];
                gvDetails.DataBind();
                lblDetails.Text = "" + CoverageReport.Tables[5].Rows.Count;
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
            //gvLead.PageIndex = e.NewPageIndex;
            // FillClodVisit();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcelMultipleTable(CoverageReport, "Coverage Report");
        }

        protected void gvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvState.PageIndex = e.NewPageIndex;
            gvState.DataSource = CoverageReport.Tables[2];
            gvState.DataBind();
        }

        protected void gvDealer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealer.PageIndex = e.NewPageIndex;
            gvDealer.DataSource = CoverageReport.Tables[3];
            gvDealer.DataBind();
        }

        protected void gvEngg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEngg.PageIndex = e.NewPageIndex;
            gvEngg.DataSource = CoverageReport.Tables[4];
            gvEngg.DataBind();

        }
        protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetails.PageIndex = e.NewPageIndex;
            gvDetails.DataSource = CoverageReport.Tables[5];
            gvDetails.DataBind();

        }

        protected void btnExcelDetails_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CoverageReport.Tables[5], "Visit Coverage Report Details");
        }

        protected void btnExcelEngineer_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CoverageReport.Tables[4], "Visit Coverage Report Engineer");
        }

        protected void btnExcelDealer_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CoverageReport.Tables[3], "Visit Coverage Report Dealer");
        }

        protected void btnExcelState_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CoverageReport.Tables[2], "Visit Coverage Report State");
        }

        protected void btnExcelRegion_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CoverageReport.Tables[1], "Visit Coverage Report Region");
        }

        protected void btnExcelOverAll_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(CoverageReport.Tables[0], "Visit Coverage Report OverAll");
        }
    }
}