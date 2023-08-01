using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
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
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
                //fillSaleOrderType(ddlOrderTo.SelectedValue);
                cxExpectedDeliveryDate.StartDate = DateTime.Now;
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
                //fillSaleOrderType(ddlOrderTo.SelectedValue);
                cxExpectedDeliveryDate.StartDate = DateTime.Now;
            }
        }        
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
        }
        //protected void ddlOrderTo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    fillSaleOrderType(ddlOrderTo.SelectedValue);
        //}
        //void fillSaleOrderType(string OrderTo)
        //{
        //    ddlOrderType.Items.Clear();
        //    ddlOrderType.DataTextField = "PurchaseOrderType";
        //    ddlOrderType.DataValueField = "PurchaseOrderTypeID";
        //    ddlOrderType.Items.Insert(0, new ListItem("Select", "0"));

        //    if (OrderTo == "1")
        //    {
        //        ddlOrderType.Items.Insert(1, new ListItem("Stock Order", "1"));
        //        ddlOrderType.Items.Insert(2, new ListItem("Emergency Order", "2"));
        //        ddlOrderType.Items.Insert(3, new ListItem("Break Down Order", "7"));
        //        ddlOrderType.Items.Insert(4, new ListItem("Machine Order", "5"));

        //    }
        //    else
        //    {
        //        ddlOrderType.Items.Insert(1, new ListItem("Intra-Dealer Order", "6"));
        //    }
        //}
        //protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlDivision.Items.Clear();
        //    ddlDivision.DataTextField = "DivisionDescription";
        //    ddlDivision.DataValueField = "DivisionID";
        //    ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

        //    string OrderType = ddlOrderType.SelectedValue;

        //    if ((OrderType == "1") || (OrderType == "2") || (OrderType == "7"))
        //    {
        //        ddlDivision.Items.Insert(1, new ListItem("Parts", "15"));
        //    }
        //    else if (ddlOrderType.SelectedValue == "5")
        //    {
        //        ddlDivision.Items.Insert(1, new ListItem("Batching Plant", "1"));
        //        ddlDivision.Items.Insert(2, new ListItem("Concrete Mixer", "2"));
        //        ddlDivision.Items.Insert(3, new ListItem("Concrete Pump", "3"));
        //        ddlDivision.Items.Insert(4, new ListItem("Dumper", "4"));
        //        ddlDivision.Items.Insert(5, new ListItem("Transit Mixer", "11"));
        //        ddlDivision.Items.Insert(6, new ListItem("Mobile Concrete Equipment", "14"));
        //        ddlDivision.Items.Insert(7, new ListItem("Placing Equipment", "19"));
        //    }
        //    else if (ddlOrderType.SelectedValue == "6")
        //    {
        //        ddlDivision.Items.Insert(1, new ListItem("Parts", "15"));
        //        ddlDivision.Items.Insert(2, new ListItem("Batching Plant", "1"));
        //        ddlDivision.Items.Insert(3, new ListItem("Concrete Mixer", "2"));
        //        ddlDivision.Items.Insert(4, new ListItem("Concrete Pump", "3"));
        //        ddlDivision.Items.Insert(5, new ListItem("Dumper", "4"));
        //        ddlDivision.Items.Insert(6, new ListItem("Transit Mixer", "11"));
        //        ddlDivision.Items.Insert(7, new ListItem("Mobile Concrete Equipment", "14"));
        //        ddlDivision.Items.Insert(8, new ListItem("Placing Equipment", "19"));
        //    }
        //}
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
                string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
                string Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
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
                SOItem_Insert.Add(SoI);
                fillItem();
                ClearItem();
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                //if (lbActions.Text == "Save")
                //{
                //    Save();
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
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
        public PSaleOrder_Insert Read()
        {
            PSaleOrder_Insert SO = new PSaleOrder_Insert();
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
            txtExpectedDeliveryDate.Text = "";
            txtInsurancePaidBy.Text = "";
            txtFrieghtPaidBy.Text = "";
            txtAttn.Text = "";
            ddlProduct.SelectedValue = "0";
            txtEquipmentSerialNo.Text = "";
            txtSelectTax.Text = "";
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
                    lblMessage.Text = "Please Add Material";
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
    }
}