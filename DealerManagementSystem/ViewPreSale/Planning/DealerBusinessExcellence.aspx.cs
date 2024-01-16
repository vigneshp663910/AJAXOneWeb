using Business;
using ClosedXML.Excel;
using Newtonsoft.Json;
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

namespace DealerManagementSystem.ViewPreSale.Planning
{
    public partial class DealerBusinessExcellence : BasePage
    {
        public List<PDealerBusinessExcellence> VT
        {
            get
            {
                if (Session["PDealerBusinessExcellence"] == null)
                {
                    Session["PDealerBusinessExcellence"] = new List<PDealerBusinessExcellence>();
                }
                return (List<PDealerBusinessExcellence>)Session["PDealerBusinessExcellence"];
            }
            set
            {
                Session["PDealerBusinessExcellence"] = value;
            }
        }
        public override SubModule SubModuleName { get { return SubModule.ViewPreSale_Planning_DealerBusinessExcellence; } }
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Planning » Dealer Business Excellence');</script>");
            if (!IsPostBack)
            {
                FillYearAndMonth();
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithDisplayName", "DID", true, "All Dealer");
                new DDLBind(ddlFunctionArea, new BDealer().GetDealerBusinessExcellenceFunctionArea(null), "FunctionArea", "DealerBusinessExcellenceCategory1", true, "All");
                ActionControlMange();
            }
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
            FillVisitTarget();
        }

