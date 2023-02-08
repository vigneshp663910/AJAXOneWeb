using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class DealerwisePermissionList : System.Web.UI.Page
    {
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Employee » Dealerwise Permission List');</script>");
            try
            {
                if (!IsPostBack)
                {
                    new DDLBind(ddlRegion, new BDMS_Address().GetRegion(null, null, null), "Region", "RegionID");
                    FillDealer();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        public List<PDealer> UserwiseDealerList
        {
            get
            {
                if (ViewState["UserwiseDealerList"] == null)
                {
                    ViewState["UserwiseDealerList"] = new List<PDealer>();
                }
                return (List<PDealer>)ViewState["UserwiseDealerList"];
            }
            set
            {
                ViewState["UserwiseDealerList"] = value;
            }
        }
        void FillDealer()
        {
            int? RegionID = (ddlRegion.SelectedValue == "0") ? (int?)null : Convert.ToInt32(ddlRegion.SelectedValue);
            List<PDMS_Dealer> DealerList = new BDMS_Dealer().GetDealer(null, "", PSession.User.UserID, RegionID);
            ListViewDealer.DataSource = DealerList;
            ListViewDealer.DataBind();
        }
        private void FillGrid()
        {
            try
            {
                string DealerIDs = "";
                int dealerCount = 0;
                foreach (ListViewItem item in ListViewDealer.Items)
                {
                    CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                    Label lblDID = (Label)item.FindControl("lblDID");
                    if (chkDealer.Checked == true)
                    {
                        DealerIDs = DealerIDs + "," + lblDID.Text;
                        dealerCount += 1;
                    }
                }
                UserwiseDealerList = new BDealer().GetUserByDealerIDs(DealerIDs);

                List<PDealer> Dealers = new List<PDealer>();
                foreach (PDealer pDealer in UserwiseDealerList)
                {
                    var duplicates = UserwiseDealerList.Where(i => i.UserName == pDealer.UserName).ToList();
                    if (duplicates.Count == dealerCount)
                    {
                        bool containsItemState = Dealers.Any(item => item.UserName == pDealer.UserName);
                        if (!containsItemState)
                        {
                            PDealer Dealer = new PDealer();
                            Dealer.UserName = pDealer.UserName;
                            Dealer.ContactName = pDealer.ContactName;
                            Dealer.MailID1 = pDealer.MailID1;
                            Dealers.Add(Dealer);
                        }
                    }
                }
                UserwiseDealerList = Dealers.ToList();


                if (UserwiseDealerList.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                    gvDealerUsers.DataSource = null;
                    gvDealerUsers.DataBind();
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvDealerUsers.PageIndex) * gvDealerUsers.PageSize) + 1) + " - " + (((gvDealerUsers.PageIndex) * gvDealerUsers.PageSize) + gvDealerUsers.Rows.Count) + " of " + UserwiseDealerList.Count;
                    gvDealerUsers.DataSource = UserwiseDealerList;
                    gvDealerUsers.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 1;
                FillGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                foreach (ListViewItem item in ListViewDealer.Items)
                {
                    CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                    chkDealer.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem item in ListViewDealer.Items)
                {
                    CheckBox chkDealer = (CheckBox)item.FindControl("chkDealer");
                    chkDealer.Checked = false;
                }
            }
        }
        protected void gvDealerUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerUsers.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerUsers.PageIndex > 0)
            {
                gvDealerUsers.PageIndex = gvDealerUsers.PageIndex - 1;
                DealerBind(gvDealerUsers, lblRowCount, UserwiseDealerList);
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerUsers.PageCount > gvDealerUsers.PageIndex)
            {
                gvDealerUsers.PageIndex = gvDealerUsers.PageIndex + 1;
                DealerBind(gvDealerUsers, lblRowCount, UserwiseDealerList);
            }
        }
        void DealerBind(GridView gv, Label lbl, List<PDealer> DealerList)
        {
            gv.DataSource = DealerList;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerList.Count;
        }
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDealer();
        }
    }
}