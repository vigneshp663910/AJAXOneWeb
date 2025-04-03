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

namespace DealerManagementSystem.ViewChangeHistory
{
    public partial class CustomerChangeLogs : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_CustomerChangeLogs; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » Customer Change Logs');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                lblMessage.Text = "Please enter dealer code";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            DataSet GetCustomerChangeLogs = new BChangeLogs().GetCustomerChangeLogs(txtCustomerCode.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

            gvCustomerLogs.DataSource = GetCustomerChangeLogs.Tables[0];
            gvCustomerLogs.DataBind();

            gvCustomerAttributeLogs.DataSource = GetCustomerChangeLogs.Tables[1];
            gvCustomerAttributeLogs.DataBind();

            gvCustomerProductLogs.DataSource = GetCustomerChangeLogs.Tables[2];
            gvCustomerProductLogs.DataBind();

            gvCustomerTeamLogs.DataSource = GetCustomerChangeLogs.Tables[3];
            gvCustomerTeamLogs.DataBind();

            gvCustomerResponsibleEmployeeLogs.DataSource = GetCustomerChangeLogs.Tables[4];
            gvCustomerResponsibleEmployeeLogs.DataBind();

            gvCustomerGroupOfCompaniesLogs.DataSource = GetCustomerChangeLogs.Tables[5];
            gvCustomerGroupOfCompaniesLogs.DataBind();

            gvCusSupDoc.DataSource = GetCustomerChangeLogs.Tables[6];
            gvCusSupDoc.DataBind();

            gvCusShiptoPar.DataSource = GetCustomerChangeLogs.Tables[7];
            gvCusShiptoPar.DataBind();
        }
    }
}