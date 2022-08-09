using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Properties
{

    public enum EmpModuleAccess
    {
        QIRCreate = 1,
        QIRCorrection,
        QIRApprove,
        QIRReport,
        EmpManagement
    }
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

    public enum LMSApplicationSettings
    {
        PasswordExpiryEnabled = 4,
        CreateUserForManagerEnabled = 5,
        DefaultPassword = 8,
        Region = 9,
        JobStatusReworkEnabled = 11,
        SMSNotificationEnabled = 14,
        MaxLoginFailureAttempt = 17,
        PasswordExpiryDurationInDays = 23,
        DisplayDashboard = 46
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
    public enum SubModule
    {
        CreateUser = 1,
        AuthorizeUser = 2,
        UserManagement = 3,
        DataUpload,
        ViewCirculars,
        CreateCirculars,
        Documents
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
        Approve,
    }

    public enum AppSetting
    {
        Admin = 1,
        SupperUser = 2,
        HOD = 3,
        Manger = 4,
        TicketAssign = 5,
        TRApprover = 6,
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
        UpdateSalesQuotationDeliveryDetails = 23 ,
        SalesQuotationDocumentsFromSap = 24
    }

    public enum DashboardControl
    {
        CustomerSatisfactionInAfterSalesSupport = 1,
        ICTicketEscalationOnBreakdownCount,
        ICTicketTransactionStatics,
        DebitNoteAcknowledgePending,
        LeadStatusOpen,
        LeadStatusAssigned,
        LeadStatusQuotation,
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
        Open = 1,
        Assigned,
        Quotation,
        Won,
        Lost,
        Cancelled,
    }
    public enum PreSaleStatus
    {
        Open = 1,
        Close,
        Cancel,
        ConvertedToLead ,
        Rejected,
    }

    public enum DealerDesignation
    {
        SalesExecutive = 4,
        ServiceTechnician = 8
    }
}
