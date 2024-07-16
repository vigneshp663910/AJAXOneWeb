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
    public partial class StockTransferOrderView : System.Web.UI.UserControl
    {
        public List<PStockTransferOrderItem_Insert> PurchaseOrderItem_Insert
        {
            get
            {
                if (ViewState["PurchaseOrderItem_Insert"] == null)
                {
                    ViewState["PurchaseOrderItem_Insert"] = new List<PStockTransferOrderItem_Insert>();
                }
                return (List<PStockTransferOrderItem_Insert>)ViewState["PurchaseOrderItem_Insert"];
            }
            set
            {
                ViewState["PurchaseOrderItem_Insert"] = value;
            }
        }
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
        public List<PStockTransferOrderDelivery> Deliverys
        {
            get
            {
                if (ViewState["Deliverys"] == null)
                {
                    ViewState["Deliverys"] = new List<PStockTransferOrderDelivery>();
                }
                return (List<PStockTransferOrderDelivery>)ViewState["Deliverys"];
            }
            set
            {
                ViewState["Deliverys"] = value;
            }
        }
        public List<PStockTransferOrderItemDelivery_Insert> Delivery_Insert
        {
            get
            {
                if (ViewState["StockTransferOrderViewDelivery_Insert"] == null)
                {
                    ViewState["StockTransferOrderViewDelivery_Insert"] = new List<PStockTransferOrderItemDelivery_Insert>();
                }
                return (List<PStockTransferOrderItemDelivery_Insert>)ViewState["StockTransferOrderViewDelivery_Insert"];
            }
            set
            {
                ViewState["StockTransferOrderViewDelivery_Insert"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageAddMaterial.Text = "";
            lblMessageDelivery.Text = "";
            //if (Session["PurchaseOrderID"] != null)
            //{
            //    lblMessage.Text = "Purchase Order Created Number : " + PurchaseOrder.PurchaseOrderNumber;
            //    Session["PurchaseOrderID"] = null;
            //}
        }
        public void fillViewPO(long StockTransferOrderID)
        {
            PApiResult Result = new BStockTransferOrder().GetStockTransferOrderByID(StockTransferOrderID);
            StockTransferOrder = JsonConvert.DeserializeObject<PStockTransferOrder>(JsonConvert.SerializeObject(Result.Data));


            PApiResult DeliveryResult = new BStockTransferOrder().GetStockTransferOrderDeliveryByID(StockTransferOrderID, null);
            Deliverys = JsonConvert.DeserializeObject<List<PStockTransferOrderDelivery>>(JsonConvert.SerializeObject(DeliveryResult.Data));

            lblPurchaseOrderNumber.Text = StockTransferOrder.StockTransferOrderNumber;
            lblPurchaseOrderDate.Text = StockTransferOrder.StockTransferOrderDate.ToString();
            lblStatus.Text = StockTransferOrder.Status.Status;


            lblReceivingLocation.Text = StockTransferOrder.DestinationOffice.OfficeName;
            lblSourceLocation.Text = StockTransferOrder.SourceOffice.OfficeName;
            lblPORemarks.Text = StockTransferOrder.Remarks;

            lblPODealer.Text = StockTransferOrder.Dealer.DealerName;


            gvPOItem.DataSource = StockTransferOrder.Items;
            gvPOItem.DataBind();
            decimal TaxableValue = 0, TaxValue = 0;
            foreach (PStockTransferOrderItem Item in StockTransferOrder.Items)
            {
                TaxableValue = TaxableValue + Item.TaxableValue;
                TaxValue = TaxValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue;
                //Item.NetValue = Item.CGSTValue + Item.SGSTValue + Item.IGSTValue + Item.TaxableAmount;
            }
            lblTaxableAmount.Text = TaxableValue.ToString();
            lblTaxAmount.Text = TaxValue.ToString();
            lblGrossAmount.Text = (TaxValue + TaxableValue).ToString();

            if (StockTransferOrder.Status.StatusID != (short)AjaxOneStatus.StockTransferOrder_Draft)
            {

                foreach (GridViewRow row in gvPOItem.Rows)
                {
                    LinkButton lnkBtnEdit = (LinkButton)row.FindControl("lnkBtnEdit");
                    LinkButton lnkBtnDelete = (LinkButton)row.FindControl("lnkBtnDelete");
                    lnkBtnEdit.Visible = false;
                    lnkBtnDelete.Visible = false;
                }
            }

            gvDeliveryView.DataSource = Deliverys;
            gvDeliveryView.DataBind();
            ActionControlMange();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if ((lbActions.ID == "lbRelease") || (lbActions.ID == "lbCancel"))
            {
                int StatusID = 0;
                if (lbActions.ID == "lbRelease")
                {
                    StatusID = (short)AjaxOneStatus.StockTransferOrder_Release;
                }
                else if (lbActions.ID == "lbCancel")
                {
                    StatusID = (short)AjaxOneStatus.StockTransferOrder_Cancelled;
                }                
                lblMessage.Visible = true;
                PApiResult Results = new BStockTransferOrder().UpdateStockTransferOrderStatus(StockTransferOrder.StockTransferOrderID, StatusID);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                fillViewPO(StockTransferOrder.StockTransferOrderID);
            } 
            else if (lbActions.ID == "lbAddMaterial")
            {
                MPE_AddMaterial.Show();
            }
            else if (lbActions.ID == "lbDelivery")
            {
                MPE_Delivery.Show();
                Delivery_Insert = new List<PStockTransferOrderItemDelivery_Insert>();
                foreach (PStockTransferOrderItem Item in StockTransferOrder.Items)
                {
                    Delivery_Insert.Add(new PStockTransferOrderItemDelivery_Insert()
                    {
                        StockTransferOrderID = StockTransferOrder.StockTransferOrderID,
                        StockTransferOrderItemID = Item.StockTransferOrderItemID,
                        MaterialID = Item.Material.MaterialID,
                        MaterialCode = Item.Material.MaterialCode,
                        MaterialDescription = Item.Material.MaterialDescription,
                        Quantity = Item.Quantity,
                        BalanceQuantity = Item.Quantity - Item.TransitQuantity - Item.DeliveredQuantity,
                        DeliveryQuantity = Item.Quantity - Item.TransitQuantity - Item.DeliveredQuantity,
                    });
                }
                gvDelivery.DataSource = Delivery_Insert;
                gvDelivery.DataBind();

            }
            else if (lbActions.ID == "lbPreviewSTO")
            {
                ViewStockTransferOrder();
            }
            else if (lbActions.ID == "lbDowloadSTO")
            {
                DownloadStockTransferOrder();
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

            lbAddMaterial.Visible = true;
            lbRelease.Visible = true;
            lbCancel.Visible = true;
           // lbPDF.Visible = true;
            lbDelivery.Visible = true;

            int StatusID = StockTransferOrder.Status.StatusID;
            if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Draft)
            {
               // lbPDF.Visible = false;
                lbDelivery.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Release)
            {
                lbAddMaterial.Visible = false;
                lbRelease.Visible = false;
                lbCancel.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_PartiallyDelivered)
            {
                lbAddMaterial.Visible = false;
                lbRelease.Visible = false;
                lbCancel.Visible = false;
            }
            else if ((StatusID == (short)AjaxOneStatus.StockTransferOrder_Delivered) || (StatusID == (short)AjaxOneStatus.StockTransferOrder_PartiallyClosed))
            {
                lbAddMaterial.Visible = false;
                lbRelease.Visible = false;
                lbCancel.Visible = false;

                lbDelivery.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Cancelled)
            {
                lbAddMaterial.Visible = false;
                lbRelease.Visible = false;
                lbCancel.Visible = false;
                lbDelivery.Visible = false;
               // lbPDF.Visible = false;
            }

            //else if ((StatusID == (short)ProcurementStatus.PoCompleted)
            //   || (StatusID == (short)ProcurementStatus.PoForceClosed) || (StatusID == (short)ProcurementStatus.PoCancelld))
            //{
            //    lbAddMaterial.Visible = false;
            //    lbReleasePO.Visible = false;
            //    lbCancelPO.Visible = false;
            //    lbReleaseApprove.Visible = false;
            //    lbCancelApprove.Visible = false;
            //    gvPOItem.Columns[15].Visible = false;
            //}
            //else if (StatusID == (short)ProcurementStatus.PoWaitingForReleaseApproval)
            //{
            //    lbAddMaterial.Visible = false;
            //    lbReleasePO.Visible = false;
            //    lbCancelPO.Visible = false;
            //    lbCancelApprove.Visible = false;

            //    gvPOItem.Columns[15].Visible = false;
            //}
            //else if (StatusID == (short)ProcurementStatus.PoWaitingForCancelApproval)
            //{
            //    lbAddMaterial.Visible = false;
            //    lbReleasePO.Visible = false;
            //    lbCancelPO.Visible = false;
            //    lbReleaseApprove.Visible = false;

            //    gvPOItem.Columns[15].Visible = false;
            //}

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.StockTransferOrderCreate).Count() == 0)
            {
                lbAddMaterial.Visible = false;
                lbDelivery.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.StockTransferOrderRelease).Count() == 0)
            {
                lbRelease.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.StockTransferOrderCancel).Count() == 0)
            {
                lbCancel.Visible = false;
            }
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderReleaseApprove).Count() == 0)
            //{
            //    lbReleaseApprove.Visible = false;
            //}
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCancelApprove).Count() == 0)
            //{
            //    lbCancelApprove.Visible = false;
            //}
        }
        public void fillEnquiryStatusHistory()
        {

        }

        protected void btnCancelPoItem_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
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
            fillViewPO(StockTransferOrder.StockTransferOrderID);
        }

        protected void lnkBtnItemAction_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;

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
                lblMessage.Visible = true;
                Label lblStockTransferOrderItemID = (Label)gvRow.FindControl("lblStockTransferOrderItemID");
                int StatusID = (short)AjaxOneStatus.StockTransferOrderItem_Cancelled;
                PApiResult Results = new BStockTransferOrder().UpdateStockTransferOrderItemStatus(Convert.ToInt64(lblStockTransferOrderItemID.Text), StatusID);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                fillViewPO(StockTransferOrder.StockTransferOrderID);
            }
            else if (lbActions.ID == "lnkBtnupdate")
            {
                PStockTransferOrderItem_Insert PoI = new PStockTransferOrderItem_Insert();

                Label lblStockTransferOrderItemID = (Label)gvRow.FindControl("lblStockTransferOrderItemID");
                Label lblMaterialID = (Label)gvRow.FindControl("lblMaterialID");
                Label lblMaterial = (Label)gvRow.FindControl("lblMaterial");
                Label lblMaterialDescription = (Label)gvRow.FindControl("lblMaterialDescription");

                Decimal.TryParse("0" + txtQuantity.Text, out decimal value);
                if (value <= 0)
                {
                    lblMessage.Text = "Please enter valid Qty.";
                    return;
                }

                PoI.StockTransferOrderItemID = Convert.ToInt64(lblStockTransferOrderItemID.Text);
                PoI.MaterialID = Convert.ToInt32(lblMaterialID.Text);
                PoI.MaterialCode = lblMaterial.Text;
                PoI.StockTransferOrderID = StockTransferOrder.StockTransferOrderID;
                PoI.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                PoI.MaterialDescription = lblMaterialDescription.Text;

                //decimal value;
                //if (!decimal.TryParse(txtQuantity.Text.Trim(), out value))
                //{
                //    lblMessage.Text = "Please enter correct format in Qty.";
                //    return;
                //}
                //if (Convert.ToDecimal(txtQuantity.Text.Trim()) < 1)
                //{
                //    lblMessage.Text = "Quantity cannot be less than 1.";
                //    return;
                //}

                PoI = new BStockTransferOrder().GetMaterialPriceForStockTransferOrder(StockTransferOrder.Dealer.DealerID, PoI);
                PurchaseOrderItem_Insert.Add(PoI);
                PApiResult Result = new BStockTransferOrder().InsertOrUpdateStockTransferOrderItem(PoI);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessageAddMaterial.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                MPE_AddMaterial.Hide();
                fillViewPO(StockTransferOrder.StockTransferOrderID);
            }
        }

         

        protected void btnSubmitMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_AddMaterial.Show();

                lblMessageAddMaterial.ForeColor = Color.Red;
                lblMessage.ForeColor = Color.Red;
                if (StockTransferOrder.Items.Any(item => item.Material.MaterialID == Convert.ToInt32(hdfMaterialID.Value)))
                {
                    lblMessageAddMaterial.Text = "Material Already Available...!";
                    MPE_AddMaterial.Show();
                    return;
                }

                Decimal.TryParse("0" + txtQty.Text, out decimal value);
                if (value <= 0)
                {
                    lblMessageAddMaterial.Text = "Please enter valid Qty.";
                    MPE_AddMaterial.Show();
                    return;
                }

                PStockTransferOrderItem_Insert PoI = new PStockTransferOrderItem_Insert();
                PoI.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
                PoI.MaterialCode = hdfMaterialCode.Value;
                PoI.StockTransferOrderID = StockTransferOrder.StockTransferOrderID;
                PoI.Quantity = Convert.ToDecimal(txtQty.Text);

                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
                PoI.MaterialDescription = m.MaterialDescription;

                PoI = new BStockTransferOrder().GetMaterialPriceForStockTransferOrder(StockTransferOrder.Dealer.DealerID, PoI);
                PurchaseOrderItem_Insert.Add(PoI);
                PApiResult Result = new BStockTransferOrder().InsertOrUpdateStockTransferOrderItem(PoI);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessageAddMaterial.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                MPE_AddMaterial.Hide();
                fillViewPO(StockTransferOrder.StockTransferOrderID);
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        protected void lnkBtngvDeliveryAction_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;


            LinkButton lnkBtnRemove = (LinkButton)gvRow.FindControl("lnkBtnRemove");
            Label lblStockTransferOrderItemID = (Label)gvRow.FindControl("lblStockTransferOrderItemID");
            //TextBox txtDeliveryQuantity = (TextBox)gvRow.FindControl("txtDeliveryQuantity");
            //Label lblBalanceQuantity = (Label)gvRow.FindControl("lblBalanceQuantity");
            foreach (PStockTransferOrderItemDelivery_Insert Item in Delivery_Insert)
            {
                if (Convert.ToInt64(lblStockTransferOrderItemID.Text) == Item.StockTransferOrderItemID)
                {
                    Delivery_Insert.Remove(Item);
                    gvDelivery.DataSource = Delivery_Insert;
                    gvDelivery.DataBind();
                    break;
                }
            }
            MPE_Delivery.Show();
        }

        protected void gvDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            readDeviveryGrid();
            gvDelivery.DataSource = Delivery_Insert;
            gvDelivery.DataBind();
        }

        protected void btnSaveDelivery_Click(object sender, EventArgs e)
        {
            MPE_Delivery.Show();
            lblMessageDelivery.ForeColor = Color.Red;
            try
            {
                readDeviveryGrid();
                PApiResult Result = new BStockTransferOrder().InsertStockTransferOrderDelivery(Delivery_Insert);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessageDelivery.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                ShowMessage(Result);
                fillViewPO(StockTransferOrder.StockTransferOrderID);
                MPE_Delivery.Hide();
            }
            catch (Exception e1)
            {
                lblMessageDelivery.Text = e1.Message;
            }
        }
        void readDeviveryGrid()
        {
            foreach (GridViewRow row in gvDelivery.Rows)
            {
                Label lblStockTransferOrderItemID = (Label)row.FindControl("lblStockTransferOrderItemID");
                Label lblBalanceQuantity = (Label)row.FindControl("lblBalanceQuantity");
                TextBox txtDeliveryQuantity = (TextBox)row.FindControl("txtDeliveryQuantity");

                Decimal.TryParse("0" + txtDeliveryQuantity.Text, out decimal DeliveryQuantity);
                if (DeliveryQuantity <= 0)
                {
                    throw new Exception("Please enter valid Qty.");
                }

                decimal BalanceQuantity = Convert.ToDecimal(lblBalanceQuantity.Text);
                if (DeliveryQuantity > BalanceQuantity)
                {
                    throw new Exception("Please check the Delivery Quantity");
                }
                foreach (PStockTransferOrderItemDelivery_Insert Item in Delivery_Insert)
                {
                    if (Convert.ToInt64(lblStockTransferOrderItemID.Text) == Item.StockTransferOrderItemID)
                    {
                        Item.DeliveryQuantity = DeliveryQuantity;
                        break;
                    }
                }
            }
        }



        protected void gvDeliveryView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDeliveryID = (Label)e.Row.FindControl("lblDeliveryID");
                    GridView gvDeliveryViewItem = (GridView)e.Row.FindControl("gvDeliveryViewItem");
                    List<PStockTransferOrderDeliveryItem> Lines = new List<PStockTransferOrderDeliveryItem>();
                    Lines = Deliverys.Find(s => s.DeliveryID == Convert.ToInt64(lblDeliveryID.Text)).Items;
                    gvDeliveryViewItem.DataSource = Lines;
                    gvDeliveryViewItem.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
        void ViewStockTransferOrder()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = StockTransferOrderRdlc(out mimeType);
                string FileName = StockTransferOrder.StockTransferOrderNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Procurement » Stock Transfer Order','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] StockTransferOrderRdlc(out string mimeType)
        {
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_DealerOffice DealerFrom = new BDMS_Dealer().GetDealerOffice(StockTransferOrder.Dealer.DealerID, StockTransferOrder.SourceOffice.OfficeID, null)[0];
            string FromAddress1 = (DealerFrom.Address1 + (string.IsNullOrEmpty(DealerFrom.Address2) ? "" : "," + DealerFrom.Address2) + (string.IsNullOrEmpty(DealerFrom.Address3) ? "" : "," + DealerFrom.Address3)).Trim(',', ' ');
            string FromAddress2 = (DealerFrom.City + (string.IsNullOrEmpty(DealerFrom.State) ? "" : "," + DealerFrom.State) + (string.IsNullOrEmpty(DealerFrom.Pincode) ? "" : "-" + DealerFrom.Pincode)).Trim(',', ' ');

            PDMS_DealerOffice DealerTo = new BDMS_Dealer().GetDealerOffice(StockTransferOrder.Dealer.DealerID, StockTransferOrder.DestinationOffice.OfficeID, null)[0];
            string ToAddress1 = (DealerTo.Address1 + (string.IsNullOrEmpty(DealerTo.Address2) ? "" : "," + DealerTo.Address2) + (string.IsNullOrEmpty(DealerTo.Address3) ? "" : "," + DealerTo.Address3)).Trim(',', ' ');
            string ToAddress2 = (DealerTo.City + (string.IsNullOrEmpty(DealerTo.State) ? "" : "," + DealerTo.State) + (string.IsNullOrEmpty(DealerTo.Pincode) ? "" : "-" + DealerTo.Pincode)).Trim(',', ' ');


            ReportParameter[] P = new ReportParameter[13];
            P[0] = new ReportParameter("DealerName", StockTransferOrder.Dealer.DealerName.ToUpper(), false);
            P[1] = new ReportParameter("FromAddress1", FromAddress1, false);
            P[2] = new ReportParameter("FromAddress2", FromAddress2, false);
            P[3] = new ReportParameter("ToAddress1", ToAddress1, false);
            P[4] = new ReportParameter("ToAddress2", ToAddress2, false);
            P[5] = new ReportParameter("From", DealerFrom.OfficeName_OfficeCode, false);
            P[6] = new ReportParameter("To", DealerTo.OfficeName_OfficeCode, false);
            P[7] = new ReportParameter("StockTransferOrderNo", StockTransferOrder.StockTransferOrderNumber, false);
            P[8] = new ReportParameter("StockTransferOrderDate", StockTransferOrder.StockTransferOrderDate.ToShortDateString(), false);
            P[9] = new ReportParameter("Remarks", StockTransferOrder.Remarks, false);
            P[10] = new ReportParameter("DealerCode", StockTransferOrder.Dealer.DealerCode, false);


            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("Uom");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Gross");
            dtItem.Columns.Add("Tax");
            dtItem.Columns.Add("Net");

            int sno = 0;
            decimal  TaxAmount = 0, NetAmount = 0;
            foreach (PStockTransferOrderItem Item in StockTransferOrder.Items)
            {
                dtItem.Rows.Add(sno += 1, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Quantity.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", (Item.TaxableValue/Item.Quantity)), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", (Item.CGSTValue + Item.SGSTValue + Item.IGSTValue)), String.Format("{0:n}", (Item.TaxableValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue)));
                //TaxableValue += Item.TaxableValue;
                TaxAmount += Item.CGSTValue + Item.SGSTValue + Item.IGSTValue;
                NetAmount += Item.TaxableValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue;
            }
            P[11] = new ReportParameter("TaxAmount", String.Format("{0:n}", TaxAmount), false);
            P[12] = new ReportParameter("NetAmount", String.Format("{0:n}", NetAmount), false);
            report.ReportPath = Server.MapPath("~/Print/StockTransferOrder.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "StockTransferOrder";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }
        void DownloadStockTransferOrder()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = StockTransferOrder.StockTransferOrderNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = StockTransferOrderRdlc(out mimeType);
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
    }
}