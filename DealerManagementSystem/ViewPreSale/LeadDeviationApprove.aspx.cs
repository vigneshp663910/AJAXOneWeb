using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DealerManagementSystem.ViewPreSale
{
    public partial class LeadDeviationApprove : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_LeadDeviationApprove; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Lead Deviation');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlSDealer, null);
                List<PLeadSource> Source = new BLead().GetLeadSource(null, null); 
                new DDLBind(ddlSRegion, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID"); 
                new DDLBind(ddlSProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillLeadDeviation();
        }
        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillLeadDeviation();
            }
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillLeadDeviation();
            }
        }
        void LeadBind(GridView gv, Label lbl, List<PLead> Lead1)
        {
            gv.DataSource = Lead1;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Lead1.Count;
        }

        void FillLeadDeviation()
        {
            PLeadSearch S = new PLeadSearch();
            S.RegionID = ddlSRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSRegion.SelectedValue);
           
            S.ProductTypeID = ddlSProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSProductType.SelectedValue);
            S.StatusID = (short)AjaxOneStatus.LeadDeviation_Requested;
            S.CustomerCode = txtSCustomer.Text.Trim();
            S.LeadDateFrom = string.IsNullOrEmpty(txtSDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtSDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtSDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtSDateTo.Text.Trim());
            S.DealerID = ddlSDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSDealer.SelectedValue);
            S.PageIndex = PageIndex;
            S.PageSize = gvLead.PageSize;

            PApiResult ResultLead = new BLead().GetLeadDeviation(S);
            gvLead.DataSource = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(ResultLead.Data));
            gvLead.DataBind();

            if (ResultLead.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
            }
            else
            {
                PageCount = (ResultLead.RowCount + gvLead.PageSize - 1) / gvLead.PageSize;
                lblRowCount.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvLead.PageSize) + 1) + " - " + (((PageIndex - 1) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + ResultLead.RowCount;
            }
        } 
        protected void btnApprove_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int LeadDeviationID = Convert.ToInt32(((Label)gvRow.FindControl("lblLeadDeviationID")).Text);
            PApiResult Results = new BLead().UpdateLeadDeviationApprove(LeadDeviationID);
            lblMessage.Text = Results.Message;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.ForeColor = Color.Red;
                return;
            }
            FillLeadDeviation();
            lblMessage.ForeColor = Color.Green;
        }
    }
}