using Business;
using ClosedXML.Excel;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class SalesIncentive : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDealerEmployee_SalesIncentive; } }
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
        public DataTable DTSalesIncentiveUpload
        {
            get
            {
                if (ViewState["DTSalesIncentiveUpload"] == null)
                {
                    ViewState["DTSalesIncentiveUpload"] = new DataTable();
                }
                return (DataTable)ViewState["DTSalesIncentiveUpload"];
            }
            set
            {
                ViewState["DTSalesIncentiveUpload"] = value;
            }
        }
        public DataTable DTSalesIncentiveByID
        {
            get
            {
                if (ViewState["SalesIncentiveByID"] == null)
                {
                    ViewState["SalesIncentiveByID"] = new DataTable();
                }
                return (DataTable)ViewState["SalesIncentiveByID"];
            }
            set
            {
                ViewState["SalesIncentiveByID"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Sales Incentive');</script>");
            try
            {
                lblMessage.Text = "";
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    DTSalesIncentiveUpload = null;
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                    new DDLBind().Year(ddlYear, 2022);
                    new DDLBind().Month(ddlMonth);
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, 1, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }
        private Boolean FillUpload()
        {
            Boolean Success = true;
                if (fileUpload.HasFile == true)
                {
                    using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                    {
                        //Read the first Sheet from Excel file.
                        IXLWorksheet workSheet = workBook.Worksheet(1);

                        //Create a new DataTable.
                        DTSalesIncentiveUpload = new DataTable();

                        //Loop through the Worksheet rows.
                        int sno = 0;
                        foreach (IXLRow row in workSheet.Rows())
                        {
                            sno += 1;
                            //Use the first row to add columns to DataTable.
                            if (sno == 1)
                            {
                                foreach (IXLCell cell in row.Cells())
                                {
                                    DTSalesIncentiveUpload.Columns.Add(cell.Value.ToString());
                                }
                            }
                            else if (sno > 1)
                            {
                                //Add rows to DataTable.
                                DTSalesIncentiveUpload.Rows.Add();
                                int i = 0;
                                foreach (IXLCell cell in row.Cells())
                                {
                                    DTSalesIncentiveUpload.Rows[DTSalesIncentiveUpload.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }
                        List<PDMS_DealerEmployee> Employee = new List<PDMS_DealerEmployee>();
                        Employee = new BDMS_Dealer().GetDealerEmployeeManage(null, null, null, null, null, null, null, null, null);
                        foreach (DataRow dr in DTSalesIncentiveUpload.Rows)
                        {
                            bool containsItem = Employee.Any(item => item.AadhaarCardNo == dr[6].ToString());
                            if (!containsItem)
                            {
                                lblMessage.Text = "Please Check Aadhaar Card No : " + dr[6].ToString() + " Not Available in the Employee List...!";
                                lblMessage.ForeColor = Color.Red;
                                Success = false;
                                return Success;
                            }
                        }
                        if (DTSalesIncentiveUpload.Rows.Count > 0)
                        {
                            GVUpload.DataSource = DTSalesIncentiveUpload;
                            GVUpload.DataBind();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please Upload the File...!";
                    lblMessage.ForeColor = Color.Red;
                    Success = false;
                    return Success;
                }
            return Success;
        }
        private void FillGrid()
        {
            try
            {
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                int? DealerEmployeeID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
                string InvoiceNo = null;
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    InvoiceNo = txtInvoiceNo.Text.Trim();
                }
                int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
                int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);

                int RowCount = 0;
                DataTable DTSalesIncentive = new BSalesIncentive().GetSalesIncentive(null, Year, Month, DealerID, DealerEmployeeID, InvoiceNo, null, null, PSession.User.UserID, PageIndex, gvSalesIncentive.PageSize);
                if (!string.IsNullOrEmpty(HiddenSalesIncentiveID.Value))
                {
                    DTSalesIncentiveByID = new BSalesIncentive().GetSalesIncentive(Convert.ToInt32(HiddenSalesIncentiveID.Value), Year, Month, DealerID, DealerEmployeeID, InvoiceNo, null, null, PSession.User.UserID, null, null);
                }
                if (DTSalesIncentive.Rows.Count > 0)
                    RowCount = Convert.ToInt32(DTSalesIncentive.Rows[0]["RowCount"].ToString());
                if (RowCount == 0)
                {
                    gvSalesIncentive.DataSource = null;
                    gvSalesIncentive.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvSalesIncentive.DataSource = DTSalesIncentive;
                    gvSalesIncentive.DataBind();
                    PageCount = (RowCount + gvSalesIncentive.PageSize - 1) / gvSalesIncentive.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvSalesIncentive.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSalesIncentive.PageSize) + gvSalesIncentive.Rows.Count) + " of " + RowCount;
                }

                if (!string.IsNullOrEmpty(HiddenSalesIncentiveID.Value))
                {
                    if (DTSalesIncentiveByID.Rows.Count > 0)
                    {
                        lblDealerCode.Text = DTSalesIncentiveByID.Rows[0]["DealerCode"].ToString();
                        lblSPAadhaarNo.Text = DTSalesIncentiveByID.Rows[0]["SalesPersonAadhaarNo"].ToString();
                        lblMonthandYear.Text = DTSalesIncentiveByID.Rows[0]["Month&Year"].ToString();
                        lblModel.Text = DTSalesIncentiveByID.Rows[0]["Model"].ToString();
                        lblDealerName.Text = DTSalesIncentiveByID.Rows[0]["DealerName"].ToString();
                        lblSPName.Text = DTSalesIncentiveByID.Rows[0]["SalesPersonName"].ToString();
                        lblSalesLevel.Text = DTSalesIncentiveByID.Rows[0]["SalesLevel"].ToString();
                        lblInvoiceNo.Text = DTSalesIncentiveByID.Rows[0]["InvoiceNo"].ToString();
                        lblInvoiceDate.Text = DTSalesIncentiveByID.Rows[0]["InvoiceDate"].ToString();
                        lblIncentiveAmount.Text = DTSalesIncentiveByID.Rows[0]["IncentiveAmount"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            divSalesIncentiveView.Visible = true;
            divSalesIncentiveList.Visible = false;
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            int? SalesIncentiveID = Convert.ToInt32(BtnView.CommandArgument);
            HiddenSalesIncentiveID.Value = SalesIncentiveID.ToString();
            FillGrid();
        }
        protected void gvSalesIncentive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSalesIncentive.PageIndex = e.NewPageIndex;
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillGrid();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillGrid();
            }
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divSalesIncentiveList.Visible = true;
            divSalesIncentiveView.Visible = false;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? DealerEmployeeID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            string InvoiceNo = null;
            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                InvoiceNo = txtInvoiceNo.Text.Trim();
            }
            //DateTime? DateF = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text.Trim());
            //DateTime? DateT = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text.Trim());
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);

            DataTable dt = new DataTable();
            dt.Columns.Add("Month&Year");
            dt.Columns.Add("InvoiceNo");
            dt.Columns.Add("InvoiceDate");
            dt.Columns.Add("DealerCode",typeof(Int32));
            dt.Columns.Add("DealerName");
            dt.Columns.Add("SalesLevel", typeof(Int32));
            dt.Columns.Add("SalesPersonAadhaarNo", typeof(Int64));
            dt.Columns.Add("SalesPersonName");
            dt.Columns.Add("DealerDesignation");
            dt.Columns.Add("Model");
            dt.Columns.Add("IncentiveAmount", typeof(decimal));

            int Index = 0;
            int Rowcount = 100;
            int CRowcount = Rowcount;

            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                DataTable DTSalesIncentive = (new BSalesIncentive().GetSalesIncentive(null, Year, Month, DealerID, DealerEmployeeID, InvoiceNo, null, null, PSession.User.UserID, Index, Rowcount));
                CRowcount = 0;
                dt.Merge(DTSalesIncentive);
                CRowcount = DTSalesIncentive.Rows.Count;
            }
            foreach(DataColumn column in dt.Columns)
            {
                if(column.ColumnName== "SalesIncentiveID")
                {
                    dt.Columns.Remove("SalesIncentiveID");
                    goto RowCount;
                }
            }
            RowCount:
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "RowCount")
                {
                    dt.Columns.Remove("RowCount");
                    goto Ready;
                }
            }
            Ready:
            new BXcel().ExporttoExcel(dt, "Sales Incentive");
        }
        protected void BtnFUpload_Click(object sender, EventArgs e)
        {
            DTSalesIncentiveUpload = new DataTable();
            GVUpload.DataSource = DTSalesIncentiveUpload;
            GVUpload.DataBind();
            divSalesIncentiveList.Visible = false;
            divSalesIncentiveView.Visible = false;
            divSalesIncentiveUpload.Visible = true;
        }
        protected void BtnFBack_Click(object sender, EventArgs e)
        {
            DTSalesIncentiveUpload = new DataTable();
            GVUpload.DataSource = DTSalesIncentiveUpload;
            GVUpload.DataBind();
            divSalesIncentiveList.Visible = true;
            divSalesIncentiveView.Visible = false;
            divSalesIncentiveUpload.Visible = false;
        }

        protected void btnView_Click1(object sender, EventArgs e)
        {
            DTSalesIncentiveUpload = new DataTable();
            GVUpload.DataSource = DTSalesIncentiveUpload;
            GVUpload.DataBind();
            if (IsPostBack && fileUpload.PostedFile != null)
            {
                if (fileUpload.PostedFile.FileName.Length > 0)
                {
                    FillUpload();
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Success = false;

                if (fileUpload.PostedFile != null)
                {
                    try
                    {
                        if (DTSalesIncentiveUpload.Rows.Count > 0)
                        {
                            Success = new BSalesIncentive().InsertOrUpdateTSalesIncentive_ForExcelUpload(DTSalesIncentiveUpload);
                        }
                        if (Success)
                        {
                            lblMessage.Text = "Sales Incentive Was Uploaded Successfully...";
                            lblMessage.ForeColor = Color.Green;
                            DTSalesIncentiveUpload = null;
                        }
                        else
                        {
                            lblMessage.Text = "Sales Incentive Not Uploaded Successfully...!";
                            lblMessage.ForeColor = Color.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message.ToString();
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string Path = Server.MapPath("Templates\\Sales Incentive-Templates.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"Sales Incentive-Templates.xlsx\"");
            byte[] data = req.DownloadData(Path);
            response.BinaryWrite(data);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end
            response.End();
        }
    }
}