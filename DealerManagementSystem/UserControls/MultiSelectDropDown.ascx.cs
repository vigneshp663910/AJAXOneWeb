using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class MultiSelectDropDown : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        void fill(string TextField, string ValueField, object data)
        {
            cbList.DataTextField = TextField;
            cbList.DataValueField = ValueField;
            cbList.DataSource = data;
            cbList.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

        }

        protected void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string li = "";
            for (int i = 0; i < cbList.Items.Count; i++)
            {
                if (cbList.Items[i].Selected)
                    li += cbList.Items[i].Value.ToString() + ",";
            }
            txtbox.Text = li.Trim(',');
        }
    }
}