using System;
using System.Web.UI;
using Business;
using Properties;


namespace DealerManagementSystem.Account
{
    public partial class MyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script1", "<script type='text/javascript'>SetScreenTitle('My Profile');</script>");
            if (!IsPostBack)
            {
                //FillDealerEmployee(1450);
                FillDealerEmployee(1015);

                if (!string.IsNullOrEmpty(Request.QueryString["DealerEmployeeID"]))
                {
                    FillDealerEmployee(1450);
                    //FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                    //FillDealerEmployeeRole(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                }

            }


            void FillDealerEmployee(int DealerEmployeeID)
            {
                PDMS_DealerEmployee Emp = new BDMS_Dealer().GetDealerEmployeeByDealerEmployeeID(DealerEmployeeID);
                lblFullName.Text = Emp.Name;
                //lblFatherName.Text = Emp.FatherName;
                //lblDOB.Text = Convert.ToString(Emp.DOB);
                //lbDesignation.Text = Emp.DealerEmployeeRole.DealerDesignation.DealerDesignation;
                lblAddress.Text = Emp.Address;

                lblState.Text = Emp.State.State;
                lblCity.Text = Emp.District.District;
                //lblCity.Text = Emp.Tehsil.Tehsil;

                lblEmail.Text = Emp.Email;
                lblContactNo1.Text = Emp.ContactNumber;
                lblContactNo2.Text = Emp.ContactNumber1;
                lblEmergencyContact.Text = Emp.EmergencyContactNumber;
            }
        }
    }
}
