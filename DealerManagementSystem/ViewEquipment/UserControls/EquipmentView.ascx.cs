using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace DealerManagementSystem.ViewEquipment.UserControls
{
    public partial class EquipmentView : System.Web.UI.UserControl
    {

        public PDMS_Equipment EquipmentViewDet
        {
            get
            {
                if (Session["EquipmentView"] == null)
                {
                    Session["EquipmentView"] = new PDMS_Equipment();
                }
                return (PDMS_Equipment)Session["EquipmentView"];
            }
            set
            {
                Session["EquipmentView"] = value;
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

            lblMessage.Text = "";
        }
        public void fillEquipment(long EquipmentHeaderID)
        {
            ViewState["EquipmentHeaderID"] = EquipmentHeaderID;

            EquipmentViewDet = new BDMS_Equipment().GetEquipmentHeaderByID(Convert.ToInt32(EquipmentHeaderID));
            EquipmentViewDet.Customer = new BDMS_Customer().GetCustomerByID(EquipmentViewDet.Customer.CustomerID);
            lblModel.Text = EquipmentViewDet.EquipmentModel.Model;
            lblModelDescription.Text = EquipmentViewDet.EquipmentModel.ModelDescription;
            lblEngineSerialNo.Text = EquipmentViewDet.EngineSerialNo;
            lblEquipmentSerialNo.Text = EquipmentViewDet.EquipmentSerialNo;
            lblDistrict.Text = EquipmentViewDet.Customer.District.District;
            lblState.Text = EquipmentViewDet.Customer.State.State;
            lblDispatchedOn.Text = EquipmentViewDet.DispatchedOn == null ? "" : ((DateTime)EquipmentViewDet.DispatchedOn).ToLongDateString();
            lblWarrantyExpiryDate.Text = EquipmentViewDet.WarrantyExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.WarrantyExpiryDate).ToLongDateString();
            lblEngineModel.Text = EquipmentViewDet.EngineModel;
            lblCurrentHMRValue.Text = EquipmentViewDet.CurrentHMRValue.ToString();
            lblCommisioningOn.Text = EquipmentViewDet.CommissioningOn == null ? "" : ((DateTime)EquipmentViewDet.CommissioningOn).ToLongDateString();
            lblCurrentHMRDate.Text = EquipmentViewDet.CurrentHMRDate == null ? "" : ((DateTime)EquipmentViewDet.CurrentHMRDate).ToLongDateString();
            //EquipmentViewDet.IsRefurbished;
            //EquipmentViewDet.RefurbishedBy;
            lblRFWarrantyStartDate.Text = EquipmentViewDet.RFWarrantyStartDate == null ? "" : ((DateTime)EquipmentViewDet.RFWarrantyStartDate).ToLongDateString();
            lblRFWarrantyExpiryDate.Text = EquipmentViewDet.RFWarrantyExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.RFWarrantyExpiryDate).ToLongDateString();
            cbIsAMC.Checked = EquipmentViewDet.IsAMC == null ? false : Convert.ToBoolean(EquipmentViewDet.IsAMC);
            lblAMCStartDate.Text = EquipmentViewDet.AMCStartDate == null ? "" : ((DateTime)EquipmentViewDet.AMCStartDate).ToLongDateString();
            lblAMCExpiryDate.Text = EquipmentViewDet.AMCExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.AMCExpiryDate).ToLongDateString();
            lblTypeOfWheelAssembly.Text = EquipmentViewDet.TypeOfWheelAssembly;
            lblMaterialCode.Text = EquipmentViewDet.Material.MaterialCode;
            lblChassisSlNo.Text = EquipmentViewDet.ChassisSlNo;
            lblESN.Text = EquipmentViewDet.ESN;
            lblPlant.Text = EquipmentViewDet.Plant;
            lblSpecialVariants.Text = EquipmentViewDet.SpecialVariants;
            lblProductionStatus.Text = EquipmentViewDet.ProductionStatus;
            lblVariantsFittingDate.Text = EquipmentViewDet.VariantsFittingDate == null ? "" : ((DateTime)EquipmentViewDet.VariantsFittingDate).ToLongDateString();
            lblManufacturingDate.Text = EquipmentViewDet.ManufacturingDate;
            lblInstalledBaseNo.Text = EquipmentViewDet.Ibase.InstalledBaseNo;
            lblIBaseLocation.Text = EquipmentViewDet.Ibase.IBaseLocation;
            lblDeliveryDate.Text = EquipmentViewDet.Ibase.DeliveryDate == null ? "" : ((DateTime)EquipmentViewDet.Ibase.DeliveryDate).ToLongDateString();
            lblIBaseCreatedOn.Text = EquipmentViewDet.Ibase.IBaseCreatedOn == null ? "" : ((DateTime)EquipmentViewDet.Ibase.IBaseCreatedOn).ToLongDateString();
            lblIbaseWarrantyStart.Text = EquipmentViewDet.Ibase.WarrantyStart == null ? "" : ((DateTime)EquipmentViewDet.Ibase.WarrantyStart).ToLongDateString();
            lblIbaseWarrantyEnd.Text = EquipmentViewDet.Ibase.WarrantyEnd == null ? "" : ((DateTime)EquipmentViewDet.Ibase.WarrantyEnd).ToLongDateString();
            lblFinancialYearOfDispatch.Text = EquipmentViewDet.Ibase.FinancialYearOfDispatch.ToString();
            lblMajorRegion.Text = EquipmentViewDet.Ibase.MajorRegion.Region;

            CustomerViewSoldTo.fillCustomer(EquipmentViewDet.Customer);
            ActionControlMange();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);

            if (lbActions.Text == "Add Effort")
            {

            }
            else if (lbActions.Text == "Add Expense")
            {

            }
            else if (lbActions.Text == "Status Change to Close")
            {

            }
            else if (lbActions.Text == "Status Change to Cancel")
            {

            }
            else if (lbActions.Text == "Add Activity")
            {


            }
        }


        void ActionControlMange()
        {

        }
    }
}