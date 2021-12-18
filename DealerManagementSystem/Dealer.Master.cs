using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
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
                if (PSession.User == null)
                {
                    Response.Redirect(UIHelper.SessionFailureRedirectionPage);
                }

                //    lblWelcome.Text = PSession.User.ContactName;
                //lblusername.Text = PSession.Emp.EmployeeName;
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
            List<PSubModuleAccess> SMsCount = SMs.Where(x => x.ParentMenu == SubModuleName).ToList();
            if (SMsCount.Count() == 0)
            {
                //return MenuDMS + "<li><a href='~/" + SMs[0].ModuleAction + ".aspx'>" + SMs[0].DisplayName1 + "</a></li>";
                return MenuDMS + "<a href='/" + SMs[0].ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SMs[0].DisplayName1.ToString() + "','" + MainMenu + "')>" + SMs[0].DisplayName1 + "</a>";
            }
            foreach (PSubModuleAccess SM in SMsCount)
            {
                List<PSubModuleAccess> PA1s = SMs.Where(x => x.ParentMenu == SM.SubModuleName).ToList();
                if (PA1s.Count() == 0)
                {
                   // MenuDMS = MenuDMS + "<li><a href='/" + SM.ModuleAction + ".aspx'>" + SM.DisplayName1 + "</a></li>";
                    MenuDMS = MenuDMS + "<a href='/" + SM.ModuleAction + ".aspx' class='w3-bar-item w3-button' onclick=ParentMenuClick('" + SM.DisplayName1.ToString() + "','" + MainMenu + "')>" + SM.DisplayName1 + "</a>";

                }
                else
                {
                    MenuDMS += "<a onclick=Menu('" + SM.DisplayName1 + "','" + SM.DisplayName1 + "') href='javascript:void(0)' class='w3-button w3-block w3-blue w3-left-align submenu' id='Menu" + SM.DisplayName1 + "'>" + SM.DisplayName1 + "<i class='fa fa-caret-down fa-2x'></i></a>";
                    MenuDMS += "<div id='" + SM.DisplayName1 + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";

                   // MenuDMS = MenuDMS + "<li> <a href=''#'>"+ SM.DisplayName1 + "</a><ul>";
                    MenuDMS =  ReMenu(PA1s, SM.SubModuleName, MenuDMS);
                    MenuDMS += "</div>";
                    // MenuDMS = MenuDMS + "</ul> </li>";
                }
            }
            return MenuDMS;
        }
        private void menu()
        { 
            string MenuDMS = "<nav id='main-nav'><ul id='main-menu' class='sm sm-blue'>"; 
            foreach (PModuleAccess AM in PSession.User.DMSModules)
            {

                MenuDMS += "<a href='javascript:void(0)'   onclick=Menu('" + AM.ModuleName + "')  class='w3-button w3-block w3-blue w3-left-align' id='Menu" + AM.ModuleName + "'>" + AM.ModuleName + "<i class='fa fa-caret-down fa-2x'></i></a>";
                MenuDMS += "<div id='" + AM.ModuleName + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";
                MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS);
                MenuDMS += "</div>";

                //MenuDMS = MenuDMS + "<li> <a href='#'>" + AM.ModuleName + "</a> <ul>";
                //MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS);
                //MenuDMS = MenuDMS + "</ul></li>";
            }
            MenuDMS += "</div>";
            //  MenuDMS = MenuDMS + "</ul></nav>";
            Menu.InnerHtml = MenuDMS;
        }
    }
}