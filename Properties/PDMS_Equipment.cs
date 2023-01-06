using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_Equipment
    {
        public long EquipmentHeaderID { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_Model EquipmentModel { get; set; }
        public string EquipmentSerialNo { get; set; }

        public string EngineModel { get; set; }
        public string EngineSerialNo { get; set; }
        public DateTime? DispatchedOn { get; set; }
        public DateTime CommissioningOn { get; set; }

        public DateTime WarrantyExpiryDate { get; set; }
        public DateTime CurrentHMRDate { get; set; }
        public int CurrentHMRValue { get; set; }
        public string CounterObjectID { get; set; }


        public Boolean? IsRefurbished { get; set; }
        public string RefurbishedBy { get; set; }
        public DateTime? RFWarrantyStartDate { get; set; }
        public DateTime? RFWarrantyExpiryDate { get; set; }
        public Boolean? IsAMC { get; set; }
        public DateTime? AMCStartDate { get; set; }
        public DateTime? AMCExpiryDate { get; set; }
        public int LastICTicketID { get; set; }
        public string TypeOfWheelAssembly { get; set; }
        public PDMS_Material Material { get; set; }
        public string ChassisSlNo { get; set; }
        public string ESN { get; set; }
        public string Plant { get; set; }
        public string SpecialVariants { get; set; }
        public string ProductionStatus { get; set; }
        public DateTime? VariantsFittingDate { get; set; }
        public string ManufacturingDate { get; set; }

        public PDMS_EquipmentIbase Ibase { get; set; }
        public PDMS_EquipmentWarrantyType EquipmentWarrantyType { get; set; }
    }
    [Serializable]
    public class PDMS_Model
    {
        public int ModelID { get; set; }
        public string ModelCode { get; set; }
        public string Model { get; set; }
        public string ModelDescription { get; set; }
        public string ModelCodeModelDescription { get; set; }
        public PDMS_Division Division { get; set; }
    }
    [Serializable]
    public class PDMS_Division
    {
        public int DivisionID { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionDescription { get; set; }
        public string UOM { get; set; }
        public string WarrantyDeliveryHours { get; set; }
        public Boolean IsActive { get; set; }
    }

    //public class PDMS_DispatchType
    //{
    //    public int DispatchTypeID { get; set; }
    //    public string DispatchType { get; set; } 
    //}

    [Serializable]
    public class PDMS_EquipmentIbase
    {
        public long EquipmentIbaseID { get; set; }
        public long EquipmentHeaderID { get; set; }
        public string InstalledBaseNo { get; set; }
        public string IBaseLocation { get; set; }
        public PDMS_Region MajorRegion { get; set; }

        public int? Item { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ProductCode { get; set; }
        public DateTime? IBaseCreatedOn { get; set; }
        public PDMS_Customer ShipToParty { get; set; }
        public PDMS_Dealer ShipToPartyDealer { get; set; }
        public PDMS_Dealer SoleToDealer { get; set; }
        public DateTime? WarrantyStart { get; set; }
        public DateTime? WarrantyEnd { get; set; }
        public int? FinancialYearOfDispatch { get; set; }
        public PDMS_Customer Buyer1st { get; set; }
        public PDMS_Customer Buyer2nd { get; set; }
        public Boolean IBaseInactive { get; set; }
        public DateTime? UpdateOn { get; set; }         
    }
}
