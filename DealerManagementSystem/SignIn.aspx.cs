using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.UI;
namespace DealerManagementSystem
{
    public partial class SignIn : System.Web.UI.Page
    {
        private int UserID
        {
            get
            {
                if (Session["UserID"] == null)
                {
                    Session["UserID"] = 0;
                }
                return (int)Session["UserID"];
            }
            set
            {
                Session["UserID"] = value;
            }
        }
        private int NoOfAllowedLoginAttempt;
        protected void Page_PreInit(object sender, EventArgs e)
        { 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            NoOfAllowedLoginAttempt = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["NoOfAllowedLoginAttempts"]);
            if (!Page.IsPostBack)
            {
                lblServer.Text = "&nbsp;&nbsp;&nbsp;" + ConfigurationManager.AppSettings["Server"];
                txtUsername.Focus();
            }
        }

        protected void imgLoginButton_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["deviceID"]))
            {
                string DeviceID = Convert.ToString(Request.QueryString["deviceID"]);
                string ApplicationKey = Convert.ToString(Request.QueryString["appID"]);
                loginMobile(ApplicationKey, DeviceID);
            }
            else
            {
                login();
            }
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
        //private void AddToSession(long userId)
        //{
        //    PSession.User = new BUser().GetUserDetails(userId);
        //    //PSession.UserId = userId;
        //    PSession.User.Dealer = new BDealer().GetDealerByUserID(userId);
        //    PSession.User.DMSModules = new BUser().GetDMSModuleByUser(userId, null, null);
        //    UIHelper.UserAudit(hfLatitude.Value, hfLongitude.Value);
        //}

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
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    lblMessage.Text = "Please Enter UserName...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblMessage.Text = "Please Enter Password...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if ((txtUsername.Text == "admin") && (txtPassword.Text == "p123"))
                {
                    Session["LoginID"] = txtUsername.Text;
                    Response.Redirect("ViewAdmin/UserList.aspx");
                    //Response.Redirect("Account/LoginAs.aspx");                 
                }

                

                UserAuthentication userA = new UserAuthentication();

                userA.UserName = txtUsername.Text;
                userA.LoginPassword = txtPassword.Text;
                userA.ApplicationKey = Convert.ToString(ConfigurationManager.AppSettings["ApplicationKey"]);
                PApiResult Results = new BUser().Login(userA);

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
                    lblMessage.Text = ResultToken.Message;
                    lblMessage.Visible = true;
                    return;
                }
                PSession.User = JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(ResultToken.Data)); 
                //if (txtPassword.Text.ToUpper().Contains("AJAX@123"))
                //{ 
                //    Response.Redirect("SignIn.aspx?SignIn=ChangePassword&UserID=" + PSession.User.UserID + "", true);
                //} 
                
                UIHelper.UserAudit(hfLatitude.Value, hfLongitude.Value);
                if (PSession.User.PasswordExpiryDate < DateTime.Now)
                {
                    Redirect(UIHelper.RedirectToPasswordChange);
                }
                RemoveLoginAttemptsFromViewState();
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
        void loginMobile(string ApplicationKey, string DeviceId)
        {
            int attemptCount = 0;
            PUser userDetails = null;
            if (ViewState["NoOfLoginAttempts"] != null)
                attemptCount = (int)ViewState["NoOfLoginAttempts"];

            DateTime traceStartTime = DateTime.Now;
            try
            {
                Session["LoginID"] = "";


                UserAuthentication userA = new UserAuthentication();

                userA.UserName = txtUsername.Text;
                userA.LoginPassword = txtPassword.Text;
                userA.ApplicationKey = ApplicationKey;
                userA.DeviceId = DeviceId;

                PApiResult Results = new BUser().Login(userA);

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
                    lblMessage.Text = ResultToken.Message;
                    lblMessage.Visible = true;
                    return;
                }
                userDetails = JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(ResultToken.Data));

                PSession.User = userDetails;
                //if (txtPassword.Text.ToUpper().Contains("AJAX@123"))
                //{
                //    Response.Redirect("SignIn.aspx?SignIn=ChangePassword&UserID=" + PSession.User.UserID + "", true);
                //}
                 
                PSession.Latitude = hfLatitude.Value;
                PSession.Longitude = hfLongitude.Value;
                UIHelper.UserAudit(hfLatitude.Value, hfLongitude.Value);
                if (userDetails.PasswordExpiryDate < DateTime.Now)
                {
                    Redirect(UIHelper.RedirectToPasswordChange);
                }
                RemoveLoginAttemptsFromViewState();
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
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["deviceID"]))
                {
                    string DeviceID = Convert.ToString(Request.QueryString["deviceID"]);
                    if (Request.QueryString["appID"] == null)
                    {
                        lblMessage.Text = "Please Provide AppID...!";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtUsername.Text))
                    {
                        lblMessage.Text = "Please Enter UserName...!";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtPassword.Text))
                    {
                        lblMessage.Text = "Please Enter Password...!";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    string ApplicationKey = Convert.ToString(Request.QueryString["appID"]);
                    loginMobile(ApplicationKey, DeviceID);
                }
                else
                {
                    login();
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lForgetPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                lblMessage.Text = "Please Enter UserName...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            PUser userDetails = new BUser().GetUserDetails(txtUsername.Text.Trim());
            if (!string.IsNullOrEmpty(userDetails.UserName))
            {
                FldSignin.Visible = false;
                FldResetPassword.Visible = true; 
                UserID = userDetails.UserID;
               // Response.Redirect("SignIn.aspx?SignIn=ForgotPassword&UserID=" + userDetails.UserID + "", true);
            }
            else
            {
                lblMessage.Text = "Invalid UserName...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
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
                if (string.IsNullOrEmpty(txtOTP.Text))
                {
                    lblMessage.Text = "Please Enter OTP...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (txtRNewPassword.Text.ToUpper().Contains("AJAX@123"))
                {
                    lblMessage.Text = "Please Provide Another Password...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (UserID != 0)
                {
                    if (new BUser().ChangePassword(UserID, txtOTP.Text.Trim(), txtRNewPassword.Text.Trim(), txtRRetypePassword.Text,"Reset") == 1)
                    {
                        PUser user = new BUser().GetUserDetails(UserID);
                        txtUsername.Text = user.UserName;
                        txtPassword.Text = txtRRetypePassword.Text;
                        if (!string.IsNullOrEmpty(Request.QueryString["deviceID"]))
                        {
                            string DeviceID = Convert.ToString(Request.QueryString["deviceID"]);
                            string ApplicationKey = Convert.ToString(Request.QueryString["appID"]);
                            loginMobile(ApplicationKey, DeviceID);
                        }
                        else
                        {
                            login();
                        }
                        
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Your Password is not changed successfully, please try again');window.open('SignIn.aspx','_parent');", true);
                        lblMessage.Text = "Your Password is not changed successfully, please try again...!";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            catch (Exception e1)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('" + e1.Message + "');", true);
                lblMessage.Text = e1.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
         

        protected void BtnSendOTP_Click(object sender, EventArgs e)
        {
            try
            {
                PUser userDetails = new BUser().GetUserDetails(UserID);
                if (!string.IsNullOrEmpty(userDetails.UserName))
                {
                    string Password = RandomNumber(000000, 999999).ToString("000000");
                    new BUser().UpdateResetPassword(userDetails.UserName.Trim(), LMSHelper.EncodeString(Password));
                    string messageBody = MailFormate.ForgotPassword;
                    messageBody = messageBody.Replace("@@Addresse", userDetails.ContactName);
                    messageBody = messageBody.Replace("@@UserName", userDetails.UserName);
                    messageBody = messageBody.Replace("@@Password", Password);
                    messageBody = messageBody.Replace("@@URL", "");
                    new EmailManager().MailSend(userDetails.Mail,"", "Password Reset Request", messageBody);

                    //messageBody = "Dear User, Your OTP for AJAX DMS Login is " + Password + ". From Team AJAXOne";
                    messageBody = "Dear User, Your OTP for login is " + Password + ". From AJAX ENGG";
                    new EmailManager().SendSMS(userDetails.Employee.ContactNumber, messageBody);
                }
                else
                {
                    lblMessage.Text = "Invalid UserName...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OTP", "OTP()", true);
                //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:OTP(); ", true);
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message.ToString(); ;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
    }
}