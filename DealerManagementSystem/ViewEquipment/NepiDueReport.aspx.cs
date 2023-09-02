using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewEquipment
{
    public partial class NepiDueReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewEquipment_NepiDueReport; } }
        public DataTable Equipment
        {
            get
            {
                if (Session["NepiDueReport"] == null)
                {
                    Session["NepiDueReport"] = new DataTable();
                }
                return (DataTable)Session["NepiDueReport"];
            }
            set
            {
                Session["NepiDueReport"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        { 
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new BDMS_Address().GetStateDDL(ddlState, null, null, null, null);
                // txtWarrantyStart.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                // txtWarrantyEnd.Text = DateTime.Now.ToShortDateString(); 
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }

        void fillNepiDueReport()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
                int? RegionID = null;

                int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                PApiResult Result = new BDMS_Equipment().GetNepiDueReport(DealerID, txtEquipment.Text.Trim(), txtCustomer.Text.Trim(), RegionID, StateID, Convert.ToInt32(ddlOverDueID.SelectedValue));

                Equipment = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));

                gvEquipment.PageIndex = 0;
                gvEquipment.DataSource = Equipment;
                gvEquipment.DataBind();
                if (Equipment.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("NepiDueReport", "fillNepiDueReport", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageIndex > 0)
            {
                gvEquipment.DataSource = Equipment;
                gvEquipment.PageIndex = gvEquipment.PageIndex - 1;

                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageCount > gvEquipment.PageIndex)
            {
                gvEquipment.DataSource = Equipment;
                gvEquipment.PageIndex = gvEquipment.PageIndex + 1;
                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(Equipment, "Over Due Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipment.DataSource = Equipment;
            gvEquipment.PageIndex = e.NewPageIndex;
            gvEquipment.DataBind();
            lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillNepiDueReport();
        }
    }
}