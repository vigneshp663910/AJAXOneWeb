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
using System.Web.UI.WebControls;

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
        public List<PProduct> GetProduct(int? ProductID, int? MakeID, int? ProductTypeID, string Product)
        {
            string endPoint = "Master/Product?ProductID=" + ProductID + "&MakeID=" + MakeID + "&ProductTypeID=" + ProductTypeID + "&Product=" + Product;
            return JsonConvert.DeserializeObject<List<PProduct>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PRelation> GetRelation(int? RelationID, string Relation)
        {
            string endPoint = "Master/Relation?RelationID=" + RelationID + "&Relation=" + Relation;
            return JsonConvert.DeserializeObject<List<PRelation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PImportance> GetImportance(int? ImportanceID, string Importance)
        {
            string endPoint = "Master/Importance?ImportanceID=" + ImportanceID + "&Importance=" + Importance;
            return JsonConvert.DeserializeObject<List<PImportance>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PPreSaleStatus> GetPreSaleStatus(int? PreSaleStatusID, string PreSaleStatus)
        {
            string endPoint = "Master/PreSaleStatus?PreSaleStatusID=" + PreSaleStatusID + "&PreSaleStatus=" + PreSaleStatus;
            return JsonConvert.DeserializeObject<List<PPreSaleStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PPriceGroup> GetPriceGroup(int? PriceGroupID, int? PriceGroupCode, string Description)
        {
            string endPoint = "Master/PriceGroup?PriceGroupID=" + PriceGroupID + "&PriceGroupCode=" + PriceGroupCode + "&Description=" + Description;
            return JsonConvert.DeserializeObject<List<PPriceGroup>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PIncoterms> GetIncoterms(int? IncoTermID, string IncoTerm, string Description)
        {
            string endPoint = "Master/Incoterms?IncoTermID=" + IncoTermID + "&IncoTerm=" + IncoTerm + "&Description=" + Description;
            return JsonConvert.DeserializeObject<List<PIncoterms>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PPaymentTerms> GetPaymentTerms(int? PaymentTermID, string PaymentTerm, string Description)
        {
            string endPoint = "Master/PaymentTerms?PaymentTermID=" + PaymentTermID + "&PaymentTerm=" + PaymentTerm + "&Description=" + Description;
            return JsonConvert.DeserializeObject<List<PPaymentTerms>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PPlant> GetPlant(int? PlantID, string PlantCode)
        {
            string endPoint = "Master/Plant?PlantID=" + PlantID + "&PlantCode=" + PlantCode;
            return JsonConvert.DeserializeObject<List<PPlant>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_Division> GetDivision(int? DivisionID, string DivisionCode)
        {
            string endPoint = "Master/Division?DivisionID=" + DivisionID + "&DivisionCode=" + DivisionCode;
            return JsonConvert.DeserializeObject<List<PDMS_Division>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public void GetDivisionForSearchGrouped(DropDownList ddl)
        {
            string endPoint = "Master/GetDivisionForSearchGrouped";
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            ddl.DataTextField = "DivisionCode";
            ddl.DataValueField = "DivisionID";
            ddl.DataSource = dt;
            ddl.DataBind(); 
        }
        public List<PICTicketCallCategory> GetICTicketCallCategory(int? CallCategoryID, string CallCategory)
        {
            string endPoint = "Master/ICTicketCallCategory?CallCategoryID=" + CallCategoryID + "&CallCategory=" + CallCategory;
            return JsonConvert.DeserializeObject<List<PICTicketCallCategory>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PProductDrawingType> GetProductDrawingType(int? ProductDrawingTypeID, string ProductDrawingTypeName)
        {
            string endPoint = "Master/ProductDrawingType?ProductDrawingTypeID=" + ProductDrawingTypeID + "&ProductDrawingTypeName=" + ProductDrawingTypeName;
            return JsonConvert.DeserializeObject<List<PProductDrawingType>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PProductDrawing> GetProductDrawing(int? ProductID)
        {
            string endPoint = "Master/GetProductDrawing?ProductID=" + ProductID;
            return JsonConvert.DeserializeObject<List<PProductDrawing>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetAttachedFileProductDrawingForDownload(string DocumentName)
        {
            string endPoint = "Master/AttachedFileProductDrawingForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }
        public List<PAjaxOneStatus> GetAjaxOneStatus(int StatusHeaderID)
        {
            string endPoint = "Master/GetAjaxOneStatus?StatusHeaderID=" + StatusHeaderID;
            return JsonConvert.DeserializeObject<List<PAjaxOneStatus>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PProductSpecification> GetProductSpecification(int? ProductID)
        {
            string endPoint = "Master/GetProductSpecification?ProductID=" + ProductID;
            return JsonConvert.DeserializeObject<List<PProductSpecification>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetICTicketMttrEscalationMatrix(string EscalationHours, int? PageIndex, int? PageSize)
        {
            string endPoint = "MAster/GetICTicketMttrEscalationMatrix?EscalationHours=" + EscalationHours + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
    }
} 