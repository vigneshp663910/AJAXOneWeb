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
            List<PDMS_Material> MaterialList = new List<PDMS_Material>();
            string[] Materials = txtMaterialCode.Text.Split(',');
            foreach (string MaterialCode in Materials)
            {
                new BAPI().ApiGetWithOutToken("Material/MaterialIntegrationFromSap?MaterialCode=" + MaterialCode.Trim());
                List<PDMS_Material> Material = new BDMS_Material().GetMaterialListSQL(null, MaterialCode.Trim(), null, null, null);
                if (Material.Count == 1)
                {
                    MaterialList.Add(Material[0]);
                }
            }
            gvMaterial.DataSource = MaterialList;
            gvMaterial.DataBind();
        }
    }
}