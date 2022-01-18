using Properties;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SapIntegration
{
    public class SMaterial
    {
        public List<PDMS_Material> getMaterial(string MaterialCode)
        {
            List<PDMS_Material> Materials = new List<PDMS_Material>();
            PDMS_Material Material = null;
            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_MATERIALS_GET");
            tagListBapi.SetValue("MATERIALCODE", MaterialCode);
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("It_MARA"); 
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                Material = new PDMS_Material();
                Material.MaterialCode = tagTable.CurrentRow.GetString(1);
                Material.MaterialDescription = tagTable.CurrentRow.GetString(2);
              
                Materials.Add(Material);
            }
            return Materials;
        }
        public List<PDMS_Material> getMaterialIntegration()
        {
            List<PDMS_Material> Materials = new List<PDMS_Material>();
            PDMS_Material Material = null;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZMM_BAPI_DMS_GET");
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("IT_MAT");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                Material = new PDMS_Material();
                Material.MaterialCode = tagTable.CurrentRow.GetString("MATERIAL");
                Material.ValidFrom= Convert.ToDateTime(tagTable.CurrentRow.GetString("VALID_FROM"));
                Material.ValidTo = Convert.ToDateTime(tagTable.CurrentRow.GetString("VALID_TO"));
                Material.MaterialDescription = tagTable.CurrentRow.GetString("MATERIAL_DESC");
                Material.MaterialType = tagTable.CurrentRow.GetString("MATERIAL_TYPE");
                Material.MaterialGroup = tagTable.CurrentRow.GetString("MATERIAL_GROUP");
                Material.Model = new PDMS_Model()
                {
                    ModelCode = tagTable.CurrentRow.GetString("MATERIAL_GROUP"),
                    Model = "",
                    Division = new PDMS_Division()
                    {
                        DivisionCode = "",
                        DivisionDescription = ""
                    },
                };
                Material.SubCategory = tagTable.CurrentRow.GetString("SUB_CATEGORY");
                Material.GrossWeight = tagTable.CurrentRow.GetDecimal("GROSS_WEIGHT");
                Material.NetWeight = tagTable.CurrentRow.GetDecimal("NET_WEIGHT");
                Material.BaseUnit = tagTable.CurrentRow.GetString("BASE_UNIT");
                Material.SerialProfile = tagTable.CurrentRow.GetString("SERIAL_PROFILE");
                Material.MaterialDivision = tagTable.CurrentRow.GetString("MATERIAL_DIVISION");
                Material.HSN = tagTable.CurrentRow.GetString("HSN_SAC");
                Material.IsActive = (tagTable.CurrentRow.GetString("ACTIVE")=="X")?false:true;
                Materials.Add(Material);
            }
            return Materials;
        }
        public List<PSupersede> getMaterialSupersedeIntegration()
        {
            List<PSupersede> Supersedes = new List<PSupersede>();
            PSupersede Supersede = null;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZMM_BAPI_DMS_GET");
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("IT_SC");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                Supersede = new PSupersede();
                Supersede.Material = tagTable.CurrentRow.GetString("MATERIAL");
                Supersede.MaterialDescription = tagTable.CurrentRow.GetString("SSMATNR");
                Supersede.Description = tagTable.CurrentRow.GetString("DESCRIPTION");
                Supersede.ValidFrom = Convert.ToDateTime(tagTable.CurrentRow.GetString("VALID_FROM"));
                Supersede.ValidTo = Convert.ToDateTime(tagTable.CurrentRow.GetString("VALID_TO"));
                Supersede.IsActive = (tagTable.CurrentRow.GetString("ACTIVE") == "X") ? false : true;
                Supersedes.Add(Supersede);
            }
            return Supersedes;
        }
        public List<PDMS_Material> getMaterialDetails(string MaterialCode)
        {
            List<PDMS_Material> Materials = new List<PDMS_Material>();
            PDMS_Material Material = null;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_MATERIALS_GET");
            tagListBapi.SetValue("MATERIALCODE", MaterialCode);
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("It_MARA");
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                Material = new PDMS_Material();
                Material.MaterialCode = tagTable.CurrentRow.GetString(1);
                Material.MaterialDescription = tagTable.CurrentRow.GetString(2);
                Material.BaseUnit = tagTable.CurrentRow.GetString(1);
                Material.MaterialType = tagTable.CurrentRow.GetString(2);
                Material.MaterialGroup = tagTable.CurrentRow.GetString(1);
                Material.GrossWeight = Convert.ToDecimal(tagTable.CurrentRow.GetString(2));
                Material.NetWeight = Convert.ToDecimal(tagTable.CurrentRow.GetString(1));
                Material.WeightUnit = tagTable.CurrentRow.GetString(2);
                Material.HSN = tagTable.CurrentRow.GetString(1);
                Material.TaxPercentage = Convert.ToDecimal(tagTable.CurrentRow.GetString(2));
                Material.MaterialCode = tagTable.CurrentRow.GetString(1);
                Material.CurrentPrice = Convert.ToDecimal(tagTable.CurrentRow.GetString(2));
                Materials.Add(Material);
            }
            return Materials;
        }
        public PDMS_ServiceMaterial getMaterialTax(string Customer, string Vendor, string OrderType, int Item, string MaterialCode, decimal Quantity, string IV_SEC_SALES, string PRICEDATE,Boolean IsWarrenty)
        {
            PDMS_ServiceMaterial Material = new PDMS_ServiceMaterial(); 

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZSIMULATE_SO");
            tagListBapi.SetValue("IV_SEC_SALES", IV_SEC_SALES);
            IRfcStructure IS_SO_HEAD = tagListBapi.GetStructure("IS_SO_HEAD");
            IS_SO_HEAD.SetValue("CUSTOMER", Customer.Trim().PadLeft(10, '0'));
            IS_SO_HEAD.SetValue("ORDER_TYPE", OrderType);
            IS_SO_HEAD.SetValue("VENDOR", Vendor.Trim().PadLeft(10, '0'));
            IS_SO_HEAD.SetValue("PRICEDATE", PRICEDATE);
            
            IRfcTable IT_SO_ITEMS = tagListBapi.GetTable("IT_SO_ITEMS");

            long n;
            if (long.TryParse(MaterialCode, out n))
            {
                MaterialCode= MaterialCode.PadLeft(18, '0');
            }

            IT_SO_ITEMS.Append();
            IT_SO_ITEMS.SetValue("ITEM_NO", 1);
            IT_SO_ITEMS.SetValue("MATERIAL", MaterialCode);
            IT_SO_ITEMS.SetValue("QUANTITY", Quantity);


            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable tagTable = tagListBapi.GetTable("IT_SO_COND");
            IRfcStructure ES_ERROR = tagListBapi.GetStructure("ES_ERROR");
            string ConditionType;
            for (int i = 0; i < tagTable.RowCount; i++)
            {
                tagTable.CurrentIndex = i;
                ConditionType = tagTable.CurrentRow.GetString("COND_TYPE");
                if ((ConditionType == "ZOSG") || (ConditionType == "JOSG"))
                {
                    Material.SGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                    Material.SGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                }
                else if (ConditionType == "ZOIG")
                {
                    Material.IGST = Convert.ToInt32(Convert.ToDecimal(tagTable.CurrentRow.GetString("PERCENTAGE")));
                    Material.IGSTValue = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                }
                else if ((ConditionType == "ZPRP") || (ConditionType == "ZASS"))
                {
                    if (IsWarrenty)
                    {
                        if (ConditionType == "ZASS")
                            Material.BasePrice = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    }
                    else
                    {
                        if (ConditionType == "ZPRP")
                            Material.BasePrice = Convert.ToDecimal(tagTable.CurrentRow.GetString("VALUE"));
                    }
                }
            }
            return Material;
        }
        public List<PDMS_Material> getAllMaterialFromEccSap(string MaterialCode)
        {
            List<PDMS_Material> Materials = new List<PDMS_Material>();
            PDMS_Material Material = null;

            IRfcFunction tagListBapi = SAP.RfcRep().CreateFunction("ZBAPI_GET_ALL_MATERIALS");
            tagListBapi.SetValue("MATERIALCODE", MaterialCode);
            tagListBapi.Invoke(SAP.RfcDes());
            IRfcTable IT_MARA_NEW = tagListBapi.GetTable("IT_MARA_NEW");
            IRfcTable IT_MARC = tagListBapi.GetTable("IT_MARC");
            IRfcTable IT_MARA = tagListBapi.GetTable("IT_MARA");
            Dictionary<string, string> Hsn = new Dictionary<string, string>();
            for (int i = 0; i < IT_MARC.RowCount; i++)
            {
                IT_MARC.CurrentIndex = i;
                string Mat = IT_MARC.CurrentRow.GetString("MATNR");
                if (!Hsn.Keys.Contains(Mat))
                {
                    Hsn.Add(Mat, IT_MARC.CurrentRow.GetString("STEUC"));
                }
            }

            Dictionary<string, string> MDescription = new Dictionary<string, string>();
            for (int i = 0; i < IT_MARA.RowCount; i++)
            {
                IT_MARA.CurrentIndex = i;
                string Mat = IT_MARA.CurrentRow.GetString(1);
                if (!MDescription.Keys.Contains(Mat))
                {
                    MDescription.Add(Mat, IT_MARA.CurrentRow.GetString(2));
                }
            }

            for (int i = 0; i < IT_MARA_NEW.RowCount; i++)
            {
                IT_MARA_NEW.CurrentIndex = i;
                Material = new PDMS_Material();
                Material.MaterialCode = IT_MARA_NEW.CurrentRow.GetString("MATNR");
               // Material.MaterialDescription = IT_MARA_NEW.CurrentRow.GetString("MAKTX");
                Material.MaterialDescription = MDescription.Keys.Contains(Material.MaterialCode) ? MDescription[Material.MaterialCode] : "";

                Material.BaseUnit = IT_MARA_NEW.CurrentRow.GetString("MEINS");
                Material.MaterialType = IT_MARA_NEW.CurrentRow.GetString("MTART");
                Material.MaterialGroup = IT_MARA_NEW.CurrentRow.GetString("MATKL");
                Material.GrossWeight = Convert.ToDecimal( IT_MARA_NEW.CurrentRow.GetString("BRGEW"));
                Material.NetWeight = Convert.ToDecimal(IT_MARA_NEW.CurrentRow.GetString("NTGEW"));
                Material.WeightUnit = IT_MARA_NEW.CurrentRow.GetString("GEWEI");
                Material.HSN = Hsn.Keys.Contains(Material.MaterialCode) ? Hsn[Material.MaterialCode] : "";  
              //  Material.TaxPercentage = Convert.ToDecimal(IT_MARA_NEW.CurrentRow.GetString(""));
              //  Material.Product = IT_MARA_NEW.CurrentRow.GetString("");
              //  Material.ProductGroup = IT_MARA_NEW.CurrentRow.GetString("");
                Materials.Add(Material);  
            }
            return Materials;
        }
    
    }
}
