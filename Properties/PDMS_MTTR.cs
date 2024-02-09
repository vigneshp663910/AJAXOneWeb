using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Properties
{
    [Serializable]
    public class PDMS_MTTR
    {
        public string sno { get; set; }
        public string f_ic_ticket_id { get; set; }
        public DateTime? f_call_login_date { get; set; }
        public string f_call_login_time { get; set; }
        public string problem_reported { get; set; }
        public string status1 { get; set; }
        public string status2 { get; set; }
        public string PsrStatus { get; set; }
        public string reason_for_closure { get; set; }
        public string f_cust_id { get; set; }
        public string d_customer_name { get; set; }
        public string present_mc_city { get; set; }
        public string present_mc_dist { get; set; }
        public string present_mc_state { get; set; }
        public string present_mc_region { get; set; }
        public string mc_slno { get; set; }
        public string mc_desc { get; set; }
        public DateTime? warranty_date_start { get; set; }
        public DateTime? warranty_date_end { get; set; }
        public string dealer_code { get; set; }
        public string dealer_name { get; set; }
        public string prior_srv_order { get; set; }
        public DateTime? ser_req_date { get; set; }
        public string ser_rec_time { get; set; }
        public DateTime? ser_rec_date { get; set; }
        public DateTime? ser_res_date { get; set; }
        public string iBaseNo { get; set; }
        public string compcode { get; set; }
        public string complaintdesc { get; set; }
        public string ser_ord_no { get; set; }
        public string ser_item { get; set; }
        public string ser_rec_no { get; set; }
        public string ser_id { get; set; }
        public string ser_name { get; set; }
        public string code { get; set; }
        public string description1 { get; set; }
        public string contact_person { get; set; }
        public string contact_person_pre { get; set; }
        public string psro { get; set; }
        public DateTime? psrdate { get; set; }
        public string flag { get; set; }

        public int? MTTR1 { get; set; }
        public int? MTTR2 { get; set; }


        public string r_application { get; set; }
        public int? r_counter_end { get; set; }


        public string r_priority_class_desc { get; set; }  //PSR
        public string r_priority_desc { get; set; }  //PSC
        public string f_part_id { get; set; }

        public string p_sr_id { get; set; }
        public string f_technician_id { get; set; }
        public string s_status { get; set; }

        public string r_tsir_no { get; set; }
        public string CustomerSatisfactionLevel { get; set; }
        public int? TotalMandays { get; set; }
        public List<PBreakdown> Breakdown { get; set; }
        public string p_sc_tech_id { get; set; }
        
    }
    [Serializable]
    public class PBreakdown
    {
        public string BreakdownNoteType { get; set; }
        public string BreakdownReason { get; set; }
        public string BreakdownDetails { get; set; }
    }
    [Serializable]
    public class PDMS_MTTR_New
    {
        public long ICTicketID { get; set; }
        public PDMS_ICTicket ICTicket { get; set; }  
        public string present_mc_region { get; set; } 
        public string prior_srv_order { get; set; } 
        public string compcode { get; set; }
        public string complaintdesc { get; set; }  
        public string code { get; set; }
        public string description1 { get; set; } 
        public string flag { get; set; }
        public int? MTTR1 { get; set; }
        public int? MTTR2 { get; set; }
        public int? MTTR1H { get; set; }
        public int? MTTR2H { get; set; }
        public int StatusSince { get; set; } 
        
        public int? r_counter_end { get; set; } 
        public string r_priority_class_desc { get; set; }  //PSR
        public string r_priority_desc { get; set; }  //PSC
        public string f_part_id { get; set; }
        public string p_sr_id { get; set; }  
        public int TotalMandays { get; set; }
        public decimal TotalHours { get; set; }
        public List<PBreakdown> Breakdown { get; set; }
        public Boolean IsNew { get; set; }

        public int? Response { get; set; }
        public int? Resolution { get; set; }
        public int? Restore { get; set; }
    }
   
}

//       p_sc_id, s_establishment, s_tenant_id, f_sr_id, f_currency, r_net_amount, 
//       s_modified_by, r_closure_date, s_action, r_tax_amt, r_total_amt, 
//       r_claim_status, s_created_on, r_date_of_first_res, s_status, 
//       f_cust_id, r_in_amc, is_ack, r_in_warranty, r_actual_closure_date, 
//       s_sync_status, f_franchise_id, r_priority, r_discount_amt, s_created_by, 
//       f_crm_sc_id, s_modified_on, r_category2, f_crm_service_id, r_category1, 
//       r_category4, r_category3, r_date_of_req, r_priority_class, f_equipment_id, 
//       channel, s_object_type, r_addn_discount, r_category2_desc, r_priority_desc, 
//       r_category4_desc, r_category3_desc, r_category1_desc, f_division, 
//       r_claim_id, r_inv_id, r_ref_obj_name, r_ref_obj_type, r_inv_ref_id, 
//       r_ro_id, r_equipment_ser_no, r_fsr_no_date, r_tsir_no, r_application, 
//       r_remarks, r_date_warr_expiry, r_product_id, f_ref_key, r_address, 
//       r_postal_code, r_contact_no, r_email_id, s_last_request_id, s_last_request_index, 
//       r_location_id, r_office_id, r_filter, f_ref_psc_id, r_longitude, 
//       r_lattitude, r_location, f_ic_ticket_id, r_psc_create_date, f_ic_ticket_date, 
//       r_assigned_date, r_question_id, r_answer_id, r_answer_key, r_goodwill_warranty
