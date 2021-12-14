using DataAccess;
using Microsoft.Reporting.WebForms;
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
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;

namespace Business
{
  public  class BDMS_OverhaulService
    {
         private IDataAccess provider;
         public BDMS_OverhaulService()
        {
            provider = new ProviderFactory().GetProvider();
        }
         public List<PDMS_PaidServiceInvoice> GetOverhaulServiceQuotation(long? ServiceQuotationID, long? ICTicketID, string QuotationNumber, DateTime? QuotationDateF, DateTime? QuotationDateT, int? DealerID, string CustomerCode)
         {
             List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
             try
             {
                 DbParameter ServiceQuotationIDP = provider.CreateParameter("ServiceQuotationID", ServiceQuotationID, DbType.Int64);
                 DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                 //  DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int64);
                 DbParameter QuotationNumberP;
                 if (!string.IsNullOrEmpty(QuotationNumber))
                     QuotationNumberP = provider.CreateParameter("QuotationNumber", QuotationNumber, DbType.String);
                 else
                     QuotationNumberP = provider.CreateParameter("QuotationNumber", null, DbType.String);

                 DbParameter QuotationDateFP = provider.CreateParameter("QuotationDateF", QuotationDateF, DbType.DateTime);
                 DbParameter QuotationDateTP = provider.CreateParameter("QuotationDateT", QuotationDateT, DbType.DateTime);
                 DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                 DbParameter CustomerCodeP;
                 if (!string.IsNullOrEmpty(CustomerCode))
                     CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                 else
                     CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);

                 DbParameter[] Params = new DbParameter[7] { ServiceQuotationIDP, ICTicketIDP, QuotationNumberP, QuotationDateFP, QuotationDateTP, DealerIDP, CustomerCodeP };

                 PDMS_PaidServiceInvoice Service = null;
                 long InvoiceID = 0;
                 using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceQuotation", Params))
                 {
                     if (DataSet != null)
                     {
                         foreach (DataRow dr in DataSet.Tables[0].Rows)
                         {
                             if (InvoiceID != Convert.ToInt64(dr["ServiceQuotationID"]))
                             {
                                 Service = new PDMS_PaidServiceInvoice();
                                 Services.Add(Service);
                                 Service.PaidServiceInvoiceID = Convert.ToInt64(dr["ServiceQuotationID"]);
                                 Service.InvoiceNumber = Convert.ToString(dr["QuotationNumber"]);
                                 Service.InvoiceDate = Convert.ToDateTime(dr["QuotationDate"]);
                                 Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                 Service.Through = Convert.ToString(dr["Through"]);
                                 Service.LRNumber = Convert.ToString(dr["LRNumber"]);

                                 Service.ICTicket = new PDMS_ICTicket();
                                 Service.ICTicket.ICTicketID = Convert.ToInt32(dr["ICTicketID"]);
                                 Service.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                 Service.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                                 Service.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                                 Service.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                                 Service.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);// Convert.ToString(dr["FSRDate"]);

                                 Service.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                 Service.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

                                 Service.ICTicket.Dealer = new PDMS_Dealer();
                                 Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                 Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                 Service.ICTicket.Dealer.DealerBank = new PDMS_DealerBankDetails();
                                 Service.ICTicket.Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                                 Service.ICTicket.Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                                 Service.ICTicket.Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                                 Service.ICTicket.Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]);

                                 Service.ICTicket.Customer = new PDMS_Customer();
                                 Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                 Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                                 Service.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                                 Service.ICTicket.Remarks = Convert.ToString(dr["Remarks"]);
                                 Service.ICTicket.KindAttn = Convert.ToString(dr["KindAttn"]);
                                 Service.ICTicket.NoOfDays = Convert.ToDecimal(dr["WorkedDay"]);
                                 InvoiceID = Service.PaidServiceInvoiceID;
                                 Service.IsDeletionAllowed = Convert.ToBoolean(dr["IsDeletionAllowed"]);
                                 Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                             }
                             Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                             {
                                 Material = new PDMS_Material()
                                 {
                                     MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                     MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                     HSN = Convert.ToString(dr["HSNCode"])
                                 },
                                 Qty = Convert.ToInt32(dr["Qty"]),
                                 Rate = Convert.ToDecimal(dr["Rate"]),
                                 Discount = Convert.ToDecimal(dr["Discount"]),
                                 TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                 CGST = Convert.ToInt32(dr["CGST"]),
                                 SGST = Convert.ToInt32(dr["SGST"]),
                                 IGST = Convert.ToInt32(dr["IGST"]),
                                 CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                 SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                 IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),

                             });
                         }
                     }
                 }
             }
             catch (SqlException sqlEx)
             { }
             catch (Exception ex)
             { }
             return Services;
         }
         public PAttachedFile OverhaulServiceQuotationfile(long ServiceQuotationID)
         {
             try
             {
                 PDMS_PaidServiceInvoice PaidServiceInvoice = GetOverhaulServiceQuotation(ServiceQuotationID, null, "", null, null, null, "")[0];
                 PDMS_Customer Dealer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                 string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                 string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.StateN.State) ? "" : "," + Dealer.StateN.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                 PDMS_Customer Customer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                 string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                 string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.StateN.State) ? "" : "," + Customer.StateN.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

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
                 //  decimal GrandTotal = 0;
                 string StateCode = Dealer.StateCode;
                 string GST_Header = "";
                 int i = 0;
                 foreach (PDMS_PaidServiceInvoiceItem item in PaidServiceInvoice.InvoiceItems)
                 {

                     i = i + 1;
                     if (item.SGST != 0)
                     {
                         GST_Header = "CGST & SGST";
                         CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);
                     }
                     else
                     {
                         GST_Header = "IGST";
                         CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);
                     }
                 }

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

                 ReportParameter[] P = new ReportParameter[26];
                 //   ViewState["Month"] = ddlMonth.SelectedValue;
                 P[0] = new ReportParameter("DealerCode", PaidServiceInvoice.ICTicket.Dealer.DealerCode, false);
                 P[1] = new ReportParameter("DealerName", PaidServiceInvoice.ICTicket.Dealer.DealerName, false);
                 P[2] = new ReportParameter("Address1", DealerAddress1, false);
                 P[3] = new ReportParameter("Address2", DealerAddress2, false);
                 P[4] = new ReportParameter("Contact", "Contact", false);
                 P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                 P[6] = new ReportParameter("GST_Header", GST_Header, false);
                 P[7] = new ReportParameter("GrandTotal", (PaidServiceInvoice.GrandTotal).ToString(), false);
                 P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(PaidServiceInvoice.GrandTotal)), false);
                 P[9] = new ReportParameter("InvoiceNumber", PaidServiceInvoice.InvoiceNumber, false);

                 P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                 P[11] = new ReportParameter("CustomerName", PaidServiceInvoice.ICTicket.Customer.CustomerName, false);
                 P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                 P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                 P[14] = new ReportParameter("CustomerMail", Customer.EMAIL, false);
                 P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                 P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                 P[17] = new ReportParameter("ICTicketNo", PaidServiceInvoice.ICTicket.ICTicketNumber, false);
                 P[18] = new ReportParameter("KindAttn", PaidServiceInvoice.ICTicket.KindAttn, false);
                 P[19] = new ReportParameter("YourRef", PaidServiceInvoice.ICTicket.FSRNumber + " " + PaidServiceInvoice.ICTicket.FSRDate, false);
                 P[20] = new ReportParameter("OurRef", PaidServiceInvoice.ICTicket.ICTicketNumber + " " + PaidServiceInvoice.ICTicket.Equipment.EquipmentSerialNo, false);
                 P[21] = new ReportParameter("Remarks", PaidServiceInvoice.ICTicket.Remarks, false);
                 P[22] = new ReportParameter("NoOfDays", Convert.ToString(PaidServiceInvoice.ICTicket.NoOfDays), false);
                 P[23] = new ReportParameter("ScopOfWork", PaidServiceInvoice.ICTicket.ScopeOfWork, false);
                 P[24] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.AcNumber + ", Bank : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.BankName + ", Branch : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.Branch + ", IFSC Code : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.IfscCode, false);
                 P[25] = new ReportParameter("InvDate", PaidServiceInvoice.InvoiceDate.ToShortDateString(), false);

                 report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceQuotation.rdlc");
                 ReportDataSource rds = new ReportDataSource();
                 rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                 rds.Value = CommissionDT;
                 report.DataSources.Add(rds);
                 report.SetParameters(P);
                 Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                 PAttachedFile InvF = new PAttachedFile();

                 InvF.FileType = mimeType;
                 InvF.AttachedFile = mybytes;
                 InvF.AttachedFileID = 0;
                 InvF.FileName = "Quotation " + PaidServiceInvoice.InvoiceNumber;
                 return InvF;
             }
             catch (Exception ex)
             {

             }
             return null;
         }
         public Boolean CancelOverhaulServiceQuotationOrProformaOrInvoice(long ServiceID, int CreatedBy, int Type)
         {

             int success = 0;

             DbParameter ServiceIDP = provider.CreateParameter("ServiceID", ServiceID, DbType.Int64);
             DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
             DbParameter TypeP = provider.CreateParameter("Type", Type, DbType.Int32);

             DbParameter[] Params = new DbParameter[3] { ServiceIDP, CreatedByP, TypeP };
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                 {
                     success = provider.Insert("ZDMS_CancelServiceQuotationOrProformaOrInvoice", Params);
                     scope.Complete();
                 }
                 //  new BDMS_Service().insertServiceInvoiceFile(ServiceInvoiceHeaderID, ServiceInvoicefile(ServiceInvoiceHeaderID));
             }
             catch (SqlException sqlEx)
             {
                 new FileLogger().LogMessage("BDMS_Service", "CancelServiceQuotationOrProformaOrInvoice", sqlEx);
                 return false;
             }
             catch (Exception ex)
             {
                 new FileLogger().LogMessage("BDMS_Service", " CancelServiceQuotationOrProformaOrInvoice", ex);
                 return false;
             }
             return true;
         }

         public List<PDMS_PaidServiceInvoice> GetOverhaulServiceProformaInvoice(long? ServiceInvoiceID, long? ICTicketID, string Proforma, DateTime? ProformaDateF, DateTime? ProformaDateT, int? DealerID, string CustomerCode)
         {
             List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
             try
             {
                 DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
                 DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                 DbParameter ProformaP;
                 if (!string.IsNullOrEmpty(Proforma))
                     ProformaP = provider.CreateParameter("Proforma", Proforma, DbType.String);
                 else
                     ProformaP = provider.CreateParameter("Proforma", null, DbType.String);

                 DbParameter QuotationDateFP = provider.CreateParameter("ProformaDateF", ProformaDateF, DbType.DateTime);
                 DbParameter QuotationDateTP = provider.CreateParameter("ProformaDateT", ProformaDateT, DbType.DateTime);
                 DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                 DbParameter CustomerCodeP;
                 if (!string.IsNullOrEmpty(CustomerCode))
                     CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                 else
                     CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);

                 DbParameter[] Params = new DbParameter[7] { ServiceInvoiceIDP, ICTicketIDP, ProformaP, QuotationDateFP, QuotationDateTP, DealerIDP, CustomerCodeP };

                 PDMS_PaidServiceInvoice Service = null;
                 long InvoiceID = 0;
                 using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceProformaInvoice", Params))
                 {
                     if (DataSet != null)
                     {
                         foreach (DataRow dr in DataSet.Tables[0].Rows)
                         {
                             if (InvoiceID != Convert.ToInt64(dr["ServiceInvoiceID"]))
                             {
                                 Service = new PDMS_PaidServiceInvoice();
                                 Services.Add(Service);
                                 Service.PaidServiceInvoiceID = Convert.ToInt64(dr["ServiceInvoiceID"]);
                                 Service.ProformaInvoiceNumber = Convert.ToString(dr["ProformaInvoiceNumber"]);
                                 Service.ProformaInvoiceDate = Convert.ToDateTime(dr["ProformaInvoiceDate"]);
                                 Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                 Service.Through = Convert.ToString(dr["Through"]);
                                 Service.LRNumber = Convert.ToString(dr["LRNumber"]);



                                 Service.ICTicket = new PDMS_ICTicket();
                                 Service.ICTicket.ICTicketID = Convert.ToInt32(dr["ICTicketID"]);
                                 Service.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                 Service.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                                 Service.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                                 Service.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                                 Service.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);//Convert.ToString(dr["FSRDate"]);

                                 Service.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                 Service.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

                                 Service.ICTicket.Dealer = new PDMS_Dealer();
                                 Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                 Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                 Service.ICTicket.Dealer.DealerBank = new PDMS_DealerBankDetails();
                                 Service.ICTicket.Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                                 Service.ICTicket.Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                                 Service.ICTicket.Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                                 Service.ICTicket.Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]);

                                 Service.ICTicket.Customer = new PDMS_Customer();
                                 Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                 Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                                 Service.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                                 Service.ICTicket.Remarks = Convert.ToString(dr["Remarks"]);
                                 Service.ICTicket.KindAttn = Convert.ToString(dr["KindAttn"]);
                                 Service.ICTicket.NoOfDays = Convert.ToDecimal(dr["WorkedDay"]);
                                 InvoiceID = Service.PaidServiceInvoiceID;
                                 Service.IsDeletionAllowed = Convert.ToBoolean(dr["IsDeletionAllowedProforma"]);
                                 Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                             }
                             Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                             {
                                 Material = new PDMS_Material()
                                 {
                                     MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                     MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                     HSN = Convert.ToString(dr["HSNCode"])
                                 },
                                 Qty = Convert.ToInt32(dr["Qty"]),
                                 Rate = Convert.ToDecimal(dr["Rate"]),
                                 Discount = Convert.ToDecimal(dr["Discount"]),
                                 TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                 CGST = Convert.ToInt32(dr["CGST"]),
                                 SGST = Convert.ToInt32(dr["SGST"]),
                                 IGST = Convert.ToInt32(dr["IGST"]),
                                 CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                 SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                 IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),

                             });
                         }
                     }
                 }
             }
             catch (SqlException sqlEx)
             { }
             catch (Exception ex)
             { }
             return Services;
         }
         public PAttachedFile OverhaulServiceProformaInvoicefile(long ServiceQuotationID)
         {
             try
             {
                 PDMS_PaidServiceInvoice PaidServiceInvoice = new BDMS_Service().GetPaidServiceProformaInvoice(ServiceQuotationID, null, "", null, null, null, "")[0];
                 PDMS_Customer Dealer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                 string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                 string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.StateN.State) ? "" : "," + Dealer.StateN.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');


                 PDMS_Customer Customer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                 string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                 string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.StateN.State) ? "" : "," + Customer.StateN.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

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
                 //  decimal GrandTotal = 0;
                 string StateCode = Dealer.StateCode;
                 string GST_Header = "";
                 int i = 0;
                 foreach (PDMS_PaidServiceInvoiceItem item in PaidServiceInvoice.InvoiceItems)
                 {

                     i = i + 1;
                     if (item.SGST != 0)
                     {
                         GST_Header = "CGST & SGST";
                         CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);
                     }
                     else
                     {
                         GST_Header = "IGST";
                         CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);
                     }
                 }

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

                 ReportParameter[] P = new ReportParameter[26];
                 //   ViewState["Month"] = ddlMonth.SelectedValue;
                 P[0] = new ReportParameter("DealerCode", PaidServiceInvoice.ICTicket.Dealer.DealerCode, false);
                 P[1] = new ReportParameter("DealerName", PaidServiceInvoice.ICTicket.Dealer.DealerName, false);
                 P[2] = new ReportParameter("Address1", DealerAddress1, false);
                 P[3] = new ReportParameter("Address2", DealerAddress2, false);
                 P[4] = new ReportParameter("Contact", "Contact", false);
                 P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                 P[6] = new ReportParameter("GST_Header", GST_Header, false);
                 P[7] = new ReportParameter("GrandTotal", (PaidServiceInvoice.GrandTotal).ToString(), false);
                 P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(PaidServiceInvoice.GrandTotal)), false);
                 P[9] = new ReportParameter("InvoiceNumber", PaidServiceInvoice.ProformaInvoiceNumber, false);

                 P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                 P[11] = new ReportParameter("CustomerName", PaidServiceInvoice.ICTicket.Customer.CustomerName, false);
                 P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                 P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                 P[14] = new ReportParameter("CustomerMail", Customer.EMAIL, false);
                 P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                 P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                 P[17] = new ReportParameter("ICTicketNo", PaidServiceInvoice.ICTicket.ICTicketNumber, false);
                 P[18] = new ReportParameter("KindAttn", PaidServiceInvoice.ICTicket.KindAttn, false);
                 P[19] = new ReportParameter("YourRef", PaidServiceInvoice.ICTicket.FSRNumber + " " + PaidServiceInvoice.ICTicket.FSRDate, false);
                 P[20] = new ReportParameter("OurRef", PaidServiceInvoice.ICTicket.ICTicketNumber + " " + PaidServiceInvoice.ICTicket.Equipment.EquipmentSerialNo, false);
                 P[21] = new ReportParameter("Remarks", PaidServiceInvoice.ICTicket.Remarks, false);
                 P[22] = new ReportParameter("NoOfDays", Convert.ToString(PaidServiceInvoice.ICTicket.NoOfDays), false);
                 P[23] = new ReportParameter("ScopOfWork", PaidServiceInvoice.ICTicket.ScopeOfWork, false);
                 P[24] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.AcNumber + ", Bank : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.BankName + ", Branch : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.Branch + ", IFSC Code : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.IfscCode, false);
                 P[25] = new ReportParameter("InvDate", PaidServiceInvoice.ProformaInvoiceDate.ToShortDateString(), false);

                 report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceProformaInvoice.rdlc");
                 ReportDataSource rds = new ReportDataSource();
                 rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                 rds.Value = CommissionDT;
                 report.DataSources.Add(rds);
                 report.SetParameters(P);
                 Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                 PAttachedFile InvF = new PAttachedFile();

                 InvF.FileType = mimeType;
                 InvF.AttachedFile = mybytes;
                 InvF.AttachedFileID = 0;
                 InvF.FileName = "Proforma Invoice " + PaidServiceInvoice.ProformaInvoiceNumber;
                 return InvF;
             }
             catch (Exception ex)
             {

             }
             return null;
         }



         public List<PDMS_PaidServiceInvoice> GetOverhaulServiceInvoice(long? ServiceInvoiceID, long? ICTicketID, string InvoiceNumber, DateTime? InvoiceDateF, DateTime? InvoiceDateT, int? DealerID, string CustomerCode)
         {
             List<PDMS_PaidServiceInvoice> Services = new List<PDMS_PaidServiceInvoice>();
             try
             {
                 DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
                 DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
                 DbParameter InvoiceNumberP;
                 if (!string.IsNullOrEmpty(InvoiceNumber))
                     InvoiceNumberP = provider.CreateParameter("InvoiceNumber", InvoiceNumber, DbType.String);
                 else
                     InvoiceNumberP = provider.CreateParameter("InvoiceNumber", null, DbType.String);

                 DbParameter InvoiceDateFP = provider.CreateParameter("InvoiceDateF", InvoiceDateF, DbType.DateTime);
                 DbParameter InvoiceDateTP = provider.CreateParameter("InvoiceDateT", InvoiceDateT, DbType.DateTime);
                 DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

                 DbParameter CustomerCodeP;
                 if (!string.IsNullOrEmpty(CustomerCode))
                     CustomerCodeP = provider.CreateParameter("CustomerCode", CustomerCode, DbType.String);
                 else
                     CustomerCodeP = provider.CreateParameter("CustomerCode", null, DbType.String);

                 DbParameter[] Params = new DbParameter[7] { ServiceInvoiceIDP, ICTicketIDP, InvoiceNumberP, InvoiceDateFP, InvoiceDateTP, DealerIDP, CustomerCodeP };

                 PDMS_PaidServiceInvoice Service = null;
                 long InvoiceID = 0;
                 using (DataSet DataSet = provider.Select("ZDMS_GetPaidServiceInvoice", Params))
                 {
                     if (DataSet != null)
                     {
                         foreach (DataRow dr in DataSet.Tables[0].Rows)
                         {
                             if (InvoiceID != Convert.ToInt64(dr["ServiceInvoiceID"]))
                             {
                                 Service = new PDMS_PaidServiceInvoice();
                                 Services.Add(Service);
                                 Service.PaidServiceInvoiceID = Convert.ToInt64(dr["ServiceInvoiceID"]);
                                 Service.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                                 Service.InvoiceDate = Convert.ToDateTime(dr["InvoiceDate"]);
                                 Service.GrandTotal = Convert.ToInt32(dr["GrandTotal"]);
                                 Service.Through = Convert.ToString(dr["Through"]);
                                 Service.LRNumber = Convert.ToString(dr["LRNumber"]);
                                 // Service.NoOfDays = Convert.ToDecimal(dr["NoOfDays"]);
                                 //Service.ScopOfWork = Convert.ToString(dr["ScopOfWork"]);
                                 //Service.Remarks = Convert.ToString(dr["Remarks"]);

                                 Service.ICTicket = new PDMS_ICTicket();
                                 Service.ICTicket.ICTicketID = Convert.ToInt32(dr["ICTicketID"]);
                                 Service.ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                                 Service.ICTicket.ICTicketDate = Convert.ToDateTime(dr["ICTicketDate"]);
                                 Service.ICTicket.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                                 Service.ICTicket.FSRNumber = Convert.ToString(dr["FSRNumber"]);
                                 Service.ICTicket.FSRDate = dr["FSRDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["FSRDate"]);//Convert.ToString(dr["FSRDate"]);

                                 Service.ICTicket.Equipment = new PDMS_EquipmentHeader();
                                 Service.ICTicket.Equipment.EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]);

                                 Service.ICTicket.Dealer = new PDMS_Dealer();
                                 Service.ICTicket.Dealer.DealerCode = Convert.ToString(dr["DealerCode"]);
                                 Service.ICTicket.Dealer.DealerName = Convert.ToString(dr["ContactName"]);
                                 Service.ICTicket.Dealer.DealerBank = new PDMS_DealerBankDetails();
                                 Service.ICTicket.Dealer.DealerBank.BankName = Convert.ToString(dr["BankName"]);
                                 Service.ICTicket.Dealer.DealerBank.Branch = Convert.ToString(dr["Branch"]);
                                 Service.ICTicket.Dealer.DealerBank.AcNumber = Convert.ToString(dr["AcNumber"]);
                                 Service.ICTicket.Dealer.DealerBank.IfscCode = Convert.ToString(dr["IfscCode"]);

                                 Service.ICTicket.Customer = new PDMS_Customer();
                                 Service.ICTicket.Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]);
                                 Service.ICTicket.Customer.CustomerName = Convert.ToString(dr["CustomerName"]);
                                 Service.ICTicket.ScopeOfWork = Convert.ToString(dr["ScopeOfWork"]);
                                 Service.ICTicket.NoOfDays = Convert.ToDecimal(dr["WorkedDay"]);
                                 Service.ICTicket.Remarks = Convert.ToString(dr["Remarks"]);
                                 Service.ICTicket.KindAttn = Convert.ToString(dr["KindAttn"]);

                                 InvoiceID = Service.PaidServiceInvoiceID;
                                 Service.InvoiceItems = new List<PDMS_PaidServiceInvoiceItem>();
                             }
                             Service.InvoiceItems.Add(new PDMS_PaidServiceInvoiceItem()
                             {
                                 Material = new PDMS_Material()
                                 {
                                     MaterialCode = Convert.ToString(dr["MaterialCode"]),
                                     MaterialDescription = Convert.ToString(dr["MaterialDescription"]),
                                     HSN = Convert.ToString(dr["HSNCode"])
                                 },
                                 Qty = Convert.ToInt32(dr["Qty"]),
                                 Rate = Convert.ToDecimal(dr["Rate"]),
                                 Discount = Convert.ToDecimal(dr["Discount"]),
                                 TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                                 CGST = Convert.ToInt32(dr["CGST"]),
                                 SGST = Convert.ToInt32(dr["SGST"]),
                                 IGST = Convert.ToInt32(dr["IGST"]),
                                 CGSTValue = Convert.ToDecimal(dr["CGSTValue"]),
                                 SGSTValue = Convert.ToDecimal(dr["SGSTValue"]),
                                 IGSTValue = Convert.ToDecimal(dr["IGSTValue"]),
                                 CessValue = dr["CessValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CessValue"])
                             });
                         }
                     }
                 }
             }
             catch (SqlException sqlEx)
             { }
             catch (Exception ex)
             { }
             return Services;
         }
         private PAttachedFile OverhaulServiceInvoicefile(long ServiceInvoiceHeaderID)
         {
             try
             {
                 PDMS_PaidServiceInvoice PaidServiceInvoice = GetOverhaulServiceInvoice(ServiceInvoiceHeaderID, null, "", null, null, null, "")[0];
                 PDMS_Customer Dealer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Dealer.DealerCode);
                 string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                 string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.StateN.State) ? "" : "," + Dealer.StateN.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                 PDMS_Customer Customer = new SCustomer().getCustomerAddress(PaidServiceInvoice.ICTicket.Customer.CustomerCode);
                 string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                 string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.StateN.State) ? "" : "," + Customer.StateN.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                 if (string.IsNullOrEmpty(Customer.GSTIN))
                 {
                     DataTable dt = new NpgsqlServer().ExecuteReader("select r_value as GSTIN from doohr_bp_statutory where r_statutory_type='GSTIN' and s_tenant_id=" + PaidServiceInvoice.ICTicket.Dealer.DealerCode + " and p_bp_id='" + PaidServiceInvoice.ICTicket.Customer.CustomerCode + "' limit 1");
                     if (dt.Rows.Count == 1)
                     {
                         Customer.GSTIN = Convert.ToString(dt.Rows[0][0]);
                     }
                 }

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
                 //  decimal GrandTotal = 0;
                 string StateCode = Dealer.StateCode;
                 string GST_Header = "";
                 int i = 0;
                 decimal CessValue = 0;

                 decimal CessSubTotal = 0;
                 foreach (PDMS_PaidServiceInvoiceItem item in PaidServiceInvoice.InvoiceItems)
                 {
                     i = i + 1;
                     if (item.SGST != 0)
                     {
                         GST_Header = "CGST & SGST";
                         CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.CGST, item.SGST, item.CGSTValue, item.SGSTValue, item.TaxableValue + item.CGSTValue + item.SGSTValue);

                         CessValue = CessValue + item.CessValue;
                         CessSubTotal = item.TaxableValue + item.CGSTValue + item.SGSTValue + item.CessValue;
                     }
                     else
                     {
                         GST_Header = "IGST";
                         CommissionDT.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Qty, item.Rate, item.TaxableValue, item.IGST, null, item.IGSTValue, null, item.TaxableValue + item.IGSTValue);

                         CessValue = CessValue + item.CessValue;
                         CessSubTotal = item.TaxableValue + item.IGSTValue + item.CessValue;

                     }
                 }

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

                 ReportParameter[] P = new ReportParameter[28];
                 //   ViewState["Month"] = ddlMonth.SelectedValue;
                 P[0] = new ReportParameter("DealerCode", PaidServiceInvoice.ICTicket.Dealer.DealerCode, false);
                 P[1] = new ReportParameter("DealerName", PaidServiceInvoice.ICTicket.Dealer.DealerName, false);
                 P[2] = new ReportParameter("Address1", DealerAddress1, false);
                 P[3] = new ReportParameter("Address2", DealerAddress2, false);
                 P[4] = new ReportParameter("Contact", "Contact", false);
                 P[5] = new ReportParameter("GSTIN", Dealer.GSTIN, false);
                 P[6] = new ReportParameter("GST_Header", GST_Header, false);
                 P[7] = new ReportParameter("GrandTotal", (PaidServiceInvoice.GrandTotal).ToString(), false);
                 P[8] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(PaidServiceInvoice.GrandTotal)), false);
                 P[9] = new ReportParameter("InvoiceNumber", PaidServiceInvoice.InvoiceNumber, false);

                 P[10] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                 P[11] = new ReportParameter("CustomerName", PaidServiceInvoice.ICTicket.Customer.CustomerName, false);
                 P[12] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                 P[13] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                 P[14] = new ReportParameter("CustomerMail", Customer.EMAIL, false);
                 P[15] = new ReportParameter("CustomerStateCode", Customer.GSTIN.Length > 2 ? Customer.GSTIN.Substring(0, 2) : "", false);
                 P[16] = new ReportParameter("CustomerGST", Customer.GSTIN, false);
                 P[17] = new ReportParameter("ICTicketNo", PaidServiceInvoice.ICTicket.ICTicketNumber, false);
                 P[18] = new ReportParameter("KindAttn", PaidServiceInvoice.ICTicket.KindAttn, false);
                 P[19] = new ReportParameter("YourRef", PaidServiceInvoice.ICTicket.FSRNumber + " " + PaidServiceInvoice.ICTicket.FSRDate, false);
                 P[20] = new ReportParameter("OurRef", PaidServiceInvoice.ICTicket.ICTicketNumber + " " + PaidServiceInvoice.ICTicket.Equipment.EquipmentSerialNo, false);
                 P[21] = new ReportParameter("Remarks", PaidServiceInvoice.ICTicket.Remarks, false);
                 P[22] = new ReportParameter("NoOfDays", Convert.ToString(PaidServiceInvoice.ICTicket.NoOfDays), false);
                 P[23] = new ReportParameter("ScopOfWork", PaidServiceInvoice.ICTicket.ScopeOfWork, false);
                 P[24] = new ReportParameter("BankDetails", "Our Bank details are : A/C No " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.AcNumber + ", Bank : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.BankName + ", Branch : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.Branch + ", IFSC Code : " + PaidServiceInvoice.ICTicket.Dealer.DealerBank.IfscCode, false);
                 P[25] = new ReportParameter("InvDate", PaidServiceInvoice.InvoiceDate.ToShortDateString(), false);
                 P[26] = new ReportParameter("CessValue", Convert.ToString(CessValue), false);
                 P[27] = new ReportParameter("CessSubTotal", Convert.ToString(CessSubTotal), false);
                 report.ReportPath = HttpContext.Current.Server.MapPath("~/Print/DMS_PaidServiceInvoice.rdlc");
                 ReportDataSource rds = new ReportDataSource();
                 rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
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

             }
             return null;
         }
         public void insertOverhaulServiceInvoiceFile(long ServiceInvoiceID, PAttachedFile InvFile)
         {
             DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.String);
             DbParameter InvFileP = provider.CreateParameter("InvFile", InvFile.AttachedFile, DbType.Binary);
             DbParameter FileTypeP = provider.CreateParameter("FileType", InvFile.FileType, DbType.String);
             DbParameter[] Params = new DbParameter[3] { ServiceInvoiceIDP, InvFileP, FileTypeP };
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                 {
                     provider.Insert("ZDMS_InsertServiceInvoiceFile", Params);
                     scope.Complete();
                 }
             }
             catch (SqlException sqlEx)
             {
                 new FileLogger().LogMessage("BDMS_Service", "insertServiceInvoiceFile", sqlEx);
             }
             catch (Exception ex)
             {
                 new FileLogger().LogMessage("BDMS_Service", " insertServiceInvoiceFile", ex);
             }
         }
         public PAttachedFile GetOverhaulServiceInvoiceFile(long ServiceInvoiceID)
         {
             DbParameter ServiceInvoiceIDP = provider.CreateParameter("ServiceInvoiceID", ServiceInvoiceID, DbType.Int64);
             PAttachedFile Files = null;
             try
             {
                 DbParameter[] Params = new DbParameter[1] { ServiceInvoiceIDP };

                 using (DataSet DS = provider.Select("ZDMS_GetServiceInvoiceFile", Params))
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
                     new BDMS_Service().insertServiceInvoiceFile(ServiceInvoiceID, OverhaulServiceInvoicefile(ServiceInvoiceID));
                 }
                 return Files;
             }
             catch (Exception ex)
             {
                 new FileLogger().LogMessage("BDMS_OverhaulService", "GetOverhaulServiceInvoiceFile", ex);
                 return null;
             }

         }

         public Boolean InsertOverhaulServiceQuotationOrProformaOrInvoice(long ICTicketID, Boolean IsIGST, int CreatedBy, int Type)
         {

             int success = 0;

             DbParameter ICTicketIDP = provider.CreateParameter("ICTicketID", ICTicketID, DbType.Int64);
             DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
             DbParameter TypeP = provider.CreateParameter("Type", Type, DbType.Int32);

             DbParameter[] Params = new DbParameter[3] { ICTicketIDP, CreatedByP, TypeP };
             try
             {
                 using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                 {
                     success = provider.Insert("ZDMS_InsertServiceQuotationOrProformaOrInvoice", Params);
                     scope.Complete();
                 }
             }
             catch (SqlException sqlEx)
             {
                 new FileLogger().LogMessage("BDMS_Service", "InsertServiceQuotationOrProformaOrInvoice", sqlEx);
                 return false;
             }
             catch (Exception ex)
             {
                 new FileLogger().LogMessage("BDMS_Service", " InsertServiceQuotationOrProformaOrInvoice", ex);
                 return false;
             }
             return true;
         }

        
  }
}
