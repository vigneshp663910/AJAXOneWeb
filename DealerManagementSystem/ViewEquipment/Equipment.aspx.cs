using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewEquipment
{
    public partial class Equipment : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewEquipment_Equipment; } }
        int? DealerID = null;
        string EquipmentSerialNo = null;
        string Customer = null;
        DateTime? WarrantyStart = null;
        DateTime? WarrantyEnd = null;
        int? RegionID = null;
        int? StateID = null;
        int? DivisionID = null;




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
        //public List<PDMS_Equipment> EquipmentHeader
        //{
        //    get
        //    {
        //        if (Session["EquipmentHeader"] == null)
        //        {
        //            Session["EquipmentHeader"] = new DataTable();
        //        }
        //        return (List<PDMS_Equipment>)Session["EquipmentHeader"];
        //    }
        //    set
        //    {
        //        Session["EquipmentHeader"] = value;
        //    }
        //}
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Equipment');</script>");
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
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
                PageCount = 0;
                PageIndex = 1;
                new BDMS_Address().GetStateDDL(ddlState, null, null, null, null);
                //txtWarrantyStart.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                //txtWarrantyEnd.Text = DateTime.Now.ToShortDateString(); 
                fillDealer();
                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;
            }
        }

        void GetEquipmentHeader()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                Search();
                int RowCount = 0;
                List<PDMS_Equipment> EquipmentHeader = new BDMS_Equipment().GetEquipmentHeader(DealerID, EquipmentSerialNo, Customer, WarrantyStart, WarrantyEnd, StateID, RegionID, DivisionID, PSession.User.UserID, PageIndex, gvEquipment.PageSize, out RowCount);


                gvEquipment.PageIndex = 0;
                gvEquipment.DataSource = EquipmentHeader;
                gvEquipment.DataBind();
                if (RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (RowCount + gvEquipment.PageSize - 1) / gvEquipment.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvEquipment.PageSize) + 1) + " - " + (((PageIndex - 1) * gvEquipment.PageSize) + gvEquipment.Rows.Count) + " of " + RowCount;
                }

                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("Equipment", "GetEquipmentHeader", e1);
                throw e1;
            }
        }

        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                GetEquipmentHeader();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                GetEquipmentHeader();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EngineSerialNo");
            dt.Columns.Add("EquipmentSerialNo");
            dt.Columns.Add("Model");
            dt.Columns.Add("ModelDescription");
            dt.Columns.Add("CustomerCode");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("District");
            dt.Columns.Add("State");
            dt.Columns.Add("DispatchedOn");
            dt.Columns.Add("WarrantyExpiryDate");
            //dt.Columns.Add("Client");
            Search();
            int Index = 0;
            int Rowcount = 1000;
            int CRowcount = Rowcount;
            while (Rowcount == CRowcount)
            {
                Index = Index + 1;
                int RRowCount = 0;
                List<PDMS_Equipment> EquipmentHeader = new BDMS_Equipment().GetEquipmentHeader(DealerID, EquipmentSerialNo, Customer, WarrantyStart, WarrantyEnd, StateID, RegionID, DivisionID, PSession.User.UserID, Index, Rowcount, out RRowCount);
                CRowcount = EquipmentHeader.Count;
                foreach (PDMS_Equipment M in EquipmentHeader)
                {
                    dt.Rows.Add(
                                                 M.EngineSerialNo
                                               , M.EquipmentSerialNo
                                               , M.EquipmentModel.Model
                                               , M.EquipmentModel.ModelDescription
                                               , M.Customer.CustomerCode
                                               , M.Customer.CustomerName
                                               , M.Customer.District.District
                                               , M.Customer.State.State
                                               , M.DispatchedOn
                                               , M.WarrantyExpiryDate
                                               //, M.EquipmentClient.Client
                                                );
                }
            }
            new BXcel().ExporttoExcel(dt, "Equipment");
        }

        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithDisplayName";
            ddlDealerCode.DataValueField = "DID";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();
            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetEquipmentHeader();
        }

        protected void btnViewEquipment_Click(object sender, EventArgs e)
        {
            divEquipmentView.Visible = true;
            divList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblEquipmentHeaderID = (Label)gvRow.FindControl("lblEquipmentHeaderID");

            UC_EquipmentView.fillEquipment(Convert.ToInt64(lblEquipmentHeaderID.Text));
        }

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divEquipmentView.Visible = false;
            divList.Visible = true;
        }

        void Search()
        {
            EquipmentSerialNo = txtEquipment.Text.Trim();
            Customer = txtCustomer.Text.Trim();
            DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
            WarrantyStart = string.IsNullOrEmpty(txtWarrantyStart.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtWarrantyStart.Text.Trim());
            WarrantyEnd = string.IsNullOrEmpty(txtWarrantyEnd.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtWarrantyEnd.Text.Trim());
            StateID = ddlState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlState.SelectedValue);
        }

        protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor='#b3ecff';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white';";
                e.Row.ToolTip = "Click On View Icon for More Details... ";
            }
        }
    }
}