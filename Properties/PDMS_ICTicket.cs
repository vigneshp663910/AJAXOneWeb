using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_ICTicket
    {
        public long ICTicketID { get; set; }
        public string GUID { get; set; }

        public string ICTicketNumber { get; set; }
        public DateTime ICTicketDate { get; set; }
        public PDMS_ICTicket LastICTicket { get; set; }

        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public DateTime? LastHMRDate { get; set; }
        public int? LastHMRValue { get; set; }
        public string PresentContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? RequestedDate { get; set; }
        // FRS
        public string FSRNumber { get; set; }
        public DateTime? ReachedDate { get; set; }
        public DateTime? CurrentHMRDate { get; set; }
        public int? CurrentHMRValue { get; set; }
        public string ServiceOrderNumber { get; set; }

        public string ComplaintCode { get; set; }
        public string ComplaintDescription { get; set; }
        public string Information { get; set; }
        public string ReasonForCloser { get; set; }
        public string OldICTicketNumber { get; set; }
        public PDMS_ServiceType ServiceType { get; set; }
        public PDMS_ServiceSubType ServiceSubType { get; set; }
        public PDMS_ServiceTypeOverhaul ServiceTypeOverhaul { get; set; }
        public PDMS_ServicePriority ICPriority { get; set; }
        public PDMS_ServicePriority ServicePriority { get; set; }
        public string ServiceDescription { get; set; }
        public PDMS_ServiceStatus ServiceStatus { get; set; }
        public Boolean IsWarranty { get; set; }

        public Boolean IsMarginWarranty { get; set; }
        public string MarginRemark { get; set; }        

        public DateTime? TechnicianAssignedDate { get; set; }
         
        public DateTime? RestoreDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalBack { get; set; }
        public PUser RegisteredBy { get; set; }
        public string ServiceRecord { get; set; }
        public PUser Technician { get; set; }
        public PDMS_Address Address { get; set; }
        public string Location { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }

        public DateTime? FSRDate { get; set; }
        
        public PDMS_MainApplication MainApplication { get; set; }
        public PDMS_SubApplication SubApplication { get; set; }
        public PDMS_CustomerSatisfactionLevel CustomerSatisfactionLevel { get; set; }
        public string CustomerRemarks { get; set; } 
        public PUser ServiceConfirmationBy { get; set; }

        public PDMS_Material Material { get; set; }
        public PDMS_ServiceCharge ServiceMaterial { get; set; }
        public PDMS_ServiceMaterial ServiceMaterialM { get; set; }
        public string DeliveryNumber { get; set; }
        public string ScopeOfWork { get; set; }
        public decimal NoOfDays { get; set; }
        public Boolean HillyRegion { get; set; }

        public string TSIRNumber { get; set; }
        public string TSIRDate { get; set; }
        public string KindAttn { get; set; }
        public string Remarks { get; set; }
        public DateTime? ReqDeclinedDate { get; set; }
        public string ReqDeclinedReason { get; set; }


        public string SiteContactPersonName { get; set; }
        public string SiteContactPersonNumber { get; set; }
        public string SiteContactPersonNumber2 { get; set; }
        public PDMS_SiteContactPersonDesignation SiteContactPersonDesignation { get; set; }
        public Boolean IsCess { get; set; }
        public PDMS_TypeOfWarranty TypeOfWarranty { get; set; }

        public PDMS_WarrantyInvoiceHeader Claim { get; set; }
        public PDMS_PaidServiceInvoice Invoice { get; set; }
        public PDMS_ICTicketFSR FSR { get; set; }

        public Boolean IsMachineActive { get; set; }
        public string SubApplicationEntry { get; set; }

        //public int? OverHaulHMR { get; set; }
        //public DateTime? OverHaulWarrantyExpiryDate { get; set; }

        public int? DayLeftForClaimCreation { get; set; }

        public Boolean NoClaim { get; set; }
        public string NoClaimReason { get; set; }

        public DateTime? McEnteredServiceDate { get; set; }
        public DateTime? ServiceStartedDate { get; set; }
        public DateTime? ServiceEndedDate { get; set; }

        public decimal? CustomerPayPercentage { get; set; }
        public decimal? DealerPayPercentage { get; set; }
        public decimal? AEPayPercentage { get; set; }

        public int RowCount { get; set; }

        public List<PDMS_ServiceTechnician> Technicians { get; set; }
        public List<PDMS_ServiceCharge> ServiceCharges { get; set; }
        public Boolean? IsLocked { get; set; }
        public int? LockedUserID { get; set; }
        public int? LockedDeviceID { get; set; }
        public DateTime? LockedOn { get; set; } 
        public Boolean SyncBlock { get; set; }


    }
    [Serializable]
    public class PICTicket_Create
    {
        public long ICTicketID { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public long EquipmentHeaderID { get; set; }
        public string Location { get; set; }
        public string PresentContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string ComplaintDescription { get; set; }
        public int? ServicePriorityID { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int DistrictID { get; set; }
        public int CallCategoryID { get; set; }
        public Boolean IsOnline { get; set; }

    }
    [Serializable]
    public class PICTicketServiceConfirmation
    {
        public long ICTicketID { get; set; }
        public string EquipmentSerialNo { get; set; }
        public string DealerCode { get; set; }
        public string CustomerCode { get; set; }
        public string Location { get; set; }
        public int? OfficeID { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReachedDate { get; set; }
        public int? ServiceTypeID { get; set; }
        public int? ServiceSubTypeID { get; set; }
        public int? ServiceTypeOverhaulID { get; set; }
        public int? ServicePriorityID { get; set; }
        public DateTime? CurrentHMRDate { get; set; }
        public int? CurrentHMRValue { get; set; }
        public Boolean IsWarranty { get; set; }
        public int? TypeOfWarrantyID { get; set; }
        public int? MainApplicationID { get; set; }
        public int? SubApplicationID { get; set; }
        public string ScopeOfWork { get; set; }
        public string KindAttn { get; set; }
        public string Remarks { get; set; }
        public string SiteContactPersonName { get; set; }
        public string SiteContactPersonNumber { get; set; }
        public string SiteContactPersonNumber2 { get; set; }
        public PDMS_SiteContactPersonDesignation SiteContactPersonDesignation { get; set; }
        public Boolean IsCess { get; set; }
        public Boolean IsMachineActive { get; set; }
        public string SubApplicationEntry { get; set; }
        public Boolean NoClaim { get; set; }
        public string NoClaimReason { get; set; }
        public DateTime? McEnteredServiceDate { get; set; }
        public DateTime? ServiceStartedDate { get; set; }
        public DateTime? ServiceEndedDate { get; set; }
        public DateTime? RequestedDate { get; set; }

        public List<PDMS_ServiceTechnician> Technicians { get; set; }
    }

    [Serializable]
    public class PICTicketServiceConfirmation_Post
    {
        public long ICTicketID { get; set; }
        //public string EquipmentSerialNo { get; set; }
        //public string DealerCode { get; set; }
        //public string CustomerCode { get; set; }
        public string Location { get; set; }
        public int? OfficeID { get; set; } 
        public int? ServiceTypeID { get; set; }
        public int? ServiceSubTypeID { get; set; }
        public int? ServiceTypeOverhaulID { get; set; }
        public int? ServicePriorityID { get; set; } 
        public int? CurrentHMRValue { get; set; }
        //public Boolean IsWarranty { get; set; }
        public int? TypeOfWarrantyID { get; set; }
        public int? MainApplicationID { get; set; }
        public int? SubApplicationID { get; set; }
        public string ScopeOfWork { get; set; }
        public string KindAttn { get; set; }
        public string Remarks { get; set; }
        public string SiteContactPersonName { get; set; }
        public string SiteContactPersonNumber { get; set; }
        public string SiteContactPersonNumber2 { get; set; }
        public PDMS_SiteContactPersonDesignation SiteContactPersonDesignation { get; set; }
        public Boolean IsCess { get; set; }
        public Boolean IsMachineActive { get; set; }
        public string SubApplicationEntry { get; set; }
        public Boolean NoClaim { get; set; }
        public string NoClaimReason { get; set; }
        public DateTime? McEnteredServiceDate { get; set; }
        public DateTime? ServiceStartedDate { get; set; }
        public DateTime? ServiceEndedDate { get; set; } 
    }
    [Serializable]
    public class PDMS_ServiceType
    {
        public int ServiceTypeID { get; set; }
        public string ServiceType { get; set; }
        public string Description { get; set; } 
        public int IsFree { get; set; }
        public Boolean ManualPriceForService { get; set; }
        public Boolean IsMaterialRequired { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceSubType
    {
        public int ServiceSubTypeID { get; set; }
        public int ServiceTypeID { get; set; }
        public string ServiceSubType { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceTypeOverhaul
    {
        public int ServiceTypeOverhaulID { get; set; }
        public string ServiceTypeOverhaul { get; set; } 
    }
    [Serializable]
    public class PDMS_ServicePriority
    {
        public int ServicePriorityID { get; set; }
        public string ServicePriority { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PDMS_ServiceStatus
    {
        public int ServiceStatusID { get; set; }
        public string ServiceStatus { get; set; }
        public Boolean IsActive { get; set; }
    } 
    
    [Serializable]
    public class PDMS_ICTicketFSR
    {
        public long FsrID { get; set; }
        public string FSRNumber { get; set; }
        public DateTime FSRDate { get; set; }
        //  public string PresentContactNumberA { get; set; }
        public string Report { get; set; }
      
        public string CustomerRemarks { get; set; }
        public string ComplaintStatus { get; set; }
        public PDMS_ModeOfPayment ModeOfPayment { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public string Designation { get; set; }
        public string Complaint { get; set; }
        public PDMS_MachineMaintenanceLevel MachineMaintenanceLevel { get; set; }
        public Boolean IsRental { get; set; }
        public string RentalName { get; set; }
        public string RentalNumber { get; set; }
        public string OperatorName { get; set; }
        public string OperatorNumber { get; set; }
        public PCustomerProduct AvailabilityOfOtherMachine { get; set; }

        public string NatureOfComplaint { get; set; }
        public string Observation { get; set; }
        public string WorkCarriedOut { get; set; }
        public string SERecommendedParts { get; set; }

        public PDMS_ICTicketFSRSignature FSRSignature { get; set; }

        //public int? OTPNumber { get; set; }
        //public Boolean? IsAcknowledged { get; set; }
        //public Boolean? _IsAcknowledged
        //{
        //    get
        //    {
        //        if (IsAcknowledged == false)
        //            return true;
        //        else
        //            return false;
        //    }
        //}

        public PDMS_MachineAttachedFile MachineBeforeAttachedFile { get; set; }
        public PDMS_MachineAttachedFile MachineAfterAttachedFile { get; set; }
       

    }
    [Serializable]
    public class PDMS_ICTicketFSR_M
    {
        public long FsrID { get; set; }
        public long ICTicketID { get; set; }
        public Boolean IsRental { get; set; }
        public string OperatorName { get; set; }
        public string OperatorNumber { get; set; }
        public string RentalName { get; set; }
        public string RentalNumber { get; set; }
        public string Report { get; set; }
        public PDMS_MachineMaintenanceLevel MachineMaintenanceLevel { get; set; }
        public PDMS_ModeOfPayment ModeOfPayment { get; set; }

        public string NatureOfComplaint { get; set; }
        public string Observation { get; set; }
        public string WorkCarriedOut { get; set; }
        public string SERecommendedParts { get; set; }

        //public string FSRNumber { get; set; }
        //public DateTime FSRDate { get; set; } 

        //public string CustomerRemarks { get; set; }
        //public string ComplaintStatus { get; set; }  

        //public string Designation { get; set; }
        //public string Complaint { get; set; } 
    }
    [Serializable]
    public class PDMS_ICTicketFSRSignature
    {
        public long FSRSignatureID { get; set; }
        public long FsrID { get; set; }

       
        public string TPhoto { get; set; }
        public string TSignature { get; set; }
        public DateTime TSignatureOn { get; set; }
        public string TName { get; set; }
        public PUser TCreatedBy { get; set; }
        public PUser TModifiedBy { get; set; }

        public string CPhoto { get; set; }
        public string CSignature { get; set; }
        public DateTime CSignatureOn { get; set; }
        public string CName { get; set; }
        public PUser CCreatedBy { get; set; }
        public PUser CModifiedBy { get; set; }

    }

    [Serializable]
    public class PICTicketFSRSignature
    {
        public long FSRSignatureID { get; set; }
        public long FsrID { get; set; }
        public PAttachedFileS TPhoto { get; set; }
        public PAttachedFileS TSignature { get; set; }
        public PAttachedFileS CPhoto { get; set; }
        public PAttachedFileS CSignature { get; set; }
        public DateTime SignatureOn { get; set; }
        public string CName { get; set; }
        public string TName { get; set; }
        public PUser TCreatedBy { get; set; }
        public string FileType { get; set; }
    }

    [Serializable]
    public class PDMS_ModeOfPayment
    {
        public int ModeOfPaymentID { get; set; }
        public string ModeOfPayment { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }

    }
    [Serializable]
    public class PDMS_MachineMaintenanceLevel
    {
        public int MachineMaintenanceLevelID { get; set; }
        public string MachineMaintenanceLevel { get; set; }
        public Boolean IsActive { get; set; }
    }
 
    
  
    [Serializable]
    public class PDMS_MachineAttachedFile
    {
        public long AttachedFileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long TicketID { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
    }
    [Serializable]
    public class PDMS_FSRAttachedFile
    {
        public long AttachedFileID { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }
        public PDMS_ICTicketFSR FSR { get; set; } 
        public long FsrID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; } 
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public PDMS_FSRAttachedName FSRAttachedName { get; set; }
    }
    [Serializable]
    public class PDMS_FSRAttachedName
    {
        public int FSRAttachedFileNameID { get; set; }
        public string FSRAttachedName { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PAttachedFileS
    {
        public long AttachedFileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
    }

    [Serializable]
    public class PICTicketCustomerFeedback
    {
        public long ICTicketCustomerFeedbackID { get; set; }
        public long ICTicketID { get; set; }
        public PAttachedFileS Photo { get; set; }
        public PAttachedFileS Signature { get; set; }
        public string Remarks { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public PDMS_CustomerSatisfactionLevel CustomerSatisfactionLevel { get; set; }
    }


    [Serializable]
    public class PDMS_FSRAttachedFile_M
    {
        public long? AttachedFileID { get; set; }
        public long ICTicketID { get; set; }
        public long FsrID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public Boolean IsDeleted { get; set; }
        public PAttachedName FSRAttachedName { get; set; }
    }

    [Serializable]
    public class PAttachedName
    {
        public int AttachedFileNameID { get; set; }
        public string AttachedName { get; set; }
        public Boolean IsActive { get; set; }
    }


    [Serializable]
    public class PICTicketCallCategory
    {
        public int CallCategoryID { get; set; }
        public string CallCategory { get; set; }
    }
    [Serializable]
    public class POnlineServiceTicket
    {
        public long OnlineServiceTicketID { get; set; }
        public string OnlineTicketNumber { get; set; }
        public DateTime OnlineTicketDate { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string ComplaintDescription { get; set; }
        public PDMS_ServicePriority ICPriority { get; set; }
        public PStatusItem Status { get; set; }
        public Boolean IsWarranty { get; set; }
        public string Location { get; set; }
        public DateTime? RestoreDate { get; set; }
        public string RestoreRemarks { get; set; }
        public PDMS_Address Address { get; set; }
        public PDMS_CustomerSatisfactionLevel CustomerSatisfactionLevel { get; set; }
        public PUser CustomerSatisfactionLevelUpdatedBy { get; set; }
        public DateTime? CustomerSatisfactionLevelUpdatedOn { get; set; }
        public PUser RegisteredBy { get; set; }
        public PUser EscalatedL1 { get; set; }
        public DateTime? EscalatedL1On { get; set; }

    }
}
