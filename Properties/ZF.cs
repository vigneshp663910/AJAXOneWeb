using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Properties
{
    public class ZF
    {
        public string GetStatus(ListBox lbStatus, string FieldName)
        {
            string Status = "";
            Boolean check = false;
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    if (li.Text.ToUpper() == "ALL")
                    {
                        check = false;
                        break;
                    }
                    Status = Status + ",'" + li.Text + "'";
                    check = true;
                }
            }
            Status = Status.Trim(',');
            Status = FieldName + " in (" + Status + ")";
            if (check == true)
                return Status;
            else
                return "";
        }
        public string GetStatusVaues(ListBox lbStatus, string FieldName)
        {
            string Status = "";
            Boolean check = false;
            foreach (ListItem li in lbStatus.Items)
            {
                if (li.Selected)
                {
                    if (li.Text.ToUpper() == "ALL")
                    {
                        check = false;
                        break;
                    }
                    Status = Status + "," + li.Value ;
                    check = true;
                }
            }
            Status = Status.Trim(','); 
            if (check == true)
                return Status;
            else
                return "";
        }
    }
}
