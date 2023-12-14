using Business;
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

namespace DealerManagementSystem.ViewPreSale.Reports
{
    public partial class DealerMissionPlanningReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_DealerMissionPlanningReport; } }
        private DataTable LeadReport
        {
            get
            {
                if (Session["DealerMissionPlanningReport"] == null)
                {
                    Session["DealerMissionPlanningReport"] = new DataTable();
                }
                return (DataTable)Session["DealerMissionPlanningReport"];
            }
            set
            {
                Session["DealerMissionPlanningReport"] = value;
            }
        }
        private DataTable LeadDetails
        {
            get
            {
                if (Session["DealerMissionPlanningReportLeadDetails"] == null)
                {
                    Session["DealerMissionPlanningReportLeadDetails"] = new DataTable();
                }
                return (DataTable)Session["DealerMissionPlanningReportLeadDetails"];
            }
            set
            {
                Session["DealerMissionPlanningReportLeadDetails"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Dealer Mission Planning Report');</script>");
            if (!IsPostBack)
            {
                LeadReport = null;
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            }
            VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
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
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillLead();
        }
        void FillLead()
        {
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);


            LeadReport = new BLead().GetDealerMissionPlanningForPreSales(Year, Month, DealerID, ProductTypeID);

           

            if (LeadReport.Rows.Count == 0)
            {
                lblRowCountV.Visible = false;
                ibtnVTArrowLeft.Visible = false;
                ibtnVTArrowRight.Visible = false;
                gvMissionPlanning.DataSource = LeadReport;
                gvMissionPlanning.DataBind();
            }
            else
            {
                lblRowCountV.Visible = true;
                ibtnVTArrowLeft.Visible = true;
                ibtnVTArrowRight.Visible = true;
                VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
                //lblRowCountV.Text = (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + 1) + " - " + (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + gvMissionPlanning.Rows.Count) + " of " + LeadReport.Rows.Count;
            } 
        }

        protected void gvVisitTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        { 
            gvMissionPlanning.PageIndex = e.NewPageIndex;
            VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
        } 
        protected void ibtnVTArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageIndex > 0)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex - 1;
                VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
            }
        }
        protected void ibtnVTArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageCount > gvMissionPlanning.PageIndex)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex + 1;
                VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
            }
        }

        void VTBind(GridView gv, Label lbl, DataTable VT)
        {
            gv.DataSource = VT;
            gv.DataBind();
            for (int i = 0; i <  gv.Rows.Count; i++)
            {
                Label lblMonthName = (Label)gv.Rows[i].FindControl("lblMonthName");
                Label lblMonth = (Label)gv.Rows[i].FindControl("lblMonth");

                lblMonthName.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(lblMonth.Text)).Substring(0, 3);
            }
                lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VT.Rows.Count;
            if (VT.Rows.Count > 0)
            {
                decimal LeadGenerationPlan = 0, LeadGenerationActual = 0, LeadGenerationActualP = 0,
                    LeadConversionPlan = 0, LeadConversionActual = 0, LeadConversionActualP = 0,
                    QuotationGeneratedPlan = 0, QuotationGeneratedActual = 0, QuotationGeneratedActualP = 0,
                    QuotationConversionPlan = 0, QuotationConversionActual = 0, QuotationConversionActualP = 0;
                foreach (DataRow dr in VT.Rows)
                {
                    LeadGenerationPlan = LeadGenerationPlan + Convert.ToDecimal(dr["New Lead Generation Plan"]);
                    LeadGenerationActual = LeadGenerationActual + Convert.ToDecimal(dr["New Lead Generation Actual"]);

                    LeadConversionPlan = LeadConversionPlan + Convert.ToDecimal(dr["Lead Conversion Plan"]);
                    LeadConversionActual = LeadConversionActual + Convert.ToDecimal(dr["Lead Conversion Actual"]);

                    QuotationGeneratedPlan = QuotationGeneratedPlan + Convert.ToDecimal(dr["Quotation Generated Plan"]);
                    QuotationGeneratedActual = QuotationGeneratedActual + Convert.ToDecimal(dr["Quotation Generated Actual"]);

                    QuotationConversionPlan = QuotationConversionPlan + Convert.ToDecimal(dr["Quotation Conversion Plan"]);
                    QuotationConversionActual = QuotationConversionActual + Convert.ToDecimal(dr["Quotation Conversion Actual"]);
                }

                LeadGenerationActualP = LeadGenerationActual * 100 / LeadGenerationPlan;
                LeadConversionActualP = LeadConversionActual * 100 / LeadConversionPlan;
                QuotationGeneratedActualP = QuotationGeneratedActual * 100 / QuotationGeneratedPlan;
                QuotationConversionActualP = QuotationConversionActual * 100 / QuotationConversionPlan;

                Label lblLeadGenerationPlanF = (Label)gv.FooterRow.FindControl("lblLeadGenerationPlanF");
                lblLeadGenerationPlanF.Text = LeadGenerationPlan.ToString("##.##");
                LinkButton lblLeadGenerationActualF = (LinkButton)gv.FooterRow.FindControl("lblLeadGenerationActualF");
                lblLeadGenerationActualF.Text = LeadGenerationActual.ToString("##.##");
                Label lblLeadGenerationActualPF = (Label)gv.FooterRow.FindControl("lblLeadGenerationActualPF");
                lblLeadGenerationActualPF.Text = LeadGenerationActualP.ToString("##.##");

                Label lblLeadConversionPlanF = (Label)gv.FooterRow.FindControl("lblLeadConversionPlanF");
                lblLeadConversionPlanF.Text = LeadConversionPlan.ToString("##.##");
                LinkButton lblLeadConversionActualF = (LinkButton)gv.FooterRow.FindControl("lblLeadConversionActualF");
                lblLeadConversionActualF.Text = LeadConversionActual.ToString("##.##");
                Label lblLeadConversionActualPF = (Label)gv.FooterRow.FindControl("lblLeadConversionActualPF");
                lblLeadConversionActualPF.Text = LeadConversionActualP.ToString("##.##");


                Label lblQuotationGeneratedPlanF = (Label)gv.FooterRow.FindControl("lblQuotationGeneratedPlanF");
                lblQuotationGeneratedPlanF.Text = QuotationGeneratedPlan.ToString("##.##");
                LinkButton lblQuotationGeneratedActualF = (LinkButton)gv.FooterRow.FindControl("lblQuotationGeneratedActualF");
                lblQuotationGeneratedActualF.Text = QuotationGeneratedActual.ToString("##.##");
                Label lblQuotationGeneratedActualPF = (Label)gv.FooterRow.FindControl("lblQuotationGeneratedActualPF");
                lblQuotationGeneratedActualPF.Text = QuotationGeneratedActualP.ToString("##.##");

                Label lblQuotationConversionPlanF = (Label)gv.FooterRow.FindControl("lblQuotationConversionPlanF");
                lblQuotationConversionPlanF.Text = QuotationConversionPlan.ToString("##.##");
                LinkButton lblQuotationConversionActualF = (LinkButton)gv.FooterRow.FindControl("lblQuotationConversionActualF");
                lblQuotationConversionActualF.Text = QuotationConversionActual.ToString("##.##");
                Label lblQuotationConversionActualPF = (Label)gv.FooterRow.FindControl("lblQuotationConversionActualPF");
                lblQuotationConversionActualPF.Text = QuotationConversionActualP.ToString("##.##");
            }
        }

        protected void OnDataBound(object sender, EventArgs e)
        {
            gvHeader(gvMissionPlanning); 
        }

        protected void gvHeader(GridView gv)
        {
            if (gv.Rows.Count != 0)
            {
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell = new TableHeaderCell();
                gvHeaderCellInfo(row, "Report Period", 5);
                gvHeaderCellInfo(row, "New Lead Generation", 3);
                gvHeaderCellInfo(row, "Lead Conversion", 3);
                gvHeaderCellInfo(row, "Quotation Generated", 3);
                gvHeaderCellInfo(row, "Quotation Conversion", 3); 
                row.BackColor = ColorTranslator.FromHtml("#fce4d6");
                gv.HeaderRow.Parent.Controls.AddAt(0, row);
            }
        }
        protected void gvHeaderCellInfo(GridViewRow row, string Name, int ColumnSpan)
        {
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = Name;
            cell.ColumnSpan = ColumnSpan;
            row.Controls.Add(cell);
        }
        protected void lblLinkButton_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
            int ReportDetails = 0;
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lblLeadGenerationActualF")
            {
                ReportDetails = 1;
            }
            else if (lbActions.ID == "lblLeadConversionActualF")
            {
                ReportDetails = 2;
            }
            else if (lbActions.ID == "lblQuotationGeneratedActualF")
            {
                ReportDetails = 3;
            }
            else if (lbActions.ID == "lblQuotationConversionActualF")
            {
                ReportDetails = 2;
            }
            else
            {
                Label lblYear = (Label)gvRow.FindControl("lblYear");
                Label lblMonth = (Label)gvRow.FindControl("lblMonth");
                Label lblDealerID = (Label)gvRow.FindControl("lblDealerID");
                Label lblProductTypeID = (Label)gvRow.FindControl("lblProductTypeID");
                Year = Convert.ToInt32(lblYear.Text);
                Month = Convert.ToInt32(lblMonth.Text);
                DealerID = Convert.ToInt32(lblDealerID.Text);
                ProductTypeID = Convert.ToInt32(lblProductTypeID.Text);

                if (lbActions.ID == "lblLeadGenerationActual")
                {
                    ReportDetails = 1;
                }
                else if (lbActions.ID == "lblLeadConversionActual")
                {
                    ReportDetails = 2;
                }
                else if (lbActions.ID == "lblQuotationGeneratedActual")
                {
                    ReportDetails = 3;
                }
                else if (lbActions.ID == "lblQuotationConversionActual")
                {
                    ReportDetails = 2;
                }

            }
            LeadDetails = new BLead().GetDealerMissionPlanningForPreSalesDetails(Year, Month, DealerID, ProductTypeID, ReportDetails);
            gvDetails.DataSource = LeadDetails;
            gvDetails.DataBind();
            MPE_LeadDetails.Show();
        }
        protected void btnExcelDetails_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    new BXcel().ExporttoExcel(LeadDetails, "Dealer Mission Planning Report Details");
                }
                catch
                {
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    new BXcel().DealerMissionPlanningReportForPreSales(LeadReport, "Dealer Mission Planning Report", "Dealer Mission Planning Report");
                }
                catch
                {
                }
                finally
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