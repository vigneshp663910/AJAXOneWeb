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
            if (!IsPostBack)
            {  
            }
        }         
        public void fillViewSO(long SaleOrderID)
        {
            SaleOrderByID = new BDMS_SalesOrder().GetSaleOrderByID(SaleOrderID);

            lblQuotationNumber.Text = SaleOrderByID.QuotationNumber;
            lblDealerOffice.Text = SaleOrderByID.Dealer.DealerOffice.OfficeName;
            lblContactPerson.Text = SaleOrderByID.ContactPerson;
            lblRemarks.Text = SaleOrderByID.Remarks;
            lblRefNumber.Text = SaleOrderByID.RefNumber;
            lblFrieghtPaidBy.Text = SaleOrderByID.FrieghtPaidBy;
            lblTaxType.Text = SaleOrderByID.TaxType;
            lblQuotationDate.Text = SaleOrderByID.QuotationDate.ToString();
            //lblCustomer.Text = SaleOrderByID.Customer.CustomerCode + " " + SaleOrderByID.Customer.CustomerName;
            lblContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;
            lblExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            lblRefDate.Text = SaleOrderByID.RefDate.ToString();
            lblAttn.Text = SaleOrderByID.Attn;
            lblSODealer.Text = SaleOrderByID.Dealer.DealerCode + " " + SaleOrderByID.Dealer.DealerName;
            lblStatus.Text = SaleOrderByID.SaleOrderStatus.Status;
            lblDivision.Text = SaleOrderByID.Division.DivisionCode;
            lblProduct.Text = SaleOrderByID.Product.Product;
            lblInsurancePaidBy.Text = SaleOrderByID.InsurancePaidBy;
            lblEquipmentSerialNo.Text = SaleOrderByID.EquipmentSerialNo;
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
            lbGenerateQuotation.Visible = true;
            lbGenerateProformaInvoice.Visible = true;
            lbReleaseSaleOrder.Visible = true;
            lbDelivery.Visible = true;
            
            int StatusID = SaleOrderByID.SaleOrderStatus.StatusID;
            
            if (StatusID == 11 || StatusID == 12) //Draft
            {
                lbDelivery.Visible = false;
            }
            if (StatusID == 13) //Order Placed
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
            }
            if (StatusID == 23) //Cancelled
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
                lbDelivery.Visible = false;
            }
            if (StatusID == 15) //Delivered
            {
                lbEditSaleOrder.Visible = false;
                lbCancelSaleOrder.Visible = false;
                lbAddSaleOrderItem.Visible = false;
                lbReleaseSaleOrder.Visible = false;
                lbGenerateQuotation.Visible = false;
                lbGenerateProformaInvoice.Visible = false;
            }
            DisableSOItemEditDelete();
        }
        private void DisableSOItemEditDelete()
        {
            if(SaleOrderByID.SaleOrderStatus.StatusID == 13 || SaleOrderByID.SaleOrderStatus.StatusID == 15 || SaleOrderByID.SaleOrderStatus.StatusID == 23)
            {
                for (int i = 0; i < gvSOItem.Rows.Count; i++)
                {
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnEdit")).Enabled = false;
                    ((LinkButton)gvSOItem.Rows[i].FindControl("lnkBtnDelete")).Enabled = false;
                }
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            lblMessage.Text = "";
            lblMessageSOEdit.ForeColor = Color.Red;
            lblMessageSOEdit.Visible = true;
            lblMessageSOEdit.Text = "";
            lblMessageAddSOItem.ForeColor = Color.Red;
            lblMessageAddSOItem.Visible = true;
            lblMessageAddSOItem.Text = "";
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Cancel")
            {
                lblMessage.Visible = true;
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/CancelSaleOrder?SaleOrderID=" + SaleOrderByID.SaleOrderID));
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SaleOrderByID.SaleOrderID + "&StatusID=" + 23));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            else if (lbActions.Text == "Edit")
            {                
                Edit();
                MPE_SaleOrderEdit.Show();
            }
            else if (lbActions.Text == "Add Item")
            {
                MPE_SaleOrderItemAdd.Show();
            }
            else if (lbActions.Text == "Release")
            {
                lblMessage.Visible = true;
                //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/ReleaseSaleOrder?SaleOrderID=" + SaleOrderByID.SaleOrderID));
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrder/UpdateSaleOrderStatus?SaleOrderID=" + SaleOrderByID.SaleOrderID + "&StatusID=" + 13));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.Text = Results.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            else if(lbActions.Text == "Generate Quotation")
            {

            }
            else if (lbActions.Text == "Generate Proforma Invoice")
            {

            }
            else if (lbActions.Text == "Delivery")
            {
                MPE_Delivery.Show();
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
                        Quantity = Item.Quantity,
                        DeliveryQuantity = Item.Quantity,
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
                gvDelivery.DataSource = SODelivery_Insert;
                gvDelivery.DataBind();
            }
        } 
        public void Edit()
        {
            cxExpectedDeliveryDate.StartDate = DateTime.Now;
            new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");

            ddlOfficeName.BorderColor = Color.Silver;
            txtCustomer.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            lblDealer.Text = SaleOrderByID.Dealer.DealerCode + " - " + SaleOrderByID.Dealer.DealerName;
            hdfCustomerId.Value = SaleOrderByID.Customer.CustomerID.ToString();
            txtCustomer.Text = SaleOrderByID.Customer.CustomerName + (string.IsNullOrEmpty(SaleOrderByID.Customer.CustomerCode) ? "" : " [" + SaleOrderByID.Customer.CustomerCode + "]");
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(SaleOrderByID.Dealer.DealerID, null, null), "OfficeName", "OfficeID", true, "Select");
           
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, SaleOrderByID.Dealer.DealerID, true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
            ddlSalesEngineer.SelectedValue = SaleOrderByID.SalesEngineer == null ? "0" : SaleOrderByID.SalesEngineer.UserID.ToString();

            ddlOfficeName.SelectedValue = SaleOrderByID.Dealer.DealerOffice.OfficeID.ToString();
            txtContactPerson.Text = SaleOrderByID.ContactPerson;
            txtContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;
            ddlDivision.SelectedValue = SaleOrderByID.Division.DivisionID.ToString();
            ddlDivision.Enabled = false;
            //lblDivisionE.Text = SaleOrderByID.Division.DivisionCode.ToString();
            txtRemarks.Text = SaleOrderByID.Remarks;
            txtExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString("dd/MM/yyyy");
            ddlInsurancePaidBy.SelectedValue = ddlInsurancePaidBy.Items.FindByText(SaleOrderByID.InsurancePaidBy).Value;
            ddlFrieghtPaidBy.SelectedValue = ddlFrieghtPaidBy.Items.FindByText(SaleOrderByID.FrieghtPaidBy).Value;
            txtAttn.Text = SaleOrderByID.Attn;
            ddlProduct.SelectedValue = SaleOrderByID.Product.ProductID.ToString();
            txtEquipmentSerialNo.Text = SaleOrderByID.EquipmentSerialNo;
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
            lblMessage.Visible = true;
            lblMessage.Text = "";
            lblMessageSOEdit.ForeColor = Color.Red;
            lblMessageSOEdit.Visible = true;
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
                lblMessage.Visible = true;
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
                SoI.DiscountPercentage = Item.DiscountPercentage;
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
                SO.ContactPerson = SaleOrderByID.ContactPerson.Trim();
                SO.ContactPersonNumber = SaleOrderByID.ContactPersonNumber.Trim();
                SO.DivisionID = Convert.ToInt32(SaleOrderByID.Division.DivisionID);
                SO.Remarks = SaleOrderByID.Remarks.Trim();
                SO.ExpectedDeliveryDate = SaleOrderByID.ExpectedDeliveryDate;
                SO.InsurancePaidBy = SaleOrderByID.InsurancePaidBy.Trim();
                SO.FrieghtPaidBy = SaleOrderByID.FrieghtPaidBy.Trim();
                SO.Attn = SaleOrderByID.Attn.Trim();
                SO.ProductID = Convert.ToInt32(SaleOrderByID.Product.ProductID);
                SO.EquipmentSerialNo = SaleOrderByID.EquipmentSerialNo.Trim();
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
            SO.CustomerID = Convert.ToInt32(hdfCustomerId.Value);
            SO.StatusID = 11;
            SO.OfficeID = Convert.ToInt32(ddlOfficeName.SelectedValue);
            SO.ContactPerson = txtContactPerson.Text.Trim();
            SO.ContactPersonNumber = txtContactPersonNumber.Text.Trim();
            SO.DivisionID = Convert.ToInt32(ddlDivision.SelectedValue);
            //SO.DivisionID = Convert.ToInt32(lblDivisionE.Text);
            SO.Remarks = txtRemarks.Text.Trim();
            SO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            SO.InsurancePaidBy = ddlInsurancePaidBy.SelectedItem.Text;
            SO.FrieghtPaidBy = ddlFrieghtPaidBy.SelectedItem.Text;
            SO.Attn = txtAttn.Text.Trim();
            SO.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            SO.EquipmentSerialNo = txtEquipmentSerialNo.Text.Trim();
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

            string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(SaleOrderByID.Customer.CustomerID)).CustomerCode;
            string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(SaleOrderByID.Dealer.DealerID), "").DealerCode;
            string Material = SoI.MaterialCode;
            string IV_SEC_SALES = "";
            string PriceDate = "";
            string IsWarrenty = "false";
            PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, "101_DPPOR_PURC_ORDER_HDR", 1, Material, SoI.Quantity, IV_SEC_SALES, PriceDate, IsWarrenty);

            Mat.CurrentPrice = Convert.ToDecimal(1000);
            Mat.Discount = Convert.ToDecimal(0);
            Mat.TaxablePrice = Convert.ToDecimal(1000);
            Mat.SGST = Convert.ToDecimal(9);
            Mat.SGSTValue = Convert.ToDecimal(90);
            Mat.CGST = Convert.ToDecimal(9);
            Mat.CGSTValue = Convert.ToDecimal(90);
            Mat.IGST = Convert.ToDecimal(0);
            Mat.IGSTValue = Convert.ToDecimal(0);

            if (Mat.CurrentPrice <= 0)
            {
                throw new Exception("Please maintain the Price for Material " + Material + " in SAP.");
            }
            if (Mat.SGST <= 0 && Mat.IGST <= 0)
            {
                throw new Exception("Please maintain the Tax for Material " + Material + " in SAP.");
            }
            if (Mat.SGSTValue <= 0 && Mat.IGSTValue <= 0)
            {
                throw new Exception("GST Tax value not found this Material " + Material + " in SAP.");
            }

            SoI.PerRate = Mat.CurrentPrice / SoI.Quantity;
            SoI.Value = Mat.CurrentPrice;
            //SoI.Discount = Mat.Discount;
            //SoI.TaxableValue = Mat.TaxablePrice;
            //SoI.Discount = SaleOrderByID.HeaderDiscount > 0 ? SaleOrderByID.HeaderDiscount : Mat.Discount;
            SoI.DiscountPercentage = SaleOrderByID.HeaderDiscountPercentage > 0 ? 0 : Mat.Discount;
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
            txtCustomer.BorderColor = Color.Silver;
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
            if (string.IsNullOrEmpty(hdfCustomerId.Value))
            {
                txtCustomer.BorderColor = Color.Red;
                return "Please enter Customer.";
            }
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
            lblMessage.Visible = true;
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
                item_Insert.DiscountPercentage = IDiscount;
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
                lblMessage.Visible = true;
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
            MPE_Delivery.Show();
            try
            {
                readSaleOrderDelivery();
                PApiResult Result = new BDMS_SalesOrder().InsertSaleOrderDelivery(SODelivery_Insert);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
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
        }
    }
}