using Business;
using ClosedXML.Excel;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewInventory
{
    public partial class InitialStock : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewInventory_InitialStock; } }
        public DataTable Mat
        {
            get
            {
                if (ViewState["Material"] == null)
                {
                    ViewState["Material"] = new DataTable();
                }
                return (DataTable)ViewState["Material"];
            }
            set
            {
                ViewState["Material"] = value;
            }
        }
        public List<PInitialStock_Post> MaterialUpload
        {
            get
            {
                if (ViewState["MaterialUpload"] == null)
                {
                    ViewState["MaterialUpload"] = new List<PInitialStock_Post>();
                }
                return (List<PInitialStock_Post>)ViewState["MaterialUpload"];
            }
            set
            {
                ViewState["MaterialUpload"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
                new DDLBind(ddlMaterialModel, new BDMS_Model().GetModel(null, null, null), "ModelDescription", "ModelID", true, "Select Model");
                new DDLBind().FillDealerAndEngneer(ddlDealerF, null);
                FillDealerOfficeF();
                new DDLBind().FillDealerAndEngneer(ddlDealerO, null);
            }
        }
        protected void btnMaterialSearch_Click(object sender, EventArgs e)
        {
            FillMaterial();
        }
        void FillMaterial()
        {
            try
            {
                int? DealerID = ddlDealerF.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerF.SelectedValue);
                int? OfficeID = ddlDealerOfficeF.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOfficeF.SelectedValue);
                int? DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
                int? ModelID = ddlMaterialModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMaterialModel.SelectedValue);
                Mat = new BInventory().GetInitialStock(DealerID, OfficeID, DivisionID, ModelID, txtMaterialCode.Text.Trim());
                gvMaterial.PageIndex = 0;
                gvMaterial.DataSource = Mat;
                gvMaterial.DataBind();

                if (Mat.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnMaterialArrowLeft.Visible = false;
                    ibtnMaterialArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnMaterialArrowLeft.Visible = true;
                    ibtnMaterialArrowRight.Visible = true;
                    lblRowCount.Text = (((gvMaterial.PageIndex) * gvMaterial.PageSize) + 1) + " - " + (((gvMaterial.PageIndex) * gvMaterial.PageSize) + gvMaterial.Rows.Count) + " of " + Mat.Rows.Count;
                }
            }
            catch (Exception e1)
            {
                DisplayErrorMessage(e1);
            }
        }

        void DisplayErrorMessage(Exception e1)
        {
            lblMessage.Text = e1.ToString();
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }
        void DisplayErrorMessage(String Message)
        {
            lblMessage.Text = Message;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }
        void DisplayMessage(String Message)
        {
            lblMessage.Text = Message;
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
        }

        protected void btnMaterialUpload_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divUpload.Visible = true;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            MaterialUpload.Clear();
            GVUpload.DataSource = MaterialUpload;
            GVUpload.DataBind();
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

            if(ddlDealerO.SelectedValue=="0")
            {
                lblMessage.Text = "Please select the Dealer";
                return;
            }
            if (ddlDealerOfficeO.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer Office";
                return;
            }
            foreach (PInitialStock_Post dr in MaterialUpload)
            {

                dr.DealerID = Convert.ToInt32(ddlDealerO.SelectedValue);
                dr.OfficeID = Convert.ToInt32(ddlDealerOfficeO.SelectedValue);
           }
            PApiResult Result = new BInventory().InsertUpdateInitialStock(MaterialUpload);
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message; 
            lblMessage.ForeColor = Color.Green;
           // FillMaterial();
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divUpload.Visible = false;
        }
        private Boolean FillUpload()
        {
            Boolean Success = true; 
            if (fileUpload.HasFile == true)
            {
                using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                {
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
                            
                            if (IXLCell_.Count !=0)
                                MaterialUpload.Add(new PInitialStock_Post()
                                {
                                    ID = Convert.ToInt32(IXLCell_[0].Value),
                                    DealerID = Convert.ToInt32(ddlDealerO.SelectedValue),
                                    OfficeID = Convert.ToInt32(ddlDealerOfficeO.SelectedValue),
                                    MaterialCode = Convert.ToString(IXLCell_[1].Value),
                                    Quantity = Convert.ToInt32(IXLCell_[2].Value),
                                    PerUnitPrice = Convert.ToDecimal(IXLCell_[3].Value)
                                });
                        }
                    }
                    List<PDMS_Material> MaterialS = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
                    foreach (PInitialStock_Post dr in MaterialUpload)
                    {
                        bool containsItem = MaterialS.Any(item => item.MaterialCode == dr.MaterialCode);
                        if (!containsItem)
                        {
                            lblMessage.Text = "Please Check Material Code : " + dr.MaterialCode + " Not Available...!";
                            lblMessage.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }
                        List<PDMS_Material> Mats = MaterialS.Where(s => s.MaterialCode == dr.MaterialCode).ToList();
                        dr.MaterialDescription = Mats[0].MaterialDescription; 
                    }
                    if (MaterialUpload.Count > 0)
                    {
                        GVUpload.DataSource = MaterialUpload;
                        GVUpload.DataBind();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please Upload the File...!";
                lblMessage.ForeColor = Color.Red;
                Success = false;
                return Success;
            }
            return Success;
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string Path = Server.MapPath("~") + "Templates\\InitialStock.xlsx";
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"InitialStock.xlsx\"");
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

        protected void gvMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ibtnMaterialArrowLeft_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ibtnMaterialArrowRight_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddlDealerO_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDealerOfficeO, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealerO.SelectedValue), null, null), "OfficeName", "OfficeID");
        }

        protected void ddlDealerF_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDealerOfficeF();
        }
        void FillDealerOfficeF()
        {
            new DDLBind(ddlDealerOfficeF, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealerF.SelectedValue), null, null), "OfficeName", "OfficeID");
        }
    }
}