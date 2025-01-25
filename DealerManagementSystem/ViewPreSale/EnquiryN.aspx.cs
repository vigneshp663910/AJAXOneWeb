using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class EnquiryN : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_EnquiryN; } }

        long? EnquiryID = null;
        int? DealerID = null;
        int? EngineerUserID = null;
        string EnquiryNumber = null;
        string CustomerName = null;
        int? CountryID = null;
        int? StateID = null;
        int? DistrictID = null;
        DateTime? DateFrom = null;
        DateTime? DateTo = null;
        int? SourceID = null;
        int? StatusID = null;

        //public List<PEnquiry> PEnquiry
        //{
        //    get
        //    {
        //        if (Session["Enquiry"] == null)
        //        {
        //            Session["Enquiry"] = new List<PEnquiry>();
        //        }
        //        return (List<PEnquiry>)Session["Enquiry"];
        //    }
        //    set
        //    {
        //        Session["Enquiry"] = value;
        //    }
        //}
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Enquiry');</script>");

            try
            {
                lblMessage.Text = "";
                lblAddEnquiryMessage.Text = "";
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;

                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                    new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                    ddlCountry.SelectedValue = "1";
                    new DDLBind(ddlState, new BDMS_Address().GetState(null, 1, null, null, null), "State", "StateID");
                    txtFromDate.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtToDate.Text = DateTime.Now.ToShortDateString();


                    List<PLeadSource> Source = new BLead().GetLeadSource(null, null);
                    new DDLBind(ddlSSource, Source, "Source", "SourceID");

                    List<PPreSaleStatus> Status = new BDMS_Master().GetPreSaleStatus(null, null);
                    if (Session["leadStatusID"] != null)
                    {
                        ddlSStatus.SelectedValue = Convert.ToString(Session["leadStatusID"]);
                        txtFromDate.Text = Convert.ToDateTime(Session["leadDateFrom"]).ToShortDateString();
                        txtToDate.Text = "";
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
                        FillGrid();
                    }

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private void FillGrid()
        {
            Search();
            //int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            //int? DealerEmployeeUserID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            //string CustomerName = txtSCustomerName.Text.Trim();
            //int? StateID = null, DistrictID = null;
            //int? CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            //if (CountryID != null)
            //{
            //    StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            //    if (StateID != null)
            //        DistrictID = ddlDistrict.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDistrict.SelectedValue);
            //}

            //int? SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
            //int? StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);

            //DateTime? DateF = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text.Trim());

            //DateTime? DateT = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text.Trim());

            PApiResult Result = new BEnquiry().GetEnquiry(null, DealerID, EngineerUserID, txtSEnquiryNumber.Text.Trim(), CustomerName, CountryID, StateID, DistrictID, DateFrom, DateTo, SourceID, StatusID, PageIndex, gvEnquiry.PageSize);

            gvEnquiry.DataSource = JsonConvert.DeserializeObject<List<PEnquiry>>(JsonConvert.SerializeObject(Result.Data));
            gvEnquiry.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnEnqArrowLeft.Visible = false;
                ibtnEnqArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvEnquiry.PageSize - 1) / gvEnquiry.PageSize;
                lblRowCount.Visible = true;
                ibtnEnqArrowLeft.Visible = true;
                ibtnEnqArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvEnquiry.PageSize) + 1) + " - " + (((PageIndex - 1) * gvEnquiry.PageSize) + gvEnquiry.Rows.Count) + " of " + Result.RowCount;
            }

            //if (PEnquiry.Count == 0)
            //{
            //    lblRowCount.Visible = false;
            //    ibtnEnqArrowLeft.Visible = false;
            //    ibtnEnqArrowRight.Visible = false;
            //}
            //else
            //{
            //    lblRowCount.Visible = true;
            //    ibtnEnqArrowLeft.Visible = true;
            //    ibtnEnqArrowRight.Visible = true;
            //    lblRowCount.Text = (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + 1) + " - " + (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + gvEnquiry.Rows.Count) + " of " + PEnquiry.Count;
            //}
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            MPE_AddEnquiry.Show();
            UC_AddEnquiry.FillMaster();
        }


        protected void BtnView_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divDetailsView.Visible = true;

            lblAddEnquiryMessage.Text = "";
            lblMessage.Text = "";
            ImageButton BtnView = (ImageButton)sender;
            ViewState["EnquiryID"] = Convert.ToInt64(BtnView.CommandArgument);
            UC_EnquiryView.fillViewEnquiry(Convert.ToInt64(BtnView.CommandArgument));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }
        protected void ibtnEnqArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillGrid();
            }
        }
        protected void ibtnEnqArrowRight_Click(object sender, ImageClickEventArgs e)
        {


            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblAddEnquiryMessage.Visible = true;
                lblAddEnquiryMessage.ForeColor = Color.Red;
                MPE_AddEnquiry.Show();
                string Message = UC_AddEnquiry.Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblAddEnquiryMessage.Text = Message;
                    return;
                }
                PEnquiry enquiryAdd = new PEnquiry();
                enquiryAdd = UC_AddEnquiry.Read();
                if (new BEnquiry().InsertOrUpdateEnquiry(enquiryAdd, PSession.User.UserID))
                {
                    lblMessage.Text = "Enquiry is saved successfully...";
                    lblMessage.ForeColor = Color.Green;
                    MPE_AddEnquiry.Hide();
                    FillGrid();
                }
                else
                {
                    lblAddEnquiryMessage.Text = "Enquiry is not saved successfully...!";
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblAddEnquiryMessage.Text = ex.Message.ToString();
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PDMS_State> State = new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            new DDLBind(ddlState, State, "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
                PApiResult Result = new BEnquiry().GetEnquiryExcel(null, DealerID, EngineerUserID, txtSEnquiryNumber.Text.Trim(), CustomerName, CountryID, StateID, DistrictID, DateFrom, DateTo, SourceID, StatusID);
                DataTable Enquirys = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

                try
                {
                    new BXcel().ExporttoExcel(Enquirys, "Enquiry Report");
                }
                catch
                {
                }
                finally
                {
                }
                // SalesCommisionClaimExportExcel();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void SalesCommisionClaimExportExcel()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sno");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Region");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Enquiry Number");
            dt.Columns.Add("Enquiry Date");
            dt.Columns.Add("Status");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Person Name");
            dt.Columns.Add("Mail");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Address1");
            dt.Columns.Add("Address2");
            dt.Columns.Add("Address3");
            dt.Columns.Add("Country");
            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("Product");
            dt.Columns.Add("Source");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Rejected Remark");
            dt.Columns.Add("Product Type");
            dt.Columns.Add("Sales Teritory Manager");

            Search();



            PApiResult Result = new BEnquiry().GetEnquiryExcel(null, DealerID, EngineerUserID, txtSEnquiryNumber.Text.Trim(), CustomerName, CountryID, StateID, DistrictID, DateFrom, DateTo, SourceID, StatusID);

            DataTable Enquirys = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

            //        dt.Rows.Add(
            //             i
            //            , Enquiry.CreatedBy.ContactName
            //            , Enquiry.State.Region.Region
            //            , Enquiry.Dealer.DealerCode
            //            , Enquiry.Dealer.DealerName
            //            , Enquiry.EnquiryNumber
            //            , (Enquiry.EnquiryDate == null) ? "" : Enquiry.EnquiryDate.ToString()
            //            , Enquiry.Status.Status
            //            , Enquiry.CustomerName
            //            , Enquiry.PersonName
            //            , Enquiry.Mail
            //            , Enquiry.Mobile
            //            , Enquiry.Address
            //            , Enquiry.Address2
            //            , Enquiry.Address3
            //            , Enquiry.Country.Country
            //            , Enquiry.State.State
            //            , Enquiry.District.District
            //            , Enquiry.Product
            //            , Enquiry.Source.Source
            //            , Enquiry.Remarks
            //            , Enquiry.RejectReason
            //            , Enquiry.ProductType.ProductType 
            //            ,Enquiry.STM.ContactName
            //            );
            //    }
            //}
            try
            {
                new BXcel().ExporttoExcel(Enquirys, "Enquiry Report");
            }
            catch
            {

            }
            finally
            {
            }
        }
        void Search()
        {
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            EngineerUserID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            CustomerName = txtSCustomerName.Text.Trim();

            CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            if (CountryID != null)
            {
                StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                if (StateID != null)
                    DistrictID = ddlDistrict.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDistrict.SelectedValue);
            }

            SourceID = ddlSSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSSource.SelectedValue);
            StatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);

            DateFrom = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text.Trim());

            DateTo = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text.Trim());
        }

        protected void gvEnquiry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#e6ccff';";
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#b3ecff';";
                //e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='aquamarine';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click On View Icon for More Details... ";
            }
        }
    }
}