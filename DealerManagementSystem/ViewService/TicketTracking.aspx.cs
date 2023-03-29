using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class TicketTracking : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_TicketTracking; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_TicketTracking.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Tracking');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPSC();
                FillICTicket();
                FillClaim();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillPSC()
        {
            try
            {
                gvPSR.DataBind();
                gvPSC.DataBind();
                gvInv.DataBind();

                TraceLogger.Log(DateTime.Now);
               
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_TicketTracking", "fillICTicket", e1);
                throw e1;
            }
        }

        void FillClaim()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);

                List<PDMS_WarrantyInvoiceHeader> InvoiceHeaders = new BDMS_WarrantyClaim().GetWarrantyClaimReport(txtICTicket.Text.Trim(), null, null, "", null, null, "", null, null, null, "", "", "", false, PSession.User.UserID);
                gvClaimByClaimID.DataSource = InvoiceHeaders;
                gvClaimByClaimID.DataBind();

                for (int i = 0; i < gvClaimByClaimID.Rows.Count; i++)
                {
                    string supplierPOID = Convert.ToString(gvClaimByClaimID.DataKeys[i].Value);
                    GridView gvICTicketItems = (GridView)gvClaimByClaimID.Rows[i].FindControl("gvICTicketItems");
                    List<PDMS_WarrantyInvoiceItem> Item = new List<PDMS_WarrantyInvoiceItem>();
                    Item = InvoiceHeaders.Find(s => s.InvoiceNumber == supplierPOID).InvoiceItems;
                    gvICTicketItems.DataSource = Item;
                    gvICTicketItems.DataBind();
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }

        void FillICTicket()
        {
            gvICTickets.PageIndex = 0;
            gvICTickets.DataSource = new BDMS_ICTicket().GetICTicket(null, "", txtICTicket.Text.Trim(), null, null, null, null);
            gvICTickets.DataBind();
        }
    }
}