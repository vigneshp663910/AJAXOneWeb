using Business;
using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketView : System.Web.UI.UserControl
    {
        //public AddFsrSignature UC_FsrSignature
        //{
        //    get
        //    {
        //        if (ViewState["UC_FsrSignature"] == null)
        //        {
        //            ViewState["UC_FsrSignature"] = (AddFsrSignature)Page.LoadControl("~/ViewService/UserControls/AddFsrSignature.ascx");
        //        }
        //        return (AddFsrSignature)ViewState["UC_FsrSignature"];
        //    }
        //    //set
        //    //{
        //    //    ViewState["SDMS_ICTicket"] = value;
        //    //}
        //}
        public PDMS_ICTicket SDMS_ICTicket
        {
            get
            {
                if (ViewState["SDMS_ICTicket"] == null)
                {
                    ViewState["SDMS_ICTicket"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)ViewState["SDMS_ICTicket"];
            }
            set
            {
                ViewState["SDMS_ICTicket"] = value;
            }
        }
        public PDMS_ICTicketFSR SDMS_ICTicketFSR
        {
            get
            {
                if (ViewState["PDMS_ICTicketFSR"] == null)
                {
                    ViewState["PDMS_ICTicketFSR"] = new PDMS_ICTicketFSR();
                }
                return (PDMS_ICTicketFSR)ViewState["PDMS_ICTicketFSR"];
            }
            set
            {
                ViewState["PDMS_ICTicketFSR"] = value;
            }
        }

        public List<PDMS_ServiceMaterial> SS_ServiceMaterialAll
        {
            get
            {
                if (ViewState["ServiceMaterialAllICTicketProcess"] == null)
                {
                    ViewState["ServiceMaterialAllICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)ViewState["ServiceMaterialAllICTicketProcess"];
            }
            set
            {
                ViewState["ServiceMaterialAllICTicketProcess"] = value;
            }
        }
        public List<PDMS_ServiceMaterial> SS_ServiceMaterial
        {
            get
            {
                if (ViewState["ServiceMaterialICTicketProcess"] == null)
                {
                    ViewState["ServiceMaterialICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)ViewState["ServiceMaterialICTicketProcess"];
            }
            set
            {
                ViewState["ServiceMaterialICTicketProcess"] = value;
            }
        }
        public List<PDMS_ICTicketTSIR> ICTicketTSIRs
        {
            get
            {
                if (ViewState["PDMS_ICTicketTSIRs"] == null)
                {
                    ViewState["PDMS_ICTicketTSIRs"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)ViewState["PDMS_ICTicketTSIRs"];
            }
            set
            {
                ViewState["PDMS_ICTicketTSIRs"] = value;
            }
        }
        public PDMS_ICTicketTSIR ICTicketTSIR
        {
            get
            {
                if (ViewState["PDMS_ICTicketTSIR"] == null)
                {
                    ViewState["PDMS_ICTicketTSIR"] = new PDMS_ICTicketTSIR();
                }
                return (PDMS_ICTicketTSIR)ViewState["PDMS_ICTicketTSIR"];
            }
            set
            {
                ViewState["PDMS_ICTicketTSIR"] = value;
            }
        }
        
        public List<PDMS_ServiceTechnician> SDMS_TechniciansWD
        {
            get
            {
                if (ViewState["DMS_ICTicketTechnicianAssignWD"] == null)
                {
                    ViewState["DMS_ICTicketTechnicianAssignWD"] = new List<PDMS_ServiceTechnician>();
                }
                return (List<PDMS_ServiceTechnician>)ViewState["DMS_ICTicketTechnicianAssignWD"];
            }
            set
            {
                ViewState["DMS_ICTicketTechnicianAssignWD"] = value;
            }
        }
         
        public  PICTicketCustomerFeedback  CustomerFeedback
        {
            get
            {
                if (ViewState["DMS_PICTicketCustomerFeedback"] == null)
                {
                    ViewState["DMS_PICTicketCustomerFeedback"] = new  PICTicketCustomerFeedback ();
                }
                return ( PICTicketCustomerFeedback )ViewState["DMS_PICTicketCustomerFeedback"];
            }
            set
            {
                ViewState["DMS_PICTicketCustomerFeedback"] = value;
            }
        }

        public DataTable MarginWarrantyReq
        {
            get
            {
                if (ViewState["MarginWarrantyReq"] == null)
                {
                    ViewState["MarginWarrantyReq"] = new DataTable();
                }
                return (DataTable)ViewState["MarginWarrantyReq"];
            }
            set
            {
                ViewState["MarginWarrantyReq"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblServiceChargeSessage.Visible = false;
            lblMessageAddTSIR.Visible = false;
            lblMessageMaterialCharges.Visible = false;
            lblReachedSiteMessage.Visible = false;
            lblMessageAssignEngineer.Visible = false;
            lblMessageCallInformation.Visible = false;
            lblFSRMessage.Visible = false;
            lblMessageFsrAttachments.Visible = false;
            lblMessageOtherMachine.Visible = false;
            lblMessageNote.Visible = false;
            lblMessageTechnicianWork.Visible = false;
            lblMessageRestore.Visible = false;
            lblMessageCustomerFeedback.Visible = false;
            Label1.Visible = false;
            lblMessageRequestDateChange.Visible = false;
            lblMessageMarginWarrantyRequest.Visible = false;
            lblMessageMarginWarrantyReject.Visible = false;
            lblMessageFsrSignature.Visible = false;
            lblMessage.Visible = false;

            if (!IsPostBack)
            {

            }

        }
        public void FillICTicket(long ICTicketID)
        {
            SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID_All(ICTicketID);
            SDMS_ICTicketFSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, SDMS_ICTicket.ICTicketID, null, null, "", null, null, null);
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            //  SS_ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
            SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
            CustomerFeedback = new BDMS_ICTicket().GetICTicketCustomerFeedback(ICTicketID);

            FillBasicInformation();

            gvTechnician.DataSource = SDMS_ICTicket.Technicians;
            gvTechnician.DataBind();
            // FillTechnicians();
            FillReached();
            FillCallInformation();
            FillFSR();
            fillICTicketAttachedFile();
            FillAvailabilityOfOtherMachine();


            gvServiceCharges.DataSource = SDMS_ICTicket.ServiceCharges;
            gvServiceCharges.DataBind(); 
            FillTSIRDetails();
            FillServiceMaterial();
            FillServiceNotes();
            FillTechniciansByTicketID();
            FillRestore();

            int RowCount = 0;
            MarginWarrantyReq = new BDMS_ICTicket().GetMarginWarrantyChangeForApproval(null, null, null, SDMS_ICTicket.ICTicketNumber, PSession.User.UserID, null, null, out RowCount);


            if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Declined
                || SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.ReqDeclined)
            {
                tpnlTechnician.Visible = false;
               // TabReached.Visible = false;
               // tpnlCallInformation.Visible = false;
                tpnlFSR.Visible = false;
                tpnlAvailabilityOfOtherMachine.Visible = false;
                tpnlServiceCharges.Visible = false;
                tpnlTSIR.Visible = false;
                tpnlMaterialCharges.Visible = false;
                tpnlNotes.Visible = false;
                TabechnicianWorkHours.Visible = false;
                TabRestore.Visible = false;
                TabSignature.Visible = false;
                FillDeclinedInfo();
            }
            else
            {
                tpnlDeclined.Visible = false;
            } 
            ActionControlMange();

        }
        void FillDeclinedInfo()
        {
            lblDeclinedReson.Text = SDMS_ICTicket.ReqDeclinedReason;
            lblDeclinedDate.Text = Convert.ToString(SDMS_ICTicket.ReqDeclinedDate);
        }
       
        public void FillBasicInformation()
        {
            lblICTicket.Text = SDMS_ICTicket.ICTicketNumber + " - " + SDMS_ICTicket.ICTicketDate;
            lblDealer.Text = SDMS_ICTicket.Dealer.DealerCode + " - " + SDMS_ICTicket.Dealer.DealerName;
            lblCustomer.Text = SDMS_ICTicket.Customer.CustomerCode + " - " + SDMS_ICTicket.Customer.CustomerName;
            lblCustomerCategory.Text = SDMS_ICTicket.Customer.CustomerCategory.CustomerCategory;
            lblStatus.Text = SDMS_ICTicket.ServiceStatus.ServiceStatus;
            lblRequestedDate.Text = SDMS_ICTicket.RequestedDate == null ? "" : (DateTime)SDMS_ICTicket.RequestedDate + "";
            cbIsWarranty.Checked = SDMS_ICTicket.IsWarranty;
            cbIsMarginWarranty.Checked = SDMS_ICTicket.IsMarginWarranty;

            lblWarrantyExpiry.Text = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString();
            lblRFWarrantyExpiryDate.Text = SDMS_ICTicket.Equipment.RFWarrantyExpiryDate == null ? "" : ((DateTime)SDMS_ICTicket.Equipment.RFWarrantyExpiryDate).ToShortDateString();
            lblAMCExpiryDate.Text = SDMS_ICTicket.Equipment.AMCExpiryDate == null ? "" : ((DateTime)SDMS_ICTicket.Equipment.AMCExpiryDate).ToShortDateString();

            lblDistrict.Text = SDMS_ICTicket.Address.District.District + " - " + SDMS_ICTicket.Address.State.State;
            lblContactPerson.Text = SDMS_ICTicket.ContactPerson + " " + SDMS_ICTicket.PresentContactNumber;
            lblComplaintDescription.Text = SDMS_ICTicket.ComplaintDescription;
            //lblInformation.Text = SDMS_ICTicket.Information;
            lblOldICTicketNumber.Text = SDMS_ICTicket.OldICTicketNumber;
            lblEquipment.Text = SDMS_ICTicket.Equipment.EquipmentSerialNo;
            lblModel.Text = SDMS_ICTicket.Equipment.EquipmentModel.Model;
            lblLastHMRValue.Text = SDMS_ICTicket.LastHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.LastHMRDate).ToShortDateString() + "  " + Convert.ToString(SDMS_ICTicket.LastHMRValue);
        }
        //private void FillTechnicians()
        //{
        //    SDMS_Technicians = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);
        //    gvTechnician.DataSource = SDMS_Technicians;
        //    gvTechnician.DataBind();
        //}

        private void FillReached()
        {
            lblLocation.Text = SDMS_ICTicket.Location;
            if (SDMS_ICTicket.DealerOffice != null)
                lblDealerOffice.Text = SDMS_ICTicket.DealerOffice.OfficeName_OfficeCode; 
            lblDepartureDate.Text = SDMS_ICTicket.DepartureDate == null ? "" : ((DateTime)SDMS_ICTicket.DepartureDate).ToString(); 
            lblReachedDate.Text = SDMS_ICTicket.ReachedDate == null ? "" : ((DateTime)SDMS_ICTicket.ReachedDate).ToString();

            if (SDMS_ICTicket.ServiceType != null)
            {
                lblServiceType.Text = SDMS_ICTicket.ServiceType.ServiceType;
                if (SDMS_ICTicket.ServiceType.ServiceTypeID == 7)
                {
                    ddlServiceTypeOverhaul.Visible = true;
                }
                else
                {
                    ddlServiceTypeOverhaul.Visible = false;
                }

                if (SDMS_ICTicket.ServiceTypeOverhaul != null)
                    ddlServiceTypeOverhaul.SelectedValue = SDMS_ICTicket.ServiceTypeOverhaul.ServiceTypeOverhaulID.ToString();

                //if (ddlServiceType.SelectedValue == "3")
                //{
                //    ddlServiceSubType.Visible = true;
                //}
                //if (SDMS_ICTicket.ServiceSubType != null)
                //{
                //    lblServiceSubType.tex = SDMS_ICTicket.ServiceSubType.ServiceSubTypeID.ToString();
                //}
            }
            if (SDMS_ICTicket.ServicePriority != null)
                lblServicePriority.Text = SDMS_ICTicket.ServicePriority.ServicePriority;
            lblHMRValue.Text = "Current HMR Value" + " ( " + SDMS_ICTicket.Equipment.EquipmentModel.Division.UOM + " ) ";
            lblHMRDate.Text = SDMS_ICTicket.CurrentHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.CurrentHMRDate).ToShortDateString();
            lblHMRValue.Text = Convert.ToString(SDMS_ICTicket.CurrentHMRValue); 
        }
        private void FillCallInformation()
        {  
            if (SDMS_ICTicket.MainApplication != null)
            {
                lblMainApplication.Text = SDMS_ICTicket.MainApplication.MainApplication;
                if (SDMS_ICTicket.SubApplication != null)
                {
                    lblSubApplication.Text = SDMS_ICTicket.SubApplication.SubApplication;
                    if (SDMS_ICTicket.SubApplication.SubApplicationID.ToString() == "26")
                    {
                        lblSubApplicationEntry.Visible = true;
                        lblSubApplicationEntry.Text = SDMS_ICTicket.SubApplicationEntry;
                    }
                }
            }

            lblScopeOfWork.Text = SDMS_ICTicket.ScopeOfWork;

            lblKindAttn.Text = SDMS_ICTicket.KindAttn;
            lblRemarks.Text = SDMS_ICTicket.Remarks;

            lblOperatorName.Text = SDMS_ICTicket.SiteContactPersonName;
            lblSiteContactPersonNumber.Text = SDMS_ICTicket.SiteContactPersonNumber;
            lblSiteContactPersonNumber2.Text = SDMS_ICTicket.SiteContactPersonNumber2;
            if (SDMS_ICTicket.SiteContactPersonDesignation != null)
            {
                lblDesignation.Text = SDMS_ICTicket.SiteContactPersonDesignation.Designation;
            }
            if (SDMS_ICTicket.TypeOfWarranty != null)
            {
                lblTypeOfWarranty.Text = SDMS_ICTicket.TypeOfWarranty.TypeOfWarranty;
            }
            cbCess.Checked = SDMS_ICTicket.IsCess;

            // lblSubApplicationEntry.Text = SDMS_ICTicket.SubApplicationEntry;
            cbIsMachineActive.Checked = SDMS_ICTicket.IsMachineActive;

            cbNoClaim.Checked = SDMS_ICTicket.NoClaim;

            lblNoClaimReason.Text = SDMS_ICTicket.NoClaimReason;

            lblMcEnteredServiceDate.Text = SDMS_ICTicket.McEnteredServiceDate == null ? "" : ((DateTime)SDMS_ICTicket.McEnteredServiceDate).ToShortDateString();
            lblServiceStartedDate.Text = SDMS_ICTicket.ServiceStartedDate == null ? "" : ((DateTime)SDMS_ICTicket.ServiceStartedDate).ToShortDateString();
            lblServiceEndedDate.Text = SDMS_ICTicket.ServiceEndedDate == null ? "" : ((DateTime)SDMS_ICTicket.ServiceEndedDate).ToShortDateString();
        }
        private void FillFSR()
        {
            if (SDMS_ICTicketFSR.FsrID != 0)
            {
                if (SDMS_ICTicketFSR.MachineMaintenanceLevel != null)
                    lblMachineMaintenanceLevel.Text = SDMS_ICTicketFSR.MachineMaintenanceLevel.MachineMaintenanceLevel;
                cbIsRental.Checked = SDMS_ICTicketFSR.IsRental;
                lblOperatorNameFSR.Text = SDMS_ICTicketFSR.OperatorName;
                lblOperatorNumber.Text = SDMS_ICTicketFSR.OperatorNumber;
                lblRentalName.Text = SDMS_ICTicketFSR.RentalName;
                lblRentalNumber.Text = SDMS_ICTicketFSR.RentalNumber;
                //  ddlComplaintStatus.SelectedValue = ICTicketFSR.ComplaintStatus;
                //  txtAlternateContactNumber.Text = ICTicketFSR.PresentContactNumberA;
                if (SDMS_ICTicketFSR.ModeOfPayment != null)
                    lblModeOfPayment.Text = SDMS_ICTicketFSR.ModeOfPayment.ModeOfPayment;
                lblReport.Text = SDMS_ICTicketFSR.Report;
                lblNatureOfComplaint.Text = SDMS_ICTicketFSR.NatureOfComplaint;
                lblObservation.Text = SDMS_ICTicketFSR.Observation;
                lblWorkCarriedOut.Text = SDMS_ICTicketFSR.WorkCarriedOut;
                //  txtSERecommendedParts.Text = ICTicketFSR.SERecommendedParts;

                PDMS_ICTicketFSRSignature FSRSignature = new BDMS_ICTicketFSR().GetICTicketFSRSignatureByFsrID(SDMS_ICTicketFSR.FsrID);
                if (FSRSignature.FSRSignatureID != 0)
                {
                    //lblTName.Text = FSRSignature.TName;
                    //lbtnTPhoto.Text = string.IsNullOrEmpty(FSRSignature.TPhoto) ? "No Photo" : "Photo Available";
                    //lbtnTSignature.Text = string.IsNullOrEmpty(FSRSignature.TSignature) ? "No Signature" : "Signature Available";
                    //lblCName.Text = FSRSignature.CName;
                    //lbtnCPhoto.Text = string.IsNullOrEmpty(FSRSignature.CPhoto) ? "No Photo" : "Photo Available";
                    //lbtnCSignature.Text = string.IsNullOrEmpty(FSRSignature.CSignature) ? "No Signature" : "Signature Available";

                    lblTName.Text = FSRSignature.TName;
                    lbtnTPhoto.Text = "Download";
                    lbtnTSignature.Text = "Download";
                    lblCName.Text = FSRSignature.CName;
                    lbtnCPhoto.Text = "Download";
                    lbtnCSignature.Text = "Download";
                }
            }
        }
        void fillICTicketAttachedFile()
        {
            try
            {
                List<PDMS_FSRAttachedFile> UploadedFile = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(SDMS_ICTicket.ICTicketID, null);
                gvAttachedFile.DataSource = UploadedFile;
                gvAttachedFile.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        public void FillAvailabilityOfOtherMachine()
        {
            List<PDMS_AvailabilityOfOtherMachine> Note = new BDMS_AvailabilityOfOtherMachine().GetAvailabilityOfOtherMachine(SDMS_ICTicket.ICTicketID, null, null, null);

            gvAvailabilityOfOtherMachine.DataSource = Note;
            gvAvailabilityOfOtherMachine.DataBind();
        }
        //public void FillServiceCharges()
        //{

        //   // List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
        //    //string ClaimNumber = "";
        //    //if (Charge.Count == 0)
        //    //{
        //    //    PDMS_ServiceCharge c = new PDMS_ServiceCharge();
        //    //    Charge.Add(c);
        //    //}
        //    //else
        //    //{
        //    //    ClaimNumber = Charge[0].ClaimNumber;
        //    //}

        //    //gvServiceCharges.DataSource = Charge;
        //    //gvServiceCharges.DataBind();
        //  //  gvServiceCharges.FooterRow.Visible = true;

        //    //HttpContext.Current.Session["IsMainServiceMaterial"] = false;
        //    //if (gvServiceCharges.Rows.Count == 1)
        //    //{
        //    //    Label lblMaterialCode = (Label)gvServiceCharges.Rows[0].FindControl("lblMaterialCode");
        //    //    if (string.IsNullOrEmpty(lblMaterialCode.Text))
        //    //    {
        //    //        HttpContext.Current.Session["IsMainServiceMaterial"] = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        if (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PromotionalActivity)
        //    //        {
        //    //            gvServiceCharges.FooterRow.Visible = false;
        //    //        }
        //    //    }
        //    //}

        //    //TextBox txtServiceMaterial = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceMaterial");
        //    //TextBox txtServiceDate = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceDate");
        //    //TextBox txtWorkedHours = (TextBox)gvServiceCharges.FooterRow.FindControl("txtWorkedHours");
        //    //TextBox txtBasePrice = (TextBox)gvServiceCharges.FooterRow.FindControl("txtBasePrice");
        //    //TextBox txtDiscount = (TextBox)gvServiceCharges.FooterRow.FindControl("txtDiscount");
        //    //LinkButton lblServiceAdd = (LinkButton)gvServiceCharges.FooterRow.FindControl("lblServiceAdd");

        //    //if (SDMS_ICTicket.ServiceType == null)
        //    //{
        //    //    txtWorkedHours.Visible = false;
        //    //    txtBasePrice.Visible = false;
        //    //    txtDiscount.Visible = false;
        //    //}
        //    //else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
        //    //    )
        //    //{
        //    //    txtWorkedHours.Visible = true;
        //    //    txtBasePrice.Visible = true;
        //    //    txtDiscount.Visible = true;
        //    //}
        //    //else
        //    //{
        //    //    txtWorkedHours.Visible = false;
        //    //    txtBasePrice.Visible = false;
        //    //    txtDiscount.Visible = false;
        //    //}
        //    //DataControlField gcServiceCharges = gvServiceCharges.Columns[14];
        //    //gcServiceCharges.Visible = true;

        //    //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
        //    //    )
        //    //{
        //    //    gvServiceCharges.Columns[8].Visible = false;

        //        //btnRequestForClaim.Visible = false;
        //        //btnGenerateQuotation.Visible = true;
        //        //btnGenerateProfarmaInvoice.Visible = true;
        //        //btnGenerateInvoice.Visible = true;
        //        //txtServiceMaterial.Visible = true;
        //        //lblServiceAdd.Visible = true;

        //        //txtWorkedHours.Visible = true;
        //        //txtBasePrice.Visible = true;
        //        //txtDiscount.Visible = true;
        //        //txtServiceDate.Visible = true;

        //        //List<PDMS_PaidServiceInvoice> Invoices = new BDMS_Service().GetPaidServiceInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "", true);
        //        //if (Invoices.Count == 1)
        //        //{
        //            //btnGenerateQuotation.Visible = false;
        //            //btnGenerateProfarmaInvoice.Visible = false;
        //            //btnGenerateInvoice.Visible = false;

        //        //    txtServiceMaterial.Visible = false;
        //        //    lblServiceAdd.Visible = false;

        //        //    txtWorkedHours.Visible = false;
        //        //    txtBasePrice.Visible = false;
        //        //    txtDiscount.Visible = false;
        //        //    txtServiceDate.Visible = false;
        //        //    gcServiceCharges.Visible = false;
        //        //    gvServiceCharges.FooterRow.Visible = false;
        //        //}
        //        //else
        //        //{
        //        //    List<PDMS_PaidServiceInvoice> Proformas = new BDMS_Service().GetPaidServiceProformaInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
        //        //    if (Proformas.Count == 1)
        //        //    {
        //                //btnGenerateProfarmaInvoice.Visible = false;
        //                //btnGenerateQuotation.Visible = false;
        //            //    txtServiceMaterial.Visible = false;
        //            //    lblServiceAdd.Visible = false;

        //            //    txtWorkedHours.Visible = false;
        //            //    txtBasePrice.Visible = false;
        //            //    txtDiscount.Visible = false;
        //            //    txtServiceDate.Visible = false;
        //            //    gcServiceCharges.Visible = false;
        //            //    gvServiceCharges.FooterRow.Visible = false;
        //            //}
        //            //else
        //            //{
        //            //    List<PDMS_PaidServiceInvoice> SOIs = new BDMS_Service().GetPaidServiceQuotation(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
        //            //    if (SOIs.Count == 1)
        //            //    {
        //                    // btnGenerateQuotation.Visible = false; 
        //               // }
        //           // }
        //    //    }
        //    //}
        //    //else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.REPI)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.RCommission)
        //    //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.RWarranty))
        //    //{
        //    //    gvServiceCharges.Columns[8].Visible = false;
        //    //    gvServiceCharges.Columns[9].Visible = false;
        //    //    gvServiceCharges.Columns[10].Visible = false;
        //    //    gvServiceCharges.Columns[11].Visible = false;

        //        //btnGenerateQuotation.Visible = false;
        //        //btnGenerateProfarmaInvoice.Visible = false;
        //        //btnGenerateInvoice.Visible = false;
        //        //btnRequestForClaim.Visible = false;
        //    //}
        //    //else
        //    //{
        //    //    gvServiceCharges.Columns[9].Visible = false;
        //    //    gvServiceCharges.Columns[10].Visible = false;
        //    //    gvServiceCharges.Columns[11].Visible = false;

        //        //btnGenerateQuotation.Visible = false;
        //        //btnGenerateProfarmaInvoice.Visible = false;
        //        //btnGenerateInvoice.Visible = false;
        //        //btnRequestForClaim.Visible = true;
        //        //if (!string.IsNullOrEmpty(ClaimNumber))
        //        //{
        //        //    gcServiceCharges.Visible = false;
        //        //    gvServiceCharges.FooterRow.Visible = false;
        //            // btnRequestForClaim.Visible = false;
        //       // }
        //   // }
        //}

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Decline Approve")
            {
                lblMessage.Visible = true;
                if (new BDMS_ICTicket().ApproveOrDeclineICTicketReqDecline(SDMS_ICTicket.ICTicketID, true))
                {
                    lblMessage.Text = "This IC ticket declined";
                    lblMessage.ForeColor = Color.Green;
                    FillICTicket(SDMS_ICTicket.ICTicketID);
                }
                else
                {
                    lblMessage.Text = "Contact administrator";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else if (lbActions.Text == "Decline Reject")
            {
                lblMessage.Visible = true;
                if (new BDMS_ICTicket().ApproveOrDeclineICTicketReqDecline(SDMS_ICTicket.ICTicketID, false))
                {
                    lblMessage.Text = "This IC ticket reopened again";
                    lblMessage.ForeColor = Color.Green;
                    FillICTicket(SDMS_ICTicket.ICTicketID);
                }
                else
                {
                    lblMessage.Text = "Contact administrator ";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else if (lbActions.Text == "Add Technician")
            {
                MPE_AddTechnician.Show();
                UC_ICTicketAddTechnician.FillMaster(SDMS_ICTicket.Dealer.DealerID);
            }
            else if (lbActions.Text == "Edit Call Information")
            {
                //foreach (PDMS_ServiceCharge SC in SDMS_ICTicket.ServiceCharges)
                //{
                //    if (!SC.Material.IsMainServiceMaterial)
                //    {
                //        lblMessage.Visible = true;
                //        lblMessage.Text = "Remove Service Charge then save the call information.";
                //        return;
                //    }
                //} 
                //foreach (PDMS_ServiceMaterial M in SS_ServiceMaterial)
                //{
                //    lblMessage.Visible = true;
                //    lblMessage.Text = "Remove Material Charge then save the call information.";
                //    return;
                //}
                 
                UC_ICTicketUpdateCallInformation.FillMaster(SDMS_ICTicket, SS_ServiceMaterial);
                MPE_CallInformation.Show();
            }
            else if (lbActions.Text == "Edit FSR")
            {
                UC_AddFSR.FillMaster(SDMS_ICTicketFSR);
                MPE_AddFSR.Show();
            }
            else if (lbActions.Text == "Add FSR Attachments")
            {
                UC_AddFSRAttachments.FillMaster(SDMS_ICTicket);
                MPE_AddFSRAttachments.Show();
            }
            else if (lbActions.Text == "Add Other Machine")
            {
                UC_ICTicketAddOtherMachine.FillMaster();
                MPE_ICTicketAddOtherMachine.Show();
            }
            else if (lbActions.Text == "Add Service Charges")
            {
                PDMS_ServiceCharge ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false)[0];

                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                if (SDMS_ICTicket.ServiceCharges.Count != 0)
                {
                    string Message = ServiceChargeActionControl(ServiceCharge);
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                }

                UC_ICTicketAddServiceCharges.FillMaster(SDMS_ICTicket);
                MPE_ICTicketAddServiceCharges.Show();
            }
            else if (lbActions.Text == "Add TSIR")
            {
                var productCodes = (from p1 in SDMS_ICTicket.ServiceCharges select new { p1.ServiceChargeID, p1.Material.MaterialCode, p1.Material.IsMainServiceMaterial, p1.Material.MaterialGroup }).Where(m => m.IsMainServiceMaterial == false && m.MaterialGroup != "891").Distinct();
                if (productCodes.Count() == 0)
                {
                    lblMessage.Text = "Please enter the service code";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                UC_AddTSIR.FillMaster(SDMS_ICTicket);
                MPE_AddTSIR.Show();
            }
            else if (lbActions.Text == "Add Material Charges")
            {
                UC_ICTicketAddMaterialCharges.FillMaster(ICTicketTSIRs);
                MPE_AddMaterialCharges.Show();
            }
            else if (lbActions.Text == "Add Notes")
            {
                UC_ICTicketAddNotes.FillMaster();
                MPE_ICTicketAddNotes.Show();
            }
            else if (lbActions.Text == "Add Technician Work")
            {
                UC_ICTicketAddTechnicianWork.FillMaster(SDMS_TechniciansWD, SDMS_ICTicket);
                MPE_AddTechnicianWork.Show();
            }
            else if (lbActions.Text == "Restore")
            {
                UC_ICTicketUpdateRestore.FillMaster();
                MPE_UpdateRestore.Show();
            }
            else if (lbActions.Text == "Customer Feedback")
            {
                UC_ICTicketCustomerFeedback.FillMaster(CustomerFeedback);
                MPE_CustomerFeedback.Show();
            }
            else if (lbActions.Text == "Service Claim")
            {

                string endPoint = "ICTicket/InsertServiceClaim?ICTicketID=" + SDMS_ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Service Quotation")
            {
                string endPoint = "ICTicket/InsertServiceQuotationOrProformaOrInvoice?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&Type=1"; ;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Service Profarma Invoice")
            {

                string endPoint = "ICTicket/InsertServiceQuotationOrProformaOrInvoice?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&Type=2";
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Service Invoice")
            {
                string endPoint = "ICTicket/InsertServiceQuotationOrProformaOrInvoice?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&Type=3";
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                List<PDMS_PaidServiceInvoice> Invoice = new BDMS_Service().GetPaidServiceInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "", true);

                if ((SDMS_ICTicket.Dealer.IsEInvoice) && (SDMS_ICTicket.Dealer.EInvoiceDate <= Invoice[0].InvoiceDate) && (SDMS_ICTicket.Dealer.ServicePaidEInvoice))
                {
                    new BDMS_EInvoice().GeneratEInvoice(Invoice[0].InvoiceNumber, "PAY");
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Material Claim")
            {
                string endPoint = "ICTicket/InsertMaterialClaim?ICTicketID=" + SDMS_ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Material Quotation")
            {
                string endPoint = "ICTicket/InsertMaterialQuotation?ICTicketID=" + SDMS_ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Request for Decline")
            {
                MPE_RequestForDecline.Show();
                txtDeclineReason.Text = "";
                // FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            //else if (lbActions.Text == "Margin Warranty Change")
            //{ 
            //    MPE_MarginWarrantyChange.Show(); 
            //}
            else if (lbActions.Text == "Request Date Change")
            {
                MPE_RequestDateChange.Show();
            }
            else if (lbActions.Text == "Unlock Ticket")
            {
                string endPoint = "ICTicket/LockOrUnlockTicket?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&IsLock=False";
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Unblock Ticket")
            {
                string endPoint = "ICTicket/UnblockICTicket?ICTicketID=" + SDMS_ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Margin Warranty Request")
            {
                lblMessageMarginWarrantyRequest.Text = "";
                lblMessageMarginWarrantyRequest.Visible = false;
                txtMarginRemarkRequest.Text = "";
                MPE_MarginWarrantyRequest.Show();
            }

            else if (lbActions.Text == "Margin Warranty Approve")
            {
                if (new BDMS_ICTicket().ApproveOrRejectMarginWarrantyChange(SDMS_ICTicket.ICTicketID, PSession.User.UserID, true, null))
                {
                    lblMessage.Text = "Margin Warrranty approved.";
                    lblMessage.ForeColor = Color.Green;
                    FillICTicket(SDMS_ICTicket.ICTicketID);
                }
                else
                {
                    lblMessage.Text = "Margin Warrranty not approved.";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else if (lbActions.Text == "Margin Warranty Reject")
            {
                //if (new BDMS_ICTicket().ApproveOrRejectMarginWarrantyChange(SDMS_ICTicket.ICTicketID, PSession.User.UserID, false, ""))
                //{
                //    lblMessage.Text = "Margin Warrranty rejected.";
                //    lblMessage.ForeColor = Color.Green;
                //    FillICTicket(SDMS_ICTicket.ICTicketID);
                //}
                //else
                //{
                //    lblMessage.Text = "Margin Warrranty not rejected.";
                //    lblMessage.ForeColor = Color.Red;
                //}
                lblMessageMarginWarrantyReject.Text = "";
                lblMessageMarginWarrantyReject.Visible = false;
                txtRejectRemarks.Text = "";
                MPE_MarginWarrantyReject.Show();
            }
            else if (lbActions.Text == "FSR Signature")
            {
                UC_FsrSignature.FillMaster();
                MPE_FsrSignature.Show(); 
            }
           
            else if (lbActions.Text == "Remove Restore Date")
            {
                string endPoint = "ICTicket/UpdateICTicketRemoveRestoreDate?ICTicketID=" + SDMS_ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Departure To Site")
            {
                if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
                {
                    lblMessage.Text = "Please Enable GeoLocation...!";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    return;
                }
                PApiResult Results = new BDMS_ICTicket().InsertICTicketDepartureDate(SDMS_ICTicket.ICTicketID, Convert.ToDecimal(hfLatitude.Value), Convert.ToDecimal(hfLongitude.Value));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.Text == "Reached in Site")
            { 
                txtLocation.Text = "";
                new BDMS_SiteContactPersonDesignation().GetSiteContactPersonDesignationDDL(ddlDesignation, null, null);
                MPE_ReachedSite.Show(); 
            }
            else if (lbActions.Text == "Arrival Back")
            {
                if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
                {
                    lblMessage.Text = "Please Enable GeoLocation...!";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                    return;
                }
                PApiResult Results = new BDMS_ICTicket().InsertICTicketArrivalBackDate(SDMS_ICTicket.ICTicketID, Convert.ToDecimal(hfLatitude.Value), Convert.ToDecimal(hfLongitude.Value));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
        }
        protected void btnSaveAssignSE_Click(object sender, EventArgs e)
        {
            MPE_AddTechnician.Show();
            string Message = UC_ICTicketAddTechnician.ValidationAssignSE();
            lblMessageAssignEngineer.ForeColor = Color.Red;
            lblMessageAssignEngineer.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageAssignEngineer.Text = Message;
                return;
            }
            string endPoint = "ICTicket/TechnicianAddOrRemoveICTicket?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&TechnicianID=" + UC_ICTicketAddTechnician.ReadAssignSE() + "&IsDeleted=false";

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAssignEngineer.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_AddTechnician.Hide();
            tbpCust.ActiveTabIndex = 0;
            // FillTechnicians();
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }
        protected void lbTechnicianDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblUserID = (Label)gvRow.FindControl("lblUserID");
            string endPoint = "ICTicket/TechnicianAddOrRemoveICTicket?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&TechnicianID=" + lblUserID.Text + "&IsDeleted=true";
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            lblMessage.Text = Results.Message;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            ShowMessage(Results);
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }

        protected void btnUpdateFSR_Click(object sender, EventArgs e)
        {
            MPE_AddFSR.Show();
            string Message = UC_AddFSR.Validation();
            lblFSRMessage.ForeColor = Color.Red;
            lblFSRMessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblFSRMessage.Text = Message;
                return;
            }
            PDMS_ICTicketFSR_M Fsr = UC_AddFSR.Read();
            Fsr.FsrID = SDMS_ICTicketFSR.FsrID;
            Fsr.ICTicketID = SDMS_ICTicket.ICTicketID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketFSR/UpdateTicketFSR", Fsr));
            if (Results.Status == PApplication.Failure)
            {
                lblFSRMessage.Text = Results.Message;
                return;
            }
            UC_AddFSR.Clear();
            ShowMessage(Results);
            MPE_AddFSR.Hide();
            tbpCust.ActiveTabIndex = 2;
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }
        protected void lbFSRAttachedFileRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);

            PDMS_FSRAttachedFile_M AttachedFile = new PDMS_FSRAttachedFile_M();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.ICTicketID = SDMS_ICTicket.ICTicketID;
            AttachedFile.IsDeleted = true;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketFSR/AddOrRemoveFSRAttachment", AttachedFile));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "File Removed";
            lblMessage.ForeColor = Color.Green;
            fillICTicketAttachedFile();
        }
        protected void btnCallInformation_Click(object sender, EventArgs e)
        {
            MPE_CallInformation.Show();
            string Message = UC_ICTicketUpdateCallInformation.ValidationReached(SDMS_ICTicket);
            lblMessageCallInformation.ForeColor = Color.Red;
            lblMessageCallInformation.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageCallInformation.Text = Message;
                return;
            }
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket/UpdateICTicketServiceCallInfo", UC_ICTicketUpdateCallInformation.Read(SDMS_ICTicket)));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageCallInformation.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_CallInformation.Hide();
            tbpCust.ActiveTabIndex = 1;
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }

        protected void lbAvailabilityOfOtherMachineRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AvailabilityOfOtherMachineID = Convert.ToInt64(gvAvailabilityOfOtherMachine.DataKeys[gvRow.RowIndex].Value);
             

            string endPoint = "ICTicket/AddOrRemoveICTicketOtherMachine?AvailabilityOfOtherMachineID="+AvailabilityOfOtherMachineID 
                +"&ICTicketID=" + SDMS_ICTicket.ICTicketID + "&IsDeleted=true";
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            ShowMessage(Results);  
            FillAvailabilityOfOtherMachine(); 
        }
        protected void btnICTicketAddOtherMachine_Click(object sender, EventArgs e)
        {
            MPE_ICTicketAddOtherMachine.Show();
            string Message = UC_ICTicketAddOtherMachine.Validation();
            lblMessageOtherMachine.ForeColor = Color.Red;
            lblMessageOtherMachine.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageOtherMachine.Text = Message;
                return;
            }
            PDMS_AvailabilityOfOtherMachine OM = UC_ICTicketAddOtherMachine.Read();
            string endPoint = "ICTicket/AddOrRemoveICTicketOtherMachine?AvailabilityOfOtherMachineID=0&ICTicketID=" + SDMS_ICTicket.ICTicketID
                + "&TypeOfMachineID=" + OM.TypeOfMachine.TypeOfMachineID + "&Quantity=" + OM.Quantity + "&MakeID=" + OM.Make.MakeID + "&IsDeleted=false";
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageOtherMachine.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_ICTicketAddOtherMachine.Hide();
            tbpCust.ActiveTabIndex = 3;
            FillAvailabilityOfOtherMachine();
        }
        
        protected void btnUpdateFSRAttachments_Click(object sender, EventArgs e)
        {
            lblMessageFsrAttachments.Visible = true;
            lblMessageFsrAttachments.ForeColor = Color.Red;
            MPE_AddFSRAttachments.Show();
            PDMS_FSRAttachedFile_M AttachedFile = UC_AddFSRAttachments.Read();
            AttachedFile.AttachedFileID = 0;
            AttachedFile.IsDeleted = false;
            AttachedFile.ICTicketID = SDMS_ICTicket.ICTicketID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketFSR/AddOrRemoveFSRAttachment", AttachedFile));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFsrAttachments.Text = Results.Message;
                return;
            }

            //if (new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            //{
            //    lblFSRAttachmentMessage.Visible = true;
            //    lblFSRAttachmentMessage.Text = "File Added";
            //    lblFSRAttachmentMessage.ForeColor = Color.Green;
            //    return;
            //}

            lblMessage.Visible = true;
            lblMessage.Text = "File is  Added";
            lblMessage.ForeColor = Color.Green;

            MPE_AddFSRAttachments.Hide();
            fillICTicketAttachedFile();
        }

        protected void lblServiceRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long ServiceChargeID = Convert.ToInt64(gvServiceCharges.DataKeys[gvRow.RowIndex].Value);
            string endPoint = "ICTicket/ICTicketServiceChargesRemove?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&ServiceChargeID=" + ServiceChargeID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            lblMessage.Text = Results.Message;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            ShowMessage(Results);
            tbpCust.ActiveTabIndex = 3;
            //FillServiceCharges();
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }
        protected void btnICTicketAddServiceCharges_Click(object sender, EventArgs e)
        {
            MPE_ICTicketAddServiceCharges.Show();
            string Message = UC_ICTicketAddServiceCharges.Validation();
            lblServiceChargeSessage.ForeColor = Color.Red;
            lblServiceChargeSessage.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblServiceChargeSessage.Text = Message;
                return;
            }
            PDMS_ServiceCharge_API OM = UC_ICTicketAddServiceCharges.Read();
            OM.ICTicketID = SDMS_ICTicket.ICTicketID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket/ICTicketServiceChargesAdd", OM));
            if (Results.Status == PApplication.Failure)
            {
                lblServiceChargeSessage.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_ICTicketAddServiceCharges.Hide();
            tbpCust.ActiveTabIndex = 3;
            //FillServiceCharges();
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }

        public void FillTSIRDetails()
        {
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            gvTSIR.DataSource = ICTicketTSIRs;
            gvTSIR.DataBind();
            //List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            //if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.TsirCancel).Count() == 1)
            //{
            //    for (int i = 0; i < gvTSIR.Rows.Count; i++)
            //    {
            //        LinkButton lblCancelTSIR = (LinkButton)gvTSIR.Rows[i].FindControl("lblCancelTSIR");
            //        lblCancelTSIR.Visible = true;
            //    }
            //}           
        }

        protected void cbCheck_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            CheckBox cbCheck = (CheckBox)gvTSIR.Rows[gvRow.RowIndex].FindControl("cbCheck");
            if (cbCheck.Checked)
            {

                for (int i = 0; i < gvTSIR.Rows.Count; i++)
                {
                    if (i != gvRow.RowIndex)
                    {
                        CheckBox cbChecki = (CheckBox)gvTSIR.Rows[i].FindControl("cbCheck");
                        cbChecki.Checked = false;
                    }
                }
                long TsirID = Convert.ToInt64(gvTSIR.DataKeys[gvRow.RowIndex].Value);
                PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
                if (!((TSIR.Status.StatusID == (short)TSIRStatus.Requested) || (TSIR.Status.StatusID == (short)TSIRStatus.SendBack) || (TSIR.Status.StatusID == (short)TSIRStatus.Rerequested)))
                {
                    cbCheck.Checked = false;
                    lblMessage.Text = "You cannot edit this TSIR. It may be Checked or Approved or Rejected";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Red;
                    // ClearTSIR();
                    return;
                }
                ICTicketTSIR = new PDMS_ICTicketTSIR();
                ICTicketTSIR.ICTicket = new PDMS_ICTicket { ICTicketID = SDMS_ICTicket.ICTicketID };
                ICTicketTSIR.TsirID = TsirID;

                //txtNatureOfFailures.Text = TSIR.NatureOfFailures;
                //txtProblemNoticedBy.Text = TSIR.ProblemNoticedBy;
                //txtUnderWhatConditionFailureTaken.Text = TSIR.UnderWhatConditionFailureTaken;
                //txtFailureDetails.Text = TSIR.FailureDetails;
                //txtPointsChecked.Text = TSIR.PointsChecked;
                //txtPossibleRootCauses.Text = TSIR.PossibleRootCauses;
                //txtSpecificPointsNoticed.Text = TSIR.SpecificPointsNoticed;
                //txtPartsInvoiceNumber.Text = TSIR.PartsInvoiceNumber;
                ViewState["TsirID"] = TSIR.TsirID;
            }
            else
            {
                //  ClearTSIR();

            }
        }

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    if (!Validation())
        //    {
        //        return;
        //    }
        //    ICTicketTSIR.TsirID = ViewState["TsirID"] == null ? 0 : (long)ViewState["TsirID"];
        //    long ServiceChargeID = Convert.ToInt64(ddlServiceChargeID.SelectedValue);
        //    if (ICTicketTSIR.TsirID == 0)
        //    {
        //        foreach (PDMS_ICTicketTSIR TSIR in ICTicketTSIRs)
        //        {
        //            if ((TSIR.ServiceCharge.Material.MaterialCode == ddlServiceChargeID.SelectedItem.Text) && (TSIR.Status.StatusID != (short)TSIRStatus.Canceled))
        //            {
        //                lblMessage.Text = "TSIR already Created for " + ddlServiceChargeID.SelectedItem.Text + " Service Code";
        //                lblMessage.ForeColor = Color.Red;
        //                return;
        //            }
        //        }
        //    }
        //    ICTicketTSIR.ICTicket = new PDMS_ICTicket();
        //    ICTicketTSIR.ICTicket.ICTicketID = SDMS_ICTicket.ICTicketID;
        //    ICTicketTSIR.ServiceCharge = new PDMS_ServiceCharge();
        //    ICTicketTSIR.ServiceCharge.ServiceChargeID = ServiceChargeID;
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
        //        ClearTSIR(); 
        //        FillTSIRDetails();
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR is not updated successfully";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //public void FillSROCoder()
        //{ 
        //    var productCodes = (from p1 in SS_ServiceCharge select new { p1.ServiceChargeID, p1.Material.MaterialCode, p1.Material.IsMainServiceMaterial, p1.Material.MaterialGroup }).Where(m => m.IsMainServiceMaterial == false && m.MaterialGroup != "891").Distinct();

        //    ddlServiceChargeID.DataSource = productCodes;
        //    ddlServiceChargeID.DataBind();
        //} 

        //void ClearTSIR()
        //{ 
        //    txtNatureOfFailures.Text = ""; 
        //    txtProblemNoticedBy.Text = "";
        //    txtUnderWhatConditionFailureTaken.Text = "";
        //    txtFailureDetails.Text = "";
        //    txtPointsChecked.Text = "";
        //    txtPossibleRootCauses.Text = "";
        //    txtSpecificPointsNoticed.Text = "";
        //    txtPartsInvoiceNumber.Text = ""; 
        //    ViewState["TsirID"] = null; 
        //    List<PDMS_TSIRAttachedFile> UploadedFile = new List<PDMS_TSIRAttachedFile>(); 
        //}

        //Boolean Validation()
        //{
        //    lblMessageTSIR.Visible = true;
        //    string Message = "";
        //    Boolean Ret = true; 

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
        //    lblMessageTSIR.Text = Message;
        //    return Ret;
        //}
        private PDMS_TSIRAttachedFile__M CreateUploadedFileFSR(HttpPostedFile file)
        {

            PDMS_TSIRAttachedFile__M AttachedFile = new PDMS_TSIRAttachedFile__M();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);
            AttachedFile.FileName = name;

            AttachedFile.FileType = file.ContentType;

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.FileSize = size;
            AttachedFile.AttachedFileID = 0;
            AttachedFile.IsDeleted = false;
            AttachedFile.ICTicketID = SDMS_ICTicket.ICTicketID;
            return AttachedFile;
        }

        protected void gvAttachedFileNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlFSRAttachedName = (DropDownList)e.Row.FindControl("ddlFSRAttachedName");
                ddlFSRAttachedName.DataTextField = "FSRAttachedName";
                ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
                ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, null, true);
                ddlFSRAttachedName.DataBind();
            }
        }

        protected void gvTSIR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string TsirID = Convert.ToString(gvTSIR.DataKeys[e.Row.RowIndex].Value);
                    GridView gvAttachedFile = (GridView)e.Row.FindControl("gvAttachedFile");
                    Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                    LinkButton lblCancelTSIR = (LinkButton)e.Row.FindControl("lblCancelTSIR");
                    //if (Convert.ToInt32(lblStatusID.Text) == (short)TSIRStatus.Canceled)
                    //{
                    //    lblCancelTSIR.Visible = false;
                    //}
                    List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, Convert.ToInt64(TsirID), null);
                    gvAttachedFile.DataSource = UploadedFile;
                    gvAttachedFile.DataBind();
                    DropDownList ddlFSRAttachedName = (DropDownList)e.Row.FindControl("ddlFSRAttachedName");
                    ddlFSRAttachedName.DataTextField = "FSRAttachedName";
                    ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
                    ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, null, true);
                    ddlFSRAttachedName.DataBind();
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIR", "gvTSIR_RowDataBound", ex);
                throw ex;
            }
        }

        protected void lblAttachedFileAddR_Click(object sender, EventArgs e)
        {
            LinkButton lblAttachedFileAdd = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lblAttachedFileAdd.NamingContainer;
            int index = row.RowIndex;
            DropDownList ddlFSRAttachedName = (DropDownList)gvTSIR.Rows[index].FindControl("ddlFSRAttachedName");
            GridView gvAF = (GridView)gvTSIR.Rows[index].FindControl("gvAttachedFile");
            lblMessage.Visible = true;
            PDMS_TSIRAttachedFile__M AttachedFile = new PDMS_TSIRAttachedFile__M();
            FileUpload fu = (FileUpload)gvTSIR.Rows[index].FindControl("fu");
            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string ext = System.IO.Path.GetExtension(fu.PostedFile.FileName).ToLower();
            List<string> Extension = new List<string>();
            Extension.Add(".jpg");
            Extension.Add(".png");
            Extension.Add(".gif");
            Extension.Add(".jpeg");
            if (!Extension.Contains(ext))
            {
                lblMessage.Text = "Please choose only .jpg, .png and .gif image types!";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            AttachedFile = CreateUploadedFileFSR(fu.PostedFile);
            AttachedFile.TsirID = Convert.ToInt64(gvTSIR.DataKeys[index].Value);
            AttachedFile.FSRAttachedName = new PDMS_FSRAttachedName() { FSRAttachedFileNameID = Convert.ToInt32(ddlFSRAttachedName.SelectedValue) };
            for (int i = 0; i < gvAF.Rows.Count; i++)
            {
                if (ddlFSRAttachedName.SelectedItem.Text == AttachedFile.FileName)
                {
                    lblMessage.Text = "This file already available";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketTsir/AddOrRemoveTsirAttachment", AttachedFile));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "File added";
            lblMessage.ForeColor = Color.Green;
            try
            {
                List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, AttachedFile.TsirID, null);
                gvAF.DataSource = UploadedFile;
                gvAF.DataBind();
            }
            catch (Exception ex)
            { }

            //if (new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            //{
            //    lblMessage.Text = "File added";
            //    lblMessage.ForeColor = Color.Green;
            //    try
            //    {
            //        List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, AttachedFile.TSIR.TsirID, null);
            //        gvAF.DataSource = UploadedFile;
            //        gvAF.DataBind();
            //    }
            //    catch (Exception ex)
            //    { }
            //}
            //else
            //{
            //    lblMessage.Text = "File is not added";
            //    lblMessage.ForeColor = Color.Red;
            //}
             
        }
        protected void lblAttachedFileRemoveR_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView Parentgrid = (GridView)(gvRow.Parent.Parent);
            long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

            GridViewRow GParentrow = (GridViewRow)(Parentgrid.NamingContainer);
            int GParentRowIndex = GParentrow.RowIndex;

            PDMS_TSIRAttachedFile__M AttachedFile = new PDMS_TSIRAttachedFile__M();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.ICTicketID = SDMS_ICTicket.ICTicketID;
            AttachedFile.TsirID = ICTicketTSIR.TsirID;
            AttachedFile.IsDeleted = true;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketTsir/AddOrRemoveTsirAttachment", AttachedFile));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "File Removed";
            lblMessage.ForeColor = Color.Green;
            try
            {
                List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, Convert.ToInt64(gvTSIR.DataKeys[GParentRowIndex].Value), null);
                Parentgrid.DataSource = UploadedFile;
                Parentgrid.DataBind();
            }
            catch (Exception ex)
            { }
        }
        protected void lnkDownloadR_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;
                GridView Parentgrid = (GridView)(gvRow.NamingContainer);


                long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

                PAttachedFile UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileForDownload(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName.Replace(",", " "));
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }
             
        public void FillServiceMaterial()
        {

            txtCustomerPayPercentage.Text = Convert.ToString(SDMS_ICTicket.CustomerPayPercentage);
            txtDealerPayPercentage.Text = Convert.ToString(SDMS_ICTicket.DealerPayPercentage);
            txtAEPayPercentage.Text = Convert.ToString(SDMS_ICTicket.AEPayPercentage);

            if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
              || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty))
            {
                divWarrantyDistribution.Visible = true;
            }
            else
            {
                divWarrantyDistribution.Visible = false;
            }
            if (SS_ServiceMaterial.Count != 0)
            {
                btnSaveWarrantyDistribution.Visible = false;
            }

            gvMaterial.DataSource = SS_ServiceMaterialAll;
                if (SS_ServiceMaterial.Count != 0)
                {
                    btnSaveWarrantyDistribution.Visible = false;
                }
 
            gvMaterial.DataBind(); 
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

        protected void lblCancelTSIR_Click(object sender, EventArgs e)
        { 
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long TsirID = Convert.ToInt64(gvTSIR.DataKeys[gvRow.RowIndex].Value);

            string endPoint = "ICTicketTsir/CancelICTicketTSIR?TsirID=" + TsirID + "&ICTicketID=" + SDMS_ICTicket.ICTicketID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAssignEngineer.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            tbpCust.ActiveTabIndex = 3;
            FillTSIRDetails();
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
            PDMS_ICTicketTSIR_API Tist = UC_AddTSIR.Read();
            Tist.ICTicketID = SDMS_ICTicket.ICTicketID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketTsir/InsertOrUpdateICTicketTSIR", Tist));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAddTSIR.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_AddTSIR.Hide();
            tbpCust.ActiveTabIndex = 4; 
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            FillTSIRDetails();
        }

        protected void btnAddMaterialCharges_Click(object sender, EventArgs e)
        {
            MPE_AddMaterialCharges.Show();
            lblMessageMaterialCharges.ForeColor = Color.Red;
            lblMessageMaterialCharges.Visible = true;
            Button lbActions = ((Button)sender);
            if (lbActions.ID == "btnAddMaterialCharges")
            {                
                string Message = UC_ICTicketAddMaterialCharges.Validation();
                lblMessageMaterialCharges.ForeColor = Color.Red;
                lblMessageMaterialCharges.Visible = true;
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageMaterialCharges.Text = Message;
                    return;
                }
                PDMS_ServiceMaterial_API OM = UC_ICTicketAddMaterialCharges.Read();
                OM.ICTicketID = SDMS_ICTicket.ICTicketID;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket/TicketMaterialChargeAdd", OM));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageMaterialCharges.Text = Results.Message;
                    return;
                }
                ShowMessage(Results);                
                tbpCust.ActiveTabIndex = 5;
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else if (lbActions.ID == "btnAddMaterialAvailability")
            { 
                HiddenField hdfMaterialID = (HiddenField)UC_ICTicketAddMaterialCharges.FindControl("hdfMaterialID");
                CheckBox cbSupersedeYN = (CheckBox)UC_ICTicketAddMaterialCharges.FindControl("cbSupersedeYN"); 
                if (string.IsNullOrEmpty(hdfMaterialID.Value))
                {
                    lblMessage.Text = "Please enter the material";
                    return;
                }
                string Material = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(hdfMaterialID.Value), null, null, null, null)[0].MaterialCode;
                string endPoint = "ICTicket/GetMateriAlavailablQty?DealerCode=" + SDMS_ICTicket.Dealer.DealerCode + "&OfficeCode=" + SDMS_ICTicket.DealerOffice.OfficeCode + "&Material=" + Material+ "&SupersedeYN=" + cbSupersedeYN.Checked;
                PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageMaterialCharges.Text = Results.Message; 
                }
                else
                {
                    if (cbSupersedeYN.Checked)
                    {
                        lblMessageMaterialCharges.Text = "Supersede Material  available quantity is "+ Convert.ToString( Results.Data);
                    }
                    else
                    {
                        lblMessageMaterialCharges.Text = "Material  available quantity is " + Convert.ToString(Results.Data);
                    }
                    lblMessageMaterialCharges.ForeColor = Color.Green; 
                }
                return;
            }
            MPE_AddMaterialCharges.Hide();
        }
        
        protected void lblMaterialRemove_Click(object sender, EventArgs e)
        { 
            lblMessage.Visible = true; 
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            long ServiceMaterialID = Convert.ToInt64(gvMaterial.DataKeys[gvRow.RowIndex].Value);

            string endPoint = "ICTicket/TicketMaterialChargeCancel?ServiceMaterialID=" + ServiceMaterialID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)); 
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAssignEngineer.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            tbpCust.ActiveTabIndex = 5;
            FillServiceMaterial();
        }
        protected void cbEdit_CheckedChanged(object sender, EventArgs e)
        { 
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            CheckBox cbEdit = (CheckBox)gvMaterial.Rows[gvRow.RowIndex].FindControl("cbEdit");
            Label lblQuotationNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblQuotationNumber"); 
            Label lblTsirID = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblTsirID");  
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

                ddlMaterialSource.DataTextField = "MaterialSource";
                ddlMaterialSource.DataValueField = "MaterialSourceID";
                ddlMaterialSource.DataSource = new BDMS_Service().GetMaterialSource(null, null);
                ddlMaterialSource.DataBind();
                ddlMaterialSource.Items.Insert(0, new ListItem("Select", "0"));

                lblMaterialSource.Visible = false;
                ddlMaterialSource.Visible = true;

                if (!string.IsNullOrEmpty(lblMaterialSourceID.Text))
                    ddlMaterialSource.SelectedValue = lblMaterialSourceID.Text;

                Label lblTsirNumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblTsirNumber");

                DropDownList ddlTSIRNumber = (DropDownList)gvMaterial.Rows[gvRow.RowIndex].FindControl("ddlTSIRNumber");
                ddlTSIRNumber.DataTextField = "TsirNumber";
                ddlTSIRNumber.DataValueField = "TsirID"; 

                List<PDMS_ICTicketTSIR> ddlTSIR = new List<PDMS_ICTicketTSIR>();
                foreach (PDMS_ICTicketTSIR t in ICTicketTSIRs)
                { 
                    if ((t.Status.StatusID != (short)TSIRStatus.Canceled))
                    {
                        ddlTSIR.Add(new PDMS_ICTicketTSIR() { TsirID = t.TsirID, TsirNumber = t.TsirNumber });
                    } 
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
            PDMS_ServiceMaterial_API ServiceMaterial = new PDMS_ServiceMaterial_API();

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
            Label lblPONumber = (Label)gvMaterial.Rows[gvRow.RowIndex].FindControl("lblPONumber");

            ServiceMaterial.ServiceMaterialID = ServiceMaterialID;
            ServiceMaterial.ICTicketID = SDMS_ICTicket.ICTicketID;
            ServiceMaterial.MaterialSerialNumber = txtMaterialSN.Text.Trim() ;
            ServiceMaterial.Qty = Convert.ToInt32(txtQty.Text.Trim());
            ServiceMaterial.IsFaultyPart = cbIsFaultyPart.Checked;
            ServiceMaterial.DefectiveMaterialSerialNumber = txtDefectiveMaterialSN.Text.Trim();

            ServiceMaterial.IsRecomenedParts = cbRecomenedParts.Checked;
            ServiceMaterial.IsQuotationParts = cbQuotationParts.Checked;

            ServiceMaterial.MaterialSource = ddlMaterialSource.SelectedValue == "0" ? null : new PDMS_MaterialSource() { MaterialSourceID = Convert.ToInt32(ddlMaterialSource.SelectedValue) };
             ;
            ServiceMaterial.PONumber = lblPONumber.Text;
            if (ddlTSIRNumber.SelectedValue != "0")
            {
                ServiceMaterial.TsirID = Convert.ToInt64(ddlTSIRNumber.SelectedValue) ;
                ServiceMaterial.IsRecomenedParts = true;
            }
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket/ICTicketMaterialChargeUpdate", ServiceMaterial));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageAssignEngineer.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            tbpCust.ActiveTabIndex = 5;

            SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
            SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");
            FillServiceMaterial();
        }
        protected void lbEditCancel_Click(object sender, EventArgs e)
        {
            FillServiceMaterial(); 
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

            if (new BDMS_ICTicket().UpdateICTicketWarrantyDistribution(SDMS_ICTicket.ICTicketID, (decimal)CustomerPayPercentage, (decimal)DealerPayPercentage, (decimal)AEPayPercentage))
            {
                lblMessage.Text = "ICTicket is updated successfully";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "ICTicket is not updated successfully";
            }
        }
              
        private void FillServiceNotes()
        {
            List<PDMS_ServiceNote> Note = new BDMS_Service().GetServiceNote(SDMS_ICTicket.ICTicketID, null, null, ""); 
            gvNotes.DataSource = Note;
            gvNotes.DataBind();
        }

        private void FillTechniciansByTicketID()
        {
            SDMS_TechniciansWD = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID); 
            List<PDMS_ServiceTechnicianWorkedDate> WorkedDate = new List<PDMS_ServiceTechnicianWorkedDate>();
            foreach (PDMS_ServiceTechnician t in SDMS_TechniciansWD)
            {
                foreach (PDMS_ServiceTechnicianWorkedDate W in t.ServiceTechnicianWorkedDate)
                {
                    WorkedDate.Add(W);
                }
            }
             
            gvTechnicianWorkDays.DataSource = WorkedDate;
            gvTechnicianWorkDays.DataBind();
        }
          
        protected void lblNoteRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long ServiceNoteID = Convert.ToInt64(gvNotes.DataKeys[gvRow.RowIndex].Value);

            string endPoint = "ICTicket/AddOrRemoveTicketNoticeInfo?ServiceNoteID=" + ServiceNoteID + "&ICTicket=" + SDMS_ICTicket.ICTicketID + "&NoteTypeID=0&IsDeleted=true";

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageNote.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_ICTicketAddNotes.Hide();
            tbpCust.ActiveTabIndex = 7;
            FillServiceNotes(); 
        }
        protected void btnAddNotes_Click(object sender, EventArgs e)
        {
            MPE_ICTicketAddNotes.Show();
            string Message = UC_ICTicketAddNotes.Validation();
            lblMessageNote.ForeColor = Color.Red;
            lblMessageNote.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageNote.Text = Message;
                return;
            }
            string endPoint = "ICTicket/AddOrRemoveTicketNoticeInfo?ServiceNoteID=0&ICTicket=" + SDMS_ICTicket.ICTicketID + UC_ICTicketAddNotes.Read() + "&IsDeleted=false";

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageNote.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_ICTicketAddNotes.Hide();
            tbpCust.ActiveTabIndex = 7;
            FillServiceNotes();
        }

        protected void btnUpdateRestore_Click(object sender, EventArgs e)
        {
            MPE_UpdateRestore.Show();
            string Message = UC_ICTicketUpdateRestore.Validation(SDMS_ICTicket);
            lblMessageRestore.ForeColor = Color.Red;
            lblMessageRestore.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageRestore.Text = Message;
                return;
            }
            string endPoint = "ICTicket/UpdateTicketRestorationInfo?ICTicket=" + SDMS_ICTicket.ICTicketID + "&" + UC_ICTicketUpdateRestore.Read() + "&IsDeleted=false";
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageRestore.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_UpdateRestore.Hide();
            tbpCust.ActiveTabIndex = 9;
            FillICTicket(SDMS_ICTicket.ICTicketID); 
        }


        protected void lbWorkedDayRemove_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            long ServiceTechnicianWorkDateID = Convert.ToInt64(((Label)gvRow.FindControl("lblServiceTechnicianWorkDateID")).Text);
            Label lblWorkedHours = (Label)gvRow.FindControl("lblWorkedHours");
            new BDMS_ICTicket().InsertOrUpdateTechnicianWorkedDateAddOrRemoveICTicket(ServiceTechnicianWorkDateID, SDMS_ICTicket.ICTicketID, null, null, Convert.ToDecimal(lblWorkedHours.Text), true, PSession.User.UserID);
            lblMessage.Text = "Technician Worked Date Removed";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            FillTechniciansByTicketID();
            DropDownList gvddlTechnician = (DropDownList)gvTechnicianWorkDays.FooterRow.FindControl("gvddlTechnician");
        }
        protected void btnAddTechnicianWork_Click(object sender, EventArgs e)
        {
            MPE_AddTechnicianWork.Show();
            string Message = UC_ICTicketAddTechnicianWork.Validation(SDMS_TechniciansWD);
            lblMessageTechnicianWork.ForeColor = Color.Red;
            lblMessageTechnicianWork.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageTechnicianWork.Text = Message;
                return;
            }
            string endPoint = "ICTicket/AddOrRemoveTicketTechnicianWorks?ServiceTechnicianWorkDateID=0&ICTicket=" + SDMS_ICTicket.ICTicketID
                 + UC_ICTicketAddTechnicianWork.Read() + "&IsDeleted=false";
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageTechnicianWork.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_AddTechnicianWork.Hide();
            tbpCust.ActiveTabIndex = 8;
            FillTechniciansByTicketID();
        }


        private void FillRestore()
        {
            lblRestoreDate.Text = SDMS_ICTicket.RestoreDate == null ? "" : ((DateTime)SDMS_ICTicket.RestoreDate).ToString();
            lblArrivalBackDate.Text = SDMS_ICTicket.ArrivalBack == null ? "" : ((DateTime)SDMS_ICTicket.ArrivalBack).ToString();

            if (SDMS_ICTicket.CustomerSatisfactionLevel != null)
                lblCustomerSatisfactionLevel.Text = SDMS_ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel;

            lblCustomerRemarks.Text = SDMS_ICTicketFSR.CustomerRemarks;

            lblComplaintStatus.Text = SDMS_ICTicketFSR.ComplaintStatus;
        }

        //private void FillCustomerFeedBack()
        //{
        //    if (CustomerFeedback != null)
        //    {
        //        if (CustomerFeedback.CustomerSatisfactionLevel != null)
        //            lblCustomerSatisfactionLevel.Text = SDMS_ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel;
        //        lblCustomerRemarks.Text = SDMS_ICTicketFSR.CustomerRemarks;
        //        lbtnPhoto.Text = CustomerFeedback.Photo == null ? "" : CustomerFeedback.Photo.FileName;
        //        lbtnSignature.Text = CustomerFeedback.Signature == null ? "" : CustomerFeedback.Signature.FileName;
        //    }
        //}
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }


        void ActionControlMange()
        {
            int ServiceTypeID = SDMS_ICTicket.ServiceType.ServiceTypeID;

            lbtnAddTechnician.Visible = true;
            lbtnEditCallInformation.Visible = true;
            lbtnEditFSR.Visible = true; 
            lbtnAddFSRAttachments.Visible = true;
            lbtnAddOtherMachine.Visible = true;
            lbtnAddServiceCharges.Visible = true; 
            lbtnAddTSIR.Visible = true;
            lbtnAddMaterialCharges.Visible = true;
            lbtnAddNotes.Visible = true; 
            lbtAddTechnicianWork.Visible = true;
            

            //lbtnCustomerFeedback.Visible = true;
            lbtnServiceClaim.Visible = true;
            lbtnServiceQuotation.Visible = true;
            lbtnServiceProfarmaInvoice.Visible = true;
            lbtnServiceInvoice.Visible = true;
            lbtnMaterialClaim.Visible = true;
            lbtnMaterialQuotation.Visible = true;
            lbtnUnlockTicket.Visible = true;

            lbtnRequestForDecline.Visible = true;
            lbtnDeclineApprove.Visible = true;
            lbtnDeclineReject.Visible = true;

            lbtnMarginWarrantyRequest.Visible = true;
            lbtnMarginWarrantyApprove.Visible = true;
            lbtnMarginWarrantyReject.Visible = true;

            lbtnRequestDateChange.Visible = true;
            lbtnRemoveRestoreDate.Visible = true;
            lbtnFsrSignature.Visible = true;

            lbtnDepartureToSite.Visible = true;
            lbtnReachedInSite.Visible = true;
            lbtnRestore.Visible = true;
            lbtnArrivalBack.Visible = true;

            // lbtnDeviatedICTicketRequest60Days.Visible = true;
            // lbtnDeviatedICTicketRequestCommissioning.Visible = true;


            if ((SDMS_ICTicket.Technicians.Where(A => A.UserID == PSession.User.UserID).Count() == 0)
                && !(PSession.User.Designation.DealerDesignationID==(short)DealerDesignation.BusinessSystemManager)
                && !(PSession.User.Designation.DealerDesignationID == (short)DealerDesignation.BusinessSystemExecutive))
            {
                lbtnDepartureToSite.Visible = false;
                lbtnReachedInSite.Visible = false;
                lbtnRestore.Visible = false;
                lbtnArrivalBack.Visible = false;
            }

            if ((Boolean)SDMS_ICTicket.IsLocked)
            {
                lbtnAddTechnician.Visible = false;
                lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                lbtnAddFSRAttachments.Visible = false;
                lbtnAddOtherMachine.Visible = false;
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false;
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false; 

                //lbtnCustomerFeedback.Visible = false;
                lbtnServiceClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false;
                lbtnMaterialClaim.Visible = false;
                lbtnMaterialQuotation.Visible = false;

                lbtnMarginWarrantyRequest.Visible = false;
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;

                lbtnRequestDateChange.Visible = false;
                lbtnAddNotes.Visible = false;
                lbtnFsrSignature.Visible = false; 
            }
            else
            {
                lbtnUnlockTicket.Visible = false;
            }

            if (!SDMS_ICTicket.SyncBlock)
            {
                lbtnUnblockTicket.Visible = false;
            }

            if (SDMS_ICTicket.DepartureDate == null)
            { 
                lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                lbtnAddFSRAttachments.Visible = false;
                lbtnAddOtherMachine.Visible = false;
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false; 
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false;
                 
                lbtnServiceClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false; 
                lbtnUnlockTicket.Visible = false;

                //lbtnRequestForDecline.Visible = false;
                //lbtnDeclineApprove.Visible = false;
                //lbtnDeclineReject.Visible = false;

               // lbtnMarginWarrantyRequest.Visible = false;
               // lbtnMarginWarrantyApprove.Visible = false;
               // lbtnMarginWarrantyReject.Visible = false;

               // lbtnRequestDateChange.Visible = false;
                lbtnRemoveRestoreDate.Visible = false;

                lbtnFsrSignature.Visible = false;
                 
                lbtnReachedInSite.Visible = false;
                lbtnArrivalBack.Visible = false;
            }
            else
            {
                lbtnDepartureToSite.Visible = false;
            }

            if (SDMS_ICTicket.ReachedDate != null)
            {
                lbtnReachedInSite.Visible = false;
            }
            if (SDMS_ICTicket.RestoreDate != null)
            {
                lbtnRestore.Visible = false;
            }
            if ((SDMS_ICTicket.ArrivalBack != null) || (SDMS_ICTicket.ServiceStatus.ServiceStatusID != (short)DMS_ServiceStatus.Restored))
            {
                lbtnArrivalBack.Visible = false;
            } 


            if (SDMS_ICTicket.IsMarginWarranty) 
            {
                lbtnMarginWarrantyRequest.Visible = false;
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;
            }

            if (!SDMS_ICTicket.ServiceType.IsMaterialRequired)
            {
                lbtnAddMaterialCharges.Visible = false;
            }

            if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Open) || (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reopen))
            {
                lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                lbtnAddFSRAttachments.Visible = false;
                lbtnAddOtherMachine.Visible = false;
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false;
                // lbtnAddNotes.Visible = false;
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false;

                //lbtnCustomerFeedback.Visible = false;
                lbtnServiceClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false; 
                lbtnDepartureToSite.Visible = false;
            }
            else if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.TechnicianAssigned)
            {    
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false; 
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false;

                //lbtnCustomerFeedback.Visible = false;
                lbtnServiceClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false;  
                 
                lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                lbtnAddFSRAttachments.Visible = false;
                lbtnAddOtherMachine.Visible = false;  

                lbtnRequestForDecline.Visible = false;
                lbtnDeclineApprove.Visible = false;
                lbtnDeclineReject.Visible = false;

                lbtnMarginWarrantyRequest.Visible = false;
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;

                lbtnRequestDateChange.Visible = false;
                lbtnRemoveRestoreDate.Visible = false;
                lbtnFsrSignature.Visible = false;                 
                lbtnArrivalBack.Visible = false;
            }
            else if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reached)
            {  
                //lbtnCustomerFeedback.Visible = false;
                lbtnServiceClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false; 
                lbtnRequestForDecline.Visible = false;
            }
            else if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Restored)
            { 
                lbtnRequestForDecline.Visible = false;

                lbtnAddTechnician.Visible = false;
                // lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                if (
                    (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
              || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
              || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
              || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
              ) 
                {

                }
                else
                {
                   // lbtnAddServiceCharges.Visible = false;
                }
                // lbtnAddTSIR.Visible = false;
                //lbtnAddMaterialCharges.Visible = false;
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false;
                lbtnRequestForDecline.Visible = false;

                lbtnMarginWarrantyRequest.Visible = false;
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;

                lbtnRequestDateChange.Visible = false;
                lbtnFsrSignature.Visible = false;
            }
            else if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Declined) || (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.ReqDeclined))
            {
                lbtnAddTechnician.Visible = false;
                lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                lbtnAddFSRAttachments.Visible = false;
                lbtnAddOtherMachine.Visible = false;
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false;
                //lbtnAddNotes.Visible = true; 
                lbtAddTechnicianWork.Visible = false;
                lbtnDepartureToSite.Visible = false;
                lbtnRestore.Visible = false;

                //lbtnCustomerFeedback.Visible = false;
                lbtnServiceClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false; 
                lbtnRequestForDecline.Visible = false;
                lbtnMarginWarrantyRequest.Visible = false;
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;
                lbtnRequestDateChange.Visible = false;


            }

            if (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
            {

            }

            if ((ServiceTypeID == (short)DMS_ServiceType.NEPI) || (ServiceTypeID == (short)DMS_ServiceType.Commission) || (ServiceTypeID == (short)DMS_ServiceType.PreCommission))
            {
                lbtnAddMaterialCharges.Visible = false; 
            }

            if ((SDMS_ICTicket.ServiceType == null) || (SDMS_ICTicket.DealerOffice == null) || (SDMS_ICTicket.CurrentHMRDate == null) || (SDMS_ICTicket.CurrentHMRValue == null))
            {
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false; 
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false;
            }

            if ((ServiceTypeID == (short)DMS_ServiceType.Paid1) || (ServiceTypeID == (short)DMS_ServiceType.Others)
              || (ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            {
                lbtnAddMaterialCharges.Visible = false;
                lbtnServiceClaim.Visible = false;  
            }
            else
            { 
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false; 
            }

            foreach (PDMS_ServiceCharge SC in SDMS_ICTicket.ServiceCharges)
            {
                //if (!SC.Material.IsMainServiceMaterial)
                //{
                //    lbtnEditCallInformation.Visible = false;
                //}
                if (!string.IsNullOrEmpty(SC.ClaimNumber))
                {
                    lbtnServiceClaim.Visible = false;
                    lbtnAddServiceCharges.Visible = false;

                    lbtnAddTechnician.Visible = false;
                    
                    lbtnEditFSR.Visible = false; 
                    lbtnAddTSIR.Visible = false;
                    lbtnAddMaterialCharges.Visible = false;
                    lbtAddTechnicianWork.Visible = false;
                    lbtnRestore.Visible = false;

                    lbtnRequestForDecline.Visible = false;
                    lbtnMarginWarrantyRequest.Visible = false;
                    lbtnMarginWarrantyApprove.Visible = false;
                    lbtnMarginWarrantyReject.Visible = false;
                    lbtnRequestDateChange.Visible = false;
                }
                if (!string.IsNullOrEmpty(SC.QuotationNumber))
                {
                    lbtnServiceQuotation.Visible = false;
                    lbtnAddServiceCharges.Visible = false;
                }
                if (!string.IsNullOrEmpty(SC.ProformaInvoiceNumber))
                {
                    lbtnServiceQuotation.Visible = false;
                    lbtnServiceProfarmaInvoice.Visible = false;
                    lbtnAddServiceCharges.Visible = false;
                }
                if (!string.IsNullOrEmpty(SC.InvoiceNumber))
                {
                    lbtnServiceQuotation.Visible = false;
                    lbtnServiceProfarmaInvoice.Visible = false;
                    lbtnServiceInvoice.Visible = false;
                    lbtnAddServiceCharges.Visible = false;

                    lbtnAddTechnician.Visible = false; 
                    lbtnEditFSR.Visible = false;
                    lbtnAddServiceCharges.Visible = false;
                    lbtnAddTSIR.Visible = false;
                    lbtnAddMaterialCharges.Visible = false; 
                    lbtAddTechnicianWork.Visible = false;
                    lbtnRestore.Visible = false;

                    lbtnRequestForDecline.Visible = false;
                    lbtnMarginWarrantyRequest.Visible = false;
                    lbtnMarginWarrantyApprove.Visible = false;
                    lbtnMarginWarrantyReject.Visible = false;
                    lbtnRequestDateChange.Visible = false;

                }
            }

            //foreach (PDMS_ServiceMaterial M in SS_ServiceMaterial)
            //{
            //    lbtnEditCallInformation.Visible = false;
            //}


            if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID != (short)DMS_ServiceStatus.ReqDeclined))
            {
                lbtnDeclineApprove.Visible = false;
                lbtnDeclineReject.Visible = false;
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.RequestForDecline).Count() == 0)
            {
                lbtnRequestForDecline.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.MarginWarrantyRequest).Count() == 0)
            {
                lbtnMarginWarrantyRequest.Visible = false;
            }

  

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.RequestDateChange).Count() == 0)
            {
                lbtnRequestDateChange.Visible = false;
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.AddServiceEngineer).Count() == 0)
            {
                lbtnAddTechnician.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EditCall_InfoFSR_TSIR_Restore).Count() == 0)
            {
                lbtnEditCallInformation.Visible = false;
                lbtnEditFSR.Visible = false;
                lbtnAddFSRAttachments.Visible = false;
                lbtnAddOtherMachine.Visible = false;
                lbtnAddServiceCharges.Visible = false;
                lbtnAddTSIR.Visible = false;
                lbtnAddMaterialCharges.Visible = false;
                lbtnMaterialQuotation.Visible = false;
                lbtAddTechnicianWork.Visible = false;
                lbtnRestore.Visible = false;
                lbtnFsrSignature.Visible = false;

                //lbtnCustomerFeedback.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.RequstForClaimAndInvoice).Count() == 0)
            {
                lbtnServiceClaim.Visible = false;
                lbtnMaterialClaim.Visible = false;
                lbtnServiceQuotation.Visible = false;
                lbtnServiceProfarmaInvoice.Visible = false;
                lbtnServiceInvoice.Visible = false;
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ICTicketUnlock).Count() == 0)
            {
                lbtnUnlockTicket.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ICTicketUnblock).Count() == 0)
            {
                lbtnUnblockTicket.Visible = false;
            }


            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ICTicketDeclineApprove).Count() == 0)
            {
                lbtnDeclineApprove.Visible = false;
                lbtnDeclineReject.Visible = false;
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ICTicketRemoveRestoreDate).Count() == 0)
            {
                lbtnRemoveRestoreDate.Visible = false; 
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.MarginWarrantyApproval).Count() == 0)
            {
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;
            }

            Boolean MaterialQuotation = true;
            Boolean MaterialClaim = true;
            foreach (PDMS_ServiceMaterial mm in SS_ServiceMaterial)
            {
                if(string.IsNullOrEmpty( mm.QuotationNumber) && mm.IsQuotationParts ==true)
                {
                    MaterialQuotation = false;
                }
                if (string.IsNullOrEmpty(mm.ClaimNumber) && mm.IsQuotationParts == true)
                {
                    MaterialClaim = false;
                }
            } 
            if (MaterialQuotation)
            {
                lbtnMaterialQuotation.Visible = false;
                
            }
            if (MaterialClaim)
            {
                lbtnMaterialClaim.Visible = false;
                lbtnMaterialQuotation.Visible = false;
            }

            if (MarginWarrantyReq.Rows.Count ==0)
            {
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;
            }
            HttpContext.Current.Session["ServiceTypeID"] =   SDMS_ICTicket.ServiceType.ServiceTypeID;

            if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID != (short)DMS_ServiceStatus.Restored))
            {
                lbtnRemoveRestoreDate.Visible = false;
            }

            // Validation Based on Sub Module Permission
            if ((PSession.User.DMSModules.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).Count() != 0))
            {
                List<PSubModuleAccess> sub = PSession.User.DMSModules.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;
                if ((sub.Where(m => m.SubModuleMasterID == (short)SubModule.ViewService_ICTicketMarginWarrantyApproval).Count() == 0))
                {
                    lbtnMarginWarrantyApprove.Visible = false;
                    lbtnMarginWarrantyReject.Visible = false;
                }
            }
            else
            {
                lbtnMarginWarrantyApprove.Visible = false;
                lbtnMarginWarrantyReject.Visible = false;
            }

            if (SDMS_ICTicketFSR.FsrID == 0)
            {
                lbtnFsrSignature.Visible = false;
            }
            PDMS_ICTicketFSRSignature FSRSignature = new BDMS_ICTicketFSR().GetICTicketFSRSignatureByFsrID(SDMS_ICTicketFSR.FsrID);
            if (FSRSignature.FSRSignatureID != 0)
            { 
                lbtnFsrSignature.Visible = false;
            }
            
            ControlBaseOn60Days();

            DisableAllGridEditDelete();
        }

        protected void lnkFSRDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);

                Label lblFileName = (Label)gvRow.FindControl("lblFileName");
                Label lblFileType = (Label)gvRow.FindControl("lblFileType");

                PAttachedFile UploadedFile = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(AttachedFileID);

                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblFileName.Text);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }
        }

        protected void btnUpdateCustomerFeedback_Click(object sender, EventArgs e)
        {
            MPE_CustomerFeedback.Show();
            string Message = "";
            Message = UC_ICTicketCustomerFeedback.Validation();
            lblMessageCustomerFeedback.ForeColor = Color.Red;
            lblMessageCustomerFeedback.Visible = true;
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageCustomerFeedback.Text = Message;
                return;
            }
            PICTicketCustomerFeedback Feedback = UC_ICTicketCustomerFeedback.Read();
            Feedback.ICTicketID = SDMS_ICTicket.ICTicketID;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket/CustomerFeedback", Feedback));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageCustomerFeedback.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_CustomerFeedback.Hide();
            tbpCust.ActiveTabIndex = 4;
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            FillTSIRDetails();
        }

        //protected void lbtnPhoto_Click(object sender, EventArgs e)
        //{  

        //    Response.AddHeader("Content-type", "image/jpeg");
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + lbtnPhoto.Text + ".jpg");
        //    HttpContext.Current.Response.Charset = "utf-16";
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //    Response.BinaryWrite(CustomerFeedback.Photo.AttachedFile);
        //    Response.Flush();
        //    Response.End();
        //} 
        //protected void lbtnSignature_Click(object sender, EventArgs e)
        //{
        //    Response.AddHeader("Content-type", "image/jpeg");
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + lbtnSignature.Text+".jpg");
        //    HttpContext.Current.Response.Charset = "utf-16";
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //    Response.BinaryWrite(CustomerFeedback.Signature.AttachedFile);
        //    Response.Flush();
        //    Response.End();
        //}

        protected void btnSaveRequestForDecline_Click(object sender, EventArgs e)
        {
            MPE_RequestForDecline.Show();
            Boolean Success = new BDMS_ICTicket().UpdateICTicketDecline(SDMS_ICTicket.ICTicketID, txtDeclineReason.Text, PSession.User.UserID);
            if (!Success)
            {
                lblMessageCustomerFeedback.Text = "";
                return;
            }
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            FillICTicket(SDMS_ICTicket.ICTicketID);
            //PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicket/CustomerFeedback", Feedback));
            //if (Results.Status == PApplication.Failure)
            //{
            //    lblMessageCustomerFeedback.Text = Results.Message;
            //    return;
            //}
            //  ShowMessage(Results);
            MPE_RequestForDecline.Hide();
        }

        //protected void btnSaveMarginWarrantyChange_Click(object sender, EventArgs e)
        //{
        //    MPE_MarginWarrantyChange.Show();
        //    //if (cbIsMarginWarranty.Checked == SDMS_ICTicket.IsMarginWarranty)
        //    //{
        //    //    lblMessage.Text = "Please change Margin Warranty";
        //    //    lblMessage.ForeColor = Color.Red;
        //    //    return;
        //    //}

        //    string endPoint = "ICTicket/UpdateICTicketMarginWarranty?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&MarginRemark=" + txtMarginRemark.Text.Trim();
        //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
        //    if (Results.Status == PApplication.Failure)
        //    {
        //        lblMessageMarginWarrantyChange.Text = Results.Message;
        //        return;
        //    }
        //    ShowMessage(Results);
        //    MPE_MarginWarrantyChange.Hide();
        //    FillICTicket(SDMS_ICTicket.ICTicketID);
        //}

        protected void btnSaveRequestDateChange_Click(object sender, EventArgs e)
        {
            MPE_RequestDateChange.Show();
            lblMessageRequestDateChange.Visible = true;
            lblMessageRequestDateChange.ForeColor = Color.Red;
            if (string.IsNullOrEmpty(txtRequestedDate.Text.Trim()))
            {
                lblMessageRequestDateChange.Text = "Please enter Requested Date";
                return;
            }
            if (ddlRequestedHH.SelectedValue == "-1")
            {
                lblMessageRequestDateChange.Text = "Please select the Requested Hour";
                return;
            }
            if (ddlRequestedMM.SelectedValue == "0")
            {
                lblMessageRequestDateChange.Text = "Please select the Requested Minute";
                return;
            }
            string RequestedDate = txtRequestedDate.Text.Trim() + " " + ddlRequestedHH.SelectedValue + ":" + ddlRequestedMM.SelectedValue;


            string endPoint = "ICTicket/ChangeICTicketRequestedDate?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&RequestedDate=" + RequestedDate;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessageRequestDateChange.Text = Results.Message;
                return;
            }
            ShowMessage(Results);
            MPE_RequestDateChange.Hide();
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }
        void ControlBaseOn60Days()
        {
            try
            { 
                int Days = Convert.ToInt32(ConfigurationManager.AppSettings["ICTicketLockDate"]);
                if (SDMS_ICTicket.ICTicketDate.AddDays(Days) < DateTime.Now)
                {
                    DataTable ICTicketDT = new BDMS_ICTicket().GetDeviatedICTicketReport(SDMS_ICTicket.Dealer.DealerID, SDMS_ICTicket.ICTicketNumber, 1, null, null, PSession.User.UserID);
                    if (ICTicketDT.Rows.Count != 0)
                    {
                        Boolean c = ICTicketDT.Rows[0]["Approved"] == DBNull.Value ? false : Convert.ToBoolean(ICTicketDT.Rows[0]["Approved"]);
                        if (c)
                        {
                            return;
                        }
                    }
                    lbtnServiceClaim.Visible = false;
                    lbtnMaterialClaim.Visible = false; 
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnReqMarginWarrantyChange_Click(object sender, EventArgs e)
        {
            MPE_MarginWarrantyRequest.Show();
            string endPoint = "ICTicket/InsertMarginWarrantyChangeRequest?ICTicketID=" + SDMS_ICTicket.ICTicketID + "&MarginRemark=" + txtMarginRemarkRequest.Text.Trim();
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

            lblMessage.Text = Results.Message;
            if (Results.Status == PApplication.Failure)
            {
                lblMessageMarginWarrantyRequest.Text = Results.Message;
                lblMessageMarginWarrantyRequest.Visible = true;
                lblMessageMarginWarrantyRequest.ForeColor = Color.Red;
                return;
            }
            ShowMessage(Results);
            MPE_MarginWarrantyRequest.Hide();
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }
        protected void btnMarginWarrantyReject_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtRejectRemarks.Text.Trim()))
            {
                lblMessageMarginWarrantyReject.Text = "";
                lblMessageMarginWarrantyReject.ForeColor = Color.Red;
                lblMessageMarginWarrantyReject.Visible = true;
                return;
            }
            if (new BDMS_ICTicket().ApproveOrRejectMarginWarrantyChange(SDMS_ICTicket.ICTicketID, PSession.User.UserID, false, txtRejectRemarks.Text.Trim()))
            {
                lblMessage.Text = "Margin Warrranty rejected.";
                lblMessage.ForeColor = Color.Green;
                FillICTicket(SDMS_ICTicket.ICTicketID);
            }
            else
            {
                lblMessage.Text = "Margin Warrranty not rejected.";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSignSave_Click(object sender, EventArgs e)
        {
            MPE_FsrSignature.Show();
            PApiResult Results = UC_FsrSignature.SaveSign(SDMS_ICTicketFSR.FsrID);
            lblMessageFsrSignature.Text = Results.Message;
            if (Results.Status == PApplication.Failure)
            {
                lblMessageFsrSignature.Visible = true;
                lblMessageFsrSignature.ForeColor = Color.Red;
                UC_FsrSignature.FillMaster();
                return;
            }
            ShowMessage(Results);
            MPE_FsrSignature.Hide();
            FillICTicket(SDMS_ICTicket.ICTicketID);
        }

        protected void lbtnFsrSignatureDownload_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            PICTicketFSRSignature FSRSignature = new BDMS_ICTicketFSR().GetICTicketFSRSignatureByFsrIDDownload(SDMS_ICTicketFSR.FsrID);
            if(FSRSignature.FSRSignatureID == 0)
            {
                lblMessage.Text = "FSR Signature is not available";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;

                return;
            }
            if (lbActions.ID == "lbtnTPhoto")
            {
                if(FSRSignature.TPhoto.AttachedFile == null)
                {
                    return;
                } 
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Technician Photo.png");
                Response.BinaryWrite(FSRSignature.TPhoto.AttachedFile);
            }
            else if (lbActions.ID == "lbtnTSignature")
            {
                if (FSRSignature.TSignature.AttachedFile == null)
                {
                    return;
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Technician Sign.png");
                Response.BinaryWrite(FSRSignature.TSignature.AttachedFile);
            }
            else if (lbActions.ID == "lbtnCPhoto")
            {
                if (FSRSignature.CPhoto.AttachedFile == null)
                {
                    return;
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Customer Photo.png");
                Response.BinaryWrite(FSRSignature.CPhoto.AttachedFile);
            }
            else if (lbActions.ID == "lbtnCSignature")
            {
                if (FSRSignature.CSignature.AttachedFile == null)
                {
                    return;
                }
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "Customer Sign.png");
                Response.BinaryWrite(FSRSignature.CSignature.AttachedFile);
            } 
            Response.AddHeader("Content-type", FSRSignature.FileType); 
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250"); 
            Response.Flush();
            Response.End();
        }

        protected void btnReachedSite_Click(object sender, EventArgs e)
        {
            MPE_ReachedSite.Show();
            lblReachedSiteMessage.ForeColor = Color.Red;
            lblReachedSiteMessage.Visible = true;
            if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
            {
                lblReachedSiteMessage.Text = "Please Enable GeoLocation...!"; 
                return;
            }
            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                lblReachedSiteMessage.Text = "Please Enter Location...!"; 
                return;
            }

            int? HMRValue = string.IsNullOrEmpty(txtHMRValue.Text.Trim())?(int?) null: Convert.ToInt32(txtHMRValue.Text.Trim());
            int? DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);


            PApiResult Results = new BDMS_ICTicket().InsertICTicketSeReached(SDMS_ICTicket.ICTicketID, txtLocation.Text.Trim(), HMRValue, txtOperatorName.Text.Trim()
                , txtSiteContactPersonNumber.Text.Trim(), DesignationID, Convert.ToDecimal(hfLatitude.Value), Convert.ToDecimal(hfLongitude.Value));
            if (Results.Status == PApplication.Failure)
            {
                lblReachedSiteMessage.Text = Results.Message;
                lblReachedSiteMessage.Visible = true;
                lblReachedSiteMessage.ForeColor = Color.Red;
                return;
            }
            ShowMessage(Results);
            FillICTicket(SDMS_ICTicket.ICTicketID);
            MPE_ReachedSite.Hide();
        }

        protected void lblServiceEdit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                long ServiceChargeID = Convert.ToInt64(gvServiceCharges.DataKeys[gvRow.RowIndex].Value);
                PDMS_ServiceCharge ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, ServiceChargeID, "", false)[0];
               
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                if (ServiceCharge.Material.IsMainServiceMaterial)
                { 
                    lblMessage.Text = "You cannot edit main Service Material (" + ServiceCharge.Material.MaterialCode + ").";
                    return ;
                }
                string Message = ServiceChargeActionControl(ServiceCharge);
                if(!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }


                UC_ICTicketAddServiceCharges.FillMaster(SDMS_ICTicket);
                UC_ICTicketAddServiceCharges.Write(ServiceCharge);
                MPE_ICTicketAddServiceCharges.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        public void ClearAll()
        {
            txtDeclineReason.Text = "";
            txtRequestedDate.Text = "";
            ddlRequestedHH.ClearSelection();
            ddlRequestedMM.ClearSelection();
            txtMarginRemarkRequest.Text = "";
            txtRejectRemarks.Text = "";
            txtLocation.Text = "";
            ViewState["TsirID"] = null;
            gvTechnician.DataSource = null;
            gvTechnician.DataBind();
            lblDepartureDate.Text = "";
            lblReachedDate.Text = "";
            lblLocation.Text = "";
            lblServiceType.Text = "";
            ddlServiceTypeOverhaul.ClearSelection();
            ddlServiceSubType.ClearSelection();
            lblServicePriority.Text = "";
            lblDealerOffice.Text = "";
            lblHMRValue.Text = "";
            lblHMRDate.Text = "";
            cbCess.Checked = false;
            lblTypeOfWarranty.Text = "";
            lblMainApplication.Text = "";
            lblSubApplication.Text = "";
            lblSubApplicationEntry.Text = "";
            lblOperatorName.Text = "";
            lblSiteContactPersonNumber.Text = "";
            lblSiteContactPersonNumber2.Text = "";
            lblDesignation.Text = "";
            lblScopeOfWork.Text = "";
            lblNoClaimReason.Text = "";
            lblMcEnteredServiceDate.Text = "";
            lblServiceStartedDate.Text = "";
            lblServiceEndedDate.Text = "";
            lblKindAttn.Text = "";
            lblRemarks.Text = "";
            cbIsMachineActive.Checked = false;
            lblModeOfPayment.Text = "";
            lblOperatorNameFSR.Text = "";
            lblOperatorNumber.Text = "";
            lblMachineMaintenanceLevel.Text = "";
            cbIsRental.Checked = false;
            lblRentalName.Text = "";
            lblRentalNumber.Text = "";
            lblNatureOfComplaint.Text = "";
            lblObservation.Text = "";
            lblWorkCarriedOut.Text = "";
            lblReport.Text = "";
            gvAttachedFile.DataSource = null;
            gvAttachedFile.DataBind();
            gvAvailabilityOfOtherMachine.DataSource = null;
            gvAvailabilityOfOtherMachine.DataBind();
            gvServiceCharges.DataSource = null;
            gvServiceCharges.DataBind();
            gvTSIR.DataSource = null;
            gvTSIR.DataBind();
            txtCustomerPayPercentage.Text = "";
            txtDealerPayPercentage.Text = "";
            txtAEPayPercentage.Text = "";
            gvMaterial.DataSource = null;
            gvMaterial.DataBind();
            gvNotes.DataSource = null;
            gvNotes.DataBind();
            gvTechnicianWorkDays.DataSource = null;
            gvTechnicianWorkDays.DataBind();
            lblRestoreDate.Text = "";
            lblArrivalBackDate.Text = "";
            lblComplaintStatus.Text = "";
            lblCustomerRemarks.Text = "";
            lblCustomerSatisfactionLevel.Text = "";
            lblTName.Text = "";
            lblTPhoto.Text = "";
            lblTSignature.Text = "";
            lblCName.Text = "";
            lblCPhoto.Text = "";
            lblCSignature.Text = "";
        }

        public string ServiceChargeActionControl(PDMS_ServiceCharge ServiceCharge)
        {
            if (!string.IsNullOrEmpty(ServiceCharge.QuotationNumber))
            {
                return "Quotation already created. You cannot edit Service Material (" + ServiceCharge.Material.MaterialCode + ").";
                 
            }
            else if (!string.IsNullOrEmpty(ServiceCharge.ProformaInvoiceNumber))
            {
                return "Proforma Invoice already created. You cannot edit main Service Material (" + ServiceCharge.Material.MaterialCode + ")."; 
            }
            else if (!string.IsNullOrEmpty(ServiceCharge.InvoiceNumber))
            {
                return "Invoice already created. You cannot edit Service Material (" + ServiceCharge.Material.MaterialCode + ")."; 
            }
            else if (!string.IsNullOrEmpty(ServiceCharge.ClaimNumber))
            {
                return "Claim already created. You cannot edit Service Material (" + ServiceCharge.Material.MaterialCode + ")."; 
            }
            return "";
        }

        private void DisableAllGridEditDelete()
        {
            if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Restored) 
            {
                for (int i = 0; i < gvTechnician.Rows.Count; i++)
                {
                    ((LinkButton)gvTechnician.Rows[i].FindControl("lbTechnicianRemove")).Enabled = false;
                }
                for (int i = 0; i < gvAttachedFile.Rows.Count; i++)
                {
                    ((LinkButton)gvAttachedFile.Rows[i].FindControl("lblAttachedFileRemove")).Enabled = false;
                }
                for (int i = 0; i < gvAvailabilityOfOtherMachine.Rows.Count; i++)
                {
                    ((LinkButton)gvAvailabilityOfOtherMachine.Rows[i].FindControl("lbAvailabilityOfOtherMachineRemove")).Enabled = false;
                }
                //for (int i = 0; i < gvServiceCharges.Rows.Count; i++)
                //{
                //    ((LinkButton)gvServiceCharges.Rows[i].FindControl("lblServiceRemove")).Enabled = false;
                //    ((LinkButton)gvServiceCharges.Rows[i].FindControl("lblServiceEdit")).Enabled = false;
                //}
                //for (int i = 0; i < gvMaterial.Rows.Count; i++)
                //{
                //    ((LinkButton)gvMaterial.Rows[i].FindControl("lblMaterialRemove")).Enabled = false;
                //    ((CheckBox)gvMaterial.Rows[i].FindControl("cbEdit")).Enabled = false;
                //}
                for (int i = 0; i < gvNotes.Rows.Count; i++)
                {
                    ((LinkButton)gvNotes.Rows[i].FindControl("lblNoteRemove")).Enabled = false;
                }
                //for (int i = 0; i < gvTechnicianWorkDays.Rows.Count; i++)
                //{
                //    ((LinkButton)gvTechnicianWorkDays.Rows[i].FindControl("lbWorkedDayRemove")).Enabled = false;
                //}
            }
            if ((Boolean)SDMS_ICTicket.IsLocked || (Boolean)SDMS_ICTicket.SyncBlock)
            {
                for (int i = 0; i < gvTechnician.Rows.Count; i++)
                {
                    ((LinkButton)gvTechnician.Rows[i].FindControl("lbTechnicianRemove")).Enabled = false;
                }
                for (int i = 0; i < gvAttachedFile.Rows.Count; i++)
                {
                    ((LinkButton)gvAttachedFile.Rows[i].FindControl("lblAttachedFileRemove")).Enabled = false;
                }
                for (int i = 0; i < gvAvailabilityOfOtherMachine.Rows.Count; i++)
                {
                    ((LinkButton)gvAvailabilityOfOtherMachine.Rows[i].FindControl("lbAvailabilityOfOtherMachineRemove")).Enabled = false;
                }
                for (int i = 0; i < gvServiceCharges.Rows.Count; i++)
                {
                    ((LinkButton)gvServiceCharges.Rows[i].FindControl("lblServiceRemove")).Enabled = false;
                    ((LinkButton)gvServiceCharges.Rows[i].FindControl("lblServiceEdit")).Enabled = false;
                }
                for (int i = 0; i < gvMaterial.Rows.Count; i++)
                {
                    ((LinkButton)gvMaterial.Rows[i].FindControl("lblMaterialRemove")).Enabled = false;
                    ((CheckBox)gvMaterial.Rows[i].FindControl("cbEdit")).Enabled = false;
                }
                for (int i = 0; i < gvNotes.Rows.Count; i++)
                {
                    ((LinkButton)gvNotes.Rows[i].FindControl("lblNoteRemove")).Enabled = false;
                }
                for (int i = 0; i < gvTechnicianWorkDays.Rows.Count; i++)
                {
                    ((LinkButton)gvTechnicianWorkDays.Rows[i].FindControl("lbWorkedDayRemove")).Enabled = false;
                }
            } 
        }
    }
}