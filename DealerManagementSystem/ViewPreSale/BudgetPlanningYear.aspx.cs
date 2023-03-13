using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
    public partial class BudgetPlanningYear : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_BudgetPlanningYear; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('PreSale » Planning » Budget-Yearly');</script>");
            if (!IsPostBack)
            {
                //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new DDLBind().Year(ddlYear, 2022);

            }
        }

        void FillVisitTargetPlanning()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            VTP = new BBudgetPlanningYearWise().GetBudgetPlanningYearWise(DealerID, null, Convert.ToInt32(ddlYear.SelectedValue));

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

        public List<PBudgetPlanningYearWise> VTP
        {
            get
            {
                if (Session["BudgetPlanningYear"] == null)
                {
                    Session["BudgetPlanningYear"] = new List<PBudgetPlanningYearWise>();
                }
                return (List<PBudgetPlanningYearWise>)Session["BudgetPlanningYear"];
            }
            set
            {
                Session["BudgetPlanningYear"] = value;
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
                VTBind(gvVisitTargetPlanning, lblRowCountVTP);
            }
        }
        protected void ibtnVTPArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvVisitTargetPlanning.PageCount > gvVisitTargetPlanning.PageIndex)
            {
                gvVisitTargetPlanning.PageIndex = gvVisitTargetPlanning.PageIndex + 1;
                VTBind(gvVisitTargetPlanning, lblRowCountVTP);
            }
        }

        void VTBind(GridView gv, Label lbl)
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
            List<PBudgetPlanningYearWise> VisitTarget = new List<PBudgetPlanningYearWise>();
            for (int i = 0; i < gvVisitTargetPlanning.Rows.Count; i++)
            {
                Label lblDealerID = (Label)gvVisitTargetPlanning.Rows[i].FindControl("lblDealerID");
                Label lblModelID = (Label)gvVisitTargetPlanning.Rows[i].FindControl("lblModelID");
                Label lblYear = (Label)gvVisitTargetPlanning.Rows[i].FindControl("lblYear");
                TextBox txtBudget = (TextBox)gvVisitTargetPlanning.Rows[i].FindControl("txtBudget");

                VisitTarget.Add(new PBudgetPlanningYearWise()
                {
                    Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) },
                    Model = new PDMS_Model() { ModelID = Convert.ToInt32(lblModelID.Text) },
                    Year = Convert.ToInt32(lblYear.Text),
                    Budget = string.IsNullOrEmpty(txtBudget.Text) ? 0 : Convert.ToInt32(txtBudget.Text)
                });
            }
            Success = new BBudgetPlanningYearWise().insertOrUpdateBudgetPlanningYearWise(VisitTarget, PSession.User.UserID);
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillVisitTargetPlanning();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                BudgetPlanningYearExportExcel(VTP, "Budget Planning Year Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void BudgetPlanningYearExportExcel(List<PBudgetPlanningYearWise> VTPs, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Model");
            dt.Columns.Add("Year");
            dt.Columns.Add("Budget");
            dt.Columns.Add("Actual");
            dt.Columns.Add("Freezed");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Created On");
            dt.Columns.Add("Modified By");
            dt.Columns.Add("Modified On");
            foreach (PBudgetPlanningYearWise VTP in VTPs)
            {
                dt.Rows.Add(
                    "'" + VTP.Dealer.DealerCode
                    , VTP.Dealer.DealerName
                    , VTP.Model.Model
                    , VTP.Year
                    , VTP.Budget
                    , VTP.Actual
                    , VTP.Freezed
                    , (VTP.CreatedBy == null) ? "" : VTP.CreatedBy.ContactName
                    , VTP.CreatedOn
                    , (VTP.ModifiedBy == null) ? "" : VTP.ModifiedBy.ContactName
                    , VTP.ModifiedOn
                    );
            }
            try
            {
                new BXcel().ExporttoExcel(dt, Name);
            }
            catch
            {

            }
            finally
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>HideProgress();</script>");
            }
        }
    }
}