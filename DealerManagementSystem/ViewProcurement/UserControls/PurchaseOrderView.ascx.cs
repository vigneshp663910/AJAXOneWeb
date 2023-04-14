using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewProcurement.UserControls
{
    public partial class PurchaseOrderView : System.Web.UI.UserControl
    {
        public PPurchaseOrder PurchaseOrder
        {
            get
            {
                if (ViewState["PPurchaseOrder"] == null)
                {
                    Session["PPurchaseOrder"] = new PPurchaseOrder();
                }
                return (PPurchaseOrder)ViewState["PPurchaseOrder"];
            }
            set
            {
                ViewState["PPurchaseOrder"] = value;
            }
        }
    

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
             
        }
        public void fillViewEnquiry(long EnquiryID)
        {
            // ViewState["EnquiryID"] = EnquiryID;
           // Enquiry = new BEnquiry().GetEnquiryByID(EnquiryID);
            

            

            fillEnquiryStatusHistory();
            ActionControlMange();
        }
        
        protected void lbActions_Click(object sender, EventArgs e)
        {
            //LinkButton lbActions = ((LinkButton)sender);
            //if (lbActions.Text == "Edit Enquiry")
            //{
            //    MPE_Enquiry.Show();
            //    UC_AddEnquiry.FillMaster();
            //    UC_AddEnquiry.Write(Enquiry);
            //}
            //if (lbActions.Text == "Convert To Lead")
            //{
            //    MPE_CustomerSelect.Show();
            //    gvCustomer.DataSource = new BDMS_Customer().GetCustomerForEnquiryToLead(Enquiry.CustomerName, Enquiry.Mobile, Enquiry.State.StateID);
            //    gvCustomer.DataBind();
            //}
            //if (lbActions.Text == "Reject")
            //{
            //    MPE_EnquiryReject.Show();
            //    new DDLBind(ddlEnquiryRejectRemarks, new BEnquiry().GetEnquiryRemark(null, null, null, null, null, true), "Remark", "EnquiryRemarkID");
            //}
            //else if (lbActions.Text == "InProgress")
            //{
            //    lblInProgressQueryID.Text = Enquiry.EnquiryNumber;
            //    new DDLBind(ddlInprogressRemarks, new BEnquiry().GetEnquiryRemark(null, null, null, null, true, null), "Remark", "EnquiryRemarkID");
            //    txtInprogressEnquiryReason.Text = string.Empty;
            //    MPE_InprogressEnquiry.Show();
            //    lblInprogressEnquiryMessage.Text = string.Empty;
            //    lblInprogressEnquiryMessage.Visible = false;
            //}
        }
 
       
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }

        void ActionControlMange()
        {
             
        }

        protected void btnInprogressEnquiry_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                
            }
        }

        public void fillEnquiryStatusHistory()
        {
            
        }
    }
}