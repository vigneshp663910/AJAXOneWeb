using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserPurchaseOrderTypeMapping : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_UserPurchaseOrderTypeMapping; } }
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
        public List<PUserPurchaseOrderTypeMapping> GetUserPurchaseOrderTypeMapping
        {
            get
            {
                if (ViewState["GetUserPurchaseOrderTypeMapping"] == null)
                {
                    ViewState["GetUserPurchaseOrderTypeMapping"] = new List<PUserPurchaseOrderTypeMapping>();
                }
                return (List<PUserPurchaseOrderTypeMapping>)ViewState["GetUserPurchaseOrderTypeMapping"];
            }
            set
            {
                ViewState["GetUserPurchaseOrderTypeMapping"] = value;
            }
        }
        public List<PUserPurchaseOrderTypeMapping> GetUserPurchaseOrderTypeMappingUpdated
        {
            get
            {
                if (ViewState["GetUserPurchaseOrderTypeMappingUpdated"] == null)
                {
                    ViewState["GetUserPurchaseOrderTypeMappingUpdated"] = new List<PUserPurchaseOrderTypeMapping>();
                }
                return (List<PUserPurchaseOrderTypeMapping>)ViewState["GetUserPurchaseOrderTypeMappingUpdated"];
            }
            set
            {
                ViewState["GetUserPurchaseOrderTypeMappingUpdated"] = value;
            }
        }
        public List<PUserPurchaseOrderTypeMapping> GetUserPurchaseOrderTypeMappingList
        {
            get
            {
                if (ViewState["GetUserPurchaseOrderTypeMappingList"] == null)
                {
                    ViewState["GetUserPurchaseOrderTypeMappingList"] = new List<PUserPurchaseOrderTypeMapping>();
                }
                return (List<PUserPurchaseOrderTypeMapping>)ViewState["GetUserPurchaseOrderTypeMappingList"];
            }
            set
            {
                ViewState["GetUserPurchaseOrderTypeMappingList"] = value;
            }
        }
        int? DealerID = null;
        int? DealerEmployeeID = null;
        int? DepartmentID = null;
        int? DesignationID = null;
        int? PurchaseOrderTypeID = null;
        bool? IsActive = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » User Purchase Order Type Mapping');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                    new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);

                    new DDLBind().FillDealerAndEngneer(ddlRDealer, null);
                    new BDMS_Dealer().GetDealerEmployeeDDL(ddlEmployee, Convert.ToInt32(ddlRDealer.SelectedValue));
                    new DDLBind(ddlRPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlRDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlRDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);

                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;

                    lblRRowCount.Visible = false;
                    ibtnRArrowLeft.Visible = false;
                    ibtnRArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerID = (ddlDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);
        }
        void Search()
        {
            DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            PurchaseOrderTypeID = ddlPurchaseOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPurchaseOrderType.SelectedValue);
            DepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            DesignationID = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue);
            if (ddlIsActive.SelectedValue == "1") { IsActive = true; } else if (ddlIsActive.SelectedValue == "2") { IsActive = false; }
        }
        void Fill()
        {
            try
            {
                if (ddlDealer.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select Dealer...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (ddlPurchaseOrderType.SelectedValue == "0")
                {
                    lblMessage.Text = "Please Select PO Type...!";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }

                Search();
                PApiResult Result = new PApiResult();

                Result = new BUserPurchaseOrderTypeMapping().GetUserPurchaseOrderTypeMapping(DealerID, PurchaseOrderTypeID, DepartmentID, DesignationID, IsActive, PageIndex, gvUserPurchaseOrderTypeMapping.PageSize);
                GetUserPurchaseOrderTypeMapping = JsonConvert.DeserializeObject<List<PUserPurchaseOrderTypeMapping>>(JsonConvert.SerializeObject(Result.Data));
                GetUserPurchaseOrderTypeMappingUpdated = null;
                gvUserPurchaseOrderTypeMapping.DataSource = GetUserPurchaseOrderTypeMapping;
                gvUserPurchaseOrderTypeMapping.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvUserPurchaseOrderTypeMapping.PageSize - 1) / gvUserPurchaseOrderTypeMapping.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvUserPurchaseOrderTypeMapping.PageSize) + 1) + " - " + (((PageIndex - 1) * gvUserPurchaseOrderTypeMapping.PageSize) + gvUserPurchaseOrderTypeMapping.Rows.Count) + " of " + Result.RowCount;
                }
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
                Fill();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                Fill();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageCount = 0;
                PageIndex = 1;
                Fill();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void ChkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblUserID = (Label)gvRow.FindControl("lblUserID");
                CheckBox ChkIsActive = (CheckBox)gvRow.FindControl("ChkIsActive");
                if ((GetUserPurchaseOrderTypeMappingUpdated.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text))))
                {
                    GetUserPurchaseOrderTypeMappingUpdated.RemoveAll(p => p.User.UserID == Convert.ToInt32(lblUserID.Text));
                }
                if (!GetUserPurchaseOrderTypeMapping.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text) && z.IsActive == ChkIsActive.Checked))
                {
                    PUserPurchaseOrderTypeMapping item = new PUserPurchaseOrderTypeMapping();
                    item.User = new PUser { UserID = Convert.ToInt32(lblUserID.Text) };
                    item.IsActive = ChkIsActive.Checked;
                    GetUserPurchaseOrderTypeMappingUpdated.Add(item);
                }
                if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.UserPurchaseOrderTypeMappingUpdate).Count() > 0)
                {
                    BtnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void cbIsActiveH_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                CheckBox cbIsActiveH = (CheckBox)sender;
                CheckBox chckheader = (CheckBox)gvUserPurchaseOrderTypeMapping.HeaderRow.FindControl("cbIsActiveH");
                chckheader.Checked = cbIsActiveH.Checked;

                foreach (GridViewRow Row in gvUserPurchaseOrderTypeMapping.Rows)
                {
                    Label lblUserID = (Label)Row.FindControl("lblUserID");
                    CheckBox ChkIsActive = (CheckBox)Row.FindControl("ChkIsActive");
                    ChkIsActive.Checked = cbIsActiveH.Checked;

                    if ((GetUserPurchaseOrderTypeMappingUpdated.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text))))
                    {
                        GetUserPurchaseOrderTypeMappingUpdated.RemoveAll(p => p.User.UserID == Convert.ToInt32(lblUserID.Text));
                    }
                    if (!GetUserPurchaseOrderTypeMapping.Any(z => z.User.UserID == Convert.ToInt32(lblUserID.Text) && z.IsActive == ChkIsActive.Checked))
                    {
                        PUserPurchaseOrderTypeMapping item = new PUserPurchaseOrderTypeMapping();
                        item.User = new PUser { UserID = Convert.ToInt32(lblUserID.Text) };
                        item.IsActive = cbIsActiveH.Checked;
                        GetUserPurchaseOrderTypeMappingUpdated.Add(item);
                    }
                }
                GetUserPurchaseOrderTypeMappingUpdated.RemoveAll(p => GetUserPurchaseOrderTypeMapping.Any(z => z.User.UserID == p.User.UserID && z.IsActive == p.IsActive));
                if (PSession.User.SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.UserPurchaseOrderTypeMappingUpdate).Count() > 0)
                {
                    BtnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                foreach (PUserPurchaseOrderTypeMapping user in GetUserPurchaseOrderTypeMappingUpdated)
                {
                    PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("UserPurchaseOrderTypeMapping/InsertOrUpdateUserPurchaseOrderTypeMapping?UserID=" + Convert.ToInt32(user.User.UserID) + "&PurchaseOrderTypeID=" + Convert.ToInt32(ddlPurchaseOrderType.SelectedValue) + "&IsActive=" + user.IsActive));
                    if (result.Status == PApplication.Failure)
                    {
                        lblMessage.Text = result.Message;
                        return;
                    }
                    lblMessage.Text = result.Message;
                    lblMessage.ForeColor = Color.Green;
                }
                Fill();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
            }
        }
        protected void ddlRDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DealerID = (ddlRDealer.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlRDealer.SelectedValue);
            new BDMS_Dealer().GetDealerEmployeeDDL(ddlEmployee, DealerID);
        }
        protected void ddlRDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            new BDMS_Dealer().GetDealerDesignationDDL(ddlRDesignation, Convert.ToInt32(ddlRDepartment.SelectedValue), null, null);
        }
        protected void tbpUserPurchaseOrderType_ActiveTabChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                    new DDLBind(ddlPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlDesignation, Convert.ToInt32(ddlDepartment.SelectedValue), null, null);

                    new DDLBind().FillDealerAndEngneer(ddlRDealer, null);
                    new BDMS_Dealer().GetDealerEmployeeDDL(ddlEmployee, Convert.ToInt32(ddlRDealer.SelectedValue));
                    new DDLBind(ddlRPurchaseOrderType, new BProcurementMasters().GetPurchaseOrderType(null, null), "PurchaseOrderType", "PurchaseOrderTypeID");
                    new BDMS_Dealer().GetDealerDepartmentDDL(ddlRDepartment, null, null);
                    new BDMS_Dealer().GetDealerDesignationDDL(ddlRDesignation, Convert.ToInt32(ddlRDepartment.SelectedValue), null, null);

                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;

                    lblRRowCount.Visible = false;
                    ibtnRArrowLeft.Visible = false;
                    ibtnRArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void btnRSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageCount = 0;
                PageIndex = 1;
                FillReport();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void FillReport()
        {
            try
            {
                SearchReportFilter();
                PApiResult Result = new PApiResult();

                Result = new BUserPurchaseOrderTypeMapping().GetUserPurchaseOrderTypeMappingReport(DealerID, DealerEmployeeID, PurchaseOrderTypeID, DepartmentID, DesignationID, PageIndex, gvUserPurchaseOrderTypeReport.PageSize);
                GetUserPurchaseOrderTypeMappingList = JsonConvert.DeserializeObject<List<PUserPurchaseOrderTypeMapping>>(JsonConvert.SerializeObject(Result.Data));

                gvUserPurchaseOrderTypeReport.DataSource = GetUserPurchaseOrderTypeMappingList;
                gvUserPurchaseOrderTypeReport.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRRowCount.Visible = false;
                    ibtnRArrowLeft.Visible = false;
                    ibtnRArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvUserPurchaseOrderTypeReport.PageSize - 1) / gvUserPurchaseOrderTypeReport.PageSize;
                    lblRRowCount.Visible = true;
                    ibtnRArrowLeft.Visible = true;
                    ibtnRArrowRight.Visible = true;
                    lblRRowCount.Text = (((PageIndex - 1) * gvUserPurchaseOrderTypeReport.PageSize) + 1) + " - " + (((PageIndex - 1) * gvUserPurchaseOrderTypeReport.PageSize) + gvUserPurchaseOrderTypeReport.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void SearchReportFilter()
        {
            DealerID = ddlRDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRDealer.SelectedValue);
            PurchaseOrderTypeID = ddlRPurchaseOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRPurchaseOrderType.SelectedValue);
            DepartmentID = ddlRDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRDepartment.SelectedValue);
            DesignationID = ddlRDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRDesignation.SelectedValue);
            DealerEmployeeID = ddlEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlEmployee.SelectedValue);
        }
        protected void ibtnRArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FillReport();
            }
        }
        protected void ibtnRArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FillReport();
            }
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SearchReportFilter();
                PApiResult Result = new PApiResult();

                Result = new BUserPurchaseOrderTypeMapping().GetUserPurchaseOrderTypeMappingReport_Excel(DealerID, DealerEmployeeID, PurchaseOrderTypeID, DepartmentID, DesignationID);
                DataSet DS = JsonConvert.DeserializeObject<DataSet>(JsonConvert.SerializeObject(Result.Data));
                new BXcel().ExporttoExcel(DS.Tables[0], "User Purchase Order Type Report");
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}