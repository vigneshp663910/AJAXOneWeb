using Business;
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
    public partial class UserMobileApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Admin » Approve Mobile User');</script>");
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                fillUser();
            }
        }
        protected void lblApprove_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int UserMobileID = Convert.ToInt32(gvUserIMEI.DataKeys[gvRow.RowIndex].Value);

                PApiResult Result = new BUser().ApproveUserMobile(UserMobileID);
                if (Result.Status == PApplication.Failure)
                {
                    lblMessage.Text = "not Approved";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Text = "Approved";
                lblMessage.ForeColor = Color.Green;
                fillUser();
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        protected void lblReject_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int UserMobileID = Convert.ToInt32(gvUserIMEI.DataKeys[gvRow.RowIndex].Value);
                TextBox txtRemarks = (TextBox)gvUserIMEI.Rows[gvRow.RowIndex].FindControl("txtRemarks");
                bool Success = false;
                Success = new BUser().RejectUserMobile(UserMobileID, txtRemarks.Text);
                //PApiResult Result = new BUser().RejectUserMobile(UserMobileID, txtRemarks.Text);
                //if (Result.Status == PApplication.Failure)
                //{
                //    lblMessage.Text = "not Rejected";
                //    lblMessage.ForeColor = Color.Red;
                //}
                if (Success == true)
                {
                    lblMessage.Text = "Rejected";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Not Rejected";
                    lblMessage.ForeColor = Color.Red;
                }
                fillUser();
                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
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
            if (gvUserIMEI.PageIndex > 0)
            {
                gvUserIMEI.PageIndex = gvUserIMEI.PageIndex - 1;
                UserBind(gvUserIMEI, lblRowCount, UserLst);
            }
        }
        protected void ibtnUserArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvUserIMEI.PageCount > gvUserIMEI.PageIndex)
            {
                gvUserIMEI.PageIndex = gvUserIMEI.PageIndex + 1;
                UserBind(gvUserIMEI, lblRowCount, UserLst);
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
                //gvUserIMEI.DataSource = new BUser().GetUserMobileForApproval();
                //gvUserIMEI.DataBind();


                UserLst = new BUser().GetUserMobileForApproval();
                gvUserIMEI.DataSource = UserLst;
                gvUserIMEI.DataBind();


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
                    lblRowCount.Text = (((gvUserIMEI.PageIndex) * gvUserIMEI.PageSize) + 1) + " - " + (((gvUserIMEI.PageIndex) * gvUserIMEI.PageSize) + gvUserIMEI.Rows.Count) + " of " + UserLst.Count;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
    }
}