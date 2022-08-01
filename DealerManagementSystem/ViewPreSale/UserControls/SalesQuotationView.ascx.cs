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
                string Message = ValidationSalesQuotationGenerate();
                lblMessage.Visible = true;
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PApiResult Results = new BSalesQuotation().CreateQuotationInSapAndPartsPortal(Quotation.QuotationID);
                
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
            else if (lbActions.Text == "Print Machine Quotation")
            {
                PrintMachineQuotation();
            }
            else if (lbActions.Text == "Print Tax Quotation")
            {
                PrintTaxQuotation();
            }
            else if (lbActions.Text == "Add Visit")
            {
                MPE_Visit.Show();
                new DDLBind(ddlActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
                new DDLBind(ddlImportance, new BDMS_Master().GetImportance(null, null), "Importance", "ImportanceID");
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
                PSalesQuotationItem MaterialTax = new SQuotation().getMaterialTaxForQuotation(Quotation, Material, IsWarrenty, Qty);



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

                MaterialTax.Plant = new PPlant() { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
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
                if (MaterialTax.TCSValue == 0)
                {
                    lblMessageProduct.Text = "TCS Tax value not found this material..!";
                    return;
                }
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
            PSalesQuotation Sq = new PSalesQuotation();
            Sq = UC_Quotation.ReadSalesQuotation();
            Sq.QuotationID = Quotation.QuotationID;
            Sq.Lead = new PLead { LeadID = Quotation.Lead.LeadID };
            Sq.CreatedBy = new PUser() { UserID = PSession.User.UserID };
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

            lblTotalEffort.Text = Convert.ToString(Quotation.TotalEffort);
            lblTotalExpense.Text = Convert.ToString(Quotation.TotalExpense);
            cbCommissionAgent.Checked = Quotation.CommissionAgent;
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

            fillShifTo();
            fillVisit();
            fillSalesCommissionClaim();
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
        void GenerateQuotation(PSalesQuotationItem QuotationItem)
        {
            try
            {
                PApiResult Results = new BSalesQuotation().CreateQuotationInSapAndPartsPortal(Quotation.QuotationID);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageProduct.Text = Results.Message;
                    lblMessageProduct.Visible = true;
                    lblMessageProduct.ForeColor = Color.Red;
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                fillViewQuotation(Quotation.QuotationID);
                lblMessage.Text = "Updated Successfully";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                //if (Quotation.CommissionAgent)
                //{
                //QuotationItemSAp(QuotationItem);
                //PSalesQuotation Q = Quotation;
                //List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);
                //List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
                //Q.Lead.Dealer.AuthorityName = DealerBank[0].AuthorityName;
                //Q.Lead.Dealer.AuthorityDesignation = DealerBank[0].AuthorityDesignation;
                //Q.Lead.Dealer.AuthorityMobile = DealerBank[0].AuthorityMobile;
                //List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, 2, Q.QuotationID);
                //DateTime VisitDate;
                //VisitDate = (Visit.Count != 0) ? Visit[0].ColdVisitDate : Q.RefQuotationDate;                    
                //if (Q.QuotationItems.Count > 0 && leadProducts.Count > 0 && Q.Competitor.Count > 0)
                //{
                //    //list[0] as Subrc [1] as Number [2] as Type [3] as Message [4] as QuotationNo [5] as QuotationDate
                //    List<string> list = new SQuotation().getQuotationIntegration(Q, leadProducts, VisitDate, QuotationItem);
                //    if (list != null)
                //    {

                //        if ((list[2] == "S") || (list[0] == "0"))
                //        {
                //            lblMessage.Text = (list[3].ToString() == "") ? "Record Was Updated" : list[3].ToString();
                //            lblMessage.Visible = true;
                //            lblMessage.ForeColor = Color.Green;
                //            if (!string.IsNullOrEmpty(list[4]) && !string.IsNullOrEmpty(list[5]))
                //            {
                //                string endPoint = "SalesQuotation/UpdateSalesQuotationNumber?SalesQuotationID=" + Quotation.QuotationID + "&QuotationNo=" + list[4] + "&QuotationDate=" + list[5] + "";
                //                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                //            }
                //        }
                //        else
                //        {
                //            lblMessage.Text = list[3].ToString() + Environment.NewLine + "\n";
                //            lblMessage.Visible = true;
                //            lblMessage.ForeColor = Color.Red;
                //        }
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = "Quotation Not Generated Successfully...!";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Red;
                //}
                //}
                //else
                //{
                //    QuotationItemSAp(QuotationItem);
                //    if (string.IsNullOrEmpty(Quotation.PgQuotationNo))
                //    {
                //        // PApiResult Results = new BSalesQuotation().CreateQuotationInPartsPortal(Quotation.QuotationID);
                //        PApiResult Results = new BSalesQuotation().CreateQuotationInSapAndPartsPortal(Quotation.QuotationID); 
                //        if (Results.Status == PApplication.Failure)
                //        {
                //            lblMessageProduct.Text = Results.Message;
                //            return;
                //        }
                //    }
                //    fillViewQuotation(Quotation.QuotationID);
                //    lblMessage.Text = "Updated Successfully";
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = Color.Green;
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }

        }
        void QuotationItemSAp(PSalesQuotationItem QuotationItem)
        {
            PSalesQuotation Q = Quotation;
            List<PLeadProduct> leadProducts = new BLead().GetLeadProduct(Q.Lead.LeadID, PSession.User.UserID);
            List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);
            Q.Lead.Dealer.AuthorityName = DealerBank[0].AuthorityName;
            Q.Lead.Dealer.AuthorityDesignation = DealerBank[0].AuthorityDesignation;
            Q.Lead.Dealer.AuthorityMobile = DealerBank[0].AuthorityMobile;
            List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, 2, Q.QuotationID, null, null, null);
            DateTime VisitDate;
            VisitDate = (Visit.Count != 0) ? Visit[0].ColdVisitDate : Q.RefQuotationDate;
            if (Q.QuotationItems.Count > 0 && leadProducts.Count > 0 && Q.Competitor.Count > 0)
            {
                //list[0] as Subrc [1] as Number [2] as Type [3] as Message [4] as QuotationNo [5] as QuotationDate
                List<string> list = new SQuotation().getQuotationIntegration(Q, leadProducts, VisitDate, QuotationItem);
                if (list != null)
                {

                    if ((list[2] == "S") || (list[0] == "0"))
                    {
                        lblMessage.Text = (list[3].ToString() == "") ? "Record Was Updated" : list[3].ToString();
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Green;
                        if (!string.IsNullOrEmpty(list[4]) && !string.IsNullOrEmpty(list[5]))
                        {
                            string endPoint = "SalesQuotation/UpdateSalesQuotationNumber?SalesQuotationID=" + Quotation.QuotationID + "&QuotationNo=" + list[4] + "&QuotationDate=" + list[5] + "";
                            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                        }
                    }
                    else
                    {
                        lblMessage.Text = list[3].ToString() + Environment.NewLine + "\n";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Quotation Not Generated Successfully...!";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        void PrintMachineQuotation()
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;
                if(string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
                {
                    lblMessage.Text = "Quotation Not Generated...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
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
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

                PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
                string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : ", " + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : ", " + Ajax.Address3)).Trim(',', ' ');
                string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : ", " + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
                string DealerCustomerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerCustomerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

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

                //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
                //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
                //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
                //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
                //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
                //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
                //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }
                //if (Subject == "") { lblMessage.Text = "Subject Not Found"; return; }

                ReportParameter[] P = new ReportParameter[38];
                //Q.Lead.Dealer.
                P[0] = new ReportParameter("QuotationType", "MACHINE QUOTATION", false);
                P[1] = new ReportParameter("QuotationNo", Q.CommissionAgent ? Q.SapQuotationNo : Q.PgQuotationNo, false);
                P[2] = new ReportParameter("QuotationDate", Q.CommissionAgent ? Q.SapQuotationDate.ToString() : Q.PgQuotationDate.ToString(), false);
                P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2, false);
                P[4] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[5] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[6] = new ReportParameter("Mobile", Q.Lead.Customer.Mobile, false);
                P[7] = new ReportParameter("EMail", Q.Lead.Customer.Email, false);
                P[8] = new ReportParameter("Attention", KindAttention, false);
                P[9] = new ReportParameter("Subject", Q.QuotationItems[0].Material.MaterialDescription, false);
                P[10] = new ReportParameter("Reference", Reference, false);
                P[11] = new ReportParameter("Annexure", "A-I", false);
                P[12] = new ReportParameter("AnnexureRef", Q.CommissionAgent ? Q.SapQuotationNo : Q.PgQuotationNo, false);
                P[13] = new ReportParameter("AnnexureDate", Q.CommissionAgent ? Q.SapQuotationDate.ToString() : Q.SapQuotationDate.ToString(), false);
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
                DataTable DTMaterialText = new DataTable();
                try
                {
                    DTMaterialText = new SQuotation().getMaterialTextForQuotation("L.900.508");
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                }
                string MaterialText = string.Empty;
                int sno = 0;
                foreach (DataRow dr in DTMaterialText.Rows)
                {
                    MaterialText += (sno == 0) ? dr["TDLINE"].ToString().Replace("•", "#") : "\n" + dr["TDLINE"].ToString().Replace("•", "#"); sno++;
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
                else /*if (Q.QuotationItems[0].Plant.PlantCode == "P001")*/
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


                if (Quotation.CommissionAgent)
                {
                    P[32] = new ReportParameter("CompanyName", Ajax.CustomerName.ToUpper(), false);
                    P[33] = new ReportParameter("CompanyAddress1", AjaxCustomerAddress1, false);
                    P[34] = new ReportParameter("CompanyAddress2", AjaxCustomerAddress2, false);
                    P[35] = new ReportParameter("CompanyCINandGST", "CIN : " + Ajax.CIN + ", GST : " + Ajax.GSTIN);
                    P[36] = new ReportParameter("CompanyPAN", "PAN : " + Ajax.PAN + ", T : " + Ajax.Mobile);
                    P[37] = new ReportParameter("CompanyTelephoneandEmail", "Email : " + Ajax.Email + ", Web : " + Ajax.Web);
                }
                else
                {
                    P[32] = new ReportParameter("CompanyName", Dealer.CustomerFullName, false);
                    P[33] = new ReportParameter("CompanyAddress1", DealerCustomerAddress1, false);
                    P[34] = new ReportParameter("CompanyAddress2", DealerCustomerAddress2, false);
                    P[35] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Dealer.GSTIN);
                    P[36] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
                    P[37] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
                }

                DataTable dtItem = new DataTable();
                dtItem.Columns.Add("TechnicalSpecification");
                dtItem.Columns.Add("Units");
                dtItem.Columns.Add("UnitPriceINR");
                dtItem.Columns.Add("AmountINR");
                decimal GrandTotal = 0;
                for (int i = 0; i < Q.QuotationItems.Count(); i++)
                {
                    dtItem.Rows.Add(Q.QuotationItems[i].Material.MaterialDescription, Q.QuotationItems[i].Qty + " " + Q.QuotationItems[i].Material.BaseUnit, String.Format("{0:n}", Q.QuotationItems[i].Rate - Q.QuotationItems[i].Discount), String.Format("{0:n}", (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Q.QuotationItems[i].Discount));
                    GrandTotal += (Q.QuotationItems[i].Qty * Q.QuotationItems[i].Rate) - Convert.ToDecimal(Q.QuotationItems[i].Discount);

                    P[16] = new ReportParameter("InWordsTotalAmount", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                    P[17] = new ReportParameter("TotalAmount", String.Format("{0:n}", GrandTotal.ToString()), false);
                }
                report.ReportPath = Server.MapPath("~/Print/SalesMachineQuotation.rdlc");
                report.SetParameters(P);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesQuotationItem";//This refers to the dataset name in the RDLC file  
                rds.Value = dtItem;
                report.DataSources.Add(rds); ;

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                //Response.BinaryWrite(mybytes); // create the file
                //Response.Flush(); // send it to the client to download
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
        void PrintTaxQuotation()
        {
            try
            {
                lblMessage.Text = "";
                PSalesQuotation Q = Quotation;
                if(string.IsNullOrEmpty(Q.SapQuotationNo) && string.IsNullOrEmpty(Q.PgQuotationNo))
                {
                    lblMessage.Text = "Quotation Not Generated...!";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
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
                ReportParameter[] P = new ReportParameter[58];

                PDMS_Customer Customer = Q.Lead.Customer;
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : ", " + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : ", " + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : ", " + Customer.State.State) + (string.IsNullOrEmpty(Customer.Country.Country) ? "" : ", " + Customer.Country.Country) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');


                PDMS_Customer CustomerShipTo = new PDMS_Customer();
                if (Q.ShipTo != null) { CustomerShipTo = CustomerShipTo = new BDMS_Customer().GetCustomerByID(Q.ShipTo.CustomerID); } else { CustomerShipTo = Q.Lead.Customer; }
            ;
                string CustomerAddressShipTo1 = (CustomerShipTo.Address1 + (string.IsNullOrEmpty(CustomerShipTo.Address2) ? "" : ", " + CustomerShipTo.Address2) + (string.IsNullOrEmpty(CustomerShipTo.Address3) ? "" : ", " + CustomerShipTo.Address3)).Trim(',', ' ');
                string CustomerAddressShipTo2 = (CustomerShipTo.City + (string.IsNullOrEmpty(CustomerShipTo.State.State) ? "" : ", " + CustomerShipTo.State.State) + (string.IsNullOrEmpty(CustomerShipTo.Country.Country) ? "" : ", " + CustomerShipTo.Country.Country) + (string.IsNullOrEmpty(CustomerShipTo.Pincode) ? "" : "-" + CustomerShipTo.Pincode)).Trim(',', ' ');

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

                //if (Reference == "") { lblMessage.Text = "Reference Not Found"; return; }
                //if (KindAttention == "") { lblMessage.Text = "KindAttention Not Found"; return; }
                //if (QNote == "") { lblMessage.Text = "Note Not Found"; return; }
                //if (Hypothecation == "") { lblMessage.Text = "Hypothecation Not Found"; return; }
                //if (TermsOfPayment == "") { lblMessage.Text = "TermsOfPayment Not Found"; return; }
                //if (Delivery == "") { lblMessage.Text = "Delivery Not Found"; return; }
                //if (Validity == "") { lblMessage.Text = "Validity Not Found"; return; }

                List<PDMS_Dealer> DealerBank = new BDMS_Dealer().GetDealerBankDetails(null, Q.Lead.Dealer.DealerCode, null);

                P[0] = new ReportParameter("QuotationType", "TAX QUOTATION", false);
                P[1] = new ReportParameter("QuotationNo", Q.CommissionAgent ? Q.SapQuotationNo : Q.PgQuotationNo, false);
                P[2] = new ReportParameter("QuotationDate", Q.CommissionAgent ? Q.SapQuotationDate.ToString() : Q.PgQuotationDate.ToString(), false);
                P[3] = new ReportParameter("CustomerName", Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2, false);
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
                P[21] = new ReportParameter("ShipToCustomerName", Q.Lead.Customer.CustomerName + " " + Q.Lead.Customer.CustomerName2, false);
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
                else if (!string.IsNullOrEmpty(Div))
                {
                    lblMessage.Text = "Please Change First Line Item of Material";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
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
                            String.Format("{0:n}", item.TaxableValue / item.Qty), String.Format("{0:n}", item.TaxableValue), item.Discount, String.Format("{0:n}", item.TaxableValue), item.SGST, String.Format("{0:n}", item.SGSTValue), item.SGST, String.Format("{0:n}", item.SGSTValue));

                        decimal TaxableValues = (from x in Q.QuotationItems select x.TaxableValue).Sum();
                        decimal CGSTValues = (from x in Q.QuotationItems select x.CGSTValue).Sum();
                        decimal SGSTValues = (from x in Q.QuotationItems select x.SGSTValue).Sum();

                        TaxSubTotal = TaxableValues + CGSTValues + SGSTValues;
                        TCSSubTotal = TaxSubTotal * item.TCSTax / 100;// Q.TCSValue;
                        SubTotal = TaxSubTotal + TCSSubTotal;
                        Lifetimetax = SubTotal * Q.LifeTimeTax / 100;//Q.LifeTimeValue;
                        GrandTotal = SubTotal + Lifetimetax;
                        P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                        P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
                        P[14] = new ReportParameter("Tax", "", false);
                        P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
                        P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
                        P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
                        P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
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
                        P[12] = new ReportParameter("AmountInWord", new BDMS_Fn().NumbersToWords(Convert.ToInt32(GrandTotal)), false);
                        P[13] = new ReportParameter("TotalAmount", String.Format("{0:n}", TaxSubTotal), false);
                        P[14] = new ReportParameter("Tax", "", false);
                        P[15] = new ReportParameter("TCS", String.Format("{0:n}", TCSSubTotal), false);
                        P[16] = new ReportParameter("SubTotal", String.Format("{0:n}", SubTotal), false);
                        P[17] = new ReportParameter("LifeTimeTax", String.Format("{0:n}", Lifetimetax), false);
                        P[18] = new ReportParameter("GrandTotal", String.Format("{0:n}", GrandTotal), false);
                    }
                }
                P[54] = new ReportParameter("TCSPer", Q.QuotationItems[0].TCSTax.ToString(), false);
                Boolean Success = false;

                Success = new BSalesQuotation().InsertSalesQuotationRevision(Q, CustomerAddress1, CustomerAddress2, CustomerAddressShipTo1, CustomerAddressShipTo2,
                    KindAttention, Hypothecation, Reference, TermsOfPayment, Delivery, QNote, Validity, Convert.ToDecimal(GrandTotal));
                if (Success == false)
                {
                    return;
                }

                PDMS_Customer Ajax = new BDMS_Customer().GetCustomerAE();
                string AjaxCustomerAddress1 = (Ajax.Address1 + (string.IsNullOrEmpty(Ajax.Address2) ? "" : "," + Ajax.Address2) + (string.IsNullOrEmpty(Ajax.Address3) ? "" : "," + Ajax.Address3)).Trim(',', ' ');
                string AjaxCustomerAddress2 = (Ajax.City + (string.IsNullOrEmpty(Ajax.State.State) ? "" : "," + Ajax.State.State) + (string.IsNullOrEmpty(Ajax.Pincode) ? "" : "-" + Ajax.Pincode)).Trim(',', ' ');

                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Q.Lead.Dealer.DealerCode);
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
                    P[55] = new ReportParameter("CompanyCINandGST", "CIN:" + Dealer.PAN + ",GST:" + Ajax.GSTIN);
                    P[56] = new ReportParameter("CompanyPAN", "PAN:" + Dealer.PAN);
                    P[57] = new ReportParameter("CompanyTelephoneandEmail", "T:" + Dealer.Mobile + ",Email:" + Dealer.Email);
                }

                report.ReportPath = Server.MapPath("~/Print/SalesTaxQuotation.rdlc");
                report.SetParameters(P);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "SalesTaxQuotationItem";//This refers to the dataset name in the RDLC file  
                rds.Value = dtItem;
                report.DataSources.Add(rds); ;

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                //Response.BinaryWrite(mybytes); // create the file
                //Response.Flush(); // send it to the client to download
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
            //  lbtnPrintMachineQuotation.Visible = true;


            //lbtnEditQuotation.Visible = false;
            //lbtnEditFinancier.Visible = false;
            //lbtnAddProduct.Visible = false;
            //lbtnAddCompetitor.Visible = false;
            //lbtnAddQuotationNote.Visible = false;
            //lbtnAddFollowUp.Visible = false;
            //lbtnAddEffort.Visible = false;
            //lbtnAddExpense.Visible = false;
            //lbtnGenerateQuotation.Visible = false;
            //lbtnPrintMachineQuotation.Visible = false;

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
            PColdVisit ColdVisitList = new PColdVisit();
            lblMessageColdVisit.ForeColor = Color.Red;
            lblMessageColdVisit.Visible = true;
            string Message = "";

            Message = ValidationColdVisit();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageColdVisit.Text = Message;
                return;
            }
            ColdVisitList.Customer = new PDMS_Customer() { CustomerID = Quotation.Lead.Customer.CustomerID };
            ColdVisitList.ColdVisitDate = Convert.ToDateTime(txtColdVisitDate.Text.Trim());
            ColdVisitList.ActionType = new PActionType() { ActionTypeID = Convert.ToInt32(ddlActionType.SelectedValue) };
            ColdVisitList.Importance = new PImportance() { ImportanceID = Convert.ToInt32(ddlImportance.SelectedValue) };
            ColdVisitList.Remark = txtVisitRemark.Text.Trim();
            ColdVisitList.Location = txtLocation.Text.Trim();
            ColdVisitList.ReferenceID = Quotation.QuotationID;
            ColdVisitList.ReferenceTableID = 2;
            ColdVisitList.CreatedBy = new PUser { UserID = PSession.User.UserID };

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
            List<PColdVisit> Visit = new BColdVisit().GetColdVisit(null, null, null, null, null, null, null, null, null, 2, Quotation.QuotationID, null, null, null);
            gvVisit.DataSource = Visit;
            gvVisit.DataBind();
        }
        public string ValidationColdVisit()
        {
            string Message = "";
            txtColdVisitDate.BorderColor = Color.Silver;
            txtVisitRemark.BorderColor = Color.Silver;
            ddlActionType.BorderColor = Color.Silver;
            if (string.IsNullOrEmpty(txtColdVisitDate.Text.Trim()))
            {
                Message = "Please enter the Cold Visit Date";
                txtColdVisitDate.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = Message + "Please enter the Location";
                txtLocation.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtVisitRemark.Text.Trim()))
            {
                Message = Message + "Please enter the Remark";
                txtVisitRemark.BorderColor = Color.Red;
            }

            else if (ddlActionType.SelectedValue == "0")
            {
                Message = Message + "Please select the Action Type";
                ddlActionType.BorderColor = Color.Red;
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
            if (Quotation.Competitor.Count == 0)
            {
                return "Please update the Competitor";
            }
            List<PLeadQuestionaries> Questionaries = new BLead().GetLeadQuestionaries(Quotation.Lead.LeadID);
            if (Questionaries.Count == 0)
            {
                return "Please Add the Questionaries";
            }
            return Message;
        }

    }
}