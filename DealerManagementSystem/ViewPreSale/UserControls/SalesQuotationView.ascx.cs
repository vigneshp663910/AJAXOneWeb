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
using System.IO;
using System.Configuration;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class SalesQuotationView : System.Web.UI.UserControl
    {
        
        public PSalesQuotation Quotation
        {
            get
            {
                if (ViewState["SalesQuotationView"] == null)
                {
                    ViewState["SalesQuotationView"] = new PSalesQuotation();
                }
                return (PSalesQuotation)ViewState["SalesQuotationView"];
            }
            set
            {
                ViewState["SalesQuotationView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;


            //if (!string.IsNullOrEmpty(Convert.ToString(ViewState["QuotationID"])))
            //{
            //    long QuotationID = Convert.ToInt64(Convert.ToString(ViewState["QuotationID"]));
            //    if (QuotationID != Quotation.QuotationID)
            //    {
            //        Quotation = new BSalesQuotation().GetSalesQuotationByID(QuotationID);
            //    }
            //}
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
                UC_Quotation.FillMaster(Quotation.Lead);
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
                else
                {
                    ddlIncoterms.SelectedValue = "5";
                    ddlPaymentTerms.SelectedValue = "22";
                }

                MPE_Financier.Show();
            }
            else if (lbActions.ID == "lbtnAddProduct")
            {
                // new DDLBind(ddlPlant, new BDMS_Master().GetPlant(null, null), "PlantCode", "PlantID");
                if (Quotation.IsStandard)
                {
                    UC_AddVariant.FillMaster(Quotation.Lead.ProductType.ProductTypeID, Quotation);
                    MPE_Variant.Show();
                }
                else
                {
                    MPE_Product.Show();
                }
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
                string Message = ValidationSalesQuotationGenerate();
                lblMessage.Visible = true;
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PApiResult Results = new BSalesQuotation().CreateQuotationInSap(Quotation.QuotationID);

                lblMessage.Text = Results.Message;
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                //GenerateQuotation(new PSalesQuotationItem());
                fillViewQuotation(Quotation.QuotationID);
            }
            else if (lbActions.Text == "Sale Order Confirmation")
            {
                string Message = ValidationSalesQuotationGenerate();
                lblMessage.Visible = true;
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PApiResult Results = new BSalesQuotation().CreateQuotationInPartsPortal(Quotation.QuotationID);

                lblMessage.Text = Results.Message;
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                //GenerateQuotation(new PSalesQuotationItem());
                fillViewQuotation(Quotation.QuotationID);
            }
            else if (lbActions.Text == "View Machine Quotation")
            {
                ViewMachineQuotation();
            }
            else if (lbActions.Text == "Download Machine Quotation")
            {
                DownloadMachineQuotation();
            }
            else if (lbActions.Text == "View Tax Quotation")
            {
                ViewTaxQuotation(false);
            }
            else if (lbActions.Text == "Download Tax Quotation")
            {
                DownloadTaxQuotation(false);
            }
            else if (lbActions.Text == "Download Consolidated Tax Quotation")
            {
                DownloadTaxQuotation(true);
            }
            else if (lbActions.Text == "Add Visit")
            {
                MPE_Visit.Show();
                int VisitMaxDay = Convert.ToInt32(ConfigurationManager.AppSettings["VisitMaxDay"]);
                ceVisitDate.StartDate = DateTime.Now.AddDays(-1 * VisitMaxDay);
                ceVisitDate.EndDate = DateTime.Now;

                new DDLBind(ddlActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
                new DDLBind(ddlImportance, new BDMS_Master().GetImportance(null, null), "Importance", "ImportanceID");
                new DDLBind(ddlPersonMet, new BDMS_Customer().GetCustomerRelation(Quotation.Lead.Customer.CustomerID, null), "ContactName", "CustomerRelationID");
            }
            else if (lbActions.Text == "Add Customer Singed Quotation")
            {
                MPE_CustomerSingedCopy.Show();
            }
            //else if (lbActions.Text == "Generate Commission Claim")
            //{ 
            //    lblMessage.Visible = true;
            //    PSalesQuotation Sqf = new PSalesQuotation();
            //    Sqf.QuotationID = Quotation.QuotationID; 
            //    Sqf.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesCommission", Sqf));
            //    if (Results.Status == PApplication.Failure)
            //    {
            //        lblMessage.Text = Results.Message;
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }  
            //    tbpSaleQuotation.ActiveTabIndex = 0;
            //    fillViewQuotation(Quotation.QuotationID);
            //    lblMessage.Text = "Updated Successfully";

            //    lblMessage.ForeColor = Color.Green;
            //}
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
            tbpSaleQuotation.ActiveTabIndex = 0;
            fillViewQuotation(Quotation.QuotationID);
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        protected void btnProductSave_Click(object sender, EventArgs e)
        {
            try
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






                string OrderType = "";
                //string Customer = Quotation.Lead.Customer.CustomerCode;
                string Vendor = "";
                string IV_SEC_SALES = "";
                string PRICEDATE = "";
                Boolean IsWarrenty = false;
                OrderType = "DEFAULT_SEC_AUART";
                Material = Material.Split(' ')[0];
                PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];

                for (int i = 0; i < gvProduct.Rows.Count; i++)
                {
                    Label lblMaterialCode = (Label)gvProduct.Rows[i].FindControl("lblMaterialCode");
                    if (lblMaterialCode.Text == MM.MaterialCode)
                    {
                        lblMessageProduct.Text = "Material " + Material + " already available";
                        return;
                    }
                }


                if (string.IsNullOrEmpty(MM.MaterialCode))
                {
                    lblMessageProduct.Text = "Please check the Material";
                    return;
                }
                else if ((MM.Model.Division.DivisionCode != Quotation.Lead.ProductType.Division.DivisionCode) && (MM.MaterialType == "FERT"))
                {
                    lblMessageProduct.Text = "Please check the Material";
                    return;
                }
                decimal Qty = Convert.ToDecimal(txtQty.Text);
                //PSalesQuotationItem MaterialTax = new SQuotation().getMaterialTaxForQuotation(Quotation, Material, IsWarrenty, Qty);

                PSalesQuotationItem MaterialTax = new BSalesQuotation().getMaterialTaxForQuotation(Quotation, Material, IsWarrenty, Qty);

                if (MaterialTax == null)
                {
                    lblMessageProduct.Text = "Please maintain the price for Material " + Material + " in SAP";
                    return;
                }
                if (MaterialTax.Rate <= 0)
                {
                    lblMessageProduct.Text = "Please maintain the price for Material " + Material + " in SAP";
                    return;
                }

                //PSalesQuotationItem Item = new PSalesQuotationItem();
                MaterialTax.SalesQuotationID = Quotation.QuotationID;
                MaterialTax.Material = new PDMS_Material();
                MaterialTax.Material.MaterialCode = MM.MaterialCode;
                MaterialTax.Material.MaterialID = MM.MaterialID;
                //Item.Material.MaterialDescription = MM.MaterialDescription;

                //MaterialTax.Plant = new PPlant() { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
                MaterialTax.Qty = Convert.ToInt32(txtQty.Text);
                //MaterialTax.Rate = MaterialTax.Rate;
                decimal P = (MaterialTax.Rate * Convert.ToDecimal(txtQty.Text));

                Decimal.TryParse(txtDiscount.Text, out Decimal Discount);
                MaterialTax.Discount = Discount;/*(Discount > 0) ? P * (Discount / 100) : 0;*/

                MaterialTax.TaxableValue = (MaterialTax.Rate * Convert.ToDecimal(txtQty.Text)) - Convert.ToDecimal(MaterialTax.Discount);

                if (MaterialTax.SGST != 0)
                {
                    MaterialTax.CGST = MaterialTax.SGST;
                    MaterialTax.CGSTValue = MaterialTax.TaxableValue * MaterialTax.SGST / 100;
                    MaterialTax.SGSTValue = MaterialTax.TaxableValue * MaterialTax.SGST / 100;
                    MaterialTax.IGSTValue = 0;
                }
                else
                {
                    MaterialTax.CGST = 0;
                    MaterialTax.CGSTValue = 0;
                    MaterialTax.SGSTValue = 0;
                    MaterialTax.IGSTValue = MaterialTax.TaxableValue * MaterialTax.IGST / 100;
                }
                if (MaterialTax.SGSTValue == 0 && MaterialTax.IGSTValue == 0)
                {
                    lblMessageProduct.Text = "GST Tax value not found this material..!";
                    return;
                }
                // this is Commented based on Rajendra Prasad
                //if (MaterialTax.TCSValue == 0)
                //{
                //    lblMessageProduct.Text = "TCS Tax value not found this material..!";
                //    return;
                //}


                MaterialTax.CreatedBy = new PUser() { UserID = PSession.User.UserID };
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/QuotationItem", MaterialTax));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageProduct.Text = Results.Message;
                    return;
                }
                //if (!string.IsNullOrEmpty(Quotation.SapQuotationNo))
                //{
                //if (Quotation.CommissionAgent)
                //{
                //    GenerateQuotation(MaterialTax);
                //}
                //PSalesQuotation Q = Quotation;
                //List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);
                //List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
                //Q.Lead.Dealer.AuthorityName = DealerBank[0].AuthorityName;
                //Q.Lead.Dealer.AuthorityDesignation = DealerBank[0].AuthorityDesignation;
                //Q.Lead.Dealer.AuthorityMobile = DealerBank[0].AuthorityMobile;
                //List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, 2, Q.QuotationID);
                //if (Visit[0].ColdVisitDate == null)
                //{
                //    lblMessage.Text = "Visit Date Not Found";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Green;
                //}

                //if (Q.QuotationItems.Count > 0 && leadProducts.Count > 0 && Q.Competitor.Count > 0)
                //{
                //    DataTable DtResult = new SQuotation().getQuotationIntegration(Q, leadProducts, Visit, MaterialTax);
                //    foreach (DataRow dr in DtResult.Rows)
                //    {
                //        if (dr["Type"].ToString() == "S")
                //        {
                //            lblMessage.Text = dr["Message"].ToString();
                //            lblMessage.Visible = true;
                //            lblMessage.ForeColor = Color.Green;
                //        }
                //        else
                //        {
                //            lblMessage.Text += dr["Message"].ToString() + Environment.NewLine + "\n";
                //            lblMessage.Visible = true;
                //            lblMessage.ForeColor = Color.Red;
                //        }
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = "Quotation Not Updated Successfully...!";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Red;
                //}
                //}

                MPE_Product.Hide();
                tbpSaleQuotation.ActiveTabIndex = 1;
                fillViewQuotation(Quotation.QuotationID);
                lblMessage.Text = "Updated Successfully";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessageProduct.Text = ex.Message.ToString();
                lblMessageProduct.ForeColor = Color.Red;
                return;
            }
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
                lblMessageCompetitor.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Competitor.Hide();
            tbpSaleQuotation.ActiveTabIndex = 2;
            fillViewQuotation(Quotation.QuotationID);
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesQuotationItemID = (Label)gvRow.FindControl("lblSalesQuotationItemID");
            //List<PSalesQuotationItem> Item = new List<PSalesQuotationItem>();
            PSalesQuotationItem Item = (PSalesQuotationItem)Quotation.QuotationItems.Where(M => M.SalesQuotationItemID == Convert.ToInt64(lblSalesQuotationItemID.Text)).ToList()[0];
            Item.SapFlag = "D";
            Item.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/QuotationItem", Item));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            //if (!string.IsNullOrEmpty(Quotation.SapQuotationNo))
            //{
            //    if (Quotation.CommissionAgent)
            //    {
            //        GenerateQuotation(Item);
            //    }
            //PSalesQuotation Q = Quotation;
            //List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);
            //List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
            //Q.Lead.Dealer.AuthorityName = DealerBank[0].AuthorityName;
            //Q.Lead.Dealer.AuthorityDesignation = DealerBank[0].AuthorityDesignation;
            //Q.Lead.Dealer.AuthorityMobile = DealerBank[0].AuthorityMobile;
            //List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, 2, Q.QuotationID);
            //if (Visit[0].ColdVisitDate == null)
            //{
            //    lblMessage.Text = "Visit Date Not Found";
            //    lblMessage.Visible = true;
            //    lblMessage.ForeColor = Color.Green;
            //}

            //if (Q.QuotationItems.Count > 0 && leadProducts.Count > 0 && Q.Competitor.Count > 0)
            //{
            //    DataTable DtResult = new SQuotation().getQuotationIntegration(Q, leadProducts, Visit, Item);
            //    foreach (DataRow dr in DtResult.Rows)
            //    {
            //        if (dr["Type"].ToString() == "S")
            //        {
            //            lblMessage.Text = dr["Message"].ToString();
            //            lblMessage.Visible = true;
            //            lblMessage.ForeColor = Color.Green;
            //        }
            //        else
            //        {
            //            lblMessage.Text += dr["Message"].ToString() + Environment.NewLine + "\n";
            //            lblMessage.Visible = true;
            //            lblMessage.ForeColor = Color.Red;
            //        }
            //    }
            //}
            //else
            //{
            //    lblMessage.Text = "Quotation Not Updated Successfully...!";
            //    lblMessage.Visible = true;
            //    lblMessage.ForeColor = Color.Red;
            //}
            //}
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            tbpSaleQuotation.ActiveTabIndex = 1;
            fillViewQuotation(Quotation.QuotationID);
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
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Note.Hide();
            tbpSaleQuotation.ActiveTabIndex = 3;
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
            PSalesQuotation_Insert Sq = new PSalesQuotation_Insert();
            Sq = UC_Quotation.ReadSalesQuotation();
            Sq.QuotationID = Quotation.QuotationID;
            Sq.LeadID = Quotation.Lead.LeadID;
            //  Sq.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation", Sq));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageQuotation.Text = Results.Message;
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
            fillViewQuotation(Quotation.QuotationID);
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
            fillViewQuotation(Quotation.QuotationID);
        }
        public void fillViewQuotation(long QuotationID)
        {
            //ViewState["QuotationID"] = QuotationID;
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

            lblSapQuotationNumber.Text = Quotation.SapQuotationNo;
            lblSapQuotationDate.Text = Quotation.SapQuotationDate == null ? "" : ((DateTime)Quotation.SapQuotationDate).ToLongDateString();
            lblPgQuotationNumber.Text = Quotation.PgQuotationNo;
            lblPgQuotationDate.Text = Quotation.PgQuotationDate == null ? "" : ((DateTime)Quotation.PgQuotationDate).ToLongDateString();

            lblQuotationType.Text = Quotation.QuotationType.QuotationType;
            lblQuotationStatus.Text = Quotation.Status.SalesQuotationStatus;
            lblValidFrom.Text = Quotation.ValidFrom == null ? "" : ((DateTime)Quotation.ValidFrom).ToLongDateString();
            lblValidTo.Text = Quotation.ValidTo == null ? "" : ((DateTime)Quotation.ValidTo).ToLongDateString();
            lblPricingDate.Text = Quotation.PricingDate == null ? "" : ((DateTime)Quotation.PricingDate).ToLongDateString();

            lblPriceGroup.Text = Quotation.PriceGroup == null ? "" : Quotation.PriceGroup.Description;
            lblUserStatus.Text = Quotation.UserStatus == null ? "" : Quotation.UserStatus.SalesQuotationUserStatus;
            lblProduct.Text = Quotation.LeadProduct.Product.Product;
            // lblTotalEffort.Text = Convert.ToString(Quotation.TotalEffort);
            // lblTotalExpense.Text = Convert.ToString(Quotation.TotalExpense);
            cbCommissionAgent.Checked = Quotation.CommissionAgent;
            fillFinancier();
            fillProduct();
            fillCompetitor();
            fillNote();
            fillFollowUp();
            //fillEffort();
            //fillExpense();
            ActionControlMange();

            UC_LeadView.fillViewLead(Quotation.Lead);
            CustomerViewSoldTo.fillCustomer(Quotation.Lead.Customer);

            fillShifTo();
            fillVisit();
            fillSalesCommissionClaim();
            fillCustomerSingedCopy();
        }
        public string ValidationFinancier()
        {
            string Message = "";
            if (ddlBankName.SelectedValue == "0")
            {
                Message = "Please select the BankName...!";
                return Message;
            }
            if (ddlIncoterms.SelectedValue == "0")
            {
                Message = "Please select the Inco Terms...!";
                return Message;
            }
            if (ddlPaymentTerms.SelectedValue == "0")
            {
                Message = "Please select the Payment Term...!";
                return Message;
            }
            if (string.IsNullOrEmpty(txtDoNumber.Text))
            {
                Message = "Please select the Do Number...!";
                return Message;
            }
            if (string.IsNullOrEmpty(txtDoDate.Text))
            {
                Message = "Please select the Do Date...!";
                return Message;
            }
            if (string.IsNullOrEmpty(txtAdvanceAmount.Text))
            {
                Message = "Please Enter Advance Amount...!";
                return Message;
            }
            if (string.IsNullOrEmpty(txtFinancierAmount.Text))
            {
                Message = "Please Enter Financier Amount...!";
                return Message;
            }
            return Message;
        }
        public string ValidationProduct()
        {
            string Message = "";
            //if (ddlPlant.SelectedValue == "0")
            //{
            //    Message = "Please select the Plant";
            //}
            //else
            if (string.IsNullOrEmpty(txtMaterial.Text.Trim()))
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
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesQuotationCompetitorID = (Label)gvRow.FindControl("lblSalesQuotationCompetitorID");

            PSalesQuotationCompetitor Sqf = new PSalesQuotationCompetitor();
            Sqf.SalesQuotationCompetitorID = Convert.ToInt64(lblSalesQuotationCompetitorID.Text);
            Sqf.Make = new PMake() { };
            Sqf.ProductType = new PProductType() { };
            Sqf.Product = new PProduct() { };

            Sqf.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Competitor", Sqf));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.ForeColor = Color.Green;
            MPE_Competitor.Hide();
            tbpSaleQuotation.ActiveTabIndex = 2;
            fillViewQuotation(Quotation.QuotationID);
        }
        protected void lblNoteRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblSalesQuotationNoteID = (Label)gvRow.FindControl("lblSalesQuotationNoteID");

            PSalesQuotationNote Sqf = new PSalesQuotationNote();
            Sqf.SalesQuotationNoteID = Convert.ToInt64(lblSalesQuotationNoteID.Text);
            Sqf.Note = new PSalesQuotationNoteList() { };
            Sqf.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Note", Sqf));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = "Updated Successfully";
            lblMessage.ForeColor = Color.Green;
            MPE_Competitor.Hide();
            tbpSaleQuotation.ActiveTabIndex = 3;
            fillViewQuotation(Quotation.QuotationID);
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
        
        Byte[] MachineQuotationRdlc(out string mimeType)
        {

            PSalesQuotation Q = Quotation;
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            //  string mimeType;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Customer Customer = Q.Lead.Customer;
            string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
            string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

            List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

            PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
            string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : ", " + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : ", " + Ajax.Address3)).Trim(',', ' ');
            string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : ", " + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

            PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(Q.Lead.Dealer.DealerCode);
            string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
            string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

            List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);

            string Reference = "", KindAttention = "", QNote = "" //, Hypothecation = "", TermsOfPayment = ""
                                                                  , Delivery = ""//, Validity = "", Foc = "", MarginMoney = "", Subject = ""
                , Name = "", Designation = "", PhoneNumber = "";
            foreach (PSalesQuotationNote Note in Q.Notes)
            {
                if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Reference) { Reference = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.KindAttention) { KindAttention = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Note) { QNote = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Hypothecation) { Hypothecation = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.TermsOfPayment) { TermsOfPayment = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Delivery) { Delivery = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Validity) { Validity = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Foc) { Foc = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.MarginMoney) { MarginMoney = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Subject) { Subject = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Name) { Name = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Designation) { Designation = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.PhoneNumber) { PhoneNumber = Note.Remark; }
            }
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;



            ReportParameter[] P = new ReportParameter[33];
            P[0] = new ReportParameter("QuotationType", "MACHINE QUOTATION", false);
            P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
            P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
            P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName/* Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2*/, false);
            P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
            P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
            P[8] = new ReportParameter("Attention", KindAttention, false);
            P[9] = new ReportParameter("Subject", Q.QuotationItems[0].Material.MaterialDescription, false);
            P[10] = new ReportParameter("Reference", Reference, false);
            P[11] = new ReportParameter("Annexure", "A-I", false);
            P[12] = new ReportParameter("AnnexureRef", Q.SapQuotationNo, false);
            P[13] = new ReportParameter("AnnexureDate", Q.SapQuotationDate.ToString(), false);
            P[14] = new ReportParameter("TCSTax", "TCSTax Persent", false);
            P[15] = new ReportParameter("Delivery", Delivery, false);
            P[18] = new ReportParameter("Note", QNote, false);
            P[19] = new ReportParameter("WarrantyDeliveryHours", Q.QuotationItems[0].Material.Model.Division.WarrantyDeliveryHours, false);//2000 
            P[20] = new ReportParameter("ConcernName", Name, false);
            P[21] = new ReportParameter("ConcernDesignation", Designation, false);
            P[22] = new ReportParameter("ConcernMobile", PhoneNumber, false);


            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("TechnicalSpecification");
            dtItem.Columns.Add("Units");
            dtItem.Columns.Add("UnitPriceINR");
            dtItem.Columns.Add("AmountINR");
            dtItem.Columns.Add("MaterialText1");
            dtItem.Columns.Add("MaterialText2");
            dtItem.Columns.Add("MaterialText3");
            dtItem.Columns.Add("MaterialText4");
            dtItem.Columns.Add("MaterialText5");
            decimal GrandTotal = 0;

            DataTable DTMaterialText = new DataTable();
            for (int i = 0; i < Q.QuotationItems.Count(); i++)
            {
                try
                {
                    //DTMaterialText = new SQuotation().getMaterialTextForQuotation(Q.QuotationItems[i].Material.MaterialCode);
                    DTMaterialText = new BSalesQuotation().getMaterialTextForQuotation(Q.QuotationItems[i].Material.MaterialCode);
                    
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                }
                int count = 0;

                string MaterialText1 = string.Empty;
                string MaterialText2 = string.Empty;
                string MaterialText3 = string.Empty;
                string MaterialText4 = string.Empty;
                string MaterialText5 = string.Empty;

                foreach (DataRow dr in DTMaterialText.Rows)
                {
                    count = count + 1;
                    if (count < 10)
                    {
                        MaterialText1 += string.IsNullOrEmpty(MaterialText1)? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#"); 
                    }
                    else if (count < 20)
                    {
                        MaterialText2 += string.IsNullOrEmpty(MaterialText2) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#");  
                    }
                    else if (count < 30)
                    {
                        MaterialText3 += string.IsNullOrEmpty(MaterialText3) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#");  
                    }
                    else if (count < 40)
                    {
                        MaterialText4 += string.IsNullOrEmpty(MaterialText4) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#");  
                    }
                    else
                    {
                        MaterialText5 += string.IsNullOrEmpty(MaterialText5) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#");  
                    }
                }

                P[23] = new ReportParameter("MaterialText", "", false);


                dtItem.Rows.Add(Q.QuotationItems[i].Material.MaterialDescription, Q.QuotationItems[i].Qty + " " + Q.QuotationItems[i].Material.BaseUnit, String.Format("{0:n}", Q.QuotationItems[i].Rate - Q.QuotationItems[i].Discount), String.Format("{0:n}", (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Q.QuotationItems[i].Discount), MaterialText1, MaterialText2, MaterialText3, MaterialText4, MaterialText5);
                GrandTotal += (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Convert.ToDecimal(Q.QuotationItems[i].Discount);

                P[16] = new ReportParameter("InWordsTotalAmount", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                P[17] = new ReportParameter("TotalAmount", String.Format("{0:n}", GrandTotal.ToString()), false);

            }
             

            if ((Q.Lead.ProductType.Division.DivisionCode == "CM") || (Q.Lead.ProductType.Division.DivisionCode == "DP"))
            {
                P[24] = new ReportParameter("TCSTaxTerms", "If TCS is applicable, it will be calculated on sale consideration Plus GST.", false);
            }
            else
            {
                P[24] = new ReportParameter("TCSTaxTerms", "", false); 
            }
            if (Q.Lead.ProductType.Division.DivisionCode == "BP")
            {
                P[25] = new ReportParameter("ErectionCommissoningHead", "ERECTION AND COMMISSONING :", false);
                P[26] = new ReportParameter("ErectionCommissoning", "Erection and Commissioning will be in customer scope. Ajax shall be deputing service engineer for supervision of Erection and commissioning of the machine, on receipt of your confirmation of receipt of equipment and readiness of your site.The standard time for erection and commissioning is 1 day and additional 1 day for trail run &Training to your operation staff.The period of stay shall be restricted to 2 working days beyond that the services shall be on chargeable basis.Customer shall provide him all lodging, boarding & local conveyance facility.Customer shall provide all pulling tools, tackles, crane, skilled / unskilled labour, consumables like oil, welding machine, electrod etc., ", false);
            }
            else
            {
                P[25] = new ReportParameter("ErectionCommissoningHead", "", false);
                P[26] = new ReportParameter("ErectionCommissoning", "", false);
            }


            if (Quotation.CommissionAgent)
            {
                P[27] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
                P[28] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
                P[29] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
                P[30] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
                P[31] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
                P[32] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
            }
            else
            {
                P[27] = new ReportParameter("CompanyName", Dealer.CustomerFullName, false);
                P[28] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
                P[29] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
                P[30] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Dealer.GSTIN);
                P[31] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
                P[32] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
            }


            report.ReportPath = Server.MapPath("~/Print/SalesMachineQuotation.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesQuotationItem";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds); ;
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }

        Byte[] MachineQuotationStdRdlc(List<PProductDrawing> ProductDrawing, out string mimeType)
        { 
            PSalesQuotation Q = Quotation;
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding; 
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            PDMS_Customer Customer = Q.Lead.Customer;
            string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
            string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

            List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

            PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
            string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : ", " + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : ", " + Ajax.Address3)).Trim(',', ' ');
            string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : ", " + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

            PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(Q.Lead.Dealer.DealerCode);
            string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
            string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

            List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);

            string Reference = "", KindAttention = "", QNote = "" //, Hypothecation = "", TermsOfPayment = ""
                                                                  , Delivery = ""//, Validity = "", Foc = "", MarginMoney = "", Subject = ""
                , Name = "", Designation = "", PhoneNumber = "";
            foreach (PSalesQuotationNote Note in Q.Notes)
            {
                if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Reference) { Reference = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.KindAttention) { KindAttention = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Note) { QNote = Note.Remark; } 
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Delivery) { Delivery = Note.Remark; } 
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Name) { Name = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Designation) { Designation = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.PhoneNumber) { PhoneNumber = Note.Remark; }
            }
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;



            ReportParameter[] P = new ReportParameter[38];
            P[0] = new ReportParameter("QuotationType", "MACHINE QUOTATION", false);
            P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
            P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
            P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName/* Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2*/, false);
            P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
            P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
            P[8] = new ReportParameter("Attention", KindAttention, false);
            P[9] = new ReportParameter("Subject", Q.QuotationItems[0].Material.MaterialDescription, false);
            P[10] = new ReportParameter("Reference", Reference, false);
            P[11] = new ReportParameter("Annexure", "A-I", false);
            P[12] = new ReportParameter("AnnexureRef", Q.SapQuotationNo, false);
            P[13] = new ReportParameter("AnnexureDate", Q.SapQuotationDate.ToString(), false);
            P[14] = new ReportParameter("TCSTax", "TCSTax Persent", false);
            P[15] = new ReportParameter("Delivery", Delivery, false);
            P[18] = new ReportParameter("Note", QNote, false);
            P[19] = new ReportParameter("WarrantyDeliveryHours", Q.QuotationItems[0].Material.Model.Division.WarrantyDeliveryHours, false);//2000 
            P[20] = new ReportParameter("ConcernName", Name, false);
            P[21] = new ReportParameter("ConcernDesignation", Designation, false);
            P[22] = new ReportParameter("ConcernMobile", PhoneNumber, false);


            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("TechnicalSpecification");
            dtItem.Columns.Add("Units");
            dtItem.Columns.Add("UnitPriceINR");
            dtItem.Columns.Add("AmountINR"); 
            decimal GrandTotal = 0;

            
            for (int i = 0; i < Q.QuotationItems.Count(); i++)
            { 
                P[23] = new ReportParameter("MaterialText", "", false);


                dtItem.Rows.Add(Q.QuotationItems[i].Material.MaterialDescription, Q.QuotationItems[i].Qty + " " + Q.QuotationItems[i].Material.BaseUnit, String.Format("{0:n}", Q.QuotationItems[i].Rate - Q.QuotationItems[i].Discount), String.Format("{0:n}", (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Q.QuotationItems[i].Discount));
                GrandTotal += (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Convert.ToDecimal(Q.QuotationItems[i].Discount);

                P[16] = new ReportParameter("InWordsTotalAmount", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                P[17] = new ReportParameter("TotalAmount", String.Format("{0:n}", GrandTotal.ToString()), false);

            }


            if ((Q.Lead.ProductType.Division.DivisionCode == "CM") || (Q.Lead.ProductType.Division.DivisionCode == "DP"))
            {
                P[24] = new ReportParameter("TCSTaxTerms", "If TCS is applicable, it will be calculated on sale consideration Plus GST.", false);
            }
            else
            {
                P[24] = new ReportParameter("TCSTaxTerms", "", false);
            }
            if (Q.Lead.ProductType.Division.DivisionCode == "BP")
            {
                P[25] = new ReportParameter("ErectionCommissoningHead", "ERECTION AND COMMISSONING :", false);
                P[26] = new ReportParameter("ErectionCommissoning", "Erection and Commissioning will be in customer scope. Ajax shall be deputing service engineer for supervision of Erection and commissioning of the machine, on receipt of your confirmation of receipt of equipment and readiness of your site.The standard time for erection and commissioning is 1 day and additional 1 day for trail run &Training to your operation staff.The period of stay shall be restricted to 2 working days beyond that the services shall be on chargeable basis.Customer shall provide him all lodging, boarding & local conveyance facility.Customer shall provide all pulling tools, tackles, crane, skilled / unskilled labour, consumables like oil, welding machine, electrod etc., ", false);
            }
            else
            {
                P[25] = new ReportParameter("ErectionCommissoningHead", "", false);
                P[26] = new ReportParameter("ErectionCommissoning", "", false);
            }


            if (Quotation.CommissionAgent)
            {
                P[27] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
                P[28] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
                P[29] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
                P[30] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
                P[31] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
                P[32] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
            }
            else
            {
                P[27] = new ReportParameter("CompanyName", Dealer.CustomerFullName, false);
                P[28] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
                P[29] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
                P[30] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Dealer.GSTIN);
                P[31] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
                P[32] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
            }
            //string   BatchingPlantImage1 = new Uri(Server.MapPath("~/d/TPhoto" + "." + FSRSignature.FileType)).AbsoluteUri;
            //string BatchingPlantImage2 = new Uri(Server.MapPath("~/" + Path + "TPhoto" + "." + FSRSignature.FileType)).AbsoluteUri;
            //string BatchingPlantTechSpec = new Uri(Server.MapPath("~/" + Path + "TPhoto" + "." + FSRSignature.FileType)).AbsoluteUri;

            string img1 = "", Image1Spec = "", img2 = "", Image2Spec = "", Spec = "";
            if (!Directory.Exists(Server.MapPath("~/Drawing/Product")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Drawing/Product"));
            }
            foreach (PProductDrawing D in ProductDrawing)
            { 
                string imgName = D.ProductDrawingID + "." + D.FileName.Split('.')[D.FileName.Split('.').Count() - 1];
                if (!File.Exists(Server.MapPath("~/Drawing/Product/"+ imgName)))
                {
                    PAttachedFile Files = new BDMS_Master().GetAttachedFileProductDrawingForDownload(D.ProductDrawingID + Path.GetExtension(D.FileName));
                    File.WriteAllBytes(Server.MapPath("~/Drawing/Product/" + imgName), Files.AttachedFile);
                } 
                if (D.ProductDrawingType.ProductDrawingTypeID == 1)
                {
                    img1 = imgName;
                    Image1Spec = D.DrawingDescription;
                }
                if (D.ProductDrawingType.ProductDrawingTypeID == 2)
                {
                    img2 = imgName;
                    Image2Spec = D.DrawingDescription;
                }
                if (D.ProductDrawingType.ProductDrawingTypeID == 3)
                {
                    Spec = imgName;
                }
            }


            string BatchingPlantImage1 = new Uri(Server.MapPath("~/Drawing/Product/"+ img1)).AbsoluteUri;
            string BatchingPlantImage2 = new Uri(Server.MapPath("~/Drawing/Product/"+ img2)).AbsoluteUri;
            string BatchingPlantTechSpec = new Uri(Server.MapPath("~/Drawing/Product/"+ Spec)).AbsoluteUri;

            
            
            P[33] = new ReportParameter("BatchingPlantImage1", BatchingPlantImage1);
            P[34] = new ReportParameter("BatchingPlantImage1Spec", Image1Spec);
            P[35] = new ReportParameter("BatchingPlantImage2", BatchingPlantImage2);
            P[36] = new ReportParameter("BatchingPlantImage2Spec", Image2Spec);
            P[37] = new ReportParameter("BatchingPlantTechSpec", BatchingPlantTechSpec);     

            report.ReportPath = Server.MapPath("~/Print/SalesMachineQuotationStd.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesQuotationItem";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds); ;
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

            return mybytes;
        }
        void ViewMachineQuotation()
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;
                if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
                {
                    lblMessage.Text = "Quotation Not Generated...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                string mimeType = string.Empty;
                Byte[] mybytes = null;
                if (Quotation.IsStandard)
                {
                    List<PProductDrawing> ProductDrawing = new BDMS_Master().GetProductDrawing(Q.LeadProduct.Product.ProductID);
                    if (ProductDrawing.Count != 3)
                    {
                        lblMessage.Text = "Drawing is not available";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    

                    mybytes = MachineQuotationStdRdlc(ProductDrawing, out mimeType);
                }
                else
                {
                      mybytes = MachineQuotationRdlc(out mimeType);
                }
                // string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
                string CustomerName = ((Q.Lead.Customer.CustomerName).Length > 20) ? (Q.Lead.Customer.CustomerName).Substring(0, 20) : (Q.Lead.Customer.CustomerName);

                string FileName = (Q.Lead.Dealer.DealerCode + "_MC_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
                string contentType = "application/pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Response.Redirect("../PDF.aspx?FileName=" + FileName + "&Title=Pre-Sales » Quotation", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
        void DownloadMachineQuotation()
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;
                if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
                {
                    lblMessage.Text = "Quotation Not Generated...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                string contentType = string.Empty;
                contentType = "application/pdf";


                // string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
                string CustomerName = ((Q.Lead.Customer.CustomerName).Length > 20) ? (Q.Lead.Customer.CustomerName).Substring(0, 20) : (Q.Lead.Customer.CustomerName);

                string FileName = (Q.Lead.Dealer.DealerCode + "_MC_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
                 
                
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                string mimeType;
                Byte[] mybytes = null;
                if (Quotation.IsStandard)
                {
                    List<PProductDrawing> ProductDrawing = new BDMS_Master().GetProductDrawing(Q.LeadProduct.Product.ProductID);
                    if (ProductDrawing.Count != 3)
                    {
                        lblMessage.Text = "Drawing is not available";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    mybytes = MachineQuotationStdRdlc(ProductDrawing,out mimeType);
                }
                else
                {
                     mybytes = MachineQuotationRdlc(out mimeType);
                }
                
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
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
         
        Byte[] TaxQuotationRdlc(out string mimeType, Boolean Consolidated)
        {

            mimeType = "";
            lblMessage.Text = "";
            PSalesQuotation Q = Quotation;
            if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
            {
                lblMessage.Text = "Quotation Not Generated...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return null;
            }
            string contentType = string.Empty;
            contentType = "application/pdf";
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            //   string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
            string CustomerName = ((Q.Lead.Customer.CustomerName).Length > 20) ? (Q.Lead.Customer.CustomerName).Substring(0, 20) : (Q.Lead.Customer.CustomerName);

            string FileName = (Q.Lead.Dealer.DealerCode + "_TAX_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
            string extension;
            string encoding;
            
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;
            ReportParameter[] P = new ReportParameter[58];

            PDMS_Customer Customer = Q.Lead.Customer;
            string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
            string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');


            PDMS_Customer CustomerShipTo = new PDMS_Customer();
            if (Q.ShipTo != null) { CustomerShipTo = CustomerShipTo = new BDMS_Customer().GetCustomerByID(Q.ShipTo.CustomerID); } else { CustomerShipTo = Q.Lead.Customer; }
                ;
            string CustomerAddressShipTo1 = (CustomerShipTo.Address1 + (string.IsNullOrEmpty(CustomerShipTo.Address2) ? "" : ", " + CustomerShipTo.Address2) + (string.IsNullOrEmpty(CustomerShipTo.Address3) ? "" : ", " + CustomerShipTo.Address3)).Trim(',', ' ');
            string CustomerAddressShipTo2 = (CustomerShipTo.City + (string.IsNullOrEmpty(CustomerShipTo.State.State) ? "" : ", " + CustomerShipTo.State.State) + (string.IsNullOrEmpty(CustomerShipTo.Country.Country) ? "" : ", " + CustomerShipTo.Country.Country) + (string.IsNullOrEmpty(CustomerShipTo.Pincode) ? "" : "-" + CustomerShipTo.Pincode)).Trim(',', ' ');

            string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = ""
                , MarginMoney = "", Name = "", Designation = "", PhoneNumber = "";

            foreach (PSalesQuotationNote Note in Q.Notes)
            {
                if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Reference) { Reference = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.KindAttention) { KindAttention = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Note) { QNote = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Hypothecation) { Hypothecation = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.TermsOfPayment) { TermsOfPayment = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Delivery) { Delivery = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Validity) { Validity = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Foc) { Foc = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.MarginMoney) { MarginMoney = Note.Remark; }

                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Name) { Name = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Designation) { Designation = Note.Remark; }
                else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.PhoneNumber) { PhoneNumber = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == ) { Foc = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == ) { MarginMoney = Note.Remark; }
                //else if (Note.Note.SalesQuotationNoteListID == (short)SalesQuotationNoteList.Subject) { Subject = Note.Remark; }


            }

            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;

            //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
            //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
            //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
            //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
            //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
            //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
            //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }


            List<PDMS_Dealer> DealerBank;
            if (Quotation.CommissionAgent)
            {
                DealerBank = new BDMS_Dealer().GetDealerBankDetails(53, null, null);
            }
            else
            {
                DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
            }




            P[0] = new ReportParameter("QuotationType", "TAX QUOTATION", false);
            P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
            P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
            P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName, false);
            P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
            P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
            P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
            P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
            P[8] = new ReportParameter("KindAttn", KindAttention, false);
            P[9] = new ReportParameter("CustomerStateCode", Q.Lead.Customer.State.StateCode, false);
            P[10] = new ReportParameter("CustomerGST", Q.Lead.Customer.GSTIN.ToUpper(), false);
            P[11] = new ReportParameter("CustomerPAN", Q.Lead.Customer.PAN.ToUpper(), false);

            P[19] = new ReportParameter("YourRef", Reference, false);
            P[20] = new ReportParameter("RevNo", Q.RevisionNumber, false);
            P[21] = new ReportParameter("ShipToCustomerName", Q.Lead.Customer.CustomerFullName, false);
            P[22] = new ReportParameter("ShipToCustomerAddress1", CustomerAddressShipTo1, false);
            P[23] = new ReportParameter("ShipToCustomerAddress2", CustomerAddressShipTo2, false);
            P[24] = new ReportParameter("ShipToMobile", CustomerShipTo.Mobile, false);
            P[25] = new ReportParameter("ShipToEMail", CustomerShipTo.Email, false);
            P[26] = new ReportParameter("ShipToCustomerStateCode", CustomerShipTo.State.StateCode, false);
            P[27] = new ReportParameter("ShipToCustomerGST", Q.Lead.Customer.GSTIN.ToUpper(), false);
            P[28] = new ReportParameter("SoldToPartyBPCode", Q.Lead.Customer.CustomerCode, false);
            P[29] = new ReportParameter("ShipToPartyBPCode", CustomerShipTo.CustomerCode, false);
            P[30] = new ReportParameter("Hypothecation", Hypothecation, false);
            string Div = Q.Lead.ProductType.Division.DivisionCode;
            if (Div == "CM" || Div == "DP" || Div == "BP" || Div == "TM" || Div == "CP" || Div == "SB")
            {
                P[31] = new ReportParameter("TermsandConditionHead", "TERMS & CONDITIONS:", false);
                P[32] = new ReportParameter("PaymentTerms", "Payment Terms :", false);
                P[33] = new ReportParameter("TermsOfPayment", TermsOfPayment + " along with order, balance payment against proforma invoice prior to dispatch . All payment to be made in favour of :", false);
                if (DealerBank.Count > 0)
                {
                    P[34] = new ReportParameter("PaymentTermAccName", "NAME                 : " + DealerBank[0].DealerName, false);
                    P[35] = new ReportParameter("PaymentTermBankName", "BANK NAME     : " + DealerBank[0].DealerBank.BankName, false);
                    P[36] = new ReportParameter("PaymentTermBankAddress", DealerBank[0].DealerBank.Branch);
                    P[37] = new ReportParameter("PaymentTermAccNo", "ACCOUNT NO.  : " + DealerBank[0].DealerBank.AcNumber, false);
                    P[38] = new ReportParameter("PaymentTermIFSCCode", "IFSC CODE         : " + DealerBank[0].DealerBank.IfscCode, false);
                }
                else
                {
                    lblMessage.Text = "Bank Details Not Found";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return null;
                }
                P[39] = new ReportParameter("Delivery", "Delivery : " + Delivery, false);
                P[40] = new ReportParameter("TransitInsurance", "Transit Insurance: Customer to arrange and send the same before dispatch", false);
                P[41] = new ReportParameter("Transportation", "Transportation : To-pay basis to customer's account.", false);
                P[42] = new ReportParameter("Validity", "Validity :This offer is valid till : " + Validity, false);
                P[43] = new ReportParameter("Note", "Note : " + QNote, false);
            }
            //else if (Div == "CP")
            //{
            //    P[31] = new ReportParameter("TermsandConditionHead", "", false);
            //    P[32] = new ReportParameter("PaymentTerms", "", false);
            //    P[33] = new ReportParameter("TermsOfPayment", "", false);
            //    P[34] = new ReportParameter("PaymentTermAccName", "", false);
            //    P[35] = new ReportParameter("PaymentTermBankName", "", false);
            //    P[36] = new ReportParameter("PaymentTermBankAddress", "");
            //    P[37] = new ReportParameter("PaymentTermAccNo", "", false);
            //    P[38] = new ReportParameter("PaymentTermIFSCCode", "", false);
            //    P[39] = new ReportParameter("Delivery", "", false);
            //    P[40] = new ReportParameter("TransitInsurance", "", false);
            //    P[41] = new ReportParameter("Transportation", "", false);
            //    P[42] = new ReportParameter("Validity", "", false);
            //    P[43] = new ReportParameter("Note", "", false);
            //}
            else if (!string.IsNullOrEmpty(Div))
            {
                lblMessage.Text = "Please Change First Line Item of Material";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return null;
            }
            else
            {
                lblMessage.Text = "Division Not Available";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return null;
            }

            P[44] = new ReportParameter("Name", Name, false);
            P[45] = new ReportParameter("Designation", Designation, false);
            P[46] = new ReportParameter("MobileNo", PhoneNumber, false);


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

            if (Consolidated)
            {
                string Material = "", Description = "", HSN = "", UOM = "";
                int Qty = 0;
                decimal Rate = 0, Total = 0, Discount = 0, Value = 0, CGSTPer = 0, CGSTVal = 0, SGSTPer = 0, SGSTVal = 0, IGSTPer = 0, IGSTVal = 0;
                foreach (PSalesQuotationItem item in Q.QuotationItems)
                {
                    i = i + 1;
                    if (item.Material.MaterialType == "FERT")
                    {
                        Material = item.Material.MaterialCode;
                        Description = item.Material.MaterialDescription;
                        HSN = item.Material.HSN;
                        UOM = item.Material.BaseUnit;
                        Qty = item.Qty; 
                    }

                    Rate = Rate + (item.TaxableValue / item.Qty);
                    Discount = Discount + item.Discount == null ? 0 : (decimal)item.Discount;

                   // Total = Total + item.TaxableValue;                    
                   // Value = Value + item.TaxableValue;
                    
                   // CGSTVal = CGSTVal + item.SGSTValue;
                   
                   

                    if (item.SGST != 0)
                    {
                        CGSTPer = item.CGST;
                        SGSTPer = item.SGST;

                        P[47] = new ReportParameter("CGST_Header", "CGST %", false);
                        P[48] = new ReportParameter("CGSTVal_Header", "CGST Value", false);
                        P[49] = new ReportParameter("SGST_Header", "SGST %", false);
                        P[50] = new ReportParameter("SGSTVal_Header", "SGST Value", false);
 
                        decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
                        decimal CGSTValues = (from x in Q.QuotationItems select x.CGSTValue).Sum();
                        decimal SGSTValues = (from x in Q.QuotationItems select x.SGSTValue).Sum();

                        TaxSubTotal = TaxableValues + CGSTValues + SGSTValues;
                        TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax;

                        Total = TaxableValues;
                        CGSTVal = CGSTValues;
                        SGSTVal = SGSTValues;

                    }
                    else
                    {
                        IGSTPer = item.IGST;

                        P[47] = new ReportParameter("CGST_Header", "", false);
                        P[48] = new ReportParameter("CGSTVal_Header", "", false);
                        P[49] = new ReportParameter("SGST_Header", "IGST %", false);
                        P[50] = new ReportParameter("SGSTVal_Header", "IGST Value", false);
                        
                        decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
                        decimal IGSTValues = (from x in Q.QuotationItems select x.IGSTValue).Sum();

                        TaxSubTotal = TaxableValues + IGSTValues;
                        TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax;

                        Total = TaxableValues;
                        IGSTVal = IGSTValues;
                    }
                }

                if (CGSTPer != 0)
                {
                    dtItem.Rows.Add(1, Material, Description, HSN, UOM, Qty,
                               String.Format("{0:n}", Rate), String.Format("{0:n}", Total), Discount, String.Format("{0:n}", Total)
                               , CGSTPer, String.Format("{0:n}", CGSTVal), SGSTPer, String.Format("{0:n}", SGSTVal));
                }
                else
                {
                    dtItem.Rows.Add(1, Material, Description, HSN, UOM, Qty,
                            String.Format("{0:n}", Rate), String.Format("{0:n}", Total), Discount, String.Format("{0:n}", Total), null, null, IGSTPer, String.Format("{0:n}", IGSTVal));

                }
            }
            else
            {
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
                            String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), item.SGST, String.Format("{0:n}", item.SGSTValue), item.SGST, String.Format("{0:n}", item.SGSTValue));

                        decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
                        decimal CGSTValues = (from x in Q.QuotationItems select x.CGSTValue).Sum();
                        decimal SGSTValues = (from x in Q.QuotationItems select x.SGSTValue).Sum();

                        TaxSubTotal = TaxableValues + CGSTValues + SGSTValues;
                        TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax; 
                    }
                    else
                    {
                        P[47] = new ReportParameter("CGST_Header", "", false);
                        P[48] = new ReportParameter("CGSTVal_Header", "", false);
                        P[49] = new ReportParameter("SGST_Header", "IGST %", false);
                        P[50] = new ReportParameter("SGSTVal_Header", "IGST Value", false);
                        dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
                            String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), null, null, item.IGST, String.Format("{0:n}", item.IGSTValue));

                        decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
                        decimal IGSTValues = (from x in Q.QuotationItems select x.IGSTValue).Sum();

                        TaxSubTotal = TaxableValues + IGSTValues;
                        TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax; 
                    }
                }
           }

            P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
            P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
            P[14] = new ReportParameter("Tax", "", false);
            P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
            P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
            P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
            P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", Convert.ToInt32(GrandTotal)), false);




            P[54] = new ReportParameter("TCSPer", Q.QuotationItems[0].TCSTax.ToString(), false);
            Boolean Success = false;

            Success = new BSalesQuotation().InsertSalesQuotationRevision(Q, CustomerAddress1, CustomerAddress2, CustomerAddressShipTo1, CustomerAddressShipTo2,
                KindAttention, Hypothecation, Reference, TermsOfPayment, Delivery, QNote, Validity, Convert.ToDecimal(GrandTotal));
            if (Success == false)
            {
                lblMessage.Text = "Revision Not Updated...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red; 
                return null;
            }

            PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
            string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
            string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

            PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(Q.Lead.Dealer.DealerCode);
            string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
            string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

            //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
            if (Quotation.CommissionAgent)
            {
                P[51] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
                P[52] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
                P[53] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
                P[55] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
                P[56] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
                P[57] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
            }
            else
            {
                P[51] = new ReportParameter("CompanyName", Dealer.CustomerFullName.ToUpper(), false);
                P[52] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
                P[53] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
                P[55] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Dealer.GSTIN);
                P[56] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
                P[57] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
            }

            if (Quotation.IsStandard)
            {
                report.ReportPath = Server.MapPath("~/Print/SalesTaxQuotationStd.rdlc");
            }
            else
            {
                report.ReportPath = Server.MapPath("~/Print/SalesTaxQuotation.rdlc");
            }
            report.SetParameters(P);

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SalesTaxQuotationItem";//This refers to the dataset name in the RDLC file  
            rds.Value = dtItem;
            report.DataSources.Add(rds); ;

            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  


            return mybytes;
        }

        void ViewTaxQuotation(Boolean Consolidated)
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;
                string contentType = string.Empty;
                contentType = "application/pdf";
                 
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
  
                 
               // string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
                string CustomerName = ((Q.Lead.Customer.CustomerName).Length > 20) ? (Q.Lead.Customer.CustomerName ).Substring(0, 20) : (Q.Lead.Customer.CustomerName );

                string FileName = (Q.Lead.Dealer.DealerCode + "_TAX_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");

                string mimeType = string.Empty;
                Byte[] mybytes = TaxQuotationRdlc(out  mimeType, Consolidated);
                if (mybytes == null)
                {
                    return;
                }
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Response.Redirect("../PDF.aspx?FileName=" + FileName + "&Title=Pre-Sales » Quotation", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
        }
        void DownloadTaxQuotation(Boolean Consolidated)
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
               // string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
                string CustomerName = ((Q.Lead.Customer.CustomerName).Length > 20) ? (Q.Lead.Customer.CustomerName).Substring(0, 20) : (Q.Lead.Customer.CustomerName);

                string FileName = (Q.Lead.Dealer.DealerCode + "_TAX_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");

                string mimeType = string.Empty;
                Byte[] mybytes = TaxQuotationRdlc(out mimeType, Consolidated);

                if (mybytes == null)
                {
                    return;
                }
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
            //lbtnAddEffort.Visible = true;
            //lbtnAddExpense.Visible = true;
            lbtnGenerateQuotation.Visible = true;

            lbtnViewTaxQuotation.Visible = true;
            lbtnDownloadTaxQuotation.Visible = true;
            lbtnDownloadConsolidatedTaxQuotation.Visible = true;
            lbtnViewMachineQuotation.Visible = true;
            lbtnDownloadMachineQuotation.Visible = true;



            lbtnSaleOrderConfirmation.Visible = true;

            lbtnAddVisit.Visible = true; 
            lbtnAddDiscount.Visible = true;

            if (Quotation.CommissionAgent)
            {
                lbtnSaleOrderConfirmation.Visible = false;
            }
            if (string.IsNullOrEmpty(Quotation.SapQuotationNo))
            {
                lbtnSaleOrderConfirmation.Visible = false;
            }

            if ((Quotation.Lead.Status.StatusID == (short)LeadStatus.Dropped)
                || (Quotation.Lead.Status.StatusID == (short)LeadStatus.SalesLost)
                || (Quotation.Lead.Status.StatusID == (short)LeadStatus.Won)
                )
            {
                lbtnEditQuotation.Visible = false;
                lbtnEditFinancier.Visible = false;
                lbtnAddProduct.Visible = false;
                lbtnAddCompetitor.Visible = false;
                lbtnAddQuotationNote.Visible = false;
                lbtnAddFollowUp.Visible = false;
                lbtnGenerateQuotation.Visible = false;

                lbtnViewTaxQuotation.Visible = false;
                lbtnDownloadTaxQuotation.Visible = false;
                lbtnDownloadConsolidatedTaxQuotation.Visible = false;
                lbtnViewMachineQuotation.Visible = false;
                lbtnDownloadMachineQuotation.Visible = false;
                lbtnSaleOrderConfirmation.Visible = false;
                lbtnAddVisit.Visible = false;
            }

            if (!Quotation.IsStandard)
            { 
                lbtnAddDiscount.Visible = false;
            } 

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TaxQuotationPrint).Count() == 0)
            {
                lbtnViewTaxQuotation.Visible = false;
                lbtnDownloadTaxQuotation.Visible = false;
                lbtnDownloadConsolidatedTaxQuotation.Visible = false;
            }
            if (Quotation.Status.SalesQuotationStatusID != (short)SalesQuotationStatus.Quotation)
            {
                lbtnEditQuotation.Visible = false;
                lbtnEditFinancier.Visible = false;
                lbtnAddProduct.Visible = false;
                lbtnAddCompetitor.Visible = false;
                lbtnAddQuotationNote.Visible = false;
                lbtnAddFollowUp.Visible = false;
                lbtnGenerateQuotation.Visible = false;
                lbtnSaleOrderConfirmation.Visible = false;
            }
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

        void fillShifTo()
        {
            if (Quotation.ShipTo != null)
            {
                //PDMS_CustomerShipTo ShipTo=  new BDMS_Customer().GetCustomerShopTo(null, Customer.CustomerID);
                lblShipToContactPerson.Text = Quotation.ShipTo.ContactPerson;
                lblShipToMobile.Text = "<a href='tel:" + Quotation.ShipTo.Mobile + "'>" + Quotation.ShipTo.Mobile + "</a>";
                lblShipToEmail.Text = "<a href='mailto:" + Quotation.ShipTo.Email + "'>" + Quotation.ShipTo.Email + "</a>";
                lblShipToAddress1.Text = Quotation.ShipTo.Address1;
                lblShipToAddress2.Text = Quotation.ShipTo.Address2;
                lblShipToAddress3.Text = Quotation.ShipTo.Address3;
                lblShipToCountry.Text = Quotation.ShipTo.Country.Country;
                lblShipToState.Text = Quotation.ShipTo.State.State;
                lblShipToDistrict.Text = Quotation.ShipTo.District.District;
                lblShipToTehsil.Text = Quotation.ShipTo.Tehsil.Tehsil;
                lblShipToPinCode.Text = Quotation.ShipTo.Pincode;
                lblShipToCity.Text = Quotation.ShipTo.City;
            }
            else
            {
                tbpSaleQuotation.Tabs[6].Visible = false;
            }
        }

        protected void btnSaveVisit_Click(object sender, EventArgs e)
        {
            MPE_Visit.Show();
            PColdVisit_Insert ColdVisitList = new PColdVisit_Insert();
            lblMessageColdVisit.ForeColor = Color.Red;
            lblMessageColdVisit.Visible = true;
            string Message = "";

            Message = ValidationColdVisit();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageColdVisit.Text = Message;
                return;
            }
            ColdVisitList.Customer = new PDMS_Customer_Insert() { CustomerID = Quotation.Lead.Customer.CustomerID };
            ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtVisitDate.Text.Trim());
            ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
            ColdVisitList.Importance = new PImportance() { ImportanceID = Convert.ToInt32(ddlImportance.SelectedValue) };
            ColdVisitList.PersonMet = ddlPersonMet.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlPersonMet.SelectedValue);
            ColdVisitList.Remark = txtVisitRemark.Text.Trim();
            ColdVisitList.Location = txtLocation.Text.Trim();
            ColdVisitList.ReferenceID = Quotation.QuotationID;
            ColdVisitList.ReferenceTableID = 2;
            //ColdVisitList.CreatedBy = new PUser { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ColdVisit", ColdVisitList));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageColdVisit.Text = Results.Message;
                return;
            }

            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = Results.Message;
            MPE_Visit.Hide();
            fillVisit();
        }
        void fillVisit()
        {
            List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, null, 2, Quotation.QuotationID, null, null, null);
            gvVisit.DataSource = Visit;
            gvVisit.DataBind();
        }
        public string ValidationColdVisit()
        {
            string Message = "";
            txtVisitDate.BorderColor = Color.Silver;
            txtVisitRemark.BorderColor = Color.Silver;
            ddlActionType.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtVisitDate.Text.Trim()))
            {
                txtVisitDate.BorderColor = Color.Red;
                return "Please enter the Cold Visit Date";
            }
            int VisitMaxDay = Convert.ToInt32(ConfigurationManager.AppSettings["VisitMaxDay"]);
            if (Convert.ToDateTime(txtVisitDate.Text.Trim()) < DateTime.Now.Date.AddDays(VisitMaxDay * -1))
            {
                txtVisitDate.BorderColor = Color.Red;
                return "You cannot select Visit Date more than 2 date back ";
            }
            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                txtLocation.BorderColor = Color.Red;
                return "Please enter the Location";
            }
            if (string.IsNullOrEmpty(txtVisitRemark.Text.Trim()))
            {
                txtVisitRemark.BorderColor = Color.Red;
                return "Please enter the Remark";
            }
            if (ddlActionType.SelectedValue == "0")
            {
                ddlActionType.BorderColor = Color.Red;
                return "Please select the Action Type";
            }
            return Message;
        }

        void fillSalesCommissionClaim()
        {
            List<PSalesCommissionClaim> claim = new BSalesCommissionClaim().GetSalesCommissionClaim(null, Quotation.QuotationID, null, null, null, null, null);
            gvSalesCommission.DataSource = claim;
            gvSalesCommission.DataBind();
            gvSalesCommissionItem.DataSource = claim;
            gvSalesCommissionItem.DataBind();

        }

        public string ValidationSalesQuotationGenerate()
        {
            string Message = "";

            if (Quotation.ValidFrom == null)
            {
                return "Please update the Valid From";
            }
            if (Quotation.ValidTo == null)
            {
                return "Please update the Valid To";
            }
            if (Quotation.PricingDate == null)
            {
                return "Please update the Pricing Date";
            }
            if (Quotation.QuotationItems.Count == 0)
            {
                return "Please update the Material";
            }
            //if (Quotation.Competitor.Count == 0)
            //{
            //    return "Please update the Competitor";
            //}
            List<PLeadQuestionaries> Questionaries = new BLead().GetLeadQuestionaries(Quotation.Lead.LeadID);
            if (Questionaries.Count == 0)
            {
                return "Please Add the Questionaries";
            }
            return Message;
        }

        protected void btnSaveVariant_Click(object sender, EventArgs e)
        { 
            try
            {
                MPE_Variant.Show();
                List<PSalesQuotationItem> MaterialTax = UC_AddVariant.ReadMaterial(); 
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/QuotationItems", MaterialTax));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageVariant.Text = Results.Message;
                    return;
                }
                MPE_Variant.Hide();
                tbpSaleQuotation.ActiveTabIndex = 1;
                fillViewQuotation(Quotation.QuotationID);
                lblMessage.Text = "Updated Successfully";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblMessageVariant.Text = ex.Message.ToString();
                lblMessageVariant.ForeColor = Color.Red;
                lblMessageVariant.Visible = true;
                return;
            }
        }

        protected void btnHeaderDiscount_Click(object sender, EventArgs e)
        {
            MPE_HeaderDiscount.Show();
            lblMessageVariant.Visible = true;
            lblMessageVariant.ForeColor = Color.Red;
            txtHeaderDiscount.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtHeaderDiscount.Text.Trim()))
            {
                txtHeaderDiscount.BorderColor = Color.Red;
                lblMessageVariant.Text = "Please enter the Discount";
            }
            decimal decimalValue;
            if (decimal.TryParse(txtHeaderDiscount.Text.Trim(), out decimalValue))
            {
                txtHeaderDiscount.BorderColor = Color.Red;
                lblMessageVariant.Text = "Please enter the Correct Discount Value";
            }
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SalesQuotation/UpdateSalesQuotationHeaderDiscount?SalesQuotationID=" + Quotation.QuotationID + "&HeaderDiscount=" + txtHeaderDiscount.Text));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageVariant.Text = Results.Message;
                return;
            }
            MPE_HeaderDiscount.Hide();
            tbpSaleQuotation.ActiveTabIndex = 1;
            fillViewQuotation(Quotation.QuotationID);
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        protected void btnCustomerSingedCopy_Click(object sender, EventArgs e)
        {
            MPE_CustomerSingedCopy.Show();
            lblMessageCustomerSingedCopy.Visible = true;
            lblMessageCustomerSingedCopy.ForeColor = Color.Red;

            txtCustomerAgreedPrice.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtCustomerAgreedPrice.Text.Trim()))
            {
                txtCustomerAgreedPrice.BorderColor = Color.Red;
                lblMessageCustomerSingedCopy.Text = "Please enter the Discount";
            }
            decimal decimalValue;
            if (decimal.TryParse(txtCustomerAgreedPrice.Text.Trim(), out decimalValue))
            {
                txtCustomerAgreedPrice.BorderColor = Color.Red;
                lblMessageCustomerSingedCopy.Text = "Please enter the Correct Discount Value";
            }
            

            if (fuCustomerSinged.PostedFile != null)
            {
                if (fuCustomerSinged.PostedFile.FileName.Length > 0)
                {
                    if (fuCustomerSinged.PostedFile.FileName.Length == 0)
                    {
                        lblMessageCustomerSingedCopy.Text = "Please select the file...!"; 
                        return;
                    }
                }
            }

            HttpPostedFile file = fuCustomerSinged.PostedFile;
          //  int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);

            byte[] fileData = new byte[file.ContentLength];
            file.InputStream.Read(fileData, 0, file.ContentLength);

            PSalesQuotationCustomerSinged Singed = new PSalesQuotationCustomerSinged();
            Singed.SalesQuotationID = Quotation.QuotationID;
            Singed.FileName = name;
            Singed.FileType = file.ContentType;
            Singed.AttachedFile = fileData;
            Singed.IsActive = true;
            Singed.CustomerAgreedPrice = Convert.ToDecimal(txtCustomerAgreedPrice.Text.Trim());
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/InsertOrUpdateSalesQuotationCustomerSinged" , Singed));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageCustomerSingedCopy.Text = Results.Message;
                return;
            }
            MPE_CustomerSingedCopy.Hide();
           // tbpSaleQuotation.ActiveTabIndex = 1;
            fillCustomerSingedCopy();
            lblMessage.Text = "Updated Successfully";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        void fillCustomerSingedCopy()
        {
            PSalesQuotationCustomerSinged Singed = new BSalesQuotation().GetSalesQuotationCustomerSinged(Quotation.QuotationID);
            lblSalesQuotationCustomerSingedID.Text = Singed.SalesQuotationCustomerSingedID.ToString();
            lblCustomerAgreedPrice.Text = Convert.ToString(Singed.CustomerAgreedPrice);
            lbtnCustomerSingedQuotationDownload.Text = Singed.FileName;
            // gvVisit.DataSource = Visit;
            // gvVisit.DataBind();
        }

        protected void lbtnCustomerSingedQuotationDownload_Click(object sender, EventArgs e)
        {
            PSalesQuotationCustomerSinged Singed = new BSalesQuotation().GetSalesQuotationCustomerSinged(Quotation.QuotationID);
            PAttachedFile Att = new BSalesQuotation().AttachedFileSalesQuotationCustomerSingedForDownload(lblSalesQuotationCustomerSingedID.Text + Path.GetExtension(lbtnCustomerSingedQuotationDownload.Text));
            Response.AddHeader("Content-type", Singed.FileType); 
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Singed.FileName);
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250"); 
            Response.BinaryWrite(Att.AttachedFile);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end 
            Response.Flush();
            Response.End();
        }
    }
}

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

