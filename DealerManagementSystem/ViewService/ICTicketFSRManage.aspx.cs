using Business;
using Microsoft.Reporting.WebForms;
using Properties; 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketFSRManage : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketFSRManage; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ICTicketFSRManage.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicketFSR> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketFSRManage"] == null)
                {
                    Session["DMS_ICTicketFSRManage"] = new List<PDMS_ICTicketFSR>();
                }
                return (List<PDMS_ICTicketFSR>)Session["DMS_ICTicketFSRManage"];
            }
            set
            {
                Session["DMS_ICTicketFSRManage"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket FSR Manage1');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

                fillServiceStatus();
                fillDealer();

                if (Session["ICTicketNumber"] != null)
                {
                    txtICTicketNumber.Text = (string)Session["ICTicketNumber"];
                    fillICTicket();
                    Session["ICTicketNumber"] = null;
                }
                else
                {
                    txtICLoginDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtICLoginDateTo.Text = DateTime.Now.ToShortDateString();
                }

                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
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
                int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                int? TechnicianID = null;
                List<PDMS_ICTicketFSR> SOIs = null;
                if (PSession.User.IsTechnician)
                {
                    TechnicianID = PSession.User.UserID;
                }

                SOIs = new BDMS_ICTicketFSR().GetICTicketFSRManage(DealerCode, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, StatusID, TechnicianID);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
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



                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("ICTicketFSRManage", "fillICTicket", e1);
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

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Requested Date");
            dt.Columns.Add("Model");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Service Priority");
            dt.Columns.Add("Service Status");
            dt.Columns.Add("Margin");

            foreach (PDMS_ICTicketFSR IC in ICTicket)
            {
                dt.Rows.Add(
                    IC.ICTicket.ICTicketNumber
                    , IC.ICTicket.ICTicketDate.ToShortDateString()
                    , IC.ICTicket.Dealer.DealerCode
                    , IC.ICTicket.Dealer.DealerName
                    , IC.ICTicket.Customer.CustomerCode
                    , IC.ICTicket.Customer.CustomerName
                    , IC.ICTicket.RequestedDate == null ? "" : ((DateTime)IC.ICTicket.RequestedDate).ToShortDateString()
                    , IC.ICTicket.Equipment.EquipmentModel
                    , IC.ICTicket.ServiceType == null ? "" : IC.ICTicket.ServiceType.ServiceType
                    , IC.ICTicket.ServicePriority == null ? "" : IC.ICTicket.ServicePriority.ServicePriority
                    , IC.ICTicket.ServiceStatus.ServiceStatus
                    , IC.ICTicket.IsMarginWarranty
                    );
            }



            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        void fillServiceStatus()
        {
            ddlStatus.DataTextField = "ServiceStatus";
            ddlStatus.DataValueField = "ServiceStatusID";
            ddlStatus.DataSource = new BDMS_Service().GetServiceStatus(null, null);
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicket;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbReqDecline = (LinkButton)e.Row.FindControl("lbReqDecline");
                    Label lblServiceStatusID = (Label)e.Row.FindControl("lblServiceStatusID");
                    lbReqDecline.Visible = false;
                    if (Convert.ToInt32(lblServiceStatusID.Text) == (short)DMS_ServiceStatus.Open)
                    {
                        lbReqDecline.Visible = true;
                    }

                    string supplierPOID = Convert.ToString(gvICTickets.DataKeys[e.Row.RowIndex].Value);

                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");

                    //Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    //GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    //gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                    //gvFileAttached.DataBind();


                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    //   supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.ICTicketNumber == supplierPOID).;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();
                     
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                PDMS_ICTicketFSR FSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null, null, null, null, null, null, null);
                PrintFSR(FSR);
                return;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        string UrlValueToImagePath(string UrlValue, string FileName)
        {
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath(string.Format("~/Signature/{0}.jpg", FileName)), FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Convert.FromBase64String(Regex.Match(UrlValue, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value));
                    bw.Close();
                }
            }
            return HttpContext.Current.Server.MapPath(string.Format("~/Signature/{0}.jpg", FileName));
        }

        void PrintFSR(PDMS_ICTicketFSR FSR)
        {
            try
            {
                new BDMS_ICTicketFSR().ICTicket_Directorys(Server.MapPath("~"));
                PDMS_Customer Dealer = new BDMS_Customer().getCustomerAddressFromSAP(FSR.ICTicket.Dealer.DealerCode);
                string DealerAddress1 = (Dealer.Address1 + (string.IsNullOrEmpty(Dealer.Address2) ? "" : "," + Dealer.Address2) + (string.IsNullOrEmpty(Dealer.Address3) ? "" : "," + Dealer.Address3)).Trim(',', ' ');
                string DealerAddress2 = (Dealer.City + (string.IsNullOrEmpty(Dealer.State.State) ? "" : "," + Dealer.State.State) + (string.IsNullOrEmpty(Dealer.Pincode) ? "" : "-" + Dealer.Pincode)).Trim(',', ' ');

                PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(FSR.ICTicket.Customer.CustomerCode);
                string CustomerAddress1 = (Customer.Address1 + (string.IsNullOrEmpty(Customer.Address2) ? "" : "," + Customer.Address2) + (string.IsNullOrEmpty(Customer.Address3) ? "" : "," + Customer.Address3)).Trim(',', ' ');
                string CustomerAddress2 = (Customer.City + (string.IsNullOrEmpty(Customer.State.State) ? "" : "," + Customer.State.State) + (string.IsNullOrEmpty(Customer.Pincode) ? "" : "-" + Customer.Pincode)).Trim(',', ' ');

                DataTable AvailabilityOfOtherMachineDT = new DataTable();
                AvailabilityOfOtherMachineDT.Columns.Add("TypeOfMachine");
                AvailabilityOfOtherMachineDT.Columns.Add("Qty");
                AvailabilityOfOtherMachineDT.Columns.Add("Mack");

                //List<PCustomerProduct> AvailabilityOfOtherMachine = new BDMS_Customer().GetCustomerProduct(null, FSR.ICTicket.ICTicketID, null, null, null);
                List<PDMS_AvailabilityOfOtherMachine> AvailabilityOfOtherMachine = new BDMS_AvailabilityOfOtherMachine().GetAvailabilityOfOtherMachine(FSR.ICTicket.ICTicketID, null, null, null);

                foreach (PDMS_AvailabilityOfOtherMachine Aom in AvailabilityOfOtherMachine)
                {
                    AvailabilityOfOtherMachineDT.Rows.Add(Aom.TypeOfMachine.TypeOfMachine, Aom.Quantity, Aom.Make.Make);
                }

                DataTable FsrFiles = new DataTable();
                FsrFiles.Columns.Add("FileName1");
                FsrFiles.Columns.Add("FiePath1");
                FsrFiles.Columns.Add("FileName2");
                FsrFiles.Columns.Add("FiePath2");


                string Path = "";
                string BeforeMachineRestoreFilePath = "";
                string AfterMachineRestoreFilePath = "";

                string TechnicianFilePath = "";
                string CustomerFilePath = "";

                string TechnicianSignatureFilePath = "";
                string CustomerSignatureFilePath = "";

                Boolean Signatured = false;
                PICTicketFSRSignature FSRSignature = new BDMS_ICTicketFSR().GetICTicketFSRSignatureByFsrIDDownload(FSR.FsrID);
                if (FSRSignature.FSRSignatureID != 0)
                {
                    Path = "ICTickrtFSR_Files/Signature/" + FSRSignature.FSRSignatureID;
                    Signatured = true;
                    

                    string FileType = string.IsNullOrEmpty(FSRSignature.FileType) ? "png" : FSRSignature.FileType;

                    if (File.Exists(Server.MapPath("~/" + Path + "TPhoto" + "." + FileType)))
                    {
                        File.Delete(Server.MapPath("~/" + Path + "TPhoto" + "." + FileType));
                    }
                    File.WriteAllBytes(Server.MapPath("~/" + Path+ "TPhoto"+"."+ FileType), FSRSignature.TPhoto.AttachedFile); 
                    TechnicianFilePath = new Uri(Server.MapPath("~/" + Path + "TPhoto" + "." + FileType)).AbsoluteUri;

                    if (File.Exists(Server.MapPath("~/" + Path + "CPhoto" + "." + FileType)))
                    {
                        File.Delete(Server.MapPath("~/" + Path + "CPhoto" + "." + FileType));
                    }
                    File.WriteAllBytes(Server.MapPath("~/" + Path + "CPhoto" + "." + FileType), FSRSignature.CPhoto.AttachedFile);
                    CustomerFilePath = new Uri(Server.MapPath("~/" + Path + "CPhoto" + "." + FileType)).AbsoluteUri;


                    if (File.Exists(Server.MapPath("~/" + Path + "TSignature" + "." + FileType)))
                    {
                        File.Delete(Server.MapPath("~/" + Path + "TSignature" + "." + FileType));
                    }
                    File.WriteAllBytes(Server.MapPath("~/" + Path + "TSignature" + "." + FileType), FSRSignature.TSignature.AttachedFile);
                    TechnicianSignatureFilePath = new Uri(Server.MapPath("~/" + Path + "TSignature" + "." + FileType)).AbsoluteUri;

                    if (File.Exists(Server.MapPath("~/" + Path + "CSignature" + "." + FileType)))
                    {
                        File.Delete(Server.MapPath("~/" + Path + "CSignature" + "." + FileType));
                    }
                    File.WriteAllBytes(Server.MapPath("~/" + Path + "CSignature" + "." + FileType), FSRSignature.CSignature.AttachedFile);
                    CustomerSignatureFilePath = new Uri(Server.MapPath("~/" + Path + "CSignature" + "." + FileType)).AbsoluteUri; 
                }
                //if (FSRSignature.FsrID != 0)
                //{
                //    Signatured = true;
                //    TechnicianFilePath = string.IsNullOrEmpty(FSRSignature.TPhoto) ? "" : new Uri(UrlValueToImagePath(FSRSignature.TPhoto, "Tech" + FSR.FSRNumber)).AbsoluteUri;
                //    CustomerFilePath = string.IsNullOrEmpty(FSRSignature.CPhoto) ? "" : new Uri(UrlValueToImagePath(FSRSignature.CPhoto, "Cust" + FSR.FSRNumber)).AbsoluteUri;
                //    TechnicianSignatureFilePath = string.IsNullOrEmpty(FSRSignature.TSignature) ? "" : new Uri(UrlValueToImagePath(FSRSignature.TSignature, "TechSing" + FSR.FSRNumber)).AbsoluteUri;
                //    CustomerSignatureFilePath = string.IsNullOrEmpty(FSRSignature.CSignature) ? "" : new Uri(UrlValueToImagePath(FSRSignature.CSignature, "CustSing" + FSR.FSRNumber)).AbsoluteUri;
                //}

                List<PDMS_FSRAttachedFile> FSRFile = new List<PDMS_FSRAttachedFile>();
                List<PDMS_FSRAttachedFile> FSRFileAll = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileDetails(FSR.ICTicket.ICTicketID, null);
                for (int i = 0; i < FSRFileAll.Count(); i++)
                {
                    Path = "";
                    int FileNameID = FSRFileAll[i].FSRAttachedName.FSRAttachedFileNameID;

                    if ((FileNameID == (short)FSRAttachedFileName.Technician) || (FileNameID == (short)FSRAttachedFileName.Customer)
                        || (FileNameID == (short)FSRAttachedFileName.TechnicianSignature) || (FileNameID == (short)FSRAttachedFileName.CustomerSignature))
                    {
                        if (!Signatured)
                        {
                           // PDMS_FSRAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(FSRFileAll[i].AttachedFileID);
                            PAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(FSRFileAll[i].AttachedFileID);
                            Path = "ICTickrtFSR_Files/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];
                            if (File.Exists(Server.MapPath("~/" + Path)))
                            {
                                File.Delete(Server.MapPath("~/" + Path));
                            }
                            File.WriteAllBytes(Server.MapPath("~/" + Path), F1.AttachedFile);

                            if (FileNameID == (short)FSRAttachedFileName.Technician)
                                TechnicianFilePath = new Uri(Server.MapPath("~/" + Path)).AbsoluteUri;
                            else if (FileNameID == (short)FSRAttachedFileName.Customer)
                                CustomerFilePath = new Uri(Server.MapPath("~/" + Path)).AbsoluteUri;
                            else if (FileNameID == (short)FSRAttachedFileName.TechnicianSignature)
                                TechnicianSignatureFilePath = new Uri(Server.MapPath("~/" + Path)).AbsoluteUri;
                            else if (FileNameID == (short)FSRAttachedFileName.CustomerSignature)
                                CustomerSignatureFilePath = new Uri(Server.MapPath("~/" + Path)).AbsoluteUri;
                        }
                    }
                    else if ((FileNameID == (short)FSRAttachedFileName.BeforeMachineRestore) || (FileNameID == (short)FSRAttachedFileName.AfterMachineRestore))
                    {
                       // PDMS_FSRAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(FSRFileAll[i].AttachedFileID);
                        PAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(FSRFileAll[i].AttachedFileID);
                        Path = "ICTickrtFSR_Files/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];
                        if (File.Exists(Server.MapPath("~/" + Path)))
                        {
                            File.Delete(Server.MapPath("~/" + Path));
                        }
                        File.WriteAllBytes(Server.MapPath("~/" + Path), F1.AttachedFile);

                        if (FileNameID == (short)FSRAttachedFileName.BeforeMachineRestore)
                            BeforeMachineRestoreFilePath = new Uri(Server.MapPath("~/" + Path)).AbsoluteUri;
                        else if (FileNameID == (short)FSRAttachedFileName.AfterMachineRestore)
                            AfterMachineRestoreFilePath = new Uri(Server.MapPath("~/" + Path)).AbsoluteUri;
                    }
                    else
                    {
                        FSRFile.Add(FSRFileAll[i]);
                    }
                }

                string Path1 = "";
                string Path2 = "";
                for (int i = 0; i < FSRFile.Count(); i++)
                {
                   // PDMS_FSRAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(FSRFile[i].AttachedFileID);
                    PAttachedFile F1 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(FSRFile[i].AttachedFileID);
                    string Url1 = "ICTickrtFSR_Files/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];
                    if (File.Exists(MapPath("~/"+Url1)))
                    {
                        File.Delete(MapPath("~/"+Url1));
                    }
                    File.WriteAllBytes(MapPath("~/"+Url1), F1.AttachedFile);
                    Path1 = new Uri(Server.MapPath("~/" + Url1)).AbsoluteUri;

                    if (i + 1 != FSRFile.Count())
                    {
                      //  PDMS_FSRAttachedFile F2 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileByID(FSRFile[i + 1].AttachedFileID);
                        PAttachedFile F2 = new BDMS_ICTicketFSR().GetICTicketFSRAttachedFileForDownload(FSRFile[i + 1].AttachedFileID);
                        string Url2 = "ICTickrtFSR_Files/" + F2.AttachedFileID + "." + F2.FileName.Split('.')[F2.FileName.Split('.').Count() - 1];
                        if (File.Exists(MapPath("~/" + Url2)))
                        {
                            File.Delete(MapPath("~/" + Url2));
                        }
                        File.WriteAllBytes(MapPath("~/" + Url2), F2.AttachedFile);
                        Path2 = new Uri(Server.MapPath("~/" + Url2)).AbsoluteUri;
                         
                        FsrFiles.Rows.Add(F1.ReferenceName, Path1, F2.ReferenceName, Path2);
                    }
                    else
                    {
                        FsrFiles.Rows.Add(F1.ReferenceName, Path1, "", "");
                    }
                    i = i + 1;
                }



                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = new ReportParameter[63];

                string Year = "";
                if (FSR.FSRDate.Month > 3)
                {
                    Year = FSR.FSRDate.Year.ToString().Substring(2, 2) + "-" + (FSR.FSRDate.Year + 1).ToString().Substring(2, 2);
                }
                else
                {
                    Year = (FSR.FSRDate.Year - 1).ToString().Substring(2, 2) + "" + FSR.FSRDate.Year.ToString().Substring(2, 2);
                }


                string FSRNumber = FSR.ICTicket.Dealer.DealerCode + "/" + FSR.ICTicket.ICTicketNumber + "/" + FSR.ICTicket.Technician.UserName + "/" + Year;

                P[0] = new ReportParameter("FsrNumber", FSRNumber, false);

                P[1] = new ReportParameter("CustomerCode", Customer.CustomerCode, false);
                P[2] = new ReportParameter("CustomerName", Customer.CustomerName, false);
                P[3] = new ReportParameter("CustomerAddress1", CustomerAddress1, false);
                P[4] = new ReportParameter("CustomerAddress2", CustomerAddress2, false);
                P[5] = new ReportParameter("CustomerMail", Customer.Email, false);
                P[6] = new ReportParameter("CustomerStateCode", Customer.State.StateCode, false);
                P[7] = new ReportParameter("CustomerGST", Customer.GSTIN, false);

                P[8] = new ReportParameter("ContactPerson", FSR.ICTicket.SiteContactPersonName, false);
                P[9] = new ReportParameter("ContactNumber1", FSR.ICTicket.SiteContactPersonNumber, false);
                P[10] = new ReportParameter("Application", FSR.ICTicket.MainApplication == null ? "" : FSR.ICTicket.MainApplication.MainApplication, false);
                P[11] = new ReportParameter("Designation", FSR.ICTicket.SiteContactPersonDesignation == null ? "" : FSR.ICTicket.SiteContactPersonDesignation.Designation, false);
                P[12] = new ReportParameter("EquipmentModel", FSR.ICTicket.Equipment.EquipmentModel.Model, false);
                P[13] = new ReportParameter("EquipmentSerialNo", FSR.ICTicket.Equipment.EquipmentSerialNo, false);
                P[14] = new ReportParameter("EngineModel", FSR.ICTicket.Equipment.EngineModel, false);
                P[15] = new ReportParameter("EngineSerialNo", FSR.ICTicket.Equipment.EngineSerialNo, false);
                P[16] = new ReportParameter("HMR", Convert.ToString(FSR.ICTicket.CurrentHMRValue) + " (" + FSR.ICTicket.Equipment.EquipmentModel.Division.UOM + ")", false);
                P[17] = new ReportParameter("FsrDate", FSR.FSRDate.ToShortDateString(), false);
                P[18] = new ReportParameter("TypeOfService", FSR.ICTicket.ServiceType.ServiceType, false);
                P[19] = new ReportParameter("Complaints", FSR.Complaint, false);
                P[20] = new ReportParameter("DepartureDate", FSR.ICTicket.DepartureDate == null ? "" : ((DateTime)FSR.ICTicket.DepartureDate).ToShortDateString(), false);
                P[21] = new ReportParameter("DepartureTime", FSR.ICTicket.DepartureDate == null ? "" : ((DateTime)FSR.ICTicket.DepartureDate).ToShortTimeString(), false);
                P[22] = new ReportParameter("ArrivalSiteDate", FSR.ICTicket.ReachedDate == null ? "" : ((DateTime)FSR.ICTicket.ReachedDate).ToShortDateString(), false);
                P[23] = new ReportParameter("ArrivalSiteTime", FSR.ICTicket.ReachedDate == null ? "" : ((DateTime)FSR.ICTicket.ReachedDate).ToShortTimeString(), false);
                P[24] = new ReportParameter("DepartureSiteDate", FSR.ICTicket.RestoreDate == null ? "" : ((DateTime)FSR.ICTicket.RestoreDate).ToShortDateString(), false);
                P[25] = new ReportParameter("DepartureSiteTime", FSR.ICTicket.RestoreDate == null ? "" : ((DateTime)FSR.ICTicket.RestoreDate).ToShortTimeString(), false);
                P[26] = new ReportParameter("ArrivalBackDate", FSR.ICTicket.ArrivalBack == null ? "" : ((DateTime)FSR.ICTicket.ArrivalBack).ToShortDateString(), false);
                P[27] = new ReportParameter("ArrivalBackTime", FSR.ICTicket.ArrivalBack == null ? "" : ((DateTime)FSR.ICTicket.ArrivalBack).ToShortTimeString(), false);
                P[28] = new ReportParameter("CustomerRemarks", FSR.CustomerRemarks, false);
                P[29] = new ReportParameter("CustomerSatisfactionLevel", FSR.ICTicket.CustomerSatisfactionLevel == null ? "" : FSR.ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevel, false);
                P[30] = new ReportParameter("ModeOfPayment", FSR.ModeOfPayment == null ? "" : FSR.ModeOfPayment.ModeOfPayment, false);
                P[31] = new ReportParameter("ComplaintStatus", FSR.ComplaintStatus, false);

                P[32] = new ReportParameter("TypeOfFailure", FSR.ICTicket.ServicePriority == null ? "" : FSR.ICTicket.ServicePriority.ServicePriority, false);
                P[33] = new ReportParameter("State", FSR.ICTicket.Address.State.State, false);
                P[34] = new ReportParameter("District", FSR.ICTicket.Address.District.District, false);
                P[35] = new ReportParameter("Location", FSR.ICTicket.Location, false);
                P[36] = new ReportParameter("MachineMaintenanceLevel", FSR.MachineMaintenanceLevel == null ? "" : FSR.MachineMaintenanceLevel.MachineMaintenanceLevel, false);
                P[37] = new ReportParameter("Rental", FSR.IsRental ? "Rental" : "", false);

                P[38] = new ReportParameter("ContractorName", FSR.RentalName, false);
                P[39] = new ReportParameter("ContractorNumber", FSR.RentalNumber, false);
                P[40] = new ReportParameter("OperatorName", FSR.OperatorName, false);
                P[41] = new ReportParameter("OperatorNumber", FSR.OperatorNumber, false);
                P[42] = new ReportParameter("NatureOfComplaint", FSR.NatureOfComplaint, false);
                P[43] = new ReportParameter("Observation", FSR.Observation, false);
                P[44] = new ReportParameter("WorkCarriedOut", FSR.WorkCarriedOut, false);


                string SERecommendedParts = "";
                List<PDMS_ServiceMaterial> MaterialC = new BDMS_Service().GetServiceMaterials(FSR.ICTicket.ICTicketID, null, null, "", false, "");
                foreach (PDMS_ServiceMaterial Mat in MaterialC)
                {
                    if (Mat.IsRecomenedParts)
                        SERecommendedParts = SERecommendedParts + ", " + Mat.Material.MaterialCode;
                }
                SERecommendedParts = SERecommendedParts.Trim(',');

                P[45] = new ReportParameter("SERecommendedParts", SERecommendedParts, false);

                P[46] = new ReportParameter("BeforeMachineRestoreFilePath", BeforeMachineRestoreFilePath, false);
                P[47] = new ReportParameter("AfterMachineRestoreFilePath", AfterMachineRestoreFilePath, false);

                P[48] = new ReportParameter("TechnicianFilePath", TechnicianFilePath, false);
                P[49] = new ReportParameter("CustomerFilePath", CustomerFilePath, false);

                P[50] = new ReportParameter("TechnicianSignatureFilePath", TechnicianSignatureFilePath, false);
                P[51] = new ReportParameter("CustomerSignatureFilePath", CustomerSignatureFilePath, false);

                P[52] = new ReportParameter("DealerCode", Dealer.CustomerCode, false);
                P[53] = new ReportParameter("DealerName", Dealer.CustomerName, false);
                P[54] = new ReportParameter("DealerAddress1", DealerAddress1, false);
                P[55] = new ReportParameter("DealerAddress2", DealerAddress2, false);
                P[56] = new ReportParameter("DealerStateCode", Dealer.State.StateCode, false);
                P[57] = new ReportParameter("DealerGST", Dealer.GSTIN, false);
                P[58] = new ReportParameter("SESuggestion", FSR.Report, false);
                P[59] = new ReportParameter("ContactNumber2", FSR.ICTicket.SiteContactPersonNumber2, false);

                decimal WorkedDays = 0;
                List<PDMS_ServiceTechnician> SDMS_TechniciansWD = new BDMS_Service().GetTechniciansByTicketID(FSR.ICTicket.ICTicketID);
                foreach (PDMS_ServiceTechnician t in SDMS_TechniciansWD)
                {
                    foreach (PDMS_ServiceTechnicianWorkedDate W in t.ServiceTechnicianWorkedDate)
                    {
                        WorkedDays = WorkedDays + 1;
                    }
                }

                P[60] = new ReportParameter("NumberOfDaysWorked", Convert.ToString(WorkedDays), false);
                P[61] = new ReportParameter("TechnicianSignatureBy", FSRSignature.TName, false);
                P[62] = new ReportParameter("CustomerSignatureBy", FSRSignature.CName, false);

                report.ReportPath = Server.MapPath("~/Print/DMS_FSR.rdlc");
                report.SetParameters(P);

                ReportDataSource rds1 = new ReportDataSource();
                rds1.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds1.Value = AvailabilityOfOtherMachineDT;
                report.DataSources.Add(rds1);

                ReportDataSource rds2 = new ReportDataSource();
                rds2.Name = "Fsrfiles";//This refers to the dataset name in the RDLC file  
                rds2.Value = FsrFiles;
                report.DataSources.Add(rds2);

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
     
        protected void btnRequest_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            PDMS_ICTicketFSR FSR = new BDMS_ICTicketFSR().GetICTicketFSRByFsrID(Convert.ToInt64(gvICTickets.DataKeys[index].Value), null, null, null, null, null, null, null);
            string Year = "";
            if (FSR.FSRDate.Month > 3)
            {
                Year = FSR.FSRDate.Year.ToString().Substring(2, 2) + "-" + (FSR.FSRDate.Year + 1).ToString().Substring(2, 2);
            }
            else
            {
                Year = (FSR.FSRDate.Year - 1).ToString().Substring(2, 2) + "" + FSR.FSRDate.Year.ToString().Substring(2, 2);
            }
            string FSRNumber = FSR.ICTicket.Dealer.DealerCode + "/" + FSR.ICTicket.ICTicketNumber + "/" + FSR.ICTicket.Technician.UserName + "/" + Year;

            PDMS_Customer Customer = new BDMS_Customer().getCustomerAddressFromSAP(FSR.ICTicket.Customer.CustomerCode);

            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);

            string URL = ConfigurationManager.AppSettings["URL"] + "/Customer/TSIROTPApprove.aspx?ICTicketNumber=" + FSR.ICTicket.ICTicketNumber;

            string Message = "Greetings from Ajax.  " + sRandomOTP + " is the one time password (OTP) for acknowledging FSR " + FSRNumber + ". Click below link to acknowledge. Do not share it with anyone. " + URL;
            if (!string.IsNullOrEmpty(Customer.Mobile.Trim()))
            {
                new BSmsManager().SendSMS(Customer.Mobile, Message);
            }
        }
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < iOTPLength; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        protected void lbUpdateSignature_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "DMS_ICTicketFSRUpdateSignature.aspx?TicketID=" + ((Label)gvICTickets.Rows[index].FindControl("lblICTicketID")).Text;
            Response.Redirect(url);
        }
    }
}