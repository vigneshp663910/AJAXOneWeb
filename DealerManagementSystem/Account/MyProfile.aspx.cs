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
               
                //FillDealerEmployee(1015);
                //FillDealerEmployee(1450);

                FillDealerEmployee(PSession.User.DealerEmployeeID); // 1450 - Murugeshan KN

                if (!string.IsNullOrEmpty(Request.QueryString["DealerEmployeeID"]))
                {
                    //FillDealerEmployee(PSession.User.DealerEmployeeID);
                    //FillDealerEmployee(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                    //FillDealerEmployeeRole(Convert.ToInt32(Request.QueryString["DealerEmployeeID"]));
                }

            }


            void FillDealerEmployee(int DealerEmployeeID)
            {
                PDMS_DealerEmployee Emp = new BDMS_Dealer().GetDealerEmployeeByDealerEmployeeID(DealerEmployeeID);
                lblFullName.Text = Emp.Name;              
                lblAddress.Text = Emp.Address;

                if (Emp.State != null)
                {
                    lblState.Text = Emp.State.State;
                    if (Emp.District != null)
                    {
                        lblDistrict.Text = Emp.District.District;
                        if (Emp.Tehsil != null)
                        {
                            lblTehsil.Text = Emp.Tehsil.Tehsil;
                        }
                    }
                }             
                lblVillage.Text = Emp.Village;
                lblEmail.Text = "<a href=MAILTO:" + Emp.Email + '>' + Emp.Email + "</a>";
                lblContactNo1.Text = Emp.ContactNumber;
                lblContactNo2.Text = Emp.ContactNumber1;
                lblEmergencyContact.Text = Emp.EmergencyContactNumber;
                lblEmpID.Text = Emp.DealerEmployeeID.ToString();
                
            }
        }
    }
}
