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
        public PSaleOrderReturn SoReturn
        {
            get
            {
                if (ViewState["PSaleOrderReturn"] == null)
                {
                    ViewState["PSaleOrderReturn"] = new PSaleOrderReturn();
                }
                return (PSaleOrderReturn)ViewState["PSaleOrderReturn"];
            }
            set
            {
                ViewState["PSaleOrderReturn"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageSoReturn.Text = "";
        }
        public void fillViewSoReturn(long SaleOrderReturnID)
        {
            SoReturn = new BSalesOrderReturn().GetSaleOrderReturnByID(SaleOrderReturnID);

            lblSaleOrderReturnNumber.Text = SoReturn.SaleOrderReturnNumber;
            lblSaleOrderReturnDate.Text = SoReturn.SaleOrderReturnDate.ToString();
            lblSaleOrderReturnStatus.Text = SoReturn.SaleOrderReturnStatus.Status;
            
            gvSoReturnItem.DataSource = SoReturn.SaleOrderReturnItems;
            gvSoReturnItem.DataBind();

            ActionControlMange();
        }
        void ActionControlMange()
        {
            lbSoReturnCancel.Visible = true;
            lbSoReturnDeliveryCreate.Visible = true;
            if (SoReturn.SaleOrderReturnStatus.StatusID != 1)
            {
                lbSoReturnCancel.Visible = false;
                lbSoReturnDeliveryCreate.Visible = false;
            }
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "SO Return Cancel")
            {
                CancelSalesReturnOrder();
            }
        }
        protected void CancelSalesReturnOrder()
        {
            long SaleOrderReturnID = SoReturn.SaleOrderReturnID;
            PSaleOrderReturn SaleOrderReturn = new PSaleOrderReturn();
            SaleOrderReturn.SaleOrderReturnID = SaleOrderReturnID;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrderReturn/CancelSaleOrderReturn?SaleOrderReturnID=" + SoReturn.SaleOrderReturnID));
            
            if (Result.Status == PApplication.Failure)
            {
                lblMessageSoReturn.Text = Result.Message;
                lblMessageSoReturn.ForeColor = Color.Red;
                return;
            }
            lblMessageSoReturn.Text = Result.Message;
            lblMessageSoReturn.Visible = true;
            lblMessageSoReturn.ForeColor = Color.Green;
            fillViewSoReturn(SoReturn.SaleOrderReturnID);
        }
        protected void lnkBtnDeleteSalesReturnItem_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            LinkButton lnkBtnDelete = (LinkButton)gvRow.FindControl("lnkBtnDeleteSalesReturnItem");

            //lblMessageSoReturn.Visible = true;

            //long SaleOrderReturnID = SoReturn.SaleOrderReturnID;
            //PSaleOrderReturn SaleOrderReturn = new PSaleOrderReturn();
            //SaleOrderReturn.SaleOrderReturnID = SaleOrderReturnID;
            //PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet("SaleOrderReturn/CancelSaleOrderReturn?SaleOrderReturnID=" + SoReturn.SaleOrderReturnID));

            //if (Result.Status == PApplication.Failure)
            //{
            //    lblMessageSoReturn.Text = Result.Message;
            //    lblMessageSoReturn.ForeColor = Color.Red;
            //    return;
            //}
            //lblMessageSoReturn.Text = Result.Message;
            //lblMessageSoReturn.Visible = true;
            //lblMessageSoReturn.ForeColor = Color.Green;
            //fillViewSoReturn(SoReturn.SaleOrderReturnID);
        }
    }
}