//void ViewTaxQuotation()
//{
//    try
//    {
//        lblMessage.Text = "";
//        PSalesQuotation Q = Quotation;
//        if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
//        {
//            lblMessage.Text = "Quotation Not Generated...!";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }
//        string contentType = string.Empty;
//        contentType = "application/pdf";
//        var CC = CultureInfo.CurrentCulture;
//        Random r = new Random();
//        string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
//        string FileName = (Q.Lead.Dealer.DealerCode + "_TAX_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
//        string extension;
//        string encoding;
//        string mimeType;
//        string[] streams;
//        Warning[] warnings;
//        LocalReport report = new LocalReport();
//        report.EnableExternalImages = true;
//        ReportParameter[] P = new ReportParameter[58];

//        PDMS_Customer Customer = Q.Lead.Customer;
//        string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
//        string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');


//        PDMS_Customer CustomerShipTo = new PDMS_Customer();
//        if (Q.ShipTo != null) { CustomerShipTo = CustomerShipTo = new BDMS_Customer().GetCustomerByID(Q.ShipTo.CustomerID); } else { CustomerShipTo = Q.Lead.Customer; }
//    ;
//        string CustomerAddressShipTo1 = (CustomerShipTo.Address1 + (string.IsNullOrEmpty(CustomerShipTo.Address2) ? "" : ", " + CustomerShipTo.Address2) + (string.IsNullOrEmpty(CustomerShipTo.Address3) ? "" : ", " + CustomerShipTo.Address3)).Trim(',', ' ');
//        string CustomerAddressShipTo2 = (CustomerShipTo.City + (string.IsNullOrEmpty(CustomerShipTo.State.State) ? "" : ", " + CustomerShipTo.State.State) + (string.IsNullOrEmpty(CustomerShipTo.Country.Country) ? "" : ", " + CustomerShipTo.Country.Country) + (string.IsNullOrEmpty(CustomerShipTo.Pincode) ? "" : "-" + CustomerShipTo.Pincode)).Trim(',', ' ');

