using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class OnboardEmployeeApprove : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDealerEmployee_OnboardEmployeeManage; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public string EmpCode
        {
            get
            {
                return txtEmpCode.Text.Trim().Replace("-", "");
            }
        }
        int? DepartmentID = null;
        int? DesignationID = null;
        string Name = null;
        public List<POnboardEmployee> OnboardEmployeePendingApproval
        {
            get
            {
                if (Session["OnboardEmployeePendingApproval"] == null)
                {
                    Session["OnboardEmployeePendingApproval"] = new List<POnboardEmployee>();
                }
                return (List<POnboardEmployee>)Session["OnboardEmployeePendingApproval"];
            }
            set
            {
                Session["OnboardEmployeePendingApproval"] = value;
            }
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Onboard Employee Approval');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);

                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageCount = 0;
                PageIndex = 1;
                FillGrid();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
            }
        }
        void Search()
        {
            Name = string.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
            DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
        }
        private void FillGrid()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BOnboardEmployee().GetOnboardEmployeePendingApproval(EmpCode, Name, DepartmentID, DesignationID, PageIndex, gvEmployee.PageSize);
                OnboardEmployeePendingApproval = JsonConvert.DeserializeObject<List<POnboardEmployee>>(JsonConvert.SerializeObject(Result.Data));
                gvEmployee.DataSource = OnboardEmployeePendingApproval;
                gvEmployee.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvEmployee.PageSize - 1) / gvEmployee.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvEmployee.PageSize) + 1) + " - " + (((PageIndex - 1) * gvEmployee.PageSize) + gvEmployee.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
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
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }

        protected void lbView_Click(object sender, EventArgs e)
        {
            LinkButton lbView = (LinkButton)sender;
            divList.Visible = false;
            divOnboardEmployeeView.Visible = true;
            UC_OnboardEmployeeView.FillOnboardEmployee(Convert.ToInt32(lbView.CommandArgument));
            UC_OnboardEmployeeView.FindControl("DivApprover").Visible = true;
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divOnboardEmployeeView.Visible = false;
        }
    }
}