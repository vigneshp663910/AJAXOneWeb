using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    [Serializable]
    class PECatalogue
    {
    }
    [Serializable]
    public class PSpcProductGroup
    {
        public int SpcProductGroupID { get; set; }
        public string PGCode { get; set; }
        public string PGDescription { get; set; }
        public string PGSCode { get; set; }
        public PDMS_Division Division { get; set; }
        public Boolean IsActive { get; set; }
        public string Remarks { get; set; }
        public string PGSCodePGDescription
        {
            get
            {
                return PGCode + " - " + PGDescription;
            }
        }
    }
    [Serializable]
    public class PSpcMaterialGroup
    {
        public int SpcMaterialGroupID { get; set; }
        public string MaterialGroup { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string MType { get; set; }
        public string MTypeDescription
        {
            get
            {
                if (MType == "C")
                {
                    return "Catalogue";
                }
                else if (MType == "B")
                {
                    return "Both";
                }
                else if (MType == "M")
                {
                    return "MBR";
                }
                return "";
            }
        }
        public string MaterialGroupDescription1
        {
            get
            {
                return MaterialGroup + " - " + Description1;
            }
        }
        public Boolean IsActive { get; set; } 
    }
    [Serializable]
    public class PSpcModel
    {
        public int SpcModelID { get; set; }
        public int SlNo { get; set; }
        public string SpcModelCode { get; set; }
        public string SpcModel { get; set; }
        public PSpcProductGroup ProductGroup { get; set; }
        public PSpcMaterialGroup MaterialGroup { get; set; }
        public Boolean IsActive { get; set; }
        public string Purpose { get; set; }
        public Boolean IsPublish { get; set; }
        public string Remarks { get; set; }
        public string SpcModelCodeWithDescription
        {
            get
            {
                return SpcModelCode + " - " + SpcModel;
            }
        } 
     
        public string PurposeDetail
        {
            get
            {
                if (Purpose == "C")
                {
                    return "Catalogue";
                }
                else if (Purpose == "B")
                {
                    return "Both";
                }
                else if (Purpose == "M")
                {
                    return "MBR";
                }
                return "";
            }
        } 
    }

    [Serializable]
    public class PSpcMaterial
    {
        public long SpcMaterialID { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
    }
    [Serializable]
    public class PSpcAssembly
    {
        public int SpcAssemblyID { get; set; }
        public int SlNo { get; set; }
        public string AssemblyCode { get; set; }
        public string AssemblyDescription { get; set; }
        public string FileName { get; set; }
        public string Remarks { get; set; }
        public string AssemblyType { get; set; }
        public Boolean IsActive { get; set; }
        public PSpcModel SpcModel { get; set; }
    }
    
    [Serializable]
    public class PSpcAssemblyPartsCoOrdinate
    {
        public long SpcAssemblyPartsCoOrdinateID { get; set; }
        public PSpcAssembly Assembly { get; set; }
        public PSpcMaterial Material { get; set; }
        public int Qty { get; set; }
        public int? X_CoOrdinate { get; set; }
        public int? Y_CoOrdinate { get; set; }
        public int Number { get; set; }
        public string Flag { get; set; }
        public string Remarks { get; set; }
    }

   
    [Serializable]
    public class PSpcAssemblyPartsCoOrdinate_Insert
    {
        public long? SpcAssemblyPartsCoOrdinateID { get; set; }
        public int? SpcAssemblyID { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
        public int? Qty { get; set; }
        public int? X_CoOrdinate { get; set; }
        public int? Y_CoOrdinate { get; set; }
        public int? Number { get; set; }
        public string Flag { get; set; }
        public string Remarks { get; set; }
    }

    [Serializable]
    public class PSpcAssemblyPartsCart_insert
    {
        public long SpcAssemblyPartsCartID { get; set; }
        public int SpcMaterialID { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
        public int Qty { get; set; } 
        public int Number { get; set; }
        public string Flag { get; set; }
        public string Remarks { get; set; }
    }

    [Serializable]
    public class PspcCart_Insert
    {
       
        public int DealerID { get; set; }
        public int OfficeID { get; set; }
        public int ItemCount { get; set; }
        public string Remarks { get; set; }
        public List<PspcCartItem_Insert> CartItem { get; set; }
    }
    [Serializable]
    public class PspcCartItem_Insert
    {
        public int SpcAssemblyID { get; set; }
        public int SpcMaterialID { get; set; }
        public int PartQty { get; set; } 
    }

    [Serializable]
    public class PspcCart
    {
        public long spcCartID { get; set; }
        public string CartOrderNo { get; set; }
        public DateTime CartOrderDate { get; set; }        
        public PDealer Dealer { get; set; }
        public int ItemCount { get; set; }
        public string Remarks { get; set; }
        public List<PspcCartItem> CartItem { get; set; }
    }
    [Serializable]
    public class PspcCartItem
    {
        public long spcCartItemID { get; set; }
        public PSpcAssembly Assembly { get; set; }
        public PSpcMaterial SpcMaterial { get; set; }
        public int PartQty { get; set; }
    }

    [Serializable]
    public class PSpcModel_Insert
    {
        public int? SpcModelID { get; set; }
        public int SlNo { get; set; }
        public int SpcProductGroupID { get; set; }
        public int SpcMaterialGroupID { get; set; }
        public string SpcModelCode { get; set; }
        public string SpcModel { get; set; }      
        public string Purpose { get; set; }        
        public string Remarks{ get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsPublish { get; set; }
    }
    [Serializable]
    public class PSpcAssembly_Insert
    {
        public int? SpcAssemblyID { get; set; }
        public int ModelID { get; set; }
        public int SlNo { get; set; }
        public string AssemblyCode { get; set; }
        public string AssemblyDescription { get; set; }
        public string AssemblyType { get; set; }
        public string Remarks { get; set; }
        public Boolean IsActive { get; set; } 
    }

    [Serializable]
    public class PSpcCartTemp_Insert
    {
        public long SpcAssemblyPartsCoOrdinateID { get; set; }
        public int SpcAssemblyID { get; set; }
        public int SpcMaterialID { get; set; }
        public int PartQty { get; set; }
        public string Remarks { get; set; }


        public string Number { get; set; }
        public string Flag { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
        public string AssemblyCode { get; set; }
        public string AssemblyDescription { get; set; } 
    }
    
}
