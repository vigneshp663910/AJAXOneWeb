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

namespace DealerManagementSystem.View
{
    public partial class Attendance : System.Web.UI.Page
    {
        public DataTable Attendance1
        {
            get
            {
                if (Session["Attendance1"] == null)
                {
                    Session["Attendance1"] = new BAttendance();
                }
                return (DataTable)Session["Attendance1"];
            }
            set
            {
                Session["Attendance1"] = value;
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
                txtDateFrom.Text = DateTime.Now.AddDays(1 + (-1 * DateTime.Now.Day)).ToString("yyyy-MM-dd");
                txtDateTo.Text = DateTime.Now.ToString("yyyy-MM-dd");

                if (PSession.User.Designation.DealerDesignationID == 4 || PSession.User.Designation.DealerDesignationID == 6 || PSession.User.Designation.DealerDesignationID == 8)
                {
                    PDealer Dealer = new BDealer().GetDealerList(null, PSession.User.ExternalReferenceID, "")[0];
                    ddlDealer.Items.Add(new ListItem(PSession.User.ExternalReferenceID, Dealer.DID.ToString()));
                    ddlDealer.Enabled = false;
                    lblEmployee.Visible = false;
                    ddlUser.Visible = false;
                    //ddlDealer.Enabled = false;
                    
                }
                else
                {
                    new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                    lblEmployee.Visible = true;
                    ddlUser.Visible = true;
                    ddlDealer.Enabled = true;
                }

                Attendance1 = new BAttendance().GetAttendance(DateTime.Now, DateTime.Now,null, PSession.User.UserID);
                btnPunch.Text = "Punch In";
                if ((Attendance1.Rows.Count > 0) && (Attendance1.Rows[0]["PunchOut"] == DBNull.Value))
                {
                    btnPunch.Text = "Punch Out";
                }
                else if(Attendance1.Rows.Count == 0)
                {
                    btnPunch.Text = "Punch In";
                }
                else
                {
                    btnPunch.Visible = false;
                }


            }
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDealerEmployee();
        }

        protected void FillDealerEmployee()
        {

            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);

            new DDLBind(ddlUser, DealerUser, "ContactName", "UserID");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            FillAttendance();
        }

        private void FillAttendance()
        {
            DateTime DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim());
            DateTime DateTo = Convert.ToDateTime(txtDateTo.Text.Trim());
            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?) null : Convert.ToInt32(ddlDealer.SelectedValue);
            Attendance1 = new BAttendance().GetAttendance(DateFrom, DateTo, DealerID, PSession.User.UserID);

            gvAttendance.DataSource = Attendance1;
            gvAttendance.DataBind();


            if (Attendance1.Rows.Count == 0)
            {
                lblRowCountAttendance.Visible = false;
                ibtnAttendanceArrowLeft.Visible = false;
                ibtnAttendanceArrowRight.Visible = false;
            }
            else
            {
                lblRowCountAttendance.Visible = true;
                ibtnAttendanceArrowLeft.Visible = true;
                ibtnAttendanceArrowRight.Visible = true;
                lblRowCountAttendance.Text = (((gvAttendance.PageIndex) * gvAttendance.PageSize) + 1) + " - " + (((gvAttendance.PageIndex) * gvAttendance.PageSize) + gvAttendance.Rows.Count) + " of " + Attendance1.Rows.Count;
            }

        }

        protected void btnPunch_Click(object sender, EventArgs e)
        {
            Boolean Success = true;
            Success = new BAttendance().InsertOrUpdateAttendance(PSession.User.UserID);
            if(Success)
            {
                lblMessage.Text = "Attendance punched successfully.";
                lblMessage.ForeColor = Color.Green;
                FillAttendance();
                Attendance1 = new BAttendance().GetAttendance(DateTime.Now, DateTime.Now, null, PSession.User.UserID);
                btnPunch.Text = "Punch In";
                if ((Attendance1.Rows.Count > 0) && (Attendance1.Rows[0]["PunchOut"] == DBNull.Value))
                {
                    btnPunch.Text = "Punch Out";
                }
                else if (Attendance1.Rows.Count == 0)
                {
                    btnPunch.Text = "Punch In";
                }
                else
                {
                    btnPunch.Visible = false;
                }
            }
            else if(!Success)
            {
                lblMessage.Text = "Attendance not punched.";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void ibtnAttendanceArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvAttendance.PageIndex > 0)
            {
                gvAttendance.PageIndex = gvAttendance.PageIndex - 1;
                AttendanceBind(gvAttendance, lblRowCountAttendance, Attendance1);
            }

        }        

        protected void ibtnAttendanceArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvAttendance.PageCount > gvAttendance.PageIndex)
            {
                gvAttendance.PageIndex = gvAttendance.PageIndex + 1;
                AttendanceBind(gvAttendance, lblRowCountAttendance, Attendance1);
            }

        }

        void AttendanceBind(GridView gv, Label lbl, DataTable CustomerCH)
        {
            gv.DataSource = CustomerCH;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + CustomerCH.Rows.Count;
        }

        protected void gvAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAttendance.PageIndex = e.NewPageIndex;
            FillAttendance();
        }
    }
}