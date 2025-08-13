
using Newtonsoft.Json;
using Properties; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Business
{
    public class BDMS_Division
    { 
        public void GetDivisionForSerchGroped(DropDownList ddl)
        {
            try
            {
                ddl.DataTextField = "DivisionCode";
                ddl.DataValueField = "DivisionID";
                ddl.Items.Insert(0, new ListItem("All", "0"));
                ddl.Items.Insert(1, new ListItem("CM", "2"));
                ddl.Items.Insert(2, new ListItem("BP", "1"));
                ddl.Items.Insert(3, new ListItem("CP", "3"));
                ddl.Items.Insert(4, new ListItem("BP,TM", "1,11"));
                ddl.Items.Insert(5, new ListItem("CP,BP,TM,PS,DP", "3,1,11,14,4"));
                ddl.Items.Insert(6, new ListItem("SB", "19"));

                //ddl.Items.Insert(3, new ListItem("SP", "15")); 
            }
            catch (SqlException sqlEx)
            { }
            catch (Exception ex)
            { }
        }
        
    }
}