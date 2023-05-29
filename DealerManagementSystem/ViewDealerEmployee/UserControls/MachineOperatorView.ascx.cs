using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee.UserControls
{
    public partial class MachineOperatorView : System.Web.UI.UserControl
    {
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Machine Operator View');</script>");
            if (!IsPostBack)
            {
                ViewState["MO_MachineOperatorDetailsID"] = null;
                ViewState["MO_PhotoAttachedFileID"] = null;
                ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"] = null;
                ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"] = null;
                ViewState["MO_PANCardCopyAttachedFileID"] = null;
                ViewState["MO_ChequeCopyAttachedFileID"] = null;
                ViewState["MO_DLCopyFrontSideAttachedFileID"] = null;
                ViewState["MO_DLCopyBackSideAttachedFileID"] = null;
                //if (!string.IsNullOrEmpty(Request.QueryString["MachineOperatorID"]))
                //{
                //    FillMachineOperator(Convert.ToInt64(Request.QueryString["MachineOperatorID"]));
                //}
            }
        }
        public void FillMachineOperator(long MachineOperatorDetailsID)
        {
            PMachineOperator Emp = new BMachineOperator().GetMachineOperatorDetailsByID(MachineOperatorDetailsID);
            Emp.ProductTypes = new BMachineOperator().GetMachineOperatorProductTypesByID(MachineOperatorDetailsID);
            lblName.Text = Emp.Name;
            lblFatherName.Text = Emp.FatherName;
            lblDOB.Text = Convert.ToString(Emp.DOB);
            lblContactNumber.Text = "<a href='tel:" + Emp.ContactNumber + "'>" + Emp.ContactNumber + "</a>";
            lblContactNumber1.Text = "<a href='tel:" + Emp.ContactNumber1 + "'>" + Emp.ContactNumber1 + "</a>";
            
            lblEmail.Text = "<a href='mailto:" + Emp.Email + "'>" + Emp.Email + "</a>";
            lblAddress.Text = Emp.Address;
            lblLocation.Text = Emp.Location;
            lblAadhaarCardNo.Text = Emp.AadhaarCardNo;
            lblTotalExperience.Text = Convert.ToString(Emp.TotalExperience);
            lblPANNo.Text = Emp.PANNo;
            lblBankName.Text = Emp.BankName;
            lblAccountNo.Text = Emp.AccountNo;
            lblIFSCCode.Text = Emp.IFSCCode;
            lblEmergencyContact.Text = Emp.EmergencyContactNumber;
            if (Emp.BloodGroup != null)
            {
                lblBloodGroup.Text = Emp.BloodGroup.BloodGroup;
            }
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

                ViewState["MO_PhotoAttachedFileID"] = Emp.Photo.AttachedFileID;

                PMachineOperatorAttachedFile PHFile = new BMachineOperator().GetMachineOperatorAttachedFile(Emp.Photo.AttachedFileID);
                PAttachedFile Files = new BMachineOperator().GetAttachedFileCustomerForDownload(PHFile.AttachedFileID + Path.GetExtension(PHFile.FileName));
                PHFile.AttachedFile = Files.AttachedFile;
                if (PHFile.FileName != null)
                {
                    string Url = "~/MachineOpPhotos/" + (Emp.MachineOperatorDetailsID).ToString() + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                    if (File.Exists(MapPath(Url)))
                    {
                        File.Delete(MapPath(Url));
                    }
                    FileSave(PHFile, (Emp.MachineOperatorDetailsID).ToString());
                    ibtnPhoto.ImageUrl = Url;
                }
            }
            if (Emp.AdhaarCardCopyFrontSide != null)
            {
                lblAdhaarCardCopyFrontSideFileName.Text = Emp.AdhaarCardCopyFrontSide.FileName;
                lbAdhaarCardCopyFrontSideFileName.Visible = true;
                ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"] = Emp.AdhaarCardCopyFrontSide.AttachedFileID;
            }
            if (Emp.AdhaarCardCopyBackSide != null)
            {
                lblAdhaarCardCopyBackSideFileName.Text = Emp.AdhaarCardCopyBackSide.FileName;
                lbAdhaarCardCopyBackSideFileName.Visible = true;
                ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"] = Emp.AdhaarCardCopyBackSide.AttachedFileID;
            }
            if (Emp.PANCardCopy != null)
            {
                lblPANCardCopyFileName.Text = Emp.PANCardCopy.FileName;
                lbPANCardCopyFileName.Visible = true;
                ViewState["MO_PANCardCopyAttachedFileID"] = Emp.PANCardCopy.AttachedFileID;
            }
            if (Emp.ChequeCopy != null)
            {
                lblChequeCopyFileName.Text = Emp.ChequeCopy.FileName;
                lbChequeCopyFileName.Visible = true;
                ViewState["MO_ChequeCopyAttachedFileID"] = Emp.ChequeCopy.AttachedFileID;
            }
            ibtnPhoto.Visible = true;
            List<PProductType> PT = new List<PProductType>();
            if (Emp.ProductTypes.Count > 0)
            {
                foreach (PMachineOperatorProductTypes types in Emp.ProductTypes)
                {
                    if (types.IsActive == true)
                    {
                        PProductType P = new PProductType();
                        P.ProductType = types.ProductType.ProductType;
                        P.ProductTypeID = types.ProductType.ProductTypeID;
                        PT.Add(P);
                    }
                }
            }
            lbProductTypes.DataTextField = "ProductType";
            lbProductTypes.DataValueField = "ProductTypeID";
            lbProductTypes.DataSource = PT;
            lbProductTypes.DataBind();
            lblDLInfo.Text = (Emp.DLInfo == true) ? "Yes" : "No";
            lblDLNumber.Text = Emp.DLNumber;
            lblDLIssueDate.Text = Emp.DLIssueDate.ToString();
            lblDLIssueingOffice.Text = Emp.DLIssueingOffice;
            lblDLExpiryDate.Text = Emp.DLExpiryDate.ToString();
            lblDLFor.Text = Emp.DLFor;
            if (Emp.DLFrontSide != null)
            {
                lblDLCopyFrontSide.Text = Emp.DLFrontSide.FileName;
                lbDLCopyFrontSide.Visible = true;
                ViewState["MO_DLCopyFrontSideAttachedFileID"] = Emp.DLFrontSide.AttachedFileID;
            }
            if (Emp.DLBackSide != null)
            {
                lblDLCopyBackSide.Text = Emp.DLBackSide.FileName;
                lbDLCopyBackSide.Visible = true;
                ViewState["MO_DLCopyBackSideAttachedFileID"] = Emp.DLBackSide.AttachedFileID;
            }
        }
        void FileSave(PMachineOperatorAttachedFile PHFile, string imgID)
        {
            try
            {
                string dir = Server.MapPath("~/MachineOpPhotos");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                //  string filePath = "~/MachineOpPhotos/" + Path.GetFileName(UploadedFile.FileName);
                string filePath = dir + "/" + imgID + "." + PHFile.FileName.Split('.')[PHFile.FileName.Split('.').Count() - 1];
                File.WriteAllBytes(filePath, PHFile.AttachedFile);
            }
            catch (Exception ex)
            {

            }
        }
        protected void lbPhotoFileName_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_PhotoAttachedFileID"]);
        }
        protected void lbfuAdhaarCardCopyFrontSide_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_AdhaarCardCopyFrontSideAttachedFileID"]);
        }
        protected void lbAdhaarCardCopyBackSide_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_AdhaarCardCopyBackSideAttachedFileID"]);
        }
        protected void lbPANCardCopy_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_PANCardCopyAttachedFileID"]);
        }
        protected void lbChequeCopy_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_ChequeCopyAttachedFileID"]);
        }
        protected void ibtnPhoto_Click(object sender, ImageClickEventArgs e)
        {
            FileDownload((long)ViewState["MO_PhotoAttachedFileID"]);
        }
        void FileDownload(long AttachedFileID)
        {
            try
            {
                PMachineOperatorAttachedFile UploadedFile = new BMachineOperator().GetMachineOperatorAttachedFile(AttachedFileID);
                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                PAttachedFile Files = new BMachineOperator().GetAttachedFileCustomerForDownload(UploadedFile.AttachedFileID + Path.GetExtension(UploadedFile.FileName));
                Response.BinaryWrite(Files.AttachedFile);
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
        protected void lbDLCopyFrontSide_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_DLCopyFrontSideAttachedFileID"]);
        }
        protected void lbDLCopyBackSide_Click(object sender, EventArgs e)
        {
            FileDownload((long)ViewState["MO_DLCopyBackSideAttachedFileID"]);
        }
    }
}