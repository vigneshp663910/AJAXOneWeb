using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.Planning
{
    public partial class DealerStockOrderControl : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_DealerStockOrderControl; } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        public List<PDealerStockOrderControl> DealerStockOrderControlList
        {
            get
            {
                if (ViewState["DealerStockOrderControlList"] == null)
                {
                    ViewState["DealerStockOrderControlList"] = new List<PDealerStockOrderControl>();
                }
                return (List<PDealerStockOrderControl>)ViewState["DealerStockOrderControlList"];
            }
            set
            {
                ViewState["DealerStockOrderControlList"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Planning » Dealer Stock Order Control');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillDealerStockOrderControlList();
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillDealerStockOrderControlList();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillDealerStockOrderControlList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillDealerStockOrderControlList()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                PApiResult Result = new BDMS_PurchaseOrder().GetDealerStockOrderControl(DealerID, PageIndex, gvDealerStockOrderControl.PageSize);
                DealerStockOrderControlList = JsonConvert.DeserializeObject<List<PDealerStockOrderControl>>(JsonConvert.SerializeObject(Result.Data));
                gvDealerStockOrderControl.PageIndex = 0;
                gvDealerStockOrderControl.DataSource = DealerStockOrderControlList;
                gvDealerStockOrderControl.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvDealerStockOrderControl.PageSize - 1) / gvDealerStockOrderControl.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvDealerStockOrderControl.PageSize) + 1) + " - " + (((PageIndex - 1) * gvDealerStockOrderControl.PageSize) + gvDealerStockOrderControl.Rows.Count) + " of " + Result.RowCount;
                }
                foreach (GridViewRow row in gvDealerStockOrderControl.Rows)
                {
                    LinkButton LnkUpdate = (LinkButton)row.FindControl("LnkUpdate");
                    LnkUpdate.Visible = false;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DealerStockOrderControl", "fillDealerStockOrderControlList", e1);
                throw e1;
            }
        }
        protected void gvDealerStockOrderControl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerStockOrderControl.PageIndex = e.NewPageIndex;
            gvDealerStockOrderControl.DataSource = DealerStockOrderControlList;
            gvDealerStockOrderControl.DataBind();
            lblRowCount.Text = (((gvDealerStockOrderControl.PageIndex) * gvDealerStockOrderControl.PageSize) + 1) + " - " + (((gvDealerStockOrderControl.PageIndex) * gvDealerStockOrderControl.PageSize) + gvDealerStockOrderControl.Rows.Count) + " of " + DealerStockOrderControlList.Count;
        }

        protected void LnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton LnkEdit = (LinkButton)sender;
            LnkEdit.Visible = false;
            GridViewRow row = (GridViewRow)(LnkEdit.NamingContainer);
            TextBox txtMaxCount = (TextBox)row.FindControl("txtMaxCount");
            txtMaxCount.Enabled = true;
            TextBox txtMinimumValue = (TextBox)row.FindControl("txtMinimumValue");
            txtMinimumValue.Enabled = true;
            LinkButton LnkUpdate = (LinkButton)row.FindControl("LnkUpdate");
            LnkUpdate.Visible = true;
        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;

            LinkButton LnkDelete = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(LnkDelete.NamingContainer);

            TextBox txtMaxCount = (TextBox)row.FindControl("txtMaxCount");
            TextBox txtMinimumValue = (TextBox)row.FindControl("txtMinimumValue");
            Label lblDealerStockOrderControlID = (Label)row.FindControl("lblDealerStockOrderControlID");
            Label lblDealerID = (Label)row.FindControl("lblDealerID");

            PDealerStockOrderControl orderControl = new PDealerStockOrderControl();
            orderControl.DealerStockOrderControlID = Convert.ToInt32(lblDealerStockOrderControlID.Text);
            orderControl.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) };
            orderControl.MaxCount = (string.IsNullOrEmpty(txtMaxCount.Text)) ? 0 : Convert.ToInt32(txtMaxCount.Text);
            orderControl.MinimumValue = (string.IsNullOrEmpty(txtMinimumValue.Text)) ? 0 : Convert.ToInt32(txtMinimumValue.Text);
            orderControl.IsActive = false;

            string result = new BAPI().ApiPut("PurchaseOrder/InsertOrUpdateDealerStockOrderControl", orderControl);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }            
            fillDealerStockOrderControlList();
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
        }

        protected void LnkUpdate_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;

            LinkButton LnkUpdate = (LinkButton)sender;
            LnkUpdate.Visible = false;
            GridViewRow row = (GridViewRow)(LnkUpdate.NamingContainer);

            TextBox txtMaxCount = (TextBox)row.FindControl("txtMaxCount");
            txtMaxCount.Enabled = false;
            TextBox txtMinimumValue = (TextBox)row.FindControl("txtMinimumValue");
            txtMinimumValue.Enabled = false;
            Label lblDealerStockOrderControlID = (Label)row.FindControl("lblDealerStockOrderControlID");
            Label lblDealerID = (Label)row.FindControl("lblDealerID");

            PDealerStockOrderControl orderControl = new PDealerStockOrderControl();
            orderControl.DealerStockOrderControlID = Convert.ToInt32(lblDealerStockOrderControlID.Text);
            orderControl.Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) };
            orderControl.MaxCount = (string.IsNullOrEmpty(txtMaxCount.Text)) ? 0 : Convert.ToInt32(txtMaxCount.Text);
            orderControl.MinimumValue = (string.IsNullOrEmpty(txtMinimumValue.Text)) ? 0 : Convert.ToInt32(txtMinimumValue.Text);
            orderControl.IsActive = true;

            string result = new BAPI().ApiPut("PurchaseOrder/InsertOrUpdateDealerStockOrderControl", orderControl);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                return;
            }
            LinkButton LnkEdit = (LinkButton)row.FindControl("LnkEdit");
            LnkEdit.Visible = true;
            fillDealerStockOrderControlList();
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
        }
    }
}