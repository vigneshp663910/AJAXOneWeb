using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class MTTR_Report : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_MTTR_Report; } }
        public List<PDMS_MTTR> SDMS_MTTR
        {
            get
            {
                if (Session["PDMS_MTTR"] == null)
                {
                    Session["PDMS_MTTR"] = new List<PDMS_MTTR>();
                }
                return (List<PDMS_MTTR>)Session["PDMS_MTTR"];
            }
            set
            {
                Session["PDMS_MTTR"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_MTTR_Report.aspx";
            Session["PageName"] = "MTTR";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                // fillMTTR();
                // FillPageNo(1);
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                txtICLoginDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtICLoginDateTo.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillMTTR();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillMTTR()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                //int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                //int PageNo = Convert.ToInt32(ddlPageNo.SelectedValue);

                //if(ddlDealerCode.SelectedValue=="0")
                //{
                //    lblMessage.Text = "Select dealer";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}
                string QueryMTTR = "SELECT t.f_ic_ticket_id,t.f_call_login_date,t.f_call_login_time,t.problem_reported,t.reason_for_closure, "
                        + " t.f_cust_id,t.d_customer_name ,t.present_mc_city, t.present_mc_dist,t.present_mc_state,t.present_mc_region,t.mc_slno, t.mc_desc,t.warranty_date_start,t.warranty_date_end,t.dealer_code,  "
                         + " t.dealer_name,  t.prior_srv_order,psc.r_date_of_req as ser_req_date, t.ser_rec_time,  psc.r_closure_date as ser_res_date,t.ibaseno,t.compcode, t.complaintdesc,  "
                         + " t.ser_id,t.ser_name,t.ser_ord_no, t.ser_item,t.code, t.description1, pscc.r_counter_end as hmr,		  pscc.r_counter_read_date as hmr_date,  "
                          + "  psc.f_sr_id,psc.r_tsir_no,t.contact_person, t.contact_person_pre,t.flag,t.sno,  "
                      + "  t.psrno,t.psrdate,t.ser_rec_no, psc.r_application,pscc.r_counter_end  ,psr.r_priority_class_desc,r_priority_desc,pscs.f_part_id ,e.r_first_name,psc.r_date_of_first_res,psr.p_sr_id,psr.s_status as psr_status,psr.f_technician_id  "
                        + " from  af_ic_tickets t "
                            + " left join dsprr_psc_hdr psc on psc.f_ic_ticket_id = t.f_ic_ticket_id and  psc.s_tenant_id <> 20 "
                            + " left join dsprr_psr_hdr psr on psr.f_ic_ticket_id = t.f_ic_ticket_id  and  psr.s_tenant_id <> 20 "
                            + " left join dsprr_psc_counter pscc on pscc.f_counter_ref_id = psc.p_sc_id  and  pscc.s_tenant_id <> 20  and pscc.s_modified_by = 'SM_USER' "
                             + " left join dsprr_psc_serv_charge_itms pscs on pscs.p_sc_id= psc.p_sc_id  and  pscs.s_tenant_id <> 20  and pscs.s_modified_by = 'SM_USER' "
                         + " left join dmmer_employee e on e.r_external_emp_id = pscs.f_technician_id  and e.s_tenant_id <> 20  ";


                string QueryMTTRTest = " SELECT  t.f_ic_ticket_id,t.f_call_login_date,t.f_call_login_time,t.problem_reported,t.reason_for_closure,t.f_cust_id,t.d_customer_name ,  "
              + " t.present_mc_city,t.present_mc_dist,t.present_mc_state,t.present_mc_region,t.mc_slno,t.mc_desc,t.warranty_date_start,t.warranty_date_end,   "
              + " t.dealer_code,t.dealer_name,t.prior_srv_order,psc.r_date_of_req as ser_req_date,t.ser_rec_time,psc.r_closure_date as ser_res_date, t.ibaseno,t.compcode,   "
            + " t.complaintdesc,t.ser_id,t.ser_name,t.ser_ord_no, t.ser_item,t.code, t.description1, pscc.r_counter_end as hmr, pscc.r_counter_read_date as hmr_date,   "
              + " psc.f_sr_id, psc.r_tsir_no,pscn.r_note_type as breakdown_note_type,pscnn.r_description as breakdown_reason,pscn.r_comments as breakdown_Details,t.contact_person,   "
          + " t.contact_person_pre,t.flag,t.sno,t.psrno,t.psrdate,t.ser_rec_no, psc.r_application,pscc.r_counter_end,psr.r_priority_class_desc,r_priority_desc,   "
       + " pscs.f_part_id,e.r_first_name,psc.r_date_of_first_res,psr.p_sr_id,psr.s_status as psr_status,psr.f_technician_id,f.r_value Customer_Satisfaction_Level ,pscs.p_sc_tech_id  "
           + " from  af_ic_tickets t  "
               + " left join dsprr_psc_hdr psc on psc.f_ic_ticket_id = t.f_ic_ticket_id and  psc.s_tenant_id <> 20  "
               + " left join dsprr_psr_hdr psr on psr.f_ic_ticket_id = t.f_ic_ticket_id  and  psr.s_tenant_id <> 20  "
               + " left join dsprr_psc_counter pscc on pscc.f_counter_ref_id = psc.p_sc_id  and  pscc.s_tenant_id <> 20  and pscc.s_modified_by = 'SM_USER'  "
               + " left join dsprr_psc_serv_charge_itms pscs on pscs.p_sc_id= psc.p_sc_id  and  pscs.s_tenant_id <> 20  and pscs.s_modified_by = 'SM_USER'  "
               + " left join dmmer_employee e on e.r_external_emp_id = pscs.f_technician_id  and e.s_tenant_id <> 20    "
               + " left join dsprr_psc_note pscn on pscn.s_tenant_id= psc.s_tenant_id and pscn.s_tenant_id <> 20  and  r_note_type ='ZS04' and pscn.f_sc_id =psc.p_sc_id  "
               + " left join dsp_notes_master pscnn  on pscnn.p_note_type =pscn.r_note_type and pscnn.s_tenant_id=pscn.s_tenant_id  "
               + " left join dsprr_feedback_master f on f.p_key = psc.r_answer_key  ";


                string Filter = " Where 1=1";

                if (!string.IsNullOrEmpty(txtICServiceTicket.Text.Trim()))
                {
                    Filter = Filter + " and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                }
                if (!string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()))
                {
                    Filter = Filter + " and t.f_call_login_date >= '" + txtICLoginDateFrom.Text.Trim().Split('/')[1] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[0] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[2] + "'";
                }
                if (!string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()))
                {
                    Filter = Filter + " and t.f_call_login_date <= '" + txtICLoginDateTo.Text.Trim().Split('/')[1] + "/" + txtICLoginDateTo.Text.Trim().Split('/')[0] + "/" + txtICLoginDateTo.Text.Trim().Split('/')[2] + "'";
                }
                if (ddlDealerCode.SelectedValue != "0")
                {
                    Filter = Filter + "and dealer_code = '" + ddlDealerCode.SelectedValue + "'";
                }
                if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                    Filter = Filter + " and t.f_cust_id = '" + txtCustomerCode.Text.Trim() + "'";

                if (ddlPsrStatus.SelectedValue != "0")
                    Filter = Filter + "and psr.s_status = '" + ddlPsrStatus.SelectedValue + "'";



                string groupBy = " group by t.f_ic_ticket_id,t.f_call_login_date,t.f_call_login_time, "
                    + " t.problem_reported,t.reason_for_closure,t.f_cust_id,t.d_customer_name ,t.present_mc_city, t.present_mc_dist, t.present_mc_state, t.present_mc_region, t.mc_slno,  t.mc_desc,  "
                  + " t.warranty_date_start, t.warranty_date_end, t.dealer_code,  t.dealer_name,  t.prior_srv_order, psc.r_date_of_req ,psc.r_closure_date ,t.ibaseno, t.compcode, t.complaintdesc, t.ser_id,  "
                   + " t.ser_name, t.ser_ord_no, t.ser_item,t.code, t.description1, pscc.r_counter_end ,pscc.r_counter_read_date , psc.f_sr_id,  psc.r_tsir_no, t.description1  , "
                       + " t.description1 , t.description1  , t.contact_person, t.contact_person_pre, t.flag,  t.sno, t.psrno,  t.psrdate, t.ser_rec_no , psc.r_application, pscc.r_counter_end  , "
                        + "  psr.r_priority_class_desc, r_priority_desc,  pscs.f_part_id , e.r_first_name, psc.r_date_of_first_res, psr.p_sr_id, psr.s_status  , psr.f_technician_id order by t.f_ic_ticket_id ,t.dealer_code,psr.s_status ";



                //if (!string.IsNullOrEmpty(txtICServiceTicket.Text.Trim()))
                //{
                //    Fillter = txtICServiceTicket.Text.Trim();
                //     Fillter = Fillter + " and t.f_ic_ticket_id = " + txtICServiceTicket.Text.Trim();
                //}
                //else
                //{
                //    Fillter = "null";
                //}
                //if (!string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()))
                //{
                //    Fillter = Fillter + ",'" + txtICLoginDateFrom.Text.Trim().Split('/')[1] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[0] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[2] + "'";
                //    Fillter = Fillter + " and t.f_call_login_date >= '" +  txtICLoginDateFrom.Text.Trim().Split('/')[1] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[0] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[2] + "'";
                //}
                //else
                //{
                //    Fillter = Fillter + "," + "null";
                //}
                //if (!string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()))
                //{
                //    Fillter = Fillter + ",'" + txtICLoginDateTo.Text.Trim().Split('/')[1] + "/" + txtICLoginDateTo.Text.Trim().Split('/')[0] + "/" + txtICLoginDateTo.Text.Trim().Split('/')[2] + "'";
                //}
                //else
                //{
                //    Fillter = Fillter + "," + "null";
                //}

                //if (ddlDealerCode.SelectedValue != "0")
                //{
                //    Fillter = Fillter + ",'" + ddlDealerCode.SelectedValue + "'";
                //}
                //else
                //{
                //    Fillter = Fillter+ "," + "null";
                //}

                //if (!string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
                //    Fillter = Fillter + ",'" + txtCustomerCode.Text.Trim() + "'";
                //else
                //    Fillter = Fillter + "," + "null";

                //if (ddlStatus2.SelectedValue != "0")
                //    Fillter = Fillter + ",'" + ddlStatus2.SelectedValue + "'";
                //else
                //    Fillter = Fillter + "," + "null";


                //if (ddlPsrStatus.SelectedValue != "0")
                //    Fillter = Fillter + ",'" + ddlPsrStatus.SelectedValue + "'";
                //else
                //    Fillter = Fillter + "," + "null";

                //Fillter = Fillter + "," + "10000000";
                //Fillter = Fillter + "," + "0";
                // int RowCount = new BDMS_MTTR().GetMttrCount(Fillter);
                // int t = (RowCount + PageSize - 1) / PageSize;
                // FillPageNo(t);
                // ddlPageNo.SelectedValue = PageNo.ToString();
                // Fillter = Fillter + "," + ddlPageSize.SelectedValue;
                // Fillter = Fillter + "," + ((PageNo - 1) * PageSize).ToString();

                GridView gv = null;
                string Query = "";
                if (rbWithOutText.Checked)
                {
                    Query = QueryMTTR + Filter + groupBy;
                    List<PDMS_MTTR> Mttrs = new BDMS_MTTR().GetMttr(Query, true);

                    if (ddlDealerCode.SelectedValue == "0")
                    {
                        var SOIs1 = (from S in Mttrs
                                     join D in PSession.User.Dealer on S.dealer_code equals D.UserName
                                     select new
                                     {
                                         S
                                     }).ToList();
                        Mttrs.Clear();
                        foreach (var w in SOIs1)
                        {
                            Mttrs.Add(w.S);
                        }
                    }


                    SDMS_MTTR = Mttrs;
                    gvICTickets.PageIndex = 0;
                    gvICTickets.DataSource = Mttrs;
                    gvICTickets.DataBind();
                    gv = gvICTickets;
                }
                else
                {
                    Query = QueryMTTRTest + Filter + " order by t.f_ic_ticket_id ";
                    List<PDMS_MTTR> MttrsWithText = new BDMS_MTTR().GetMttrWithText(Query, true);

                    if (ddlDealerCode.SelectedValue == "0")
                    {
                        var SOIs1 = (from S in MttrsWithText
                                     join D in PSession.User.Dealer on S.dealer_code equals D.UserName
                                     select new
                                     {
                                         S
                                     }).ToList();
                        MttrsWithText.Clear();
                        foreach (var w in SOIs1)
                        {
                            MttrsWithText.Add(w.S);
                        }
                    }

                    SDMS_MTTR = MttrsWithText;
                    gvICTicketsWithText.PageIndex = 0;
                    gvICTicketsWithText.DataSource = SDMS_MTTR;
                    gvICTicketsWithText.DataBind();
                    gv = gvICTicketsWithText;
                }

                if (SDMS_MTTR.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_MTTR.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            if (rbWithOutText.Checked)
            {
                gv = gvICTickets;
            }
            else
            {
                gv = gvICTicketsWithText;
            }

            if (gv.PageIndex > 0)
            {
                gv.PageIndex = gv.PageIndex - 1;
                gv.DataSource = SDMS_MTTR;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_MTTR.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;
            if (rbWithOutText.Checked)
            {
                gv = gvICTickets;
            }
            else
            {
                gv = gvICTicketsWithText;
            }

            if (gv.PageCount > gv.PageIndex)
            {
                gv.PageIndex = gv.PageIndex + 1;
                gv.DataSource = SDMS_MTTR;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_MTTR.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Tkt No.");
            dt.Columns.Add("Call Login Dt(IC)");
            dt.Columns.Add("Ser. Req. Dt");
            dt.Columns.Add("SE Reached Dt");
            dt.Columns.Add("SE Restore Dt");
            dt.Columns.Add("MTTR1-Act Resp(Days)");
            dt.Columns.Add("MTTR2-Actual Restored(Day)");
            dt.Columns.Add("MTTR1-Act Resp(Hour)");
            dt.Columns.Add("MTTR2-Actual Restored(Hour)");
            dt.Columns.Add("Status Since (Hour)");
            dt.Columns.Add("Status1 (Op. Based)");
            dt.Columns.Add("PSR Status");
            dt.Columns.Add("Cust. ID");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Present M/C City");
            dt.Columns.Add("M/C Loc Dist(IC)");
            dt.Columns.Add("M/C Loc State(IC)");
            dt.Columns.Add("M/C Loc Region(IC)");
            dt.Columns.Add("Problem Reported");
            dt.Columns.Add("Model Des");
            dt.Columns.Add("Serial No.");
            dt.Columns.Add("Ser Engg Name");
            dt.Columns.Add("Service charge Code");
            dt.Columns.Add("Service charge des");
            dt.Columns.Add("Description");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Prior Desc. (IC)");
            dt.Columns.Add("Prior Desc. (Srv. Order)");
            dt.Columns.Add("Application");
            dt.Columns.Add("Contatc no.");
            dt.Columns.Add("Contact person");
            dt.Columns.Add("FSR No");
            dt.Columns.Add("PSR No");
            dt.Columns.Add("TSIR No");



            if (rbWithOutText.Checked)
            {
                foreach (PDMS_MTTR M in SDMS_MTTR)
                {
                    dt.Rows.Add(
                          M.f_ic_ticket_id
                        , M.f_call_login_date == null ? "" : ((DateTime)M.f_call_login_date).ToShortDateString()
                        , M.ser_req_date == null ? "" : ((DateTime)M.ser_req_date).ToShortDateString()
                        , M.ser_rec_date == null ? "" : ((DateTime)M.ser_rec_date).ToShortDateString()
                        , M.ser_res_date == null ? "" : ((DateTime)M.ser_res_date).ToShortDateString()
                        , M.MTTR1
                        , M.MTTR2
                        , ""
                        , ""
                        , ""
                        , M.status1
                        , M.PsrStatus
                        , M.f_cust_id
                        , M.d_customer_name
                        , M.dealer_code
                        , M.dealer_name
                        , M.present_mc_city
                        , M.present_mc_dist
                        , M.present_mc_state
                        , M.present_mc_region
                        , M.problem_reported
                        , M.mc_desc
                        , M.mc_slno
                        , M.ser_name
                        , M.code
                        , M.f_part_id
                        , M.description1
                        , M.r_counter_end
                        , M.r_priority_class_desc
                        , M.r_priority_desc
                        , M.r_application
                        , M.contact_person_pre
                        , M.contact_person
                        , ""
                        , M.p_sr_id
                        , M.r_tsir_no
                        );
                }
            }
            else
            {
                dt.Columns.Add("Cust Sat Level");
                dt.Columns.Add("Total Mandays");
                dt.Columns.Add("Total Hours");
                dt.Columns.Add("Margin Remark");
                dt.Columns.Add("Scope Of Work");
                dt.Columns.Add("Hilly Region");
                dt.Columns.Add("Breakdown Reason");
                dt.Columns.Add("Breakdown Details");
                foreach (PDMS_MTTR M in SDMS_MTTR)
                {
                    foreach (PBreakdown B in M.Breakdown)
                    {

                        dt.Rows.Add(
                              M.f_ic_ticket_id
                            , M.f_call_login_date == null ? "" : ((DateTime)M.f_call_login_date).ToShortDateString()
                            , M.ser_req_date == null ? "" : ((DateTime)M.ser_req_date).ToShortDateString()
                            , M.ser_rec_date == null ? "" : ((DateTime)M.ser_rec_date).ToShortDateString()
                            , M.ser_res_date == null ? "" : ((DateTime)M.ser_res_date).ToShortDateString()
                            , M.MTTR1
                            , M.MTTR2
                            , ""
                            , ""
                            , ""
                            , M.status1
                            , M.PsrStatus
                            , M.f_cust_id
                            , M.d_customer_name
                            , M.dealer_code
                            , M.dealer_name
                            , M.present_mc_city
                            , M.present_mc_dist
                            , M.present_mc_state
                            , M.present_mc_region
                            , M.problem_reported
                            , M.mc_desc
                            , M.mc_slno
                            , M.ser_name
                            , M.code
                            , M.f_part_id
                            , M.description1
                            , M.r_counter_end
                            , M.r_priority_class_desc
                            , M.r_priority_desc
                            , M.r_application
                            , M.contact_person_pre
                            , M.contact_person
                            , ""
                            , M.p_sr_id
                            , M.r_tsir_no
                            , M.CustomerSatisfactionLevel
                            , M.TotalMandays
                            , ""
                            , ""
                            , ""
                            , ""
                            , B.BreakdownReason
                            , B.BreakdownDetails);


                        M.f_call_login_date = null;
                        M.ser_req_date = null;
                        M.ser_rec_date = null;
                        M.ser_res_date = null;
                        M.MTTR1 = null;
                        M.MTTR2 = null;
                        M.status1 = null;
                        M.PsrStatus = null;
                        M.f_cust_id = null;
                        M.d_customer_name = null;
                        M.dealer_code = null;
                        M.dealer_name = null;
                        M.present_mc_city = null;
                        M.present_mc_dist = null;
                        M.present_mc_state = null;
                        M.present_mc_region = null;
                        M.problem_reported = null;
                        M.mc_desc = null;
                        M.mc_slno = null;
                        M.ser_name = null;
                        M.code = null;
                        M.f_part_id = null;
                        M.description1 = null;
                        M.r_counter_end = null;
                        M.r_priority_class_desc = null;
                        M.r_priority_desc = null;
                        M.r_application = null;
                        M.contact_person_pre = null;
                        M.contact_person = null;

                        M.p_sr_id = null;
                        M.r_tsir_no = null;
                        M.CustomerSatisfactionLevel = null;
                        M.TotalMandays = null;
                    }
                }
            }
            new BXcel().ExporttoExcel(dt, "MTTR Report");
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = null;
            if (rbWithOutText.Checked)
            {
                gv = gvICTickets;
            }
            else
            {
                gv = gvICTicketsWithText;
            }
            gv.PageIndex = e.NewPageIndex;
            gv.DataSource = SDMS_MTTR;
            gv.DataBind();
            lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + SDMS_MTTR.Count;

        }

        protected void rbWithOutText_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWithOutText.Checked)
            {
                gvICTickets.Visible = true;
                gvICTicketsWithText.Visible = false;
            }
            else
            {
                gvICTickets.Visible = false;
                gvICTicketsWithText.Visible = true;
            }
            SDMS_MTTR = new List<PDMS_MTTR>();
            gvICTickets.DataSource = SDMS_MTTR;
            gvICTickets.DataBind();
            gvICTicketsWithText.DataSource = SDMS_MTTR;
            gvICTicketsWithText.DataBind();

            lblRowCount.Visible = false;
            ibtnArrowLeft.Visible = false;
            ibtnArrowRight.Visible = false;

        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void gvICTicketsWithText_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string f_ic_ticket_id = Convert.ToString(gvICTicketsWithText.DataKeys[e.Row.RowIndex].Value);
                    GridView gvBreakdown = (GridView)e.Row.FindControl("gvBreakdown");



                    List<PBreakdown> Breakdown = new List<PBreakdown>();
                    Breakdown = SDMS_MTTR.Find(s => s.f_ic_ticket_id == f_ic_ticket_id).Breakdown;

                    gvBreakdown.DataSource = Breakdown;
                    gvBreakdown.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", ex);
                throw ex;
            }
        }
    }
}