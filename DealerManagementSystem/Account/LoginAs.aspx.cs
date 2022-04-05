using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.Account
{
    public partial class LoginAs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Login As');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {

                FillUser();
            }
        }
        void FillUser()
        {

            List<PUser> u = new BUser().GetUsers(null, txtEmp.Text, null, "", null,null);
            //u = u.FindAll(m => m.SystemCategoryID == (short)SystemCategory.Dealer && m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));
            u = u.FindAll(m => m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));
            gvEmployee.DataSource = u;
            gvEmployee.DataBind();
        }
        protected void lbEmpId_Click(object sender, EventArgs e)
        {


            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            int UserID = Convert.ToInt32(((TextBox)gvEmployee.Rows[index].FindControl("txtUserID")).Text);
         
            AddToSession(UserID);
            Response.Redirect(UIHelper.RedirectToHomePage);
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillUser();

        }
        protected void gvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployee.PageIndex = e.NewPageIndex;
            FillUser();

        }
        private void AddToSession(long userId)
        {
            PSession.User = new BUser().GetUserDetails(userId);
            PSession.UserId = userId;
            PSession.User.Dealer = new BDealer().GetDealerByUserID(userId);
            PSession.User.DMSModules = new BUser().GetDMSModuleByUser(userId, null, null);
            UIHelper.UserAudit();
        }
    }
}