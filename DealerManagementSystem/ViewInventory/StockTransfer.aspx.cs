using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace DealerManagementSystem.ViewInventory
{
    public partial class StockTransfer : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewInventory_StockTransfer; } }
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Inventory » Warehouse Stock');</script>");
            lblMessage.Text = "";
            lblMessageStockMove.Text = "";
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            PageCount = 0;
            PageIndex = 1;
            FilStock();
        }
        void FilStock()
        {
            int? DealerID = null;
            int? OfficeID = null;
            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                OfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            }
            // int? DivisionID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            // int? ModelID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            string MaterialCode = txtMaterial.Text.Trim();
            PApiResult Result = new BInventory().GetDealerStock(DealerID, OfficeID, null, null, MaterialCode, PageIndex, gvStock.PageSize);

            gvStock.DataSource = JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data));
            gvStock.DataBind();

            if (Result.RowCount == 0)
            {
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
            else
            {
                PageCount = (Result.RowCount + gvStock.PageSize - 1) / gvStock.PageSize;
                lblRowCount.Visible = true;
                ibtnArrowLeft.Visible = true;
                ibtnArrowRight.Visible = true;
                lblRowCount.Text = (((PageIndex - 1) * gvStock.PageSize) + 1) + " - " + (((PageIndex - 1) * gvStock.PageSize) + gvStock.Rows.Count) + " of " + Result.RowCount;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                FilStock();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                FilStock();
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDealerOffice, new BDMS_Dealer().GetDealerOffice(Convert.ToInt32(ddlDealer.SelectedValue), null, null), "OfficeName", "OfficeID");
        }

        protected void btnTransit_Click(object sender, EventArgs e)
        {
            new DDLBind(ddlStockMovementType, new BInventory().GetStockMovementType(null, "True"), "StockMovementType", "StockMovementTypeID");
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblUnrestrictedQtyGV = (Label)gvRow.FindControl("lblUnrestrictedQty");
            Label lblRestrictedQtyGV = (Label)gvRow.FindControl("lblRestrictedQty");
            Label lblBlockedQtyGV = (Label)gvRow.FindControl("lblBlockedQty");

            Label lblDealerIDGV = (Label)gvRow.FindControl("lblDealerID");
            Label lblOfficeIDGV = (Label)gvRow.FindControl("lblOfficeID");
            Label lblMaterialIDGV = (Label)gvRow.FindControl("lblMaterialID");

            lblUnrestrictedQty.Text = lblUnrestrictedQtyGV.Text;
            lblRestrictedQty.Text = lblRestrictedQtyGV.Text;
            lblBlockedQty.Text = lblBlockedQtyGV.Text;

            lblDealerID.Text = lblDealerIDGV.Text;
            lblOfficeID.Text = lblOfficeIDGV.Text;
            lblMaterialID.Text = lblMaterialIDGV.Text;
            MPE_StockTranfer.Show();
            txtQuantity.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            MPE_StockTranfer.Show();
            lblMessageStockMove.ForeColor = Color.Red;
            if (!decimal.TryParse(txtQuantity.Text.Trim(), out var value))
            {
                lblMessageStockMove.Text = "Please check the Quantity";
                return;
            }
            if (ddlStockMovementType.SelectedValue == "0")
            {
                lblMessageStockMove.Text = "Please check the Stock Movement Type";
                return;
            }

            int MaterialTransitID = Convert.ToInt32(ddlStockMovementType.SelectedValue);            
            decimal Qty = Convert.ToDecimal(txtQuantity.Text.Trim());
            
            decimal UnrestrictedQty = Convert.ToDecimal(lblUnrestrictedQty.Text);
            decimal RestrictedQty = Convert.ToDecimal(lblRestrictedQty.Text);
            decimal BlockedQty = Convert.ToDecimal(lblBlockedQty.Text);
            if ((MaterialTransitID == (short)StockMovementType.UnrestrictedToRestricted) || (MaterialTransitID == (short)StockMovementType.UnrestrictedToBlocked))
            {
                if (Qty > UnrestrictedQty)
                {
                    lblMessageStockMove.Text = "Please check the Unrestricted Qty";
                    return;
                }
            }
            else if ((MaterialTransitID == (short)StockMovementType.RestrictedToUnrestricted) || (MaterialTransitID == (short)StockMovementType.RestrictedToBlocked))
            {
                if (Qty > RestrictedQty)
                {
                    lblMessageStockMove.Text = "Please check the Restricted Qty";
                    return;
                }
            }
            else if ((MaterialTransitID == (short)StockMovementType.BlockedToUnrestricted) || (MaterialTransitID == (short)StockMovementType.BlockedToRestricted))
            {
                if (Qty > BlockedQty)
                {
                    lblMessageStockMove.Text = "Please check the Blocked Qty";
                    return;
                }
            }
           
            
            string endPoint = "Inventory/InsertMaterialTransfer?DealerID=" + lblDealerID.Text + "&OfficeID=" + lblOfficeID.Text
                + "&MaterialID=" + lblMaterialID.Text + "&Quantity=" + txtQuantity.Text.Trim() + "&StockMovementTypeID=" + ddlStockMovementType.SelectedValue;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

            if (Result.Status == PApplication.Failure)
            {
                lblMessageStockMove.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
            lblDealerID.Text = "0";
            lblOfficeID.Text = "0";
            lblMaterialID.Text = "0";
            FilStock();
            MPE_StockTranfer.Hide();
        }
    }
}