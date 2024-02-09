using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class PaidServiceReportNew : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_PaidServiceReportNew; } }
        public List<PDMS_PaidServiceHeader> SDMS_PaidService
        {
            get
            {
                if (Session["DMS_PaidServiceReportNew"] == null)
                {
                    Session["DMS_PaidServiceReportNew"] = new List<PDMS_PaidServiceHeader>();
                }
                return (List<PDMS_PaidServiceHeader>)Session["DMS_PaidServiceReportNew"];
            }
            set
            {
                Session["DMS_PaidServiceReportNew"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_PaidServiceReportNew.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » PaidService » Report');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                // fillMTTR();
                // FillPageNo(1);
                txtICTicketDateF.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtICTicketDateT.Text = DateTime.Now.ToShortDateString();
                 
                    fillDealer(); 
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPaidService();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillPaidService()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);


                string ICTicketNumber = txtICTicketNumber.Text.Trim();
                DateTime? ICTicketDateF = null;
                DateTime? ICTicketDateT = null;

                if (!string.IsNullOrEmpty(txtICTicketDateF.Text.Trim()))
                {
                    ICTicketDateF = Convert.ToDateTime(txtICTicketDateF.Text.Trim());
                }
                if (!string.IsNullOrEmpty(txtICTicketDateT.Text.Trim()))
                {
                    ICTicketDateT = Convert.ToDateTime(txtICTicketDateT.Text.Trim());
                }

                int? DealerID = null;
                if (ddlDealer.SelectedValue != "0")
                {
                    DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                }
                string CustomerCode = txtCustomerCode.Text.Trim();

                int? ServiceStatusID = null;
                if (ddlServiceStatusID.SelectedValue != "0")
                {
                    ServiceStatusID = Convert.ToInt32(ddlServiceStatusID.SelectedValue);
                }
                int? ServiceTypeID = null;
                if (ddlServiceTypeID.SelectedValue != "0")
                {
                    ServiceTypeID = Convert.ToInt32(ddlServiceTypeID.SelectedValue);
                }
                SDMS_PaidService = new BDMS_Service().GetPaidServiceReport(ICTicketNumber, ICTicketDateF, ICTicketDateT, DealerID, CustomerCode, ServiceStatusID, ServiceTypeID);


                if (ddlDealer.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SDMS_PaidService
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    SDMS_PaidService.Clear();
                    foreach (var w in SOIs1)
                    {
                        SDMS_PaidService.Add(w.S);
                    }
                }

                //   SDMS_PurchaseOrder = PurchaseOrder;

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SDMS_PaidService;
                gvICTickets.DataBind();
                if (SDMS_PaidService.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PaidService.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_PaidServiceReport", "fillPaidService", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataSource = SDMS_PaidService;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PaidService.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataSource = SDMS_PaidService;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PaidService.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Ticket Number");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Customer Code");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Service Status");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Model");
            dt.Columns.Add("Peirod");
            dt.Columns.Add("Technician");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Quotation Number");
            dt.Columns.Add("Proforma Invoice Number");
            dt.Columns.Add("Invoice Number");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Value Before Tax");
            dt.Columns.Add("Tax"); ;
            dt.Columns.Add("Total");


            foreach (PDMS_PaidServiceHeader M in SDMS_PaidService)
            {
                dt.Rows.Add(
                    M.ICTicketNumber
                    , M.ICTicketDate.ToShortDateString()

                    , M.Dealer.DealerCode, M.Dealer.DealerName
                    , M.Customer.CustomerCode
                    , M.Customer.CustomerName
                    , M.ServiceStatus.ServiceStatus
                    , M.ServiceType.ServiceType
                    , M.Model
                     , M.Peirod
                     , M.Technician.ContactName
                    , "'" + M.ServiceItem.Material

                      , M.ServiceItem.MaterialDesc
                    , M.ServiceItem.QuotationNumber
                     , M.ServiceItem.ProformaInvoiceNumber
                      , M.ServiceItem.InvoiceNumber
                      , M.ServiceItem.InvoiceDate == null ? "" : ((DateTime)M.ServiceItem.InvoiceDate).ToShortDateString()
                        , M.ServiceItem.ValueBeforeTax == null ? 0 : decimal.Round((decimal)M.ServiceItem.ValueBeforeTax, 2, MidpointRounding.AwayFromZero)
                   , M.ServiceItem.Tax == null ? 0 : decimal.Round((decimal)M.ServiceItem.Tax, 2, MidpointRounding.AwayFromZero)
                   , M.ServiceItem.Total == null ? 0 : decimal.Round((decimal)M.ServiceItem.Total, 2, MidpointRounding.AwayFromZero)
                   );
            }
            new BXcel().ExporttoExcel(dt, "Paid Service Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_PaidService;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_PaidService.Count;

        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}