using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class AjaxEmployeeCreate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {
            new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);

            int DealerID = Convert.ToInt32(ConfigurationManager.AppSettings["AjaxDealerID"]);
            List<PDMS_DealerEmployee> Employee = new BDMS_Dealer().GetDealerEmployeeByDealerID(DealerID, null, null, null, null);
            ddlReportingTo.DataValueField = "DealerEmployeeID";
            ddlReportingTo.DataTextField = "Name";
            ddlReportingTo.DataSource = Employee;
            ddlReportingTo.DataBind();
            ddlReportingTo.Items.Insert(0, new ListItem("Select", "0"));

            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        Boolean ValidationRoal()
        {
            //lblMessage.ForeColor = Color.Red;
           // lblMessage.Visible = true; 
            ddlDealerOffice.BorderColor = Color.Silver;
            ddlDepartment.BorderColor = Color.Silver;
            ddlDesignation.BorderColor = Color.Silver;
            Boolean Ret = true;
            string Message = "";
             
            if (ddlDealerOffice.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Dealer Office";
                Ret = false;
                ddlDealerOffice.BorderColor = Color.Red;
            }
            if (ddlDepartment.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Department";
                Ret = false;
                ddlDepartment.BorderColor = Color.Red;
            }
            if (ddlDesignation.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Designation";
                Ret = false;
                ddlDesignation.BorderColor = Color.Red;
            }
           
            

          //  lblMessage.Text = Message;
            if (!Ret)
            {
                return Ret;
            }

            

           // lblMessage.Text = Message;
            return Ret;
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        void SaveAjaxEmp()
        {
            if (!ValidationRoal())
            {
                return;
            }

            long? RoleID = string.IsNullOrEmpty(lblDealerEmployeeRoleID.Text) ? (long?)null : Convert.ToInt64(lblDealerEmployeeRoleID.Text);
            int? ReportingTo = ddlReportingTo.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlReportingTo.SelectedValue);
            int DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            int DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            Boolean S = new BEmployees().InsertOrUpdateAjaxEmp(Convert.ToInt32(lblEID.Text), txtUserName.Text.Trim(), RoleID, ReportingTo, DepartmentID, DesignationID, PSession.User.UserID);

            //  lblMessage.Text = "Employee is updated successfully";
            // lblMessage.ForeColor = Color.Green;
            //  btnSave.Visible = false;

        }

        public void FillData(PEmployee Emp)
        {
            lblEID.Text = Emp.EmpId.ToString();
            if (Emp.DmsEmp.DealerEmployeeRole != null)
            {
                lblDealerEmployeeRoleID.Text = Convert.ToString(Emp.DmsEmp.DealerEmployeeRole.DealerEmployeeRoleID);
            }
            lblName.Text = Emp.EmployeeName;
            lblSAPEmpCode.Text = Emp.EmpId.ToString();

            //txtUserName.Text = Emp.DmsEmp.LoginUserName;
            if (Emp.DmsEmp.DealerEmployeeRole != null)
            {
                if (Emp.DmsEmp.DealerEmployeeRole.DealerOffice != null)
            {
                ddlDealerOffice.SelectedValue = Emp.DmsEmp.DealerEmployeeRole.DealerOffice.OfficeID.ToString();
            }
           
                if (Emp.DmsEmp.DealerEmployeeRole.DealerDepartment != null)
                {
                    ddlDepartment.SelectedValue = Emp.DmsEmp.DealerEmployeeRole.DealerDepartment.DealerDepartmentID.ToString();
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
                }
                if (Emp.DmsEmp.DealerEmployeeRole.DealerDesignation != null)
                {
                    ddlDesignation.SelectedValue = Emp.DmsEmp.DealerEmployeeRole.DealerDesignation.DealerDesignationID.ToString();
                }
                if (Emp.DmsEmp.DealerEmployeeRole.ReportingTo != null)
                {
                    ddlReportingTo.SelectedValue = Emp.DmsEmp.DealerEmployeeRole.ReportingTo.DealerEmployeeID.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveAjaxEmp();
        }
    }
}