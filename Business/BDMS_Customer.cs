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
        public int IntegrationCustomer(string[] CustomerFiles)
        {
            TraceLogger.Log(DateTime.Now);
            new FileLogger().LogMessageService("Started", "Customer Integration", null);
            List<string> querys = new List<string>();
            PDMS_CustomerJSON Customers = new PDMS_CustomerJSON();
            try
            {
                foreach (string file in CustomerFiles)
                {

                    try
                    {
                        string json = File.ReadAllText(file);
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        Customers = ser.Deserialize<PDMS_CustomerJSON>(json);
                        foreach (PDMS_resultsJSON C in Customers.results)
                        {
                            querys = new List<string>();
                            DbParameter CustomerCode = provider.CreateParameter("CustomerCode", C.p_bp_id, DbType.String);
                            DbParameter CustomerName = provider.CreateParameter("CustomerName", C.r_org_name, DbType.String);
                            DbParameter[] Params = new DbParameter[2] { CustomerCode, CustomerName };
                            try
                            {
                                provider.Insert("ZDMS_InsertOrUpdateCustomer", Params);
                                if (string.IsNullOrEmpty(C.r_src_bp_tenant_id))
                                {
                                    continue;
                                }
                                string ch = new NpgsqlServer().ExecuteScalar(" select p_bp_id from doohr_bp where p_bp_id = '" + C.p_bp_id + "' and  s_tenant_id = " + C.r_src_bp_tenant_id);
                                if (string.IsNullOrEmpty(ch))
                                {
                                    if (string.IsNullOrEmpty(C.r_bp_tenant_id))
                                        C.r_bp_tenant_id = "null";
                                    if (string.IsNullOrEmpty(C.r_is_default_estab))
                                        C.r_is_default_estab = "false";
                                    if (string.IsNullOrEmpty(C.r_date_era))
                                        C.r_date_era = "null";
                                    if (string.IsNullOrEmpty(C.r_src_bp_est))
                                        C.r_src_bp_est = "null";
                                    if (string.IsNullOrEmpty(C.r_src_bp_est))
                                        C.r_src_bp_est = "null";

                                    if (string.IsNullOrEmpty(C.r_frieght_p))
                                        C.r_frieght_p = "null";
                                    if (string.IsNullOrEmpty(C.r_insurance_p))
                                        C.r_insurance_p = "null";
                                    if (string.IsNullOrEmpty(C.r_cr_limit_actv))
                                        C.r_cr_limit_actv = "null";
                                    if (string.IsNullOrEmpty(C.r_ord_actv))
                                        C.r_ord_actv = "null";
                                    if (string.IsNullOrEmpty(C.is_ack))
                                        C.is_ack = "null";

                                    if (string.IsNullOrEmpty(C.r_del_actv))
                                        C.r_del_actv = "null";

                                    string r_org_name = C.r_org_name.Replace("'", "''");
                                    string r_org_nick_name = C.r_org_nick_name.Replace("'", "''");
                                    string Query = "INSERT INTO public.doohr_bp( "
    + " s_establishment,    p_bp_id,            p_bp_type,           s_tenant_id,           r_org_name,            r_def_lang,           r_valid_to,           r_org_nick_name,           r_valid_form,           s_modified_by,            r_def_address_id,           r_src_bp_tenant_id,          r_bp_tenant_id,          r_zone,             s_created_by,           s_created_on,           r_def_currency,           r_is_default_estab,           r_bp_role,            r_primary_contact_id,            r_src_bp_id,           s_modified_on,          r_date_era,           r_status,            r_bp_owner,           r_src_bp_est,           r_def_date_format,           r_frieght_p,          r_insurance_p,          r_cr_limit_actv,          r_ord_actv,          r_del_actv,          r_bp_establishment,          f_pay_term,           s_status,           s_object_type,           s_sync_status,            s_action,            channel,           is_ack,           r_longitude,          r_latitude)"
    + " VALUES (1000,'" + C.p_bp_id + "','" + C.p_bp_type + "'," + C.r_src_bp_tenant_id + ",'" + r_org_name + "','" + C.r_def_lang + "','" + C.r_valid_to + "','" + r_org_nick_name + "','" + C.r_valid_form + "','" + C.s_modified_by + "','" + C.r_def_address_id + "'," + C.r_src_bp_tenant_id + "," + C.r_bp_tenant_id + ",'" + C.r_zone + "','AF',now(),'" + C.r_def_currency + "'," + C.r_is_default_estab + ",'" + C.r_bp_role + "','" + C.r_primary_contact_id + "','" + C.r_src_bp_id + "',now()," + C.r_date_era + ",'" + C.r_status + "','" + C.r_bp_owner + "'," + C.r_src_bp_est + ",'" + C.r_def_date_format + "'," + C.r_frieght_p + "," + C.r_insurance_p + "," + C.r_cr_limit_actv + "," + C.r_ord_actv + "," + C.r_del_actv + "," + C.r_bp_establishment + "," + C.f_pay_term + ",'" + C.s_status + "'," + C.s_object_type + ",'" + C.s_sync_status + "','" + C.s_action + "','" + C.channel + "'," + C.is_ack + ",'" + C.r_longitude + "','" + C.r_latitude + " ')";

                                    querys.Add(Query);
                                    foreach (PDMS_AddressJSON A in C.bp_address)
                                    {
                                        string r_address1 = A.r_address1.Replace("'", "''");
                                        string r_city = A.r_city.Replace("'", "''");
                                        string r_address2 = A.r_address2.Replace("'", "''");
                                        Query = "INSERT INTO public.doohr_bp_address( "
                   + " s_establishment,   p_bp_id,            s_tenant_id,            p_office_id,            r_fax,              r_city,          s_modified_by,            r_country,            r_landline_no,            p_office_type_id,            r_landmark,            r_state,            r_zone,            s_created_by,           s_created_on,           r_postal_code,            r_primary_contact_id,           s_modified_on,           r_address1,            r_office_desc,            channel,            r_latitude,            r_longitude,            r_address2)"
                   + " VALUES (1000,'" + A.p_bp_id + "'," + C.r_src_bp_tenant_id + ",'" + A.p_office_id + "','" + A.r_fax + "','" + r_city + "','" + C.s_modified_by + "','" + A.r_country + "','" + A.r_landline_no + "','" + A.p_office_type_id + "','" + A.r_landmark + "','" + A.r_state + "','" + A.r_zone + "','" + C.s_created_by + "',now(),'" + A.r_postal_code + "','" + A.r_primary_contact_id + "',now(),'" + r_address1 + "','" + A.r_office_desc + "','" + C.channel + "','" + A.r_latitude + "','" + A.r_longitude + "','" + r_address2 + "')";
                                        querys.Add(Query);
                                        foreach (PDMS_ContactJSON Co in A.bp_contact)
                                        {
                                            Query = "INSERT INTO public.doohr_bp_contact( "
                  + " s_establishment,    p_bp_id,             p_contact_id,            s_tenant_id,            r_contact_type,           s_modified_on,            r_office_id,            s_modified_by,             r_value,            s_created_by,           s_created_on,            r_contact_per_name,            s_sync_status,            s_action,            channel,           is_ack) "
                  + " VALUES (1000,'" + Co.p_bp_id + "','" + Co.p_contact_id + "'," + C.r_src_bp_tenant_id + ",'" + Co.r_contact_type + "',now(),'" + Co.r_office_id + "','" + C.s_modified_by + "','" + Co.r_value + "','" + C.s_created_by + "',now(),'" + Co.r_contact_per_name + "','" + C.s_sync_status + "','" + C.s_action + "','" + C.channel + "'," + C.is_ack + ")";
                                            querys.Add(Query);
                                        }
                                    }
                                    foreach (PDMS_statutoryJSON S in C.bp_statutory)
                                    {
                                        Query = "INSERT INTO public.doohr_bp_statutory( "
        + " s_establishment,   p_bp_id,            p_statutory_id,           s_tenant_id,           r_statutory_type,           s_modified_on,           s_modified_by,            r_value,            s_created_by,           s_created_on,           r_issued_by,            s_action,            channel,           is_ack)"
        + " VALUES (1000,'" + S.p_bp_id + "','" + S.p_statutory_id + "'," + C.r_src_bp_tenant_id + ",'" + S.r_statutory_type + "',now(),'" + C.s_modified_by + "','" + S.r_value + "','" + C.s_created_by + "',now(),'" + S.r_issued_by + "','" + C.s_action + "','" + C.channel + "'," + C.is_ack + ")";
                                        querys.Add(Query);
                                    }
                                }
                            }
                            catch (SqlException sqlEx)
                            {
                                new FileLogger().LogMessageService("BDMS_Customer", "IntegrationCustomer", sqlEx);

                                throw;
                            }
                            catch (Exception ex)
                            {
                                new FileLogger().LogMessageService("BDMS_Customer", " IntegrationCustomer", ex);
                                throw;
                            }
                        }
                        new NpgsqlServer().UpdateTransactions(querys);
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\Processed"));
                    }
                    catch (Exception e1)
                    {
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\FAILED"));
                        new FileLogger().LogMessageService("BDMS_Customer", "IntegrationCustomer", e1);
                        //  throw e1;
                    }

                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Customer", "IntegrationCustomer", ex);
                throw ex;
            }
            new FileLogger().LogMessageService("End", "Customer Integration", null);
            if (Customers.results != null)
                return Customers.results.Count();
            else
                return 0;

        }

        public List<PDMS_Customer> GetCustomer(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                string Query = "select bp.s_tenant_id,t.description,bp.p_bp_id,bp.r_org_name,bpa.p_office_id as office_id,bpa.r_address1,bpa.r_address2,r_city,r_state,r_postal_code"
 + " ,GSTIN.r_value as GSTIN,PAN.r_value as PAN,MOBILE.r_value as MOBILE,EMAIL.r_value as EMAIL from "
  + " doohr_bp bp  "
  + " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20  "
  + " left join doohr_bp_statutory GSTIN on GSTIN.p_bp_id = bp.p_bp_id and GSTIN.r_statutory_type = 'GSTIN' and GSTIN.s_tenant_id = bp.s_tenant_id "
  + " left join doohr_bp_statutory PAN on PAN.p_bp_id = bp.p_bp_id and PAN.r_statutory_type = 'PAN' and PAN.s_tenant_id = bp.s_tenant_id "
  + " left join m_tenant t on t.tenantid = bp.s_tenant_id"
  + " left join doohr_bp_contact MOBILE on MOBILE.p_bp_id = bp.p_bp_id and bpa.p_office_id = MOBILE.r_office_id and MOBILE.r_contact_type = 'MOBILE' and MOBILE.s_tenant_id = bp.s_tenant_id   "
  + " left join doohr_bp_contact EMAIL on EMAIL.p_bp_id = bp.p_bp_id and bpa.p_office_id = EMAIL.r_office_id and EMAIL.r_contact_type = 'EMAIL' and EMAIL.s_tenant_id = bp.s_tenant_id   "
  + filter
  + " group by bp.s_tenant_id,t.description,bp.p_bp_id,bp.r_org_name,bpa.p_office_id,bpa.r_address1,bpa.r_address2,r_city,r_state,r_postal_code,GSTIN.r_value,PAN.r_value,MOBILE.r_value,EMAIL.r_value";
                DataTable dt = new NpgsqlServer().ExecuteReader(Query);
                //   DataTable dt = new NpgsqlServer().ExecuteReader("SELECT  * from pr_get_customer(" + filter + ")");
                PDMS_Customer Customer = new PDMS_Customer();
                foreach (DataRow dr in dt.Rows)
                {
                    Customer = new PDMS_Customer();
                    Customer.CustomerCode = Convert.ToString(dr["p_bp_id"]);
                    Customer.CustomerName = Convert.ToString(dr["r_org_name"]).Trim();
                    // Customer.OrgName = Convert.ToString(dr["r_org_name"]).Trim();
                    Customer.OfficeID = Convert.ToString(dr["Office_ID"]).Trim();
                    Customer.Address1 = Convert.ToString(dr["r_address1"]).Trim();
                    Customer.Address2 = Convert.ToString(dr["r_address2"]).Trim();
                    Customer.City = Convert.ToString(dr["r_city"]).Trim();
                    Customer.State = new PDMS_State() { State = Convert.ToString(dr["r_state"]).Trim() };
                    Customer.Pincode = Convert.ToString(dr["r_postal_code"]).Trim();
                    //  Customer.DealarCode = Convert.ToString(dr["s_tenant_id"]);
                    //   Customer.DealarName = Convert.ToString(dr["description"]);
                    Customer.GSTIN = Convert.ToString(dr["GSTIN"]).Trim();
                    Customer.PAN = Convert.ToString(dr["PAN"]).Trim();
                    Customer.Email = Convert.ToString(dr["EMAIL"]).Trim();
                    Customer.Mobile  = Convert.ToString(dr["MOBILE"]).Trim();
                    Customers.Add(Customer);
                }
                return Customers;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return Customers;
        }
        public List<PDMS_Dealer> GetCustomerAdress(string filter)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Dealer> Customers = new List<PDMS_Dealer>();
            try
            {

                DataTable dt = new NpgsqlServer().ExecuteReader("SELECT  * from pr_get_customer_address(" + filter + ")");
                PDMS_Dealer Customer = new PDMS_Dealer();
                foreach (DataRow dr in dt.Rows)
                {
                    Customer = new PDMS_Dealer();
                    Customer.DealerCode = Convert.ToString(dr["p_bp_id"]);
                    //      Customer.DealerName = Convert.ToString(dr["r_org_name"]);
                    Customer.Address1 = Convert.ToString(dr["address1"]);
                    Customer.Address2 = Convert.ToString(dr["address2"]);
                    Customer.City = Convert.ToString(dr["city"]);
                    Customer.State = Convert.ToString(dr["state"]);
                    Customer.Country = Convert.ToString(dr["country"]);
                    Customer.Pincode = Convert.ToString(dr["postal"]);

                    Customer.Email = Convert.ToString(dr["email"]);
                    Customer.Mobile = Convert.ToString(dr["mobile"]);

                    Customer.GSTIN = Convert.ToString(dr["gstin"]);
                    Customers.Add(Customer);
                }
                return Customers;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_MTTR", "GetMttr", ex);
                throw ex;
            }
            return Customers;
        }

        public List<PDMS_Customer> GetCustomerName()
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DataTable dt = new NpgsqlServer().ExecuteReader("select  p_bp_id   ,r_org_name  FROM      doohr_bp   where    p_bp_type = 'CU'    group by p_bp_id,r_org_name");
                PDMS_Customer Customer = new PDMS_Customer();
                foreach (DataRow dr in dt.Rows)
                {
                    Customer = new PDMS_Customer();
                    Customer.CustomerCode = Convert.ToString(dr["p_bp_id"]);
                    Customer.CustomerName = Convert.ToString(dr["r_org_name"]);
                    Customers.Add(Customer);
                }
                return Customers;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "GetCustomerName", ex);
                throw ex;
            }
            return Customers;
        }
        public int InsertOrUpdateCustomer(string CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);

            PDMS_CustomerJSON Customers = new PDMS_CustomerJSON();
            try
            {
                string folderPath = "\\\\192.168.19.35\\Clarion\\DCONNECT\\out";
                // foreach (string file in Directory.EnumerateFiles(folderPath, "*.json"))
                foreach (string file in Directory.GetFiles(folderPath, "doohr_bp*.json"))
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        JavaScriptSerializer ser = new JavaScriptSerializer();
                        Customers = ser.Deserialize<PDMS_CustomerJSON>(json);
                        foreach (PDMS_resultsJSON C in Customers.results)
                        {
                            if (!InsertOrUpdateCustomer(C.p_bp_id, C.r_org_name))
                            {
                                throw new Exception();
                            }
                        }
                        File.Move(file, file.Replace("DCONNECT", "DCONNECT\\Processed"));
                    }
                    catch (Exception e1)
                    {

                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Material", "IntegrationMaterial", ex);
                throw ex;
            }
            return Customers.results.Count();
        }
        public Boolean InsertOrUpdateCustomer(string CustomerCode, string CustomerName)
        {
            TraceLogger.Log(DateTime.Now);
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                    DbParameter CustomerNameP = provider.CreateParameter("CustomerName", CustomerName, DbType.String);
                    DbParameter[] Params = new DbParameter[2] { CustomerCodeP, CustomerNameP };
                    provider.Insert("ZDMS_InsertOrUpdateCustomer", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (Exception e1)
            {
            }
            TraceLogger.Log(DateTime.Now);
            return false;
        }
        public int IntegrationCustomerFromPG()
        {
            TraceLogger.Log(DateTime.Now);
            new FileLogger().LogMessageService("Started", "Customer Integration From PG", null);
            List<PDMS_Customer> CustomerS = GetCustomerNameFromPG();
            int Count = 0;
            try
            {

                foreach (PDMS_Customer Customer in CustomerS)
                {
                    DbParameter CustomerCode = provider.CreateParameter("CustomerCode", Customer.CustomerCode, DbType.String);
                    DbParameter CustomerName = provider.CreateParameter("CustomerName", Customer.CustomerName, DbType.String);
                    DbParameter GSTIN = provider.CreateParameter("GSTIN", Customer.GSTIN, DbType.String);
                    DbParameter State = provider.CreateParameter("State", Customer.State.State, DbType.String);
                    DbParameter[] Params = new DbParameter[4] { CustomerCode, CustomerName, GSTIN, State };

                    provider.Insert("ZDMS_InsertOrUpdateCustomerPG", Params);
                }
                TraceLogger.Log(DateTime.Now);
                Count = CustomerS.Count;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessageService("BDMS_Customer", "IntegrationCustomer", ex);
                throw ex;
            }
            new FileLogger().LogMessageService("End", "Customer Integration", null);
            return Count;
        }
        public List<PDMS_Customer> GetCustomerNameFromPG()
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DataTable dt = new NpgsqlServer().ExecuteReader("select bp.p_bp_id, bp.r_org_name,bps.r_value as  GSTIN, r_state  from doohr_bp bp left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' "
                + " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where  p_office_type_id='ST'  group by bp.p_bp_id, bp.r_org_name,bps.r_value,r_state");
                PDMS_Customer Customer = new PDMS_Customer();
                foreach (DataRow dr in dt.Rows)
                {
                    Customer = new PDMS_Customer();
                    Customer.CustomerCode = Convert.ToString(dr["p_bp_id"]);
                    Customer.CustomerName = Convert.ToString(dr["r_org_name"]);
                    Customer.GSTIN = Convert.ToString(dr["GSTIN"]);
                    Customer.State = new PDMS_State() { State = Convert.ToString(dr["r_state"]) };
                    Customers.Add(Customer);
                }
                return Customers;
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Customer", "GetCustomerName", ex);
                throw ex;
            }
            return Customers;
        }

        public PDMS_Customer GetCustomerAddressFromPG(String DealerCode, String CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_Customer Customer = new PDMS_Customer();
            try
            {
                DataTable dt = new NpgsqlServer().ExecuteReader("select   bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value as  GSTIN ,bpsPan.r_value as  PAN from doohr_bp bp "
+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' left Join  doohr_bp_statutory bpsPan on bpsPan.p_bp_id =bp.p_bp_id and bpsPan.r_statutory_type='PAN' "
+ " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where  p_office_type_id='ST' and bp.p_bp_id = '" + CustomerCode + "' and  bp.s_tenant_id = " + DealerCode
+ "group by  bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value  ,bpsPan.r_value ");
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
        public PDMS_Customer GetCustomerAddressFromPG_p_office_id(String DealerCode, String CustomerCode,string p_office_id)
        {
            TraceLogger.Log(DateTime.Now);
            PDMS_Customer Customer = new PDMS_Customer();
            try
            {
                DataTable dt = new NpgsqlServer().ExecuteReader("select   bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value as  GSTIN ,bpsPan.r_value as  PAN from doohr_bp bp "
+ " left Join  doohr_bp_statutory bps on bps.p_bp_id =bp.p_bp_id and bps.r_statutory_type='GSTIN' left Join  doohr_bp_statutory bpsPan on bpsPan.p_bp_id =bp.p_bp_id and bpsPan.r_statutory_type='PAN' "
+ " left join doohr_bp_address bpa on bpa.p_bp_id= bp.p_bp_id and bp.s_tenant_id = bpa.s_tenant_id and bp.s_tenant_id <> 20 where    p_office_id = '" + p_office_id + "' and bp.p_bp_id = '" + CustomerCode + "' and  bp.s_tenant_id = " + DealerCode
+ " group by  bp.p_bp_id, bp.r_org_name,r_address1,r_address2,r_city,r_state,r_postal_code,bps.r_value  ,bpsPan.r_value ");
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
                //  Customer.MOBILE = ConfigurationManager.AppSettings["EInvoiveDate"]; 

                
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
        public List<PDMS_Customer>  GetCustomerSQL(int? CustomerID, string CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DbParameter CustomerIDP = provider.CreateParameter("CustomerID", CustomerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                DbParameter[] Params = new DbParameter[2] { CustomerIDP, CustomerCodeP };

                   PDMS_Customer Customer = new PDMS_Customer();
                   using (DataSet DataSet = provider.Select("ZDMS_GetCustomer", Params))
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
                PDMS_Customer cust = new SCustomer().getCustomerAddress(CustomerCode);
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


        public List<PDMS_Customer> GetCustomer(long? CustomerID, string CustomerCode, string CustomerName, string Mobile, int? CountryID, int? StateID, int? DistrictID)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer?CustomerID=" + CustomerID + "&CustomerCode=" + CustomerCode + "&CustomerName=" + CustomerName + "&Mobile=" + Mobile
                + "&CountryID=" + CountryID + "&StateID=" + StateID + "&DistrictID=" + DistrictID;
            return JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
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
        public List<PCustomerAttribute> GetCustomerAttribute(long? CustomerID,int? CreatedBy)
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
            string endPoint = "Customer/ResponsibleEmployee?CustomerResponsibleEmployeeID=" + CustomerResponsibleEmployeeID + "&CustomerID=" + CustomerID ;
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
        public  PAttachedFile GetAttachedFileCustomerForDownload(string DocumentName)
        {
            string endPoint = "Customer/AttachedFileForDownload?DocumentName=" + DocumentName;
            return JsonConvert.DeserializeObject<PAttachedFile>(new BAPI().ApiGet(endPoint));
        }

        public List<PDMS_Customer> GetCustomerAutocomplete(string Customer)
        {
            TraceLogger.Log(DateTime.Now);
            string endPoint = "Customer/CustomerAutocomplete?Customer=" + Customer;
            return JsonConvert.DeserializeObject<List<PDMS_Customer>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
            
        }
        public List<PDMS_Customer> GetDealerCustomer(int? DealerID, string DealerCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
                DbParameter DealerCodeP = provider.CreateParameter("DealerCode", DealerCode, DbType.String);
                DbParameter[] Params = new DbParameter[2] { DealerIDP, DealerCodeP };

                PDMS_Customer Customer = new PDMS_Customer();
                using (DataSet DataSet = provider.Select("ZDMS_GetDealerCustomer", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            Customer = new PDMS_Customer();
                            Customer.CustomerID = Convert.ToInt32(dr["CustomerID"]);
                            Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                            Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                            Customers.Add(Customer);
                        }
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("GetDealerCustomer", "ZDMS_GetDealerCustomer", ex);
                throw ex;
            }
            return Customers;
        }
        public int InsertOrUpdateDealerCustomerMapping(int? DealerCustomerMappingID, int? DealerID, string CustomerCode, int UserID, Boolean IsActive)
        {
            int result = 0;
            TraceLogger.Log(DateTime.Now);
            DbParameter DealerCustomerMappingIDP = provider.CreateParameter("DealerCustomerMappingID", DealerCustomerMappingID, DbType.Int32);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter OutValue = provider.CreateParameter("OutValue", 0, DbType.Int32, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[6] { DealerCustomerMappingIDP, DealerIDP, CustomerCodeP, UserIDP, IsActiveP, OutValue };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    result=provider.Insert("ZDMS_InsertOrUpdateDealerCustomerMapping", Params);
                    if (result != 0)
                    {
                        result = Convert.ToInt32(OutValue.Value);
                    }
                    scope.Complete();
                }
                return result;
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
            return result;
        }
        public List<PDMS_Customer> GetCustomerFromSQL(long? CustomerID, string CustomerCode)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_Customer> Customers = new List<PDMS_Customer>();
            try
            {
                DbParameter CustomerIDP = provider.CreateParameter("CustomerID", CustomerID, DbType.Int32);
                DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                DbParameter[] Params = new DbParameter[2] { CustomerIDP, CustomerCodeP };

                PDMS_Customer Customer = new PDMS_Customer();
                using (DataSet DataSet = provider.Select("ZDMS_GetCustomer", Params))
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
                            Customer.CustomerName2 = Convert.ToString(dr["CustomerName2"]);
                            Customer.Title = new PCustomerTitle();
                            Customer.Title.Title = Convert.ToString(dr["Title"]);
                            Customer.Address1 = Convert.ToString(dr["Address1"]);
                            Customer.Address2 = Convert.ToString(dr["Address2"]);
                            Customer.Tehsil = new PDMS_Tehsil();
                            Customer.Tehsil.Tehsil = Convert.ToString(dr["Tehsil"]);
                            Customer.Address3 = Convert.ToString(dr["Address3"]);
                            Customer.District = new PDMS_District();
                            Customer.District.District = Convert.ToString(dr["District"]);
                            Customer.District.SalesOffice = new PSalesOffice();
                            Customer.District.SalesOffice.SalesOffice = Convert.ToString(dr["SalesOffice"]);
                            Customer.District.SalesOffice.SalesGroup = Convert.ToString(dr["SalesGroup"]);
                            Customer.Pincode = Convert.ToString(dr["Pincode"]);
                            Customer.City = Convert.ToString(dr["City"]);
                            Customer.Country = new PDMS_Country();
                            Customer.Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                            Customer.Country.CountryCode = Convert.ToString(dr["CountryCode"]);
                            Customer.Country.SalesOrganization = Convert.ToString(dr["SalesOrganization"]);
                            Customer.State = new PDMS_State();
                            Customer.State.StateID = Convert.ToInt32(dr["StateID"]);
                            Customer.State.StateCode = Convert.ToString(dr["StateCode"]);
                            Customer.State.Region = new PDMS_Region();
                            Customer.State.Region.Region = Convert.ToString(dr["Region"]);
                            Customer.Mobile = Convert.ToString(dr["Mobile"]);
                            Customer.AlternativeMobile = Convert.ToString(dr["AlternativeMobile"]);
                            Customer.Email = Convert.ToString(dr["EMail"]);
                            Customer.GSTIN = Convert.ToString(dr["GSTNo"]);
                            Customer.PAN = Convert.ToString(dr["PAN"]);
                            Customer.ContactPerson = Convert.ToString(dr["ContactPerson"]);
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
        public int UpdateCustomerCodeFromSapToSql(PDMS_Customer Customer)
        {
            //int? CustomerID, string CustomerCode
            TraceLogger.Log(DateTime.Now);
            int success = 0;
            try
            {
                //List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerFromSQL(CustomerID, null);
               // string CustomerCode = Customer[0].CustomerCode;
                if (string.IsNullOrEmpty(Customer.CustomerCode))
                {
                    string CustomerCode = new SapIntegration.SCustomer().CreateCustomerInSAP(Customer);
                    if (!string.IsNullOrEmpty(CustomerCode))
                    { 
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                        {
                            DbParameter CustomerIDP = provider.CreateParameter("CustomerID", Customer.CustomerID, DbType.Int32);
                            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);

                            DbParameter[] Params = new DbParameter[2] { CustomerIDP, CustomerCodeP };
                            success = provider.Insert("ZDMS_UpdateCustomer", Params);
                            scope.Complete();
                        }
                    }
                }
                else
                {
                    string SUBRC= new SapIntegration.SCustomer().ChangeCustomerInSAP(Customer);
                    if (SUBRC == "0") { success = 1; }
                }
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

            foreach(PDMS_Customer C in Customers)
            {
                InsertOrUpdateCustomerSap(C.CustomerCode);
            }
        }
    }
}
