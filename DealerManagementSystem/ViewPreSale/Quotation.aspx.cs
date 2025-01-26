using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
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

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Quotation : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Quotation; } }
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

        long? SalesQuotationID = null;
        string QuotationNo = null;
        string QuotationDateFrom = null;
        string QuotationDateTo = null;
        long? LeadID = null;
        string LeadNumber = null;
        int? StatusID = null;
        int? UserStatusID = null;
        int? DealerID = null;
        int? SalesEngineerID = null;
        string CustomerCode = null;
        int? ProductTypeID = null;
        int? ProductID = null;
        int? SalesChannelTypeID = null;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Quotation');</script>");

            lblMessage.Text = "";
            //lblMessageColdVisit.Text = "";

            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                if (Request.QueryString["Quotation"] != null)
                {
                    divColdVisitView.Visible = true;
                    btnBackToList.Visible = true;
                    divList.Visible = false;
                    UC_QuotationView.fillViewQuotation(Convert.ToInt64(Request.QueryString["Quotation"]));
                    Label lblMessageView = ((Label)UC_QuotationView.FindControl("lblMessage"));
                    lblMessageView.Text = "Your request successfully processed";
                    lblMessage.Visible = true;
                    lblMessageView.ForeColor = Color.Green;
                }
                //List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                //new DDLBind(ddlSCountry, Country, "Country", "CountryID", true, "All Country");
                //ddlSCountry.SelectedValue = "1";
                //List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, null);

                new DDLBind(ddlUserStatus, new BSalesQuotation().GetSalesQuotationUserStatus(null, null), "SalesQuotationUserStatus", "SalesQuotationUserStatusID");
                new DDLBind(ddlQuotationStatus, new BSalesQuotation().GetSalesQuotationStatus(null, null), "SalesQuotationStatus", "SalesQuotationStatusID");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
                new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, Convert.ToInt32(ddlProductType.SelectedValue), null), "Product", "ProductID");
                new DDLBind(ddlSSalesChannelType, new BPreSale().GetPreSalesMasterItem((short)PreSalesMasterHeader.SalesChannelType), "ItemText", "MasterItemID");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            PageCount = 0;
            PageIndex = 1;
            FillQuotation();
        }
              
        protected void ibtnQuoteArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillQuotation();
            }
        }
        protected void ibtnQuoteArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillQuotation();
            }
        }
  
        void Search()
        { 
            SalesQuotationID = null;
            QuotationNo = txtQuotationNumber.Text.Trim();
            QuotationDateFrom = txtDateFrom.Text.Trim();
            QuotationDateTo = txtDateTo.Text.Trim();
            //  long? LeadID = null;
            LeadNumber = txtLeadNumber.Text.Trim();
            StatusID = ddlQuotationStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlQuotationStatus.SelectedValue);
            UserStatusID = ddlUserStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlUserStatus.SelectedValue);
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            CustomerCode = txtCustomer.Text.Trim();
            ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
            ProductID = ddlProduct.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProduct.SelectedValue);
            SalesChannelTypeID = ddlSSalesChannelType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSalesChannelType.SelectedValue);
        }
        void FillQuotation()
        {
            Search();
            //List<PSalesQuotation> Quotations = new BSalesQuotation().GetSalesQuotationBasic(SalesQuotationID, RefQuotationID, LeadID, RefQuotationDate, QuotationNo, QuotationDateFrom, QuotationDateTo, QuotationTypeID, StatusID, DealerID, CustomerCode);
            PApiResult Result = new BSalesQuotation().GetSalesQuotationBasic(SalesQuotationID, QuotationNo, QuotationDateFrom, QuotationDateTo
               , LeadID, LeadNumber, StatusID, UserStatusID, ProductTypeID, ProductID, DealerID, SalesEngineerID, CustomerCode,SalesChannelTypeID, PageIndex, gvQuotation.PageSize);

            gvQuotation.DataSource = JsonConvert.DeserializeObject<List<PSalesQuotation>>(JsonConvert.SerializeObject(Result.Data));
            gvQuotation.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnQuoteArrowLeft.Visible = false;
                ibtnQuoteArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvQuotation.PageSize - 1) / gvQuotation.PageSize;
                lblRowCount.Visible = true;
                ibtnQuoteArrowLeft.Visible = true;
                ibtnQuoteArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvQuotation.PageSize) + 1) + " - " + (((PageIndex - 1) * gvQuotation.PageSize) + gvQuotation.Rows.Count) + " of " + Result.RowCount;
            }
        }
        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    MPE_Customer.Show();
        //    PColdVisit ColdVisitList = new PColdVisit();
        //    lblMessageColdVisit.ForeColor = Color.Red;
        //    lblMessageColdVisit.Visible = true;
        //    string Message = ""; 
        //    if (!string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
        //    {
        //        ColdVisitList.Customer = new PDMS_Customer();
        //        ColdVisitList.Customer.CustomerID = Convert.ToInt64(txtCustomerID.Text.Trim());
        //    }
        //    else
        //    {
        //        //Message = UC_Customer.ValidationCustomer();

        //        //if (!string.IsNullOrEmpty(Message))
        //        //{
        //        //    lblMessageColdVisit.Text = Message;
        //        //    return;
        //        //}
        //        //ColdVisitList.Customer = UC_Customer.ReadCustomer();
        //    }

        //    Message = ValidationColdVisit();
        //    if (!string.IsNullOrEmpty(Message))
        //    {
        //        lblMessageColdVisit.Text = Message;
        //        return;
        //    }

        //    ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtColdVisitDate.Text.Trim());
        //    ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
        //    ColdVisitList.Importance = new PImportance() { ImportanceID = Convert.ToInt32(ddlImportance.SelectedValue) };
        //    ColdVisitList.Remark = txtRemark.Text.Trim();

        //    ColdVisitList.CreatedBy = new PUser { UserID = PSession.User.UserID };
        //    string result = new BAPI().ApiPut("ColdVisit", ColdVisitList);

        //    result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
        //    if (result == "0")
        //    {
        //        MPE_Customer.Show();
        //        lblMessageColdVisit.Text = "Customer is not updated successfully ";
        //        return;
        //    }
        //    else
        //    {
        //        lblMessage.Visible = true;
        //        lblMessage.ForeColor = Color.Green;
        //        lblMessage.Text = "Customer is updated successfully ";
        //    }
        //    List<PColdVisit> Leads = new BColdVisit().GetColdVisit(Convert.ToInt64(result), null, null, null, null, null, null, null, null, null);
        //    gvQuotation.DataSource = Leads;
        //    gvQuotation.DataBind();
        //   // UC_Customer.FillClean();
        //    MPE_Customer.Hide();
        //}
        //public string ValidationColdVisit()
        //{
        //    string Message = "";
        //    txtColdVisitDate.BorderColor = Color.Silver;
        //    txtRemark.BorderColor = Color.Silver;
        //    ddlActionType.BorderColor = Color.Silver;
        //    if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
        //    {
        //        Message = "Please enter the Cold Visit Date";
        //        txtColdVisitDate.BorderColor = Color.Red;
        //    }
        //    if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please enter the Remark";
        //        txtRemark.BorderColor = Color.Red;
        //    }

        //    if (ddlActionType.SelectedValue == "0")
        //    {
        //        Message = Message + "<br/>Please select the Action Type";
        //        ddlActionType.BorderColor = Color.Red;
        //    }
        //    return Message;
        //}

        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
            //divCustomerView.Visible = true;
            //divColdVisitView.Visible = false;
            //btnBackToList.Visible = true;
            //divList.Visible = false;

            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            //Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");
            //UC_CustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text));
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divColdVisitView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }

        //protected void btnAddColdVisit_Click(object sender, EventArgs e)
        //{
        //    MPE_Customer.Show();
        //    //UC_Customer.FillMaster();
        //    new DDLBind(ddlActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
        //    new DDLBind(ddlImportance, new BDMS_Master().GetImportance(null, null), "Importance", "ImportanceID");
        //}
 
        protected void btnViewQuotation_Click(object sender, EventArgs e)
        {
            divColdVisitView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblQuotationID = (Label)gvRow.FindControl("lblQuotationID");
            UC_QuotationView.fillViewQuotation(Convert.ToInt64(lblQuotationID.Text));
        }
        protected void btnAddQuotation_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewPreSale/Lead.aspx");
        }

        [WebMethod]
        public static List<string> SearchSMaterial(string input)
        {
            List<string> Materials = new BDMS_Material().GetMaterialAutocomplete(input, "",null);
            return Materials.FindAll(item => item.ToLower().Contains(input.ToLower()));
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }

        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null,Convert.ToInt32(ddlProductType.SelectedValue),null), "Product", "ProductID");
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
                Search();
                //List<PSalesQuotation> Quotations = new BSalesQuotation().GetSalesQuotationBasic(SalesQuotationID, RefQuotationID, LeadID, RefQuotationDate, QuotationNo, QuotationDateFrom, QuotationDateTo, QuotationTypeID, StatusID, DealerID, CustomerCode);
                PApiResult Result = new BSalesQuotation().GetSalesQuotationExcel(SalesQuotationID, QuotationNo, QuotationDateFrom, QuotationDateTo
                   , LeadID, LeadNumber, StatusID, UserStatusID, ProductTypeID, ProductID, DealerID, SalesEngineerID, CustomerCode,SalesChannelTypeID);
                 
                try
                {
                    new BXcel().ExporttoExcel(JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data)), "Quotation Report");
                }
                catch
                {
                }
                finally
                {
                }
                // SalesCommisionClaimExportExcel();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }


        protected void gvQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#b3ecff';";
                //e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                //e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#80ff80';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click On View Icon for More Details... ";
            }
        }
    }
}