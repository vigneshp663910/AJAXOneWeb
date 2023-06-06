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
        public string MaterialCode_MaterialDescription {
            get
            { 
                return MaterialCode +" "+ MaterialDescription;
            } 
        }
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
        public decimal CST
        {
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
        public PDealerBinLocation Bin { get; set; }

        public string Product { get; set; }
        public string ProductGroup { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public PDMS_Model Model { get; set; }
        public string SubCategory { get; set; }
        public string SerialProfile { get; set; }
        public bool IsActive { get; set; } 
    }
    [Serializable]
    public class PMaterial
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
        public string MaterialCode_MaterialDescription
        {
            get
            {
                return MaterialCode + " " + MaterialDescription;
            }
        }
        public string BaseUnit { get; set; }
        public string MaterialType { get; set; }
        public string MaterialGroup { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public string WeightUnit { get; set; }
        public string MaterialDivision { get; set; }
        public string HSN { get; set; }

        public decimal CurrentPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxablePrice { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }

        public decimal CGSTValue { get; set; }
        public decimal SGSTValue { get; set; }
        public decimal IGSTValue { get; set; }

        public Boolean IsMainServiceMaterial { get; set; }
        public string Product { get; set; }
        public string ProductGroup { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public PDMS_Model Model { get; set; }
        public bool IsActive { get; set; }
    }
    [Serializable]
    public class PSupersede
    {
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
        public string Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsActive { get; set; }
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
    public class PMainModel
    {
        public int ModelMainID { get; set; }
        public string ModelMain { get; set; } 
    }

    [Serializable]
    public class PMaterialVariantType
    {
        public int VariantsTypeID { get; set; }
        public string VariantName { get; set; }
        public PMainModel MainModel { get; set; }
        public int CountToSelect { get; set; }  
        public bool IsActive { get; set; }
        public PDMS_Material Material { get; set; }
    }

    [Serializable]
    public class PMaterialVariantsMapping
    {
        public int MaterialVariantsMappingID { get; set; }
        public PMaterialVariantType VariantsType { get; set; }
        public PDMS_Material Material { get; set; } 
        public bool IsActive { get; set; }
    }
}
