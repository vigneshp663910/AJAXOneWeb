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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Pre-Sales');</script>");

            if (!IsPostBack)
            {
                try
                {
                    List<PLeadSource> Source = new BPresalesMasters().GetLeadSource(null, null);
                    new DDLBind(ddlLeadSource, Source, "Source", "SourceID");

                    List<PActionType> pActionTypes = new BPresalesMasters().GetActionType(null, null);
                    new DDLBind(ddlActionType, pActionTypes, "ActionType", "ActionTypeID");

                    List<PCustomerAttributeMain> pCustomerAttributeMains = new BPresalesMasters().GetCustomerAttributeMain(null, null);
                    new DDLBind(ddlCustomerAttributeMain, pCustomerAttributeMains, "AttributeMain", "AttributeMainID");
                    new DDLBind(ddlSCustomerAttributeMain, pCustomerAttributeMains, "AttributeMain", "AttributeMainID");

                    List<PCustomerAttributeSub> pCustomerAttributeSub = new BPresalesMasters().GetCustomerAttributeSub(null, null, null);
                    new DDLBind(ddlCustomerAttributeSub, pCustomerAttributeSub, "AttributeSub", "AttributeSubID");

                    List<PEffortType> EffortTypes = new BPresalesMasters().GetEffortType(null, null);
                    new DDLBind(ddlEffortType, EffortTypes, "EffortType", "EffortTypeID");

                    List<PExpenseType> pExpenseTypes = new BPresalesMasters().GetExpenseType(null, null);
                    new DDLBind(ddlExpenseType, pExpenseTypes, "ExpenseType", "ExpenseTypeID");

                    SearchLeadSource();
                    SearchActionType();
                    SearchCustomerAttributeMain();
                    SearchCustomerAttributeSub();
                    SearchEffortType();
                    SearchExpenseType();
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
            if (leadSources.Count == 0)
            {
                PLeadSource pLeadSource = new PLeadSource();
                leadSources.Add(pLeadSource);
                gvLeadSource.DataSource = leadSources;
                gvLeadSource.DataBind();
            }
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
            if (ActionTypes.Count == 0)
            {
                PActionType pActionType = new PActionType();
                ActionTypes.Add(pActionType);
                gvActionType.DataSource = ActionTypes;
                gvActionType.DataBind();
            }
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
        void SearchCustomerAttributeMain()
        {
            int? CustomerAttributeMainID = ddlCustomerAttributeMain.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCustomerAttributeMain.SelectedValue);
            string CustomerAttributeMain = ddlCustomerAttributeMain.SelectedValue == "0" ? (string)null : ddlCustomerAttributeMain.SelectedItem.Text.Trim();

            List<PCustomerAttributeMain> CustomerAttributeMains = new BPresalesMasters().GetCustomerAttributeMain(CustomerAttributeMainID, CustomerAttributeMain);

            gvCustomerAttributeMain.DataSource = CustomerAttributeMains;
            gvCustomerAttributeMain.DataBind();
            if (CustomerAttributeMains.Count == 0)
            {
                PCustomerAttributeMain pCustomerAttributeMain = new PCustomerAttributeMain();
                CustomerAttributeMains.Add(pCustomerAttributeMain);
                gvCustomerAttributeMain.DataSource = CustomerAttributeMains;
                gvCustomerAttributeMain.DataBind();
            }
        }
        protected void btnSearchCustomerAttributeMain_Click(object sender, EventArgs e)
        {
            try
            {
                SearchCustomerAttributeMain();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void gvCustomerAttributeMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchCustomerAttributeMain();
            gvCustomerAttributeMain.PageIndex = e.NewPageIndex;
            gvCustomerAttributeMain.DataBind();
        }
        protected void BtnAddCustomerAttributeMain_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddCustomerAttributeMain = (Button)gvCustomerAttributeMain.FooterRow.FindControl("BtnAddCustomerAttributeMain");

                string CustomerAttributeMain = ((TextBox)gvCustomerAttributeMain.FooterRow.FindControl("txtCustomerAttributeMain")).Text.Trim();
                if (string.IsNullOrEmpty(CustomerAttributeMain))
                {
                    lblMessage.Text = "Please Enter Customer Attribute Main";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddCustomerAttributeMain.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateCustomerAttributeMain(null, CustomerAttributeMain, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchCustomerAttributeMain();
                        lblMessage.Text = "Customer Attribute Main Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Customer Attribute Main Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Customer Attribute Main Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateCustomerAttributeMain(Convert.ToInt32(HiddenID.Value), CustomerAttributeMain, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchCustomerAttributeMain();
                        lblMessage.Text = "Customer Attribute Main Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Customer Attribute Main Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Customer Attribute Main Not Updated Successfully...!";
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
        protected void lblCustomerAttributeMainEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblCustomerAttributeMainEdit = (LinkButton)sender;
                TextBox txtCustomerAttributeMain = (TextBox)gvCustomerAttributeMain.FooterRow.FindControl("txtCustomerAttributeMain");
                Button BtnAddCustomerAttributeMain = (Button)gvCustomerAttributeMain.FooterRow.FindControl("BtnAddCustomerAttributeMain");
                GridViewRow row = (GridViewRow)(lblCustomerAttributeMainEdit.NamingContainer);
                string CustomerAttributeMain = ((Label)row.FindControl("lblCustomerAttributeMain")).Text.Trim();
                txtCustomerAttributeMain.Text = CustomerAttributeMain;
                HiddenID.Value = Convert.ToString(lblCustomerAttributeMainEdit.CommandArgument);
                BtnAddCustomerAttributeMain.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void lblCustomerAttributeMainDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblCustomerAttributeMainDelete = (LinkButton)sender;
                long CustomerAttributeMainID = Convert.ToInt32(lblCustomerAttributeMainDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblCustomerAttributeMainDelete.NamingContainer);
                string CustomerAttributeMain = ((Label)row.FindControl("lblCustomerAttributeMain")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateCustomerAttributeMain(CustomerAttributeMainID, CustomerAttributeMain, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchCustomerAttributeMain();
                    lblMessage.Text = "Customer Attribute Main was Deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Customer Attribute Main was not Deleted successfully";
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
        void SearchCustomerAttributeSub()
        {
            int? CustomerAttributeMainID = ddlCustomerAttributeMain.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCustomerAttributeMain.SelectedValue);
            int? CustomerAttributeSubID = ddlCustomerAttributeSub.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCustomerAttributeSub.SelectedValue);
            string CustomerAttributeSub = ddlCustomerAttributeSub.SelectedValue == "0" ? (string)null : ddlCustomerAttributeSub.SelectedItem.Text.Trim();

            List<PCustomerAttributeSub> pCustomerAttributeSubs = new BPresalesMasters().GetCustomerAttributeSub(CustomerAttributeMainID, CustomerAttributeSubID, CustomerAttributeSub);

            gvCustomerAttributeSub.DataSource = pCustomerAttributeSubs;
            gvCustomerAttributeSub.DataBind();
            if (pCustomerAttributeSubs.Count == 0)
            {
                PCustomerAttributeSub pCustomerAttributeSub = new PCustomerAttributeSub();
                pCustomerAttributeSubs.Add(pCustomerAttributeSub);
                gvCustomerAttributeSub.DataSource = pCustomerAttributeSubs;
                gvCustomerAttributeSub.DataBind();
            }
        }
        protected void btnSearchCustomerAttributeSub_Click(object sender, EventArgs e)
        {
            try
            {
                SearchCustomerAttributeSub();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void gvCustomerAttributeSub_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchCustomerAttributeSub();
            gvCustomerAttributeSub.PageIndex = e.NewPageIndex;
            gvCustomerAttributeSub.DataBind();
        }
        protected void gvCustomerAttributeSub_DataBound(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddlGAttributeMain = gvCustomerAttributeSub.FooterRow.FindControl("ddlGAttributeMain") as DropDownList;
                new DDLBind(ddlGAttributeMain, new BPresalesMasters().GetCustomerAttributeMain(null, null), "AttributeMain", "AttributeMainID", true, "Select");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void BtnAddCustomerAttributeSub_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddCustomerAttributeSub = (Button)gvCustomerAttributeSub.FooterRow.FindControl("BtnAddCustomerAttributeSub");

                DropDownList ddlGAttributeMain = (DropDownList)gvCustomerAttributeSub.FooterRow.FindControl("ddlGAttributeMain");
                if (ddlGAttributeMain.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Customer Attribute Main";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                int AttributeMainID = Convert.ToInt32(ddlGAttributeMain.SelectedValue);
                string AttributeSub = ((TextBox)gvCustomerAttributeSub.FooterRow.FindControl("txtAttributeSub")).Text.Trim();
                if (string.IsNullOrEmpty(AttributeSub))
                {
                    lblMessage.Text = "Please Enter Customer Attribute Sub";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddCustomerAttributeSub.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateCustomerAttributeSub(null, AttributeMainID, AttributeSub, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchCustomerAttributeSub();
                        lblMessage.Text = "Customer Attribute Sub Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Customer Attribute Sub Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Customer Attribute Sub Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateCustomerAttributeSub(Convert.ToInt32(HiddenID.Value), AttributeMainID, AttributeSub, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchCustomerAttributeSub();
                        lblMessage.Text = "Customer Attribute Sub Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Customer Attribute Sub Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Customer Attribute Sub Not Updated Successfully...!";
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
        protected void lblCustomerAttributeSubEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblCustomerAttributeSubEdit = (LinkButton)sender;
                DropDownList ddlGAttributeMain = (DropDownList)gvCustomerAttributeSub.FooterRow.FindControl("ddlGAttributeMain");
                TextBox txtAttributeSub = (TextBox)gvCustomerAttributeSub.FooterRow.FindControl("txtAttributeSub");
                Button BtnAddCustomerAttributeSub = (Button)gvCustomerAttributeSub.FooterRow.FindControl("BtnAddCustomerAttributeSub");
                GridViewRow row = (GridViewRow)(lblCustomerAttributeSubEdit.NamingContainer);
                Label lblAttributeMainID = (Label)row.FindControl("lblAttributeMainID");
                ddlGAttributeMain.SelectedValue = lblAttributeMainID.Text;
                string AttributeSub = ((Label)row.FindControl("lblAttributeSub")).Text.Trim();
                txtAttributeSub.Text = AttributeSub;
                HiddenID.Value = Convert.ToString(lblCustomerAttributeSubEdit.CommandArgument);
                BtnAddCustomerAttributeSub.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void lblCustomerAttributeSubDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblCustomerAttributeSubDelete = (LinkButton)sender;
                long AttributeSubID = Convert.ToInt32(lblCustomerAttributeSubDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblCustomerAttributeSubDelete.NamingContainer);
                int AttributeMainID = Convert.ToInt32(((Label)row.FindControl("lblAttributeMainID")).Text.Trim());
                string AttributeSub = ((Label)row.FindControl("lblAttributeSub")).Text.Trim();

                success = new BPresalesMasters().InsertOrUpdateCustomerAttributeSub(AttributeSubID, AttributeMainID, AttributeSub, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchCustomerAttributeSub();
                    lblMessage.Text = "Customer Attribute Sub was Deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Customer Attribute Sub was not Deleted successfully";
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
        void SearchEffortType()
        {
            int? EffortTypeID = ddlEffortType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlEffortType.SelectedValue);
            string EffortType = ddlEffortType.SelectedValue == "0" ? (string)null : ddlEffortType.SelectedItem.Text.Trim();

            List<PEffortType> EffortTypes = new BPresalesMasters().GetEffortType(EffortTypeID, EffortType);

            gvEffortType.DataSource = EffortTypes;
            gvEffortType.DataBind();
            if (EffortTypes.Count==0)
            {
                PEffortType pEffortType = new PEffortType();
                EffortTypes.Add(pEffortType);
                gvEffortType.DataSource = EffortTypes;
                gvEffortType.DataBind();
            }
        }
        protected void ddlEffortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchEffortType();
        }
        protected void gvEffortType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchEffortType();
            gvEffortType.PageIndex = e.NewPageIndex;
            gvEffortType.DataBind();
        }
        protected void BtnAddEffortType_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddEffortType = (Button)gvEffortType.FooterRow.FindControl("BtnAddEffortType");

                string EffortType = ((TextBox)gvEffortType.FooterRow.FindControl("txtEffortType")).Text.Trim();
                if (string.IsNullOrEmpty(EffortType))
                {
                    lblMessage.Text = "Please Enter Effort Type";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddEffortType.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateEffortType(null, EffortType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchEffortType();
                        lblMessage.Text = "Effort Type Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Effort Type Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Effort Type Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateEffortType(Convert.ToInt32(HiddenID.Value), EffortType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchEffortType();
                        lblMessage.Text = "Effort Type Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Effort Type Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Effort Type Not Updated Successfully...!";
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
        protected void lblEffortTypeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblEffortTypeEdit = (LinkButton)sender;
                TextBox txtEffortType = (TextBox)gvEffortType.FooterRow.FindControl("txtEffortType");
                Button BtnAddEffortType = (Button)gvEffortType.FooterRow.FindControl("BtnAddEffortType");
                GridViewRow row = (GridViewRow)(lblEffortTypeEdit.NamingContainer);
                string EffortType = ((Label)row.FindControl("lblEffortType")).Text.Trim();
                txtEffortType.Text = EffortType;
                HiddenID.Value = Convert.ToString(lblEffortTypeEdit.CommandArgument);
                BtnAddEffortType.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void lblEffortTypeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblEffortTypeDelete = (LinkButton)sender;
                long EffortTypeID = Convert.ToInt32(lblEffortTypeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblEffortTypeDelete.NamingContainer);
                string EffortType = ((Label)row.FindControl("lblEffortType")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateEffortType(EffortTypeID, EffortType, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchEffortType();
                    lblMessage.Text = "EffortType was Deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "EffortType was not Deleted successfully";
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
        void SearchExpenseType()
        {
            int? ExpenseTypeID = ddlExpenseType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlExpenseType.SelectedValue);
            string ExpenseType = ddlExpenseType.SelectedValue == "0" ? (string)null : ddlExpenseType.SelectedItem.Text.Trim();

            List<PExpenseType> ExpenseTypes = new BPresalesMasters().GetExpenseType(ExpenseTypeID, ExpenseType);

            gvExpenseType.DataSource = ExpenseTypes;
            gvExpenseType.DataBind();
            if (ExpenseTypes.Count == 0)
            {
                PExpenseType pExpenseType = new PExpenseType();
                ExpenseTypes.Add(pExpenseType);
                gvExpenseType.DataSource = ExpenseTypes;
                gvExpenseType.DataBind();
            }
        }
        protected void ddlExpenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchExpenseType();
        }
        protected void gvExpenseType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchExpenseType();
            gvExpenseType.PageIndex = e.NewPageIndex;
            gvExpenseType.DataBind();
        }
        protected void BtnAddExpenseType_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddExpenseType = (Button)gvExpenseType.FooterRow.FindControl("BtnAddExpenseType");

                string ExpenseType = ((TextBox)gvExpenseType.FooterRow.FindControl("txtExpenseType")).Text.Trim();
                if (string.IsNullOrEmpty(ExpenseType))
                {
                    lblMessage.Text = "Please Enter Expense Type";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddExpenseType.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateExpenseType(null, ExpenseType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        SearchExpenseType();
                        lblMessage.Text = "Expense Type Created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Expense Type Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Expense Type Not Created Successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateExpenseType(Convert.ToInt32(HiddenID.Value), ExpenseType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        SearchExpenseType();
                        lblMessage.Text = "Expense Type Updated Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Expense Type Already Found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Expense Type Not Updated Successfully...!";
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
        protected void lblExpenseTypeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lblExpenseTypeEdit = (LinkButton)sender;
                TextBox txtExpenseType = (TextBox)gvExpenseType.FooterRow.FindControl("txtExpenseType");
                Button BtnAddExpenseType = (Button)gvExpenseType.FooterRow.FindControl("BtnAddExpenseType");
                GridViewRow row = (GridViewRow)(lblExpenseTypeEdit.NamingContainer);
                string ExpenseType = ((Label)row.FindControl("lblExpenseType")).Text.Trim();
                txtExpenseType.Text = ExpenseType;
                HiddenID.Value = Convert.ToString(lblExpenseTypeEdit.CommandArgument);
                BtnAddExpenseType.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void lblExpenseTypeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lblExpenseTypeDelete = (LinkButton)sender;
                long ExpenseTypeID = Convert.ToInt32(lblExpenseTypeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblExpenseTypeDelete.NamingContainer);
                string ExpenseType = ((Label)row.FindControl("lblExpenseType")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateExpenseType(ExpenseTypeID, ExpenseType, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    SearchExpenseType();
                    lblMessage.Text = "Expense Type was Deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Expense Type was not Deleted successfully";
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