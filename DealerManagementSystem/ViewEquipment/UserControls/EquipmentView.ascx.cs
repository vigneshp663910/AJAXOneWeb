using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace DealerManagementSystem.ViewEquipment.UserControls
{
    public partial class EquipmentView : System.Web.UI.UserControl
    {
      
        public PDMS_EquipmentHeader ColdVisit
        {
            get
            {
                if (Session["EquipmentView"] == null)
                {
                    Session["EquipmentView"] = new PDMS_EquipmentHeader();
                }
                return (PDMS_EquipmentHeader)Session["EquipmentView"];
            }
            set
            {
                Session["EquipmentView"] = value;
            }
        } 
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            lblMessage.Text = ""; 
        }
        public void fillEquipment(long EquipmentHeaderID)
        { 
            ViewState["EquipmentHeaderID"] = EquipmentHeaderID; 
            ActionControlMange(); 
        }

        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);

            if (lbActions.Text == "Add Effort")
            {
                
            }
            else if (lbActions.Text == "Add Expense")
            {
                
            }
            else if (lbActions.Text == "Status Change to Close")
            {
               
            }
            else if (lbActions.Text == "Status Change to Cancel")
            {
               
            }
            else if (lbActions.Text == "Add Activity")
            {
               
               
            }
        }

       
        void ActionControlMange()
        {
            
        } 
    }
}