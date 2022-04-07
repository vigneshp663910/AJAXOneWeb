using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class AddLeadProduct : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }
        public void FillMaster(PLead Lead)
        {
            new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            ddlProductType.SelectedValue = Convert.ToString(Lead.ProductType.ProductTypeID);
            ddlProductType.Enabled = false;
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, 1, Convert.ToInt32(ddlProductType.SelectedValue), null), "Product", "ProductID");
        }
        public PLeadProduct ReadProduct()
        {
            PLeadProduct Lead = new PLeadProduct();
            Lead.ProductType = new PProductType { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
            Lead.Product = new PProduct { ProductID = Convert.ToInt32(ddlProduct.SelectedValue) }; 
            Lead.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim()); 
            Lead.Remark = txtRemark.Text;

            Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };
            return Lead;
        }

        public string ValidationProduct()
        {
            string Message = "";
            ddlProductType.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver; 
            txtQuantity.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver;

            if (ddlProductType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Product Type";
                ddlProductType.BorderColor = Color.Red;
            } 
            if (ddlProduct.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Product";
                ddlProduct.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtQuantity.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Quantity";
                txtQuantity.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Remark";
                txtRemark.BorderColor = Color.Red;
            }
            return Message;
        }

        protected void FillProduct(object sender, EventArgs e)
        {  
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, 1, Convert.ToInt32(ddlProductType.SelectedValue), null), "Product", "ProductID");
        }
    }
}