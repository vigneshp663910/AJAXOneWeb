using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class GrCreate : System.Web.UI.UserControl
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        public void FillMaster(PAsn PAsnView)
        {
            ViewState["AsnID"] = Convert.ToInt64(PAsnView.AsnID);
            lblAsnNumber.Text = PAsnView.AsnNumber;
            lblAsnID.Text = PAsnView.AsnID.ToString();

            gvPOAsnItem.DataSource = null;
            gvPOAsnItem.DataBind();
            PAsnItemView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnItemByID(PAsnView.AsnID);
            gvPOAsnItem.DataSource = PAsnItemView;
            gvPOAsnItem.DataBind();
        }
        protected void btnGrItemAdd_Click(object sender, EventArgs e)
        {
            //lblMessage.ForeColor = Color.Red;
            //lblMessage.Visible = true;
            //string Message = Validation();
            //if (!string.IsNullOrEmpty(Message))
            //{
            //    lblMessage.Text = Message;
            //    return;
            //}
            //Message = ValidationItem();
            //if (!string.IsNullOrEmpty(Message))
            //{
            //    lblMessage.Text = Message;
            //    return;
            //}
            //if (PO_Insert.PurchaseOrderItems == null)
            //{
            //    PO_Insert.PurchaseOrderItems = new List<PPurchaseOrderItem_Insert>();
            //}

            //PPurchaseOrderItem_Insert PoI = ReadItem();
            //PO_Insert.PurchaseOrderItems.Add(PoI);

            //string Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
            //string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
            //string OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
            //string Material = PoI.MaterialCode;
            //string IV_SEC_SALES = "";
            ////string PriceDate = DateTime.Now.ToShortDateString();
            //string PriceDate = "";
            //string IsWarrenty = "false";

            //PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, OrderType, 1, Material, PoI.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);
            //PoI.Price = Mat.CurrentPrice;
            //PoI.DiscountAmount = Mat.Discount;
            //PoI.TaxableAmount = Mat.TaxablePrice;
            //PoI.SGST = Mat.SGST;
            //PoI.SGSTValue = Mat.SGSTValue;
            //PoI.CGST = Mat.CGST;
            //PoI.CGSTValue = Mat.CGSTValue;
            //PoI.CGST = Mat.CGST;
            //PoI.IGSTValue = Mat.IGSTValue;

            //PurchaseOrderItem_Insert.Add(PoI);
            //fillItem();
            //ClearItem();
        }
    }
}