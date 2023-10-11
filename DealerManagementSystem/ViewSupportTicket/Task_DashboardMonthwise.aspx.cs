using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSupportTicket
{
    public partial class Task_DashboardMonthwise : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSupportTicket_Task_DashboardMonthwise; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Monthwise Dashboard');</script>");
            if (!IsPostBack)
            {
                FillYearAndMonth();
                new DDLBind().FillDealerAndEngneer(ddlDealer, ddlEmployee);
            }
        }
        void FillYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = 2022; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(i + 1 - 2022, new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();

            ddlMonth.Items.Insert(0, new ListItem("Select", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(i, new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, DealerID, true, null, null, null);
            new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            MonthWiseReport();
        }
        void MonthWiseReport()
        {
            int? DealerEmployeeUserID = (ddlEmployee.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlEmployee.SelectedValue);
            string Year = (ddlYear.SelectedValue == "0") ? (string)null : ddlYear.SelectedValue;
            string Month = (ddlMonth.SelectedValue == "0") ? (string)null : ddlMonth.SelectedValue;
            int? DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            var lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month));
            DateTime DateFrom = Convert.ToDateTime("01-" + Month + "-" + Year);
            DateTime DateTo = Convert.ToDateTime(lastDayOfMonth + "-" + Month + "-" + Year);
            Boolean Dealerwise = ((DealerID != null && ddlEmployee.SelectedValue == "0") || (DealerID == null && ddlEmployee.SelectedValue == "0")) ? true : false;
            DataSet ds = new BTickets().GetTicketDetailsMonthwiseCountByStatus(DealerEmployeeUserID, DealerID, Dealerwise, DateFrom, DateTo);
            gvTicketsMonthwise.DataSource = null;
            gvTicketsMonthwise.DataBind();
            DataTable dtMonthwise = new DataTable();
            dtMonthwise.Columns.Add("Date", typeof(string));
            dtMonthwise.Columns.Add("Opening", typeof(Int32));
            dtMonthwise.Columns.Add("Created", typeof(Int32));
            dtMonthwise.Columns.Add("Closed", typeof(Int32));
            dtMonthwise.Columns.Add("Closing", typeof(Int32));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
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
                    //gvTicketsMonthwise.DataSource = dtMonthwise;
                    //gvTicketsMonthwise.DataBind();
                }
            }
            DataTable dt2 = new DataTable();
            for (int i = 0; i <= dtMonthwise.Rows.Count; i++)
            {
                dt2.Columns.Add();
            }
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

            dt2.AcceptChanges();
            gvTicketsMonthwise.DataSource = dt2;
            gvTicketsMonthwise.DataBind();
            int rowcount = 0;
            foreach (GridViewRow row in gvTicketsMonthwise.Rows)
            {
                row.Cells[0].CssClass = "Header";
                rowcount += 1;
                if(rowcount==1)
                {
                    for(int i = 0; i < row.Cells.Count; i++)
                    {
                        row.Cells[i].CssClass = "Header";
                    }
                }
            }
        }
    }
}