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
            S.DealerID = Convert.ToInt32(lblDealerID.Text);
            S.SalesEngineerID = Convert.ToInt32(lblEnggUserID.Text);
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

            
            S.CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            S.StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            //S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            //S.SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            LeadDetails = new BLead().GetLeadReportForDefinedPeriodDetails(S);

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
                    new BXcel().ExporttoExcel(LeadReport, "Lead Report For Defined Period");
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