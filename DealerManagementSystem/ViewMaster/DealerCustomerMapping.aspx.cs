using System;
using System.Collections.Generic;
using Business;
using Properties;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerCustomerMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillCustomer();
        }
        void FillCustomer()
        {
            int? DealerID = null;
            string DealerCode = null;
            if (string.IsNullOrEmpty(txtDealerCode.Text.Trim()))
            {
                DealerCode = txtDealerCode.Text.Trim();
            }
            List<PDMS_Dealer> Dealer = new BDMS_Customer().GetDealerCustomer(DealerID, DealerCode);
            gvCustomer.DataSource = Dealer;
            gvCustomer.DataBind();
        }
        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomer.PageIndex = e.NewPageIndex;
            FillCustomer();
        }
    }
}