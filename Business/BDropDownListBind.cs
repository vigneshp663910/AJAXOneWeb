using Properties;
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
        public DDLBind()
        { 
        }
        public DDLBind(DropDownList ddl, object data, string DataTextField, string DataValueField, Boolean isSelect = true, string Select = "Select")
        {
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataSource = data;
            ddl.DataBind();
            if (isSelect)
                ddl.Items.Insert(0, new ListItem(Select, "0"));
        }

        public void Year(DropDownList ddl, int StarYear, Boolean isSelect = true, string Select = "Select")
        {
            if (isSelect)
                ddl.Items.Insert(0, new ListItem(Select, "0"));
            for (int i = StarYear; i <= DateTime.Now.Year; i++)
            {
                ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            
        }
        public void Month(DropDownList ddl, Boolean isSelect = true, string Select = "Select")
        {
            if (isSelect)
                ddl.Items.Insert(0, new ListItem(Select, "0"));
            for (int i = 1; i <= 12; i++)
            {
                DateTime date = new DateTime(2020, i, 1);
                ddl.Items.Add(new ListItem(date.ToString("MMMM"), i.ToString()));
            } 
        }

        public void FillDealerAndEngneer(DropDownList ddlDealer, DropDownList ddlEngneer)
        {
            int DesignationID = PSession.User.Designation.DealerDesignationID;
            if ((DesignationID == (short)DealerDesignation.SalesExecutive) || (DesignationID == (short)DealerDesignation.ServiceTechnician))
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID", false);
                List<PUser> DealerUser = new List<PUser>();
                DealerUser.Add(PSession.User);
                new DDLBind(ddlEngneer, DealerUser, "ContactName", "UserID", false);
            }
            else
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                List<PUser> DealerUser = new BUser().GetUsers(null, null, null, null, Convert.ToInt32(ddlDealer.SelectedValue), true, null, null, null);
                new DDLBind(ddlEngneer, DealerUser, "ContactName", "UserID");
            }
        }
    }
}
