using Business;
using Properties;
using System;
using System.Drawing;
using System.Web.UI;

namespace DealerManagementSystem.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Change Password');</script>");
        }

        protected void btnChangePw_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCurrentPassword.Text))
                {
                    lblMessage.Text = "Please Enter Old Password...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (txtNewPassword.Text.ToUpper().Contains("AJAX@123"))
                {
                    lblMessage.Text = "Please Provide Another Password...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (PSession.User != null)
                {
                    if (new BUser().ChangePassword(PSession.User.UserID, txtCurrentPassword.Text.Trim(), txtNewPassword.Text.Trim(), txtReTypeNewPassword.Text, "Change") == 1)
                    {
                        AddToSession(PSession.User.UserID);
                        lblMessage.Text = "Your Password is changed successfully, please use the new password when you login next time...!";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Green;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Your Password is not changed successfully, please try again...!";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception e1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('" + e1.Message + "');", true);

            }
        }
        private void AddToSession(long userId)
        {
            PSession.User = new BUser().GetUserDetails(userId);
           // PSession.UserId = userId;
            PSession.User.Dealer = new BDealer().GetDealerByUserID(userId);
            PSession.User.DMSModules = new BUser().GetDMSModuleByUser(userId, null, null);
            UIHelper.UserAudit(hfLatitude.Value, hfLongitude.Value);
        }
    }
}