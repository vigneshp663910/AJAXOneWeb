using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketTSIRMessage : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketTSIRMessage; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ICTicketTSIRReport.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicketTSIR> TSIRs
        {
            get
            {
                if (Session["DMS_ICTicketTSIRReport"] == null)
                {
                    Session["DMS_ICTicketTSIRReport"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["DMS_ICTicketTSIRReport"];
            }
            set
            {
                Session["DMS_ICTicketTSIRReport"] = value;
            }
        }

        //public List<PDMS_ICTicketTSIRStatus> ICTicketStatus
        //{
        //    get
        //    {
        //        if (Session["DMS_ICTicketStatus"] == null)
        //        {
        //            Session["DMS_ICTicketStatus"] = new List<PDMS_ICTicketTSIRStatus>();
        //        }
        //        return (List<PDMS_ICTicketTSIRStatus>)Session["DMS_ICTicketStatus"];
        //    }
        //    set
        //    {
        //        Session["DMS_ICTicketStatus"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » TSIR » Message');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtTSIRDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtTSIRDateTo.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                new BDMS_ICTicketTSIR().GetTSIRStatusDDL(ddlTsirStatus, null, null);
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
                List<PDMS_ICTicketTSIR> SOIs = null;

                int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());

                DateTime? TSIRDateF = string.IsNullOrEmpty(txtTSIRDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateFrom.Text.Trim());
                DateTime? TSIRDateT = string.IsNullOrEmpty(txtTSIRDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateTo.Text.Trim());

                DateTime? TSIRMDateF = string.IsNullOrEmpty(txtMessageFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtMessageFrom.Text.Trim());
                DateTime? TSIRMDateT = string.IsNullOrEmpty(txtMessageTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtMessageTo.Text.Trim());

                int? TsirStatusID = ddlTsirStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTsirStatus.SelectedValue);

                SOIs = new BDMS_ICTicketTSIR().GetICTicketTSIRMessage(DealerCode, txtCustomerCode.Text.Trim(), txtTSIRNo.Text.Trim(), TSIRDateF, TSIRDateT
                    , txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, TSIRMDateF, TSIRMDateT, txtMachineSerialNumber.Text.Trim(), TsirStatusID);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName select new { S }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }


                TSIRs = SOIs;

                gvTsir.PageIndex = 0;
                gvTsir.DataSource = SOIs;
                gvTsir.DataBind();
                if (SOIs.Count == 0)
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
                    lblRowCount.Text = (((gvTsir.PageIndex) * gvTsir.PageSize) + 1) + " - " + (((gvTsir.PageIndex) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + SOIs.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRReport", "fillICTicket", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvTsir.PageIndex > 0)
            {
                gvTsir.DataSource = TSIRs;
                gvTsir.PageIndex = gvTsir.PageIndex - 1;

                gvTsir.DataBind();
                lblRowCount.Text = (((gvTsir.PageIndex) * gvTsir.PageSize) + 1) + " - " + (((gvTsir.PageIndex) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + TSIRs.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvTsir.PageCount > gvTsir.PageIndex)
            {
                gvTsir.DataSource = TSIRs;
                gvTsir.PageIndex = gvTsir.PageIndex + 1;
                gvTsir.DataBind();
                lblRowCount.Text = (((gvTsir.PageIndex) * gvTsir.PageSize) + 1) + " - " + (((gvTsir.PageIndex) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + TSIRs.Count;
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

            dt.Columns.Add("Service Code");
            dt.Columns.Add("Service Desc");
            dt.Columns.Add("Count Overall");
            dt.Columns.Add("Count Based MC");

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

            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Delivery Number");
            dt.Columns.Add("Qty");
            foreach (PDMS_ICTicketTSIR IC in TSIRs)
            {
                if (IC.SMaterials.Count == 0)
                {
                    dt.Rows.Add(
                             IC.TsirNumber
                             , IC.TsirDate.ToShortDateString()
                             , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
                            , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
                           , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                            , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                            , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
                            , IC.ServiceCharge.Material.MaterialCode
                            , IC.ServiceCharge.Material.MaterialDescription
                            , IC.ServiceCharge.CountOverall
                            , IC.ServiceCharge.CountBasedMC
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
                           , ""
                           , ""
                           , ""
                           , ""
                           );
                }
                else
                {
                    foreach (PDMS_ServiceMaterial SM in IC.SMaterials)
                    {
                        dt.Rows.Add(
                              IC.TsirNumber
                              , IC.TsirDate.ToShortDateString()
                              , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
                             , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
                            , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                             , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                             , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
                             , IC.ServiceCharge.Material.MaterialCode
                             , IC.ServiceCharge.Material.MaterialDescription
                              , IC.ServiceCharge.CountOverall
                             , IC.ServiceCharge.CountBasedMC
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
                            , SM.Material.MaterialCode
                            , SM.Material.MaterialDescription
                            , SM.DeliveryNumber
                            , SM.Qty
                            );
                    }
                }
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

        protected void gvTsir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    long TsirID = Convert.ToInt64(gvTsir.DataKeys[e.Row.RowIndex].Value);
                    List<PDMS_ServiceMaterial> ServiceMaterial = new List<PDMS_ServiceMaterial>();
                    ServiceMaterial = TSIRs.Find(s => s.TsirID == TsirID).SMaterials;

                    GridView gvTsirItems = (GridView)e.Row.FindControl("gvTsirItems");
                    gvTsirItems.DataSource = ServiceMaterial;
                    gvTsirItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRReport", "gvTsir_RowDataBound", ex);
                throw ex;
            }
        }

        protected void gvTsir_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTsir.PageIndex = e.NewPageIndex;
            gvTsir.DataSource = TSIRs;
            gvTsir.DataBind();
            lblRowCount.Text = (((gvTsir.PageIndex) * gvTsir.PageSize) + 1) + " - " + (((gvTsir.PageIndex) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + TSIRs.Count;

        }
    }
}