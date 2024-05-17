using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SaleOrderReturnView : System.Web.UI.UserControl
    {
        public int StatusID
        {
            get
            {
                if (ViewState["StatusID"] == null)
                {
                    ViewState["StatusID"] = 0;
                }
                return (int)ViewState["StatusID"];
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        public long SaleOrderReturnID
        {
            get
            {
                if (ViewState["SaleOrderReturnID"] == null)
                {
                    ViewState["SaleOrderReturnID"] = 0;
                }
                return (long)ViewState["SaleOrderReturnID"];
            }
            set
            {
                ViewState["SaleOrderReturnID"] = value;
            }
        }
        public PSaleOrderReturn SaleOrderReturnByID
        {
            get
            {
                if (ViewState["SaleOrderReturnByID"] == null)
                {
                    ViewState["SaleOrderReturnByID"] = new PSaleOrderReturn();
                }
                return (PSaleOrderReturn)ViewState["StockTransferOrderView"];
            }
            set
            {
                ViewState["StockTransferOrderView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageSoReturn.Text = "";
            if (Session["SaleOrderReturnID"] != null)
            {
                lblMessageSoReturn.Text = "Sale Order Return Created.";
                lblMessageSoReturn.ForeColor = Color.Green;
                Session["SaleOrderReturnID"] = null;
            }
        }
        public void fillViewSoReturn(long SaleOrderReturnID_)
        {
            SaleOrderReturnByID = new BSalesOrderReturn().GetSaleOrderReturnByID(SaleOrderReturnID_);
            SaleOrderReturnID = SaleOrderReturnID_;
            StatusID = SaleOrderReturnByID.ReturnStatus.StatusID;
            lblSaleOrderReturnNumber.Text = SaleOrderReturnByID.SaleOrderReturnNumber;
            lblSaleOrderReturnDate.Text = SaleOrderReturnByID.SaleOrderReturnDate.ToShortDateString();
            lblInvoiceNumber.Text = SaleOrderReturnByID.SaleOrderDelivery.InvoiceNumber;
            lblInvoiceDate.Text = SaleOrderReturnByID.SaleOrderDelivery.InvoiceDate == null ? "" : ((DateTime)SaleOrderReturnByID.SaleOrderDelivery.InvoiceDate).ToShortDateString();

            lblSODealer.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Dealer.DealerCode + " " + SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Dealer.DealerOffice.OfficeName;
            lblDivision.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Division.DivisionCode;
            lblSaleOrderType.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.SaleOrderType.SaleOrderType;

            lblSaleOrderReturnStatus.Text = SaleOrderReturnByID.ReturnStatus.Status;
            lblCustomer.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Customer.CustomerCode + " " + SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Customer.CustomerName;
            lblContactPerson.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Customer.ContactPerson;
            lblContactPersonNumber.Text = SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Customer.Mobile;

            lblCreatedBy.Text = SaleOrderReturnByID.CreatedBy.ContactName;
            lblCreditNoteNumber.Text = SaleOrderReturnByID.CreditNoteNumber;
            lblCreditNoteDate.Text = SaleOrderReturnByID.CreditNoteDate == null ? "" : ((DateTime)SaleOrderReturnByID.CreditNoteDate).ToShortDateString();

            lblRemarks.Text = SaleOrderReturnByID.Remarks;
            lblApprovedBy.Text = SaleOrderReturnByID.ApprovedBy == null ? "" : SaleOrderReturnByID.ApprovedBy.ContactName;
            lblApprovedDate.Text = SaleOrderReturnByID.ApprovedOn == null ? "" : ((DateTime)SaleOrderReturnByID.ApprovedOn).ToShortDateString();

            lblCancelledBy.Text = SaleOrderReturnByID.CancelledBy == null ? "" : SaleOrderReturnByID.CancelledBy.ContactName;
            lblCancelledDate.Text = SaleOrderReturnByID.CancelledOn == null ? "" : ((DateTime)SaleOrderReturnByID.CancelledOn).ToShortDateString();

            decimal Value = 0, TaxableValue = 0, TaxValue = 0, NetValue = 0;
            foreach (PSaleOrderReturnItem saleOrderReturnItem in SaleOrderReturnByID.SaleOrderReturnItems)
            {
                Value = Value + saleOrderReturnItem.SaleOrderDeliveryItem.Value;
                TaxableValue = TaxableValue + saleOrderReturnItem.SaleOrderDeliveryItem.TaxableValue;
                TaxValue = TaxValue + saleOrderReturnItem.SaleOrderDeliveryItem.CGSTValue + saleOrderReturnItem.SaleOrderDeliveryItem.SGSTValue + saleOrderReturnItem.SaleOrderDeliveryItem.IGSTValue;
                NetValue = NetValue + saleOrderReturnItem.SaleOrderDeliveryItem.TaxableValue + saleOrderReturnItem.SaleOrderDeliveryItem.CGSTValue + saleOrderReturnItem.SaleOrderDeliveryItem.SGSTValue + saleOrderReturnItem.SaleOrderDeliveryItem.IGSTValue;
                saleOrderReturnItem.SaleOrderDeliveryItem.SaleOrderItem.NetAmount = saleOrderReturnItem.SaleOrderDeliveryItem.TaxableValue + saleOrderReturnItem.SaleOrderDeliveryItem.CGSTValue + saleOrderReturnItem.SaleOrderDeliveryItem.SGSTValue + saleOrderReturnItem.SaleOrderDeliveryItem.IGSTValue;
            }

            lblValue.Text = Value.ToString();
            lblTaxableValue.Text = TaxableValue.ToString();
            lblTaxValue.Text = TaxValue.ToString();
            lblNetValue.Text = NetValue.ToString();

            gvSoReturnItem.DataSource = SaleOrderReturnByID.SaleOrderReturnItems;
            gvSoReturnItem.DataBind();
            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbCancel.Visible = true;
            lbApprove.Visible = true;
            lbCreateCreditNote.Visible = true;
            lbPreviewCreditNote.Visible = true;
            lbDowloadCreditNote.Visible = true;
            if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_ApprovalPending)
            {
                lbCreateCreditNote.Visible = false;
                lbPreviewCreditNote.Visible = false;
                lbDowloadCreditNote.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_Cancelled)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbPreviewCreditNote.Visible = false;
                lbDowloadCreditNote.Visible = false;
                lbCreateCreditNote.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_Approved)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbPreviewCreditNote.Visible = false;
                lbDowloadCreditNote.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_CreditNote)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbCreateCreditNote.Visible = false;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbCancel")
            {
                UpdateSaleOrderReturnStatus((short)AjaxOneStatus.SaleOrderReturn_Cancelled);
            }
            else if (lbActions.ID == "lbApprove")
            {
                UpdateSaleOrderReturnStatus((short)AjaxOneStatus.SaleOrderReturn_Approved);
            }
            else if (lbActions.ID == "lbCreateCreditNote")
            {
                UpdateSaleOrderReturnStatus((short)AjaxOneStatus.SaleOrderReturn_CreditNote);
            }
            else if(lbActions.ID == "lbPreviewCreditNote")
            {
                ViewSalesReturnCreditNote();
            }
            else if (lbActions.ID == "lbDowloadCreditNote")
            {
                DownloadSalesReturnCreditNote();
            }
        }
        protected void UpdateSaleOrderReturnStatus(int StatusID)
        {
            PSaleOrderReturn SaleOrderReturn = new PSaleOrderReturn();
            SaleOrderReturn.SaleOrderReturnID = SaleOrderReturnID;
            PApiResult Result = new BSalesOrderReturn().UpdateSaleOrderReturnStatus(SaleOrderReturnID, StatusID);
            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturn.Text = Result.Message;
                lblMessageSoReturn.ForeColor = Color.Red;
                return;
            }
            lblMessageSoReturn.Text = Result.Message;
            lblMessageSoReturn.ForeColor = Color.Green;
            fillViewSoReturn(SaleOrderReturnID);
        }
        void ViewSalesReturnCreditNote()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = SalesReturnCreditNoteRdlc(out mimeType);
                string FileName = SaleOrderReturnByID.SaleOrderReturnNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Procurement » Stock Transfer Order','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessageSoReturn.Text = ex.Message.ToString();
                lblMessageSoReturn.ForeColor = Color.Red;
            }
        }
        Byte[] SalesReturnCreditNoteRdlc(out string mimeType)
        {
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Dealer = new BDealer().GetDealerAddress(SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Dealer.DealerID)[0];
            string DealerAddress1 = (Dealer.Address.Address1 + (string.IsNullOrEmpty(Dealer.Address.Address2) ? "" : "," + Dealer.Address.Address2) + (string.IsNullOrEmpty(Dealer.Address.Address3) ? "" : "," + Dealer.Address.Address3)).Trim(',', ' ');
            string DealerAddress2 = (Dealer.Address.City + (string.IsNullOrEmpty(Dealer.Address.State.State) ? "" : "," + Dealer.Address.State.State) + (string.IsNullOrEmpty(Dealer.Address.Pincode) ? "" : "-" + Dealer.Address.Pincode)).Trim(',', ' ');

            string CustomerAddress1 = "", CustomerAddress2 = "", StateCode = "", GSTIN = "", PAN = "", CustomerCode = "", CustomerName = "";

            PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Customer.CustomerID);
            CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
            CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');
            
            ReportParameter[] P = new ReportParameter[18];

            P[0] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[1] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[2] = new ReportParameter("DealerAddress1", DealerAddress1, false);
            P[3] = new ReportParameter("DealerAddress2", DealerAddress2, false);
            P[4] = new ReportParameter("Remarks", SaleOrderReturnByID.Remarks, false);
            P[5] = new ReportParameter("TaxAmount", "", false);
            P[6] = new ReportParameter("NetAmount", "", false);
            P[7] = new ReportParameter("SalesReturnOrderNo", SaleOrderReturnByID.SaleOrderReturnNumber, false);
            P[8] = new ReportParameter("OrderDate", SaleOrderReturnByID.SaleOrderReturnDate.ToShortDateString(), false);
            P[9] = new ReportParameter("RefInvoice", SaleOrderReturnByID.SaleOrderDelivery.InvoiceNumber, false);
            P[10] = new ReportParameter("InvoiceCreditNoteNo", SaleOrderReturnByID.CreditNoteNumber, false);
            P[11] = new ReportParameter("Date", (SaleOrderReturnByID.CreditNoteDate == null) ? "" : Convert.ToDateTime(SaleOrderReturnByID.CreditNoteDate).ToShortDateString(), false);
            P[12] = new ReportParameter("Customer", SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Customer.CustomerCode, false);
            P[13] = new ReportParameter("Dealer", SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Dealer.DealerCode, false);
            P[14] = new ReportParameter("CustomerName", Customer.CustomerName.ToUpper(), false);
            P[15] = new ReportParameter("DealerName", SaleOrderReturnByID.SaleOrderDelivery.SaleOrder.Dealer.DealerName.ToUpper(), false);
            P[16] = new ReportParameter("IRNNo", "", false);
            P[17] = new ReportParameter("QRCodeImg", "", false);

            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("Uom");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Gross");
            dtItem.Columns.Add("Tax");
            dtItem.Columns.Add("Net");

            int sno = 0;
            decimal TaxableValue = 0, TaxAmount = 0, NetAmount = 0;
            foreach (PSaleOrderReturnItem Item in SaleOrderReturnByID.SaleOrderReturnItems)
            {
                dtItem.Rows.Add(sno += 1, Item.SaleOrderDeliveryItem.Material.MaterialCode, Item.SaleOrderDeliveryItem.Material.MaterialDescription, Item.Qty.ToString("0"), Item.SaleOrderDeliveryItem.Material.BaseUnit, String.Format("{0:n}", (Item.SaleOrderDeliveryItem.TaxableValue / Item.Qty)), String.Format("{0:n}", Item.SaleOrderDeliveryItem.TaxableValue), String.Format("{0:n}", (Item.SaleOrderDeliveryItem.CGSTValue + Item.SaleOrderDeliveryItem.SGSTValue + Item.SaleOrderDeliveryItem.IGSTValue)), String.Format("{0:n}", (Item.SaleOrderDeliveryItem.TaxableValue + Item.SaleOrderDeliveryItem.CGSTValue + Item.SaleOrderDeliveryItem.SGSTValue + Item.SaleOrderDeliveryItem.IGSTValue)));
                TaxableValue += Item.SaleOrderDeliveryItem.TaxableValue;
                TaxAmount += Item.SaleOrderDeliveryItem.CGSTValue + Item.SaleOrderDeliveryItem.SGSTValue + Item.SaleOrderDeliveryItem.IGSTValue;
                NetAmount += TaxableValue + TaxAmount;
            }
            P[5] = new ReportParameter("TaxAmount", String.Format("{0:n}", TaxAmount), false);
            P[6] = new ReportParameter("NetAmount", String.Format("{0:n}", NetAmount), false);

            report.ReportPath = Server.MapPath("~/Print/SalesInvoiceCreditNote.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesInvoiceCreditNote";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }
        void DownloadSalesReturnCreditNote()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = SaleOrderReturnByID.SaleOrderReturnNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = SalesReturnCreditNoteRdlc(out mimeType);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                new BXcel().PdfDowload();
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessageSoReturn.Text = ex.Message.ToString();
                lblMessageSoReturn.ForeColor = Color.Red;
            }
        }
    }
}