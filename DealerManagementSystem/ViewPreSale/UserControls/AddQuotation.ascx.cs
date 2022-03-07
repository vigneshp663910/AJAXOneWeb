using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using SapIntegration;
using System.Drawing;
using Newtonsoft.Json;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class AddQuotation : System.Web.UI.UserControl
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
        public PSalesQuotation PSO
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
        //private List<PDMS_ServiceMaterial> PSM
        //{
        //    get
        //    {
        //        if (ViewState["PSM"] == null)
        //        {
        //            ViewState["PSM"] = new List<PDMS_ServiceMaterial>();
        //        }
        //        return (List<PDMS_ServiceMaterial>)ViewState["PSM"];
        //    }
        //    set
        //    {
        //        ViewState["PSM"] = value;
        //    }
        //}
       

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                 
            }
        }

        public void FillMaster()
        {
            FillGetDealerOffice();
     


           

            new DDLBind(ddlQuotationType, new BSalesQuotation().GetSalesQuotationType(null, null), "QuotationType", "QuotationTypeID");
            new DDLBind(ddlStatus, new BSalesQuotation().GetSalesQuotationStatus(null, null), "Status", "SaleQuotationStatusID");

            new DDLBind(ddlPriceGroup, new BDMS_Master().GetPriceGroup(null, null, null), "PriceGroupCode", "PriceGroupID");
            

            new DDLBind(ddlUserStatus, new BSalesQuotation().GetSalesQuotationUserStatus(null, null), "Status", "SalesQuotationUserStatusID");

            new DDLBind(ddlUserStatusRemarks, new BSalesQuotation().GetSaleQuotationRejectionReason(null, null), "Reason", "SalesQuotationRejectionReasonID");

            //new DDLBind(ddlBankName, new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");


           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //List<PDMS_WebQuotation> PSOs = new BDMS_WebQuotation().GetWebQuotationByID(null, txtPurchaseOrder.Text.Trim());
            //if (PSOs.Count != 0)
            //{
            //    PSO = PSOs[0];
            //    fillPrimarySalesOrderEdit();
            //    pnlAllowM.Enabled = true;
            //}
            //else
            //{
            //    fillPrimarySalesOrderCreate();
            //}
        }

       
        void fillPrimarySalesOrderEdit()
        {
            //ddlBankName.Text = PSO.Financier == null ? "0" : Convert.ToString(PSO.Financier.QuotationFinancierID);
            //txtDoNumber.Text = PSO.Financier.DoNumber;
            //txtDoDate.Text = Convert.ToString(PSO.Financier.DoDate);
            //ddlIncoterms.SelectedValue = PSO.Financier.IncoTerm == null ? "0" : Convert.ToString(PSO.Financier.IncoTerm.IncoTermID);

            //txtAdvanceAmount.Text = Convert.ToString(PSO.Financier.AdvanceAmount);
            //txtFinancierAmount.Text = Convert.ToString(PSO.Financier.FinancierAmount);

          

        }

 

        protected void btnSaveBasicInformation_Click(object sender, EventArgs e)
        {
            //if (!ValidationBasicInformation())
            //{
            //    return;
            //} 

            //PSO.Customer = new PDMS_Customer() { CustomerCode = txtCustomer.Text.Trim() };
            //PSO.BillTo = new PDMS_Customer() { CustomerCode = txtBillTo.Text.Trim() }; 


            //PSO.Customer = new PDMS_Customer() { CustomerID = Customer.CustomerID };
            //PSO.BillTo = new PDMS_Customer() { CustomerID = Customer.CustomerID };


            //PSO.ShipTo = new PDMS_Customer() { CustomerCode = txtShipTo.Text.Trim() };



            PSO.QuotationID = new BSalesQuotation().InsertOrUpdateSalesQuotationBasicInformation(PSO);
            if (PSO.QuotationID != 0)
            {
                //lblBasicInformationMessage.Text = "Sales Order is updated successfully";
                //lblBasicInformationMessage.ForeColor = Color.Green;
 
            }
            else
            {
                //lblBasicInformationMessage.Text = "Sales Order is not updated successfully";
                //lblBasicInformationMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSaveFinanceInformation_Click(object sender, EventArgs e)
        {
            //if (!ValidationFinanceInformation1())
            //{
            //    return;
            //}

            //PSO.ShipTo.CustomerCode = txtShipTo.Text.Trim();


            //Financier 
            //PSO.Financier = ddlBankName.SelectedValue == "0" ? null : new PSalesQuotationFinancier() { QuotationFinancierID = Convert.ToInt32(ddlBankName.SelectedValue) };

            //PSO.Financier.DoNumber = txtDoNumber.Text.Trim();
            //PSO.Financier.DoDate = string.IsNullOrEmpty(txtDoDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDoDate.Text.Trim());

            //PSO.Financier.IncoTerm = ddlIncoterms.SelectedValue == "0" ? null : new PDMS_IncoTerm() { IncoTermID = Convert.ToInt32(ddlIncoterms.SelectedValue) };
            //PSO.Financier.AdvanceAmount = string.IsNullOrEmpty(txtAdvanceAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
            //PSO.Financier.FinancierAmount = string.IsNullOrEmpty(txtFinancierAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtFinancierAmount.Text.Trim());

            if (new BSalesQuotation().InsertOrUpdateSalesQuotationFinanceInformation(PSO))
            {
                //lblFinanceInformationMessage.Text = "Sales Order is updated successfully";
                //lblFinanceInformationMessage.ForeColor = Color.Green;
            }
            else
            {
                //lblFinanceInformationMessage.Text = "Sales Order is not updated successfully";
                //lblFinanceInformationMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSaveSalesInformation_Click(object sender, EventArgs e)
        {

            //PSO.DiscountSales = string.IsNullOrEmpty(txtDiscountSales.Text.Trim()) ? 0 : Convert.ToDecimal(txtDiscountSales.Text.Trim());
            //PSO.FreightValue = string.IsNullOrEmpty(txtFreightValue.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtFreightValue.Text.Trim());
            //PSO.InsuranceValue = string.IsNullOrEmpty(txtInsuranceValue.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtInsuranceValue.Text.Trim());
            //PSO.TRDate = string.IsNullOrEmpty(txtTRDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTRDate.Text.Trim());
            //PSO.ConsolidationInvoicePrint = Convert.ToBoolean(cbConsolidationInvoicePrint.Checked);
            //PSO.FreightAmount = string.IsNullOrEmpty(txtFreightAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtFreightAmount.Text.Trim());
            //PSO.Billing = ddlBilling.SelectedValue;


            if (new BSalesQuotation().InsertOrUpdateSalesQuotationSalesInformation(PSO))
            {
                //lblSalesInformationMessage.Text = "Sales Order is updated successfully";
                //lblSalesInformationMessage.ForeColor = Color.Green;
            }
            else
            {
                //lblSalesInformationMessage.Text = "Sales Order is not updated successfully";
                //lblSalesInformationMessage.ForeColor = Color.Red;
            }
        }
      
      
      
        private void FillGetDealerOffice()
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            //  ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }

       
        public PSalesQuotation ReadSalesQuotation()
        {
            PSalesQuotation Sq = new PSalesQuotation();
            Sq.Lead = new PLead { LeadID = Lead.LeadID };
            Sq.QuotationType = new PSalesQuotationType() { QuotationTypeID = Convert.ToInt32(ddlQuotationType.SelectedValue) };
            Sq.Status = new PSalesQuotationStatus() { SalesQuotationStatusID = Convert.ToInt32(ddlStatus.SelectedValue) };
            Sq.ValidFrom = Convert.ToDateTime(txtValidFrom.Text.Trim());
            Sq.ValidTo = Convert.ToDateTime(txtValidTo.Text.Trim());
            Sq.PricingDate = Convert.ToDateTime(txtPricingDate.Text.Trim());
            Sq.PriceGroup = new PPriceGroup() { PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedValue) };
            Sq.UserStatus = new PSalesQuotationUserStatus() { SalesQuotationUserStatusID = Convert.ToInt32(ddlUserStatus.SelectedValue) };
            Sq.UserStatusRemarks = new PSaleQuotationRejectionReason() { SalesQuotationRejectionReasonID = Convert.ToInt32(ddlUserStatusRemarks.SelectedValue) };

            Sq.CreatedBy = new PUser { UserID = PSession.User.UserID };

            return Sq;
        }



    }
}