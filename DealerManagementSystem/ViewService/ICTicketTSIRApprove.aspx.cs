using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketTSIRApprove : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketTSIRApprove; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ICTicketTSIRApprove.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicketTSIR> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketTSIRApprove"] == null)
                {
                    Session["DMS_ICTicketTSIRApprove"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["DMS_ICTicketTSIRApprove"];
            }
            set
            {
                Session["DMS_ICTicketTSIRApprove"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » TSIR » Sales Approval');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtTSIRDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtTSIRDateTo.Text = DateTime.Now.ToShortDateString();

                 
                    fillDealer(); 

                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

                new BDMS_Model().GetTypeOfWarrantyDDL(ddlModelID, null, null, null); 
                 
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
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());

                DateTime? TSIRDateF = string.IsNullOrEmpty(txtTSIRDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateFrom.Text.Trim());
                DateTime? TSIRDateT = string.IsNullOrEmpty(txtTSIRDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateTo.Text.Trim());
                 
                int TsirStatusID = 0;


                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirCheck).Count() == 1)
                {
                    TsirStatusID = 1;
                }
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirApprove).Count() == 1)
                {
                    TsirStatusID = 2;
                }
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirSalesApproveL1).Count() == 1)
                {
                    TsirStatusID = 5;
                }
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirSalesApproveL2).Count() == 1)
                {
                    TsirStatusID = 7;
                } 

                PApiResult result = new BDMS_ICTicketTSIR().GetICTicketTSIRForApprove(DealerID, txtCustomerCode.Text.Trim(), txtTSIRNo.Text.Trim(), TSIRDateF, TSIRDateT
                   , txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, TsirStatusID, null);

                if (result.Status == "Failed")
                {
                    lblMessage.Text = result.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ICTicket = JsonConvert.DeserializeObject<List<PDMS_ICTicketTSIR>>(JsonConvert.SerializeObject(result.Data));
                 
                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = ICTicket;
                gvICTickets.DataBind();
                if (ICTicket.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
                }
                 
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRManage", "fillClaim", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSIR");
            dt.Columns.Add("TSIR Date");
            dt.Columns.Add("FSR");
            dt.Columns.Add("FSR Date");

            dt.Columns.Add("Commissioning Date");
            dt.Columns.Add("M/C Dispatch Date");
            dt.Columns.Add("Type Of Warranty");

            dt.Columns.Add("Nature of Failure");
            dt.Columns.Add("Failure Details");
            dt.Columns.Add("Points checked");
            dt.Columns.Add("Possible Root Causes / Specific Points Noticed");

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");

            dt.Columns.Add("HMR");
            dt.Columns.Add("Application");
            dt.Columns.Add("Machine Serial Number");
            dt.Columns.Add("Machine Model");

            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("Location");
            dt.Columns.Add("TSIR Status");
            foreach (PDMS_ICTicketTSIR IC in ICTicket)
            {
                dt.Rows.Add(
                      IC.TsirNumber
                      , IC.TsirDate.ToShortDateString()
                      , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
                     , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
                    , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                     , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                     , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
                     , IC.NatureOfFailures
                     , IC.FailureDetails
                     , IC.PointsChecked
                     , IC.PossibleRootCauses
                    , IC.ICTicket.ICTicketNumber
                    , IC.ICTicket.ICTicketDate.ToShortDateString()
                   , IC.ICTicket.Customer.CustomerCode
                    , IC.ICTicket.Customer.CustomerName
                    , IC.ICTicket.Dealer.DealerCode
                    , IC.ICTicket.Dealer.DealerName
                     , IC.ICTicket.CurrentHMRValue
                     , IC.ICTicket.MainApplication.MainApplication
                    , IC.ICTicket.Equipment.EquipmentSerialNo
                    , IC.ICTicket.Equipment.EquipmentModel.Model
                   , IC.ICTicket.Address.State.State
                    , IC.ICTicket.Address.District.District
                    , IC.ICTicket.Location
                    , IC.Status.Status
                    );
            }
            new BXcel().ExporttoExcel(dt, "TSIR Details");
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

       protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = ICTicket;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            divTSIRView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblTsirID = (Label)gvRow.FindControl("lblTsirID");
            UC_TSIRView.FillTsir(Convert.ToInt64(gvICTickets.DataKeys[gvRow.RowIndex].Values[0].ToString()));

        }
      
        protected void btnBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divTSIRView.Visible = false;
            btnBackToList.Visible = false;
        }
    }
}