using Business;
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

namespace DealerManagementSystem.ViewSales.Report
{
    public partial class OofCustomerReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_Report_OofCustomerReport; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Reports » OOF Customer Report');</script>");
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtDate.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            }
        }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                fillOOFCustomerReport();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillOOFCustomerReport()
        {
            int? DealerID = null;
            DateTime SaleOrderDate;

            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            SaleOrderDate = Convert.ToDateTime(txtDate.Text.Trim());

            PApiResult Result = new BDMS_SalesOrder().GetOOFCustomerReport(DealerID, SaleOrderDate.ToString(), PageIndex, gvOOFCustomer.PageSize);
            Result.RowCount = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data)).Rows.Count;
            gvOOFCustomer.DataSource = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            gvOOFCustomer.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCountOOFCustomer.Visible = false;
                ibtnArrowLeftOOFCustomer.Visible = false;
                ibtnArrowRightOOFCustomer.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvOOFCustomer.PageSize - 1) / gvOOFCustomer.PageSize;
                lblRowCountOOFCustomer.Visible = true;
                ibtnArrowLeftOOFCustomer.Visible = true;
                ibtnArrowRightOOFCustomer.Visible = true;
                lblRowCountOOFCustomer.Text = (((PageIndex - 1) * gvOOFCustomer.PageSize) + 1) + " - " + (((PageIndex - 1) * gvOOFCustomer.PageSize) + gvOOFCustomer.Rows.Count) + " of " + Result.RowCount;
            }
        }
        protected void ibtnArrowLeftOOFCustomer_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillOOFCustomerReport();
            }
        }
        protected void ibtnArrowRightOOFCustomer_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillOOFCustomerReport();
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            int? DealerID = null;
            DateTime SaleOrderDate;
            DataTable dt = new DataTable();
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            SaleOrderDate = Convert.ToDateTime(txtDate.Text.Trim());

            PApiResult Result = new BDMS_SalesOrder().GetOOFCustomerReport(DealerID, SaleOrderDate.ToString(), PageIndex, gvOOFCustomer.PageSize);
            dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            
            new BXcel().ExporttoExcel(dt, "OOF Customer Report");
        }
    }
}