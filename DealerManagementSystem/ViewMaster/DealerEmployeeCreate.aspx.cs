using Business;
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

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerEmployeeCreate : System.Web.UI.Page
    {
        public string AadhaarCardNo
        {
            get
            {

                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }
        public PDMS_DealerEmployeeAttachedFile PhotoFile
        {
            get
            {
                if (Session["DMS_PhotoFile"] == null)
                {
                    Session["DMS_PhotoFile"] = new PDMS_DealerEmployeeAttachedFile();
                }
                return (PDMS_DealerEmployeeAttachedFile)Session["DMS_PhotoFile"];
            }
            set
            {
                Session["DMS_PhotoFile"] = value;
            }
        }
        public PDMS_DealerEmployeeAttachedFile AdhaarCardCopyFrontSideFile
        {
            get
            {
                if (Session["DMS_AdhaarCardCopyFrontSideFile"] == null)
                {
                    Session["DMS_AdhaarCardCopyFrontSideFile"] = new PDMS_DealerEmployeeAttachedFile();
                }
                return (PDMS_DealerEmployeeAttachedFile)Session["DMS_AdhaarCardCopyFrontSideFile"];
            }
            set
            {
                Session["DMS_AdhaarCardCopyFrontSideFile"] = value;
            }
        }
        public PDMS_DealerEmployeeAttachedFile AdhaarCardCopyBackSideFile
        {
            get
            {
                if (Session["DMS_AdhaarCardCopyBackSideFile"] == null)
                {
                    Session["DMS_AdhaarCardCopyBackSideFile"] = new PDMS_DealerEmployeeAttachedFile();
                }
                return (PDMS_DealerEmployeeAttachedFile)Session["DMS_AdhaarCardCopyBackSideFile"];
            }
            set
            {
                Session["DMS_AdhaarCardCopyBackSideFile"] = value;
            }
        }
        public PDMS_DealerEmployeeAttachedFile PANCardCopyFile
        {
            get
            {
                if (Session["DMS_PANCardCopyFile"] == null)
                {
                    Session["DMS_PANCardCopyFile"] = new PDMS_DealerEmployeeAttachedFile();
                }
                return (PDMS_DealerEmployeeAttachedFile)Session["DMS_PANCardCopyFile"];
            }
            set
            {
                Session["DMS_PANCardCopyFile"] = value;
            }
        }
        public PDMS_DealerEmployeeAttachedFile ChequeCopyFile
        {
            get
            {
                if (Session["DMS_ChequeCopyFile"] == null)
                {
                    Session["DMS_ChequeCopyFile"] = new PDMS_DealerEmployeeAttachedFile();
                }
                return (PDMS_DealerEmployeeAttachedFile)Session["DMS_ChequeCopyFile"];
            }
            set
            {
                Session["DMS_ChequeCopyFile"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_DealerEmployeeCreate.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PhotoFile = new PDMS_DealerEmployeeAttachedFile();
                AdhaarCardCopyFrontSideFile = new PDMS_DealerEmployeeAttachedFile();
                AdhaarCardCopyBackSideFile = new PDMS_DealerEmployeeAttachedFile();
                PANCardCopyFile = new PDMS_DealerEmployeeAttachedFile();
                ChequeCopyFile = new PDMS_DealerEmployeeAttachedFile();
                ViewState["DealerEmployeeID"] = null;

                ViewState["PhotoAttachedFileID"] = null;
                ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = null;
                ViewState["AdhaarCardCopyBackSideAttachedFileID"] = null;
                ViewState["PANCardCopyAttachedFileID"] = null;
                ViewState["ChequeCopyAttachedFileID"] = null;

                new BDMS_Address().GetState(ddlState, null, null);
                new BDMS_Dealer().GetEqucationalQualificationDDL(ddlEqucationalQualification, null, null);
                new BDMS_Dealer().GetBloodGroupDDL(ddlBloodGroup, null, null);



                if (!string.IsNullOrEmpty(Request.QueryString["DealerEmployeeID"]))
                {

                    FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));

                    if (ViewState["PhotoAttachedFileID"] != null)
                    {
                        PDMS_DealerEmployeeAttachedFile PHFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile((long)ViewState["PhotoAttachedFileID"]);
                        string Url = "DealerEmpPhotos/" + ((int)ViewState["DealerEmployeeID"]).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                        if (File.Exists(MapPath(Url)))
                        {
                            File.Delete(MapPath(Url));
                        }
                        FileSave(PHFile, ((int)ViewState["DealerEmployeeID"]).ToString());
                        ibtnPhoto.ImageUrl = Url;
                    }

                    ibtnPhoto.Visible = true;
                    lbAdhaarCardCopyFrontSideFileDownload.Visible = true;
                    lbAdhaarCardCopyBackSideFileDownload.Visible = true;
                    lbPANCardCopyFileDownload.Visible = true;
                    lbChequeCopyFileDownload.Visible = true;

                    lbPhotoFileRemove.Visible = true;
                    lbAdhaarCardCopyFrontSideFileRemove.Visible = true;
                    lbAdhaarCardCopyBackSideFileRemove.Visible = true;
                    lbPANCardCopyFileRemove.Visible = true;
                    lbChequeCopyFileRemove.Visible = true;


                    fuPhoto.Visible = false;
                    fuAdhaarCardCopyFrontSide.Visible = false;
                    fuAdhaarCardCopyBackSide.Visible = false;
                    fuPANCardCopy.Visible = false;
                    fuChequeCopy.Visible = false;
                    txtAadhaarCardNo.Enabled = false;

                }
                if (Session["DealerEmployeeApproval"] != null)
                {
                    ViewState["DealerEmployeeApproval"] = Session["DealerEmployeeApproval"];
                    Session["DealerEmployeeApproval"] = null;
                    FillDealerEmployee(Convert.ToInt32(ViewState["DealerEmployeeApproval"]));
                    btnSave.Text = "Approve";
                    btnBack.Visible = true;
                    txtAadhaarCardNo.Enabled = false;
                    ViewPhoto();
                    lbPhotoFileRemove.Visible = false;
                    lbAdhaarCardCopyFrontSideFileRemove.Visible = false;
                    lbAdhaarCardCopyBackSideFileRemove.Visible = false;
                    lbPANCardCopyFileRemove.Visible = false;
                    lbChequeCopyFileRemove.Visible = false;

                    lblPhotoFileName.Visible = false;

                    fuPhoto.Visible = false;
                    fuAdhaarCardCopyFrontSide.Visible = false;
                    fuAdhaarCardCopyBackSide.Visible = false;
                    fuPANCardCopy.Visible = false;
                    fuChequeCopy.Visible = false;

                    ibtnPhoto.Visible = true;
                    lbAdhaarCardCopyFrontSideFileDownload.Visible = true;
                    lbAdhaarCardCopyBackSideFileDownload.Visible = true;
                    lbPANCardCopyFileDownload.Visible = true;
                    lbChequeCopyFileDownload.Visible = true;
                }
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
                new BDMS_Address().GetDistrict(ddlDistrict, null, Convert.ToInt32(ddlState.SelectedValue), null);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
                new BDMS_Address().GetTehsil(ddlTehsil, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            PDMS_DealerEmployee Emp = new PDMS_DealerEmployee();
            Emp.DealerEmployeeID = ViewState["DealerEmployeeID"] == null ? 0 : (int)ViewState["DealerEmployeeID"];
            // Emp.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(ddlDealer.SelectedValue) };
            Emp.Name = txtName.Text.Trim();
            Emp.FatherName = txtFatherName.Text.Trim();
            Emp.Photo = PhotoFile;

            Emp.DOB = Convert.ToDateTime(txtDOB.Text.Trim());
            Emp.ContactNumber = txtContactNumber.Text.Trim();
            Emp.ContactNumber1 = txtContactNumber1.Text.Trim();
            //  Emp.DateOfJoining = string.IsNullOrEmpty(txtDateOfJoining.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateOfJoining.Text.Trim());
            //if (ddlDepartment.SelectedValue != "0")
            //{
            //    Emp.DealerDepartment = new PDMS_DealerDepartment();
            //    Emp.DealerDepartment.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            //}
            //if (ddlDesignation.SelectedValue != "0")
            //{
            //    Emp.DealerDesignation = new PDMS_DealerDesignation();
            //    Emp.DealerDesignation.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            //}
            //if (ddlReportingTo.SelectedValue != "0")
            //{
            //    Emp.ReportingTo = new PDMS_DealerEmployee();
            //    Emp.ReportingTo.DealerEmployeeID = Convert.ToInt32(ddlReportingTo.SelectedValue);
            //}
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
            Emp.AdhaarCardCopyFrontSide = AdhaarCardCopyFrontSideFile;
            Emp.AdhaarCardCopyBackSide = AdhaarCardCopyBackSideFile;

            if (ddlEqucationalQualification.SelectedValue != "0")
            {
                Emp.EqucationalQualification = new PDMS_EqucationalQualification();
                Emp.EqucationalQualification.EqucationalQualificationID = Convert.ToInt32(ddlEqucationalQualification.SelectedValue);
            }
            Emp.TotalExperience = string.IsNullOrEmpty(txtTotalExperience.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtTotalExperience.Text.Trim());

            Emp.PANNo = txtPANNo.Text.Trim();
            Emp.PANCardCopy = PANCardCopyFile;

            Emp.BankName = txtBankName.Text.Trim();
            Emp.AccountNo = txtAccountNo.Text.Trim();
            Emp.IFSCCode = txtIFSCCode.Text.Trim();
            Emp.ChequeCopy = ChequeCopyFile;

            Emp.EmergencyContactNumber = txtEmergencyContactNumber.Text.Trim();
            if (ddlBloodGroup.SelectedValue != "0")
            {
                Emp.BloodGroup = new PDMS_BloodGroup() { BloodGroupID = Convert.ToInt32(ddlBloodGroup.SelectedValue) };
            }
            int DealerEmployeeID = new BDMS_Dealer().InsertOrUpdateDealerEmployee(Emp, PSession.User.UserID);
            if (btnSave.Text == "Approve")
            {

                if (new BDMS_Dealer().ApproveDealerEmployee(DealerEmployeeID, PSession.User.UserID))
                {
                    lblMessage.Text = "Dealer Employee approved successfully";
                    lblMessage.ForeColor = Color.Green;
                    btnSave.Visible = false;
                }
                else
                {
                    lblMessage.Text = "Dealer Employee is not approved successfully";
                    lblMessage.ForeColor = Color.Red;
                }

            }
            else
            {

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
            ViewState["DealerEmployeeID"] = Emp.DealerEmployeeID;
            txtName.Text = Emp.Name;
            txtFatherName.Text = Emp.FatherName;
            txtDOB.Text = Convert.ToString(Emp.DOB);
            txtContactNumber.Text = Emp.ContactNumber;
            txtContactNumber1.Text = Emp.ContactNumber1;
            //txtDateOfJoining.Text = Convert.ToString(Emp.DateOfJoining);
            //if (Emp.DealerDepartment != null)
            //{
            //    ddlDepartment.SelectedValue = Convert.ToString(Emp.DealerDepartment.DealerDepartmentID);
            //}
            //if (Emp.DealerDesignation != null)
            //{
            //    ddlDesignation.SelectedValue = Convert.ToString(Emp.DealerDesignation.DealerDesignationID);
            //}
            //if (Emp.ReportingTo != null)
            //{
            //    ddlReportingTo.SelectedValue = Convert.ToString(Emp.ReportingTo.DealerEmployeeID);
            //}
            txtEmail.Text = Emp.Email;
            txtAddress.Text = Emp.Address;
            txtLocation.Text = Emp.Location;
            txtAadhaarCardNo.Text = Emp.AadhaarCardNo;
            txtTotalExperience.Text = Convert.ToString(Emp.TotalExperience);
            txtPANNo.Text = Emp.PANNo;
            txtBankName.Text = Emp.BankName;
            txtAccountNo.Text = Emp.AccountNo;
            txtIFSCCode.Text = Emp.IFSCCode;
            txtEmergencyContactNumber.Text = Emp.EmergencyContactNumber;
            if (Emp.BloodGroup != null)
            {
                ddlBloodGroup.SelectedValue = Convert.ToString(Emp.BloodGroup.BloodGroupID);
            }

            if (Emp.State != null)
            {
                ddlState.SelectedValue = Convert.ToString(Emp.State.StateID);
                new BDMS_Address().GetDistrict(ddlDistrict, null, Convert.ToInt32(ddlState.SelectedValue), null);
                if (Emp.District != null)
                {
                    ddlDistrict.SelectedValue = Convert.ToString(Emp.District.DistrictID);
                    new BDMS_Address().GetTehsil(ddlTehsil, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
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


            lblPhotoFileName.Text = Emp.Photo.FileName;
            ViewState["PhotoAttachedFileID"] = Emp.Photo.AttachedFileID;
            PhotoFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.Photo.AttachedFileID);

            lblAdhaarCardCopyFrontSideFileName.Text = Emp.AdhaarCardCopyFrontSide.FileName;
            ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = Emp.AdhaarCardCopyFrontSide.AttachedFileID;
            AdhaarCardCopyFrontSideFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.AdhaarCardCopyFrontSide.AttachedFileID);

            lblAdhaarCardCopyBackSideFileName.Text = Emp.AdhaarCardCopyBackSide.FileName;
            ViewState["AdhaarCardCopyBackSideAttachedFileID"] = Emp.AdhaarCardCopyBackSide.AttachedFileID;
            AdhaarCardCopyBackSideFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.AdhaarCardCopyBackSide.AttachedFileID);

            lblPANCardCopyFileName.Text = Emp.PANCardCopy.FileName;
            ViewState["PANCardCopyAttachedFileID"] = Emp.PANCardCopy.AttachedFileID;
            PANCardCopyFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.PANCardCopy.AttachedFileID);

            lblChequeCopyFileName.Text = Emp.ChequeCopy.FileName;
            ViewState["ChequeCopyAttachedFileID"] = Emp.ChequeCopy.AttachedFileID;
            ChequeCopyFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.ChequeCopy.AttachedFileID);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuPhoto.PostedFile != null)
            {
                if (fuPhoto.PostedFile.FileName.Length != 0)
                {
                    PhotoFile = CreateUploadedFile(fuPhoto.PostedFile);
                    lblPhotoFileName.Text = PhotoFile.FileName;
                    lbPhotoFileRemove.Visible = true;
                    ibtnPhoto.Visible = true;
                    fuPhoto.Visible = false;
                    // PhotoFile.AttachedFileID = ViewState["PhotoAttachedFileID"] == null ? 0 : (long)ViewState["PhotoAttachedFileID"];
                    PhotoFile.AttachedFileID = 0;
                    ViewState["PhotoAttachedFileID"] = null;

                    string Url = "DealerEmpPhotos/" + PSession.User.UserID.ToString() + "." + PhotoFile.FileName.Split('.')[PhotoFile.FileName.Split('.').Count() - 1];
                    if (File.Exists(MapPath(Url)))
                    {
                        File.Delete(MapPath(Url));
                    }
                    FileSave(PhotoFile, PSession.User.UserID.ToString());
                    ibtnPhoto.ImageUrl = Url;
                }
            }
            if (fuAdhaarCardCopyFrontSide.PostedFile != null)
            {
                if (fuAdhaarCardCopyFrontSide.PostedFile.FileName.Length != 0)
                {
                    AdhaarCardCopyFrontSideFile = CreateUploadedFile(fuAdhaarCardCopyFrontSide.PostedFile);
                    lblAdhaarCardCopyFrontSideFileName.Text = AdhaarCardCopyFrontSideFile.FileName;
                    lbAdhaarCardCopyFrontSideFileRemove.Visible = true;
                    lbAdhaarCardCopyFrontSideFileDownload.Visible = true;
                    fuAdhaarCardCopyFrontSide.Visible = false;
                    //  AdhaarCardCopyFrontSideFile.AttachedFileID = ViewState["AdhaarCardCopyFrontSideAttachedFileID"] == null ? 0 : (long)ViewState["AdhaarCardCopyFrontSideAttachedFileID"];
                    AdhaarCardCopyFrontSideFile.AttachedFileID = 0;
                    ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = null;
                }
            }
            if (fuAdhaarCardCopyBackSide.PostedFile != null)
            {
                if (fuAdhaarCardCopyBackSide.PostedFile.FileName.Length != 0)
                {
                    AdhaarCardCopyBackSideFile = CreateUploadedFile(fuAdhaarCardCopyBackSide.PostedFile);
                    lblAdhaarCardCopyBackSideFileName.Text = AdhaarCardCopyBackSideFile.FileName;
                    lbAdhaarCardCopyBackSideFileRemove.Visible = true;
                    lbAdhaarCardCopyBackSideFileDownload.Visible = true;
                    fuAdhaarCardCopyBackSide.Visible = false;
                    //  AdhaarCardCopyBackSideFile.AttachedFileID = ViewState["AdhaarCardCopyBackSideAttachedFileID"] == null ? 0 : (long)ViewState["AdhaarCardCopyBackSideAttachedFileID"];
                    AdhaarCardCopyBackSideFile.AttachedFileID = 0;
                    ViewState["AdhaarCardCopyBackSideAttachedFileID"] = null;
                }
            }
            if (fuPANCardCopy.PostedFile != null)
            {
                if (fuPANCardCopy.PostedFile.FileName.Length != 0)
                {
                    PANCardCopyFile = CreateUploadedFile(fuPANCardCopy.PostedFile);
                    lblPANCardCopyFileName.Text = PANCardCopyFile.FileName;
                    lbPANCardCopyFileRemove.Visible = true;
                    lbPANCardCopyFileDownload.Visible = true;
                    fuPANCardCopy.Visible = false;
                    //  PANCardCopyFile.AttachedFileID = ViewState["PANCardCopyAttachedFileID"] == null ? 0 : (long)ViewState["PANCardCopyAttachedFileID"];
                    PANCardCopyFile.AttachedFileID = 0;
                    ViewState["PANCardCopyAttachedFileID"] = null;
                }
            }
            if (fuChequeCopy.PostedFile != null)
            {
                if (fuChequeCopy.PostedFile.FileName.Length != 0)
                {
                    ChequeCopyFile = CreateUploadedFile(fuChequeCopy.PostedFile);
                    lblChequeCopyFileName.Text = ChequeCopyFile.FileName;
                    lbChequeCopyFileRemove.Visible = true;
                    lbChequeCopyFileDownload.Visible = true;
                    fuChequeCopy.Visible = false;
                    //  ChequeCopyFile.AttachedFileID = ViewState["ChequeCopyAttachedFileID"] == null ? 0 : (long)ViewState["ChequeCopyAttachedFileID"];
                    ChequeCopyFile.AttachedFileID = 0;
                    ViewState["ChequeCopyAttachedFileID"] = null;
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string url = "DMS_DealerEmployeeApproval.aspx";
            Response.Redirect(url);
        }

        void FileDownload(long AttachedFileID, int N)
        {
            try
            {
                PDMS_DealerEmployeeAttachedFile UploadedFile = new PDMS_DealerEmployeeAttachedFile();
                if (AttachedFileID == 0)
                {
                    if (N == 1)
                    {
                        UploadedFile = PhotoFile;
                    }
                    else if (N == 2)
                    {
                        UploadedFile = AdhaarCardCopyFrontSideFile;
                    }
                    else if (N == 3)
                    {
                        UploadedFile = AdhaarCardCopyBackSideFile;
                    }
                    else if (N == 4)
                    {
                        UploadedFile = PANCardCopyFile;
                    }
                    else if (N == 5)
                    {
                        UploadedFile = ChequeCopyFile;
                    }
                }
                else
                {
                    UploadedFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(AttachedFileID);
                }
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

        void FileSave(PDMS_DealerEmployeeAttachedFile PHFile, string imgID)
        {
            try
            {
                string dir = Server.MapPath("DealerEmpPhotos");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //  string filePath = "~/DealerEmpPhotos/" + Path.GetFileName(UploadedFile.FileName);
                string filePath = dir + "/" + imgID + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                File.WriteAllBytes(filePath, PHFile.AttachedFile);
            }
            catch (Exception ex)
            {

            }
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
            txtPANNo.BorderColor = Color.Silver;
            txtBankName.BorderColor = Color.Silver;
            txtAccountNo.BorderColor = Color.Silver;
            txtIFSCCode.BorderColor = Color.Silver;

            ddlEqucationalQualification.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;

            fuPhoto.BorderColor = Color.Silver;
            fuAdhaarCardCopyFrontSide.BorderColor = Color.Silver;
            fuAdhaarCardCopyBackSide.BorderColor = Color.Silver;
            fuPANCardCopy.BorderColor = Color.Silver;
            fuChequeCopy.BorderColor = Color.Silver;

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

            if (string.IsNullOrEmpty(txtPANNo.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the PAN No";
                Ret = false;
                txtPANNo.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Bank Name";
                Ret = false;
                txtBankName.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Account No";
                Ret = false;
                txtAccountNo.BorderColor = Color.Red;
            }

            if (string.IsNullOrEmpty(txtIFSCCode.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the IFSC Code";
                Ret = false;
                txtIFSCCode.BorderColor = Color.Red;
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
            if (PhotoFile.AttachedFile == null)
            {
                Message = Message + "<br/> Please attaché the Photo";
                Ret = false;
                fuPhoto.BorderColor = Color.Red;
            }
            if (AdhaarCardCopyFrontSideFile.AttachedFile == null)
            {
                Message = Message + "<br/> Please attaché the Adhaar Card Copy Front Side";
                Ret = false;
                fuAdhaarCardCopyFrontSide.BorderColor = Color.Red;
            }
            if (AdhaarCardCopyBackSideFile.AttachedFile == null)
            {
                Message = Message + "<br/> Please attaché the Adhaar Card Copy Back Side";
                Ret = false;
                fuAdhaarCardCopyBackSide.BorderColor = Color.Red;
            }
            if (PANCardCopyFile.AttachedFile == null)
            {
                Message = Message + "<br/> Please attaché the PAN Card Copy";
                Ret = false;
                fuPANCardCopy.BorderColor = Color.Red;
            }
            if (ChequeCopyFile.AttachedFile == null)
            {
                Message = Message + "<br/> Please attaché the Cheque Copy";
                Ret = false;
                fuChequeCopy.BorderColor = Color.Red;
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

        protected void lbPhotoFileRemove_Click(object sender, EventArgs e)
        {
            PhotoFile = new PDMS_DealerEmployeeAttachedFile();
            lblPhotoFileName.Text = "";
            lbPhotoFileRemove.Visible = false;
            ibtnPhoto.Visible = false;
            fuPhoto.Visible = true;
        }
        protected void ibtnPhoto_Click(object sender, ImageClickEventArgs e)
        {
            FileDownload(ViewState["PhotoAttachedFileID"] == null ? 0 : (long)ViewState["PhotoAttachedFileID"], 1);
        }

        protected void lbAdhaarCardCopyFrontSideFileRemove_Click(object sender, EventArgs e)
        {
            AdhaarCardCopyFrontSideFile = new PDMS_DealerEmployeeAttachedFile();
            lblAdhaarCardCopyFrontSideFileName.Text = "";
            lbAdhaarCardCopyFrontSideFileRemove.Visible = false;
            lbAdhaarCardCopyFrontSideFileDownload.Visible = false;
            fuAdhaarCardCopyFrontSide.Visible = true;
        }
        protected void lbAdhaarCardCopyFrontSideFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["AdhaarCardCopyFrontSideAttachedFileID"] == null ? 0 : (long)ViewState["AdhaarCardCopyFrontSideAttachedFileID"], 2);
        }

        protected void lbAdhaarCardCopyBackSideFileRemove_Click(object sender, EventArgs e)
        {
            AdhaarCardCopyBackSideFile = new PDMS_DealerEmployeeAttachedFile();
            lblAdhaarCardCopyBackSideFileName.Text = "";
            lbAdhaarCardCopyBackSideFileRemove.Visible = false;
            lbAdhaarCardCopyBackSideFileDownload.Visible = false;
            fuAdhaarCardCopyBackSide.Visible = true;
        }
        protected void lbAdhaarCardCopyBackSideFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["AdhaarCardCopyBackSideAttachedFileID"] == null ? 0 : (long)ViewState["AdhaarCardCopyBackSideAttachedFileID"], 3);
        }

        protected void lbPANCardCopyFileRemove_Click(object sender, EventArgs e)
        {
            PANCardCopyFile = new PDMS_DealerEmployeeAttachedFile();
            lblPANCardCopyFileName.Text = "";
            lbPANCardCopyFileRemove.Visible = false;
            lbPANCardCopyFileDownload.Visible = false;
            fuPANCardCopy.Visible = true;
        }
        protected void lbPANCardCopyFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["PANCardCopyAttachedFileID"] == null ? 0 : (long)ViewState["PANCardCopyAttachedFileID"], 4);
        }

        protected void lbChequeCopyFileRemove_Click(object sender, EventArgs e)
        {

            ChequeCopyFile = new PDMS_DealerEmployeeAttachedFile();
            lblChequeCopyFileName.Text = "";
            lbChequeCopyFileRemove.Visible = false;
            lbChequeCopyFileDownload.Visible = false;
            fuChequeCopy.Visible = true;


        }
        protected void lbChequeCopyFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["ChequeCopyAttachedFileID"] == null ? 0 : (long)ViewState["ChequeCopyAttachedFileID"], 5);
        }

        protected void ViewPhoto()
        {
            if (ViewState["PhotoAttachedFileID"] != null)
            {
                PDMS_DealerEmployeeAttachedFile PHFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile((long)ViewState["PhotoAttachedFileID"]);
                string Url = "DealerEmpPhotos/" + ((int)ViewState["DealerEmployeeID"]).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                if (File.Exists(MapPath(Url)))
                {
                    File.Delete(MapPath(Url));
                }
                FileSave(PHFile, ((int)ViewState["DealerEmployeeID"]).ToString());
                ibtnPhoto.ImageUrl = Url;
            }
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
    }
}