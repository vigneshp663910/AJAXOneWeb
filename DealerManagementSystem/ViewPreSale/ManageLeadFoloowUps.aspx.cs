using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewPreSale
{
   
    public partial class ManageLeadFoloowUps : System.Web.UI.Page
    {
        public long LeadFollowUpID
        {
            get
            {
                if (Session["LeadFollowUpID"] == null)
                {
                    Session["LeadFollowUpID"] = 0;
                }
                return Convert.ToInt64(Session["LeadFollowUpID"]);
            }
            set
            {
                Session["LeadFollowUpID"] = value;
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
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Pre-Sales » Manage Follow up');</script>");

                txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                FillFollowUps();
            }
        }


        public List<PLeadFollowUp> FU
        {
            get
            {
                if (Session["FU"] == null)
                {
                    Session["FU"] = new List<PLeadFollowUp>();
                }
                return (List<PLeadFollowUp>)Session["FU"];
            }
            set
            {
                Session["FU"] = value;
            }
        }

        protected void ibtnFUArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvFollowUp.PageIndex > 0)
            {
                gvFollowUp.PageIndex = gvFollowUp.PageIndex - 1;
                FUBind(gvFollowUp, lblRowCount, FU);
            }
        }
        protected void ibtnFUArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvFollowUp.PageCount > gvFollowUp.PageIndex)
            {
                gvFollowUp.PageIndex = gvFollowUp.PageIndex + 1;
                FUBind(gvFollowUp, lblRowCount, FU);
            }
        }

        void FUBind(GridView gv, Label lbl, List<PLeadFollowUp> FU)
        {
            gv.DataSource = FU;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + FU.Count;
        }



        void FillFollowUps()
        {
            long? LeadID = null;
            int? SalesEngineerUserID = null;
            //DateTime? From = string.IsNullOrEmpty(txtDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateFrom.Text.Trim());
            //DateTime? To = string.IsNullOrEmpty(txtDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDateTo.Text.Trim());
            int? DealerID =  null;
            //List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUp(LeadID, SalesEngineerUserID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), DealerID, txtCustomer.Text.Trim());

            FU = new BLead().GetLeadFollowUp(LeadID, SalesEngineerUserID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), DealerID, txtCustomer.Text.Trim());

            gvFollowUp.DataSource = FU;
            gvFollowUp.DataBind();

            if (FU.Count == 0)
            {
                lblRowCount.Visible = false;
                ibtnFUArrowLeft.Visible = false;
                ibtnFUArrowRight.Visible = false;
            }
            else
            {
                lblRowCount.Visible = true;
                ibtnFUArrowLeft.Visible = true;
                ibtnFUArrowRight.Visible = true;
                lblRowCount.Text = (((gvFollowUp.PageIndex) * gvFollowUp.PageSize) + 1) + " - " + (((gvFollowUp.PageIndex) * gvFollowUp.PageSize) + gvFollowUp.Rows.Count) + " of " + FU.Count;
            }

        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Customer")
            {
                
            }
            else if (lbActions.Text == "Add Attribute")
            {
                
            }
            MPE_FoloowUpStatus.Show();
        }
        protected void btnFoloowUpStatus_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblLeadFollowUpID = (Label)gvRow.FindControl("lblLeadFollowUpID");
            PLeadFollowUp FollowUp = new PLeadFollowUp();
            FollowUp.LeadFollowUpID = Convert.ToInt64(lblLeadFollowUpID.Text);

            FollowUp.Remark = txtRemark.Text.Trim();

            FollowUp.Status = new PPreSaleStatus() { StatusID = 2 };

            FollowUp.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Lead/FollowUpStatus", FollowUp)).Data);
            lblMessage.Visible = true;
            if (s == "0")
            {
                lblMessage.Text = "Something went wrong try again.";
                lblMessage.ForeColor = Color.Red;
            }
            else
            {
                lblMessage.Text = "Removed successfully";
                lblMessage.ForeColor = Color.Green;
            }
            FillFollowUps();
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillFollowUps();
        }
    }
}