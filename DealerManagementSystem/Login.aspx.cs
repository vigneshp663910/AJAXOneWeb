using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Drawing;
using System.Web.UI;

namespace DealerManagementSystem
{
    public partial class Login : Page
    {
        private int NoOfAllowedLoginAttempt;
        protected void Page_PreInit(object sender, EventArgs e)
        {
             
            //if (Request.Cookies["deviceID"] == null)
            //{
            //    Response.Cookies["deviceID"].Value = "";
            //}

            //  https://dmswebview.ajax-engg.com:1444/?deviceID=ANDROID822a3d22b81e72ab&appID=QWpheE9uZU1vYmlsZQ==

            if (!string.IsNullOrEmpty(Request.QueryString["deviceID"]))
            {
                // string deviceID = string.IsNullOrEmpty(Request.QueryString["deviceID"]) ? Request.Cookies["deviceID"].Value : Convert.ToString(Request.QueryString["deviceID"]);
                //  Response.Cookies["deviceID"].Value = deviceID;
                string DeviceID = Convert.ToString(Request.QueryString["deviceID"]);
                string ApplicationKey = Convert.ToString(Request.QueryString["appID"]);
                // PUserMobile UserMobile = new BUser().GetUserIDByIMEI(DeviceID);
                txtUsername.Text = "2000ITH0001";
                txtPassword.Text = "Kml@1234";
                loginMobile(ApplicationKey, DeviceID);
                string Message = "";

                //if (UserMobile == null)
                //{
                //    Response.Cookies["deviceID"].Value = deviceID;
                //}else
                //if (!UserMobile.IsActive)
                //{
                //    Message = "";
                //    Response.Redirect("RegisterUserMobile.aspx?Message=" + Message);
                //}
                //else if (UserMobile.ApprovedBy == null)
                //{
                //    Response.Redirect("RegisterUserMobile.aspx?Message=Your request waiting for approval");
                //}
                //else
                //{
                //    AddToSession(UserMobile.UserID);

                //    if (!string.IsNullOrEmpty(Request.QueryString["Session_End"]))
                //    {
                //        Redirect(UIHelper.RedirectToHomePage + "?Session_End=This page idle for long lime so system went home page");
                //    }
                //    else
                //    {
                //        Redirect(UIHelper.RedirectToHomePage);
                //    }
                //}
            }
            else
            {

                txtUsername.Text = "IT.OFFICER4";
                txtPassword.Text = "abc@123";
                txtUsername.Text = "2000ITH0001";
                txtPassword.Text = "Kml@1234";
                //txtPassword.Text = "kML@1234";
                login();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string d = hfLatitude.Value;
            NoOfAllowedLoginAttempt = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["NoOfAllowedLoginAttempts"]);
            if (!Page.IsPostBack)
            {
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
                    Response.Redirect("ViewAdmin/UserList.aspx");
                    //Response.Redirect("Account/LoginAs.aspx");                 
                }

                UserAuthentication userA = new UserAuthentication();

                userA.UserName = txtUsername.Text;
                userA.LoginPassword = txtPassword.Text;
                userA.ApplicationKey = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ApplicationKey"]);
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
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    return;
                }
                PSession.User = JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(ResultToken.Data));
 

