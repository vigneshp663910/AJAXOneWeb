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

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class EnquiryIndiamartView : System.Web.UI.UserControl
    {
        public string EnquiryIndiamartViewID
        {
            get
            {
                if (ViewState["EnquiryIndiamartViewEnquiryIndiamartID"] == null)
                {
                    ViewState["EnquiryIndiamartViewEnquiryIndiamartID"] = null;
                }
                return Convert.ToString(ViewState["EnquiryIndiamartViewEnquiryIndiamartID"]);
            }
            set
            {
                ViewState["EnquiryIndiamartViewEnquiryIndiamartID"] = value;
            }
        }
      
        public DataTable Enquiry
        {
            get
            {
                if (ViewState["EnquiryIndiamartView"] == null)
                {
                    ViewState["EnquiryIndiamartView"] = new DataTable();
                }
                return (DataTable)ViewState["EnquiryIndiamartView"];
            }
            set
            {
                ViewState["EnquiryIndiamartView"] = value;
            }
        }
        //public DataTable EnquiryStatusHistory
        //{
        //    get
        //    {
        //        if (Session["EnquiryIndiamartViewStatusHistory"] == null)
        //        {
        //            Session["EnquiryIndiamartViewStatusHistory"] = new DataTable();
        //        }
        //        return (DataTable)Session["EnquiryIndiamartViewStatusHistory"];
        //    }
        //    set
        //    {
        //        Session["EnquiryIndiamartViewStatusHistory"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblAddEnquiryMessage.Text = "";
            lblRejectEnquiryMessage.Text = "";
            lblInprogressEnquiryMessage.Text = "";
            //if (!string.IsNullOrEmpty(EnquiryIndiamartViewID))
            //{ 
            //    if (Convert.ToInt64(EnquiryIndiamartViewID) != Convert.ToInt64(Enquiry.Rows[0]["EnquiryIndiamartID"]))
            //    {
            //        Enquiry = new BEnquiry().GetEnquiryIndiamartByID(Convert.ToInt64(EnquiryIndiamartViewID));
            //    }
            //}
        }
        public void fillViewEnquiryIndiamart(long EnquiryIndiamartID)
        {
            EnquiryIndiamartViewID = Convert.ToString(EnquiryIndiamartID);
            Enquiry = new BEnquiry().GetEnquiryIndiamartByID(EnquiryIndiamartID);
            if (Enquiry.Rows.Count > 0)
            {
                lblVQueryID.Text = Enquiry.Rows[0]["Query ID"].ToString();
                lblQueryType.Text = Enquiry.Rows[0]["Query Type"].ToString();
                lblVStatus.Text = Enquiry.Rows[0]["Status"].ToString();
                lblSenderName.Text = Enquiry.Rows[0]["Sender Name"].ToString();
                lblSenderEmail.Text = "<a href='mailto:" + Enquiry.Rows[0]["Sender Email"].ToString() + "'>" + Enquiry.Rows[0]["Sender Email"].ToString() + "</a>";
                lblMobile.Text = "<a href='tel:" + Enquiry.Rows[0]["MOB"].ToString() + "'>" + Enquiry.Rows[0]["MOB"].ToString() + "</a>";
                lblCompanyName.Text = Enquiry.Rows[0]["Company Name"].ToString();
                lblAddress.Text = Enquiry.Rows[0]["Address"].ToString();
                lblCity.Text = Enquiry.Rows[0]["City"].ToString();
                lblState.Text = Enquiry.Rows[0]["State"].ToString();
                lblCountry.Text = Enquiry.Rows[0]["Country"].ToString();
                lblProductName.Text = Enquiry.Rows[0]["Product Name"].ToString();
                lblVMessage.Text = Enquiry.Rows[0]["Message"].ToString();
                lblDate.Text = Enquiry.Rows[0]["Date"].ToString();
                lblReceiverMob.Text = Enquiry.Rows[0]["Receiver Mob"].ToString();
                lblEmailAlt.Text = "<a href='mailto:" + Enquiry.Rows[0]["Email Alt"].ToString() + "'>" + Enquiry.Rows[0]["Email Alt"].ToString() + "</a>";
                lblMobileAlt.Text = "<a href='tel:" + Enquiry.Rows[0]["Mobile Alt"].ToString() + "'>" + Enquiry.Rows[0]["Mobile Alt"].ToString() + "</a>";
            }

            gvEnquiryPresaleStatus.DataSource = new BEnquiry().GetEnquiryIndiamartStatusHistory(EnquiryIndiamartID);
            gvEnquiryPresaleStatus.DataBind();
            //EnquiryStatusHistory = new BEnquiry().GetEnquiryIndiamartStatusHistory(EnquiryIndiamartID);
            //if (EnquiryStatusHistory.Rows.Count > 0)
            //{
            //    gvEnquiryPresaleStatus.DataSource = EnquiryStatusHistory;
            //    gvEnquiryPresaleStatus.DataBind();
            //}
            //else
            //{
            //    gvEnquiryPresaleStatus.DataSource = null;
            //    gvEnquiryPresaleStatus.DataBind();
            //}
            ActionControlMange();
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
        void ActionControlMange()
        {
                lbConverttoEnquiry.Visible = true;
                lbReject.Visible = true;
                lbInProgress.Visible = true;

                if (Enquiry.Rows[0]["Status"].ToString().Contains("Close") || Enquiry.Rows[0]["Status"].ToString().Contains("Rejected"))
                {
                    lbConverttoEnquiry.Visible = false;
                    lbReject.Visible = false;
                    lbInProgress.Visible = false;
                }
        }
        //protected void gvEnquiryPresaleStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvEnquiryPresaleStatus.PageIndex = e.NewPageIndex;
        //    gvEnquiryPresaleStatus.DataSource = EnquiryStatusHistory;
        //    gvEnquiryPresaleStatus.DataBind();
        //}        
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Convert to Enquiry")
            {
                long EnquiryIndiamartID = Convert.ToInt64(ViewState["EnquiryIndiamartID"]);
                MPE_AddEnquiry.Show();
                UC_AddEnquiry.FillMaster();
                Panel pnlAddEnquiry = new Panel();
                lblQueryIDAdd.Text = Enquiry.Rows[0]["Query ID"].ToString();
                if (Enquiry.Rows[0]["Company Name"].ToString() != "")
                {
                    ((TextBox)UC_AddEnquiry.FindControl("txtCustomerName")).Text = Enquiry.Rows[0]["Company Name"].ToString();
                }
                else
                {
                    ((TextBox)UC_AddEnquiry.FindControl("txtCustomerName")).Text = Enquiry.Rows[0]["Sender Name"].ToString();
                }
                ((TextBox)UC_AddEnquiry.FindControl("txtEnquiryDate")).Text = Enquiry.Rows[0]["Date"].ToString();
                ((TextBox)UC_AddEnquiry.FindControl("txtPersonName")).Text = Enquiry.Rows[0]["Sender Name"].ToString();
                ((TextBox)UC_AddEnquiry.FindControl("txtMobile")).Text = Enquiry.Rows[0]["MOB"].ToString().Replace("+91-", "");
                ((TextBox)UC_AddEnquiry.FindControl("txtMail")).Text = Enquiry.Rows[0]["Sender Email"].ToString();

                TextBox txtAddress = ((TextBox)UC_AddEnquiry.FindControl("txtAddress"));
                TextBox txtAddress2 = ((TextBox)UC_AddEnquiry.FindControl("txtAddress2"));
                TextBox txtAddress3 = ((TextBox)UC_AddEnquiry.FindControl("txtAddress3"));
                AddressSplit(Enquiry.Rows[0]["Address"].ToString(), txtAddress, txtAddress2, txtAddress3);

                List<PDMS_State> State = new BDMS_Address().GetState(null, 1, null, null, Enquiry.Rows[0]["State"].ToString());
                if (State.Count == 1)
                {
                    ((DropDownList)UC_AddEnquiry.FindControl("ddlState")).SelectedValue = State[0].StateID.ToString();
                }

                List<PDMS_District> District = new BDMS_Address().GetDistrict(1, null, State[0].StateID, null, Enquiry.Rows[0]["City"].ToString(), null);
                if (District.Count >= 1)
                {
                    ((DropDownList)UC_AddEnquiry.FindControl("ddlDistrict")).SelectedValue = District[0].DistrictID.ToString();
                }

                ((TextBox)UC_AddEnquiry.FindControl("txtProduct")).Text = Enquiry.Rows[0]["Product Name"].ToString();


                DropDownList ddlSource = ((DropDownList)UC_AddEnquiry.FindControl("ddlSource"));
                ddlSource.Enabled = true;
                if (Enquiry.Rows[0]["SourceID"] != DBNull.Value)
                {
                    ddlSource.SelectedValue = Enquiry.Rows[0]["SourceID"].ToString();
                    ddlSource.Enabled = false;
                }
            }
            else if (lbActions.Text == "Reject")
            {
                lblQueryID.Text = Enquiry.Rows[0]["Query ID"].ToString();
                new DDLBind(ddlRejectionRemarks, new BEnquiry().GetEnquiryRemark(null, null, null, true, null, null), "Remark", "EnquiryRemarkID");
                txtRejectEnquiryReason.Text = string.Empty;
                MPE_RejectEnquiry.Show();
                lblRejectEnquiryMessage.Text = string.Empty;
                lblRejectEnquiryMessage.Visible = false;
            }
            else if (lbActions.Text == "InProgress")
            {
                lblInProgressQueryID.Text = Enquiry.Rows[0]["Query ID"].ToString();
                new DDLBind(ddlInprogressRemarks, new BEnquiry().GetEnquiryRemark(null, null, true, null, null, null), "Remark", "EnquiryRemarkID");
                txtInprogressEnquiryReason.Text = string.Empty;
                MPE_InprogressEnquiry.Show();
                lblInprogressEnquiryMessage.Text = string.Empty;
                lblInprogressEnquiryMessage.Visible = false;
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
                    if (new BEnquiry().UpdateEnquiryIndiamartStatus(Convert.ToInt64(EnquiryIndiamartViewID), null, 2, PSession.User.UserID, enquiryAdd.Remarks.Trim()))
                    {
                        lblMessage.Text = "Enquiry from India Mart saved successfully.";
                        lblMessage.ForeColor = Color.Green;
                        fillViewEnquiryIndiamart(Convert.ToInt64(EnquiryIndiamartViewID));
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
                if (ddlRejectionRemarks.SelectedValue == "0")
                {
                    lblRejectEnquiryMessage.Text = "Please select the Remark...!";
                    lblRejectEnquiryMessage.ForeColor = Color.Red;
                    lblRejectEnquiryMessage.Visible = true;
                    MPE_RejectEnquiry.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtRejectEnquiryReason.Text.Trim()))
                {
                    lblRejectEnquiryMessage.Text = "Please enter the Reasons to reject the Enquiry.";
                    lblRejectEnquiryMessage.ForeColor = Color.Red;
                    lblRejectEnquiryMessage.Visible = true;
                    MPE_RejectEnquiry.Show();
                    return;
                }

                if (new BEnquiry().UpdateEnquiryIndiamartStatus(Convert.ToInt64(EnquiryIndiamartViewID), Convert.ToInt32(ddlRejectionRemarks.SelectedValue), 5, PSession.User.UserID, txtRejectEnquiryReason.Text.Trim()))
                {
                    lblMessage.Text = "Enquiry from India Mart rejected successfully.";
                    lblMessage.ForeColor = Color.Green;
                    fillViewEnquiryIndiamart(Convert.ToInt64(EnquiryIndiamartViewID));
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
        protected void btnInprogressEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlInprogressRemarks.SelectedValue == "0")
                {
                    lblInprogressEnquiryMessage.Text = "Please select the Remark...!";
                    lblInprogressEnquiryMessage.ForeColor = Color.Red;
                    lblInprogressEnquiryMessage.Visible = true;
                    MPE_InprogressEnquiry.Show();
                    return;
                }

                if (string.IsNullOrEmpty(txtInprogressEnquiryReason.Text.Trim()))
                {
                    lblInprogressEnquiryMessage.Text = "Please enter the Comments.";
                    lblInprogressEnquiryMessage.ForeColor = Color.Red;
                    lblInprogressEnquiryMessage.Visible = true;
                    MPE_InprogressEnquiry.Show();
                    return;
                }

                if (new BEnquiry().UpdateEnquiryIndiamartStatus(Convert.ToInt64(EnquiryIndiamartViewID), Convert.ToInt32(ddlInprogressRemarks.SelectedValue), 6, PSession.User.UserID, txtInprogressEnquiryReason.Text.Trim()))
                {
                    lblMessage.Text = "Enquiry India Mart Status is updated successfully.";
                    lblMessage.ForeColor = Color.Green;
                    fillViewEnquiryIndiamart(Convert.ToInt64(EnquiryIndiamartViewID));
                    MPE_InprogressEnquiry.Hide();
                }
                else
                {
                    lblInprogressEnquiryMessage.Text = "Enquiry India Mart Status was not updated successfully!";
                    lblInprogressEnquiryMessage.ForeColor = Color.Red;
                    lblInprogressEnquiryMessage.Visible = true;
                    MPE_InprogressEnquiry.Show();
                }
            }
            catch (Exception ex)
            {
                lblInprogressEnquiryMessage.Text = ex.Message.ToString();
                lblInprogressEnquiryMessage.ForeColor = Color.Red;
                lblInprogressEnquiryMessage.Visible = true;
                MPE_InprogressEnquiry.Show();
            }
        }
    }
}