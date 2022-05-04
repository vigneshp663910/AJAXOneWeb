using DataAccess;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Business
{
    public class BSalesCommissionClaim
    {
        private IDataAccess provider;

        public BSalesCommissionClaim() { provider = new ProviderFactory().GetProvider(); }
        public List<PSalesQuotation> GetSalesQuotationForClaimCreate(string QuotationNo, string QuotationDateFrom, string QuotationDateTo)
        {
            string endPoint = "SalesCommission/SalesQuotationForClaimCreate?QuotationNo=" + QuotationNo + "&QuotationDateFrom=" + QuotationDateFrom + "&QuotationDateTo=" + QuotationDateTo;
            return JsonConvert.DeserializeObject<List<PSalesQuotation>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public int InsertSalesCommissionClaim(long SalesQuotationID)
        {
            string endPoint = "SalesCommission/InsertSalesCommissionClaim?SalesQuotationID=" + SalesQuotationID;
            return JsonConvert.DeserializeObject<int>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesCommissionClaim> GetSalesCommissionClaim(long? SalesCommissionClaimID, long? SalesQuotationID
         , string ClaimNumber, string ClaimDateFrom, string ClaimDateTo, int? StatusID, int? DealerID)
        {
            string endPoint = "SalesCommission/SalesCommissionClaim?SalesCommissionClaimID=" + SalesCommissionClaimID + "&SalesQuotationID=" + SalesQuotationID + "&ClaimNumber=" + ClaimNumber
                + "&ClaimDateFrom=" + ClaimDateFrom + "&ClaimDateTo=" + ClaimDateTo + "&StatusID=" + StatusID + "&DealerID=" + DealerID;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public PSalesCommissionClaim GetSalesCommissionClaimByID(long SalesCommissionClaimID)
        {
            string endPoint = "SalesCommission/GetSalesCommissionClaimByID?SalesCommissionClaimID=" + SalesCommissionClaimID;
            return JsonConvert.DeserializeObject<PSalesCommissionClaim>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));

        }
        public List<PSalesCommissionClaim> GetSalesCommissionClaimApproval(int? DealerID, string ClaimNumber, string ClaimDateFrom, string ClaimDateTo, int? StatusID)
        {
            string endPoint = "SalesCommission/ClaimForApproval?DealerID=" + DealerID + "&ClaimNumber=" + ClaimNumber + "&ClaimDateFrom=" + ClaimDateFrom + "&ClaimDateTo=" + ClaimDateTo + "&StatusID=" + StatusID;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public List<PSalesCommissionClaim> GetSalesCommissionClaimForInvoiceCreate(int? DealerID, string ClaimNumber, string ClaimDateFrom, string ClaimDateTo)
        {
            string endPoint = "SalesCommission/ClaimForInvoiceCreate?DealerID=" + DealerID + "&ClaimNumber=" + ClaimNumber + "&ClaimDateFrom=" + ClaimDateFrom + "&ClaimDateTo=" + ClaimDateTo;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }

        public List<PSalesCommissionClaimInvoice> GetSalesCommissionClaimInvoice(long? SalesCommissionClaimInvoiceID, int? DealerID, string InvoiceNumber, string InvoiceDateFrom, string InvoiceDateTo)
        {
            string endPoint = "SalesCommission/SalesCommissionClaimInvoice?SalesCommissionClaimInvoiceID" + SalesCommissionClaimInvoiceID
                + "&DealerID=" + DealerID + "&InvoiceNumber=" + InvoiceNumber + "&InvoiceDateFrom=" + InvoiceDateFrom + "&InvoiceDateTo=" + InvoiceDateTo;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaimInvoice>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
        }
        public PAttachedFile GetSalesCommissionClaimInvoiceFile(long SalesCommissionClaimInvoiceID)
        {
            DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("SalesCommissionClaimInvoiceID", SalesCommissionClaimInvoiceID, DbType.Int64);
            PAttachedFile Files = null;
            try
            {
                DbParameter[] Params = new DbParameter[1] { WarrantyClaimInvoiceIDP };

                using (DataSet DS = provider.Select("GetSalesCommissionClaimInvoiceFile", Params))
                {
                    if (DS != null)
                    {
                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            Files = new PAttachedFile()
                            {
                                AttachedFile = (Byte[])(dr["InvoiceFiIe"]),
                                FileType = Convert.ToString(dr["ContentType"]),
                                FileName = Convert.ToString(dr["FileName"])
                            };
                        }
                    }
                }
                if (Files == null)
                {
                    InsertSalesCommissionClaimInvoiceFile(SalesCommissionClaimInvoiceID, SalesCommissionClaimInvoice(SalesCommissionClaimInvoiceID));
                    //Files = GetSalesCommissionClaimInvoiceFile_(SalesCommissionClaimInvoiceID);
                }
                return Files;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "GetWarrantyClaimInvoiceFile", ex);
                return null;
            }

        }

        //private  PAttachedFile GetSalesCommissionClaimInvoiceFile_(long SalesCommissionClaimInvoiceID)
        //{
        //    DbParameter WarrantyClaimInvoiceIDP = provider.CreateParameter("SalesCommissionClaimInvoiceID", SalesCommissionClaimInvoiceID, DbType.Int64);
        //    PAttachedFile Files = null;
        //    try
        //    {
        //        DbParameter[] Params = new DbParameter[1] { WarrantyClaimInvoiceIDP };

        //        using (DataSet DS = provider.Select("GetSalesCommissionClaimInvoiceFile", Params))
        //        {
        //            if (DS != null)
        //            {
        //                foreach (DataRow dr in DS.Tables[0].Rows)
        //                {
        //                    Files = new PAttachedFile()
        //                    {
        //                        AttachedFile = (Byte[])(dr["InvoiceFiIe"]),
        //                        FileType = Convert.ToString(dr["ContentType"]),
        //                        FileName = Convert.ToString(dr["FileName"])
        //                    };
        //                }
        //            }
        //        } 
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "GetWarrantyClaimInvoiceFile", ex);
        //        return null;
        //    }
        //    return Files;
        //}
        public void InsertSalesCommissionClaimInvoiceFile(long SalesCommissionClaimInvoiceID, PAttachedFile InvFile)
        {
            DbParameter SalesCommissionClaimInvoiceIDP = provider.CreateParameter("SalesCommissionClaimInvoiceID", SalesCommissionClaimInvoiceID, DbType.String);
            DbParameter InvFileP = provider.CreateParameter("InvFile", InvFile.AttachedFile, DbType.Binary);
            DbParameter FileTypeP = provider.CreateParameter("FileType", InvFile.FileType, DbType.String);
            DbParameter[] Params = new DbParameter[3] { SalesCommissionClaimInvoiceIDP, InvFileP, FileTypeP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertSalesCommissionClaimInvoiceFile", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", "InsertSalesCommissionClaimInvoiceFile", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_WarrantyClaimInvoice", " InsertSalesCommissionClaimInvoiceFile", ex);
            }
        }
        private PAttachedFile SalesCommissionClaimInvoice(long SalesCommissionClaimInvoiceID)
        {
            try
            {
                PSalesCommissionClaimInvoice SalesCommissionClaimInvoice = new BSalesCommissionClaim().GetSalesCommissionClaimInvoice(SalesCommissionClaimInvoiceID, null, null, null, null)[0];

                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SalesCommissionClaimInvoice.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
                string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
                string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');



                //DataTable CommissionDT = new DataTable();
                //CommissionDT.Columns.Add("SNO");
                //CommissionDT.Columns.Add("Material");
                //CommissionDT.Columns.Add("Description");
                //CommissionDT.Columns.Add("HSN");
                //CommissionDT.Columns.Add("Qty");
                //CommissionDT.Columns.Add("Rate");
                //CommissionDT.Columns.Add("Value", typeof(decimal));
                //CommissionDT.Columns.Add("CGST");
                //CommissionDT.Columns.Add("SGST");
                //CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                //CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                //CommissionDT.Columns.Add("Amount", typeof(decimal));


                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;

                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = null;
                if ((SalesCommissionClaimInvoice.Dealer.IsEInvoice) && (SalesCommissionClaimInvoice.Dealer.EInvoiceDate <= SalesCommissionClaimInvoice.InvoiceDate))
                {
                    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getSalesCommissionClaimInvoiceESigned(SalesCommissionClaimInvoiceID);
                    P = new ReportParameter[43];
                    P[41] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, SalesCommissionClaimInvoice.InvoiceNumber), false);
                    P[42] = new ReportParameter("IRN", "IRN : " + SalesCommissionClaimInvoice.IRN, false);
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesCommisionTaxQuotationQRCode.rdlc");
                }
                else
                {
                    P = new ReportParameter[41];
                    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesCommisionTaxQuotation.rdlc");
                }

                string StateCode = Dealer.State.StateCode;
                decimal GrandTotal = 0;
                PSalesCommissionClaimInvoiceItem item = SalesCommissionClaimInvoice.InvoiceItem;
                if (item.SGST != 0)
                {
                    P[23] = new ReportParameter("Amount", (item.Qty * item.Rate).ToString(), false);
                    P[24] = new ReportParameter("SGSTValue", item.SGSTValue.ToString(), false);
                    P[25] = new ReportParameter("CGSTValue", item.CGSTValue.ToString(), false);
                    P[39] = new ReportParameter("SGST", "SGST @ " + item.SGST, false);
                    P[40] = new ReportParameter("CGST", "CGST @ " + item.CGST, false);
                    //CommissionDT.Rows.Add(1, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, (item.Qty * item.Rate), item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, (item.Qty * item.Rate) + item.CGSTValue + item.SGSTValue);
                    GrandTotal = (item.Qty * item.Rate) + item.CGSTValue + item.SGSTValue;
                    P[26] = new ReportParameter("GrandTotal", GrandTotal.ToString(), false);
                    P[27] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                }
                else
                {
                    P[23] = new ReportParameter("Amount", (item.Qty * item.Rate).ToString(), false);
                    P[24] = new ReportParameter("SGSTValue", "", false);
                    P[25] = new ReportParameter("CGSTValue", item.IGSTValue.ToString(), false);
                    P[39] = new ReportParameter("", "", false);
                    P[40] = new ReportParameter("CGST", "IGST @ " + item.IGST, false);
                    //CommissionDT.Rows.Add(1, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, (item.Qty * item.Rate), item.IGST, null, item.IGSTValue, null, (item.Qty * item.Rate) + item.IGSTValue);
                    GrandTotal = (item.Qty * item.Rate) + item.IGSTValue;
                    P[26] = new ReportParameter("GrandTotal", GrandTotal.ToString(), false);
                    P[27] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                }

                P[0] = new ReportParameter("CompanyName", Dealer.CustomerFullName, false);
                P[1] = new ReportParameter("CompanyAddress1", DealerAddress1, false);
                P[2] = new ReportParameter("CompanyAddress2", DealerAddress2, false);
                P[3] = new ReportParameter("QuotationType", "TAX INVOICE", false);
                P[4] = new ReportParameter("InvoiceNo", SalesCommissionClaimInvoice.InvoiceNumber, false);
                P[5] = new ReportParameter("InvoiceDate", SalesCommissionClaimInvoice.InvoiceDate.ToString(), false);
                P[6] = new ReportParameter("IncomeTaxPAN", Dealer.PAN, false);
                P[7] = new ReportParameter("ITGST", Dealer.GSTIN, false);
                P[8] = new ReportParameter("ITGSTStateCode", Dealer.State.StateCode, false);
                P[9] = new ReportParameter("ITGSTState", Dealer.State.State, false);
                P[10] = new ReportParameter("CustomerStateCode", Ajax.State.StateCode, false);
                P[11] = new ReportParameter("AFPAN", Ajax.PAN, false);
                P[12] = new ReportParameter("AFGSTN", Ajax.GSTIN, false);
                P[13] = new ReportParameter("Nameofservice", "", false);
                P[14] = new ReportParameter("ServiceCategory", "", false);
                P[15] = new ReportParameter("HSNCode", item.Material.HSN, false);
                P[16] = new ReportParameter("Placeofsupply", "", false);
                P[17] = new ReportParameter("Model", item.Material.Model.ModelCode + " - " + item.Material.MaterialDivision, false);
                P[18] = new ReportParameter("SerialNo", "", false);
                P[19] = new ReportParameter("MInvoiceNo", "", false);
                P[20] = new ReportParameter("MInvoiceDate", "", false);
                P[21] = new ReportParameter("CustomerName", SalesCommissionClaimInvoice.Customer.CustomerName + " " + SalesCommissionClaimInvoice.Customer.CustomerName2, false);
                P[22] = new ReportParameter("CustomerCode", SalesCommissionClaimInvoice.Customer.CustomerCode, false);
                P[28] = new ReportParameter("ClaimNo", "", false);
                P[29] = new ReportParameter("ClaimDate", "", false);
                P[30] = new ReportParameter("AccDocNo", "", false);
                P[31] = new ReportParameter("AccYear", "", false);
                P[32] = new ReportParameter("AjaxName", Ajax.CustomerFullName, false);
                P[33] = new ReportParameter("AjaxAddress1", AjaxCustomerAddress1, false);
                P[34] = new ReportParameter("AjaxAddress2", AjaxCustomerAddress2, false);
                P[35] = new ReportParameter("AjaxCINandGST", "CIN:" + Ajax.PAN + ",GST:" + Ajax.GSTIN, false);
                P[36] = new ReportParameter("AjaxPAN", "PAN:" + Ajax.PAN, false);
                P[38] = new ReportParameter("AjaxTelephoneandEmail", "T:" + Ajax.Mobile + ",Email:" + Ajax.Email, false);



                //ReportDataSource rds = new ReportDataSource();
                //rds.Name = "SalesCommisionInvoice";//This refers to the dataset name in the RDLC file  
                //rds.Value = CommissionDT;
                //report.DataSources.Add(rds);
                report.SetParameters(P);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;

                return InvF;
            }
            catch (Exception ex)
            {
                //  lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                //   lblMessage.ForeColor = Color.Red;
                //  lblMessage.Visible = true;
            }
            return null;
        }
    }
}
