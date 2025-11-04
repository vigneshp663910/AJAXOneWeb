using Business;
using ClosedXML.Excel;
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
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcAssemblyMaster : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcAssemblyMaster; } }

        int? ModelID = null;
        int? SpcProductGroupID = null;
        Boolean? Isactive = null;
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        public List<PSpcAssembly_Insert> Assembly_Insert
        {
            get
            {
                if (ViewState["Assembly_Insert"] == null)
                {
                    ViewState["Assembly_Insert"] = new List<PSpcAssembly_Insert>();
                }
                return (List<PSpcAssembly_Insert>)ViewState["Assembly_Insert"];
            }
            set
            {
                ViewState["Assembly_Insert"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Assembly Master');</script>");
            lblMessage.Text = "";
            lblAssemblyMessage.Text = "";
            lblMessageAssemblyUpload.Text = ""; 
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;

                new DDLBind(ddlProductGroup, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, " All");
                //new DDLBind(ddlModel, new BECatalogue().GetSpcModel(null, null, null, true), "SpcModel", "SpcModelID", true, "Select Model");
                new DDLBind(ddlModel, new BECatalogue().GetSpcModel(null, null, null, true, null), "SpcModelCodeWithDescription", "SpcModelID", true, "All");

                fillAssembly();

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcAssemblyMaster_CreateAndEditAssembly).Count() != 0)
                {
                    btnCreateAssembly.Visible = true;
                }
                else
                {
                    btnCreateAssembly.Visible = false;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                fillAssembly();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void Search()
        {
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            ModelID = ddlModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModel.SelectedValue);
            Isactive = ddlIsActive.SelectedValue == "-1" ? (Boolean?)null : Convert.ToBoolean(Convert.ToInt32(ddlIsActive.SelectedValue));
        }
        void fillAssembly()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BECatalogue().GetSpcAssembly(SpcProductGroupID, ModelID, null, txtAssemblyCode.Text.Trim(), Isactive, 0, PageIndex, gvAssembly.PageSize);
                List<PSpcAssembly> Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
                gvAssembly.PageIndex = 0;
                gvAssembly.DataSource = Assembly;
                gvAssembly.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvAssembly.PageSize - 1) / gvAssembly.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvAssembly.PageSize) + 1) + " - " + (((PageIndex - 1) * gvAssembly.PageSize) + gvAssembly.Rows.Count) + " of " + Result.RowCount;
                }

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcAssemblyMaster_CreateAndEditAssembly).Count() != 0)
                {
                    for (int i = 0; i < gvAssembly.Rows.Count; i++)
                        ((LinkButton)gvAssembly.Rows[i].FindControl("lblEditAssembly")).Visible = true;
                }
                else
                {
                    for (int i = 0; i < gvAssembly.Rows.Count; i++)
                        ((LinkButton)gvAssembly.Rows[i].FindControl("lblEditAssembly")).Visible = false;
                }
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.SpcAssemblyMaster_DeleteAssembly).Count() != 0)
                {
                    for (int i = 0; i < gvAssembly.Rows.Count; i++)
                        ((LinkButton)gvAssembly.Rows[i].FindControl("lblDeleteAssembly")).Visible = true;
                }
                else
                {
                    for (int i = 0; i < gvAssembly.Rows.Count; i++)
                        ((LinkButton)gvAssembly.Rows[i].FindControl("lblDeleteAssembly")).Visible = false;
                }

            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SpcAssemblyMaster", "fillAssembly", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillAssembly();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillAssembly();
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            Search();
            PApiResult Result = new BECatalogue().GetSpcAssembly(SpcProductGroupID, ModelID, null, txtAssemblyCode.Text.Trim(), Isactive, 1, PageIndex, gvAssembly.PageSize);
            DataTable Assembly = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data));
            new BXcel().ExporttoExcel(Assembly, "Assembly List");
        }

        protected void ddlProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpcProductGroupID = ddlProductGroup.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProductGroup.SelectedValue);
            new DDLBind(ddlModel, new BECatalogue().GetSpcModel(null, SpcProductGroupID, null, true, null), "SpcModelCodeWithDescription", "SpcModelID", true, "Select Model");
        }

        protected void btnAssemblySave_Click(object sender, EventArgs e)
        {
            lblAssemblyMessage.ForeColor = Color.Red;
            MPE_AssemblyCreate.Show();
            try
            {
                int number;
                if (ddlProductGroupC.SelectedValue == "0")
                {
                    lblAssemblyMessage.Text = "Please select Product Group.";
                    return;
                }
                else if (ddlModelC.SelectedValue == "0")
                {
                    lblAssemblyMessage.Text = "Please select Model / PM Code.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtSlNoC.Text.Trim()))
                {
                    lblAssemblyMessage.Text = "Please enter SlNo.";
                    return;
                }
                else if (!int.TryParse(txtSlNoC.Text.Trim(), out number))
                {
                    lblAssemblyMessage.Text = "Please enter valid SlNo.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtAssemblyCodeC.Text.Trim()))
                {
                    lblAssemblyMessage.Text = "Please enter Assembly Code.";
                    return;
                }
                else if (string.IsNullOrEmpty(txtAssemblyDescriptionC.Text.Trim()))
                {
                    lblAssemblyMessage.Text = "Please enter Assembly Description.";
                    return;
                }

                PSpcAssembly_Insert Assembly = new PSpcAssembly_Insert();
                Assembly.SpcAssemblyID = Convert.ToInt32(lblSpcAssemblyID.Text);
                Assembly.ModelID = Convert.ToInt32(ddlModelC.SelectedValue);
                Assembly.SlNo = Convert.ToInt32(txtSlNoC.Text.Trim());
                Assembly.AssemblyCode = txtAssemblyCodeC.Text.Trim();
                Assembly.AssemblyDescription = txtAssemblyDescriptionC.Text.Trim();
                Assembly.AssemblyType = ddlAssemblyTypeC.SelectedValue;
                Assembly.Remarks = txtRemarksC.Text.Trim();
                Assembly.IsActive = cbIsActiveC.Checked;
                PApiResult Results = new BECatalogue().InsertorUpdateSpcAssembly(Assembly);
                if (Results.Status == PApplication.Failure)
                {
                    lblAssemblyMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillAssembly();
            }
            catch (Exception ex)
            {
                lblAssemblyMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_AssemblyCreate.Hide();
        }
        protected void btnCreateAssembly_Click(object sender, EventArgs e)
        {
            MPE_AssemblyCreate.Show();
            new DDLBind(ddlProductGroupC, new BECatalogue().GetSpcProductGroup(null, null, true), "PGSCodePGDescription", "SpcProductGroupID", true, " Select");
            ddlProductGroupC_SelectedIndexChanged(null, null);
            lblSpcAssemblyID.Text = "0";
            txtSlNoC.Text = "";
            txtAssemblyCodeC.Text = "";
            txtAssemblyDescriptionC.Text = "";
            txtRemarksC.Text = "";

            ddlAssemblyTypeC.SelectedValue = "Common";
            cbIsActiveC.Checked = true;
        }
        protected void ddlProductGroupC_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_AssemblyCreate.Show();
            int ProductGroupCID = Convert.ToInt32(ddlProductGroupC.SelectedValue);
            new DDLBind(ddlModelC, new BECatalogue().GetSpcModel(null, ProductGroupCID, null, true, null), "SpcModel", "SpcModelID", true, "Select Model");
        }


        protected void lblEditAssembly_Click(object sender, EventArgs e)
        {
            MPE_AssemblyCreate.Show();
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblID = (Label)gvRow.FindControl("lblSpcAssemblyID");
            lblSpcAssemblyID.Text = lblID.Text;
            PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, Convert.ToInt32(lblID.Text), "", null, 0);
            List<PSpcAssembly> Assemblys = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
            PSpcAssembly Assembly = Assemblys[0];

            new DDLBind(ddlProductGroupC, new BECatalogue().GetSpcProductGroup(null, null, null), "PGSCodePGDescription", "SpcProductGroupID", true, " Select");
            ddlProductGroupC.SelectedValue = Convert.ToString(Assembly.SpcModel.ProductGroup.SpcProductGroupID);
            new DDLBind(ddlModelC, new BECatalogue().GetSpcModel(null, Assembly.SpcModel.ProductGroup.SpcProductGroupID, null, null, null), "SpcModel", "SpcModelID", true, "Select Model");
            ddlModelC.SelectedValue = Convert.ToString(Assembly.SpcModel.SpcModelID);

            txtSlNoC.Text = Convert.ToString(Assembly.SlNo);
            txtAssemblyCodeC.Text = Assembly.AssemblyCode;
            txtAssemblyDescriptionC.Text = Assembly.AssemblyDescription;
            txtRemarksC.Text = Assembly.Remarks;
            ddlAssemblyTypeC.SelectedValue = Assembly.AssemblyType;
            cbIsActiveC.Checked = Assembly.IsActive;
        }

        protected void lnkBtnDeleteAssembly_Click(object sender, EventArgs e)
        {

            try
            {
                lblMessage.ForeColor = Color.Red;
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                PApiResult Results = new BECatalogue().UpdateSpcAssemblyDelete(Convert.ToInt32(((Label)gvRow.FindControl("lblSpcAssemblyID")).Text));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillAssembly();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }



        protected void ibPDF_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblSpcAssemblyID = (Label)gvAssembly.Rows[index].FindControl("lblSpcAssemblyID");
            Fdf(Convert.ToInt32(lblSpcAssemblyID.Text));
        }

        void Fdf(int SpcAssemblyID)
        {
            try
            {
                PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, SpcAssemblyID, "", null, 0);
                List<PSpcAssembly> Assemblys = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));

                DataTable AssemblyDT = new DataTable();
                AssemblyDT.Columns.Add("ProductGroup");
                AssemblyDT.Columns.Add("Model");
                AssemblyDT.Columns.Add("AssemblyCode");
                AssemblyDT.Columns.Add("AssemblyID");
                AssemblyDT.Columns.Add("Assembly");
                AssemblyDT.Columns.Add("Image");
                AssemblyDT.Columns.Add("SNO");
                AssemblyDT.Columns.Add("ALT");
                AssemblyDT.Columns.Add("PartNo");
                AssemblyDT.Columns.Add("Description");
                AssemblyDT.Columns.Add("Qty");
                foreach (PSpcAssembly Ass in Assemblys)
                {
                    string Path1 = new Uri(Server.MapPath("~/" + Ass.FileName)).AbsoluteUri;

                    //string Url1 = "~/ICTickrtFSR_Files/Org/" + F1.AttachedFileID + "." + F1.FileName.Split('.')[F1.FileName.Split('.').Count() - 1];

                    //if (File.Exists(MapPath(Url1)))
                    //{
                    //    File.Delete(MapPath(Url1));
                    //}
                    //File.WriteAllBytes(MapPath(Url1), F1.AttachedFile);


                    PApiResult ResultParts = new BECatalogue().GetAssemblyPartsCoOrdinate(null, Ass.SpcAssemblyID);
                    List<PSpcAssemblyPartsCoOrdinate> PartsCoOrdinate = JsonConvert.DeserializeObject<List<PSpcAssemblyPartsCoOrdinate>>(JsonConvert.SerializeObject(ResultParts.Data));

                    foreach (PSpcAssemblyPartsCoOrdinate Mat in PartsCoOrdinate)
                    {
                        AssemblyDT.Rows.Add(Ass.SpcModel.ProductGroup.PGCode, Ass.SpcModel.SpcModel, Ass.AssemblyCode, Ass.SpcAssemblyID, Ass.AssemblyDescription
                          , Path1, Mat.Number, Mat.Flag, Mat.Material.Material, Mat.Material.MaterialDescription, Mat.Qty);
                    }
                }



                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "TSIR_" + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                report.ReportPath = Server.MapPath("~/Print/Spc.rdlc");

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "Assembly";//This refers to the dataset name in the RDLC file  
                rds.Value = AssemblyDT;
                report.DataSources.Add(rds);



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

        protected void lblViewImage_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            Label lblSpcModelCode = (Label)gvAssembly.Rows[index].FindControl("lblSpcModelCode");
            Label lblAssemblyCode = (Label)gvAssembly.Rows[index].FindControl("lblAssemblyCode");
            Label lblSpcAssemblyDescription = (Label)gvAssembly.Rows[index].FindControl("lblSpcAssemblyDescription");
            lblDPmCode.Text = lblSpcModelCode.Text;
            lblDAssemblyCode.Text = lblAssemblyCode.Text;
            lblDAssemblyDescription.Text = lblSpcAssemblyDescription.Text;

            Label lblFileName = (Label)gvAssembly.Rows[index].FindControl("lblFileName");

            if (!string.IsNullOrEmpty(lblFileName.Text))
            {
                new BECatalogue().DowloadSpcFile(lblFileName.Text);
                Session["filePath"] = lblFileName.Text;
                imgAssemblyImage.ImageUrl = "UserControls\\ImageHandlerECatalogue.ashx?file=example.jpg";
            }
            MPE_AssemblyImage.Show();
        }

        protected void lbChangeAssemblyDrawing_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblSpcAssemblyID = (Label)gvAssembly.Rows[index].FindControl("lblSpcAssemblyID");
            Label lblSpcModelCode = (Label)gvAssembly.Rows[index].FindControl("lblSpcModelCode");
            Label lblAssemblyCode = (Label)gvAssembly.Rows[index].FindControl("lblAssemblyCode");

            lblDSpcAssemblyID.Text = lblSpcAssemblyID.Text.Trim();
            lblDSpcModelCode.Text = lblSpcModelCode.Text.Trim();
            lblDSpcAssemblyCode.Text = lblAssemblyCode.Text.Trim();

            MPE_AssemblyDrawing.Show();
        }
        protected void btnAssemblyDrawingSave_Click(object sender, EventArgs e)
        {
            lblDrawingMessage.ForeColor = Color.Red;
            MPE_AssemblyDrawing.Show();
            try
            {
                if (fuAssemblyDrawing.PostedFile.FileName.Length == 0)
                {
                    lblDrawingMessage.Text = "Please select the file...!";
                    return;
                }

                PAttachedFile_Azure File = CreateUploadedFileFSR(fuAssemblyDrawing.PostedFile);
                PApiResult Results = new BECatalogue().UploadSpcFile(File);
                if (Results.Status == PApplication.Failure)
                {
                    lblDrawingMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
            }
            catch (Exception ex)
            {
                lblDrawingMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_AssemblyDrawing.Hide();
        }
        private PAttachedFile_Azure CreateUploadedFileFSR(HttpPostedFile file)
        {
            PAttachedFile_Azure AttachedFile = new PAttachedFile_Azure();
            int size = file.ContentLength;

            AttachedFile.FileName = lblDSpcModelCode.Text + "_" + lblDSpcAssemblyCode.Text + "_" + lblDSpcAssemblyID.Text + Path.GetExtension(file.FileName);
            AttachedFile.FileType = file.ContentType;
            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.AttachedFileID = Convert.ToInt64(lblDSpcAssemblyID.Text);
            return AttachedFile;
        }

        protected void btnViewExcel_Click(object sender, EventArgs e)
        {
            lblMessageAssemblyUpload.Text = "";
            lblMessageAssemblyUpload.ForeColor = Color.Red;
            if (IsPostBack && fileUpload.PostedFile != null)
            {
                if (fileUpload.PostedFile.FileName.Length > 0)
                {
                    bool Success = FillUpload();
                    if (Success)
                    {
                        BtnSaveExcel.Visible = true;
                        btnViewExcel.Visible = false;
                        btnDownload.Visible = false;
                        btnBack.Visible = true;
                        fileUpload.Visible = false;
                    }
                }
                else
                {
                    lblMessageAssemblyUpload.Text = "Please upload file.";
                }
            }
            else
            {
                lblMessageAssemblyUpload.Text = "Please upload file.";
            }
            MPE_AssemblyUpload.Show();
        }
        private Boolean FillUpload()
        {
            Boolean Success = false;
            lblMessageAssemblyUpload.ForeColor = Color.Red;
            if (fileUpload.HasFile == true)
            {
                PApiResult ResultMo = new BECatalogue().GetSpcModelWithResult(null, null, null, null, true, 0, null, null);
                List<PSpcModel> Models = JsonConvert.DeserializeObject<List<PSpcModel>>(JsonConvert.SerializeObject(ResultMo.Data));

                PApiResult ResultAC = new BECatalogue().GetSpcAssembly(null, null, null, null, true, 0, null, null);
                List<PSpcAssembly> AssemblyCode = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(ResultAC.Data));

                using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable DTAssemblyMasterUpload = new DataTable();
                    Assembly_Insert = new List<PSpcAssembly_Insert>();
                    //Loop through the Worksheet rows.
                    int Lineno = 0;
                    foreach (IXLRow row in workSheet.RowsUsed())
                    {
                        Lineno += 1;
                        //Use the first row to add columns to DataTable.
                        if (Lineno == 1)
                        {
                            DTAssemblyMasterUpload.Columns.Add(row.Cell(1).Value.ToString(), typeof(int));
                            DTAssemblyMasterUpload.Columns.Add(row.Cell(2).Value.ToString(), typeof(string));
                            DTAssemblyMasterUpload.Columns.Add(row.Cell(3).Value.ToString(), typeof(string));
                            DTAssemblyMasterUpload.Columns.Add(row.Cell(4).Value.ToString(), typeof(string));
                            DTAssemblyMasterUpload.Columns.Add(row.Cell(5).Value.ToString(), typeof(string));
                            DTAssemblyMasterUpload.Columns.Add(row.Cell(6).Value.ToString(), typeof(string));
                        }
                        else if (Lineno > 1)
                        {
                            //Add rows to DataTable.
                            //DTAssemblyMasterUpload.Rows.Add();
                            if (string.IsNullOrEmpty(row.Cell(1).Value.ToString()) || string.IsNullOrEmpty(row.Cell(2).Value.ToString()) || string.IsNullOrEmpty(row.Cell(3).Value.ToString()) || string.IsNullOrEmpty(row.Cell(4).Value.ToString()))
                            {
                                lblMessageAssemblyUpload.Text = "Please enter data in the empty cell in Excel.";
                                return Success = false;
                            }
                            PSpcAssembly_Insert AssemblyInsert = new PSpcAssembly_Insert();
                            AssemblyInsert.SlNo = Convert.ToInt32(row.Cell(1).Value);

                            List<PSpcModel> Model = Models.Where(item => item.SpcModelCode == row.Cell(2).Value.ToString()).ToList();

                            if (Model.Count != 1)
                            {
                                lblMessageAssemblyUpload.Text = "The model was not found in the master record.";
                                return Success = false;
                            }
                            AssemblyInsert.ModelID = Model[0].SpcModelID;

                            bool containsItem = AssemblyCode.Any(item => item.SpcModel.SpcModelID == AssemblyInsert.ModelID && item.AssemblyCode == row.Cell(3).Value.ToString());
                            if (containsItem)
                            {
                                lblMessageAssemblyUpload.Text = "Model and Assembly Code already exist in the system.";
                                Success = false;
                                return Success;
                            }

                            AssemblyInsert.AssemblyCode = row.Cell(3).Value.ToString();
                            AssemblyInsert.AssemblyDescription = row.Cell(4).Value.ToString();
                            AssemblyInsert.AssemblyType = row.Cell(5).Value.ToString();
                            AssemblyInsert.Remarks = row.Cell(6).Value.ToString();
                            AssemblyInsert.IsActive = true;
                            Assembly_Insert.Add(AssemblyInsert);
                            DTAssemblyMasterUpload.Rows.Add(Convert.ToInt32(row.Cell(1).Value), row.Cell(2).Value.ToString(), row.Cell(3).Value.ToString(), row.Cell(4).Value.ToString(), row.Cell(5).Value.ToString(), row.Cell(6).Value.ToString());
                        }
                    }
                    var duplicateValues = Assembly_Insert.GroupBy(x => x.AssemblyCode)
                                .Where(g => g.Count() > 1)
                                .Select(g => g.Key)
                                .ToList();
                    if (duplicateValues.Count > 0)
                    {
                        lblMessageAssemblyUpload.Text = "Duplicates detected in Excel data.";
                        Assembly_Insert = null;
                        DTAssemblyMasterUpload = null;
                        return Success = false;
                    }
                    if (DTAssemblyMasterUpload.Rows.Count > 0)
                    {
                        GVUpload.DataSource = DTAssemblyMasterUpload;
                        GVUpload.DataBind();
                        return Success = true;                        
                    }
                }
            }
            else
            {
                lblMessageAssemblyUpload.Text = "Please Upload the File.";
                return Success = false;
            }
            return Success;
        }
        protected void BtnSaveExcel_Click(object sender, EventArgs e)
        {
            lblMessageAssemblyUpload.ForeColor = Color.Red;
            MPE_AssemblyUpload.Show();
            try
            {
                foreach (PSpcAssembly_Insert Ass in Assembly_Insert)
                {
                    PApiResult Results = new BECatalogue().InsertorUpdateSpcAssembly(Ass);
                    if (Results.Status == PApplication.Failure)
                    {
                        lblMessageAssemblyUpload.Text = Results.Message;
                        return;
                    }
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = Results.Message;
                    lblMessageAssemblyUpload.ForeColor = Color.Green;
                    lblMessageAssemblyUpload.Text = Results.Message;
                }
                
                fillAssembly();
                Clear();
                ActionControlMange();
            }
            catch (Exception ex)
            {
                lblMessageAssemblyUpload.Text = ex.Message.ToString();
                MPE_AssemblyUpload.Show();
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            Clear();
            ActionControlMange(); 
            MPE_AssemblyUpload.Show();
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string Path = Server.MapPath("..\\Templates\\AssemblyMasterTemplate.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"AssemblyMasterTemplate.xlsx\"");
            byte[] data = req.DownloadData(Path);
            response.BinaryWrite(data);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end
            response.End();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Clear();
            ActionControlMange();
            MPE_AssemblyUpload.Show();
        }
        void Clear()
        {
            Assembly_Insert = new List<PSpcAssembly_Insert>();
            GVUpload.DataSource = new DataTable();
            GVUpload.DataBind();
        }
        void ActionControlMange()
        {
            fileUpload.Visible = true;
            BtnSaveExcel.Visible = false;
            btnBack.Visible = false;
            btnViewExcel.Visible = true;
            btnDownload.Visible = true;
        }
    }
}