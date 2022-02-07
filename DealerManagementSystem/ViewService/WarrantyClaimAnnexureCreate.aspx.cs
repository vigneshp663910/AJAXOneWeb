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
    public partial class WarrantyClaimAnnexureCreate : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimAnnexureCreate.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public PDMS_WarrantyClaimAnnexureHeader SDMS_ClaimAnnexure
        {
            get
            {
                if (Session["DMS_WarrantyClaimAnnexureCreate"] == null)
                {
                    Session["DMS_WarrantyClaimAnnexureCreate"] = new PDMS_WarrantyClaimAnnexureHeader();
                }
                return (PDMS_WarrantyClaimAnnexureHeader)Session["DMS_WarrantyClaimAnnexureCreate"];
            }
            set
            {
                Session["DMS_WarrantyClaimAnnexureCreate"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            lblAnnexureNumber.Text = "";
            btnGenerate.Visible = true;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                FillYear();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                // FillInvoiceType();
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

                lblMessage.Visible = true;
                lblMessage.Text = "No record approved in " + ddlYear.SelectedValue + " " + ddlMonth.SelectedItem.Value + " " + "W" + ddlMonthRange.SelectedValue;
                lblMessage.ForeColor = Color.Orange;
                if (DateTime.Now.Year == Convert.ToInt32(ddlYear.SelectedValue))
                {
                    if (DateTime.Now.Month == Convert.ToInt32(ddlMonth.SelectedValue))
                    {
                        if (DateTime.Now.Day <= 7)
                        {
                            lblMessage.Text = "No record approved in " + ddlYear.SelectedValue + " " + ddlMonth.SelectedItem.Value + " " + "W" + ddlMonthRange.SelectedValue;
                            return;
                        }
                        else if ((DateTime.Now.Day <= 15) && (Convert.ToInt32(ddlMonthRange.SelectedValue) >= 2))
                        {
                            return;
                        }
                        else if ((DateTime.Now.Day <= 23) && (Convert.ToInt32(ddlMonthRange.SelectedValue) >= 3))
                        {
                            return;
                        }
                        else if (ddlMonthRange.SelectedValue == "4")
                        {
                            return;
                        }
                    }
                    else if (DateTime.Now.Month < Convert.ToInt32(ddlMonth.SelectedValue))
                    {
                        return;
                    }
                }
                else if (DateTime.Now.Year < Convert.ToInt32(ddlYear.SelectedValue))
                {
                    return;
                }
                lblMessage.Text = "";
                fillAnnexure();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillAnnexure()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                ViewState["DealerCode"] = ddlDealerCode.SelectedValue;
                ViewState["Year"] = ddlYear.SelectedValue;
                ViewState["Month"] = ddlMonth.SelectedValue;
                ViewState["MonthRange"] = ddlMonthRange.SelectedValue;

                ViewState["InvoiceTypeID"] = ddlInvoiceTypeID.SelectedValue;
                ViewState["DeliveryChallan"] = txtDeliveryChallan.Text.Trim();



                List<PDMS_WarrantyClaimAnnexureHeader> SOIs = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, ddlDealerCode.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlMonthRange.SelectedValue), Convert.ToInt32(ddlInvoiceTypeID.SelectedValue), "", null);

                if (SOIs.Count != 0)
                {
                    SDMS_ClaimAnnexure = SOIs[0];
                    gvICTickets.DataSource = SDMS_ClaimAnnexure.AnnexureItems;
                    gvICTickets.DataBind();

                    btnGenerate.Visible = false;
                    lblAnnexureNumber.Text = "Annexure already created Annexure Number is “ " + SOIs[0].AnnexureNumber + " ”";
                    lblAnnexureNumber.ForeColor = Color.FromArgb(145, 1, 126);
                }
                else
                {
                    PDMS_WarrantyClaimAnnexureHeader SOI = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureToGenerate(ddlDealerCode.SelectedValue, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlMonthRange.SelectedValue), Convert.ToInt32(ddlInvoiceTypeID.SelectedValue), "");

                    SDMS_ClaimAnnexure = SOI;
                    gvICTickets.DataSource = SOI.AnnexureItems;
                    gvICTickets.DataBind();
                    if (SOI.AnnexureItems.Count == 0)
                    {
                        // lblMessage.Text = "Zero record approved in this week";
                        lblMessage.Text = "No record approved in " + ddlYear.SelectedValue + " " + ddlMonth.SelectedItem.Value + " " + "W" + ddlMonthRange.SelectedValue;
                        lblMessage.ForeColor = Color.FromArgb(145, 1, 126);
                        btnGenerate.Visible = false;
                        return;
                    }

                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimAnnexureCreate", "fillAnnexure", e1);
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

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {

            DataTable CommissionDT = new DataTable();
            CommissionDT.Columns.Add("Sl. No");
            CommissionDT.Columns.Add("Dealer");
            CommissionDT.Columns.Add("Dealer Name");
            CommissionDT.Columns.Add("IC Ticket ID");
            CommissionDT.Columns.Add("IC Ticket Date");
            CommissionDT.Columns.Add("Restore Date");
            CommissionDT.Columns.Add("Approved Date");
            CommissionDT.Columns.Add("Customer");
            CommissionDT.Columns.Add("Customer Name");
            CommissionDT.Columns.Add("Model");
            CommissionDT.Columns.Add("HMR");
            CommissionDT.Columns.Add("Machine Serial Number");

            CommissionDT.Columns.Add("SAC / HSN Code");
            CommissionDT.Columns.Add("Material");
            CommissionDT.Columns.Add("Amount", typeof(decimal));
            CommissionDT.Columns.Add("Approved Amount", typeof(decimal));

            int RowNo = 0;
            foreach (PDMS_WarrantyClaimAnnexureItem wc in SDMS_ClaimAnnexure.AnnexureItems)
            {

                RowNo = RowNo + 1;

                CommissionDT.Rows.Add(RowNo, SDMS_ClaimAnnexure.Dealer.DealerCode, SDMS_ClaimAnnexure.Dealer.DealerName, wc.ICTicketID, wc.ICTicketDate, ((DateTime)wc.RestoreDate).ToShortDateString()
                    , wc.ApprovedDate == null ? "" : ((DateTime)wc.ApprovedDate).ToShortDateString(), wc.CustomerCode
                    , wc.CustomerName, wc.Model, wc.HMR, wc.MachineSerialNumber, wc.HSNCode, wc.Material, wc.ClaimAmount, wc.ApprovedAmount);
            }

            new BXcel().ExporttoExcel(CommissionDT, "Annexure");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string DealerCode = (string)ViewState["DealerCode"];
                string Year = (string)ViewState["Year"];
                string Month = (string)ViewState["Month"];
                string MonthRange = (string)ViewState["MonthRange"];

                int InvoiceTypeID = Convert.ToInt32(ViewState["InvoiceTypeID"]);
                string DeliveryChallan = (string)ViewState["DeliveryChallan"];

                //PDealer DealerOffice = new BDealer().GetDealerList(null, DealerCode, "")[0];
                //string Fillter = "'" + DealerCode + "','" + DealerOffice.HeadOfficeID + "','WARR_CLAIM'";
                //PDMS_Dealer Dealer = new BDMS_Customer().GetCustomerAdress(Fillter)[0];



                string Annexure = "";
                if (ddlInvoiceTypeID.SelectedValue == "1")
                {
                    Annexure = DealerCode + Year.Substring(2, 2) + Month + "N" + MonthRange;
                }
                else if (ddlInvoiceTypeID.SelectedValue == "2")
                {
                    Annexure = DealerCode + Year.Substring(2, 2) + Month + "W" + MonthRange;
                }
                else if (ddlInvoiceTypeID.SelectedValue == "5")
                {
                    Annexure = DealerCode + Year.Substring(2, 2) + Month + "DW" + MonthRange;
                }
                if (MonthRange == "1")
                {
                    SDMS_ClaimAnnexure.PeriodFrom = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 1);
                    SDMS_ClaimAnnexure.PeriodTo = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 7);
                }
                else if (MonthRange == "2")
                {
                    SDMS_ClaimAnnexure.PeriodFrom = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 8);
                    SDMS_ClaimAnnexure.PeriodTo = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 15);
                }
                else if (MonthRange == "3")
                {
                    SDMS_ClaimAnnexure.PeriodFrom = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 16);
                    SDMS_ClaimAnnexure.PeriodTo = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 23);
                }
                else
                {
                    SDMS_ClaimAnnexure.PeriodFrom = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 23);
                    SDMS_ClaimAnnexure.PeriodTo = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(Month), 1).AddMonths(1);
                    SDMS_ClaimAnnexure.PeriodTo = SDMS_ClaimAnnexure.PeriodTo.AddDays(-1);
                }

                PDMS_Customer Dealer = new SCustomer().getCustomerAddress(DealerCode);
                SDMS_ClaimAnnexure.Address1 = Dealer.Address2 + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3) + (string.IsNullOrEmpty(Dealer.Address1) ? "" : "," + Dealer.Address1);
                SDMS_ClaimAnnexure.Address2 = Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode);
                SDMS_ClaimAnnexure.Address1 = SDMS_ClaimAnnexure.Address1.Trim(',', ' ');
                SDMS_ClaimAnnexure.Address2 = SDMS_ClaimAnnexure.Address2.Trim(',', ' ');

                // SDMS_ClaimAnnexure.Contact = Dealer.Mobile + "," + Dealer.Email;
                SDMS_ClaimAnnexure.GSTIN = Dealer.GSTIN;

                SDMS_ClaimAnnexure.AnnexureNumber = Annexure;
                SDMS_ClaimAnnexure.MonthRange = Convert.ToInt32(MonthRange);

                SDMS_ClaimAnnexure.Year = Convert.ToInt32(Year);
                SDMS_ClaimAnnexure.Month = Convert.ToInt32(Month);

                if (new BDMS_WarrantyClaimAnnexure().InsertWarrantyClaimAnnexureHeader(SDMS_ClaimAnnexure, InvoiceTypeID, DeliveryChallan, PSession.User.UserID))
                {
                    lblMessage.Text = "Annexure number -" + Annexure + " generated successfully";
                    lblMessage.ForeColor = Color.Green;
                    btnGenerate.Visible = false;
                }
                else
                {
                    lblMessage.Text = "Annexure is not generated successfully";
                    lblMessage.ForeColor = Color.Red;
                }

                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        //void FillInvoiceType()
        //{
        //    ddlInvoiceTypeID.DataTextField = "InvoiceType";
        //    ddlInvoiceTypeID.DataValueField = "InvoiceTypeID";
        //    ddlInvoiceTypeID.DataSource =  new BDMS_WarrantyClaimInvoice().GetWarrantyInvoiceType();
        //    ddlInvoiceTypeID.DataBind();
        //}
    }
}