//        string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "";

//        foreach (PSalesQuotationNote Note in Q.Notes)
//        {
//            if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
//        }

//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;

//        //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
//        //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
//        //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
//        //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
//        //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
//        //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
//        //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }


//        List<PDMS_Dealer> DealerBank;
//        if (Quotation.CommissionAgent)
//        {
//            DealerBank = new BDMS_Dealer().GetDealerBankDetails(53, null, null);
//        }
//        else
//        {
//            DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
//        }




//        P[0] = new ReportParameter("QuotationType", "TAX QUOTATION", false);
//        P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
//        P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
//        P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName, false);
//        P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
//        P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
//        P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
//        P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
//        P[8] = new ReportParameter("KindAttn", KindAttention, false);
//        P[9] = new ReportParameter("CustomerStateCode", Q.Lead.Customer.State.StateCode, false);
//        P[10] = new ReportParameter("CustomerGST", Q.Lead.Customer.GSTIN.ToUpper(), false);
//        P[11] = new ReportParameter("CustomerPAN", Q.Lead.Customer.PAN.ToUpper(), false);

//        P[19] = new ReportParameter("YourRef", Reference, false);
//        P[20] = new ReportParameter("RevNo", Q.RevisionNumber, false);
//        P[21] = new ReportParameter("ShipToCustomerName", Q.Lead.Customer.CustomerFullName, false);
//        P[22] = new ReportParameter("ShipToCustomerAddress1", CustomerAddressShipTo1, false);
//        P[23] = new ReportParameter("ShipToCustomerAddress2", CustomerAddressShipTo2, false);
//        P[24] = new ReportParameter("ShipToMobile", CustomerShipTo.Mobile, false);
//        P[25] = new ReportParameter("ShipToEMail", CustomerShipTo.Email, false);
//        P[26] = new ReportParameter("ShipToCustomerStateCode", CustomerShipTo.State.StateCode, false);
//        P[27] = new ReportParameter("ShipToCustomerGST", Q.Lead.Customer.GSTIN.ToUpper(), false);
//        P[28] = new ReportParameter("SoldToPartyBPCode", Q.Lead.Customer.CustomerCode, false);
//        P[29] = new ReportParameter("ShipToPartyBPCode", CustomerShipTo.CustomerCode, false);
//        P[30] = new ReportParameter("Hypothecation", Hypothecation, false);
//        string Div = Q.Lead.ProductType.Division.DivisionCode;
//        if (Div == "CM" || Div == "DP" || Div == "BP" || Div == "TM" || Div == "CP" || Div == "SB")
//        {
//            P[31] = new ReportParameter("TermsandConditionHead", "TERMS & CONDITIONS:", false);
//            P[32] = new ReportParameter("PaymentTerms", "Payment Terms :", false);
//            P[33] = new ReportParameter("TermsOfPayment", TermsOfPayment + " along with order, balance payment against proforma invoice prior to dispatch . All payment to be made in favour of :", false);
//            if (DealerBank.Count > 0)
//            {
//                P[34] = new ReportParameter("PaymentTermAccName", "NAME                 : " + DealerBank[0].DealerName, false);
//                P[35] = new ReportParameter("PaymentTermBankName", "BANK NAME     : " + DealerBank[0].DealerBank.BankName, false);
//                P[36] = new ReportParameter("PaymentTermBankAddress", DealerBank[0].DealerBank.Branch);
//                P[37] = new ReportParameter("PaymentTermAccNo", "ACCOUNT NO.  : " + DealerBank[0].DealerBank.AcNumber, false);
//                P[38] = new ReportParameter("PaymentTermIFSCCode", "IFSC CODE         : " + DealerBank[0].DealerBank.IfscCode, false);
//            }
//            else
//            {
//                lblMessage.Text = "Bank Details Not Found";
//                lblMessage.Visible = true;
//                lblMessage.ForeColor = Color.Red;
//                return;
//            }
//            P[39] = new ReportParameter("Delivery", "Delivery : " + Delivery, false);
//            P[40] = new ReportParameter("TransitInsurance", "Transit Insurance: Customer to arrange and send the same before dispatch", false);
//            P[41] = new ReportParameter("Transportation", "Transportation : To-pay basis to customer's account.", false);
//            P[42] = new ReportParameter("Validity", "Validity :This offer is valid till : " + Validity, false);
//            P[43] = new ReportParameter("Note", "Note : " + QNote, false);
//        }
//        //else if (Div == "CP")
//        //{
//        //    P[31] = new ReportParameter("TermsandConditionHead", "", false);
//        //    P[32] = new ReportParameter("PaymentTerms", "", false);
//        //    P[33] = new ReportParameter("TermsOfPayment", "", false);
//        //    P[34] = new ReportParameter("PaymentTermAccName", "", false);
//        //    P[35] = new ReportParameter("PaymentTermBankName", "", false);
//        //    P[36] = new ReportParameter("PaymentTermBankAddress", "");
//        //    P[37] = new ReportParameter("PaymentTermAccNo", "", false);
//        //    P[38] = new ReportParameter("PaymentTermIFSCCode", "", false);
//        //    P[39] = new ReportParameter("Delivery", "", false);
//        //    P[40] = new ReportParameter("TransitInsurance", "", false);
//        //    P[41] = new ReportParameter("Transportation", "", false);
//        //    P[42] = new ReportParameter("Validity", "", false);
//        //    P[43] = new ReportParameter("Note", "", false);
//        //}
//        else if (!string.IsNullOrEmpty(Div))
//        {
//            lblMessage.Text = "Please Change First Line Item of Material";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }
//        else
//        {
//            lblMessage.Text = "Division Not Available";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }

