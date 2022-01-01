using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public static class UIHelper
    {
        private const String redirectToPasswordChange = "/ChangePassword.aspx";
        //private const String redirectOnSuccessfulLogin = "Home.aspx";
        //private const String redirectOnCreateUserCancel = "Home.aspx";
        private const String sessionFailureRedirectionPage = "/Login.aspx";
        //private const String redirectOnCreateSupplierASNCancel = "Home.aspx";
        private const String redirectOnUserManagementCancel = "/UserManagement.aspx";
        private const String redirectOnCreateUserSuccess = "/AuthorizeUser.aspx";
        private const String redirectOnAccessViolation = "/ForbidAccess.aspx";
        private const String redirectAfterChangePassword = "/Login.aspx";
        //private const String redirectOnChangePasswordCancel = "Home.aspx";
        private const String redirectToHomePage = "/Home.aspx";
        private const String redirectToCreateCircularPage = "/PlantCirculars.aspx";
        private const String redirectToViewCircularPage = "/ViewPlantCircular.aspx";

        public static String RedirectToCreateCircularPage
        {
            get
            {
                return redirectToCreateCircularPage;
            }
        }
        public static String RedirectToHomePage
        {
            get
            {
                return redirectToHomePage;
            }
        }

        public static String RedirectAfterChangePassword
        {
            get
            {
                return redirectAfterChangePassword;
            }
        }


        public static String SessionFailureRedirectionPage
        {
            get
            {
                return sessionFailureRedirectionPage;
            }
        }
        public static String RedirectToPasswordChange
        {
            get
            {
                return redirectToPasswordChange;
            }
        }


        public static String RedirectOnUserManagementCancel
        {
            get
            {
                return redirectOnUserManagementCancel;
            }
        }
        public static String RedirectOnCreateUserSuccess
        {
            get
            {
                return redirectOnCreateUserSuccess;
            }
        }
        public static String RedirectOnAccessViolation
        {
            get
            {
                return redirectOnAccessViolation;
            }
        }

        public static String RedirectToViewCircularPage
        {
            get
            {
                return redirectToViewCircularPage;
            }
        }

        public static string GetUserStatus(bool status)
        {
            if (status)
                return "Enabled";
            else
                return "Disabled";

        }
        public static string GetUserLockedStatus(bool isLocked)
        {
            if (isLocked)
                return "Yes";
            else
                return "No";

        }

        public static void UserAudit()
        {
            new BUser().UserAudit(new PUserAudit()
            {
                Browser = PSession.UserBrowser,
                IPAddress = PSession.UserIPAddress,
                IsSessionExpired = false,
                LoginDate = DateTime.Now,
                LogoutDate = DateTime.Now,
                SesionId = PSession.SessionId,
                UserId = PSession.User.UserID
            });

        }
        public static string GetFileName(string fileNameStartsWith, string extension)
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", fileNameStartsWith, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, extension);
        }
        public static void ShowHideGridviewColumns(GridView gridView, List<string> columnHeaders, bool status)
        {
            foreach (DataControlField col in gridView.Columns)
            {
                if (columnHeaders.Contains(col.HeaderText))
                    col.Visible = status;
            }
        }

    }

    static class MailFormate
    {

        public static string MailTsir = "<html><head> <style>  p.MsoNormal, li.MsoNormal, div.MsoNormal	{margin:0cm;margin-bottom:.0001pt;font-size:11.0pt;font-family:\"Calibri\",sans-serif;mso-fareast-language:EN-US;}</style> </head>"
 + "<body><table width=\"600px\"><tr><td style=\"height: 100px; width: 600px;\"><p><span>Dear Sir / Madam,</span></p><p><span><b>Please refer attached field complaint. Request you to look into this matter on priority.<br/><br/></b></span></p><br></td></tr></table>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>Regards,</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><b><span style='font-family:\"Swis721 Lt BT\";color:#002060;mso-fareast-language:EN-IN'>@@Name</span></b><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-family:\"Swis721 BT\",sans-serif;color:#7F7F7F;mso-fareast-language:EN-IN'>@@Designation</span><span style='font-family:\"Swis721 BT\",sans-serif;color:#1F497D;mso-fareast-language:EN-IN'><o:p></o:p></span></p><p class=MsoNormal><span style='font-family:\"Swis721 BT\",sans-serif;color:#1F497D;mso-fareast-language:EN-IN'><o:p>&nbsp;</o:p></span></p>"
 + "<p class=MsoNormal><b><span style='font-family:\"Swis721 Lt BT\";color:#002060;mso-fareast-language:EN-IN'>AJAX ENGINEERING PVT. LTD.</span></b><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-family:\"Swis721 Lt BT\";color:#7F7F7F;mso-fareast-language:EN-IN'>(Formerly AJAX FIORI ENGINEERING (I) PVT. LTD.)</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'> <o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'># 3, 16&amp;17, KIADB Industrial Area, Bashettyhalli</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>Doddaballapur &#8211; 561203, Karnataka, India.</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>Toll free No.: 1-800-419-0628</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>  (M): @@Phone</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>E-mail: </span><span lang=EN-GB><a href=\"mailto:@@MailID\"><span lang=EN-IN style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";mso-fareast-language:EN-IN'>@@MailID</span></a>"
 + "</span><span lang=EN-GB style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'> </span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p></body></html>";




        public static string ForgotPassword = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">"
 + "<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><title>User Authorized</title></head><body><div><table width=\"600px\"><tr><td><p><strong>Subject: : Password reset request for Ajax DMS</strong></p>"
 + "</td></tr><tr><td style=\"height: 200px; width: 600px;\"><p><span>Dear <span>@@Addresse</span>,</span></p><p style=\"margin: 0in 0in 0pt\">You requested to have your password reset, below is your new password."
 + "</p><br /><p style=\"margin: 0in 0in 0pt\">Your account details are:</p> <p style=\"margin: 0in 0in 0pt\"> Login Id : @@UserName <br />  Password : @@Password<br /> </p> </td>"
 + "</tr> <tr> <td style=\"height: 60px; width: 600px;\">  <p> After logging in, you will be redirected to change password page to change your password.</p> <span> <br />"
 + "Thanks,<br /> Ajax DMS </span> </td></tr> </table> </div> </body> </html>";

    }
}