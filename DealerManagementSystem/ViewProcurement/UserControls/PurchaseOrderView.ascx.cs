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
    public partial class PurchaseOrderView : System.Web.UI.UserControl
    {
        public PPurchaseOrder PurchaseOrder
        {
            get
            {
                if (ViewState["PPurchaseOrder"] == null)
                {
                    ViewState["PPurchaseOrder"] = new PPurchaseOrder();
                }
                return (PPurchaseOrder)ViewState["PPurchaseOrder"];
            }
            set
            {
                ViewState["PPurchaseOrder"] = value;
            }
        }
        public List<PAsn> Asns
        {
            get
            {
                if (ViewState["Asns"] == null)
                {
                    ViewState["Asns"] = new List<PAsn>();
                }
                return (List<PAsn>)ViewState["Asns"];
            }
            set
            {
                ViewState["Asns"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblAddMaterialMessage.Text = "";
            if (Session["PurchaseOrderID"] != null)
            {
                lblMessage.Text = "Purchase Order Created Number  ";// + PurchaseOrder.PurchaseOrderNumber;
                lblMessage.ForeColor = Color.Green;
                Session["PurchaseOrderID"] = null;
            }
        }
        public void fillViewPO(long PurchaseOrderID)
        {
            PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderByID(PurchaseOrderID);

            Asns = new BDMS_PurchaseOrder().GetPurchaseOrderAsnByID(PurchaseOrderID, null);

            lblPurchaseOrderNumber.Text = PurchaseOrder.PurchaseOrderNumber;
            lblPurchaseOrderDate.Text = PurchaseOrder.PurchaseOrderDate.ToString();
            lblSoNumber.Text = PurchaseOrder.SaleOrderNumber;
            // lblSoDate.Text = PurchaseOrder..ToString();
            lblStatus.Text = PurchaseOrder.PurchaseOrderStatus.ProcurementStatus;
            lblDivision.Text = PurchaseOrder.Division.DivisionCode;
            hdfDivisionID.Value = PurchaseOrder.Division.DivisionID.ToString();
            lblRefNo.Text = PurchaseOrder.ReferenceNo;


            lblOrderType.Text = PurchaseOrder.PurchaseOrderType.PurchaseOrderType;
            lblReceivingLocation.Text = PurchaseOrder.Location.OfficeName;
            lblPORemarks.Text = PurchaseOrder.Remarks;

            lblPODealer.Text = PurchaseOrder.Dealer.DealerCode + " " + PurchaseOrder.Dealer.DealerName;
            lblPOVendor.Text = PurchaseOrder.Vendor.DealerCode + " " + PurchaseOrder.Vendor.DealerName;
            lblExpectedDeliveryDate.Text = PurchaseOrder.ExpectedDeliveryDate.ToShortDateString();
            lblOrderTo.Text = PurchaseOrder.PurchaseOrderTo.PurchaseOrderTo.ToString();
            lblCreatedBy.Text = PurchaseOrder.Created.ContactName;
            lblCancelledBy.Text = (PurchaseOrder.Cancelled == null) ? "" : PurchaseOrder.Cancelled.ContactName;
            lblCancelledOn.Text = (PurchaseOrder.CancelledOn == null) ? "" : PurchaseOrder.CancelledOn.ToString();

            gvPOItem.DataSource = PurchaseOrder.PurchaseOrderItems;
            gvPOItem.DataBind();
            hdfItemCount.Value = PurchaseOrder.PurchaseOrderItems.Count().ToString();
            decimal Price = 0, Discount = 0, TaxableAmount = 0, TaxAmount = 0;
            foreach (PPurchaseOrderItem Item in PurchaseOrder.PurchaseOrderItems)
            {
                Price = Price + Item.Price;
                Discount = Discount + Item.Discount;
                TaxableAmount = TaxableAmount + Item.TaxableValue;
                TaxAmount = TaxAmount + Item.TaxValue;
            }
            //lblPrice.Text = Price.ToString();
            lblDiscount.Text = Discount.ToString();
            lblTaxableAmount.Text = TaxableAmount.ToString();
            lblTaxAmount.Text = TaxAmount.ToString();
            lblGrossAmount.Text = PurchaseOrder.NetAmount.ToString();

            gvPAsn.DataSource = Asns;
            gvPAsn.DataBind();
            ActionControlMange();

        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbReleasePO")
            {


                int DealerID = PurchaseOrder.Dealer.DealerID;
                PApiResult Result = new BDMS_PurchaseOrder().GetDealerStockOrderControl(DealerID, null, null);
                List<PDealerStockOrderControl> DealerStockOrderControlList = JsonConvert.DeserializeObject<List<PDealerStockOrderControl>>(JsonConvert.SerializeObject(Result.Data));

                if (PurchaseOrder.PurchaseOrderType.PurchaseOrderTypeID == 1)
                {
                    if (Convert.ToDecimal(lblTaxableAmount.Text) < Convert.ToDecimal(DealerStockOrderControlList[0].MinimumValue))
                    {
                        lblMessage.Text = "Taxable Value is Less than stock order defined value ("+ DealerStockOrderControlList[0].MinimumValue + ")";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }

                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/ReleasePurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (PurchaseOrder.PurchaseOrderType.PurchaseOrderTypeID == (short)PurchaseOrderType.MachineOrder)
                {
                    lblMessage.Text = "Waiting For Release Approval";
                }
                else
                {
                    lblMessage.Text = "Updated Successfully";
                }

                lblMessage.ForeColor = Color.Green;
                fillViewPO(PurchaseOrder.PurchaseOrderID);
            }
            else if (lbActions.ID == "Edit PO")
            {

            }
            else if (lbActions.ID == "lbCancelPO")
            {
                lblMessage.ForeColor = Color.Red;

                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnHeader(null, null, null, null, null, null, (short)ProcurementStatus.AsnGRPending, PurchaseOrder.PurchaseOrderNumber, null, null, null, 1, 10);
                List<PAsn>  PAsnHeader = JsonConvert.DeserializeObject<List<PAsn>>(JsonConvert.SerializeObject(Result.Data)); 
                if (PAsnHeader.Count != 0)
                {
                    lblMessage.Text = "ASN Pending for GR so that you cannot cancel the PO";
                    return;
                }
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelPurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message; 
                    return;
                }
                int StatusID = PurchaseOrder.PurchaseOrderStatus.ProcurementStatusID;
                if (StatusID == (short)ProcurementStatus.PoDraft)
                {
                    lblMessage.Text = "Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Waiting For Cancel Approval";
                }
                lblMessage.ForeColor = Color.Green;
                fillViewPO(PurchaseOrder.PurchaseOrderID);
            }
            else if (lbActions.ID == "lbReleaseApprove")
            {

                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/ReleaseApprovePurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Text = "Updated Successfully";
                lblMessage.ForeColor = Color.Green;
                fillViewPO(PurchaseOrder.PurchaseOrderID);
            }
            else if (lbActions.ID == "lbCancelApprove")
            {
                lblMessage.ForeColor = Color.Red;
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnHeader(null, null, null, null, null, null, (short)ProcurementStatus.AsnGRPending, PurchaseOrder.PurchaseOrderNumber, null, null, null, 1, 10);
                List<PAsn> PAsnHeader = JsonConvert.DeserializeObject<List<PAsn>>(JsonConvert.SerializeObject(Result.Data));
                if (PAsnHeader.Count != 0)
                {
                    lblMessage.Text = "ASN Pending for GR so that you cannot cancel the PO";
                    return;
                }

                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelApprovePurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message; 
                    return;
                }

                lblMessage.Text = "Updated Successfully";
                lblMessage.ForeColor = Color.Green;
                fillViewPO(PurchaseOrder.PurchaseOrderID);
            }
            else if (lbActions.ID == "lbViewPurchaseOrder")
            {
                ViewPurchaseOrder();
            }
            else if (lbActions.ID == "lbDownloadPurchaseOrder")
            {
                DownloadPurchaseOrder();
            }
            else if (lbActions.ID == "lbAddMaterial")
            {
                txtMaterial.Text = "";
                hdfMaterialID.Value = "0";
                hdfMaterialCode.Value = "";
                txtQty.Text = "";
                MPE_AddMaterial.Show();
            }
        }
        void ActionControlMange()
        {
            lbAddMaterial.Visible = true;
            lbReleasePO.Visible = true;
            lbReleaseApprove.Visible = true;
            lbCancelPO.Visible = true;
            lbCancelApprove.Visible = true;
            gvPOItem.Columns[15].Visible = true;

            int StatusID = PurchaseOrder.PurchaseOrderStatus.ProcurementStatusID;
            if (StatusID == (short)ProcurementStatus.PoDraft)
            {
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReleased)
            {
                lbAddMaterial.Visible = false;
                lbReleasePO.Visible = false;
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
                gvPOItem.Columns[15].Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoPartialReceived)
            {
                lbAddMaterial.Visible = false;
                lbReleasePO.Visible = false;
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;

                gvPOItem.Columns[15].Visible = false;
            }
            else if ((StatusID == (short)ProcurementStatus.PoCompleted)
               || (StatusID == (short)ProcurementStatus.PoForceClosed) || (StatusID == (short)ProcurementStatus.PoCancelld))
            {
                lbAddMaterial.Visible = false;
                lbReleasePO.Visible = false;
                lbCancelPO.Visible = false;
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
                gvPOItem.Columns[15].Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoWaitingForReleaseApproval)
            {
                lbAddMaterial.Visible = false;
                lbReleasePO.Visible = false;
                lbCancelPO.Visible = false;
                lbCancelApprove.Visible = false;

                gvPOItem.Columns[15].Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoWaitingForCancelApproval)
            {
                lbAddMaterial.Visible = false;
                lbReleasePO.Visible = false;
                lbCancelPO.Visible = false;
                lbReleaseApprove.Visible = false;

                gvPOItem.Columns[15].Visible = false;
            }

            int PurchaseOrderTypeID = PurchaseOrder.PurchaseOrderType.PurchaseOrderTypeID;
            if (PurchaseOrderTypeID == (short)PurchaseOrderType.WarrantyOrder || PurchaseOrderTypeID == (short)PurchaseOrderType.IntraDealerOrder)
            {
                lbAddMaterial.Visible = false;
                gvPOItem.Columns[15].Visible = false;
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCreate).Count() == 0)
            {
                lbAddMaterial.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderReleasePO).Count() == 0)
            {
                lbReleasePO.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCancelPO).Count() == 0)
            {
                lbCancelPO.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderReleaseApprove).Count() == 0)
            {
                lbReleaseApprove.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCancelApprove).Count() == 0)
            {
                lbCancelApprove.Visible = false;
            }
        }
        protected void btnCancelPoItem_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblPurchaseOrderItemID = (Label)gvRow.FindControl("lblPurchaseOrderItemID");
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelPurchaseOrderItem?PurchaseOrderItemID=" + lblPurchaseOrderItemID.Text));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.ForeColor = Color.Green;
            fillViewPO(PurchaseOrder.PurchaseOrderID);
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
                txtQuantity.Text = lblQuantity.Text;
            }
            else if (lbActions.ID == "lnkBtnupdate")
            {
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtQuantity.Text))
                {
                    lblMessage.Text = "Please Enter the Quantity.";
                    return;
                }
                decimal value;
                if (!decimal.TryParse(txtQuantity.Text, out value))
                {
                    lblMessage.Text = "Please enter correct format in Qty";
                    return;
                }
                if (value < 1)
                {
                    lblMessage.Text = "Please enter qty more than zero";
                    return;
                }
                if (Convert.ToDecimal(txtQuantity.Text) < 1)
                {
                    lblMessage.Text = "Please Enter Quantity greater than zero.";
                    return;
                }

                Label lblMaterial = (Label)gvRow.FindControl("lblMaterial");
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, lblMaterial.Text, null, null, null);
                if (PurchaseOrder.PurchaseOrderType.PurchaseOrderTypeID == (short)PurchaseOrderType.MachineOrder)
                {
                    if (Convert.ToDecimal(txtQuantity.Text) != 1 && Materials[0].MaterialType == "FERT")
                    {
                        lblMessage.Text = "In machine Order you allowed to add one quantity for FERT material : " + Materials[0].MaterialCode;
                        return;
                    }
                }

                Label lblPurchaseOrderItemID = (Label)gvRow.FindControl("lblPurchaseOrderItemID");
                PPurchaseOrderItem_Insert POi = new PPurchaseOrderItem_Insert();
                POi.PurchaseOrderID = PurchaseOrder.PurchaseOrderID;
                POi.PurchaseOrderItemID = Convert.ToInt64(lblPurchaseOrderItemID.Text);
                POi.Quantity = Convert.ToDecimal(txtQuantity.Text);
                POi.MaterialCode = lblMaterial.Text;

                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = PurchaseOrder.Dealer.DealerCode;
                MaterialPrice.Vendor = PurchaseOrder.Vendor.DealerCode;
                MaterialPrice.OrderType = PurchaseOrder.PurchaseOrderType.SapOrderType;
                MaterialPrice.Division = PurchaseOrder.Division.DivisionCode;
                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                {
                    ItemNo = "10",
                    Material = POi.MaterialCode,
                    Quantity = POi.Quantity
                });

                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                PMaterial Mat = Mats[0];
                POi.Price = Mat.CurrentPrice;
                POi.DiscountAmount = Mat.Discount;
                POi.TaxableAmount = Mat.TaxablePrice;
                POi.SGST = Mat.SGST;
                POi.SGSTValue = Mat.SGSTValue;
                POi.CGST = Mat.SGST;
                POi.CGSTValue = Mat.SGSTValue;
                POi.IGST = Mat.IGST;
                POi.IGSTValue = Mat.IGSTValue;
                POi.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
                POi.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
                POi.NetValue = POi.TaxableAmount + POi.SGSTValue + POi.CGSTValue + POi.IGSTValue;

                string result = new BAPI().ApiPut("PurchaseOrder/UpdatePurchaseOrderItem", POi);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewPO(PurchaseOrder.PurchaseOrderID);
                lnkBtnEdit.Visible = true;
                lnkBtnupdate.Visible = false;
                lnkBtnCancel.Visible = false;
                lnkBtnDelete.Visible = true;

                txtQuantity.Visible = false;
                lblQuantity.Visible = true;
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
                lblMessage.ForeColor = Color.Red;
                Label lblPurchaseOrderItemID = (Label)gvRow.FindControl("lblPurchaseOrderItemID");
                string Material = ((Label)gvRow.FindControl("lblMaterial")).Text.Trim();
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null);
                if (PurchaseOrder.PurchaseOrderItems.Any(item => "FERT" == Materials[0].MaterialType && item.Material.MaterialCode == Material))
                {
                    if (PurchaseOrder.PurchaseOrderItems.Count() != 1)
                    {
                        lblMessage.Text = "First remove other materials then remove FERT material : " + Materials[0].MaterialCode;
                        return;
                    }
                }

                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelPurchaseOrderItem?PurchaseOrderItemID=" + Convert.ToInt64(lblPurchaseOrderItemID.Text)));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                int StatusID = PurchaseOrder.PurchaseOrderStatus.ProcurementStatusID;
                if (StatusID == (short)ProcurementStatus.PoDraft)
                {
                    lblMessage.Text = "Updated Successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Waiting For Cancel Approval";
                }

                fillViewPO(PurchaseOrder.PurchaseOrderID);
            }
        }

        protected void gvPAsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblAsnID = (Label)e.Row.FindControl("lblAsnID");
                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvAsnItem");
                    List<PAsnItem> Lines = new List<PAsnItem>();
                    Lines = Asns.Find(s => s.AsnID == Convert.ToInt64(lblAsnID.Text)).AsnItemS;
                    gvClaimInvoiceItem.DataSource = Lines;
                    gvClaimInvoiceItem.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
        void ViewPurchaseOrder()
        {
            try
            {
                PPurchaseOrder PO = PurchaseOrder;
                //if (string.IsNullOrEmpty(PO.SaleOrderNumber))
                //{
                //    lblMessage.Text = "SaleOrder Number Not Generated...!";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
                string mimeType = string.Empty;
                Byte[] mybytes = PurchaseOrderRdlc(out mimeType);
                string FileName = PurchaseOrder.PurchaseOrderNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Procurement » Purchase Order','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] PurchaseOrderRdlc(out string mimeType)
        {
            PPurchaseOrder PO = PurchaseOrder;
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Supplier = new BDealer().GetDealerAddress(PO.Vendor.DealerID)[0];
            string SupplierAddress1 = (Supplier.Address.Address1 + (string.IsNullOrEmpty(Supplier.Address.Address2) ? "" : "," + Supplier.Address.Address2) + (string.IsNullOrEmpty(Supplier.Address.Address3) ? "" : "," + Supplier.Address.Address3)).Trim(',', ' ');
            string SupplierAddress2 = (Supplier.Address.City + (string.IsNullOrEmpty(Supplier.Address.State.State) ? "" : "," + Supplier.Address.State.State) + (string.IsNullOrEmpty(Supplier.Address.Pincode) ? "" : "-" + Supplier.Address.Pincode)).Trim(',', ' ');

            PDMS_DealerOffice BillTo = new BDMS_Dealer().GetDealerOffice(PO.Dealer.DealerID, PO.Location.OfficeID, null)[0];
            string BillToAddress1 = (BillTo.Address1 + (string.IsNullOrEmpty(BillTo.Address2) ? "" : "," + BillTo.Address2) + (string.IsNullOrEmpty(BillTo.Address3) ? "" : "," + BillTo.Address3)).Trim(',', ' ');
            string BillToAddress2 = (BillTo.City + (string.IsNullOrEmpty(BillTo.State) ? "" : "," + BillTo.State) + (string.IsNullOrEmpty(BillTo.Pincode) ? "" : "-" + BillTo.Pincode)).Trim(',', ' ');

            ReportParameter[] P = new ReportParameter[21];
            P[0] = new ReportParameter("PurchaseOrderNumber", PO.PurchaseOrderNumber, false);
            P[1] = new ReportParameter("PurchaseOrderDate", PO.PurchaseOrderDate.ToShortDateString(), false);
            P[2] = new ReportParameter("SupplierName", Supplier.DealerName, false);
            P[3] = new ReportParameter("SupplierAddress1", SupplierAddress1, false);
            P[4] = new ReportParameter("SupplierAddress2", SupplierAddress2, false);
            P[5] = new ReportParameter("SupplierMobile", Supplier.Address.Mobile, false);
            P[6] = new ReportParameter("SupplierEMail", Supplier.Address.Email, false);
            P[7] = new ReportParameter("BillToCustomerName", PO.Dealer.DealerName, false);
            P[8] = new ReportParameter("BillToCustomerAddress1", BillToAddress1, false);
            P[9] = new ReportParameter("BillToCustomerAddress2", BillToAddress2, false);
            P[10] = new ReportParameter("BillToMobile", BillTo.Mobile, false);
            P[11] = new ReportParameter("BillToEMail", BillTo.Email, false);
            P[12] = new ReportParameter("PurchaseOrderType", PO.PurchaseOrderType.PurchaseOrderType, false);
            P[13] = new ReportParameter("SaleOrderNumber", PO.SaleOrderNumber, false);
            P[14] = new ReportParameter("ExpectedDeliveryDate", PO.ExpectedDeliveryDate.ToShortDateString(), false);
            P[15] = new ReportParameter("ReceivingLocation", PO.Location.OfficeName, false);


            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Taxable");
            dtItem.Columns.Add("Tax");
            dtItem.Columns.Add("Net");

            decimal GrandTotal = 0, TaxTotal = 0;

            DataTable DTMaterialText = new DataTable();
            foreach (PPurchaseOrderItem Item in PO.PurchaseOrderItems)
            {
                dtItem.Rows.Add(Item.POItem, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Quantity.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Material.CurrentPrice), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", Item.TaxValue), String.Format("{0:n}", (Item.TaxableValue + Item.TaxValue)));
                TaxTotal += Item.TaxValue;
                GrandTotal += (Item.TaxableValue + Item.TaxValue);
            }
            P[16] = new ReportParameter("TaxAmount", String.Format("{0:n}", TaxTotal), false);
            P[17] = new ReportParameter("NetAmount", String.Format("{0:n}", GrandTotal), false);
            P[18] = new ReportParameter("Remarks", PO.Remarks, false);
            P[19] = new ReportParameter("SupplierCode", Supplier.DealerCode, false);
            P[20] = new ReportParameter("BillToCode", PO.Dealer.DealerCode, false);
            report.ReportPath = Server.MapPath("~/Print/PurchaseOrder.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "PurchaseOrder";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
            return mybytes;
        }
        void DownloadPurchaseOrder()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = PurchaseOrder.PurchaseOrderNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = PurchaseOrderRdlc(out mimeType);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                new BXcel().PdfDowload();
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSubmitMaterial_Click(object sender, EventArgs e)
        {
            MPE_AddMaterial.Show();
            lblAddMaterialMessage.ForeColor = Color.Red;
            lblMessage.ForeColor = Color.Red;
            try
            {
                if (PurchaseOrder.PurchaseOrderItems.Any(item => item.Material.MaterialID == Convert.ToInt32(hdfMaterialID.Value)))
                {
                    lblAddMaterialMessage.Text = "Material Already Available...!";
                    return;
                }

                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(hdfMaterialID.Value), null, null, null, null);
                if (PurchaseOrder.PurchaseOrderType.PurchaseOrderTypeID == (short)PurchaseOrderType.MachineOrder)
                {
                    if (PurchaseOrder.PurchaseOrderItems.Any(item => item.Material.MaterialType == Materials[0].MaterialType && Materials[0].MaterialType == "FERT"))
                    {
                        lblAddMaterialMessage.Text = "Already FERT Material Available : " + Materials[0].MaterialCode;
                        return;
                    }
                    if (Convert.ToDecimal(txtQty.Text) != 1 && Materials[0].MaterialType == "FERT")
                    {
                        lblAddMaterialMessage.Text = "In machine Order you allowed to add one quantity for FERT material : " + Materials[0].MaterialCode;
                        return;
                    }
                }

                PPurchaseOrderItem_Insert POi = new PPurchaseOrderItem_Insert();
                POi.PurchaseOrderID = Convert.ToInt64(PurchaseOrder.PurchaseOrderID);
                POi.Quantity = Convert.ToDecimal(txtQty.Text);
                POi.MaterialCode = hdfMaterialCode.Value;

                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = PurchaseOrder.Dealer.DealerCode;
                MaterialPrice.Vendor = PurchaseOrder.Vendor.DealerCode;
                MaterialPrice.OrderType = PurchaseOrder.PurchaseOrderType.SapOrderType;
                MaterialPrice.Division = PurchaseOrder.Division.DivisionCode;

                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                {
                    ItemNo = "10",
                    Material = POi.MaterialCode,
                    Quantity = POi.Quantity
                });
                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                PMaterial Mat = Mats[0];

                POi.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
                POi.Price = Mat.CurrentPrice;
                POi.DiscountAmount = Mat.Discount;
                POi.TaxableAmount = Mat.TaxablePrice;
                POi.SGST = Mat.SGST;
                POi.SGSTValue = Mat.SGSTValue;
                POi.CGST = Mat.SGST;
                POi.CGSTValue = Mat.SGSTValue;
                POi.IGST = Mat.IGST;
                POi.IGSTValue = Mat.IGSTValue;
                POi.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
                POi.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
                POi.NetValue = POi.TaxableAmount + POi.SGSTValue + POi.CGSTValue + POi.IGSTValue;

                string result = new BAPI().ApiPut("PurchaseOrder/UpdatePurchaseOrderItem", POi);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblAddMaterialMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                MPE_AddMaterial.Hide();
                tbp.ActiveTabIndex = 1;
                fillViewPO(PurchaseOrder.PurchaseOrderID);
            }
            catch (Exception ex)
            {
                lblAddMaterialMessage.Text = ex.Message;
            }
        }
    }
}