using Business;
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
    public partial class EnquiryIndiamart : System.Web.UI.Page
    {
        public DataTable Enquiry
        {
            get
            {
                if (Session["EnquiryIndiamart"] == null)
                {
                    Session["EnquiryIndiamart"] = new DataTable();
                }
                return (DataTable)Session["EnquiryIndiamart"];
            }
            set
            {
                Session["EnquiryIndiamart"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Enquiry Indiamart');</script>");
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        protected void btnEnquiryIndiamart_Click(object sender, EventArgs e)
        {
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            Enquiry = new BEnquiryIndiamart().GetEnquiryIndiamart(DateFrom, DateTo);
            gvEnquiry.DataSource = Enquiry;
            gvEnquiry.DataBind();
            lblRowCountEnquiryIM.Text = (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + 1) + " - " + (((gvEnquiry.PageIndex) * gvEnquiry.PageSize) + gvEnquiry.Rows.Count) + " of " + Enquiry.Rows.Count;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(Enquiry, "Enquiry Indiamart");
        }
        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnquiry.PageIndex = e.NewPageIndex;
            gvEnquiry.DataSource = Enquiry;
            gvEnquiry.DataBind();
        }
        protected void ibtnEnquiryIMArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageIndex > 0)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex - 1;
                EnquiryIndiamartBind(gvEnquiry, lblRowCountEnquiryIM, Enquiry);
            }
        }
        protected void ibtnEnquiryIMArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageCount > gvEnquiry.PageIndex)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex + 1;
                EnquiryIndiamartBind(gvEnquiry, lblRowCountEnquiryIM, Enquiry);
            }
        }
        void EnquiryIndiamartBind(GridView gv, Label lbl,DataTable Enquiry)
        {
            gv.DataSource = Enquiry;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Enquiry.Rows.Count;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Convert to Enquiry")
            {
                MPE_AddEnquiry.Show();
                UC_AddEnquiry.FillMaster();
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Panel pnlAddEnquiry = new Panel();
                //TextBox txtCustomerName = UC_AddEnquiry.FindControl("txtCustomerName") as TextBox;
                //txtCustomerName.Text = gvEnquiry.DataKeys[gvRow.RowIndex].Value.ToString();
                if (gvEnquiry.DataKeys[gvRow.RowIndex].Values[3].ToString() != "")
                {
                    ((TextBox)UC_AddEnquiry.FindControl("txtCustomerName")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[3].ToString();
                }
                else 
                {
                    ((TextBox)UC_AddEnquiry.FindControl("txtCustomerName")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[0].ToString();
                }
                ((TextBox)UC_AddEnquiry.FindControl("txtEnquiryDate")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[8].ToString();
                ((TextBox)UC_AddEnquiry.FindControl("txtPersonName")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[0].ToString();
                ((TextBox)UC_AddEnquiry.FindControl("txtMobile")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[2].ToString();
                ((TextBox)UC_AddEnquiry.FindControl("txtMail")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[1].ToString();
                ((TextBox)UC_AddEnquiry.FindControl("txtAddress")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[4].ToString();

                if (gvEnquiry.DataKeys[gvRow.RowIndex].Values[4].ToString().Length > 40)
                {
                    ((TextBox)UC_AddEnquiry.FindControl("txtAddress")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[4].ToString().Substring(0, 40);
                    ((TextBox)UC_AddEnquiry.FindControl("txtAddress2")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[4].ToString().Substring(40);

                    if (((TextBox)UC_AddEnquiry.FindControl("txtAddress2")).Text.Length > 40)
                    {
                        ((TextBox)UC_AddEnquiry.FindControl("txtAddress3")).Text = ((TextBox)UC_AddEnquiry.FindControl("txtAddress2")).Text.Substring(0);
                    }                    
                }
                
                List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, gvEnquiry.DataKeys[gvRow.RowIndex].Values[5].ToString());
                if (State.Count == 1)
                {
                    ((DropDownList)UC_AddEnquiry.FindControl("ddlState")).SelectedValue = State[0].StateID.ToString();
                }

                ((TextBox)UC_AddEnquiry.FindControl("txtProduct")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[7].ToString();
            }
            else if (lbActions.Text == "Cancel Enquiry")
            { 
                
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
                    lblMessage.Text = "Enquiry Was Saved Successfully...";
                    lblMessage.ForeColor = Color.Green;
                    MPE_AddEnquiry.Hide();
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