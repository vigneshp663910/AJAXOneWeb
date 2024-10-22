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

            //if (StockTransferOrder.Status.StatusID != (short)AjaxOneStatus.StockTransferOrder_Draft)
            //{

            //    foreach (GridViewRow row in gvPOItem.Rows)
            //    {
            //        LinkButton lnkBtnEdit = (LinkButton)row.FindControl("lnkBtnEdit");
            //        LinkButton lnkBtnDelete = (LinkButton)row.FindControl("lnkBtnDelete");
            //        lnkBtnEdit.Visible = false;
            //        lnkBtnDelete.Visible = false;
            //    }
            //}

            gvDeliveryView.DataSource = Deliverys;
            gvDeliveryView.DataBind();
            ActionControlMange();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbDelivery")
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
              //  ViewStockTransferOrder();
            }
            else if (lbActions.ID == "lbDowloadSTO")
            {
               // DownloadStockTransferOrder();
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
            lbDelivery.Visible = true;
            int StatusID = StockTransferOrder.Status.StatusID;
            if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Draft)
            { 
                lbDelivery.Visible = false;
            }            
             
            else if ((StatusID == (short)AjaxOneStatus.StockTransferOrder_Delivered) || (StatusID == (short)AjaxOneStatus.StockTransferOrder_PartiallyClosed))
            { 
                lbDelivery.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.StockTransferOrder_Cancelled)
            { 
                lbDelivery.Visible = false; 
            }           

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.StockTransferOrderCreate).Count() == 0)
            { 
                lbDelivery.Visible = false;
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
        
    }
}