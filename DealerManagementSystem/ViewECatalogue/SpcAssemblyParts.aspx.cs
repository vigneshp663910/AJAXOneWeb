using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcAssemblyParts : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcAssemblyParts; } }

        //public int xyUpdate
        //{
        //    get
        //    {
        //        if (ViewState["SPAssemblyImageView_xyUpdate"] == null)
        //        {
        //            ViewState["SPAssemblyImageView_xyUpdate"] = 0;
        //        }
        //        return (int)ViewState["SPAssemblyImageView_xyUpdate"];
        //    }
        //    set
        //    {
        //        ViewState["SPAssemblyImageView_xyUpdate"] = value;
        //    }
        //}
        //public int xyBulkUpdate
        //{
        //    get
        //    {
        //        if (ViewState["SPAssemblyImageView_xyBulkUpdate"] == null)
        //        {
        //            ViewState["SPAssemblyImageView_xyBulkUpdate"] = 0;
        //        }
        //        return (int)ViewState["SPAssemblyImageView_xyBulkUpdate"];
        //    }
        //    set
        //    {
        //        ViewState["SPAssemblyImageView_xyBulkUpdate"] = value;
        //    }
        //}
        int? SpcModelID = null;
        int? SpcProductGroupID = null;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Assembly Parts');</script>");

            if (!IsPostBack)
            {
                new DDLBind(ddlProductGroup, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, " All");
                
            }
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            SpcModelID = ddlModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModel.SelectedValue);
            PApiResult Result = new BECatalogue().GetSpcAssembly(SpcProductGroupID, SpcModelID, null, null, true, 0, null, null);
            List<PSpcAssembly> Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
            new DDLBind(ddlAssembly, Assembly, "AssemblyDescription", "SpcAssemblyID", true, "All");

        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        { 
            iframe_PartsList.Attributes["src"] = "UserControls/SpcAssemblyPartsList.aspx?SpcAssemblyID=" + ddlAssembly.SelectedValue;

            PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, Convert.ToInt32( ddlAssembly.SelectedValue), "", null, 0);
            PSpcAssembly Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data))[0];            
            if (!string.IsNullOrEmpty( Assembly.FileName))
            {
                new BECatalogue().DowloadSpcFile(Assembly.FileName);
                Session["filePath"] = Assembly.FileName;
                imgAssemblyImage.ImageUrl = "UserControls\\ImageHandlerECatalogue.ashx?file=example.jpg";
            }
        }

        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            new DDLBind(ddlModel, new BECatalogue().GetSpcModel(null, SpcProductGroupID, null, true, null), "SpcModelCodeWithDescription", "SpcModelID", true, "All");
        
        }
    }
}