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
    public partial class MaterialView : System.Web.UI.UserControl
    {
        public PDMS_Material MaterialByID
        {
            get
            {
                if (ViewState["MaterialByID"] == null)
                {
                    ViewState["MaterialByID"] = new PDMS_Material();
                }
                return (PDMS_Material)ViewState["MaterialByID"];
            }
            set
            {
                ViewState["MaterialByID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void fillMaterialByID(int? MaterialID)
        {
            ViewState["MaterialID"] = MaterialID;
            MaterialByID = new BDMS_Material().GetMaterialListSQL(MaterialID, "", null, null, null)[0];
            lblMaterialCode.Text = MaterialByID.MaterialCode;
            lblMaterialDesc.Text = MaterialByID.MaterialDescription;
            lblUOM.Text = MaterialByID.BaseUnit;
            lblMaterialType.Text = MaterialByID.MaterialType;
            lblDivisionCode.Text = MaterialByID.Model.Division.DivisionCode;
            lblModeCode.Text = MaterialByID.Model.ModelCode;
            lblModel.Text = MaterialByID.Model.Model;
            lblModelDesc.Text = MaterialByID.Model.ModelDescription;
            lblGrossWt.Text = MaterialByID.GrossWeight.ToString();
            lblNetWt.Text = MaterialByID.NetWeight.ToString();
            lblWtUnit.Text = MaterialByID.WeightUnit;
            lblDiv.Text = MaterialByID.MaterialDivision;
            lblHSN.Text = MaterialByID.HSN;
            lblCSTPer.Text = MaterialByID.CST.ToString();
            lblSSTPer.Text = MaterialByID.SST.ToString();
            lblGSTPer.Text = MaterialByID.GST.ToString();
            GetMaterialDrawing();
            //ActionControlMange();
        }

        protected void lnkBtnActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Add Drawing")
            {
                MPE_AddDrawing.Show();
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

                PMaterialDrawing MD = new PMaterialDrawing();
                MD.Material = new PDMS_Material() { MaterialID = Convert.ToInt64(MaterialByID.MaterialID) };
                MD.FileName = name;
                MD.FileType = file.ContentType;
                MD.AttachedFile = fileData;
                MD.IsActive = true;
                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Material/InsertOrUpdateMaterialDrawing", MD));
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
                GetMaterialDrawing();
            }
            catch (Exception ex)
            {
                lblAddDrawingMessage.Text = ex.Message.ToString();
                MPE_AddDrawing.Show();
            }
        }
        private void GetMaterialDrawing()
        {
            long? MaterialID = MaterialByID.MaterialID;
            List<PMaterialDrawing> MaterialDrawing = new BDMS_Material().GetMaterialDrawing(MaterialID);
            gvMaterialDrawing.DataSource = null;
            gvMaterialDrawing.DataBind();
            if (MaterialDrawing.Count > 0)
            {
                gvMaterialDrawing.DataSource = MaterialDrawing;
                gvMaterialDrawing.DataBind();
            }
        }
        protected void lblMaterialDrawingDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
            try
            {
                LinkButton lblMaterialDrawingDelete = (LinkButton)sender;
                long MaterialDrawingID = Convert.ToInt64(lblMaterialDrawingDelete.CommandArgument);
                PMaterialDrawing MD = new PMaterialDrawing();
                MD.MaterialDrawingID = Convert.ToInt64(MaterialDrawingID);
                MD.IsActive = false;

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Material/InsertOrUpdateMaterialDrawing", MD));
                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = result.Message;
                    return;
                }

                lblMessage.Text = result.Message;
                lblMessage.ForeColor = Color.Green;
                GetMaterialDrawing();
            }
            catch (Exception ex)
            {
                lblAddDrawingMessage.Text = ex.Message.ToString();
                MPE_AddDrawing.Show();
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
            long MaterialDrawingID = Convert.ToInt64(lnkDownload.CommandArgument);
            Label lblFileType = (Label)GVRow.FindControl("lblFileType");
            string FileType = lblFileType.Text;

            Response.AddHeader("Content-type", FileType);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            HttpContext.Current.Response.Charset = "utf-16";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            PAttachedFile Files = new BDMS_Material().GetAttachedFileMaterialDrawingForDownload(MaterialDrawingID + Path.GetExtension(FileName));
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
    }
}