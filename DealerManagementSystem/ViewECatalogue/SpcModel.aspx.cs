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
    public partial class SpcModel : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcModel; } }
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
        //public List<PSpcModel> Model
        //{
        //    get
        //    {
        //        if (Session["SpcModel"] == null)
        //        {
        //            Session["SpcModel"] = new List<PSpcModel>();
        //        }
        //        return (List<PSpcModel>)Session["SpcModel"];
        //    }
        //    set
        //    {
        //        Session["SpcModel"] = value;
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
            lblModelEditMessage.Text = "";
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Model');</script>");
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    new DDLBind(ddlProductGroup, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, "All"); 
                    FillModel();
                    List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                    if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcModel_CreateAndEditModel).Count() != 0)
                    {
                        btnCreateModel.Visible = true;
                    }
                    else
                    {
                        btnCreateModel.Visible = false;
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
        void FillModel()
        {
            PApiResult Result = GetSpcModel(0);  
            gvModel.PageIndex = 0;
            gvModel.DataSource = JsonConvert.DeserializeObject<List<PSpcModel>>(JsonConvert.SerializeObject(Result.Data)); 
            gvModel.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvModel.PageSize - 1) / gvModel.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvModel.PageSize) + 1) + " - " + (((PageIndex - 1) * gvModel.PageSize) + gvModel.Rows.Count) + " of " + Result.RowCount;
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcModel_CreateAndEditModel).Count() != 0)
            {
                for (int i = 0; i < gvModel.Rows.Count; i++)
                    ((LinkButton)gvModel.Rows[i].FindControl("lblEditModel")).Visible = true;
            }
            else
            {
                for (int i = 0; i < gvModel.Rows.Count; i++)
                    ((LinkButton)gvModel.Rows[i].FindControl("lblEditModel")).Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcModel_DeleteModel).Count() != 0)
            {
                for (int i = 0; i < gvModel.Rows.Count; i++)
                    ((LinkButton)gvModel.Rows[i].FindControl("lblDeleteModel")).Visible = true;
            }
            else
            {
                for (int i = 0; i < gvModel.Rows.Count; i++)
                    ((LinkButton)gvModel.Rows[i].FindControl("lblDeleteModel")).Visible = false;
            }
             
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {

            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillModel();
            }
        } 
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillModel();
            } 
        } 

       
        protected void btnSpcModelSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_ModelCreate.Show();
                lblModelEditMessage.ForeColor = Color.Red;
                int number;
                if (ddlSpcProductGroupC.SelectedValue == "0")
                {
                    lblModelEditMessage.Text = "Please select  Product Group.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtSlNoC.Text.Trim()))
                {
                    lblModelEditMessage.Text = "Please enter SlNo.";
                    return;
                }
                else if (!int.TryParse(txtSlNoC.Text.Trim(), out number))
                {
                    lblModelEditMessage.Text = "Please enter valid SlNo." ;
                    return;
                }
                else if (string.IsNullOrEmpty(txtSpcModelCodeC.Text.Trim()))
                {
                    lblModelEditMessage.Text = "Please enter Model / PM Code.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtSpcModelC.Text.Trim()))
                {
                    lblModelEditMessage.Text = "Please enter Product Model Description.";
                    return;
                }
                else if   (ddlSpcMaterialGroupC.SelectedValue == "0")
                    {
                        lblModelEditMessage.Text = "Please select  Material Group.";
                        return;
                    }

                PSpcModel_Insert Model_Insert = new PSpcModel_Insert()
                {
                    SpcModelID = Convert.ToInt32(lblSpcModelID.Text),
                    SlNo = Convert.ToInt32(txtSlNoC.Text),
                    SpcProductGroupID = Convert.ToInt32(ddlSpcProductGroupC.SelectedValue),
                    SpcMaterialGroupID = Convert.ToInt32(ddlSpcMaterialGroupC.SelectedValue),
                    SpcModelCode = txtSpcModelCodeC.Text.Trim(),
                    SpcModel = txtSpcModelC.Text.Trim(), 
                    Purpose = ddlPurposeC.SelectedValue,
                    Remarks = txtRemarksC.Text.Trim(),
                    IsActive = cbActive.Checked,
                    IsPublish = cbPublish.Checked 
                };
                PApiResult Results = new BECatalogue().InsertorUpdateSpcModel(Model_Insert);
                if (Results.Status == PApplication.Failure)
                {
                    lblModelEditMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                MPE_ModelCreate.Hide();

                int PageIndex = gvModel.PageIndex;
                FillModel();
                gvModel.PageIndex = PageIndex;
                gvModel.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red; 
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            FillModel();
        }

        protected void btnCreateModel_Click(object sender, EventArgs e)
        {
            new DDLBind(ddlSpcProductGroupC, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, "Select");
            PApiResult Result = new BECatalogue().GetSpcMaterialGroup(null, null, null, 0, null, null); 
            new DDLBind(ddlSpcMaterialGroupC, JsonConvert.DeserializeObject<List<PSpcMaterialGroup>>(JsonConvert.SerializeObject(Result.Data)), "MaterialGroupDescription1", "SpcMaterialGroupID", true, "Select");
            lblSpcModelID.Text = "0";
            txtSlNoC.Text = "";
            lblSpcModelCodeC.Text = "";
            txtSpcModelCodeC.Text = "";
            lblSpcModelC.Text = "";
            txtSpcModelC.Text = "";
            txtRemarksC.Text = "";
            cbActive.Checked = true;
            cbPublish.Checked = true;
            ddlSpcProductGroupC.Enabled = true;
            ddlSpcMaterialGroupC.Enabled = true;
            MPE_ModelCreate.Show();
        } 
        PApiResult GetSpcModel(int Excel)
        {
            try
            {
                int? ProductGroupnID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
                Boolean? Isactive = ddlIsActive.SelectedValue == "-1" ? (Boolean?)null : Convert.ToBoolean(Convert.ToInt32(ddlIsActive.SelectedValue));
                Boolean? Publish = ddlPublish.SelectedValue == "-1" ? (Boolean?)null : Convert.ToBoolean(Convert.ToInt32(ddlPublish.SelectedValue));
                return new BECatalogue().GetSpcModelWithResult(null, ProductGroupnID, txtModelCode.Text.Trim(), Isactive, Publish, Excel, PageIndex, gvModel.PageSize);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SpcModel", "GetSpcModel", e1);
                throw e1;
            }
        }
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            PApiResult Result = GetSpcModel(1);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(dt, "Model List");
        }
        //protected void txtMaterialGroupC_TextChanged(object sender, EventArgs e)
        //{
        //    MPE_ModelCreate.Show();

        //    try
        //    {
        //        txtSpcModelCodeC.Text ="";
        //        txtSpcModelC.Text = "";

        //        lblSpcModelCodeC.Text = "";
        //        lblSpcModelC.Text = "";


        //        PSpcModel Model = new BECatalogue().GetSpcModelFromSap(txtMaterialGroupC.Text.Trim());
        //        txtSpcModelCodeC.Text = Model.MaterialGroup;
        //        txtSpcModelC.Text = Model.MaterialGroupDescription1;
                 
        //        lblSpcModelCodeC.Text = Model.MaterialGroup;
        //        lblSpcModelC.Text = Model.MaterialGroupDescription1;

        //    }
        //    catch(Exception ex)
        //    {
        //        lblModelEditMessage.Text = ex.Message;
        //        lblModelEditMessage.ForeColor = Color.Red;
        //    }
        //}         
        protected void ddlSpcProductGroupC_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_ModelCreate.Show();

            try
            {
                ddlSpcMaterialGroupC.SelectedValue = "0"; 

                txtSpcModelCodeC.Text = "";
                lblSpcModelCodeC.Text = "";
                txtSpcModelC.Text = "";
                lblSpcModelC.Text = "";

                if (ddlSpcProductGroupC.SelectedValue == "3")
                {
                    txtSpcModelCodeC.Visible = true;
                    txtSpcModelC.Visible = true;

                    lblSpcModelCodeC.Visible = false;
                    lblSpcModelC.Visible = false;
                }
                else
                {
                    txtSpcModelCodeC.Visible = false;
                    txtSpcModelC.Visible = false;

                    lblSpcModelCodeC.Visible = true;
                    lblSpcModelC.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblModelEditMessage.Text = ex.Message;
                lblModelEditMessage.ForeColor = Color.Red;
            }
        }
        protected void lblEditModel_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                new DDLBind(ddlSpcProductGroupC, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, "Select");
                PApiResult Result = new BECatalogue().GetSpcMaterialGroup(null, null, null, 0, null, null);
                new DDLBind(ddlSpcMaterialGroupC, JsonConvert.DeserializeObject<List<PSpcMaterialGroup>>(JsonConvert.SerializeObject(Result.Data)), "MaterialGroupDescription1", "SpcMaterialGroupID", true, "Select");
                
                lblSpcModelID.Text = ((Label)gvRow.FindControl("lblSpcModelID")).Text;
                PSpcModel Model = new BECatalogue().GetSpcModel(Convert.ToInt32(lblSpcModelID.Text), null, null, null, null, null, null)[0];

                txtSlNoC.Text = Convert.ToString(Model.SlNo);
                ddlSpcProductGroupC.SelectedValue = Convert.ToString(Model.ProductGroup.SpcProductGroupID);
                ddlSpcMaterialGroupC.SelectedValue = Convert.ToString(Model.MaterialGroup.SpcMaterialGroupID);
                ddlSpcProductGroupC.Enabled = false;
                txtSpcModelCodeC.Text = Model.SpcModelCode;
                txtSpcModelC.Text = Model.SpcModel;
                lblSpcModelCodeC.Text = Model.SpcModelCode;
                lblSpcModelC.Text = Model.SpcModel;                 
                ddlSpcMaterialGroupC.Enabled = false;
                ddlPurposeC.SelectedValue = Convert.ToString(Model.Purpose);
                txtRemarksC.Text = Model.Remarks;
                cbActive.Checked = Model.IsActive;
                cbPublish.Checked = Model.IsPublish;                 

                if (ddlSpcProductGroupC.SelectedValue == "1")
                {
                    txtSpcModelCodeC.Visible = true;
                    txtSpcModelC.Visible = true;

                    lblSpcModelCodeC.Visible = false;
                    lblSpcModelC.Visible = false;
                }
                else
                {
                    txtSpcModelCodeC.Visible = false;
                    txtSpcModelC.Visible = false;

                    lblSpcModelCodeC.Visible = true;
                    lblSpcModelC.Visible = true;
                }
                MPE_ModelCreate.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lnkBtnDeleteModelp_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                PApiResult Results = new BECatalogue().UpdateSpcModelDelete(Convert.ToInt32(((Label)gvRow.FindControl("lblSpcModelID")).Text));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                FillModel();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void ddlSpcMaterialGroupC_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_ModelCreate.Show();
            try
            {
                txtSpcModelCodeC.Text = "";
                txtSpcModelC.Text = ""; 
                lblSpcModelCodeC.Text = "";
                lblSpcModelC.Text = "";
                if (ddlSpcMaterialGroupC.SelectedValue != "0")
                {
                    PApiResult Result = new BECatalogue().GetSpcMaterialGroup(Convert.ToInt32(ddlSpcMaterialGroupC.SelectedValue), null, null, 0, null, null);
                    PSpcMaterialGroup MGroup = JsonConvert.DeserializeObject<List<PSpcMaterialGroup>>(JsonConvert.SerializeObject(Result.Data))[0];
                    txtSpcModelCodeC.Text = MGroup.MaterialGroup;
                    txtSpcModelC.Text = MGroup.Description1;
                    lblSpcModelCodeC.Text = MGroup.MaterialGroup;
                    lblSpcModelC.Text = MGroup.Description1;
                }
            }
            catch (Exception ex)
            {
                lblModelEditMessage.Text = ex.Message;
                lblModelEditMessage.ForeColor = Color.Red;
            }
        }
    }
}