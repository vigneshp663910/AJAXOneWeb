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
        public long ServiceChargeID
        {
            get
            {
                if (ViewState["ServiceChargeID"] == null)
                {
                    ViewState["ServiceChargeID"] = Convert.ToInt64("0");
                }
                return (long)ViewState["ServiceChargeID"];
            }
            set
            {
                ViewState["ServiceChargeID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";

        }
        public void FillMaster(PDMS_ICTicket SDMS_ICTicket)
        {
            Clear();
            ServiceChargeID = 0;
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
            txtWorkedHours.Text = "";
            txtBasePrice.Text = "";
            txtDiscount.Text = "0";

            txtServiceDate.Text = "";
            txtServiceMaterial.Text = "";
            hdfMaterialID.Value = "0";

        }
        public PDMS_ServiceCharge_API Read()
        { 
            PDMS_ServiceCharge_API OM = new PDMS_ServiceCharge_API();
            OM.ServiceChargeID = ServiceChargeID; 
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
            if (txtWorkedHours.Visible == true)
            {
                if (string.IsNullOrEmpty(txtWorkedHours.Text.Trim()))
                {
                    return "Please enter the Worked Hours";
                }
                if (string.IsNullOrEmpty(txtBasePrice.Text.Trim()))
                {
                    return "Please enter the Base Price";
                }
                if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                {
                    return "Please enter the Discount";
                }

                decimal value;
                if (!decimal.TryParse(txtWorkedHours.Text, out value))
                {
                    return "Please enter correct format in Worked Hours";
                }
                if (!decimal.TryParse(txtBasePrice.Text, out value))
                {
                    return "Please enter correct format in Base Price";
                }
                if (!decimal.TryParse(txtDiscount.Text, out value))
                {
                    return "Please enter correct format in Discount";
                }   
            }
            return "";
        }

        public void Write(PDMS_ServiceCharge OM)
        {
            ServiceChargeID = OM.ServiceChargeID;
            
            hdfMaterialID.Value = OM.Material.MaterialID.ToString();
            txtServiceMaterial.Text = OM.Material.MaterialCode_MaterialDescription;
            txtServiceDate.Text = Convert.ToString(OM.Date);
            if (txtWorkedHours.Visible == true)
            {
                txtWorkedHours.Text = Convert.ToString(OM.WorkedHours);
                txtBasePrice.Text = Convert.ToString(OM.BasePrice);
                txtDiscount.Text = Convert.ToString(OM.Discount);
            }
            if (OM.Material.IsMainServiceMaterial)
            {
                throw new Exception("You cannot edit main Service Material (" + OM.Material.MaterialCode + ").");
            }
        }
    }
}