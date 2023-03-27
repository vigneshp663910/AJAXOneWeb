using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class MaterialSync : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_MaterialSync; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnMaterialSync_Click(object sender, EventArgs e)
        {
            new BAPI().ApiGetWithOutToken("Material/MaterialIntegrationFromSap?MaterialCode=" + txtMaterialCode.Text.Trim());
        }
    }
}