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

namespace DealerManagementSystem.ViewMaster
{
    public partial class CustomerGstApproval : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_CustomerGSTApproval; } }
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
        public List<P_CustomerGSTApproval> CustomerGSTApprovalReport
        {
            get
            {
                if (ViewState["CustomerGSTApproval"] == null)
                {
                    ViewState["CustomerGSTApproval"] = new List<P_CustomerGSTApproval>();
                }
                return (List<P_CustomerGSTApproval>)ViewState["CustomerGSTApproval"];
            }
            set
            {
                ViewState["CustomerGSTApproval"] = value;
            }
        }
        public P_CustomerGSTApproval CustomerGSTApprovalByID
        {
            get
            {
                if (ViewState["CustomerGSTApprovalByID"] == null)
                {
                    ViewState["CustomerGSTApprovalByID"] = new P_CustomerGSTApproval();
                }
                return (P_CustomerGSTApproval)ViewState["CustomerGSTApprovalByID"];
            }
            set
            {
                ViewState["CustomerGSTApprovalByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Customer GST Approval');</script>");
            try
            {
                lblMessage.Text = "";
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    txtFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                    txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    FillGrid();
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
            try
            {
                string CustomerCode = null;
                DateTime? From = null, To = null;
                bool? IsApproved = null;
                if(ddlIsApproved.SelectedValue!="0")
                {
                    IsApproved = (ddlIsApproved.SelectedValue == "1") ? true : false;
                }
                if (!string.IsNullOrEmpty(txtFrom.Text))
                {
                    From = Convert.ToDateTime(txtFrom.Text);
                }
                if (!string.IsNullOrEmpty(txtTo.Text))
                {
                    To = Convert.ToDateTime(txtTo.Text);
                }
                CustomerCode = (string.IsNullOrEmpty(txtCustomerCode.Text)) ? null : txtCustomerCode.Text;
                
                PApiResult Result = new BDMS_Customer().GetCustomerGstApproval(CustomerCode, Convert.ToDateTime(From), Convert.ToDateTime(To), IsApproved, PageIndex, gvCustomerGSTApproval.PageSize);
                CustomerGSTApprovalReport = JsonConvert.DeserializeObject<List<P_CustomerGSTApproval>>(JsonConvert.SerializeObject(Result.Data));
                int RowCount = Result.RowCount;
                gvCustomerGSTApproval.DataSource = CustomerGSTApprovalReport;
                gvCustomerGSTApproval.DataBind();
                if (RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnCustArrowLeft.Visible = false;
                    ibtnCustArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvCustomerGSTApproval.PageSize - 1) / gvCustomerGSTApproval.PageSize;
                    lblRowCount.Visible = true;
                    ibtnCustArrowLeft.Visible = true;
                    ibtnCustArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvCustomerGSTApproval.PageSize) + 1) + " - " + (((PageIndex - 1) * gvCustomerGSTApproval.PageSize) + gvCustomerGSTApproval.Rows.Count) + " of " + RowCount;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void gvCustomerGSTApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCustomerGSTApproval.PageIndex = e.NewPageIndex;
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillGrid();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillGrid();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Button btnView = (sender as Button);
            divView.Visible = true;
            divList.Visible = false;
            long CustomerGSTApprovalID = Convert.ToInt64(btnView.CommandArgument);
            GetCustomerGstApprovalByID(CustomerGSTApprovalID);            
        }
        void GetCustomerGstApprovalByID(long CustomerGSTApprovalID)
        {
            PApiResult Result = new BDMS_Customer().GetCustomerGstApprovalByID(CustomerGSTApprovalID);
            CustomerGSTApprovalByID = JsonConvert.DeserializeObject<P_CustomerGSTApproval>(JsonConvert.SerializeObject(Result.Data));

            if (CustomerGSTApprovalByID != null)
            {
                lblCustomerName.Text = CustomerGSTApprovalByID.CustomerName;
                lblSendSAP.Text = CustomerGSTApprovalByID.SendSAP.ToString();
                lblSuccess.Text = CustomerGSTApprovalByID.Success.ToString();
                lblUnregistered.Text = (CustomerGSTApprovalByID.Unregistered == true) ? CustomerGSTApprovalByID.Unregistered.ToString() : false.ToString();
                lblGSTIN.Text = CustomerGSTApprovalByID.GSTIN;
                lblPAN.Text = CustomerGSTApprovalByID.PAN;
                lblIsApproved.Text = (CustomerGSTApprovalByID.IsApproved == true) ? CustomerGSTApprovalByID.IsApproved.ToString() : false.ToString();
                lblApprovedBy.Text = (CustomerGSTApprovalByID.ApprovedBy == null) ? "" : CustomerGSTApprovalByID.ApprovedBy.ContactName;
                lblApprovedOn.Text = CustomerGSTApprovalByID.ApprovedOn.ToString();
                lblCreatedBy.Text = (CustomerGSTApprovalByID.CreatedBy == null) ? "" : CustomerGSTApprovalByID.CreatedBy.ContactName;
                lblCreatedOn.Text = CustomerGSTApprovalByID.CreatedOn.ToString();
            }
            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(CustomerGSTApprovalByID.CustomerID);
            UC_CustomerView.fillCustomer(Customer);
            ActionControlMange();
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divView.Visible = false;
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.Text == "Approve")
                {
                    btnApproveCustomerGST.Text = "Approve";
                    MPE_ApproveCustomerGST.Show();
                }
                if (lbActions.Text == "Reject")
                {
                    btnApproveCustomerGST.Text = "Reject";
                    MPE_ApproveCustomerGST.Show();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnApproveCustomerGST_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageApproveCustomerGST.Visible = true;
                lblMessageApproveCustomerGST.Text = "";
                lblMessageApproveCustomerGST.ForeColor = Color.Red;
                lblMessage.Visible = true;
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtApproverRemarks.Text))
                {
                    lblMessageApproveCustomerGST.Text = "Please Enter Remarks...!";
                    return;
                }
                P_CustomerGSTApproval Gst = new P_CustomerGSTApproval();
                Gst.CustomerGSTApprovalID = CustomerGSTApprovalByID.CustomerGSTApprovalID;
                if (btnApproveCustomerGST.Text == "Approve")
                {
                    Gst.IsApproved = true;
                }
                Gst.ApproverRemark = txtApproverRemarks.Text.Trim();
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/ApproveOrRejectCustomerGstApproval", Gst));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageApproveCustomerGST.Text = Results.Message;
                    return;
                }

                PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(CustomerGSTApprovalByID.CustomerID);
                if (!Customer.IsVerified)
                {                    
                    long C = new BDMS_Customer().UpdateCustomerCodeFromSapToSql(Customer, false);
                    if (C != 0)
                    {
                        lblMessage.Text = "Updated successfully";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong try again.";
                        lblMessage.ForeColor = Color.Red;
                    }
                }

                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Green;
                FillGrid();
                GetCustomerGstApprovalByID(CustomerGSTApprovalByID.CustomerGSTApprovalID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        void ActionControlMange()
        {
            lbApproveCustomerGST.Visible = true;
            lbRejectCustomerGST.Visible = true;

            if (CustomerGSTApprovalByID.IsApproved != null)
            {
                lbApproveCustomerGST.Visible = false;
                lbRejectCustomerGST.Visible = false;
            }
        }
    }
}