using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class AddVariant : System.Web.UI.UserControl
    {
        public List<PSalesQuotationItem> Items
        {
            get
            {
                if (ViewState["AddVariant"] == null)
                {
                    ViewState["AddVariant"] = new List<PSalesQuotationItem>();
                }
                return (List<PSalesQuotationItem>)ViewState["AddVariant"];
            }
            set
            {
                ViewState["AddVariant"] = value;
            }
        }
        public PSalesQuotation Quotation
        {
            get
            {
                if (ViewState["AddVariantQuotation"] == null)
                {
                    ViewState["AddVariantQuotation"] = new PSalesQuotation();
                }
                return (PSalesQuotation)ViewState["AddVariantQuotation"];
            }
            set
            {
                ViewState["AddVariantQuotation"] = value;
            }
        }
         
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }
        public void FillMaster(int ProductTypeID, PSalesQuotation Quotation_)
        {
            Quotation=Quotation_;
            Items = new List<PSalesQuotationItem>();
            new DDLBind(ddlVariantType, new BDMS_Material().GetMaterialVariantType(ProductTypeID), "VariantName", "VariantTypeID");
            FillMaterial();
           // txtDiscount.Text = "0";
        }
        public List<PSalesQuotationItem> ReadMaterial()
        {
            List<PSalesQuotationItem> Items = new List<PSalesQuotationItem>();
            foreach(GridViewRow gv in gvMaterial.Rows)
            {
                TextBox txtQty = (TextBox)gv.FindControl("txtQty");

                if(string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    continue;
                } 
                int value;
                if (!int.TryParse(txtQty.Text.Trim(), out value))
                {
                    txtQty.BorderColor = Color.Red;
                    throw new Exception("Please enter the Correct Quantity Value");
                }

                Label lblMaterialCode = (Label)gv.FindControl("lblMaterialCode");
                Label lblMaterialID = (Label)gv.FindControl("lblMaterialID");
                if (Quotation.QuotationItems != null)
                {
                    foreach (PSalesQuotationItem Item in Quotation.QuotationItems)
                    {
                        if (Item.Material.MaterialCode == lblMaterialCode.Text)
                        {
                            throw new Exception("Material " + lblMaterialCode.Text + " already available"); 
                        }
                    }
                }
                decimal Qty = Convert.ToDecimal(txtQty.Text);
                PSalesQuotationItem MaterialTax = new BSalesQuotation().getMaterialTaxForQuotation(Quotation, lblMaterialCode.Text, false, Qty);
                if (MaterialTax == null)
                {
                    throw new Exception("Please maintain the price for Material " + lblMaterialCode.Text + " in SAP"); 
                }
                if (MaterialTax.Rate <= 0)
                {
                    throw new Exception("Please maintain the price for Material " + lblMaterialCode.Text + " in SAP"); 
                }
                MaterialTax.SalesQuotationID = Quotation.QuotationID;
                MaterialTax.Material = new PDMS_Material();
                MaterialTax.Material.MaterialCode = lblMaterialCode.Text;
                MaterialTax.Material.MaterialID = Convert.ToInt64(lblMaterialID.Text); 
                MaterialTax.Qty = Convert.ToInt32(txtQty.Text);
               // decimal P = (MaterialTax.Rate * Qty);
                MaterialTax.Discount = 0;

                MaterialTax.TaxableValue = (MaterialTax.Rate * Qty) - Convert.ToDecimal(MaterialTax.Discount);

                if (MaterialTax.SGST != 0)
                {
                    MaterialTax.CGST = MaterialTax.SGST;
                    MaterialTax.CGSTValue = MaterialTax.TaxableValue * MaterialTax.SGST / 100;
                    MaterialTax.SGSTValue = MaterialTax.TaxableValue * MaterialTax.SGST / 100;
                    MaterialTax.IGSTValue = 0;
                }
                else
                {
                    MaterialTax.CGST = 0;
                    MaterialTax.CGSTValue = 0;
                    MaterialTax.SGSTValue = 0;
                    MaterialTax.IGSTValue = MaterialTax.TaxableValue * MaterialTax.IGST / 100;
                }
                if (MaterialTax.SGSTValue == 0 && MaterialTax.IGSTValue == 0)
                {
                    throw new Exception("GST Tax value not found this material..!"); 
                }
                MaterialTax.CreatedBy = new PUser() { UserID = PSession.User.UserID };
                Items.Add(MaterialTax);

            }
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red; 
           // Clear();

            return Items;
        }

        
        //public string ValidationProduct()
        //{ 
        //    //ddlMaterial.BorderColor = Color.Silver;
        //    ////ddlProduct.BorderColor = Color.Silver;
        //    // txtQty.BorderColor = Color.Silver;
        //    // txtDiscount.BorderColor = Color.Silver;

        //    //if (ddlMaterial.SelectedValue == "0")
        //    //{ 
        //    //    ddlMaterial.BorderColor = Color.Red;
        //    //    return "Please select the Material";
        //    //}
        //    ////if (ddlProduct.SelectedValue == "0")
        //    ////{
        //    ////    Message = Message + "<br/>Please select the Product";
        //    ////    ddlProduct.BorderColor = Color.Red;
        //    ////}
        //    //if (string.IsNullOrEmpty(txtQty.Text.Trim()))
        //    //{
        //    //    txtQty.BorderColor = Color.Red;
        //    //    return "Please enter the Quantity"; 
        //    //}
        //    //int value;
        //    //if (int.TryParse(txtQty.Text.Trim(), out value))
        //    //{
        //    //    txtQty.BorderColor = Color.Red;
        //    //    return "Please enter the Correct Quantity Value";
        //    //}

        //    //if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
        //    //{
        //    //    txtDiscount.BorderColor = Color.Red;
        //    //    return "Please enter the Discount"; 
        //    //} 
        //    //decimal decimalValue;
        //    //if (decimal.TryParse(txtQty.Text.Trim(), out decimalValue))
        //    //{
        //    //    txtDiscount.BorderColor = Color.Red;
        //    //    return "Please enter the Correct Discount Value";
        //    //}
        //    return "";
        //}

        protected void ddlVariantType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMaterial();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //lblMessage.Visible = true;
            //lblMessage.ForeColor = Color.Red;
            //PDMS_Material MM = new BDMS_Material().GetMaterialListSQL(Convert.ToInt32(ddlMaterial.SelectedValue), null, null, null, null)[0];
            //Decimal.TryParse(txtDiscount.Text, out Decimal Discount);

            //string Message = ValidationProduct(); 
            //if(!string.IsNullOrEmpty(Message))
            //{
            //    lblMessage.Text = Message;
            //}
            //string Material = MM.MaterialCode;

            //if (Items != null)
            //{
            //    foreach (PSalesQuotationItem Item in Items)
            //    {
            //        if (Item.Material.MaterialCode == MM.MaterialCode)
            //        {
            //            lblMessage.Text = "Material " + Material + " already available";
            //            return;
            //        }
            //    }
            //} 
            //if (Quotation.QuotationItems != null)
            //{
            //    foreach (PSalesQuotationItem Item in Quotation.QuotationItems)
            //    {
            //        if (Item.Material.MaterialCode == MM.MaterialCode)
            //        {
            //            lblMessage.Text = "Material " + Material + " already available";
            //            return;
            //        }
            //    }
            //}
            //decimal Qty = Convert.ToDecimal(txtQty.Text);
            //PSalesQuotationItem MaterialTax = new BSalesQuotation().getMaterialTaxForQuotation(Quotation, Material, false, Qty);
            //if (MaterialTax == null)
            //{
            //    lblMessage.Text = "Please maintain the price for Material " + Material + " in SAP";
            //    return;
            //}
            //if (MaterialTax.Rate <= 0)
            //{
            //    lblMessage.Text = "Please maintain the price for Material " + Material + " in SAP" ;
            //    return;
            //}
            //MaterialTax.SalesQuotationID = Quotation.QuotationID;
            //MaterialTax.Material = new PDMS_Material();
            //MaterialTax.Material.MaterialCode = MM.MaterialCode;
            //MaterialTax.Material.MaterialID = MM.MaterialID; 
            ////  MaterialTax.Plant = new PPlant() { PlantID = Convert.ToInt32(ddlPlant.SelectedValue) };
            //MaterialTax.Qty = Convert.ToInt32(txtQty.Text);
            //decimal P = (MaterialTax.Rate * Convert.ToDecimal(txtQty.Text)); 
            //MaterialTax.Discount = Discount;

            //MaterialTax.TaxableValue = (MaterialTax.Rate * Convert.ToDecimal(txtQty.Text)) - Convert.ToDecimal(MaterialTax.Discount);

            //if (MaterialTax.SGST != 0)
            //{
            //    MaterialTax.CGST = MaterialTax.SGST;
            //    MaterialTax.CGSTValue = MaterialTax.TaxableValue * MaterialTax.SGST / 100;
            //    MaterialTax.SGSTValue = MaterialTax.TaxableValue * MaterialTax.SGST / 100;
            //    MaterialTax.IGSTValue = 0;
            //}
            //else
            //{
            //    MaterialTax.CGST = 0;
            //    MaterialTax.CGSTValue = 0;
            //    MaterialTax.SGSTValue = 0;
            //    MaterialTax.IGSTValue = MaterialTax.TaxableValue * MaterialTax.IGST / 100;
            //}
            //if (MaterialTax.SGSTValue == 0 && MaterialTax.IGSTValue == 0)
            //{
            //    lblMessage.Text = "GST Tax value not found this material..!" ;
            //    return;
            //}
            //MaterialTax.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            //Items.Add(MaterialTax);
            //lblMessage.ForeColor = Color.Green;
            //lblMessage.Text = "Material Added.";
            //Clear();
        }

        void Clear()
        { 
            FillMaterial();
            //txtQty.Text = "";
            //txtDiscount.Text = "0";
        }

        void FillMaterial()
        {

            List<PMaterialVariantTypeMapping> mm = new BDMS_Material().GetMaterialByProductIDAndMaterialCategoryID(Quotation.LeadProduct.Product.ProductID, Convert.ToInt32(ddlVariantType.SelectedValue));
            //ddlMaterial.Items.Clear();
            //ddlMaterial.Items.Insert(0, new ListItem("Select", "0"));
            //int i = 0;
            //foreach (PMaterialVariantTypeMapping M in mm)
            //{
            //    i = i + 1;
            //    ddlMaterial.Items.Insert(i, new ListItem(M.Material.MaterialDescription +" - "+M.Material.MaterialCode, M.Material.MaterialID.ToString()));
            //}
            gvMaterial.DataSource = mm;
            gvMaterial.DataBind();

            //List<PSalesQuotationItem> Materials = new List<PSalesQuotationItem>();
            //foreach (PMaterialVariantTypeMapping M in mm)
            //{
            //    string Material = M.Material.MaterialCode;

            //    //string OrderType = "";
            //    //string Vendor = "";
            //    //string IV_SEC_SALES = "";
            //    //string PRICEDATE = "";
            //     Boolean IsWarrenty = false;
            //    //OrderType = "DEFAULT_SEC_AUART"; 

            //    //if (M.Material.MaterialType == "FERT")
            //    //{
            //    //    //  lblMessageProduct.Text = "Please check the Material";
            //    //    return;
            //    //} 

            //    PSalesQuotationItem MaterialTax = new BSalesQuotation().getMaterialTaxForQuotation(Quotation, Material, IsWarrenty, 1);

            //    if (MaterialTax == null)
            //    {
            //        // lblMessageProduct.Text = "Please maintain the price for Material " + Material + " in SAP";
            //        return;
            //    }
            //    if (MaterialTax.Rate <= 0)
            //    {
            //        // lblMessageProduct.Text = "Please maintain the price for Material " + Material + " in SAP";
            //        return;
            //    }

            //    MaterialTax.SalesQuotationID = Quotation.QuotationID;
            //    MaterialTax.Material = new PDMS_Material();
            //    MaterialTax.Material.MaterialCode = M.Material.MaterialCode;
            //    MaterialTax.Material.MaterialDescription = M.Material.MaterialDescription;
            //    MaterialTax.Material.MaterialID = M.Material.MaterialID;

            //    //  MaterialTax.Rate  

            //    Materials.Add(MaterialTax);
            //}

            //gvMaterial.DataSource = Materials;
            //gvMaterial.DataBind();
        }

        protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lblMaterialID = (Label)e.Row.FindControl("lblMaterialID");
                    List<PMaterialDrawing> MaterialDrawing = new BDMS_Material().GetMaterialDrawing(Convert.ToInt32(lblMaterialID.Text));
                    if (MaterialDrawing.Count == 1)
                    {
                        string PathName = Server.MapPath(UIHelper.MaterialDrawingFolder+"/" + MaterialDrawing[0].MaterialDrawingID + "." + Path.GetExtension(MaterialDrawing[0].FileName));

                        if (!File.Exists(PathName))
                        {
                            if (!Directory.Exists(Server.MapPath(UIHelper.MaterialDrawingFolder)))
                            {
                                Directory.CreateDirectory(Server.MapPath(UIHelper.MaterialDrawingFolder));
                            }
                            PAttachedFile Files = new BDMS_Material().GetAttachedFileMaterialDrawingForDownload(MaterialDrawing[0].MaterialDrawingID + Path.GetExtension(MaterialDrawing[0].FileName));

                            File.WriteAllBytes(Server.MapPath(UIHelper.MaterialDrawingFolder + "/" + MaterialDrawing[0].MaterialDrawingID + "." + Path.GetExtension(MaterialDrawing[0].FileName)), Files.AttachedFile);
                        }
                        System.Web.UI.WebControls.Image lblMaterialImage = (System.Web.UI.WebControls.Image)e.Row.FindControl("lblMaterialImage");
                        lblMaterialImage.ImageUrl = UIHelper.MaterialDrawingFolder +"/" + MaterialDrawing[0].MaterialDrawingID + "." + Path.GetExtension(MaterialDrawing[0].FileName);
                    }
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
    }
}