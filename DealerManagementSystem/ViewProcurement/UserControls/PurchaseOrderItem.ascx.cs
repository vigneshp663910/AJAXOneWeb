using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class PurchaseOrderItem : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {
            Clear();
        }

        void Clear()
        {
        }
        public PPurchaseOrderItem_Insert Read()
        {
            PPurchaseOrderItem_Insert SM = new PPurchaseOrderItem_Insert();
            SM.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SM.MaterialCode =  hdfMaterialCode.Value;
            // SM.SupersedeYN = cbSupersedeYN.Checked;
            SM.Quantity = Convert.ToInt32(txtQty.Text.Trim());  
            return SM;
        }
        public string Validation()
        {
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                return "Please select the Material";
            }
             
            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                return "Please enter the Qty";
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty";
            }
            return "";
        }

        public void FillPoItem(List<PurchaseOrderItem> Items)
        {
            gvPOItem.DataSource = Items;
            gvPOItem.DataBind();
        }
    }
}