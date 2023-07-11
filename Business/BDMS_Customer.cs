using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class BDMS_Customer
    {
        private IDataAccess provider;
        public BDMS_Customer()
        {
            provider = new ProviderFactory().GetProvider();
        }
         
        public PDMS_Customer GetCustomerAddressFromPG(String DealerCode, String CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_Customer Customer = new PDMS_Customer();
            try
            {
                string Query = "select   bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value as  GSTIN ,bpsPan.r_value as  PAN from doohr_bp bp "
+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' left Join  doohr_bp_statutory bpsPan on bpsPan.p_bp_id =bp.p_bp_id and bpsPan.r_statutory_type='PAN' "
+ " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where  p_office_type_id='ST' and bp.p_bp_id = '" + CustomerCode + "' and  bp.s_tenant_id = " + DealerCode
+ "group by  bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value  ,bpsPan.r_value ";

                DataTable dt = new BPG().OutputDataTable(Query);
                

               //  DataTable dt = new NpgsqlServer().ExecuteReader("select   bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value as  GSTIN ,bpsPan.r_value as  PAN from doohr_bp bp "
//+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' left Join  doohr_bp_statutory bpsPan on bpsPan.p_bp_id =bp.p_bp_id and bpsPan.r_statutory_type='PAN' "
//+ " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where  p_office_type_id='ST' and bp.p_bp_id = '" + CustomerCode + "' and  bp.s_tenant_id = " + DealerCode
//+ "group by  bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value  ,bpsPan.r_value ");
                foreach (DataRow dr in dt.Rows)
                {
                    Customer = new PDMS_Customer();
                    Customer.CustomerCode = Convert.ToString(dr["p_bp_id"]);
                    Customer.CustomerName = Convert.ToString(dr["r_org_name"]);

                    Customer.Address1 = Convert.ToString(dr["r_address1"]);
                    Customer.Address2 = Convert.ToString(dr["r_address2"]);
                    // Customer.Address3 = tagListBapi.GetString("ADD3");
                    Customer.City = Convert.ToString(dr["r_city"]);

                    Customer.State = new PDMS_State() { State = Convert.ToString(dr["r_state"]) };

                    Customer.Pincode = Convert.ToString(dr["r_postal_code"]);

                    Customer.GSTIN = Convert.ToString(dr["GSTIN"]);
                    Customer.State.StateCode = Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "";
                    Customer.PAN = Convert.ToString(dr["PAN"]);

                }
                return Customer;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "GetCustomerName", ex);
                throw ex;
            }
            return Customer;
        }
        public PDMS_Customer GetCustomerAddressFromPG_p_office_id(String DealerCode, String CustomerCode, string p_office_id)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_Customer Customer = new PDMS_Customer();
            try
            {
                string Query = "select bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value as GSTIN ,bpsPan.r_value as PAN from doohr_bp bp "
+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' left Join  doohr_bp_statutory bpsPan on bpsPan.p_bp_id =bp.p_bp_id and bpsPan.r_statutory_type='PAN' "
+ " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where    p_office_id = '" + p_office_id + "' and bp.p_bp_id = '" + CustomerCode + "' and  bp.s_tenant_id = " + DealerCode
+ " group by  bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value  ,bpsPan.r_value ";

                DataTable dt = new BPG().OutputDataTable(Query);

                //                DataTable dt = new NpgsqlServer().ExecuteReader("select   bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value as  GSTIN ,bpsPan.r_value as  PAN from doohr_bp bp "
                //+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' left Join  doohr_bp_statutory bpsPan on bpsPan.p_bp_id =bp.p_bp_id and bpsPan.r_statutory_type='PAN' "
                //+ " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where    p_office_id = '" + p_office_id + "' and bp.p_bp_id = '" + CustomerCode + "' and  bp.s_tenant_id = " + DealerCode
                //+ " group by  bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value  ,bpsPan.r_value ");
                foreach (DataRow dr in dt.Rows)
                {
                    Customer = new PDMS_Customer();
                    Customer.CustomerCode = Convert.ToString(dr["p_bp_id"]);
                    Customer.CustomerName = Convert.ToString(dr["r_org_name"]);

                    Customer.Address1 = Convert.ToString(dr["r_address1"]);
                    Customer.Address2 = Convert.ToString(dr["r_address2"]);
                    // Customer.Address3 = tagListBapi.GetString("ADD3");
                    Customer.City = Convert.ToString(dr["r_city"]);

                    Customer.State = new PDMS_State() { State = Convert.ToString(dr["r_state"]) };

                    Customer.Pincode = Convert.ToString(dr["r_postal_code"]);

                    Customer.GSTIN = Convert.ToString(dr["GSTIN"]);
                    Customer.State.StateCode = Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "";
                    Customer.PAN = Convert.ToString(dr["PAN"]);

                }
                return Customer;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "GetCustomerName", ex);
                throw ex;
            }
            return Customer;
        }

        public PDMS_Customer GetCustomerAE()
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_Customer Customer = new PDMS_Customer();
            try
            {
                Customer.CustomerName = ConfigurationManager.AppSettings["EOrgName"];
                //  Customer.OrgName = ConfigurationManager.AppSettings["EOrgName"];
                Customer.Address1 = ConfigurationManager.AppSettings["EAddress1"];
                Customer.Address2 = ConfigurationManager.AppSettings["EAddress2"];
                Customer.City = ConfigurationManager.AppSettings["ECity"];
                Customer.State = new PDMS_State() { State = ConfigurationManager.AppSettings["EState"], StateCode = ConfigurationManager.AppSettings["EStateCode"] };
                Customer.Pincode = ConfigurationManager.AppSettings["EPincode"];
                Customer.GSTIN = ConfigurationManager.AppSettings["EGSTIN"];
                Customer.PAN = ConfigurationManager.AppSettings["EPAN"];
                Customer.Email = ConfigurationManager.AppSettings["EMAIL"];
                Customer.Mobile = ConfigurationManager.AppSettings["Mobile"];
                Customer.Web = ConfigurationManager.AppSettings["EWeb"];
                Customer.CIN = ConfigurationManager.AppSettings["ECIN"];

            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return Customer;
        }

        public string CustomerValidation(PDMS_Customer Customer)
        {

            if (string.IsNullOrEmpty(Customer.GSTIN))
            {
                return "Please update Customer GST Number";
            }
            String regexS = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexS);
            if (Customer.GSTIN == "URD")
            { }
            else if (regex.Match(Customer.GSTIN).Success)
            {
                Customer.State.StateCode = Customer.GSTIN.Substring(0, 2);
            }
            else
            {
                return "Please update correct GST Number";
            }

            if (!new BDMS_EInvoice().ValidatePincode(Customer.Pincode.Substring(0, 2), Customer.State.StateCode))
            {
                return "Please check Customer Pincode and Statecode";
            }
            if (string.IsNullOrEmpty(Customer.Address12.Trim()))
            {
                return "Please update Customer Address";
            }
            return "";
        }
        public List<PDMS_Customer> GetCustomerByCode(int? CustomerID, string CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DbParameter CustomerIDP = provider.CreateParameter("CustomerID", CustomerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                DbParameter[] Params = new DbParameter[2] { CustomerIDP, CustomerCodeP };

                PDMS_Customer Customer = new PDMS_Customer();
                using (DataSet DataSet = provider.Select("GetCustomerByCode", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Customer = new PDMS_Customer();
                            Customers.Add(Customer);
                            Customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                            Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            Customer.CustomerName = Convert.ToString(dr["CustomerName"]);

                        }
                    }
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return Customers;
        }

        public int InsertOrUpdateCustomerSap(string CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            int CustomerID = 0;
            try
            {
                PDMS_Customer cust = new BDMS_Customer().getCustomerAddressFromSAP(CustomerCode);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", cust.CustomerCode, DbType.String);
                    DbParameter CustomerNameP = provider.CreateParameter("CustomerName", cust.CustomerName, DbType.String);

                    DbParameter Address1 = provider.CreateParameter("Address1", cust.Address1, DbType.String);
                    DbParameter Address2 = provider.CreateParameter("Address2", cust.Address2, DbType.String);
                    DbParameter Address3 = provider.CreateParameter("Address3", cust.Address3, DbType.String);
                    DbParameter City = provider.CreateParameter("City", cust.City, DbType.String);
                    DbParameter State = provider.CreateParameter("State", cust.State.State, DbType.String);
                    DbParameter StateCode = provider.CreateParameter("StateCode", cust.State.StateCode, DbType.String);
                    DbParameter Pincode = provider.CreateParameter("PostalCode", cust.Pincode, DbType.String);
                    DbParameter GSTIN = provider.CreateParameter("GSTIN", cust.GSTIN, DbType.String);
                    DbParameter PAN = provider.CreateParameter("PAN", cust.PAN, DbType.String);
                    DbParameter MOBILE = provider.CreateParameter("MOBILE", cust.Mobile, DbType.String);
                    DbParameter EMAIL = provider.CreateParameter("EMAIL", cust.Email, DbType.String);
                    DbParameter ContactPerson = provider.CreateParameter("ContactPerson", cust.ContactPerson, DbType.String);
                    DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int32, Convert.ToInt32(ParameterDirection.Output));

                    DbParameter[] Params = new DbParameter[14] { CustomerCodeP, CustomerNameP, Address1, Address2, Address3, City, State, Pincode, GSTIN, PAN, MOBILE, EMAIL, ContactPerson, OutValue };
                    success = provider.Insert("ZDMS_InsertOrUpdateCustomerFromSap", Params);
                    if (success != 0)
                    {
                        CustomerID = Convert.ToInt32(OutValue.Value);
                    }
                    scope.Complete();
                }
                return CustomerID;
            }
            catch (Exception e1)
            {
            }
            TraceLogger.Log(DateTime.Now);
            return CustomerID;
        }


        public List<PDMS_Customer> GetCustomer(long? CustomerID, string CustomerCode, string CustomerName, string Mobile, int? CountryID, int? StateID, int? DistrictID, int? DealerID, int? PageIndex, int? PageSize)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer?CustomerID=" + CustomerID + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName + "&Mobile=" + Mobile
                + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID + "&DealerID=" + DealerID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PApiResult GetCustomerN(long? CustomerID, string CustomerCode, string CustomerName, string Mobile, int? CountryID, int? StateID, int? DistrictID, int? DealerID, int? PageIndex, int? PageSize)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer?CustomerID=" + CustomerID + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName + "&Mobile=" + Mobile
                + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID + "&DealerID=" + DealerID + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        }
        public DataTable GetCustomerExcelDownload(long? CustomerID, string CustomerCode, string CustomerName, string Mobile, int? CountryID, int? StateID, int? DealerID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/ExcelDownload?CustomerID=" + CustomerID + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName + "&Mobile=" + Mobile
                + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PDMS_Customer GetCustomerByID(long CustomerID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/CustomerByID?CustomerID=" + CustomerID;
            return JsonConvert.DeserializeObject<PDMS_Customer>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PCustomerTitle> GetCustomerTitle(int? TitleID, string Title)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/Title?TitleID=" + TitleID + "&Title=" + Title;
            return JsonConvert.DeserializeObject<List<PCustomerTitle>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PCustomerAttribute> GetCustomerAttribute(long? CustomerID, int? CreatedBy)
        {
            string endPoint = "Customer/Attribute?CustomerID=" + CustomerID + "&CreatedBy=" + CreatedBy;
            return JsonConvert.DeserializeObject<List<PCustomerAttribute>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PCustomerAttributeMain> GetCustomerAttributeMain(int? AttributeMainID, string AttributeMain)
        {
            string endPoint = "Customer/AttributeMain?AttributeMainID=" + AttributeMainID + "&AttributeMain=" + AttributeMain;
            return JsonConvert.DeserializeObject<List<PCustomerAttributeMain>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PCustomerAttributeSub> GetCustomerAttributeSub(int? AttributeSubID, int? AttributeMainID, string AttributeSub)
        {
            string endPoint = "Customer/AttributeSub?AttributeSubID=" + AttributeSubID + "&AttributeMainID=" + AttributeMainID + "&AttributeSub=" + AttributeSub;
            return JsonConvert.DeserializeObject<List<PCustomerAttributeSub>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PCustomerRelation> GetCustomerRelation(long? CustomerID, int? CreatedBy)
        {
            string endPoint = "Customer/Relation?CustomerID=" + CustomerID + "&CreatedBy=" + CreatedBy;
            return JsonConvert.DeserializeObject<List<PCustomerRelation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PCustomerProduct> GetCustomerProduct(long? CustomerID, long? ICTicketID, long? AvailabilityOfOtherMachineID, int? TypeOfMachineID, int? MakeID)
        {
            string endPoint = "Customer/Product?CustomerID=" + CustomerID + "&ICTicketID=" + ICTicketID + "&AvailabilityOfOtherMachineID=" + AvailabilityOfOtherMachineID
                + "&TypeOfMachineID=" + TypeOfMachineID + "&MakeID=" + MakeID;
            return JsonConvert.DeserializeObject<List<PCustomerProduct>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PCustomerResponsibleEmployee> GetCustomerResponsibleEmployee(long? CustomerResponsibleEmployeeID, long? CustomerID)
        {
            string endPoint = "Customer/ResponsibleEmployee?CustomerResponsibleEmployeeID=" + CustomerResponsibleEmployeeID + "&CustomerID=" + CustomerID;
            return JsonConvert.DeserializeObject<List<PCustomerResponsibleEmployee>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PCustomerFleet> GetCustomerFleet(long? CustomerFleetID, long? CustomerID)
        {
            string endPoint = "Customer/Fleet?CustomerFleetID=" + CustomerFleetID + "&CustomerID=" + CustomerID;
            return JsonConvert.DeserializeObject<List<PCustomerFleet>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PAttachedFile> GetAttachedFileCustomer(long? CustomerID)
        {
            string endPoint = "Customer/AttachedFile?CustomerID=" + CustomerID;
            return JsonConvert.DeserializeObject<List<PAttachedFile>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetAttachedFileCustomerForDownload(string DocumentName)
        {
            string endPoint = "Customer/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }

        public List<PDMS_Customer> GetCustomerAutocomplete(string Customer, int IsDealerMapping)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/CustomerAutocomplete?Customer=" + Customer + "&IsDealerMapping=" + IsDealerMapping;
            return JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public List<PDMS_Customer> GetCustomerForEnquiryToLead(string Customer, string Mobile, int StateID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/CustomerForEnquiryToLead?Customer=" + Customer + "&Mobile=" + Mobile + "&StateID=" + StateID;
            return JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PDMS_Customer> CustomerForDuplicateVerificatio(long CustomerID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/CustomerForDuplicateVerificatio?CustomerID=" + CustomerID;
            return JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public DataTable GetDealerCustomer(int? DealerID, string DealerCode, string CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                DbParameter[] Params = new DbParameter[3] { DealerIDP, DealerCodeP, CustomerCodeP };

                PDMS_Customer Customer = new PDMS_Customer();
                using (DataSet DataSet = provider.Select("ZDMS_GetDealerCustomer", Params))
                {
                    if (DataSet != null)
                    {
                        return DataSet.Tables[0];
                        //foreach (DataRow dr in DataSet.Tables[0].Rows)
                        //{
                        //    Customer = new PDMS_Customer();
                        //    Customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                        //    //Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                        //    Customer.CustomerCode = Convert.ToString(dr["DealerCodeCustomerCode"]);
                        //    Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                        //    Customers.Add(Customer);
                        //}
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("GetDealerCustomer", "ZDMS_GetDealerCustomer", ex);
                throw ex;
            }
            return null;
        }
        public int InsertOrUpdateDealerCustomerMapping(int? DealerCustomerMappingID, int? DealerID, string CustomerCode, int UserID, Boolean IsActive)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter DealerCustomerMappingIDP = provider.CreateParameter("DealerCustomerMappingID", DealerCustomerMappingID, DbType.Int32);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter[] Params = new DbParameter[5] { DealerCustomerMappingIDP, DealerIDP, CustomerCodeP, UserIDP, IsActiveP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_InsertOrUpdateDealerCustomerMapping", Params);
                    scope.Complete();
                }
                return 1;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_Customer", "ZDMS_InsertOrUpdateDealerCustomerMapping", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "ZDMS_InsertOrUpdateDealerCustomerMapping", ex);
            }

            TraceLogger.Log(DateTime.Now);
            return 0;
        }
        //public List<PDMS_Customer> GetCustomerFromSQL(long? CustomerID, string CustomerCode)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    List<PDMS_Customer> Customers = new List<PDMS_Customer>();
        //    try
        //    {
        //        DbParameter CustomerIDP = provider.CreateParameter("CustomerID", CustomerID, DbType.Int32);
        //        DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
        //        DbParameter[] Params = new DbParameter[2] { CustomerIDP, CustomerCodeP };

        //        PDMS_Customer Customer = new PDMS_Customer();
        //        using (DataSet DataSet = provider.Select("ZDMS_GetCustomer", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    Customer = new PDMS_Customer();
        //                    Customers.Add(Customer);
        //                    Customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
        //                    Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
        //                    Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
        //                    Customer.CustomerName2 = Convert.ToString(dr["CustomerName2"]);
        //                    Customer.Title = new PCustomerTitle();
        //                    Customer.Title.Title = Convert.ToString(dr["Title"]);
        //                    Customer.Address1 = Convert.ToString(dr["Address1"]);
        //                    Customer.Address2 = Convert.ToString(dr["Address2"]);
        //                    Customer.Tehsil = new PDMS_Tehsil();
        //                    Customer.Tehsil.Tehsil = Convert.ToString(dr["Tehsil"]);
        //                    Customer.Address3 = Convert.ToString(dr["Address3"]);
        //                    Customer.District = new PDMS_District();
        //                    Customer.District.District = Convert.ToString(dr["District"]);
        //                    Customer.District.SalesOffice = new PSalesOffice();
        //                    Customer.District.SalesOffice.SalesOffice = Convert.ToString(dr["SalesOffice"]);
        //                    Customer.District.SalesOffice.SalesGroup = Convert.ToString(dr["SalesGroup"]);
        //                    Customer.Pincode = Convert.ToString(dr["Pincode"]);
        //                    Customer.City = Convert.ToString(dr["City"]);
        //                    Customer.Country = new PDMS_Country();
        //                    Customer.Country.CountryID = Convert.ToInt32(dr["CountryID"]);
        //                    Customer.Country.CountryCode = Convert.ToString(dr["CountryCode"]);
        //                    Customer.Country.SalesOrganization = Convert.ToString(dr["SalesOrganization"]);
        //                    Customer.State = new PDMS_State();
        //                    Customer.State.StateID = Convert.ToInt32(dr["StateID"]);
        //                    Customer.State.StateCode = Convert.ToString(dr["StateCode"]);
        //                    Customer.State.Region = new PDMS_Region();
        //                    Customer.State.Region.Region = Convert.ToString(dr["Region"]);
        //                    Customer.Mobile = Convert.ToString(dr["Mobile"]);
        //                    Customer.AlternativeMobile = Convert.ToString(dr["AlternativeMobile"]);
        //                    Customer.Email = Convert.ToString(dr["EMail"]);
        //                    Customer.GSTIN = Convert.ToString(dr["GSTNo"]);
        //                    Customer.PAN = Convert.ToString(dr["PAN"]);
        //                    Customer.ContactPerson = Convert.ToString(dr["ContactPerson"]);
        //                }
        //            }
        //        }

        //        TraceLogger.Log(DateTime.Now);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
        //        throw ex;
        //    }
        //    return Customers;
        //}
        public int UpdateCustomerCodeFromSapToSql(PDMS_Customer Customer, Boolean IsShipTo)
        {
            //int? CustomerID, string CustomerCode
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            string Message = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Customer.CustomerCode))
                {
                    // DataTable DtResult = new SapIntegration.SCustomer().CreateCustomerInSAP(Customer, IsShipTo);
                    string endPoint = "Customer/CreateCustomerInSAP?CustomerID=" + Customer.CustomerID + "&IsShipTo=" + IsShipTo;
                    DataTable DtResult = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

                   
                    foreach (DataRow dr in DtResult.Rows)
                    {
                        if (dr["MSGTYP"].ToString() == "S")
                        {
                            Message = dr["MSGV1"].ToString();

                            if (!string.IsNullOrEmpty(Message))
                            {
                                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                                {
                                    DbParameter CustomerIDP = provider.CreateParameter("CustomerID", Customer.CustomerID, DbType.Int32);
                                    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", Message, DbType.String);
                                    DbParameter IsShipToP = provider.CreateParameter("IsShipTo", IsShipTo, DbType.Boolean);

                                    DbParameter[] Params = new DbParameter[3] { CustomerIDP, CustomerCodeP, IsShipToP };
                                    success = provider.Insert("ZDMS_UpdateCustomer", Params);
                                    scope.Complete();
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                Message += dr["MSGV1"].ToString() + Environment.NewLine + "\n";
                                throw new Exception(Message);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
                else
                {
                    // DataTable DtResult = new SapIntegration.SCustomer().ChangeCustomerInSAP(Customer, IsShipTo);
                    string endPoint = "Customer/ChangeCustomerInSAP?CustomerID=" + Customer.CustomerID + "&IsShipTo=" + IsShipTo;
                    DataTable DtResult = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

                    foreach (DataRow dr in DtResult.Rows)
                    {
                        if (dr["MSGTYP"].ToString() == "S")
                        {
                            Message = dr["MSGV1"].ToString();
                            if (Message == "0")
                            {
                                success = 1;
                            }
                            else
                            {
                                throw new Exception(Message);
                            }
                        }
                        else
                        {
                            try
                            {
                                Message += dr["MSGV1"].ToString() + Environment.NewLine + "\n";
                                throw new Exception(Message);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }

                new BAPI().ApiGet("Customer/SysCustomerWithPG?CustomerID=" + Customer.CustomerID);

                return success;
            }
            catch (Exception e1)
            {
                throw e1;
            }
            TraceLogger.Log(DateTime.Now);
            return success;
        }


        public void UpdateCustomerAddressFromSapToSql()
        {
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            PDMS_Customer Customer = new PDMS_Customer();
            using (DataSet DataSet = provider.Select("GetCustomerForUpdateAddressFromSapToSql"))
            {
                if (DataSet != null)
                {
                    foreach (DataRow dr in DataSet.Tables[0].Rows)
                    {
                        Customer = new PDMS_Customer();
                        Customers.Add(Customer);
                        Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);

                    }
                }
            }

            foreach (PDMS_Customer C in Customers)
            {
                InsertOrUpdateCustomerSap(C.CustomerCode);
            }
        }

        public List<PDMS_CustomerShipTo> GetCustomerShopTo(long? CustomerShipToID, long? CustomerID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/ShipTo?CustomerShipToID=" + CustomerShipToID + "&CustomerID=" + CustomerID;
            return JsonConvert.DeserializeObject<List<PDMS_CustomerShipTo>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PDMS_CustomerChangeForApproval> GetCustomerChangeForApproval(string CustomerCode, int? PageIndex, int? PageSize, out int RowCount)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_CustomerChangeForApproval> Customers = new List<PDMS_CustomerChangeForApproval>();
            RowCount = 0;
            try
            {
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                DbParameter PageIndexP = provider.CreateParameter("PageIndex", PageIndex, DbType.Int32);
                DbParameter PageSizeP = provider.CreateParameter("PageSize", PageSize, DbType.Int32);
                DbParameter[] Params = new DbParameter[3] { CustomerCodeP, PageIndexP, PageSizeP };

                using (DataSet DataSet = provider.Select("GetCustomerChangeForApproval", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            PDMS_CustomerChangeForApproval Customer = new PDMS_CustomerChangeForApproval();
                            Customer.CustomerChangeForApprovalID = Convert.ToInt64(dr["CustomerChangeForApprovalID"]);
                            Customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                            Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                            Customer.Unregistered = Convert.ToBoolean(dr["Unregistered"]);
                            Customer.GSTIN = Convert.ToString(dr["GST"]);
                            Customer.PAN = Convert.ToString(dr["PAN"]);
                            Customer.IsApproved = dr["IsApproved"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["IsApproved"]);

                            Customer.ApprovedBy = DBNull.Value == dr["ApprovedBy"] ? null : new PUser()
                            {
                                ContactName = Convert.ToString(dr["ApprovedByName"])
                            };
                            Customer.ApprovedOn = dr["ApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ApprovedOn"]);
                            Customer.CreatedBy = DBNull.Value == dr["CreatedBy"] ? null : new PUser()
                            {
                                ContactName = Convert.ToString(dr["CreatedByName"])
                            };
                            Customer.CreatedOn = dr["CreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedOn"]);
                            Customer.SendSAP = dr["SendSAP"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SendSAP"]);
                            Customer.Success = dr["Success"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Success"]);
                            Customers.Add(Customer);
                            RowCount = Convert.ToInt32(dr["RowCount"]);
                        }
                    }
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "GetCustomerChangeForApproval", ex);
                throw ex;
            }
            return Customers;
        }
        public PDMS_CustomerChangeForApproval GetCustomerChangeForApprovalByID(long CustomerChangeForApprovalID)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_CustomerChangeForApproval Customer = null;
            try
            {
                DbParameter CustomerChangeForApprovalIDP = provider.CreateParameter("CustomerChangeForApprovalID", CustomerChangeForApprovalID, DbType.Int64);
                DbParameter[] Params = new DbParameter[1] { CustomerChangeForApprovalIDP };

                using (DataSet DataSet = provider.Select("GetCustomerChangeForApprovalByID", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Customer = new PDMS_CustomerChangeForApproval();
                            Customer.CustomerChangeForApprovalID = Convert.ToInt64(dr["CustomerChangeForApprovalID"]);
                            Customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                            Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                            Customer.Unregistered = Convert.ToBoolean(dr["Unregistered"]);
                            Customer.GSTIN = Convert.ToString(dr["GST"]);
                            Customer.PAN = Convert.ToString(dr["PAN"]);
                            Customer.IsApproved = dr["IsApproved"] == DBNull.Value ? (bool?)null : Convert.ToBoolean(dr["IsApproved"]);

                            Customer.ApprovedBy = DBNull.Value == dr["ApprovedBy"] ? null : new PUser()
                            {
                                ContactName = Convert.ToString(dr["ApprovedByName"])
                            };
                            Customer.ApprovedOn = dr["ApprovedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["ApprovedOn"]);
                            Customer.CreatedBy = DBNull.Value == dr["CreatedBy"] ? null : new PUser()
                            {
                                ContactName = Convert.ToString(dr["CreatedByName"])
                            };
                            Customer.CreatedOn = dr["CreatedOn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["CreatedOn"]);
                            Customer.SendSAP = dr["SendSAP"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["SendSAP"]);
                            Customer.Success = dr["Success"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Success"]);
                        }
                    }
                }                
                TraceLogger.Log(DateTime.Now);
                return Customer;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "GetCustomerChangeForApprovalByID", ex);
                throw ex;
            }
        }

        public List<PCustomerEmployeeDesignation> GetCustomerEmployeeDesignation(int? DesignationID, string Designation)
        {
            string endPoint = "Customer/CustomerEmployeeDesignation?DesignationID=" + DesignationID + "&Designation=" + Designation;
            return JsonConvert.DeserializeObject<List<PCustomerEmployeeDesignation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PDMS_Customer getCustomerAddressFromSAP(string CustomerCode)
        {
            string endPoint = "Customer/getCustomerAddressFromSAP?CustomerCode=" + CustomerCode;
            return JsonConvert.DeserializeObject<PDMS_Customer>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data)); 
        }
    }
}
