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

        public List<PGr_Insert> Gr_Insert
        {
            get
            {
                if (ViewState["PGr_Insert"] == null)
                {
                    ViewState["PGr_Insert"] = new List<PGr_Insert>();
                }
                return (List<PGr_Insert>)ViewState["PGr_Insert"];
            }
            set
            {
                ViewState["PGr_Insert"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageRestrictedQty.Text = "";
            lblMessageGrCreation.Text = "";
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
                Gr_Insert = new List<PGr_Insert>();
                lblGrAsnNumber.Text = PAsnView.AsnNumber;
                foreach (PAsnItem Item in PAsnView.AsnItemS)
                {
                    if (Item.Qty - Item.GrQty != 0)
                    {
                        Gr_Insert.Add(new PGr_Insert()
                        {
                            AsnID = PAsnView.AsnID,
                            AsnItemID = Item.AsnItemID,
                            MaterialCode= Item.Material.MaterialCode,
                            MaterialDescription = Item.Material.MaterialDescription,
                            AsnBalanceQty = Item.Qty - Item.GrQty,
                            UnrestrictedQty = Item.Qty - Item.GrQty,
                            RestrictedQty = 0,
                            BlockedList = new List<PGrBlocked_Insert>()
                        }); ;
                    }
                }
                gvPOAsnGrItem.DataSource = Gr_Insert;
                gvPOAsnGrItem.DataBind();
                //FillGr(PAsnView);
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
            // List<PGr_Insert> GrList = GrRead();
            foreach (PGr_Insert Item in Gr_Insert)
            {
                Item.RemarksHeader = txtRemarksHeader.Text.Trim();
            }
                string result = new BAPI().ApiPut("PurchaseOrder/InsertPOAsnGr", Gr_Insert);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            fillViewPOAsn(PAsnView.AsnID);
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        public void FillGr(PAsn PAsnView)
        {
            ViewState["AsnID"] = Convert.ToInt64(PAsnView.AsnID);

            lblGrAsnNumber.Text = PAsnView.AsnNumber;
            lblGrAsnID.Text = PAsnView.AsnID.ToString(); 

            //gvPOAsnGrItem.DataSource = PAsnView.AsnItemS;
            //gvPOAsnGrItem.DataBind();

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
        //public List<PGr_Insert> GrRead()
        //{
        //    List<PGr_Insert> GrList = new List<PGr_Insert>();

        //    foreach (PAsnItem asnItem in PAsnView.AsnItemS)
        //    {
        //        PGr_Insert pGr = new PGr_Insert();
        //        if (asnItem.GrItem != null)
        //        {
        //            pGr.AsnItemID = asnItem.AsnItemID.ToString();
        //            pGr.AsnID = Convert.ToInt64(asnItem.AsnID);
        //            pGr.DeliveredQty = asnItem.GrItem.ReceivedQty;
        //            pGr.UnrestrictedQty = asnItem.GrItem.UnrestrictedQty;
        //            pGr.RestrictedQty = asnItem.GrItem.RestrictedQty;
        //            pGr.GrRemarks = txtRemarks.Text;

        //            pGr.BlockedList = new List<PGrBlocked_Insert>();
        //            foreach (PGrBlocked pgb in asnItem.GrItem.GrBlocked)
        //            {
        //                PGrBlocked_Insert pGrBlocked_ = new PGrBlocked_Insert();
        //                pGrBlocked_.Qty = pgb.Qty;
        //                pGrBlocked_.Remarks = pgb.Remark;
        //                pGrBlocked_.statusID = pgb.GrBlockedStatus.ProcurementStatusID;
        //                pGr.BlockedList.Add(pGrBlocked_);
        //            }
        //        }
        //        else
        //        {
        //            pGr.AsnItemID = asnItem.AsnItemID.ToString();
        //            pGr.AsnID = Convert.ToInt64(asnItem.AsnID);
        //            pGr.DeliveredQty = asnItem.Qty;
        //            pGr.UnrestrictedQty = asnItem.Qty;
        //            pGr.RestrictedQty = 0;
        //            pGr.GrRemarks = txtRemarks.Text;

        //            pGr.BlockedList = new List<PGrBlocked_Insert>();

        //            PGrBlocked_Insert pGrBlocked_ = new PGrBlocked_Insert();
        //            pGrBlocked_.Qty = 0;
        //            pGrBlocked_.Remarks = "";
        //            pGrBlocked_.statusID = 19;
        //            pGr.BlockedList.Add(pGrBlocked_);

        //            pGrBlocked_ = new PGrBlocked_Insert();
        //            pGrBlocked_.Qty = 0;
        //            pGrBlocked_.Remarks = "";
        //            pGrBlocked_.statusID = 20;
        //            pGr.BlockedList.Add(pGrBlocked_);
        //        }
        //        GrList.Add(pGr);
        //    }
        //    return GrList;
        //}
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
            LinkButton lnkSetRestrictedQty = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkSetRestrictedQty.NamingContainer);

            Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
            HidAsnItemID.Value = lblAsnItemID.Text; 
            
            txtDamagedQty.Text = "0";
            txtMissingQty.Text = "0";

            foreach (PGr_Insert Item in Gr_Insert)
            {
                if (Item.AsnItemID == Convert.ToInt64(HidAsnItemID.Value))
                {
                    txtUnrestrictedQty.Text = Item.AsnBalanceQty.ToString();
                    if (Item.BlockedList.Count != 0)
                    {
                        txtUnrestrictedQty.Text = Item.UnrestrictedQty.ToString();
                        foreach (PGrBlocked_Insert Block in Item.BlockedList)
                        {
                            if (Block.statusID == (short)ProcurementStatus.PoAsnGrBlocked_Missed)
                            {
                                txtMissingQty.Text = Block.Qty.ToString();
                                
                            }
                            else if (Block.statusID == (short)ProcurementStatus.PoAsnGrBlocked_Damaged)
                            {
                                txtDamagedQty.Text = Block.Qty.ToString();
                            }
                        }
                    }
                }
            }
            MPE_UpdateRestrictedQty.Show();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            MPE_UpdateRestrictedQty.Show();
            lblMessageRestrictedQty.ForeColor = Color.Red; 
            if (string.IsNullOrEmpty(txtUnrestrictedQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please UnRestricted Quantity...!"; 
                return;
            }
            if (string.IsNullOrEmpty(txtMissingQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please Missing Quantity...!"; 
                return;
            }
            if (string.IsNullOrEmpty(txtDamagedQty.Text))
            {
                lblMessageRestrictedQty.Text = "Please Damaged Quantity...!"; 
                return;
            }
            
            foreach (PGr_Insert Item in Gr_Insert)
            {
                if (Item.AsnItemID == Convert.ToInt64(HidAsnItemID.Value))
                {
                    Item.BlockedList = new List<PGrBlocked_Insert>();
                    if (Item.AsnBalanceQty != (Convert.ToDecimal(txtUnrestrictedQty.Text) + Convert.ToDecimal(txtMissingQty.Text) + Convert.ToDecimal(txtDamagedQty.Text)))
                    {
                        lblMessageRestrictedQty.Text = "Received Qty Not match with (UnRestricted+Missing+Damage) Quantity...!"; 
                        return;
                    }
                    Item.RemarksItem =txtRemarksItem.Text;
                    Item.UnrestrictedQty = Convert.ToDecimal(txtUnrestrictedQty.Text);
                    Item.RestrictedQty = Convert.ToDecimal(txtMissingQty.Text) + Convert.ToDecimal(txtDamagedQty.Text);
                    PGrBlocked_Insert pgrb = null;
                    if (txtMissingQty.Text.Trim() != "0")
                    {
                        pgrb = new PGrBlocked_Insert();
                        pgrb.Qty = Convert.ToDecimal(txtMissingQty.Text);
                        pgrb.statusID = (short)ProcurementStatus.PoAsnGrBlocked_Missed; 
                        Item.BlockedList.Add(pgrb);
                    }
                    if (txtDamagedQty.Text.Trim() != "0")
                    {
                        pgrb = new PGrBlocked_Insert();
                        pgrb.Qty = Convert.ToDecimal(txtDamagedQty.Text);
                        pgrb.statusID = (short)ProcurementStatus.PoAsnGrBlocked_Damaged; 
                        Item.BlockedList.Add(pgrb); 
                    } 
                }
            } 
            MPE_GrCreate.Show();
            MPE_UpdateRestrictedQty.Hide(); 
            gvPOAsnGrItem.DataSource = Gr_Insert;
            gvPOAsnGrItem.DataBind();
        }
    }
}