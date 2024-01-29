using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewInventory
{
    public partial class PhysicalInventoryPosting : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewInventory_PhysicalInventoryPosting; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Inventory » Physical- Inventory Posting');</script>");
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
            PApiResult Result = new BInventory().GetDealerPhysicalInventoryPosting(DealerID, OfficeID, null, null, PageIndex, gvStock.PageSize);

            gvStock.DataSource = JsonConvert.DeserializeObject<List<PhysicalInventoryPosting>>(JsonConvert.SerializeObject(Result.Data));
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

        protected void btnAddLead_Click(object sender, EventArgs e)
        {
            divList.Visible = false; 
            divCreate.Visible = true;
            UC_Create.FillMaster();
        }
        protected void btnView_Click(object sender, EventArgs e)
        { 
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblLeadID = (Label)gvRow.FindControl("lblLeadID");
            ViewState["LeadID"] = lblLeadID.Text;

            divList.Visible = false;
            divView.Visible = true;
          //  UC_View.fillViewLead(Convert.ToInt64(lblLeadID.Text));
        }
        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divView.Visible = false;
            divCreate.Visible = false;
        }
    }
}