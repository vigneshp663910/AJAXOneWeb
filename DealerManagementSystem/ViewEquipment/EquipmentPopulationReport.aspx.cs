using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewEquipment
{
    public partial class EquipmentPopulationReport : System.Web.UI.Page
    {
        public DataTable Equipment
        {
            get
            {
                if (Session["EquipmentPopulationReport"] == null)
                {
                    Session["EquipmentPopulationReport"] = new DataTable();
                }
                return (DataTable)Session["EquipmentPopulationReport"];
            }
            set
            {
                Session["EquipmentPopulationReport"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["PageName"] = "Equipment Population Report";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {

                //    new BDMS_Division().GetDivisionForSerchGroped(ddlDivision);
                new BDMS_Address().GetState(ddlState, null,null,null,null);
                //  new BDMS_Address().Getr(ddlState, null, "");

                txtWarrantyStart.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtWarrantyEnd.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID, new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0].DID.ToString()));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }

        void fillEquipmentPopulationReport()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);

                int? RegionID = null, DivisionID = null;
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? WarrantyStart = string.IsNullOrEmpty(txtWarrantyStart.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtWarrantyStart.Text.Trim());
                DateTime? WarrantyEnd = string.IsNullOrEmpty(txtWarrantyEnd.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtWarrantyEnd.Text.Trim());
                //   RegionID = ddlRegion.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
                int? StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
                //int? DivisionID = ddlDivision.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDivision.SelectedValue);

                //string Division = "";
                //if (ddlDivision.SelectedValue != "0")
                //{
                //    Division = ddlDivision.SelectedValue;
                //}
                Equipment = new BDMS_Equipment().GetEquipmentPopulationReport(DealerID, txtEquipment.Text.Trim(), txtCustomer.Text.Trim(), WarrantyStart, WarrantyEnd, StateID, RegionID, DivisionID).Tables[0];

                if (ddlDealerCode.SelectedValue == "0")
                {
                    List<string> DealerIDs = new List<string>();

                    foreach (PDealer ID in PSession.User.Dealer)
                    {
                        DealerIDs.Add(ID.UserName);
                    }
                    for (int i = 0; i < Equipment.Rows.Count; i++)
                    {
                        if (!DealerIDs.Contains(Convert.ToString(Equipment.Rows[i]["IC Dealer Code"])))
                        {
                            Equipment.Rows[i].Delete();
                        }
                    }
                    Equipment.AcceptChanges();
                }

                gvEquipment.PageIndex = 0;
                gvEquipment.DataSource = Equipment;
                gvEquipment.DataBind();
                if (Equipment.Rows.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_WarrantyClaim", "fillClaim", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageIndex > 0)
            {
                gvEquipment.DataSource = Equipment;
                gvEquipment.PageIndex = gvEquipment.PageIndex - 1;

                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvEquipment.PageCount > gvEquipment.PageIndex)
            {
                gvEquipment.DataSource = Equipment;
                gvEquipment.PageIndex = gvEquipment.PageIndex + 1;
                gvEquipment.DataBind();
                lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            new BXcel().ExporttoExcel(Equipment, "Equipment Population Report");
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEquipment.DataSource = Equipment;
            gvEquipment.PageIndex = e.NewPageIndex;
            gvEquipment.DataBind();
            lblRowCount.Text = (((gvEquipment.PageIndex) * gvEquipment.PageSize) + 1) + " - " + (((gvEquipment.PageIndex) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + Equipment.Rows.Count;
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            fillEquipmentPopulationReport();
        }
    }
}