using DataAccess;
using Properties;  
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO; 
using System.Transactions;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_BankDepositClearing
    {
        private IDataAccess provider;
        public BDMS_BankDepositClearing() { provider = new ProviderFactory().GetProvider(); }
        public long InsertOrUpdateBankDepositClearing(PDMS_BankDepositClearing Bank)
        {
            int success = 0;
            long ID = 0;
            DbParameter BankDepositClearingID = provider.CreateParameter("BankDepositClearingID", Bank.BankDepositClearingID, DbType.Int64);
            DbParameter DealerID = provider.CreateParameter("DealerID", Bank.Dealer.DealerID, DbType.Int32);
            DbParameter BankAccount = provider.CreateParameter("BankAccount", Bank.BankAccount, DbType.String);
            DbParameter TransactionDate = provider.CreateParameter("TransactionDate", Bank.TransactionDate, DbType.DateTime);
            DbParameter ValueDate = provider.CreateParameter("ValueDate", Bank.ValueDate, DbType.DateTime);
            DbParameter BankDescription = provider.CreateParameter("BankDescription", Bank.BankDescription, DbType.String);
            DbParameter BranchCode = provider.CreateParameter("BranchCode", Bank.BranchCode, DbType.String);
            DbParameter Amount = provider.CreateParameter("Amount", Bank.Amount, DbType.Decimal);

            DbParameter IsMultipleCustomer = provider.CreateParameter("IsMultipleCustomer", Bank.IsMultipleCustomer, DbType.Boolean);
            DbParameter CustomerCode = provider.CreateParameter("CustomerCode", Bank.Customer == null ? null : Bank.Customer.CustomerCode, DbType.String);
            DbParameter DepositFor = provider.CreateParameter("DepositFor", Bank.DepositFor, DbType.String);
            DbParameter InvoiceNumber = provider.CreateParameter("InvoiceNumber", Bank.InvoiceNumber, DbType.String);
            DbParameter PONumber = provider.CreateParameter("PONumber", Bank.PONumber, DbType.String);
            DbParameter SONumber = provider.CreateParameter("SONumber", Bank.SONumber, DbType.String);
            DbParameter MachineModel = provider.CreateParameter("MachineModel", Bank.MachineModel, DbType.String);
            DbParameter Department = provider.CreateParameter("Department", Bank.Department, DbType.String);
            DbParameter Place = provider.CreateParameter("Place", Bank.Place, DbType.String);
            DbParameter StateID = provider.CreateParameter("StateID", Bank.State == null ? (int?)null : Bank.State.StateID, DbType.Int32);
            DbParameter RegionID = provider.CreateParameter("RegionID", Bank.Region == null ? (int?)null : Bank.Region.RegionID, DbType.Int32);

            DbParameter BillDetailGivenBy = provider.CreateParameter("BillDetailGivenBy", Bank.BillDetailGivenBy, DbType.String);
            DbParameter BillDetailUpdatedOn = provider.CreateParameter("BillDetailUpdatedOn", Bank.BillDetailUpdatedOn, DbType.DateTime);
            DbParameter Remarks = provider.CreateParameter("Remarks", Bank.Remarks, DbType.String);

            DbParameter ReferenceNo = provider.CreateParameter("ReferenceNo", Bank.ReferenceNo, DbType.String);
            DbParameter HeaderText = provider.CreateParameter("HeaderText", Bank.HeaderText, DbType.String);
            DbParameter Assignment = provider.CreateParameter("Assignment", Bank.Assignment, DbType.String);

            DbParameter RemitterAccount = provider.CreateParameter("RemitterAccount", Bank.RemitterAccount, DbType.String);
            DbParameter RemitterName = provider.CreateParameter("RemitterName", Bank.RemitterName, DbType.String);
            DbParameter RemitterEmail = provider.CreateParameter("RemitterEmail", Bank.RemitterEmail, DbType.String);
            DbParameter RemitterMobile = provider.CreateParameter("RemitterMobile", Bank.RemitterMobile, DbType.String);
            DbParameter RemitterBank = provider.CreateParameter("RemitterBank", Bank.RemitterBank, DbType.String);
            DbParameter RemitterIFSC = provider.CreateParameter("RemitterIFSC", Bank.RemitterIFSC, DbType.String);



            DbParameter CreatedBy = provider.CreateParameter("UserID", Bank.CreatedBy.UserID, DbType.Int32);

            DbParameter WarrantyClaimHeader = provider.CreateParameter("OutValue", ID, DbType.Int64, Convert.ToInt32(ParameterDirection.Output));
            DbParameter[] Params = new DbParameter[33] { BankDepositClearingID,DealerID, BankAccount, TransactionDate,ValueDate,BankDescription ,BranchCode,Amount,IsMultipleCustomer,CustomerCode,DepositFor,
            InvoiceNumber,PONumber,SONumber,MachineModel,Department,Place,StateID,RegionID,BillDetailGivenBy,BillDetailUpdatedOn,Remarks,ReferenceNo,HeaderText,Assignment,
            RemitterAccount,RemitterName,RemitterEmail,RemitterMobile,RemitterBank,RemitterIFSC,CreatedBy,WarrantyClaimHeader};

            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    success = provider.Insert("ZDMS_InsertOrUpdateBankDepositClearing", Params);
                    if (success != 0)
                    {
                        ID = Convert.ToInt64(WarrantyClaimHeader.Value);
                    }
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_BankDepositClearing", "InsertOrUpdateBankDepositClearing", sqlEx);
                ID = 0;
                throw;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_BankDepositClearing", " InsertOrUpdateBankDepositClearing", ex);
                ID = 0;
                throw;
            }
            return ID;
        }
        public Boolean UpdateBankDepositClearing(PDMS_BankDepositClearing Bank)
        {
            DbParameter BankDepositClearingID = provider.CreateParameter("BankDepositClearingID", Bank.BankDepositClearingID, DbType.Int64);
            DbParameter AccountedBy = provider.CreateParameter("AccountedBy", Bank.AccountedBy.UserID, DbType.Int32);
            DbParameter AccountedOn = provider.CreateParameter("AccountedOn", Bank.AccountedOn, DbType.DateTime);
            DbParameter SapAccountNo = provider.CreateParameter("SapAccountNo", Bank.SapAccountNo, DbType.String);
            DbParameter SapPostedOn = provider.CreateParameter("SapPostedOn", Bank.SapPostedOn, DbType.DateTime);
            DbParameter SapClearedOn = provider.CreateParameter("SapClearedOn", Bank.SapClearedOn, DbType.DateTime);
            DbParameter[] Params = new DbParameter[6] { BankDepositClearingID, AccountedBy, AccountedOn, SapAccountNo, SapPostedOn, SapClearedOn };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateBankDepositClearing", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_BankDepositClearing", "UpdateBankDepositClearing", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_BankDepositClearing", " UpdateBankDepositClearing", ex);
                return false;
            }
            return true;
        }
        public Boolean UpdateBankDepositClearingStatus(long BankDepositClearingID, int StatusID, int UserID)
        {
            DbParameter BankDepositClearingIDP = provider.CreateParameter("BankDepositClearingID", BankDepositClearingID, DbType.Int64);
            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
            DbParameter UserIDP = provider.CreateParameter("UserID", UserID, DbType.Int32);
            DbParameter[] Params = new DbParameter[3] { BankDepositClearingIDP, StatusIDP, UserIDP };
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    provider.Insert("ZDMS_UpdateBankDepositClearingStatus", Params);
                    scope.Complete();
                }
            }
            catch (SqlException sqlEx)
            {
                new FileLogger().LogMessage("BDMS_BankDepositClearing", "UpdateBankDepositClearingStatus", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_BankDepositClearing", " UpdateBankDepositClearingStatus", ex);
                return false;
            }
            return true;
        }
        public List<PDMS_BankDepositClearing> GetBankDepositClearing(long? BankDepositClearingID, int? DealerID, string CustomerCode, DateTime? TransactionDateF, DateTime? TransactionDateT
           , int? CreatedBy, DateTime? CreatedOnF, DateTime? CreatedOnT, int? AccountedBy, DateTime? AccountedOnF, DateTime? AccountedOnT, int? StatusID, int? StateID, int? RegionID)
        {
            List<PDMS_BankDepositClearing> Ws = new List<PDMS_BankDepositClearing>();
            PDMS_BankDepositClearing W = null;
            DbParameter BankDepositClearingIDP = provider.CreateParameter("BankDepositClearingID", BankDepositClearingID, DbType.Int64);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);
            DbParameter CustomerCodeP = provider.CreateParameter("CustomerCode", string.IsNullOrEmpty(CustomerCode) ? null : CustomerCode, DbType.String);

            DbParameter TransactionDateFP = provider.CreateParameter("TransactionDateF", TransactionDateF, DbType.DateTime);
            DbParameter TransactionDateTP = provider.CreateParameter("TransactionDateT", TransactionDateT, DbType.DateTime);

            DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
            DbParameter CreatedOnFP = provider.CreateParameter("CreatedOnF", CreatedOnF, DbType.DateTime);
            DbParameter CreatedOnTP = provider.CreateParameter("CreatedOnT", CreatedOnT, DbType.DateTime);

            DbParameter AccountedByP = provider.CreateParameter("AccountedBy", AccountedBy, DbType.Int32);
            DbParameter AccountedOnFP = provider.CreateParameter("AccountedOnF", AccountedOnF, DbType.DateTime);
            DbParameter AccountedOnTP = provider.CreateParameter("AccountedOnT", AccountedOnT, DbType.DateTime);


            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
            DbParameter StateIDP = provider.CreateParameter("StateID", StateID, DbType.Int32);
            DbParameter RegionIDP = provider.CreateParameter("RegionID", RegionID, DbType.Int32);

            DbParameter[] Params = new DbParameter[14] { BankDepositClearingIDP,DealerIDP,CustomerCodeP, TransactionDateFP, TransactionDateTP
                  , CreatedByP, CreatedOnFP, CreatedOnTP, AccountedByP,AccountedOnFP,AccountedOnTP, StatusIDP,StateIDP,RegionIDP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetBankDepositClearing", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            W = new PDMS_BankDepositClearing();
                            Ws.Add(W);
                            W.BankDepositClearingID = Convert.ToInt64(dr["BankDepositClearingID"]);

                            W.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.BankAccount = Convert.ToString(dr["BankAccount"]);
                            W.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            W.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
                            W.BankDescription = Convert.ToString(dr["BankDescription"]);
                            W.BranchCode = Convert.ToString(dr["BranchCode"]);
                            W.Amount = Convert.ToDecimal(dr["Amount"]);

                            W.IsMultipleCustomer = dr["IsMultipleCustomer"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsMultipleCustomer"]);
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.DepositFor = Convert.ToString(dr["DepositFor"]);

                            W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            W.PONumber = Convert.ToString(dr["PONumber"]);
                            W.SONumber = Convert.ToString(dr["SONumber"]);

                            W.MachineModel = Convert.ToString(dr["MachineModel"]);
                            W.Department = Convert.ToString(dr["Department"]);

                            W.BillDetailGivenBy = Convert.ToString(dr["BillDetailGivenBy"]);
                            W.BillDetailUpdatedOn = DBNull.Value == dr["BillDetailUpdatedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["BillDetailUpdatedOn"]);
                            W.Remarks = Convert.ToString(dr["Remarks"]);

                            W.Place = Convert.ToString(dr["Place"]);
                            W.State = new PDMS_State() { State = Convert.ToString(dr["State"]) };
                            W.Region = new PDMS_Region() { Region = Convert.ToString(dr["Region"]) };

                            W.ReferenceNo = Convert.ToString(dr["ReferenceNo"]);
                            W.HeaderText = Convert.ToString(dr["HeaderText"]);
                            W.Assignment = Convert.ToString(dr["Assignment"]);

                            W.RemitterAccount = Convert.ToString(dr["RemitterAccount"]);
                            W.RemitterName = Convert.ToString(dr["RemitterName"]);
                            W.RemitterEmail = Convert.ToString(dr["RemitterEmail"]);

                            W.RemitterMobile = Convert.ToString(dr["RemitterMobile"]);
                            W.RemitterBank = Convert.ToString(dr["RemitterBank"]);
                            W.RemitterIFSC = Convert.ToString(dr["RemitterIFSC"]);

                            W.CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedBy"]) };
                            W.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);

                            W.AccountedBy = dr["AccountedBy"] == DBNull.Value ? null : new PUser() { ContactName = Convert.ToString(dr["AccountedBy_ContactName"]) };
                            W.AccountedOn = DBNull.Value == dr["AccountedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["AccountedOn"]);

                            W.SapAccountNo = Convert.ToString(dr["SapAccountNo"]);
                            W.SapPostedOn = DBNull.Value == dr["SapPostedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["SapPostedOn"]);
                            W.SapClearedOn = DBNull.Value == dr["SapClearedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["SapClearedOn"]);
                            W.Status = new PDMS_BankDepositClearingStatus() { StatusID = Convert.ToInt32(dr["BankDepositClearingStatusID"]), Status = Convert.ToString(dr["BankDepositClearingStatus"]) };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }

        public void GetBankDepositClearingStatus(DropDownList ddl, int? StatusID, string Status)
        {
            try
            {
                List<PDMS_BankDepositClearingStatus> MML = GetBankDepositClearingStatus(StatusID, Status);
                ddl.DataValueField = "StatusID";
                ddl.DataTextField = "Status";
                ddl.DataSource = MML;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        public List<PDMS_BankDepositClearingStatus> GetBankDepositClearingStatus(int? StatusID, string Status)
        {
            List<PDMS_BankDepositClearingStatus> Ws = new List<PDMS_BankDepositClearingStatus>();
            PDMS_BankDepositClearingStatus W = null;
            DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);
            DbParameter StatusP = provider.CreateParameter("Status", string.IsNullOrEmpty(Status) ? null : Status, DbType.String);
            DbParameter[] Params = new DbParameter[2] { StatusIDP, StatusP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("GetBankDepositClearingStatus", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            W = new PDMS_BankDepositClearingStatus();
                            Ws.Add(W);
                            W.StatusID = Convert.ToInt32(dr["StatusID"]);
                            W.Status = Convert.ToString(dr["Status"]);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }


        public List<PDMS_BankDepositClearing> GetBankDepositClearingForEditAndConfirm(long? BankDepositClearingID, int? DealerID, DateTime? TransactionDateF, DateTime? TransactionDateT
        , int? CreatedBy, DateTime? CreatedOnF, DateTime? CreatedOnT)
        {
            List<PDMS_BankDepositClearing> Ws = new List<PDMS_BankDepositClearing>();
            PDMS_BankDepositClearing W = null;
            DbParameter BankDepositClearingIDP = provider.CreateParameter("BankDepositClearingID", BankDepositClearingID, DbType.Int64);
            DbParameter DealerIDP = provider.CreateParameter("DealerID", DealerID, DbType.Int32);

            DbParameter TransactionDateFP = provider.CreateParameter("TransactionDateF", TransactionDateF, DbType.DateTime);
            DbParameter TransactionDateTP = provider.CreateParameter("TransactionDateT", TransactionDateT, DbType.DateTime);

            DbParameter CreatedByP = provider.CreateParameter("CreatedBy", CreatedBy, DbType.Int32);
            DbParameter CreatedOnFP = provider.CreateParameter("CreatedOnF", CreatedOnF, DbType.DateTime);
            DbParameter CreatedOnTP = provider.CreateParameter("CreatedOnT", CreatedOnT, DbType.DateTime);

            //DbParameter AccountedByP = provider.CreateParameter("AccountedBy", AccountedBy, DbType.Int32);
            //DbParameter AccountedOnFP = provider.CreateParameter("AccountedOnF", AccountedOnF, DbType.DateTime);
            //DbParameter AccountedOnTP = provider.CreateParameter("AccountedOnT", AccountedOnT, DbType.DateTime);
            //DbParameter StatusIDP = provider.CreateParameter("StatusID", StatusID, DbType.Int32);


            DbParameter[] Params = new DbParameter[7] { BankDepositClearingIDP, DealerIDP, TransactionDateFP, TransactionDateTP, CreatedByP, CreatedOnFP, CreatedOnTP };
            try
            {
                using (DataSet EmployeeDataSet = provider.Select("ZDMS_GetBankDepositClearingForEditAndConfirm", Params))
                {
                    if (EmployeeDataSet != null)
                    {
                        foreach (DataRow dr in EmployeeDataSet.Tables[0].Rows)
                        {
                            W = new PDMS_BankDepositClearing();
                            Ws.Add(W);
                            W.BankDepositClearingID = Convert.ToInt64(dr["BankDepositClearingID"]);

                            W.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(dr["DealerID"]), DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                            W.BankAccount = Convert.ToString(dr["BankAccount"]);
                            W.TransactionDate = Convert.ToDateTime(dr["TransactionDate"]);
                            W.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
                            W.BankDescription = Convert.ToString(dr["BankDescription"]);
                            W.BranchCode = Convert.ToString(dr["BranchCode"]);
                            W.Amount = Convert.ToDecimal(dr["Amount"]);

                            W.IsMultipleCustomer = dr["IsMultipleCustomer"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsMultipleCustomer"]);
                            W.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                            W.DepositFor = Convert.ToString(dr["DepositFor"]);

                            W.InvoiceNumber = Convert.ToString(dr["InvoiceNumber"]);
                            W.PONumber = Convert.ToString(dr["PONumber"]);
                            W.SONumber = Convert.ToString(dr["SONumber"]);

                            W.MachineModel = Convert.ToString(dr["MachineModel"]);
                            W.Department = Convert.ToString(dr["Department"]);

                            W.BillDetailGivenBy = Convert.ToString(dr["BillDetailGivenBy"]);
                            W.BillDetailUpdatedOn = DBNull.Value == dr["BillDetailUpdatedOn"] ? (DateTime?)null : Convert.ToDateTime(dr["BillDetailUpdatedOn"]);
                            W.Remarks = Convert.ToString(dr["Remarks"]);

                            W.Place = Convert.ToString(dr["Place"]);
                            W.State = new PDMS_State() { State = Convert.ToString(dr["State"]) };
                            W.Region = new PDMS_Region() { Region = Convert.ToString(dr["Region"]) };

                            W.ReferenceNo = Convert.ToString(dr["ReferenceNo"]);
                            W.HeaderText = Convert.ToString(dr["HeaderText"]);
                            W.Assignment = Convert.ToString(dr["Assignment"]);

                            W.RemitterAccount = Convert.ToString(dr["RemitterAccount"]);
                            W.RemitterName = Convert.ToString(dr["RemitterName"]);
                            W.RemitterEmail = Convert.ToString(dr["RemitterEmail"]);

                            W.RemitterMobile = Convert.ToString(dr["RemitterMobile"]);
                            W.RemitterBank = Convert.ToString(dr["RemitterBank"]);
                            W.RemitterIFSC = Convert.ToString(dr["RemitterIFSC"]);

                            W.CreatedBy = new PUser() { ContactName = Convert.ToString(dr["CreatedBy"]) };
                            W.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);

                            W.Status = new PDMS_BankDepositClearingStatus() { StatusID = Convert.ToInt32(dr["BankDepositClearingStatusID"]), Status = Convert.ToString(dr["BankDepositClearingStatus"]) };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
            return Ws;
        }
    }
}
