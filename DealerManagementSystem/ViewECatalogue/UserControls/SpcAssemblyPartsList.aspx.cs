using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
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

namespace DealerManagementSystem.ViewECatalogue.UserControls
{
    public partial class SpcAssemblyPartsList : System.Web.UI.Page
    {
        public int SpcAssemblyID
        {
            get
            {
                if (ViewState["SpcAssemblyPartsList_SpcAssemblyID"] == null)
                {
                    ViewState["SpcAssemblyPartsList_SpcAssemblyID"] = new PSpcAssembly();
                }
                return (int)ViewState["SpcAssemblyPartsList_SpcAssemblyID"];
            }
            set
            {
                ViewState["SpcAssemblyPartsList_SpcAssemblyID"] = value;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblPatrsListUploadMessage.Text = ""; 
            //lblPartAddMessage.Text = "";
            
            if (!IsPostBack)
            {
                SpcAssemblyID = Convert.ToInt32(Request.QueryString["SpcAssemblyID"]); 
                fillParts();
                ActionControlMange();
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
        public void fillParts()
        { 
            PApiResult ResultParts = new BECatalogue().GetAssemblyPartsCoOrdinate(null, SpcAssemblyID);
            PartsCoOrdinate = JsonConvert.DeserializeObject<List<PSpcAssemblyPartsCoOrdinate>>(JsonConvert.SerializeObject(ResultParts.Data));
            gvParts.DataSource = PartsCoOrdinate;
            gvParts.DataBind();

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CreateAssemblyAndCreatePartsCoordinates).Count() == 0)
            {
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    ((LinkButton)gvParts.Rows[i].FindControl("lnkBtnDelete")).Visible = false;
                    ((LinkButton)gvParts.Rows[i].FindControl("lblEditParts")).Visible = false;
                }
            }
            else
            {
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    ((LinkButton)gvParts.Rows[i].FindControl("lnkBtnDelete")).Visible = true;
                    ((LinkButton)gvParts.Rows[i].FindControl("lblEditParts")).Visible = true;
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
                    rbParts.Enabled = true;

                    cbParts.Checked = false;
                    rbParts.Checked = false;
                }
            }
            else if (lbActions.ID == "lbEditAllXYCoOrdinate")
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
                xyBulkUpdate = 0;
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
            
