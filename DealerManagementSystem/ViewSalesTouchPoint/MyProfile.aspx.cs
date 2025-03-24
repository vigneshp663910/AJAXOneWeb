using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSalesTouchPoint
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession_STP.SalesTouchPointUser == null)
            {
                Response.Redirect(UIHelper.RedirectToSalesTouchPointLogin);
            }
        }
        public PSalesTouchPointUser GetSalesTouchPointUserByID
        {
            get
            {
                if (ViewState["GetSalesTouchPointUserByID"] == null)
                {
                    ViewState["GetSalesTouchPointUserByID"] = new PSalesTouchPointUser();
                }
                return (PSalesTouchPointUser)ViewState["GetSalesTouchPointUserByID"];
            }
            set
            {
                ViewState["GetSalesTouchPointUserByID"] = value;
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
        private string OTPType
        {
            get
            {
                if (ViewState["OTPType"] == null)
                {
                    ViewState["OTPType"] = "";
                }
                return (string)ViewState["OTPType"];
            }
            set
            {
                ViewState["OTPType"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('My Profile');</script>");
            if (!IsPostBack)
            {
                GetSalesTouchPointUserByID = new BSalesTouchPoint().GetSalesTouchPointUserByID(PSession_STP.SalesTouchPointUser.SalesTouchPointUserID);
                Fill();
            }
        }
        void Fill()
        {
            PSalesTouchPointUser User = GetSalesTouchPointUserByID;
            lblAadhaarNumber.Text = User.AadharNumber.ToString();
            lblFullName.Text = User.Name;
            lblAddress1.Text = User.Address1;
            lblAddress2.Text = User.Address2;

            if (User.State != null)
            {
                lblState.Text = User.State.State;
                if (User.District != null)
                {
                    lblDistrict.Text = User.District.District;
                }
            }
            lblEmail.Text = "<a href=MAILTO:" + User.EmailID + '>' + User.EmailID + "</a>";
            lblContactNo.Text = "<a href=TEL:" + User.ContactNumber + '>' + User.ContactNumber + "</a>";
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            PSalesTouchPointUser User = GetSalesTouchPointUserByID;
            txtAadhaarNumber.Text = User.AadharNumber.ToString();
            txtName.Text = User.Name;
            txtMobileNumber.Text = User.ContactNumber;
            VerifyMobileOTP.ImageUrl = "~/Images/Verified.jpg";
            txtEmail.Text = User.EmailID;
            //VerifyEmailOTP.ImageUrl = "~/Images/Verified.jpg";
            txtAddress1.Text = User.Address1;
            txtAddress2.Text = User.Address2;
            new DDLBind(ddlState, new BOnboardEmployee().GetState(null, null, null, null, null), "State", "StateID", true, "Select");
            ddlState.SelectedValue = (User.State == null) ? "0" : User.State.StateID.ToString();
            new DDLBind(ddlDistrict, new BOnboardEmployee().GetDistrict(null, null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID", true, "Select");
            ddlDistrict.SelectedValue = (User.District == null) ? "0" : User.District.DistrictID.ToString();
            FldUpdateProfile.Visible = true;
            FldViewProfile.Visible = false;
            FldResetOTP.Visible = false;
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BOnboardEmployee().GetDistrict(null, null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID", true, "Select");
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            if (!Validation())
            {
                return;
            }

            PSalesTouchPointUser_Insert User = FillUser();
            User.ModifiedBy = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID;
            User.AadharNumber = txtAadhaarNumber.Text.Trim();
            User.Name = txtName.Text.Trim();
            User.ContactNumber = txtMobileNumber.Text.Trim();
            User.EmailID = txtEmail.Text.Trim();
            User.Address1 = txtAddress1.Text.Trim();
            User.Address2 = txtAddress2.Text.Trim();
            User.StateID = Convert.ToInt32(ddlState.SelectedValue);
            User.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            User.IsActive = true;
            User.IsLocked = false;
            User.IsEnabled = true;
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
                FldResetOTP.Visible = false;
                FldUpdateProfile.Visible = false;
                FldViewProfile.Visible = true;
                GetSalesTouchPointUserByID = new BSalesTouchPoint().GetSalesTouchPointUserByID(PSession_STP.SalesTouchPointUser.SalesTouchPointUserID);
                Fill();
            }
            else
            {
                lblMessage.Text = "SalesTouchPoint User is not updated successfully";
            }
        }

        protected void btnMobileVerify_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtMobileNumber.Text))
            {
                lblMessage.Text = "Invalid mobile no...!";
                return;
            }
            if (txtMobileNumber.Text == GetSalesTouchPointUserByID.ContactNumber)
            {
                lblMessage.Text = "Use diffrent mobile no...!";
                return;
            }
            OTPType = "Mobile";
            OTP(OTPType);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "MobileOTP", "MobileOTP()", true);
            txtOTP.Visible = true;
            BtnVerifyOTP.Visible = true;
            VerifyMobileOTP.Visible = true;
            FldResetOTP.Visible = true;
            FldUpdateProfile.Visible = false;
            FldViewProfile.Visible = false;
        }

        protected void btnEmailVerify_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblMessage.Text = "Invalid Email ID...!";
                return;
            }
            if (txtEmail.Text == GetSalesTouchPointUserByID.EmailID)
            {
                lblMessage.Text = "Use diffrent email id...!";
                return;
            }
            OTPType = "Email";
            OTP(OTPType);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "EmailOTP", "EmailOTP()", true);
            txtOTP.Visible = true;
            BtnVerifyOTP.Visible = true;
            //VerifyEmailOTP.Visible = true;
            FldResetOTP.Visible = true;
            FldUpdateProfile.Visible = false;
            FldViewProfile.Visible = false;
        }

        protected void BtnVerifyOTP_Click(object sender, EventArgs e)
        {
            FldResetOTP.Visible = false;
            FldViewProfile.Visible = false;
            FldUpdateProfile.Visible = true;
            if (OTPType == "Mobile")
            {
                if (MobileOTP == txtOTP.Text)
                {
                    VerifyMobileOTP.ImageUrl = "~/Images/Verified.jpg";
                    txtOTP.Text = "";
                    MobileOTP = "Verified";
                }
            }
            else
            {
                if (EmailOTP == txtOTP.Text)
                {
                    //VerifyEmailOTP.ImageUrl = "~/Images/Verified.jpg";
                    txtOTP.Text = "";
                    EmailOTP = "Verified";
                }
            }
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        private PSalesTouchPointUser_Insert FillUser()
        {
            PSalesTouchPointUser_Insert UpdateUser = new PSalesTouchPointUser_Insert();
            try
            {
                PSalesTouchPointUser User = new BSalesTouchPoint().GetSalesTouchPointUserByID(PSession_STP.SalesTouchPointUser.SalesTouchPointUserID);
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
            }
            return UpdateUser;
        }
        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;

            txtAadhaarNumber.BorderColor = Color.Silver;
            txtName.BorderColor = Color.Silver;
            txtMobileNumber.BorderColor = Color.Silver;
            txtEmail.BorderColor = Color.Silver;
            txtAddress1.BorderColor = Color.Silver;
            txtAddress2.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;

            Boolean Ret = true;
            string Message = "";

            if (txtAadhaarNumber.Text.Length != 12)
            {
                Message = "Please enter Valid AadharNo";
                Ret = false;
                txtAadhaarNumber.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Name";
                Ret = false;
                txtName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtMobileNumber.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Mobile Number";
                Ret = false;
                txtMobileNumber.BorderColor = Color.Red;
            }
            if (txtMobileNumber.Text.Trim().Count() != 10)
            {
                Message = Message + "<br/>Please check the Mobile Number";
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
            if (txtMobileNumber.Text != GetSalesTouchPointUserByID.ContactNumber)
            {
                if (MobileOTP != "Verified")
                {
                    Message = Message + "<br/>Please Verify Mobile";
                    Ret = false;
                    txtMobileNumber.BorderColor = Color.Red;
                }
                PApiResult Result = new BSalesTouchPoint().GetSalesTouchPointUserList(txtMobileNumber.Text.Trim(), null, null, null, null);
                if (JsonConvert.DeserializeObject<List<PSalesTouchPointUser>>(JsonConvert.SerializeObject(Result.Data)).Count > 0)
                {
                    Message = Message + "<br/>Please enter another mobile number";
                    Ret = false;
                    txtMobileNumber.BorderColor = Color.Red;
                }
            }
            if (txtEmail.Text != GetSalesTouchPointUserByID.EmailID)
            {
                //if (EmailOTP != "Verified")
                //{
                //    Message = Message + "<br/>Please Verify Email";
                //    Ret = false;
                //    txtEmail.BorderColor = Color.Red;
                //}
                PApiResult Result = new BSalesTouchPoint().GetSalesTouchPointUserList(null, txtEmail.Text.Trim(), null, null, null);
                if (JsonConvert.DeserializeObject<List<PSalesTouchPointUser>>(JsonConvert.SerializeObject(Result.Data)).Count > 0)
                {
                    Message = Message + "<br/>Please enter another email id";
                    Ret = false;
                    txtMobileNumber.BorderColor = Color.Red;
                }
            }

            if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Address1";
                Ret = false;
                txtAddress1.BorderColor = Color.Red;
            }
            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select State";
                Ret = false;
                ddlState.BorderColor = Color.Red;
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select District";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            return Ret;
        }

        protected void BtnSendOTP_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                OTP("Mobile");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        void OTP(string Type)
        {
            if (Type == "Mobile")
            {
                MobileOTP = RandomNumber(000000, 999999).ToString("000000");
                string messageBody = "";
                messageBody = "Dear User, Your OTP for login is " + MobileOTP + ". From AJAX ENGG";
                new EmailManager().SendSMS(txtMobileNumber.Text, messageBody);
            }
            else
            {
                EmailOTP = RandomNumber(000000, 999999).ToString("000000");
                string messageBody = MailFormate.ForgotPassword;
                messageBody = messageBody.Replace("@@Addresse", txtName.Text);
                messageBody = messageBody.Replace("@@UserName", txtName.Text);
                messageBody = messageBody.Replace("@@Password", EmailOTP);
                messageBody = messageBody.Replace("@@URL", "");
                new EmailManager().MailSend(txtEmail.Text, "", "Verify Email Request", messageBody);
            }
        }

        protected void BtnOTPBack_Click(object sender, EventArgs e)
        {
            FldResetOTP.Visible = false;
            FldViewProfile.Visible = false;
            FldUpdateProfile.Visible = true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            FldResetOTP.Visible = false;
            FldViewProfile.Visible = true;
            FldUpdateProfile.Visible = false;
        }

        protected void txtMobileNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtMobileNumber.Text != GetSalesTouchPointUserByID.ContactNumber)
            {
                if (MobileOTP != "Verified")
                {
                    VerifyMobileOTP.ImageUrl = "~/Images/NotVerified.jpg";
                }
            }
            else
            {
                VerifyMobileOTP.ImageUrl = "~/Images/Verified.jpg";
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text != GetSalesTouchPointUserByID.EmailID)
            {
                if (EmailOTP != "Verified")
                {
                    //VerifyEmailOTP.ImageUrl = "~/Images/NotVerified.jpg";
                }
            }
            else
            {
                //VerifyEmailOTP.ImageUrl = "~/Images/Verified.jpg";
            }
        }
    }
}