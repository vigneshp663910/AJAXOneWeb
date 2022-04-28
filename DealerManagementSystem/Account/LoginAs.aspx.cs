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

        public List<PUser> UserLst
        {
            get
            {
                if (Session["PUser"] == null)
                {
                    Session["PUser"] = new List<PUser>();
                }
                return (List<PUser>)Session["PUser"];
            }
            set
            {
                Session["PUser"] = value;
            }
        }

        protected void ibtnUserArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEmployee.PageIndex > 0)
            {
                gvEmployee.PageIndex = gvEmployee.PageIndex - 1;
                UserBind(gvEmployee, lblRowCount, UserLst);
            }
        }
        protected void ibtnUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEmployee.PageCount > gvEmployee.PageIndex)
            {
                gvEmployee.PageIndex = gvEmployee.PageIndex + 1;
                UserBind(gvEmployee, lblRowCount, UserLst);
            }
        }

        void UserBind(GridView gv, Label lbl, List<PUser> UserLst)
        {
            gv.DataSource = UserLst;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + UserLst.Count;
        }


        void FillUser()
        {
            bool? IsEnabled = null;
            if (ddlIsEnabled.SelectedValue == "1") { IsEnabled = true; } else if (ddlIsEnabled.SelectedValue == "2") { IsEnabled = false; }
            //List<PUser> u = new BUser().GetUsers(null, txtEmp.Text, null, "", null, IsEnabled,null);
            ////u = u.FindAll(m => m.SystemCategoryID == (short)SystemCategory.Dealer && m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));
            //u = u.FindAll(m => m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));

            UserLst = new BUser().GetUsers(null, txtEmp.Text, null, "", null, IsEnabled, null, null, null);
            UserLst = UserLst.FindAll(m => m.ContactName.ToLower().Contains(txtContactName.Text.Trim().ToLower()));
            gvEmployee.DataSource = UserLst;

            //gvEmployee.DataSource = u;
            gvEmployee.DataBind();


            if (UserLst.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnUserArrowLeft.Visible = false;
                ibtnUserArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnUserArrowLeft.Visible = true;
                ibtnUserArrowRight.Visible = true;
                lblRowCount.Text = (((gvEmployee.PageIndex) * gvEmployee.PageSize) + 1) + " - " + (((gvEmployee.PageIndex) * gvEmployee.PageSize) + gvEmployee.Rows.Count) + " of " + UserLst.Count;
            }

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
            PSession.User = new BUser().GetUserByToken();
            // UIHelper.UserAudit();
            Response.Redirect(UIHelper.RedirectToHomePage);

            //AddToSession(UserID);
            //Response.Redirect(UIHelper.RedirectToHomePage);
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