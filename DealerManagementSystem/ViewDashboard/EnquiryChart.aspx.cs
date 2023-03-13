using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard
{
    public partial class EnquiryChart : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDashboard_EnquiryChart; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public class GraphData
        {
            public int Year { get; set; }
            public int Sales { get; set; }
            public int Expenses { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dashboard » Enquiry');</script>");
            if (!IsPostBack)
            { 
                new DDLBind(ddlMCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID"); 
                loadYearAndMonth();

                ddlmDealer.Fill("CodeWithDisplayName", "DID", PSession.User.Dealer);
                ddlmProductType.Fill("ProductType", "ProductTypeID", new BDMS_Master().GetProductType(null, null));
            }
        }
        void loadYearAndMonth()
        {
            //ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            //for (int i = 2018; i <= DateTime.Now.Year; i++)
            //{
            //    ddlYear.Items.Insert(ddlYear.Items.Count, new ListItem(i.ToString(), i.ToString()));
            //}
            //ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
            //for (int i = 1; i <= 12; i++)
            //{
            //    ddlMonth.Items.Insert(ddlMonth.Items.Count, new ListItem(i.ToString(), i.ToString()));
            //}
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string Dealer = ddlmDealer.SelectedValue;
            int? CountryID = ddlMCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMCountry.SelectedValue);
            string Region = ddlMRegion.SelectedValue == "0" ? null : ddlMRegion.SelectedValue;
            string ProductType = ddlmProductType.SelectedValue;
            DataTable dt = new BEnquiry().GetEnquirySourceWiseCount(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), Dealer, CountryID, Region, ProductType);
            int Total = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Total = Total + Convert.ToInt32(dr["Count"]);
            }
            lblEnquiryCount.Text = Total.ToString();


            DataSet ds = new BEnquiry().GetEnquiryConversionPercentage(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), Dealer, CountryID, Region, ProductType);

            gvEnquiryProductType.DataSource = ds.Tables[0];
            gvEnquiryProductType.DataBind();

            gvEnquirySource.DataSource = ds.Tables[1];
            gvEnquirySource.DataBind();

            int TotalCount = 0, ConvertedCount = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TotalCount = TotalCount + Convert.ToInt32(dr["TotalCount"]);
                ConvertedCount = ConvertedCount + Convert.ToInt32(dr["ConvertedCount"]);
            }
            lblTotalConversion.Text = Convert.ToDecimal((ConvertedCount * 100.00 / TotalCount)).ToString("00.00") + " %";

            HttpContext.Current.Session["Dealer"] = Dealer;
            HttpContext.Current.Session["ProductType"] = ProductType;

            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(RegionChart); ", true);
            ClientScript.RegisterStartupScript(GetType(), "hwa2", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(SourceChart); ", true);
            ClientScript.RegisterStartupScript(GetType(), "hwa3", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(VelocityChart);", true); 

            //   Boolean IsMainServiceMaterial = (Boolean)HttpContext.Current.Session["IsMainServiceMaterial"];

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {

           
        }
        protected void ddlMCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlMRegion, new BDMS_Address().GetRegion(ddlMCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMCountry.SelectedValue), null, null), "Region", "RegionID");
        }


        [WebMethod]
        public static List<object> EnquiryRegionWiseCount(string DateFrom, string DateTo, string Country, string Region)
        {
            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string ProductType = (string)HttpContext.Current.Session["ProductType"];
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Task", "Open" }); 
            int? CountryID = string.IsNullOrEmpty(Country) || Country == "0" ? (int?)null : Convert.ToInt32(Country);
             
            Dealer = Dealer == "0" ? "" : Dealer;
            Region = Region == "0" ? "" : Region;
            ProductType = ProductType == "0" ? "" : ProductType;
            DataTable dt = new BEnquiry().GetEnquiryRegionWiseCount( DateFrom, DateTo, Dealer, CountryID, Region, ProductType);
            foreach (DataRow dr in dt.Rows)
            {
                chartData.Add(new object[] { Convert.ToString(dr["Region"]), Convert.ToInt32(dr["Count"]) });
            }

            return chartData;

        }
        [WebMethod]
        public static List<object> EnquirySourceWiseCount(string DateFrom, string DateTo,   string Country, string Region )
        {
            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string ProductType = (string)HttpContext.Current.Session["ProductType"];

            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Task", "Open" });
            int? CountryID = string.IsNullOrEmpty(Country) || Country == "0" ? (int?)null : Convert.ToInt32(Country);
            Dealer = Dealer == "0" ? "" : Dealer;
            Region = Region == "0" ? "" : Region;
            ProductType = ProductType == "0" ? "" : ProductType;
            DataTable dt = new BEnquiry().GetEnquirySourceWiseCount(DateFrom, DateTo, Dealer, CountryID, Region, ProductType);
            foreach (DataRow dr in dt.Rows)
            {
                chartData.Add(new object[] { Convert.ToString(dr["Source"]), Convert.ToInt32(dr["Count"])});
            }
            return chartData;
        }

        [WebMethod]
        public static List<object> GetVelocity(string DateFrom, string DateTo, string Country, string Region)
        {
            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string ProductType = (string)HttpContext.Current.Session["ProductType"];
             

            int? CountryID = string.IsNullOrEmpty(Country) || Country == "0" ? (int?)null : Convert.ToInt32(Country);
            Dealer = Dealer == "0" ? "" : Dealer;
            Region = Region == "0" ? "" : Region;
            ProductType = ProductType == "0" ? "" : ProductType;
             
            DataSet ds = new BEnquiry().GetEnquiryVelocityCount(DateFrom, DateTo, Dealer, CountryID, Region, ProductType);
            List<object> Main = new List<object>();

            foreach (DataTable dt in ds.Tables)
            {
                Main.Add(GetVelocityData(dt));
            }

          //  List<object> Sub = null;

            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });            
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });


            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });


            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[2].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });

            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[3].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });

            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[4].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });

            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[5].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });

            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[6].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Sub.Add(new object[] { "0",0 });

            //Main.Add(new object[] { Sub });

            //Sub = new List<object>();
            //Sub.Add(new object[] { "Description", "Count" });
            //foreach (DataRow dr in ds.Tables[7].Rows)
            //{
            //    Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            //}
            //Main.Add(new object[] { Sub });

            return Main;
        }


        static List<object> GetVelocityData(DataTable dt)
        {
            List<object> Sub = new List<object>();
            Sub.Add(new object[] { "Description", "Count" });
            foreach (DataRow dr in dt.Rows)
            {
                Sub.Add(new object[] { Convert.ToString(dr["Description"]), Convert.ToInt32(dr["Count"]) });
            }
            if (Sub.Count == 1)
            {
                Sub.Add(new object[] { "0", 0 });
            } 
            return Sub;
        }
    }
}