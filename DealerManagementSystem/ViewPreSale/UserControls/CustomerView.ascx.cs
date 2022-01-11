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

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class CustomerView : System.Web.UI.UserControl
    { 
        public long CustomerID
        {
            get
            {
                if (Session["CustomerID"] == null)
                {
                    Session["CustomerID"] = 0;
                }
                return Convert.ToInt64(Session["CustomerID"]);
            }
            set
            {
                Session["CustomerID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        public void fillCustomer(long CustomerID)
        {

            Session["CustomerID"] = CustomerID;
            PDMS_Customer Customer = new PDMS_Customer();
            Customer = new BDMS_Customer().GetCustomerProspect(CustomerID, "", "", null, null, null, null)[0];

            lblCustomer.Text = (Customer.CustomerCode + " " + Customer.CustomerName).Trim();
            lblContactPerson.Text = Customer.ContactPerson;
            lblMobile.Text = Customer.Mobile;
            lblAlternativeMobile.Text = Customer.AlternativeMobile;
            lblEmail.Text = Customer.Email;
            lblGSTIN.Text = Customer.GSTIN;
            lblPAN.Text = Customer.PAN;

            string Location = Customer.Address1 + ", " + Customer.Address2 + ", " + Customer.District.District + ", " + Customer.State.State;
            lblLocation.Text = Location;

            fillMarketSegment(CustomerID);
            fillRelation(CustomerID);
            fillProduct(CustomerID);
        }

        protected void ibtnMarketSegmentDelete_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ibtnProductDelete_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ibtnRelationDelete_Click(object sender, ImageClickEventArgs e)
        {

        }


        protected void btnAddMarketSegment_Click(object sender, EventArgs e)
        {
            new DDLBind(ddlMarketSegment, new BDMS_Master().GetMarketSegment(null, null), "MarketSegment", "MarketSegmentID");
            MPE_MarketSegment.Show();
        }
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            new DDLBind(ddlMake, new BDMS_Master().GetMake(null, null), "Make", "MakeID");
            new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
            new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null), "Product", "ProductID");
            MPE_Product.Show();
        }
        protected void btnAddRelations_Click(object sender, EventArgs e)
        {
            new DDLBind(ddlRelation, new BDMS_Master().GetRelation(null, null), "Relation", "RelationID");
            MPE_Relation.Show();
        }



        protected void btnSaveMarketSegment_Click(object sender, EventArgs e)
        {
            string Message = ValidationMarketSegment();
            if (!string.IsNullOrEmpty(Message))
            {
                //  lblMessage.Text = Message;
                return;
            }
            PCustomerMarketSegment MarketSegment = new PCustomerMarketSegment();
            MarketSegment.CustomerID = CustomerID;
            MarketSegment.MarketSegment = new PMarketSegment() { MarketSegmentID = Convert.ToInt32(ddlMarketSegment.SelectedValue) };
            MarketSegment.Remark = txtRemark.Text.Trim();
            MarketSegment.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/MarketSegment", MarketSegment)).Data);
            fillMarketSegment(CustomerID);
        }
        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            string Message = ValidationMarketSegment();
            if (!string.IsNullOrEmpty(Message))
            {
                //  lblMessage.Text = Message;
                return;
            }
            PCustomerProduct Product = new PCustomerProduct();
            Product.CustomerrProductID = 0;
            Product.CustomerID = CustomerID;
            Product.Make = new PMake() { MakeID = Convert.ToInt32(ddlMake.SelectedValue) };
            Product.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
            Product.Product = new PProduct() { ProductID = Convert.ToInt32(ddlProduct.SelectedValue) };
            Product.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            Product.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Product", Product)).Data);
            fillProduct(CustomerID);
        }
        protected void btnSaveRelation_Click(object sender, EventArgs e)
        {
            string Message = ValidationMarketSegment();
            if (!string.IsNullOrEmpty(Message))
            {
                //  lblMessage.Text = Message;
                return;
            }
            PCustomerRelation Relation = new PCustomerRelation();
            Relation.CustomerID = CustomerID;
            Relation.ContactName = txtPersonName.Text.Trim();
            Relation.Mobile = txtMobile.Text.Trim();
            Relation.Relation = new PRelation() { RelationID = Convert.ToInt32(ddlRelation.SelectedValue) };
            Relation.DOB = string.IsNullOrEmpty(txtBirthDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtBirthDate.Text.Trim());
            Relation.DOAnniversary = string.IsNullOrEmpty(txtAnniversaryDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAnniversaryDate.Text.Trim());
            Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Relation", Relation)).Data);
            fillRelation(CustomerID);
        }
         
        string ValidationMarketSegment()
        {
            return "";
        }

        void fillMarketSegment(long CustomerID)
        {
            gvMarketSegment.DataSource = new BDMS_Customer().GetCustomerMarketSegment(CustomerID, null);
            gvMarketSegment.DataBind();
        }
        void fillProduct(long CustomerID)
        {
            gvProduct.DataSource = new BDMS_Customer().GetCustomerProduct(CustomerID, null, null, null, null);
            gvProduct.DataBind();
        }
        void fillRelation(long CustomerID)
        {
            gvRelation.DataSource = new BDMS_Customer().GetCustomerRelation(CustomerID, null);
            gvRelation.DataBind();
        }

        protected void btnEditCustomer_Click(object sender, EventArgs e)
        {
            string Message = UC_Customer.ValidationCustomer();
            lblMessageCustomerEdit.ForeColor = Color.Red;
            lblMessageCustomerEdit.Visible = true;
            //MPE_Customer.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageCustomerEdit.Text = Message;
                return;
            } 
            PDMS_Customer Customer = UC_Customer.ReadCustomer();
            Customer.CustomerID = CustomerID;
            Customer.CreatedBy = new PUser { UserID = PSession.User.UserID };
            string result = new BAPI().ApiPut("Customer/CustomerProspect", Customer);
            fillCustomer(CustomerID);
        }

        protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (ddlAction.Text == "Edit Customer")
            {
                MPE_Customer.Show();
                PDMS_Customer Customer = new PDMS_Customer();
                Customer = new BDMS_Customer().GetCustomerProspect(CustomerID, "", "", null, null, null, null)[0]; 
                UC_Customer.FillMaster();
                UC_Customer.FillCustomer(Customer);
            }
            else if (ddlAction.Text == "Add Expense")
            { 
            }
        }
    }
}