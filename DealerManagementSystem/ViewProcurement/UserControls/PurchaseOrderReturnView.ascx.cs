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
        public List<PPurchaseOrderReturnDelivery> PurchaseOrderReturnDeliveryList
        {
            get
            {
                if (ViewState["PurchaseOrderReturnDelivery"] == null)
                {
                    ViewState["PurchaseOrderReturnDelivery"] = new List<PPurchaseOrderReturnDelivery>();
                }
                return (List<PPurchaseOrderReturnDelivery>)ViewState["PurchaseOrderReturnDelivery"];
            }
            set
            {
                ViewState["PurchaseOrderReturnDelivery"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessagePoReturn.Text = "";
        }
        public void fillViewPoReturn(long PurchaseOrderReturnID)
        {
            PoReturn = new BDMS_PurchaseOrder().GetPurchaseOrderReturnByID(PurchaseOrderReturnID);

            lblPurchaseOrderReturnNumber.Text = PoReturn.PurchaseOrderReturnNumber;
            lblPurchaseOrderReturnDate.Text = PoReturn.PurchaseOrderReturnDate.ToString();
            lblPurchaseOrderReturnStatus.Text = PoReturn.PurchaseOrderReturnStatus.PurchaseOrderReturnStatusDescription;
            lblRemarks.Text = PoReturn.Remarks;

            gvPOReturnItem.DataSource = PoReturn.PurchaseOrderReturnItems;
            gvPOReturnItem.DataBind();

            gvPoReturnDelivery.DataSource = null;
            gvPoReturnDelivery.DataBind();
            PurchaseOrderReturnDeliveryList = new BDMS_PurchaseOrder().GetPurchaseOrderReturnDeliveryByPoReturnID(PurchaseOrderReturnID);
            gvPoReturnDelivery.DataSource = PurchaseOrderReturnDeliveryList;
            gvPoReturnDelivery.DataBind();

            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbPoReturnCancel.Visible = true;
            if (PoReturn.PurchaseOrderReturnStatus.PurchaseOrderReturnStatusID != 1)
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
                Panel pnlPoReturnDeliveryCreate = (Panel)UC_PurchaseOrderReturnDeliveryCreate.FindControl("pnlPoReturnDeliveryCreate");
                pnlPoReturnDeliveryCreate.Visible = true;
                
                PnlPurchaseOrderReturnView.Visible = false;
                PnlPurchaseOrderReturnDeliveryCreate.Visible = true;
                UC_PurchaseOrderReturnDeliveryCreate.fillPOReturnItem(PoReturn.PurchaseOrderReturnID);
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
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    string message = UC_PurchaseOrderReturnDeliveryCreate.Validation();
        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        lblMessagePoReturnDeliveryCreate.Text = message;
        //        lblMessagePoReturnDeliveryCreate.Visible = true;
        //        lblMessagePoReturnDeliveryCreate.ForeColor = Color.Red;
        //        return;
        //    }
        //    List<PPurchaseOrderReturnDeliveryItem_Insert> poReturnDelivery = UC_PurchaseOrderReturnDeliveryCreate.Read();
        //    string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderReturnDeliveryCreate", poReturnDelivery);
        //    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

        //    if (Result.Status == PApplication.Failure)
        //    {
        //        lblMessagePoReturnDeliveryCreate.Text = Result.Message;
        //        return;
        //    }
        //    //divList.Visible = false;
        //    //divPoReturnDetailsView.Visible = true;
        //    //divPurchaseOrderReturnDeliveryCreate.Visible = false;
        //    //Label lblMessagePOReturn = (Label)UC_PurchaseOrderReturnView.FindControl("lblMessagePOReturn");
        //    //lblMessagePOReturn.Text = Result.Message;
        //    //lblMessagePOReturn.Visible = true;
        //    //lblMessagePOReturn.ForeColor = Color.Green;
        //    //UC_PurchaseOrderReturnView.fillViewPoReturn(Convert.ToInt64(Result.Data));
        //}
        protected void btnPurchaseOrderReturnDeliveryCreateBack_Click(object sender, EventArgs e)
        {
            PnlPurchaseOrderReturnView.Visible = true;
            Panel pnlPoReturnDeliveryCreate = (Panel)UC_PurchaseOrderReturnDeliveryCreate.FindControl("pnlPoReturnDeliveryCreate");
            pnlPoReturnDeliveryCreate.Visible = false;
            PnlPurchaseOrderReturnDeliveryCreate.Visible = false;
        }
    }
}