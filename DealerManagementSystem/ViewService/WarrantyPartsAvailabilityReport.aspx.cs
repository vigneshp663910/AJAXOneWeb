using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyPartsAvailabilityReport : System.Web.UI.Page
    {
        public List<PDMS_ServiceMaterial> ServiceMaterial
        {
            get
            {
                if (Session["DMS_WarrantyPartsAvailabilityReport"] == null)
                {
                    Session["DMS_WarrantyPartsAvailabilityReport"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)Session["DMS_WarrantyPartsAvailabilityReport"];
            }
            set
            {
                Session["DMS_WarrantyPartsAvailabilityReport"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Parts Availability Report');</script>");


            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtICLoginDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtICLoginDateTo.Text = DateTime.Now.ToShortDateString();

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
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }

        void FillWarrantyPartsAvailabilityReport()
        {
            int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
            DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());
            ServiceMaterial = new BDMS_Service().GetWarrantyPartsAvailabilityReport(DealerID, txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, txtMaterial.Text.Trim(), PSession.User.UserID);
            gvICTicket.DataSource = ServiceMaterial;
            gvICTicket.DataBind();

            lblRowCount.Text = (((gvICTicket.PageIndex) * gvICTicket.PageSize) + 1) + " - " + (((gvICTicket.PageIndex) * gvICTicket.PageSize) + gvICTicket.Rows.Count) + " of " + ServiceMaterial.Count;
            lblRowCount.Visible = true;
            ibtnArrowLeft.Visible = true;
            ibtnArrowRight.Visible = true;
        }
        protected void gvICTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTicket.DataSource = ServiceMaterial;
            gvICTicket.PageIndex = e.NewPageIndex;
            gvICTicket.DataBind();
            lblRowCount.Text = (((gvICTicket.PageIndex) * gvICTicket.PageSize) + 1) + " - " + (((gvICTicket.PageIndex) * gvICTicket.PageSize) + gvICTicket.Rows.Count) + " of " + ServiceMaterial.Count;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillWarrantyPartsAvailabilityReport();
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
            dt.Columns.Add("Model");
            dt.Columns.Add("MaterialC ode");
            dt.Columns.Add("Material Description");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Available Qty");
            dt.Columns.Add("Quotation Number");
            dt.Columns.Add("Quotation Date");
            dt.Columns.Add("Delivery Number");
            dt.Columns.Add("Delivery Date");
            dt.Columns.Add("Available Percentage");

            foreach (PDMS_ServiceMaterial IC in ServiceMaterial)
            {
                dt.Rows.Add(
                    IC.ICTicket.ICTicketNumber
                    , IC.ICTicket.ICTicketDate.ToShortDateString()
                    , IC.ICTicket.Dealer.DealerCode
                    , IC.ICTicket.Dealer.DealerName
                    , IC.ICTicket.Customer.CustomerCode
                    , IC.ICTicket.Customer.CustomerName
                    , IC.ICTicket.Equipment.EquipmentModel.Model
                    , "'" + IC.Material.MaterialCode
                    , IC.Material.MaterialDescription
                    , IC.Qty
                    , IC.AvailableQty
                    , IC.QuotationNumber
                    , IC.QuotationDate == null ? "" : ((DateTime)IC.QuotationDate).ToShortDateString()
                    , IC.DeliveryNumber
                    , IC.DeliveryDate == null ? "" : ((DateTime)IC.DeliveryDate).ToShortDateString()
                     , IC.AvailablePercentage
                    );
            }
            new BXcel().ExporttoExcel(dt, "Warranty Parts Availability");
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTicket.PageIndex > 0)
            {
                gvICTicket.PageIndex = gvICTicket.PageIndex - 1;
                gvICTicket.DataSource = ServiceMaterial;
                gvICTicket.DataBind();
                lblRowCount.Text = (((gvICTicket.PageIndex) * gvICTicket.PageSize) + 1) + " - " + (((gvICTicket.PageIndex) * gvICTicket.PageSize) + gvICTicket.Rows.Count) + " of " + ServiceMaterial.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTicket.PageCount > gvICTicket.PageIndex)
            {
                gvICTicket.PageIndex = gvICTicket.PageIndex + 1;
                gvICTicket.DataSource = ServiceMaterial;
                gvICTicket.DataBind();
                lblRowCount.Text = (((gvICTicket.PageIndex) * gvICTicket.PageSize) + 1) + " - " + (((gvICTicket.PageIndex) * gvICTicket.PageSize) + gvICTicket.Rows.Count) + " of " + ServiceMaterial.Count;
            }
        }
    }
}