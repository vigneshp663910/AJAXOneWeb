using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public List<PCustomerGSTApproval> CustomerGSTApprovalReport
        {
            get
            {
                if (ViewState["CustomerGSTApproval"] == null)
                {
                    ViewState["CustomerGSTApproval"] = new List<PCustomerGSTApproval>();
                }
                return (List<PCustomerGSTApproval>)ViewState["CustomerGSTApproval"];
            }
            set
            {
                ViewState["CustomerGSTApproval"] = value;
            }
        }
        public PCustomerGSTApproval CustomerGSTApprovalByID
        {
            get
            {
                if (ViewState["CustomerGSTApprovalByID"] == null)
                {
                    ViewState["CustomerGSTApprovalByID"] = new PCustomerGSTApproval();
                }
                return (PCustomerGSTApproval)ViewState["CustomerGSTApprovalByID"];
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
                string From = null, To = null;
                bool? IsApproved = null;
                if(ddlIsApproved.SelectedValue!="0")
                {
                    IsApproved = (ddlIsApproved.SelectedValue == "1") ? true : (ddlIsApproved.SelectedValue == "2") ? false : (bool?)null;
                }
                if (!string.IsNullOrEmpty(txtFrom.Text))
                {
                    From = Convert.ToDateTime(txtFrom.Text.Trim()).ToString();
                }
                if (!string.IsNullOrEmpty(txtTo.Text))
                {
                    To = Convert.ToDateTime(txtTo.Text.Trim()).ToString();
                }
                CustomerCode = (string.IsNullOrEmpty(txtCustomerCode.Text)) ? null : txtCustomerCode.Text;
                //int RowCount = 0;
                //CustomerGSTApprovalReport = new BDMS_Customer().GetCustomerGstApproval(CustomerCode, Convert.ToDateTime(From), Convert.ToDateTime(To), IsApproved, PageIndex, gvCustomerGSTApproval.PageSize, out RowCount);
                PApiResult Result = new BDMS_Customer().GetCustomerGstApproval(CustomerCode, From, To, IsApproved, PageIndex, gvCustomerGSTApproval.PageSize);
                CustomerGSTApprovalReport = JsonConvert.DeserializeObject<List<PCustomerGSTApproval>>(JsonConvert.SerializeObject(Result.Data));
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
            CustomerGSTApprovalByID = JsonConvert.DeserializeObject<PCustomerGSTApproval>(JsonConvert.SerializeObject(Result.Data));

            if (CustomerGSTApprovalByID != null)
            {
                lblCustomerName.Text = CustomerGSTApprovalByID.CustomerName;
                lblOldGst.Text = CustomerGSTApprovalByID.OldGSTIN.ToString();
                lblOldPan.Text = CustomerGSTApprovalByID.OldPAN.ToString();
                lblGSTIN.Text = CustomerGSTApprovalByID.GSTIN;
                lblPAN.Text = CustomerGSTApprovalByID.PAN;
                lblIsApproved.Text = (CustomerGSTApprovalByID.IsApproved == true) ? "Approved" : (CustomerGSTApprovalByID.IsApproved == false) ? "Rejected" : "";
                lblApprovedBy.Text = (CustomerGSTApprovalByID.ApprovedBy == null) ? "" : CustomerGSTApprovalByID.ApprovedBy.ContactName;
                lblApprovedOn.Text = CustomerGSTApprovalByID.ApprovedOn.ToString();
                lblApprovedRemark.Text = CustomerGSTApprovalByID.ApproverRemark;
                lblCreatedBy.Text = (CustomerGSTApprovalByID.CreatedBy == null) ? "" : CustomerGSTApprovalByID.CreatedBy.ContactName;
                lblCreatedOn.Text = CustomerGSTApprovalByID.CreatedOn.ToString();
            }
            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(CustomerGSTApprovalByID.CustomerID);
            UC_CustomerView.fillCustomer(Customer);
            fillSupportDocument();
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
                    MPE_ApproveCustomerGST.Show();
                    return;
                }
                PCustomerGSTApproval Gst = new PCustomerGSTApproval();
                Gst.CustomerGSTApprovalID = CustomerGSTApprovalByID.CustomerGSTApprovalID;
                Gst.CustomerID = CustomerGSTApprovalByID.CustomerID;
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
        protected void lbSupportDocumentDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
                Label lblFileName = (Label)gvRow.FindControl("lblFileName");
                Label lblFileType = (Label)gvRow.FindControl("lblFileType");

                PAttachedFile UploadedFile = new BDMS_Customer().GetAttachedFileCustomerForDownload(AttachedFileID + Path.GetExtension(lblFileName.Text));

                Response.AddHeader("Content-type", lblFileType.Text);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblFileName.Text);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }
        }
        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BDMS_Customer().GetAttachedFileCustomer(CustomerGSTApprovalByID.CustomerID);
            gvSupportDocument.DataBind();
        }
    }
}