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
    public partial class WarrantyFailureMaterialDCCreation : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyFailureMaterialDCCreation; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_FailedMaterialReturn.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        public long[,] GridSelected
        {
            get
            {
                if (Session["GridSelected"] == null)
                {
                    Session["GridSelected"] = new long[1, 1];
                }
                return (long[,])Session["GridSelected"];
            }
            set
            {
                Session["GridSelected"] = value;
            }
        }

        public List<PDMS_WarrantyFailureMaterial> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaim"] == null)
                {
                    Session["DMS_WarrantyClaim"] = new List<PDMS_WarrantyFailureMaterial>();
                }
                return (List<PDMS_WarrantyFailureMaterial>)Session["DMS_WarrantyClaim"];
            }
            set
            {
                Session["DMS_WarrantyClaim"] = value;
            }
        }
        PDMS_WarrantyFailureMaterial Ws = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Failed Material » Warranty DC Creation');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                //new BDMS_WarrantyClaim().insertWarrantyClaim();
                
                    fillDealer(); 
                FillTemplate();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;


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

                if (ddlDealerCode.SelectedValue != "0")
                {
                    DealerCode = ddlDealerCode.SelectedValue;
                    ViewState["DealerCodeID"] = DealerCode;
                }
                else
                {
                    lblMessage.Text = "Please select dealer";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                long? DCTemplateID = null;
                if (ddlDCTemplate.SelectedValue != "0")
                {
                    DCTemplateID = Convert.ToInt64(ddlDCTemplate.SelectedValue);
                }


                //  CategoryID = ddlCategory.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory.SelectedValue);
                List<PDMS_WarrantyFailureMaterial> SOIs = new BDMS_WarrantyFailureMaterial().GetFailedMaterialDCTemplateToCreateDC(DCTemplateID, Convert.ToInt32(ddlDealerCode.SelectedValue));


                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.FailureMaterialItem.Invoice.DealerCode equals D.UserName
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
                gvDCTemplate.PageIndex = 0;
                gvDCTemplate.DataSource = SOIs;

                gvDCTemplate.DataBind();
                GridSelected = new long[gvDCTemplate.PageSize, gvDCTemplate.PageCount];

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
                    lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
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
            if (gvDCTemplate.PageIndex > 0)
            {
                gvDCTemplate.PageIndex = gvDCTemplate.PageIndex - 1;
                gvDCTemplate.DataSource = SDMS_WarrantyClaimHeader;
                gvDCTemplate.DataBind();
                lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDCTemplate.PageCount > gvDCTemplate.PageIndex)
            {
                gvDCTemplate.PageIndex = gvDCTemplate.PageIndex + 1;
                gvDCTemplate.DataSource = SDMS_WarrantyClaimHeader;
                gvDCTemplate.DataBind();
                lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
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

            //foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            //{
            //    foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
            //    {
            //        dt.Rows.Add(
            //            // M.ClaimID
            //            // , M.ClaimDate == null ? "" : ((DateTime)M.ClaimDate).ToShortDateString()
            //              M.ICTicketID
            //            , M.ICTicketDate == null ? "" : ((DateTime)M.ICTicketDate).ToShortDateString()
            //            , M.CustomerCode
            //            , M.CustomerName
            //            , M.DealerCode
            //            , M.DealerName
            //            , M.HMR
            //            , M.MarginWarranty
            //            , M.MachineSerialNumber
            //            //  , M.Status
            //            , M.Approved1By.ContactName
            //            , M.Approved1On
            //           , M.Approved2By.ContactName
            //            , M.Approved2On
            //            , M.InvoiceNumber
            //            , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
            //            , M.TSIRNumber
            //            , M.Model
            //            , Item.HSNCode
            //            , "'" + Item.Material
            //            , Item.MaterialDesc
            //            , Item.Category
            //            , Item.Qty
            //            , Item.UnitOM
            //            , Item.Amount
            //            , Item.BaseTax
            //             , Item.MaterialStatusRemarks1
            //            , Item.Approved1Amount
            //            , Item.Approved1Remarks
            //            , Item.MaterialStatusRemarks2
            //            , Item.Approved2Amount
            //            , Item.Approved2Remarks
            //            );
            //    }
            //}
            new BXcel().ExporttoExcel(dt, "Warranty Claim");
        }

        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{

                //    if (ddlDCTemplate.SelectedValue != "0")
                //    {
                //        Label lblWarrantyInvoiceItemID = (Label)e.Row.FindControl("lblWarrantyInvoiceItemID");
                //        var MaterialItem = Ws.FailureMaterialItems.Find(s => s.InvoiceItem.WarrantyInvoiceItemID == Convert.ToInt64(lblWarrantyInvoiceItemID.Text));
                //        if (MaterialItem != null)
                //        {
                //            if (Ws.FailureMaterialItems.Find(s => s.InvoiceItem.WarrantyInvoiceItemID == Convert.ToInt64(lblWarrantyInvoiceItemID.Text)).InvoiceItem.WarrantyInvoiceItemID != null)
                //            {
                //                GridSelected[e.Row.RowIndex, gvICTickets.PageIndex] = Convert.ToInt64(lblWarrantyInvoiceItemID.Text);
                //            }
                //        }
                //    }

                //    CheckBox cbSelectedMaterial = (CheckBox)e.Row.FindControl("cbSelectedMaterial");
                //    if (GridSelected[e.Row.RowIndex, gvICTickets.PageIndex] == 0)
                //    {
                //        cbSelectedMaterial.Checked = false;
                //    }
                //    else
                //    {
                //        cbSelectedMaterial.Checked = true;
                //    }
                //}

                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
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
            //foreach (PDMS_WarrantyInvoiceHeader M in SDMS_WarrantyClaimHeader)
            //{
            //    foreach (PDMS_WarrantyInvoiceItem Item in M.InvoiceItems)
            //    {
            //        dt.Rows.Add(
            //              M.OrderType, M.SalesOrg, M.DisChannel, M.Division
            //            , M.DealerCode, M.DealerCode
            //            , M.InvoiceDate == null ? "" : ((DateTime)M.InvoiceDate).ToShortDateString()
            //            , M.PaymentTerm, M.IncoTerms, M.OrderReason
            //            , "'" + Item.Material + ".FL", Item.Qty
            //            , "T001", M.CustomerCode
            //            , M.Model
            //            , M.MachineSerialNumber
            //            , M.FSRNumber
            //            , ""
            //            , M.Approved1By == null ? "" : M.Approved1By.ContactName
            //            , M.Approved2By == null ? "" : M.Approved2By.ContactName
            //             , M.Location
            //            , M.HMR
            //            , M.WarrantyEndDate == null ? "" : ((DateTime)M.WarrantyEndDate).ToShortDateString()
            //            , M.DateOfCommissioning == null ? "" : ((DateTime)M.DateOfCommissioning).ToShortDateString()
            //            , ""
            //            , M.ReasonForFailure
            //            , ""// M.ClaimDate == null ? "" : ((DateTime)M.ClaimDate).ToShortDateString()
            //            , ""
            //            , M.Application
            //            , M.TSIRNumber
            //           );
            //    }
            //}
            new BXcel().ExporttoExcel(dt, "Claim For SAP Upload");
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void gvDCTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    long DCTemplateID = Convert.ToInt64(gvDCTemplate.DataKeys[e.Row.RowIndex].Value);

                    GridView gvDCItem = (GridView)e.Row.FindControl("gvDCItem");

                    List<PDMS_WarrantyFailureMaterialItem> MaterialItem = new List<PDMS_WarrantyFailureMaterialItem>();
                    MaterialItem = SDMS_WarrantyClaimHeader.Find(s => s.DCTemplateID == DCTemplateID).FailureMaterialItems;
                    gvDCItem.DataSource = MaterialItem;
                    gvDCItem.DataBind();
                }

                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvDCTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDCTemplate.PageIndex = e.NewPageIndex;
            gvDCTemplate.DataSource = SDMS_WarrantyClaimHeader;
            gvDCTemplate.DataBind();

            lblRowCount.Text = (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + 1) + " - " + (((gvDCTemplate.PageIndex) * gvDCTemplate.PageSize) + gvDCTemplate.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void FillTemplate()
        {
            int? DealerID = null;
            if (ddlDealerCode.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealerCode.SelectedValue);
            }
            ddlDCTemplate.DataTextField = "DCTemplateName";
            ddlDCTemplate.DataValueField = "DCTemplateID";
            ddlDCTemplate.DataSource = new BDMS_WarrantyFailureMaterial().GetDCTemplateNameActive(DealerID);
            ddlDCTemplate.DataBind();

            ddlDCTemplate.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void btnCreateDC_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;

            //if (string.IsNullOrEmpty(txtDeliveryTo.Text.Trim()))
            //{
            //    lblMessage.Text = "Please enter the Delivery To";
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtTransporterName.Text.Trim()))
            //{
            //    lblMessage.Text = "Please enter the Transporter Name";
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtDocketDetails.Text.Trim()))
            //{
            //    lblMessage.Text = "Please enter the Docket Details";
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            List<long> WarrantyInvoiceItemIDs = new List<long>();
            for (int i = 0; i < gvDCTemplate.Rows.Count; i++)
            {
                GridView gvDCItem = (GridView)gvDCTemplate.Rows[i].FindControl("gvDCItem");
                for (int j = 0; j < gvDCItem.Rows.Count; j++)
                {
                    Label lblWarrantyInvoiceItemID = (Label)gvDCItem.Rows[j].FindControl("lblWarrantyInvoiceItemID");
                    CheckBox cbSelectedMaterial = (CheckBox)gvDCItem.Rows[j].FindControl("cbSelectedMaterial");
                    if (cbSelectedMaterial.Checked)
                    {
                        WarrantyInvoiceItemIDs.Add(Convert.ToInt64(lblWarrantyInvoiceItemID.Text));
                    }
                }
            }
            if (WarrantyInvoiceItemIDs.Count == 0)
            {
                lblMessage.Text = "Please select the material";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            long st = new BDMS_WarrantyFailureMaterial().InsertWarrantyFailureMaterialDC(0, lblDeliveryTo.Text.Trim(), txtTransporterName.Text.Trim(), txtDocketDetails.Text.Trim(), txtPackingDetails.Text.Trim(), WarrantyInvoiceItemIDs, PSession.User.UserID, Convert.ToInt64(ViewState["DealerCodeID"]));
            if (st != 0)
            {
                List<PDMS_WarrantyFailureMaterial> SOIs = new BDMS_WarrantyFailureMaterial().GetWarrantyFailedMaterialDeliveryChallan(st, "", null, null, "", null, null, null);
                lblMessage.Text = "Delivery Challan Number " + SOIs[0].DeliveryChallanNumber;
                lblMessage.ForeColor = Color.Green;
                FillTemplate();
                fillClaim();

            }
            else
            {
                lblMessage.Text = "Delivery Challan Number is not created";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            FillTemplate();
        }
    }
}