        protected void gvVisitTarget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvMissionPlanning.PageIndex = e.NewPageIndex;
            FillVisitTarget();
            gvMissionPlanning.DataBind();
        }
        protected void ibtnVTArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageIndex > 0)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex - 1;
                VTBind(gvMissionPlanning, lblRowCountV, VT);
            }
        }
        protected void ibtnVTArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMissionPlanning.PageCount > gvMissionPlanning.PageIndex)
            {
                gvMissionPlanning.PageIndex = gvMissionPlanning.PageIndex + 1;
                VTBind(gvMissionPlanning, lblRowCountV, VT);
            }
        }
        void VTBind(GridView gv, Label lbl, List<PDealerBusinessExcellence> VT)
        {
            gv.DataSource = VT;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + VT.Count;
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                CheckBox cbIsSubmitted = (CheckBox)gv.Rows[i].FindControl("cbIsSubmitted");
                if (cbIsSubmitted.Checked)
                {
                    LinkButton lnkBtnMissionPlanningEdit = (LinkButton)gv.Rows[i].FindControl("lnkBtnMissionPlanningEdit");
                    lnkBtnMissionPlanningEdit.Visible = false;
                }
            }
        }

        void FillVisitTarget()
        {
            int? Year = ddlYear.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlYear.SelectedValue);
            int? Month = ddlMonth.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlMonth.SelectedValue);
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            int? Category1ID = ddlFunctionArea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlFunctionArea.SelectedValue);
            int? Category2ID = null;
            if (Category1ID != null)
            {
                Category2ID = ddlFunctionSubArea.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlFunctionSubArea.SelectedValue);
            }
            VT = new BDealer().GetDealerBusinessExcellenceToUpdate(Year, Month, DealerID, Category1ID, Category2ID);
            gvMissionPlanning.DataSource = VT;
            gvMissionPlanning.DataBind();

            if (VT.Count == 0)
            {
                lblRowCountV.Visible = false;
                ibtnVTArrowLeft.Visible = false;
                ibtnVTArrowRight.Visible = false;
            }
            else
            {
                lblRowCountV.Visible = true;
                ibtnVTArrowLeft.Visible = true;
                ibtnVTArrowRight.Visible = true;
                VTBind(gvMissionPlanning, lblRowCountV, VT);
                //lblRowCountV.Text = (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + 1) + " - " + (((gvMissionPlanning.PageIndex) * gvMissionPlanning.PageSize) + gvMissionPlanning.Rows.Count) + " of " + VT.Count;
            }

        }

        void Update()
        {
            List<PDealerMissionPlanning> VisitTarget = new List<PDealerMissionPlanning>();
            for (int i = 0; i < gvMissionPlanning.Rows.Count; i++)
            {
                //  Label lblVisitTargetID = (Label)gvMissionPlanning.Rows[i].FindControl("lblVisitTargetID"); 

                Label lblDealerID = (Label)gvMissionPlanning.Rows[i].FindControl("lblDealerID");
                Label lblYear = (Label)gvMissionPlanning.Rows[i].FindControl("lblYear");
                Label lblMonth = (Label)gvMissionPlanning.Rows[i].FindControl("lblMonth");

                TextBox txtBillingPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtBillingPlan");
                TextBox txtBillingRevenuePlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtBillingRevenuePlan");
                TextBox txtRetailPlan = (TextBox)gvMissionPlanning.Rows[i].FindControl("txtRetailPlan");


                VisitTarget.Add(new PDealerMissionPlanning()
                {
                    Dealer = new PDMS_Dealer() { DealerID = Convert.ToInt32(lblDealerID.Text) },
                    Year = Convert.ToInt32(lblYear.Text),
                    Month = DateTime.ParseExact(lblMonth.Text, "MMM", CultureInfo.CurrentCulture).Month,
                    BillingPlan = Convert.ToInt32(txtBillingPlan.Text),
                    BillingRevenuePlan = Convert.ToInt32(txtBillingRevenuePlan.Text),
                    RetailPlan = Convert.ToInt32(txtRetailPlan.Text),
                    CreatedBy = new PUser() { UserID = PSession.User.UserID }

                });
            }
            string result = new BAPI().ApiPut("Dealer/InsertOrUpdateDealerBusinessExcellence", VisitTarget);
            result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(result).Data);
            if (result == "0")
            {
                lblMessage.Text = "Data is not updated successfully ";
                return;
            }
            else
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Data is updated successfully ";
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnMissionPlanningEdit = (LinkButton)sender;
            GridViewRow row = (GridViewRow)(lnkBtnMissionPlanningEdit.NamingContainer);

            Label lblTarget = (Label)row.FindControl("lblTarget");
            Label lblActual = (Label)row.FindControl("lblActual");
            Label lblRemarks = (Label)row.FindControl("lblRemarks");

            TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
            TextBox txtActual = (TextBox)row.FindControl("txtActual");
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

            lblTarget.Visible = false;
            lblActual.Visible = false;
            lblRemarks.Visible = false;

            txtTarget.Visible = true;
            txtActual.Visible = true;
            txtRemarks.Visible = true;

            txtTarget.Text = lblTarget.Text;
            txtActual.Text = lblActual.Text;
            txtRemarks.Text = lblRemarks.Text;

            Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
            Button btnBack = (Button)row.FindControl("btnBack");
            BtnUpdateMissionPlanning.Visible = true;
            btnBack.Visible = true;
            lnkBtnMissionPlanningEdit.Visible = false;
        }

        protected void OnDataBound(object sender, EventArgs e)
        {


        }

        protected void BtnUpdateMissionPlanning_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)(btn.NamingContainer);

            Label lblTarget = (Label)row.FindControl("lblTarget");
            Label lblActual = (Label)row.FindControl("lblActual");
            Label lblRemarks = (Label)row.FindControl("lblRemarks");

            TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
            TextBox txtActual = (TextBox)row.FindControl("txtActual");
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

            if (btn.ID == "btnBack")
            {
                lblTarget.Visible = true;
                lblActual.Visible = true;
                lblRemarks.Visible = true;

                txtTarget.Visible = false;
                txtActual.Visible = false;
                txtRemarks.Visible = false;

                Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
                Button btnBack = (Button)row.FindControl("btnBack");
                LinkButton lnkBtnMissionPlanningEdit = (LinkButton)row.FindControl("lnkBtnMissionPlanningEdit");
                BtnUpdateMissionPlanning.Visible = false;
                btnBack.Visible = false;
                lnkBtnMissionPlanningEdit.Visible = true;

            }
            else
            {
                Label lblDealerID = (Label)row.FindControl("lblDealerID");

                Label lblParameterID = (Label)row.FindControl("lblParameterID");
                Label lblYear = (Label)row.FindControl("lblYear");
                Label lblMonth = (Label)row.FindControl("lblMonth");
                List<PDealerBusinessExcellence> Plannings = new List<PDealerBusinessExcellence>();
                decimal value;
                if (!Decimal.TryParse(txtTarget.Text.Trim(), out value))
                {
                    lblMessage.Text = "Please update proper value in Target";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                if (!Decimal.TryParse(txtActual.Text.Trim(), out value))
                {
                    lblMessage.Text = "Please update proper value in Actual";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                PDealerBusinessExcellence Planning = new PDealerBusinessExcellence()
                {
                    DealerID = Convert.ToInt32(lblDealerID.Text),
                    DealerBusinessExcellenceCategory3ID = Convert.ToInt32(lblParameterID.Text),
                    Year = Convert.ToInt32(lblYear.Text),
                    Month = DateTime.ParseExact(lblMonth.Text, "MMM", CultureInfo.CurrentCulture).Month,
                    Target = string.IsNullOrEmpty(txtTarget.Text.Trim()) ? 0 : Convert.ToDecimal(txtTarget.Text.Trim()),
                    Actual = string.IsNullOrEmpty(txtActual.Text.Trim()) ? 0 : Convert.ToDecimal(txtActual.Text.Trim()),
                    Remarks = txtRemarks.Text.Trim()
                };
                Plannings.Add(Planning);

                PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/InsertOrUpdateDealerBusinessExcellence", Plannings));

                if (result.Status == PApplication.Failure)
                {
                    lblMessage.Text = "Dealer Mission Planning is not updated successfully ";
                    lblMessage.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "Dealer Mission Planning is updated successfully ";

                    lblTarget.Visible = true;
                    lblActual.Visible = true;
                    lblRemarks.Visible = true;

                    txtTarget.Visible = false;
                    txtActual.Visible = false;
                    txtRemarks.Visible = false;

                    lblTarget.Text = txtTarget.Text;
                    lblActual.Text = txtActual.Text;
                    lblRemarks.Text = txtRemarks.Text; ;

                    Button BtnUpdateMissionPlanning = (Button)row.FindControl("BtnUpdateMissionPlanning");
                    Button btnBack = (Button)row.FindControl("btnBack");
                    LinkButton lnkBtnMissionPlanningEdit = (LinkButton)row.FindControl("lnkBtnMissionPlanningEdit");
                    BtnUpdateMissionPlanning.Visible = false;
                    btnBack.Visible = false;
                    lnkBtnMissionPlanningEdit.Visible = true;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int Year = Convert.ToInt32(ddlYear.SelectedValue);
            int Month = Convert.ToInt32(ddlMonth.SelectedValue);
            int DealerID = Convert.ToInt32(ddlDealer.SelectedValue);
            string endPoint = "Dealer/UpdateDealerBusinessExcellenceSubmit?Year=" + Year + "&Month=" + Month + "&DealerID=" + DealerID ;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = Results.Message;
                return;
            }
            else
            {
                lblMessage.Text = "Submitted Successfully";
                FillVisitTarget();
            }
        }

        void ActionControlMange()
        {
            btnSubmit.Visible = true;  
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.DealerBusinessExcellenceSubmit).Count() == 0)
            {
                btnSubmit.Visible = false; 
            } 
        }

        protected void ddlFunctionArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlFunctionSubArea, new BDealer().GetDealerBusinessExcellenceFunctionSubArea(Convert.ToInt32(ddlFunctionArea.SelectedValue), null), "FunctionSubArea", "DealerBusinessExcellenceCategory2", true, "All");

        }
    }
}