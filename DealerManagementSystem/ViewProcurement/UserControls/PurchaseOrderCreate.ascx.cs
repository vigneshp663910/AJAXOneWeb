using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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

        public DataTable MaterialCart
        {
            get
            {
                if (Session["MaterialCart"] == null)
                {
                    Session["MaterialCart"] = new DataTable();
                }
                return (DataTable)Session["MaterialCart"];
            }
            set
            {
                Session["MaterialCart"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }
        public void FillMaster()
        {
            fillDealer();
            new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
            fillVendor(ddlOrderTo.SelectedValue);
            fillPurchaseOrderType(ddlOrderTo.SelectedValue);
            Clear();
            fillItem();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.ID == "lbUploadMaterial")
                {
                    MPE_MaterialUpload.Show();
                }
                else if (lbActions.ID == "lbDownloadMaterialTemplate")
                {
                    DownloadMaterialTemplate();
                }
                else if (lbActions.ID == "lbSave")
                {
                    Save();
                }
                else if (lbActions.ID == "lbAddMaterialFromCart")
                {
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                    MaterialCart = new BDMS_PurchaseOrder().GetPurchaseOrderFromCart(new BDMS_Dealer().GetDealer(Convert.ToInt32(ddlDealer.SelectedValue), null, null, null)[0].DealerCode);

                    DataTable dt = MaterialCart.AsEnumerable()
       .GroupBy(r => new { OrderNo = r["OrderNo"], OrderDate = r["OrderDate"], CustomerCode = r["CustomerCode"], DealerCode = r["DealerCode"] })
       .Select(g => g.OrderBy(r => r["OrderNo"]).First())
       .CopyToDataTable();

                    gvMaterialFromCart.DataSource = dt;
                    gvMaterialFromCart.DataBind();
                    MPE_MaterialFromCart.Show();
                }
                else if (lbActions.ID == "lbCopyFromPO")
                {
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                    MPE_CopyOrder.Show();
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
            // PO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
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
            //txtExpectedDeliveryDate.BorderColor = Color.Silver;
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
            //if (string.IsNullOrEmpty(txtExpectedDeliveryDate.Text.Trim()))
            //{
            //    txtExpectedDeliveryDate.BorderColor = Color.Red;
            //    return "Please Enter the Expected Delivery Date";
            //}


            return Message;
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithDisplayName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == 2);
            ddlDealer.DataBind();
            //ddlDealer.Items.Insert(0, new ListItem("All", "0"));
            FillGetDealerOffice();
        }
        void fillVendor(string OrderTo)
        {
            ddlVendor.DataTextField = "CodeWithDisplayName";
            ddlVendor.DataValueField = "DealerID";
            ddlVendor.DataSource = new BDMS_Dealer().GetDealerAll(null, null, null, OrderTo == "1" ? 1 : 2);
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("Select", "0"));
            if (ddlOrderTo.SelectedValue == "1")
                ddlVendor.SelectedValue = ConfigurationManager.AppSettings["AjaxDealerID"];
        }
        void fillItem()
        {
            gvPOItem.DataSource = PurchaseOrderItem_Insert;
            gvPOItem.DataBind();

            decimal Price = 0, Discount = 0, TaxableAmount = 0, TaxAmount = 0;
            foreach (PPurchaseOrderItem_Insert Item in PurchaseOrderItem_Insert)
            {
                Price = Price + Item.Price;
                Discount = Discount + Item.DiscountAmount;
                TaxableAmount = TaxableAmount + Item.TaxableAmount;
                TaxAmount = TaxAmount + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue;

                Item.NetValue = Item.CGSTValue + Item.SGSTValue + Item.IGSTValue + Item.TaxableAmount;
            }
            lblPrice.Text = Price.ToString();
            lblDiscount.Text = Discount.ToString();
            lblTaxableAmount.Text = TaxableAmount.ToString();
            lblTaxAmount.Text = TaxAmount.ToString();
            lblGrossAmount.Text = (TaxableAmount + TaxAmount).ToString();

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
            try
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                Message = ValidationItem(hdfMaterialID.Value, txtQty.Text.Trim());
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                if (PurchaseOrderItem_Insert.Any(item => item.MaterialID == Convert.ToInt32(hdfMaterialID.Value)))
                {
                    lblMessage.Text = "Material already available.";
                    return;
                }
                PPurchaseOrderItem_Insert PoI = ReadItem(hdfMaterialID.Value, hdfMaterialCode.Value.Trim(), txtQty.Text.Trim());
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
                PoI.MaterialDescription = m.MaterialDescription;
                PoI.UOM = m.BaseUnit;
                if (string.IsNullOrEmpty(m.HSN))
                {
                    lblMessage.Text = "HSN is Not updated for this material " + m.MaterialCode + ". Please contact Admin.";
                    return;
                }
                PO_Insert = Read();
                if (PO_Insert.PurchaseOrderTypeID == (short)PurchaseOrderType.MachineOrder)
                {
                    if ((PurchaseOrderItem_Insert.Count != 0) || (PoI.Quantity != 1))
                    {
                        lblMessage.Text = "In machine Order you cannot add more material or more quantity.";
                        return;
                    }
                }

                //  string Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                // string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                //  string OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
                string Material = PoI.MaterialCode;
                string IV_SEC_SALES = "";
                //string PriceDate = DateTime.Now.ToShortDateString();
                // string PriceDate = "";
                // string IsWarrenty = "false";

                //PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, OrderType, 1, Material, PoI.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);

                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                MaterialPrice.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;

                //MaterialPrice.Division = "";
                //    MaterialPrice.PriceDate =
                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                {
                    ItemNo = "10",
                    Material = PoI.MaterialCode,
                    Quantity = PoI.Quantity
                });

                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                PMaterial Mat = Mats[0];
                if (Mat.CurrentPrice == 0)
                {
                    lblMessage.Text = "Price is Not updated for this material " + Material + ". Please contact Admin.";
                    return;
                }
                PoI.UnitPrice = Mat.CurrentPrice / PoI.Quantity;
                PoI.Price = Mat.CurrentPrice;
                PoI.DiscountAmount = Mat.Discount;
                PoI.TaxableAmount = Mat.TaxablePrice;
                PoI.SGST = Mat.SGST;
                PoI.SGSTValue = Mat.SGSTValue;
                PoI.CGST = Mat.SGST;
                PoI.CGSTValue = Mat.SGSTValue;
                PoI.IGST = Mat.IGST;
                PoI.IGSTValue = Mat.IGSTValue;
                PoI.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
                PoI.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
                PoI.NetValue = PoI.TaxableAmount + PoI.SGSTValue + PoI.CGSTValue + PoI.IGSTValue;
                PurchaseOrderItem_Insert.Add(PoI);
                PoI.Item = PurchaseOrderItem_Insert.Count * 10;
                fillItem();
                ClearItem(); 
            }
            catch(Exception e1)
            {
                lblMessage.Text = e1.Message; 
            }
        }
        protected string Save(string MaterialID, string MaterialCode, string Qty)
        {

            lblMessageMaterialUpload.ForeColor = Color.Red;
            lblMessageMaterialUpload.Visible = true;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            string Message = Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return Message;
            }
            Message = ValidationItem(MaterialID, Qty);
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageMaterialUpload.Text = Message;
                MPE_MaterialUpload.Show();
                return Message;
            }
            if (PurchaseOrderItem_Insert.Any(item => item.MaterialID == Convert.ToInt32(MaterialID)))
            {
                Message = "Dublicate Material Found.";
                lblMessageMaterialUpload.Text = Message;
                MPE_MaterialUpload.Show();
                return Message;
            }
            List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(MaterialCode, "", Convert.ToInt32(ddlDivision.SelectedValue), "false");
            if (Materials.Count == 0)
            {
                Message = "Material " + MaterialCode + " Not Available.";
                lblMessageMaterialUpload.Text = Message;
                MPE_MaterialUpload.Show();
                return Message;
            }
            PPurchaseOrderItem_Insert PoI = ReadItem(MaterialID, MaterialCode, Qty);
            PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
            PoI.MaterialDescription = m.MaterialDescription;
            PoI.UOM = m.BaseUnit;
            if (string.IsNullOrEmpty(m.HSN))
            {
                Message = "HSN is Not updated for this material " + m.MaterialCode + ". Please contact Admin.";
                lblMessageMaterialUpload.Text = Message;
                MPE_MaterialUpload.Show();
                return Message;
            }
            PO_Insert = Read();
            if (PO_Insert.PurchaseOrderTypeID == (short)PurchaseOrderType.MachineOrder)
            {
                if ((PurchaseOrderItem_Insert.Count != 0) || (PoI.Quantity != 1))
                {
                    Message = "In machine Order you cannot add more material or more quantity.";
                    lblMessageMaterialUpload.Text = Message;
                    MPE_MaterialUpload.Show();
                    return Message;
                }
            }

            //string Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
            //string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
            //string OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
            //string Material = PoI.MaterialCode;
            //string IV_SEC_SALES = "";
            ////string PriceDate = DateTime.Now.ToShortDateString();
            //string PriceDate = "";
            //string IsWarrenty = "false";

            //PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, OrderType, 1, Material, PoI.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);

            List<PMaterial_Api> Material_SapS = new List<PMaterial_Api>();
            Material_SapS.Add(new PMaterial_Api()
            {
                MaterialCode = PoI.MaterialCode,
                Quantity = PoI.Quantity,
                Item = (Material_SapS.Count + 1) * 10
            });
            PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
            MaterialPrice.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
            MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
            MaterialPrice.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;

            MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
            MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
            {
                ItemNo = "10",
                Material = PoI.MaterialCode,
                Quantity = PoI.Quantity
            });


            List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
            PMaterial Mat = Mats[0];

            if (Mat.CurrentPrice == 0)
            {
                Message = "Price is Not updated for this material " + PoI.MaterialCode + ". Please contact Admin.";
                lblMessageMaterialUpload.Text = Message;
                MPE_MaterialUpload.Show();
                return Message;
            }
            PoI.UnitPrice = Mat.CurrentPrice / PoI.Quantity;
            PoI.Price = Mat.CurrentPrice;
            PoI.DiscountAmount = Mat.Discount;
            PoI.TaxableAmount = Mat.TaxablePrice;
            PoI.SGST = Mat.SGST;
            PoI.SGSTValue = Mat.SGSTValue;
            PoI.CGST = Mat.SGST;
            PoI.CGSTValue = Mat.SGSTValue;
            PoI.IGST = Mat.IGST;
            PoI.IGSTValue = Mat.IGSTValue;
            PoI.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
            PoI.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
            PoI.NetValue = PoI.TaxableAmount + PoI.SGSTValue + PoI.CGSTValue + PoI.IGSTValue;
            PurchaseOrderItem_Insert.Add(PoI);
            PoI.Item = PurchaseOrderItem_Insert.Count * 10;
            fillItem();
            ClearItem();
            return Message;
        }
        protected void ddlPurchaseOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPurchaseOrderType.SelectedValue == "1")
            {
                PApiResult Result = new BDMS_PurchaseOrder().GetValidateDealerStockOrderControl(Convert.ToInt32(ddlDealer.SelectedValue));

                if (Result.Status == PApplication.Failure)
                {
                    Response.Write("<script>alert('" + Result.Message + "');</script>");
                    //lblMessage.Text = Result.Message;
                    //lblMessage.Visible = true;
                    //lblMessage.ForeColor = Color.Red;
                    ddlPurchaseOrderType.SelectedValue = "0";
                    return;
                }
            }

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
            fillVendor(ddlOrderTo.SelectedValue);
            fillPurchaseOrderType(ddlOrderTo.SelectedValue);
        }
        void fillPurchaseOrderType(string OrderTo)
        {
            ddlPurchaseOrderType.Items.Clear();
            ddlPurchaseOrderType.DataTextField = "PurchaseOrderType";
            ddlPurchaseOrderType.DataValueField = "PurchaseOrderTypeID";
            ddlPurchaseOrderType.Items.Insert(0, new ListItem("Select", "0"));

            if (OrderTo == "1")
            {
                ddlPurchaseOrderType.Items.Insert(1, new ListItem("Stock Order-Within 15 Days", "1"));
                ddlPurchaseOrderType.Items.Insert(2, new ListItem("Emergency Order-Within 3 Days", "2"));
                ddlPurchaseOrderType.Items.Insert(3, new ListItem("Break Down Order-Within 3 Days", "7"));
                ddlPurchaseOrderType.Items.Insert(4, new ListItem("Machine Order-Within 3 Days", "5"));

            }
            else
            {
                ddlPurchaseOrderType.Items.Insert(1, new ListItem("Intra-Dealer Order-Within 3 Days", "6"));
            }
        }
        void ClearItem()
        {
            hdfMaterialID.Value = "";
            hdfMaterialCode.Value = "";
            txtMaterial.Text = "";
            txtQty.Text = "";
        }
        public PPurchaseOrderItem_Insert ReadItem(string MaterialID, string MaterialCode, string Qty)
        {
            PPurchaseOrderItem_Insert SM = new PPurchaseOrderItem_Insert();
            SM.MaterialID = Convert.ToInt32(MaterialID);
            SM.MaterialCode = MaterialCode;
            // SM.SupersedeYN = cbSupersedeYN.Checked;
            SM.Quantity = Convert.ToInt32(Qty);
            return SM;
        }
        public string ValidationItem(string MaterialID, string Qty)
        {
            if (string.IsNullOrEmpty(MaterialID))
            {
                return "Please select the Material";
            }
            if (string.IsNullOrEmpty(Qty))
            {
                return "Please enter the Qty";
            }
            decimal value;
            if (!decimal.TryParse(Qty, out value))
            {
                return "Please enter correct format in Qty";
            }
            if (value < 1)
            {
                return "Please enter qty more than zero";
            }
            return "";
        }
        public void Save()
        {
            PO_Insert = Read();
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            if ((PO_Insert.PurchaseOrderTypeID == 2) || (PO_Insert.PurchaseOrderTypeID == 7))
            {
                if (string.IsNullOrEmpty(PO_Insert.ReferenceNo))
                {
                    lblMessage.Text = "Please enter the ReferenceNo";
                    return;
                }
            }
            PO_Insert.PurchaseOrderItems = PurchaseOrderItem_Insert;
            string result = new BAPI().ApiPut("PurchaseOrder", PO_Insert);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            Session["PurchaseOrderID"] = Result.Data;
            Response.Redirect("PurchaseOrder.aspx");
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
        }

        protected void lnkBtnPoItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = true;
                LinkButton lnkBtnCountryDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnCountryDelete.NamingContainer);
                string Material = ((Label)row.FindControl("lblMaterial")).Text.Trim();

                int i = 0;
                foreach (PPurchaseOrderItem_Insert Item in PurchaseOrderItem_Insert)
                {
                    if (Item.MaterialCode == Material)
                    {
                        PurchaseOrderItem_Insert.RemoveAt(i);
                        lblMessage.Text = "Material Removed successfully";
                        lblMessage.ForeColor = Color.Green;
                        int ItemNo = 0;
                        foreach (PPurchaseOrderItem_Insert ItemN in PurchaseOrderItem_Insert)
                        {
                            ItemNo = ItemNo + 10;
                            ItemN.Item = ItemNo;
                        }
                        fillItem();
                        return;
                    }
                    i = i + 1;
                }
                lblMessage.Text = "Please Contact admin.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
            lblMessage.ForeColor = Color.Red;
        }

        protected void btnMaterialFromCart_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_MaterialFromCart.Show();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                lblMessageMaterialFromCart.Text = "";
                lblMessageMaterialFromCart.ForeColor = Color.Red;
                lblMessageMaterialFromCart.Visible = true;

                List<PMaterial_Api> Material_SapS = new List<PMaterial_Api>();
                PMaterial_Api Material_Sap = new PMaterial_Api();
                for (int i = 0; i < gvMaterialFromCart.Rows.Count; i++)
                {
                    GridView gvMaterialFromCartItem = (GridView)gvMaterialFromCart.Rows[i].FindControl("gvMaterialFromCartItem");
                    for (int j = 0; j < gvMaterialFromCartItem.Rows.Count; j++)
                    {
                        CheckBox cbSelectChild = (CheckBox)gvMaterialFromCartItem.Rows[j].FindControl("cbSelectChild");
                        if (cbSelectChild.Checked)
                        {
                            Label lblMaterial = (Label)gvMaterialFromCartItem.Rows[j].FindControl("lblMaterial");
                            List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(lblMaterial.Text, "", Convert.ToInt32(ddlDivision.SelectedValue), "false");
                            if (Materials.Count == 0)
                            {
                                lblMessageMaterialFromCart.Text = "Material " + lblMaterial.Text + " Not Available.";
                                lblMessageMaterialFromCart.Visible = true;
                                lblMessageMaterialFromCart.ForeColor = Color.Red;
                                MPE_MaterialFromCart.Show();
                                return;
                            }
                            foreach (PPurchaseOrderItem_Insert Item in PurchaseOrderItem_Insert)
                            {
                                if (Item.MaterialCode == lblMaterial.Text)
                                {
                                    continue;
                                }
                            }
                            if (Material_SapS.Where(M => M.MaterialCode == lblMaterial.Text).Count() + (PurchaseOrderItem_Insert.Where(M => M.MaterialCode == lblMaterial.Text).Count()) == 0)
                            {
                                Label lblPartQty = (Label)gvMaterialFromCartItem.Rows[j].FindControl("lblPartQty");
                                Material_Sap = new PMaterial_Api();
                                Material_Sap.MaterialCode = lblMaterial.Text;
                                Material_Sap.Quantity = Convert.ToDecimal(lblPartQty.Text);
                                Material_Sap.Item = (Material_SapS.Count + 1) * 10;
                                Material_SapS.Add(Material_Sap);
                            }

                        }
                    }

                }

                //string Material = PoI.MaterialCode; 
                //PMaterialTax_Api MaterialTax_Sap = new PMaterialTax_Api();
                //MaterialTax_Sap.Material = Material_SapS;
                //MaterialTax_Sap.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                //MaterialTax_Sap.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                //MaterialTax_Sap.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
                //MaterialTax_Sap.IV_SEC_SALES = "";
                //// MaterialTax_Sap.PriceDate = "";
                //MaterialTax_Sap.IsWarrenty = false;
                //List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapMulti(MaterialTax_Sap);




                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                MaterialPrice.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                foreach (PMaterial_Api M in Material_SapS)
                {
                    MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                    {
                        ItemNo = M.Item.ToString(),
                        Material = M.MaterialCode,
                        Quantity = M.Quantity
                    });
                }

                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                foreach (PMaterial Mat in Mats)
                {
                    if (Mat.CurrentPrice == 0)
                    {
                        lblMessageMaterialFromCart.Text = "Price is Not updated for this material " + Mat.MaterialCode + ". Please contact Admin.";
                        lblMessageMaterialFromCart.Visible = true;
                        lblMessageMaterialFromCart.ForeColor = Color.Red;
                        MPE_MaterialFromCart.Show();
                        return;
                    }
                }
                foreach (PMaterial Mat in Mats)
                {
                    PPurchaseOrderItem_Insert PoI = new PPurchaseOrderItem_Insert();
                    PDMS_Material MaterialSql = new BDMS_Material().GetMaterialListSQL(null, Mat.MaterialCode, null, null, null)[0];
                    PoI.MaterialID = MaterialSql.MaterialID;
                    PoI.UOM = MaterialSql.BaseUnit;
                    //PoI.
                    PoI.MaterialCode = Mat.MaterialCode;
                    foreach (PMaterial_Api M in Material_SapS)
                    {
                        if (Mat.MaterialCode == M.MaterialCode)
                        {
                            PoI.Quantity = M.Quantity;
                        }
                    }
                    PoI.Price = Mat.CurrentPrice;
                    PoI.DiscountAmount = Mat.Discount;
                    PoI.TaxableAmount = Mat.TaxablePrice;
                    PoI.SGST = Mat.SGST;
                    PoI.SGSTValue = Mat.SGSTValue;
                    PoI.CGST = Mat.SGST;
                    PoI.CGSTValue = Mat.SGSTValue;
                    PoI.IGST = Mat.IGST;
                    PoI.IGSTValue = Mat.IGSTValue;
                    PoI.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
                    PoI.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
                    PoI.NetValue = PoI.TaxableAmount + PoI.SGSTValue + PoI.CGSTValue + PoI.IGSTValue;
                    PurchaseOrderItem_Insert.Add(PoI);
                    PoI.Item = PurchaseOrderItem_Insert.Count * 10;
                }
                fillItem();
                ClearItem();
                MPE_MaterialFromCart.Hide();
            }
            catch (Exception e1)
            {
                lblMessageMaterialFromCart.Text = e1.Message;
                lblMessageMaterialFromCart.Visible = true;
                lblMessageMaterialFromCart.ForeColor = Color.Red;
                return;
            }
        }

        protected void gvMaterialFromCart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // string DeliveryNumber = Convert.ToString(gvDelivery.DataKeys[e.Row.RowIndex].Value);
                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvMaterialFromCartItem");
                    //List<PDMS_DeliveryItem> Lines = new List<PDMS_DeliveryItem>();
                    //Lines = SDMS_WarrantyClaimHeader.Find(s => s.DeliveryNumber == DeliveryNumber).DeliveryItems;


                    Label lblOrderNo = (Label)e.Row.FindControl("lblOrderNo");
                    DataTable dt = MaterialCart.AsEnumerable()
      .Where(r => Convert.ToString(r["OrderNo"]) == lblOrderNo.Text)
      .Select(g => g)
      .CopyToDataTable();

                    //DataTable dt1 = from customer in MaterialCart.AsEnumerable()
                    //                where customer.Field<string>("OrderNo") == "8"
                    //         select new
                    //                {
                    //                    PartNo = customer.Field<int>("PartNo"),
                    //                    PartDescription = customer.Field<string>("PartDescription"),
                    //                    PartQty = customer.Field<string>("PartQty")
                    //                };  

                    gvClaimInvoiceItem.DataSource = dt;

                    gvClaimInvoiceItem.DataBind();

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearchCopyOrder_Click(object sender, EventArgs e)
        {
            //PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderHeader(null, null, txtPoNumber.Text.Trim(), null, null, null, null, null, null, 1, 1);
            //List<PPurchaseOrder> PO = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(Result.Data));
            PPurchaseOrder PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderByMaterial(txtPoNumber.Text.Trim(), Convert.ToInt32(ddlDivision.SelectedValue));
            gvMaterialCopyOrder.DataSource = PurchaseOrder.PurchaseOrderItems;
            gvMaterialCopyOrder.DataBind();

            MPE_CopyOrder.Show();
        }

        protected void btnCopyPoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageCopyOrder.Text = "";
                lblMessageCopyOrder.ForeColor = Color.Red;
                lblMessageCopyOrder.Visible = true;
                List<PMaterial_Api> Material_SapS = new List<PMaterial_Api>();
                PMaterial_Api Material_Sap = new PMaterial_Api();

                for (int j = 0; j < gvMaterialCopyOrder.Rows.Count; j++)
                {
                    CheckBox cbSelectChild = (CheckBox)gvMaterialCopyOrder.Rows[j].FindControl("cbSelectChild");
                    if (cbSelectChild.Checked)
                    {
                        Label lblMaterial = (Label)gvMaterialCopyOrder.Rows[j].FindControl("lblMaterial");
                        List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(lblMaterial.Text, "", Convert.ToInt32(ddlDivision.SelectedValue), "false");
                        if (Materials.Count == 0)
                        {
                            lblMessageCopyOrder.Text = "Material " + lblMaterial.Text + " Not Available.";
                            lblMessageCopyOrder.Visible = true;
                            lblMessageCopyOrder.ForeColor = Color.Red;
                            MPE_CopyOrder.Show();
                            return;
                        }
                        foreach (PPurchaseOrderItem_Insert Item in PurchaseOrderItem_Insert)
                        {
                            if (Item.MaterialCode == lblMaterial.Text)
                            {
                                continue;
                            }
                        }
                        if (Material_SapS.Where(M => M.MaterialCode == lblMaterial.Text).Count() + (PurchaseOrderItem_Insert.Where(M => M.MaterialCode == lblMaterial.Text).Count()) == 0)
                        {
                            Label lblPartQty = (Label)gvMaterialCopyOrder.Rows[j].FindControl("lblPartQty");
                            Material_Sap = new PMaterial_Api();
                            Material_Sap.MaterialCode = lblMaterial.Text;
                            Material_Sap.Quantity = Convert.ToDecimal(lblPartQty.Text);
                            Material_Sap.Item = (Material_SapS.Count + 1) * 10;
                            Material_SapS.Add(Material_Sap);
                        }
                    }
                }

                //PMaterialTax_Api MaterialTax_Sap = new PMaterialTax_Api();
                //MaterialTax_Sap.Material = Material_SapS;
                //MaterialTax_Sap.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                //MaterialTax_Sap.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                //MaterialTax_Sap.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
                //MaterialTax_Sap.IV_SEC_SALES = "";
                //// MaterialTax_Sap.PriceDate = "";
                //MaterialTax_Sap.IsWarrenty = false;
                //List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapMulti(MaterialTax_Sap);

                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                MaterialPrice.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;
                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                foreach (PMaterial_Api M in Material_SapS)
                {
                    MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                    {
                        ItemNo = M.Item.ToString(),
                        Material = M.MaterialCode,
                        Quantity = M.Quantity
                    });
                }

                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                foreach (PMaterial Mat in Mats)
                {
                    if (Mat.CurrentPrice == 0)
                    {
                        lblMessageMaterialFromCart.Text = "Price is Not updated for this material " + Mat.MaterialCode + ". Please contact Admin.";
                        lblMessageMaterialFromCart.Visible = true;
                        lblMessageMaterialFromCart.ForeColor = Color.Red;
                        MPE_MaterialFromCart.Show();
                        return;
                    }
                }
                foreach (PMaterial Mat in Mats)
                {
                    PPurchaseOrderItem_Insert PoI = new PPurchaseOrderItem_Insert();
                    PDMS_Material MaterialSql = new BDMS_Material().GetMaterialListSQL(null, Mat.MaterialCode, null, null, null)[0];
                    PoI.MaterialID = MaterialSql.MaterialID;
                    PoI.UOM = MaterialSql.BaseUnit;
                    //PoI.
                    PoI.MaterialCode = Mat.MaterialCode;
                    foreach (PMaterial_Api M in Material_SapS)
                    {
                        if (Mat.MaterialCode == M.MaterialCode)
                        {
                            PoI.Quantity = M.Quantity;
                        }
                    }

                    PoI.UnitPrice = Mat.CurrentPrice / PoI.Quantity;
                    PoI.Price = Mat.CurrentPrice;
                    PoI.DiscountAmount = Mat.Discount;
                    PoI.TaxableAmount = Mat.TaxablePrice;
                    PoI.SGST = Mat.SGST;
                    PoI.SGSTValue = Mat.SGSTValue;
                    PoI.CGST = Mat.SGST;
                    PoI.CGSTValue = Mat.SGSTValue;
                    PoI.IGST = Mat.IGST;
                    PoI.IGSTValue = Mat.IGSTValue;
                    PoI.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
                    PoI.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
                    PoI.NetValue = PoI.TaxableAmount + PoI.SGSTValue + PoI.CGSTValue + PoI.IGSTValue;

                    PurchaseOrderItem_Insert.Add(PoI);
                    PoI.Item = PurchaseOrderItem_Insert.Count * 10;
                }
                fillItem();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        void Upload()
        {
            try
            {
                if (fileUpload.HasFile == true)
                {
                    using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                    {
                        //Read the first Sheet from Excel file.
                        IXLWorksheet workSheet = workBook.Worksheet(1);

                        //Create a new DataTable.
                        DataTable DTDealerOperatorDetailsUpload = new DataTable();

                        //Loop through the Worksheet rows.
                        int sno = 0;
                        foreach (IXLRow row in workSheet.Rows())
                        {
                            sno += 1;
                            if (sno > 1)
                            {
                                string MaterialID = string.Empty, Qty = string.Empty, MaterialCode = string.Empty;
                                List<IXLCell> Cells = row.Cells().ToList();
                                if (Cells.Count != 0)
                                {

                                    List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(Convert.ToString(Cells[1].Value), "", null);
                                    if (Materials.Count > 0)
                                    {
                                        MaterialID = Materials[0].MaterialID.ToString();
                                        MaterialCode = Materials[0].MaterialCode;
                                        Qty = Convert.ToString(Cells[2].Value);
                                        string Message = Save(MaterialID, MaterialCode, Qty);
                                        if (!string.IsNullOrEmpty(Message))
                                        {
                                            PurchaseOrderItem_Insert = null;
                                            gvPOItem.DataSource = PurchaseOrderItem_Insert;
                                            gvPOItem.DataBind();
                                            lblMessageMaterialUpload.Text = Message;
                                            lblMessageMaterialUpload.ForeColor = Color.Red;
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageMaterialUpload.Text = ex.ToString();
                lblMessageMaterialUpload.ForeColor = Color.Red;
            }
        }

        protected void btnUploadMaterial_Click(object sender, EventArgs e)
        {
            Upload();
        }
        void DownloadMaterialTemplate()
        {
            string Path = Server.MapPath("~/Templates/Material.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"Material.xlsx\"");
            byte[] data = req.DownloadData(Path);
            response.BinaryWrite(data);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end
            response.End();
        }

        protected void ChkMailH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkMailH = (CheckBox)sender;
            foreach (GridViewRow row in gvMaterialCopyOrder.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbSelectChild = row.FindControl("cbSelectChild") as CheckBox;
                    cbSelectChild.Checked = (ChkMailH.Checked) ? true : false;
                }
            }
            MPE_CopyOrder.Show();
        }

        protected void cbSelectChild_CheckedChanged(object sender, EventArgs e)
        {
            bool ChkHeader = true;
            CheckBox ChkMailH = (CheckBox)gvMaterialCopyOrder.HeaderRow.FindControl("ChkMailH");
            foreach (GridViewRow row in gvMaterialCopyOrder.Rows)
            {
                CheckBox cbSelectChild = row.FindControl("cbSelectChild") as CheckBox;
                if (cbSelectChild.Checked == false)
                {
                    ChkHeader = false;
                }
            }
            ChkMailH.Checked = ChkHeader;
            MPE_CopyOrder.Show();
        }

        protected void Btn_MatAvailability_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                Message = ValidationItem(hdfMaterialID.Value, txtQty.Text.Trim());
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                if (PurchaseOrderItem_Insert.Any(item => item.MaterialID == Convert.ToInt32(hdfMaterialID.Value)))
                {
                    lblMessage.Text = "Material already available.";
                    return;
                }
                PPurchaseOrderItem_Insert PoI = ReadItem(hdfMaterialID.Value, hdfMaterialCode.Value.Trim(), txtQty.Text.Trim());
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
                PoI.MaterialDescription = m.MaterialDescription;
                PoI.UOM = m.BaseUnit;
                if (string.IsNullOrEmpty(m.HSN))
                {
                    lblMessage.Text = "HSN is Not updated for this material " + m.MaterialCode + ". Please contact Admin.";
                    return;
                }
                PO_Insert = Read();
                if (PO_Insert.PurchaseOrderTypeID == (short)PurchaseOrderType.MachineOrder)
                {
                    if ((PurchaseOrderItem_Insert.Count != 0) || (PoI.Quantity != 1))
                    {
                        lblMessage.Text = "In machine Order you cannot add more material or more quantity.";
                        return;
                    }
                }

                PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                MaterialPrice.Customer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlVendor.SelectedValue), "").DealerCode;
                MaterialPrice.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;

                MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                {
                    ItemNo = "10",
                    Material = PoI.MaterialCode,
                    Quantity = PoI.Quantity
                });

                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                PMaterial Mat = Mats[0];
                if (Mat.CurrentPrice == 0)
                {
                    lblMessage.Text = "Price is Not updated for this material " + PoI.MaterialCode + ". Please contact Admin.";
                    return;
                }
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
    }
}