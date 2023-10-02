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
        //public List<PAsnItem> PAsnItemView
        //{
        //    get
        //    {
        //        if (ViewState["PAsnItemView"] == null)
        //        {
        //            Session["PAsnItemView"] = new List<PAsnItem>();
        //        }
        //        return (List<PAsnItem>)ViewState["PAsnItemView"];
        //    }
        //    set
        //    {
        //        ViewState["PAsnItemView"] = value;
        //    }
        //}
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
           // PAsnItemView = new BDMS_PurchaseOrder().GetPurchaseOrderAsnItemByID(PAsnView.AsnID);
            gvPOAsnItem.DataSource = PAsnView.AsnItemS;
            gvPOAsnItem.DataBind();
        }
        public List<PGr_Insert> Read()
        {
            List<PGr_Insert> GrList = new List<PGr_Insert>();
            foreach (GridViewRow row in gvPOAsnItem.Rows)
            {
                Label lblAsnID = (Label)row.FindControl("lblAsnID");
                Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
                TextBox txtDeliveredQty = (TextBox)row.FindControl("txtDeliveredQty");
                TextBox txtReceivedQty = (TextBox)row.FindControl("txtReceivedQty");
                TextBox txtDamagedQty = (TextBox)row.FindControl("txtDamagedQty");
                TextBox txtMissingQty = (TextBox)row.FindControl("txtMissingQty");

                GrList.Add(new PGr_Insert()
                {
                    AsnItemID = lblAsnItemID.Text.Trim(),
                    AsnID = Convert.ToInt64(lblAsnID.Text),
                    DeliveredQty = Convert.ToDecimal("0" + txtDeliveredQty.Text),
                    ReceivedQty = Convert.ToDecimal("0" + txtReceivedQty.Text),
                    DamagedQty = Convert.ToDecimal("0" + txtDamagedQty.Text),
                    MissingQty = Convert.ToDecimal("0" + txtMissingQty.Text),
                    GrRemarks = txtRemarks.Text
                });
            }
            return GrList;
        }
        public Boolean ValidationItem()
        {
            Boolean Result = false;
            foreach (GridViewRow row in gvPOAsnItem.Rows)
            {
                Label lblAsnID = (Label)row.FindControl("lblAsnID");
                Label lblAsnItemID = (Label)row.FindControl("lblAsnItemID");
                Label lblQty = (Label)row.FindControl("lblQty");
                TextBox txtDeliveredQty = (TextBox)row.FindControl("txtDeliveredQty");
                TextBox txtReceivedQty = (TextBox)row.FindControl("txtReceivedQty");
                TextBox txtDamagedQty = (TextBox)row.FindControl("txtDamagedQty");
                TextBox txtMissingQty = (TextBox)row.FindControl("txtMissingQty");

                decimal DeliveredQty = 0, ReceivedQty = 0, DamagedQty = 0, MissingQty = 0;
                decimal.TryParse(txtDeliveredQty.Text, out DeliveredQty);
                decimal.TryParse(txtReceivedQty.Text, out ReceivedQty);
                decimal.TryParse(txtDamagedQty.Text, out DamagedQty);
                decimal.TryParse(txtMissingQty.Text, out MissingQty);

                if (Convert.ToDecimal("0" + lblQty.Text) != (ReceivedQty + DamagedQty + MissingQty))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Please Equal To AsnQty with Received,Damage and Missing Qty";
                    lblMessage.Visible = true;
                    Result = true;
                }
                if (Convert.ToDecimal("0" + lblQty.Text) != (DeliveredQty + MissingQty))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Please Equal To AsnQty with Delivered and Missing Qty";
                    lblMessage.Visible = true;
                    Result = true;
                }
            }
            return Result;
        }
    }
}