using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.Dashboard
{
    public partial class WarrantyMaterialAnalysis : BasePage
    {
        private BasePage _page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SerDealerID"] != null)
                {
                    ddlDealer.SelectedValue = Convert.ToString((int)Session["SerDealerID"]);
                }

                if (Session["SerDateFrom"] != null)
                {
                    txtDateFrom.Text = Convert.ToString(Session["SerDateFrom"]);
                }
                if (Session["SerDateTo"] != null)
                {
                    txtDateTo.Text = Convert.ToString(Session["SerDateTo"]);
                }

                fillDealer();
                fill(this);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        { 
            fill(this);
        }

        void fill(BasePage page)
        { 
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            DateTime? DateFrom = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime? DateTo = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());

           
            string Material = "";
            DataTable dt = new BDMS_Service().GetWarrantyMaterialAnalysis(DealerID, DateFrom, DateTo, Material, PSession.User.UserID);
            gvMaterialAnalysis.DataSource = dt;
            gvMaterialAnalysis.DataBind();


        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();

            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }
        
    }
}