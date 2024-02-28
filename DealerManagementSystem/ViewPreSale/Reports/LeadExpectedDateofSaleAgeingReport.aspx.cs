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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Lead Next Follow Up Ageing Report');</script>");
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
            FillLead();
        }
        void FillLead()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
            EnquiryReport = new BLead().GetLeadExpectedDateofSaleAgeingReport(DealerID, RegionID);
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
                decimal DaysLeN60 = 0, DaysN31_60 = 0, DaysN1_30 = 0, Days0 = 0, Days1_30 = 0, Days31_60 = 0, DaysGr60 = 0, Total = 0;
                foreach (DataRow dr in VT.Rows)
                {
                    DaysLeN60 = DaysLeN60 + Convert.ToDecimal(dr["Days < -60"]);
                    DaysN31_60 = DaysN31_60 + Convert.ToDecimal(dr["Days -31 To -60"]);
                    DaysN1_30 = DaysN1_30 + Convert.ToDecimal(dr["Days -1 To -30"]);
                    Days0 = Days0 + Convert.ToDecimal(dr["Days 0"]);

                    Days1_30 = Days1_30 + Convert.ToDecimal(dr["Days 1 To 30"]);
                    Days31_60 = Days31_60 + Convert.ToDecimal(dr["Days 31 To 60"]);
                    DaysGr60 = DaysGr60 + Convert.ToDecimal(dr["Days > 60"]);
                    Total = Total + Convert.ToDecimal(dr["Total"]);
                }


                LinkButton lblDaysLeN60F = (LinkButton)gv.FooterRow.FindControl("lblDaysLeN60F");
                lblDaysLeN60F.Text = DaysLeN60.ToString("##.##");
                LinkButton lblDaysN31To60F = (LinkButton)gv.FooterRow.FindControl("lblDaysN31To60F");
                lblDaysN31To60F.Text = DaysN31_60.ToString("##.##");
                LinkButton lblDaysN1To30F = (LinkButton)gv.FooterRow.FindControl("lblDaysN1To30F");
                lblDaysN1To30F.Text = DaysN1_30.ToString("##.##");
                LinkButton lblDays0F = (LinkButton)gv.FooterRow.FindControl("lblDays0F");
                lblDays0F.Text = Days0.ToString("##.##");

                LinkButton lblDays1To30F = (LinkButton)gv.FooterRow.FindControl("lblDays1To30F");
                lblDays1To30F.Text = Days1_30.ToString("##.##");
                LinkButton lblDays31To60F = (LinkButton)gv.FooterRow.FindControl("lblDays31To60F");
                lblDays31To60F.Text = Days31_60.ToString("##.##");
                LinkButton lblDaysGr60F = (LinkButton)gv.FooterRow.FindControl("lblDaysGr60F");
                lblDaysGr60F.Text = DaysGr60.ToString("##.##");
                LinkButton lblGrandTotalF = (LinkButton)gv.FooterRow.FindControl("lblGrandTotalF");
                lblGrandTotalF.Text = Total.ToString("##.##");
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
                gvHeaderCellInfo(row, "Lead Expected Date of Sale",8);
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
            if (lbActions.ID == "lblDaysLeN60F")
            {
                ReportDetails = 1;
            }
            else if (lbActions.ID == "lblDaysN31To60F")
            {
                ReportDetails = 2;
            }
            else if (lbActions.ID == "lblDaysN1To30F")
            {
                ReportDetails = 3;
            }
            else if (lbActions.ID == "lblDays0F")
            {
                ReportDetails = 4;
            }
            else if (lbActions.ID == "lblDays1To30F")
            {
                ReportDetails = 5;
            }
            else if (lbActions.ID == "lblDays31To60F")
            {
                ReportDetails = 6;
            }
            else if (lbActions.ID == "lblDaysGr60F")
            {
                ReportDetails =7 ;
            }
            else if (lbActions.ID == "lblGrandTotalF")
            {
                ReportDetails =8 ;
            } 
            else
            {
                Label lblDealerID = (Label)gvRow.FindControl("lblDealerID");
                Label lblRegionID = (Label)gvRow.FindControl("lblRegionID");
                DealerID = Convert.ToInt32(lblDealerID.Text);
                RegionID = Convert.ToInt32(lblRegionID.Text);
                if (lbActions.ID == "lblDaysLeN60")
                {
                    ReportDetails = 1;
                }
                else if (lbActions.ID == "lblDaysN31To60")
                {
                    ReportDetails = 2;
                }
                else if (lbActions.ID == "lblDaysN1To30")
                {
                    ReportDetails = 3;
                }
                else if (lbActions.ID == "lblDays0")
                {
                    ReportDetails = 4;
                }
                else if (lbActions.ID == "lblDays1To30")
                {
                    ReportDetails = 5;
                }
                else if (lbActions.ID == "lblDays31To60")
                {
                    ReportDetails = 6;
                }
                else if (lbActions.ID == "lblDaysGr60")
                {
                    ReportDetails = 7;
                }
                else if (lbActions.ID == "lblGrandTotal")
                {
                    ReportDetails =8 ;
                } 

            }
            EnquiryDetails = new BLead().GetLeadExpectedDateofSaleAgeingDetails(DealerID, RegionID, ReportDetails);
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
                    new BXcel().ExporttoExcelForLeadExpectedDateofSaleAgeingReport(EnquiryReport, "Lead Expected Date of Sale Ageing", "Lead Expected Dateof Sale Ageing");
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