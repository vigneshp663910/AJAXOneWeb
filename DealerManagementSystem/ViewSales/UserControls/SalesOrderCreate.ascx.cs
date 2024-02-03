using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SalesOrderCreate : System.Web.UI.UserControl
    {
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
            lblMessage.Text = string.Empty;
        }
        public void FillMaster()
        {
            if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
            {
                int? DealerID = null;
                ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, PSession.User.Dealer[0].DID.ToString()));
                ddlDealer.Enabled = false;
                DealerID = PSession.User.Dealer[0].DID;

                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(DealerID, null, null), "OfficeName", "OfficeID", true, "Select");
                //new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                //new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
                //cxExpectedDeliveryDate.StartDate = DateTime.Now;
                //ddlDivision.SelectedValue = "15"; ddlDivision.Enabled = false;
                new DDLBind(ddlSalesEngineer, DealerID, "ContactName", "UserID");
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
                //new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                //new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
                //cxExpectedDeliveryDate.StartDate = DateTime.Now;
                //ddlDivision.SelectedValue = "15"; ddlDivision.Enabled = false;
            }
            new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            cxExpectedDeliveryDate.StartDate = DateTime.Now;
            txtExpectedDeliveryDate.Text = DateTime.Now.ToShortDateString();
            ddlDivision.SelectedValue = "15"; ddlDivision.Enabled = false;
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
            txtBoxHeaderDiscountPercent.Text = "0";
        }        
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");

            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
        }
        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            lblMessage.Text = "";
            try
            {
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
                PSaleOrderItem_Insert SoI = ReadItem();
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(SoI.MaterialID), null, null, null, null)[0];
                //if (string.IsNullOrEmpty(m.HSN))
                //{
                //    lblMessage.Text = "HSN Code is not updated for this Material. Please contact Parts Admin.";
                //    return;
                //}
                string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
                string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                string Material = SoI.MaterialCode;
                string IV_SEC_SALES = "";
                string PriceDate = "";
                string IsWarrenty = "false";

                PMaterial Mat = new BDMS_Material().MaterialPriceFromSap(Customer, Vendor, "101_DSSOR_SALES_ORDER_HDR", 1, Material, SoI.Qty, IV_SEC_SALES, PriceDate, IsWarrenty);
                //if (Mat.CurrentPrice <= 0)
                //{
                //    lblMessage.Text = "Please maintain the price for Material " + Material + " in SAP.";
                //    return;
                //}
                //if (Mat.SGST <= 0 && Mat.IGST <= 0)
                //{
                //    lblMessage.Text = "Please maintain the Tax for Material " + Material + " in SAP.";
                //    return;
                //}
                //if (Mat.SGSTValue <= 0 && Mat.IGSTValue <= 0)
                //{
                //    lblMessage.Text = "GST Tax value not found this Material " + Material + " in SAP.";
                //    return;
                //}

                //Mat.CurrentPrice = Convert.ToDecimal(1000);
                //Mat.Discount = Convert.ToDecimal(0);
                //Mat.TaxablePrice = Convert.ToDecimal(1000);
                //Mat.SGST = Convert.ToDecimal(9);
                //Mat.SGSTValue = Convert.ToDecimal(90);
                //Mat.CGST = Convert.ToDecimal(9);
                //Mat.CGSTValue = Convert.ToDecimal(90);
                //Mat.IGST = Convert.ToDecimal(0);
                //Mat.IGSTValue = Convert.ToDecimal(0);

                SoI.UnitPrice = Mat.CurrentPrice / SoI.Qty;
                SoI.Value = Mat.CurrentPrice;
                //SoI.Discount = Mat.Discount;
                //SoI.TaxableAmount = Mat.TaxablePrice;

                decimal HDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());

                if (HDiscount >= 100)
                {
                    lblMessage.Text = "Discount Percentage cannot be more 100.";
                    lblMessage.Visible = true;
                    return;
                }

                //SoI.Discount = Mat.CurrentPrice - (Mat.CurrentPrice * HDiscount / 100);
                //SoI.TaxableAmount = SoI.Discount;
                SoI.Discount = Mat.Discount;
                SoI.TaxableAmount = Mat.CurrentPrice - (Mat.CurrentPrice * (HDiscount / 100));
                //SoI.SGST = Mat.SGST;
                //SoI.SGSTAmt = Mat.SGSTValue;
                //SoI.CGST = Mat.CGST;
                //SoI.CGSTAmt = Mat.CGSTValue;
                //SoI.IGST = Mat.IGST;
                //SoI.IGSTAmt = Mat.IGSTValue;
                SoI.StatusID = 11;
                SoI.MaterialDescription = m.MaterialDescription;
                SoI.HSN = m.HSN;
                SoI.UOM = m.BaseUnit;
                
                if (ddlTax.SelectedValue == "1")
                {
                    SoI.SGST =  (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                    SoI.SGSTAmt = (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                    SoI.CGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                    SoI.CGSTAmt = (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                    SoI.IGST = 0;
                    SoI.IGSTAmt = 0;
                }
                else
                {
                    SoI.SGST = 0;
                    SoI.SGSTAmt = 0;
                    SoI.CGST = 0;
                    SoI.CGSTAmt = 0;
                    SoI.IGST = Mat.SGST + Mat.CGST + Mat.IGST;
                    SoI.IGSTAmt = Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue;
                }


                SoI.SGSTAmt = SoI.TaxableAmount * (SoI.SGST / 100);
                SoI.CGSTAmt = SoI.TaxableAmount * (SoI.CGST / 100);
                SoI.IGSTAmt = SoI.TaxableAmount * (SoI.IGST / 100);

                SoI.NetAmt = SoI.TaxableAmount + SoI.SGSTAmt + SoI.CGSTAmt+ SoI.IGSTAmt;
                SOItem_Insert.Add(SoI);
                fillItem();
                ClearItem();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        public string Validation()
        {
            ddlDealer.BorderColor = Color.Silver;
            ddlOfficeName.BorderColor = Color.Silver;
            txtCustomer.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            ddlTax.BorderColor = Color.Silver;
            txtBoxHeaderDiscountPercent.BorderColor = Color.Silver;
            string Message = "";

            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                return "Please select the Dealer.";
            }
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
            if (ddlDivision.SelectedValue == "0")
            {
                ddlDivision.BorderColor = Color.Red;
                return "Please select the Division.";
            }
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
            if (ddlTax.SelectedValue == "0")
            {
                ddlTax.BorderColor = Color.Red;
                return "Please select the Tax.";
            }
            decimal value;
            if (!decimal.TryParse(txtBoxHeaderDiscountPercent.Text, out value))
            {
                txtBoxHeaderDiscountPercent.BackColor = Color.Red;
                return "Please enter correct format in Header Discount Percent.";
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
                    return "Duplicate Material.";
                }
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty";
            }
            return "";
        }
        public PSaleOrder_Insert Read()
        {
            PSaleOrder_Insert SO = new PSaleOrder_Insert();
            SO.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            SO.CustomerID = Convert.ToInt32(hdfCustomerId.Value);
            SO.StatusID = 11;
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
            //SO.SelectTax = txtSelectTax.Text.Trim();
            SO.SelectTax = ddlTax.SelectedItem.Text;
            SO.SaleOrderTypeID = 1;
            SO.SalesEngineerID = ddlSalesEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesEngineer.SelectedValue);
            SO.HeaderDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
            
            return SO;
        }
        public PSaleOrderItem_Insert ReadItem()
        {
            PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
            SoI.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SoI.MaterialCode = hdfMaterialCode.Value;
            SoI.Qty = Convert.ToInt32(txtQty.Text.Trim());
            return SoI;
        }
        void fillItem()
        {
            gvSOItem.DataSource = SOItem_Insert;
            gvSOItem.DataBind();
        }
        protected void lnkBtnSoItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "";
                LinkButton lnkBtnCountryDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnCountryDelete.NamingContainer);
                string Material = ((Label)row.FindControl("lblMaterial")).Text.Trim();

                int i = 0;
                foreach (PSaleOrderItem_Insert Item in SOItem_Insert)
                {
                    if (Item.MaterialCode == Material)
                    {
                        SOItem_Insert.RemoveAt(i);
                        lblMessage.Text = "Material Removed successfully";
                        lblMessage.ForeColor = Color.Green;
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
        }
        void ClearHeader()
        {
            ddlDealer.SelectedValue = "0";
            txtCustomer.Text = "";
            hdfCustomerId.Value = "";
            ddlOfficeName.SelectedValue = "0";
            txtContactPerson.Text = "";
            txtContactPersonNumber.Text = "";
            ddlDivision.SelectedValue = "0";
            txtRemarks.Text = "";
            //txtExpectedDeliveryDate.Text = "";
            txtInsurancePaidBy.Text = "";
            txtFrieghtPaidBy.Text = "";
            txtAttn.Text = "";
            ddlProduct.SelectedValue = "0";
            txtEquipmentSerialNo.Text = "";
            //txtSelectTax.Text = "";
            ddlTax.SelectedValue = "0";
            ddlSalesEngineer.SelectedValue = "0";
            gvSOItem.DataSource = null;
            gvSOItem.DataBind();
        }
        void ClearItem()
        {
            hdfMaterialID.Value = "";
            hdfMaterialCode.Value = "";
            txtMaterial.Text = "";
            txtQty.Text = "";
        }
        protected void btnSaveSOItem_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            try
            {
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                if (SOItem_Insert.Count == 0)
                {
                    lblMessage.Text = "Please add Material.";
                    return;
                }
                SO_Insert = Read();
                SO_Insert.SaleOrderItems = SOItem_Insert;
                string result = new BAPI().ApiPut("SaleOrder", SO_Insert);
                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = Result.Message;
                    return;
                }
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Green;
                ClearHeader();
                ClearItem();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void txtBoxHeaderDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            
            foreach (GridViewRow row in gvSOItem.Rows)
            {
                Label MaterialID = (Label)row.FindControl("lblMaterialID");
                Label lblTaxableAmount = (Label)row.FindControl("lblTaxableAmount");
                Label lblTaxAmount = (Label)row.FindControl("lblTaxAmount");
                Label lblNetAmount = (Label)row.FindControl("lblNetAmount");

                decimal HDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
                decimal IDiscount = Convert.ToDecimal(((TextBox)row.FindControl("txtBoxDiscountPercent")).Text.Trim());

                if ((HDiscount + IDiscount < 0) || (HDiscount + IDiscount >= 100))
                {
                    lblMessage.Text = "Discount Percentage cannot be less than 0 and more 100.";
                    lblMessage.Visible = true;
                    return;
                }

                foreach (PSaleOrderItem_Insert SOI in SOItem_Insert)
                {
                    //if (SOI.MaterialID == Convert.ToInt64(MaterialID.Text))
                    //{
                        decimal discountValue = SOI.Value * ((HDiscount + IDiscount) / 100);
                        decimal discountedPrice = SOI.Value - discountValue;
                        SOI.TaxableAmount = discountedPrice;

                        SOI.SGSTAmt = SOI.TaxableAmount * (SOI.SGST / 100);
                        SOI.CGSTAmt = SOI.TaxableAmount * (SOI.CGST / 100);
                        SOI.IGSTAmt = SOI.TaxableAmount * (SOI.IGST / 100);

                        SOI.NetAmt = SOI.TaxableAmount + SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt;
                        SOI.Discount = IDiscount;

                        lblTaxableAmount.Text = SOI.TaxableAmount.ToString();
                        lblTaxAmount.Text = Convert.ToString(SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt);
                        lblNetAmount.Text = SOI.NetAmt.ToString();
                    //}
                }
            }
        }
        protected void txtBoxDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txtBoxDiscountPercent = (TextBox)gvRow.FindControl("txtBoxDiscountPercent");
            Label MaterialID = (Label)gvRow.FindControl("lblMaterialID");

            //Label lblDiscountAmount = (Label)gvRow.FindControl("lblDiscountAmount");
            Label lblTaxableAmount = (Label)gvRow.FindControl("lblTaxableAmount");
            Label lblTaxAmount = (Label)gvRow.FindControl("lblTaxAmount");
            Label lblNetAmount = (Label)gvRow.FindControl("lblNetAmount");

            decimal HDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
            decimal IDiscount = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtBoxDiscountPercent")).Text.Trim());

            if ((HDiscount + IDiscount < 0) || (HDiscount + IDiscount >= 100))
            {
                lblMessage.Text = "Discount Percentage cannot be less than 0 and more 100.";
                lblMessage.Visible = true;
                return;
            }

            foreach(PSaleOrderItem_Insert SOI in SOItem_Insert)
            {
                if(SOI.MaterialID == Convert.ToInt64(MaterialID.Text))
                {
                    decimal discountValue = SOI.Value * (HDiscount + IDiscount) / 100;
                    decimal discountedPrice = SOI.Value - discountValue;
                    SOI.TaxableAmount = discountedPrice;

                    SOI.SGSTAmt = SOI.TaxableAmount * (SOI.SGST / 100);
                    SOI.CGSTAmt = SOI.TaxableAmount * (SOI.CGST / 100);
                    SOI.IGSTAmt = SOI.TaxableAmount * (SOI.IGST / 100);
                    
                    SOI.NetAmt = SOI.TaxableAmount + SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt;
                    SOI.Discount = IDiscount;

                    lblTaxableAmount.Text = SOI.TaxableAmount.ToString();
                    lblTaxAmount.Text = Convert.ToString(SOI.SGSTAmt + SOI.CGSTAmt + SOI.IGSTAmt);
                    lblNetAmount.Text = SOI.NetAmt.ToString();
                }
            }
        }
    }
}