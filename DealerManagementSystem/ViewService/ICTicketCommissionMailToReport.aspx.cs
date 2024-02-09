using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketCommissionMailToReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketCommissionMailToReport; } }
        public DataTable ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketCommissionMailToReport"] == null)
                {
                    Session["DMS_ICTicketCommissionMailToReport"] = new DataTable();
                }
                return (DataTable)Session["DMS_ICTicketCommissionMailToReport"];
            }
            set
            {
                Session["DMS_ICTicketCommissionMailToReport"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Commission Mail To Report');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtICTicketDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtICTicketDateTo.Text = DateTime.Now.ToShortDateString();


                fillDealer();
            }
        }
        void fillICTicketCommissionMailTo()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICTicketDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICTicketDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICTicketDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICTicketDateTo.Text.Trim());
                ICTicket = new BDMS_ICTicket().GetICTicketCommissionMailTo(DealerCode, ICTicketDateF, ICTicketDateT, txtICTicketNumber.Text.Trim(), txtMachineSerialNumber.Text.Trim());

                gvICTickets.DataSource = ICTicket;
                gvICTickets.DataBind();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ICTicketCommissionMailToReport", "fillICTicketCommissionMailTo", e1);
                throw e1;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(ICTicket, "IC Ticket Commission Mail To");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            fillICTicketCommissionMailTo();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillICTicketCommissionMailTo();
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}