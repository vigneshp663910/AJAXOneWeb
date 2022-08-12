using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class ColdVisitsView : System.Web.UI.UserControl
    {
        public long ColdVisitID
        {
            get
            {
                if (Session["ColdVisitID"] == null)
                {
                    Session["ColdVisitID"] = 0;
                }
                return Convert.ToInt64(Session["ColdVisitID"]);
            }
            set
            {
                Session["ColdVisitID"] = value;
            }
        }
        public PColdVisit ColdVisit
        {
            get
            {
                if (Session["ColdVisitsView"] == null)
                {
                    Session["ColdVisitsView"] = new PColdVisit();
                }
                return (PColdVisit)Session["ColdVisitsView"];
            }
            set
            {
                Session["ColdVisitsView"] = value;
            }
        }
        public List<PActivity> Activity1
        {
            get
            {
                if (Session["Activity1"] == null)
                {
                    Session["Activity1"] = new List<PActivity>();
                }
                return (List<PActivity>)Session["Activity1"];
            }
            set
            {
                Session["Activity1"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageEffort.Text = "";
            lblMessageExpense.Text = "";
            lblMessage.Text = "";
        }
        public void fillViewColdVisit(long ColdVisitID)
        {
            this.ColdVisitID = ColdVisitID;

            ColdVisit = new BColdVisit().GetColdVisit(ColdVisitID, null, null, null, null, null, null, null, null, null, null, null, null, null, null)[0];
            lblLeadNumber.Text = ColdVisit.ColdVisitNumber;
            lblLeadDate.Text = ColdVisit.ColdVisitDate.ToShortDateString();// Convert.ToString(Lead.ColdVisitDate); 
                                                                           //  lblRemarks.Text = ColdVisit.Remarks; 

            lblLocation.Text = ColdVisit.Location;
            lblCustomer.Text = ColdVisit.Customer.CustomerFullName;
            lblContactPerson.Text = ColdVisit.Customer.ContactPerson;
            lblMobile.Text = "<a href='tel:" + ColdVisit.Customer.Mobile + "'>" + ColdVisit.Customer.Mobile + "</a>";
            lblEmail.Text = "<a href='mailto:" + ColdVisit.Customer.Email + "'>" + ColdVisit.Customer.Email + "</a>";

            lblStatus.Text = ColdVisit.Status.Status;
            lblImportance.Text = ColdVisit.Importance.Importance;

            string Location = ColdVisit.Customer.Address1 + ", " + ColdVisit.Customer.Address2 + ", " + ColdVisit.Customer.District.District + ", " + ColdVisit.Customer.State.State;
            lblAddress.Text = Location;
            fillEffort();
            fillExpense();
            FillActivity();


            ActionControlMange();

            
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);

            if (lbActions.Text == "Add Effort")
            {
                List<PUser> User = new List<PUser>();
                User.Add(ColdVisit.CreatedBy);
                DropDownList ddlSalesEngineer = (DropDownList)UC_Effort.FindControl("ddlSalesEngineer");
                DropDownList ddlEffortType = (DropDownList)UC_Effort.FindControl("ddlEffortType");

                new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                new DDLBind(ddlSalesEngineer, User, "ContactName", "UserID", false);

                ddlSalesEngineer.Enabled = false;
                MPE_Effort.Show();
            }
            else if (lbActions.Text == "Add Expense")
            {
                List<PUser> User = new List<PUser>();
                User.Add(ColdVisit.CreatedBy);
                DropDownList ddlSalesEngineer = (DropDownList)UC_Expense.FindControl("ddlSalesEngineer");
                DropDownList ddlExpenseType = (DropDownList)UC_Expense.FindControl("ddlExpenseType");

                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
                new DDLBind(ddlSalesEngineer, User, "ContactName", "UserID", false);

                ddlSalesEngineer.Enabled = false;

                MPE_Expense.Show();
            }
            else if (lbActions.Text == "Status Change to Close")
            {
                string endPoint = "ColdVisit/UpdateColdVisitStatus?ColdVisitID=" + ColdVisitID + "&StatusID=2" + "&UserID=" + PSession.User.UserID;
                string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                if (Convert.ToBoolean(s) == true)
                {
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillViewColdVisit(ColdVisitID);
                }
                else
                {
                    lblMessage.Text = "Something went wrong try again.";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
            else if (lbActions.Text == "Status Change to Cancel")
            {
                string endPoint = "ColdVisit/UpdateColdVisitStatus?ColdVisitID=" + ColdVisitID + "&StatusID=3" + "&UserID=" + PSession.User.UserID;
                string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                if (Convert.ToBoolean(s) == true)
                {
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillViewColdVisit(ColdVisitID);
                }
                else
                {
                    lblMessage.Text = "Something went wrong try again.";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
            else if (lbActions.Text == "Add Activity")
            {
                List<PActivity> PendingVisitActivity = new BActivity().GetPendingVisitActivity(null, PSession.User.UserID, PSession.User.UserID);
                if (PendingVisitActivity.Count > 0)
                {
                    //lblActivityTypeE.Text = PendingUserActivity[0].ActivityType.ActivityTypeName;
                    //lblActivityTypeIDE.Text = PendingUserActivity[0].ActivityType.ActivityTypeID.ToString();
                    ////ViewState["ActivityID"] = PendingUserActivity[0].ActivityID;
                    //lblActivityIDE.Text = PendingUserActivity[0].ActivityID.ToString();
                    //lblEndActivityDate.Text = DateTime.Now.ToString();
                    //List<PActivityReferenceType> ActivityReferenceType = new BActivity().GetActivityReferenceType(null, null);
                    //new DDLBind(ddlReferenceTypeE, ActivityReferenceType, "ReferenceTable", "ActivityReferenceTableID");
                    //new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                    //new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");

                    //ddlReferenceTypeE_SelectedIndexChanged(sender, e);
                    EndActivity(PendingVisitActivity[0].ActivityType.ActivityTypeName, PendingVisitActivity[0].ActivityType.ActivityTypeID.ToString(), PendingVisitActivity[0].ActivityID.ToString(), PendingVisitActivity[0].Remark.ToString());

                    lblEndActivityMessage.Text = "Visit Activity is Pending. Please close this Visit Activity to add a new Visit Activity.";
                    lblEndActivityMessage.ForeColor = Color.Red;
                    lblEndActivityMessage.Visible = true;
                    //txtLocationS.Text = string.Empty;
                    //txtLocationS.BorderColor = Color.Silver;
                    //txtRemarks.Text = string.Empty;
                    //txtRemarks.BorderColor = Color.Silver;
                    lblValidationMessage.Text = string.Empty;
                    lblValidationMessage.Visible = false;
                    MPE_EndActivity.Show();
                }
                else
                {
                    new DDLBind(ddlActivityTypeS, new BActivity().GetActivityType(null, null), "ActivityTypeName", "ActivityTypeID");
                    lblStartActivityDate.Text = DateTime.Now.ToString();
                    lblAddActivityMessage.Text = string.Empty;
                    ddlActivityTypeS.BorderColor = Color.Silver;
                    txtLocation.Text = string.Empty;
                    txtLocation.BorderColor = Color.Silver;
                    txtRemarksS.Text = string.Empty;
                    txtRemarksS.BorderColor = Color.Silver;
                    MPE_AddActivity.Show();
                }
            }
        }

        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {
            MPE_Effort.Show();
            string Message = UC_Effort.ValidationEffort();
            lblMessageEffort.ForeColor = Color.Red;
            lblMessageEffort.Visible = true; 
            if (!string.IsNullOrEmpty(Message))
            { 
                lblMessageEffort.Text = Message;
                return;
            }
            PLeadEffort Effort = new PLeadEffort();
            Effort = UC_Effort.ReadEffort();
            Effort.LeadID = ColdVisitID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Effort", Effort));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            MPE_Effort.Hide();
            tbpCust.ActiveTabIndex = 0;
            fillEffort(); 
        }

        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            MPE_Expense.Show();
            string Message = UC_Expense.ValidationExpense();
            lblMessageExpense.ForeColor = Color.Red;
            lblMessageExpense.Visible = true;
            
            if (!string.IsNullOrEmpty(Message))
            {
              
                lblMessageExpense.Text = Message;
                return;
            }
            PLeadExpense Expense = new PLeadExpense();
            Expense = UC_Expense.ReadExpense();
            Expense.LeadID = ColdVisitID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/Expense", Expense));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            MPE_Expense.Hide();
            tbpCust.ActiveTabIndex =1;
            fillExpense();
        }
        void fillEffort()
        {
            gvEffort.DataSource = new BColdVisit().GetColdVisitEffort(ColdVisitID, PSession.User.UserID);
            gvEffort.DataBind();

        }
        void fillExpense()
        {

            gvExpense.DataSource = new BColdVisit().GetColdVisitExpense(ColdVisitID, PSession.User.UserID);
            gvExpense.DataBind();

        }
        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BColdVisit().GetAttachedFileColdVisit(ColdVisitID);
            gvSupportDocument.DataBind();
        }
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            if (fileUpload.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            HttpPostedFile file = fileUpload.PostedFile;
             

            PAttachedFile F = new PAttachedFile();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);

            F.FileName = name;
            F.AttachedFile = fileData; 
            F.FileType = file.ContentType;
            F.FileSize = size;
            F.AttachedFileID = 0;
            F.ReferenceID = ColdVisitID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/AttachedFile", F));
            lblMessage.Visible = true;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Results.Message; 
            lblMessage.ForeColor = Color.Green; 
            fillSupportDocument(); 
           
        }

        protected void lbSupportDocumentDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                Label lblFileName = (Label)gvRow.FindControl("lblFileName");
                Label lblFileType = (Label)gvRow.FindControl("lblFileType");

                PAttachedFile UploadedFile = new BColdVisit().GetAttachedFileColdVisitForDownload(AttachedFileID + Path.GetExtension(lblFileName.Text));

                Response.AddHeader("Content-type", lblFileType.Text);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblFileName.Text);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }
        }

        protected void lbSupportDocumentDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
            PAttachedFile F = new PAttachedFile();
            F.AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
            F.ReferenceID = ColdVisitID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit/AttachedFile", F));

            lblMessage.Visible = true;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.ForeColor = Color.Green;
            fillSupportDocument();
        }
        void ActionControlMange()
        { 
            lbtnStatusChangeToClose.Visible = true;
            lbtnStatusChangeToCancel.Visible = true;
            if ((ColdVisit.Status.StatusID == 2) || (ColdVisit.Status.StatusID == 3))
            {
                lbtnStatusChangeToClose.Visible = false;
                lbtnStatusChangeToCancel.Visible = false;
            }

            //if (ColdVisit.ReferenceID != null)
            //{
            //    lbtnAddEffort.Visible = false;
            //    lbtnAddExpense.Visible = false;
            //    tbpCust.Visible = false;
            //}
        }

        protected void btnStartActivity_Click(object sender, EventArgs e)
        {
            try
            {
                ddlActivityTypeS.BorderColor = Color.Silver;
                txtLocation.BorderColor = Color.Silver;
                txtRemarksS.BorderColor = Color.Silver;
                MPE_AddActivity.Show();

                if (ddlActivityTypeS.SelectedValue == "0")
                {
                    lblAddActivityMessage.Text = "Please select the Activity Type...!";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    lblAddActivityMessage.Visible = true;
                    ddlActivityTypeS.BorderColor = Color.Red;
                    return;
                }
                if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
                {
                    lblAddActivityMessage.Text = "Please Enable GeoLocation...!";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    lblAddActivityMessage.Visible = true;
                    ddlActivityTypeS.BorderColor = Color.Red;
                    return;
                }
                if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
                {
                    lblAddActivityMessage.Text = "Please enter the Location";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    txtLocation.BorderColor = Color.Red;
                    return;
                }
                PApiResult Results = new BActivity().StartActivityWithVisit(ColdVisit.ColdVisitID, txtLocation.Text.Trim(), txtRemarksS.Text.Trim(), Convert.ToDecimal(Session["Latitude"]), Convert.ToDecimal(Session["Longitude"]), Convert.ToInt32(ddlActivityTypeS.SelectedValue));
                if (Results.Status == PApplication.Failure)
                {
                    lblAddActivityMessage.Text = "Activity not added successfully.";
                    lblAddActivityMessage.ForeColor = Color.Red;
                    return;
                }

                lblMessage.Text = "Activity added successfully.";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;

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
        
        protected void btnEndActivity_Click(object sender, EventArgs e)
        {
           GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
           Button btnEndActivity = (Button)gvRow.FindControl("btnEndActivity");

            if (btnEndActivity.Text == "End Activity")
            {
                Label lblActivityType = (Label)gvRow.FindControl("lblActivityType");
                Label lblActivityTypeID = (Label)gvRow.FindControl("lblActivityTypeID");
                Label lblActivityID = (Label)gvRow.FindControl("lblActivityID");
                Label lblRemarks = (Label)gvRow.FindControl("lblRemarks");

                new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");

                EndActivity(lblActivityType.Text, lblActivityTypeID.Text, lblActivityID.Text, lblRemarks.Text); 
            }

            else if (btnEndActivity.Text == "Track Activity")
            {
                MPE_TrackActivity.Show();
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                
                Label lblActivityID = (Label)gvRow.FindControl("lblActivityID");
                PActivitySearch S = new PActivitySearch();
                S.ActivityID = Convert.ToInt64(lblActivityID.Text);
                Activity1 = new BActivity().GetActivity(Convert.ToInt64(lblActivityID.Text), null, null, null);
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
        void EndActivity(string ActivityType, string ActivityTypeID, string ActivityID, string Remark)
        {
            lblEndActivityMessage.Text = string.Empty;
            lblEndActivityMessage.Visible = false;
            lblValidationMessage.Text = string.Empty;
            lblValidationMessage.Visible = false;
            //txtLocation.Text = string.Empty;
            MPE_EndActivity.Show();

            lblActivityTypeE.Text = ActivityType;
            lblActivityTypeIDE.Text = ActivityTypeID;
            lblActivityIDE.Text = ActivityID;
            lblEndActivityDate.Text = DateTime.Now.ToString();
            txtRemarkE.Text = Remark;

            //lblEndActivityMessage.ForeColor = Color.Red;
            //lblEndActivityMessage.Visible = true;

            //List<PActivityReferenceType> ActivityReferenceType = new BActivity().GetActivityReferenceType(null, null);
            //new DDLBind(ddlReferenceTypeE, ActivityReferenceType, "ReferenceTable", "ActivityReferenceTableID");
            new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
            new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
            //ddlReferenceTypeE.SelectedValue = "0";
            //txtCustomerName.Text = string.Empty;
            //txtCustomerID.Text = string.Empty;

            txtEffortDuration.Text = string.Empty;
            ddlExpenseType.SelectedValue = "0";
            txtAmount.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            //ViewState["ActivityID"] = lblActivityID.Text;

            PActivity Activity = new PActivity();
            lblEndActivityMessage.ForeColor = Color.Red;
            lblEndActivityMessage.Visible = true;

        }
        public string CurrentLocation
        {
            get
            {
                if (Session["VisitActivityReport"] == null)
                {
                    Session["ViistActivityReport"] = "";
                }
                return (string)Session["VisitActivityReport"];
            }
            set
            {
                Session["VisitActivityReport"] = value;
            }
        }
        protected void btnEndActivityE_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_EndActivity.Show();
                lblEndActivityMessage.ForeColor = Color.Red;
                lblEndActivityMessage.Visible = true;

                int? ExpenseTypeID = ddlExpenseType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlExpenseType.SelectedValue);
                decimal?  Amount = string.IsNullOrEmpty(txtAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtAmount.Text.Trim()); 

                int? EffortTypeID = ddlEffortType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlEffortType.SelectedValue.Trim());
                decimal? EffortDuration = string.IsNullOrEmpty(txtEffortDuration.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtEffortDuration.Text.Trim()); 

                PApiResult Results = new BActivity().EndActivityWithVisit(Convert.ToInt32(lblActivityIDE.Text), txtRemarkE.Text.Trim(), Convert.ToDecimal(Session["Latitude"]), Convert.ToDecimal(Session["Longitude"])
                    , ExpenseTypeID, Amount, EffortTypeID, EffortDuration);
                 
                if (Results.Status == PApplication.Failure)
                {
                    lblValidationMessage.Text = "<br/>" + Results.Message;
                    lblValidationMessage.ForeColor = Color.Red;
                    lblValidationMessage.Visible = true;
                    return;
                }
                lblMessage.Text = "Visit Activity ended successfully.";
                lblMessage.ForeColor = Color.Green;

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
        void FillActivity()
        {
            PActivitySearch S = new PActivitySearch();

            //S.ActivityTypeID = ddlActivityType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlActivityType.SelectedValue);
            //S.ActivityID = string.IsNullOrEmpty(txtActivityID.Text.Trim()) ? (Int64?)null : Convert.ToInt64(txtActivityID.Text.Trim());
            //S.ActivityDateFrom = string.IsNullOrEmpty(txtActivityDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtActivityDateFrom.Text.Trim());
            //S.ActivityDateTo = string.IsNullOrEmpty(txtActivityDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtActivityDateTo.Text.Trim());
            ////S.CustomerCode = txtCustomerCode.Text.Trim();
            ////S.EquipmentSerialNo = txtEquipment.Text.Trim();
            //S.ActivityReferenceTableID = ddlReferenceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlReferenceType.SelectedValue);
            //S.ReferenceNumber = txtReferenceNumber.Text.Trim();
            //Activity1 = new BActivity().GetActivity(S, PSession.User.UserID);

            Activity1 = new BActivity().GetActivityWithVisitByID(null, ColdVisit.ColdVisitID);

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
    }
}