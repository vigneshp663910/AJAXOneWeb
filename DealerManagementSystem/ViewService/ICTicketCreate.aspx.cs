using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Create');</script>");
            if (!IsPostBack)
            {
                FillMaster();

            }
        }
        public void FillMaster()
        { 
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            PDealer Dealer = PSession.User.Dealer[0];
            int CountryID = Dealer.Country.CountryID;
            ddlCountry.SelectedValue = Convert.ToString(CountryID);
            new DDLBind(ddlState, new BDMS_Address().GetState(CountryID, null, null, null), "State", "StateID");
            FillGetServicePriority();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PDMS_State> State = new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            new DDLBind(ddlState, State, "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            gvEquipment.DataSource = new BDMS_Equipment().GetEquipmentForCreateICTicket(null, txtEquipment.Text.Trim(), txtCustomer.Text.Trim());
            gvEquipment.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
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

            PDMS_ICTicket IC = new PDMS_ICTicket();
            IC.RequestedDate = Convert.ToDateTime(txtRequestedDate.Text.Trim());
            IC.ContactPerson = txtContactPerson.Text;
            IC.PresentContactNumber = txtContactNumber.Text;
            IC.ComplaintDescription = txtComplaintDescription.Text;
            IC.Information = txtInformation.Text;
            IC.ServicePriority = new PDMS_ServicePriority() { ServicePriorityID = Convert.ToInt32(ddlServicePriority.SelectedValue) };

            IC.Equipment = new PDMS_EquipmentHeader() { EquipmentHeaderID = Convert.ToInt64(lblEquipmentHeaderID.Text) };
            IC.Customer = new PDMS_Customer() { CustomerID = Convert.ToInt64(lblCustomerID.Text) };
            IC.Address = new PDMS_Address()
            {
                Country = new PDMS_Country() { CountryID = Convert.ToInt32(ddlCountry.SelectedValue) },
                State = new PDMS_State() { StateID = Convert.ToInt32(ddlState.SelectedValue) },
                District = new PDMS_District() { DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue) }
            };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket", IC));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }

        }
        private void FillGetServicePriority()
        {
            ddlServicePriority.DataTextField = "ServicePriority";
            ddlServicePriority.DataValueField = "ServicePriorityID";
            ddlServicePriority.DataSource = new BDMS_Service().GetServicePriority(null, null);
            ddlServicePriority.DataBind();
            ddlServicePriority.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}