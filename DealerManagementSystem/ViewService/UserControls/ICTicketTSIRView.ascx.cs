using Business; 
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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
                MPE_AddTSIR.Show();
                UC_AddTSIR.Write(Tsir);
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
            else if (lbActions.Text == "TSIR Sales Approve L1")
            {
                btnSaleApproveL1.Visible = true;
                btnSaleApproveL2.Visible = false;
                MPE_SaleApprove.Show();
            }
            else if (lbActions.Text == "TSIR Sales Approve L2")
            {
                btnSaleApproveL1.Visible = false;
                btnSaleApproveL2.Visible = true;
                MPE_SaleApprove.Show();
            }
            else if (lbActions.Text == "TSIR Sales Reject")
            {
                if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.SalesRejected, PSession.User.UserID, 0))
                {
                    lblMessage.Text = "TSIR Status changed to Sales Rejected";
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
            else if (lbActions.Text == "TSIR Cancel")
            {

                string endPoint = "ICTicketTsir/CancelICTicketTSIR?TsirID=" + Tsir.TsirID + "&ICTicketID=" + Tsir.ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    lblMessage.Text = Results.Message;
                    return;
                }
                ShowMessage(Results);

                //List<string> querys = new List<string>();
                //lblMessage.Visible = true;
                //List<PDMS_ServiceCharge> SS_ServiceCharge = new BDMS_Service().GetServiceCharges(Tsir.ICTicket.ICTicketID, null, "", false);
                // foreach (PDMS_ServiceCharge SC in SS_ServiceCharge)
                //{
                //    if (!string.IsNullOrEmpty(SC.ClaimNumber))
                //    {
                //        lblMessage.Text = "Service claim generated. Please cancel the Claim";
                //        lblMessage.ForeColor = Color.Red;
                //        return;
                //    }
                //}
                //List<PDMS_ServiceMaterial> SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(Tsir.ICTicket.ICTicketID, null, null, "", false, "");
                //foreach (PDMS_ServiceMaterial SM in SS_ServiceMaterial)
                //{
                //    if (SM.TSIR.TsirID == Tsir.TsirID)
                //    {
                //        new BDMS_Service().UpdateSaleOrderNumberFromPostgres();
                //        if (!string.IsNullOrEmpty(SM.ClaimNumber))
                //        {
                //            lblMessage.Text = "claim generated for Material " + SM.Material.MaterialCode;
                //            lblMessage.ForeColor = Color.Red;
                //            return;
                //        }
                //        if (!string.IsNullOrEmpty(SM.DeliveryNumber))
                //        {
                //            lblMessage.Text = "Delivery Completed for Material " + SM.Material.MaterialCode;
                //            lblMessage.ForeColor = Color.Red;
                //            return;
                //        }
                //    }
                //}
                //Boolean ID = new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.Canceled, PSession.User.UserID, 0);
                //if (ID)
                //{
                //    lblMessage.Text = "TSIR is Canceled successfully";
                //    lblMessage.ForeColor = Color.Green;
                //    FillTsir(Tsir.TsirID);
                //}
                //else
                //{
                //    lblMessage.Text = "TSIR is not Canceled successfully";
                //    lblMessage.ForeColor = Color.Red;
                //}
            }
            //else if (lbActions.ID == "lbtnHoComments1")
            //{
            //    lblComments.Text = "Update HO Comments 1";
            //    MPE_Comments.Show();
            //}
            //else if (lbActions.ID == "lbtnHoComments2")
            //{
            //    lblComments.Text = "Update HO Comments 2";
            //    MPE_Comments.Show();
            //}
            else if (lbActions.ID == "lbtnSendMail")
            { 
                MPE_MailToSupplier.Show();
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

        protected void btnSaleApproveL1_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;

            if (string.IsNullOrEmpty(txtSalesApproveAmount.Text.Trim()))
            {
                lblMessage.Text = "Please check the Sales 1 Approve Amount";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            Decimal ApproveAmount = Convert.ToDecimal(txtSalesApproveAmount.Text.Trim());
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.SalesApprovedLevel1, PSession.User.UserID, ApproveAmount))
            {
                lblMessage.Text = "TSIR Status changed to Sales Approved";
                lblMessage.ForeColor = Color.Green;
                FillTsir(Tsir.TsirID); 
            }
            else
            {
                lblMessage.Text = "TSIR Status is not changed";
                lblMessage.ForeColor = Color.Red;
            }

        }
        protected void btnSaleApproveL2_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;

            if (string.IsNullOrEmpty(txtSalesApproveAmount.Text.Trim()))
            {
                lblMessage.Text = "Please check the Sales 2 Approve Amount";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            Decimal ApproveAmount = Convert.ToDecimal(txtSalesApproveAmount.Text.Trim());
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(Tsir.TsirID, (short)TSIRStatus.SalesApproved, PSession.User.UserID, ApproveAmount))
            {
                lblMessage.Text = "TSIR Status changed to Sales Approved";
                lblMessage.ForeColor = Color.Green;
                FillTsir(Tsir.TsirID);
            }
            else
            {
                lblMessage.Text = "TSIR Status is not changed";
                lblMessage.ForeColor = Color.Red;
            }
        }
       
        void ActionControlMange()
        {
            lbtnEdit.Visible = true;
            lbtnCheck.Visible = true;
            lbtnApprove.Visible = true;
            lbtnReject.Visible = true;
            lbtnSalesApproveL1.Visible = true;
            lbtnSalesApproveL2.Visible = true;
            lbtnSalesReject.Visible = true;
            lbtnSendBack.Visible = true;            
            lbtnCancel.Visible = true;
            lbtnSendMail.Visible = true;

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirCheck).Count() == 0)
            { 
                lbtnCheck.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirApprove).Count() == 0)
            { 
                lbtnApprove.Visible = false;
                lbtnReject.Visible = false;
                lbtnSendBack.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirSalesApproveL1).Count() == 0)
            { 
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesReject.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirSalesApproveL2).Count() == 0)
            { 
                lbtnSalesApproveL2.Visible = false;
                lbtnSalesReject.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirCancel).Count() == 0)
            { 
                lbtnCancel.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirEdit).Count() == 0)
            {
                lbtnEdit.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirMailToSupplier).Count() == 0)
            {
                lbtnSendMail.Visible = false;
            }


            if (Tsir.Status.StatusID == (short)TSIRStatus.Requested || Tsir.Status.StatusID == (short)TSIRStatus.Rerequested)
            {
                lbtnApprove.Visible = false;
                lbtnReject.Visible = false;
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSalesReject.Visible = false;
                lbtnSendBack.Visible = false;
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.Checked)
            {
                // lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;

                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSalesReject.Visible = false;
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.Approved || (Tsir.Status.StatusID == (short)TSIRStatus.SalesApproved) || Tsir.Status.StatusID == (short)TSIRStatus.SalesRejected)
            {
                lbtnEdit.Visible = false;
                lbtnCheck.Visible = false;
                lbtnApprove.Visible = false;
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSalesReject.Visible = false;
                lbtnSendBack.Visible = false;
                lbtnReject.Visible = false;
                lbtnCancel.Visible = false;
            }
            else if (Tsir.Status.StatusID == (short)TSIRStatus.SendBack)
            {
                lbtnApprove.Visible = false;
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSalesReject.Visible = false;
                lbtnSendBack.Visible = false;
                lbtnReject.Visible = false;
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
                lbtnReject.Visible = false; 
                //  lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                //lbtnSalesReject.Visible = false; 
                lbtnSendBack.Visible = false;
            }

            if ((Tsir.ICTicket.ServiceType.ServiceTypeID != (short)DMS_ServiceType.GoodwillWarranty))
            {
                lbtnSalesApproveL1.Visible = false;
                lbtnSalesApproveL2.Visible = false;
                lbtnSalesReject.Visible = false;
            }
        }

        protected void btnAddTSIR_Click(object sender, EventArgs e)
        {
            MPE_AddTSIR.Show();
            string Message = "";
             Message = UC_AddTSIR.Validation();
            lblMessageAddTSIR.ForeColor = Color.Red;
            lblMessageAddTSIR.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageAddTSIR.Text = Message;
                return;
            }
            PDMS_ICTicketTSIR_API TistUpdate = UC_AddTSIR.Read();
            TistUpdate.ICTicketID = Tsir.ICTicket.ICTicketID;
            TistUpdate.TsirID = Tsir.TsirID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketTsir/InsertOrUpdateICTicketTSIR", TistUpdate));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAddTSIR.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_AddTSIR.Hide();
            FillTsir(Tsir.TsirID);
        }

        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        protected void btnComments_Click(object sender, EventArgs e)
        {
            //if (lblComments.Text == "Update HO Comments 1")
            //{
            //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRComments(Tsir.TsirID, txtComments.Text, 1, PSession.User.UserID))
            //    {
            //        lblMessage.Text = "Updated Successfully";
            //        lblMessage.ForeColor = Color.Green;
            //        lblMessage.Visible = true;
            //    }
            //    else
            //    {
            //        lblMessage.Text = "Updated is not Successfully";
            //        lblMessage.ForeColor = Color.Red;
            //        lblMessage.Visible = true;
            //    }
            //}
            //else if (lblComments.Text == "Update HO Comments 2")
            //{
            //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRComments(Tsir.TsirID, txtComments.Text, 2, PSession.User.UserID))
            //    {
            //        lblMessage.Text = "Updated Successfully";
            //        lblMessage.ForeColor = Color.Green;
            //        lblMessage.Visible = true;
            //    }
            //    else
            //    {
            //        lblMessage.Text = "Updated is not Successfully";
            //        lblMessage.ForeColor = Color.Red;
            //        lblMessage.Visible = true;
            //    }
            //}           

        }
        private Byte[] SendPDFTSIR(PDMS_ICTicketTSIR TSIR)
        {
            try
            {
                new BDMS_ICTicketFSR().ICTicket_Directorys(Server.MapPath("~"));

                 

                string FailureCode = "";
                PDMS_ICTicket ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(TSIR.ICTicket.ICTicketID);
                PDMS_ICTicketFSR FSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, TSIR.ICTicket.ICTicketID, null, null, null, null, null, null);

                List<PDMS_WarrantyInvoiceHeader> ClaimList = new BDMS_WarrantyClaim().GetWarrantyClaimReport(TSIR.ICTicket.ICTicketNumber, null, null, "", null, null, "", null, null, null, "", "", "", false, null);
                string TL_ContactDetails = "";
                string SM_ContactDetails = "";
                if (ClaimList.Count != 0)
                {
                    TL_ContactDetails = ClaimList[0].Approved1By == null ? "" : ClaimList[0].Approved1By.ContactName + "  " + ClaimList[0].Approved1By.ContactNumber;
                    SM_ContactDetails = ClaimList[0].Approved2By == null ? "" : ClaimList[0].Approved2By.ContactName + "  " + ClaimList[0].Approved2By.ContactNumber;
                }
                if (string.IsNullOrEmpty(TL_ContactDetails))
                {
                    TL_ContactDetails = ICTicket.Dealer.TL == null ? "" : ICTicket.Dealer.TL.ContactName + "  " + ICTicket.Dealer.TL.ContactNumber;
                    SM_ContactDetails = ICTicket.Dealer.SM == null ? "" : ICTicket.Dealer.SM.ContactName + "  " + ICTicket.Dealer.SM.ContactNumber;
                }
                List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(TSIR.ICTicket.ICTicketID, null, "", false);
                foreach (PDMS_ServiceCharge SC in Charge)
                {
                    List<string> Materials = new BDMS_Material().GetMaterialServiceAutocomplete(SC.Material.MaterialCode, "", TSIR.ICTicket.ServiceType.ServiceTypeID, null, true);
                    if (Materials.Count() == 1)
                    {
                        FailureCode = SC.Material.MaterialCode;
                    }
                }
                PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(TSIR.ICTicket.Customer.CustomerCode);
                string CustomerAddress = Customer.Address1 + ", " + Customer.Address1 + ", " + Customer.Address3 + ", " + Customer.City + ", " + Customer.State.State + " - " + Customer.Pincode;
                CustomerAddress = CustomerAddress.Replace(", ,", ",").Replace(",,", ",");
                CustomerAddress = CustomerAddress.Trim(',', ' ');

                DataTable MaterialDT = new DataTable();
                MaterialDT.Columns.Add("Material");
                MaterialDT.Columns.Add("Description");
                MaterialDT.Columns.Add("HSN");
                MaterialDT.Columns.Add("Qty");

                DataTable FMaterialDT = new DataTable();
                FMaterialDT.Columns.Add("Material");
                FMaterialDT.Columns.Add("Description");
                FMaterialDT.Columns.Add("HSN");

                List<PDMS_ServiceMaterial> MaterialC = new BDMS_Service().GetServiceMaterials(TSIR.ICTicket.ICTicketID, null, TSIR.TsirID, "", false, "");
                foreach (PDMS_ServiceMaterial Mat in MaterialC)
                {
                    MaterialDT.Rows.Add(Mat.Material.MaterialCode, Mat.Material.MaterialDescription, Mat.Material.MaterialSerialNumber, Mat.Qty);
                    if (Mat.DefectiveMaterial != null)
                        FMaterialDT.Rows.Add(Mat.DefectiveMaterial.MaterialCode, Mat.DefectiveMaterial.MaterialDescription, Mat.DefectiveMaterial.MaterialSerialNumber);
                }

                List<string> FileNames = new List<string>();
                List<string> FiePath = new List<string>();

                DataTable FsrFiles = new DataTable();
                FsrFiles.Columns.Add("FileName1");
                FsrFiles.Columns.Add("FiePath1");
                FsrFiles.Columns.Add("FileName2");
                FsrFiles.Columns.Add("FiePath2");

                string Path1 = "";
                List<PDMS_FSRAttachedFile> FSRFile = new List<PDMS_FSRAttachedFile>();
                List<PDMS_FSRAttachedFile> FSRFileAll = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(TSIR.ICTicket.ICTicketID, null);
                for (int i = 0; i < FSRFileAll.Count(); i++)
                {
                    int FileNameID = FSRFileAll[i].FSRAttachedName.FSRAttachedFileNameID;
                    if ((FileNameID == (short)FSRAttachedFileName.Technician) || (FileNameID == (short)FSRAttachedFileName.Customer)
                         || (FileNameID == (short)FSRAttachedFileName.TechnicianSignature) || (FileNameID == (short)FSRAttachedFileName.CustomerSignature))
                    {
                    }
                    else
                    {
                        FSRFile.Add(FSRFileAll[i]);
                    }
                }
                for (int i = 0; i < FSRFile.Count(); i++)
                {
                    //PDMS_FSRAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(FSRFile[i].AttachedFileID);
                    PAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(FSRFile[i].AttachedFileID);
                    string Url1 = "~/ICTickrtFSR_Files/Org/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

                    if (File.Exists(MapPath(Url1)))
                    {
                        File.Delete(MapPath(Url1));
                    }
                    File.WriteAllBytes(MapPath(Url1), F1.AttachedFile);


                    string DestPath = "ICTickrtFSR_Files/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

                  new BDMS_ICTicketTSIR().resizeImage2(MapPath(Url1), Server.MapPath("~/" + DestPath));
                    Path1 = new Uri(Server.MapPath("~/" + DestPath)).AbsoluteUri;
                    //  FsrFiles.Rows.Add(F1.FSRAttachedName.FSRAttachedName, Path1);
                    FileNames.Add(F1.FileName);
                    FiePath.Add(Path1);
                }

                List<PDMS_TSIRAttachedFile> TSIRFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(TSIR.ICTicket.ICTicketID, TSIR.TsirID, null);
                for (int i = 0; i < TSIRFile.Count(); i++)
                {
                   // PDMS_TSIRAttachedFile T1 = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileByID(TSIRFile[i].AttachedFileID);
                    PAttachedFile T1 = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileForDownload(TSIRFile[i].AttachedFileID);

                    string Url1 = "~/ICTickrtTSIR_Files/Org/" + T1.AttachedFileID + "." + T1.FileName.Split('.')[T1.FileName.Split('.').Count() - 1];
                    if (File.Exists(MapPath(Url1)))
                    {
                        File.Delete(MapPath(Url1));
                    }
                    File.WriteAllBytes(MapPath(Url1), T1.AttachedFile);
                    string DestPath = "ICTickrtTSIR_Files/" + T1.AttachedFileID + "." + T1.FileName.Split('.')[T1.FileName.Split('.').Count() - 1];

                    new BDMS_ICTicketTSIR().resizeImage2(MapPath(Url1), Server.MapPath("~/" + DestPath));
                    Path1 = new Uri(Server.MapPath("~/" + DestPath)).AbsoluteUri;

                    //FsrFiles.Rows.Add(T1.FSRAttachedName.FSRAttachedName, Path1);
                    FileNames.Add(T1.FileName);
                    FiePath.Add(Path1);
                }


                for (int i = 0; i < FileNames.Count; i++)
                {
                    if (i + 1 != FileNames.Count())
                    {
                        FsrFiles.Rows.Add(FileNames[i], FiePath[i], FileNames[i + 1], FiePath[i + 1]);
                        i = i + 1;
                    }
                    else
                    {
                        FsrFiles.Rows.Add(FileNames[i], FiePath[i], "", "");
                    }
                }

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "TSIR_" + TSIR.TsirNumber + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;
                ReportParameter[] P = new ReportParameter[37];
                P[0] = new ReportParameter("TSIRNumber", TSIR.TsirNumber, false);
                P[1] = new ReportParameter("TSIRDate", TSIR.TsirDate.ToShortDateString(), false);
                P[2] = new ReportParameter("ICTicketNo", TSIR.ICTicket.ICTicketNumber, false);
                P[3] = new ReportParameter("ICTicketDate", TSIR.ICTicket.ICTicketDate.ToShortDateString(), false);
                P[4] = new ReportParameter("FSRNo", ICTicket.ICTicketNumber + "/" + ICTicket.Dealer.DealerCode + "/" + ICTicket.Technician.UserName + "/" + FSR.FSRDate.Year, false);
                P[5] = new ReportParameter("Application", ICTicket.MainApplication == null ? "" : TSIR.ICTicket.MainApplication.MainApplication, false);
                P[6] = new ReportParameter("EquipmentModel", ICTicket.Equipment.EquipmentModel.Model, false);
                P[7] = new ReportParameter("EquipmentSerialNo", ICTicket.Equipment.EquipmentSerialNo, false);
                P[8] = new ReportParameter("DealerCode", ICTicket.Dealer.DealerCode, false);
                P[9] = new ReportParameter("DealerName", ICTicket.Dealer.DealerName, false);
                P[10] = new ReportParameter("HMR", ICTicket.CurrentHMRValue == null ? "" : Convert.ToString(ICTicket.CurrentHMRValue) + " (" + ICTicket.Equipment.EquipmentModel.Division.UOM + ")", false);
                P[11] = new ReportParameter("TypeOfWarranty", ICTicket.TypeOfWarranty == null ? "" : ICTicket.TypeOfWarranty.TypeOfWarranty, false);
                P[12] = new ReportParameter("FSRDate", FSR.FSRDate.ToShortDateString(), false);
                P[13] = new ReportParameter("CustomerName", Customer.CustomerName, false);
                P[14] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[15] = new ReportParameter("Location", ICTicket.Location, false);
                P[16] = new ReportParameter("CustomerGSTStateCode", "GST State Code : " + Customer.State.StateCode, false);
                P[17] = new ReportParameter("CustomerGSTIN", "GSTIN/UIN No : " + Customer.GSTIN, false);
                P[18] = new ReportParameter("CustomerAddress", CustomerAddress, false);
                P[19] = new ReportParameter("NatureOfFailures", TSIR.NatureOfFailures, false);
                P[20] = new ReportParameter("ProblemNoticedBy", TSIR.ProblemNoticedBy, false);
                P[21] = new ReportParameter("UnderWhatConditionFailureTaken", TSIR.UnderWhatConditionFailureTaken, false);
                P[22] = new ReportParameter("FailureDetails", TSIR.FailureDetails, false);
                P[23] = new ReportParameter("PointsChecked", TSIR.PointsChecked, false);
                P[24] = new ReportParameter("PossibleRootCauses", TSIR.PossibleRootCauses, false);
                P[25] = new ReportParameter("SpecificPointsNoticed", TSIR.SpecificPointsNoticed, false);
                P[26] = new ReportParameter("ProblemCategory", ICTicket.ServicePriority.ServicePriority, false);
                P[27] = new ReportParameter("CommissioningOn", ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)ICTicket.Equipment.CommissioningOn).ToShortDateString(), false);
                P[28] = new ReportParameter("DispatchedOn", ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)ICTicket.Equipment.DispatchedOn).ToShortDateString(), false);
                P[29] = new ReportParameter("HOComments", TSIR.ServiceComments + " " + TSIR.ServiceComments, false);
                P[30] = new ReportParameter("FailureCode", FailureCode, false);
                P[31] = new ReportParameter("FsrfilesDisplay", FsrFiles.Rows.Count.ToString(), false);
                P[32] = new ReportParameter("SE_Name", ICTicket.Technician.ContactName, false);
                P[33] = new ReportParameter("SE_ContactNumber", ICTicket.Technician.ContactNumber, false);
                P[34] = new ReportParameter("TL_ContactDetails", TL_ContactDetails, false);
                P[35] = new ReportParameter("SM_ContactDetails", SM_ContactDetails, false);
                P[36] = new ReportParameter("PartsInvoiceNumber", TSIR.PartsInvoiceNumber, false);

                report.ReportPath = Server.MapPath("~/Print/DMS_TSIR2.rdlc");
                report.SetParameters(P);
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "Fsrfiles";//This refers to the dataset name in the RDLC file  
                rds.Value = FsrFiles;
                report.DataSources.Add(rds);
                ReportDataSource rds2 = new ReportDataSource();
                rds2.Name = "Material";//This refers to the dataset name in the RDLC file  
                rds2.Value = MaterialDT;
                report.DataSources.Add(rds2);
                ReportDataSource rds3 = new ReportDataSource();
                rds3.Name = "FMaterial";//This refers to the dataset name in the RDLC file  
                rds3.Value = FMaterialDT;
                report.DataSources.Add(rds3);
                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                return mybytes;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            return null;
        }

        protected void btnMailToSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(Tsir.TsirID, null);
                Byte[] MyByte = SendPDFTSIR(TSIR);
                string[] MailIDs = txtMailID.Text.Trim().Split(',', ';', ':');
                string Subject = "TSIR " + TSIR.TsirNumber + " - " + TSIR.ServiceCharge.Material.MaterialCode + " - " + TSIR.ICTicket.CurrentHMRValue;

                string messageBody = MailFormate.MailTsir;
                messageBody = messageBody.Replace("@@Name", PSession.User.ContactName);
                messageBody = messageBody.Replace("@@Designation", PSession.User.Designation.DealerDesignation);
                messageBody = messageBody.Replace("@@Phone", PSession.User.ContactNumber);
                messageBody = messageBody.Replace("@@MailID", PSession.User.Mail);
                foreach (string MailID in MailIDs)
                {
                    Boolean Success = new EmailManager().MailSendTSIR(MailID, Subject, messageBody, MyByte, "TSIR - " + TSIR.TsirNumber + ".PDF");
                    new BDMS_ICTicketTSIR().InsertICTicketTSIRMailToVendor(TSIR.TsirID, MailID, PSession.User.UserID, Success);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
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