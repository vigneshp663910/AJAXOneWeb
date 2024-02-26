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
        public long InventoryID
        {
            get
            {
                if (ViewState["PhysicalInventoryPostingID"] == null)
                {
                    ViewState["PhysicalInventoryPostingID"] = 0;
                }
                return (long)ViewState["PhysicalInventoryPostingID"];
            }
            set
            {
                ViewState["PhysicalInventoryPostingID"] = value;
            }
        }
        public long StatusID
        {
            get
            {
                if (ViewState["StatusID"] == null)
                {
                    ViewState["StatusID"] = 0;
                }
                return (long)ViewState["StatusID"];
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 
        }
        public void fillViewEnquiry(long PhysicalInventoryPostingID)
        {
            InventoryID = PhysicalInventoryPostingID;
            
            PPhysicalInventoryPosting Inventory = new BInventory().GetDealerPhysicalInventoryPostingByID(PhysicalInventoryPostingID);
            StatusID = Inventory.Status.StatusID;
            lblDealerCode.Text = Inventory.Dealer.DealerCode;
            lblDealerName.Text = Inventory.Dealer.DealerName;
            lblOfficeName.Text = Inventory.DealerOffice.OfficeName;
            lblDocumentNumber.Text = Inventory.DocumentNumber;
            lblDocumentDate.Text = Inventory.DocumentDate.ToString("dd/MM/yyyy");
            lblPostingDate.Text = Inventory.PostingDate == null ? "" : ((DateTime)Inventory.PostingDate).ToString("dd/MM/yyyy HH:mm:ss");
            lblPostingBy.Text = Inventory.PostingBy.ContactName;
            lblCreatedBy.Text = Inventory.CreatedBy.ContactName;
            lblCreatedOn.Text = Inventory.CreatedOn.ToString("dd/MM/yyyy");
            lblStatus.Text = Inventory.Status.Status;
            lblReasonOfPosting.Text = Inventory.ReasonOfPosting;
            lblInventoryPostingType.Text = Inventory.InventoryPostingType.Status;
            gvStatusHistory.DataSource = Inventory.Items;
            gvStatusHistory.DataBind();
            ActionControlMange();
        }    
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbtnPost")
            {
                PApiResult Results = new BInventory().UpdateDealerPhysicalInventoryPosting(InventoryID);

                lblMessage.Text = Results.Message;
                if (Results.Status == PApplication.Failure)
                {
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                lblMessage.ForeColor = Color.Green;
                fillViewEnquiry(InventoryID);
            } 
        }  
        void ShowMessage(PApiResult Results)
        {
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
        }
        void ActionControlMange()
        {
            lbtnPost.Visible = true;
            if ((StatusID == (short)AjaxOneStatus.PostingInventoryStatus_Posted))
            {
                lbtnPost.Visible = false;
            }
        }
    }
}