using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Properties
{

   
    public enum FileType
    {
        Unknown = 1,
        Message = 2,
        Word = 3,
        RAR = 4,
        Excel = 5,
        MSG = 6,
        Pdf = 7,
        XML = 8,
        zipped = 9,
        Jpeg = 10,
        Png = 11

    }
    public enum FunctionalErrorCode
    {
        UserExists = 1,
        UserExistForThisVendor = 2,
        UserExistsForStaff = 3,
        InvalidPassword = 4,
        InvalidUserName = 5,
        AccountLocked = 6,
        AccountDisabled = 7,
        GeneralFailure = 8,
        ChangePwdOldPwdIncorrect = 9,
        ChangePwdNewAndConfirmPwdNotMatching = 10,
        ChangePwdStdNotMet = 11,
        InvalidOTP = 12,
        OTPTimeExpired = 13
    }
    public enum MessageModuleType
    {
        AuthorizeAccount = 1,
        DiclineAccount = 2,
        CreateUser = 3,
        ForgotPassword,
        UnLockUser,
        EnableUser,
        DisableUser
    }
    public enum ErrorCode
    {
        AE = 1,
        IOE,
        DBE,
        SQLDBE,
        FNE,
        GENE,
        NURE,
        WEBSERVICE,
        MSGERROR,
        FTPERROR
    }
    public enum ParameterDirections
    {
        Intput = 1,
        Output = 2
    }

    public enum Category
    {
        SAP = 1,
        EnterpriseCollaboration = 2,
        EnterpriseProjects,
        Infrastructure,
        SAPDevelopment,
        Domain,
        CustomApps,
        CRM,
        Exchange_Email,
        SAP_MaterialMaster,
        SAP_VendorMaster,
        SparesMIS,
        CustomerList,
        PartsProcurement,
        SpareSales,
        Service,
        Reports,
        TallyExport,
        MachineProcurement,
        MachineSales,
        Warranty,
        DMS,
        SAPAuthorization,
        SAP_BI,
        PLM,
        SAPBug,
        SAPCRM,
        AdminSupportDpurPlant_Obedenahalli,
        Attendance,
        HealthInsurance,
        ProvidentFund,
        HRLetters,
        ePMSRelatedIssues,
        PersonalInfoUpdation,
        HRPolicyRelatedClarification,
        Payroll,
        Form16,
        DealerEmployeeMasterData,
        HROthers,
        AdminSupportDpurPlant_Bashettihalli,
        AdminSupportPeenyaOffice,
        AdminSupportRegionalOffices,
        SoftwareConfiguration,
    }

    public enum ApplicationSettings
    {
        DebitNoteNumberFormat = 1,
        ICTicketNumber = 2,
        EInvoiceToken = 3 
    }

    public enum UserTypes
    {
        Director = 1,
        SupperUser,
        HOD,
        Manager,
        Associate,
        Admin,
        Dealer
    }
    public class LMSFunctionalException : Exception
    {
        public LMSFunctionalException(FunctionalErrorCode message) : base(message.ToString()) { }
    }
   

    public static class ErrorHandler
    {
        #region Properties
        public static string GetErrorMessage(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.AE:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "AEErrorMessage"));

                case ErrorCode.IOE:
                    return "";
                case ErrorCode.FTPERROR:
                    return "";
                case ErrorCode.SQLDBE:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "SQLDBEErrorMessage"));
                case ErrorCode.WEBSERVICE:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "WEBSERVICEErrorMessage"));
                case ErrorCode.MSGERROR:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "MSGERRORErrorMessage"));
                default:
                    return "";


            }
        }
        public static string GetErrorMessageWithoutErrorCode(string key)
        {
            return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", key));
        }

        public static ErrorCode GetErrorCode(string errorCode)
        {
            switch (errorCode)
            {
                case "AE":
                    return ErrorCode.AE;
                case "IOE":
                    return ErrorCode.IOE;
                case "DBE":
                    return ErrorCode.DBE;
                case "SQLDBE":
                    return ErrorCode.SQLDBE;
                case "FNE":
                    return ErrorCode.FNE;
                case "WEBSERVICE":
                    return ErrorCode.WEBSERVICE;
                case "MSGERROR":
                    return ErrorCode.MSGERROR;
                default:
                    return ErrorCode.AE;
            }
        }
        public static FunctionalErrorCode GetFunctionalErrorCode(string errorCode)
        {
            switch (errorCode)
            {
                case "UserExists":
                    return FunctionalErrorCode.UserExists;
                case "UserExistForThisVendor":
                    return FunctionalErrorCode.UserExistForThisVendor;
                case "UserExistsForStaff":
                    return FunctionalErrorCode.UserExistsForStaff;
                case "InvalidPassword":
                    return FunctionalErrorCode.InvalidPassword;
                case "InvalidUserName":
                    return FunctionalErrorCode.InvalidUserName;
                case "AccountLocked":
                    return FunctionalErrorCode.AccountLocked;
                case "AccountDisabled":
                    return FunctionalErrorCode.AccountDisabled;
                case "ChangePwdOldPwdIncorrect":
                    return FunctionalErrorCode.ChangePwdOldPwdIncorrect;
                case "ChangePwdNewAndConfirmPwdNotMatching":
                    return FunctionalErrorCode.ChangePwdNewAndConfirmPwdNotMatching;
                case "ChangePwdStdNotMet":
                    return FunctionalErrorCode.ChangePwdStdNotMet;
                default:
                    return FunctionalErrorCode.GeneralFailure;
            }
        }
        public static string GetErrorMessage(FunctionalErrorCode code)
        {
            switch (code)
            {
                case FunctionalErrorCode.UserExists:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "UserExists"));
                case FunctionalErrorCode.UserExistForThisVendor:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "UserExistForThisVendor"));
                case FunctionalErrorCode.UserExistsForStaff:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "UserExistsForStaff"));
                case FunctionalErrorCode.InvalidPassword:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "InvalidPassword"));
                case FunctionalErrorCode.InvalidUserName:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "InvalidUserName"));
                case FunctionalErrorCode.AccountLocked:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "AccountLocked"));
                case FunctionalErrorCode.AccountDisabled:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "AccountDisabled"));

                case FunctionalErrorCode.ChangePwdOldPwdIncorrect:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "ChangePwdOldPwdIncorrect"));
                case FunctionalErrorCode.ChangePwdNewAndConfirmPwdNotMatching:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "ChangePwdNewAndConfirmPwdNotMatching"));
                case FunctionalErrorCode.ChangePwdStdNotMet:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "ChangePwdStdNotMet"));
                default:
                    return Convert.ToString(HttpContext.GetGlobalResourceObject("Resource", "GeneralFailure"));
            }
        }

        #endregion
    }

    public class LMSMessageStatus
    {
        public string Errormessage { get; set; }
        public bool IsProcessed { get; set; }
    }

    public enum TicketStatus
    {
        Open = 1,
        Assigned,
        InProgress,
        Resolved,
        Closed,
        WaitingForApproval,
        Cancel,
        Approved,
        Reopen,
        Deleted,
        Foreclose
    }

    
    public enum SystemCategory
    {
        AF = 1,
        Dealer,
        Vendor,
        SupportTR,
        Support,
        HR
    }

    public enum TicketStatus1
    {
        Open = 1,
        Assigned,
        InProgress,
        Resolved,
        Closed,
        Approve,
    }
    public enum DMS_MenuMain
    {
        Master = 1,
        Procurement,
        Sales,
        Service,
        Stock,
        ChangeLog,
        Finance,
        Admin,
        Employee,
        Marketing,
        Dashboard,
        Organisation,
        ECatalogue,
        Help,
        PreSales,
        Task
    }
    public enum DMS_MenuSub
    {
        Material = 1,
        Customer = 2,
        Equipment = 3,
        BIN = 4,
        Price = 5,
        Supercede = 6,
        ROQ_DOQ = 7,
        POList = 8,
        ASNList = 9,
        GRList = 10,
        POReturn = 11,
        PendingSAPPO = 12,
        PendingASN = 13,
        PendingGR = 14,
        POPerfomance = 15,
        Quotation = 16,
        SaleOrderHeader = 17,
        SaleOrderItem = 18,
        Delivery = 19,
        SaleOrderInvoicePartsReport = 20,
        SalesReturn = 21,
        PendingDelivery = 22,
        PendingInvoice = 23,
        SalesPerfomance = 24,
        MTTR = 25,
        PaidService = 26,
        WarrantyClaim = 27,
        ApprovalStatus = 28,
        WarrantyFailedMaterialReturn = 29,
        WarrantyClaimInvoiceReport = 30,
        WarrantyClaimInvoiceCreate = 31,
        WarrantyClaimAnnexureReport = 32,
        ServicePerfomance = 33,
        ConvertedStock = 34,
        StockOverview = 35,
        StockSummary = 36,
        DMS_AFAccount = 37,
        Procurement = 38,
        Sales = 39,
        Service = 40,
        Stock = 41,
        ClaimPrint = 42,
        TicketTracking = 43,
        WarrantyClaimAnnexureCreate = 44,
        WarrantyDeliveryReport = 45,
        WarrantyReqForClaimApproval = 46,
        WarrantyFinalInvoiceCreateAbove50K = 47,
        WarrantyDebitNoteCreate = 48,
        DMSUserManagement = 49,
        ICTicketManage = 50,
        PaidServiceQuotation = 51,
        PaidServiceProformaInvoice = 52,
        PaidServiceInvoice = 53,
        MTTR_New = 54,
        ApproveDeclinedICTicket = 55,
        WarrantyFailedMaterialDCCreation,
        WarrantyFailedMaterialDCReport,
        WarrantyFailedMaterialDCGateEntry,
        ICTicketRequestedDateChange,
        ICTicketMarginWarrantyChange,
        UserManagement,
        MobileUserApprove,
        WarrantyDebitNoteAcknowledge,
        WarrantyDebitNoteReport,
        CampignCreateOrUpdate,
        CampignReport,
        ICTicketFSRManage,
        ICTicketTsirManage,
        DealerEmployeeCreate,
        DealerEmployeeManage,
        DealerEmployeeApproval,
        DealerEmployeeRoleAssign,
        DealerEmployeeLeaving,
        ActivityInfoMater,
        ActivityPlan,
        ActivityActual,
        ICTicketTsirMailSendVendorReport,
        ICTicketStatusReport,
        ICTicketTsirReport,
        ActivityClaimApprovalMarketingLevel1,
        ActivityClaimApprovalMarketingLevel2,
        ActivityClaimApprovalServiceLevel1,
        ActivityClaimApprovalServiceLevel2,
        ActivityClaimApprovalSparesLevel2,
        ActivityInvoiceReport,
        WarrantyPartsAvailabilityReport,
        DeviatedICTicketRequestForApproval,
        DeviatedICTicketApprove,
        DeviatedCliamRequestForApproval,
        DeviatedCliamApprove,

        GeographyManage,
        TehsilTransfer,
        VillageTransfer,
        ActivityInfoMaster,
        ABPPlanModelWise,
        MonthlyPlanModelWise,
        EquipmentHistory,
        ServiceEngineerUtilisationReport,
        ICTicketListDUMP,
        SaleOrderInvoiceMcReport,
        SaleOrderInvoiceWarrantyReport,
        DirectClaimEntry,
        ActivityClaimApprovalLevel1,
        ActivityClaimApprovalLevel2,
        ABPSparePart,
        ABPSparePartRetail,
        LostCustomerData,
        ICTicketTSIRSalesApproveLevel1,
        ICTicketTSIRSalesApprove,
        ActivityClaimApprovalSalesLevel1,
        ActivityClaimApprovalSalesLevel2,
        ActivityClaimApprovalTrainingLevel1,
        ActivityClaimApprovalTrainingLevel2,
        DashboardICTicketService,
        DashboardICTicketClaim,
        ICTicketTSIRMessage,
        EInvoiceRequest,
        EDebitNoteRequest,
        WebQuotationCreate,
        WebQuotationApprove,
        WebQuotationReport,
        WebQuotationSendToSAP,


        BankDepositClearingCreate,
        BankDepositClearingEditAndConfirm,
        BankDepositClearingPostingInSAP,
        BankDepositClearingReport,
        UpdateCommissioningDate,
        EquipmentPopulationReportForAE

    }
    public enum SubModule
    {
        ViewMaster_Material = 1,
        ViewMaster_Customer = 2,
        ViewEquipment_Equipment = 3,
        ViewEquipment_EquipmentChangeApproval = 4,
        ViewDashboard_BIEnquiry = 5,
        UnderCons = 6,
        ViewDashboard_Task_Dashboard = 7,
        ViewProcurement_PurchaseOrder = 8,
        ViewProcurement_PurchaseOrderASNReport = 9,
       // UnderCons = 10,
     //   UnderCons = 11, 
//1 =  12, 
//1 =  13,
     //   UnderCons = 14,
        ViewProcurement_PurchaseOrderPerformance = 15,
        ViewSales_SaleOrderInvoicePartsReport = 16,
     //   UnderCons = 17,
        ViewSales_SalesOrder = 18,
     //   UnderCons = 19, 
//**** Menu Level-2 ****************************** =  20, 
//1 =  21,
    //    UnderCons = 22,
     //   UnderCons = 23,
        ViewSales_SalesOrderPerformance = 24,
        ViewService_MTTR_Report = 25,
        ViewService_PaidServiceReportNew = 26,
        ViewService_WarrantyClaim = 27,
        ViewService_WarrantyClaimApprovalLevel1 = 28,
        ViewService_WarrantyFailureMaterialReport = 29,
        ViewService_WarrantyClaimInvoiceReport = 30,
        ViewService_WarrantyClaimInvoiceCreate = 31,
        ViewService_WarrantyClaimAnnexureReport = 32, 
 //=  33,
     //   UnderCons = 34,
     //   UnderCons = 35,
        ViewSupportTicket_RequestSupportTicket = 36, 
//**** Menu Level-2 ****************************** =  37,
 //       UnderCons = 38,
 //       UnderCons = 39,
 //       UnderCons = 40,
 //       UnderCons = 41, 
 //=  42,
        ViewService_TicketTracking = 43,
        ViewService_WarrantyClaimAnnexureCreate = 44,
        ViewService_WarrantyDeliveryReport = 45,
        ViewService_WarrantyClaimApprovalRequest = 46,
        ViewService_WarrantyClaimInvoiceCreate5k = 47,
        ViewService_WarrantyClaimDebitNoteCreate = 48, 
 //=  49,
        ViewService_ICTicket = 50,
        ViewService_PaidServiceQuotation = 51,
        ViewService_PaidServiceProformaInvoice = 52,
        ViewService_PaidServiceInvoice = 53,
        ViewService_MTTR_New = 54,
        ViewService_ApproveDeclinedICTicket = 55,
        ViewService_WarrantyFailureMaterialDCCreation = 56,
        ViewService_WarrantyFailureMaterialDCReport = 57,
        ViewService_WarrantyFailureMaterialGateEntry = 58,
        ViewService_ICTicketReachedDateChange = 59,
        ViewService_ICTicketMarginWarrantyChange = 60,
        ViewAdmin_UserManagement = 61,
        ViewAdmin_UserMobileApproval = 62,
        ViewService_WarrantyClaimDebitNoteAcknowledge = 63,
        ViewService_WarrantyClaimDebitNoteReport = 64,
        //UnderCons = 65,
        //UnderCons = 66,
        ViewService_ICTicketFSRManage = 67,
        ViewService_ICTicketTSIRManage = 68,
        ViewMaster_DealerEmployeeCreate = 69,
        ViewMaster_DealerEmployeeManage = 70,
        ViewMaster_DealerEmployeeApproval = 71,
        ViewMaster_DealerEmployeeAssigningRole = 72,
        ViewMaster_DealerEmployeeLeaving = 73,
     //   UnderCons = 74,
        ViewMarketing_ActivityPlan = 75,
        ViewMarketing_ActivityActual = 76,
        ViewService_ICTicketTSIRMailToVendorReport = 77,
        ViewService_ICTicketStatusReport = 78,
        ViewService_ICTicketTSIRReport = 79,
     //   ViewMarketing_ActivityClaimApproval.aspx?PAGE = 1 & FA = 0 = 80,
       // ViewMarketing_ActivityClaimApproval.aspx?PAGE = 2 & FA = 0 = 81,
        //UnderCons = 82,
        //UnderCons = 83,
        //UnderCons = 84,
        ViewMarketing_ActivityInvReports = 85,
        ViewService_WarrantyPartsAvailabilityReport = 86,
        ViewService_ApprovalForDeviatedICTicketRequest = 87,
        ViewService_ApproveDeviatedICTicket = 88,
        ViewService_ApprovalForDeviatedCliamReques = 89,
        ViewService_ApproveDeviatedCliamApprove = 90,
        ViewMaster_Location = 91,
        TehsilTransfer = 92,
        VillageTransfer = 93,
        ActivityInfoMaster = 94,
        ABPPlanModelWise = 95,
        MonthlyPlanModelWise = 96,
        ViewService_EquipmentHistory = 97,
        ViewService_ICTicketServiceEngineerUtilisationReport = 98,
        ICTicketListDUMP = 99,
        ViewSales_SaleOrderInvoiceMcReport = 100,
        ViewSales_SaleOrderInvoiceWarrantyReport = 101,
        ViewMarketing_ClaimEntry = 102,
        //UnderCons = 103,
        //UnderCons = 104,
        ViewMarketing_ABPSparePart = 105,
        ViewMarketing_ABPSparePart_Retail = 106,
        ViewMarketing_LostCustomerData = 107,
      //  ViewService_ICTicketTSIRApprove = 108,
        //UnderCons = 109,
        //UnderCons = 110,
        //UnderCons = 111,
        //UnderCons = 112,
        //UnderCons = 113,
      //  _DMS_DashboardICTicketService.aspx = 114,
      //  _DMS_DashboardICTicketClaim.aspx = 115,
        ViewService_ICTicketTSIRMessage = 116,
        ViewMaster_EInvoiceRequest = 117,
        ViewService_EDebitNoteRequest = 118,
        //UnderCons = 119,
        //UnderCons = 120,
        //UnderCons = 121,
        //UnderCons = 122,
        //UnderCons = 123,
        //UnderCons = 124,
        //UnderCons = 125,
        //UnderCons = 126,
     //   _DMS_UpdateCommissioningDate.aspx = 127,
        ViewEquipment_EquipmentPopulationReportForAE = 128,
        Enquiry_Indiamart = 129, 
//**** Menu Level-2 ****************************** =  130,
        ViewAdmin_tab_AdminModule = 131,
        ViewAdmin_DefaultUserModule = 132,
        Account_LoginAs = 133,
        ViewAdmin_UserList = 134,
//        UnderCons = 135,
//        UnderCons = 136, 
//**** Menu Level-2 ****************************** =  137,
        Help_HelpOld = 138,
       // UnderCons = 139,
        ViewPreSale_Quotation = 140, 
//**** Menu Level-2 ****************************** =  141,
        ViewPreSale_Pre_Sales_Dashboard = 142,
        SignIn = 143,
        SubMenuLevel2 =  144,
        ProcessFlow_OrgChart = 145,
        ProcessFlow_Process_Flow = 146,
        ProcessFlow_ProjectTeam = 147,
        ViewOrganization_Organization = 148, 
//**** Menu Level-2 ****************************** =  149, 
//**** Menu Level-2 ****************************** =  150, 
//**** Menu Level-2 ****************************** =  151, 
//**** Menu Level-2 ****************************** =  152,
        ViewPreSale_Visit = 153,
        ProcessFlow_DMM_Process = 154,
        ViewPreSale_VisitTargetManage = 155, 
//**** Menu Level-2 ****************************** =  156, 
//**** Menu Level-3 ****************************** =  157, 
// =  158,
        ViewPreSale_ManageLeadFoloowUps = 159,
        ViewSupportTicket_AssignedSupportTicket = 160,
        ViewSupportTicket_InProgressSupportTicket = 161,
        ViewSupportTicket_ManageSupportTicket = 162,
        Account_ChangePassword = 163,
        Account_SignOut = 164,
        Help_Help = 165,
        ViewSupportTicket_OpenSupportTicket = 166,
        ViewSupportTicket_ClosedSupportTicket = 167,
        ViewAdmin_UserMobileManagement = 168,
        ViewSales_SalesCommissionClaim = 169,
        ViewSales_SalesCommissionClaimApprove = 170,
        ProcessFlow_Pre_Sales = 171,
        ViewPreSale_LeadN = 172,
        Account_CompanyProfile = 173,
        Account_MyProfile = 174,
//        UnderCons = 175, 
//**** Menu Level-2 ****************************** =  176,
        ViewPreSale_EnquiryIndiamart = 177,
        ViewSales_SalesCommissionClaimCreate = 178,
        ViewSales_SalesCommissionClaimInvoiceCreate = 179,
        ViewSales_SalesCommissionClaimInvoice = 180,
        ViewService_ICTicketCreate = 181,
        ViewMaster_Dealer = 182,
        ViewMaster_CreateAjaxEmployee = 183,
        ViewChangeHistory_CustomerChangeHistory = 184,
        ViewChangeHistory_LeadChangeHistory = 185,
        ViewChangeHistory_QuotationChangeHistory = 186,
        ViewChangeHistory_ICTicketChangeHistory = 187,
        ViewMaster_LocationMap = 188,
        ViewMaster_UserLocationTrackMap = 189,
        ViewMaster_SalesCommissionClaimPrice = 190,
        ViewProcurement_PurchaseOrderInvoiceReport = 191,
        ViewService_ICTicketCommissionMailToReport = 192,
        ViewDealerEmployee_UserMonthlyVerification = 193,
        ViewDashboard_LeadChart = 194, 
//**** Menu Level-2 ****************************** =  195,
        ViewActivity_Activity = 196,
        View_Attendance = 197,
        ViewPreSale_BudgetPlanningYear = 198,
        ViewPreSale_BudgetPlanningMonth = 199,
        ViewPreSale_EnquiryN = 200,
        RedirectTo_eCatalogue = 201,
        RedirectTo_SPT = 202,
        ProcessFlow_Service = 203,
        ViewAdmin_DealerCustomerMapping = 204,
        ViewMaster_Application = 205,
        ViewMaster_PresalesMasters = 206,
        ViewPreSale_VisitReport = 207,
        ViewPreSale_PreSalesReport = 208,
        View_Project = 209,
        ViewAdmin_DealerStateMapping = 210,
        ViewDealerEmployee_SalesIncentive = 211,
        ViewDashboard_EnquiryChart = 212,
        ViewDealerEmployee_DealerOperatorDetails = 213,
        ProcessFlow_Ticketing = 214,
        ViewAdmin_DealerwisePermissionList = 215,
        ViewAdmin_ModulePermissionList = 216,
        ViewSupportTicket_Task_DashboardMonthwise = 217,
        ViewSales_SalesCommissionClaimInvoiceVerify = 218,
        ViewService_ICTicketStatusReportForIC = 219,
        ViewSales_SalesCommissionClaimPerformanceReport = 220,
        ViewPreSale_LeadFoloowUpReport = 221,
        ViewAdmin_UserActivityTrackingReport = 222,
        ViewEquipment_EquipmentMissing = 223,
        ViewPreSale_SalesPipelineReport = 224,
        ViewService_ICTicketTSIRApprove = 224,
    }
    public enum SubModuleChildMaster
    {
        TsirCheck = 1,
        TsirApprove,
        TsirCancel,
        ClaimApproval1,
        ClaimApproval2,
        ClaimApproval3,
        ClaimCancel,
        SalesCommClaimApproval1,
        SalesCommClaimApproval2,
        SalesCommClaimApproval3 = 10,
        EmployeeEdit,
        EditLead,
        AssignLead,
        AddFollowUpLead,
        CustomerConversationLead,
        CustomerVerify,
        TaxQuotationPrint,
        FinancialInfoLead,
        AddProductLead,
        ConvertToQuotationLead = 20,
        AddQuestionariesLead,
        AddVisitLead,
        LostLead,
        CancelLead,
        EditVisitTarget = 25,
        EditApplication,
        AddEditLocation,
        EidtDistrictSalesEngineer,
        CustomerExcelDownload,
        MaterialExcelDownload,
        SyncToParts = 31,
        TsirSalesApproveL1,
        TsirSalesApproveL2,
        UpdateCommDate,
        WarrantyTypeChange,
        OwnershipChange,
        ExpiryDateChange,
        ApproveWarrantyTypeChange,
        ApproveOwnershipChange,
        ApproveExpiryDateChange,
        LeadAjax,
        RequestForDecline = 42,
        MarginWarrantyChange,
        RequestDateChange,
        ICTicketUnlock,
        AddServiceEngineer,
        EditCall_InfoFSR_TSIR_Restore = 47,
        RequstForClaimAndInvoice,
        ActivityClaimApprovalLevel1,
        ActivityClaimApprovalLevel2,
        ActivityClaimApprovalMarketingLevel1,
        ActivityClaimApprovalMarketingLevel2,
        ActivityClaimApprovalServiceLevel1,
        ActivityClaimApprovalServiceLevel2,
        ActivityClaimApprovalSparesLevel1,
        ActivityClaimApprovalSparesLevel2,
        ActivityClaimApprovalSalesLevel1,
        ActivityClaimApprovalSalesLevel2,
        ActivityClaimApprovalTrainingLevel1,
        ActivityClaimApprovalTrainingLevel2
    }
    public enum DMS_WarrantyClaimStatus
    {
        REQUESTED = 1,
        REJECTED,
        APPROVED_L1,
        APPROVED,
        ANNEXURED,
        INVOICED,
        ACCOUNTED,
        CANCELED,
        Man_INVOICED,
        PAID,
        APPROVED_L2,
    }

    public enum DMS_InvoiceType
    {
        NEPI_Commission = 1,
        Warranty_Service,
        Above50k,
        DebitNote,
        Warranty_ServicePartial,
        Above50kPartial
    }

    public enum DMS_ServiceType
    {
        Paid1 = 1,
        Warranty,
        NEPI,
        Commission,
        PreCommission,
        Others,
        OverhaulService,
        PromotionalActivity,
        PolicyWarranty,
        PartsWarranty,
        REPI,
        RCommission,
        AMC,
        RWarranty,
        GoodwillWarranty
    }
    public enum DMS_ServiceStatus
    {
        Open = 1,
        TechnicianAssigned,
        Reached,
        Restored,
        ReqDeclined,
        Declined,
        Reopen
    }

    public enum Jobs
    {
        SendSMS = 1,
        SendMail = 2,
        ICTicketIntegrationFromCRM = 3,
        MaterialIntegrationFromPostgre = 4,
        SAPDocumentForWarrantyInvoiceFromSAP = 5,
        SaleOrderNumberForSrviceQuatationFromSAP = 6,
        TechnicianIntegrationFromSAP = 7,
        UpdateICTicketToSAP = 8,
        ModelForClaim = 9,
        Category = 10,
        ICTicketIntegrationVerification = 11,
        QuotationForJSN = 12,
        IntegrationSalesOrder = 13,
        IntegrationSalesOrderInvoice = 14,
        IntegrationClaimAnnexure = 15,
        EInvoice = 16,
        SendMailMttrEscalationMatrix = 17,
        IntegrationEquipmentFromSAP = 18,
        IntegrationMaterialFromEccSap = 19,
        GetPurchaseOrderIntegration = 20,
        MaterialIntegrationFromSAP = 21,
        CustomerIntegration =22  ,
      //  UpdateSalesQuotationDeliveryDetails = 23 ,
        SalesQuotationFlowFromSap = 24,
        EnquiryFromCRM = 25,
        LeadQualificationByExpectedDateOfSale = 26,
        IntegrationEquipmentFromSAP_New = 27,
        EnquiryIndiamart =28
    }

    public enum DashboardControl
    {
        CustomerSatisfactionInAfterSalesSupport = 1,
        ICTicketEscalationOnBreakdownCount,
        ICTicketTransactionStatics,
        DebitNoteAcknowledgePending,
        LeadStatus, 
        FoloowUpCount,
        //LeadStatusAssigned,
        //LeadStatusAssigned,
        //LeadStatusAssigned, 

        //ICTicketEscalationOnBreakdownLevel2,
        //ICTicketEscalationOnBreakdownLevel3,
        //ICTicketEscalationOnBreakdownLevel4, 
    }
    public enum FSRAttachedFileName
    {
        BeforeMachineRestore = 1,
        AfterMachineRestore,
        Technician,
        Customer,
        TechnicianSignature,
        CustomerSignature,
        CheckList
    }
    public enum MaterialSource
    {
        Dealer = 1,
        Vendor,
        Ajax,
        Customer
    }
    public enum TSIRStatus
    {
        Requested = 1,
        Checked,
        Approved,
        SendBack,
        Rejected,
        Rerequested,
        SalesApprovedLevel1,
        SalesApproved,
        SalesRejected,
        Canceled
    }
    public enum TypeOfWarranty
    {
        Warranty = 1,
        Parts_Warranty,
        OnlyForInfo,
        Policy_Warranty,
        MarginWarranty
    }
    public enum RefreshEnum
    {
        ServiceChargesAddOrRemove = 1
    }
    public enum LeadStatus
    {
        Unattended = 1,
        InProgress = 2,
        Quotation = 3,
        Won = 4,
        SalesLost = 5,
        Dropped = 6
    }
    public enum PreSaleStatus
    {
        Unattended = 1,
        Close = 2,
        Cancel = 3,
        ConvertedToLead = 4,
        Rejected = 5,
        InProgress = 6
    }
     


    public enum DealerDesignation
    {
        SalesExecutive = 4,
        ServiceTechnician = 8,
        BusinessSystemManager = 31,
        BusinessSystemExecutive = 32,
        BusinessSystemHead = 51
    }

    public enum SalesQuotationStatus
    {
        Quotation = 1,
        SaleOrder,
        Delivery,
        Invoice,
        Lost,
        ConvertedToOtherProduct,
        Closed

    }

    public enum SalesQuotationNoteList
    {
        Reference = 1,
        KindAttention,
        Note,
        Hypothecation,
        TermsOfPayment,
        Delivery,
        Validity,
        Foc,
        MarginMoney,
        Subject,
        Name,
        Designation,
        PhoneNumber
    }
    public enum enumRegion
    {
        East = 2,
        North = 3,
        South1 = 4,
        South2 = 5,
        West = 6
    }
}