//        P[44] = new ReportParameter("Name", Q.Lead.AssignedTo.ContactName, false);
//        P[45] = new ReportParameter("Designation", Q.Lead.AssignedTo.Designation.DealerDesignation, false);
//        P[46] = new ReportParameter("MobileNo", Q.Lead.AssignedTo.ContactNumber, false);


//        DataTable dtItem = new DataTable();
//        dtItem.Columns.Add("SNO");
//        dtItem.Columns.Add("Material");
//        dtItem.Columns.Add("Description");
//        dtItem.Columns.Add("HSN");
//        dtItem.Columns.Add("UOM");
//        dtItem.Columns.Add("Qty");
//        dtItem.Columns.Add("Rate");
//        dtItem.Columns.Add("Total");
//        dtItem.Columns.Add("Discount");
//        dtItem.Columns.Add("Value");
//        dtItem.Columns.Add("CGSTPer");
//        dtItem.Columns.Add("CGSTVal");
//        dtItem.Columns.Add("SGSTPer");
//        dtItem.Columns.Add("SGSTVal");

//        int i = 0;
//        decimal TaxSubTotal = 0;
//        decimal TCSSubTotal = 0;
//        decimal SubTotal = 0;
//        decimal Lifetimetax = 0;
//        decimal GrandTotal = 0;
//        foreach (PSalesQuotationItem item in Q.QuotationItems)
//        {
//            i = i + 1;
//            if (item.SGST != 0)
//            {
//                P[47] = new ReportParameter("CGST_Header", "CGST %", false);
//                P[48] = new ReportParameter("CGSTVal_Header", "CGST Value", false);
//                P[49] = new ReportParameter("SGST_Header", "SGST %", false);
//                P[50] = new ReportParameter("SGSTVal_Header", "SGST Value", false);
//                dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
//                    String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), item.SGST, String.Format("{0:n}", item.SGSTValue), item.SGST, String.Format("{0:n}", item.SGSTValue));

