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
    public partial class EnquiryChart : System.Web.UI.Page
    {
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
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dashboard » Enquiry');</script>");
                new DDLBind(ddlMDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind(ddlMCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
                loadYearAndMonth();
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(RegionChart); ", true);
            ClientScript.RegisterStartupScript(GetType(), "hwa2", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(SourceChart); ", true);

            string Dealer = "";
            int? CountryID =  ddlMCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMCountry.SelectedValue);
            string Region = ddlMRegion.SelectedValue == "0" ? null :  ddlMRegion.SelectedValue;
            string ProductType = ddlProductType.SelectedValue == "0" ? null : ddlProductType.SelectedValue;
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
            lblTotalConversion.Text = Convert.ToDecimal((ConvertedCount * 100 / TotalCount)).ToString("00.00") + " %";
            
        } 
        protected void ddlMCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlMRegion, new BDMS_Address().GetRegion(ddlMCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMCountry.SelectedValue), null, null), "Region", "RegionID");
        }


        [WebMethod]
        public static List<object> EnquiryRegionWiseCount(string DateFrom, string DateTo, string Dealer, string Country, string Region, string ProductType)
        {
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
        public static List<object> EnquirySourceWiseCount(string DateFrom, string DateTo, string Dealer, string Country, string Region, string ProductType)
        {
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
    }
}