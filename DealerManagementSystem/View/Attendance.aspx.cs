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

        public string CurrentLocation
        {
            get
            {
                if (Session["PreSalesReport"] == null)
                {
                    Session["PreSalesReport"] = "";
                }
                return (string)Session["PreSalesReport"];
            }
            set
            {
                Session["PreSalesReport"] = value;
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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Activity » Attendance');</script>");

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
        public string ConvertDataTabletoString()
        {
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            //Dictionary<string, object> row;

            //row = new Dictionary<string, object>();
            //row.Add("title", "1");
            //row.Add("lat", "12.897400");
            //row.Add("lng", "80.288000");
            //row.Add("description", "1");
            //rows.Add(row);

            //row = new Dictionary<string, object>();
            //row.Add("title", "2");
            //row.Add("lat", "12.997450");
            //row.Add("lng", "80.298050");
            //row.Add("description", "2");

            //rows.Add(row);

            //return serializer.Serialize(rows);

            return CurrentLocation;

        }
        protected void btnPunch_Click(object sender, EventArgs e)
        {
            Boolean Success = true;

            if (string.IsNullOrEmpty(hfLatitude.Value) || string.IsNullOrEmpty(hfLongitude.Value))
            {
                lblMessage.Text = "Please Enable GeoLocation!";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                return;
            }
            decimal Latitude = Convert.ToDecimal(hfLatitude.Value);
            decimal Longitude = Convert.ToDecimal(hfLongitude.Value);
            Success = new BAttendance().InsertOrUpdateAttendance(PSession.User.UserID, Latitude, Longitude);
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

        protected void btnTrackAttendance_Click(object sender, EventArgs e)
        {
            MPE_TrackAttendance.Show();

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            int? DealerID = ddlDealer.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealer.SelectedValue);
            Attendance1 = new BAttendance().GetAttendance(null, null, DealerID, PSession.User.UserID);

            foreach (DataRow dr in Attendance1.Rows)
            {
                row = new Dictionary<string, object>();

                ////String url = "http://maps.google.com/maps/api/geocode/xml?address=" + Convert.ToString(dr["GeoLocation"]) + "&sensor=false"; 
                //String url = "https://maps.google.com/maps/api/geocode/xml?address=" + Convert.ToString(dr["GeoLocation"]) + "&key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI";
                //WebRequest request = WebRequest.Create(url);
                //string Latitude = "0";
                //string Longitude = "0";
                //using (WebResponse response = (HttpWebResponse)request.GetResponse())
                //{
                //    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                //    {

                //        DataSet dsResult = new DataSet();
                //        dsResult.ReadXml(reader);
                //        DataTable dtCoordinates = new DataTable();
                //        foreach (DataRow row1 in dsResult.Tables["result"].Rows)
                //        {
                //            string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                //            DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                //            dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);
                //            Latitude = Convert.ToString(location["lat"]);
                //            Longitude = Convert.ToString(location["lng"]);
                //        }
                //    }
                //}

                //row.Add("GeoLocation", Convert.ToString(dr["GeoLocation"]));
                //row.Add("title", Convert.ToString(dr["Name"]));
                //row.Add("lat", Latitude);
                //row.Add("lng", Longitude);
                row.Add("lat", Convert.ToString(dr["StartLatitude"]));
                row.Add("lng", Convert.ToString(dr["StartLongitude"]));
                row.Add("description", Convert.ToString(dr["StartLatitudeLongitudeDate"]));
                row.Add("image", Convert.ToString(dr["StartMapImage"]));
                rows.Add(row);


                if (DBNull.Value != dr["EndLatitude"])
                {
                    row = new Dictionary<string, object>();
                    row.Add("lat", Convert.ToString(dr["EndLatitude"]));
                    row.Add("lng", Convert.ToString(dr["EndLongitude"]));
                    row.Add("description", Convert.ToString(dr["EndLatitudeLongitudeDate"]));
                    row.Add("image", Convert.ToString(dr["EndMapImage"]));
                    rows.Add(row);
                }

            }
            CurrentLocation = serializer.Serialize(rows);
        }
    }
}