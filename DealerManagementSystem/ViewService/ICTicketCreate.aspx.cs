using Business;
using Newtonsoft.Json;
using Newtonsoft.JsonResult;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketCreate : System.Web.UI.Page
    {
        public long EquipmentHeaderID
        {
            get
            {
                if (ViewState["EquipmentHeaderID"] == null)
                {
                    ViewState["EquipmentHeaderID"] = 0;
                }
                return (long)ViewState["EquipmentHeaderID"];
            }
            set
            {
                ViewState["EquipmentHeaderID"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Create');</script>");
            if (!IsPostBack)
            {
                hdfCustomerId.Value = "";
                FillMaster(); 
            }
        }
        public void FillMaster()
        { 
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            PDealer Dealer = PSession.User.Dealer[0];
            int CountryID = Dealer.Country.CountryID;
            ddlCountry.SelectedValue = Convert.ToString(CountryID);
            new DDLBind(ddlState, new BDMS_Address().GetState(null, CountryID, null, null, null), "State", "StateID");
            FillGetServicePriority();
            Clear();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PDMS_State> State = new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            new DDLBind(ddlState, State, "State", "StateID");
            ddlDistrict.Items.Clear();
            ddlDistrict.DataSource = null;
            ddlDistrict.DataBind();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            long? CustomerID = null;
            string EquipmentSerialNo = txtEquipment.Text.Trim();
            string Customer = txtCustomer.Text.Trim();
            if (!string.IsNullOrEmpty(hdfCustomerId.Value))
            {
                CustomerID = Convert.ToInt64(hdfCustomerId.Value);
                Customer = null;
            }
            gvEquipment.DataSource = new BDMS_Equipment().GetEquipmentForCreateICTicket(CustomerID, EquipmentSerialNo, Customer);
            gvEquipment.DataBind();

            gvICTickets.Visible = false;
            gvEquipment.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            Label lblEquipmentHeaderID = null;
            Label lblCustomerID = null;
            for (int i = 0; i < gvEquipment.Rows.Count; i++)
            {
                RadioButton rbCheck = (RadioButton)gvEquipment.Rows[i].FindControl("rbCheck"); 
                if (rbCheck.Checked)
                {
                    lblEquipmentHeaderID = (Label)gvEquipment.Rows[i].FindControl("lblEquipmentHeaderID");
                    lblCustomerID = (Label)gvEquipment.Rows[i].FindControl("lblCustomerID");
                    break;
                }
            }

            if(lblEquipmentHeaderID == null)
            {
                lblMessage.Text = "Please select the Equipment ";
                return;
            }

            string Valid = Validation();
            if (!string.IsNullOrEmpty(Valid))
            {
                lblMessage.Text = Valid;
                return;
            }
            PICTicket_Create IC = new PICTicket_Create();
         //   IC.RequestedDate = Convert.ToDateTime(txtRequestedDate.Text.Trim()); 
          //  IC.RequestedDate = Convert.ToDateTime(txtRequestedDate.Text.Trim() + " " + ddlRequestedHH.SelectedValue + ":" + ddlRequestedMM.SelectedValue);
            IC.ContactPerson = txtContactPerson.Text;
            IC.PresentContactNumber = txtContactNumber.Text;
            IC.ComplaintDescription = txtComplaintDescription.Text;
             IC.Location = txtLocation.Text;
            IC.ServicePriorityID = Convert.ToInt32(ddlServicePriority.SelectedValue) ;

            IC.EquipmentHeaderID = Convert.ToInt64(lblEquipmentHeaderID.Text) ;
            IC.CustomerID = Convert.ToInt64(lblCustomerID.Text) ;
            IC.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            IC.StateID = Convert.ToInt32(ddlState.SelectedValue);
            IC.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket", IC));
            lblMessage.Text = Results.Message;
            if (Results.Status == PApplication.Failure)
            {
                return;
            }

            Clear();
            lblMessage.ForeColor = Color.Green;
            gvICTickets.Visible = false;
            gvEquipment.Visible = true;
        }
        private void FillGetServicePriority()
        {
            ddlServicePriority.DataTextField = "ServicePriority";
            ddlServicePriority.DataValueField = "ServicePriorityID";
            ddlServicePriority.DataSource = new BDMS_Service().GetServicePriority(null, null);
            ddlServicePriority.DataBind();
            ddlServicePriority.Items.Insert(0, new ListItem("Select", "0"));
        }

        [WebMethod]
        public static string GetCustomer(string CustS)
        {
            //  List<string> Emp = new List<string>();
            //  List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS, 0);
            //int i = 0;
            //foreach (PDMS_Customer cust in Customer)
            //{
            //    i = i + 1; 
            //    string div = "<label id='lblCustomerID" + i + "' style='display: none'>" + cust.CustomerID + "</label>"
            //        + "<p><label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label><span>" + cust.CustomerType + "</span></p>"

            //        + "<div class='customer-info'><label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label>"
            //        + "<label id='lblMobile" + i + "'>" + cust.Mobile + "</label></div>";
            //    Emp.Add(div); 
            // }

            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS, 0);
            return JsonConvert.SerializeObject(Customer);
        }

        string Validation()
        {
           
            txtContactNumber.BorderColor = Color.Silver;
            txtContactPerson.BorderColor = Color.Silver;
            txtComplaintDescription.BorderColor = Color.Silver;
           // txtRequestedDate.BorderColor = Color.Silver;

           // ddlRequestedHH.BorderColor = Color.Silver;
           // ddlRequestedMM.BorderColor = Color.Silver;
            ddlServicePriority.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtContactNumber.Text.Trim()))
            {
                txtContactNumber.BorderColor = Color.Red;
                return "Please enter the Contact Number."; 
            }
            long value;
            if (!long.TryParse("0" + txtContactNumber.Text, out value))
            {
                txtContactNumber.BorderColor = Color.Red;
                return "Please enter valid Mobile number";
            }
            if (string.IsNullOrEmpty(txtContactPerson.Text.Trim()))
            {
                txtContactPerson.BorderColor = Color.Red;
                return "Please enter the Contact Person.";
            }
            if (string.IsNullOrEmpty(txtComplaintDescription.Text.Trim()))
            {
                txtComplaintDescription.BorderColor = Color.Red;
                return "Please enter the Complaint Description";
            }

            //if (string.IsNullOrEmpty(txtRequestedDate.Text.Trim()))
            //{
            //    txtRequestedDate.BorderColor = Color.Red;
            //    return "Please enter the Requested Date.";
            //}
            //if (ddlRequestedHH.SelectedValue == "-1")
            //{
            //    ddlRequestedHH.BorderColor = Color.Red;
            //    return "Please enter the Requested Hour."; 
            //}
            //if (ddlRequestedMM.SelectedValue == "0")
            //{
            //    ddlRequestedMM.BorderColor = Color.Red;
            //    return "Please enter the Requested Minute."; 
            //}

            if (ddlServicePriority.SelectedValue == "0")
            {
                ddlServicePriority.BorderColor = Color.Red;
                return "Please enter the Service Priority.";
            }
            if (ddlState.SelectedValue == "0")
            {
                ddlState.BorderColor = Color.Red;
                return "Please enter the State.";
            }

            if (ddlDistrict.SelectedValue == "0")
            {
                ddlDistrict.BorderColor = Color.Red;
                return "Please enter the District.";
            }
            return "";
        }

        void Clear()
        {
            txtContactNumber.Text = "";
            txtContactPerson.Text = "";
            txtComplaintDescription.Text = "";
            txtLocation.Text = "";
            ddlServicePriority.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlDistrict.Items.Clear();
            ddlDistrict.DataSource = null;
            ddlDistrict.DataBind();
            gvEquipment.DataSource = null;
            gvEquipment.DataBind();
        }

        protected void rbCheck_CheckedChanged(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            Label lblEquipmentHeaderID = (Label)gvEquipment.Rows[index].FindControl("lblEquipmentHeaderID");
            long  ID = Convert.ToInt64(lblEquipmentHeaderID.Text);
            EquipmentHeaderID = ID;
            gvICTickets.DataSource = new BDMS_ICTicket().GetICTicketStatusReportForIC(EquipmentHeaderID, null, null, null, null, null, null, null);
            gvICTickets.DataBind();

            gvICTickets.Visible = true;
            gvEquipment.Visible = false;

        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = new BDMS_ICTicket().GetICTicketStatusReportForIC(EquipmentHeaderID, null, null, null, null, null, null, null); ;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
        }
    }
}