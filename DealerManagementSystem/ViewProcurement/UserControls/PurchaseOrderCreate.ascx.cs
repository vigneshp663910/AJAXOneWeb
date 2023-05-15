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
        public List<PPurchaseOrderItem_Insert> PurchaseOrderItem_Insert
        {
            get
            {
                if (ViewState["PurchaseOrderItem_Insert"] == null)
                {
                    ViewState["PurchaseOrderItem_Insert"] = new List<PPurchaseOrderItem_Insert>();
                }
                return (List<PPurchaseOrderItem_Insert>)ViewState["PurchaseOrderItem_Insert"];
            }
            set
            {
                ViewState["PurchaseOrderItem_Insert"] = value;
            }
        }
        public PPurchaseOrder_Insert PO_Insert
        {
            get
            {
                if (ViewState["PPurchaseOrder_Insert"] == null)
                {
                    ViewState["PPurchaseOrder_Insert"] = new PPurchaseOrder_Insert();
                }
                return (PPurchaseOrder_Insert)ViewState["PPurchaseOrder_Insert"];
            }
            set
            {
                ViewState["PPurchaseOrder_Insert"] = value;
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
           // new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID");
            //ddlDealerOffice
            Clear();
            fillItem();
            fillPurchaseOrderType();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.Text == "Upload Material")
                {
                }
                else if (lbActions.Text == "Save")
                {
                    Save();
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
        public PPurchaseOrder_Insert Read()
        {
            PPurchaseOrder_Insert PO = new PPurchaseOrder_Insert();
            PO.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            PO.DealerOfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            PO.PurchaseOrderToID = Convert.ToInt32(ddlOrderTo.SelectedValue);
            PO.VendorID = Convert.ToInt32(ddlVendor.SelectedValue);
            PO.PurchaseOrderTypeID = Convert.ToInt32(ddlPurchaseOrderType.SelectedValue);
            PO.DivisionID = Convert.ToInt32(ddlDivision.SelectedValue);
            PO.ReferenceNo = txtReferenceNo.Text.Trim();
            PO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            PO.Remarks = txtRemarks.Text.Trim(); 
            return PO;
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
            
            ddlDealer.BorderColor = Color.Silver;
            ddlVendor.BorderColor = Color.Silver;
            ddlPurchaseOrderType.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            string Message = "";

            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                return "Please select the Dealer";
            }
            if (ddlVendor.SelectedValue == "0")
            {
                ddlVendor.BorderColor = Color.Red;
                return "Please select the Vendor";
            }
            if (ddlPurchaseOrderType.SelectedValue == "0")
            {
                ddlPurchaseOrderType.BorderColor = Color.Red;
                return "Please select the Purchase Order Type";
            }
            if (ddlDivision.SelectedValue == "0")
            {
                ddlDivision.BorderColor = Color.Red;
                return "Please select the Division";
            }
            if (ddlDealerOffice.SelectedValue == "0")
            {
                ddlDealerOffice.BorderColor = Color.Red;
                return "Please select the Dealer Office";
            }
            if (string.IsNullOrEmpty(txtExpectedDeliveryDate.Text.Trim()))
            {
                txtExpectedDeliveryDate.BorderColor = Color.Red;
                return "Please Enter the Expected Delivery Date";
            }
             

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
            gvPOItem.DataSource = PurchaseOrderItem_Insert;
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
           
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            string Message = Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            Message = ValidationItem();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            if (PO_Insert.PurchaseOrderItems == null)
            {
                PO_Insert.PurchaseOrderItems = new List<PPurchaseOrderItem_Insert>();
            }

            PPurchaseOrderItem_Insert PoI = ReadItem();
            PO_Insert.PurchaseOrderItems.Add(PoI);

            string Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
            string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
            string OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
            string Material = PoI.MaterialCode;
            string IV_SEC_SALES = "";
            //string PriceDate = DateTime.Now.ToShortDateString();
            string PriceDate = "";
            string IsWarrenty = "false";

            PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, OrderType, 1, Material, PoI.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);
            PoI.Price = Mat.CurrentPrice;
            PoI.DiscountAmount = Mat.Discount;
            PoI.TaxableAmount = Mat.TaxablePrice;
            PoI.SGST = Mat.SGST;
            PoI.SGSTValue = Mat.SGSTValue;
            PoI.CGST = Mat.CGST;
            PoI.CGSTValue = Mat.CGSTValue;
            PoI.CGST = Mat.CGST;
            PoI.IGSTValue = Mat.IGSTValue;

            PurchaseOrderItem_Insert.Add(PoI);
            fillItem();
            ClearItem();
        }

        protected void ddlPurchaseOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDivision.Items.Clear();
            ddlDivision.DataTextField = "DivisionDescription";
            ddlDivision.DataValueField = "DivisionID";
            ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

            string OrderType = ddlPurchaseOrderType.SelectedValue;

            if ((OrderType == "1") || (OrderType == "2") || (OrderType == "7"))
            {
                ddlDivision.Items.Insert(1, new ListItem("Parts", "15")); 
            }
            else if (ddlPurchaseOrderType.SelectedValue == "5")
            {
                ddlDivision.Items.Insert(1, new ListItem("Batching Plant", "1"));
                ddlDivision.Items.Insert(2, new ListItem("Concrete Mixer", "2"));
                ddlDivision.Items.Insert(3, new ListItem("Concrete Pump", "3"));
                ddlDivision.Items.Insert(4, new ListItem("Dumper", "4"));
                ddlDivision.Items.Insert(5, new ListItem("Transit Mixer", "11"));
                ddlDivision.Items.Insert(6, new ListItem("Mobile Concrete Equipment", "14"));
                ddlDivision.Items.Insert(7, new ListItem("Placing Equipment", "19"));
            }
            else if (ddlPurchaseOrderType.SelectedValue == "6")
            {
                ddlDivision.Items.Insert(1, new ListItem("Parts", "15"));
                ddlDivision.Items.Insert(2, new ListItem("Batching Plant", "1"));
                ddlDivision.Items.Insert(3, new ListItem("Concrete Mixer", "2"));
                ddlDivision.Items.Insert(4, new ListItem("Concrete Pump", "3"));
                ddlDivision.Items.Insert(5, new ListItem("Dumper", "4"));
                ddlDivision.Items.Insert(6, new ListItem("Transit Mixer", "11"));
                ddlDivision.Items.Insert(7, new ListItem("Mobile Concrete Equipment", "14"));
                ddlDivision.Items.Insert(8, new ListItem("Placing Equipment", "19"));
            } 
        }

        protected void ddlOrderTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillPurchaseOrderType();
        }

        void fillPurchaseOrderType()
        {
            ddlPurchaseOrderType.Items.Clear();
            ddlPurchaseOrderType.DataTextField = "PurchaseOrderType";
            ddlPurchaseOrderType.DataValueField = "PurchaseOrderTypeID";
            ddlPurchaseOrderType.Items.Insert(0, new ListItem("Select", "0"));

            if (ddlOrderTo.SelectedValue == "1")
            {
                ddlPurchaseOrderType.Items.Insert(1, new ListItem("Stock Order", "1"));
                ddlPurchaseOrderType.Items.Insert(2, new ListItem("Emergency Order", "2"));
                ddlPurchaseOrderType.Items.Insert(3, new ListItem("Break Down Order", "7"));
                ddlPurchaseOrderType.Items.Insert(4, new ListItem("Machine Order", "5"));

            }
            else
            {
                ddlPurchaseOrderType.Items.Insert(1, new ListItem("Intra-Dealer Order", "6"));
            }
        }


        void ClearItem()
        {
            hdfMaterialID.Value = "";
            hdfMaterialCode.Value = "";
            txtMaterial.Text = "";
            txtQty.Text = "";
        }
        public PPurchaseOrderItem_Insert ReadItem()
        {
            PPurchaseOrderItem_Insert SM = new PPurchaseOrderItem_Insert();
            SM.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SM.MaterialCode = hdfMaterialCode.Value;
            // SM.SupersedeYN = cbSupersedeYN.Checked;
            SM.Quantity = Convert.ToInt32(txtQty.Text.Trim());
            return SM;
        }
        public string ValidationItem()
        {
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                return "Please select the Material";
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                return "Please enter the Qty";
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty";
            }
            return "";
        }

        public void Save()
        {
            PO_Insert = Read(); 
            PO_Insert.PurchaseOrderItems = PurchaseOrderItem_Insert;
            string result = new BAPI().ApiPut("PurchaseOrder", PO_Insert);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
    }
}