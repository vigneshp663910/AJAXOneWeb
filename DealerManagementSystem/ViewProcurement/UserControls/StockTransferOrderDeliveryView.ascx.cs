using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class StockTransferOrderDeliveryView : System.Web.UI.UserControl
    {
        public PStockTransferOrder StockTransferOrder
        {
            get
            {
                if (ViewState["StockTransferOrderView"] == null)
                {
                    ViewState["StockTransferOrderView"] = new PStockTransferOrder();
                }
                return (PStockTransferOrder)ViewState["StockTransferOrderView"];
            }
            set
            {
                ViewState["StockTransferOrderView"] = value;
            }
        }
        public PStockTransferOrderDelivery Deliverys
        {
            get
            {
                if (ViewState["Deliverys"] == null)
                {
                    ViewState["Deliverys"] = new PStockTransferOrderDelivery();
                }
                return (PStockTransferOrderDelivery)ViewState["Deliverys"];
            }
            set
            {
                ViewState["Deliverys"] = value;
            }
        }
        public List< PStockTransferOrderItemGr_Insert> Gr_Insert
        {
            get
            {
                if (ViewState["PAsnView"] == null)
                {
                    ViewState["PAsnView"] = new List<PStockTransferOrderItemGr_Insert>();
                }
                return (List<PStockTransferOrderItemGr_Insert>)ViewState["PAsnView"];
            }
            set
            {
                ViewState["PAsnView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageRestrictedQty.Text = "";
            lblMessageGrCreation.Text = "";
            //if (Session["PurchaseOrderID"] != null)
            //{
            //    lblMessage.Text = "Purchase Order Created Number : " + PurchaseOrder.PurchaseOrderNumber;
            //    Session["PurchaseOrderID"] = null;
            //}
        }
        public void fillViewDelivery(long DeliveryID)
        {
            //PApiResult Result = new BStockTransferOrder().GetStockTransferOrderByID(StockTransferOrderID);
            //StockTransferOrder = JsonConvert.DeserializeObject<PStockTransferOrder>(JsonConvert.SerializeObject(Result.Data));

            PApiResult DeliveryResult = new BStockTransferOrder().GetStockTransferOrderDeliveryByID(null, DeliveryID);
            Deliverys = (JsonConvert.DeserializeObject< List<PStockTransferOrderDelivery>>(JsonConvert.SerializeObject(DeliveryResult.Data)))[0];

            PApiResult Result = new BStockTransferOrder().GetStockTransferOrderByID(Deliverys.StockTransferOrder.StockTransferOrderID);
            StockTransferOrder = JsonConvert.DeserializeObject<PStockTransferOrder>(JsonConvert.SerializeObject(Result.Data));


            lblPurchaseOrderNumber.Text = StockTransferOrder.StockTransferOrderNumber;
            lblPurchaseOrderDate.Text = StockTransferOrder.StockTransferOrderDate.ToString();

            lblDeliveryNumber.Text = Deliverys.DeliveryNumber;
            lblDeliveryDate.Text = Deliverys.DeliveryDate.ToString();

            lblGrNumber.Text = Deliverys.GrNumber;
            lblGrDate.Text = Convert.ToString(Deliverys.GrDate);


            lblStatus.Text = Deliverys.Status.Status;

            lblReceivingLocation.Text = StockTransferOrder.DestinationOffice.OfficeName;
            lblSourceLocation.Text = StockTransferOrder.SourceOffice.OfficeName;

            lblPORemarks.Text = StockTransferOrder.Remarks;

            lblPODealer.Text = StockTransferOrder.Dealer.DealerName;


            gvPOItem.DataSource = StockTransferOrder.Items;
            gvPOItem.DataBind();

            gvDeliveryViewItem.DataSource = Deliverys.Items;
            gvDeliveryViewItem.DataBind();
            ActionControlMange();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);

            if (lbActions.ID == "lbPDF")
            {
                ViewPurchaseOrder();
            }
            else if (lbActions.ID == "lbGrCreate")
            {
                MPE_GrCreate.Show();
                Gr_Insert = new List<PStockTransferOrderItemGr_Insert>();
                foreach (PStockTransferOrderDeliveryItem Item in Deliverys.Items)
                {
                    Gr_Insert.Add(new PStockTransferOrderItemGr_Insert()
                    {
                        DeliveryID = Deliverys.DeliveryID,
                        DealerID = Deliverys.StockTransferOrder.Dealer.DealerID,
                        DeliveryItemID = Item.DeliveryItemID,
                        MaterialCode = Item.Material.MaterialCode,
                        MaterialDescription = Item.Material.MaterialDescription,
                        Quantity = Item.DeliveryQuantity,
                        UnrestrictedQty = Item.DeliveryQuantity,
                        RestrictedQty = 0,
                        RestrictedItem = new List<PGrRestricted_Insert>()
                    });
                }
                FillGr();
            }
        } 

        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        void ActionControlMange()
        {

            //lbAddMaterial.Visible = true;
            //lbRelease.Visible = true;
            //lbCancel.Visible = true;
            //lbPDF.Visible = true;

            //int StatusID = StockTransferOrder.Status.StatusID;
            //if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Draft)
            //{
            //    lbPDF.Visible = false;
            //}
            //else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Release)
            //{
            //    lbAddMaterial.Visible = false;
            //    lbRelease.Visible = false;
            //    lbCancel.Visible = false;
            //}
            //else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_PartiallyDelivered)
            //{
            //    lbAddMaterial.Visible = false;
            //    lbRelease.Visible = false;
            //    lbCancel.Visible = false;
            //}
            //else if ((StatusID == (short)AjaxOneStatus.StockTransferOrder_Delivered) || (StatusID == (short)AjaxOneStatus.StockTransferOrder_PartiallyClosed))
            //{
            //    lbAddMaterial.Visible = false;
            //    lbRelease.Visible = false;
            //    lbCancel.Visible = false;
            //}
            //else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Cancelled)
            //{
            //    lbAddMaterial.Visible = false;
            //    lbRelease.Visible = false;
            //    lbCancel.Visible = false;
            //}

          
        }
          
        protected void lnkBtnItemAction_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;


            LinkButton lnkBtnEdit = (LinkButton)gvRow.FindControl("lnkBtnEdit");
            LinkButton lnkBtnupdate = (LinkButton)gvRow.FindControl("lnkBtnupdate");
            LinkButton lnkBtnCancel = (LinkButton)gvRow.FindControl("lnkBtnCancel");
            LinkButton lnkBtnDelete = (LinkButton)gvRow.FindControl("lnkBtnDelete");

            TextBox txtQuantity = (TextBox)gvRow.FindControl("txtQuantity");
            Label lblQuantity = (Label)gvRow.FindControl("lblQuantity");
            if (lbActions.ID == "lnkBtnEdit")
            {
                lnkBtnEdit.Visible = false;
                lnkBtnupdate.Visible = true;
                lnkBtnCancel.Visible = true;
                lnkBtnDelete.Visible = false;

                txtQuantity.Visible = true;
                lblQuantity.Visible = false;
            }
            else if (lbActions.ID == "lnkBtnCancel")
            {
                lnkBtnEdit.Visible = true;
                lnkBtnupdate.Visible = false;
                lnkBtnCancel.Visible = false;
                lnkBtnDelete.Visible = true;
                txtQuantity.Visible = false;
                lblQuantity.Visible = true;
            }
            else if (lbActions.ID == "lnkBtnDelete")
            {
                //lblMessage.Visible = true;
                //Label lblPurchaseOrderItemID = (Label)gvRow.FindControl("lblPurchaseOrderItemID");
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelPurchaseOrderItem?PurchaseOrderItemID=" + Convert.ToInt64(lblPurchaseOrderItemID.Text)));
                //if (Results.Status == PApplication.Failure)
                //{
                //    lblMessage.Text = Results.Message;
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
                //int StatusID = PurchaseOrder.PurchaseOrderStatus.ProcurementStatusID;
                //if (StatusID == (short)ProcurementStatus.PoDraft)
                //{
                //    lblMessage.Text = "Updated Successfully";
                //}
                //else
                //{
                //    lblMessage.Text = "Waiting For Cancel Approval";
                //}
                //lblMessage.ForeColor = Color.Green;
                //fillViewPO(PurchaseOrder.PurchaseOrderID);
            }

        }

        void ViewPurchaseOrder()
        {
            //try
            //{
            //    lblMessage.Text = "";
            //    PPurchaseOrder PO = PurchaseOrder;
            //    if (string.IsNullOrEmpty(PO.SaleOrderNumber))
            //    {
            //        lblMessage.Text = "SaleOrder Number Not Generated...!";
            //        lblMessage.Visible = true;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    lblMessage.Visible = true;
            //    lblMessage.ForeColor = Color.Red;
            //    string mimeType = string.Empty;
            //    Byte[] mybytes = PurchaseOrderRdlc(out mimeType);
            //    string CustomerName = (PO.Dealer.DisplayName);

            //    string FileName = (PO.Dealer.DealerCode + "_PO_" + CustomerName + "_" + Convert.ToDateTime(PO.PurchaseOrderDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
            //    var uploadPath = Server.MapPath("~/Backup");
            //    var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
            //    File.WriteAllBytes(tempfilenameandlocation, mybytes);
            //    Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Procurement » Purchase Order','_newtab');</script>");
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message.ToString();
            //    lblMessage.Visible = true;
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
        }
        //Byte[] PurchaseOrderRdlc(out string mimeType)
        //{

        //    PPurchaseOrder PO = PurchaseOrder;
        //    var CC = CultureInfo.CurrentCulture;
        //    Random r = new Random();
        //    string extension;
        //    string encoding;
        //    string[] streams;
        //    Warning[] warnings;
        //    LocalReport report = new LocalReport();
        //    report.EnableExternalImages = true;


        //    PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
        //    string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : ", " + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : ", " + Ajax.Address3)).Trim(',', ' ');
        //    string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : ", " + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

        //    PDMS_Customer Supplier = new BDMS_Customer().getCustomerAddressFromSAP(PO.Vendor.DealerCode);
        //    string SupplierAddress1 = (Supplier.Address1 + (string.IsNullOrEmpty(Supplier.Address2) ? "" : "," + Supplier.Address2) + (string.IsNullOrEmpty(Supplier.Address3) ? "" : "," + Supplier.Address3)).Trim(',', ' ');
        //    string SupplierAddress2 = (Supplier.City + (string.IsNullOrEmpty(Supplier.State.State) ? "" : "," + Supplier.State.State) + (string.IsNullOrEmpty(Supplier.Pincode) ? "" : "-" + Supplier.Pincode)).Trim(',', ' ');

        //    PDMS_Customer BillTo = new BDMS_Customer().getCustomerAddressFromSAP(PO.Dealer.DealerCode);
        //    string BillToAddress1 = (BillTo.Address1 + (string.IsNullOrEmpty(BillTo.Address2) ? "" : "," + BillTo.Address2) + (string.IsNullOrEmpty(BillTo.Address3) ? "" : "," + BillTo.Address3)).Trim(',', ' ');
        //    string BillToAddress2 = (BillTo.City + (string.IsNullOrEmpty(BillTo.State.State) ? "" : "," + BillTo.State.State) + (string.IsNullOrEmpty(BillTo.Pincode) ? "" : "-" + BillTo.Pincode)).Trim(',', ' ');


        //    //lblMessage.Visible = true;
        //    //lblMessage.ForeColor = Color.Red;



        //    ReportParameter[] P = new ReportParameter[28];
        //    P[0] = new ReportParameter("PurchaseOrderNumber", PO.PurchaseOrderNumber, false);
        //    P[1] = new ReportParameter("PurchaseOrderDate", PO.PurchaseOrderDate.ToString(), false);
        //    P[2] = new ReportParameter("SupplierName", Supplier.CustomerFullName, false);
        //    P[3] = new ReportParameter("SupplierAddress1", SupplierAddress1, false);
        //    P[4] = new ReportParameter("SupplierAddress2", SupplierAddress2, false);
        //    P[5] = new ReportParameter("SupplierMobile", Supplier.Mobile, false);
        //    P[6] = new ReportParameter("SupplierEMail", Supplier.Email, false);
        //    P[7] = new ReportParameter("BillToCustomerName", BillTo.CustomerFullName, false);
        //    P[8] = new ReportParameter("BillToCustomerAddress1", BillToAddress1, false);
        //    P[9] = new ReportParameter("BillToCustomerAddress2", BillToAddress2, false);
        //    P[10] = new ReportParameter("BillToMobile", BillTo.Mobile, false);
        //    P[11] = new ReportParameter("BillToEMail", BillTo.Email, false);
        //    P[12] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
        //    P[13] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
        //    P[14] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
        //    P[15] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
        //    P[16] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
        //    P[17] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
        //    P[18] = new ReportParameter("PurchaseOrderType", PO.PurchaseOrderType.PurchaseOrderType, false);
        //    P[19] = new ReportParameter("SaleOrderNumber", PO.SaleOrderNumber, false);
        //    P[20] = new ReportParameter("ExpectedDeliveryDate", PO.ExpectedDeliveryDate.ToString(), false);
        //    P[21] = new ReportParameter("ReceivingLocation", PO.Location.OfficeName, false);
        //    P[22] = new ReportParameter("SystemDate", DateTime.Now.ToString(), false);


        //    DataTable dtItem = new DataTable();
        //    dtItem.Columns.Add("ItemNo");
        //    dtItem.Columns.Add("PartNo");
        //    dtItem.Columns.Add("Description");
        //    dtItem.Columns.Add("Qty");
        //    dtItem.Columns.Add("UOM");
        //    dtItem.Columns.Add("UnitPrice");
        //    dtItem.Columns.Add("Taxable");
        //    dtItem.Columns.Add("Tax");
        //    dtItem.Columns.Add("Net");

        //    decimal GrandTotal = 0, TaxTotal = 0;

        //    DataTable DTMaterialText = new DataTable();
        //    foreach (PPurchaseOrderItem Item in PO.PurchaseOrderItems)
        //    {
        //        dtItem.Rows.Add(Item.POItem, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Quantity.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Material.CurrentPrice), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", Item.TaxValue), String.Format("{0:n}", (Item.TaxableValue + Item.TaxValue)));
        //        TaxTotal += Item.TaxValue;
        //        GrandTotal += (Item.TaxableValue + Item.TaxValue);
        //    }
        //    P[23] = new ReportParameter("TaxAmount", String.Format("{0:n}", TaxTotal.ToString()), false);
        //    P[24] = new ReportParameter("NetAmount", String.Format("{0:n}", GrandTotal.ToString()), false);
        //    P[25] = new ReportParameter("Remarks", PO.Remarks, false);
        //    P[26] = new ReportParameter("SupplierCode", Supplier.CustomerCode, false);
        //    P[27] = new ReportParameter("BillToCode", BillTo.CustomerCode, false);
        //    report.ReportPath = Server.MapPath("~/Print/PurchaseOrder.rdlc");
        //    report.SetParameters(P);
        //    ReportDataSource rds = new ReportDataSource();
        //    rds.Name = "PurchaseOrder";//This refers to the dataset name in the RDLC file  
        //    rds.Value = dtItem;
        //    report.DataSources.Add(rds);
        //    Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

        //    return mybytes;
        //}

        protected void btnSubmitMaterial_Click(object sender, EventArgs e)
        {
            //lblAddMaterialMessage.Text = "";
            //lblAddMaterialMessage.ForeColor = Color.Red;
            //lblAddMaterialMessage.Visible = true;
            //lblMessage.Text = "";
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Visible = true;

            //if (PurchaseOrder.PurchaseOrderItems.Any(item => item.Material.MaterialID == Convert.ToInt32(hdfMaterialID.Value)))
            //{
            //    lblAddMaterialMessage.Text = "Material Already Available...!";
            //    MPE_AddMaterial.Show();
            //    return;
            //}

            //PPurchaseOrderItem_Insert POi = new PPurchaseOrderItem_Insert();
            //POi.PurchaseOrderID = Convert.ToInt64(PurchaseOrder.PurchaseOrderID);
            //POi.Quantity = Convert.ToDecimal(txtQty.Text);
            //POi.MaterialCode = hdfMaterialCode.Value;

            //string Customer = PurchaseOrder.Dealer.DealerCode;
            //string Vendor = PurchaseOrder.Vendor.DealerCode;
            //string OrderType = PurchaseOrder.PurchaseOrderType.SapOrderType;
            //string Material = POi.MaterialCode;
            //string IV_SEC_SALES = "";
            ////string PriceDate = DateTime.Now.ToShortDateString();
            //string PriceDate = "";
            //string IsWarrenty = "false";

            //PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, OrderType, 1, Material, POi.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);
            //POi.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            //POi.Price = Mat.CurrentPrice;
            //POi.DiscountAmount = Mat.Discount;
            //POi.TaxableAmount = Mat.TaxablePrice;
            //POi.SGST = Mat.SGST;
            //POi.SGSTValue = Mat.SGSTValue;
            //POi.CGST = Mat.SGST;
            //POi.CGSTValue = Mat.SGSTValue;
            //POi.IGST = Mat.IGST;
            //POi.IGSTValue = Mat.IGSTValue;
            //POi.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
            //POi.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
            //POi.NetValue = POi.TaxableAmount + POi.SGSTValue + POi.CGSTValue + POi.IGSTValue;

            //string result = new BAPI().ApiPut("PurchaseOrder/UpdatePurchaseOrderItem", POi);
            //PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            //if (Result.Status == PApplication.Failure)
            //{
            //    lblAddMaterialMessage.Text = Result.Message;
            //    MPE_AddMaterial.Show();
            //    return;
            //}
            //lblMessage.Text = Result.Message;
            //lblMessage.ForeColor = Color.Green;
            //fillViewPO(PurchaseOrder.PurchaseOrderID);
        }
        public void FillGr()
        {  
            lblGrAsnNumber.Text = Deliverys.DeliveryNumber;
            lblGrAsnID.Text = Deliverys.DeliveryID.ToString(); 

            gvPOAsnGrItem.DataSource = Gr_Insert;
            gvPOAsnGrItem.DataBind();

            //PGr Gr = new PGr();
            // Gr.GrItemS = new List<PGrItem>();
             

            //foreach (GridViewRow row in gvPOAsnGrItem.Rows)
            //{
            //    Label lblAsnID = (Label)row.FindControl("lblAsnID");
            //    Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
            //    Label lblReceivedQty = (Label)row.FindControl("lblReceivedQty");
            //    Label lblUnrestrictedQty = (Label)row.FindControl("lblUnrestrictedQty");
            //    Label lblRestrictedQty = (Label)row.FindControl("lblRestrictedQty");

            //    Gr.GrItemS.Add(new PGrItem()
            //    {
            //        AsnItem = new PAsnItem() { AsnItemID = Convert.ToInt64(lblAsnItemID.Text.Trim()) },
            //        ReceivedQty = Convert.ToDecimal("0" + lblReceivedQty.Text),
            //        UnrestrictedQty = Convert.ToDecimal("0" + lblUnrestrictedQty.Text),
            //        RestrictedQty = Convert.ToDecimal("0" + lblRestrictedQty.Text),
            //        GrBlocked = new List<PGrBlocked>()
            //    });
            //}
        }

        protected void btnGrCreate_Click(object sender, EventArgs e)
        { 
            MPE_GrCreate.Show();
            lblMessageGrCreation.ForeColor = Color.Red; 
            Boolean Validation = ValidationGrItem();
            if (Validation)
            {
                MPE_GrCreate.Show();
                return;
            }
            PApiResult Result = new BStockTransferOrder().UpdateStockTransferOrderGr(Gr_Insert);
            if (Result.Status == PApplication.Failure)
            {
                lblMessageGrCreation.Text = Result.Message;
                return;
            }
            fillViewDelivery(Deliverys.DeliveryID);
            lblMessage.Text = Result.Message; 
            lblMessage.ForeColor = Color.Green;
            MPE_GrCreate.Hide();
        }
        public Boolean ValidationGrItem()
        {
            lblMessageGrCreation.ForeColor = Color.Red;
            lblMessageGrCreation.Visible = true;
            lblMessageGrCreation.Text = "";
            Boolean Result = false;
            foreach (GridViewRow row in gvPOAsnGrItem.Rows)
            {
                Label lblAsnID = (Label)row.FindControl("lblAsnID");
                Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
                Label lblQty = (Label)row.FindControl("lblQty");
                Label lblAsnItem = (Label)row.FindControl("lblAsnItem");

                Label lblReceivedQty = (Label)row.FindControl("lblReceivedQty");
                Label lblUnrestrictedQty = (Label)row.FindControl("lblUnrestrictedQty");
                Label lblRestrictedQty = (Label)row.FindControl("lblRestrictedQty");

                decimal ReceivedQty = 0, UnrestrictedQty = 0, RestrictedQty = 0;
                decimal.TryParse(lblReceivedQty.Text, out ReceivedQty);
                decimal.TryParse(lblUnrestrictedQty.Text, out UnrestrictedQty);
                decimal.TryParse(lblRestrictedQty.Text, out RestrictedQty);

                if (Convert.ToDecimal("0" + lblQty.Text) != ReceivedQty)
                {
                    lblMessageGrCreation.Text = "Please Equal To AsnQty with Received Qty From Item No : " + lblAsnItem.Text;
                    Result = true;
                }
                if (ReceivedQty != (UnrestrictedQty + RestrictedQty))
                {
                    lblMessageGrCreation.Text = "Please Equal To Received Qty with (UnRestricted + Restricted) From Item No : " + lblAsnItem.Text;
                    Result = true;
                }
            }
            return Result;
        }

        protected void lnkSetRestrictedQty_Click(object sender, EventArgs e)
        {
            lblMessageGrCreation.Text = string.Empty;
            lblMessageGrCreation.ForeColor = Color.Red;
            lblMessageGrCreation.Visible = true;
            LinkButton lnkSetRestrictedQty = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkSetRestrictedQty.NamingContainer);

            Label lblDeliveryItemID = (Label)row.FindControl("lblDeliveryItemID");
            Label lblDeliveryID = (Label)row.FindControl("lblDeliveryID");
            Label lblAsnItem = (Label)row.FindControl("lblAsnItem");
            Label lblReceivedQty = (Label)row.FindControl("lblReceivedQty");

            HidAsnItemID.Value = lblDeliveryItemID.Text; 
            HidReceivedQty.Value = lblReceivedQty.Text;

            txtUnrestrictedQty.Text = HidReceivedQty.Value;
            txtDamagedQty.Text = "0";
            txtMissingQty.Text = "0";

            MPE_UpdateRestrictedQty.Show();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            MPE_UpdateRestrictedQty.Show(); 
            lblMessageRestrictedQty.ForeColor = Color.Red; 
            if (string.IsNullOrEmpty(txtUnrestrictedQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please UnRestricted Quantity...!"; 
                return;
            }
            if (string.IsNullOrEmpty(txtMissingQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please Missing Quantity...!"; 
                return;
            }
            if (string.IsNullOrEmpty(txtDamagedQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please Damaged Quantity...!"; 
                return;
            }
            Decimal UnrestrictedQty = Convert.ToDecimal(txtUnrestrictedQty.Text);
            Decimal MissingQty = Convert.ToDecimal(txtMissingQty.Text);
            Decimal DamagedQty = Convert.ToDecimal(txtDamagedQty.Text);
            if (Convert.ToDecimal(HidReceivedQty.Value) != (UnrestrictedQty + MissingQty + DamagedQty))
            {
                lblMessageRestrictedQty.Text = "Received Qty Not match with (UnRestricted+Missing+Damage) Quantity...!"; 
                return;
            }
            foreach (PStockTransferOrderItemGr_Insert asn in Gr_Insert)
            {
                if (asn.DeliveryItemID == Convert.ToInt64(HidAsnItemID.Value))
                {
                    asn.Remark = txtRemark.Text;
                    asn.RestrictedItem = new List<PGrRestricted_Insert>(); 
                    if (MissingQty != 0)
                    {
                        asn.RestrictedItem.Add(new PGrRestricted_Insert() { Qty = MissingQty, RestrictedStatusID = (short)AjaxOneStatus.StockTransferOrderGrRestricted_MissingQty });
                    }
                    if (DamagedQty != 0)
                    {
                        asn.RestrictedItem.Add(new PGrRestricted_Insert() { Qty = DamagedQty, RestrictedStatusID = (short)AjaxOneStatus.StockTransferOrderGrRestricted_DamagedQty });
                    }
                    asn.UnrestrictedQty = UnrestrictedQty;
                    asn.RestrictedQty = MissingQty + DamagedQty;
                }
            } 
            gvPOAsnGrItem.DataSource = Gr_Insert;
            gvPOAsnGrItem.DataBind();
            MPE_UpdateRestrictedQty.Hide();
            MPE_GrCreate.Show();
        }
    }
}