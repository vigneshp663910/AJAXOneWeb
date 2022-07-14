using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewMaster.UserControls
{
    public partial class CustomerVerification : System.Web.UI.UserControl
    {
        public PDMS_Customer Customer
        {
            get
            {
                if (Session["CustomerView"] == null)
                {
                    Session["CustomerView"] = new PDMS_Customer();
                }
                return (PDMS_Customer)Session["CustomerView"];
            }
            set
            {
                Session["CustomerView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        { 
        }
        public void FillMaster()
        {
            List<PDMS_Customer> cust = new List<PDMS_Customer>();
            cust.Add(Customer);
            gvCustomer.DataSource = cust;
            gvCustomer.DataBind();
            gvCustomerDuplicate.DataSource = new BDMS_Customer().CustomerForDuplicateVerificatio(Customer.CustomerID);
            gvCustomerDuplicate.DataBind();
        }

        protected void btnVerified_Click(object sender, EventArgs e)
        { 
 
            string endPoint = "Customer/UpdateCustomerVerified?CustomerID=" + Customer.CustomerID;
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
            if (Convert.ToBoolean(s) == true)
            {
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Green;
                pnlVerification.Visible = false;
            }
            else
            {
                lblMessage.Text = "Something went wrong try again.";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }

        protected void btnMergeCustomer_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerID = (Label)gvRow.FindControl("lblCustomerID");

            string endPoint = "Customer/UpdateCustomerVerified?CustomerID=" + Customer.CustomerID + "&MergeToCustomerID=" + lblCustomerID.Text;
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint));

            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
        }

        protected void gvCustomerDuplicate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerDuplicate.PageIndex = e.NewPageIndex;
            gvCustomerDuplicate.DataSource = new BDMS_Customer().CustomerForDuplicateVerificatio(Customer.CustomerID);
            gvCustomerDuplicate.DataBind();
        }
    }
}