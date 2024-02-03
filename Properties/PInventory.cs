using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{ 
    public class PInventory
    { 
    }
    [Serializable]
    public class PInitialStock
    {
        public int InitialStockID { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_Material Material { get; set; }
        public Decimal Quantity { get; set; }
        public PUser PostedBy { get; set; }
        public DateTime PostedOn { get; set; }
    }
    [Serializable]
    public class PInitialStock_Post
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public int OfficeID { get; set; }
        public string MaterialCode { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal PerUnitPrice { get; set; }
    }
    [Serializable]
    public class PDealerStock
    {
        public int InitialStockID { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }
        public PDMS_Material Material { get; set; }
        public Decimal OnOrderQty { get; set; }
        public Decimal TransitQty { get; set; }
        public Decimal UnrestrictedQty { get; set; }
        public Decimal RestrictedQty { get; set; }
        public Decimal BlockedQty { get; set; }
        public Decimal ReservedQty { get; set; } 
    }

    [Serializable]
    public class PPhysicalInventoryPosting
    {
        public long PhysicalInventoryPostingID { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public PDealer Dealer { get; set; }
        public PDMS_DealerOffice DealerOffice { get; set; }         
        public DateTime PostingDate { get; set; }
        public PUser CreatedBy { get; set; }
        public PAjaxOneStatus InventoryPostingType { get; set; }
        public List<PPhysicalInventoryPostingItem> Items { get; set; }
    }
    [Serializable]
    public class PPhysicalInventoryPostingItem
    {
        public long PhysicalInventoryPostingItemID { get; set; }
        public long PhysicalInventoryPostingID { get; set; }
        public PDMS_Material Material { get; set; }
        public Decimal SystemStock { get; set; }
        public Decimal PhysicalStock { get; set; }
        public Boolean IsPosted { get; set; }
    }
    [Serializable]
    public class PPhysicalInventoryPosting_Post
    {
        public int ID { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public int DealerID { get; set; }
        public int OfficeID { get; set; }
        public int PostingTypeID { get; set; }
        public string MaterialCode { get; set; }
        public int MaterialID { get; set; }
        public Decimal SystemStock { get; set; }
        public Decimal PhysicalStock { get; set; }
    }
}
