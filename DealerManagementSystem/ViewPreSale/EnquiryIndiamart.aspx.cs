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
            int? PreSaleStatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);
            Enquiry = new BEnquiryIndiamart().GetEnquiryIndiamart(DateFrom, DateTo, PreSaleStatusID);
            gvEnquiry.DataSource = Enquiry;
            gvEnquiry.DataBind();
            //for (int i = 0; i < gvEnquiry.Rows.Count; i++)
            //{
            //    if (gvEnquiry.Rows[i].Cells[4].Text != "Open")                 
            //    {
            //        LinkButton lnkBtnConvert = (LinkButton)gvEnquiry.Rows[i].FindControl("lnkBtnConvert");
            //        LinkButton lnkBtnReject = (LinkButton)gvEnquiry.Rows[i].FindControl("lnkBtnReject");
            //        lnkBtnConvert.Visible = false;
            //        lnkBtnReject.Visible = false;
            //    }
            //}
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
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            if (lbActions.Text == "Convert to Enquiry")
            {
                MPE_AddEnquiry.Show();
                UC_AddEnquiry.FillMaster();
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Panel pnlAddEnquiry = new Panel();
                lblQueryIDAdd.Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[10].ToString();
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
                ((TextBox)UC_AddEnquiry.FindControl("txtMobile")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[2].ToString().Replace("+91-", "");
                ((TextBox)UC_AddEnquiry.FindControl("txtMail")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[1].ToString();

                TextBox txtAddress = ((TextBox)UC_AddEnquiry.FindControl("txtAddress"));
                TextBox txtAddress2 = ((TextBox)UC_AddEnquiry.FindControl("txtAddress2"));
                TextBox txtAddress3 = ((TextBox)UC_AddEnquiry.FindControl("txtAddress3"));
                AddressSplit(gvEnquiry.DataKeys[gvRow.RowIndex].Values[4].ToString(), txtAddress, txtAddress2, txtAddress3);

                //List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                //if (Country.Count == 1)
                //{
                //    ((DropDownList)UC_AddEnquiry.FindControl("ddlCountry")).SelectedValue = Country[0].CountryID.ToString();
                //}

                //List<PDMS_State> State = new BDMS_Address().GetState(Country[0].CountryID, null, null, gvEnquiry.DataKeys[gvRow.RowIndex].Values[5].ToString());
                List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, gvEnquiry.DataKeys[gvRow.RowIndex].Values[5].ToString());
                if (State.Count == 1)
                {
                    ((DropDownList)UC_AddEnquiry.FindControl("ddlState")).SelectedValue = State[0].StateID.ToString();
                }

                //List<PDMS_District> District = new BDMS_Address().GetDistrict(Country[0].CountryID, null, State[0].StateID, null, null, null);
                List<PDMS_District> District = new BDMS_Address().GetDistrict(1, null, State[0].StateID, null, gvEnquiry.DataKeys[gvRow.RowIndex].Values[9].ToString(), null);
                if (District.Count >= 1)
                {
                    ((DropDownList)UC_AddEnquiry.FindControl("ddlDistrict")).SelectedValue = District[0].DistrictID.ToString();
                }

                ((TextBox)UC_AddEnquiry.FindControl("txtProduct")).Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[7].ToString();
            }
            else if (lbActions.Text == "Reject")
            {
                
                lblQueryID.Text = gvEnquiry.DataKeys[gvRow.RowIndex].Values[10].ToString();
                MPE_RejectEnquiry.Show();
                lblRejectEnquiryMessage.Text = string.Empty;
                lblRejectEnquiryMessage.Visible = false;
            }
        }

        public void AddressSplit(string Input, TextBox txtAddress1, TextBox txtAddress2, TextBox txtAddress3)
        {
            string[] SplitedInput = Input.Split(' ');

            foreach (string Word in SplitedInput)
            {
                if (((txtAddress1.Text + Word).Length <= 40) && (string.IsNullOrEmpty(txtAddress2.Text)))
                {
                    txtAddress1.Text = txtAddress1.Text + " " + Word;
                }
                else if (((txtAddress2.Text + Word).Length <= 40) && (string.IsNullOrEmpty(txtAddress3.Text)))
                {
                    txtAddress2.Text = txtAddress2.Text + " " + Word;
                }
                else
                {
                    txtAddress3.Text = txtAddress3.Text + " " + Word;
                }
            }
            txtAddress1.Text = txtAddress1.Text.Trim();
            txtAddress2.Text = txtAddress2.Text.Trim();
            txtAddress3.Text = txtAddress3.Text.Trim();
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
                    //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                    //string QUERY_ID = Convert.ToString(gvEnquiry.DataKeys[gvRow.RowIndex].Value);
                    
                    if (new BEnquiryIndiamart().UpdateEnquiryIndiamartStatus(lblQueryIDAdd.Text, 2, PSession.User.UserID, txtRejectEnquiryReason.Text.Trim()))
                    {
                        lblMessage.Text = "Enquiry from India Mart saved successfully.";
                        lblMessage.ForeColor = Color.Green;
                        MPE_AddEnquiry.Hide();
                    }
                    else
                    {
                        lblAddEnquiryMessage.Text = "Enquiry from India Mart not saved successfully!";
                        lblAddEnquiryMessage.ForeColor = Color.Red;
                    }
                    
                }
                else
                {
                    lblAddEnquiryMessage.Text = "Enquiry from India Mart not saved successfully!!";
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblAddEnquiryMessage.Text = ex.Message.ToString();
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }

        protected void btnRejectEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                //Panel pnlRejectEnquiry = new Panel();
                //GridViewRow gvRow = (GridViewRow)(sender as Control);
                //string QUERY_ID = gvEnquiry.DataKeys[gvRow.RowIndex].Values[11].ToString();

                if (string.IsNullOrEmpty(txtRejectEnquiryReason.Text.Trim()))
                {
                    lblRejectEnquiryMessage.Text = "Please enter the Reasons to reject the Enquiry.";
                    lblRejectEnquiryMessage.ForeColor = Color.Red;
                    lblRejectEnquiryMessage.Visible = true;
                    MPE_RejectEnquiry.Show();
                    return;
                }

                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                //LinkButton lnkBtnReject = (LinkButton)gvRow.FindControl("lnkBtnReject");
                //string QUERY_ID = Convert.ToString(gvEnquiry.DataKeys[gvRow.RowIndex].Values[11].ToString());

                if (new BEnquiryIndiamart().UpdateEnquiryIndiamartStatus(lblQueryID.Text, 5, PSession.User.UserID, txtRejectEnquiryReason.Text.Trim()))
                {
                    lblMessage.Text = "Enquiry from India Mart rejected successfully.";
                    lblMessage.ForeColor = Color.Green;
                    MPE_RejectEnquiry.Hide();
                }
                else
                {
                    lblRejectEnquiryMessage.Text = "Enquiry from India Mart not rejected successfully!";
                    lblRejectEnquiryMessage.ForeColor = Color.Red;
                    lblRejectEnquiryMessage.Visible = true;
                    MPE_RejectEnquiry.Show();
                }
            }
            catch (Exception ex)
            {
                lblRejectEnquiryMessage.Text = ex.Message.ToString();
                lblRejectEnquiryMessage.ForeColor = Color.Red;
                lblRejectEnquiryMessage.Visible = true;
                MPE_RejectEnquiry.Show();
            }
        }
    }
}