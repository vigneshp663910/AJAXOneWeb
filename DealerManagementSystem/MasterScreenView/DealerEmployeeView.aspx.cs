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

namespace DealerManagementSystem.MasterScreenView
{
    public partial class DealerEmployeeView : System.Web.UI.Page
    {
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
            Session["previousUrl"] = "DMS_DealerEmployeeView.aspx";
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

                ViewState["PhotoAttachedFileID"] = null;
                ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = null;
                ViewState["AdhaarCardCopyBackSideAttachedFileID"] = null;
                ViewState["PANCardCopyAttachedFileID"] = null;
                ViewState["ChequeCopyAttachedFileID"] = null;
                if (!string.IsNullOrEmpty(Request.QueryString["DealerEmployeeID"]))
                {
                    FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                    FillDealerEmployeeRole(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                }
            }
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
            lblContactNumber1.Text = Emp.ContactNumber1;
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

                ViewState["PhotoAttachedFileID"] = Emp.Photo.AttachedFileID;

                PDMS_DealerEmployeeAttachedFile PHFile = new BDMS_Dealer().GetDealerEmployeeAttachedFile(Emp.Photo.AttachedFileID);
                string Url = "DealerEmpPhotos/" + (Emp.DealerEmployeeID).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                if (File.Exists(MapPath(Url)))
                {
                    File.Delete(MapPath(Url));
                }
                FileSave(PHFile, (Emp.DealerEmployeeID).ToString());
                ibtnPhoto.ImageUrl = Url;
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
            ibtnPhoto.Visible = true;
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
        private void FillDealerEmployeeRole(int DealerEmployeeID)
        {
            List<PDMS_DealerEmployeeRole> Role = new BDMS_Dealer().GetDealerEmployeeRole(null, DealerEmployeeID, null, null);
            gvRole.DataSource = Role;
            gvRole.DataBind();
            ViewState["ActiveRoleID"] = null;
            foreach (PDMS_DealerEmployeeRole Ro in Role)
            {
                if (Ro.IsActive)
                {
                    ViewState["ActiveRoleID"] = Ro.DealerEmployeeRoleID;
                    new BDMS_Dealer().GetDealerEmployeeDDL(ddlReportingTo, Convert.ToInt32(Ro.Dealer.DealerID));
                    FillGetDealerOffice(Ro.Dealer.DealerID);
                    ddlDealerOffice.SelectedValue = Convert.ToString(Ro.DealerOffice.OfficeID);
                    if (Ro.ReportingTo != null)
                        ddlReportingTo.SelectedValue = Convert.ToString(Ro.ReportingTo.DealerEmployeeID);

                    txtSAPEmpCode.Text = Ro.SAPEmpCode;
                }
            }
            if (ViewState["ActiveRoleID"] == null)
            {
                pnlUpdateOffice.Visible = false;
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
        protected void ibtnPhoto_Click(object sender, ImageClickEventArgs e)
        {
            FileDownload((long)ViewState["PhotoAttachedFileID"]);
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string url = "DMS_DealerEmployeeManage.aspx";
            Response.Redirect(url);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validation())
            {
                return;
            }
            long DealerEmployeeRoleID = (long)ViewState["ActiveRoleID"];
            int OfficeCodeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            int? ReportingTo = ddlReportingTo.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlReportingTo.SelectedValue);


            if (new BDMS_Dealer().UpdateDealerEmployeeRole(DealerEmployeeRoleID, OfficeCodeID, ReportingTo, txtSAPEmpCode.Text.Trim(), PSession.User.UserID))
            {
                lblMessage.Text = "Role updated successfully ";
                lblMessage.ForeColor = Color.Green;
                FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                FillDealerEmployeeRole(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
            }
            else
            {
                lblMessage.Text = "Role is not updated successfully ";
                lblMessage.ForeColor = Color.Red;
            }

            lblMessage.Visible = true;
        }
        private void FillGetDealerOffice(int DealerID)
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;

            ddlDealerOffice.BorderColor = Color.Silver;
            Boolean Ret = true;
            string Message = "";

            if (ddlDealerOffice.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Dealer Office";
                Ret = false;
                ddlDealerOffice.BorderColor = Color.Red;
            }

            lblMessage.Text = Message;
            return Ret;
        }
    }
}