using Business;
using DealerManagementSystem.ViewPreSale.UserControls;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Quotation : System.Web.UI.Page
    {
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
                new BDealer().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
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
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID", true, "All Country");
                ddlSCountry.SelectedValue = "1";
                List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, null);

                new DDLBind(ddlUserStatus, new BSalesQuotation().GetSalesQuotationUserStatus(null, null), "SalesQuotationUserStatus", "SalesQuotationUserStatusID");
                new DDLBind(ddlQuotationStatus, new BSalesQuotation().GetSalesQuotationStatus(null, null), "SalesQuotationStatus", "SalesQuotationStatusID");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillQuotation();
        }

        public List<PSalesQuotation> Quote
        {
            get
            {
                if (Session["Quote"] == null)
                {
                    Session["Quote"] = new List<PSalesQuotation>();
                }
                return (List<PSalesQuotation>)Session["Quote"];
            }
            set
            {
                Session["Quote"] = value;
            }
        }

        protected void ibtnQuoteArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageIndex > 0)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex - 1;
                QuoteBind(gvQuotation, lblRowCount, Quote);
            }
        }


        protected void ibtnQuoteArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvQuotation.PageCount > gvQuotation.PageIndex)
            {
                gvQuotation.PageIndex = gvQuotation.PageIndex + 1;
                QuoteBind(gvQuotation, lblRowCount, Quote);
            }
        }

        void QuoteBind(GridView gv, Label lbl, List<PSalesQuotation> Quote)
        {
            gv.DataSource = Quote ;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Quote.Count ;
        }


        void FillQuotation()
        {
            long? SalesQuotationID = null;
            long? RefQuotationID = null;
            long? LeadID = null;
            DateTime? RefQuotationDate = null;
            string QuotationNo = null;
            DateTime? QuotationDateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? QuotationDateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());


            int? QuotationTypeID = null;
            int? StatusID = ddlQuotationStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlQuotationStatus.SelectedValue);

            string CustomerCode = null;
            int? UserStatusID = ddlUserStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlUserStatus.SelectedValue);

            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            
            //List<PSalesQuotation> Quotations = new BSalesQuotation().GetSalesQuotationBasic(SalesQuotationID, RefQuotationID, LeadID, RefQuotationDate, QuotationNo, QuotationDateFrom, QuotationDateTo, QuotationTypeID, StatusID, DealerID, CustomerCode);
            Quote = new BSalesQuotation().GetSalesQuotationBasic(SalesQuotationID, RefQuotationID, LeadID, RefQuotationDate, QuotationNo, QuotationDateFrom, QuotationDateTo, QuotationTypeID, StatusID, UserStatusID, DealerID, SalesEngineerID,  CustomerCode);
            gvQuotation.DataSource = Quote;
            gvQuotation.DataBind();


            if (Quote.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnQuoteArrowLeft.Visible = false;
                ibtnQuoteArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnQuoteArrowLeft.Visible = true;
                ibtnQuoteArrowRight.Visible = true;
                lblRowCount.Text = (((gvQuotation.PageIndex) * gvQuotation.PageSize) + 1) + " - " + (((gvQuotation.PageIndex) * gvQuotation.PageSize) + gvQuotation.Rows.Count) + " of " + Quote.Count;
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

        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvQuotation.PageIndex = e.NewPageIndex;
            FillQuotation();
        }

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
    }
}