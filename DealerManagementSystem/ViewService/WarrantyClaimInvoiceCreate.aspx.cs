using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimInvoiceCreate : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyClaimInvoiceCreate; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimInvoiceCreate.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        public PDMS_WarrantyClaimAnnexureHeader SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimInvoiceCreate"] == null)
                {
                    Session["DMS_WarrantyClaimInvoiceCreate"] = new PDMS_WarrantyClaimAnnexureHeader();
                }
                return (PDMS_WarrantyClaimAnnexureHeader)Session["DMS_WarrantyClaimInvoiceCreate"];
            }
            set
            {
                Session["DMS_WarrantyClaimInvoiceCreate"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Final Invoice Create');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                FillYear();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                //   FillInvoiceType();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["DealerCode"] = ddlDealerCode.SelectedValue;
                ViewState["Year"] = ddlYear.SelectedValue;
                ViewState["Month"] = ddlMonth.SelectedValue;
                ViewState["MonthRange"] = ddlMonthRange.SelectedValue;
                ViewState["InvoiceTypeID"] = ddlInvoiceTypeID.SelectedValue;


                string DealerCode = (string)ViewState["DealerCode"];
                string Year = (string)ViewState["Year"];
                string Month = (string)ViewState["Month"];
                string MonthRange = (string)ViewState["MonthRange"];
                int InvoiceTypeID = Convert.ToInt32(ViewState["InvoiceTypeID"]);

                if (Convert.ToInt16(ddlInvoiceTypeID.SelectedValue) == (short)DMS_InvoiceType.NEPI_Commission)
                    fillNEPI_Commission(DealerCode, Year, Month, MonthRange, InvoiceTypeID);
                else if (Convert.ToInt16(ddlInvoiceTypeID.SelectedValue) == (short)DMS_InvoiceType.Warranty_Service)
                    fillWarranty_Service(DealerCode, Year, Month, MonthRange, InvoiceTypeID);
                else if (Convert.ToInt16(ddlInvoiceTypeID.SelectedValue) == (short)DMS_InvoiceType.Warranty_ServicePartial)
                    fillWarranty_Service(DealerCode, Year, Month, MonthRange, InvoiceTypeID);
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillNEPI_Commission(string DealerCode, string Year, string Month, string MonthRange, int InvoiceTypeID)
        {
            try
            {
                gvICTickets.DataSource = null;
                gvICTickets.DataBind();
                gvWS.DataSource = null;
                gvWS.DataBind();

                TraceLogger.Log(DateTime.Now);

                List<PDMS_WarrantyClaimInvoice> ClaimInvoice = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceByMonthAndMonthRange(DealerCode, Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(MonthRange), Convert.ToInt32(InvoiceTypeID));
                btnGenerateInvoice.Visible = false;

                if (ClaimInvoice.Count != 0)
                {
                    lblInvNo.Text = "Invoice Number : " + ClaimInvoice[0].InvoiceNumber;
                    lblInvNo.ForeColor = Color.FromArgb(145, 1, 126);
                    return;
                }

                List<PDMS_WarrantyClaimAnnexureHeader> SOIs = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, DealerCode, Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(MonthRange), Convert.ToInt32(InvoiceTypeID), "", null);
                if (SOIs.Count == 0)
                {
                    lblMessage.Text = "Please generate the annexure for " + ddlYear.SelectedValue + " " + ddlMonth.SelectedItem.Value + " " + "W" + ddlMonthRange.SelectedValue;
                    lblMessage.ForeColor = Color.FromArgb(145, 1, 126);
                    lblMessage.Visible = true;
                    return;
                }

                btnGenerateInvoice.Visible = true;
                lblInvNo.Text = "";

                PDMS_WarrantyClaimAnnexureHeader AnnexureH = new PDMS_WarrantyClaimAnnexureHeader();
                AnnexureH = SOIs[0];
                SDMS_WarrantyClaimHeader = AnnexureH;

                decimal SumOfCommission = 0, SumOfNEPI = 0;

                foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                {
                    if (item.Category == "Commission")
                        SumOfCommission = SumOfCommission + (decimal)item.ApprovedAmount;
                    if (item.Category == "NEPI")
                        SumOfNEPI = SumOfNEPI + (decimal)item.ApprovedAmount;
                }

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SL. No");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN Code");
                CommissionDT.Columns.Add("Approved Value");
                CommissionDT.Columns.Add("Discount");
                CommissionDT.Columns.Add("Taxable");

                CommissionDT.Columns.Add("CGST %");
                CommissionDT.Columns.Add("SGST %");
                CommissionDT.Columns.Add("CGST Value");
                CommissionDT.Columns.Add("SGST Value");

                CommissionDT.Columns.Add("IGST %");
                CommissionDT.Columns.Add("IGST Value");

                PDealer DealerOffice = new BDealer().GetDealerList(null, ddlDealerCode.SelectedValue, "")[0];
                string StateCode = DealerOffice.StateCode;
                //foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                //{
                if (StateCode == "29")
                {
                    CommissionDT.Rows.Add(1, "SER 00002", "Commissioning", "998719", SumOfCommission, "0.00", SumOfCommission, "9", "9", SumOfCommission * 9 / 100, SumOfCommission * 9 / 100, "0", "0");
                    CommissionDT.Rows.Add(2, "NEPI", "NEPI", "998719", SumOfNEPI, "0.00", SumOfNEPI, "9", "9", SumOfNEPI * 9 / 100, SumOfNEPI * 9 / 100, "0", "0");
                    //gvICTickets.Columns[7].Visible = true;
                    //gvICTickets.Columns[8].Visible = true;
                    //gvICTickets.Columns[9].Visible = true;
                    //gvICTickets.Columns[10].Visible = true;
                    //gvICTickets.Columns[11].Visible = false;
                    //gvICTickets.Columns[12].Visible = false;

                }
                else
                {

                    CommissionDT.Rows.Add(1, "SER 00002", "Commissioning", "998719", SumOfCommission, "0.00", SumOfCommission, "0", "0", "0", "0", "18", SumOfCommission * 18 / 100);
                    CommissionDT.Rows.Add(2, "NEPI", "NEPI", "998719", SumOfNEPI, "0.00", SumOfNEPI, "0", "0", "0", "0", "18", SumOfNEPI * 18 / 100);

                    //gvICTickets.Columns[7].Visible = false;
                    //gvICTickets.Columns[8].Visible = false;
                    //gvICTickets.Columns[9].Visible = false;
                    //gvICTickets.Columns[10].Visible = false;
                    //gvICTickets.Columns[11].Visible = true;
                    //gvICTickets.Columns[12].Visible = true;

                }
                //  }
                gvICTickets.DataSource = CommissionDT;
                gvICTickets.DataBind();

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ClaimConsolidationAnnexure", "fillClaim", e1);
                throw e1;
            }
        }

        void fillWarranty_Service(string DealerCode, string Year, string Month, string MonthRange, int InvoiceTypeID)
        {
            try
            {
                gvICTickets.DataSource = null;
                gvICTickets.DataBind();
                gvWS.DataSource = null;
                gvWS.DataBind();

                TraceLogger.Log(DateTime.Now);
                List<PDMS_WarrantyClaimInvoice> ClaimInvoice = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceByMonthAndMonthRange(DealerCode, Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(MonthRange), Convert.ToInt32(InvoiceTypeID));
                btnGenerateInvoice.Visible = false;

                if (ClaimInvoice.Count != 0)
                {
                    lblInvNo.Text = "Invoice Number : " + ClaimInvoice[0].InvoiceNumber;
                    lblInvNo.ForeColor = Color.FromArgb(145, 1, 126);
                    return;
                }

                List<PDMS_WarrantyClaimAnnexureHeader> SOIs = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, DealerCode, Convert.ToInt32(Year), Convert.ToInt32(Month), Convert.ToInt32(MonthRange), Convert.ToInt32(InvoiceTypeID), "", null);
                if (SOIs.Count == 0)
                {
                    lblMessage.Text = "Please generate the annexure for " + ddlYear.SelectedValue + " " + ddlMonth.SelectedItem.Value + " " + "W" + ddlMonthRange.SelectedValue;
                    lblMessage.ForeColor = Color.FromArgb(145, 1, 126);
                    lblMessage.Visible = true;
                    return;
                }

                lblInvNo.Text = "";

                PDMS_WarrantyClaimAnnexureHeader AnnexureH = new PDMS_WarrantyClaimAnnexureHeader();
                AnnexureH = SOIs[0];
                SDMS_WarrantyClaimHeader = AnnexureH;

                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SLNo");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("Description");
                CommissionDT.Columns.Add("HSN");
                //CommissionDT.Columns.Add("UOM");
                CommissionDT.Columns.Add("Qty");
                CommissionDT.Columns.Add("Rate");
                CommissionDT.Columns.Add("Value");
                CommissionDT.Columns.Add("Discount");
                CommissionDT.Columns.Add("TaxableValue");
                CommissionDT.Columns.Add("CGST %");
                CommissionDT.Columns.Add("SGST %");
                CommissionDT.Columns.Add("CGST Value");
                CommissionDT.Columns.Add("SGST Value");

                CommissionDT.Columns.Add("IGST %");
                CommissionDT.Columns.Add("IGST Value");
                CommissionDT.Columns.Add("Amount");

                PDealer DealerOffice = new BDealer().GetDealerList(null, ddlDealerCode.SelectedValue, "")[0];
                string StateCode = DealerOffice.StateCode;
                int i = 0;
                decimal Rate, Value, Amount;
                decimal GSTValue = 0;
                foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                {
                    if ((item.TaxPercentage == null) || (item.TaxPercentage == 0))
                    {
                        btnGenerateInvoice.Visible = false;
                        lblMessage.Text = "Tax is not maintained in material ‘" + item.Material + "’.Please contact Admin ";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    i = i + 1;
                    Rate = decimal.Round(((decimal)item.ApprovedAmount) / (int)item.Qty, 2, MidpointRounding.AwayFromZero);
                    Value = decimal.Round(item.ApprovedAmount, 2, MidpointRounding.AwayFromZero);
                    if (StateCode == "29")
                    {
                        GSTValue = decimal.Round(item.ApprovedAmount * (decimal)item.TaxPercentage / 100, 2, MidpointRounding.AwayFromZero);

                        //  Amount = Rate + Value + GSTValue + GSTValue;
                        Amount = Value + GSTValue + GSTValue;
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, Rate, Value, 0, Value, (decimal)item.TaxPercentage, (decimal)item.TaxPercentage, GSTValue, GSTValue, "0", "0", Amount);
                        //gvICTickets.Columns[7].Visible = true;
                        //gvICTickets.Columns[8].Visible = true;
                        //gvICTickets.Columns[9].Visible = true;
                        //gvICTickets.Columns[10].Visible = true;

                        //gvICTickets.Columns[11].Visible = false;
                        //gvICTickets.Columns[12].Visible = false;

                    }
                    else
                    {
                        GSTValue = decimal.Round(item.ApprovedAmount * ((decimal)item.TaxPercentage * 2) / 100, 2, MidpointRounding.AwayFromZero);
                        //   Amount = Rate + Value + GSTValue + GSTValue;
                        Amount = Value + GSTValue;
                        CommissionDT.Rows.Add(i, item.Material, item.MaterialDesc, item.HSNCode, item.Qty, Rate, Value, 0, Value, "0", "0", "0", "0", ((decimal)item.TaxPercentage * 2), GSTValue, Amount);
                        //gvICTickets.Columns[7].Visible = false;
                        //gvICTickets.Columns[8].Visible = false;
                        //gvICTickets.Columns[9].Visible = false;
                        //gvICTickets.Columns[10].Visible = false;

                        //gvICTickets.Columns[11].Visible = true;
                        //gvICTickets.Columns[12].Visible = true;
                    }

                }
                gvWS.DataSource = CommissionDT;
                gvWS.DataBind();

                btnGenerateInvoice.Visible = true;


                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceCreate", "fillWarranty_Service", e1);
                throw e1;
            }
        }


        void FillYear()
        {

            for (int Year = 2015; Year <= DateTime.Now.Year; Year++)
            {
                ddlYear.Items.Add(new ListItem(Year.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        {
            string DealerCode = (string)ViewState["DealerCode"];
            string Year = (string)ViewState["Year"];
            string Month = (string)ViewState["Month"];
            string MonthRange = (string)ViewState["MonthRange"];
            int InvoiceTypeID = Convert.ToInt32(ViewState["InvoiceTypeID"]);


            PDMS_WarrantyClaimInvoice Inv = new PDMS_WarrantyClaimInvoice();
            Inv.Dealer = new PDMS_Dealer();
            Inv.Dealer.DealerCode = DealerCode;
            Inv.Year = Convert.ToInt32(Year);
            Inv.Month = Convert.ToInt32(Month);
            long WarrantyClaimInvoiceID = 0;

            PDMS_Customer Dealer = new SCustomer().getCustomerAddress(Inv.Dealer.DealerCode);
            PDMS_Customer CustomerAE = new BDMS_Customer().GetCustomerAE();


            if (InvoiceTypeID == (short)DMS_InvoiceType.NEPI_Commission)
            {
                WarrantyClaimInvoiceID = new BDMS_WarrantyClaimInvoice().InsertWarrantyClaimInvoiceNEPI_Commission(Inv, Convert.ToInt32(MonthRange), PSession.User.UserID, Dealer, CustomerAE);
            }
            else if (InvoiceTypeID == (short)DMS_InvoiceType.Warranty_Service)
            {
                WarrantyClaimInvoiceID = new BDMS_WarrantyClaimInvoice().InsertWarrantyClaimInvoiceWarranty_Service(Inv, Convert.ToInt32(MonthRange), PSession.User.UserID, Dealer, CustomerAE);
            }
            else if (InvoiceTypeID == (short)DMS_InvoiceType.Warranty_ServicePartial)
            {
                WarrantyClaimInvoiceID = new BDMS_WarrantyClaimInvoice().InsertWarrantyClaimInvoiceWarranty_ServicePartial(Inv, Convert.ToInt32(MonthRange), PSession.User.UserID, Dealer, CustomerAE);
            }
            lblMessage.Text = "Invoice not  generated successfully";
            lblMessage.ForeColor = Color.Red;
            if (WarrantyClaimInvoiceID != 0)
            {
                PDMS_WarrantyClaimInvoice ClaimInvoice = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(WarrantyClaimInvoiceID, "", null, null, null, null, "")[0];
                //if (InvoiceTypeID == (short)DMS_InvoiceType.NEPI_Commission)
                //{
                //  new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvNEPI_CommissionFile(ClaimInvoice));
                //}
                //else if (InvoiceTypeID == (short)DMS_InvoiceType.Warranty_Service)
                //{
                //    new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(WarrantyClaimInvoiceID, InvWarranty_Service(WarrantyClaimInvoiceID));
                //}
                lblMessage.Text = "Invoice number -" + ClaimInvoice.InvoiceNumber + " generated successfully";
                if ((ClaimInvoice.Dealer.IsEInvoice) && (ClaimInvoice.Dealer.EInvoiceDate <= ClaimInvoice.InvoiceDate))
                {
                    //  john new BDMS_EInvoice().GeneratEInvoice(ClaimInvoice.InvoiceNumber);
                }
                lblMessage.ForeColor = Color.Green;
                btnGenerateInvoice.Visible = false;
            }
            lblMessage.Visible = true;
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
        }
    }
}