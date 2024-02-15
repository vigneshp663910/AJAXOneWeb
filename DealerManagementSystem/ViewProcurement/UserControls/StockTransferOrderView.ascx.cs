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
            //   lblReceivingLocation.Text = StockTransferOrder.DestinationOffice.OfficeName;
            lblPORemarks.Text = StockTransferOrder.Remarks;

            lblPODealer.Text = StockTransferOrder.Dealer.DealerName;


            gvPOItem.DataSource = StockTransferOrder.Items;
            gvPOItem.DataBind();
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
            else if (lbActions.ID == "lbPDF")
            {
                ViewPurchaseOrder();
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
            lbPDF.Visible = true;
            lbDelivery.Visible = true;

            int StatusID = StockTransferOrder.Status.StatusID;
            if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Draft)
            {
                lbPDF.Visible = false;
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
                lbPDF.Visible = false;
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

            //List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCreate).Count() == 0)
            //{
            //    lbAddMaterial.Visible = false;
            //}
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderReleasePO).Count() == 0)
            //{
            //    lbReleasePO.Visible = false;
            //}
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCancelPO).Count() == 0)
            //{
            //    lbCancelPO.Visible = false;
            //}
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
      

        protected void btnSubmitMaterial_Click(object sender, EventArgs e)
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
                    Delivery_Insert.Remove(Item);
                gvDelivery.DataSource = Delivery_Insert;
                gvDelivery.DataBind();
            }
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
                lblMessage.Text = e1.Message;
            }
        }
        void readDeviveryGrid()
        {
            foreach (GridViewRow row in gvDelivery.Rows)
            {
                Label lblStockTransferOrderItemID = (Label)row.FindControl("lblStockTransferOrderItemID");
                Label lblBalanceQuantity = (Label)row.FindControl("lblBalanceQuantity");
                TextBox txtDeliveryQuantity = (TextBox)row.FindControl("txtDeliveryQuantity");

                decimal BalanceQuantity = Convert.ToDecimal(lblBalanceQuantity.Text);
                decimal DeliveryQuantity = Convert.ToDecimal(txtDeliveryQuantity.Text);
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
    }
}