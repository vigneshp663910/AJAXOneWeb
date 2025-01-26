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

namespace DealerManagementSystem.ViewSalesTouchPoint
{
    public partial class SalesTouchPointUser : System.Web.UI.Page
    {
        private int PageCount
        {
            get
            {
                if (ViewState["PageCount"] == null)
                {
                    ViewState["PageCount"] = 0;
                }
                return (int)ViewState["PageCount"];
            }
            set
            {
                ViewState["PageCount"] = value;
            }
        }
        private int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] == null)
                {
                    ViewState["PageIndex"] = 1;
                }
                return (int)ViewState["PageIndex"];
            }
            set
            {
                ViewState["PageIndex"] = value;
            }
        }
        public List<PSalesTouchPointUser> GetSalesTouchPointUserList
        {
            get
            {
                if (ViewState["GetSalesTouchPointUserList"] == null)
                {
                    ViewState["GetSalesTouchPointUserList"] = new List<PSalesTouchPointUser>();
                }
                return (List<PSalesTouchPointUser>)ViewState["GetSalesTouchPointUserList"];
            }
            set
            {
                ViewState["GetSalesTouchPointUserList"] = value;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession_STP.SalesTouchPointUser == null)
            {
                Response.Redirect(UIHelper.RedirectToSalesTouchPointLogin);
            }
        }
        string ContactNo;
        string EmailID;
        string Name;

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Task » Measurement');</script>");
            lblMessage.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    PageCount = 0;
                    PageIndex = 1;
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message.ToString();
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                PageIndex = 1;
                Fill();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        void fillter()
        {
            ContactNo = string.IsNullOrEmpty(txtContactNo.Text.Trim()) ? null : txtContactNo.Text.Trim();
            EmailID = string.IsNullOrEmpty(txtEmailId.Text.Trim()) ? null : txtEmailId.Text.Trim();
            Name = string.IsNullOrEmpty(txtName.Text.Trim()) ? null : txtName.Text.Trim();
        }
        void Fill()
        {
            lblMessage.ForeColor = Color.Red;
            try
            {
                fillter();
                PApiResult Result = new PApiResult();
                Result = new BSalesTouchPoint().GetSalesTouchPointUserList(txtContactNo.Text.Trim(), txtEmailId.Text, txtName.Text, PageIndex, gvUser.PageSize);
                GetSalesTouchPointUserList = JsonConvert.DeserializeObject<List<PSalesTouchPointUser>>(JsonConvert.SerializeObject(Result.Data));
                //GetSalesTouchPointUserList = new BSalesTouchPointUser().GetSalesTouchPointUserList(txtContactNo.Text.Trim(), txtEmailId.Text, txtName.Text, PageIndex, gvUser.PageSize);

                gvUser.DataSource = GetSalesTouchPointUserList;
                gvUser.DataBind();

                if (Result.RowCount == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    PageCount = (Result.RowCount + gvUser.PageSize - 1) / gvUser.PageSize;
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((PageIndex - 1) * gvUser.PageSize) + 1) + " - " + (((PageIndex - 1) * gvUser.PageSize) + gvUser.Rows.Count) + " of " + Result.RowCount;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.ToString();
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex = PageIndex - 1;
                Fill();
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (PageCount > PageIndex)
            {
                PageIndex = PageIndex + 1;
                Fill();
            }
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            Button btnLock = (Button)sender;
            Int32 SalesTouchPointUserID = Convert.ToInt32(btnLock.CommandArgument);
            PSalesTouchPointUser_Insert UpdateUser = FillUser(SalesTouchPointUserID);
            UpdateUser.ModifiedBy = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID;
            UpdateUser.IsLocked = (UpdateUser.IsLocked == true) ? false : true;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
            Fill();
        }

        protected void gvUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnLock = (Button)e.Row.FindControl("btnLock");
                Label lblLock = (Label)e.Row.FindControl("lblLock");
                LinkButton lnkActive = (LinkButton)e.Row.FindControl("lnkActive");
                Label lblActive = (Label)e.Row.FindControl("lblActive");
                
                if (lblLock.Text == "True")
                {
                    btnLock.Text = "UnLock";
                }
                else
                {
                    btnLock.Text = "Lock";
                }
                if (lblActive.Text == "True")
                {
                    lnkActive.CssClass = "fa fa-fw fa-toggle-on";
                }
                else
                {
                    lnkActive.CssClass = "fa fa-fw fa-toggle-off";
                }
            }
        }
        private PSalesTouchPointUser_Insert FillUser(int SalesTouchPointUserID)
        {
            PSalesTouchPointUser_Insert UpdateUser = new PSalesTouchPointUser_Insert();
            try
            {
                PSalesTouchPointUser User = new BSalesTouchPoint().GetSalesTouchPointUserByID(SalesTouchPointUserID);
                UpdateUser.SalesTouchPointUserID = User.SalesTouchPointUserID;
                UpdateUser.AadharNumber = User.AadharNumber;
                UpdateUser.Name = User.Name;
                UpdateUser.ContactNumber = User.ContactNumber;
                UpdateUser.EmailID = User.EmailID;
                UpdateUser.Password = User.Password;
                UpdateUser.Address1 = User.Address1;
                UpdateUser.Address2 = User.Address2;
                UpdateUser.IsActive = User.IsActive;
                UpdateUser.IsLocked = User.IsLocked;
                UpdateUser.IsEnabled = User.IsEnabled;
                UpdateUser.StateID = User.State.StateID;
                UpdateUser.DistrictID = User.District.DistrictID;
                UpdateUser.PasswordExpirationDate = User.PasswordExpirationDate;
                UpdateUser.Latitude = User.Latitude;
                UpdateUser.Longitude = User.Longitude;
                UpdateUser.LatitudeLongitudeDate = User.LatitudeLongitudeDate;
                UpdateUser.LoginCount = User.LoginCount;
                UpdateUser.ManualLockDate = User.ManualLockDate;
                UpdateUser.OTP = User.OTP;
                UpdateUser.OTPExpiry = User.OTPExpiry;
                return UpdateUser;
            }
            catch (Exception ex)
            {
                
            }
            return UpdateUser;
        }

        protected void lnkActive_Click(object sender, EventArgs e)
        {
            LinkButton lnkActive = (LinkButton)sender;
            Int32 SalesTouchPointUserID = Convert.ToInt32(lnkActive.CommandArgument);
            PSalesTouchPointUser_Insert UpdateUser = FillUser(SalesTouchPointUserID);
            UpdateUser.ModifiedBy = PSession_STP.SalesTouchPointUser.SalesTouchPointUserID;
            UpdateUser.IsActive = (UpdateUser.IsActive == true) ? false : true;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointUser", UpdateUser));
            Fill();            
        }
    }
}