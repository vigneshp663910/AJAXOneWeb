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
    public partial class ICTicketStatusReportForIC : System.Web.UI.Page
    {
        public List<PDMS_ICTicket> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketStatusReport"] == null)
                {
                    Session["DMS_ICTicketStatusReport"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["DMS_ICTicketStatusReport"];
            }
            set
            {
                Session["DMS_ICTicketStatusReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Status Report');</script>");

            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                fillStatus();
                if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
                {
                    long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                    PDMS_ICTicket DMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);

                    txtICTicketNumber.Text = DMS_ICTicket.ICTicketNumber;
                    txtICLoginDateFrom.Text = "";
                    txtICLoginDateTo.Text = "";
                    fillICTicket();
                    RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[0].FindControl("rbICTicketID");
                    rbICTicketID.Checked = true;
                }
                else
                {
                    txtICLoginDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtICLoginDateTo.Text = DateTime.Now.ToShortDateString();
                }
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

            }
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
                 List<PDMS_ICTicket> SOIs = null;
                
                SOIs = new BDMS_ICTicket().GetICTicketStatusReportForIC(DealerID, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, StatusID, txtMachineSerialNumber.Text.Trim());

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }
                ICTicket = SOIs;

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
                if (SOIs.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
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
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IC Tkt No");
            dt.Columns.Add("Call Login Dt(IC)");
            dt.Columns.Add("Ser. Req. Dt");
            dt.Columns.Add("Ticket type");
            dt.Columns.Add("Cust. ID");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Ticket Status");
            dt.Columns.Add("Reason for Decline");
            dt.Columns.Add("Problem Reported");
            dt.Columns.Add("Model");
            dt.Columns.Add("Serial No."); 
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");

            foreach (PDMS_ICTicket IC in ICTicket)
            {
                dt.Rows.Add(
                    IC.ICTicketNumber
                    , IC.ICTicketDate.ToShortDateString()
                    , IC.RequestedDate == null ? "" : ((DateTime)IC.RequestedDate).ToShortDateString()
                    , IC.ServiceType == null ? "" : IC.ServiceType.ServiceType
                    , IC.Customer.CustomerCode
                    , IC.Customer.CustomerName
                    , IC.ServiceStatus.ServiceStatus
                    , IC.ReqDeclinedReason
                    , IC.ComplaintDescription
                    , IC.Equipment.EquipmentModel.Model
                    , IC.Equipment.EquipmentSerialNo                    
                    , IC.Dealer.DealerCode
                    , IC.Dealer.DealerName
                   );
            }


            new BXcel().ExporttoExcel(dt, "IC Ticket Status Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicket;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        void fillStatus()
        {
            ddlStatus.DataTextField = "ServiceStatus";
            ddlStatus.DataValueField = "ServiceStatusID";
            ddlStatus.DataSource = new BDMS_Service().GetServiceStatus(null, null);
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}