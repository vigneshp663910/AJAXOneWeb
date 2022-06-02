using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimInvoiceCreate5k : System.Web.UI.Page
    {
        public List<PDMS_WarrantyInvoiceHeader> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimInvoiceCreate5k"] == null)
                {
                    Session["DMS_WarrantyClaimInvoiceCreate5k"] = new List<PDMS_WarrantyInvoiceHeader>();
                }
                return (List<PDMS_WarrantyInvoiceHeader>)Session["DMS_WarrantyClaimInvoiceCreate5k"];
            }
            set
            {
                Session["DMS_WarrantyClaimInvoiceCreate5k"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimInvoiceCreate5k.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Final Invoice Create > 50K');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                FillYear();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCount.Visible = false;
            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillClaim();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillClaim()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string DealerCode = ddlDealerCode.SelectedValue == "0" ? "" : ddlDealerCode.SelectedValue;
                int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
                int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
                string ClaimNumber = txtClaimNumber.Text.Trim();
                List<PDMS_WarrantyInvoiceHeader> SOIs = null;
                GridView gv = null;
                SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimForGenerateInvoiceAbove50k(DealerCode, Year, Month, ClaimNumber);
                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.DealerCode equals D.UserName
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
                SDMS_WarrantyClaimHeader = SOIs;
                gv = gvClaimByClaimID;
                gvClaimByClaimID.Visible = true;
                gv.PageIndex = 0;
                gv.DataSource = SOIs;
                gv.DataBind();
                if (SOIs.Count == 0)
                {
                    lblRowCount.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        void FillYear()
        {

            for (int Year = 2015; Year <= DateTime.Now.Year; Year++)
            {
                ddlYear.Items.Add(new ListItem(Year.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }



        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = null;

            gv = gvClaimByClaimID;
            gv.DataSource = SDMS_WarrantyClaimHeader;


            gv.PageIndex = e.NewPageIndex;
            gv.DataBind();
            lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimByClaimID.DataKeys[e.Row.RowIndex].Value);
                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");

                    Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    if (!string.IsNullOrEmpty(lblPscID.Text))
                    {
                        GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                        gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                        gvFileAttached.DataBind();
                    }
                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.InvoiceNumber == supplierPOID).InvoiceItems;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();


                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            gv = gvClaimByClaimID;
            gv.DataSource = SDMS_WarrantyClaimHeader;


            if (gv.PageIndex > 0)
            {
                gv.PageIndex = gv.PageIndex - 1;

                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;

            gv = gvClaimByClaimID;
            gv.DataSource = SDMS_WarrantyClaimHeader;

            if (gv.PageCount > gv.PageIndex)
            {
                gv.PageIndex = gv.PageIndex + 1;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblICTicketID = (Label)gvClaimByClaimID.Rows[gvRow.RowIndex].FindControl("lblICTicketID");

                Label lblInvoiceNumber = (Label)gvClaimByClaimID.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
                TextBox txtThrough = (TextBox)gvClaimByClaimID.Rows[gvRow.RowIndex].FindControl("txtThrough");
                TextBox txtLRNumber = (TextBox)gvClaimByClaimID.Rows[gvRow.RowIndex].FindControl("txtLRNumber");
                TraceLogger.Log(DateTime.Now);
                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(ddlDealerCode.SelectedValue);
                PDMS_Customer CustomerAE = new BDMS_Customer().GetCustomerAE();

                PDMS_ICTicket IC = new BDMS_ICTicket().GetICTicket(null, null, lblICTicketID.Text, null, null, null, null)[0];

                long WarrantyClaimInvoiceID = new BDMS_WarrantyClaimInvoice().InsertWarrantyClaimInvoiceAbove5K(ddlDealerCode.SelectedValue, lblInvoiceNumber.Text, PSession.User.UserID, Server.MapPath("~/Print/DMS_ClaimInvoice50K.rdlc"), txtThrough.Text, txtLRNumber.Text, Dealer, CustomerAE, (IC.AEPayPercentage == 100 || IC.AEPayPercentage == null) ? false : true);

                //   new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvoiceAbove50K(WarrantyClaimInvoiceID));

                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, 3, "")[0];

                lblMessage.Text = "Invoice number -" + ClaimInvoice.InvoiceNumber + " generated successfully - Select 'Final invoice report' to print above 50K invoice";
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    new BDMS_EInvoice().GeneratEInvoice(ClaimInvoice.InvoiceNumber);
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
                fillClaim();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceCreate5k", "btnGenerateInvoice_Click", e1);
                throw e1;
            }
        }
    }
}