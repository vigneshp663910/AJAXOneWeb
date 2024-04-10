using Business;
using ClosedXML.Excel; 
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewInventory.UserControls
{
    public partial class CreatePhysicalInventoryPosting : System.Web.UI.UserControl
    {
        public List<PPhysicalInventoryPosting_Post> MaterialUpload
        {
            get
            {
                if (ViewState["CreatePhysicalInventoryPosting"] == null)
                {
                    ViewState["CreatePhysicalInventoryPosting"] = new List<PPhysicalInventoryPosting_Post>();
                }
                return (List<PPhysicalInventoryPosting_Post>)ViewState["CreatePhysicalInventoryPosting"];
            }
            set
            {
                ViewState["CreatePhysicalInventoryPosting"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        public void FillMaster()
        {
            new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            //  new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            // ddlCountry.SelectedValue = "1";
            // new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            // new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            Clear();
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDealerOffice, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null), "OfficeName", "OfficeID");
        }
        void Clear()
        {
            txtDocumentNumber.Text = string.Empty;
            txtDocumentDate.Text = string.Empty; 
            new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            new DDLBind(ddlDealerOffice, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null), "OfficeName", "OfficeID");
            new DDLBind(ddlPostingInventoryType, new BDMS_Master().GetAjaxOneStatus((short)AjaxOneStatusHeader.PostingInventoryType), "Status", "StatusID");
        }
        
        public string Validation()
        {
            //txtCustomerName.BorderColor = Color.Silver; 
            //txtMobile.BorderColor = Color.Silver;
            //ddlProductType.BorderColor = Color.Silver;
            //ddlSource.BorderColor = Color.Silver;
            //ddlCountry.BorderColor = Color.Silver;
            //ddlState.BorderColor = Color.Silver;
            //ddlDistrict.BorderColor = Color.Silver;
            //txtNextFollowUpDate.BorderColor = Color.Silver;
            string Message = "";
            //if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            //{
            //    txtCustomerName.BorderColor = Color.Red;
            //    return "Please enter the Customer Name...!";
            //}
            //if (string.IsNullOrEmpty(txtNextFollowUpDate.Text.Trim()))
            //{
            //    txtNextFollowUpDate.BorderColor = Color.Red;
            //    return "Please select the Next FollowUp Date.!";
            //}
            //if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            //{
            //    txtMobile.BorderColor = Color.Red;
            //    return "Please Enter the Mobile...!";
            //}
            //if (txtMobile.Text.Trim().Length != 10)
            //{
            //    txtMobile.BorderColor = Color.Red;
            //    return "Mobile Length should be 10 digit";
            //}
            
            return Message;
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            MaterialUpload.Clear();
            GVUpload.DataSource = MaterialUpload;
            GVUpload.DataBind();
            lblMessage.ForeColor = System.Drawing.Color.Red;
            if (ddlDealer.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer";
                return;
            }
            if (ddlDealerOffice.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer Office";
                return;
            }
            if (string.IsNullOrEmpty(txtDocumentNumber.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Document Number";
                return;
            }
            if (string.IsNullOrEmpty(txtDocumentDate.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Document Date";
                return;
            }
            if (ddlPostingInventoryType.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Posting Inventory Type";
                return;
            }
            if (IsPostBack && fileUpload.PostedFile != null)
            {
                if (fileUpload.PostedFile.FileName.Length > 0)
                {
                    FillUpload();
                }
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            PApiResult Result = new BInventory().InsertDealerPhysicalInventoryPosting(MaterialUpload);
            lblMessage.Text = Result.Message;
            if (Result.Status == PApplication.Failure)
            { 
                return;
            } 
            lblMessage.ForeColor = System.Drawing.Color.Green; 
        }

        
        private Boolean FillUpload()
        {
            Boolean Success = true;
            if (fileUpload.HasFile == true)
            {
                using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                {
                    ddlDealer.Enabled = false;
                    ddlDealerOffice.Enabled = false;
                    ddlPostingInventoryType.Enabled = false;
                    txtDocumentNumber.Enabled = false;
                    txtDocumentDate.Enabled = false; 

                    int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                    int OfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);

                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Loop through the Worksheet rows.
                    int sno = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    { 
                        sno += 1;
                        if (sno > 1)
                        {
                            List<IXLCell> IXLCell_ = row.Cells().ToList();

                            if (IXLCell_.Count != 0)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(IXLCell_[0].Value)))
                                {
                                    continue;
                                }
                                MaterialUpload.Add(new PPhysicalInventoryPosting_Post()
                                {
                                    ID = Convert.ToInt32(IXLCell_[0].Value),
                                    DealerID = DealerID,
                                    OfficeID = OfficeID,
                                    DocumentDate = Convert.ToDateTime(txtDocumentDate.Text.Trim()),
                                    DocumentNumber = txtDocumentNumber.Text.Trim(),
                                    MaterialCode = Convert.ToString(IXLCell_[1].Value),
                                    PhysicalStock = Convert.ToDecimal(IXLCell_[3].Value),
                                    PostingTypeID = Convert.ToInt32(ddlPostingInventoryType.SelectedValue),
                                    ReasonOfPosting = txtReasonOfPosting.Text.Trim()

                                });
                            }
                        }
                    }
                   
                    PApiResult Result = new BInventory().GetDealerStock(DealerID, OfficeID, null, null, null, null, null);

                    List<PDealerStock> DealerStocks = JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data)); 
                    foreach (PPhysicalInventoryPosting_Post dr in MaterialUpload)
                    {
                        List<PDealerStock> DealerStock = DealerStocks.Where(s => s.Material.MaterialCode == dr.MaterialCode).ToList();
                        if(DealerStock.Count==0)
                        {
                            lblMessage.Text = "Please Check Material Code : " + dr.MaterialCode + " Not Available...!";  
                            return false;
                        }
                        dr.MaterialID = DealerStock[0].Material.MaterialID;
                        dr.MaterialDescription = DealerStock[0].Material.MaterialDescription;
                        dr.SystemStock = DealerStock[0].UnrestrictedQty;
                        dr.DeferenceQuantity = dr.SystemStock - dr.PhysicalStock;
                    }
                    if (MaterialUpload.Count > 0)
                    {
                        GVUpload.DataSource = MaterialUpload;
                        GVUpload.DataBind();
                    }
                    lblTotalMaterial.Text = MaterialUpload.Count.ToString();
                }
            }
            else
            {
                lblMessage.Text = "Please Upload the File...!";  
                return false;
            }
            return Success;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;

            if (ddlDealer.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer";
                return;
            }
            if (ddlDealerOffice.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer Office";
                return;
            }
            if (ddlPostingInventoryType.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Posting Inventory Type";
                return;
            }
            int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            int OfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
            PApiResult Result = new BInventory().GetDealerStock(DealerID, OfficeID, null, null, null, null, null);

            List<PDealerStock> DealerStocks = JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data));

            string Name = Server.MapPath("~") + "Template/PhysicalInventoryPostingTemplate" + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".xlsx";
            try
            {

                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(Name, SpreadsheetDocumentType.Workbook);
                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "mySheet"
                };
                sheets.Append(sheet);
                Worksheet worksheet = new Worksheet();
                SheetData sheetData = new SheetData();
                Row row = new Row();

                row = new Row() { RowIndex = 1U, Spans = new ListValue<StringValue>() };
                Cell cell = new Cell();

                cell = new Cell()
                {
                    CellReference = "A1", // Location of Cell
                    DataType = CellValues.String,
                    CellValue = new CellValue("ID") // Setting Cell Value
                };
                row.Append(cell);
                row.Append(new Cell() { CellReference = "B1", DataType = CellValues.String, CellValue = new CellValue("MaterialCode") });
                row.Append(new Cell() { CellReference = "C1", DataType = CellValues.String, CellValue = new CellValue("SystemStock") });
                row.Append(new Cell() { CellReference = "D1", DataType = CellValues.String, CellValue = new CellValue("PhysicalStock") });
                sheetData.Append(row);
                int ExcelRow = 1;

                foreach (PDealerStock S in DealerStocks)
                {
                    ExcelRow = ExcelRow + 1;
                    row = new Row() { Spans = new ListValue<StringValue>() };
                    row.Append(new Cell() { CellReference = "A" + ExcelRow.ToString(), DataType = CellValues.String, CellValue = new CellValue(ExcelRow - 1) });
                    row.Append(new Cell() { CellReference = "B" + ExcelRow.ToString(), DataType = CellValues.String, CellValue = new CellValue(S.Material.MaterialCode) });
                    row.Append(new Cell() { CellReference = "C" + ExcelRow.ToString(), DataType = CellValues.String, CellValue = new CellValue(S.UnrestrictedQty) });
                    //row.Append(new Cell() { CellReference = "D" + ExcelRow.ToString(), DataType = CellValues.String, CellValue = new CellValue("PhysicalStock") });
                    sheetData.Append(row);
                }


                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                if (!Directory.Exists(Server.MapPath("~") + "/Template"))
                {
                    Directory.CreateDirectory(Server.MapPath("~") + "/Template");
                }
                workbookpart.Workbook.Save();
                // Close the document.
                spreadsheetDocument.Close(); 


                //string Path = Server.MapPath("Templates\\InitialStock.xlsx");
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"PhysicalInventoryPostingTemplate.xlsx\"");
                byte[] data = req.DownloadData(Name);
                response.BinaryWrite(data);
                // Append cookie
                HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
                cookie.Value = "Flag";
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.AppendCookie(cookie);
                // end
                response.End();
                //  new BXcel().ExporttoExcel(dt, "PhysicalInventoryPostingTemplate");
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
            finally
            {
                if (File.Exists(Name))
                {
                    File.Delete(Name);
                }
            }
        }
        //protected void btnDownload_Click(object sender, EventArgs e)
        //{

        //    if (ddlDealer.SelectedValue == "0")
        //    {
        //        lblMessage.Text = "Please select the Dealer";
        //        return;
        //    }
        //    if (ddlDealerOffice.SelectedValue == "0")
        //    {
        //        lblMessage.Text = "Please select the Dealer Office";
        //        return;
        //    }
        //    if (ddlPostingInventoryType.SelectedValue == "0")
        //    {
        //        lblMessage.Text = "Please select the Posting Inventory Type";
        //        return;
        //    }
        //    int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
        //    int OfficeID = Convert.ToInt32(ddlDealerOffice.SelectedValue);
        //    PApiResult Result = new BInventory().GetDealerStock(DealerID, OfficeID, null, null, null, null, null);

        //    List<PDealerStock> DealerStocks = JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data));
        //    //DataTable dt = new DataTable();
        //    //dt.Columns.Add("ID");
        //    //dt.Columns.Add("MaterialCode");
        //    //dt.Columns.Add("SystemStock");
        //    //dt.Columns.Add("PhysicalStock");
        //    //int i = 0;
        //    //foreach (PDealerStock S in DealerStocks)
        //    //{
        //    //    i = i + 1;
        //    //    if ((short)AjaxOneStatus.PostingInventoryType_PostingUnrestricted == Convert.ToInt32(ddlPostingInventoryType.SelectedValue))
        //    //    {
        //    //        dt.Rows.Add(i,S.Material.MaterialCode, S.UnrestrictedQty, "");
        //    //    }
        //    //    else if ((short)AjaxOneStatus.PostingInventoryType_PostingRestricted == Convert.ToInt32(ddlPostingInventoryType.SelectedValue))
        //    //    {
        //    //        dt.Rows.Add(i, S.Material.MaterialCode, S.RestrictedQty, "");
        //    //    }
        //    //    else if ((short)AjaxOneStatus.PostingInventoryType_PostingBlocked == Convert.ToInt32(ddlPostingInventoryType.SelectedValue))
        //    //    {
        //    //        dt.Rows.Add(i, S.Material.MaterialCode, S.BlockedQty, "");
        //    //    }
        //    //}
        //    int i = 0;
        //    string Name = Server.MapPath("~") + "Template/PhysicalInventoryPostingTemplate" + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".xlsx";
        //    try
        //    { 
        //        var wb = new XLWorkbook();
        //        var worksheet = wb.Worksheets.Add("SINS");
        //        worksheet.Column(1).DataType = XLDataType.Text;
        //        worksheet.Column(2).DataType = XLDataType.Text;
        //        worksheet.Cell(1, 1).Value = "ID";
        //        worksheet.Cell(1, 2).Value = "MaterialCode";
        //        worksheet.Cell(1, 3).Value = "SystemStock";
        //        worksheet.Cell(1, 4).Value = "PhysicalStock";
        //        foreach (PDealerStock S in DealerStocks)
        //        {
        //            i = i + 1;
        //            if ((short)AjaxOneStatus.PostingInventoryType_PostingUnrestricted == Convert.ToInt32(ddlPostingInventoryType.SelectedValue))
        //            {
        //                worksheet.Cell(i + 1, 1).Value = i; 
        //                worksheet.Cell(i + 1, 2).Value = S.Material.MaterialCode.ToString();
        //                worksheet.Cell(i + 1, 3).Value = S.UnrestrictedQty; 
        //            }
        //            else if ((short)AjaxOneStatus.PostingInventoryType_PostingRestricted == Convert.ToInt32(ddlPostingInventoryType.SelectedValue))
        //            {
        //                worksheet.Cell(i + 1, 1).Value = i;
        //                worksheet.Cell(i + 1, 2).Value = S.Material.MaterialCode;
        //                worksheet.Cell(i + 1, 3).Value = S.RestrictedQty; 
        //            }
        //            else if ((short)AjaxOneStatus.PostingInventoryType_PostingBlocked == Convert.ToInt32(ddlPostingInventoryType.SelectedValue))
        //            {
        //                worksheet.Cell(i + 1, 1).Value = i;
        //                worksheet.Cell(i + 1, 2).Value = S.Material.MaterialCode;
        //                worksheet.Cell(i + 1, 3).Value = S.BlockedQty; 
        //            }
        //        } 
        //        if (!Directory.Exists(Server.MapPath("~") + "/Template"))
        //        {
        //            Directory.CreateDirectory(Server.MapPath("~") + "/Template");
        //        }               
        //        wb.SaveAs(Name);

        //        //string Path = Server.MapPath("Templates\\InitialStock.xlsx");
        //        WebClient req = new WebClient();
        //        HttpResponse response = HttpContext.Current.Response;
        //        response.Clear();
        //        response.ClearContent();
        //        response.ClearHeaders();
        //        response.Buffer = true;
        //        response.AddHeader("Content-Disposition", "attachment;filename=\"PhysicalInventoryPostingTemplate.xlsx\"");
        //        byte[] data = req.DownloadData(Name);
        //        response.BinaryWrite(data);
        //        // Append cookie
        //        HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
        //        cookie.Value = "Flag";
        //        cookie.Expires = DateTime.Now.AddDays(1);
        //        HttpContext.Current.Response.AppendCookie(cookie);
        //        // end
        //        response.End();
        //        //  new BXcel().ExporttoExcel(dt, "PhysicalInventoryPostingTemplate");
        //    }
        //    catch(Exception e1)
        //    {
        //    }
        //    finally
        //    {
        //        if (File.Exists(Name))
        //        {
        //            File.Delete(Name);
        //        }
        //    }
        //}
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}