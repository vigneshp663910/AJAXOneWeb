using Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class EnquiryIndiamart : System.Web.UI.Page
    {
        public DataTable Enquiry
        {
            get
            {
                if (Session["EnquiryIndiamart"] == null)
                {
                    Session["EnquiryIndiamart"] = new DataTable();
                }
                return (DataTable)Session["EnquiryIndiamart"];
            }
            set
            {
                Session["EnquiryIndiamart"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Enquiry Indiamart');</script>");
            if (!IsPostBack)
            {
                txtDateFrom.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                txtDateTo.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void btnEnquiryIndiamart_Click(object sender, EventArgs e)
        {
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            Enquiry = new BEnquiryIndiamart().GetEnquiryIndiamart(DateFrom, DateTo);
            gvEnquiry.DataSource = Enquiry;
            gvEnquiry.DataBind();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(Enquiry, "Enquiry Indiamart");
        }

        protected void gvEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEnquiry.PageIndex = e.NewPageIndex;
            gvEnquiry.DataSource = Enquiry;
            gvEnquiry.DataBind();
        }

        protected void ibtnEnquiryIMArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageIndex > 0)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex - 1;
                EnquiryIndiamartBind(gvEnquiry, lblRowCount, Enquiry);
            }
        }
        protected void ibtnEnquiryIMArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEnquiry.PageCount > gvEnquiry.PageIndex)
            {
                gvEnquiry.PageIndex = gvEnquiry.PageIndex + 1;
                EnquiryIndiamartBind(gvEnquiry, lblRowCount, Enquiry);
            }
        }


        void EnquiryIndiamartBind(GridView gv, Label lbl,DataTable Enquiry)
        {
            gv.DataSource = Enquiry;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + Enquiry.Rows.Count;
        }
    }
}