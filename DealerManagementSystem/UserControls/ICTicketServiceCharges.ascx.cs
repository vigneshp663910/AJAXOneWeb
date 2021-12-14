using AjaxControlToolkit;
using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketServiceCharges : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }

        public List<Int16> RefreshList
        {
            get
            {
                if (Session["DMS_RefreshList"] == null)
                {
                    Session["DMS_RefreshList"] = new List<Int16>();
                }
                return (List<Int16>)Session["DMS_RefreshList"];
            }
            set
            {
                Session["DMS_RefreshList"] = value;
            }
        }
        public string TaxP
        {
            get
            {
                return (string)Session["txtTaxP"];
            }
            set
            {
                Session["txtTaxP"] = value;
            }
        }
        //public PDMS_ICTicketTSIR ICTicketTSIR1
        //{
        //    get
        //    {
        //        if (ViewState["PDMS_ICTicketTSIR"] == null)
        //        {
        //            ViewState["PDMS_ICTicketTSIR"] = new PDMS_ICTicketTSIR();
        //        }
        //        return (PDMS_ICTicketTSIR)ViewState["PDMS_ICTicketTSIR"];
        //    }
        //    set
        //    {
        //        ViewState["PDMS_ICTicketTSIR"] = value;
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
        //public List<PDMS_ServiceMaterial> SS_ServiceMaterial
        //{
        //    get
        //    {
        //        if (Session["ServiceMaterialICTicketProcess"] == null)
        //        {
        //            Session["ServiceMaterialICTicketProcess"] = new List<PDMS_ServiceMaterial>();
        //        }
        //        return (List<PDMS_ServiceMaterial>)Session["ServiceMaterialICTicketProcess"];
        //    }
        //    set
        //    {
        //        Session["ServiceMaterialICTicketProcess"] = value;
        //    }
        //}
        public List<PDMS_ServiceCharge> SS_ServiceCharge
        {
            get
            {
                if (Session["ServiceChargeICTicketProcess"] == null)
                {
                    Session["ServiceChargeICTicketProcess"] = new List<PDMS_ServiceCharge>();
                }
                return (List<PDMS_ServiceCharge>)Session["ServiceChargeICTicketProcess"];
            }
            set
            {
                Session["ServiceChargeICTicketProcess"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

                TaxP = "";
                if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
                {
                    long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                    //  SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);


                    lblMessage.ForeColor = Color.Red;

                    btnGenerateQuotation.Visible = false;
                    btnGenerateProfarmaInvoice.Visible = false;
                    btnGenerateInvoice.Visible = false;
                    btnRequestForClaim.Visible = false;



                    btnGenerateQuotation.Visible = true;
                    btnGenerateProfarmaInvoice.Visible = true;
                    btnGenerateInvoice.Visible = true;
                    btnRequestForClaim.Visible = true;
                    FillServiceCharges();
                    ((TextBox)gvServiceCharges.HeaderRow.FindControl("txtTaxP")).Text = "10";
                    //if (SDMS_ICTicket.Category1 == null)
                    //{
                    //    return;
                    //}
                    //HttpContext.Current.Session["Category1ID"] = SDMS_ICTicket.Category1.Category1ID;
                    if (SDMS_ICTicket.ServiceType != null)
                        HttpContext.Current.Session["ServiceTypeID"] = SDMS_ICTicket.ServiceType.ServiceTypeID;
                    if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                    {

                    }
                    else
                    {

                    }
                    CalendarExtender ceServiceDate = (CalendarExtender)gvServiceCharges.FooterRow.FindControl("ceServiceDate");
                    ceServiceDate.StartDate = SDMS_ICTicket.ReachedDate;

                }
            }
        }
        public void displayMessage()
        {
            string Message = "";
            if ((SDMS_ICTicket.ServiceType == null) || (SDMS_ICTicket.DealerOffice == null) || (SDMS_ICTicket.CurrentHMRDate == null) || (SDMS_ICTicket.CurrentHMRValue == null))
            {
                Message = "For Service and Material charge entry Reached Date and Time,Location, Service type,Delivery location, HMR value with date are mandatory.";
            }
            //if (SDMS_ICTicket.DealerOffice == null)
            //{
            //    Message = Message + "Please select Delivery Location in Service Confirmation screen. </br>";
            //}
            //if (SDMS_ICTicket.CurrentHMRDate == null)
            //{
            //    Message = Message + "Please select HMR Date in Service Confirmation screen.</br>";
            //}
            //if (SDMS_ICTicket.CurrentHMRValue == null)
            //{
            //    Message = Message + "Please enter HMR Value in Service Confirmation screen.";
            //}
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Maroon;
            }
            else
            {
                lblMessage.Visible = false;
            }
        }

        //protected void lblNoteAdd_Click(object sender, EventArgs e)
        //{

        //    DropDownList ddlNoteType = (DropDownList)gvNotes.FooterRow.FindControl("ddlNoteType");
        //    TextBox txtComments = (TextBox)gvNotes.FooterRow.FindControl("txtComments");
        //    if (ddlNoteType.SelectedValue == "0")
        //    {
        //        return;
        //    }
        //    new BDMS_ICTicket().InsertOrUpdateNoteAddOrRemoveICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(ddlNoteType.SelectedValue), txtComments.Text, false, PSession.User.UserID);

        //}

        public void FillServiceCharges()
        {

            List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            string ClaimNumber = "";
            if (Charge.Count == 0)
            {
                PDMS_ServiceCharge c = new PDMS_ServiceCharge();
                Charge.Add(c);
            }
            else
            {
                ClaimNumber = Charge[0].ClaimNumber;
            }

            gvServiceCharges.DataSource = Charge;
            gvServiceCharges.DataBind();
            gvServiceCharges.FooterRow.Visible = true;

            HttpContext.Current.Session["IsMainServiceMaterial"] = false;
            if (gvServiceCharges.Rows.Count == 1)
            {
                Label lblMaterialCode = (Label)gvServiceCharges.Rows[0].FindControl("lblMaterialCode");
                if (string.IsNullOrEmpty(lblMaterialCode.Text))
                {
                    HttpContext.Current.Session["IsMainServiceMaterial"] = true;
                }
                else
                {
                    if (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PromotionalActivity)
                    {
                        gvServiceCharges.FooterRow.Visible = false;
                    }
                }
            }

            TextBox txtServiceMaterial = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceMaterial");
            TextBox txtServiceDate = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceDate");
            TextBox txtWorkedHours = (TextBox)gvServiceCharges.FooterRow.FindControl("txtWorkedHours");
            TextBox txtBasePrice = (TextBox)gvServiceCharges.FooterRow.FindControl("txtBasePrice");
            TextBox txtDiscount = (TextBox)gvServiceCharges.FooterRow.FindControl("txtDiscount");
            LinkButton lblServiceAdd = (LinkButton)gvServiceCharges.FooterRow.FindControl("lblServiceAdd");

            if (SDMS_ICTicket.ServiceType == null)
            {
                txtWorkedHours.Visible = false;
                txtBasePrice.Visible = false;
                txtDiscount.Visible = false;
            }
            else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
                )
            {
                txtWorkedHours.Visible = true;
                txtBasePrice.Visible = true;
                txtDiscount.Visible = true;
            }
            else
            {
                txtWorkedHours.Visible = false;
                txtBasePrice.Visible = false;
                txtDiscount.Visible = false;
            }
            DataControlField gcServiceCharges = gvServiceCharges.Columns[14];
            gcServiceCharges.Visible = true;

            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
                )
            {
                gvServiceCharges.Columns[8].Visible = false;

                btnRequestForClaim.Visible = false;
                btnGenerateQuotation.Visible = true;
                btnGenerateProfarmaInvoice.Visible = true;
                btnGenerateInvoice.Visible = true;
                txtServiceMaterial.Visible = true;
                lblServiceAdd.Visible = true;

                txtWorkedHours.Visible = true;
                txtBasePrice.Visible = true;
                txtDiscount.Visible = true;
                txtServiceDate.Visible = true;

                List<PDMS_PaidServiceInvoice> Invoices = new BDMS_Service().GetPaidServiceInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "", true);
                if (Invoices.Count == 1)
                {
                    btnGenerateQuotation.Visible = false;
                    btnGenerateProfarmaInvoice.Visible = false;
                    btnGenerateInvoice.Visible = false;

                    txtServiceMaterial.Visible = false;
                    lblServiceAdd.Visible = false;

                    txtWorkedHours.Visible = false;
                    txtBasePrice.Visible = false;
                    txtDiscount.Visible = false;
                    txtServiceDate.Visible = false;
                    gcServiceCharges.Visible = false;
                    gvServiceCharges.FooterRow.Visible = false;
                }
                else
                {
                    List<PDMS_PaidServiceInvoice> Proformas = new BDMS_Service().GetPaidServiceProformaInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
                    if (Proformas.Count == 1)
                    {
                        btnGenerateProfarmaInvoice.Visible = false;
                        btnGenerateQuotation.Visible = false;
                        txtServiceMaterial.Visible = false;
                        lblServiceAdd.Visible = false;

                        txtWorkedHours.Visible = false;
                        txtBasePrice.Visible = false;
                        txtDiscount.Visible = false;
                        txtServiceDate.Visible = false;
                        gcServiceCharges.Visible = false;
                        gvServiceCharges.FooterRow.Visible = false;
                    }
                    else
                    {
                        List<PDMS_PaidServiceInvoice> SOIs = new BDMS_Service().GetPaidServiceQuotation(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
                        if (SOIs.Count == 1)
                        {
                            btnGenerateQuotation.Visible = false;
                            // txtServiceMaterial.Visible = false;
                            // lblServiceAdd.Visible = false;

                            // txtWorkedHours.Visible = false;
                            // txtBasePrice.Visible = false;
                            // txtDiscount.Visible = false;
                            // txtServiceDate.Visible = false;
                            //  gcServiceCharges.Visible = false;
                            //  gvServiceCharges.FooterRow.Visible = false;
                        }
                    }
                }
            }
            else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.REPI)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.RCommission)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.RWarranty))
            {
                gvServiceCharges.Columns[8].Visible = false;
                gvServiceCharges.Columns[9].Visible = false;
                gvServiceCharges.Columns[10].Visible = false;
                gvServiceCharges.Columns[11].Visible = false;

                btnGenerateQuotation.Visible = false;
                btnGenerateProfarmaInvoice.Visible = false;
                btnGenerateInvoice.Visible = false;
                btnRequestForClaim.Visible = false;
            }
            else
            {
                gvServiceCharges.Columns[9].Visible = false;
                gvServiceCharges.Columns[10].Visible = false;
                gvServiceCharges.Columns[11].Visible = false;

                btnGenerateQuotation.Visible = false;
                btnGenerateProfarmaInvoice.Visible = false;
                btnGenerateInvoice.Visible = false;
                btnRequestForClaim.Visible = true;
                if (!string.IsNullOrEmpty(ClaimNumber))
                {
                    gcServiceCharges.Visible = false;
                    gvServiceCharges.FooterRow.Visible = false;
                    btnRequestForClaim.Visible = false;
                }
            }
        }

        protected void lblServiceAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            TextBox txtServiceMaterial = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceMaterial");
            TextBox txtServiceDate = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceDate");
            TextBox txtDiscount = (TextBox)gvServiceCharges.FooterRow.FindControl("txtDiscount");
            TextBox txtTaxP = (TextBox)gvServiceCharges.HeaderRow.FindControl("txtTaxP");
            TaxP = txtTaxP.Text.Trim();
            if (string.IsNullOrEmpty(txtServiceMaterial.Text.Trim()))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }
            if (string.IsNullOrEmpty(txtServiceDate.Text.Trim()))
            {
                lblMessage.Text = "Please enter the service date";
                return;
            }

            string ServiceMaterial = "";

            PDMS_Material MaterialsDescription = new BDMS_Material().GetMaterialServiceByMaterialAndDescription(txtServiceMaterial.Text.Trim());

            //if (txtServiceMaterial.Text.Contains("SA"))
            //{
            //    ServiceMaterial = txtServiceMaterial.Text.Split(' ')[0];
            //}
            //else
            //{
            //    ServiceMaterial = txtServiceMaterial.Text.Split(' ')[0] + " " + txtServiceMaterial.Text.Split(' ')[1];
            //}

            ServiceMaterial = MaterialsDescription.MaterialCode;
            Boolean IsMainServiceMaterial = (Boolean)HttpContext.Current.Session["IsMainServiceMaterial"];


            List<string> Materials = new BDMS_Material().GetMaterialServiceAutocomplete(ServiceMaterial, "", SDMS_ICTicket.ServiceType.ServiceTypeID, null, IsMainServiceMaterial);
            if (Materials.Count() != 1)
            {
                lblMessage.Text = "You can not use this Material " + ServiceMaterial;
                return;
            }

            int ServiceTypeID = (int)HttpContext.Current.Session["ServiceTypeID"];
            if ((ServiceTypeID == (short)DMS_ServiceType.NEPI) || (ServiceTypeID == (short)DMS_ServiceType.Commission) || (ServiceTypeID == (short)DMS_ServiceType.PreCommission))
            {
                int Status = new BDMS_ICTicket().ValidateNEPI(ServiceMaterial, SDMS_ICTicket.Equipment.EquipmentSerialNo);
                if (Status != 0)
                {
                    if (ServiceTypeID == (short)DMS_ServiceType.NEPI)
                    {
                        lblMessage.Text = "This NEPI service already completed. You cannot use this code.";
                    }
                    else if (ServiceTypeID == (short)DMS_ServiceType.Commission)
                    {
                        lblMessage.Text = "This Commission service already completed. You cannot use this code.";
                    }
                    else
                    {
                        lblMessage.Text = "This Pre Commission service already completed. You cannot use this code.";
                    }
                    return;
                }
            }

            // if (IsMainServiceMaterial == true)
            // {
            for (int i = 0; i < gvServiceCharges.Rows.Count; i++)
            {
                Label lblMaterialCode = (Label)gvServiceCharges.Rows[i].FindControl("lblMaterialCode");
                if (lblMaterialCode.Text == ServiceMaterial)
                {
                    lblMessage.Text = "Material " + ServiceMaterial + " already available";
                    return;
                }
            }
            // }
            decimal WorkedHours = 0, BasePrice = 0, Discount = 0;

            if (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
                )
            {
                TextBox txtWorkedHours = (TextBox)gvServiceCharges.FooterRow.FindControl("txtWorkedHours");
                TextBox txtBasePrice = (TextBox)gvServiceCharges.FooterRow.FindControl("txtBasePrice");

                //if ((!string.IsNullOrEmpty(txtTaxP.Text.Trim())) && (string.IsNullOrEmpty(txtDiscount.Text.Trim())))
                if (!string.IsNullOrEmpty(txtTaxP.Text.Trim()))
                {
                    txtDiscount.Text = Convert.ToString(Convert.ToDecimal(txtBasePrice.Text.Trim()) * Convert.ToDecimal(txtTaxP.Text.Trim()) / 100);
                }

                if (string.IsNullOrEmpty(txtWorkedHours.Text.Trim()))
                {
                    lblMessage.Text = "Please enter the worked hours";
                    return;
                }

                decimal value;
                if (!decimal.TryParse(txtWorkedHours.Text, out value))
                {
                    lblMessage.Text = "Please enter correct format in worked hours";
                    return;
                }

                if (string.IsNullOrEmpty(txtBasePrice.Text.Trim()))
                {
                    lblMessage.Text = "Please enter the base price";
                    return;
                }
                if (!decimal.TryParse(txtBasePrice.Text, out value))
                {
                    lblMessage.Text = "Please enter correct format in base price";
                    return;
                }
                if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                {
                    lblMessage.Text = "Please enter the discount";
                    return;
                }
                if (!decimal.TryParse(txtDiscount.Text, out value))
                {
                    lblMessage.Text = "Please enter correct format in discount";
                    return;
                }
                WorkedHours = Convert.ToDecimal(txtWorkedHours.Text.Trim());
                BasePrice = Convert.ToDecimal(txtBasePrice.Text.Trim());
                Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
            }

            PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Dealer.DealerCode);
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);
            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            {
                if (SDMS_ICTicket.Dealer.IsEInvoice)
                {
                    string Message = new BDMS_Customer().CustomerValidation(Customer);
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                }
            }
            if ((new System.Text.RegularExpressions.Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")).Match(Customer.GSTIN).Success)
            {
                Customer.StateCode = Customer.GSTIN.Substring(0, 2);
            }

            Boolean IsIGST = true;
            if (Dealer.StateCode == Customer.StateCode)
                IsIGST = false;

            if (new BDMS_ICTicket().InsertOrUpdateMaterialServiceAddOrRemoveICTicket(0, "", SDMS_ICTicket.ICTicketID, ServiceMaterial, Convert.ToDateTime(txtServiceDate.Text), WorkedHours, BasePrice, Discount, false, IsIGST, PSession.User.UserID))
            {
                lblMessage.Text = "Material " + ServiceMaterial + " is added";
                lblMessage.ForeColor = Color.Green;
                SS_ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
                RefreshList.Add((short)RefreshEnum.ServiceChargesAddOrRemove);
            }
            else
            {
                lblMessage.Text = "Material " + ServiceMaterial + " is not added";
            }
            FillServiceCharges();
        }
        protected void lblServiceRemove_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;

            // lbFocus.Focus();
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long ServiceChargeID = Convert.ToInt64(gvServiceCharges.DataKeys[gvRow.RowIndex].Value);

            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others))
            //{
            //    Label lblQuotationNumber = (Label)gvServiceCharges.Rows[gvRow.RowIndex].FindControl("lblQuotationNumber");
            //    Label lblProformaInvoiceNumber = (Label)gvServiceCharges.Rows[gvRow.RowIndex].FindControl("lblProformaInvoiceNumber");
            //    Label lblInvoiceNumber = (Label)gvServiceCharges.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            //    if (!string.IsNullOrEmpty(lblInvoiceNumber.Text))
            //    {
            //        lblMessage.Text = "You are already raised Invoice. First cancel the Invoice, Proforma Invoice   and Quotation then Remove the service material.";
            //        return;
            //    }
            //    else if (!string.IsNullOrEmpty(lblProformaInvoiceNumber.Text))
            //    {
            //        lblMessage.Text = "You are already raised Proforma Invoice. First cancel the Proforma Invoice   and Quotation then Remove the service material.";
            //        return;
            //    }
            //    else if (!string.IsNullOrEmpty(lblQuotationNumber.Text))
            //    {
            //        lblMessage.Text = "You are already raised Quotation. First cancel the Quotation then Remove the service material.";
            //        return;
            //    }
            //}
            //else
            //{
            //    Label lblClaimNumber = (Label)gvServiceCharges.Rows[gvRow.RowIndex].FindControl("lblClaimNumber");
            //    if (!string.IsNullOrEmpty(lblClaimNumber.Text))
            //    {
            //        lblMessage.Text = "You are already requested for claim. First cancel the claim then Remove the service material.";
            //        return;
            //    }
            //}

            Label lblMaterialCode = (Label)gvServiceCharges.Rows[gvRow.RowIndex].FindControl("lblMaterialCode");

            PDMS_Material Material = new BDMS_Material().GetMaterialListSQL(null, lblMaterialCode.Text)[0];
            if (Material.IsMainServiceMaterial)
            {
                lblMessage.Text = "You cannot remove main service material (" + lblMaterialCode.Text + ")";
                return;
            }
            //if (gvRow.RowIndex == 0)
            //{
            //    if (gvServiceCharges.Rows.Count != 1)
            //    {                 
            //        lblMessage.Text = "You cannot remove main service material (" + lblMaterialCode.Text + ") without removing sub service material";
            //        return;
            //    }
            //}

            foreach (PDMS_ICTicketTSIR Sm in ICTicketTSIRs)
            {

                if ((Sm.ServiceCharge.Material.MaterialCode == lblMaterialCode.Text))
                {
                    if ((Sm.Status.StatusID == (short)TSIRStatus.Rejected) || (Sm.Status.StatusID == (short)TSIRStatus.Canceled))
                    {
                        break;
                    }
                    else
                    {
                        lblMessage.Text = "TSIR Already created – SRO code cannot be deleted.  To delete the SRO – get the respective TSIR rejected.";
                        return;
                    }
                }
            }

            if (new BDMS_ICTicket().InsertOrUpdateMaterialServiceAddOrRemoveICTicket(ServiceChargeID, "", SDMS_ICTicket.ICTicketID, "", null, 0, 0, 0, true, false, PSession.User.UserID))
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "service material removed";
                FillServiceCharges();
                SS_ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
                RefreshList.Add((short)RefreshEnum.ServiceChargesAddOrRemove);
            }
            else
            {
                lblMessage.Text = "service material is not removed";
            }
        }

        protected void btnGenerateQuotation_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            //   lbFocus.Focus();
            List<PDMS_PaidServiceInvoice> SOIs = new BDMS_Service().GetPaidServiceQuotation(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
            if (SOIs.Count == 1)
            {
                lblMessage.Text = "Quotation (" + SOIs[0].InvoiceNumber + ") already created";
                lblMessage.ForeColor = Color.Green;
                return;
            }
            PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Dealer.DealerCode);
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);
            Boolean IsIGST = true;
            if (Dealer.StateCode == Customer.StateCode)
                IsIGST = false;
            if (new BDMS_Service().InsertServiceQuotationOrProformaOrInvoice(SDMS_ICTicket, IsIGST, PSession.User.UserID, 1, Dealer, Customer))
            {
                SOIs = new BDMS_Service().GetPaidServiceQuotation(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
                lblMessage.Text = "Quotation (" + SOIs[0].InvoiceNumber + ") is created successfully";
                lblMessage.ForeColor = Color.Green;
                FillServiceCharges();
            }
            else
            {
                lblMessage.Text = "Quotation is not created successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnGenerateProfarmaInvoice_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            //  lbFocus.Focus();
            PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Dealer.DealerCode);
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);
            Boolean IsIGST = true;

            if (Dealer.StateCode == Customer.StateCode)
                IsIGST = false;


            List<PDMS_PaidServiceInvoice> Proformas = new BDMS_Service().GetPaidServiceProformaInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
            if (Proformas.Count == 1)
            {
                lblMessage.Text = "Proforma Invoice (" + Proformas[0].InvoiceNumber + ") already created";
                lblMessage.ForeColor = Color.Green;
                return;
            }

            if (new BDMS_Service().InsertServiceQuotationOrProformaOrInvoice(SDMS_ICTicket, IsIGST, PSession.User.UserID, 2, Dealer, Customer))
            {
                Proformas = new BDMS_Service().GetPaidServiceProformaInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
                lblMessage.Text = "Proforma Invoice (" + Proformas[0].ProformaInvoiceNumber + ") is created successfully";
                lblMessage.ForeColor = Color.Green;
                FillServiceCharges();
            }
            else
            {
                lblMessage.Text = "Proforma Invoice is not created successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            //  lbFocus.Focus();
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;

            PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Dealer.DealerCode);
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);
            string Message = new BDMS_Customer().CustomerValidation(Customer);
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            if ((new System.Text.RegularExpressions.Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")).Match(Customer.GSTIN).Success)
            {
                Customer.StateCode = Customer.GSTIN.Substring(0, 2);
            }

            Boolean IsIGST = true;
            if (Dealer.StateCode == Customer.StateCode)
                IsIGST = false;

            List<PDMS_PaidServiceInvoice> Invoice = new BDMS_Service().GetPaidServiceInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "", true);
            if (Invoice.Count == 1)
            {
                lblMessage.Text = "Invoice (" + Invoice[0].InvoiceNumber + ") already created";
                lblMessage.ForeColor = Color.Green;
                return;
            }

            if (new BDMS_Service().InsertServiceQuotationOrProformaOrInvoice(SDMS_ICTicket, IsIGST, PSession.User.UserID, 3, Dealer, Customer))
            {
                Invoice = new BDMS_Service().GetPaidServiceInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "", true);
                lblMessage.Text = "Invoice (" + Invoice[0].InvoiceNumber + ") is created successfully";

                if ((SDMS_ICTicket.Dealer.IsEInvoice) && (SDMS_ICTicket.Dealer.EInvoiceDate <= Invoice[0].InvoiceDate))
                {
                    new BDMS_EInvoice().GeneratEInvoice(Invoice[0].InvoiceNumber);
                }
                lblMessage.ForeColor = Color.Green;
                FillServiceCharges();
            }
            else
            {
                lblMessage.Text = "Invoice is not created successfully";
            }
        }
        protected void btnRequestForClaim_Click(object sender, EventArgs e)
        {
            //  lbFocus.Focus();



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

            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty)
                || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty)
                )
            {
                List<PDMS_ICTicketTSIR> TSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
                if (TSIRs.Count == 0)
                {
                    lblMessage.Text = "Without TSIR you cannot request Claim";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                foreach (PDMS_ServiceCharge SS in SS_ServiceCharge)
                {
                    if (SS.Material.IsMainServiceMaterial == true)
                    {
                        continue;
                    }
                    if (SS.Material.MaterialGroup == "891")
                    {
                        continue;
                    }
                    if (SS.TSIR == null)
                    {
                        lblMessage.Text = "Without TSIR you cannot request Claim";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    if ((SS.TSIR.Status.StatusID == (short)TSIRStatus.Approved) || (SS.TSIR.Status.StatusID == (short)TSIRStatus.Rejected) || (SS.TSIR.Status.StatusID == (short)TSIRStatus.SalesApproved) || (SS.TSIR.Status.StatusID == (short)TSIRStatus.SalesRejected))
                    {

                    }
                    else
                    {
                        lblMessage.Text = "TSIR approval is not completed so that you cannot request Claim";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            Decimal amount = 0;
            foreach (PDMS_ServiceCharge CH in SS_ServiceCharge)
            {
                amount = amount + CH.BasePrice;
            }
            if (amount == 0)
            {
                lblMessage.Text = "Please check the claim amount.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            if (new BDMS_ICTicket().InsertClaimForService(SDMS_ICTicket.ICTicketID, PSession.User.UserID))
            {
                List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);

                lblMessage.Text = "Claim is Requested";
                lblMessage.ForeColor = Color.Green;
                if (Charge.Count != 0)
                {

                    lblMessage.Text = "Claim ( " + Charge[0].ClaimNumber + " ) is Requested";
                }
            }
            else
            {
                lblMessage.Text = "Claim is not Requested";
                lblMessage.ForeColor = Color.Red;
            }
            FillServiceCharges();
            //lbFocus.Focus();
        }

        protected void gvServiceCharges_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    TextBox txtTaxP = (TextBox)e.Row.FindControl("txtTaxP");
                    txtTaxP.Text = TaxP;
                }

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    CalendarExtender ceServiceDate = (CalendarExtender)e.Row.FindControl("ceServiceDate");
                    ceServiceDate.StartDate = SDMS_ICTicket.ReachedDate;
                    if (SDMS_ICTicket.RestoreDate != null)
                    {
                        ceServiceDate.EndDate = SDMS_ICTicket.RestoreDate;
                    }

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_MTTR_Report", "fillMTTR", ex);
                throw ex;
            }
        }
    }
}