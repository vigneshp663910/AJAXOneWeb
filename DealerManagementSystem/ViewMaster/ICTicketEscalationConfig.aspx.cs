using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class ICTicketEscalationConfig : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_ICTicketEscalationConfig; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » ICTicket Escalation Config');</script>");
            lblMessageICTicketEscalationConfig.Text = "";
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                try
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                    fillICTicketEscalationConfig();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        void fillDealer(DropDownList dll, object Data, string select)
        {
            dll.DataTextField = "Codewithname";
            dll.DataValueField = "DealerCode";
            dll.DataSource = Data;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem(select, "0"));
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                MPE_ICTicketEscalationConfigCreate.Show();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicketEscalationConfig();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void fillICTicketEscalationConfig()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int RowCount = 0;

                string EscalationHours = null;
                if (ddlSEscalationHours.SelectedValue != "0")
                {
                    EscalationHours = ddlSEscalationHours.SelectedItem.Text.ToString();
                }
                PApiResult Result = new BDMS_Master().GetICTicketMttrEscalationMatrix(EscalationHours, PageIndex, gvICTicketEscalationConfig.PageSize);
                List<PICTicketMttrEscalationMatrix> ICTicketMttrEscalationMatrix = JsonConvert.DeserializeObject<List<PICTicketMttrEscalationMatrix>>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;

                gvICTicketEscalationConfig.PageIndex = 0;
                gvICTicketEscalationConfig.DataSource = ICTicketMttrEscalationMatrix;
                gvICTicketEscalationConfig.DataBind();

                if (RowCount == 0)
                {
                    gvICTicketEscalationConfig.DataSource = null;
                    gvICTicketEscalationConfig.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    imgBtnExportExcel.Visible = false;
                }
                else
                {
                    gvICTicketEscalationConfig.DataSource = ICTicketMttrEscalationMatrix;
                    gvICTicketEscalationConfig.DataBind();
                    PageCount = (RowCount + gvICTicketEscalationConfig.PageSize - 1) / gvICTicketEscalationConfig.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    imgBtnExportExcel.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvICTicketEscalationConfig.PageSize) + 1) + " - " + (((PageIndex - 1) * gvICTicketEscalationConfig.PageSize) + gvICTicketEscalationConfig.Rows.Count) + " of " + RowCount;
                }
                ActionControlMange();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillICTicketEscalationConfig();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillICTicketEscalationConfig();
            }
        }
        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string EscalationHours = null;
                if (ddlSEscalationHours.SelectedValue != "0")
                {
                    EscalationHours = ddlSEscalationHours.SelectedItem.Text.ToString();
                }
                PApiResult Result = new BDMS_Master().GetICTicketMttrEscalationMatrix(EscalationHours, null, null);
                List<PICTicketMttrEscalationMatrix> ICTicketMttrEscalationMatrix = JsonConvert.DeserializeObject<List<PICTicketMttrEscalationMatrix>>(JsonConvert.SerializeObject(Result.Data));

                DataTable Rtdt = new DataTable();
                Rtdt.Columns.Add("Region");
                Rtdt.Columns.Add("Subject");
                Rtdt.Columns.Add("Description");
                Rtdt.Columns.Add("To MailID");
                Rtdt.Columns.Add("Cc MailID");
                Rtdt.Columns.Add("Escalation Hours");
                Rtdt.Columns.Add("Created By");
                Rtdt.Columns.Add("Created On");
                Rtdt.Columns.Add("Modified By");
                Rtdt.Columns.Add("Modified On");
                foreach (PICTicketMttrEscalationMatrix Rt in ICTicketMttrEscalationMatrix)
                {
                    Rtdt.Rows.Add(Rt.Region, Rt.Subject, Rt.Description, Rt.ToMailID, Rt.CcMailID, Rt.EscalationHours, (Rt.CreatedBy == null) ? "" : Rt.CreatedBy.ContactName, (Rt.CreatedOn == null) ? "" : Rt.CreatedOn.ToString(), (Rt.ModifiedBy == null) ? "" : Rt.ModifiedBy.ContactName, (Rt.ModifiedOn == null) ? "" : Rt.ModifiedOn.ToString());
                }
                new BXcel().ExporttoExcel(Rtdt, "ICTicket Escalation Configuration Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Result = Validation();
                if (Result)
                {
                    MPE_ICTicketEscalationConfigCreate.Show();
                    return;
                }
                PICTicketMttrEscalationMatrix_Insert EsConfig = new PICTicketMttrEscalationMatrix_Insert();
                if (btnSave.Text == "Update")
                {
                    EsConfig.EscalationMatrixID = Convert.ToInt32(HidEscalationMatrixID.Value);
                }
                EsConfig.Region = ddlDealer.SelectedValue.ToString();
                EsConfig.Subject = txtSubject.Text;
                EsConfig.Description = txtDescription.Text;
                EsConfig.ToMailID = txtToMailID.Text;
                EsConfig.CcMailID = txtCcMailID.Text;
                EsConfig.EscalationHours = ddlEscalationHours.SelectedValue.ToString();
                EsConfig.IsActive = true;
                PApiResult Results = InsertOrUpdateICTicketMttrEscalationMatrix(EsConfig);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageICTicketEscalationConfig.ForeColor = Color.Red;
                    lblMessageICTicketEscalationConfig.Text = "ICTicket Escalation Configuration is Not Created Successfully.";
                    MPE_ICTicketEscalationConfigCreate.Show();
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillICTicketEscalationConfig();
                Clear();
            }
            catch (Exception ex)
            {
                lblMessageICTicketEscalationConfig.ForeColor = Color.Red;
                lblMessageICTicketEscalationConfig.Text = ex.Message.ToString();
                MPE_ICTicketEscalationConfigCreate.Show();
                return;
            }
        }
        PApiResult InsertOrUpdateICTicketMttrEscalationMatrix(PICTicketMttrEscalationMatrix_Insert Insert)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Master/InsertOrUpdateICTicketMttrEscalationMatrix", Insert));
        }
        public Boolean Validation()
        {
            Boolean Result = false;
            lblMessageICTicketEscalationConfig.ForeColor = Color.Red;
            if (ddlDealer.SelectedValue == "0")
            {
                lblMessageICTicketEscalationConfig.Text = "Please Select Dealer.";
                Result = true;
            }
            if (string.IsNullOrEmpty(txtSubject.Text))
            {
                lblMessageICTicketEscalationConfig.Text = "Please enter subject.";
                Result = true;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                lblMessageICTicketEscalationConfig.Text = "Please enter description.";
                Result = true;
            }
            if (string.IsNullOrEmpty(txtToMailID.Text))
            {
                lblMessageICTicketEscalationConfig.Text = "Please enter ToMailID.";
                Result = true;
            }
            if (string.IsNullOrEmpty(txtCcMailID.Text))
            {
                lblMessageICTicketEscalationConfig.Text = "Please enter ccmailid.";
                Result = true;
            }
            if (ddlEscalationHours.SelectedValue == "0")
            {
                lblMessageICTicketEscalationConfig.Text = "Please Select Escalation Hours.";
                Result = true;
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            string email = txtToMailID.Text;            
            Match match = regex.Match(email);
            if (!match.Success)
            {
                lblMessageICTicketEscalationConfig.Text = "To MailID is not correct.";
                Result = true;
            }
            email = txtCcMailID.Text;
            match = regex.Match(email);
            if (!match.Success)
            {
                lblMessageICTicketEscalationConfig.Text = "Cc MailID is not correct.";
                Result = true;
            }

            if (btnSave.Text == "Save")
            {
                PApiResult ResultIsExist = new BDMS_Master().GetICTicketMttrEscalationMatrix(ddlEscalationHours.SelectedValue, null, null);
                List<PICTicketMttrEscalationMatrix> ICTEConfig = JsonConvert.DeserializeObject<List<PICTicketMttrEscalationMatrix>>(JsonConvert.SerializeObject(ResultIsExist.Data));

                if (ICTEConfig.Any(item => item.Region == ddlDealer.SelectedValue))
                {
                    lblMessageICTicketEscalationConfig.Text = "ICTicket Escalation Config already available.";
                    Result = true;
                }
            }
            return Result;
        }

        protected void lnkEditICTicketEscalationConfig_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageICTicketEscalationConfig.Text = "";
                lblMessage.Text = "";
                LinkButton lnkEditICTicketEscalationConfig = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkEditICTicketEscalationConfig.NamingContainer);
                Label lblEscalationMatrixID = (Label)row.FindControl("lblEscalationMatrixID");
                Label lblRegion = (Label)row.FindControl("lblRegion");
                Label lblSubject = (Label)row.FindControl("lblSubject");
                Label lblDescription = (Label)row.FindControl("lblDescription");
                Label lblToMailID = (Label)row.FindControl("lblToMailID");
                Label lblCcMailID = (Label)row.FindControl("lblCcMailID");
                Label lblEscalationHours = (Label)row.FindControl("lblEscalationHours");
                fillDealer(ddlDealer, PSession.User.Dealer, "Select");
                ddlDealer.SelectedValue = "0";
                if (PSession.User.Dealer.Any(item=>item.DealerCode== lblRegion.Text))
                {
                    ddlDealer.SelectedValue = lblRegion.Text;
                }
                else
                {
                    lblMessage.Text = "This Region is not allowed to Edit.";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                txtSubject.Text = lblSubject.Text;
                txtDescription.Text = lblDescription.Text;
                txtToMailID.Text = lblToMailID.Text;
                txtCcMailID.Text = lblCcMailID.Text;
                ddlEscalationHours.SelectedValue = lblEscalationHours.Text;
                divRegion.Visible = false;
                divEscalationHours.Visible = false;
                divDescription.Visible = false;
                HidEscalationMatrixID.Value = lblEscalationMatrixID.Text;
                btnSave.Text = "Update";
                MPE_ICTicketEscalationConfigCreate.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lnkDeleteICTicketEscalationConfig_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageICTicketEscalationConfig.Text = "";
                lblMessage.Text = "";
                LinkButton lnkDeleteICTicketEscalationConfig = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkDeleteICTicketEscalationConfig.NamingContainer);
                Label lblEscalationMatrixID = (Label)row.FindControl("lblEscalationMatrixID");
                Label lblRegion = (Label)row.FindControl("lblRegion");
                Label lblSubject = (Label)row.FindControl("lblSubject");
                Label lblDescription = (Label)row.FindControl("lblDescription");
                Label lblToMailID = (Label)row.FindControl("lblToMailID");
                Label lblCcMailID = (Label)row.FindControl("lblCcMailID");
                Label lblEscalationHours = (Label)row.FindControl("lblEscalationHours");
                lblMessage.Text = "";

                PICTicketMttrEscalationMatrix_Insert EsConfig = new PICTicketMttrEscalationMatrix_Insert();
                EsConfig.EscalationMatrixID = Convert.ToInt32(lblEscalationMatrixID.Text);
                EsConfig.Region = lblRegion.Text;
                EsConfig.Subject = lblSubject.Text;
                EsConfig.Description = lblDescription.Text;
                EsConfig.ToMailID = lblToMailID.Text;
                EsConfig.CcMailID = lblCcMailID.Text;
                EsConfig.EscalationHours = lblEscalationHours.Text;
                EsConfig.IsActive = false;
                PApiResult Results = InsertOrUpdateICTicketMttrEscalationMatrix(EsConfig);

                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "ICTicket Escalation Config is not deleted Successfully.";
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "ICTicket Escalation Config is deleted Successfully.";
                fillICTicketEscalationConfig();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void Clear()
        {
            fillDealer(ddlDealer, PSession.User.Dealer, "Select");            
            ddlDealer.SelectedValue = "0";
            txtSubject.Text="";
            txtToMailID.Text = "";
            txtCcMailID.Text = "";
            ddlEscalationHours.SelectedValue = "24";
            txtDescription.Text = "";
            divRegion.Visible = true;
            divEscalationHours.Visible = false;
            divDescription.Visible = true;
            btnSave.Text = "Save";
        }
        void ActionControlMange()
        {
            try
            {
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ICTicketEscalationCreateUpdateDelete).Count() == 0)
                {
                    for (int i = 0; i < gvICTicketEscalationConfig.Rows.Count; i++)
                    {
                        ((LinkButton)gvICTicketEscalationConfig.Rows[i].FindControl("lnkEditICTicketEscalationConfig")).Visible = false;
                        ((LinkButton)gvICTicketEscalationConfig.Rows[i].FindControl("lnkDeleteICTicketEscalationConfig")).Visible = false;
                    }
                    btnCreate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}