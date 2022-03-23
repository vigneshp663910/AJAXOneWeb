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
    public partial class Material : System.Web.UI.Page
    {
        public List<PDMS_Material> Mat
        {
            get
            {
                if (Session["Material"] == null)
                {
                    Session["Material"] = new List<PDMS_Material>();
                }
                return (List<PDMS_Material>)Session["Material"];
            }
            set
            {
                Session["Material"] = value;
            }
        }
        public List<PDMS_Material> MaterialSupersede
        {
            get
            {
                if (Session["MaterialSupersede"] == null)
                {
                    Session["MaterialSupersede"] = new List<PDMS_Material>();
                }
                return (List<PDMS_Material>)Session["MaterialSupersede"];
            }
            set
            {
                Session["MaterialSupersede"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Material');</script>");
            if (!IsPostBack)
            {
                try
                {
                    //GetMaterial();
                    //GetMaterialPrice();
                    //GetMaterialSupersede();
                }
                catch (Exception e1)
                {
                    DisplayErrorMessage(e1);
                }
            }
        }

        protected void ibtnMaterialArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMaterial.PageIndex > 0)
            {
                gvMaterial.PageIndex = gvMaterial.PageIndex - 1;
                MaterialBind(gvMaterial, lblRowCount, Mat);
            }
        }
        protected void ibtnMaterialArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMaterial.PageCount > gvMaterial.PageIndex)
            {
                gvMaterial.PageIndex = gvMaterial.PageIndex + 1;
                MaterialBind(gvMaterial, lblRowCount, Mat);
            }
        }
        protected void btnMaterialSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Mat = new BDMS_Material().GetMaterialListSQL(null, txtMaterialCode.Text.Trim());
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
        protected void gvMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMaterial.PageIndex = e.NewPageIndex;
            MaterialBind(gvMaterial, lblRowCount, Mat);
        }
        protected void btnMaterialExportExcel_Click(object sender, EventArgs e)
        {
            MaterialExportExcel(Mat, "Material Report");
        }
        protected void btnMaterialPriceSerach_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaterialCodePrice.Text.Trim()))
                {
                    return;
                }
                gvMaterialPrice.DataSource = new BDMS_Material().GetMaterialListSQL(null, txtMaterialCodePrice.Text.Trim());
                gvMaterialPrice.DataBind();
            }
            catch (Exception e1)
            {
                DisplayErrorMessage(e1);
            }
        }
        protected void btnMaterailSupersedeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                MaterialSupersede = new BDMS_Material().GetMaterialSupersede(null, txtMaterialCodeSupersede.Text.Trim());
                gvMaterialSupersede.PageIndex = 0;
                gvMaterialSupersede.DataSource = MaterialSupersede;
                gvMaterialSupersede.DataBind();
                if (MaterialSupersede.Count == 0)
                {
                    lblMaterialSupersedeCount.Visible = false;
                    ibtnMaterialSupersedeArrowLeft.Visible = false;
                    ibtnMaterialSupersedeArrowRight.Visible = false;
                }
                else
                {
                    lblMaterialSupersedeCount.Visible = true;
                    ibtnMaterialSupersedeArrowLeft.Visible = true;
                    ibtnMaterialSupersedeArrowRight.Visible = true;
                    lblMaterialSupersedeCount.Text = (((gvMaterialSupersede.PageIndex) * gvMaterialSupersede.PageSize) + 1) + " - " + (((gvMaterialSupersede.PageIndex) * gvMaterialSupersede.PageSize) + gvMaterialSupersede.Rows.Count) + " of " + MaterialSupersede.Count;
                }
            }
            catch (Exception e1)
            {
                DisplayErrorMessage(e1);
            }
        }
        protected void ibtnMaterialSupersedeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMaterialSupersede.PageIndex > 0)
            {
                gvMaterialSupersede.PageIndex = gvMaterialSupersede.PageIndex - 1;
                MaterialBind(gvMaterialSupersede, lblRowCount, MaterialSupersede);
            }
        }
        protected void ibtnMaterialSupersedeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMaterialSupersede.PageCount > gvMaterialSupersede.PageIndex)
            {
                gvMaterialSupersede.PageIndex = gvMaterialSupersede.PageIndex + 1;
                MaterialBind(gvMaterialSupersede, lblRowCount, MaterialSupersede);
            }
        }
        protected void gvMaterialSupersede_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMaterialSupersede.PageIndex = e.NewPageIndex;
            MaterialBind(gvMaterialSupersede, lblRowCount, MaterialSupersede);
        }
        protected void btnMaterialSupersedeExportExcel_Click(object sender, EventArgs e)
        {
            MaterialExportExcel(MaterialSupersede, "Material Superede");
        }
        void MaterialBind(GridView gv, Label lbl, List<PDMS_Material> Mat)
        {
            gv.DataSource = Mat;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Mat.Count;
        }
        void MaterialExportExcel(List<PDMS_Material> Mat, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Material Code");
            dt.Columns.Add("Material Description");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Material Type");
            dt.Columns.Add("Material Group");
            dt.Columns.Add("Gross Weight");
            dt.Columns.Add("Net Weight");
            dt.Columns.Add("Weight Unit");
            dt.Columns.Add("Division");
            dt.Columns.Add("HSN");
            dt.Columns.Add("CST");
            dt.Columns.Add("SST");
            dt.Columns.Add("GST");
            foreach (PDMS_Material M in Mat)
            {
                dt.Rows.Add(
                    "'" + M.MaterialCode
                    , M.MaterialDescription
                    , M.BaseUnit
                   , M.MaterialType
                    , M.MaterialGroup
                    , M.GrossWeight
                    , M.NetWeight
                    , M.WeightUnit
                    , M.MaterialDivision
                    , M.HSN
                    , M.CST
                    , M.SST
                    , M.GST
                    );
            }
            new BXcel().ExporttoExcel(dt, Name);
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