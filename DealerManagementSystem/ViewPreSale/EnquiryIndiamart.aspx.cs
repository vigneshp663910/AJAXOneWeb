using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class EnquiryIndiamart : System.Web.UI.Page
    {
        private int PageCount
        {
            get
            {
                if (ViewState["EnquiryIndiamartPageCount"] == null)
                {
                    ViewState["EnquiryIndiamartPageCount"] = 0;
                }
                return (int)ViewState["EnquiryIndiamartPageCount"];
            }
            set
            {
                ViewState["EnquiryIndiamartPageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["EnquiryIndiamartPageIndex"] == null)
                {
                    ViewState["EnquiryIndiamartPageIndex"] = 1;
                }
                return (int)ViewState["EnquiryIndiamartPageIndex"];
            }
            set
            {
                ViewState["EnquiryIndiamartPageIndex"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Enquiry Indiamart');</script>");
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null,null), "Source", "SourceID");
                new DDLBind(ddlSStatus, new BDMS_Master().GetPreSaleStatus(null, null), "Status", "StatusID");
              //  UC_ViewEquiryIndiamart.EnquiryIndiamartViewID = null;
            }
        }        
        private void FillGrid()
        {
            try
            {
                DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
                DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
                int? PreSaleStatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);
                int? SourceID = ddlSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSource.SelectedValue);
                int RowCount = 0;
                DataTable Enquiry = new BEnquiry().GetEnquiryIndiamart(DateFrom, DateTo, PreSaleStatusID, SourceID, PageIndex, gvEnquiry.PageSize);
                
                if (Enquiry.Rows.Count > 0)
                    RowCount = Convert.ToInt32(Enquiry.Rows[0]["RowCount"].ToString());
                if (RowCount == 0)
                {
                    gvEnquiry.DataSource = null;
                    gvEnquiry.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvEnquiry.DataSource = Enquiry;
                    gvEnquiry.DataBind();
                    PageCount = (RowCount + gvEnquiry.PageSize - 1) / gvEnquiry.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvEnquiry.PageSize) + 1) + " - " + (((PageIndex - 1) * gvEnquiry.PageSize) + gvEnquiry.Rows.Count) + " of " + RowCount;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        public void AddressSplit(string Input, TextBox txtAddress1, TextBox txtAddress2, TextBox txtAddress3)
        {
            string[] SplitedInput = Input.Split(' ');

            foreach (string Word in SplitedInput)
            {
                if (((txtAddress1.Text + Word).Length <= 40) && (string.IsNullOrEmpty(txtAddress2.Text)))
                {
                    txtAddress1.Text = txtAddress1.Text + " " + Word;
                }
                else if (((txtAddress2.Text + Word).Length <= 40) && (string.IsNullOrEmpty(txtAddress3.Text)))
                {
                    txtAddress2.Text = txtAddress2.Text + " " + Word;
                }
                else
                {
                    txtAddress3.Text = txtAddress3.Text + " " + Word;
                }
            }
            txtAddress1.Text = txtAddress1.Text.Trim();
            txtAddress2.Text = txtAddress2.Text.Trim();
            txtAddress3.Text = txtAddress3.Text.Trim();
        }
        protected void btnEnquiryIndiamart_Click(object sender, EventArgs e)
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
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            int? PreSaleStatusID = ddlSStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSStatus.SelectedValue);
            int? SourceID = ddlSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSource.SelectedValue);

            DataTable dt = new DataTable();
            dt.Columns.Add("Query ID");
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Query Type");
            dt.Columns.Add("Status");
            dt.Columns.Add("LeadSource");
            dt.Columns.Add("Sender Name");
            dt.Columns.Add("Sender Email");
            dt.Columns.Add("MOB");
            dt.Columns.Add("Company Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("State");
            dt.Columns.Add("Country");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Receiver Mob");
            dt.Columns.Add("Email Alt");
            dt.Columns.Add("Mobile Alt");
            dt.Columns.Add("Message");

            int Index = 0;
            int Rowcount = 100;
            int CRowcount = Rowcount;

            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                DataTable Enquiry = new BEnquiry().GetEnquiryIndiamart(DateFrom, DateTo, PreSaleStatusID, SourceID, Index, Rowcount);
                CRowcount = 0;
                dt.Merge(Enquiry);
                CRowcount = Enquiry.Rows.Count;
            }
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "EnquiryIndiamartID")
                {
                    dt.Columns.Remove("EnquiryIndiamartID");
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
            new BXcel().ExporttoExcel(dt, "Enquiry Indiamart");
        }       
        protected void BtnView_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divDetailsView.Visible = true;

            //lblAddEnquiryMessage.Text = "";
            lblMessage.Text = "";
            Button BtnView = (Button)sender; 
            UC_ViewEquiryIndiamart.fillViewEnquiryIndiamart(Convert.ToInt64(BtnView.CommandArgument));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
            btnEnquiryIndiamart_Click(null, null);
        }
        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvEnquiry.PageIndex = e.NewPageIndex;
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
    }
}