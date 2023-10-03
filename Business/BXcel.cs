using Microsoft.Reporting.Map.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public class BXcel : System.Web.UI.Page
    {
        public void ExporttoExcel(DataTable table, string strFile)
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

            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
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
                HitRatioF = (WinHotF + WinWarmF + WinColdF) * 100 / Total;
                LostRatioF = (LostHotF + LostWarmF + LostColdF) * 100 / Total;
                DropRatioF = (DropHotF + DropWarmF + DropColdF) * 100 / Total;
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
    }
}
