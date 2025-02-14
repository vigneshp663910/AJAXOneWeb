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

namespace DealerManagementSystem.ViewAdmin.UserControls
{
    public partial class UserAccessByDealer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » User Access By Sub Module');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    ddlDealerAccess.DataTextField = "CodeWithName";
                    ddlDealerAccess.DataValueField = "DID";
                    ddlDealerAccess.DataSource = PSession.User.Dealer;
                    ddlDealerAccess.DataBind();

                    ddlDealer.DataTextField = "CodeWithName";
                    ddlDealer.DataValueField = "DID";
                    ddlDealer.DataSource = PSession.User.Dealer;
                    ddlDealer.DataBind();
                    ddlDealer.Items.Insert(0, new ListItem("All", "0"));


                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }

        private void FillGrid()
        {
            try
            {
                //if (ddlMainModule.SelectedValue == "0")
                //{
                //    lblMessage.Text = "Please Select Main Module...!";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
                //if (ddlSubModule.SelectedValue == "0")
                //{
                //    lblMessage.Text = "Please Select Sub Module...!";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
                int? DepartmentID = null;
                int? DesignationID = null;
                bool? IsActive = null;
                DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
                DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
                if (ddlIsActive.SelectedValue == "1") { IsActive = true; } else if (ddlIsActive.SelectedValue == "2") { IsActive = false; }

                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                PApiResult Result = new BUser().GetUserAccessByDealer(Convert.ToInt32(ddlDealerAccess.SelectedValue), DealerID, DepartmentID, DesignationID, IsActive);
                gvUsers.DataSource = JsonConvert.DeserializeObject<List<PUserAccessBySubModule>>(JsonConvert.SerializeObject(Result.Data));
                gvUsers.DataBind();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                FillGrid();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
            }
        }
        protected void cbIsActiveH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbIsActiveH = (CheckBox)sender;
            foreach (GridViewRow Row in gvUsers.Rows)
            {
                CheckBox ChkIsActive = (CheckBox)Row.FindControl("ChkIsActive");
                ChkIsActive.Checked = cbIsActiveH.Checked;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<int, Dictionary<int, Boolean>> Access = new Dictionary<int, Dictionary<int, bool>>();
                Dictionary<int, Boolean> Userlist = new Dictionary<int, Boolean>();

                foreach (GridViewRow Row in gvUsers.Rows)
                {
                    Label lblUserID = (Label)Row.FindControl("lblUserID");
                    Label lblDealerID = (Label)Row.FindControl("lblDealerID");
                    CheckBox ChkIsActive = (CheckBox)Row.FindControl("ChkIsActive");
                    CheckBox chIsActiveCurrent = (CheckBox)Row.FindControl("chIsActiveCurrent");
                    if (ChkIsActive.Checked != chIsActiveCurrent.Checked)
                        Userlist.Add(Convert.ToInt32(lblUserID.Text), ChkIsActive.Checked);
                }
                Access.Add(Convert.ToInt32(ddlDealerAccess.SelectedValue), Userlist);
                PApiResult result = new BUser().InsertOrUpdateUserDealerAccess(Access);
                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    return;
                }
                lblMessage.Text = result.Message;
                lblMessage.ForeColor = Color.Green;

                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
    }
}