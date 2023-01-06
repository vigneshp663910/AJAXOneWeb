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
        public PDMS_ServiceMaterial_API Read()
        {
            PDMS_ServiceMaterial_API SM = new PDMS_ServiceMaterial_API();
            SM.MaterialID = Convert.ToInt32(hdfMaterialID.Value);
            SM.MaterialSerialNumber = txtMaterialSN.Text.Trim();
            SM.SupersedeYN = cbSupersedeYN.Checked;
            SM.Qty = Convert.ToInt32(txtQty.Text.Trim());
            SM.IsFaultyPart = cbIsFaultyPart.Checked;

            SM.DefectiveMaterialID = Convert.ToInt32(hdfDefectiveMaterialID.Value);
            SM.DefectiveMaterialSerialNumber = txtDefectiveMaterialSN.Text.Trim();

            SM.IsRecomenedParts = cbRecomenedParts.Checked;
            SM.IsQuotationParts = cbQuotationParts.Checked;
            SM.MaterialSource = ddlMaterialSource.SelectedValue == "0" ? null : new PDMS_MaterialSource() { MaterialSourceID = Convert.ToInt32(ddlMaterialSource.SelectedValue) };

            SM.TsirID = ddlTSIRNumber.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlTSIRNumber.SelectedValue);
            SM.OldInvoice = txtOldInvoice.Text.Trim();
            return SM;
        }   
        public string Validation()
        {
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                return "Please select the Material";
            }
            if (string.IsNullOrEmpty(hdfDefectiveMaterialID.Value))
            {
                return "Please select the Defective Material";
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
               return "Please enter the Qty"; 
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty"; 
            }
            return "";
        }
    }
}