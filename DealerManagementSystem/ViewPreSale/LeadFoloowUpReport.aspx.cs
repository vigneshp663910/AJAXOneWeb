using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class LeadFoloowUpReport : System.Web.UI.Page
    {
        public DataTable LeadFoloowUp
        {
            get
            {
                if (Session["LeadFoloowUpReport"] == null)
                {
                    Session["LeadFoloowUpReport"] = new DataTable();
                }
                return (DataTable)Session["LeadFoloowUpReport"];
            }
            set
            {
                Session["LeadFoloowUpReport"] = value;
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
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales »  Lead »  Lead FollowUp Report');</script>");

            lblMessage.Text = "";
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                List<PLeadQualification> Qualification = new BLead().GetLeadQualification(null, null);
                new DDLBind(ddlSQualification, Qualification, "Qualification", "QualificationID");

                List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
                new DDLBind(ddlSSource, Source, "Source", "SourceID");

                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
                 
                List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, null);
                new DDLBind(ddlSState, State, "State", "StateID");

                List<PLeadStatus> Status = new BLead().GetLeadStatus(null, null);
                new DDLBind(ddlSStatus, Status, "Status", "StatusID"); 
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID"); 
            } 
        }

        void fillEquipmentPopulationReport()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
               
                PLeadSearch S = new PLeadSearch(); 
                S.LeadNumber = txtLeadNumber.Text.Trim();
                S.StateID = ddlSState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSState.SelectedValue);
                S.QualificationID = ddlSQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSQualification.SelectedValue);
                S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
                S.ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
                S.CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
                S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);

                S.CustomerCode = txtCustomer.Text.Trim();
                S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
                S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

                S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                S.SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                LeadFoloowUp = new BLead().GetLeadFollowUpReport(S).Tables[0];

                gvEquipment.PageIndex = 0;
                gvEquipment.DataSource = LeadFoloowUp;
                gvEquipment.DataBind();
                if (LeadFoloowUp.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + LeadFoloowUp.Rows.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageIndex > 0)
            {
                gvEquipment.DataSource = LeadFoloowUp;
                gvEquipment.PageIndex = gvEquipment.PageIndex - 1;

                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + LeadFoloowUp.Rows.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageCount > gvEquipment.PageIndex)
            {
                gvEquipment.DataSource = LeadFoloowUp;
                gvEquipment.PageIndex = gvEquipment.PageIndex + 1;
                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + LeadFoloowUp.Rows.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(LeadFoloowUp, "Lead FoloowUp Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipment.DataSource = LeadFoloowUp;
            gvEquipment.PageIndex = e.NewPageIndex;
            gvEquipment.DataBind();
            lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + LeadFoloowUp.Rows.Count;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillEquipmentPopulationReport();
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }
    }
}