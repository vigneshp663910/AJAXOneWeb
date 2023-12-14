using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.Reports
{
    public partial class LeadExpectedDateofSaleAgeingReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Reports_LeadExpectedDateofSaleAgeingReport; } }
        private DataTable EnquiryReport
        {
            get
            {
                if (Session["LeadExpectedDateofSaleAgeingReport"] == null)
                {
                    Session["LeadExpectedDateofSaleAgeingReport"] = new DataTable();
                }
                return (DataTable)Session["LeadExpectedDateofSaleAgeingReport"];
            }
            set
            {
                Session["LeadExpectedDateofSaleAgeingReport"] = value;
            }
        }
        private DataTable EnquiryDetails
        {
            get
            {
                if (Session["LeadExpectedDateofSaleAgeingReportDetails"] == null)
                {
                    Session["LeadExpectedDateofSaleAgeingReportDetails"] = new DataTable();
                }
                return (DataTable)Session["LeadExpectedDateofSaleAgeingReportDetails"];
            }
            set
            {
                Session["LeadExpectedDateofSaleAgeingReportDetails"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Dealer Mission Planning Report');</script>");
            if (!IsPostBack)
            {
                EnquiryReport = null;
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind(ddlRegion, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID");
            }
            VTBind(gvEnquiry, lblRowCountV, EnquiryReport);
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillEnquiry();
        }
        void FillEnquiry()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
            EnquiryReport = new BEnquiry().GetEnquiryUnattendedAgeing(DealerID, RegionID);
            if (EnquiryReport.Rows.Count == 0)
            {
                lblRowCountV.Visible = false;
                ibtnLeft.Visible = false;
                ibtnRight.Visible = false;
                gvEnquiry.DataSource = EnquiryReport;
                gvEnquiry.DataBind();
            }
            else
            {
                lblRowCountV.Visible = true;
                ibtnLeft.Visible = true;
                ibtnRight.Visible = true;
                VTBind(gvEnquiry, lblRowCountV, EnquiryReport);
                //lblRowCountV.Text = (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + 1) + " - " + (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + gvMissionPlanning.Rows.Count) + " of " + LeadReport.Rows.Count;
            }
        }


        protected void ibtnLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageIndex > 0)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex - 1;
                VTBind(gvEnquiry, lblRowCountV, EnquiryReport);
            }
        }
        protected void ibtnRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageCount > gvEnquiry.PageIndex)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex + 1;
                VTBind(gvEnquiry, lblRowCountV, EnquiryReport);
            }
        }

        void VTBind(GridView gv, Label lbl, DataTable VT)
        {
            gv.DataSource = VT;
            gv.DataBind();

            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VT.Rows.Count;
            if (VT.Rows.Count > 0)
            {
                decimal Days0_3 = 0, Days4_6 = 0, DaysGr6 = 0, EnquiryTotal = 0;
                foreach (DataRow dr in VT.Rows)
                {
                    Days0_3 = Days0_3 + Convert.ToDecimal(dr["Days 0 to 3"]);
                    Days4_6 = Days4_6 + Convert.ToDecimal(dr["Days 4 to 6"]);
                    DaysGr6 = DaysGr6 + Convert.ToDecimal(dr["Days > 6"]);
                    EnquiryTotal = EnquiryTotal + Convert.ToDecimal(dr["Enquiry Total"]);
                }


                LinkButton lblDays0To3F = (LinkButton)gv.FooterRow.FindControl("lblDays0To3F");
                lblDays0To3F.Text = Days0_3.ToString("##.##");
                LinkButton lblDays4To6 = (LinkButton)gv.FooterRow.FindControl("lblDays4To6F");
                lblDays4To6.Text = Days4_6.ToString("##.##");
                LinkButton lblDaysGr6F = (LinkButton)gv.FooterRow.FindControl("lblDaysGr6F");
                lblDaysGr6F.Text = DaysGr6.ToString("##.##");
                LinkButton lblEnquiryTotalF = (LinkButton)gv.FooterRow.FindControl("lblEnquiryTotalF");
                lblEnquiryTotalF.Text = EnquiryTotal.ToString("##.##");
            }
        }

        protected void OnDataBound(object sender, EventArgs e)
        {
            gvHeader(gvEnquiry);
        }

        protected void gvHeader(GridView gv)
        {
            if (gv.Rows.Count != 0)
            {
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableHeaderCell cell = new TableHeaderCell();
                gvHeaderCellInfo(row, "", 4);
                gvHeaderCellInfo(row, "Enquiry Ageing", 4);
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
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
            int ReportDetails = 0;
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lblDays0To3F")
            {
                ReportDetails = 1;
            }
            else if (lbActions.ID == "lblDays4To6F")
            {
                ReportDetails = 2;
            }
            else if (lbActions.ID == "lblDaysGr6F")
            {
                ReportDetails = 3;
            }
            else if (lbActions.ID == "lblEnquiryTotalF")
            {
                ReportDetails = 4;
            }
            else
            {
                Label lblDealerID = (Label)gvRow.FindControl("lblDealerID");
                Label lblRegionID = (Label)gvRow.FindControl("lblRegionID");

                DealerID = Convert.ToInt32(lblDealerID.Text);
                RegionID = Convert.ToInt32(lblRegionID.Text);

                if (lbActions.ID == "lblDays0To3")
                {
                    ReportDetails = 1;
                }
                else if (lbActions.ID == "lblDays4To6")
                {
                    ReportDetails = 2;
                }
                else if (lbActions.ID == "lblDaysGr6")
                {
                    ReportDetails = 3;
                }
                else if (lbActions.ID == "lblEnquiryTotal")
                {
                    ReportDetails = 4;
                }

            }
            EnquiryDetails = new BEnquiry().GetEnquiryUnattendedAgeingDetails(DealerID, RegionID, ReportDetails);
            gvDetails.DataSource = EnquiryDetails;
            gvDetails.DataBind();
            MPE_LeadDetails.Show();
        }
        protected void btnExcelDetails_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    new BXcel().ExporttoExcel(EnquiryDetails, "Lead Expected Date of Sale Ageing Details");
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
                    new BXcel().DealerMissionPlanningReportForPreSales(EnquiryReport, "Lead Expected Date of Sale Ageing", "Lead Expected Dateof Sale Ageing");
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