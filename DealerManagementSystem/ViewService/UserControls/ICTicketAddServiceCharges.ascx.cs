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
        public void FillMaster()
        {

        }

        void Clear()
        {


        }
        public PDMS_ServiceCharge Read()
        {
            PDMS_ServiceCharge OM = new PDMS_ServiceCharge();
            OM.ServiceChargeID = 0;
            OM.ICTicketID = 0;
            OM.IsDeleted = false;
            
            PDMS_Material MaterialsDescription = new BDMS_Material().GetMaterialServiceByMaterialAndDescription(txtServiceMaterial.Text.Trim());

            OM.Material = new PDMS_Material() { MaterialID = MaterialsDescription.MaterialID };
            OM.Date = Convert.ToDateTime(txtServiceDate.Text);
            OM.WorkedHours = Convert.ToDecimal(txtWorkedHours.Text.Trim());
            OM.BasePrice = Convert.ToDecimal(txtBasePrice.Text.Trim());
            OM.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
            return OM;
        }
        public string Validation()
        {
            string Message = "";

            return Message;
        }
    }
}