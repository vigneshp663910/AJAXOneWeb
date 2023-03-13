using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class AjaxEmployeeManage : BasePage
    {
       // public override SubModule SubModuleName { get { return SubModule.ViewMaster_AjaxEmployeeManage; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                fillDepartment();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Department = "";
            if (ddlDepartment.SelectedValue != "0")
            {
                Department = ddlDepartment.SelectedItem.Text;
            }
            gvEmployee.DataSource = new BEmployees().GetEmployeeListJohn(null, null, "", "", Department,null);
            gvEmployee.DataBind();
        }
        private void fillDepartment()
        {
            ddlDepartment.DataSource = new BEmployees().GetDepartment(null, null);
            ddlDepartment.DataBind();
        //    ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void btnAjaxEmpView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txtEId = (TextBox)gvRow.FindControl("txtEId");
            Label lblDealerEmployeeRoleID = (Label)gvRow.FindControl("lblDealerEmployeeRoleID");
            long? DealerEmployeeRoleID = string.IsNullOrEmpty(lblDealerEmployeeRoleID.Text) ? (long?)null : Convert.ToInt32(lblDealerEmployeeRoleID.Text);
            PEmployee emp = new BEmployees().GetEmployeeListJohn(Convert.ToInt32(txtEId.Text), null, null, "", "", DealerEmployeeRoleID)[0];
            MPE_AjaxEmp.Show();
            UC_Ajax.FillMaster();
            UC_Ajax.FillData(emp);
        }
    }
}