using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerEmployeeLeaving : System.Web.UI.Page
    {
        public List<PDMS_DealerEmployee> EmployeeManageLeave
        {
            get
            {
                if (Session["EmployeeManageLeave"] == null)
                {
                    Session["EmployeeManageLeave"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["EmployeeManageLeave"];
            }
            set
            {
                Session["EmployeeManageLeave"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_DealerEmployeeLeaving.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealership Employee » Leaving / Exit');</script>");

            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                ViewState["PhotoAttachedFileID"] = null;
                ViewState["AdhaarCardCopyFrontSideAttachedFileID"] = null;
                ViewState["AdhaarCardCopyBackSideAttachedFileID"] = null;
                ViewState["PANCardCopyAttachedFileID"] = null;
                ViewState["ChequeCopyAttachedFileID"] = null;
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
                caDateOfJoining.StartDate = DateTime.Now.AddDays(-7);
                caDateOfJoining.EndDate = DateTime.Now;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerEmployeeManageLeaving();

        }
        private void FillDealerEmployeeManageLeaving()
        {
            EmployeeManageLeave = new BDMS_Dealer().GetDealerEmployeeManageLeaving(Convert.ToInt32(ddlDealer.SelectedValue), txtName.Text.Trim());
            gvDealerEmployee.DataSource = EmployeeManageLeave;
            gvDealerEmployee.DataBind();
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
            lblContactNumber.Text = "<a href='tel:" + Emp.ContactNumber + "'>" + Emp.ContactNumber + "</a>";

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

            lblEmail.Text = "<a href='mailto:" + Emp.Email + "'>" + Emp.Email + "</a>";
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
            Role.WishToLeave = Convert.ToBoolean(Convert.ToInt32(ddlWishToLeave.SelectedValue));
            Role.DateOfLeaving = Convert.ToDateTime(txtDateOfLeaving.Text.Trim());

            Role.DealerDepartment = new PDMS_DealerDepartment();

            if (new BDMS_Dealer().UpdateDealerEmployeeLeaving(Role, PSession.User.UserID))
            {
                lblMessage.Text = "Employee deactivated from role  ";
                lblMessage.ForeColor = Color.Green;
                btnAssignRole.Visible = false;
                FillDealerEmployeeRole();
                txtDateOfLeaving.Text = "";
            }
            else
            {
                lblMessage.Text = "Employee is not deactivated from role  ";
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
            txtDateOfLeaving.Text = "";
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.DataSource = EmployeeManageLeave;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;

                gvDealerEmployee.DataBind();
                lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageLeave.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.DataSource = EmployeeManageLeave;
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                gvDealerEmployee.DataBind();
                lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageLeave.Count;
            }
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
            txtDateOfLeaving.Text = "";
        }

        Boolean Validation()
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            txtDateOfLeaving.BorderColor = Color.Silver;
            ddlWishToLeave.BorderColor = Color.Silver;
            Boolean Ret = true;
            string Message = "";
            if (string.IsNullOrEmpty(txtDateOfLeaving.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Date of Leaving";
                Ret = false;
                txtDateOfLeaving.BorderColor = Color.Red;
            }
            if (ddlWishToLeave.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Wish To Leave";
                Ret = false;
                ddlWishToLeave.BorderColor = Color.Red;
            }

            DateTime DateOfLeaving = Convert.ToDateTime(txtDateOfLeaving.Text.Trim());

            if (caDateOfJoining.StartDate <= DateOfLeaving && DateOfLeaving <= caDateOfJoining.EndDate)
            {
            }
            else
            {
                Message = Message + "<br/>Please Check the Date Of Leaving";
                Ret = false;
                txtDateOfLeaving.BorderColor = Color.Red;
            }


            List<PDMS_DealerEmployeeRole> Role = new BDMS_Dealer().GetDealerEmployeeRole(null, (int)ViewState["DealerEmployeeID"], null, null);
            if (Role.Count() != 0)
            {
                //   DateTime dt = Convert.ToDateTime(txtDateOfLeaving.Text.Trim());
                foreach (PDMS_DealerEmployeeRole R in Role)
                {
                    if (R.DateOfJoining > DateOfLeaving)
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

        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.DataSource = EmployeeManageLeave;
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            gvDealerEmployee.DataBind();
            lblRowCount.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + EmployeeManageLeave.Count;
        }
    }
}