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

namespace DealerManagementSystem.Dashboard
{
    public partial class FirstTimeRightForWarrantyService : BasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        public List<PDMS_ICTicket> ICTicket
        {
            get
            {
                if (Session["DMS_DashFirstTimeRightForWarrantyService"] == null)
                {
                    Session["DMS_DashFirstTimeRightForWarrantyService"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["DMS_DashFirstTimeRightForWarrantyService"];
            }
            set
            {
                Session["DMS_DashFirstTimeRightForWarrantyService"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

                if (Session["SerDealerID"] != null)
                {
                    ddlDealer.SelectedValue = Convert.ToString((int)Session["SerDealerID"]);
                }

                if (Session["SerDateFrom"] != null)
                {
                    txtDateFrom.Text = Convert.ToString(Session["SerDateFrom"]);
                }
                if (Session["SerDateTo"] != null)
                {
                    txtDateTo.Text = Convert.ToString(Session["SerDateTo"]);
                }


                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealer.Enabled = false;
                }
                else
                {
                    ddlDealer.Enabled = true;
                    fillDealer();
                }
                fillICTicket();

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
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
                DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

                Session["SerDealerID"] = DealerID;
                Session["SerDateFrom"] = DateFrom;
                Session["SerDateTo"] = DateTo;

                ICTicket = new BDMS_ICTicket().GetICTicketFirstTimeRightForWarrantyService(DealerID, DateFrom, DateTo, PSession.User.UserID);

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = ICTicket;
                gvICTickets.DataBind();
                if (ICTicket.Count == 0)
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

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Requested Date");
            dt.Columns.Add("Model");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Service Priority");
            dt.Columns.Add("Service Status");
            dt.Columns.Add("Margin");
            dt.Columns.Add("Technician");
            dt.Columns.Add("Last IC Ticket");
            dt.Columns.Add("Last IC Ticket Date");
            dt.Columns.Add("Last IC Dealer");
            dt.Columns.Add("Last IC Technician");

            foreach (PDMS_ICTicket IC in ICTicket)
            {
                dt.Rows.Add(
                    IC.ICTicketNumber
                    , IC.ICTicketDate.ToShortDateString()
                    , IC.Dealer.DealerCode
                    , IC.Dealer.DealerName
                    , IC.Customer.CustomerCode
                    , IC.Customer.CustomerName
                    , IC.RequestedDate == null ? "" : ((DateTime)IC.RequestedDate).ToShortDateString()
                    , IC.Equipment.EquipmentModel.Model
                    , IC.ServiceType == null ? "" : IC.ServiceType.ServiceType
                    , IC.ServicePriority == null ? "" : IC.ServicePriority.ServicePriority
                    , IC.ServiceStatus.ServiceStatus
                    , IC.IsMarginWarranty
                    , IC.Technician.ContactName
                    , IC.LastICTicket.ICTicketNumber
                    , IC.LastICTicket.ICTicketDate.ToShortDateString()
                    , IC.LastICTicket.Dealer.DealerCode
                    , IC.LastICTicket.Technician.ContactName
                    );
            }



            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
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
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();

            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }



        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "DMS_ICTicketView.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }

    }
}