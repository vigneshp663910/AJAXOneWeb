using Business;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace DealerManagementSystem.Account
{
    public partial class CompanyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Company Profile');</script>");

            PUser userDetails = (PUser)Session["userDetails"];
            //FillDealerEmployee(Convert.ToInt32(userDetails.DealerEmployeeID));        
            //FillDealerEmployeeRole(Convert.ToInt32(userDetails.DealerEmployeeID));
            FillDealerEmployee(1450);
            FillDealerEmployeeRole(1450);
            //FillDealerEmployee(1166);
            //FillDealerEmployeeRole(1166);
        }
        void FillDealerEmployee(int DealerEmployeeID)
        {
            PDMS_DealerEmployee Emp = new BDMS_Dealer().GetDealerEmployeeByDealerEmployeeID(DealerEmployeeID);
            //lblDateOfJoining.Text = Convert.ToString(Emp.DateOfJoining);
            //if (Emp.DealerDepartment != null)
            //{
            //    lblDepartment.Text = Emp.DealerDepartment.DealerDepartment;
            //}
            //if (Emp.DealerDesignation != null)
            //{
            //    lblDesignation.Text = Emp.DealerDesignation.DealerDesignation;
            //}
            //if (Emp.ReportingTo != null)
            //{
            //    lblReportingTo.Text = Emp.ReportingTo.Name;
            //}
        

            if (Emp.Photo != null)
            {

                ViewState["PhotoAttachedFileID"] = Emp.Photo.AttachedFileID;

                //PDMS_DealerEmployeeAttachedFile PHFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.Photo.AttachedFileID);
                //if (PHFile.FileName != null)
                //{
                //    string Url = "DealerEmpPhotos/" + (Emp.DealerEmployeeID).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                //    if (File.Exists(MapPath(Url)))
                //    {
                //        File.Delete(MapPath(Url));
                //    }
                //    FileSave(PHFile, (Emp.DealerEmployeeID).ToString());
                //    ibtnPhoto.ImageUrl = Url;
                //}
            }
            ibtnPhoto.Visible = true;
        }
        private void FillDealerEmployeeRole(int DealerEmployeeID)
        {
            List<PDMS_DealerEmployeeRole> Role = new BDMS_Dealer().GetDealerEmployeeRole(null, DealerEmployeeID, null, null);
            PDMS_Customer Dealer = new PDMS_Customer();
            if (Role[0].Dealer.DealerCode == "2000")
            {
                Dealer = new BDMS_Customer().GetCustomerAE();
            }
            else 
            {
                Dealer = new SCustomer().getCustomerAddress(Role[0].Dealer.DealerCode);
            }

            
            lblCompanyName.Text = Role[0].Dealer.DealerName;
            lblCompanyCode.Text = Role[0].DealerOffice.OfficeName;
            lblAddress.Text = Dealer.Address1;
            lblCountry.Text = Dealer.GSTIN;
            lblState.Text = Dealer.State.State;
            lblCity.Text = Dealer.City;
            lblPincode.Text = Dealer.Pincode;

            lblContactNo1.Text = "";
            lblContactNo2.Text = "";
            lblEmail.Text = Dealer.Email;
            lblMobileNumber.Text = Dealer.Mobile;
            lblRegistrationDate.Text = "";
            lblActivationDate.Text = "";
            lblURL.Text = "";
            if (Role[0].DealerOffice.State != null)
            {
                lblState.Text = Role[0].DealerOffice.State;
                lblCountry.Text = Role[0].DealerOffice.Country;
            }
        }
    }
}