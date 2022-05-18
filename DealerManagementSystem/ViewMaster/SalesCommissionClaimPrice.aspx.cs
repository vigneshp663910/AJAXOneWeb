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

namespace DealerManagementSystem.ViewMaster
{
    public partial class SalesCommissionClaimPrice : System.Web.UI.Page
    {
        public List<PSalesCommissionClaimPrice> SalesCommClaimPrice
        {
            get
            {
                if (Session["SalesCommClaimPrice"] == null)
                {
                    Session["SalesCommClaimPrice"] = new List<PSalesCommissionClaimPrice>();
                }
                return (List<PSalesCommissionClaimPrice>)Session["SalesCommClaimPrice"];
            }
            set
            {
                Session["SalesCommClaimPrice"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Sales Commission Claim Price');</script>");
            if (!IsPostBack)
            {
                try
                {
                    new DDLBind(ddlPlant, new BDMS_Master().GetPlant(null, null), "PlantCode", "PlantID");
                    //new DDLBind(ddlMaterial.SelectedValue,new BDMS_Material().GetMaterialListSQL(null, null, null, null, null), "MaterialCode", "MaterialID");
                    GetSalesCommissionClaimPrice();
                }
                catch (Exception e1)
                {
                    DisplayErrorMessage(e1);
                }
            }
        }
        //protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetSalesCommissionClaimPrice();
        //}
        //protected void ddlMaterial_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GetSalesCommissionClaimPrice();
        //}

        protected void ibtnSalCommClaimPriceArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvSalCommClaimPrice.PageIndex > 0)
            {
                gvSalCommClaimPrice.PageIndex = gvSalCommClaimPrice.PageIndex - 1;
                SalesCommClaimPriceBind(gvSalCommClaimPrice, lblRowCountSalCommClaimPrice, SalesCommClaimPrice);
            }
        }
        protected void ibtnSalCommClaimPriceArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvSalCommClaimPrice.PageCount > gvSalCommClaimPrice.PageIndex)
            {
                gvSalCommClaimPrice.PageIndex = gvSalCommClaimPrice.PageIndex + 1;
                SalesCommClaimPriceBind(gvSalCommClaimPrice, lblRowCountSalCommClaimPrice, SalesCommClaimPrice);
            }
        }
        protected void btnSalCommClaimPriceSearch_Click(object sender, EventArgs e)
        {
            GetSalesCommissionClaimPrice();
        }
        protected void gvSalCommClaimPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSalCommClaimPrice.PageIndex = e.NewPageIndex;
            SalesCommClaimPriceBind(gvSalCommClaimPrice, lblRowCountSalCommClaimPrice, SalesCommClaimPrice);
        }
        
        void SalesCommClaimPriceBind(GridView gv, Label lbl, List<PSalesCommissionClaimPrice> Mat)
        {
            gv.DataSource = Mat;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Mat.Count;
        }
        void DisplayErrorMessage(Exception e1)
        {
            lblMessage.Text = e1.ToString();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }
        void DisplayErrorMessage(String Message)
        {
            lblMessage.Text = Message;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }
        void DisplayMessage(String Message)
        {
            lblMessage.Text = Message;
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
        }


        private void GetSalesCommissionClaimPrice()
        {
            try
            {
                int? PlantID = ddlPlant.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPlant.SelectedValue);
                //int? MaterailID = ddlMaterial.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMaterial.SelectedValue);
                string Materail = txtMaterial.Text.Trim();

                SalesCommClaimPrice = new BSalesCommissionClaim().GetSalesCommissionClaimPrice(PlantID, Materail);
                gvSalCommClaimPrice.PageIndex = 0;
                gvSalCommClaimPrice.DataSource = SalesCommClaimPrice;
                gvSalCommClaimPrice.DataBind();

                if (SalesCommClaimPrice.Count == 0)
                {
                    lblRowCountSalCommClaimPrice.Visible = false;
                    ibtnSalCommClaimPriceArrowLeft.Visible = false;
                    ibtnSalCommClaimPriceArrowRight.Visible = false;

                }
                else
                {
                    lblRowCountSalCommClaimPrice.Visible = true;
                    ibtnSalCommClaimPriceArrowLeft.Visible = true;
                    ibtnSalCommClaimPriceArrowRight.Visible = true;
                    lblRowCountSalCommClaimPrice.Text = (((gvSalCommClaimPrice.PageIndex) * gvSalCommClaimPrice.PageSize) + 1) + " - " + (((gvSalCommClaimPrice.PageIndex) * gvSalCommClaimPrice.PageSize) + gvSalCommClaimPrice.Rows.Count) + " of " + SalesCommClaimPrice.Count;
                }
            }
            catch (Exception e1)
            {
                DisplayErrorMessage(e1);
            }
        }
    }
}