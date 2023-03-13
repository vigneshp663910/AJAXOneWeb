using Business;
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
    public partial class CustomerChangeForApproval : BasePage
    {
       // public override SubModule SubModuleName { get { return SubModule.ViewMaster_CustomerChangeForApproval; } }
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
        public List<PDMS_CustomerChangeForApproval> Customers
        {
            get
            {
                if (ViewState["CustomerChangeForApproval"] == null)
                {
                    ViewState["CustomerChangeForApproval"] = new List<PDMS_CustomerChangeForApproval>();
                }
                return (List<PDMS_CustomerChangeForApproval>)ViewState["CustomerChangeForApproval"];
            }
            set
            {
                ViewState["CustomerChangeForApproval"] = value;
            }
        }
        public PDMS_CustomerChangeForApproval Customer
        {
            get
            {
                if (ViewState["CustomerChangeForApprovalByID"] == null)
                {
                    ViewState["CustomerChangeForApprovalByID"] = new PDMS_CustomerChangeForApproval();
                }
                return (PDMS_CustomerChangeForApproval)ViewState["CustomerChangeForApprovalByID"];
            }
            set
            {
                ViewState["CustomerChangeForApprovalByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Customer Change For Approval');</script>");
            try
            {
                lblMessage.Text = "";
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
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
                CustomerCode = (string.IsNullOrEmpty(txtCustomerCode.Text)) ? null : txtCustomerCode.Text;
                int RowCount = 0;
                Customers = new BDMS_Customer().GetCustomerChangeForApproval(CustomerCode, PageIndex, gvCustomerChangeForApproval.PageSize, out RowCount);

                gvCustomerChangeForApproval.DataSource = Customers;
                gvCustomerChangeForApproval.DataBind();
                if (RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnCustArrowLeft.Visible = false;
                    ibtnCustArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvCustomerChangeForApproval.PageSize - 1) / gvCustomerChangeForApproval.PageSize;
                    lblRowCount.Visible = true;
                    ibtnCustArrowLeft.Visible = true;
                    ibtnCustArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvCustomerChangeForApproval.PageSize) + 1) + " - " + (((PageIndex - 1) * gvCustomerChangeForApproval.PageSize) + gvCustomerChangeForApproval.Rows.Count) + " of " + RowCount;
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

        protected void gvCustomerChangeForApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCustomerChangeForApproval.PageIndex = e.NewPageIndex;
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
            long CustomerChangeForApprovalID = Convert.ToInt64(btnView.CommandArgument);
            Customer = new BDMS_Customer().GetCustomerChangeForApprovalByID(CustomerChangeForApprovalID);
            if(Customer!=null)
            {
                lblCustomerName.Text = Customer.CustomerName;
                lblSendSAP.Text = Customer.SendSAP.ToString();
                lblSuccess.Text = Customer.Success.ToString();
                lblUnregistered.Text = (Customer.Unregistered==true)?Customer.Unregistered.ToString():false.ToString();
                lblGSTIN.Text = Customer.GSTIN;
                lblPAN.Text = Customer.PAN;
                lblIsApproved.Text = (Customer.IsApproved==true)?Customer.IsApproved.ToString():false.ToString();
                lblApprovedBy.Text = (Customer.ApprovedBy==null)?"":Customer.ApprovedBy.ContactName;
                lblApprovedOn.Text = Customer.ApprovedOn.ToString();
                lblCreatedBy.Text = (Customer.CreatedBy==null)?"":Customer.CreatedBy.ContactName;
                lblCreatedOn.Text = Customer.CreatedOn.ToString();
            }
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divView.Visible = false;
        }
    }
}