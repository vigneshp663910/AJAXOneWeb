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
    public partial class AddFSR : System.Web.UI.UserControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster( PDMS_ICTicketFSR SDMS_ICTicketFSR)
        {
            new BDMS_MachineMaintenanceLevel().GetMachineMaintenanceLevelDDL(ddlMachineMaintenanceLevel, null, null);
            FillGetModeOfPayment();
            FillFSRDetails(SDMS_ICTicketFSR);
        }
        private void FillGetModeOfPayment()
        {
            ddlModeOfPayment.DataTextField = "ModeOfPayment";
            ddlModeOfPayment.DataValueField = "ModeOfPaymentID";
            ddlModeOfPayment.DataSource = new BDMS_ModeOfPayment().GetModeOfPayment(null, null);
            ddlModeOfPayment.DataBind();
            ddlModeOfPayment.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillFSRDetails( PDMS_ICTicketFSR SDMS_ICTicketFSR)
        {
            if (SDMS_ICTicketFSR.FsrID != 0)
            {
                if (SDMS_ICTicketFSR.MachineMaintenanceLevel != null)
                    ddlMachineMaintenanceLevel.SelectedValue = SDMS_ICTicketFSR.MachineMaintenanceLevel.MachineMaintenanceLevelID.ToString();
                cbIsRental.Checked = SDMS_ICTicketFSR.IsRental;
                txtOperatorName.Text = SDMS_ICTicketFSR.OperatorName;
                txtOperatorNumber.Text = SDMS_ICTicketFSR.OperatorNumber;
                txtRentalName.Text = SDMS_ICTicketFSR.RentalName;
                txtRentalNumber.Text = SDMS_ICTicketFSR.RentalNumber; 
                if (SDMS_ICTicketFSR.ModeOfPayment != null)
                    ddlModeOfPayment.SelectedValue = SDMS_ICTicketFSR.ModeOfPayment.ModeOfPaymentID.ToString();
                txtReport.Text = SDMS_ICTicketFSR.Report;
                txtNatureOfComplaint.Text = SDMS_ICTicketFSR.NatureOfComplaint;
                txtObservation.Text = SDMS_ICTicketFSR.Observation;
                txtWorkCarriedOut.Text = SDMS_ICTicketFSR.WorkCarriedOut; 
            }
        }
        void Clear()
        {
             

        }
        public PDMS_ICTicketFSR_M Read()
        {
            PDMS_ICTicketFSR_M FSR = new PDMS_ICTicketFSR_M();
            //FSR.FsrID = SDMS_ICTicketFSR.FsrID;
            //FSR.ICTicketID = SDMS_ICTicket.ICTicketID;
            FSR.IsRental = cbIsRental.Checked;
            FSR.OperatorName = txtOperatorName.Text.Trim();
            FSR.OperatorNumber = txtOperatorNumber.Text.Trim();
            FSR.RentalName = txtRentalName.Text.Trim()  ;
            if (ddlMachineMaintenanceLevel.SelectedValue != "0")
            {
                FSR.MachineMaintenanceLevel = new PDMS_MachineMaintenanceLevel() { MachineMaintenanceLevelID = Convert.ToInt32(ddlMachineMaintenanceLevel.SelectedValue) };
            }
            if (ddlModeOfPayment.SelectedValue != "0")
            {
                FSR.ModeOfPayment = new PDMS_ModeOfPayment() { ModeOfPaymentID = Convert.ToInt32(ddlModeOfPayment.SelectedValue) };
            }
            FSR.Report = txtReport.Text.Trim();
            FSR.RentalNumber = txtRentalNumber.Text.Trim();
            FSR.NatureOfComplaint = txtNatureOfComplaint.Text.Trim();
            FSR.Observation = txtObservation.Text.Trim();
            FSR.WorkCarriedOut = txtWorkCarriedOut.Text.Trim();  
            return FSR;



           // long IsNew = SDMS_ICTicketFSR.FsrID;
            //if (FsrIDP != 0)
            //{
            //    SDMS_ICTicketFSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(FsrIDP, null, null, null, "", null, null, null);
            //    Display();
            //    lblMessage.ForeColor = Color.Green;
            //    string Year = "";
            //    if (SDMS_ICTicketFSR.FSRDate.Month > 3)
            //    {
            //        Year = SDMS_ICTicketFSR.FSRDate.Year.ToString().Substring(2, 2) + "-" + (SDMS_ICTicketFSR.FSRDate.Year + 1).ToString().Substring(2, 2);
            //    }
            //    else
            //    {
            //        Year = (SDMS_ICTicketFSR.FSRDate.Year - 1).ToString().Substring(2, 2) + "" + SDMS_ICTicketFSR.FSRDate.Year.ToString().Substring(2, 2);
            //    }

            //    string FSRNumber = SDMS_ICTicketFSR.ICTicket.Dealer.DealerCode + "/" + SDMS_ICTicketFSR.ICTicket.ICTicketNumber + "/" + SDMS_ICTicketFSR.ICTicket.Technician.UserName + "/" + Year;


            //    if (IsNew == 0)
            //    {
            //        lblMessage.Text = "FSR Number (" + FSRNumber + ") Created Successfully ";
            //    }
            //    else
            //    {
            //        lblMessage.Text = "FSR Number (" + FSRNumber + ") Updated Successfully ";
            //        pnlAttachments.Enabled = true;
            //    }
            //}
            //else
            //{
            //    if (IsNew == 0)
            //    {
            //        lblMessage.Text = "FSR Number is not Created Successfully ";
            //    }
            //    else
            //    {
            //        lblMessage.Text = "FSR is not Updated Successfully ";
            //        lblMessage.ForeColor = Color.Red;
            //    }
            //}
        }
        public string Validation()
        { 
            string Message = ""; 
            PDMS_Material MaterialsDescription = new BDMS_Material().GetMaterialServiceByMaterialAndDescription(txtNatureOfComplaint.Text.Trim()); 
            if (string.IsNullOrEmpty(MaterialsDescription.MaterialCode))
            {
                Message = "Nature of Complaint is not available."; 
            } 
            return Message;
        }
    }
}