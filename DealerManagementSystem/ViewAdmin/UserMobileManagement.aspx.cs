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

namespace DealerManagementSystem.ViewAdmin
{
    public partial class UserMobileManagement : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewAdmin_UserMobileManagement; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Manage Mobile User');</script>");

            if (!IsPostBack)
            {
                fillDealer();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage.Visible = false;
            fillUser();
        }

        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUser.PageIndex = e.NewPageIndex;
            fillUser();
        }

        public List<PUserMobile> UserLst
        {
            get
            {
                if (Session["PUser"] == null)
                {
                    Session["PUser"] = new List<PUserMobile>();
                }
                return (List<PUserMobile>)Session["PUser"];
            }
            set
            {
                Session["PUser"] = value;
            }
        }


        protected void ibtnUserArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvUser.PageIndex > 0)
            {
                gvUser.PageIndex = gvUser.PageIndex - 1;
                UserBind(gvUser, lblRowCount, UserLst);
            }
        }
        protected void ibtnUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvUser.PageCount > gvUser.PageIndex)
            {
                gvUser.PageIndex = gvUser.PageIndex + 1;
                UserBind(gvUser, lblRowCount, UserLst);
            }
        }

        void UserBind(GridView gv, Label lbl, List<PUserMobile> UserLst)
        {
            gv.DataSource = UserLst;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + UserLst.Count;
        }



        void fillUser()
        {
            try
            {
                int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue); 
                //gvUser.DataSource = new BUser().GetUserMobileManage(DealerID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
                //gvUser.DataBind();

                UserLst = new BUser().GetUserMobileManage(DealerID, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(),Convert.ToBoolean(Convert.ToInt32(ddlStatus.SelectedValue)), txtUsername.Text.Trim());
                gvUser.DataSource = UserLst;
                gvUser.DataBind();

                if (UserLst.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnUserArrowLeft.Visible = false;
                    ibtnUserArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnUserArrowLeft.Visible = true;
                    ibtnUserArrowRight.Visible = true;
                    lblRowCount.Text = (((gvUser.PageIndex) * gvUser.PageSize) + 1) + " - " + (((gvUser.PageIndex) * gvUser.PageSize) + gvUser.Rows.Count) + " of " + UserLst.Count;
                }

            }
            catch (Exception ex)
            {
            }
        }
        void fillDealer()
        {
            ddlDealer.DataTextField = "CodeWithDisplayName";
            ddlDealer.DataValueField = "DID";
            ddlDealer.DataSource = PSession.User.Dealer;
            ddlDealer.DataBind();

            ddlDealer.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void lblDeactivate_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblUserMobileID = (Label)gvRow.FindControl("lblUserMobileID");

            

            string endPoint = "User/UserMobileInActive?UserMobileID=" + lblUserMobileID.Text;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            if (Results.Status == PApplication.Failure)
            { 
                
            }
        }

        protected void lblActiveOrIncative_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblUserMobileID = (Label)gvRow.FindControl("lblUserMobileID");
            LinkButton lblActiveOrIncative = (LinkButton)gvRow.FindControl("lblActiveOrIncative");

            Boolean IsActive = false; 
            if(lblActiveOrIncative.Text == "Activate")
            {
                IsActive = true;
            }
            string endPoint = "User/UserMobileActiveOrInActive?UserMobileID= " + Convert.ToInt32(lblUserMobileID.Text) + "&IsActive=" + IsActive;
            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));
            lblMessage.Visible = true;
            if (Results.Status == PApplication.Failure)
            {
                lblMessage.Text = "Your request not processed.";
                lblMessage.ForeColor = Color.Red;
            }
            else
            {
                lblMessage.Text = "Your request successfully processed.";
                lblMessage.ForeColor = Color.Green;
                fillUser();
            }
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime traceStartTime = DateTime.Now;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lblActiveOrIncative = (LinkButton)e.Row.FindControl("lblActiveOrIncative");              

                    if (lblActiveOrIncative.Text == "True")
                    {
                        lblActiveOrIncative.Text = "Deactivate";
                    }
                    else
                    {
                        lblActiveOrIncative.Text = "Activate";
                    }
                }
                TraceLogger.Log(traceStartTime);
            }
            catch (Exception ex)
            {

            }
        }
    }
}