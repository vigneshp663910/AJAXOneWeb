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
    public partial class PurchaseOrderReturnView : System.Web.UI.UserControl
    {
        public PPurchaseOrderReturn PoReturn
        {
            get
            {
                if (ViewState["PPurchaseOrderReturn"] == null)
                {
                    ViewState["PPurchaseOrderReturn"] = new PPurchaseOrderReturn();
                }
                return (PPurchaseOrderReturn)ViewState["PPurchaseOrderReturn"];
            }
            set
            {
                ViewState["PPurchaseOrderReturn"] = value;
            }
        }
        //public List<PPurchaseOrderReturnDelivery> PurchaseOrderReturnDeliveryList
        //{
        //    get
        //    {
        //        if (ViewState["PurchaseOrderReturnDelivery"] == null)
        //        {
        //            ViewState["PurchaseOrderReturnDelivery"] = new List<PPurchaseOrderReturnDelivery>();
        //        }
        //        return (List<PPurchaseOrderReturnDelivery>)ViewState["PurchaseOrderReturnDelivery"];
        //    }
        //    set
        //    {
        //        ViewState["PurchaseOrderReturnDelivery"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 
            lblMessageDeliveryCreate.Text = "";
            lblMessageCancel.Text = "";
        }
        public void fillViewPoReturn(long PurchaseOrderReturnID)
        {
            PoReturn = new BDMS_PurchaseOrder().GetPurchaseOrderReturnByID(PurchaseOrderReturnID);

            lblPurchaseOrderReturnNumber.Text = PoReturn.PurchaseOrderReturnNumber;
            lblPurchaseOrderReturnDate.Text = PoReturn.PurchaseOrderReturnDate.ToString();
            lblPurchaseOrderReturnStatus.Text = PoReturn.PurchaseOrderReturnStatus.ProcurementStatus;
            lblRemarks.Text = PoReturn.Remarks;

            gvPOReturnItem.DataSource = PoReturn.PurchaseOrderReturnItems;
            gvPOReturnItem.DataBind();

            gvPoReturnDelivery.DataSource = null;
            gvPoReturnDelivery.DataBind();
            List<PPurchaseOrderReturnDelivery>  PurchaseOrderReturnDeliveryList = new BDMS_PurchaseOrder().GetPurchaseOrderReturnDeliveryByPoReturnID(PurchaseOrderReturnID);
            gvPoReturnDelivery.DataSource = PurchaseOrderReturnDeliveryList;
            gvPoReturnDelivery.DataBind();

            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbRequestForApproval.Visible = true;
            lbApprove.Visible = true;
            lbReject.Visible = true;
            lbCancel.Visible = true;
            lbDeliveryCreate.Visible = true;

            int StatusID = PoReturn.PurchaseOrderReturnStatus.ProcurementStatusID;
            if (StatusID == (short)ProcurementStatus.PoReturnDraft)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbReject.Visible = false; 
                lbDeliveryCreate.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReturnWaitingForApproval)
            {
                lbCancel.Visible = false;
                lbRequestForApproval.Visible = false; 
                lbDeliveryCreate.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReturnApproved)
            {
                lbRequestForApproval.Visible = false;
                lbApprove.Visible = false;
                lbReject.Visible = false;
                lbCancel.Visible = false; 
            }
            else if (StatusID == (short)ProcurementStatus.PoReturnRejected || StatusID == (short)ProcurementStatus.PoReturnCancelled || StatusID == (short)ProcurementStatus.PoReturnDeliveryCreated)
            {
                lbRequestForApproval.Visible = false;
                lbApprove.Visible = false;
                lbReject.Visible = false;
                lbCancel.Visible = false;
                lbDeliveryCreate.Visible = false;
            } 
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbRequestForApproval")
            {
                PApiResult Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturnWaitingForApproval, "");
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Result.Message;
                fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
            }
            else if (lbActions.ID == "lbApprove")
            {
                PApiResult Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturnApproved, "");
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Result.Message;
                fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
            }
            else if (lbActions.ID == "lbReject")
            {
                txtRejectRemarks.Text = "";
                MPE_PoReturnReject.Show();
            }
            else if(lbActions.ID == "lbCancel")
            {
                txtCancelRemarks.Text = "";
                MPE_PoReturnCancel.Show();
            }
            else if(lbActions.ID == "lbDeliveryCreate")
            {
                Clear(); 
                divProceeedDelivery.Visible = false;
                MPE_PoReturnDeliveryCreate.Show();
                UC_PurchaseOrderReturnDeliveryCreate.fillPOReturnItem(PoReturn.PurchaseOrderReturnID);
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderReturnItemForDeliveryCreation(PoReturn.PurchaseOrderReturnID);
                if(JsonConvert.DeserializeObject<List<PPurchaseOrderReturn>>(JsonConvert.SerializeObject(Result.Data)).Count>0)
                {
                    divProceeedDelivery.Visible = true;
                }
            }
        }
        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            Button lbActions = ((Button)sender);
            PApiResult Result = null;
            if (lbActions.ID == "btnPoReject")
            {
                MPE_PoReturnReject.Show();
                Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturnRejected, txtRejectRemarks.Text.Trim());
            }
            else if (lbActions.ID == "btnPoCancel")
            {
                MPE_PoReturnCancel.Show();
                Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturnCancelled, txtCancelRemarks.Text.Trim()); 
            }
             
            if (Result.Status == PApplication.Failure)
            {
                lblMessageCancel.Text = Result.Message;
                return;
            }
            MPE_PoReturnReject.Hide();
            MPE_PoReturnCancel.Hide();
            
            fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
        }        
        protected void btnPurchaseOrderReturnDeliveryCreateBack_Click(object sender, EventArgs e)
        {
            PnlPurchaseOrderReturnView.Visible = true;
            Panel pnlPoReturnDeliveryCreate = (Panel)UC_PurchaseOrderReturnDeliveryCreate.FindControl("pnlPoReturnDeliveryCreate");
            pnlPoReturnDeliveryCreate.Visible = false;
            pnlPoReturnDeliveryCreate.Visible = false;
        }
        protected void btnProceedDelivery_Click(object sender, EventArgs e)
        { 
            UC_PurchaseOrderReturnDeliveryCreate.ReadPoReturnItem();
            divSave.Visible = true;
            divProceeedDelivery.Visible = false;
            MPE_PoReturnDeliveryCreate.Show();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        { 
            lblMessageDeliveryCreate.ForeColor = Color.Red;
            MPE_PoReturnDeliveryCreate.Show();
            string message = UC_PurchaseOrderReturnDeliveryCreate.RValidateReturnDelivery();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessageDeliveryCreate.Text = message;
                return;
            }

            List<PPurchaseOrderReturnDeliveryItem_Insert> poReturnDelivery = UC_PurchaseOrderReturnDeliveryCreate.ReadPoReturnDelivery();
            string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderReturnDeliveryCreate", poReturnDelivery);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageDeliveryCreate.Text = Result.Message;
                return;
            }
            
            lblMessage.Text = Result.Message; 
            lblMessage.ForeColor = Color.Green;
            fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
            tbpPoReturn.ActiveTabIndex = 1;
            Clear();
            MPE_PoReturnDeliveryCreate.Hide();
        }
        void Clear()
        {
            divSave.Visible = false;
            UC_PurchaseOrderReturnDeliveryCreate.Clear();
        }
    }
}