//                decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
//                decimal CGSTValues = (from x in Q.QuotationItems select x.CGSTValue).Sum();
//                decimal SGSTValues = (from x in Q.QuotationItems select x.SGSTValue).Sum();

//                TaxSubTotal = TaxableValues + CGSTValues + SGSTValues;
//                TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
//                SubTotal = TaxSubTotal + TCSSubTotal;
//                Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
//                GrandTotal = SubTotal + Lifetimetax;
//                P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
//                P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
//                P[14] = new ReportParameter("Tax", "", false);
//                P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
//                P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
//                P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
//                P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
//            }
//            else
//            {
//                P[47] = new ReportParameter("CGST_Header", "", false);
//                P[48] = new ReportParameter("CGSTVal_Header", "", false);
//                P[49] = new ReportParameter("SGST_Header", "IGST %", false);
//                P[50] = new ReportParameter("SGSTVal_Header", "IGST Value", false);
//                dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
//                    String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), null, null, item.IGST, String.Format("{0:n}", item.IGSTValue));

//                decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
//                decimal IGSTValues = (from x in Q.QuotationItems select x.IGSTValue).Sum();

//                TaxSubTotal = TaxableValues + IGSTValues;
//                TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
//                SubTotal = TaxSubTotal + TCSSubTotal;
//                Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
//                GrandTotal = SubTotal + Lifetimetax;
//                P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
//                P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
//                P[14] = new ReportParameter("Tax", "", false);
//                P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
//                P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
//                P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
//                P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
//            }
//        }
//        P[54] = new ReportParameter("TCSPer", Q.QuotationItems[0].TCSTax.ToString(), false);
//        Boolean Success = false;

