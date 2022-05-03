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
                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(SalesCommissionClaimInvoiceID, "", null, null, null, 3, "")[0];
                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(ClaimInvoice.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                PDMS_WarrantyInvoiceHeader WarrantyInvoiceHeader = new BDMS_WarrantyClaim().GetWarrantyClaimReport("", null, null, ClaimInvoice.AnnexureNumber, null, null, "", null, null, null, "", "", "", false, 1)[0];

                PDMS_Customer Customer = new SCustomer().getCustomerAddress(WarrantyInvoiceHeader.CustomerCode);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');


                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value", typeof(decimal));
                CommissionDT.Columns.Add("CGST");
                CommissionDT.Columns.Add("SGST");
                CommissionDT.Columns.Add("CGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("SGSTValue", typeof(decimal));
                CommissionDT.Columns.Add("Amount", typeof(decimal));

                CommissionDT.Rows.Add(1, "Material", "Desc", 9876, 1, 10, 100, 18, 18, 18, 180, 100 + 18 + 18);


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

                P = new ReportParameter[21];
                P[19] = new ReportParameter("QRCodeImg", "");
                P[20] = new ReportParameter("IRN", "IRN : ", false);
                report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_SalesClaimInvoice.rdlc");


                //   ViewState["Month"] = ddlMonth.SelectedValue;
                P[0] = new ReportParameter("DealerCode", "", false);
                P[1] = new ReportParameter("Annexure", "", false);
                P[2] = new ReportParameter("DateOfClaim", "", false);
                P[3] = new ReportParameter("DealerName", "", false);
                P[4] = new ReportParameter("Address1", "", false);
                P[5] = new ReportParameter("Address2", "", false);
                P[6] = new ReportParameter("Contact", "Contact", false);
                P[7] = new ReportParameter("GSTIN", "", false);
                P[8] = new ReportParameter("GST_Header", "", false);
                P[9] = new ReportParameter("GrandTotal", "", false);
                P[10] = new ReportParameter("AmountInWord", "", false);
                P[11] = new ReportParameter("InvoiceNumber", "", false);
                P[12] = new ReportParameter("PeriodFrom", "", false);
                P[13] = new ReportParameter("PeriodTo", "", false);
                P[14] = new ReportParameter("PAN", "", false);
                //DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                //string NewLogo = "0";
                //if (NewLogoDate <= ClaimInvoice.InvoiceDate)
                //{
                //    NewLogo = "1";
                //}
                P[15] = new ReportParameter("NewLogo", "", false);
                P[16] = new ReportParameter("TCSValue", "", false);
                P[17] = new ReportParameter("TCSSubTotal", "", false);
                P[18] = new ReportParameter("TCSTax", "", false);


                string StateCode = Dealer.State.StateCode;
                string GST_Header = "";
                int i = 0;
                decimal TCSSubTotal = 0;
                foreach (PDMS_WarrantyClaimInvoiceItem item in ClaimInvoice.InvoiceItems)
                {

                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        GST_Header = "CGST & SGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.ApprovedValue + item.CGSTValue + item.SGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.CGSTValue + item.SGSTValue;
                    }
                    else
                    {
                        GST_Header = "IGST";
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, item.Rate, item.ApprovedValue, item.IGST, null, item.IGSTValue, null, item.ApprovedValue + item.IGSTValue);
                        TCSSubTotal = TCSSubTotal + item.ApprovedValue + item.IGSTValue;
                    }
                }

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesCommisionInvoice";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
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
