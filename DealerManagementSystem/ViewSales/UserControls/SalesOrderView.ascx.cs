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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropdown();
                cxExpectedDeliveryDate.StartDate = DateTime.Now;
            }
        }
        void LoadDropdown()
        {
            if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
            {
                int? DealerID = null;
                ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, PSession.User.Dealer[0].DID.ToString()));
                ddlDealer.Enabled = false;
                DealerID = PSession.User.Dealer[0].DID;

                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(DealerID, null, null), "OfficeName", "OfficeID", true, "Select");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            }
            else
            {
                ddlDealer.Enabled = true;
                ddlDealer.DataTextField = "CodeWithName";
                ddlDealer.DataValueField = "DID";
                ddlDealer.DataSource = PSession.User.Dealer;
                ddlDealer.DataBind();
                ddlDealer.Items.Insert(0, new ListItem("Select", "0"));

                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID", true, "Select");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            }
        }
        public void fillViewSO(long SaleOrderID)
        {
            SaleOrderByID = new BDMS_SalesOrder().GetSaleOrderByID(SaleOrderID);

            lblSaleOrderNumber.Text = SaleOrderByID.SaleOrderNumber;
            lblDealerOffice.Text = SaleOrderByID.Dealer.DealerOffice.OfficeName;
            lblContactPerson.Text = SaleOrderByID.ContactPerson;
            lblRemarks.Text = SaleOrderByID.Remarks;
            lblRefNumber.Text = SaleOrderByID.RefNumber;
            lblFrieghtPaidBy.Text = SaleOrderByID.FrieghtPaidBy;
            lblSelectTax.Text = SaleOrderByID.SelectTax;
            lblSaleOrderDate.Text = SaleOrderByID.SaleOrderDate.ToString();
            lblCustomer.Text = SaleOrderByID.Customer.CustomerCode + " " + SaleOrderByID.Customer.CustomerName;
            lblContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;
            lblExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString();
            lblRefDate.Text = SaleOrderByID.RefDate.ToString();
            lblAttn.Text = SaleOrderByID.Attn;
            lblSODealer.Text = SaleOrderByID.Dealer.DealerCode + " " + SaleOrderByID.Dealer.DealerName;
            lblStatus.Text = SaleOrderByID.SaleOrderStatus.Status;
            lblDivision.Text = SaleOrderByID.Division.DivisionCode;
            lblProduct.Text = SaleOrderByID.Product.Product;
            lblInsurancePaidBy.Text = SaleOrderByID.InsurancePaidBy;
            lblEquipmentSerialNo.Text = SaleOrderByID.EquipmentSerialNo;

            gvSOItem.DataSource = SaleOrderByID.SaleOrderItems;
            gvSOItem.DataBind();
            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbCancelSaleOrder.Visible = true;

            int StatusID = SaleOrderByID.SaleOrderStatus.StatusID;
            if (StatusID == 2)
            {
                lbCancelSaleOrder.Visible = false;
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
            if (lbActions.Text == "Cancel SO")
            {
                Cancel();
            }
            else if (lbActions.Text == "Edit SO")
            {                
                Edit();
                MPE_SaleOrderEdit.Show();
            }
            else if (lbActions.Text == "Add SO Item")
            {
                MPE_SaleOrderItemAdd.Show();
            }
        }
        public void Cancel()
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;            
            try
            {
                PSaleOrder_Insert SO = new PSaleOrder_Insert();
                SO.SaleOrderID = Convert.ToInt64(SaleOrderByID.SaleOrderID);
                SO.DealerID = Convert.ToInt32(SaleOrderByID.Dealer.DealerID);
                SO.CustomerID = Convert.ToInt32(SaleOrderByID.Customer.CustomerID);
                SO.StatusID = 2;
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
                SO.SelectTax = SaleOrderByID.SelectTax.Trim();
                SO.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                string result = new BAPI().ApiPut("SaleOrder", SO);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                fillViewSO(SaleOrderByID.SaleOrderID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        public void Edit()
        {
            ddlDealer.BorderColor = Color.Silver;
            ddlOfficeName.BorderColor = Color.Silver;
            txtCustomer.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;

            ddlDealer.SelectedValue = SaleOrderByID.Dealer.DealerID.ToString();
            hdfCustomerId.Value = SaleOrderByID.Customer.CustomerID.ToString();
            txtCustomer.Text = SaleOrderByID.Customer.CustomerName + (string.IsNullOrEmpty(SaleOrderByID.Customer.CustomerCode) ? "" : " [" + SaleOrderByID.Customer.CustomerCode + "]");
            ddlDealer_SelectedIndexChanged(null, null);
            ddlOfficeName.SelectedValue = SaleOrderByID.Dealer.DealerOffice.OfficeID.ToString();
            txtContactPerson.Text = SaleOrderByID.ContactPerson;
            txtContactPersonNumber.Text = SaleOrderByID.ContactPersonNumber;
            ddlDivision.SelectedValue = SaleOrderByID.Division.DivisionID.ToString();
            txtRemarks.Text = SaleOrderByID.Remarks;
            txtExpectedDeliveryDate.Text = SaleOrderByID.ExpectedDeliveryDate.ToString();
            txtInsurancePaidBy.Text = SaleOrderByID.InsurancePaidBy;
            txtFrieghtPaidBy.Text = SaleOrderByID.FrieghtPaidBy;
            txtAttn.Text = SaleOrderByID.Attn;
            ddlProduct.SelectedValue = SaleOrderByID.Product.ProductID.ToString();
            txtEquipmentSerialNo.Text = SaleOrderByID.EquipmentSerialNo;
            txtSelectTax.Text = SaleOrderByID.SelectTax;
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");
            MPE_SaleOrderEdit.Show();
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
                SO_Insert.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                string result = new BAPI().ApiPut("SaleOrder", SO_Insert);
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
            lblMessage.Visible = true;
            lblMessage.Text = "";
            lblMessageAddSOItem.ForeColor = Color.Red;
            lblMessageAddSOItem.Visible = true;
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

                PSaleOrder_Insert SO = new PSaleOrder_Insert();
                SO.SaleOrderID = Convert.ToInt64(SaleOrderByID.SaleOrderID);
                SO.DealerID = Convert.ToInt32(SaleOrderByID.Dealer.DealerID);
                SO.CustomerID = Convert.ToInt32(SaleOrderByID.Customer.CustomerID);
                SO.StatusID = 1;
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
                SO.SelectTax = SaleOrderByID.SelectTax.Trim();

                SO.SaleOrderItems = new List<PSaleOrderItem_Insert>();
                SO.SaleOrderItems.Add(ReadItem());

                string result = new BAPI().ApiPut("SaleOrder", SO);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessageAddSOItem.Text = Result.Message;
                    MPE_SaleOrderItemAdd.Show();
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.Visible = true;
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
            lblMessage.Visible = true;
            lblMessage.Text = "";
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblSaleOrderItemID = (Label)gvRow.FindControl("lblSaleOrderItemID");
                PSaleOrderItem Item = (PSaleOrderItem)SaleOrderByID.SaleOrderItems.Where(M => M.SaleOrderItemID == Convert.ToInt64(lblSaleOrderItemID.Text)).ToList()[0];

                PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
                SoI.SaleOrderItemID = Item.SaleOrderItemID;
                SoI.MaterialID = Item.Material.MaterialID;
                SoI.MaterialCode = Item.Material.MaterialCode;
                SoI.Qty = Item.Qty;
                SoI.UnitPrice = Item.UnitPrice;
                SoI.Value = Item.Value;
                SoI.Discount = Item.Discount;
                SoI.TaxableAmount = Item.TaxableAmount;
                SoI.SGST = Item.Material.SGST;
                SoI.SGSTAmt = Item.Material.SGSTValue;
                SoI.CGST = Item.Material.CGST;
                SoI.CGSTAmt = Item.Material.CGSTValue;
                SoI.CGST = Item.Material.CGST;
                SoI.IGSTAmt = Item.Material.IGSTValue;

                PSaleOrder_Insert SO = new PSaleOrder_Insert();
                SO.SaleOrderID = Convert.ToInt64(SaleOrderByID.SaleOrderID);
                SO.DealerID = Convert.ToInt32(SaleOrderByID.Dealer.DealerID);
                SO.CustomerID = Convert.ToInt32(SaleOrderByID.Customer.CustomerID);
                SO.StatusID = 1;
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
                SO.SelectTax = SaleOrderByID.SelectTax.Trim();

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
                lblMessage.Visible = true;
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
            SO.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            SO.CustomerID = Convert.ToInt32(hdfCustomerId.Value);
            SO.StatusID = 1;
            SO.OfficeID = Convert.ToInt32(ddlOfficeName.SelectedValue);
            SO.ContactPerson = txtContactPerson.Text.Trim();
            SO.ContactPersonNumber = txtContactPersonNumber.Text.Trim();
            SO.DivisionID = Convert.ToInt32(ddlDivision.SelectedValue);
            SO.Remarks = txtRemarks.Text.Trim();
            SO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            SO.InsurancePaidBy = txtInsurancePaidBy.Text.Trim();
            SO.FrieghtPaidBy = txtFrieghtPaidBy.Text.Trim();
            SO.Attn = txtAttn.Text.Trim();
            SO.ProductID = Convert.ToInt32(ddlProduct.SelectedValue);
            SO.EquipmentSerialNo = txtEquipmentSerialNo.Text.Trim();
            SO.SelectTax = txtSelectTax.Text.Trim();
            return SO;
        }
        public PSaleOrderItem_Insert ReadItem()
        {
            PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
            SoI.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SoI.MaterialCode = hdfMaterialCode.Value;
            SoI.Qty = Convert.ToInt32(txtQty.Text.Trim());

            string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(SaleOrderByID.Customer.CustomerID)).CustomerCode;
            string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(SaleOrderByID.Dealer.DealerID), "").DealerCode;
            string Material = SoI.MaterialCode;
            string IV_SEC_SALES = "";
            string PriceDate = "";
            string IsWarrenty = "false";
            PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, null, 1, Material, SoI.Qty, IV_SEC_SALES, PriceDate, IsWarrenty);
            SoI.UnitPrice = Mat.CurrentPrice / SoI.Qty;
            SoI.Value = Mat.CurrentPrice;
            SoI.Discount = Mat.Discount;
            SoI.TaxableAmount = Mat.TaxablePrice;
            SoI.SGST = Mat.SGST;
            SoI.SGSTAmt = Mat.SGSTValue;
            SoI.CGST = Mat.CGST;
            SoI.CGSTAmt = Mat.CGSTValue;
            SoI.CGST = Mat.CGST;
            SoI.IGSTAmt = Mat.IGSTValue;
            return SoI;
        }
        public string Validation()
        {
            ddlDealer.BorderColor = Color.Silver;
            ddlOfficeName.BorderColor = Color.Silver;
            txtCustomer.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            string Message = "";

            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                return "Please select the Dealer";
            }
            if (ddlOfficeName.SelectedValue == "0")
            {
                ddlOfficeName.BorderColor = Color.Red;
                return "Please select the DealerOffice";
            }
            if (string.IsNullOrEmpty(hdfCustomerId.Value))
            {
                txtCustomer.BorderColor = Color.Red;
                return "Please Enter Customer";
            }
            if (ddlDivision.SelectedValue == "0")
            {
                ddlDivision.BorderColor = Color.Red;
                return "Please select the Division";
            }
            if (ddlProduct.SelectedValue == "0")
            {
                ddlProduct.BorderColor = Color.Red;
                return "Please select the Product";
            }
            if (string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
            {
                txtExpectedDeliveryDate.BorderColor = Color.Red;
                return "Please Enter the Expected Delivery Date";
            }
            return Message;
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

            foreach (PSaleOrderItem_Insert Item in SOItem_Insert)
            {
                if (Item.MaterialID == Convert.ToInt32(hdfMaterialID.Value))
                {
                    return "Material already available.";
                }
            }

            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty";
            }

            return "";
        }        
    }
}