//        Success = new BSalesQuotation().InsertSalesQuotationRevision(Q, CustomerAddress1, CustomerAddress2, CustomerAddressShipTo1, CustomerAddressShipTo2,
//            KindAttention, Hypothecation, Reference, TermsOfPayment, Delivery, QNote, Validity, Convert.ToDecimal(GrandTotal));
//        if (Success == false)
//        {
//            return;
//        }

//        PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
//        string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
//        string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

//        PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
//        string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
//        string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

//        //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
//        if (Quotation.CommissionAgent)
//        {
//            P[51] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
//            P[52] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
//            P[53] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
//            P[55] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
//            P[56] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
//            P[57] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
//        }
//        else
//        {
//            P[51] = new ReportParameter("CompanyName", Dealer.CustomerFullName.ToUpper(), false);
//            P[52] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
//            P[53] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
//            P[55] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Ajax.GSTIN);
//            P[56] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
//            P[57] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
//        }


//        report.ReportPath = Server.MapPath("~/Print/SalesTaxQuotation.rdlc");
//        report.SetParameters(P);

//        ReportDataSource rds = new ReportDataSource();
//        rds.Name = "SalesTaxQuotationItem";//This refers to the dataset name in the RDLC file  
//        rds.Value = dtItem;
//        report.DataSources.Add(rds); ;

//        Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

//        //Response.Buffer = true;
//        //Response.Clear();
//        //Response.ContentType = mimeType;
//        //Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
//        //Response.BinaryWrite(mybytes); // create the file
//        //Response.Flush(); // send it to the client to download
//        var uploadPath = Server.MapPath("~/Backup");
//        var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
//        File.WriteAllBytes(tempfilenameandlocation, mybytes);
//        Response.Redirect("../PDF.aspx?FileName=" + FileName + "&Title=Pre-Sales » Quotation", false);
//    }
//    catch (Exception ex)
//    {
//        lblMessage.Text = ex.Message.ToString();
//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;
//        return;
//    }
//}
//void DownloadTaxQuotation()
//{
//    try
//    {
//        lblMessage.Text = "";
//        PSalesQuotation Q = Quotation;
//        if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
//        {
//            lblMessage.Text = "Quotation Not Generated...!";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }
//        string contentType = string.Empty;
//        contentType = "application/pdf";
//        var CC = CultureInfo.CurrentCulture;
//        Random r = new Random();
//        string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
//        string FileName = (Q.Lead.Dealer.DealerCode + "_TAX_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
//        string extension;
//        string encoding;
//        string mimeType;
//        string[] streams;
//        Warning[] warnings;
//        LocalReport report = new LocalReport();
//        report.EnableExternalImages = true;
//        ReportParameter[] P = new ReportParameter[58];

//        PDMS_Customer Customer = Q.Lead.Customer;
//        string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
//        string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');


//        PDMS_Customer CustomerShipTo = new PDMS_Customer();
//        if (Q.ShipTo != null) { CustomerShipTo = CustomerShipTo = new BDMS_Customer().GetCustomerByID(Q.ShipTo.CustomerID); } else { CustomerShipTo = Q.Lead.Customer; }
//    ;
//        string CustomerAddressShipTo1 = (CustomerShipTo.Address1 + (string.IsNullOrEmpty(CustomerShipTo.Address2) ? "" : ", " + CustomerShipTo.Address2) + (string.IsNullOrEmpty(CustomerShipTo.Address3) ? "" : ", " + CustomerShipTo.Address3)).Trim(',', ' ');
//        string CustomerAddressShipTo2 = (CustomerShipTo.City + (string.IsNullOrEmpty(CustomerShipTo.State.State) ? "" : ", " + CustomerShipTo.State.State) + (string.IsNullOrEmpty(CustomerShipTo.Country.Country) ? "" : ", " + CustomerShipTo.Country.Country) + (string.IsNullOrEmpty(CustomerShipTo.Pincode) ? "" : "-" + CustomerShipTo.Pincode)).Trim(',', ' ');

//        string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "";

//        foreach (PSalesQuotationNote Note in Q.Notes)
//        {
//            if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
//        }

//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;

//        //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
//        //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
//        //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
//        //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
//        //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
//        //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
//        //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }

//        List<PDMS_Dealer> DealerBank;
//        if (Quotation.CommissionAgent)
//        {
//            DealerBank = new BDMS_Dealer().GetDealerBankDetails(53, null, null);
//        }
//        else
//        {
//            DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
//        }

//        P[0] = new ReportParameter("QuotationType", "TAX QUOTATION", false);
//        P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
//        P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
//        P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName, false);
//        P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
//        P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
//        P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
//        P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
//        P[8] = new ReportParameter("KindAttn", KindAttention, false);
//        P[9] = new ReportParameter("CustomerStateCode", Q.Lead.Customer.State.StateCode, false);
//        P[10] = new ReportParameter("CustomerGST", Q.Lead.Customer.GSTIN.ToUpper(), false);
//        P[11] = new ReportParameter("CustomerPAN", Q.Lead.Customer.PAN.ToUpper(), false);

//        P[19] = new ReportParameter("YourRef", Reference, false);
//        P[20] = new ReportParameter("RevNo", Q.RevisionNumber, false);
//        P[21] = new ReportParameter("ShipToCustomerName", Q.Lead.Customer.CustomerFullName, false);
//        P[22] = new ReportParameter("ShipToCustomerAddress1", CustomerAddressShipTo1, false);
//        P[23] = new ReportParameter("ShipToCustomerAddress2", CustomerAddressShipTo2, false);
//        P[24] = new ReportParameter("ShipToMobile", CustomerShipTo.Mobile, false);
//        P[25] = new ReportParameter("ShipToEMail", CustomerShipTo.Email, false);
//        P[26] = new ReportParameter("ShipToCustomerStateCode", CustomerShipTo.State.StateCode, false);
//        P[27] = new ReportParameter("ShipToCustomerGST", Q.Lead.Customer.GSTIN.ToUpper(), false);
//        P[28] = new ReportParameter("SoldToPartyBPCode", Q.Lead.Customer.CustomerCode, false);
//        P[29] = new ReportParameter("ShipToPartyBPCode", CustomerShipTo.CustomerCode, false);
//        P[30] = new ReportParameter("Hypothecation", Hypothecation, false);
//        string Div = Q.Lead.ProductType.Division.DivisionCode;
//        if (Div == "CM" || Div == "DP" || Div == "BP" || Div == "TM" || Div == "CP" || Div == "SB")
//        {
//            P[31] = new ReportParameter("TermsandConditionHead", "TERMS & CONDITIONS:", false);
//            P[32] = new ReportParameter("PaymentTerms", "Payment Terms :", false);
//            P[33] = new ReportParameter("TermsOfPayment", TermsOfPayment + " along with order, balance payment against proforma invoice prior to dispatch . All payment to be made in favour of :", false);
//            if (DealerBank.Count > 0)
//            {
//                P[34] = new ReportParameter("PaymentTermAccName", "NAME                 : " + DealerBank[0].DealerName, false);
//                P[35] = new ReportParameter("PaymentTermBankName", "BANK NAME     : " + DealerBank[0].DealerBank.BankName, false);
//                P[36] = new ReportParameter("PaymentTermBankAddress", DealerBank[0].DealerBank.Branch);
//                P[37] = new ReportParameter("PaymentTermAccNo", "ACCOUNT NO.  : " + DealerBank[0].DealerBank.AcNumber, false);
//                P[38] = new ReportParameter("PaymentTermIFSCCode", "IFSC CODE         : " + DealerBank[0].DealerBank.IfscCode, false);
//            }
//            else
//            {
//                lblMessage.Text = "Bank Details Not Found";
//                lblMessage.Visible = true;
//                lblMessage.ForeColor = Color.Red;
//                return;
//            }
//            P[39] = new ReportParameter("Delivery", "Delivery : " + Delivery, false);
//            P[40] = new ReportParameter("TransitInsurance", "Transit Insurance: Customer to arrange and send the same before dispatch", false);
//            P[41] = new ReportParameter("Transportation", "Transportation : To-pay basis to customer's account.", false);
//            P[42] = new ReportParameter("Validity", "Validity :This offer is valid till : " + Validity, false);
//            P[43] = new ReportParameter("Note", "Note : " + QNote, false);
//        }
//        //else if (Div == "CP")
//        //{
//        //    P[31] = new ReportParameter("TermsandConditionHead", "", false);
//        //    P[32] = new ReportParameter("PaymentTerms", "", false);
//        //    P[33] = new ReportParameter("TermsOfPayment", "", false);
//        //    P[34] = new ReportParameter("PaymentTermAccName", "", false);
//        //    P[35] = new ReportParameter("PaymentTermBankName", "", false);
//        //    P[36] = new ReportParameter("PaymentTermBankAddress", "");
//        //    P[37] = new ReportParameter("PaymentTermAccNo", "", false);
//        //    P[38] = new ReportParameter("PaymentTermIFSCCode", "", false);
//        //    P[39] = new ReportParameter("Delivery", "", false);
//        //    P[40] = new ReportParameter("TransitInsurance", "", false);
//        //    P[41] = new ReportParameter("Transportation", "", false);
//        //    P[42] = new ReportParameter("Validity", "", false);
//        //    P[43] = new ReportParameter("Note", "", false);
//        //}
//        else if (!string.IsNullOrEmpty(Div))
//        {
//            lblMessage.Text = "Please Change First Line Item of Material";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }
//        else
//        {
//            lblMessage.Text = "Division Not Available";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }

