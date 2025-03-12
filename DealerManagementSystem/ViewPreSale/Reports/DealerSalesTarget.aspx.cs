using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.Reports
{
    public partial class DealerSalesTarget :   BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Reports_DealerSalesTarget; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dashboard » Incident Per 100 Machine');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                ddlmDealer.Fill("CodeWithDisplayName", "DID", PSession.User.Dealer); 
                ddlmRegion.Fill("Region", "RegionID", new BDMS_Address().GetRegion(1, null, null));
                int i = 0;
                // ddlYear.Items.Insert(0, new ListItem("Year", "0"));
                for (int Year = 2024; Year <= DateTime.Now.Year; Year++)
                {
                    ddlYear.Items.Insert(i, new ListItem(Year.ToString(), Year.ToString()));
                    i = i + 1;
                }
                // ddlYear.Items.Insert(0, new ListItem("Year", "0"));


                for (int Month = 1; Month <= 12; Month++)
                {
                    ddlMonth.Items.Insert(Month-1, new ListItem(Month.ToString(), Month.ToString()));
                }
            }
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
            string Year = (string)HttpContext.Current.Session["Year"];
            string Month = (string)HttpContext.Current.Session["Month"]; 
            dt = ((DataSet)new BDMS_Dealer().ZYA_GetDealerSalesTarget( Dealer, Region,   Year, Month,0)).Tables[0];
            return dt;
        }


        public void PopulateGridView()
        {
            DataTable dt = GetData();

            if (dt.Rows.Count > 0)
            {
                //gvData.DataSource = dt;
                //gvData.DataBind();
            }
        }

        [System.Web.Services.WebMethod]
        public static List<List<object>> IncidentPer()
        {
            List<List<object>> OutPut = new List<List<object>>();


            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string Region = (string)HttpContext.Current.Session["Region"]; 
            string Year = (string)HttpContext.Current.Session["Year"];
            string Month = (string)HttpContext.Current.Session["Month"];

            DataSet ds = new BDMS_Dealer().ZYA_GetDealerSalesTarget(Dealer, Region,  Year, Month,0);

            //List<object> chartData = new List<object>();  
            //chartData.Add(new object[3] { "Name", "Target", "Actual" });
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{ 
            //    chartData.Add(new object[3] { Convert.ToString(dr["ContactName"]), Convert.ToInt32(dr["Target"]), Convert.ToInt32(dr["Actual"]) });
            //}
            //OutPut.Add(chartData);


            //chartData = new List<object>();
            //chartData.Add(new object[3] { "State", "Target", "Actual" });
            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    chartData.Add(new object[3] { Convert.ToString(dr["ContactName"]), Convert.ToInt32(dr["Target"]), Convert.ToInt32(dr["Actual"]) });
            //}
            //OutPut.Add(chartData);

            OutPut.Add(FillSalesPerson(ds.Tables[0]));
            OutPut.Add(FillSalesPerson(ds.Tables[1]));
            OutPut.Add(FillSalesPerson(ds.Tables[2]));
            OutPut.Add(FillSalesPerson(ds.Tables[3]));

            OutPut.Add(FillState(ds.Tables[4]));            
            OutPut.Add(FillState(ds.Tables[5]));            
            OutPut.Add(FillState(ds.Tables[6]));            
            OutPut.Add(FillState(ds.Tables[7]));
            return OutPut;
        }
       static List<object> FillSalesPerson(DataTable dt)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[4] { "Name", "Target", "Actual", "Performance" });
            foreach (DataRow dr in dt.Rows)
            {
                chartData.Add(new object[4] { Convert.ToString(dr["ContactName"]), Convert.ToInt32(dr["Target"]), Convert.ToInt32(dr["Actual"]), Convert.ToInt32(dr["Performance"]) });
            }
            return chartData;
        }
        static List<object> FillState(DataTable dt)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[4] { "State", "Target", "Actual", "Performance" });
            foreach (DataRow dr in dt.Rows)
            {
                chartData.Add(new object[4] { Convert.ToString(dr["State"]), Convert.ToInt32(dr["Target"]), Convert.ToInt32(dr["Actual"]), Convert.ToInt32(dr["Performance"]) });
            }
            return chartData;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string Dealer = ddlmDealer.SelectedValue;
            string Region = ddlmRegion.SelectedValue; 
            string Year = ddlYear.SelectedValue;
            string Month = ddlMonth.SelectedValue; 

            HttpContext.Current.Session["Dealer"] = Dealer;
            HttpContext.Current.Session["Region"] = Region; 
            HttpContext.Current.Session["Year"] = Year;
            HttpContext.Current.Session["Month"] = Month; 

            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(RegionEastChart); ", true);

            PopulateGridView();

        }

        protected void BtnLineChartData_Click(object sender, EventArgs e)
        {
            DataTable dt = GetData();
            new BXcel().ExporttoExcel(dt, "Incident Per 100 Machine Chart Data");
        }

        protected void BtnDetailData_Click(object sender, EventArgs e)
        {
            string Dealer = (string)HttpContext.Current.Session["Dealer"];
            string Region = (string)HttpContext.Current.Session["Region"]; 
            string Year = (string)HttpContext.Current.Session["Year"];
            string Month = (string)HttpContext.Current.Session["Month"];
            DataTable dt = ((DataSet)new BDMS_Dealer().ZYA_GetDealerSalesTarget(Dealer, Region,  Year, Month,1)).Tables[0]; 
            new BXcel().ExporttoExcel(dt, "Incident Per 100 Machine");
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex; 
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#0000b3'; this.style.color = 'white' ";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'; this.style.color = 'black'; ";

            }
        }

        protected void btnUploadTarget_Click(object sender, EventArgs e)
        {
            lblMessageMaterialUpload.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDealerSalesTarget_Insert> SalesTarget = new List<PDealerSalesTarget_Insert>();
            int DealerID = 0;
            int DivisionID = 0;

            try
            {
                List<PDMS_Division> division = new BDMS_Master().GetDivision(null, null);
                if (fileUpload.HasFile != true)
                {
                    lblMessageMaterialUpload.Text = "Please check the file.";
                    return;
                }
                string validExcel = ".xlsx";
                string FileExtension = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                if (validExcel != FileExtension)
                {
                    lblMessageMaterialUpload.Text = "Please check the file format.";
                    return;
                }
                using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable DTDealerOperatorDetailsUpload = new DataTable();

                    //Loop through the Worksheet rows.
                    int sno = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        sno += 1;
                        if (sno > 1)
                        {
                            List<IXLCell> Cells = row.Cells().ToList();
                            if (Cells.Count != 0)
                            {
                                string Dealer = Convert.ToString(Cells[1].Value).TrimEnd('\0');
                                string Year = Convert.ToString(Cells[2].Value).TrimEnd('\0');
                                string Month = Convert.ToString(Cells[3].Value).TrimEnd('\0');
                                string Division = Convert.ToString(Cells[4].Value).TrimEnd('\0');
                                string Target = Convert.ToString(Cells[5].Value).TrimEnd('\0');

                                List<PDealer> D = PSession.User.Dealer.Where(A => A.DealerCode == Dealer).ToList();
                                if (D.Count == 1)
                                {
                                    DealerID = D[0].DealerID;
                                }
                                else
                                {
                                    return;
                                }
                                List<PDMS_Division> Di = division.Where(A => A.DivisionCode == Division).ToList();
                                if (D.Count == 1)
                                {
                                    DivisionID = Di[0].DivisionID;
                                }
                                else
                                {
                                    return;
                                }

                                SalesTarget.Add(new PDealerSalesTarget_Insert()
                                {
                                    DealerID = DealerID,
                                    Year = Convert.ToInt32(Year),
                                    Month = Convert.ToInt32(Month),
                                    DivisionID = DivisionID,
                                    Target = Convert.ToInt32(Target)
                                });
                            }
                        }
                    }
                }

                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerSalesTarget", SalesTarget));
                if (Results.Status == PApplication.Failure)
                {
                    //lblMessageMarginWarrantyRequest.Text = Results.Message;
                    //lblMessageMarginWarrantyRequest.Visible = true;
                    //lblMessageMarginWarrantyRequest.ForeColor = Color.Red;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageMaterialUpload.Text = ex.Message.ToString();
            }

        }
    }
}