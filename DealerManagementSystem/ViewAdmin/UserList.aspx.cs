using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserList : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_UserList; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » User List');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {

                FillUser();
            }
        }
        void FillUser()
        {

            List<PUser> u = new BUser().GetUsers(null, txtEmp.Text, null, "", null,null,null, null, null);
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
             
            PApiResult Results = new BUser().GetTokenByID(UserID);

            if (Results.Status == PApplication.Failure)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = Results.Message;
                lblMessage.Visible = true;
                return;
            }
            PSession.AccessToken = Convert.ToString(Results.Data);

            PApiResult ResultToken = new BUser().GetUserByToken();
            if (ResultToken.Status == PApplication.Failure)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = Results.Message;
                lblMessage.Visible = true;
                return;
            }
            PSession.User = JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(ResultToken.Data));
            // UIHelper.UserAudit();
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
        //private void AddToSession(long userId)
        //{
        //    PSession.User = new BUser().GetUserDetails(userId);
        //    PSession.UserId = userId;
        //    PSession.User.Dealer = new BDealer().GetDealerByUserID(userId);
        //    PSession.User.DMSModules = new BUser().GetDMSModuleByUser(userId, null, null);
        //    UIHelper.UserAudit();
        //}

    }
}