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
    public partial class ICTicketAddMaterialCharges : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster(List<PDMS_ICTicketTSIR> ICTicketTSIRs)
        {
            ddlMaterialSource.DataTextField = "MaterialSource";
            ddlMaterialSource.DataValueField = "MaterialSourceID";
            ddlMaterialSource.DataSource = new BDMS_Service().GetMaterialSource(null, null);
            ddlMaterialSource.DataBind();
            ddlMaterialSource.Items.Insert(0, new ListItem("Select", "0"));


            ddlTSIRNumber.DataTextField = "TsirNumber";
            ddlTSIRNumber.DataValueField = "TsirID";


            List<PDMS_ICTicketTSIR> ddlTSIR = new List<PDMS_ICTicketTSIR>();
            foreach (PDMS_ICTicketTSIR t in ICTicketTSIRs)
            {
                if ((t.Status.StatusID != (short)TSIRStatus.Canceled))
                {
                    ddlTSIR.Add(new PDMS_ICTicketTSIR() { TsirID = t.TsirID, TsirNumber = t.TsirNumber });
                }
            }
            ddlTSIRNumber.DataSource = ddlTSIR;
            ddlTSIRNumber.DataBind();
            ddlTSIRNumber.Items.Insert(0, new ListItem("Select", "0"));
        }

        void Clear()
        {
           

        }
        public PDMS_ServiceMaterial Read()
        {  
            PDMS_ServiceMaterial SM = new PDMS_ServiceMaterial();  
            SM.Material = new PDMS_Material() { MaterialSerialNumber = txtMaterialSN.Text.Trim() };
            SM.Qty = Convert.ToInt32(txtQty.Text.Trim());
            SM.IsFaultyPart = cbIsFaultyPart.Checked;
            SM.DefectiveMaterial = new PDMS_Material() { MaterialSerialNumber = txtDefectiveMaterialSN.Text.Trim() }; 
            SM.IsRecomenedParts = cbRecomenedParts.Checked;
            SM.IsQuotationParts = cbQuotationParts.Checked;
            SM.MaterialSource = ddlMaterialSource.SelectedValue == "0" ? null : new PDMS_MaterialSource() { MaterialSourceID = Convert.ToInt32(ddlMaterialSource.SelectedValue) };

            SM.TSIR = null;
            if (ddlTSIRNumber.SelectedValue != "0")
            {
                SM.TSIR = new PDMS_ICTicketTSIR() { TsirID = Convert.ToInt64(ddlTSIRNumber.SelectedValue) };
                SM.IsRecomenedParts = true;
            } 
            return SM;
        }   
        public string Validation()
        {
            string Message = "";
            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                Message = "Please enter the Qty";
                return Message;
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                Message = "Please enter correct format in Qty";
                return Message;
            }
            return Message;
        }
    }
}