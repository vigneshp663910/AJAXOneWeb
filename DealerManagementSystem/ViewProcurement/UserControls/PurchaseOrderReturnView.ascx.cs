using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
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
                    ViewState["PPurchaseOrderReturn"] = new PurchaseOrder();
                }
                return (PPurchaseOrderReturn)ViewState["PPurchaseOrderReturn"];
            }
            set
            {
                ViewState["PPurchaseOrderReturn"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessagePoReturn.Text = "";
        }
        public void fillViewPoReturn(long PurchaseOrderReturnID)
        {
            PoReturn = new BPurchaseOrderReturn().GetPurchaseOrderReturnByID(PurchaseOrderReturnID);

            lblPurchaseOrderReturnNumber.Text = PoReturn.PurchaseOrderReturnNumber;
            lblPurchaseOrderReturnDate.Text = PoReturn.PurchaseOrderReturnDate.ToString();
            lblPurchaseOrderReturnStatus.Text = PoReturn.PurchaseOrderReturnStatus.PurchaseOrderReturnStatusDescription;
            lblRemarks.Text = PoReturn.Remarks;

            gvPOReturnItem.DataSource = PoReturn.PurchaseOrderReturnItems;
            gvPOReturnItem.DataBind();
                 
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
    }
}