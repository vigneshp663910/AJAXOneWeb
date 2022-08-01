using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDashboard.UserControls
{
    public partial class WarrantyClaimDebitNoteAcknowledgePending : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblOpen.Text = "0";
            int? DealerID = (int?)Session["SerDealerID"];
            int UserID = PSession.User.UserID;
            List<PDMS_WarrantyClaimDebitNote> SOIs = new BDMS_WarrantyClaimDebitNote().getWarrantyClaimDebitNoteAcknowledge(null, DealerID, null, null, null, null, UserID);
            lblOpen.Text = SOIs.Count.ToString();

        }
        
        //protected void lbActions_Click(object sender, EventArgs e)
        //{
        //    Session["leadStatusID"] = 1;
        //    Session["leadDateFrom"] = (DateTime?)Session["SerDateFrom"];
        //    Response.Redirect("ViewPreSale/lead.aspx");
        //}
    }
}