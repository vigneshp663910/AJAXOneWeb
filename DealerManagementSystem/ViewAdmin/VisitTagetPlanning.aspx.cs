using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewAdmin
{
    public partial class VisitTagetPlanning : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Visit Target Planning');</script>");
            if (!IsPostBack)
            {
                FillVisitTargetPlanning();
            }
        }

        void FillVisitTargetPlanning()
        {
            VTP = new BDMS_Dealer().GetVistTargetPlan(null, null);

            gvVisitTargetPlanning.DataSource = VTP;
            gvVisitTargetPlanning.DataBind();

            if (VTP.Count == 0)
            {
                lblRowCountVTP.Visible = false;
                ibtnVTPArrowLeft.Visible = false;
                ibtnVTPArrowRight.Visible = false;
            }
            else
            {
                lblRowCountVTP.Visible = true;
                ibtnVTPArrowLeft.Visible = true;
                ibtnVTPArrowRight.Visible = true;
                lblRowCountVTP.Text = (((gvVisitTargetPlanning.PageIndex) * gvVisitTargetPlanning.PageSize) + 1) + " - " + (((gvVisitTargetPlanning.PageIndex) * gvVisitTargetPlanning.PageSize) + gvVisitTargetPlanning.Rows.Count) + " of " + VTP.Count;
            }

        }

        public List<PDMS_DealerDesignation> VTP
        {
            get
            {
                if (Session["PDMS_DealerDesignation"] == null)
                {
                    Session["PDMS_DealerDesignation"] = new List<PVisitTarget>();
                }
                return (List<PDMS_DealerDesignation>)Session["PDMS_DealerDesignation"];
            }
            set
            {
                Session["PDMS_DealerDesignation"] = value;
            }
        }

        protected void gvVisitTargetPlanning_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvVisitTargetPlanning.PageIndex = e.NewPageIndex;
            FillVisitTargetPlanning();
            gvVisitTargetPlanning.DataBind();
        }


        protected void ibtnVTPArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvVisitTargetPlanning.PageIndex > 0)
            {
                gvVisitTargetPlanning.PageIndex = gvVisitTargetPlanning.PageIndex - 1;
                VTBind(gvVisitTargetPlanning, lblRowCountVTP, VTP);
            }
        }
        protected void ibtnVTPArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvVisitTargetPlanning.PageCount > gvVisitTargetPlanning.PageIndex)
            {
                gvVisitTargetPlanning.PageIndex = gvVisitTargetPlanning.PageIndex + 1;
                VTBind(gvVisitTargetPlanning, lblRowCountVTP, VTP);
            }
        }

        void VTBind(GridView gv, Label lbl, List<PDMS_DealerDesignation> VTP)
        {
            gv.DataSource = VTP;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VTP.Count;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Update();
            FillVisitTargetPlanning();
            
        }

        void Update()
        {
            Boolean Success;
            List<PDMS_DealerDesignation> VisitTarget = new List<PDMS_DealerDesignation>();
            for (int i = 0; i < gvVisitTargetPlanning.Rows.Count; i++)
            {
                Label lblDesignationID = (Label)gvVisitTargetPlanning.Rows[i].FindControl("lblDesignationID");
                TextBox txtSalesColdCustomerVisitTarget = (TextBox)gvVisitTargetPlanning.Rows[i].FindControl("txtSalesColdCustomerVisitTarget");
                TextBox txtSalesProspectCustomertVisitTarget = (TextBox)gvVisitTargetPlanning.Rows[i].FindControl("txtSalesProspectCustomertVisitTarget");
                TextBox txtSalesExistCustomerVisitTarget = (TextBox)gvVisitTargetPlanning.Rows[i].FindControl("txtSalesExistCustomerVisitTarget");

                VisitTarget.Add(new PDMS_DealerDesignation()
                {
                    DealerDesignationID = Convert.ToInt32(lblDesignationID.Text),
                    SalesColdCustomerVisitTarget = string.IsNullOrEmpty(txtSalesColdCustomerVisitTarget.Text) ?0: Convert.ToInt32(txtSalesColdCustomerVisitTarget.Text),
                    SalesProspecCustomertVisitTarget = string.IsNullOrEmpty(txtSalesProspectCustomertVisitTarget.Text) ? 0 : Convert.ToInt32(txtSalesProspectCustomertVisitTarget.Text),
                    SalesExistCustomerVisitTarget = string.IsNullOrEmpty(txtSalesExistCustomerVisitTarget.Text) ? 0 : Convert.ToInt32(txtSalesExistCustomerVisitTarget.Text),
                    ModifiedBy = new PUser() { UserID = PSession.User.UserID }
                });
            }
            Success = new BDMS_Dealer().UpdateVisitTargetPlanning(VisitTarget);
            if (!Success)
            {
                lblMessage.Text = "Visit Target is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Visit Target is updated successfully ";
            }
        }
    }
}