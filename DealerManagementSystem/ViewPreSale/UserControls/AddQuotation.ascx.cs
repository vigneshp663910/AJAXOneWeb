using DataAccess;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using SapIntegration;
using System.Drawing;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class AddQuotation : System.Web.UI.UserControl
    {
        public PSalesQuotation PSO
        {
            get
            {
                if (Session["AddQuotation"] == null)
                {
                    Session["AddQuotation"] = new PSalesQuotation();
                }
                return (PSalesQuotation)Session["AddQuotation"];
            }
            set
            {
                Session["AddQuotation"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    FillGetDealerOffice();
                }
                else
                {
                }
                List<PSalesQuotationItem> PrimarySOItem = new List<PSalesQuotationItem>();
                if (PrimarySOItem.Count == 0)
                {
                    PSalesQuotationItem N = new PSalesQuotationItem();
                    PrimarySOItem.Add(N);
                }
                gvMaterial.DataSource = PrimarySOItem;
                gvMaterial.DataBind();
                new BDMS_IncoTerm().GetIncoTermDDL(ddlIncoterms, null, null);
                new BDMS_Financier().GetFinancierDDL(ddlFinancier, null, null);

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //List<PDMS_WebQuotation> PSOs = new BDMS_WebQuotation().GetWebQuotationByID(null, txtPurchaseOrder.Text.Trim());
            //if (PSOs.Count != 0)
            //{
            //    PSO = PSOs[0];
            //    fillPrimarySalesOrderEdit();
            //    pnlAllowM.Enabled = true;
            //}
            //else
            //{
            //    fillPrimarySalesOrderCreate();
            //}
        }

        //void fillPrimarySalesOrderCreate()
        //{
        //    if (string.IsNullOrEmpty(txtPurchaseOrder.Text.Trim()))
        //    {
        //        return;
        //    }
        //    string query = "select p_po_id,PH.s_created_on,PH.r_ext_id,PH.r_order_date,PH.s_tenant_id,p_bp_id, r_org_name,f_bill_to,f_ship_to,PH.f_office,f_material_id,d_material_desc,r_order_qty,r_unit_price,PI.r_discount_amt,PI.r_add_discount_amt,PI.r_tax_amt "
        //       + " from dppor_purc_order_hdr PH inner join dppor_purc_order_item  PI on PI.K_po_id = PH.p_po_id  "
        // + "   inner join doohr_bp Bp on Bp.p_bp_id = PH.f_bill_to and Bp.s_tenant_id = PH.s_tenant_id  where PH.s_tenant_id <> 23 and p_po_id ='" + txtPurchaseOrder.Text.Trim() + "'";
        //    DataTable dt = new NpgsqlServer().ExecuteReader(query);

        //    if (dt.Rows.Count == 0)
        //    {
        //        return;
        //    }


        //    lblDealer.Text = Convert.ToString(dt.Rows[0]["s_tenant_id"]);
        //    List<PDMS_Dealer> Dealer = new BDMS_Dealer().GetDealer(null, lblDealer.Text);
        //    lblDealerName.Text = Dealer[0].DealerName;
        //    PSO.Dealer = Dealer[0];

        //    PSO.SalesOrderNumber = Convert.ToString(dt.Rows[0]["r_ext_id"]);
        //    lblSoNumber.Text = PSO.SalesOrderNumber;
        //    PSO.SalesOrderDate = dt.Rows[0]["r_order_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["r_order_date"]);

        //    PSO.PrimaryPurchaseOrder = new PDMS_PrimaryPurchaseOrder();
        //    PSO.PrimaryPurchaseOrder.PurchaseOrderNumber = txtPurchaseOrder.Text.Trim();
        //    PSO.PrimaryPurchaseOrder.PurchaseOrderDate = Convert.ToDateTime(dt.Rows[0]["s_created_on"]);

        //    lblDMSPONumber.Text = PSO.PrimaryPurchaseOrder.PurchaseOrderNumber;
        //    lblDMSPODate.Text = Convert.ToString(PSO.PrimaryPurchaseOrder.PurchaseOrderDate);


        //    PSO.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dt.Rows[0]["p_bp_id"]), CustomerName = Convert.ToString(dt.Rows[0]["r_org_name"]) };
        //    lblCustomer.Text = Convert.ToString(dt.Rows[0]["p_bp_id"]);
        //    lblCustomerName.Text = Convert.ToString(dt.Rows[0]["r_org_name"]);
        //    List<PDMS_Customer> CustomerID = new BDMS_Customer().GetCustomerSQL(null, PSO.Customer.CustomerCode);
        //    if (CustomerID.Count == 0)
        //    {
        //        new BDMS_Customer().InsertOrUpdateCustomer(lblCustomer.Text, lblCustomerName.Text);
        //        CustomerID = new BDMS_Customer().GetCustomerSQL(null, lblCustomer.Text);
        //        PSO.Customer = CustomerID[0];
        //    }
        //    else
        //    {
        //        PSO.Customer = CustomerID[0];
        //    }



        //    PSO.BillTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dt.Rows[0]["f_bill_to"]) };
        //    PSO.ShipTo = new PDMS_Customer() { CustomerCode = Convert.ToString(dt.Rows[0]["f_ship_to"]) };
        //    PSO.Office = Convert.ToString(dt.Rows[0]["f_office"]);

        //    if (string.IsNullOrEmpty(PSO.BillTo.CustomerCode))
        //    {
        //        PSO.BillTo.CustomerCode = PSO.Customer.CustomerCode;
        //    }

        //    PSO.BillTo = new SCustomer().getCustomerAddress(PSO.BillTo.CustomerCode);

        //    if (string.IsNullOrEmpty(PSO.ShipTo.CustomerCode))
        //    {
        //        PSO.ShipTo = PSO.BillTo;
        //    }
        //    PSO.ShipTo = new SCustomer().getCustomerAddress(PSO.ShipTo.CustomerCode);

        //    lblState.Text = PSO.BillTo.StateN.State;
        //    // lblDistrict.Text = Customer.dis;
        //    lblCity.Text = PSO.BillTo.City;
        //    lblAddress1.Text = PSO.BillTo.Address1;
        //    lblAddress2.Text = PSO.BillTo.Address2;
        //    lblPostalCode.Text = PSO.BillTo.Pincode;

        //    txtShipTo.Text = PSO.ShipTo.CustomerCode;
        //    if (!string.IsNullOrEmpty(PSO.ShipTo.StateN.State))
        //    {
        //        List<PDMS_State> State = new BDMS_Address().GetState(null, PSO.ShipTo.StateN.State);
        //        if (State.Count == 1)
        //        {
        //            ddlShipToState.SelectedValue = Convert.ToString(State[0].StateID);
        //            new BDMS_Address().GetDistrict(ddlShipToDistrict, null, Convert.ToInt32(ddlShipToState.SelectedValue), null);
        //        }

        //        if (PSO.ShipTo.District != null)
        //        {
        //            List<PDMS_District> District = new BDMS_Address().GetDistrict(null, Convert.ToInt32(ddlShipToState.SelectedValue), PSO.ShipTo.District.District);
        //            if (District.Count == 1)
        //            {
        //                ddlShipToDistrict.SelectedValue = Convert.ToString(District[0].DistrictID);
        //            }
        //        }
        //    }
        //    txtShipToCity.Text = PSO.ShipTo.City;
        //    txtShipToAddress1.Text = PSO.ShipTo.Address1;
        //    txtShipToAddress2.Text = PSO.ShipTo.Address2;
        //    txtShipToPostalCode.Text = PSO.ShipTo.Pincode;



        //    decimal r_order_qty, r_unit_price, r_discount_amt;

        //    r_order_qty = dt.Rows[0]["r_order_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(dt.Rows[0]["r_order_qty"]);
        //    r_unit_price = dt.Rows[0]["r_unit_price"] == DBNull.Value ? 0 : Convert.ToDecimal(dt.Rows[0]["r_unit_price"]);
        //    r_discount_amt = dt.Rows[0]["r_discount_amt"] == DBNull.Value ? 0 : Convert.ToDecimal(dt.Rows[0]["r_discount_amt"]);

        //    txtInvoiceValue.Text = ((r_order_qty * r_unit_price) + r_discount_amt).ToString("#.##");

        //    PSO.WebQuotationItems = new List<PDMS_WebQuotationItem>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        PSO.WebQuotationItems.Add(new PDMS_WebQuotationItem()
        //        {
        //            WebQuotationItemID = 0,
        //            WebQuotationID = 0,
        //            Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["f_material_id"]) },
        //            Qty = Convert.ToInt32(dr["r_order_qty"]),
        //            BasicPrice = Convert.ToDecimal(dr["r_unit_price"]),
        //            Discount1 = Convert.ToDecimal(dr["r_discount_amt"]),
        //            Discount2 = 0,
        //            Discount3 = 0,
        //        });
        //    }
        //    gvMaterial.DataSource = PSO.WebQuotationItems;
        //    gvMaterial.DataBind();
        //}
        void fillPrimarySalesOrderEdit()
        {
            ddlFinancier.Text = PSO.Financier == null ? "0" : Convert.ToString(PSO.Financier.QuotationFinancierID);
            txtDoNumber.Text = PSO.Financier.DoNumber;
            txtDoDate.Text = Convert.ToString(PSO.Financier.DoDate);
            ddlIncoterms.SelectedValue = PSO.Financier.IncoTerm == null ? "0" : Convert.ToString(PSO.Financier.IncoTerm.IncoTermID);

            txtAdvanceAmount.Text = Convert.ToString(PSO.Financier.AdvanceAmount);
            txtFinancierAmount.Text = Convert.ToString(PSO.Financier.FinancierAmount);
            gvMaterial.DataSource = PSO.QuotationItems;
            gvMaterial.DataBind();

        }

        //private void FillMainApplication()
        //{
        //    ddlUsage.DataTextField = "MainApplication";
        //    ddlUsage.DataValueField = "MainApplicationID";
        //    ddlUsage.DataSource = new BDMS_Service().GetMainApplication(null, null);
        //    ddlUsage.DataBind();
        //    ddlUsage.Items.Insert(0, new ListItem("Select", "0"));
        //}

        protected void btnSaveBasicInformation_Click(object sender, EventArgs e)
        {
            //if (!ValidationBasicInformation())
            //{
            //    return;
            //} 

            //PSO.Customer = new PDMS_Customer() { CustomerCode = txtCustomer.Text.Trim() };
            //PSO.BillTo = new PDMS_Customer() { CustomerCode = txtBillTo.Text.Trim() }; 


            //PSO.Customer = new PDMS_Customer() { CustomerID = Customer.CustomerID };
            //PSO.BillTo = new PDMS_Customer() { CustomerID = Customer.CustomerID };


            //PSO.ShipTo = new PDMS_Customer() { CustomerCode = txtShipTo.Text.Trim() };



            PSO.QuotationID = new BSalesQuotation().InsertOrUpdateSalesQuotationBasicInformation(PSO);
            if (PSO.QuotationID != 0)
            {
                //lblBasicInformationMessage.Text = "Sales Order is updated successfully";
                //lblBasicInformationMessage.ForeColor = Color.Green;
                pnlAllowM.Enabled = true;
            }
            else
            {
                //lblBasicInformationMessage.Text = "Sales Order is not updated successfully";
                //lblBasicInformationMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSaveFinanceInformation_Click(object sender, EventArgs e)
        {
            //if (!ValidationFinanceInformation1())
            //{
            //    return;
            //}

            //PSO.ShipTo.CustomerCode = txtShipTo.Text.Trim();


            //Financier 
            PSO.Financier = ddlFinancier.SelectedValue == "0" ? null : new PSalesQuotationFinancier() { QuotationFinancierID = Convert.ToInt32(ddlFinancier.SelectedValue) };

            PSO.Financier.DoNumber = txtDoNumber.Text.Trim();
            PSO.Financier.DoDate = string.IsNullOrEmpty(txtDoDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDoDate.Text.Trim());

            PSO.Financier.IncoTerm = ddlIncoterms.SelectedValue == "0" ? null : new PDMS_IncoTerm() { IncoTermID = Convert.ToInt32(ddlIncoterms.SelectedValue) };
            PSO.Financier.AdvanceAmount = string.IsNullOrEmpty(txtAdvanceAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
            PSO.Financier.FinancierAmount = string.IsNullOrEmpty(txtFinancierAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtFinancierAmount.Text.Trim());

            if (new BSalesQuotation().InsertOrUpdateSalesQuotationFinanceInformation(PSO))
            {
                //lblFinanceInformationMessage.Text = "Sales Order is updated successfully";
                //lblFinanceInformationMessage.ForeColor = Color.Green;
            }
            else
            {
                //lblFinanceInformationMessage.Text = "Sales Order is not updated successfully";
                //lblFinanceInformationMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSaveSalesInformation_Click(object sender, EventArgs e)
        {

            //PSO.DiscountSales = string.IsNullOrEmpty(txtDiscountSales.Text.Trim()) ? 0 : Convert.ToDecimal(txtDiscountSales.Text.Trim());
            //PSO.FreightValue = string.IsNullOrEmpty(txtFreightValue.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtFreightValue.Text.Trim());
            //PSO.InsuranceValue = string.IsNullOrEmpty(txtInsuranceValue.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtInsuranceValue.Text.Trim());
            //PSO.TRDate = string.IsNullOrEmpty(txtTRDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTRDate.Text.Trim());
            //PSO.ConsolidationInvoicePrint = Convert.ToBoolean(cbConsolidationInvoicePrint.Checked);
            //PSO.FreightAmount = string.IsNullOrEmpty(txtFreightAmount.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtFreightAmount.Text.Trim());
            //PSO.Billing = ddlBilling.SelectedValue;


            if (new BSalesQuotation().InsertOrUpdateSalesQuotationSalesInformation(PSO))
            {
                //lblSalesInformationMessage.Text = "Sales Order is updated successfully";
                //lblSalesInformationMessage.ForeColor = Color.Green;
            }
            else
            {
                //lblSalesInformationMessage.Text = "Sales Order is not updated successfully";
                //lblSalesInformationMessage.ForeColor = Color.Red;
            }
        }
        protected void lblMaterialAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            LinkButton lbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lbtn.NamingContainer;
            string Material = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterial")).Text.Trim();
            Label lblMaterialDescription = (Label)row.FindControl("lblMaterialDescription");
            Label lblUnit = (Label)row.FindControl("lblUnit");
            TextBox txtQty = (TextBox)gvMaterial.FooterRow.FindControl("txtQty");
            Label lblBasicPrice = (Label)row.FindControl("lblBasicPrice");
            TextBox txtDiscount = (TextBox)gvMaterial.FooterRow.FindControl("txtDiscount");
            Label lblTaxableValue = (Label)row.FindControl("lblTaxableValue");
            Label lblTaxPersent = (Label)row.FindControl("lblTaxPersent");
            Label lblTaxvalue = (Label)row.FindControl("lblTaxvalue");
            Label lblNetValue = (Label)row.FindControl("lblNetValue");

            if (string.IsNullOrEmpty(Material))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Qty";
                return;
            }

            int valueqty;
            if (!int.TryParse(txtQty.Text, out valueqty))
            {
                lblMessage.Text = "Please enter correct format in Qty";
                return;
            }
            int valuediscount=0;
            if (!string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                if (!int.TryParse("0" + txtDiscount.Text.Trim(), out valuediscount))
                {
                    lblMessage.Text = "Please enter correct format in Discount";
                    return;
                }
            }
            int valueBasicPrice = 0;
            if (!string.IsNullOrEmpty(lblBasicPrice.Text))
            {
                int.TryParse("0" + lblBasicPrice.Text.Trim(), out valueBasicPrice);
            }
            int valueTaxableValue=0;
            lblTaxableValue.Text = ((valueqty* valueBasicPrice) - valuediscount).ToString();
            if (!string.IsNullOrEmpty(lblTaxableValue.Text.Trim()))
            {
                int.TryParse("0" + lblTaxableValue.Text.Trim(), out valueTaxableValue);
            }            
            int valueTaxPersent=0;
            if (!string.IsNullOrEmpty(lblTaxPersent.Text.Trim()))
            {
                int.TryParse("0" + lblTaxPersent.Text.Trim(), out valueTaxPersent);
            }
            int valueTaxValue=0;
            lblTaxvalue.Text = (valueTaxableValue * (valueTaxPersent / 100)).ToString();
            if (!string.IsNullOrEmpty(lblTaxvalue.Text.Trim()))
            {
                int.TryParse("0" + lblTaxvalue.Text.Trim(), out valueTaxValue);
            }
            int valueNetValue=0;
            lblNetValue.Text = valueTaxValue.ToString();
            if (!string.IsNullOrEmpty(lblTaxvalue.Text.Trim()))
            {
                int.TryParse("0" + lblTaxvalue.Text.Trim(), out valueTaxValue);
            }


            List<PDMS_Material> MM = new BDMS_Material().GetMaterialListSQL(null, Material);
            if (MM.Count != 1)
            {
                lblMessage.Text = "Please check the Material";
                return;
            }
            PSalesQuotationItem Item = new PSalesQuotationItem();
            Item.Material = new PDMS_Material();
            Item.Material.MaterialCode = Material;
            Item.Qty = Convert.ToInt32(txtQty.Text);
            Item.BasicPrice = MM[0].CurrentPrice;
            Item.Discount = Convert.ToDecimal("0" + txtDiscount.Text);
            //Item.WebQuotationID = PSO.QuotationID;
            //Item.WebQuotationItemID = 0;
            //Item.Material = new PDMS_Material();
            //Item.Material.MaterialCode = Material;
            //Item.Qty = Convert.ToInt32(txtQty.Text);
            //Item.BasicPrice = MM[0].CurrentPrice;
            //Item.Discount1 = Convert.ToDecimal("0" + txtDiscount1.Text);
            //Item.Discount2 = Convert.ToDecimal("0" + txtDiscount2.Text);
            //Item.Discount3 = Convert.ToDecimal("0" + txtDiscount3.Text);

            gvMaterial.DataSource = Item;
            gvMaterial.DataBind();
            return;



            lblMessage.Text = "";
            if (new BSalesQuotation().InsertOrUpdateSalesQuotationItem(Item))
            {
                lblMessage.ForeColor = Color.Green;

                lblMessage.Text = "Material " + Material + " is added";
                FillMaterial();
            }
            else
            {
                lblMessage.Text = "Material " + Material + " is not added";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            PDMS_WebQuotationItem Item = new PDMS_WebQuotationItem();
            Item.WebQuotationItemID = Convert.ToInt64(gvMaterial.DataKeys[gvRow.RowIndex].Value);

            if (new BDMS_WebQuotation().InsertOrUpdateWebQuotationItem(Item))
            {
                lblMessage.Text = "Material is Removed successfully";
                lblMessage.ForeColor = Color.Green;
                FillMaterial();
            }
            else
            {
                lblMessage.Text = "Material is not Removed successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void FillMaterial()
        {
            List<PDMS_WebQuotationItem> Item = new BDMS_WebQuotation().GetWebQuotationItem(PSO.QuotationID, null);
            //if (Item.Count==0)
            //{
            //    Item.Add(new PDMS_PrimarySalesOrderItem());
            //}
            gvMaterial.DataSource = Item;
            gvMaterial.DataBind();
        }

        private void FillGetDealerOffice()
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            //  ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
        }
    }
}