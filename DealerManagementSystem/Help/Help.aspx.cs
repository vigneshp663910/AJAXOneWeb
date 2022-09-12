using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DealerManagementSystem.Help
{
    public partial class Help : System.Web.UI.Page
    {
        string PreviousModuleRowID = string.Empty;
        int GridRowindex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Help » Contents');</script>");
                SearchHelp();
            }            
        }
        void SearchHelp()
        {

            List<PHelp> helps = new BHelp().GetDocumentAttachment(null);

            gvDocument.DataSource = helps;
            gvDocument.DataBind();
            if (helps.Count == 0)
            {
                PHelp help = new PHelp();
                helps.Add(help);
                gvDocument.DataSource = helps;
                gvDocument.DataBind();
            }


            if (helps.Count == 0)
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
                lblRowCount.Text = (((gvDocument.PageIndex) * gvDocument.PageSize) + 1) + " - " + (((gvDocument.PageIndex) * gvDocument.PageSize) + gvDocument.Rows.Count) + " of " + helps.Count;
            }

        }
        protected void gvDocument_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocument.PageIndex = e.NewPageIndex;
            SearchHelp();
            gvDocument.DataBind();
        }
        protected void ibdelete_Click(object sender, ImageClickEventArgs e)
        {
            Boolean Success = false;
            ImageButton Ibtn = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)Ibtn.NamingContainer;

            Label lblsno = (Label)gvRow.FindControl("lblsno");
            Label lblDescription = (Label)gvRow.FindControl("lblDescription");
            HyperLink HyperLinkpdf = (HyperLink)gvRow.FindControl("HyperLinkpdf");
            HyperLink HyperLinkpps = (HyperLink)gvRow.FindControl("HyperLinkpps");
            HyperLink HyperLinklink = (HyperLink)gvRow.FindControl("HyperLinklink");
            Label lblOrderNo = (Label)gvRow.FindControl("lblOrderNo");

            PHelp help = new PHelp();
            help.DocumentAttachmentID = Convert.ToInt32(Ibtn.CommandArgument);
            help.Sno = lblsno.Text.Trim();
            help.Description = lblDescription.Text.Trim();
            help.PDFAttachment = HyperLinkpdf.NavigateUrl;
            help.PPSAttachment = HyperLinkpps.NavigateUrl;
            help.VideoLink = HyperLinklink.NavigateUrl;
            help.OrderNo = Convert.ToInt32(lblOrderNo.Text.Trim());
            help.IsDeleted = true;
            help.CreatedBy = PSession.User.UserID;

            Success = new BHelp().InsertOrUpdateDocumentAttachment(help);
            if (Success == true)
            {
                lblMessage.Text = "Document is deleted successfully";
                lblMessage.ForeColor = Color.Green;
                SearchHelp();
            }
        }
        protected void gvDocument_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (DataBinder.Eval(e.Row.DataItem, "MainModule.ModuleMasterID") != null)
            {
                if ((PreviousModuleRowID == string.Empty) || (PreviousModuleRowID != DataBinder.Eval(e.Row.DataItem, "MainModule.ModuleMasterID").ToString()))
                {
                    GridView gvDocument = (GridView)sender;
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    TableCell cell = new TableCell();
                    cell.Text = "Application Name : " + DataBinder.Eval(e.Row.DataItem, "MainModule.ModuleName").ToString();
                    cell.ColumnSpan = 7;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    gvDocument.Controls[0].Controls.AddAt(e.Row.RowIndex + GridRowindex, row);
                    GridRowindex++;
                }
                PreviousModuleRowID = DataBinder.Eval(e.Row.DataItem, "MainModule.ModuleMasterID").ToString();
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                int success = 0;
                Button BtnAdd = (Button)gvDocument.FooterRow.FindControl("BtnAdd");
                string txtsno = ((TextBox)gvDocument.FooterRow.FindControl("txtsno")).Text.Trim();
                string txtDescription = ((TextBox)gvDocument.FooterRow.FindControl("txtDescription")).Text.Trim();
                FileUpload fileUploadPDF = ((FileUpload)gvDocument.FooterRow.FindControl("fileUploadPDF"));
                FileUpload fileUploadPPS = ((FileUpload)gvDocument.FooterRow.FindControl("fileUploadPPS"));
                string txtVideoLink = ((TextBox)gvDocument.FooterRow.FindControl("txtVideoLink")).Text.Trim();
                string txtOrderNo = ((TextBox)gvDocument.FooterRow.FindControl("txtOrderNo")).Text.Trim();


                if (string.IsNullOrEmpty(txtDescription))
                {
                    lblMessage.Text = "Please Enter Description";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                PHelp help = new PHelp();
                help.Sno = txtsno.Trim();
                help.Description = txtDescription.Trim();
                if (fileUploadPDF.PostedFile.FileName.Length != 0)
                {
                    help.PDFAttachment = "~/Help/HelpDoc.aspx?aFileName=../Help/Files/" + fileUploadPDF.FileName;
                }
                if (fileUploadPPS.PostedFile.FileName.Length != 0)
                {
                    help.PPSAttachment = "Files/" + fileUploadPPS.FileName;
                }
                help.VideoLink = txtVideoLink;
                help.OrderNo = Convert.ToInt32(txtOrderNo);
                help.IsDeleted = false;
                help.CreatedBy = PSession.User.UserID;
                Boolean Success = false;
                if (BtnAdd.Text == "Add")
                {
                    Success = new BHelp().InsertOrUpdateDocumentAttachment(help);
                    if (Success == true)
                    {
                        lblMessage.Text = "Document is saved successfully";
                        lblMessage.ForeColor = Color.Green;
                        SearchHelp();
                    }

                    //success = new BPresalesMasters().InsertOrUpdateLeadSource(null, LeadSource, true, PSession.User.UserID);
                    //if (success == 1)
                    //{
                    //    SearchHelp();
                    //    lblMessage.Text = "Lead Source Created Successfully...!";
                    //    lblMessage.ForeColor = Color.Green;
                    //    return;
                    //}
                    //else if (success == 2)
                    //{
                    //    lblMessage.Text = "Lead Source Already Found";
                    //    lblMessage.ForeColor = Color.Red;
                    //    return;
                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Lead Source Not Created Successfully...!";
                    //    lblMessage.ForeColor = Color.Red;
                    //    return;
                    //}
                }
                else
                {
                    //success = new BPresalesMasters().InsertOrUpdateLeadSource(Convert.ToInt32(HiddenID.Value), LeadSource, true, PSession.User.UserID);
                    //if (success == 1)
                    //{
                    //    HiddenID.Value = null;
                    //    SearchLeadSource();
                    //    lblMessage.Text = "Lead Source Updated Successfully...!";
                    //    lblMessage.ForeColor = Color.Green;
                    //    return;
                    //}
                    //else if (success == 2)
                    //{
                    //    lblMessage.Text = "Lead Source Already Found";
                    //    lblMessage.ForeColor = Color.Red;
                    //    return;
                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Lead Source Not Updated Successfully...!";
                    //    lblMessage.ForeColor = Color.Red;
                    //    return;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
    }
}