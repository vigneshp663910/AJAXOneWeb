using AjaxControlToolkit;
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
    public partial class LeadDeviation : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_LeadDeviation; } }
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
                new DDLBind(ddlSSource, Source, "Source", "SourceID");  
                new DDLBind(ddlSRegion, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID");
                new DDLBind(ddlSStatus, new BDMS_Master().GetAjaxOneStatus((short)AjaxOneStatusHeader.LeadDeviation), "Status", "StatusID"); 
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
            S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
            S.ProductTypeID = ddlSProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSProductType.SelectedValue); 
            S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            PLeadDeviation_Insert Lead = new PLeadDeviation_Insert();
            lblMessageLead.ForeColor = Color.Red;
            lblMessageLead.Visible = true;
            string Message = "";
            Message = Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageLead.Text = Message;
                return;
            }
            Lead = Read();  

            string result = new BAPI().ApiPut("Lead/InsertLeadDeviation", Lead);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageLead.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            PLeadSearch S = new PLeadSearch();
            S.LeadID = Convert.ToInt64(Result.Data);
            PApiResult ResultLead1 = new BLead().GetLeadDeviation(S);
            DataTable LeadD = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(ResultLead1.Data));
            gvLead.DataSource = LeadD;
            gvLead.DataBind();

            if (LeadD.Rows.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvLead.PageSize - 1) / gvLead.PageSize;
                lblRowCount.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvLead.PageSize) + 1) + " - " + (((PageIndex - 1) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + LeadD.Rows.Count;
            } 
            MPE_Customer.Hide();
        }
        public string Validation()
        {
            string Message = "";
            txtNextFollowUpDate.BorderColor = Color.Silver;
            ddlProductType.BorderColor = Color.Silver;
            txtExpectedDateOfSale.BorderColor = Color.Silver; 
            ddlSource.BorderColor = Color.Silver;  
            if (string.IsNullOrEmpty(txtNextFollowUpDate.Text.Trim()))
            {
                txtNextFollowUpDate.BorderColor = Color.Red;
                return "Please enter the Next FollowUp Date.";
            } 
            if (ddlProductType.SelectedValue == "0")
            {
                ddlProductType.BorderColor = Color.Red;
                return "Please select the Product Type";
            }
            if (string.IsNullOrEmpty(txtExpectedDateOfSale.Text.Trim()))
            {
                txtExpectedDateOfSale.BorderColor = Color.Red;
                return "Please select the Expected Date of Sale";
            } 
            if (ddlSource.SelectedValue == "0")
            {
                ddlSource.BorderColor = Color.Red;
                return "Please select the Source";
            }  
            return Message;
        }
        public PLeadDeviation_Insert Read()
        {
            PLeadDeviation_Insert Lead = new PLeadDeviation_Insert();
            Lead.DealerID= Convert.ToInt32(ddlDealer.SelectedValue);
            List<PDMS_Customer> cust = new BDMS_Customer().GetCustomerByCode(null, txtCustomer.Text.Trim());
            if(cust.Count!=1)
            {
                throw new Exception( "");
            }
            Lead.CustomerID = cust[0].CustomerID;
            Lead.ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue); 
            Lead.SourceID = ddlSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSource.SelectedValue); 
            Lead.ExpectedDateOfSale = Convert.ToDateTime(txtExpectedDateOfSale.Text.Trim());
            Lead.MainApplicationID = ddlApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlApplication.SelectedValue);
            Lead.CustomerFeedback = txtCustomerFeedback.Text.Trim();
            Lead.Remarks = txtRemarks.Text.Trim();
            Lead.NextFollowUpDate = Convert.ToDateTime(txtNextFollowUpDate.Text.Trim());
            Lead.SalesChannelTypeID = ddlSalesChannelType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesChannelType.SelectedValue);
            return Lead;
        }
        protected void btnAddLead_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");

            new DDLBind(ddlSource, new BLead().GetLeadSource(null, null), "Source", "SourceID");
            new DDLBind(ddlApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID");
            new DDLBind(ddlSalesChannelType, new BPreSale().GetPreSalesMasterItem((short)PreSalesMasterHeader.SalesChannelType), "ItemText", "MasterItemID");
        }
 
    }
}