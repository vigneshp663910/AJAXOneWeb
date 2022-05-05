 
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
    public partial class DealerView : System.Web.UI.UserControl
    {

        public PDMS_Dealer Dealer
        {
            get
            {
                if (Session["DealerView"] == null)
                {
                    Session["DealerView"] = new PDMS_Dealer();
                }
                return (PDMS_Dealer)Session["DealerView"];
            }
            set
            {
                Session["DealerView"] = value;
            }
        }

        public List<PDMS_DealerOffice> DealerOfficeList
        {
            get
            {
                if (Session["DealerOffice"] == null)
                {
                    Session["DealerOffice"] = new List<PDMS_DealerOffice>();
                }
                return (List<PDMS_DealerOffice>)Session["DealerOffice"];
            }
            set
            {
                Session["DealerOffice"] = value;
            }
        }

        public List<PDMS_DealerEmployee> DealerEmployeeList
        {
            get
            {
                if (Session["DealerEmployee"] == null)
                {
                    Session["DealerEmployee"] = new List<PDMS_DealerOffice>();
                }
                return (List<PDMS_DealerEmployee>)Session["DealerEmployee"];
            }
            set
            {
                Session["DealerOffice"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                ActionControlMange();
            }
        }
        public void filldealer(int DealerID)
        {
            Dealer = new BDMS_Dealer().GetDealer(DealerID, "", null)[0];

            lblDealerCode.Text = Dealer.DealerCode;
            lblDealerName.Text = Dealer.DealerName;
            lblMobile.Text = Dealer.Mobile;
            lblEmail.Text = Dealer.Email;
            cbIsActive.Checked = Dealer.IsActive;
            lblDealerCountry.Text = Dealer.Country;
            lblDealerState.Text = Dealer.StateN.State;
            //lblTeamLead.Text = Dealer.TL.ContactName;
            //lblSerivceManager.Text = Dealer.SM.ContactName;
            lbtnInActivateDealer.Visible = true;
            if (!Dealer.IsActive)
            {
                lbtnInActivateDealer.Visible = false;
            }

            fillDealerOffice();
            fillDealerEmployee();
            fillNotification();
            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);
                if (lbActions.Text == "Edit Dealer")
                {
                    MPE_Dealer.Show();
                    UC_Dealer.FillMaster();
                    //UC_Dealer.FillDealer(Dealer);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }

        //protected void btnSaveMarketSegment_Click(object sender, EventArgs e)
        //{
        //    lblMessageAttribute.Visible = true;
        //    lblMessageAttribute.ForeColor = Color.Red;
        //    MPE_Attribute.Show();
        //    string Message = ValidationAttribute();
        //    if (!string.IsNullOrEmpty(Message))
        //    {
        //        lblMessageAttribute.Text = Message;
        //        return;
        //    }
        //    PCustomerAttribute Attribute = new PCustomerAttribute();
        //    Attribute.CustomerID = Customer.CustomerID;
        //    Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = Convert.ToInt32(ddlAttributeMain.SelectedValue) };
        //    Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = Convert.ToInt32(ddlAttributeSub.SelectedValue) };
        //    Attribute.Remark = txtRemark.Text.Trim();
        //    Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };

        //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute));
        //    if (Results.Status == PApplication.Failure)
        //    {
        //        lblMessageAttribute.Text = Results.Message;
        //        return;
        //    }
        //    lblMessage.Text = Results.Message;
        //    lblMessage.Visible = true;
        //    lblMessage.ForeColor = Color.Green;

        //    ddlAttributeMain.Items.Clear();
        //    ddlAttributeSub.Items.Clear();
        //    txtRemark.Text = "";
        //    tbpCust.ActiveTabIndex = 0;
        //    MPE_Attribute.Hide();
        //    fillAttribute();
        //}
        //protected void btnSaveProduct_Click(object sender, EventArgs e)
        //{
        //    lblMessageProduct.Visible = true;
        //    lblMessageProduct.ForeColor = Color.Red;

        //    MPE_Product.Show();
        //    string Message = ValidationProduct();
        //    if (!string.IsNullOrEmpty(Message))
        //    {
        //        lblMessageProduct.Text = Message;
        //        return;
        //    }
        //    PCustomerProduct Product = new PCustomerProduct();
        //    Product.CustomerProductID = 0;
        //    Product.CustomerID = Customer.CustomerID;
        //    Product.Make = new PMake() { MakeID = Convert.ToInt32(ddlMake.SelectedValue) };
        //    Product.ProductType = new PProductType() { ProductTypeID = Convert.ToInt32(ddlProductType.SelectedValue) };
        //    Product.Product = new PProduct() { ProductID = Convert.ToInt32(ddlProduct.SelectedValue) };
        //    Product.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
        //    Product.Remark = txtRemarkProduct.Text.Trim();

        //    Product.CreatedBy = new PUser() { UserID = PSession.User.UserID };

        //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Product", Product));
        //    if (Results.Status == PApplication.Failure)
        //    {
        //        lblMessageProduct.Text = Results.Message;
        //        return;
        //    }

        //    lblMessage.Text = Results.Message;
        //    lblMessage.Visible = true;
        //    lblMessage.ForeColor = Color.Green;


        //    ddlMake.Items.Clear();
        //    ddlProductType.Items.Clear();
        //    ddlProduct.Items.Clear();
        //    txtQuantity.Text = "";
        //    tbpCust.ActiveTabIndex = 1;
        //    MPE_Product.Hide();
        //    fillProduct();
        //}
        //protected void btnSaveRelation_Click(object sender, EventArgs e)
        //{
        //    lblMessageRelation.Visible = true;
        //    lblMessageRelation.ForeColor = Color.Red;
        //    MPE_Relation.Show();
        //    string Message = ValidationRelation();
        //    if (!string.IsNullOrEmpty(Message))
        //    {
        //        lblMessageRelation.Text = Message;
        //        return;
        //    }
        //    PCustomerRelation Relation = new PCustomerRelation();
        //    Relation.CustomerID = Customer.CustomerID;
        //    Relation.ContactName = txtPersonName.Text.Trim();
        //    Relation.Mobile = txtMobile.Text.Trim();
        //    Relation.Relation = new PRelation() { RelationID = Convert.ToInt32(ddlRelation.SelectedValue) };
        //    Relation.DOB = string.IsNullOrEmpty(txtBirthDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtBirthDate.Text.Trim());
        //    Relation.DOAnniversary = string.IsNullOrEmpty(txtAnniversaryDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtAnniversaryDate.Text.Trim());
        //    Relation.CreatedBy = new PUser() { UserID = PSession.User.UserID };
        //    PApiResult Results = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Relation", Relation));
        //    if (Results.Status == PApplication.Failure)
        //    {
        //        lblMessageRelation.Text = Results.Message;
        //        return;
        //    }

        //    lblMessage.Text = Results.Message;
        //    lblMessage.Visible = true;
        //    lblMessage.ForeColor = Color.Green;

        //    ddlRelation.Items.Clear();
        //    txtPersonName.Text = "";
        //    txtMobile.Text = "";
        //    txtBirthDate.Text = "";
        //    txtAnniversaryDate.Text = "";
        //    tbpCust.ActiveTabIndex = 2;
        //    MPE_Relation.Hide();
        //    fillRelation();
        //}

        void fillDealerOffice()
        {
            DealerOfficeList = new BDMS_Dealer().GetDealerOffice(Dealer.DealerID, null, null);
            gvDealerOffice.DataSource = DealerOfficeList;
            gvDealerOffice.DataBind();
        }
        void fillDealerEmployee()
        {
            gvDealerEmployee.DataSource = new BDMS_Dealer().GetDealerEmployeeManage(Dealer.DealerID, null, null, null, null, null, null);
            gvDealerEmployee.DataBind();
        }

        void fillNotification()
        {
            gvNotification.DataSource = new BDMS_Dealer().GetDealerNotification(Dealer.DealerID);
            gvNotification.DataBind();
        }

        //protected void btnUpdateDealer_Click(object sender, EventArgs e)
        //{
        //    string Message = UC_Dealer.ValidationCustomer();
        //    lblMessageDealerEdit.ForeColor = Color.Red;
        //    lblMessageDealerEdit.Visible = true;
        //    MPE_Dealer.Show();
        //    if (!string.IsNullOrEmpty(Message))
        //    {
        //        lblMessageDealerEdit.Text = Message;
        //        return;
        //    }
        //    PDMS_Dealer DealerU = UC_Dealer.ReadCustomer();
        //    DealerU.DealerID = Dealer.DealerID;
        //    DealerU.CreatedBy = new PUser { UserID = PSession.User.UserID };
        //    PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer", CustomerU));

        //    if (Result.Status == PApplication.Failure)
        //    {
        //        lblMessageDealerEdit.Text = Result.Message;
        //        return;
        //    }
        //    lblMessage.Text = Result.Message;
        //    lblMessage.Visible = true;
        //    lblMessage.ForeColor = Color.Green;
        //    MPE_Dealer.Hide();
        //    //  fillCustomer(Customer.CustomerID);
        //}

        protected void ibtnDealerOfficeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerOffice.PageIndex > 0)
            {
                gvDealerOffice.PageIndex = gvDealerOffice.PageIndex - 1;
                DealerOfficeBind(gvDealerOffice, lblRowCount, DealerOfficeList);
            }
        }
        protected void ibtnDealerOfficeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerOffice.PageCount > gvDealerOffice.PageIndex)
            {
                gvDealerOffice.PageIndex = gvDealerOffice.PageIndex + 1;
                DealerOfficeBind(gvDealerOffice, lblRowCount, DealerOfficeList);
            }
        }

        void DealerOfficeBind(GridView gv, Label lbl, List<PDMS_DealerOffice> DealerList)
        {
            gv.DataSource = DealerOfficeList;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerOfficeList.Count;
        }

        protected void gvDealerOffice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerOffice.PageIndex = e.NewPageIndex;
            fillDealerOffice();
        }

        protected void ibtnDealerEmployeeArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageIndex > 0)
            {
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex - 1;
                DealerEmployeeBind(gvDealerEmployee, lblRowCount, DealerEmployeeList);
            }
        }
        protected void ibtnDealerEmployeeArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvDealerEmployee.PageCount > gvDealerEmployee.PageIndex)
            {
                gvDealerEmployee.PageIndex = gvDealerEmployee.PageIndex + 1;
                DealerEmployeeBind(gvDealerEmployee, lblRowCount, DealerEmployeeList);
            }
        }

        void DealerEmployeeBind(GridView gv, Label lbl, List<PDMS_DealerEmployee> DealerEmployeeList)
        {
            gv.DataSource = DealerEmployeeList;
            gv.DataBind();
            lbl.Text = (((gv.PageIndex) * gv.PageSize) + 1) + " - " + (((gv.PageIndex) * gv.PageSize) + gv.Rows.Count) + " of " + DealerEmployeeList.Count;
        }

        protected void gvDealerEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDealerEmployee.PageIndex = e.NewPageIndex;
            fillDealerEmployee();
        }

        protected void lnkbtnDealerOfficeDelete_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblOfficeCode = (Label)gvRow.FindControl("lblOfficeCode");
            PDMS_DealerOffice DealerOffice = new PDMS_DealerOffice();
            DealerOffice.OfficeCode = lblOfficeCode.Text;
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            DealerOffice.OfficeID = Convert.ToInt32(lblOfficeID.Text);

            //Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = 0 };
            //Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = 0 };
            //Attribute.Remark = txtRemark.Text.Trim();
            //Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };


            //Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            //PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Customer/Attribute", Attribute));
            //lblMessage.Visible = true;
            //if (Result.Status == PApplication.Failure)
            //{
            //    lblMessage.Text = Result.Message;
            //    lblMessage.ForeColor = Color.Red;
            //    return;
            //}
            //lblMessage.Text = Result.Message;
            //lblMessage.ForeColor = Color.Green;

            fillDealerOffice();

        }

        protected void lnkBtnNotificationDelete_Click(object sender, EventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            Label lblDealerNotificationID = (Label)gvRow.FindControl("lblDealerNotificationID");
            PDealerNotification DealerNotification = new PDealerNotification();
            DealerNotification.DealerNotificationID = Convert.ToInt32(lblDealerNotificationID.Text);
            DealerNotification.IsActive = false;

            //Attribute.AttributeMain = new PCustomerAttributeMain() { AttributeMainID = 0 };
            //Attribute.AttributeSub = new PCustomerAttributeSub() { AttributeSubID = 0 };
            //Attribute.Remark = txtRemark.Text.Trim();
            //Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };


            //Attribute.CreatedBy = new PUser() { UserID = PSession.User.UserID };
            PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Dealer/DealerNotification", DealerNotification));
            lblMessage.Visible = true;
            if (Result.Status == PApplication.Failure)
            {
                lblMessage.Text = Result.Message;
                lblMessage.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = Result.Message;
            lblMessage.ForeColor = Color.Green;

            fillNotification();

        }

        void ActionControlMange()
        {

            lbtnActivateDealer.Visible = false;
            lbtnInActivateDealer.Visible = true;

            lbEditDealer.Visible = true;

            if (Dealer.IsActive)
            {

            }
            else
            {
                lbtnActivateDealer.Visible = true;
                lbtnInActivateDealer.Visible = false;

                lbEditDealer.Visible = false;
            }
        }       
        
    }
}