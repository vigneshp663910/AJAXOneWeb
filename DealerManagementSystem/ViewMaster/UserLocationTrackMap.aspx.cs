using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class UserLocationTrackMap : System.Web.UI.Page
    {
        public string  Location
        {
            get
            {
                if (Session["EmpLocationTrack"] == null)
                {
                    Session["EmpLocationTrack"] = "";
                }
                return (string)Session["EmpLocationTrack"];
            }
            set
            {
                Session["EmpLocationTrack"] = value;
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

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle(' ');</script>");

            lblMessage.Text = "";

            if (!IsPostBack)
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                //new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
            }
        }
        public string ConvertDataTabletoString()
        { 
            return Location;

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

           
            int? DealerID = null;


            //int? DealerDepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue);

                
            }

            int UserID = Convert.ToInt32(ddlEmployee.SelectedValue);  
            DataTable dt = new BUser().GetUserLocationTrack(UserID);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    row = new Dictionary<string, object>();
            //    row.Add("title", Convert.ToString(dr["Name"]));
            //    row.Add("lat", Convert.ToString(dr["Latitude"]));
            //    row.Add("lng", Convert.ToString(dr["Longitude"]));
            //    row.Add("description", Convert.ToString(dr["LatitudeLongitudeDate"])); 
            //    rows.Add(row);
            //}

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>(); 
                row.Add("lat", Convert.ToString(dr["Latitude"]));
                row.Add("lng", Convert.ToString(dr["Longitude"]));

                row.Add("description", Convert.ToString(dr["LatitudeLongitudeDate"]));

                row.Add("image", Convert.ToString(dr["MapImage"]));

                rows.Add(row);
            } 
            Location = serializer.Serialize(rows);
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null); 
            new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");
        }
    }
}