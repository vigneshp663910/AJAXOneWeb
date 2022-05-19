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
    public partial class LocationMap : System.Web.UI.Page
    {
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

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle(' ');</script>");

            lblMessage.Text = "";

            if (!IsPostBack)
            { 
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            DataTable dt = new BUser().GetUserLocationCurrent();
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                row.Add("title", Convert.ToString(dr["Name"]));
                row.Add("lat", Convert.ToString(dr["Latitude"]));
                row.Add("lng", Convert.ToString(dr["Longitude"]));
                row.Add("description", Convert.ToString(dr["LatitudeLongitudeDate"]));
                rows.Add(row);
            } 
            CurrentLocation = serializer.Serialize(rows);
        }

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null,null);

            new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");
        }
    }
}