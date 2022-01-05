using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;  

namespace Business
{
    public class DDLBind
    {
        public DDLBind(DropDownList ddl, object data, string DataTextField, string DataValueField, Boolean isSelect = true)
        {
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataSource = data;
            ddl.DataBind();
            if (isSelect)
                ddl.Items.Insert(0, new ListItem("Select", "0"));
        }
      
    }
}
