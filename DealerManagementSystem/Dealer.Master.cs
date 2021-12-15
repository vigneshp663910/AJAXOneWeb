using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                lblWelcome.Text = PSession.User.ContactName;


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
                DivMenu.InnerHtml = MenuCon; 
                menu();
            }
        }
       
        string ReMenu(List<PSubModuleAccess> SMs, string SubModuleName, string MenuDMS)
        {
            List<PSubModuleAccess> SMsCount = SMs.Where(x => x.ParentMenu == SubModuleName).ToList();
            if (SMsCount.Count() == 0)
            {
                return MenuDMS + "<li><a href='/DMS_CampignTicketCreate.aspx'>"+ SMs[0].SubModuleName + "</a></li>";
            }
            foreach (PSubModuleAccess SM in SMsCount)
            {
                List<PSubModuleAccess> PA1s = SMs.Where(x => x.ParentMenu == SM.SubModuleName).ToList();
                if (PA1s.Count() == 0)
                {
                    MenuDMS = MenuDMS + "<li><a href='/DMS_CampignTicketCreate.aspx'>"+ SM.SubModuleName + "</a></li>";
                }
                else
                {
                    MenuDMS = MenuDMS + "<li> <a href=''#'>"+ SM.SubModuleName + "</a><ul>";
                    MenuDMS =  ReMenu(PA1s, SM.SubModuleName, MenuDMS);
                    MenuDMS = MenuDMS + "</ul> </li>";
                }
            }
            return MenuDMS;
        }
        private void menu()
        { 
            string MenuDMS = "<nav id='main-nav'><ul id='main-menu' class='sm sm-blue'>"; 
            foreach (PModuleAccess AM in PSession.User.DMSModules)
            {
                MenuDMS = MenuDMS + "<li> <a href='#'>" + AM.ModuleName + "</a> <ul>";
                MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS);
                MenuDMS = MenuDMS + "</ul></li>";
            }
            MenuDMS = MenuDMS + "</ul></nav>";
            bluemenu.InnerHtml = MenuDMS;
        }
    }
}