using Business;
using Newtonsoft.Json;
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
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Customer');</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master <i class= '+ '"' + 'fa fa-angle-double-down fa-2x' + '"'> </i>Customer');</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master < i class='fa fa-fw fa-home font-white' style='color: lightgray'></i> Customer');</script>");


            lblMessageCustomer.Text = "";
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
                ddlSCountry.SelectedValue = "1";
                List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID"); 
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }
        void SearchCustomer()
        {
            long? CustomerID = null;
            string CustomerCode = null;
            string CustomerName = txtCustomer.Text.Trim();
            string Mobile = txtMobile.Text.Trim();

            int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            int? DistrictID = null;
            List<PDMS_Customer> Leads = new BDMS_Customer().GetCustomer(CustomerID, CustomerCode, CustomerName, Mobile, CountryID, StateID, DistrictID);

            gvCustomer.DataSource = Leads;
            gvCustomer.DataBind();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Message = UC_Customer.ValidationCustomer();
            lblMessageCustomer.ForeColor = Color.Red;
            lblMessageCustomer.Visible = true;
            MPE_Customer.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageCustomer.Text = Message;
                return;
            }
            PDMS_Customer cust = UC_Customer.ReadCustomer();
            string result = new BAPI().ApiPut("Customer", cust);
            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data); 
            if (result == "0")
            {
                MPE_Customer.Show();
                lblMessageCustomer.Text = "Customer is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Customer is updated successfully ";
            }
            List<PDMS_Customer> Leads = new BDMS_Customer().GetCustomer(Convert.ToInt64(result), "", "", "", null, null, null);
            gvCustomer.DataSource = Leads;
            gvCustomer.DataBind();
            UC_Customer.FillClean();
            MPE_Customer.Hide(); 
        }
        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");

            UC_CustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text));
            //CustomerView ucCustomerView = (CustomerView)LoadControl("~/ViewPreSale/UserControls/CustomerView.ascx");
            //ucCustomerView.ID = "ucCustomerView";

            //PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_1");
            //phDashboard.Controls.Add(ucCustomerView);
            //ucCustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text)); 
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = false;
            divList.Visible = true;
        }

        protected void btnAddColdVisit_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            UC_Customer.FillMaster();
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomer.PageIndex = e.NewPageIndex;
            SearchCustomer();
        }
    }
}