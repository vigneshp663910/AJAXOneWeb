using Business;
using Microsoft.Reporting.WebForms;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketTSIRManage : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketTSIRManage; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ICTicketTSIRManage.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicketTSIR> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketTSIRManage"] == null)
                {
                    Session["DMS_ICTicketTSIRManage"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["DMS_ICTicketTSIRManage"];
            }
            set
            {
                Session["DMS_ICTicketTSIRManage"] = value;
            }
        }
        public PDMS_ICTicketTSIR ICTicketTSIR
        {
            get
            {
                if (Session["ICTicketTSIR"] == null)
                {
                    Session["ICTicketTSIR"] = new PDMS_ICTicketTSIR();
                }
                return (PDMS_ICTicketTSIR)Session["ICTicketTSIR"];
            }
            set
            {
                Session["ICTicketTSIR"] = value;
            }
        }
        public List<PDMS_ICTicketTSIRStatus> ICTicketStatus
        {
            get
            {
                if (Session["DMS_ICTicketStatus"] == null)
                {
                    Session["DMS_ICTicketStatus"] = new List<PDMS_ICTicketTSIRStatus>();
                }
                return (List<PDMS_ICTicketTSIRStatus>)Session["DMS_ICTicketStatus"];
            }
            set
            {
                Session["DMS_ICTicketStatus"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » TSIR » IC Ticket TSIR Manage');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtTSIRDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtTSIRDateTo.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;

                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }


                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
                new BDMS_TypeOfWarranty().GetTypeOfWarrantyDDL(ddlTypeOfWarranty, null, null);
                new BDMS_Model().GetTypeOfWarrantyDDL(ddlModelID, null, null, null);
                ICTicketStatus = new BDMS_ICTicketTSIR().GetTSIRStatus(null, null);
                new BDMS_ICTicketTSIR().GetTSIRStatusDDL(ddlTsirStatus, null, null);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicket();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillICTicket()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                //     int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?) null: Convert.ToInt32( ddlDealerCode.SelectedValue);
                int? DealerCode = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());

                DateTime? TSIRDateF = string.IsNullOrEmpty(txtTSIRDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateFrom.Text.Trim());
                DateTime? TSIRDateT = string.IsNullOrEmpty(txtTSIRDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateTo.Text.Trim());
                //  int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                int? TechnicianID = null;
                List<PDMS_ICTicketTSIR> SOIs = null;
                if (PSession.User.IsTechnician)
                {
                    TechnicianID = PSession.User.UserID;
                }

                int? TypeOfWarrantyID = ddlTypeOfWarranty.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTypeOfWarranty.SelectedValue);
                int? ModelID = ddlModelID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModelID.SelectedValue);
                int? TsirStatusID = ddlTsirStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTsirStatus.SelectedValue);
                SOIs = new BDMS_ICTicketTSIR().GetICTicketTSIR(null, DealerCode, txtCustomerCode.Text.Trim(), txtTSIRNo.Text.Trim(), TSIRDateF, TSIRDateT
                    , txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, txtSroCode.Text.Trim(), TechnicianID, TypeOfWarrantyID, ModelID, txtMachineSerialNumber.Text.Trim(), TsirStatusID);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName select new { S }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }

                List<PDMS_Division> Division = new BDMS_ICTicketTSIR().GetICTicketTSIRUserDivisionList(PSession.User.UserID);
                if (Division.Count != 0)
                {
                    var SOIs2 = (from S in SOIs join D in Division on S.ICTicket.Equipment.EquipmentModel.Division.DivisionID equals D.DivisionID select new { S }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs2)
                    {
                        SOIs.Add(w.S);
                    }
                }

                ICTicket = SOIs;

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
                if (SOIs.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
                }

                //string[] HOComments1 = ConfigurationManager.AppSettings["HOComments1"].Split(',');
                //string[] HOComments2 = ConfigurationManager.AppSettings["HOComments2"].Split(',');
                //string[] TSIRApprove = ConfigurationManager.AppSettings["TSIRApprove"].Split(',');
                //if (HOComments1.Contains(PSession.User.UserID.ToString()))
                //{ 
                //    for (int i = 0; i < gvICTickets.Rows.Count; i++)
                //    {
                //        Label lblQualityComments = (Label)gvICTickets.Rows[i].FindControl("lblQualityComments");
                //        TextBox txtQualityComments = (TextBox)gvICTickets.Rows[i].FindControl("txtQualityComments");
                //        Button btnQualityCommentsSave = (Button)gvICTickets.Rows[i].FindControl("btnQualityCommentsSave");
                //        lblQualityComments.Visible = false;
                //        txtQualityComments.Visible = true;
                //        btnQualityCommentsSave.Visible = true;
                //    }
                //}
                //if (HOComments2.Contains(PSession.User.UserID.ToString()))
                //{
                //    for (int i = 0; i < gvICTickets.Rows.Count; i++)
                //    {
                //        Label lblServiceComments = (Label)gvICTickets.Rows[i].FindControl("lblServiceComments");
                //        TextBox txtServiceComments = (TextBox)gvICTickets.Rows[i].FindControl("txtServiceComments");
                //        Button btnServiceCommentsSave = (Button)gvICTickets.Rows[i].FindControl("btnServiceCommentsSave");
                //        lblServiceComments.Visible = false;
                //        txtServiceComments.Visible = true;
                //        btnServiceCommentsSave.Visible = true;
                //    }
                //}

                //if (TSIRApprove.Contains(PSession.User.UserID.ToString()))
                //{
                //    gvICTickets.Columns[18].Visible = true;
                //}
                //else
                //{
                //    gvICTickets.Columns[18].Visible = false;
                //}

                //if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                //{
                //    gvICTickets.Columns[16].Visible = false;
                //}


                //string[] MailToSupplier = ConfigurationManager.AppSettings["MailToSupplier"].Split(',');
                //if (MailToSupplier.Contains(PSession.User.UserID.ToString()))
                //{
                //    gvICTickets.Columns[19].Visible = true;
                //}
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRManage", "fillClaim", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSIR");
            dt.Columns.Add("TSIR Date");
            dt.Columns.Add("FSR");
            dt.Columns.Add("FSR Date");

            dt.Columns.Add("Commissioning Date");
            dt.Columns.Add("M/C Dispatch Date");
            dt.Columns.Add("Type Of Warranty");

            dt.Columns.Add("Nature of Failure");
            dt.Columns.Add("Failure Details");
            dt.Columns.Add("Points checked");
            dt.Columns.Add("Possible Root Causes / Specific Points Noticed");

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");

            dt.Columns.Add("HMR");
            dt.Columns.Add("Application");
            dt.Columns.Add("Machine Serial Number");
            dt.Columns.Add("Machine Model");

            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("Location");
            dt.Columns.Add("TSIR Status");
            foreach (PDMS_ICTicketTSIR IC in ICTicket)
            {
                dt.Rows.Add(
                      IC.TsirNumber
                      , IC.TsirDate.ToShortDateString()
                      , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
                     , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
                    , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                     , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                     , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
                     , IC.NatureOfFailures
                     , IC.FailureDetails
                     , IC.PointsChecked
                     , IC.PossibleRootCauses
                    , IC.ICTicket.ICTicketNumber
                    , IC.ICTicket.ICTicketDate.ToShortDateString()
                   , IC.ICTicket.Customer.CustomerCode
                    , IC.ICTicket.Customer.CustomerName
                    , IC.ICTicket.Dealer.DealerCode
                    , IC.ICTicket.Dealer.DealerName
                     , IC.ICTicket.CurrentHMRValue
                     , IC.ICTicket.MainApplication.MainApplication
                    , IC.ICTicket.Equipment.EquipmentSerialNo
                    , IC.ICTicket.Equipment.EquipmentModel.Model
                   , IC.ICTicket.Address.State.State
                    , IC.ICTicket.Address.District.District
                    , IC.ICTicket.Location
                    , IC.Status.Status
                    );
            }
            new BXcel().ExporttoExcel(dt, "TSIR Details");
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        //protected void lbICTicket_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

        //    int index = gvRow.RowIndex;
        //    string url = "DMS_ICTicketView.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
        //    Response.Redirect(url);
        //}
        //protected void btnMaterialCharge_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < gvICTickets.Rows.Count; i++)
        //    {
        //        RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
        //        if (rbICTicketID.Checked)
        //        {
        //            string url = "DMS_ICTicketMaterialCharges.aspx?TicketID=" + gvICTickets.DataKeys[i].Value.ToString();
        //            Response.Redirect(url);
        //        }
        //    }
        //}
        //protected void lbEdit_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

        //    int index = gvRow.RowIndex;
        //    string url = "DMS_ICTicketTSIRCreate.aspx?TsirIDEdit=" + gvICTickets.DataKeys[index].Value.ToString();
        //    Response.Redirect(url);
        //}
        //protected void lbView_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    int index = gvRow.RowIndex;
        //    string url = "DMS_ICTicketTSIRCreate.aspx?TsirIDView=" + gvICTickets.DataKeys[index].Value.ToString();
        //    Response.Redirect(url);
        //}
        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null);
                PrintFSR2(TSIR);
                return;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
         
        void PrintFSR2(PDMS_ICTicketTSIR TSIR)
        {
            try
            {
                string FailureCode = "";
                PDMS_ICTicket ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(TSIR.ICTicket.ICTicketID);
                PDMS_ICTicketFSR FSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, TSIR.ICTicket.ICTicketID, null, null, null, null, null, null);

                List<PDMS_WarrantyInvoiceHeader> ClaimList = new BDMS_WarrantyClaim().GetWarrantyClaimReport(TSIR.ICTicket.ICTicketNumber, null, null, "", null, null, "", null, null, null, "", "", "", false, null);
                string TL_ContactDetails = "";
                string SM_ContactDetails = "";
                if (ClaimList.Count != 0)
                {
                    TL_ContactDetails = ClaimList[0].Approved1By == null ? "" : ClaimList[0].Approved1By.ContactName + "  " + ClaimList[0].Approved1By.ContactNumber;
                    SM_ContactDetails = ClaimList[0].Approved2By == null ? "" : ClaimList[0].Approved2By.ContactName + "  " + ClaimList[0].Approved2By.ContactNumber;
                }
                if (string.IsNullOrEmpty(TL_ContactDetails))
                {
                    TL_ContactDetails = ICTicket.Dealer.TL == null ? "" : ICTicket.Dealer.TL.ContactName + "  " + ICTicket.Dealer.TL.ContactNumber;
                    SM_ContactDetails = ICTicket.Dealer.SM == null ? "" : ICTicket.Dealer.SM.ContactName + "  " + ICTicket.Dealer.SM.ContactNumber;
                }
                List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(TSIR.ICTicket.ICTicketID, null, "", false);
                foreach (PDMS_ServiceCharge SC in Charge)
                {
                    List<string> Materials = new BDMS_Material().GetMaterialServiceAutocomplete(SC.Material.MaterialCode, "", TSIR.ICTicket.ServiceType.ServiceTypeID, null, true);
                    if (Materials.Count() == 1)
                    {
                        FailureCode = SC.Material.MaterialCode;
                    }
                }
                PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(TSIR.ICTicket.Customer.CustomerCode);
                string CustomerAddress = Customer.Address1 + ", " + Customer.Address1 + ", " + Customer.Address3 + ", " + Customer.City + ", " + Customer.State.State + " - " + Customer.Pincode;
                CustomerAddress = CustomerAddress.Replace(", ,", ",").Replace(",,", ",");
                CustomerAddress = CustomerAddress.Trim(',', ' ');

                DataTable MaterialDT = new DataTable();
                MaterialDT.Columns.Add("Material");
                MaterialDT.Columns.Add("Description");
                MaterialDT.Columns.Add("HSN");
                MaterialDT.Columns.Add("Qty");

                DataTable FMaterialDT = new DataTable();
                FMaterialDT.Columns.Add("Material");
                FMaterialDT.Columns.Add("Description");
                FMaterialDT.Columns.Add("HSN");

                List<PDMS_ServiceMaterial> MaterialC = new BDMS_Service().GetServiceMaterials(TSIR.ICTicket.ICTicketID, null, TSIR.TsirID, "", false, "");
                foreach (PDMS_ServiceMaterial Mat in MaterialC)
                {
                    MaterialDT.Rows.Add(Mat.Material.MaterialCode, Mat.Material.MaterialDescription, Mat.Material.MaterialSerialNumber, Mat.Qty);
                    if (Mat.DefectiveMaterial != null)
                        FMaterialDT.Rows.Add(Mat.DefectiveMaterial.MaterialCode, Mat.DefectiveMaterial.MaterialDescription, Mat.DefectiveMaterial.MaterialSerialNumber);
                }


                List<string> FileNames = new List<string>();
                List<string> FiePath = new List<string>();

                DataTable FsrFiles = new DataTable();
                FsrFiles.Columns.Add("FileName1");
                FsrFiles.Columns.Add("FiePath1");
                FsrFiles.Columns.Add("FileName2");
                FsrFiles.Columns.Add("FiePath2");

                string Path1 = "";
                List<PDMS_FSRAttachedFile> FSRFile = new List<PDMS_FSRAttachedFile>();
                List<PDMS_FSRAttachedFile> FSRFileAll = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(TSIR.ICTicket.ICTicketID, null);
                for (int i = 0; i < FSRFileAll.Count(); i++)
                {
                    int FileNameID = FSRFileAll[i].FSRAttachedName.FSRAttachedFileNameID;
                    if ((FileNameID == (short)FSRAttachedFileName.Technician) || (FileNameID == (short)FSRAttachedFileName.Customer)
                         || (FileNameID == (short)FSRAttachedFileName.TechnicianSignature) || (FileNameID == (short)FSRAttachedFileName.CustomerSignature))
                    {
                    }
                    else
                    {
                        FSRFile.Add(FSRFileAll[i]);
                    }
                }
                for (int i = 0; i < FSRFile.Count(); i++)
                {
                    PAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(FSRFile[i].AttachedFileID);
                    string Url1 = "ICTickrtFSR_Files/Org/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

                    if (File.Exists(MapPath(Url1)))
                    {
                        File.Delete(MapPath(Url1));
                    }
                    File.WriteAllBytes(MapPath(Url1), F1.AttachedFile);
                    string DestPath = "ICTickrtFSR_Files/Org/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

                    resizeImage2(MapPath(Url1), Server.MapPath("~/" + DestPath));
                    Path1 = new Uri(Server.MapPath("~/" + DestPath)).AbsoluteUri;
                    // FsrFiles.Rows.Add(F1.FSRAttachedName.FSRAttachedName, Path1);

                    FileNames.Add(F1.FileName);
                    FiePath.Add(Path1);
                }

                List<PDMS_TSIRAttachedFile> TSIRFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(TSIR.ICTicket.ICTicketID, TSIR.TsirID, null);
                for (int i = 0; i < TSIRFile.Count(); i++)
                {
                    PAttachedFile T1 = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileForDownload(TSIRFile[i].AttachedFileID);
                    string Url1 = "ICTickrtTSIR_Files/Org/" + T1.AttachedFileID + "." + T1.FileName.Split('.')[T1.FileName.Split('.').Count() - 1];
                    if (File.Exists(MapPath(Url1)))
                    {
                        File.Delete(MapPath(Url1));
                    }
                    
                    File.WriteAllBytes(MapPath(Url1), T1.AttachedFile);
                    string DestPath = "ICTickrtTSIR_Files/Org/" + T1.AttachedFileID + "." + T1.FileName.Split('.')[T1.FileName.Split('.').Count() - 1];

                    resizeImage2(MapPath(Url1), Server.MapPath("~/" + DestPath));
                    Path1 = new Uri(Server.MapPath("~/" + DestPath)).AbsoluteUri;
                    //  FsrFiles.Rows.Add(T1.FSRAttachedName.FSRAttachedName, Path1);

                    FileNames.Add(T1.FileName);
                    FiePath.Add(Path1);
                }

                for (int i = 0; i < FileNames.Count; i++)
                {
                    if (i + 1 != FileNames.Count())
                    {
                        FsrFiles.Rows.Add(FileNames[i], FiePath[i], FileNames[i + 1], FiePath[i + 1]);
                        i = i + 1;
                    }
                    else
                    {
                        FsrFiles.Rows.Add(FileNames[i], FiePath[i], "", "");
                    }
                }

                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "TSIR_" + TSIR.TsirNumber + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;
                ReportParameter[] P = new ReportParameter[37];

                P[0] = new ReportParameter("TSIRNumber", TSIR.TsirNumber, false);
                P[1] = new ReportParameter("TSIRDate", TSIR.TsirDate.ToShortDateString(), false);
                P[2] = new ReportParameter("ICTicketNo", TSIR.ICTicket.ICTicketNumber, false);
                P[3] = new ReportParameter("ICTicketDate", TSIR.ICTicket.ICTicketDate.ToShortDateString(), false);
                P[4] = new ReportParameter("FSRNo", ICTicket.ICTicketNumber + "/" + ICTicket.Dealer.DealerCode + "/" + ICTicket.Technician.UserName + "/" + FSR.FSRDate.Year, false);
                P[5] = new ReportParameter("Application", ICTicket.MainApplication == null ? "" : TSIR.ICTicket.MainApplication.MainApplication, false);
                P[6] = new ReportParameter("EquipmentModel", ICTicket.Equipment.EquipmentModel.Model, false);
                P[7] = new ReportParameter("EquipmentSerialNo", ICTicket.Equipment.EquipmentSerialNo, false);

                P[8] = new ReportParameter("DealerCode", ICTicket.Dealer.DealerCode, false);
                P[9] = new ReportParameter("DealerName", ICTicket.Dealer.DealerName, false);
                P[10] = new ReportParameter("HMR", ICTicket.CurrentHMRValue == null ? "" : Convert.ToString(ICTicket.CurrentHMRValue) + " (" + ICTicket.Equipment.EquipmentModel.Division.UOM + ")", false);
                P[11] = new ReportParameter("TypeOfWarranty", ICTicket.TypeOfWarranty == null ? "" : ICTicket.TypeOfWarranty.TypeOfWarranty, false);
                P[12] = new ReportParameter("FSRDate", FSR.FSRDate.ToShortDateString(), false);
                P[13] = new ReportParameter("CustomerName", Customer.CustomerName, false);
                P[14] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[15] = new ReportParameter("Location", ICTicket.Location, false);
                P[16] = new ReportParameter("CustomerGSTStateCode", "GST State Code : " + Customer.State.StateCode, false);
                P[17] = new ReportParameter("CustomerGSTIN", "GSTIN/UIN No : " + Customer.GSTIN, false);
                P[18] = new ReportParameter("CustomerAddress", CustomerAddress, false);

                P[19] = new ReportParameter("NatureOfFailures", TSIR.NatureOfFailures, false);
                P[20] = new ReportParameter("ProblemNoticedBy", TSIR.ProblemNoticedBy, false);
                P[21] = new ReportParameter("UnderWhatConditionFailureTaken", TSIR.UnderWhatConditionFailureTaken, false);
                P[22] = new ReportParameter("FailureDetails", TSIR.FailureDetails, false);
                P[23] = new ReportParameter("PointsChecked", TSIR.PointsChecked, false);
                P[24] = new ReportParameter("PossibleRootCauses", TSIR.PossibleRootCauses, false);
                P[25] = new ReportParameter("SpecificPointsNoticed", TSIR.SpecificPointsNoticed, false);


                P[26] = new ReportParameter("ProblemCategory", ICTicket.ServicePriority.ServicePriority, false);
                P[27] = new ReportParameter("CommissioningOn", ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)ICTicket.Equipment.CommissioningOn).ToShortDateString(), false);
                P[28] = new ReportParameter("DispatchedOn", ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)ICTicket.Equipment.DispatchedOn).ToShortDateString(), false);
                P[29] = new ReportParameter("HOComments", TSIR.ServiceComments + " " + TSIR.ServiceComments, false);
                P[30] = new ReportParameter("FailureCode", FailureCode, false);
                P[31] = new ReportParameter("FsrfilesDisplay", FsrFiles.Rows.Count.ToString(), false);

                P[32] = new ReportParameter("SE_Name", ICTicket.Technician.ContactName, false);
                P[33] = new ReportParameter("SE_ContactNumber", ICTicket.Technician.ContactNumber, false);



                P[34] = new ReportParameter("TL_ContactDetails", TL_ContactDetails, false);
                P[35] = new ReportParameter("SM_ContactDetails", SM_ContactDetails, false);
                P[36] = new ReportParameter("PartsInvoiceNumber", TSIR.PartsInvoiceNumber, false);
                report.ReportPath = Server.MapPath("~/Print/DMS_TSIR2.rdlc");
                report.SetParameters(P);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "Fsrfiles";//This refers to the dataset name in the RDLC file  
                rds.Value = FsrFiles;
                report.DataSources.Add(rds);


                ReportDataSource rds2 = new ReportDataSource();
                rds2.Name = "Material";//This refers to the dataset name in the RDLC file  
                rds2.Value = MaterialDT;
                report.DataSources.Add(rds2);

                ReportDataSource rds3 = new ReportDataSource();
                rds3.Name = "FMaterial";//This refers to the dataset name in the RDLC file  
                rds3.Value = FMaterialDT;
                report.DataSources.Add(rds3);

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                new BXcel().PdfDowload();
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        public void resizeImage1(string SouPath, string DestPath)
        {
            System.Drawing.Image imgToResize = System.Drawing.Image.FromFile(SouPath);
            try
            {
                if (File.Exists(DestPath))
                {
                    File.Delete(DestPath);
                }
                int OSizeW = imgToResize.Size.Width;
                int OSizeH = imgToResize.Size.Height;
                int DSize = 0;
                decimal DP = 0;
                if (OSizeW > 1000)
                {
                    DSize = OSizeW - 1000;
                    DP = OSizeW / Convert.ToDecimal(DSize);
                    OSizeW = 1000;
                    OSizeH = Convert.ToInt32(OSizeH - (OSizeH / DP));
                }
                if (OSizeH > 1000)
                {
                    DSize = OSizeH - 1000;
                    DP = OSizeH / Convert.ToDecimal(DSize);

                    OSizeW = Convert.ToInt32(OSizeW - (OSizeW / DP));
                    OSizeH = 1000;
                }
                //((System.Drawing.Image)(new Bitmap(imgToResize, new Size(imgToResize.Size.Width, imgToResize.Size.Height)))).Save(DestPath);
                ((System.Drawing.Image)(new Bitmap(imgToResize, new Size(OSizeW, OSizeH)))).Save(DestPath);
            }
            catch (Exception e1)
            { }
            finally
            {
                imgToResize.Dispose();
            }
        }
        public void resizeImage2(string SouPath, string DestPath)
        {
            System.Drawing.Image imgToResize = System.Drawing.Image.FromFile(SouPath);
            try
            {
                if (File.Exists(DestPath))
                {
                    File.Delete(DestPath);
                }

                int OSizeW = imgToResize.Size.Width;
                int OSizeH = imgToResize.Size.Height;
                int DSize = 0;
                decimal DP = 0;
                if (OSizeW > 250)
                {
                    DSize = OSizeW - 250;
                    DP = OSizeW / Convert.ToDecimal(DSize);
                    OSizeW = 250;
                    OSizeH = Convert.ToInt32(OSizeH - (OSizeH / DP));
                }
                if (OSizeH > 250)
                {
                    DSize = OSizeH - 250;
                    DP = OSizeH / Convert.ToDecimal(DSize);

                    OSizeW = Convert.ToInt32(OSizeW - (OSizeW / DP));
                    OSizeH = 250;
                }

                //((System.Drawing.Image)(new Bitmap(imgToResize, new Size(imgToResize.Size.Width, imgToResize.Size.Height)))).Save(DestPath);
                ((System.Drawing.Image)(new Bitmap(imgToResize, new Size(OSizeW, OSizeH)))).Save(DestPath);

            }
            catch (Exception e1)
            { }
            finally
            {
                imgToResize.Dispose();
            }

        }

        //protected void btnQualityCommentsSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        int index = gvRow.RowIndex;
        //        string QualityComments = ((TextBox)gvICTickets.Rows[index].FindControl("txtQualityComments")).Text;
        //        if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRComments(Convert.ToInt64(gvICTickets.DataKeys[index].Value), QualityComments, 1, PSession.User.UserID))
        //        {
        //            lblMessage.Text = "Updated Successfully";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //        }
        //        else
        //        {
        //            lblMessage.Text = "Updated is not Successfully";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Please Contact Administrator. " + ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}
        //protected void btnServiceCommentsSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        int index = gvRow.RowIndex;
        //        string ServiceComments = ((TextBox)gvICTickets.Rows[index].FindControl("txtServiceComments")).Text;
        //        if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRComments(Convert.ToInt64(gvICTickets.DataKeys[index].Value), ServiceComments, 2, PSession.User.UserID))
        //        {
        //            lblMessage.Text = "Updated Successfully";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //        }
        //        else
        //        {
        //            lblMessage.Text = "Updated is not Successfully";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Please Contact Administrator. " + ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}
        //protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
        //            if (lblStatusID.Text == "1")
        //            {
        //                TextBox txtStatusRemarks = (TextBox)e.Row.FindControl("txtStatusRemarks");
        //                Button btnApprove = (Button)e.Row.FindControl("btnApprove");
        //                Button btnReject = (Button)e.Row.FindControl("btnReject");
        //                txtStatusRemarks.Visible = true;
        //                btnApprove.Visible = true;
        //                btnReject.Visible = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new FileLogger().LogMessage("DMS_ICTicketTSIRManage", "gvICTickets_RowDataBound", ex);
        //        throw ex;
        //    }
        //}

        //protected void btnApprove_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        int index = gvRow.RowIndex;
        //        string StatusRemarks = ((TextBox)gvICTickets.Rows[index].FindControl("txtStatusRemarks")).Text;
        //        if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRApproveOrReject(Convert.ToInt64(gvICTickets.DataKeys[index].Value), StatusRemarks, 2, PSession.User.UserID))
        //        {
        //            lblMessage.Text = "Updated Successfully";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //        }
        //        else
        //        {
        //            lblMessage.Text = "Updated is not Successfully";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Please Contact Administrator. " + ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}
        //protected void btnReject_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        int index = gvRow.RowIndex;
        //        string StatusRemarks = ((TextBox)gvICTickets.Rows[index].FindControl("txtStatusRemarks")).Text;
        //        if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRApproveOrReject(Convert.ToInt64(gvICTickets.DataKeys[index].Value), StatusRemarks, 3, PSession.User.UserID))
        //        {
        //            lblMessage.Text = "Updated Successfully";
        //            lblMessage.ForeColor = Color.Green;
        //            lblMessage.Visible = true;
        //        }
        //        else
        //        {
        //            lblMessage.Text = "Updated is not Successfully";
        //            lblMessage.ForeColor = Color.Red;
        //            lblMessage.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Please Contact Administrator. " + ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}

        //protected void btnSendMail_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //        int index = gvRow.RowIndex;
        //        TextBox txtCustomerEmailID = (TextBox)gvICTickets.Rows[index].FindControl("txtCustomerEmailID");
        //        PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null);
        //        Byte[] MyByte = SendPDFTSIR(TSIR);
        //        string[] MailIDs = txtCustomerEmailID.Text.Trim().Split(',', ';', ':');
        //        string Subject = "TSIR " + TSIR.TsirNumber + " - " + TSIR.ServiceCharge.Material.MaterialCode + " - " + TSIR.ICTicket.CurrentHMRValue;

        //        string messageBody = MailFormate.MailTsir;
        //        messageBody = messageBody.Replace("@@Name", PSession.User.ContactName);
        //        messageBody = messageBody.Replace("@@Designation", PSession.User.UsersDesignation.UsersDesignation);
        //        messageBody = messageBody.Replace("@@Phone", PSession.User.ContactNumber);
        //        messageBody = messageBody.Replace("@@MailID", PSession.User.Mail);
        //        foreach (string MailID in MailIDs)
        //        {
        //            Boolean Success = new EmailManager().MailSendTSIR(MailID, Subject, messageBody, MyByte, "TSIR - " + TSIR.TsirNumber + ".PDF");
        //            new BDMS_ICTicketTSIR().InsertICTicketTSIRMailToVendor(TSIR.TsirID, MailID, PSession.User.UserID, Success);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Please Contact Administrator. " + ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //}
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = ICTicket;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;

        }
        
        //private Byte[] SendPDFTSIR(PDMS_ICTicketTSIR TSIR)
        //{
        //    try
        //    {
        //        string FailureCode = "";
        //        PDMS_ICTicket ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(TSIR.ICTicket.ICTicketID);
        //        PDMS_ICTicketFSR FSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(null, TSIR.ICTicket.ICTicketID, null, null, null, null, null, null);

        //        List<PDMS_WarrantyInvoiceHeader> ClaimList = new BDMS_WarrantyClaim().GetWarrantyClaimReport(TSIR.ICTicket.ICTicketNumber, null, null, "", null, null, "", null, null, null, "", "", "", false, null);
        //        string TL_ContactDetails = "";
        //        string SM_ContactDetails = "";
        //        if (ClaimList.Count != 0)
        //        {
        //            TL_ContactDetails = ClaimList[0].Approved1By == null ? "" : ClaimList[0].Approved1By.ContactName + "  " + ClaimList[0].Approved1By.ContactNumber;
        //            SM_ContactDetails = ClaimList[0].Approved2By == null ? "" : ClaimList[0].Approved2By.ContactName + "  " + ClaimList[0].Approved2By.ContactNumber;
        //        }
        //        if (string.IsNullOrEmpty(TL_ContactDetails))
        //        {
        //            TL_ContactDetails = ICTicket.Dealer.TL == null ? "" : ICTicket.Dealer.TL.ContactName + "  " + ICTicket.Dealer.TL.ContactNumber;
        //            SM_ContactDetails = ICTicket.Dealer.SM == null ? "" : ICTicket.Dealer.SM.ContactName + "  " + ICTicket.Dealer.SM.ContactNumber;
        //        }
        //        List<PDMS_ServiceCharge> Charge = new BDMS_Service().GetServiceCharges(TSIR.ICTicket.ICTicketID, null, "", false);
        //        foreach (PDMS_ServiceCharge SC in Charge)
        //        {
        //            List<string> Materials = new BDMS_Material().GetMaterialServiceAutocomplete(SC.Material.MaterialCode, "", TSIR.ICTicket.ServiceType.ServiceTypeID, null, true);
        //            if (Materials.Count() == 1)
        //            {
        //                FailureCode = SC.Material.MaterialCode;
        //            }
        //        }
        //        PDMS_Customer Customer = new SCustomer().getCustomerAddress(TSIR.ICTicket.Customer.CustomerCode);
        //        string CustomerAddress = Customer.Address1 + ", " + Customer.Address1 + ", " + Customer.Address3 + ", " + Customer.City + ", " + Customer.State.State + " - " + Customer.Pincode;
        //        CustomerAddress = CustomerAddress.Replace(", ,", ",").Replace(",,", ",");
        //        CustomerAddress = CustomerAddress.Trim(',', ' ');

        //        DataTable MaterialDT = new DataTable();
        //        MaterialDT.Columns.Add("Material");
        //        MaterialDT.Columns.Add("Description");
        //        MaterialDT.Columns.Add("HSN");
        //        MaterialDT.Columns.Add("Qty");

        //        DataTable FMaterialDT = new DataTable();
        //        FMaterialDT.Columns.Add("Material");
        //        FMaterialDT.Columns.Add("Description");
        //        FMaterialDT.Columns.Add("HSN");

        //        List<PDMS_ServiceMaterial> MaterialC = new BDMS_Service().GetServiceMaterials(TSIR.ICTicket.ICTicketID, null, TSIR.TsirID, "", false, "");
        //        foreach (PDMS_ServiceMaterial Mat in MaterialC)
        //        {
        //            MaterialDT.Rows.Add(Mat.Material.MaterialCode, Mat.Material.MaterialDescription, Mat.Material.MaterialSerialNumber, Mat.Qty);
        //            if (Mat.DefectiveMaterial != null)
        //                FMaterialDT.Rows.Add(Mat.DefectiveMaterial.MaterialCode, Mat.DefectiveMaterial.MaterialDescription, Mat.DefectiveMaterial.MaterialSerialNumber);
        //        }

        //        List<string> FileNames = new List<string>();
        //        List<string> FiePath = new List<string>();

        //        DataTable FsrFiles = new DataTable();
        //        FsrFiles.Columns.Add("FileName1");
        //        FsrFiles.Columns.Add("FiePath1");
        //        FsrFiles.Columns.Add("FileName2");
        //        FsrFiles.Columns.Add("FiePath2");

        //        string Path1 = "";
        //        List<PDMS_FSRAttachedFile> FSRFile = new List<PDMS_FSRAttachedFile>();
        //        List<PDMS_FSRAttachedFile> FSRFileAll = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(TSIR.ICTicket.ICTicketID, null);
        //        for (int i = 0; i < FSRFileAll.Count(); i++)
        //        {
        //            int FileNameID = FSRFileAll[i].FSRAttachedName.FSRAttachedFileNameID;
        //            if ((FileNameID == (short)FSRAttachedFileName.Technician) || (FileNameID == (short)FSRAttachedFileName.Customer)
        //                 || (FileNameID == (short)FSRAttachedFileName.TechnicianSignature) || (FileNameID == (short)FSRAttachedFileName.CustomerSignature))
        //            {
        //            }
        //            else
        //            {
        //                FSRFile.Add(FSRFileAll[i]);
        //            }
        //        }
        //        for (int i = 0; i < FSRFile.Count(); i++)
        //        {
        //            PDMS_FSRAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(FSRFile[i].AttachedFileID);
        //            string Url1 = "ICTickrtFSR_Files/Org/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

        //            if (File.Exists(MapPath(Url1)))
        //            {
        //                File.Delete(MapPath(Url1));
        //            }
        //            File.WriteAllBytes(MapPath(Url1), F1.AttachedFile);


        //            string DestPath = "ICTickrtFSR_Files/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

        //            resizeImage2(MapPath(Url1), Server.MapPath("~/" + DestPath));
        //            Path1 = new Uri(Server.MapPath("~/" + DestPath)).AbsoluteUri;
        //            //  FsrFiles.Rows.Add(F1.FSRAttachedName.FSRAttachedName, Path1);
        //            FileNames.Add(F1.FSRAttachedName.FSRAttachedName);
        //            FiePath.Add(Path1);
        //        }

        //        List<PDMS_TSIRAttachedFile> TSIRFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(TSIR.ICTicket.ICTicketID, TSIR.TsirID, null);
        //        for (int i = 0; i < TSIRFile.Count(); i++)
        //        {
        //            PDMS_TSIRAttachedFile T1 = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileByID(TSIRFile[i].AttachedFileID);
        //            string Url1 = "ICTickrtTSIR_Files/Org/" + T1.AttachedFileID + "." + T1.FileName.Split('.')[T1.FileName.Split('.').Count() - 1];
        //            if (File.Exists(MapPath(Url1)))
        //            {
        //                File.Delete(MapPath(Url1));
        //            }
        //            File.WriteAllBytes(MapPath(Url1), T1.AttachedFile);
        //            string DestPath = "ICTickrtTSIR_Files/" + T1.AttachedFileID + "." + T1.FileName.Split('.')[T1.FileName.Split('.').Count() - 1];

        //            resizeImage2(MapPath(Url1), Server.MapPath("~/" + DestPath));
        //            Path1 = new Uri(Server.MapPath("~/" + DestPath)).AbsoluteUri;

        //            //FsrFiles.Rows.Add(T1.FSRAttachedName.FSRAttachedName, Path1);
        //            FileNames.Add(T1.FSRAttachedName.FSRAttachedName);
        //            FiePath.Add(Path1);
        //        }


        //        for (int i = 0; i < FileNames.Count; i++)
        //        {
        //            if (i + 1 != FileNames.Count())
        //            {
        //                FsrFiles.Rows.Add(FileNames[i], FiePath[i], FileNames[i + 1], FiePath[i + 1]);
        //                i = i + 1;
        //            }
        //            else
        //            {
        //                FsrFiles.Rows.Add(FileNames[i], FiePath[i], "", "");
        //            }
        //        }

        //        string contentType = string.Empty;
        //        contentType = "application/pdf";
        //        var CC = CultureInfo.CurrentCulture;
        //        string FileName = "TSIR_" + TSIR.TsirNumber + ".pdf";
        //        string extension;
        //        string encoding;
        //        string mimeType;
        //        string[] streams;
        //        Warning[] warnings;
        //        LocalReport report = new LocalReport();
        //        report.EnableExternalImages = true;
        //        ReportParameter[] P = new ReportParameter[37];
        //        P[0] = new ReportParameter("TSIRNumber", TSIR.TsirNumber, false);
        //        P[1] = new ReportParameter("TSIRDate", TSIR.TsirDate.ToShortDateString(), false);
        //        P[2] = new ReportParameter("ICTicketNo", TSIR.ICTicket.ICTicketNumber, false);
        //        P[3] = new ReportParameter("ICTicketDate", TSIR.ICTicket.ICTicketDate.ToShortDateString(), false);
        //        P[4] = new ReportParameter("FSRNo", ICTicket.ICTicketNumber + "/" + ICTicket.Dealer.DealerCode + "/" + ICTicket.Technician.UserName + "/" + FSR.FSRDate.Year, false);
        //        P[5] = new ReportParameter("Application", ICTicket.MainApplication == null ? "" : TSIR.ICTicket.MainApplication.MainApplication, false);
        //        P[6] = new ReportParameter("EquipmentModel", ICTicket.Equipment.EquipmentModel.Model, false);
        //        P[7] = new ReportParameter("EquipmentSerialNo", ICTicket.Equipment.EquipmentSerialNo, false);
        //        P[8] = new ReportParameter("DealerCode", ICTicket.Dealer.DealerCode, false);
        //        P[9] = new ReportParameter("DealerName", ICTicket.Dealer.DealerName, false);
        //        P[10] = new ReportParameter("HMR", ICTicket.CurrentHMRValue == null ? "" : Convert.ToString(ICTicket.CurrentHMRValue) + " (" + ICTicket.Equipment.EquipmentModel.Division.UOM + ")", false);
        //        P[11] = new ReportParameter("TypeOfWarranty", ICTicket.TypeOfWarranty == null ? "" : ICTicket.TypeOfWarranty.TypeOfWarranty, false);
        //        P[12] = new ReportParameter("FSRDate", FSR.FSRDate.ToShortDateString(), false);
        //        P[13] = new ReportParameter("CustomerName", Customer.CustomerName, false);
        //        P[14] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
        //        P[15] = new ReportParameter("Location", ICTicket.Location, false);
        //        P[16] = new ReportParameter("CustomerGSTStateCode", "GST State Code : " + Customer.State.StateCode, false);
        //        P[17] = new ReportParameter("CustomerGSTIN", "GSTIN/UIN No : " + Customer.GSTIN, false);
        //        P[18] = new ReportParameter("CustomerAddress", CustomerAddress, false);
        //        P[19] = new ReportParameter("NatureOfFailures", TSIR.NatureOfFailures, false);
        //        P[20] = new ReportParameter("ProblemNoticedBy", TSIR.ProblemNoticedBy, false);
        //        P[21] = new ReportParameter("UnderWhatConditionFailureTaken", TSIR.UnderWhatConditionFailureTaken, false);
        //        P[22] = new ReportParameter("FailureDetails", TSIR.FailureDetails, false);
        //        P[23] = new ReportParameter("PointsChecked", TSIR.PointsChecked, false);
        //        P[24] = new ReportParameter("PossibleRootCauses", TSIR.PossibleRootCauses, false);
        //        P[25] = new ReportParameter("SpecificPointsNoticed", TSIR.SpecificPointsNoticed, false);
        //        P[26] = new ReportParameter("ProblemCategory", ICTicket.ServicePriority.ServicePriority, false);
        //        P[27] = new ReportParameter("CommissioningOn", ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)ICTicket.Equipment.CommissioningOn).ToShortDateString(), false);
        //        P[28] = new ReportParameter("DispatchedOn", ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)ICTicket.Equipment.DispatchedOn).ToShortDateString(), false);
        //        P[29] = new ReportParameter("HOComments", TSIR.ServiceComments + " " + TSIR.ServiceComments, false);
        //        P[30] = new ReportParameter("FailureCode", FailureCode, false);
        //        P[31] = new ReportParameter("FsrfilesDisplay", FsrFiles.Rows.Count.ToString(), false);
        //        P[32] = new ReportParameter("SE_Name", ICTicket.Technician.ContactName, false);
        //        P[33] = new ReportParameter("SE_ContactNumber", ICTicket.Technician.ContactNumber, false);
        //        P[34] = new ReportParameter("TL_ContactDetails", TL_ContactDetails, false);
        //        P[35] = new ReportParameter("SM_ContactDetails", SM_ContactDetails, false);
        //        P[36] = new ReportParameter("PartsInvoiceNumber", TSIR.PartsInvoiceNumber, false);

        //        report.ReportPath = Server.MapPath("~/Print/DMS_TSIR2.rdlc");
        //        report.SetParameters(P);
        //        ReportDataSource rds = new ReportDataSource();
        //        rds.Name = "Fsrfiles";//This refers to the dataset name in the RDLC file  
        //        rds.Value = FsrFiles;
        //        report.DataSources.Add(rds);
        //        ReportDataSource rds2 = new ReportDataSource();
        //        rds2.Name = "Material";//This refers to the dataset name in the RDLC file  
        //        rds2.Value = MaterialDT;
        //        report.DataSources.Add(rds2);
        //        ReportDataSource rds3 = new ReportDataSource();
        //        rds3.Name = "FMaterial";//This refers to the dataset name in the RDLC file  
        //        rds3.Value = FMaterialDT;
        //        report.DataSources.Add(rds3);
        //        Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
        //        return mybytes;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Please Contact Administrator. " + ex.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        lblMessage.Visible = true;
        //    }
        //    return null;
        //}

        protected void btnView_Click(object sender, EventArgs e)
        {

            divTSIRView.Visible = true;
            btnBackToList.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblTsirID = (Label)gvRow.FindControl("lblTsirID");
            UC_TSIRView.FillTsir(Convert.ToInt64(lblTsirID.Text));

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divTSIRView.Visible = false;
            btnBackToList.Visible = false;
        }

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    pnlTSIRManage.Visible = false;
        //    pnlTSIRView.Visible = true;

        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    long TsirID = Convert.ToInt64(gvICTickets.DataKeys[gvRow.RowIndex].Value);
        //    FillTsirDetails(TsirID);
        //}
        //void FillTsirDetails(long TsirID)
        //{
        //    //  txtNatureOfFailures.Enabled = false;
        //    //   txtProblemNoticedBy.Enabled = false;
        //    //  txtUnderWhatConditionFailureTaken.Enabled = false;
        //    //  txtFailureDetails.Enabled = false;
        //    //  txtPointsChecked.Enabled = false;
        //    //  txtPossibleRootCauses.Enabled = false;
        //    //  txtSpecificPointsNoticed.Enabled = false;
        //    //  txtPartsInvoiceNumber.Enabled = false;



        //    // btnSave.Visible = false;

        //    ICTicketTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
        //    txtTsirNumber.Text = ICTicketTSIR.TsirNumber;
        //    txtTsirStatus.Text = ICTicketTSIR.Status.Status;
        //    txtServiceCharge.Text = ICTicketTSIR.ServiceCharge.Material.MaterialCode + " - " + ICTicketTSIR.ServiceCharge.Material.MaterialDescription;

        //    txtNatureOfFailures.Text = ICTicketTSIR.NatureOfFailures;
        //    txtProblemNoticedBy.Text = ICTicketTSIR.ProblemNoticedBy;
        //    txtUnderWhatConditionFailureTaken.Text = ICTicketTSIR.UnderWhatConditionFailureTaken;
        //    txtFailureDetails.Text = ICTicketTSIR.FailureDetails;
        //    txtPointsChecked.Text = ICTicketTSIR.PointsChecked;
        //    txtPossibleRootCauses.Text = ICTicketTSIR.PossibleRootCauses;
        //    txtSpecificPointsNoticed.Text = ICTicketTSIR.SpecificPointsNoticed;
        //    txtPartsInvoiceNumber.Text = ICTicketTSIR.PartsInvoiceNumber;
        //    ViewState["TsirID"] = ICTicketTSIR.TsirID;

        //    FillMessage(TsirID);

        //    PDMS_ICTicket ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketTSIR.ICTicket.ICTicketID);
        //    UC_BasicInformation.SDMS_ICTicket = ICTicket;
        //    UC_BasicInformation.FillBasicInformation();

        //    gvMaterial.DataSource = new BDMS_Service().GetServiceMaterials(ICTicket.ICTicketID, null, TsirID, "", false, "");
        //    gvMaterial.DataBind();

        //    btnChecked.Visible = false;
        //    btnSendBack.Visible = false;
        //    btnReject.Visible = false;

        //    if (ICTicket.RestoreDate == null)
        //    {
        //        return;
        //    }
        //    string[] TsirCheck = ConfigurationManager.AppSettings["TsirCheck"].Split(',');
        //    string[] TsirApprove = ConfigurationManager.AppSettings["TsirApprove"].Split(',');
        //    if (TsirCheck.Contains(PSession.User.UserID.ToString()))
        //    {
        //        btnSendBack.Visible = false;
        //        btnReject.Visible = false;
        //        btnChecked.Visible = false;

        //        if ((ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Requested) || (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Rerequested))
        //        {
        //            btnChecked.Text = "Checked";
        //            btnChecked.Visible = true;

        //            //txtNatureOfFailures.Enabled = true;
        //            //txtProblemNoticedBy.Enabled = true;
        //            //txtUnderWhatConditionFailureTaken.Enabled = true;
        //            //txtFailureDetails.Enabled = true;
        //            //txtPointsChecked.Enabled = true;
        //            //txtPossibleRootCauses.Enabled = true;
        //            //txtSpecificPointsNoticed.Enabled = true;
        //            //txtPartsInvoiceNumber.Enabled = true;


        //            //btnSave.Visible = true;
        //        }
        //    }
        //    else if (TsirApprove.Contains(PSession.User.UserID.ToString()))
        //    {
        //        btnChecked.Visible = false;
        //        btnSendBack.Visible = false;
        //        btnReject.Visible = false;
        //        if (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Checked)
        //        {
        //            btnChecked.Visible = true;
        //            btnSendBack.Visible = true;
        //            btnReject.Visible = true;
        //            btnChecked.Text = "Approve";
        //        }
        //    }

        //    if ((ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Rejected) || (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Approved))
        //    {
        //        btnChecked.Visible = false;
        //        btnSendBack.Visible = false;
        //        btnReject.Visible = false;
        //    }

        //}
        //void FillMessage(long TsirID)
        //{
        //    Boolean? DisplayToDealer = null;

        //    if (PSession.User.UserTypeID == (short)UserTypes.Dealer)
        //    {
        //        DisplayToDealer = true;
        //        gvTSIRMessage.Columns[1].Visible = false;
        //    }


        //    List<PDMS_ICTicketTSIRMessage> TSIRMessage = new BDMS_ICTicketTSIR().GetICTicketTSIRMessage(null, TsirID, DisplayToDealer);
        //    if (TSIRMessage.Count == 0)
        //    {
        //        PDMS_ICTicketTSIRMessage N = new PDMS_ICTicketTSIRMessage();
        //        TSIRMessage.Add(N);
        //    }
        //    gvTSIRMessage.DataSource = TSIRMessage;
        //    gvTSIRMessage.DataBind();
        //}
        //protected void lblTSIRMessageAdd_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Visible = true;
        //    TextBox txtTSIRMessage = (TextBox)gvTSIRMessage.FooterRow.FindControl("txtTSIRMessage");
        //    CheckBox cbDisplayToDealer = (CheckBox)gvTSIRMessage.FooterRow.FindControl("cbDisplayToDealer");

        //    if (string.IsNullOrEmpty(txtTSIRMessage.Text.Trim()))
        //    {
        //        lblMessage.Text = "Please enter the Message";
        //        lblMessage.ForeColor = Color.Red;
        //        return;
        //    }
        //    long TsirID = Convert.ToInt64(ViewState["TsirID"]);
        //    Boolean DisplayToDealer = cbDisplayToDealer.Checked;
        //    if (PSession.User.UserTypeID == (short)UserTypes.Dealer)
        //    {
        //        DisplayToDealer = true;
        //    }
        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRMessage(TsirID, txtTSIRMessage.Text.Trim(), DisplayToDealer, PSession.User.UserID))
        //    {
        //        lblMessage.Text = "New Message is added for this TSIR";
        //        lblMessage.ForeColor = Color.Green;
        //        FillMessage(TsirID);
        //        txtTSIRMessage.Text = "";
        //        cbDisplayToDealer.Checked = false;
        //    }
        //    else
        //    {
        //        lblMessage.Text = "New Message is not added for this TSIR";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //protected void btnChecked_Click(object sender, EventArgs e)
        //{
        //    //int StatusID = 1;
        //    int StatusID = ICTicketTSIR.Status.StatusID;

        //    if ((ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Requested) || (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Rerequested))
        //    {
        //        StatusID = (short)TSIRStatus.Checked;
        //        lblMessage.Text = "TSIR Status changed to Checked";
        //    }
        //    else if (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Checked)
        //    {
        //        string[] TsirApprove = ConfigurationManager.AppSettings["TsirApprove"].Split(',');
        //        if (TsirApprove.Contains(PSession.User.UserID.ToString()))
        //        {
        //            StatusID = (short)TSIRStatus.Approved;
        //            lblMessage.Text = "TSIR Status changed to Approved";
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }

        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, StatusID, PSession.User.UserID, 0))
        //    {

        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}

        //protected void btnSendBack_Click(object sender, EventArgs e)
        //{
        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.SendBack, PSession.User.UserID, 0))
        //    {
        //        lblMessage.Text = "TSIR Status changed to Send Back";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}

        //protected void btnBack_Click(object sender, EventArgs e)
        //{
        //    pnlTSIRManage.Visible = true;
        //    pnlTSIRView.Visible = false;
        //}

        //protected void btnReject_Click1(object sender, EventArgs e)
        //{
        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.Rejected, PSession.User.UserID, 0))
        //    {
        //        lblMessage.Text = "TSIR Status changed to Rejected";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (!Validation())
        //    {
        //        return;
        //    }
        //    ICTicketTSIR.TsirID = ViewState["TsirID"] == null ? 0 : (long)ViewState["TsirID"];

        //    ICTicketTSIR.ServiceCharge = new PDMS_ServiceCharge();
        //    ICTicketTSIR.ServiceCharge.ServiceChargeID = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(ICTicketTSIR.TsirID, null).ServiceCharge.ServiceChargeID;
        //    ICTicketTSIR.NatureOfFailures = txtNatureOfFailures.Text.Trim();
        //    ICTicketTSIR.ProblemNoticedBy = txtProblemNoticedBy.Text.Trim();
        //    ICTicketTSIR.UnderWhatConditionFailureTaken = txtUnderWhatConditionFailureTaken.Text.Trim();
        //    ICTicketTSIR.FailureDetails = txtFailureDetails.Text.Trim();
        //    ICTicketTSIR.PointsChecked = txtPointsChecked.Text.Trim();
        //    ICTicketTSIR.PossibleRootCauses = txtPossibleRootCauses.Text.Trim();
        //    ICTicketTSIR.SpecificPointsNoticed = txtSpecificPointsNoticed.Text.Trim();
        //    ICTicketTSIR.PartsInvoiceNumber = txtPartsInvoiceNumber.Text.Trim();
        //    long DealerEmployeeID = new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIR(ICTicketTSIR, PSession.User.UserID);
        //    if (DealerEmployeeID != 0)
        //    {
        //        lblMessage.Text = "TSIR is updated successfully";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR is not updated successfully";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}

        //Boolean Validation()
        //{
        //    lblMessage.Visible = true;
        //    string Message = "";
        //    Boolean Ret = true;
        //    //if (string.IsNullOrEmpty(txtFailureRepeats.Text.Trim()))
        //    //{
        //    //    Message = Message + "<br/>Please Enter the Failure Repeats";
        //    //    Ret = false;
        //    //}

        //    if (string.IsNullOrEmpty(txtNatureOfFailures.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the NatureOfFailures";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtProblemNoticedBy.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the How Was Problem Noticed / Who  / When";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtUnderWhatConditionFailureTaken.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Under What Condition Failure Taken";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtFailureDetails.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Failure Details";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtPointsChecked.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Points Checked";
        //        Ret = false;
        //    }

        //    if (string.IsNullOrEmpty(txtPossibleRootCauses.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the Possible Root Causes";
        //        Ret = false;
        //    }
        //    if (string.IsNullOrEmpty(txtSpecificPointsNoticed.Text.Trim()))
        //    {
        //        Message = Message + "<br/>Please Enter the SpecificPoints Noticed";
        //        Ret = false;
        //    }
        //    lblMessage.Text = Message;
        //    return Ret;
        //}
    }
    static class MailFormate
    {

        public static string MailTsir = "<html><head> <style>  p.MsoNormal, li.MsoNormal, div.MsoNormal	{margin:0cm;margin-bottom:.0001pt;font-size:11.0pt;font-family:\"Calibri\",sans-serif;mso-fareast-language:EN-US;}</style> </head>"
 + "<body><table width=\"600px\"><tr><td style=\"height: 100px; width: 600px;\"><p><span>Dear Sir / Madam,</span></p><p><span><b>Please refer attached field complaint. Request you to look into this matter on priority.<br/><br/></b></span></p><br></td></tr></table>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>Regards,</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><b><span style='font-family:\"Swis721 Lt BT\";color:#002060;mso-fareast-language:EN-IN'>@@Name</span></b><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-family:\"Swis721 BT\",sans-serif;color:#7F7F7F;mso-fareast-language:EN-IN'>@@Designation</span><span style='font-family:\"Swis721 BT\",sans-serif;color:#1F497D;mso-fareast-language:EN-IN'><o:p></o:p></span></p><p class=MsoNormal><span style='font-family:\"Swis721 BT\",sans-serif;color:#1F497D;mso-fareast-language:EN-IN'><o:p>&nbsp;</o:p></span></p>"
 + "<p class=MsoNormal><b><span style='font-family:\"Swis721 Lt BT\";color:#002060;mso-fareast-language:EN-IN'>AJAX ENGINEERING PVT. LTD.</span></b><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-family:\"Swis721 Lt BT\";color:#7F7F7F;mso-fareast-language:EN-IN'>(Formerly AJAX FIORI ENGINEERING (I) PVT. LTD.)</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'> <o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'># 3, 16&amp;17, KIADB Industrial Area, Bashettyhalli</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>Doddaballapur &#8211; 561203, Karnataka, India.</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>Toll free No.: 1-800-419-0628</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>  (M): @@Phone</span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p>"
 + "<p class=MsoNormal><span style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'>E-mail: </span><span lang=EN-GB><a href=\"mailto:@@MailID\"><span lang=EN-IN style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";mso-fareast-language:EN-IN'>@@MailID</span></a>"
 + "</span><span lang=EN-GB style='font-size:10.0pt;font-family:\"Swis721 Lt BT\";color:black;mso-fareast-language:EN-IN'> </span><span style='font-size:12.0pt;font-family:\"Times New Roman\",serif;color:#002060;mso-fareast-language:EN-IN'><o:p></o:p></span></p></body></html>";




        public static string ForgotPassword = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">"
 + "<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><title>User Authorized</title></head><body><div><table width=\"600px\"><tr><td><p><strong>Subject: : Password reset request for Ajax DMS</strong></p>"
 + "</td></tr><tr><td style=\"height: 200px; width: 600px;\"><p><span>Dear <span>@@Addresse</span>,</span></p><p style=\"margin: 0in 0in 0pt\">You requested to have your password reset, below is your new password."
 + "</p><br /><p style=\"margin: 0in 0in 0pt\">Your account details are:</p> <p style=\"margin: 0in 0in 0pt\"> Login Id : @@UserName <br />  Password : @@Password<br /> </p> </td>"
 + "</tr> <tr> <td style=\"height: 60px; width: 600px;\">  <p> After logging in, you will be redirected to change password page to change your password.</p> <span> <br />"
 + "Thanks,<br /> Ajax DMS </span> </td></tr> </table> </div> </body> </html>";

    }
}