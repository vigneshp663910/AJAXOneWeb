using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class PresalesMastersCompetatiorData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Application');</script>");
            if (!IsPostBack)
            {
                try
                {
                    GetMake();
                    GetProductType();
                    GetProduct();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }

        private void GetMake()
        {
            int? MakeID = (int?)null;
            string Make = (string)null;

            List<PMake> make = new BDMS_Master().GetMake(MakeID, Make);
            gvMake.DataSource = make;
            gvMake.DataBind();
            if (make.Count == 0)
            {
                PMake pMake = new PMake();
                make.Add(pMake);
                gvMake.DataSource = make;
                gvMake.DataBind();
            }
        }

        protected void gvMake_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetMake();
            gvMake.PageIndex = e.NewPageIndex;
            gvMake.DataBind();
        }

        protected void BtnAddOrUpdateMake_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddOrUpdateMake = (Button)gvMake.FooterRow.FindControl("BtnAddOrUpdateMake");

                string Make = ((TextBox)gvMake.FooterRow.FindControl("txtMake")).Text.Trim();
                if (string.IsNullOrEmpty(Make))
                {
                    lblMessage.Text = "Please enter Make";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateMake.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateMake(null, Make, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        GetMake();
                        lblMessage.Text = "Make created successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Make already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Make not created successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateMake(Convert.ToInt32(HiddenID.Value), Make, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        GetMake();
                        lblMessage.Text = "Make updated successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Make already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Make not updated successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnMakeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkMakeEdit = (LinkButton)sender;
                TextBox txtMake = (TextBox)gvMake.FooterRow.FindControl("txtMake");
                Button BtnAddOrUpdateMake = (Button)gvMake.FooterRow.FindControl("BtnAddOrUpdateMake");
                GridViewRow row = (GridViewRow)(lnkMakeEdit.NamingContainer);
                string Make = ((Label)row.FindControl("lblMake")).Text.Trim();
                txtMake.Text = Make;
                HiddenID.Value = Convert.ToString(lnkMakeEdit.CommandArgument);
                BtnAddOrUpdateMake.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnMakeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lnkBtnMakeDelete = (LinkButton)sender;
                int MakeID = Convert.ToInt32(lnkBtnMakeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnMakeDelete.NamingContainer);
                string Make = ((Label)row.FindControl("lblMake")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateMake(MakeID, Make, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    GetMake();
                    lblMessage.Text = "Make deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Make not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        private void GetProductType()
        {
            int? ProductTypeID = (int?)null;
            string ProductType = (string)null;

            List<PProductType> productType = new BDMS_Master().GetProductType(ProductTypeID, ProductType);
            gvProductType.DataSource = productType;
            gvProductType.DataBind();
            if (productType.Count == 0)
            {
                PProductType pProductType = new PProductType();
                productType.Add(pProductType);
                gvProductType.DataSource = pProductType;
                gvProductType.DataBind();
            }
        }

        protected void gvProductType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetProductType();
            gvProductType.PageIndex = e.NewPageIndex;
            gvProductType.DataBind();
        }

        protected void BtnAddOrUpdateProductType_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddOrUpdateProductType = (Button)gvProductType.FooterRow.FindControl("BtnAddOrUpdateProductType");

                string ProductType = ((TextBox)gvProductType.FooterRow.FindControl("txtProductType")).Text.Trim();
                if (string.IsNullOrEmpty(ProductType))
                {
                    lblMessage.Text = "Please enter Product Type";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateProductType.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateProductType(null, ProductType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        GetProductType();
                        lblMessage.Text = "Product Type created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product Type already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product Type not created successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateProductType(Convert.ToInt32(HiddenID.Value), ProductType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        GetProductType();
                        lblMessage.Text = "Product Type updated successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product Type already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product Type not updated successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnProductTypeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnProductTypeEdit = (LinkButton)sender;
                TextBox txtProductType = (TextBox)gvProductType.FooterRow.FindControl("txtProductType");
                Button BtnAddOrUpdateProductType = (Button)gvProductType.FooterRow.FindControl("BtnAddOrUpdateProductType");
                GridViewRow row = (GridViewRow)(lnkBtnProductTypeEdit.NamingContainer);
                string ProductType = ((Label)row.FindControl("lblProductType")).Text.Trim();
                txtProductType.Text = ProductType;
                HiddenID.Value = Convert.ToString(lnkBtnProductTypeEdit.CommandArgument);
                BtnAddOrUpdateProductType.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnProductTypeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lnkBtnProductTypeDelete = (LinkButton)sender;
                int ProductTypeID = Convert.ToInt32(lnkBtnProductTypeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnProductTypeDelete.NamingContainer);
                string ProductType = ((Label)row.FindControl("lblProductType")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateProductType(ProductTypeID, ProductType, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    GetProductType();
                    lblMessage.Text = "Product Type deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Product Type not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        private void GetProduct()
        {
            int? ProductID = (int?)null;
            string Product = (string)null;

            List<PProduct> product = new BDMS_Master().GetProduct(ProductID, Product);
            gvProduct.DataSource = product;
            gvProduct.DataBind();
            if (product.Count == 0)
            {
                PProduct pProduct = new PProduct();
                product.Add(pProduct);
                gvProduct.DataSource = pProduct;
                gvProduct.DataBind();
            }
        }

        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetProduct();
            gvProduct.PageIndex = e.NewPageIndex;
            gvProduct.DataBind();
        }

        protected void BtnAddOrUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddOrUpdateProduct = (Button)gvProduct.FooterRow.FindControl("BtnAddOrUpdateProduct");

                string Product = ((TextBox)gvProduct.FooterRow.FindControl("txtProduct")).Text.Trim();
                if (string.IsNullOrEmpty(Product))
                {
                    lblMessage.Text = "Please enter Product";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateProduct.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateProduct(null, Product, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        GetProduct();
                        lblMessage.Text = "Product created successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product not created successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateProduct(Convert.ToInt32(HiddenID.Value), Product, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        GetProduct();
                        lblMessage.Text = "Product updated successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product not updated successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnProductEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnProductEdit = (LinkButton)sender;
                TextBox txtProduct = (TextBox)gvProduct.FooterRow.FindControl("txtProduct");
                Button BtnAddOrUpdateProduct = (Button)gvProduct.FooterRow.FindControl("BtnAddOrUpdateProduct");
                GridViewRow row = (GridViewRow)(lnkBtnProductEdit.NamingContainer);
                string Make = ((Label)row.FindControl("lblProduct")).Text.Trim();
                txtProduct.Text = Make;
                HiddenID.Value = Convert.ToString(lnkBtnProductEdit.CommandArgument);
                BtnAddOrUpdateProduct.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnProductDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lnkBtnProductDelete = (LinkButton)sender;
                int ProductID = Convert.ToInt32(lnkBtnProductDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnProductDelete.NamingContainer);
                string Product = ((Label)row.FindControl("lblProduct")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateProduct(ProductID, Product, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    GetProduct();
                    lblMessage.Text = "Product deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Product not deleted successfully";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
    }
}