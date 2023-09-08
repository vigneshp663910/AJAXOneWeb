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
        //public PLead Lead
        //{
        //    get
        //    {
        //        if (Session["LeadView"] == null)
        //        {
        //            Session["LeadView"] = new PLead();
        //        }
        //        return (PLead)Session["LeadView"];
        //    }
        //    set
        //    {
        //        Session["LeadView"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {    
            }
        }
        public void FillMaster(PLead Lead)
        {
            txtValidFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtValidTo.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            txtPricingDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtRequestedDeliveryDate.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            //FillGetDealerOffice(); 
            // new DDLBind(ddlQuotationType, new BSalesQuotation().GetSalesQuotationType(null, null), "QuotationType", "QuotationTypeID");
            new DDLBind(ddlStatus, new BSalesQuotation().GetSalesQuotationStatus(null, null), "SalesQuotationStatus", "SalesQuotationStatusID");

            new DDLBind(ddlPriceGroup, new BDMS_Master().GetPriceGroup(null, null, null), "PriceGroupCode", "PriceGroupID");
            ddlPriceGroup.SelectedValue = "1";

            new DDLBind(ddlUserStatus, new BSalesQuotation().GetSalesQuotationUserStatus(null, null), "SalesQuotationUserStatus", "SalesQuotationUserStatusID",false);

            new DDLBind(ddlUserStatusRemarks, new BSalesQuotation().GetSaleQuotationRejectionReason(null, null), "Reason", "SalesQuotationRejectionReasonID");

            //new DDLBind(ddlBankName, new BDMS_Master().GetBankName(null, null), "BankName", "BankNameID");
             
            new DDLBind(ddlShipParty, new BDMS_Customer().GetCustomerShopTo(null, Lead.Customer.CustomerID), "CustomerCode", "CustomerShipToID");
        }


        public PSalesQuotation_Insert ReadSalesQuotation()
        {
            PSalesQuotation_Insert Sq = new PSalesQuotation_Insert();
            //Sq.QuotationType = new PSalesQuotationType() { QuotationTypeID = Convert.ToInt32(ddlQuotationType.SelectedValue) };
          //  Sq.Status = new PSalesQuotationStatus() { SalesQuotationStatusID = Convert.ToInt32(ddlStatus.SelectedValue) };
            Sq.ValidFrom = string.IsNullOrEmpty(txtValidFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtValidFrom.Text.Trim());
            Sq.ValidTo = string.IsNullOrEmpty(txtValidTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtValidTo.Text.Trim());
            Sq.PricingDate = string.IsNullOrEmpty(txtPricingDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPricingDate.Text.Trim());
            Sq.PriceGroup = ddlPriceGroup.SelectedValue == "0" ? null : new PPriceGroup() { PriceGroupID = Convert.ToInt32(ddlPriceGroup.SelectedValue) };
            Sq.SalesQuotationUserStatusID = ddlUserStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlUserStatus.SelectedValue);

            Sq.SalesQuotationRejectionReasonID = ddlUserStatusRemarks.SelectedValue == "0" ? (int?)null :    Convert.ToInt32(ddlUserStatusRemarks.SelectedValue);
          
            Sq.LifeTimeTax = string.IsNullOrEmpty(txtLifeTimeTax.Text.Trim()) ? 0 : Convert.ToDecimal(txtLifeTimeTax.Text.Trim());
          
            Sq.RequestedDeliveryDate = string.IsNullOrEmpty(txtRequestedDeliveryDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDeliveryDate.Text.Trim());
            Sq.CommissionAgent = cbCommissionAgent.Checked;
            Sq.CustomerShipToID = ddlShipParty.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlShipParty.SelectedValue);
            if (rbIsStandardYes.Checked)
            {
                Sq.IsStandard = true;
            }
            else
            { 
                Sq.IsStandard = false; 
            }
            return Sq;
        }
        public string ValidationSalesQuotation()
        {
            string Message = "";
            //ddlQuotationType.BorderColor = Color.Silver;
            ddlStatus.BorderColor = Color.Silver;

            //if (ddlQuotationType.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Quotation Type";
            //    ddlQuotationType.BorderColor = Color.Red;
            //}
            //else if (ddlStatus.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Quotation Status";
            //    ddlStatus.BorderColor = Color.Red;
            //}

            //if (string.IsNullOrEmpty(txtLifeTimeTax.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Life Time Tax Percentage";
            //    txtLifeTimeTax.BorderColor = Color.Red;
            //}

            //if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Remark";
            //    txtRemark.BorderColor = Color.Red;
            //}
            return Message;
        }
        public void FillQuotation(PSalesQuotation Sq)
        {
            //ddlQuotationType.SelectedValue = Convert.ToString(Sq.QuotationType.QuotationTypeID);
            ddlStatus.SelectedValue = Convert.ToString(Sq.Status.SalesQuotationStatusID);
            txtValidFrom.Text = Sq.ValidFrom == null ? "" : ((DateTime)Sq.ValidFrom).ToString("yyyy-MM-dd");
            txtValidTo.Text = Sq.ValidTo == null ? "" : ((DateTime)Sq.ValidTo).ToString("yyyy-MM-dd"); 
            txtPricingDate.Text = Sq.PricingDate == null ? "" : ((DateTime)Sq.PricingDate).ToString("yyyy-MM-dd");  
            txtRequestedDeliveryDate.Text = Sq.RequestedDeliveryDate == null ? "" : ((DateTime)Sq.RequestedDeliveryDate).ToString("yyyy-MM-dd"); 
            cbCommissionAgent.Checked = Sq.CommissionAgent;
            ddlPriceGroup.SelectedValue = Sq.PriceGroup == null ? "0" : Convert.ToString(Sq.PriceGroup.PriceGroupID);
            ddlUserStatus.SelectedValue = Sq.UserStatus == null ? "0" : Convert.ToString(Sq.UserStatus.SalesQuotationUserStatusID);
            ddlUserStatusRemarks.SelectedValue = Sq.UserStatusRemarks == null ? "0" : Convert.ToString(Sq.UserStatusRemarks.SalesQuotationRejectionReasonID);
            ddlShipParty.SelectedValue = Sq.ShipTo == null ? "0" : Convert.ToString(Sq.ShipTo.CustomerShipToID);

            txtLifeTimeTax.Text = Convert.ToString(Sq.LifeTimeTax);

            if (Sq.IsStandard)
            {
                rbIsStandardYes.Checked = true;
            }
            else
            {
                rbIsStandardNo.Checked = true;
            }

           
            if(Sq.Lead.ProductType.ProductTypeID !=3 )
            {
                divStandardProduct.Visible = false;
            }
        }

    }
}