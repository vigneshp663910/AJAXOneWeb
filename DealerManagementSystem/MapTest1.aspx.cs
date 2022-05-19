using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class MapTest1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConvertDataTabletoString();
        }
        public string ConvertDataTabletoString()
        {


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            row = new Dictionary<string, object>();
            row.Add("title", "1");
            row.Add("lat", "12.897400");
            row.Add("lng", "80.288000");
            row.Add("description", "1");
            rows.Add(row);

            row = new Dictionary<string, object>();
            row.Add("title", "2");
            row.Add("lat", "12.997450");
            row.Add("lng", "80.298050");
            row.Add("description", "2");

            rows.Add(row);

            return serializer.Serialize(rows);

        }
    }
}