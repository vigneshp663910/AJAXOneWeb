using Business;
using DataAccess;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketBasicInformation_N : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
        public List<PDMS_ServiceCharge> SS_ServiceCharge
        {
            get
            {
                if (Session["ServiceChargeICTicketProcess"] == null)
                {
                    Session["ServiceChargeICTicketProcess"] = new List<PDMS_ServiceCharge>();
                }
                return (List<PDMS_ServiceCharge>)Session["ServiceChargeICTicketProcess"];
            }
            set
            {
                Session["ServiceChargeICTicketProcess"] = value;
            }
        }
        public List<PDMS_ServiceMaterial> SS_ServiceMaterial
        {
            get
            {
                if (Session["ServiceMaterialICTicketProcess"] == null)
                {
                    Session["ServiceMaterialICTicketProcess"] = new List<PDMS_ServiceMaterial>();
                }
                return (List<PDMS_ServiceMaterial>)Session["ServiceMaterialICTicketProcess"];
            }
            set
            {
                Session["ServiceMaterialICTicketProcess"] = value;
            }
        }

        public List<PDMS_ICTicket> ICTicketOverhaul
        {
            get
            {
                if (Session["ICTicketOverhaul"] == null)
                {
                    Session["ICTicketOverhaul"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["ICTicketOverhaul"];
            }
            set
            {
                Session["ICTicketOverhaul"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageTechnicianInformation.Visible = false;
            lblMessageTechnicianInformation.Text = "";

            lblMessageCallInformation.Visible = false;
            lblMessageCallInformation.Text = "";
            UC_BasicInformation.SDMS_ICTicket = SDMS_ICTicket;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["TicketID"]))
                {
                    long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                    if (PSession.User.IsTechnician == true)
                    {
                        pnlTechnician.Visible = false;
                    }
                    else
                    {
                        pnlTechnician.Visible = true;
                    }
                    SDMS_Technicians = new BDMS_Service().GetTechniciansByDealerID(SDMS_ICTicket.Dealer.DealerID);
                    FillTechniciansByTicketID();
                    //    cbIsWarranty.Checked = SDMS_ICTicket.IsWarranty;

                    if ((SDMS_ICTicket.Equipment.IsRefurbished == true) && (SDMS_ICTicket.Equipment.RefurbishedBy == SDMS_ICTicket.Dealer.DealerID) && (SDMS_ICTicket.Equipment.RFWarrantyExpiryDate >= SDMS_ICTicket.ICTicketDate))
                    {
                        FillGetServiceType(2);
                    }
                    else if (SDMS_ICTicket.IsWarranty == true)
                    {
                        FillGetServiceType(1);
                    }
                    else
                    {
                        FillGetServiceType(0);
                    }

                    FillGetDealerOffice();
                    FillGetServicePriority();
                    new BDMS_TypeOfWarranty().GetTypeOfWarrantyDDL(ddlTypeOfWarranty, null, null);

                    FillCallInformation();

                    //  ceReachedDate.StartDate = SDMS_ICTicket.RequestedDate;
                    ceReachedDate.EndDate = DateTime.Now;
                    ValidateReachedDate();
                    if (SDMS_ICTicket.Dealer.DealerCode == "9005")
                    {
                        lblCess.Visible = true;
                        cbCess.Visible = true;
                    }
                    else
                    {
                        lblCess.Visible = false;
                        cbCess.Visible = false;
                    }
                }
            }
            DisplayOrHide();
        }
        void DisplayOrHide()
        {
            if (SDMS_ICTicket.ServiceStatus.ServiceStatusID == (short)DMS_ServiceStatus.Open)
            {
                pnlCallInformationSH.Enabled = false;
                lblMessageCallInformation.Visible = true;
                lblMessageCallInformation.Text = "For call information entry - technician assignment is mandatory";
                lblMessageCallInformation.ForeColor = Color.Maroon;
            }
            else
            {
                pnlCallInformationSH.Enabled = true;
                lblMessageCallInformation.Visible = false;
            }
        }
        //private void FillBasicInformation()
        //{

        //    lblICTicket.Text = SDMS_ICTicket.ICTicketNumber + " - " + SDMS_ICTicket.ICTicketDate;
        //    lblDealer.Text = SDMS_ICTicket.Dealer.DealerCode + " - " + SDMS_ICTicket.Dealer.DealerName;
        //    lblCustomer.Text = SDMS_ICTicket.Customer.CustomerCode + " - " + SDMS_ICTicket.Customer.CustomerName;
        //    lblStatus.Text = SDMS_ICTicket.ServiceStatus.ServiceStatus;
        //    lblRequestedDate.Text = SDMS_ICTicket.RequestedDate == null ? "" : (DateTime)SDMS_ICTicket.RequestedDate + "";
        //    cbIsWarranty.Checked = SDMS_ICTicket.IsWarranty;
        //    cbIsMarginWarranty.Checked = SDMS_ICTicket.IsMarginWarranty;

        //    lblWarrantyExpiry.Text = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).ToShortDateString();
        //    lblDistrict.Text = SDMS_ICTicket.Address.District.District + " - " + SDMS_ICTicket.Address.State.State;
        //    lblContactPerson.Text = SDMS_ICTicket.ContactPerson;
        //    lblPresentContactNumber.Text = SDMS_ICTicket.PresentContactNumber;
        //    lblComplaintDescription.Text = SDMS_ICTicket.ComplaintDescription;
        //    lblInformation.Text = SDMS_ICTicket.Information;
        //    lblOldICTicketNumber.Text = SDMS_ICTicket.OldICTicketNumber;


        //    lblEquipment.Text = SDMS_ICTicket.Equipment.EquipmentSerialNo;
        //    lblModel.Text = SDMS_ICTicket.Equipment.EquipmentModel.Model;

        //    lblLastHMRValue.Text = Convert.ToString(SDMS_ICTicket.LastHMRValue);
        //    lblLastHMRDate.Text = SDMS_ICTicket.LastHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.LastHMRDate).ToShortDateString();
        //    lblNewHMRValue.Text = Convert.ToString(SDMS_ICTicket.CurrentHMRValue);
        //    lblNewHMRDate.Text = SDMS_ICTicket.CurrentHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.CurrentHMRDate).ToShortDateString();

        //}
        //
        public List<PDMS_ServiceTechnician> SDMS_Technicians
        {
            get
            {
                if (Session["DMS_ICTicketTechnicianAssign"] == null)
                {
                    Session["DMS_ICTicketTechnicianAssign"] = new List<PDMS_ServiceTechnician>();
                }
                return (List<PDMS_ServiceTechnician>)Session["DMS_ICTicketTechnicianAssign"];
            }
            set
            {
                Session["DMS_ICTicketTechnicianAssign"] = value;
            }
        }
        // public PDMS_ICTicket SDMS_ICTicket
        //{
        //    get
        //    {
        //        if (Session["DMS_ICTicket"] == null)
        //        {
        //            Session["DMS_ICTicket"] = new PDMS_ICTicket();
        //        }
        //        return (PDMS_ICTicket)Session["DMS_ICTicket"];
        //    }
        //    set
        //    {
        //        Session["DMS_ICTicket"] = value;
        //    }
        //}
        private void FillTechniciansByTicketID()
        {
            SDMS_Technicians = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);
            int rowC = SDMS_Technicians.Count;
            if (SDMS_Technicians.Count == 0)
            {
                SDMS_Technicians.Add(new PDMS_ServiceTechnician());
            }
            gvTechnician.DataSource = SDMS_Technicians;
            gvTechnician.DataBind();
            if (rowC == 1)
            {
                gvTechnician.Columns[4].Visible = false;
            }
            else
            {
                gvTechnician.Columns[4].Visible = true;
            }
        }
        protected void gvTechnician_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbTechnicianRemove = (LinkButton)e.Row.FindControl("lbTechnicianRemove");
                Label lblUserID = (Label)e.Row.FindControl("lblUserID");

                PDMS_ServiceTechnician WorkedDate = new PDMS_ServiceTechnician();
                WorkedDate = SDMS_Technicians.Find(s => s.UserID == Convert.ToInt32(lblUserID.Text));
                if (WorkedDate.ServiceTechnicianWorkedDate != null)
                {
                    if (WorkedDate.ServiceTechnicianWorkedDate.Count != 0)
                    {
                        lbTechnicianRemove.Visible = false;
                    }
                }
                //if (SDMS_ICTicket.Technician.UserID == Convert.ToInt32(lblUserID.Text))
                //{
                //    lbTechnicianRemove.Visible = false;
                //}
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList gvddlTechnician = (DropDownList)e.Row.FindControl("gvddlTechnician");
                gvddlTechnician.DataTextField = "ContactName";
                gvddlTechnician.DataValueField = "UserID";
                gvddlTechnician.DataSource = new BDMS_Service().GetTechniciansByDealerID(SDMS_ICTicket.Dealer.DealerID); ;
                gvddlTechnician.DataBind();
                gvddlTechnician.Items.Insert(0, new ListItem("Select", "0"));

            }
        }

        protected void lbTechnicianAdd_Click(object sender, EventArgs e)
        {
            lblMessageTechnicianInformation.Visible = true;
            DropDownList gvddlTechnician = (DropDownList)gvTechnician.FooterRow.FindControl("gvddlTechnician");
            //  gvddlTechnician.Focus();
            if (gvddlTechnician.SelectedValue == "0")
            {
                lblMessageTechnicianInformation.Text = "Please select the Technician";
                lblMessageTechnicianInformation.ForeColor = Color.Red;
                return;
            }
            for (int i = 0; i < gvTechnician.Rows.Count; i++)
            {
                Label lblUserID = (Label)gvTechnician.Rows[i].FindControl("lblUserID");
                if (lblUserID.Text == gvddlTechnician.SelectedValue)
                {
                    lblMessageTechnicianInformation.Text = "Already this Technician added for this ticket. Please check it";
                    lblMessageTechnicianInformation.ForeColor = Color.Red;
                    return;
                }
            }

            if (new BDMS_ICTicket().InsertOrUpdateTechnicianAddOrRemoveICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(gvddlTechnician.SelectedValue), false, PSession.User.UserID))
            {
                lblMessageTechnicianInformation.Text = "New Technician Assigned";
                lblMessageTechnicianInformation.ForeColor = Color.Green;
                FillTechniciansByTicketID();

                long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);
                DisplayOrHide();
            }
            else
            {
                lblMessageTechnicianInformation.Text = "New Technician is not Assigned";
                lblMessageTechnicianInformation.ForeColor = Color.Red;
            }
        }

        protected void lbTechnicianRemove_Click(object sender, EventArgs e)
        {
            lblMessageTechnicianInformation.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            Label lblUserIDR = (Label)gvRow.FindControl("lblUserID");

            SDMS_Technicians = new BDMS_Service().GetTechniciansByTicketID(SDMS_ICTicket.ICTicketID);

            PDMS_ServiceTechnician WorkedDate = new PDMS_ServiceTechnician();
            WorkedDate = SDMS_Technicians.Find(s => s.UserID == Convert.ToInt32(lblUserIDR.Text));
            if (WorkedDate.ServiceTechnicianWorkedDate != null)
            {
                if (WorkedDate.ServiceTechnicianWorkedDate.Count != 0)
                {
                    lblMessageTechnicianInformation.Text = "You can not remove this Technician";
                    lblMessageTechnicianInformation.ForeColor = Color.Red;
                    FillTechniciansByTicketID();
                    return;
                }
            }


            if (new BDMS_ICTicket().InsertOrUpdateTechnicianAddOrRemoveICTicket(SDMS_ICTicket.ICTicketID, Convert.ToInt32(lblUserIDR.Text), true, PSession.User.UserID))
            {
                lblMessageTechnicianInformation.Text = "Technician Removed";
                lblMessageTechnicianInformation.ForeColor = Color.Green;
                FillTechniciansByTicketID();
            }
            else
            {
                lblMessageTechnicianInformation.Text = "Technician is not Removed";
                lblMessageTechnicianInformation.ForeColor = Color.Red;
            }
            //   DropDownList gvddlTechnician = (DropDownList)gvTechnician.FooterRow.FindControl("gvddlTechnician");
            //  gvddlTechnician.Focus();
        }
        private void FillCallInformation()
        {


            txtLocation.Text = SDMS_ICTicket.Location;
            if (SDMS_ICTicket.DealerOffice != null)
                ddlDealerOffice.SelectedValue = SDMS_ICTicket.DealerOffice.OfficeID.ToString();


            txtDepartureDate.Text = SDMS_ICTicket.DepartureDate == null ? "" : ((DateTime)SDMS_ICTicket.DepartureDate).ToShortDateString();
            ddlDepartureHH.SelectedValue = SDMS_ICTicket.DepartureDate == null ? "-1" : ((DateTime)SDMS_ICTicket.DepartureDate).Hour.ToString();
            if (SDMS_ICTicket.DepartureDate != null)
            {
                int DepartureMMMinute = ((DateTime)SDMS_ICTicket.DepartureDate).Minute;
                int adjustment = DepartureMMMinute % 5;
                if (adjustment != 0)
                {
                    DepartureMMMinute = (DepartureMMMinute - adjustment) + 5;
                }
                ddlDepartureMM.SelectedValue = DepartureMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlDepartureMM.SelectedValue = "0";
            }

            txtReachedDate.Text = SDMS_ICTicket.ReachedDate == null ? "" : ((DateTime)SDMS_ICTicket.ReachedDate).ToShortDateString();
            ddlReachedHH.SelectedValue = SDMS_ICTicket.ReachedDate == null ? "-1" : ((DateTime)SDMS_ICTicket.ReachedDate).Hour.ToString();
            if (SDMS_ICTicket.ReachedDate != null)
            {
                int ReachedMMMinute = ((DateTime)SDMS_ICTicket.ReachedDate).Minute;
                int adjustment = ReachedMMMinute % 5;
                if (adjustment != 0)
                {
                    ReachedMMMinute = (ReachedMMMinute - adjustment) + 5;
                }
                ddlReachedMM.SelectedValue = ReachedMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlReachedMM.SelectedValue = "0";
            }
            //  ddlReachedMM.SelectedValue = SDMS_ICTicket.ReachedDate == null ? "0" : Math.Round(((DateTime)SDMS_ICTicket.ReachedDate).Minute, 5.0m).ToString();  



            if (SDMS_ICTicket.ServiceType != null)
            {
                ddlServiceType.SelectedValue = SDMS_ICTicket.ServiceType.ServiceTypeID.ToString();
                if (ddlServiceType.SelectedValue == "7")
                {
                    ddlServiceTypeOverhaul.Visible = true;
                }
                else
                {
                    ddlServiceTypeOverhaul.Visible = false;
                }

                if (SDMS_ICTicket.ServiceTypeOverhaul != null)
                    ddlServiceTypeOverhaul.SelectedValue = SDMS_ICTicket.ServiceTypeOverhaul.ServiceTypeOverhaulID.ToString();

                FillGetServiceSubType(SDMS_ICTicket.ServiceType.ServiceTypeID);
                if (ddlServiceType.SelectedValue == "3")
                {
                    ddlServiceSubType.Visible = true;
                }
                if (SDMS_ICTicket.ServiceSubType != null)
                {
                    ddlServiceSubType.SelectedValue = SDMS_ICTicket.ServiceSubType.ServiceSubTypeID.ToString();
                }
            }
            if (SDMS_ICTicket.ServicePriority != null)
                ddlServicePriority.SelectedValue = SDMS_ICTicket.ServicePriority.ServicePriorityID.ToString();
            lblHMRValue.Text = "Current HMR Value" + " ( " + SDMS_ICTicket.Equipment.EquipmentModel.Division.UOM + " ) ";
            txtHMRDate.Text = SDMS_ICTicket.CurrentHMRDate == null ? "" : ((DateTime)SDMS_ICTicket.CurrentHMRDate).ToShortDateString();
            txtHMRValue.Text = Convert.ToString(SDMS_ICTicket.CurrentHMRValue);

            new BDMS_Service().GetCategory1DDL(ddlCategory1, null, null);
            //FillCategory1();
            //if (SDMS_ICTicket.Category1 != null)
            //{
            //    ddlCategory1.SelectedValue = SDMS_ICTicket.Category1.Category1ID.ToString();
            //    //  FillCategory2();
            //    new BDMS_Service().GetCategory2DDL(ddlCategory2, Convert.ToInt32(ddlCategory1.SelectedValue), null, null);
            //    if (SDMS_ICTicket.Category2 != null)
            //    {
            //        ddlCategory2.SelectedValue = SDMS_ICTicket.Category2.Category2ID.ToString();
            //        FillCategory3();
            //        if (SDMS_ICTicket.Category3 != null)
            //        {
            //            ddlCategory3.SelectedValue = SDMS_ICTicket.Category3.Category3ID.ToString();
            //            FillCategory4();
            //            if (SDMS_ICTicket.Category4 != null)
            //            {
            //                ddlCategory4.SelectedValue = SDMS_ICTicket.Category4.Category4ID.ToString();
            //                FillCategory5();
            //                if (SDMS_ICTicket.Category5 != null)
            //                {
            //                    ddlCategory5.SelectedValue = SDMS_ICTicket.Category5.Category5ID.ToString();
            //                }
            //            }
            //        }
            //    }
            //}

            FillMainApplication();
            if (SDMS_ICTicket.MainApplication != null)
            {
                ddlMainApplication.SelectedValue = SDMS_ICTicket.MainApplication.MainApplicationID.ToString();
                FillSubApplication();
                if (SDMS_ICTicket.SubApplication != null)
                {
                    ddlSubApplication.SelectedValue = SDMS_ICTicket.SubApplication.SubApplicationID.ToString();
                    if (SDMS_ICTicket.SubApplication.SubApplicationID.ToString() == "26")
                    {
                        txtSubApplicationEntry.Visible = true;
                    }
                }
            }


            txtScopeOfWork.Text = SDMS_ICTicket.ScopeOfWork;


            txtKindAttn.Text = SDMS_ICTicket.KindAttn;
            txtRemarks.Text = SDMS_ICTicket.Remarks;

            txtOperatorName.Text = SDMS_ICTicket.SiteContactPersonName;
            txtSiteContactPersonNumber.Text = SDMS_ICTicket.SiteContactPersonNumber;
            txtSiteContactPersonNumber2.Text = SDMS_ICTicket.SiteContactPersonNumber2;


            EnableOrDesableBasedOnServiceCharges();

            new BDMS_SiteContactPersonDesignation().GetSiteContactPersonDesignationDDL(ddlDesignation, null, null);
            if (SDMS_ICTicket.SiteContactPersonDesignation != null)
            {
                ddlDesignation.SelectedValue = SDMS_ICTicket.SiteContactPersonDesignation.DesignationID.ToString();
            }
            if (SDMS_ICTicket.TypeOfWarranty != null)
            {
                ddlTypeOfWarranty.SelectedValue = SDMS_ICTicket.TypeOfWarranty.TypeOfWarrantyID.ToString();
            }
            cbCess.Checked = SDMS_ICTicket.IsCess;

            txtSubApplicationEntry.Text = SDMS_ICTicket.SubApplicationEntry;
            cbIsMachineActive.Checked = SDMS_ICTicket.IsMachineActive;

            cbNoClaim.Checked = SDMS_ICTicket.NoClaim;

            txtNoClaimReason.Text = SDMS_ICTicket.NoClaimReason;

            txtMcEnteredServiceDate.Text = SDMS_ICTicket.McEnteredServiceDate == null ? "" : ((DateTime)SDMS_ICTicket.McEnteredServiceDate).ToShortDateString();
            txtServiceStartedDate.Text = SDMS_ICTicket.ServiceStartedDate == null ? "" : ((DateTime)SDMS_ICTicket.ServiceStartedDate).ToShortDateString();
            txtServiceEndedDate.Text = SDMS_ICTicket.ServiceEndedDate == null ? "" : ((DateTime)SDMS_ICTicket.ServiceEndedDate).ToShortDateString();


        }

        private void FillGetServiceType(int IsWarranty)
        {
            ddlServiceType.DataSource = null;
            ddlServiceType.DataBind();
            ddlServiceType.DataTextField = "ServiceType";
            ddlServiceType.DataValueField = "ServiceTypeID";
            ddlServiceType.DataSource = new BDMS_Service().GetServiceType(null, null, IsWarranty);


            //if ((SDMS_ICTicket.Equipment.CommissioningOn == null) && SDMS_ICTicket.IsWarranty)
            //{
            //    ddlServiceType.DataSource = null;
            //    ddlServiceType.DataBind();
            //    ddlServiceType.DataTextField = "ServiceType";
            //    ddlServiceType.DataValueField = "ServiceTypeID";
            //    ddlServiceType.Items.Insert(0, new ListItem("Commission", "4"));
            //    ddlServiceType.Items.Insert(1, new ListItem("Pre Commission", "5"));
            //}



            //{
            //    ddlServiceType.DataSource = new BDMS_Service().GetServiceType(null, null, true);
            //}
            //else
            //{
            //    ddlServiceType.DataSource = new BDMS_Service().GetServiceType(null, null, false);
            //}

            ddlServiceType.DataBind();
            ddlServiceTypeOverhaul.DataSource = new BDMS_Service().GetServiceTypeddlServiceTypeOverhaul(null, null);
            ddlServiceTypeOverhaul.DataBind();
            ddlServiceTypeOverhaul.Items.Insert(0, new ListItem("Select", "0"));
            //   ddlServiceType.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillGetServicePriority()
        {
            ddlServicePriority.DataTextField = "ServicePriority";
            ddlServicePriority.DataValueField = "ServicePriorityID";
            ddlServicePriority.DataSource = new BDMS_Service().GetServicePriority(null, null);
            ddlServicePriority.DataBind();
            ddlServicePriority.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillGetDealerOffice()
        {
            ddlDealerOffice.DataTextField = "OfficeName_OfficeCode";
            ddlDealerOffice.DataValueField = "OfficeID";
            ddlDealerOffice.DataSource = new BDMS_Dealer().GetDealerOffice(SDMS_ICTicket.Dealer.DealerID, null, null);
            ddlDealerOffice.DataBind();
            ddlDealerOffice.Items.Insert(0, new ListItem("Select", "0"));
        }


        private void FillCategory3()
        {
            ddlCategory3.DataTextField = "Category3";
            ddlCategory3.DataValueField = "Category3ID";
            ddlCategory3.DataSource = new BDMS_Service().GetCategory3(Convert.ToInt32(ddlCategory2.SelectedValue), null, null);
            ddlCategory3.DataBind();
            ddlCategory3.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillCategory4()
        {
            ddlCategory4.DataTextField = "Category4";
            ddlCategory4.DataValueField = "Category4ID";
            ddlCategory4.DataSource = new BDMS_Service().GetCategory4(Convert.ToInt32(ddlCategory3.SelectedValue), null, null);
            ddlCategory4.DataBind();
            ddlCategory4.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillCategory5()
        {
            ddlCategory5.DataTextField = "Category5";
            ddlCategory5.DataValueField = "Category5ID";
            ddlCategory5.DataSource = new BDMS_Service().GetCategory5(Convert.ToInt32(ddlCategory4.SelectedValue), null, null);
            ddlCategory5.DataBind();
            ddlCategory5.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void FillMainApplication()
        {
            ddlMainApplication.DataTextField = "MainApplication";
            ddlMainApplication.DataValueField = "MainApplicationID";
            ddlMainApplication.DataSource = new BDMS_Service().GetMainApplication(null, null);
            ddlMainApplication.DataBind();
            ddlMainApplication.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void FillSubApplication()
        {
            ddlSubApplication.DataTextField = "SubApplication";
            ddlSubApplication.DataValueField = "SubApplicationID";
            ddlSubApplication.DataSource = new BDMS_Service().GetSubApplication(Convert.ToInt32(ddlMainApplication.SelectedValue), null, null);
            ddlSubApplication.DataBind();
            ddlSubApplication.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void ddlCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // FillCategory2();
            new BDMS_Service().GetCategory2DDL(ddlCategory2, Convert.ToInt32(ddlCategory1.SelectedValue), null, null);
            ddlCategory3.Items.Clear();
            ddlCategory4.Items.Clear();
            ddlCategory5.Items.Clear();
            //   btnSave.Focus();
        }

        protected void ddlCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCategory3();
            ddlCategory4.Items.Clear();
            ddlCategory5.Items.Clear();
            //  btnSave.Focus();
        }

        protected void ddlCategory3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCategory4();
            ddlCategory5.Items.Clear();
            //   btnSave.Focus();
        }

        protected void ddlCategory4_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCategory5();
            //   btnSave.Focus();
        }

        protected void ddlMainApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSubApplication();
            //    btnSave.Focus();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //   btnSave.Focus();
            if (!ValidationReached())
            {
                return;
            }
            int? ServiceType = ddlServiceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceType.SelectedValue);
            int? ServiceSubTypeID = ddlServiceType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceSubType.SelectedValue);
            if ((ServiceType == (short)DMS_ServiceType.NEPI) || (ServiceType == (short)DMS_ServiceType.Commission) || (ServiceType == (short)DMS_ServiceType.PreCommission))
            {
                if (ServiceSubTypeID != null)
                {
                    int Status = new BDMS_ICTicket().ValidateNEPIUsingServiceSubType((int)ServiceSubTypeID, SDMS_ICTicket.Equipment.EquipmentSerialNo, SDMS_ICTicket.ICTicketID);
                    if (Status != 0)
                    {
                        if (ServiceType == (short)DMS_ServiceType.NEPI)
                        {
                            lblMessage.Text = "This NEPI service already completed. You cannot use this code.";
                        }
                        else if (ServiceType == (short)DMS_ServiceType.Commission)
                        {
                            lblMessage.Text = "This Commission service already completed. You cannot use this code.";
                        }
                        else
                        {
                            lblMessage.Text = "This Pre Commission service already completed. You cannot use this code.";
                        }
                        return;
                    }
                }
            }

            DateTime? DepartureDate = null;
            DateTime? ReachedDate = null;

            DepartureDate = string.IsNullOrEmpty(txtDepartureDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtDepartureDate.Text.Trim() + " " + ddlDepartureHH.SelectedValue + ":" + ddlDepartureMM.SelectedValue);
            ReachedDate = string.IsNullOrEmpty(txtReachedDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtReachedDate.Text.Trim() + " " + ddlReachedHH.SelectedValue + ":" + ddlReachedMM.SelectedValue);

            if (DepartureDate > ReachedDate)
            {
                lblMessageCallInformation.ForeColor = Color.Red;
                lblMessageCallInformation.Visible = true;
                lblMessageCallInformation.Text = "Departure Date should be lesser then Reached Date";
                return;
            }

            int MTTR1H = (int)(((DateTime)ReachedDate - (DateTime)SDMS_ICTicket.RequestedDate).TotalHours);
            if (MTTR1H > 8)
            {
                List<PDMS_ServiceNote> Note = new BDMS_Service().GetServiceNote(SDMS_ICTicket.ICTicketID, null, null, "");
                if (Note.Count == 0)
                {
                    lblMessageCallInformation.ForeColor = Color.Red;
                    lblMessageCallInformation.Visible = true;
                    lblMessageCallInformation.Text = "This ticket crossed 8 hours. Please update \"Reason for Delay\" in Notes.";
                    return;
                }
            }

            string Location = txtLocation.Text.Trim();
            int? DealerOffice = ddlDealerOffice.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerOffice.SelectedValue);

            DateTime? HMRDate = string.IsNullOrEmpty(txtHMRDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtHMRDate.Text.Trim());
            int? HMRValue = string.IsNullOrEmpty(txtHMRValue.Text.Trim()) ? (int?)null : Convert.ToInt32(txtHMRValue.Text.Trim());

            int? TypeOfWarrantyID = ddlTypeOfWarranty.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlTypeOfWarranty.SelectedValue);


            int? ServiceTypeOverhaul = ddlServiceTypeOverhaul.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServiceTypeOverhaul.SelectedValue);
            int? ServicePriority = ddlServicePriority.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlServicePriority.SelectedValue);

            //int? Category1 = null;
            //int? Category2 = null;
            //int? Category3 = null;
            //int? Category4 = null;
            //int? Category5 = null;
            //if (ddlCategory1.SelectedValue != "0")
            //{
            //    Category1 = Convert.ToInt32(ddlCategory1.SelectedValue);
            //    if (ddlCategory2.SelectedValue != "0")
            //    {
            //        Category2 = Convert.ToInt32(ddlCategory2.SelectedValue);
            //        if (ddlCategory3.SelectedValue != "0")
            //        {
            //            Category3 = Convert.ToInt32(ddlCategory3.SelectedValue);
            //            if (ddlCategory4.SelectedValue != "0")
            //            {
            //                Category4 = Convert.ToInt32(ddlCategory4.SelectedValue);
            //                Category5 = ddlCategory5.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCategory5.SelectedValue);
            //            }
            //        }
            //    }
            //}

            int? MainApplication = null;
            int? SubApplication = null;
            if (ddlMainApplication.SelectedValue != "0")
            {
                MainApplication = Convert.ToInt32(ddlMainApplication.SelectedValue);
                SubApplication = ddlSubApplication.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlSubApplication.SelectedValue);
            }

            lblMessageCallInformation.ForeColor = Color.Red;
            lblMessageCallInformation.Visible = true;
            CheckBox UCcbIsWarranty = (CheckBox)UC_BasicInformation.FindControl("cbIsWarranty");


            int? DesignationID = null;
            if (ddlDesignation.SelectedValue != "0")
            {
                DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
            }
            string SubApplicationEntry = txtSubApplicationEntry.Text.Trim();

            DateTime? McEnteredServiceDate = string.IsNullOrEmpty(txtMcEnteredServiceDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtMcEnteredServiceDate.Text.Trim()); ;
            DateTime? ServiceStartedDate = string.IsNullOrEmpty(txtServiceStartedDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtServiceStartedDate.Text.Trim()); ;
            DateTime? ServiceEndedDate = string.IsNullOrEmpty(txtServiceEndedDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtServiceEndedDate.Text.Trim()); ;

            PDMS_Customer Dealer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Dealer.DealerCode);
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);

            Boolean IsIGST = true;
            if (Dealer.StateCode == Customer.StateCode)
                IsIGST = false;


            if (new BDMS_ICTicket().InsertOrUpdateServiceConfirmation(SDMS_ICTicket.ICTicketID, Location, DealerOffice, DepartureDate, ReachedDate, ServiceType, ServiceSubTypeID, ServiceTypeOverhaul, ServicePriority,
                  HMRDate, HMRValue, UCcbIsWarranty.Checked, TypeOfWarrantyID, MainApplication, SubApplication, txtScopeOfWork.Text.Trim(),
                  txtKindAttn.Text.Trim(), txtRemarks.Text.Trim(), txtOperatorName.Text.Trim(), txtSiteContactPersonNumber.Text.Trim(), txtSiteContactPersonNumber2.Text.Trim(), DesignationID, cbCess.Checked, cbIsMachineActive.Checked
                  , SubApplicationEntry, PSession.User.UserID, cbNoClaim.Checked, txtNoClaimReason.Text.Trim(), McEnteredServiceDate, ServiceStartedDate, ServiceEndedDate, IsIGST))
            {
                lblMessageCallInformation.Text = "ICTicket is updated successfully";
                lblMessageCallInformation.ForeColor = Color.Green;

                List<string> querys = new List<string>();


                string f_bill_to = "";
                string r_insurance_p = "Seller";
                string f_order_type = "101";
                string s_object_type = "101";
                string r_remarks = "";
                string r_frieght_p = "Seller";
                string r_auto = "false";
                string r_ref_obj_name = "";
                string r_ref_obj_type = "null";
                string r_price_grp = "07";


                if (SDMS_ICTicket.IsWarranty)
                {
                    f_bill_to = "B001";
                    r_insurance_p = "";
                    f_order_type = "108";
                    s_object_type = "108";
                    r_remarks = "In Warranty Quotation";
                    r_frieght_p = "";
                    r_auto = "null";
                    r_ref_obj_name = "dsprr_psc_hdr";
                    r_ref_obj_type = "101";
                    r_price_grp = "";
                }
                if (ServiceType == (short)DMS_ServiceType.OverhaulService)
                {
                    f_order_type = "302";
                    s_object_type = "302";
                    r_ref_obj_name = "Ser-Center-Quotation";
                    r_remarks = "Overhaul Service Quotation";
                }
                else if (ServiceType == (short)DMS_ServiceType.PolicyWarranty)
                {
                    f_order_type = "108";
                    s_object_type = "108";
                    r_ref_obj_name = "Policy Warranty";
                    r_remarks = "Policy Warranty Quotation";
                }
                else if (ServiceType == (short)DMS_ServiceType.PartsWarranty)
                {
                    f_order_type = "108";
                    s_object_type = "108";
                    r_ref_obj_name = "Parts Warranty";
                    r_remarks = "Parts Warranty Quotation";
                }
                else if (ServiceType == (short)DMS_ServiceType.GoodwillWarranty)
                {
                    f_order_type = "108";
                    s_object_type = "108";
                    r_ref_obj_name = "Goodwil Warranty";
                    r_remarks = "Goodwil Warranty Quotation";
                }
                string Q = "f_bill_to = '" + f_bill_to + "', r_insurance_p = '" + r_insurance_p + "' ,f_order_type = '"
                    + f_order_type + "', s_object_type = '" + s_object_type + "',r_remarks='" + r_remarks + "',  r_frieght_p = '"
                    + r_frieght_p + "',  r_auto = " + r_auto + ", r_ref_obj_name = '" + r_ref_obj_name + "', r_ref_obj_type = "
                    + r_ref_obj_type + ",r_price_grp = '" + r_price_grp + "' ";

                foreach (PDMS_ServiceMaterial SM in SS_ServiceMaterial)
                {
                    if (!string.IsNullOrEmpty(SM.QuotationNumber))
                    {
                        string query = "update dssor_sales_order_hdr set  " + Q + "  where s_tenant_id =" + SDMS_ICTicket.Dealer.DealerCode + " and 	p_so_id ='" + SM.QuotationNumber + "'";
                        querys.Add(query);
                    }
                }
                if (querys.Count > 0)
                {
                    Boolean Status = new NpgsqlServer().UpdateTransactions(querys);
                }

                long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);

                if (SDMS_ICTicket.ServiceType != null)
                    HttpContext.Current.Session["ServiceTypeID"] = SDMS_ICTicket.ServiceType.ServiceTypeID;
                //  FillBasicInformation();
                FillCallInformation();

                ValidateReachedDate();
            }
            else
            {
                lblMessageCallInformation.Text = "ICTicket is not updated successfully";
            }

        }
        Boolean ValidationReached()
        {
            lblMessageCallInformation.ForeColor = Color.Red;
            lblMessageCallInformation.Visible = true;
            txtReachedDate.BorderColor = Color.Silver;
            txtLocation.BorderColor = Color.Silver;
            Boolean Ret = true;
            string Message = "";

            if (string.IsNullOrEmpty(txtDepartureDate.Text.Trim()))
            {
                Message = Message + "<br/> Please Enter the Departure Date";
                Ret = false;
            }
            if (ddlDepartureHH.SelectedValue == "-1")
            {
                Message = Message + "<br/> Please Enter the Departure Hour";
                Ret = false;
            }
            if (ddlDepartureMM.SelectedValue == "0")
            {
                Message = Message + "<br/> Please Enter the Departure Minute";
                Ret = false;
            }


            if (string.IsNullOrEmpty(txtReachedDate.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Reached Date";
                Ret = false;
                txtReachedDate.BorderColor = Color.Red;
            }
            else
            {
                if (ddlReachedHH.SelectedValue == "-1")
                {
                    Message = Message + "<br/>Please select the Reached Hour";
                    Ret = false;
                    ddlReachedHH.BorderColor = Color.Red;
                }

                if (ddlReachedMM.SelectedValue == "0")
                {
                    Message = Message + "<br/>Please select the Reached Minute";
                    Ret = false;
                    ddlReachedMM.BorderColor = Color.Red;
                }
                //DateTime? ReachedDate = string.IsNullOrEmpty(txtReachedDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtReachedDate.Text.Trim() + " " + ddlReachedHH.SelectedValue + ":" + ddlReachedMM.SelectedValue);


                //if (SDMS_ICTicket.RequestedDate > ReachedDate)
                //{
                //    Message = Message + "<br/>Requested date should not be less than Reached date.";
                //    Ret = false;
                //    txtReachedDate.BorderColor = Color.Red;
                //}
            }

            if (string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the Location";
                Ret = false;
                txtLocation.BorderColor = Color.Red;
            }
            lblMessageCallInformation.Text = Message;
            if (!Ret)
            {
                return Ret;
            }

            int value;
            if (!string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
            {
                if (!int.TryParse("0" + txtHMRValue.Text, out value))
                {
                    Message = Message + "<br/> Please enter integer in HMR Value";
                    Ret = false;
                    txtHMRValue.BorderColor = Color.Red;
                }
                if (SDMS_ICTicket.LastHMRValue > Convert.ToInt32(txtHMRValue.Text.Trim()))
                {
                    Message = Message + "<br/>HMR value should not be less than last HMR value.";
                    Ret = false;
                    txtHMRValue.BorderColor = Color.Red;
                }
            }

            if (!string.IsNullOrEmpty(txtHMRDate.Text.Trim()))
            {
                if (Convert.ToDateTime(txtReachedDate.Text.Trim()) > Convert.ToDateTime(txtHMRDate.Text.Trim()))
                {
                    Message = Message + "<br/>HMR date should not be less than Reached date.";
                    Ret = false;
                    txtHMRDate.BorderColor = Color.Red;
                }
            }
            lblMessageCallInformation.Text = Message;
            return Ret;
        }

        Boolean ValidatetionRestore()
        {
            ddlServiceType.BorderColor = Color.Silver;
            ddlServicePriority.BorderColor = Color.Silver;
            ddlDealerOffice.BorderColor = Color.Silver;
            txtHMRDate.BorderColor = Color.Silver;
            txtHMRValue.BorderColor = Color.Silver;
            ddlCategory1.BorderColor = Color.Silver;
            ddlCategory2.BorderColor = Color.Silver;
            ddlCategory3.BorderColor = Color.Silver;
            ddlCategory4.BorderColor = Color.Silver;

            ddlMainApplication.BorderColor = Color.Silver;
            ddlSubApplication.BorderColor = Color.Silver;

            lblMessageCallInformation.Visible = true;
            string Message = "";
            Boolean Ret = true;
            if (ddlServiceType.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Service Type";
                Ret = false;
                ddlServiceType.BorderColor = Color.Red;
            }
            if (ddlServicePriority.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Service Priority";
                Ret = false;
                ddlServicePriority.BorderColor = Color.Red;
            }

            if (ddlDealerOffice.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Delivery Location";
                Ret = false;
                ddlDealerOffice.BorderColor = Color.Red;
            }


            if (string.IsNullOrEmpty(txtHMRDate.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the HMR Date";
                Ret = false;
                txtHMRDate.BorderColor = Color.Red;
            }
            else
            {
                if (Convert.ToDateTime(txtReachedDate.Text.Trim()) > Convert.ToDateTime(txtHMRDate.Text.Trim()))
                {
                    Message = Message + "<br/>HMR date should not be less than Reached date.";
                    Ret = false;
                    txtHMRDate.BorderColor = Color.Red;
                }
            }
            int value;
            if (string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
            {
                Message = Message + "<br/>Please enter the HMR Value";
                Ret = false;
                txtHMRValue.BorderColor = Color.Red;
            }
            else
            {
                if (!int.TryParse("0" + txtHMRValue.Text, out value))
                {
                    Message = Message + "<br/>Please enter integer in HMR Value";
                    Ret = false;
                    txtHMRValue.BorderColor = Color.Red;
                }
                if (SDMS_ICTicket.LastHMRValue > Convert.ToInt32(txtHMRValue.Text.Trim()))
                {
                    Message = Message + "<br/>HMR value should not be less than last HMR value.";
                    Ret = false;
                    txtHMRValue.BorderColor = Color.Red;
                }
            }

            if (ddlCategory1.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Category1";
                Ret = false;
                ddlCategory1.BorderColor = Color.Red;
                ddlCategory2.BorderColor = Color.Red;
                ddlCategory3.BorderColor = Color.Red;
                ddlCategory4.BorderColor = Color.Red;
            }
            else if (ddlCategory2.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Category2";
                Ret = false;
                ddlCategory2.BorderColor = Color.Red;
                ddlCategory3.BorderColor = Color.Red;
                ddlCategory4.BorderColor = Color.Red;
            }
            else if (ddlCategory3.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Category3";
                Ret = false;
                ddlCategory3.BorderColor = Color.Red;
                ddlCategory4.BorderColor = Color.Red;
            }
            else if (ddlCategory4.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Category4";
                Ret = false;
                ddlCategory4.BorderColor = Color.Red;
            }




            if (ddlMainApplication.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Main Application";
                Ret = false;
                ddlMainApplication.BorderColor = Color.Red;
                ddlSubApplication.BorderColor = Color.Red;
            }
            else if (ddlSubApplication.SelectedValue == "0")
            {
                Message = Message + "<br/>Please select the Sub Application";
                Ret = false;
                ddlSubApplication.BorderColor = Color.Red;
            }

            //   List<PDMS_ServiceCharge> ServiceCharges = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);

            if (SS_ServiceCharge.Count == 0)
            {
                Message = Message + "<br/>Please add service code in service charges screen.";
                Ret = false;
            }



            lblMessageCallInformation.Text = Message;
            return Ret;

        }

        protected void txtHMRValue_TextChanged(object sender, EventArgs e)
        {
            WarrantyCalculation();
        }
        protected void txtReachedDate_TextChanged(object sender, EventArgs e)
        {
            ValidateReachedDate();
            WarrantyCalculation();
            //  btnSave.Focus();
        }
        void ValidateReachedDate()
        {
            if (string.IsNullOrEmpty(txtReachedDate.Text.Trim()))
            {

                txtHMRDate.Text = "";
                txtHMRDate.Enabled = false;
                return;
            }
            ceHMRDate.StartDate = Convert.ToDateTime(txtReachedDate.Text);
            ceHMRDate.EndDate = DateTime.Now;

            //  List<PDMS_ServiceCharge> ServiceCharges = new BDMS_Service().GetServiceCharges(SDMS_ICTicket.ICTicketID, null, "", false);
            // List<PDMS_ServiceMaterial> ServiceMaterial = new BDMS_Service().GetServiceMaterials(SDMS_ICTicket.ICTicketID, null, "", false); 
            if (SS_ServiceCharge.Count + SS_ServiceMaterial.Count == 0)
            {
                txtHMRDate.Enabled = true;
            }
        }
        public void EnableOrDesableBasedOnServiceCharges()
        {
            //if (SS_ServiceCharge.Count + SS_ServiceMaterial.Count != 0)
            //{
            //    txtReachedDate.Enabled = false;
            //    ddlReachedHH.Enabled = false;
            //    ddlReachedMM.Enabled = false;
            //    txtHMRDate.Enabled = false;
            //    txtHMRValue.Enabled = false;
            //    ddlDealerOffice.Enabled = false;
            //}
            //else
            //{
            //    txtReachedDate.Enabled = true;
            //    ddlReachedHH.Enabled = true;
            //    ddlReachedMM.Enabled = true;
            //    txtHMRDate.Enabled = true;
            //    txtHMRValue.Enabled = true;
            //    ddlDealerOffice.Enabled = true;
            //}

            string ClaimNumber = "";
            if (SS_ServiceCharge.Count != 0)
            { ClaimNumber = SS_ServiceCharge[0].ClaimNumber; }

            if (string.IsNullOrEmpty(ClaimNumber)) { ddlServiceType.Enabled = true; }
            else { ddlServiceType.Enabled = false; }

            string QuotationNumber = "";
            if (SS_ServiceMaterial.Count != 0)
            {
                foreach (PDMS_ServiceMaterial ServiceMaterial in SS_ServiceMaterial)
                {
                    QuotationNumber = ServiceMaterial.QuotationNumber;
                    if (!string.IsNullOrEmpty(QuotationNumber)) { break; }
                }
            }
            if (string.IsNullOrEmpty(QuotationNumber))
            {
                ddlDealerOffice.Enabled = true;
                txtHMRDate.Enabled = true;
                txtHMRValue.Enabled = true;
                txtReachedDate.Enabled = true;
                ddlReachedHH.Enabled = true;
                ddlReachedMM.Enabled = true;
            }
            else
            {
                ddlDealerOffice.Enabled = false;
                txtHMRDate.Enabled = false;
                txtHMRValue.Enabled = false;
                txtReachedDate.Enabled = false;
                ddlReachedHH.Enabled = false;
                ddlReachedMM.Enabled = false;
            }

            UC_BasicInformation.SDMS_ICTicket = SDMS_ICTicket;
            UC_BasicInformation.FillBasicInformation();
            WarrantyCalculation();
        }

        void WarrantyCalculation()
        {
            CheckBox UCcbIsWarranty = (CheckBox)UC_BasicInformation.FindControl("cbIsWarranty");
            if (SDMS_ICTicket.Equipment.IsAMC == true)
            {
            }
            else if ((SDMS_ICTicket.Equipment.IsRefurbished == true) && (SDMS_ICTicket.Equipment.RefurbishedBy == SDMS_ICTicket.Dealer.DealerID) && (SDMS_ICTicket.Equipment.RFWarrantyExpiryDate >= SDMS_ICTicket.ICTicketDate))
            {
                //  UCcbIsWarranty.Checked = true;
                //    FillGetServiceType(2);
            }
            else if (SDMS_ICTicket.IsMarginWarranty == true)
            {
                //  UCcbIsWarranty.Checked = true;
                //   FillGetServiceType(1);
            }
            else if (SDMS_ICTicket.Equipment.EquipmentModel.Division.UOM == "Cum")
            {
            }
            else
            {
                if (!string.IsNullOrEmpty(txtHMRValue.Text.Trim()))
                {

                    Boolean Old = UCcbIsWarranty.Checked;
                    int vHMR = 2000;
                    DateTime cD = ((DateTime)SDMS_ICTicket.Equipment.WarrantyExpiryDate).AddYears(-1);
                    if (cD < Convert.ToDateTime("01/11/2018"))
                    {
                        vHMR = 1000;
                    }

                    if (SDMS_ICTicket.Equipment.EquipmentWarrantyType != null)
                    {
                        if (SDMS_ICTicket.Equipment.EquipmentWarrantyType.HMR != 0)
                        {
                            vHMR = SDMS_ICTicket.Equipment.EquipmentWarrantyType.HMR;
                        }
                    }

                    if ((Convert.ToInt32(txtHMRValue.Text.Trim()) > vHMR) || (SDMS_ICTicket.Equipment.WarrantyExpiryDate < SDMS_ICTicket.ICTicketDate))
                    {
                        UCcbIsWarranty.Checked = false;
                    }
                    else
                    {
                        UCcbIsWarranty.Checked = true;
                    }
                    if (Old != UCcbIsWarranty.Checked)
                    {
                        FillGetServiceType(Convert.ToInt32(UCcbIsWarranty.Checked));
                    }
                }
            }
        }

        protected void ddlSubApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubApplication.SelectedValue == "26")
            {
                txtSubApplicationEntry.Visible = true;
            }
            else
            {
                txtSubApplicationEntry.Visible = false;
            }
        }

        protected void ddlServiceType_TextChanged(object sender, EventArgs e)
        {
            int ServiceTypeID = Convert.ToInt32(ddlServiceType.SelectedValue);
            if (ddlServiceType.SelectedValue == "7")
            {
                ddlServiceTypeOverhaul.Visible = true;
            }
            else
            {
                ddlServiceTypeOverhaul.Visible = false;
                ddlServiceTypeOverhaul.SelectedIndex = 0;
            }
            FillGetServiceSubType(Convert.ToInt32(ddlServiceType.SelectedValue));


            if (ddlServiceSubType.Items.Count > 1)
            {
                ddlServiceSubType.Visible = true;
            }
            else
            {
                ddlServiceSubType.Visible = false;
            }


            //if (ddlServiceType.SelectedValue == "3")
            //{
            //    ddlServiceSubType.Visible = true;
            //}
            //else
            //{
            //    ddlServiceSubType.Visible = false;
            //}
        }
        private void FillGetServiceSubType(int ServiceTypeID)
        {
            ddlServiceSubType.DataSource = new BDMS_Service().GetServiceSubType(null, ServiceTypeID);
            ddlServiceSubType.DataBind();
        }

        protected void txtSubApplicationEntry_TextChanged(object sender, EventArgs e)
        {

        }
    }
}