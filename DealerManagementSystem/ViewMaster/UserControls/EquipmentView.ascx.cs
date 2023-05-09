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
    public partial class EquipmentView : System.Web.UI.UserControl
    {
        //public long CustomerID
        //{
        //    get
        //    {
        //        if (Session["CustomerID"] == null)
        //        {
        //            Session["CustomerID"] = 0;
        //        }
        //        return Convert.ToInt64(Session["CustomerID"]);
        //    }
        //    set
        //    {
        //        Session["CustomerID"] = value;
        //    }
        //}

        public PDMS_Customer Customer
        {
            get
            {
                if (ViewState["CustomerView"] == null)
                {
                    ViewState["CustomerView"] = new PDMS_Customer();
                }
                return (PDMS_Customer)ViewState["CustomerView"];
            }
            set
            {
                ViewState["CustomerView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ActionControlMange();
            }

            //if (!string.IsNullOrEmpty(Convert.ToString(ViewState["CustomerID"])))
            //{
            //    long CustomerID = Convert.ToInt64(Convert.ToString(ViewState["CustomerID"]));
            //    if (CustomerID != Customer.CustomerID)
            //    {
            //        Customer = new BDMS_Customer().GetCustomerByID(CustomerID);
            //    }
            //}
        }
        public void fillCustomer(long CustomerID)
        {
            // this.CustomerID = CustomerID; ;
            //ViewState["CustomerID"] = CustomerID;
            Customer = new BDMS_Customer().GetCustomerByID(CustomerID);
            lblCustomer.Text = Customer.CustomerFullName;
            lblContactPerson.Text = Customer.ContactPerson;
            lblMobile.Text = "<a href='tel:" + Customer.Mobile + "'>" + Customer.Mobile + "</a>";
            lblAlternativeMobile.Text = "<a href='tel:" + Customer.AlternativeMobile + "'>" + Customer.AlternativeMobile + "</a>";
            lblEmail.Text = "<a href='mailto:" + Customer.Email + "'>" + Customer.Email + "</a>";
            lblGSTIN.Text = Customer.GSTIN;
            lblPAN.Text = Customer.PAN;

            string Address = Customer.Address1 + ", " + Customer.Address2 + ", " + Customer.District.District + ", " + Customer.State.State;

            lblAddress1.Text = Customer.Address1;
            lblAddress2.Text = Customer.Address2;
            lblAddress3.Text = Customer.Address3;
            lblCity.Text = Customer.City;
            lblDistrict.Text = Customer.District.District;
            lblState.Text = Customer.State.State;
            lblPinCode.Text = Customer.Pincode;
            lblLastVisitDate.Text = Convert.ToString(Customer.LastVisitDate);

            cbVerified.Checked = Customer.IsVerified;
            cbIsActive.Checked = Customer.IsActive;
            cbOrderBlock.Checked = Customer.OrderBlock;
            cbDeliveryBlock.Checked = Customer.DeliveryBlock;
            cbBillingBlock.Checked = Customer.BillingBlock;

            lbtnVerifiedCustomer.Visible = true;
            if (Customer.IsVerified)
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
            ActionControlMange(); 
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.Text == "Edit Customer")
                {
                    MPE_Customer.Show();
                    UC_Customer.FillMaster();
                    UC_Customer.FillCustomer(Customer);
                }
                
                else if (lbActions.Text == "In Activate Customer")
                {
                    string endPoint = "Customer/UpdateCustomerInActivate?CustomerID=" + Customer.CustomerID + "&UserID=" + PSession.User.UserID + "&Active=False";
                    string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                    if (Convert.ToBoolean(s) == true)
                    {
                        lblMessage.Text = "Updated successfully";
                        lblMessage.ForeColor = Color.Green;
                        fillCustomer(Customer.CustomerID);
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong try again.";
                        lblMessage.ForeColor = Color.Red;
                    }
                    lblMessage.Visible = true;
                }
                else if (lbActions.Text == "Activate Customer")
                {
                    string endPoint = "Customer/UpdateCustomerInActivate?CustomerID=" + Customer.CustomerID + "&UserID=" + PSession.User.UserID + "&Active=True";
                    string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                    if (Convert.ToBoolean(s) == true)
                    {
                        lblMessage.Text = "Updated successfully";
                        lblMessage.ForeColor = Color.Green;
                        fillCustomer(Customer.CustomerID);
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong try again.";
                        lblMessage.ForeColor = Color.Red;
                    }
                    lblMessage.Visible = true;
                }
                else if (lbActions.Text == "Sync to Sap")
                {

                    if (!Customer.IsVerified)
                    {
                        lblMessage.Text = "Please Verify Customer then Sync to Sap";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    long C = new BDMS_Customer().UpdateCustomerCodeFromSapToSql(Customer, false);
                    //   string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                    //if (Convert.ToBoolean(s) == true)
                    if (C != 0)
                    {
                        lblMessage.Text = "Updated successfully";
                        lblMessage.ForeColor = Color.Green;
                        fillCustomer(Customer.CustomerID);
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong try again.";
                        lblMessage.ForeColor = Color.Red;
                    }
                    lblMessage.Visible = true;
                    fillCustomer(Customer.CustomerID);
                }
              
                
                else if (lbActions.Text == "In Activate ShipTo")
                {
                    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                    Label lblCustomerShipToID = (Label)gvRow.FindControl("lblCustomerShipToID");
                    PDMS_CustomerShipTo ShipTo = new BDMS_Customer().GetCustomerShopTo(Convert.ToInt64(lblCustomerShipToID.Text), null)[0];
                    ShipTo.IsActive = false;
                    ShipTo.CreatedBy = new PUser() { UserID = PSession.User.UserID };
                    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/CustomerShipTo", ShipTo));
                    if (Results.Status == PApplication.Failure)
                    {
                        lblMessage.Text = Results.Message;
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Green; 
                }
                else if (lbActions.Text == "Activate ShipTo")
                {
                    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                    Label lblCustomerShipToID = (Label)gvRow.FindControl("lblCustomerShipToID");
                    PDMS_CustomerShipTo ShipTo = new BDMS_Customer().GetCustomerShopTo(Convert.ToInt64(lblCustomerShipToID.Text), null)[0];
                    ShipTo.IsActive = true;
                    ShipTo.CreatedBy = new PUser() { UserID = PSession.User.UserID };
                    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/CustomerShipTo", ShipTo));
                    if (Results.Status == PApplication.Failure)
                    {
                        lblMessage.Text = Results.Message;
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }
                    lblMessage.Text = Results.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = Color.Green; 
                }
                else if (lbActions.Text == "ShipTo Sync to Sap")
                {
                    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                    Label lblCustomerShipToID = (Label)gvRow.FindControl("lblCustomerShipToID");
                    PDMS_CustomerShipTo ShipTo = new BDMS_Customer().GetCustomerShopTo(Convert.ToInt64(lblCustomerShipToID.Text), null)[0];

                    PDMS_Customer CustomerS = new BDMS_Customer().GetCustomerByID(Customer.CustomerID);
                    CustomerS.CustomerCode = ShipTo.CustomerCode;
                    CustomerS.CustomerID = ShipTo.CustomerShipToID;
                    CustomerS.Address1 = ShipTo.Address1;
                    CustomerS.Address2 = ShipTo.Address2;
                    CustomerS.Address3 = ShipTo.Address3;
                    CustomerS.ContactPerson = ShipTo.ContactPerson;
                    CustomerS.Mobile = ShipTo.Mobile;
                    CustomerS.Email = ShipTo.Email;
                    CustomerS.Country = ShipTo.Country;
                    CustomerS.State = ShipTo.State;
                    CustomerS.District = ShipTo.District;
                    CustomerS.Tehsil = ShipTo.Tehsil;
                    CustomerS.Pincode = ShipTo.Pincode;
                    CustomerS.City = ShipTo.City;

                    long C = new BDMS_Customer().UpdateCustomerCodeFromSapToSql(CustomerS, true);

                    //   string s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiGet(endPoint)).Data);
                    //if (Convert.ToBoolean(s) == true)
                    if (C != 0)
                    {
                        lblMessage.Text = "Updated successfully";
                        lblMessage.ForeColor = Color.Green;
                        fillCustomer(Customer.CustomerID);
                    }
                    else
                    {
                        lblMessage.Text = "Something went wrong try again.";
                        lblMessage.ForeColor = Color.Red;
                    }
                    lblMessage.Visible = true;
                    fillCustomer(Customer.CustomerID);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
       
 
        void fillAttribute()
        {
            gvAttribute.DataSource = new BDMS_Customer().GetCustomerAttribute(Customer.CustomerID, null);
            gvAttribute.DataBind();
        }
        void fillProduct()
        {
            gvProduct.DataSource = new BDMS_Customer().GetCustomerProduct(Customer.CustomerID, null, null, null, null);
            gvProduct.DataBind();
        }
        void fillRelation()
        {
            gvRelation.DataSource = new BDMS_Customer().GetCustomerRelation(Customer.CustomerID, null);
            gvRelation.DataBind();
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
            PDMS_Customer_Insert CustomerU = UC_Customer.ReadCustomer();
            CustomerU.CustomerID = Customer.CustomerID; 
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer", CustomerU));

            if (Result.Status == PApplication.Failure)
            {
                lblMessageCustomerEdit.Text = Result.Message;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Green;
            MPE_Customer.Hide();
            fillCustomer(Customer.CustomerID);
        }

     
        protected void lbRelationDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblCustomerRelationID = (Label)gvRow.FindControl("lblCustomerRelationID");
            PCustomerRelation Relation = new PCustomerRelation();
            Relation.CustomerRelationID = Convert.ToInt64(lblCustomerRelationID.Text);
            Relation.CustomerID = Customer.CustomerID;
            Relation.Relation = new PRelation() { RelationID = 0 };
            Relation.Designation = new PCustomerEmployeeDesignation() { DesignationID = 0 };
            Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Relation", Relation));
            lblMessage.Visible = true;
            if (Result.Status == PApplication.Failure)
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
            Relation.CustomerID = Customer.CustomerID;
            Relation.Employee = new PDMS_DealerEmployee() { DealerEmployeeID = 0 };
            Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            // PApiResult s = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/ResponsibleEmployee", Relation)).Data);

            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/ResponsibleEmployee", Relation));

            lblMessage.Visible = true;
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green; 
        }

      
        
        void ActionControlMange()
        {
            lbtnActivateCustomer.Visible = false;
            lbtnInActivateCustomer.Visible = true;

            lbEditCustomer.Visible = true;
            lbAddAttribute.Visible = true;
            lbAddProduct.Visible = true;
            lbAddRelation.Visible = true;
            lbAddFleet.Visible = true;
            lbAddResponsibleEmployee.Visible = true;
            lbtnVerifiedCustomer.Visible = true;
            lbtnSyncToSap.Visible = true;
            if (Customer.IsVerified)
            {
                lbtnVerifiedCustomer.Visible = false;
            }
            if (Customer.IsActive)
            {

            }
            else
            {
                lbtnActivateCustomer.Visible = true;
                lbtnInActivateCustomer.Visible = false;

                lbEditCustomer.Visible = false;
                lbAddAttribute.Visible = false;
                lbAddProduct.Visible = false;
                lbAddRelation.Visible = false;
                lbAddFleet.Visible = false;
                lbAddResponsibleEmployee.Visible = false;
                lbtnVerifiedCustomer.Visible = false;
                lbtnSyncToSap.Visible = false;
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.CustomerVerify).Count() == 0)
            {
                lbtnVerifiedCustomer.Visible = false;
            }

        } 
    }
}