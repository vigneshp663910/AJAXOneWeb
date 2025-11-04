using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcMaterialGroup : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcMaterialGroup; } }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
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
            lblMaterialGroupEditMessage.Text = "";
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Material Group');</script>");
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    FillMaterialGroup();

                    List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                    if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcMaterialGroup_CreateAndEditMaterialGroup).Count() != 0)
                    {
                        btnCreateMaterialGroup.Visible = true;
                    }
                    else
                    {
                        btnCreateMaterialGroup.Visible = false;
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
        void FillMaterialGroup()
        {
            PApiResult Result = GetSpcMaterialGroup(0);
            gvMaterialGroup.PageIndex = 0;
            gvMaterialGroup.DataSource = JsonConvert.DeserializeObject<List<PSpcMaterialGroup>>(JsonConvert.SerializeObject(Result.Data));
            gvMaterialGroup.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvMaterialGroup.PageSize - 1) / gvMaterialGroup.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvMaterialGroup.PageSize) + 1) + " - " + (((PageIndex - 1) * gvMaterialGroup.PageSize) + gvMaterialGroup.Rows.Count) + " of " + Result.RowCount;
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcMaterialGroup_CreateAndEditMaterialGroup).Count() != 0)
            {
                for (int i = 0; i < gvMaterialGroup.Rows.Count; i++) 
                    ((LinkButton)gvMaterialGroup.Rows[i].FindControl("lblEditMaterialGroup")).Visible = true; 
            }
            else
            {
                for (int i = 0; i < gvMaterialGroup.Rows.Count; i++) 
                    ((LinkButton)gvMaterialGroup.Rows[i].FindControl("lblEditMaterialGroup")).Visible = false; 
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcMaterialGroup_DeleteMaterialGroup).Count() != 0)
            {
                for (int i = 0; i < gvMaterialGroup.Rows.Count; i++) 
                    ((LinkButton)gvMaterialGroup.Rows[i].FindControl("lblDeleteMaterialGroup")).Visible = true;  
            }
            else
            {
                for (int i = 0; i < gvMaterialGroup.Rows.Count; i++) 
                    ((LinkButton)gvMaterialGroup.Rows[i].FindControl("lblDeleteMaterialGroup")).Visible = false;  
            } 
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillMaterialGroup();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillMaterialGroup();
            }
        }
         
        protected void btnSpcModelSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_MaterialGroupCreate.Show();
                lblMaterialGroupEditMessage.ForeColor = Color.Red; 
                if (string.IsNullOrEmpty(txtMaterialGroupC.Text.Trim()))
                {
                    lblMaterialGroupEditMessage.Text = "Please enter SAP Material Group.";
                    return;
                }

                int? SpcMaterialGroupID = Convert.ToInt32(lblSpcMaterialGroupID.Text);

                //PApiResult Result = new BECatalogue().GetSpcMaterialGroup(null, txtMaterialGroupC.Text.Trim(), null, 0, null, null);

                //List<PSpcMaterialGroup> G = JsonConvert.DeserializeObject<List<PSpcMaterialGroup>>(JsonConvert.SerializeObject(Result.Data));
                //if (G.Count != 0)
                //{
                //    if (SpcMaterialGroupID != G[0].SpcMaterialGroupID)
                //    {
                //        lblMaterialGroupEditMessage.Text = "Material Group already exist in system.";
                //        return;
                //    }
                //}

                
                string MaterialGroup = txtMaterialGroupC.Text.Trim();
                string Description1 = lblMaterialGroupDescription1C.Text;
                string Description2 = lblMaterialGroupDescription2C.Text;
                string MType = ddlMaterialTypeC.SelectedValue;
                PApiResult Results = new BECatalogue().InsertorUpdateSpcMaterialGroup(SpcMaterialGroupID, MaterialGroup, Description1, Description2, MType, cbActive.Checked);
                if (Results.Status == PApplication.Failure)
                {
                    lblMaterialGroupEditMessage.Text = Results.Message;
                    return;
                } 
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                MPE_MaterialGroupCreate.Hide();

                int PageIndex = gvMaterialGroup.PageIndex;
                FillMaterialGroup();
                gvMaterialGroup.PageIndex = PageIndex;
                gvMaterialGroup.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillMaterialGroup();
        }

        
        PApiResult GetSpcMaterialGroup(int Excel)
        {
            try
            { 
                Boolean? Isactive = ddlIsActive.SelectedValue == "-1" ? (Boolean?)null : Convert.ToBoolean(Convert.ToInt32(ddlIsActive.SelectedValue)); 
                return new BECatalogue().GetSpcMaterialGroup(null,  txtMaterialGroupCode.Text.Trim(), Isactive, Excel, PageIndex, gvMaterialGroup.PageSize);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SpcMaterialGroup", "GetSpcMaterialGroup", e1);
                throw e1;
            }
        }

        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            PApiResult Result = GetSpcMaterialGroup(1);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(dt, "Model List");
        }

        protected void txtMaterialGroupC_TextChanged(object sender, EventArgs e)
        {
            MPE_MaterialGroupCreate.Show();
            try
            { 
                lblMaterialGroupDescription1C.Text = "";
                lblMaterialGroupDescription2C.Text = "";
                PSpcMaterialGroup MaterialGroup = new BECatalogue().GetSpcModelFromSap(txtMaterialGroupC.Text.Trim());  
                lblMaterialGroupDescription1C.Text = MaterialGroup.Description1;
                lblMaterialGroupDescription2C.Text = MaterialGroup.Description2;
            }
            catch (Exception ex)
            {
                lblMaterialGroupEditMessage.Text = ex.Message;
                lblMaterialGroupEditMessage.ForeColor = Color.Red;
            }
        }
         

        protected void btnCreateMaterialGroup_Click(object sender, EventArgs e)
        {
            lblSpcMaterialGroupID.Text = "0";
            lblMaterialGroupDescription1C.Text = "";
            lblMaterialGroupDescription2C.Text = "";
            txtMaterialGroupC.Text = "";
            txtMaterialGroupC.Enabled = true;
            MPE_MaterialGroupCreate.Show();
        }

        protected void lblEditMaterialGroup_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                lblSpcMaterialGroupID.Text = ((Label)gvRow.FindControl("lblSpcMaterialGroupID")).Text;
                PApiResult Result = new BECatalogue().GetSpcMaterialGroup(Convert.ToInt32(lblSpcMaterialGroupID.Text), null, null, 0);
                PSpcMaterialGroup MaterialGroup = JsonConvert.DeserializeObject<List<PSpcMaterialGroup>>(JsonConvert.SerializeObject(Result.Data))[0];
                txtMaterialGroupC.Text = MaterialGroup.MaterialGroup;
                txtMaterialGroupC.Enabled = false;
                cbActive.Checked = MaterialGroup.IsActive;
                lblMaterialGroupDescription1C.Text = MaterialGroup.Description1;
                lblMaterialGroupDescription2C.Text = MaterialGroup.Description2;
                MPE_MaterialGroupCreate.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void lnkBtnDeleteMaterialGroup_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                PApiResult Results = new BECatalogue().UpdateSpcMaterialGroupDelete(Convert.ToInt32(((Label)gvRow.FindControl("lblSpcMaterialGroupID")).Text));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message; 
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                FillMaterialGroup();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
    }
}