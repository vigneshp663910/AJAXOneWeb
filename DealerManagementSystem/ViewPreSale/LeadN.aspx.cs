using AjaxControlToolkit;
using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class LeadN : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_LeadN; } }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Lead');</script>");

            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                List<PLeadQualification> Qualification = new BLead().GetLeadQualification(null, null);
                new DDLBind(ddlSQualification, Qualification, "Qualification", "QualificationID");

                List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
                new DDLBind(ddlSSource, Source, "Source", "SourceID");
                new DDLBind(ddlSSalesChannelType, new BPreSale().GetPreSalesMasterItem((short)PreSalesMasterHeader.SalesChannelType), "ItemText", "MasterItemID");
                List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                new DDLBind(ddlSCountry, Country, "Country", "CountryID");

                // ddlCountry.SelectedValue = "1"; 
                new DDLBind(ddlRegion, new BDMS_Address().GetRegion(1, null, null), "Region", "RegionID");

                List<PLeadStatus> Status = new BLead().GetLeadStatus(null, null);
                new DDLBind(ddlSStatus, Status, "Status", "StatusID");
                ddlSStatus.Items.Insert(ddlSStatus.Items.Count, new ListItem("Expected date of sales is less than today date", "100"));
                ddlSStatus.Items.Insert(ddlSStatus.Items.Count, new ListItem("Next Follow-up Date is Less Than today Date", "101"));
                ddlSStatus.Items.Insert(ddlSStatus.Items.Count, new ListItem("Engineer not Defined", "102"));
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");


                if (Session["leadStatusID"] != null)
                {
                    ddlSStatus.SelectedValue = Convert.ToString(Session["leadStatusID"]);
                    txtLeadDateFrom.Text = Convert.ToDateTime(Session["leadDateFrom"]).ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["leadDealerID"])))
                    {
                        ddlDealer.SelectedValue = Convert.ToString(Session["leadDealerID"]);
                        List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
                        new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["EngineerUserID"])))
                    {
                        ddlDealerEmployee.SelectedValue = Convert.ToString(Session["EngineerUserID"]);
                    }
                    Session["leadStatusID"] = null;
                    Session["leadDateFrom"] = null;
                    Session["leadDealerID"] = null;
                    Session["EngineerUserID"] = null;
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
            Lead = UC_AddLead.Read();

             
            Message = UC_Customer.ValidationCustomer();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageLead.Text = Message;
                return;
            }
            Lead.Customer = UC_Customer.ReadCustomer();
            if (Lead.Customer.CustomerID == 0)
            {
                if (Lead.ProductTypeID == (short)ProductType.Udaan)
                {
                    Lead.Customer.CustomerSalesTypeID = (short)PreSalesMasterItem.UdaanCustomer;
                }
                else
                {
                    Lead.Customer.CustomerSalesTypeID = (short)PreSalesMasterItem.RegularCustomer;
                }
            }
            else
            {
                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(Lead.Customer.CustomerID);
                if (Customer.SalesType.MasterItemID == (short)PreSalesMasterItem.RegularCustomer)
                {
                    if (Lead.ProductTypeID == (short)ProductType.Udaan)
                    {
                        lblMessageLead.Text = "You can not select this customer. To select this customer, please contact Co-ordinator";
                        return;
                    }
                }
                else if (Customer.SalesType.MasterItemID == (short)PreSalesMasterItem.UdaanCustomer)
                {
                    if (Lead.ProductTypeID != (short)ProductType.Udaan)
                    {
                        lblMessageLead.Text = "You can not select this customer. To select this customer, please contact Co-ordinator";
                        return;
                    }
                }
            }

            string result = new BAPI().ApiPut("Lead", Lead);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);
            
            if (Result.Status == PApplication.Failure)
            {
                lblMessageLead.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            
            PLeadSearch S = new PLeadSearch();
            S.LeadID = Convert.ToInt64(Result.Data);
            PApiResult ResultLead = new BLead().GetLead(S);
            gvLead.DataSource = JsonConvert.DeserializeObject<List<PLead>>(JsonConvert.SerializeObject(ResultLead.Data));
            gvLead.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvLead.PageSize - 1) / gvLead.PageSize;
                lblRowCount.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvLead.PageSize) + 1) + " - " + (((PageIndex - 1) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + ResultLead.RowCount;
            }

            UC_Customer.FillClean();
            MPE_Customer.Hide();
        }
         

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillLead();
        }
         
        //public List<PLead> Lead1
        //{
        //    get
        //    {
        //        if (Session["Lead1"] == null)
        //        {
        //            Session["Lead1"] = new List<PLead>();
        //        }
        //        return (List<PLead>)Session["Lead1"];
        //    }
        //    set
        //    {
        //        Session["Lead1"] = value;
        //    }
        //}
         
        protected void ibtnLeadArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillLead();
            }
            //if (gvLead.PageIndex > 0)
            //{
            //    gvLead.PageIndex = gvLead.PageIndex - 1;
            //    LeadBind(gvLead, lblRowCount, Lead1);
            //}
        }
        protected void ibtnLeadArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillLead();
            }
            //if (gvLead.PageCount > gvLead.PageIndex)
            //{
            //    gvLead.PageIndex = gvLead.PageIndex + 1;
            //    LeadBind(gvLead, lblRowCount, Lead1);
            //}
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
            S.RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue); 
            S.QualificationID = ddlSQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSQualification.SelectedValue);
            S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
            S.SalesChannelTypeID = ddlSSalesChannelType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSalesChannelType.SelectedValue);
            S.ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
            S.CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);             
            S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue); 
            S.CustomerCode = txtCustomer.Text.Trim();
            S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
            S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

            S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            S.SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);

            S.PageIndex = PageIndex;
            S.PageSize = gvLead.PageSize;

            //Lead1 = new BLead().GetLead(S);
            //gvLead.DataSource = Lead1;

            PApiResult ResultLead = new BLead().GetLead(S);

            gvLead.DataSource = JsonConvert.DeserializeObject<List<PLead>>(JsonConvert.SerializeObject(ResultLead.Data)); 
            gvLead.DataBind();


            if (ResultLead.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnLeadArrowLeft.Visible = false;
                ibtnLeadArrowRight.Visible = false;
            }
            else
            {
                PageCount = (ResultLead.RowCount + gvLead.PageSize - 1) / gvLead.PageSize;
                lblRowCount.Visible = true;
                ibtnLeadArrowLeft.Visible = true;
                ibtnLeadArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvLead.PageSize) + 1) + " - " + (((PageIndex - 1) * gvLead.PageSize) + gvLead.Rows.Count) + " of " + ResultLead.RowCount;
            }

        } 
        protected void ddlSCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlRegion, new BDMS_Address().GetRegion( Convert.ToInt32(ddlSCountry.SelectedValue),  null, null), "Region", "RegionID");
        } 

        protected void btnAddLead_Click(object sender, EventArgs e)
        {
            MPE_Customer.Show();
            UC_AddLead.FillMaster();
            UC_Customer.FillMaster();
            UC_Customer.FindControl("txtCity");
            UC_Customer.FindControl("txtDOB");
            UC_Customer.FindControl("txtDOAnniversary");
            UC_Customer.FindControl("cbSendSMS");
            UC_Customer.FindControl("cbSendEmail");

            UC_Customer.FindControl("txtLatitude");
            UC_Customer.FindControl("txtDOB");
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
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                PLeadSearch S = new PLeadSearch();
                S.LeadNumber = txtLeadNumber.Text.Trim();
                S.RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
                S.QualificationID = ddlSQualification.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSQualification.SelectedValue);
                S.SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
                S.ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
                S.CountryID = ddlSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSCountry.SelectedValue);
                S.StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);

                S.CustomerCode = txtCustomer.Text.Trim();
                S.LeadDateFrom = string.IsNullOrEmpty(txtLeadDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateFrom.Text.Trim());
                S.LeadDateTo = string.IsNullOrEmpty(txtLeadDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtLeadDateTo.Text.Trim());

                S.DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                S.SalesEngineerID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                 
                try
                {
                    new BXcel().ExporttoExcel(new BLead().GetLeadExcel(S), "Lead Report");
                }
                catch
                {
                }
                finally
                {
                } 
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
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
                    + "<p><label id='lblCustomerName" + i + "'>" + cust.CustomerName + "</label><span>" + cust.CustomerType + "</span></p>"

                    + "<div class='customer-info'><label id='lblContactPerson" + i + "'>" + cust.ContactPerson + "</label>"
                    + "<label id='lblMobile" + i + "'>" + cust.Mobile + "</label></div>";
                Emp.Add(div);


            }
            return Emp;
        }
        [WebMethod]
        public static string GetProject(string Pro)
        {
            List<PProject> Project = new BProject().GetProjectAutocomplete(Pro);
            return JsonConvert.SerializeObject(Project);
        }

        protected void gvLead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {             
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click On View Icon for More Details... ";

                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                string lStatus = lblStatus.Text; 

                if (lStatus == "Unattended") { e.Row.Cells[0].Attributes["style"] = "background-color: darkgoldenrod"; }
                else if (lStatus == "In Progress") { e.Row.Cells[0].Attributes["style"] = "background-color: #3598dc"; }           
                else if (lStatus == "Quotation") { e.Row.Cells[0].Attributes["style"] = "background-color: #32c5d2"; }
                else if (lStatus == "Won") { e.Row.Cells[0].Attributes["style"] = "background-color: #26c281"; }
                else if (lStatus == "Sales Lost ") { e.Row.Cells[0].Attributes["style"] = "background-color: #d91e18"; }
                //else if (lStatus == "Sales Lost ") { e.Row.Cells[0].Attributes["style"] = "background-color: red"; }
                else if (lStatus == "Dropped") { e.Row.Cells[0].Attributes["style"] = "background-color: #d05454"; }


            }
        }
    }
}