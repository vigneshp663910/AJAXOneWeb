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

namespace DealerManagementSystem.ViewECatalogue
{
    public partial class SpcAssembly : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewECatalogue_SpcAssembly; } }

        int? ModelID = null;
        int? DivisionID = null;

        public List<PSpcAssembly> Assembly
        {
            get
            {
                if (ViewState["SPAssemblyImagePSpcAssemblyImage"] == null)
                {
                    ViewState["SPAssemblyImagePSpcAssemblyImage"] = new List<PSpcAssembly>();
                }
                return (List<PSpcAssembly>)ViewState["SPAssemblyImagePSpcAssemblyImage"];
            }
            set
            {
                ViewState["SPAssemblyImagePSpcAssemblyImage"] = value;
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('E Catalogue » Assembly');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
                new DDLBind(ddlModel, new BECatalogue().GetSpcModel(null, null, null), "Model", "ModelID", true, "Select Model");
                fillAssembly();

                List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
                if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CreateAssemblyAndCreatePartsCoordinates).Count() == 0)
                {
                    btnCreateAssembly.Visible = false;
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillAssembly();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void Search()
        {
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            ModelID = ddlModel.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModel.SelectedValue);
        }
        void fillAssembly()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BECatalogue().GetSpcAssembly(DivisionID, ModelID, null,txtAssemblyCode.Text.Trim(), PageIndex, gvAssembly.PageSize);
                Assembly = JsonConvert.DeserializeObject<List<PSpcAssembly>>(JsonConvert.SerializeObject(Result.Data));
                gvAssembly.PageIndex = 0;
                gvAssembly.DataSource = Assembly;
                gvAssembly.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvAssembly.PageSize - 1) / gvAssembly.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvAssembly.PageSize) + 1) + " - " + (((PageIndex - 1) * gvAssembly.PageSize) + gvAssembly.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("PurchaseOrder", "fillPurchaseOrder", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillAssembly();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillAssembly();
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            TraceLogger.Log(DateTime.Now);
            Search();
            //PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderExportToExcel(DealerID, VendorID, PurchaseOrderNo, PurchaseOrderDateF
            //        , PurchaseOrderDateT, PurchaseOrderStatusID, PurchaseOrderTypeID, DivisionID, DealerOfficeID, 0);
            //DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data)); 
            //new BXcel().ExporttoExcel(dt, "Purchase Order Report");
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
            divPurchaseOrderCreate.Visible = false;
        }
        protected void btnPurchaseOrderCreateBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divPurchaseOrderCreate.Visible = false;
        }
        protected void btnPurchaseOrderViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divDetailsView.Visible = false;
        }

        protected void btnViewPO_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                Label lblSpcAssemblyImageID = (Label)gvRow.FindControl("lblSpcAssemblyImageID");
                divList.Visible = false;
                divDetailsView.Visible = true;
                UC_SpcAssemblyView.Clear();
                UC_SpcAssemblyView.fillParts(Convert.ToInt32(lblSpcAssemblyImageID.Text));
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.Message;
            }
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue); 
            new DDLBind(ddlModel, new BECatalogue().GetSpcModel(DivisionID, null, null), "Model", "ModelID", true, "Select Model"); 
        }

        protected void btnAssemblySave_Click(object sender, EventArgs e)
        {
            lblAssemblyEditMessage.ForeColor = Color.Red;
            MPE_AssemblyCreate.Show();
            try
            {
                int ModelID = Convert.ToInt32(ddlModelAssemblyC.SelectedValue);
                string AssemblyCode = txtAssemblyCodeC.Text.Trim();
                string AssemblyDescription = txtAssemblyDescription.Text.Trim();
                string AssemblyType = ddlAssemblyType.SelectedValue;
                string Remarks = txtRemarks.Text.Trim();

                PApiResult Results = new BECatalogue().InsertorUpdateSpcAssembly(null, ModelID, AssemblyCode, AssemblyDescription, AssemblyType, Remarks);
                if (Results.Status == PApplication.Failure)
                {
                    lblAssemblyEditMessage.Text = Results.Message;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = Results.Message;
                fillAssembly();
            }
            catch (Exception ex)
            {
                lblAssemblyEditMessage.Text = ex.Message.ToString();
                return;
            }
            MPE_AssemblyCreate.Hide();
        }

        protected void btnCreateAssembly_Click(object sender, EventArgs e)
        {
            MPE_AssemblyCreate.Show();
            new DDLBind(ddlDivisionC, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select Division");
           // 
            txtAssemblyCodeC.Text = "";
            txtAssemblyDescription.Text = "";
            txtRemarks.Text = "";
        }

        protected void ddlDivisionC_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPE_AssemblyCreate.Show();
            int DivisionCID = Convert.ToInt32(ddlDivisionC.SelectedValue);
            new DDLBind(ddlModelAssemblyC, new BECatalogue().GetSpcModel(DivisionCID, null, null), "Model", "ModelID", true, "Select Model");
        }
    }
}