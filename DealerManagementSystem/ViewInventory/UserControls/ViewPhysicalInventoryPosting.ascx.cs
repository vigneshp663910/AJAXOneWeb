using Business;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewInventory.UserControls
{
    public partial class ViewPhysicalInventoryPosting : System.Web.UI.UserControl
    { 
        public long InventoryID
        {
            get
            {
                if (ViewState["PhysicalInventoryPostingID"] == null)
                {
                    ViewState["PhysicalInventoryPostingID"] = 0;
                }
                return (long)ViewState["PhysicalInventoryPostingID"];
            }
            set
            {
                ViewState["PhysicalInventoryPostingID"] = value;
            }
        }
        public long StatusID
        {
            get
            {
                if (ViewState["StatusID"] == null)
                {
                    ViewState["StatusID"] = 0;
                }
                return (long)ViewState["StatusID"];
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        public PPhysicalInventoryPosting PhysicalInventoryPostingByID
        {
            get
            {
                if (ViewState["PhysicalInventoryPostingByID"] == null)
                {
                    ViewState["PhysicalInventoryPostingByID"] = new PPhysicalInventoryPosting();
                }
                return (PPhysicalInventoryPosting)ViewState["PhysicalInventoryPostingByID"];
            }
            set
            {
                ViewState["PhysicalInventoryPostingByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 
        }
        public void fillViewEnquiry(long PhysicalInventoryPostingID)
        {
            InventoryID = PhysicalInventoryPostingID;
            PhysicalInventoryPostingByID = new BInventory().GetDealerPhysicalInventoryPostingByID(PhysicalInventoryPostingID);
            StatusID = PhysicalInventoryPostingByID.Status.StatusID;
            lblDealerCode.Text = PhysicalInventoryPostingByID.Dealer.DealerCode;
            lblDealerName.Text = PhysicalInventoryPostingByID.Dealer.DealerName;
            lblOfficeName.Text = PhysicalInventoryPostingByID.DealerOffice.OfficeName;
            lblDocumentNumber.Text = PhysicalInventoryPostingByID.DocumentNumber;
            lblDocumentDate.Text = PhysicalInventoryPostingByID.DocumentDate.ToString("dd/MM/yyyy");
            lblPostingDate.Text = PhysicalInventoryPostingByID.PostingDate == null ? "" : ((DateTime)PhysicalInventoryPostingByID.PostingDate).ToString("dd/MM/yyyy HH:mm:ss");
            lblPostingBy.Text = PhysicalInventoryPostingByID.PostingBy.ContactName;
            lblCreatedBy.Text = PhysicalInventoryPostingByID.CreatedBy.ContactName;
            lblCreatedOn.Text = PhysicalInventoryPostingByID.CreatedOn.ToString("dd/MM/yyyy");
            lblStatus.Text = PhysicalInventoryPostingByID.Status.Status;
            lblReasonOfPosting.Text = PhysicalInventoryPostingByID.ReasonOfPosting;
            lblInventoryPostingType.Text = PhysicalInventoryPostingByID.InventoryPostingType.Status;
            gvStatusHistory.DataSource = PhysicalInventoryPostingByID.Items;
            gvStatusHistory.DataBind();
            ActionControlMange();
        }    
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbtnPost")
            {
                PApiResult Results = new BInventory().UpdateDealerPhysicalInventoryPosting(InventoryID);

                lblMessage.Text = Results.Message;
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                fillViewEnquiry(InventoryID);
            }
            else if (lbActions.ID == "lbtnViewPDF")
            {
                try
                {
                    ViewPIP();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
            else if (lbActions.ID == "lbtnDownloadPDF")
            {
                try
                {
                    DownloadPIP();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
            }
        }  
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        void ActionControlMange()
        {
            lbtnPost.Visible = true;
            if ((StatusID == (short)AjaxOneStatus.PostingInventoryStatus_Posted))
            {
                lbtnPost.Visible = false;
            } 
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.PostingPhysicalInventory).Count() == 0)
            {
                lbtnPost.Visible = false;
            }
        }
        void ViewPIP()
        {
            try
            {
                string mimeType = string.Empty;
                Byte[] mybytes = PhysicalInventoryPostingRdlc(out mimeType);
                string FileName = PhysicalInventoryPostingByID.DocumentNumber + ".pdf";
                var uploadPath = Server.MapPath("~/Backup");
                var tempfilenameandlocation = Path.Combine(uploadPath, Path.GetFileName(FileName));
                File.WriteAllBytes(tempfilenameandlocation, mybytes);
                Context.Response.Write("<script language='javascript'>window.open('../PDF.aspx?FileName=" + FileName + "&Title=Inventory » Physical Inventory Posting','_newtab');</script>");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        Byte[] PhysicalInventoryPostingRdlc(out string mimeType)
        {
            PPhysicalInventoryPosting PIP = PhysicalInventoryPostingByID;
            var CC = CultureInfo.CurrentCulture;
            Random r = new Random();
            string extension;
            string encoding;
            string[] streams;
            Warning[] warnings;
            LocalReport report = new LocalReport();
            report.EnableExternalImages = true;

            ReportParameter[] P = new ReportParameter[14];
            P[0] = new ReportParameter("DocRef", PIP.DocumentNumber, false);
            P[1] = new ReportParameter("CountDate", PIP.DocumentDate.ToShortDateString(), false);
            P[2] = new ReportParameter("Dealer", PIP.Dealer.DealerName, false);
            P[3] = new ReportParameter("Status", PIP.Status.Status, false);
            P[4] = new ReportParameter("Location", PIP.DealerOffice.OfficeName, false);
            P[5] = new ReportParameter("Book", "", false);
            P[6] = new ReportParameter("Physical", "", false);
            P[7] = new ReportParameter("Diff", "", false);
            P[8] = new ReportParameter("CreatedBy", PIP.CreatedBy.ContactName, false);
            P[9] = new ReportParameter("CreatedDate", PIP.CreatedOn.ToString(), false);
            P[10] = new ReportParameter("ApprovedBy", "", false);
            P[11] = new ReportParameter("ApprovedDate", "", false);
            P[12] = new ReportParameter("PostedBy", PIP.PostingBy.ContactName, false);
            P[13] = new ReportParameter("PostedDate", PIP.PostingDate.ToString(), false);


            DataTable dtItem = new DataTable();
            dtItem.Columns.Add("Sno");
            dtItem.Columns.Add("StockType");
            dtItem.Columns.Add("Material");
            dtItem.Columns.Add("Book");
            dtItem.Columns.Add("Physical");
            dtItem.Columns.Add("Diff");
            dtItem.Columns.Add("Remarks");

            decimal Book = 0, Physical = 0, Diff = 0;
            int sno = 0;
            DataTable DTMaterialText = new DataTable();
            foreach (PPhysicalInventoryPostingItem Item in PIP.Items)
            {
                dtItem.Rows.Add(sno+=1, PIP.InventoryPostingType.Status, Item.Material.MaterialCode + " " + Item.Material.MaterialDescription, Item.SystemStock, Item.PhysicalStock, Item.SystemStock - Item.PhysicalStock, "");
                Book += Item.SystemStock;
                Physical += Item.PhysicalStock;
                Diff += Item.SystemStock - Item.PhysicalStock;
            }
            P[5] = new ReportParameter("Book", Book.ToString(), false);
            P[6] = new ReportParameter("Physical", Physical.ToString(), false);
            P[7] = new ReportParameter("Diff", Diff.ToString(), false);
            report.ReportPath = Server.MapPath("~/Print/PhysicalInventoryPosting.rdlc");
            report.SetParameters(P);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "PhysicalInventoryPosting";
            rds.Value = dtItem;
            report.DataSources.Add(rds);
            Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings);
            return mybytes;
        }
        void DownloadPIP()
        {
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                string FileName = PhysicalInventoryPostingByID.DocumentNumber + ".pdf";
                string mimeType;
                Byte[] mybytes = PhysicalInventoryPostingRdlc(out mimeType);
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
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}