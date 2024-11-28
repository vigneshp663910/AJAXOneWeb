using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class OnboardEmployeeRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["OnboardEmployeeID"] = null;
                new DDLBind(ddlState, new BOnboardEmployee().GetState(null, null, null, null, null), "State", "StateID", true, "Select");
                new BDMS_Dealer().GetEqucationalQualificationDDL(ddlEducationalQualification, null, null);
                new BDMS_Dealer().GetBloodGroupDDL(ddlBloodGroup, null, null);

                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);

                if (!string.IsNullOrEmpty(Request.QueryString["OnboardEmployeeID"]))
                {
                    ViewState["OnboardEmployeeID"] = Convert.ToInt32(Request.QueryString["OnboardEmployeeID"]);
                }
                else
                {
                    fillDealer();
                }
            }
        }
        void fillDealer()
        {
            int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, DealerID);
        }
        private void FillGetDealerOffice(int DealerID)
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void txtEmpCode_TextChanged(object sender, EventArgs e)
        {
            PApiResult Result = new BOnboardEmployee().GetOnboardEmployee(txtEmpCode.Text.Trim(), null, null, null, null, null);
            if (JsonConvert.DeserializeObject<List<POnboardEmployee>>(JsonConvert.SerializeObject(Result.Data)).Count() != 0)
            {
                lblMessage.Text = "This Emp Code Already Available";
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
                new DDLBind(ddlDistrict, new BOnboardEmployee().GetDistrict(null, null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID", true, "Select");
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Address().GetTehsil(ddlTehsil, null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationOnboardEmp())
            {
                return;
            }
            POnboardEmployee_Insert Emp = new POnboardEmployee_Insert();
            Emp.OnboardEmployeeID = ViewState["OnboardEmployeeID"] == null ? 0 : (int)ViewState["DealerEmployeeID"];
            Emp.EmpCode = txtEmpCode.Text.Trim();
            Emp.Name = txtName.Text.Trim();
            Emp.FatherName = txtFatherName.Text.Trim();
            Emp.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDOB.Text.Trim());
            Emp.ContactNumber1 = txtContactNumber.Text.Trim();
            Emp.ContactNumber2 = txtContactNumber1.Text.Trim();
            Emp.EmailID = txtEmail.Text.Trim();
            Emp.Address = txtAddress.Text.Trim();
            Emp.StateID = Convert.ToInt32(ddlState.SelectedValue);
            Emp.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            Emp.TehsilID = (ddlTehsil.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlTehsil.SelectedValue);
            Emp.Village = txtVillage.Text.Trim();
            Emp.Location = txtLocation.Text.Trim();
            Emp.EducationalQualificationID = (ddlEducationalQualification.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlEducationalQualification.SelectedValue);
            Emp.TotalExperience = string.IsNullOrEmpty(txtTotalExperience.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtTotalExperience.Text.Trim());
            Emp.EmergencyContactNumber = txtEmergencyContactNumber.Text.Trim();
            Emp.BloodGroupID = (ddlBloodGroup.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlBloodGroup.SelectedValue);
            Emp.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            Emp.OfficeCodeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            Emp.DateOfJoining = Convert.ToDateTime(txtDateOfJoining.Text.Trim());
            Emp.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            Emp.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            Emp.ReportingTo = Convert.ToInt32(ddlReportingTo.SelectedValue);
            Emp.StatusId = (short)OnboardEmployeeStatus.Requested;

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee", Emp));
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            int OnboardEmployeeID = Convert.ToInt32(Result.Data);
            if (OnboardEmployeeID != 0)
            {
                lblMessage.Text = "Onboard Employee is updated successfully";
                lblMessage.ForeColor = Color.Green;
                btnSave.Visible = false;
            }
            else
            {
                lblMessage.Text = "Onboard Employee is not updated successfully";
            }
            btnSave.Focus();
        }
        Boolean ValidationOnboardEmp()
        {
            lblMessage.ForeColor = Color.Red;

            txtEmpCode.BorderColor = Color.Silver;
            txtName.BorderColor = Color.Silver;
            txtFatherName.BorderColor = Color.Silver;
            txtDOB.BorderColor = Color.Silver;
            txtContactNumber.BorderColor = Color.Silver;
            txtEmail.BorderColor = Color.Silver;
            txtTotalExperience.BorderColor = Color.Silver;
            txtAddress.BorderColor = Color.Silver;
            txtLocation.BorderColor = Color.Silver;
            txtEmpCode.BorderColor = Color.Silver;
            ddlEducationalQualification.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            txtDateOfJoining.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver;
            ddlDepartment.BorderColor = Color.Silver;
            ddlDesignation.BorderColor = Color.Silver;
            ddlReportingTo.BorderColor = Color.Silver;

            Boolean Ret = true;
            string Message = "";


            if (string.IsNullOrEmpty(txtEmpCode.Text.Trim()))
            {
                Message = "Please enter the Emp Code";
                Ret = false;
                txtEmpCode.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Name";
                Ret = false;
                txtName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtDOB.Text.Trim()))
            {
                Message = Message + "<br/>Please select DOB";
                Ret = false;
                txtDOB.BorderColor = Color.Red;
            }
            if (txtContactNumber.Text.Trim().Count() != 10)
            {
                Message = Message + "<br/>Please check the Contact Number";
                Ret = false;
                txtContactNumber.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Email";
                Ret = false;
                txtEmail.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            if (!Ret)
            {
                return Ret;
            }
            string email = txtEmail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                Message = Message + "<br/>" + email + " is not correct";
                Ret = false;
                txtEmail.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Address";
                Ret = false;
                txtAddress.BorderColor = Color.Red;
            }
            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select State";
                Ret = false;
                ddlState.BorderColor = Color.Red;
            }

            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select District";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
            }

            long valueLong;

            if (!long.TryParse("0" + txtEmpCode.Text.Trim(), out valueLong))
            {
                Message = Message + "<br/> Please enter integer in Employee Code";
                Ret = false;
                txtEmpCode.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtDateOfJoining.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Date of Joining";
                Ret = false;
                txtDateOfJoining.BorderColor = Color.Red;
            }
            if (ddlDealerOffice.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Dealer Office";
                Ret = false;
                ddlDealerOffice.BorderColor = Color.Red;
            }
            if (ddlDepartment.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Department";
                Ret = false;
                ddlDepartment.BorderColor = Color.Red;
            }
            if (ddlDesignation.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Designation";
                Ret = false;
                ddlDesignation.BorderColor = Color.Red;
            }
            if (ddlReportingTo.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Reporting To";
                Ret = false;
                ddlReportingTo.BorderColor = Color.Red;
            }

            PApiResult Result = new BOnboardEmployee().GetOnboardEmployee(txtEmpCode.Text.Trim(), null, null, null, null, null);
            if (JsonConvert.DeserializeObject<List<POnboardEmployee>>(JsonConvert.SerializeObject(Result.Data)).Count() != 0)
            {
                Message = Message + "<br/>Employee Code Already Available";
                Ret = false;
                txtEmpCode.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            return Ret;
        }
    }
}