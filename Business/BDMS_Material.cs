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
        public int IntegrationMaterialOld()
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {
                string query = "select m.p_material,r_description,r_base_unit,f_mat_type,f_mat_grp,r_weight_unit,r_material_division,r_gross_weight,r_net_weight ,r_hsn_id,tax_percentage,r_mrp ,m.r_model,m.r_sub_category from af_m_materials m left join af_m_price p on m.p_material =p.p_material  ";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_Material SOI = new PDMS_Material();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_Material();
                    SOI.MaterialCode = Convert.ToString(dr["p_material"]);
                    SOI.MaterialDescription = Convert.ToString(dr["r_description"]);
                    SOI.BaseUnit = Convert.ToString(dr["r_base_unit"]);
                    SOI.MaterialType = Convert.ToString(dr["f_mat_type"]);
                    SOI.MaterialGroup = Convert.ToString(dr["f_mat_grp"]);
                    SOI.WeightUnit = Convert.ToString(dr["r_weight_unit"]);
                    SOI.MaterialDivision = Convert.ToString(dr["r_material_division"]);
                    SOI.GrossWeight = DBNull.Value == dr["r_gross_weight"] ? 0 : Convert.ToDecimal(dr["r_gross_weight"]);
                    SOI.NetWeight = DBNull.Value == dr["r_net_weight"] ? 0 : Convert.ToDecimal(dr["r_net_weight"]);
                    SOI.HSN = Convert.ToString(dr["r_hsn_id"]);
                    SOI.TaxPercentage = DBNull.Value == dr["tax_percentage"] ? 0 : Convert.ToDecimal(dr["tax_percentage"]);
                    SOI.CurrentPrice = DBNull.Value == dr["r_mrp"] ? 0 : Convert.ToDecimal(dr["r_mrp"]);

                    SOI.Product = Convert.ToString(dr["r_model"]);
                    SOI.ProductGroup = Convert.ToString(dr["r_sub_category"]);
                    SOIs.Add(SOI);
                }



                foreach (PDMS_Material Material in SOIs)
                {
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode", Material.MaterialCode, DbType.String);
                    DbParameter MaterialDescription = provider.CreateParameter("MaterialDescription", Material.MaterialDescription, DbType.String);

                    DbParameter BaseUnit = provider.CreateParameter("BaseUnit", Material.BaseUnit, DbType.String);
                    DbParameter MaterialType = provider.CreateParameter("MaterialType", Material.MaterialType, DbType.String);
                    DbParameter MaterialGroup = provider.CreateParameter("MaterialGroup", Material.MaterialGroup, DbType.String);
                    DbParameter GrossWeight = provider.CreateParameter("GrossWeight", Material.GrossWeight, DbType.Decimal);
                    DbParameter NetWeight = provider.CreateParameter("NetWeight", Material.NetWeight, DbType.Decimal);


                    DbParameter WeightUnit = provider.CreateParameter("WeightUnit", Material.WeightUnit, DbType.String);
                    DbParameter HSNCode = provider.CreateParameter("HSNCode", Material.HSN, DbType.String);
                    DbParameter CurrentPrice = provider.CreateParameter("CurrentPrice", Material.CurrentPrice, DbType.Decimal);
                    DbParameter TaxPercentage = provider.CreateParameter("TaxPercentage", Material.TaxPercentage, DbType.Decimal);

                    DbParameter Product = provider.CreateParameter("Product", Material.Product, DbType.String);
                    DbParameter ProductGroup = provider.CreateParameter("ProductGroup", Material.ProductGroup, DbType.String);

                    DbParameter[] Params = new DbParameter[13] { MaterialCode, MaterialDescription, BaseUnit,MaterialType
                                    , MaterialGroup, GrossWeight, NetWeight, WeightUnit,HSNCode, CurrentPrice, TaxPercentage ,Product,ProductGroup};
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateMaterialOld", Params);
                            scope.Complete();
                        }

                    }
                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", sqlEx);

                        throw;
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessage("BDMS_Material", " IntegrationMaterial", ex);
                        throw;
                    }
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", ex);
            }
            return SOIs.Count();
        }
        public int IntegrationMaterial()
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {
                SOIs = new SMaterial().getMaterialIntegration();

                foreach (PDMS_Material Material in SOIs)
                {
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode", Material.MaterialCode, DbType.String);
                    DbParameter ValidFrom = provider.CreateParameter("ValidFrom", Material.ValidFrom, DbType.DateTime);
                    DbParameter ValidTo = provider.CreateParameter("ValidTo", Material.ValidTo, DbType.DateTime);
                    DbParameter MaterialDescription = provider.CreateParameter("MaterialDescription", Material.MaterialDescription, DbType.String);

                    DbParameter BaseUnit = provider.CreateParameter("BaseUnit", Material.BaseUnit, DbType.String);
                    DbParameter MaterialType = provider.CreateParameter("MaterialType", Material.MaterialType, DbType.String);
                    DbParameter MaterialGroup = provider.CreateParameter("MaterialGroup", Material.MaterialGroup, DbType.String);
                    DbParameter ModelCode = provider.CreateParameter("ModelCode", Material.Model.ModelCode, DbType.String);
                    DbParameter SubCategory = provider.CreateParameter("SubCategory", Material.SubCategory, DbType.String);
                    DbParameter GrossWeight = provider.CreateParameter("GrossWeight", Material.GrossWeight, DbType.Decimal);
                    DbParameter NetWeight = provider.CreateParameter("NetWeight", Material.NetWeight, DbType.Decimal);

                    DbParameter SerialProfile = provider.CreateParameter("SerialProfile", Material.SerialProfile, DbType.String);

                    DbParameter WeightUnit = provider.CreateParameter("WeightUnit", Material.WeightUnit, DbType.String);
                    DbParameter HSNCode = provider.CreateParameter("HSNCode", Material.HSN, DbType.String);
                    DbParameter CurrentPrice = provider.CreateParameter("CurrentPrice", Material.CurrentPrice, DbType.Decimal);
                    DbParameter TaxPercentage = provider.CreateParameter("TaxPercentage", Material.TaxPercentage, DbType.Decimal);

                    DbParameter Product = provider.CreateParameter("Product", Material.Product, DbType.String);
                    DbParameter ProductGroup = provider.CreateParameter("ProductGroup", Material.ProductGroup, DbType.String);
                    DbParameter Model = provider.CreateParameter("Model", Material.Model.Model, DbType.String);
                    DbParameter DivisionCode = provider.CreateParameter("DivisionCode", Material.Model.Division.DivisionCode, DbType.String);
                    DbParameter DivisionDescription = provider.CreateParameter("DivisionDescription", Material.Model.Division.DivisionDescription, DbType.String);
                    DbParameter IsActive = provider.CreateParameter("IsActive", Material.IsActive, DbType.Boolean);
                    


                    DbParameter[] Params = new DbParameter[22] { MaterialCode,ValidFrom,ValidTo, MaterialDescription, BaseUnit,MaterialType
                                    , MaterialGroup,ModelCode,SubCategory, GrossWeight, NetWeight,SerialProfile,WeightUnit,HSNCode, CurrentPrice, TaxPercentage ,Product,ProductGroup,Model,DivisionCode,DivisionDescription,IsActive};
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateMaterial", Params);
                            scope.Complete();
                            new SMaterial().setMaterialInActive(Material.MaterialCode);
                        }

                    }
                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", sqlEx);

                        throw;
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessage("BDMS_Material", " IntegrationMaterial", ex);
                        throw;
                    }
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", ex);
            }
            return SOIs.Count();
        }
        public int IntegrationMaterialSupersede()
        {
            TraceLogger.Log(DateTime.Now);
            List<PSupersede> SOIs = new List<PSupersede>();
            try
            {
                SOIs = new SMaterial().getMaterialSupersedeIntegration();

                foreach (PSupersede Supersedes in SOIs)
                {
                    DbParameter Material = provider.CreateParameter("Material", Supersedes.Material, DbType.String);
                    DbParameter Supersede = provider.CreateParameter("Supersede", Supersedes.MaterialDescription, DbType.String);
                    DbParameter Description = provider.CreateParameter("Description", Supersedes.Description, DbType.String);
                    DbParameter ValidFrom = provider.CreateParameter("ValidFrom", Supersedes.ValidFrom, DbType.DateTime);
                    DbParameter ValidTo = provider.CreateParameter("ValidTo", Supersedes.ValidTo, DbType.DateTime);
                    DbParameter IsActive = provider.CreateParameter("IsActive", Supersedes.IsActive, DbType.Boolean);


                    DbParameter[] Params = new DbParameter[6] { Material,Supersede,Description,ValidFrom,ValidTo,IsActive};
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateMaterialSupersede", Params);
                            scope.Complete();
                            new SMaterial().setMaterialSupersedeInActive(Supersedes.Material, Supersedes.MaterialDescription);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessage("BDMS_Material", "IntegrationSuperSeed", sqlEx);

                        throw;
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessage("BDMS_Material", " IntegrationSuperSeed", ex);
                        throw;
                    }
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", ex);
            }
            return SOIs.Count();
        }
        public List<PDMS_Material> GetMaterial(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {
                // string query = "SELECT  * from af_m_materials where 1 = 1 " + filter;

                string query = "select m.p_material,r_description,r_base_unit,f_mat_type,f_mat_grp,r_weight_unit,r_material_division,r_gross_weight,r_net_weight ,r_hsn_id,tax_percentage,r_mrp from af_m_materials m left join af_m_price p on m.p_material =p.p_material where 1 = 1  " + filter;
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_Material SOI = new PDMS_Material();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_Material();
                    SOI.MaterialCode = Convert.ToString(dr["p_material"]);
                    SOI.MaterialDescription = Convert.ToString(dr["r_description"]);
                    SOI.BaseUnit = Convert.ToString(dr["r_base_unit"]);
                    SOI.MaterialType = Convert.ToString(dr["f_mat_type"]);
                    SOI.MaterialGroup = Convert.ToString(dr["f_mat_grp"]);
                    SOI.WeightUnit = Convert.ToString(dr["r_weight_unit"]);
                    SOI.MaterialDivision = Convert.ToString(dr["r_material_division"]);
                    SOI.GrossWeight = DBNull.Value == dr["r_gross_weight"] ? 0 : Convert.ToDecimal(dr["r_gross_weight"]);
                    SOI.NetWeight = DBNull.Value == dr["r_net_weight"] ? 0 : Convert.ToDecimal(dr["r_net_weight"]);
                    SOI.HSN = Convert.ToString(dr["r_hsn_id"]);
                    SOI.TaxPercentage = DBNull.Value == dr["tax_percentage"] ? 0 : Convert.ToDecimal(dr["tax_percentage"]);
                    SOI.CurrentPrice = DBNull.Value == dr["r_mrp"] ? 0 : Convert.ToDecimal(dr["r_mrp"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterial", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_Material> GetMaterialSupersede(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {
                // string query = "SELECT  * from af_m_materials where 1 = 1 " + filter;

                string query = "select s.p_rmaterial,mm.r_description as  mm_desc,s.p_smaterial,ms.r_description  as sm_desc ,s.valid_from, s.valid_to, s.r_message_desc  from af_m_supersede s left join af_m_materials mm on mm.p_material = s.p_rmaterial left join af_m_materials ms on ms.p_material = s.p_smaterial   where 1 = 1  " + filter + "  order by valid_from desc";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_Material SOI = new PDMS_Material();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_Material();
                    SOI.MaterialCode = Convert.ToString(dr["p_rmaterial"]);
                    SOI.MaterialDescription = Convert.ToString(dr["mm_desc"]);
                    SOI.Supersede = new PSupersede();
                    SOI.Supersede.Material = Convert.ToString(dr["p_smaterial"]);
                    SOI.Supersede.MaterialDescription = Convert.ToString(dr["sm_desc"]);
                    SOI.Supersede.ValidFrom = Convert.ToDateTime(dr["valid_from"]);
                    SOI.Supersede.ValidTo = Convert.ToDateTime(dr["valid_to"]);
                    SOI.Supersede.Description = Convert.ToString(dr["r_message_desc"]);
                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialSupersede", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<PDMS_Material> GetMaterialRoqDoq(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {
                // string query = "SELECT  * from af_m_materials where 1 = 1 " + filter;

                string query = "SELECT   r.p_material,r.s_tenant_id, k_season_code, p_office, r.s_status,r.r_valid_from, r_req_roq_qty, r_doq_qty,   r.r_valid_to,   d_office_desc,  r_unit,   r_roq_qty ,r_description,description	,s.r_valid_from as  season_valid_from ,s.r_valid_to as  season_valid_to FROM public.dmror_roqdoq r	left join af_m_materials m on m.p_material =r.p_material	left  JOIN m_tenant ten ON  ten.tenantid = r.s_tenant_id     left join dmror_season s on s.p_season_code = r.k_season_code and s.s_tenant_id = r.s_tenant_id   where 1 = 1  " + filter + "  order by r_valid_from desc";
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_Material SOI = new PDMS_Material();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_Material();
                    SOI.MaterialCode = Convert.ToString(dr["p_material"]);
                    SOI.MaterialDescription = Convert.ToString(dr["r_description"]);
                    SOI.RoqDoq = new PRoqDoq();
                    SOI.RoqDoq.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]), DealerName = Convert.ToString(dr["description"]) };
                    SOI.RoqDoq.SeasonCode = Convert.ToString(dr["k_season_code"]);
                    SOI.RoqDoq.SeasonValidFrom = Convert.ToDateTime(dr["season_valid_from"]);
                    SOI.RoqDoq.SeasonValidTo = Convert.ToDateTime(dr["season_valid_to"]);
                    SOI.RoqDoq.Office = Convert.ToString(dr["p_office"]);
                    SOI.RoqDoq.ValidFrom = Convert.ToDateTime(dr["r_valid_from"]);
                    SOI.RoqDoq.ValidTo = Convert.ToDateTime(dr["r_valid_to"]);
                    SOI.RoqDoq.Status = Convert.ToString(dr["s_status"]);

                    SOI.RoqDoq.ReqRoqQty = dr["r_req_roq_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["r_req_roq_qty"]); ;
                    SOI.RoqDoq.DoqQty = dr["r_doq_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["r_doq_qty"]);

                    SOI.RoqDoq.OfficeDesc = Convert.ToString(dr["d_office_desc"]);
                    SOI.RoqDoq.Unit = Convert.ToString(dr["r_unit"]);
                    SOI.RoqDoq.RoqQty = dr["r_roq_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["r_roq_qty"]);

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialSupersede", ex);
                throw ex;
            }
            return SOIs;
        }
        public List<PDMS_Material> GetMaterialBin(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {
                // string query = "SELECT  * from af_m_materials where 1 = 1 " + filter;

                string query = "SELECT  b.p_material ,r_description,s_tenant_id,description, p_office, p_location,      r_bin_id  	FROM public.dmmer_bin_location b left join af_m_materials m on m.p_material =b.p_material left  JOIN m_tenant ten ON  ten.tenantid = b.s_tenant_id    where 1 = 1  " + filter;
                DataTable dt = new NpgsqlServer().ExecuteReader(query);
                PDMS_Material SOI = new PDMS_Material();
                foreach (DataRow dr in dt.Rows)
                {
                    SOI = new PDMS_Material();
                    SOI.MaterialCode = Convert.ToString(dr["p_material"]);
                    SOI.MaterialDescription = Convert.ToString(dr["r_description"]);
                    SOI.Bin = new PBin();
                    SOI.Bin.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["s_tenant_id"]), DealerName = Convert.ToString(dr["description"]) };

                    SOI.Bin.Office = Convert.ToString(dr["p_office"]);
                    SOI.Bin.Location = Convert.ToString(dr["p_location"]);
                    SOI.Bin.BinID = Convert.ToString(dr["r_bin_id"]);

                    SOIs.Add(SOI);
                }
                return SOIs;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "GetMaterialSupersede", ex);
                throw ex;
            }
            return SOIs;
        }

        public List<string> GetMaterialAutocomplete(string Material, string MaterialType)
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

                DbParameter[] Params = new DbParameter[2] { MaterialP, MaterialTypeP };
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

        public List<PDMS_Material> GetMaterialListSQL(int? MaterialID, string MaterialCode)
        {
            string endPoint = "Material?MaterialID=" + MaterialID + "&MaterialCode=" + MaterialCode;
            return JsonConvert.DeserializeObject<List<PDMS_Material>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

            //TraceLogger.Log(DateTime.Now);
            //List<PDMS_Material> SOIs = new List<PDMS_Material>();
            //PDMS_Material SOI = new PDMS_Material();
            //try
            //{
            //    DbParameter MaterialIDP = provider.CreateParameter("MaterialID", MaterialID, DbType.Int32);
            //    DbParameter MaterialCodeP = provider.CreateParameter("MaterialCode", string.IsNullOrEmpty(MaterialCode) ? null : MaterialCode, DbType.String);
            //    DbParameter[] Params = new DbParameter[2] { MaterialIDP, MaterialCodeP };

            //    using (DataSet DataSet = provider.Select("ZDMS_GetMaterialList", Params))
            //    {
            //        if (DataSet != null)
            //        {
            //            foreach (DataRow dr in DataSet.Tables[0].Rows)
            //            {
            //                SOI = new PDMS_Material();
            //                SOI.MaterialID = Convert.ToInt32(dr["MaterialID"]);
            //                SOI.MaterialCode = Convert.ToString(dr["MaterialCode"]);
            //                SOI.MaterialDescription = Convert.ToString(dr["MaterialDescription"]);
            //                SOI.BaseUnit = Convert.ToString(dr["BaseUnit"]);
            //                SOI.MaterialType = Convert.ToString(dr["MaterialType"]);
            //                SOI.MaterialGroup = Convert.ToString(dr["MaterialGroup"]);
            //                SOI.WeightUnit = Convert.ToString(dr["WeightUnit"]);
            //                //  SOI.MaterialDivision = Convert.ToString(dr["MaterialDivision"]);
            //                SOI.GrossWeight = DBNull.Value == dr["GrossWeight"] ? 0 : Convert.ToDecimal(dr["GrossWeight"]);
            //                SOI.NetWeight = DBNull.Value == dr["NetWeight"] ? 0 : Convert.ToDecimal(dr["NetWeight"]);
            //                SOI.HSN = Convert.ToString(dr["HSNCode"]);
            //                SOI.TaxPercentage = DBNull.Value == dr["TaxPercentage"] ? 0 : Convert.ToDecimal(dr["TaxPercentage"]);
            //                SOI.CurrentPrice = DBNull.Value == dr["CurrentPrice"] ? 0 : Convert.ToDecimal(dr["CurrentPrice"]);
            //                SOI.IsMainServiceMaterial = DBNull.Value == dr["IsMainServiceMaterial"] ? false : Convert.ToBoolean(dr["IsMainServiceMaterial"]);
            //                SOIs.Add(SOI);
            //            }
            //            return SOIs;
            //            TraceLogger.Log(DateTime.Now);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    new FileLogger().LogMessage("BDMS_Material", "GetMaterial", ex);
            //    throw ex;
            //}
            //return SOIs;
        }

        public int IntegrationMaterialFromEccSap()
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Material> SOIs = new List<PDMS_Material>();
            try
            {

                SOIs = new SMaterial().getAllMaterialFromEccSap("");

                foreach (PDMS_Material Material in SOIs)
                {
                    DbParameter MaterialCode = provider.CreateParameter("MaterialCode", Material.MaterialCodeWithOutZero, DbType.String);
                    DbParameter MaterialDescription = provider.CreateParameter("MaterialDescription", Material.MaterialDescription, DbType.String);

                    DbParameter BaseUnit = provider.CreateParameter("BaseUnit", Material.BaseUnit, DbType.String);
                    DbParameter MaterialType = provider.CreateParameter("MaterialType", Material.MaterialType, DbType.String);
                    DbParameter MaterialGroup = provider.CreateParameter("MaterialGroup", Material.MaterialGroup, DbType.String);
                    DbParameter GrossWeight = provider.CreateParameter("GrossWeight", Material.GrossWeight, DbType.Decimal);
                    DbParameter NetWeight = provider.CreateParameter("NetWeight", Material.NetWeight, DbType.Decimal);


                    DbParameter WeightUnit = provider.CreateParameter("WeightUnit", Material.WeightUnit, DbType.String);
                    DbParameter HSNCode = provider.CreateParameter("HSNCode", Material.HSN, DbType.String);
                    //  DbParameter CurrentPrice = provider.CreateParameter("CurrentPrice", Material.CurrentPrice, DbType.Decimal);
                    //  DbParameter TaxPercentage = provider.CreateParameter("TaxPercentage", Material.TaxPercentage, DbType.Decimal);

                    //  DbParameter Product = provider.CreateParameter("Product", Material.Product, DbType.String);
                    //   DbParameter ProductGroup = provider.CreateParameter("ProductGroup", Material.ProductGroup, DbType.String);

                    DbParameter[] Params = new DbParameter[9] { MaterialCode, MaterialDescription, BaseUnit,MaterialType
                                    , MaterialGroup, GrossWeight, NetWeight, WeightUnit,HSNCode};
                    try
                    {
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            provider.Insert("ZDMS_InsertOrUpdateMaterialFromEccSap", Params);
                            scope.Complete();
                        }

                    }
                    catch (SqlException sqlEx)
                    {
                        new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", sqlEx);

                        throw;
                    }
                    catch (Exception ex)
                    {
                        new FileLogger().LogMessage("BDMS_Material", " IntegrationMaterial", ex);
                        throw;
                    }
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", ex);
            }
            return SOIs.Count();
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
    }
}
