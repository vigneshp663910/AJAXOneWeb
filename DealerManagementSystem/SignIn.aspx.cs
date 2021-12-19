using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace DealerManagementSystem
{
    public partial class SignIn : System.Web.UI.Page
    {

        //string ls_con = AppVariable.gsConnection_MRPINVENT;


        protected void Page_Load(object sender, EventArgs e)
        {
            //txtLoginID.Text = AJX_Apps.Global.gs_WinAdUserId; 
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //SqlConnection con = null;

            //try
            //{
            //    string ls_sql = string.Empty;
            //    string ls_loginname = txtLoginID.Text.Trim().ToUpper();
            //    string ls_loginpw = txtPassword.Text.Trim();

            //    Session["AJAX_USER"] = "true";
            //    ls_sql = "SELECT x.LoginId, x.Password, x.Active, x.LoginName, '', '', XRefCode = convert(varchar, x.LoginId) FROM coTgLogins x WHERE UPPER(x.LoginName) = '" + ls_loginname + "'";


            //    DataTable dtbl = new DataTable();
            //    using (SqlConnection sqlCon = new SqlConnection(ls_con))
            //    {
            //        sqlCon.Open();
            //        SqlDataAdapter sde = new SqlDataAdapter(ls_sql, sqlCon);
            //        sde.Fill(dtbl);
            //    }

            //    if (dtbl.Rows.Count > 0)
            //    {

            //        string ls_act_pw = dtbl.Rows[0][1].ToString();
            //        string ls_active = dtbl.Rows[0][2].ToString();

            //        if (ls_loginpw != ls_act_pw) { lblMessage.Text = "Invalid Password !"; return; }
            //        if (ls_active == "N") { lblMessage.Text = "Your Login Account is Not Active."; return; }


            //        AJX_Apps.Function.f_Return_DataTable rDt = new AJX_Apps.Function.f_Return_DataTable();
            //        rDt.as_sql = "SELECT x.EmpId,x.EmployeeName, x.Phone, x.Mail, DeptId = convert(varchar, x.DepartmentID), x.DepartmentName, x.EmpGroup FROM hrViEmployee x WHERE UPPER(x.EmployeeUserID) = '" + ls_loginname + "'";
            //        DataTable dtEmpInfo = rDt.GetData();

            //        if (dtEmpInfo.Rows.Count > 0)

            //        {
                        
            //            Session["LoginId"] = ls_loginname.ToLower();
            //            Session["LoginName"] = dtEmpInfo.Rows[0]["EmployeeName"].ToString();
            //            Session["EmployeeNo"] = dtEmpInfo.Rows[0]["EmpId"].ToString();
            //            Session["MobileNo"] = dtEmpInfo.Rows[0]["Phone"].ToString();
            //            Session["EmailID"] = dtEmpInfo.Rows[0]["Mail"].ToString();
            //            Session["XRefCode"] = dtEmpInfo.Rows[0]["EmpId"].ToString();
            //            Session["FinYear"] = "2021-22";
            //            Session["Appraisee"] = dtEmpInfo.Rows[0]["EmpId"].ToString();
            //            Session["AppraiseeName"] = dtEmpInfo.Rows[0]["EmployeeName"].ToString();
            //            Session["DeptId"] = dtEmpInfo.Rows[0]["DeptId"].ToString();
            //            Session["DeptName"] = dtEmpInfo.Rows[0]["DepartmentName"].ToString();

            //            string ls_PANo = dtEmpInfo.Rows[0]["EmpGroup"].ToString();

            //            if (ls_PANo == "A") { ls_PANo = "1"; }
            //            if (ls_PANo == "B") { ls_PANo = "0"; }


            //            Session["AppraiseeStatus"] = ls_PANo;
            //            Session["AJAX_USER"] = "true";


            //            if (DropDownList1.Text == "ePMS")
            //            {

            //                Response.Redirect("ePMS.aspx?App=ePMS");
            //            }
            //            else if (DropDownList1.Text == "SmartShopFloor")
            //            {
            //                Response.Redirect("~/Digitization/Index.aspx");
            //            }
            //            else if (DropDownList1.Text == "Kaizen")
            //            {
            //                Response.Redirect("~/eKaizen/eKaizen.aspx");
            //            }

            //        }
            //        else
            //        {

            //            lblMessage.Text = "No Such User ID / Employee Exist !";
            //        }
            //    }
            //    else
            //    {
            //        lblMessage.Text = "Invalid Login ID !";
            //    }

            //}
            //catch (Exception e7)
            //{
            //    lblMessage.Text = e7.Message;
            //}

        }

        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            ////SqlConnection con = null;

            //string ls_loginname = txtLoginID.Text.Trim().ToString().ToUpper();

            //if (ls_loginname == "") { lblMessage.Text = "Please specify your Login Account Id."; return; }

            //try
            //{
            //    string ls_sql;


            //    ls_sql = "SELECT SignInId, aPassword, Active, FirstName, LastName, MobileNo FROM enTiSignIn WHERE UPPER(LoginName) = '" + ls_loginname + "'";

            //    DataTable dtbl = new DataTable();
            //    using (SqlConnection sqlCon = new SqlConnection(ls_con))
            //    {
            //        sqlCon.Open();
            //        SqlDataAdapter sde = new SqlDataAdapter(ls_sql, sqlCon);
            //        sde.Fill(dtbl);
            //    }

            //    if (dtbl.Rows.Count > 0)
            //    {
            //        string ls_loginpw = dtbl.Rows[0][1].ToString();
            //        string ls_firstname = dtbl.Rows[0][3].ToString();
            //        string ls_lastname = dtbl.Rows[0][4].ToString();
            //        string ls_mobileno = dtbl.Rows[0][5].ToString();

            //        //lblMessage.Text = "Your Login Account Password is " + dtbl.Rows[0][1].ToString();
            //        Mail_Password(ls_loginname, ls_loginpw, ls_firstname, ls_firstname, ls_mobileno);

            //    }
            //    else
            //    {
            //        lblMessage.Text = "Invalid Login Account Id Specified !";

            //    }

            //}
            //catch (Exception e7)
            //{
            //    lblMessage.Text = e7.Message;
            //}

        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            string ls_loginid = Request.QueryString["LoginId"];
            if (ls_loginid != null)
            {
                txtLoginID.Text = ls_loginid;
            }
        }


        void Mail_Password(string ls_login_id, string ls_login_pw, string ls_firstname, string ls_lastname, string ls_mobileno)
        {
            //try
            //{
            //    MailMessage mailMessage = new MailMessage();
            //    mailMessage.To.Add(ls_login_id);
            //    mailMessage.From = new MailAddress("mis@ajax-engg.com");
            //    mailMessage.Subject = "Your e-Catalogue Login Account Password";
            //    mailMessage.Body = "Dear " + ls_firstname + ",\n\nPlease find your e-Catalogue Login Account Password : " + ls_login_pw + "\n\n";
            //    SmtpClient smtpClient = new SmtpClient();
            //    smtpClient.Host = "smtp.office365.com";
            //    smtpClient.Port = 587;
            //    smtpClient.UseDefaultCredentials = false;
            //    smtpClient.Credentials = new System.Net.NetworkCredential("mis@ajax-engg.com", "ajax@321");
            //    //Or your Smtp Email ID and Password
            //    smtpClient.EnableSsl = true;
            //    mailMessage.IsBodyHtml = true;
            //    smtpClient.Send(mailMessage);

            //    lblMessage.Text = "Your Login Account Password is mailed to your mail id as well to your registered Mobile No.Please check.";

            //    f_QuickSMS(ls_mobileno, ls_login_pw);
            //    //Response.Redirect("SignIn?LoginId=" + txtLoginID.Text.Trim() + "&Status=P");

            //}
            //catch (Exception ex)
            //{

            //    lblMessage.Text = "Could not send an e-mail - error: " + ex.Message + "\n\nPlease notify to IT Admin";
            //}
        }

        void f_QuickSMS(string ls_mobileno, string ls_pw)
        {

            //string result = ""; WebRequest request = null;
            //HttpWebResponse response = null;
            //try
            //{
            //    string ls_url = "https://enterprise.smsgupshup.com/GatewayAPI/rest?method=SendMessage&send_to=" + ls_mobileno + "&msg=" + ls_pw + " is the password of your AJAX e-Catalogue Account. Thank You, Team Ajax.&msg_type=TEXT&userid=2000138608&auth_scheme=plain&password=Gupsms@123&v=1.1&format=text";
            //    request = WebRequest.Create(ls_url);
            //    // Send the 'HttpWebRequest' and wait for response. 
            //    response = (HttpWebResponse)request.GetResponse();
            //    Stream stream = response.GetResponseStream();
            //    Encoding ec = System.Text.Encoding.GetEncoding("utf-8"); StreamReader reader = new

            //    System.IO.StreamReader(stream, ec);
            //    result = reader.ReadToEnd(); Console.WriteLine(result);
            //    reader.Close();
            //    stream.Close();
            //}
            //catch (Exception exp)
            //{
            //    Console.WriteLine(exp.ToString());
            //}
            //finally
            //{
            //    if (response != null) response.Close();
            //}
        }
        public static string DecodeString(string value)
        {
            Byte[] decodeValueToBytes = Convert.FromBase64String(value);
            return ASCIIEncoding.ASCII.GetString(decodeValueToBytes);
        }
    }

}



