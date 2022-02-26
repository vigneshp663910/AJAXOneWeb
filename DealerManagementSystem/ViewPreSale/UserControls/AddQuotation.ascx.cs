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
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
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
        private List<PSalesQuotationItem> PSOItem
        {
            get
            {
                if (ViewState["PLSO"] == null)
                {
                    ViewState["PLSO"] = new List<PSalesQuotationItem>();
                }
                return (List<PSalesQuotationItem>)ViewState["PLSO"];
            }
            set
            {
                ViewState["PLSO"] = value;
            }
        }
        private List<PDMS_ServiceMaterial> PSM
        {
            get
            {
                if (ViewState["PSM"] == null)
                {
                    ViewState["PSM"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)ViewState["PSM"];
            }
            set
            {
                ViewState["PSM"] = value;
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
            //  btnGenerateQuotation.Focus();
            string Material = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterial")).Text.Trim();
            if (string.IsNullOrEmpty(Material))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }

            TextBox txtQty = (TextBox)gvMaterial.FooterRow.FindControl("txtQty");
            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Qty";
                return;
            }
            TextBox txtDiscount = (TextBox)gvMaterial.FooterRow.FindControl("txtDiscount");
            if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Qty";
                return;
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                lblMessage.Text = "Please enter correct format in Qty";
                return;
            }
            //     string 
            Material = Material.Split(' ')[0];
            string MaterialOrg = Material;

            //CheckBox cbSupersedeYN = (CheckBox)gvMaterial.FooterRow.FindControl("cbSupersedeYN");
            //if (cbSupersedeYN.Checked)
            //{
            //    string smaterial = Material;
            //    do
            //    {
            //        Material = smaterial;
            //        string query = "select s.p_smaterial from af_m_supersede s left join af_m_materials mm on mm.p_material = s.p_rmaterial left join af_m_materials ms on ms.p_material = s.p_smaterial   where s.valid_from <= Now() and s.valid_to >= Now() and  p_rmaterial = '" + smaterial + "'";
            //        smaterial = new NpgsqlServer().ExecuteScalar(query);
            //    } while (!string.IsNullOrEmpty(smaterial));
            //}

            //for (int i = 0; i < gvMaterial.Rows.Count; i++)
            //{
            //    Label lblMaterialCode = (Label)gvMaterial.Rows[i].FindControl("lblMaterialCode");
            //    Label lblCancel = (Label)gvMaterial.Rows[i].FindControl("lblCancel");
            //    if ((lblMaterialCode.Text == Material) && (lblCancel.Visible == false))
            //    {
            //        if (MaterialOrg != Material)
            //        {
            //            lblMessage.Text = "Part Number " + MaterialOrg + "is superseded with " + Material + "  and already available";
            //        }
            //        else
            //        {
            //            lblMessage.Text = "Material " + Material + " already available";
            //        }
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //}

            string OrderType = "";
            string Customer = "";
            string Vendor = "";
            string IV_SEC_SALES = "X";
            string PRICEDATE = "";
            Boolean IsWarrenty = false;
            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            //{
            //    OrderType = "DEFAULT_SEC_AUART";
            //    Customer = SDMS_ICTicket.Customer.CustomerCode;
            //    Vendor = SDMS_ICTicket.Dealer.DealerCode;
            //}
            //else
            //{
            //    OrderType = "301_DSSOR_SALES_ORDER_HDR";
            //    Customer = SDMS_ICTicket.Dealer.DealerCode;
            //    Vendor = "9031";
            //    IsWarrenty = true;
            //}

            decimal Available = 0;
            Boolean IsIGST = true;
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material)[0];
            if (string.IsNullOrEmpty(MM.MaterialCode))
            {
                lblMessage.Text = "Please check the Material";
                return;
            }
            PSM = new List<PDMS_ServiceMaterial>();
            decimal Qty = Convert.ToDecimal(txtQty.Text);
            PDMS_ServiceMaterial MaterialTax = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, Material, Qty, IV_SEC_SALES, PRICEDATE, IsWarrenty);


            PDMS_Material MM_SQL = new BDMS_Material().GetMaterialListSQL(null, Material)[0];
            Available = Convert.ToDecimal(txtQty.Text);

            PDMS_Customer Dealer = new SCustomer().getCustomerAddress("9001");
            PDMS_Customer CustomerP = new SCustomer().getCustomerAddress("343559");

            MaterialTax.BasePrice = MM_SQL.CurrentPrice;
            if (MaterialTax.BasePrice <= 0)
            {
                lblMessage.Text = "Please maintain the price for Material " + Material + " in SAP";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            
            MaterialTax.TaxablePrice = (MaterialTax.BasePrice * Convert.ToDecimal(txtQty.Text))- Convert.ToDecimal(txtDiscount.Text);

            PSOItem = new List<PSalesQuotationItem>();
            PSalesQuotationItem Item = new PSalesQuotationItem();
            Item.Material = new PDMS_Material();
            Item.Material.MaterialCode = MM.MaterialCode;
            Item.Material.MaterialDescription = MM.MaterialDescription;

            Item.Plant = new PPlant() { PlantCode = "" };
            Item.Unit = 1;
            Item.Qty = Convert.ToInt32(txtQty.Text);
            Item.BasicPrice = MaterialTax.BasePrice;
            Item.Discount = Convert.ToDecimal(txtDiscount.Text);
            Item.TaxableValue = MaterialTax.TaxablePrice;

            if (Dealer.State.StateCode == CustomerP.State.StateCode)
            {
                IsIGST = false;
                MaterialTax.IGST = MM_SQL.TaxPercentage;
                MaterialTax.IGSTValue = MM_SQL.TaxPercentage * MaterialTax.TaxablePrice * 2 / 100;
                Item.TaxPersent = MaterialTax.IGST;
                Item.TaxValue = MaterialTax.IGSTValue;
            }
            else
            {
                MaterialTax.SGST = MM_SQL.TaxPercentage;
                MaterialTax.SGSTValue = MM_SQL.TaxPercentage * MaterialTax.TaxablePrice / 100;
                Item.TaxPersent = MaterialTax.SGST;
                Item.TaxValue = MaterialTax.SGSTValue;
            }
            Item.NetValue = Item.TaxValue;
            PSOItem.Add(Item);
            gvMaterial.DataSource = PSOItem;
            gvMaterial.DataBind();
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