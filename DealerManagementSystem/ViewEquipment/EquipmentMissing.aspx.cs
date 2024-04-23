using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewEquipment
{
    public partial class EquipmentMissing : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewEquipment_EquipmentMissing; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("Sap/InsertMissingEquipment?EquipmentSerNo=" + txtEquipment.Text.Trim());
            int RowCount = 0;
            gvEquipment.DataSource = new BDMS_Equipment().GetEquipmentHeader(null, txtEquipment.Text.Trim(), null, null, null, null, null, null, PSession.User.UserID, 1, 10, out RowCount);
            gvEquipment.DataBind();
        }
    }
}