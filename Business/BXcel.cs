 
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Reporting.Map.WebForms;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public class BXcel : System.Web.UI.Page
    {
        //public void ExporttoExcel(DataTable table, string strFile)
        //{
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ClearContent();
        //    HttpContext.Current.Response.ClearHeaders();
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.ContentType = "application/ms-excel";
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
        //    HttpContext.Current.Response.Charset = "utf-16";
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //    HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
        //    HttpContext.Current.Response.Write("<BR><BR><BR>");
        //    HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> <TR>");
        //    int columnscount = table.Columns.Count;

        //    for (int j = 0; j < columnscount; j++)
        //    {
        //        HttpContext.Current.Response.Write("<Td>");
        //        HttpContext.Current.Response.Write("<B>");
        //        HttpContext.Current.Response.Write(table.Columns[j].ToString());
        //        HttpContext.Current.Response.Write("</B>");
        //        HttpContext.Current.Response.Write("</Td>");
        //    }
        //    HttpContext.Current.Response.Write("</TR>");
        //    foreach (DataRow row in table.Rows)
        //    {
        //        HttpContext.Current.Response.Write("<TR>");
        //        for (int i = 0; i < table.Columns.Count; i++)
        //        {
        //            HttpContext.Current.Response.Write("<Td>");
        //            HttpContext.Current.Response.Write(row[i].ToString());
        //            HttpContext.Current.Response.Write("</Td>");
        //        }

        //        HttpContext.Current.Response.Write("</TR>");
        //    }
        //    HttpContext.Current.Response.Write("</Table>");
        //    HttpContext.Current.Response.Write("</font>");

        //    // Append cookie
        //    HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
        //    cookie.Value = "Flag";
        //    cookie.Expires = DateTime.Now.AddDays(1);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //    // end

        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.End();
        //}
        public void ExporttoExcel(DataTable dt, string strFile)
        {
            if (!Directory.Exists(Server.MapPath("~") + "/Templates"))
            {
                Directory.CreateDirectory(Server.MapPath("~") + "/Templates");
            }
            string Name = Server.MapPath("~") + "Templates/" + strFile + PSession.User.UserID.ToString() + DateTime.Now.ToLongTimeString().Replace(':', '_') + ".xlsx";
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(Name, SpreadsheetDocumentType.Workbook);
            try
            {
                

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());
                // Add Sheets to the Workbook.
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                // Append a new worksheet and associate it with the workbook.
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = strFile };
                sheets.Append(sheet);
                Worksheet worksheet = new Worksheet();
                SheetData sheetData = new SheetData();
                Row row = new Row();

                row = new Row() { RowIndex = 1U, Spans = new ListValue<StringValue>() };
                Cell cell = new Cell();
                List<string> ExcelCName = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
                    , "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ"
                    , "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ" };
                int i = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    row.Append(new Cell() { CellReference = ExcelCName[i] + "1", DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) });
                    i = i + 1;
                }
                sheetData.Append(row);
                int ExcelRow = 1;

                foreach (DataRow row1 in dt.Rows)
                {
                    i = 0;
                    ExcelRow = ExcelRow + 1;
                    row = new Row() { Spans = new ListValue<StringValue>() };
                    foreach (DataColumn column in dt.Columns)
                    {
                        row.Append(new Cell() { CellReference = ExcelCName[i] + ExcelRow.ToString(), DataType = CellValues.String, CellValue = new CellValue(Convert.ToString(row1[column])) });
                        i = i + 1;
                    }
                    sheetData.Append(row);
                    
                }

                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
               
                workbookpart.Workbook.Save();
                // Close the document.
              spreadsheetDocument.Close();
             
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"" + strFile + ".xlsx\"");
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
            }
            finally
            {
                //spreadsheetDocument.Close();
                if (File.Exists(Name))
                {
                    File.Delete(Name);
                }
            }
        }
        public void PdfDowload()
        { 
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end 
        }

        public void ExporttoExcelMultipleTable(DataSet ds, string strFile)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            
            foreach (DataTable dt in ds.Tables)
            {
                MultipleTable(dt);
            }

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        public void MultipleTable(DataTable table)
        {
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> <TR>");
           
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
             
        }


        public void ExporttoExcelForLeadDefinedPeriod(DataTable table, string strFile,string FirstLine)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR ><Td  colspan='30' style='background-color: #b4c6e7; text-align: center;' ><B>" + FirstLine + "</B></Td></TR> ");
            HttpContext.Current.Response.Write("<TR > ");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Region</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Code</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Name</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Salesman</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #ddebf7; text-align: center' ><B>Ratio</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #c6e0b4; text-align: center' ><B>Open</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #ddebf7; text-align: center' ><B>Generated</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #c6e0b4; text-align: center' ><B>Win</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #de6614; text-align: center' ><B>Lost</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #f8cbad; text-align: center' ><B>Drop</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #c6e0b4; text-align: center' ><B>Closing</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='5' style='background-color: #ddebf7; text-align: center' ><B>Ageing</B></Td>");
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hit</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Lost</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Drop</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hot</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Warm</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Cold</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hot</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Warm</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Cold</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hot</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Warm</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Cold</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hot</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Warm</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Cold</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hot</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Warm</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Cold</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Hot</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Warm</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Cold</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>0-30</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f8cbad;' ><B>31-60</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f4b084;' ><B>61-90</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f0904e;' ><B>91-180</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #de6614;' ><B>>180</B></Td>");

            HttpContext.Current.Response.Write("</TR>");
            //HttpContext.Current.Response.Write("<TR>");
            //for (int j = 0; j < columnscount; j++)
            //{
            //    HttpContext.Current.Response.Write("<Td>");
            //    HttpContext.Current.Response.Write("<B>");
            //    HttpContext.Current.Response.Write(table.Columns[j].ToString());
            //    HttpContext.Current.Response.Write("</B>");
            //    HttpContext.Current.Response.Write("</Td>");
            //}
            //HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < 30; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }

            // footer

            decimal HitRatioF = 0, LostRatioF = 0, DropRatioF = 0, OpenHotF = 0, OpenWarmF = 0, OpenColdF = 0,
                GeneratedHotF = 0, GeneratedWarmF = 0, GeneratedColdF = 0, WinHotF = 0, WinWarmF = 0, WinColdF = 0,
                LostHotF = 0, LostWarmF = 0, LostColdF = 0, DropHotF = 0, DropWarmF = 0, DropColdF = 0,
                ClosingHotF = 0, ClosingWarmF = 0, ClosingColdF = 0, Age30F = 0, Age60F = 0, Age90F = 0, Age180F = 0, AgeA180F = 0;
            foreach (DataRow dr in table.Rows)
            {  
                OpenHotF = OpenHotF + Convert.ToDecimal(dr["Open Hot"]);
                OpenWarmF = OpenWarmF + Convert.ToDecimal(dr["Open Warm"]);
                OpenColdF = OpenColdF + Convert.ToDecimal(dr["Open Cold"]);

                GeneratedHotF = GeneratedHotF + Convert.ToDecimal(dr["Generated Hot"]);
                GeneratedWarmF = GeneratedWarmF + Convert.ToDecimal(dr["Generated Warm"]);
                GeneratedColdF = GeneratedColdF + Convert.ToDecimal(dr["Generated Cold"]);

                WinHotF = WinHotF + Convert.ToDecimal(dr["Win Hot"]);
                WinWarmF = WinWarmF + Convert.ToDecimal(dr["Win Warm"]);
                WinColdF = WinColdF + Convert.ToDecimal(dr["Win Cold"]);

                LostHotF = LostHotF + Convert.ToDecimal(dr["Lost Hot"]);
                LostWarmF = LostWarmF + Convert.ToDecimal(dr["Lost Warm"]);
                LostColdF = LostColdF + Convert.ToDecimal(dr["Lost Cold"]);

                DropHotF = DropHotF + Convert.ToDecimal(dr["Drop Hot"]);
                DropWarmF = DropWarmF + Convert.ToDecimal(dr["Drop Warm"]);
                DropColdF = DropColdF + Convert.ToDecimal(dr["Drop Cold"]);

                ClosingHotF = ClosingHotF + Convert.ToDecimal(dr["Closing Hot"]);
                ClosingWarmF = ClosingWarmF + Convert.ToDecimal(dr["Closing Warm"]);
                ClosingColdF = ClosingColdF + Convert.ToDecimal(dr["Closing Cold"]);

                Age30F = Age30F + Convert.ToDecimal(dr["Age 0 - 30"]);
                Age60F = Age60F + Convert.ToDecimal(dr["Age 31 - 60"]);
                Age90F = Age90F + Convert.ToDecimal(dr["Age 61 - 90"]);
                Age180F = Age180F + Convert.ToDecimal(dr["Age 91 - 180"]);
                AgeA180F = AgeA180F + Convert.ToDecimal(dr["Age > 180"]);

            }
            decimal Total = (OpenHotF + OpenWarmF + OpenColdF + GeneratedHotF + GeneratedWarmF + GeneratedColdF);
            if (Total > 0)
            {
                HitRatioF = Math.Round((WinHotF + WinWarmF + WinColdF) * 100 / Total,2);
                LostRatioF = Math.Round((LostHotF + LostWarmF + LostColdF) * 100 / Total, 2);
                DropRatioF = Math.Round((DropHotF + DropWarmF + DropColdF) * 100 / Total, 2);
            }

            HttpContext.Current.Response.Write("<TR>");

            HttpContext.Current.Response.Write("<Td colspan='4'><B>Total</Td>"); 

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + HitRatioF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + LostRatioF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + DropRatioF.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + OpenHotF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + OpenWarmF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + OpenColdF.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + GeneratedHotF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + GeneratedWarmF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + GeneratedColdF.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + WinHotF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + WinWarmF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + WinColdF.ToString() + "</B></Td>"); 

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + LostHotF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + LostWarmF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + LostColdF.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + DropHotF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + DropWarmF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + DropColdF.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + ClosingHotF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + ClosingWarmF.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + ClosingColdF.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + Age30F.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f8cbad;' ><B>" + Age60F.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f4b084;' ><B>" + Age90F.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f0904e;' ><B>" + Age180F.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #de6614;' ><B>" + AgeA180F.ToString() + "</B></Td>");




            HttpContext.Current.Response.Write("</TR>"); 


            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public void DealerMissionPlanningReportForPreSales(DataTable table, string strFile, string FirstLine)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR ><Td  colspan='17' style='background-color: #b4c6e7; text-align: center;' ><B>" + FirstLine + "</B></Td></TR> ");
            HttpContext.Current.Response.Write("<TR > ");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Year</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Month</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Code</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Name</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Salesman</B></Td>"); 
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #c6e0b4; text-align: center' ><B>Lead Generation</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #de6614; text-align: center' ><B>Lead Conversion</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #f8cbad; text-align: center' ><B>Quotation Generated</B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #c6e0b4; text-align: center' ><B>Quotation Conversion</B></Td>"); 
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Plan</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Actual</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>%Actual</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Plan</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Actual</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>%Actual</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Plan</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Actual</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>%Actual</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>Plan</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>Actual</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>%Actual</B></Td>"); 

            HttpContext.Current.Response.Write("</TR>"); 
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < 17; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }

            // footer
             

            decimal LeadGenerationPlan = 0, LeadGenerationActual = 0, LeadGenerationActualP = 0,
                 LeadConversionPlan = 0, LeadConversionActual = 0, LeadConversionActualP = 0,
                 QuotationGeneratedPlan = 0, QuotationGeneratedActual = 0, QuotationGeneratedActualP = 0,
                 QuotationConversionPlan = 0, QuotationConversionActual = 0, QuotationConversionActualP = 0;
            foreach (DataRow dr in table.Rows)
            {
                LeadGenerationPlan = LeadGenerationPlan + Convert.ToDecimal(dr["New Lead Generation Plan"]);
                LeadGenerationActual = LeadGenerationActual + Convert.ToDecimal(dr["New Lead Generation Actual"]);

                LeadConversionPlan = LeadConversionPlan + Convert.ToDecimal(dr["Lead Conversion Plan"]);
                LeadConversionActual = LeadConversionActual + Convert.ToDecimal(dr["Lead Conversion Actual"]);

                QuotationGeneratedPlan = QuotationGeneratedPlan + Convert.ToDecimal(dr["Quotation Generated Plan"]);
                QuotationGeneratedActual = QuotationGeneratedActual + Convert.ToDecimal(dr["Quotation Generated Actual"]);

                QuotationConversionPlan = QuotationConversionPlan + Convert.ToDecimal(dr["Quotation Conversion Plan"]);
                QuotationConversionActual = QuotationConversionActual + Convert.ToDecimal(dr["Quotation Conversion Actual"]);
            }
            LeadGenerationActualP = LeadGenerationActual * 100 / LeadGenerationPlan;
            LeadConversionActualP = LeadConversionActual * 100 / LeadConversionPlan;
            QuotationGeneratedActualP = QuotationGeneratedActual * 100 / QuotationGeneratedPlan;
            QuotationConversionActualP = QuotationConversionActual * 100 / QuotationConversionPlan;

            HttpContext.Current.Response.Write("<TR>");

            HttpContext.Current.Response.Write("<Td colspan='5'><B>Total</Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + LeadGenerationPlan.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + LeadGenerationActual.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>" + Math.Round(LeadGenerationActualP, 2).ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + LeadConversionPlan.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + LeadConversionActual.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>" + Math.Round(LeadConversionActualP, 2).ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + QuotationGeneratedPlan.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + QuotationGeneratedActual.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>" + Math.Round(QuotationGeneratedActualP, 2).ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ddebf7;' ><B>" + QuotationConversionPlan.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #c6e0b4;' ><B>" + QuotationConversionActual.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #8dcc62;' ><B>" + Math.Round(QuotationConversionActualP, 2).ToString() + "</B></Td>"); 
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public void ExporttoExcelForEnquiryUnattendedAgeing(DataTable table, string strFile, string FirstLine)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR ><Td  colspan='7' style='background-color: #b4c6e7; text-align: center;' ><B>" + FirstLine + "</B></Td></TR> ");
            HttpContext.Current.Response.Write("<TR > ");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Region</B></Td>"); 
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Code</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Name</B></Td>"); 
            HttpContext.Current.Response.Write("<Td colspan='4' style='background-color: #b4c6e7; text-align: center' ><B>Enquiry Ageing</B></Td>"); 
            HttpContext.Current.Response.Write("</TR>");
            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td style='background-color: #c2d2b6;' ><B>Days < 3</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #a9d08e;' ><B>Days 4 to 6</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #92cf68;' ><B>Days > 6</B></Td>"); 
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>Grand Total</B></Td>"); 

            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < 7; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }

            // footer

            decimal Days0_3 = 0, Days4_6 = 0, DaysGr6 = 0, EnquiryTotal = 0;
            foreach (DataRow dr in table.Rows)
            {
                Days0_3 = Days0_3 + Convert.ToDecimal(dr["Days 0 to 3"]);
                Days4_6 = Days4_6 + Convert.ToDecimal(dr["Days 4 to 6"]); 
                DaysGr6 = DaysGr6 + Convert.ToDecimal(dr["Days > 6"]);
                EnquiryTotal = EnquiryTotal + Convert.ToDecimal(dr["Enquiry Total"]); 
            } 

            HttpContext.Current.Response.Write("<TR>");

            HttpContext.Current.Response.Write("<Td colspan='3'><B>Total</Td>"); 
            HttpContext.Current.Response.Write("<Td style='background-color: #c2d2b6;' ><B>" + Days0_3.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #a9d08e;' ><B>" + Days4_6.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #92cf68;' ><B>" + DaysGr6.ToString() + "</B></Td>"); 
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + EnquiryTotal.ToString() + "</B></Td>"); 
            HttpContext.Current.Response.Write("</TR>"); 
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public void ExporttoExcelForLeadExpectedDateofSaleAgeingReport(DataTable table, string strFile, string FirstLine)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR ><Td  colspan='11' style='background-color: #b4c6e7; text-align: center;' ><B>" + FirstLine + "</B></Td></TR> ");
            HttpContext.Current.Response.Write("<TR > ");
            HttpContext.Current.Response.Write("<Td rowspan='3' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Region</B></Td>"); 
            HttpContext.Current.Response.Write("<Td rowspan='3' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Code</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='3' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Name</B></Td>"); 
            HttpContext.Current.Response.Write("<Td  colspan='8' style='background-color: #c6e0b4; text-align: center' ><B>Expected Date of Sale (EDS) from Today Date - Ageing in Days</B></Td>");
             
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #ff8f8f; text-align: center;' ><B> - Days </B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B> 0 </B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #6dce2b; text-align: center;' ><B> + Days </B></Td>"); 
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #f8cbad; text-align: center; vertical-align: middle;' ><B> Grand Total </B></Td>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td style='background-color: #ff4747;' ><B> < (-60) </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ff8f8f;' ><B> (-31) To (-60) </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ffd1d1;' ><B> (-1) To (-30) </B></Td>"); 

            HttpContext.Current.Response.Write("<Td style='background-color: #67b92f;' ><B> 1 To 30 </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #6dce2b;' ><B> 31 To 60 </B></Td>"); 
            HttpContext.Current.Response.Write("<Td style='background-color: #92cf68;' ><B> > 60 </B></Td>");

            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < 11; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }

            // footer


            decimal DaysLeN60 = 0, DaysN31_60 = 0, DaysN1_30 = 0, Days0 = 0, Days1_30 = 0, Days31_60 = 0, DaysGr60 = 0, Total = 0;
            foreach (DataRow dr in table.Rows)
            {
                DaysLeN60 = DaysLeN60 + Convert.ToDecimal(dr["Days < -60"]);
                DaysN31_60 = DaysN31_60 + Convert.ToDecimal(dr["Days -31 To -60"]);
                DaysN1_30 = DaysN1_30 + Convert.ToDecimal(dr["Days -1 To -30"]);
                Days0 = Days0 + Convert.ToDecimal(dr["Days 0"]);

                Days1_30 = Days1_30 + Convert.ToDecimal(dr["Days 1 To 30"]);
                Days31_60 = Days31_60 + Convert.ToDecimal(dr["Days 31 To 60"]);
                DaysGr60 = DaysGr60 + Convert.ToDecimal(dr["Days > 60"]);
                Total = Total + Convert.ToDecimal(dr["Total"]);
            } 

            HttpContext.Current.Response.Write("<TR>");

            HttpContext.Current.Response.Write("<Td colspan='3'><B>Total</Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ff4747;' ><B>" + DaysLeN60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ff8f8f;' ><B>" + DaysN31_60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ffd1d1;' ><B>" + DaysN1_30.ToString() + "</B></Td>"); 
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + Days0.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #67b92f;' ><B>" + Days1_30.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #6dce2b;' ><B>" + Days31_60.ToString() + "</B></Td>"); 
            HttpContext.Current.Response.Write("<Td style='background-color: #92cf68;' ><B>" + DaysGr60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f8cbad;' ><B>" + Total.ToString() + "</B></Td>"); 
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public void ExporttoExcelForLeadNextFollowUpAgeingReport(DataTable table, string strFile, string FirstLine)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR ><Td  colspan='11' style='background-color: #b4c6e7; text-align: center;' ><B>" + FirstLine + "</B></Td></TR> ");
            HttpContext.Current.Response.Write("<TR > ");
            HttpContext.Current.Response.Write("<Td rowspan='3' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Region</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='3' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Code</B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='3' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Dealer Name</B></Td>");
            HttpContext.Current.Response.Write("<Td  colspan='8' style='background-color: #c6e0b4; text-align: center' ><B>Next Follow Up) from Today Date - Ageing in Days</B></Td>");

            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #ff8f8f; text-align: center;' ><B> - Days </B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B> 0 </B></Td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='background-color: #6dce2b; text-align: center;' ><B> + Days </B></Td>");
            HttpContext.Current.Response.Write("<Td rowspan='2' style='background-color: #f8cbad; text-align: center; vertical-align: middle;' ><B> Grand Total </B></Td>");
            HttpContext.Current.Response.Write("</TR>");

            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td style='background-color: #ff4747;' ><B> < (-60) </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ff8f8f;' ><B> (-31) To (-60) </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ffd1d1;' ><B> (-1) To (-30) </B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #67b92f;' ><B> 1 To 30 </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #6dce2b;' ><B> 31 To 60 </B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #92cf68;' ><B> > 60 </B></Td>");

            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < 11; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }

            // footer


            decimal DaysLeN60 = 0, DaysN31_60 = 0, DaysN1_30 = 0, Days0 = 0, Days1_30 = 0, Days31_60 = 0, DaysGr60 = 0, Total = 0;
            foreach (DataRow dr in table.Rows)
            {
                DaysLeN60 = DaysLeN60 + Convert.ToDecimal(dr["Days < -60"]);
                DaysN31_60 = DaysN31_60 + Convert.ToDecimal(dr["Days -31 To -60"]);
                DaysN1_30 = DaysN1_30 + Convert.ToDecimal(dr["Days -1 To -30"]);
                Days0 = Days0 + Convert.ToDecimal(dr["Days 0"]);

                Days1_30 = Days1_30 + Convert.ToDecimal(dr["Days 1 To 30"]);
                Days31_60 = Days31_60 + Convert.ToDecimal(dr["Days 31 To 60"]);
                DaysGr60 = DaysGr60 + Convert.ToDecimal(dr["Days > 60"]);
                Total = Total + Convert.ToDecimal(dr["Total"]);
            }

            HttpContext.Current.Response.Write("<TR>");

            HttpContext.Current.Response.Write("<Td colspan='3'><B>Total</Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #ff4747;' ><B>" + DaysLeN60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ff8f8f;' ><B>" + DaysN31_60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #ffd1d1;' ><B>" + DaysN1_30.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #fce4d6;' ><B>" + Days0.ToString() + "</B></Td>");

            HttpContext.Current.Response.Write("<Td style='background-color: #67b92f;' ><B>" + Days1_30.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #6dce2b;' ><B>" + Days31_60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #92cf68;' ><B>" + DaysGr60.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("<Td style='background-color: #f8cbad;' ><B>" + Total.ToString() + "</B></Td>");
            HttpContext.Current.Response.Write("</TR>");


            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public void ExporttoExcelDealerBusinessExcellenceReport(DataTable table, string strFile, string FirstLine)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            int columnscount = table.Columns.Count;
            HttpContext.Current.Response.Write("<TR ><Td  colspan='14' style='background-color: #b4c6e7; text-align: center;' ><B>" + FirstLine + "</B></Td></TR> ");
            HttpContext.Current.Response.Write("<TR > ");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Year</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Month</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Dealer Code</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Dealer Name</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Function Area</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Function Sub Area</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Parameter</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Max Score</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Target</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Actual</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Achievement %</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Minimum Qualifying</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Final Score</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #c6e0b4; text-align: center; vertical-align: middle;' ><B>Remarks</B></Td>");
               

            HttpContext.Current.Response.Write("</TR>");

          

             

            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < 14; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }              
            HttpContext.Current.Response.Write("</Table>");
             

            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> ");
            
            HttpContext.Current.Response.Write("<TR >");
            HttpContext.Current.Response.Write("<Td  style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Function Area</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Max Score</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>Final Score</B></Td>");
            HttpContext.Current.Response.Write("<Td  style='background-color: #fce4d6; text-align: center; vertical-align: middle;' ><B>% Score</B></Td>"); 
             
            HttpContext.Current.Response.Write("</TR>");

            decimal SalesScore = 0, FinanceScore = 0, ServiceScore = 0, PartsScore = 0, NPSScore = 0, ManpowerScore = 0, InfrastructureScore = 0, Total=0;
            foreach (DataRow dr in table.Rows)
            {
                switch (Convert.ToString(dr["FunctionArea"]) )
                {
                    case "Sales":
                        SalesScore = SalesScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                    case "Finance":
                        FinanceScore = FinanceScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                    case "Service":
                        ServiceScore = ServiceScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                    case "Parts":
                        PartsScore = PartsScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                    case "NPS":
                        NPSScore = NPSScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                    case "Manpower":
                        ManpowerScore = ManpowerScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                    case "Infrastructure":
                        InfrastructureScore = InfrastructureScore + Convert.ToDecimal(dr["Final Score"]);
                        break;
                } 
            } 

            HttpContext.Current.Response.Write("</TR>"); 
            HttpContext.Current.Response.Write("<TR><Td>Sales</Td><Td>300</Td><Td>" + SalesScore + "</Td><Td>"+ SalesScore*100/300 + "%</Td></TR>");
            HttpContext.Current.Response.Write("<TR><Td>Finance</Td><Td>100</Td><Td>" + FinanceScore + "</Td><Td>" + FinanceScore + "%</Td></TR>");
            HttpContext.Current.Response.Write("<TR><Td>Service</Td><Td>200</Td><Td>" + ServiceScore + "</Td><Td>" + ServiceScore * 100 / 200 + "%</Td></TR>");
            HttpContext.Current.Response.Write("<TR><Td>Parts</Td><Td>100</Td><Td>" + PartsScore + "</Td><Td>" + PartsScore + "%</Td></TR>");
            HttpContext.Current.Response.Write("<TR><Td>NPS</Td><Td>100</Td><Td>" + NPSScore + "</Td><Td>" + NPSScore + "%</Td></TR>");
            HttpContext.Current.Response.Write("<TR><Td>Manpower</Td><Td>100</Td><Td>" + ManpowerScore + "</Td><Td>" + ManpowerScore + "%</Td></TR>");
            HttpContext.Current.Response.Write("<TR><Td>Infrastructure</Td><Td>100</Td><Td>" + InfrastructureScore + "</Td><Td>" + InfrastructureScore + "%</Td></TR>");
            Total = SalesScore + FinanceScore + ServiceScore + PartsScore + NPSScore + ManpowerScore + InfrastructureScore;
            HttpContext.Current.Response.Write("<TR><Td style='background-color: #fce4d6;'>Total</Td><Td style='background-color: #fce4d6;'>1000</Td><Td style='background-color: #fce4d6;'>" + Total + "</Td><Td style='background-color: #fce4d6;'>" + Total * 100 / 1000 + "%</Td><Td colspan = '3' style='background-color: #b4c6e7; text-align: center;'>Dealer Signature</Td><Td  colspan = '7' style='background-color: #b4c6e7; text-align: center;'>Ajax Team Member Signature</Td></TR>");
             

            HttpContext.Current.Response.Write("</Table>");


            HttpContext.Current.Response.Write("</font>");

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
