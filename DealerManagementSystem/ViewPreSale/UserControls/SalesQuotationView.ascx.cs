using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SapIntegration;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class SalesQuotationView : System.Web.UI.UserControl
    {
        //private PLead Lead
        //{
        //    get
        //    {
        //        if (Session["PLead"] == null)
        //        {
        //            Session["PLead"] = new PLead();
        //        }
        //        return (PLead)Session["PLead"];
        //    }
        //    set
        //    {
        //        Session["PLead"] = value;
        //    }
        //}
        public PSalesQuotation Quotation
        {
            get
            {
                if (Session["SalesQuotationView"] == null)
                {
                    Session["SalesQuotationView"] = new PSalesQuotation();
                }
                return (PSalesQuotation)Session["SalesQuotationView"];
            }
            set
            {
                Session["SalesQuotationView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }
        public void FillMaster()
        {
            //FillGetDealerOffice();
            //new BDMS_IncoTerm().GetIncoTermDDL(ddlIncoterms, null, null);
            //new BDMS_Financier().GetFinancierDDL(ddlBankName, null, null); 

            new DDLBind(ddlIncoterms, new BDMS_Master().GetIncoterms(null, null, null), "IncoTerm", "IncoTermID");
            new DDLBind(ddlPaymentTerms, new BDMS_Master().GetPaymentTerms(null, null, null), "PaymentTerm", "PaymentTermID");
            new DDLBind(ddlBankName, new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Quotation Basic Info")
            {
                MPE_Quotation.Show();
                UC_Quotation.FillMaster();
                UC_Quotation.FillQuotation(Quotation);
            }
            else if (lbActions.Text == "Edit Financier Info")
            {
                new DDLBind(ddlBankName, new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");
                new DDLBind(ddlIncoterms, new BDMS_Master().GetIncoterms(null, null, null), "IncoTerms", "IncoTermsID");
                new DDLBind(ddlPaymentTerms, new BDMS_Master().GetPaymentTerms(null, null, null), "PaymentTerms", "PaymentTermsID");
                if (Quotation.Financier != null)
                {
                    ddlBankName.SelectedValue = Convert.ToString(Quotation.Financier.BankName.BankNameID);
                    ddlIncoterms.SelectedValue = Convert.ToString(Quotation.Financier.IncoTerms.IncoTermsID);
                    ddlPaymentTerms.SelectedValue = Convert.ToString(Quotation.Financier.PaymentTerms.PaymentTermsID);
                    txtDoNumber.Text = Quotation.Financier.DoNumber;
                    txtDoDate.Text = Quotation.Financier.DoDate == null ? "" : ((DateTime)Quotation.Financier.DoDate).ToString("yyyy-MM-dd");
                    txtAdvanceAmount.Text = Convert.ToString(Quotation.Financier.AdvanceAmount);
                    txtFinancierAmount.Text = Convert.ToString(Quotation.Financier.FinancierAmount);
                }

                MPE_Financier.Show();
            }
            else if (lbActions.Text == "Add Product")
            {
                new DDLBind(ddlPlant, new BDMS_Master().GetPlant(null, null), "PlantCode", "PlantID");
                MPE_Product.Show();
            }
            else if (lbActions.Text == "Add Competitor")
            {
                new DDLBind(ddlMake, new BDMS_Master().GetMake(null, null).Where(M => M.MakeID != 1), "Make", "MakeID");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
                MPE_Competitor.Show();
            }
            else if (lbActions.Text == "Add Quotation Note")
            {
                new DDLBind(ddlNote, new BSalesQuotation().GetSaleQuotationNoteList(null, null), "Note", "SalesQuotationNoteListID");
                MPE_Note.Show();
            }
            else if (lbActions.Text == "Add Follow-up")
            {

                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Quotation.Lead.LeadID, PSession.User.UserID, true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind((DropDownList)UC_FollowUp.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID", false);
                MPE_FollowUp.Show();
                UC_FollowUp.FillMaster();
            }
            else if (lbActions.Text == "Add Effort")
            {


                DropDownList ddlSalesEngineer = (DropDownList)UC_Effort.FindControl("ddlSalesEngineer");
                DropDownList ddlEffortType = (DropDownList)UC_Effort.FindControl("ddlEffortType");

                new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
                ddlSalesEngineer.Enabled = false;

                MPE_Effort.Show();

                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Quotation.Lead.LeadID, PSession.User.UserID, true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID", false);

            }
            else if (lbActions.Text == "Add Expense")
            {

                DropDownList ddlSalesEngineer = (DropDownList)UC_Expense.FindControl("ddlSalesEngineer");
                DropDownList ddlExpenseType = (DropDownList)UC_Expense.FindControl("ddlExpenseType");

                new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");

                ddlSalesEngineer.Enabled = false;
                MPE_Expense.Show();

                List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Quotation.Lead.LeadID, PSession.User.UserID, true);
                List<PUser> U = new List<PUser>();
                foreach (PLeadSalesEngineer SE in SalesEngineer)
                {
                    U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
                }
                new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID", false);

            }
            else if (lbActions.Text == "Generate Quotation")
            {
                GenerateQuotation();
                fillViewQuotation(Quotation.QuotationID);
            }
            else if (lbActions.Text == "Print PDF")
            {
                GeneratePDF();
            }
            else if (lbActions.Text == "Print Tax Quotation")
            {
                PrintTaxQuotation();
            }
        }
        protected void btnFinancier_Click(object sender, EventArgs e)
        {
            MPE_Financier.Show();
            string Message = ValidationFinancier();
            lblMessageFinancier.ForeColor = Color.Red;
            lblMessageFinancier.Visible = true;

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageFinancier.Text = Message;
                return;
            }

            PSalesQuotationFinancier Sqf = new PSalesQuotationFinancier();
            Sqf.QuotationID = Quotation.QuotationID;

            Sqf.BankName = new PBankName() { BankNameID = Convert.ToInt32(ddlBankName.SelectedValue) };
            Sqf.IncoTerms = new PIncoTerms() { IncoTermsID = Convert.ToInt32(ddlIncoterms.SelectedValue) };
            Sqf.PaymentTerms = new PPaymentTerms() { PaymentTermsID = Convert.ToInt32(ddlPaymentTerms.SelectedValue) };

            Sqf.DoNumber = txtDoNumber.Text.Trim();
            Sqf.DoDate = Convert.ToDateTime(txtDoDate.Text.Trim());

            Sqf.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
            Sqf.FinancierAmount = Convert.ToDecimal(txtFinancierAmount.Text.Trim());
            Sqf.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Financier", Sqf));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFinancier.Text = Results.Message;
                return;
            }
            Quotation.Financier = Sqf;
            MPE_Financier.Hide();
            tbpCust.ActiveTabIndex = 0;
            fillViewQuotation(Quotation.QuotationID);
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        protected void btnProductSave_Click(object sender, EventArgs e)
        {
            MPE_Product.Show();
            string Message = ValidationProduct();
            lblMessageProduct.ForeColor = Color.Red;
            lblMessageProduct.Visible = true;

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageProduct.Text = Message;
                return;
            }
            string Material = txtMaterial.Text.Trim();


            for (int i = 0; i < gvProduct.Rows.Count; i++)
            {
                Label lblMaterialCode = (Label)gvProduct.Rows[i].FindControl("lblMaterialCode");
                if (lblMaterialCode.Text == Material)
                {
                    lblMessageProduct.Text = "Material " + Material + " already available";
                    return;
                }
            }



            string OrderType = "";
            string Customer = Quotation.Lead.Customer.CustomerCode;
            string Vendor = "";
            string IV_SEC_SALES = "";
            string PRICEDATE = "";
            Boolean IsWarrenty = false;
            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            //{
            OrderType = "DEFAULT_SEC_AUART";
            //    Customer = SDMS_ICTicket.Customer.CustomerCode;
            //    Vendor = SDMS_ICTicket.Dealer.DealerCode;
            //}
            Material = Material.Split(' ')[0];
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material)[0];
            if (string.IsNullOrEmpty(MM.MaterialCode))
            {
                lblMessageProduct.Text = "Please check the Material";
                return;
            }
            decimal Qty = Convert.ToDecimal(txtQty.Text);
            //PDMS_ServiceMaterial MaterialTax = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, Material, Qty, IV_SEC_SALES, PRICEDATE, IsWarrenty);
            PDMS_ServiceMaterial MaterialTax = new SQuotation().getMaterialTaxForQuotation(Customer, Material, IsWarrenty);

            if (MaterialTax.BasePrice <= 0)
            {
                lblMessageProduct.Text = "Please maintain the price for Material " + Material + " in SAP";
                return;
            }

            PSalesQuotationItem Item = new PSalesQuotationItem();
            Item.SalesQuotationID = Quotation.QuotationID;
            Item.Material = new PDMS_Material();
            Item.Material.MaterialCode = MM.MaterialCode;
            Item.Material.MaterialID = MM.MaterialID;
            //Item.Material.MaterialDescription = MM.MaterialDescription;

            Item.Plant = new PPlant() { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
            Item.Qty = Convert.ToInt32(txtQty.Text);
            Item.Rate = MaterialTax.BasePrice;
            decimal P = (MaterialTax.BasePrice * Convert.ToDecimal(txtQty.Text));
            decimal Discount = P * Convert.ToDecimal(txtDiscount.Text) / 100;
            Item.Discount = Discount;

            Item.TaxableValue = (MaterialTax.BasePrice * Convert.ToDecimal(txtQty.Text)) - Discount;

            Item.CGST = MaterialTax.SGST;
            Item.SGST = MaterialTax.SGST;
            Item.IGST = MaterialTax.IGST;
            Item.CGSTValue = MaterialTax.SGSTValue;
            Item.SGSTValue = MaterialTax.SGSTValue;
            Item.IGSTValue = MaterialTax.IGSTValue;
            Item.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/QuotationItem", Item));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            //  Quotation.QuotationItems = Item;
            MPE_Product.Hide();
            tbpCust.ActiveTabIndex = 1;
            fillProduct();
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        protected void btnCompetitorSave_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            MPE_Competitor.Show();
            string Message = ValidationCompetitor();
            lblMessageCompetitor.ForeColor = Color.Red;
            lblMessageCompetitor.Visible = true;

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageCompetitor.Text = Message;
                return;
            }

            PSalesQuotationCompetitor Sqf = new PSalesQuotationCompetitor();
            Sqf.SalesQuotationID = Quotation.QuotationID;

            Sqf.Make = new PMake() { MakeID = Convert.ToInt32(ddlMake.SelectedValue) };
            Sqf.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
            Sqf.Product = new PProduct() { ProductID = Convert.ToInt32(ddlProduct.SelectedValue) };

            Sqf.Remark = txtCompetitorRemark.Text.Trim();
            Sqf.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Competitor", Sqf));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Competitor.Hide();
            tbpCust.ActiveTabIndex = 2;
            fillViewQuotation(Quotation.QuotationID);
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            PDMS_WebQuotationItem Item = new PDMS_WebQuotationItem();
            // Item.WebQuotationItemID = Convert.ToInt64(gvMaterial.DataKeys[gvRow.RowIndex].Value);

            if (new BDMS_WebQuotation().InsertOrUpdateWebQuotationItem(Item))
            {
                lblMessage.Text = "Material is Removed successfully";
                lblMessage.ForeColor = Color.Green;
                //  FillMaterial();
            }
            else
            {
                lblMessage.Text = "Material is not Removed successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnNoteRemark_Click(object sender, EventArgs e)
        {
            MPE_Note.Show();
            string Message = ValidationNote();
            lblMessageNote.ForeColor = Color.Red;
            lblMessageNote.Visible = true;

            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageNote.Text = Message;
                return;
            }

            PSalesQuotationNote Sqf = new PSalesQuotationNote();
            Sqf.SalesQuotationID = Quotation.QuotationID;

            Sqf.Note = new PSalesQuotationNoteList() { SalesQuotationNoteListID = Convert.ToInt32(ddlNote.SelectedValue) };

            Sqf.Remark = txtNoteRemark.Text.Trim();
            Sqf.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Note", Sqf));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Note.Hide();
            tbpCust.ActiveTabIndex = 3;
            fillViewQuotation(Quotation.QuotationID);
        }
        protected void BtnSaveQuotation_Click(object sender, EventArgs e)
        {
            MPE_Quotation.Show();
            string Message = UC_Quotation.ValidationSalesQuotation();
            lblMessageQuotation.ForeColor = Color.Red;
            lblMessageQuotation.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageQuotation.Text = Message;
                return;
            }
            PSalesQuotation Sq = new PSalesQuotation();
            Sq = UC_Quotation.ReadSalesQuotation();
            Sq.QuotationID = Quotation.QuotationID;
            Sq.Lead = new PLead { LeadID = Quotation.Lead.LeadID };
            Sq.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation", Sq));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            MPE_Quotation.Hide();
            fillViewQuotation(Quotation.QuotationID);
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        protected void btnSaveFollowUp_Click(object sender, EventArgs e)
        {
            MPE_FollowUp.Show();
            string Message = UC_FollowUp.ValidationFollowUp();
            lblMessageFollowUp.ForeColor = Color.Red;
            lblMessageFollowUp.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageFollowUp.Text = Message;
                return;
            }
            PLeadFollowUp FollowUp = new PLeadFollowUp();
            FollowUp = UC_FollowUp.ReadFollowUp();
            FollowUp.LeadFollowUpID = 0;
            FollowUp.LeadID = Quotation.Lead.LeadID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/FollowUp", FollowUp));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFollowUp.Text = Results.Message;
                return;
            }
            ShowMessage(Results);

            MPE_FollowUp.Hide();
            fillFollowUp();
        }
        //protected void btnSaveFollowUp_Click(object sender, EventArgs e)
        //{
        //    MPE_FollowUp.Show();
        //    string Message = UC_FollowUp.ValidationFollowUp();
        //    lblMessageFollowUp.ForeColor = Color.Red;
        //    lblMessageFollowUp.Visible = true;
        //    if (!string.IsNullOrEmpty(Message))
        //    {
        //        lblMessageFollowUp.Text = Message;
        //        return;
        //    }
        //    PLeadFollowUp FollowUp = new PLeadFollowUp();
        //    FollowUp = UC_FollowUp.ReadFollowUp();
        //    FollowUp.LeadFollowUpID = 0;
        //    FollowUp.LeadID = Quotation.QuotationID;


        //    PSalesQuotationFollowUp SalesQuotation = new PSalesQuotationFollowUp();

        //    SalesQuotation.FollowUpDate = FollowUp.FollowUpDate;
        //    SalesQuotation.FollowUpNote = FollowUp.FollowUpNote;
        //    SalesQuotation.SalesEngineer = FollowUp.SalesEngineer;
        //    SalesQuotation.CreatedBy = Lead.CreatedBy;

        //    SalesQuotation.SalesQuotationFollowUpID = 0;
        //    SalesQuotation.SalesQuotationID = Quotation.QuotationID;
        //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/FollowUp", SalesQuotation));
        //    if (Results.Status == PApplication.Failure)
        //    {
        //        lblMessageEffort.Text = Results.Message;
        //        return;
        //    }
        //    lblMessage.Text = "Updated Successfully";
        //    lblMessage.Visible = true;
        //    lblMessage.ForeColor = Color.Green;
        //    MPE_FollowUp.Hide();
        //    fillFollowUp();
        //}
        protected void btnSaveEffort_Click(object sender, EventArgs e)
        {

            MPE_Effort.Show();
            string Message = UC_Effort.ValidationEffort();
            lblMessageEffort.ForeColor = Color.Red;
            lblMessageEffort.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageEffort.Text = Message;
                return;
            }
            PLeadEffort Lead = new PLeadEffort();
            Lead = UC_Effort.ReadEffort();
            Lead.LeadEffortID = 0;
            Lead.LeadID = Quotation.QuotationID;

            PSalesQuotationEffort SalesQuotation = new PSalesQuotationEffort();

            SalesQuotation.SalesQuotationEffortID = 0;
            SalesQuotation.SalesQuotationID = Quotation.QuotationID;

            SalesQuotation.SalesEngineer = Lead.SalesEngineer;

            SalesQuotation.EffortDate = Lead.EffortDate;
            SalesQuotation.EffortStartTime = Lead.EffortStartTime;
            SalesQuotation.EffortEndTime = Lead.EffortEndTime;

            SalesQuotation.EffortType = Lead.EffortType;
            SalesQuotation.Remark = Lead.Remark;
            SalesQuotation.Effort = Lead.Effort;

            SalesQuotation.CreatedBy = Lead.CreatedBy;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Effort", SalesQuotation));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Effort.Hide();
            fillEffort();
        }
        protected void btnSaveExpense_Click(object sender, EventArgs e)
        {
            MPE_Expense.Show();
            string Message = UC_Expense.ValidationExpense();
            lblMessageExpense.ForeColor = Color.Red;
            lblMessageExpense.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageExpense.Text = Message;
                return;
            }
            PLeadExpense Lead = new PLeadExpense();
            Lead = UC_Expense.ReadExpense();
            Lead.LeadExpenseID = 0;
            Lead.LeadID = Quotation.QuotationID;

            PSalesQuotationExpense SalesQuotation = new PSalesQuotationExpense();
            SalesQuotation.SalesQuotationExpenseID = 0;
            SalesQuotation.SalesQuotationID = Quotation.QuotationID;
            SalesQuotation.SalesEngineer = Lead.SalesEngineer;
            SalesQuotation.ExpenseDate = Lead.ExpenseDate;
            SalesQuotation.Amount = Lead.Amount;
            SalesQuotation.ExpenseType = Lead.ExpenseType;
            SalesQuotation.Remark = Lead.Remark;

            SalesQuotation.CreatedBy = Lead.CreatedBy;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Expense", SalesQuotation));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageEffort.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Expense.Hide();
            fillExpense();
        }
        public void fillViewQuotation(long QuotationID)
        {
            Quotation = new BSalesQuotation().GetSalesQuotationByID(QuotationID);
            if (Quotation.QuotationID == 0)
            {
                lblMessage.Text = "Please Contact Administrator...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblRefQuotationNo.Text = Quotation.RefQuotationNo;
            lblRefQuotationDate.Text = Quotation.RefQuotationDate.ToLongDateString();

            lblQuotationNumber.Text = Quotation.QuotationNo;
            lblQuotationDate.Text = Quotation.QuotationDate == null ? "" : ((DateTime)Quotation.QuotationDate).ToLongDateString();
            lblQuotationType.Text = Quotation.QuotationType.QuotationType;
            lblQuotationStatus.Text = Quotation.Status.SalesQuotationStatus;
            lblValidFrom.Text = Quotation.ValidFrom == null ? "" : ((DateTime)Quotation.ValidFrom).ToLongDateString();
            lblValidTo.Text = Quotation.ValidTo == null ? "" : ((DateTime)Quotation.ValidTo).ToLongDateString();
            lblPricingDate.Text = Quotation.PricingDate == null ? "" : ((DateTime)Quotation.PricingDate).ToLongDateString();

            lblPriceGroup.Text = Quotation.PriceGroup == null ? "" : Quotation.PriceGroup.Description;
            lblUserStatus.Text = Quotation.UserStatus == null ? "" : Quotation.UserStatus.SalesQuotationUserStatus;

            fillFinancier();
            fillProduct();
            fillCompetitor();
            fillNote();
            fillFollowUp();
            fillEffort();
            fillExpense();
            ActionControlMange();

            UC_LeadView.fillViewLead(Quotation.Lead);
            CustomerViewSoldTo.fillCustomer(Quotation.Lead.Customer);
            //if (Quotation.ShipTo != null)
            //    CustomerViewShifTo.fillCustomer(Quotation.ShipTo);
        }
        public string ValidationFinancier()
        {
            string Message = "";
            if (ddlBankName.SelectedValue == "0")
            {
                Message = "Please select the Make";
            }
            if (ddlIncoterms.SelectedValue == "0")
            {
                Message = "Please select the Product Type";
            }
            if (ddlPaymentTerms.SelectedValue == "0")
            {
                Message = "Please select the Product";
            }
            return Message;
        }
        public string ValidationProduct()
        {
            string Message = "";
            if (ddlPlant.SelectedValue == "0")
            {
                Message = "Please select the Plant";
            }
            else if (string.IsNullOrEmpty(txtMaterial.Text.Trim()))
            {
                Message = "Please enter the note Material";
            }
            return Message;
        }
        public string ValidationCompetitor()
        {
            string Message = "";
            if (ddlMake.SelectedValue == "0")
            {
                Message = "Please select the Make";
            }
            if (ddlProductType.SelectedValue == "0")
            {
                Message = "Please select the Product Type";
            }
            if (ddlProduct.SelectedValue == "0")
            {
                Message = "Please select the Product";
            }
            return Message;
        }
        public string ValidationNote()
        {
            string Message = "";
            if (ddlNote.SelectedValue == "0")
            {
                Message = "Please select the note";
            }
            else if (string.IsNullOrEmpty(txtNoteRemark.Text.Trim()))
            {
                Message = "Please enter the note Remark";
            }
            return Message;
        }
        protected void lblCompetitorRemove_Click(object sender, EventArgs e)
        {

        }
        protected void lblNoteRemove_Click(object sender, EventArgs e)
        {

        }
        public void fillFinancier()
        {
            if (Quotation.Financier != null)
            {
                lblBankName.Text = Quotation.Financier.BankName == null ? "" : Quotation.Financier.BankName.BankName;
                lblIncoTerms.Text = Quotation.Financier.IncoTerms == null ? "" : Quotation.Financier.IncoTerms.IncoTerm_Description;
                lblPaymentTerms.Text = Quotation.Financier.PaymentTerms == null ? "" : Quotation.Financier.PaymentTerms.PaymentTerms;

                lblDoNumber.Text = Quotation.Financier.DoNumber;
                lblDoDate.Text = Convert.ToString(Quotation.Financier.DoDate);
                lblAdvanceAmount.Text = Convert.ToString(Quotation.Financier.AdvanceAmount);
                lblFinancierAmount.Text = Convert.ToString(Quotation.Financier.FinancierAmount);
            }
        }
        public void fillProduct()
        {
            gvProduct.DataSource = Quotation.QuotationItems;
            gvProduct.DataBind();
        }
        public void fillCompetitor()
        {
            gvCompetitor.DataSource = Quotation.Competitor;
            gvCompetitor.DataBind();
        }
        public void fillNote()
        {
            gvNote.DataSource = Quotation.Notes;
            gvNote.DataBind();
        }
        void fillFollowUp()
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUpByID(Quotation.Lead.LeadID, null);
            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();
        }
        void fillEffort()
        {
            gvEffort.DataSource = new BSalesQuotation().GetSalesQuotationEffort(Quotation.QuotationID, PSession.User.UserID);
            gvEffort.DataBind();



        }
        void fillExpense()
        {
            gvExpense.DataSource = new BSalesQuotation().GetSalesQuotationExpense(Quotation.QuotationID, PSession.User.UserID);
            gvExpense.DataBind();


        }
        void GenerateQuotation()
        {
            PSalesQuotation Q = Quotation;
            List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);
            DataTable DtResult = new SQuotation().getQuotationIntegration(Q, leadProducts);
            lblMessage.Text = "";
            foreach (DataRow dr in DtResult.Rows)
            {
                if (dr["Type"].ToString() == "S")
                {
                    lblMessage.Text = dr["Message"].ToString();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text += dr["Message"].ToString() + Environment.NewLine + "\n";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        void GeneratePDF()
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                Random r = new Random();
                string FileName = "QT_" + r.Next(0, 1000000) + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                PDMS_Customer Customer = Q.Lead.Customer;
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : "," + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

                PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
                string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
                string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

                List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);

                string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "", Subject = "";
                foreach (PSalesQuotationNote Note in Q.Notes)
                {
                    if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 10) { Subject = Note.Remark; }
                }
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;

                if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
                if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
                if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
                if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
                if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
                if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
                if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }
                if (Subject == "") { lblMessage.Text = "Subject Not Found"; return; }

                ReportParameter[] P = new ReportParameter[35];
                //Q.Lead.Dealer.
                P[0] = new ReportParameter("QuotationType", "MACHINE QUOTATION", false);
                P[1] = new ReportParameter("QuotationNo", Q.RefQuotationNo, false);
                P[2] = new ReportParameter("QuotationDate", Q.RefQuotationDate.ToString("dd.MM.yyyy"), false);
                P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2, false);
                P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
                P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
                P[8] = new ReportParameter("Attention", KindAttention, false);
                P[9] = new ReportParameter("Subject", Subject, false);
                P[10] = new ReportParameter("Reference", Reference, false);
                P[11] = new ReportParameter("Annexure", "A-I", false);
                P[12] = new ReportParameter("AnnexureRef", Q.RefQuotationNo, false);
                P[13] = new ReportParameter("AnnexureDate", Q.RefQuotationDate.ToString("dd.MM.yyyy"), false);
                P[14] = new ReportParameter("TCSTax", "TCSTax Persent", false);
                P[15] = new ReportParameter("Delivery", Delivery, false);
                //P[16] = new ReportParameter("InWordsTotalAmount", "ZERO RUPEES Only", false);
                //P[17] = new ReportParameter("TotalAmount", "00000.000", false);
                P[18] = new ReportParameter("Note", QNote, false);
                P[19] = new ReportParameter("WarrantyDeliveryHours", Q.QuotationItems[0].Material.Model.Division.WarrantyDeliveryHours, false);//2000
                //if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "CM")
                //{
                //    P[19] = new ReportParameter("WarrantyDeliveryHours", "2000", false);//2000
                //}
                //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "DP")
                //{
                //    P[19] = new ReportParameter("WarrantyDeliveryHours", "2000", false);//2000
                //}
                //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "BP")
                //{
                //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
                //}
                //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "CP")
                //{
                //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
                //}
                //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "TM")
                //{
                //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
                //}
                //else
                //{
                //    lblMessage.Text = "Division Not Available";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}

                P[20] = new ReportParameter("ConcernName", DealerBank[0].AuthorityName, false);
                P[21] = new ReportParameter("ConcernDesignation", DealerBank[0].AuthorityDesignation, false);
                P[22] = new ReportParameter("ConcernMobile", DealerBank[0].AuthorityMobile, false);
                string MaterialText = string.Empty;
                try
                {
                    MaterialText = new SQuotation().getMaterialTextForQuotation("L.900.508         AJF GT");                    
                }
                catch(Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                }
                P[23] = new ReportParameter("MaterialText", MaterialText, false);
                List<PPlant> Plant = new BDMS_Master().GetPlant(null, Q.QuotationItems[0].Plant.PlantCode);
                string PlantAddress1 = (Plant[0].Address1 + (string.IsNullOrEmpty(Plant[0].Address2) ? "" : "," + Plant[0].Address2) + (string.IsNullOrEmpty(Plant[0].Address3) ? "" : "," + Plant[0].Address3)).Trim(',', ' ');
                string PlantAddress2 = (Plant[0].City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Plant[0].State.State) + (string.IsNullOrEmpty(Plant[0].Country.Country) ? "" : "," + Plant[0].Country.Country)).Trim(',', ' ');

                if (Q.QuotationItems[0].Plant.PlantCode == "P003")
                {
                    P[24] = new ReportParameter("FactoryName", Plant[0].PlantName, false);
                    P[25] = new ReportParameter("FactoryAddress", PlantAddress1 + PlantAddress2, false);
                    P[26] = new ReportParameter("FactoryPhoneno", Ajax.Mobile, false);
                    P[27] = new ReportParameter("FactoryFax", "", false);
                    P[28] = new ReportParameter("FactoryWebsite", "www.ajax-engg.com", false);
                    P[29] = new ReportParameter("TCSTaxTerms", "If TCS is applicable, it will be calculated on sale consideration Plus GST.", false);
                }
                else if (Q.QuotationItems[0].Plant.PlantCode == "P001")
                {
                    P[24] = new ReportParameter("FactoryName", Plant[0].PlantName, false);
                    P[25] = new ReportParameter("FactoryAddress", PlantAddress1 + PlantAddress2, false);
                    P[26] = new ReportParameter("FactoryPhoneno", Ajax.Mobile, false);
                    P[27] = new ReportParameter("FactoryFax", "", false);
                    P[28] = new ReportParameter("FactoryWebsite", "www.ajax-engg.com", false);
                    P[29] = new ReportParameter("TCSTaxTerms", "", false);
                }
                
                if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "BP")
                {
                    P[30] = new ReportParameter("ErectionCommissoningHead", "ERECTION AND COMMISSONING :", false);
                    P[31] = new ReportParameter("ErectionCommissoning", "Erection and Commissioning will be in customer scope. Ajax shall be deputing service engineer for supervision of Erection and commissioning of the machine, on receipt of your confirmation of receipt of equipment and readiness of your site.The standard time for erection and commissioning is 1 day and additional 1 day for trail run &Training to your operation staff.The period of stay shall be restricted to 2 working days beyond that the services shall be on chargeable basis.Customer shall provide him all lodging, boarding & local conveyance facility.Customer shall provide all pulling tools, tackles, crane, skilled / unskilled labour, consumables like oil, welding machine, electrod etc., ", false);
                }
                else
                {
                    P[30] = new ReportParameter("ErectionCommissoningHead", "", false);
                    P[31] = new ReportParameter("ErectionCommissoning", "", false);
                }

                

                P[32] = new ReportParameter("CompanyName", Ajax.CustomerFullName, false);
                P[33] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
                P[34] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);


                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("TechnicalSpecification");
                dtItem.Columns.Add("Units");
                dtItem.Columns.Add("UnitPriceINR");
                dtItem.Columns.Add("AmountINR");
                decimal GrandTotal = 0;
                for (int i = 0; i < Q.QuotationItems.Count(); i++)
                {
                    dtItem.Rows.Add(Q.QuotationItems[i].Material.MaterialDescription, Q.QuotationItems[i].Material.BaseUnit, Q.QuotationItems[i].Rate, Q.QuotationItems[i].NetValue);
                    GrandTotal = Q.QuotationItems[i].NetValue;
                    P[16] = new ReportParameter("InWordsTotalAmount", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                    P[17] = new ReportParameter("TotalAmount", GrandTotal.ToString(), false);
                }
                report.ReportPath = Server.MapPath("~/Print/VigneshMachineQuotation.rdlc");
                report.SetParameters(P);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesQuotationItem";//This refers to the dataset name in the RDLC file  
                rds.Value = dtItem;
                report.DataSources.Add(rds); ;

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
        void PrintTaxQuotation()
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                Random r = new Random();
                string FileName = "QT_Tax" + r.Next(0, 1000000) + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;
                ReportParameter[] P = new ReportParameter[55];

                PDMS_Customer Customer = Q.Lead.Customer;
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : "," + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                PDMS_Customer CustomerShipTo = new PDMS_Customer();
                if (Q.ShipTo != null) { CustomerShipTo = CustomerShipTo = new BDMS_Customer().GetCustomerByID(Q.ShipTo.CustomerID); } else { CustomerShipTo = Q.Lead.Customer; }
            ;
                string CustomerAddressShipTo1 = (CustomerShipTo.Address1 + (string.IsNullOrEmpty(CustomerShipTo.Address2) ? "" : "," + CustomerShipTo.Address2) + (string.IsNullOrEmpty(CustomerShipTo.Address3) ? "" : "," + CustomerShipTo.Address3)).Trim(',', ' ');
                string CustomerAddressShipTo2 = (CustomerShipTo.City + (string.IsNullOrEmpty(CustomerShipTo.State.State) ? "" : "," + CustomerShipTo.State.State) + (string.IsNullOrEmpty(CustomerShipTo.Country.Country) ? "" : "," + CustomerShipTo.Country.Country) + (string.IsNullOrEmpty(CustomerShipTo.Pincode) ? "" : "-" + CustomerShipTo.Pincode)).Trim(',', ' ');

                string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "";

                foreach (PSalesQuotationNote Note in Q.Notes)
                {
                    if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
                    if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
                }

                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;

                if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
                if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
                if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
                if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
                if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
                if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
                if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }

                List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

                P[0] = new ReportParameter("QuotationType", "TAX QUOTATION", false);
                P[1] = new ReportParameter("QuotationNo", Q.RefQuotationNo, false);
                P[2] = new ReportParameter("QuotationDate", Q.RefQuotationDate.ToString(), false);
                P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2, false);
                P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
                P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
                P[8] = new ReportParameter("KindAttn", KindAttention, false);
                P[9] = new ReportParameter("CustomerStateCode", Q.Lead.Customer.State.StateCode, false);
                P[10] = new ReportParameter("CustomerGST", Q.Lead.Customer.GSTIN, false);
                P[11] = new ReportParameter("CustomerPAN", Q.Lead.Customer.PAN, false);

                P[19] = new ReportParameter("YourRef", Reference, false);
                P[20] = new ReportParameter("RevNo", "", false);
                P[21] = new ReportParameter("ShipToCustomerName", Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2, false);
                P[22] = new ReportParameter("ShipToCustomerAddress1", CustomerAddressShipTo1, false);
                P[23] = new ReportParameter("ShipToCustomerAddress2", CustomerAddressShipTo2, false);
                P[24] = new ReportParameter("ShipToMobile", CustomerShipTo.Mobile, false);
                P[25] = new ReportParameter("ShipToEMail", CustomerShipTo.Email, false);
                P[26] = new ReportParameter("ShipToCustomerStateCode", CustomerShipTo.State.StateCode, false);
                P[27] = new ReportParameter("ShipToCustomerGST", Q.Lead.Customer.GSTIN, false);
                P[28] = new ReportParameter("SoldToPartyBPCode", Q.Lead.Customer.CustomerCode, false);
                P[29] = new ReportParameter("ShipToPartyBPCode", CustomerShipTo.CustomerCode, false);
                P[30] = new ReportParameter("Hypothecation", Hypothecation, false);
                string Div = Q.QuotationItems[0].Material.Model.Division.DivisionCode;
                if (Div == "CM" || Div == "DP" || Div == "BP" || Div == "TM")
                {
                    P[31] = new ReportParameter("TermsandConditionHead", "TERMS & CONDITIONS:", false);
                    P[32] = new ReportParameter("PaymentTerms", "Payment Terms :", false);
                    P[33] = new ReportParameter("TermsOfPayment", TermsOfPayment + " along with order, balance payment against proforma invoice prior to dispatch . All payment to be made in favour of :", false);
                    if (DealerBank.Count > 0)
                    {
                        P[34] = new ReportParameter("PaymentTermAccName", "NAME                  : " + DealerBank[0].DealerName, false);
                        P[35] = new ReportParameter("PaymentTermBankName", "BANK NAME       : " + DealerBank[0].DealerBank.BankName, false);
                        P[36] = new ReportParameter("PaymentTermBankAddress", DealerBank[0].DealerBank.Branch);
                        P[37] = new ReportParameter("PaymentTermAccNo", "ACCOUNT NO.   : " + DealerBank[0].DealerBank.AcNumber, false);
                        P[38] = new ReportParameter("PaymentTermIFSCCode", "IFSC CODE         : " + DealerBank[0].DealerBank.IfscCode, false);
                    }
                    else
                    {
                        lblMessage.Text = "Bank Details Not Found";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    P[39] = new ReportParameter("Delivery", "Delivery : " + Delivery, false);
                    P[40] = new ReportParameter("TransitInsurance", "Transit Insurance: Customer to arrange and send the same before dispatch", false);
                    P[41] = new ReportParameter("Transportation", "Transportation : To-pay basis to customer's account.", false);
                    P[42] = new ReportParameter("Validity", "Validity :This offer is valid till : " + Validity, false);
                    P[43] = new ReportParameter("Note", "Note : " + QNote, false);
                }
                else if (Div == "CP")
                {
                    P[31] = new ReportParameter("TermsandConditionHead", "", false);
                    P[32] = new ReportParameter("PaymentTerms", "", false);
                    P[33] = new ReportParameter("TermsOfPayment", "", false);
                    P[34] = new ReportParameter("PaymentTermAccName", "", false);
                    P[35] = new ReportParameter("PaymentTermBankName", "", false);
                    P[36] = new ReportParameter("PaymentTermBankAddress", "");
                    P[37] = new ReportParameter("PaymentTermAccNo", "", false);
                    P[38] = new ReportParameter("PaymentTermIFSCCode", "", false);
                    P[39] = new ReportParameter("Delivery", "", false);
                    P[40] = new ReportParameter("TransitInsurance", "", false);
                    P[41] = new ReportParameter("Transportation", "", false);
                    P[42] = new ReportParameter("Validity", "", false);
                    P[43] = new ReportParameter("Note", "", false);
                }
                else
                {
                    lblMessage.Text = "Division Not Available";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                P[44] = new ReportParameter("Name", DealerBank[0].AuthorityName, false);
                P[45] = new ReportParameter("Designation", DealerBank[0].AuthorityDesignation, false);
                P[46] = new ReportParameter("MobileNo", DealerBank[0].AuthorityMobile, false);



                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("SNO");
                dtItem.Columns.Add("Material");
                dtItem.Columns.Add("Description");
                dtItem.Columns.Add("HSN");
                dtItem.Columns.Add("UOM");
                dtItem.Columns.Add("Qty");
                dtItem.Columns.Add("Rate");
                dtItem.Columns.Add("Total");
                dtItem.Columns.Add("Discount");
                dtItem.Columns.Add("Value");
                dtItem.Columns.Add("CGSTPer");
                dtItem.Columns.Add("CGSTVal");
                dtItem.Columns.Add("SGSTPer");
                dtItem.Columns.Add("SGSTVal");

                int i = 0;
                decimal TaxSubTotal = 0;
                decimal TCSSubTotal = 0;
                decimal SubTotal = 0;
                decimal Lifetimetax = 0;
                decimal GrandTotal = 0;
                foreach (PSalesQuotationItem item in Q.QuotationItems)
                {
                    i = i + 1;
                    if (item.SGST != 0)
                    {
                        P[47] = new ReportParameter("CGST_Header", "CGST %", false);
                        P[48] = new ReportParameter("CGSTVal_Header", "CGST Value", false);
                        P[49] = new ReportParameter("SGST_Header", "SGST %", false);
                        P[50] = new ReportParameter("SGSTVal_Header", "SGST Value", false);
                        dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
                            item.Rate, item.Qty * item.Rate, item.Discount, item.TaxableValue, item.CGST, item.CGSTValue, item.SGST, item.SGSTValue);
                        TaxSubTotal = item.TaxableValue + item.CGSTValue + item.SGSTValue;
                        TCSSubTotal = Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax;
                        P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                        P[13] = new ReportParameter("TotalAmount", TaxSubTotal.ToString(), false);
                        P[14] = new ReportParameter("Tax", "", false);
                        P[15] = new ReportParameter("TCS", TCSSubTotal.ToString(), false);
                        P[16] = new ReportParameter("SubTotal", SubTotal.ToString(), false);
                        P[17] = new ReportParameter("LifeTimeTax", Lifetimetax.ToString(), false);
                        P[18] = new ReportParameter("GrandTotal", GrandTotal.ToString(), false);
                    }
                    else
                    {
                        P[47] = new ReportParameter("CGST_Header", "", false);
                        P[48] = new ReportParameter("CGSTVal_Header", "", false);
                        P[49] = new ReportParameter("SGST_Header", "IGST %", false);
                        P[50] = new ReportParameter("SGSTVal_Header", "IGST Value", false);
                        dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
                            item.Rate, item.Qty * item.Rate, item.Discount, item.TaxableValue, null, null, item.IGST, item.IGSTValue);
                        TaxSubTotal = item.TaxableValue + item.CGSTValue + item.SGSTValue;
                        TCSSubTotal = Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax;
                        P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                        P[13] = new ReportParameter("TotalAmount", TaxSubTotal.ToString(), false);
                        P[14] = new ReportParameter("Tax", Q.TCSTax.ToString(), false);
                        P[15] = new ReportParameter("TCS", TCSSubTotal.ToString(), false);
                        P[16] = new ReportParameter("SubTotal", SubTotal.ToString(), false);
                        P[17] = new ReportParameter("LifeTimeTax", Lifetimetax.ToString(), false);
                        P[18] = new ReportParameter("GrandTotal", GrandTotal.ToString(), false);
                    }
                }
                PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
                string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
                string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');


                //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);

                P[51] = new ReportParameter("CompanyName", Ajax.CustomerFullName, false);
                P[52] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
                P[53] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
                P[54] = new ReportParameter("TCSPer", Q.TCSTax.ToString(), false);
                report.ReportPath = Server.MapPath("~/Print/VigneshTaxQuotation.rdlc");
                report.SetParameters(P);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesTaxQuotationItem";//This refers to the dataset name in the RDLC file  
                rds.Value = dtItem;
                report.DataSources.Add(rds); ;

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
        void ActionControlMange()
        {
            lbtnEditQuotation.Visible = true;
            lbtnEditFinancier.Visible = true;
            lbtnAddProduct.Visible = true;
            lbtnAddCompetitor.Visible = true;
            lbtnAddQuotationNote.Visible = true;
            lbtnAddFollowUp.Visible = true;
            lbtnAddEffort.Visible = true;
            lbtnAddExpense.Visible = true;
            lbtnGenerateQuotation.Visible = true;
            lbtnPrintPDF.Visible = true;


            //lbtnEditQuotation.Visible = false;
            //lbtnEditFinancier.Visible = false;
            //lbtnAddProduct.Visible = false;
            //lbtnAddCompetitor.Visible = false;
            //lbtnAddQuotationNote.Visible = false;
            //lbtnAddFollowUp.Visible = false;
            //lbtnAddEffort.Visible = false;
            //lbtnAddExpense.Visible = false;
            //lbtnGenerateQuotation.Visible = false;
            //lbtnPrintPDF.Visible = false;

        }
        protected void FillProduct(object sender, EventArgs e)
        {
            MPE_Competitor.Show();

            int? MakeID = ddlMake.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMake.SelectedValue);
            int? ProductTypeID = ddlProductType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductType.SelectedValue);
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, MakeID, ProductTypeID, null), "Product", "ProductID");
        }
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
    }
}