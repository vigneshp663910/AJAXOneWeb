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
                if (ViewState["OnboardEmployeeByID"] == null)
                {
                    ViewState["OnboardEmployeeByID"] = new POnboardEmployee();
                }
                return (POnboardEmployee)ViewState["OnboardEmployeeByID"];
            }
            set
            {
                ViewState["OnboardEmployeeByID"] = value;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }
        public void FillOnboardEmployee(int OnboardEmployeeID, string ViewMode)
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
                lblApproverRemarks.Text = Emp.ApprovedRemark;
                txtModulePermission.Text = Emp.ModulePermission;
                if (Emp.User != null)
                {
                    lblUserName.Text = Emp.User.UserName;
                    lblUserCreatedOn.Text = Emp.User.CreatedOn.ToString();
                }

                DealerList = new List<POnboardEmployeeDealer_Insert>();
                foreach (PDealer Dealer in Emp.DealerList)
                {
                    POnboardEmployeeDealer_Insert D = new POnboardEmployeeDealer_Insert();
                    D.OnboardEmployeeID = Emp.OnboardEmployeeID;
                    D.DealerID = Dealer.DealerID;
                    D.IsActive = true;
                    DealerList.Add(D);
                }
                ListViewDealerList.DataSource = Emp.DealerList;
                ListViewDealerList.DataBind();
                if (ViewMode == "Approve")
                {
                    DivApproverInfo.Visible = false;
                }
                else
                {
                    DivApproverInfo.Visible = true;
                    DivApprover.Visible = false;
                }
                FillDealer();
                ActionControlMange(ViewMode);
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
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    lblMessage.Text = "Please enter remarks";
                    return;
                }
                if (DealerList.Count == 0)
                {
                    lblMessage.Text = "Please select dealer permission.";
                    return;
                }
                POnboardEmployee_Insert employee = new POnboardEmployee_Insert();
                employee.OnboardEmployeeID = Emp.OnboardEmployeeID;
                employee.ModulePermission = txtModulePermission.Text.Trim();
                employee.ApprovedRemark = txtRemarks.Text.Trim();
                employee.IsApproved = true;
                employee.StatusId = (short)OnboardEmployeeStatus.Approved;

                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/InsertorUpdateOnboardEmployeeDealerPermission", DealerList));
                bool Success = Convert.ToBoolean(Result.Data);
                if (Success == true)
                {
                    Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/ApproveOnboardEmployee", employee));
                    Success = Convert.ToBoolean(Result.Data);
                    if (Success == true)
                    {
                        lblMessage.Text = "Onboard employee successfully approved.";
                        lblMessage.ForeColor = Color.Green;
                    }
                }
                FillOnboardEmployee(Emp.OnboardEmployeeID, "View");
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
                POnboardEmployee_Insert employee = new POnboardEmployee_Insert();
                employee.OnboardEmployeeID = Emp.OnboardEmployeeID;
                employee.ModulePermission = txtModulePermission.Text.Trim();
                employee.ApprovedRemark = txtRemarks.Text.Trim();
                employee.IsApproved = false;
                employee.StatusId = (short)OnboardEmployeeStatus.Rejected;

                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("OnboardEmployee/ApproveOnboardEmployee", employee));
                bool Success = Convert.ToBoolean(Result.Data);
                if (Success == true)
                {
                    lblMessage.Text = "Onboard employee successfully rejected.";
                    lblMessage.ForeColor = Color.Green;
                }
                FillOnboardEmployee(Emp.OnboardEmployeeID, "View");
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
                    Session["OnboardEmployeeToAjaxID"] = Emp.OnboardEmployeeID;
                    string url = "~/ViewMaster/CreateAjaxEmployee.aspx";
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        void ActionControlMange(string ViewMode)
        {
            lbApprove.Visible = true;
            lbReject.Visible = true;
            lbGenerate.Visible = true;
            DivApprover.Visible = true;

            PApiResult Result = new BOnboardEmployee().GetOnboardEmployeePendingApproval(null, null, null, null, null, null, null);

            if (!JsonConvert.DeserializeObject<List<POnboardEmployee>>(JsonConvert.SerializeObject(Result.Data)).Any(item => item.ReportingTo.DealerEmployeeID == PSession.User.DealerEmployeeID && item.OnboardEmployeeID == Emp.OnboardEmployeeID && item.Status.StatusId == (short)OnboardEmployeeStatus.Requested))
            {
                lbApprove.Visible = false;
                lbReject.Visible = false;
                DivApprover.Visible = false;
            }
            if (ViewMode == "View")
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
        void FillDealer()
        {
            List<PDMS_Dealer> DealerList1 = new BDMS_Dealer().GetDealer(null, "", null, null);
            ListViewDealer.DataSource = DealerList1;
            ListViewDealer.DataBind();
            //ListViewMDealer.DataSource = DealerList1;
            //ListViewMDealer.DataBind();
            foreach (ListViewItem item in ListViewDealer.Items)
            {
                CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                Label lblDID = (Label)item.FindControl("lblDID");
                bool containsItem = DealerList.Any(x => x.DealerID == Convert.ToInt32(lblDID.Text) && x.OnboardEmployeeID == Emp.OnboardEmployeeID && x.IsActive == true);
                if (containsItem)
                {
                    chkDealer.Checked = true;
                }
            }
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
                    D.OnboardEmployeeID = Emp.OnboardEmployeeID;
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
                    D.OnboardEmployeeID = Emp.OnboardEmployeeID;
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
            bool containsItem = DealerList.Any(item => item.DealerID == Convert.ToInt32(lblDID.Text) && item.OnboardEmployeeID == Emp.OnboardEmployeeID);
            if (containsItem)
            {
                DealerList.Where(w => w.DealerID == Convert.ToInt32(lblDID.Text)).Select(w => { w.IsActive = chkDealer.Checked; return w; }).ToList();
            }
            else
            {
                POnboardEmployeeDealer_Insert D = new POnboardEmployeeDealer_Insert();
                D.OnboardEmployeeID = Emp.OnboardEmployeeID;
                D.DealerID = Convert.ToInt32(lblDID.Text);
                D.IsActive = chkDealer.Checked;
                DealerList.Add(D);
            }
        }
    }
}