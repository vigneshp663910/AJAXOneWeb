using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerEmployeeApproval : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_DealerEmployeeApproval; } }
        public List<PDMS_DealerEmployee> ICTicket
        {
            get
            {
                if (Session["DMS_DealerEmployeeApproval"] == null)
                {
                    Session["DMS_DealerEmployeeApproval"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["DMS_DealerEmployeeApproval"];
            }
            set
            {
                Session["DMS_DealerEmployeeApproval"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DealerEmployeeApproval.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Approve');</script>");

            if (!IsPostBack)
            {

                FillDealerEmployee();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerEmployee();
        }
        private void FillDealerEmployee()
        {
            ICTicket = new BDMS_Dealer().GetDealerEmployeeForApproval(null);
            gvDealerEmployee.DataSource = ICTicket;
            gvDealerEmployee.DataBind();
            lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;

        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.DataSource = ICTicket;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;

                gvDealerEmployee.DataBind();
                lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.DataSource = ICTicket;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                gvDealerEmployee.DataBind();
                lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Requested Date");
            dt.Columns.Add("Model");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Service Priority");
            dt.Columns.Add("Service Status");
            dt.Columns.Add("Margin Warranty");

            //foreach (PDMS_ICTicketFSR IC in ICTicket)
            //{
            //    dt.Rows.Add(
            //        IC.ICTicket.ICTicketNumber
            //        , IC.ICTicket.ICTicketDate.ToShortDateString()
            //        , IC.ICTicket.Dealer.DealerCode
            //        , IC.ICTicket.Dealer.DealerName
            //        , IC.ICTicket.Customer.CustomerCode
            //        , IC.ICTicket.Customer.CustomerName
            //        , IC.ICTicket.RequestedDate == null ? "" : ((DateTime)IC.ICTicket.RequestedDate).ToShortDateString()
            //        , IC.ICTicket.Equipment.EquipmentModel
            //        , IC.ICTicket.ServiceType == null ? "" : IC.ICTicket.ServiceType.ServiceType
            //        , IC.ICTicket.ServicePriority == null ? "" : IC.ICTicket.ServicePriority.ServicePriority
            //        , IC.ICTicket.ServiceStatus.ServiceStatus
            //        , IC.ICTicket.IsMarginWarranty
            //        );
            //}



            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }

        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Session["DealerEmployeeApproval"] = gvDealerEmployee.DataKeys[index].Value.ToString();
            string url = "DealerEmployeeCreate.aspx";
            Response.Redirect(url);
        }
        protected void lbApproval_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            if (new BDMS_Dealer().ApproveDealerEmployee(Convert.ToInt32(gvDealerEmployee.DataKeys[index].Value), PSession.User.UserID))
            {
                lblMessage.Text = "Dealer Employee approved successfully";
                lblMessage.ForeColor = Color.Green;
                FillDealerEmployee();
            }
            else
            {
                lblMessage.Text = "Dealer Employee is not approved successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.DataSource = ICTicket;
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            gvDealerEmployee.DataBind();
            lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + ICTicket.Count;

        }
    }
}