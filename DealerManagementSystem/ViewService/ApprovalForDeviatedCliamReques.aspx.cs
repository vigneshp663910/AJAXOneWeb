using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ApprovalForDeviatedCliamReques : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ApprovalForDeviatedCliamReques; } }
        public List<PClaimDeviation> ICTicketDT
        {
            get
            {
                if (Session["DMS_DeviatedCliamRequestForApproval"] == null)
                {
                    Session["DMS_DeviatedCliamRequestForApproval"] = new List<PClaimDeviation>();
                }
                return (List<PClaimDeviation>)Session["DMS_DeviatedCliamRequestForApproval"];
            }
            set
            {
                Session["DMS_DeviatedCliamRequestForApproval"] = value;
            }
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
                txtRequestedDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtRequestedDateTo.Text = DateTime.Now.ToShortDateString();
                fillDealer();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            if (ddlDealerCode.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (ddlDeviationType.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Deviation Type";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtClaimNumber.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Claim Number";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            string DealerCode = ddlDealerCode.SelectedValue;

          //  List<PDMS_WarrantyInvoiceHeader> Claim = new BDMS_WarrantyClaim().GetWarrantyClaimReport("", null, null, txtClaimNumber.Text.Trim(), null, null, DealerCode, null, null, null, "", "", "", false, PSession.User.UserID);


            //var SOIs1 = (from S in ICTicket
            //             join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
            //             select new
            //             {
            //                 S
            //             }).ToList();
            //ICTicket.Clear();
            //foreach (var w in SOIs1)
            //{
            //    ICTicket.Add(w.S);
            //}


            //if (Claim.Count == 1)
            //{
                PApiResult Results = new BDMS_WarrantyClaim().InsertDeviatedClaimRequestForApproval(txtClaimNumber.Text.Trim(), Convert.ToInt32(ddlDeviationType.SelectedValue));

                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = "Claim sent for approval";
                lblMessage.ForeColor = Color.Green;
                txtClaimNumber.Text = "";

                //if (new BDMS_WarrantyClaim().InsertDeviatedClaimRequestForApproval(Claim[0].WarrantyInvoiceHeaderID, PSession.User.UserID))
                //{

                //    lblMessage.Text = "Claim sent for approval";
                //    lblMessage.ForeColor = Color.Green;
                //    txtClaimNumber.Text = "";
                //}
                //else
                //{
                //    lblMessage.Text = "Claim is not sent for approval";
                //    lblMessage.ForeColor = Color.Red;
                //}
            //}
            //else
            //{
            //    lblMessage.Text = "Please enter the correct Claim Number";
            //    lblMessage.ForeColor = Color.Red;
            //}

        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));

            ddlDealerF.DataTextField = "CodeWithName";
            ddlDealerF.DataValueField = "DID";
            ddlDealerF.DataSource = PSession.User.Dealer;
            ddlDealerF.DataBind();
            ddlDealerF.Items.Insert(0, new ListItem("Select", "0"));

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlDeviationTypeF.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Deviated Type";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            int? DealerID = ddlDealerF.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerF.SelectedValue);
            DateTime? DateF = string.IsNullOrEmpty(txtRequestedDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateFrom.Text.Trim());
            DateTime? DateT = string.IsNullOrEmpty(txtRequestedDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateTo.Text.Trim());

            ICTicketDT = new BDMS_WarrantyClaim().GetDeviatedClaimReport(DealerID, txtClaimNumberF.Text.Trim(), txtRequestedDateFrom.Text.Trim(), txtRequestedDateTo.Text.Trim(), Convert.ToInt32(ddlDeviationTypeF.SelectedValue));

            gvICTickets.DataSource = ICTicketDT;
            gvICTickets.DataBind();
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicketDT;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicketDT.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicketDT;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicketDT.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicketDT;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicketDT.Count;
        }

        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    long WarrantyInvoiceHeaderID = Convert.ToInt64(gvICTickets.DataKeys[e.Row.RowIndex].Value);
                    GridView gvICTicketItems = (GridView)e.Row.FindControl("gvICTicketItems");
                    List<PDMS_WarrantyInvoiceItem> ClaimItem = new List<PDMS_WarrantyInvoiceItem>();
                    ClaimItem = ICTicketDT.Find(s => s.WarrantyInvoiceHeaderID == WarrantyInvoiceHeaderID).InvoiceItems;

                    gvICTicketItems.DataSource = ClaimItem;
                    gvICTicketItems.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_DeviatedCliamApprove", "gvICTickets_RowDataBound", ex);
                throw ex;
            }
        }


        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Margin Warranty");
            dt.Columns.Add("Machine Serial Number");

            dt.Columns.Add("Claim Number");
            dt.Columns.Add("Claim Date");
          //  dt.Columns.Add("TSIR Number");
            dt.Columns.Add("Model");
            dt.Columns.Add("SAC / HSN Code");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Category");
            dt.Columns.Add("Qty");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Amount");
            dt.Columns.Add("BaseTax");

            dt.Columns.Add("Approve");
            dt.Columns.Add("Reject");
            foreach (PClaimDeviation M in ICTicketDT)
            {
                foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                {
                    dt.Rows.Add(
                          M.ICTicketID
                        , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
                        , M.CustomerCode
                        , M.CustomerName
                        , M.DealerCode
                        , M.DealerName
                        , M.HMR
                        , M.MarginWarranty
                        , M.MachineSerialNumber
                        , M.InvoiceNumber
                        , ((DateTime)M.InvoiceDate).ToShortDateString()
                      //  , M.TSIRNumber
                        , M.Model
                        , Item.HSNCode
                        , "'" + Item.Material
                        , Item.MaterialDesc
                        , Item.Category
                        , Item.Qty
                        , Item.UnitOM
                        , Item.Amount
                        , Item.BaseTax
                        , M.DeviatedIsApproved
                        , M.DeviatedIsRejected
                        );
                }
            }
            new BXcel().ExporttoExcel(dt, "Deviated Claim Request");
        }
    }
}