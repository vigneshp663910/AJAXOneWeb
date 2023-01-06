using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class ICTicketAddServiceCharges : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(PDMS_ICTicket SDMS_ICTicket)
        { 
           
            //if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
            //  || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
            //  || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
            //  || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.AMC)
            //  )
            //{
            //    txtWorkedHours.Visible = true;
            //    txtBasePrice.Visible = true;
            //    txtDiscount.Visible = true;
            //}

            if (SDMS_ICTicket.ServiceType.ManualPriceForService)
            {
                txtWorkedHours.Visible = true;
                txtBasePrice.Visible = true;
                txtDiscount.Visible = true;
            }
            else
            {
                txtWorkedHours.Visible = false;
                txtBasePrice.Visible = false;
                txtDiscount.Visible = false;
            }
        }

        void Clear()
        {


        }
        public PDMS_ServiceCharge_API Read()
        {
            PDMS_ServiceCharge_API OM = new PDMS_ServiceCharge_API();
            OM.ServiceChargeID = 0; 
            OM.IsDeleted = false; 
            //  PDMS_Material MaterialsDescription = new BDMS_Material().GetMaterialServiceByMaterialAndDescription(txtServiceMaterial.Text.Trim()); 
            OM.MaterialID = Convert.ToInt32(hdfMaterialID.Value); 
            //OM.MaterialWithDescription = txtServiceMaterial.Text.Trim();
            OM.Date = Convert.ToDateTime(txtServiceDate.Text);
            if (txtWorkedHours.Visible == true)
            {
                OM.WorkedHours = Convert.ToDecimal(txtWorkedHours.Text.Trim());
                OM.BasePrice = Convert.ToDecimal(txtBasePrice.Text.Trim());
                OM.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
            }
             
            return OM;
        }
        public string Validation()
        { 
            if(string.IsNullOrEmpty( hdfMaterialID.Value))
            {
                return "Please select the Material";
            }
            if (string.IsNullOrEmpty(txtServiceDate.Text.Trim()))
            {
                return "Please select the Service Date";
            }
            return "";
        }
    }
}