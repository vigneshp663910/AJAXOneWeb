using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class MaterialView : System.Web.UI.UserControl
    {
        public PDMS_Material MaterialByID
        {
            get
            {
                if (ViewState["MaterialByID"] == null)
                {
                    ViewState["MaterialByID"] = new PDMS_Material();
                }
                return (PDMS_Material)ViewState["MaterialByID"];
            }
            set
            {
                ViewState["MaterialByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillMaterialByID(int? MaterialID)
        {
            ViewState["MaterialID"] = MaterialID;
            MaterialByID = new BDMS_Material().GetMaterialListSQL(MaterialID, "", null, null, null)[0];
            lblMaterialCode.Text = MaterialByID.MaterialCode;
            lblMaterialDesc.Text = MaterialByID.MaterialDescription;
            lblUOM.Text = MaterialByID.BaseUnit;
            lblMaterialType.Text = MaterialByID.MaterialType;
            lblDivisionCode.Text = MaterialByID.Model.Division.DivisionCode;
            lblModeCode.Text = MaterialByID.Model.ModelCode;
            lblModel.Text = MaterialByID.Model.Model;
            lblModelDesc.Text = MaterialByID.Model.ModelDescription;
            lblGrossWt.Text = MaterialByID.GrossWeight.ToString();
            lblNetWt.Text = MaterialByID.NetWeight.ToString();
            lblWtUnit.Text = MaterialByID.WeightUnit;
            lblDiv.Text = MaterialByID.MaterialDivision;
            lblHSN.Text = MaterialByID.HSN;
            lblCSTPer.Text = MaterialByID.CST.ToString();
            lblSSTPer.Text = MaterialByID.SST.ToString();
            lblGSTPer.Text = MaterialByID.GST.ToString();

            //ActionControlMange();
        }

        protected void lnkBtnActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Add Drawing")
            {
                MPE_AddDrawing.Show();
            }            
        }
        protected void btnAddFile_Click(object sender, EventArgs e)
        {

        }
        protected void btnAddDrawing_Click(object sender, EventArgs e)
        {

        }
    }
}