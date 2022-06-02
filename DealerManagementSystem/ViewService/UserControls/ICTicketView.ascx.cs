using Business;
using DataAccess;
using Newtonsoft.Json;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketView : System.Web.UI.UserControl
    {

        public string PageName { get; set; }

        public PDMS_ICTicket SDMS_ICTicket
        {
            get
            {
                if (Session["SDMS_ICTicket"] == null)
                {
                    Session["SDMS_ICTicket"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)Session["SDMS_ICTicket"];
            }
            set
            {
                Session["SDMS_ICTicket"] = value;
            }
        }
        public PDMS_ICTicketFSR SDMS_ICTicketFSR
        {
            get
            {
                if (Session["PDMS_ICTicketFSR"] == null)
                {
                    Session["PDMS_ICTicketFSR"] = new PDMS_ICTicketFSR();
                }
                return (PDMS_ICTicketFSR)Session["PDMS_ICTicketFSR"];
            }
            set
            {
                Session["PDMS_ICTicketFSR"] = value;
            }
        }
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

        public List<PDMS_ServiceTechnician> SDMS_TechniciansWD
        {
            get
            {
                if (Session["DMS_ICTicketTechnicianAssignWD"] == null)
                {
                    Session["DMS_ICTicketTechnicianAssignWD"] = new List<PDMS_ServiceTechnician>();
                }
                return (List<PDMS_ServiceTechnician>)Session["DMS_ICTicketTechnicianAssignWD"];
            }
            set
            {
                Session["DMS_ICTicketTechnicianAssignWD"] = value;
            }
        }
        public List<PDMS_ServiceTechnician> SDMS_Technicians
        {
            get
            {
                if (Session["DMS_ICTicketTechnicianAssign"] == null)
                {
                    Session["DMS_ICTicketTechnicianAssign"] = new List<PDMS_ServiceTechnician>();
                }
                return (List<PDMS_ServiceTechnician>)Session["DMS_ICTicketTechnicianAssign"];
            }
            set
            {
                Session["DMS_ICTicketTechnicianAssign"] = value;
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
            if (!IsPostBack)
            {
            }

        }
        public void FillICTicket(long ICTicketID)
        {
            SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);
            SDMS_ICTicketFSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, SDMS_ICTicket.ICTicketID, null, null, "", null, null, null);
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            SS_ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
            SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");


            FillBasicInformation();
            FillTechnicians();
            FillCallInformation();
            FillFSR();
            fillICTicketAttachedFile();
            FillAvailabilityOfOtherMachine();
            FillServiceCharges();
            FillServiceNotes();
            FillTechniciansByTicketID();
            FillRestore();
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
            lblInformation.Text = SDMS_ICTicket.Information;
            lblOldICTicketNumber.Text = SDMS_ICTicket.OldICTicketNumber;
            lblEquipment.Text = SDMS_ICTicket.Equipment.EquipmentSerialNo;
            lblModel.Text = SDMS_ICTicket.Equipment.EquipmentModel.Model;
            lblLastHMRValue.Text = SDMS_ICTicket.LastHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.LastHMRDate).ToShortDateString() + "  " + Convert.ToString(SDMS_ICTicket.LastHMRValue);
        }
        private void FillTechnicians()
        {
            SDMS_Technicians = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);
            gvTechnician.DataSource = SDMS_Technicians;
            gvTechnician.DataBind();
        }
        private void FillCallInformation()
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

            if (SDMS_ICTicket.MainApplication != null)
            {
                lblMainApplication.Text = SDMS_ICTicket.MainApplication.MainApplication;
                if (SDMS_ICTicket.SubApplication != null)
                {
                    lblSubApplication.Text = SDMS_ICTicket.SubApplication.SubApplicationID.ToString();
                    //if (SDMS_ICTicket.SubApplication.SubApplicationID.ToString() == "26")
                    //{
                    //    txtSubApplicationEntry.Visible = true;
                    //}
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
                lblOperatorName.Text = SDMS_ICTicketFSR.OperatorName;
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
            if (Note.Count == 0)
            {
                PDMS_AvailabilityOfOtherMachine N = new PDMS_AvailabilityOfOtherMachine();
                Note.Add(N);
            }
            gvAvailabilityOfOtherMachine.DataSource = Note;
            gvAvailabilityOfOtherMachine.DataBind();
        }
        public void FillServiceCharges()
        {

            List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            //string ClaimNumber = "";
            //if (Charge.Count == 0)
            //{
            //    PDMS_ServiceCharge c = new PDMS_ServiceCharge();
            //    Charge.Add(c);
            //}
            //else
            //{
            //    ClaimNumber = Charge[0].ClaimNumber;
            //}

            gvServiceCharges.DataSource = Charge;
            gvServiceCharges.DataBind();
          //  gvServiceCharges.FooterRow.Visible = true;

            //HttpContext.Current.Session["IsMainServiceMaterial"] = false;
            //if (gvServiceCharges.Rows.Count == 1)
            //{
            //    Label lblMaterialCode = (Label)gvServiceCharges.Rows[0].FindControl("lblMaterialCode");
            //    if (string.IsNullOrEmpty(lblMaterialCode.Text))
            //    {
            //        HttpContext.Current.Session["IsMainServiceMaterial"] = true;
            //    }
            //    else
            //    {
            //        if (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PromotionalActivity)
            //        {
            //            gvServiceCharges.FooterRow.Visible = false;
            //        }
            //    }
            //}

            //TextBox txtServiceMaterial = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceMaterial");
            //TextBox txtServiceDate = (TextBox)gvServiceCharges.FooterRow.FindControl("txtServiceDate");
            //TextBox txtWorkedHours = (TextBox)gvServiceCharges.FooterRow.FindControl("txtWorkedHours");
            //TextBox txtBasePrice = (TextBox)gvServiceCharges.FooterRow.FindControl("txtBasePrice");
            //TextBox txtDiscount = (TextBox)gvServiceCharges.FooterRow.FindControl("txtDiscount");
            //LinkButton lblServiceAdd = (LinkButton)gvServiceCharges.FooterRow.FindControl("lblServiceAdd");

            //if (SDMS_ICTicket.ServiceType == null)
            //{
            //    txtWorkedHours.Visible = false;
            //    txtBasePrice.Visible = false;
            //    txtDiscount.Visible = false;
            //}
            //else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
            //    )
            //{
            //    txtWorkedHours.Visible = true;
            //    txtBasePrice.Visible = true;
            //    txtDiscount.Visible = true;
            //}
            //else
            //{
            //    txtWorkedHours.Visible = false;
            //    txtBasePrice.Visible = false;
            //    txtDiscount.Visible = false;
            //}
            //DataControlField gcServiceCharges = gvServiceCharges.Columns[14];
            //gcServiceCharges.Visible = true;

            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
            //    )
            //{
            //    gvServiceCharges.Columns[8].Visible = false;

                //btnRequestForClaim.Visible = false;
                //btnGenerateQuotation.Visible = true;
                //btnGenerateProfarmaInvoice.Visible = true;
                //btnGenerateInvoice.Visible = true;
                //txtServiceMaterial.Visible = true;
                //lblServiceAdd.Visible = true;

                //txtWorkedHours.Visible = true;
                //txtBasePrice.Visible = true;
                //txtDiscount.Visible = true;
                //txtServiceDate.Visible = true;

                //List<PDMS_PaidServiceInvoice> Invoices = new BDMS_Service().GetPaidServiceInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "", true);
                //if (Invoices.Count == 1)
                //{
                    //btnGenerateQuotation.Visible = false;
                    //btnGenerateProfarmaInvoice.Visible = false;
                    //btnGenerateInvoice.Visible = false;

                //    txtServiceMaterial.Visible = false;
                //    lblServiceAdd.Visible = false;

                //    txtWorkedHours.Visible = false;
                //    txtBasePrice.Visible = false;
                //    txtDiscount.Visible = false;
                //    txtServiceDate.Visible = false;
                //    gcServiceCharges.Visible = false;
                //    gvServiceCharges.FooterRow.Visible = false;
                //}
                //else
                //{
                //    List<PDMS_PaidServiceInvoice> Proformas = new BDMS_Service().GetPaidServiceProformaInvoice(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
                //    if (Proformas.Count == 1)
                //    {
                        //btnGenerateProfarmaInvoice.Visible = false;
                        //btnGenerateQuotation.Visible = false;
                    //    txtServiceMaterial.Visible = false;
                    //    lblServiceAdd.Visible = false;

                    //    txtWorkedHours.Visible = false;
                    //    txtBasePrice.Visible = false;
                    //    txtDiscount.Visible = false;
                    //    txtServiceDate.Visible = false;
                    //    gcServiceCharges.Visible = false;
                    //    gvServiceCharges.FooterRow.Visible = false;
                    //}
                    //else
                    //{
                    //    List<PDMS_PaidServiceInvoice> SOIs = new BDMS_Service().GetPaidServiceQuotation(null, SDMS_ICTicket.ICTicketID, "", null, null, null, "");
                    //    if (SOIs.Count == 1)
                    //    {
                            // btnGenerateQuotation.Visible = false; 
                       // }
                   // }
            //    }
            //}
            //else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.REPI)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.RCommission)
            //    || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.RWarranty))
            //{
            //    gvServiceCharges.Columns[8].Visible = false;
            //    gvServiceCharges.Columns[9].Visible = false;
            //    gvServiceCharges.Columns[10].Visible = false;
            //    gvServiceCharges.Columns[11].Visible = false;

                //btnGenerateQuotation.Visible = false;
                //btnGenerateProfarmaInvoice.Visible = false;
                //btnGenerateInvoice.Visible = false;
                //btnRequestForClaim.Visible = false;
            //}
            //else
            //{
            //    gvServiceCharges.Columns[9].Visible = false;
            //    gvServiceCharges.Columns[10].Visible = false;
            //    gvServiceCharges.Columns[11].Visible = false;

                //btnGenerateQuotation.Visible = false;
                //btnGenerateProfarmaInvoice.Visible = false;
                //btnGenerateInvoice.Visible = false;
                //btnRequestForClaim.Visible = true;
                //if (!string.IsNullOrEmpty(ClaimNumber))
                //{
                //    gcServiceCharges.Visible = false;
                //    gvServiceCharges.FooterRow.Visible = false;
                    // btnRequestForClaim.Visible = false;
               // }
           // }
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Add Technician")
            {
                MPE_AddTechnician.Show(); 
            }
            //else if (lbActions.Text == "Convert to Prospect")
            //{
            //    string endPoint = "Lead/UpdateLeadStatus?LeadID=" + Lead.LeadID + "&StatusID=3" + "&UserID=" + PSession.User.UserID;
            //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            //    ShowMessage(Results);
            //    if (Results.Status == PApplication.Failure)
            //    {
            //        lblMessage.ForeColor = Color.Red;
            //        return;
            //    }
            //    fillViewLead(Lead.LeadID);
            //}
            //else if (lbActions.Text == "Lost Lead")
            //{
            //    MPE_LostReason.Show();
            //}
            //else if (lbActions.Text == "Cancel Lead")
            //{
            //    MPE_RejectedBySales.Show();
            //}
            //else if (lbActions.Text == "Assign")
            //{
            //    MPE_AssignSE.Show();
            //    UC_AssignSE.FillMaster(Lead);
            //}
            //else if (lbActions.Text == "Add Follow-up")
            //{
            //    MPE_FollowUp.Show();

            //    List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, true);
            //    List<PUser> U = new List<PUser>();
            //    foreach (PLeadSalesEngineer SE in SalesEngineer)
            //    {
            //        U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            //    }
            //    new DDLBind((DropDownList)UC_FollowUp.FindControl("ddlSalesEngineer"), U, "ContactName", "UserID", false);
            //    UC_FollowUp.FillMaster();
            //}
            //else if (lbActions.Text == "Customer Conversation")
            //{
            //    MPE_Conversation.Show();
            //    UC_CustomerConversation.FillMaster(Lead.LeadID);


            //}
            //else if (lbActions.Text == "Financial Info")
            //{
            //    MPE_Financial.Show();
            //    UC_Financial.FillMaster();
            //}
            //else if (lbActions.Text == "Add Effort")
            //{
            //    DropDownList ddlSalesEngineer = (DropDownList)UC_Effort.FindControl("ddlSalesEngineer");
            //    DropDownList ddlEffortType = (DropDownList)UC_Effort.FindControl("ddlEffortType");

            //    new DDLBind(ddlEffortType, new BDMS_Master().GetEffortType(null, null), "EffortType", "EffortTypeID");
            //    ddlSalesEngineer.Enabled = false;

            //    MPE_Effort.Show();

            //    List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, true);
            //    List<PUser> U = new List<PUser>();
            //    foreach (PLeadSalesEngineer SE in SalesEngineer)
            //    {
            //        U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            //    }
            //    new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID", false);
            //}
            //else if (lbActions.Text == "Add Expense")
            //{
            //    DropDownList ddlSalesEngineer = (DropDownList)UC_Expense.FindControl("ddlSalesEngineer");
            //    DropDownList ddlExpenseType = (DropDownList)UC_Expense.FindControl("ddlExpenseType");
            //    new DDLBind(ddlExpenseType, new BDMS_Master().GetExpenseType(null, null), "ExpenseType", "ExpenseTypeID");
            //    ddlSalesEngineer.Enabled = false;
            //    MPE_Expense.Show();
            //    List<PLeadSalesEngineer> SalesEngineer = new BLead().GetLeadSalesEngineer(Lead.LeadID, PSession.User.UserID, true);
            //    List<PUser> U = new List<PUser>();
            //    foreach (PLeadSalesEngineer SE in SalesEngineer)
            //    {
            //        U.Add(new PUser() { UserID = SE.SalesEngineer.UserID, ContactName = SE.SalesEngineer.ContactName });
            //    }
            //    new DDLBind(ddlSalesEngineer, U, "ContactName", "UserID", false);
            //}
            //else if (lbActions.Text == "Add Product")
            //{
            //    MPE_Product.Show();
            //    UC_Product.FillMaster(Lead);
            //}
            //else if (lbActions.Text == "Convert to Quotation")
            //{
            //    MPE_Quotation.Show();
            //    UC_Quotation.FillMaster(Lead);
            //}
            //else if (lbActions.Text == "Add Questionaries")
            //{
            //    new DDLBind(ddlQuestionariesMain, new BLead().GetLeadQuestionariesMain(null, null), "LeadQuestionariesMain", "LeadQuestionariesMainID");
            //    MPE_Questionaries.Show();
            //}
            //else if (lbActions.Text == "Add Visit")
            //{
            //    MPE_Visit.Show();
            //    new DDLBind(ddlActionType, new BPreSale().GetActionType(null, null), "ActionType", "ActionTypeID");
            //    new DDLBind(ddlImportance, new BDMS_Master().GetImportance(null, null), "Importance", "ImportanceID");
            //}
        }
         

        protected void lbTechnicianDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblLeadQuestionariesID = (Label)gvRow.FindControl("lblLeadQuestionariesID");
            PLeadQuestionaries Questionaries = new PLeadQuestionaries();
            //Questionaries.LeadQuestionariesID = Convert.ToInt64(lblLeadQuestionariesID.Text);
            //Questionaries.LeadID = Lead.LeadID;

            //Questionaries.QuestionariesMain = new PLeadQuestionariesMain() { LeadQuestionariesMainID = 0 };
            //Questionaries.QuestionariesSub = new PLeadQuestionariesSub() { LeadQuestionariesSubID = 0 };
            //Questionaries.Remark = txtRemark.Text.Trim();
            //Questionaries.CreatedBy = new PUser() { UserID = PSession.User.UserID };


            //Questionaries.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/Questionaries", Questionaries));
            lblMessage.Visible = true;
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;

            FillICTicket(SDMS_ICTicket.ICTicketID);

        }
        protected void lbFSRAttachedFileRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);

            PDMS_FSRAttachedFile AttachedFile = new PDMS_FSRAttachedFile();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.IsDeleted = true;
            if (new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessage.Text = "File Removed";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "File is not Removed";
                lblMessage.ForeColor = Color.Red;
            }
            fillICTicketAttachedFile();
        }
        protected void lbAvailabilityOfOtherMachineRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AvailabilityOfOtherMachineID = Convert.ToInt64(gvAvailabilityOfOtherMachine.DataKeys[gvRow.RowIndex].Value);
            new BDMS_AvailabilityOfOtherMachine().InsertOrUpdateAvailabilityOfOtherMachineAddOrRemoveICTicket(AvailabilityOfOtherMachineID, 0, 0, 0, 0, true, PSession.User.UserID);
            lblMessage.Text = "Note is removed from this ticket";
            lblMessage.ForeColor = Color.Green;
            long ICTicketID = (long)ViewState["ICTicketID"];
            FillAvailabilityOfOtherMachine();
            DropDownList ddlTypeOfMachine = (DropDownList)gvAvailabilityOfOtherMachine.FooterRow.FindControl("ddlTypeOfMachine");
        }
        protected void lblServiceRemove_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long ServiceChargeID = Convert.ToInt64(gvServiceCharges.DataKeys[gvRow.RowIndex].Value);


            Label lblMaterialCode = (Label)gvServiceCharges.Rows[gvRow.RowIndex].FindControl("lblMaterialCode");

            PDMS_Material Material = new BDMS_Material().GetMaterialListSQL(null, lblMaterialCode.Text, null, null, null)[0];
            if (Material.IsMainServiceMaterial)
            {
                lblMessage.Text = "You cannot remove main service material (" + lblMaterialCode.Text + ")";
                return;
            }


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



        public void FillTSIRDetails()
        {
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            gvTSIR.DataSource = ICTicketTSIRs;
            gvTSIR.DataBind();
            string[] TsirCancel = ConfigurationManager.AppSettings["TsirCancel"].Split(',');
            for (int i = 0; i < gvTSIR.Rows.Count; i++)
            {
                LinkButton lblCancelTSIR = (LinkButton)gvTSIR.Rows[i].FindControl("lblCancelTSIR");
                lblCancelTSIR.Visible = false;
                if (TsirCancel.Contains(PSession.User.UserID.ToString()))
                {
                    lblCancelTSIR.Visible = true;
                }
            }
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
        private PDMS_TSIRAttachedFile CreateUploadedFileFSR(HttpPostedFile file)
        {

            PDMS_TSIRAttachedFile AttachedFile = new PDMS_TSIRAttachedFile();
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
            AttachedFile.ICTicket = new PDMS_ICTicket() { ICTicketID = SDMS_ICTicket.ICTicketID };
            return AttachedFile;
        }
        protected void gvAttachedFile_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    if (Convert.ToInt32(lblStatusID.Text) == (short)TSIRStatus.Canceled)
                    {
                        lblCancelTSIR.Visible = false;
                    }
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
            PDMS_TSIRAttachedFile AttachedFile = new PDMS_TSIRAttachedFile();
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
            AttachedFile.TSIR = new PDMS_ICTicketTSIR();
            AttachedFile.TSIR.TsirID = Convert.ToInt64(gvTSIR.DataKeys[index].Value);
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
            if (new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessage.Text = "File added";
                lblMessage.ForeColor = Color.Green;
                try
                {
                    List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, AttachedFile.TSIR.TsirID, null);
                    gvAF.DataSource = UploadedFile;
                    gvAF.DataBind();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                lblMessage.Text = "File is not added";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lblAttachedFileRemoveR_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView Parentgrid = (GridView)(gvRow.Parent.Parent);
            long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

            GridViewRow GParentrow = (GridViewRow)(Parentgrid.NamingContainer);
            int GParentRowIndex = GParentrow.RowIndex;

            PDMS_TSIRAttachedFile AttachedFile = new PDMS_TSIRAttachedFile();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.ICTicket = new PDMS_ICTicket();
            AttachedFile.ICTicket.ICTicketID = SDMS_ICTicket.ICTicketID;
            AttachedFile.TSIR = new PDMS_ICTicketTSIR();
            AttachedFile.TSIR.TsirID = ICTicketTSIR.TsirID;
            AttachedFile.IsDeleted = true;
            if (new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
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
            else
            {
                lblMessage.Text = "File is not Removed";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lnkDownloadR_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;
                GridView Parentgrid = (GridView)(gvRow.NamingContainer);


                long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

                PDMS_TSIRAttachedFile UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileByID(AttachedFileID);
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

        protected void lblCancelTSIR_Click(object sender, EventArgs e)
        {
            List<string> querys = new List<string>();
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long TsirID = Convert.ToInt64(gvTSIR.DataKeys[gvRow.RowIndex].Value);
            foreach (PDMS_ServiceCharge SC in SS_ServiceCharge)
            {
                if (!string.IsNullOrEmpty(SC.ClaimNumber))
                {
                    lblMessage.Text = "Service claim generated. Please cancel the Claim";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            foreach (PDMS_ServiceMaterial SM in SS_ServiceMaterial)
            {
                if (SM.TSIR.TsirID == TsirID)
                {
                    new BDMS_Service().UpdateSaleOrderNumberFromPostgres();
                    if (!string.IsNullOrEmpty(SM.ClaimNumber))
                    {
                        lblMessage.Text = "claim generated for Material " + SM.Material.MaterialCode;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    if (!string.IsNullOrEmpty(SM.DeliveryNumber))
                    {
                        lblMessage.Text = "Delivery Completed for Material " + SM.Material.MaterialCode;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            Boolean ID = new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(TsirID, (short)TSIRStatus.Canceled, PSession.User.UserID, 0);
            if (ID)
            {
                lblMessage.Text = "TSIR is Canceled successfully";
                lblMessage.ForeColor = Color.Green;
                FillTSIRDetails();
            }
            else
            {
                lblMessage.Text = "TSIR is not Canceled successfully";
                lblMessage.ForeColor = Color.Red;
            }
        }

        public void FillServiceMaterial()
        { 
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

            string Material = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterialF")).Text.Trim();
            if (string.IsNullOrEmpty(Material))
            {
                lblMessage.Text = "Please enter the material";
                return;
            }
            string MaterialSN = ((TextBox)gvMaterial.FooterRow.FindControl("txtMaterialSNF")).Text.Trim();

            string DefectiveMaterial = ((TextBox)gvMaterial.FooterRow.FindControl("txtDefectiveMaterialF")).Text.Trim();
            if (string.IsNullOrEmpty(DefectiveMaterial))
            {
                lblMessage.Text = "Please enter the defective material";
                return;
            }

            DefectiveMaterial = DefectiveMaterial.Split(' ')[0]; 

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
            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0];


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
                PDMS_Material MM_SQL = new BDMS_Material().GetMaterialListSQL(null, Material,null,null,null)[0];
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


            PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, lblMaterialCode.Text,null,null,null)[0];
            string Status = "";
            if (MM.MaterialGroup != "887")
            {

                 string Q1 = "update dssor_sales_order_item set s_status = 'CANCELLED' where s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + "  and p_so_id ='" + lblQuotationNumber.Text + "' and f_material_id = '" + lblMaterialCode.Text + "'";
                querys.Add(Q1);
                

                if (!string.IsNullOrEmpty(lblDeliveryNumber.Text.Trim()))
                {
                    string f_office = new NpgsqlServer().ExecuteScalar("select  f_office from dsder_delv_item  where p_del_id ='" + lblDeliveryNumber.Text.Trim() + "' and f_material_id='" + lblMaterialCode.Text + "' limit 1");
                    string p_location = new NpgsqlServer().ExecuteScalar("select  f_location from dsder_delv_item  where p_del_id ='" + lblDeliveryNumber.Text.Trim() + "' and f_material_id='" + lblMaterialCode.Text + "' limit 1");

                    string Q2 = "INSERT INTO public.af_stock_ledger_icticket(" +
                       "s_establishment, s_tenant_id, p_location, p_office, p_material, p_stock_type,  p_batch, r_document_type, r_document_id, r_posting_date, f_ref_id1, r_opening_qty, r_inward_qty, r_outward_qty, r_closing_qty, r_current_stock, nes_flag, s_status, created_by, created_on) "
         + " VALUES (1000, " + SDMS_ICTicket.Dealer.DealerCode + ", '" + p_location + "','" + f_office + "', '" + lblMaterialCode.Text + "', 'SALE', 'B1', 'DLV','" + lblDeliveryNumber.Text.Trim() + "', now(),'" + SDMS_ICTicket.ICTicketNumber + "', 0,+" + txtQty.Text + ", 0, 0, 0, 'N','Created','sa',now() )";

                    querys.Add(Q2);
                }

               

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
                    PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(null, lblMaterialCode.Text,null,null,null)[0];
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

        protected void lblNoteRemove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long ServiceNoteID = Convert.ToInt64(gvNotes.DataKeys[gvRow.RowIndex].Value);
            new BDMS_ICTicket().InsertOrUpdateNoteAddOrRemoveICTicket(ServiceNoteID, 0, 0, "", true, PSession.User.UserID);
            lblMessage.Text = "Note is removed from this ticket";
            lblMessage.ForeColor = Color.Green; 
            FillServiceNotes();
            DropDownList ddlNoteType = (DropDownList)gvNotes.FooterRow.FindControl("ddlNoteType"); 
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
            if (WorkedDate.Count == 0)
            {
                PDMS_ServiceTechnicianWorkedDate c = new PDMS_ServiceTechnicianWorkedDate();
                WorkedDate.Add(c);
            }

            gvTechnicianWorkDays.DataSource = WorkedDate;
            gvTechnicianWorkDays.DataBind();
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

        private void FillRestore()
        {
            lblRestoreDate.Text = SDMS_ICTicket.RestoreDate == null ? "" : ((DateTime)SDMS_ICTicket.RestoreDate).ToString();
            lblArrivalBackDate.Text = SDMS_ICTicket.ArrivalBack == null ? "" : ((DateTime)SDMS_ICTicket.ArrivalBack).ToString();

            if (SDMS_ICTicket.CustomerSatisfactionLevel != null)
                lblCustomerSatisfactionLevel.Text = SDMS_ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID.ToString();

            lblCustomerRemarks.Text = SDMS_ICTicketFSR.CustomerRemarks;

            lblComplaintStatus.Text = SDMS_ICTicketFSR.ComplaintStatus;
        }
    }
}