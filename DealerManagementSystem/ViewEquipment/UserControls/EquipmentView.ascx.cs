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
using System.Data;

namespace DealerManagementSystem.ViewEquipment.UserControls
{
    public partial class EquipmentView : System.Web.UI.UserControl
    {
        public PDMS_EquipmentHeader EquipmentViewDet
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
        public List<PDMS_ICTicket> ICTickets1
        {
            get
            {
                if (Session["DMS_EquipmentHistory1"] == null)
                {
                    Session["DMS_EquipmentHistory1"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["DMS_EquipmentHistory1"];
            }
            set
            {
                Session["DMS_EquipmentHistory1"] = value;
            }
        }
        public List<PEquipmentAttachedFilee_Insert> AttachedFileTemp
        {
            get
            {
                if (Session["PEquipmentAttachedFileEquipmentView"] == null)
                {
                    Session["PEquipmentAttachedFileEquipmentView"] = new List<PEquipmentAttachedFilee_Insert>();
                }
                return (List<PEquipmentAttachedFilee_Insert>)Session["PEquipmentAttachedFileEquipmentView"];
            }
            set
            {
                Session["PEquipmentAttachedFileEquipmentView"] = value;
            }
        }
        public DataTable EquipmentChgReqHst
        {
            get
            {
                if (Session["EquipmentChgReqHst"] == null)
                {
                    Session["EquipmentChgReqHst"] = new DataTable();
                }
                return (DataTable)Session["EquipmentChgReqHst"];
            }
            set
            {
                Session["EquipmentChgReqHst"] = value;
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

            EquipmentViewDet = new BDMS_Equipment().GetEquipmentHeaderByID(Convert.ToInt32(EquipmentHeaderID));
            EquipmentViewDet.Customer = new BDMS_Customer().GetCustomerByID(EquipmentViewDet.Customer.CustomerID);
            lblModel.Text = EquipmentViewDet.EquipmentModel.Model;
            lblModelDescription.Text = EquipmentViewDet.EquipmentModel.ModelDescription;
            lblEngineSerialNo.Text = EquipmentViewDet.EngineSerialNo;
            lblEquipmentSerialNo.Text = EquipmentViewDet.EquipmentSerialNo;
            //lblDistrict.Text = EquipmentViewDet.Customer.District.District;
            lblDistrict.Text = EquipmentViewDet.Customer.District == null ? "" : Convert.ToString(EquipmentViewDet.Customer.District.District);
            //lblState.Text = EquipmentViewDet.Customer.State.State;
            lblState.Text = EquipmentViewDet.Customer.State == null ? "" : Convert.ToString(EquipmentViewDet.Customer.State.State);
            lblDispatchedOn.Text = EquipmentViewDet.DispatchedOn == null ? "" : ((DateTime)EquipmentViewDet.DispatchedOn).ToShortDateString();
            lblWarrantyExpiryDate.Text = EquipmentViewDet.WarrantyExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.WarrantyExpiryDate).ToShortDateString();
            lblEngineModel.Text = EquipmentViewDet.EngineModel;
            lblCurrentHMRValue.Text = EquipmentViewDet.CurrentHMRValue.ToString();
            lblCommisioningOn.Text = EquipmentViewDet.CommissioningOn == null ? "" : ((DateTime)EquipmentViewDet.CommissioningOn).ToShortDateString();
            lblCurrentHMRDate.Text = EquipmentViewDet.CurrentHMRDate == null ? "" : ((DateTime)EquipmentViewDet.CurrentHMRDate).ToShortDateString();
            //EquipmentViewDet.IsRefurbished;
            //EquipmentViewDet.RefurbishedBy;
            lblRFWarrantyStartDate.Text = EquipmentViewDet.RFWarrantyStartDate == null ? "" : ((DateTime)EquipmentViewDet.RFWarrantyStartDate).ToShortDateString();
            lblRFWarrantyExpiryDate.Text = EquipmentViewDet.RFWarrantyExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.RFWarrantyExpiryDate).ToShortDateString();
            cbIsAMC.Checked = EquipmentViewDet.IsAMC == null ? false : Convert.ToBoolean(EquipmentViewDet.IsAMC);
            lblAMCStartDate.Text = EquipmentViewDet.AMCStartDate == null ? "" : ((DateTime)EquipmentViewDet.AMCStartDate).ToShortDateString();
            lblAMCExpiryDate.Text = EquipmentViewDet.AMCExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.AMCExpiryDate).ToShortDateString();
            lblTypeOfWheelAssembly.Text = EquipmentViewDet.TypeOfWheelAssembly;
            lblMaterialCode.Text = EquipmentViewDet.Material.MaterialCode;
            lblChassisSlNo.Text = EquipmentViewDet.ChassisSlNo;
            lblESN.Text = EquipmentViewDet.ESN;
            lblPlant.Text = EquipmentViewDet.Plant;
            lblSpecialVariants.Text = EquipmentViewDet.SpecialVariants;
            lblProductionStatus.Text = EquipmentViewDet.ProductionStatus;
            lblVariantsFittingDate.Text = EquipmentViewDet.VariantsFittingDate == null ? "" : ((DateTime)EquipmentViewDet.VariantsFittingDate).ToShortDateString();
            lblManufacturingDate.Text = Convert.ToString(EquipmentViewDet.ManufacturingDate);
            if (EquipmentViewDet.Ibase != null)
            {
                lblInstalledBaseNo.Text = EquipmentViewDet.Ibase.InstalledBaseNo;
                lblIBaseLocation.Text = EquipmentViewDet.Ibase.IBaseLocation;
                lblDeliveryDate.Text = EquipmentViewDet.Ibase.DeliveryDate == null ? "" : ((DateTime)EquipmentViewDet.Ibase.DeliveryDate).ToShortDateString();
                lblIBaseCreatedOn.Text = EquipmentViewDet.Ibase.IBaseCreatedOn == null ? "" : ((DateTime)EquipmentViewDet.Ibase.IBaseCreatedOn).ToShortDateString();
                lblIbaseWarrantyStart.Text = EquipmentViewDet.Ibase.WarrantyStart == null ? "" : ((DateTime)EquipmentViewDet.Ibase.WarrantyStart).ToShortDateString();
                lblIbaseWarrantyEnd.Text = EquipmentViewDet.Ibase.WarrantyEnd == null ? "" : ((DateTime)EquipmentViewDet.Ibase.WarrantyEnd).ToShortDateString();
                lblFinancialYearOfDispatch.Text = Convert.ToString(EquipmentViewDet.Ibase.FinancialYearOfDispatch);
                lblMajorRegion.Text = EquipmentViewDet.Ibase.MajorRegion.Region;
            }
            lblWarrantyType.Text = EquipmentViewDet.EquipmentWarrantyType == null ? "" : EquipmentViewDet.EquipmentWarrantyType.Description;
            CustomerViewSoldTo.fillCustomer(EquipmentViewDet.Customer);
            fillEquipmentService();
            ActionControlMange();
            //fillWarrantyTypeChangeSupportDocument();
            //fillOwnershipChangeSupportDocument();
            //fillWarrantyExpiryDateChangeSupportDocument();
            fillSupportDocument();
            fillChangeRequestHistory();
        }
        void fillEquipmentService()
        {
            DataSet ds = new BDMS_Equipment().GetEquipmentHistory(null, lblEquipmentSerialNo.Text.Trim());

            if (ds.Tables.Count == 0)
            {
                gvICTickets1.DataSource = null;
                gvICTickets1.DataBind();
                return;
            }
            ICTickets1 = GetEquipmentDT1toClass(ds.Tables[1]);

            //gvICTickets1.DataSource = ICTickets1;
            //gvICTickets1.DataBind();
            EquipmentServiceBind();
        }
        public List<PDMS_ICTicket> GetEquipmentDT1toClass(DataTable dt)
        {
            TraceLogger.Log(DateTime.Now);
            List<PDMS_ICTicket> ICTickets = new List<PDMS_ICTicket>();
            try
            {

                PDMS_ICTicket ICTicket = new PDMS_ICTicket();

                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ICTicket = new PDMS_ICTicket();
                        ICTickets.Add(ICTicket);
                        ICTicket.Equipment = new PDMS_EquipmentHeader()
                        {
                            EquipmentHeaderID = Convert.ToInt64(dr["EquipmentHeaderID"]),
                            EquipmentModel = new PDMS_Model()
                            {
                                Model = Convert.ToString(dr["EquipmentModel"]),
                                // Division = new PDMS_Division() {  DivisionCode = Convert.ToString(dr["DivisionCode"]), DivisionDescription = Convert.ToString(dr["DivisionDescription"]) }
                            },
                            EquipmentSerialNo = Convert.ToString(dr["EquipmentSerialNo"]),
                        };
                        ICTicket.Customer = new PDMS_Customer() { CustomerCode = Convert.ToString(dr["CustomerCode"]), CustomerName = Convert.ToString(dr["CustomerName"]) };
                        ICTicket.Dealer = new PDMS_Dealer() { DealerCode = Convert.ToString(dr["DealerCode"]), DealerName = Convert.ToString(dr["DealerName"]) };
                        ICTicket.ComplaintDescription = Convert.ToString(dr["ComplaintDescription"]);
                        ICTicket.ServiceMaterial = new PDMS_ServiceCharge()
                        {
                            Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["SMaterialCode"]), MaterialDescription = Convert.ToString(dr["SMaterialDescription"]) },
                        };
                        ICTicket.ICTicketNumber = Convert.ToString(dr["ICTicketNumber"]);
                        ICTicket.RequestedDate = DBNull.Value == dr["RequestedDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RequestedDate"]);
                        ICTicket.RestoreDate = DBNull.Value == dr["RestoreDate"] ? (DateTime?)null : Convert.ToDateTime(dr["RestoreDate"]);
                        ICTicket.ServiceType = new PDMS_ServiceType() { ServiceType = Convert.ToString(dr["ServiceType"]) };
                        ICTicket.CurrentHMRValue = DBNull.Value == dr["CurrentHMRValue"] ? 0 : Convert.ToInt32(dr["CurrentHMRValue"]);
                        ICTicket.ServiceMaterialM = new PDMS_ServiceMaterial()
                        {
                            Material = new PDMS_Material() { MaterialCode = Convert.ToString(dr["MaterialCode"]), MaterialDescription = Convert.ToString(dr["MaterialDescription"]) },
                            TSIR = new PDMS_ICTicketTSIR() { TsirNumber = Convert.ToString(dr["TsirNumber"]) }
                        };
                    }
                }
                return ICTickets;
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("BDMS_Equipment", "GetEquipment", ex);
                throw ex;
            }
        }
        protected void ibtnServiceArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets1.PageIndex > 0)
            {
                gvICTickets1.PageIndex = gvICTickets1.PageIndex - 1;
                EquipmentServiceBind();
            }
        }
        protected void ibtnServiceArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets1.PageCount > gvICTickets1.PageIndex)
            {
                gvICTickets1.PageIndex = gvICTickets1.PageIndex + 1;
                EquipmentServiceBind();
            }
        }
        void EquipmentServiceBind()
        {
            gvICTickets1.DataSource = ICTickets1;
            gvICTickets1.DataBind();
            lblRowCountService.Text = (((gvICTickets1.PageIndex) * gvICTickets1.PageSize) + 1) + " - " + (((gvICTickets1.PageIndex) * gvICTickets1.PageSize) + gvICTickets1.Rows.Count) + " of " + ICTickets1.Count;
        }
        protected void gvICTickets1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvICTickets1.PageIndex = e.NewPageIndex;
            fillEquipmentService();
        }
        protected void lnkBtnActions_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbActions = ((LinkButton)sender);

                lblMessage.Visible = true;

                if (lbActions.Text == "Update Commissioning Date")
                {
                    lblMessageUpdateCommissioningDate.Text = "";
                    lblMessageUpdateCommissioningDate.Visible = false;
                    lblCustomerC.Text = EquipmentViewDet.Customer.CustomerFullName;
                    lblModelC.Text = EquipmentViewDet.EquipmentModel.Model;
                    lblEquipmentSerialNoC.Text = EquipmentViewDet.EquipmentSerialNo;
                    lblHMRC.Text = EquipmentViewDet.CurrentHMRValue.ToString();
                    txtCommissioningDate.Text = "";
                    MPE_UpdateCommiDate.Show();
                }
                if (lbActions.Text == "Warranty Type Change Request")
                {
                    lblMessageWarrantyTypeChangeReq.Text = "";
                    lblMessageWarrantyTypeChangeReq.Visible = false;
                    gvWarrantyTypeSupportDocument.DataSource = null;
                    gvWarrantyTypeSupportDocument.DataBind();
                    AttachedFileTemp.Clear();
                    //txtWarrantyStartDate.Text = "";
                    //txtWarrantyEndDate.Text = "";
                    //txtWarrantyHMRValue.Text = "";
                    new DDLBind(ddlWarranty, new BDMS_Equipment().GetEquipmentWarrantyType(null, null), "Description", "EquipmentWarrantyTypeID");
                    //ddlWarranty.SelectedValue = EquipmentViewDet.EquipmentWarrantyType.EquipmentWarrantyTypeID.ToString();
                    ddlWarranty.SelectedValue = EquipmentViewDet.EquipmentWarrantyType == null ? "0" : EquipmentViewDet.EquipmentWarrantyType.EquipmentWarrantyTypeID.ToString();
                    lblCustomer.Text = EquipmentViewDet.Customer.CustomerFullName;
                    lblModelP.Text = EquipmentViewDet.EquipmentModel.Model;
                    lblEquipmentSerialNoP.Text = EquipmentViewDet.EquipmentSerialNo;
                    MPE_WarrantyTypeChangeReq.Show();
                }
                if (lbActions.Text == "Ownership Change Request")
                {
                    lblMessageOwnershipChangeReq.Text = "";
                    lblMessageOwnershipChangeReq.Visible = false;
                    gvOwnershipChangeReqSupportDocument.DataSource = null;
                    gvOwnershipChangeReqSupportDocument.DataBind();
                    AttachedFileTemp.Clear();
                    txtBoxCustomerOwnershipChange.Text = "";
                    txtSoldDate.Text = "";
                    lblCustomerOwnership.Text = EquipmentViewDet.Customer.CustomerFullName;
                    lblModelOwnership.Text = EquipmentViewDet.EquipmentModel.Model;
                    lblEquipmentSerialNoOwnership.Text = EquipmentViewDet.EquipmentSerialNo;
                    MPE_OwnershipChangeReq.Show();
                }
                if (lbActions.Text == "Expiry Date Change Request")
                {
                    lblMessageWarrantyExpiryDateChangeReq.Text = "";
                    lblMessageWarrantyExpiryDateChangeReq.Visible = false;
                    gvWarrantyExpiryDateChangeSupportDocument.DataSource = null;
                    gvWarrantyExpiryDateChangeSupportDocument.DataBind();
                    AttachedFileTemp.Clear();
                    txtWarrantyExpiryDate.Text = "";
                    lblCustomerWarrantyExpiryDate.Text = EquipmentViewDet.Customer.CustomerFullName;
                    lblModelWarrantyExpiryDate.Text = EquipmentViewDet.EquipmentModel.Model;
                    lblEquipmentSerialNoWarrantyExpiryDate.Text = EquipmentViewDet.EquipmentSerialNo;
                    lblWarrantyExpiryDateP.Text = Convert.ToString(EquipmentViewDet.WarrantyExpiryDate);
                    MPE_WarrantyExpiryDateChangeReq.Show();
                }
                //if (lbActions.Text == "Approve/Decline Warranty Type Change Request")
                //{
                //    lblMessageApprDeclineWarrantyTypeChangeReq.Text = "";
                //    lblMessageApprDeclineWarrantyTypeChangeReq.Visible = false;
                //    MPE_ApprDeclineWarrantyTypeChangeReq.Show();
                //}
                //if (lbActions.Text == "Approve/Decline Ownership Change Request")
                //{
                //    lblMessageApprDeclineOwnershipChangeReq.Text = "";
                //    lblMessageApprDeclineOwnershipChangeReq.Visible = false;
                //    MPE_ApprDeclineOwnershipChangeReq.Show();
                //}
                //if (lbActions.Text == "Approve/Decline Warranty Expiry Date Change Request")
                //{
                //    lblMessageApprDeclineWarrantyExpiryDateChangeReq.Text = "";
                //    lblMessageApprDeclineWarrantyExpiryDateChangeReq.Visible = false;
                //    lblWarrantyExpiryDateP.Text = EquipmentViewDet.WarrantyExpiryDate == null ? "" : ((DateTime)EquipmentViewDet.WarrantyExpiryDate).ToLongDateString();
                //    MPE_ApprDeclineWarrantyExpiryDateChangeReq.Show();
                //}
                if (lbActions.Text == "Approve Warranty Type Change")
                {
                    if (new BDMS_Equipment().ApproveOrRejectEquipmentWarrrantyTypeChange(Convert.ToInt64(lblWarrantyTypeChangeID.Text), EquipmentViewDet.EquipmentHeaderID, PSession.User.UserID, true))
                    {
                        lblMessage.Text = "Equipment Warrranty Type Change approved.";
                        lblMessage.ForeColor = Color.Green;
                        fillEquipment(EquipmentViewDet.EquipmentHeaderID);
                    }
                    else
                    {
                        lblMessage.Text = "Equipment Warrranty Type Change not approved.";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                if (lbActions.Text == "Reject Warranty Type Change")
                {
                    if (new BDMS_Equipment().ApproveOrRejectEquipmentWarrantyExpiryDateChange(Convert.ToInt64(lblWarrantyTypeChangeID.Text), EquipmentViewDet.EquipmentHeaderID, PSession.User.UserID, false))
                    {
                        lblMessage.Text = "Equipment Warrranty Type Change rejected.";
                        lblMessage.ForeColor = Color.Green;
                        fillEquipment(EquipmentViewDet.EquipmentHeaderID);
                    }
                    else
                    {
                        lblMessage.Text = "Equipment Warrranty Type Change not rejected.";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                if (lbActions.Text == "Approve Ownership Change")
                {
                    if (new BDMS_Equipment().ApproveOrRejectEquipmentOwnershipChange(Convert.ToInt64(lblOwnershipChangeID.Text), EquipmentViewDet.EquipmentHeaderID, PSession.User.UserID, true))
                    {
                        lblMessage.Text = "Equipment Ownership Change approved.";
                        lblMessage.ForeColor = Color.Green;
                        fillEquipment(EquipmentViewDet.EquipmentHeaderID);
                    }
                    else
                    {
                        lblMessage.Text = "Equipment Ownership Change not approved.";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                if (lbActions.Text == "Reject Ownership Change")
                {
                    if (new BDMS_Equipment().ApproveOrRejectEquipmentOwnershipChange(Convert.ToInt64(lblOwnershipChangeID.Text), EquipmentViewDet.EquipmentHeaderID, PSession.User.UserID, false))
                    {
                        lblMessage.Text = "Equipment Ownership Change rejected.";
                        lblMessage.ForeColor = Color.Green;
                        fillEquipment(EquipmentViewDet.EquipmentHeaderID);
                    }
                    else
                    {
                        lblMessage.Text = "Equipment Ownership Change not rejected.";
                        lblMessage.ForeColor = Color.Red;
                    }
                } 
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
                lblMessage.ForeColor = Color.Red;
            }
        }
        void ActionControlMange()
        {
            lnkBtnUpdateCommDate.Visible = true;

            lnkBtnReqWarrantyTypeChange.Visible = true;
            lnkBtnReqOwnershipChange.Visible = true; 

            lnkBtnApprWarrantyTypeChangeReq.Visible = false;
            lnkBtnRejWarrantyTypeChangeReq.Visible = false;
            lnkBtnApprOwnershipChangeReq.Visible = false;
            lnkBtnRejOwnershipChangeReq.Visible = false;  

            divWarrantyTypeApproval.Visible = false;
            divOwnershipApproval.Visible = false; 

            DataTable Equip = new BDMS_Equipment().GetEquipmentChangeForApproval(null, null, EquipmentViewDet.EquipmentSerialNo);

            foreach (DataRow dr in Equip.Rows)
            {
                if (Convert.ToString(dr["ChangeRequested"]) == "Warranty Type")
                {
                    lnkBtnApprWarrantyTypeChangeReq.Visible = true;
                    lnkBtnRejWarrantyTypeChangeReq.Visible = true;
                    divWarrantyTypeApproval.Visible = true;
                    if (!string.IsNullOrEmpty(dr["NewValue"].ToString()))
                    {
                        lblOldWarrantyType.Text = EquipmentViewDet.EquipmentWarrantyType == null ? "" : EquipmentViewDet.EquipmentWarrantyType.Description;
                        lblNewWarrantyType.Text = dr["NewValue"].ToString();
                        lblOldWarrantyHMR.Text = EquipmentViewDet.WarrantyHMR == null ? "" : EquipmentViewDet.WarrantyHMR.ToString();
                        lblNewWarrantyHMR.Text = dr["WarrantyHMR"].ToString();
                        lblWarrantyTypeChangeID.Text = dr["ChangeID"].ToString();

                        List<PEquipmentAttachedFile> WarrantyTypeChangeAttachedFile = new BDMS_Equipment().GetEquipmentAttachedFileDetails(EquipmentViewDet.EquipmentHeaderID, null, Convert.ToInt64(dr["ChangeID"]));
                        gvlWarrantyTypeChangeAttachedFile.DataSource = WarrantyTypeChangeAttachedFile;
                        gvlWarrantyTypeChangeAttachedFile.DataBind();
                    }
                }
                if (Convert.ToString(dr["ChangeRequested"]) == "Ownership Change")
                {
                    lnkBtnApprOwnershipChangeReq.Visible = true;
                    lnkBtnRejOwnershipChangeReq.Visible = true;
                    divOwnershipApproval.Visible = true;
                    if (!string.IsNullOrEmpty(dr["NewValue"].ToString()))
                    {
                        lblOldCustomer.Text = EquipmentViewDet.Customer.CustomerFullName;
                        lblNewCustomer.Text = dr["NewValue"].ToString();
                        lblOwnershipChangeID.Text = dr["ChangeID"].ToString();
                    }
                    List<PEquipmentAttachedFile> OwnershipChangeAttachedFile = new BDMS_Equipment().GetEquipmentAttachedFileDetails(EquipmentViewDet.EquipmentHeaderID, null, Convert.ToInt64(dr["ChangeID"]));
                    gvOwnershipChangeAttachedFile.DataSource = OwnershipChangeAttachedFile;
                    gvOwnershipChangeAttachedFile.DataBind();
                } 
            }

            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.UpdateCommDate).Count() == 0)
            {
                lnkBtnUpdateCommDate.Visible = false;
            }

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.WarrantyTypeChange).Count() == 0)
            {
                lnkBtnReqWarrantyTypeChange.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.OwnershipChange).Count() == 0)
            {
                lnkBtnReqOwnershipChange.Visible = false;
            }
            

            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ApproveWarrantyTypeChange).Count() == 0)
            {
                lnkBtnApprWarrantyTypeChangeReq.Visible = false;
                lnkBtnRejWarrantyTypeChangeReq.Visible = false;
                divWarrantyTypeApproval.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.ApproveOwnershipChange).Count() == 0)
            {
                lnkBtnApprOwnershipChangeReq.Visible = false;
                lnkBtnRejOwnershipChangeReq.Visible = false;
                divOwnershipApproval.Visible = false;
            }
            
        }
        protected void btnUpdateCommiDate_Click(object sender, EventArgs e)
        {
            lblMessageUpdateCommissioningDate.Text = string.Empty;
            lblMessageUpdateCommissioningDate.Visible = true;
            if (string.IsNullOrEmpty(txtCommissioningDate.Text))
            {
                lblMessageUpdateCommissioningDate.ForeColor = Color.Red;
                lblMessageUpdateCommissioningDate.Text = "Please select Commissioning Date.";
                MPE_UpdateCommiDate.Show();
                return;
            }
            if (Convert.ToDateTime(txtCommissioningDate.Text) < EquipmentViewDet.DispatchedOn)
            {
                lblMessageUpdateCommissioningDate.ForeColor = Color.Red;
                lblMessageUpdateCommissioningDate.Text = "Commissioning Date cannot be earlier than the Dispatched Date.";
                MPE_UpdateCommiDate.Show();
                return;
            }
            if (new BDMS_Equipment().UpdateCommissioningDate(Convert.ToInt64(EquipmentViewDet.EquipmentHeaderID), Convert.ToDateTime(txtCommissioningDate.Text.Trim()), PSession.User.UserID))
            {
                lblCustomerC.Text = "";
                lblModelC.Text = "";
                lblEquipmentSerialNoC.Text = "";
                lblHMRC.Text = "";
                txtCommissioningDate.Text = "";

                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "Commissioning Date Equipment updated successfully.";
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Commissioning Date for Equipment not updated successfully.";
            }
            fillEquipment(EquipmentViewDet.EquipmentHeaderID);
        }
        protected void btnReqWarrantyTypeChange_Click(object sender, EventArgs e)
        {
            lblMessageWarrantyTypeChangeReq.Visible = true;
            MPE_WarrantyTypeChangeReq.Show();

            if (ddlWarranty.SelectedValue == "0")
            {
                lblMessageWarrantyTypeChangeReq.Text = "Please select the Warranty Type.";
                lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
                return;
            }

            //if (string.IsNullOrEmpty(txtWarrantyStartDate.Text))
            //{
            //    lblMessageWarrantyTypeChangeReq.Text = "Please select Warranty Start Date.";
            //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtWarrantyEndDate.Text))
            //{
            //    lblMessageWarrantyTypeChangeReq.Text = "Please select Warranty End Date.";
            //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtWarrantyHMRValue.Text))
            //{
            //    lblMessageWarrantyTypeChangeReq.Text = "Please enter the Warranty HMR.";
            //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
            //    return;
            //}
            //if (Convert.ToInt32(txtWarrantyHMRValue.Text) < Convert.ToInt32(EquipmentViewDet.WarrantyHMR))
            //{
            //    lblMessageWarrantyTypeChangeReq.Text = "Warranty HMR cannot be lesser than previous HMR.";
            //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
            //    return;
            //}
            //if (Convert.ToDateTime(txtWarrantyStartDate.Text) > Convert.ToDateTime(txtWarrantyEndDate.Text))
            //{
            //    lblMessageWarrantyTypeChangeReq.Text = "Warranty End Date earlier than the End Date.";
            //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
            //    return;
            //}

            if (fileUpload.FileName.Length != 0)
            {
                Boolean old = true;
                foreach (PEquipmentAttachedFilee_Insert f in AttachedFileTemp)
                {
                    if (f.FileName == fileUpload.FileName)
                    {
                        old = false;
                    }
                }
                if (old)
                {
                    //string Message = Validation(fileUpload.PostedFile);
                    //if (!string.IsNullOrEmpty(Message))
                    //{
                    //    lblMessageWarrantyTypeChangeReq.Text = Message;
                    //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
                    //    return;
                    //}
                    AttachedFileTemp.Add(CreateUploadedFileEquipment(fileUpload.PostedFile));
                }
            }

            //if (AttachedFileTemp.Count == 0)
            //{
            //    lblMessageWarrantyTypeChangeReq.Text = "Please upload the File.";
            //    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
            //    return;
            //}

            PEquipmentWarranty_Insert WT = new PEquipmentWarranty_Insert();
            WT.EquipmentHeaderID = EquipmentViewDet.EquipmentHeaderID;
            WT.EquipmentWarrantyTypeID = Convert.ToInt32(ddlWarranty.SelectedValue);
            //WT.WarrantyStartDate = Convert.ToDateTime(txtWarrantyStartDate.Text);
            //WT.WarrantyEndDate = Convert.ToDateTime(txtWarrantyEndDate.Text);
            //WT.WarrantyHMR = Convert.ToInt32(txtWarrantyHMRValue.Text);
            WT.AttachedFile = new List<PEquipmentAttachedFilee_Insert>();
            WT.AttachedFile = AttachedFileTemp;

            PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Equipment/InsertEquipmentWarrantyTypeChangeRequest", WT));
            if (result.Status == PApplication.Failure)
            {
                lblMessageWarrantyTypeChangeReq.Text = result.Message;
                lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
                return;
            }

            MPE_WarrantyTypeChangeReq.Hide();
            lblMessage.Text = "Equipment Warranty Type Change is requested.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;

            //if (new BDMS_Equipment().InsertEquipmentWarrantyTypeChangeRequest(EquipmentViewDet.EquipmentHeaderID, Convert.ToInt32(ddlWarranty.SelectedValue), PSession.User.UserID, 1, AttachedFileTemp))
            //{
            //    lblMessage.Text = "Equipment Warranty Type Change is requested.";
            //    lblMessage.ForeColor = Color.Green;
            //    lblMessage.Visible = true;
            //}
            //else
            //{
            //    lblMessage.Text = "Equipment Warranty Type Change not requested.";
            //    lblMessage.ForeColor = Color.Red;
            //}
            fillEquipment(EquipmentViewDet.EquipmentHeaderID);
        }
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            MPE_WarrantyTypeChangeReq.Show();
            lblMessageWarrantyTypeChangeReq.Visible = true;
            foreach (PEquipmentAttachedFilee_Insert f in AttachedFileTemp)
            {
                if (f.FileName == fileUpload.FileName)
                {
                    lblMessageWarrantyTypeChangeReq.Text = "This file already available";
                    lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;
                    return;
                }
            }
            string Message = Validation(fileUpload.PostedFile);
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageWarrantyTypeChangeReq.Text = Message;
                lblMessageWarrantyTypeChangeReq.ForeColor = Color.Red;

                return;
            }
            AttachedFileTemp.Add(CreateUploadedFileEquipment(fileUpload.PostedFile));
            gvWarrantyTypeSupportDocument.DataSource = AttachedFileTemp;
            gvWarrantyTypeSupportDocument.DataBind();
        }
        public string Validation(HttpPostedFile file)
        {
            if (file.FileName.Length == 0)
            {
                return "Please select the File.";
            }

            string ext = System.IO.Path.GetExtension(file.FileName).ToLower(); //fileUpload.PostedFile.FileName
            List<string> Extension = new List<string>();
            Extension.Add(".jpg");
            Extension.Add(".png");
            Extension.Add(".gif");
            Extension.Add(".jpeg");
            if (!Extension.Contains(ext))
            {
                return "Please choose only .jpg, .png and .gif image types!";
            }
            return "";
        }
        private PEquipmentAttachedFilee_Insert CreateUploadedFileEquipment(HttpPostedFile file)
        {
            int size = file.ContentLength;
            string name = file.FileName;
            int position = name.LastIndexOf("\\");
            name = name.Substring(position + 1);
            byte[] fileData = new byte[size];
            file.InputStream.Read(fileData, 0, size);
            return new PEquipmentAttachedFilee_Insert()
            {
                FileName = name,
                //   ReferenceName = file.ContentType,
                AttachedFile = fileData,
                //   AttachedFileID = 0,
                //  CreatedBy = new PUser() { UserID = PSession.User.UserID },
                //  Equipment = new PDMS_EquipmentHeader() { EquipmentHeaderID = EquipmentViewDet.EquipmentHeaderID }
            };
        }
        protected void lnkBtnEquipmentAttachedFileDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkDownload.NamingContainer;

                Label AttachedFileID = (Label)gvRow.FindControl("lblAttachedFileID");
                //Label lblFileName = (Label)gvRow.FindControl("lblFileName");
                

                PEquipmentAttachedFile UploadedFile = new BDMS_Equipment().GetEquipmentAttachedFileByID(AttachedFileID.Text + Path.GetExtension(lnkDownload.Text));
                
                Response.AddHeader("Content-type", UploadedFile.ReferenceName);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkDownload.Text);
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
        protected void lnkBtnvWarrantyTypeAttachedFileRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            AttachedFileTemp.RemoveAt(gvRow.RowIndex);

            gvWarrantyTypeSupportDocument.DataSource = AttachedFileTemp;
            gvWarrantyTypeSupportDocument.DataBind();
            MPE_WarrantyTypeChangeReq.Show();
        }
        
        protected void btnAddFileOwnershipChange_Click(object sender, EventArgs e)
        {
            MPE_OwnershipChangeReq.Show();
            lblMessageOwnershipChangeReq.Visible = true;
            foreach (PEquipmentAttachedFilee_Insert f in AttachedFileTemp)
            {
                if (f.FileName == fileUploadOwnershipChange.FileName)
                {
                    lblMessageOwnershipChangeReq.Text = "This file already available";
                    lblMessageOwnershipChangeReq.ForeColor = Color.Red;
                    return;
                }
            }
            string Message = Validation(fileUploadOwnershipChange.PostedFile);
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageOwnershipChangeReq.Text = Message;
                lblMessageOwnershipChangeReq.ForeColor = Color.Red;

                return;
            }
            AttachedFileTemp.Add(CreateUploadedFileEquipment(fileUploadOwnershipChange.PostedFile));
            gvOwnershipChangeReqSupportDocument.DataSource = AttachedFileTemp;
            gvOwnershipChangeReqSupportDocument.DataBind();
        }
        protected void lnkBtnAttachedFileOwnershipChangeRemove_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            AttachedFileTemp.RemoveAt(gvRow.RowIndex);

            gvOwnershipChangeReqSupportDocument.DataSource = AttachedFileTemp;
            gvOwnershipChangeReqSupportDocument.DataBind();
            MPE_OwnershipChangeReq.Show();
        }
        protected void btnReqOwnershipChange_Click(object sender, EventArgs e)
        {
            lblMessageOwnershipChangeReq.Visible = true;
            MPE_OwnershipChangeReq.Show();

            if (string.IsNullOrEmpty(txtBoxCustomerOwnershipChange.Text))
            {
                lblMessageOwnershipChangeReq.Text = "Please enter the Customer Code";
                lblMessageOwnershipChangeReq.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(txtSoldDate.Text))
            {
                lblMessageOwnershipChangeReq.Text = "Please select the Sold Date";
                lblMessageOwnershipChangeReq.ForeColor = Color.Red;
                return;
            }

            //long? CustomerID = null;
            List<PDMS_Customer> Customer = new BDMS_Customer().GetCustomerByCode(null, txtBoxCustomerOwnershipChange.Text.Trim());
            if (Customer.Count == 0)
            {
                lblMessageOwnershipChangeReq.Text = "Customer Code is not avialable.";
                //txtBoxCustomerOwnershipChange.BorderColor = Color.Red;
                return;
            }
            //CustomerID = Customer[0].CustomerID;

            if (fileUploadOwnershipChange.FileName.Length != 0)
            {
                Boolean old = true;
                foreach (PEquipmentAttachedFilee_Insert f in AttachedFileTemp)
                {
                    if (f.FileName == fileUploadOwnershipChange.FileName)
                    {
                        old = false;
                    }
                }
                if (old)
                {
                    //string Message = Validation(fileUploadOwnershipChange.PostedFile);
                    //if (!string.IsNullOrEmpty(Message))
                    //{
                    //    lblMessageOwnershipChangeReq.Text = Message;
                    //    lblMessageOwnershipChangeReq.ForeColor = Color.Red;
                    //    return;
                    //}
                    AttachedFileTemp.Add(CreateUploadedFileEquipment(fileUploadOwnershipChange.PostedFile));
                }
            }

            //if (AttachedFileTemp.Count == 0)
            //{act
            //    lblMessageOwnershipChangeReq.Text = "Please upload the File.";
            //    lblMessageOwnershipChangeReq.ForeColor = Color.Red;
            //    return;
            //}

            PEquipmentWarranty_Insert WT = new PEquipmentWarranty_Insert();
            WT.EquipmentHeaderID = EquipmentViewDet.EquipmentHeaderID;
            WT.CustomerID = Customer[0].CustomerID;
            WT.SoldDate = Convert.ToDateTime(txtSoldDate.Text.Trim());
            WT.AttachedFile = new List<PEquipmentAttachedFilee_Insert>();
            WT.AttachedFile = AttachedFileTemp;

            PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Equipment/InsertEquipmentOwnershipChangeRequest", WT));
            if (result.Status == PApplication.Failure)
            {
                lblMessageOwnershipChangeReq.Text = result.Message;
                lblMessageOwnershipChangeReq.ForeColor = Color.Red;
                return;
            }
            lblMessage.Text = "Ownership Change is requested successfully ";
            lblMessage.ForeColor = Color.Green;
            MPE_OwnershipChangeReq.Hide();
            fillEquipment(EquipmentViewDet.EquipmentHeaderID);
        }
        protected void btnAddFileWarrantyExpiryDateChange_Click(object sender, EventArgs e)
        {
            MPE_WarrantyExpiryDateChangeReq.Show();
            lblMessageWarrantyExpiryDateChangeReq.Visible = true;
            foreach (PEquipmentAttachedFilee_Insert f in AttachedFileTemp)
            {
                if (f.FileName == fileUploadOwnershipChange.FileName)
                {
                    lblMessageWarrantyExpiryDateChangeReq.Text = "This file already available";
                    lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
                    return;
                }
            }
            string Message = Validation(fileUploadWarrantyExpiryDateChange.PostedFile);
            if (!string.IsNullOrEmpty(Message))
            {
                lblMessageWarrantyExpiryDateChangeReq.Text = Message;
                lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
                return;
            }
            AttachedFileTemp.Add(CreateUploadedFileEquipment(fileUploadWarrantyExpiryDateChange.PostedFile));
            gvWarrantyExpiryDateChangeSupportDocument.DataSource = AttachedFileTemp;
            gvWarrantyExpiryDateChangeSupportDocument.DataBind();
        }
        protected void lnkBtnWarrantyExpiryDateChangeRemove_Click(object sender, EventArgs e)
        {
            lblMessageWarrantyExpiryDateChangeReq.Visible = true;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            AttachedFileTemp.RemoveAt(gvRow.RowIndex);

            gvWarrantyExpiryDateChangeSupportDocument.DataSource = AttachedFileTemp;
            gvWarrantyExpiryDateChangeSupportDocument.DataBind();
            MPE_WarrantyExpiryDateChangeReq.Show();
        }
        protected void btnReqWarrantyExpiryDate_Click(object sender, EventArgs e)
        {
            lblMessageWarrantyExpiryDateChangeReq.Visible = true;
            MPE_WarrantyExpiryDateChangeReq.Show();
            if (string.IsNullOrEmpty(txtWarrantyExpiryDate.Text))
            {
                lblMessageWarrantyExpiryDateChangeReq.Text = "Please select the Warranty Expiry Date.";
                lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
                return;
            }
            if (EquipmentViewDet.WarrantyExpiryDate != null)
            {
                if (Convert.ToDateTime(txtWarrantyExpiryDate.Text) < Convert.ToDateTime(EquipmentViewDet.WarrantyExpiryDate))
                {
                    lblMessageWarrantyExpiryDateChangeReq.Text = "Warranty Expiry Date earlier than the current Warranty Expiry Date.";
                    lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
                    return;
                }
            }

            if (fileUploadWarrantyExpiryDateChange.FileName.Length != 0)
            {
                Boolean old = true;
                foreach (PEquipmentAttachedFilee_Insert f in AttachedFileTemp)
                {
                    if (f.FileName == fileUploadWarrantyExpiryDateChange.FileName)
                    {
                        old = false;
                    }
                }
                if (old)
                {
                    //string Message = Validation(fileUploadWarrantyExpiryDateChange.PostedFile);
                    //if (!string.IsNullOrEmpty(Message))
                    //{
                    //    lblMessageWarrantyExpiryDateChangeReq.Text = Message;
                    //    lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
                    //    return;
                    //}
                    AttachedFileTemp.Add(CreateUploadedFileEquipment(fileUploadWarrantyExpiryDateChange.PostedFile));
                }
            }

            //if (AttachedFileTemp.Count == 0)
            //{
            //    lblMessageWarrantyExpiryDateChangeReq.Text = "Please upload the File.";
            //    lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
            //    return;
            //}

            PEquipmentWarranty_Insert WT = new PEquipmentWarranty_Insert();
            WT.EquipmentHeaderID = EquipmentViewDet.EquipmentHeaderID;
            WT.OldExpiryDate = Convert.ToDateTime(EquipmentViewDet.WarrantyExpiryDate);
            WT.NewExpiryDate = Convert.ToDateTime(txtWarrantyExpiryDate.Text.Trim());
            WT.AttachedFile = new List<PEquipmentAttachedFilee_Insert>();
            WT.AttachedFile = AttachedFileTemp;
            
            PApiResult result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("Equipment/InsertEquipmentWarrantyExpiryDateChangeRequest", WT));
            if (result.Status == PApplication.Failure)
            {
                lblMessageWarrantyExpiryDateChangeReq.Text = result.Message;
                lblMessageWarrantyExpiryDateChangeReq.ForeColor = Color.Red;
                return;
            }
            MPE_WarrantyExpiryDateChangeReq.Hide();
            lblMessage.Text = "Warranty Expiry Date Change is requested successfully ";
            lblMessage.ForeColor = Color.Green;
            fillEquipment(EquipmentViewDet.EquipmentHeaderID);
        }
        void fillSupportDocument()
        {
            try
            {
                List<PEquipmentAttachedFile> UploadedFile = new BDMS_Equipment().GetEquipmentAttachedFileDetails(EquipmentViewDet.EquipmentHeaderID, null, null);
                gvAttachedFile.DataSource = UploadedFile;
                gvAttachedFile.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        void fillChangeRequestHistory()
        {
            EquipmentChgReqHst = new BDMS_Equipment().GetEquipmentChangeRequestHistory(EquipmentViewDet.EquipmentHeaderID);

            gvChgReqHst.DataSource = EquipmentChgReqHst;
            gvChgReqHst.DataBind();

            if (gvChgReqHst.Rows.Count == 0)
            {
                gvChgReqHst.DataSource = null;
                gvChgReqHst.DataBind();
                //lblRowCountChgReqHst.Visible = false;
                //ibtnChgReqHstArrowLeft.Visible = false;
                //ibtnChgReqHstArrowRight.Visible = false;
                return;
            }
            EquipmentChangeReqHstBind();
        }
        void EquipmentChangeReqHstBind()
        {
            gvChgReqHst.DataSource = EquipmentChgReqHst;
            gvChgReqHst.DataBind();
            lblRowCountChgReqHst.Text = (((gvChgReqHst.PageIndex) * gvChgReqHst.PageSize) + 1) + " - " + (((gvChgReqHst.PageIndex) * gvChgReqHst.PageSize) + gvChgReqHst.Rows.Count) + " of " + EquipmentChgReqHst.Rows.Count;
        }
        protected void ibtnChgReqHstArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvChgReqHst.PageIndex > 0)
            {
                gvChgReqHst.PageIndex = gvChgReqHst.PageIndex - 1;
                EquipmentChangeReqHstBind();
            }
        }
        protected void ibtnChgReqHstArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvChgReqHst.PageCount > gvChgReqHst.PageIndex)
            {
                gvChgReqHst.PageIndex = gvChgReqHst.PageIndex + 1;
                EquipmentChangeReqHstBind();
            }
        }
        protected void gvChgReqHst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvChgReqHst.PageIndex = e.NewPageIndex;
            fillChangeRequestHistory();
        }
        protected void lnkBtnWarrantyTypeChangeAttachedFileDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnWarrantyTypeChangeAttachedFileDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkBtnWarrantyTypeChangeAttachedFileDownload.NamingContainer;

                Label lblWarrantyTypeChangeAttachedFileID = (Label)gvRow.FindControl("lblWarrantyTypeChangeAttachedFileID");

                PEquipmentAttachedFile UploadedFile = new BDMS_Equipment().GetEquipmentAttachedFileByID(lblWarrantyTypeChangeAttachedFileID.Text + Path.GetExtension(lnkBtnWarrantyTypeChangeAttachedFileDownload.Text));

                Response.AddHeader("Content-type", UploadedFile.ReferenceName);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkBtnWarrantyTypeChangeAttachedFileDownload.Text);
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
        protected void lnkBtOwnershipChangeAttachedFileDownload_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtOwnershipChangeAttachedFileDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkBtOwnershipChangeAttachedFileDownload.NamingContainer;

                Label lblOwnershipChangeAttachedFileID = (Label)gvRow.FindControl("lblOwnershipChangeAttachedFileID");

                PEquipmentAttachedFile UploadedFile = new BDMS_Equipment().GetEquipmentAttachedFileByID(lblOwnershipChangeAttachedFileID.Text + Path.GetExtension(lnkBtOwnershipChangeAttachedFileDownload.Text));

                Response.AddHeader("Content-type", UploadedFile.ReferenceName);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkBtOwnershipChangeAttachedFileDownload.Text);
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
        protected void lnkBtnWarrantyExpiryDateChangeAttachedFileDownload(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnWarrantyExpiryDateChangeAttachedFileDownload = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkBtnWarrantyExpiryDateChangeAttachedFileDownload.NamingContainer;

                Label lblWarrantyExpiryDateChangeAttachedFileID = (Label)gvRow.FindControl("lblWarrantyExpiryDateChangeAttachedFileID");

                PEquipmentAttachedFile UploadedFile = new BDMS_Equipment().GetEquipmentAttachedFileByID(lblWarrantyExpiryDateChangeAttachedFileID.Text + Path.GetExtension(lnkBtnWarrantyExpiryDateChangeAttachedFileDownload.Text));

                Response.AddHeader("Content-type", UploadedFile.ReferenceName);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + lnkBtnWarrantyExpiryDateChangeAttachedFileDownload.Text);
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
    }
}