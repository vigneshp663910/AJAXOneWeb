using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void FillMaster()
        { 
            new DDLBind(ddlCountry, new BDMS_Address().GetCountry(null, null), "Country", "CountryID");
            PDealer Dealer = PSession.User.Dealer[0];
            int CountryID = Dealer.Country.CountryID;
            ddlCountry.SelectedValue = Convert.ToString(CountryID);
            new DDLBind(ddlState, new BDMS_Address().GetState(CountryID, null, null, null), "State", "StateID"); 

        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PDMS_State> State = new BDMS_Address().GetState(Convert.ToInt32(ddlCountry.SelectedValue), null, null, null);
            new DDLBind(ddlState, State, "State", "StateID");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            new DDLBind(ddlDistrict, new BDMS_Address().GetDistrict(Convert.ToInt32(ddlCountry.SelectedValue), null, Convert.ToInt32(ddlState.SelectedValue), null, null, null), "District", "DistrictID");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            new BDMS_Equipment().GetEquipmentForCreateICTicket(EquipmentHeaderID, EquipmentSerialNo, Customer);
        }
    }
}