using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSales
{
    public partial class SaleOrderReturn : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewSales_SaleOrderReturn; } }
        int? DealerID = null;
        int? OfficeCodeID = null;
        int? DivisionID = null;
        string CustomerCode = null;
        string SaleOrderReturnNo = null;
        DateTime? SaleOrderReturnDateF = null;
        DateTime? SaleOrderReturnDateT = null;
        int? SaleOrderReturnStatusID = null;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Sales » Sales Return');</script>");
            lblMessageSoReturn.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtSoReturnDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year; ;
                txtSoReturnDateTo.Text = DateTime.Now.ToShortDateString();
                new DDLBind(ddlSoReturnStatus, new BSalesOrderReturn().GetSaleOrderReturnStatus(null, null), "Status", "StatusID");

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
                new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealerCode.SelectedValue), null, null), "OfficeName", "OfficeID", true, "Select");
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
                lblRowCountSoReturn.Visible = false;
                ibtnArrowLeftSoReturn.Visible = false;
                ibtnArrowRightSoReturn.Visible = false;
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
            new DDLBind(ddlOfficeName, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealerCode.SelectedValue), null, null), "OfficeName", "OfficeID", true, "Select");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillSaleOrderReturn();
            }
            catch (Exception e1)
            {
                lblMessageSoReturn.Text = e1.ToString();
                lblMessageSoReturn.ForeColor = Color.Red;
                lblMessageSoReturn.Visible = true;
            }
        }
        void fillSaleOrderReturn(long? SaleOrderReturnID = null)
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                PApiResult Result = new BSalesOrderReturn().GetSaleOrderReturnHeader(DealerID, OfficeCodeID, DivisionID, CustomerCode, SaleOrderReturnID, SaleOrderReturnNo, SaleOrderReturnDateF
                    , SaleOrderReturnDateT, SaleOrderReturnStatusID, PageIndex, gvSoReturn.PageSize);
                gvSoReturn.PageIndex = 0;
                gvSoReturn.DataSource = JsonConvert.DeserializeObject<List<PSaleOrderReturn>>(JsonConvert.SerializeObject(Result.Data)); ;
                gvSoReturn.DataBind();
                if (Result.RowCount == 0)
                {
                    lblRowCountSoReturn.Visible = false;
                    ibtnArrowLeftSoReturn.Visible = false;
                    ibtnArrowRightSoReturn.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvSoReturn.PageSize - 1) / gvSoReturn.PageSize;
                    lblRowCountSoReturn.Visible = true;
                    ibtnArrowLeftSoReturn.Visible = true;
                    ibtnArrowRightSoReturn.Visible = true;
                    lblRowCountSoReturn.Text = (((PageIndex - 1) * gvSoReturn.PageSize) + 1) + " - " + (((PageIndex - 1) * gvSoReturn.PageSize) + gvSoReturn.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("SaleOrderReturn", "fillSaleOrderReturn", e1);
                throw e1;
            }
        }
        void Search()
        {
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            OfficeCodeID = ddlOfficeName.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlOfficeName.SelectedValue);
            DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            SaleOrderReturnDateF = null;
            SaleOrderReturnDateF = string.IsNullOrEmpty(txtSoReturnDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtSoReturnDateFrom.Text.Trim());
            SaleOrderReturnDateT = string.IsNullOrEmpty(txtSoReturnDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtSoReturnDateTo.Text.Trim());

            CustomerCode = string.IsNullOrEmpty(txtCustomer.Text.Trim()) ? null : txtCustomer.Text.Trim();
            SaleOrderReturnNo = txtSoReturnNumber.Text.Trim();
            SaleOrderReturnStatusID = ddlSoReturnStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSoReturnStatus.SelectedValue);
        }
        protected void ibtnArrowLeftSoReturn_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillSaleOrderReturn();
            }
        }
        protected void ibtnArrowRightSoReturn_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillSaleOrderReturn();
            }
        }
        protected void btnSaleOrderReturnCreateBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divSoReturnDetailsView.Visible = false;
            divSaleOrderReturnCreate.Visible = false;
        }
        protected void btnSaleOrderReturnViewBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divSaleOrderReturnCreate.Visible = false;
            divSoReturnDetailsView.Visible = false;
        }
        protected void btnCreateSoReturn_Click(object sender, EventArgs e)
        {
            Clear();
            divList.Visible = false;
            divSoReturnDetailsView.Visible = false;
            divSaleOrderReturnCreate.Visible = true;
            lblMessageSoReturn.Text = "";
            Button BtnView = (Button)sender;
            UC_SaleOrderReturnCreate.FillMaster();
        }
        private void Clear()
        {
            divCreateSalesReturn.Visible = true;
            divSave.Visible = false;
            UC_SaleOrderReturnCreate.Clear();
        }
        protected void btnCreateSalesReturn_Click(object sender, EventArgs e)
        {
            lblMessageSoReturnCreate.Text = "";
            UC_SaleOrderReturnCreate.ReadSoDelivery();
            divSave.Visible = true;
            divCreateSalesReturn.Visible = false;
        }
        protected void btnViewSoReturn_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblPurchaseOrderReturnID = (Label)gvRow.FindControl("lblSaleOrderReturnID");
            divList.Visible = false;
            divSaleOrderReturnCreate.Visible = false;
            divSoReturnDetailsView.Visible = true;
            UC_SaleOrderReturnView.fillViewSoReturn(Convert.ToInt64(lblPurchaseOrderReturnID.Text));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessageSoReturnCreate.Visible = true;
            lblMessageSoReturnCreate.ForeColor = Color.Red;
            string message = UC_SaleOrderReturnCreate.RValidateReturnDelivery();
            if (!string.IsNullOrEmpty(message))
            {
                lblMessageSoReturnCreate.Text = message;
                return;
            }

            List<PSaleOrderReturnItem_Insert> pSoReturnItem = UC_SaleOrderReturnCreate.ReadSoDeliveryItem();
            string result = new BAPI().ApiPut("SaleOrderReturn/SaleOrderReturnCreate", pSoReturnItem);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(result);

            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturnCreate.Text = Result.Message;
                return;
            }

            divList.Visible = false;
            divSoReturnDetailsView.Visible = true;
            divSaleOrderReturnCreate.Visible = false;
            Label lblMessagePOReturn = (Label)UC_SaleOrderReturnView.FindControl("lblMessageSoReturn");
            lblMessagePOReturn.Text = Result.Message;
            lblMessagePOReturn.Visible = true;
            lblMessagePOReturn.ForeColor = Color.Green;
            UC_SaleOrderReturnView.fillViewSoReturn(Convert.ToInt64(Result.Data));
        }

        [WebMethod]
        public static string GetCustomer(string CustS)
        {
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerAutocomplete(CustS, 0);
            return JsonConvert.SerializeObject(Customer);
        }
    }
}