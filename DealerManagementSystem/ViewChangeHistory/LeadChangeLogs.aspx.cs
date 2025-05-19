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
    public partial class LeadChangeLogs : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewChangeHistory_LeadChangeLogs; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Report » Lead Change Logs');</script>");
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                new BDMS_Dealer().LoadDealerDDL(ddlDealer);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? DealerID = null;
            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            }
            DataSet GetLeadChangeLogs = new BChangeLogs().GetLeadChangeLogs(txtLeadNumber.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), DealerID);

            gvLead.DataSource = GetLeadChangeLogs.Tables[0];
            gvLead.DataBind();

            gvLeadSalesEngineer.DataSource = GetLeadChangeLogs.Tables[1];
            gvLeadSalesEngineer.DataBind();

            gvLeadVisit.DataSource = GetLeadChangeLogs.Tables[2];
            gvLeadVisit.DataBind();

            gvLeadProduct.DataSource = GetLeadChangeLogs.Tables[3];
            gvLeadProduct.DataBind();

            gvLeadSupDoc.DataSource = GetLeadChangeLogs.Tables[4];
            gvLeadSupDoc.DataBind();

            gvLeadQuestionaries.DataSource = GetLeadChangeLogs.Tables[5];
            gvLeadQuestionaries.DataBind();
        }
    }
}