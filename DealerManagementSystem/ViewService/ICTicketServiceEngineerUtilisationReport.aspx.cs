using Business;
using Properties;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketServiceEngineerUtilisationReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketServiceEngineerUtilisationReport; } }
        public DataTable ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketServiceEngineerUtilisationReport"] == null)
                {
                    Session["DMS_ICTicketServiceEngineerUtilisationReport"] = new DataTable();
                }
                return (DataTable)Session["DMS_ICTicketServiceEngineerUtilisationReport"];
            }
            set
            {
                Session["DMS_ICTicketServiceEngineerUtilisationReport"] = value;
            }
        }
        public DataTable UtilisationReportD1
        {
            get
            {
                if (Session["UtilisationReportD1"] == null)
                {
                    Session["UtilisationReportD1"] = new DataTable();
                }
                return (DataTable)Session["UtilisationReportD1"];
            }
            set
            {
                Session["UtilisationReportD1"] = value;
            }
        }
        public DataTable UtilisationReportD2
        {
            get
            {
                if (Session["UtilisationReportD2"] == null)
                {
                    Session["UtilisationReportD2"] = new DataTable();
                }
                return (DataTable)Session["UtilisationReportD2"];
            }
            set
            {
                Session["UtilisationReportD2"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Service Engineer Utilisation Report');</script>");
            if (!IsPostBack)
            {
                fillStatus();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0].DID.ToString()));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
            }
        }
        void fillStatus()
        {
            ddlStatus.DataTextField = "ServiceStatus";
            ddlStatus.DataValueField = "ServiceStatusID";
            ddlStatus.DataSource = new BDMS_Service().GetServiceStatus(null, null);
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicket();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillICTicket()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());
                int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);

                divUtilisationReportDetails.Visible = false;
                if (cbDetails.Checked)
                {
                    divUtilisationReportDetails.Visible = true;
                    DataSet DataSet2 = new BDMS_ICTicket().GetICTicketServiceEngineerUtilisationReportDetails(DealerID, txtEmployeeCode.Text.Trim(), ICTicketDateF, ICTicketDateT, StatusID, PSession.User.UserID);

                    UtilisationReportD1 = DataSet2.Tables[0];
                    UtilisationReportD2 = DataSet2.Tables[1];
                    gvURD1.PageIndex = 0;
                    gvURD1.DataSource = UtilisationReportD1;
                    gvURD1.DataBind();

                    gvURD2.PageIndex = 0;
                    gvURD2.DataSource = UtilisationReportD2;
                    gvURD2.DataBind();


                    lblURD1Codunt.Text = "Total Calls : " + UtilisationReportD1.Rows.Count;
                    lblURD2Codunt.Text = "Total Man days : " + UtilisationReportD2.Rows.Count;
                }

                DataSet DataSet1 = new BDMS_ICTicket().GetICTicketServiceEngineerUtilisationReport(DealerID, txtEmployeeCode.Text.Trim(), ICTicketDateF, ICTicketDateT, StatusID, PSession.User.UserID);
                ICTicket = DataSet1.Tables[0];
                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = ICTicket;
                gvICTickets.DataBind();

                if (ICTicket.Rows.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Rows.Count;
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
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Rows.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Rows.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(ICTicket, "IC Ticket Details");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicket;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Rows.Count;
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void gvURD1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvURD1.DataSource = UtilisationReportD1;
            gvURD1.PageIndex = e.NewPageIndex;
            gvURD1.DataBind();

        }

        protected void gvURD2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvURD2.DataSource = UtilisationReportD2;
            gvURD2.PageIndex = e.NewPageIndex;
            gvURD2.DataBind();
        }

        protected void btnExportURD1_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(UtilisationReportD1, "Total Calls UtilisationReport");
        }

        protected void btnExportURD2_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(UtilisationReportD2, "Total Man days UtilisationReport");
        }
    }
}