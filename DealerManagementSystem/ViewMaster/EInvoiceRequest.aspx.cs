using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class EInvoiceRequest : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "BDMS_EInvoiceRequest.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_EInvoice> EInvoice
        {
            get
            {
                if (Session["BDMS_EInvoiceRequest"] == null)
                {
                    Session["BDMS_EInvoiceRequest"] = new List<PDMS_EInvoice>();
                }
                return (List<PDMS_EInvoice>)Session["BDMS_EInvoiceRequest"];
            }
            set
            {
                Session["BDMS_EInvoiceRequest"] = value;
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


                string InvoiceNumber = txtInvoiceNumber.Text.Trim();
                DateTime? InvoiceDateF = string.IsNullOrEmpty(txtInvoiceDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateFrom.Text.Trim()); ;
                DateTime? InvoiceDateT = string.IsNullOrEmpty(txtInvoiceDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtInvoiceDateTo.Text.Trim()); ;
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                string CustomerCode = txtCustomerCode.Text.Trim();

                EInvoice = new BDMS_EInvoice().GetInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);


                gvClaimInvoice.PageIndex = 0;
                gvClaimInvoice.DataSource = EInvoice;
                gvClaimInvoice.DataBind();
                if (EInvoice.Count == 0)
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
                    lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + EInvoice.Count;
                }
                GridEditButtonVisible();
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
            //  FillCheckedInv();
            if (gvClaimInvoice.PageIndex > 0)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex - 1;
                gvClaimInvoice.DataSource = EInvoice;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + EInvoice.Count;
            }
            GridEditButtonVisible();
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            //FillCheckedInv();
            if (gvClaimInvoice.PageCount > gvClaimInvoice.PageIndex)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex + 1;
                gvClaimInvoice.DataSource = EInvoice;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + EInvoice.Count;
            }
            GridEditButtonVisible();
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //FillCheckedInv();
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            gvClaimInvoice.DataSource = EInvoice;
            gvClaimInvoice.DataBind();
            lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + EInvoice.Count;
            GridEditButtonVisible();
        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");

                    List<PDMS_EInvoiceItem> Lines = new List<PDMS_EInvoiceItem>();
                    Lines = EInvoice.Find(s => s.EInvoiceID == Convert.ToInt64(supplierPOID)).EInvoiceItems;
                    gvClaimInvoiceItem.DataSource = Lines;
                    gvClaimInvoiceItem.DataBind();

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("vendor_code");
            dt.Columns.Add("invoice_date");
            dt.Columns.Add("reference");
            dt.Columns.Add("material");
            dt.Columns.Add("Taxable_value");
            dt.Columns.Add("total");

            dt.Columns.Add("bp_code");
            dt.Columns.Add("bp_name");
            dt.Columns.Add("ic_ticket");
            dt.Columns.Add("machine_serial_no");
            dt.Columns.Add("hsn_code");
            dt.Columns.Add("Created by");
            dt.Columns.Add("model");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Remark");
            dt.Columns.Add("Annexure No");
            dt.Columns.Add("Period");

            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }



        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label InvoiceNumber = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBillingDocument");
                PDMS_EInvoice EInvoice = new BDMS_EInvoice().GetInvoiceForRequestEInvoice(InvoiceNumber.Text, null, null, null, null)[0];
                string EInvoivePathImport = PDMS_EInvoice.EInvoivePathImport + "/Deleted/" + InvoiceNumber.Text + DateTime.Now.ToString("yyyymmddHHMMss");
                if (!Directory.Exists(EInvoivePathImport))
                {
                    Directory.CreateDirectory(EInvoivePathImport);
                }
                new BDMS_EInvoice().DownloadAndDeleteFtp(EInvoice.EInvoiceFTPPath + "output_files/", EInvoice.EInvoiceFTPUserID, EInvoice.EInvoiceFTPPassword, EInvoivePathImport, EInvoice.BillingDocument);

                lblMessage.Text = new BDMS_EInvoice().GeneratEInvoice(InvoiceNumber.Text);
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            fillWarrantyInvoice();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            lblMessage.Visible = true;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                Label lblBuyerGSTIN = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerGSTIN");
                Label lblBuyerStateCode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerStateCode");
                Label lblBuyer_addr1 = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_addr1");
                Label lblBuyer_loc = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_loc");
                Label lblBuyerPincode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerPincode");

                TextBox txtBuyerGSTIN = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerGSTIN");
                TextBox txtBuyerStateCode = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerStateCode");
                TextBox txtBuyer_addr1 = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_addr1");
                TextBox txtBuyer_loc = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_loc");
                TextBox txtBuyerPincode = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerPincode");

                Button btnEdit = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnEdit");
                Button btnUpdate = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnUpdate");
                Button btnCancel = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnCancel");

                lblBuyerGSTIN.Visible = false;
                lblBuyerStateCode.Visible = false;
                lblBuyer_addr1.Visible = false;
                lblBuyer_loc.Visible = false;
                lblBuyerPincode.Visible = false;

                txtBuyerGSTIN.Visible = true;
                txtBuyerStateCode.Visible = true;
                txtBuyer_addr1.Visible = true;
                txtBuyer_loc.Visible = true;
                txtBuyerPincode.Visible = true;

                btnEdit.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblBillingDocument = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBillingDocument");

                Label lblBuyerGSTIN = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerGSTIN");
                Label lblBuyerStateCode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerStateCode");
                Label lblBuyer_addr1 = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_addr1");
                Label lblBuyer_loc = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_loc");
                Label lblBuyerPincode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerPincode");

                TextBox txtBuyerGSTIN = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerGSTIN");
                TextBox txtBuyerStateCode = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerStateCode");
                TextBox txtBuyer_addr1 = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_addr1");
                TextBox txtBuyer_loc = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_loc");
                TextBox txtBuyerPincode = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerPincode");


                string InvoiceNumber = lblBillingDocument.Text.Trim();
                string BuyerGSTIN = txtBuyerGSTIN.Text.Trim();
                string BuyerStateCode = txtBuyerStateCode.Text.Trim();
                string Buyer_addr1 = txtBuyer_addr1.Text.Trim(); ;
                string Buyer_loc = txtBuyer_loc.Text.Trim();
                string BuyerPincode = txtBuyerPincode.Text.Trim();

                if (new BDMS_EInvoice().UpdateEInvoiveBuyerDetails(InvoiceNumber, BuyerGSTIN, BuyerStateCode, Buyer_addr1, Buyer_loc, BuyerPincode, PSession.User.UserID))
                {
                    Button btnEdit = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnEdit");
                    Button btnUpdate = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnUpdate");
                    Button btnCancel = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnCancel");

                    lblBuyerGSTIN.Visible = true;
                    lblBuyerStateCode.Visible = true;
                    lblBuyer_addr1.Visible = true;
                    lblBuyer_loc.Visible = true;
                    lblBuyerPincode.Visible = true;

                    txtBuyerGSTIN.Visible = false;
                    txtBuyerStateCode.Visible = false;
                    txtBuyer_addr1.Visible = false;
                    txtBuyer_loc.Visible = false;
                    txtBuyerPincode.Visible = false;

                    btnEdit.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;

                    lblBuyerGSTIN.Text = txtBuyerGSTIN.Text;
                    lblBuyerStateCode.Text = txtBuyerStateCode.Text;
                    lblBuyer_addr1.Text = txtBuyer_addr1.Text;
                    lblBuyer_loc.Text = txtBuyer_loc.Text;
                    lblBuyerPincode.Text = txtBuyerPincode.Text;

                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                Label lblBuyerGSTIN = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerGSTIN");
                Label lblBuyerStateCode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerStateCode");
                Label lblBuyer_addr1 = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_addr1");
                Label lblBuyer_loc = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_loc");
                Label lblBuyerPincode = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerPincode");

                TextBox txtBuyerGSTIN = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerGSTIN");
                TextBox txtBuyerStateCode = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerStateCode");
                TextBox txtBuyer_addr1 = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_addr1");
                TextBox txtBuyer_loc = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_loc");
                TextBox txtBuyerPincode = (TextBox)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerPincode");

                Button btnEdit = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnEdit");
                Button btnUpdate = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnUpdate");
                Button btnCancel = (Button)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("btnCancel");

                lblBuyerGSTIN.Visible = true;
                lblBuyerStateCode.Visible = true;
                lblBuyer_addr1.Visible = true;
                lblBuyer_loc.Visible = true;
                lblBuyerPincode.Visible = true;

                txtBuyerGSTIN.Visible = false;
                txtBuyerStateCode.Visible = false;
                txtBuyer_addr1.Visible = false;
                txtBuyer_loc.Visible = false;
                txtBuyerPincode.Visible = false;

                btnEdit.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void GridEditButtonVisible()
        {
            for (int i = 0; i < gvClaimInvoice.Rows.Count; i++)
            {
                if (PSession.User.UserName.Contains("2000IT"))
                {
                    Button btnEdit = (Button)gvClaimInvoice.Rows[i].FindControl("btnEdit");
                    btnEdit.Visible = true;

                    GridView gvClaimInvoiceItem = (GridView)gvClaimInvoice.Rows[i].FindControl("gvClaimInvoiceItem");
                    for (int j = 0; j < gvClaimInvoiceItem.Rows.Count; j++)
                    {
                        Button btnEditItem = (Button)gvClaimInvoiceItem.Rows[j].FindControl("btnEditItem");
                        btnEditItem.Visible = true;
                    }

                }
            }
        }

        protected void btnEditItem_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                Button GV = (Button)sender; // Select either of inner gridview who request for Edit / cancel edit out of all innner gridview  
                GridViewRow grdParent = (GridViewRow)GV.Parent.Parent; // Find controls from parent grid view  


                Label lblHSNCode = (Label)grdParent.FindControl("lblHSNCode");
                TextBox txtHSNCode = (TextBox)grdParent.FindControl("txtHSNCode");

                Button btnEditItem = (Button)grdParent.FindControl("btnEditItem");
                Button btnUpdateItem = (Button)grdParent.FindControl("btnUpdateItem");
                Button btnCancelItem = (Button)grdParent.FindControl("btnCancelItem");

                lblHSNCode.Visible = false;
                txtHSNCode.Visible = true;

                btnEditItem.Visible = false;
                btnUpdateItem.Visible = true;
                btnCancelItem.Visible = true;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnUpdateItem_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                Button GV = (Button)sender; // Select either of inner gridview who request for Edit / cancel edit out of all innner gridview  
                GridViewRow grdParent = (GridViewRow)GV.Parent.Parent; // Find controls from parent grid view  


                Label lblBillingDocument = (Label)grdParent.FindControl("lblBillingDocument");
                Label lblInvoiceItemID = (Label)grdParent.FindControl("lblInvoiceItemID");
                Label lblHSNCode = (Label)grdParent.FindControl("lblHSNCode");
                TextBox txtHSNCode = (TextBox)grdParent.FindControl("txtHSNCode");


                string InvoiceNumber = lblBillingDocument.Text.Trim();
                long InvoiceItemID = Convert.ToInt64(lblInvoiceItemID.Text.Trim());
                string HSNCode = txtHSNCode.Text.Trim();


                if (new BDMS_EInvoice().UpdateEInvoiveHSNCode(InvoiceNumber, InvoiceItemID, HSNCode, PSession.User.UserID))
                {
                    Button btnEditItem = (Button)grdParent.FindControl("btnEditItem");
                    Button btnUpdateItem = (Button)grdParent.FindControl("btnUpdateItem");
                    Button btnCancelItem = (Button)grdParent.FindControl("btnCancelItem");

                    lblHSNCode.Visible = true;

                    txtHSNCode.Visible = false;


                    btnEditItem.Visible = true;
                    btnUpdateItem.Visible = false;
                    btnCancelItem.Visible = false;

                    lblHSNCode.Text = txtHSNCode.Text;

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnCancelItem_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                Button GV = (Button)sender; // Select either of inner gridview who request for Edit / cancel edit out of all innner gridview  
                GridViewRow grdParent = (GridViewRow)GV.Parent.Parent; // Find controls from parent grid view  

                Label lblHSNCode = (Label)grdParent.FindControl("lblHSNCode");
                TextBox txtHSNCode = (TextBox)grdParent.FindControl("txtHSNCode");

                Button btnEditItem = (Button)grdParent.FindControl("btnEditItem");
                Button btnUpdateItem = (Button)grdParent.FindControl("btnUpdateItem");
                Button btnCancelItem = (Button)grdParent.FindControl("btnCancelItem");

                lblHSNCode.Visible = true;

                txtHSNCode.Visible = false;

                btnEditItem.Visible = true;
                btnUpdateItem.Visible = false;
                btnCancelItem.Visible = false;

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
    }
}