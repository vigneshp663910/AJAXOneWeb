using Business;
using Newtonsoft.Json;
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
            try
            {
                Emp = new BOnboardEmployee().GetOnboardEmployeeByID(OnboardEmployeeID);
                lblEmpCode.Text = Emp.EmpCode;
                lblName.Text = Emp.Name;
                lblFatherName.Text = Emp.FatherName;
                lblDOB.Text = Convert.ToString(Emp.DOB);
                lblContactNumber1.Text = "<a href='tel:" + Emp.ContactNumber1 + "'>" + Emp.ContactNumber1 + "</a>";
                lblContactNumber2.Text = "<a href='tel:" + Emp.ContactNumber2 + "'>" + Emp.ContactNumber2 + "</a>";
                lblEmail.Text = "<a href='mailto:" + Emp.EmailID + "'>" + Emp.EmailID + "</a>";
                lblEducationalQualification.Text = (Emp.EducationalQualification == null) ? "" : Emp.EducationalQualification.EqucationalQualification;
                lblTotalExperience.Text = Convert.ToString(Emp.TotalExperience);
                lblEmergencyContact.Text = Emp.EmergencyContactNumber;

                lblBloodGroup.Text = "";
                if (Emp.BloodGroup != null)
                {
                    lblBloodGroup.Text = Emp.BloodGroup.BloodGroup;
                }
                lblAddress.Text = Emp.Address;

                lblState.Text = "";
                lblDistrict.Text = "";
                lblTehsil.Text = "";
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

                lblApprovedBy.Text = "";
                lblApprovedOn.Text = "";
                if (Emp.ApprovedOn != null)
                {
                    lblApprovedBy.Text = Emp.Approver.ContactName;
                    lblApprovedOn.Text = Emp.ApprovedOn.ToString();
                }
                lblStatus.Text = Emp.Status.Status;
                lblModulePermission.Text = Emp.ModulePermission;
                lblDealerPermission.Text = Emp.DealerPermission;
                lblApproverRemarks.Text = Emp.ApprovedRemark;
                ActionControlMange();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }

        void Approve()
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtModulePermission.Text))
                {
                    lblMessage.Text = "Please enter module permission.";
                    return;
                }
                if (string.IsNullOrEmpty(txtDealerPermission.Text))
                {
                    lblMessage.Text = "Please enter dealer permission.";
                    return;
                }
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    lblMessage.Text = "Please enter remarks";
                    return;
                }
                bool Success = new BOnboardEmployee().ApproveOnboardEmployee(Emp.OnboardEmployeeID, txtModulePermission.Text.Trim(), txtDealerPermission.Text.Trim(), txtRemarks.Text.Trim(), true, (short)OnboardEmployeeStatus.Approved);
                if (Success == true)
                {
                    lblMessage.Text = "Onboard employee successfully approved.";
                    lblMessage.ForeColor = Color.Green;
                }
                FillOnboardEmployee(Emp.OnboardEmployeeID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }

        void Reject()
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    lblMessage.Text = "Please enter remarks.";
                    return;
                }
                bool Success = new BOnboardEmployee().ApproveOnboardEmployee(Emp.OnboardEmployeeID, txtModulePermission.Text.Trim(), txtDealerPermission.Text.Trim(), txtRemarks.Text.Trim(), false, (short)OnboardEmployeeStatus.Rejected);
                if (Success == true)
                {
                    lblMessage.Text = "Onboard employee successfully rejected.";
                    lblMessage.ForeColor = Color.Green;
                }
                FillOnboardEmployee(Emp.OnboardEmployeeID);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        void GenerateUser()
        {
            try
            {
                lblMessage.ForeColor = Color.Red;

                POnboardEmployee_Insert DEmp = new POnboardEmployee_Insert();
                DEmp.Name = Emp.Name;
                DEmp.FatherName = Emp.FatherName;
                DEmp.DOB = Emp.DOB;
                DEmp.ContactNumber1 = Emp.ContactNumber1;
                DEmp.ContactNumber2 = Emp.ContactNumber2;
                DEmp.EmailID = Emp.EmailID;
                DEmp.Address = Emp.Address;
                if (Emp.State != null)
                {
                    DEmp.StateID = Emp.State.StateID;
                }
                if (Emp.District != null)
                {
                    DEmp.DistrictID = Emp.District.DistrictID;
                }
                if (Emp.Tehsil != null)
                {
                    DEmp.TehsilID = Emp.Tehsil.TehsilID;
                }
                DEmp.Village = Emp.Village;
                DEmp.Location = Emp.Location;
                DEmp.EmpCode = Emp.EmpCode;
                if (Emp.EducationalQualification != null)
                {
                    DEmp.EducationalQualificationID = Emp.EducationalQualification.EqucationalQualificationID;
                }
                DEmp.TotalExperience = (Emp.TotalExperience != null) ? (decimal?)null : Emp.TotalExperience;
                DEmp.EmergencyContactNumber = Emp.EmergencyContactNumber;
                if (Emp.BloodGroup != null)
                {
                    DEmp.BloodGroupID = Emp.BloodGroup.BloodGroupID;
                }
                DEmp.OfficeCodeID = Emp.DealerOffice.OfficeID;
                DEmp.DateOfJoining = Emp.DateOfJoining;
                DEmp.DealerDepartmentID = Emp.DealerDepartment.DealerDepartmentID;
                DEmp.DealerDesignationID = Emp.DealerDesignation.DealerDesignationID;
                DEmp.ReportingTo = Emp.ReportingTo.DealerEmployeeID;
                DEmp.StatusId = (short)OnboardEmployeeStatus.Created;

                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/InsertOrUpdateOnboardUserCreation", DEmp));
                int DealerEmployeeID = Convert.ToInt32(Result.Data);
                new BDMS_Dealer().ApproveDealerEmployee(DealerEmployeeID, PSession.User.UserID);
                if (DealerEmployeeID != 0)
                {
                    lblMessage.Text = "Employee user is updated successfully";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Employee user is not updated successfully";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                LinkButton lbActions = (LinkButton)sender;
                if (lbActions.ID == "lbApprove")
                {
                    Approve();
                }
                else if (lbActions.ID == "lbReject")
                {
                    Reject();
                }
                else if (lbActions.ID == "lbGenerate")
                {
                    GenerateUser();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        void ActionControlMange()
        {
            lbApprove.Visible = true;
            lbReject.Visible = true;
            lbGenerate.Visible = true;
            DivApprover.Visible = true;

            PApiResult Result = new BOnboardEmployee().GetOnboardEmployeePendingApproval(null, null, null, null, null, null);

            if (!JsonConvert.DeserializeObject<List<POnboardEmployee>>(JsonConvert.SerializeObject(Result.Data)).Any(item => item.ReportingTo.DealerEmployeeID == PSession.User.DealerEmployeeID && item.OnboardEmployeeID == Emp.OnboardEmployeeID && item.Status.StatusId == (short)OnboardEmployeeStatus.Requested))
            {
                lbApprove.Visible = false;
                lbReject.Visible = false;
                DivApprover.Visible = false;
            }
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (Emp.Status.StatusId != (short)OnboardEmployeeStatus.Approved || SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.OnboardEmployeeGenerateUser).Count() == 0)
            {
                lbGenerate.Visible = false;
            }
        }
    }
}