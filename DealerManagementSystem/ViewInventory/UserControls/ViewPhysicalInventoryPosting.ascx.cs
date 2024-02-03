using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewInventory.UserControls
{
    public partial class ViewPhysicalInventoryPosting : System.Web.UI.UserControl
    { 
        public PPhysicalInventoryPosting Inventory
        {
            get
            {
                if (ViewState["ViewPhysicalInventoryPosting"] == null)
                {
                    ViewState["ViewPhysicalInventoryPosting"] = new PPhysicalInventoryPosting();
                }
                return (PPhysicalInventoryPosting)ViewState["ViewPhysicalInventoryPosting"];
            }
            set
            {
                ViewState["ViewPhysicalInventoryPosting"] = value;
            }
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 
        }
        public void fillViewEnquiry(long PhysicalInventoryPostingID)
        {
            Inventory = new BInventory().GetDealerPhysicalInventoryPostingByID(PhysicalInventoryPostingID);
            lblDealerCode.Text = Inventory.Dealer.DealerCode;
            lblDealerName.Text = Inventory.Dealer.DealerName;
            lblOfficeName.Text = Inventory.DealerOffice.OfficeName;
            lblDocumentNumber.Text = Inventory.DocumentNumber;
            lblDocumentDate.Text = Inventory.DocumentDate.ToString("dd/MM/yyyy");
            lblPostingDate.Text = Inventory.PostingDate.ToString("dd/MM/yyyy HH:mm:ss");
            lblCreatedByContactName.Text = Inventory.CreatedBy.ContactName;
            lblInventoryPostingType.Text = Inventory.InventoryPostingType.Status;
            gvStatusHistory.DataSource = Inventory.Items;
            gvStatusHistory.DataBind();
            ActionControlMange();
        }    
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender); 
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
        }  
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        void ActionControlMange()
        {
            //lbEditEnquiry.Visible = true;
            //lbInActive.Visible = true;
            //lbReject.Visible = true;
            //lbInProgress.Visible = true;


            //if ((Enquiry.Status.StatusID == (short)PreSaleStatus.ConvertedToLead) || (Enquiry.Status.StatusID == (short)PreSaleStatus.Rejected))
            //{
            //    lbEditEnquiry.Visible = false;
            //    lbInActive.Visible = false;
            //    lbReject.Visible = false;
            //    lbInProgress.Visible = false;
            //}

            //lbInProgress.Visible = false;
        } 
        
    }
}