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
            FillFollowUps();
        }
        void FillFollowUps()
        {
            List<PLeadFollowUp> FollowUp = new BLead().GetLeadFollowUp(null, null, null, null, PSession.User.UserID);
            gvFollowUp.DataSource = FollowUp;
            gvFollowUp.DataBind();
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
    }
}