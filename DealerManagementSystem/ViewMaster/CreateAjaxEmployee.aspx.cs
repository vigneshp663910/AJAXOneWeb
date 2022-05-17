using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class CreateAjaxEmployee : System.Web.UI.Page
    {
        public string AadhaarCardNo
        {
            get
            {

                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }       
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DealerEmployeeCreate.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)

        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealership Employee » Create / Manpower Registration');</script>");

            if (!IsPostBack)
            {                
                ViewState["DealerEmployeeID"] = null;
                new BDMS_Address().GetState(ddlState, null, null, null, null);
                new BDMS_Dealer().GetEqucationalQualificationDDL(ddlEqucationalQualification, null, null);
                new BDMS_Dealer().GetBloodGroupDDL(ddlBloodGroup, null, null);

                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);

              int DealerID =  new BDMS_Dealer().GetDealer(null,"2000", null)[0].DealerID;
                new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, DealerID);
                FillGetDealerOffice(DealerID);
                if (!string.IsNullOrEmpty(Request.QueryString["DealerEmployeeID"]))
                {
                    ViewState["DealerEmployeeID"] = Convert.ToInt32(Request.QueryString["DealerEmployeeID"]);
                    FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));   
                    btnBack.Visible = true; 
                }
            }
        }
        private void FillGetDealerOffice(int DealerID)
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
                new BDMS_Address().GetDistrict(ddlDistrict, null, null, null, Convert.ToInt32(ddlState.SelectedValue), null, null);
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        { 
                new BDMS_Address().GetTehsil(ddlTehsil, null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationAjaxEmp())
            {
                return;
            }
            PDMS_DealerEmployee Emp = new PDMS_DealerEmployee();
            Emp.DealerEmployeeID = ViewState["DealerEmployeeID"] == null ? 0 : (int)ViewState["DealerEmployeeID"];
            Emp.Name = txtName.Text.Trim();
            Emp.FatherName = txtFatherName.Text.Trim();

            Emp.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDOB.Text.Trim());
            Emp.ContactNumber = txtContactNumber.Text.Trim();
            Emp.ContactNumber1 = txtContactNumber1.Text.Trim();

            Emp.Email = txtEmail.Text.Trim();

            Emp.Address = txtAddress.Text.Trim();
            if (ddlState.SelectedValue != "0")
            {
                Emp.State = new PDMS_State();
                Emp.State.StateID = Convert.ToInt32(ddlState.SelectedValue);
                if (ddlDistrict.SelectedValue != "0")
                {
                    Emp.District = new PDMS_District();
                    Emp.District.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    if (ddlTehsil.SelectedValue != "0")
                    {
                        Emp.Tehsil = new PDMS_Tehsil();
                        Emp.Tehsil.TehsilID = Convert.ToInt32(ddlTehsil.SelectedValue);
                    }
                }
            }
            Emp.Village = txtVillage.Text.Trim();
            Emp.Location = txtLocation.Text.Trim();

            Emp.AadhaarCardNo = AadhaarCardNo;

            if (ddlEqucationalQualification.SelectedValue != "0")
            {
                Emp.EqucationalQualification = new PDMS_EqucationalQualification();
                Emp.EqucationalQualification.EqucationalQualificationID = Convert.ToInt32(ddlEqucationalQualification.SelectedValue);
            }
            Emp.TotalExperience = string.IsNullOrEmpty(txtTotalExperience.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtTotalExperience.Text.Trim());

         
            Emp.EmergencyContactNumber = txtEmergencyContactNumber.Text.Trim();
            if (ddlBloodGroup.SelectedValue != "0")
            {
                Emp.BloodGroup = new PDMS_BloodGroup() { BloodGroupID = Convert.ToInt32(ddlBloodGroup.SelectedValue) };
            }

            PDMS_DealerEmployeeRole Role = new PDMS_DealerEmployeeRole();
            Role.DealerEmployeeID = ViewState["DealerEmployeeID"] == null ? 0 : (int)ViewState["DealerEmployeeID"];
            Role.DealerOffice = new PDMS_DealerOffice();
            Role.DealerOffice.OfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            Role.DateOfJoining = Convert.ToDateTime(txtDateOfJoining.Text.Trim());
            Role.SAPEmpCode = txtAadhaarCardNo.Text.Trim();
            Role.DealerDepartment = new PDMS_DealerDepartment();
            Role.DealerDepartment.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            Role.DealerDesignation = new PDMS_DealerDesignation();
            Role.DealerDesignation.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlReportingTo.SelectedValue != "0")
            {
                Role.ReportingTo = new PDMS_DealerEmployee();
                Role.ReportingTo.DealerEmployeeID = Convert.ToInt32(ddlReportingTo.SelectedValue);
            }

            int DealerEmployeeID = new BDMS_Dealer().InsertOrUpdateAjaxEmployee(Emp, Role, PSession.User.UserID);

            new BDMS_Dealer().ApproveDealerEmployee(DealerEmployeeID, PSession.User.UserID); 
            if (DealerEmployeeID != 0)
            {
                lblMessage.Text = "Employee is updated successfully";
                lblMessage.ForeColor = Color.Green;
                btnSave.Visible = false;
            }
            else
            {
                lblMessage.Text = "Employee is not updated successfully";
            }
            btnSave.Focus();
        }
 
        void FillDealerEmployee(int DealerEmployeeID)
        {
            PDMS_DealerEmployee Emp = new BDMS_Dealer().GetDealerEmployeeByDealerEmployeeID(DealerEmployeeID);
            ViewState["DealerEmployeeID"] = Emp.DealerEmployeeID;
            txtName.Text = Emp.Name;
            txtFatherName.Text = Emp.FatherName;
            txtDOB.Text = Convert.ToString(Emp.DOB);
            txtContactNumber.Text = Emp.ContactNumber;
            txtContactNumber1.Text = Emp.ContactNumber1;
            
            txtEmail.Text = Emp.Email;
            txtAddress.Text = Emp.Address;
            txtLocation.Text = Emp.Location;
            txtAadhaarCardNo.Text = Emp.AadhaarCardNo;
            txtTotalExperience.Text = Convert.ToString(Emp.TotalExperience); 
            
            txtEmergencyContactNumber.Text = Emp.EmergencyContactNumber;
            if (Emp.BloodGroup != null)
            {
                ddlBloodGroup.SelectedValue = Convert.ToString(Emp.BloodGroup.BloodGroupID);
            }
            if (Emp.State != null)
            {
                ddlState.SelectedValue = Convert.ToString(Emp.State.StateID);
                new BDMS_Address().GetDistrict(ddlDistrict, null, null, null, Convert.ToInt32(ddlState.SelectedValue), null, null);
                if (Emp.District != null)
                {
                    ddlDistrict.SelectedValue = Convert.ToString(Emp.District.DistrictID);
                    new BDMS_Address().GetTehsil(ddlTehsil, null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
                    if (Emp.Tehsil != null)
                    {
                        ddlTehsil.SelectedValue = Convert.ToString(Emp.Tehsil.TehsilID);
                    }
                }
            }
            txtVillage.Text = Emp.Village;
            if (Emp.EqucationalQualification != null)
            {
                ddlEqucationalQualification.SelectedValue = Convert.ToString(Emp.EqucationalQualification.EqucationalQualificationID);
            }
            if (Emp.DealerEmployeeRole != null)
            {
                FillDealerEmployeeRole(Emp.DealerEmployeeRole.DealerEmployeeRoleID);
            }
        }        
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string url = "DealerEmployeeApproval.aspx";
            Response.Redirect(url);
        }
 
        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;

            txtName.BorderColor = Color.Silver;
            txtFatherName.BorderColor = Color.Silver;
            txtDOB.BorderColor = Color.Silver;
            txtContactNumber.BorderColor = Color.Silver;
            txtEmail.BorderColor = Color.Silver;

            txtTotalExperience.BorderColor = Color.Silver;
            txtAddress.BorderColor = Color.Silver;
            txtLocation.BorderColor = Color.Silver;
            txtAadhaarCardNo.BorderColor = Color.Silver; 

            ddlEqucationalQualification.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver; 

            Boolean Ret = true;
            string Message = "";

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Message = "Please enter the Name";
                Ret = false;
                txtName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtFatherName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Father Name";
                Ret = false;
                txtFatherName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtDOB.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the DOB";
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

            if (ddlEqucationalQualification.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Equcational Qualification";
                Ret = false;
                ddlEqucationalQualification.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtTotalExperience.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Total Experience";
                Ret = false;
                txtTotalExperience.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Address";
                Ret = false;
                txtAddress.BorderColor = Color.Red;
            }

            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the State";
                Ret = false;
                ddlState.BorderColor = Color.Red;
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the District";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Location";
                Ret = false;
                txtLocation.BorderColor = Color.Red;
            }
            if (AadhaarCardNo.Count() != 12)
            {
                Message = Message + "<br/>Please check the Aadhaar Card No";
                Ret = false;
                txtAadhaarCardNo.BorderColor = Color.Red;
            }

            
           
            lblMessage.Text = Message;
            if (!Ret)
            {
                return Ret;
            }

            decimal value;

            if (!decimal.TryParse("0" + txtTotalExperience.Text, out value))
            {
                Message = Message + "<br/> Please enter integer in TotalExperience";
                Ret = false;
                txtTotalExperience.BorderColor = Color.Red;
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
             
             
            long valueLong;

            if (!long.TryParse("0" + AadhaarCardNo, out valueLong))
            {
                Message = Message + "<br/> Please enter integer in Aadhaar Card No";
                Ret = false;
                txtTotalExperience.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            return Ret;
        }
        Boolean ValidationAjaxEmp()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;


            txtName.BorderColor = Color.Silver;
            txtFatherName.BorderColor = Color.Silver;
            txtDOB.BorderColor = Color.Silver;
            txtContactNumber.BorderColor = Color.Silver;
            txtEmail.BorderColor = Color.Silver;

            txtTotalExperience.BorderColor = Color.Silver;
            txtAddress.BorderColor = Color.Silver;
            txtLocation.BorderColor = Color.Silver;
            txtAadhaarCardNo.BorderColor = Color.Silver; 

            ddlEqucationalQualification.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;

            txtDateOfJoining.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver;
            ddlDepartment.BorderColor = Color.Silver;
            ddlDesignation.BorderColor = Color.Silver;

            Boolean Ret = true;
            string Message = "";

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Message = "Please enter the Name";
                Ret = false;
                txtName.BorderColor = Color.Red;
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

            long valueLong;

            if (!long.TryParse("0" + AadhaarCardNo, out valueLong))
            {
                Message = Message + "<br/> Please enter integer in Aadhaar Card No";
                Ret = false;
                txtTotalExperience.BorderColor = Color.Red;
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
             
            
            lblMessage.Text = Message;
            return Ret;
        }

        protected void txtAadhaarCardNo_TextChanged(object sender, EventArgs e)
        {
            if (new BDMS_Dealer().GetDealerEmployeeManage(null, AadhaarCardNo, null, null, "", null, null).Count() != 0)
            {
                lblMessage.Text = "This Aadhaar Card Already Available";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                txtAadhaarCardNo.Enabled = false;
                btnSave.Visible = false;
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        private void FillDealerEmployeeRole(long DealerEmployeeRoleID)
        {
            PDMS_DealerEmployeeRole Role = new BDMS_Dealer().GetDealerEmployeeRole(DealerEmployeeRoleID, null, null, null)[0];

            ddlDealerOffice.SelectedValue = Convert.ToString(Role.DealerOffice.OfficeID);
            ddlDepartment.SelectedValue = Convert.ToString(Role.DealerDepartment.DealerDepartmentID);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
            ddlDesignation.SelectedValue = Convert.ToString(Role.DealerDesignation.DealerDesignationID);
            ddlReportingTo.SelectedValue = Role.ReportingTo == null ? "0" : Convert.ToString(Role.ReportingTo.DealerEmployeeID);
            txtDateOfJoining.Text = Convert.ToString(Role.DateOfJoining);
        }
    }
}