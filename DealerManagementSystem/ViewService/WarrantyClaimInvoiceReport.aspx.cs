using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class WarrantyClaimInvoiceReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_WarrantyClaimInvoiceReport; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_WarrantyClaimInvoiceReport.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_WarrantyClaimInvoice> SDMS_WarrantyClaimHeader
        {
            get
            {
                if (Session["DMS_WarrantyClaimInvoiceReport"] == null)
                {
                    Session["DMS_WarrantyClaimInvoiceReport"] = new List<PDMS_WarrantyClaimInvoice>();
                }
                return (List<PDMS_WarrantyClaimInvoice>)Session["DMS_WarrantyClaimInvoiceReport"];
            }
            set
            {
                Session["DMS_WarrantyClaimInvoiceReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Warranty » Final Invoice Report');</script>");

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                // List<PModuleAccess> user = PSession.User.DMSModules;
                // List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimInvoiceCreate).Count() != 0))
                //{
                //    btnGenerate.Visible = true;
                //}
                //else
                //{
                //    btnGenerate.Visible = false;
                //}      

                FillYear();
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
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

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillWarrantyInvoice();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillWarrantyInvoice()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                string DealerCode = ddlDealerCode.SelectedValue == "0" ? "" : ddlDealerCode.SelectedValue;
                int? Year = null;
                int? Month = null;
                int? MonthRange = null;
                int? InvoiceTypeID = null;
                if (ddlYear.SelectedValue != "0")
                    Year = Convert.ToInt32(ddlYear.SelectedValue);
                if (ddlMonth.SelectedValue != "0")
                    Month = Convert.ToInt32(ddlMonth.SelectedValue);
                if (ddlMonthRange.SelectedValue != "0")
                    MonthRange = Convert.ToInt32(ddlMonthRange.SelectedValue);
                if (ddlInvoiceTypeID.SelectedValue != "0")
                    InvoiceTypeID = Convert.ToInt32(ddlInvoiceTypeID.SelectedValue);

                List<PDMS_WarrantyClaimInvoice> SOIs = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(null, DealerCode, Year, Month, MonthRange, InvoiceTypeID, "", cbNotAccounted.Checked, cbEInvoiceGenerated.Checked);

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

                gvClaimInvoice.PageIndex = 0;
                gvClaimInvoice.DataSource = SOIs;
                gvClaimInvoice.DataBind();
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
                    lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "fillWarrantyInvoice", e1);
                throw e1;
            }
        }
        void FillYear()
        {
            ddlYear.Items.Add(new ListItem("All", "0"));
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

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageIndex > 0)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex - 1;
                gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvClaimInvoice.PageCount > gvClaimInvoice.PageIndex)
            {
                gvClaimInvoice.PageIndex = gvClaimInvoice.PageIndex + 1;
                gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
                gvClaimInvoice.DataBind();
                lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            gvClaimInvoice.DataSource = SDMS_WarrantyClaimHeader;
            gvClaimInvoice.DataBind();
            lblRowCount.Text = (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + 1) + " - " + (((gvClaimInvoice.PageIndex) * gvClaimInvoice.PageSize) + gvClaimInvoice.Rows.Count) + " of " + SDMS_WarrantyClaimHeader.Count;

        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string supplierPOID = Convert.ToString(gvClaimInvoice.DataKeys[e.Row.RowIndex].Value);
                    GridView gvClaimInvoiceItem = (GridView)e.Row.FindControl("gvClaimInvoiceItem");

                    //Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    //GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    //gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                    //gvFileAttached.DataBind();


                    List<PDMS_WarrantyClaimInvoiceItem> Lines = new List<PDMS_WarrantyClaimInvoiceItem>();
                    Lines = SDMS_WarrantyClaimHeader.Find(s => s.WarrantyClaimInvoiceID == Convert.ToInt64(supplierPOID)).InvoiceItems;

                    gvClaimInvoiceItem.DataSource = Lines;

                    gvClaimInvoiceItem.DataBind();
                    //if(Lines.Count !=0)
                    //{
                    //    if(Lines[0].CGST==0)
                    //    {
                    //        gvClaimInvoiceItem.Columns[4].Visible = false;
                    //        gvClaimInvoiceItem.Columns[5].Visible = false;
                    //        gvClaimInvoiceItem.Columns[7].Visible = false;
                    //        gvClaimInvoiceItem.Columns[8].Visible = false;
                    //    }
                    //    else
                    //    {
                    //        gvClaimInvoiceItem.Columns[6].Visible = false;
                    //        gvClaimInvoiceItem.Columns[9].Visible = false;
                    //    }
                    //}

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
                Label lblWarrantyClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblWarrantyClaimInvoiceID");

                PDMS_WarrantyClaimInvoice SOIs = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text), "", null, null, null, null, "")[0];

                foreach (PDMS_WarrantyClaimInvoiceItem inv in SOIs.InvoiceItems)
                {
                    if (string.IsNullOrEmpty(inv.HSNCode))
                    {
                        lblMessage.Text = "HSN Code Missed. Please contact admin";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                        return;
                    }
                    else if (inv.CGSTValue + inv.SGSTValue + inv.IGSTValue == 0)
                    {
                        lblMessage.Text = "GST Value Missed. Please contact admin";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                        return;
                    }
                }
                if ((SOIs.Dealer.IsEInvoice) && (SOIs.Dealer.EInvoiceDate <= SOIs.InvoiceDate))
                {
                    if (string.IsNullOrEmpty(SOIs.IRN))
                    {
                        PDMS_EInvoiceSigned EInvoiceSigned = new BDMS_EInvoice().getWarrantyClaimInvoiceESigned(SOIs.WarrantyClaimInvoiceID);
                        if (!string.IsNullOrEmpty(EInvoiceSigned.Comments))
                        {
                            lblMessage.Text = EInvoiceSigned.Comments;
                        }
                        else
                        {
                            lblMessage.Text = "E Invoice Not generated.";
                        }
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                        return;
                    }
                }


                PAttachedFile UploadedFile = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceFile(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text));
                if (UploadedFile == null)
                {
                    UploadedFile = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceFile(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text));
                    //PDMS_WarrantyClaimInvoice SOIs = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text), "", null, null, null, null, "")[0];
                    //if (SOIs.InvoiceType.InvoiceTypeID == 3)
                    //{
                    //    new BDMS_WarrantyClaimInvoice().insertWarrantyClaimInvoiceFile(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text), InvoiceAbove50K(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text)));
                    //    UploadedFile = new BDMS_WarrantyClaimInvoice().GetWarrantyClaimInvoiceFile(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text));
                    //}

                }

                Response.AddHeader("Content-type", UploadedFile.FileType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + UploadedFile.FileName + "." + UploadedFile.FileType);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                new BXcel().PdfDowload();
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }
        protected void btnExportExcelForSAP_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Dealer_Code");
            dt.Columns.Add("Invoice_Date");
            dt.Columns.Add("Invoice_Number");
            dt.Columns.Add("Material");
            dt.Columns.Add("Taxable_Value");
            dt.Columns.Add("Total");

            dt.Columns.Add("BP_Code");
            dt.Columns.Add("BP_Name");
            dt.Columns.Add("IC_Ticket");
            dt.Columns.Add("Machine_Serial_No");
            dt.Columns.Add("HSN_Code");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Model");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Remark");
            dt.Columns.Add("Annexure No");
            dt.Columns.Add("Period");
            dt.Columns.Add("TCS Amt.");

            dt.Columns.Add("IRN No.");
            dt.Columns.Add("IRN DATE");


            dt.Columns.Add("CGST %");
            dt.Columns.Add("CGST Value");
            dt.Columns.Add("SGST %");
            dt.Columns.Add("SGST Value");
            dt.Columns.Add("IGST %");
            dt.Columns.Add("IGST Value");

            dt.Columns.Add("SAP Doc");
            dt.Columns.Add("AE Inv. Accounted Date");
            dt.Columns.Add("Payment Voucher. No");
            dt.Columns.Add("Payment Date");
            dt.Columns.Add("Payment Value");
            dt.Columns.Add("TDS Value");
            try
            {
                foreach (PDMS_WarrantyClaimInvoice M in SDMS_WarrantyClaimHeader)
                {
                    int i = 0;
                    List<PDMS_WarrantyClaimAnnexureItem> AnnexureItemS = new List<PDMS_WarrantyClaimAnnexureItem>();
                    if (!string.IsNullOrEmpty(M.AnnexureNumber))
                    {
                        AnnexureItemS = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureByID(null, null, M.AnnexureNumber)[0].AnnexureItems;
                    }

                    foreach (PDMS_WarrantyClaimInvoiceItem Item in M.InvoiceItems)
                    {
                        i = 1 + i;
                        //  PDMS_WarrantyInvoiceHeader SOIs = new BDMS_WarrantyClaim().GetWarrantyClaimReport(txtICServiceTicket.Text.Trim(), ICTicketDateF, ICTicketDateT, txtClaimNumber.Text.Trim(), ClaimDateF, ClaimDateT, DealerCode, StatusID, ApprovedDateF, ApprovedDateT, txtTSIRNumber.Text.Trim(), false, PSession.User.UserID)[0];
                        PDMS_WarrantyClaimAnnexureItem AnnexureItem = new PDMS_WarrantyClaimAnnexureItem();
                        if ((Item.WarrantyClaimAnnexureItemID != 0) && (AnnexureItemS.Count > 0))
                        {
                            //  AnnexureItem = new BDMS_WarrantyClaimAnnexure().GetWarrantyClaimAnnexureByID(null, Item.WarrantyClaimAnnexureItemID)[0].AnnexureItems[0];
                            //   AnnexureItem = (PDMS_WarrantyClaimAnnexureItem)AnnexureItemS.Where(m => m.WarrantyClaimAnnexureItemID == Item.WarrantyClaimAnnexureItemID);
                            //  var SOIs1 = (from S in AnnexureItemS where (S.WarrantyClaimAnnexureItemID == Item.WarrantyClaimAnnexureItemID) select new { S }).ToList();
                            //       AnnexureItem = (PDMS_WarrantyClaimAnnexureItem) SOIs1;

                            var M1 = (from m in AnnexureItemS where m.WarrantyClaimAnnexureItemID == Item.WarrantyClaimAnnexureItemID select m);
                            if (M1.Count() == 1)
                            {
                                AnnexureItem.CustomerCode = M1.ToList()[0].CustomerCode;
                                AnnexureItem.CustomerName = M1.ToList()[0].CustomerName;
                                AnnexureItem.ICTicketID = M1.ToList()[0].ICTicketID;
                                AnnexureItem.MachineSerialNumber = M1.ToList()[0].MachineSerialNumber;
                                AnnexureItem.Model = M1.ToList()[0].Model;
                                AnnexureItem.HMR = M1.ToList()[0].HMR;
                                AnnexureItem.ICTicket = M1.ToList()[0].ICTicket;
                            }
                        }

                        dt.Rows.Add(
                             string.Format("80{0}", M.Dealer.DealerCode.Substring(2))
                            , ((DateTime)M.InvoiceDate).ToShortDateString()
                            , M.InvoiceNumber
                            , "'" + Item.Material
                            , Item.TaxableValue
                            , Item.TaxableValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue
                            , AnnexureItem.CustomerCode
                            , AnnexureItem.CustomerName
                            , AnnexureItem.ICTicketID
                            , AnnexureItem.MachineSerialNumber
                            , Item.HSNCode
                            , ""
                            , AnnexureItem.Model
                            , AnnexureItem.HMR
                            , AnnexureItem.ICTicket == null ? "" : AnnexureItem.ICTicket.ServiceType.ServiceType
                            , M.AnnexureNumber
                            , ((DateTime)M.PeriodFrom).ToShortDateString() + " - " + ((DateTime)M.PeriodTo).ToShortDateString()
                            , i == 1 ? Convert.ToString(M.TCSValue) : ""
                            , M.IRN
                            , M.IRNDate == null ? "" : ((DateTime)M.IRNDate).ToShortDateString()
                            , Item.CGST
                            , Item.CGSTValue
                            , Item.SGST
                            , Item.SGSTValue
                            , Item.IGST
                            , Item.IGSTValue
                            , M.SAPDoc
                            , M.SAPPostingDate == null ? "" : ((DateTime)M.SAPPostingDate).ToShortDateString()
                            , M.SAPClearingDocument
                            , M.SAPClearingDate == null ? "" : ((DateTime)M.SAPClearingDate).ToShortDateString()
                            , M.SAPInvoiceValue
                            , M.SAPInvoiceTDSValue);
                    };
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "btnExportExcelForSAP_Click", ex);
            }
            new BXcel().ExporttoExcel(dt, "Claim Invoice Report");
        }
        protected void btnGenerateEInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblWarrantyClaimInvoiceID = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblWarrantyClaimInvoiceID");

                PDMS_WarrantyClaimInvoice SOIs = new BDMS_WarrantyClaimInvoice().getWarrantyClaimInvoice(Convert.ToInt64(lblWarrantyClaimInvoiceID.Text), "", null, null, null, null, "")[0];

                foreach (PDMS_WarrantyClaimInvoiceItem inv in SOIs.InvoiceItems)
                {
                    if (string.IsNullOrEmpty(inv.HSNCode))
                    {
                        lblMessage.Text = "HSN Code Missed. Please contact admin";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                        return;
                    }
                    else if (inv.CGSTValue + inv.SGSTValue + inv.IGSTValue == 0)
                    {
                        lblMessage.Text = "GST Value Missed. Please contact admin";
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                        return;
                    }
                }
                if ((SOIs.Dealer.IsEInvoice) && (SOIs.Dealer.EInvoiceDate <= SOIs.InvoiceDate))
                {
                    if (string.IsNullOrEmpty(SOIs.IRN))
                    {
                        lblMessage.Visible = true;
                        try
                        {
                            PDealer Dealer = new BDealer().GetDealerByID(null, SOIs.Dealer.DealerCode);

                            Label InvoiceNumber = (Label)gvClaimInvoice.Rows[gvRow.RowIndex].FindControl("lblBillingDocument");
                            //PApiEInv ul = new PApiEInv();
                            //ul.handle = Dealer.EInvUserAPI.Handle;
                            //ul.handleType = Dealer.EInvUserAPI.HandleType;
                            //ul.password = Dealer.EInvUserAPI.Password;

                           //string AccessToken = new JavaScriptSerializer().Deserialize<PData>(new JavaScriptSerializer().Serialize(new JavaScriptSerializer().Deserialize<PApiResult>(new BAPI().GetAccessToken(ul)).Data)).token;

                           // string AccessToken1 =  JsonConvert.DeserializeObject<PData>(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult1>(new BApiEInv().GetAccessToken(ul)).Data)).token;
                            //new BDMS_EInvoice().GeneratEInvoiceUsingAPI(InvoiceNumber.Text, AccessToken);
                            lblMessage.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Visible = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class PApiResult1
    {
        public PData Data { get; set; }
    }
    public class PData
    {
        public string token { get; set; }
    }
}