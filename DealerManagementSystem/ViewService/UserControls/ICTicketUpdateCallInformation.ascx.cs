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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(PDMS_ICTicket SDMS_ICTicket)
        { 
            txtLocation.Text = SDMS_ICTicket.Location;
            if (SDMS_ICTicket.DealerOffice != null)
                ddlDealerOffice.SelectedValue = SDMS_ICTicket.DealerOffice.OfficeID.ToString();


            txtDepartureDate.Text = SDMS_ICTicket.DepartureDate == null ? "" : ((DateTime)SDMS_ICTicket.DepartureDate).ToShortDateString();
            ddlDepartureHH.SelectedValue = SDMS_ICTicket.DepartureDate == null ? "-1" : ((DateTime)SDMS_ICTicket.DepartureDate).Hour.ToString();
            if (SDMS_ICTicket.DepartureDate != null)
            {
                int DepartureMMMinute = ((DateTime)SDMS_ICTicket.DepartureDate).Minute;
                int adjustment = DepartureMMMinute % 5;
                if (adjustment != 0)
                {
                    DepartureMMMinute = (DepartureMMMinute - adjustment) + 5;
                }
                ddlDepartureMM.SelectedValue = DepartureMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlDepartureMM.SelectedValue = "0";
            }

            txtReachedDate.Text = SDMS_ICTicket.ReachedDate == null ? "" : ((DateTime)SDMS_ICTicket.ReachedDate).ToShortDateString();
            ddlReachedHH.SelectedValue = SDMS_ICTicket.ReachedDate == null ? "-1" : ((DateTime)SDMS_ICTicket.ReachedDate).Hour.ToString();
            if (SDMS_ICTicket.ReachedDate != null)
            {
                int ReachedMMMinute = ((DateTime)SDMS_ICTicket.ReachedDate).Minute;
                int adjustment = ReachedMMMinute % 5;
                if (adjustment != 0)
                {
                    ReachedMMMinute = (ReachedMMMinute - adjustment) + 5;
                }
                ddlReachedMM.SelectedValue = ReachedMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlReachedMM.SelectedValue = "0";
            }
            
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
          //  lblHMRValue.Text = "Current HMR Value" + " ( " + SDMS_ICTicket.Equipment.EquipmentModel.Division.UOM + " ) ";
            txtHMRDate.Text = SDMS_ICTicket.CurrentHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.CurrentHMRDate).ToShortDateString();
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
            //    btnSave.Focus();
        }
       
      public  string ValidationReached(PDMS_ICTicket SDMS_ICTicket)
        {
            string Message = "";
            txtReachedDate.BorderColor = Color.Silver;
            txtLocation.BorderColor = Color.Silver;

            if (string.IsNullOrEmpty(txtDepartureDate.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the Departure Date"; 
            }
            if (ddlDepartureHH.SelectedValue == "-1")
            {
                Message = Message + "<br/> Please Enter the Departure Hour"; 
            }
            if (ddlDepartureMM.SelectedValue == "0")
            {
                Message = Message + "<br/> Please Enter the Departure Minute"; 
            }


            if (string.IsNullOrEmpty(txtReachedDate.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Reached Date"; 
                txtReachedDate.BorderColor = Color.Red;
            }
            else
            {
                if (ddlReachedHH.SelectedValue == "-1")
                {
                    Message = Message + "<br/>Please select the Reached Hour"; 
                    ddlReachedHH.BorderColor = Color.Red;
                }

                if (ddlReachedMM.SelectedValue == "0")
                {
                    Message = Message + "<br/>Please select the Reached Minute"; 
                    ddlReachedMM.BorderColor = Color.Red;
                }

            }

            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Location"; 
                txtLocation.BorderColor = Color.Red;
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
                    Message = Message + "<br/> Please enter integer in HMR Value"; 
                    txtHMRValue.BorderColor = Color.Red;
                }
                if (SDMS_ICTicket.LastHMRValue > Convert.ToInt32(txtHMRValue.Text.Trim()))
                {
                    Message = Message + "<br/>HMR value should not be less than last HMR value."; 
                    txtHMRValue.BorderColor = Color.Red;
                }
            }

            if (!string.IsNullOrEmpty(txtHMRDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtReachedDate.Text.Trim()) > Convert.ToDateTime(txtHMRDate.Text.Trim()))
                {
                    Message = Message + "<br/>HMR date should not be less than Reached date."; 
                    txtHMRDate.BorderColor = Color.Red;
                }
            }
            return Message; 
        }

        string ValidatetionRestore(PDMS_ICTicket SDMS_ICTicket)
        {
            ddlServiceType.BorderColor = Color.Silver;
            ddlServicePriority.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver;
            txtHMRDate.BorderColor = Color.Silver;
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


            if (string.IsNullOrEmpty(txtHMRDate.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the HMR Date"; 
                txtHMRDate.BorderColor = Color.Red;
            }
            else
            {
                if (Convert.ToDateTime(txtReachedDate.Text.Trim()) > Convert.ToDateTime(txtHMRDate.Text.Trim()))
                {
                    Message = Message + "<br/>HMR date should not be less than Reached date."; 
                    txtHMRDate.BorderColor = Color.Red;
                }
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
             
            return "";

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
            //if (string.IsNullOrEmpty(txtReachedDate.Text.Trim()))
            //{

            //    txtHMRDate.Text = "";
            //    txtHMRDate.Enabled = false;
            //    return;
            //}
            //ceHMRDate.StartDate = Convert.ToDateTime(txtReachedDate.Text);
            //ceHMRDate.EndDate = DateTime.Now;

            // if (SS_ServiceCharge.Count + SS_ServiceMaterial.Count == 0)
            //{
            //    txtHMRDate.Enabled = true;
            //}
        }
        public void EnableOrDesableBasedOnServiceCharges()
        { 

            //string ClaimNumber = "";
            //if (SS_ServiceCharge.Count != 0)
            //{ ClaimNumber = SS_ServiceCharge[0].ClaimNumber; }

            //if (string.IsNullOrEmpty(ClaimNumber)) { ddlServiceType.Enabled = true; }
            //else { ddlServiceType.Enabled = false; }

            //string QuotationNumber = "";
            //if (SS_ServiceMaterial.Count != 0)
            //{
            //    foreach (PDMS_ServiceMaterial ServiceMaterial in SS_ServiceMaterial)
            //    {
            //        QuotationNumber = ServiceMaterial.QuotationNumber;
            //        if (!string.IsNullOrEmpty(QuotationNumber)) { break; }
            //    }
            //}
            //if (string.IsNullOrEmpty(QuotationNumber))
            //{
            //    ddlDealerOffice.Enabled = true;
            //    txtHMRDate.Enabled = true;
            //    txtHMRValue.Enabled = true;
            //    txtReachedDate.Enabled = true;
            //    ddlReachedHH.Enabled = true;
            //    ddlReachedMM.Enabled = true;
            //}
            //else
            //{
            //    ddlDealerOffice.Enabled = false;
            //    txtHMRDate.Enabled = false;
            //    txtHMRValue.Enabled = false;
            //    txtReachedDate.Enabled = false;
            //    ddlReachedHH.Enabled = false;
            //    ddlReachedMM.Enabled = false;
            //}

            //UC_BasicInformation.SDMS_ICTicket = SDMS_ICTicket;
            //UC_BasicInformation.FillBasicInformation();
            //WarrantyCalculation();
        }

        void WarrantyCalculation()
        {
            //CheckBox UCcbIsWarranty = (CheckBox)UC_BasicInformation.FindControl("cbIsWarranty");
            //if (SDMS_ICTicket.Equipment.IsAMC == true)
            //{
            //}
            //else if ((SDMS_ICTicket.Equipment.IsRefurbished == true) && (SDMS_ICTicket.Equipment.RefurbishedBy == SDMS_ICTicket.Dealer.DealerID) && (SDMS_ICTicket.Equipment.RFWarrantyExpiryDate >= SDMS_ICTicket.ICTicketDate))
            //{
                
            //}
            //else if (SDMS_ICTicket.IsMarginWarranty == true)
            //{
               
            //}
            //else if (SDMS_ICTicket.Equipment.EquipmentModel.Division.UOM == "Cum")
            //{
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
            //    {

            //        Boolean Old = UCcbIsWarranty.Checked;
            //        int vHMR = 2000;
            //        DateTime cD = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).AddYears(-1);
            //        if (cD < Convert.ToDateTime("01/11/2018"))
            //        {
            //            vHMR = 1000;
            //        }

            //        if (SDMS_ICTicket.Equipment.EquipmentWarrantyType != null)
            //        {
            //            if (SDMS_ICTicket.Equipment.EquipmentWarrantyType.HMR != 0)
            //            {
            //                vHMR = SDMS_ICTicket.Equipment.EquipmentWarrantyType.HMR;
            //            }
            //        }

            //        if ((Convert.ToInt32(txtHMRValue.Text.Trim()) > vHMR) || (SDMS_ICTicket.Equipment.WarrantyExpiryDate < SDMS_ICTicket.ICTicketDate))
            //        {
            //            UCcbIsWarranty.Checked = false;
            //        }
            //        else
            //        {
            //            UCcbIsWarranty.Checked = true;
            //        }
            //        if (Old != UCcbIsWarranty.Checked)
            //        {
            //            FillGetServiceType(Convert.ToInt32(UCcbIsWarranty.Checked));
            //        }
            //    }
            //}
        }

        protected void ddlSubApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}