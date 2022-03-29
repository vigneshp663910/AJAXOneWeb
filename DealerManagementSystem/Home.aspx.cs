using Business;
using System;
using Properties;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "ContactName", "DID", true,"All Dealer");
                new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID",true, "All Country"); 
                new DDLBind(ddlZone, new BDMS_Address().GetRegion(null, null,null), "Region", "RegionID", true, "All Zone");
                //new DDLBind(ddlEngineer, new BDMS_Customer().GetCustomerTitle(null, null), "Title", "TitleID", true, "All Engineer");

                //new DDLBind(ddlDivision, new BDMS_Customer().GetCustomerTitle(null, null), "Title", "TitleID", true, "All Product");
                //new DDLBind(ddlModel, new BDMS_Service().Get(null, null), "Title", "TitleID", true, "All Model");
                new DDLBind(ddlApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "All Application");

                FillDivision();
                FillModel();
                FillFiscalYear();
            }
        }
        void FillDivision()
        {
            ddlDivision.Items.Insert(0, new ListItem("All Product", "0"));
            ddlDivision.Items.Insert(1, new ListItem("Batching Plant", "1"));
            ddlDivision.Items.Insert(2, new ListItem("Concrete Mixer", "2"));
            ddlDivision.Items.Insert(3, new ListItem("Concrete Pump", "3"));
            ddlDivision.Items.Insert(4, new ListItem("Dumper", "4"));
            ddlDivision.Items.Insert(5, new ListItem("Transit Mixer", "11")); 
        }

        void FillModel()
        {
            ddlModel.Items.Insert(0, new ListItem("All Model", "0"));
            ddlModel.Items.Insert(1, new ListItem("ARGO 1000", "1"));
            ddlModel.Items.Insert(2, new ListItem("ARGO 2000", "2"));
            ddlModel.Items.Insert(3, new ListItem("ARGO 2500", "3"));
            ddlModel.Items.Insert(4, new ListItem("ARGO 4000", "4"));
            ddlModel.Items.Insert(5, new ListItem("ARGO 4500", "5"));
            ddlModel.Items.Insert(6, new ListItem("AMBISON", "9"));
            ddlModel.Items.Insert(7, new ListItem("ASP", "10"));
            ddlModel.Items.Insert(8, new ListItem("CBP", "13"));
            ddlModel.Items.Insert(9, new ListItem("BOOM PUMP", "19"));
            ddlModel.Items.Insert(5, new ListItem("IRB", "29"));
        }

        void FillFiscalYear()
        {
            int i = 1;
            ddlFiscalYear.Items.Insert(0, new ListItem("Fiscal Year", "0"));
            for(int Year=2018; Year <= DateTime.Now.Year; Year++)
            {
                ddlFiscalYear.Items.Insert(i, new ListItem(Year.ToString(), Year.ToString()));
                i = i + 1;
            } 
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}