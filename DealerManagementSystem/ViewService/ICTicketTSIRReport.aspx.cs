using Business;
using Newtonsoft.Json;
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
    public partial class ICTicketTSIRReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketTSIRReport; } }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » TSIR » Detail Report');</script>");

            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;

                txtTSIRDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtTSIRDateTo.Text = DateTime.Now.ToShortDateString(); 
                fillDealer(); 
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                new BDMS_TypeOfWarranty().GetTypeOfWarrantyDDL(ddlTypeOfWarranty, null, null);
                new BDMS_Model().GetTypeOfWarrantyDDL(ddlModelID, null, null, null);
                //   ICTicketStatus = new BDMS_ICTicketTSIR().GetTSIRStatus(null, null);
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
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);  
                int? TechnicianID = null; 
                if (PSession.User.IsTechnician)
                {
                    TechnicianID = PSession.User.UserID;
                } 
                int? TypeOfWarrantyID = ddlTypeOfWarranty.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTypeOfWarranty.SelectedValue);
                int? ModelID = ddlModelID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModelID.SelectedValue);
                int? TsirStatusID = ddlTsirStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTsirStatus.SelectedValue);
                PApiResult Result =   new BDMS_ICTicketTSIR().GetICTicketTSIRDetailReport(DealerID, txtCustomerCode.Text.Trim(), txtTSIRNo.Text.Trim(), txtTSIRDateFrom.Text.Trim(), txtTSIRDateTo.Text.Trim()
                    , txtICTicketNumber.Text.Trim(), txtICDateF.Text.Trim(), txtICDateT.Text.Trim(), txtSroCode.Text.Trim(), TechnicianID
                    , TypeOfWarrantyID, ModelID, txtMachineSerialNumber.Text.Trim(), TsirStatusID, PageIndex, gvTsir.PageSize);

                //if (ddlDealerCode.SelectedValue == "0")
                //{
                //    var SOIs1 = (from S in SOIs join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName select new { S }).ToList();
                //    SOIs.Clear();
                //    foreach (var w in SOIs1)
                //    {
                //        SOIs.Add(w.S);
                //    }
                //}

                //List<PDMS_Division> Division = new BDMS_ICTicketTSIR().GetICTicketTSIRUserDivisionList(PSession.User.UserID);
                //if (Division.Count != 0)
                //{
                //    var SOIs2 = (from S in SOIs join D in Division on S.ICTicket.Equipment.EquipmentModel.Division.DivisionID equals D.DivisionID select new { S }).ToList();
                //    SOIs.Clear();
                //    foreach (var w in SOIs2)
                //    {
                //        SOIs.Add(w.S);
                //    }
                //}

                //TSIRs = SOIs;
                TSIRs = JsonConvert.DeserializeObject<List<PDMS_ICTicketTSIR>>(JsonConvert.SerializeObject(Result.Data));
                gvTsir.PageIndex = 0;
                gvTsir.DataSource = TSIRs;
                gvTsir.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvTsir.PageSize - 1) / gvTsir.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvTsir.PageSize) + 1) + " - " + (((PageIndex - 1) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + Result.RowCount;
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
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillICTicket();
            }
            //if (gvTsir.PageIndex > 0)
            //{
            //    gvTsir.DataSource = TSIRs;
            //    gvTsir.PageIndex = gvTsir.PageIndex - 1;

            //    gvTsir.DataBind();
            //    lblRowCount.Text = (((gvTsir.PageIndex) * gvTsir.PageSize) + 1) + " - " + (((gvTsir.PageIndex) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + TSIRs.Count;
            //}
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillICTicket();
            }
            //if (gvTsir.PageCount > gvTsir.PageIndex)
            //{
            //    gvTsir.DataSource = TSIRs;
            //    gvTsir.PageIndex = gvTsir.PageIndex + 1;
            //    gvTsir.DataBind();
            //    lblRowCount.Text = (((gvTsir.PageIndex) * gvTsir.PageSize) + 1) + " - " + (((gvTsir.PageIndex) * gvTsir.PageSize) + gvTsir.Rows.Count) + " of " + TSIRs.Count;
            //}
        }

        protected void btnExportExcelTsirPopup_Click(object sender, EventArgs e)
        {
            MPE_TsirApproveDate.Show();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        { 
            int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue); 
            int? TechnicianID = null;
            if (PSession.User.IsTechnician)
            {
                TechnicianID = PSession.User.UserID;
            }
            int? TypeOfWarrantyID = ddlTypeOfWarranty.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTypeOfWarranty.SelectedValue);
            int? ModelID = ddlModelID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModelID.SelectedValue);
            int? TsirStatusID = ddlTsirStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTsirStatus.SelectedValue);
            DataTable dt = new BDMS_ICTicketTSIR().GetICTicketTSIRDetailReportExcel(DealerID, txtCustomerCode.Text.Trim(), txtTSIRNo.Text.Trim(), txtTSIRDateFrom.Text.Trim(), txtTSIRDateTo.Text.Trim()
                , txtICTicketNumber.Text.Trim(), txtICDateF.Text.Trim(), txtICDateT.Text.Trim(), txtSroCode.Text.Trim(), TechnicianID
                , TypeOfWarrantyID, ModelID, txtMachineSerialNumber.Text.Trim(), TsirStatusID,txtTsirApproveDateFrom.Text.Trim(),txtTsirApproveDateTo.Text.Trim());
            try
            {
                new BXcel().ExporttoExcel(dt, "TSIR Details");
            }
            catch
            { }
            MPE_TsirApproveDate.Hide();

            //DataTable dt = new DataTable();
            //dt.Columns.Add("TSIR");
            //dt.Columns.Add("TSIR Date");
            //dt.Columns.Add("FSR");
            //dt.Columns.Add("FSR Date");

            //dt.Columns.Add("Commissioning Date");
            //dt.Columns.Add("M/C Dispatch Date");
            //dt.Columns.Add("Type Of Warranty");

            //dt.Columns.Add("Service Code");
            //dt.Columns.Add("Service Desc");
            //dt.Columns.Add("Count Overall");
            //dt.Columns.Add("Count Based MC");

            //dt.Columns.Add("Nature of Failure");
            //dt.Columns.Add("Failure Details");
            //dt.Columns.Add("Points checked");
            //dt.Columns.Add("Possible Root Causes / Specific Points Noticed");

            //dt.Columns.Add("IC Ticket");
            //dt.Columns.Add("IC Ticket Date");
            //dt.Columns.Add("Cust. Code");
            //dt.Columns.Add("Cust. Name");
            //dt.Columns.Add("Dealer Code");
            //dt.Columns.Add("Dealer Name");

            //dt.Columns.Add("HMR");
            //dt.Columns.Add("Application");
            //dt.Columns.Add("Machine Serial Number");
            //dt.Columns.Add("Machine Model");

            //dt.Columns.Add("State");
            //dt.Columns.Add("District");
            //dt.Columns.Add("Location");
            //dt.Columns.Add("TSIR Status");

            //dt.Columns.Add("Material");
            //dt.Columns.Add("Material Desc");
            //dt.Columns.Add("Delivery Number");
            //dt.Columns.Add("Qty");
            //foreach (PDMS_ICTicketTSIR IC in TSIRs)
            //{
            //    if (IC.SMaterials.Count == 0)
            //    {
            //        dt.Rows.Add(
            //                 IC.TsirNumber
            //                 , IC.TsirDate.ToShortDateString()
            //                 , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
            //                , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
            //               , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
            //                , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
            //                , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
            //                , IC.ServiceCharge.Material.MaterialCode
            //                , IC.ServiceCharge.Material.MaterialDescription
            //                , IC.ServiceCharge.CountOverall
            //                , IC.ServiceCharge.CountBasedMC
            //                , IC.NatureOfFailures
            //                , IC.FailureDetails
            //                , IC.PointsChecked
            //                , IC.PossibleRootCauses
            //               , IC.ICTicket.ICTicketNumber
            //               , IC.ICTicket.ICTicketDate.ToShortDateString()
            //              , IC.ICTicket.Customer.CustomerCode
            //               , IC.ICTicket.Customer.CustomerName
            //               , IC.ICTicket.Dealer.DealerCode
            //               , IC.ICTicket.Dealer.DealerName
            //                , IC.ICTicket.CurrentHMRValue
            //                , IC.ICTicket.MainApplication.MainApplication
            //               , IC.ICTicket.Equipment.EquipmentSerialNo
            //               , IC.ICTicket.Equipment.EquipmentModel.Model
            //              , IC.ICTicket.Address.State.State
            //               , IC.ICTicket.Address.District.District
            //               , IC.ICTicket.Location
            //               , IC.Status.Status
            //               , ""
            //               , ""
            //               , ""
            //               , ""
            //               );
            //    }
            //    else
            //    {
            //        foreach (PDMS_ServiceMaterial SM in IC.SMaterials)
            //        {
            //            dt.Rows.Add(
            //                  IC.TsirNumber
            //                  , IC.TsirDate.ToShortDateString()
            //                  , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
            //                 , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
            //                , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
            //                 , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
            //                 , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
            //                 , IC.ServiceCharge.Material.MaterialCode
            //                 , IC.ServiceCharge.Material.MaterialDescription
            //                  , IC.ServiceCharge.CountOverall
            //                 , IC.ServiceCharge.CountBasedMC
            //                 , IC.NatureOfFailures
            //                 , IC.FailureDetails
            //                 , IC.PointsChecked
            //                 , IC.PossibleRootCauses
            //                , IC.ICTicket.ICTicketNumber
            //                , IC.ICTicket.ICTicketDate.ToShortDateString()
            //               , IC.ICTicket.Customer.CustomerCode
            //                , IC.ICTicket.Customer.CustomerName
            //                , IC.ICTicket.Dealer.DealerCode
            //                , IC.ICTicket.Dealer.DealerName
            //                 , IC.ICTicket.CurrentHMRValue
            //                 , IC.ICTicket.MainApplication.MainApplication
            //                , IC.ICTicket.Equipment.EquipmentSerialNo
            //                , IC.ICTicket.Equipment.EquipmentModel.Model
            //               , IC.ICTicket.Address.State.State
            //                , IC.ICTicket.Address.District.District
            //                , IC.ICTicket.Location
            //                , IC.Status.Status
            //                , SM.Material.MaterialCode
            //                , SM.Material.MaterialDescription
            //                , SM.DeliveryNumber
            //                , SM.Qty
            //                );
            //        }
            //    }
            //}
            //new BXcel().ExporttoExcel(dt, "TSIR Details");
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