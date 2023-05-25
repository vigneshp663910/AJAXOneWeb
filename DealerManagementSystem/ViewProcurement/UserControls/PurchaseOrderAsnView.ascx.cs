using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class PurchaseOrderAsnView : System.Web.UI.UserControl
    {
        public PAsn PAsnView
        {
            get
            {
                if (ViewState["PAsnView"] == null)
                {
                    Session["PAsnView"] = new PAsn();
                }
                return (PAsn)ViewState["PAsnView"];
            }
            set
            {
                ViewState["PAsnView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        public void fillViewPOAsn(long AsnID)
        {
            PAsnView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnByID(AsnID);

            lblAsnNumber.Text = PAsnView.AsnNumber;
            lblAsnDate.Text = PAsnView.AsnDate.ToString();
            lblDealer.Text = PAsnView.Dealer.DealerName;

            lblPoNumber.Text = PAsnView.PurchaseOrderNumber;
            lblPoDate.Text = PAsnView.PurchaseOrderDate.ToString();
            lblVendor.Text = PAsnView.Vendor.DealerName;

            lblDeliveryNumber.Text = PAsnView.DeliveryNumber;
            lblDeliveryDate.Text = PAsnView.DeliveryDate.ToString();
            lblAsnStatus.Text = PAsnView.AsnStatus.AsnStatus;

            lblLRNo.Text = PAsnView.LRNo;
            lblRemarks.Text = PAsnView.Remarks;

            gvPOAsnItem.DataSource = PAsnView.AsnItemS;
            gvPOAsnItem.DataBind();
            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            //if (lbActions.Text == "Release PO")
            //{

            //}
        }
        void ActionControlMange()
        {

        }
    }
}