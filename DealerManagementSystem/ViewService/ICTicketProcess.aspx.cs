using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketProcess : System.Web.UI.Page
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get
            {
                if (Session["SDMS_ICTicket"] == null)
                {
                    Session["SDMS_ICTicket"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)Session["SDMS_ICTicket"];
            }
            set
            {
                Session["SDMS_ICTicket"] = value;
            }
        }
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
        public List<PDMS_ServiceCharge> SS_ServiceCharge
        {
            get
            {
                if (Session["ServiceChargeICTicketProcess"] == null)
                {
                    Session["ServiceChargeICTicketProcess"] = new List<PDMS_ServiceCharge>();
                }
                return (List<PDMS_ServiceCharge>)Session["ServiceChargeICTicketProcess"];
            }
            set
            {
                Session["ServiceChargeICTicketProcess"] = value;
            }
        }
        public List<PDMS_ServiceMaterial> SS_ServiceMaterialAll
        {
            get
            {
                if (Session["ServiceMaterialAllICTicketProcess"] == null)
                {
                    Session["ServiceMaterialAllICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)Session["ServiceMaterialAllICTicketProcess"];
            }
            set
            {
                Session["ServiceMaterialAllICTicketProcess"] = value;
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
        public List<Int16> RefreshList
        {
            get
            {
                if (Session["DMS_RefreshList"] == null)
                {
                    Session["DMS_RefreshList"] = new List<Int16>();
                }
                return (List<Int16>)Session["DMS_RefreshList"];
            }
            set
            {
                Session["DMS_RefreshList"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ICTicketProcess.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
            lblMessageServiceCenter.Text = "";
            lblMessageServiceCenter.Visible = false;
            if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
            {
                long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);

                SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);

                SDMS_ICTicketFSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, SDMS_ICTicket.ICTicketID, null, null, "", null, null, null);
                ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);

                SS_ServiceCharge = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
                SS_ServiceMaterialAll = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", null, "");
                SS_ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, null, "", false, "");

                UC_BasicInformation_N.SDMS_ICTicket = SDMS_ICTicket;
                DMS_ICTicketServiceCharges.SDMS_ICTicket = SDMS_ICTicket;
                DMS_ICTicketTSIR.SDMS_ICTicket = SDMS_ICTicket;

                DMS_ICTicketMaterialCharges.SDMS_ICTicket = SDMS_ICTicket;
                DMS_ICTicketNote.SDMS_ICTicket = SDMS_ICTicket;
                UC_TechnicianWorkInformation.SDMS_ICTicket = SDMS_ICTicket;

                DMS_ICTicketFSR.SDMS_ICTicket = SDMS_ICTicket;
                DMS_ICTicketFSR.SDMS_ICTicketFSR = SDMS_ICTicketFSR;

                UC_DMS_ICTicketRestore.SDMS_ICTicket = SDMS_ICTicket;

                UC_DMS_ICTicketRestore.ICTicketFSR = SDMS_ICTicketFSR;

                ICTicketTabDisplay();
                if (!IsPostBack)
                {
                    fillICTicketAttachedFile();
                    fillServiceCenterAttachedFile();
                }
            }
        }
        protected void ICTicketTabDisplay()
        {
            if (SDMS_ICTicket.ServiceStatus == null)
            {
                pnlUC_TechnicianWorkInformation.Enabled = false;
                pnlDMS_ICTicketServiceCharges.Enabled = false;
                pnlDMS_ICTicketMaterialCharges.Enabled = false;
            }
            else if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Open) || (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reopen))
            {
                pnlUC_TechnicianWorkInformation.Enabled = false;
                pnlDMS_ICTicketServiceCharges.Enabled = false;
                pnlDMS_ICTicketMaterialCharges.Enabled = false;
            }
            else if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.TechnicianAssigned))
            {
                pnlUC_TechnicianWorkInformation.Enabled = false;
                pnlDMS_ICTicketServiceCharges.Enabled = false;
                pnlDMS_ICTicketMaterialCharges.Enabled = false;
            }
            else if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reached) || (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Restored))
            {
                pnlUC_TechnicianWorkInformation.Enabled = true;
                pnlDMS_ICTicketServiceCharges.Enabled = false;
                pnlDMS_ICTicketMaterialCharges.Enabled = false;
                if ((SDMS_ICTicket.ServiceType != null) && (SDMS_ICTicket.DealerOffice != null) && (SDMS_ICTicket.CurrentHMRDate != null) && (SDMS_ICTicket.CurrentHMRValue != null))
                {
                    pnlDMS_ICTicketServiceCharges.Enabled = true;
                    if (
                           (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PolicyWarranty)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PartsWarranty)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.GoodwillWarranty)
                        || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PreCommission)
                        )
                    {
                        pnlDMS_ICTicketMaterialCharges.Enabled = true;
                    }
                    else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.NEPI) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Commission))
                    {
                        pnlDMS_ICTicketMaterialCharges.Enabled = false;
                    }
                }
            }

            if (SDMS_ICTicket.TypeOfWarranty != null)
            {
                if (SDMS_ICTicket.TypeOfWarranty.TypeOfWarrantyID == (short)TypeOfWarranty.OnlyForInfo)
                {
                    pnlDMS_ICTicketMaterialCharges.Enabled = false;
                }
            }
            //   List<PDMS_ServiceCharge> ServiceCharges = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            if (SS_ServiceCharge.Count != 0)
            {
                pnlICTicketRestore.Enabled = true;
            }
            else
            {
                pnlICTicketRestore.Enabled = false;
            }

            if (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService)
            {
                pnlTSIR.Visible = false;
                pnlAttachments.Visible = false;
                pnlServiceCenter.Visible = true;
            }
            else
            {
                pnlTSIR.Visible = true;
                pnlAttachments.Visible = true;
                pnlServiceCenter.Visible = false;
                if (gvAttachedFile.Rows.Count == 0)
                { pnlAttachments.Visible = false; }
            }
        }

        protected void ICTicketChanged()
        {
            GridView gvTechnicianWorkDays = (GridView)UC_TechnicianWorkInformation.FindControl("gvTechnicianWorkDays");
            DropDownList gvddlTechnician = (DropDownList)gvTechnicianWorkDays.FooterRow.FindControl("gvddlTechnician");
            int id = gvddlTechnician.SelectedIndex;
            gvddlTechnician.DataTextField = "ContactName";
            gvddlTechnician.DataValueField = "UserID";
            gvddlTechnician.DataSource = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);
            gvddlTechnician.DataBind();
            gvddlTechnician.Items.Insert(0, new ListItem("Select", "0"));
            gvddlTechnician.SelectedIndex = id;
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
            SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);

            UC_BasicInformation_N.SDMS_ICTicket = SDMS_ICTicket;
            UC_BasicInformation_N.EnableOrDesableBasedOnServiceCharges();


            DMS_ICTicketServiceCharges.SDMS_ICTicket = SDMS_ICTicket;

            if ((SDMS_ICTicket.ServiceType == null) || (SDMS_ICTicket.DealerOffice == null) || (SDMS_ICTicket.CurrentHMRDate == null) || (SDMS_ICTicket.CurrentHMRValue == null))
            {
                DMS_ICTicketServiceCharges.displayMessage();
            }
            DMS_ICTicketServiceCharges.FillServiceCharges();

            DMS_ICTicketMaterialCharges.SDMS_ICTicket = SDMS_ICTicket;
            DMS_ICTicketNote.SDMS_ICTicket = SDMS_ICTicket;
            UC_TechnicianWorkInformation.SDMS_ICTicket = SDMS_ICTicket;
            ICTicketChanged();
            ICTicketTabDisplay();

            Label lblModeOfPayment = (Label)DMS_ICTicketFSR.FindControl("lblModeOfPayment");
            DropDownList ddlModeOfPayment = (DropDownList)DMS_ICTicketFSR.FindControl("ddlModeOfPayment");
            if (SDMS_ICTicket.ServiceType == null)
            {
                lblModeOfPayment.Visible = false;
                ddlModeOfPayment.Visible = false;

            }
            else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.OverhaulService))
            {
                lblModeOfPayment.Visible = true;
                ddlModeOfPayment.Visible = true;
            }
            else
            {
                lblModeOfPayment.Visible = false;
                ddlModeOfPayment.Visible = false;
            }


            DMS_ICTicketMaterialCharges.FillTSIRNumberInGvDropDownList();

            ControlBaseOn60Days();

            if (RefreshList.Contains((short)RefreshEnum.ServiceChargesAddOrRemove))
            {
                RefreshList.Remove((short)RefreshEnum.ServiceChargesAddOrRemove);
                DMS_ICTicketTSIR.FillTSIRDetails();
                DMS_ICTicketTSIR.FillSROCoder();
            }
        }

        private PAttachedFile CreateUploadedFile(HttpPostedFile file)
        {

            PAttachedFile AttachedFile = new PAttachedFile();
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
            AttachedFile.TicketID = SDMS_ICTicket.ICTicketID;
            return AttachedFile;

        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                //   LinkButton lnkDownload = (LinkButton)sender;
                // GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;


                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);
                PAttachedFile UploadedFile = new BDMS_ICTicket().GetICTicketAttachedFile(null, AttachedFileID)[0];

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

        void fillICTicketAttachedFile()
        {
            try
            {
                List<PAttachedFile> UploadedFile = new BDMS_ICTicket().GetICTicketAttachedFile(SDMS_ICTicket.ICTicketID, null);
                if (UploadedFile.Count == 0)
                {
                    UploadedFile.Add(new PAttachedFile() { });
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
            LinkButton lblAttachedFileAdd = (LinkButton)gvAttachedFile.FooterRow.FindControl("lblAttachedFileAdd");
            //lblAttachedFileAdd.Focus();
            lblMessage.Visible = true;
            PAttachedFile AttachedFile = new PAttachedFile();
            FileUpload fu = (FileUpload)gvAttachedFile.FooterRow.FindControl("fu");

            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";

                lblMessage.ForeColor = Color.Red;
                return;
            }

            AttachedFile = CreateUploadedFile(fu.PostedFile);
            for (int i = 0; i < gvAttachedFile.Rows.Count; i++)
            {
                Label lblFileName = (Label)gvAttachedFile.Rows[i].FindControl("lblFileName");
                if (lblFileName.Text == AttachedFile.FileName)
                {
                    lblMessage.Text = "This file already available";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
            }



            if (new BDMS_ICTicket().InsertOrUpdateICTicketAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessage.Text = "File added";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "File is not added";
                lblMessage.ForeColor = Color.Red;
            }
            fillICTicketAttachedFile();
        }
        protected void lblAttachedFileRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AttachedFileID = Convert.ToInt64(gvAttachedFile.DataKeys[gvRow.RowIndex].Value);

            PAttachedFile AttachedFile = new PAttachedFile();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.IsDeleted = true;
            if (new BDMS_ICTicket().InsertOrUpdateICTicketAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
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
            LinkButton lblAttachedFileAdd = (LinkButton)gvAttachedFile.FooterRow.FindControl("lblAttachedFileAdd");
            // lblAttachedFileAdd.Focus();
        }

        [WebMethod]
        public static List<string> SearchMaterials(string input)
        {
            //  int Category1ID = (int)HttpContext.Current.Session["Category1ID"];
            int ServiceTypeID = (int)HttpContext.Current.Session["ServiceTypeID"];
            Boolean IsMainServiceMaterial = (Boolean)HttpContext.Current.Session["IsMainServiceMaterial"];
            List<string> Materials = new BDMS_Material().GetMaterialServiceAutocomplete(input, "", ServiceTypeID, null, IsMainServiceMaterial);
            return Materials.FindAll(item => item.ToLower().Contains(input.ToLower()));
        }
        [WebMethod]
        public static List<string> SearchSMaterial(string input)
        {
            List<string> Materials = new BDMS_Material().GetMaterialAutocomplete(input, "",null);
            return Materials.FindAll(item => item.ToLower().Contains(input.ToLower()));
        }
        [WebMethod]
        public static List<string> SearchMaterialNatureOfComplaint(string input)
        {
            //  int Category1ID = (int)HttpContext.Current.Session["Category1ID"];
            int ServiceTypeID = (int)HttpContext.Current.Session["ServiceTypeID"];

            List<string> Materials = new BDMS_Material().GetMaterialServiceAutocomplete(input, "", ServiceTypeID, null, false);
            return Materials.FindAll(item => item.ToLower().Contains(input.ToLower()));
        }
        protected void gvAttachedFile_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LinkButton lblAttachedFileAdd = (LinkButton)gvAttachedFile.FooterRow.FindControl("lblAttachedFileAdd");
            //  lblAttachedFileAdd.Focus();
            lblMessage.Visible = true;
            PAttachedFile AttachedFile = new PAttachedFile();
            FileUpload fu = (FileUpload)gvAttachedFile.FooterRow.FindControl("fu");

            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            AttachedFile = CreateUploadedFile(fu.PostedFile);


        }
        void ControlBaseOn60Days()
        {
            try
            {
                Button btnRequestForClaimMM = (Button)DMS_ICTicketMaterialCharges.FindControl("btnRequestForClaim");
                Button btnRequestForClaimMS = (Button)DMS_ICTicketServiceCharges.FindControl("btnRequestForClaim");
                btnRequestForClaimMM.Enabled = false;
                btnRequestForClaimMS.Enabled = false;
                if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Restored)
                {
                    btnRequestForClaimMM.Enabled = true;
                    btnRequestForClaimMS.Enabled = true;
                }

                int Days = Convert.ToInt32(ConfigurationManager.AppSettings["ICTicketLockDate"]);
                if (SDMS_ICTicket.ICTicketDate.AddDays(Days) < DateTime.Now)
                {
                    DataTable ICTicketDT = new BDMS_ICTicket().GetDeviatedICTicketReport(SDMS_ICTicket.Dealer.DealerID, SDMS_ICTicket.ICTicketNumber, 1, null, null, PSession.User.UserID);
                    if (ICTicketDT.Rows.Count != 0)
                    {
                        Boolean c = ICTicketDT.Rows[0]["Approved"] == DBNull.Value ? false : Convert.ToBoolean(ICTicketDT.Rows[0]["Approved"]);
                        if (c)
                        {
                            return;
                        }
                    }
                    btnRequestForClaimMM.Enabled = false;
                    btnRequestForClaimMS.Enabled = false;
                    //  Panel UC_BasicInformation_N = (Panel)DMS_ICTicketMaterialCharges.FindControl("UC_BasicInformation_N");

                    //  UC_BasicInformation_N.Enabled = false;
                    //  pnlFSR.Enabled = false;
                    //  pnlDMS_ICTicketServiceCharges.Enabled = false;
                    //  pnlDMS_ICTicketMaterialCharges.Enabled = false;
                    //  pnlDMS_ICTicketNote.Enabled = false;
                    //  pnlAttachments.Enabled = false;
                    //  pnlUC_TechnicianWorkInformation.Enabled = false;
                    //  pnlICTicketRestore.Enabled = false;            
                }
            }
            catch (Exception ex)
            {
            }
        }

        void fillServiceCenterAttachedFile()
        {
            try
            {
                List<PAttachedFile> UploadedFile = new BDMS_ICTicket().GetICTicketServiceCenterAttachedFile(SDMS_ICTicket.ICTicketID, null);
                if (UploadedFile.Count == 0)
                {
                    UploadedFile.Add(new PAttachedFile() { });
                }
                gvSCAttachment.DataSource = UploadedFile;
                gvSCAttachment.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        private PAttachedFile CreateServiceCenterUploadedFile(HttpPostedFile file, string FileName)
        {

            PAttachedFile AttachedFile = new PAttachedFile();
            int size = file.ContentLength;
            //string name = file.FileName;
            //int position = name.LastIndexOf("\\");
            //name = name.Substring(position + 1);
            AttachedFile.FileName = FileName + "." + file.FileName.Split('.')[file.FileName.Split('.').Count() - 1];

            AttachedFile.FileType = file.ContentType;

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.FileSize = size;
            AttachedFile.AttachedFileID = 0;
            AttachedFile.IsDeleted = false;
            AttachedFile.TicketID = SDMS_ICTicket.ICTicketID;
            return AttachedFile;

        }
        protected void lblServiceCenterAttachedFileAdd_Click(object sender, EventArgs e)
        {
            LinkButton lblServiceCenterAttachedFileAdd = (LinkButton)gvSCAttachment.FooterRow.FindControl("lblServiceCenterAttachedFileAdd");
            lblServiceCenterAttachedFileAdd.Focus();
            lblMessageServiceCenter.Visible = true;
            PAttachedFile AttachedFile = new PAttachedFile();
            FileUpload fu = (FileUpload)gvSCAttachment.FooterRow.FindControl("fu");

            DropDownList ddlSCAttachedName = (DropDownList)gvSCAttachment.FooterRow.FindControl("ddlSCAttachedName");

            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessageServiceCenter.Text = "Please select the file";

                lblMessageServiceCenter.ForeColor = Color.Red;
                return;
            }
            AttachedFile = CreateServiceCenterUploadedFile(fu.PostedFile, ddlSCAttachedName.SelectedItem.Text);

            for (int i = 0; i < gvSCAttachment.Rows.Count; i++)
            {
                Label lblFileName = (Label)gvSCAttachment.Rows[i].FindControl("lblFileName");
                if (lblFileName.Text == AttachedFile.FileName)
                {
                    lblMessageServiceCenter.Text = "This file already available";
                    lblMessageServiceCenter.ForeColor = Color.Red;
                    return;
                }
            }

            if (new BDMS_ICTicket().InsertOrUpdateICTicketServiceCenterAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessageServiceCenter.Text = "File added";
                lblMessageServiceCenter.ForeColor = Color.Green;
            }
            else
            {
                lblMessageServiceCenter.Text = "File is not added";
                lblMessageServiceCenter.ForeColor = Color.Red;
            }
            fillServiceCenterAttachedFile();
        }
        protected void lblServiceCenterAttachedFileRemove_Click(object sender, EventArgs e)
        {
            lblMessageServiceCenter.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long AttachedFileID = Convert.ToInt64(gvSCAttachment.DataKeys[gvRow.RowIndex].Value);

            PAttachedFile AttachedFile = new PAttachedFile();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.IsDeleted = true;
            if (new BDMS_ICTicket().InsertOrUpdateICTicketServiceCenterAttachedFileAddOrRemove(AttachedFile, PSession.User.UserID))
            {
                lblMessageServiceCenter.Text = "File Removed";
                lblMessageServiceCenter.ForeColor = Color.Green;
            }
            else
            {
                lblMessageServiceCenter.Text = "File is not Removed";
                lblMessageServiceCenter.ForeColor = Color.Red;
            }
            fillServiceCenterAttachedFile();
        }
        protected void lnkServiceCenterAttachedFileDownload_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                long AttachedFileID = Convert.ToInt64(gvSCAttachment.DataKeys[gvRow.RowIndex].Value);
                PAttachedFile UploadedFile = new BDMS_ICTicket().GetICTicketServiceCenterAttachedFile(null, AttachedFileID)[0];

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
    }
}