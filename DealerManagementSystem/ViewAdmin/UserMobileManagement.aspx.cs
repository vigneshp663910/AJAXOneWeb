using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserMobileManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Manage Mobile User');</script>");

            if (!IsPostBack)
            {
                fillDealer();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            fillUser();
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            fillUser();
        }
        void fillUser()
        {
            try
            {
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); 
                gvUser.DataSource = new BUser().GetUserMobileManage(DealerID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
                gvUser.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();

            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}