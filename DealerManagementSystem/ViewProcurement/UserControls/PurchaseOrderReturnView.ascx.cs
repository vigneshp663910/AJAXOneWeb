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
                    ViewState["PPurchaseOrderReturn"] = new PurchaseOrderReturn();
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
            lblMessagePoReturn.Text = "";
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
            lbPoReturnCancel.Visible = true;
            if (PoReturn.PurchaseOrderReturnStatus.ProcurementStatusID != (short)ProcurementStatus.PoReturnDraft)
            {
                lbPoReturnCancel.Visible = false;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "PO Return Cancel")
            {
                MPE_PoReturnCancel.Show();
            }
            if(lbActions.Text == "Create Delivery")
            {
                Clear();
                lblMessagePoReturnDeliveryCreate.Text = "";
                lblMessagePoReturnDeliveryCreate.Visible = false;
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
        protected void btnPoReturnCancel_Click(object sender, EventArgs e)
        {
            long PurchaseOrderReturnID = PoReturn.PurchaseOrderReturnID;
            PPurchaseOrder PurchaseOrderReturn = new PPurchaseOrder();
            PurchaseOrderReturn.PurchaseOrderID = PurchaseOrderReturnID;
            PurchaseOrderReturn.Remarks = txtRemarks.Text.Trim();
            string result = new BAPI().ApiPut("PurchaseOrder/CancelPurchaseOrderReturn", PurchaseOrderReturn);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessagePoReturnCancel.Text = Result.Message;
                return;
            }
            fillViewPoReturn(PurchaseOrderReturnID);
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
            lblMessagePoReturnDeliveryCreate.Text = "";
            UC_PurchaseOrderReturnDeliveryCreate.ReadPoReturnItem();
            divSave.Visible = true;
            divProceeedDelivery.Visible = false;
            MPE_PoReturnDeliveryCreate.Show();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessagePoReturnDeliveryCreate.Visible = true;
            lblMessagePoReturnDeliveryCreate.ForeColor = Color.Red;
            MPE_PoReturnDeliveryCreate.Show();
            string message = UC_PurchaseOrderReturnDeliveryCreate.RValidateReturnDelivery();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessagePoReturnDeliveryCreate.Text = message;
                return;
            }

            List<PPurchaseOrderReturnDeliveryItem_Insert> poReturnDelivery = UC_PurchaseOrderReturnDeliveryCreate.ReadPoReturnDelivery();
            string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderReturnDeliveryCreate", poReturnDelivery);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessagePoReturnDeliveryCreate.Text = Result.Message;
                return;
            }
            
            lblMessagePoReturn.Text = Result.Message;
            lblMessagePoReturn.Visible = true;
            lblMessagePoReturn.ForeColor = Color.Green;
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