            else if (lbActions.ID == "lbUploadParts")
            {
                MPE_PatrsListUpload.Show();
                PartsListUpload = new List<PSpcAssemblyPartsCoOrdinate>();
                GgUploadPartsList.DataSource = PartsListUpload;
                GgUploadPartsList.DataBind();

                btnViewPatrsList.Visible = true;
                BtnSavePatrsList.Visible = false;

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
                response.AddHeader("Content-Disposition", "attachment;filename=\"AssemblyPartsList.xlsx\"");
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
            
            else if (lbActions.ID == "lbAddParts")
            {
                lblSpcAssemblyPartsCoOrdinateID.Text = "0";
                txtNumberC.Text = "";
                txtFlagC.Text = "";
                txtPartC.Text = "";
                txtPartDescription.Text = "";
                txtQtyC.Text = "";
                txtRemarksC.Text = "";
                MPE_AddPart.Show();
            }
            else if (lbActions.ID == "lbDeleteSelectedParts")
            {
                PApiResult Results = new PApiResult();
                for (int i = 0; i < gvParts.Rows.Count; i++)
                {
                    CheckBox cbParts = (CheckBox)gvParts.Rows[i].FindControl("cbParts");
                    if (cbParts.Checked)
                    {
                        Label lblSpcAssemblyPartsCoOrdinateID = (Label)gvParts.Rows[i].FindControl("lblSpcAssemblyPartsCoOrdinateID");
                        Results = new BECatalogue().UpdateSpcAssemblyPartsDelete(Convert.ToInt64(lblSpcAssemblyPartsCoOrdinateID.Text));
                        if (Results.Status == PApplication.Failure)
                        {
                            lblMessage.Text = Results.Message;
                            lblMessage.ForeColor = Color.Red;
                            return;
                        }

                    }
                }
                ShowMessage(Results);
                fillParts();
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
            lbEditAllXYCoOrdinate.Visible = true;
            lbEditXYCoOrdinate.Visible = true;
            lbCancelXYCoOrdinate.Visible = true;
            lbSaveXYCoOrdinate.Visible = true;
            lbAddParts.Visible = true;
            lbUploadParts.Visible = true; 
            lbDownloadTemplate.Visible = true; 
            lbDeleteSelectedParts.Visible = true;

            if (xyUpdate == 1)
            {
                lbEditXYCoOrdinate.Visible = false;
                lbEditAllXYCoOrdinate.Visible = false;
                lbAddParts.Visible = false;
                lbUploadParts.Visible = false;
                lbDownloadTemplate.Visible = false; 
                lbDeleteSelectedParts.Visible = false;
            }
            else if (xyBulkUpdate == 1)
            {
                lbEditXYCoOrdinate.Visible = false;
                lbEditAllXYCoOrdinate.Visible = false;
                lbAddParts.Visible = false;
                lbUploadParts.Visible = false;
                lbDownloadTemplate.Visible = false; 

                lbDeleteSelectedParts.Visible = false;
            }
            else
            {
                lbCancelXYCoOrdinate.Visible = false;
                lbSaveXYCoOrdinate.Visible = false;
            }




            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CreateAssemblyAndCreatePartsCoordinates).Count() == 0)
            {
                lbEditAllXYCoOrdinate.Visible = false;
                lbEditXYCoOrdinate.Visible = false;
                lbCancelXYCoOrdinate.Visible = false;
                lbSaveXYCoOrdinate.Visible = false;
                lbAddParts.Visible = false;
                lbUploadParts.Visible = false;
                lbDownloadTemplate.Visible = false; 
                lbDeleteSelectedParts.Visible = false;
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
                fillParts();
            }
            else if (lbActions.ID == "lblEditParts")
            {
                Label lblSpcAssemblyPartsCoOrdinateIDC = (Label)gvRow.FindControl("lblSpcAssemblyPartsCoOrdinateID");
                Label lblNumber = (Label)gvRow.FindControl("lblNumber");
                Label lblFlag = (Label)gvRow.FindControl("lblFlag");
                Label lblMaterial = (Label)gvRow.FindControl("lblMaterial");
                Label lblMaterialDescription = (Label)gvRow.FindControl("lblMaterialDescription");
                Label lblQty = (Label)gvRow.FindControl("lblQty");
                Label lblRemarks = (Label)gvRow.FindControl("lblRemarks");

                lblSpcAssemblyPartsCoOrdinateID.Text = lblSpcAssemblyPartsCoOrdinateIDC.Text;
                txtNumberC.Text = lblNumber.Text.Trim();
                txtFlagC.Text = lblFlag.Text.Trim();
                txtPartC.Text = lblMaterial.Text.Trim();
                txtPartDescription.Text = lblMaterialDescription.Text.Trim();
                txtQtyC.Text = lblQty.Text.Trim();
                txtRemarksC.Text = lblRemarks.Text.Trim();
                MPE_AddPart.Show();
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
            fillParts();
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

            lblPatrsListUploadMessage.ForeColor = Color.Red;
            try
            {
                if (fileUploadBinLocationConfig.HasFile != true)
                {
                    lblPatrsListUploadMessage.Text = "Please check the file.";
                    return;
                }
                string validExcel = ".xlsx";
                string FileExtension = System.IO.Path.GetExtension(fileUploadBinLocationConfig.PostedFile.FileName);
                if (validExcel != FileExtension)
                {
                    lblPatrsListUploadMessage.Text = "Please check the file format.";
                    return;
                }


                FillUploadPartsFromExcel();
                if (PartsListUpload.Count > 0)
                {
                    GgUploadPartsList.DataSource = PartsListUpload;
                    GgUploadPartsList.DataBind();
                    btnViewPatrsList.Visible = false;
                    BtnSavePatrsList.Visible = true;
                }
            }
            catch (Exception E1)
            {
                PartsListUpload = new List<PSpcAssemblyPartsCoOrdinate>();
                lblPatrsListUploadMessage.Text = E1.Message;
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
                        SpcAssemblyID = SpcAssemblyID,
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
                fillParts();
                BtnSavePatrsList.Visible = false;
            }
            catch (Exception ex)
            {
                lblPatrsListUploadMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_PatrsListUpload.Hide();
        }

        private Boolean FillUploadPartsFromExcel()
        {
            BtnSavePatrsList.Visible = false;
            int number;
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
                            string _Number;
                            string _Flag;
                            string _Material;
                            string _MaterialDescription;
                            string _Qty;
                            try
                            {
                                _Number = Convert.ToString(Cells[1].Value).TrimEnd('\0');
                            }

                            catch { throw new Exception("Please chech POS. Line :" + Convert.ToString(sno - 1)); }

                            try
                            {
                                _Flag = Convert.ToString(Cells[2].Value).TrimEnd('\0');
                            }

                            catch { throw new Exception("Please chech Flag. Line :" + Convert.ToString(sno - 1)); }

                            try
                            {
                                _Material = Convert.ToString(Cells[3].Value).TrimEnd('\0');
                            }
                            catch { throw new Exception("Please chech Part. Line :" + Convert.ToString(sno - 1)); }

                            try
                            {
                                _MaterialDescription = Convert.ToString(Cells[4].Value).TrimEnd('\0');
                            }
                            catch { throw new Exception("Please chech Part Description. Line :" + Convert.ToString(sno - 1)); }

                            try
                            {
                                _Qty = Convert.ToString(Cells[5].Value).TrimEnd('\0');
                            }
                            catch { throw new Exception("Please chech Qty. Line :" + Convert.ToString(sno - 1)); }

                            if (string.IsNullOrEmpty(_Number))
                            {
                                throw new Exception("Please enter POS.");
                            }
                            else if (!int.TryParse(_Number, out number))
                            {
                                throw new Exception("Please enter valid POS.");
                            }
                            else if (string.IsNullOrEmpty(_Material))
                            {
                                throw new Exception("Please enter Part.");
                            }
                            else if (string.IsNullOrEmpty(_MaterialDescription))
                            {
                                throw new Exception("Please enter Part Description.");
                            }
                            else if (string.IsNullOrEmpty(_Qty))
                            {
                                throw new Exception("Please enter Qty.");
                            }
                            else if (!int.TryParse(_Qty, out number))
                            {
                                throw new Exception("Please enter valid Qty.");
                            }
                            string _Remarks = "";
                            try
                            {
                                _Remarks = Convert.ToString(Cells[6].Value).TrimEnd('\0');
                            }
                            catch { }
                            PartsListUpload.Add(new PSpcAssemblyPartsCoOrdinate()
                            {
                                Number = Convert.ToInt32(_Number),
                                Flag = Convert.ToString(Cells[2].Value).TrimEnd('\0'),
                                Material = new PSpcMaterial()
                                {
                                    Material = _Material,
                                    MaterialDescription = _MaterialDescription
                                },
                                Qty = Convert.ToInt32(_Qty),
                                Remarks = _Remarks,
                            });
                        }
                    }
                }
            }
            return true;
        } 
        protected void link_Show(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblX_CoOrdinate = (Label)gvRow.FindControl("lblX_CoOrdinate");
            Label lblY_CoOrdinate = (Label)gvRow.FindControl("lblY_CoOrdinate");
            Label lblMaterial = (Label)gvRow.FindControl("lblMaterial");
            Label lblMaterialDescription = (Label)gvRow.FindControl("lblMaterialDescription");

            ImageButton btn_imgShow = (ImageButton)sender;                // get the link button which trigger the event           
            GridViewRow row = (GridViewRow)btn_imgShow.NamingContainer;   // get the GridViewRow that contains the linkbutton

            string ls_x = lblX_CoOrdinate.Text;
            string ls_y = lblY_CoOrdinate.Text;
            string ls_pno = lblMaterial.Text;
            string ls_pdesc = lblMaterialDescription.Text;
            row.BackColor = System.Drawing.Color.FromArgb(int.Parse("#ffe0b3".Replace("#", ""), System.Globalization.NumberStyles.AllowHexSpecifier));

            ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "CallOut(" + ls_x + "," + ls_y + ",'" + ls_pno + "','" + ls_pdesc + "');", true);

        } 
        protected void btnPartsSave_Click(object sender, EventArgs e)
        {
            lblPartAddMessage.ForeColor = Color.Red;
            MPE_AddPart.Show();
            try
            {
                string Message = Partsvalidation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblPartAddMessage.Text = Message;
                    return;
                }
                List<PSpcAssemblyPartsCoOrdinate_Insert> PartsList_ = new List<PSpcAssemblyPartsCoOrdinate_Insert>();

                PartsList_.Add(new PSpcAssemblyPartsCoOrdinate_Insert()
                {
                    SpcAssemblyPartsCoOrdinateID = lblSpcAssemblyPartsCoOrdinateID.Text == "0" ? (long?)null : Convert.ToInt64(lblSpcAssemblyPartsCoOrdinateID.Text),
                    SpcAssemblyID = SpcAssemblyID,
                    Number = Convert.ToInt32(txtNumberC.Text.Trim()),
                    Flag = txtFlagC.Text.Trim(),
                    Material = txtPartC.Text,
                    MaterialDescription = txtPartDescription.Text.Trim(),
                    Qty = Convert.ToInt32(txtQtyC.Text.Trim()),
                    Remarks = txtRemarksC.Text
                });
                PApiResult Results = new BECatalogue().InsertSpcAssemblyPartsFromExcel(PartsList_);
                if (Results.Status == PApplication.Failure)
                {
                    lblPartAddMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillParts();
            }
            catch (Exception ex)
            {
                lblPartAddMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_AddPart.Hide();
        }

        string Partsvalidation()
        {
            //int number;
            //if (string.IsNullOrEmpty(txtNumberC.Text.Trim()))
            //{
            //    return "Please enter POS.";
            //}
            //else if (!int.TryParse(txtNumberC.Text.Trim(), out number))
            //{
            //    return "Please enter valid POS.";
            //}
            //else if (string.IsNullOrEmpty(txtPartC.Text.Trim()))
            //{
            //    return "Please enter Part.";
            //}
            //else if (string.IsNullOrEmpty(txtPartDescription.Text.Trim()))
            //{
            //    return "Please enter Part Description.";
            //}
            //else if (string.IsNullOrEmpty(txtQtyC.Text.Trim()))
            //{
            //    return "Please enter Qty.";
            //}
            //else if (!int.TryParse(txtQtyC.Text.Trim(), out number))
            //{
            //    return "Please enter valid Qty.";
            //}
            return "";
        }
 
    }
}