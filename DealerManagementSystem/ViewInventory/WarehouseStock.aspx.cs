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
    public partial class WarehouseStock : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewInventory_WarehouseStock; } }
        private DataTable StockTrackDetail
        {
            get
            {
                if (Session["WarehouseStockStockTrackDetail"] == null)
                {
                    Session["WarehouseStockStockTrackDetail"] = new DataTable();
                }
                return (DataTable)Session["WarehouseStockStockTrackDetail"];
            }
            set
            {
                Session["WarehouseStockStockTrackDetail"] = value;
            }
        }
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
            if (!IsPostBack)
            {
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new DDLBind(ddlDivision, new BDMS_Master().GetDivision(null, null), "DivisionDescription", "DivisionID", true, "Select");
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
            string MaterialCode = txtMaterial.Text.Trim();
            int? DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            PApiResult Result = new BInventory().GetDealerStock(DealerID, OfficeID, DivisionID, null, MaterialCode, PageIndex, gvStock.PageSize);
            List<PDealerStock> DealerStock = JsonConvert.DeserializeObject<List<PDealerStock>>(JsonConvert.SerializeObject(Result.Data));
            gvStock.DataSource = DealerStock;
            gvStock.DataBind();
            lblTotalInventoryValue.Text = DealerStock.Count == 0 ? "0" : Convert.ToString(DealerStock[0].TotalInventoryValue);

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

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            int? DealerID = null;
            int? OfficeID = null;
            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
                OfficeID = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);
            }
            string MaterialCode = txtMaterial.Text.Trim();
            int? DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);
            PApiResult Result = new BInventory().GetDealerStockExcel(DealerID, OfficeID, DivisionID, null, MaterialCode);

            //gvStock.DataSource = ;
            try
            {
                new BXcel().ExporttoExcel(JsonConvert.DeserializeObject<DataTable>(JsonConvert.SerializeObject(Result.Data)), "Dealer Stock");
            }
            catch
            {
            }
            finally
            {
            }
        }

        protected void lblLinkButton_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblDealerID = (Label)gvRow.FindControl("lblDealerID");
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            Label lblMaterialID = (Label)gvRow.FindControl("lblMaterialID"); 
            int TrackTypeID = 0;
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.ID == "lbOnOrderQty")
            {
                TrackTypeID = 1;
            }
            else if (lbActions.ID == "lbTransitQty")
            {
                TrackTypeID = 2;
            }
            else if (lbActions.ID == "lbReservedQty")
            {
                TrackTypeID = 3;
            } 
            StockTrackDetail = new BInventory().GetDealerStockTrackDetail(lblDealerID.Text, lblOfficeID.Text, lblMaterialID.Text, TrackTypeID);
            gvDetails.DataSource = StockTrackDetail;
            gvDetails.DataBind();
            MPE_LeadDetails.Show();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    new BXcel().ExporttoExcelForLeadNextFollowUpAgeingReport(StockTrackDetail, "Lead Next Follow Up Ageing", "Lead Next Follow Up Ageing");
                }
                catch
                {
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}