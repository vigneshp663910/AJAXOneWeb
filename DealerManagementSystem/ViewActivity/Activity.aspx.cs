using Business;
using Properties;
using System;
using System.Collections.Generic;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Activity');</script>");

            if (!IsPostBack)
            {
                List<PActivityType> ActivityType = new BActivity().GetActivityType(null, null, null, true);
                new DDLBind(ddlActivityType, ActivityType, "ActivityTypeName", "ActivityTypeID");

                List<PActivityReferenceType> ActivityReferenceType = new BActivity().GetActivityReferenceType(null, null, true);
                new DDLBind(ddlReference, ActivityReferenceType, "ReferenceTable", "ActivityReferenceTableID");

                txtActivityDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtActivityDateFrom.TextMode = TextBoxMode.Date;

                txtActivityDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtActivityDateTo.TextMode = TextBoxMode.Date;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //MPE_Customer.Show();
            //PLead Lead = new PLead();
            //lblMessageLead.ForeColor = Color.Red;
            //lblMessageLead.Visible = true;
            //string Message = "";
            //if (!string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            //{
            //    Lead.Customer = new PDMS_Customer();
            //    Lead.Customer.CustomerID = Convert.ToInt64(txtCustomerID.Text.Trim());
            //}
            //else
            //{
            //    Message = UC_Customer.ValidationCustomer();
            //    if (!string.IsNullOrEmpty(Message))
            //    {
            //        lblMessageLead.Text = Message;
            //        return;
            //    }
            //    Lead.Customer = UC_Customer.ReadCustomer();
            //}

            //Message = ValidationLead();
            //if (!string.IsNullOrEmpty(Message))
            //{
            //    lblMessageLead.Text = Message;
            //    return;
            //}

            //Lead.LeadDate = Convert.ToDateTime(txtLeadDate.Text.Trim());
            //Lead.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
 
            //Lead.Category = new PLeadCategory() { CategoryID = Convert.ToInt32(ddlCategory.SelectedValue) };
            //Lead.Qualification = new PLeadQualification() { QualificationID = Convert.ToInt32(ddlQualification.SelectedValue) };
            //Lead.Source = new PLeadSource() { SourceID = Convert.ToInt32(ddlSource.SelectedValue) };
            //Lead.Type = new PLeadType() { TypeID = Convert.ToInt32(ddlLeadType.SelectedValue) };
            //Lead.Remarks = txtRemarks.Text.Trim();
            //Lead.CreatedBy = new PUser { UserID = PSession.User.UserID };

            //string result = new BAPI().ApiPut("Lead", Lead);
            //result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            //if (result == "0")
            //{
            //    MPE_Customer.Show();
            //    lblMessageLead.Text = "Customer is not updated successfully ";
            //    return;
            //}
            //else
            //{
            //    lblMessage.Visible = true;
            //    lblMessage.ForeColor = Color.Green;
            //    lblMessage.Text = "Customer is updated successfully ";
            //}

            //PLeadSearch S = new PLeadSearch();
            //S.LeadID = Convert.ToInt64(result);

            //gvLead.DataSource = new BLead().GetLead(S);
            //gvLead.DataBind();
            //UC_Customer.FillClean();
            //MPE_Customer.Hide();
        }

        public string ValidationActivity()
        {
            string Message = "";
            txtActivityID.BorderColor = Color.Silver;
            txtActivityDateFrom.BorderColor = Color.Silver;
            txtActivityDateTo.BorderColor = Color.Silver;
            ddlActivityType.BorderColor = Color.Silver;
            txtCustomerCode.BorderColor = Color.Silver;
            txtCustomerName.BorderColor = Color.Silver;
            
            //if (string.IsNullOrEmpty(txtLeadDate.Text.Trim()))
            //{
            //    Message = "Please enter the Lead Date";
            //    txtLeadDate.BorderColor = Color.Red;
            //}
            //else if (ddlProductType.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Product Type";
            //    ddlProductType.BorderColor = Color.Red;
            //}

           
            //else if (ddlCategory.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Category";
            //    ddlCategory.BorderColor = Color.Red;
            //}
            //else if (ddlQualification.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Qualification";
            //    ddlQualification.BorderColor = Color.Red;
            //}
            //else if (ddlSource.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Source";
            //    ddlSource.BorderColor = Color.Red;
            //}
            //else if (ddlLeadType.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the LeadType";
            //    ddlLeadType.BorderColor = Color.Red;
            //}
            //else if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Remark";
            //    txtRemarks.BorderColor = Color.Red;
            //}
            return Message;
        }

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
            //gv.DataSource = Activity1;
            //gv.DataBind();
            //lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Lead1.Count;
        }

        void FillActivity()
        {
            int? ActivityTypeID = ddlActivityType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlActivityType.SelectedValue);
            DateTime? ActivityDateFrom = string.IsNullOrEmpty(txtActivityDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtActivityDateFrom.Text.Trim());
            DateTime? ActivityDateTo = string.IsNullOrEmpty(txtActivityDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtActivityDateTo.Text.Trim());
            Int64? CustomerCode = string.IsNullOrEmpty(txtCustomerCode.Text.Trim()) ? (Int64?)null : Convert.ToInt64(txtCustomerCode.Text.Trim());
            string CustomerName = string.IsNullOrEmpty(txtCustomerName.Text.Trim()) ? (string)null : Convert.ToString(txtCustomerName.Text.Trim());
            string Equipment = string.IsNullOrEmpty(txtEquipment.Text.Trim()) ? (string)null : Convert.ToString(txtEquipment.Text.Trim());
            int? ReferenceID = ddlReference.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlReference.SelectedValue);
            Activity1 = new BActivity().GetActivity(ActivityTypeID, ActivityDateFrom, ActivityDateTo, CustomerCode, CustomerName, Equipment, ReferenceID);

            gvActivity.DataSource = Activity1;
            gvActivity.DataBind();

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
            if (1 == 0)
            {
                MPE_EndActivity.Show();
            }
            else
            {
                MPE_Activity.Show();
                lblActivityDate.Text = DateTime.Now.ToString();
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

        protected void btnViewLead_Click(object sender, EventArgs e)
        {
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
            //ViewState["LeadID"] = lblLeadID.Text;

            //divList.Visible = false;
            //divDetailsView.Visible = true;
            //UC_LeadView.fillViewLead(Convert.ToInt64(lblLeadID.Text));
        }

        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvLead.PageIndex = e.NewPageIndex;
            //FillLead();
        }

        protected void gvActivities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            string Latitude = hfLatitude.Value;
            string Longitude = hfLongitude.Value;
        }

        protected void btnEndActivity_Click(object sender, EventArgs e)
        {

        }
    }
}