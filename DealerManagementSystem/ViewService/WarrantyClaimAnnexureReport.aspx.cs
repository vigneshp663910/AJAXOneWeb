using Business;
using Microsoft.Reporting.WebForms;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimAnnexureReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyClaimAnnexureReport; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "WarrantyClaimAnnexureReport.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            } 
        }
        public List<PDMS_WarrantyClaimAnnexureHeader> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimAnnexureReport"] == null)
                {
                    Session["DMS_WarrantyClaimAnnexureReport"] = new List<PDMS_WarrantyClaimAnnexureHeader>();
                }
                return (List<PDMS_WarrantyClaimAnnexureHeader>)Session["DMS_WarrantyClaimAnnexureReport"];
            }
            set
            {
                Session["DMS_WarrantyClaimAnnexureReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Claim Annexure Report');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                List<PModuleAccess> user = PSession.User.DMSModules;
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimAnnexureCreate).Count() != 0))
                //{
                //    btnGenerate.Visible = true;
                //}
                //else
                //{
                //    btnGenerate.Visible = false;
                //}               

                FillYear();

                fillDealer();
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillWarrantyClaimAnnexure();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillWarrantyClaimAnnexure()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                //ViewState["DealerCode"] = ddlDealerCode.SelectedValue;
                //ViewState["Year"] = ddlYear.SelectedValue;
                //ViewState["Month"] = ddlMonth.SelectedValue;
                List<PDMS_WarrantyClaimAnnexureHeader> SOIs = null;

                int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
                string DealerCode = ddlDealerCode.SelectedValue == "0" ? "" : ddlDealerCode.SelectedValue;
                SOIs = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureReport(null, DealerCode, Convert.ToInt32(ddlYear.SelectedValue), Month, null, null, "", null);
                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
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

                SDMS_WarrantyClaimHeader = SOIs;
                gvClaimByClaimID.DataSource = SOIs;
                gvClaimByClaimID.DataBind();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimAnnexureReport", "fillWarrantyClaimAnnexure", e1);
                throw e1;
            }
        }
        void FillYear()
        {

            for (int Year = 2015; Year <= DateTime.Now.Year; Year++)
            {
                ddlYear.Items.Add(new ListItem(Year.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable CommissionDT = new DataTable();
            CommissionDT.Columns.Add("Sl. No");
            CommissionDT.Columns.Add("Dealer");
            CommissionDT.Columns.Add("Dealer Name");
            CommissionDT.Columns.Add("IC Ticket ID");
            CommissionDT.Columns.Add("IC Ticket Date");
            CommissionDT.Columns.Add("Restore Date");
            CommissionDT.Columns.Add("Approved Date");
            CommissionDT.Columns.Add("Annexure Number");
            CommissionDT.Columns.Add("Annexure Date");
            CommissionDT.Columns.Add("Year");
            CommissionDT.Columns.Add("Month");

            CommissionDT.Columns.Add("Period From");
            CommissionDT.Columns.Add("Period To");
            CommissionDT.Columns.Add("Invoice Number");

            CommissionDT.Columns.Add("Customer");
            CommissionDT.Columns.Add("Customer Name");
            CommissionDT.Columns.Add("Model");
            CommissionDT.Columns.Add("HMR");
            CommissionDT.Columns.Add("Machine Serial Number");

            CommissionDT.Columns.Add("SAC / HSN Code");
            CommissionDT.Columns.Add("Material");
            CommissionDT.Columns.Add("Amount", typeof(decimal));
            CommissionDT.Columns.Add("Approved Amount", typeof(decimal));

            int RowNo = 0;
            foreach (PDMS_WarrantyClaimAnnexureHeader H in SDMS_WarrantyClaimHeader)
            {
                foreach (PDMS_WarrantyClaimAnnexureItem wc in H.AnnexureItems)
                {

                    RowNo = RowNo + 1;

                    CommissionDT.Rows.Add(RowNo, H.Dealer.DealerCode, H.Dealer.DealerName, wc.ICTicketID, wc.ICTicketDate,
                        wc.RestoreDate == null ? "" : ((DateTime)wc.RestoreDate).ToShortDateString()
                        , wc.ApprovedDate == null ? "" : ((DateTime)wc.ApprovedDate).ToShortDateString()
                        , H.AnnexureNumber, H.CreatedOn.ToShortDateString(), H.Year, H.MonthName, H.PeriodFrom.ToShortDateString(), H.PeriodTo.ToShortDateString()
                        , H.InvoiceNumber, wc.CustomerCode
                        , wc.CustomerName, wc.Model, wc.HMR, wc.MachineSerialNumber, wc.HSNCode, wc.Material, wc.ClaimAmount, wc.ApprovedAmount);
                }
            }

            new BXcel().ExporttoExcel(CommissionDT, "Annexure " + ddlDealerCode.SelectedValue);
        }

        protected void gvClaimByClaimID_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string supplierPOID = Convert.ToString(gvClaimByClaimID.DataKeys[e.Row.RowIndex].Value);

                    GridView gvItem = (GridView)e.Row.FindControl("gvICTicketItems");


                    List<PDMS_WarrantyClaimAnnexureItem> AnnexureItems = new List<PDMS_WarrantyClaimAnnexureItem>();
                    AnnexureItems = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyClaimAnnexureHeaderID == Convert.ToInt64(supplierPOID)).AnnexureItems;

                    gvItem.DataSource = AnnexureItems;
                    gvItem.DataBind();

                    Label lblInvoiceNumber = (Label)e.Row.FindControl("lblInvoiceNumber");
                    //    Button btnGenerateInvoice = (Button)e.Row.FindControl("btnGenerateInvoice");

                    if (string.IsNullOrEmpty(lblInvoiceNumber.Text))
                    {
                        lblInvoiceNumber.Visible = false;
                        List<PModuleAccess> user = PSession.User.DMSModules;
                        List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;
                        //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimInvoiceCreate).Count() != 0))
                        //    btnGenerateInvoice.Visible = true;
                    }
                    else
                    {
                        lblInvoiceNumber.Visible = true;
                        //btnGenerateInvoice.Visible = false;
                    }

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }

        void PrintC()
        {
            try
            {
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

                report.ReportPath = Server.MapPath("~/Print/Report1.rdlc");

                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(mybytes); // create the file
                Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
              //  PrintC();
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                string WarrantyClaimAnnexureHeaderID = Convert.ToString(gvClaimByClaimID.DataKeys[gvRow.RowIndex].Value);
                PDMS_WarrantyClaimAnnexureHeader AnnexureH = new PDMS_WarrantyClaimAnnexureHeader();
                AnnexureH = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyClaimAnnexureHeaderID == Convert.ToInt64(WarrantyClaimAnnexureHeaderID));

                if (AnnexureH.AnnexureNumber.Contains('N'))
                {
                    PrintAnnexureNC(AnnexureH);
                }
                else if (AnnexureH.AnnexureNumber.Contains('W'))
                {
                    PrintAnnexureWS(AnnexureH);
                }
                else
                {
                    PrintAnnexure50K(AnnexureH);
                }
                return;

                //decimal SumOfCommission = 0, SumOfNEPI = 0, Total = 0, ApprovedTotal = 0;
                //DataTable CommissionDT = new DataTable();
                //CommissionDT.Columns.Add("SNO");
                //CommissionDT.Columns.Add("ICTicketID");
                //CommissionDT.Columns.Add("ICTicketDate");
                //CommissionDT.Columns.Add("CustomerCode");
                //CommissionDT.Columns.Add("CustomerName");
                //CommissionDT.Columns.Add("Material");
                //CommissionDT.Columns.Add("MaterialDesc");
                //CommissionDT.Columns.Add("HMR");
                //CommissionDT.Columns.Add("MachineSerialNumber");
                //CommissionDT.Columns.Add("Model");
                //CommissionDT.Columns.Add("HSNCode");
                //CommissionDT.Columns.Add("RestoreDate");
                //CommissionDT.Columns.Add("Amount", typeof(decimal));
                //CommissionDT.Columns.Add("ApprovedAmount", typeof(decimal));

                //int CommissionRowNo = 0;
                //int NEPIRowNo = 0;
                ////  List<PDMS_WarrantyClaimAnnexureHeader> ClaimWarranty = SDMS_WarrantyClaimHeader.Where(M => M.AnnexureItem.Category == "Commission").ToList();
                ////  List<PDMS_WarrantyClaimAnnexureHeader> ClaimNEPI = SDMS_WarrantyClaimHeader.Where(M => M.InvoiceItem.Category == "NEPI").ToList();

                //DataTable ClaimNEPIDT = CommissionDT.Clone();

                //foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                //{
                //    if (item.Category == "Commission")
                //    {
                //        CommissionRowNo = CommissionRowNo + 1;
                //        CommissionDT.Rows.Add(CommissionRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR,
                //            item.MachineSerialNumber, item.Model, item.HSNCode, ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount);
                //        SumOfCommission = SumOfCommission + (decimal)item.ClaimAmount;
                //        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                //    }
                //    if (item.Category == "NEPI")
                //    {
                //        NEPIRowNo = NEPIRowNo + 1;
                //        ClaimNEPIDT.Rows.Add(NEPIRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR,
                //            item.MachineSerialNumber, item.Model, item.HSNCode, ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount);
                //        SumOfNEPI = SumOfNEPI + (decimal)item.ClaimAmount;
                //        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                //    }
                //}

                //DataTable InSAP = CommissionDT.Clone();



                //string contentType = string.Empty;
                //contentType = "application/pdf";
                //var CC = CultureInfo.CurrentCulture;
                //string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                //string extension;
                //string encoding;
                //string mimeType;
                //string[] streams;
                //Warning[] warnings;

                //LocalReport report = new LocalReport();
                //report.EnableExternalImages = true;

                //ReportParameter[] P = new ReportParameter[15];



                //string Annexure = "Annexure -" + AnnexureH.AnnexureNumber;
                //P[0] = new ReportParameter("DealerCode", AnnexureH.Dealer.DealerCode, false);
                //P[1] = new ReportParameter("DealerAddress", "b", false);
                //P[2] = new ReportParameter("FromDate", AnnexureH.PeriodFrom.ToShortDateString(), false);
                //P[3] = new ReportParameter("ToDate", AnnexureH.PeriodTo.ToShortDateString(), false);
                //P[4] = new ReportParameter("Annexure", Annexure, false);
                //P[5] = new ReportParameter("DateOfClaim", AnnexureH.CreatedOn.ToShortDateString(), false);
                //P[6] = new ReportParameter("SumOfCommission", SumOfCommission.ToString(), false);
                //P[7] = new ReportParameter("SumOfNEPI", SumOfNEPI.ToString(), false);
                //P[8] = new ReportParameter("Total", (SumOfCommission + SumOfNEPI).ToString(), false);

                //P[9] = new ReportParameter("DealerName", AnnexureH.Dealer.DealerName.ToUpper(), false);
                //P[10] = new ReportParameter("Address1", AnnexureH.Address1.ToUpper(), false);
                //P[11] = new ReportParameter("Address2", AnnexureH.Address2.ToUpper(), false);
                //P[12] = new ReportParameter("Contact", "Contact" + AnnexureH.Contact.ToUpper(), false);
                //P[13] = new ReportParameter("GSTIN", AnnexureH.GSTIN.ToUpper(), false);
                //P[14] = new ReportParameter("ApprovedTotal", ApprovedTotal.ToString(), false);

                //report.ReportPath = Server.MapPath("~/Print/DMS_ClaimAnnexure.rdlc");
                //ReportDataSource rds = new ReportDataSource();
                //rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                //rds.Value = CommissionDT;
                //report.DataSources.Add(rds);

                //ReportDataSource rds2 = new ReportDataSource();
                //rds2.Name = "DataSet2";//This refers to the dataset name in the RDLC file  
                //rds2.Value = ClaimNEPIDT;
                //report.DataSources.Add(rds2);



                //report.SetParameters(P);
                //Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  

                //Response.Buffer = true;
                //Response.Clear();
                //Response.ContentType = mimeType;
                //Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                //Response.BinaryWrite(mybytes); // create the file
                //Response.Flush(); // send it to the client to download
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("DMS_WarrantyClaimAnnexureCreate.aspx");
        //}

        //protected void btnGenerateInvoice_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    string WarrantyClaimAnnexureHeaderID = Convert.ToString(gvClaimByClaimID.DataKeys[gvRow.RowIndex].Value);
        //    Response.Redirect("DMS_WarrantyClaimInvoiceCreate.aspx");

        //}

        void PrintAnnexureNC(PDMS_WarrantyClaimAnnexureHeader AnnexureH)
        {
            try
            {
                decimal SumOfCommission = 0, SumOfNEPI = 0, Total = 0, ApprovedTotal = 0;
                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("ICTicketID");
                CommissionDT.Columns.Add("ICTicketDate");
                CommissionDT.Columns.Add("CustomerCode");
                CommissionDT.Columns.Add("CustomerName");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("MaterialDesc");
                CommissionDT.Columns.Add("HMR");
                CommissionDT.Columns.Add("MachineSerialNumber");
                CommissionDT.Columns.Add("Model");
                CommissionDT.Columns.Add("HSNCode");
                CommissionDT.Columns.Add("RestoreDate");
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                CommissionDT.Columns.Add("ApprovedAmount", typeof(decimal));

                int CommissionRowNo = 0;
                int NEPIRowNo = 0;
                //  List<PDMS_WarrantyClaimAnnexureHeader> ClaimWarranty = SDMS_WarrantyClaimHeader.Where(M => M.AnnexureItem.Category == "Commission").ToList();
                //  List<PDMS_WarrantyClaimAnnexureHeader> ClaimNEPI = SDMS_WarrantyClaimHeader.Where(M => M.InvoiceItem.Category == "NEPI").ToList();

                DataTable ClaimNEPIDT = CommissionDT.Clone();

                foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                {
                    if (item.Category == "Commission")
                    {
                        CommissionRowNo = CommissionRowNo + 1;
                        CommissionDT.Rows.Add(CommissionRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR,
                            item.MachineSerialNumber, item.Model, item.HSNCode, item.RestoreDate == null ? "" : ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount);
                        SumOfCommission = SumOfCommission + (decimal)item.ClaimAmount;
                        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                    }
                    if (item.Category == "NEPI")
                    {
                        NEPIRowNo = NEPIRowNo + 1;
                        ClaimNEPIDT.Rows.Add(NEPIRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR,
                            item.MachineSerialNumber, item.Model, item.HSNCode, item.RestoreDate == null ? "" : ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount);
                        SumOfNEPI = SumOfNEPI + (decimal)item.ClaimAmount;
                        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                    }
                }
                DataTable InSAP = CommissionDT.Clone();
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

                ReportParameter[] P = new ReportParameter[16];
                string Annexure = AnnexureH.AnnexureNumber;

                P[0] = new ReportParameter("DealerCode", AnnexureH.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerAddress", "b", false);
                P[2] = new ReportParameter("FromDate", AnnexureH.PeriodFrom.ToShortDateString(), false);
                P[3] = new ReportParameter("ToDate", AnnexureH.PeriodTo.ToShortDateString(), false);
                P[4] = new ReportParameter("Annexure", Annexure, false);
                P[5] = new ReportParameter("DateOfClaim", AnnexureH.CreatedOn.ToShortDateString(), false);
                P[6] = new ReportParameter("SumOfCommission", SumOfCommission.ToString(), false);
                P[7] = new ReportParameter("SumOfNEPI", SumOfNEPI.ToString(), false);
                P[8] = new ReportParameter("Total", (SumOfCommission + SumOfNEPI).ToString(), false);
                P[9] = new ReportParameter("DealerName", AnnexureH.Dealer.DealerName.ToUpper(), false);
                P[10] = new ReportParameter("Address1", AnnexureH.Address1.ToUpper(), false);
                P[11] = new ReportParameter("Address2", AnnexureH.Address2.ToUpper(), false);
                P[12] = new ReportParameter("Contact", "Contact" + AnnexureH.Contact.ToUpper(), false);
                P[13] = new ReportParameter("GSTIN", AnnexureH.GSTIN.ToUpper(), false);
                P[14] = new ReportParameter("ApprovedTotal", ApprovedTotal.ToString(), false);


                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= AnnexureH.CreatedOn)
                {
                    NewLogo = "1";
                }
                P[15] = new ReportParameter("NewLogo", NewLogo, false);
                // report.ReportPath = Server.MapPath("~/Print/DMS_ClaimAnnexureNC.rdlc");
                report.ReportPath = Server.MapPath("~/Print/DMS_ClaimAnnexureNC.rdlc");
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);

                ReportDataSource rds2 = new ReportDataSource();
                rds2.Name = "DataSet2";//This refers to the dataset name in the RDLC file  
                rds2.Value = ClaimNEPIDT;
                report.DataSources.Add(rds2);

                report.SetParameters(P);
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
        void PrintAnnexureWS(PDMS_WarrantyClaimAnnexureHeader AnnexureH)
        {
            try
            {
                decimal SumOfCommission = 0, SumOfNEPI = 0, ApprovedTotal = 0;
                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("ICTicketID");
                CommissionDT.Columns.Add("ICTicketDate");
                CommissionDT.Columns.Add("CustomerCode");
                CommissionDT.Columns.Add("CustomerName");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("MaterialDesc");
                CommissionDT.Columns.Add("HMR");
                CommissionDT.Columns.Add("MachineSerialNumber");
                CommissionDT.Columns.Add("Model");
                CommissionDT.Columns.Add("HSNCode");
                CommissionDT.Columns.Add("RestoreDate");
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                CommissionDT.Columns.Add("ApprovedAmount", typeof(decimal));
                CommissionDT.Columns.Add("Qty", typeof(decimal));

                int CommissionRowNo = 0;
                int NEPIRowNo = 0;
                DataTable ClaimNEPIDT = CommissionDT.Clone();
                foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                {
                    if (item.Category == "Warranty Material")
                    {
                        CommissionRowNo = CommissionRowNo + 1;
                        CommissionDT.Rows.Add(CommissionRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR, item.MachineSerialNumber, item.Model, item.HSNCode, item.RestoreDate == null ? "" : ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount, item.Qty);
                        SumOfCommission = SumOfCommission + (decimal)item.ClaimAmount;
                        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                    }
                    else
                    {
                        NEPIRowNo = NEPIRowNo + 1;
                        ClaimNEPIDT.Rows.Add(NEPIRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR, item.MachineSerialNumber, item.Model, item.HSNCode, item.RestoreDate == null ? "" : ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount, item.Qty);
                        SumOfNEPI = SumOfNEPI + (decimal)item.ClaimAmount;
                        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                    }
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

                ReportParameter[] P = new ReportParameter[16];
                string Annexure = AnnexureH.AnnexureNumber;
                P[0] = new ReportParameter("DealerCode", AnnexureH.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerAddress", "b", false);
                P[2] = new ReportParameter("FromDate", AnnexureH.PeriodFrom.ToShortDateString(), false);
                P[3] = new ReportParameter("ToDate", AnnexureH.PeriodTo.ToShortDateString(), false);
                P[4] = new ReportParameter("Annexure", Annexure, false);
                P[5] = new ReportParameter("DateOfClaim", AnnexureH.CreatedOn.ToShortDateString(), false);
                P[6] = new ReportParameter("SumOfCommission", SumOfCommission.ToString(), false);

                P[7] = new ReportParameter("Total", (SumOfCommission + SumOfNEPI).ToString(), false);
                P[8] = new ReportParameter("DealerName", AnnexureH.Dealer.DealerName.ToUpper(), false);
                P[9] = new ReportParameter("Address1", AnnexureH.Address1.ToUpper(), false);
                P[10] = new ReportParameter("Address2", AnnexureH.Address2.ToUpper(), false);
                P[11] = new ReportParameter("Contact", "Contact" + AnnexureH.Contact.ToUpper(), false);
                P[12] = new ReportParameter("GSTIN", AnnexureH.GSTIN.ToUpper(), false);
                P[13] = new ReportParameter("ApprovedTotal", ApprovedTotal.ToString(), false);
                P[14] = new ReportParameter("SumOfNEPI", SumOfNEPI.ToString(), false);
                DateTime NewLogoDate = Convert.ToDateTime(ConfigurationManager.AppSettings["NewLogoDate"]);
                string NewLogo = "0";
                if (NewLogoDate <= AnnexureH.CreatedOn)
                {
                    NewLogo = "1";
                }
                P[15] = new ReportParameter("NewLogo", NewLogo, false);

                report.ReportPath = Server.MapPath("~/Print/DMS_ClaimAnnexureWS.rdlc");
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);

                ReportDataSource rds2 = new ReportDataSource();
                rds2.Name = "DataSet2";//This refers to the dataset name in the RDLC file  
                rds2.Value = ClaimNEPIDT;
                report.DataSources.Add(rds2);

                report.SetParameters(P);
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
        void PrintAnnexure50K(PDMS_WarrantyClaimAnnexureHeader AnnexureH)
        {
            try
            {
                decimal SumOfCommission = 0,
                    Total = 0, ApprovedTotal = 0;
                DataTable CommissionDT = new DataTable();
                CommissionDT.Columns.Add("SNO");
                CommissionDT.Columns.Add("ICTicketID");
                CommissionDT.Columns.Add("ICTicketDate");
                CommissionDT.Columns.Add("CustomerCode");
                CommissionDT.Columns.Add("CustomerName");
                CommissionDT.Columns.Add("Material");
                CommissionDT.Columns.Add("MaterialDesc");
                CommissionDT.Columns.Add("HMR");
                CommissionDT.Columns.Add("MachineSerialNumber");
                CommissionDT.Columns.Add("Model");
                CommissionDT.Columns.Add("HSNCode");
                CommissionDT.Columns.Add("RestoreDate");
                CommissionDT.Columns.Add("Amount", typeof(decimal));
                CommissionDT.Columns.Add("ApprovedAmount", typeof(decimal));

                int CommissionRowNo = 0;
                int NEPIRowNo = 0;
                //  List<PDMS_WarrantyClaimAnnexureHeader> ClaimWarranty = SDMS_WarrantyClaimHeader.Where(M => M.AnnexureItem.Category == "Commission").ToList();
                //  List<PDMS_WarrantyClaimAnnexureHeader> ClaimNEPI = SDMS_WarrantyClaimHeader.Where(M => M.InvoiceItem.Category == "NEPI").ToList();

                DataTable ClaimNEPIDT = CommissionDT.Clone();

                foreach (PDMS_WarrantyClaimAnnexureItem item in AnnexureH.AnnexureItems)
                {
                    if (item.Category == "Commission")
                    {
                        CommissionRowNo = CommissionRowNo + 1;
                        CommissionDT.Rows.Add(CommissionRowNo, item.ICTicketID, ((DateTime)item.ICTicketDate).ToShortDateString(), item.CustomerCode, item.CustomerName.ToUpper(), item.Material, item.MaterialDesc, item.HMR,
                            item.MachineSerialNumber, item.Model, item.HSNCode, item.RestoreDate == null ? "" : ((DateTime)item.RestoreDate).ToShortDateString(), item.ClaimAmount, item.ApprovedAmount);
                        SumOfCommission = SumOfCommission + (decimal)item.ClaimAmount;
                        ApprovedTotal = ApprovedTotal + (decimal)item.ApprovedAmount;
                    }
                }
                DataTable InSAP = CommissionDT.Clone();
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

                ReportParameter[] P = new ReportParameter[14];
                string Annexure = "Annexure -" + AnnexureH.AnnexureNumber;
                P[0] = new ReportParameter("DealerCode", AnnexureH.Dealer.DealerCode, false);
                P[1] = new ReportParameter("DealerAddress", "b", false);
                P[2] = new ReportParameter("FromDate", AnnexureH.PeriodFrom.ToShortDateString(), false);
                P[3] = new ReportParameter("ToDate", AnnexureH.PeriodTo.ToShortDateString(), false);
                P[4] = new ReportParameter("Annexure", Annexure, false);
                P[5] = new ReportParameter("DateOfClaim", AnnexureH.CreatedOn.ToShortDateString(), false);
                P[6] = new ReportParameter("SumOfCommission", SumOfCommission.ToString(), false);

                P[7] = new ReportParameter("Total", (SumOfCommission).ToString(), false);
                P[8] = new ReportParameter("DealerName", AnnexureH.Dealer.DealerName.ToUpper(), false);
                P[9] = new ReportParameter("Address1", AnnexureH.Address1.ToUpper(), false);
                P[10] = new ReportParameter("Address2", AnnexureH.Address2.ToUpper(), false);
                P[11] = new ReportParameter("Contact", "Contact" + AnnexureH.Contact.ToUpper(), false);
                P[12] = new ReportParameter("GSTIN", AnnexureH.GSTIN.ToUpper(), false);
                P[13] = new ReportParameter("ApprovedTotal", ApprovedTotal.ToString(), false);

                report.ReportPath = Server.MapPath("~/Print/DMS_ClaimAnnexure.rdlc");
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds.Value = CommissionDT;
                report.DataSources.Add(rds);




                report.SetParameters(P);
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



        protected void gvClaimByClaimID_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimByClaimID.PageIndex = e.NewPageIndex;
            gvClaimByClaimID.DataSource = SDMS_WarrantyClaimHeader;
            gvClaimByClaimID.DataBind();

        }
    }
}