
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Business
{
    public static class LMSHelper
    {
        public static string EncodeString(string value)
        {
            Byte[] encodeValueToBytes = Encoding.ASCII.GetBytes(value);
            return Convert.ToBase64String(encodeValueToBytes);
        }
        public static string DecodeString(string value)
        {
            Byte[] decodeValueToBytes = Convert.FromBase64String(value);
            return ASCIIEncoding.ASCII.GetString(decodeValueToBytes);
        }
        public static string GetMessageBody(MessageModuleType Type, string FilePath, string FileName)
        {
            string messageBody = string.Empty;
            try
            {
                messageBody = GetFileContent(Path.Combine(FilePath, FileName));
            }
            catch (LMSException vpex)
            {
                throw vpex;
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return messageBody;
        }
        public static string GetFileContent(string FilePath)
        {
            string msg = string.Empty;
            try
            {
                if (File.Exists(FilePath))
                {
                    FileStream fStream = new FileStream(FilePath, FileMode.Open);
                    StreamReader sReader = new StreamReader(fStream);
                    msg = sReader.ReadToEnd();
                    fStream.Close();
                }
                else
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException fex)
            {
                throw new LMSException(ErrorCode.FNE, fex);
            }
            catch (Exception ex)
            {
                throw new LMSException(ErrorCode.GENE, ex);
            }
            return msg;
        }
        public static void ExporttoExcel(DataTable table, string strFile)
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
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}