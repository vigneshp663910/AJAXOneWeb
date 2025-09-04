using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{

    public class PMaster
    {
    }
    [Serializable]
    public class PBankName
    {
        public int BankNameID { get; set; }
        public string BankName { get; set; }
    }
    [Serializable]
    public class PEffortType
    {
        public int EffortTypeID { get; set; }
        public string EffortType { get; set; }
    }
    [Serializable]
    public class PExpenseType
    {
        public int ExpenseTypeID { get; set; }
        public string ExpenseType { get; set; }
    }


    [Serializable]
    public class PRelation
    {
        public int RelationID { get; set; }
        public string Relation { get; set; }
    } 
    [Serializable]
    public class PMake
    {
        public int MakeID { get; set; }
        public string Make { get; set; }
        public Boolean IsActive { get; set; }
    }
    [Serializable]
    public class PProductType
    {
        public int ProductTypeID { get; set; }
        public string ProductType { get; set; }
        public PDMS_Division Division { get; set; }

    }
    [Serializable]
    public class PProduct
    {
        public int ProductID { get; set; }
        public string Product { get; set; }
        public PMake Make { get; set; }
        public PProductType ProductType { get; set; }
    }
    [Serializable]
    public class PProductDrawing
    {
        public long ProductDrawingID { get; set; }
        public PProduct Product { get; set; }
        public PProductDrawingType ProductDrawingType { get; set; }
        public string DrawingDescription { get; set; }
        public byte[] AttachedFile { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public bool IsActive { get; set; }
    }
    [Serializable]
    public class PProductDrawingType
    {
        public int ProductDrawingTypeID { get; set; }
        public string ProductDrawingTypeName { get; set; }
    }
    [Serializable]
    public class PImportance
    {
        public int ImportanceID { get; set; }
        public string Importance { get; set; }
    }
    [Serializable]
    public class PPreSaleStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }

    [Serializable]
    public class PPriceGroup
    {
        public Int32 PriceGroupID { get; set; }
        public string PriceGroupCode { get; set; }
        public string Description { get; set; }
    }

    [Serializable]
    public class PIncoterms
    {
        public Int32 IncoTermsID { get; set; }
        public string IncoTerms { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class PPaymentTerms
    {
        public Int32 PaymentTermsID { get; set; }
        public string PaymentTerms { get; set; }
        public string Description { get; set; }
    }
    [Serializable]
    public class PAjaxOneStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }
    [Serializable]
    public class PAttachedFile_Azure
    {
        public long AttachedFileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] AttachedFile { get; set; }
    }
    [Serializable]
    public class PProductSpecification
    {
        public int ProductSpecificationID { get; set; }
        public PProduct Product { get; set; }
        public string SpecificationText { get; set; }
        public string SpecificationDescription { get; set; }
        public int OrderByNo { get; set; }
        public bool IsActive { get; set; }
    }
    [Serializable]
    public class PStatusItem
    {
        public int StatusItemID { get; set; }
        public string StatusItem { get; set; }
    }
    [Serializable]
    public class PICTicketMttrEscalationMatrix
    {
        public int EscalationMatrixID { get; set; }
        public string Region { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string ToMailID { get; set; }
        public string CcMailID { get; set; }
        public string EscalationHours { get; set; }
        public bool IsActive { get; set; }
        public PUser CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public PUser ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    [Serializable]
    public class PICTicketMttrEscalationMatrix_Insert
    {
        public int EscalationMatrixID { get; set; }
        public string Region { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string ToMailID { get; set; }
        public string CcMailID { get; set; }
        public string EscalationHours { get; set; }
        public bool IsActive { get; set; }
    }

    [Serializable]
    public class PInvoiceAdress
    {
        public long ID { get; set; }
        public string SupplierGSTIN { get; set; }
        public string Supplier_addr1 { get; set; }
        public string Supplier_addr2 { get; set; }
        public string SupplierLocation { get; set; }
        public string SupplierPincode { get; set; }
        public string SupplierStateCode { get; set; }

        public string BuyerName { get; set; }
        public string BuyerGSTIN { get; set; }
        public string BuyerStateCode { get; set; }
        public string Buyer_addr1 { get; set; }
        public string Buyer_addr2 { get; set; }
        public string Buyer_loc { get; set; }
        public string BuyerPincode { get; set; }
    }
}
