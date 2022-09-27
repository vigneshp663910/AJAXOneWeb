using Business;
using Newtonsoft.Json;
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

        public class PSuccess
        {
            public string data { get; set; }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
           // PSuccess Data = JsonConvert.DeserializeObject<PSuccess>("{\"data\":\"{\"AckNo\":152212890354493,\"AckDt\":\"2022-09-23 17:45:00\",\"Irn\":\"dsgffrgsr\",\"SignedInvoice\":\"fsgdfgsgdfg..-gAOf7SCXZTUuHp\",\"SignedQRCode\":\"..YcJruW70p2g1TJ\",\"Status\":\"ACT\",\"EwbNo\":null,\"EwbDt\":null,\"EwbValidTill\":null,\"Remarks\":null}\"}");
           // PSuccessEInv PSuccess1 = JsonConvert.DeserializeObject<PSuccessEInv>("{\"data\": {\"AckNo\": 162210030870114,\"AckDt\": \"2022-01-10 12:21:00\",\"Irn\": \"Irn0158eb6a8b\",\"SignedInvoice\": \"SignedInvoiceuMMJAeuQ\",\"SignedQRCode\": \"SignedQRCodeFyA\",\"Status\": \"ACT\",\"EwbNo\": null,\"EwbDt\": null,\"EwbValidTill\": null,\"Remarks\": null }}");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }


        public List<PEInvoiceGrid> InvoiceGrid
        {
            get
            {
                if (Session["EInvoiceRequestEInvoiceGrid"] == null)
                {
                    Session["EInvoiceRequestEInvoiceGrid"] = new List<PEInvoiceGrid>();
                }
                return (List<PEInvoiceGrid>)Session["EInvoiceRequestEInvoiceGrid"];
            }
            set
            {
                Session["EInvoiceRequestEInvoiceGrid"] = value;
            }
        }

        //public List<PEInvoice> EMarketingInvoice
        //{
        //    get
        //    {
        //        if (Session["EInvoiceRequestEMarketingInvoice"] == null)
        //        {
        //            Session["EInvoiceRequestEMarketingInvoice"] = new List<PEInvoice>();
        //        }
        //        return (List<PEInvoice>)Session["EInvoiceRequestEMarketingInvoice"];
        //    }
        //    set
        //    {
        //        Session["EInvoiceRequestEMarketingInvoice"] = value;
        //    }
        //}
        //public List<PEInvoice> EPayInvoice
        //{
        //    get
        //    {
        //        if (Session["EInvoiceRequestEPayInvoice"] == null)
        //        {
        //            Session["EInvoiceRequestEPayInvoice"] = new List<PEInvoice>();
        //        }
        //        return (List<PEInvoice>)Session["EInvoiceRequestEPayInvoice"];
        //    }
        //    set
        //    {
        //        Session["EInvoiceRequestEPayInvoice"] = value;
        //    }
        //}
        //public List<PEInvoice> EWarrInvoice
        //{
        //    get
        //    {
        //        if (Session["EInvoiceRequestEWarrInvoice"] == null)
        //        {
        //            Session["EInvoiceRequestEWarrInvoice"] = new List<PEInvoice>();
        //        }
        //        return (List<PEInvoice>)Session["EInvoiceRequestEWarrInvoice"];
        //    }
        //    set
        //    {
        //        Session["EInvoiceRequestEWarrInvoice"] = value;
        //    }
        //}

        //public List<PEInvoice> ESalesComInvoice
        //{
        //    get
        //    {
        //        if (Session["EInvoiceRequestESalesComInvoice"] == null)
        //        {
        //            Session["EInvoiceRequestESalesComInvoice"] = new List<PEInvoice>();
        //        }
        //        return (List<PEInvoice>)Session["EInvoiceRequestESalesComInvoice"];
        //    }
        //    set
        //    {
        //        Session["EInvoiceRequestESalesComInvoice"] = value;
        //    }
        //}

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

                InvoiceGrid = new List<PEInvoiceGrid>();
                List<PDMS_PaidServiceInvoice> Paidinvs = new BDMS_EInvoice().GetPaidServiceInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);
                foreach (PDMS_PaidServiceInvoice inv in Paidinvs)
                {
                    InvoiceGrid.Add(new PEInvoiceGrid() { EInvoice = new BDMS_EInvoice().ConvertPaidServiceInvoice(inv), InvType = "PAY" });
                } 

                List<PDMS_WarrantyClaimInvoice> Pinv = new BDMS_EInvoice().getActivityInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID);
                foreach (PDMS_WarrantyClaimInvoice inv in Pinv)
                {
                    InvoiceGrid.Add(new PEInvoiceGrid() { EInvoice = new BDMS_EInvoice().ConvertActivityInvoice(inv), InvType = "ATY" });
                }

                List<PDMS_WarrantyClaimInvoice> PinvW = new BDMS_EInvoice().getWarrantyClaimInvoiceForRequestEInvoice(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID,"");
                foreach (PDMS_WarrantyClaimInvoice inv in PinvW)
                {
                    InvoiceGrid.Add(new PEInvoiceGrid() { EInvoice = new BDMS_EInvoice().ConvertActivityInvoice(inv), InvType = "WARR" });
                }

                gvInv.PageIndex = 0;
                gvInv.DataSource = InvoiceGrid;
                gvInv.DataBind();
                lblRowCount.Text = (((gvInv.PageIndex) * gvInv.PageSize) + 1) + " - " + (((gvInv.PageIndex) * gvInv.PageSize) + gvInv.Rows.Count) + " of " + InvoiceGrid.Count;


                //  EWarrInvoice = new BDMS_EInvoice().GetInvoiceForRequestEInvoice_New(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);
                //  ESalesComInvoice = new BDMS_EInvoice().GetInvoiceForRequestEInvoice_New(InvoiceNumber, InvoiceDateF, InvoiceDateT, DealerID, CustomerCode);


                //gvPayInvoice.PageIndex = 0;
                //gvPayInvoice.DataSource = EPayInvoice;
                //gvPayInvoice.DataBind();
                //lblRowCount.Text = (((gvPayInvoice.PageIndex) * gvPayInvoice.PageSize) + 1) + " - " + (((gvPayInvoice.PageIndex) * gvPayInvoice.PageSize) + gvPayInvoice.Rows.Count) + " of " + EPayInvoice.Count;
                 

                //gvMarketingInv.PageIndex = 0;
                //gvMarketingInv.DataSource = EMarketingInvoice;
                //gvMarketingInv.DataBind();
                //lblRowCount.Text = (((gvMarketingInv.PageIndex) * gvMarketingInv.PageSize) + 1) + " - " + (((gvMarketingInv.PageIndex) * gvMarketingInv.PageSize) + gvMarketingInv.Rows.Count) + " of " + EMarketingInvoice.Count;


                //  GridEditButtonVisible();
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
         
         
        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label InvoiceNumber = (Label)gvInv.Rows[gvRow.RowIndex].FindControl("lblBillingDocument");
                Label lblInvType = (Label)gvInv.Rows[gvRow.RowIndex].FindControl("lblInvType");
                //  PEInvoice EInvoice = new BDMS_EInvoice().GetInvoiceForRequestEInvoice_New(InvoiceNumber.Text, null, null, null, null)[0];

                lblMessage.Text = new BDMS_EInvoice().GeneratEInvoice(InvoiceNumber.Text, lblInvType.Text);
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
         
        void GridEditButtonVisible()
        {
            for (int i = 0; i < gvInv.Rows.Count; i++)
            {
                if (PSession.User.UserName.Contains("2000IT"))
                {
                    Button btnEdit = (Button)gvInv.Rows[i].FindControl("btnEdit");
                    btnEdit.Visible = true;

                    GridView gvClaimInvoiceItem = (GridView)gvInv.Rows[i].FindControl("gvClaimInvoiceItem");
                    for (int j = 0; j < gvClaimInvoiceItem.Rows.Count; j++)
                    {
                        Button btnEditItem = (Button)gvClaimInvoiceItem.Rows[j].FindControl("btnEditItem");
                        btnEditItem.Visible = true;
                    }

                }
            }
        }
          
      
        protected void gvInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //  string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
                    Label lblBillingDocument = (Label)e.Row.FindControl("lblBillingDocument");
                    List<PItemList> Lines = new List<PItemList>();
                    Lines = InvoiceGrid.Find(s => s.EInvoice.DocDtls.No == lblBillingDocument.Text).EInvoice.ItemList;
                    gvClaimInvoiceItem.DataSource = Lines;
                    gvClaimInvoiceItem.DataBind();

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
            }
        }

        protected void gvInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInv.PageIndex = e.NewPageIndex;
            gvInv.DataSource = InvoiceGrid;
            gvInv.DataBind();
            lblRowCount.Text = (((gvInv.PageIndex) * gvInv.PageSize) + 1) + " - " + (((gvInv.PageIndex) * gvInv.PageSize) + gvInv.Rows.Count) + " of " + InvoiceGrid.Count;
            GridEditButtonVisible();
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

        //protected void gvPayInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    DateTime traceStartTime = DateTime.Now;
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            //  string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
        //            GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
        //            Label lblBillingDocument = (Label)e.Row.FindControl("lblBillingDocument");
        //            List<PItemList> Lines = new List<PItemList>();
        //            Lines = EPayInvoice.Find(s => s.DocDtls.No == lblBillingDocument.Text).ItemList;
        //            gvClaimInvoiceItem.DataSource = Lines;
        //            gvClaimInvoiceItem.DataBind();

        //        }
        //        TraceLogger.Log(traceStartTime);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //protected void gvPayInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvPayInvoice.PageIndex = e.NewPageIndex;
        //    gvPayInvoice.DataSource = EPayInvoice;
        //    gvPayInvoice.DataBind();
        //    lblRowCount.Text = (((gvPayInvoice.PageIndex) * gvPayInvoice.PageSize) + 1) + " - " + (((gvPayInvoice.PageIndex) * gvPayInvoice.PageSize) + gvPayInvoice.Rows.Count) + " of " + EPayInvoice.Count;
        //    GridEditButtonVisible();
        //}

        //protected void gvMarketingInv_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    DateTime traceStartTime = DateTime.Now;
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            //  string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
        //            GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");
        //            Label lblBillingDocument = (Label)e.Row.FindControl("lblBillingDocument");
        //            List<PItemList> Lines = new List<PItemList>();
        //            Lines = EMarketingInvoice.Find(s => s.DocDtls.No == lblBillingDocument.Text).ItemList;
        //            gvClaimInvoiceItem.DataSource = Lines;
        //            gvClaimInvoiceItem.DataBind();

        //        }
        //        TraceLogger.Log(traceStartTime);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //protected void gvMarketingInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvMarketingInv.PageIndex = e.NewPageIndex;
        //    gvMarketingInv.DataSource = EPayInvoice;
        //    gvMarketingInv.DataBind();
        //    lblRowCount.Text = (((gvMarketingInv.PageIndex) * gvMarketingInv.PageSize) + 1) + " - " + (((gvMarketingInv.PageIndex) * gvMarketingInv.PageSize) + gvMarketingInv.Rows.Count) + " of " + EMarketingInvoice.Count;
        //    GridEditButtonVisible();
        //}

        //protected void btnEditItem_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    try
        //    {
        //        Button GV = (Button)sender; // Select either of inner gridview who request for Edit / cancel edit out of all innner gridview  
        //        GridViewRow grdParent = (GridViewRow)GV.Parent.Parent; // Find controls from parent grid view  


        //        Label lblHSNCode = (Label)grdParent.FindControl("lblHSNCode");
        //        TextBox txtHSNCode = (TextBox)grdParent.FindControl("txtHSNCode");

        //        Button btnEditItem = (Button)grdParent.FindControl("btnEditItem");
        //        Button btnUpdateItem = (Button)grdParent.FindControl("btnUpdateItem");
        //        Button btnCancelItem = (Button)grdParent.FindControl("btnCancelItem");

        //        lblHSNCode.Visible = false;
        //        txtHSNCode.Visible = true;

        //        btnEditItem.Visible = false;
        //        btnUpdateItem.Visible = true;
        //        btnCancelItem.Visible = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void btnUpdateItem_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    try
        //    {
        //        Button GV = (Button)sender; // Select either of inner gridview who request for Edit / cancel edit out of all innner gridview  
        //        GridViewRow grdParent = (GridViewRow)GV.Parent.Parent; // Find controls from parent grid view  


        //        Label lblBillingDocument = (Label)grdParent.FindControl("lblBillingDocument");
        //        Label lblInvoiceItemID = (Label)grdParent.FindControl("lblInvoiceItemID");
        //        Label lblHSNCode = (Label)grdParent.FindControl("lblHSNCode");
        //        TextBox txtHSNCode = (TextBox)grdParent.FindControl("txtHSNCode");


        //        string InvoiceNumber = lblBillingDocument.Text.Trim();
        //        long InvoiceItemID = Convert.ToInt64(lblInvoiceItemID.Text.Trim());
        //        string HSNCode = txtHSNCode.Text.Trim();


        //        if (new BDMS_EInvoice().UpdateEInvoiveHSNCode(InvoiceNumber, InvoiceItemID, HSNCode, PSession.User.UserID))
        //        {
        //            Button btnEditItem = (Button)grdParent.FindControl("btnEditItem");
        //            Button btnUpdateItem = (Button)grdParent.FindControl("btnUpdateItem");
        //            Button btnCancelItem = (Button)grdParent.FindControl("btnCancelItem");

        //            lblHSNCode.Visible = true;

        //            txtHSNCode.Visible = false;


        //            btnEditItem.Visible = true;
        //            btnUpdateItem.Visible = false;
        //            btnCancelItem.Visible = false;

        //            lblHSNCode.Text = txtHSNCode.Text;

        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void btnCancelItem_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    try
        //    {
        //        Button GV = (Button)sender; // Select either of inner gridview who request for Edit / cancel edit out of all innner gridview  
        //        GridViewRow grdParent = (GridViewRow)GV.Parent.Parent; // Find controls from parent grid view  

        //        Label lblHSNCode = (Label)grdParent.FindControl("lblHSNCode");
        //        TextBox txtHSNCode = (TextBox)grdParent.FindControl("txtHSNCode");

        //        Button btnEditItem = (Button)grdParent.FindControl("btnEditItem");
        //        Button btnUpdateItem = (Button)grdParent.FindControl("btnUpdateItem");
        //        Button btnCancelItem = (Button)grdParent.FindControl("btnCancelItem");

        //        lblHSNCode.Visible = true;

        //        txtHSNCode.Visible = false;

        //        btnEditItem.Visible = true;
        //        btnUpdateItem.Visible = false;
        //        btnCancelItem.Visible = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void btnEdit_Click(object sender, EventArgs e)
        //{

        //    lblMessage.Visible = true;
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

        //        Label lblBuyerGSTIN = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerGSTIN");
        //        Label lblBuyerStateCode = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerStateCode");
        //        Label lblBuyer_addr1 = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_addr1");
        //        Label lblBuyer_loc = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_loc");
        //        Label lblBuyerPincode = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerPincode");

        //        TextBox txtBuyerGSTIN = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerGSTIN");
        //        TextBox txtBuyerStateCode = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerStateCode");
        //        TextBox txtBuyer_addr1 = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_addr1");
        //        TextBox txtBuyer_loc = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_loc");
        //        TextBox txtBuyerPincode = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerPincode");

        //        Button btnEdit = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnEdit");
        //        Button btnUpdate = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnUpdate");
        //        Button btnCancel = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnCancel");

        //        lblBuyerGSTIN.Visible = false;
        //        lblBuyerStateCode.Visible = false;
        //        lblBuyer_addr1.Visible = false;
        //        lblBuyer_loc.Visible = false;
        //        lblBuyerPincode.Visible = false;

        //        txtBuyerGSTIN.Visible = true;
        //        txtBuyerStateCode.Visible = true;
        //        txtBuyer_addr1.Visible = true;
        //        txtBuyer_loc.Visible = true;
        //        txtBuyerPincode.Visible = true;

        //        btnEdit.Visible = false;
        //        btnUpdate.Visible = true;
        //        btnCancel.Visible = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }

        //}

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        Label lblBillingDocument = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBillingDocument");

        //        Label lblBuyerGSTIN = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerGSTIN");
        //        Label lblBuyerStateCode = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerStateCode");
        //        Label lblBuyer_addr1 = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_addr1");
        //        Label lblBuyer_loc = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_loc");
        //        Label lblBuyerPincode = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerPincode");

        //        TextBox txtBuyerGSTIN = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerGSTIN");
        //        TextBox txtBuyerStateCode = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerStateCode");
        //        TextBox txtBuyer_addr1 = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_addr1");
        //        TextBox txtBuyer_loc = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_loc");
        //        TextBox txtBuyerPincode = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerPincode");


        //        string InvoiceNumber = lblBillingDocument.Text.Trim();
        //        string BuyerGSTIN = txtBuyerGSTIN.Text.Trim();
        //        string BuyerStateCode = txtBuyerStateCode.Text.Trim();
        //        string Buyer_addr1 = txtBuyer_addr1.Text.Trim(); ;
        //        string Buyer_loc = txtBuyer_loc.Text.Trim();
        //        string BuyerPincode = txtBuyerPincode.Text.Trim();

        //        if (new BDMS_EInvoice().UpdateEInvoiveBuyerDetails(InvoiceNumber, BuyerGSTIN, BuyerStateCode, Buyer_addr1, Buyer_loc, BuyerPincode, PSession.User.UserID))
        //        {
        //            Button btnEdit = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnEdit");
        //            Button btnUpdate = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnUpdate");
        //            Button btnCancel = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnCancel");

        //            lblBuyerGSTIN.Visible = true;
        //            lblBuyerStateCode.Visible = true;
        //            lblBuyer_addr1.Visible = true;
        //            lblBuyer_loc.Visible = true;
        //            lblBuyerPincode.Visible = true;

        //            txtBuyerGSTIN.Visible = false;
        //            txtBuyerStateCode.Visible = false;
        //            txtBuyer_addr1.Visible = false;
        //            txtBuyer_loc.Visible = false;
        //            txtBuyerPincode.Visible = false;

        //            btnEdit.Visible = true;
        //            btnUpdate.Visible = false;
        //            btnCancel.Visible = false;

        //            lblBuyerGSTIN.Text = txtBuyerGSTIN.Text;
        //            lblBuyerStateCode.Text = txtBuyerStateCode.Text;
        //            lblBuyer_addr1.Text = txtBuyer_addr1.Text;
        //            lblBuyer_loc.Text = txtBuyer_loc.Text;
        //            lblBuyerPincode.Text = txtBuyerPincode.Text;

        //        }
        //        else
        //        {

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

        //        Label lblBuyerGSTIN = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerGSTIN");
        //        Label lblBuyerStateCode = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerStateCode");
        //        Label lblBuyer_addr1 = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_addr1");
        //        Label lblBuyer_loc = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyer_loc");
        //        Label lblBuyerPincode = (Label)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("lblBuyerPincode");

        //        TextBox txtBuyerGSTIN = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerGSTIN");
        //        TextBox txtBuyerStateCode = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerStateCode");
        //        TextBox txtBuyer_addr1 = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_addr1");
        //        TextBox txtBuyer_loc = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyer_loc");
        //        TextBox txtBuyerPincode = (TextBox)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("txtBuyerPincode");

        //        Button btnEdit = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnEdit");
        //        Button btnUpdate = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnUpdate");
        //        Button btnCancel = (Button)gvPayInvoice.Rows[gvRow.RowIndex].FindControl("btnCancel");

        //        lblBuyerGSTIN.Visible = true;
        //        lblBuyerStateCode.Visible = true;
        //        lblBuyer_addr1.Visible = true;
        //        lblBuyer_loc.Visible = true;
        //        lblBuyerPincode.Visible = true;

        //        txtBuyerGSTIN.Visible = false;
        //        txtBuyerStateCode.Visible = false;
        //        txtBuyer_addr1.Visible = false;
        //        txtBuyer_loc.Visible = false;
        //        txtBuyerPincode.Visible = false;

        //        btnEdit.Visible = true;
        //        btnUpdate.Visible = false;
        //        btnCancel.Visible = false;

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}
    }
}