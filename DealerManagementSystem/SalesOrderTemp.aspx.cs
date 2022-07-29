using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class SalesOrderTemp : System.Web.UI.Page
    {
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
            fillViewQuotation(78);
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
            lblModeofbilling.Text = "";
            lblSapQtnNumber.Text = Quotation.SapQuotationNo;
            lblCustomerCode.Text = Quotation.Lead.Customer.CustomerCode;
            lblTitle.Text = Quotation.Lead.Customer.Title.Title;
            string KindAttention = "", hypothecation = "";
            foreach (PSalesQuotationNote Note in Quotation.Notes)
            {
                if (Note.Note.SalesQuotationNoteListID == 2)
                {
                    KindAttention = Note.Note.Note;
                }
                if (Note.Note.SalesQuotationNoteListID == 4)
                {
                    hypothecation = Note.Note.Note;
                }
            }
            lblInvoicenameandaddress.Text = Quotation.Lead.Customer.CustomerFullName + "," + "<br>" + Quotation.Lead.Customer.Address1 + "," + "<br>" + Quotation.Lead.Customer.Address2 + "," + "<br>" + Quotation.Lead.Customer.Address3 + "," + "<br>" + Quotation.Lead.Customer.District.District + "-" + Quotation.Lead.Customer.Pincode + "," + "<br>" + Quotation.Lead.Customer.State.State + "," + "<br>" + "Ph. No / Mobile :" + Quotation.Lead.Customer.Mobile + "/" + Quotation.Lead.Customer.AlternativeMobile + "," + "<br>" + "Kind Attn.: " + KindAttention;
            lblDeliveryAddress.Text = Quotation.Lead.Customer.CustomerFullName + "," + "<br>" + Quotation.Lead.Customer.Address1 + "," + "<br>" + Quotation.Lead.Customer.Address2 + "," + "<br>" + Quotation.Lead.Customer.Address3 + "," + "<br>" + Quotation.Lead.Customer.District.District + "-" + Quotation.Lead.Customer.Pincode + "," + "<br>" + Quotation.Lead.Customer.State.State + "," + "<br>" + "Ph. No / Mobile :" + Quotation.Lead.Customer.Mobile + "/" + Quotation.Lead.Customer.AlternativeMobile + "," + "<br>" + "Kind Attn.: " + KindAttention;
            lblgstnumber.Text = Quotation.Lead.Customer.GSTIN;
            lblpannumber.Text = Quotation.Lead.Customer.PAN;
            lblponumberanddate.Text = "";
            lblbasicprice.Text = "";
            lblhypothecation.Text = hypothecation;
            lblbacktobackdoendorsedtoajax.Text = "";
            lbldonumber.Text = "";
            lbldodate.Text = "";
            lbldoamount.Text = "";
            lblcreditdays.Text = "";
            lblinvoicevalue.Text = "";
            lblmodel.Text = Quotation.Model.Model;
            lblspecialrequirement1.Text = "";
            lblspecialrequirement2.Text = "";
            lblspecialrequirement3.Text = "";
            lblspecialrequirement4.Text = "";
            lblspecialrequirement5.Text = "";
            lblmachineqty.Text = "";
            lblfocservicekit.Text = "";
            lblfocwheelassy.Text = "";
            lblfocextensionchutes.Text = "";
            lblfocothers.Text = "";
            lblsourceofenquiry.Text = Quotation.Lead.Source.Source;
            lblreasonfororderconversion.Text = "";
            lblcustomertype.Text = "";
            lblprofile.Text = "";
            lblsize.Text = "";
            lblownershippattern.Text = "";
            lblapplication.Text = "";
            lblnameoftheproject.Text = "";
            lbltransportationandinsurance.Text = "";
            lblsalesregion.Text = "";
            lblsalesoffice.Text = "";
            lbldealercode.Text = Quotation.Lead.Dealer.DealerCode;
            lbldealername.Text = Quotation.Lead.Dealer.DealerName;
        }
    }
}