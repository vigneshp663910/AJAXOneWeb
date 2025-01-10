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
        public string SelectedValue { get { return GetSelectedValue(); } }
        public event EventHandler ButtonClicked;
        protected void Page_Load(object sender, EventArgs e)
        { 
        }
        public void Fill(string TextField, string ValueField, object data)
        {
            cbList.DataTextField = TextField;
            cbList.DataValueField = ValueField;
            cbList.DataSource = data;
            cbList.DataBind();
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

        protected void btnView_Click(object sender, ImageClickEventArgs e)
        {
            if (divMultiSelect.Visible)
            {
                divMultiSelect.Visible = false;
                btnView.ImageUrl = "~/Images/ArrowDown.png";
                lblDisplay.Text = "";
                for (int i = 0; i < cbList.Items.Count; i++)
                {
                    if (cbList.Items[i].Selected)
                    {
                        if(!string.IsNullOrEmpty(lblDisplay.Text))
                        {
                            lblDisplay.Text = "Selected Multiple Value";
                        }
                        else
                        {
                            lblDisplay.Text = cbList.Items[i].Text.ToString();
                        }
                    } 
                } 
            }
            else
            {
                divMultiSelect.Visible = true;
                btnView.ImageUrl = "~/Images/CloseIcon.jpg";
            }

            if (ButtonClicked != null)
            {
                ButtonClicked(this, EventArgs.Empty);
            }
        }

        string GetSelectedValue()
        {
            string li = "";
            for (int i = 0; i < cbList.Items.Count; i++)
            {
                if (cbList.Items[i].Selected)
                    li += cbList.Items[i].Value.ToString() + ",";
            }
           return li.Trim(',');
        }
    }
}