using Business;
using ClosedXML.Excel;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class DealerOperatorDetails : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDealerEmployee_DealerOperatorDetails; } }
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
        public DataTable DTDealerOperatorDetailsUpload
        {
            get
            {
                if (ViewState["DTDealerOperatorDetailsUpload"] == null)
                {
                    ViewState["DTDealerOperatorDetailsUpload"] = new DataTable();
                }
                return (DataTable)ViewState["DTDealerOperatorDetailsUpload"];
            }
            set
            {
                ViewState["DTDealerOperatorDetailsUpload"] = value;
            }
        }
        public DataTable DTDealerOperatorDetailsByID
        {
            get
            {
                if (ViewState["DealerOperatorDetailsByID"] == null)
                {
                    ViewState["DealerOperatorDetailsByID"] = new DataTable();
                }
                return (DataTable)ViewState["DealerOperatorDetailsByID"];
            }
            set
            {
                ViewState["DealerOperatorDetailsByID"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Dealer Operator Details');</script>");
            try
            {
                lblMessage.Text = "";
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    DTDealerOperatorDetailsUpload = null;
                    new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                    new DDLBind(ddlRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID");
                    new DDLBind(ddlState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID");
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        private Boolean FillUpload()
        {
            Boolean Success = true;
            if (DTDealerOperatorDetailsUpload.Rows.Count > 0)
            {
                List<PDMS_Dealer> pDMS_Dealers = new List<PDMS_Dealer>();
                pDMS_Dealers = new BDMS_Dealer().GetDealer(null, null, null, null);
                List<PDMS_State> pDMS_State = new List<PDMS_State>();
                pDMS_State = new BDMS_Address().GetState(null, null, null, null, null);
                foreach (DataRow dr in DTDealerOperatorDetailsUpload.Rows)
                {
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                    {
                        bool containsItem = pDMS_Dealers.Any(item => item.DealerCode == dr[3].ToString());
                        if (!containsItem)
                        {
                            lblMessage.Text = "Please Check the DealerCode : " + dr[3].ToString() + " Not Available in the Dealer List...!";
                            lblMessage.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }

                        bool containsItemState = pDMS_State.Any(item => item.State.ToUpper() == dr[1].ToString().ToUpper());
                        if (!containsItemState)
                        {
                            lblMessage.Text = "Please Check the State : " + dr[1].ToString() + " Not Available in the State List...!";
                            lblMessage.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }
                    }
                }
                var duplicates = DTDealerOperatorDetailsUpload.AsEnumerable().GroupBy(i => new { State = i.Field<string>("State"), Region = i.Field<string>("Region"), DealerCode = i.Field<string>("Dealer Code"), AjaxOperatorName = i.Field<string>("Ajax Operator Name"), ContactNo = i.Field<string>("Contact No.") }).Where(g => g.Count() > 1).Select(g => new { g.Key.State, g.Key.Region, g.Key.DealerCode, g.Key.AjaxOperatorName, g.Key.ContactNo }).ToList();
                if (duplicates.Count > 0)
                {
                    lblMessage.Text = "Duplicate Records Found : " + duplicates.Count + "...!";
                    lblMessage.ForeColor = Color.Red;
                    Success = false;
                    return Success;
                }
                if (DTDealerOperatorDetailsUpload.Rows.Count > 0)
                {
                    GVUpload.DataSource = DTDealerOperatorDetailsUpload;
                    GVUpload.DataBind();
                }
            }
            else
            {
                if (fileUpload.HasFile == true)
                {
                    using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                    {
                        //Read the first Sheet from Excel file.
                        IXLWorksheet workSheet = workBook.Worksheet(2);

                        //Create a new DataTable.
                        DTDealerOperatorDetailsUpload = new DataTable();

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
                                    DTDealerOperatorDetailsUpload.Columns.Add(cell.Value.ToString());
                                }
                            }
                            else if (sno > 1)
                            {
                                //Add rows to DataTable.
                                DTDealerOperatorDetailsUpload.Rows.Add();
                                int i = 0;


                                foreach (IXLCell cell in row.Cells())
                                {
                                    DTDealerOperatorDetailsUpload.Rows[DTDealerOperatorDetailsUpload.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }
                        List<PDMS_Dealer> pDMS_Dealers = new List<PDMS_Dealer>();
                        pDMS_Dealers = new BDMS_Dealer().GetDealer(null, null, null, null);
                        List<PDMS_State> pDMS_State = new List<PDMS_State>();
                        pDMS_State = new BDMS_Address().GetState(null, null, null, null, null);
                        foreach (DataRow dr in DTDealerOperatorDetailsUpload.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr[0].ToString()))
                            {
                                bool containsItem = pDMS_Dealers.Any(item => item.DealerCode == dr[3].ToString());
                                if (!containsItem)
                                {
                                    lblMessage.Text = "Please Check the DealerCode : " + dr[3].ToString() + " Not Available in the Dealer List...!";
                                    lblMessage.ForeColor = Color.Red;
                                    Success = false;
                                    return Success;
                                }
                                bool containsItemState = pDMS_State.Any(item => item.State.ToUpper() == dr[1].ToString().ToUpper());
                                if (!containsItemState)
                                {
                                    lblMessage.Text = "Please Check the State : " + dr[1].ToString() + " Not Available in the State List...!";
                                    lblMessage.ForeColor = Color.Red;
                                    Success = false;
                                    return Success;
                                }
                            }
                        }

                        var duplicates = DTDealerOperatorDetailsUpload.AsEnumerable().GroupBy(i => new { State = i.Field<string>("State"), Region = i.Field<string>("Region"), DealerCode = i.Field<string>("Dealer Code"), AjaxOperatorName = i.Field<string>("Ajax Operator Name"), ContactNo = i.Field<string>("Contact No.") }).Where(g => g.Count() > 1).Select(g => new { g.Key.State, g.Key.Region, g.Key.DealerCode, g.Key.AjaxOperatorName, g.Key.ContactNo }).ToList();
                        if (duplicates.Count > 0)
                        {
                            lblMessage.Text = "Duplicate Records Found : " + duplicates.Count + "...!";
                            lblMessage.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }

                        if (DTDealerOperatorDetailsUpload.Rows.Count > 0)
                        {
                            GVUpload.DataSource = DTDealerOperatorDetailsUpload;
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
            }
            return Success;
        }
        private void FillGrid()
        {
            try
            {
                int? DealerID = null, StateID = null, RegionID = null;
                DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
                int RowCount = 0;
                DataTable DTDealerOperatorDetails = new BDealerOperatorDetails().GetDealerOperatorDetails(null, DealerID, StateID, RegionID, null, PSession.User.UserID, PageIndex, gvDealerOperatorDetails.PageSize);
                if (!string.IsNullOrEmpty(HiddenDealerOperatorDetailsID.Value))
                {
                    DTDealerOperatorDetailsByID = new BDealerOperatorDetails().GetDealerOperatorDetails(Convert.ToInt32(HiddenDealerOperatorDetailsID.Value), DealerID, StateID, RegionID, null, PSession.User.UserID, null, null);
                }
                if (DTDealerOperatorDetails.Rows.Count > 0)
                    RowCount = Convert.ToInt32(DTDealerOperatorDetails.Rows[0]["RowCount"].ToString());
                if (RowCount == 0)
                {
                    gvDealerOperatorDetails.DataSource = null;
                    gvDealerOperatorDetails.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvDealerOperatorDetails.DataSource = DTDealerOperatorDetails;
                    gvDealerOperatorDetails.DataBind();
                    PageCount = (RowCount + gvDealerOperatorDetails.PageSize - 1) / gvDealerOperatorDetails.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvDealerOperatorDetails.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerOperatorDetails.PageSize) + gvDealerOperatorDetails.Rows.Count) + " of " + RowCount;
                }

                if (!string.IsNullOrEmpty(HiddenDealerOperatorDetailsID.Value))
                {
                    if (DTDealerOperatorDetailsByID.Rows.Count > 0)
                    {
                        lblDealerCode.Text = DTDealerOperatorDetailsByID.Rows[0]["DealerCode"].ToString();
                        lblDealerName.Text = DTDealerOperatorDetailsByID.Rows[0]["DealerName"].ToString();
                        lblDealerOperatorName.Text = DTDealerOperatorDetailsByID.Rows[0]["OperatorName"].ToString();
                        lblState.Text = DTDealerOperatorDetailsByID.Rows[0]["State"].ToString();
                        lblRegion.Text = DTDealerOperatorDetailsByID.Rows[0]["Region"].ToString();
                        lblContactNumber.Text = "<a href='tel:" + DTDealerOperatorDetailsByID.Rows[0]["ContactNo"].ToString() + "'>" + DTDealerOperatorDetailsByID.Rows[0]["ContactNo"].ToString() + "</a>";
                        lblEmailID.Text = "<a href='mailto:" + DTDealerOperatorDetailsByID.Rows[0]["EmailID"].ToString() + "'>" + DTDealerOperatorDetailsByID.Rows[0]["EmailID"].ToString() + "</a>";
                        lblYearsOfExperience.Text = DTDealerOperatorDetailsByID.Rows[0]["YearsOfExperience"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? RegionID = (ddlRegion.SelectedValue != "0") ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
            new DDLBind(ddlState, new BDMS_Address().GetState(null, null, RegionID, null, null), "State", "StateID");
        }        
        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Success = false;
                if (BtnUpload.Text == "Submit")
                {
                    if (fileUpload.PostedFile != null)
                    {
                        try
                        {
                            if (DTDealerOperatorDetailsUpload.Rows.Count > 0)
                            {
                                Success = new BDealerOperatorDetails().InsertOrUpdateTDealerOperatorDetails_ForExcelUpload(DTDealerOperatorDetailsUpload);
                            }
                            if (Success)
                            {
                                lblMessage.Text = "Operator Details Was Uploaded Successfully...";
                                lblMessage.ForeColor = Color.Green;
                                DTDealerOperatorDetailsUpload = null;
                            }
                            else
                            {
                                lblMessage.Text = "Operator Details Not Uploaded Successfully...!";
                                lblMessage.ForeColor = Color.Red;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.ToString();
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    BtnUpload.Text = "Upload";
                }
                else
                {
                    Boolean Result = false;
                    Result = FillUpload();
                    if (Result)
                        BtnUpload.Text = "Submit";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
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
            divDealerOperatorDetailsView.Visible = true;
            divDealerOperatorDetailsList.Visible = false;
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            int? DealerOperatorDetailsID = Convert.ToInt32(BtnView.CommandArgument);
            HiddenDealerOperatorDetailsID.Value = DealerOperatorDetailsID.ToString();
            FillGrid();
        }
        protected void gvDealerOperatorDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDealerOperatorDetails.PageIndex = e.NewPageIndex;
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
            divDealerOperatorDetailsList.Visible = true;
            divDealerOperatorDetailsView.Visible = false;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int? DealerID = null, StateID = null, RegionID = null;
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
            RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);

            DataTable dt = new DataTable();
            dt.Columns.Add("State");
            dt.Columns.Add("Region");
            dt.Columns.Add("DealerCode", typeof(Int32));
            dt.Columns.Add("DealerName");
            dt.Columns.Add("OperatorName");
            dt.Columns.Add("ContactNo");
            dt.Columns.Add("EmailID");
            dt.Columns.Add("YearsOfExperience", typeof(decimal));

            int Index = 0;
            int Rowcount = 100;
            int CRowcount = Rowcount;

            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                DataTable DTDealerOperatorDetails = (new BDealerOperatorDetails().GetDealerOperatorDetails(null, DealerID, StateID, RegionID, null, PSession.User.UserID, Index, Rowcount));
                CRowcount = 0;
                dt.Merge(DTDealerOperatorDetails);
                CRowcount = DTDealerOperatorDetails.Rows.Count;
            }
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "DealerOperatorDetailsID")
                {
                    dt.Columns.Remove("DealerOperatorDetailsID");
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
            new BXcel().ExporttoExcel(dt, "Dealer Operator Details");
        }
        protected void BtnFUpload_Click(object sender, EventArgs e)
        {
            FldSearch.Visible = false;
            DivReport.Visible = false;
            FldUpload.Visible = true;
        }
        protected void BtnFBack_Click(object sender, EventArgs e)
        {
            FldSearch.Visible = true;
            DivReport.Visible = true;
            FldUpload.Visible = false;
        }
    }
}