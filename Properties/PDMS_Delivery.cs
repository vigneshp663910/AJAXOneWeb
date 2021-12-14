using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_DeliveryHeader
    {
        public long DeliveryID { get; set; }
        public string DeliveryNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string SoNumber { get; set; } 
        public PDMS_Dealer Dealer { get; set; }
        public PDMS_Customer Customer { get; set; }

        public string ICTicketID { get; set; }
        public DateTime ICTicketDate { get; set; }

        public int? HMR { get; set; } 
        public string MachineSerialNumber { get; set; }
        public string Model { get; set; }  
        public DateTime? DateOfCommissioning { get; set; } 
        public string TSIRNumber { get; set; } 
        public int GrandTotal { get; set; }

        public string TransportationThrough { get; set; }
        public DateTime? TransportationDate { get; set; }
        public string VehicleNumber { get; set; }

        public List<PDMS_DeliveryItem> DeliveryItems { get; set; }
        public PDMS_DeliveryType DeliveryType { get; set; }

        public string AddressID { get; set; }
    }
    [Serializable]
    public class PDMS_DeliveryItem
    {

        public long WarrantyClaimInvoiceItemID { get; set; }
      
        public string Material { get; set; }
        public string MaterialDesc { get; set; }
        public string HSNCode { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxableValue { get; set; }
        public int CGST { get; set; }
        public int SGST { get; set; }
        public int IGST { get; set; }
        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }
        public decimal TaxPercentage { get; set; }
    }

    [Serializable]
    public class PDMS_DeliveryType
    {
        public int DeliveryTypeID { get; set; }
        public string DeliveryType { get; set; }
    }
}