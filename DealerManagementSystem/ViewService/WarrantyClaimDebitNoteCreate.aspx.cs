using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimDebitNoteCreate : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimDebitNoteCreate.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyClaimInvoice> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimDebitNoteCreate"] == null)
                {
                    Session["DMS_WarrantyClaimDebitNoteCreate"] = new List<PDMS_WarrantyClaimInvoice>();
                }
                return (List<PDMS_WarrantyClaimInvoice>)Session["DMS_WarrantyClaimDebitNoteCreate"];
            }
            set
            {
                Session["DMS_WarrantyClaimDebitNoteCreate"] = value;
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

                long? WarrantyClaimInvoiceID = null;
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                string InvoiceNumber = txtInvoiceNumber.Text.Trim();



                DateTime? InvoiceDateF = string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateFrom.Text.Trim());
                DateTime? InvoiceDateT = string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateTo.Text.Trim());

                string ClaimNumber = txtClaimNumber.Text.Trim();
                string ICTicketNumber = txtICTicketNumber.Text.Trim();

                int? InvoiceTypeID = ddlInvoiceTypeID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlInvoiceTypeID.SelectedValue);



                List<PDMS_WarrantyClaimInvoice> SOIs = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimInvoiceForCreateDebitNote(WarrantyClaimInvoiceID, DealerID, InvoiceNumber, InvoiceDateF, InvoiceDateT, ClaimNumber, ICTicketNumber, InvoiceTypeID);

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
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
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
                    List<PDMS_WarrantyClaimInvoiceItem> Lines = new List<PDMS_WarrantyClaimInvoiceItem>();
                    Lines = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyClaimInvoiceID == Convert.ToInt64(supplierPOID)).InvoiceItems;

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

        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblWarrantyClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblWarrantyClaimInvoiceID");

                PAttachedFile UploadedFile = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceFile(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text));
                if (UploadedFile == null)
                {
                    UploadedFile = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceFile(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text));

                }

                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName + "." + UploadedFile.FileType);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnCreateDebitNote_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView gvLinesGrid = (GridView)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("gvClaimInvoiceItem");
            Label lblWarrantyClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblWarrantyClaimInvoiceID");
            Label lblDealerCode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblDealerCode");
            decimal GrandTotal = 0;
            decimal Tax = 0;


            List<PDMS_WarrantyClaimInvoiceItem> Claims = new List<PDMS_WarrantyClaimInvoiceItem>();

            for (int i = 0; i < gvLinesGrid.Rows.Count; i++)
            {
                Label lblWarrantyClaimItemID = (Label)gvLinesGrid.Rows[i].FindControl("lblWarrantyClaimItemID");

                TextBox txtDebitValue = (TextBox)gvLinesGrid.Rows[i].FindControl("txtDebitValue");
                Label lblAmount = (Label)gvLinesGrid.Rows[i].FindControl("lblApprovedValue");
                Label lblCGST = (Label)gvLinesGrid.Rows[i].FindControl("lblCGST");
                Label lblSGST = (Label)gvLinesGrid.Rows[i].FindControl("lblSGST");
                Label lblIGST = (Label)gvLinesGrid.Rows[i].FindControl("lblIGST");

                TextBox txtRemarks = (TextBox)gvLinesGrid.Rows[i].FindControl("txtRemarks");
                FileUpload fu = (FileUpload)gvLinesGrid.Rows[i].FindControl("fu");



                decimal Amount = Convert.ToDecimal(lblAmount.Text);


                if (string.IsNullOrEmpty(txtDebitValue.Text) || txtDebitValue.Text == "0")
                {
                    continue;
                }
                decimal parsedValue;
                if (!decimal.TryParse(txtDebitValue.Text, out parsedValue))
                {
                    lblMessage.Text = "Please Enter decimal value in debit amount !";
                    txtDebitValue.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (Amount < Convert.ToDecimal(txtDebitValue.Text))
                {
                    lblMessage.Text = "Please enter debit amount less than or equal of claim amount";
                    txtDebitValue.Focus();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                Tax = Convert.ToDecimal(lblCGST.Text) + Convert.ToDecimal(lblSGST.Text) + Convert.ToDecimal(lblIGST.Text);
                GrandTotal = GrandTotal + Convert.ToDecimal(txtDebitValue.Text) + (Tax * Convert.ToDecimal(txtDebitValue.Text) / 100);



                string FileName = "";
                string ContentType = "";
                int FileSize = 0;
                byte[] AttachedByte;

                if (fu.PostedFile.FileName.Length != 0)
                {
                    string name = fu.PostedFile.FileName;
                    int position = name.LastIndexOf("\\");
                    name = name.Substring(position + 1);
                    FileName = name;
                    ContentType = fu.PostedFile.ContentType;
                    FileSize = fu.PostedFile.ContentLength;

                    byte[] fileData = new byte[FileSize];
                    fu.PostedFile.InputStream.Read(fileData, 0, FileSize);
                    AttachedByte = fileData;
                }
                else
                {
                    AttachedByte = new byte[0];
                }
                Claims.Add(new PDMS_WarrantyClaimInvoiceItem()
                {
                    WarrantyClaimInvoiceItemID = Convert.ToInt64(lblWarrantyClaimItemID.Text),
                    WarrantyClaimInvoiceID = Convert.ToInt64(lblWarrantyClaimInvoiceID.Text),
                    ApprovedValue = Convert.ToDecimal(txtDebitValue.Text),
                    Remark = txtRemarks.Text,
                    FileName = FileName,
                    ContentType = ContentType,
                    FileSize = FileSize,
                    AttachedByte = AttachedByte
                });

            }
            PDMS_Customer Supplier = new BDMS_Customer().GetCustomerAE();
            PDMS_Customer Buyer = new SCustomer().getCustomerAddress(lblDealerCode.Text);
            decimal? TCSValue = null;
            decimal? TCSTax = null;
            //if (PDMS_EInvoice.TcsDate <= DateTime.Now)
            //{
            //   TCSTax = PDMS_EInvoice.TcsTax;
            //  TCSValue = (GrandTotal * PDMS_EInvoice.TcsTax / 100);
            //  GrandTotal = GrandTotal + (decimal)TCSValue;
            //}

            long SNo = new BDMS_WarrantyClaimDebitNote().InsertWarrantyClaimDebitNote(Claims, PSession.User.UserID, GrandTotal, TCSValue, TCSTax, Supplier, Buyer);
            if (SNo != 0)
            {
                string InvoiceNumber = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteReport(SNo, null, null, null, null, null, PSession.User.UserID)[0].DebitNoteNumber;

                lblMessage.Text = "Debit Note number " + InvoiceNumber + " is requested";
                lblMessage.ForeColor = Color.Green;

                fillWarrantyInvoice();
            }
            else
            {
                // lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is not approved";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }
    }
}