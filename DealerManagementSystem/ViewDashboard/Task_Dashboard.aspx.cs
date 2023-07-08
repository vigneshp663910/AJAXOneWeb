using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard
{
    public partial class Task_Dashboard : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDashboard_Task_Dashboard; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        DateTime? From = null;
        DateTime? To = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Dashboard');</script>");
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlDealerEmployee);
                new FillDropDownt().Category(ddlCategory, null, null);
                ddlCategory_SelectedIndexChanged(null, null);
                //List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, null, true, null, 7, null);
                List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, null, true, null, null, null);
                //new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");
                //if(PSession.User.Department.DealerDepartmentID!=7)
                //{
                //    ddlEmployee.SelectedValue = PSession.User.UserID.ToString();
                //    ddlEmployee.Enabled = false;
                //}
                //else
                //{
                //    ddlEmployee.Enabled = true;
                //}
                var today = DateTime.Now;
                txtTicketFrom.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                txtTicketTo.Text = DateTime.Now.ToString("yyyy-MM-dd");
                FillStatusCount();
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlDealerEmployee, DealerUser, "ContactName", "UserID");
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            new FillDropDownt().SubCategory(ddlSubcategory, null, null, CategoryID);
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            Session["DashboardTaskUserID"] = (ddlDealerEmployee.SelectedValue == "0") ? PSession.User.UserID.ToString() : ddlDealerEmployee.SelectedValue;
            Session["DashboardTaskDealerID"] = ddlDealer.SelectedValue;
            if (lbActions.Text == "Created")
            {
                Session["DashboardTaskStatus"] = "Created";
                if (ddlDealer.SelectedValue != "53")
                {
                    if (ddlDealerEmployee.SelectedValue != "0")
                    {
                        Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                    }
                }
            }
            else if (lbActions.Text == "Open")
            {
                Session["DashboardTaskStatus"] = ",Open";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
            else if (lbActions.Text == "Assigned")
            {
                Session["DashboardTaskStatus"] = ",Assigned";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
            else if (lbActions.Text == "InProgress")
            {
                Session["DashboardTaskStatus"] = ",In Progress";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
            else if (lbActions.Text == "Approved")
            {
                Session["DashboardTaskStatus"] = ",Approved";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
            else if (lbActions.Text == "Reject")
            {
                Session["DashboardTaskStatus"] = ",Reject";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
            else if (lbActions.Text == "Resolved+")
            {
                Session["DashboardTaskStatus"] = ",Resolved,Closed,Cancel,Foreclose";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
            else if (lbActions.Text == "Approval")
            {
                Session["DashboardTaskStatus"] = ",Waiting for Approval";
                if (ddlDealerEmployee.SelectedValue != "0")
                {
                    Response.Redirect("../ViewSupportTicket/ManageSupportTicket.aspx");
                }
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillStatusCount();

        }
        void FillStatusCount()
        {
            lblCreated.Text = "0";
            lblOpen.Text = "0";
            lblWaitingForApproval.Text = "0";
            lblAssigned.Text = "0";
            lblInProgress.Text = "0";
            lblApproved.Text = "0";
            //lblResolved.Text = "0";
            lblReject.Text = "0";
            lblClosed.Text = "0";
            DateTime TodayDate = DateTime.Now.Date;

            if (!string.IsNullOrEmpty(txtTicketFrom.Text))
            {
                From = Convert.ToDateTime(txtTicketFrom.Text);
            }
            if (!string.IsNullOrEmpty(txtTicketTo.Text))
            {
                To = Convert.ToDateTime(txtTicketTo.Text);
            }
            int? CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
            int? SubCategoryID = ddlSubcategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubcategory.SelectedValue);
            int? DealerEmployeeUserID = ddlDealerEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DealerEmployeeUserID = (DealerEmployeeUserID == null) ? PSession.User.UserID : DealerEmployeeUserID;
            Boolean Dealerwise = ((DealerID != null && ddlDealerEmployee.SelectedValue == "0")|| (DealerID == null && ddlDealerEmployee.SelectedValue == "0")) ? true : false;
            gvTickets.DataSource = null;
            gvTickets.DataBind();
            DataSet ds = new BTickets().GetTicketDetailsCountByStatus(DealerID, DealerEmployeeUserID, Dealerwise, null, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCreated.Text = ds.Tables[0].Compute("Sum(TotalCreated)", "").ToString();
                lblOpen.Text = ds.Tables[0].Compute("Sum(Opened)", "").ToString();
                lblWaitingForApproval.Text = ds.Tables[0].Compute("Sum(WaitingForApproval)", "").ToString();
                lblAssigned.Text = ds.Tables[0].Compute("Sum(Assigned)", "").ToString();
                lblInProgress.Text = ds.Tables[0].Compute("Sum(InProgress)", "").ToString();
                lblApproved.Text = ds.Tables[0].Compute("Sum(Approved)", "").ToString();
                lblReject.Text = ds.Tables[0].Compute("Sum(Reject)", "").ToString();
                //lblResolved.Text = ds.Tables[0].Compute("Sum(Resolved)", "").ToString();
                //lblClosed.Text = ds.Tables[0].Compute("Sum(Closed)", "").ToString();
                lblClosed.Text = (Convert.ToDouble(ds.Tables[0].Compute("Sum(Resolved)", "")) + Convert.ToDouble(ds.Tables[0].Compute("Sum(Closed)", ""))).ToString();

                gvTickets.DataSource = ds.Tables[0];
                gvTickets.DataBind();

                gvTickets.FooterRow.CssClass = "FooterStyle";
                gvTickets.FooterRow.Cells[1].Text = "Total";
                gvTickets.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[2].Text = ds.Tables[0].Compute("SUM(TotalCreated)", "").ToString();//total.ToString("N2");
                gvTickets.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[2].BackColor = Color.FromArgb(135, 117, 167);
                gvTickets.FooterRow.Cells[2].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[3].Text = ds.Tables[0].Compute("SUM(Opened)", "").ToString();
                gvTickets.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[3].BackColor = Color.Firebrick;
                gvTickets.FooterRow.Cells[3].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[4].Text = ds.Tables[0].Compute("SUM(Assigned)", "").ToString();
                gvTickets.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[4].BackColor = Color.Firebrick;
                gvTickets.FooterRow.Cells[4].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[5].Text = ds.Tables[0].Compute("SUM(InProgress)", "").ToString();
                gvTickets.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[5].BackColor = Color.Firebrick;
                gvTickets.FooterRow.Cells[5].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[6].Text = ds.Tables[0].Compute("SUM(WaitingForApproval)", "").ToString();
                gvTickets.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[6].BackColor = Color.Firebrick;
                gvTickets.FooterRow.Cells[6].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[7].Text = ds.Tables[0].Compute("SUM(Approved)", "").ToString();
                gvTickets.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[7].BackColor = Color.Firebrick;
                gvTickets.FooterRow.Cells[7].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[8].Text = ds.Tables[0].Compute("SUM(Reject)", "").ToString();
                gvTickets.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[8].BackColor = Color.DarkOliveGreen;
                gvTickets.FooterRow.Cells[8].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[9].Text = ds.Tables[0].Compute("SUM(Resolved)", "").ToString();
                gvTickets.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[9].BackColor = Color.DarkOliveGreen;
                gvTickets.FooterRow.Cells[9].ForeColor = Color.White;
                gvTickets.FooterRow.Cells[10].Text = ds.Tables[0].Compute("SUM(Closed)", "").ToString();
                gvTickets.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                gvTickets.FooterRow.Cells[10].BackColor = Color.DarkOliveGreen;
                gvTickets.FooterRow.Cells[10].ForeColor = Color.White;
                double Average = ((Convert.ToDouble(ds.Tables[0].Compute("SUM(Opened)", "")) + Convert.ToDouble(ds.Tables[0].Compute("SUM(WaitingForApproval)", "")) + Convert.ToDouble(ds.Tables[0].Compute("SUM(Assigned)", "")) + Convert.ToDouble(ds.Tables[0].Compute("SUM(InProgress)", "")) + Convert.ToDouble(ds.Tables[0].Compute("SUM(Approved)", "")) + Convert.ToDouble(ds.Tables[0].Compute("SUM(Reject)", ""))) / Convert.ToDouble(ds.Tables[0].Compute("SUM(TotalCreated)", ""))) * 100;
                gvTickets.FooterRow.Cells[11].Text = Average.ToString("N2");
                gvTickets.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;

                GridViewRow footer = gvTickets.FooterRow;
                int numCells = footer.Cells.Count;
                GridViewRow newRow = new GridViewRow(footer.RowIndex + 1, -1, footer.RowType, footer.RowState);
                for (int j = 0; j <= numCells - 1; j++)
                {
                    TableCell emptyCell = new TableCell();
                    emptyCell.ApplyStyle(gvTickets.Columns[j].ItemStyle);
                    newRow.Cells.Add(emptyCell);
                }
                newRow.Cells[1].Text = "Average";
                newRow.Cells[2].Text = (Convert.ToInt32(ds.Tables[0].Compute("SUM(TotalCreated)", "")) / Convert.ToInt32(ds.Tables[0].Rows.Count)).ToString();
                newRow.Cells[2].ForeColor = Color.FromArgb(135, 117, 167);
                newRow.Cells[7].Text = (Convert.ToInt32(ds.Tables[0].Compute("SUM(Opened)", "")) + Convert.ToInt32(ds.Tables[0].Compute("SUM(Assigned)", "")) + Convert.ToInt32(ds.Tables[0].Compute("SUM(InProgress)", "")) + Convert.ToInt32(ds.Tables[0].Compute("SUM(WaitingForApproval)", "")) + Convert.ToInt32(ds.Tables[0].Compute("SUM(Approved)", ""))).ToString();
                newRow.Cells[7].ForeColor = Color.Firebrick;
                newRow.Cells[10].Text = (Convert.ToInt32(ds.Tables[0].Compute("SUM(Resolved)", "")) + Convert.ToInt32(ds.Tables[0].Compute("SUM(Closed)", "")) + Convert.ToInt32(ds.Tables[0].Compute("SUM(Reject)", ""))).ToString();
                newRow.Cells[10].ForeColor = Color.DarkOliveGreen;
                gvTickets.Controls[0].Controls.Add(newRow);



                lblMonthlyReportTitle.Text = (ddlDealerEmployee.SelectedValue == "0") ? " Over All Status" : " Over All Status - " + ddlDealerEmployee.SelectedItem.Text.Trim();
            }
            gvTicketsMonthwise.DataSource = null;
            gvTicketsMonthwise.DataBind();

            ClientScript.RegisterStartupScript(GetType(), "hwa1", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(TaskStatusChart); ", true);
            if (!string.IsNullOrEmpty(txtTicketTo.Text))
            {
                DaywiseReport();
                ClientScript.RegisterStartupScript(GetType(), "hwa2", "google.charts.load('current', { packages: ['corechart'] });  google.charts.setOnLoadCallback(DailyTaskStatusChart); ", true);
            }
        }
        [WebMethod]
        public static List<object> TaskStatusChart(string DateFrom, string DateTo, string DealerEmployeeUser, string Dealer)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[] { "Year&Month", "Created", "Closed", "Progress" });
            //int? CategoryID = Category == "0" ? (int?)null : Convert.ToInt32(Category);
            //int? SubcategoryID = Subcategory == "0" ? (int?)null : Convert.ToInt32(Subcategory);
            int? DealerEmployeeUserID = DealerEmployeeUser == "0" ? (int?)null : Convert.ToInt32(DealerEmployeeUser);
            int? DealerID = Dealer == "0" ? (int?)null : Convert.ToInt32(Dealer);
            DealerEmployeeUserID = (DealerEmployeeUserID == null) ? PSession.User.UserID : DealerEmployeeUserID;
            Boolean Dealerwise = ((DealerID != null && DealerEmployeeUser == "0") || (DealerID == null && DealerEmployeeUser == "0")) ? true : false;
            DateTime? From = string.IsNullOrEmpty(DateFrom) ? (DateTime?)null : Convert.ToDateTime(DateFrom);
            DateTime? To = string.IsNullOrEmpty(DateTo) ? (DateTime?)null : Convert.ToDateTime(DateTo);

            //DataSet ds = new BTickets().GetTicketDetailsCountByStatusForChart(CategoryID, SubcategoryID, PSession.User.UserID, From, To);
            DataSet ds = new BTickets().GetTicketDetailsCountByStatusForChart(DealerEmployeeUserID, DealerID, Dealerwise, null, null);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        chartData.Add(new object[] { Convert.ToString(dr["Year"]) + "-" + Convert.ToString(dr["Month"]), Convert.ToInt32(dr["TotalCreated"]), Convert.ToInt32(dr["Closed"]), Convert.ToInt32(dr["InProgress"]) });
                    }
                }
                else
                {
                    chartData = null;
                }
            }
            else
            {
                chartData = null;
            }
            return chartData;
        }
        void DaywiseReport()
        {
            int? DealerEmployeeUserID = (ddlDealerEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealerEmployee.SelectedValue);
            string Year = string.IsNullOrEmpty(txtTicketTo.Text) ? DateTime.Now.Year.ToString() : Convert.ToDateTime(txtTicketTo.Text).Year.ToString();
            string Month = string.IsNullOrEmpty(txtTicketTo.Text) ? DateTime.Now.Month.ToString() : Convert.ToDateTime(txtTicketTo.Text).Month.ToString();
            int? DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            var lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month));
            From = Convert.ToDateTime("01-" + Month + "-" + Year);
            To = Convert.ToDateTime(lastDayOfMonth + "-" + Month + "-" + Year);
            DealerEmployeeUserID = (DealerEmployeeUserID == null) ? PSession.User.UserID : DealerEmployeeUserID;
            Boolean Dealerwise = ((DealerID != null && ddlDealerEmployee.SelectedValue == "0") || (DealerID == null && ddlDealerEmployee.SelectedValue == "0")) ? true : false;
            if (DateTime.Now < To)
            {
                if (DateTime.Now.Month == Convert.ToDateTime(To).Month)
                {
                    lastDayOfMonth = DateTime.Now.Day;
                }
                else
                {
                    return;
                }
            }

            DataSet ds = new BTickets().GetTicketDetailsMonthwiseCountByStatus(DealerEmployeeUserID, DealerID, Dealerwise, From, To);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtMonthwise = new DataTable();
                    dtMonthwise.Columns.Add("Date", typeof(string));
                    dtMonthwise.Columns.Add("Opening", typeof(Int32));
                    dtMonthwise.Columns.Add("Created", typeof(Int32));
                    dtMonthwise.Columns.Add("Closed", typeof(Int32));
                    dtMonthwise.Columns.Add("Closing", typeof(Int32));

                    int OP = Convert.ToInt32(ds.Tables[0].Rows[0]["OpeningBalance"]), CL = 0, Count = 0, CreatedCount = 0, ClosedCount = 0;
                    for (int i = 1; i <= lastDayOfMonth; i++)
                    {
                        var Tickets = from Ticket in ds.Tables[0].AsEnumerable()
                                      where Ticket.Field<DateTime>("TnDate") == Convert.ToDateTime(i + "-" + Month + "-" + Year)
                                      select Ticket;
                        var data = Tickets.ToList();
                        if (data.Count > 0)
                        {
                            Count += 1;
                            OP = (Count == 1) ? OP : CL;
                            CL = (OP + Convert.ToInt32(data[0][1].ToString())) - Convert.ToInt32(data[0][2]);
                            dtMonthwise.Rows.Add(Convert.ToDateTime(data[0][4]).ToString("dd"), OP, Convert.ToInt32(data[0][1]), Convert.ToInt32(data[0][2]), CL);
                            CreatedCount += Convert.ToInt32(data[0][1]);
                            ClosedCount += Convert.ToInt32(data[0][2]);
                            lblDailyReportTitle.Text = (ddlDealerEmployee.SelectedValue == "0") ? "Over All Status" : ((string.IsNullOrEmpty(txtTicketTo.Text)) ? "" : " For The Month Of " + Convert.ToDateTime(txtTicketTo.Text).ToString("MMM-yyyy") + " - " + ddlDealerEmployee.SelectedItem.Text.Trim());
                        }
                        else
                        {
                            Count += 1;
                            OP = (Count == 1) ? OP : CL;
                            CL = OP;
                            dtMonthwise.Rows.Add(i.ToString("00"), OP, 0, 0, CL);
                        }
                    }

                    DataTable dt2 = new DataTable();
                    for (int i = 0; i <= dtMonthwise.Rows.Count; i++)
                    {
                        dt2.Columns.Add();
                    }
                    dt2.Columns.Add("Total", typeof(String));
                    for (int i = 0; i < dtMonthwise.Columns.Count; i++)
                    {
                        dt2.Rows.Add();
                        dt2.Rows[i][0] = dtMonthwise.Columns[i].ColumnName;
                    }
                    for (int i = 0; i < dtMonthwise.Columns.Count; i++)
                    {
                        for (int j = 0; j < dtMonthwise.Rows.Count; j++)
                        {
                            dt2.Rows[i][j + 1] = dtMonthwise.Rows[j][i];
                        }
                    }
                    dt2.Rows[0]["Total"] = "Total";
                    dt2.Rows[1]["Total"] = Convert.ToInt32(ds.Tables[0].Rows[0]["OpeningBalance"]);
                    dt2.Rows[2]["Total"] = CreatedCount;
                    dt2.Rows[3]["Total"] = ClosedCount;
                    dt2.Rows[4]["Total"] = (Convert.ToInt32(ds.Tables[0].Rows[0]["OpeningBalance"]) + CreatedCount) - ClosedCount;
                    dt2.AcceptChanges();
                    gvTicketsMonthwise.DataSource = dt2;
                    gvTicketsMonthwise.DataBind();
                    int rowcount = 0;
                    foreach (GridViewRow row in gvTicketsMonthwise.Rows)
                    {
                        row.Cells[0].CssClass = "Header";
                        rowcount += 1;
                        if (rowcount == 1)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                row.Cells[i].CssClass = "Header";
                            }
                        }
                    }
                }
            }
        }
        [WebMethod]
        public static List<object> DailyTaskStatusChart(string DateFrom, string DateTo, string DealerEmployeeUser, string Dealer)
        {
            List<object> chartData = new List<object>();
            //chartData.Add(new object[] { "Date", "Opening Balance", "Created", "Closed", "Closing Balance" });
            chartData.Add(new object[] { "Date", "Opening + Created", "Closed" });
            int? DealerEmployeeUserID = DealerEmployeeUser == "0" ? (int?)null : Convert.ToInt32(DealerEmployeeUser);
            int? DealerID = Dealer == "0" ? (int?)null : Convert.ToInt32(Dealer);

            string Year = string.IsNullOrEmpty(DateTo) ? DateTime.Now.Year.ToString() : Convert.ToDateTime(DateTo).Year.ToString();
            string Month = string.IsNullOrEmpty(DateTo) ? DateTime.Now.Month.ToString() : Convert.ToDateTime(DateTo).Month.ToString();
            var lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month));
            DateTime? From = Convert.ToDateTime("01-" + Month + "-" + Year);
            DateTime? To = Convert.ToDateTime(lastDayOfMonth + "-" + Month + "-" + Year);
            DealerEmployeeUserID = (DealerEmployeeUserID == null) ? PSession.User.UserID : DealerEmployeeUserID;
            Boolean Dealerwise = ((DealerID != null && DealerEmployeeUser == "0") || (DealerID == null && DealerEmployeeUser == "0")) ? true : false;
            if (DateTime.Now < To)
            {
                if (DateTime.Now.Month == Convert.ToDateTime(To).Month)
                {
                    lastDayOfMonth = DateTime.Now.Day;
                }
                else
                {
                    chartData = null;
                    goto Fail;
                }
            }

            DataSet ds = new BTickets().GetTicketDetailsMonthwiseCountByStatus(DealerEmployeeUserID, DealerID, Dealerwise, From, To);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtMonthwise = new DataTable();
                    dtMonthwise.Columns.Add("Date", typeof(string));
                    dtMonthwise.Columns.Add("Opening", typeof(Int32));
                    dtMonthwise.Columns.Add("Created", typeof(Int32));
                    dtMonthwise.Columns.Add("Closed", typeof(Int32));
                    dtMonthwise.Columns.Add("Closing", typeof(Int32));

                    int OP = Convert.ToInt32(ds.Tables[0].Rows[0]["OpeningBalance"]), CL = 0, Count = 0;
                    for (int i = 1; i <= lastDayOfMonth; i++)
                    {
                        var Tickets = from Ticket in ds.Tables[0].AsEnumerable()
                                      where Ticket.Field<DateTime>("TnDate") == Convert.ToDateTime(i + "-" + Month + "-" + Year)
                                      select Ticket;
                        var data = Tickets.ToList();
                        if (data.Count > 0)
                        {
                            Count += 1;
                            OP = (Count == 1) ? OP : CL;
                            CL = (OP + Convert.ToInt32(data[0][1].ToString())) - Convert.ToInt32(data[0][2]);
                            dtMonthwise.Rows.Add(Convert.ToDateTime(data[0][4]).ToString("dd"), OP, Convert.ToInt32(data[0][1]), Convert.ToInt32(data[0][2]), CL);
                        }
                        else
                        {
                            Count += 1;
                            OP = (Count == 1) ? OP : CL;
                            CL = OP;
                            dtMonthwise.Rows.Add(i.ToString("00"), OP, 0, 0, CL);
                        }
                    }

                    foreach (DataRow dr in dtMonthwise.Rows)
                    {
                        //chartData.Add(new object[] { Convert.ToString(dr["Date"]), Convert.ToInt32(dr["Opening"]), Convert.ToInt32(dr["Created"]), Convert.ToInt32(dr["Closed"]), Convert.ToInt32(dr["Closing"]) });
                        chartData.Add(new object[] { Convert.ToString(dr["Date"]), (Convert.ToInt32(dr["Opening"]) + Convert.ToInt32(dr["Created"])), Convert.ToInt32(dr["Closed"]) });
                    }
                }
                else
                {
                    chartData = null;
                }
            }
            else
            {
                chartData = null;
            }
        Fail:
            return chartData;
        }
    }
}