//        P[44] = new ReportParameter("Name", Q.Lead.AssignedTo.ContactName, false);
//        P[45] = new ReportParameter("Designation", Q.Lead.AssignedTo.Designation.DealerDesignation, false);
//        P[46] = new ReportParameter("MobileNo", Q.Lead.AssignedTo.ContactNumber, false);



//        DataTable dtItem = new DataTable();
//        dtItem.Columns.Add("SNO");
//        dtItem.Columns.Add("Material");
//        dtItem.Columns.Add("Description");
//        dtItem.Columns.Add("HSN");
//        dtItem.Columns.Add("UOM");
//        dtItem.Columns.Add("Qty");
//        dtItem.Columns.Add("Rate");
//        dtItem.Columns.Add("Total");
//        dtItem.Columns.Add("Discount");
//        dtItem.Columns.Add("Value");
//        dtItem.Columns.Add("CGSTPer");
//        dtItem.Columns.Add("CGSTVal");
//        dtItem.Columns.Add("SGSTPer");
//        dtItem.Columns.Add("SGSTVal");

//        int i = 0;
//        decimal TaxSubTotal = 0;
//        decimal TCSSubTotal = 0;
//        decimal SubTotal = 0;
//        decimal Lifetimetax = 0;
//        decimal GrandTotal = 0;
//        foreach (PSalesQuotationItem item in Q.QuotationItems)
//        {
//            i = i + 1;
//            if (item.SGST != 0)
//            {
//                P[47] = new ReportParameter("CGST_Header", "CGST %", false);
//                P[48] = new ReportParameter("CGSTVal_Header", "CGST Value", false);
//                P[49] = new ReportParameter("SGST_Header", "SGST %", false);
//                P[50] = new ReportParameter("SGSTVal_Header", "SGST Value", false);
//                dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
//                    String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), item.SGST, String.Format("{0:n}", item.SGSTValue), item.SGST, String.Format("{0:n}", item.SGSTValue));

//                decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
//                decimal CGSTValues = (from x in Q.QuotationItems select x.CGSTValue).Sum();
//                decimal SGSTValues = (from x in Q.QuotationItems select x.SGSTValue).Sum();

//                TaxSubTotal = TaxableValues + CGSTValues + SGSTValues;
//                TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
//                SubTotal = TaxSubTotal + TCSSubTotal;
//                Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
//                GrandTotal = SubTotal + Lifetimetax;
//                P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
//                P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
//                P[14] = new ReportParameter("Tax", "", false);
//                P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
//                P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
//                P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
//                P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
//            }
//            else
//            {
//                P[47] = new ReportParameter("CGST_Header", "", false);
//                P[48] = new ReportParameter("CGSTVal_Header", "", false);
//                P[49] = new ReportParameter("SGST_Header", "IGST %", false);
//                P[50] = new ReportParameter("SGSTVal_Header", "IGST Value", false);
//                dtItem.Rows.Add(i, item.Material.MaterialCode, item.Material.MaterialDescription, item.Material.HSN, item.Material.BaseUnit, item.Qty,
//                    String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), null, null, item.IGST, String.Format("{0:n}", item.IGSTValue));

//                decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
//                decimal IGSTValues = (from x in Q.QuotationItems select x.IGSTValue).Sum();

//                TaxSubTotal = TaxableValues + IGSTValues;
//                TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
//                SubTotal = TaxSubTotal + TCSSubTotal;
//                Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
//                GrandTotal = SubTotal + Lifetimetax;
//                P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
//                P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
//                P[14] = new ReportParameter("Tax", "", false);
//                P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
//                P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
//                P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
//                P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
//            }
//        }
//        P[54] = new ReportParameter("TCSPer", Q.QuotationItems[0].TCSTax.ToString(), false);
//        Boolean Success = false;

//        Success = new BSalesQuotation().InsertSalesQuotationRevision(Q, CustomerAddress1, CustomerAddress2, CustomerAddressShipTo1, CustomerAddressShipTo2,
//            KindAttention, Hypothecation, Reference, TermsOfPayment, Delivery, QNote, Validity, Convert.ToDecimal(GrandTotal));
//        if (Success == false)
//        {
//            return;
//        }

//        PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
//        string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
//        string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

//        PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
//        string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
//        string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

//        //PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
//        if (Quotation.CommissionAgent)
//        {
//            P[51] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
//            P[52] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
//            P[53] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
//            P[55] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
//            P[56] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
//            P[57] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
//        }
//        else
//        {
//            P[51] = new ReportParameter("CompanyName", Dealer.CustomerFullName.ToUpper(), false);
//            P[52] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
//            P[53] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
//            P[55] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Ajax.GSTIN);
//            P[56] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
//            P[57] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
//        }

//        report.ReportPath = Server.MapPath("~/Print/SalesTaxQuotation.rdlc");
//        report.SetParameters(P);

//        ReportDataSource rds = new ReportDataSource();
//        rds.Name = "SalesTaxQuotationItem";//This refers to the dataset name in the RDLC file  
//        rds.Value = dtItem;
//        report.DataSources.Add(rds); ;

//        Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

//        Response.Buffer = true;
//        Response.Clear();
//        Response.ContentType = mimeType;
//        Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
//        Response.BinaryWrite(mybytes); // create the file
//        new BXcel().PdfDowload();
//        Response.Flush(); // send it to the client to download

//    }
//    catch (Exception ex)
//    {
//        lblMessage.Text = ex.Message.ToString();
//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;
//        return;
//    }
//}

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

//void fillEffort()
//{
//    gvEffort.DataSource = new BSalesQuotation().GetSalesQuotationEffort(Quotation.QuotationID, PSession.User.UserID);
//    gvEffort.DataBind();
//}
//void fillExpense()
//{
//    gvExpense.DataSource = new BSalesQuotation().GetSalesQuotationExpense(Quotation.QuotationID, PSession.User.UserID);
//    gvExpense.DataBind();


//}
//void ViewMachineQuotation()
//{
//    try
//    {
//        lblMessage.Text = "";
//        PSalesQuotation Q = Quotation;
//        if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
//        {
//            lblMessage.Text = "Quotation Not Generated...!";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }
//        string contentType = string.Empty;
//        contentType = "application/pdf";
//        var CC = CultureInfo.CurrentCulture;
//        Random r = new Random();
//        string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
//        string FileName = (Q.Lead.Dealer.DealerCode + "_MC_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
//        string extension;
//        string encoding;
//        string mimeType;
//        string[] streams;
//        Warning[] warnings;
//        LocalReport report = new LocalReport();
//        report.EnableExternalImages = true;

//        PDMS_Customer Customer = Q.Lead.Customer;
//        string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
//        string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

//        List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

//        PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
//        string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : ", " + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : ", " + Ajax.Address3)).Trim(',', ' ');
//        string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : ", " + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

//        PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
//        string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
//        string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

//        List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);

//        string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "", Subject = "";
//        foreach (PSalesQuotationNote Note in Q.Notes)
//        {
//            if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 10) { Subject = Note.Remark; }
//        }
//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;

//        //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
//        //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
//        //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
//        //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
//        //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
//        //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
//        //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }
//        //if (Subject == "") { lblMessage.Text = "Subject Not Found"; return; }

//        ReportParameter[] P = new ReportParameter[38];
//        //Q.Lead.Dealer.
//        P[0] = new ReportParameter("QuotationType", "MACHINE QUOTATION", false);
//        P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
//        P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
//        P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName/* Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2*/, false);
//        P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
//        P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
//        P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
//        P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
//        P[8] = new ReportParameter("Attention", KindAttention, false);
//        P[9] = new ReportParameter("Subject", Q.QuotationItems[0].Material.MaterialDescription, false);
//        P[10] = new ReportParameter("Reference", Reference, false);
//        P[11] = new ReportParameter("Annexure", "A-I", false);
//        P[12] = new ReportParameter("AnnexureRef", Q.SapQuotationNo, false);
//        P[13] = new ReportParameter("AnnexureDate", Q.SapQuotationDate.ToString(), false);
//        P[14] = new ReportParameter("TCSTax", "TCSTax Persent", false);
//        P[15] = new ReportParameter("Delivery", Delivery, false);
//        //P[16] = new ReportParameter("InWordsTotalAmount", "ZERO RUPEES Only", false);
//        //P[17] = new ReportParameter("TotalAmount", "00000.000", false);
//        P[18] = new ReportParameter("Note", QNote, false);
//        P[19] = new ReportParameter("WarrantyDeliveryHours", Q.QuotationItems[0].Material.Model.Division.WarrantyDeliveryHours, false);//2000
//        //if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "CM")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "2000", false);//2000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "DP")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "2000", false);//2000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "BP")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "CP")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "TM")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
//        //}
//        //else
//        //{
//        //    lblMessage.Text = "Division Not Available";
//        //    lblMessage.Visible = true;
//        //    lblMessage.ForeColor = Color.Red;
//        //    return;
//        //}

//        P[20] = new ReportParameter("ConcernName", Q.Lead.AssignedTo.ContactName, false);
//        P[21] = new ReportParameter("ConcernDesignation", Q.Lead.AssignedTo.Designation.DealerDesignation, false);
//        P[22] = new ReportParameter("ConcernMobile", Q.Lead.AssignedTo.ContactNumber, false);


//        DataTable dtItem = new DataTable();
//        dtItem.Columns.Add("TechnicalSpecification");
//        dtItem.Columns.Add("Units");
//        dtItem.Columns.Add("UnitPriceINR");
//        dtItem.Columns.Add("AmountINR");
//        dtItem.Columns.Add("MaterialText");
//        decimal GrandTotal = 0;
//        DataTable DTMaterialText = new DataTable();
//        for (int i = 0; i < Q.QuotationItems.Count(); i++)
//        {
//            try
//            {
//                DTMaterialText = new SQuotation().getMaterialTextForQuotation(Q.QuotationItems[i].Material.MaterialCode);
//            }
//            catch (Exception ex)
//            {
//                lblMessage.Text = ex.Message.ToString();
//                lblMessage.Visible = true;
//                lblMessage.ForeColor = Color.Red;
//            }
//            string MaterialText = string.Empty;
//            int sno = 0;
//            foreach (DataRow dr in DTMaterialText.Rows)
//            {
//                MaterialText += (sno == 0) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#"); sno++;
//            }
//            P[23] = new ReportParameter("MaterialText", "", false);


//            dtItem.Rows.Add(Q.QuotationItems[i].Material.MaterialDescription, Q.QuotationItems[i].Qty + " " + Q.QuotationItems[i].Material.BaseUnit, String.Format("{0:n}", Q.QuotationItems[i].Rate - Q.QuotationItems[i].Discount), String.Format("{0:n}", (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Q.QuotationItems[i].Discount), MaterialText);
//            GrandTotal += (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Convert.ToDecimal(Q.QuotationItems[i].Discount);

//            P[16] = new ReportParameter("InWordsTotalAmount", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
//            P[17] = new ReportParameter("TotalAmount", String.Format("{0:n}", GrandTotal.ToString()), false);

//        }

//        List<PPlant> Plant = new BDMS_Master().GetPlant(null, Q.QuotationItems[0].Plant.PlantCode);
//        string PlantAddress1 = (Plant[0].Address1 + (string.IsNullOrEmpty(Plant[0].Address2) ? "" : "," + Plant[0].Address2) + (string.IsNullOrEmpty(Plant[0].Address3) ? "" : "," + Plant[0].Address3)).Trim(',', ' ');
//        string PlantAddress2 = (Plant[0].City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Plant[0].State.State) + (string.IsNullOrEmpty(Plant[0].Country.Country) ? "" : "," + Plant[0].Country.Country)).Trim(',', ' ');

//        if (Q.QuotationItems[0].Plant.PlantCode == "P003")
//        {
//            P[24] = new ReportParameter("FactoryName", Plant[0].PlantName, false);
//            P[25] = new ReportParameter("FactoryAddress", PlantAddress1 + PlantAddress2, false);
//            P[26] = new ReportParameter("FactoryPhoneno", Ajax.Mobile, false);
//            P[27] = new ReportParameter("FactoryFax", "", false);
//            P[28] = new ReportParameter("FactoryWebsite", "www.ajax-engg.com", false);
//            P[29] = new ReportParameter("TCSTaxTerms", "If TCS is applicable, it will be calculated on sale consideration Plus GST.", false);
//        }
//        else /*if (Q.QuotationItems[0].Plant.PlantCode == "P001")*/
//        {
//            P[24] = new ReportParameter("FactoryName", Plant[0].PlantName, false);
//            P[25] = new ReportParameter("FactoryAddress", PlantAddress1 + PlantAddress2, false);
//            P[26] = new ReportParameter("FactoryPhoneno", Ajax.Mobile, false);
//            P[27] = new ReportParameter("FactoryFax", "", false);
//            P[28] = new ReportParameter("FactoryWebsite", "www.ajax-engg.com", false);
//            P[29] = new ReportParameter("TCSTaxTerms", "", false);
//        }

