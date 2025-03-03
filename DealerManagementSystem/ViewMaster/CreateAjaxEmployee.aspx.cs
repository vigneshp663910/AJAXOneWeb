using Business;
using Newtonsoft.Json;
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
    public partial class CreateAjaxEmployee : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_CreateAjaxEmployee; } }
        public string AadhaarCardNo
        {
            get
            {
                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }
        public List<POnboardEmployeeDealer_Insert> DealerList
        {
            get
            {
                if (ViewState["DealerList"] == null)
                {
                    ViewState["DealerList"] = new List<POnboardEmployeeDealer_Insert>();
                }
                return (List<POnboardEmployeeDealer_Insert>)ViewState["DealerList"];
            }
            set
            {
                ViewState["DealerList"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('OE Employee » Create / Manpower Registration');</script>");

            if (!IsPostBack)
            {
                ViewState["DealerEmployeeID"] = null;
                new BDMS_Address().GetStateDDL(ddlState, 1, null, null, null);
                new BDMS_Dealer().GetEqucationalQualificationDDL(ddlEqucationalQualification, null, null);
                new BDMS_Dealer().GetBloodGroupDDL(ddlBloodGroup, null, null);

                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                // int DealerID =  new BDMS_Dealer().GetDealer(null,"2000", null, null)[0].DealerID;                
                if (!string.IsNullOrEmpty(Request.QueryString["DealerEmployeeID"]))
                {
                    ViewState["DealerEmployeeID"] = Convert.ToInt32(Request.QueryString["DealerEmployeeID"]);
                    FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                    btnBack.Visible = true;
                }
                if (Session["OnboardEmployeeToAjaxID"] != null)
                {
                    FillDealerEmployee(Convert.ToInt32(Session["OnboardEmployeeToAjaxID"]));
                    fldDealerPermission.Visible = true;
                    btnBack.Visible = true;
                }
                else
                {
                    fillDealer();
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

            PAjaxEmployee_Insert DEmp = new PAjaxEmployee_Insert();
            DEmp.Name = txtName.Text.Trim();
            DEmp.FatherName = txtFatherName.Text.Trim();
            DEmp.DOB = string.IsNullOrEmpty(txtDOB.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDOB.Text.Trim());
            DEmp.ContactNumber = txtContactNumber.Text.Trim();
            DEmp.ContactNumber1 = txtContactNumber1.Text.Trim();
            DEmp.Email = txtEmail.Text.Trim();
            DEmp.Address = txtAddress.Text.Trim();

            if (ddlState.SelectedValue != "0")
            {
                DEmp.StateID = Convert.ToInt32(ddlState.SelectedValue); ;
            }
            if (ddlDistrict.SelectedValue != "0")
            {
                DEmp.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            }
            if (ddlTehsil.SelectedValue != "0")
            {
                DEmp.TehsilID = Convert.ToInt32(ddlTehsil.SelectedValue);
            }

            DEmp.Village = txtVillage.Text.Trim();
            DEmp.Location = txtLocation.Text.Trim();
            

            if (ddlEqucationalQualification.SelectedValue != "0")
            {
                DEmp.EducationalQualificationID = Convert.ToInt32(ddlEqucationalQualification.SelectedValue);
            }
            DEmp.TotalExperience = string.IsNullOrEmpty(txtTotalExperience.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtTotalExperience.Text.Trim());
            DEmp.EmergencyContactNumber = txtEmergencyContactNumber.Text.Trim();
            if (ddlBloodGroup.SelectedValue != "0")
            {
                DEmp.BloodGroupID = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            }
            DEmp.AadhaarCardNo = txtAadhaarCardNo.Text.Trim();
            DEmp.OfficeCodeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            DEmp.DateOfJoining = Convert.ToDateTime(txtDateOfJoining.Text.Trim());
            DEmp.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            DEmp.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlReportingTo.SelectedValue != "0")
            {
                DEmp.ReportingTo = Convert.ToInt32(ddlReportingTo.SelectedValue);
            }

            PApiResult Result = new PApiResult();
            bool Success = false;
            if (Session["OnboardEmployeeToAjaxID"] != null)
            {                
                DEmp.ModulePermission = txtModulePermission.Text.Trim();
                DEmp.ApprovedRemark = txtRemarks.Text.Trim();                
                DEmp.StatusId = (short)OnboardEmployeeStatus.Created;

                Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/InsertorUpdateOnboardEmployeeDealerPermission", DealerList));
                Success = Convert.ToBoolean(Result.Data);
                if (Success == true)
                {
                    Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/InsertOrUpdateOnboardUserCreation", DEmp));
                    int DealerEmployeeID = Convert.ToInt32(Result.Data);
                    new BDMS_Dealer().ApproveDealerEmployee(DealerEmployeeID, PSession.User.UserID);
                    if (DealerEmployeeID != 0)
                    {
                        Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/InsertOrUpdateOnboardUserDealerPermission", DealerList));
                        Success = Convert.ToBoolean(Result.Data);
                        if (Success == true)
                        {
                            lblMessage.Text = "Employee userid is Created successfully";
                            lblMessage.ForeColor = Color.Green;
                            btnSave.Visible = false;
                        }
                        else
                        {
                            lblMessage.Text = "Employee userid is Created and Dealer Permission not updated successfully";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Employee userid is not created successfully";
                    }
                }
            }
            else
            {
                DEmp.AjaxEmployeeID = ViewState["DealerEmployeeID"] == null ? 0 : (int)ViewState["DealerEmployeeID"];

                Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/InsertOrUpdateOnboardUserCreation", DEmp));
                int DealerEmployeeID = Convert.ToInt32(Result.Data);

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
            }
            btnSave.Focus();
        }

        void FillDealerEmployee(int AjaxEmployeeID)
        {
            PAjaxEmployee Emp = new PAjaxEmployee();

            if (Session["OnboardEmployeeToAjaxID"]!=null)
            {
                Emp = new BOnboardEmployee().GetOnboardEmployeeByGenerateAjaxEmployee(AjaxEmployeeID);
                ddlDealer.SelectedValue = Convert.ToString(Emp.DealerEmployeeRole.Dealer.DealerID);
                fillDealer();
                ddlDealerOffice.SelectedValue = Convert.ToString(Emp.DealerEmployeeRole.DealerOffice.OfficeID);
                ddlDepartment.SelectedValue = Convert.ToString(Emp.DealerEmployeeRole.DealerDepartment.DealerDepartmentID);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
                ddlDesignation.SelectedValue = Convert.ToString(Emp.DealerEmployeeRole.DealerDesignation.DealerDesignationID);
                ddlReportingTo.SelectedValue = Emp.DealerEmployeeRole.ReportingTo == null ? "0" : Convert.ToString(Emp.DealerEmployeeRole.ReportingTo.DealerEmployeeID);
                txtDateOfJoining.Text = Convert.ToString(Emp.DealerEmployeeRole.DateOfJoining);
                txtModulePermission.Text = Emp.ModulePermission;
                txtRemarks.Text = Emp.ApprovedRemark;

                DealerList = new List<POnboardEmployeeDealer_Insert>();
                foreach (PDealer Dealer in Emp.DealerPermissionList)
                {
                    POnboardEmployeeDealer_Insert D = new POnboardEmployeeDealer_Insert();
                    D.OnboardEmployeeID = Emp.AjaxEmployeeID;
                    D.DealerID = Dealer.DealerID;
                    D.IsActive = true;
                    DealerList.Add(D);
                }

                List<PDMS_Dealer> DealerList1 = new BDMS_Dealer().GetDealer(null, "", null, null);
                ListViewDealer.DataSource = DealerList1;
                ListViewDealer.DataBind();

                foreach (ListViewItem item in ListViewDealer.Items)
                {
                    CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                    Label lblDID = (Label)item.FindControl("lblDID");
                    bool containsItem = DealerList.Any(x => x.DealerID == Convert.ToInt32(lblDID.Text) && x.OnboardEmployeeID == Emp.AjaxEmployeeID && x.IsActive == true);
                    if (containsItem)
                    {
                        chkDealer.Checked = true;
                    }
                }
            }
            else
            {
                Emp = new BOnboardEmployee().GetDealerEmployeeByDealerEmployeeID(AjaxEmployeeID);
                ViewState["DealerEmployeeID"] = Emp.AjaxEmployeeID;
                if (Emp.DealerEmployeeRole != null)
                {
                    FillDealerEmployeeRole(Emp.DealerEmployeeRole.DealerEmployeeRoleID);
                }
            }
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
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["OnboardEmployeeToAjaxID"] != null)
            {
                Session["OnboardEmployeeToAjaxID"] = null;
                string url = "~/ViewDealerEmployee/OnboardEmployeeManage.aspx";
                Response.Redirect(url);
            }
            else
            {
                string url = "DealerEmployeeApproval.aspx";
                Response.Redirect(url);
            }
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

            if (Session["OnboardEmployeeToAjaxID"] != null)
            {
                if (string.IsNullOrEmpty(txtModulePermission.Text))
                {
                    Message = Message + "<br/>Please enter module permission";
                    Ret = false;
                }
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    Message = Message + "<br/>Please enter Remark";
                    Ret = false;
                }
                if (DealerList.Count == 0)
                {
                    Message = Message + "<br/>Please select dealer permission";
                    Ret = false;
                }
            }

            lblMessage.Text = Message;
            return Ret;
        }
        protected void txtAadhaarCardNo_TextChanged(object sender, EventArgs e)
        {
            if (new BDMS_Dealer().GetDealerEmployeeManage(null, AadhaarCardNo, null, null, "", null, null, null, null).Count() != 0)
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
            ddlDealer.SelectedValue = Convert.ToString(Role.Dealer.DealerID);
            fillDealer();
            ddlDealerOffice.SelectedValue = Convert.ToString(Role.DealerOffice.OfficeID);
            ddlDepartment.SelectedValue = Convert.ToString(Role.DealerDepartment.DealerDepartmentID);
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
            ddlDesignation.SelectedValue = Convert.ToString(Role.DealerDesignation.DealerDesignationID);
            ddlReportingTo.SelectedValue = Role.ReportingTo == null ? "0" : Convert.ToString(Role.ReportingTo.DealerEmployeeID);
            txtDateOfJoining.Text = Convert.ToString(Role.DateOfJoining);
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDealer();
        }
        void fillDealer()
        {
            int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            //new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, DealerID);
            List<PDMS_DealerEmployee> Employee = new BDMS_Dealer().GetDealerEmployeeByDealerID(DealerID, null, null, null, null);
            ddlReportingTo.DataValueField = "DealerEmployeeID";
            ddlReportingTo.DataTextField = "Name";
            ddlReportingTo.DataSource = Employee;
            ddlReportingTo.DataBind();
            ddlReportingTo.Items.Insert(0, new ListItem("Select", "0"));
            FillGetDealerOffice(DealerID);
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                DealerList = new List<POnboardEmployeeDealer_Insert>();
                foreach (ListViewItem item in ListViewDealer.Items)
                {
                    CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                    Label lblDID = (Label)item.FindControl("lblDID");
                    chkDealer.Checked = true;
                    POnboardEmployeeDealer_Insert D = new POnboardEmployeeDealer_Insert();
                    D.OnboardEmployeeID = Convert.ToInt32(Session["OnboardEmployeeToAjaxID"]);
                    D.DealerID = Convert.ToInt32(lblDID.Text);
                    D.IsActive = true;
                    DealerList.Add(D);
                }
            }
            else
            {
                DealerList = new List<POnboardEmployeeDealer_Insert>();
                foreach (ListViewItem item in ListViewDealer.Items)
                {
                    CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                    Label lblDID = (Label)item.FindControl("lblDID");
                    chkDealer.Checked = false;
                    POnboardEmployeeDealer_Insert D = new POnboardEmployeeDealer_Insert();
                    D.OnboardEmployeeID = Convert.ToInt32(Session["OnboardEmployeeToAjaxID"]);
                    D.DealerID = Convert.ToInt32(lblDID.Text);
                    D.IsActive = false;
                    DealerList.Add(D);
                }
            }
        }
        protected void chkDealer_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkDealer = (CheckBox)sender;
            Label lblDID = (Label)chkDealer.FindControl("lblDID");
            if (!chkDealer.Checked)
            {
                chkSelectAll.Checked = false;
            }
            bool containsItem = DealerList.Any(item => item.DealerID == Convert.ToInt32(lblDID.Text) && item.OnboardEmployeeID == Convert.ToInt32(Session["OnboardEmployeeToAjaxID"]));
            if (containsItem)
            {
                DealerList.Where(w => w.DealerID == Convert.ToInt32(lblDID.Text)).Select(w => { w.IsActive = chkDealer.Checked; return w; }).ToList();
            }
            else
            {
                POnboardEmployeeDealer_Insert D = new POnboardEmployeeDealer_Insert();
                D.OnboardEmployeeID = Convert.ToInt32(Session["OnboardEmployeeToAjaxID"]);
                D.DealerID = Convert.ToInt32(lblDID.Text);
                D.IsActive = chkDealer.Checked;
                DealerList.Add(D);
            }
        }
    }
}