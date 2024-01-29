using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        }
        public PEnquiry Read()
        {
            PEnquiry enquiry = new PEnquiry();
            //enquiry.CustomerName = txtCustomerName.Text.Trim();
            //enquiry.EnquiryNextFollowUpDate = Convert.ToDateTime(txtNextFollowUpDate.Text.Trim());
            //enquiry.PersonName = txtPersonName.Text.Trim();
            //enquiry.Mobile = txtMobile.Text.Trim();
            //enquiry.Mail = txtMail.Text.Trim();
            //enquiry.ProductType = new PProductType();
            //enquiry.ProductType.ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue);

          
            //enquiry.Remarks = txtRemarks.Text.Trim();
            //enquiry.CreatedBy = new PUser();
            return enquiry;
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
            PApiResult Result = new BInventory().InsertDealerPhysicalInventoryPosting(MaterialUpload);
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green; 
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
                                MaterialUpload.Add(new PPhysicalInventoryPosting_Post()
                                {
                                    ID = Convert.ToInt32(IXLCell_[0].Value),
                                    DealerID = DealerID,
                                    OfficeID = OfficeID,
                                    MaterialCode = Convert.ToString(IXLCell_[1].Value),
                                    PhysicalStock = Convert.ToInt32(IXLCell_[2].Value) 
                                });
                        }
                    }
                   
                    PApiResult Result = new BInventory().GetDealerStock(DealerID, OfficeID, null, null, null, null, null);

                    List<PDealerStock> DealerStocks = JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data)); 

                   // List<PDMS_Material> Employee = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
                    foreach (PPhysicalInventoryPosting_Post dr in MaterialUpload)
                    {
                        List<PDealerStock> DealerStock = DealerStocks.Where(s => s.Material.MaterialCode == dr.MaterialCode).ToList();
                        if(DealerStock.Count==0)
                        {
                            lblMessage.Text = "Please Check Material Code : " + dr.MaterialCode + " Not Available...!";
                            lblMessage.ForeColor = Color.Red;
                            Success = false;
                            return Success;
                        }
                        dr.MaterialID = DealerStock[0].Material.MaterialID;
                        dr.SystemStock = DealerStock[0].UnrestrictedQty;

                        //bool containsItem = Employee.Any(item => item.MaterialCode == dr.MaterialCode);
                        //if (!containsItem)
                        //{
                        //    lblMessage.Text = "Please Check Material Code : " + dr.MaterialCode + " Not Available...!";
                        //    lblMessage.ForeColor = Color.Red;
                        //    Success = false;
                        //    return Success;
                        //}
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
            string Path = Server.MapPath("Templates\\PhysicalInventoryPosting.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"PhysicalInventoryPosting.xlsx\"");
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

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}