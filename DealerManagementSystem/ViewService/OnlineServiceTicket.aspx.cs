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

namespace DealerManagementSystem.ViewService
{
    public partial class OnlineServiceTicket : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicket; } }
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Online Ticket » Manage');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                fillStatus();
                new BDMS_Division().GetDivisionForSerchGroped(ddlDivision); 

                txtDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtDateTo.Text = DateTime.Now.ToShortDateString();

                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            void fillStatus()
            {
                ddlStatus.DataTextField = "StatusItemID";
                ddlStatus.DataValueField = "StatusItem";
                ddlStatus.DataSource = new BMaster().GetStatusItem(1);
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicket();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillICTicket()
        {
            try
            {
                PApiResult Result = GetICTicket(0);
                gvICTickets.DataSource = JsonConvert.DeserializeObject<List<POnlineServiceTicket>>(JsonConvert.SerializeObject(Result.Data));
                gvICTickets.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvICTickets.PageSize - 1) / gvICTickets.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    //  lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTickets.Count;
                    lblRowCount.Text = (((PageIndex - 1) * gvICTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Result.RowCount;
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("ICTicket", "fillICTicket", e1);
                throw e1;
            }
        }
        PApiResult GetICTicket(int Excel)
        {
            try
            {
                TraceLogger.Log(DateTime.Now); 
                int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);  
                string Division = "";
                if (ddlDivision.SelectedValue != "0")
                {
                    Division = ddlDivision.SelectedValue;
                }
                return new BDMS_ICTicket().GetOnlineServiceTicket(null,txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), StatusID,   Division, Excel, PageIndex, gvICTickets.PageSize);

            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("ICTicket", "fillICTicket", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillICTicket();
            }

            //if (gvICTickets.PageIndex > 0)
            //{
            //    gvICTickets.DataSource = ICTickets;
            //    gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

            //    gvICTickets.DataBind();
            //    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTickets.Count;
            //}
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillICTicket();
            }
            //if (gvICTickets.PageCount > gvICTickets.PageIndex)
            //{
            //    gvICTickets.DataSource = ICTickets;
            //    gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
            //    gvICTickets.DataBind();
            //    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTickets.Count;
            //}
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();

            //dt.Columns.Add("IC Ticket");
            //dt.Columns.Add("IC Ticket Date");
            //dt.Columns.Add("Dealer Code");
            //dt.Columns.Add("Dealer Name");
            //dt.Columns.Add("Cust. Code");
            //dt.Columns.Add("Cust. Name");
            //dt.Columns.Add("Requested Date");
            //dt.Columns.Add("Model");
            //dt.Columns.Add("Service Type");
            //dt.Columns.Add("Service Priority");
            //dt.Columns.Add("Service Status");
            //dt.Columns.Add("Margin");

            //foreach (PDMS_ICTicket IC in ICTickets)
            //{
            //    dt.Rows.Add(
            //        IC.ICTicketNumber
            //        , IC.ICTicketDate.ToShortDateString()
            //        , IC.Dealer.DealerCode
            //        , IC.Dealer.DealerName
            //        , IC.Customer.CustomerCode
            //        , IC.Customer.CustomerName
            //        , IC.RequestedDate == null ? "" : ((DateTime)IC.RequestedDate).ToShortDateString()
            //        , IC.Equipment.EquipmentModel.Model
            //        , IC.ServiceType == null ? "" : IC.ServiceType.ServiceType
            //        , IC.ServicePriority == null ? "" : IC.ServicePriority.ServicePriority
            //        , IC.ServiceStatus.ServiceStatus
            //        , IC.IsMarginWarranty
            //        );
            //}

            PApiResult Result = GetICTicket(1);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }
        protected void lbICTicket_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            int index = gvRow.RowIndex;
            string url = "DMS_ICTicketView.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }

        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex; 
            divList.Visible = false;
            divDetailsView.Visible = true; 
            UC_ICTicketView.FillOnlineServiceTicket(Convert.ToInt64(gvICTickets.DataKeys[index].Value.ToString())); 
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }
    }
}