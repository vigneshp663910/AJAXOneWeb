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
using System.IO;
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
        public List<PSalesCommissionClaim> GetSalesQuotationForClaimCreate(int? DealerID, string QuotationNo, string QuotationDateFrom, string QuotationDateTo)
        {
            string endPoint = "SalesCommission/SalesQuotationForClaimCreate?DealerID="+ DealerID + " & QuotationNo=" + QuotationNo + "&QuotationDateFrom=" + QuotationDateFrom + "&QuotationDateTo=" + QuotationDateTo;
            return JsonConvert.DeserializeObject<List<PSalesCommissionClaim>>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data));
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
                //if (Files == null)
                //{
                //    InsertSalesCommissionClaimInvoiceFile(SalesCommissionClaimInvoiceID, SalesCommissionClaimInvoice(SalesCommissionClaimInvoiceID));
                //    //Files = GetSalesCommissionClaimInvoiceFile_(SalesCommissionClaimInvoiceID);
                //}
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

                //PSalesCommissionClaim salesCommissionClaim = GetSalesCommissionClaimByID(SalesCommissionClaimInvoice.SalesCommissionClaimID);

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


                //string contentType = string.Empty;
                //contentType = "application/pdf";
                //var CC = CultureInfo.CurrentCulture;
                //string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                //string extension;
                //string encoding;
                //string mimeType;
                //string[] streams;
                //Warning[] warnings;

                //LocalReport report = new LocalReport();
                //report.EnableExternalImages = true;

                //ReportParameter[] P = null;

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                Random r = new Random();
                string FileName = "SC_Tax" + r.Next(0, 1000000) + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;
                ReportParameter[] P = null;


                //if ((SalesCommissionClaimInvoice.Dealer.IsEInvoice) && (SalesCommissionClaimInvoice.Dealer.EInvoiceDate <= SalesCommissionClaimInvoice.InvoiceDate))
                //{
                //    PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getSalesCommissionClaimInvoiceESigned(SalesCommissionClaimInvoiceID);
                //    P = new ReportParameter[42];
                //    P[40] = new ReportParameter("QRCodeImg", new BDMS_EInvoice().GetQRCodePath(EInvoiceSigned.SignedQRCode, SalesCommissionClaimInvoice.InvoiceNumber), false);
                //    P[41] = new ReportParameter("IRN", "IRN : " + SalesCommissionClaimInvoice.IRN, false);
                //    report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesCommisionTaxQuotationQRCode.rdlc");
                //}
                //else
                //{
                P = new ReportParameter[40];
                    //report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/SalesCommisionTaxQuotation.rdlc");
                //}

                string StateCode = Dealer.State.StateCode;
                decimal GrandTotal = 0;
                PSalesCommissionClaimInvoiceItem item = SalesCommissionClaimInvoice.InvoiceItem;
                if (item.SGST != 0)
                {
                    P[23] = new ReportParameter("Amount", (item.Qty * item.Rate).ToString(), false);
                    P[24] = new ReportParameter("SGSTValue", item.SGSTValue.ToString(), false);
                    P[25] = new ReportParameter("CGSTValue", item.CGSTValue.ToString(), false);
                    P[38] = new ReportParameter("SGST", "SGST @ " + item.SGST, false);
                    P[39] = new ReportParameter("CGST", "CGST @ " + item.CGST, false);
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
                    P[38] = new ReportParameter("SGST", "", false);
                    P[39] = new ReportParameter("CGST", "IGST @ " + item.IGST.ToString(), false);
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
                P[13] = new ReportParameter("Nameofservice", "Sales Commission", false);
                P[14] = new ReportParameter("ServiceCategory", "Services provided for a fee/commission or contract basis on retail trade", false);
                P[15] = new ReportParameter("HSNCode", "996211", false);
                P[16] = new ReportParameter("Placeofsupply", "Karnataka", false);
                P[17] = new ReportParameter("Model", (SalesCommissionClaimInvoice.Quotation.Model.ModelCode==null)?"": SalesCommissionClaimInvoice.Quotation.Model.ModelCode + " - " + SalesCommissionClaimInvoice.Quotation.Model.Division.DivisionDescription, false);
                P[18] = new ReportParameter("SerialNo", (item.Material.MaterialSerialNumber==null)?"":item.Material.MaterialSerialNumber, false);
                P[19] = new ReportParameter("MInvoiceNo", SalesCommissionClaimInvoice.Quotation.SalesInvoiceNumber, false);
                P[20] = new ReportParameter("MInvoiceDate", SalesCommissionClaimInvoice.Quotation.SalesInvoiceDate.ToString(), false);
                // P[21] = new ReportParameter("CustomerName", (SalesCommissionClaimInvoice.Claim.Quotation==null) ? "" : SalesCommissionClaimInvoice.Claim.Quotation.Lead.Customer.CustomerName + " " + SalesCommissionClaimInvoice.Quotation.Lead.Customer.CustomerName2, false);
                P[21] = new ReportParameter("CustomerName", (SalesCommissionClaimInvoice.Claim.Quotation == null) ? "" : SalesCommissionClaimInvoice.Claim.Quotation.Lead.Customer.CustomerName, false);
                P[22] = new ReportParameter("CustomerCode", (SalesCommissionClaimInvoice.Claim.Quotation == null) ? "" : SalesCommissionClaimInvoice.Quotation.Lead.Customer.CustomerCode, false);
                P[28] = new ReportParameter("ClaimNo", SalesCommissionClaimInvoice.Claim.ClaimNumber, false);
                P[29] = new ReportParameter("ClaimDate", SalesCommissionClaimInvoice.Claim.ClaimDate.ToString(), false);
                P[30] = new ReportParameter("AccDocNo", "", false);
                P[31] = new ReportParameter("AccYear", "", false);
                P[32] = new ReportParameter("AjaxName", Ajax.CustomerFullName, false);
                P[33] = new ReportParameter("AjaxAddress1", AjaxCustomerAddress1, false);
                P[34] = new ReportParameter("AjaxAddress2", AjaxCustomerAddress2, false);
                P[35] = new ReportParameter("AjaxCINandGST", "CIN:" + Ajax.PAN + ",GST:" + Ajax.GSTIN, false);
                P[36] = new ReportParameter("AjaxPAN", "PAN:" + Ajax.PAN, false);
                P[37] = new ReportParameter("AjaxTelephoneandEmail", "T:" + Ajax.Mobile + ",Email:" + Ajax.Email, false);

                string pic = Path.GetFileName("SalesCommisionTaxQuotation.rdlc");//image name
                string path = Path.GetFullPath(Path.Combine(HttpContext.Current.Server.MapPath("../../"), @"Print/"));//path in my project api
                string path2 = Path.Combine(path, Path.GetFileName(pic));//try combine path folder in api + image name


                report.ReportPath = path2;
                report.SetParameters(P);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesCommisionInvoice";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                PAttachedFile InvF = new PAttachedFile();

                InvF.FileType = mimeType;
                InvF.AttachedFile = mybytes;
                InvF.AttachedFileID = 0;
                InvF.FileName = FileName;
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
        //public List<PSalesCommissionClaimPrice> GetSalesCommissionClaimPrice(int? PlantID, string Material)
        //{
        //    List<PSalesCommissionClaimPrice> MML = new List<PSalesCommissionClaimPrice>();
        //    try
        //    {
        //        DbParameter PlantIDP = provider.CreateParameter("PlantID", PlantID, DbType.Int32);
        //        DbParameter MaterailP = provider.CreateParameter("Material", String.IsNullOrEmpty(Material) ? null : Material, DbType.String);

        //        DbParameter[] Params = new DbParameter[2] { PlantIDP, MaterailP };
        //        using (DataSet DataSet = provider.Select("GetSalesCommissionClaimPrice", Params))
        //        {
        //            if (DataSet != null)
        //            {
        //                foreach (DataRow dr in DataSet.Tables[0].Rows)
        //                {
        //                    MML.Add(new PSalesCommissionClaimPrice()
        //                    {
        //                        SalesCommissionClaimPriceID = Convert.ToInt32(dr["SalesCommissionClaimPriceID"]),
        //                        PlantName = new PPlant()
        //                        {
        //                            PlantCode = Convert.ToString(dr["PlantCode"]),
        //                            PlantID = Convert.ToInt32(dr["PlantID"]),
        //                        }, 
        //                        Materail = new PDMS_Material()
        //                        {
        //                            MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
        //                            MaterialCode = Convert.ToString(dr["MaterialCode"]),
        //                            MaterialID=Convert.ToInt32(dr["MaterialID"])
        //                        }, 

        //                        Percentage = dr["Percentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Percentage"]),
        //                        Amount = dr["Percentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Amount"]),
        //                        IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"])
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    { }
        //    catch (Exception ex)
        //    { }
        //    return MML;
        //}

        public List<PSalesCommissionClaimPrice> GetSalesCommissionClaimPrice(string Material)
        {
            List<PSalesCommissionClaimPrice> MML = new List<PSalesCommissionClaimPrice>();
            try
            {
                DbParameter MaterailP = provider.CreateParameter("Material", String.IsNullOrEmpty(Material) ? null : Material, DbType.String);

                DbParameter[] Params = new DbParameter[1] { MaterailP };
                using (DataSet DataSet = provider.Select("GetSalesCommissionClaimPrice", Params))
                {
                    if (DataSet != null)
                    {
                        foreach (DataRow dr in DataSet.Tables[0].Rows)
                        {
                            MML.Add(new PSalesCommissionClaimPrice()
                            {
                                SalesCommissionClaimPriceID = Convert.ToInt32(dr["SalesCommissionClaimPriceID"]),
                                //PlantName = new PPlant()
                                //{
                                //    PlantCode = Convert.ToString(dr["PlantCode"]),
                                //    PlantID = Convert.ToInt32(dr["PlantID"]),
                                //},
                                Materail = new PDMS_Material()
                                {
                                    MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                    MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                    MaterialID = Convert.ToInt32(dr["MaterialID"])
                                },

                                Percentage = dr["Percentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Percentage"]),
                                Amount = dr["Percentage"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(dr["Amount"]),
                                IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"])
                            });
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return MML;
        }
        //public Boolean InsertOrUpdateSalesCommissionClaimPrice(int? SalesCommissionClaimPriceID, int? PlantID, int? MaterialID, decimal Percentage, decimal Amount, int UserID, Boolean IsActive)
        //{
        //    TraceLogger.Log(DateTime.Now);
        //    DbParameter SalesCommissionClaimPriceIDP = provider.CreateParameter("SalesCommissionClaimPriceID", SalesCommissionClaimPriceID, DbType.Int32);
        //    DbParameter PlantIDP = provider.CreateParameter("PlantID", PlantID, DbType.Int32);
        //    DbParameter MaterialIDP = provider.CreateParameter("MaterialID", MaterialID, DbType.Int32);
        //    DbParameter PercentageP = provider.CreateParameter("Percentage",Percentage, DbType.Decimal);
        //    DbParameter AmountP = provider.CreateParameter("Amount",  Amount, DbType.Decimal);
        //    DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
        //    DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
        //    DbParameter[] Params = new DbParameter[7] { SalesCommissionClaimPriceIDP, PlantIDP, MaterialIDP, PercentageP, AmountP, IsActiveP, UserIDP };
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            provider.Insert("InsertOrUpdateSalesCommissionClaimPrice", Params);
        //            scope.Complete();
        //        }
        //        return true;
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        new FileLogger().LogMessage("BSalesCommissionClaim", " InsertOrUpdateSalesCommissionClaimPrice", sqlEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("BSalesCommissionClaim", "  InsertOrUpdateSalesCommissionClaimPrice", ex);
        //    }

        //    TraceLogger.Log(DateTime.Now);
        //    return false;
        //}

        public Boolean InsertOrUpdateSalesCommissionClaimPrice(int? SalesCommissionClaimPriceID, int? MaterialID, decimal Percentage, decimal Amount, int UserID, Boolean IsActive)
        {
            TraceLogger.Log(DateTime.Now);
            DbParameter SalesCommissionClaimPriceIDP = provider.CreateParameter("SalesCommissionClaimPriceID", SalesCommissionClaimPriceID, DbType.Int32);
            DbParameter MaterialIDP = provider.CreateParameter("MaterialID", MaterialID, DbType.Int32);
            DbParameter PercentageP = provider.CreateParameter("Percentage", Percentage, DbType.Decimal);
            DbParameter AmountP = provider.CreateParameter("Amount", Amount, DbType.Decimal);
            DbParameter IsActiveP = provider.CreateParameter("IsActive", IsActive, DbType.Boolean);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[6] { SalesCommissionClaimPriceIDP, MaterialIDP, PercentageP, AmountP, IsActiveP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("InsertOrUpdateSalesCommissionClaimPrice", Params);
                    scope.Complete();
                }
                return true;
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BSalesCommissionClaim", " InsertOrUpdateSalesCommissionClaimPrice", sqlEx);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BSalesCommissionClaim", "  InsertOrUpdateSalesCommissionClaimPrice", ex);
            }

            TraceLogger.Log(DateTime.Now);
            return false;
        }

    }
}
