using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class StockTransferOrderCreate : System.Web.UI.UserControl
    {
        public List<PStockTransferOrderItem_Insert> PurchaseOrderItem_Insert
        {
            get
            {
                if (ViewState["PurchaseOrderItem_Insert"] == null)
                {
                    ViewState["PurchaseOrderItem_Insert"] = new List<PStockTransferOrderItem_Insert>();
                }
                return (List<PStockTransferOrderItem_Insert>)ViewState["PurchaseOrderItem_Insert"];
            }
            set
            {
                ViewState["PurchaseOrderItem_Insert"] = value;
            }
        }
        public PStockTransferOrder_Insert PO_Insert
        {
            get
            {
                if (ViewState["PPurchaseOrder_Insert"] == null)
                {
                    ViewState["PPurchaseOrder_Insert"] = new PStockTransferOrder_Insert();
                }
                return (PStockTransferOrder_Insert)ViewState["PPurchaseOrder_Insert"];
            }
            set
            {
                ViewState["PPurchaseOrder_Insert"] = value;
            }
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessageMaterialUpload.Text = string.Empty;
        }
        public void FillMaster()
        {
            fillDealer(); 
            Clear();
            fillItem();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.ID == "lbUploadMaterial")
                {
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                    MPE_MaterialUpload.Show();
                }
                else if (lbActions.ID == "lbDownloadMaterialTemplate")
                {
                    DownloadMaterialTemplate();
                }
                else if (lbActions.ID == "lbSave")
                {
                    lblMessage.ForeColor = Color.Red;
                    if (PurchaseOrderItem_Insert.Count == 0)
                    {
                        lblMessage.Text = "Please select the Material.";
                        return;
                    }
                    Save();
                }                 
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message; 
                lblMessage.ForeColor = Color.Red;
            }
        }
        void Clear()
        {
            //txtCustomerName.Text = string.Empty;
            ////  txtEnquiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //txtPersonName.Text = string.Empty;
            //txtMobile.Text = string.Empty;
            //txtMail.Text = string.Empty;
            //ddlSource.Items.Clear();
            //ddlCountry.Items.Clear();
            //ddlState.Items.Clear();
            //ddlDistrict.Items.Clear();
            //new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            //ddlCountry.SelectedValue = "1";
            //new DDLBind(ddlState, new BDMS_Address().GetState(null, Convert.ToInt32(ddlCountry.SelectedValue), null, null, null), "State", "StateID");
            //new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null, null, null), "District", "DistrictID");
            //new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            //new DDLBind(ddlSource, new BPresalesMasters().GetLeadSource(null, null), "Source", "SourceID");
            //txtAddress.Text = string.Empty;
            //txtAddress2.Text = string.Empty;
            //txtAddress3.Text = string.Empty;
            //txtProduct.Text = string.Empty;
            //txtRemarks.Text = string.Empty;

            //txtNextFollowUpDate.Text = string.Empty;
        }
        public PStockTransferOrder_Insert Read()
        {
            PStockTransferOrder_Insert PO = new PStockTransferOrder_Insert();
            PO.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            PO.DestinationOfficeID = Convert.ToInt32(ddlDestinationOffice.SelectedValue);
            PO.SourceOfficeID = Convert.ToInt32(ddlSourceOffice.SelectedValue); 
            PO.Remarks = txtRemarks.Text.Trim();
            return PO;
        } 
        public string Validation()
        {

            ddlDealer.BorderColor = Color.Silver;  
            ddlDestinationOffice.BorderColor = Color.Silver;
            ddlSourceOffice.BorderColor = Color.Silver;
            //txtExpectedDeliveryDate.BorderColor = Color.Silver;
            string Message = "";

            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                return "Please select the Dealer.";
            }
            if (ddlDestinationOffice.SelectedValue == "0")
            {
                ddlDestinationOffice.BorderColor = Color.Red;
                return "Please select the Receiving Location.";
            }
            if (ddlSourceOffice.SelectedValue == "0")
            {
                ddlSourceOffice.BorderColor = Color.Red;
                return "Please select the Source Location.";
            }
            if (ddlSourceOffice.SelectedValue == ddlDestinationOffice.SelectedValue)
            {
                ddlSourceOffice.BorderColor = Color.Red;
                return "Please select different Receiving and Source Location.";
            }
            return Message;
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithDisplayName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == 2);
            ddlDealer.DataBind();
            //ddlDealer.Items.Insert(0, new ListItem("All", "0"));
            FillGetDealerOffice();
            FillGetSourceDealer();
        }        
        void fillItem()
        {
            gvPOItem.DataSource = PurchaseOrderItem_Insert;
            gvPOItem.DataBind();
            decimal TaxableValue = 0, TaxValue = 0;
            foreach (PStockTransferOrderItem_Insert Item in PurchaseOrderItem_Insert)
            { 
                TaxableValue = TaxableValue + Item.TaxableValue;
                TaxValue = TaxValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue; 
               //Item.NetValue = Item.CGSTValue + Item.SGSTValue + Item.IGSTValue + Item.TaxableAmount;
            } 
            lblTaxableAmount.Text = TaxableValue.ToString();
            lblTaxAmount.Text = TaxValue.ToString();
            lblGrossAmount.Text = (TaxValue + TaxableValue).ToString(); 
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetDealerOffice();
            FillGetSourceDealer();
        }
        private void FillGetDealerOffice()
        {
            ddlDestinationOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDestinationOffice.DataValueField = "OfficeID";
            ddlDestinationOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null);
            ddlDestinationOffice.DataBind();
            ddlDestinationOffice.Items.Insert(0, new ListItem("Select", "0"));

            
        }
        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                Message = ValidationItem(hdfMaterialID.Value, txtQty.Text.Trim());
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                if (PurchaseOrderItem_Insert.Any(item => item.MaterialID == Convert.ToInt32(hdfMaterialID.Value)))
                {
                    lblMessage.Text = "Material already available.";
                    return;
                }
                PO_Insert = Read();
                PStockTransferOrderItem_Insert PoI = ReadItem(PO_Insert.DealerID, hdfMaterialID.Value, hdfMaterialCode.Value.Trim(), txtQty.Text.Trim());
                 
                PurchaseOrderItem_Insert.Add(PoI);
                PoI.Item = PurchaseOrderItem_Insert.Count * 10;
                fillItem();
                ClearItem();
            }
            catch(Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        void Save(string MaterialID, string MaterialCode, string Qty)
        {
            try
            {
                lblMessageMaterialUpload.ForeColor = Color.Red;
                lblMessage.ForeColor = Color.Red;
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                Message = ValidationItem(MaterialID, Qty);
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessageMaterialUpload.Text = Message;
                    MPE_MaterialUpload.Show();
                    return;
                }
                if (PurchaseOrderItem_Insert.Any(item => item.MaterialID == Convert.ToInt32(MaterialID)))
                {
                    lblMessageMaterialUpload.Text = "Dublicate Material Found.";
                    MPE_MaterialUpload.Show();
                    return;
                }
                PO_Insert = Read();
                PStockTransferOrderItem_Insert PoI = ReadItem(PO_Insert.DealerID, MaterialID, MaterialCode, Qty);
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
                PoI.MaterialDescription = m.MaterialDescription;
                if (string.IsNullOrEmpty(m.HSN))
                {
                    lblMessageMaterialUpload.Text = "HSN is Not updated for this material. Please contact Admin.";
                    MPE_MaterialUpload.Show();
                    return;
                }
                PurchaseOrderItem_Insert.Add(PoI);
                PoI.Item = PurchaseOrderItem_Insert.Count * 10;
                fillItem();
                ClearItem();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }               
        void ClearItem()
        {
            hdfMaterialID.Value = "";
            hdfMaterialCode.Value = "";
            txtMaterial.Text = "";
            txtQty.Text = "";
        }
        public PStockTransferOrderItem_Insert ReadItem(int DealerID,string MaterialID, string MaterialCode, string Qty)
        {
            PStockTransferOrderItem_Insert SM = new PStockTransferOrderItem_Insert();
            SM.MaterialID = Convert.ToInt32(MaterialID);
            SM.MaterialCode = MaterialCode;
            // SM.SupersedeYN = cbSupersedeYN.Checked;
            SM.Quantity = Convert.ToInt32(Qty);
            PDMS_Material m = new BDMS_Material().GetMaterialListSQL(SM.MaterialID, null, null, null, null)[0];
            SM.MaterialDescription = m.MaterialDescription;
            SM = new BStockTransferOrder().GetMaterialPriceForStockTransferOrder(DealerID, SM);
            return SM;
        }
        public string ValidationItem(string MaterialID, string Qty)
        {
            if (string.IsNullOrEmpty(MaterialID))
            {
                return "Please select the Material.";
            }
            if (string.IsNullOrEmpty(Qty))
            {
                return "Please enter the Qty.";
            }
            //decimal value;
            //if (!decimal.TryParse(Qty, out value))
            //{
            //    return "Please enter correct format in Qty.";
            //}
            Decimal.TryParse("0" + Qty, out decimal value);
            if (value <= 0)
            {
                return "Please enter Valid Qty.";
            }
            return "";
        }
        public void Save()
        {
            PO_Insert = Read(); 
            lblMessage.ForeColor = Color.Red;
             
            PO_Insert.Items = PurchaseOrderItem_Insert; 
            PApiResult Result = new BStockTransferOrder().InsertStockTransferOrder(PO_Insert);  
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            Session["StockTransferOrderID"] = Result.Data;
            Response.Redirect("StockTransferOrder.aspx");
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
        }
        protected void lnkBtnPoItemDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                LinkButton lnkBtnCountryDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnCountryDelete.NamingContainer);
                string Material = ((Label)row.FindControl("lblMaterial")).Text.Trim();

                int i = 0;
                foreach (PStockTransferOrderItem_Insert Item in PurchaseOrderItem_Insert)
                {
                    if (Item.MaterialCode == Material)
                    {
                        PurchaseOrderItem_Insert.RemoveAt(i);
                        lblMessage.Text = "Material removed successfully.";
                        lblMessage.ForeColor = Color.Green;
                        int ItemNo = 0;
                        foreach (PStockTransferOrderItem_Insert ItemN in PurchaseOrderItem_Insert)
                        {
                            ItemNo = ItemNo + 10;
                            ItemN.Item = ItemNo;
                        }
                        fillItem();
                        return;
                    }
                    i = i + 1;
                }
                lblMessage.Text = "Please Contact Admin.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
            lblMessage.ForeColor = Color.Red;
        }         
        void Upload()
        {
            try
            {
                if (fileUpload.HasFile == true)
                {
                    string validExcel = ".xlsx";
                    string FileExtension = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                    if (validExcel != FileExtension)
                    {
                        lblMessageMaterialUpload.Text = "Please check the File format.";
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
                                string MaterialID = string.Empty, Qty = string.Empty, MaterialCode = string.Empty;
                                List<IXLCell> Cells = row.Cells().ToList();
                                if (Cells.Count != 0)
                                {

                                    List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null,Convert.ToString(Cells[1].Value),null,null, null);
                                    if (Materials.Count > 0)
                                    {
                                        MaterialID = Materials[0].MaterialID.ToString();
                                        MaterialCode = Materials[0].MaterialCode;
                                        Qty = Convert.ToString(Cells[2].Value);
                                        Save(MaterialID, MaterialCode, Qty);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMessageMaterialUpload.Text = "Please check the file.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageMaterialUpload.Text = ex.Message.ToString();
                lblMessageMaterialUpload.ForeColor = Color.Red;
            }
        }
        protected void btnUploadMaterial_Click(object sender, EventArgs e)
        {
            Upload();
        }
        void DownloadMaterialTemplate()
        {
            string Path = Server.MapPath("~/Templates/Material.xlsx");
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"Material.xlsx\"");
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
        protected void btnAvailability_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red; 
            string Message = Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            if (string.IsNullOrEmpty(hdfMaterialCode.Value.Trim()))
            {
                lblMessage.Text = "Please select the Material.";
                return;
            }
            PDealerStock DealerStock = new BInventory().GetDealerStockCountByID(Convert.ToInt32(ddlSourceDealer.SelectedValue), Convert.ToInt32(ddlSourceOffice.SelectedValue), Convert.ToInt32(hdfMaterialID.Value));

           // List<PDealerStock> PDealerStock =  JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data));
            if (DealerStock.UnrestrictedQty == 0)
            {
                lblMessage.Text = "Material is not available.";
            }
            else
            {

                lblMessage.Text = "Available Material is : " + DealerStock.UnrestrictedQty;
            }
        }

        private void FillGetSourceDealer()
        {
            ddlSourceDealer.DataTextField = "CodeWithDisplayName";
            ddlSourceDealer.DataValueField = "DID";
            ddlSourceDealer.DataSource = new BStockTransferOrder().GetSourceDealerForStockTransferOrder(Convert.ToInt32(ddlDealer.SelectedValue));
            ddlSourceDealer.DataBind();
        }
        protected void ddlSourceDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGetSourceDealerOffice();
        } 
        private void FillGetSourceDealerOffice()
        {
            ddlSourceOffice.DataTextField = "OfficeName_OfficeCode";
            ddlSourceOffice.DataValueField = "OfficeID";
            ddlSourceOffice.DataSource = new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlSourceDealer.SelectedValue), null, null);
            ddlSourceOffice.DataBind();
            ddlSourceOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}