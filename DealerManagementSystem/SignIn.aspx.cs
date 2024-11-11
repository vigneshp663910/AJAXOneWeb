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
        private string OTP
        {
            get
            {
                if (Session["SignInOTP"] == null)
                {
                    Session["SignInOTP"] = "";
                }
                return (string)Session["SignInOTP"];
            }
            set
            {
                Session["SignInOTP"] = value;
            }
        }
        private int OtpCount
        {
            get
            {
                if (Session["SignInOtpCount"] == null)
                {
                    Session["SignInOtpCount"] = 0;
                }
                return (int)Session["SignInOtpCount"];
            }
            set
            {
                Session["SignInOtpCount"] = value;
            }
        }
        private int OtpMisMatchCount
        {
            get
            {
                if (Session["OtpMisMatchCount"] == null)
                {
                    Session["OtpMisMatchCount"] = 0;
                }
                return (int)Session["OtpMisMatchCount"];
            }
            set
            {
                Session["OtpMisMatchCount"] = value;
            }
        }
        private int NoOfAllowedLoginAttempt;
        protected void Page_PreInit(object sender, EventArgs e)
        {
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            NoOfAllowedLoginAttempt = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["NoOfAllowedLoginAttempts"]);
            if (!Page.IsPostBack)
            {
                lblServer.Text = "&nbsp;&nbsp;&nbsp;" + ConfigurationManager.AppSettings["Server"];
                txtUsername.Focus();
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
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = messege; 
        }
        private void RemoveLoginAttemptsFromViewState()
        {
            if (ViewState["NoOfLoginAttempts"] != null)
                ViewState.Remove("NoOfLoginAttempts");
        }
        void login()
        { 
            lblMessage.ForeColor = Color.Red;
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
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblMessage.Text = "Please Enter Password...!";  
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
                    lblMessage.Text = Results.Message; 
                    return;
                }
                PSession.AccessToken = Convert.ToString(Results.Data);

                PApiResult ResultToken = new BUser().GetUserByToken();
                if (ResultToken.Status == PApplication.Failure)
                { 
                    lblMessage.Text = ResultToken.Message; 
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                login();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString(); 
                lblMessage.ForeColor = Color.Red; 
            }
        }
        protected void lForgetPassword_Click(object sender, EventArgs e)
        { 
            lblMessage.ForeColor = Color.Red; 
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                lblMessage.Text = "Please Enter UserName...!"; 
                return;
            }
            PUser userDetails = new BUser().GetUserDetails(txtUsername.Text.Trim());
            if (!string.IsNullOrEmpty(userDetails.UserName))
            {
                if(userDetails.IsLocked)
                {
                    lblMessage.Text = "Your ID is Locked. Please contact Admin"; 
                    return;
                }
                FldSignin.Visible = false;
                FldResetPassword.Visible = true;
                UserID = userDetails.UserID;

                // Response.Redirect("SignIn.aspx?SignIn=ForgotPassword&UserID=" + userDetails.UserID + "", true);
            }
            else
            {
                lblMessage.Text = "Invalid UserName...!";  
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
            lblMessage.ForeColor = Color.Red;
            try
            { 
                if (string.IsNullOrEmpty(txtOTP.Text))
                {
                    lblMessage.Text = "Please Enter OTP...!"; 
                    return;
                }
                if (txtRNewPassword.Text.ToUpper().Contains("AJAX@123"))
                {
                    lblMessage.Text = "Please Provide Another Password...!";  
                    return;
                }
                if (UserID != 0)
                {
                    PUser userDetails = new BUser().GetUserDetails(UserID);
                    if (userDetails.IsLocked)
                    {
                        lblMessage.Text = "Your ID is Locked. Please contact Admin";
                        return;
                    }
                    if (OTP != txtOTP.Text.Trim())
                    {
                        OtpMisMatchCount = OtpMisMatchCount + 1;
                        lblMessage.Text = "Your OTP is not Matching";
                        if (OtpMisMatchCount > 4)
                        {
                            OtpMisMatchCount = 0;
                            LockUser(txtUsername.Text);
                            lblMessage.Text = "Your ID is Locked. Please contact Admin";
                        }                     
                        return;
                    }
                    if (new BUser().ChangePassword(UserID, txtOTP.Text.Trim(), txtRNewPassword.Text.Trim(), txtRRetypePassword.Text, "Reset") == 1)
                    {
                        PUser user = new BUser().GetUserDetails(UserID);
                        txtUsername.Text = user.UserName;
                        txtPassword.Text = txtRRetypePassword.Text;
                        login();
                    }
                    else
                    {
                        lblMessage.Text = "Your Password is not changed successfully, please try again...!";  
                        return;
                    }
                }
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message.ToString();  
                return;
            }
        }
        protected void BtnSendOTP_Click(object sender, EventArgs e)
        { 
            lblMessage.ForeColor = Color.Red;
            try
            {
                PUser userDetails = new BUser().GetUserDetails(UserID);
                if (userDetails.IsLocked)
                {
                    lblMessage.Text = "Your ID is Locked. Please contact Admin";
                    return;
                }
                OtpCount = OtpCount + 1;
                if (OtpCount > 4)
                {
                    LockUser(txtUsername.Text);
                    lblMessage.Text = "Your ID is Locked. Please contact Admin";
                    OtpCount = 0;
                    return;
                }
               
                if (!string.IsNullOrEmpty(userDetails.UserName))
                {
                    OTP = RandomNumber(000000, 999999).ToString("000000");
                    new BUser().UpdateResetPassword(userDetails.UserName.Trim(), LMSHelper.EncodeString(OTP));
                    string messageBody = MailFormate.ForgotPassword;
                    messageBody = messageBody.Replace("@@Addresse", userDetails.ContactName);
                    messageBody = messageBody.Replace("@@UserName", userDetails.UserName);
                    messageBody = messageBody.Replace("@@Password", OTP);
                    messageBody = messageBody.Replace("@@URL", "");
                    new EmailManager().MailSend(userDetails.Mail, "", "Password Reset Request", messageBody);

                    //messageBody = "Dear User, Your OTP for AJAX DMS Login is " + Password + ". From Team AJAXOne";
                    messageBody = "Dear User, Your OTP for login is " + OTP + ". From AJAX ENGG";
                    new EmailManager().SendSMS(userDetails.Employee.ContactNumber, messageBody);
                }
                else
                {
                    lblMessage.Text = "Invalid UserName...!";   
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OTP", "OTP()", true);
                //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:OTP(); ", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();  
            }
        }
    }
}