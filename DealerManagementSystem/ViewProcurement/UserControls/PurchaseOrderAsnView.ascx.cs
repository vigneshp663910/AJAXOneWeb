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

        public List<PAsnItem> PAsnItemView
        {
            get
            {
                if (ViewState["PAsnItemView"] == null)
                {
                    Session["PAsnItemView"] = new List<PAsnItem>();
                }
                return (List<PAsnItem>)ViewState["PAsnItemView"];
            }
            set
            {
                ViewState["PAsnItemView"] = value;
            }
        }

        public List<PGr> GrList
        {
            get
            {
                if (ViewState["GrList"] == null)
                {
                    Session["GrList"] = new PAsn();
                }
                return (List<PGr>)ViewState["GrList"];
            }
            set
            {
                ViewState["GrList"] = value;
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
            lblDealer.Text = PAsnView.PurchaseOrder.Dealer.DealerName;

            lblPoNumber.Text = PAsnView.PurchaseOrder.PurchaseOrderNumber;
            lblPoDate.Text = PAsnView.PurchaseOrder.PurchaseOrderDate.ToString();
            lblVendor.Text = PAsnView.PurchaseOrder.Vendor.DealerName;

            lblDeliveryNumber.Text = PAsnView.DeliveryNumber;
            lblDeliveryDate.Text = PAsnView.DeliveryDate.ToString();
            lblAsnStatus.Text = PAsnView.AsnStatus.AsnStatus;

            lblGrNumber.Text = PAsnView.Gr.GrNumber;
            lblGrDate.Text = PAsnView.Gr.GrDate.ToString();
            lblLRNo.Text = PAsnView.LRNo;

            lblRemarks.Text = PAsnView.Remarks;

            gvPOAsnItem.DataSource = null;
            gvPOAsnItem.DataBind();
            PAsnItemView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnItemByID(AsnID);
            gvPOAsnItem.DataSource = PAsnItemView;
            gvPOAsnItem.DataBind();

            GVGr.DataSource = null;
            GVGr.DataBind();
            GrList = new BDMS_PurchaseOrder().GetPurchaseOrderAsnGrDetByID(AsnID);
            GVGr.DataSource = GrList;
            GVGr.DataBind();

            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Gr Creation")
            {
                MPE_GrCreate.Show();
                UC_GrCreate.FillMaster(PAsnView);
            }
        }
        void ActionControlMange()
        {

        }

        protected void btnGrCreate_Click(object sender, EventArgs e)
        {

        }
        
    }
}