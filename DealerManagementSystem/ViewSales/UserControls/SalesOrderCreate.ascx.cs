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

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SalesOrderCreate : System.Web.UI.UserControl
    {
        public List<PSaleOrderItem_Insert> SOItem_Insert
        {
            get
            {
                if (ViewState["SOItem_Insert"] == null)
                {
                    ViewState["SOItem_Insert"] = new List<PSaleOrderItem_Insert>();
                }
                return (List<PSaleOrderItem_Insert>)ViewState["SOItem_Insert"];
            }
            set
            {
                ViewState["SOItem_Insert"] = value;
            }
        }
        public PSaleOrder_Insert SO_Insert
        {
            get
            {
                if (ViewState["SO_Insert"] == null)
                {
                    ViewState["SO_Insert"] = new PSaleOrder_Insert();
                }
                return (PSaleOrder_Insert)ViewState["SO_Insert"];
            }
            set
            {
                ViewState["SO_Insert"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessageMaterialUpload.Text = string.Empty;
        }
        public void FillMaster()
        {
            //new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
            //new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            //cxExpectedDeliveryDate.StartDate = DateTime.Now; 
            //ddlDivision.SelectedValue = "15"; ddlDivision.Enabled = false;

            new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID", false, "Select");
            int? DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(DealerID, null, null), "OfficeName", "OfficeID", true, "Select");
            //new DDLBind(ddlSalesEngineer, 0, "ContactName", "UserID");

            new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");

            new DDLBind(ddlSalesType, new BDMS_Master().GetAjaxOneStatus((short)AjaxOneStatusHeader.SalesType), "Status", "StatusID", true, "Select");
            cxExpectedDeliveryDate.StartDate = DateTime.Now;
            txtExpectedDeliveryDate.Text = DateTime.Now.ToShortDateString();
            ddlDivision.SelectedValue = "15"; 
            ddlDivision.Enabled = false;
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
            txtHeaderDiscountPercent.Text = "0";
            ClearHeader();
            ClearItem();
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");

            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");

            txtCustomer.Text = "";
            hdfCustomerId.Value = "";
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
        }
        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                string Message = Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                Message = ValidationItem();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                int MaterialID = Convert.ToInt32(hdfMaterialID.Value);
                if (cbSupersede.Checked)
                {
                    MaterialID = new BDMS_Material().GetMaterialSupersedeFinalByID(Convert.ToInt32(hdfMaterialID.Value));
                } 
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(MaterialID, null, null, null, null)[0];

                if (MaterialID != Convert.ToInt32(hdfMaterialID.Value))
                {
                    lblMessage.Text =  "Material :" + hdfMaterialCode.Value + "Supersede to " + "Material :" + m.MaterialCode;
                }

                foreach (PSaleOrderItem_Insert Item in SOItem_Insert)
                {
                    if (Item.MaterialID == MaterialID)
                    {
                        lblMessage.Text = "Duplicate Material.";
                        return ;
                    }
                }
                string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
                string Dealer  = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                decimal HDiscountPercent = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim()); 
                int Qty = Convert.ToInt32(txtQty.Text.Trim());
                if (SOItem_Insert.Count == 0)
                {
                    Message = new BDMS_SalesOrder().ValidationCustomerGST(Convert.ToInt64(hdfCustomerId.Value), Convert.ToInt32(ddlDealer.SelectedValue), ddlTaxType.SelectedItem.Text);
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                }
                SOItem_Insert.Add(new BDMS_SalesOrder().ReadItem(m, Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlOfficeName.SelectedValue), Qty, Customer, Dealer, HDiscountPercent, 0,0, ddlTaxType.SelectedItem.Text));
                //Message = AddMat(m, txtQty.Text.Trim());
                //if (!string.IsNullOrEmpty(Message))
                //{
                //    lblMessage.Text = Message;
                //    return;
                //}
                ValidationMaterialCount();
                fillItem();
                ClearItem();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public PSaleOrder_Insert Read()
        {
            PSaleOrder_Insert SO = new PSaleOrder_Insert();
            SO.DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            SO.OfficeID = Convert.ToInt32(ddlOfficeName.SelectedValue);

            SO.CustomerID = Convert.ToInt32(hdfCustomerId.Value);

            SO.ProductID = ddlProduct.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlProduct.SelectedValue);
            SO.EquipmentID = ddlEquipment.SelectedValue == "0" ? (long?)null : Convert.ToInt64(ddlEquipment.SelectedValue);
            SO.ContactPersonNumber = txtContactPersonNumber.Text.Trim();
            SO.Attn = txtAttn.Text.Trim();

            SO.StatusID = 11;
            SO.DivisionID = Convert.ToInt32(ddlDivision.SelectedValue);
            SO.Remarks = txtRemarks.Text.Trim();
            SO.ExpectedDeliveryDate = Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            SO.InsurancePaidBy = ddlInsurancePaidBy.SelectedValue == "0" ? null : ddlInsurancePaidBy.SelectedItem.Text;
            SO.FrieghtPaidBy = ddlFrieghtPaidBy.SelectedValue == "0" ? null : ddlFrieghtPaidBy.SelectedItem.Text;
            SO.TaxType = ddlTaxType.SelectedItem.Text;
            SO.SaleOrderTypeID = 1;
            SO.SalesEngineerID = ddlSalesEngineer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesEngineer.SelectedValue);
            SO.HeaderDiscountPercentage = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim());
            SO.RefNumber = txtRefNumber.Text.Trim();
            SO.RefDate = string.IsNullOrEmpty(txtRefDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtExpectedDeliveryDate.Text.Trim());
            SO.SalesTypeID = ddlSalesType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSalesType.SelectedValue);
            SO.Freight = string.IsNullOrEmpty(txtFreight.Text.Trim()) ? 0 : Convert.ToDecimal(txtFreight.Text.Trim());
            SO.PackingAndForward = string.IsNullOrEmpty(txtPackingAndForward.Text.Trim()) ? 0 : Convert.ToDecimal(txtPackingAndForward.Text.Trim());
            return SO;
        }

        void fillItem()
        {
            gvSOItem.DataSource = SOItem_Insert;
            gvSOItem.DataBind();
            FillAmountCall();
        }
        void FillAmountCall()
        {
            decimal DiscountValue = 0, TaxableValue = 0, TaxValue = 0, TCSValue = 0;
            foreach (PSaleOrderItem_Insert Item in SOItem_Insert)
            {
                DiscountValue = DiscountValue + Item.DiscountValue;
                TaxableValue = TaxableValue + Item.TaxableValue;
                TaxValue = TaxValue + Item.CGSTValue + Item.SGSTValue + Item.IGSTValue;
            }
            lblDiscountValue.Text = DiscountValue.ToString();
            lblTaxableValue.Text = TaxableValue.ToString();
            lblTaxValue.Text = TaxValue.ToString();
            lblTCSValue.Text = TCSValue.ToString();
            lblTotalValue.Text = (TaxableValue + TaxValue + TCSValue).ToString();
        }
        protected void lnkBtnSoItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.ForeColor = Color.Red;
                LinkButton lnkBtnCountryDelete = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkBtnCountryDelete.NamingContainer);
                string Material = ((Label)row.FindControl("lblMaterial")).Text.Trim();

                int i = 0;
                foreach (PSaleOrderItem_Insert Item in SOItem_Insert)
                {
                    if (Item.MaterialCode == Material)
                    {
                        SOItem_Insert.RemoveAt(i);
                        lblMessage.Text = "Material removed successfully.";
                        lblMessage.ForeColor = Color.Green;
                        fillItem();
                        ValidationMaterialCount();
                        return;
                    }
                    i = i + 1;
                }
                lblMessage.Text = "Please contact Admin.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        void ClearHeader()
        {
           // ddlDealer.SelectedValue = "0";
            txtCustomer.Text = "";
            hdfCustomerId.Value = "";
            ddlOfficeName.SelectedValue = "0";
            txtContactPersonNumber.Text = "";
            //ddlDivision.SelectedValue = "0";
            txtRemarks.Text = "";
            //txtExpectedDeliveryDate.Text = "";
            //txtInsurancePaidBy.Text = "";
            ddlInsurancePaidBy.SelectedValue = "0";
            //txtFrieghtPaidBy.Text = "";
            ddlFrieghtPaidBy.SelectedValue = "0";
            txtAttn.Text = "";
            ddlProduct.SelectedValue = "0";
            //txtSelectTax.Text = "";
            ddlTaxType.SelectedValue = "1";
            cxExpectedDeliveryDate.StartDate = DateTime.Now;
            ddlSalesEngineer.SelectedValue = "0";
            gvSOItem.DataSource = null;
            gvSOItem.DataBind();
        }
        void ClearItem()
        {
            hdfMaterialID.Value = "";
            hdfMaterialCode.Value = "";
            txtMaterial.Text = "";
            txtQty.Text = "";
        }
        //protected void btnSaveSOItem_Click(object sender, EventArgs e)
        //{
        //    lblMessage.ForeColor = Color.Red;
        //    try
        //    {
        //        string Message = Validation();
        //        if (!string.IsNullOrEmpty(Message))
        //        {
        //            lblMessage.Text = Message;
        //            return;
        //        }
        //        if (SOItem_Insert.Count == 0)
        //        {
        //            lblMessage.Text = "Please add Material.";
        //            return;
        //        }
        //        SO_Insert = Read();
        //        SO_Insert.SaleOrderItems = SOItem_Insert;
        //        string result = new BAPI().ApiPut("SaleOrder", SO_Insert);
        //        PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

        //        if (Result.Status == PApplication.Failure)
        //        {
        //            lblMessage.Text = Result.Message;
        //            return;
        //        }

        //        Session["SaleOrderID"] = Result.Data;
        //        Response.Redirect("PurchaseOrder.aspx");

        //        //lblMessage.Text = Result.Message;
        //        //lblMessage.ForeColor = Color.Green;
        //        //ClearHeader();
        //        //ClearItem();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //    }
        //}
        protected void txtHeaderDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red; 
            decimal HDiscountPercent = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim());
            if (HDiscountPercent < 0 || HDiscountPercent >= 100)
            {
                lblMessage.Text = "Header Discount Percentage cannot be less than 0 and more than 100.";
                return;
            }
            foreach (PSaleOrderItem_Insert SOI in SOItem_Insert)
            {
                decimal HDiscountValue = (SOI.Value * HDiscountPercent) / 100;
                SOI.DiscountValue = HDiscountValue + SOI.ItemDiscountValue;
                SOI.TaxableValue = SOI.Value - SOI.DiscountValue; 
                SOI.SGSTValue = SOI.TaxableValue * (SOI.SGST / 100);
                SOI.CGSTValue = SOI.TaxableValue * (SOI.CGST / 100);
                SOI.IGSTValue = SOI.TaxableValue * (SOI.IGST / 100);
                SOI.NetAmount = SOI.TaxableValue + SOI.SGSTValue + SOI.CGSTValue + SOI.IGSTValue;
            }
            fillItem();
        }
        protected void txtBoxDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent; 
            Label MaterialID = (Label)gvRow.FindControl("lblMaterialID"); 
            Label lblDiscountValue = (Label)gvRow.FindControl("lblDiscountValue");
            Label lblTaxableValue = (Label)gvRow.FindControl("lblTaxableValue");
            Label lblTaxValue = (Label)gvRow.FindControl("lblTaxValue");
            Label lblNetAmount = (Label)gvRow.FindControl("lblNetAmount");

            decimal HDiscountPercent = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim());
            
            
            foreach (PSaleOrderItem_Insert SOI in SOItem_Insert)
            { 
                if (SOI.MaterialID == Convert.ToInt64(MaterialID.Text))
                {
                    TextBox txtItemDiscountPercentage = (TextBox)gvRow.FindControl("txtItemDiscountPercentage");

                    decimal ItemDiscountPercentage = Convert.ToDecimal(txtItemDiscountPercentage.Text.Trim());
                    TextBox txtItemDiscountValue =  (TextBox)gvRow.FindControl("txtItemDiscountValue");
                    if (ItemDiscountPercentage < 0)
                    {
                        lblMessage.Text = "Item Discount Percentage cannot be less than 0.";
                        return;
                    }
                    decimal IDiscountValue = (SOI.Value * ItemDiscountPercentage) / 100;
                    txtItemDiscountValue.Text = Convert.ToString(IDiscountValue);
                    txtItemDiscountPercentage.Text = "";
                    decimal HDiscountValue = (SOI.Value * HDiscountPercent) / 100;
                    SOI.ItemDiscountValue = IDiscountValue;
                    SOI.DiscountValue = HDiscountValue + IDiscountValue;
                    SOI.TaxableValue = SOI.Value - SOI.DiscountValue;
                    SOI.SGSTValue = SOI.TaxableValue * (SOI.SGST / 100);
                    SOI.CGSTValue = SOI.TaxableValue * (SOI.CGST / 100);
                    SOI.IGSTValue = SOI.TaxableValue * (SOI.IGST / 100);
                    SOI.NetAmount = SOI.TaxableValue + SOI.SGSTValue + SOI.CGSTValue + SOI.IGSTValue; 
                
                    lblDiscountValue.Text = SOI.DiscountValue.ToString();
                    lblTaxableValue.Text = SOI.TaxableValue.ToString();
                    lblTaxValue.Text = Convert.ToString(SOI.SGSTValue + SOI.CGSTValue + SOI.IGSTValue);
                    lblNetAmount.Text = SOI.NetAmount.ToString();
                }
            }
            FillAmountCall();
        }
        protected void txtBoxDiscountValue_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label MaterialID = (Label)gvRow.FindControl("lblMaterialID");
            Label lblDiscountValue = (Label)gvRow.FindControl("lblDiscountValue");
            Label lblTaxableValue = (Label)gvRow.FindControl("lblTaxableValue");
            Label lblTaxValue = (Label)gvRow.FindControl("lblTaxValue");
            Label lblNetAmount = (Label)gvRow.FindControl("lblNetAmount");

            decimal HDiscountPercent = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim());
            decimal IDiscountValue = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtItemDiscountValue")).Text.Trim());
            if (IDiscountValue < 0)
            {
                lblMessage.Text = "Item Discount Value cannot be less than 0.";
                return;
            }
            foreach (PSaleOrderItem_Insert SOI in SOItem_Insert)
            {
                if (SOI.MaterialID == Convert.ToInt64(MaterialID.Text))
                {
                    decimal HDiscountValue = (SOI.Value * HDiscountPercent) / 100;
                    SOI.ItemDiscountValue = IDiscountValue;
                    SOI.DiscountValue = HDiscountValue + IDiscountValue;
                    SOI.TaxableValue = SOI.Value - SOI.DiscountValue;
                    SOI.SGSTValue = SOI.TaxableValue * (SOI.SGST / 100);
                    SOI.CGSTValue = SOI.TaxableValue * (SOI.CGST / 100);
                    SOI.IGSTValue = SOI.TaxableValue * (SOI.IGST / 100);
                    SOI.NetAmount = SOI.TaxableValue + SOI.SGSTValue + SOI.CGSTValue + SOI.IGSTValue;

                    lblDiscountValue.Text = SOI.DiscountValue.ToString();
                    lblTaxableValue.Text = SOI.TaxableValue.ToString();
                    lblTaxValue.Text = Convert.ToString(SOI.SGSTValue + SOI.CGSTValue + SOI.IGSTValue);
                    lblNetAmount.Text = SOI.NetAmount.ToString();
                }
            }
            FillAmountCall();
        }
        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            if(hdfCustomerId.Value =="" || hdfCustomerId.Value == "0") 
            {
                lblMessage.Text = "Please check the Customer.";
                lblMessage.ForeColor = Color.Red;
                txtCustomer.Text = "";
                return;
            }
            List<PDMS_EquipmentHeader> EQs = new BDMS_Equipment().GetEquipmentForCreateICTicket(Convert.ToInt64(hdfCustomerId.Value), null, null);
            new DDLBind(ddlEquipment, EQs, "EquipmentSerialNo", "EquipmentHeaderID", true, "Select");
        }

        protected void BtnAvailability_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            ddlDealer.BorderColor = Color.Silver;
            ddlOfficeName.BorderColor = Color.Silver; 
            string Message = "";
             
            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                lblMessage.Text = "Please select the Dealer.";
                return;
            }
            if (ddlOfficeName.SelectedValue == "0")
            {
                ddlOfficeName.BorderColor = Color.Red;
                lblMessage.Text = "Please select the Dealer Office.";
                return;
            }
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                lblMessage.Text = "Please select the Material.";
            }
             
            PDealerStock s = new BInventory().GetDealerStockCountByID(Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlOfficeName.SelectedValue), Convert.ToInt64(hdfMaterialID.Value));

            if (s != null)
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "On Order Qty : " + s.OnOrderQty.ToString() + ", Transit Qty : " + s.TransitQty.ToString() + ", Unrestricted Qty : " + s.UnrestrictedQty.ToString();
            }
            else
            {
                lblMessage.Text = "Stock is not available";
            }
            
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.ID == "lbUploadMaterial")
                {
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        lblMessage.ForeColor = Color.Red;
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
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                    if (SOItem_Insert.Count == 0)
                    {
                        lblMessage.Text = "Please add Material.";
                        return;
                    }

                    Message = new BDMS_SalesOrder().ValidationCustomerGST(Convert.ToInt64(hdfCustomerId.Value), Convert.ToInt32(ddlDealer.SelectedValue), ddlTaxType.SelectedItem.Text);
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }

                   
                    SO_Insert = Read();
                    SO_Insert.SaleOrderItems = SOItem_Insert;
                    string result = new BAPI().ApiPut("SaleOrder", SO_Insert);
                    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

                    if (Result.Status == PApplication.Failure)
                    {
                        lblMessage.Text = Result.Message;
                        return;
                    }

                    Session["SaleOrderID"] = Result.Data;
                    Response.Redirect("SaleOrder.aspx");
                }
                else if (lbActions.ID == "lbAddMaterialFromCart")
                {
                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                    txtCartDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtCartDateTo.Text = DateTime.Now.ToShortDateString();
                    new DDLBind(ddlCartDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                    ddlCartDivision_SelectedIndexChanged(null, null);
                    fillCart();

                    MPE_MaterialFromCart.Show();
                }
                else if (lbActions.ID == "lbCopyFromSO")
                {

                    string Message = Validation();
                    if (!string.IsNullOrEmpty(Message))
                    {
                        lblMessage.Text = Message;
                        return;
                    }
                    txtPoNumber.Text = "";
                    gvMaterialCopyOrder.DataSource = null;
                    gvMaterialCopyOrder.DataBind();
                    MPE_CopyOrder.Show();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
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
        protected void btnUploadMaterial_Click(object sender, EventArgs e)
        {
            MPE_MaterialUpload.Show();
            lblMessageMaterialUpload.ForeColor = Color.Red;
            List<PDMS_Material> Supersede = new List<PDMS_Material>();
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            try
            {
                SOItem_Insert = new List<PSaleOrderItem_Insert>();
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
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, (short)Division.Parts, null, null);

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
                            string Qty = string.Empty;
                            List<IXLCell> Cells = row.Cells().ToList();
                            if (Cells.Count != 0)
                            {
                                string ExcelMaterialCode = Convert.ToString(Cells[1].Value).TrimEnd('\0');
                                Boolean SupersedeCheck = Convert.ToBoolean(Convert.ToInt32(Convert.ToString(Cells[3].Value).TrimEnd('\0')));
                                string MaterialCode = ExcelMaterialCode;
                                if (SupersedeCheck)
                                {
                                    MaterialCode = new BDMS_Material().GetMaterialSupersedeFinalByCode(ExcelMaterialCode);
                                }
                                MaterialCode = MaterialCode.Trim(); 

                                if (MaterialCode != ExcelMaterialCode)
                                {
                                    Supersede.Add(new PDMS_Material()
                                    {
                                        MaterialCode = ExcelMaterialCode,
                                        Supersede = new PSupersede() { Material = MaterialCode }
                                    });
                                } 
                                List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode).ToList();
                                if (Material.Count == 0)
                                {
                                    MaterialIssue.Add(MaterialCode, "Material (" + MaterialCode + ") is not available.");
                                    continue; 
                                }

                                if (SOItem_Insert.Any(item => item.MaterialID == Material[0].MaterialID))
                                {
                                    MaterialIssue.Add(Material[0].MaterialCode, "Duplicate Material (" + Material[0].MaterialCode + ") Found. It is removed in list");
                                    continue; 
                                }
                                try
                                {

                                    //string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
                                    //string Dealer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                                    //decimal HDiscountPercent = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim());
                                    //int Qty1 = Convert.ToInt32(Convert.ToString(Cells[2].Value)); 
                                    //PSaleOrderItem_Insert item_Insert = new BDMS_SalesOrder().ReadItem(Material[0], Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlOfficeName.SelectedValue), Qty1, Customer, Dealer, HDiscountPercent, 0,0, ddlTaxType.SelectedItem.Text);
                                    //SOItem_Insert.Add(item_Insert);

                                    int Qty1 = Convert.ToInt32(Convert.ToString(Cells[2].Value));
                                    AddMaterial(Material[0], Qty1);
                                }
                                catch (Exception e1)
                                {
                                    MaterialIssue.Add(Material[0].MaterialCode, e1.Message); 
                                }

                            }
                        }
                    }
                }
                MPE_MaterialUpload.Hide(); 
                FillMessage(Supersede, MaterialIssue);  
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        } 
        public string Validation()
        {
            ddlDealer.BorderColor = Color.Silver;
            ddlOfficeName.BorderColor = Color.Silver;
            txtCustomer.BorderColor = Color.Silver;
            txtContactPersonNumber.BorderColor = Color.Silver;
            ddlSalesType.BorderColor = Color.Silver;
            ddlSalesEngineer.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            ddlTaxType.BorderColor = Color.Silver;
            txtHeaderDiscountPercent.BorderColor = Color.Silver;
            string Message = "";

            if (ddlDealer.SelectedValue == "0")
            {
                ddlDealer.BorderColor = Color.Red;
                return "Please select the Dealer.";
            }
            if (ddlOfficeName.SelectedValue == "0")
            {
                ddlOfficeName.BorderColor = Color.Red;
                return "Please select the Dealer Office.";
            }
            if (string.IsNullOrEmpty(hdfCustomerId.Value))
            {
                txtCustomer.BorderColor = Color.Red;
                return "Please enter Customer.";
            }
            if (!string.IsNullOrEmpty(txtContactPersonNumber.Text.Trim()))
            {
                long longCheck;
                if (!long.TryParse(txtContactPersonNumber.Text.Trim(), out longCheck))
                {
                    txtContactPersonNumber.BorderColor = Color.Red;
                    return "Contact Number should be 10 Digit.";
                }
            }
            if (ddlSalesType.SelectedValue == "0")
            {
                ddlSalesType.BorderColor = Color.Red;
                return "Please select the Sales Type.";
            }
            if (Convert.ToInt32(ddlSalesType.SelectedValue) == (short)AjaxOneStatus.PartsSalesType_Engineer)
            {
                if (ddlSalesEngineer.SelectedValue == "0")
                {
                    ddlSalesEngineer.BorderColor = Color.Red;
                    return "Please select the Sales Engnieer.";
                }
            }
            if (ddlDivision.SelectedValue == "0")
            {
                ddlDivision.BorderColor = Color.Red;
                return "Please select the Division.";
            }
            if (ddlProduct.SelectedValue == "0")
            {
                ddlProduct.BorderColor = Color.Red;
                return "Please select the Product.";
            }
            if (string.IsNullOrEmpty(txtExpectedDeliveryDate.Text))
            {
                txtExpectedDeliveryDate.BorderColor = Color.Red;
                return "Please enter the Expected Delivery Date.";
            }
            if (ddlTaxType.SelectedValue == "0")
            {
                ddlTaxType.BorderColor = Color.Red;
                return "Please select the Tax.";
            }
            decimal value;
            if (!decimal.TryParse(txtHeaderDiscountPercent.Text, out value))
            {
                txtHeaderDiscountPercent.BackColor = Color.Red;
                return "Please enter correct format in Header Discount Percent.";
            }
            
            return Message;
        }
        public string ValidationItem()
        {
            if (string.IsNullOrEmpty(hdfMaterialID.Value))
            {
                return "Please select the Material.";
            }
            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                return "Please enter the Qty.";
            }
            
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty.";
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) < 1)
            {
                return "Quantity cannot be less than 1.";
            }

            return "";
        }
        //public string ValidationCustomerGST()
        //{ 
        //    string Message = ""; 
        //    PDMS_Customer Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt64(hdfCustomerId.Value));
        //    PDMS_Dealer Dealer = new BDMS_Dealer().GetDealer(Convert.ToInt32(ddlDealer.SelectedValue), null, null, null)[0];
        //    if (Customer.GSTIN != "URD")
        //    {
        //        if (ddlTaxType.SelectedItem.Text == "IGST")
        //        {
        //            if (Customer.State.StateID == Dealer.StateN.StateID)
        //            {
        //                return "Please check the Tax Type w.r.t to Customer."; 
        //            }
        //        }
        //        else
        //        {
        //            if (Customer.State.StateID != Dealer.StateN.StateID)
        //            {
        //                return "Please check the Tax Type w.r.t to Customer."; 
        //            }
        //        }
        //    } 
        //    return Message;
        //}

        public void ValidationMaterialCount()
        {
            if (SOItem_Insert.Count==0)
            {
                ddlDealer.Visible = true;
                ddlOfficeName.Visible = true;
                txtCustomer.Visible = true;
                ddlTaxType.Visible = true;

                lblDealer.Visible = false;
                lblOfficeName.Visible = false;
                lblCustomer.Visible = false;
                lblTaxType.Visible = false;
            }
            else
            {
                ddlDealer.Visible = false;
                ddlOfficeName.Visible = false;
                txtCustomer.Visible = false;
                ddlTaxType.Visible = false;

                lblDealer.Visible = true;
                lblOfficeName.Visible = true;
                lblCustomer.Visible = true;
                lblTaxType.Visible = true;

                lblDealer.Text = ddlDealer.SelectedItem.Text;
                lblOfficeName.Text = ddlOfficeName.SelectedItem.Text;
                lblCustomer.Text = txtCustomer.Text;
                lblTaxType.Text = ddlTaxType.SelectedItem.Text;
            }  
        }

        protected void btnCopyPoAdd_Click(object sender, EventArgs e)
        {
            MPE_CopyOrder.Show();
            lblMessageCopyOrder.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDMS_Material> Supersede = new List<PDMS_Material>();
            ClearIssueGV();
            try
            {
                if (gvMaterialCopyOrder.Rows.Count == 0)
                {
                    lblMessageCopyOrder.Text = "Please select the material";
                    return;
                }
                for (int j = 0; j < gvMaterialCopyOrder.Rows.Count; j++)
                {

                    CheckBox cbSelectChild = (CheckBox)gvMaterialCopyOrder.Rows[j].FindControl("cbSelectChild");
                    if (cbSelectChild.Checked)
                    {
                        Label lblMaterial = (Label)gvMaterialCopyOrder.Rows[j].FindControl("lblMaterial");
                        TextBox txtPartQty = (TextBox)gvMaterialCopyOrder.Rows[j].FindControl("txtPartQty");
                        if (string.IsNullOrEmpty(txtPartQty.Text))
                        {
                            lblMessageCopyOrder.Text = "Please Enter the Quantity for these material " + lblMaterial.Text + ".";
                            return;
                        }
                        decimal value;
                        if (!decimal.TryParse(txtPartQty.Text, out value))
                        {
                            lblMessageCopyOrder.Text = "Please enter correct format in Qty";
                            return;
                        }
                        if (value < 1)
                        {
                            lblMessageCopyOrder.Text = "Please enter qty more than zero for these material " + lblMaterial.Text + ".";
                            return;
                        }
                    }
                }

                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
                for (int j = 0; j < gvMaterialCopyOrder.Rows.Count; j++)
                {
                    CheckBox cbSelectChild = (CheckBox)gvMaterialCopyOrder.Rows[j].FindControl("cbSelectChild");
                    if (cbSelectChild.Checked)
                    {
                        Label lblMaterial = (Label)gvMaterialCopyOrder.Rows[j].FindControl("lblMaterial");
                        TextBox txtPartQty = (TextBox)gvMaterialCopyOrder.Rows[j].FindControl("txtPartQty");
                        string MaterialCode = new BDMS_Material().GetMaterialSupersedeFinalByCode(lblMaterial.Text);
                        if (MaterialCode != lblMaterial.Text)
                        {
                            Supersede.Add(new PDMS_Material()
                            {
                                MaterialCode = lblMaterial.Text,
                                Supersede = new PSupersede() { Material = MaterialCode }
                            });
                        } 
                        List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode && s.Model.Division.DivisionID == Convert.ToInt32(ddlDivision.SelectedValue) && s.IsActive == true).ToList();
                        if (Material.Count == 0)
                        {
                            MaterialIssue.Add(MaterialCode, "Material " + MaterialCode + " Not Available.");
                            continue;
                        }
                        if (SOItem_Insert.Any(item => item.MaterialCode == lblMaterial.Text))
                        {
                            MaterialIssue.Add(MaterialCode, "Duplicate Material (" + MaterialCode + ") Found.");
                            continue;
                        }

                        try
                        {
                            AddMaterial(Material[0], Convert.ToInt32(Convert.ToDecimal(txtPartQty.Text)));
                        }
                        catch (Exception e1)
                        {
                            MaterialIssue.Add(Material[0].MaterialCode, e1.Message);
                        } 
                    }
                }

                FillMessage(Supersede, MaterialIssue);
                MPE_CopyOrder.Hide();
            }
            catch (Exception e1)
            {
                lblMessageCopyOrder.Text = e1.Message;
            }
        }
        protected void btnMaterialFromCart_Click(object sender, EventArgs e)
        {
            MPE_MaterialFromCart.Show();
            lblMessageMaterialFromCart.ForeColor = Color.Red;
            lblMessage.ForeColor = Color.Red;
            Dictionary<string, string> MaterialIssue = new Dictionary<string, string>();
            List<PDMS_Material> Supersede = new List<PDMS_Material>();
            ClearIssueGV();
            try
            {
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);
                for (int i = 0; i < gvMaterialFromCart.Rows.Count; i++)
                {
                    GridView gvMaterialFromCartItem = (GridView)gvMaterialFromCart.Rows[i].FindControl("gvMaterialFromCartItem");
                    for (int j = 0; j < gvMaterialFromCartItem.Rows.Count; j++)
                    {
                        CheckBox cbSelectChild = (CheckBox)gvMaterialFromCartItem.Rows[j].FindControl("cbSelectChild");
                        if (cbSelectChild.Checked)
                        {
                            Label lblMaterial = (Label)gvMaterialFromCartItem.Rows[j].FindControl("lblMaterial");
                            Label lblPartQty = (Label)gvMaterialFromCartItem.Rows[j].FindControl("lblPartQty");
                            string MaterialCode = lblMaterial.Text;

                            if (MaterialCode != lblMaterial.Text)
                            {
                                Supersede.Add(new PDMS_Material()
                                {
                                    MaterialCode = lblMaterial.Text,
                                    Supersede = new PSupersede() { Material = MaterialCode }
                                });
                            }
                            List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode && s.Model.Division.DivisionID == Convert.ToInt32(ddlDivision.SelectedValue) && s.IsActive == true).ToList();
                            if (Material.Count == 0)
                            {
                                MaterialIssue.Add(MaterialCode, "Material " + MaterialCode + " Not Available.");
                                continue;
                            }
                            if (SOItem_Insert.Any(item => item.MaterialCode == lblMaterial.Text))
                            {
                                MaterialIssue.Add(MaterialCode, "Duplicate Material (" + MaterialCode + ") Found.");
                                continue;
                            }
                            try
                            {
                                AddMaterial(Material[0],Convert.ToInt32(lblPartQty.Text));
                            }
                            catch (Exception e1)
                            {
                                MaterialIssue.Add(Material[0].MaterialCode, e1.Message); 
                            }
                        }
                    }
                }
                MPE_MaterialFromCart.Hide();
                FillMessage(Supersede, MaterialIssue);
            }
            catch (Exception e1)
            {
                lblMessageMaterialFromCart.Text = e1.Message;
            }
        }
        void ClearIssueGV()
        {
            gvSupersede.DataSource = null;
            gvSupersede.DataBind();
            gvMaterialIssue.DataSource = null;
            gvMaterialIssue.DataBind();
        }
        void FillMessage(List<PDMS_Material> Supersede, Dictionary<string, string> MaterialIssue)
        {
            if (Supersede.Count != 0)
            {
                gvSupersede.DataSource = Supersede;
                gvSupersede.DataBind();
                MPE_Supersede.Show();
            }

            if (MaterialIssue.Count != 0)
            {
                gvMaterialIssue.DataSource = MaterialIssue;
                gvMaterialIssue.DataBind();
                MPE_Supersede.Show();
            }
            gvSOItem.DataSource = SOItem_Insert;
            gvSOItem.DataBind();
         //   hdfItemCount.Value = PurchaseOrderItem_Insert.Count().ToString();

            if (Supersede.Count != 0)
            {
                gvSupersede.DataSource = Supersede;
                gvSupersede.DataBind();
                MPE_Supersede.Show();
            }
             
            fillItem();
        }

        protected string AddMaterial(PDMS_Material Material, int Qty)
        { 
                string Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
                string Dealer = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                decimal HDiscountPercent = Convert.ToDecimal(txtHeaderDiscountPercent.Text.Trim());
              //  int Qty1 = Convert.ToInt32(lblPartQty.Text);
                PSaleOrderItem_Insert item_Insert = new BDMS_SalesOrder().ReadItem(Material, Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlOfficeName.SelectedValue), Qty, Customer, Dealer, HDiscountPercent, 0, 0, ddlTaxType.SelectedItem.Text);
                SOItem_Insert.Add(item_Insert);
           

            return "";
        }

        protected void btnSearchCopyOrder_Click(object sender, EventArgs e)
        {
            MPE_CopyOrder.Show(); 

            if (string.IsNullOrEmpty(txtPoNumber.Text.Trim()))
            {
                lblMessageCopyOrder.Text = "Please enter the Purchase Order Number";
                lblMessageCopyOrder.ForeColor = Color.Red;
                return;
            }
            PApiResult Result = new BDMS_SalesOrder().GetSaleOrderHeader(null, null, null, txtPoNumber.Text.Trim(), null, null,Convert.ToInt32(ddlDealer.SelectedValue), null, Convert.ToInt32(ddlDivision.SelectedValue), null, null, null, 1, 1);
            List<PSaleOrder> SalesOrder = JsonConvert.DeserializeObject<List<PSaleOrder>>(JsonConvert.SerializeObject(Result.Data));
            if (SalesOrder.Count == 0)
            {
                lblMessageCopyOrder.Text = "Please check the Purchase Order Number";
                lblMessageCopyOrder.ForeColor = Color.Red;
                return;
            }
            PSaleOrder SOrder = new BDMS_SalesOrder().GetSaleOrderByID(SalesOrder[0].SaleOrderID);  
            gvMaterialCopyOrder.DataSource = SOrder.SaleOrderItems;
            gvMaterialCopyOrder.DataBind();
            btnCopyPoAdd.Visible = true;
        }

        protected void ChkMailH_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkMailH = (CheckBox)sender;
            foreach (GridViewRow row in gvMaterialCopyOrder.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbSelectChild = row.FindControl("cbSelectChild") as CheckBox;
                    cbSelectChild.Checked = (ChkMailH.Checked) ? true : false;
                }
            }
            MPE_CopyOrder.Show();
        }

        protected void cbSelectChild_CheckedChanged(object sender, EventArgs e)
        {
            bool ChkHeader = true;
            CheckBox ChkMailH = (CheckBox)gvMaterialCopyOrder.HeaderRow.FindControl("ChkMailH");
            foreach (GridViewRow row in gvMaterialCopyOrder.Rows)
            {
                CheckBox cbSelectChild = row.FindControl("cbSelectChild") as CheckBox;
                if (cbSelectChild.Checked == false)
                {
                    ChkHeader = false;
                }
            }
            ChkMailH.Checked = ChkHeader;
            MPE_CopyOrder.Show();
        }

        protected void btnCartSearch_Click(object sender, EventArgs e)
        {
            MPE_MaterialFromCart.Show();
            fillCart();
        }

       void fillCart()
        {
            int? DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            int? OfficeID = Convert.ToInt32(ddlOfficeName.SelectedValue);
            string OrderNo = txtCartNo.Text;
            string DateF = txtCartDateFrom.Text;
            string DateT = txtCartDateTo.Text;
            int? DivisionID = ddlCartDivision.SelectedValue == "0"?(int?)null : Convert.ToInt32(ddlCartDivision.SelectedValue);
            int? ModelID = ddlCartModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCartModel.SelectedValue);

            PApiResult Result = new BECatalogue().GetSpcCart(null, DealerID, OfficeID, OrderNo, DateF, DateT, DivisionID, ModelID, null, null);
            List<PspcCart> Cart = JsonConvert.DeserializeObject<List<PspcCart>>(JsonConvert.SerializeObject(Result.Data));
            gvMaterialFromCart.DataSource = Cart;
            gvMaterialFromCart.DataBind();

            for (int i = 0; i < gvMaterialFromCart.Rows.Count; i++)
            {
                GridView gvClaimInvoiceItem = (GridView)gvMaterialFromCart.Rows[i].FindControl("gvMaterialFromCartItem");
                Label lblspcCartID = (Label)gvMaterialFromCart.Rows[i].FindControl("lblspcCartID");
                gvClaimInvoiceItem.DataSource = Cart.Find(s => s.spcCartID == Convert.ToInt32(lblspcCartID.Text)).CartItem;
                gvClaimInvoiceItem.DataBind();
            }
        }

        protected void ddlCartDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_MaterialFromCart.Show();
            int? DivisionID = ddlCartDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCartDivision.SelectedValue);
            new DDLBind(ddlCartModel, new BECatalogue().GetSpcModel(null, DivisionID, null, true, null), "SpcModelCode", "SpcModelID");
        }
    }
}