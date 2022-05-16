using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketManage : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {            
            Session["previousUrl"] = "DMS_ICTicketManage.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicket> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketManage"] == null)
                {
                    Session["DMS_ICTicketManage"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["DMS_ICTicketManage"];
            }
            set
            {
                Session["DMS_ICTicketManage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('<a style= \"color:red\">New Technician Assigned!</a>')", true);
            // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('New Technician Assigned');", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Service » IC Ticket » IC Ticket Manage');</script>");
            lblMessage.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                fillStatus();
                new BDMS_Division().GetDivisionForSerchGroped(ddlDivision);
                FillGetServiceType();

                if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
                {
                    long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                    PDMS_ICTicket DMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);

                    txtICTicketNumber.Text = DMS_ICTicket.ICTicketNumber;
                    txtICLoginDateFrom.Text = "";
                    txtICLoginDateTo.Text = "";
                    fillICTicket();
                    RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[0].FindControl("rbICTicketID");
                    rbICTicketID.Checked = true;
                }
                else
                {
                    txtICLoginDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtICLoginDateTo.Text = DateTime.Now.ToShortDateString();
                }
                //if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                //{
                //    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                //    ddlDealerCode.Enabled = false;
                //}
                //else
                //{
                //    ddlDealerCode.Enabled = true;
                //    fillDealer();
                //}
                fillDealer();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicket();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }

        void fillICTicket()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                      int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?) null: Convert.ToInt32( ddlDealerCode.SelectedValue);
              //  string DealerCode = ddlDealerCode.SelectedValue == "0" ? "" : ddlDealerCode.SelectedValue;
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());
                int? StatusID = ddlStatus.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlStatus.SelectedValue);
                int? ServiceTypeID = ddlServiceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceType.SelectedValue);
                int? TechnicianID = null;
                List<PDMS_ICTicket> SOIs = null;
                if (PSession.User.IsTechnician)
                {
                    TechnicianID = PSession.User.UserID;
                }
                string Division = "";
                if (ddlDivision.SelectedValue != "0")
                {
                    Division = ddlDivision.SelectedValue;
                }

                SOIs = new BDMS_ICTicket().GetICTicketManage(DealerID, txtCustomerCode.Text.Trim(), txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, StatusID, TechnicianID, ServiceTypeID, Division);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs
                                 join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
                                 select new
                                 {
                                     S
                                 }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }
                ICTicket = SOIs;

                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
                if (SOIs.Count == 0)
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
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Requested Date");
            dt.Columns.Add("Model");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Service Priority");
            dt.Columns.Add("Service Status");
            dt.Columns.Add("Margin");

            foreach (PDMS_ICTicket IC in ICTicket)
            {
                dt.Rows.Add(
                    IC.ICTicketNumber
                    , IC.ICTicketDate.ToShortDateString()
                    , IC.Dealer.DealerCode
                    , IC.Dealer.DealerName
                    , IC.Customer.CustomerCode
                    , IC.Customer.CustomerName
                    , IC.RequestedDate == null ? "" : ((DateTime)IC.RequestedDate).ToShortDateString()
                    , IC.Equipment.EquipmentModel.Model
                    , IC.ServiceType == null ? "" : IC.ServiceType.ServiceType
                    , IC.ServicePriority == null ? "" : IC.ServicePriority.ServicePriority
                    , IC.ServiceStatus.ServiceStatus
                    , IC.IsMarginWarranty
                    );
            }



            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicket;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
        }
        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbReqDecline = (LinkButton)e.Row.FindControl("lbReqDecline");
                    Label lblServiceStatusID = (Label)e.Row.FindControl("lblServiceStatusID");
                    lbReqDecline.Visible = false;
                    if (Convert.ToInt32(lblServiceStatusID.Text) == (short)DMS_ServiceStatus.Open)
                    {
                        lbReqDecline.Visible = true;
                    }

                    string supplierPOID = Convert.ToString(gvICTickets.DataKeys[e.Row.RowIndex].Value);

                    GridView supplierPOLinesGrid = (GridView)e.Row.FindControl("gvICTicketItems");

                    Label lblPscID = (Label)e.Row.FindControl("lblPscID");
                    GridView gvFileAttached = (GridView)e.Row.FindControl("gvFileAttached");
                    gvFileAttached.DataSource = new BDMS_WarrantyClaim().GetAttachment("'" + lblPscID.Text.Trim() + "'");
                    gvFileAttached.DataBind();


                    List<PDMS_WarrantyInvoiceItem> supplierPurchaseOrderLines = new List<PDMS_WarrantyInvoiceItem>();
                    //   supplierPurchaseOrderLines = SDMS_WarrantyClaimHeader.Find(s => s.ICTicketNumber == supplierPOID).;

                    supplierPOLinesGrid.DataSource = supplierPurchaseOrderLines;
                    supplierPOLinesGrid.DataBind();

                    if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                    {
                        Label lblAnnexureNumber = (Label)e.Row.FindControl("lblAnnexureNumber");
                        Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                        if ((string.IsNullOrEmpty(lblAnnexureNumber.Text)) && (lblStatus.Text != "CANCELED"))
                        {
                            Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                            btnCancel.Visible = true;
                        }
                    }

                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

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

        void fillStatus()
        {
            ddlStatus.DataTextField = "ServiceStatus";
            ddlStatus.DataValueField = "ServiceStatusID";
            ddlStatus.DataSource = new BDMS_Service().GetServiceStatus(null, null);
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        void FillGetServiceType()
        {
            ddlServiceType.DataTextField = "ServiceType";
            ddlServiceType.DataValueField = "ServiceTypeID";
            ddlServiceType.DataSource = new BDMS_Service().GetServiceType(null, null, null);
            ddlServiceType.DataBind();
            ddlServiceType.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblInvoiceNumber = (Label)gvICTickets.Rows[gvRow.RowIndex].FindControl("lblInvoiceNumber");
            if (new BDMS_WarrantyClaim().CancelWarrantyClaims(lblInvoiceNumber.Text, PSession.User.UserID))
            {
                lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is canceled";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "Claime number " + lblInvoiceNumber.Text + " is not canceled";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }

        protected void lbICTicket_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            int index = gvRow.RowIndex;
            string url = "DMS_ICTicketView.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }

        protected void btnTechnicianAssign_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvICTickets.Rows.Count; i++)
            {
                RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
                if (rbICTicketID.Checked)
                {
                    string url = "DMS_ICTicketTechnicianAssign.aspx?TicketID=" + gvICTickets.DataKeys[i].Value.ToString();
                    Response.Redirect(url);
                }
            }
        }

        protected void btnServiceConfirmation_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvICTickets.Rows.Count; i++)
            {
                RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
                if (rbICTicketID.Checked)
                {
                    string url = "DMS_ICTicketServiceConfirmation.aspx?TicketID=" + gvICTickets.DataKeys[i].Value.ToString();
                    Response.Redirect(url);
                }
            }
        }

        protected void btnNote_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvICTickets.Rows.Count; i++)
            {
                RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
                if (rbICTicketID.Checked)
                {
                    string url = "DMS_ICTicketNote.aspx?TicketID=" + gvICTickets.DataKeys[i].Value.ToString();
                    Response.Redirect(url);
                }
            }
        }

        protected void btnServiceCharge_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvICTickets.Rows.Count; i++)
            {
                RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
                if (rbICTicketID.Checked)
                {
                    string url = "DMS_ICTicketServiceCharges.aspx?TicketID=" + gvICTickets.DataKeys[i].Value.ToString();
                    Response.Redirect(url);
                }
            }
        }

        protected void btnMaterialCharge_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvICTickets.Rows.Count; i++)
            {
                RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
                if (rbICTicketID.Checked)
                {
                    string url = "ICTicketMaterialCharges.aspx?TicketID=" + gvICTickets.DataKeys[i].Value.ToString();
                    Response.Redirect(url);
                }
            }
        }

        protected void btnBackDecline_Click(object sender, EventArgs e)
        {
            divICTicketManage.Visible = true;
            divDecline.Visible = false;
        }

        protected void btnSaveDecline_Click(object sender, EventArgs e)
        {
            new BDMS_ICTicket().UpdateICTicketDecline(Convert.ToInt64(ViewState["ICTicketID"]), txtDeclineReason.Text, PSession.User.UserID);
            divICTicketManage.Visible = true;
            divDecline.Visible = false;
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvICTickets.Rows.Count; i++)
            {
                RadioButton rbICTicketID = (RadioButton)gvICTickets.Rows[i].FindControl("rbICTicketID");
                if (rbICTicketID.Checked)
                {
                    ViewState["ICTicketID"] = gvICTickets.DataKeys[i].Value.ToString();
                    divICTicketManage.Visible = false;
                    divDecline.Visible = true;
                }
            }

        }

        protected void lbEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            int index = gvRow.RowIndex;
            string url = "ICTicketProcess.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }

        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            string url = "ICTicketView.aspx?TicketID=" + gvICTickets.DataKeys[index].Value.ToString();
            Response.Redirect(url);
        }

        protected void lbReqDecline_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            ViewState["ICTicketID"] = gvICTickets.DataKeys[index].Value.ToString();
            divICTicketManage.Visible = false;
            divDecline.Visible = true;
        }

        protected void gvICTickets_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvICTickets_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbReqDecline = (LinkButton)e.Row.FindControl("lbReqDecline");
                Label lblServiceStatusID = (Label)e.Row.FindControl("lblServiceStatusID");
                lbReqDecline.Visible = false;
                if (Convert.ToInt32(lblServiceStatusID.Text) == (short)DMS_ServiceStatus.Open)
                {
                    lbReqDecline.Visible = true;
                }
                if ((Convert.ToInt32(lblServiceStatusID.Text) == (short)DMS_ServiceStatus.Declined) || (Convert.ToInt32(lblServiceStatusID.Text) == (short)DMS_ServiceStatus.ReqDeclined))
                {
                    LinkButton lbEdit = (LinkButton)e.Row.FindControl("lbEdit");
                    lbEdit.Visible = false;
                    lbReqDecline.Visible = false;
                }
            }
        }
    }
}