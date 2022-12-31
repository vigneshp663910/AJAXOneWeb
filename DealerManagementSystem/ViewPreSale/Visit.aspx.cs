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
    public partial class Visit : System.Web.UI.Page
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Customer Visit');</script>");

            lblMessage.Text = "";
            lblMessageColdVisit.Text = "";

            if (!IsPostBack)
            { 
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");
                ddlSCountry.SelectedValue = "1";
                List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, null);
                new DDLBind(ddlState, State, "State", "StateID");
                new DDLBind(ddlSActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
            }
        }

        protected void UserControl_ButtonClick(object sender, EventArgs e)
        {
            //handle the event 
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillClodVisit();
        }

        public List<PColdVisit> VisitList
        {
            get
            {
                if (Session["ColdVisits"] == null)
                {
                    Session["ColdVisits"] = new List<PColdVisit>();
                }
                return (List<PColdVisit>)Session["ColdVisits"];
            }
            set
            {
                Session["ColdVisits"] = value;
            }
        }

        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageIndex > 0)
            {
                gvLead.PageIndex = gvLead.PageIndex - 1;
                LeadBind(gvLead, lblRowCount);
            }
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageCount > gvLead.PageIndex)
            {
                gvLead.PageIndex = gvLead.PageIndex + 1;
                LeadBind(gvLead, lblRowCount);
            }
        }
        void LeadBind(GridView gv, Label lbl)
        {
            gv.DataSource = VisitList;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VisitList.Count;
        }

        void FillClodVisit()
        {
            DateTime? ColdVisitDateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? ColdVisitDateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

            long? CustomerID = null;
            string CustomerCode = null;
            string CustomerName = txtCustomer.Text.Trim();
            string Mobile = txtMobile.Text.Trim();

            int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);

            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            int? ActionTypeID = ddlSActionType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSActionType.SelectedValue);

            VisitList = new BColdVisit().GetColdVisit(null, null, ColdVisitDateFrom, ColdVisitDateTo, CustomerID, CustomerCode, CustomerName, Mobile, CountryID, StateID, null, null, DealerID, SalesEngineerID, ActionTypeID);

            gvLead.DataSource = VisitList;
            gvLead.DataBind();

            if (VisitList.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                lblRowCount.Text = (((gvLead.PageIndex) * gvLead.PageSize) + 1) + " - " + (((gvLead.PageIndex) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + VisitList.Count;
            }

        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        } 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
            {
                lblMessage.Text = "Please Enable GeoLocation!";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }
            decimal Latitude = Convert.ToDecimal(hfLatitude.Value);
            decimal Longitude = Convert.ToDecimal(hfLongitude.Value);
            PColdVisit_Insert ColdVisitList = new PColdVisit_Insert();
            ColdVisitList.Latitude = Latitude;
            ColdVisitList.Longitude = Longitude;
            lblMessageColdVisit.ForeColor = Color.Red;
            lblMessageColdVisit.Visible = true;
            string Message = "";

            Message = UC_Customer.ValidationCustomer();

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageColdVisit.Text = Message;
                return;
            }
            ColdVisitList.Customer = UC_Customer.ReadCustomer();


            Message = ValidationColdVisit();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageColdVisit.Text = Message;
                return;
            }

            ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtColdVisitDate.Text.Trim());
            ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
            ColdVisitList.Importance = new PImportance() { ImportanceID = Convert.ToInt32(ddlImportance.SelectedValue) };
            ColdVisitList.Remark = txtRemark.Text.Trim();
            ColdVisitList.Location = txtLocation.Text.Trim();

            string result = new BAPI().ApiPut("ColdVisit", ColdVisitList);

            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            if (result == "0")
            {
                MPE_Customer.Show();
                lblMessageColdVisit.Text = "Customer is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Customer is updated successfully ";
            }
            List<PColdVisit> Leads = new BColdVisit().GetColdVisit(Convert.ToInt64(result), null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            gvLead.DataSource = Leads;
            gvLead.DataBind();
            UC_Customer.FillClean();
            MPE_Customer.Hide();
        }
        public string ValidationColdVisit()
        {
            string Message = "";
            txtColdVisitDate.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver;
            ddlActionType.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
            {
                Message = "Please enter the Cold Visit Date";
                txtColdVisitDate.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = Message + "Please enter the Location";
                txtLocation.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = Message + "Please enter the Remark";
                txtRemark.BorderColor = Color.Red;
            }

            else if (ddlActionType.SelectedValue == "0")
            {
                Message = Message + "Please select the Action Type";
                ddlActionType.BorderColor = Color.Red;
            }
            return Message;
        }



        protected void lbViewCustomer_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = true;
            divColdVisitView.Visible = false;
            btnBackToList.Visible = true;
            divList.Visible = false;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
            Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");
            UC_CustomerView.fillCustomer(Convert.ToInt64(lblCustomerID.Text));
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = false;
            divColdVisitView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }

        protected void btnAddColdVisit_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            UC_Customer.FillMaster();
            new DDLBind(ddlActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
            new DDLBind(ddlImportance, new BDMS_Master().GetImportance(null, null), "Importance", "ImportanceID");
        } 
        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLead.PageIndex = e.NewPageIndex;
            FillClodVisit();
        }

        protected void btnViewColdVisit_Click(object sender, EventArgs e)
        {
            divCustomerView.Visible = false;
            divColdVisitView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblColdVisitID = (Label)gvRow.FindControl("lblColdVisitID");
            UC_ColdVisitsView.fillViewColdVisit(Convert.ToInt64(lblColdVisitID.Text));


            if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
            {
                lblMessage.Text = "Please Enable GeoLocation...!";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }
            Session["Latitude"] = hfLatitude.Value;
            Session["Longitude"] = hfLongitude.Value;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? ColdVisitDateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
                DateTime? ColdVisitDateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

                long? CustomerID = null;
                string CustomerCode = null;
                string CustomerName = txtCustomer.Text.Trim();
                string Mobile = txtMobile.Text.Trim();

                int? CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
                int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);

                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                int? SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                int? ActionTypeID = ddlSActionType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSActionType.SelectedValue);
                //List<PColdVisit> Leads = new BColdVisit().GetColdVisit(null, ColdVisitDateFrom, ColdVisitDateTo, CustomerID, CustomerCode, CustomerName, Mobile, CountryID, StateID, null,null);

                DataTable dt = new BColdVisit().GetColdVisitExcelDownload(null, ColdVisitDateFrom, ColdVisitDateTo, CustomerID, CustomerCode, CustomerName, Mobile, CountryID, StateID, null, null, DealerID, SalesEngineerID, ActionTypeID);

                new BXcel().ExporttoExcel(dt, "Visit Detail Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void BudgetPlanningYearExportExcel(List<PColdVisit> coldVisits, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Cold Visit No");
            dt.Columns.Add("Cold Visit Date");
            dt.Columns.Add("Action Type");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Contact Person");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Email");
            dt.Columns.Add("Status");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Created On");
            dt.Columns.Add("Modified By");
            dt.Columns.Add("Modified On");
            foreach (PColdVisit coldVisit in coldVisits)
            {
                dt.Rows.Add(
                    "'" + coldVisit.ColdVisitNumber
                    , coldVisit.ColdVisitDate
                    , coldVisit.ActionType.ActionType
                    , coldVisit.Customer.CustomerName
                    , coldVisit.Customer.ContactPerson
                    , coldVisit.Customer.Mobile
                    , coldVisit.Customer.Email
                    , coldVisit.Status.Status
                    , (coldVisit.CreatedBy == null) ? "" : coldVisit.CreatedBy.ContactName
                    //, coldVisit.CreatedOn
                    //, (coldVisit.ModifiedBy == null) ? "" : coldVisit.ModifiedBy.ContactName
                    //, coldVisit.ModifiedOn
                    , ""
                    , ""
                    , ""
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

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }

        protected void btnExportSAP_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sl No.");
            dt.Columns.Add("Description");
            dt.Columns.Add("Location");
            dt.Columns.Add("Planned date");
            dt.Columns.Add("time");
            dt.Columns.Add("customer code");
            dt.Columns.Add("reference number");
            dt.Columns.Add("Employee");
            dt.Columns.Add("Sumary description");

            int i = 0;
            foreach (PColdVisit coldVisit in VisitList)
            {
                i = i + 1;
                dt.Rows.Add(i, coldVisit.ActionType.ActionType, coldVisit.Location, coldVisit.ColdVisitDate.ToShortDateString(), coldVisit.ColdVisitDate.ToShortTimeString()
                    , coldVisit.Customer, coldVisit.ColdVisitNumber
                    , coldVisit.Customer.ContactPerson
                    , coldVisit.Customer.Mobile
                    , coldVisit.Customer.Email
                    , coldVisit.Status.Status
                    , (coldVisit.CreatedBy == null) ? "" : coldVisit.CreatedBy.ContactName
                    //, coldVisit.CreatedOn
                    //, (coldVisit.ModifiedBy == null) ? "" : coldVisit.ModifiedBy.ContactName
                    //, coldVisit.ModifiedOn
                    , ""
                    , ""
                    , ""
                    );
            } 
        }

        protected void btnTrackActivity_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Button btnEndActivity = (Button)gvRow.FindControl("btnTrackActivity");
            MPE_TrackActivity.Show();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            Label lblLatitude = (Label)gvRow.FindControl("lblLatitude");
            Label lblLongitude = (Label)gvRow.FindControl("lblLongitude");

            row = new Dictionary<string, object>();
            row.Add("lat", lblLatitude.Text);
            row.Add("lng", lblLongitude.Text); 
            rows.Add(row);
            CurrentLocation = serializer.Serialize(rows);
        }
        public string CurrentLocation
        {
            get
            {
                if (Session["ActivityReport"] == null)
                {
                    Session["ActivityReport"] = "";
                }
                return (string)Session["ActivityReport"];
            }
            set
            {
                Session["ActivityReport"] = value;
            }
        }
        public string ConvertDataTabletoString()
        {
            return CurrentLocation;
        }
        [WebMethod]
        public static string GetCustomer1(string Cust)
        {
            List<string> Emp = new List<string>();
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(Cust, 1);
            return JsonConvert.SerializeObject(Customer);
        }
    }
}