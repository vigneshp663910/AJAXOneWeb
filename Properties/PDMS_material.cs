using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
     [Serializable]
    public class PDMS_Material
    {
        public long MaterialID { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialCodeWithZero
        {
            get
            {
                if (string.IsNullOrEmpty(MaterialCode))
                    return "";
                long n;
                if (long.TryParse(MaterialCode, out n))
                {
                    return MaterialCode.PadLeft(18, '0');
                }
                return MaterialCode;
            }
            set
            {
                MaterialCode = value;
            }
        }
        public string MaterialCodeWithOutZero
        {
            get
            {
                if (string.IsNullOrEmpty(MaterialCode))
                    return "";
                long n;
                if (long.TryParse(MaterialCode, out n))
                {
                    return MaterialCode.TrimStart('0');
                }
                return MaterialCode;
            }
            set
            {
                MaterialCode = value;
            }
        }
        public string MaterialDescription { get; set; }
        public string MaterialCode_MaterialDescription { get; set; }
        public string MaterialSerialNumber { get; set; }
        public string BaseUnit { get; set; }
        public string MaterialType { get; set; }
        public string MaterialGroup { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public string WeightUnit { get; set; }
        public string MaterialDivision { get; set; }
        public string HSN { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal CST {
            get
            { 
                return TaxPercentage;
            } 
        }
        public decimal SST
        {
            get
            {
                return TaxPercentage;
            }
        }
        public decimal GST
        {
            get
            {
                return TaxPercentage * 2;
            }
        }
        public decimal CurrentPrice { get; set; }
        public Boolean IsMainServiceMaterial { get; set; } 

        public PSupersede Supersede { get; set; }
        public PRoqDoq RoqDoq { get; set; }
        public PBin Bin { get; set; }

        public string Product { get; set; }
        public string ProductGroup { get; set; }
    }

     [Serializable]
     public class PSupersede
   {
       public string Material { get; set; }
       public string MaterialDescription { get; set; }
       public string Description { get; set; }
       public DateTime ValidFrom { get; set; }
       public DateTime ValidTo { get; set; }
   }

     [Serializable]
     public class PRoqDoq
    {
        public string SeasonCode { get; set; }
        public DateTime SeasonValidFrom { get; set; }
        public DateTime SeasonValidTo { get; set; }
        public string Office { get; set; }
        public PDMS_Dealer Dealer { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public string Status { get; set; }
        public decimal ReqRoqQty { get; set; }
        public decimal DoqQty { get; set; }
        public string OfficeDesc { get; set; }
        public string Unit { get; set; }
        public decimal RoqQty { get; set; }
        
    }
     [Serializable]
     public class PBin
    {
        public PDMS_Dealer Dealer { get; set; }
        public string Office { get; set; }
        public string Location { get; set; }
        public string BinID { get; set; }
        
    }
}
