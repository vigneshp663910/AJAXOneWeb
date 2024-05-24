using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class PurchaseOrderReturnView : System.Web.UI.UserControl
    {
        public PPurchaseOrderReturn PoReturn
        {
            get
            {
                if (ViewState["PPurchaseOrderReturn"] == null)
                {
                    ViewState["PPurchaseOrderReturn"] = new PPurchaseOrderReturn();
                }
                return (PPurchaseOrderReturn)ViewState["PPurchaseOrderReturn"];
            }
            set
            {
                ViewState["PPurchaseOrderReturn"] = value;
            }
        }
        //public List<PPurchaseOrderReturnDelivery> PurchaseOrderReturnDeliveryList
        //{
        //    get
        //    {
        //        if (ViewState["PurchaseOrderReturnDelivery"] == null)
        //        {
        //            ViewState["PurchaseOrderReturnDelivery"] = new List<PPurchaseOrderReturnDelivery>();
        //        }
        //        return (List<PPurchaseOrderReturnDelivery>)ViewState["PurchaseOrderReturnDelivery"];
        //    }
        //    set
        //    {
        //        ViewState["PurchaseOrderReturnDelivery"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageDeliveryCreate.Text = "";
            lblMessageCancel.Text = "";
        }
        public void fillViewPoReturn(long PurchaseOrderReturnID)
        {
            PoReturn = new BDMS_PurchaseOrder().GetPurchaseOrderReturnByID(PurchaseOrderReturnID);
            lblPurchaseOrderReturnNumber.Text = PoReturn.PurchaseOrderReturnNumber;
            lblPurchaseOrderReturnDate.Text = PoReturn.PurchaseOrderReturnDate.ToString();
            lblPurchaseOrderReturnStatus.Text = PoReturn.PurchaseOrderReturnStatus.ProcurementStatus;
            lblPurchaseOrderReturnDealer.Text = PoReturn.Dealer.DealerCode + " " + PoReturn.Dealer.DealerName;
            lblPurchaseOrderReturnVendor.Text = PoReturn.Vendor.DealerCode + " " + PoReturn.Vendor.DealerName;
            lblLocation.Text = PoReturn.Location.OfficeName;
            lblRemarks.Text = PoReturn.Remarks;

            decimal TaxableAmt = 0, TaxAmt = 0, GrossAmt = 0;
            foreach (PPurchaseOrderReturnItem Item in PoReturn.PurchaseOrderReturnItems)
            {
                TaxableAmt += Item.TaxableValue;
                TaxAmt += Item.Material.CGSTValue + Item.Material.SGSTValue + Item.Material.IGSTValue;
            }
            GrossAmt = TaxableAmt + TaxAmt;
            lblTaxableAmount.Text = TaxableAmt.ToString();
            lblTaxAmount.Text = TaxAmt.ToString();
            lblGrossAmount.Text = GrossAmt.ToString();

            gvPOReturnItem.DataSource = PoReturn.PurchaseOrderReturnItems;
            gvPOReturnItem.DataBind();

            List<PPurchaseOrderReturnDelivery> PurchaseOrderReturnDeliveryList = new BDMS_PurchaseOrder().GetPurchaseOrderReturnDeliveryByPoReturnID(PurchaseOrderReturnID);
            gvPoReturnDelivery.DataSource = PurchaseOrderReturnDeliveryList;
            gvPoReturnDelivery.DataBind();

            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbRequestForApproval.Visible = true;
            lbApprove.Visible = true;
            lbReject.Visible = true;
            lbCancel.Visible = true;
            lbDeliveryCreate.Visible = true;

            int StatusID = PoReturn.PurchaseOrderReturnStatus.ProcurementStatusID;
            if (StatusID == (short)ProcurementStatus.PoReturnDraft)
            {
                lbApprove.Visible = false;
                lbReject.Visible = false;
                lbDeliveryCreate.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReturn_WaitingForApproval)
            {
                lbRequestForApproval.Visible = false;
                lbDeliveryCreate.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReturn_Approved || StatusID == (short)ProcurementStatus.PoReturn_PartiallyDelivered)
            {
                lbRequestForApproval.Visible = false;
                lbApprove.Visible = false;
                lbReject.Visible = false;
                lbCancel.Visible = false;
            }
            else if (StatusID == (short)ProcurementStatus.PoReturn_Rejected || StatusID == (short)ProcurementStatus.PoReturn_Cancelled || StatusID == (short)ProcurementStatus.PoReturn_Delivered)
            {
                lbRequestForApproval.Visible = false;
                lbApprove.Visible = false;
                lbReject.Visible = false;
                lbCancel.Visible = false;
                lbDeliveryCreate.Visible = false;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbPreviewPoReturn")
            {
                ViewPurchaseOrderReturn();
            }
            else if (lbActions.ID == "lbDownloadPoReturn")
            {
                DownloadPurchaseOrderReturn();
            }
            else if (lbActions.ID == "lbRequestForApproval")
            {
                PApiResult Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturn_WaitingForApproval, "");
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Result.Message;
                fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
            }
            else if (lbActions.ID == "lbApprove")
            {
                PApiResult Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturn_Approved, "");
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Result.Message;
                fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
            }
            else if (lbActions.ID == "lbReject")
            {
                txtRejectRemarks.Text = "";
                MPE_PoReturnReject.Show();
            }
            else if (lbActions.ID == "lbCancel")
            {
                txtCancelRemarks.Text = "";
                MPE_PoReturnCancel.Show();
            }
            else if (lbActions.ID == "lbDeliveryCreate")
            {
                Clear();
                divProceeedDelivery.Visible = false;
                MPE_PoReturnDeliveryCreate.Show();
                UC_PurchaseOrderReturnDeliveryCreate.fillPOReturnItem(PoReturn.PurchaseOrderReturnID);
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderReturnItemForDeliveryCreation(PoReturn.PurchaseOrderReturnID);
                if (JsonConvert.DeserializeObject<List<PPurchaseOrderReturn>>(JsonConvert.SerializeObject(Result.Data)).Count > 0)
                {
                    divProceeedDelivery.Visible = true;
                }
            }
        }
        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            Button lbActions = ((Button)sender);
            PApiResult Result = null;
            if (lbActions.ID == "btnReject")
            {
                MPE_PoReturnReject.Show();
                Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturn_Rejected, txtRejectRemarks.Text.Trim());
            }
            else if (lbActions.ID == "btnCancel")
            {
                MPE_PoReturnCancel.Show();
                Result = new BDMS_PurchaseOrder().UpdatePurchaseOrderReturnStatus(PoReturn.PurchaseOrderReturnID, (short)ProcurementStatus.PoReturn_Cancelled, txtCancelRemarks.Text.Trim());
            }

            if (Result.Status == PApplication.Failure)
            {
                lblMessageCancel.Text = Result.Message;
                return;
            }
            MPE_PoReturnReject.Hide();
            MPE_PoReturnCancel.Hide();
            fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
        }
        protected void btnPurchaseOrderReturnDeliveryCreateBack_Click(object sender, EventArgs e)
        {
            PnlPurchaseOrderReturnView.Visible = true;
            Panel pnlPoReturnDeliveryCreate = (Panel)UC_PurchaseOrderReturnDeliveryCreate.FindControl("pnlPoReturnDeliveryCreate");
            pnlPoReturnDeliveryCreate.Visible = false;
            pnlPoReturnDeliveryCreate.Visible = false;
        }
        protected void btnProceedDelivery_Click(object sender, EventArgs e)
        {
            UC_PurchaseOrderReturnDeliveryCreate.ReadPoReturnItem();
            divSave.Visible = true;
            divProceeedDelivery.Visible = false;
            MPE_PoReturnDeliveryCreate.Show();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessageDeliveryCreate.ForeColor = Color.Red;
            MPE_PoReturnDeliveryCreate.Show();
            string message = UC_PurchaseOrderReturnDeliveryCreate.RValidateReturnDelivery();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessageDeliveryCreate.Text = message;
                return;
            }

            List<PPurchaseOrderReturnDeliveryItem_Insert> poReturnDelivery = UC_PurchaseOrderReturnDeliveryCreate.ReadPoReturnDelivery();
            string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderReturnDeliveryCreate", poReturnDelivery);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageDeliveryCreate.Text = Result.Message;
                return;
            }

            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
            fillViewPoReturn(PoReturn.PurchaseOrderReturnID);
            tbpPoReturn.ActiveTabIndex = 1;
            Clear();
            MPE_PoReturnDeliveryCreate.Hide();
        }
        void Clear()
        {
            divSave.Visible = false;
            UC_PurchaseOrderReturnDeliveryCreate.Clear();
        }
        void ViewPurchaseOrderReturn()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = PurchaseOrderReturnRdlc(out mimeType);
                string FileName = PoReturn.PurchaseOrderReturnNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Procurement » Purchase Order','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] PurchaseOrderReturnRdlc(out string mimeType)
        {
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Dealer Supplier = new BDealer().GetDealerAddress(PoReturn.Vendor.DealerID)[0];
            string SupplierAddress1 = (Supplier.Address.Address1 + (string.IsNullOrEmpty(Supplier.Address.Address2) ? "" : "," + Supplier.Address.Address2) + (string.IsNullOrEmpty(Supplier.Address.Address3) ? "" : "," + Supplier.Address.Address3)).Trim(',', ' ');
            string SupplierAddress2 = (Supplier.Address.City + (string.IsNullOrEmpty(Supplier.Address.State.State) ? "" : "," + Supplier.Address.State.State) + (string.IsNullOrEmpty(Supplier.Address.Pincode) ? "" : "-" + Supplier.Address.Pincode)).Trim(',', ' ');

            PDMS_DealerOffice BillTo = new BDMS_Dealer().GetDealerOffice(PoReturn.Dealer.DealerID, PoReturn.Location.OfficeID, null)[0];
            string BillToAddress1 = (BillTo.Address1 + (string.IsNullOrEmpty(BillTo.Address2) ? "" : "," + BillTo.Address2) + (string.IsNullOrEmpty(BillTo.Address3) ? "" : "," + BillTo.Address3)).Trim(',', ' ');
            string BillToAddress2 = (BillTo.City + (string.IsNullOrEmpty(BillTo.State) ? "" : "," + BillTo.State) + (string.IsNullOrEmpty(BillTo.Pincode) ? "" : "-" + BillTo.Pincode)).Trim(',', ' ');

            //PDMS_Dealer BillTo = new BDealer().GetDealerAddress(PoReturn.Dealer.DealerID)[0];
            //string BillToAddress1 = (BillTo.Address.Address1 + (string.IsNullOrEmpty(BillTo.Address.Address2) ? "" : "," + BillTo.Address.Address2) + (string.IsNullOrEmpty(BillTo.Address.Address3) ? "" : "," + BillTo.Address.Address3)).Trim(',', ' ');
            //string BillToAddress2 = (BillTo.Address.City + (string.IsNullOrEmpty(BillTo.Address.State.State) ? "" : "," + BillTo.Address.State.State) + (string.IsNullOrEmpty(BillTo.Address.Pincode) ? "" : "-" + BillTo.Address.Pincode)).Trim(',', ' ');

            ReportParameter[] P = new ReportParameter[15];
            P[0] = new ReportParameter("ReturnPurchaseOrderNumber", PoReturn.PurchaseOrderReturnNumber, false);
            P[1] = new ReportParameter("ReturnPurchaseOrderDate", PoReturn.PurchaseOrderReturnDate.ToShortDateString(), false);
            P[2] = new ReportParameter("SupplierName", Supplier.DealerName, false);
            P[3] = new ReportParameter("SupplierAddress1", SupplierAddress1, false);
            P[4] = new ReportParameter("SupplierAddress2", SupplierAddress2, false);
            P[5] = new ReportParameter("BillToCustomerName", PoReturn.Dealer.DealerName, false);
            P[6] = new ReportParameter("BillToCustomerAddress1", BillToAddress1, false);
            P[7] = new ReportParameter("BillToCustomerAddress2", BillToAddress2, false);
            P[8] = new ReportParameter("ReceivingLocation", PoReturn.Location.OfficeName, false);
            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("ItemNo");
            dtItem.Columns.Add("PartNo");
            dtItem.Columns.Add("Description");
            dtItem.Columns.Add("Qty");
            dtItem.Columns.Add("UOM");
            dtItem.Columns.Add("UnitPrice");
            dtItem.Columns.Add("Taxable");
            dtItem.Columns.Add("Tax");
            dtItem.Columns.Add("Net");
            decimal GrandTotal = 0, TaxTotal = 0;
            foreach (PPurchaseOrderReturnItem Item in PoReturn.PurchaseOrderReturnItems)
            {
                if (Item.Material.IGST == 0)
                {
                    dtItem.Rows.Add(Item.Item, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Quantity.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Material.CurrentPrice), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", (Item.Material.CGSTValue + Item.Material.SGSTValue)), String.Format("{0:n}", ((Item.TaxableValue + Item.Value) + (Item.Material.CGSTValue + Item.Material.SGSTValue))));
                    TaxTotal += (Item.Material.CGSTValue + Item.Material.SGSTValue);
                    GrandTotal += (Item.TaxableValue + Item.Material.CGSTValue + Item.Material.SGSTValue);
                }
                else
                {
                    dtItem.Rows.Add(Item.Item, Item.Material.MaterialCode, Item.Material.MaterialDescription, Item.Quantity.ToString("0"), Item.Material.BaseUnit, String.Format("{0:n}", Item.Value), String.Format("{0:n}", Item.TaxableValue), String.Format("{0:n}", (Item.Material.IGSTValue)), String.Format("{0:n}", (Item.TaxableValue + Item.Material.IGSTValue)));
                    TaxTotal += (Item.Material.IGSTValue);
                    GrandTotal += (Item.TaxableValue + Item.Material.IGSTValue);
                }
            }
            P[9] = new ReportParameter("TaxAmount", String.Format("{0:n}", TaxTotal), false);
            P[10] = new ReportParameter("NetAmount", String.Format("{0:n}", GrandTotal), false);
            P[11] = new ReportParameter("Remarks", PoReturn.Remarks, false);
            P[12] = new ReportParameter("SupplierCode", Supplier.DealerCode, false);
            P[13] = new ReportParameter("BillToCode", PoReturn.Dealer.DealerCode, false);
            P[14] = new ReportParameter("Status", PoReturn.PurchaseOrderReturnStatus.ProcurementStatus, false);
            report.ReportPath = Server.MapPath("~/Print/PurchaseOrderReturn.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "ReturnPurchaseOrder";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
            return mybytes;
        }
        void DownloadPurchaseOrderReturn()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = PoReturn.PurchaseOrderReturnNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = PurchaseOrderReturnRdlc(out mimeType);
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
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}