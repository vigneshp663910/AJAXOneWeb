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
    public partial class MTTR_New : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_MTTR_New; } }
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
            Session["previousUrl"] = "DMS_MTTR_New.aspx";
            Session["PageName"] = "MTTR New";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
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
                PageCount = 0;
                PageIndex = 1;

                fillStatus();
                new BDMS_Division().GetDivisionForSerchGroped(ddlDivision);
                new BDMS_Dealer().LoadDealerDDL(ddlDealerCode);

                txtICLoginDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtICLoginDateTo.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                fillMTTR();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillMTTR()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                string Division = "";
                if (ddlDivision.SelectedValue != "0")
                {
                    Division = ddlDivision.SelectedValue;
                }
                //   List<PDMS_MTTR_New> MttrsWithText
                PApiResult Result = new BDMS_MTTR().GetMTTR(DealerID, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), txtICLoginDateFrom.Text.Trim(), txtICLoginDateTo.Text.Trim(), StatusID, Division, PageIndex, gvICTickets.PageSize);

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = JsonConvert.DeserializeObject<List<PDMS_MTTR_New>>(JsonConvert.SerializeObject(Result.Data));
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
                    lblRowCount.Text = (((PageIndex - 1) * gvICTickets.PageSize) + 1) + " - " + (((PageIndex - 1) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Result.RowCount;
                }


                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", e1);
                throw e1;
            }
        }
        void fillStatus()
        {
            ddlStatus.DataTextField = "ServiceStatus";
            ddlStatus.DataValueField = "ServiceStatusID";
            ddlStatus.DataSource = new BDMS_Service().GetServiceStatus(null, null);
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillMTTR();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillMTTR();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (ddlExportExcelType.SelectedValue == "1")
            {
                ExportExcelWithoutNote();
            }
            else
            {
                ExportExcelWithNote();
            } 
        }
        protected void btnExportExcel_Click_Old(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Tkt No.");
            dt.Columns.Add("Call Login Dt(IC)");

            dt.Columns.Add("Ser. Req. Dt");
            dt.Columns.Add("SE Reached Dt");
            dt.Columns.Add("SE Restore Dt");

            dt.Columns.Add("Model");
            dt.Columns.Add("Machine Serial No.");
            dt.Columns.Add("Problem Reported");
            dt.Columns.Add("Prior Desc. (Srv. Order)");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Status1 (Op. Based)");
            dt.Columns.Add("Call Open Reason");
            dt.Columns.Add("Call Open Reason Details");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Present M/C City");


            dt.Columns.Add("MTTR1-Act Resp(Days)");
            dt.Columns.Add("MTTR2-Actual Restored(Day)");
            dt.Columns.Add("Status Since (Hour)");

            dt.Columns.Add("Service Type");
            dt.Columns.Add("Dispatched Dt");
            dt.Columns.Add("Commissioning Dt");
            dt.Columns.Add("Warranty Expiry Dt");
            dt.Columns.Add("Is Warranty");



            //  dt.Columns.Add("PSR Status");
            dt.Columns.Add("Cust. ID");

            dt.Columns.Add("Dealer Code");


            dt.Columns.Add("M/C Loc Dist(IC)");
            dt.Columns.Add("M/C Loc State(IC)");
            dt.Columns.Add("M/C Loc Region(IC)");


            dt.Columns.Add("Division");

            dt.Columns.Add("Ser Engg Name");
            dt.Columns.Add("Service charge Code");
            dt.Columns.Add("Service charge des");
            dt.Columns.Add("Description");

            // dt.Columns.Add("Prior Desc. (IC)");

            dt.Columns.Add("Application");
            dt.Columns.Add("Contatc no.");
            dt.Columns.Add("Contact person");
            dt.Columns.Add("FSR No");
            //  dt.Columns.Add("PSR No");
            dt.Columns.Add("TSIR No");

            dt.Columns.Add("Cust Sat Level");
            dt.Columns.Add("Total Mandays");
            dt.Columns.Add("Total Hours");
            dt.Columns.Add("Margin Remark");
            dt.Columns.Add("Scope Of Work");
            dt.Columns.Add("Hilly Region");


            dt.Columns.Add("Decline Reason");

            dt.Columns.Add("Call Login Time(IC)");
            dt.Columns.Add("Ser. Req. Time");
            dt.Columns.Add("SE Reached Time");
            dt.Columns.Add("SE Restore Time");
            dt.Columns.Add("MTTR1-Act Resp(Hour)");
            dt.Columns.Add("MTTR2-Actual Restored(Hour)");
            int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
            string Division = "";
            if (ddlDivision.SelectedValue != "0")
            {
                Division = ddlDivision.SelectedValue;
            }
            int Index = 0;
            int Rowcount = 200;
            int CRowcount = Rowcount;
            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                PApiResult Result = new BDMS_MTTR().GetMTTR(DealerID, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), txtICLoginDateFrom.Text.Trim(), txtICLoginDateTo.Text.Trim(), StatusID, Division, Index, 100);
                List<PDMS_MTTR_New> SDMS_MTTR = JsonConvert.DeserializeObject<List<PDMS_MTTR_New>>(JsonConvert.SerializeObject(Result.Data));
                CRowcount = SDMS_MTTR.Count;
                foreach (PDMS_MTTR_New M in SDMS_MTTR)
                {
                    List<PDMS_ServiceNote> Breakdown = new BDMS_Service().GetServiceNote(Convert.ToInt64(M.ICTicketID), null, null, "");
                    if (Breakdown.Count == 0)
                    {
                        dt.Rows.Add(
                                                     M.ICTicket.ICTicketNumber
                                                   , M.ICTicket.ICTicketDate.ToShortDateString()

                                                   , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortDateString()
                                                   , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortDateString()
                                                   , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortDateString()



                                                   , M.ICTicket.Equipment.EquipmentModel.Model
                                                   , M.ICTicket.Equipment.EquipmentSerialNo
                                                   , M.ICTicket.ComplaintDescription
                                                   , M.ICTicket.ServicePriority.ServicePriority
                                                     , M.ICTicket.CurrentHMRValue
                                                     , M.ICTicket.ServiceStatus.ServiceStatus
                                                       , ""
                                                   , ""
                                                    , M.ICTicket.Customer.CustomerName
                                                    , M.ICTicket.Dealer.DealerName
                                                     , M.ICTicket.Location
                                                   , M.MTTR1
                                                   , M.MTTR2

                                                   , M.StatusSince

                                                   , M.ICTicket.ServiceType.ServiceType
                                                   , M.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)M.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                                                   , M.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)M.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                                                   , M.ICTicket.Equipment.WarrantyExpiryDate == null ? "" : ((DateTime)M.ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString()
                                                   , Convert.ToString(M.ICTicket.IsWarranty)

                                                   , M.ICTicket.Customer.CustomerCode

                                                   , M.ICTicket.Dealer.DealerCode


                                                   , M.ICTicket.Address.District.District
                                                   , M.ICTicket.Address.State.State
                                                   , M.ICTicket.Address.Region.Region


                                                   , M.ICTicket.Equipment.EquipmentModel.Division.DivisionCode

                                                   , M.ICTicket.Technician.ContactName
                                                   , M.code
                                                   , M.ICTicket.Material.MaterialCode
                                                   , M.description1

                                                   // , M.ICTicket.ICPriority == null ? "" : M.ICTicket.ICPriority.ServicePriority

                                                   , M.ICTicket.MainApplication.MainApplication
                                                   , M.ICTicket.PresentContactNumber
                                                   , M.ICTicket.ContactPerson
                                                   , M.ICTicket.FSRNumber
                                                   // ,""
                                                   , ""
                                                   , M.ICTicket.CustomerSatisfactionLevel == null ? "" : M.ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel
                                                   , M.TotalMandays
                                                   , M.TotalHours
                                                   , M.ICTicket.MarginRemark
                                                   , M.ICTicket.ScopeOfWork
                                                   , M.ICTicket.HillyRegion

                                                   , M.ICTicket.ReqDeclinedReason
                                                    , M.ICTicket.ICTicketDate.ToShortTimeString()
                                                   , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortTimeString()
                                                   , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortTimeString()
                                                   , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortTimeString()
                                                     , M.MTTR1H
                                                   , M.MTTR2H
                                                    );
                    }
                    else
                    {
                        foreach (PDMS_ServiceNote B in Breakdown)
                        {
                            dt.Rows.Add(
                                  M.ICTicket.ICTicketNumber
                                , M.ICTicket.ICTicketDate.ToShortDateString()
                                                   , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortDateString()
                                                   , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortDateString()
                                                   , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortDateString()


                                                     , M.ICTicket.Equipment.EquipmentModel.Model
                                                   , M.ICTicket.Equipment.EquipmentSerialNo
                                                   , M.ICTicket.ComplaintDescription
                                                   , M.ICTicket.ServicePriority.ServicePriority
                                                     , M.ICTicket.CurrentHMRValue
                                                     , M.ICTicket.ServiceStatus.ServiceStatus
                                                      , B.NoteType.NoteType
                                , B.Comments
                                                    , M.ICTicket.Customer.CustomerName
                                                    , M.ICTicket.Dealer.DealerName
                                                     , M.ICTicket.Location


                                , M.MTTR1
                                , M.MTTR2
                                , M.StatusSince
                                , M.ICTicket.ServiceType.ServiceType
                                                   , M.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)M.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                                                   , M.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)M.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                                                   , M.ICTicket.Equipment.WarrantyExpiryDate == null ? "" : ((DateTime)M.ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString()
                                 , Convert.ToString(M.ICTicket.IsWarranty)
                                , M.ICTicket.Customer.CustomerCode
                                , M.ICTicket.Dealer.DealerCode
                                , M.ICTicket.Address.District.District
                                , M.ICTicket.Address.State.State
                                , M.ICTicket.Address.Region.Region
                                , M.ICTicket.Equipment.EquipmentModel.Division.DivisionCode
                                , M.ICTicket.Technician.ContactName
                                , M.code
                                , M.ICTicket.Material.MaterialCode
                                , M.description1
                                //, M.ICTicket.ICPriority == null ? "" : M.ICTicket.ICPriority.ServicePriority 
                                , M.ICTicket.MainApplication.MainApplication
                                , M.ICTicket.PresentContactNumber
                                , M.ICTicket.ContactPerson
                                , M.ICTicket.FSRNumber
                                                   // , ""
                                                   , ""
                                , M.ICTicket.CustomerSatisfactionLevel == null ? "" : M.ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel
                                , M.TotalMandays
                                , M.TotalHours
                                , M.ICTicket.MarginRemark
                                , M.ICTicket.ScopeOfWork
                                , M.ICTicket.HillyRegion

                                , M.ICTicket.ReqDeclinedReason
                                 , M.ICTicket.ICTicketDate.ToShortTimeString()
                                                   , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortTimeString()
                                                   , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortTimeString()
                                                   , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortTimeString()
                                                     , M.MTTR1H
                                                   , M.MTTR2H
                                                   );
                        }
                    }
                }
            }
            new BXcel().ExporttoExcel(dt, "MTTR New Report");
        }

        protected void ExportExcelWithNote()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Tkt No.");
            dt.Columns.Add("Call Login Dt(IC)");

            dt.Columns.Add("Ser. Req. Dt");
            dt.Columns.Add("SE Reached Dt");
            dt.Columns.Add("SE Restore Dt");

            dt.Columns.Add("Model");
            dt.Columns.Add("Machine Serial No.");
            dt.Columns.Add("Problem Reported");
            dt.Columns.Add("Prior Desc. (Srv. Order)");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Status1 (Op. Based)");
            dt.Columns.Add("Call Open Reason");
            dt.Columns.Add("Call Open Reason Details");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Present M/C City");


            dt.Columns.Add("MTTR1-Act Resp(Days)");
            dt.Columns.Add("MTTR2-Actual Restored(Day)");
            dt.Columns.Add("Status Since (Hour)");

            dt.Columns.Add("Service Type");
            dt.Columns.Add("Dispatched Dt");
            dt.Columns.Add("Commissioning Dt");
            dt.Columns.Add("Warranty Expiry Dt");
            dt.Columns.Add("Is Warranty");



            //  dt.Columns.Add("PSR Status");
            dt.Columns.Add("Cust. ID");

            dt.Columns.Add("Dealer Code");


            dt.Columns.Add("M/C Loc Dist(IC)");
            dt.Columns.Add("M/C Loc State(IC)");
            dt.Columns.Add("M/C Loc Region(IC)");


            dt.Columns.Add("Division");

            dt.Columns.Add("Ser Engg Name");
            dt.Columns.Add("Work Date");
            dt.Columns.Add("Work Hrs");



            dt.Columns.Add("Service charge Code");
            dt.Columns.Add("Service charge des");
            dt.Columns.Add("Description");

            // dt.Columns.Add("Prior Desc. (IC)");

            dt.Columns.Add("Application");
            dt.Columns.Add("Contatc no.");
            dt.Columns.Add("Contact person");
            dt.Columns.Add("FSR No");
            //  dt.Columns.Add("PSR No");
            dt.Columns.Add("TSIR No");

            dt.Columns.Add("Cust Sat Level");
            dt.Columns.Add("Total Mandays");
            dt.Columns.Add("Total Hours");
            dt.Columns.Add("Margin Remark");
            dt.Columns.Add("Scope Of Work");
            dt.Columns.Add("Hilly Region");


            dt.Columns.Add("Decline Reason");

            dt.Columns.Add("Call Login Time(IC)");
            dt.Columns.Add("Ser. Req. Time");
            dt.Columns.Add("SE Reached Time");
            dt.Columns.Add("SE Restore Time");
            dt.Columns.Add("MTTR1-Act Resp(Hour)");
            dt.Columns.Add("MTTR2-Actual Restored(Hour)");

            string CallOpenReason = "";
            string CallOpenReasonDetails = "";

            string SerEnggName = "";
            DateTime? WorkDate = null;
            Decimal? WorkHrs = null;


            int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
            string Division = "";
            if (ddlDivision.SelectedValue != "0")
            {
                Division = ddlDivision.SelectedValue;
            }
            int Index = 0;
            int Rowcount = 1000;
            int CRowcount = Rowcount;
            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                PApiResult Result = new BDMS_MTTR().GetMTTR(DealerID, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), txtICLoginDateFrom.Text.Trim(), txtICLoginDateTo.Text.Trim(), StatusID, Division, Index, Rowcount);

                List<PDMS_MTTR_New> SDMS_MTTR = JsonConvert.DeserializeObject<List<PDMS_MTTR_New>>(JsonConvert.SerializeObject(Result.Data));
                CRowcount = SDMS_MTTR.Count;
                foreach (PDMS_MTTR_New M in SDMS_MTTR)
                {
                    List<PDMS_ServiceNote> Breakdown = new BDMS_Service().GetServiceNote(M.ICTicketID, null, null, "");
                    List<Technician> Technicians = fillTechnician(M.ICTicketID);

                    int totalC = Breakdown.Count > Technicians.Count ? Breakdown.Count : Technicians.Count;
                    int i = 0;

                    do
                    {
                        CallOpenReason = "";
                        CallOpenReasonDetails = "";

                        SerEnggName = "";
                        WorkDate = null;
                        WorkHrs = null;
                        if (i < Breakdown.Count)
                        {
                            CallOpenReason = Breakdown[i].NoteType.NoteType;
                            CallOpenReasonDetails = Breakdown[i].Comments;
                        }

                        if (i < Technicians.Count)
                        {
                            SerEnggName = Technicians[i].Name;
                            WorkDate = Technicians[i].WorkedDate;
                            WorkHrs = Technicians[i].WorkedHours;
                        }


                        dt.Rows.Add(
                                                     M.ICTicket.ICTicketNumber
                                                   , M.ICTicket.ICTicketDate.ToShortDateString()

                                                   , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortDateString()
                                                   , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortDateString()
                                                   , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortDateString()



                                                   , M.ICTicket.Equipment.EquipmentModel.Model
                                                   , M.ICTicket.Equipment.EquipmentSerialNo
                                                   , M.ICTicket.ComplaintDescription
                                                   , M.ICTicket.ServicePriority.ServicePriority
                                                     , M.ICTicket.CurrentHMRValue
                                                     , M.ICTicket.ServiceStatus.ServiceStatus
                                                       , CallOpenReason
                                                   , CallOpenReasonDetails
                                                    , M.ICTicket.Customer.CustomerName
                                                    , M.ICTicket.Dealer.DealerName
                                                     , M.ICTicket.Location
                                                   , M.MTTR1
                                                   , M.MTTR2

                                                   , M.StatusSince

                                                   , M.ICTicket.ServiceType.ServiceType
                                                   , M.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)M.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                                                   , M.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)M.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                                                   , M.ICTicket.Equipment.WarrantyExpiryDate == null ? "" : ((DateTime)M.ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString()
                                                   , Convert.ToString(M.ICTicket.IsWarranty)

                                                   , M.ICTicket.Customer.CustomerCode

                                                   , M.ICTicket.Dealer.DealerCode


                                                   , M.ICTicket.Address.District.District
                                                   , M.ICTicket.Address.State.State
                                                   , M.ICTicket.Address.Region.Region


                                                   , M.ICTicket.Equipment.EquipmentModel.Division.DivisionCode

                                                   , SerEnggName
                                                   , WorkDate
                                                   , WorkHrs
                                                   , M.code
                                                   , M.ICTicket.Material.MaterialCode
                                                   , M.description1

                                                   // , M.ICTicket.ICPriority == null ? "" : M.ICTicket.ICPriority.ServicePriority

                                                   , M.ICTicket.MainApplication.MainApplication
                                                   , M.ICTicket.PresentContactNumber
                                                   , M.ICTicket.ContactPerson
                                                   , M.ICTicket.FSRNumber
                                                   // ,""
                                                   , ""
                                                   , M.ICTicket.CustomerSatisfactionLevel == null ? "" : M.ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel
                                                   , M.TotalMandays
                                                   , M.TotalHours
                                                   , M.ICTicket.MarginRemark
                                                   , M.ICTicket.ScopeOfWork
                                                   , M.ICTicket.HillyRegion

                                                   , M.ICTicket.ReqDeclinedReason
                                                    , M.ICTicket.ICTicketDate.ToShortTimeString()
                                                   , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortTimeString()
                                                   , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortTimeString()
                                                   , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortTimeString()
                                                     , M.MTTR1H
                                                   , M.MTTR2H
                                                    );

                        i++;
                    } while (totalC > i);

                }
            }
            new BXcel().ExporttoExcel(dt, "MTTR New Report");
        }
        protected void ExportExcelWithoutNote()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Tkt No.");
            dt.Columns.Add("Call Login Dt(IC)");

            dt.Columns.Add("Ser. Req. Dt");
            dt.Columns.Add("SE Reached Dt");
            dt.Columns.Add("SE Restore Dt");

            dt.Columns.Add("Model");
            dt.Columns.Add("Machine Serial No.");
            dt.Columns.Add("Problem Reported");
            dt.Columns.Add("Prior Desc. (Srv. Order)");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Status1 (Op. Based)");
            //dt.Columns.Add("Call Open Reason");
            //dt.Columns.Add("Call Open Reason Details");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Present M/C City");


            dt.Columns.Add("MTTR1-Act Resp(Days)");
            dt.Columns.Add("MTTR2-Actual Restored(Day)");
            dt.Columns.Add("Status Since (Hour)");

            dt.Columns.Add("Service Type");
            dt.Columns.Add("Dispatched Dt");
            dt.Columns.Add("Commissioning Dt");
            dt.Columns.Add("Warranty Expiry Dt");
            dt.Columns.Add("Is Warranty");


            dt.Columns.Add("Cust. ID");

            dt.Columns.Add("Dealer Code");


            dt.Columns.Add("M/C Loc Dist(IC)");
            dt.Columns.Add("M/C Loc State(IC)");
            dt.Columns.Add("M/C Loc Region(IC)");


            dt.Columns.Add("Division");

            //dt.Columns.Add("Ser Engg Name");
            //dt.Columns.Add("Work Date");
            //dt.Columns.Add("Work Hrs");



            dt.Columns.Add("Service charge Code");
            dt.Columns.Add("Service charge des");
            dt.Columns.Add("Description");

            dt.Columns.Add("Application");
            dt.Columns.Add("Contatc no.");
            dt.Columns.Add("Contact person");
            dt.Columns.Add("FSR No");
            dt.Columns.Add("TSIR No");

            dt.Columns.Add("Cust Sat Level");
            dt.Columns.Add("Total Mandays");
            dt.Columns.Add("Total Hours");
            dt.Columns.Add("Margin Remark");
            dt.Columns.Add("Scope Of Work");
            dt.Columns.Add("Hilly Region");

            dt.Columns.Add("Decline Reason");

            dt.Columns.Add("Call Login Time(IC)");
            dt.Columns.Add("Ser. Req. Time");
            dt.Columns.Add("SE Reached Time");
            dt.Columns.Add("SE Restore Time");
            dt.Columns.Add("MTTR1-Act Resp(Hour)");
            dt.Columns.Add("MTTR2-Actual Restored(Hour)");

            int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
            string Division = "";
            if (ddlDivision.SelectedValue != "0")
            {
                Division = ddlDivision.SelectedValue;
            }
            int Index = 0;
            int Rowcount = 1000;
            int CRowcount = Rowcount;
            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                PApiResult Result = new BDMS_MTTR().GetMTTR(DealerID, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), txtICLoginDateFrom.Text.Trim(), txtICLoginDateTo.Text.Trim(), StatusID, Division, Index, Rowcount);

                List<PDMS_MTTR_New> SDMS_MTTR = JsonConvert.DeserializeObject<List<PDMS_MTTR_New>>(JsonConvert.SerializeObject(Result.Data));
                CRowcount = SDMS_MTTR.Count;
                foreach (PDMS_MTTR_New M in SDMS_MTTR)
                {

                    dt.Rows.Add(
                            M.ICTicket.ICTicketNumber
                        , M.ICTicket.ICTicketDate.ToShortDateString()

                        , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortDateString()
                        , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortDateString()
                        , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortDateString()

                        , M.ICTicket.Equipment.EquipmentModel.Model
                        , M.ICTicket.Equipment.EquipmentSerialNo
                        , M.ICTicket.ComplaintDescription
                        , M.ICTicket.ServicePriority.ServicePriority
                            , M.ICTicket.CurrentHMRValue
                            , M.ICTicket.ServiceStatus.ServiceStatus
                        //    , CallOpenReason
                        //, CallOpenReasonDetails
                        , M.ICTicket.Customer.CustomerName
                        , M.ICTicket.Dealer.DealerName
                            , M.ICTicket.Location
                        , M.MTTR1
                        , M.MTTR2

                        , M.StatusSince

                        , M.ICTicket.ServiceType.ServiceType
                        , M.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)M.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                        , M.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)M.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                        , M.ICTicket.Equipment.WarrantyExpiryDate == null ? "" : ((DateTime)M.ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString()
                        , Convert.ToString(M.ICTicket.IsWarranty)

                        , M.ICTicket.Customer.CustomerCode
                        , M.ICTicket.Dealer.DealerCode
                        , M.ICTicket.Address.District.District
                        , M.ICTicket.Address.State.State
                        , M.ICTicket.Address.Region.Region
                        , M.ICTicket.Equipment.EquipmentModel.Division.DivisionCode

                        //, SerEnggName
                        //, WorkDate
                        //, WorkHrs
                        , M.code
                        , M.ICTicket.Material.MaterialCode
                        , M.description1
                        , M.ICTicket.MainApplication.MainApplication
                        , M.ICTicket.PresentContactNumber
                        , M.ICTicket.ContactPerson
                        , M.ICTicket.FSRNumber
                        , ""
                        , M.ICTicket.CustomerSatisfactionLevel == null ? "" : M.ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel
                        , M.TotalMandays
                        , M.TotalHours
                        , M.ICTicket.MarginRemark
                        , M.ICTicket.ScopeOfWork
                        , M.ICTicket.HillyRegion

                        , M.ICTicket.ReqDeclinedReason
                        , M.ICTicket.ICTicketDate.ToShortTimeString()
                        , M.ICTicket.RequestedDate == null ? "" : ((DateTime)M.ICTicket.RequestedDate).ToShortTimeString()
                        , M.ICTicket.ReachedDate == null ? "" : ((DateTime)M.ICTicket.ReachedDate).ToShortTimeString()
                        , M.ICTicket.RestoreDate == null ? "" : ((DateTime)M.ICTicket.RestoreDate).ToShortTimeString()
                            , M.MTTR1H
                        , M.MTTR2H
                        );
                }
            }
            new BXcel().ExporttoExcel(dt, "MTTR New Report");
        }

        protected void gvICTicketsWithText_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string f_ic_ticket_id = Convert.ToString(gvICTickets.DataKeys[e.Row.RowIndex].Value);
                    Boolean cbIsNew = ((CheckBox)e.Row.FindControl("cbIsNew")).Checked;
                    GridView gvBreakdown = (GridView)e.Row.FindControl("gvBreakdown");

                    long ICTicketID = Convert.ToInt64(f_ic_ticket_id);
                    if (cbIsNew)
                    {
                        gvBreakdown.DataSource = new BDMS_Service().GetServiceNote(ICTicketID, null, null, "");
                        gvBreakdown.DataBind();
                    }
                    else
                    {
                        gvBreakdown.DataSource = new BDMS_MTTR().GetMTTRNote(ICTicketID, null, null, "");
                        gvBreakdown.DataBind();
                    }
                    GridView gvTechnician = (GridView)e.Row.FindControl("gvTechnician");
                    gvTechnician.DataSource = fillTechnician(ICTicketID);
                    gvTechnician.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", ex);
                throw ex;
            }
        }
        List<Technician> fillTechnician(long ICTicketID)
        {
            List<Technician> tech = new List<Technician>();
            List<PDMS_ServiceTechnician> Technicians = new BDMS_Service().GetTechniciansByTicketID(ICTicketID);
            foreach (PDMS_ServiceTechnician t in Technicians)
            {
                if (t.ServiceTechnicianWorkedDate != null)
                {
                    foreach (PDMS_ServiceTechnicianWorkedDate tw in t.ServiceTechnicianWorkedDate)
                    {
                        tech.Add(new Technician() { Name = t.ContactName, WorkedDate = tw.WorkedDate, WorkedHours = tw.WorkedHours });
                    }
                }
                else
                {
                    tech.Add(new Technician() { Name = t.ContactName });
                }
            }
            return tech;
        }
    }
    partial class Technician
    {
        public String Name { get; set; }
        public DateTime? WorkedDate { get; set; }
        public Decimal? WorkedHours { get; set; }
    }
}