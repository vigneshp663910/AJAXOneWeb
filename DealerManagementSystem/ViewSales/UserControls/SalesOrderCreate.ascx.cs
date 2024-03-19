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

            new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID", true, "Select");

            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(0, null, null), "OfficeName", "OfficeID", true, "Select");
            //new DDLBind(ddlSalesEngineer, 0, "ContactName", "UserID");

            new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null, null, null), "Product", "ProductID", true, "Select");
            cxExpectedDeliveryDate.StartDate = DateTime.Now;
            txtExpectedDeliveryDate.Text = DateTime.Now.ToShortDateString();
            ddlDivision.SelectedValue = "15"; 
            ddlDivision.Enabled = false;
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
            txtBoxHeaderDiscountPercent.Text = "0";
            ClearHeader();
            ClearItem();
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? CDealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(CDealerID, null, null), "OfficeName", "OfficeID", true, "Select");

            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
            new DDLBind(ddlSalesEngineer, DealerUser, "ContactName", "UserID");
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
                int MaterialID = new BDMS_Material().GetMaterialSupersedeFinalByID(Convert.ToInt32(hdfMaterialID.Value));
                string MessageSupersede = "";
                PDMS_Material m = new BDMS_Material().GetMaterialListSQL(MaterialID, null, null, null, null)[0];

                if (MaterialID != Convert.ToInt32(hdfMaterialID.Value))
                {
                    lblMessage.Text = MessageSupersede = "Material :" + hdfMaterialCode.Value + "Supersede to " + "Material :" + m.MaterialCode;
                }

                Message = AddMat(m, txtQty.Text.Trim());
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
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
            SO.HeaderDiscountPercentage = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());

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
            ddlDealer.SelectedValue = "0";
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
            ddlTaxType.SelectedValue = "0";
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
        protected void btnSaveSOItem_Click(object sender, EventArgs e)
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
                if (SOItem_Insert.Count == 0)
                {
                    lblMessage.Text = "Please add Material.";
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
                Response.Redirect("PurchaseOrder.aspx");

                //lblMessage.Text = Result.Message;
                //lblMessage.ForeColor = Color.Green;
                //ClearHeader();
                //ClearItem();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void txtBoxHeaderDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            foreach (GridViewRow row in gvSOItem.Rows)
            {
                Label MaterialID = (Label)row.FindControl("lblMaterialID");
                Label lblDiscountValue = (Label)row.FindControl("lblDiscountValue");
                Label lblTaxableValue = (Label)row.FindControl("lblTaxableValue");
                Label lblTaxValue = (Label)row.FindControl("lblTaxValue");
                Label lblNetAmount = (Label)row.FindControl("lblNetAmount");

                foreach (PSaleOrderItem_Insert SOI in SOItem_Insert)
                {
                    decimal HDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
                    decimal IDiscount = Convert.ToDecimal(((TextBox)row.FindControl("txtBoxDiscountPercent")).Text.Trim());

                    if ((HDiscount + IDiscount < 0) || (HDiscount + IDiscount >= 100))
                    {
                        lblMessage.Text = "Discount Percentage cannot be less than 0 and more than 100.";
                        return;
                    }

                    if (SOI.MaterialID == Convert.ToInt64(MaterialID.Text))
                    {

                        decimal discountValue = SOI.Value * ((HDiscount + IDiscount) / 100);
                        decimal discountedPrice = SOI.Value - discountValue;
                        SOI.ItemDiscountPercentage = IDiscount;
                        SOI.DiscountValue = discountValue;
                        SOI.TaxableValue = discountedPrice;

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

            }
            FillAmountCall();
        }
        protected void txtBoxDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            TextBox txtBoxDiscountPercent = (TextBox)gvRow.FindControl("txtBoxDiscountPercent");
            Label MaterialID = (Label)gvRow.FindControl("lblMaterialID");

            //Label lblDiscountAmount = (Label)gvRow.FindControl("lblDiscountAmount");
            Label lblDiscountValue = (Label)gvRow.FindControl("lblDiscountValue");
            Label lblTaxableValue = (Label)gvRow.FindControl("lblTaxableValue");
            Label lblTaxValue = (Label)gvRow.FindControl("lblTaxValue");
            Label lblNetAmount = (Label)gvRow.FindControl("lblNetAmount");

            foreach (PSaleOrderItem_Insert SOI in SOItem_Insert)
            {
                decimal HDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());
                decimal IDiscount = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtBoxDiscountPercent")).Text.Trim());

                if ((HDiscount + IDiscount < 0) || (HDiscount + IDiscount >= 100))
                {
                    lblMessage.Text = "Discount Percentage cannot be less than 0 and more than 100.";
                    return;
                }

                if (SOI.MaterialID == Convert.ToInt64(MaterialID.Text))
                {
                    decimal discountValue = SOI.Value * ((HDiscount + IDiscount) / 100);
                    decimal discountedPrice = SOI.Value - discountValue;
                    SOI.ItemDiscountPercentage = IDiscount;
                    SOI.DiscountValue = discountValue;
                    SOI.TaxableValue = discountedPrice;

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
            List<PDMS_EquipmentHeader> EQs = new BDMS_Equipment().GetEquipmentForCreateICTicket(Convert.ToInt64(hdfCustomerId.Value), null, null);
            new DDLBind(ddlEquipment, EQs, "EquipmentSerialNo", "EquipmentHeaderID", true, "Select");

        }

        protected void BtnAvailability_Click(object sender, EventArgs e)
        {
            string Message = Validation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessage.Text = Message;
                return;
            }
            PDealerStock s = new BInventory().GetDealerStockCountByID(Convert.ToInt32(ddlDealer.SelectedValue), Convert.ToInt32(ddlOfficeName.SelectedValue), Convert.ToInt64(hdfMaterialID.Value));

            if (s != null)
            {
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
                    //             string Message = Validation();
                    //             if (!string.IsNullOrEmpty(Message))
                    //             {
                    //                 lblMessage.Text = Message;
                    //                 return;
                    //             }
                    //             MaterialCart = new BDMS_PurchaseOrder().GetPurchaseOrderFromCart(new BDMS_Dealer().GetDealer(Convert.ToInt32(ddlDealer.SelectedValue), null, null, null)[0].DealerCode);

                    //             DataTable dt = MaterialCart.AsEnumerable()
                    //.GroupBy(r => new { OrderNo = r["OrderNo"], OrderDate = r["OrderDate"], CustomerCode = r["CustomerCode"], DealerCode = r["DealerCode"] })
                    //.Select(g => g.OrderBy(r => r["OrderNo"]).First())
                    //.CopyToDataTable();

                    //             gvMaterialFromCart.DataSource = dt;
                    //             gvMaterialFromCart.DataBind();
                    //             MPE_MaterialFromCart.Show();
                }
                else if (lbActions.ID == "lbCopyFromPO")
                {
                    //string Message = Validation();
                    //if (!string.IsNullOrEmpty(Message))
                    //{
                    //    lblMessage.Text = Message;
                    //    return;
                    //}
                    //MPE_CopyOrder.Show();
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
                List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, null, null, null, null);

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

                                string MaterialCode = new BDMS_Material().GetMaterialSupersedeFinalByCode(Convert.ToString(Cells[1].Value));
                                MaterialCode = MaterialCode.Trim();
                                // PDMS_Material m = new BDMS_Material().GetMaterialListSQL(MaterialID, TMaterialCode, null, null, null)[0];

                                if (MaterialCode != Convert.ToString(Cells[1].Value))
                                {
                                    Supersede.Add(new PDMS_Material()
                                    {
                                        MaterialCode = Convert.ToString(Cells[1].Value),
                                        Supersede = new PSupersede() { Material = MaterialCode }
                                    });
                                }
                                //   List<PDMS_Material> Material = new BDMS_Material().GetMaterialListSQL(null, MaterialCode, null, null, null);

                                List<PDMS_Material> Material = Materials.Where(s => s.MaterialCode == MaterialCode).ToList();
                                if (Material.Count == 0)
                                {
                                    MaterialIssue.Add(MaterialCode, "Material (" + MaterialCode + ") is not available.");
                                    continue;
                                    // lblMessageMaterialUpload.Text = "Check Material (" + Material[0].MaterialCode + ").";
                                    // return;
                                }

                                if (SOItem_Insert.Any(item => item.MaterialID == Material[0].MaterialID))
                                {
                                    MaterialIssue.Add(Material[0].MaterialCode, "Dublicate Material (" + Material[0].MaterialCode + ") Found. It is removed in list");
                                    continue;
                                    //lblMessageMaterialUpload.Text = "Dublicate Material (" + Material[0].MaterialCode + ") Found.";
                                    //return;
                                }

                                if (Material.Count > 0)
                                {
                                    string Message = AddMat(Material[0], Convert.ToString(Cells[2].Value));
                                    if (!string.IsNullOrEmpty(Message))
                                    {
                                        //SOItem_Insert = null;
                                        //gvSOItem.DataSource = SOItem_Insert;
                                        //gvSOItem.DataBind();

                                        MaterialIssue.Add(Material[0].MaterialCode, Message);
                                        continue;

                                        //lblMessageMaterialUpload.Text = Message; 
                                        //return;
                                    }
                                }
                            }
                        }
                    }
                }

                MPE_MaterialUpload.Hide();
                if (Supersede.Count != 0)
                {
                    gvSupersede.DataSource = Supersede;
                    gvSupersede.DataBind();
                    MPE_Supersede.Show();
                }

                if (MaterialIssue.Count != 0)
                {
                    //SOItem_Insert = null;  
                    gvMaterialIssue.DataSource = MaterialIssue;
                    gvMaterialIssue.DataBind();
                    MPE_Supersede.Show();
                }

                gvSOItem.DataSource = SOItem_Insert;
                gvSOItem.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected string AddMat(PDMS_Material m, string Qty)
        {
            try
            {
                //PSaleOrderItem_Insert SoI = ReadItem();
                PSaleOrderItem_Insert SoI = new PSaleOrderItem_Insert();
                SoI.MaterialID = m.MaterialID;
                SoI.MaterialCode = m.MaterialCode;
                SoI.Quantity = Convert.ToInt32(Qty);
                //PDMS_Material m = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(SoI.MaterialID), null, null, null, null)[0];
                if (string.IsNullOrEmpty(m.HSN))
                {
                    return "HSN Code is not updated for this Material. Please contact Parts Admin.";
                }

                PMaterial Mat = null;
                if (m.MaterialGroup != "887")
                {

                    PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
                    MaterialPrice.Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
                    MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
                    //MaterialPrice.OrderType = "101_DSSOR_SALES_ORDER_HDR";
                    MaterialPrice.OrderType = "DEFAULT_SEC_AUART";
                    MaterialPrice.Division = "SP";
                    MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
                    MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
                    {
                        ItemNo = "10",
                        Material = SoI.MaterialCode,
                        Quantity = SoI.Quantity
                    });
                    List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
                    Mat = Mats[0];
                    if (Mat.CurrentPrice <= 0)
                    {
                        return "Please maintain the price for Material " + SoI.MaterialCode + " in SAP.";
                    }
                    if (Mat.SGST <= 0 && Mat.IGST <= 0)
                    {
                        return "Please maintain the Tax for Material " + SoI.MaterialCode + " in SAP.";
                    }
                    if (Mat.SGSTValue <= 0 && Mat.IGSTValue <= 0)
                    {
                        return "GST Tax value not found this Material " + SoI.MaterialCode + " in SAP.";
                    }
                }
                else
                {

                    Mat = new PMaterial();
                    Mat.Discount = 0;
                    Mat.CurrentPrice = m.CurrentPrice;
                    Mat.TaxablePrice = m.CurrentPrice * SoI.Quantity;
                    if (ddlTaxType.SelectedValue == "1")
                    {
                        Mat.SGST = m.TaxPercentage;
                        Mat.CGST = m.TaxPercentage;
                        SoI.SGSTValue = Mat.SGST * Mat.CurrentPrice * SoI.Quantity / 100;
                        SoI.CGSTValue = Mat.CGST * Mat.CurrentPrice * SoI.Quantity / 100;

                        Mat.IGST = 0;
                        SoI.IGSTValue = 0;
                    }
                    else
                    {
                        Mat.SGST = 0;
                        Mat.CGST = 0;
                        SoI.SGSTValue = 0;
                        SoI.CGSTValue = 0;

                        Mat.IGST = m.TaxPercentage * 2;
                        SoI.IGSTValue = Mat.IGST * Mat.CurrentPrice * SoI.Quantity / 100;
                    }
                }

                //Mat.CurrentPrice = Convert.ToDecimal(1000);
                //Mat.Discount = Convert.ToDecimal(0);
                //Mat.TaxablePrice = Convert.ToDecimal(1000);
                //Mat.SGST = Convert.ToDecimal(9);
                //Mat.SGSTValue = Convert.ToDecimal(90);
                //Mat.CGST = Convert.ToDecimal(9);
                //Mat.CGSTValue = Convert.ToDecimal(90);
                //Mat.IGST = Convert.ToDecimal(0);
                //Mat.IGSTValue = Convert.ToDecimal(0);



                SoI.PerRate = Mat.CurrentPrice / SoI.Quantity;
                SoI.Value = Mat.CurrentPrice;
                //SoI.Discount = Mat.Discount;
                //SoI.TaxableValue = Mat.TaxablePrice;

                decimal HDiscount = Convert.ToDecimal(txtBoxHeaderDiscountPercent.Text.Trim());

                if (HDiscount >= 100)
                {
                    return "Discount Percentage cannot exceed 100.";
                }

                //SoI.Discount = Mat.CurrentPrice - (Mat.CurrentPrice * HDiscount / 100);
                //SoI.TaxableValue = SoI.Discount;
                //SoI.Discount = HDiscount > 0 ? (Mat.CurrentPrice - (Mat.CurrentPrice * (HDiscount / 100))) : Mat.Discount;
                SoI.ItemDiscountPercentage = HDiscount > 0 ? 0 : Mat.Discount;
                SoI.DiscountValue = HDiscount > 0 ? (Mat.CurrentPrice * (HDiscount / 100)) : Mat.Discount;
                SoI.TaxableValue = HDiscount > 0 ? (Mat.CurrentPrice - (Mat.CurrentPrice * (HDiscount / 100))) : Mat.TaxablePrice;
                //SoI.SGST = Mat.SGST;
                //SoI.SGSTValue = Mat.SGSTValue;
                //SoI.CGST = Mat.CGST;
                //SoI.CGSTValue = Mat.CGSTValue;
                //SoI.IGST = Mat.IGST;
                //SoI.IGSTValue = Mat.IGSTValue;
                SoI.StatusID = 19;
                SoI.MaterialDescription = m.MaterialDescription;
                SoI.HSN = m.HSN;
                SoI.UOM = m.BaseUnit;

                if (ddlTaxType.SelectedValue == "1")
                {
                    SoI.SGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                    SoI.SGSTValue = (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                    SoI.CGST = (Mat.SGST + Mat.CGST + Mat.IGST) / 2;
                    SoI.CGSTValue = (Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue) / 2;
                    SoI.IGST = 0;
                    SoI.IGSTValue = 0;
                }
                else
                {
                    SoI.SGST = 0;
                    SoI.SGSTValue = 0;
                    SoI.CGST = 0;
                    SoI.CGSTValue = 0;
                    SoI.IGST = Mat.SGST + Mat.CGST + Mat.IGST;
                    SoI.IGSTValue = Mat.SGSTValue + Mat.CGSTValue + Mat.IGSTValue;
                }

                SoI.SGSTValue = SoI.TaxableValue * (SoI.SGST / 100);
                SoI.CGSTValue = SoI.TaxableValue * (SoI.CGST / 100);
                SoI.IGSTValue = SoI.TaxableValue * (SoI.IGST / 100);

                SoI.NetAmount = SoI.TaxableValue + SoI.SGSTValue + SoI.CGSTValue + SoI.IGSTValue;
                SOItem_Insert.Add(SoI);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            // lblMessageMaterialUpload.ForeColor = Color.Red;
            // lblMessageMaterialUpload.Visible = true;
            // lblMessage.ForeColor = Color.Red;
            // lblMessage.Visible = true;
            // string Message = "";
            // if (SOItem_Insert.Any(item => item.MaterialID == Convert.ToInt32(MaterialID)))
            // {
            //     Message = "Dublicate Material Found.";
            //     lblMessageMaterialUpload.Text = Message;
            //     MPE_MaterialUpload.Show();
            //     return Message;
            // }
            // //   List<PDMS_Material> Materials = new BDMS_Material().GetMaterialAutocompleteN(MaterialCode, "", Convert.ToInt32(ddlDivision.SelectedValue), "false");

            // List<PDMS_Material> Materials = new BDMS_Material().GetMaterialListSQL(null, MaterialCode, Convert.ToInt32(ddlDivision.SelectedValue), null, null);

            // if (Materials.Count == 0)
            // {
            //     Message = "Material " + MaterialCode + " Not Available.";
            //     lblMessageMaterialUpload.Text = Message;
            //     MPE_MaterialUpload.Show();
            //     return Message;
            // } 
            // PSaleOrderItem_Insert PoI = new PSaleOrderItem_Insert();
            // PoI.MaterialID = Convert.ToInt32(MaterialID);
            // PoI.MaterialCode = MaterialCode;
            // // SM.SupersedeYN = cbSupersedeYN.Checked;
            // PoI.Quantity = Convert.ToInt32(Qty);

            // PDMS_Material m = new BDMS_Material().GetMaterialListSQL(PoI.MaterialID, null, null, null, null)[0];
            // PoI.MaterialDescription = m.MaterialDescription;
            // PoI.UOM = m.BaseUnit;
            // if (string.IsNullOrEmpty(m.HSN))
            // {
            //     Message = "HSN is Not updated for this material " + m.MaterialCode + ". Please contact Admin.";
            //     lblMessageMaterialUpload.Text = Message;
            //     MPE_MaterialUpload.Show();
            //     return Message;
            // }

            // List<PMaterial_Api> Material_SapS = new List<PMaterial_Api>();
            // Material_SapS.Add(new PMaterial_Api()
            // {
            //     MaterialCode = PoI.MaterialCode,
            //     Quantity = PoI.Quantity,
            //     Item = (Material_SapS.Count + 1) * 10
            // }); 

            // if (m.MaterialGroup != "887")
            // {
            //     PSapMatPrice_Input MaterialPrice = new PSapMatPrice_Input();
            //     MaterialPrice.Customer = new BDMS_Customer().GetCustomerByID(Convert.ToInt32(hdfCustomerId.Value)).CustomerCode;
            //     MaterialPrice.Vendor = new BDealer().GetDealerByID(Convert.ToInt32(ddlDealer.SelectedValue), "").DealerCode;
            //     MaterialPrice.OrderType = "DEFAULT_SEC_AUART";
            //     //MaterialPrice.OrderType = new BProcurementMasters().GetPurchaseOrderType(Convert.ToInt32(ddlPurchaseOrderType.SelectedValue), null)[0].SapOrderType;

            //     MaterialPrice.Item = new List<PSapMatPriceItem_Input>();
            //     MaterialPrice.Item.Add(new PSapMatPriceItem_Input()
            //     {
            //         ItemNo = "10",
            //         Material = PoI.MaterialCode,
            //         Quantity = PoI.Quantity
            //     });


            //     List<PMaterial> Mats = new BDMS_Material().MaterialPriceFromSapApi(MaterialPrice);
            //     PMaterial Mat = Mats[0];

            //     if (Mat.CurrentPrice == 0)
            //     {
            //         Message = "Price is Not updated for this material " + PoI.MaterialCode + ". Please contact Admin.";
            //         lblMessageMaterialUpload.Text = Message;
            //         MPE_MaterialUpload.Show();
            //         return Message;
            //     }
            //     PoI.PerRate = Mat.CurrentPrice / PoI.Quantity;
            //     PoI.Value = Mat.CurrentPrice;
            //     PoI.DiscountValue = Mat.Discount;
            //     PoI.TaxableValue = Mat.TaxablePrice;
            //     PoI.SGST = Mat.SGST;
            //     PoI.SGSTValue = Mat.SGSTValue;
            //     PoI.CGST = Mat.SGST;
            //     PoI.CGSTValue = Mat.SGSTValue;
            //     PoI.IGST = Mat.IGST;
            //     PoI.IGSTValue = Mat.IGSTValue;


            // }
            // else
            // {
            //     PDMS_Material MM_SQL = Materials[0]; //new BDMS_Material().GetMaterialListSQL(null, Material, null, null, null)[0]; 
            //     PoI.PerRate = MM_SQL.CurrentPrice;
            //     PoI.Value = MM_SQL.CurrentPrice * PoI.Quantity;
            //     PoI.DiscountValue = 0;
            //     PoI.TaxableValue = PoI.Value;

            //     if (ddlTaxType.SelectedItem.Text == "")
            //     {
            //         PoI.IGST = MM_SQL.TaxPercentage;
            //         PoI.IGSTValue = MM_SQL.TaxPercentage * MM_SQL.CurrentPrice * 2 / 100;
            //     }
            //     else
            //     {
            //         PoI.SGST = MM_SQL.TaxPercentage;
            //         PoI.SGSTValue = MM_SQL.TaxPercentage * MM_SQL.CurrentPrice / 100;
            //         PoI.CGST = MM_SQL.TaxPercentage;
            //         PoI.CGSTValue = MM_SQL.TaxPercentage * MM_SQL.CurrentPrice / 100; 
            //     }
            // } 
            // //  PoI.Tax = Mat.SGST + Mat.SGST + Mat.IGST;
            // //  PoI.TaxValue = Mat.SGSTValue + Mat.SGSTValue + Mat.IGSTValue;
            // //  PoI.NetValue = PoI.TaxableAmount + PoI.SGSTValue + PoI.CGSTValue + PoI.IGSTValue;
            // SOItem_Insert.Add(PoI);
            //// PoI.Item = SOItem_Insert.Count * 10;
            // fillItem();
            // ClearItem();
            // return Message;
        }

        public string Validation()
        {
            ddlDealer.BorderColor = Color.Silver;
            ddlOfficeName.BorderColor = Color.Silver;
            txtCustomer.BorderColor = Color.Silver;
            txtContactPersonNumber.BorderColor = Color.Silver;
            ddlDivision.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtExpectedDeliveryDate.BorderColor = Color.Silver;
            ddlTaxType.BorderColor = Color.Silver;
            txtBoxHeaderDiscountPercent.BorderColor = Color.Silver;
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
            if (!decimal.TryParse(txtBoxHeaderDiscountPercent.Text, out value))
            {
                txtBoxHeaderDiscountPercent.BackColor = Color.Red;
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
            foreach (PSaleOrderItem_Insert Item in SOItem_Insert)
            {
                if (Item.MaterialID == Convert.ToInt32(hdfMaterialID.Value))
                {
                    return "Duplicate Material.";
                }
            }
            decimal value;
            if (!decimal.TryParse(txtQty.Text, out value))
            {
                return "Please enter correct format in Qty.";
            }
            return "";
        }
    }
}