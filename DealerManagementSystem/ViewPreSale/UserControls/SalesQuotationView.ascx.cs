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
                if (Session["AddQuotation"] == null)
                {
                    Session["AddQuotation"] = new PSalesQuotation();
                }
                return (PSalesQuotation)Session["AddQuotation"];
            }
            set
            {
                Session["AddQuotation"] = value;
            }
        }
        private List<PSalesQuotationItem> PSOItem
        {
            get
            {
                if (ViewState["PLSO"] == null)
                {
                    ViewState["PLSO"] = new List<PSalesQuotationItem>();
                }
                return (List<PSalesQuotationItem>)ViewState["PLSO"];
            }
            set
            {
                ViewState["PLSO"] = value;
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

            List<PSalesQuotationItem> PrimarySOItem = new List<PSalesQuotationItem>();
            if (PrimarySOItem.Count == 0)
            {
                PSalesQuotationItem N = new PSalesQuotationItem();
                PrimarySOItem.Add(N);
            }
            gvMaterial.DataSource = PrimarySOItem;
            gvMaterial.DataBind();
        }
        protected void btnFinancier_Click(object sender, EventArgs e)
        {
            PSalesQuotationFinancier Sqf = new PSalesQuotationFinancier();
            Sqf.QuotationID = Quotation.QuotationID;

            Sqf.BankName = new PBankName() { BankNameID = Convert.ToInt32(ddlBankName.SelectedValue) };
            Sqf.IncoTerm = new PDMS_IncoTerm() { IncoTermID = Convert.ToInt32(ddlIncoterms.SelectedValue) };
            Sqf.CreditDays = new PDMS_PaymentTerm() { PaymentTermID = Convert.ToInt32(ddlPaymentTerms.SelectedValue) };

            Sqf.DoNumber = txtDoNumber.Text.Trim();
            Sqf.DoDate = Convert.ToDateTime(txtDoDate.Text.Trim());

            Sqf.AdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
            Sqf.FinancierAmount = Convert.ToDecimal(txtFinancierAmount.Text.Trim());
        }

        public void FillMaterial()
        {
            List<PDMS_WebQuotationItem> Item = new BDMS_WebQuotation().GetWebQuotationItem(Quotation.QuotationID, null);
            //if (Item.Count==0)
            //{
            //    Item.Add(new PDMS_PrimarySalesOrderItem());
            //}
            gvMaterial.DataSource = Item;
            gvMaterial.DataBind();
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            PDMS_WebQuotationItem Item = new PDMS_WebQuotationItem();
            Item.WebQuotationItemID = Convert.ToInt64(gvMaterial.DataKeys[gvRow.RowIndex].Value);

            if (new BDMS_WebQuotation().InsertOrUpdateWebQuotationItem(Item))
            {
                lblMessage.Text = "Material is Removed successfully";
                lblMessage.ForeColor = Color.Green;
                FillMaterial();
            }
            else
            {
                lblMessage.Text = "Material is not Removed successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lblMaterialAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            //  btnGenerateQuotation.Focus();
            string Material = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterial")).Text.Trim();
            if (string.IsNullOrEmpty(Material))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }

            TextBox txtQty = (TextBox)gvMaterial.FooterRow.FindControl("txtQty");
            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Qty";
                return;
            }
            TextBox txtDiscount = (TextBox)gvMaterial.FooterRow.FindControl("txtDiscount");
            if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Qty";
                return;
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                lblMessage.Text = "Please enter correct format in Qty";
                return;
            }
            //     string 
            Material = Material.Split(' ')[0];
            string MaterialOrg = Material;

            //CheckBox cbSupersedeYN = (CheckBox)gvMaterial.FooterRow.FindControl("cbSupersedeYN");
            //if (cbSupersedeYN.Checked)
            //{
            //    string smaterial = Material;
            //    do
            //    {
            //        Material = smaterial;
            //        string query = "select s.p_smaterial from af_m_supersede s left join af_m_materials mm on mm.p_material = s.p_rmaterial left join af_m_materials ms on ms.p_material = s.p_smaterial   where s.valid_from <= Now() and s.valid_to >= Now() and  p_rmaterial = '" + smaterial + "'";
            //        smaterial = new NpgsqlServer().ExecuteScalar(query);
            //    } while (!string.IsNullOrEmpty(smaterial));
            //}

            //for (int i = 0; i < gvMaterial.Rows.Count; i++)
            //{
            //    Label lblMaterialCode = (Label)gvMaterial.Rows[i].FindControl("lblMaterialCode");
            //    Label lblCancel = (Label)gvMaterial.Rows[i].FindControl("lblCancel");
            //    if ((lblMaterialCode.Text == Material) && (lblCancel.Visible == false))
            //    {
            //        if (MaterialOrg != Material)
            //        {
            //            lblMessage.Text = "Part Number " + MaterialOrg + "is superseded with " + Material + "  and already available";
            //        }
            //        else
            //        {
            //            lblMessage.Text = "Material " + Material + " already available";
            //        }
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //}

            string OrderType = "";
            string Customer = "";
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
            //else
            //{
            //    OrderType = "301_DSSOR_SALES_ORDER_HDR";
            //    Customer = SDMS_ICTicket.Dealer.DealerCode;
            //    Vendor = "9031";
            //    IsWarrenty = true;
            //}

            decimal Available = 0;
            Boolean IsIGST = true;
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material)[0];
            if (string.IsNullOrEmpty(MM.MaterialCode))
            {
                lblMessage.Text = "Please check the Material";
                return;
            }
            decimal Qty = Convert.ToDecimal(txtQty.Text);
            PDMS_ServiceMaterial MaterialTax = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, Material, Qty, IV_SEC_SALES, PRICEDATE, IsWarrenty);


            PDMS_Material MM_SQL = new BDMS_Material().GetMaterialListSQL(null, Material)[0];
            Available = Convert.ToDecimal(txtQty.Text);

            PDMS_Customer Dealer = new SCustomer().getCustomerAddress("9001");
            PDMS_Customer CustomerP = new SCustomer().getCustomerAddress("343559");

            MaterialTax.BasePrice = MM_SQL.CurrentPrice;
            if (MaterialTax.BasePrice <= 0)
            {
                lblMessage.Text = "Please maintain the price for Material " + Material + " in SAP";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            MaterialTax.TaxablePrice = (MaterialTax.BasePrice * Convert.ToDecimal(txtQty.Text)) - Convert.ToDecimal(txtDiscount.Text);

            PSOItem = new List<PSalesQuotationItem>();
            PSalesQuotationItem Item = new PSalesQuotationItem();
            Item.Material = new PDMS_Material();
            Item.Material.MaterialCode = MM.MaterialCode;
            Item.Material.MaterialDescription = MM.MaterialDescription;

            Item.Plant = new PPlant() { PlantCode = "" };
            Item.Unit = 1;
            Item.Qty = Convert.ToInt32(txtQty.Text);
            Item.BasicPrice = MaterialTax.BasePrice;
            Item.Discount = Convert.ToDecimal(txtDiscount.Text);
            Item.TaxableValue = MaterialTax.TaxablePrice;

            if (Dealer.State.StateCode == CustomerP.State.StateCode)
            {
                IsIGST = false;
                MaterialTax.IGST = MM_SQL.TaxPercentage;
                MaterialTax.IGSTValue = MM_SQL.TaxPercentage * MaterialTax.TaxablePrice * 2 / 100;
                Item.TaxPersent = MaterialTax.IGST;
                Item.TaxValue = MaterialTax.IGSTValue;
            }
            else
            {
                MaterialTax.SGST = MM_SQL.TaxPercentage;
                MaterialTax.SGSTValue = MM_SQL.TaxPercentage * MaterialTax.TaxablePrice / 100;
                Item.TaxPersent = MaterialTax.SGST;
                Item.TaxValue = MaterialTax.SGSTValue;
            }
            Item.NetValue = Item.TaxValue;
            PSOItem.Add(Item);
            gvMaterial.DataSource = PSOItem;
            gvMaterial.DataBind();
        }
        public void fillViewQuotation(long QuotationID)
        {
            PSalesQuotation Quotation = new BSalesQuotation().GetSalesQuotationByID(QuotationID);
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

            //string Location = Lead.Customer.Address1 + ", " + Lead.Customer.Address2 + ", " + Lead.Customer.District.District + ", " + Lead.Customer.State.State;
            //lblLocation.Text = Location;

            //fillAssignSalesEngineer(LeadID);
            //fillFollowUp(LeadID);
            //fillConvocation(LeadID);
            //fillFinancial(LeadID);
            //fillEffort(LeadID);
            //fillExpense(LeadID);
            //fillProduct(LeadID);
        }
    }
}