                if ((!PSession.User.ajaxOne) || (!PSession.User.ajaxOneDealer))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "You are not allowed to use";
                    lblMessage.Visible = true;
                    return;
                }
                PSession.Latitude = hfLatitude.Value;
                PSession.Longitude = hfLongitude.Value;
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
        void loginMobile(string ApplicationKey,string DeviceId)
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
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    return;
                }
                userDetails = JsonConvert.DeserializeObject<PUser>(JsonConvert.SerializeObject(ResultToken.Data));  

                PSession.User = userDetails;

                if ((!userDetails.ajaxOne) || (!userDetails.ajaxOneDealer))
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "You are not allowed to use";
                    lblMessage.Visible = true;
                    return;
                }
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
        protected void lForgetPassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                PUser userDetails = new BUser().GetUserDetails(txtUsername.Text.Trim());
                string Password = "abc@123";
                new BUser().UpdateResetPassword(txtUsername.Text.Trim(), LMSHelper.EncodeString(Password));
                string messageBody = MailFormate.ForgotPassword;
                messageBody = messageBody.Replace("@@Addresse", userDetails.ContactName);
                messageBody = messageBody.Replace("@@UserName", userDetails.UserName);
                messageBody = messageBody.Replace("@@Password", Password);
                new EmailManager().MailSend(userDetails.Mail,"", "Password Reset Request", messageBody);
            }
        }


        //void login()
        //{
        //    int attemptCount = 0;
        //    PUser userDetails = null;
        //    if (ViewState["NoOfLoginAttempts"] != null)
        //        attemptCount = (int)ViewState["NoOfLoginAttempts"];

        //    DateTime traceStartTime = DateTime.Now;
        //    try
        //    {
        //        Session["LoginID"] = "";
        //        if ((txtUsername.Text == "admin") && (txtPassword.Text == "p123"))
        //        {
        //            Session["LoginID"] = txtUsername.Text;
        //            Response.Redirect("ViewAdmin/UserList.aspx");
        //            //Response.Redirect("Account/LoginAs.aspx");                 
        //        }
        //        userDetails = new BUser().AuthenticateUser(txtUsername.Text, txtPassword.Text);
        //        if (userDetails.PasswordExpiryDate < DateTime.Now)
        //        {
        //            AddToSession(userDetails.UserID);
        //            Redirect(UIHelper.RedirectToPasswordChange);
        //        }

        //        if ((!string.IsNullOrEmpty(Request.QueryString["IMEI"])) || (!string.IsNullOrEmpty(Request.Cookies["IMEI"].Value)))
        //        {
        //            string IMEI = string.IsNullOrEmpty(Request.QueryString["IMEI"]) ? Request.Cookies["IMEI"].Value : Convert.ToString(Request.QueryString["IMEI"]);
        //            string Message = "";
        //            if (new BUser().InserUserMobileIMEI(userDetails.UserID, IMEI))
        //            {
        //                Message = "You are new mobile user. Your request send to approval. Once approval you can open the application.";
        //            }
        //            else
        //            {
        //                Message = "Please contact the administrator.";
        //            }
        //            Response.Redirect("RegisterUserMobile.aspx?Message=" + Message);
        //        }
        //        RemoveLoginAttemptsFromViewState();

        //        AddToSession(userDetails.UserID);
        //        Redirect(UIHelper.RedirectToHomePage);
        //    }

        //    catch (LMSException vpExe)
        //    {
        //        RemoveLoginAttemptsFromViewState();
        //        DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetErrorCode(vpExe.Message)));
        //    }
        //    catch (LMSFunctionalException vpsFun)
        //    {
        //        if ((attemptCount + 1 >= NoOfAllowedLoginAttempt) && (ErrorHandler.GetFunctionalErrorCode(vpsFun.Message) == FunctionalErrorCode.InvalidPassword))
        //        {
        //            LockUser(txtUsername.Text);
        //        }
        //        else
        //        {
        //            DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetFunctionalErrorCode(vpsFun.Message)));
        //            ViewState["NoOfLoginAttempts"] = attemptCount + 1;
        //        }
        //    }
        //}
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    if (Request.Cookies["IMEI"] == null)
        //    {
        //        Response.Cookies["IMEI"].Value = "";
        //    }
        //    if ((!string.IsNullOrEmpty(Request.QueryString["IMEI"])) || (!string.IsNullOrEmpty(Request.Cookies["IMEI"].Value)))
        //    {
        //        string IMEI = string.IsNullOrEmpty(Request.QueryString["IMEI"]) ? Request.Cookies["IMEI"].Value : Convert.ToString(Request.QueryString["IMEI"]);
        //        Response.Cookies["IMEI"].Value = IMEI;
        //        PUserMobile UserMobile = new BUser().GetUserIDByIMEI(IMEI);
        //        string Message = "";

        //        if (UserMobile == null)
        //        {
        //            Response.Cookies["IMEI"].Value = IMEI;
        //        }
        //        else if (!UserMobile.IsActive)
        //        {
        //            Message = "";
        //            Response.Redirect("RegisterUserMobile.aspx?Message=" + Message);
        //        }
        //        else if (UserMobile.ApprovedBy == null)
        //        {
        //            Response.Redirect("RegisterUserMobile.aspx?Message=Your request waiting for approval");
        //        }
        //        else
        //        {
        //            AddToSession(UserMobile.UserID);

        //            if (!string.IsNullOrEmpty(Request.QueryString["Session_End"]))
        //            {
        //                Redirect(UIHelper.RedirectToHomePage + "?Session_End=This page idle for long lime so system went home page");
        //            }
        //            else
        //            {
        //                Redirect(UIHelper.RedirectToHomePage);
        //            }
        //        }
        //    }
        //    //txtUsername.Text = "IT.OFFICER4";
        //    //txtPassword.Text = "abc@123";
        //    txtUsername.Text = "IT.MGR2";
        //    txtPassword.Text = "aJAX@123";

        //    login();
        //}

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