using AjaxControlToolkit;
using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class Lead : System.Web.UI.Page
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Lead');</script>");

            if (!IsPostBack)
            { 

                List <PLeadQualification > Qualification = new BLead().GetLeadQualification(null, null); 
                new DDLBind(ddlSQualification, Qualification, "Qualification", "QualificationID");

                List<PLeadSource> Source = new BLead().GetLeadSource(null, null); 
                new DDLBind(ddlSSource, Source, "Source", "SourceID"); 

                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null); 
                new DDLBind(ddlSCountry, Country, "Country", "CountryID"); 
           
               // ddlCountry.SelectedValue = "1";
                List < PDMS_State > State= new BDMS_Address().GetState(1, null, null, null);
              //  new DDLBind(ddlState, State, "State", "StateID");
                new DDLBind(ddlSState, State, "State", "StateID");
                // new DDLBind(ddlCState, State, "State", "StateID"); 

                List<PLeadStatus> Status = new BLead().GetLeadStatus(null, null);
                new DDLBind(ddlSStatus, Status, "Status", "StatusID");
               // new DDLBind(ddlStatus, Status, "Status", "StatusID");

                //ddlProgressStatus.SelectedValue = "1";
                //ddlStatus.SelectedValue = "1";

               
                //cbCustomers.DataTextField = "State";
                //cbCustomers.DataValueField = "StateID";
                //cbCustomers.DataSource = State;
                //cbCustomers.DataBind();

                //cbCustomers.Items.Insert(0, new ListItem("Select", "0"));

                if(Session["leadStatusID"] != null)
                {
                    ddlSStatus.SelectedValue = Convert.ToString(Session["leadStatusID"]);
                    txtLeadDateFrom.Text = Convert.ToDateTime(Session["leadDateFrom"]).ToString("yyyy-MM-dd");
                    Session["leadStatusID"] = null;
                    Session["leadDateFrom"] = null;
                    FillLead();
                }

            }
        }

        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            PLead_Insert Lead = new PLead_Insert();
            lblMessageLead.ForeColor = Color.Red;
            lblMessageLead.Visible = true;
            string Message = "";
            Message = UC_AddLead.Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageLead.Text = Message;
                return;
            }
            Lead= UC_AddLead.Read();


            if (!string.IsNullOrEmpty(txtCustomerID.Text.Trim()))
            {
                Lead.Customer = new PDMS_Customer();
                Lead.Customer.CustomerID = Convert.ToInt64(txtCustomerID.Text.Trim());
            }
            else
            {
                Message = UC_Customer.ValidationCustomer(); 
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageLead.Text = Message;
                    return;
                }
                Lead.Customer = UC_Customer.ReadCustomer();
            }   
            

            string result = new BAPI().ApiPut("Lead", Lead); 
            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            if (result == "0")
            {
                MPE_Customer.Show();
                lblMessageLead.Text = "Customer is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Customer is updated successfully ";
            }

            PLeadSearch S = new PLeadSearch();
            S.LeadID = Convert.ToInt64(result);
             
            gvLead.DataSource = new BLead().GetLead(S);
            gvLead.DataBind();  
            UC_Customer.FillClean();
            MPE_Customer.Hide();
        }

        

        //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    new DDLBind(ddlState, new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
        //}

        //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null), "District", "DistrictID");
        //}

        //protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    new DDLBind(ddlTehsil, new BDMS_Address().GetTehsil(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), null), "Tehsil", "TehsilID");
        //}

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillLead();
        }


        public List<PLead> Lead1
        {
            get
            {
                if (Session["Lead1"] == null)
                {
                    Session["Lead1"] = new List<PLead>();
                }
                return (List<PLead>)Session["Lead1"];
            }
            set
            {
                Session["Lead1"] = value;
            }
        }


        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageIndex > 0)
            {
                gvLead.PageIndex = gvLead.PageIndex - 1;
                LeadBind(gvLead, lblRowCount, Lead1);
            }
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvLead.PageCount > gvLead.PageIndex)
            {
                gvLead.PageIndex = gvLead.PageIndex + 1;
                LeadBind(gvLead, lblRowCount, Lead1);
            }
        }

        void LeadBind(GridView gv, Label lbl, List<PLead> Lead1)
        {
            gv.DataSource = Lead1;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Lead1.Count;
        }

        void FillLead()
        {
            PLeadSearch S = new PLeadSearch();
            S.LeadNumber = txtLeadNumber.Text.Trim();
            S.StateID = ddlSState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSState.SelectedValue);
           // S.ProgressStatusID = ddlSProgressStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSProgressStatus.SelectedValue);
         //   S.CategoryID = ddlSCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCategory.SelectedValue);
            S.QualificationID = ddlSQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSQualification.SelectedValue);
            S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
          //  S.TypeID = ddlSType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSType.SelectedValue);
            S.CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
            S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);

            S.CustomerCode = txtCustomer.Text.Trim();
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            //List<PLead> Leads = new BLead().GetLead(S);
            Lead1 = new BLead().GetLead(S);

            gvLead.DataSource = Lead1;
            gvLead.DataBind();


            if (Lead1.Count == 0)
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
                lblRowCount.Text = (((gvLead.PageIndex) * gvLead.PageSize) + 1) + " - " + (((gvLead.PageIndex) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + Lead1.Count;
            }

        }


        //protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    DropDownList ddlAction = (DropDownList)gvRow.FindControl("ddlAction");
        //    Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
        //    ViewState["LeadID"] = lblLeadID.Text;
             
        //    if (ddlAction.Text == "View Lead")
        //    {
        //        divList.Visible = false;
        //        divDetailsView.Visible = true;
        //        UC_LeadView.fillViewLead(Convert.ToInt64(lblLeadID.Text));
        //    }
            //else if (ddlAction.Text == "Assign")
            //{
            //    MP_AssignSE.Show();
            //    fillAssignSalesEngineer(Convert.ToInt64(lblLeadID.Text)); 
            //}
            //else if (ddlAction.Text == "Add Follow-up")
            //{
            //    MPE_FollowUp.Show();
            //    fillFollowUp(Convert.ToInt64(lblLeadID.Text));
            //}
            //else if (ddlAction.Text == "Customer Conversation")
            //{
            //    MPE_Conversation.Show();
            //    fillConversation(Convert.ToInt64(lblLeadID.Text)); 
            //}            
            //else if (ddlAction.Text == "Edit Financial Info")
            //{
            //    MPE_Financial.Show();
            //    fillFinancial(Convert.ToInt64(lblLeadID.Text));
            //}
            //else if (ddlAction.Text == "Add Effort")
            //{
            //    MPE_Effort.Show();
            //    fillEffort(Convert.ToInt64(lblLeadID.Text));
            //}
            //else if (ddlAction.Text == "Add Expense")
            //{
            //    MPE_Expense.Show();
            //    fillExpense(Convert.ToInt64(lblLeadID.Text));
            //}
        //}
        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlSState, new BDMS_Address().GetState(Convert.ToInt32(ddlSCountry.SelectedValue), null, null, null), "State", "StateID");
        }
       
        //protected void cbNewCustomer_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(cbNewCustomer.Checked)
        //    {
        //        pnlNewCustomerName.Visible = true;
        //        pnlOldCustomerName.Visible = false;
        //    }
        //    else
        //    {
        //        pnlNewCustomerName.Visible = false;
        //        pnlOldCustomerName.Visible = true;
        //    }
        //}
           
        protected void btnAddLead_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show(); 
            UC_AddLead.FillMaster();
            UC_Customer.FillMaster();
        }
        [WebMethod]
        public static List<string> GetCustomer(string CustS)
        {
            List<string> Emp = new List<string>();
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS,1);
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
                //    + "<table><tr><td>"
                //    + "<label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label></td><td>"+ cust.CustomerType + "</td></tr >" + "<tr><td>"
                //    + "<label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label></td><td>"
                //    + "<label id='lblMobile" + i + "'>" + cust.Mobile + " </td></tr></ table >";
                //Emp.Add(div);
            }
            return Emp;
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }

        protected void btnViewLead_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
            ViewState["LeadID"] = lblLeadID.Text;

            divList.Visible = false;
            divDetailsView.Visible = true;
            UC_LeadView.fillViewLead(Convert.ToInt64(lblLeadID.Text));
        }

        protected void gvLead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLead.PageIndex = e.NewPageIndex;
            FillLead();
        }
    }
}