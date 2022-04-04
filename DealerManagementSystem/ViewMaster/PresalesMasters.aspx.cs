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

                    new DDLBind(ddlMake, new BDMS_Master().GetMake(null, null), "Make", "MakeID", true, "Select");
                    new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select");

                    SearchLeadSource();
                    SearchActionType();
                    SearchCustomerAttributeMain();
                    SearchCustomerAttributeSub();
                    SearchEffortType();
                    SearchExpenseType();
                    GetMake();
                    GetProductType();
                    GetProduct();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }



        public List<PLeadSource> LLead
        {
            get
            {
                if (Session["PLeadSource"] == null)
                {
                    Session["PLeadSource"] = new List<PLeadSource>();
                }
                return (List<PLeadSource>)Session["PLeadSource"];
            }
            set
            {
                Session["PLeadSource"] = value;
            }
        }


        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLeadSource.PageIndex > 0)
            {
                gvLeadSource.PageIndex = gvLeadSource.PageIndex - 1;
                LeadBind(gvLeadSource, lblRowCountE, LLead);
            }
        }


        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLeadSource.PageCount > gvLeadSource.PageIndex)
            {
                gvLeadSource.PageIndex = gvLeadSource.PageIndex + 1;
                LeadBind(gvLeadSource, lblRowCountE, LLead);
            }
        }

        void LeadBind(GridView gv, Label lbl, List<PLeadSource> LLead)
        {
            gv.DataSource = LLead;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + LLead.Count;
        }


        void SearchLeadSource()
        {
            int? LeadSourceID = ddlLeadSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlLeadSource.SelectedValue);
            string LeadSource = ddlLeadSource.SelectedValue == "0" ? (string)null : ddlLeadSource.SelectedItem.Text.Trim();

            //List<PLeadSource> leadSources = new BPresalesMasters().GetLeadSource(LeadSourceID, LeadSource);

            LLead = new BPresalesMasters().GetLeadSource(LeadSourceID, LeadSource);

            gvLeadSource.DataSource = LLead;
            gvLeadSource.DataBind();
            if (LLead.Count == 0)
            {
                PLeadSource pLeadSource = new PLeadSource();
                LLead.Add(pLeadSource);
                gvLeadSource.DataSource = LLead;
                gvLeadSource.DataBind();
            }


            if (LLead.Count == 0)
            {
                lblRowCountE.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
            }
            else
            {
                lblRowCountE.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                lblRowCountE.Text = (((gvLeadSource.PageIndex) * gvLeadSource.PageSize) + 1) + " - " + (((gvLeadSource.PageIndex) * gvLeadSource.PageSize) + gvLeadSource.Rows.Count) + " of " + LLead.Count;
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
            //SearchLeadSource();
            gvLeadSource.PageIndex = e.NewPageIndex;
            SearchLeadSource();
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


        public List<PCustomerAttributeSub> CAS
        {
            get
            {
                if (Session["CAS"] == null)
                {
                    Session["CAS"] = new List<PCustomerAttributeSub>();
                }
                return (List<PCustomerAttributeSub>)Session["CAS"];
            }
            set
            {
                Session["CAS"] = value;
            }
        }

        protected void ibtnCASArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustomerAttributeSub.PageIndex > 0)
            {
                gvCustomerAttributeSub.PageIndex = gvCustomerAttributeSub.PageIndex - 1;
                CASBind(gvCustomerAttributeSub, lblRowCountS, CAS);
            }
        }
        protected void ibtnCASArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustomerAttributeSub.PageCount > gvCustomerAttributeSub.PageIndex)
            {
                gvCustomerAttributeSub.PageIndex = gvCustomerAttributeSub.PageIndex + 1;
                CASBind(gvCustomerAttributeSub, lblRowCountS, CAS);
            }
        }

        void CASBind(GridView gv, Label lbl, List<PCustomerAttributeSub> CAS)
        {
            gv.DataSource = CAS;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + CAS.Count;
        }




        void SearchCustomerAttributeSub()
        {
            int? CustomerAttributeMainID = ddlSCustomerAttributeMain.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCustomerAttributeMain.SelectedValue);
            int? CustomerAttributeSubID = ddlCustomerAttributeSub.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCustomerAttributeSub.SelectedValue);
            string CustomerAttributeSub = ddlCustomerAttributeSub.SelectedValue == "0" ? (string)null : ddlCustomerAttributeSub.SelectedItem.Text.Trim();

            //List<PCustomerAttributeSub> pCustomerAttributeSubs = new BDMS_Customer().GetCustomerAttributeSub(CustomerAttributeSubID,CustomerAttributeMainID,  CustomerAttributeSub);

            CAS = new BDMS_Customer().GetCustomerAttributeSub(CustomerAttributeSubID, CustomerAttributeMainID, CustomerAttributeSub);

            gvCustomerAttributeSub.DataSource = CAS;
            gvCustomerAttributeSub.DataBind();
            if (CAS.Count == 0)
            {
                PCustomerAttributeSub pCustomerAttributeSub = new PCustomerAttributeSub();
                CAS.Add(pCustomerAttributeSub);
                gvCustomerAttributeSub.DataSource = CAS;
                gvCustomerAttributeSub.DataBind();
            }


            if (CAS.Count == 0)
            {
                lblRowCountS.Visible = false;
                ibtnCASArrowLeft.Visible = false;
                ibtnCASArrowRight.Visible = false;
            }
            else
            {
                lblRowCountS.Visible = true;
                ibtnModelArrowLeft.Visible = true;
                ibtnModelArrowRight.Visible = true;
                lblRowCountS.Text = (((gvCustomerAttributeSub.PageIndex) * gvCustomerAttributeSub.PageSize) + 1) + " - " + (((gvCustomerAttributeSub.PageIndex) * gvCustomerAttributeSub.PageSize) + gvCustomerAttributeSub.Rows.Count) + " of " + CAS.Count;
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
            //SearchCustomerAttributeSub();
            gvCustomerAttributeSub.PageIndex = e.NewPageIndex;
            SearchCustomerAttributeSub();
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

        private void GetMake()
        { 
            List<PMake> make = new BDMS_Master().GetMake(null, null); 
            if (make.Count == 0)
            {
                PMake pMake = new PMake();
                make.Add(pMake); 
            }
            gvMake.DataSource = make;
            gvMake.DataBind();
        }

        protected void gvMake_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetMake();
            gvMake.PageIndex = e.NewPageIndex;
            gvMake.DataBind();
        }

        protected void BtnAddOrUpdateMake_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddOrUpdateMake = (Button)gvMake.FooterRow.FindControl("BtnAddOrUpdateMake");

                string Make = ((TextBox)gvMake.FooterRow.FindControl("txtMake")).Text.Trim();
                if (string.IsNullOrEmpty(Make))
                {
                    lblMessage.Text = "Please enter Make";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateMake.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateMake(null, Make, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        GetMake();
                        lblMessage.Text = "Make created successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Make already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Make not created successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateMake(Convert.ToInt32(HiddenID.Value), Make, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        GetMake();
                        lblMessage.Text = "Make updated successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Make already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Make not updated successfully...!";
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

        protected void lnkBtnMakeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkMakeEdit = (LinkButton)sender;
                TextBox txtMake = (TextBox)gvMake.FooterRow.FindControl("txtMake");
                Button BtnAddOrUpdateMake = (Button)gvMake.FooterRow.FindControl("BtnAddOrUpdateMake");
                GridViewRow row = (GridViewRow)(lnkMakeEdit.NamingContainer);
                string Make = ((Label)row.FindControl("lblMake")).Text.Trim();
                txtMake.Text = Make;
                HiddenID.Value = Convert.ToString(lnkMakeEdit.CommandArgument);
                BtnAddOrUpdateMake.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnMakeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lnkBtnMakeDelete = (LinkButton)sender;
                int MakeID = Convert.ToInt32(lnkBtnMakeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnMakeDelete.NamingContainer);
                string Make = ((Label)row.FindControl("lblMake")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateMake(MakeID, Make, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    GetMake();
                    lblMessage.Text = "Make deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Make not deleted successfully";
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

        private void GetProductType()
        {
            int? ProductTypeID = (int?)null;
            string ProductType = (string)null;

            List<PProductType> productType = new BDMS_Master().GetProductType(ProductTypeID, ProductType); 
            if (productType.Count == 0)
            {
                PProductType pProductType = new PProductType();
                productType.Add(pProductType); 
            }
            gvProductType.DataSource = productType;
            gvProductType.DataBind();
        }

        protected void gvProductType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetProductType();
            gvProductType.PageIndex = e.NewPageIndex;
            gvProductType.DataBind();
        }

        protected void BtnAddOrUpdateProductType_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddOrUpdateProductType = (Button)gvProductType.FooterRow.FindControl("BtnAddOrUpdateProductType");

                string ProductType = ((TextBox)gvProductType.FooterRow.FindControl("txtProductType")).Text.Trim();
                if (string.IsNullOrEmpty(ProductType))
                {
                    lblMessage.Text = "Please enter Product Type";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                if (BtnAddOrUpdateProductType.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateProductType(null, ProductType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        GetProductType();
                        lblMessage.Text = "Product Type created Successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product Type already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product Type not created successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateProductType(Convert.ToInt32(HiddenID.Value), ProductType, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        GetProductType();
                        lblMessage.Text = "Product Type updated successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product Type already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product Type not updated successfully...!";
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

        protected void lnkBtnProductTypeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnProductTypeEdit = (LinkButton)sender;
                TextBox txtProductType = (TextBox)gvProductType.FooterRow.FindControl("txtProductType");
                Button BtnAddOrUpdateProductType = (Button)gvProductType.FooterRow.FindControl("BtnAddOrUpdateProductType");
                GridViewRow row = (GridViewRow)(lnkBtnProductTypeEdit.NamingContainer);
                string ProductType = ((Label)row.FindControl("lblProductType")).Text.Trim();
                txtProductType.Text = ProductType;
                HiddenID.Value = Convert.ToString(lnkBtnProductTypeEdit.CommandArgument);
                BtnAddOrUpdateProductType.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnProductTypeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                LinkButton lnkBtnProductTypeDelete = (LinkButton)sender;
                int ProductTypeID = Convert.ToInt32(lnkBtnProductTypeDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnProductTypeDelete.NamingContainer);
                string ProductType = ((Label)row.FindControl("lblProductType")).Text.Trim();
                success = new BPresalesMasters().InsertOrUpdateProductType(ProductTypeID, ProductType, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    GetProductType();
                    lblMessage.Text = "Product Type deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Product Type not deleted successfully";
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

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetProduct();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetProduct();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void gvProduct_DataBound(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        public List<PProduct> Prod
        {
            get
            {
                if (Session["Product"] == null)
                {
                    Session["Product"] = new List<PProduct>();
                }
                return (List<PProduct>)Session["Product"];
            }
            set
            {
                Session["Product"] = value;
            }
        }


        protected void ibtnModelArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvProduct.PageIndex > 0)
            {
                gvProduct.PageIndex = gvProduct.PageIndex - 1;
                ProductBind(gvProduct, lblRowCount, Prod);
            }
        }


        protected void ibtnModelArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvProduct.PageCount > gvProduct.PageIndex)
            {
                gvProduct.PageIndex = gvProduct.PageIndex + 1;
                ProductBind(gvProduct, lblRowCount, Prod);
            }
        }


        void ProductBind(GridView gv, Label lbl, List<PProduct> Prod)
        {
            gv.DataSource = Prod;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Prod.Count;
        }


        private void GetProduct()
        {
            int? ProductID = null;
            string Product = null;

            int? MakeID = ddlMake.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMake.SelectedValue);
            int? ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
            //List<PProduct> product = new BDMS_Master().GetProduct(ProductID,MakeID, ProductTypeID, Product);
            Prod = new BDMS_Master().GetProduct(ProductID, MakeID, ProductTypeID, Product);

            if (Prod.Count == 0)
            {
                PProduct pProduct = new PProduct();
                Prod.Add(pProduct); 
            }
            gvProduct.DataSource = Prod;
            gvProduct.DataBind();
            DropDownList ddlProductMake = gvProduct.FooterRow.FindControl("ddlProductMakeF") as DropDownList;
            new DDLBind(ddlProductMake, new BDMS_Master().GetMake(null, null), "Make", "MakeID", true, "Select");

            DropDownList ddlProductTypeF = gvProduct.FooterRow.FindControl("ddlProductTypeF") as DropDownList;
            new DDLBind(ddlProductTypeF, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select");


            if (Prod.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnModelArrowLeft.Visible = false;
                ibtnModelArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnModelArrowLeft.Visible = true;
                ibtnModelArrowRight.Visible = true;
                lblRowCount.Text = (((gvProduct.PageIndex) * gvProduct.PageSize) + 1) + " - " + (((gvProduct.PageIndex) * gvProduct.PageSize) + gvProduct.Rows.Count) + " of " + Prod.Count;
            }

        }

        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
            gvProduct.PageIndex = e.NewPageIndex;
            GetProduct();
            gvProduct.DataBind();
        }

        protected void BtnAddOrUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAddOrUpdateProduct = (Button)gvProduct.FooterRow.FindControl("BtnAddOrUpdateProduct");
                DropDownList ddlGProductMake = (DropDownList)gvProduct.FooterRow.FindControl("ddlProductMakeF");
                DropDownList ddlGProductType = (DropDownList)gvProduct.FooterRow.FindControl("ddlProductTypeF");
                string Product = ((TextBox)gvProduct.FooterRow.FindControl("txtProduct")).Text.Trim();
                
                if (ddlGProductMake.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Product Make";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (ddlGProductType.SelectedValue == "0")
                {
                    lblMessage.Text = "Please select Product Type";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (string.IsNullOrEmpty(Product))
                {
                    lblMessage.Text = "Please enter Product";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                int MakeID = Convert.ToInt32(ddlGProductMake.SelectedValue);
                int ProductTypeID = Convert.ToInt32(ddlGProductType.SelectedValue);
                if (BtnAddOrUpdateProduct.Text == "Add")
                {
                    success = new BPresalesMasters().InsertOrUpdateProduct(null, MakeID, ProductTypeID, Product, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        GetProduct();
                        lblMessage.Text = "Product created successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product not created successfully...!";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    success = new BPresalesMasters().InsertOrUpdateProduct(Convert.ToInt32(HiddenID.Value), MakeID, ProductTypeID, Product, true, PSession.User.UserID);
                    if (success == 1)
                    {
                        HiddenID.Value = null;
                        GetProduct();
                        lblMessage.Text = "Product updated successfully...!";
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else if (success == 2)
                    {
                        lblMessage.Text = "Product already found";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Product not updated successfully...!";
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

        protected void lnkBtnProductEdit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                LinkButton lnkBtnProductEdit = (LinkButton)sender;
                DropDownList ddlProductMakeF = (DropDownList)gvProduct.FooterRow.FindControl("ddlProductMakeF");
                DropDownList ddlProductTypeF = (DropDownList)gvProduct.FooterRow.FindControl("ddlProductTypeF");
                TextBox txtProduct = (TextBox)gvProduct.FooterRow.FindControl("txtProduct");
                Button BtnAddOrUpdateProduct = (Button)gvProduct.FooterRow.FindControl("BtnAddOrUpdateProduct");
                GridViewRow row = (GridViewRow)(lnkBtnProductEdit.NamingContainer);
                Label lblProductMakeID = (Label)row.FindControl("lblProductMakeID");
                ddlProductMakeF.SelectedValue = lblProductMakeID.Text;

                Label lblProductTypeID = (Label)row.FindControl("lblProductTypeID");
                ddlProductTypeF.SelectedValue = lblProductTypeID.Text;

                string Product = ((Label)row.FindControl("lblProduct")).Text.Trim();
                txtProduct.Text = Product;
                HiddenID.Value = Convert.ToString(lnkBtnProductEdit.CommandArgument);
                BtnAddOrUpdateProduct.Text = "Update";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void lnkBtnProductDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;

                LinkButton lnkBtnProductDelete = (LinkButton)sender;
                int ProductID = Convert.ToInt32(lnkBtnProductDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lnkBtnProductDelete.NamingContainer);
                int ProductMakeID = Convert.ToInt32(((Label)row.FindControl("lblProductMakeID")).Text.Trim());
                int ProductTypeID = Convert.ToInt32(((Label)row.FindControl("lblProductTypeID")).Text.Trim());
                string Product = ((Label)row.FindControl("lblProduct")).Text.Trim();



                success = new BPresalesMasters().InsertOrUpdateProduct(ProductID, ProductMakeID, ProductTypeID, Product, false, PSession.User.UserID);
                if (success == 1)
                {
                    HiddenID.Value = null;
                    GetProduct();
                    lblMessage.Text = "Product deleted successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Product not deleted successfully";
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