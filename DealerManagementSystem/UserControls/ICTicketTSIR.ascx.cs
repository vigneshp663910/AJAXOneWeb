using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketTSIR : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
        public PDMS_ICTicketTSIR P_ICTicketTSIR
        {
            get
            {
                if (ViewState["PDMS_ICTicketTSIR"] == null)
                {
                    ViewState["PDMS_ICTicketTSIR"] = new PDMS_ICTicketTSIR();
                }
                return (PDMS_ICTicketTSIR)ViewState["PDMS_ICTicketTSIR"];
            }
            set
            {
                ViewState["PDMS_ICTicketTSIR"] = value;
            }
        }
        public List<PDMS_ICTicketTSIR> ICTicketTSIRs
        {
            get
            {
                if (Session["PDMS_ICTicketTSIRs"] == null)
                {
                    Session["PDMS_ICTicketTSIRs"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["PDMS_ICTicketTSIRs"];
            }
            set
            {
                Session["PDMS_ICTicketTSIRs"] = value;
            }
        }
       
        public List<PDMS_ServiceMaterial> SS_ServiceMaterial
        {
            get
            {
                if (Session["ServiceMaterialICTicketProcess"] == null)
                {
                    Session["ServiceMaterialICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)Session["ServiceMaterialICTicketProcess"];
            }
            set
            {
                Session["ServiceMaterialICTicketProcess"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageTSIR.Visible = false;
            if (!IsPostBack)
            {
                FillSROCoder();
                FillTSIRDetails();
            }
        }
        public void FillTSIRDetails()
        {
            ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
            gvTSIR.DataSource = ICTicketTSIRs;
            gvTSIR.DataBind();
            string[] TsirCancel = ConfigurationManager.AppSettings["TsirCancel"].Split(',');
            for (int i = 0; i < gvTSIR.Rows.Count; i++)
            {
                LinkButton lblCancelTSIR = (LinkButton)gvTSIR.Rows[i].FindControl("lblCancelTSIR");
                lblCancelTSIR.Visible = false;
                if (TsirCancel.Contains(PSession.User.UserID.ToString()))
                {
                    lblCancelTSIR.Visible = true;
                }
            }
        }
        protected void cbCheck_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            CheckBox cbCheck = (CheckBox)gvTSIR.Rows[gvRow.RowIndex].FindControl("cbCheck");
            if (cbCheck.Checked)
            {

                for (int i = 0; i < gvTSIR.Rows.Count; i++)
                {
                    if (i != gvRow.RowIndex)
                    {
                        CheckBox cbChecki = (CheckBox)gvTSIR.Rows[i].FindControl("cbCheck");
                        cbChecki.Checked = false;
                    }
                }
                long TsirID = Convert.ToInt64(gvTSIR.DataKeys[gvRow.RowIndex].Value);
                PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
                if (!((TSIR.Status.StatusID == (short)TSIRStatus.Requested) || (TSIR.Status.StatusID == (short)TSIRStatus.SendBack) || (TSIR.Status.StatusID == (short)TSIRStatus.Rerequested)))
                {
                    cbCheck.Checked = false;
                    lblMessageTSIR.Text = "You cannot edit this TSIR. It may be Checked or Approved or Rejected";
                    lblMessageTSIR.Visible = true;
                    lblMessageTSIR.ForeColor = Color.Red;
                    ClearTSIR();
                    return;
                }
                P_ICTicketTSIR = new PDMS_ICTicketTSIR();
                P_ICTicketTSIR.ICTicket = new PDMS_ICTicket { ICTicketID = SDMS_ICTicket.ICTicketID };
                P_ICTicketTSIR.TsirID = TsirID;

                txtNatureOfFailures.Text = TSIR.NatureOfFailures;
                txtProblemNoticedBy.Text = TSIR.ProblemNoticedBy;
                txtUnderWhatConditionFailureTaken.Text = TSIR.UnderWhatConditionFailureTaken;
                txtFailureDetails.Text = TSIR.FailureDetails;
                txtPointsChecked.Text = TSIR.PointsChecked;
                txtPossibleRootCauses.Text = TSIR.PossibleRootCauses;
                txtSpecificPointsNoticed.Text = TSIR.SpecificPointsNoticed;
                txtPartsInvoiceNumber.Text = TSIR.PartsInvoiceNumber;
                ViewState["TsirID"] = TSIR.TsirID;
                //   ClearTSIR();
                //  mp1.Show(); 
                //fillICTicketAttachedFile(ICTicketTSIR.TsirID);
                //  LinkButton lblAttachedFileAdd = (LinkButton)gvAttachedFile.FooterRow.FindControl("lblAttachedFileAdd");
                // btnSave.Focus();
            }
            else
            {
                ClearTSIR();

            }
            //  btnSave.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessageTSIR.Visible = true;
            if (!Validation())
            {
                return;
            }
            P_ICTicketTSIR.TsirID = ViewState["TsirID"] == null ? 0 : (long)ViewState["TsirID"];
            long ServiceChargeID = Convert.ToInt64(ddlServiceChargeID.SelectedValue);
            if (P_ICTicketTSIR.TsirID == 0)
            {
                foreach (PDMS_ICTicketTSIR TSIR in ICTicketTSIRs)
                {
                    if ((TSIR.ServiceCharge.Material.MaterialCode == ddlServiceChargeID.SelectedItem.Text) && (TSIR.Status.StatusID != (short)TSIRStatus.Canceled))
                    {
                        lblMessageTSIR.Text = "TSIR already Created for " + ddlServiceChargeID.SelectedItem.Text + " Service Code";
                        lblMessageTSIR.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            P_ICTicketTSIR.ICTicket = new PDMS_ICTicket();
            P_ICTicketTSIR.ICTicket.ICTicketID = SDMS_ICTicket.ICTicketID;
            P_ICTicketTSIR.ServiceCharge = new PDMS_ServiceCharge();
            P_ICTicketTSIR.ServiceCharge.ServiceChargeID = ServiceChargeID;
            P_ICTicketTSIR.NatureOfFailures = txtNatureOfFailures.Text.Trim();
            P_ICTicketTSIR.ProblemNoticedBy = txtProblemNoticedBy.Text.Trim();
            P_ICTicketTSIR.UnderWhatConditionFailureTaken = txtUnderWhatConditionFailureTaken.Text.Trim();
            P_ICTicketTSIR.FailureDetails = txtFailureDetails.Text.Trim();
            P_ICTicketTSIR.PointsChecked = txtPointsChecked.Text.Trim();
            P_ICTicketTSIR.PossibleRootCauses = txtPossibleRootCauses.Text.Trim();
            P_ICTicketTSIR.SpecificPointsNoticed = txtSpecificPointsNoticed.Text.Trim();
            P_ICTicketTSIR.PartsInvoiceNumber = txtPartsInvoiceNumber.Text.Trim();
            long DealerEmployeeID = new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIR(P_ICTicketTSIR, PSession.User.UserID);
            if (DealerEmployeeID != 0)
            {
                lblMessageTSIR.Text = "TSIR is updated successfully";
                lblMessageTSIR.ForeColor = Color.Green;
                ClearTSIR();
                //fillICTicketAttachedFile(DealerEmployeeID);
                // ViewState["TsirID"] = DealerEmployeeID;
                FillTSIRDetails();
            }
            else
            {
                lblMessageTSIR.Text = "TSIR is not updated successfully";
                lblMessageTSIR.ForeColor = Color.Red;
            }
        }

        public void FillSROCoder()
        {
            // List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            var productCodes = (from p1 in SDMS_ICTicket.ServiceCharges select new { p1.ServiceChargeID, p1.Material.MaterialCode, p1.Material.IsMainServiceMaterial, p1.Material.MaterialGroup }).Where(m => m.IsMainServiceMaterial == false && m.MaterialGroup != "891").Distinct();

            ddlServiceChargeID.DataSource = productCodes;
            ddlServiceChargeID.DataBind();
        }
        //void FillUser()
        //{

        //    List<PUser> u = new BUser().GetUsers(null, "", null, "");
        //    u = u.FindAll(m => m.SystemCategoryID == (short)SystemCategory.Dealer);
        //    ddlProblemNoticedBy.DataTextField = "ContactName";
        //    ddlProblemNoticedBy.DataValueField = "UserID";
        //    ddlProblemNoticedBy.DataSource = u;
        //    ddlProblemNoticedBy.DataBind();
        //    ddlProblemNoticedBy.Items.Insert(0, new ListItem("Select", "0"));
        //}       

        void ClearTSIR()
        {
            // txtFailureRepeats.Text = "";
            txtNatureOfFailures.Text = "";
            //ddlProblemNoticedBy.SelectedValue = "0";
            txtProblemNoticedBy.Text = "";
            txtUnderWhatConditionFailureTaken.Text = "";
            txtFailureDetails.Text = "";
            txtPointsChecked.Text = "";
            txtPossibleRootCauses.Text = "";
            txtSpecificPointsNoticed.Text = "";
            txtPartsInvoiceNumber.Text = "";
            //  txtHOComments.Text = TSIR.HOComments;
            ViewState["TsirID"] = null;
            //    txtSERecommendedParts.Text = "";
            List<PDMS_TSIRAttachedFile> UploadedFile = new List<PDMS_TSIRAttachedFile>();
            //   gvAttachedFile.DataSource = UploadedFile;
            //   gvAttachedFile.DataBind();
        }

        Boolean Validation()
        {
            lblMessageTSIR.Visible = true;
            string Message = "";
            Boolean Ret = true;
            //if (string.IsNullOrEmpty(txtFailureRepeats.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please Enter the Failure Repeats";
            //    Ret = false;
            //}

            if (string.IsNullOrEmpty(txtNatureOfFailures.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the NatureOfFailures";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtProblemNoticedBy.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the How Was Problem Noticed / Who  / When";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtUnderWhatConditionFailureTaken.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Under What Condition Failure Taken";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtFailureDetails.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Failure Details";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtPointsChecked.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Points Checked";
                Ret = false;
            }

            if (string.IsNullOrEmpty(txtPossibleRootCauses.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Possible Root Causes";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtSpecificPointsNoticed.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the SpecificPoints Noticed";
                Ret = false;
            }
            //if (string.IsNullOrEmpty(txtSERecommendedParts.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please Enter the SE Recommended Parts";
            //    Ret = false;
            //}
            lblMessageTSIR.Text = Message;
            return Ret;
        }
        private PDMS_TSIRAttachedFile CreateUploadedFileFSR(HttpPostedFile file)
        {

            PDMS_TSIRAttachedFile AttachedFile = new PDMS_TSIRAttachedFile();
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

        protected void gvAttachedFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlFSRAttachedName = (DropDownList)e.Row.FindControl("ddlFSRAttachedName");
                ddlFSRAttachedName.DataTextField = "FSRAttachedName";
                ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
                ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, null, true);
                ddlFSRAttachedName.DataBind();
            }
        }
        protected void gvAttachedFileNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlFSRAttachedName = (DropDownList)e.Row.FindControl("ddlFSRAttachedName");
                ddlFSRAttachedName.DataTextField = "FSRAttachedName";
                ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
                ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, null, true);
                ddlFSRAttachedName.DataBind();
            }
        }

        protected void gvTSIR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string TsirID = Convert.ToString(gvTSIR.DataKeys[e.Row.RowIndex].Value);
                    GridView gvAttachedFile = (GridView)e.Row.FindControl("gvAttachedFile");

                    Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                    LinkButton lblCancelTSIR = (LinkButton)e.Row.FindControl("lblCancelTSIR");
                    if (Convert.ToInt32(lblStatusID.Text) == (short)TSIRStatus.Canceled)
                    {
                        lblCancelTSIR.Visible = false;
                    }

                    List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, Convert.ToInt64(TsirID), null);
                    //if (UploadedFile.Count == 0)
                    //{
                    //    UploadedFile.Add(new PDMS_TSIRAttachedFile() { });
                    //}
                    gvAttachedFile.DataSource = UploadedFile;
                    gvAttachedFile.DataBind();
                    //for (int i = 0; i < gvAttachedFile.Rows.Count; i++)
                    //{

                    //   DropDownList ddlFSRAttachedName = (DropDownList)gvAttachedFile.FooterRow.FindControl("ddlFSRAttachedName");
                    DropDownList ddlFSRAttachedName = (DropDownList)e.Row.FindControl("ddlFSRAttachedName");

                    ddlFSRAttachedName.DataTextField = "FSRAttachedName";
                    ddlFSRAttachedName.DataValueField = "FSRAttachedFileNameID";
                    ddlFSRAttachedName.DataSource = new BDMS_ICTicketFSR().GetFSRAttachedFileName(null, null, null, true);
                    ddlFSRAttachedName.DataBind();
                    // }

                }

                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIR", "gvTSIR_RowDataBound", ex);
                throw ex;
            }
        }

        protected void lblAttachedFileAddR_Click(object sender, EventArgs e)
        {
            //mp1.Show();
            LinkButton lblAttachedFileAdd = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lblAttachedFileAdd.NamingContainer;
            int index = row.RowIndex;
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //int index = gvRow.RowIndex;
            DropDownList ddlFSRAttachedName = (DropDownList)gvTSIR.Rows[index].FindControl("ddlFSRAttachedName");
            //  LinkButton lblAttachedFileAdd = (LinkButton)gvTSIR.Rows[index].FindControl("lblAttachedFileAdd");
            GridView gvAF = (GridView)gvTSIR.Rows[index].FindControl("gvAttachedFile");
            lblMessageTSIR.Visible = true;
            PDMS_TSIRAttachedFile AttachedFile = new PDMS_TSIRAttachedFile();
            FileUpload fu = (FileUpload)gvTSIR.Rows[index].FindControl("fu");
            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessageTSIR.Text = "Please select the file";
                lblMessageTSIR.ForeColor = Color.Red;
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
                lblMessageTSIR.Text = "Please choose only .jpg, .png and .gif image types!";
                lblMessageTSIR.ForeColor = Color.Red;
                return;
            }
            AttachedFile = CreateUploadedFileFSR(fu.PostedFile);
            AttachedFile.TSIR = new PDMS_ICTicketTSIR();
            AttachedFile.TSIR.TsirID = Convert.ToInt64(gvTSIR.DataKeys[index].Value);
            AttachedFile.FSRAttachedName = new PDMS_FSRAttachedName() { FSRAttachedFileNameID = Convert.ToInt32(ddlFSRAttachedName.SelectedValue) };
            for (int i = 0; i < gvAF.Rows.Count; i++)
            {
                if (ddlFSRAttachedName.SelectedItem.Text == AttachedFile.FileName)
                {
                    lblMessageTSIR.Text = "This file already available";
                    lblMessageTSIR.ForeColor = Color.Red;
                    return;
                }
            }
            if (new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessageTSIR.Text = "File added";
                lblMessageTSIR.ForeColor = Color.Green;
                //  fillICTicketAttachedFile(ICTicketTSIR.TsirID);
                try
                {
                    List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, AttachedFile.TSIR.TsirID, null);
                    gvAF.DataSource = UploadedFile;
                    gvAF.DataBind();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                lblMessageTSIR.Text = "File is not added";
                lblMessageTSIR.ForeColor = Color.Red;
            }
            btnSave.Focus();
        }
        protected void lblAttachedFileRemoveR_Click(object sender, EventArgs e)
        {
            lblMessageTSIR.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView Parentgrid = (GridView)(gvRow.Parent.Parent);
            long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

            GridViewRow GParentrow = (GridViewRow)(Parentgrid.NamingContainer);
            int GParentRowIndex = GParentrow.RowIndex;

            PDMS_TSIRAttachedFile AttachedFile = new PDMS_TSIRAttachedFile();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.ICTicket = new PDMS_ICTicket();
            AttachedFile.ICTicket.ICTicketID = SDMS_ICTicket.ICTicketID;
            AttachedFile.TSIR = new PDMS_ICTicketTSIR();
            AttachedFile.TSIR.TsirID = P_ICTicketTSIR.TsirID;
            AttachedFile.IsDeleted = true;
            if (new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIRAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessageTSIR.Text = "File Removed";
                lblMessageTSIR.ForeColor = Color.Green;
                try
                {
                    List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, Convert.ToInt64(gvTSIR.DataKeys[GParentRowIndex].Value), null);
                    Parentgrid.DataSource = UploadedFile;
                    Parentgrid.DataBind();
                }
                catch (Exception ex)
                { }
            }
            else
            {
                lblMessageTSIR.Text = "File is not Removed";
                lblMessageTSIR.ForeColor = Color.Red;
            }
            // fillICTicketAttachedFile(ICTicketTSIR.TsirID);
            // LinkButton lblAttachedFileAdd = (LinkButton)gvTSIR.Rows[GParentRowIndex].FindControl("lblAttachedFileAdd");
            //  lblAttachedFileAdd.Focus();
            //  btnSave.Focus();
            //  mp1.Show();
        }
        protected void lnkDownloadR_Click(object sender, EventArgs e)
        {
            try
            {
                //LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                //GridView Parentgrid = (GridView)(gvRow.Parent.Parent);
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;
                GridView Parentgrid = (GridView)(gvRow.NamingContainer);


                long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

                PDMS_TSIRAttachedFile UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileByID(AttachedFileID);
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
            }
        }

        protected void lblCancelTSIR_Click(object sender, EventArgs e)
        {
            List<string> querys = new List<string>();
            lblMessageTSIR.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long TsirID = Convert.ToInt64(gvTSIR.DataKeys[gvRow.RowIndex].Value);
            foreach (PDMS_ServiceCharge SC in SDMS_ICTicket.ServiceCharges)
            {
                if (!string.IsNullOrEmpty(SC.ClaimNumber))
                {
                    lblMessageTSIR.Text = "Service claim generated. Please cancel the Claim";
                    lblMessageTSIR.ForeColor = Color.Red;
                    return;
                }
            }
            foreach (PDMS_ServiceMaterial SM in SS_ServiceMaterial)
            {
                if (SM.TSIR.TsirID == TsirID)
                {
                    new BDMS_Service().UpdateSaleOrderNumberFromPostgres();
                    if (!string.IsNullOrEmpty(SM.ClaimNumber))
                    {
                        lblMessageTSIR.Text = "claim generated for Material " + SM.Material.MaterialCode;
                        lblMessageTSIR.ForeColor = Color.Red;
                        return;
                    }
                    if (!string.IsNullOrEmpty(SM.DeliveryNumber))
                    {
                        lblMessageTSIR.Text = "Delivery Completed for Material " + SM.Material.MaterialCode;
                        lblMessageTSIR.ForeColor = Color.Red;
                        return;
                    }
                }
            }
            Boolean ID = new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(TsirID, (short)TSIRStatus.Canceled, PSession.User.UserID, 0);
            if (ID)
            {
                lblMessageTSIR.Text = "TSIR is Canceled successfully";
                lblMessageTSIR.ForeColor = Color.Green;
                FillTSIRDetails();
            }
            else
            {
                lblMessageTSIR.Text = "TSIR is not Canceled successfully";
                lblMessageTSIR.ForeColor = Color.Red;
            }
        }
    }
}