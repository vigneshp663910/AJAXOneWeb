using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketMarginWarrantyReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewService_ICTicketMarginWarrantyReport; } }
        int? DealerID = null;
        string ICTicket = null;
        DateTime? MarginWarrantyRequestedDateFrom = null;
        DateTime? MarginWarrantyRequestedDateTo = null;
        bool? IsApproved = null;
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
        //public DataTable MarginWarrantyChange
        //{
        //    get
        //    {
        //        if (ViewState["MarginWarrantyChange"] == null)
        //        {
        //            ViewState["MarginWarrantyChange"] = new DataTable();
        //        }
        //        return (DataTable)ViewState["MarginWarrantyChange"];
        //    }
        //    set
        //    {
        //        ViewState["MarginWarrantyChange"] = value;
        //    }
        //}
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageMarginWarrantyChangeReport.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » Margin Warranty Change Report');</script>");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }

            if (!IsPostBack)
            {
                PageCount = 0;
                PageIndex = 1;
                txtRequestedDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtRequestedDateTo.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0].DID.ToString()));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCountMarginWarrantyChange.Visible = false;
                ibtnMarginWarrantyChangeArrowLeft.Visible = false;
                ibtnMarginWarrantyChangeArrowRight.Visible = false;
            }
        }
        private void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillMarginWarrantyChange();
        }
        private void fillMarginWarrantyChange()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                int RowCount = 0;
                Search();
                DataTable MarginWarrantyChange = new BDMS_ICTicket().GetMarginWarrantyChange(DealerID, null, null, ICTicket, IsApproved, PSession.User.UserID,  PageIndex, gvMarginWarrantyChange.PageSize, out RowCount);

                gvMarginWarrantyChange.PageIndex = 0;
                gvMarginWarrantyChange.DataSource = MarginWarrantyChange;
                gvMarginWarrantyChange.DataBind();
                

                if (RowCount == 0)
                {
                    lblRowCountMarginWarrantyChange.Visible = false;
                    ibtnMarginWarrantyChangeArrowLeft.Visible = false;
                    ibtnMarginWarrantyChangeArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvMarginWarrantyChange.PageSize - 1) / gvMarginWarrantyChange.PageSize;
                    lblRowCountMarginWarrantyChange.Visible = true;
                    ibtnMarginWarrantyChangeArrowLeft.Visible = true;
                    ibtnMarginWarrantyChangeArrowRight.Visible = true;
                    //lblRowCountMarginWarrantyChange.Text = (((gvMarginWarrantyChange.PageIndex) * gvMarginWarrantyChange.PageSize) + 1) + " - " + (((gvMarginWarrantyChange.PageIndex) * gvMarginWarrantyChange.PageSize) + gvMarginWarrantyChange.Rows.Count) + " of " + gvMarginWarrantyChange.Rows.Count;
                    lblRowCountMarginWarrantyChange.Text = (((PageIndex - 1) * gvMarginWarrantyChange.PageSize) + 1) + " - " + (((PageIndex - 1) * gvMarginWarrantyChange.PageSize) + gvMarginWarrantyChange.Rows.Count) + " of " + RowCount;
                }
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("ICTicketMarginWarrantyReport", "fillMarginWarrantyChange", e1);
                throw e1;
            }
        }
        void Search()
        {
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            ICTicket = txtICTicket.Text.Trim();
            MarginWarrantyRequestedDateFrom = string.IsNullOrEmpty(txtRequestedDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateFrom.Text.Trim());
            MarginWarrantyRequestedDateTo = string.IsNullOrEmpty(txtRequestedDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateTo.Text.Trim());
           if (ddlStatus.SelectedValue == "1") { IsApproved = true; } else if (ddlStatus.SelectedValue == "2") { IsApproved = false; }
        }

        protected void ibtnMarginWarrantyChangeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            //if (gvMarginWarrantyChange.PageIndex > 0)
            //{
            //    gvMarginWarrantyChange.PageIndex = gvMarginWarrantyChange.PageIndex - 1;
            //    MarginWarrantyChangeBind(gvMarginWarrantyChange, lblRowCountMarginWarrantyChange, MarginWarrantyChange);
            //}
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                fillMarginWarrantyChange();
            }
        }
        protected void ibtnMarginWarrantyChangeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            //if (gvMarginWarrantyChange.PageCount > gvMarginWarrantyChange.PageIndex)
            //{
            //    gvMarginWarrantyChange.PageIndex = gvMarginWarrantyChange.PageIndex + 1;
            //    MarginWarrantyChangeBind(gvMarginWarrantyChange, lblRowCountMarginWarrantyChange, MarginWarrantyChange);
            //}
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                fillMarginWarrantyChange();
            }
        }
        //protected void gvMarginWarrantyChange_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvMarginWarrantyChange.PageIndex = e.NewPageIndex;
        //    fillMarginWarrantyChange();
        //}
        //void MarginWarrantyChangeBind(GridView gv, Label lbl, DataTable EquipChgReq)
        //{
        //    gv.DataSource = EquipChgReq;
        //    gv.DataBind();
        //    lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + EquipChgReq.Rows.Count;
        //}
    }
}