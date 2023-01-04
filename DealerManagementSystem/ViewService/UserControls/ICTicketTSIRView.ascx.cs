using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketTSIRView : System.Web.UI.UserControl
    {
        public PDMS_ICTicketTSIR Tsir
        {
            get
            {
                if (ViewState["ICTicketTSIRView"] == null)
                {
                    ViewState["ICTicketTSIRView"] = new PDMS_ICTicketTSIR();
                }
                return (PDMS_ICTicketTSIR)ViewState["ICTicketTSIRView"];
            }
            set
            {
                ViewState["ICTicketTSIRView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (!string.IsNullOrEmpty(Convert.ToString(ViewState["QuotationID"])))
            {
                long TsirID = Convert.ToInt64(Convert.ToString(ViewState["QuotationID"]));
                if (TsirID != Tsir.TsirID)
                {
                    Tsir = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
                }
            }
        }


        public void FillTsir(long TsirID)
        {
            Tsir = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
            lblTsirNumber.Text = Tsir.TsirNumber;
            lblTsirStatus.Text = Tsir.Status.Status;
            lblServiceCharge.Text = Tsir.ServiceCharge.Material.MaterialCode + " - " + Tsir.ServiceCharge.Material.MaterialDescription;

            lblNatureOfFailures.Text = Tsir.NatureOfFailures;
            lblProblemNoticedBy.Text = Tsir.ProblemNoticedBy;
            lblUnderWhatConditionFailureTaken.Text = Tsir.UnderWhatConditionFailureTaken;
            lblFailureDetails.Text = Tsir.FailureDetails;
            lblPointsChecked.Text = Tsir.PointsChecked;
            lblPossibleRootCauses.Text = Tsir.PossibleRootCauses;
            lblSpecificPointsNoticed.Text = Tsir.SpecificPointsNoticed;
            lblPartsInvoiceNumber.Text = Tsir.PartsInvoiceNumber;
            ViewState["TsirID"] = Tsir.TsirID;

            FillMessage(TsirID);

            PDMS_ICTicket ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(Tsir.ICTicket.ICTicketID);
            //UC_BasicInformation.SDMS_ICTicket = ICTicket;
            //UC_BasicInformation.FillBasicInformation();

            gvMaterial.DataSource = new BDMS_Service().GetServiceMaterials(ICTicket.ICTicketID, null, TsirID, "", false, "");
            gvMaterial.DataBind();
            if (ICTicket.RestoreDate == null)
            {
                return;
            }
        }
        void FillMessage(long TsirID)
        {
            Boolean? DisplayToDealer = null;

            if (PSession.User.UserTypeID == (short)UserTypes.Dealer)
            {
                DisplayToDealer = true;
                gvTSIRMessage.Columns[1].Visible = false;
            }


            List<PDMS_ICTicketTSIRMessage> TSIRMessage = new BDMS_ICTicketTSIR().GetICTicketTSIRMessage(null, TsirID, DisplayToDealer);
            if (TSIRMessage.Count == 0)
            {
                PDMS_ICTicketTSIRMessage N = new PDMS_ICTicketTSIRMessage();
                TSIRMessage.Add(N);
            }
            gvTSIRMessage.DataSource = TSIRMessage;
            gvTSIRMessage.DataBind();
        }
        protected void lblTSIRMessageAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            TextBox txtTSIRMessage = (TextBox)gvTSIRMessage.FooterRow.FindControl("txtTSIRMessage");
            CheckBox cbDisplayToDealer = (CheckBox)gvTSIRMessage.FooterRow.FindControl("cbDisplayToDealer");

            if (string.IsNullOrEmpty(txtTSIRMessage.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Message";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            long TsirID = Convert.ToInt64(ViewState["TsirID"]);
            Boolean DisplayToDealer = cbDisplayToDealer.Checked;
            if (PSession.User.UserTypeID == (short)UserTypes.Dealer)
            {
                DisplayToDealer = true;
            }
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRMessage(TsirID, txtTSIRMessage.Text.Trim(), DisplayToDealer, PSession.User.UserID))
            {
                lblMessage.Text = "New Message is added for this TSIR";
                lblMessage.ForeColor = Color.Green;
                FillMessage(TsirID);
                txtTSIRMessage.Text = "";
                cbDisplayToDealer.Checked = false;
            }
            else
            {
                lblMessage.Text = "New Message is not added for this TSIR";
                lblMessage.ForeColor = Color.Red;
            }
        }

        //protected void btnChecked_Click(object sender, EventArgs e)
        //{
        //    //int StatusID = 1;
        //    int StatusID = ICTicketTSIR.Status.StatusID;

        //    if ((ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Requested) || (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Rerequested))
        //    {
        //        StatusID = (short)TSIRStatus.Checked;
        //        lblMessage.Text = "TSIR Status changed to Checked";
        //    }
        //    else if (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Checked)
        //    {
        //        string[] TsirApprove = ConfigurationManager.AppSettings["TsirApprove"].Split(',');
        //        if (TsirApprove.Contains(PSession.User.UserID.ToString()))
        //        {
        //            StatusID = (short)TSIRStatus.Approved;
        //            lblMessage.Text = "TSIR Status changed to Approved";
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }

        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, StatusID, PSession.User.UserID, 0))
        //    {

        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}

        //protected void btnSendBack_Click(object sender, EventArgs e)
        //{
        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.SendBack, PSession.User.UserID, 0))
        //    {
        //        lblMessage.Text = "TSIR Status changed to Send Back";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}       

        //protected void btnReject_Click1(object sender, EventArgs e)
        //{
        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.Rejected, PSession.User.UserID, 0))
        //    {
        //        lblMessage.Text = "TSIR Status changed to Rejected";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (!Validation())
        //    {
        //        return;
        //    }
        //    ICTicketTSIR.TsirID = ViewState["TsirID"] == null ? 0 : (long)ViewState["TsirID"];

        //    ICTicketTSIR.ServiceCharge = new PDMS_ServiceCharge();
        //    ICTicketTSIR.ServiceCharge.ServiceChargeID = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(ICTicketTSIR.TsirID, null).ServiceCharge.ServiceChargeID;
        //    ICTicketTSIR.NatureOfFailures = txtNatureOfFailures.Text.Trim();
        //    ICTicketTSIR.ProblemNoticedBy = txtProblemNoticedBy.Text.Trim();
        //    ICTicketTSIR.UnderWhatConditionFailureTaken = txtUnderWhatConditionFailureTaken.Text.Trim();
        //    ICTicketTSIR.FailureDetails = txtFailureDetails.Text.Trim();
        //    ICTicketTSIR.PointsChecked = txtPointsChecked.Text.Trim();
        //    ICTicketTSIR.PossibleRootCauses = txtPossibleRootCauses.Text.Trim();
        //    ICTicketTSIR.SpecificPointsNoticed = txtSpecificPointsNoticed.Text.Trim();
        //    ICTicketTSIR.PartsInvoiceNumber = txtPartsInvoiceNumber.Text.Trim();
        //    long DealerEmployeeID = new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIR(ICTicketTSIR, PSession.User.UserID);
        //    if (DealerEmployeeID != 0)
        //    {
        //        lblMessage.Text = "TSIR is updated successfully";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR is not updated successfully";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //Boolean Validation()
        //{
        //    lblMessage.Visible = true;
        //    string Message = "";
        //    Boolean Ret = true;
        //    //if (string.IsNullOrEmpty(txtFailureRepeats.Text.Trim()))
        //    //{
        //    //    Message = Message + "<br/>Please Enter the Failure Repeats";
        //    //    Ret = false;
        //    //}

        //    if (string.IsNullOrEmpty(txtNatureOfFailures.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the NatureOfFailures";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtProblemNoticedBy.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the How Was Problem Noticed / Who  / When";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtUnderWhatConditionFailureTaken.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Under What Condition Failure Taken";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtFailureDetails.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Failure Details";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtPointsChecked.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Points Checked";
        //        Ret = false;
        //    }

        //    if (string.IsNullOrEmpty(txtPossibleRootCauses.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Possible Root Causes";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtSpecificPointsNoticed.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the SpecificPoints Noticed";
        //        Ret = false;
        //    }
        //    lblMessage.Text = Message;
        //    return Ret;
        //}
    }
}