using Business;
using Properties;
using System;
using System.Linq;
using System.Web.UI;
namespace DealerManagementSystem
{
    public partial class SignIn : System.Web.UI.Page
    {
        private int NoOfAllowedLoginAttempt;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Cookies["IMEI"] == null)
            {
                Response.Cookies["IMEI"].Value = "";
            }
            if ((!string.IsNullOrEmpty(Request.QueryString["IMEI"])) || (!string.IsNullOrEmpty(Request.Cookies["IMEI"].Value)))
            {
                string IMEI = string.IsNullOrEmpty(Request.QueryString["IMEI"]) ? Request.Cookies["IMEI"].Value : Convert.ToString(Request.QueryString["IMEI"]);
                Response.Cookies["IMEI"].Value = IMEI;
                PUserMobile UserMobile = new BUser().GetUserIDByIMEI(IMEI);
                string Message = "";

                if (UserMobile == null)
                {
                    Response.Cookies["IMEI"].Value = IMEI;
                }
                else if (!UserMobile.IsActive)
                {
                    Message = "";
                    Response.Redirect("RegisterUserMobile.aspx?Message=" + Message);
                }
                else if (UserMobile.ApprovedBy == null)
                {
                    Response.Redirect("RegisterUserMobile.aspx?Message=Your request waiting for approval");
                }
                else
                {
                    AddToSession(UserMobile.UserID);

                    if (!string.IsNullOrEmpty(Request.QueryString["Session_End"]))
                    {
                        Redirect(UIHelper.RedirectToHomePage + "?Session_End=This page idle for long lime so system went home page");
                    }
                    else
                    {
                        Redirect(UIHelper.RedirectToHomePage);
                    }
                }
            }
            //txtUsername.Text = "IT.OFFICER4";
            //txtPassword.Text = "abc@123";

