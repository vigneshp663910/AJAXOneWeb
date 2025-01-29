using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class TaskSummaryDetails : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_TaskSummaryDetails; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
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
        public DataSet dtTicketSummaryDetails
        {
            get
            {
                if (ViewState["dtTicketSummaryDetails"] == null)
                {
                    ViewState["dtTicketSummaryDetails"] = new DataSet();
                }
                return (DataSet)ViewState["dtTicketSummaryDetails"];
            }
            set
            {
                ViewState["dtTicketSummaryDetails"] = value;
            }
        }
        public DataSet dtTicketSummaryDetailsView
        {
            get
            {
                if (ViewState["dtTicketSummaryDetailsView"] == null)
                {
                    ViewState["dtTicketSummaryDetailsView"] = new DataSet();
                }
                return (DataSet)ViewState["dtTicketSummaryDetailsView"];
            }
            set
            {
                ViewState["dtTicketSummaryDetailsView"] = value;
            }
        }
        long? HeaderId;
        int? Year;
        int? DepartmentID;
        int? CategoryID;
        int? CreatedBy;
        int? AssignedTo;
        string SLAType;
        string ReportType;
        int? StatusID;
        int? Month;
        string SLA;
        string TicketStatus;
        int? TicketSeverity;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Summary Details');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlCreatedBy);
                    new DDLBind().FillDealerAndEngneer(ddlDealer, ddlAssignedTo);
                    new FillDropDownt().Category(ddlCategory, null, null);
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    FillStatus();
                    new FillDropDownt().Severity(ddlSeverity, null, null);
                    txtYear.Text = DateTime.Now.Year.ToString();
                    MonthBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void FillStatus()
        {
            lbStatus.DataTextField = "Status";
            lbStatus.DataValueField = "StatusID";
            lbStatus.DataSource = JsonConvert.DeserializeObject<List<PStatus>>(JsonConvert.SerializeObject(new BTickets().getTicketStatus(null, null).Data));
            lbStatus.DataBind();
            lbStatus.Items.Insert(0, new ListItem("Select", "0"));
        }
        void MonthBind()
        {
            ddlMonth.Items.Clear();
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            for (int i = 1; i < 13; i++)
            {
                ddlMonth.Items.Add(new ListItem(info.GetMonthName(i).Substring(0, 3), i.ToString()));
            }
            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
            ddlMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
        }
        void fillter()
        {
            HeaderId = string.IsNullOrEmpty(txtTicketNo.Text.Trim()) ? (long?)null : Convert.ToInt32(txtTicketNo.Text.Trim());
            Year = string.IsNullOrEmpty(txtYear.Text.Trim()) ? (int?)null : Convert.ToInt32(txtYear.Text.Trim());
            Month = (ddlMonth.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);            
            CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            CreatedBy = ddlCreatedBy.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCreatedBy.SelectedValue);
            AssignedTo = ddlAssignedTo.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAssignedTo.SelectedValue);
            SLAType = ddlSLA.SelectedValue.ToString();
            TicketSeverity = ddlSeverity.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSeverity.SelectedValue);
            TicketStatus = "";
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    TicketStatus = TicketStatus + "," + li.Text;
                }
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlAssignedTo, DealerUser, "ContactName", "UserID");
            new DDLBind(ddlCreatedBy, DealerUser, "ContactName", "UserID");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageIndex = 1;
                FillTickets();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        void FillTickets()
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                fillter();
                GVTicketDetails.DataSource = null;
                GVTicketDetails.DataBind();
                if (ddlReportSelection.SelectedValue == "1")
                {
                    RptTickets.Visible = true;
                    RptDeptCatewise.Visible = false;
                    RptCategorywise.Visible = false;
                    RptAssignTo.Visible = false;
                    RptCreatorBy.Visible = false;
                    RptSLA.Visible = false;
                    ReportType = ddlReportSelection.SelectedItem.Text;

                    PApiResult Result = new BTickets().GetTicketSummaryDetails(HeaderId, Year, Month, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, TicketSeverity, TicketStatus, PageIndex, gvTickets.PageSize);
                    dtTicketSummaryDetails = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                    gvTickets.DataSource = dtTicketSummaryDetails;
                    gvTickets.DataBind();

                    if (Result.RowCount == 0)
                    {
                        lblRowCount.Visible = false;
                        ibtnArrowLeft.Visible = false;
                        ibtnArrowRight.Visible = false;
                    }
                    else
                    {
                        PageCount = (Result.RowCount + gvTickets.PageSize - 1) / gvTickets.PageSize;
                        lblRowCount.Visible = true;
                        ibtnArrowLeft.Visible = true;
                        ibtnArrowRight.Visible = true;
                        lblRowCount.Text = (((PageIndex - 1) * gvTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvTickets.PageSize) + gvTickets.Rows.Count) + " of " + Result.RowCount;
                    }
                    GridCustomize(gvTickets, dtTicketSummaryDetails);
                }
                else if (ddlReportSelection.SelectedValue == "2")
                {
                    RptTickets.Visible = false;
                    RptDeptCatewise.Visible = true;
                    RptCategorywise.Visible = false;
                    RptAssignTo.Visible = false;
                    RptCreatorBy.Visible = false;
                    RptSLA.Visible = false;
                    ReportType = ddlReportSelection.SelectedItem.Text.Replace("/", "");

                    PApiResult Result = new BTickets().GetTicketSummaryDetails(HeaderId, Year, Month, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, TicketSeverity, TicketStatus, PageIndex, gvDeptCatewise.PageSize);
                    dtTicketSummaryDetails = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                    gvDeptCatewise.DataSource = dtTicketSummaryDetails;
                    gvDeptCatewise.DataBind();

                    if (Result.RowCount == 0)
                    {
                        lblRowCount.Visible = false;
                        ibtnArrowLeft.Visible = false;
                        ibtnArrowRight.Visible = false;
                    }
                    else
                    {
                        PageCount = (Result.RowCount + gvDeptCatewise.PageSize - 1) / gvDeptCatewise.PageSize;
                        lblRowCount.Visible = true;
                        ibtnArrowLeft.Visible = true;
                        ibtnArrowRight.Visible = true;
                        lblRowCount.Text = (((PageIndex - 1) * gvDeptCatewise.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDeptCatewise.PageSize) + gvDeptCatewise.Rows.Count) + " of " + Result.RowCount;
                    }
                    GridCustomize(gvDeptCatewise, dtTicketSummaryDetails);
                }
                else if (ddlReportSelection.SelectedValue == "3")
                {
                    RptTickets.Visible = false;
                    RptDeptCatewise.Visible = false;
                    RptCategorywise.Visible = true;
                    RptAssignTo.Visible = false;
                    RptCreatorBy.Visible = false;
                    RptSLA.Visible = false;
                    ReportType = ddlReportSelection.SelectedItem.Text;

                    PApiResult Result = new BTickets().GetTicketSummaryDetails(HeaderId, Year, Month, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, TicketSeverity, TicketStatus, PageIndex, gvCategorywise.PageSize);
                    dtTicketSummaryDetails = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                    gvCategorywise.DataSource = dtTicketSummaryDetails;
                    gvCategorywise.DataBind();

                    if (Result.RowCount == 0)
                    {
                        lblRowCount.Visible = false;
                        ibtnArrowLeft.Visible = false;
                        ibtnArrowRight.Visible = false;
                    }
                    else
                    {
                        PageCount = (Result.RowCount + gvCategorywise.PageSize - 1) / gvCategorywise.PageSize;
                        lblRowCount.Visible = true;
                        ibtnArrowLeft.Visible = true;
                        ibtnArrowRight.Visible = true;
                        lblRowCount.Text = (((PageIndex - 1) * gvCategorywise.PageSize) + 1) + " - " + (((PageIndex - 1) * gvCategorywise.PageSize) + gvCategorywise.Rows.Count) + " of " + Result.RowCount;
                    }
                    GridCustomize(gvCategorywise, dtTicketSummaryDetails);
                }
                else if (ddlReportSelection.SelectedValue == "4")
                {
                    RptTickets.Visible = false;
                    RptDeptCatewise.Visible = false;
                    RptCategorywise.Visible = false;
                    RptAssignTo.Visible = true;
                    RptCreatorBy.Visible = false;
                    RptSLA.Visible = false;
                    ReportType = ddlReportSelection.SelectedItem.Text.Replace(" ", "");

                    PApiResult Result = new BTickets().GetTicketSummaryDetails(HeaderId, Year, Month, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, TicketSeverity, TicketStatus, PageIndex, gvAssignTo.PageSize);
                    dtTicketSummaryDetails = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                    gvAssignTo.DataSource = dtTicketSummaryDetails;
                    gvAssignTo.DataBind();

                    if (Result.RowCount == 0)
                    {
                        lblRowCount.Visible = false;
                        ibtnArrowLeft.Visible = false;
                        ibtnArrowRight.Visible = false;
                    }
                    else
                    {
                        PageCount = (Result.RowCount + gvAssignTo.PageSize - 1) / gvAssignTo.PageSize;
                        lblRowCount.Visible = true;
                        ibtnArrowLeft.Visible = true;
                        ibtnArrowRight.Visible = true;
                        lblRowCount.Text = (((PageIndex - 1) * gvAssignTo.PageSize) + 1) + " - " + (((PageIndex - 1) * gvAssignTo.PageSize) + gvAssignTo.Rows.Count) + " of " + Result.RowCount;
                    }
                    GridCustomize(gvAssignTo, dtTicketSummaryDetails);
                }
                else if (ddlReportSelection.SelectedValue == "5")
                {
                    RptTickets.Visible = false;
                    RptDeptCatewise.Visible = false;
                    RptCategorywise.Visible = false;
                    RptAssignTo.Visible = false;
                    RptCreatorBy.Visible = true;
                    RptSLA.Visible = false;
                    ReportType = ddlReportSelection.SelectedItem.Text.Replace(" ", "");

                    PApiResult Result = new BTickets().GetTicketSummaryDetails(HeaderId, Year, Month, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, TicketSeverity, TicketStatus, PageIndex, gvCreatorBy.PageSize);
                    dtTicketSummaryDetails = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                    gvCreatorBy.DataSource = dtTicketSummaryDetails;
                    gvCreatorBy.DataBind();

                    if (Result.RowCount == 0)
                    {
                        lblRowCount.Visible = false;
                        ibtnArrowLeft.Visible = false;
                        ibtnArrowRight.Visible = false;
                    }
                    else
                    {
                        PageCount = (Result.RowCount + gvCreatorBy.PageSize - 1) / gvCreatorBy.PageSize;
                        lblRowCount.Visible = true;
                        ibtnArrowLeft.Visible = true;
                        ibtnArrowRight.Visible = true;
                        lblRowCount.Text = (((PageIndex - 1) * gvCreatorBy.PageSize) + 1) + " - " + (((PageIndex - 1) * gvCreatorBy.PageSize) + gvCreatorBy.Rows.Count) + " of " + Result.RowCount;
                    }
                    GridCustomize(gvCreatorBy, dtTicketSummaryDetails);
                }
                else if (ddlReportSelection.SelectedValue == "6")
                {
                    RptTickets.Visible = false;
                    RptDeptCatewise.Visible = false;
                    RptCategorywise.Visible = false;
                    RptAssignTo.Visible = false;
                    RptCreatorBy.Visible = false;
                    RptSLA.Visible = true;
                    ReportType = ddlReportSelection.SelectedItem.Text;

                    PApiResult Result = new BTickets().GetTicketSummaryDetails(HeaderId, Year, Month, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, TicketSeverity, TicketStatus, PageIndex, gvSLA.PageSize);
                    dtTicketSummaryDetails = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                    gvSLA.DataSource = dtTicketSummaryDetails;
                    gvSLA.DataBind();

                    foreach (GridViewRow Row in gvSLA.Rows)
                    {
                        Label lnk_YesPer = (Label)Row.FindControl("lnk_YesPer");
                        if (lnk_YesPer.Text == "100")
                        {
                            Row.Cells[6].BackColor = Color.Green;
                            Row.Cells[7].BackColor = Color.Green;
                        }
                        else
                        {
                            Row.Cells[6].BackColor = Color.Red;
                            Row.Cells[7].BackColor = Color.Red;
                        }
                    }

                    if (Result.RowCount == 0)
                    {
                        lblRowCount.Visible = false;
                        ibtnArrowLeft.Visible = false;
                        ibtnArrowRight.Visible = false;
                    }
                    else
                    {
                        PageCount = (Result.RowCount + gvSLA.PageSize - 1) / gvSLA.PageSize;
                        lblRowCount.Visible = true;
                        ibtnArrowLeft.Visible = true;
                        ibtnArrowRight.Visible = true;
                        lblRowCount.Text = (((PageIndex - 1) * gvSLA.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSLA.PageSize) + gvSLA.Rows.Count) + " of " + Result.RowCount;
                    }
                }
                lblViewRowCount.Text = "";
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        void GridCustomize(GridView gv, DataSet dtTicketSummaryDetails)
        {
            if (dtTicketSummaryDetails.Tables[0].Rows.Count == 0)
            {
                return;
            }
            if (ddlReportSelection.SelectedValue == "6")
            {
                return;
            }
            int index = 0;
            if (ddlReportSelection.SelectedValue == "1")
            {
                index = 0;
            }
            else if (ddlReportSelection.SelectedValue == "2")
            {
                index = 2;
            }
            else if (ddlReportSelection.SelectedValue == "3")
            {
                index = 1;
            }
            else if (ddlReportSelection.SelectedValue == "4")
            {
                index = 3;
            }
            else if (ddlReportSelection.SelectedValue == "5")
            {
                index = 3;
            }
            //gv.FooterRow.BackColor = Color.FromArgb(0, 154, 215);
            gv.FooterRow.CssClass = "FooterStyle";
            gv.FooterRow.Cells[index + 2].Text = "Total";
            gv.FooterRow.Cells[index + 2].ForeColor = Color.Black;
            gv.FooterRow.Cells[index + 2].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 2].Font.Bold = true;
            gv.FooterRow.Cells[index + 3].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Created)", "").ToString();
            gv.FooterRow.Cells[index + 3].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 3].BackColor = Color.FromArgb(135, 117, 167);
            gv.FooterRow.Cells[index + 3].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 4].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Opened)", "").ToString();
            gv.FooterRow.Cells[index + 4].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 4].BackColor = Color.Firebrick;
            gv.FooterRow.Cells[index + 4].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 5].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Assigned)", "").ToString();
            gv.FooterRow.Cells[index + 5].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 5].BackColor = Color.Firebrick;
            gv.FooterRow.Cells[index + 5].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 6].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Inprogress)", "").ToString();
            gv.FooterRow.Cells[index + 6].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 6].BackColor = Color.Firebrick;
            gv.FooterRow.Cells[index + 6].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 7].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Approval)", "").ToString();
            gv.FooterRow.Cells[index + 7].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 7].BackColor = Color.Firebrick;
            gv.FooterRow.Cells[index + 7].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 8].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Approved)", "").ToString();
            gv.FooterRow.Cells[index + 8].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 8].BackColor = Color.Firebrick;
            gv.FooterRow.Cells[index + 8].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 9].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Resolved)", "").ToString();
            gv.FooterRow.Cells[index + 9].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 9].BackColor = Color.DarkOliveGreen;
            gv.FooterRow.Cells[index + 9].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 10].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Closed)", "").ToString();
            gv.FooterRow.Cells[index + 10].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 10].BackColor = Color.DarkOliveGreen;
            gv.FooterRow.Cells[index + 10].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 11].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Rejected)", "").ToString();
            gv.FooterRow.Cells[index + 11].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 11].BackColor = Color.DarkOliveGreen;
            gv.FooterRow.Cells[index + 11].ForeColor = Color.White;
            gv.FooterRow.Cells[index + 12].Text = dtTicketSummaryDetails.Tables[0].Compute("SUM(Deleted)", "").ToString();
            gv.FooterRow.Cells[index + 12].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 12].BackColor = Color.DarkOliveGreen;
            gv.FooterRow.Cells[index + 12].ForeColor = Color.White;
            double Average = ((Convert.ToDouble(dtTicketSummaryDetails.Tables[0].Compute("SUM(Opened)", "")) + Convert.ToDouble(dtTicketSummaryDetails.Tables[0].Compute("SUM(Approval)", "")) + Convert.ToDouble(dtTicketSummaryDetails.Tables[0].Compute("SUM(Assigned)", "")) + Convert.ToDouble(dtTicketSummaryDetails.Tables[0].Compute("SUM(Inprogress)", "")) + Convert.ToDouble(dtTicketSummaryDetails.Tables[0].Compute("SUM(Approved)", ""))) / Convert.ToDouble(dtTicketSummaryDetails.Tables[0].Compute("SUM(Created)", ""))) * 100;
            gv.FooterRow.Cells[index + 13].Text = Average.ToString("N2");
            gv.FooterRow.Cells[index + 13].HorizontalAlign = HorizontalAlign.Center;
            gv.FooterRow.Cells[index + 13].ForeColor = Color.Black;

            GridViewRow footer = gv.FooterRow;
            int numCells = footer.Cells.Count;
            GridViewRow newRow = new GridViewRow(footer.RowIndex + 1, -1, footer.RowType, footer.RowState);
            for (int j = 0; j <= numCells - 1; j++)
            {
                TableCell emptyCell = new TableCell();
                emptyCell.ApplyStyle(gv.Columns[j].ItemStyle);
                newRow.Cells.Add(emptyCell);
            }
            //newRow.BackColor = Color.FromArgb(0, 154, 215);
            newRow.CssClass = "FooterStyle";
            newRow.Cells[index + 2].Text = "Average";
            newRow.Cells[index + 2].ForeColor = Color.Black;
            newRow.Cells[index + 2].Font.Bold = true;
            newRow.Cells[index + 3].Text = (Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Created)", "")) / Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Rows.Count)).ToString();
            newRow.Cells[index + 3].ForeColor = Color.White;
            newRow.Cells[index + 3].BackColor = Color.FromArgb(135, 117, 167);
            newRow.Cells[index + 8].Text = (Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Opened)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Assigned)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Inprogress)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Approval)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Approved)", ""))).ToString();
            newRow.Cells[index + 8].ForeColor = Color.White;
            newRow.Cells[index + 8].BackColor = Color.Firebrick;
            newRow.Cells[index + 12].Text = (Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Resolved)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Closed)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Rejected)", "")) + Convert.ToInt32(dtTicketSummaryDetails.Tables[0].Compute("SUM(Deleted)", ""))).ToString();
            newRow.Cells[index + 12].BackColor = Color.DarkOliveGreen;
            newRow.Cells[index + 12].ForeColor = Color.White;
            gv.Controls[0].Controls.Add(newRow);
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillTickets();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillTickets();
            }
        }
        protected void TicketHeaderDetails_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                fillter();

                if (ddlReportSelection.SelectedValue == "1")
                {
                    ReportType = ddlReportSelection.SelectedItem.Text;
                }
                else if (ddlReportSelection.SelectedValue == "2")
                {
                    ReportType = ddlReportSelection.SelectedItem.Text.Replace("/", "");
                }
                else if (ddlReportSelection.SelectedValue == "3")
                {
                    ReportType = ddlReportSelection.SelectedItem.Text;
                }
                else if (ddlReportSelection.SelectedValue == "4")
                {
                    ReportType = ddlReportSelection.SelectedItem.Text.Replace(" ", "");
                }
                else if (ddlReportSelection.SelectedValue == "5")
                {
                    ReportType = ddlReportSelection.SelectedItem.Text.Replace(" ", "");
                }
                else if (ddlReportSelection.SelectedValue == "6")
                {
                    ReportType = ddlReportSelection.SelectedItem.Text;
                }

                LinkButton TicketHeaderDetails = (LinkButton)sender;
                GridViewRow Row = (GridViewRow)TicketHeaderDetails.NamingContainer;
                Label lblMonth = (Label)Row.FindControl("lblMonth");
                if (ddlReportSelection.SelectedValue != "6")
                    StatusID = (TicketHeaderDetails.CommandArgument == "0") ? (int?)null : Convert.ToInt32(TicketHeaderDetails.CommandArgument);
                Month = Convert.ToInt32(lblMonth.Text);

                if (Row.NamingContainer.ID == "gvDeptCatewise")
                {
                    DepartmentID = Convert.ToInt32(((Label)Row.FindControl("lblDepartmentID")).Text);
                    CategoryID = Convert.ToInt32(((Label)Row.FindControl("lblCategoryID")).Text);
                }
                else if (Row.NamingContainer.ID == "gvCategorywise")
                {
                    CategoryID = Convert.ToInt32(((Label)Row.FindControl("lblCategoryID")).Text);
                }
                else if (Row.NamingContainer.ID == "gvAssignTo")
                {
                    AssignedTo = Convert.ToInt32(((Label)Row.FindControl("lblAssignedTo")).Text);
                    DepartmentID = Convert.ToInt32(((Label)Row.FindControl("lblDepartmentID")).Text);
                    CategoryID = Convert.ToInt32(((Label)Row.FindControl("lblCategoryID")).Text);
                }
                else if (Row.NamingContainer.ID == "gvCreatorBy")
                {
                    CreatedBy = Convert.ToInt32(((Label)Row.FindControl("lblCreatedBy")).Text);
                    DepartmentID = Convert.ToInt32(((Label)Row.FindControl("lblDepartmentID")).Text);
                    CategoryID = Convert.ToInt32(((Label)Row.FindControl("lblCategoryID")).Text);
                }
                else if (Row.NamingContainer.ID == "gvSLA")
                {
                    CategoryID = Convert.ToInt32(((Label)Row.FindControl("lblCategoryID")).Text);
                    LinkButton lnkSLA = (LinkButton)sender;
                    SLA = lnkSLA.CommandArgument;
                }
                PApiResult Result = new BTickets().GetTicketSummaryDetails_TicketInfo(HeaderId, Year, DepartmentID, txtTicketFrom.Text, txtTicketTo.Text, CategoryID, CreatedBy, AssignedTo, SLAType, ReportType, StatusID, Month, SLA, TicketSeverity, TicketStatus);
                dtTicketSummaryDetailsView = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));

                fillTicketDetails();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        void fillTicketDetails()
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                GVTicketDetails.DataSource = dtTicketSummaryDetailsView;
                GVTicketDetails.DataBind();

                if (dtTicketSummaryDetailsView.Tables[0].Rows.Count == 0)
                {
                    lblViewRowCount.Visible = false;
                }
                else
                {
                    lblViewRowCount.Visible = true;
                    lblViewRowCount.Text = (((GVTicketDetails.PageIndex) * GVTicketDetails.PageSize) + 1) + " - " + (((GVTicketDetails.PageIndex) * GVTicketDetails.PageSize) + GVTicketDetails.Rows.Count) + " of " + dtTicketSummaryDetailsView.Tables[0].Rows.Count;
                }
                GridCustomize(gvTickets, dtTicketSummaryDetails);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void GVTicketDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVTicketDetails.PageIndex = e.NewPageIndex;
            fillTicketDetails();
        }
        protected void lnk_HeaderID_Click(object sender, EventArgs e)
        {
            LinkButton lnk_HeaderID = (LinkButton)sender;
            divSupportTicketView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            UC_SupportTicketView.FillTickets(Convert.ToInt32(lnk_HeaderID.Text));
            UC_SupportTicketView.FillChat(Convert.ToInt32(lnk_HeaderID.Text));
            UC_SupportTicketView.FillChatTemp(Convert.ToInt32(lnk_HeaderID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divSupportTicketView.Visible = false;
            btnBackToList.Visible = false;
            divList.Visible = true;
        }
    }
}