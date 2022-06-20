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
    public partial class ICTicketAddOtherMachine : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        { 
            new BDMS_AvailabilityOfOtherMachine().GetTypeOfMachine(ddlTypeOfMachine, null, null); 
            new BDMS_AvailabilityOfOtherMachine().GetMake(ddlMake, null, null);
        }
        
        void Clear()
        {


        }
        public PDMS_AvailabilityOfOtherMachine Read()
        {
            PDMS_AvailabilityOfOtherMachine OM = new PDMS_AvailabilityOfOtherMachine();
            OM.TypeOfMachine = new PDMS_TypeOfMachine() { TypeOfMachineID = Convert.ToInt32(ddlTypeOfMachine.SelectedValue) };
            OM.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            OM.Make = new PMake() { MakeID = Convert.ToInt32(ddlMake.SelectedValue) };
            return OM;
        }
        public string Validation()
            {
                string Message = "";
                if (ddlTypeOfMachine.SelectedValue == "0")
                {
                    Message = "Please select the Note Type";
                    return Message;
                }
                if (ddlMake.SelectedValue == "0")
                {
                    Message = "Please select the Make";
                    return Message;
                }

                if (string.IsNullOrEmpty(txtQuantity.Text))
                {
                    Message = "Please enter the Quantity";
                    return Message;
                }
                return Message;
            }
    }
}