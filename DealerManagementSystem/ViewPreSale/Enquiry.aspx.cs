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
    public partial class Enquiry : System.Web.UI.Page
    {
        public List<PEnquiry> PEnquiry
        {
            get
            {
                if (Session["Enquiry"] == null)
                {
                    Session["Enquiry"] = new List<PEnquiry>();
                }
                return (List<PEnquiry>)Session["Enquiry"];
            }
            set
            {
                Session["Enquiry"] = value;
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
            try
            {
                if (!IsPostBack)
                {
                    ClearField();
                    //FillGrid(null);
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
            int? CountryID = null, StateID = null, DistrictID = null;
            string CustomerName = null;
            if (!string.IsNullOrEmpty(txtSCustomerName.Text))
            {
                CustomerName = txtSCustomerName.Text.Trim();
            }
            if (ddlSCountry.SelectedValue != "0")
            {
                CountryID = Convert.ToInt32(ddlSCountry.SelectedValue);
            }
            if (ddlSState.SelectedValue != "0")
            {
                StateID = Convert.ToInt32(ddlSState.SelectedValue);
            }
            if (ddlSDistrict.SelectedValue != "0")
            {
                DistrictID = Convert.ToInt32(ddlSDistrict.SelectedValue);
            }
            DateTime? DateF = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime? DateT = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text.Trim());
            PEnquiry = new BEnquiry().GetEnquiry(null, (txtSEnquiryNumber.Text.Trim() == "") ? null : txtSEnquiryNumber.Text.Trim(), CustomerName, CountryID, StateID, DistrictID, DateF, DateT);
            gvEnquiry.DataSource = PEnquiry;
            gvEnquiry.DataBind();

            if (PEnquiry.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnEnqArrowLeft.Visible = false;
                ibtnEnqArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnEnqArrowLeft.Visible = true;
                ibtnEnqArrowRight.Visible = true;
                lblRowCount.Text = (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + 1) + " - " + (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + gvEnquiry.Rows.Count) + " of " + PEnquiry.Count;
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

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            MPE_AddEnquiry.Show();
            UC_AddEnquiry.FillMaster(); 
        }

        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
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

            lblAddEnquiryMessage.Text = "";
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            ViewState["EnquiryID"] = Convert.ToInt64(BtnView.CommandArgument);
            UC_EnquiryView.fillViewEnquiry(Convert.ToInt64(BtnView.CommandArgument));
        }
         
        void ClearField()
        {
            new DDLBind(ddlSCountry, new BDMS_Address().GetCountry(1, null), "Country", "CountryID");
            ddlSCountry.SelectedValue = "1";
            new DDLBind(ddlSState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");
            new DDLBind(ddlSDistrict, new BDMS_Address().GetDistrict(1, null, null, null, null, null), "District", "DistrictID");
            txtFromDate.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
            txtToDate.Text = DateTime.Now.ToShortDateString();
        }
      

        //protected void BtnBack_Click(object sender, EventArgs e)
        //{
        //    ClearField(); 
        //}

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }

     

        protected void ibtnEnqArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageIndex > 0)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex - 1;
                EnquiryBind(gvEnquiry, lblRowCount, PEnquiry);
            }
        }

        protected void ibtnEnqArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageCount > gvEnquiry.PageIndex)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex + 1;
                EnquiryBind(gvEnquiry, lblRowCount, PEnquiry);
            }
        }
        void EnquiryBind(GridView gv, Label lbl, List<PEnquiry> PEnquiry)
        {
            gv.DataSource = PEnquiry;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + PEnquiry.Count;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_AddEnquiry.Show();
                string Message = UC_AddEnquiry.Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                PEnquiry enquiryAdd = new PEnquiry();
                enquiryAdd = UC_AddEnquiry.Read(); 
                if (new BEnquiry().InsertOrUpdateEnquiry(enquiryAdd, PSession.User.UserID))
                {
                    lblMessage.Text = "Enquiry Was Saved Successfully...";
                    lblMessage.ForeColor = Color.Green; 
                }
                else
                {
                    lblAddEnquiryMessage.Text = "Enquiry Not Saved Successfully...!";
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblAddEnquiryMessage.Text = ex.Message.ToString();
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }
    }
}