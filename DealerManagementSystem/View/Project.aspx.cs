using Business;
using ClosedXML.Excel;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.View
{
    public partial class Project : System.Web.UI.Page
    {
        public List<PProject> PProject
        {
            get
            {
                if (Session["Project"] == null)
                {
                    Session["Project"] = new List<PProject>();
                }
                return (List<PProject>)Session["Project"];
            }
            set
            {
                Session["Project"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Projects » Create/Maintain');</script>");

            try
            {
                if (!IsPostBack)
                {
                    new DDLBind(ddlState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");
                    new DDLBind(ddlSState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");
                    //new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(1, null, null, null, null, null), "District", "DistrictID");
                    //new DDLBind(ddlSDistrict, new BDMS_Address().GetDistrict(1, null, null, null, null, null), "District", "DistrictID");
                    //FillGrid(null);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_Project.Show();
                if (!Validation())
                {
                    return;
                }
                PProject project = new PProject();
                if (!string.IsNullOrEmpty(HiddenProjectID.Value))
                {
                    project.ProjectID = Convert.ToInt32(HiddenProjectID.Value);
                    project.ProjectNumber = HiddenProjectID.Value;
                }
                project.ProjectName = txtProjectName.Text.Trim();
                //project.ProjectNumber = "";
                project.EmailDate = Convert.ToDateTime(txtEmailDate.Text.Trim());
                project.TenderNumber = txtTenderNumber.Text.Trim();
                project.State = new PDMS_State();
                project.State.StateID = Convert.ToInt32(ddlState.SelectedValue);
                project.District = new PDMS_District();
                project.District.DistrictID = Convert.ToInt32(ddlDistrict.SelectedValue);
                project.Value = Convert.ToDecimal(txtValue.Text);
                project.L1ContractorName = txtL1ContractorName.Text.Trim();
                project.L1ContractorAddress = txtAddress1.Text.Trim();
                project.L2Bidder = txtL2Bidder.Text.Trim();
                project.L3Bidder = txtL3Bidder.Text.Trim();
                project.ContractAwardDate = Convert.ToDateTime(txtContractAwardDate.Text.Trim());
                project.ContractEndDate = Convert.ToDateTime(txtContractEndDate.Text.Trim());
                project.Remarks = txtRemarks.Text.Trim();
                if (new BProject().InsertOrUpdateProject(project))
                {
                    lblMessage.Text = "Project Was Saved Successfully...";
                    lblMessage.ForeColor = Color.Green;
                    FillGrid(project.ProjectID);
                    ClearField();
                    MPE_Project.Hide();
                }
                else
                {
                    lblAddProjectMessage.Text = "Project Not Saved Successfully...!";
                    lblAddProjectMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblAddProjectMessage.Text = ex.Message.ToString();
                lblAddProjectMessage.ForeColor = Color.Red;
            }
        }
        Boolean Validation()
        {
            lblAddProjectMessage.ForeColor = Color.Red;
            Boolean Ret = true;
            string Message = "";
            if (string.IsNullOrEmpty(txtProjectName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Project Name...!";
                Ret = false;
                txtProjectName.BorderColor = Color.Red;
                goto Message;
            }
            if (string.IsNullOrEmpty(txtEmailDate.Text.Trim()))
            {
                Message = Message + "<br/>Please select the Email Date...!";
                Ret = false;
                txtEmailDate.BorderColor = Color.Red;
                goto Message;
            }
            if (ddlState.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the State";
                Ret = false;
                ddlState.BorderColor = Color.Red;
                goto Message;
            }
            if (ddlDistrict.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the District";
                Ret = false;
                ddlDistrict.BorderColor = Color.Red;
                goto Message;
            }
            if (string.IsNullOrEmpty(txtValue.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Value...!";
                Ret = false;
                txtValue.BorderColor = Color.Red;
                goto Message;
            }
            if (string.IsNullOrEmpty(txtL1ContractorName.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the L1ContractorName...!";
                Ret = false;
                txtL1ContractorName.BorderColor = Color.Red;
                goto Message;
            }
            if (string.IsNullOrEmpty(txtContractAwardDate.Text.Trim()))
            {
                Message = Message + "<br/>Please select the ContractAwardDate...!";
                Ret = false;
                txtContractAwardDate.BorderColor = Color.Red;
                goto Message;
            }
            if (string.IsNullOrEmpty(txtContractEndDate.Text.Trim()))
            {
                Message = Message + "<br/>Please select the ContractEndDate...!";
                Ret = false;
                txtContractEndDate.BorderColor = Color.Red;
                goto Message;
            }
        Message:
            lblAddProjectMessage.Text = Message;
            return Ret;
        }
        void ClearField()
        {
            txtProjectName.Text = string.Empty;
            txtEmailDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            txtTenderNumber.Text = string.Empty;
            ddlState.Items.Clear();
            ddlDistrict.Items.Clear();
            txtValue.Text = string.Empty;
            txtL1ContractorName.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtL2Bidder.Text = string.Empty;
            txtL3Bidder.Text = string.Empty;
            txtContractAwardDate.Text = string.Empty;
            txtContractEndDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            FillGrid(null);
        }
        private void FillGrid(long? ProjectID)
        {
            int? StateID = null, DistrictID = null;
            string ProjectName = null, ProjectNumber = null;
            if (!string.IsNullOrEmpty(txtProjectNumber.Text))
            {
                ProjectNumber = txtProjectNumber.Text;
            }
            if (!string.IsNullOrEmpty(txtSProjectName.Text))
            {
                ProjectName = txtSProjectName.Text.Trim();
            }
            if (ddlSState.SelectedValue != "0")
            {
                StateID = Convert.ToInt32(ddlSState.SelectedValue);
            }
            if (ddlSDistrict.SelectedValue != "0" && ddlSDistrict.SelectedValue != "")
            {
                DistrictID = Convert.ToInt32(ddlSDistrict.SelectedValue);
            }
            DateTime? DateF = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtFromDate.Text.Trim());
            DateTime? DateT = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtToDate.Text.Trim());
            PProject = new BProject().GetProject(null, StateID, DistrictID, DateF, DateT, ProjectName, ProjectNumber);


            if (PProject.Count == 0)
            {
                gvProject.DataSource = null;
                gvProject.DataBind();
                lblRowCount.Visible = false;
                ibtnPjtArrowLeft.Visible = false;
                ibtnPjtArrowRight.Visible = false;
            }
            else
            {
                gvProject.DataSource = PProject;
                gvProject.DataBind();
                lblRowCount.Visible = true;
                ibtnPjtArrowLeft.Visible = true;
                ibtnPjtArrowRight.Visible = true;
                lblRowCount.Text = (((gvProject.PageIndex) * gvProject.PageSize) + 1) + " - " + (((gvProject.PageIndex) * gvProject.PageSize) + gvProject.Rows.Count) + " of " + PProject.Count;
            }

            if (ProjectID != null)
            {
                PProject project = new BProject().GetProject(Convert.ToInt32(ProjectID), null, null, null, null, null, null)[0];
                lblProjectName.Text = project.ProjectName;
                lblEmailDate.Text = project.EmailDate.ToString("dd/MM/yyyy HH:mm:ss");
                lblTenderNumber.Text = project.TenderNumber;
                lblState.Text = project.State.State.ToString();
                lblDistrict.Text = project.District.District.ToString();
                lblValue.Text = project.Value.ToString();
                lblL1ContractorName.Text = project.L1ContractorName;
                lblAddress1.Text = project.L1ContractorAddress;
                lblL2Bidder.Text = project.L2Bidder;
                lblL3Bidder.Text = project.L3Bidder;
                lblContractAwardDate.Text = project.ContractAwardDate.ToString("dd/MM/yyyy");
                lblContractEndDate.Text = project.ContractEndDate.ToString("dd/MM/yyyy");
                lblRemarks.Text = project.Remarks;
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            ClearField();
            MPE_Project.Hide();
        }
        protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvProject.PageIndex = e.NewPageIndex;
                FillGrid(null);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillGrid(null);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            MPE_Project.Show();
            HiddenProjectID.Value = "";
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_Project.Show();
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(null, null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }
        protected void ddlSState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlSDistrict, new BDMS_Address().GetDistrict(null, null, Convert.ToInt32(ddlSState.SelectedValue), null, null, null), "District", "DistrictID");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            divProjectView.Visible = true;
            divProjectList.Visible = false;
            lblAddProjectMessage.Text = "";
            lblMessage.Text = "";
            Button BtnView = (Button)sender;
            int? ProjectID = Convert.ToInt32(BtnView.CommandArgument);
            HiddenProjectID.Value = ProjectID.ToString();
            FillGrid(ProjectID);
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divProjectView.Visible = false;
            divProjectList.Visible = true;
            ClearField();
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Project")
            {
                MPE_Project.Show();
                lblMessage.Text = "";
                lblAddProjectMessage.Text = "";
                int? ProjectID = Convert.ToInt32(HiddenProjectID.Value);
                PProject project = new BProject().GetProject(ProjectID, null, null, null, null, null, null)[0];
                txtProjectName.Text = project.ProjectName;
                txtEmailDate.Text = project.EmailDate.ToString("dd/MM/yyyy HH:mm:ss");
                txtTenderNumber.Text = project.TenderNumber;
                new DDLBind(ddlState, new BDMS_Address().GetState(1, null, null, null), "State", "StateID");
                new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(1, null, null, null, null, null), "District", "DistrictID");
                ddlState.SelectedValue = project.State.StateID.ToString();
                ddlDistrict.SelectedValue = project.District.DistrictID.ToString();
                txtValue.Text = project.Value.ToString();
                txtL1ContractorName.Text = project.L1ContractorName;
                txtAddress1.Text = project.L1ContractorAddress;
                txtL2Bidder.Text = project.L2Bidder;
                txtL3Bidder.Text = project.L3Bidder;
                txtContractAwardDate.Text = project.ContractAwardDate.ToString("dd/MM/yyyy");
                txtContractEndDate.Text = project.ContractEndDate.ToString("dd/MM/yyyy");
                txtRemarks.Text = project.Remarks;
            }
        }

        protected void ibtnPjtArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvProject.PageIndex > 0)
            {
                gvProject.PageIndex = gvProject.PageIndex - 1;
                ProjectBind(gvProject, lblRowCount, PProject);
            }
        }

        protected void ibtnPjtArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvProject.PageCount > gvProject.PageIndex)
            {
                gvProject.PageIndex = gvProject.PageIndex + 1;
                ProjectBind(gvProject, lblRowCount, PProject);
            }
        }
        void ProjectBind(GridView gv, Label lbl, List<PProject> PProject)
        {
            if (PProject.Count > 0)
            {
                gv.DataSource = PProject;
                gv.DataBind();
            }

            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + PProject.Count;
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectExportExcel(PProject, "Project Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void ProjectExportExcel(List<PProject> Projects, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Project Code");
            dt.Columns.Add("Project Name");
            dt.Columns.Add("Email Date");
            dt.Columns.Add("Tender Number");
            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("Value");
            dt.Columns.Add("L1 Contractor Name");
            dt.Columns.Add("L1 Contractor Address");
            dt.Columns.Add("L2 Bidder");
            dt.Columns.Add("L3 Bidder");
            dt.Columns.Add("Contract Award Date");
            dt.Columns.Add("Contract End Date");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Created On");
            dt.Columns.Add("Modified By");
            dt.Columns.Add("Modified On");
            foreach (PProject Project in Projects)
            {
                dt.Rows.Add(
                    "'" + Project.ProjectNumber
                    , Project.ProjectName
                    , Project.EmailDate
                    , Project.TenderNumber
                    , Project.State.State
                    , Project.District.District
                    , Project.Value
                    , Project.L1ContractorName
                    , Project.L1ContractorAddress
                    , Project.L2Bidder
                    , Project.L3Bidder
                    , Project.ContractAwardDate
                    , Project.ContractEndDate
                    , Project.Remarks
                    //, (Project.CreatedBy == null) ? "" : Project.CreatedBy.ContactName
                    //, Project.CreatedOn
                    //, (Project.ModifiedBy == null) ? "" : Project.ModifiedBy.ContactName
                    //, Project.ModifiedOn
                    , ""
                    , ""
                    , ""
                    , ""
                    );
            }
            try
            {
                ExporttoExcel(dt, Name);
            }
            catch
            {

            }
            finally
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>HideProgress();</script>");
            }
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            DivUpload.Visible = true;

            if (BtnUpload.Text == "Submit")
            {
                if (fileUpload.PostedFile != null)
                {
                    try
                    {                       
                        using (XLWorkbook workBook = new XLWorkbook(fileUpload.PostedFile.InputStream))
                        {
                            //Read the first Sheet from Excel file.
                            IXLWorksheet workSheet = workBook.Worksheet(1);

                            //Create a new DataTable.
                            DataTable dt = new DataTable();

                            //Loop through the Worksheet rows.
                            bool firstRow = true;
                            foreach (IXLRow row in workSheet.Rows())
                            {
                                //Use the first row to add columns to DataTable.
                                if (firstRow)
                                {
                                    foreach (IXLCell cell in row.Cells())
                                    {
                                        dt.Columns.Add(cell.Value.ToString());
                                    }
                                    firstRow = false;
                                }
                                else
                                {
                                    //Add rows to DataTable.
                                    dt.Rows.Add();
                                    int i = 0;
                                    foreach (IXLCell cell in row.Cells())
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                        i++;
                                    }
                                }
                                lblMessage.Text = "Your file was uploaded : "+ dt.Rows.Count;
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                //GridView1.DataSource = dt;
                                //GridView1.DataBind();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Your file not uploaded";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                DivUpload.Visible = false;
                BtnUpload.Text = "Upload";
            }
            else
            {
                BtnUpload.Text = "Submit";
            }
        }
        void ExporttoExcel(DataTable table, string strFile)
        {
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFile + ".xls");
            //HttpContext.Current.Response.Charset = "utf-16";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //HttpContext.Current.Response.Write("<font style='font-size:11.0pt; font-family:Calibri;'>");
            //HttpContext.Current.Response.Write("<BR><BR><BR>");
            //HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:11.0pt; font-family:Calibri; background:white;'> <TR>");
            //int columnscount = table.Columns.Count;

            //for (int j = 0; j < columnscount; j++)
            //{
            //    HttpContext.Current.Response.Write("<Td>");
            //    HttpContext.Current.Response.Write("<B>");
            //    HttpContext.Current.Response.Write(table.Columns[j].ToString());
            //    HttpContext.Current.Response.Write("</B>");
            //    HttpContext.Current.Response.Write("</Td>");
            //}
            //HttpContext.Current.Response.Write("</TR>");
            //foreach (DataRow row in table.Rows)
            //{
            //    HttpContext.Current.Response.Write("<TR>");
            //    for (int i = 0; i < table.Columns.Count; i++)
            //    {
            //        HttpContext.Current.Response.Write("<Td>");
            //        HttpContext.Current.Response.Write(row[i].ToString());
            //        HttpContext.Current.Response.Write("</Td>");
            //    }

            //    HttpContext.Current.Response.Write("</TR>");
            //}
            //HttpContext.Current.Response.Write("</Table>");
            //HttpContext.Current.Response.Write("</font>");

            //// Append cookie
            //HttpCookie cookie = new HttpCookie("ExcelDownloadFlag");
            //cookie.Value = "Flag";
            //cookie.Expires = DateTime.Now.AddDays(1);
            //HttpContext.Current.Response.AppendCookie(cookie);
            //// end

            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();












            using (XLWorkbook wb = new XLWorkbook())
            {
                //Create a DataTable with schema same as DataSet Table columns.
                DataTable dt = new DataTable("Projects");
                foreach (DataColumn column in table.Columns)
                {
                    dt.Columns.Add(column.ColumnName);
                }

                DataRow dr = dt.NewRow();
                foreach (DataColumn column in table.Columns)
                {
                    dr[column.ColumnName] = column.ColumnName;
                }
                //Add Header rows from DataSet Table to DataTable.
                dt.Rows.Add(dr);
                //Loop and add rows from DataSet Table to DataTable.
                foreach (DataRow row in table.Rows)
                {
                    dt.ImportRow(row);
                }

                var ws = wb.Worksheets.Add(dt.TableName);
                ws.Cell(1, 1).InsertData(dt.Rows);
                ws.Columns().AdjustToContents();

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Projects.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
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
    }
}