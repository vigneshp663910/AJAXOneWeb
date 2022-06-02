using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using SapIntegration;

namespace DealerManagementSystem.ViewMarketing
{
    public partial class ActivityInvReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<PDealer> Dealer = new BDMS_Activity().GetDealerByUserID(PSession.User.UserID);
                ddlDealerSearch.DataTextField = "CodeWithName"; ddlDealerSearch.DataValueField = "DID"; ddlDealerSearch.DataSource = Dealer; ddlDealerSearch.DataBind();
                if (ddlDealerSearch.Items.Count > 1)
                {
                    ddlDealerSearch.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
                }
                else
                {
                    ddlDealerSearch.Enabled = false;

                }
                txtFromDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).ToString("dd-MMM-yyyy");
                txtToDateSearch.Text = DateTime.Now.AddDays(-1 * DateTime.Now.Day + 1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            DataTable dt = oActivity.GetInvoiceReportData_grid(Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID, Convert.ToInt32(ddlStatus.SelectedValue), ddlActivityType.SelectedValue);
            gvData.DataSource = dt;
            gvData.DataBind();
            foreach (GridViewRow gvRow in gvData.Rows)
            {

                ImageButton lnkEdit = gvRow.FindControl("lnkEdit") as ImageButton;
                HiddenField hdnInvID = gvRow.FindControl("hdnInvID") as HiddenField;
                HiddenField hdnInvNo = gvRow.FindControl("hdnInvNo") as HiddenField;
                HiddenField hdnTaxableAmount = gvRow.FindControl("hdnTaxableAmount") as HiddenField;
                Label lblSAPDoc = gvRow.FindControl("lblSAPDoc") as Label;
                Label lblAEAccDate = gvRow.FindControl("lblAEAccDate") as Label;
                Label lblPaymentVoucher = gvRow.FindControl("lblPaymentVoucher") as Label;
                Label lblPaymentDate = gvRow.FindControl("lblPaymentDate") as Label;
                Label lblPaymentValue = gvRow.FindControl("lblPaymentValue") as Label;
                Label lblTDS = gvRow.FindControl("lblTDS") as Label;
                DateTime? datPaymentDate;
                if (Convert.ToDouble("0" + lblPaymentValue.Text) == 0 || lblPaymentDate.Text == "")
                {

                    PSAPDocumentNumber SAP = new SSAPDocumentNumber().getSAPDocumentNumber(hdnInvNo.Value);
                    if (SAP.SAPDoc != null)
                    {
                        lblSAPDoc.Text = SAP.SAPDoc;
                        lblAEAccDate.Text = SAP.SAPPostingDate.ToString("dd-MMM-yyyy") == "01-Jan-0001" ? "" : SAP.SAPPostingDate.ToString("dd.MM.yyyy");
                        lblPaymentVoucher.Text = SAP.SAPClearingDocument;
                        datPaymentDate = SAP.SAPClearingDate;

                        if (datPaymentDate != null)
                        {
                            lblPaymentDate.Text = SAP.SAPClearingDate.Value.ToString("dd.MM.yyyy") == "01.01.1900" ? "" : SAP.SAPClearingDate.Value.ToString("dd.MM.yyyy");
                            datPaymentDate = SAP.SAPClearingDate.Value.ToString("dd.MM.yyyy") == "01.01.1900" ? DateTime.Parse("01-Jan-1900") : SAP.SAPClearingDate.Value;
                        }

                        lblPaymentValue.Text = SAP.SAPInvoiceValue == 0 ? "" : SAP.SAPInvoiceValue.ToString("#0.00");
                        double dblTDS = (double)(SAP.SAPInvoiceTDSValue == 0 ? 0 : SAP.SAPInvoiceTDSValue);
                        lblTDS.Text = dblTDS.ToString();
                        oActivity.UpdateInvoice_SapData(Convert.ToInt32(hdnInvID.Value), SAP.SAPDoc, SAP.SAPPostingDate.ToString("dd-MMM-yyyy"), SAP.SAPClearingDocument,
                            datPaymentDate, Convert.ToDouble(SAP.SAPInvoiceValue), dblTDS);
                    }
                }
                HiddenField hdnActualID = gvRow.FindControl("hdnActualID") as HiddenField;
                if (hdnInvID.Value != "0")
                {
                    lnkEdit.Attributes.Add("onclick", "window.open('ActivityInvoice.aspx?AID=" + oActivity.Encrypt(hdnActualID.Value) + "', 'newwindow', 'toolbar=no,location=no,menubar=no,width=1000,height=600,titlebar=no, fullscreen=no,resizable=yes,scrollbars=yes,top=60,left=60');return false;");
                    //lnkEdit.Attributes.Add("onclick", "window.open('YDMS_ActivityInvoice.aspx?AID=" + oActivity.Encrypt(hdnActualID.Value) + "', 'newwindow', 'toolbar=no,location=no,menubar=no,width=1000,height=600,titlebar=no, fullscreen=no,resizable=yes,scrollbars=yes,top=60,left=60');return false;");
                }
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            DataTable dt = oActivity.GetInvoiceReportData(Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID, Convert.ToInt32(ddlStatus.SelectedValue), ddlActivityType.SelectedValue);
            dt.Columns.Remove("AIH_PkHdrID");
            dt.Columns.Remove("PKActualID");
            new BXcel().ExporttoExcel(dt, "Invoice Report");
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnSapData_Click(object sender, EventArgs e)
        {
            BDMS_Activity oActivity = new BDMS_Activity();
            DataTable dt = oActivity.GetInvoiceReportData_ForSAP(Convert.ToInt32(ddlDealerSearch.SelectedValue), txtFromDateSearch.Text, txtToDateSearch.Text, PSession.User.UserID, Convert.ToInt32(ddlStatus.SelectedValue), ddlActivityType.SelectedValue);
            new BXcel().ExporttoExcel(dt, "Invoice Data for SAP");
        }
        int NewPageIndex = 0;
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewPageIndex = e.NewPageIndex;
        }

        protected void gvData_PageIndexChanged(object sender, EventArgs e)
        {
            gvData.PageIndex = NewPageIndex;
            Search_Click(null, null);
        }
    }
}