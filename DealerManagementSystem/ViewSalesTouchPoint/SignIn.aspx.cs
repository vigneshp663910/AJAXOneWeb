using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSalesTouchPoint
{
    public partial class SignIn : System.Web.UI.Page
    {
        private int SalesTouchPointUserID
        {
            get
            {
                if (ViewState["SalesTouchPointUserID"] == null)
                {
                    ViewState["SalesTouchPointUserID"] = 0;
                }
                return (int)ViewState["SalesTouchPointUserID"];
            }
            set
            {
                ViewState["SalesTouchPointUserID"] = value;
            }
        }
        private string MobileOTP
        {
            get
            {
                if (ViewState["MobileOTP"] == null)
                {
                    ViewState["MobileOTP"] = "";
                }
                return (string)ViewState["MobileOTP"];
            }
            set
            {
                ViewState["MobileOTP"] = value;
            }
        }
        private string EmailOTP
        {
            get
            {
                if (ViewState["EmailOTP"] == null)
                {
                    ViewState["EmailOTP"] = "";
                }
                return (string)ViewState["EmailOTP"];
            }
            set
            {
                ViewState["EmailOTP"] = value;
            }
        }
        private string ResetOTP
        {
            get
            {
                if (ViewState["ResetOTP"] == null)
                {
                    ViewState["ResetOTP"] = "";
                }
                return (string)ViewState["ResetOTP"];
            }
            set
            {
                ViewState["ResetOTP"] = value;
            }
        }
        private int OtpCount
        {
            get
            {
                if (ViewState["SignInOtpCount"] == null)
                {
                    ViewState["SignInOtpCount"] = 0;
                }
                return (int)ViewState["SignInOtpCount"];
            }
            set
            {
                ViewState["SignInOtpCount"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            txtRegPassword.Attributes["value"] = txtRegPassword.Text;
            txtRegConfirmPassword.Attributes["value"] = txtRegConfirmPassword.Text;
            txtRNewPassword.Attributes["value"] = txtRNewPassword.Text;
            txtRRetypePassword.Attributes["value"] = txtRRetypePassword.Text;
            txtPassword.Attributes["value"] = txtPassword.Text;
            if (!IsPostBack)
            {
                ViewState["SalesTouchPointUserID"] = null;
                new DDLBind(ddlState, new BOnboardEmployee().GetState(null, null, null, null, null), "State", "StateID", true, "Select");
                new DDLBind(ddlDistrict, new BOnboardEmployee().GetDistrict(null, null, 0, null, null, null), "District", "DistrictID", true, "Select");
            }
        }

        protected void BtnSendMobileOTP_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                MobileOTP = RandomNumber(000000, 999999).ToString("000000");
                string messageBody = "";
                messageBody = "Dear User, Your OTP for login is " + MobileOTP + ". From AJAX ENGG";
                new EmailManager().SendSMS(txtMobileNumber.Text, messageBody);
                Mobilesome_div.Visible = true;
                txtMobileOTP.Visible = true;
                BtnVerifyMobileOTP.Visible = true;
                VerifyMobileOTP.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "MobileOTP", "MobileOTP()", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        protected void BtnVerifyMobileOTP_Click(object sender, EventArgs e)
        {
            if (txtMobileOTP.Text == MobileOTP)
            {
                txtMobileNumber.Enabled = false;
                VerifyMobileOTP.ImageUrl = "~/Images/Verified.jpg";
                BtnSendMobileOTP.Visible = false;
                Mobilesome_div.Visible = false;
                txtMobileOTP.Visible = false;
                BtnVerifyMobileOTP.Visible = false;
                btnMobileVerify.Visible = false;

                FldVerifyEmailOTP.Visible = false;
                FldVerifyMobileOTP.Visible = false;
                FldRegister.Visible = true;
                FldSignin.Visible = false;
                FldResetPassword.Visible = false;
                FldChangePassword.Visible = false;
                MobileOTP = "Verified";
            }
        }

        protected void BtnSendEmailOTP_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                EmailOTP = RandomNumber(000000, 999999).ToString("000000");
                string messageBody = MailFormate.ForgotPassword;
                messageBody = messageBody.Replace("@@Addresse", txtName.Text);
                messageBody = messageBody.Replace("@@UserName", txtName.Text);
                messageBody = messageBody.Replace("@@Password", EmailOTP);
                messageBody = messageBody.Replace("@@URL", "");
                new EmailManager().MailSend(txtEmail.Text, "", "Verify Email Request", messageBody);
                Emailsome_div.Visible = true;
                txtEmailOTP.Visible = true;
                BtnVerifyEmailOTP.Visible = true;
                //VerifyEmailOTP.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "EmailOTP", "EmailOTP()", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }

        protected void BtnVerifyEmailOTP_Click(object sender, EventArgs e)
        {
            if (txtEmailOTP.Text == EmailOTP)
            {
                txtEmail.Enabled = false;
                Emailsome_div.Visible = false;
                //VerifyEmailOTP.ImageUrl = "~/Images/Verified.jpg";
                BtnSendEmailOTP.Visible = false;
                Emailsome_div.Visible = false;
                txtEmailOTP.Visible = false;
                BtnVerifyEmailOTP.Visible = false;
                //btnEmailVerify.Visible = false;

                FldVerifyEmailOTP.Visible = false;
                FldVerifyMobileOTP.Visible = false;
                FldRegister.Visible = true;
                FldSignin.Visible = false;
                FldResetPassword.Visible = false;
                FldChangePassword.Visible = false;
                EmailOTP = "Verified";
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
                new DDLBind(ddlDistrict, new BOnboardEmployee().GetDistrict(null, null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID", true, "Select");
        }

        protected void btnRegSignIn_Click(object sender, EventArgs e)
        {
            FldSignin.Visible = true;
            FldRegister.Visible = false;
            FldVerifyEmailOTP.Visible = false;
            FldVerifyMobileOTP.Visible = false;
            FldResetPassword.Visible = false;
            FldChangePassword.Visible = false;
        }

        protected void btnLogRegister_Click(object sender, EventArgs e)
        {
            FldSignin.Visible = false;
            FldRegister.Visible = true;
            FldVerifyEmailOTP.Visible = false;
            FldVerifyMobileOTP.Visible = false;
            FldResetPassword.Visible = false;
            FldChangePassword.Visible = false;
        }

        protected void btnEmailVerify_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblMessage.Text = "Invalid Email ID...!";
                return;
            }
            FldVerifyEmailOTP.Visible = true;
            FldVerifyMobileOTP.Visible = false;
            FldRegister.Visible = false;
            FldSignin.Visible = false;
            FldResetPassword.Visible = false;
            FldChangePassword.Visible = false;
            BtnSendEmailOTP_Click(null, null);
        }

        protected void btnMobileVerify_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtMobileNumber.Text))
            {
                lblMessage.Text = "Invalid Mobile No...!";
                return;
            }

            FldVerifyEmailOTP.Visible = false;
            FldVerifyMobileOTP.Visible = true;
            FldRegister.Visible = false;
            FldSignin.Visible = false;
            FldResetPassword.Visible = false;
            FldChangePassword.Visible = false;
            BtnSendMobileOTP_Click(null, null);
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            PSalesTouchPointUser_Insert User = new PSalesTouchPointUser_Insert();
            User.SalesTouchPointUserID = ViewState["SalesTouchPointUserID"] == null ? 0 : (int)ViewState["SalesTouchPointUserID"];
            User.AadharNumber = Convert.ToString(txtAadharNo.Text.Trim());
            User.Name = txtName.Text.Trim();
            User.ContactNumber = txtMobileNumber.Text.Trim();
            User.EmailID = txtEmail.Text.Trim();
            User.Password = LMSHelper.EncodeString(txtRegConfirmPassword.Text.Trim());
            User.Address1 = txtAddress1.Text.Trim();
            User.Address2 = txtAddress2.Text.Trim();
            User.StateID = Convert.ToInt32(ddlState.SelectedValue);
            User.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            User.IsActive = true;
            User.IsLocked = false;
            User.IsEnabled = true;
            User.PasswordExpirationDate = DateTime.Now.AddMonths(3);
            User.Latitude = hfLatitude.Value;
            User.Longitude = hfLongitude.Value;
            User.LatitudeLongitudeDate = DateTime.Now;

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", User));
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            int SalesTouchPointUserID = Convert.ToInt32(Result.Data);
            if (SalesTouchPointUserID != 0)
            {
                lblMessage.Text = "SalesTouchPoint User is updated successfully";
                lblMessage.ForeColor = Color.Green;
                btnRegister.Visible = false;
            }
            else
            {
                lblMessage.Text = "SalesTouchPoint User is not updated successfully";
            }
            btnRegister.Focus();
        }
        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;

            txtAadharNo.BorderColor = Color.Silver;
            txtName.BorderColor = Color.Silver;
            txtMobileNumber.BorderColor = Color.Silver;
            txtEmail.BorderColor = Color.Silver;
            txtRegPassword.BorderColor = Color.Silver;
            txtRegConfirmPassword.BorderColor = Color.Silver;
            txtAddress1.BorderColor = Color.Silver;
            txtAddress2.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;

            Boolean Ret = true;
            string Message = "";

            if (txtAadharNo.Text.Length!=12)
            {
                Message = "Please enter Valid AadharNo";
                Ret = false;
                txtAadharNo.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the name";
                Ret = false;
                txtName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtMobileNumber.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the mobile number";
                Ret = false;
                txtMobileNumber.BorderColor = Color.Red;
            }
            if (txtMobileNumber.Text.Trim().Count() != 10)
            {
                Message = Message + "<br/>Please check the mobile number";
                Ret = false;
                txtMobileNumber.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Email";
                Ret = false;
                txtEmail.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            if (!Ret)
            {
                return Ret;
            }
            string email = txtEmail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                Message = Message + "<br/>" + email + " is not correct";
                Ret = false;
                txtEmail.BorderColor = Color.Red;
            }
            if (MobileOTP != "Verified")
            {
                Message = Message + "<br/>Please verify mobile";
                Ret = false;
                txtMobileNumber.BorderColor = Color.Red;
            }
            //if (EmailOTP != "Verified")
            //{
            //    Message = Message + "<br/>Please verify email";
            //    Ret = false;
            //    txtEmail.BorderColor = Color.Red;
            //}
            if (string.IsNullOrEmpty(txtRegPassword.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the password";
                Ret = false;
                txtRegPassword.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtRegPassword.Text.Trim()) != string.IsNullOrEmpty(txtRegConfirmPassword.Text.Trim()))
            {
                Message = Message + "<br/>Mismatch the confirm password";
                Ret = false;
                txtRegPassword.BorderColor = Color.Red;
                txtRegConfirmPassword.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the address1";
                Ret = false;
                txtAddress1.BorderColor = Color.Red;
            }
            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select state";
                Ret = false;
                ddlState.BorderColor = Color.Red;
            }

            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select district";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
            }

            PSalesTouchPointUser User = new BSalesTouchPoint().GetSalesTouchPointUser(txtMobileNumber.Text.Trim(), null);
            if (User != null)
            {
                Message = Message + "<br/>User already available";
                Ret = false;
                txtMobileNumber.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            return Ret;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                lblMessage.Text = "Please enter username...!";
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblMessage.Text = "Please enter password...!";
                return;
            }

            UserAuthentication userA = new UserAuthentication();

            userA.UserName = txtUsername.Text;
            userA.LoginPassword = LMSHelper.EncodeString(txtPassword.Text);
            userA.ApplicationKey = Convert.ToString(ConfigurationManager.AppSettings["ApplicationKey"]);

            PApiResult Results = new BSalesTouchPoint().Login(userA);

            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            PSession_STP.AccessToken = Convert.ToString(Results.Data);

            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }

            PSalesTouchPointUser User = new BSalesTouchPoint().GetSalesTouchPointUser(txtUsername.Text.Trim(), LMSHelper.EncodeString(txtPassword.Text.Trim()));
            if (User != null)
            {
                PSession_STP.SalesTouchPointUser = User;

                PUserAudit_STP UserAudit = new PUserAudit_STP()
                {
                    Browser = PSession_STP.UserBrowser,
                    IPAddress = PSession_STP.UserIPAddress,
                    IsSessionExpired = false,
                    LoginDate = DateTime.Now,
                    LogoutDate = DateTime.Now,
                    SesionId = PSession_STP.SessionId,
                    UserId = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID,
                    Latitude = hfLatitude.Value,
                    Longitude = hfLongitude.Value
                };

                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser/InsertUpdateSalesTouchPointUserAudit", UserAudit));

                if (PSession_STP.SalesTouchPointUser.PasswordExpirationDate < DateTime.Now)
                {
                    FldSignin.Visible = false;
                    FldRegister.Visible = false;
                    FldVerifyEmailOTP.Visible = false;
                    FldVerifyMobileOTP.Visible = false;
                    FldResetPassword.Visible = false;
                    FldChangePassword.Visible = true;
                    return;
                }
                RemoveLoginAttemptsFromViewState();
                Redirect(UIHelper.RedirectToSalesTouchPointHomePage);
                lblMessage.Text = "";
            }
            else
            {
                lblMessage.Text = "Invalid Username and Password...!";
                return;
            }
        }


        protected void LnkForgotPassword_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                lblMessage.Text = "Please Enter UserName...!";
                return;
            }
            PSalesTouchPointUser_Insert UpdateUser = FillUser(txtUsername.Text);
            UpdateUser.ModifiedBy = UpdateUser.SalesTouchPointUserID;
            if (!string.IsNullOrEmpty(UpdateUser.ContactNumber))
            {
                if (UpdateUser.IsLocked)
                {
                    lblMessage.Text = "Your ID is Locked. Please contact Admin";
                    return;
                }

                OtpCount = OtpCount + 1;
                if (OtpCount > 4)
                {
                    UpdateUser.IsLocked = true;
                    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
                    RemoveLoginAttemptsFromViewState();

                    lblMessage.Text = "Your ID is Locked. Please contact Admin";
                    OtpCount = 0;
                    return;
                }
                SalesTouchPointUserID = UpdateUser.SalesTouchPointUserID;

                if (!string.IsNullOrEmpty(UpdateUser.ContactNumber))
                {
                    ResetOTP = RandomNumber(000000, 999999).ToString("000000");
                    UpdateUser.OTP = LMSHelper.EncodeString(ResetOTP);
                    UpdateUser.OTPExpiry = DateTime.Now.AddMinutes(10);
                    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
                    string messageBody = MailFormate.ForgotPassword;
                    messageBody = messageBody.Replace("@@Addresse", UpdateUser.Name);
                    messageBody = messageBody.Replace("@@UserName", UpdateUser.ContactNumber);
                    messageBody = messageBody.Replace("@@Password", ResetOTP);
                    messageBody = messageBody.Replace("@@URL", "");
                    new EmailManager().MailSend(UpdateUser.EmailID, "", "Password Reset Request", messageBody);

                    //messageBody = "Dear User, Your OTP for AJAX DMS Login is " + Password + ". From Team AJAXOne";
                    messageBody = "Dear User, Your OTP for login is " + ResetOTP + ". From AJAX ENGG";
                    new EmailManager().SendSMS(UpdateUser.ContactNumber, messageBody);
                }
                else
                {
                    lblMessage.Text = "Invalid UserName...!";
                }
                SalesTouchPointUserID = UpdateUser.SalesTouchPointUserID;
                FldVerifyEmailOTP.Visible = false;
                FldVerifyMobileOTP.Visible = false;
                FldRegister.Visible = false;
                FldSignin.Visible = false;
                FldSignin.Visible = false;
                FldResetPassword.Visible = true;
                BtnVerifyMobileorEmailOTP.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OTP", "OTP()", true);
            }
            else
            {
                lblMessage.Text = "Invalid UserName...!";
                return;
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
                PSalesTouchPointUser_Insert UpdateUser = FillUser(userName);
                UpdateUser.ModifiedBy = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID;
                UpdateUser.IsLocked = true;
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
                RemoveLoginAttemptsFromViewState();
                DisplayMessages(Convert.ToString(GetGlobalResourceObject("Resource", "userAccountLocked")));
            }
            catch (LMSException vpExe)
            {
                RemoveLoginAttemptsFromViewState();
                DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetErrorCode(vpExe.Message)));
            }
        }
        private PSalesTouchPointUser_Insert FillUser(string userName)
        {
            PSalesTouchPointUser_Insert UpdateUser = new PSalesTouchPointUser_Insert();
            try
            {
                PSalesTouchPointUser User = new BSalesTouchPoint().GetSalesTouchPointUser(userName, null);
                UpdateUser.SalesTouchPointUserID = User.SalesTouchPointUserID;
                UpdateUser.AadharNumber = User.AadharNumber;
                UpdateUser.Name = User.Name;
                UpdateUser.ContactNumber = User.ContactNumber;
                UpdateUser.EmailID = User.EmailID;
                UpdateUser.Password = User.Password;
                UpdateUser.Address1 = User.Address1;
                UpdateUser.Address2 = User.Address2;
                UpdateUser.IsActive = User.IsActive;
                UpdateUser.IsLocked = User.IsLocked;
                UpdateUser.IsEnabled = User.IsEnabled;
                UpdateUser.StateID = User.State.StateID;
                UpdateUser.DistrictID = User.District.DistrictID;
                UpdateUser.PasswordExpirationDate = User.PasswordExpirationDate;
                UpdateUser.Latitude = User.Latitude;
                UpdateUser.Longitude = User.Longitude;
                UpdateUser.LatitudeLongitudeDate = User.LatitudeLongitudeDate;
                UpdateUser.LoginCount = User.LoginCount;
                UpdateUser.ManualLockDate = User.ManualLockDate;
                UpdateUser.OTP = User.OTP;
                UpdateUser.OTPExpiry = User.OTPExpiry;
                return UpdateUser;
            }
            catch (Exception ex)
            {
                DisplayMessages(ErrorHandler.GetErrorMessage(ErrorHandler.GetErrorCode(ex.Message)));
            }
            return UpdateUser;
        }
        private void RemoveLoginAttemptsFromViewState()
        {
            if (ViewState["NoOfLoginAttempts"] != null)
                ViewState.Remove("NoOfLoginAttempts");
        }
        private void DisplayMessages(string messege)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = messege;
        }
        protected void BtnVerifyMobileorEmailOTP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOTP.Text))
            {
                lblMessage.Text = "Please Enter the OTP.";
                return;
            }
            if (txtOTP.Text == ResetOTP)
            {
                ResetOTP = "Verified";
                txtOTP.Enabled = false;
                VerifyMobileorEmailOTP.Visible = true;
                VerifyMobileorEmailOTP.ImageUrl = "~/Images/Verified.jpg";
                BtnVerifyMobileorEmailOTP.Visible = false;
                BtnSendOTP.Visible = false;
            }
            else
            {
                OtpCount = OtpCount + 1;
                lblMessage.Text = "Your OTP is not Matching";
                if (OtpCount > 4)
                {
                    LockUser(txtUsername.Text);
                    OtpCount = 0;
                    lblMessage.Text = "Your ID is Locked. Please contact Admin";
                }
                return;
            }
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
                if (SalesTouchPointUserID != 0)
                {
                    PSalesTouchPointUser_Insert UpdateUser = FillUser(txtUsername.Text);
                    UpdateUser.ModifiedBy = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID;
                    if (UpdateUser.IsLocked)
                    {
                        lblMessage.Text = "Your ID is Locked. Please contact Admin";
                        return;
                    }
                    if (ResetOTP != "Verified")
                    {
                        lblMessage.Text = "Please Verify Email/Mobile?";
                        return;
                    }
                    if (new BSalesTouchPoint().ValidateChangePassword(UpdateUser, txtOTP.Text.Trim(), txtRNewPassword.Text.Trim(), txtRRetypePassword.Text, "Reset") == true)
                    {
                        UpdateUser.Password = LMSHelper.EncodeString(txtRRetypePassword.Text);
                        UpdateUser.PasswordExpirationDate = DateTime.Now.AddMonths(3);
                        PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
                        txtUsername.Text = UpdateUser.ContactNumber;
                        txtPassword.Text = txtRRetypePassword.Text;
                        btnLogRegister_Click(null, null);
                        btnLogin_Click(null, null);
                    }
                    else
                    {
                        lblMessage.Text = "Your Password is not changed successfully, please try again...!";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                return;
            }
        }

        protected void BtnChange_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PSalesTouchPointUser_Insert UpdateUser = FillUser(txtUsername.Text);
                if (new BSalesTouchPoint().ValidateChangePassword(UpdateUser, txtOldPassword.Text.Trim(), txtNewPassword.Text.Trim(), txtRetypePassword.Text, "Change") == true)
                {
                    UpdateUser.ModifiedBy = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID;
                    UpdateUser.Password = LMSHelper.EncodeString(txtRetypePassword.Text);
                    UpdateUser.PasswordExpirationDate = DateTime.Now.AddMonths(3);
                    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
                    txtUsername.Text = UpdateUser.ContactNumber;
                    txtPassword.Text = txtRRetypePassword.Text;
                    FldSignin.Visible = true;
                    FldRegister.Visible = false;
                    FldVerifyEmailOTP.Visible = false;
                    FldVerifyMobileOTP.Visible = false;
                    FldResetPassword.Visible = false;
                    FldChangePassword.Visible = false;
                    btnLogin_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                return;
            }
        }
        protected void BtnEmailOTPBack_Click(object sender, EventArgs e)
        {
            FldVerifyEmailOTP.Visible = false;
            FldVerifyMobileOTP.Visible = false;
            FldRegister.Visible = true;
            FldSignin.Visible = false;
            FldResetPassword.Visible = false;
            FldChangePassword.Visible = false;
        }
        protected void BtnMobileOTPBack_Click(object sender, EventArgs e)
        {
            FldVerifyEmailOTP.Visible = false;
            FldVerifyMobileOTP.Visible = false;
            FldRegister.Visible = true;
            FldSignin.Visible = false;
            FldResetPassword.Visible = false;
            FldChangePassword.Visible = false;
        }
    }
}