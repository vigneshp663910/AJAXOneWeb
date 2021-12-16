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
    public partial class DMSDynamic : System.Web.UI.MasterPage
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
        string ReMenu(List<PSubModuleAccess> SMs, string SubModuleName, string MenuDMS)
        {
            List<PSubModuleAccess> SMsCount = SMs.Where(x => x.ParentMenu == SubModuleName).ToList();
            if (SMsCount.Count() == 0)
            {
                return MenuDMS + "<a href='Home.aspx' onclick='w3_close()' class='w3-bar-item w3-button'>" + SMs[0].SubModuleName + "</a>";
            }
            foreach (PSubModuleAccess SM in SMsCount)
            {
                List<PSubModuleAccess> PA1s = SMs.Where(x => x.ParentMenu == SM.SubModuleName).ToList();
                if (PA1s.Count() == 0)
                {
                    MenuDMS = MenuDMS + "<a href='Home.aspx' onclick='w3_close()' class='w3-bar-item w3-button'>" + SM.SubModuleName + "</a>";
                }
                else
                {
                    MenuDMS += "<a onclick='Menu" + SM.SubModuleName + "()' href='javascript:void(0)' class='w3-button w3-block w3-blue w3-left-align' id='Menu" + SM.SubModuleName + "'>" + SM.SubModuleName + "<i class='fa fa-caret-down fa-2x'></i></a>";
                    MenuDMS += "<div id='" + SM.SubModuleName + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";
                    MenuDMS = ReMenu(PA1s, SM.SubModuleName, MenuDMS);
                    MenuDMS += "</div>";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(" <script> ");
                    sb.Append(" function Menu" + SM.SubModuleName + "() { ");
                    sb.Append(" var x = document.getElementById('" + SM.SubModuleName + "'); ");
                    sb.Append(" if (x.className.indexOf('w3 - show') == -1) { ");
                    sb.Append(" x.className += ' w3 - show'; ");
                    sb.Append(" } else { ");
                    sb.Append(" x.className = x.className.replace(' w3 - show', ''); ");
                    sb.Append(" } ");
                    sb.Append(" } ");
                    sb.Append(" </script> ");
                    Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:Menu" + SM.SubModuleName + "(); ", true);


                    //MenuDMS = MenuDMS + "<li> <a href=''#'>" + SM.SubModuleName + "</a><ul>";
                    //MenuDMS = ReMenu(PA1s, SM.SubModuleName, MenuDMS);
                    //MenuDMS = MenuDMS + "</ul> </li>";
                }
            }
            return MenuDMS;
        }
        private void menu()
        {
            string MenuDMS = "";
            foreach (PModuleAccess AM in PSession.User.DMSModules)
            {
                MenuDMS += "<a onclick='Menu"+ AM.ModuleName + "()' href='javascript:void(0)' class='w3-button w3-block w3-blue w3-left-align' id='Menu" + AM.ModuleName + "'>" + AM.ModuleName + "<i class='fa fa-caret-down fa-2x'></i></a>";
                MenuDMS += "<div id='" + AM.ModuleName + "' class='w3-bar-block w3-hide w3-padding-large w3-medium' runat='server'>";
                MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS);
                MenuDMS += "</div>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" <script> ");
                sb.Append(" function Menu" + AM.ModuleName + "() { ");
                sb.Append(" var x = document.getElementById('" + AM.ModuleName + "'); ");
                sb.Append(" if (x.className.indexOf('w3 - show') == -1) { ");
                sb.Append(" x.className += ' w3 - show'; ");
                sb.Append(" } else { ");
                sb.Append(" x.className = x.className.replace(' w3 - show', ''); ");
                sb.Append(" } ");
                sb.Append(" } ");
                sb.Append(" </script> ");
                Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:Menu" + AM.ModuleName + "(); ", true);
                //MenuDMS = ReMenu(AM.SubModuleAccess, AM.ModuleName, MenuDMS);
                //MenuDMS = MenuDMS + "</ul></li>";
            }
            //MenuDMS = MenuDMS + "</ul></nav>";
            MenuDMS += "</div>";
            Menu.InnerHtml = MenuDMS;
        }
    }
}