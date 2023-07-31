using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_EquipmentHeader
    {
        public DateTime? CurrentHMRDate { get; set; }
        public int? CurrentHMRValue { get; set; }
        public long EquipmentHeaderID { get; set; }
        public PEquipmentClient EquipmentClient { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_Model EquipmentModel { get; set; }
        public string EquipmentSerialNo { get; set; }
        public string EngineModel { get; set; }
        public string EngineSerialNo { get; set; }
        public string CorrectSMR { get; set; }
        public DateTime? DispatchedOn { get; set; }
        public DateTime? CommissioningOn { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }

        public Boolean? IsRefurbished { get; set; }
        public int? RefurbishedBy { get; set; }
        public DateTime? RFWarrantyStartDate { get; set; }
        public DateTime? RFWarrantyExpiryDate { get; set; }

        public Boolean? IsAMC { get; set; }
        public DateTime? AMCStartDate { get; set; }
        public DateTime? AMCExpiryDate { get; set; }
        public DateTime? HMRDate { get; set; }
        public int? HMRValue { get; set; }
        public string CounterObjectID { get; set; }

        public PDMS_EquipmentItem MachineItem { get; set; }
        public List<PDMS_EquipmentItem> MachineItems { get; set; }
        public DateTime? Service100Hrs { get; set; }
        public DateTime? Service500Hrs { get; set; }
        public DateTime? Service1000Hrs { get; set; }
        public DateTime? Service1500Hrs { get; set; }


        public string TypeOfWheelAssembly { get; set; }
        public PDMS_Material Material { get; set; }
        public string ChassisSlNo { get; set; }
        public string ESN { get; set; }
        public string Plant { get; set; }
        public string Dispatch { get; set; }
        public string SpecialVariants { get; set; }
        public string ProductionStatus { get; set; }
        public DateTime? VariantsFittingDate { get; set; }

        public DateTime? ManufacturingDate { get; set; }
        public string ManufacturingMonthYear
        {
            get
            {
                return ManufacturingDate == null ? "" : ((DateTime)ManufacturingDate).Month + "" + ((DateTime)ManufacturingDate).Year;
            }
        }
        public string HorsePower { get; set; }

        public PDMS_EquipmentWarrantyType EquipmentWarrantyType { get; set; }
        public PDMS_EquipmentIbase Ibase { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public int? WarrantyHMR { get; set; }
    }
    [Serializable]
    public class PDMS_EquipmentItem
    {
        public long MachineItemID { get; set; }
        public string PartName { get; set; }
        public string PartModel { get; set; }
        public string PartSerialNo { get; set; }
    }


    [Serializable]
    public class PDMS_EquipmentWarrantyType
    {
        public int EquipmentWarrantyTypeID { get; set; }
        public string WarrantyType { get; set; }
        public int HMR { get; set; }
        public decimal Period { get; set; }
        public string TimeUnit { get; set; }
        public string BaseCategory { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class PEquipmentWarrantyTypeApproval
    {
        public long WarrantyTypeChangeID { get; set; }
        public PDMS_EquipmentWarrantyType WarrantyType { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public PUser RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public PUser ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Boolean? IsApproved { get; set; }
        public List<PEquipmentAttachedFile> AttachedFile { get; set; }
    }
    [Serializable]
    public class PEquipmentOwnershipChangeApproval
    {
        public long OwnershipChangeID { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public DateTime SoldDate { get; set; }
        public PUser RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public PUser ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Boolean? IsApproved { get; set; }
    }
    public class PWarrantyExpiryDateChangeApproval
    {
        public long WarrantyExpiryDateChangeID { get; set; } 
        public PDMS_EquipmentHeader Equipment { get; set; }
        public DateTime OldWarrantyExpiryDate { get; set; }
        public DateTime NewWarrantyExpiryDate { get; set; }
        public PUser RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        public PUser ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Boolean? IsApproved { get; set; }
    }
    [Serializable]
    public class PEquipmentAttachedFile
    {
        public long AttachedFileID { get; set; }
        public string FileName { get; set; }
        public byte[] AttachedFile { get; set; }
        public long FileSize { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }
        public long ReferenceID { get; set; }
        public string ReferenceName { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
    }

    public class PEquipmentWarranty_Insert
    {
        public long EquipmentHeaderID { get; set; }
        public int EquipmentClientID { get; set; }
        public int EquipmentWarrantyTypeID { get; set; }
        public long CustomerID { get; set; }
        public DateTime OldExpiryDate { get; set; }
        public DateTime NewExpiryDate { get; set; }
        public List<PEquipmentAttachedFilee_Insert> AttachedFile { get; set; }
        public DateTime SoldDate { get; set; }
        public DateTime WarrantyStartDate { get; set; }
        public DateTime WarrantyEndDate { get; set; }
        public int WarrantyHMR { get; set; }
    }
    public class PEquipmentAttachedFilee_Insert
    {
        public string FileName { get; set; }
        public byte[] AttachedFile { get; set; }
    }
    [Serializable]
    public class PEquipmentClient
    {
        public int EquipmentClientID { get; set; }
        public string Client { get; set; }
        public bool IsActive { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}