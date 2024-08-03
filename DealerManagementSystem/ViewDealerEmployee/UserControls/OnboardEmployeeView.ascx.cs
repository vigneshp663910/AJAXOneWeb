using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee.UserControls
{
    public partial class OnboardEmployeeView : System.Web.UI.UserControl
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public POnboardEmployee Emp
        {
            get
            {
                if (Session["OnboardEmployeeByID"] == null)
                {
                    Session["OnboardEmployeeByID"] = new POnboardEmployee();
                }
                return (POnboardEmployee)Session["OnboardEmployeeByID"];
            }
            set
            {
                Session["OnboardEmployeeByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }
        public void FillOnboardEmployee(int OnboardEmployeeID)
        {
            Emp = new BOnboardEmployee().GetOnboardEmployeeByID(OnboardEmployeeID);
            lblEmpCode.Text = Emp.EmpCode;
            lblName.Text = Emp.Name;
            lblFatherName.Text = Emp.FatherName;
            lblDOB.Text = Convert.ToString(Emp.DOB);
            lblContactNumber1.Text = "<a href='tel:" + Emp.ContactNumber1 + "'>" + Emp.ContactNumber1 + "</a>";
            lblContactNumber2.Text = "<a href='tel:" + Emp.ContactNumber2 + "'>" + Emp.ContactNumber2 + "</a>";
            lblEmail.Text = "<a href='mailto:" + Emp.EmailID + "'>" + Emp.EmailID + "</a>";
            lblEducationalQualification.Text = (Emp.EducationalQualification == null)? "" : Emp.EducationalQualification.EqucationalQualification;
            lblTotalExperience.Text = Convert.ToString(Emp.TotalExperience);
            lblEmergencyContact.Text = Emp.EmergencyContactNumber;
            if (Emp.BloodGroup != null)
            {
                lblBloodGroup.Text = Emp.BloodGroup.BloodGroup;
            }
            lblAddress.Text = Emp.Address;
            if (Emp.State != null)
            {
                lblState.Text = Emp.State.State;
                if (Emp.District != null)
                {
                    lblDistrict.Text = Emp.District.District;
                    if (Emp.Tehsil != null)
                    {
                        lblTehsil.Text = Emp.Tehsil.Tehsil;
                    }
                }
            }
            lblVillage.Text = Emp.Village;
            lblLocation.Text = Emp.Location;
            lblDealer.Text = Emp.Dealer.DealerName;
            lblDealerOffice.Text = Emp.DealerOffice.OfficeName;
            lblDOJ.Text = Emp.DateOfJoining.ToString();
            lblDepartment.Text = Emp.DealerDepartment.DealerDepartment;
            lblDesignation.Text = Emp.DealerDesignation.DealerDesignation;
            lblReportingTo.Text = Emp.ReportingTo.Name;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtModulePermission.Text))
                {
                    lblMessage.Text = "Please Enter Module Permission";                    
                    return;
                }
                if (string.IsNullOrEmpty(txtDealerPermission.Text))
                {
                    lblMessage.Text = "Please Enter Dealer Permission";
                    return;
                }
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    lblMessage.Text = "Please Enter Approver Remarks";
                    return;
                }
                bool Success = new BOnboardEmployee().ApproveOnboardEmployee(Emp.OnboardEmployeeID, txtModulePermission.Text.Trim(), txtDealerPermission.Text.Trim(), txtRemarks.Text.Trim(), true);
                if (Success == true)
                {
                    lblMessage.Text = "Onboard Employee Successfully Approved.";
                    lblMessage.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                return;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtModulePermission.Text))
                {
                    lblMessage.Text = "Please Enter Module Permission";
                    return;
                }
                if (string.IsNullOrEmpty(txtDealerPermission.Text))
                {
                    lblMessage.Text = "Please Enter Dealer Permission";
                    return;
                }
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    lblMessage.Text = "Please Enter Approver Remarks";
                    return;
                }
                bool Success = new BOnboardEmployee().ApproveOnboardEmployee(Emp.OnboardEmployeeID, txtModulePermission.Text.Trim(), txtDealerPermission.Text.Trim(), txtRemarks.Text.Trim(), false);
                if (Success == true)
                {
                    lblMessage.Text = "Onboard Employee Successfully Rejected.";
                    lblMessage.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                return;
            }
        }
    }
}