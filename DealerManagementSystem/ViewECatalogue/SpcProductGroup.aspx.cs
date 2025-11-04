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
    public partial class SpcProductGroup : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcProductGroup; } }

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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Product Group');</script>");
            if (!IsPostBack)
            {
                try
                { 
                    new DDLBind(ddlProductGroup, new BECatalogue().GetSpcProductGroup(null, null,  true), "PGSCodePGDescription", "SpcProductGroupID", true, "All");
                    FillProductGroupl();
                    List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                    if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcProductGroup_CreateAndEditProductGroup).Count() != 0)
                    {
                        btnCreateProductGroup.Visible = true;
                    }
                    else
                    {
                        btnCreateProductGroup.Visible = false;
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
        void FillProductGroupl()
        {
            int? SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            Boolean? Isactive = ddlIsActive.SelectedValue == "-1" ? (Boolean?)null : Convert.ToBoolean(Convert.ToInt32(ddlIsActive.SelectedValue)); 
            gvProductGroup.DataSource = new BECatalogue().GetSpcProductGroup(SpcProductGroupID, txtPGCode.Text.Trim(), Isactive);
            gvProductGroup.DataBind(); 
             
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcProductGroup_CreateAndEditProductGroup).Count() != 0)
            {
                for (int i = 0; i < gvProductGroup.Rows.Count; i++) 
                    ((LinkButton)gvProductGroup.Rows[i].FindControl("lblEditProductGroup")).Visible = true;  
            }
            else
            {
                for (int i = 0; i < gvProductGroup.Rows.Count; i++) 
                    ((LinkButton)gvProductGroup.Rows[i].FindControl("lblEditProductGroup")).Visible = false; 
            } 
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcProductGroup_DeleteProductGroup).Count() != 0)
            {
                for (int i = 0; i < gvProductGroup.Rows.Count; i++)
                    ((LinkButton)gvProductGroup.Rows[i].FindControl("lblDeleteProductGroup")).Visible = true; 
            }
            else
            {
                for (int i = 0; i < gvProductGroup.Rows.Count; i++) 
                    ((LinkButton)gvProductGroup.Rows[i].FindControl("lblDeleteProductGroup")).Visible = false; 
            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillProductGroupl();
        }
 
        protected void btnSpcProductGroupSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_ProductGroupCreate.Show();
                lblModelEditMessage.ForeColor = Color.Red;
                if (ddlDivisionC.SelectedValue == "0")
                {
                    lblModelEditMessage.Text = "Please select Division.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtSpcPGCodeC.Text.Trim()))
                {
                    lblModelEditMessage.Text = "Please enter PG Code.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtSpcPGDescriptionC.Text.Trim()))
                {
                    lblModelEditMessage.Text = "Please enter PG Description.";
                    return;
                }


                int SpcProductGroupID = Convert.ToInt32(lblSpcProductGroupID.Text);
                int DivisionID = Convert.ToInt32(ddlDivisionC.SelectedValue);
                string PGCode = txtSpcPGCodeC.Text.Trim();
                string PGDescription = txtSpcPGDescriptionC.Text.Trim();
                string PGSCode = txtSpcPGSCodeC.Text.Trim();
                string Remarks = txtRemarksC.Text.Trim();
                Boolean IsActive = cbActive.Checked;


                PApiResult Results = new BECatalogue().InsertorUpdateSpcProductGroup(SpcProductGroupID, DivisionID, PGCode, PGDescription, PGSCode, Remarks, IsActive);
                if (Results.Status == PApplication.Failure)
                {
                    lblModelEditMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                MPE_ProductGroupCreate.Hide();

                FillProductGroupl();
                gvProductGroup.DataBind();
                clear();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnCreateProductGroup_Click(object sender, EventArgs e)
        {
            new BECatalogue().FillDivision(ddlDivisionC);
            lblSpcProductGroupID.Text = "0";
            clear();
            MPE_ProductGroupCreate.Show();
        }

        protected void lblEditProductGroup_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                new DDLBind(ddlDivisionC, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
                lblSpcProductGroupID.Text = ((Label)gvRow.FindControl("lblSpcProductGroupID")).Text;
                PSpcProductGroup Model = new BECatalogue().GetSpcProductGroup(Convert.ToInt32(lblSpcProductGroupID.Text), null, null)[0];

                ddlDivisionC.SelectedValue = Convert.ToString(Model.Division.DivisionID);
                txtSpcPGCodeC.Text = Model.PGCode;
                txtSpcPGDescriptionC.Text = Model.PGDescription;
                txtSpcPGSCodeC.Text = Model.PGSCode;

                txtRemarksC.Text = Model.Remarks;
                cbActive.Checked = Model.IsActive;

                MPE_ProductGroupCreate.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void lnkBtnDeleteProductGroup_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent; 
                PApiResult Results = new BECatalogue().UpdateSpcProductGroupDelete(Convert.ToInt32(((Label)gvRow.FindControl("lblSpcProductGroupID")).Text));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                FillProductGroupl(); 
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }


        void clear()
        {
            ddlDivisionC.SelectedValue = Convert.ToString("0");
            txtSpcPGCodeC.Text = "";
            txtSpcPGDescriptionC.Text = "";
            txtSpcPGSCodeC.Text = ""; 
            txtRemarksC.Text = "";
            cbActive.Checked = true;
        }
    }
}