using Business;
using ClosedXML.Excel;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.Reports
{
    public partial class DealerSalesTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUploadTarget_Click(object sender, EventArgs e)
        { 
            lblMessageMaterialUpload.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDMS_Material> Supersede = new List<PDMS_Material>(); 
            try
            {
                if (fileUpload.HasFile != true)
                {
                    lblMessageMaterialUpload.Text = "Please check the file.";
                    return;
                }
                string validExcel = ".xlsx";
                string FileExtension = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                if (validExcel != FileExtension)
                {
                    lblMessageMaterialUpload.Text = "Please check the file format.";
                    return;
                } 
                using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable DTDealerOperatorDetailsUpload = new DataTable();

                    //Loop through the Worksheet rows.
                    int sno = 0;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        sno += 1;
                        if (sno > 1)
                        {
                            List<IXLCell> Cells = row.Cells().ToList();
                            if (Cells.Count != 0)
                            {
                                string Dealer = Convert.ToString(Cells[1].Value).TrimEnd('\0');
                                string Year = Convert.ToString(Cells[2].Value).TrimEnd('\0');
                                string Month = Convert.ToString(Cells[3].Value).TrimEnd('\0');
                                string Division = Convert.ToString(Cells[4].Value).TrimEnd('\0');
                                string Target = Convert.ToString(Cells[5].Value).TrimEnd('\0'); 


                            }
                        }
                    } 
                } 
            }
            catch (Exception ex)
            {
                lblMessageMaterialUpload.Text = ex.Message.ToString();
            }
        }
    }
}