//        if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "BP")
//        {
//            P[30] = new ReportParameter("ErectionCommissoningHead", "ERECTION AND COMMISSONING :", false);
//            P[31] = new ReportParameter("ErectionCommissoning", "Erection and Commissioning will be in customer scope. Ajax shall be deputing service engineer for supervision of Erection and commissioning of the machine, on receipt of your confirmation of receipt of equipment and readiness of your site.The standard time for erection and commissioning is 1 day and additional 1 day for trail run &Training to your operation staff.The period of stay shall be restricted to 2 working days beyond that the services shall be on chargeable basis.Customer shall provide him all lodging, boarding & local conveyance facility.Customer shall provide all pulling tools, tackles, crane, skilled / unskilled labour, consumables like oil, welding machine, electrod etc., ", false);
//        }
//        else
//        {
//            P[30] = new ReportParameter("ErectionCommissoningHead", "", false);
//            P[31] = new ReportParameter("ErectionCommissoning", "", false);
//        }


//        if (Quotation.CommissionAgent)
//        {
//            P[32] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
//            P[33] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
//            P[34] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
//            P[35] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
//            P[36] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
//            P[37] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
//        }
//        else
//        {
//            P[32] = new ReportParameter("CompanyName", Dealer.CustomerFullName, false);
//            P[33] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
//            P[34] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
//            P[35] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Dealer.GSTIN);
//            P[36] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
//            P[37] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
//        }

//        //if (File.Exists(Server.MapPath("~/Print/SalesSpec_Machine_" + Q.QuotationItems[0].Material.MaterialCode + ".rdlc")))
//        //{
//        //    report.ReportPath = Server.MapPath("~/Print/SalesSpec_Machine_" + Q.QuotationItems[0].Material.MaterialCode + ".rdlc");
//        //}
//        //else
//        //{
//            report.ReportPath = Server.MapPath("~/Print/SalesMachineQuotation.rdlc");
//        //}
//        report.SetParameters(P);
//        ReportDataSource rds = new ReportDataSource();
//        rds.Name = "SalesQuotationItem";//This refers to the dataset name in the RDLC file  
//        rds.Value = dtItem;
//        report.DataSources.Add(rds); ;

//        Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

//        //Response.Buffer = true;
//        //Response.Clear();
//        //Response.ContentType = mimeType;
//        //Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
//        //Response.BinaryWrite(mybytes); // create the file
//        //Response.Flush(); // send it to the client to download
//        var uploadPath = Server.MapPath("~/Backup");
//        var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
//        File.WriteAllBytes(tempfilenameandlocation, mybytes);
//        Response.Redirect("../PDF.aspx?FileName=" + FileName + "&Title=Pre-Sales » Quotation", false);
//    }
//    catch (Exception ex)
//    {
//        lblMessage.Text = ex.Message.ToString();
//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;
//        return;
//    }
//}
//void DownloadMachineQuotation()
//{
//    try
//    {
//        lblMessage.Text = "";
//        PSalesQuotation Q = Quotation;
//        if (string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
//        {
//            lblMessage.Text = "Quotation Not Generated...!";
//            lblMessage.Visible = true;
//            lblMessage.ForeColor = Color.Red;
//            return;
//        }
//        string contentType = string.Empty;
//        contentType = "application/pdf";
//        var CC = CultureInfo.CurrentCulture;
//        Random r = new Random();
//        string CustomerName = ((Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Length > 20) ? (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2).Substring(0, 20) : (Q.Lead.Customer.CustomerName + "_" + Q.Lead.Customer.CustomerName2);
//        string FileName = (Q.Lead.Dealer.DealerCode + "_MC_" + CustomerName + "_" + Q.Lead.Customer.CustomerCode + "_" + Q.Model.Model + "_" + Convert.ToDateTime(Q.SapQuotationDate).ToString("dd.MM.yyyy") + ".pdf").Replace("&", "");
//        string extension;
//        string encoding;
//        string mimeType;
//        string[] streams;
//        Warning[] warnings;
//        LocalReport report = new LocalReport();
//        report.EnableExternalImages = true;

//        PDMS_Customer Customer = Q.Lead.Customer;
//        string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
//        string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

//        List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

//        PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
//        string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : ", " + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : ", " + Ajax.Address3)).Trim(',', ' ');
//        string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : ", " + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

//        PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
//        string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
//        string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

//        List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);

//        string Reference = "", KindAttention = "", QNote = "", Hypothecation = "", TermsOfPayment = "", Delivery = "", Validity = "", Foc = "", MarginMoney = "", Subject = "";
//        foreach (PSalesQuotationNote Note in Q.Notes)
//        {
//            if (Note.Note.SalesQuotationNoteListID == 1) { Reference = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 2) { KindAttention = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 3) { QNote = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 4) { Hypothecation = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 5) { TermsOfPayment = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 6) { Delivery = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 7) { Validity = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 8) { Foc = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 9) { MarginMoney = Note.Remark; }
//            if (Note.Note.SalesQuotationNoteListID == 10) { Subject = Note.Remark; }
//        }
//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;

//        //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
//        //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
//        //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
//        //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
//        //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
//        //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
//        //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }
//        //if (Subject == "") { lblMessage.Text = "Subject Not Found"; return; }

//        ReportParameter[] P = new ReportParameter[38];
//        //Q.Lead.Dealer.
//        P[0] = new ReportParameter("QuotationType", "MACHINE QUOTATION", false);
//        P[1] = new ReportParameter("QuotationNo", Q.SapQuotationNo, false);
//        P[2] = new ReportParameter("QuotationDate", Q.SapQuotationDate.ToString(), false);
//        P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerFullName/* + " " + Q.Lead.Customer.CustomerName2*/, false);
//        P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
//        P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
//        P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
//        P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
//        P[8] = new ReportParameter("Attention", KindAttention, false);
//        P[9] = new ReportParameter("Subject", Q.QuotationItems[0].Material.MaterialDescription, false);
//        P[10] = new ReportParameter("Reference", Reference, false);
//        P[11] = new ReportParameter("Annexure", "A-I", false);
//        P[12] = new ReportParameter("AnnexureRef", Q.SapQuotationNo, false);
//        P[13] = new ReportParameter("AnnexureDate", Q.SapQuotationDate.ToString(), false);
//        P[14] = new ReportParameter("TCSTax", "TCSTax Persent", false);
//        P[15] = new ReportParameter("Delivery", Delivery, false);
//        //P[16] = new ReportParameter("InWordsTotalAmount", "ZERO RUPEES Only", false);
//        //P[17] = new ReportParameter("TotalAmount", "00000.000", false);
//        P[18] = new ReportParameter("Note", QNote, false);
//        P[19] = new ReportParameter("WarrantyDeliveryHours", Q.QuotationItems[0].Material.Model.Division.WarrantyDeliveryHours, false);//2000
//        //if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "CM")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "2000", false);//2000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "DP")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "2000", false);//2000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "BP")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "CP")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
//        //}
//        //else if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "TM")
//        //{
//        //    P[19] = new ReportParameter("WarrantyDeliveryHours", "1000", false);//1000
//        //}
//        //else
//        //{
//        //    lblMessage.Text = "Division Not Available";
//        //    lblMessage.Visible = true;
//        //    lblMessage.ForeColor = Color.Red;
//        //    return;
//        //}

//        P[20] = new ReportParameter("ConcernName", Q.Lead.AssignedTo.ContactName, false);
//        P[21] = new ReportParameter("ConcernDesignation", Q.Lead.AssignedTo.Designation.DealerDesignation, false);
//        P[22] = new ReportParameter("ConcernMobile", Q.Lead.AssignedTo.ContactNumber, false);


//        DataTable dtItem = new DataTable();
//        dtItem.Columns.Add("TechnicalSpecification");
//        dtItem.Columns.Add("Units");
//        dtItem.Columns.Add("UnitPriceINR");
//        dtItem.Columns.Add("AmountINR");
//        dtItem.Columns.Add("MaterialText");
//        decimal GrandTotal = 0;
//        DataTable DTMaterialText = new DataTable();
//        for (int i = 0; i < Q.QuotationItems.Count(); i++)
//        {
//            try
//            {
//                DTMaterialText = new SQuotation().getMaterialTextForQuotation(Q.QuotationItems[i].Material.MaterialCode);
//            }
//            catch (Exception ex)
//            {
//                lblMessage.Text = ex.Message.ToString();
//                lblMessage.Visible = true;
//                lblMessage.ForeColor = Color.Red;
//            }
//            string MaterialText = string.Empty;
//            int sno = 0;
//            foreach (DataRow dr in DTMaterialText.Rows)
//            {
//                MaterialText += (sno == 0) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#"); sno++;
//            }
//            P[23] = new ReportParameter("MaterialText", "", false);


//            dtItem.Rows.Add(Q.QuotationItems[i].Material.MaterialDescription, Q.QuotationItems[i].Qty + " " + Q.QuotationItems[i].Material.BaseUnit, String.Format("{0:n}", Q.QuotationItems[i].Rate - Q.QuotationItems[i].Discount), String.Format("{0:n}", (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Q.QuotationItems[i].Discount), MaterialText);
//            GrandTotal += (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Convert.ToDecimal(Q.QuotationItems[i].Discount);

//            P[16] = new ReportParameter("InWordsTotalAmount", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
//            P[17] = new ReportParameter("TotalAmount", String.Format("{0:n}", GrandTotal.ToString()), false);

//        }
//        List<PPlant> Plant = new BDMS_Master().GetPlant(null, Q.QuotationItems[0].Plant.PlantCode);
//        string PlantAddress1 = (Plant[0].Address1 + (string.IsNullOrEmpty(Plant[0].Address2) ? "" : "," + Plant[0].Address2) + (string.IsNullOrEmpty(Plant[0].Address3) ? "" : "," + Plant[0].Address3)).Trim(',', ' ');
//        string PlantAddress2 = (Plant[0].City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Plant[0].State.State) + (string.IsNullOrEmpty(Plant[0].Country.Country) ? "" : "," + Plant[0].Country.Country)).Trim(',', ' ');

//        if (Q.QuotationItems[0].Plant.PlantCode == "P003")
//        {
//            P[24] = new ReportParameter("FactoryName", Plant[0].PlantName, false);
//            P[25] = new ReportParameter("FactoryAddress", PlantAddress1 + PlantAddress2, false);
//            P[26] = new ReportParameter("FactoryPhoneno", Ajax.Mobile, false);
//            P[27] = new ReportParameter("FactoryFax", "", false);
//            P[28] = new ReportParameter("FactoryWebsite", "www.ajax-engg.com", false);
//            P[29] = new ReportParameter("TCSTaxTerms", "If TCS is applicable, it will be calculated on sale consideration Plus GST.", false);
//        }
//        else /*if (Q.QuotationItems[0].Plant.PlantCode == "P001")*/
//        {
//            P[24] = new ReportParameter("FactoryName", Plant[0].PlantName, false);
//            P[25] = new ReportParameter("FactoryAddress", PlantAddress1 + PlantAddress2, false);
//            P[26] = new ReportParameter("FactoryPhoneno", Ajax.Mobile, false);
//            P[27] = new ReportParameter("FactoryFax", "", false);
//            P[28] = new ReportParameter("FactoryWebsite", "www.ajax-engg.com", false);
//            P[29] = new ReportParameter("TCSTaxTerms", "", false);
//        }

//        if (Q.QuotationItems[0].Material.Model.Division.DivisionCode == "BP")
//        {
//            P[30] = new ReportParameter("ErectionCommissoningHead", "ERECTION AND COMMISSONING :", false);
//            P[31] = new ReportParameter("ErectionCommissoning", "Erection and Commissioning will be in customer scope. Ajax shall be deputing service engineer for supervision of Erection and commissioning of the machine, on receipt of your confirmation of receipt of equipment and readiness of your site.The standard time for erection and commissioning is 1 day and additional 1 day for trail run &Training to your operation staff.The period of stay shall be restricted to 2 working days beyond that the services shall be on chargeable basis.Customer shall provide him all lodging, boarding & local conveyance facility.Customer shall provide all pulling tools, tackles, crane, skilled / unskilled labour, consumables like oil, welding machine, electrod etc., ", false);
//        }
//        else
//        {
//            P[30] = new ReportParameter("ErectionCommissoningHead", "", false);
//            P[31] = new ReportParameter("ErectionCommissoning", "", false);
//        }


//        if (Quotation.CommissionAgent)
//        {
//            P[32] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
//            P[33] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
//            P[34] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
//            P[35] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
//            P[36] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
//            P[37] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
//        }
//        else
//        {
//            P[32] = new ReportParameter("CompanyName", Dealer.CustomerFullName, false);
//            P[33] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
//            P[34] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
//            P[35] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Dealer.GSTIN);
//            P[36] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
//            P[37] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
//        }


//        report.ReportPath = Server.MapPath("~/Print/SalesMachineQuotation.rdlc");
//        report.SetParameters(P);
//        ReportDataSource rds = new ReportDataSource();
//        rds.Name = "SalesQuotationItem";//This refers to the dataset name in the RDLC file  
//        rds.Value = dtItem;
//        report.DataSources.Add(rds);

//        Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

//        Response.Buffer = true;
//        Response.Clear();
//        Response.ContentType = mimeType;
//        Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
//        Response.BinaryWrite(mybytes); // create the file
//        new BXcel().PdfDowload();
//        Response.Flush(); // send it to the client to download


//    }
//    catch (Exception ex)
//    {
//        lblMessage.Text = ex.Message.ToString();
//        lblMessage.Visible = true;
//        lblMessage.ForeColor = Color.Red;
//        return;
//    }
//}