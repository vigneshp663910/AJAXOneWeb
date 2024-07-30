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

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class ModelView : System.Web.UI.UserControl
    {
        public PProduct ModelByID
        {
            get
            {
                if (ViewState["ModelByID"] == null)
                {
                    ViewState["ModelByID"] = new PProduct();
                }
                return (PProduct)ViewState["ModelByID"];
            }
            set
            {
                ViewState["ModelByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillModelByID(int? ProductID)
        {
            ModelByID = new BDMS_Master().GetProduct(ProductID, null, null, null)[0];
            lblMake.Text = ModelByID.Make.Make;
            lblProductType.Text = ModelByID.ProductType.ProductType;
            lblModel.Text = ModelByID.Product;
            GetProductDrawing();
            GetProductSpecification();
            //ActionControlMange();
        }

        protected void lnkBtnActions_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Add Drawing")
            {
                lblAddDrawingMessage.Text = "";
                lblAddDrawingMessage.ForeColor = Color.Red;
                lblAddDrawingMessage.Visible = true;
                List<PProductDrawingType> PDT = new BDMS_Master().GetProductDrawingType(null, null);
                new DDLBind(ddlDrawingType, PDT, "ProductDrawingTypeName", "ProductDrawingTypeID");
                txtDrawingDescription.Text = "";
                MPE_AddDrawing.Show();
            }
            else if (lbActions.Text == "Edit Model")
            {
                lblEditProductMessage.Text = "";
                lblEditProductMessage.ForeColor = Color.Red;
                lblEditProductMessage.Visible = true;
                GetEditModel();
                MPE_EditProduct.Show();
            }
            else if (lbActions.Text == "Add Specification")
            {
                lblProductSpecificationMessage.Text = "";
                lblProductSpecificationMessage.ForeColor = Color.Red;
                lblProductSpecificationMessage.Visible = true;
                //GetEditModel();
                txtSpecText.Text = "";
                txtSpecDesc.Text = "";
                txtOrderByNo.Text = "";
                MPE_ProductSpecification.Show();
            }
        }
        protected void btnAddDrawing_Click(object sender, EventArgs e)
        {
            lblAddDrawingMessage.Text = "";
            lblAddDrawingMessage.ForeColor = Color.Red;
            lblAddDrawingMessage.Visible = true;
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {
                if (ddlDrawingType.SelectedValue == "0")
                {
                    lblAddDrawingMessage.Text = "Please select Drawing Type...!";
                    MPE_AddDrawing.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtDrawingDescription.Text))
                {
                    lblAddDrawingMessage.Text = "Please enter Drawing Description...!";
                    MPE_AddDrawing.Show();
                    return;
                }
                if (fileUpload.PostedFile != null)
                {
                    if (fileUpload.PostedFile.FileName.Length > 0)
                    {
                        if (fileUpload.PostedFile.FileName.Length == 0)
                        {
                            lblAddDrawingMessage.Text = "Please select the file...!";
                            MPE_AddDrawing.Show();
                            return;
                        }
                    }
                }


                HttpPostedFile file = fileUpload.PostedFile;

                if (file.ContentType != "image/png" && file.ContentType != "image/jpeg" && file.ContentType != "image/gif")
                {
                    lblAddDrawingMessage.Text = "Please select image file only (.png,.jpg,.jpeg,.gif)...!";
                    MPE_AddDrawing.Show();
                    return;
                }

                int size = file.ContentLength;
                string name = file.FileName;
                int position = name.LastIndexOf("\\");
                name = name.Substring(position + 1);

                byte[] fileData = new byte[size];
                file.InputStream.Read(fileData, 0, size);

                PProductDrawing PD = new PProductDrawing();
                PD.Product = new PProduct() { ProductID = Convert.ToInt32(ModelByID.ProductID) };
                PD.ProductDrawingType = new PProductDrawingType() { ProductDrawingTypeID = Convert.ToInt32(ddlDrawingType.SelectedValue) };
                PD.DrawingDescription = txtDrawingDescription.Text.Trim();
                PD.FileName = name;
                PD.FileType = file.ContentType;
                PD.AttachedFile = fileData;
                PD.IsActive = true;
                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Master/InsertOrUpdateProductDrawing", PD));
                if (result.Status == PApplication.Failure)
                {
                    lblAddDrawingMessage.Text = result.Message;
                    MPE_AddDrawing.Show();
                    return;
                }

                lblAddDrawingMessage.Text = result.Message;
                lblAddDrawingMessage.ForeColor = Color.Green;
                lblMessage.Text = result.Message;
                lblMessage.ForeColor = Color.Green;
                GetProductDrawing();
            }
            catch (Exception ex)
            {
                lblAddDrawingMessage.Text = ex.Message.ToString();
                MPE_AddDrawing.Show();
            }
        }
        private void GetProductDrawing()
        {
            int? ProductID = ModelByID.ProductID;
            List<PProductDrawing> ProductDrawing = new BDMS_Master().GetProductDrawing(ProductID);
            gvProductDrawing.DataSource = null;
            gvProductDrawing.DataBind();
            if (ProductDrawing.Count > 0)
            {
                gvProductDrawing.DataSource = ProductDrawing;
                gvProductDrawing.DataBind();
            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            LinkButton lnkDownload = (LinkButton)sender;
            GridViewRow GVRow = (GridViewRow)lnkDownload.NamingContainer;
            string FileName = lnkDownload.Text;
            long ProductDrawingID = Convert.ToInt64(lnkDownload.CommandArgument);
            Label lblFileType = (Label)GVRow.FindControl("lblFileType");
            string FileType = lblFileType.Text;

            Response.AddHeader("Content-type", FileType);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            PAttachedFile Files = new BDMS_Master().GetAttachedFileProductDrawingForDownload(ProductDrawingID + Path.GetExtension(FileName));
            Response.BinaryWrite(Files.AttachedFile);
            // Append cookie
            HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            cookie.Value = "Flag";
            cookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.AppendCookie(cookie);
            // end 
            Response.Flush();
            Response.End();
        }

        protected void lblProductDrawingDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {
                LinkButton lblProductDrawingDelete = (LinkButton)sender;
                long ProductDrawingID = Convert.ToInt64(lblProductDrawingDelete.CommandArgument);
                PProductDrawing PD = new PProductDrawing();
                PD.ProductDrawingID = Convert.ToInt64(ProductDrawingID);
                PD.IsActive = false;

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Master/InsertOrUpdateProductDrawing", PD));
                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    return;
                }

                lblMessage.Text = result.Message;
                lblMessage.ForeColor = Color.Green;
                GetProductDrawing();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                lblEditProductMessage.Text = string.Empty;
                lblEditProductMessage.ForeColor = Color.Red;
                lblEditProductMessage.Visible = true;

                int success = 0;

                if (ddlProductMake.SelectedValue == "0")
                {
                    lblEditProductMessage.Text = "Please select Product Make";
                    MPE_EditProduct.Show();
                    return;
                }
                if (ddlProductType.SelectedValue == "0")
                {
                    lblEditProductMessage.Text = "Please select Product Type";
                    MPE_EditProduct.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtProduct.Text))
                {
                    lblEditProductMessage.Text = "Please enter Model";
                    MPE_EditProduct.Show();
                    return;
                }
                int MakeID = Convert.ToInt32(ddlProductMake.SelectedValue);
                int ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue);
                success = new BPresalesMasters().InsertOrUpdateProduct(ModelByID.ProductID, MakeID, ProductTypeID, txtProduct.Text, true, PSession.User.UserID);
                if (success == 1)
                {
                    fillModelByID(ModelByID.ProductID);
                    GetProductDrawing();
                    lblMessage.Text = "Product created successfully...!";
                    lblMessage.ForeColor = Color.Green;
                    return;
                }
                else if (success == 2)
                {
                    lblEditProductMessage.Text = "Product already found";
                    MPE_EditProduct.Show();
                    return;
                }
                else
                {
                    lblEditProductMessage.Text = "Product not created successfully...!";
                    MPE_EditProduct.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblEditProductMessage.Text = ex.Message.ToString();
                MPE_EditProduct.Show();
            }
        }
        private void GetEditModel()
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {

                new DDLBind(ddlProductMake, new BDMS_Master().GetMake(null, null), "Make", "MakeID", true, "Select");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID", true, "Select");
                txtProduct.Text = "";
                ddlProductMake.SelectedValue = ModelByID.Make.MakeID.ToString();
                ddlProductType.SelectedValue = ModelByID.ProductType.ProductTypeID.ToString();
                txtProduct.Text = ModelByID.Product;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        protected void btnSaveProductSpecification_Click(object sender, EventArgs e)
        {
            lblProductSpecificationMessage.Text = "";
            lblProductSpecificationMessage.ForeColor = Color.Red;
            lblProductSpecificationMessage.Visible = true;
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {

                if (string.IsNullOrEmpty(txtSpecText.Text))
                {
                    lblProductSpecificationMessage.Text = "Please Enter Specification Text...!";
                    MPE_ProductSpecification.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtSpecDesc.Text))
                {
                    lblProductSpecificationMessage.Text = "Please Enter Specification Description...!";
                    MPE_ProductSpecification.Show();
                    return;
                }
                if (string.IsNullOrEmpty(txtOrderByNo.Text))
                {
                    lblProductSpecificationMessage.Text = "Please Enter OrderBy No...!";
                    MPE_ProductSpecification.Show();
                    return;
                }

                PProductSpecification PD = new PProductSpecification();
                if(!string.IsNullOrEmpty(Hid_ProductSpecificationID.Value))
                {
                    PD.ProductSpecificationID = Convert.ToInt32(Hid_ProductSpecificationID.Value);
                }                
                PD.Product = new PProduct() { ProductID = Convert.ToInt32(ModelByID.ProductID) };
                PD.SpecificationText = txtSpecText.Text.Trim();
                PD.SpecificationDescription = txtSpecDesc.Text.Trim();
                PD.OrderByNo = Convert.ToInt32(txtOrderByNo.Text);
                PD.IsActive = true;
                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Master/InsertOrUpdateProductSpecification", PD));
                if (result.Status == PApplication.Failure)
                {
                    lblProductSpecificationMessage.Text = result.Message;
                    MPE_ProductSpecification.Show();
                    return;
                }

                lblProductSpecificationMessage.Text = result.Message;
                lblProductSpecificationMessage.ForeColor = Color.Green;
                lblMessage.Text = result.Message;
                lblMessage.ForeColor = Color.Green;
                Hid_ProductSpecificationID.Value = "";
                GetProductSpecification();
            }
            catch (Exception ex)
            {
                lblProductSpecificationMessage.Text = ex.Message.ToString();
                MPE_ProductSpecification.Show();
            }
        }
        private void GetProductSpecification()
        {
            int? ProductID = ModelByID.ProductID;
            List<PProductSpecification> ProductSpecification = new BDMS_Master().GetProductSpecification(ProductID);
            GVProductSpecification.DataSource = null;
            GVProductSpecification.DataBind();
            if (ProductSpecification.Count > 0)
            {
                GVProductSpecification.DataSource = ProductSpecification;
                GVProductSpecification.DataBind();
            }
        }
        protected void lblProductSpecificationDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {
                LinkButton lblProductSpecificationDelete = (LinkButton)sender;
                int ProductSpecificationID = Convert.ToInt32(lblProductSpecificationDelete.CommandArgument);
                GridViewRow row = (GridViewRow)(lblProductSpecificationDelete.NamingContainer);
                string SpecificationText = Convert.ToString(((Label)row.FindControl("lblSpecificationText")).Text.Trim());
                string SpecificationDescription = Convert.ToString(((Label)row.FindControl("lblSpecificationDescription")).Text.Trim());
                int OrderByNo = Convert.ToInt32(((Label)row.FindControl("lblOrderByNo")).Text.Trim());

                PProductSpecification PD = new PProductSpecification();
                PD.ProductSpecificationID = Convert.ToInt32(ProductSpecificationID);
                PD.Product = new PProduct() { ProductID = Convert.ToInt32(ModelByID.ProductID) };
                PD.SpecificationText = SpecificationText;
                PD.SpecificationDescription = SpecificationDescription;
                PD.OrderByNo = OrderByNo;
                PD.IsActive = false;

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Master/InsertOrUpdateProductSpecification", PD));
                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    return;
                }

                lblMessage.Text = result.Message;
                lblMessage.ForeColor = Color.Green;
                GetProductSpecification();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void lblProductSpecificationEdit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {
                LinkButton lblProductSpecificationDelete = (LinkButton)sender;
                Hid_ProductSpecificationID.Value = lblProductSpecificationDelete.CommandArgument;
                GridViewRow row = (GridViewRow)(lblProductSpecificationDelete.NamingContainer);
                txtSpecText.Text = Convert.ToString(((Label)row.FindControl("lblSpecificationText")).Text.Trim());
                txtSpecDesc.Text = Convert.ToString(((Label)row.FindControl("lblSpecificationDescription")).Text.Trim());
                txtOrderByNo.Text = Convert.ToString(((Label)row.FindControl("lblOrderByNo")).Text.Trim());
                btnSaveProductSpecification.Text = "Update";
                MPE_ProductSpecification.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
    }
}