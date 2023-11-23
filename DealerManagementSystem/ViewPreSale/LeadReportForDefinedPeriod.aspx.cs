using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class LeadReportForDefinedPeriod : BasePage
    {
        private DataTable LeadReport
        {
            get
            {
                if (Session["LeadReportForDefinedPeriod"] == null)
                {
                    Session["LeadReportForDefinedPeriod"] = new DataTable();
                }
                return (DataTable)Session["LeadReportForDefinedPeriod"];
            }
            set
            {
                Session["LeadReportForDefinedPeriod"] = value;
            }
        }
        private DataTable LeadDetails
        {
            get
            {
                if (Session["LeadReportForDefinedPeriodLeadDetails"] == null)
                {
                    Session["LeadReportForDefinedPeriodLeadDetails"] = new DataTable();
                }
                return (DataTable)Session["LeadReportForDefinedPeriodLeadDetails"];
            }
            set
            {
                Session["LeadReportForDefinedPeriodLeadDetails"] = value;
            }
        }
        private string DateFrom
        {
            get
            { 
                return (string)ViewState["DateFrom"];
            }
            set
            {
                ViewState["DateFrom"] = value;
            }
        }
        private string DateTo
        {
            get
            { 
                return (string)ViewState["DateTo"];
            }
            set
            {
                ViewState["DateTo"] = value;
            }
        }
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_LeadReportForDefinedPeriod; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Lead Report For Defined Period');</script>");

            if (!IsPostBack)
            {
                LeadReport = null;
                string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee, (short)DealerDesignation.SalesExecutive);
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlCountry, Country, "Country", "CountryID");
                ddlCountry.SelectedValue = "1";
                List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID");

                txtLeadDateFrom.Text = (Convert.ToDateTime("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year)).ToString("yyyy-MM-dd");
                txtLeadDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            LeadBind();
            //gvHeader();
        }
         

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillLead();
        }

      

        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageIndex > 0)
            {
                gvLead.PageIndex = gvLead.PageIndex - 1;
                LeadBind();
            }
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageCount > gvLead.PageIndex)
            {
                gvLead.PageIndex = gvLead.PageIndex + 1;
                LeadBind();
            }
        }

        void LeadBind()
        {
            gvLead.DataSource = LeadReport;
            gvLead.DataBind();
            lblRowCount.Text = (((gvLead.PageIndex) * gvLead.PageSize) + 1) + " - " + (((gvLead.PageIndex) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + LeadReport.Rows.Count;

            if (LeadReport.Rows.Count > 0)
            {
                decimal HitRatioF = 0, LostRatioF = 0, DropRatioF = 0, OpenHotF = 0, OpenWarmF = 0, OpenColdF = 0,
                    GeneratedHotF = 0, GeneratedWarmF = 0, GeneratedColdF = 0, WinHotF = 0, WinWarmF = 0, WinColdF = 0,
                    LostHotF = 0, LostWarmF = 0, LostColdF = 0, DropHotF = 0, DropWarmF = 0, DropColdF = 0,
                    ClosingHotF = 0, ClosingWarmF = 0, ClosingColdF = 0, Age30F = 0, Age60F = 0, Age90F = 0, Age180F = 0, AgeA180F = 0;
                foreach (DataRow dr in LeadReport.Rows)
                {  
                    OpenHotF = OpenHotF + Convert.ToDecimal(dr["Open Hot"]);
                    OpenWarmF = OpenWarmF + Convert.ToDecimal(dr["Open Warm"]);
                    OpenColdF = OpenColdF + Convert.ToDecimal(dr["Open Cold"]);

                    GeneratedHotF = GeneratedHotF + Convert.ToDecimal(dr["Generated Hot"]);
                    GeneratedWarmF = GeneratedWarmF + Convert.ToDecimal(dr["Generated Warm"]);
                    GeneratedColdF = GeneratedColdF + Convert.ToDecimal(dr["Generated Cold"]);

                    WinHotF = WinHotF + Convert.ToDecimal(dr["Win Hot"]);
                    WinWarmF = WinWarmF + Convert.ToDecimal(dr["Win Warm"]);
                    WinColdF = WinColdF + Convert.ToDecimal(dr["Win Cold"]);

                    LostHotF = LostHotF + Convert.ToDecimal(dr["Lost Hot"]);
                    LostWarmF = LostWarmF + Convert.ToDecimal(dr["Lost Warm"]);
                    LostColdF = LostColdF + Convert.ToDecimal(dr["Lost Cold"]);

                    DropHotF = DropHotF + Convert.ToDecimal(dr["Drop Hot"]);
                    DropWarmF = DropWarmF + Convert.ToDecimal(dr["Drop Warm"]);
                    DropColdF = DropColdF + Convert.ToDecimal(dr["Drop Cold"]);

                    ClosingHotF = ClosingHotF + Convert.ToDecimal(dr["Closing Hot"]);
                    ClosingWarmF = ClosingWarmF + Convert.ToDecimal(dr["Closing Warm"]);
                    ClosingColdF = ClosingColdF + Convert.ToDecimal(dr["Closing Cold"]);

                    Age30F = Age30F + Convert.ToDecimal(dr["Age 0 - 30"]);
                    Age60F = Age60F + Convert.ToDecimal(dr["Age 31 - 60"]);
                    Age90F = Age90F + Convert.ToDecimal(dr["Age 61 - 90"]);
                    Age180F = Age180F + Convert.ToDecimal(dr["Age 91 - 180"]);
                    AgeA180F = AgeA180F + Convert.ToDecimal(dr["Age > 180"]);

                }
                decimal Total = (OpenHotF + OpenWarmF + OpenColdF + GeneratedHotF + GeneratedWarmF + GeneratedColdF);
                if (Total > 0)
                {
                    HitRatioF = (WinHotF + WinWarmF + WinColdF) * 100 / Total;
                    LostRatioF = (LostHotF + LostWarmF + LostColdF) * 100 / Total;
                    DropRatioF = (DropHotF + DropWarmF + DropColdF) * 100 / Total;
                }
                Label lblHitRatioF = (Label)gvLead.FooterRow.FindControl("lblHitRatioF");
                lblHitRatioF.Text = HitRatioF.ToString("##.##");
                Label lblLostRatioF = (Label)gvLead.FooterRow.FindControl("lblLostRatioF");
                lblLostRatioF.Text = LostRatioF.ToString("##.##");
                Label lblDropRatioF = (Label)gvLead.FooterRow.FindControl("lblDropRatioF");
                lblDropRatioF.Text = DropRatioF.ToString("##.##");

                LinkButton lblOpenHotF = (LinkButton)gvLead.FooterRow.FindControl("lblOpenHotF");
                lblOpenHotF.Text = OpenHotF.ToString();
                LinkButton lblOpenWarmF = (LinkButton)gvLead.FooterRow.FindControl("lblOpenWarmF");
                lblOpenWarmF.Text = OpenWarmF.ToString();
                LinkButton lblOpenColdF = (LinkButton)gvLead.FooterRow.FindControl("lblOpenColdF");
                lblOpenColdF.Text = OpenColdF.ToString();

                LinkButton lblGeneratedHotF = (LinkButton)gvLead.FooterRow.FindControl("lblGeneratedHotF");
                lblGeneratedHotF.Text = GeneratedHotF.ToString();
                LinkButton lblGeneratedWarmF = (LinkButton)gvLead.FooterRow.FindControl("lblGeneratedWarmF");
                lblGeneratedWarmF.Text = GeneratedWarmF.ToString();
                LinkButton lblGeneratedColdF = (LinkButton)gvLead.FooterRow.FindControl("lblGeneratedColdF");
                lblGeneratedColdF.Text = GeneratedColdF.ToString();


                LinkButton lblWinHotF = (LinkButton)gvLead.FooterRow.FindControl("lblWinHotF");
                lblWinHotF.Text = WinHotF.ToString();
                LinkButton lblWinWarmF = (LinkButton)gvLead.FooterRow.FindControl("lblWinWarmF");
                lblWinWarmF.Text = WinWarmF.ToString();
                LinkButton lblWinColdF = (LinkButton)gvLead.FooterRow.FindControl("lblWinColdF");
                lblWinColdF.Text = WinColdF.ToString();

                LinkButton lblLostHotF = (LinkButton)gvLead.FooterRow.FindControl("lblLostHotF");
                lblLostHotF.Text = LostHotF.ToString();
                LinkButton lblLostWarmF = (LinkButton)gvLead.FooterRow.FindControl("lblLostWarmF");
                lblLostWarmF.Text = LostWarmF.ToString();
                LinkButton lblLostColdF = (LinkButton)gvLead.FooterRow.FindControl("lblLostColdF");
                lblLostColdF.Text = LostColdF.ToString();

                LinkButton lblDropHotF = (LinkButton)gvLead.FooterRow.FindControl("lblDropHotF");
                lblDropHotF.Text = DropHotF.ToString();
                LinkButton lblDropWarmF = (LinkButton)gvLead.FooterRow.FindControl("lblDropWarmF");
                lblDropWarmF.Text = DropWarmF.ToString();
                LinkButton lblDropColdF = (LinkButton)gvLead.FooterRow.FindControl("lblDropColdF");
                lblDropColdF.Text = DropColdF.ToString();

                LinkButton lblClosingHotF = (LinkButton)gvLead.FooterRow.FindControl("lblClosingHotF");
                lblClosingHotF.Text = ClosingHotF.ToString();
                LinkButton lblClosingWarmF = (LinkButton)gvLead.FooterRow.FindControl("lblClosingWarmF");
                lblClosingWarmF.Text = ClosingWarmF.ToString();
                LinkButton lblClosingColdF = (LinkButton)gvLead.FooterRow.FindControl("lblClosingColdF");
                lblClosingColdF.Text = ClosingColdF.ToString();

                LinkButton lblAge30F = (LinkButton)gvLead.FooterRow.FindControl("lblAge30F");
                lblAge30F.Text = Age30F.ToString();
                LinkButton lblAge60F = (LinkButton)gvLead.FooterRow.FindControl("lblAge60F");
                lblAge60F.Text = Age60F.ToString();
                LinkButton lblAge90F = (LinkButton)gvLead.FooterRow.FindControl("lblAge90F");
                lblAge90F.Text = Age90F.ToString();
                LinkButton lblAge180F = (LinkButton)gvLead.FooterRow.FindControl("lblAge180F");
                lblAge180F.Text = Age180F.ToString();
                LinkButton lblAgeA180F = (LinkButton)gvLead.FooterRow.FindControl("lblAgeA180F");
                lblAgeA180F.Text = AgeA180F.ToString();
            }
        }

        void FillLead()
        {
            PLeadSearch S = new PLeadSearch(); 
            S.CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            S.StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue); 
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            S.SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);

            DateFrom = ((DateTime)S.LeadDateFrom).ToString("dd-MM-yyyy");
            DateTo = ((DateTime)S.LeadDateTo).ToString("dd-MM-yyyy");
            LeadReport = new BLead().GetLeadReportForDefinedPeriod(S);  
            if (LeadReport.Rows.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
                gvLead.DataSource = LeadReport;
                gvLead.DataBind();
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                LeadBind();
            }

        }
        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        }
         
        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLead.PageIndex = e.NewPageIndex;
            LeadBind();
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null,  (short)DealerDesignation.SalesExecutive);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }

        protected void lblLinkButton_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblDealerID = (Label)gvRow.FindControl("lblDealerID");
            Label lblEnggUserID = (Label)gvRow.FindControl("lblEnggUserID");
            // LinkButton lbActions = (LinkButton)gvRow.FindControl("lblLeadID");
            LinkButton lbActions = ((LinkButton)sender);
            PLeadSearch S = new PLeadSearch();
            
            if (lbActions.ID == "lblOpenHot")
            {
                S.LeadID = 1;
                S.QualificationID = 3;
            } 
            else if (lbActions.ID == "lblOpenWarm")
            {
                S.LeadID = 1;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblOpenCold")
            {
                S.LeadID = 1;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblGeneratedHot")
            {
                S.LeadID = 2;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblGeneratedWarm")
            {
                S.LeadID = 2;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblGeneratedCold")
            {
                S.LeadID = 2;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblWinHot")
            {
                S.LeadID = 3;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblWinWarm")
            {
                S.LeadID = 3;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblWinCold")
            {
                S.LeadID = 3;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblLostHot")
            {
                S.LeadID = 4;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblLostWarm")
            {
                S.LeadID = 4;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblLostCold")
            {
                S.LeadID = 4;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblDropHot")
            {
                S.LeadID = 5;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblDropWarm")
            {
                S.LeadID = 5;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblDropCold")
            {
                S.LeadID = 5;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblClosingHot")
            {
                S.LeadID = 6;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblClosingWarm")
            {
                S.LeadID = 6;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblClosingCold")
            {
                S.LeadID = 6;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblAge30")
            {
                S.LeadID = 7;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblAge60")
            {
                S.LeadID = 7;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblAge90")
            {
                S.LeadID = 7;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblAge180")
            {
                S.LeadID = 7;
                S.QualificationID = 4;
            }
            else if (lbActions.ID == "lblAgeA180")
            {
                S.LeadID = 7;
                S.QualificationID = 5;
            }

            S.CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            S.StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); //Convert.ToInt32(lblDealerID.Text);
            S.SalesEngineerID = Convert.ToInt32(lblEnggUserID.Text);

            LeadDetails = new BLead().GetLeadReportForDefinedPeriodDetails(S);

            gvLeadDetails.DataSource = LeadDetails;
            gvLeadDetails.DataBind(); 
            MPE_LeadDetails.Show();
        }
        protected void lblLinkButtonF_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent; 
            LinkButton lbActions = ((LinkButton)sender);
            PLeadSearch S = new PLeadSearch();

            if (lbActions.ID == "lblOpenHotF")
            {
                S.LeadID = 1;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblOpenWarmF")
            {
                S.LeadID = 1;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblOpenColdF")
            {
                S.LeadID = 1;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblGeneratedHotF")
            {
                S.LeadID = 2;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblGeneratedWarmF")
            {
                S.LeadID = 2;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblGeneratedColdF")
            {
                S.LeadID = 2;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblWinHotF")
            {
                S.LeadID = 3;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblWinWarmF")
            {
                S.LeadID = 3;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblWinColdF")
            {
                S.LeadID = 3;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblLostHotF")
            {
                S.LeadID = 4;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblLostWarmF")
            {
                S.LeadID = 4;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblLostColdF")
            {
                S.LeadID = 4;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblDropHotF")
            {
                S.LeadID = 5;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblDropWarmF")
            {
                S.LeadID = 5;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblDropColdF")
            {
                S.LeadID = 5;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblClosingHotF")
            {
                S.LeadID = 6;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblClosingWarmF")
            {
                S.LeadID = 6;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblClosingColdF")
            {
                S.LeadID = 6;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblAge30F")
            {
                S.LeadID = 7;
                S.QualificationID = 1;
            }
            else if (lbActions.ID == "lblAge60F")
            {
                S.LeadID = 7;
                S.QualificationID = 2;
            }
            else if (lbActions.ID == "lblAge90F")
            {
                S.LeadID = 7;
                S.QualificationID = 3;
            }
            else if (lbActions.ID == "lblAge180F")
            {
                S.LeadID = 7;
                S.QualificationID = 4;
            }
            else if (lbActions.ID == "lblAgeA180F")
            {
                S.LeadID = 7;
                S.QualificationID = 5;
            }

            S.CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            S.StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim()); 
           // S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            // S.SalesEngineerID = null;
            DataTable dt = new DataTable();
            LeadDetails = new DataTable();
           foreach (DataRow r in LeadReport.Rows)
            {
                S.DealerID = Convert.ToInt32(r["DealerID"]);
                S.SalesEngineerID = Convert.ToInt32(r["EnggUserID"]);
                dt = new BLead().GetLeadReportForDefinedPeriodDetails(S);
                LeadDetails.Merge(dt);
            }
           // LeadDetails = new BLead().GetLeadReportForDefinedPeriodDetails(S);

            gvLeadDetails.DataSource = LeadDetails;
            gvLeadDetails.DataBind();
            MPE_LeadDetails.Show();
        }

        protected void OnDataBound(object sender, EventArgs e)
        {
            gvHeader();
        }


        protected void gvHeader()
        {
            
                if (gvLead.Rows.Count != 0)
                {
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableHeaderCell cell = new TableHeaderCell();
                    gvHeaderCellInfo(row, "Report Period", 4);
                    gvHeaderCellInfo(row, "Ratio", 3);
                    gvHeaderCellInfo(row, "Opening Lead", 3);
                    gvHeaderCellInfo(row, "Lead Generated", 3);
                    gvHeaderCellInfo(row, "Win to Ajax", 3);
                    gvHeaderCellInfo(row, "Lead Lost", 3);
                    gvHeaderCellInfo(row, "Lead Drop", 3);
                    gvHeaderCellInfo(row, "Closing Lead", 3);
                    gvHeaderCellInfo(row, "Ageing - Closing Lead", 5);
                    row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
                    gvLead.HeaderRow.Parent.Controls.AddAt(0, row);
                }
             
        }
        protected void gvHeaderCellInfo(GridViewRow row,string Name, int ColumnSpan)
        { 
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = Name;
            cell.ColumnSpan = ColumnSpan;
            row.Controls.Add(cell); 
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                
                try
                {
                    new BXcel().ExporttoExcelForLeadDefinedPeriod(LeadReport, "Lead Report For Defined Period", "Lead Report for defined period -" + DateFrom + " to " + DateTo);
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

        protected void btnExcelDetails_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    new BXcel().ExporttoExcel(LeadDetails, "Lead Report For Defined Period Details");
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