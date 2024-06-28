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
            int RowCount = 0;
            List<PDMS_Equipment> EquipmentList = new List<PDMS_Equipment>();
            string[] EquipS = txtEquipment.Text.Split(',');
            foreach (string Equipment in EquipS)
            {
                new BAPI().ApiGetWithOutToken("Sap/InsertMissingEquipment?EquipmentSerNo=" + Equipment.Trim());
                List<PDMS_Equipment> Material = new BDMS_Equipment().GetEquipmentHeader(null, Equipment.Trim(), null, null, null, null, null, null, PSession.User.UserID, 1, 10, out RowCount);
                if (Material.Count == 1)
                {
                    EquipmentList.Add(Material[0]);
                }
            }

            //  new BAPI().ApiGetWithOutToken("Sap/InsertMissingEquipment?EquipmentSerNo=" + txtEquipment.Text.Trim());

            // gvEquipment.DataSource = new BDMS_Equipment().GetEquipmentHeader(null, txtEquipment.Text.Trim(), null, null, null, null, null, null, PSession.User.UserID, 1, 10, out RowCount);
            gvEquipment.DataSource = EquipmentList;
            gvEquipment.DataBind();
        }
    }
}