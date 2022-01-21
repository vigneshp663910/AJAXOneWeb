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
            Customer = new BDMS_Customer().GetCustomer(CustomerID, "", "", null, null, null, null)[0];

            lblCustomer.Text = (Customer.CustomerCode + " " + Customer.CustomerName).Trim();
            lblContactPerson.Text = Customer.ContactPerson;
            lblMobile.Text = Customer.Mobile;
            lblAlternativeMobile.Text = Customer.AlternativeMobile;
            lblEmail.Text = Customer.Email;
            lblGSTIN.Text = Customer.GSTIN;
            lblPAN.Text = Customer.PAN;

            string Location = Customer.Address1 + ", " + Customer.Address2 + ", " + Customer.District.District + ", " + Customer.State.State;
            lblLocation.Text = Location;

            fillAttribute(CustomerID);
            fillRelation(CustomerID);
            fillProduct(CustomerID);
        }

     

        protected void btnSaveMarketSegment_Click(object sender, EventArgs e)
        {
            string Message = ValidationMarketSegment();
            if (!string.IsNullOrEmpty(Message))
            {
                //  lblMessage.Text = Message;
                return;
            }
            PCustomerAttribute Attribute = new PCustomerAttribute();
            Attribute.CustomerID = CustomerID;
            Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = Convert.ToInt32(ddlAttributeMain.SelectedValue) };
            Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = Convert.ToInt32(ddlAttributeSub.SelectedValue) };
            Attribute.Remark = txtRemark.Text.Trim();
            Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute)).Data);
            fillAttribute(CustomerID);
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

        void fillAttribute(long CustomerID)
        {
            gvAttribute.DataSource = new BDMS_Customer().GetCustomerAttribute(CustomerID, null);
            gvAttribute.DataBind();
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

        protected void btnUpdateCustomer_Click(object sender, EventArgs e)
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
            string result = new BAPI().ApiPut("Customer/Customer", Customer);
            fillCustomer(CustomerID);
        } 

        protected void lbMarketSegmentDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerAttributeID = (Label)gvRow.FindControl("lblCustomerAttributeID");
            PCustomerAttribute Attribute = new PCustomerAttribute();
            Attribute.CustomerAttributeID = Convert.ToInt64(lblCustomerAttributeID.Text);
            Attribute.CustomerID = CustomerID;  
             
            Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = 0 };
            Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = 0};
            Attribute.Remark = txtRemark.Text.Trim();
            Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };


            Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute)).Data);
            fillAttribute(CustomerID);
        }

        protected void lbProductDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerrProductID = (Label)gvRow.FindControl("lblCustomerrProductID");
            PCustomerProduct Product = new PCustomerProduct();
            Product.CustomerrProductID = Convert.ToInt64(lblCustomerrProductID.Text);
            Product.CustomerID = CustomerID;
            Product.Make = new PMake() { MakeID = 0 };
            Product.ProductType = new PProductType() { ProductTypeID = 0 };
            Product.Product = new PProduct() { ProductID = 0 };
            Product.Quantity = 0;
            Product.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Product", Product)).Data);
            fillProduct(CustomerID);
        }

        protected void lbRelationDelete_Click(object sender, EventArgs e)
        { 
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerRelationID = (Label)gvRow.FindControl("lblCustomerRelationID");
            PCustomerRelation Relation = new PCustomerRelation();
            Relation.CustomerRelationID = Convert.ToInt64(lblCustomerRelationID.Text);
            Relation.CustomerID = CustomerID;
            Relation.Relation = new PRelation() { RelationID = 0 };
            Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Relation", Relation)).Data);
            fillRelation(CustomerID);
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Customer")
            {
                MPE_Customer.Show();
                PDMS_Customer Customer = new PDMS_Customer();
                Customer = new BDMS_Customer().GetCustomer(CustomerID, "", "", null, null, null, null)[0];
                UC_Customer.FillMaster();
                UC_Customer.FillCustomer(Customer);
            }
            else if (lbActions.Text == "Add Attribute")
            {
                new DDLBind(ddlAttributeMain, new BDMS_Customer().GetCustomerAttributeMain(null, null), "AttributeMain", "AttributeMainID");    
                MPE_MarketSegment.Show();
            }
            else if (lbActions.Text == "Add Product")
            {
                new DDLBind(ddlMake, new BDMS_Master().GetMake(null, null), "Make", "MakeID");
                new DDLBind(ddlProductType, new BDMS_Master().GetProductType(null, null), "ProductType", "ProductTypeID");
                new DDLBind(ddlProduct, new BDMS_Master().GetProduct(null, null), "Product", "ProductID");
                MPE_Product.Show();
            }
            else if (lbActions.Text == "Add Relation")
            {
                new DDLBind(ddlRelation, new BDMS_Master().GetRelation(null, null), "Relation", "RelationID");
                MPE_Relation.Show();
            } 
        }

        protected void ddlAttributeMain_SelectedIndexChanged(object sender, EventArgs e)
        { 
            new DDLBind(ddlAttributeSub, new BDMS_Customer().GetCustomerAttributeSub(null,Convert.ToInt32(ddlAttributeMain.SelectedValue), null), "AttributeSub", "AttributeSubID");
            MPE_MarketSegment.Show();
        }
    }
}