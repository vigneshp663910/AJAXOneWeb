using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class PurchaseOrderCreate : System.Web.UI.UserControl
    {
        public List<PPurchaseOrderItem> PurchaseOrderItem
        {
            get
            {
                if (ViewState["PurchaseOrderCreateItem"] == null)
                {
                    ViewState["PurchaseOrderCreateItem"] = new List<PPurchaseOrderItem>();
                }
                return (List<PPurchaseOrderItem>)ViewState["PurchaseOrderCreateItem"];
            }
            set
            {
                ViewState["PurchaseOrderCreateItem"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        {
            fillDealer();
            new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
            fillVendor();
            // new DDLBind(ddlVendor, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
            new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID");
            //ddlDealerOffice
            Clear();
            fillItem();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.Text == "Add Material")
                {
                    MPE_AddMaterial.Show(); 
                } 
                else if (lbActions.Text == "Upload Material")
                {
                     
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        void Clear()
        {
            //txtCustomerName.Text = string.Empty;
            ////  txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //txtPersonName.Text = string.Empty;
            //txtMobile.Text = string.Empty;
            //txtMail.Text = string.Empty;
            //ddlSource.Items.Clear();
            //ddlCountry.Items.Clear();
            //ddlState.Items.Clear();
            //ddlDistrict.Items.Clear();
            //new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            //ddlCountry.SelectedValue = "1";
            //new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            //new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null, null, null), "District", "DistrictID");
            //new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            //new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            //txtAddress.Text = string.Empty;
            //txtAddress2.Text = string.Empty;
            //txtAddress3.Text = string.Empty;
            //txtProduct.Text = string.Empty;
            //txtRemarks.Text = string.Empty;

            //txtNextFollowUpDate.Text = string.Empty;
        }
        public PPurchaseOrder Read()
        {
            PPurchaseOrder enquiry = new PPurchaseOrder();
            //enquiry.CustomerName = txtCustomerName.Text.Trim();
            //enquiry.EnquiryNextFollowUpDate = Convert.ToDateTime(txtNextFollowUpDate.Text.Trim());
            //enquiry.PersonName = txtPersonName.Text.Trim();
            //enquiry.Mobile = txtMobile.Text.Trim();
            //enquiry.Mail = txtMail.Text.Trim();
            //enquiry.ProductType = new PProductType();
            //enquiry.ProductType.ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue);

            //enquiry.Source = new PLeadSource();
            //enquiry.Source.SourceID = Convert.ToInt32(ddlSource.SelectedValue);
            //enquiry.Status = new PPreSaleStatus();
            //enquiry.Status.StatusID = 1;
            //enquiry.Country = new PDMS_Country();
            //enquiry.Country.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            //enquiry.State = new PDMS_State();
            //enquiry.State.StateID = Convert.ToInt32(ddlState.SelectedValue);
            //enquiry.District = new PDMS_District();
            //enquiry.District.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            //enquiry.Address = txtAddress.Text.Trim();
            //enquiry.Address2 = txtAddress2.Text.Trim();
            //enquiry.Address3 = txtAddress3.Text.Trim();
            //enquiry.Product = txtProduct.Text.Trim();
            //enquiry.Remarks = txtRemarks.Text.Trim();
            //enquiry.CreatedBy = new PUser();
            return enquiry;
        }
        public void Write(PPurchaseOrder enquiry)
        {
            //txtCustomerName.Text = enquiry.CustomerName; 
            //txtMobile.Text = enquiry.Mobile;
            //txtMail.Text = enquiry.Mail;
            //new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            //new DDLBind(ddlState, new BDMS_Address().GetState(null, null, null, null, null), "State", "StateID");
            //new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(null, null, null, null, null, null), "District", "DistrictID");
            //new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            //new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            //ddlCountry.SelectedValue = enquiry.Country.CountryID.ToString();
            //ddlState.SelectedValue = enquiry.State.StateID.ToString();
            //ddlDistrict.SelectedValue = enquiry.District.DistrictID.ToString();
            //ddlSource.SelectedValue = enquiry.Source.SourceID.ToString();
            //ddlProductType.SelectedValue = enquiry.ProductType == null ? "0" : enquiry.ProductType.ProductTypeID.ToString();
            //txtAddress.Text = enquiry.Address.ToString();
            //txtAddress2.Text = enquiry.Address2.ToString();
            //txtAddress3.Text = enquiry.Address3.ToString();
            //txtProduct.Text = enquiry.Product;
            //txtRemarks.Text = enquiry.Remarks;
            //txtNextFollowUpDate.Text = Convert.ToString(enquiry.EnquiryNextFollowUpDate);
        }
        public string Validation()
        {
            //txtCustomerName.BorderColor = Color.Silver; 
            //txtMobile.BorderColor = Color.Silver;
            //ddlProductType.BorderColor = Color.Silver;
            //ddlSource.BorderColor = Color.Silver;
            //ddlCountry.BorderColor = Color.Silver;
            //ddlState.BorderColor = Color.Silver;
            //ddlDistrict.BorderColor = Color.Silver;
            //txtNextFollowUpDate.BorderColor = Color.Silver;
            string Message = "";
            //if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            //{
            //    txtCustomerName.BorderColor = Color.Red;
            //    return "Please enter the Customer Name...!";
            //}
            //if (string.IsNullOrEmpty(txtNextFollowUpDate.Text.Trim()))
            //{
            //    txtNextFollowUpDate.BorderColor = Color.Red;
            //    return "Please select the Next FollowUp Date.!";
            //}
            //if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            //{
            //    txtMobile.BorderColor = Color.Red;
            //    return "Please Enter the Mobile...!";
            //}
            //if (txtMobile.Text.Trim().Length != 10)
            //{
            //    txtMobile.BorderColor = Color.Red;
            //    return "Mobile Length should be 10 digit";
            //}
            //if (ddlProductType.SelectedValue == "0")
            //{
            //    ddlProductType.BorderColor = Color.Red;
            //    return "Please select the Product Type";
            //}
            //if (ddlSource.SelectedValue == "0")
            //{
            //    ddlSource.BorderColor = Color.Red;
            //    return "Please select the Source";
            //}
            //if (ddlCountry.SelectedValue == "0")
            //{
            //    ddlCountry.BorderColor = Color.Red;
            //    return "Please select the Country";
            //}
            //if (ddlState.SelectedValue == "0")
            //{
            //    ddlState.BorderColor = Color.Red;
            //    return "Please select the State";
            //}
            //if (ddlDistrict.SelectedValue == "0")
            //{
            //    ddlDistrict.BorderColor = Color.Red;
            //    return "Please select the District";
            //}
            return Message;
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
        void fillVendor()
        {
            ddlVendor.DataTextField = "CodeWithName";
            ddlVendor.DataValueField = "DID";
            ddlVendor.DataSource = PSession.User.Dealer;
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("All", "0"));
        }
        void fillItem()
        { 
            if(PurchaseOrderItem.Count==0)
            {
                PurchaseOrderItem.Add(new PPurchaseOrderItem());
            }
            gvPOItem.DataSource = PurchaseOrderItem;
            gvPOItem.DataBind(); 
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }
        private void FillGetDealerOffice()
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {

        }
    }
}