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
    public partial class SalesCommissionClaimPrice : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_SalesCommissionClaimPrice; } }
        //public List<PSalesCommissionClaimPrice> SalesCommClaimPrice
        //{
        //    get
        //    {
        //        if (Session["SalesCommClaimPrice"] == null)
        //        {
        //            Session["SalesCommClaimPrice"] = new List<PSalesCommissionClaimPrice>();
        //        }
        //        return (List<PSalesCommissionClaimPrice>)Session["SalesCommClaimPrice"];
        //    }
        //    set
        //    {
        //        Session["SalesCommClaimPrice"] = value;
        //    }
        //}
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Sales Commission Claim Price');</script>");
            if (!IsPostBack)
            {
                try
                {
                    //new DDLBind(ddlPlant, new BDMS_Master().GetPlant(null, null), "PlantCode", "PlantID");
                    //new DDLBind(ddlMaterial.SelectedValue,new BDMS_Material().GetMaterialListSQL(null, null, null, null, null), "MaterialCode", "MaterialID");
                    GetSalesCommissionClaimPrice();
                }
                catch (Exception e1)
                {
                    DisplayErrorMessage(e1);
                }
            }
        }
 

        protected void ibtnSalCommClaimPriceArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvSalCommClaimPrice.PageIndex > 0)
            {
                gvSalCommClaimPrice.PageIndex = gvSalCommClaimPrice.PageIndex - 1;
                GetSalesCommissionClaimPrice();
            }
        }
        protected void ibtnSalCommClaimPriceArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvSalCommClaimPrice.PageCount > gvSalCommClaimPrice.PageIndex)
            {
                gvSalCommClaimPrice.PageIndex = gvSalCommClaimPrice.PageIndex + 1;
                GetSalesCommissionClaimPrice();
            }
        }
        protected void btnSalCommClaimPriceSearch_Click(object sender, EventArgs e)
        {
            gvSalCommClaimPrice.PageIndex = 0;
            GetSalesCommissionClaimPrice();
        }
        protected void gvSalCommClaimPrice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSalCommClaimPrice.PageIndex = e.NewPageIndex;
            GetSalesCommissionClaimPrice();
        }       
       
        void DisplayErrorMessage(Exception e1)
        {
            lblMessage.Text = e1.ToString();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }
        
        private void GetSalesCommissionClaimPrice()
        {
            try
            {
                //int? PlantID = ddlPlant.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPlant.SelectedValue);
                //int? MaterailID = ddlMaterial.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMaterial.SelectedValue);
                string Materail = null;
                if (!string.IsNullOrEmpty(txtMaterial.Text))
                {
                    Materail = txtMaterial.Text.Trim();
                }

                //SalesCommClaimPrice = new BSalesCommissionClaim().GetSalesCommissionClaimPrice(PlantID, Materail);
                List<PSalesCommissionClaimPrice> SalesCommClaimPrice = new BSalesCommissionClaim().GetSalesCommissionClaimPrice(Materail);

                if (SalesCommClaimPrice.Count == 0)
                {
                    SalesCommClaimPrice.Add(new PSalesCommissionClaimPrice());
                    lblRowCountSalCommClaimPrice.Visible = false;
                    ibtnSalCommClaimPriceArrowLeft.Visible = false;
                    ibtnSalCommClaimPriceArrowRight.Visible = false;
                    gvSalCommClaimPrice.DataSource = SalesCommClaimPrice;
                    gvSalCommClaimPrice.DataBind();
                }
                else
                { 
                    gvSalCommClaimPrice.DataSource = SalesCommClaimPrice;
                    gvSalCommClaimPrice.DataBind();

                    lblRowCountSalCommClaimPrice.Visible = true;
                    ibtnSalCommClaimPriceArrowLeft.Visible = true;
                    ibtnSalCommClaimPriceArrowRight.Visible = true;
                    lblRowCountSalCommClaimPrice.Text = (((gvSalCommClaimPrice.PageIndex) * gvSalCommClaimPrice.PageSize) + 1) + " - " + (((gvSalCommClaimPrice.PageIndex) * gvSalCommClaimPrice.PageSize) + gvSalCommClaimPrice.Rows.Count) + " of " + SalesCommClaimPrice.Count;
                }

                //DropDownList ddlGPlant = gvSalCommClaimPrice.FooterRow.FindControl("ddlGPlant") as DropDownList;
                //new DDLBind(ddlGPlant, new BDMS_Master().GetPlant(null, null), "PlantCode", "PlantID", true, "Select Plant");
            }
            catch (Exception e1)
            {
                DisplayErrorMessage(e1);
            }
            ActionControlMange();
        }
        protected void lnkBtnSalCommClaimPriceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnSalCommClaimPriceEdit = (LinkButton)sender;
                DropDownList ddlGPlant = (DropDownList)gvSalCommClaimPrice.FooterRow.FindControl("ddlGPlant");
                //TextBox txtMaterail = (TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtMaterail");
                TextBox txtMaterailCode = (TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtMaterailCode");
                TextBox txtPercentage = (TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtPercentage");
                TextBox txtAmount = (TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtAmount");
                //CheckBox chkbxFIsActive = (CheckBox)gvSalCommClaimPrice.FooterRow.FindControl("chkbxFIsActive");
                Button BtnAddOrUpdateSalCommClaimPrice = (Button)gvSalCommClaimPrice.FooterRow.FindControl("BtnAddOrUpdateSalCommClaimPrice");
                GridViewRow row = (GridViewRow)(lnkBtnSalCommClaimPriceEdit.NamingContainer);
                //Label lblPlantID = (Label)row.FindControl("lblPlantID");
                //ddlGPlant.SelectedValue = lblPlantID.Text;
                //ddlGPlant.Enabled = false;
                //Label lblMaterial = (Label)row.FindControl("lblMaterial");
                //txtMaterail.Text = lblMaterial.Text;
                Label lblMaterialCode = (Label)row.FindControl("lblMaterialCode");
                txtMaterailCode.Text = lblMaterialCode.Text;
                txtMaterailCode.Enabled = false;
                Label lblPercentage = (Label)row.FindControl("lblPercentage");
                txtPercentage.Text = lblPercentage.Text;
                Label lblAmount = (Label)row.FindControl("lblAmount");
                txtAmount.Text = lblAmount.Text;
                //CheckBox chkbxGIsActive = (CheckBox)row.FindControl("chkbxGIsActive");
                //if(chkbxGIsActive.Checked)
                //{
                //    chkbxFIsActive.Checked = true;
                //}
                //else if(!chkbxGIsActive.Checked)
                //{
                //    chkbxFIsActive.Checked = false;
                //}
                //chkbxFIsActive.Enabled = true;
                HiddenID.Value = Convert.ToString(lnkBtnSalCommClaimPriceEdit.CommandArgument);
                BtnAddOrUpdateSalCommClaimPrice.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void lnkBtnSalCommClaimPriceDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean success = true;
                LinkButton lnkBtnSalCommClaimPriceDelete = (LinkButton)sender;
                int SalesCommissionClaimPriceID = Convert.ToInt32(lnkBtnSalCommClaimPriceDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnSalCommClaimPriceDelete.NamingContainer);
                int SalesCommissionClaimPriceID1 = Convert.ToInt32(((Label)row.FindControl("lblSalesCommissionClaimPriceID")).Text.Trim());
                //int PlantID = Convert.ToInt32(((Label)row.FindControl("lblPlantID")).Text.Trim());
                int MaterialID = Convert.ToInt32(((Label)row.FindControl("lblMaterialID")).Text.Trim());
                decimal Percentage = Convert.ToDecimal(((Label)row.FindControl("lblPercentage")).Text.Trim());
                decimal Amount = Convert.ToDecimal(((Label)row.FindControl("lblAmount")).Text.Trim());
                bool IsActive = Convert.ToBoolean(0);

                //success = new BSalesCommissionClaim().InsertOrUpdateSalesCommissionClaimPrice(SalesCommissionClaimPriceID, PlantID, MaterialID, Percentage, Amount, PSession.User.UserID, IsActive);
                success = new BSalesCommissionClaim().InsertOrUpdateSalesCommissionClaimPrice(SalesCommissionClaimPriceID, MaterialID, Percentage, Amount, PSession.User.UserID, IsActive);
                if (success == true)
                {
                    HiddenID.Value = null;
                    GetSalesCommissionClaimPrice();
                    lblMessage.Text = "Sales Commission Claim Price deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else if (success == false)
                {
                    lblMessage.Text = "Sales Commission Claim Price not deleted successfully";
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
        protected void BtnAddOrUpdateSalCommClaimPrice_Click(object sender, EventArgs e)
        {
            try
            {

                int SalesCommissionClaimPriceID = 0;
               
               
                decimal Percentage = 0;
                decimal Amount = 0;
                bool IsActive = true;
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                Boolean Success = true;
                Button BtnAddOrUpdateCountry = (Button)gvSalCommClaimPrice.FooterRow.FindControl("BtnAddOrUpdateSalCommClaimPrice");
                 
                string MaterailCode = ((TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtMaterailCode")).Text.Trim();
                if (string.IsNullOrEmpty(MaterailCode))
                {
                    lblMessage.Text = "Please enter Materail Code.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                List <PDMS_Material> MM = new BDMS_Material().GetMaterialListSQL(null, MaterailCode, null, null, "1");

                if(MM.Count == 0)
                {
                    lblMessage.Text = "Materail does not exists.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                TextBox txtPercentage = (TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtPercentage");
                TextBox txtAmount = (TextBox)gvSalCommClaimPrice.FooterRow.FindControl("txtAmount");

                txtPercentage.Text = txtPercentage.Text.Replace("0.00", "0"); 
                txtAmount.Text = txtAmount.Text.Replace("0.00", "0");

                Percentage = Convert.ToDecimal("0" + txtPercentage.Text.Trim());
                Amount = Convert.ToDecimal("0" + txtAmount.Text.Trim());

                if ((Percentage == 0) && (Amount == 0))
                {
                    lblMessage.Text = "Please enter Amount or Percentage.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                } 
                if (Percentage != 0 && Amount != 0)
                {
                    lblMessage.Text = "Please enter either Amount or Percentage.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                 
                int MaterialID =0;
                
                if (BtnAddOrUpdateCountry.Text == "Add")
                {
                    Success = new BSalesCommissionClaim().InsertOrUpdateSalesCommissionClaimPrice(SalesCommissionClaimPriceID, Convert.ToInt32(MM[0].MaterialID), Percentage, Amount, PSession.User.UserID, IsActive);
                    if (Success == true)
                    {
                        GetSalesCommissionClaimPrice();
                        lblMessage.Text = "Sales Commission Claim Price is added successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Sales Commission Claim Price is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Sales Commission Claim Price not created successfully.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    SalesCommissionClaimPriceID = Convert.ToInt32(HiddenID.Value);
                    Success = new BSalesCommissionClaim().InsertOrUpdateSalesCommissionClaimPrice(SalesCommissionClaimPriceID, Convert.ToInt32(MM[0].MaterialID), Percentage, Amount, PSession.User.UserID, IsActive);
                    if (Success == true)
                    {
                        GetSalesCommissionClaimPrice();
                        lblMessage.Text = "Sales Commission Claim Price is updated successfully.";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (Success == false)
                    {
                        lblMessage.Text = "Sales Commission Claim Price is already found.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Sales Commission Claim Price not updated successfully.";
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
        void ActionControlMange()
        {
            

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SalesClaimPriceCreateAndEdit).Count() == 0)
            {
                gvSalCommClaimPrice.FooterRow.Visible = false;
                gvSalCommClaimPrice.Columns[5].Visible = false;
            }
        }
    }
}