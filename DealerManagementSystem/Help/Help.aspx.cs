using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
                Boolean Success = false;
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
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
                help.VideoLink = txtVideoLink;
                help.OrderNo = Convert.ToInt32(txtOrderNo);
                help.IsDeleted = false;
                help.CreatedBy = PSession.User.UserID;

                if (BtnAdd.Text == "Add")
                {
                    string fileName = Path.GetFileName(fileUploadPDF.PostedFile.FileName);
                    if (fileUploadPDF.PostedFile.FileName.Length != 0)
                    {
                        help.PDFAttachment = "~/Help/HelpDoc.aspx?aFileName=../Help/Files/" + fileUploadPDF.FileName;                        
                        if (File.Exists(Server.MapPath("~/Help/Files/") + fileName))
                        {
                            File.Delete(Server.MapPath("~/Help/Files/") + fileName);
                        }
                        fileUploadPDF.PostedFile.SaveAs(Server.MapPath("~/Help/Files/") + fileName);
                    }
                    fileName = Path.GetFileName(fileUploadPPS.PostedFile.FileName);
                    if (fileUploadPPS.PostedFile.FileName.Length != 0)
                    {
                        help.PPSAttachment = "Files/" + fileUploadPPS.FileName;
                        if (File.Exists(Server.MapPath("~/Help/Files/") + fileName))
                        {
                            File.Delete(Server.MapPath("~/Help/Files/") + fileName);
                        }
                        fileUploadPPS.PostedFile.SaveAs(Server.MapPath("~/Help/Files/") + fileName);
                    }
                    Success = new BHelp().InsertOrUpdateDocumentAttachment(help);
                    if (Success == true)
                    {
                        lblMessage.Text = "Document is saved successfully";
                        lblMessage.ForeColor = Color.Green;
                        SearchHelp();
                    }
                    else
                    {
                        lblMessage.Text = "Document is not saved successfully";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                else if (BtnAdd.Text == "Update")
                {
                    help.DocumentAttachmentID = Convert.ToInt32(HiddenID.Value);
                    help.PDFAttachment = (fileUploadPDF.PostedFile.FileName.Length != 0) ? "~/Help/HelpDoc.aspx?aFileName=../Help/Files/" + fileUploadPDF.FileName : HiddenFieldpdf.Value;
                    string fileName = Path.GetFileName(fileUploadPDF.PostedFile.FileName);
                    if (fileUploadPDF.PostedFile.FileName.Length != 0)
                    {
                        if (File.Exists(Server.MapPath("~/Help/Files/") + fileName))
                        {
                            File.Delete(Server.MapPath("~/Help/Files/") + fileName);
                        }
                        fileUploadPDF.PostedFile.SaveAs(Server.MapPath("~/Help/Files/") + fileName);
                    }

                    help.PPSAttachment = (fileUploadPPS.PostedFile.FileName.Length != 0) ? "Files/" + fileUploadPPS.FileName : HiddenFieldpps.Value;
                    fileName = Path.GetFileName(fileUploadPPS.PostedFile.FileName);
                    if (fileUploadPPS.PostedFile.FileName.Length != 0)
                    {
                        if (File.Exists(Server.MapPath("~/Help/Files/") + fileName))
                        {
                            File.Delete(Server.MapPath("~/Help/Files/") + fileName);
                        }
                        fileUploadPPS.PostedFile.SaveAs(Server.MapPath("~/Help/Files/") + fileName);
                    }

                    Success = new BHelp().InsertOrUpdateDocumentAttachment(help);
                    if (Success == true)
                    {
                        lblMessage.Text = "Document is updated successfully";
                        lblMessage.ForeColor = Color.Green;
                        SearchHelp();
                    }
                    else
                    {
                        lblMessage.Text = "Document is not updated successfully";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void ibedit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)Ibtn.NamingContainer;

            Label lblsno = (Label)gvRow.FindControl("lblsno");
            Label lblDescription = (Label)gvRow.FindControl("lblDescription");
            HyperLink HyperLinkpdf = (HyperLink)gvRow.FindControl("HyperLinkpdf");
            HyperLink HyperLinkpps = (HyperLink)gvRow.FindControl("HyperLinkpps");
            HyperLink HyperLinklink = (HyperLink)gvRow.FindControl("HyperLinklink");
            Label lblOrderNo = (Label)gvRow.FindControl("lblOrderNo");

            ((TextBox)gvDocument.FooterRow.FindControl("txtsno")).Text = lblsno.Text;
            ((TextBox)gvDocument.FooterRow.FindControl("txtDescription")).Text = lblDescription.Text;
            ((TextBox)gvDocument.FooterRow.FindControl("txtVideoLink")).Text = HyperLinklink.NavigateUrl;
            ((TextBox)gvDocument.FooterRow.FindControl("txtOrderNo")).Text = lblOrderNo.Text;

            ((Button)gvDocument.FooterRow.FindControl("BtnAdd")).Text = "Update";
            HiddenFieldpdf.Value = HyperLinkpdf.NavigateUrl;
            HiddenFieldpps.Value = HyperLinkpps.NavigateUrl;
            HiddenID.Value = Ibtn.CommandArgument;
        }
    }
}