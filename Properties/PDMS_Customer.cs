using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_Customer
    {
        public int CustomerID { get; set; } 
        public string CustomerCode { get; set; }
        public string CustomerCodeWithOutZero {
            get
            {
                if (string.IsNullOrEmpty(CustomerCode))
                    return "";
                return Convert.ToString(Convert.ToInt64(CustomerCode));
            }
            
        }
        public string CustomerName { get; set; }
        public string OrgName { get; set; }
        public string OfficeID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public PDMS_State StateN { get; set; }
        public PDMS_District District { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string GSTIN { get; set; }
        public string PAN { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string ContactPerson  { get; set; }
        public string Address12 { get; set; }
        public PDMS_CustomerCategory CustomerCategory { get; set; }
    }
    [Serializable]
    public class PDMS_CustomerCategory
    {
        public int CustomerCategoryID { get; set; }
        public string CustomerCategory { get; set; }        
    }

    public class PDMS_CustomerJSON
    {
        public string fromentityname { get; set; }
        public string tenant { get; set; }
        public string flavor { get; set; }
        public string establishment { get; set; }
        public string msg_id { get; set; }
        public string msg_type { get; set; }
        public IEnumerable<PDMS_resultsJSON> results { get; set; }

    }
    public class PDMS_resultsJSON
    {
        public string p_bp_id { get; set; }
        public string p_bp_type { get; set; }
        public string r_org_name { get; set; }
        public string r_def_lang { get; set; }
        public string r_valid_to { get; set; }
        public string r_org_nick_name { get; set; }
        public string r_valid_form { get; set; }
        public string r_def_address_id { get; set; }
        public string r_src_bp_tenant_id { get; set; }
        public string r_bp_tenant_id { get; set; }
        public string r_zone { get; set; }
        public string r_def_currency { get; set; }
        public string r_is_default_estab { get; set; }
        public string r_bp_role { get; set; }
        public string r_primary_contact_id { get; set; }
        public string r_src_bp_id { get; set; }
        public string r_date_era { get; set; }
        public string r_status { get; set; }
        public string r_bp_owner { get; set; }
        public string r_src_bp_est { get; set; }
        public string r_def_date_format { get; set; }
        public string r_frieght_p { get; set; }
        public string r_insurance_p { get; set; }
        public string r_cr_limit_actv { get; set; }
        public string r_ord_actv { get; set; }
        public string r_del_actv { get; set; }
        public string r_bp_establishment { get; set; }
        public string f_pay_term { get; set; }
        public string is_ack { get; set; }
        public string r_longitude { get; set; }
        public string r_latitude { get; set; }
        public string s_establishment { get; set; }
        public string s_tenant_id1 { get; set; }
        public string s_modified_by { get; set; }
        public string s_created_by { get; set; }
        public string s_created_on1 { get; set; }
        public string s_modified_on1 { get; set; }
        public string s_action { get; set; }
        public string s_status { get; set; }
        public string s_sync_status { get; set; }
        public string s_object_type { get; set; }
        public string channel { get; set; }   
        public IEnumerable<PDMS_AddressJSON> bp_address { get; set; }
        public IEnumerable<PDMS_statutoryJSON> bp_statutory { get; set; }
    }
    public class PDMS_AddressJSON
    {
        public string p_bp_id { get; set; }
        public string p_office_id { get; set; }
        public string p_office_type_id { get; set; }
        public string r_address1 { get; set; }
        public string r_address2 { get; set; }
        public string r_city { get; set; }
        public string r_country { get; set; }
        public string r_fax { get; set; }
        public string r_landline_no { get; set; }
        public string r_landmark { get; set; }
        public string r_latitude { get; set; }
        public string r_longitude { get; set; }
        public string r_office_desc { get; set; }
        public string r_postal_code { get; set; }
        public string r_primary_contact_id { get; set; }
        public string r_state { get; set; }
        public string r_zone { get; set; }
        public string s_tenant_id1 { get; set; }
        public IEnumerable<PDMS_ContactJSON> bp_contact { get; set; }
    }
    public class PDMS_ContactJSON
    {
        public string p_bp_id { get; set; }
        public string p_contact_id { get; set; }
        public string r_contact_per_name { get; set; }
        public string r_contact_type { get; set; }
        public string r_office_id { get; set; }
        public string r_value { get; set; }
        public string s_tenant_id1 { get; set; }
    }
    public class PDMS_statutoryJSON
    {
        public string p_bp_id { get; set; }
        public string p_statutory_id { get; set; }
        public string r_issued_by { get; set; }
        public string r_statutory_type { get; set; }
        public string r_value { get; set; }
        public string s_tenant_id1 { get; set; }
    }
}
