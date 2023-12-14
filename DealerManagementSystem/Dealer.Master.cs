using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class Dealer : System.Web.UI.MasterPage
    {
        private BasePage CurrentPage = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentPage = (BasePage)this.Page;
            Boolean BDefaultPage = false;
            lblPageName.Text = (string)Session["PageName"];
            Session["PageName"] = "";
            if (!IsPostBack)
            {
                lblServer.Text = "&nbsp;&nbsp;&nbsp;" + ConfigurationManager.AppSettings["Server"];
                //var retrievedPerson = JSON.parse(localStorage.getItem('person'));
                //var x = sessionStorage.getItem("test1");
                //lblProjectTitle.Text = "&nbsp;";
                if (PSession.User == null)
                {
                    Response.Redirect(UIHelper.SessionFailureRedirectionPage);
                }


                if ((CurrentPage.ToString() == "ASP.home_aspx") || (CurrentPage.ToString() == "ASP.myprofile_aspx") ||
                    (CurrentPage.ToString() == "ASP.aboutus_aspx") || (CurrentPage.ToString() == "ASP.account_changepassword_aspx") ||
                    (CurrentPage.ToString() == "ASP.account_signout_aspx") || (CurrentPage.ToString() == "ASP.pdf_aspx") ||
                    (CurrentPage.ToString() == "ASP.help_help_aspx") || (CurrentPage.ToString() == "ASP.undercons_aspx") ||
                     (CurrentPage.ToString() == "ASP.help_helpdoc_aspx") ||
                    (CurrentPage.ToString() == "ASP.account_myprofile_aspx") || (CurrentPage.ToString() == "ASP.account_companyprofile_aspx"))
                {
                    BDefaultPage = true;
                }

                if (!UIHelper.HasAccess((short)CurrentPage.SubModuleName) && !BDefaultPage)
                {
                    new BAPI().ApiGet("User/InsertUnauthorizedAccess?PageID=" + (short)CurrentPage.SubModuleName + "&PageName=" + CurrentPage.ToString());

                    if ((short)CurrentPage.SubModuleName != 0)
                    {
                        Session.Clear();
                        Session.Abandon();
                        Redirect(UIHelper.RedirectOnAccessViolation);
                    }
                }

                //    lblWelcome.Text = PSession.User.ContactName;
                lblusername.Text = PSession.User.ContactName;

                string MenuCon = "<ul id='topnav'>";
                //if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                //{
                //    MenuCon = MenuCon + "<li><a title='Services' href='CreateTicket.aspx'>New support ticket</a></li>";
                //    MenuCon = MenuCon + "<li><a title='Enquiries' href='ManageTickets.aspx'>Check ticket status </a></li>";
                //    if (new BFeedback().CheckPendingFeedback(PSession.User.UserID))
                //    {
                //        MenuCon = MenuCon + "<li><a title='Feedback' href='Feedback.aspx'>Feed back</a></li>";
                //    }
                //    //divbluemenu.Visible = false;
                //}
                //else
                //{
                //    // divbluemenu.Visible = true;
                //}
                MenuCon = MenuCon + "<li style='float: right; margin-top: 0px;'><ul style='list-style-type: none;'>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='Home.aspx' style='white-space: pre;'><img src='Ajax/HomeLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='ChangePassword.aspx' style='white-space: pre;'><img src='Ajax/ChangePasswordLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='Login.aspx' style='white-space: pre;'><img src='Ajax/SignOutLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + " <li><a href='DMS_ContactUs.aspx' style='white-space: pre;'><img src='Ajax/ContactUsLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "</ul></li></ul>";
                MenuNew.InnerHtml = MenuCon;
                menu();
                FillNotification();
            }

        }
        //StringBuilder ReMenu(List<PSubModuleAccess> SMs, string SubModuleName, StringBuilder MenuDMS, [Optional] string MainMenu)
        //{
        //    List<PSubModuleAccess> SMsCount = SMs.Where(x => x.ParentMenu == SubModuleName).ToList();
        //    if (SMsCount.Count() == 0)
        //    {
        //        //return MenuDMS + "<a href='/" + SMs[0].ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SMs[0].DisplayName1.Replace(" ", "") + "','" + MainMenu + "')>" + SMs[0].DisplayName1 + "</a>";
        //         MenuDMS.Append(MenuDMS + "<a href='/" + SMs[0].ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SMs[0].DisplayName1.Replace(" ", "") + "','" + MainMenu + "')><i class='fa fa-bolt' style='color: #2fb7c3'></i>&nbsp;<span>" + SMs[0].DisplayName1 + "</span></a>");
        //        return MenuDMS;
        //    }
        //    foreach (PSubModuleAccess SM in SMsCount)
        //    {
        //        List<PSubModuleAccess> PA1s = SMs.Where(x => x.ParentMenu == SM.SubModuleName).ToList();
        //        if (PA1s.Count() == 0)
        //        {
        //            MenuDMS.Append("<div class='menu-item'>");
        //            MenuDMS.Append("<a href='/" + SM.ModuleAction + ".aspx' class='w3-bar-item w3-button w3-padding' onclick=ParentMenuClick('" + SM.ParentMenu.Replace(" ", "") + "','" + MainMenu + "')><i class='fa fa-mercury' style='color: #db6e6e'></i>&nbsp;<span>" + SM.DisplayName1 + "</span></a>");
        //            MenuDMS.Append("</div>");
        //        }
        //        else
        //        {
        //            MenuDMS.Append("<div class='menu-item'>");
        //            MenuDMS.Append("<a onclick=Menu('" + SM.DisplayName1.Replace(" ", "") + "','" + SM.ParentMenu.Replace(" ", "") + "','i" + SM.DisplayName1.Replace(" ", "") + "') href='javascript:void(0)' class='w3-bar-item w3-button w3-block w3-left-align submenu' id='Menu" + SM.DisplayName1.Replace(" ", "") + "'><i class='fa fa-mercury' style='color: #db6e6e'></i>&nbsp;<span>" + SM.DisplayName1 + "</span><i id='i" + SM.DisplayName1.Replace(" ", "") + "' class='sub-menu-icon fa fa-angle-down fa-2x' style='color: lightgray'></i></a>");
        //            MenuDMS.Append("<div id='" + SM.DisplayName1.Replace(" ", "") + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>");
        //            MenuDMS  = ReMenu(PA1s, SM.SubModuleName, MenuDMS, SM.ParentMenu);
        //            MenuDMS.Append("</div>");
        //            MenuDMS.Append("</div>");
        //        }
        //    }
        //    return MenuDMS;
        //}
        //private void menu()
        //{
        //    StringBuilder MenuDMS = new StringBuilder();
        //    MenuDMS.Append("<nav id='main-nav'>");
        //    MenuDMS.Append("<ul id='main-menu' class='sm sm-blue'>");
        //    // MenuDMS += "<li class='menu-item-list'>";

        //    foreach (PModuleAccess AM in PSession.User.DMSModules)
        //    {
        //        MenuDMS.Append("<li class='menu-item-list'><a href='javascript:void(0)' onclick=Menu('" + AM.ModuleName.Replace(" ", "") + "','','i" + AM.ModuleName.Replace(" ", "") + "') class='w3-button w3-block w3-blue w3-left-align w3-padding' id='Menu" + AM.ModuleName.Replace(" ", "") + "'>" +
        //            "<i class='" + AM.ModuleAwesomeIco + " fa-sm' style='color: #2fa8b3'></i>&nbsp;<span>" + AM.ModuleName.Replace(" ", "") + "</span><i id='i" + AM.ModuleName.Replace(" ", "") + "' class='fa fa-angle-down fa-2x' style='color: lightgray'></i></a>");
        //        MenuDMS.Append("<div id='" + AM.ModuleName.Replace(" ", "") + "' class='w3-bar-block w3-hide w3-medium' runat='server'>");
        //        MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS, AM.ModuleName.Replace(" ", ""));
        //        MenuDMS.Append("</div></li>");
        //    } 
        //    MenuDMS.Append("</ul>");
        //    MenuDMS.Append("</nav>");
        //    MenuNew.InnerHtml = MenuDMS.ToString();
        //}
        string ReMenu(List<PSubModuleAccess> SMs, string SubModuleName, String MenuDMS, [Optional] string MainMenu)
        {
            List<PSubModuleAccess> SMsCount = SMs.Where(x => x.ParentMenu == SubModuleName).ToList();
            if (SMsCount.Count() == 0)
            {
                //return MenuDMS + "<a href='/" + SMs[0].ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SMs[0].DisplayName1.Replace(" ", "") + "','" + MainMenu + "')>" + SMs[0].DisplayName1 + "</a>";
                return MenuDMS + "<a href='/" + SMs[0].ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SMs[0].DisplayName1.Replace(" ", "") + "','" + MainMenu + "')><i class='fa fa-bolt' style='color: #2fb7c3'></i>&nbsp;<span>" + SMs[0].DisplayName1 + "</span></a>";
            }
            foreach (PSubModuleAccess SM in SMsCount)
            {
                List<PSubModuleAccess> PA1s = SMs.Where(x => x.ParentMenu == SM.SubModuleName).ToList();
                if (PA1s.Count() == 0)
                {
                    MenuDMS += "<div class='menu-item'>";
                    MenuDMS += "<a href='/" + SM.ModuleAction + ".aspx' class='w3-bar-item w3-button w3-padding' onclick=ParentMenuClick('" + SM.ParentMenu.Replace(" ", "") + "','" + MainMenu + "')><i class='fa fa-mercury' style='color: #db6e6e'></i>&nbsp;<span>" + SM.DisplayName1 + "</span></a>";
                    MenuDMS += "</div>";
                }
                else
                {
                    MenuDMS += "<div class='menu-item'>";
                    MenuDMS += "<a onclick=Menu('" + SM.DisplayName1.Replace(" ", "") + "','" + SM.ParentMenu.Replace(" ", "") + "','i" + SM.DisplayName1.Replace(" ", "") + "') href='javascript:void(0)' class='w3-bar-item w3-button w3-block w3-left-align submenu' id='Menu" + SM.DisplayName1.Replace(" ", "") + "'><i class='fa fa-mercury' style='color: #db6e6e'></i>&nbsp;<span>" + SM.DisplayName1 + "</span><i id='i" + SM.DisplayName1.Replace(" ", "") + "' class='sub-menu-icon fa fa-angle-down fa-2x' style='color: lightgray'></i></a>";
                    MenuDMS += "<div id='" + SM.DisplayName1.Replace(" ", "") + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";
                    MenuDMS = ReMenu(PA1s, SM.SubModuleName, MenuDMS, SM.ParentMenu);
                    MenuDMS += "</div>";
                    MenuDMS += "</div>";
                }
            }
            return MenuDMS;
        }
        private void menu()
        {
            String MenuDMS = "<nav id='main-nav'>";
            MenuDMS += "<ul id='main-menu' class='sm sm-blue'>";
            // MenuDMS += "<li class='menu-item-list'>";

            foreach (PModuleAccess AM in PSession.User.DMSModules)
            {
                MenuDMS += "<li class='menu-item-list'><a href='javascript:void(0)' onclick=Menu('" + AM.ModuleName.Replace(" ", "") + "','','i" + AM.ModuleName.Replace(" ", "") + "') class='w3-button w3-block w3-blue w3-left-align w3-padding' id='Menu" + AM.ModuleName.Replace(" ", "") + "'>" +
                    "<i class='" + AM.ModuleAwesomeIco + " fa-sm' style='color: #2fa8b3'></i>&nbsp;<span>" + AM.ModuleName.Replace(" ", "") + "</span><i id='i" + AM.ModuleName.Replace(" ", "") + "' class='fa fa-angle-down fa-2x' style='color: lightgray'></i></a>";
                MenuDMS += "<div id='" + AM.ModuleName.Replace(" ", "") + "' class='w3-bar-block w3-hide w3-medium' runat='server'>";
                MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS, AM.ModuleName.Replace(" ", ""));
                MenuDMS += "</div></li>";
            }
            //MenuDMS += "</li>";
            MenuDMS += "</ul>";
            MenuDMS += "</nav>";
            MenuNew.InnerHtml = MenuDMS;
        }
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            lblFeedbackErrMsg.Text = string.Empty;
            textfeedback.Text = string.Empty;
            Star1.Attributes.Add("class", "star outline");
            Star2.Attributes.Add("class", "star outline");
            Star3.Attributes.Add("class", "star outline");
            Star4.Attributes.Add("class", "star outline");
            Star5.Attributes.Add("class", "star outline");
            mp1.Show();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            lblFeedbackErrMsg.Text = string.Empty;
            if (textfeedback.Text == "")
            {
                lblFeedbackErrMsg.ForeColor = Color.Red;
                lblFeedbackErrMsg.Text = "Please Enter Query...!";
                lblFeedbackErrMsg.Visible = true;
                mp1.Show();
                return;
            }
            if (HiddenStar.Value == "")
            {
                lblFeedbackErrMsg.ForeColor = Color.Red;
                lblFeedbackErrMsg.Text = "Please Click Ratings...!";
                lblFeedbackErrMsg.Visible = true;
                mp1.Show();
                return;
            }
            PComment Comment = new PComment();
            Comment.ModuleNo = 203;
            Comment.UserID = PSession.User.UserID;
            Comment.Comments = textfeedback.Text.Trim();
            Comment.Ratings = Convert.ToInt32(HiddenStar.Value);
            long success = new BFeedback().coTg_Insert_AppsFeedBack(Comment);
            if (success != 0)
            {
                Star1.Attributes.Add("class", "star outline");
                Star2.Attributes.Add("class", "star outline");
                Star3.Attributes.Add("class", "star outline");
                Star4.Attributes.Add("class", "star outline");
                Star5.Attributes.Add("class", "star outline");
                HiddenStar.Value = "0";
                textfeedback.Text = string.Empty;
                mp1.Hide();
            }
        }

        protected void FeedBackClose_Click(object sender, EventArgs e)
        {
            lblFeedbackErrMsg.Text = string.Empty;
            textfeedback.Text = string.Empty;
            mp1.Hide();
        }

        protected void Star1_ServerClick(object sender, EventArgs e)
        {
            Star1.Attributes.Add("class", "star filled");
            Star2.Attributes.Add("class", "star outline");
            Star3.Attributes.Add("class", "star outline");
            Star4.Attributes.Add("class", "star outline");
            Star5.Attributes.Add("class", "star outline");
            HiddenStar.Value = "1";
            mp1.Show();
        }

        protected void Star2_ServerClick(object sender, EventArgs e)
        {
            Star1.Attributes.Add("class", "star filled");
            Star2.Attributes.Add("class", "star filled");
            Star3.Attributes.Add("class", "star outline");
            Star4.Attributes.Add("class", "star outline");
            Star5.Attributes.Add("class", "star outline");
            HiddenStar.Value = "2";
            mp1.Show();
        }

        protected void Star3_ServerClick(object sender, EventArgs e)
        {
            Star1.Attributes.Add("class", "star filled");
            Star2.Attributes.Add("class", "star filled");
            Star3.Attributes.Add("class", "star filled");
            Star4.Attributes.Add("class", "star outline");
            Star5.Attributes.Add("class", "star outline");
            HiddenStar.Value = "3";
            mp1.Show();
        }

        protected void Star4_ServerClick(object sender, EventArgs e)
        {
            Star1.Attributes.Add("class", "star filled");
            Star2.Attributes.Add("class", "star filled");
            Star3.Attributes.Add("class", "star filled");
            Star4.Attributes.Add("class", "star filled");
            Star5.Attributes.Add("class", "star outline");
            HiddenStar.Value = "4";
            mp1.Show();
        }

        protected void Star5_ServerClick(object sender, EventArgs e)
        {
            Star1.Attributes.Add("class", "star filled");
            Star2.Attributes.Add("class", "star filled");
            Star3.Attributes.Add("class", "star filled");
            Star4.Attributes.Add("class", "star filled");
            Star5.Attributes.Add("class", "star filled");
            HiddenStar.Value = "5";
            mp1.Show();
        }

        private void Redirect(string pageName)
        {
            Response.Redirect(pageName);
        }
        private void FillNotification()
        {
            List<PMessageAnnouncementHeader> MsgList = new BMessageAnnouncement().GetMessageAnnouncementHeader(null, null, null, null, PSession.User.UserID, false, DateTime.Now.ToString("yyyy-MM-dd"));
            DivNotification.Visible = false;
            NotificationCount.Visible = false;
            gvMessageAnnouncement.DataSource = null;
            gvMessageAnnouncement.DataBind();
            if (MsgList.Count > 0)
            {
                lblNotification.Text = MsgList.Count.ToString();
                NotificationCount.Visible = true;
                DivNotification.Visible = true;
                gvMessageAnnouncement.DataSource = MsgList;
                gvMessageAnnouncement.DataBind();
            }
        }
        protected void ChkReadMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkReadMessage.Checked)
            {
                bool readstatus = true;
                new BAPI().ApiPut("MessageNotification/UpdateMessageReadStatus", readstatus);
                //new BMessageAnnouncement().UpdateMessageAnnouncementItemReadStatus(Convert.ToInt64(PSession.User.UserID), true);
            }
            FillNotification();
        }
    }
}