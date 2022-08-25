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
    public partial class BudgetPlanningMonth : System.Web.UI.Page
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('PreSale » Planning » Budget-Monthly');</script>");
            if (!IsPostBack)
            {
                // new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new DDLBind().Year(ddlYear, 2022);
                new DDLBind().Month(ddlMonth); 
            }
        }

        void FillVisitTargetPlanning()
        {
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            VTP = new BBudgetPlanningYearWise().GetBudgetPlanningMonthWise(DealerID, null, Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue));

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

        public List<PBudgetPlanningMonthWise> VTP
        {
            get
            {
                if (Session["BudgetPlanningMonth"] == null)
                {
                    Session["BudgetPlanningMonth"] = new List<PBudgetPlanningMonthWise>();
                }
                return (List<PBudgetPlanningMonthWise>)Session["BudgetPlanningMonth"];
            }
            set
            {
                Session["BudgetPlanningMonth"] = value;
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
            List<PBudgetPlanningMonthWise> VisitTarget = new List<PBudgetPlanningMonthWise>();
            for (int i = 0; i < gvVisitTargetPlanning.Rows.Count; i++)
            {
                Label lblBudgetPYWiseID = (Label)gvVisitTargetPlanning.Rows[i].FindControl("lblBudgetPYWiseID"); 
                Label lblMonth = (Label)gvVisitTargetPlanning.Rows[i].FindControl("lblMonth");
                TextBox txtPlaned = (TextBox)gvVisitTargetPlanning.Rows[i].FindControl("txtPlaned");

                VisitTarget.Add(new PBudgetPlanningMonthWise()
                {
                    BudgetPlanningYear = new PBudgetPlanningYearWise()
                    {
                        BudgetPYWiseID = Convert.ToInt64(lblBudgetPYWiseID.Text)  
                    },
                    Month = Convert.ToInt32(lblMonth.Text),
                    Planed = string.IsNullOrEmpty(txtPlaned.Text) ? 0 : Convert.ToInt32(txtPlaned.Text),
                });
            }
            Success = new BBudgetPlanningYearWise().insertOrUpdateBudgetPlanningMonthWise(VisitTarget, PSession.User.UserID);
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
                BudgetPlanningYearExportExcel(VTP, "Budget Planning Month Report");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
        void BudgetPlanningYearExportExcel(List<PBudgetPlanningMonthWise> VTPs, String Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Model");
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("Budget");
            dt.Columns.Add("Actual");
            dt.Columns.Add("Freezed");
            dt.Columns.Add("Created By");
            dt.Columns.Add("Created On");
            dt.Columns.Add("Modified By");
            dt.Columns.Add("Modified On");
            foreach (PBudgetPlanningMonthWise VTP in VTPs)
            {
                dt.Rows.Add(
                    "'" + VTP.BudgetPlanningYear.Dealer.DealerCode
                    , VTP.BudgetPlanningYear.Dealer.DealerName
                    , VTP.BudgetPlanningYear.Model.Model
                    , VTP.BudgetPlanningYear.Year
                    , VTP.Month
                    , VTP.Planed
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