using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketTSIRMailToVendorReport : System.Web.UI.Page
    {
        public List<PDMS_ICTicketTSIR> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketTSIRMailToVendorReport"] == null)
                {
                    Session["DMS_ICTicketTSIRMailToVendorReport"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["DMS_ICTicketTSIRMailToVendorReport"];
            }
            set
            {
                Session["DMS_ICTicketTSIRMailToVendorReport"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » TSIR » Mail Send To Vendor');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtTSIRDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtTSIRDateTo.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }

            }
        }
        void fillICTicketTSIR()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? TSIRDateF = string.IsNullOrEmpty(txtTSIRDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateFrom.Text.Trim());
                DateTime? TSIRDateT = string.IsNullOrEmpty(txtTSIRDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateTo.Text.Trim());
                gvICTickets.DataSource = new BDMS_ICTicketTSIR().GetICTicketTSIRMailToVendor(DealerCode, txtTSIRNo.Text.Trim(), TSIRDateF, TSIRDateT, txtICTicketNumber.Text.Trim(), txtMachineSerialNumber.Text.Trim());
                gvICTickets.DataBind();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRMailToVendorReport", "fillICTicketTSIR", e1);
                throw e1;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            DateTime? TSIRDateF = string.IsNullOrEmpty(txtTSIRDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateFrom.Text.Trim());
            DateTime? TSIRDateT = string.IsNullOrEmpty(txtTSIRDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateTo.Text.Trim());

            DataTable dt = new BDMS_ICTicketTSIR().GetICTicketTSIRMailToVendor(DealerCode, txtTSIRNo.Text.Trim(), TSIRDateF, TSIRDateT, txtICTicketNumber.Text.Trim(), txtMachineSerialNumber.Text.Trim());
            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            fillICTicketTSIR();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillICTicketTSIR();
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