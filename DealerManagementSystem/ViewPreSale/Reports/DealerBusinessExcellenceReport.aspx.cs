using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale.Reports
{
    public partial class DealerBusinessExcellenceReport : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Reports_DealerBusinessExcellenceReport; } }
        private DataTable LeadReport
        {
            get
            {
                if (Session["DealerBusinessExcellenceReport"] == null)
                {
                    Session["DealerBusinessExcellenceReport"] = new DataTable();
                }
                return (DataTable)Session["DealerBusinessExcellenceReport"];
            }
            set
            {
                Session["DealerBusinessExcellenceReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Dealer Mission Planning Report');</script>");
            if (!IsPostBack)
            {
                LeadReport = null;
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer"); 
            }
            VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
        }
        void FillYearAndMonth()
        {
            ddlYear.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 2023; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Insert(i + 1 - 2023, new ListItem(i.ToString(), i.ToString()));
            }

            ddlMonth.Items.Insert(0, new ListItem("All", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Insert(i, new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillLead();
        }
        void FillLead()
        {
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); 


            LeadReport = new BDealer().GetDealerBusinessExcellenceReport(Year, Month, DealerID, null);



            if (LeadReport.Rows.Count == 0)
            {
                lblRowCountV.Visible = false;
                ibtnVTArrowLeft.Visible = false;
                ibtnVTArrowRight.Visible = false;
                gvMissionPlanning.DataSource = LeadReport;
                gvMissionPlanning.DataBind();
            }
            else
            {
                lblRowCountV.Visible = true;
                ibtnVTArrowLeft.Visible = true;
                ibtnVTArrowRight.Visible = true;
                VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
                //lblRowCountV.Text = (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + 1) + " - " + (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + gvMissionPlanning.Rows.Count) + " of " + LeadReport.Rows.Count;
            }
        }

        protected void gvVisitTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMissionPlanning.PageIndex = e.NewPageIndex;
            VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
        }
        protected void ibtnVTArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageIndex > 0)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex - 1;
                VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
            }
        }
        protected void ibtnVTArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageCount > gvMissionPlanning.PageIndex)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex + 1;
                VTBind(gvMissionPlanning, lblRowCountV, LeadReport);
            }
        }

        void VTBind(GridView gv, Label lbl, DataTable VT)
        {
            gv.DataSource = VT;
            gv.DataBind();
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                Label lblMonthName = (Label)gv.Rows[i].FindControl("lblMonthName");
                Label lblMonth = (Label)gv.Rows[i].FindControl("lblMonth");

                lblMonthName.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(lblMonth.Text)).Substring(0, 3);
            }
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VT.Rows.Count;
            if (VT.Rows.Count > 0)
            {
                decimal MaxScore = 0, FinalScore = 0;
                foreach (DataRow dr in VT.Rows)
                {
                    MaxScore = MaxScore + Convert.ToDecimal(dr["Max Score"]);
                    FinalScore = FinalScore + Convert.ToDecimal(dr["Final Score"]);
                }
                Label lblMaxScoreF = (Label)gv.FooterRow.FindControl("lblMaxScoreF");
                lblMaxScoreF.Text = MaxScore.ToString("##.##"); 
                Label lblFinalScoreF = (Label)gv.FooterRow.FindControl("lblFinalScoreF");
                lblFinalScoreF.Text = FinalScore.ToString("##.##");
            }
        }

        protected void OnDataBound(object sender, EventArgs e)
        {
            gvHeader(gvMissionPlanning);
        }

        protected void gvHeader(GridView gv)
        {
            
        }
        protected void gvHeaderCellInfo(GridViewRow row, string Name, int ColumnSpan)
        {
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = Name;
            cell.ColumnSpan = ColumnSpan;
            row.Controls.Add(cell);
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    new BXcel().ExporttoExcel(LeadReport, "Dealer Business Excellence Report");
                }
                catch
                {
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}