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
    public partial class PurchaseOrderASNGrView : System.Web.UI.UserControl
    {
        public PGr GrView
        {
            get
            {
                if (ViewState["GrView"] == null)
                {
                    ViewState["GrView"] = new PGr();
                }
                return (PGr)ViewState["GrView"];
            }
            set
            {
                ViewState["GrView"] = value;
            }
        }
        public List<PGrItem> GrPOItemView
        {
            get
            {
                if (ViewState["GrPOItemView"] == null)
                {
                    ViewState["GrPOItemView"] = new PGr();
                }
                return (List<PGrItem>)ViewState["GrPOItemView"];
            }
            set
            {
                ViewState["GrPOItemView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        public void fillViewPOAsn(long GrID)
        {
            GrView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnGrByID(GrID);

            lblAsnNumber.Text = GrView.ASN.AsnNumber;
            lblAsnDate.Text = GrView.ASN.AsnDate.ToString();
            lblDealer.Text = GrView.ASN.PurchaseOrder.Dealer.DealerName;

            lblPoNumber.Text = GrView.ASN.PurchaseOrder.PurchaseOrderNumber;
            lblPoDate.Text = GrView.ASN.PurchaseOrder.PurchaseOrderDate.ToString();
            lblVendor.Text = GrView.ASN.PurchaseOrder.Vendor.DealerName;

            lblAsnStatus.Text = GrView.Status.GrStatus;

            lblGrNumber.Text = GrView.GrNumber;
            lblGrDate.Text = GrView.GrDate.ToString();
            lblRemarks.Text = GrView.Remarks;

            lblPostedBy.Text = GrView.PostedBy.ContactName;
            lblPostedOn.Text = GrView.PostedOn.ToString();
            lblCancelledBy.Text = (GrView.CancelledBy == null) ? "" : GrView.CancelledBy.ContactName;
            lblCancelledOn.Text = (GrView.CancelledBy == null) ? "" : GrView.CancelledOn.ToString();

            GVGr.DataSource = null;
            GVGr.DataBind();
            GVGr.DataSource = GrView.GrItemS;
            GVGr.DataBind();

            lblPurchaseOrderNumber.Text = GrView.ASN.PurchaseOrder.PurchaseOrderNumber;
            lblOrderTo.Text = GrView.ASN.PurchaseOrder.PurchaseOrderTo.PurchaseOrderTo;
            lblDivision.Text = GrView.ASN.PurchaseOrder.Division.DivisionCode;
            lblRefNo.Text = GrView.ASN.PurchaseOrder.ReferenceNo;

            lblPurchaseOrderDate.Text = GrView.ASN.PurchaseOrder.PurchaseOrderDate.ToString();
            lblOrderType.Text = GrView.ASN.PurchaseOrder.PurchaseOrderType.PurchaseOrderType;
            lblReceivingLocation.Text = GrView.ASN.PurchaseOrder.Location.OfficeName;
            lblPORemarks.Text = GrView.ASN.PurchaseOrder.Remarks;

            lblPODealer.Text = GrView.ASN.PurchaseOrder.Dealer.DealerName;
            lblPOVendor.Text = GrView.ASN.PurchaseOrder.Vendor.DealerName;
            lblExpectedDeliveryDate.Text = GrView.ASN.PurchaseOrder.ExpectedDeliveryDate.ToString();

            GVGrPO.DataSource = null;
            GVGrPO.DataBind();
            GrPOItemView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnGrByIDPOItem(GrID);
            GVGrPO.DataSource = GrPOItemView;
            GVGrPO.DataBind();

            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Gr Cancel")
            {
                MPE_GrCancel.Show();
            }
        }
        void ActionControlMange()
        {
            lbGrCancel.Visible = true;
            if (GrView.Status.GrStatusID != 1)
            {
                lbGrCancel.Visible = false;
            }
        }
        protected void btnGrCancel_Click(object sender, EventArgs e)
        {
            long GrID = GrView.GrID;
            PGr pGr = new PGr();
            pGr.GrID = GrID;
            pGr.Remarks = txtRemarks.Text.Trim();
            string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderAsnGrCancel", pGr);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }

            fillViewPOAsn(GrID);
        }
    }
}