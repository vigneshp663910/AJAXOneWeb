using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.Dashboard
{
    public partial class WarrantyClaimDebitNoteAcknowledgePending : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            fillWarrantyInvoice();
        }
        void fillWarrantyInvoice()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = (int?)Session["SerDealerID"];
                int UserID = PSession.User.UserID;
                List<PDMS_WarrantyClaimDebitNote> SOIs = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteAcknowledge(null, DealerID, null, null, null, null, UserID);
                gvClaimInvoice.PageIndex = 0;
                gvClaimInvoice.DataSource = SOIs;
                gvClaimInvoice.DataBind();
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaimInvoiceReport", "fillWarrantyInvoice", e1);
                throw e1;
            }
        }

        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int? DealerID = (int?)Session["SerDealerID"];
            int UserID = PSession.User.UserID;
            List<PDMS_WarrantyClaimDebitNote> SOIs = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteAcknowledge(null, DealerID, null, null, null, null, UserID);
            gvClaimInvoice.PageIndex = e.NewPageIndex;
            gvClaimInvoice.DataSource = SOIs;
            gvClaimInvoice.DataBind();

        }
    }
}