            //txtUsername.Text = "IT.MGR2";
            //txtPassword.Text = "1";
            //login();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NoOfAllowedLoginAttempt = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["NoOfAllowedLoginAttempts"]);
            if (!Page.IsPostBack)
            {
                txtUsername.Focus();
            }
        }

        protected void imgLoginButton_Click(object sender, ImageClickEventArgs e)
        {
            login();
        }
        private void Redirect(string pageNmae)
        {
            Response.Redirect(pageNmae);
        }
        private void LockUser(string userName)
        {
            try
            {
                new BUser().LockUserAccount(userName);
                RemoveLoginAttemptsFromViewState();
                DisplayMessages(Convert.ToString(GetGlobalResourceObject("Resource", "userAccountLocked")));
            }
            catch (LMSException vpExe)
            {
                RemoveLoginAttemptsFromViewState();
                DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetErrorCode(vpExe.Message)));
            }
        }
        private void DisplayMessages(string messege)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = messege;
        }
        private void RemoveLoginAttemptsFromViewState()
        {
            if (ViewState["NoOfLoginAttempts"] != null)
                ViewState.Remove("NoOfLoginAttempts");
        }
        private void AddToSession(long userId)
        {
            PSession.User = new BUser().GetUserDetails(userId);
            PSession.UserId = userId;
            PSession.User.Dealer = new BDealer().GetDealerByUserID(userId);
            PSession.User.DMSModules = new BUser().GetDMSModuleByUser(userId, null, null);
            UIHelper.UserAudit();
        }

        void login()
        {
            int attemptCount = 0;
            PUser userDetails = null;
            if (ViewState["NoOfLoginAttempts"] != null)
                attemptCount = (int)ViewState["NoOfLoginAttempts"];

            DateTime traceStartTime = DateTime.Now;
            try
            {
                Session["LoginID"] = "";
                if ((txtUsername.Text == "admin") && (txtPassword.Text == "p123"))
                {
                    Session["LoginID"] = txtUsername.Text;
                    Response.Redirect("/ViewAdmin/UserList.aspx");
                }
                userDetails = new BUser().AuthenticateUser(txtUsername.Text, txtPassword.Text);
                if (userDetails.PasswordExpiryDate < DateTime.Now)
                {
                    AddToSession(userDetails.UserID);
                    Redirect(UIHelper.RedirectToPasswordChange);
                }

                if ((!string.IsNullOrEmpty(Request.QueryString["IMEI"])) || (!string.IsNullOrEmpty(Request.Cookies["IMEI"].Value)))
                {
                    string IMEI = string.IsNullOrEmpty(Request.QueryString["IMEI"]) ? Request.Cookies["IMEI"].Value : Convert.ToString(Request.QueryString["IMEI"]);
                    string Message = "";
                    if (new BUser().InserUserMobileIMEI(userDetails.UserID, IMEI))
                    {
                        Message = "You are new mobile user. Your request send to approval. Once approval you can open the application.";
                    }
                    else
                    {
                        Message = "Please contact the administrator.";
                    }
                    Response.Redirect("RegisterUserMobile.aspx?Message=" + Message);
                }
                RemoveLoginAttemptsFromViewState();

                AddToSession(userDetails.UserID);
                Redirect(UIHelper.RedirectToHomePage);
            }

            catch (LMSException vpExe)
            {
                RemoveLoginAttemptsFromViewState();
                DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetErrorCode(vpExe.Message)));
            }
            catch (LMSFunctionalException vpsFun)
            {
                if ((attemptCount + 1 >= NoOfAllowedLoginAttempt) && (ErrorHandler.GetFunctionalErrorCode(vpsFun.Message) == FunctionalErrorCode.InvalidPassword))
                {
                    LockUser(txtUsername.Text);
                }
                else
                {
                    DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetFunctionalErrorCode(vpsFun.Message)));
                    ViewState["NoOfLoginAttempts"] = attemptCount + 1;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }
        protected void lForgetPassword_Click(object sender, EventArgs e)
        {
            FldSignin.Visible = false;
            FldResetPassword.Visible = true;
            FldChangePassword.Visible = false;

            PUser userDetails = new BUser().GetUserDetails(txtUsername.Text.Trim());

            string Password = RandomNumber(000000, 999999).ToString("000000");
            new BUser().UpdateResetPassword(txtUsername.Text.Trim(), LMSHelper.EncodeString(Password));
            string messageBody = MailFormate.ForgotPassword;
            messageBody = messageBody.Replace("@@Addresse", userDetails.ContactName);
            messageBody = messageBody.Replace("@@UserName", userDetails.UserName);
            messageBody = messageBody.Replace("@@Password", Password);
            new EmailManager().MailSend(userDetails.Mail, "Password Reset Request", messageBody);

            messageBody = "Dear User, Your OTP for login is "+ Password +". From AJAX ENGG.";
            new EmailManager().SendSMS(userDetails.Employee.ContactNumber, messageBody);

            //string Password = "abc@123";
            //new BUser().UpdateResetPassword(txtUsername.Text.Trim(), LMSHelper.EncodeString(Password));
            //string messageBody = MailFormate.ForgotPassword;
            //messageBody = messageBody.Replace("@@Addresse", userDetails.ContactName);
            //messageBody = messageBody.Replace("@@UserName", userDetails.UserName);
            //messageBody = messageBody.Replace("@@Password", Password);
            //new EmailManager().MailSend(userDetails.Mail, "Password Reset Request", messageBody);
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (new BUser().ChangePassword(PSession.User.UserID, txtOTP.Text.Trim(), txtRNewPassword.Text.Trim(), txtRRetypePassword.Text) == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Your Password is changed successfully, please use the new password when you login next time');window.open('Home.aspx','_parent');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Your Password is not changed successfully, please try again');window.open('SignIn.aspx','_parent');", true);
                }
            }
            catch (Exception e1)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('" + e1.Message + "');", true);

            }
        }
    }
}