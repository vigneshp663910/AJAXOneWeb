using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class MachineOperatorManage : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDealerEmployee_MachineOperatorManage; } }
        public string AadhaarCardNo
        {
            get
            {
                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }
        public List<PMachineOperator> MachineOperator
        {
            get
            {
                if (Session["MachineOperatorManage"] == null)
                {
                    Session["MachineOperatorManage"] = new List<PMachineOperator>();
                }
                return (List<PMachineOperator>)Session["MachineOperatorManage"];
            }
            set
            {
                Session["MachineOperatorManage"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Machine Operator » Manage');</script>");
            if (!IsPostBack)
            {
                new BDMS_Address().GetStateDDL(ddlState, null, null, null, null);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
            }
        }
        private void FillMachineOperator()
        {
            //int? StateID = null;
            //Boolean? StatusID = null;
            //if (ddlStatus.SelectedValue != "-1")
            //{
            //    StatusID = Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue));
            //}
            string SAadhaarCardNo = null, DLNumber = null, Name = null;
            if (!string.IsNullOrEmpty(AadhaarCardNo))
            {
                SAadhaarCardNo = AadhaarCardNo;
            }
            if (!string.IsNullOrEmpty(txtDLNumber.Text))
            {
                DLNumber = txtDLNumber.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Name = txtName.Text.Trim();
            }
            int? DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            int? DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            MachineOperator = new BMachineOperator().GetMachineOperatorDetailsManage(SAadhaarCardNo, DLNumber, Name, DepartmentID, DesignationID);
            gvMachineOperator.DataSource = MachineOperator;
            gvMachineOperatorDataBind();
        }
        void gvMachineOperatorDataBind()
        {
            gvMachineOperator.DataBind();
            lblRowCount.Text = (((gvMachineOperator.PageIndex) * gvMachineOperator.PageSize) + 1) + " - " + (((gvMachineOperator.PageIndex) * gvMachineOperator.PageSize) + gvMachineOperator.Rows.Count) + " of " + MachineOperator.Count;
            Boolean EditAlloved = false;
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.OperatorEdit).Count() != 0)
            {
                EditAlloved = true;
            }
            for (int i = 0; i < gvMachineOperator.Rows.Count; i++)
            {
                Label lblCreatedByID = (Label)gvMachineOperator.Rows[i].FindControl("lblCreatedByID");
                CheckBox cbIsAjaxHPApproved = (CheckBox)gvMachineOperator.Rows[i].FindControl("cbIsAjaxHPApproved");
                LinkButton lbEdit = (LinkButton)gvMachineOperator.Rows[i].FindControl("lbEdit");
                if (cbIsAjaxHPApproved.Checked)
                {
                    lbEdit.Visible = false;
                }
                if (EditAlloved == true)
                {
                    lbEdit.Visible = true;
                }
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMachineOperator.PageIndex > 0)
            {
                gvMachineOperator.DataSource = MachineOperator;
                gvMachineOperator.PageIndex = gvMachineOperator.PageIndex - 1;
                gvMachineOperatorDataBind();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMachineOperator.PageCount > gvMachineOperator.PageIndex)
            {
                gvMachineOperator.DataSource = MachineOperator;
                gvMachineOperator.PageIndex = gvMachineOperator.PageIndex + 1;
                gvMachineOperatorDataBind();
            }
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "";
            url = "MachineOperatorRegister.aspx?MO_MachineOperatorDetailsID=" + gvMachineOperator.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }
        protected void lbView_Click(object sender, EventArgs e)
        {
            divMachineOperatorView.Visible = true;
            btnBackToList.Visible = true;
            divMachineOperatorList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            //string url = "MachineOperatorView.aspx?MachineOperatorID=" + gvMachineOperator.DataKeys[index].Value.ToString();
            //Response.Redirect(url);
            UC_MachineOperatorView.FillMachineOperator(Convert.ToInt64(gvMachineOperator.DataKeys[index].Value.ToString()));
        }
        protected void gvMachineOperator_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMachineOperator.PageIndex = e.NewPageIndex;
            gvMachineOperator.DataSource = MachineOperator;
            gvMachineOperatorDataBind();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillMachineOperator();
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divMachineOperatorView.Visible = false;
            btnBackToList.Visible = false;
            divMachineOperatorList.Visible = true;
        }
    }
}