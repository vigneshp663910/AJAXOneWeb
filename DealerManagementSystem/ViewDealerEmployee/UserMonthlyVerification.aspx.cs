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
    public partial class UserMonthlyVerification : System.Web.UI.Page
    {
        public List<PDMS_DealerEmployee> DealerEmployeeList
        {
            get
            {
                if (Session["DealerEmployee"] == null)
                {
                    Session["DealerEmployee"] = new List<PDMS_DealerEmployee>();
                }
                return (List<PDMS_DealerEmployee>)Session["DealerEmployee"];
            }
            set
            {
                Session["DealerEmployee"] = value;
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

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Monthly User Verification');</script>");


            lblMessage.Text = "";
            if (!IsPostBack)
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
            }
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDealerEmployee();
        }

        void fillDealerEmployee()
        {
            
            int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            if (DealerID == 0)
            {
                lblMessage.Text = "Please select the Dealer.";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            DealerEmployeeList = new BDMS_Dealer().GetDealerEmployeeForUserMonthlyVerification(DealerID, null, null, null, null);
            DealerEmployeeBind();
            //gvDealerEmployee.DataSource = DealerEmployeeList;
            //gvDealerEmployee.DataBind();
        }

        protected void ibtnDealerEmployeeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;
                DealerEmployeeBind();
            }
        }

        protected void ibtnDealerEmployeeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                DealerEmployeeBind();
            }
        }

        void DealerEmployeeBind()
        {
            gvDealerEmployee.DataSource = DealerEmployeeList;
            gvDealerEmployee.DataBind();
            lblRowCountDealerEmployee.Text = (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + 1) + " - " + (((gvDealerEmployee.PageIndex) * gvDealerEmployee.PageSize) + gvDealerEmployee.Rows.Count) + " of " + DealerEmployeeList.Count;
        }

        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            fillDealerEmployee();
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblUserID = (Label)gvRow.FindControl("lblUserID");
            int VerifiedMonth = DateTime.Now.Month;

            if (new BDMS_Dealer().UpdateUserMontlyVerification(Convert.ToInt64(lblUserID.Text), VerifiedMonth, PSession.User.UserID))
            {
                lblMessage.Text = "User monthly verification is done";
                lblMessage.ForeColor = Color.Green;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "User monthly verification is not done";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            fillDealerEmployee();
        }
    }
}