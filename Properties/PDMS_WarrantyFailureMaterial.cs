using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_WarrantyFailureMaterial
    {
        public long DeliveryChallanID { get; set; }
        public long DCTemplateID { get; set; }
        public string DCTemplateName { get; set; }

        public string DeliveryChallanNumber { get; set; }
        public DateTime? DeliveryChallanDate { get; set; }
        public string DeliveryTo { get; set; }
        public string TransporterName { get; set; }
        public string DocketDetails { get; set; }
        public string PackingDetails { get; set; }

        public PDMS_Dealer Dealer { get; set; }
        public PUser CreatedBy { get; set; }
        public PDMS_WarrantyFailureMaterialItem FailureMaterialItem { get; set; }
        public List<PDMS_WarrantyFailureMaterialItem> FailureMaterialItems { get; set; }
    }
    [Serializable]
    public class PDMS_WarrantyFailureMaterialItem
    {
        public long DCTemplateItemID { get; set; }
        public long DeliveryChallanItemID { get; set; }
        public PDMS_WarrantyInvoiceHeader Invoice { get; set; }
       // public PDMS_WarrantyInvoiceItem InvoiceItem { get; set; }
        public Boolean IsAcknowledged { get; set; }
        public Boolean IsCanceled { get; set; }
        public string AcknowledgeStatus {
            get
            {
                if (IsAcknowledged == true)
                {
                    return "Acknowledged";
                }
                else if (IsCanceled == true)
                {
                    return "Canceled";
                }
                return "Created";
            }
            
        }
        public Boolean _IsAcknowledged
        {
            get
            {
                if (IsAcknowledged == true)
                {
                    return false;
                }
                else if (IsCanceled == true)
                {
                    return false;
                }
                return true;
            }           
        }
        public DateTime? AcknowledgedOn { get; set; }
    }
}