using Properties;
using System;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketCommonBasicInfo : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SDMS_ICTicket != null)
                    FillBasicInformation();
            }

        }
        public void FillBasicInformation()
        {

            lblICTicket.Text = SDMS_ICTicket.ICTicketNumber + " - " + SDMS_ICTicket.ICTicketDate;
            lblDealer.Text = SDMS_ICTicket.Dealer.DealerCode + " - " + SDMS_ICTicket.Dealer.DealerName;
            lblCustomer.Text = SDMS_ICTicket.Customer.CustomerCode + " - " + SDMS_ICTicket.Customer.CustomerName;
            lblStatus.Text = SDMS_ICTicket.ServiceStatus.ServiceStatus;
            lblRequestedDate.Text = SDMS_ICTicket.RequestedDate == null ? "" : (DateTime)SDMS_ICTicket.RequestedDate + "";
            cbIsWarranty.Checked = SDMS_ICTicket.IsWarranty;
            cbIsMarginWarranty.Checked = SDMS_ICTicket.IsMarginWarranty;

            lblWarrantyExpiry.Text = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString();
            lblDistrict.Text = SDMS_ICTicket.Address.District.District + " - " + SDMS_ICTicket.Address.State.State;
            lblContactPerson.Text = SDMS_ICTicket.ContactPerson + " " + SDMS_ICTicket.PresentContactNumber;
            lblComplaintDescription.Text = SDMS_ICTicket.ComplaintDescription;
            lblInformation.Text = SDMS_ICTicket.Information;
            lblOldICTicketNumber.Text = SDMS_ICTicket.OldICTicketNumber;
            lblEquipment.Text = SDMS_ICTicket.Equipment.EquipmentSerialNo;
            lblModel.Text = SDMS_ICTicket.Equipment.EquipmentModel.Model;
            lblLastHMRValue.Text = SDMS_ICTicket.LastHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.LastHMRDate).ToShortDateString() + "  " + Convert.ToString(SDMS_ICTicket.LastHMRValue);
        }
    }
}