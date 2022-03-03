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

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class SalesQuotationView : System.Web.UI.UserControl
    {
        private PLead Lead
        {
            get
            {
                if (Session["PLead"] == null)
                {
                    Session["PLead"] = new PLead();
                }
                return (PLead)Session["PLead"];
            }
            set
            {
                Session["PLead"] = value;
            }
        }
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

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Financier", Sqf)).Data);
            if (s == "false")
            {
                lblMessageFinancier.Text = "Something went wrong try again";
                return;
            }
            Quotation.Financier = Sqf;
            MPE_Financier.Hide();
            tbpCust.ActiveTabIndex = 0;
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
    
        public void fillViewQuotation(long QuotationID)
        {
            Quotation = new BSalesQuotation().GetSalesQuotationByID(QuotationID);

            lblRefQuotationNo.Text = Quotation.RefQuotationNo;
            lblRefQuotationDate.Text = Convert.ToString(Quotation.RefQuotationDate);
            lblQuotationNumber.Text = Quotation.QuotationNo;
            lblQuotationDate.Text = Convert.ToString(Quotation.QuotationDate);
            lblQuotationType.Text = Quotation.QuotationType.QuotationType;
            lblQuotationStatus.Text = Quotation.Status.SalesQuotationStatus;
            lblValidFrom.Text = Convert.ToString(Quotation.ValidFrom);
            lblValidTo.Text = Convert.ToString(Quotation.ValidTo);
            lblPricingDate.Text = Convert.ToString(Quotation.PricingDate);

            lblPriceGroup.Text = Quotation.RefQuotationNo;
            lblUserStatus.Text = Quotation.RefQuotationNo;
            lblLeadNumber.Text = Quotation.RefQuotationNo;
            lblLeadDate.Text = Quotation.RefQuotationNo;
            lblDealer.Text = Quotation.RefQuotationNo;
            lblRemarks.Text = Quotation.RefQuotationNo;
            lblCustomer.Text = Quotation.RefQuotationNo;

            lblContactPerson.Text = Quotation.RefQuotationNo;
            lblMobile.Text = Quotation.RefQuotationNo;

            lblEmail.Text = Quotation.RefQuotationNo;
            lblLocation.Text = Quotation.RefQuotationNo;
            lblImportance.Text = Quotation.RefQuotationNo;



            //this.LeadID = LeadID;
            //PLeadSearch S = new PLeadSearch();
            //S.LeadID = LeadID;
            //Lead = new BLead().GetLead(S)[0];
            //lblLeadNumber.Text = Lead.LeadNumber;
            //lblLeadDate.Text = Lead.LeadDate.ToLongDateString();
            //lblCategory.Text = Lead.Category.Category;
            //lblProgressStatus.Text = Lead.ProgressStatus.ProgressStatus;
            //lblQualification.Text = Lead.Qualification.Qualification;
            //lblSource.Text = Lead.Source.Source;
            //lblStatus.Text = Lead.Status.Status;
            //lblType.Text = Lead.Type.Type;
            //lblDealer.Text = Lead.Dealer.DealerCode;
            //lblRemarks.Text = Lead.Remarks;
            //lblCustomer.Text = Lead.Customer.CustomerFullName;
            //lblContactPerson.Text = Lead.Customer.ContactPerson;
            //lblMobile.Text = Lead.Customer.Mobile;
            //lblEmail.Text = Lead.Customer.Email;


            fillFinancier();
            fillProduct();
            fillCompetitor();
            fillNote();
        }
       
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Quotation Basic Info")
            {
                //MPE_Customer.Show();
                //PDMS_Customer Customer = new PDMS_Customer();
                //Customer = new BDMS_Customer().GetCustomer(CustomerID, "", "", null, null, null, null)[0];
                //UC_Customer.FillMaster();
                //UC_Customer.FillCustomer(Customer);
            }
            else if (lbActions.Text == "Update Financier Info")
            {
                new DDLBind(ddlBankName, new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");
                new DDLBind(ddlIncoterms, new BDMS_Master().GetIncoterms(null, null, null), "IncoTerms", "IncoTermsID");
                new DDLBind(ddlPaymentTerms, new BDMS_Master().GetPaymentTerms(null, null, null), "PaymentTerms", "PaymentTermsID");
               if( Quotation.Financier != null)
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
                new DDLBind(ddlMake, new BDMS_Master().GetMake(null, null), "Make", "MakeID");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
                new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null), "Product", "ProductID");
                MPE_Competitor.Show();
            }
            else if (lbActions.Text == "Add Quotation Note")
            {
                new DDLBind(ddlNote, new BSalesQuotation().GetSaleQuotationNoteList(null, null), "Note", "SalesQuotationNoteListID");
                MPE_Note.Show();
            }

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

            string OrderType = "";
            string Customer = Quotation.Lead.Customer.CustomerCode;
            string Vendor = "";
            string IV_SEC_SALES = "X";
            string PRICEDATE = "";
            Boolean IsWarrenty = false;
            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            //{
            //    OrderType = "DEFAULT_SEC_AUART";
            //    Customer = SDMS_ICTicket.Customer.CustomerCode;
            //    Vendor = SDMS_ICTicket.Dealer.DealerCode;
            //}
              
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material)[0];
            if (string.IsNullOrEmpty(MM.MaterialCode))
            {
                lblMessageProduct.Text = "Please check the Material";
                return;
            }
            decimal Qty = Convert.ToDecimal(txtQty.Text);
            PDMS_ServiceMaterial MaterialTax = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, Material, Qty, IV_SEC_SALES, PRICEDATE, IsWarrenty);
             
            //if (MaterialTax.BasePrice <= 0)
            //{
            //    lblMessageProduct.Text = "Please maintain the price for Material " + Material + " in SAP"; 
            //    return;
            //} 
             
            PSalesQuotationItem Item = new PSalesQuotationItem();
            Item.Material = new PDMS_Material();
            Item.Material.MaterialCode = MM.MaterialCode;
            Item.Material.MaterialID = MM.MaterialID;
            //Item.Material.MaterialDescription = MM.MaterialDescription;

            Item.Plant = new PPlant() { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) }; 
            Item.Qty = Convert.ToInt32(txtQty.Text);
            Item.Rate = MaterialTax.BasePrice;
            Item.Discount = Convert.ToDecimal(txtDiscount.Text);
            Item.TaxableValue = (MaterialTax.BasePrice * Convert.ToDecimal(txtQty.Text)) - Convert.ToDecimal(txtDiscount.Text);

            Item.CGST = MaterialTax.SGST;
            Item.SGST = MaterialTax.SGST;
            Item.IGST = MaterialTax.IGST;
            Item.CGSTValue = MaterialTax.SGSTValue;
            Item.SGSTValue = MaterialTax.SGSTValue;
            Item.IGSTValue = MaterialTax.IGSTValue;
            Item.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/QuotationItem", Item)).Data);
            if (s == "false")
            {
                lblMessageFinancier.Text = "Something went wrong try again";
                return;
            }
            //  Quotation.QuotationItems = Item;
            MPE_Product.Hide();
            tbpCust.ActiveTabIndex = 1;
            fillProduct();
        }

   

        protected void btnCompetitorSave_Click(object sender, EventArgs e)
        {
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

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Competitor", Sqf)).Data);
            if (s == "false")
            {
                lblMessageCompetitor.Text = "Something went wrong try again";
                return;
            }
            
            MPE_Competitor.Hide();
            tbpCust.ActiveTabIndex = 2;
            fillViewQuotation(Quotation.QuotationID);
        }

        public void fillFinancier()
        {
            lblBankName.Text = Quotation.Financier.BankName.BankName;
            lblIncoTerms.Text = Quotation.Financier.IncoTerms.IncoTerm_Description;
            lblPaymentTerms.Text = Quotation.Financier.PaymentTerms.PaymentTerms;

            lblDoNumber.Text = Quotation.Financier.DoNumber;
            lblDoDate.Text = Convert.ToString(Quotation.Financier.DoDate);
            lblAdvanceAmount.Text = Convert.ToString(Quotation.Financier.AdvanceAmount);
            lblFinancierAmount.Text = Convert.ToString(Quotation.Financier.FinancierAmount);
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

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesQuotation/Note", Sqf)).Data);
            if (s == "false")
            {
                lblMessageNote.Text = "Something went wrong try again";
                return;
            }

            MPE_Note.Hide();
            tbpCust.ActiveTabIndex = 3;
            fillViewQuotation(Quotation.QuotationID);
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
    }
}