using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewFinance
{
    public partial class DealerBalanceConfirmationUpdate : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewFinance_DealerBalanceConfirmationUpdate; } }
        private List<PAttachedFile_Azure> AttchedFile
        {
            get
            {
                if (ViewState["AttchedFileDealerBalanceConfirmationUpdate"] == null)
                {
                    ViewState["AttchedFileDealerBalanceConfirmationUpdate"] = new List<PAttachedFile_Azure>();
                }
                return (List<PAttachedFile_Azure>)ViewState["AttchedFileDealerBalanceConfirmationUpdate"];
            }
        }
        private DataTable DealerBalanceConfirmationToUpdate
        {
            get
            {
                if (ViewState["DealerBalanceConfirmationToUpdate"] == null)
                {
                    ViewState["DealerBalanceConfirmationToUpdate"] = new DataTable();
                }
                return (DataTable)ViewState["DealerBalanceConfirmationToUpdate"];
            }
            set
            {
                ViewState["DealerBalanceConfirmationToUpdate"] = value;
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
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Finance » Dealer Balance Confirmation Update');</script>");
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new DDLBind(ddlBalanceConfirmationStatus, new BDMS_Master().GetAjaxOneStatus(2), "Status", "StatusID");
            }


        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillDealerBalanceConfirmationToUpdate();
        }
        void FillDealerBalanceConfirmationToUpdate()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            string DateFrom = txtFromDate.Text.Trim();
            string DateTo = txtToDate.Text.Trim();
            int? BalanceConfirmationStatusID = ddlBalanceConfirmationStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlBalanceConfirmationStatus.SelectedValue);

            DealerBalanceConfirmationToUpdate = new BDealerBusiness().GetDealerBalanceConfirmationToUpdate(DealerID, BalanceConfirmationStatusID, DateFrom, DateTo);
            gvDealerBalanceConfirmation.DataSource = DealerBalanceConfirmationToUpdate;
            gvDealerBalanceConfirmation.DataBind();

            if (DealerBalanceConfirmationToUpdate.Rows.Count == 0)
            {
                lblRowCountDealerBalConfirm.Visible = false;
                ibtnDealerBalConfirmArrowLeft.Visible = false;
                ibtnDealerBalConfirmArrowRight.Visible = false;
            }
            else
            {
                lblRowCountDealerBalConfirm.Visible = true;
                ibtnDealerBalConfirmArrowLeft.Visible = true;
                ibtnDealerBalConfirmArrowRight.Visible = true;
                DealerBalanceConfirmationToUpdateBind(gvDealerBalanceConfirmation, lblRowCountDealerBalConfirm, DealerBalanceConfirmationToUpdate);
            }
        }
        void DealerBalanceConfirmationToUpdateBind(GridView gv, Label lbl, DataTable DealerBC)
        {
            gv.DataSource = DealerBC;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerBC.Rows.Count;
        }
        protected void gvDealerBalanceConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerBalanceConfirmation.PageIndex = e.NewPageIndex;
            FillDealerBalanceConfirmationToUpdate();
            gvDealerBalanceConfirmation.DataBind();
        }
        protected void ibtnDealerBalConfirmArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerBalanceConfirmation.PageIndex > 0)
            {
                gvDealerBalanceConfirmation.PageIndex = gvDealerBalanceConfirmation.PageIndex - 1;
                DealerBalanceConfirmationToUpdateBind(gvDealerBalanceConfirmation, lblRowCountDealerBalConfirm, DealerBalanceConfirmationToUpdate);
            }
        }
        protected void ibtnDealerBalConfirmArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerBalanceConfirmation.PageCount > gvDealerBalanceConfirmation.PageIndex)
            {
                gvDealerBalanceConfirmation.PageIndex = gvDealerBalanceConfirmation.PageIndex + 1;
                DealerBalanceConfirmationToUpdateBind(gvDealerBalanceConfirmation, lblRowCountDealerBalConfirm, DealerBalanceConfirmationToUpdate);
            }
        }
        protected void lnkBtnBalanceConfirmationEdit_Click(object sender, EventArgs e)
        {
            MPE_Edit.Show();
            LinkButton lnkBtnBalanceConfirmationEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkBtnBalanceConfirmationEdit.NamingContainer);
            new DDLBind(ddlBalanceConfirmationStatusG, new BDMS_Master().GetAjaxOneStatus(2), "Status", "StatusID");
            Label lblDealerBalanceConfirmationIDGV = (Label)row.FindControl("lblDealerBalanceConfirmationID");
            lblDealerBalanceConfirmationID.Text = lblDealerBalanceConfirmationIDGV.Text;

            txtVendorBalanceAsPerDealer.Text = "";
             txtCustomerBalanceAsPerDealer.Text = "";
            txtTotalOutstandingAsPerDealer.Text = ""; 
            AttchedFile.Clear();
            //Label lblVendorBalanceAsPerDealer = (Label)row.FindControl("lblVendorBalanceAsPerDealer");
            //Label lblCustomerBalanceAsPerDealer = (Label)row.FindControl("lblCustomerBalanceAsPerDealer");
            //Label lblTotalOutstandingAsPerDealer = (Label)row.FindControl("lblTotalOutstandingAsPerDealer");


            //Label lblBalanceConfirmationStatusG = (Label)row.FindControl("lblBalanceConfirmationStatusG");
            //// Label lblBalanceConfirmationStatusIDG = (Label)row.FindControl("lblBalanceConfirmationStatusIDG");

            //TextBox txtVendorBalanceAsPerDealer = (TextBox)row.FindControl("txtVendorBalanceAsPerDealer");
            //TextBox txtCustomerBalanceAsPerDealer = (TextBox)row.FindControl("txtCustomerBalanceAsPerDealer");
            //TextBox txtTotalOutstandingAsPerDealer = (TextBox)row.FindControl("txtTotalOutstandingAsPerDealer");


            //DropDownList ddlBalanceConfirmationStatusG = (DropDownList)row.FindControl("ddlBalanceConfirmationStatusG");

            //lblVendorBalanceAsPerDealer.Visible = false;
            //lblCustomerBalanceAsPerDealer.Visible = false;
            //lblTotalOutstandingAsPerDealer.Visible = false;

            //lblBalanceConfirmationStatusG.Visible = false;

            //txtTotalOutstandingAsPerDealer.Text = lblTotalOutstandingAsPerDealer.Text;


            //txtVendorBalanceAsPerDealer.Visible = true;
            //txtCustomerBalanceAsPerDealer.Visible = true;
            //txtTotalOutstandingAsPerDealer.Visible = true;


            //new DDLBind(ddlBalanceConfirmationStatusG, new BDMS_Master().GetAjaxOneStatus(2), "Status", "StatusID");
            //ddlBalanceConfirmationStatusG.Visible = true;

            //Button btnUpdateBalanceConfirmation = (Button)row.FindControl("btnUpdateBalanceConfirmation");
            //Button btnBack = (Button)row.FindControl("btnBack");
            //btnUpdateBalanceConfirmation.Visible = true;
            //btnBack.Visible = true;
            //lnkBtnBalanceConfirmationEdit.Visible = false;
        }
        
        protected void lblAttachedFileAddR_Click(object sender, EventArgs e)
        {
            MPE_Edit.Show();
            lblMessage.Visible = true; 
            if (fu.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            string ext = System.IO.Path.GetExtension(fu.PostedFile.FileName).ToLower();

            AttchedFile.Add(CreateUploadedFileFSR(fu.PostedFile));
            gvAttachedFile.DataSource = AttchedFile;
            gvAttachedFile.DataBind();
           
        }
        protected void lblAttachedFileRemoveR_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            GridView Parentgrid = (GridView)(gvRow.Parent.Parent);
            long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

            GridViewRow GParentrow = (GridViewRow)(Parentgrid.NamingContainer);
            int GParentRowIndex = GParentrow.RowIndex;

            PDMS_TSIRAttachedFile__M AttachedFile = new PDMS_TSIRAttachedFile__M();
            AttachedFile.AttachedFileID = AttachedFileID;
            AttachedFile.IsDeleted = true;

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("ICTicketTsir/AddOrRemoveTsirAttachment", AttachedFile));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "File Removed";
            lblMessage.ForeColor = Color.Green;
            try
            {
                //List<PDMS_TSIRAttachedFile> UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileDetails(SDMS_ICTicket.ICTicketID, Convert.ToInt64(gvTSIR.DataKeys[GParentRowIndex].Value), null);
                //Parentgrid.DataSource = UploadedFile;
                //Parentgrid.DataBind();
            }
            catch (Exception ex)
            { }
        }
        protected void lnkDownloadR_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;
                GridView Parentgrid = (GridView)(gvRow.NamingContainer);


                long AttachedFileID = Convert.ToInt64(Parentgrid.DataKeys[gvRow.RowIndex].Value);

                PAttachedFile UploadedFile = new BDMS_ICTicketTSIR().GetICTicketTSIRAttachedFileForDownload(AttachedFileID);
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
        private PAttachedFile_Azure CreateUploadedFileFSR(HttpPostedFile file)
        {

            PAttachedFile_Azure AttachedFile = new PAttachedFile_Azure();
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);
            AttachedFile.FileName = name;

            AttachedFile.FileType = file.ContentType;

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.AttachedFileID = 0;
            return AttachedFile;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            MPE_Edit.Show();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            txtTotalOutstandingAsPerDealer.BorderColor = Color.Silver;
            ddlBalanceConfirmationStatusG.BorderColor = Color.Silver;

            if (string.IsNullOrEmpty(txtVendorBalanceAsPerDealer.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Vendor Balance.";
                txtTotalOutstandingAsPerDealer.BorderColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtCustomerBalanceAsPerDealer.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Customer Balance.";
                txtTotalOutstandingAsPerDealer.BorderColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtTotalOutstandingAsPerDealer.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Total Outstanding Amount.";
                txtTotalOutstandingAsPerDealer.BorderColor = Color.Red;
                return;
            }
            if (ddlBalanceConfirmationStatusG.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Balance Confirmation Status.";
                ddlBalanceConfirmationStatusG.BorderColor = Color.Red;
                return;
            }

            decimal x = 0;
            if (!decimal.TryParse(txtTotalOutstandingAsPerDealer.Text.Trim(), out x))
            {
                lblMessage.Text = "Please enter the Amount in Total Outstanding As Per Dealer.";
                txtTotalOutstandingAsPerDealer.BorderColor = Color.Red;
                return;
            }

            string endPoint = "DealerBusiness/UpdateDealerBalanceConfirmation";
            PDealerBalanceConfirmation_Post Dealer = new PDealerBalanceConfirmation_Post();
            Dealer.DealerBalanceConfirmationID = Convert.ToInt64(lblDealerBalanceConfirmationID.Text);
            Dealer.VendorBalanceAsPerDealer = Convert.ToDecimal(txtVendorBalanceAsPerDealer.Text);
            Dealer.CustomerBalanceAsPerDealer = Convert.ToDecimal(txtCustomerBalanceAsPerDealer.Text);
            Dealer.TotalOutstandingAsPerDealer = Convert.ToDecimal(txtTotalOutstandingAsPerDealer.Text);
            Dealer.BalanceConfirmationStatusID = Convert.ToInt32(ddlBalanceConfirmationStatusG.SelectedValue);
            Dealer.AttachedFile = new List<PAttachedFile_Azure>();
            Dealer.AttachedFile = AttchedFile;
            PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut(endPoint, Dealer));

            if (result.Status == PApplication.Failure)
            {
                lblMessage.Text = "Dealer Balance Confirmation is not updated successfully.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Dealer Balance Confirmation is updated successfully.";
            }
            MPE_Edit.Hide();
        }
    }
}