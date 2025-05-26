using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class DealerRetailerConfig : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_DealerRetailerConfig; } }
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
        int? DealerID = null;
        int? RetailerID = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Dealer Retailer Config');</script>");
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
                    fillDealer(ddlDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Dealer), "Select");
                    fillDealer(ddlRetailer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Retailer), "Select");
                    fillDealerRetailerConfig();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message.ToString();
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }
        void LoadDropdown()
        {
            fillDealer(ddlCDealer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Dealer), "Select");
            fillDealer(ddlCRetailer, PSession.User.Dealer.Where(m => m.DealerType.DealerTypeID == (short)DealerType.Retailer), "Select");
        }
        void fillDealer(DropDownList dll, object Data, string select)
        {
            dll.DataTextField = "CodeWithName";
            dll.DataValueField = "DealerID";
            dll.DataSource = Data;
            dll.DataBind();
            dll.Items.Insert(0, new ListItem(select, "0"));
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDropdown();
                ddlCRetailer.Enabled = true;
                MPE_DealerRetailerCreate.Show();
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
                fillDealerRetailerConfig();
            }
            catch (Exception Ex)
            {
                lblMessage.Text = Ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void Search()
        {
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            RetailerID = ddlRetailer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRetailer.SelectedValue);
        }
        void fillDealerRetailerConfig()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                int RowCount = 0;

                PApiResult Result = new BDMS_Dealer().GetDealerRetailerSalesConfig(RetailerID, DealerID, PageIndex, gvDealerRetailerConfig.PageSize);
                List<PDealerReatailer> PDealerReatailer = JsonConvert.DeserializeObject<List<PDealerReatailer>>(JsonConvert.SerializeObject(Result.Data));
                RowCount = Result.RowCount;

                gvDealerRetailerConfig.PageIndex = 0;
                gvDealerRetailerConfig.DataSource = PDealerReatailer;
                gvDealerRetailerConfig.DataBind();

                if (RowCount == 0)
                {
                    gvDealerRetailerConfig.DataSource = null;
                    gvDealerRetailerConfig.DataBind();
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    gvDealerRetailerConfig.DataSource = PDealerReatailer;
                    gvDealerRetailerConfig.DataBind();
                    PageCount = (RowCount + gvDealerRetailerConfig.PageSize - 1) / gvDealerRetailerConfig.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    imgBtnExportExcel.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvDealerRetailerConfig.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerRetailerConfig.PageSize) + gvDealerRetailerConfig.Rows.Count) + " of " + RowCount;
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
                fillDealerRetailerConfig();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillDealerRetailerConfig();
            }
        }

        protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Search();
                PApiResult Result = new BDMS_Dealer().GetDealerRetailerSalesConfig(RetailerID, DealerID, null, null);
                List<PDealerReatailer> PDealerReatailer = JsonConvert.DeserializeObject<List<PDealerReatailer>>(JsonConvert.SerializeObject(Result.Data));

                DataTable Rtdt = new DataTable();
                Rtdt.Columns.Add("Retailer Code");
                Rtdt.Columns.Add("Retailer Name");
                Rtdt.Columns.Add("Dealer Code");
                Rtdt.Columns.Add("Dealer Name");
                Rtdt.Columns.Add("Created By");
                Rtdt.Columns.Add("Created On");
                Rtdt.Columns.Add("Modified By");
                Rtdt.Columns.Add("Modified On");
                foreach (PDealerReatailer Rt in PDealerReatailer)
                {
                    Rtdt.Rows.Add(Rt.Retailer.DealerCode, Rt.Retailer.DealerName, Rt.Dealer.DealerCode, Rt.Dealer.DealerName, Rt.CreatedBy.ContactName, Rt.CreatedOn, (Rt.ModifiedBy == null) ? "" : Rt.ModifiedBy.ContactName, (Rt.ModifiedOn == null) ? "" : Rt.ModifiedOn.ToString());
                }
                new BXcel().ExporttoExcel(Rtdt, "Dealer Retailer Sales Configuration Report");
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
                    MPE_DealerRetailerCreate.Show();
                    return;
                }
                PDealerReatailer_Insert Rt = new PDealerReatailer_Insert();
                Rt.RetailerID = Convert.ToInt32(ddlCRetailer.SelectedValue);
                Rt.DealerID = Convert.ToInt32(ddlCDealer.SelectedValue);
                Rt.IsActive = true;
                PApiResult Results = InsertOrUpdateDealerRetailer(Rt);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessageDealerRetailer.ForeColor = Color.Red;
                    lblMessageDealerRetailer.Text = "Dealer Retailer Configuration is Not Created Successfully.";
                    MPE_DealerRetailerCreate.Show();
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillDealerRetailerConfig();
            }
            catch (Exception ex)
            {
                lblMessageDealerRetailer.ForeColor = Color.Red;
                lblMessageDealerRetailer.Text = ex.Message.ToString();
                MPE_DealerRetailerCreate.Show();
                return;
            }
        }
        PApiResult InsertOrUpdateDealerRetailer(PDealerReatailer_Insert Insert)
        {
            return JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerRetailerSalesConfig", Insert));
        }
        public Boolean Validation()
        {
            Boolean Result = false;
            lblMessageDealerRetailer.ForeColor = Color.Red;
            if (ddlCDealer.SelectedValue == "0")
            {
                lblMessageDealerRetailer.Text = "Please Select Dealer..!";
                Result = true;
            }
            if (ddlCRetailer.SelectedValue == "0")
            {
                lblMessageDealerRetailer.Text = "Please Select Retailer..!";
                Result = true;
            }
            if (btnSave.Text == "Save")
            {
                PApiResult ResultIsExist = new BDMS_Dealer().GetDealerRetailerSalesConfig(Convert.ToInt32(ddlCRetailer.SelectedValue), null, null, null);
                List<PDealerReatailer> PDealerReatailer = JsonConvert.DeserializeObject<List<PDealerReatailer>>(JsonConvert.SerializeObject(ResultIsExist.Data));
                if (PDealerReatailer.Count > 0)
                {
                    lblMessageDealerRetailer.Text = "Retailer already available..!";
                    Result = true;
                }
            }
            return Result;
        }

        protected void lnkEditDealerRetailer_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageDealerRetailer.Text = "";
                lblMessage.Text = "";
                LinkButton lnkEditDealerRetailer = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkEditDealerRetailer.NamingContainer);
                Label lblDealerID = (Label)row.FindControl("lblDealerID");
                Label lblRetailerID = (Label)row.FindControl("lblRetailerID");
                LoadDropdown();
                ddlCDealer.SelectedValue = lblDealerID.Text;
                ddlCRetailer.SelectedValue = lblRetailerID.Text;
                ddlCRetailer.Enabled = false;
                HidRetailerID.Value = lblRetailerID.Text;
                btnSave.Text = "Update";
                MPE_DealerRetailerCreate.Show();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void lnkDeleteDealerRetailer_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDeleteDealerRetailer = (LinkButton)sender;
                GridViewRow row = (GridViewRow)(lnkDeleteDealerRetailer.NamingContainer);
                Label lblDealerID = (Label)row.FindControl("lblDealerID");
                Label lblRetailerID = (Label)row.FindControl("lblRetailerID");
                lblMessage.Text = "";
                PDealerReatailer_Insert Rt = new PDealerReatailer_Insert();
                Rt.RetailerID = Convert.ToInt32(lblRetailerID.Text);
                Rt.DealerID = Convert.ToInt32(lblDealerID.Text);
                Rt.IsActive = false;
                PApiResult Results = InsertOrUpdateDealerRetailer(Rt);
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Dealer retailer is not deleted Successfully.";
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Dealer retailer is deleted Successfully.";
                fillDealerRetailerConfig();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void ActionControlMange()
        {
            try
            {
                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerRetailerCreateUpdateDelete).Count() == 0)
                {
                    for (int i = 0; i < gvDealerRetailerConfig.Rows.Count; i++)
                    {
                        ((LinkButton)gvDealerRetailerConfig.Rows[i].FindControl("lnkEditDealerRetailer")).Visible = false;
                        ((LinkButton)gvDealerRetailerConfig.Rows[i].FindControl("lnkDeleteDealerRetailer")).Visible = false;
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