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
    public partial class PurchaseOrderAsnView : System.Web.UI.UserControl
    {
        public PAsn PAsnView
        {
            get
            {
                if (ViewState["PAsnView"] == null)
                {
                    ViewState["PAsnView"] = new PAsn();
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
            PAsnView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnByID(null, AsnID)[0];

            lblAsnNumber.Text = PAsnView.AsnNumber;
            lblAsnDate.Text = PAsnView.AsnDate.ToString();
            lblDeliveryNumber.Text = PAsnView.DeliveryNumber;
            lblDeliveryDate.Text = PAsnView.DeliveryDate.ToString();
            lblAsnStatus.Text = PAsnView.AsnStatus.ProcurementStatus;

            lblGrNumber.Text = (PAsnView.Gr == null) ? "" : PAsnView.Gr.GrNumber;
            lblGrDate.Text = (PAsnView.Gr == null) ? "" : PAsnView.Gr.GrDate.ToString();
            lblLRNo.Text = PAsnView.LRNo;

            lblRemarks.Text = PAsnView.Remarks;

            gvPOAsnItem.DataSource = null;
            gvPOAsnItem.DataBind();
            //List<PAsnItem> PAsnItemView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnItemByID(AsnID);
            gvPOAsnItem.DataSource = PAsnView.AsnItemS;
            gvPOAsnItem.DataBind();

            GVGr.DataSource = null;
            GVGr.DataBind();
            List<PGr> GrList = new BDMS_PurchaseOrder().GetPurchaseOrderAsnGrDetByID(AsnID);
            GVGr.DataSource = GrList;
            GVGr.DataBind();

            lblPurchaseOrderNumber.Text = PAsnView.PurchaseOrder.PurchaseOrderNumber;
            lblOrderTo.Text = PAsnView.PurchaseOrder.PurchaseOrderTo.PurchaseOrderTo;
            lblDivision.Text = PAsnView.PurchaseOrder.Division.DivisionCode;
            lblRefNo.Text = PAsnView.PurchaseOrder.ReferenceNo;

            lblPurchaseOrderDate.Text = PAsnView.PurchaseOrder.PurchaseOrderDate.ToString();
            lblOrderType.Text = PAsnView.PurchaseOrder.PurchaseOrderType.PurchaseOrderType;
            lblReceivingLocation.Text = PAsnView.PurchaseOrder.Location.OfficeName;
            lblPORemarks.Text = PAsnView.PurchaseOrder.Remarks;

            lblPODealer.Text = PAsnView.PurchaseOrder.Dealer.DealerName;
            lblPOVendor.Text = PAsnView.PurchaseOrder.Vendor.DealerName;
            lblExpectedDeliveryDate.Text = PAsnView.PurchaseOrder.ExpectedDeliveryDate.ToString();
            lblGrossAmount.Text = PAsnView.PurchaseOrder.NetAmount.ToString();


            GVAsnPO.DataSource = null;
            GVAsnPO.DataBind();
            List<PAsnItem> AsnPOItemView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnByIDPOItem(AsnID);
            GVAsnPO.DataSource = AsnPOItemView;
            GVAsnPO.DataBind();

            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Gr Creation")
            {
                MPE_GrCreate.Show();
                FillGr(PAsnView);
            }
        }
        void ActionControlMange()
        {
            lbGrCreation.Visible = true;
            if (PAsnView.AsnStatus.ProcurementStatusID != (short)ProcurementStatus.AsnGRPending)
            {
                lbGrCreation.Visible = false;
            }
        }
        protected void btnGrCreate_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            lblMessageGrCreation.ForeColor = Color.Red;
            lblMessageGrCreation.Text = "";
            lblMessageGrCreation.Visible = true;
            Boolean Validation = ValidationGrItem();
            if (Validation)
            {
                MPE_GrCreate.Show();
                return;
            }
            List<PGr_Insert> GrList = GrRead();

            string result = new BAPI().ApiPut("PurchaseOrder/InsertPOAsnGr", GrList);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            fillViewPOAsn(GrList[0].AsnID);
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        public void FillGr(PAsn PAsnView)
        {
            ViewState["AsnID"] = Convert.ToInt64(PAsnView.AsnID);

            lblGrAsnNumber.Text = PAsnView.AsnNumber;
            lblGrAsnID.Text = PAsnView.AsnID.ToString();

            gvPOAsnGrItem.DataSource = null;
            gvPOAsnGrItem.DataBind();

            gvPOAsnGrItem.DataSource = PAsnView.AsnItemS;
            gvPOAsnGrItem.DataBind();

            PAsnView.Gr = new PGr();
            PAsnView.Gr.GrItemS = new List<PGrItem>();

            //GrInsert = new List<PGr_Insert>();

            foreach (GridViewRow row in gvPOAsnGrItem.Rows)
            {
                Label lblAsnID = (Label)row.FindControl("lblAsnID");
                Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
                //Label lblReceivedQty = (Label)row.FindControl("lblReceivedQty");
                Label lblUnrestrictedQty = (Label)row.FindControl("lblUnrestrictedQty");
                Label lblRestrictedQty = (Label)row.FindControl("lblRestrictedQty");

                PAsnView.Gr.GrItemS.Add(new PGrItem()
                {
                    AsnItem = new PAsnItem() { AsnItemID = Convert.ToInt64(lblAsnItemID.Text.Trim()) },
                    //ReceivedQty = Convert.ToDecimal("0" + lblReceivedQty.Text),
                    UnrestrictedQty = Convert.ToDecimal("0" + lblUnrestrictedQty.Text),
                    RestrictedQty = Convert.ToDecimal("0" + lblRestrictedQty.Text),
                    GrBlocked = new List<PGrBlocked>()
                });
            }
        }
        public List<PGr_Insert> GrRead()
        {
            List<PGr_Insert> GrList = new List<PGr_Insert>();

            foreach (PAsnItem asnItem in PAsnView.AsnItemS)
            {
                PGr_Insert pGr = new PGr_Insert();
                if (asnItem.GrItem != null)
                {
                    pGr.AsnItemID = asnItem.AsnItemID.ToString();
                    pGr.AsnID = Convert.ToInt64(asnItem.AsnID);
                    pGr.DeliveredQty = asnItem.GrItem.ReceivedQty;
                    pGr.UnrestrictedQty = asnItem.GrItem.UnrestrictedQty;
                    pGr.RestrictedQty = asnItem.GrItem.RestrictedQty;
                    pGr.GrRemarks = txtRemarks.Text;

                    pGr.BlockedList = new List<PGrBlocked_Insert>();
                    foreach (PGrBlocked pgb in asnItem.GrItem.GrBlocked)
                    {
                        PGrBlocked_Insert pGrBlocked_ = new PGrBlocked_Insert();
                        pGrBlocked_.Qty = pgb.Qty;
                        pGrBlocked_.Remarks = pgb.Remark;
                        pGrBlocked_.statusID = pgb.GrBlockedStatus.ProcurementStatusID;
                        pGr.BlockedList.Add(pGrBlocked_);
                    }
                }
                else
                {
                    pGr.AsnItemID = asnItem.AsnItemID.ToString();
                    pGr.AsnID = Convert.ToInt64(asnItem.AsnID);
                    pGr.DeliveredQty = asnItem.Qty;
                    pGr.UnrestrictedQty = asnItem.Qty;
                    pGr.RestrictedQty = 0;
                    pGr.GrRemarks = txtRemarks.Text;

                    pGr.BlockedList = new List<PGrBlocked_Insert>();

                    PGrBlocked_Insert pGrBlocked_ = new PGrBlocked_Insert();
                    pGrBlocked_.Qty = 0;
                    pGrBlocked_.Remarks = "";
                    pGrBlocked_.statusID = 19;
                    pGr.BlockedList.Add(pGrBlocked_);

                    pGrBlocked_ = new PGrBlocked_Insert();
                    pGrBlocked_.Qty = 0;
                    pGrBlocked_.Remarks = "";
                    pGrBlocked_.statusID = 20;
                    pGr.BlockedList.Add(pGrBlocked_);
                }
                GrList.Add(pGr);
            }
            return GrList;
        }
        public Boolean ValidationGrItem()
        {
            lblMessageGrCreation.ForeColor = Color.Red;
            lblMessageGrCreation.Visible = true;
            lblMessageGrCreation.Text = "";
            Boolean Result = false;
            foreach (GridViewRow row in gvPOAsnGrItem.Rows)
            {
                Label lblAsnID = (Label)row.FindControl("lblAsnID");
                Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
                Label lblQty = (Label)row.FindControl("lblQty");
                Label lblAsnItem = (Label)row.FindControl("lblAsnItem");

                //Label lblReceivedQty = (Label)row.FindControl("lblReceivedQty");
                Label lblUnrestrictedQty = (Label)row.FindControl("lblUnrestrictedQty");
                Label lblRestrictedQty = (Label)row.FindControl("lblRestrictedQty");

                decimal Qty = 0, UnrestrictedQty = 0, RestrictedQty = 0;
                decimal.TryParse(lblQty.Text, out Qty);
                decimal.TryParse(lblUnrestrictedQty.Text, out UnrestrictedQty);
                decimal.TryParse(lblRestrictedQty.Text, out RestrictedQty);

                 
                if (Qty != (UnrestrictedQty + RestrictedQty))
                {
                    lblMessageGrCreation.Text = "Please Equal To Received Qty with (UnRestricted + Restricted) From Item No : " + lblAsnItem.Text;
                    Result = true;
                }
            }
            return Result;
        }
        protected void lnkSetRestrictedQty_Click(object sender, EventArgs e)
        {
            lblMessageGrCreation.Text = string.Empty;
            lblMessageGrCreation.ForeColor = Color.Red;
            lblMessageGrCreation.Visible = true;
            LinkButton lnkSetRestrictedQty = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkSetRestrictedQty.NamingContainer);

            Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
            Label lblAsnID = (Label)row.FindControl("lblAsnID");
            Label lblAsnItem = (Label)row.FindControl("lblAsnItem");
            Label lblQty = (Label)row.FindControl("lblQty");

            HidAsnItemID.Value = lblAsnItemID.Text;
            HidAsnID.Value = lblAsnID.Text;
            HidAsnItem.Value = lblAsnItem.Text;
            HidReceivedQty.Value = lblQty.Text;

            if (string.IsNullOrEmpty(txtMissingQty.Text) && string.IsNullOrEmpty(txtDamagedQty.Text))
            {
                txtUnrestrictedQty.Text = HidReceivedQty.Value;
                txtDamagedQty.Text = "0";
                txtMissingQty.Text = "0";
            }
            MPE_UpdateRestrictedQty.Show();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessageRestrictedQty.Text = "";
            lblMessageRestrictedQty.ForeColor = Color.Red;
            lblMessageRestrictedQty.Visible = true;
            if (string.IsNullOrEmpty(txtUnrestrictedQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please UnRestricted Quantity...!";
                MPE_UpdateRestrictedQty.Show();
                return;
            }
            if (string.IsNullOrEmpty(txtMissingQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please Missing Quantity...!";
                MPE_UpdateRestrictedQty.Show();
                return;
            }
            if (string.IsNullOrEmpty(txtDamagedQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please Damaged Quantity...!";
                MPE_UpdateRestrictedQty.Show();
                return;
            }
            if (Convert.ToDecimal(HidReceivedQty.Value) != (Convert.ToDecimal(txtUnrestrictedQty.Text) + Convert.ToDecimal(txtMissingQty.Text) + Convert.ToDecimal(txtDamagedQty.Text)))
            {
                lblMessageRestrictedQty.Text = "Received Qty Not match with (UnRestricted+Missing+Damage) Quantity...!";
                MPE_UpdateRestrictedQty.Show();
                return;
            }
            foreach (PAsnItem asn in PAsnView.AsnItemS)
            {
                if (asn.AsnItemID == Convert.ToInt64(HidAsnItemID.Value))
                {
                    asn.GrItem = (asn.GrItem == null) ? new PGrItem() : asn.GrItem;
                    asn.GrItem.GrBlocked = new List<PGrBlocked>();
                    PGrBlocked pgrb = new PGrBlocked();
                    pgrb.GrItemID = Convert.ToInt64(HidAsnItemID.Value);
                    pgrb.Qty = Convert.ToDecimal(txtMissingQty.Text);
                    pgrb.GrBlockedStatus = new PProcurementStatus() { ProcurementStatusID = 20 };
                    pgrb.Remark = txtRemark.Text;
                    asn.GrItem.GrBlocked.Add(pgrb);

                    pgrb = new PGrBlocked();
                    pgrb.GrItemID = Convert.ToInt64(HidAsnItemID.Value);
                    pgrb.Qty = Convert.ToDecimal(txtDamagedQty.Text);
                    pgrb.GrBlockedStatus = new PProcurementStatus() { ProcurementStatusID = 19 };
                    pgrb.Remark = txtRemark.Text;
                    asn.GrItem.GrBlocked.Add(pgrb);

                    asn.GrItem.ReceivedQty = Convert.ToDecimal(HidReceivedQty.Value);
                    asn.GrItem.UnrestrictedQty = Convert.ToDecimal(txtUnrestrictedQty.Text);
                    asn.GrItem.RestrictedQty = (Convert.ToDecimal(txtMissingQty.Text) + Convert.ToDecimal(txtDamagedQty.Text));
                }
            }
            MPE_GrCreate.Show();
            MPE_UpdateRestrictedQty.Hide();
            gvPOAsnGrItem.DataSource = null;
            gvPOAsnGrItem.DataBind();

            gvPOAsnGrItem.DataSource = PAsnView.AsnItemS;
            gvPOAsnGrItem.DataBind();
        }
    }
}