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
            lblMessageCopyOrder.Text = string.Empty;
            lblMessageMaterialUpload.Text = string.Empty; 
        }
        public void FillMaster()
        {

            fillDealer();
            PDealer P = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), null);
            lblEdfsCashBalance.Text = Convert.ToString(new BSap().GetEdfsCashBalance(P.DealerCode));
            //new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
            fillVendor(ddlOrderTo.SelectedValue);
            fillPurchaseOrderType(ddlOrderTo.SelectedValue);
            ddlPurchaseOrderType_SelectedIndexChanged(null, null);
            PurchaseOrderItem_Insert = new List<PPurchaseOrderItem_Insert>();
            Clear();
            fillItem();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.ID == "lbUploadMaterial")
                {
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return ;
                    }
                    MPE_MaterialUpload.Show();
                }
                else if (lbActions.ID == "lbDownloadMaterialTemplate")
                {
                    DownloadMaterialTemplate();
                }
                else if (lbActions.ID == "lbSave")
                {
                    lblMessage.ForeColor = Color.Red;
                    if (PurchaseOrderItem_Insert.Count == 0)
                    {
                        lblMessage.Text = "Please select the Material.";
                        return;
                    }
                    PO_Insert = Read();
                    PO_Insert.PurchaseOrderItems = PurchaseOrderItem_Insert; 
                    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("PurchaseOrder", PO_Insert)); 
                    if (Result.Status == PApplication.Failure)
                    {
                        lblMessage.Text = Result.Message;
                        return;
                    }
                    Session["PurchaseOrderID"] = Result.Data;
                    Response.Redirect("PurchaseOrder.aspx"); 
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
                    txtPoNumber.Text = "";
                    gvMaterialCopyOrder.DataSource = null;
                    gvMaterialCopyOrder.DataBind();
                    MPE_CopyOrder.Show();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message; 
                
            }
        }
        void Clear()
        {
            txtReferenceNo.Text= string.Empty;
            txtRemarks.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtQty.Text = string.Empty;
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
            txtReferenceNo.BorderColor = Color.Silver;
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
            if ((ddlPurchaseOrderType.SelectedValue == "2") || (ddlPurchaseOrderType.SelectedValue == "7"))
            {
                if (string.IsNullOrEmpty(txtReferenceNo.Text))
                {
                    txtReferenceNo.BorderColor = Color.Red;
                    return "Please enter the ReferenceNo";
                }
            }

            string From = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
            string To = DateTime.Now.ToShortDateString();
            PApiResult ResultPOList = new BDMS_PurchaseOrder().GetPurchaseOrderHeader(Convert.ToInt32(ddlDealer.SelectedValue), null, null, Convert.ToDateTime(From)
                    , Convert.ToDateTime(To), null, null, null, null, null, null);
            List<PPurchaseOrder> PurchaseOrderList = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(ResultPOList.Data));

            //PApiResult ResultStockCount = new BDMS_PurchaseOrder().GetDealerStockOrderControl(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            //List<PDealerStockOrderControl> StockOrderControl = JsonConvert.DeserializeObject<List<PDealerStockOrderControl>>(JsonConvert.SerializeObject(ResultStockCount.Data));

            //if (ddlPurchaseOrderType.SelectedValue == "1")
            //{   
            //    if(StockOrderControl[0].MaxCount > PurchaseOrderList.Count)
            //    {
            //        return "Stock order count exceeded";
            //    }
            //}

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
           // lblPrice.Text = Price.ToString();
            lblDiscount.Text = Discount.ToString();
            lblTaxableAmount.Text = TaxableAmount.ToString();
            lblTaxAmount.Text = TaxAmount.ToString();
            lblGrossAmount.Text = (TaxableAmount + TaxAmount).ToString();

            HeaderFieldVisibleConrol();

        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PDealer P = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), null);
                lblEdfsCashBalance.Text = Convert.ToString(new BSap().GetEdfsCashBalance(P.DealerCode));
                FillGetDealerOffice();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
            
        }
        private void FillGetDealerOffice()
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
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

            if ((OrderType == "1") || (OrderType == "2") || (OrderType == "7") || (OrderType == "8"))
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
            //if (OrderType == "1" || OrderType == "6")
            //{
            //    Btn_MatAvailability.Visible = true;
            //}
            //else
            //{
            //    Btn_MatAvailability.Visible = false;
            //}
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
                ddlPurchaseOrderType.Items.Insert(5, new ListItem("Merchandising-Within 3 Days", "8"));

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
        
        protected void lnkBtnPoItemDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                LinkButton lnkBtnCountryDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnCountryDelete.NamingContainer);
                string Material = ((Label)row.FindControl("lblMaterial")).Text.Trim();

                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null);
                if (PurchaseOrderItem_Insert.Any(item => "FERT" == Materials[0].MaterialType && item.MaterialCode == Material))
                {
                    if (PurchaseOrderItem_Insert.Count() != 1)
                    {
                        lblMessage.Text = "First remove other materials then remove FERT material : " + Materials[0].MaterialCode;
                        return;
                    }
                }


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
                        hdfItemCount.Value = PurchaseOrderItem_Insert.Count().ToString();
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
            MPE_CopyOrder.Show();
            //PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderHeader(null, null, txtPoNumber.Text.Trim(), null, null, null, null, null, null, 1, 1);
            //List<PPurchaseOrder> PO = JsonConvert.DeserializeObject<List<PPurchaseOrder>>(JsonConvert.SerializeObject(Result.Data));

            if (string.IsNullOrEmpty(txtPoNumber.Text.Trim()))
            {
                lblMessageCopyOrder.Text = "Please enter the Purchase Order Number";
                lblMessageCopyOrder.ForeColor = Color.Red;
                return;
            }

            PPurchaseOrder PurchaseOrder = new BDMS_PurchaseOrder().GetPurchaseOrderByMaterial(txtPoNumber.Text.Trim(), Convert.ToInt32(ddlDivision.SelectedValue));
            if (PurchaseOrder.PurchaseOrderItems.Count == 0)
            {
                lblMessageCopyOrder.Text = "Please check the Purchase Order Number";
                lblMessageCopyOrder.ForeColor = Color.Red;
                return;
            }

            gvMaterialCopyOrder.DataSource = PurchaseOrder.PurchaseOrderItems;
            gvMaterialCopyOrder.DataBind();
            btnCopyPoAdd.Visible = true; 
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

        
        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
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
                int MaterialID = new BDMS_Material().GetMaterialSupersedeFinalByID(Convert.ToInt32(hdfMaterialID.Value));

                if (MaterialID != Convert.ToInt32(hdfMaterialID.Value))
                {
                    //Supersede.Add(new PDMS_Material()
                    //{
                    //    MaterialCode = lblMaterial.Text,
                    //    Supersede = new PSupersede() { Material = MaterialCode }
                    //});
                }

                //lblMessage.Text = "The entered material : " + hdfMaterialCode.Value + " is superseded by other material number " + PoI.MaterialCode + ".";
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(MaterialID, null, null, null, null)[0];

                
                Message = AddMaterial(m, txtQty.Text.Trim());
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                FillMessage(new List<PDMS_Material>(), new Dictionary<string, string>());
                fillItem();
                ClearItem();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        protected void btnCopyPoAdd_Click(object sender, EventArgs e)
        {
            MPE_CopyOrder.Show();
            lblMessageCopyOrder.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDMS_Material> Supersede = new List<PDMS_Material>();
            ClearIssueGV();
            try
            {
                if (gvMaterialCopyOrder.Rows.Count == 0)
                {
                    lblMessageCopyOrder.Text = "Please select the material";
                    return;
                }
                for (int j = 0; j < gvMaterialCopyOrder.Rows.Count; j++)
                {

                    CheckBox cbSelectChild = (CheckBox)gvMaterialCopyOrder.Rows[j].FindControl("cbSelectChild");
                    if (cbSelectChild.Checked)
                    {
                        Label lblMaterial = (Label)gvMaterialCopyOrder.Rows[j].FindControl("lblMaterial");
                        TextBox txtPartQty = (TextBox)gvMaterialCopyOrder.Rows[j].FindControl("txtPartQty");
                        if (string.IsNullOrEmpty(txtPartQty.Text))
                        {
                            lblMessageCopyOrder.Text = "Please Enter the Quantity for these material " + lblMaterial.Text + ".";
                            return;
                        }
                        decimal value;
                        if (!decimal.TryParse(txtPartQty.Text, out value))
                        {
                            lblMessageCopyOrder.Text = "Please enter correct format in Qty";
                            return;
                        }
                        if (value < 1)
                        {
                            lblMessageCopyOrder.Text = "Please enter qty more than zero for these material " + lblMaterial.Text + ".";
                            return;
                        }
                    }
                }

                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
                for (int j = 0; j < gvMaterialCopyOrder.Rows.Count; j++)
                {
                    CheckBox cbSelectChild = (CheckBox)gvMaterialCopyOrder.Rows[j].FindControl("cbSelectChild");
                    if (cbSelectChild.Checked)
                    {
                        Label lblMaterial = (Label)gvMaterialCopyOrder.Rows[j].FindControl("lblMaterial");
                        TextBox txtPartQty = (TextBox)gvMaterialCopyOrder.Rows[j].FindControl("txtPartQty");
                        string MaterialCode = new BDMS_Material().GetMaterialSupersedeFinalByCode(lblMaterial.Text); 
                        if (MaterialCode != lblMaterial.Text)
                        {
                            Supersede.Add(new PDMS_Material()
                            {
                                MaterialCode = lblMaterial.Text,
                                Supersede = new PSupersede() { Material = MaterialCode }
                            });
                        }
                        //string MaterialCode = lblMaterial.Text;
                        //if (MaterialCode != lblMaterial.Text)
                        //{
                        //    Supersede.Add(new PDMS_Material()
                        //    {
                        //        MaterialCode = lblMaterial.Text,
                        //        Supersede = new PSupersede() { Material = MaterialCode }
                        //    });
                        //}
                        List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode && s.Model.Division.DivisionID == Convert.ToInt32(ddlDivision.SelectedValue) && s.IsActive == true).ToList();
                        if (Material.Count == 0)
                        {
                            MaterialIssue.Add(MaterialCode, "Material " + MaterialCode + " Not Available.");
                            continue;
                        }
                        if (PurchaseOrderItem_Insert.Any(item => item.MaterialCode == lblMaterial.Text))
                        {
                            MaterialIssue.Add(MaterialCode, "Duplicate Material (" + MaterialCode + ") Found.");
                            continue;
                        }
                        string Message = AddMaterial(Material[0], txtPartQty.Text);
                        if (!string.IsNullOrEmpty(Message))
                        {
                            MaterialIssue.Add(MaterialCode, Message);
                            continue;
                        }
                    }
                }

                FillMessage(Supersede, MaterialIssue);
                MPE_CopyOrder.Hide();
            }
            catch (Exception e1)
            {
                lblMessageCopyOrder.Text = e1.Message;
            }
        }
        protected void btnUploadMaterial_Click(object sender, EventArgs e)
        {
            MPE_MaterialUpload.Show();
            lblMessageMaterialUpload.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDMS_Material> Supersede = new List<PDMS_Material>();
            ClearIssueGV();
            try
            {
                if (fileUpload.HasFile != true)
                {
                    lblMessageMaterialUpload.Text = "Please check the file.";
                    return;
                }
                string validExcel = ".xlsx";
                string FileExtension = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                if (validExcel != FileExtension)
                {
                    lblMessageMaterialUpload.Text = "Please check the file format.";
                    return;
                }
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
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
                            List<IXLCell> Cells = row.Cells().ToList();
                            if (Cells.Count != 0)
                            {
                                string ExcelMaterialCode = Convert.ToString(Cells[1].Value).TrimEnd('\0');
                                string MaterialCode = new BDMS_Material().GetMaterialSupersedeFinalByCode(ExcelMaterialCode);
                                MaterialCode = MaterialCode.Trim(); 
                                if (MaterialCode != Convert.ToString(ExcelMaterialCode))
                                {
                                    Supersede.Add(new PDMS_Material()
                                    {
                                        MaterialCode = Convert.ToString(ExcelMaterialCode),
                                        Supersede = new PSupersede() { Material = MaterialCode }
                                    });
                                } 
                                List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode).ToList();
                                if (Material.Count == 0)
                                {
                                    MaterialIssue.Add(MaterialCode, "Material (" + MaterialCode + ") is not available.");
                                    continue;
                                }
                                if (PurchaseOrderItem_Insert.Any(item => item.MaterialID == Material[0].MaterialID))
                                {
                                    MaterialIssue.Add(Material[0].MaterialCode, "Duplicate Material (" + Material[0].MaterialCode + ") Found. It is removed in list");
                                    continue;
                                }

                                if (Material.Count > 0)
                                {
                                    string Message = AddMaterial(Material[0], Convert.ToString(Cells[2].Value));
                                    if (!string.IsNullOrEmpty(Message))
                                    {
                                        MaterialIssue.Add(MaterialCode, Message);
                                        continue;
                                    }
                                }
                            }
                        }
                    }

                }
                FillMessage(Supersede, MaterialIssue);
                MPE_MaterialUpload.Hide();
            }
            catch (Exception ex)
            {
                lblMessageMaterialUpload.Text = ex.Message.ToString();
            }
        }
        protected void btnMaterialFromCart_Click(object sender, EventArgs e)
        {
            MPE_MaterialFromCart.Show();
            lblMessageMaterialFromCart.ForeColor = Color.Red;
            lblMessage.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDMS_Material> Supersede = new List<PDMS_Material>();
            ClearIssueGV();
            try
            {
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
                for (int i = 0; i < gvMaterialFromCart.Rows.Count; i++)
                {
                    GridView gvMaterialFromCartItem = (GridView)gvMaterialFromCart.Rows[i].FindControl("gvMaterialFromCartItem");
                    for (int j = 0; j < gvMaterialFromCartItem.Rows.Count; j++)
                    {
                        CheckBox cbSelectChild = (CheckBox)gvMaterialFromCartItem.Rows[j].FindControl("cbSelectChild");
                        if (cbSelectChild.Checked)
                        {
                            Label lblMaterial = (Label)gvMaterialFromCartItem.Rows[j].FindControl("lblMaterial");
                            Label lblPartQty = (Label)gvMaterialFromCartItem.Rows[j].FindControl("lblPartQty");
                            string MaterialCode = lblMaterial.Text;

                            if (MaterialCode != lblMaterial.Text)
                            {
                                Supersede.Add(new PDMS_Material()
                                {
                                    MaterialCode = lblMaterial.Text,
                                    Supersede = new PSupersede() { Material = MaterialCode }
                                });
                            }
                            List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode && s.Model.Division.DivisionID == Convert.ToInt32(ddlDivision.SelectedValue) && s.IsActive == true).ToList();
                            if (Material.Count == 0)
                            {
                                MaterialIssue.Add(MaterialCode, "Material " + MaterialCode + " Not Available.");
                                continue;
                            }
                            if (PurchaseOrderItem_Insert.Any(item => item.MaterialCode == lblMaterial.Text))
                            {
                                MaterialIssue.Add(MaterialCode, "Duplicate Material (" + MaterialCode + ") Found.");
                                continue;
                            }
                            string Message = AddMaterial(Material[0], lblPartQty.Text);
                            if (!string.IsNullOrEmpty(Message))
                            {
                                MaterialIssue.Add(MaterialCode, Message);
                                continue;
                            }
                        }
                    }
                }
                MPE_MaterialFromCart.Hide();
                FillMessage(Supersede, MaterialIssue);
            }
            catch (Exception e1)
            {
                lblMessageMaterialFromCart.Text = e1.Message;
            }
        }
        protected string AddMaterial(PDMS_Material m, string QtyN)
        {
            //string Message = ValidationItem(m.MaterialID.ToString(), Qty);
            //if (!string.IsNullOrEmpty(Message))
            //{
            //    return Message; 
            //} 
            //List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(m.MaterialCode, "", Convert.ToInt32(ddlDivision.SelectedValue), "false");
            //if (Materials.Count == 0)
            //{
            //    return "Material " + m.MaterialCode + " Not Available."; 
            //}
            int Qty1 = Convert.ToInt32(Convert.ToDecimal(QtyN));

            if (Qty1 < 1)
            {
                return "Please Check the Quantity of matrial : " + m.MaterialCode;
            }

            if (Convert.ToInt32(ddlPurchaseOrderType.SelectedValue) == (short)PurchaseOrderType.MachineOrder)
            {
                Boolean chMaterialType = PurchaseOrderItem_Insert.Any(item => item.MaterialType == m.MaterialType);
                if (chMaterialType)
                {
                    return "Already FERT Material Available : " + m.MaterialCode;
                }
                if ( Qty1 != 1 && m.MaterialType == "FERT")
                {
                    return "In machine Order you allowed to add one quantity for FERT material : " + m.MaterialCode;
                }
            }

            PPurchaseOrderItem_Insert PoI   = new PPurchaseOrderItem_Insert();
            PoI.MaterialID = m.MaterialID;
            PoI.MaterialCode = m.MaterialCode;
            PoI.Quantity = Qty1;
           

            //   PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
            PoI.MaterialDescription = m.MaterialDescription;
            PoI.UOM = m.BaseUnit;
            PoI.MaterialType = m.MaterialType;
            if (string.IsNullOrEmpty(m.HSN))
            {
                return "HSN is Not updated for this material " + m.MaterialCode + ". Please contact Admin."; 
            }
            PO_Insert = Read(); 

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

            MaterialPrice.Division = new BDMS_Master().GetDivision(Convert.ToInt32(ddlDivision.SelectedValue), null)[0].DivisionCode;
            MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
            MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
            {
                ItemNo = "10",
                Material = PoI.MaterialCode,
                Quantity = PoI.Quantity
            });

            try
            {
                List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                PMaterial Mat = Mats[0];
                if (Mat.CurrentPrice == 0)
                {
                    return "Price is Not updated for this material " + PoI.MaterialCode + ". Please contact Admin.";
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
            catch(Exception e)
            {
                return e.Message;
            }
            fillItem(); 
            return "";
        } 

        void FillMessage(List<PDMS_Material> Supersede, Dictionary<string, string> MaterialIssue)
        {
            if (Supersede.Count != 0)
            {
                gvSupersede.DataSource = Supersede;
                gvSupersede.DataBind();
                MPE_Supersede.Show();
            }

            if (MaterialIssue.Count != 0)
            {
                gvMaterialIssue.DataSource = MaterialIssue;
                gvMaterialIssue.DataBind();
                MPE_Supersede.Show();
            }
            gvPOItem.DataSource = PurchaseOrderItem_Insert;
            gvPOItem.DataBind();
            hdfItemCount.Value = PurchaseOrderItem_Insert.Count().ToString();
            fillItem();
        }

        void ClearIssueGV()
        {
            gvSupersede.DataSource = null;
            gvSupersede.DataBind();
            gvMaterialIssue.DataSource = null;
            gvMaterialIssue.DataBind();
        }
        void HeaderFieldVisibleConrol()
        {
            if(PurchaseOrderItem_Insert.Count == 0)
            {
                ddlDealer.Enabled = true;
                ddlDealerOffice.Enabled = true;
                ddlOrderTo.Enabled = true;
                ddlVendor.Enabled = true;
                ddlPurchaseOrderType.Enabled = true;
                ddlDivision.Enabled = true;
                txtReferenceNo.Enabled = true;
                txtRemarks.Enabled = true;
            }
            else
            {
                ddlDealer.Enabled = false;
                ddlDealerOffice.Enabled = false;
                ddlOrderTo.Enabled = false;
                ddlVendor.Enabled = false;
                ddlPurchaseOrderType.Enabled = false;
                ddlDivision.Enabled = false;
                txtReferenceNo.Enabled = false;
                txtRemarks.Enabled = false;
            }
        }

        protected void BtnVendorStock_Click(object sender, EventArgs e)
        { 
            try
            {
                lblMessage.Text = "";
                lblMessage.ForeColor = Color.Red;
                decimal Stock = 0;
                if (ddlOrderTo.SelectedValue == "1")
                {
                    Stock = new BSap().GetMaterialStock(hdfMaterialCode.Value);
                }
                else
                {
                    List<PDMS_DealerOffice> VendorOffice = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlVendor.SelectedValue), null, null);
                    foreach (PDMS_DealerOffice Office in VendorOffice)
                    {
                        PDealerStock s = new BInventory().GetDealerStockCountByID(Convert.ToInt32(ddlVendor.SelectedValue), Office.OfficeID, Convert.ToInt64(hdfMaterialID.Value));
                        Stock = Stock + s.UnrestrictedQty;
                    }
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "These Material Stock is available : " + Stock;
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }

        protected void BtnCurrentStock_Click(object sender, EventArgs e)
        {
            try
            { 
                lblMessage.ForeColor = Color.Red;
                 
                if (ddlDealer.SelectedValue == "0")
                {
                    ddlDealer.BorderColor = Color.Red;
                    lblMessage.Text = "Please select the Dealer.";
                    return;
                }
                if (ddlDealerOffice.SelectedValue == "0")
                {
                    ddlDealerOffice.BorderColor = Color.Red;
                    lblMessage.Text = "Please select the Dealer Office.";
                    return;
                }
                if (string.IsNullOrEmpty(hdfMaterialID.Value))
                {
                    lblMessage.Text = "Please select the Material.";
                }

                PDealerStock s = new BInventory().GetDealerStockCountByID(Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlDealerOffice.SelectedValue), Convert.ToInt64(hdfMaterialID.Value));

                if (s != null)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "On Order Qty : " + s.OnOrderQty.ToString() 
                        + ", Transit Qty : " + s.TransitQty.ToString() 
                        + ", Unrestricted Qty : " + s.UnrestrictedQty.ToString()
                        + ", Reserved Qty : " + s.ReservedQty.ToString();
                }
                else
                {
                    lblMessage.Text = "Stock is not available";
                }                 
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
    }
}