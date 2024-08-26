using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class Material : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_Material; } }
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


        public List<PDMS_Model> MatModel
        {
            get
            {
                if (Session["MaterialM"] == null)
                {
                    Session["MaterialM"] = new List<PDMS_Model>();
                }
                return (List<PDMS_Model>)Session["MaterialM"];
            }
            set
            {
                Session["MaterialM"] = value;
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
        public List<PMaterialVariantType> MaterialVariantsType
        {
            get
            {
                if (ViewState["MaterialVariantsType"] == null)
                {
                    ViewState["MaterialVariantsType"] = new List<PMaterialVariantType>();
                }
                return (List<PMaterialVariantType>)ViewState["MaterialVariantsType"];
            }
            set
            {
                ViewState["MaterialVariantsType"] = value;
            }
        }
        public List<PMaterialVariantTypeMapping> MaterialVariantsTypeMapping
        {
            get
            {
                if (ViewState["MaterialVariantsTypeMapping"] == null)
                {
                    ViewState["MaterialVariantsTypeMapping"] = new List<PMaterialVariantTypeMapping>();
                }
                return (List<PMaterialVariantTypeMapping>)ViewState["MaterialVariantsTypeMapping"];
            }
            set
            {
                ViewState["MaterialVariantsTypeMapping"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Material');</script>");
            lblMessage.Text = string.Empty;
            if (!IsPostBack)
            {
                try
                {
                    new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
                    new DDLBind(ddlDivisionMC, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
                    new DDLBind(ddlMaterialModel, new BDMS_Model().GetModel(null, null, null), "ModelDescription", "ModelID", true, "Select Model");

                    new DDLBind(ddlSProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select ProductType");
                    new DDLBind(ddlSMProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select ProductType");
                    new DDLBind(ddlSMProduct, new BDMS_Master().GetProduct(null, 1, Convert.ToInt32(ddlSMProductType.SelectedValue), null), "Product", "ProductID", true, "Select Product");
                    new DDLBind(ddlSMVariantName, new BDMS_Material().GetMaterialVariantType(null), "VariantName", "VariantTypeID", true, "Select Material Category");

                    GetDivision();
                    GetMaterailModel();
                    //GetMaterial();
                    //GetMaterialPrice();
                    //GetMaterialSupersede();
                    fillMaterialVariantType();
                    fillMaterialVariantTypeMapping();
                    ActionControlMange();
                }
                catch (Exception e1)
                {
                    DisplayErrorMessage(e1);
                }
            }
        }
        private void GetDivision()
        {
            int? DivisionID = (int?)null;
            string Division = (string)null;

            List<PDMS_Division> division = new BDMS_Master().GetDivision(DivisionID, Division);
            gvDivision.DataSource = division;
            gvDivision.DataBind();
            if (division.Count == 0)
            {
                PDMS_Division pDivsison = new PDMS_Division();
                division.Add(pDivsison);
                gvDivision.DataSource = division;
                gvDivision.DataBind();
            }
        }
        protected void gvDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetDivision();
            gvDivision.PageIndex = e.NewPageIndex;
            gvDivision.DataBind();
        }

        protected void ibtnModelArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMaterailModel.PageIndex > 0)
            {
                gvMaterailModel.PageIndex = gvMaterailModel.PageIndex - 1;
                MaterialModelBind(gvMaterailModel, lblRowCountM, MatModel);
            }
        }
        protected void ibtnModelArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMaterailModel.PageCount > gvMaterailModel.PageIndex)
            {
                gvMaterailModel.PageIndex = gvMaterailModel.PageIndex + 1;
                MaterialModelBind(gvMaterailModel, lblRowCountM, MatModel);
            }
        }

        void MaterialModelBind(GridView gv, Label lbl, List<PDMS_Model> MatModel)
        {
            gv.DataSource = MatModel;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + MatModel.Count;
        }


        private void GetMaterailModel()
        {
            int? MaterailID = (int?)null;
            string Materail = (string)null;
            int? DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);

            //List<PDMS_Model> materailModel = new BDMS_Model().GetModel(MaterailID, Materail, DivisionID);

            MatModel = new BDMS_Model().GetModel(MaterailID, Materail, DivisionID);
            if (MatModel.Count == 0)
            {
                PDMS_Model pMaterailModel = new PDMS_Model();
                MatModel.Add(pMaterailModel);
            }
            gvMaterailModel.DataSource = MatModel;
            gvMaterailModel.DataBind();

            if (MatModel.Count == 0)
            {
                lblRowCountM.Visible = false;
                ibtnModelArrowLeft1.Visible = false;
                ibtnModelArrowRight1.Visible = false;
            }
            else
            {
                lblRowCountM.Visible = true;
                ibtnModelArrowLeft1.Visible = true;
                ibtnModelArrowRight1.Visible = true;
                lblRowCountM.Text = (((gvMaterailModel.PageIndex) * gvMaterailModel.PageSize) + 1) + " - " + (((gvMaterailModel.PageIndex) * gvMaterailModel.PageSize) + gvMaterailModel.Rows.Count) + " of " + MatModel.Count;
            }

        }

        protected void gvMaterailModel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvMaterailModel.PageIndex = e.NewPageIndex;
            GetMaterailModel();
            gvMaterailModel.DataBind();
        }

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaterailModel();
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
            FillMaterial();
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
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtMaterialCodePrice.Text.Trim()))
                {
                    lblMessage.Text = "Please Check Material Code";
                    return;
                }
                List<PDMS_Material> Material = new BDMS_Material().GetMaterialListSQL(null, txtMaterialCodePrice.Text.Trim(), null, null, null);
                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer ="9001";
                MaterialPrice.Vendor = "9001";
                MaterialPrice.OrderType = "DEFAULT_SEC_AUART"; 
                MaterialPrice.Division = Material[0].Model.Division.DivisionCode;
                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                {
                    ItemNo = "10",
                    Material = txtMaterialCodePrice.Text.Trim(),
                    Quantity = 1,

                });

                List<PMaterial> Ms = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                if (Ms.Count == 1)
                {
                    if (Ms[0].CurrentPrice < 0)
                    {
                        lblMessage.Text = "Please Check Material Code : " + txtMaterialCodePrice.Text.Trim() + " Price is not valid!";
                        return;
                    } 
                }
               
                Material[0].CurrentPrice = Ms[0].CurrentPrice;
                gvMaterialPrice.DataSource = Material;
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
            try
            {
                new BXcel().ExporttoExcel(dt, Name);
            }
            catch
            {

            }
            finally
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>HideProgress();</script>");
            }
        }
        void DisplayErrorMessage(Exception e1)
        {
            lblMessage.Text = e1.ToString();
            lblMessage.ForeColor = Color.Red; 
        }
        void DisplayErrorMessage(String Message)
        {
            lblMessage.Text = Message;
            lblMessage.ForeColor = Color.Red; 
        }
        void DisplayMessage(String Message)
        {
            lblMessage.Text = Message;
            lblMessage.ForeColor = Color.Green; 
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
        void ActionControlMange()
        {
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.MaterialExcelDownload).Count() == 0)
            {
                btnMaterialExportExcel.Visible = false;
                btnMaterailSupersedeExportExcel.Visible = false;
            }
        }
        protected void btnMatVariantTypeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillMaterialVariantType();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }
        void fillMaterialVariantType()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? ProductTypeID = (ddlSProductType.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSProductType.SelectedValue);
                MaterialVariantsType = new BDMS_Material().GetMaterialVariantType(ProductTypeID);

                GVMatVariantType.PageIndex = 0;
                GVMatVariantType.DataSource = null;
                GVMatVariantType.DataBind();
                if (MaterialVariantsType.Count == 0)
                {
                    PMaterialVariantType MVT = new PMaterialVariantType();
                    MaterialVariantsType.Add(MVT);
                    GVMatVariantType.DataSource = MaterialVariantsType;
                    GVMatVariantType.DataBind();
                    lblMatVariantTypeRowCount.Visible = false;
                    ibtnMatVariantTypeArrowLeft.Visible = false;
                    ibtnMatVariantTypeArrowRight.Visible = false;
                }
                else
                {
                    GVMatVariantType.DataSource = MaterialVariantsType;
                    GVMatVariantType.DataBind();

                    lblMatVariantTypeRowCount.Visible = true;
                    ibtnMatVariantTypeArrowLeft.Visible = true;
                    ibtnMatVariantTypeArrowRight.Visible = true;
                    lblMatVariantTypeRowCount.Text = (((GVMatVariantType.PageIndex) * GVMatVariantType.PageSize) + 1) + " - " + (((GVMatVariantType.PageIndex) * GVMatVariantType.PageSize) + GVMatVariantType.Rows.Count) + " of " + MaterialVariantsType.Count;

                    //List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                    //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EquipmentClientAddEditDelete).Count() == 0)
                    //{
                    //    for (int i = 0; i < GVMatVariantType.Columns.Count; i++)
                    //    {
                    //        if (GVMatVariantType.Columns[i].HeaderText == "Action")
                    //        {
                    //            GVMatVariantType.Columns[i].Visible = false;
                    //            GVMatVariantType.FooterRow.Visible = false;
                    //        }
                    //    }
                    //}
                }
                DropDownList ddlProductType = GVMatVariantType.FooterRow.FindControl("ddlProductType") as DropDownList;
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select");

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("Material", "fillMaterialVariantType", e1);
                throw e1;
            }
        }
        void MatVariantTypeBind(GridView gv, Label lbl, List<PMaterialVariantType> Mat)
        {
            gv.DataSource = Mat;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Mat.Count;
        }
        protected void ibtnMatVariantTypeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (GVMatVariantType.PageIndex > 0)
            {
                GVMatVariantType.PageIndex = GVMatVariantType.PageIndex - 1;
                MatVariantTypeBind(GVMatVariantType, lblMatVariantTypeRowCount, MaterialVariantsType);
            }
        }
        protected void ibtnMatVariantTypeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (GVMatVariantType.PageCount > GVMatVariantType.PageIndex)
            {
                GVMatVariantType.PageIndex = GVMatVariantType.PageIndex + 1;
                MatVariantTypeBind(GVMatVariantType, lblMatVariantTypeRowCount, MaterialVariantsType);
            }
        }
        protected void GVMatVariantType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillMaterialVariantType();
            GVMatVariantType.PageIndex = e.NewPageIndex;
            GVMatVariantType.DataBind();
        }
        protected void lblMatVariantTypeEdit_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 
                LinkButton lblMatVariantTypeEdit = (LinkButton)sender;
                TextBox txtVariantName = (TextBox)GVMatVariantType.FooterRow.FindControl("txtVariantName");
                DropDownList ddlProductType = (DropDownList)GVMatVariantType.FooterRow.FindControl("ddlProductType");
                TextBox txtMaxToSelect = (TextBox)GVMatVariantType.FooterRow.FindControl("txtMaxToSelect");
                Button BtnAddMatVariantType = (Button)GVMatVariantType.FooterRow.FindControl("BtnAddMatVariantType");
                GridViewRow row = (GridViewRow)(lblMatVariantTypeEdit.NamingContainer);
                string lblVariantName = ((Label)row.FindControl("lblVariantName")).Text.Trim();
                string lblProductTypeID = ((Label)row.FindControl("lblProductTypeID")).Text.Trim();
                string lblMaxToSelect = ((Label)row.FindControl("lblMaxToSelect")).Text.Trim();
                txtVariantName.Text = lblVariantName;
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select ProductType");
                ddlProductType.SelectedValue = lblProductTypeID;
                txtMaxToSelect.Text = lblMaxToSelect;
                HidMatVariantType.Value = Convert.ToString(lblMatVariantTypeEdit.CommandArgument);
                BtnAddMatVariantType.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }
        protected void lblMatVariantTypeDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 

                LinkButton lblMatVariantTypeDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lblMatVariantTypeDelete.NamingContainer);
                string lblVariantName = ((Label)row.FindControl("lblVariantName")).Text.Trim();
                string lblProductTypeID = ((Label)row.FindControl("lblProductTypeID")).Text.Trim();
                string lblMaxToSelect = ((Label)row.FindControl("lblMaxToSelect")).Text.Trim();
                HidMatVariantType.Value = Convert.ToString(lblMatVariantTypeDelete.CommandArgument);

                PMaterialVariantType MVT = new PMaterialVariantType();
                MVT.VariantTypeID = Convert.ToInt32(HidMatVariantType.Value);
                MVT.VariantName = lblVariantName.Trim();
                MVT.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(lblProductTypeID) };
                MVT.MaxToSelect = Convert.ToInt32(lblMaxToSelect.Trim());
                MVT.IsActive = false;

                Boolean Result = new BDMS_Material().InsertOrUpdateMaterialVariantType(MVT, PSession.User.UserID);
                if (Result)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Material Category is Deleted Successfully.."; 
                    fillMaterialVariantType();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Material Category is Not Deleted Successfully..!"; 
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString(); 
                return;
            }
        }

        protected void BtnAddMatVariantType_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 
                Button BtnAddMatVariantType = (Button)GVMatVariantType.FooterRow.FindControl("BtnAddMatVariantType");

                TextBox txtVariantName = (TextBox)GVMatVariantType.FooterRow.FindControl("txtVariantName");
                DropDownList ddlProductType = (DropDownList)GVMatVariantType.FooterRow.FindControl("ddlProductType");
                TextBox txtMaxToSelect = (TextBox)GVMatVariantType.FooterRow.FindControl("txtMaxToSelect");

                if (string.IsNullOrEmpty(txtVariantName.Text))
                {
                    lblMessage.Text = "Please Enter Material Category...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (ddlProductType.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Product Type...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (string.IsNullOrEmpty(txtMaxToSelect.Text))
                {
                    lblMessage.Text = "Please Enter MaxToSelect...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PMaterialVariantType MVT = new PMaterialVariantType();
                if (BtnAddMatVariantType.Text == "Update")
                {
                    MVT.VariantTypeID = Convert.ToInt32(HidMatVariantType.Value);
                }
                MVT.VariantName = txtVariantName.Text.Trim();
                MVT.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
                MVT.MaxToSelect = Convert.ToInt32(txtMaxToSelect.Text.Trim());
                MVT.IsActive = true;

                Boolean Result = new BDMS_Material().InsertOrUpdateMaterialVariantType(MVT, PSession.User.UserID);
                if (Result)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Material Category is Created Successfully.."; 
                    fillMaterialVariantType();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Material Category is Not Created Successfully..!"; 
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString(); 
                return;
            }
        }

        protected void btnMatVariantTypeMappingSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillMaterialVariantTypeMapping();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }
        void fillMaterialVariantTypeMapping()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? ProductTypeID = (ddlSMProductType.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSMProductType.SelectedValue);
                int? ProductID = (ddlSMProduct.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSMProduct.SelectedValue);
                int? VariantTypeID = (ddlSMVariantName.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlSMVariantName.SelectedValue);
                string MaterialCode = txtSMMaterial.Text.Trim();
                MaterialVariantsTypeMapping = new BDMS_Material().GetMaterialVariantTypeMapping(ProductTypeID, ProductID, VariantTypeID, MaterialCode);

                GVMatVariantTypeMapping.PageIndex = 0;
                GVMatVariantTypeMapping.DataSource = null;
                GVMatVariantTypeMapping.DataBind();
                if (MaterialVariantsTypeMapping.Count == 0)
                {
                    PMaterialVariantTypeMapping MVTM = new PMaterialVariantTypeMapping();
                    MaterialVariantsTypeMapping.Add(MVTM);
                    GVMatVariantTypeMapping.DataSource = MaterialVariantsTypeMapping;
                    GVMatVariantTypeMapping.DataBind();
                    lblMatVariantTypeMappingRowCount.Visible = false;
                    ibtnMatVariantTypeMappingArrowLeft.Visible = false;
                    ibtnMatVariantTypeMappingArrowRight.Visible = false;
                }
                else
                {
                    GVMatVariantTypeMapping.DataSource = MaterialVariantsTypeMapping;
                    GVMatVariantTypeMapping.DataBind();

                    lblMatVariantTypeMappingRowCount.Visible = true;
                    ibtnMatVariantTypeMappingArrowLeft.Visible = true;
                    ibtnMatVariantTypeMappingArrowRight.Visible = true;
                    lblMatVariantTypeMappingRowCount.Text = (((GVMatVariantTypeMapping.PageIndex) * GVMatVariantTypeMapping.PageSize) + 1) + " - " + (((GVMatVariantTypeMapping.PageIndex) * GVMatVariantTypeMapping.PageSize) + GVMatVariantTypeMapping.Rows.Count) + " of " + MaterialVariantsTypeMapping.Count;

                    //List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                    //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EquipmentClientAddEditDelete).Count() == 0)
                    //{
                    //    for (int i = 0; i < GVMatVariantType.Columns.Count; i++)
                    //    {
                    //        if (GVMatVariantType.Columns[i].HeaderText == "Action")
                    //        {
                    //            GVMatVariantType.Columns[i].Visible = false;
                    //            GVMatVariantType.FooterRow.Visible = false;
                    //        }
                    //    }
                    //}
                }

                DropDownList ddlAddProductType = GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProductType") as DropDownList;
                new DDLBind(ddlAddProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select");

                DropDownList ddlAddProduct = GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProduct") as DropDownList;
                new DDLBind(ddlAddProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "ALL");

                DropDownList ddlAddVariantType = GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddVariantType") as DropDownList;
                new DDLBind(ddlAddVariantType, new BDMS_Material().GetMaterialVariantType(null), "VariantName", "VariantTypeID", true, "Select");

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("Material", "fillMaterialVariantTypeMapping", e1);
                throw e1;
            }
        }
        void MatVariantTypeMappingBind(GridView gv, Label lbl, List<PMaterialVariantTypeMapping> Mat)
        {
            gv.DataSource = Mat;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Mat.Count;
        }
        protected void ibtnMatVariantTypeMappingArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (GVMatVariantTypeMapping.PageIndex > 0)
            {
                GVMatVariantTypeMapping.PageIndex = GVMatVariantTypeMapping.PageIndex - 1;
                MatVariantTypeMappingBind(GVMatVariantTypeMapping, lblMatVariantTypeMappingRowCount, MaterialVariantsTypeMapping);
            }
        }
        protected void ibtnMatVariantTypeMappingArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (GVMatVariantTypeMapping.PageCount > GVMatVariantTypeMapping.PageIndex)
            {
                GVMatVariantTypeMapping.PageIndex = GVMatVariantTypeMapping.PageIndex + 1;
                MatVariantTypeMappingBind(GVMatVariantTypeMapping, lblMatVariantTypeMappingRowCount, MaterialVariantsTypeMapping);
            }
        }
        protected void GVMatVariantTypeMapping_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            fillMaterialVariantTypeMapping();
            GVMatVariantTypeMapping.PageIndex = e.NewPageIndex;
            GVMatVariantTypeMapping.DataBind();
        }
        protected void lblMatVariantTypeMappingEdit_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 
                LinkButton lblMatVariantTypeMappingEdit = (LinkButton)sender;

                DropDownList ddlAddProductType = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProductType");
                DropDownList ddlAddVariantType = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddVariantType");
                DropDownList ddlAddProduct = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProduct");
                TextBox txtAddMaterial = (TextBox)GVMatVariantTypeMapping.FooterRow.FindControl("txtAddMaterial");
                Button BtnAddMatVariantTypeMapping = (Button)GVMatVariantTypeMapping.FooterRow.FindControl("BtnAddMatVariantTypeMapping");

                GridViewRow row = (GridViewRow)(lblMatVariantTypeMappingEdit.NamingContainer);

                string lblMappingProductTypeID = ((Label)row.FindControl("lblMappingProductTypeID")).Text.Trim();
                string lblVariantTypeID = ((Label)row.FindControl("lblVariantTypeID")).Text.Trim();
                string lblProductID = ((Label)row.FindControl("lblProductID")).Text.Trim();
                string lblMaterial = ((Label)row.FindControl("lblMaterial")).Text.Trim();

                //new DDLBind(ddlAddProductType, new BDMS_Material().GetMaterialVariantType(null), "ProductType.ProductType", "ProductType.ProductTypeID", true, "Select ProductType");
                new DDLBind(ddlAddProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select ProductType");
                ddlAddProductType.SelectedValue = lblMappingProductTypeID;
                ddlAddProductType_SelectedIndexChanged(null, null);
                ddlAddVariantType.SelectedValue = lblVariantTypeID;
                ddlAddProduct.SelectedValue = string.IsNullOrEmpty(lblProductID) ? "0" : lblProductID;
                txtAddMaterial.Text = lblMaterial;

                HidMatVariantTypeMapping.Value = Convert.ToString(lblMatVariantTypeMappingEdit.CommandArgument);
                BtnAddMatVariantTypeMapping.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }
        protected void lblMatVariantTypeMappingDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 

                LinkButton lblMatVariantTypeMappingDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lblMatVariantTypeMappingDelete.NamingContainer);
                string lblMappingProductTypeID = ((Label)row.FindControl("lblMappingProductTypeID")).Text.Trim();
                string lblVariantTypeID = ((Label)row.FindControl("lblVariantTypeID")).Text.Trim();
                string lblProductID = ((Label)row.FindControl("lblProductID")).Text.Trim();
                string lblMaterialID = ((Label)row.FindControl("lblMaterialID")).Text.Trim();

                HidMatVariantTypeMapping.Value = Convert.ToString(lblMatVariantTypeMappingDelete.CommandArgument);

                PMaterialVariantTypeMapping MVTM = new PMaterialVariantTypeMapping();
                MVTM.MaterialVariantTypeMappingID = Convert.ToInt32(HidMatVariantTypeMapping.Value);
                MVTM.VariantType = new PMaterialVariantType() { VariantTypeID = Convert.ToInt32(lblVariantTypeID) };
                MVTM.Product = new PProduct() { ProductID = string.IsNullOrEmpty(lblProductID)?0:Convert.ToInt32(lblProductID) };
                MVTM.Material = new PDMS_Material() { MaterialID = Convert.ToInt32(lblMaterialID) };
                MVTM.IsActive = false;

                Boolean Result = new BDMS_Material().InsertOrUpdateMaterialVariantTypeMapping(MVTM, PSession.User.UserID);
                if (Result)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Material Category Mapping is Deleted Successfully.."; 
                    fillMaterialVariantTypeMapping();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Material Category Mapping is Not Deleted Successfully..!"; 
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString(); 
                return;
            }
        }
        protected void BtnAddMatVariantTypeMapping_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red; 
                Button BtnAddMatVariantTypeMapping = (Button)GVMatVariantTypeMapping.FooterRow.FindControl("BtnAddMatVariantTypeMapping");

                DropDownList ddlAddProductType = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProductType");
                DropDownList ddlAddVariantType = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddVariantType");
                DropDownList ddlAddProduct = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProduct");
                TextBox txtAddMaterial = (TextBox)GVMatVariantTypeMapping.FooterRow.FindControl("txtAddMaterial");

                if (ddlAddProductType.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Product Type...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (ddlAddVariantType.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Material Category...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                //if (ddlAddProduct.SelectedValue == "0")
                //{
                //    lblMessage.Text = "Please Select Product...!";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
                string Material = txtAddMaterial.Text.Trim();
                Material = Material.Split(' ')[0];
                PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];

                if (MM == null)
                {
                    lblMessage.Text = "Please Select Valid Material..!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PMaterialVariantTypeMapping MVTM = new PMaterialVariantTypeMapping();
                if (BtnAddMatVariantTypeMapping.Text == "Update")
                {
                    MVTM.MaterialVariantTypeMappingID = Convert.ToInt32(HidMatVariantTypeMapping.Value);
                }
                MVTM.VariantType = new PMaterialVariantType() { VariantTypeID = Convert.ToInt32(ddlAddVariantType.SelectedValue) };
                MVTM.Product = new PProduct() { ProductID = Convert.ToInt32(ddlAddProduct.SelectedValue) };
                MVTM.Material = new PDMS_Material() { MaterialID = MM.MaterialID };
                MVTM.IsActive = true;
                Boolean Result = new BDMS_Material().InsertOrUpdateMaterialVariantTypeMapping(MVTM, PSession.User.UserID);
                if (Result)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Material Category Mapping is Created Successfully.."; 
                    fillMaterialVariantTypeMapping();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Material Category Mapping is Not Created Successfully..!"; 
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message.ToString(); 
                return;
            }
        }
        [WebMethod]
        public static List<string> SearchMaterial(string input)
        {
            List<string> Materials = new BDMS_Material().GetMaterialAutocomplete(input, "", null);
            return Materials.FindAll(item => item.ToLower().Contains(input.ToLower()));
        }
        protected void ddlAddProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlAddProductType = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProductType");
            int? ProductTypeID = null;
            if (ddlAddProductType.SelectedValue != "0")
            {
                ProductTypeID = Convert.ToInt32(ddlAddProductType.SelectedValue);
                DropDownList ddlAddProduct = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddProduct");
                DropDownList ddlAddVariantType = (DropDownList)GVMatVariantTypeMapping.FooterRow.FindControl("ddlAddVariantType");
                new DDLBind(ddlAddVariantType, new BDMS_Material().GetMaterialVariantType(ProductTypeID), "VariantName", "VariantTypeID", true, "Select");
                new DDLBind(ddlAddProduct, new BDMS_Master().GetProduct(null, null, ProductTypeID, null), "Product", "ProductID", true, "ALL");
            }
        }

        protected void btnViewMaterial_Click(object sender, EventArgs e)
        {
            divMaterialView.Visible = true;
            divList.Visible = false;
            Button btnViewMaterial = (Button)(sender as Control);

            UC_MaterialView.fillMaterialByID(Convert.ToInt32(btnViewMaterial.CommandArgument));
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divMaterialView.Visible = false;
            divList.Visible = true;
        }

        protected void ddlSMProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlSMProduct, new BDMS_Master().GetProduct(null, 1, Convert.ToInt32(ddlSMProductType.SelectedValue), null), "Product", "ProductID", true, "Select Product");
        }
    }
}