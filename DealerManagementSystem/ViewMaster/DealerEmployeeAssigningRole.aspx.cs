using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerEmployeeAssigningRole : System.Web.UI.Page
    {
        public string AadhaarCardNo
        {
            get
            {
                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }
        public List<PDMS_DealerEmployee> EmployeeManageRole
        {
            get
            {
                if (Session["EmployeeManageRole"] == null)
                {
                    Session["EmployeeManageRole"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["EmployeeManageRole"];
            }
            set
            {
                Session["EmployeeManageRole"] = value;
            }
        }
        public List<PDMS_District> District
        {
            get
            {
                if (Session["PDMS_DistrictRA"] == null)
                {
                    Session["PDMS_DistrictRA"] = new List<PDMS_District>();
                }
                return (List<PDMS_District>)Session["PDMS_DistrictRA"];
            }
            set
            {
                Session["PDMS_DistrictRA"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_DealerEmployeeAssigningRole.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Assign Role');</script>");

            lblMessage.Visible = false;
            if (!IsPostBack)
            {

                ViewState["PhotoAttachedFileID"] = null;
                ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = null;
                ViewState["AdhaarCardCopyBackSideAttachedFileID"] = null;
                ViewState["PANCardCopyAttachedFileID"] = null;
                ViewState["ChequeCopyAttachedFileID"] = null;

                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealer.Enabled = false;
                }
                else
                {
                    ddlDealer.Enabled = true;
                    fillDealer();
                }
                new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, Convert.ToInt32(ddlDealer.SelectedValue));
                //ddlDistrict.DataSource = new BDMS_Address().GetDistrict(null, null, null, null, null, null);
                //ddlDistrict.DataTextField = "District";
                //ddlDistrict.DataValueField = "DistrictID";
                //ddlDistrict.DataBind();
                new DDLBind(ddlDistrict, new BDMS_Address().GetDistrictBySalesEngineerUserID(Convert.ToInt32(ddlDealer.SelectedValue)), "District", "DistrictID");
                caDateOfJoining.StartDate = DateTime.Now.AddDays(-7);
                caDateOfJoining.EndDate = DateTime.Now;
            }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerEmployeeManageRole();
            new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, Convert.ToInt32(ddlDealer.SelectedValue));
            FillGetDealerOffice();
        }
        private void FillDealerEmployeeManageRole()
        {
            EmployeeManageRole = new BDMS_Dealer().GetDealerEmployeeManageRole(null, AadhaarCardNo, txtName.Text.Trim());
            gvDealerEmployee.DataSource = EmployeeManageRole;
            gvDealerEmployee.DataBind();
            lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageRole.Count;
        }

        private PDMS_DealerEmployeeAttachedFile CreateUploadedFile(HttpPostedFile file)
        {

            PDMS_DealerEmployeeAttachedFile AttachedFile = new PDMS_DealerEmployeeAttachedFile();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);
            AttachedFile.FileName = name;

            AttachedFile.FileType = file.ContentType;

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.FileSize = size;
            AttachedFile.AttachedFileID = 0;
            AttachedFile.IsDeleted = false;
            AttachedFile.DealerEmployeeID = 0;
            return AttachedFile;

        }

        void FillDealerEmployee(int DealerEmployeeID)
        {
            PDMS_DealerEmployee Emp = new BDMS_Dealer().GetDealerEmployeeByDealerEmployeeID(DealerEmployeeID);
            lblName.Text = Emp.Name;
            lblFatherName.Text = Emp.FatherName;
            lblDOB.Text = Convert.ToString(Emp.DOB);
            lblContactNumber.Text = Emp.ContactNumber;

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

            lblEmail.Text = Emp.Email;
            lblAddress.Text = Emp.Address;
            lblLocation.Text = Emp.Location;
            lblAadhaarCardNo.Text = Emp.AadhaarCardNo;
            lblTotalExperience.Text = Convert.ToString(Emp.TotalExperience);
            lblPANNo.Text = Emp.PANNo;
            lblBankName.Text = Emp.BankName;
            lblAccountNo.Text = Emp.AccountNo;
            lblIFSCCode.Text = Emp.IFSCCode;
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
            if (Emp.EqucationalQualification != null)
            {
                lblEqucationalQualification.Text = Emp.EqucationalQualification.EqucationalQualification;
            }

            if (Emp.Photo != null)
            {
                lblPhotoFileName.Text = Emp.Photo.FileName;
                lbPhotoFileName.Visible = true;
                ViewState["PhotoAttachedFileID"] = Emp.Photo.AttachedFileID;
            }
            if (Emp.AdhaarCardCopyFrontSide != null)
            {
                lblAdhaarCardCopyFrontSideFileName.Text = Emp.AdhaarCardCopyFrontSide.FileName;
                lbAdhaarCardCopyFrontSideFileName.Visible = true;
                ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = Emp.AdhaarCardCopyFrontSide.AttachedFileID;
            }
            if (Emp.AdhaarCardCopyBackSide != null)
            {
                lblAdhaarCardCopyBackSideFileName.Text = Emp.AdhaarCardCopyBackSide.FileName;
                lbAdhaarCardCopyBackSideFileName.Visible = true;
                ViewState["AdhaarCardCopyBackSideAttachedFileID"] = Emp.AdhaarCardCopyBackSide.AttachedFileID;
            }
            if (Emp.PANCardCopy != null)
            {
                lblPANCardCopyFileName.Text = Emp.PANCardCopy.FileName;
                lbPANCardCopyFileName.Visible = true;
                ViewState["PANCardCopyAttachedFileID"] = Emp.PANCardCopy.AttachedFileID;
            }
            if (Emp.ChequeCopy != null)
            {
                lblChequeCopyFileName.Text = Emp.ChequeCopy.FileName;
                lbChequeCopyFileName.Visible = true;
                ViewState["ChequeCopyAttachedFileID"] = Emp.ChequeCopy.AttachedFileID;
            }
        }

        protected void lbPhotoFileName_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["PhotoAttachedFileID"]);
        }

        protected void lbfuAdhaarCardCopyFrontSide_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["AdhaarCardCopyFrontSideAttachedFileID"]);
        }

        protected void lbAdhaarCardCopyBackSide_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["AdhaarCardCopyBackSideAttachedFileID"]);
        }

        protected void lbPANCardCopy_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["PANCardCopyAttachedFileID"]);
        }

        protected void lbChequeCopy_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["ChequeCopyAttachedFileID"]);
        }

        void FileDownload(long AttachedFileID)
        {
            try
            {

                PDMS_DealerEmployeeAttachedFile UploadedFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAssignRole_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            lblMessage.Visible = true;
            PDMS_DealerEmployeeRole Role = new PDMS_DealerEmployeeRole();
            Role.DealerEmployeeID = (int)ViewState["DealerEmployeeID"];
            Role.Dealer = new PDMS_Dealer();
            //Role.LoginUserName = Convert.ToString(txtLoginUserName.Text.Trim());
            Role.Dealer.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            Role.Dealer.DealerOffice = new PDMS_DealerOffice();
            Role.Dealer.DealerOffice.OfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            Role.DateOfJoining = Convert.ToDateTime(txtDateOfJoining.Text.Trim());
            Role.SAPEmpCode = txtSAPEmpCode.Text.Trim();
            Role.DealerDepartment = new PDMS_DealerDepartment();
            Role.DealerDepartment.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            Role.DealerDesignation = new PDMS_DealerDesignation();
            Role.DealerDesignation.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlReportingTo.SelectedValue != "0")
            {
                Role.ReportingTo = new PDMS_DealerEmployee();
                Role.ReportingTo.DealerEmployeeID = Convert.ToInt32(ddlReportingTo.SelectedValue);
            }
            
            if (new BDMS_Dealer().InsertDealerEmployeeRole(Role, PSession.User.UserID, HiddenDistrictID.Value))
            {
                lblMessage.Text = "New Role Assigned Employee";
                lblMessage.ForeColor = Color.Green;
                btnAssignRole.Visible = false;
                FillDealerEmployeeRole();
                ClearField();
            }
            else
            {
                lblMessage.Text = "New Role is not Assigned Employee";
                lblMessage.ForeColor = Color.Red;
            }
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();
        }

        protected void lbEditRole_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            ViewState["DealerEmployeeID"] = Convert.ToInt32(gvDealerEmployee.DataKeys[index].Value.ToString());
            FillDealerEmployee(Convert.ToInt32(gvDealerEmployee.DataKeys[index].Value.ToString()));
            FillDealerEmployeeRole();
            pnlManage.Visible = false;
            pnlAssingn.Visible = true;
            ClearField();
            new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, Convert.ToInt32(ddlDealer.SelectedValue));
            FillGetDealerOffice();
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.DataSource = EmployeeManageRole;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;

                gvDealerEmployee.DataBind();
                lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageRole.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.DataSource = EmployeeManageRole;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                gvDealerEmployee.DataBind();
                lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageRole.Count;
            }
        }
        private void FillGetDealerOffice()
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillDealerEmployeeRole()
        {
            gvRole.DataSource = new BDMS_Dealer().GetDealerEmployeeRole(null, (int)ViewState["DealerEmployeeID"], null, null);
            gvRole.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlManage.Visible = true;
            pnlAssingn.Visible = false;
            FillDealerEmployeeManageRole();
            ClearField();
        }

        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            txtDateOfJoining.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver;
            ddlDepartment.BorderColor = Color.Silver;
            ddlDesignation.BorderColor = Color.Silver;
            Boolean Ret = true;
            string Message = "";
            //if (string.IsNullOrEmpty(txtLoginUserName.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Login User Name";
            //    Ret = false;
            //    txtLoginUserName.BorderColor = Color.Red;
            //}
            DateTime DateOfJoining = DateTime.Now;
            if (ddlDealerOffice.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Dealer Office";
                Ret = false;
                ddlDealerOffice.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtDateOfJoining.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Date of Joining";
                Ret = false;
                txtDateOfJoining.BorderColor = Color.Red;
            }
            else
            {
                  DateOfJoining = Convert.ToDateTime(txtDateOfJoining.Text.Trim());

                if (caDateOfJoining.StartDate <= DateOfJoining && DateOfJoining <= caDateOfJoining.EndDate)
                {
                }
                else
                {
                    Message = Message + "<br/>Please Check the Date Of Joining";
                    Ret = false;
                    txtDateOfJoining.BorderColor = Color.Red;
                }
            }
            if (string.IsNullOrEmpty(txtSAPEmpCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the SAP Emp Code";
                Ret = false;
                txtSAPEmpCode.BorderColor = Color.Red;
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
            //Commented By Vignesh and Suggestion by John
            //if (ddlReportingTo.SelectedValue == "0")
            //{
            //    Message = Message + "<br/>Please select the Reporting To";
            //    Ret = false;
            //    ddlReportingTo.BorderColor = Color.Red;
            //}
            //Commented By Vignesh and Suggestion by John
            //if (District.Count == 0)
            //{
            //    Message = Message + "<br/>Please select the District";
            //    Ret = false;
            //    ddlDistrict.BorderColor = Color.Red;
            //}


            List<PDMS_DealerEmployee> Employee = new BDMS_Dealer().GetDealerEmployeeManage(null, null, null, null, "", txtSAPEmpCode.Text.Trim(), null);
            if (Employee.Count() != 0)
            {
                Message = Message + "<br/>This SAP Emp Code already used. Please enter new SAP Emp Code";
                Ret = false;
                txtSAPEmpCode.BorderColor = Color.Red;
            }

            



            List<PDMS_DealerEmployeeRole> Role = new BDMS_Dealer().GetDealerEmployeeRole(null, (int)ViewState["DealerEmployeeID"], null, null);
            if (Role.Count() != 0)
            {
                foreach (PDMS_DealerEmployeeRole R in Role)
                {
                    if (R.DateOfLeaving > DateOfJoining)
                    {
                        Message = Message + "<br/>Please Check the Date Of Joining";
                        Ret = false;
                        break;
                    }
                }
            }


            lblMessage.Text = Message;
            return Ret;
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }

        void ClearField()
        {
            ddlDealerOffice.Items.Clear();
            txtDateOfJoining.Text = "";
            ddlDepartment.SelectedValue = "0";
            Session["PDMS_District"] = null;
            District = null;
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }

        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.DataSource = EmployeeManageRole;
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            gvDealerEmployee.DataBind();
            lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageRole.Count;
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenDistrictID.Value+=(HiddenDistrictID.Value=="")?ddlDistrict.SelectedValue:","+ddlDistrict.SelectedValue;
            PDMS_District pDMS_District = new PDMS_District();
            pDMS_District.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
            pDMS_District.District = ddlDistrict.SelectedItem.Text.Trim();
            District.Add(pDMS_District);
            GVAssignDistrict.DataSource = District;
            GVAssignDistrict.DataBind();
            Session["PDMS_DistrictRA"] = District;
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrictBySalesEngineerUserID(Convert.ToInt32(ddlDealer.SelectedValue)), "District", "DistrictID");
        }

        protected void LnkDistrict_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblDistrictID = (Label)GVAssignDistrict.Rows[gvRow.RowIndex].FindControl("lblDistrictID");
            var itemToRemove = District.Single(r => r.DistrictID == Convert.ToInt32(lblDistrictID.Text));
            District.Remove(itemToRemove);
            GVAssignDistrict.DataSource = District;
            GVAssignDistrict.DataBind();
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrictBySalesEngineerUserID(Convert.ToInt32(ddlDealer.SelectedValue)), "District", "DistrictID");
            new DDLBind(ddlDealerOffice, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null), "OfficeName_OfficeCode", "OfficeID", true, "Select");
        }
    }
}