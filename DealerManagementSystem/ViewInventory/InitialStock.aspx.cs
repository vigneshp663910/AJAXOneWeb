using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewInventory
{
    public partial class InitialStock : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewInventory_InitialStock; } }
        public List<PDMS_Material> Mat
        {
            get
            {
                if (ViewState["Material"] == null)
                {
                    ViewState["Material"] = new List<PDMS_Material>();
                }
                return (List<PDMS_Material>)ViewState["Material"];
            }
            set
            {
                ViewState["Material"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnMaterialSearch_Click(object sender, EventArgs e)
        {
            FillMaterial();
        }
        void FillMaterial()
        {
            try
            {
                int? DivisionID = ddlDivisionMC.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivisionMC.SelectedValue);
                int? ModelID = ddlMaterialModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMaterialModel.SelectedValue);

                // Boolean? IsActive = ddlMaterialStatus.SelectedValue==""? (Boolean?)null: Convert.ToBoolean(ddlMaterialStatus.SelectedValue) ;
                Mat = new BDMS_Material().GetMaterialListSQL(null, txtMaterialCode.Text.Trim(), DivisionID, ModelID, ddlMaterialStatus.SelectedValue);
                gvMaterial.PageIndex = 0;
                gvMaterial.DataSource = Mat;
                gvMaterial.DataBind();

                if (Mat.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnMaterialArrowLeft.Visible = false;
                    ibtnMaterialArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnMaterialArrowLeft.Visible = true;
                    ibtnMaterialArrowRight.Visible = true;
                    lblRowCount.Text = (((gvMaterial.PageIndex) * gvMaterial.PageSize) + 1) + " - " + (((gvMaterial.PageIndex) * gvMaterial.PageSize) + gvMaterial.Rows.Count) + " of " + Mat.Count;
                }
            }
            catch (Exception e1)
            {
                DisplayErrorMessage(e1);
            }
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
    }
}