using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class AjaxEmployeeManage : System.Web.UI.Page
    {
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
            gvEmployee.DataSource = new BEmployees().GetEmployeeListJohn(null, null, "", "", Department);
            gvEmployee.DataBind();
        }
        private void fillDepartment()
        {
            ddlDepartment.DataSource = new BEmployees().GetDepartment(null, null);
            ddlDepartment.DataBind();
        //    ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}