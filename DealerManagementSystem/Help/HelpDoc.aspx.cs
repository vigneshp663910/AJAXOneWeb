using System;

namespace DealerManagementSystem.Help
{
    public partial class HelpDoc : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Help » Contents');</script>");

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string ls_filename = Request.QueryString["aFileName"];
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1000px\" height=\"1200px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            //ls_filename = "~/HelpFile/" + ls_filename;
            //ls_filename = "~/Help/Files/" + ls_filename;

            ltEmbed.Text = string.Format(embed, ResolveUrl(ls_filename)); ;

        }
    }
}

