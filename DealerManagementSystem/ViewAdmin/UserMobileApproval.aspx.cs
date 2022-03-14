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
            lblMessage.Visible = false;
            if (!IsPostBack)
            {
                fillUser();
            }
        }
        protected void lblApprove_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int UserMobileID = Convert.ToInt32(gvUserIMEI.DataKeys[gvRow.RowIndex].Value);

            PApiResult Result=    new BUser().ApproveUserMobile(UserMobileID);
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
        void fillUser()
        {
            try
            { 
                gvUserIMEI.DataSource = new BUser().GetUserMobileForApproval();
                gvUserIMEI.DataBind();
            }
            catch (Exception ex)
            { 
            }
        }
    }
}