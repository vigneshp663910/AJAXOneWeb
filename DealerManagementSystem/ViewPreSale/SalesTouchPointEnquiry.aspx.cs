using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class SalesTouchPointEnquiry : BasePage
    {
        long? SalesTouchPointEnquiryID = null;
        string SalesTouchPointsEnquiryNumber = null;
        string CustomerName = null;
        int? CountryID = null;
        int? StateID = null;
        int? DistrictID = null;
        DateTime? DateFrom = null;
        DateTime? DateTo = null;
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
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_SalesTouchPointEnquiry; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Sales Touch Point Enquiry');</script>");
            try
            {
                lblMessage.Text = "";
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;

                    new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                    ddlCountry.SelectedValue = "1";
                    new DDLBind(ddlState, new BDMS_Address().GetState(null, 1, null, null, null), "State", "StateID", true, "Select");
                    txtFromDate.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtToDate.Text = DateTime.Now.ToShortDateString();
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
            PApiResult Result = new BSalesTouchPoint().GetSalesTouchPointEnquiry(SalesTouchPointEnquiryID, SalesTouchPointsEnquiryNumber, DateFrom, DateTo, CountryID, StateID, DistrictID, CustomerName, PageIndex, gvEnquiry.PageSize, null);

            List<PSalesTouchPointEnquiry> Enq = JsonConvert.DeserializeObject<List<PSalesTouchPointEnquiry>>(JsonConvert.SerializeObject(Result.Data));
            gvEnquiry.DataSource = Enq;
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
        protected void BtnView_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divDetailsView.Visible = true;

            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            ViewState["SalesTouchPointEnquiryID"] = Convert.ToInt64(BtnView.CommandArgument);
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
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PDMS_State> State = new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            new DDLBind(ddlState, State, "State", "StateID");
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }
        void Search()
        {
            SalesTouchPointsEnquiryNumber = txtSEnquiryNumber.Text.Trim();
            CustomerName = txtSCustomerName.Text.Trim();
            CountryID = ddlCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCountry.SelectedValue);
            if (CountryID != null)
            {
                StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                if (StateID != null)
                    DistrictID = ddlDistrict.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDistrict.SelectedValue);
            }

            DateFrom = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text.Trim());

            DateTo = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text.Trim());
        }
    }
}