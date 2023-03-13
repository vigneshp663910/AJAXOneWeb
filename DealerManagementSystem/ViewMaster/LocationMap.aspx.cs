using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster
{
    public partial class LocationMap : BasePage
    {
        public override SubModule SubModuleName { get { return SubModule.ViewMaster_LocationMap; } }
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

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('Activities » Current Location');</script>");

            lblMessage.Text = "";

            if (!IsPostBack)
            {
                //new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                new DDLBind().FillDealerAndEngneer(ddlDealer, null);
                new BDMS_Dealer().GetDealerDepartmentDDL(ddlDepartment, null, null);
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

            int? UserID = null;
            int? DealerID = null;


            int? DealerDepartmentID = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue);
            if (ddlDealer.SelectedValue != "0")
            {
                DealerID = Convert.ToInt32(ddlDealer.SelectedValue);

                UserID = ddlEmployee.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlEmployee.SelectedValue);
            }

            DataTable dt = new BUser().GetUserLocationCurrent(DealerID, UserID, DealerDepartmentID, PSession.User.UserID);
            foreach (DataRow dr in dt.Rows)
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
                row.Add("lat", Convert.ToString(dr["Latitude"]));
                row.Add("lng", Convert.ToString(dr["Longitude"]));

                row.Add("description", Convert.ToString(dr["LatitudeLongitudeDate"]));

                row.Add("image", Convert.ToString(dr["MapImage"]));

                rows.Add(row);
            }
            CurrentLocation = serializer.Serialize(rows);
        }
        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);

            new DDLBind(ddlEmployee, DealerUser, "ContactName", "UserID");
        }
    }
}