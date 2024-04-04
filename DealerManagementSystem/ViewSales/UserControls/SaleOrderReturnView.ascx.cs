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

namespace DealerManagementSystem.ViewSales.UserControls
{
    public partial class SaleOrderReturnView : System.Web.UI.UserControl
    { 
        public int StatusID
        {
            get
            {
                if (ViewState["StatusID"] == null)
                {
                    ViewState["StatusID"] = 0;
                }
                return (int)ViewState["StatusID"];
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        public long SaleOrderReturnID
        {
            get
            {
                if (ViewState["SaleOrderReturnID"] == null)
                {
                    ViewState["SaleOrderReturnID"] = 0;
                }
                return (long)ViewState["SaleOrderReturnID"];
            }
            set
            {
                ViewState["SaleOrderReturnID"] = value;
            }
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageSoReturn.Text = "";
            if (Session["SaleOrderReturnID"] != null)
            {
                lblMessageSoReturn.Text = "Sale Order Return Created.";
                lblMessageSoReturn.ForeColor = Color.Green;
                Session["SaleOrderReturnID"] = null;
            }
        }
        public void fillViewSoReturn(long SaleOrderReturnID_)
        {
            PSaleOrderReturn SoReturn = new BSalesOrderReturn().GetSaleOrderReturnByID(SaleOrderReturnID_);
            SaleOrderReturnID = SaleOrderReturnID_;
            StatusID = SoReturn.ReturnStatus.StatusID;
            lblSaleOrderReturnNumber.Text = SoReturn.SaleOrderReturnNumber;
            lblSaleOrderReturnDate.Text = SoReturn.SaleOrderReturnDate.ToShortDateString();
            lblCreditNoteNumber.Text = SoReturn.CreditNoteNumber;
            lblCreditNoteDate.Text = SoReturn.CreditNoteDate == null ? "" : ((DateTime)SoReturn.CreditNoteDate).ToShortDateString();

            lblSODealer.Text = SoReturn.SaleOrderDelivery.SaleOrder.Dealer.DealerCode + " " + SoReturn.SaleOrderDelivery.SaleOrder.Dealer.DealerName;
            //lblDealerOffice.Text = SoReturn.SaleOrderDelivery.SaleOrder.Dealer.DealerCode + " " + SoReturn.SaleOrderDelivery.SaleOrder.Dealer.DealerName;
            lblDealerOffice.Text = SoReturn.SaleOrderDelivery.SaleOrder.Dealer.DealerOffice.OfficeName;
            lblContactPerson.Text = SoReturn.SaleOrderDelivery.SaleOrder.Customer.ContactPerson;
            lblCustomer.Text = SoReturn.SaleOrderDelivery.SaleOrder.Customer.CustomerCode + " " + SoReturn.SaleOrderDelivery.SaleOrder.Customer.CustomerName;
            lblContactPersonNumber.Text = SoReturn.SaleOrderDelivery.SaleOrder.Customer.Mobile;
            lblSaleOrderReturnStatus.Text = SoReturn.ReturnStatus.Status;
            lblDivision.Text = SoReturn.SaleOrderDelivery.SaleOrder.Division.DivisionCode;
            lblApprovedBy.Text = SoReturn.ApprovedBy == null ? "" : SoReturn.ApprovedBy.ContactName.ToString();
            lblApprovedDate.Text = SoReturn.ApprovedOn == null ? "" : ((DateTime)SoReturn.ApprovedOn).ToShortDateString();
            lblCancelledBy.Text = SoReturn.CancelledBy == null ? "" : SoReturn.CancelledBy.ContactName.ToString();
            lblCancelledOn.Text = SoReturn.CancelledOn == null ? "" : ((DateTime)SoReturn.CancelledOn).ToShortDateString();
            gvSoReturnItem.DataSource = SoReturn.SaleOrderReturnItems;
            gvSoReturnItem.DataBind(); 
            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbCancel.Visible = true;
            lbApprove.Visible = true;
            lbCreateCreditNote.Visible = true;
            lbPreviewCreditNote.Visible = true;
            lbDowloadCreditNote.Visible = true;
            if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_ApprovalPending)
            {
                lbCreateCreditNote.Visible = false;
                lbPreviewCreditNote.Visible = false;
                lbDowloadCreditNote.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_Cancelled)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbPreviewCreditNote.Visible = false;
                lbDowloadCreditNote.Visible = false;
                lbCreateCreditNote.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_Approved)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbPreviewCreditNote.Visible = false;
                lbDowloadCreditNote.Visible = false;
            }
            else if (StatusID == (short)AjaxOneStatus.SaleOrderReturn_CreditNote)
            {
                lbCancel.Visible = false;
                lbApprove.Visible = false;
                lbCreateCreditNote.Visible = false;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbCancel")
            {
                UpdateSaleOrderReturnStatus((short)AjaxOneStatus.SaleOrderReturn_Cancelled);
            }
            else if (lbActions.ID == "lbApprove")
            {
                UpdateSaleOrderReturnStatus((short)AjaxOneStatus.SaleOrderReturn_Approved);
            }
            else if (lbActions.ID == "lbCreateCreditNote")
            {
                UpdateSaleOrderReturnStatus((short)AjaxOneStatus.SaleOrderReturn_CreditNote);
            }
        }
        protected void UpdateSaleOrderReturnStatus(int StatusID)
        { 
            PSaleOrderReturn SaleOrderReturn = new PSaleOrderReturn();
            SaleOrderReturn.SaleOrderReturnID = SaleOrderReturnID;
            PApiResult Result = new BSalesOrderReturn().UpdateSaleOrderReturnStatus(SaleOrderReturnID, StatusID);            
            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturn.Text = Result.Message;
                lblMessageSoReturn.ForeColor = Color.Red;
                return;
            }
            lblMessageSoReturn.Text = Result.Message;
            lblMessageSoReturn.ForeColor = Color.Green;
            fillViewSoReturn(SaleOrderReturnID);
        } 
    }
}