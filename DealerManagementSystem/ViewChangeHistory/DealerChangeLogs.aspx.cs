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
    public partial class DealerChangeLogs : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_DealerChangeLogs; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » Dealer Change Logs');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        } 
        protected void btnSearch_Click(object sender, EventArgs e)
        {  
            if (string.IsNullOrEmpty(txtDealerCode.Text.Trim()))
            {
                lblMessage.Text = "Please enter dealer code";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            DataSet GetDealerChangeLogs = new BChangeLogs().GetDealerChangeLogs(txtDealerCode.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());

            gvDealerLogs.DataSource = GetDealerChangeLogs.Tables[0];
            gvDealerLogs.DataBind();

            gvDealerAddressLogs.DataSource = GetDealerChangeLogs.Tables[1];
            gvDealerAddressLogs.DataBind();

            gvDealerOfficeLogs.DataSource = GetDealerChangeLogs.Tables[2];
            gvDealerOfficeLogs.DataBind();

            gvDealerBankLogs.DataSource = GetDealerChangeLogs.Tables[3];
            gvDealerBankLogs.DataBind();

            gvDealerNotificationLogs.DataSource = GetDealerChangeLogs.Tables[4];
            gvDealerNotificationLogs.DataBind();
        }
    }
}