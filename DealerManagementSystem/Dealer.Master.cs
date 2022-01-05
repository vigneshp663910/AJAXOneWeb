using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class Dealer : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPageName.Text = (string)Session["PageName"];
            Session["PageName"] = "";
            if (!IsPostBack)
            {
                lblQuality.Text = ConfigurationManager.AppSettings["IsQuality"];
                //var retrievedPerson = JSON.parse(localStorage.getItem('person'));
                //var x = sessionStorage.getItem("test1");
                lblProjectTitle.Text = "&nbsp;";
                if (PSession.User == null)
                {
                    Response.Redirect(UIHelper.SessionFailureRedirectionPage);
                }
                //    lblWelcome.Text = PSession.User.ContactName;
                lblusername.Text = PSession.User.ContactName;

                string MenuCon = "<ul id='topnav'>";
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    MenuCon = MenuCon + "<li><a title='Services' href='CreateTicket.aspx'>New support ticket</a></li>";
                    MenuCon = MenuCon + "<li><a title='Enquiries' href='ManageTickets.aspx'>Check ticket status </a></li>";
                    if (new BFeedback().CheckPendingFeedback(PSession.User.UserID))
                    {
                        MenuCon = MenuCon + "<li><a title='Feedback' href='Feedback.aspx'>Feed back</a></li>";
                    }
                    //divbluemenu.Visible = false;
                }
                else
                {
                    // divbluemenu.Visible = true;
                }
                MenuCon = MenuCon + "<li style='float: right; margin-top: 0px;'><ul style='list-style-type: none;'>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='Home.aspx' style='white-space: pre;'><img src='Ajax/HomeLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='ChangePassword.aspx' style='white-space: pre;'><img src='Ajax/ChangePasswordLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='Login.aspx' style='white-space: pre;'><img src='Ajax/SignOutLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + " <li><a href='DMS_ContactUs.aspx' style='white-space: pre;'><img src='Ajax/ContactUsLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "</ul></li></ul>";
                Menu.InnerHtml = MenuCon;
                menu();
            }
        }

        string ReMenu(List<PSubModuleAccess> SMs, string SubModuleName, string MenuDMS, [Optional] string MainMenu)
        {
            List <PSubModuleAccess> SMsCount = SMs.Where(x => x.ParentMenu == SubModuleName).ToList();
            if (SMsCount.Count() == 0)
            { return MenuDMS + "<a href='/" + SMs[0].ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SMs[0].DisplayName1.Replace(" ", "") + "','" + MainMenu + "')>" + SMs[0].DisplayName1 + "</a>"; }
            foreach (PSubModuleAccess SM in SMsCount)
            {
                List<PSubModuleAccess> PA1s = SMs.Where(x => x.ParentMenu == SM.SubModuleName).ToList();
                if (PA1s.Count() == 0)
                { MenuDMS = MenuDMS + "<a href='/" + SM.ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SM.ParentMenu.Replace(" ", "") + "','" + MainMenu + "')>" + SM.DisplayName1.Replace(" ", "") + "</a>"; }
                else
                {
                    MenuDMS += "<a onclick=Menu('" + SM.DisplayName1 + "','" + SM.ParentMenu.Replace(" ", "") + "','i" + SM.DisplayName1.Replace(" ", "") + "') href='javascript:void(0)' class='w3-button w3-block w3-blue w3-left-align submenu' id='Menu" + SM.DisplayName1.Replace(" ", "") + "'>" + SM.DisplayName1 + "<i id='i" + SM.DisplayName1.Replace(" ", "") + "' class='fa fa-angle-double-down fa-2x'></i></a>";
                    MenuDMS += "<div id='" + SM.DisplayName1.Replace(" ", "") + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";
                    MenuDMS = ReMenu(PA1s, SM.SubModuleName, MenuDMS, SM.ParentMenu);
                    MenuDMS += "</div>";
                }
            }
            return MenuDMS;
        }
        private void menu()
        {
            string MenuDMS = "<nav id='main-nav'><ul id='main-menu' class='sm sm-blue'>";
            //MenuDMS += "<a href='/Home.aspx' onclick=Menu('Home','Home') class='w3-button w3-block w3-blue w3-left-align'>Home</a>";
            foreach (PModuleAccess AM in PSession.User.DMSModules)
            {
                MenuDMS += "<a href='javascript:void(0)'   onclick=Menu('" + AM.ModuleName.Replace(" ", "") + "','','i" + AM.ModuleName.Replace(" ", "") + "')  class='w3-button w3-block w3-blue w3-left-align' id='Menu" + AM.ModuleName.Replace(" ", "") + "'>" + AM.ModuleName + "<i id='i" + AM.ModuleName.Replace(" ", "") + "' class='fa fa-angle-down fa-2x'></i></a>";
                MenuDMS += "<div id='" + AM.ModuleName.Replace(" ", "") + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";
                MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS, AM.ModuleName.Replace(" ", ""));
                MenuDMS += "</div>";
            }
            MenuDMS += "</div>";
            Menu.InnerHtml = MenuDMS;
        }
    }
}

