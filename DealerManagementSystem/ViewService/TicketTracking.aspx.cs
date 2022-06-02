using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class TicketTracking : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_TicketTracking.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » Tracking');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPSC();
                FillICTicket();
                FillClaim();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillPSC()
        {
            try
            {
                gvPSR.DataBind();
                gvPSC.DataBind();
                gvInv.DataBind();

                TraceLogger.Log(DateTime.Now);
                string Fillter = "";

                if (!string.IsNullOrEmpty(txtICTicket.Text.Trim()))
                    Fillter = " and m.p_material = '" + txtICTicket.Text.Trim() + "'";
                else
                    return;
                string PsrQuery = "select p_sr_id as SR_ID,s_tenant_id as Dealer,f_technician_id as Technician_ID,s_status as Status,f_cust_id as Customer from  dsprr_psr_hdr where f_ic_ticket_id = " + txtICTicket.Text.Trim();

                DataTable dtPSR = new BDMS_Master().ExecuteReader(PsrQuery, true);
                if (dtPSR.Rows.Count != 0)
                {
                    gvPSR.DataSource = dtPSR;
                    gvPSR.DataBind();
                    string PscQuery = "select p_sc_id as SC_ID,f_sr_id as SR_ID,f_ic_ticket_id as IC_Ticket_ID ,f_ic_ticket_date as I_Ticket_Date,s_tenant_id as Dealer from dsprr_psc_hdr where f_ic_ticket_id = " + txtICTicket.Text.Trim();

                    DataTable dtPSC = new BDMS_Master().ExecuteReader(PscQuery, true);
                    if (dtPSC.Rows.Count != 0)
                    {
                        gvPSC.DataSource = dtPSC;
                        gvPSC.DataBind();

                        string InvQuery = "select  r_ext_id as SC_ID, inv.s_status as Status ,p_inv_id as Invoice ,r_inv_date as Invoice_date,invi.p_inv_item as ITEM,f_del_Id as Delivery_ID  ,f_material_id  as material "
   + " ,(case when   d_material_desc is null then  m.r_description else   d_material_desc end)  material_desc  "
   + " , f_uom as uom,r_qty as qty ,r_hsn_id as HSN_Code,inv.f_customer_id as Customer,cust_bp.r_org_name  as Customer_Name "
   + " ,invi.r_gross_amt  Base_Value,case when  invi.r_gross_amt = invi.r_net_amt then invi.r_net_amt * 1.18 else invi.r_net_amt end   as Total	,invi.p_inv_item "
   + " from  dsinr_inv_hdr inv  inner join dsinr_inv_item invi ON ( invi.k_id = inv.p_id AND invi.s_tenant_id = inv.s_tenant_id )  "
   + " INNER JOIN doohr_bp AS cust_bp ON ( cust_bp.s_tenant_id = invi.s_tenant_id AND cust_bp.p_bp_id = inv.f_customer_id  AND cust_bp.p_bp_type = 'CU' )  "
   + " left join af_m_materials m on m.p_material = invi.f_material_id   where r_ext_id = '" + dtPSC.Rows[0]["SC_ID"].ToString() + "'";

                        DataTable dtInv = new BDMS_Master().ExecuteReader(InvQuery, true);
                        if (dtInv.Rows.Count != 0)
                        {
                            gvInv.DataSource = dtInv;
                            gvInv.DataBind();
                        }
                    }
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_TicketTracking", "fillICTicket", e1);
                throw e1;
            }
        }

        void FillClaim()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);

                List<PDMS_WarrantyInvoiceHeader> InvoiceHeaders = new BDMS_WarrantyClaim().GetWarrantyClaimReport(txtICTicket.Text.Trim(), null, null, "", null, null, "", null, null, null, "", "", "", false, PSession.User.UserID);
                gvClaimByClaimID.DataSource = InvoiceHeaders;
                gvClaimByClaimID.DataBind();

                for (int i = 0; i < gvClaimByClaimID.Rows.Count; i++)
                {
                    string supplierPOID = Convert.ToString(gvClaimByClaimID.DataKeys[i].Value);
                    GridView gvICTicketItems = (GridView)gvClaimByClaimID.Rows[i].FindControl("gvICTicketItems");
                    List<PDMS_WarrantyInvoiceItem> Item = new List<PDMS_WarrantyInvoiceItem>();
                    Item = InvoiceHeaders.Find(s => s.InvoiceNumber == supplierPOID).InvoiceItems;
                    gvICTicketItems.DataSource = Item;
                    gvICTicketItems.DataBind();
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }

        void FillICTicket()
        {
            gvICTickets.PageIndex = 0;
            gvICTickets.DataSource = new BDMS_ICTicket().GetICTicket(null, "", txtICTicket.Text.Trim(), null, null, null, null);
            gvICTickets.DataBind();
        }
    }
}