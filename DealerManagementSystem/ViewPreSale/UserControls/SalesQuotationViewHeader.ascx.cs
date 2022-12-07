using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class SalesQuotationViewHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillViewQuotation(PSalesQuotation Quotation)
        { 
            if (Quotation.QuotationID == 0)
            { 
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

            //lblTotalEffort.Text = Convert.ToString(Quotation.TotalEffort);
            //lblTotalExpense.Text = Convert.ToString(Quotation.TotalExpense);
        }
    }
}