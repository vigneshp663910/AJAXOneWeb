using Business;
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

namespace DealerManagementSystem.ViewActivity
{
    public partial class Activity : System.Web.UI.Page
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Activity » Manage');</script>");

            if (!IsPostBack)
            {
                List<PActivityType> ActivityType = new BActivity().GetActivityType(null, null, null, null);
                new DDLBind(ddlActivityType, ActivityType, "ActivityTypeName", "ActivityTypeID");

                List<PActivityReferenceType> ActivityReferenceType = new BActivity().GetActivityReferenceType(null, null);
                new DDLBind(ddlReferenceType, ActivityReferenceType, "ReferenceTable", "ActivityReferenceTableID");

                txtActivityDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtActivityDateFrom.TextMode = TextBoxMode.Date;

                txtActivityDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtActivityDateTo.TextMode = TextBoxMode.Date;
            }
        }

        //public string ValidationActivity()
        //{
        //    string Message = "";
        //    ddlActivityType.BorderColor = Color.Silver;
        //    txtActivityID.BorderColor = Color.Silver;
        //    txtActivityDateFrom.BorderColor = Color.Silver;
        //    txtActivityDateTo.BorderColor = Color.Silver;
        //    txtCustomerCode.BorderColor = Color.Silver;
        //    txtEquipment.BorderColor = Color.Silver;
        //    ddlReferenceType.BorderColor = Color.Silver;
        //    txtReferenceNumber.BorderColor = Color.Silver;

        //    if (string.IsNullOrEmpty(txtLeadDate.Text.Trim()))
        //    {
        //        Message = "Please enter the Lead Date";
        //        txtLeadDate.BorderColor = Color.Red;
        //    }
        //    else if (ddlProductType.SelectedValue == "0")
        //    {
        //        Message = Message + "<br/>Please select the Product Type";
        //        ddlProductType.BorderColor = Color.Red;
        //    }


        //    else if (ddlCategory.SelectedValue == "0")
        //    {
        //        Message = Message + "<br/>Please select the Category";
        //        ddlCategory.BorderColor = Color.Red;
        //    }
        //    else if (ddlQualification.SelectedValue == "0")
        //    {
        //        Message = Message + "<br/>Please select the Qualification";
        //        ddlQualification.BorderColor = Color.Red;
        //    }
        //    else if (ddlSource.SelectedValue == "0")
        //    {
        //        Message = Message + "<br/>Please select the Source";
        //        ddlSource.BorderColor = Color.Red;
        //    }
        //    else if (ddlLeadType.SelectedValue == "0")
        //    {
        //        Message = Message + "<br/>Please select the LeadType";
        //        ddlLeadType.BorderColor = Color.Red;
        //    }
        //    else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please enter the Remark";
        //        txtRemarks.BorderColor = Color.Red;
        //    }
        //    return Message;
        //}
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillActivity();
        }
        public List<PActivity> Activity1
        {
            get
            {
                if (Session["Activity1"] == null)
                {
                    Session["Activity1"] = new List<PLead>();
                }
                return (List<PActivity>)Session["Activity1"];
            }
            set
            {
                Session["Activity1"] = value;
            }
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

        protected void ibtnActivityArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvActivity.PageIndex > 0)
            {
                gvActivity.PageIndex = gvActivity.PageIndex - 1;
                ActivityBind(gvActivity, lblRowCountActivity, Activity1);
            }
        }
        protected void ibtnActivityArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvActivity.PageCount > gvActivity.PageIndex)
            {
                gvActivity.PageIndex = gvActivity.PageIndex + 1;
                ActivityBind(gvActivity, lblRowCountActivity, Activity1);
            }
        }
        void ActivityBind(GridView gv, Label lbl, List<PActivity> Activity1)
        {
            gv.DataSource = Activity1;
            gv.DataBind();
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                Label lblEndDate = (Label)gv.Rows[i].FindControl("lblEndDate");
                Button btnEndActivity = (Button)gv.Rows[i].FindControl("btnEndActivity");
                if (!string.IsNullOrEmpty(lblEndDate.Text))
                {

                    //btnEndActivity.Visible = false;
                    btnEndActivity.Text = "Track Activity";
                }
            }
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Activity1.Count;
        }       
        void FillActivity()
        {
            PActivitySearch S = new PActivitySearch();

            S.ActivityTypeID = ddlActivityType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlActivityType.SelectedValue);
            S.ActivityID = string.IsNullOrEmpty(txtActivityID.Text.Trim()) ? (Int64?)null : Convert.ToInt64(txtActivityID.Text.Trim());
            S.ActivityDateFrom = string.IsNullOrEmpty(txtActivityDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtActivityDateFrom.Text.Trim());
            S.ActivityDateTo = string.IsNullOrEmpty(txtActivityDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtActivityDateTo.Text.Trim());
            //S.CustomerCode = txtCustomerCode.Text.Trim();
            //S.EquipmentSerialNo = txtEquipment.Text.Trim();
            S.ActivityReferenceTableID = ddlReferenceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlReferenceType.SelectedValue);
            S.ReferenceNumber = txtReferenceNumber.Text.Trim();
            Activity1 = new BActivity().GetActivity(S, PSession.User.UserID);

            gvActivity.DataSource = Activity1;
            gvActivity.DataBind();

            for (int i = 0; i < gvActivity.Rows.Count; i++)
            {
                Label lblEndDate = (Label)gvActivity.Rows[i].FindControl("lblEndDate");
                Button btnEndActivity = (Button)gvActivity.Rows[i].FindControl("btnEndActivity");
                if (!string.IsNullOrEmpty(lblEndDate.Text))
                {
                    //btnEndActivity.Visible = false;
                    btnEndActivity.Text = "Track Activity";
                }
            }
            if (Activity1.Count == 0)
            {
                lblRowCountActivity.Visible = false;
                ibtnActivityArrowLeft.Visible = false;
                ibtnActivityArrowRight.Visible = false;
            }
            else
            {
                lblRowCountActivity.Visible = true;
                ibtnActivityArrowLeft.Visible = true;
                ibtnActivityArrowRight.Visible = true;
                lblRowCountActivity.Text = (((gvActivity.PageIndex) * gvActivity.PageSize) + 1) + " - " + (((gvActivity.PageIndex) * gvActivity.PageSize) + gvActivity.Rows.Count) + " of " + Activity1.Count;
            }
        }
        protected void btnAddActivity_Click(object sender, EventArgs e)
        {
            List<PActivity> PendingUserActivity = new BActivity().GetPendingUserActivitiy(PSession.User.UserID);
            if (PendingUserActivity.Count > 0)
            {
                lblActivityTypeE.Text = PendingUserActivity[0].ActivityType.ActivityTypeName;
                lblActivityTypeIDE.Text = PendingUserActivity[0].ActivityType.ActivityTypeID.ToString();
                //ViewState["ActivityID"] = PendingUserActivity[0].ActivityID;
                lblActivityIDE.Text = PendingUserActivity[0].ActivityID.ToString();
                lblEndActivityDate.Text = DateTime.Now.ToString();
                List<PActivityReferenceType> ActivityReferenceType = new BActivity().GetActivityReferenceType(null, null);
                new DDLBind(ddlReferenceTypeE, ActivityReferenceType, "ReferenceTable", "ActivityReferenceTableID");
                lblEndActivityMessage.Text = "Activity is Pending. Please close this Activity to add a new Activity.";
                lblEndActivityMessage.ForeColor = Color.Red;
                lblEndActivityMessage.Visible = true;
                MPE_EndActivity.Show();
            }
            else
            {
                List<PActivityType> ActivityTypeS = new BActivity().GetActivityType(null, null, null, null);
                new DDLBind(ddlActivityTypeS, ActivityTypeS, "ActivityTypeName", "ActivityTypeID");
                lblStartActivityDate.Text = DateTime.Now.ToString();
                MPE_AddActivity.Show();
            }
        }
        [WebMethod]
        public static List<string> GetCustomer(string CustS)
        {
            List<string> Emp = new List<string>();
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS, 1);
            int i = 0;
            foreach (PDMS_Customer cust in Customer)
            {
                i = i + 1;
                string div = "<label id='lblCustomerID" + i + "' style='display: none'>" + cust.CustomerID + "</label>"
                    + "<table><tr><td>"
                    + "<label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label></td><td>Prospect</td></tr >" + "<tr><td>"
                    + "<label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label></td><td>"
                    + "<label id='lblMobile" + i + "'>" + cust.Mobile + " </td></tr></ table >";
                Emp.Add(div);
            }
            return Emp;
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            //divList.Visible = true;
            //divDetailsView.Visible = false;
        }
        protected void gvActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActivity.PageIndex = e.NewPageIndex;
            FillActivity();
        }
        protected void btnStartActivity_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_AddActivity.Show();
                if (ddlActivityTypeS.SelectedValue == "0")
                {
                    lblAddActivityMessage.Text = "Please select the Activity Type...!";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    lblAddActivityMessage.Visible = true;
                    ddlActivityTypeS.BorderColor = Color.Red;
                    return;
                }
                if(string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
                {
                    lblAddActivityMessage.Text = "Please Enable GeoLocation...!";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    lblAddActivityMessage.Visible = true;
                    ddlActivityTypeS.BorderColor = Color.Red;
                    return;
                }
                PActivity Activity = new PActivity();
                lblAddActivityMessage.ForeColor = Color.Red;
                lblAddActivityMessage.Visible = true;
                Activity.ActivityStartLatitude = Convert.ToDecimal(hfLatitude.Value);
                Activity.ActivityStartLongitude = Convert.ToDecimal(hfLongitude.Value);
                Activity.ActivityType = new PActivityType();
                Activity.ActivityType.ActivityTypeID = Convert.ToInt32(ddlActivityTypeS.SelectedValue);
                Activity.ActivityStartDate = Convert.ToDateTime(lblStartActivityDate.Text.Trim());
                Activity.SalesEngineer = new PUser { UserID = PSession.User.UserID };

                Int64 result = new BActivity().InsertOrUpdateActivity(Activity);
                if (result == 0)
                {
                    lblAddActivityMessage.Text = "Activity not added successfully.";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    return;
                }
                lblActivityMessage.Text = "Activity added successfully.";
                lblActivityMessage.ForeColor = Color.Green;
                lblActivityMessage.Visible = true;

                FillActivity();
                MPE_AddActivity.Hide();
            }
            catch (Exception ex)
            {
                lblAddActivityMessage.Text = ex.Message.ToString();
                lblAddActivityMessage.ForeColor = Color.Red;
                lblAddActivityMessage.Visible = true;
            }
        }
        public string ConvertDataTabletoString()
        {
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            //Dictionary<string, object> row;

            //row = new Dictionary<string, object>();
            //row.Add("title", "1");
            //row.Add("lat", "12.897400");
            //row.Add("lng", "80.288000");
            //row.Add("description", "1");
            //rows.Add(row);

            //row = new Dictionary<string, object>();
            //row.Add("title", "2");
            //row.Add("lat", "12.997450");
            //row.Add("lng", "80.298050");
            //row.Add("description", "2");

            //rows.Add(row);

            //return serializer.Serialize(rows);

            return CurrentLocation;

        }
        protected void btnEndActivity_Click(object sender, EventArgs e)
        {
            //List<PActivity> PendingUserActivity = new BActivity().GetPendingUserActivitiy(PSession.User.UserID);
            //if (PendingUserActivity.Count == 0)
            //{
            //    lblActivityMessage.Text = "Activity already ended.";
            //    lblActivityMessage.ForeColor = Color.Red;
            //    lblActivityMessage.Visible = true;
            //    return;
            //}

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Button btnEndActivity = (Button)gvRow.FindControl("btnEndActivity");

            if (btnEndActivity.Text == "End Activity")
            {
                lblEndActivityMessage.Text = string.Empty;
                lblEndActivityMessage.Visible = false;
                MPE_EndActivity.Show();
                Label lblActivityType = (Label)gvRow.FindControl("lblActivityType");
                Label lblActivityTypeID = (Label)gvRow.FindControl("lblActivityTypeID");
                Label lblActivityID = (Label)gvRow.FindControl("lblActivityID");
                lblActivityTypeE.Text = lblActivityType.Text;
                lblActivityTypeIDE.Text = lblActivityTypeID.Text;
                lblEndActivityDate.Text = DateTime.Now.ToString();
                List<PActivityReferenceType> ActivityReferenceType = new BActivity().GetActivityReferenceType(null, null);
                new DDLBind(ddlReferenceTypeE, ActivityReferenceType, "ReferenceTable", "ActivityReferenceTableID");
                //ViewState["ActivityID"] = lblActivityID.Text;
                lblActivityIDE.Text = lblActivityID.Text;
                PActivity Activity = new PActivity();
                lblEndActivityMessage.ForeColor = Color.Red;
                lblEndActivityMessage.Visible = true;
            }

            else if(btnEndActivity.Text == "Track Activity")
            {
                MPE_TrackActivity.Show(); 

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;

                Label lblActivityID = (Label)gvRow.FindControl("lblActivityID");
                //DataTable dt = new BActivity().GetActivityLocation(Convert.ToInt32(lblActivityID.Text));


                //foreach (DataRow dr in dt.Rows)
                //{
                //    row = new Dictionary<string, object>(); 
                //    row.Add("lat", Convert.ToString(dr["StartLatitude"]));
                //    row.Add("lng", Convert.ToString(dr["StartLongitude"])); 
                //    row.Add("image", Convert.ToString(dr["MapImage"]));
                //    rows.Add(row); 
                //    row = new Dictionary<string, object>();
                //    row.Add("lat", Convert.ToString(dr["EndLatitude"]));
                //    row.Add("lng", Convert.ToString(dr["EndLongitude"]));  
                //    row.Add("image", Convert.ToString(dr["MapImage"])); 
                //    rows.Add(row);
                //}

                PActivitySearch S = new PActivitySearch();

                S.ActivityID = Convert.ToInt64(lblActivityID.Text);
                Activity1 = new BActivity().GetActivity(S, PSession.User.UserID);

                foreach (var Activity in Activity1)
                {
                    row = new Dictionary<string, object>();
                    row.Add("lat", Activity.ActivityStartLatitude);
                    row.Add("lng", Activity.ActivityStartLongitude);
                    row.Add("description", Activity.StartLatitudeLongitudeDate);
                    row.Add("image", Activity.StartMapImage);
                    rows.Add(row);

                    row = new Dictionary<string, object>();
                    row.Add("lat", Activity.ActivityEndLatitude);
                    row.Add("lng", Activity.ActivityEndLongitude);
                    row.Add("description", Activity.EndLatitudeLongitudeDate);
                    row.Add("image", Activity.EndMapImage);
                    rows.Add(row);
                }

                CurrentLocation = serializer.Serialize(rows);
            }
        }
        protected void btnEndActivityE_Click(object sender, EventArgs e)
        {
            try
            {

                MPE_EndActivity.Show();
                PActivity Activity = new PActivity();
                lblEndActivityMessage.ForeColor = Color.Red;
                lblEndActivityMessage.Visible = true;

                string Message = ValidationEndAvtivity();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblEndActivityMessage.Text = Message;
                    return;
                }

                //long? CustomerID = null;
                //if (!string.IsNullOrEmpty(txtCustomerCodeE.Text.Trim()))
                //{
                //    List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, txtCustomerCodeE.Text.Trim());
                //    if (Customer.Count == 0)
                //    {
                //        lblEndActivityMessage.Text = "Customer Code not avialable.";
                //        //txtCustomerCodeE.BorderColor = Color.Red;
                //        return;
                //    }
                //    else
                //    {
                //        CustomerID = Customer[0].CustomerID;
                //    }
                //}
                //long? EquipmentHeaderID = null;
                //if (!string.IsNullOrEmpty(txtEquipmentE.Text.Trim()))
                //{
                //    List<PDMS_EquipmentHeader> Equ = new BDMS_Equipment().GetEquipment(null, txtEquipmentE.Text.Trim());
                //    if (Equ.Count == 0)
                //    {
                //        lblEndActivityMessage.Text = "Equipment not avialable.";
                //        //txtEquipmentE.BorderColor = Color.Red;
                //        return;
                //    }
                //    else
                //    {
                //        EquipmentHeaderID = Equ[0].EquipmentHeaderID;
                //    }
                //}

                long? ReferenceNumberID = null;
                long? CustomerID = null;
                if (ddlReferenceTypeE.SelectedValue == "1")
                {
                    List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, txtReferenceNumberE.Text.Trim());
                    if (Customer.Count == 0)
                    {
                        lblEndActivityMessage.Text = "Customer Code is not avialable.";
                        //txtCustomerCodeE.BorderColor = Color.Red;
                        return;
                    }
                    //else
                    //{
                    CustomerID = Customer[0].CustomerID;
                    ReferenceNumberID = Customer[0].CustomerID;
                    //}
                }

                long? LeadID = null;
                if (ddlReferenceTypeE.SelectedValue == "2")
                {
                    //PLead Lead = new BLead().GetLeadByLeadNumber(txtReferenceNumberE.Text.Trim());
                    PLeadSearch S = new PLeadSearch();
                    S.LeadNumber = txtReferenceNumberE.Text.Trim();
                    //S.CustomerID = CustomerID;
                    List<PLead> Lead = new BLead().GetLead(S);
                    if (Lead.Count == 0)
                    {
                        lblEndActivityMessage.Text = "Lead Number is not avialable.";
                        //txtReferenceNumberE.BorderColor = Color.Red;
                        return;
                    }
                    LeadID = Lead[0].LeadID;
                    ReferenceNumberID = Lead[0].LeadID;
                }

                long? QuotationID = null;
                if (ddlReferenceTypeE.SelectedValue == "3")
                {
                    List<PSalesQuotation> Quotation = new BSalesQuotation().GetSalesQuotationBasic(null, null, null, null, txtReferenceNumberE.Text.Trim(), null, null, null, null, null, null);
                    if (Quotation.Count == 0)
                    {
                        lblEndActivityMessage.Text = "Quotation Number is not avialable.";
                        //txtReferenceNumberE.BorderColor = Color.Red;
                        return;
                    }
                    QuotationID = Quotation[0].QuotationID;
                    ReferenceNumberID = Quotation[0].QuotationID;
                }

                long? EquipmentHeaderID = null;
                if (ddlReferenceTypeE.SelectedValue == "4")
                {
                    List<PDMS_EquipmentHeader> Equ = new BDMS_Equipment().GetEquipment(null, txtReferenceNumberE.Text.Trim());
                    if (Equ.Count == 0)
                    {
                        lblEndActivityMessage.Text = "Equipment not avialable.";
                        //txtEquipmentE.BorderColor = Color.Red;
                        return;
                    }
                    else
                    {
                        EquipmentHeaderID = Equ[0].EquipmentHeaderID;
                        ReferenceNumberID = Equ[0].EquipmentHeaderID;
                    }
                }

                Activity.ActivityID = Convert.ToInt32(lblActivityIDE.Text);
                Activity.ActivityEndLatitude = Convert.ToDecimal(hfLatitude.Value);
                Activity.ActivityEndLongitude = Convert.ToDecimal(hfLongitude.Value);
                Activity.ActivityType = new PActivityType();
                Activity.ActivityType.ActivityTypeID = Convert.ToInt32(lblActivityTypeIDE.Text);
                Activity.ActivityEndDate = Convert.ToDateTime(lblEndActivityDate.Text.Trim());
                Activity.SalesEngineer = new PUser { UserID = PSession.User.UserID };
                Activity.Location = Convert.ToString(txtLocation.Text.Trim());
                //Activity.Customer = new PDMS_Customer();
                //Activity.Customer.CustomerCode = Convert.ToString(txtCustomerCodeE.Text.Trim());
                //Activity.Customer.CustomerID = Convert.ToInt64(CustomerID);
                //Activity.Equipment = new PDMS_EquipmentHeader();
                //Activity.Equipment.EquipmentSerialNo = Convert.ToString(txtEquipmentE.Text.Trim());
                //Activity.Equipment.EquipmentHeaderID = Convert.ToInt64(EquipmentHeaderID);
                Activity.Amount = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtAmount.Text.Trim());

                Activity.ActivityReference = new PActivityReferenceType();
                Activity.ActivityReference = ddlReferenceTypeE.SelectedValue == "0" ? null : new PActivityReferenceType() { ActivityReferenceTableID = Convert.ToInt32(ddlReferenceTypeE.SelectedValue) };

                Activity.ReferenceNumber = Convert.ToString(txtReferenceNumberE.Text.Trim());
                //Activity.ReferenceID = Convert.ToInt64(LeadID);

                Activity.Remark = Convert.ToString(txtRemarks.Text.Trim());
                Activity.ReferenceNumberID = ReferenceNumberID == null ? (Int64?)null : ReferenceNumberID;

                Int64 result = new BActivity().InsertOrUpdateActivity(Activity);
                if (result == 0)
                {
                    lblEndActivityMessage.Text = "Activity not ended successfully.";
                    lblEndActivityMessage.ForeColor = Color.Red;
                    return;
                }
                lblActivityMessage.Text = "Activity ended successfully.";
                lblActivityMessage.ForeColor = Color.Green;

                FillActivity();
                MPE_EndActivity.Hide();
            }
            catch (Exception ex)
            {
                lblEndActivityMessage.Text = ex.Message.ToString();
                lblEndActivityMessage.ForeColor = Color.Red;
                lblEndActivityMessage.Visible = true;
            }
        }

        public string ValidationEndAvtivity()
        {
            string Message = "";
            txtLocation.BorderColor = Color.Silver;
            //txtCustomerCodeE.BorderColor = Color.Silver;
            //txtEquipmentE.BorderColor = Color.Silver;
            ddlReferenceTypeE.BorderColor = Color.Silver;
            txtReferenceNumberE.BorderColor = Color.Silver;
            txtRemarks.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = "Please enter the Location.";
                txtLocation.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
            {
                Message = "Please Enable GeoLocation...!";
            }
            //if(string.IsNullOrEmpty(txtCustomerCodeE.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Customer Code.";
            //    txtCustomerCodeE.BorderColor = Color.Red;
            //}

            //if(string.IsNullOrEmpty(txtEquipmentE.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Equipment Serial Number.";
            //    txtEquipmentE.BorderColor = Color.Red;
            //}
            //if (string.IsNullOrEmpty(txtAmount.Text.Trim())|| txtAmount.Text.Trim() == "0")
            //{
            //    Message = Message + "<br/>Please enter the Amount.";
            //    txtAmount.BorderColor = Color.Red;
            //}

            //if (ddlReferenceTypeE.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Reference Type";
            //    ddlReferenceTypeE.BorderColor = Color.Red;
            //}
            //if (string.IsNullOrEmpty(txtReferenceNumberE.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please select the Reference Number";
            //    txtReferenceNumberE.BorderColor = Color.Red;
            //}
            //if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Remark";
            //    txtRemarks.BorderColor = Color.Red;
            //}
            if (ddlReferenceTypeE.SelectedValue != "0" && string.IsNullOrEmpty(txtReferenceNumberE.Text.Trim()))
            {

                Message = Message + "<br/>Please enter the Reference Number";
                txtReferenceNumberE.BorderColor = Color.Red;
            }
            return Message;
        }
    }
}