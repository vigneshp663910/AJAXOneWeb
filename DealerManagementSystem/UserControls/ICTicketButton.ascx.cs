using Business;
using Properties;
using System;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketButton : System.Web.UI.UserControl
    {
        public string PageName { get; set; }
        public PDMS_ICTicket SDMS_ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketButton"] == null)
                {
                    Session["DMS_ICTicketButton"] = new PDMS_ICTicket();
                }
                return (PDMS_ICTicket)Session["DMS_ICTicketButton"];
            }
            set
            {
                Session["DMS_ICTicketButton"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblICTicketID.Text = SDMS_ICTicket.ICTicketID.ToString();
            }
            else
            {
                SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(Convert.ToInt64(lblICTicketID.Text));
            }
            lblICTicketID.Text = SDMS_ICTicket.ICTicketID.ToString();
            btnTechnicianAssign.Visible = false;
            btnServiceConfirmation.Visible = false;
            btnNote.Visible = false;
            btnServiceCharge.Visible = false;
            btnMaterialCharge.Visible = false;

            if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Open)
            {
                btnTechnicianAssign.Visible = true;
                btnNote.Visible = true;
            }
            else if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.TechnicianAssigned)
            {
                btnTechnicianAssign.Visible = true;
                btnServiceConfirmation.Visible = true;
                btnNote.Visible = true;
            }
            else if ((SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Reached) || (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Restored))
            {
                btnTechnicianAssign.Visible = true;
                btnServiceConfirmation.Visible = true;
                btnNote.Visible = true;
                if (SDMS_ICTicket.ServiceType != null)
                {
                    btnServiceCharge.Visible = true;
                    if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Paid1) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Warranty) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Others))
                    {
                        btnMaterialCharge.Visible = true;
                    }
                    else if ((SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.NEPI) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.Commission) || (SDMS_ICTicket.ServiceType.ServiceTypeID == (short)DMS_ServiceType.PreCommission))
                    {
                        btnMaterialCharge.Visible = false;
                    }
                }
            }

            if (PageName == "DMS_ICTicketNote")
            {
                btnNote.Visible = false;
            }
            else if (PageName == "DMS_ICTicketTechnicianAssign")
            {
                btnTechnicianAssign.Visible = false;
            }
            else if (PageName == "DMS_ICTicketMaterialCharges")
            {
                btnMaterialCharge.Visible = false;
            }
            else if (PageName == "DMS_ICTicketServiceCharges")
            {
                btnServiceCharge.Visible = false;
            }
            else if (PageName == "DMS_ICTicketServiceConfirmation")
            {
                btnServiceConfirmation.Visible = false;
            }

            // }
        }
        protected void btnTechnicianAssign_Click(object sender, EventArgs e)
        {
            string url = "DMS_ICTicketTechnicianAssign.aspx?TicketID=" + lblICTicketID.Text;
            Response.Redirect(url);
        }

        protected void btnServiceConfirmation_Click(object sender, EventArgs e)
        {
            string url = "DMS_ICTicketServiceConfirmation.aspx?TicketID=" + lblICTicketID.Text;
            Response.Redirect(url);
        }

        protected void btnNote_Click(object sender, EventArgs e)
        {
            string url = "DMS_ICTicketNote.aspx?TicketID=" + lblICTicketID.Text;
            Response.Redirect(url);
        }

        protected void btnServiceCharge_Click(object sender, EventArgs e)
        {
            string url = "DMS_ICTicketServiceCharges.aspx?TicketID=" + lblICTicketID.Text;
            Response.Redirect(url);
        }

        protected void btnMaterialCharge_Click(object sender, EventArgs e)
        {
            string url = "DMS_ICTicketMaterialCharges.aspx?TicketID=" + lblICTicketID.Text;
            Response.Redirect(url);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = "DMS_ICTicketManage.aspx?TicketID=" + lblICTicketID.Text;
            Response.Redirect(url);
        }
    }
}