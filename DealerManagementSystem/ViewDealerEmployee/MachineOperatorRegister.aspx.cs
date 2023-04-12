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

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class MachineOperatorRegister : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDealerEmployee_MachineOperatorRegister; } }
        public string AadhaarCardNo
        {
            get
            {
                return txtAadhaarCardNo.Text.Trim().Replace("-", "");
            }
        }
        public PMachineOperatorAttachedFile PhotoFile
        {
            get
            {
                if (Session["DMSMO_PhotoFile"] == null)
                {
                    Session["DMSMO_PhotoFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_PhotoFile"];
            }
            set
            {
                Session["DMSMO_PhotoFile"] = value;
            }
        }
        public PMachineOperatorAttachedFile AdhaarCardCopyFrontSideFile
        {
            get
            {
                if (Session["DMSMO_AdhaarCardCopyFrontSideFile"] == null)
                {
                    Session["DMSMO_AdhaarCardCopyFrontSideFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_AdhaarCardCopyFrontSideFile"];
            }
            set
            {
                Session["DMSMO_AdhaarCardCopyFrontSideFile"] = value;
            }
        }
        public PMachineOperatorAttachedFile AdhaarCardCopyBackSideFile
        {
            get
            {
                if (Session["DMSMO_AdhaarCardCopyBackSideFile"] == null)
                {
                    Session["DMSMO_AdhaarCardCopyBackSideFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_AdhaarCardCopyBackSideFile"];
            }
            set
            {
                Session["DMSMO_AdhaarCardCopyBackSideFile"] = value;
            }
        }
        public PMachineOperatorAttachedFile PANCardCopyFile
        {
            get
            {
                if (Session["DMSMO_PANCardCopyFile"] == null)
                {
                    Session["DMSMO_PANCardCopyFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_PANCardCopyFile"];
            }
            set
            {
                Session["DMSMO_PANCardCopyFile"] = value;
            }
        }
        public PMachineOperatorAttachedFile ChequeCopyFile
        {
            get
            {
                if (Session["DMSMO_ChequeCopyFile"] == null)
                {
                    Session["DMSMO_ChequeCopyFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_ChequeCopyFile"];
            }
            set
            {
                Session["DMSMO_ChequeCopyFile"] = value;
            }
        }
        public PMachineOperatorAttachedFile DLCopyFrontSideFile
        {
            get
            {
                if (Session["DMSMO_DLCopyFrontSideFile"] == null)
                {
                    Session["DMSMO_DLCopyFrontSideFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_DLCopyFrontSideFile"];
            }
            set
            {
                Session["DMSMO_DLCopyFrontSideFile"] = value;
            }
        }
        public PMachineOperatorAttachedFile DLCopyBackSideFile
        {
            get
            {
                if (Session["DMSMO_DLCopyBackSideFile"] == null)
                {
                    Session["DMSMO_DLCopyBackSideFile"] = new PMachineOperatorAttachedFile();
                }
                return (PMachineOperatorAttachedFile)Session["DMSMO_DLCopyBackSideFile"];
            }
            set
            {
                Session["DMSMO_DLCopyBackSideFile"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Create / Machine Operator Registration');</script>");
            if (!IsPostBack)
            {
                PhotoFile = new PMachineOperatorAttachedFile();
                AdhaarCardCopyFrontSideFile = new PMachineOperatorAttachedFile();
                AdhaarCardCopyBackSideFile = new PMachineOperatorAttachedFile();
                PANCardCopyFile = new PMachineOperatorAttachedFile();
                ChequeCopyFile = new PMachineOperatorAttachedFile();
                DLCopyFrontSideFile = new PMachineOperatorAttachedFile();
                DLCopyBackSideFile = new PMachineOperatorAttachedFile();
                ViewState["MO_MachineOperatorDetailsID"] = null;
                ViewState["MO_PhotoAttachedFileID"] = null;
                ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"] = null;
                ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"] = null;
                ViewState["MO_PANCardCopyAttachedFileID"] = null;
                ViewState["MO_ChequeCopyAttachedFileID"] = null;
                ViewState["MO_DLCopyFrontSideAttachedFileID"] = null;
                ViewState["MO_DLCopyBackSideAttachedFileID"] = null;

                new BDMS_Address().GetStateDDL(ddlState, null, null, null, null);
                new BDMS_Dealer().GetEqucationalQualificationDDL(ddlEqucationalQualification, null, null);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, null, null, null);
                new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, null);
                new BDMS_Dealer().GetBloodGroupDDL(ddlBloodGroup, null, null);
                FillProductType();
                if (!string.IsNullOrEmpty(Request.QueryString["MO_MachineOperatorDetailsID"]))
                {
                    FillMachineOperatorDetails(Convert.ToInt32(Request.QueryString["MO_MachineOperatorDetailsID"]));
                    if (ViewState["MO_PhotoAttachedFileID"] != null)
                    {
                        PMachineOperatorAttachedFile PHFile = new BMachineOperator().GetMachineOperatorAttachedFile((long)ViewState["MO_PhotoAttachedFileID"]);
                        PAttachedFile Files = new BMachineOperator().GetAttachedFileCustomerForDownload(PHFile.AttachedFileID + Path.GetExtension(PHFile.FileName));
                        PHFile.AttachedFile = Files.AttachedFile;
                        string Url = "MachineOpPhotos/" + ((long)ViewState["MO_MachineOperatorDetailsID"]).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                        if (File.Exists(MapPath(Url)))
                        {
                            File.Delete(MapPath(Url));
                        }
                        FileSave(PHFile, ((long)ViewState["MO_MachineOperatorDetailsID"]).ToString());
                        ibtnPhoto.ImageUrl = Url;
                    }
                    ibtnPhoto.Visible = true;
                    lbPhotoFileRemove.Visible = true;
                    fuPhoto.Visible = false;

                    lbAdhaarCardCopyFrontSideFileDownload.Visible = true;
                    lbAdhaarCardCopyFrontSideFileRemove.Visible = true;
                    fuAdhaarCardCopyFrontSide.Visible = false;

                    lbAdhaarCardCopyBackSideFileDownload.Visible = true;
                    lbAdhaarCardCopyBackSideFileRemove.Visible = true;
                    fuAdhaarCardCopyBackSide.Visible = false;

                    lbPANCardCopyFileDownload.Visible = true;
                    lbPANCardCopyFileRemove.Visible = true;
                    fuPANCardCopy.Visible = false;

                    lbChequeCopyFileDownload.Visible = true;
                    lbChequeCopyFileRemove.Visible = true;
                    fuChequeCopy.Visible = false;

                    lbDLCopyFrontSideFileDownload.Visible = true;
                    lbDLCopyFrontSideFileRemove.Visible = true;
                    fuDLCopyFrontSide.Visible = false;

                    lbDLCopyBackSideFileDownload.Visible = true;
                    lbDLCopyBackSideFileRemove.Visible = true;
                    fuDLCopyBackSide.Visible = false;

                    txtAadhaarCardNo.Enabled = false;
                }
                if (Session["MachineOperatorApproval"] != null)
                {
                    ViewState["MachineOperatorApproval"] = Session["MachineOperatorApproval"];
                    Session["MachineOperatorApproval"] = null;
                    FillMachineOperatorDetails(Convert.ToInt32(ViewState["MachineOperatorApproval"]));
                    btnSave.Text = "Approve";
                    btnBack.Visible = true;
                    txtAadhaarCardNo.Enabled = false;
                    ViewPhoto();
                    lbPhotoFileRemove.Visible = false;
                    lbAdhaarCardCopyFrontSideFileRemove.Visible = false;
                    lbAdhaarCardCopyBackSideFileRemove.Visible = false;
                    lbPANCardCopyFileRemove.Visible = false;
                    lbChequeCopyFileRemove.Visible = false;
                    lbDLCopyFrontSideFileRemove.Visible = false;
                    lbDLCopyBackSideFileRemove.Visible = false;

                    lblPhotoFileName.Visible = false;
                    fuPhoto.Visible = false;
                    fuAdhaarCardCopyFrontSide.Visible = false;
                    fuAdhaarCardCopyBackSide.Visible = false;
                    fuPANCardCopy.Visible = false;
                    fuChequeCopy.Visible = false;
                    fuDLCopyFrontSide.Visible = false;
                    fuDLCopyBackSide.Visible = false;

                    ibtnPhoto.Visible = true;
                    lbAdhaarCardCopyFrontSideFileDownload.Visible = true;
                    lbAdhaarCardCopyBackSideFileDownload.Visible = true;
                    lbPANCardCopyFileDownload.Visible = true;
                    lbChequeCopyFileDownload.Visible = true;
                    lbDLCopyFrontSideFileDownload.Visible = true;
                    lbDLCopyBackSideFileDownload.Visible = true;
                }
            }
        }
        void FillProductType()
        {
            List<PProductType> ProductType = new BDMS_Master().GetProductType(null, null);
            ListViewProductType.DataSource = ProductType;
            ListViewProductType.DataBind();
        }
        void FileSave(PMachineOperatorAttachedFile PHFile, string imgID)
        {
            try
            {
                string dir = Server.MapPath("MachineOpPhotos");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string filePath = dir + "/" + imgID + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                File.WriteAllBytes(filePath, PHFile.AttachedFile);
            }
            catch (Exception ex)
            {

            }
        }
        void FillMachineOperatorDetails(long MachineOperatorDetailsID)
        {
            PMachineOperator Emp = new BMachineOperator().GetMachineOperatorDetailsByID(MachineOperatorDetailsID);
            Emp.ProductTypes = new BMachineOperator().GetMachineOperatorProductTypesByID(MachineOperatorDetailsID);
            ViewState["MO_MachineOperatorDetailsID"] = Emp.MachineOperatorDetailsID;
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
            txtPANNo.Text = Emp.PANNo;
            txtBankName.Text = Emp.BankName;
            txtAccountNo.Text = Emp.AccountNo;
            txtIFSCCode.Text = Emp.IFSCCode;
            ddlDepartment.SelectedValue = Emp.Department.DealerDepartmentID.ToString();
            ddlDesignation.SelectedValue = Emp.Designation.DealerDesignationID.ToString();
            ddlReportingTo.SelectedValue = Emp.ReportingTo.DealerEmployeeID.ToString();
            txtEmergencyContactNumber.Text = Emp.EmergencyContactNumber;
            ddlBloodGroup.SelectedValue = Emp.BloodGroup.BloodGroupID.ToString();
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
            if (Emp.Photo != null)
            {
                lblPhotoFileName.Text = Emp.Photo.FileName;
                ViewState["MO_PhotoAttachedFileID"] = Emp.Photo.AttachedFileID;
                PhotoFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.Photo.AttachedFileID);
            }
            if (Emp.AdhaarCardCopyFrontSide != null)
            {
                lblAdhaarCardCopyFrontSideFileName.Text = Emp.AdhaarCardCopyFrontSide.FileName;
                ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"] = Emp.AdhaarCardCopyFrontSide.AttachedFileID;
                AdhaarCardCopyFrontSideFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.AdhaarCardCopyFrontSide.AttachedFileID);
            }
            if (Emp.AdhaarCardCopyBackSide != null)
            {
                lblAdhaarCardCopyBackSideFileName.Text = Emp.AdhaarCardCopyBackSide.FileName;
                ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"] = Emp.AdhaarCardCopyBackSide.AttachedFileID;
                AdhaarCardCopyBackSideFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.AdhaarCardCopyBackSide.AttachedFileID);
            }
            if (Emp.PANCardCopy != null)
            {
                lblPANCardCopyFileName.Text = Emp.PANCardCopy.FileName;
                ViewState["MO_PANCardCopyAttachedFileID"] = Emp.PANCardCopy.AttachedFileID;
                PANCardCopyFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.PANCardCopy.AttachedFileID);
            }
            if (Emp.ChequeCopy != null)
            {
                lblChequeCopyFileName.Text = Emp.ChequeCopy.FileName;
                ViewState["MO_ChequeCopyAttachedFileID"] = Emp.ChequeCopy.AttachedFileID;
                ChequeCopyFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.ChequeCopy.AttachedFileID);
            }
            foreach (ListViewItem item in ListViewProductType.Items)
            {
                CheckBox chkProductType = (CheckBox)item.FindControl("chkProductType");
                Label lblProductType = (Label)item.FindControl("lblProductType");
                Label lblProductTypeID = (Label)item.FindControl("lblProductTypeID");

                bool containsItem = Emp.ProductTypes.Any(e => e.MachineOperatorDetailsID == MachineOperatorDetailsID && e.ProductType.ProductTypeID == Convert.ToInt32(lblProductTypeID.Text) && e.IsActive == true);
                if (containsItem)
                {
                    chkProductType.Checked = true;
                }
            }
            if (Emp.DLFrontSide != null)
            {
                lblDLCopyFrontSideFileName.Text = Emp.DLFrontSide.FileName;
                ViewState["MO_DLCopyFrontSideAttachedFileID"] = Emp.DLFrontSide.AttachedFileID;
                DLCopyFrontSideFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.DLFrontSide.AttachedFileID);
            }
            if (Emp.DLBackSide != null)
            {
                lblDLCopyBackSideFileName.Text = Emp.DLBackSide.FileName;
                ViewState["MO_DLCopyBackSideAttachedFileID"] = Emp.DLBackSide.AttachedFileID;
                DLCopyBackSideFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.DLBackSide.AttachedFileID);
            }
            txtDLNumber.Text = Emp.DLNumber;
            txtDLIssueDate.Text = Emp.DLIssueDate.ToString();
            txtDLIssuingOffice.Text = Emp.DLIssueingOffice;
            txtDLExpiryDate.Text = Emp.DLExpiryDate.ToString();
            txtDLFor.Text = Emp.DLFor;
        }
        protected void ViewPhoto()
        {
            if (ViewState["MO_PhotoAttachedFileID"] != null)
            {
                PMachineOperatorAttachedFile PHFile = new BMachineOperator().GetMachineOperatorAttachedFile((long)ViewState["MO_PhotoAttachedFileID"]);
                PAttachedFile Files = new BMachineOperator().GetAttachedFileCustomerForDownload(PHFile.AttachedFileID + Path.GetExtension(PHFile.FileName));
                PHFile.AttachedFile = Files.AttachedFile;
                string Url = "MachineOpPhotos/" + ((long)ViewState["MO_MachineOperatorDetailsID"]).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                if (File.Exists(MapPath(Url)))
                {
                    File.Delete(MapPath(Url));
                }
                FileSave(PHFile, ((long)ViewState["MO_MachineOperatorDetailsID"]).ToString());
                ibtnPhoto.ImageUrl = Url;
            }
        }
        protected void lbPhotoFileRemove_Click(object sender, EventArgs e)
        {
            PhotoFile = new PMachineOperatorAttachedFile();
            lblPhotoFileName.Text = "";
            lbPhotoFileRemove.Visible = false;
            ibtnPhoto.Visible = false;
            fuPhoto.Visible = true;
        }
        protected void ibtnPhoto_Click(object sender, ImageClickEventArgs e)
        {
            FileDownload(ViewState["MO_PhotoAttachedFileID"] == null ? 0 : (long)ViewState["MO_PhotoAttachedFileID"], 1);
        }
        protected void lbAdhaarCardCopyFrontSideFileRemove_Click(object sender, EventArgs e)
        {
            AdhaarCardCopyFrontSideFile = new PMachineOperatorAttachedFile();
            lblAdhaarCardCopyFrontSideFileName.Text = "";
            lbAdhaarCardCopyFrontSideFileRemove.Visible = false;
            lbAdhaarCardCopyFrontSideFileDownload.Visible = false;
            fuAdhaarCardCopyFrontSide.Visible = true;
        }
        protected void lbAdhaarCardCopyFrontSideFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"] == null ? 0 : (long)ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"], 2);
        }
        protected void lbAdhaarCardCopyBackSideFileRemove_Click(object sender, EventArgs e)
        {
            AdhaarCardCopyBackSideFile = new PMachineOperatorAttachedFile();
            lblAdhaarCardCopyBackSideFileName.Text = "";
            lbAdhaarCardCopyBackSideFileRemove.Visible = false;
            lbAdhaarCardCopyBackSideFileDownload.Visible = false;
            fuAdhaarCardCopyBackSide.Visible = true;
        }
        protected void lbAdhaarCardCopyBackSideFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"] == null ? 0 : (long)ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"], 3);
        }
        protected void lbPANCardCopyFileRemove_Click(object sender, EventArgs e)
        {
            PANCardCopyFile = new PMachineOperatorAttachedFile();
            lblPANCardCopyFileName.Text = "";
            lbPANCardCopyFileRemove.Visible = false;
            lbPANCardCopyFileDownload.Visible = false;
            fuPANCardCopy.Visible = true;
        }
        protected void lbPANCardCopyFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["MO_PANCardCopyAttachedFileID"] == null ? 0 : (long)ViewState["MO_PANCardCopyAttachedFileID"], 4);
        }
        protected void lbChequeCopyFileRemove_Click(object sender, EventArgs e)
        {
            ChequeCopyFile = new PMachineOperatorAttachedFile();
            lblChequeCopyFileName.Text = "";
            lbChequeCopyFileRemove.Visible = false;
            lbChequeCopyFileDownload.Visible = false;
            fuChequeCopy.Visible = true;
        }
        protected void lbChequeCopyFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["MO_ChequeCopyAttachedFileID"] == null ? 0 : (long)ViewState["MO_ChequeCopyAttachedFileID"], 5);
        }
        protected void lbDLCopyFrontSideFileRemove_Click(object sender, EventArgs e)
        {
            DLCopyFrontSideFile = new PMachineOperatorAttachedFile();
            lblDLCopyFrontSideFileName.Text = "";
            lbDLCopyFrontSideFileRemove.Visible = false;
            lbDLCopyFrontSideFileDownload.Visible = false;
            fuDLCopyFrontSide.Visible = true;
        }
        protected void lbDLCopyFrontSideFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["MO_DLCopyFrontSideAttachedFileID"] == null ? 0 : (long)ViewState["MO_DLCopyFrontSideAttachedFileID"], 6);
        }
        protected void lbDLCopyBackSideFileRemove_Click(object sender, EventArgs e)
        {
            DLCopyBackSideFile = new PMachineOperatorAttachedFile();
            lblDLCopyBackSideFileName.Text = "";
            lbDLCopyBackSideFileRemove.Visible = false;
            lbDLCopyBackSideFileDownload.Visible = false;
            fuDLCopyBackSide.Visible = true;
        }
        protected void lbDLCopyBackSideFileDownload_Click(object sender, EventArgs e)
        {
            FileDownload(ViewState["MO_DLCopyBackSideAttachedFileID"] == null ? 0 : (long)ViewState["MO_DLCopyBackSideAttachedFileID"], 7);
        }
        void FileDownload(long AttachedFileID, int N)
        {
            try
            {
                PMachineOperatorAttachedFile UploadedFile = new PMachineOperatorAttachedFile();
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
                    else if (N == 6)
                    {
                        UploadedFile = DLCopyFrontSideFile;
                    }
                    else if (N == 7)
                    {
                        UploadedFile = DLCopyBackSideFile;
                    }
                }
                else
                {
                    UploadedFile = new BMachineOperator().GetMachineOperatorAttachedFile(AttachedFileID);
                }
                PAttachedFile File = new BMachineOperator().GetAttachedFileCustomerForDownload(AttachedFileID + Path.GetExtension(UploadedFile.FileName));
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(File.AttachedFile);
                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.AppendCookie(cookie);
                // end 
                Response.Flush();                
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != "0")
                new BDMS_Address().GetDistrict(ddlDistrict, null, null, null, Convert.ToInt32(ddlState.SelectedValue), null, null);
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
                new BDMS_Address().GetTehsil(ddlTehsil, null, null, Convert.ToInt32(ddlDistrict.SelectedValue), null);
        }
        protected void txtAadhaarCardNo_TextChanged(object sender, EventArgs e)
        {
            if (new BDMS_Dealer().GetDealerEmployeeManage(null, AadhaarCardNo, null, null, "", null, null, null, null).Count() != 0)
            {
                lblMessage.Text = "This Aadhaar Card ( " + AadhaarCardNo + " ) Already Available";
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                txtAadhaarCardNo.Text = "";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            PMachineOperator_Insert Emp = new PMachineOperator_Insert();
            Emp.MachineOperatorDetailsID = ViewState["MO_MachineOperatorDetailsID"] == null ? 0 : (long)ViewState["MO_MachineOperatorDetailsID"];
            Emp.Name = txtName.Text.Trim();
            Emp.FatherName = txtFatherName.Text.Trim();

            Emp.DOB = Convert.ToDateTime(txtDOB.Text.Trim());
            Emp.ContactNumber = txtContactNumber.Text.Trim();
            Emp.ContactNumber1 = txtContactNumber1.Text.Trim();
            if (ddlDepartment.SelectedValue != "0")
            {
                Emp.DealerDepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            if (ddlDesignation.SelectedValue != "0")
            {
                Emp.DealerDesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            }
            if (ddlReportingTo.SelectedValue != "0")
            {
                Emp.ReportingToID = Convert.ToInt32(ddlReportingTo.SelectedValue);
            }
            Emp.Email = txtEmail.Text.Trim();
            Emp.Address = txtAddress.Text.Trim();
            if (ddlState.SelectedValue != "0")
            {
                Emp.StateID = Convert.ToInt32(ddlState.SelectedValue);
                if (ddlDistrict.SelectedValue != "0")
                {
                    Emp.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
                    if (ddlTehsil.SelectedValue != "0")
                    {
                        Emp.TehsilID = Convert.ToInt32(ddlTehsil.SelectedValue);
                    }
                }
            }
            Emp.Village = txtVillage.Text.Trim();
            Emp.Location = txtLocation.Text.Trim();
            Emp.AadhaarCardNo = AadhaarCardNo;

            if (ddlEqucationalQualification.SelectedValue != "0")
            {
                Emp.EqucationalQualificationID = Convert.ToInt32(ddlEqucationalQualification.SelectedValue);
            }
            Emp.TotalExperience = string.IsNullOrEmpty(txtTotalExperience.Text.Trim()) ? (decimal?)null : Convert.ToDecimal(txtTotalExperience.Text.Trim());
            Emp.PANNo = txtPANNo.Text.Trim();

            Emp.BankName = txtBankName.Text.Trim();
            Emp.AccountNo = txtAccountNo.Text.Trim();
            Emp.IFSCCode = txtIFSCCode.Text.Trim();

            Emp.EmergencyContactNumber = txtEmergencyContactNumber.Text.Trim();
            Emp.DLNumber = txtDLNumber.Text.Trim();
            if (!string.IsNullOrEmpty(txtDLIssueDate.Text))
            {
                Emp.DLIssueDate = Convert.ToDateTime(txtDLIssueDate.Text.Trim());
            }
            Emp.DLIssueingOffice = txtDLIssuingOffice.Text.Trim();
            if (!string.IsNullOrEmpty(txtDLExpiryDate.Text))
            {
                Emp.DLExpiryDate = Convert.ToDateTime(txtDLExpiryDate.Text.Trim());
            }
            Emp.DLFor = txtDLFor.Text.Trim();

            Emp.ProductTypes = new List<PMachineOperatorProductTypes_Insert>();
            foreach (ListViewItem item in ListViewProductType.Items)
            {
                CheckBox chkProductType = (CheckBox)item.FindControl("chkProductType");
                Label lblProductType = (Label)item.FindControl("lblProductType");
                Label lblProductTypeID = (Label)item.FindControl("lblProductTypeID");
                PMachineOperatorProductTypes_Insert p = new PMachineOperatorProductTypes_Insert();
                p.ProductTypeID = Convert.ToInt32(lblProductTypeID.Text);
                p.IsActive = chkProductType.Checked;
                Emp.ProductTypes.Add(p);
            }
            if (ddlBloodGroup.SelectedValue != "0")
            {
                Emp.BloodGroupID = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            }
            Emp.UserID = PSession.User.UserID;

            Emp.Photo = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = PhotoFile.AttachedFileID,
                FileName = PhotoFile.FileName,
                FileType = PhotoFile.FileType,
                AttachedFile = PhotoFile.AttachedFile,
                FileSize = PhotoFile.FileSize,
                IsDeleted = PhotoFile.IsDeleted,
                UserID = PhotoFile.UserID
            };
            Emp.AdhaarCardCopyFrontSide = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = AdhaarCardCopyFrontSideFile.AttachedFileID,
                FileName = AdhaarCardCopyFrontSideFile.FileName,
                FileType = AdhaarCardCopyFrontSideFile.FileType,
                AttachedFile = AdhaarCardCopyFrontSideFile.AttachedFile,
                FileSize = AdhaarCardCopyFrontSideFile.FileSize,
                IsDeleted = AdhaarCardCopyFrontSideFile.IsDeleted,
                UserID = AdhaarCardCopyFrontSideFile.UserID
            };
            Emp.AdhaarCardCopyBackSide = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = AdhaarCardCopyBackSideFile.AttachedFileID,
                FileName = AdhaarCardCopyBackSideFile.FileName,
                FileType = AdhaarCardCopyBackSideFile.FileType,
                AttachedFile = AdhaarCardCopyBackSideFile.AttachedFile,
                FileSize = AdhaarCardCopyBackSideFile.FileSize,
                IsDeleted = AdhaarCardCopyBackSideFile.IsDeleted,
                UserID = AdhaarCardCopyBackSideFile.UserID
            };
            Emp.PANCardCopy = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = PANCardCopyFile.AttachedFileID,
                FileName = PANCardCopyFile.FileName,
                FileType = PANCardCopyFile.FileType,
                AttachedFile = PANCardCopyFile.AttachedFile,
                FileSize = PANCardCopyFile.FileSize,
                IsDeleted = PANCardCopyFile.IsDeleted,
                UserID = PANCardCopyFile.UserID
            };
            Emp.ChequeCopy = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = ChequeCopyFile.AttachedFileID,
                FileName = ChequeCopyFile.FileName,
                FileType = ChequeCopyFile.FileType,
                AttachedFile = ChequeCopyFile.AttachedFile,
                FileSize = ChequeCopyFile.FileSize,
                IsDeleted = ChequeCopyFile.IsDeleted,
                UserID = ChequeCopyFile.UserID
            };
            Emp.DLFrontSide = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = DLCopyFrontSideFile.AttachedFileID,
                FileName = DLCopyFrontSideFile.FileName,
                FileType = DLCopyFrontSideFile.FileType,
                AttachedFile = DLCopyFrontSideFile.AttachedFile,
                FileSize = DLCopyFrontSideFile.FileSize,
                IsDeleted = DLCopyFrontSideFile.IsDeleted,
                UserID = DLCopyFrontSideFile.UserID
            };
            Emp.DLBackSide = new PMachineOperatorAttachedFile_Insert()
            {
                AttachedFileID = DLCopyBackSideFile.AttachedFileID,
                FileName = DLCopyBackSideFile.FileName,
                FileType = DLCopyBackSideFile.FileType,
                AttachedFile = DLCopyBackSideFile.AttachedFile,
                FileSize = DLCopyBackSideFile.FileSize,
                IsDeleted = DLCopyBackSideFile.IsDeleted,
                UserID = DLCopyBackSideFile.UserID
            };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Operator", Emp));
            long MachineOperatorDetailsID = Convert.ToInt64(Results.Data);
            //long MachineOperatorDetailsID = new BMachineOperator().InsertOrUpdateMachineOperatorDetails(Emp);
            if (btnSave.Text == "Approve")
            {
                //if (new BMachineOperator().ApproveMachineOperatorDetails(MachineOperatorDetailsID, PSession.User.UserID))
                PApiResult ResultApprove = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Operator/RequestApproval", MachineOperatorDetailsID));
                if (ResultApprove.Status == PApplication.Failure)
                {
                    lblMessage.Text = ResultApprove.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (Convert.ToBoolean(ResultApprove.Data))
                {
                    lblMessage.Text = "Machine Operator approved successfully";
                    lblMessage.ForeColor = Color.Green;
                    btnSave.Visible = false;
                }
                else
                {
                    lblMessage.Text = "Machine Operator is not approved successfully";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else
            {
                if (MachineOperatorDetailsID != 0)
                {
                    lblMessage.Text = "Machine Operator is updated successfully";
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
        Boolean Validation()
        {
            lblMessage.Text = string.Empty;
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

            txtDLNumber.BorderColor = Color.Silver;
            txtDLIssueDate.BorderColor = Color.Silver;
            txtDLIssuingOffice.BorderColor = Color.Silver;
            txtDLExpiryDate.BorderColor = Color.Silver;
            txtDLFor.BorderColor = Color.Silver;

            ddlEqucationalQualification.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;
            ddlDistrict.BorderColor = Color.Silver;
            ddlState.BorderColor = Color.Silver;

            fuPhoto.BorderColor = Color.Silver;
            fuAdhaarCardCopyFrontSide.BorderColor = Color.Silver;
            fuAdhaarCardCopyBackSide.BorderColor = Color.Silver;
            fuPANCardCopy.BorderColor = Color.Silver;
            fuChequeCopy.BorderColor = Color.Silver;
            fuDLCopyFrontSide.BorderColor = Color.Silver;
            fuDLCopyBackSide.BorderColor = Color.Silver;

            ddlDepartment.BorderColor = Color.Silver;
            ddlDesignation.BorderColor = Color.Silver;
            ddlReportingTo.BorderColor = Color.Silver;

            Boolean Ret = true;
            string Message = "";
            if (AadhaarCardNo.Count() != 12)
            {
                Message = "Please check the Aadhaar Card No";
                Ret = false;
                txtAadhaarCardNo.BorderColor = Color.Red;
            }
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Name";
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

            if (ddlDepartment.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the department";
                Ret = false;
                ddlDepartment.BorderColor = Color.Red;
            }
            if (ddlDesignation.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the designation";
                Ret = false;
                ddlDesignation.BorderColor = Color.Red;
            }
            if (ddlReportingTo.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Reporting To";
                Ret = false;
                ddlReportingTo.BorderColor = Color.Red;
            }

            string ProductTypes = "";
            int productCount = 0;
            foreach (ListViewItem item in ListViewProductType.Items)
            {
                CheckBox chkProductType = (CheckBox)item.FindControl("chkProductType");
                Label lblProductType = (Label)item.FindControl("lblProductType");
                Label lblProductTypeID = (Label)item.FindControl("lblProductTypeID");
                if (chkProductType.Checked == true)
                {
                    productCount += 1;
                    if (lblProductType.Text == "SLCM" || lblProductType.Text == "Transit Mixer" || lblProductType.Text == "Boom Pump" || lblProductType.Text == "Placing Equipment")
                    {
                        if (string.IsNullOrEmpty(txtDLNumber.Text))
                        {
                            Message = Message + "<br/> Please Enter DL Number";
                            Ret = false;
                            txtDLNumber.BorderColor = Color.Red;
                        }
                        if (string.IsNullOrEmpty(txtDLIssueDate.Text))
                        {
                            Message = Message + "<br/> Please Enter DL Issue Date";
                            Ret = false;
                            txtDLIssueDate.BorderColor = Color.Red;
                        }
                        if (string.IsNullOrEmpty(txtDLIssuingOffice.Text))
                        {
                            Message = Message + "<br/> Please Enter DL Issueing Office";
                            Ret = false;
                            txtDLIssuingOffice.BorderColor = Color.Red;
                        }
                        if (string.IsNullOrEmpty(txtDLExpiryDate.Text))
                        {
                            Message = Message + "<br/> Please Enter DL Expiry Date";
                            Ret = false;
                            txtDLExpiryDate.BorderColor = Color.Red;
                        }
                        if (string.IsNullOrEmpty(txtDLFor.Text))
                        {
                            Message = Message + "<br/> Please Enter DL For";
                            Ret = false;
                            txtDLFor.BorderColor = Color.Red;
                        }
                        if (DLCopyFrontSideFile.FileName == null)
                        {
                            Message = Message + "<br/> Please attach the DL Copy Front Side";
                            Ret = false;
                            fuDLCopyFrontSide.BorderColor = Color.Red;
                        }
                        if (DLCopyBackSideFile.FileName == null)
                        {
                            Message = Message + "<br/> Please attach the DL Copy Back Side";
                            Ret = false;
                            fuDLCopyBackSide.BorderColor = Color.Red;
                        }
                    }
                }
            }
            if (productCount == 0)
            {
                Message = Message + "<br/>Please select the ProductType";
                Ret = false;
                ddlReportingTo.BorderColor = Color.Red;
            }

            //if (string.IsNullOrEmpty(txtPANNo.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the PAN No";
            //    Ret = false;
            //    txtPANNo.BorderColor = Color.Red;
            //}
            //if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Bank Name";
            //    Ret = false;
            //    txtBankName.BorderColor = Color.Red;
            //}
            //if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the Account No";
            //    Ret = false;
            //    txtAccountNo.BorderColor = Color.Red;
            //}
            //if (string.IsNullOrEmpty(txtIFSCCode.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please enter the IFSC Code";
            //    Ret = false;
            //    txtIFSCCode.BorderColor = Color.Red;
            //}

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
            if (PhotoFile.FileName == null)
            {
                Message = Message + "<br/> Please attach the Photo";
                Ret = false;
                fuPhoto.BorderColor = Color.Red;
            }
            if (AdhaarCardCopyFrontSideFile.FileName == null)
            {
                Message = Message + "<br/> Please attach the Adhaar Card Copy Front Side";
                Ret = false;
                fuAdhaarCardCopyFrontSide.BorderColor = Color.Red;
            }
            if (AdhaarCardCopyBackSideFile.FileName == null)
            {
                Message = Message + "<br/> Please attach the Adhaar Card Copy Back Side";
                Ret = false;
                fuAdhaarCardCopyBackSide.BorderColor = Color.Red;
            }
            //if (PANCardCopyFile.AttachedFile == null)
            //{
            //    Message = Message + "<br/> Please attach the PAN Card Copy";
            //    Ret = false;
            //    fuPANCardCopy.BorderColor = Color.Red;
            //}
            //if (ChequeCopyFile.AttachedFile == null)
            //{
            //    Message = Message + "<br/> Please attach the Cheque Copy";
            //    Ret = false;
            //    fuChequeCopy.BorderColor = Color.Red;
            //}


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
                    PhotoFile.AttachedFileID = 0;
                    ViewState["MO_PhotoAttachedFileID"] = null;

                    string Url = "MachineOpPhotos/" + PSession.User.UserID.ToString() + "." + PhotoFile.FileName.Split('.')[PhotoFile.FileName.Split('.').Count() - 1];
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
                    AdhaarCardCopyFrontSideFile.AttachedFileID = 0;
                    ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"] = null;
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
                    AdhaarCardCopyBackSideFile.AttachedFileID = 0;
                    ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"] = null;
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
                    PANCardCopyFile.AttachedFileID = 0;
                    ViewState["MO_PANCardCopyAttachedFileID"] = null;
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
                    ChequeCopyFile.AttachedFileID = 0;
                    ViewState["MO_ChequeCopyAttachedFileID"] = null;
                }
            }
            if (fuDLCopyFrontSide.PostedFile != null)
            {
                if (fuDLCopyFrontSide.PostedFile.FileName.Length != 0)
                {
                    DLCopyFrontSideFile = CreateUploadedFile(fuDLCopyFrontSide.PostedFile);
                    lblDLCopyFrontSideFileName.Text = DLCopyFrontSideFile.FileName;
                    lbDLCopyFrontSideFileRemove.Visible = true;
                    lbDLCopyFrontSideFileDownload.Visible = true;
                    fuDLCopyFrontSide.Visible = false;
                    DLCopyFrontSideFile.AttachedFileID = 0;
                    ViewState["MO_DLCopyFrontSideAttachedFileID"] = null;
                }
            }
            if (fuDLCopyBackSide.PostedFile != null)
            {
                if (fuDLCopyBackSide.PostedFile.FileName.Length != 0)
                {
                    DLCopyBackSideFile = CreateUploadedFile(fuDLCopyBackSide.PostedFile);
                    lblDLCopyBackSideFileName.Text = DLCopyBackSideFile.FileName;
                    lbDLCopyBackSideFileRemove.Visible = true;
                    lbDLCopyBackSideFileDownload.Visible = true;
                    fuDLCopyBackSide.Visible = false;
                    DLCopyBackSideFile.AttachedFileID = 0;
                    ViewState["MO_DLCopyBackSideAttachedFileID"] = null;
                }
            }
        }
        private PMachineOperatorAttachedFile CreateUploadedFile(HttpPostedFile file)
        {
            PMachineOperatorAttachedFile AttachedFile = new PMachineOperatorAttachedFile();
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
            AttachedFile.UserID = PSession.User.UserID;
            return AttachedFile;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string url = "MachineOperatorApproval.aspx";
            Response.Redirect(url);
        }
    }
}