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
    public partial class PresalesMasters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    List<PLeadSource> Source = new BPresalesMasters().GetLeadSource(null, null);
                    new DDLBind(ddlLeadSource, Source, "Source", "SourceID");

                    List<PActionType> pActionTypes = new BPresalesMasters().GetActionType(null, null);
                    new DDLBind(ddlActionType, pActionTypes, "ActionType", "ActionTypeID");
                    SearchLeadSource();
                    SearchActionType();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }
        void SearchLeadSource()
        {
            int? LeadSourceID = ddlLeadSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlLeadSource.SelectedValue);
            string LeadSource = ddlLeadSource.SelectedValue == "0" ? (string)null : ddlLeadSource.SelectedItem.Text.Trim();

            List<PLeadSource> leadSources = new BPresalesMasters().GetLeadSource(LeadSourceID, LeadSource);

            gvLeadSource.DataSource = leadSources;
            gvLeadSource.DataBind();
        }

        protected void btnSearchLeadSource_Click(object sender, EventArgs e)
        {
            try
            {
                SearchLeadSource();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvLeadSource_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchLeadSource();
            gvLeadSource.PageIndex = e.NewPageIndex;
            gvLeadSource.DataBind();
        }

        protected void BtnAddLeadSource_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddLeadSource = (Button)gvLeadSource.FooterRow.FindControl("BtnAddLeadSource");

                string LeadSource = ((TextBox)gvLeadSource.FooterRow.FindControl("txtLeadSource")).Text.Trim();
                if (string.IsNullOrEmpty(LeadSource))
                {
                    lblMessage.Text = "Please Enter Lead Source";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddLeadSource.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateLeadSource(null, LeadSource, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchLeadSource();
                        lblMessage.Text = "Lead Source Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Lead Source Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Lead Source Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateLeadSource(Convert.ToInt32(HiddenID.Value), LeadSource, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchLeadSource();
                        lblMessage.Text = "Lead Source Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Lead Source Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Lead Source Not Updated Successfully...!";
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

        protected void lblLeadSourceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblLeadSourceEdit = (LinkButton)sender;
                TextBox txtLeadSource = (TextBox)gvLeadSource.FooterRow.FindControl("txtLeadSource");
                Button BtnAddLeadSource = (Button)gvLeadSource.FooterRow.FindControl("BtnAddLeadSource");
                GridViewRow row = (GridViewRow)(lblLeadSourceEdit.NamingContainer);
                string LeadSource = ((Label)row.FindControl("lblLeadSource")).Text.Trim();
                txtLeadSource.Text = LeadSource;
                HiddenID.Value = Convert.ToString(lblLeadSourceEdit.CommandArgument);
                BtnAddLeadSource.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblLeadSourceDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblLeadSourceDelete = (LinkButton)sender;
                long LeadSourceID = Convert.ToInt32(lblLeadSourceDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblLeadSourceDelete.NamingContainer);
                string LeadSource = ((Label)row.FindControl("lblLeadSource")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateLeadSource(LeadSourceID, LeadSource, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchLeadSource();
                    lblMessage.Text = "LeadSource was Deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "LeadSource was not Deleted successfully";
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
        void SearchActionType()
        {
            int? ActionTypeID = ddlActionType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlActionType.SelectedValue);
            string ActionType = ddlActionType.SelectedValue == "0" ? (string)null : ddlActionType.SelectedItem.Text.Trim();

            List<PActionType> ActionTypes = new BPresalesMasters().GetActionType(ActionTypeID, ActionType);

            gvActionType.DataSource = ActionTypes;
            gvActionType.DataBind();
        }

        protected void btnSearchActionType_Click(object sender, EventArgs e)
        {
            try
            {
                SearchActionType();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvActionType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchActionType();
            gvActionType.PageIndex = e.NewPageIndex;
            gvActionType.DataBind();
        }

        protected void BtnAddActionType_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddActionType = (Button)gvActionType.FooterRow.FindControl("BtnAddActionType");

                string ActionType = ((TextBox)gvActionType.FooterRow.FindControl("txtActionType")).Text.Trim();
                if (string.IsNullOrEmpty(ActionType))
                {
                    lblMessage.Text = "Please Enter Action Type";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddActionType.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateActionType(null, ActionType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchActionType();
                        lblMessage.Text = "Action Type Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Action Type Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Action Type Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateActionType(Convert.ToInt32(HiddenID.Value), ActionType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchActionType();
                        lblMessage.Text = "Action Type Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Action Type Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Action Type Not Updated Successfully...!";
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

        protected void lblActionTypeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblActionTypeEdit = (LinkButton)sender;
                TextBox txtActionType = (TextBox)gvActionType.FooterRow.FindControl("txtActionType");
                Button BtnAddActionType = (Button)gvActionType.FooterRow.FindControl("BtnAddActionType");
                GridViewRow row = (GridViewRow)(lblActionTypeEdit.NamingContainer);
                string ActionType = ((Label)row.FindControl("lblActionType")).Text.Trim();
                txtActionType.Text = ActionType;
                HiddenID.Value = Convert.ToString(lblActionTypeEdit.CommandArgument);
                BtnAddActionType.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lblActionTypeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblActionTypeDelete = (LinkButton)sender;
                long ActionTypeID = Convert.ToInt32(lblActionTypeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblActionTypeDelete.NamingContainer);
                string ActionType = ((Label)row.FindControl("lblActionType")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateActionType(ActionTypeID, ActionType, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchActionType();
                    lblMessage.Text = "ActionType was Deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "ActionType was not Deleted successfully";
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