using Business;
using System;
using Properties;
using System.Web.UI.WebControls;
using System.Web.UI;
//using DealerManagementSystem.Dashboard;
using System.Collections.Generic;
using System.Drawing;
using DealerManagementSystem.ViewDashboard;

namespace DealerManagementSystem
{
    public partial class Home : BasePage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Welcome');</script>");

            if (!IsPostBack)
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "ContactName", "DID", true,"All Dealer");
                new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID",true, "All Country"); 
                new DDLBind(ddlZone, new BDMS_Address().GetRegion(null, null,null), "Region", "RegionID", true, "All Zone");
                //new DDLBind(ddlEngineer, new BDMS_Customer().GetCustomerTitle(null, null), "Title", "TitleID", true, "All Engineer");

                //new DDLBind(ddlDivision, new BDMS_Customer().GetCustomerTitle(null, null), "Title", "TitleID", true, "All Product");
                //new DDLBind(ddlModel, new BDMS_Service().Get(null, null), "Title", "TitleID", true, "All Model");
                new DDLBind(ddlApplication, new BDMS_Service().GetMainApplication(null, null), "MainApplication", "MainApplicationID", true, "All Application");

                FillDivision();
                FillModel();
                FillFiscalYear();

                txtDateFrom.Text = "01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                txtDateTo.Text = DateTime.Now.ToShortDateString();
                if (!string.IsNullOrEmpty(Request.QueryString["Session_End"]))
                {
                    lblMessage.Text = Convert.ToString(Request.QueryString["Session_End"]);
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                } 
                List<PDMS_Dashboard> Dashboards = new BDMS_Dashboard().GetDashboardByUserID(PSession.User.UserID);
                if (Dashboards.Count == 0)
                {
                    pnlFilter.Visible = false;
                }
                
            }
            DisplayDashboard();

        }
        void FillDivision()
        {
            ddlDivision.Items.Insert(0, new ListItem("All Product", "0"));
            ddlDivision.Items.Insert(1, new ListItem("Batching Plant", "1"));
            ddlDivision.Items.Insert(2, new ListItem("Concrete Mixer", "2"));
            ddlDivision.Items.Insert(3, new ListItem("Concrete Pump", "3"));
            ddlDivision.Items.Insert(4, new ListItem("Dumper", "4"));
            ddlDivision.Items.Insert(5, new ListItem("Transit Mixer", "11")); 
        }

        void FillModel()
        {
            ddlModel.Items.Insert(0, new ListItem("All Model", "0"));
            ddlModel.Items.Insert(1, new ListItem("ARGO 1000", "1"));
            ddlModel.Items.Insert(2, new ListItem("ARGO 2000", "2"));
            ddlModel.Items.Insert(3, new ListItem("ARGO 2500", "3"));
            ddlModel.Items.Insert(4, new ListItem("ARGO 4000", "4"));
            ddlModel.Items.Insert(5, new ListItem("ARGO 4500", "5"));
            ddlModel.Items.Insert(6, new ListItem("AMBISON", "9"));
            ddlModel.Items.Insert(7, new ListItem("ASP", "10"));
            ddlModel.Items.Insert(8, new ListItem("CBP", "13"));
            ddlModel.Items.Insert(9, new ListItem("BOOM PUMP", "19"));
            ddlModel.Items.Insert(5, new ListItem("IRB", "29"));
        }

        void FillFiscalYear()
        {
            int i = 1;
            ddlFiscalYear.Items.Insert(0, new ListItem("Fiscal Year", "0"));
            for(int Year=2018; Year <= DateTime.Now.Year; Year++)
            {
                ddlFiscalYear.Items.Insert(i, new ListItem(Year.ToString(), Year.ToString()));
                i = i + 1;
            } 
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
             
        }
        private void DisplayDashboard()
        {

            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

            Session["SerDealerID"] = DealerID;
            Session["SerDateFrom"] = DateFrom;
            Session["SerDateTo"] = DateTo;
            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            }
             
            DashboardFactory dashboardFactory = new DashboardFactory(this);
            if (dashboardFactory.accessibleUserControls.Count > 0)
            {
                tblDashboard.Visible = true;
            }
            int seq = 1;
            foreach (UserControl uc in dashboardFactory.accessibleUserControls)
            {

                PlaceHolder phDashboard = (PlaceHolder)tblDashboard.FindControl("ph_usercontrols_" + seq++.ToString());
                if (phDashboard != null)
                {
                    phDashboard.Controls.Add(uc);
                }
            }
        }
    }
    class DashboardFactory
    {
        private BasePage _page;
        private PUser _user;

        private Int32 _plantId;
        private bool _isVendorMode;
        private Int32 _vendorId;
        private DateTime _fromDate;
        private DateTime _toDate;
        private List<UserControl> _userControls = new List<UserControl>();

        public List<UserControl> accessibleUserControls
        {
            get { return _userControls; }
        }
        public DashboardFactory(BasePage page)
        {
            _page = page;
            _user = new PUser();
            _user = PSession.User;

            DateTime baseDate = DateTime.Today;
            _fromDate = baseDate.AddDays(1 - baseDate.Day);
            _toDate = _fromDate.AddMonths(1).AddSeconds(-1);
            //_fromDate = _fromDate.AddMonths(-30);


            //Creating all the dashboards
            _userControls = GetDashboardUserControlsForUser((UserTypes)_user.UserTypeID);
        }
        private List<UserControl> GetDashboardUserControlsForUser(UserTypes currentUserType)
        {
            try
            {
                List<DashboardControl> lstUserDashboardControls = GetDashboardControlListForUser(currentUserType);
                List<DashboardControl> lstConfiguredDashboardControls = GetDashboardControlConfigured();
                List<UserControl> lstUserControls = GetUserControlsForUserInConfiguredDashboardControls(lstUserDashboardControls, lstConfiguredDashboardControls);
                ReOrderUserControlsBySize(lstUserControls);
                return lstUserControls;
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError("GetAccessibleUserControls", ex);
                throw ex;
            }
        }
        private List<DashboardControl> GetDashboardControlListForUser(UserTypes currentUserType)
        {
            List<DashboardControl> lstUserDashboardControls = new List<DashboardControl>();
            //donot load any dashboard for admin

            List<PDMS_Dashboard> Dashboards = new BDMS_Dashboard().GetDashboardByUserID(PSession.User.UserID);
            foreach (PDMS_Dashboard D in Dashboards)
            {
                lstUserDashboardControls.Add((DashboardControl)D.DashboardID);
            }

            //lstUserDashboardControls.Add(DashboardControl.CustomerSatisfactionInAfterSalesSupport);
            //lstUserDashboardControls.Add(DashboardControl.WarrantyMaterialAnalysis);
            //lstUserDashboardControls.Add(DashboardControl.ICTicketEscalationOnBreakdownLevel1);
            //lstUserDashboardControls.Add(DashboardControl.ICTicketEscalationOnBreakdownLevel2);
            //lstUserDashboardControls.Add(DashboardControl.ICTicketEscalationOnBreakdownLevel3);
            //lstUserDashboardControls.Add(DashboardControl.ICTicketEscalationOnBreakdownLevel4);



            //switch (currentUserType)
            //{
            //    case UserTypes.Dealer:
            //        lstUserDashboardControls.Add(DashboardControl.Adherence);
            //lstUserDashboardControls.Add(DashboardControl.Rejections);
            //lstUserDashboardControls.Add(DashboardControl.Deliverables);
            //lstUserDashboardControls.Add(DashboardControl.Payments);
            //lstUserDashboardControls.Add(DashboardControl.MonthlyAdherence);
            //lstUserDashboardControls.Add(DashboardControl.Circulars);
            //        break; 
            //}
            return lstUserDashboardControls;
        }
        private List<DashboardControl> GetDashboardControlConfigured()
        {
            List<DashboardControl> lstConfiguredDashboardControls = new List<DashboardControl>();
            //string displayDashboardValue = applicationSettings.Find(a => (a.AppSettingsID == (short)VPSApplicationSettings.DisplayDashboard)).SettingsValue;
            //foreach (string displayDashboard in displayDashboardValue.Split(new char[] { ',' }))
            //{
            //    Int16 dashboardNumber;
            //    if (Int16.TryParse(displayDashboard, out dashboardNumber))
            //    {
            //        DashboardControl dashboardControl = (DashboardControl)dashboardNumber;
            //        if (!lstConfiguredDashboardControls.Contains(dashboardControl))
            //            lstConfiguredDashboardControls.Add(dashboardControl);
            //    }
            //}

            List<PDMS_Dashboard> Dashboards = new BDMS_Dashboard().GetDashboardByUserID(PSession.User.UserID);
            foreach (PDMS_Dashboard D in Dashboards)
            {
                DashboardControl dashboardControl = (DashboardControl)D.DashboardID;
                lstConfiguredDashboardControls.Add(dashboardControl);
            }

            return lstConfiguredDashboardControls;
        }
        private List<UserControl> GetUserControlsForUserInConfiguredDashboardControls(List<DashboardControl> lstUserDashboardControls, List<DashboardControl> lstConfiguredDashboardControls)
        {
            List<UserControl> dashboardUserControlsForUser = new List<UserControl>();
            foreach (DashboardControl dashboardControl in lstConfiguredDashboardControls)
            {
                if (lstUserDashboardControls.Contains(dashboardControl))
                    dashboardUserControlsForUser.Add(GetUserControl(dashboardControl));
            }
            return dashboardUserControlsForUser;
        }
        private bool IsSmallDashboard(string DashboardType)
        {
            bool isSmallDashboard;
            switch (DashboardType)
            {
                case "ASP.usercontrols_vendorasns_ascx":
                    isSmallDashboard = true;
                    break;
                case "ASP.usercontrols_rejections_ascx":
                    isSmallDashboard = true;
                    break;
                default:
                    isSmallDashboard = false;
                    break;
            }
            return isSmallDashboard;
        }
        private void ReOrderUserControlsBySize(List<UserControl> lstUserControls)
        {
            int dashboardSeq = 0;
            for (int i = 0; i < lstUserControls.Count; i++)
            {
                if (IsSmallDashboard(lstUserControls[i].GetType().ToString()))
                    dashboardSeq = dashboardSeq + 1;
                else
                {
                    if (dashboardSeq > 0 && dashboardSeq % 2 == 1)
                    {
                        for (int j = i + 1; j < lstUserControls.Count; j++)
                        {
                            if (IsSmallDashboard(lstUserControls[j].GetType().ToString()))
                            {
                                UserControl ucTemp = lstUserControls[i];
                                lstUserControls[i] = lstUserControls[j];
                                lstUserControls[j] = ucTemp;
                                dashboardSeq = dashboardSeq + 1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        dashboardSeq = dashboardSeq + 2;
                    }
                }
            }
        }
        private UserControl GetUserControl(DashboardControl dashboardControl)
        {
            UserControl uc = null;
            switch (dashboardControl)
            {
                case DashboardControl.CustomerSatisfactionInAfterSalesSupport:
                    CustomerSatisfactionInAfterSalesSupport ucCSAS = (CustomerSatisfactionInAfterSalesSupport)_page.LoadControl("~/ViewDashboard/CustomerSatisfactionInAfterSalesSupport.ascx");
                    ucCSAS.ID = "ucCustomerSatisfactionInAfterSalesSupport";
                    uc = ucCSAS;
                    break;
                case DashboardControl.ICTicketTransactionStatics:
                    ICTicketTransactionStatics ucICTicketTransactionStatics = (ICTicketTransactionStatics)_page.LoadControl("~/ViewDashboard/ICTicketTransactionStatics.ascx");
                    ucICTicketTransactionStatics.ID = "ucICTicketTransactionStatics";
                    uc = ucICTicketTransactionStatics;
                    break;
                case DashboardControl.ICTicketEscalationOnBreakdownCount:
                    ICTicketEscalationOnBreakdownCount ucICTicketEscalationOnBreakdownCount = (ICTicketEscalationOnBreakdownCount)_page.LoadControl("~/ViewDashboard/ICTicketEscalationOnBreakdownCount.ascx");
                    ucICTicketEscalationOnBreakdownCount.ID = "ucICTicketEscalationOnBreakdownCount";
                    uc = ucICTicketEscalationOnBreakdownCount;
                    break;
                //case DashboardControl.DebitNoteAcknowledgePending:
                //    WarrantyClaimDebitNoteAcknowledgePending ucDMS_WarrantyClaimDebitNoteAcknowledgePending = (WarrantyClaimDebitNoteAcknowledgePending)_page.LoadControl("~/Dashboard/WarrantyClaimDebitNoteAcknowledgePending.ascx");
                //    ucDMS_WarrantyClaimDebitNoteAcknowledgePending.ID = "ucDMS_WarrantyClaimDebitNoteAcknowledgePending";
                //    uc = ucDMS_WarrantyClaimDebitNoteAcknowledgePending;
                //    break;
                case DashboardControl.LeadStatusOpen:
                    LeadStatusOpen StatusOpen = (LeadStatusOpen)_page.LoadControl("~/ViewDashboard/LeadStatusOpen.ascx");
                    StatusOpen.ID = "ucLeadStatusOpen";
                    uc = StatusOpen;
                    break;
                case DashboardControl.LeadStatusAssigned:
                    LeadStatusAssigned StatusAssigned = (LeadStatusAssigned)_page.LoadControl("~/ViewDashboard/LeadStatusAssigned.ascx");
                    StatusAssigned.ID = "ucLeadStatusAssigned";
                    uc = StatusAssigned;
                    break;
                case DashboardControl.LeadStatusQuotation:
                    LeadStatusQuotation StatusQuotation = (LeadStatusQuotation)_page.LoadControl("~/ViewDashboard/LeadStatusQuotation.ascx");
                    StatusQuotation.ID = "ucLeadStatusQuotation";
                    uc = StatusQuotation;
                    break;
            }
            return uc;
        }
    }
}