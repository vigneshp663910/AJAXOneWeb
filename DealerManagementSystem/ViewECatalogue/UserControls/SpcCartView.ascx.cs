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

namespace DealerManagementSystem.ViewECatalogue.UserControls
{
    public partial class SpcCartView : System.Web.UI.UserControl
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
         

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 
            if (Session["PurchaseOrderID"] != null)
            {
                lblMessage.Text = "Purchase Order Created Number  ";// + PurchaseOrder.PurchaseOrderNumber;
                lblMessage.ForeColor = Color.Green;
                Session["PurchaseOrderID"] = null;
            }
        }
        public void fillViewPO(long spcCartID)
        {
            PApiResult Result = new BECatalogue().GetSpcCart(spcCartID, null, null, null, null, null, null, null, null, null);

            PspcCart Cart = JsonConvert.DeserializeObject<List<PspcCart>>(JsonConvert.SerializeObject(Result.Data))[0];
             
             

            lblPurchaseOrderNumber.Text = PurchaseOrder.PurchaseOrderNumber;
            lblPurchaseOrderDate.Text = PurchaseOrder.PurchaseOrderDate.ToString();
            lblSoNumber.Text = PurchaseOrder.SaleOrderNumber;
            // lblSoDate.Text = PurchaseOrder..ToString();
            lblStatus.Text = PurchaseOrder.PurchaseOrderStatus.ProcurementStatus;
           

            lblOrderType.Text = PurchaseOrder.PurchaseOrderType.PurchaseOrderType;
            lblReceivingLocation.Text = PurchaseOrder.Location.OfficeName; 

            lblPODealer.Text = PurchaseOrder.Dealer.DealerCode + " " + PurchaseOrder.Dealer.DealerName;
            lblPOVendor.Text = PurchaseOrder.Vendor.DealerCode + " " + PurchaseOrder.Vendor.DealerName;
            lblExpectedDeliveryDate.Text = PurchaseOrder.ExpectedDeliveryDate.ToShortDateString();
            lblOrderTo.Text = PurchaseOrder.PurchaseOrderTo.PurchaseOrderTo.ToString(); 

            
            ActionControlMange();

        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbCancelPO")
            {
                lblMessage.ForeColor = Color.Red;

                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderAsnHeader(null, null, null, null, null, null, (short)ProcurementStatus.AsnGRPending, PurchaseOrder.PurchaseOrderNumber, null, null, null, 1, 10);
                List<PAsn> PAsnHeader = JsonConvert.DeserializeObject<List<PAsn>>(JsonConvert.SerializeObject(Result.Data));
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
          
        }
        void ActionControlMange()
        { 
            lbCancelPO.Visible = true;  
            int StatusID = PurchaseOrder.PurchaseOrderStatus.ProcurementStatusID;
                

            int PurchaseOrderTypeID = PurchaseOrder.PurchaseOrderType.PurchaseOrderTypeID;
           
           

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PurchaseOrderCreate).Count() == 0)
            {
                lbCancelPO.Visible = false;
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
         
    }
}