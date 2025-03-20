using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class DealerCreate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void Clear()
        {
            txtDealerCode.Text = "";
            txtContactName.Text = "";
            txtDisplayName.Text = "";
            txtGSTIN.Text = "";
            txtPAN.Text = "";
            txtContactPerson.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            ddlDealerType.SelectedValue = "0";
            
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            ddlDistrict.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlCountry.SelectedValue = "0";
            txtPincode.Text = "";

            txtBank.Text = "";
            txtBranch.Text = "";
            txtAccountNo.Text = "";
            txtIFSCCode.Text = "";

            txtOfficeCode.Text = "";
            txtOfficeName.Text = "";
            txtSapLocationCode.Text = "";

            cbEInvoice.Checked = false;
            cxEInvoiceDate.StartDate = DateTime.Now;
            txtApiUserName.Text = "";
            txtApiPassword.Text = "";
            cbServicePaidEInvoice.Checked = false;

            //FillMaster();
        }
        public void FillMaster()
        {
            new DDLBind(ddlDealerType, new BDMS_Dealer().GetDealerType(null, null), "DealerType", "DealerTypeID");
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            PDealer Dealer = PSession.User.Dealer[0];
            int CountryID = Dealer.Country.CountryID;
            ddlCountry.SelectedValue = Convert.ToString(CountryID);
            //new DDLBind(ddlState, new BDMS_Address().GetState(Dealer.DealerID, CountryID, null, null, null), "State", "StateID");
            new DDLBind(ddlState, new BDMS_Address().GetState(null, CountryID, null, null, null), "State", "StateID");
            cxEInvoiceDate.StartDate = DateTime.Now;
            //txtEInvoiceDate.Text = DateTime.Now.ToShortDateString();
            
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            // List<PDMS_State> State = new BDMS_Address().GetState(Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            List<PDMS_State> State = new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            new DDLBind(ddlState, State, "State", "StateID");
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue == "1") { txtPincode.MaxLength = 6; } else { txtPincode.MaxLength = 10; }

        }
        public string ValidationDealer()
        {
            long longCheck;

            string Message = "";
            txtDealerCode.BorderColor = Color.Silver;
            txtContactName.BorderColor = Color.Silver;
            txtDisplayName.BorderColor = Color.Silver;
            txtGSTIN.BorderColor = Color.Silver;
            txtPAN.BorderColor = Color.Silver;
            txtContactPerson.BorderColor = Color.Silver;
            txtEmail.BorderColor = Color.Silver;
            txtMobile.BorderColor = Color.Silver;
            ddlDealerType.BorderColor = Color.Silver;

            txtAddress1.BorderColor = Color.Silver;
            txtAddress2.BorderColor = Color.Silver;
            txtAddress3.BorderColor = Color.Silver;
            txtCity.BorderColor = Color.Silver;
            txtPincode.BorderColor = Color.Silver;

            ddlCountry.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;

            txtBank.BorderColor = Color.Silver;
            txtBranch.BorderColor = Color.Silver;
            txtAccountNo.BorderColor = Color.Silver;
            txtIFSCCode.BorderColor = Color.Silver;

            cbEInvoice.Checked = false;
            txtEInvoiceDate.BorderColor = Color.Silver;
            txtApiUserName.BorderColor = Color.Silver;
            txtApiPassword.BorderColor = Color.Silver;
            cbServicePaidEInvoice.Checked = false;

            txtSapLocationCode.BorderColor = Color.Silver;
            txtOfficeCode.BorderColor = Color.Silver;
            txtOfficeName.BorderColor = Color.Silver;

            Regex regex = new Regex(@"^[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[a-zA-Z0-9]{3}$");

            if (string.IsNullOrEmpty(txtDealerCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Dealer Code.";
                txtDealerCode.BorderColor = Color.Red;
                //return Message;
            }

            if (string.IsNullOrEmpty(txtContactName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Dealer Name.";
                txtContactName.BorderColor = Color.Red;
                //return Message;
            }

            if(string.IsNullOrEmpty(txtDisplayName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Dealer Short Name.";
                txtDisplayName.BorderColor = Color.Red;
                //return Message;
            }

            if(!string.IsNullOrEmpty(txtGSTIN.Text.Trim()))
            {
                if ((!regex.Match(txtGSTIN.Text.Trim()).Success) && (txtGSTIN.Text.Trim() != "URD"))
                {
                    Message = Message + "GST Number " + txtGSTIN.Text.Trim() + " is not correct.";
                    txtGSTIN.BorderColor = Color.Red;
                    return Message;
                }
                if ((txtGSTIN.Text.Trim() != "URD") && (!string.IsNullOrEmpty(txtGSTIN.Text.Trim())))
                {
                    string gst = txtGSTIN.Text.Trim().Remove(0, 2).Substring(0, 10);
                    if (txtPAN.Text.Trim().ToUpper() != gst.ToUpper())
                    {
                        Message = Message + "<br/>PAN and GSTIN are not matching.";
                        txtPAN.BorderColor = Color.Red;
                        return Message;
                    }
                }
            }

            //if (string.IsNullOrEmpty(txtContactPerson.Text.Trim()))
            //{
            //    Message = "<br/>Please enter the Contact Person.";
            //    txtContactPerson.BorderColor = Color.Red;
            //    return Message;
            //}
            //if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            //{
            //    Message = "<br/>Please enter the Email.";
            //    txtEmail.BorderColor = Color.Red;
            //    return Message;
            //}
            //if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            //{
            //    Message = "<br/>Please enter the Mobile.";
            //    txtMobile.BorderColor = Color.Red;
            //    return Message;
            //}
            //if (txtMobile.Text.Trim().Length != 10)
            //{
            //    Message = Message + "<br/>Mobile Length should be 10 digit.";
            //    txtMobile.BorderColor = Color.Red;
            //    return Message;
            //}
            if (!string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                if (!long.TryParse(txtMobile.Text.Trim(), out longCheck))
                {
                    Message = Message + "<br/>Mobile should be 10 digit.";
                    txtMobile.BorderColor = Color.Red;
                    return Message;
                }
            }
            if (ddlDealerType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Dealer Type.";
                ddlDealerType.BorderColor = Color.Red;
                //return Message;
            }
            if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Address1.";
                txtAddress1.BorderColor = Color.Red;
                //return Message;
            }
            if (ddlCountry.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Country.";
                ddlCountry.BorderColor = Color.Red;
                //return Message;
            }
            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the State.";
                ddlState.BorderColor = Color.Red;
                //return Message;
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the District.";
                ddlDistrict.BorderColor = Color.Red;
                //return Message;
            }
            
            if (string.IsNullOrEmpty(txtCity.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the City.";
                txtCity.BorderColor = Color.Red;
                //return Message;
            }
            if (string.IsNullOrEmpty(txtPincode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Pincode.";
                txtPincode.BorderColor = Color.Red;
                //return Message;
            }
            if (string.IsNullOrEmpty(txtBank.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Bank.";
                txtBank.BorderColor = Color.Red;
                //return Message;
            }
            if (string.IsNullOrEmpty(txtBranch.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Branch.";
                txtBranch.BorderColor = Color.Red;
                //return Message;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Account No.";
                txtAccountNo.BorderColor = Color.Red;
                //return Message;
            }
            if (string.IsNullOrEmpty(txtIFSCCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the IFSC Code.";
                txtIFSCCode.BorderColor = Color.Red;
                //return Message;
            }
            if (!string.IsNullOrEmpty(txtPincode.Text.Trim()))
            {
                if (!long.TryParse(txtPincode.Text.Trim(), out longCheck))
                {
                    Message = Message + "<br/>Pincode should be in digit.";
                    txtPincode.BorderColor = Color.Red;
                    //return Message;
                }
            }
            if(cbEInvoice.Checked)
            {
                if(string.IsNullOrEmpty(txtApiUserName.Text.Trim()))
                {
                    Message = Message + "<br/>Please enter API Username.";
                    txtApiUserName.BorderColor = Color.Red;
                    return Message;
                }
                if(string.IsNullOrEmpty(txtApiPassword.Text.Trim()))
                {
                    Message = Message + "<br/>Please enter API Password.";
                    txtApiPassword.BorderColor = Color.Red;
                    return Message;
                }                
            }

            if(string.IsNullOrEmpty(txtSapLocationCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the SAP Location Code.";
                txtSapLocationCode.BorderColor = Color.Red;
                //return Message;
            }
            else if(string.IsNullOrEmpty(txtOfficeCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Office Code.";
                txtOfficeCode.BorderColor = Color.Red;
                //return Message;
            }
            else if(string.IsNullOrEmpty(txtOfficeName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Office Name.";
                txtOfficeName.BorderColor = Color.Red;
                //return Message;
            }

            return Message;
        }
        public PDealer_Insert ReadDealer()
        {
            PDealer_Insert Dealer = new PDealer_Insert();

            Dealer.DealerCode = txtDealerCode.Text.Trim();
            Dealer.DealerName = txtContactName.Text.Trim();
            Dealer.DealerShortName = txtDisplayName.Text.Trim();
            Dealer.GSTIN = txtGSTIN.Text.Trim().ToUpper();
            Dealer.PAN = txtPAN.Text.Trim().ToUpper();
            Dealer.ContactPerson = txtContactPerson.Text.Trim();            
            Dealer.Email = txtEmail.Text.Trim();
            Dealer.Mobile = txtMobile.Text.Trim();
            Dealer.DealerTypeID = Convert.ToInt32(ddlDealerType.SelectedValue);

            Dealer.Address1 = txtAddress1.Text.Trim();
            Dealer.Address2 = txtAddress2.Text.Trim();
            Dealer.Address3 = txtAddress3.Text.Trim();
            Dealer.City = txtCity.Text.Trim();
            Dealer.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            Dealer.StateID = Convert.ToInt32(ddlState.SelectedValue);
            Dealer.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            Dealer.Pincode = txtPincode.Text.Trim();

            Dealer.BankName = txtBank.Text.Trim();
            Dealer.Branch = txtBranch.Text.Trim().Trim();
            Dealer.AcNumber = txtAccountNo.Text.Trim();
            Dealer.IfscCode = txtIFSCCode.Text.Trim();

            Dealer.OfficeCode = txtOfficeCode.Text.Trim();
            Dealer.OfficeName = txtOfficeName.Text.Trim();
            Dealer.SapLocationCode = txtSapLocationCode.Text.Trim();

            Dealer.IsEInvoice = cbEInvoice.Checked;
            Dealer.EInvoiceDate = String.IsNullOrEmpty(txtEInvoiceDate.Text) ? (DateTime?) null : Convert.ToDateTime(txtEInvoiceDate.Text.Trim());
            Dealer.APIUsername = txtApiUserName.Text.Trim();
            Dealer.APIPassword = txtApiPassword.Text.Trim();
            Dealer.IsServicePaidEInvoice = cbServicePaidEInvoice.Checked;
            
            return Dealer;
        }
    }
}