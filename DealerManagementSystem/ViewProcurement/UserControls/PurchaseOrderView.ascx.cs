using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        }
        public void fillViewPO(long PurchaseOrderID)
        {
            PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderByID(PurchaseOrderID);
            Asns = new BDMS_PurchaseOrder().GetPurchaseOrderAsnByID(PurchaseOrderID,null);

            lblPurchaseOrderNumber.Text = PurchaseOrder.PurchaseOrderNumber;
            lblStatus.Text = PurchaseOrder.PurchaseOrderStatus.ProcurementStatus;
            lblDivision.Text = PurchaseOrder.Division.DivisionCode;
            lblRefNo.Text = PurchaseOrder.ReferenceNo;

            lblPurchaseOrderDate.Text = PurchaseOrder.PurchaseOrderDate.ToString();
            lblOrderType.Text = PurchaseOrder.PurchaseOrderType.PurchaseOrderType;
            lblReceivingLocation.Text = PurchaseOrder.Location.OfficeName;
            lblPORemarks.Text = PurchaseOrder.Remarks;

            lblPODealer.Text = PurchaseOrder.Dealer.DealerName;
            lblPOVendor.Text = PurchaseOrder.Vendor.DealerName;
            lblExpectedDeliveryDate.Text = PurchaseOrder.ExpectedDeliveryDate.ToString();


            gvPOItem.DataSource = PurchaseOrder.PurchaseOrderItems;
            gvPOItem.DataBind();
            decimal Price = 0, Discount=0, TaxableAmount=0, TaxAmount=0;
            foreach(PPurchaseOrderItem Item in PurchaseOrder.PurchaseOrderItems)
            {
                Price = Price + Item.Price;
                Discount = Discount + Item.Discount;
                TaxableAmount = TaxableAmount + Item.TaxableValue;
                TaxAmount = TaxAmount + Item.TaxValue; 
            }
            lblPrice.Text = Price.ToString();
            lblDiscount.Text = Discount.ToString();
            lblTaxableAmount.Text = TaxableAmount.ToString();
            lblTaxAmount.Text = TaxAmount.ToString();
            lblGrossAmount.Text = (TaxableAmount + TaxAmount).ToString();


            gvPAsn.DataSource = Asns;
            gvPAsn.DataBind();
            ActionControlMange();

        }
        
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbReleasePO")
            {
                lblMessage.Visible = true;
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
                lblMessage.Visible = true;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelPurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
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

                lblMessage.Visible = true;
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
                lblMessage.Visible = true;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelApprovePurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
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
        }
 
       
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        void ActionControlMange()
        {
            lbReleasePO.Visible = true;
            lbEditPO.Visible = true;
            lbCancelPO.Visible = true;
            lbReleaseApprove.Visible = true;
            lbCancelApprove.Visible = true;

            int StatusID = PurchaseOrder.PurchaseOrderStatus.ProcurementStatusID;
            if (StatusID == (short)ProcurementStatus.PoDraft)
            {
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReleased)
            {
                lbReleasePO.Visible = false;
                lbEditPO.Visible = false;
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoPartialReceived)
            {
                lbReleasePO.Visible = false;
                lbEditPO.Visible = false;
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
            }
            else if ((StatusID == (short)ProcurementStatus.PoCompleted)
               || (StatusID == (short)ProcurementStatus.PoForceClosed) || (StatusID == (short)ProcurementStatus.PoCancelld))
            {
                lbReleasePO.Visible = false;
                lbEditPO.Visible = false;
                lbCancelPO.Visible = false;
                lbReleaseApprove.Visible = false;
                lbCancelApprove.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoWaitingForReleaseApproval)
            {
                lbReleasePO.Visible = false;
                lbEditPO.Visible = false;
                lbCancelPO.Visible = false;
                lbCancelApprove.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoWaitingForCancelApproval)
            {
                lbReleasePO.Visible = false;
                lbEditPO.Visible = false;
                lbCancelPO.Visible = false;
                lbReleaseApprove.Visible = false;
            }
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
            }
            else if (lbActions.ID == "lnkBtnupdate")
            {
                Label lblPurchaseOrderItemID = (Label)gvRow.FindControl("lblPurchaseOrderItemID");
                Label lblMaterial = (Label)gvRow.FindControl("lblMaterial");

                

                PPurchaseOrderItem_Insert POi = new PPurchaseOrderItem_Insert();
                POi.PurchaseOrderItemID = Convert.ToInt64(lblPurchaseOrderItemID.Text);
                POi.Quantity = Convert.ToDecimal(txtQuantity.Text);
                POi.MaterialCode = lblMaterial.Text;

                string Customer = PurchaseOrder.Dealer.DealerCode;
                string Vendor = PurchaseOrder.Vendor.DealerCode;
                string OrderType = PurchaseOrder.PurchaseOrderType.SapOrderType;
                string Material = POi.MaterialCode;
                string IV_SEC_SALES = "";
                //string PriceDate = DateTime.Now.ToShortDateString();
                string PriceDate = "";
                string IsWarrenty = "false";

                PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, OrderType, 1, Material, POi.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);
                POi.Price = Mat.CurrentPrice;
                POi.DiscountAmount = Mat.Discount;
                POi.TaxableAmount = Mat.TaxablePrice;
                POi.SGST = Mat.SGST;
                POi.SGSTValue = Mat.SGSTValue;
                POi.CGST = Mat.CGST;
                POi.CGSTValue = Mat.CGSTValue;
                POi.CGST = Mat.CGST;
                POi.IGSTValue = Mat.IGSTValue;


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
                lblMessage.Visible = true;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("PurchaseOrder/CancelPurchaseOrder?PurchaseOrderID=" + PurchaseOrder.PurchaseOrderID.ToString()));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
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
    }
}