using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketFSR : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }

        //public PDMS_MachineAttachedFile MachineBeforeFile
        //{
        //    get
        //    {
        //        if (Session["DMS_MachineBeforeFile"] == null)
        //        {
        //            Session["DMS_MachineBeforeFile"] = new PDMS_MachineAttachedFile();
        //        }
        //        return (PDMS_MachineAttachedFile)Session["DMS_MachineBeforeFile"];
        //    }
        //    set
        //    {
        //        Session["DMS_MachineBeforeFile"] = value;
        //    }
        //}
        //public PDMS_MachineAttachedFile MachineAfterFile
        //{
        //    get
        //    {
        //        if (Session["DMS_MachineAfterFile"] == null)
        //        {
        //            Session["DMS_MachineAfterFile"] = new PDMS_MachineAttachedFile();
        //        }
        //        return (PDMS_MachineAttachedFile)Session["DMS_MachineAfterFile"];
        //    }
        //    set
        //    {
        //        Session["DMS_MachineAfterFile"] = value;
        //    }
        //}

        //public PDMS_ICTicketFSR ICTicketFSR
        //{             
        //    get;
        //    set;
        //}
        public PDMS_ICTicketFSR SDMS_ICTicketFSR
        {
            get
            {
                if (Session["PDMS_ICTicketFSR"] == null)
                {
                    Session["PDMS_ICTicketFSR"] = new PDMS_ICTicketFSR();
                }
                return (PDMS_ICTicketFSR)Session["PDMS_ICTicketFSR"];
            }
            set
            {
                Session["PDMS_ICTicketFSR"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new BDMS_MachineMaintenanceLevel().GetMachineMaintenanceLevelDDL(ddlMachineMaintenanceLevel, null, null);
                FillGetModeOfPayment();
                //  ICTicketFSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, null, null, SDMS_ICTicket.ICTicketNumber, null, null, null);
                FillFSRDetails();
                fillICTicketAttachedFile();
                DMS_AvailabilityOfOtherMachine.FillAvailabilityOfOtherMachine(SDMS_ICTicket.ICTicketID);
            }

            Display();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            //  btnSave.Focus();

            if (!Validatetion())
            {
                return;
            }

            string Report = "";


            int? ModeOfPaymentID = null;
            int? MachineMaintenanceLevelID = null;

            MachineMaintenanceLevelID = ddlMachineMaintenanceLevel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMachineMaintenanceLevel.SelectedValue);
            lblMessage.ForeColor = Color.Red;
            Report = txtReport.Text.Trim();

            ModeOfPaymentID = ddlModeOfPayment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModeOfPayment.SelectedValue);



            string NatureOfComplaint = txtNatureOfComplaint.Text.Trim();
            string Observation = txtObservation.Text.Trim();
            string WorkCarriedOut = txtWorkCarriedOut.Text.Trim();
            // string SERecommendedParts = txtSERecommendedParts.Text.Trim();

            long IsNew = SDMS_ICTicketFSR.FsrID;

            long FsrIDP = new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSR_New(SDMS_ICTicketFSR.FsrID, SDMS_ICTicket.ICTicketID
                 , cbIsRental.Checked, txtOperatorName.Text.Trim(), txtOperatorNumber.Text.Trim(), txtRentalName.Text.Trim(),
                 MachineMaintenanceLevelID, ModeOfPaymentID, Report, txtRentalNumber.Text.Trim(), NatureOfComplaint, Observation
                 , WorkCarriedOut, "", PSession.User.UserID);

            if (FsrIDP != 0)
            {
                SDMS_ICTicketFSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(FsrIDP, null, null, null, "", null, null, null);
                Display();
                lblMessage.ForeColor = Color.Green;
                string Year = "";
                if (SDMS_ICTicketFSR.FSRDate.Month > 3)
                {
                    Year = SDMS_ICTicketFSR.FSRDate.Year.ToString().Substring(2, 2) + "-" + (SDMS_ICTicketFSR.FSRDate.Year + 1).ToString().Substring(2, 2);
                }
                else
                {
                    Year = (SDMS_ICTicketFSR.FSRDate.Year - 1).ToString().Substring(2, 2) + "" + SDMS_ICTicketFSR.FSRDate.Year.ToString().Substring(2, 2);
                }

                string FSRNumber = SDMS_ICTicketFSR.ICTicket.Dealer.DealerCode + "/" + SDMS_ICTicketFSR.ICTicket.ICTicketNumber + "/" + SDMS_ICTicketFSR.ICTicket.Technician.UserName + "/" + Year;


                if (IsNew == 0)
                {
                    lblMessage.Text = "FSR Number (" + FSRNumber + ") Created Successfully ";
                }
                else
                {
                    lblMessage.Text = "FSR Number (" + FSRNumber + ") Updated Successfully ";
                    pnlAttachments.Enabled = true;
                }
            }
            else
            {
                if (IsNew == 0)
                {
                    lblMessage.Text = "FSR Number is not Created Successfully ";
                }
                else
                {
                    lblMessage.Text = "FSR is not Updated Successfully ";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        //protected void btnUpload_Click(object sender, EventArgs e)
        //{
        //    if (fuMachineBefore.PostedFile != null)
        //    {
        //        if (fuMachineBefore.PostedFile.FileName.Length != 0)
        //        {
        //            MachineBeforeFile = CreateUploadedFile(fuMachineBefore.PostedFile);
        //            lblMachineBefore.Text = fuMachineBefore.FileName;
        //            lbMachineBeforeRemove.Visible = true;
        //            lbMachineBeforeDownload.Visible = true;
        //            fuMachineBefore.Visible = false;
        //            MachineBeforeFile.AttachedFileID = ViewState["MachineBeforeAttachedFileID"] == null ? 0 : (long)ViewState["MachineBeforeAttachedFileID"];
        //        }
        //    }
        //    if (fuMachineAfter.PostedFile != null)
        //    {
        //        if (fuMachineAfter.PostedFile.FileName.Length != 0)
        //        {
        //            MachineAfterFile = CreateUploadedFile(fuMachineAfter.PostedFile);
        //            lblMachineAfter.Text = fuMachineAfter.FileName;
        //            lbMachineAfterRemove.Visible = true;
        //            lbMachineAfterDownload.Visible = true;
        //            fuMachineAfter.Visible = false;
        //            MachineAfterFile.AttachedFileID = ViewState["MachineAfterAttachedFileID"] == null ? 0 : (long)ViewState["MachineAfterAttachedFileID"];
        //        }
        //    }

        //}
        private PDMS_MachineAttachedFile CreateUploadedFile(HttpPostedFile file)
        {

            PDMS_MachineAttachedFile AttachedFile = new PDMS_MachineAttachedFile();
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
            return AttachedFile;

        }
        //protected void lbMachineBeforeRemove_Click(object sender, EventArgs e)
        //{
        //    MachineBeforeFile = new PDMS_MachineAttachedFile();
        //    lblMachineBefore.Text = "";
        //    lbMachineBeforeRemove.Visible = false;
        //    lbMachineAfterDownload.Visible = false;
        //    fuMachineBefore.Visible = true;
        //    lbMachineBeforeDownload.Visible = false;

        //}
        //protected void lbMachineBeforeDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        Response.AddHeader("Content-type", SDMS_ICTicketFSR.MachineBeforeAttachedFile.FileType);
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + SDMS_ICTicketFSR.MachineBeforeAttachedFile.FileName);
        //        HttpContext.Current.Response.Charset = "utf-16";
        //        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //        Response.BinaryWrite(SDMS_ICTicketFSR.MachineBeforeAttachedFile.AttachedFile);
        //        Response.Flush();
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    { }
        //}
        //protected void lbMachineAfterRemove_Click(object sender, EventArgs e)
        //{
        //    MachineAfterFile = new PDMS_MachineAttachedFile();
        //    lblMachineAfter.Text = "";
        //    lbMachineAfterRemove.Visible = false;
        //    lbMachineAfterDownload.Visible = false;
        //    fuMachineAfter.Visible = true;
        //}
        //protected void lbMachineAfterDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        Response.AddHeader("Content-type", SDMS_ICTicketFSR.MachineAfterAttachedFile.FileType);
        //        Response.AddHeader("Content-Disposition", "attachment; filename=" + SDMS_ICTicketFSR.MachineAfterAttachedFile.FileName);
        //        HttpContext.Current.Response.Charset = "utf-16";
        //        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //        Response.BinaryWrite(SDMS_ICTicketFSR.MachineAfterAttachedFile.AttachedFile);
        //        Response.Flush();
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    { }
        //}
        private void FillGetModeOfPayment()
        {
            ddlModeOfPayment.DataTextField = "ModeOfPayment";
            ddlModeOfPayment.DataValueField = "ModeOfPaymentID";
            ddlModeOfPayment.DataSource = new BDMS_ModeOfPayment().GetModeOfPayment(null, null);
            ddlModeOfPayment.DataBind();
            ddlModeOfPayment.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillFSRDetails()
        {
            if (SDMS_ICTicketFSR.FsrID != 0)
            {
                if (SDMS_ICTicketFSR.MachineMaintenanceLevel != null)
                    ddlMachineMaintenanceLevel.SelectedValue = SDMS_ICTicketFSR.MachineMaintenanceLevel.MachineMaintenanceLevelID.ToString();
                cbIsRental.Checked = SDMS_ICTicketFSR.IsRental;
                txtOperatorName.Text = SDMS_ICTicketFSR.OperatorName;
                txtOperatorNumber.Text = SDMS_ICTicketFSR.OperatorNumber;
                txtRentalName.Text = SDMS_ICTicketFSR.RentalName;
                txtRentalNumber.Text = SDMS_ICTicketFSR.RentalNumber;
                //  ddlComplaintStatus.SelectedValue = ICTicketFSR.ComplaintStatus;
                //  txtAlternateContactNumber.Text = ICTicketFSR.PresentContactNumberA;
                if (SDMS_ICTicketFSR.ModeOfPayment != null)
                    ddlModeOfPayment.SelectedValue = SDMS_ICTicketFSR.ModeOfPayment.ModeOfPaymentID.ToString();
                txtReport.Text = SDMS_ICTicketFSR.Report;
                txtNatureOfComplaint.Text = SDMS_ICTicketFSR.NatureOfComplaint;
                txtObservation.Text = SDMS_ICTicketFSR.Observation;
                txtWorkCarriedOut.Text = SDMS_ICTicketFSR.WorkCarriedOut;
                //  txtSERecommendedParts.Text = ICTicketFSR.SERecommendedParts;

                //if (SDMS_ICTicketFSR.MachineBeforeAttachedFile != null)
                //{
                //    MachineBeforeFile = SDMS_ICTicketFSR.MachineBeforeAttachedFile;
                //    lblMachineBefore.Text = MachineBeforeFile.FileName;
                //    lbMachineBeforeRemove.Visible = true;
                //    lbMachineBeforeDownload.Visible = true;
                //    fuMachineBefore.Visible = false;
                //}
                //if (ICTicketFSR.MachineAfterAttachedFile != null)
                //{
                //    MachineAfterFile = ICTicketFSR.MachineAfterAttachedFile;
                //    lblMachineAfter.Text = MachineAfterFile.FileName;
                //    lbMachineAfterRemove.Visible = true;
                //    lbMachineAfterDownload.Visible = true;
                //    fuMachineAfter.Visible = false;
                //}
            }
        }
        private PDMS_FSRAttachedFile CreateUploadedFileFSR(HttpPostedFile file)
        {
            PDMS_FSRAttachedFile AttachedFile = new PDMS_FSRAttachedFile();
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
            AttachedFile.ICTicket = new PDMS_ICTicket() { ICTicketID = SDMS_ICTicket.ICTicketID };
            //     AttachedFile.FSR = new PDMS_ICTicketFSR() { FsrID = SDMS_ICTicket.ICTicketID };
            return AttachedFile;
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);
                PDMS_FSRAttachedFile UploadedFile = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(AttachedFileID);

                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName.Replace(",", " "));
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }
        }

        void fillICTicketAttachedFile()
        {
            try
            {
                List<PDMS_FSRAttachedFile> UploadedFile = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(SDMS_ICTicket.ICTicketID, null);
                if (UploadedFile.Count == 0)
                {
                    UploadedFile.Add(new PDMS_FSRAttachedFile() { });
                }
                gvAttachedFile.DataSource = UploadedFile;
                gvAttachedFile.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lblAttachedFileAdd_Click(object sender, EventArgs e)
        {
            DropDownList ddlFSRAttachedName = (DropDownList)gvAttachedFile.FooterRow.FindControl("ddlFSRAttachedName");
            LinkButton lblAttachedFileAdd = (LinkButton)gvAttachedFile.FooterRow.FindControl("lblAttachedFileAdd");
            lblAttachedFileAdd.Focus();
            lblMessage.Visible = true;
            PDMS_FSRAttachedFile AttachedFile = new PDMS_FSRAttachedFile();
            FileUpload fu = (FileUpload)gvAttachedFile.FooterRow.FindControl("fu");

            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string ext = System.IO.Path.GetExtension(fu.PostedFile.FileName).ToLower();
            List<string> Extension = new List<string>();
            Extension.Add(".jpg");
            Extension.Add(".png");
            Extension.Add(".gif");
            Extension.Add(".jpeg");
            if (!Extension.Contains(ext))
            {
                //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Please choose only .jpg, .png and .gif image types!')", true);
                lblMessage.Text = "Please choose only .jpg, .png and .gif image types!";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            AttachedFile = CreateUploadedFileFSR(fu.PostedFile);
            AttachedFile.FSRAttachedName = new PDMS_FSRAttachedName() { FSRAttachedFileNameID = Convert.ToInt32(ddlFSRAttachedName.SelectedValue) };
            for (int i = 0; i < gvAttachedFile.Rows.Count; i++)
            {
                if (ddlFSRAttachedName.SelectedItem.Text == AttachedFile.FileName)
                {
                    lblMessage.Text = "This file already available";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }
            if (new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessage.Text = "File added";
                lblMessage.ForeColor = Color.Green;

                fillICTicketAttachedFile();
            }
            else
            {
                lblMessage.Text = "File is not added";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void lblAttachedFileRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);

            PDMS_FSRAttachedFile AttachedFile = new PDMS_FSRAttachedFile();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.IsDeleted = true;
            if (new BDMS_ICTicketFSR().InsertOrUpdateICTicketFSRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessage.Text = "File Removed";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "File is not Removed";
                lblMessage.ForeColor = Color.Red;
            }
            fillICTicketAttachedFile();
            //LinkButton lblAttachedFileAdd = (LinkButton)gvAttachedFile.FooterRow.FindControl("lblAttachedFileAdd");
            //lblAttachedFileAdd.Focus();
        }

        protected void gvAttachedFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlFSRAttachedName = (DropDownList)e.Row.FindControl("ddlFSRAttachedName");
                ddlFSRAttachedName.DataTextField = "FSRAttachedName";
                ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
                ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, true, null);
                ddlFSRAttachedName.DataBind();
            }
        }

        Boolean Validatetion()
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;

            PDMS_Material MaterialsDescription = new BDMS_Material().GetMaterialServiceByMaterialAndDescription(txtNatureOfComplaint.Text.Trim());

            if (string.IsNullOrEmpty(MaterialsDescription.MaterialCode))
            {
                lblMessage.Text = "Nature of Complaint is not available.";
                return false;
            }
            return true;
        }
        void Display()
        {
            //if (SDMS_ICTicketFSR.FsrID != 0)
            //{
            //    pnlAttachments.Enabled = true;
            //}
            //else
            //{
            //    pnlAttachments.Enabled = false;
            //}
        }
    }
}