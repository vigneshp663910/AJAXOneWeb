using Business;
using Properties;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement
{
    public partial class PurchaseOrderASNReport : System.Web.UI.Page
    {
        public DataTable Asn
        {
            get
            {
                if (Session["DMS_PurchaseOrderASNReport"] == null)
                {
                    Session["DMS_PurchaseOrderASNReport"] = new DataTable();
                }
                return (DataTable)Session["DMS_PurchaseOrderASNReport"];
            }
            set
            {
                Session["DMS_PurchaseOrderASNReport"] = value;
            }
        }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Procurement » Advance Shipping Notice');</script>");
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0].DID.ToString()));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                txtAsnDateF.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtAsnDateT.Text = DateTime.Now.ToShortDateString();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillAsn();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillAsn()
        {
            int? DealerID = null;
            string CustomerCode = "";

            string AsnNumber = "";
            DateTime? AsnDateF = null;
            DateTime? AsnDateT = null;
            string PurchaseOrderNo = "";
            DateTime? PurchaseOrderDateF = null;
            DateTime? PurchaseOrderDateT = null;

            string SaleOrderInvoiceStatusID = "";
            try
            {
                TraceLogger.Log(DateTime.Now);
                //int PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
                //int PageNo = Convert.ToInt32(ddlPageNo.SelectedValue);

                DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                CustomerCode = txtCustomerCode.Text.Trim();

                AsnNumber = txtAsnNumber.Text.Trim();
                AsnDateF = string.IsNullOrEmpty(txtAsnDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAsnDateF.Text.Trim());
                AsnDateT = string.IsNullOrEmpty(txtAsnDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAsnDateT.Text.Trim());
                int? PurchaseOrderAsnStatusID = ddlPurchaseOrderAsnStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPurchaseOrderAsnStatus.SelectedValue);

                PurchaseOrderNo = txtPurchaseOrderNo.Text.Trim();
                PurchaseOrderDateF = string.IsNullOrEmpty(txtPurchaseOrderDateF.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPurchaseOrderDateF.Text.Trim());
                PurchaseOrderDateT = string.IsNullOrEmpty(txtPurchaseOrderDateT.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtPurchaseOrderDateT.Text.Trim());

                int? PurchaseOrderTypeID = ddlPurchaseOrderType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlPurchaseOrderType.SelectedValue);

                DataTable SOIs = null;

                GridView gv = null;
                gvICTickets.Visible = false;
                gvDM.Visible = false;
                gvDCM.Visible = false;

                SOIs = new BDMS_PurchaseOrder().GetPurchaseOrderAsnReport(DealerID, CustomerCode, AsnNumber, AsnDateF, AsnDateT, PurchaseOrderAsnStatusID, PurchaseOrderNo, PurchaseOrderDateF, PurchaseOrderDateT, PurchaseOrderTypeID, txtMaterial.Text.Trim(), PSession.User.UserID);
                gv = gvICTickets;
                gv.Visible = true;

                //if (ddlDealerCode.SelectedValue == "0")
                //{
                //    var SOIs1 = (from S in SOIs
                //                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
                //                 select new
                //                 {
                //                     S
                //                 }).ToList();
                //    SOIs.Clear();
                //    foreach (var w in SOIs1)
                //    {
                //        SOIs.Add(w.S);
                //    }
                //}

                Asn = SOIs;


                gv.PageIndex = 0;
                gv.DataSource = SOIs;
                gv.DataBind();
                if (Asn.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Asn.Rows.Count;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_SalesOrderItems", "fillSalesOrderItems", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;

            gv = gvICTickets;
            if (gv.PageIndex > 0)
            {
                gv.PageIndex = gv.PageIndex - 1;
                gv.DataSource = Asn;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Asn.Rows.Count;
            }
        }

        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            GridView gv = null;

            gv = gvICTickets;

            if (gv.PageCount > gv.PageIndex)
            {

                gv.PageIndex = gv.PageIndex + 1;
                gv.DataSource = Asn;
                gv.DataBind();
                lblRowCount.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Asn.Rows.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(Asn, "Sales Invoice Dealer-Customer-Material Wise");


        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = Asn;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + Asn.Rows.Count;

        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ddlSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    if (ddlSalesType.SelectedValue == "0")
            //    {
            //        ddlSOStatus.Items.Insert(0, new ListItem("Customer Order", "0"));
            //    }
            //    else
            //    {
            //        ddlSOStatus.Items.Insert(0, new ListItem("All", "0"));
            //        ddlSOStatus.Items.Insert(1, new ListItem("Machine Quotation", "1"));
            //        ddlSOStatus.Items.Insert(1, new ListItem("Machine Order", "1"));
            //    }

        }

        protected void gvDM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDM.PageIndex = e.NewPageIndex;
            gvDM.DataSource = Asn;
            gvDM.DataBind();
            lblRowCount.Text = (((gvDM.PageIndex) * gvDM.PageSize) + 1) + " - " + (((gvDM.PageIndex) * gvDM.PageSize) + gvDM.Rows.Count) + " of " + Asn.Rows.Count;

        }

        protected void gvDCM_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDCM.PageIndex = e.NewPageIndex;
            gvDCM.DataSource = Asn;
            gvDCM.DataBind();
            lblRowCount.Text = (((gvDCM.PageIndex) * gvDCM.PageSize) + 1) + " - " + (((gvDCM.PageIndex) * gvDCM.PageSize) + gvDCM.Rows.Count) + " of " + Asn.Rows.Count;

        }
    }
}