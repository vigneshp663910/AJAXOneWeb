using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_PrimaryMCSalesOrder
    {

        public long PrimaryMCSalesOrderID { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }
        public PDMS_EquipmentHeader Equipment { get; set; }

        public string PurchaseOrder { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public PDMS_Address ShipTo { get; set; }
        public string FinacierCode { get; set; }
        public int MachineQuantity { get; set; }
        public decimal? BasicPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? InvoiceValue { get; set; }
        public string DoNumber { get; set; }
        public DateTime? DoDate { get; set; }
        public decimal? DoAmount { get; set; }
        public PDMS_PaymentTerm TermsOfPayment { get; set; }
        public decimal? MarginMoney { get; set; }
        public PDMS_DiscountType DiscountType { get; set; }
        public PIncoTerms IncoTerms { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? FinancierAmount { get; set; }
        public decimal? FreightAmount { get; set; }
        public string BenificiaryOfDO { get; set; }
        public decimal? SubventionAmount { get; set; }
        public PDMS_MainApplication Usage { get; set; }

        public DateTime? TRDate { get; set; }
        public string HorsePower { get; set; }
        public string RetailCustomer { get; set; }
        public string ConsolidationInvoicePrint { get; set; }
        public string Hypothecation { get; set; }
        public string BackToBackDoEndorsedToAjax { get; set; }
        public string SpecialRequirements { get; set; }
        public string FocServiceKit { get; set; }
        public string FocWheelAssy { get; set; }
        public string FocExtensionChutes { get; set; }
        public string FocOthers { get; set; }
        public PDMS_SourceOfEnquiry SourceOfEnquiry { get; set; }
        public string ReasonForOrderConversion { get; set; }
        public string CustomerType { get; set; }
        public string Profile { get; set; }
        public string Size { get; set; }
        public string OwnershipPattern { get; set; }
        public string NameOfTheProject { get; set; }
        public string TransportationAndInsurance { get; set; }
    }
}
