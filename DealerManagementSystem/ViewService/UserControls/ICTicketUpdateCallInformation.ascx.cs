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
    public partial class ICTicketUpdateCallInformation : System.Web.UI.UserControl
    {
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

        }
        public void FillMaster(PDMS_ICTicket ICTicket)
        {
            SDMS_ICTicket = ICTicket;
            Clear();
            if ((SDMS_ICTicket.Equipment.IsRefurbished == true) && (SDMS_ICTicket.Equipment.RefurbishedBy == SDMS_ICTicket.Dealer.DealerID) && (SDMS_ICTicket.Equipment.RFWarrantyExpiryDate >= SDMS_ICTicket.ICTicketDate))
            {
                FillGetServiceType(2);
            }
            else if (SDMS_ICTicket.IsWarranty == true)
            {
                FillGetServiceType(1);
            }
            else
            {
                FillGetServiceType(0);
            }
            FillGetServicePriority();
            FillGetDealerOffice(SDMS_ICTicket.Dealer.DealerID);
            new BDMS_TypeOfWarranty().GetTypeOfWarrantyDDL(ddlTypeOfWarranty, null, null);

            txtLocation.Text = SDMS_ICTicket.Location;
            if (SDMS_ICTicket.DealerOffice != null)
                ddlDealerOffice.SelectedValue = SDMS_ICTicket.DealerOffice.OfficeID.ToString();

           

            if (SDMS_ICTicket.ServiceType != null)
            {
                ddlServiceType.SelectedValue = SDMS_ICTicket.ServiceType.ServiceTypeID.ToString();
                if (ddlServiceType.SelectedValue == "7")
                {
                    ddlServiceTypeOverhaul.Visible = true;
                }
                else
                {
                    ddlServiceTypeOverhaul.Visible = false;
                }

                if (SDMS_ICTicket.ServiceTypeOverhaul != null)
                    ddlServiceTypeOverhaul.SelectedValue = SDMS_ICTicket.ServiceTypeOverhaul.ServiceTypeOverhaulID.ToString();

                FillGetServiceSubType(SDMS_ICTicket.ServiceType.ServiceTypeID);
                if (ddlServiceType.SelectedValue == "3")
                {
                    ddlServiceSubType.Visible = true;
                }
                if (SDMS_ICTicket.ServiceSubType != null)
                {
                    ddlServiceSubType.SelectedValue = SDMS_ICTicket.ServiceSubType.ServiceSubTypeID.ToString();
                }
            }
            if (SDMS_ICTicket.ServicePriority != null)
                ddlServicePriority.SelectedValue = SDMS_ICTicket.ServicePriority.ServicePriorityID.ToString();
          
            txtHMRValue.Text = Convert.ToString(SDMS_ICTicket.CurrentHMRValue);



            FillMainApplication();
            if (SDMS_ICTicket.MainApplication != null)
            {
                ddlMainApplication.SelectedValue = SDMS_ICTicket.MainApplication.MainApplicationID.ToString();
                FillSubApplication();
                if (SDMS_ICTicket.SubApplication != null)
                {
                    ddlSubApplication.SelectedValue = SDMS_ICTicket.SubApplication.SubApplicationID.ToString();
                    if (SDMS_ICTicket.SubApplication.SubApplicationID.ToString() == "26")
                    {
                        txtSubApplicationEntry.Visible = true;
                    }
                    else
                    {
                        txtSubApplicationEntry.Visible = false;
                    }
                }
            }


            txtScopeOfWork.Text = SDMS_ICTicket.ScopeOfWork;

            txtKindAttn.Text = SDMS_ICTicket.KindAttn;
            txtRemarks.Text = SDMS_ICTicket.Remarks;

            txtOperatorName.Text = SDMS_ICTicket.SiteContactPersonName;
            txtSiteContactPersonNumber.Text = SDMS_ICTicket.SiteContactPersonNumber;
            txtSiteContactPersonNumber2.Text = SDMS_ICTicket.SiteContactPersonNumber2;


            EnableOrDesableBasedOnServiceCharges();

            new BDMS_SiteContactPersonDesignation().GetSiteContactPersonDesignationDDL(ddlDesignation, null, null);
            if (SDMS_ICTicket.SiteContactPersonDesignation != null)
            {
                ddlDesignation.SelectedValue = SDMS_ICTicket.SiteContactPersonDesignation.DesignationID.ToString();
            }
            if (SDMS_ICTicket.TypeOfWarranty != null)
            {
                ddlTypeOfWarranty.SelectedValue = SDMS_ICTicket.TypeOfWarranty.TypeOfWarrantyID.ToString();
            }
            cbCess.Checked = SDMS_ICTicket.IsCess;

            txtSubApplicationEntry.Text = SDMS_ICTicket.SubApplicationEntry;
            cbIsMachineActive.Checked = SDMS_ICTicket.IsMachineActive;

            cbNoClaim.Checked = SDMS_ICTicket.NoClaim;

            txtNoClaimReason.Text = SDMS_ICTicket.NoClaimReason;

            txtMcEnteredServiceDate.Text = SDMS_ICTicket.McEnteredServiceDate == null ? "" : ((DateTime)SDMS_ICTicket.McEnteredServiceDate).ToShortDateString();
            txtServiceStartedDate.Text = SDMS_ICTicket.ServiceStartedDate == null ? "" : ((DateTime)SDMS_ICTicket.ServiceStartedDate).ToShortDateString();
            txtServiceEndedDate.Text = SDMS_ICTicket.ServiceEndedDate == null ? "" : ((DateTime)SDMS_ICTicket.ServiceEndedDate).ToShortDateString();

            if (SDMS_ICTicket.Dealer.DealerCode == "9005")
            {
                cbCess.Visible = true;
            }
            else
            {
                cbCess.Visible = false;
            }
        }

        private void FillGetServiceType(int IsWarranty)
        {
            ddlServiceType.DataSource = null;
            ddlServiceType.DataBind();
            ddlServiceType.DataTextField = "ServiceType";
            ddlServiceType.DataValueField = "ServiceTypeID";
            ddlServiceType.DataSource = new BDMS_Service().GetServiceType(null, null, IsWarranty);


            ddlServiceType.DataBind();
            ddlServiceTypeOverhaul.DataSource = new BDMS_Service().GetServiceTypeddlServiceTypeOverhaul(null, null);
            ddlServiceTypeOverhaul.DataBind();
            ddlServiceTypeOverhaul.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillGetServicePriority()
        {
            ddlServicePriority.DataTextField = "ServicePriority";
            ddlServicePriority.DataValueField = "ServicePriorityID";
            ddlServicePriority.DataSource = new BDMS_Service().GetServicePriority(null, null);
            ddlServicePriority.DataBind();
            ddlServicePriority.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillGetDealerOffice(int DealerID)
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void FillMainApplication()
        {
            ddlMainApplication.DataTextField = "MainApplication";
            ddlMainApplication.DataValueField = "MainApplicationID";
            ddlMainApplication.DataSource = new BDMS_Service().GetMainApplication(null, null);
            ddlMainApplication.DataBind();
            ddlMainApplication.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillSubApplication()
        {
            ddlSubApplication.DataTextField = "SubApplication";
            ddlSubApplication.DataValueField = "SubApplicationID";
            ddlSubApplication.DataSource = new BDMS_Service().GetSubApplication(Convert.ToInt32(ddlMainApplication.SelectedValue), null, null);
            ddlSubApplication.DataBind();
            ddlSubApplication.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlMainApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubApplication(); 
        }

        public string ValidationReached(PDMS_ICTicket SDMS_ICTicket)
        {
            string Message = "";
           
            txtLocation.BorderColor = Color.Silver;

            if((SDMS_ICTicket.Equipment.WarrantyExpiryDate == null)|| (SDMS_ICTicket.Equipment.CommissioningOn == null))
            {
                int ServiceTypeID = Convert.ToInt32(ddlServiceType.SelectedValue);
                if((ServiceTypeID == (short)DMS_ServiceType.PreCommission) || (ServiceTypeID == (short)DMS_ServiceType.Commission))
                {

                }
                else
                {
                    return "Warranty Date and Commissioning On is not available so please select Pre -Commission or Commission ";
                }
            } 

            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                txtLocation.BorderColor = Color.Red;
                return "Please enter the Location"; 
            }

            if (string.IsNullOrEmpty(Message))
            {
                return Message;
            }

            int value;
            if (!string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
            {
                if (!int.TryParse("0" + txtHMRValue.Text, out value))
                {
                    txtHMRValue.BorderColor = Color.Red;
                    return " Please enter integer in HMR Value";
                   
                }
                if (SDMS_ICTicket.LastHMRValue > Convert.ToInt32(txtHMRValue.Text.Trim()))
                {
                    txtHMRValue.BorderColor = Color.Red;
                    return "HMR value should not be less than last HMR value.";
                    
                }
            } 
            return Message;
        }

        string ValidatetionRestore(PDMS_ICTicket SDMS_ICTicket)
        {
            ddlServiceType.BorderColor = Color.Silver;
            ddlServicePriority.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver; 
            txtHMRValue.BorderColor = Color.Silver; 

            ddlMainApplication.BorderColor = Color.Silver;
            ddlSubApplication.BorderColor = Color.Silver;

            string Message = "";
            if (ddlServiceType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Service Type";
                ddlServiceType.BorderColor = Color.Red;
            }
            if (ddlServicePriority.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Service Priority";
                ddlServicePriority.BorderColor = Color.Red;
            }

            if (ddlDealerOffice.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Delivery Location";
                ddlDealerOffice.BorderColor = Color.Red;
            }


             
            int value;
            if (string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the HMR Value";
                txtHMRValue.BorderColor = Color.Red;
            }
            else
            {
                if (!int.TryParse("0" + txtHMRValue.Text, out value))
                {
                    Message = Message + "<br/>Please enter integer in HMR Value";
                    txtHMRValue.BorderColor = Color.Red;
                }
                if (SDMS_ICTicket.LastHMRValue > Convert.ToInt32(txtHMRValue.Text.Trim()))
                {
                    Message = Message + "<br/>HMR value should not be less than last HMR value.";
                    txtHMRValue.BorderColor = Color.Red;
                }
            }


            if (ddlMainApplication.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Main Application";
                ddlMainApplication.BorderColor = Color.Red;
                ddlSubApplication.BorderColor = Color.Red;
            }
            else if (ddlSubApplication.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sub Application";
                ddlSubApplication.BorderColor = Color.Red;
            }
            //if (SS_ServiceCharge.Count == 0)
            //{
            //    Message = Message + "<br/>Please add service code in service charges screen."; 
            //}

            return Message;

        }

        protected void txtHMRValue_TextChanged(object sender, EventArgs e)
        {
            WarrantyCalculation();
        }
        protected void txtReachedDate_TextChanged(object sender, EventArgs e)
        {
            ValidateReachedDate();
            WarrantyCalculation();
        }
        void ValidateReachedDate()
        { 
        }
        public void EnableOrDesableBasedOnServiceCharges()
        {

            string ClaimNumber = "";
            if (SDMS_ICTicket.ServiceCharges.Count != 0)
            { ClaimNumber = SDMS_ICTicket.ServiceCharges[0].ClaimNumber; }

            if (string.IsNullOrEmpty(ClaimNumber)) { ddlServiceType.Enabled = true; }
            else { ddlServiceType.Enabled = false; }

            string QuotationNumber = "";
            if (SS_ServiceMaterial.Count != 0)
            {
                foreach (PDMS_ServiceMaterial ServiceMaterial in SS_ServiceMaterial)
                {
                    QuotationNumber = ServiceMaterial.QuotationNumber;
                    if (!string.IsNullOrEmpty(QuotationNumber)) { break; }
                }
            }
            if (string.IsNullOrEmpty(QuotationNumber))
            {
                ddlDealerOffice.Enabled = true; 
                txtHMRValue.Enabled = true; 
            }
            else
            {
                ddlDealerOffice.Enabled = false; 
                txtHMRValue.Enabled = false; 
            }

            //UC_BasicInformation.SDMS_ICTicket = SDMS_ICTicket;
            //UC_BasicInformation.FillBasicInformation();
            WarrantyCalculation();
        }

        void WarrantyCalculation()
        {
           // CheckBox UCcbIsWarranty = (CheckBox)UC_BasicInformation.FindControl("cbIsWarranty");
            if (SDMS_ICTicket.Equipment.IsAMC == true)
            {
            }
            else if ((SDMS_ICTicket.Equipment.IsRefurbished == true) && (SDMS_ICTicket.Equipment.RefurbishedBy == SDMS_ICTicket.Dealer.DealerID) && (SDMS_ICTicket.Equipment.RFWarrantyExpiryDate >= SDMS_ICTicket.ICTicketDate))
            {

            }
            else if (SDMS_ICTicket.IsMarginWarranty == true)
            {

            }
            else if (SDMS_ICTicket.Equipment.EquipmentModel.Division.UOM == "Cum")
            {
            }
            else
            {
                if (!string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
                {

                    //Boolean Old = UCcbIsWarranty.Checked;
                    Boolean Old = SDMS_ICTicket.IsWarranty;
                    int vHMR = 2000;
                    DateTime cD = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).AddYears(-1);
                    if (cD < Convert.ToDateTime("01/11/2018"))
                    {
                        vHMR = 1000;
                    }

                    if (SDMS_ICTicket.Equipment.EquipmentWarrantyType != null)
                    {
                        if (SDMS_ICTicket.Equipment.EquipmentWarrantyType.HMR != 0)
                        {
                            vHMR = SDMS_ICTicket.Equipment.EquipmentWarrantyType.HMR;
                        }
                    }

                    if ((Convert.ToInt32(txtHMRValue.Text.Trim()) > vHMR) || (SDMS_ICTicket.Equipment.WarrantyExpiryDate < SDMS_ICTicket.ICTicketDate))
                    {
                        // UCcbIsWarranty.Checked = false;
                        SDMS_ICTicket.IsWarranty = false;
                    }
                    else
                    {
                        // UCcbIsWarranty.Checked = true;
                        SDMS_ICTicket.IsWarranty = true;
                    }
                    // if (Old != UCcbIsWarranty.Checked)
                    if (Old != SDMS_ICTicket.IsWarranty)
                    {
                        //FillGetServiceType(Convert.ToInt32(UCcbIsWarranty.Checked));
                        FillGetServiceType(Convert.ToInt32(SDMS_ICTicket.IsWarranty));
                    }
                }
            }
        }

        protected void ddlSubApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSubApplicationEntry.Text = "";
            if (ddlSubApplication.SelectedValue == "26")
            {
                txtSubApplicationEntry.Visible = true;
            }
            else
            {
                txtSubApplicationEntry.Visible = false;
            }
        }

        protected void ddlServiceType_TextChanged(object sender, EventArgs e)
        {
            int ServiceTypeID = Convert.ToInt32(ddlServiceType.SelectedValue);
            if (ddlServiceType.SelectedValue == "7")
            {
                ddlServiceTypeOverhaul.Visible = true;
            }
            else
            {
                ddlServiceTypeOverhaul.Visible = false;
                ddlServiceTypeOverhaul.SelectedIndex = 0;
            }
            FillGetServiceSubType(Convert.ToInt32(ddlServiceType.SelectedValue));


            if (ddlServiceSubType.Items.Count > 1)
            {
                ddlServiceSubType.Visible = true;
            }
            else
            {
                ddlServiceSubType.Visible = false;
            }
        }
        private void FillGetServiceSubType(int ServiceTypeID)
        {
            ddlServiceSubType.DataSource = new BDMS_Service().GetServiceSubType(null, ServiceTypeID);
            ddlServiceSubType.DataBind();
        }

        protected void txtSubApplicationEntry_TextChanged(object sender, EventArgs e)
        {

        }

        public PICTicketServiceConfirmation_Post Read(PDMS_ICTicket ICTicket)
        {
            PICTicketServiceConfirmation_Post IC = new PICTicketServiceConfirmation_Post(); 
           // IC.DealerCode = ICTicket.Dealer.DealerCode;
          //  IC.CustomerCode = ICTicket.Customer.CustomerCode;
            IC.ICTicketID = ICTicket.ICTicketID;
         //   IC.EquipmentSerialNo = ICTicket.Equipment.EquipmentSerialNo;
            IC.Location = txtLocation.Text.Trim();
            IC.OfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            
            IC.ServiceTypeID = ddlServiceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceType.SelectedValue);
            IC.ServiceSubTypeID = ddlServiceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceSubType.SelectedValue);

            IC.ServiceTypeOverhaulID = ddlServiceTypeOverhaul.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceTypeOverhaul.SelectedValue);
            IC.ServicePriorityID = ddlServicePriority.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServicePriority.SelectedValue);
             
            IC.CurrentHMRValue = string.IsNullOrEmpty(txtHMRValue.Text.Trim()) ? (int?)null : Convert.ToInt32(txtHMRValue.Text.Trim());

           // IC.IsWarranty = ICTicket.IsWarranty;

            IC.TypeOfWarrantyID = ddlTypeOfWarranty.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTypeOfWarranty.SelectedValue);


            IC.MainApplicationID = ddlMainApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMainApplication.SelectedValue);
            if (IC.MainApplicationID != null)
                IC.SubApplicationID = ddlSubApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubApplication.SelectedValue);


            IC.ScopeOfWork = txtScopeOfWork.Text.Trim();
            IC.KindAttn = txtKindAttn.Text.Trim();
            IC.Remarks = txtRemarks.Text.Trim();

            IC.SiteContactPersonName = txtOperatorName.Text.Trim();
            IC.SiteContactPersonNumber = txtSiteContactPersonNumber.Text.Trim();
            IC.SiteContactPersonNumber2 = txtSiteContactPersonNumber2.Text.Trim();

            if (ddlDesignation.SelectedValue != "0")
            {
                IC.SiteContactPersonDesignation = new PDMS_SiteContactPersonDesignation() { DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue) };
            }

            IC.IsCess = cbCess.Checked;
            IC.IsMachineActive = cbIsMachineActive.Checked;
            IC.SubApplicationEntry = txtSubApplicationEntry.Text.Trim();
             
            IC.NoClaim = cbNoClaim.Checked;
            IC.NoClaimReason = txtNoClaimReason.Text.Trim();

            IC.McEnteredServiceDate = string.IsNullOrEmpty(txtMcEnteredServiceDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtMcEnteredServiceDate.Text.Trim());
            IC.ServiceStartedDate = string.IsNullOrEmpty(txtServiceStartedDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtServiceStartedDate.Text.Trim());
            IC.ServiceEndedDate = string.IsNullOrEmpty(txtServiceEndedDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtServiceEndedDate.Text.Trim());

            return IC;
        }

        void Clear()
        {
            txtLocation.Text = "";

            //  ddlServiceType.SelectedValue = SDMS_ICTicket.ServiceType.ServiceTypeID.ToString(); 
            //  ddlServiceTypeOverhaul.SelectedValue = SDMS_ICTicket.ServiceTypeOverhaul.ServiceTypeOverhaulID.ToString(); 
            //  ddlServiceSubType.SelectedValue = SDMS_ICTicket.ServiceSubType.ServiceSubTypeID.ToString(); 
            //  ddlServicePriority.SelectedValue = SDMS_ICTicket.ServicePriority.ServicePriorityID.ToString();


            txtHMRValue.Text = "";

            //  ddlMainApplication.SelectedValue = SDMS_ICTicket.MainApplication.MainApplicationID.ToString();
            //  FillSubApplication(); 
            //  ddlSubApplication.SelectedValue = SDMS_ICTicket.SubApplication.SubApplicationID.ToString();



            txtScopeOfWork.Text = "";
            txtKindAttn.Text = "";
            txtRemarks.Text = "";
            txtOperatorName.Text = "";
            txtSiteContactPersonNumber.Text = "";
            txtSiteContactPersonNumber2.Text = "";
            // EnableOrDesableBasedOnServiceCharges(); 
            // ddlDesignation.SelectedValue = SDMS_ICTicket.SiteContactPersonDesignation.DesignationID.ToString(); 
            // ddlTypeOfWarranty.SelectedValue = SDMS_ICTicket.TypeOfWarranty.TypeOfWarrantyID.ToString();

            cbCess.Checked = false;
            txtSubApplicationEntry.Text = "";
            cbIsMachineActive.Checked = false;
            cbNoClaim.Checked = false;
            txtNoClaimReason.Text = "";
            txtMcEnteredServiceDate.Text = "";
            txtServiceStartedDate.Text = "";
            txtServiceEndedDate.Text = "";
        }
    }
}