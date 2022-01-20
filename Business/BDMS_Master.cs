using DataAccess;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Business
{
    public class BDMS_Master
    {
        private IDataAccess provider;
        public BDMS_Master()
        {
            provider = new ProviderFactory().GetProvider();
        }
        public List<PDMS_Remarks> GetRemarks(int RemarksMainID, int? RemarksSubID)
        {
            List<PDMS_Remarks> Remarks = new List<Properties.PDMS_Remarks>();
            DbParameter RemarksMainIDP = provider.CreateParameter("RemarksMainID", RemarksMainID, DbType.Int32);
            DbParameter RemarksSubIDP = provider.CreateParameter("RemarksSubID", RemarksSubID, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { RemarksMainIDP, RemarksSubIDP };
            try
            {

                using (DataSet ds = provider.Select("ZDMS_GetRemarks", Params))
                {
                    if (ds != null)
                    { 
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Remarks.Add(new PDMS_Remarks()
                            {
                                RemarksMainID = Convert.ToInt32(dr["RemarksMainID"]),
                                RemarksSubID = Convert.ToInt32(dr["RemarksSubID"]),
                                Remarks = Convert.ToString(dr["Remarks"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
            }
            catch (Exception ex)
            { }
            return Remarks;
          }

        public List<PDMS_WarrantyClaimCategory> GetCategory(int? CategoryID, string Category)
        {
            List<PDMS_WarrantyClaimCategory> Ca = new List<PDMS_WarrantyClaimCategory>();
            DbParameter RemarksMainIDP = provider.CreateParameter("CategoryID", CategoryID, DbType.Int32);
            DbParameter RemarksSubIDP = provider.CreateParameter("Category", Category, DbType.Int32);
            DbParameter[] Params = new DbParameter[2] { RemarksMainIDP, RemarksSubIDP };
            try
            { 
                using (DataSet ds = provider.Select("ZDMS_GetWarrantyClaimCategory", Params))
                {
                    if (ds != null)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Ca.Add(new PDMS_WarrantyClaimCategory()
                            {
                                CategoryID = Convert.ToInt32(dr["CategoryID"]), 
                                Category = Convert.ToString(dr["Category"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
            }
            catch (Exception ex)
            { }
            return Ca;
        }
        public DataTable ExecuteReader(String Query,Boolean live = false)
        {
            return new NpgsqlServer().ExecuteReader(Query);
        }

        public List<PBankName> GetBankName(int? BankNameID, string BankName)
        {
            string endPoint = "Master/BankName?BankNameID=" + BankNameID + "&BankName=" + BankName;
            return JsonConvert.DeserializeObject<List<PBankName>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PEffortType> GetEffortType(int? EffortTypeID, string EffortType)
        {
            string endPoint = "Master/EffortType?EffortTypeID=" + EffortTypeID + "&EffortType=" + EffortType;
            return JsonConvert.DeserializeObject<List<PEffortType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PExpenseType> GetExpenseType(int? ExpenseTypeID, string ExpenseType)
        {
            string endPoint = "Master/ExpenseType?ExpenseTypeID=" + ExpenseTypeID + "&ExpenseType=" + ExpenseType;
            return JsonConvert.DeserializeObject<List<PExpenseType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }


        public List<PMake> GetMake(int? MakeID, string Make)
        {
            string endPoint = "Master/Make?MakeID=" + MakeID + "&Make=" + Make;
            return JsonConvert.DeserializeObject<List<PMake>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PProductType> GetProductType(int? MarketSegmentID, string MarketSegment)
        {
            string endPoint = "Master/ProductType?ProductTypeID=" + MarketSegmentID + "&ProductType=" + MarketSegment;
            return JsonConvert.DeserializeObject<List<PProductType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PProduct> GetProduct(int? ProductID, string Product)
        {
            string endPoint = "Master/Product?ProductID=" + ProductID + "&Product=" + Product;
            return JsonConvert.DeserializeObject<List<PProduct>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PRelation> GetRelation(int? RelationID, string Relation)
        {
            string endPoint = "Master/Relation?RelationID=" + RelationID + "&Relation=" + Relation;
            return JsonConvert.DeserializeObject<List<PRelation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
    }
} 