using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;
using System.Drawing;
using System.Data;

namespace DealerManagementSystem.ServiceView
{
    public partial class ApprovalForDeviatedICTicketRequest : System.Web.UI.Page
    {
        public DataTable ICTicketDT
        {
            get
            {
                if (Session["DMS_DeviatedICTicketRequestForApproval"] == null)
                {
                    Session["DMS_DeviatedICTicketRequestForApproval"] = new DataTable();
                }
                return (DataTable)Session["DMS_DeviatedICTicketRequestForApproval"];
            }
            set
            {
                Session["DMS_DeviatedICTicketRequestForApproval"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    txtRequestedDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                    txtRequestedDateTo.Text = DateTime.Now.ToShortDateString();

                    if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                    {
                        ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                        ddlDealerCode.Enabled = false;


                        PDealer DealerF = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                        ddlDealerF.Items.Add(new ListItem(PSession.User.ExternalReferenceID, DealerF.DID.ToString()));
                        ddlDealerF.Enabled = false;
                    }
                    else
                    {
                        ddlDealerCode.Enabled = true;
                        ddlDealerF.Enabled = true;
                        fillDealer();
                    }

                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
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
            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            if (ddlDealerCode.SelectedValue == "0")
            {
                lblMessage.Text = "Please select the Dealer";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            string DealerCode = ddlDealerCode.SelectedValue;
            List<PDMS_ICTicket> ICTicket = new BDMS_ICTicket().GetICTicketManage(DealerCode, "", txtICTicketNumber.Text.Trim(), null, null, null, null, null, null);

            //var SOIs1 = (from S in ICTicket
            //             join D in PSession.User.Dealer on S.Dealer.DealerCode equals D.UserName
            //             select new
            //             {
            //                 S
            //             }).ToList();
            //ICTicket.Clear();
            //foreach (var w in SOIs1)
            //{
            //    ICTicket.Add(w.S);
            //}

            if (ICTicket.Count == 1)
            {
                if (new BDMS_ICTicket().InsertDeviatedICTicketRequestForApproval(ICTicket[0].ICTicketID, Convert.ToInt32(ddlICTicketDeviationType.SelectedValue), PSession.User.UserID))
                {

                    lblMessage.Text = "IC Ticket sent for approval";
                    lblMessage.ForeColor = Color.Green;
                    txtICTicketNumber.Text = "";
                }
                else
                {
                    lblMessage.Text = "IC Ticket is not sent for approval";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            else
            {
                lblMessage.Text = "Please enter the correct IC Ticket Number";
                lblMessage.ForeColor = Color.Red;
            }

        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("Select", "0"));

            ddlDealerF.DataTextField = "CodeWithName";
            ddlDealerF.DataValueField = "DID";
            ddlDealerF.DataSource = PSession.User.Dealer;
            ddlDealerF.DataBind();
            ddlDealerF.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int? DealerID = ddlDealerF.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerF.SelectedValue);
            DateTime? ICTicketDateF = string.IsNullOrEmpty(txtRequestedDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateFrom.Text.Trim());
            DateTime? ICTicketDateT = string.IsNullOrEmpty(txtRequestedDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRequestedDateTo.Text.Trim());

            ICTicketDT = new BDMS_ICTicket().GetDeviatedICTicketReport(DealerID, txtICTicketNumberF.Text.Trim(), null, ICTicketDateF, ICTicketDateT, PSession.User.UserID);

            gvICTickets.DataSource = ICTicketDT;
            gvICTickets.DataBind();
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicketDT;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicketDT.Rows.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicketDT;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicketDT.Rows.Count;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets.DataSource = ICTicketDT;
            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicketDT.Rows.Count;
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(ICTicketDT, "Deviated ICTicket Request");
        }
    }
}