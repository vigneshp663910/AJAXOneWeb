using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewFinance
{
    public partial class BankDepositClearingCreate : System.Web.UI.Page
    {
        public PDMS_BankDepositClearing Bank
        {
            get
            {
                if (Session["DMS_BankDepositClearingCreate"] == null)
                {
                    Session["DMS_BankDepositClearingCreate"] = new PDMS_BankDepositClearing();
                }
                return (PDMS_BankDepositClearing)Session["DMS_BankDepositClearingCreate"];
            }
            set
            {
                Session["DMS_BankDepositClearingCreate"] = value;
            }
        }
        public List<PDMS_BankDepositClearing> BankS
        {
            get
            {
                if (Session["DMS_BankDepositClearingCreateS"] == null)
                {
                    Session["DMS_BankDepositClearingCreateS"] = new List<PDMS_BankDepositClearing>();
                }
                return (List<PDMS_BankDepositClearing>)Session["DMS_BankDepositClearingCreateS"];
            }
            set
            {
                Session["DMS_BankDepositClearingCreateS"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty((string)Session["Edit_BankDepositClearingID"]))
                {
                    List<PModuleAccess> AccessModule = new BUser().GetDMSModuleByUser(PSession.User.UserID, null, (short)DMS_MenuSub.BankDepositClearingEditAndConfirm);
                    if (AccessModule.Count() == 0)
                    {
                        Response.Redirect("Home.aspx");
                    }
                    FillBankDepositClearing(Convert.ToInt64((string)Session["Edit_BankDepositClearingID"]));
                    pnlCreate.Enabled = false;
                    Session["Edit_BankDepositClearingID"] = null;
                    tbpnlAddress.Visible = false;

                    List<PModuleAccess> Create = new BUser().GetDMSModuleByUser(PSession.User.UserID, null, (short)DMS_MenuSub.BankDepositClearingCreate);
                    if (Create.Count() != 0)
                    {
                        Response.Redirect("Home.aspx");
                        pnlCreate.Enabled = true;
                    }

                }
                else
                {
                    List<PModuleAccess> AccessModule = new BUser().GetDMSModuleByUser(PSession.User.UserID, null, (short)DMS_MenuSub.BankDepositClearingCreate);
                    if (AccessModule.Count() == 0)
                    {
                        Response.Redirect("Home.aspx");
                    }
                    pnlEdit.Visible = false;
                }
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealer.Enabled = false;
                }
                else
                {
                    ddlDealer.Enabled = true;
                    fillDealer();
                }
                new BDMS_Address().GetStateDDL(ddlState, null, null, null, null);
                //new BDMS_Address().GetRegion(ddlRegion, null, null);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            Bank.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(ddlDealer.SelectedValue) };
            Bank.BankAccount = ddlBankAccount.SelectedValue.Trim();
            Bank.TransactionDate = Convert.ToDateTime(txtTransactionDate.Text.Trim());
            Bank.ValueDate = Convert.ToDateTime(txtValueDate.Text.Trim());
            Bank.BankDescription = txtBankDescription.Text.Trim();
            Bank.BranchCode = txtBranchCode.Text.Trim();
            Bank.Amount = Convert.ToDecimal(txtAmount.Text.Trim());

            Bank.IsMultipleCustomer = cbIsMultipleCustomer.Checked;
            if (Bank.IsMultipleCustomer)
            {
            }
            else if (!string.IsNullOrEmpty(txtCustomer.Text.Trim()))
            {
                List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, txtCustomer.Text.Trim());
                if (Customer.Count == 1)
                {
                    Bank.Customer = new PDMS_Customer() { CustomerID = Customer[0].CustomerID, CustomerCode = Customer[0].CustomerCode };
                }
                else
                {
                    lblMessage.Text = "Customer is not available";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }

            Bank.DepositFor = ddlDepositFor.Text.Trim();
            Bank.InvoiceNumber = txtInvoiceNumber.Text.Trim();
            Bank.PONumber = txtPONumber.Text.Trim();
            //   Bank.SONumber = txtSONumber.Text.Trim();

            Bank.MachineModel = txtMachineModel.Text.Trim();
            Bank.Department = ddlDepartment.SelectedValue;

            Bank.Place = txtPlace.Text.Trim();
            Bank.State = ddlState.SelectedValue == "0" ? null : new PDMS_State() { StateID = Convert.ToInt32(ddlState.SelectedValue) };
            Bank.Region = ddlRegion.SelectedValue == "0" ? null : new PDMS_Region() { RegionID = Convert.ToInt32(ddlRegion.SelectedValue) };
            Bank.BillDetailGivenBy = txtBillDetailGivenBy.Text.Trim();
            Bank.BillDetailUpdatedOn = DateTime.Now;
            Bank.Remarks = txtRemarks.Text.Trim();

            //  Bank.ReferenceNo = txtReferenceNo.Text.Trim();
            Bank.HeaderText = txtHeaderText.Text.Trim();
            Bank.Assignment = txtAssignment.Text.Trim();

            //Bank.RemitterAccount = txtRemitterAccount.Text.Trim();
            //Bank.RemitterName = txtRemitterName.Text.Trim();
            //Bank.RemitterEmail = txtRemitterEmail.Text.Trim();
            //Bank.RemitterMobile = txtRemitterMobile.Text.Trim();
            //Bank.RemitterBank = txtRemitterBank.Text.Trim();
            //Bank.RemitterIFSC = txtRemitterIFSC.Text.Trim();
            Bank.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            long BankDepositClearingID = new BDMS_BankDepositClearing().InsertOrUpdateBankDepositClearing(Bank);
            if (BankDepositClearingID != 0)
            {
                Bank.BankDepositClearingID = BankDepositClearingID;
                lblMessage.Text = "Bank Deposit Clearing is updated successfully";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "Bank Deposit Clearing is not updated successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            Boolean Ret = true;
            string Message = "";

            if (string.IsNullOrEmpty(txtTransactionDate.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the Transaction Date";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the Amount";
                Ret = false;
            }
            lblMessage.Text = Message;
            return Ret;
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            //  ddlDealer.Items.Insert(0, new ListItem("Select", "0"));
        }

        void FillBankDepositClearing(long BankDepositClearingID)
        {
            Bank = new BDMS_BankDepositClearing().GetBankDepositClearing(BankDepositClearingID, null, null, null, null, null, null, null, null, null, null, null, null, null)[0];
            ddlDealer.SelectedValue = Convert.ToString(Bank.Dealer.DealerID);
            ddlBankAccount.SelectedValue = Bank.BankAccount;
            txtTransactionDate.Text = Convert.ToString(Bank.TransactionDate);
            txtValueDate.Text = Convert.ToString(Bank.ValueDate);
            txtBankDescription.Text = Bank.BankDescription;
            txtBranchCode.Text = Bank.BranchCode;
            txtAmount.Text = Convert.ToString(Bank.Amount);

            cbIsMultipleCustomer.Checked = Bank.IsMultipleCustomer;
            txtCustomer.Text = Bank.Customer == null ? "" : Bank.Customer.CustomerCode;
            ddlDepositFor.SelectedValue = Bank.DepositFor;
            txtInvoiceNumber.Text = Bank.InvoiceNumber;
            txtPONumber.Text = Bank.PONumber;
            //  txtSONumber.Text = Bank.SONumber;

            txtMachineModel.Text = Bank.MachineModel;
            ddlDepartment.SelectedValue = Bank.Department;

            txtBillDetailGivenBy.Text = Bank.BillDetailGivenBy;
            txtBillDetailGivenOn.Text = Convert.ToString(Bank.BillDetailUpdatedOn);
            txtRemarks.Text = Bank.Remarks;

            txtPlace.Text = Bank.Place;
            ddlState.SelectedValue = Bank.State == null ? "0" : Convert.ToString(Bank.State.StateID);
            ddlRegion.SelectedValue = Bank.Region == null ? "0" : Convert.ToString(Bank.Region.RegionID);

            // txtReferenceNo.Text = Bank.ReferenceNo;
            txtHeaderText.Text = Bank.HeaderText;
            txtAssignment.Text = Bank.Assignment;

            //txtRemitterAccount.Text = Bank.RemitterAccount;
            //txtRemitterName.Text = Bank.RemitterName;
            //txtRemitterEmail.Text = Bank.RemitterEmail;
            //txtRemitterMobile.Text = Bank.RemitterMobile;
            //txtRemitterBank.Text = Bank.RemitterBank;
            //txtRemitterIFSC.Text = Bank.RemitterIFSC;

        }

        protected void lbBankDepositClearingTemplate_Click(object sender, EventArgs e)
        {
            string Path = Server.MapPath("Template/Bank Deposit Clearing Template.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"QRI Template.xlsx\"");
            byte[] data = req.DownloadData(Path);
            response.BinaryWrite(data);
            response.End();
        }

        protected void btnExelRead_Click(object sender, EventArgs e)
        {
            if (fu.PostedFile.FileName.Length > 0)
            {
                string path = ConfigurationManager.AppSettings["BasePath"] + "Upload/" + PSession.User.UserID + "/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string FilePath = path + long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")) + fu.PostedFile.FileName.Split('\\')[fu.PostedFile.FileName.Split('\\').Count() - 1];

                fu.SaveAs(FilePath);
                ConvertExcelToXML(FilePath);
                try
                {
                    //     File.Delete(FilePath);
                }
                catch
                { }
            }
        }


        private void ConvertExcelToXML(string path)
        {
            try
            {
                OleDbConnection MyConnection;
                System.Data.DataTable dtExcel = new System.Data.DataTable();
                System.Data.DataTable dtFiltered = new System.Data.DataTable();
                OleDbDataAdapter MyCommand;
                string conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data source={0};Extended Properties= 'Excel 12.0;HDR=YES;IMEX=1';", path);
                using (MyConnection = new OleDbConnection(conString))
                {
                    MyCommand = new OleDbDataAdapter("select * from [Sheet1$]", MyConnection);
                    MyCommand.Fill(dtExcel);
                    MyConnection.Close();
                    if (dtExcel.Rows.Count > 0)
                    {
                        BankS.Clear();
                        foreach (DataRow dr in dtExcel.Rows)
                        {

                            string DealerCode = Convert.ToString(dr["Dealer Code"]);
                            string AjaxBankAccount = Convert.ToString(dr["Ajax Bank Account"]);
                            string TransactionDate = Convert.ToString(dr["Transaction Date"]);
                            string BankDetail = Convert.ToString(dr["Bank Detail"]);
                            string BranchCode = Convert.ToString(dr["Branch Code"]);
                            string Amount = Convert.ToString(dr["Amount"]);

                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Visible = true;

                            if (string.IsNullOrEmpty(DealerCode))
                            {
                                lblMessage.Text = "Please Check Dealer Code";
                                lblMessage.ForeColor = Color.Red;
                                return;
                            }
                            if (string.IsNullOrEmpty(TransactionDate))
                            {
                                lblMessage.Text = "Please Check Transaction Date";
                                lblMessage.ForeColor = Color.Red;
                                return;
                            }
                            if (string.IsNullOrEmpty(Amount))
                            {
                                lblMessage.Text = "Please Check Amount";
                                lblMessage.ForeColor = Color.Red;
                                return;
                            }

                            List<PDMS_Dealer> Dealer = new BDMS_Dealer().GetDealer(null, DealerCode, null, null);
                            if (Dealer.Count != 1)
                            {
                                lblMessage.Text = "Please Check Dealer Code";
                                lblMessage.ForeColor = Color.Red;
                                return;
                            }

                            BankS.Add(new PDMS_BankDepositClearing()
                            {
                                Dealer = new PDMS_Dealer() { DealerCode = Dealer[0].DealerCode, DealerID = Dealer[0].DealerID, DealerName = Dealer[0].DealerName },

                                BankAccount = Convert.ToString(dr["Bank Account"]),
                                TransactionDate = Convert.ToDateTime(dr["Transaction Date"]),
                                ValueDate = Convert.ToDateTime(dr["Value Date"]),
                                BankDescription = Convert.ToString(dr["Bank Description"]),
                                BranchCode = Convert.ToString(dr["Branch Code"]),
                                Amount = Convert.ToDecimal(dr["Amount"]),
                            });
                        }
                        gvBank.DataSource = BankS;
                        gvBank.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnSaveMultiple_Click(object sender, EventArgs e)
        {
            foreach (PDMS_BankDepositClearing B in BankS)
            {

                B.CreatedBy = new PUser() { UserID = PSession.User.UserID };
                B.BankDepositClearingID = new BDMS_BankDepositClearing().InsertOrUpdateBankDepositClearing(B);
            }
            gvBank.DataSource = BankS;
            gvBank.DataBind();
            lblMessage.Text = "Bank Deposit Clearing is updated successfully";
            lblMessage.ForeColor = Color.Green;
            btnSaveMultiple.Visible = false;
        }
    }
}