using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web.Script.Serialization;
namespace Business
{
    public class BDMS_Material
    {
        private IDataAccess provider;
        public BDMS_Material()
        {
            provider = new ProviderFactory().GetProvider();
        }


        public List<string> GetMaterialAutocomplete(string Material, string MaterialType, int? DivisionID)
        {
            List<string> Materials = new List<string>();
            try
            {
                DbParameter MaterialP;
                DbParameter MaterialTypeP;

                if (!string.IsNullOrEmpty(Material))
                    MaterialP = provider.CreateParameter("Material", Material, DbType.String);
                else
                    MaterialP = provider.CreateParameter("Material", null, DbType.String);

                if (!string.IsNullOrEmpty(MaterialType))
                    MaterialTypeP = provider.CreateParameter("MaterialType", MaterialType, DbType.String);
                else
                    MaterialTypeP = provider.CreateParameter("MaterialType", null, DbType.String);
                DbParameter DivisionIDP = provider.CreateParameter("DivisionID", DivisionID, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] { MaterialP, MaterialTypeP, DivisionIDP };

                using (DataSet DataSet = provider.Select("ZDMS_GetMaterialAutocomplete", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Materials.Add(Convert.ToString(dr["MaterialCode"]) + " " + Convert.ToString(dr["MaterialDescription"]));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Materials;
        }
        public List<PDMS_Material> GetMaterialAutocompleteN(string Material, string MaterialType, int? DivisionID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "";
            if (MaterialType == "DIEN")
            {
                endPoint = "Material/ServiceMaterialAutocomplete?Material=" + Material + "&DivisionID=" + DivisionID;
            }
            else if (MaterialType == "FERT")
            {
                endPoint = "Material/MaterialFinishedGoodsAutocomplete?Material=" + Material + "&MaterialType=" + MaterialType + "&DivisionID=" + DivisionID;
            }
            else
            {
                endPoint = "Material/MaterialAccessoriesAutocomplete?Material=" + Material + "&MaterialType=" + MaterialType + "&DivisionID=" + DivisionID;
            }
            return JsonConvert.DeserializeObject<List<PDMS_Material>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<string> GetMaterialServiceAutocomplete(string Material, string MaterialType, int? ServiceTypeID, int? Category1ID, Boolean IsMainServiceMaterial)
        {
            List<string> Materials = new List<string>();
            try
            {


                DbParameter MaterialP;
                DbParameter MaterialTypeP;

                if (!string.IsNullOrEmpty(Material))
                    MaterialP = provider.CreateParameter("Material", Material, DbType.String);
                else
                    MaterialP = provider.CreateParameter("Material", null, DbType.String);

                if (!string.IsNullOrEmpty(MaterialType))
                    MaterialTypeP = provider.CreateParameter("MaterialType", MaterialType, DbType.String);
                else
                    MaterialTypeP = provider.CreateParameter("MaterialType", null, DbType.String);


                DbParameter ServiceTypeIDP = provider.CreateParameter("ServiceTypeID", ServiceTypeID, DbType.Int32);
                DbParameter Category1IDP = provider.CreateParameter("Category1ID", Category1ID, DbType.Int32);

                DbParameter IsMainServiceMaterialP = provider.CreateParameter("IsMainServiceMaterial", IsMainServiceMaterial, DbType.Boolean);

                DbParameter[] Params = new DbParameter[5] { MaterialP, MaterialTypeP, ServiceTypeIDP, Category1IDP, IsMainServiceMaterialP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMaterialServiceAutocomplete", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Materials.Add(Convert.ToString(dr["MaterialCode"]) + " " + Convert.ToString(dr["MaterialDescription"]));
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Materials;
        }

        public PDMS_Material GetMaterialServiceByMaterialAndDescription(string MaterialAndDescription)
        {
            PDMS_Material Materials = new PDMS_Material();
            try
            {
                DbParameter MaterialAndDescriptionP = provider.CreateParameter("MaterialAndDescription", MaterialAndDescription, DbType.String);
                DbParameter[] Params = new DbParameter[1] { MaterialAndDescriptionP };
                using (DataSet DataSet = provider.Select("ZDMS_GetMaterialServiceByMaterialAndDescription", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Materials.MaterialID = Convert.ToInt64(dr["MaterialID"]);
                            Materials.MaterialCode = Convert.ToString(dr["MaterialCode"]);
                            Materials.MaterialDescription = Convert.ToString(dr["MaterialDescription"]);
                            //Materials.BaseUnit = Convert.ToString(dr["r_base_unit"]);
                            //Materials.MaterialType = Convert.ToString(dr["f_mat_type"]);
                            //Materials.MaterialGroup = Convert.ToString(dr["f_mat_grp"]);
                            //Materials.WeightUnit = Convert.ToString(dr["r_weight_unit"]);
                            //Materials.MaterialDivision = Convert.ToString(dr["r_material_division"]);
                            //Materials.GrossWeight = DBNull.Value == dr["r_gross_weight"] ? 0 : Convert.ToDecimal(dr["r_gross_weight"]);
                            //Materials.NetWeight = DBNull.Value == dr["r_net_weight"] ? 0 : Convert.ToDecimal(dr["r_net_weight"]);
                            //Materials.HSN = Convert.ToString(dr["r_hsn_id"]);
                            //Materials.TaxPercentage = DBNull.Value == dr["tax_percentage"] ? 0 : Convert.ToDecimal(dr["tax_percentage"]);
                            //Materials.CurrentPrice = DBNull.Value == dr["r_mrp"] ? 0 : Convert.ToDecimal(dr["r_mrp"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Materials;
        }

        public List<PDMS_Material> GetMaterialListSQL(int? MaterialID, string MaterialCode, int? DivisionID, int? ModelID, string IsActive)
        {
            string endPoint = "Material/Material?MaterialID=" + MaterialID + "&MaterialCode=" + MaterialCode
                + "&DivisionID=" + DivisionID + "&ModelID=" + ModelID + "&IsActive=" + IsActive;
            return JsonConvert.DeserializeObject<List<PDMS_Material>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }

        public List<PDMS_Material> GetMaterialSupersede(int? MaterialID, string MaterialCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            PDMS_Material SOI = new PDMS_Material();
            try
            {
                DbParameter MaterialIDP = provider.CreateParameter("MaterialID", MaterialID, DbType.Int32);
                DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
                DbParameter[] Params = new DbParameter[2] { MaterialIDP, MaterialCodeP };

                using (DataSet DataSet = provider.Select("GetMaterialSupersede", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {

                            SOI = new PDMS_Material();
                            SOI.MaterialCode = Convert.ToString(dr["MaterialCode"]);
                            SOI.MaterialDescription = Convert.ToString(dr["MaterialDescription"]);
                            SOI.Supersede = new PSupersede();
                            SOI.Supersede.Material = Convert.ToString(dr["SupersedeCode"]);
                            SOI.Supersede.MaterialDescription = Convert.ToString(dr["SupersedeMaterailDescription"]);
                            SOI.Supersede.ValidFrom = Convert.ToDateTime(dr["ValidFrom"]);
                            SOI.Supersede.ValidTo = Convert.ToDateTime(dr["ValidTo"]);
                            SOI.Supersede.Description = Convert.ToString(dr["SupersedeRemarks"]);
                            SOIs.Add(SOI);
                        }
                        return SOIs;
                        TraceLogger.Log(DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialSupersede", ex);
                throw ex;
            }
            return SOIs;
        }
        public PMaterial MaterialPriceFromSap(string Customer, string Vendor, string OrderType, int Item, string Material, decimal Quantity, string IV_SEC_SALES, string PriceDate, string IsWarrenty)
        {
            string endPoint = "Material/MaterialPriceFromSap?Customer=" + Customer + "&Vendor=" + Vendor + "&OrderType=" + OrderType + "&Item=" + Item
                + "&Material=" + Material + "&Quantity=" + Quantity + "&IV_SEC_SALES=" + IV_SEC_SALES + "&PriceDate=" + PriceDate + "&IsWarrenty=" + IsWarrenty;
            return JsonConvert.DeserializeObject<PMaterial>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PMaterialVariantType> GetMaterialVariantType(int? ProductTypeID)
        {
            try
            {
                string endPoint = "Material/GetMaterialVariantType?ProductTypeID=" + ProductTypeID;
                return JsonConvert.DeserializeObject<List<PMaterialVariantType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialVariantType", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialVariantType", ex);
                throw ex;
            }
        }
        public List<PMaterialVariantTypeMapping> GetMaterialByProductIDAndMaterialCategoryID(int ProductID, int MaterialCategoryID)
        {
            try
            {
                string endPoint = "Material/GetMaterialByProductIDAndMaterialCategoryID?ProductID=" + ProductID + "&MaterialCategoryID=" + MaterialCategoryID;
                return JsonConvert.DeserializeObject<List<PMaterialVariantTypeMapping>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialVariantTypeMappingProductID", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialVariantTypeMappingProductID", ex);
                throw ex;
            }
        }
        public Boolean InsertOrUpdateMaterialVariantType(PMaterialVariantType MVT, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            Boolean success = false;
            try
            {
                DbParameter VariantTypeIDP = provider.CreateParameter("VariantTypeID", MVT.VariantTypeID, DbType.Int32);
                DbParameter VariantNameP = provider.CreateParameter("VariantName", MVT.VariantName, DbType.String);
                DbParameter ProductTypeID = provider.CreateParameter("ProductTypeID", MVT.ProductType.ProductTypeID, DbType.Int32);
                DbParameter MaxToSelect = provider.CreateParameter("MaxToSelect", MVT.MaxToSelect, DbType.Int32);
                DbParameter IsActiveP = provider.CreateParameter("IsActive", MVT.IsActive, DbType.Boolean);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
                DbParameter[] Params = new DbParameter[7] { VariantTypeIDP, VariantNameP, ProductTypeID, MaxToSelect, IsActiveP, UserIDP, OutValue };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertOrUpdateMaterialVariantType", Params);
                    scope.Complete();
                }
                success = true;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("BDMS_Material", "InsertOrUpdateMaterialVariantType", e1);
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return success;
        }
        public List<PMaterialVariantTypeMapping> GetMaterialVariantTypeMapping(int? ProductTypeID, int? ProductID, int? VariantTypeID, string MaterialCode)
        {
            try
            {
                string endPoint = "Material/GetMaterialVariantTypeMapping?ProductTypeID=" + ProductTypeID + "&ProductID=" + ProductID + "&VariantTypeID=" + VariantTypeID + "&MaterialCode=" + MaterialCode;
                return JsonConvert.DeserializeObject<List<PMaterialVariantTypeMapping>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialVariantTypeMapping", sqlEx);
                throw sqlEx;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialVariantTypeMapping", ex);
                throw ex;
            }
        }
        public Boolean InsertOrUpdateMaterialVariantTypeMapping(PMaterialVariantTypeMapping MVTM, int UserID)
        {
            TraceLogger.Log(DateTime.Now);
            Boolean success = false;
            try
            {
                DbParameter MaterialVariantTypeMappingIDP = provider.CreateParameter("MaterialVariantTypeMappingID", MVTM.MaterialVariantTypeMappingID, DbType.Int32);
                DbParameter ProductIDP = provider.CreateParameter("ProductID", (MVTM.Product.ProductID == 0) ? (Int32?)null : MVTM.Product.ProductID, DbType.Int32);
                DbParameter VariantTypeIDP = provider.CreateParameter("VariantTypeID", MVTM.VariantType.VariantTypeID, DbType.Int32);
                DbParameter MaterialIDP = provider.CreateParameter("MaterialID", MVTM.Material.MaterialID, DbType.Int32);
                DbParameter IsActiveP = provider.CreateParameter("IsActive", MVTM.IsActive, DbType.Boolean);
                DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
                DbParameter[] Params = new DbParameter[6] { MaterialVariantTypeMappingIDP, ProductIDP, VariantTypeIDP, MaterialIDP, IsActiveP, UserIDP };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertOrUpdateMaterialVariantTypeMapping", Params);
                    scope.Complete();
                }
                success = true;
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("BDMS_Material", "InsertOrUpdateMaterialVariantTypeMapping", e1);
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return success;
        }
        public List<PMaterialDrawing> GetMaterialDrawing(long? MaterialID)
        {
            string endPoint = "Material/GetMaterialDrawing?MaterialID=" + MaterialID;
            return JsonConvert.DeserializeObject<List<PMaterialDrawing>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetAttachedFileMaterialDrawingForDownload(string DocumentName)
        {
            string endPoint = "Material/AttachedFileMaterialDrawingForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
    }
}
