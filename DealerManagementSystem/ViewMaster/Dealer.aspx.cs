using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class Dealer : System.Web.UI.Page
    {
        public List<PDMS_Dealer> DealerList
        {
            get
            {
                if (Session["Dealer"] == null)
                {
                    Session["Dealer"] = new List<PDMS_Dealer>();
                }
                return (List<PDMS_Dealer>)Session["Dealer"];
            }
            set
            {
                Session["Dealer"] = value;
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

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master » Dealer');</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master <i class= '+ '"' + 'fa fa-angle-double-down fa-2x' + '"'> </i>Customer');</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Master < i class='fa fa-fw fa-home font-white' style='color: lightgray'></i> Customer');</script>");


            lblDealerMessage.Text = "";
            if (!IsPostBack)
            {
                //List<PDMS_Country> Country = new BDMS_Address().GetCountry(null, null);
                //new DDLBind(ddlDSCountry, Country, "Country", "CountryID");
                //ddlDSCountry.SelectedValue = "1";
                //List<PDMS_State> State = new BDMS_Address().GetState(1, null, null, null);
                //new DDLBind(ddlDState, State, "State", "StateID");
                //List<PDMS_District> District = new BDMS_Address().GetDistrict(1, null, Convert.ToInt32(ddlDState.SelectedValue), null, null, Convert.ToInt32(ddlDealer.SelectedValue));
                //new DDLBind(ddlDistrict, State, "District", "DistrictID"); 
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchDealer();
        }

        protected void ibtnDealerArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealer.PageIndex > 0)
            {
                gvDealer.PageIndex = gvDealer.PageIndex - 1;
                DealerBind(gvDealer, lblRowCount, DealerList);
            }
        }
        protected void ibtnDealerArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealer.PageCount > gvDealer.PageIndex)
            {
                gvDealer.PageIndex = gvDealer.PageIndex + 1;
                DealerBind(gvDealer, lblRowCount, DealerList);
            }
        }


        void DealerBind(GridView gv, Label lbl, List<PDMS_Dealer> DealerList)
        {
            gv.DataSource = DealerList;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerList.Count;
        }

        void SearchDealer()
        {
            //int? CountryID = ddlDSCountry.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDSCountry.SelectedValue);
            //int? StateID = ddlDState.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDState.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);

            DealerList   = new BDMS_Dealer().GetDealer(DealerID, "", PSession.User.UserID);
             
            gvDealer.DataSource = DealerList;
            gvDealer.DataBind();


            if (DealerList.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnDealerArrowLeft.Visible = false;
                ibtnDealerArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnDealerArrowLeft.Visible = true;
                ibtnDealerArrowRight.Visible = true;
                lblRowCount.Text = (((gvDealer.PageIndex) * gvDealer.PageSize) + 1) + " - " + (((gvDealer.PageIndex) * gvDealer.PageSize) + gvDealer.Rows.Count) + " of " + DealerList.Count;
            }

        }
        
       

        protected void btnBackToList_Click(object sender, EventArgs e)
        {
            divDealerView.Visible = false;
            divDealerList.Visible = true;
        }

        protected void btnAddDealer_Click(object sender, EventArgs e)
        {
            //MPE_Customer.Show();
            //UC_Customer.FillMaster();
        }

        protected void gvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealer.PageIndex = e.NewPageIndex;
            SearchDealer();
        }
       
        protected void btnViewDealer_Click(object sender, EventArgs e)
        {
            divDealerView.Visible = true;
            divDealerList.Visible = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent; 
            Label lblDealerID = (Label)gvRow.FindControl("lblDealerID");

            UC_DealerView.filldealer(Convert.ToInt32(lblDealerID.Text));
        }

        protected void gvDealer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealer.PageIndex = e.NewPageIndex;
            SearchDealer();
        }
    }
}