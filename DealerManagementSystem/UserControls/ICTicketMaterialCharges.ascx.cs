using Business;
using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketMaterialCharges : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
        //public List<PDMS_ICTicketTSIR> TSIR
        //{
        //    get
        //    {
        //        if (Session["PDMS_TSIR"] == null)
        //        {
        //            Session["PDMS_TSIR"] = new List<PDMS_ICTicketTSIR>();
        //        }
        //        return (List<PDMS_ICTicketTSIR>)Session["PDMS_TSIR"];
        //    }
        //    set
        //    {
        //        Session["PDMS_TSIR"] = value;
        //    }
        //}
        public List<PDMS_ICTicketTSIR> ICTicketTSIRs
        {
            get
            {
                if (Session["PDMS_ICTicketTSIRs"] == null)
                {
                    Session["PDMS_ICTicketTSIRs"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["PDMS_ICTicketTSIRs"];
            }
            set
            {
                Session["PDMS_ICTicketTSIRs"] = value;
            }
        }
        public List<PDMS_MaterialSource> MSource
        {
            get
            {
                if (Session["PDMS_MaterialSource"] == null)
                {
                    Session["PDMS_MaterialSource"] = new List<PDMS_MaterialSource>();
                }
                return (List<PDMS_MaterialSource>)Session["PDMS_MaterialSource"];
            }
            set
            {
                Session["PDMS_MaterialSource"] = value;
            }
        }

        public List<PDMS_ServiceMaterial> SS_ServiceMaterialAll
        {
            get
            {
                if (Session["ServiceMaterialAllICTicketProcess"] == null)
                {
                    Session["ServiceMaterialAllICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)Session["ServiceMaterialAllICTicketProcess"];
            }
            set
            {
                Session["ServiceMaterialAllICTicketProcess"] = value;
            }
        }
        public List<PDMS_ServiceMaterial> SS_ServiceMaterial
        {
            get
            {
                if (Session["ServiceMaterialICTicketProcess"] == null)
                {
                    Session["ServiceMaterialICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)Session["ServiceMaterialICTicketProcess"];
            }
            set
            {
                Session["ServiceMaterialICTicketProcess"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            // TSIR = new List<PDMS_ICTicketTSIR>();
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                MSource = new BDMS_Service().GetMaterialSource(null, null);
                btnGenerateQuotation.Visible = false;
                btnRequestForClaim.Visible = false;
                btnGenerateQuotation.Visible = true;
                btnRequestForClaim.Visible = true;
                FillServiceMaterial();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                }
                else
                {
                }

                if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
                {
                    btnRequestForClaim.Visible = false;
                    //  gvMaterial.Columns[10].Visible = false;
                    gvMaterial.Columns[15].Visible = false;
                }
                else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.NEPI) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Commission) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PreCommission))
                {
                    btnRequestForClaim.Visible = true;
                    //  gvMaterial.Columns[10].Visible = true;
                    gvMaterial.Columns[15].Visible = true;
                }
                txtCustomerPayPercentage.Text = Convert.ToString(SDMS_ICTicket.CustomerPayPercentage);
                txtDealerPayPercentage.Text = Convert.ToString(SDMS_ICTicket.DealerPayPercentage);
                txtAEPayPercentage.Text = Convert.ToString(SDMS_ICTicket.AEPayPercentage);
            }


            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty))
            {
                divWarrantyDistribution.Visible = true;
            }
            else
            {
                divWarrantyDistribution.Visible = false;
            }
        }
        public void displayMessage()
        {
            string Message = "";
            if (SDMS_ICTicket.ServiceType == null)
            {
                Message = Message + "Please select Service Type in Service Confirmation screen.</br>";
            }
            if (SDMS_ICTicket.DealerOffice == null)
            {
                Message = Message + "Please select Delivery Location in Service Confirmation screen.</br>";
            }
            if (SDMS_ICTicket.CurrentHMRDate == null)
            {
                Message = Message + "Please select HMR Date in Service Confirmation screen.</br>";
            }
            if (SDMS_ICTicket.CurrentHMRValue == null)
            {
                Message = Message + "Please enter HMR Value in Service Confirmation screen.</br>";
            }
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = Message;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;
            }
        }

        public void FillServiceMaterial()
        {
            // List<PDMS_ServiceMaterial> ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, "", null);
            if (SS_ServiceMaterialAll.Count == 0)
            {
                List<PDMS_ServiceMaterial> ServiceMaterial = new List<PDMS_ServiceMaterial>();
                ServiceMaterial.Add(new PDMS_ServiceMaterial());
                gvMaterial.DataSource = ServiceMaterial;
            }
            else
            {
                gvMaterial.DataSource = SS_ServiceMaterialAll;
                if (SS_ServiceMaterial.Count != 0)
                {
                    btnSaveWarrantyDistribution.Visible = false;
                }

            }

            gvMaterial.DataBind();
            FillTSIRNumberInGvDropDownList();
            for (int i = 0; i < gvMaterial.Rows.Count; i++)
            {
                Label lblCancel = (Label)gvMaterial.Rows[i].FindControl("lblCancel");
                LinkButton lblMaterialRemove = (LinkButton)gvMaterial.Rows[i].FindControl("lblMaterialRemove");
                if (lblCancel.Visible == true)
                {
                    lblMaterialRemove.Visible = false;
                }
            }
        }
        protected void lblMaterialAdd_Click(object sender, EventArgs e)
        {
            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty))
            {
                decimal Percentage = (SDMS_ICTicket.DealerPayPercentage == null ? 0 : (decimal)SDMS_ICTicket.DealerPayPercentage) + (SDMS_ICTicket.AEPayPercentage == null ? 0 : (decimal)SDMS_ICTicket.AEPayPercentage) + (SDMS_ICTicket.CustomerPayPercentage == null ? 0 : (decimal)SDMS_ICTicket.CustomerPayPercentage);
                if (Percentage != 100)
                {
                    lblMessage.Text = "Please check the warranty distribution .";
                    return;
                }
            }

            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            //  btnGenerateQuotation.Focus();
            string Material = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterialF")).Text.Trim();
            if (string.IsNullOrEmpty(Material))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }
            string MaterialSN = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterialSNF")).Text.Trim();

            string DefectiveMaterial = ((TextBox)gvMaterial.FooterRow.FindControl("txtDefectiveMaterialF")).Text.Trim();


            DefectiveMaterial = DefectiveMaterial.Split(' ')[0];
            //if (!DefectiveMaterial.Contains(".FL"))
            //{
            //    lblMessage.Text = "Please enter the correct defective material";
            //    return;
            //}

            string DefectiveMaterialSN = ((TextBox)gvMaterial.FooterRow.FindControl("txtDefectiveMaterialSNF")).Text.Trim();

            TextBox txtQtyF = (TextBox)gvMaterial.FooterRow.FindControl("txtQtyF");
            if (string.IsNullOrEmpty(txtQtyF.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Qty";
                return;
            }
            CheckBox cbRecomenedPartsF = (CheckBox)gvMaterial.FooterRow.FindControl("cbRecomenedPartsF");
            CheckBox cbQuotationPartsF = (CheckBox)gvMaterial.FooterRow.FindControl("cbQuotationPartsF");

            DropDownList ddlMaterialSourceF = (DropDownList)gvMaterial.FooterRow.FindControl("ddlMaterialSourceF");
            int? MaterialSourceID = ddlMaterialSourceF.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMaterialSourceF.SelectedValue);

            CheckBox cbFaultPart = (CheckBox)gvMaterial.FooterRow.FindControl("cbIsFaultyPartF");

            DropDownList ddlTSIRNumberF = (DropDownList)gvMaterial.FooterRow.FindControl("ddlTSIRNumberF");
            long? TsirID = null;
            if (ddlTSIRNumberF.SelectedValue != "0")
            {
                TsirID = Convert.ToInt64(ddlTSIRNumberF.SelectedValue);
                cbRecomenedPartsF.Checked = true;
            }

            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.NEPI) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Commission) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PreCommission))
            //{
            //    if (TsirID == null)
            //    {
            //        lblMessage.Text = "Please select TSIR Number";
            //        return;
            //    }
            //}


            decimal value;
            if (!decimal.TryParse(txtQtyF.Text, out value))
            {
                lblMessage.Text = "Please enter correct format in Qty";
                return;
            }
            //     string 
            Material = Material.Split(' ')[0];
            string MaterialOrg = Material;

            CheckBox cbSupersedeYN = (CheckBox)gvMaterial.FooterRow.FindControl("cbSupersedeYN");
            if (cbSupersedeYN.Checked)
            {
                string smaterial = Material;
                do
                {
                    Material = smaterial;
                    string query = "select s.p_smaterial from af_m_supersede s left join af_m_materials mm on mm.p_material = s.p_rmaterial left join af_m_materials ms on ms.p_material = s.p_smaterial   where s.valid_from <= Now() and s.valid_to >= Now() and  p_rmaterial = '" + smaterial + "'";
                    smaterial = new NpgsqlServer().ExecuteScalar(query);
                } while (!string.IsNullOrEmpty(smaterial));
            }

            for (int i = 0; i < gvMaterial.Rows.Count; i++)
            {
                Label lblMaterialCode = (Label)gvMaterial.Rows[i].FindControl("lblMaterialCode");
                Label lblCancel = (Label)gvMaterial.Rows[i].FindControl("lblCancel");
                if ((lblMaterialCode.Text == Material) && (lblCancel.Visible == false))
                {
                    if (MaterialOrg != Material)
                    {
                        lblMessage.Text = "Part Number " + MaterialOrg + "is superseded with " + Material + "  and already available";
                    }
                    else
                    {
                        lblMessage.Text = "Material " + Material + " already available";
                    }
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }

            string OrderType = "";
            string Customer = "";
            string Vendor = "";
            string IV_SEC_SALES = "X";
            string PRICEDATE = "";
            Boolean IsWarrenty = false;
            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            {
                OrderType = "DEFAULT_SEC_AUART";
                Customer = SDMS_ICTicket.Customer.CustomerCode;
                Vendor = SDMS_ICTicket.Dealer.DealerCode;
            }
            else
            {
                OrderType = "301_DSSOR_SALES_ORDER_HDR";
                Customer = SDMS_ICTicket.Dealer.DealerCode;
                Vendor = "9031";
                IsWarrenty = true;
            }

            decimal Available = 0;
            Boolean IsIGST = true;
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material,null,null,null)[0];


            decimal QtyF = Convert.ToDecimal(txtQtyF.Text);
            PDMS_ServiceMaterial MaterialTax = new SMaterial().getMaterialTax(Customer, Vendor, OrderType, 1, Material, QtyF, IV_SEC_SALES, PRICEDATE, IsWarrenty);


            if (MM.MaterialGroup != "887")
            {
                if (SDMS_ICTicket.ServiceType.ServiceTypeID != (short)DMS_ServiceType.OverhaulService)
                {
                    if (string.IsNullOrEmpty(DefectiveMaterial))
                    {
                        lblMessage.Text = "Please enter the defective material";
                        return;
                    }
                }

                string Qty = new NpgsqlServer().ExecuteScalar("select  r_available_qty from  dmstr_stock where s_tenant_id = " + SDMS_ICTicket.Dealer.DealerCode + " and p_material ='" + Material + "' and p_office ='" + SDMS_ICTicket.DealerOffice.OfficeCode + "' and  p_stock_type='SALE'");
                Available = Convert.ToDecimal("0" + Qty.Trim());
                if (Convert.ToDecimal(txtQtyF.Text) < Available)
                {
                    Available = Convert.ToDecimal(txtQtyF.Text);
                }
            }
            else
            {
                PDMS_Material MM_SQL = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];
                Available = Convert.ToDecimal(txtQtyF.Text);

                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Dealer.DealerCode);
                PDMS_Customer CustomerP = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);

                MaterialTax.BasePrice = MM_SQL.CurrentPrice * Convert.ToDecimal(txtQtyF.Text);
                if (Dealer.State.StateCode == CustomerP.State.StateCode)
                {
                    IsIGST = false;
                    MaterialTax.IGST = MM_SQL.TaxPercentage;
                    MaterialTax.IGSTValue = MM_SQL.TaxPercentage * MaterialTax.BasePrice * 2 / 100;
                }
                else
                {
                    MaterialTax.SGST = MM_SQL.TaxPercentage;
                    MaterialTax.SGSTValue = MM_SQL.TaxPercentage * MaterialTax.BasePrice / 100;
                }
            }


            if (MaterialTax.BasePrice <= 0)
            {
                lblMessage.Text = "Please maintain the price for  Material " + Material + " in SAP";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            MaterialTax.OldInvoice = ((TextBox)gvMaterial.FooterRow.FindControl("txtOldInvoiceF")).Text.Trim();

            long ID = new BDMS_ICTicket().InsertOrUpdateMaterialAddOrRemoveICTicket(0, SDMS_ICTicket.ICTicketID, Material, MaterialSN, DefectiveMaterial
                , DefectiveMaterialSN, QtyF, Available, MaterialSourceID, cbFaultPart.Checked, TsirID, false, PSession.User.UserID
                , IsIGST, cbRecomenedPartsF.Checked, cbQuotationPartsF.Checked, MaterialTax);
            lblMessage.Text = "";
            if (ID != 0)
            {
                lblMessage.ForeColor = Color.Green;
                if (MaterialOrg != Material)
                {
                    lblMessage.Text = "Part Number " + MaterialOrg + "is superseded with " + Material + "  and getting replaced";
                }

                int ServiceTypeID = SDMS_ICTicket.ServiceType.ServiceTypeID;

                //if (((Convert.ToDecimal(txtQtyF.Text) != Available) && (SDMS_ICTicket.IsWarranty) && (MM.MaterialGroup != "887")) 
                //    || ((short) DMS_ServiceType.PolicyWarranty == SDMS_ICTicket.ServiceType.ServiceTypeID)
                //     || ((short)DMS_ServiceType.GoodwillWarranty == SDMS_ICTicket.ServiceType.ServiceTypeID)
                //     || ((short)DMS_ServiceType.PartsWarranty == SDMS_ICTicket.ServiceType.ServiceTypeID )
                //   || cbQuotationPartsF.Checked
                //    )
                if (
                    (Convert.ToDecimal(txtQtyF.Text) != Available) && (MM.MaterialGroup != "887") && cbQuotationPartsF.Checked
                    && (
                            ((short)DMS_ServiceType.PolicyWarranty == ServiceTypeID)
                            || ((short)DMS_ServiceType.GoodwillWarranty == ServiceTypeID)
                            || ((short)DMS_ServiceType.PartsWarranty == ServiceTypeID)
                            || (SDMS_ICTicket.IsWarranty)
                        )
                    )
                {
                    string PO = new BDMS_ICTicket().CreateWarrantyPOForMaterial(SDMS_ICTicket, ID, PSession.User);
                    lblMessage.Text = lblMessage.Text + " And New Warranty PO " + PO;
                }

                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
                FillServiceMaterial();
            }
            else
            {
                lblMessage.Text = "Material " + Material + " is not added";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        {
            List<string> querys = new List<string>();
            lblMessage.Visible = true;
            // btnGenerateQuotation.Focus();
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;


            long ServiceMaterialID = Convert.ToInt64(gvMaterial.DataKeys[gvRow.RowIndex].Value);

            Label lblMaterialCode = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblMaterialCode");

            Label lblQuotationNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblQuotationNumber");
            Label lblSaleOrderNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblSaleOrderNumber");
            Label lblDeliveryNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblDeliveryNumber");
            Label lblClaimNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblClaimNumber");

            TextBox txtQty = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtQty");

            Label lblPONumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblPONumber");

            if (!string.IsNullOrEmpty(lblClaimNumber.Text))
            {
                lblMessage.Text = "Already claim requested. You can cancel the claim and then remove material.";
                lblMessage.ForeColor = Color.Red;
                return;
            }


            Label lblTsirID = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblTsirID");
            if (!string.IsNullOrEmpty(lblTsirID.Text))
            {
                PDMS_ICTicketTSIR Ts = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(Convert.ToInt64(lblTsirID.Text), null);
                if ((Ts.Status.StatusID == (short)TSIRStatus.Requested) || (Ts.Status.StatusID == (short)TSIRStatus.Rerequested) || (Ts.Status.StatusID == (short)TSIRStatus.SendBack) || (Ts.Status.StatusID == (short)TSIRStatus.Rejected))
                {

                }
                else
                {
                    lblMessage.Text = "TSIR Status should be in Requested , Re-Requested , Send Back or Rejected.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }


            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, lblMaterialCode.Text, null, null, null)[0];
            string Status = "";
            if (MM.MaterialGroup != "887")
            {

                //Status = new NpgsqlServer().ExecuteScalar(" select s_status from dssor_sales_order_item  where  s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + " and 	p_so_id ='" + lblQuotationNumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'");
                //if (Status == "DRAFT" || Status == "CANCELED")
                //{
                // new NpgsqlServer().ExecuteScalar("update dssor_sales_order_item set s_status = 'CANCELED' where s_status = 'DRAFT' and  s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + "  and 	p_so_id ='" + lblQuotationNumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'");
                // new NpgsqlServer().ExecuteScalar("delete from dssor_sales_order_item  where s_status = 'DRAFT' and  s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + "  and 	p_so_id ='" + lblQuotationNumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'");
                // new NpgsqlServer().ExecuteScalar("update dssor_sales_order_item set s_status = 'CANCELLED' where   s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + "  and 	p_so_id ='" + lblQuotationNumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'");
                string Q1 = "update dssor_sales_order_item set s_status = 'CANCELLED' where s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + "  and p_so_id ='" + lblQuotationNumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'";
                querys.Add(Q1);
                //}
                //else if (!string.IsNullOrEmpty(lblQuotationNumber.Text))
                //{
                //    lblMessage.Text = "Already sale order created or delivery happened so you can cancel the sale order and then remove the material.";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}

                if (!string.IsNullOrEmpty(lblDeliveryNumber.Text.Trim()))
                {
                    string f_office = new NpgsqlServer().ExecuteScalar("select  f_office from dsder_delv_item  where p_del_id ='" + lblDeliveryNumber.Text.Trim() + "' and f_material_id='" + lblMaterialCode.Text + "' limit 1");
                    string p_location = new NpgsqlServer().ExecuteScalar("select  f_location from dsder_delv_item  where p_del_id ='" + lblDeliveryNumber.Text.Trim() + "' and f_material_id='" + lblMaterialCode.Text + "' limit 1");

                    string Q2 = "INSERT INTO public.af_stock_ledger_icticket(" +
                       "s_establishment, s_tenant_id, p_location, p_office, p_material, p_stock_type,  p_batch, r_document_type, r_document_id, r_posting_date, f_ref_id1, r_opening_qty, r_inward_qty, r_outward_qty, r_closing_qty, r_current_stock, nes_flag, s_status, created_by, created_on) "
         + " VALUES (1000, " + SDMS_ICTicket.Dealer.DealerCode + ", '" + p_location + "','" + f_office + "', '" + lblMaterialCode.Text + "', 'SALE', 'B1', 'DLV','" + lblDeliveryNumber.Text.Trim() + "', now(),'" + SDMS_ICTicket.ICTicketNumber + "', 0,+" + txtQty.Text + ", 0, 0, 0, 'N','Created','sa',now() )";

                    querys.Add(Q2);
                }

                //string StatusWPO = new NpgsqlServer().ExecuteScalar(" select s_status from dppor_purc_order_item  where  s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + " and k_po_id ='" + lblPONumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'");
                //if (StatusWPO == "DRAFT" || StatusWPO == "CANCELLED")
                //{
                //     string Q3 = "Delete from dppor_purc_order_item where s_status = 'DRAFT' and  s_tenant_id = " + SDMS_ICTicket.Dealer.DealerCode + "  and k_po_id ='" + lblPONumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'";
                //    querys.Add(Q3);
                //}
                //else if (!string.IsNullOrEmpty(lblPONumber.Text))
                //{
                //    lblMessage.Text = "Already PO processed so you can cancel the PO and then remove the material.";
                //    lblMessage.ForeColor = Color.Red;
                //    return;
                //}

            }
            long ID = new BDMS_ICTicket().InsertOrUpdateMaterialAddOrRemoveICTicket(ServiceMaterialID, SDMS_ICTicket.ICTicketID, "", "", "", "", 0, 0, null, false, 0, true, PSession.User.UserID
                , false, false, false, new PDMS_ServiceMaterial());

            if (ID != 0)
            {
                if (new NpgsqlServer().UpdateTransactions(querys))
                {
                    new BDMS_ICTicket().UpdateMaterialRemoveICTicketSapStatus(ServiceMaterialID, true);
                }
                else
                {
                    new BDMS_ICTicket().UpdateMaterialRemoveICTicketSapStatus(ServiceMaterialID, false);
                }
                lblMessage.Text = "Material is Removed successfully";
                lblMessage.ForeColor = Color.Green;
                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
                FillServiceMaterial();
            }
            else
            {
                lblMessage.Text = "Material is not Removed successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnGenerateQuotation_Click_Old(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            ///  btnGenerateQuotation.Focus(); 
            List<PDMS_ServiceMaterial> MaterialList = new List<PDMS_ServiceMaterial>();
            // List<PDMS_ServiceMaterial> ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, "", false);
            List<string> Quot = new List<string>();


            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others))
            {
                foreach (PDMS_ServiceMaterial Mat in SS_ServiceMaterial)
                {
                    if (!string.IsNullOrEmpty(Mat.QuotationNumber))
                    {
                        if (!Quot.Contains(Mat.QuotationNumber))
                            Quot.Add(Mat.QuotationNumber);
                        continue;
                    }
                    if ((Mat.IsQuotationParts == false))
                    {
                        continue;
                    }
                    //if (Material.Material.MaterialGroup == "887")
                    //{
                    //    continue;
                    //} 
                    MaterialList.Add(Mat);
                }
            }
            else
            {
                foreach (PDMS_ServiceMaterial Mat in SS_ServiceMaterial)
                {
                    if (Mat.TSIR == null)
                    {
                        continue;
                    }
                    if ((Mat.TSIR.Status.StatusID == (short)TSIRStatus.Approved) || (Mat.TSIR.Status.StatusID == (short)TSIRStatus.SalesApproved))
                    {
                    }
                    else
                    {
                        continue;
                    }


                    if (!string.IsNullOrEmpty(Mat.QuotationNumber))
                    {
                        if (!Quot.Contains(Mat.QuotationNumber))
                            Quot.Add(Mat.QuotationNumber);
                        continue;
                    }
                    if ((Mat.IsQuotationParts == false))
                    {
                        continue;
                    }
                    //if (Material.MaterialSource.MaterialSourceID == (short)MaterialSource.Customer)
                    //{
                    //    continue;
                    //}
                    //if (Material.Material.MaterialGroup == "887")
                    //{
                    //    continue;
                    //} 
                    MaterialList.Add(Mat);
                }
            }
            if (MaterialList.Count == 0)
            {
                lblMessage.Text = "Please check the material list. It May be empty or TSIR may not be approved.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string Quotation = new BDMS_ICTicket().CreateQuotationForMaterial(SDMS_ICTicket, MaterialList, PSession.User, Quot);


            if (!string.IsNullOrEmpty(Quotation))
            {
                lblMessage.Text = "Quotation (" + Quotation + ") is created successfully";
                lblMessage.ForeColor = Color.Green;
                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
                FillServiceMaterial();
            }
            else
            {
                lblMessage.Text = "Quotation  is not created successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnGenerateQuotation_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            List<PDMS_ServiceMaterial> MaterialList = new List<PDMS_ServiceMaterial>();
            List<string> Quot = new List<string>();

            foreach (PDMS_ServiceMaterial Mat in SS_ServiceMaterial)
            {
                if (!string.IsNullOrEmpty(Mat.QuotationNumber))
                {
                    if (!Quot.Contains(Mat.QuotationNumber))
                        Quot.Add(Mat.QuotationNumber);
                    continue;
                }
                if ((Mat.IsQuotationParts == false))
                {
                    continue;
                }
                //if (Mat.Material.MaterialGroup == "887")
                //{
                //    continue;
                //} 
                MaterialList.Add(Mat);
            }

            if (MaterialList.Count == 0)
            {
                lblMessage.Text = "Please check the material list.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string Quotation = new BDMS_ICTicket().CreateQuotationForMaterial(SDMS_ICTicket, MaterialList, PSession.User, Quot);


            if (!string.IsNullOrEmpty(Quotation))
            {
                lblMessage.Text = "Quotation (" + Quotation + ") is created successfully";
                lblMessage.ForeColor = Color.Green;
                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
                FillServiceMaterial();
            }
            else
            {
                lblMessage.Text = "Quotation  is not created successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnRequestForClaim_Click(object sender, EventArgs e)
        {

            lblMessage.Visible = true;

            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PreCommission) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Commission))
            {

            }
            else if ((SDMS_ICTicket.Equipment.CommissioningOn == null) && SDMS_ICTicket.IsWarranty)
            {
                lblMessage.Text = "Please Update Commissioning date";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (SS_ServiceMaterial.Count == 0)
            {
                lblMessage.Text = "Please add the material.";
                lblMessage.ForeColor = Color.Red;
                return;
            }


            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty))
            {
                if ((SDMS_ICTicket.CustomerPayPercentage == null) || (SDMS_ICTicket.DealerPayPercentage == null) || (SDMS_ICTicket.AEPayPercentage == null))
                {
                    lblMessage.Text = "Please check the warranty distribution .";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                decimal TotalP = (decimal)SDMS_ICTicket.CustomerPayPercentage + (decimal)SDMS_ICTicket.DealerPayPercentage + (decimal)SDMS_ICTicket.AEPayPercentage;
                if (TotalP != 100)
                {
                    lblMessage.Text = "Please check the warranty distribution total.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }

            string MaterialCode = "";
            foreach (PDMS_ServiceMaterial Material in SS_ServiceMaterial)
            {
                //  if ((string.IsNullOrEmpty(Material.ClaimNumber)) && (!string.IsNullOrEmpty(Material.DeliveryNumber)) && (Material.IsCustomerStock == false)) 
                if (string.IsNullOrEmpty(Material.ClaimNumber) && Material.BasePrice == 0)
                {
                    lblMessage.Text = "Claim material ( " + Material.Material.MaterialCode + " ) price is not maintained correctly. Please call Admin.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if ((string.IsNullOrEmpty(Material.ClaimNumber)) && (!string.IsNullOrEmpty(Material.DeliveryNumber)))
                {
                    MaterialCode = Material.Material.MaterialCode;
                    break;
                }
                else if (string.IsNullOrEmpty(Material.ClaimNumber) && (Material.Material.MaterialGroup == "887"))
                {
                    MaterialCode = Material.Material.MaterialCode;
                    break;
                }
            }
            if (string.IsNullOrEmpty(MaterialCode))
            {
                lblMessage.Text = "Already claim requested for all material or delivery not happened";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            if (new BDMS_ICTicket().InsertClaimForMaterial(SDMS_ICTicket.ICTicketID, PSession.User.UserID))
            {
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
                lblMessage.Text = "Claim is Requested";

                foreach (PDMS_ServiceMaterial Material in SS_ServiceMaterial)
                {
                    if (MaterialCode == Material.Material.MaterialCode)
                    {
                        lblMessage.Text = "Claim ( " + Material.ClaimNumber + " ) is Requested";
                        break;
                    }
                }
                lblMessage.ForeColor = Color.Green;
                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                FillServiceMaterial();
            }
            else
            {
                lblMessage.Text = "Claim is not Requested";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void lbCheckAvailability_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            // btnGenerateQuotation.Focus();
            TextBox txtMaterialF = (TextBox)gvMaterial.FooterRow.FindControl("txtMaterialF");
            Label lblCheckAvailability = (Label)gvMaterial.FooterRow.FindControl("lblCheckAvailability");
            if (string.IsNullOrEmpty(txtMaterialF.Text.Trim()))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }
            string Material = txtMaterialF.Text.Split(' ')[0];

            string MaterialOrg = Material;

            CheckBox cbSupersedeYN = (CheckBox)gvMaterial.FooterRow.FindControl("cbSupersedeYN");
            if (cbSupersedeYN.Checked)
            {
                string smaterial = Material;
                do
                {
                    Material = smaterial;
                    string query = "select s.p_smaterial from af_m_supersede s left join af_m_materials mm on mm.p_material = s.p_rmaterial left join af_m_materials ms on ms.p_material = s.p_smaterial   where s.valid_from <= Now() and s.valid_to >= Now() and  p_rmaterial = '" + smaterial + "'";
                    smaterial = new NpgsqlServer().ExecuteScalar(query);
                } while (!string.IsNullOrEmpty(smaterial));
            }

            string Qty = new NpgsqlServer().ExecuteScalar("select  r_available_qty from  dmstr_stock where s_tenant_id = " + SDMS_ICTicket.Dealer.DealerCode + " and p_material ='" + Material + "' and p_office ='" + SDMS_ICTicket.DealerOffice.OfficeCode + "' and  p_stock_type='SALE'");
            if (MaterialOrg != Material)
            {
                lblMessage.Text = "Available Qty for superseded  " + Material + " : " + Convert.ToDecimal("0" + Qty.Trim()).ToString("F");
            }
            else
            {
                lblMessage.Text = "Available Qty  " + Material + " : " + Convert.ToDecimal("0" + Qty.Trim()).ToString("F");
            }
            lblMessage.ForeColor = Color.Green;
            //   lblCheckAvailability.Text = Qty; 
        }

        protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQuotationNumber = (Label)e.Row.FindControl("lblQuotationNumber");
                if (!string.IsNullOrEmpty(lblQuotationNumber.Text))
                {
                    CheckBox cbQuotationParts = (CheckBox)e.Row.FindControl("cbQuotationParts");
                    cbQuotationParts.Enabled = false;
                }
                Label lblMaterialSourceID = (Label)e.Row.FindControl("lblMaterialSourceID");
                DropDownList ddlMaterialSource = (DropDownList)e.Row.FindControl("ddlMaterialSource");
                ddlMaterialSource.DataTextField = "MaterialSource";
                ddlMaterialSource.DataValueField = "MaterialSourceID";
                ddlMaterialSource.DataSource = MSource;
                ddlMaterialSource.DataBind();
                ddlMaterialSource.Items.Insert(0, new ListItem("Select", "0"));
                if (!string.IsNullOrEmpty(lblMaterialSourceID.Text))
                    ddlMaterialSource.SelectedValue = lblMaterialSourceID.Text;

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlMaterialSourceF = (DropDownList)e.Row.FindControl("ddlMaterialSourceF");
                ddlMaterialSourceF.DataTextField = "MaterialSource";
                ddlMaterialSourceF.DataValueField = "MaterialSourceID";
                ddlMaterialSourceF.DataSource = MSource;
                ddlMaterialSourceF.DataBind();
                ddlMaterialSourceF.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void cbEdit_CheckedChanged(object sender, EventArgs e)
        {
            // btnGenerateQuotation.Focus();
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            CheckBox cbEdit = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbEdit");
            Label lblQuotationNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblQuotationNumber");

            //if (!string.IsNullOrEmpty(lblQuotationNumber.Text))
            //{
            //    lblMessage.Text = "Quotation already created so that you cannot edit the material";
            //    lblMessage.ForeColor = Color.Red;
            //    lblMessage.Visible = true;
            //    cbEdit.Checked = false;
            //}            

            Label lblTsirID = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblTsirID");
            //if (!string.IsNullOrEmpty(lblTsirID.Text))
            //{
            //    PDMS_ICTicketTSIR Ts = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(Convert.ToInt64(lblTsirID.Text), null);
            //    if ((Ts.Status.StatusID == 1) || (Ts.Status.StatusID == 6))
            //    {

            //    }
            //    else
            //    {
            //        lblMessage.Text = "TSIR Status should be in Requested or Re-Requested.";
            //        lblMessage.ForeColor = Color.Red;
            //        lblMessage.Visible = true;
            //        cbEdit.Checked = false;
            //    }
            //}


            if (cbEdit.Checked)
            {
                for (int i = 0; i < gvMaterial.Rows.Count; i++)
                {
                    cbEdit = (CheckBox)gvMaterial.Rows[i].FindControl("cbEdit");
                    cbEdit.Visible = false;
                }

                LinkButton lbUpdate = (LinkButton)gvMaterial.Rows[gvRow.RowIndex].FindControl("lbUpdate");
                LinkButton lbEditCancel = (LinkButton)gvMaterial.Rows[gvRow.RowIndex].FindControl("lbEditCancel");
                lbUpdate.Visible = true;
                lbEditCancel.Visible = true;

                Label lblMaterialSN = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblMaterialSN");
                TextBox txtMaterialSN = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtMaterialSN");
                lblMaterialSN.Visible = false;
                txtMaterialSN.Visible = true;


                CheckBox cbIsFaultyPart = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbIsFaultyPart");
                cbIsFaultyPart.Enabled = true;



                Label lblDefectiveMaterialSN = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblDefectiveMaterialSN");
                TextBox txtDefectiveMaterialSN = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtDefectiveMaterialSN");
                lblDefectiveMaterialSN.Visible = false;
                txtDefectiveMaterialSN.Visible = true;

                CheckBox cbRecomenedParts = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbRecomenedParts");
                cbRecomenedParts.Enabled = true;

                if (string.IsNullOrEmpty(lblQuotationNumber.Text))
                {
                    TextBox txtQty = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtQty");
                    txtQty.Enabled = true;

                    CheckBox cbQuotationParts = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbQuotationParts");
                    cbQuotationParts.Enabled = true;

                }

                Label lblMaterialSource = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblMaterialSource");
                Label lblMaterialSourceID = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblMaterialSourceID");
                DropDownList ddlMaterialSource = (DropDownList)gvMaterial.Rows[gvRow.RowIndex].FindControl("ddlMaterialSource");
                lblMaterialSource.Visible = false;
                ddlMaterialSource.Visible = true;

                if (!string.IsNullOrEmpty(lblMaterialSourceID.Text))
                    ddlMaterialSource.SelectedValue = lblMaterialSourceID.Text;

                Label lblTsirNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblTsirNumber");

                DropDownList ddlTSIRNumber = (DropDownList)gvMaterial.Rows[gvRow.RowIndex].FindControl("ddlTSIRNumber");
                ddlTSIRNumber.DataTextField = "TsirNumber";
                ddlTSIRNumber.DataValueField = "TsirID";

                //  TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIR(null, null, null, null, null, SDMS_ICTicket.ICTicketNumber, null, null, null, null, null, null, "", null);

                //   TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);

                List<PDMS_ICTicketTSIR> ddlTSIR = new List<PDMS_ICTicketTSIR>();
                foreach (PDMS_ICTicketTSIR t in ICTicketTSIRs)
                {
                    //if ((t.Status.StatusID == 1) || (t.Status.StatusID == 6))
                    //{
                    if ((t.Status.StatusID != (short)TSIRStatus.Canceled))
                    {
                        ddlTSIR.Add(new PDMS_ICTicketTSIR() { TsirID = t.TsirID, TsirNumber = t.TsirNumber });
                    }
                    //  }
                }

                lblTsirNumber.Visible = false;
                ddlTSIRNumber.Visible = true;
                ddlTSIRNumber.DataSource = ddlTSIR;
                ddlTSIRNumber.DataBind();
                ddlTSIRNumber.Items.Insert(0, new ListItem("Select", "0"));

                if (!string.IsNullOrEmpty(lblTsirID.Text))
                    ddlTSIRNumber.SelectedValue = lblTsirID.Text;

            }
        }

        protected void lbUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            PDMS_ServiceMaterial ServiceMaterial = new PDMS_ServiceMaterial();

            long ServiceMaterialID = Convert.ToInt64(gvMaterial.DataKeys[gvRow.RowIndex].Value);

            CheckBox cbEdit = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbEdit");
            LinkButton lbUpdate = (LinkButton)gvMaterial.Rows[gvRow.RowIndex].FindControl("lbUpdate");
            LinkButton lbEditCancel = (LinkButton)gvMaterial.Rows[gvRow.RowIndex].FindControl("lbEditCancel");

            TextBox txtMaterialSN = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtMaterialSN");
            TextBox txtQty = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtQty");

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
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

            CheckBox cbIsFaultyPart = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbIsFaultyPart");
            TextBox txtDefectiveMaterialSN = (TextBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("txtDefectiveMaterialSN");

            CheckBox cbRecomenedParts = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbRecomenedParts");
            CheckBox cbQuotationParts = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbQuotationParts");
            DropDownList ddlMaterialSource = (DropDownList)gvMaterial.Rows[gvRow.RowIndex].FindControl("ddlMaterialSource");
            DropDownList ddlTSIRNumber = (DropDownList)gvMaterial.Rows[gvRow.RowIndex].FindControl("ddlTSIRNumber");


            ServiceMaterial.ServiceMaterialID = ServiceMaterialID;

            ServiceMaterial.Material = new PDMS_Material() { MaterialSerialNumber = txtMaterialSN.Text.Trim() };
            ServiceMaterial.Qty = Convert.ToInt32(txtQty.Text.Trim());
            ServiceMaterial.IsFaultyPart = cbIsFaultyPart.Checked;
            ServiceMaterial.DefectiveMaterial = new PDMS_Material() { MaterialSerialNumber = txtDefectiveMaterialSN.Text.Trim() };

            ServiceMaterial.IsRecomenedParts = cbRecomenedParts.Checked;
            ServiceMaterial.IsQuotationParts = cbQuotationParts.Checked;
            ServiceMaterial.MaterialSource = ddlMaterialSource.SelectedValue == "0" ? null : new PDMS_MaterialSource() { MaterialSourceID = Convert.ToInt32(ddlMaterialSource.SelectedValue) };

            ServiceMaterial.TSIR = null;
            if (ddlTSIRNumber.SelectedValue != "0")
            {
                ServiceMaterial.TSIR = new PDMS_ICTicketTSIR() { TsirID = Convert.ToInt64(ddlTSIRNumber.SelectedValue) };
                ServiceMaterial.IsRecomenedParts = true;
            }
            if (new BDMS_Service().UpdateICTicketMaterial(ServiceMaterial, PSession.User.UserID))
            {
                lblMessage.Text = "Material is updated successfully";
                lblMessage.ForeColor = Color.Green;
                Label lblPONumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblPONumber");
                if (string.IsNullOrEmpty(lblPONumber.Text))
                {
                    int ServiceTypeID = SDMS_ICTicket.ServiceType.ServiceTypeID;
                    Label lblMaterialCode = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblMaterialCode");
                    PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, lblMaterialCode.Text, null, null, null)[0];
                    decimal Available = 0;
                    string Qty = new NpgsqlServer().ExecuteScalar("select  r_available_qty from  dmstr_stock where s_tenant_id = " + SDMS_ICTicket.Dealer.DealerCode + " and p_material ='" + lblMaterialCode.Text + "' and p_office ='" + SDMS_ICTicket.DealerOffice.OfficeCode + "' and  p_stock_type='SALE'");
                    Available = Convert.ToDecimal("0" + Qty.Trim());
                    if (Convert.ToDecimal(txtQty.Text) < Available)
                    {
                        Available = Convert.ToDecimal(txtQty.Text);
                    }
                    if ((Convert.ToDecimal(txtQty.Text) != Available) && (MM.MaterialGroup != "887") && cbQuotationParts.Checked &&
                        (
                               ((short)DMS_ServiceType.PolicyWarranty == ServiceTypeID)
                            || ((short)DMS_ServiceType.GoodwillWarranty == ServiceTypeID)
                            || ((short)DMS_ServiceType.PartsWarranty == ServiceTypeID)
                            || (SDMS_ICTicket.IsWarranty)
                          )
                        )
                    {
                        string PO = new BDMS_ICTicket().CreateWarrantyPOForMaterial(SDMS_ICTicket, ServiceMaterialID, PSession.User);
                        lblMessage.Text = lblMessage.Text + " And New Warranty PO " + PO;
                    }
                }
                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
                FillServiceMaterial();
            }
            else
            {
                lblMessage.Text = "Material is not updated successfully";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;

        }

        protected void lbEditCancel_Click(object sender, EventArgs e)
        {
            FillServiceMaterial();
            // btnGenerateQuotation.Focus();
        }
        public void FillTSIRNumberInGvDropDownList()
        {
            //  TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIR(null, null, null, null, null, SDMS_ICTicket.ICTicketNumber, null, null, null, null, null, null, "",null);
            //  TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            List<PDMS_ICTicketTSIR> ddlTSIR = new List<PDMS_ICTicketTSIR>();
            foreach (PDMS_ICTicketTSIR t in ICTicketTSIRs)
            {
                if ((t.Status.StatusID == 1) || (t.Status.StatusID == 6))
                {
                    ddlTSIR.Add(new PDMS_ICTicketTSIR() { TsirID = t.TsirID, TsirNumber = t.TsirNumber });
                }
            }
            //for (int i = 0; i < gvMaterial.Rows.Count; i++)
            //{
            //    DropDownList ddlTSIRNumber = (DropDownList)gvMaterial.Rows[i].FindControl("ddlTSIRNumber");
            //    ddlTSIRNumber.Items.Clear();
            //    ddlTSIRNumber.DataTextField = "TsirNumber";
            //    ddlTSIRNumber.DataValueField = "TsirID";
            //    ddlTSIRNumber.DataSource = ddlTSIR;
            //    ddlTSIRNumber.DataBind();
            //    ddlTSIRNumber.Items.Insert(0, new ListItem("Select", "0"));
            //    Label lblTsirID = (Label)gvMaterial.Rows[i].FindControl("lblTsirID");
            //    if (!string.IsNullOrEmpty(lblTsirID.Text))
            //        ddlTSIRNumber.SelectedValue = lblTsirID.Text;
            //}
            DropDownList ddlTSIRNumberF = (DropDownList)gvMaterial.FooterRow.FindControl("ddlTSIRNumberF");
            ddlTSIRNumberF.DataTextField = "TsirNumber";
            ddlTSIRNumberF.DataValueField = "TsirID";
            ddlTSIRNumberF.DataSource = ddlTSIR;
            ddlTSIRNumberF.DataBind();
            ddlTSIRNumberF.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void btnSaveWarrantyDistribution_Click(object sender, EventArgs e)
        {

            decimal? CustomerPayPercentage = string.IsNullOrEmpty(txtCustomerPayPercentage.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtCustomerPayPercentage.Text.Trim());
            decimal? DealerPayPercentage = string.IsNullOrEmpty(txtDealerPayPercentage.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtDealerPayPercentage.Text.Trim());
            decimal? AEPayPercentage = string.IsNullOrEmpty(txtAEPayPercentage.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtAEPayPercentage.Text.Trim());


            if ((CustomerPayPercentage == null) || (DealerPayPercentage == null) || (AEPayPercentage == null))
            {
                lblMessage.Text = "Please check the warranty distribution .";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            decimal TotalP = (decimal)CustomerPayPercentage + (decimal)DealerPayPercentage + (decimal)AEPayPercentage;
            if (TotalP != 100)
            {
                lblMessage.Text = "Please check the warranty distribution total.";
                lblMessage.ForeColor = Color.Red;
                return;
            }


            if (new BDMS_ICTicket().UpdateICTicketWarrantyDistribution(SDMS_ICTicket.ICTicketID, CustomerPayPercentage, DealerPayPercentage, AEPayPercentage))
            {
                lblMessage.Text = "ICTicket is updated successfully";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "ICTicket is not updated successfully";
            }
        }
    }
}