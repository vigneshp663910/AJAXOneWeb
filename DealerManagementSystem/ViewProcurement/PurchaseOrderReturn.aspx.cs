using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement
{
    public partial class PurchaseOrderReturn : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewProcurement_PurchaseOrderReturn; } }
        int? DealerID = null;
        int? DealerOfficeID = null;
        string PurchaseOrderReturnNo = null;
        DateTime? PurchaseOrderReturnDateF = null;
        DateTime? PurchaseOrderReturnDateT = null;
        int? PurchaseOrderReturnStatusID = null;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Purchase Return');</script>");
            lblMessagePoReturn.Text = ""; 
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtPoReturnDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtPoReturnDateTo.Text = DateTime.Now.ToShortDateString();
                //new DDLBind(ddlPoReturnStatus, new BDMS_PurchaseOrder().GetPurchaseOrderReturnStatus(null, null), "PurchaseOrderReturnStatusDescription", "PurchaseOrderReturnStatusID");
                
                fillDealer();
                ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
                lblRowCountPoReturn.Visible = false;
                ibtnArrowLeftPoReturn.Visible = false;
                ibtnArrowRightPoReturn.Visible = false;
            }
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void ddlDealerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDealerOffice();
        }
        private void FillDealerOffice()
        {
            DealerID = Convert.ToInt32(ddlDealerCode.SelectedValue);
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillPurchaseOrderReturn();
            }
            catch (Exception e1)
            {
                lblMessagePoReturn.Text = e1.ToString();
                lblMessagePoReturn.ForeColor = Color.Red;
                lblMessagePoReturn.Visible = true;
            }
        }        
        void fillPurchaseOrderReturn(long? PurchaseOrderReturnID =null)
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BDMS_PurchaseOrder().GetPurchaseOrderReturnHeader(DealerID, PurchaseOrderReturnNo, PurchaseOrderReturnDateF
                    , PurchaseOrderReturnDateT, DealerOfficeID, PageIndex, gvPoReturn.PageSize);
                gvPoReturn.PageIndex = 0;
                gvPoReturn.DataSource = JsonConvert.DeserializeObject<List<PPurchaseOrderReturn>>(JsonConvert.SerializeObject(Result.Data)); ;
                gvPoReturn.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCountPoReturn.Visible = false;
                    ibtnArrowLeftPoReturn.Visible = false;
                    ibtnArrowRightPoReturn.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvPoReturn.PageSize - 1) / gvPoReturn.PageSize;
                    lblRowCountPoReturn.Visible = true;
                    ibtnArrowLeftPoReturn.Visible = true;
                    ibtnArrowRightPoReturn.Visible = true;
                    lblRowCountPoReturn.Text = (((PageIndex - 1) * gvPoReturn.PageSize) + 1) + " - " + (((PageIndex - 1) * gvPoReturn.PageSize) + gvPoReturn.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("PurchaseOrderReturn", "fillPurchaseOrderReturn", e1);
                throw e1;
            }
        }
        void Search()
        {
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            DealerOfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            PurchaseOrderReturnDateF = null;
            PurchaseOrderReturnDateF = string.IsNullOrEmpty(txtPoReturnDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPoReturnDateFrom.Text.Trim());
            PurchaseOrderReturnDateT = string.IsNullOrEmpty(txtPoReturnDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPoReturnDateTo.Text.Trim());

            PurchaseOrderReturnStatusID = ddlPoReturnStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPoReturnStatus.SelectedValue);
            PurchaseOrderReturnNo = txtPoReturnNumber.Text.Trim();
        }
        protected void ibtnArrowLeftPoReturn_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillPurchaseOrderReturn();
            }
        }
        protected void ibtnArrowRightPoReturn_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillPurchaseOrderReturn();
            }
        }
        protected void btnPurchaseOrderReturnCreateBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divPurchaseOrderReturnCreate.Visible = false;
        }
        protected void btnPurchaseOrderReturnViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divPoReturnDetailsView.Visible = false;
        }
        protected void btnCreatePoReturn_Click(object sender, EventArgs e)
        {
            divList.Visible = false;
            divPurchaseOrderReturnCreate.Visible = true;
            lblMessagePoReturn.Text = "";
            Button BtnView = (Button)sender;
            UC_PurchaseOrderReturnCreate.FillMaster();
        }
        protected void btnViewPoReturn_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblPurchaseOrderReturnID = (Label)gvRow.FindControl("lblPurchaseOrderReturnID");
            divList.Visible = false;
            divPoReturnDetailsView.Visible = true;
            UC_PurchaseOrderReturnView.fillViewPoReturn(Convert.ToInt64(lblPurchaseOrderReturnID.Text));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = UC_PurchaseOrderReturnCreate.Validation();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessagePoReturnCreate.Text = message;
                lblMessagePoReturnCreate.Visible = true;
                lblMessagePoReturnCreate.ForeColor = Color.Red;
                return;
            }
            List<PPurchaseOrderReturnItem_Insert> pGrItem = UC_PurchaseOrderReturnCreate.Read(); 
            string result = new BAPI().ApiPut("PurchaseOrder/PurchaseOrderReturnCreate", pGrItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessagePoReturnCreate.Text = Result.Message;
                return;
            }   
            divList.Visible = false;
            divPoReturnDetailsView.Visible = true;
            divPurchaseOrderReturnCreate.Visible = false;
            Label lblMessagePOReturn = (Label)UC_PurchaseOrderReturnView.FindControl("lblMessagePOReturn");
            lblMessagePOReturn.Text = Result.Message;
            lblMessagePOReturn.Visible = true;
            lblMessagePOReturn.ForeColor = Color.Green;
            UC_PurchaseOrderReturnView.fillViewPoReturn(Convert.ToInt64(Result.Data));
        }
    }
}