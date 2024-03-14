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

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SalesOrderView : System.Web.UI.UserControl
    {
        public PSaleOrder SaleOrderByID
        {
            get
            {
                if (ViewState["SaleOrderByID"] == null)
                {
                    ViewState["SaleOrderByID"] = new PSaleOrder();
                }
                return (PSaleOrder)ViewState["SaleOrderByID"];
            }
            set
            {
                ViewState["SaleOrderByID"] = value;
            }
        }
        public List<PSaleOrderItem_Insert> SOItem_Insert
        {
            get
            {
                if (ViewState["SOItem_Insert"] == null)
                {
                    ViewState["SOItem_Insert"] = new List<PSaleOrderItem_Insert>();
                }
                return (List<PSaleOrderItem_Insert>)ViewState["SOItem_Insert"];
            }
            set
            {
                ViewState["SOItem_Insert"] = value;
            }
        }
        public PSaleOrder_Insert SO_Insert
        {
            get
            {
                if (ViewState["SO_Insert"] == null)
                {
                    ViewState["SO_Insert"] = new PSaleOrder_Insert();
                }
                return (PSaleOrder_Insert)ViewState["SO_Insert"];
            }
            set
            {
                ViewState["SO_Insert"] = value;
            }
        }
        public List<PSaleOrderDeliveryItem_Insert> SODelivery_Insert
        {
            get
            {
                if (ViewState["SaleOrderDelivery_Insert"] == null)
                {
                    ViewState["SaleOrderDelivery_Insert"] = new List<PSaleOrderDeliveryItem_Insert>();
                }
                return (List<PSaleOrderDeliveryItem_Insert>)ViewState["SaleOrderDelivery_Insert"];
            }
            set
            {
                ViewState["SaleOrderDelivery_Insert"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessageAddSOItem.Text = "";
            lblMessageSOEdit.Text = "";  
            lblMessageCreateSODelivery.Text = "";

            if (!IsPostBack)
            {  
            }
        }         
        public void fillViewSO(long SaleOrderID)
        {
            SaleOrderByID = new BDMS_SalesOrder().GetSaleOrderByID(SaleOrderID);

            lblQuotationNumber.Text = SaleOrderByID.QuotationNumber;
            lblQuotationDate.Text = SaleOrderByID.QuotationDate.ToString("dd/MM/yyyy");
            lblSaleOrderNumber.Text = SaleOrderByID.SaleOrderNumber;
            lblSaleOrderDate.Text =  (SaleOrderByID.SaleOrderDate == null) ? null: Convert.ToDateTime(SaleOrderByID.SaleOrderDate).ToString("dd/MM/yyyy");
            lblProformaInvoiceNumber.Text = SaleOrderByID.ProformaInvoiceNumber;
            lblProformaInvoiceDate.Text = (SaleOrderByID.ProformaInvoiceDate == null) ? null : Convert.ToDateTime(SaleOrderByID.ProformaInvoiceDate).ToString("dd/MM/yyyy");
            lblDealerOffice.Text = SaleOrderByID.Dealer.DealerOffice.OfficeName;
            //lblContactPerson.Text = SaleOrderByID.ContactPerson;
            lblRemarks.Text = SaleOrderByID.Remarks; 
            lblFrieghtPaidBy.Text = SaleOrderByID.FrieghtPaidBy;
            lblTaxType.Text = SaleOrderByID.TaxType;
            //lblCustomer.Text = SaleOrderByID.Customer.CustomerCode + " " + SaleOrderByID.Customer.CustomerName;
            lblContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;
            lblExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString("dd/MM/yyyy"); 
            lblAttn.Text = SaleOrderByID.Attn;
            lblSODealer.Text = SaleOrderByID.Dealer.DealerCode + " " + SaleOrderByID.Dealer.DealerName;
            lblStatus.Text = SaleOrderByID.SaleOrderStatus.Status;
            lblDivision.Text = SaleOrderByID.Division.DivisionCode;
            lblProduct.Text = SaleOrderByID.Product == null ? "" : SaleOrderByID.Product.Product;
            lblInsurancePaidBy.Text = SaleOrderByID.InsurancePaidBy;
            lblEquipmentSerialNo.Text = SaleOrderByID.Equipment == null ? "" : SaleOrderByID.Equipment.EquipmentSerialNo;
            lblSaleOrderType.Text = SaleOrderByID.SaleOrderType.SaleOrderType;
            lblSalesEngnieer.Text = SaleOrderByID.SalesEngineer == null ? "" : SaleOrderByID.SalesEngineer.ContactName;
            lblHeaderDiscount.Text = SaleOrderByID.HeaderDiscountPercentage.ToString();

            decimal Price = 0, TaxableValue = 0, TaxValue = 0, NetAmount = 0;
            foreach (PSaleOrderItem SaleOrderItem in SaleOrderByID.SaleOrderItems)
            {
                Price = Price + SaleOrderItem.Value;
                TaxableValue = TaxableValue + SaleOrderItem.TaxableValue;
                TaxValue = TaxValue + SaleOrderItem.Material.CGSTValue + SaleOrderItem.Material.SGSTValue + SaleOrderItem.Material.IGSTValue;
                NetAmount = NetAmount + SaleOrderItem.TaxableValue + SaleOrderItem.Material.CGSTValue + SaleOrderItem.Material.SGSTValue + SaleOrderItem.Material.IGSTValue;
                SaleOrderItem.NetAmount = SaleOrderItem.TaxableValue + SaleOrderItem.Material.CGSTValue + SaleOrderItem.Material.SGSTValue + SaleOrderItem.Material.IGSTValue;
            }

            lblPrice.Text = Price.ToString();
            lblTaxableValue.Text = TaxableValue.ToString();
            lblTaxValue.Text = TaxValue.ToString();
            lblNetAmount.Text = NetAmount.ToString();

            gvSOItem.DataSource = SaleOrderByID.SaleOrderItems;
            gvSOItem.DataBind();
            SaleOrderByID.Customer = new BDMS_Customer().GetCustomerByID(SaleOrderByID.Customer.CustomerID);
            UC_CustomerView.fillCustomer(SaleOrderByID.Customer);
            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbEditSaleOrder.Visible = true;
            lbCancelSaleOrder.Visible = true;
            lbAddSaleOrderItem.Visible = true;
           // lbGenerateQuotation.Visible = true;
            lbGenerateProformaInvoice.Visible = true;
            lbReleaseSaleOrder.Visible = true;
            lbDelivery.Visible = true;
            
            int StatusID = SaleOrderByID.SaleOrderStatus.StatusID;
            
            if (StatusID == (short)AjaxOneStatus.SaleOrder_Quotation   || StatusID == (short)AjaxOneStatus.SaleOrder_ProformaInvoice) // Draft
            {
                lbDelivery.Visible = false;
            }
            if (StatusID == (short)AjaxOneStatus.SaleOrder_OrderPlaced) //Order Placed
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
            }
            if (StatusID == (short)AjaxOneStatus.SaleOrder_Cancelled) //Cancelled
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                //lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
                lbDelivery.Visible = false;
            }
            if (StatusID == (short)AjaxOneStatus.SaleOrder_Delivered) //Delivered
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
               // lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
                lbDelivery.Visible = false;
            }
            if (StatusID == (short)AjaxOneStatus.SaleOrder_PartiallyDelivered) //Partially Delivered
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                //lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
            }

            if (SaleOrderByID.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder)
            { 
                lbAddSaleOrderItem.Visible = false; 
            }
            DisableSOItemEditDelete();
        }
        private void DisableSOItemEditDelete()
        {
            int StatusID = SaleOrderByID.SaleOrderStatus.StatusID;
            if (StatusID == (short)AjaxOneStatus.SaleOrder_OrderPlaced 
                || StatusID == (short)AjaxOneStatus.SaleOrder_Delivered 
                || StatusID == (short)AjaxOneStatus.SaleOrder_Cancelled 
                )
            {
                for (int i = 0; i < gvSOItem.Rows.Count; i++)
                {
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnEdit")).Visible = false;
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnDelete")).Visible = false;
                }
            }
            if (SaleOrderByID.SaleOrderType.SaleOrderTypeID == (short)SaleOrderType.IntraDealerOrder)
            {
                for (int i = 0; i < gvSOItem.Rows.Count; i++)
                {
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnDelete")).Visible = false;
                }
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;  
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbCancelSaleOrder")
            { 
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/CancelSaleOrder?SaleOrderID=" + SaleOrderByID.SaleOrderID));
                // PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SaleOrderByID.SaleOrderID + "&StatusID=" + 23));
                PApiResult Results = new BDMS_SalesOrder().UpdateSaleOrderStatus(SaleOrderByID.SaleOrderID, (short)AjaxOneStatus.SaleOrder_Cancelled);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message; 
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            else if (lbActions.ID == "lbEditSaleOrder")
            {                
                Edit();
                MPE_SaleOrderEdit.Show();
            }
            else if (lbActions.ID == "lbAddSaleOrderItem")
            {
                MPE_SaleOrderItemAdd.Show();
            }
            else if (lbActions.ID == "lbReleaseSaleOrder")
            { 
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/ReleaseSaleOrder?SaleOrderID=" + SaleOrderByID.SaleOrderID));
                // PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SaleOrderByID.SaleOrderID + "&StatusID=" + 13));
                PApiResult Results = new BDMS_SalesOrder().UpdateSaleOrderStatus(SaleOrderByID.SaleOrderID,(short)AjaxOneStatus.SaleOrder_OrderPlaced);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message; 
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            } 
            else if (lbActions.ID == "lbGenerateProformaInvoice")
            { 
                PApiResult Results = new BDMS_SalesOrder().UpdateSaleOrderStatus(SaleOrderByID.SaleOrderID, (short)AjaxOneStatus.SaleOrder_ProformaInvoice);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message; 
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            else if (lbActions.ID == "lbDelivery")
            {
                MPE_Delivery.Show();
                List<PDMS_EquipmentHeader> EQs = new BDMS_Equipment().GetEquipmentForCreateICTicket(Convert.ToInt64(SaleOrderByID.Dealer.DealerID), null, null);
                new DDLBind(ddlEquipment, EQs, "EquipmentSerialNo", "EquipmentHeaderID", true, "Select");
                cxDispatchDate.StartDate = DateTime.Now;
                txtBoxDispatchDate.Text = DateTime.Now.ToShortDateString();

                cxCourierDate.StartDate = DateTime.Now;
                txtBoxCourierDate.Text = DateTime.Now.ToShortDateString();

                cxPickupDate.StartDate = DateTime.Now;
                txtBoxPickupDate.Text = DateTime.Now.ToShortDateString();

                if(SaleOrderByID.SaleOrderType.SaleOrderTypeID == 4)
                {
                    divEquipment.Visible = true;
                }

                SODelivery_Insert = new List<PSaleOrderDeliveryItem_Insert>();
                foreach (PSaleOrderItem Item in SaleOrderByID.SaleOrderItems)
                {
                    SODelivery_Insert.Add(new PSaleOrderDeliveryItem_Insert()
                    {
                        SaleOrderID = SaleOrderByID.SaleOrderID,
                        SaleOrderItemID = Item.SaleOrderItemID,
                        MaterialCode = Item.Material.MaterialCode,
                        MaterialDescription = Item.Material.MaterialDescription,
                        UOM = Item.Material.BaseUnit,
                        Quantity = Item.Quantity - Item.DeliveredQuantity,
                        DeliveryQuantity = Item.Quantity - Item.DeliveredQuantity,
                        Value = Item.Value,
                        TaxableValue = Item.TaxableValue,
                        CGST = Item.Material.CGST,
                        CGSTValue = Item.Material.CGSTValue,
                        SGST = Item.Material.SGST,
                        SGSTValue = Item.Material.SGSTValue,
                        IGST = Item.Material.IGST,
                        IGSTValue = Item.Material.IGSTValue,
                    });
                }

                List<PDMS_CustomerShipTo> ShipTos = new BDMS_Customer().GetCustomerShopTo(null, SaleOrderByID.Customer.CustomerID);
                foreach (PDMS_CustomerShipTo ShipTo in ShipTos)
                {
                    ShipTo.Address1 = ShipTo.Address1 + "," + ShipTo.Address2 + "," + ShipTo.Address3 + "," + ShipTo.District.District + "," + ShipTo.State.State;
                }
                new DDLBind(ddlShiftTo, ShipTos, "Address1", "CustomerShipToID", true, "Select");

                lblBillingAddress.Text = SaleOrderByID.Customer.Address1 + "," 
                    + SaleOrderByID.Customer.Address2 + "," 
                    + SaleOrderByID.Customer.Address3 + "," 
                    + SaleOrderByID.Customer.District.District + "," 
                    + SaleOrderByID.Customer.State.State;

                lblDeliveryAddress.Text = lblBillingAddress.Text;

                gvDelivery.DataSource = SODelivery_Insert;
                gvDelivery.DataBind();
            }
        } 
        public void Edit()
        {
            cxExpectedDeliveryDate.StartDate = DateTime.Now; 
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(SaleOrderByID.Dealer.DealerID, null, null), "OfficeName", "OfficeID", true, "Select");
            ddlOfficeName.SelectedValue = SaleOrderByID.Dealer.DealerOffice.OfficeID.ToString();

            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;  
             
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, SaleOrderByID.Dealer.DealerID, true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
            ddlSalesEngineer.SelectedValue = SaleOrderByID.SalesEngineer == null ? "0" : SaleOrderByID.SalesEngineer.UserID.ToString();
             
            txtContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;  
            txtRemarks.Text = SaleOrderByID.Remarks;
            txtExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty( SaleOrderByID.InsurancePaidBy))
                ddlInsurancePaidBy.SelectedValue = ddlInsurancePaidBy.Items.FindByText(SaleOrderByID.InsurancePaidBy).Value;
            if (!string.IsNullOrEmpty(SaleOrderByID.FrieghtPaidBy))
                ddlFrieghtPaidBy.SelectedValue = ddlFrieghtPaidBy.Items.FindByText(SaleOrderByID.FrieghtPaidBy).Value;
            txtAttn.Text = SaleOrderByID.Attn;
            if (SaleOrderByID.Product != null)
                ddlProduct.SelectedValue = SaleOrderByID.Product.ProductID.ToString();
            ddlTaxType.SelectedValue = ddlTaxType.Items.FindByText(SaleOrderByID.TaxType).Value;
            txtBoxHeaderDiscountPercent.Text = SaleOrderByID.HeaderDiscountPercentage.ToString();
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            MPE_SaleOrderEdit.Show();
        }
        protected void btnUpdateSO_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red; 
            lblMessage.Text = "";
            lblMessageSOEdit.ForeColor = Color.Red; 
            lblMessageSOEdit.Text = "";
            try
            {
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageSOEdit.Text = Message;
                    MPE_SaleOrderEdit.Show();
                    return;
                }

                SO_Insert = Read();
                //SO_Insert.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                //string result = new BAPI().ApiPut("SaleOrder", SO_Insert);
                string result = new BAPI().ApiPut("SaleOrder/UpdateSaleOrder", SO_Insert);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessageSOEdit.Text = Result.Message;
                    MPE_SaleOrderEdit.Show();
                    return;
                }
                lblMessage.Text = Result.Message; 
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            catch (Exception ex)
            {
                lblMessageSOEdit.Text = ex.Message.ToString();
                MPE_SaleOrderEdit.Show();
                return;
            }
        }
        protected void btnSaveSOItem_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red; 
            lblMessage.Text = "";
            lblMessageAddSOItem.ForeColor = Color.Red; 
            lblMessageAddSOItem.Text = "";
            try
            {
                string Message = ValidationItem();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageAddSOItem.Text = Message;
                    MPE_SaleOrderItemAdd.Show();
                    return;
                }

                PSaleOrderItem_Insert pSaleOrderItem = new PSaleOrderItem_Insert();
                pSaleOrderItem = ReadItem();
                string result = new BAPI().ApiPut("SaleOrder/UpdateSaleOrderItem", pSaleOrderItem);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessageAddSOItem.Text = Result.Message;
                    MPE_SaleOrderItemAdd.Show();
                    return;
                }
                lblMessage.Text = Result.Message; 
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
                
            }
            catch (Exception ex)
            {
                lblMessageAddSOItem.Text = ex.Message.ToString();
                MPE_SaleOrderItemAdd.Show();
                return;
            }
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            //if (SaleOrderByID.SaleOrderStatus.StatusID == 23)
            //{
            //    lblMessage.Text = "Sale Order is Cancelled.";
            //    return;
            //}
            //if (SaleOrderByID.SaleOrderStatus.StatusID == 15)
            //{
            //    lblMessage.Text = "Sale Order is delivered. Item not allowed edit.";
            //    return;
            //}
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");
                PSaleOrderItem Item = (PSaleOrderItem)SaleOrderByID.SaleOrderItems.Where(M => M.SaleOrderItemID == Convert.ToInt64(lblSaleOrderItemID.Text)).ToList()[0];

                PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
                SoI.SaleOrderItemID = Item.SaleOrderItemID;
                SoI.MaterialID = Item.Material.MaterialID;
                SoI.MaterialCode = Item.Material.MaterialCode;
                SoI.Quantity = Item.Quantity;
                SoI.PerRate = Item.PerRate;
                SoI.Value = Item.Value;
                SoI.ItemDiscountPercentage = Item.ItemDiscountPercentage;
                SoI.TaxableValue = Item.TaxableValue;
                SoI.SGST = Item.Material.SGST;
                SoI.SGSTValue = Item.Material.SGSTValue;
                SoI.CGST = Item.Material.CGST;
                SoI.CGSTValue = Item.Material.CGSTValue;
                SoI.IGST = Item.Material.IGST;
                SoI.IGSTValue = Item.Material.IGSTValue;
                SoI.StatusID = 20;

                PSaleOrder_Insert SO = new PSaleOrder_Insert();
                SO.SaleOrderID = Convert.ToInt64(SaleOrderByID.SaleOrderID);
                SO.DealerID = Convert.ToInt32(SaleOrderByID.Dealer.DealerID);
                SO.CustomerID = Convert.ToInt32(SaleOrderByID.Customer.CustomerID);
                SO.StatusID = 23;
                SO.OfficeID = Convert.ToInt32(SaleOrderByID.Dealer.DealerOffice.OfficeID); 
                SO.ContactPersonNumber = SaleOrderByID.ContactPersonNumber.Trim();
                SO.DivisionID = Convert.ToInt32(SaleOrderByID.Division.DivisionID);
                SO.Remarks = SaleOrderByID.Remarks.Trim();
                SO.ExpectedDeliveryDate = SaleOrderByID.ExpectedDeliveryDate;
                SO.InsurancePaidBy = SaleOrderByID.InsurancePaidBy.Trim();
                SO.FrieghtPaidBy = SaleOrderByID.FrieghtPaidBy.Trim();
                SO.Attn = SaleOrderByID.Attn.Trim();
                SO.ProductID = Convert.ToInt32(SaleOrderByID.Product.ProductID); 
                SO.TaxType = SaleOrderByID.TaxType.Trim();
                SO.SaleOrderTypeID = SaleOrderByID.SaleOrderType.SaleOrderTypeID;
                SO.SalesEngineerID = SaleOrderByID.SalesEngineer.UserID;
                SO.HeaderDiscountPercentage = SaleOrderByID.HeaderDiscountPercentage;

                SO.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                SO.SaleOrderItems.Add(SoI);

                string result = new BAPI().ApiPut("SaleOrder", SO);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message; 
                    return;
                }
                lblMessage.Text = Result.Message; 
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                return;
            }
        }
        public PSaleOrder_Insert Read()
        {
            PSaleOrder_Insert SO = new PSaleOrder_Insert();
            SO.SaleOrderID = SaleOrderByID.SaleOrderID;
            SO.DealerID = SaleOrderByID.Dealer.DealerID;
           // SO.CustomerID = Convert.ToInt32(hdfCustomerId.Value);
            SO.StatusID = 11;
            SO.OfficeID = Convert.ToInt32(ddlOfficeName.SelectedValue); 
            SO.ContactPersonNumber = txtContactPersonNumber.Text.Trim();
            //SO.DivisionID = Convert.ToInt32(ddlDivision.SelectedValue); 
            SO.Remarks = txtRemarks.Text.Trim();
            SO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            SO.InsurancePaidBy = ddlInsurancePaidBy.SelectedItem.Text;
            SO.FrieghtPaidBy = ddlFrieghtPaidBy.SelectedItem.Text;
            SO.Attn = txtAttn.Text.Trim();
            SO.ProductID = Convert.ToInt32(ddlProduct.SelectedValue); 
            SO.TaxType = ddlTaxType.SelectedItem.Text;
            SO.SaleOrderTypeID = SaleOrderByID.SaleOrderType.SaleOrderTypeID;
            SO.SalesEngineerID = Convert.ToInt32(ddlSalesEngineer.SelectedValue);
            SO.HeaderDiscountPercentage = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
            return SO;
        }
        public PSaleOrderItem_Insert ReadItem()
        {
            PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
            SoI.SaleOrderID = SaleOrderByID.SaleOrderID;
            SoI.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SoI.MaterialCode = hdfMaterialCode.Value;
            SoI.Quantity = Convert.ToInt32(txtQty.Text.Trim());

            PDMS_Material m = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(SoI.MaterialID), null, null, null, null)[0];
            if (string.IsNullOrEmpty(m.HSN))
            {
                throw new Exception("HSN Code is not updated for this Material. Please contact Parts Admin.");
            }

            //string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(SaleOrderByID.Customer.CustomerID)).CustomerCode;
            //string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(SaleOrderByID.Dealer.DealerID), "").DealerCode;
            //string Material = SoI.MaterialCode;
            //string IV_SEC_SALES = "";
            //string PriceDate = "";
            //string IsWarrenty = "false";
            //PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, "101_DPPOR_PURC_ORDER_HDR", 1, Material, SoI.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);

            List<PMaterial_Api> Material_SapS = new List<PMaterial_Api>();
            Material_SapS.Add(new PMaterial_Api()
            {
                MaterialCode = SoI.MaterialCode,
                Quantity = SoI.Quantity,
                Item = (Material_SapS.Count + 1) * 10
            }); 
            
            PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
            MaterialPrice.Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(SaleOrderByID.Customer.CustomerID)).CustomerCode;
            MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(SaleOrderByID.Dealer.DealerID), "").DealerCode;
            MaterialPrice.OrderType = "101_DSSOR_SALES_ORDER_HDR";

            MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
            MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
            {
                ItemNo = "10",
                Material = SoI.MaterialCode,
                Quantity = SoI.Quantity
            });
            List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
            PMaterial Mat = Mats[0];

            //Mat.CurrentPrice = Convert.ToDecimal(1000);
            //Mat.Discount = Convert.ToDecimal(0);
            //Mat.TaxablePrice = Convert.ToDecimal(1000);
            //Mat.SGST = Convert.ToDecimal(9);
            //Mat.SGSTValue = Convert.ToDecimal(90);
            //Mat.CGST = Convert.ToDecimal(9);
            //Mat.CGSTValue = Convert.ToDecimal(90);
            //Mat.IGST = Convert.ToDecimal(0);
            //Mat.IGSTValue = Convert.ToDecimal(0);

            if (Mat.CurrentPrice <= 0)
            {
                throw new Exception("Please maintain the Price for Material " + SoI.MaterialCode + " in SAP.");
            }
            if (Mat.SGST <= 0 && Mat.IGST <= 0)
            {
                throw new Exception("Please maintain the Tax for Material " + SoI.MaterialCode + " in SAP.");
            }
            if (Mat.SGSTValue <= 0 && Mat.IGSTValue <= 0)
            {
                throw new Exception("GST Tax value not found this Material " + SoI.MaterialCode + " in SAP.");
            }

            SoI.PerRate = Mat.CurrentPrice / SoI.Quantity;
            SoI.Value = Mat.CurrentPrice;
            //SoI.Discount = Mat.Discount;
            //SoI.TaxableValue = Mat.TaxablePrice;
            //SoI.Discount = SaleOrderByID.HeaderDiscount > 0 ? SaleOrderByID.HeaderDiscount : Mat.Discount;
            SoI.ItemDiscountPercentage = SaleOrderByID.HeaderDiscountPercentage > 0 ? 0 : Mat.Discount;
            SoI.DiscountValue = SaleOrderByID.HeaderDiscountPercentage > 0 ? (Mat.CurrentPrice * (SaleOrderByID.HeaderDiscountPercentage / 100)) : Mat.Discount;

            SoI.TaxableValue = SaleOrderByID.HeaderDiscountPercentage > 0 ? (Mat.CurrentPrice - (Mat.CurrentPrice * (SaleOrderByID.HeaderDiscountPercentage / 100))) : Mat.TaxablePrice;
            //SoI.SGST = Mat.SGST;
            //SoI.SGSTValue = Mat.SGSTValue;
            //SoI.CGST = Mat.CGST;
            //SoI.CGSTValue = Mat.CGSTValue;
            //SoI.IGST = Mat.IGST;
            //SoI.IGSTValue = Mat.IGSTValue;
            if (SaleOrderByID.TaxType == "SGST & CGST")
            {
                SoI.SGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                SoI.SGSTValue = SaleOrderByID.HeaderDiscountPercentage > 0 ? (SoI.TaxableValue * (SoI.SGST / 100)) : (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                SoI.CGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                SoI.CGSTValue = SaleOrderByID.HeaderDiscountPercentage > 0 ? (SoI.TaxableValue * (SoI.CGST / 100)) : (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                SoI.IGST = 0;
                SoI.IGSTValue = 0;
            }
            else 
            {
                SoI.SGST = 0;
                SoI.SGSTValue = 0;
                SoI.CGST = 0;
                SoI.CGSTValue = 0;
                SoI.IGST = Mat.SGST + Mat.CGST + Mat.IGST;
                SoI.IGSTValue = SaleOrderByID.HeaderDiscountPercentage > 0 ? (SoI.TaxableValue * (SoI.IGST / 100)) : (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue);
            }

            SoI.NetAmount = SoI.TaxableValue + SoI.SGSTValue + SoI.CGSTValue + SoI.IGSTValue;
            SoI.StatusID = 19;
            SoI.MaterialDescription = m.MaterialDescription;
            SoI.HSN = m.HSN;
            SoI.UOM = m.BaseUnit;
            return SoI;
        }
        public string Validation()
        { 
            ddlOfficeName.BorderColor = Color.Silver;
            //txtCustomer.BorderColor = Color.Silver;
            txtContactPersonNumber.BorderColor = Color.Silver;
            //ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            ddlTaxType.BorderColor = Color.Silver;
            txtBoxHeaderDiscountPercent.BorderColor = Color.Silver;
            string Message = "";
            if (ddlOfficeName.SelectedValue == "0")
            {
                ddlOfficeName.BorderColor = Color.Red;
                return "Please select the Dealer Office.";
            }
            //if (string.IsNullOrEmpty(hdfCustomerId.Value))
            //{
            //    txtCustomer.BorderColor = Color.Red;
            //    return "Please enter Customer.";
            //}
            if (!string.IsNullOrEmpty(txtContactPersonNumber.Text.Trim()))
            {
                long longCheck;
                if (!long.TryParse(txtContactPersonNumber.Text.Trim(), out longCheck))
                {
                    txtContactPersonNumber.BorderColor = Color.Red;
                    return "Mobile should be 10 Digit.";
                }
            }
            //if (ddlDivision.SelectedValue == "0")
            //{
            //    ddlDivision.BorderColor = Color.Red;
            //    return "Please select the Division.";
            //}
            if (ddlProduct.SelectedValue == "0")
            {
                ddlProduct.BorderColor = Color.Red;
                return "Please select the Product.";
            }
            if (string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
            {
                txtExpectedDeliveryDate.BorderColor = Color.Red;
                return "Please enter the Expected Delivery Date.";
            }
            if (ddlTaxType.SelectedValue == "0")
            {
                ddlTaxType.BorderColor = Color.Red;
                return "Please select Tax.";
            }
            decimal value;
            if (!decimal.TryParse(txtBoxHeaderDiscountPercent.Text, out value))
            {
                txtBoxHeaderDiscountPercent.BackColor = Color.Red;
                return "Please enter correct format in Header Discount Percent.";
            }
            if (Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text) >= 100)
            {
                txtBoxHeaderDiscountPercent.BackColor = Color.Red;
                return "Discount Percentage cannot exceed 100.";
            }
            return Message;
        }
        public string ValidationItem()
        {
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                return "Please select the Material.";
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                return "Please enter the Qty.";
            }

            foreach (PSaleOrderItem Item in SaleOrderByID.SaleOrderItems)
            {
                if (Item.Material.MaterialID == Convert.ToInt32(hdfMaterialID.Value))
                {
                    return "Duplicate Material.";
                }
            }

            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty.";
            }
            return "";
        }
        protected void lnkBtnItemAction_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red; 
            //if (SaleOrderByID.SaleOrderStatus.StatusID == 23)
            //{
            //    lblMessage.Text = "Sale Order is Cancelled.";
            //    return;
            //}
            //if (SaleOrderByID.SaleOrderStatus.StatusID == 15)
            //{
            //    lblMessage.Text = "Sale Order is Delivered. Item not allowed edit.";
            //    return;
            //}

            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;


            LinkButton lnkBtnEdit = (LinkButton)gvRow.FindControl("lnkBtnEdit");
            LinkButton lnkBtnUpdate = (LinkButton)gvRow.FindControl("lnkBtnupdate");
            LinkButton lnkBtnCancel = (LinkButton)gvRow.FindControl("lnkBtnCancel");
            LinkButton lnkBtnDelete = (LinkButton)gvRow.FindControl("lnkBtnDelete");
            //LinkButton lblMaterialRemove = (LinkButton)gvRow.FindControl("lblMaterialRemove");

            TextBox txtBoxQuantity = (TextBox)gvRow.FindControl("txtBoxQuantity");
            Label lblQuantity = (Label)gvRow.FindControl("lblQuantity");
            TextBox txtBoxDiscountPercent = (TextBox)gvRow.FindControl("txtBoxDiscountPercent");
            Label lblDiscountPercent = (Label)gvRow.FindControl("lblDiscountPercent");
            
            if (lbActions.ID == "lnkBtnEdit")
            {
                lnkBtnEdit.Visible = false;
                lnkBtnUpdate.Visible = true;
                lnkBtnCancel.Visible = true;
                lnkBtnDelete.Visible = false;
                //lblMaterialRemove.Visible = false;

                txtBoxQuantity.Visible = true;
                lblQuantity.Visible = false;
                txtBoxDiscountPercent.Visible = true;
                lblDiscountPercent.Visible = false;
            }
            else if (lbActions.ID == "lnkBtnUpdate")
            {
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");

                decimal value;
                if (!decimal.TryParse(txtBoxQuantity.Text, out value))
                {
                    lblMessage.Text = "Please enter correct format in Qty.";
                    return;
                }
                if (!decimal.TryParse(txtBoxDiscountPercent.Text, out value))
                {
                    lblMessage.Text = "Please enter correct format in Discount Percent.";
                    return;
                }
                decimal IDiscount = Convert.ToDecimal(txtBoxDiscountPercent.Text);
                if (SaleOrderByID.HeaderDiscountPercentage + IDiscount >= 100)
                {
                    lblMessage.Text = "Discount Percentage cannot exceed 100.";
                    return;
                }

                PSaleOrderItem_Insert item_Insert = new PSaleOrderItem_Insert();
                item_Insert.SaleOrderItemID = Convert.ToInt64(lblSaleOrderItemID.Text);
                item_Insert.Quantity = Convert.ToDecimal(txtBoxQuantity.Text);
                item_Insert.ItemDiscountPercentage = IDiscount;
                item_Insert.StatusID = 19;

                string result = new BAPI().ApiPut("SaleOrder/UpdateSaleOrderItem", item_Insert);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
                lnkBtnEdit.Visible = true;
                lnkBtnUpdate.Visible = false;
                lnkBtnCancel.Visible = false;
                lnkBtnDelete.Visible = true;
                //lblMaterialRemove.Visible = true;

                txtBoxQuantity.Visible = false;
                lblQuantity.Visible = true;
            }
            else if (lbActions.ID == "lnkBtnCancel")
            {
                lnkBtnEdit.Visible = true;
                lnkBtnUpdate.Visible = false;
                lnkBtnCancel.Visible = false;
                lnkBtnDelete.Visible = true;
                //lblMaterialRemove.Visible = true;
                txtBoxQuantity.Visible = false;
                lblQuantity.Visible = true;
                txtBoxDiscountPercent.Visible = false;
                lblDiscountPercent.Visible = true;
            }
            else if (lbActions.ID == "lnkBtnDelete")
            { 
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/CancelSaleOrderItem?SaleOrderItemID=" + Convert.ToInt64(lblSaleOrderItemID.Text)));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
        }
        protected void btnSaveDelivery_Click(object sender, EventArgs e)
        {
            lblMessageCreateSODelivery.ForeColor = Color.Red; 
            lblMessageCreateSODelivery.Text = "";
            MPE_Delivery.Show();
            try
            {
                if(SaleOrderByID.SaleOrderType.SaleOrderTypeID == 4)
                {
                    lblMessageCreateSODelivery.Text = "Please select the Equipment.";
                    return;
                }

                readSaleOrderDelivery();
                foreach (PSaleOrderDeliveryItem_Insert T in SODelivery_Insert)
                {
                    T.ShiftToID = ddlShiftTo.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlShiftTo.SelectedValue);
                    T.NetWeight = Convert.ToDecimal(txtBoxNetWeight.Text);
                    T.Remarks = txtBoxRemarks.Text.Trim();
                    T.DispatchDate = Convert.ToDateTime(txtBoxDispatchDate.Text.Trim());
                    T.CourierID = txtBoxCourierId.Text.Trim();
                    T.CourierDate = Convert.ToDateTime(txtBoxCourierDate.Text.Trim());
                    T.CourierCompanyName = txtBoxCourierCompanyName.Text.Trim();
                    T.CourierPerson = txtBoxCourierPerson.Text.Trim();
                    T.LRNo = txtBoxLRNo.Text.Trim();
                    T.PackingDescription = txtBoxPackingDescription.Text.Trim();
                    T.PackingRemarks = txtBoxPackingRemarks.Text.Trim();
                    T.TransportDetails = txtBoxTransportDetails.Text.Trim();
                    T.TransportMode = ddlTransportMode.SelectedItem.Text;
                    T.EquipmentHeaderID = ddlEquipment.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlEquipment.SelectedValue);
                    T.PickupDate = Convert.ToDateTime(txtBoxPickupDate.Text.Trim());
                }
                
                PApiResult Result = new BDMS_SalesOrder().InsertSaleOrderDelivery(SODelivery_Insert);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessageCreateSODelivery.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
                MPE_Delivery.Hide();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        void readSaleOrderDelivery()
        {
            foreach (GridViewRow row in gvDelivery.Rows)
            {
                Label lblSaleOrderItemID = (Label)row.FindControl("lblSaleOrderItemID");
                Label lblOrderQty = (Label)row.FindControl("lblOrderQty");
                TextBox txtDeliveryQuantity = (TextBox)row.FindControl("txtDeliveryQuantity");

                decimal OrderQty = Convert.ToDecimal(lblOrderQty.Text);
                decimal DeliveryQuantity = Convert.ToDecimal(txtDeliveryQuantity.Text);
                if (string.IsNullOrEmpty(txtDeliveryQuantity.Text))
                {
                    throw new Exception("Please enter the Delivery Quantity.");
                }
                if (DeliveryQuantity > OrderQty)
                {
                    throw new Exception("Please check the Delivery Quantity.");
                }
                foreach (PSaleOrderDeliveryItem_Insert Item in SODelivery_Insert)
                {
                    if (Convert.ToInt64(lblSaleOrderItemID.Text) == Item.SaleOrderItemID)
                    {
                        Item.DeliveryQuantity = DeliveryQuantity;
                        break;
                    }
                }
            }
        }
        protected void gvDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            readSaleOrderDelivery();
            gvDelivery.DataSource = SODelivery_Insert;
            gvDelivery.PageIndex = e.NewPageIndex;
            gvDelivery.DataBind();
            MPE_Delivery.Show();
        }
        protected void ddlShiftTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_Delivery.Show();
            if (ddlShiftTo.SelectedValue == "0")
            {
                lblDeliveryAddress.Text = lblBillingAddress.Text;
            }
            else
            {
                PDMS_CustomerShipTo ShiftTo = new BDMS_Customer().GetCustomerShopTo(Convert.ToInt64(ddlShiftTo.SelectedValue), SaleOrderByID.Customer.CustomerID)[0];
                lblDeliveryAddress.Text = ShiftTo.Address1 + "," + ShiftTo.Address2 + "," + ShiftTo.Address3 + "," + ShiftTo.District.District + "," + ShiftTo.State.State;
            }
        }
    }
}