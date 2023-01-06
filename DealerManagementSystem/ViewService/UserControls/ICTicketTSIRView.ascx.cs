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

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit TSIR")
            {
                //MPE_AddTechnician.Show();
                //UC_ICTicketAddTechnician.FillMaster(SDMS_ICTicket.Dealer.DealerID);
            }
            else if (lbActions.Text == "TSIR Check")
            {
                if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.Checked, PSession.User.UserID, 0))
                {
                    lblMessage.ForeColor = Color.Green;
                    FillTsir(Tsir.TsirID);
                    lblMessage.Text = "TSIR Status changed to Checked";
                }
                else
                {
                    lblMessage.Text = "TSIR Status is not changed";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
            else if (lbActions.Text == "TSIR Approve")
            {
                // string[] TsirApprove = ConfigurationManager.AppSettings["TsirApprove"].Split(',');
                //if (TsirApprove.Contains(PSession.User.UserID.ToString()))
                //{

                if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.Approved, PSession.User.UserID, 0))
                {
                    lblMessage.ForeColor = Color.Green;
                    FillTsir(Tsir.TsirID);
                    lblMessage.Text = "TSIR Status changed to Approved";
                }
                else
                {
                    lblMessage.Text = "TSIR Status is not changed";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
                //}
                //else
                //{
                //    return;
                //}
            }
            else if (lbActions.Text == "TSIR Sales Approve L1")
            {
                //    UC_AddFSRAttachments.FillMaster(SDMS_ICTicket);
                //    MPE_AddFSRAttachments.Show();
            }
            else if (lbActions.Text == "TSIR Sales Approve L2")
            {
                //    UC_AddFSRAttachments.FillMaster(SDMS_ICTicket);
                //    MPE_AddFSRAttachments.Show();
            }
            else if (lbActions.Text == "TSIR Reject")
            {
                if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.Rejected, PSession.User.UserID, 0))
                {
                    lblMessage.Text = "TSIR Status changed to Rejected";
                    lblMessage.ForeColor = Color.Green;
                    FillTsir(Tsir.TsirID);
                }
                else
                {
                    lblMessage.Text = "TSIR Status is not changed";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
            else if (lbActions.Text == "TSIR Send Back")
            {
                if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.SendBack, PSession.User.UserID, 0))
                {
                    lblMessage.Text = "TSIR Status changed to Send Back";
                    lblMessage.ForeColor = Color.Green;
                    FillTsir(Tsir.TsirID);
                }
                else
                {
                    lblMessage.Text = "TSIR Status is not changed";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
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
            ActionControlMange();
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

        void ActionControlMange()
        {
            lbtnEdit.Visible = true;
            lbtnCheck.Visible = true;
            lbtnApprove.Visible = true;
            lbtnSalesApproveL1.Visible = true;
            lbtnSalesApproveL2.Visible = true;
            lbtnSendBack.Visible = true;
            lbtnReject.Visible = true;
            lbtnCancel.Visible = true;
            

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirCheck).Count() == 0)
            {
                lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirApprove).Count() == 0)
            {
                lbtnApprove.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirSalesApproveL1).Count() == 0)
            {
                lbtnSalesApproveL1.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirSalesApproveL2).Count() == 0)
            {
                lbtnSalesApproveL2.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirCancel).Count() == 0)
            {
                lbtnCancel.Visible = false;
            }
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TaxQuotationPrint).Count() == 0)
            //{
            //    lbtnReject.Visible = false;
            //}



            if (Tsir.Status.StatusID == (short)TSIRStatus.Requested || Tsir.Status.StatusID == (short)TSIRStatus.Rerequested)
            {
                lbtnApprove.Visible = false;
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSendBack.Visible = false;
                lbtnReject.Visible = false;
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.Checked)
            {
                lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;

                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false; 
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.Approved || (Tsir.Status.StatusID == (short)TSIRStatus.SalesApproved) || (Tsir.Status.StatusID == (short)TSIRStatus.SendBack))
            {
                lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;
                lbtnApprove.Visible = false;
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSendBack.Visible = false;
                lbtnReject.Visible = false;
                lbtnCancel.Visible = false;
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.SalesApprovedLevel1)
            {
                lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;
                lbtnApprove.Visible = false;
                lbtnSalesApproveL1.Visible = false; 
                lbtnSendBack.Visible = false;
                lbtnReject.Visible = false;
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.Rejected)
            {
                lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;
                lbtnApprove.Visible = false; 
                lbtnSalesApproveL2.Visible = false;
                lbtnSendBack.Visible = false;
                lbtnReject.Visible = false;
            }
        }

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