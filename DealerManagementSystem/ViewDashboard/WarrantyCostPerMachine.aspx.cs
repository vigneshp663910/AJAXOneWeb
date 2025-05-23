using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard
{
    public partial class WarrantyCostPerMachine : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDashboard_WarrantyCostPerMachine; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dashboard » Warranty Cast Per Machine');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlmDivision.ButtonClicked += new EventHandler(UserControl_ModelFill);
            if (!IsPostBack)
            {
                ddlmDealer.Fill("CodeWithDisplayName", "DID", PSession.User.Dealer);
                ddlmDivision.Fill("DivisionDescription", "DivisionID", new BDMS_Master().GetDivision(null, null));
                UserControl_ModelFill(null, null);
                ddlmRegion.Fill("Region", "RegionID", new BDMS_Address().GetRegion(1, null, null));

                DataTable dtServiceType = new DataTable();
                dtServiceType.Columns.Add("ServiceTypeID");
                dtServiceType.Columns.Add("ServiceType");
                dtServiceType.Rows.Add("2", "Warranty");
                dtServiceType.Rows.Add("15", "Goodwill Warranty");
                dtServiceType.Rows.Add("10", "Parts Warranty ");
                dtServiceType.Rows.Add("5", "Pre Commission");
                dtServiceType.Rows.Add("8", "Promotional Activity");
                dtServiceType.Rows.Add("9", "Policy Warranty");
                ddlmServiceType.Fill("ServiceType", "ServiceTypeID", dtServiceType);
                //ddlmServiceType.Fill("ServiceType", "ServiceTypeID", new BDMS_Service().GetServiceType(null, null, null));

                DataTable dtHMR = new DataTable();
                dtHMR.Columns.Add("ID");
                dtHMR.Columns.Add("Type");
                dtHMR.Rows.Add("1", "HMR 0-15");
                dtHMR.Rows.Add("2", "HMR 0-250");
                dtHMR.Rows.Add("3", "HMR 0-500");
                dtHMR.Rows.Add("4", "HMR 0-750");
                dtHMR.Rows.Add("5", "HMR 0-1000");
                dtHMR.Rows.Add("6", "HMR 0-1500");
                dtHMR.Rows.Add("7", "HMR 0-2000");
                dtHMR.Rows.Add("8", "HMR All");
                ddlmHMR.Fill("Type", "Type", dtHMR);

            }

        }
        protected void UserControl_ModelFill(object sender, EventArgs e)
        {
            //HtmlGenericControl MultiSelect = ddlmModel.MultiSelect;
            //if (MultiSelect.Visible)
            //{
            ddlmModel.Fill("Model", "ModelID", new BDMS_WarrantyClaim().ZYA_GetModel(ddlmDivision.SelectedValue));
            //}
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(RegionEastChart); ", true);

        }

        public DataTable GetData()
        {

            DataTable dt = new DataTable();
            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string Region = (string)HttpContext.Current.Session["Region"];
            string Division = (string)HttpContext.Current.Session["Division"];
            string ServiceType = (string)HttpContext.Current.Session["ServiceType"];
            string Model = (string)HttpContext.Current.Session["Model"];
            string Gragh = (string)HttpContext.Current.Session["Gragh"];
            dt = ((DataSet)new BDMS_WarrantyClaim().ZYA_GetWarrantyCostPerMachine(txtMfgDateFrom.Text.Trim(), txtMfgDateTo.Text.Trim(), txtAsOnDate.Text.Trim(), Dealer, Region, ServiceType, Division, Model, Gragh)).Tables[0];
            return dt;
        }

        public void PopulateGridView()
        {
            DataTable dt = GetData();
            
            if (dt.Rows.Count > 0)
            {
                gvData.DataSource = dt;
                gvData.DataBind();
            }
        }

     
        [WebMethod]
        public static List<object> RegionEastChart(string DateFrom, string DateTo, string AsOnDate)
        {
            string HMR = (string)HttpContext.Current.Session["HMR"];

            if (string.IsNullOrEmpty(HMR))
            {
                HMR = "HMR 0-15,HMR 0-250,HMR 0-500,HMR 0-750,HMR 0-1000,HMR 0-1500,HMR 0-2000,HMR All";
            }

            List<string> hmrObt = HMR.Split(',').ToList();
            List<object> chartData = new List<object>();
            object[] ob = new object[hmrObt.Count + 1];
            int i = 0;
            ob[i] = "Quarter";
            if (hmrObt.Contains("HMR 0-15"))
            {
                i = i + 1;
                ob[i] = "HMR 0-15";
            }
            if (hmrObt.Contains("HMR 0-250"))
            {
                i = i + 1;
                ob[i] = "HMR 0-250";
            }
            if (hmrObt.Contains("HMR 0-500"))
            {
                i = i + 1;
                ob[i] = "HMR 0-500";
            }
            if (hmrObt.Contains("HMR 0-750"))
            {
                i = i + 1;
                ob[i] = "HMR 0-750";
            }
            if (hmrObt.Contains("HMR 0-1000"))
            {
                i = i + 1;
                ob[i] = "HMR 0-1000";
            }
            if (hmrObt.Contains("HMR 0-1500"))
            {
                i = i + 1;
                ob[i] = "HMR 0-1500";
            }
            if (hmrObt.Contains("HMR 0-2000"))
            {
                i = i + 1;
                ob[i] = "HMR 0-2000";
            }
            if (hmrObt.Contains("HMR All"))
            {
                i = i + 1;
                ob[i] = "HMR All";
            }
            chartData.Add(ob); 

            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string Region = (string)HttpContext.Current.Session["Region"];
            string Division = (string)HttpContext.Current.Session["Division"];
            string ServiceType = (string)HttpContext.Current.Session["ServiceType"];
            string Model = (string)HttpContext.Current.Session["Model"];
            string Gragh = (string)HttpContext.Current.Session["Gragh"];
            DataTable dt = ((DataSet)new BDMS_WarrantyClaim().ZYA_GetWarrantyCostPerMachine(DateFrom, DateTo, AsOnDate, Dealer, Region, ServiceType, Division, Model, Gragh)).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {

                object[] obtData = new object[hmrObt.Count + 1];
                i = 0;
                obtData[i] = Convert.ToString(dr["Quarter"]) + "-" + Convert.ToString(dr["FinYear"]);
                if (hmrObt.Contains("HMR 0-15"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-15"]);
                }
                if (hmrObt.Contains("HMR 0-250"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-250"]);
                }
                if (hmrObt.Contains("HMR 0-500"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-500"]);
                }
                if (hmrObt.Contains("HMR 0-750"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-750"]);
                }
                if (hmrObt.Contains("HMR 0-1000"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-1000"]);
                }
                if (hmrObt.Contains("HMR 0-1500"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-1500"]);
                }
                if (hmrObt.Contains("HMR 0-2000"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR 0-2000"]);
                }
                if (hmrObt.Contains("HMR All"))
                {
                    i = i + 1;
                    obtData[i] = Convert.ToInt32(dr["HMR All"]);
                }
                chartData.Add(obtData); 
            }
            return chartData;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string Dealer = ddlmDealer.SelectedValue;
            string Region = ddlmRegion.SelectedValue;
            string ServiceType = ddlmServiceType.SelectedValue;
            string Division = ddlmDivision.SelectedValue;
            string Model = ddlmModel.SelectedValue;
            string HMR = ddlmHMR.SelectedValue;

            HttpContext.Current.Session["Dealer"] = Dealer;
            HttpContext.Current.Session["Region"] = Region;
            HttpContext.Current.Session["ServiceType"] = ServiceType;
            HttpContext.Current.Session["Division"] = Division;
            HttpContext.Current.Session["Model"] = Model;
            HttpContext.Current.Session["HMR"] = HMR;
            HttpContext.Current.Session["Gragh"] = ddlGrapgType.SelectedValue;

            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(RegionEastChart); ", true);

            PopulateGridView();

        }

        protected void BtnLineChartData_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();
            new BXcel().ExporttoExcel(dt, "Warranty Cast Per Machine Chart Data");
        }

        protected void BtnDetailData_Click(object sender, EventArgs e)
        {
            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string Region = (string)HttpContext.Current.Session["Region"];
            string Division = (string)HttpContext.Current.Session["Division"];
            string ServiceType = (string)HttpContext.Current.Session["ServiceType"];
            string Model = (string)HttpContext.Current.Session["Model"];
            string Gragh = (string)HttpContext.Current.Session["Gragh"];
            DataTable dt = ((DataSet)new BDMS_WarrantyClaim().ZYA_GetWarrantyCostPerMachine(txtMfgDateFrom.Text.Trim(), txtMfgDateTo.Text.Trim(), txtAsOnDate.Text.Trim(), Dealer, Region, ServiceType, Division, Model, Gragh, 1)).Tables[0];
            new BXcel().ExporttoExcel(dt, "Warranty Cast Per Machine");
        }


        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            //PopulateGridView();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#0000b3'; this.style.color = 'white' ";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'; this.style.color = 'black'; ";
            }
        }
    }
}