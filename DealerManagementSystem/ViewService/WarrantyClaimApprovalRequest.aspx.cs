using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimApprovalRequest : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyClaimApprovalRequest; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimApprovalRequest.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyInvoiceHeader> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimApprovalRequest"] == null)
                {
                    Session["DMS_WarrantyClaimApprovalRequest"] = new List<PDMS_WarrantyInvoiceHeader>();
                }
                return (List<PDMS_WarrantyInvoiceHeader>)Session["DMS_WarrantyClaimApprovalRequest"];
            }
            set
            {
                Session["DMS_WarrantyClaimApprovalRequest"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Claim Approve Request');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
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
                txtClaimDate.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillClaimApproval();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillClaimApproval()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string Filter = " where inv.p_inv_id in ( select InvoiceNumber  from  ( select   invg.p_inv_id as  InvoiceNumber , sum(invig.r_gross_amt) Sum_Value FROM   dsinr_inv_item invig  inner JOIN dsinr_inv_hdr invg ON ( invig.k_id = invg.p_id AND invig.s_tenant_id = invg.s_tenant_id and  d_inv_type_desc = 'Warranty Invoice'  )  where 1 = 1  ";

                if (ddlDealerCode.SelectedValue != "0")
                {
                    Filter = Filter + " and ten.tenantid = " + ddlDealerCode.SelectedValue;
                }


                if (!string.IsNullOrEmpty(txtClaimID.Text.Trim()))
                {
                    Filter = Filter + " and inv.p_inv_id = '" + txtClaimID.Text.Trim() + "'";
                }
                else if (!string.IsNullOrEmpty(txtClaimDate.Text.Trim()))
                {
                    Filter = Filter + " and inv.r_inv_date  = '" + txtClaimDate.Text.Trim().Split('/')[1] + "/" + txtClaimDate.Text.Trim().Split('/')[0] + "/" + txtClaimDate.Text.Trim().Split('/')[2] + "'";
                }
                else
                {
                    lblMessage.Text = "Please enter the claim number od date ";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    return;
                }
                Filter = Filter + "  group by invg.p_inv_id  ,invg.s_tenant_id )	t where Sum_Value >= 50000) ";


                //if (!string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()))
                //{
                //    Filter = Filter + " and psc.f_ic_ticket_date >= '" + txtICLoginDateFrom.Text.Trim().Split('/')[1] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[0] + "/" + txtICLoginDateFrom.Text.Trim().Split('/')[2] + "'";
                //}
                //if (!string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()))
                //{
                //    Filter = Filter + " and psc.f_ic_ticket_date <= '" + txtICLoginDateTo.Text.Trim().Split('/')[1] + "/" + txtICLoginDateTo.Text.Trim().Split('/')[0] + "/" + txtICLoginDateTo.Text.Trim().Split('/')[2] + "'";
                //}

                //if (!string.IsNullOrEmpty(txtClaimDateF.Text.Trim()))
                //{
                //    Filter = Filter + " and inv.r_inv_date >= '" + txtClaimDateF.Text.Trim().Split('/')[1] + "/" + txtClaimDateF.Text.Trim().Split('/')[0] + "/" + txtClaimDateF.Text.Trim().Split('/')[2] + "'";
                //}
                //if (!string.IsNullOrEmpty(txtClaimDateT.Text.Trim()))
                //{
                //    Filter = Filter + " and inv.r_inv_date <= '" + txtClaimDateT.Text.Trim().Split('/')[1] + "/" + txtClaimDateT.Text.Trim().Split('/')[0] + "/" + txtClaimDateT.Text.Trim().Split('/')[2] + "'";
                //}


                List<PDMS_WarrantyInvoiceHeader> SOIs = new BDMS_WarrantyClaim().GetWarrantyInvoiceFromPostGres(Filter);

                lblMessage.Text = SOIs.Count == 0 ? "No claim requested for approval" : (SOIs.Count + " Claim requested for approval");
                lblMessage.ForeColor = (SOIs.Count == 0)?Color.Red: Color.Green;
                lblMessage.Visible = true;

                SDMS_WarrantyClaimHeader = SOIs;

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
                if (SOIs.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ClaimApprovalList1", "fillClaimApproval", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = SDMS_WarrantyClaimHeader;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvICTickets.DataKeys[e.Row.RowIndex].Value);
                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");

                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.InvoiceNumber == supplierPOID).InvoiceItems;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimApprovalRequest", "gvICTickets_RowDataBound", ex);
                throw ex;
            }
        }


        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
        }
    }
}