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
    public partial class AjaxrEmployeeCreate : System.Web.UI.UserControl
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
            PDMS_DealerEmployee Emp = new PDMS_DealerEmployee();
                

            int DealerEmployeeID = new BDMS_Dealer().InsertOrUpdateDealerEmployee(Emp, PSession.User.UserID);

            PDMS_DealerEmployeeRole Role = new PDMS_DealerEmployeeRole();
            Role.DealerEmployeeID = DealerEmployeeID;
            Role.Dealer = new PDMS_Dealer();
            Role.Dealer.DealerID = Convert.ToInt32(ConfigurationManager.AppSettings["AjaxDealerID"]);
            Role.Dealer.DealerOffice = new PDMS_DealerOffice();
            Role.Dealer.DealerOffice.OfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);          
            Role.DealerDepartment = new PDMS_DealerDepartment();
            Role.DealerDepartment.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            Role.DealerDesignation = new PDMS_DealerDesignation();
            Role.DealerDesignation.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);



            if (DealerEmployeeID != 0)
            {
                new BDMS_Dealer().ApproveDealerEmployee(DealerEmployeeID, PSession.User.UserID);
                new BDMS_Dealer().InsertDealerEmployeeRole(Role, PSession.User.UserID);
              //  lblMessage.Text = "Employee is updated successfully";
               // lblMessage.ForeColor = Color.Green;
              //  btnSave.Visible = false;
            }
            else
            {
               // lblMessage.Text = "Employee is not updated successfully";
            }

          //  btnSave.Focus();
        }

    }
}