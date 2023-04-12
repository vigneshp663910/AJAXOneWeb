using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewDealerEmployee
{
    public partial class MachineOperatorApproval : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewDealerEmployee_MachineOperatorApproval; } }
        public List<PMachineOperator> MachineOperator
        {
            get
            {
                if (Session["MO_Approval"] == null)
                {
                    Session["MO_Approval"] = new List<PMachineOperator>();
                }
                return (List<PMachineOperator>)Session["MO_Approval"];
            }
            set
            {
                Session["MO_Approval"] = value;
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
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Dealer Employee » Machine Operator Approval');</script>");
            if (!IsPostBack)
            {
                FillMachineOperatorDetails();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillMachineOperatorDetails();
        }
        private void FillMachineOperatorDetails()
        {
            MachineOperator = new BMachineOperator().GetMachineOperatorDetailsForApproval();
            gvMOP.DataSource = MachineOperator;
            gvMOP.DataBind();
            lblRowCount.Text = (((gvMOP.PageIndex) * gvMOP.PageSize) + 1) + " - " + (((gvMOP.PageIndex) * gvMOP.PageSize) + gvMOP.Rows.Count) + " of " + MachineOperator.Count;
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMOP.PageIndex > 0)
            {
                gvMOP.DataSource = MachineOperator;
                gvMOP.PageIndex = gvMOP.PageIndex - 1;
                gvMOP.DataBind();
                lblRowCount.Text = (((gvMOP.PageIndex) * gvMOP.PageSize) + 1) + " - " + (((gvMOP.PageIndex) * gvMOP.PageSize) + gvMOP.Rows.Count) + " of " + MachineOperator.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvMOP.PageCount > gvMOP.PageIndex)
            {
                gvMOP.DataSource = MachineOperator;
                gvMOP.PageIndex = gvMOP.PageIndex + 1;
                gvMOP.DataBind();
                lblRowCount.Text = (((gvMOP.PageIndex) * gvMOP.PageSize) + 1) + " - " + (((gvMOP.PageIndex) * gvMOP.PageSize) + gvMOP.Rows.Count) + " of " + MachineOperator.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Requested Date");
            dt.Columns.Add("Model");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Service Priority");
            dt.Columns.Add("Service Status");
            dt.Columns.Add("Margin Warranty");

            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }
        protected void lbView_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Session["MachineOperatorApproval"] = gvMOP.DataKeys[index].Value.ToString();
            string url = "MachineOperatorRegister.aspx";
            Response.Redirect(url);
        }
        //protected void lbApproval_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    int index = gvRow.RowIndex;
        //    //if (new BMachineOperator().ApproveMachineOperatorDetails(Convert.ToInt64(gvMOP.DataKeys[index].Value), PSession.User.UserID))


        //    PApiResult ResultApprove = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Operator/RequestApproval", Convert.ToInt64(gvMOP.DataKeys[index].Value)));
        //    if (ResultApprove.Status == PApplication.Failure)
        //    {
        //        lblMessage.Text = ResultApprove.Message;
        //        lblMessage.ForeColor = Color.Red;
        //        return;
        //    }
        //    if (Convert.ToBoolean(ResultApprove.Data))
        //    {
        //        lblMessage.Text = "Machine Operator approved successfully";
        //        lblMessage.ForeColor = Color.Green;
        //        FillMachineOperatorDetails();
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Machine Operator is not approved successfully";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}
        protected void gvMOP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMOP.DataSource = MachineOperator;
            gvMOP.PageIndex = e.NewPageIndex;
            gvMOP.DataBind();
            lblRowCount.Text = (((gvMOP.PageIndex) * gvMOP.PageSize) + 1) + " - " + (((gvMOP.PageIndex) * gvMOP.PageSize) + gvMOP.Rows.Count) + " of " + MachineOperator.Count;
        }
    }
}