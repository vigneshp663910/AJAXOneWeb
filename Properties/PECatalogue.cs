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
    public class PSpcAssembly
    {
        public int SpcAssemblyID { get; set; }
        public string AssemblyCode { get; set; }
        public string AssemblyDescription { get; set; }
        public string FileName { get; set; }
        public string Remarks { get; set; }
        public string AssemblyType { get; set; }
        public PDMS_Model Model { get; set; }
    }
    [Serializable]
    public class PSpcMaterial
    {
        public long SpcMaterialID { get; set; }
        public string Material { get; set; }
        public string MaterialDescription { get; set; }
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
    public class PSpcAssemblyImage_Insert
    {
        public int? SpcAssemblyID { get; set; }
        public string AssemblyCode { get; set; }
        public string AssemblyDescription { get; set; }
        public string FileName { get; set; }
        public string Remarks { get; set; }
        public string AssemblyType { get; set; }
        public int? ModelID { get; set; }
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
        public int SpcAssemblyID { get; set; }
        public int DealerID { get; set; }
        public int OfficeID { get; set; }
        public int ItemCount { get; set; }
        public string Remarks { get; set; }
        public List<PspcCartItem_Insert> CartItem { get; set; }
    }
    [Serializable]
    public class PspcCartItem_Insert
    { 
        public int SpcMaterialID { get; set; }
        public int PartQty { get; set; } 
    }

    [Serializable]
    public class PspcCart
    {
        public long spcCartID { get; set; }
        public string CartOrderNo { get; set; }
        public DateTime CartOrderDate { get; set; }
        public PSpcAssembly Assembly { get; set; }
        public PDealer Dealer { get; set; }
        public int ItemCount { get; set; }
        public string Remarks { get; set; }
        public List<PspcCartItem> CartItem { get; set; }
    }
    [Serializable]
    public class PspcCartItem
    {
        public long spcCartItemID { get; set; }
        public PSpcMaterial SpcMaterial { get; set; }
        public int PartQty { get; set; }
    }
}
