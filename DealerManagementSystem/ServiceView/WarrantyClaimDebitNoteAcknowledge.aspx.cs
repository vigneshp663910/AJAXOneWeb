using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;
using System.Drawing;

namespace DealerManagementSystem.ServiceView
{
    public partial class WarrantyClaimDebitNoteAcknowledge : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimDebitNoteAcknowledge.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyClaimDebitNote> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimDebitNoteAcknowledge"] == null)
                {
                    Session["DMS_WarrantyClaimDebitNoteAcknowledge"] = new List<PDMS_WarrantyClaimDebitNote>();
                }
                return (List<PDMS_WarrantyClaimDebitNote>)Session["DMS_WarrantyClaimDebitNoteAcknowledge"];
            }
            set
            {
                Session["DMS_WarrantyClaimDebitNoteAcknowledge"] = value;
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
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillWarrantyInvoice();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillWarrantyInvoice()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);

                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                string DebitNoteNumber = txtDebitNoteNumber.Text.Trim();

                DateTime? DebitNoteDateF = string.IsNullOrEmpty(txtDebitNoteDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDebitNoteDateF.Text.Trim());
                DateTime? DebitNoteDateT = string.IsNullOrEmpty(txtDebitNoteDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDebitNoteDateT.Text.Trim());
                string InvoiceNumber = txtDebitNoteNumber.Text.Trim();
                int UserID = PSession.User.UserID;


                List<PDMS_WarrantyClaimDebitNote> SOIs = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteAcknowledge(null, DealerID, DebitNoteNumber, DebitNoteDateF, DebitNoteDateT, InvoiceNumber, UserID);




                SDMS_WarrantyClaimHeader = SOIs;

                gvClaimInvoice.PageIndex = 0;
                gvClaimInvoice.DataSource = SOIs;
                gvClaimInvoice.DataBind();
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
                    lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "fillWarrantyInvoice", e1);
                throw e1;
            }
        }

        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageIndex > 0)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex - 1;
                gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageCount > gvClaimInvoice.PageIndex)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex + 1;
                gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
            gvClaimInvoice.DataBind();
            lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
                    Label lblInvoiceTypeID = (Label)e.Row.FindControl("lblInvoiceTypeID");

                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
                    List<PDMS_WarrantyClaimDebitNoteItem> Lines = new List<PDMS_WarrantyClaimDebitNoteItem>();
                    Lines = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyClaimDebitNoteID == Convert.ToInt64(supplierPOID)).DebitNoteItems;

                    gvClaimInvoiceItem.DataSource = Lines;
                    gvClaimInvoiceItem.DataBind();
                    //for (int i = 0; i < gvClaimInvoiceItem.Rows.Count; i++)
                    //{
                    //    TextBox txtDebitQty = (TextBox)gvClaimInvoiceItem.Rows[i].FindControl("txtDebitQty");
                    //    txtDebitQty.Visible = false;
                    //}

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnCreateDebitNote_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblWarrantyClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblWarrantyClaimDebitNoteID");

            Boolean SNo = new BDMS_WarrantyClaimDebitNote().AcknowledgeWarrantyClaimDebitNote(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text), PSession.User.UserID);
            if (SNo)
            {

                lblMessage.Text = "Debit Note is Acknowledge";
                lblMessage.ForeColor = Color.Green;

                fillWarrantyInvoice();
            }
            else
            {
                lblMessage.Text = "Debit Note is not Acknowledge";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnkButton = sender as LinkButton;
                //child gridview row clicked
                GridViewRow childRow = lnkButton.NamingContainer as GridViewRow;
                //child grid clicked
                GridView childGrid = childRow.NamingContainer as GridView;
                //parent gridviewrow containing the child grid
                GridViewRow parentRow = (childGrid.NamingContainer as GridViewRow);
                //Id is the datakeyname of my gridview
                // GridView gvClaimInvoiceItem = (GridView)gvClaimInvoice.Rows[parentRow.RowIndex].FindControl("gvClaimInvoiceItem");

                //  GridView gvClaimInvoiceItem = (GridView)gvClaimInvoice.Rows[parentRow.RowIndex].FindControl("gvClaimInvoiceItem");


                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                PDMS_WarrantyClaimDebitNoteItem UploadedFile = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteItemAttachment(Convert.ToInt64(lnkButton.CommandName));

                Response.AddHeader("Content-type", UploadedFile.ContentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedByte);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }
    }
}