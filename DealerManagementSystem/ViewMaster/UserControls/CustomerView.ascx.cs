using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
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
            lblMessageAttribute.Text = "";
            lblMessageProduct.Text = "";
            lblMessageRelation.Text = "";
            lblMessageResponsible.Text = "";
            lblMessageFleet.Text = "";
            lblMessage.Text = "";
            if (!IsPostBack)
            {

            }
        }
        public void fillCustomer(long CustomerID)
        {

            this.CustomerID = CustomerID;
            PDMS_Customer Customer = new PDMS_Customer();
            Customer = new BDMS_Customer().GetCustomer(CustomerID, "", "", null, null, null, null)[0];
            lblCustomer.Text = Customer.CustomerFullName;
             lblContactPerson.Text = Customer.ContactPerson;
            lblMobile.Text = Customer.Mobile;
            lblAlternativeMobile.Text = Customer.AlternativeMobile;
            lblEmail.Text = Customer.Email;
            lblGSTIN.Text = Customer.GSTIN;
            lblPAN.Text = Customer.PAN;

            string Location = Customer.Address1 + ", " + Customer.Address2 + ", " + Customer.District.District + ", " + Customer.State.State;
            lblLocation.Text = Location;

            cbVerified.Checked = Customer.IsVerified;
            cbIsActive.Checked = Customer.IsActive;
            cbOrderBlock.Checked = Customer.OrderBlock;
            cbDeliveryBlock.Checked = Customer.DeliveryBlock;
            cbBillingBlock.Checked = Customer.BillingBlock;

            lbtnVerifiedCustomer.Visible = true;
            if(Customer.IsVerified)
            {
                lbtnVerifiedCustomer.Visible = false;
            }
            lbtnInActivateCustomer.Visible = true;
            if (!Customer.IsActive)
            {
                lbtnInActivateCustomer.Visible = false;
            }

            fillAttribute();
            fillRelation();
            fillProduct();
            fillResponsible();
            fillFleet();
            fillLead();
            fillVisit();
            fillSupportDocument();
        }

        public void fillLead()
        {
            PLeadSearch S = new PLeadSearch(); 
            S.CustomerID = CustomerID; 
            List<PLead> Leads = new BLead().GetLead(S);
            gvLead.DataSource = Leads;
            gvLead.DataBind(); 
        }
        public void fillVisit()
        {  
            gvColdVisit.DataSource = new BColdVisit().GetColdVisit(null, null, null, CustomerID, null, null, null, null, null, null);
            gvColdVisit.DataBind();
        }

        protected void btnSaveMarketSegment_Click(object sender, EventArgs e)
        {
            lblMessageAttribute.Visible = true;
            lblMessageAttribute.ForeColor = Color.Red;
            MPE_Attribute.Show();
            string Message = ValidationAttribute();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageAttribute.Text = Message;
                return;
            }
            PCustomerAttribute Attribute = new PCustomerAttribute();
            Attribute.CustomerID = CustomerID;
            Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = Convert.ToInt32(ddlAttributeMain.SelectedValue) };
            Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = Convert.ToInt32(ddlAttributeSub.SelectedValue) };
            Attribute.Remark = txtRemark.Text.Trim();
            Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute));
            if (Results.Staus == PApplication.Failure)
            {
                lblMessageAttribute.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            ddlAttributeMain.Items.Clear();
            ddlAttributeSub.Items.Clear();
            txtRemark.Text = "";
            tbpCust.ActiveTabIndex = 0;
            MPE_Attribute.Hide();
            fillAttribute();
        }
        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            lblMessageProduct.Visible = true;
            lblMessageProduct.ForeColor = Color.Red;

            MPE_Product.Show();
            string Message = ValidationProduct();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageProduct.Text = Message;
                return;
            }
            PCustomerProduct Product = new PCustomerProduct();
            Product.CustomerProductID = 0;
            Product.CustomerID = CustomerID;
            Product.Make = new PMake() { MakeID = Convert.ToInt32(ddlMake.SelectedValue) };
            Product.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
            Product.Product = new PProduct() { ProductID = Convert.ToInt32(ddlProduct.SelectedValue) };
            Product.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            Product.Remark = txtRemarkProduct.Text.Trim();

            Product.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Product", Product));
            if (Results.Staus == PApplication.Failure)
            {
                lblMessageProduct.Text = Results.Message;
                return;
            } 
             
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;


            ddlMake.Items.Clear();
            ddlProductType.Items.Clear();
            ddlProduct.Items.Clear();
            txtQuantity.Text = "";
            tbpCust.ActiveTabIndex = 1;
            MPE_Product.Hide();
            fillProduct();
        }
        protected void btnSaveRelation_Click(object sender, EventArgs e)
        {
            lblMessageRelation.Visible = true;
            lblMessageRelation.ForeColor = Color.Red;
            MPE_Relation.Show();
            string Message = ValidationRelation();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageRelation.Text = Message;
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
            PApiResult Results =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Relation", Relation));
            if (Results.Staus == PApplication.Failure)
            {
                lblMessageRelation.Text = Results.Message;
                return;
            }

            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            ddlRelation.Items.Clear();
            txtPersonName.Text = "";
            txtMobile.Text = "";
            txtBirthDate.Text = "";
            txtAnniversaryDate.Text = "";
            tbpCust.ActiveTabIndex = 2;
            MPE_Relation.Hide();
            fillRelation();
        }
        protected void btnResponsibleEmp_Click(object sender, EventArgs e)
        {
            MPE_ResponsibleEmp.Show();
            lblMessageResponsible.Visible = true;
            lblMessageResponsible.ForeColor = Color.Red;
            string Message = ValidationEmployee();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageResponsible.Text = Message;
                return;
            }
            PCustomerResponsibleEmployee Relation = new PCustomerResponsibleEmployee();
            Relation.CustomerID = CustomerID;
            Relation.Employee = new PDMS_DealerEmployee() { DealerEmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue) };
            Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/ResponsibleEmployee", Relation));
            if (Results.Staus == PApplication.Failure)
            {
                lblMessageResponsible.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;

            ddlEmployee.Items.Clear(); 
            tbpCust.ActiveTabIndex = 3;
            MPE_ResponsibleEmp.Hide();
            fillResponsible();
        }
        protected void btnFleed_Click(object sender, EventArgs e)
        {
            MPE_Fleed.Show();
            lblMessageFleet.Visible = true;
            lblMessageFleet.ForeColor = Color.Red;
            string Message = ValidationFleet();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageFleet.Text = Message;
                return;
            }

            PCustomerFleet Fleet = new PCustomerFleet();
            Fleet.CustomerFleetID = 0;
            Fleet.CustomerID = CustomerID;
            Fleet.Fleet =new PDMS_Customer() { CustomerID = Convert.ToInt64(txtFleetID.Text) };
            Fleet.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Results =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Fleet", Fleet));
            if (Results.Staus == PApplication.Failure)
            {
                lblMessageFleet.Text = Results.Message;
                return;
            }
            lblMessage.Text = Results.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            txtFleetID.Text = "";
            txtFleet.Text = "";
            tbpCust.ActiveTabIndex = 4;
            MPE_Fleed.Hide();

            fillFleet();
        }



        void fillAttribute()
        {
            gvAttribute.DataSource = new BDMS_Customer().GetCustomerAttribute(CustomerID, null);
            gvAttribute.DataBind();
        }
        void fillProduct()
        {
            gvProduct.DataSource = new BDMS_Customer().GetCustomerProduct(CustomerID, null, null, null, null);
            gvProduct.DataBind();
        }
        void fillRelation()
        {
            gvRelation.DataSource = new BDMS_Customer().GetCustomerRelation(CustomerID, null);
            gvRelation.DataBind();
        }
        void fillResponsible()
        { 
            gvEmployee.DataSource = new BDMS_Customer().GetCustomerResponsibleEmployee( null, CustomerID);
            gvEmployee.DataBind();
        }
        void fillFleet()
        {
            gvFleet.DataSource = new BDMS_Customer().GetCustomerFleet(null, CustomerID);
            gvFleet.DataBind();
        }
        void fillSupportDocument()
        {
            gvSupportDocument.DataSource = new BDMS_Customer().GetAttachedFileCustomer( CustomerID);
            gvSupportDocument.DataBind();
        }
          
        protected void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            string Message = UC_Customer.ValidationCustomer();
            lblMessageCustomerEdit.ForeColor = Color.Red;
            lblMessageCustomerEdit.Visible = true;
            MPE_Customer.Show();
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageCustomerEdit.Text = Message;
                return;
            } 
            PDMS_Customer Customer = UC_Customer.ReadCustomer();
            Customer.CustomerID = CustomerID;
            Customer.CreatedBy = new PUser { UserID = PSession.User.UserID };
            string result = new BAPI().ApiPut("Customer", Customer);
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer", Customer));

            if (Result.Staus == PApplication.Failure)
            {
                lblMessageCustomerEdit.Text = Result.Message; 
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Customer.Hide();
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
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute));
            lblMessage.Visible = true;
            if (Result.Staus == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message; 
            lblMessage.ForeColor = Color.Green;

            fillAttribute();

        }

        protected void lbProductDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerrProductID = (Label)gvRow.FindControl("lblCustomerProductID");
            PCustomerProduct Product = new PCustomerProduct();
            Product.CustomerProductID = Convert.ToInt64(lblCustomerrProductID.Text);
            Product.CustomerID = CustomerID;
            Product.Make = new PMake() { MakeID = 0 };
            Product.ProductType = new PProductType() { ProductTypeID = 0 };
            Product.Product = new PProduct() { ProductID = 0 };
            Product.Quantity = 0;
            Product.CreatedBy = new PUser() { UserID = PSession.User.UserID };

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Product", Product));
            lblMessage.Visible = true;
            if (Result.Staus == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;

            fillProduct();
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
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Relation", Relation));
            lblMessage.Visible = true;
            if (Result.Staus == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
            fillRelation();
        }
        protected void lbResponsibleDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerResponsibleEmployeeID = (Label)gvRow.FindControl("lblCustomerResponsibleEmployeeID");
            PCustomerResponsibleEmployee Relation = new PCustomerResponsibleEmployee();
            Relation.CustomerResponsibleEmployeeID = Convert.ToInt64(lblCustomerResponsibleEmployeeID.Text);
            Relation.CustomerID = CustomerID;
            Relation.Employee = new PDMS_DealerEmployee() { DealerEmployeeID = 0 };
            Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
           // PApiResult s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/ResponsibleEmployee", Relation)).Data);

            PApiResult Result =  JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/ResponsibleEmployee", Relation));

            lblMessage.Visible = true;
            if (Result.Staus == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
            fillResponsible();
        }

        protected void lbFleetDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerFleetID = (Label)gvRow.FindControl("lblCustomerFleetID"); 
            PCustomerFleet Fleet = new PCustomerFleet();
            Fleet.CustomerFleetID = Convert.ToInt64(lblCustomerFleetID.Text);
            Fleet.CustomerID = CustomerID;
            Fleet.Fleet = new PDMS_Customer() { CustomerID = 0 };
            Fleet.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Fleet", Fleet));
            lblMessage.Visible = true;
            if (Result.Staus == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;
            fillFleet();
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
                MPE_Attribute.Show();
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
            else if (lbActions.Text == "Add Fleet")
            {
                // new DDLBind(ddlRelation, new BDMS_Master().GetRelation(null, null), "Relation", "RelationID");
                MPE_Fleed.Show();
            }
            else if (lbActions.Text == "Add Responsible Employee")
            {
                new DDLBind(ddlDealer, PSession.User.Dealer, "CodeWithName", "DID");
                MPE_ResponsibleEmp.Show();
            }
            else if (lbActions.Text == "Verified Customer")
            {
                string endPoint = "Customer/UpdateCustomerVerified?CustomerID=" + CustomerID + "&UserID=" + PSession.User.UserID;
                string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                if (Convert.ToBoolean(s) == true)
                {
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillCustomer(CustomerID);
                }
                else
                {
                    lblMessage.Text = "Something went wrong try again.";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
            else if (lbActions.Text == "In Activate Customer")
            {
                string endPoint = "Customer/UpdateCustomerInActivate?CustomerID=" + CustomerID + "&UserID=" + PSession.User.UserID;
                string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                if (Convert.ToBoolean(s) == true)
                {
                    lblMessage.Text = "Updated successfully";
                    lblMessage.ForeColor = Color.Green;
                    fillCustomer(CustomerID);
                }
                else
                {
                    lblMessage.Text = "Something went wrong try again.";
                    lblMessage.ForeColor = Color.Red;
                }
                lblMessage.Visible = true;
            }
        }

        protected void ddlAttributeMain_SelectedIndexChanged(object sender, EventArgs e)
        { 
            new DDLBind(ddlAttributeSub, new BDMS_Customer().GetCustomerAttributeSub(null,Convert.ToInt32(ddlAttributeMain.SelectedValue), null), "AttributeSub", "AttributeSubID");
            MPE_Attribute.Show();
        }

        public string ValidationAttribute()
        {
            string Message = "";
            ddlAttributeMain.BorderColor = Color.Silver;
            ddlAttributeSub.BorderColor = Color.Silver;
            txtRemark.BorderColor = Color.Silver;
            if (ddlAttributeMain.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Attribute Main";
                ddlAttributeMain.BorderColor = Color.Red;
            }

            else if (ddlAttributeSub.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Attribute Sub";
                ddlAttributeSub.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
            {
                Message = "Please enter the Remark";
                txtRemark.BorderColor = Color.Red;
            }
            return Message;
        }
        public string ValidationProduct()
        {
            int intCheck;
            string Message = "";
            ddlMake.BorderColor = Color.Silver;
            ddlProductType.BorderColor = Color.Silver;
            ddlProduct.BorderColor = Color.Silver;
            txtQuantity.BorderColor = Color.Silver;
            if (ddlMake.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Make";
                ddlMake.BorderColor = Color.Red;
            } 
            else if (ddlProductType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Product Type";
                ddlProductType.BorderColor = Color.Red;
            }
            else if (ddlProduct.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Product";
                ddlProduct.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtQuantity.Text.Trim()))
            {
                Message = "<br/>Please enter the Quantity";
                txtQuantity.BorderColor = Color.Red;
            }
            else if (!int.TryParse(txtQuantity.Text.Trim(), out intCheck))
            {
                Message = Message + "<br/>Quantity should be digit";
                txtMobile.BorderColor = Color.Red;
            }
            return Message;
        }
        public string ValidationRelation()
        {
            long longCheck;
            string Message = "";
            txtPersonName.BorderColor = Color.Silver; 
            txtMobile.BorderColor = Color.Silver;
          
            if (string.IsNullOrEmpty(txtPersonName.Text.Trim()))
            {
                Message = "Please enter the Person Name";
                txtPersonName.BorderColor = Color.Red;
            } 
            else if (string.IsNullOrEmpty(txtMobile.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Mobile";
                txtMobile.BorderColor = Color.Red;
            }
            else if (txtMobile.Text.Trim().Length != 10)
            {
                Message = Message + "<br/>Mobile Length should be 10 digit";
                txtMobile.BorderColor = Color.Red;
            }
            else if (!long.TryParse(txtMobile.Text.Trim(), out longCheck))
            {
                Message = Message + "<br/>Mobile should be 10 digit";
                txtMobile.BorderColor = Color.Red;
            } 
            return Message;
        }
        public string ValidationEmployee()
        { 
            string Message = "";
            ddlEmployee.BorderColor = Color.Silver; 
            if ((ddlEmployee.SelectedValue == "0") || (ddlEmployee.SelectedValue == ""))
            {
                Message = Message + "<br/>Please select the Employee";
                ddlEmployee.BorderColor = Color.Red;
            }
            
            return Message;
        }
        public string ValidationFleet()
        { 
            string Message = "";
            txtFleet.BorderColor = Color.Silver;

            if (string.IsNullOrEmpty(txtFleetID.Text.Trim()))
            {
                Message = "Please enter the Customer";
                txtFleet.BorderColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(txtFleet.Text.Trim()))
            {
                Message = "Please enter the Customer";
                txtFleet.BorderColor = Color.Red;
            }
            return Message;
        }
       

        protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlEmployee, new BDMS_Dealer().GetDealerEmployeeByDealerID(Convert.ToInt32(ddlDealer.SelectedValue),null,null,"",""), "Name", "DealerEmployeeID");
            MPE_ResponsibleEmp.Show();
        }

        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            if (fileUpload.PostedFile.FileName.Length == 0)
            {
                lblMessage.Text = "Please select the file";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            byte[] buffer = new byte[100];
            Stream stream = new MemoryStream(buffer); 
            HttpPostedFile file = fileUpload.PostedFile;
            PAttachedFile F = new PAttachedFile();           
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);

            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size); 

            F.FileName = name;
            F.AttachedFile = fileData;
            F.FileType = file.ContentType;             
            F.FileSize = size;
            F.AttachedFileID = 0;
            F.ReferenceID = CustomerID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID }; 

            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/AttachedFile", F)).Data);
            if (Convert.ToBoolean(s) == true)
            {
                lblMessage.Text = "Updated successfully";
                lblMessage.ForeColor = Color.Green;
                fillSupportDocument();
            }
            else
            {
                lblMessage.Text = "Something went wrong try again.";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true; 
        }

        protected void lbSupportDocumentDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // LinkButton lnkDownload = (LinkButton)sender;
                //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
                long AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text); 
                Label lblFileName = (Label)gvRow.FindControl("lblFileName");
                Label lblFileType = (Label)gvRow.FindControl("lblFileType");

                PAttachedFile UploadedFile = new BDMS_Customer().GetAttachedFileCustomerForDownload(AttachedFileID + Path.GetExtension(lblFileName.Text));

                Response.AddHeader("Content-type", lblFileType.Text);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblFileName.Text);
                HttpContext.Current.Response.Charset = "utf-16";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                Response.BinaryWrite(UploadedFile.AttachedFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Response.End();
            }
        }

        protected void lbSupportDocumentDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblAttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
            PAttachedFile F = new PAttachedFile();
            F.AttachedFileID = Convert.ToInt64(lblAttachedFileID.Text);
            F.ReferenceID = CustomerID;
            F.CreatedBy = new PUser() { UserID = PSession.User.UserID }; 
            string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/AttachedFile", F)).Data);
            lblMessage.Visible = true;
            if (Convert.ToBoolean(s) == true)
            {
                lblMessage.Text = "Removed successfully";
                lblMessage.ForeColor = Color.Green;
                fillSupportDocument();
            }
            else
            {
                lblMessage.Text = "Something went wrong try again.";
                lblMessage.ForeColor = Color.Red;
            } 
        }
    }
}