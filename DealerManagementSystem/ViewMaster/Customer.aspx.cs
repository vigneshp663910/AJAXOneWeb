using Business;
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

namespace DealerManagementSystem.ViewMaster
{
    public partial class Customer : System.Web.UI.Page
    {
        public List<PDMS_Customer> Cust
        {
            get
            {
                if (Session["Customer"] == null)
                {
                    Session["Customer"] = new List<PDMS_Customer>();
                }
                return (List<PDMS_Customer>)Session["Customer"];
            }
            set
            {
                Session["Customer"] = value;
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
                new BDMS_Dealer().LoadDealerDDL(ddlDealer);
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        protected void ibtnCustArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustomer.PageIndex > 0)
            {
                gvCustomer.PageIndex = gvCustomer.PageIndex - 1;
                CustBind(gvCustomer, lblRowCount, Cust);
            }
        }
        protected void ibtnCustArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvCustomer.PageCount > gvCustomer.PageIndex)
            {
                gvCustomer.PageIndex = gvCustomer.PageIndex + 1;
                CustBind(gvCustomer, lblRowCount, Cust);
            }
        }


        void CustBind(GridView gv, Label lbl, List<PDMS_Customer> Cust)
        {
            gv.DataSource = Cust;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Cust.Count;
        }

        void SearchCustomer()
        {
            long? CustomerID = null;
            string CustomerCode = txtCustomerCode.Text.Trim();
            string CustomerName = txtCustomer.Text.Trim();
            string Mobile = txtMobile.Text.Trim();

            int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            Cust = new BDMS_Customer().GetCustomer(CustomerID, CustomerCode, CustomerName, Mobile, CountryID, StateID, DealerID); 
            gvCustomer.DataSource = Cust;
            gvCustomer.DataBind();


            if (Cust.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnCustArrowLeft.Visible = false;
                ibtnCustArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnCustArrowLeft.Visible = true;
                ibtnCustArrowRight.Visible = true;
                lblRowCount.Text = (((gvCustomer.PageIndex) * gvCustomer.PageSize) + 1) + " - " + (((gvCustomer.PageIndex) * gvCustomer.PageSize) + gvCustomer.Rows.Count) + " of " + Cust.Count;
            }

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
            
            //CustomerView ucCustomerView = (CustomerView)LoadControl("~/ViewPreSale/UserControls/CustomerView.ascx");
            //ucCustomerView.ID = "ucCustomerView";

            //PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_1");
            //phDashboard.Controls.Add(ucCustomerView);
            //ucCustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text)); 
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            Panel PnlCustomerVerification = (Panel)UC_CustomerView.FindControl("PnlCustomerVerification");
            if (PnlCustomerVerification.Visible)
            {
                PnlCustomerVerification.Visible = false;
                Panel PnlCustomerView = (Panel)UC_CustomerView.FindControl("PnlCustomerView");
                PnlCustomerView.Visible = true;

                UC_CustomerView.fillCustomer(UC_CustomerView.Customer.CustomerID);
            }
            else
            {
                divCustomerView.Visible = false;
                divList.Visible = true;
            }
        }

        public void btnBackToList_Click()
        {
            Panel PnlCustomerVerification = (Panel)UC_CustomerView.FindControl("PnlCustomerVerification");
            if (PnlCustomerVerification.Visible)
            {
                PnlCustomerVerification.Visible = false;
                Panel PnlCustomerView = (Panel)UC_CustomerView.FindControl("PnlCustomerView");
                PnlCustomerView.Visible = true;
            }
            else
            {
                divCustomerView.Visible = false;
                divList.Visible = true;
            }

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
        [WebMethod]
        public static List<string> GetCustomer(string CustS)
        {
            List<string> Emp = new List<string>();
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS,0);
            int i = 0;
            foreach (PDMS_Customer cust in Customer)
            {
                i = i + 1;

                string div = "<label id='lblCustomerID" + i + "' style='display: none'>" + cust.CustomerID + "</label>"
                    + "<p><label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label><span>" + cust.CustomerType + "</span></p>"

                    + "<div class='customer-info'><label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label>"
                    + "<label id='lblMobile" + i + "'>" + cust.Mobile + "</label></div>";
                Emp.Add(div);


                //string div = "<label id='lblCustomerID" + i + "' style='display: none'>" + cust.CustomerID + "</label>"
                //                   + "<table><tr><td>"
                //                   + "<label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label></td><td>Prospect</td></tr >" + "<tr><td>"
                //                   + "<label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label></td><td>"
                //                   + "<label id='lblMobile" + i + "'>" + cust.Mobile + " </td></tr></ table >";
                //Emp.Add(div);
            }
            return Emp;
        }

        protected void btnViewCustomer_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");

            UC_CustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text));
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerExportExcel(Cust, "Customer Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void CustomerExportExcel(List<PDMS_Customer> Customers, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerID");
            dt.Columns.Add("Customer Code");
            dt.Columns.Add("Title");
            dt.Columns.Add("Name");
            dt.Columns.Add("Name2");
            dt.Columns.Add("Contact Person");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Alternative Mobile");
            dt.Columns.Add("Email");
            dt.Columns.Add("Address1");
            dt.Columns.Add("Address2");
            dt.Columns.Add("Address3");
            dt.Columns.Add("Country");
            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("GSTIN");
            dt.Columns.Add("PAN");
            dt.Columns.Add("Tehsil");
            dt.Columns.Add("City");
            dt.Columns.Add("DOB");
            dt.Columns.Add("Anniversary Dt");
            dt.Columns.Add("SendSMS");
            dt.Columns.Add("SendEmail");            
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("CustomerType");
            dt.Columns.Add("IsActive");
            //dt.Columns.Add("IsVerified");
            //dt.Columns.Add("VerifiedBy");
            //dt.Columns.Add("VerifiedOn");
            foreach (PDMS_Customer Customer in Customers)
            {
                dt.Rows.Add(
                    "'" + Customer.CustomerID
                    , Customer.CustomerCode
                    , Customer.Title.Title
                   , Customer.CustomerName
                   , Customer.CustomerName2
                   , Customer.ContactPerson
                   , Customer.Mobile
                   , Customer.AlternativeMobile
                   , Customer.Email
                   , Customer.Address1
                   , Customer.Address2
                   , Customer.Address3
                   , Customer.Country.Country
                   , Customer.State.State
                   , Customer.District.District
                   , Customer.GSTIN
                   , Customer.PAN
                   , Customer.Tehsil
                   , Customer.City
                   , Customer.DOB
                   , Customer.DOAnniversary
                   , Customer.SendSMS
                   , Customer.SendEmail
                   , Customer.Latitude
                   , Customer.Longitude
                   , Customer.CustomerType
                   , Customer.IsActive
                   //, Customer.IsVerified
                   //, Customer.VerifiedBy.ContactName
                   //, Customer.VerifiedOn
                    );
            }
            try
            {
                new BXcel().ExporttoExcel(dt, Name);
            }
            catch
            {

            }
            finally
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>HideProgress();</script>");
            }
        }
    }
}