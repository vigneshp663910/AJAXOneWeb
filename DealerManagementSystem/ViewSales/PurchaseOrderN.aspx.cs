using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DealerManagementSystem.ViewSales
{
    public partial class PurchaseOrderN : System.Web.UI.Page
    {
        public List<PDMS_PurchaseOrderN> SDMS_SalesInvoice
        {
            get
            {
                if (Session["DMS_SaleOrderInvoiceMcReport"] == null)
                {
                    Session["DMS_SaleOrderInvoiceMcReport"] = new List<PDMS_PurchaseOrderN>();
                }
                return (List<PDMS_PurchaseOrderN>)Session["DMS_SaleOrderInvoiceMcReport"];
            }
            set
            {
                Session["DMS_SaleOrderInvoiceMcReport"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}