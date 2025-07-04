using Business;
using ClosedXML.Excel;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewECatalogue.UserControls
{
    public partial class SpcAssemblyView : System.Web.UI.UserControl
    {
        public PSpcAssembly Assembly
        {
            get
            {
                if (ViewState["SPAssemblyImageView_PSpAssemblyImage"] == null)
                {
                    ViewState["SPAssemblyImageView_PSpAssemblyImage"] = new PSpcAssembly();
                }
                return (PSpcAssembly)ViewState["SPAssemblyImageView_PSpAssemblyImage"];
            }
            set
            {
                ViewState["SPAssemblyImageView_PSpAssemblyImage"] = value;
            }
        }
        public List<PSpcAssemblyPartsCoOrdinate> PartsCoOrdinate
        {
            get
            {
                if (ViewState["SPAssemblyImageView_PartsCoOrdinate"] == null)
                {
                    ViewState["SPAssemblyImageView_PartsCoOrdinate"] = new List<PSpcAssemblyPartsCoOrdinate>();
                }
                return (List<PSpcAssemblyPartsCoOrdinate>)ViewState["SPAssemblyImageView_PartsCoOrdinate"];
            }
            set
            {
                ViewState["SPAssemblyImageView_PartsCoOrdinate"] = value;
            }
        }

        public int xyUpdate
        {
            get
            {
                if (ViewState["SPAssemblyImageView_xyUpdate"] == null)
                {
                    ViewState["SPAssemblyImageView_xyUpdate"] = 0;
                }
                return (int)ViewState["SPAssemblyImageView_xyUpdate"];
            }
            set
            {
                ViewState["SPAssemblyImageView_xyUpdate"] = value;
            }
        }
        public int xyBulkUpdate
        {
            get
            {
                if (ViewState["SPAssemblyImageView_xyBulkUpdate"] == null)
                {
                    ViewState["SPAssemblyImageView_xyBulkUpdate"] = 0;
                }
                return (int)ViewState["SPAssemblyImageView_xyBulkUpdate"];
            }
            set
            {
                ViewState["SPAssemblyImageView_xyBulkUpdate"] = value;
            }
        }

        public List<PSpcAssemblyPartsCoOrdinate> PartsListUpload
        {
            get
            {
                if (ViewState["SPAssemblyImageView_PartsListUpload"] == null)
                {
                    ViewState["SPAssemblyImageView_PartsListUpload"] = new List<PSpcAssemblyPartsCoOrdinate>();
                }
                return (List<PSpcAssemblyPartsCoOrdinate>)ViewState["SPAssemblyImageView_PartsListUpload"];
            }
            set
            {
                ViewState["SPAssemblyImageView_PartsListUpload"] = value;
            }
        }
        public List<PDealerBinLocation_Insert> InsertBinLocationConfigUpload
        {
            get
            {
                if (ViewState["BinLocationConfigUpload"] == null)
                {
                    ViewState["BinLocationConfigUpload"] = new List<PDealerBinLocation_Insert>();
                }
                return (List<PDealerBinLocation_Insert>)ViewState["BinLocationConfigUpload"];
            }
            set
            {
                ViewState["BinLocationConfigUpload"] = value;
            }
        }

        public List<PSpcAssemblyPartsCart_insert> PartsCart
        {
            get
            {
                if (ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"] == null)
                {
                    ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"] = new List<PSpcAssemblyPartsCart_insert>();
                }
                return (List<PSpcAssemblyPartsCart_insert>)ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"];
            }
            set
            {
                ViewState["PSpcAssemblyPartsCart_insert_SpcAssemblyView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblAssemblyEditMessage.Text = "";
            lblPatrsListUploadMessage.Text = "";
            lblAssemblyDrawingMessage.Text = "";

            if (Assembly != null)
            {
                if (Assembly.Model != null)
                {
                    Session["filePath"] = Assembly.FileName;
                }
            }

        }
        public void Clear()
        {
            xyUpdate = 0;
            xyBulkUpdate = 0;
            hdnUpdatedIDs.Value = "";
            hdnX.Value = "";
            hdnY.Value = "";
            ActionControlMange();
        }
        public void fillParts(int SpcAssemblyID)
        {
            PApiResult Result = new BECatalogue().GetSpcAssembly(null, null, SpcAssemblyID,"");
            List<PSpcAssembly> Assemblys = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
            Assembly = Assemblys[0];
            lblDivision.Text = Assembly.Model.Division.DivisionCode;
            lblModel.Text = Assembly.Model.Model;
            lblModelCode.Text = Assembly.Model.ModelCode;
            lblAssembly.Text = Assembly.AssemblyCode;
            lblAssemblyDes.Text = Assembly.AssemblyDescription;
            lblAssemblyType.Text = Assembly.AssemblyType;
            lblRemarks.Text = Assembly.Remarks;
            Session["filePath"] = Assembly.FileName;

            new BECatalogue().DowloadSpcFile(Assembly.FileName);


            PApiResult ResultParts = new BECatalogue().GetAssemblyPartsCoOrdinate(null, SpcAssemblyID);
            PartsCoOrdinate = JsonConvert.DeserializeObject<List<PSpcAssemblyPartsCoOrdinate>>(JsonConvert.SerializeObject(ResultParts.Data));
            gvParts.DataSource = PartsCoOrdinate;
            gvParts.DataBind();

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CreateAssemblyAndCreatePartsCoordinates).Count() == 0)
            {
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    LinkButton lnkBtnDelete = (LinkButton)gvParts.Rows[i].FindControl("lnkBtnDelete");
                    lnkBtnDelete.Visible = false; 
                }
            }

            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbEditXYCoOrdinate")
            {
                xyUpdate = 1;
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    CheckBox cbParts = (CheckBox)gvParts.Rows[i].FindControl("cbParts");
                    RadioButton rbParts = (RadioButton)gvParts.Rows[i].FindControl("rbParts");
                    cbParts.Visible = false;
                    rbParts.Visible = true;

                    cbParts.Checked = false;
                    rbParts.Checked = false;
                }
            }
            else if (lbActions.ID == "lbUpdateMultyXYCoOrdinate")
            {
                xyBulkUpdate = 1;
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    CheckBox cbParts = (CheckBox)gvParts.Rows[i].FindControl("cbParts");
                    RadioButton rbParts = (RadioButton)gvParts.Rows[i].FindControl("rbParts");
                    cbParts.Visible = false;
                    rbParts.Visible = true;
                    rbParts.Enabled = false;

                    cbParts.Checked = false;
                    rbParts.Checked = false;
                }
            }
            else if (lbActions.ID == "lbCancelXYCoOrdinate")
            {
                xyUpdate = 0;
                hdnUpdatedIDs.Value = "";
                hdnX.Value = "";
                hdnY.Value = "";
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    CheckBox cbParts = (CheckBox)gvParts.Rows[i].FindControl("cbParts");
                    RadioButton rbParts = (RadioButton)gvParts.Rows[i].FindControl("rbParts");
                    cbParts.Visible = true;
                    rbParts.Visible = false;

                    cbParts.Checked = false;
                    rbParts.Checked = false;
                }
            }
            else if (lbActions.ID == "lbSaveXYCoOrdinate")
            {
                if (string.IsNullOrEmpty(hdnUpdatedIDs.Value.Trim(',')))
                {
                    lblMessage.Text = "Please select X Y CoOrdinate ";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                MPE_SaveCoOrdinate.Show();

                string[] hdnID = hdnUpdatedIDs.Value.Trim(',').Split(',');
                List<long> IDList = hdnID.Select(id => long.Parse(id)).ToList();

                string[] hdnX_ = hdnX.Value.Trim(',').Split(',');
                List<int> XList = hdnX_.Select(id => int.Parse(id)).ToList();

                string[] hdnY_ = hdnY.Value.Trim(',').Split(',');
                List<int> YList = hdnY_.Select(id => int.Parse(id)).ToList();

                List<PSpcAssemblyPartsCoOrdinate> CoOrdinate = PartsCoOrdinate.Where(p => IDList.Contains(p.SpcAssemblyPartsCoOrdinateID)).ToList();
                foreach (PSpcAssemblyPartsCoOrdinate co in CoOrdinate)
                {
                    for (int i = 0; i < hdnX_.Count(); i++)
                    {
                        if (IDList[i] == co.SpcAssemblyPartsCoOrdinateID)
                        {
                            co.X_CoOrdinate = XList[i];
                            co.Y_CoOrdinate = YList[i];
                        }
                    }
                }

                gvPartsCoordinats.DataSource = CoOrdinate;
                gvPartsCoordinats.DataBind();
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    RadioButton rbParts = (RadioButton)gvParts.Rows[i].FindControl("rbParts");
                    rbParts.Checked = false;
                }

            }
            else if (lbActions.ID == "lbSaveToCart")
            {
                MPE_SaveToCart.Show();
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    CheckBox cbParts = (CheckBox)gvParts.Rows[i].FindControl("cbParts");
                    if (cbParts.Checked)
                    {
                        Label lblNumber = (Label)gvParts.Rows[i].FindControl("lblNumber");
                        Label lblFlag = (Label)gvParts.Rows[i].FindControl("lblFlag");
                        Label lblMaterial = (Label)gvParts.Rows[i].FindControl("lblMaterial");
                        Label lblMaterialDescription = (Label)gvParts.Rows[i].FindControl("lblMaterialDescription");
                        Label lblQty = (Label)gvParts.Rows[i].FindControl("lblQty");

                        PartsCart.Add(new PSpcAssemblyPartsCart_insert()
                        {
                            Flag = lblFlag.Text,
                            Material = lblMaterial.Text,
                            MaterialDescription = lblMaterialDescription.Text,
                            Number = Convert.ToInt32(lblNumber.Text),
                            Qty = Convert.ToInt32(lblQty.Text)
                        });
                    }
                }
                gvToCart.DataSource = PartsCart;
                gvToCart.DataBind();
            }
            else if (lbActions.ID == "lbUploadParts")
            {
                MPE_PatrsListUpload.Show();
            }
            else if (lbActions.ID == "lbDownloadTemplate")
            {
                string Path = Server.MapPath("~/Templates/AssemblyPatrsList.xlsx");
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=\"AssemblyPatrsList.xlsx\"");
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
            else if (lbActions.ID == "lbEditAssembly")
            {
                MPE_AssemblyEdit.Show();
                new DDLBind(ddlModelAssemblyEdit, new BDMS_Model().GetModel(null, null, null), "Model", "ModelID", true, "Select Model");
                ddlModelAssemblyEdit.SelectedValue = Convert.ToString(Assembly.Model.ModelID);
                txtAssemblyCode.Text = Assembly.AssemblyCode;
                txtAssemblyDescription.Text = Assembly.AssemblyDescription;
                txtRemarks.Text = Assembly.Remarks;
                ddlAssemblyType.SelectedValue = Assembly.AssemblyType;
            }
            else if (lbActions.ID == "lbChangeAssemblyDrawing")
            {
                MPE_AssemblyDrawing.Show();
            }
            ActionControlMange();
        }
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message; 
            lblMessage.ForeColor = Color.Green;
        }
        void ActionControlMange()
        {
            lbUpdateMultyXYCoOrdinate.Visible = true;
            lbEditXYCoOrdinate.Visible = true;
            lbCancelXYCoOrdinate.Visible = true;
            lbSaveXYCoOrdinate.Visible = true;
            lbSaveToCart.Visible = true;
            lbUploadParts.Visible = true;
            lbDownloadTemplate.Visible = true;
            lbEditAssembly.Visible = true;
            lbChangeAssemblyDrawing.Visible = true;

            if (xyUpdate == 1)
            {
                lbEditXYCoOrdinate.Visible = false;
                lbUpdateMultyXYCoOrdinate.Visible = false;
                lbSaveToCart.Visible = false;
                lbUploadParts.Visible = false;
                lbDownloadTemplate.Visible = false;
                lbEditAssembly.Visible = false;
                lbChangeAssemblyDrawing.Visible = false;
            }
            else if (xyBulkUpdate == 1)
            {
                lbEditXYCoOrdinate.Visible = false;
                lbUpdateMultyXYCoOrdinate.Visible = false;
                lbSaveToCart.Visible = false;
                lbUploadParts.Visible = false;
                lbDownloadTemplate.Visible = false;
                lbEditAssembly.Visible = false;
                lbChangeAssemblyDrawing.Visible = false;
            }
            else
            {
                lbCancelXYCoOrdinate.Visible = false;
                lbSaveXYCoOrdinate.Visible = false;
            }

            
             

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CreateAssemblyAndCreatePartsCoordinates).Count() == 0)
            {
                lbUpdateMultyXYCoOrdinate.Visible = false;
                lbEditXYCoOrdinate.Visible = false;
                lbCancelXYCoOrdinate.Visible = false;
                lbSaveXYCoOrdinate.Visible = false; 
                lbUploadParts.Visible = false;
                lbDownloadTemplate.Visible = false;
                lbEditAssembly.Visible = false;
                lbChangeAssemblyDrawing.Visible = false;
            }
                
        }
        protected void lnkBtnItemAction_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;

            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            if (lbActions.ID == "lnkBtnDelete")
            { 
                Label lblSpcAssemblyPartsCoOrdinateID = (Label)gvRow.FindControl("lblSpcAssemblyPartsCoOrdinateID"); 
                PApiResult Results = new BECatalogue().UpdateSpcAssemblyPartsDelete(Convert.ToInt64(lblSpcAssemblyPartsCoOrdinateID.Text));
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.Text = Results.Message;
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                ShowMessage(Results);
                fillParts(Assembly.SpcAssemblyID);
            }
        }

        protected void btnSaveCoOrdinate_Click(object sender, EventArgs e)
        {
            MPE_SaveCoOrdinate.Show();
            List<PSpcAssemblyPartsCoOrdinate_Insert> CoOrdinate = new List<PSpcAssemblyPartsCoOrdinate_Insert>();

            string[] hdnID = hdnUpdatedIDs.Value.Trim(',').Split(',');
            List<long> IDList = hdnID.Select(id => long.Parse(id)).ToList();
            List<PSpcAssemblyPartsCoOrdinate> CoOrdinat = PartsCoOrdinate.Where(p => IDList.Contains(p.SpcAssemblyPartsCoOrdinateID)).ToList();

            foreach (PSpcAssemblyPartsCoOrdinate co in CoOrdinat)
            {
                CoOrdinate.Add(new PSpcAssemblyPartsCoOrdinate_Insert()
                {
                    SpcAssemblyPartsCoOrdinateID = co.SpcAssemblyPartsCoOrdinateID,
                    X_CoOrdinate = co.X_CoOrdinate,
                    Y_CoOrdinate = co.Y_CoOrdinate
                });
            }
            PApiResult Results = new BECatalogue().UpdateSpcAssemblyPartsCoOrdinate(CoOrdinate);
            if (Results.Status == PApplication.Failure)
            {
                lblCoOrdinateMessage.Text = Results.Message;
                return;
            }
            lblMessage.Text = "Updated Successfully"; 
            lblMessage.ForeColor = Color.Green;
            MPE_SaveCoOrdinate.Hide();
            fillParts(Assembly.SpcAssemblyID); 
            xyUpdate = 0;
            hdnUpdatedIDs.Value = "";
            hdnX.Value = "";
            hdnY.Value = "";
            ActionControlMange();
        }

        protected void btnViewPatrsList_Click(object sender, EventArgs e)
        {
            MPE_PatrsListUpload.Show();
            GgUploadPartsList.DataSource = null;
            GgUploadPartsList.DataBind();

            if (fileUploadBinLocationConfig.HasFile != true)
            {
                lblPatrsListUploadMessage.Text = "Please check the file.";
            }
            string validExcel = ".xlsx";
            string FileExtension = System.IO.Path.GetExtension(fileUploadBinLocationConfig.PostedFile.FileName);
            if (validExcel != FileExtension)
            {
                lblPatrsListUploadMessage.Text = "Please check the file format.";
            }


            FillUploadBinLocationConfig();
            if (PartsListUpload.Count > 0)
            {
                GgUploadPartsList.DataSource = PartsListUpload;
                GgUploadPartsList.DataBind();
                btnViewPatrsList.Visible = false;
                BtnSavePatrsList.Visible = true;
            }
        }
        protected void BtnSavePatrsList_Click(object sender, EventArgs e)
        {
            lblPatrsListUploadMessage.ForeColor = Color.Red;
            MPE_PatrsListUpload.Show();
            try
            {
                List<PSpcAssemblyPartsCoOrdinate_Insert> PartsList_ = new List<PSpcAssemblyPartsCoOrdinate_Insert>();

                foreach (PSpcAssemblyPartsCoOrdinate co in PartsListUpload)
                {
                    PartsList_.Add(new PSpcAssemblyPartsCoOrdinate_Insert()
                    {
                        SpcAssemblyID = Assembly.SpcAssemblyID,
                        Number = co.Number,
                        Flag = co.Flag,
                        Material = co.Material.Material,
                        MaterialDescription = co.Material.MaterialDescription,
                        Qty = co.Qty,
                        Remarks = co.Remarks
                    });
                }

                PApiResult Results = new BECatalogue().InsertSpcAssemblyPartsFromExcel(PartsList_);
                if (Results.Status == PApplication.Failure)
                {
                    lblPatrsListUploadMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillParts(Assembly.SpcAssemblyID);
                BtnSavePatrsList.Visible = false;
            }
            catch (Exception ex)
            {
                lblPatrsListUploadMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_PatrsListUpload.Hide();
        }

        private Boolean FillUploadBinLocationConfig()
        {
            BtnSavePatrsList.Visible = false;
            using (XLWorkbook workBook = new XLWorkbook(fileUploadBinLocationConfig.PostedFile.InputStream))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                DataTable DTDealerOperatorDetailsUpload = new DataTable();
                int sno = 0;
                foreach (IXLRow row in workSheet.Rows())
                {
                    sno += 1;
                    if (sno > 1)
                    {
                        List<IXLCell> Cells = row.Cells().ToList();
                        if (Cells.Count != 0)
                        {
                            PartsListUpload.Add(new PSpcAssemblyPartsCoOrdinate()
                            {
                                Number = Convert.ToInt32(Convert.ToString(Cells[1].Value).TrimEnd('\0')),
                                Flag = Convert.ToString(Cells[2].Value).TrimEnd('\0'),
                                Material = new PSpcMaterial()
                                {
                                    Material = Convert.ToString(Cells[3].Value).TrimEnd('\0'),
                                    MaterialDescription = Convert.ToString(Cells[4].Value).TrimEnd('\0')
                                },
                                Qty = Convert.ToInt32(Convert.ToString(Cells[5].Value).TrimEnd('\0')),
                                Remarks = Convert.ToString(Cells[6].Value).TrimEnd('\0'),
                            });
                        }
                    }
                }
            }
            return true;
        }

        protected void btnAssemblyEditSave_Click(object sender, EventArgs e)
        {
            lblAssemblyEditMessage.ForeColor = Color.Red;
            MPE_AssemblyEdit.Show();
            try
            {
                int ModelID = Convert.ToInt32(ddlModelAssemblyEdit.SelectedValue);
                string AssemblyCode = txtAssemblyCode.Text.Trim();
                string AssemblyDescription = txtAssemblyDescription.Text.Trim();
                string AssemblyType = ddlAssemblyType.SelectedValue;
                string Remarks = txtRemarks.Text.Trim();

                PApiResult Results = new BECatalogue().InsertorUpdateSpcAssembly(Assembly.SpcAssemblyID, ModelID, AssemblyCode, AssemblyDescription, AssemblyType, Remarks);
                if (Results.Status == PApplication.Failure)
                {
                    lblAssemblyEditMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillParts(Assembly.SpcAssemblyID);
            }
            catch (Exception ex)
            {
                lblAssemblyEditMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_AssemblyEdit.Hide();
        }

        protected void btnAssemblyDrawingSave_Click(object sender, EventArgs e)
        {
            lblAssemblyDrawingMessage.ForeColor = Color.Red;
            MPE_AssemblyDrawing.Show();
            try
            {
                PAttachedFile_Azure File = CreateUploadedFileFSR(fuAssemblyDrawing.PostedFile);
                PApiResult Results = new BECatalogue().UploadSpcFile(File);
                if (Results.Status == PApplication.Failure)
                {
                    lblAssemblyEditMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillParts(Assembly.SpcAssemblyID);
            }
            catch (Exception ex)
            {
                lblAssemblyDrawingMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_AssemblyDrawing.Hide();
        }
        private PAttachedFile_Azure CreateUploadedFileFSR(HttpPostedFile file)
        {
            PAttachedFile_Azure AttachedFile = new PAttachedFile_Azure();
            int size = file.ContentLength;
            AttachedFile.FileName = Assembly.SpcAssemblyID + Path.GetExtension(file.FileName);
            AttachedFile.FileType = file.ContentType;
            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            AttachedFile.AttachedFile = fileData;
            AttachedFile.AttachedFileID = Assembly.SpcAssemblyID;
            return AttachedFile;
        }

        protected void btnSaveToCart_Click(object sender, EventArgs e)
        {

        }
    }
}