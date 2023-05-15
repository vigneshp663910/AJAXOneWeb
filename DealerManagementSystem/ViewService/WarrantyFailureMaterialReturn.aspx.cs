using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyFailureMaterialReturn : BasePage
    {
       // public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyFailureMaterialReturn; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_FailedMaterialReturn.aspx";
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
                if (Session["DMS_WarrantyClaim"] == null)
                {
                    Session["DMS_WarrantyClaim"] = new List<PDMS_WarrantyInvoiceHeader>();
                }
                return (List<PDMS_WarrantyInvoiceHeader>)Session["DMS_WarrantyClaim"];
            }
            set
            {
                Session["DMS_WarrantyClaim"] = value;
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
                //new BDMS_WarrantyClaim().insertWarrantyClaim();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
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
                FillStatus();
                //FillCategory();

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillClaim();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillClaim()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string DealerCode = "";
                DateTime? ICTicketDateF = null;
                DateTime? ICTicketDateT = null;

                DateTime? ClaimDateF = null;
                DateTime? ClaimDateT = null;
                int? StatusID = null;
                //   int? CategoryID = null;


                if (ddlDealerCode.SelectedValue != "0")
                {
                    DealerCode = ddlDealerCode.SelectedValue;
                }
                if (!string.IsNullOrEmpty(txtICLoginDateFrom.Text))
                {
                    ICTicketDateF = Convert.ToDateTime(txtICLoginDateFrom.Text);
                }
                if (!string.IsNullOrEmpty(txtICLoginDateTo.Text))
                {
                    ICTicketDateT = Convert.ToDateTime(txtICLoginDateTo.Text);
                }

                if (!string.IsNullOrEmpty(txtClaimDateF.Text))
                {
                    ClaimDateF = Convert.ToDateTime(txtClaimDateF.Text);
                }
                if (!string.IsNullOrEmpty(txtClaimDateT.Text))
                {
                    ClaimDateT = Convert.ToDateTime(txtClaimDateT.Text);
                }

                StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                //  CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
                List<PDMS_WarrantyInvoiceHeader> SOIs = new BDMS_WarrantyFailureMaterial().GetFailedMaterialReturn(txtICServiceTicket.Text.Trim(), ICTicketDateF, ICTicketDateT, txtClaimID.Text.Trim(), ClaimDateF, ClaimDateT, DealerCode, StatusID, "", null, null, PSession.User.UserID);


                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }
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
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", e1);
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

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order Type");
            dt.Columns.Add("Sales Org");
            dt.Columns.Add("Dis Channel");
            dt.Columns.Add("Division");
            dt.Columns.Add("Sold To Party");
            dt.Columns.Add("Ship To Party");
            dt.Columns.Add("Pricing Date");
            dt.Columns.Add("Payment Term");
            dt.Columns.Add("Inco Terms");
            dt.Columns.Add("Order Reason");
            dt.Columns.Add("Material");
            dt.Columns.Add("Status");
            dt.Columns.Add("Apr.1 By");
            dt.Columns.Add("Apr.1 On");
            dt.Columns.Add("Apr.2 By");
            dt.Columns.Add("Apr.2 On");

            dt.Columns.Add("Invoice Number");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("TSIR Number");
            dt.Columns.Add("Model");
            dt.Columns.Add("SAC / HSN Code");
            dt.Columns.Add("Material");
            dt.Columns.Add("Material Desc");
            dt.Columns.Add("Category");
            dt.Columns.Add("Qty");
            dt.Columns.Add("UOM");
            dt.Columns.Add("Amount");
            dt.Columns.Add("BaseTax");
            // dt.Columns.Add("Material Status");
            dt.Columns.Add("Failure Mat Remarks 1");
            dt.Columns.Add("Apr.1 Amt");
            dt.Columns.Add("Apr.1 Remarks");
            dt.Columns.Add("Failure Mat Remarks 2");
            dt.Columns.Add("Apr.2 Amt");
            dt.Columns.Add("Apr.2 Remarks");

            foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                {
                    dt.Rows.Add(
                          // M.ClaimID
                          // , M.ClaimDate == null ? "" : ((DateTime)M.ClaimDate).ToShortDateString()
                          M.ICTicketID
                        , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
                        , M.CustomerCode
                        , M.CustomerName
                        , M.DealerCode
                        , M.DealerName
                        , M.HMR
                        , M.MarginWarranty
                        , M.MachineSerialNumber
                        //  , M.Status
                        , M.Approved1By.ContactName
                        , M.Approved1On
                       , M.Approved2By.ContactName
                        , M.Approved2On
                        , M.InvoiceNumber
                        , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
                        , M.TSIRNumber
                        , M.Model
                        , Item.HSNCode
                        , "'" + Item.Material
                        , Item.MaterialDesc
                        , Item.Category
                        , Item.Qty
                        , Item.UnitOM
                        , Item.Amount
                        , Item.BaseTax
                         , Item.MaterialStatusRemarks1
                        , Item.Approved1Amount
                        , Item.Approved1Remarks
                        , Item.MaterialStatusRemarks2
                        , Item.Approved2Amount
                        , Item.Approved2Remarks
                        );
                }
            }
            new BXcel().ExporttoExcel(dt, "Warranty Claim");
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

                    //Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    //GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    //gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                    //gvFileAttached.DataBind();


                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyInvoiceHeaderID == Convert.ToInt64(supplierPOID)).InvoiceItems;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();


                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        void FillStatus()
        {
            List<PDMS_WarrantyStatus> Status = new BDMS_WarrantyClaim().GetWarrantyClaimStatus();
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataSource = Status;
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));

        }
        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order Type");
            dt.Columns.Add("Sales Org");
            dt.Columns.Add("Dis Channel");
            dt.Columns.Add("Division");
            dt.Columns.Add("Sold To Party");
            dt.Columns.Add("Ship To Party");
            dt.Columns.Add("Pricing Date");
            dt.Columns.Add("Payment Term");
            dt.Columns.Add("Inco Terms");
            dt.Columns.Add("Order Reason");
            dt.Columns.Add("Material");
            dt.Columns.Add("Order Qty");
            dt.Columns.Add("Plant");
            dt.Columns.Add("Partner");
            dt.Columns.Add("Text- Eqip.Model");
            dt.Columns.Add("Text-Equip Sr No");
            dt.Columns.Add("FSR No");
            dt.Columns.Add("TR Date");
            dt.Columns.Add("Approved1 By");
            dt.Columns.Add("Approved2 By");
            dt.Columns.Add("Site Location");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Warranty Expiry");
            dt.Columns.Add("Date Of Comm");
            dt.Columns.Add("Kind Attention");
            dt.Columns.Add("Reason for Failure");
            dt.Columns.Add("Warr Claim Date");
            dt.Columns.Add("Number of Days");
            dt.Columns.Add("Application");
            dt.Columns.Add("TSIR");
            foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
                {
                    dt.Rows.Add(
                          M.OrderType, M.SalesOrg, M.DisChannel, M.Division
                        , M.DealerCode, M.DealerCode
                        , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
                        , M.PaymentTerm, M.IncoTerms, M.OrderReason
                        , "'" + Item.Material + ".FL", Item.Qty
                        , "T001", M.CustomerCode
                        , M.Model
                        , M.MachineSerialNumber
                        , M.FSRNumber
                        , ""
                        , M.Approved1By == null ? "" : M.Approved1By.ContactName
                        , M.Approved2By == null ? "" : M.Approved2By.ContactName
                         , M.Location
                        , M.HMR
                        , M.WarrantyEndDate == null ? "" : ((DateTime)M.WarrantyEndDate).ToShortDateString()
                        , M.DateOfCommissioning == null ? "" : ((DateTime)M.DateOfCommissioning).ToShortDateString()
                        , ""
                        , M.ReasonForFailure
                        , ""// M.ClaimDate == null ? "" : ((DateTime)M.ClaimDate).ToShortDateString()
                        , ""
                        , M.Application
                        , M.TSIRNumber
                       );
                }
            }
            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
    }
}