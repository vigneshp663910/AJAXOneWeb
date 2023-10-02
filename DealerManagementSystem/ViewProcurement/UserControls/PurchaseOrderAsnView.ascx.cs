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
        //public List<PAsnItem> PAsnItemView
        //{
        //    get
        //    {
        //        if (ViewState["PAsnItemView"] == null)
        //        {
        //            ViewState["PAsnItemView"] = new List<PAsnItem>();
        //        }
        //        return (List<PAsnItem>)ViewState["PAsnItemView"];
        //    }
        //    set
        //    {
        //        ViewState["PAsnItemView"] = value;
        //    }
        //}
        //public List<PGr> GrList
        //{
        //    get
        //    {
        //        if (ViewState["GrList"] == null)
        //        {
        //            ViewState["GrList"] = new PAsn();
        //        }
        //        return (List<PGr>)ViewState["GrList"];
        //    }
        //    set
        //    {
        //        ViewState["GrList"] = value;
        //    }
        //}
        //public List<PAsnItem> AsnPOItemView
        //{
        //    get
        //    {
        //        if (ViewState["AsnPOItemView"] == null)
        //        {
        //            ViewState["AsnPOItemView"] = new PGr();
        //        }
        //        return (List<PAsnItem>)ViewState["AsnPOItemView"];
        //    }
        //    set
        //    {
        //        ViewState["AsnPOItemView"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        public void fillViewPOAsn(long AsnID)
        {
            PAsnView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnByID(null,AsnID)[0];

            lblAsnNumber.Text = PAsnView.AsnNumber;
            lblAsnDate.Text = PAsnView.AsnDate.ToString();
            //lblDealer.Text = PAsnView.PurchaseOrder.Dealer.DealerName;

            //lblPoNumber.Text = PAsnView.PurchaseOrder.PurchaseOrderNumber;
            //lblPoDate.Text = PAsnView.PurchaseOrder.PurchaseOrderDate.ToString();
            //lblVendor.Text = PAsnView.PurchaseOrder.Vendor.DealerName;

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
                UC_GrCreate.FillMaster(PAsnView);
            }
        }
        void ActionControlMange()
        {
            lbGrCreation.Visible = true;
            if(PAsnView.AsnStatus.ProcurementStatusID != (short)ProcurementStatus.AsnGRPending)
            {
                lbGrCreation.Visible = false;
            }
        }
        protected void btnGrCreate_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            Boolean Validation = UC_GrCreate.ValidationItem();
            if (Validation)
            {
                MPE_GrCreate.Show();
                return;
            }
            List<PGr_Insert> GrList = UC_GrCreate.Read();

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
    }
}