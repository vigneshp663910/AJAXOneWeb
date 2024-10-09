using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Data;

namespace DealerManagementSystem.ViewEquipment
{
    public partial class EquipmentHistory : BasePage
    {
       // public override SubModule SubModuleName { get { return SubModule.ViewEquipment_EquipmentHistory; } }
        public List<PDMS_ICTicket> ICTickets0
        {
            get
            {
                if (Session["EquipmentHistory0"] == null)
                {
                    Session["EquipmentHistory0"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["EquipmentHistory0"];
            }
            set
            {
                Session["EquipmentHistory0"] = value;
            }
        }
        public List<PDMS_ICTicket> ICTickets1
        {
            get
            {
                if (Session["EquipmentHistory1"] == null)
                {
                    Session["EquipmentHistory1"] = new List<PDMS_ICTicket>();
                }
                return (List<PDMS_ICTicket>)Session["EquipmentHistory1"];
            }
            set
            {
                Session["EquipmentHistory1"] = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEquipment.Text.Trim()))
            {
                lblCustomerCode.Text = "";
                lblCustomerName.Text = "";
                lblDateOfCommissioning.Text = "";
                lblModel.Text = "";
                lblDivision.Text = "";

                lblEquipmentModelNumber.Text = "";
                txtEquipmentModelNumber.Text = "";
                lblEngineModel.Text = "";
                txtEngineModel.Text = "";
                lblEngineSerialNumber.Text = "";
                txtEngineSerialNumber.Text = "";

                gvICTickets1.DataSource = null;
                gvICTickets1.DataBind();
                gvICTickets0.DataSource = null;
                gvICTickets0.DataBind();
                gvEquipment.DataSource = null;
                gvEquipment.DataBind();
                return;
            }

            DataSet ds = new BDMS_Equipment().GetEquipmentHistory(null, txtEquipment.Text.Trim());

            if (ds.Tables.Count == 0)
            {
                lblCustomerCode.Text = "";
                lblCustomerName.Text = "";
                lblDateOfCommissioning.Text = "";
                lblModel.Text = "";
                lblDivision.Text = "";

                lblEquipmentModelNumber.Text = "";
                txtEquipmentModelNumber.Text = "";
                lblEngineModel.Text = "";
                txtEngineModel.Text = "";
                lblEngineSerialNumber.Text = "";
                txtEngineSerialNumber.Text = "";

                gvICTickets1.DataSource = null;
                gvICTickets1.DataBind();
                gvICTickets0.DataSource = null;
                gvICTickets0.DataBind();
                gvEquipment.DataSource = null;
                gvEquipment.DataBind();
                return;
            }
            ICTickets0 = GetEquipmentDT0toClass(ds.Tables[0]);
            ICTickets1 = GetEquipmentDT1toClass(ds.Tables[1]);



            gvICTickets1.DataSource = ICTickets1;
            gvICTickets1.DataBind();
            gvICTickets0.DataSource = ICTickets0;
            gvICTickets0.DataBind();

            lblCustomerCode.Text = Convert.ToString(ds.Tables[2].Rows[0]["CustomerCode"]);
            lblCustomerName.Text = Convert.ToString(ds.Tables[2].Rows[0]["CustomerName"]); ;
            lblDateOfCommissioning.Text = Convert.ToString(ds.Tables[2].Rows[0]["CommissioningOn"]);  //ICTickets1[0].Equipment.CommissioningOn == null ? "" : ((DateTime)ICTickets1[0].Equipment.CommissioningOn).ToShortDateString();
            lblModel.Text = Convert.ToString(ds.Tables[2].Rows[0]["Model"]); //ICTickets1[0].Equipment.EquipmentModel.Model;
            lblDivision.Text = Convert.ToString(ds.Tables[2].Rows[0]["Division"]); // ICTickets1[0].Equipment.EquipmentModel.Division.DivisionCode;

            lblEquipmentModelNumber.Text = Convert.ToString(ds.Tables[2].Rows[0]["EquipmentModelNumber"]);
            txtEquipmentModelNumber.Text = Convert.ToString(ds.Tables[2].Rows[0]["EquipmentModelNumber"]);
            lblEngineModel.Text = Convert.ToString(ds.Tables[2].Rows[0]["EngineModel"]);
            txtEngineModel.Text = Convert.ToString(ds.Tables[2].Rows[0]["EngineModel"]);
            lblEngineSerialNumber.Text = Convert.ToString(ds.Tables[2].Rows[0]["EngineSerialNo"]);
            txtEngineSerialNumber.Text = Convert.ToString(ds.Tables[2].Rows[0]["EngineSerialNo"]);

            lblEquipmentHeaderID.Text = Convert.ToString(ds.Tables[2].Rows[0]["EquipmentHeaderID"]);


            gvEquipment.DataSource = ds.Tables[2];
            gvEquipment.DataBind();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Equipment Serial No");
            dt.Columns.Add("Customer Code");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Date Of Oommissioning");
            dt.Columns.Add("100 Hrs Service");
            dt.Columns.Add("500 Hrs Service");
            dt.Columns.Add("1000 Hrs Service");
            dt.Columns.Add("1500 Hrs Service");
            dt.Columns.Add("Issues");
            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("Service Request Date");
            dt.Columns.Add("Service Requested Date");
            dt.Columns.Add("Service Type");
            dt.Columns.Add("Warranty Material Replaced");
            dt.Columns.Add("Material Description");
            dt.Columns.Add("HMR");
            dt.Columns.Add("Tsir Number");
            dt.Columns.Add("Dealer");
            dt.Columns.Add("Dealer Name");

            foreach (PDMS_ICTicket IC in ICTickets1)
            {
                dt.Rows.Add(
                    IC.Equipment.EquipmentSerialNo
                    , IC.Customer.CustomerCode
                    , IC.Customer.CustomerName
                    , IC.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.Equipment.CommissioningOn).ToShortDateString()
                    , IC.Equipment.Service100Hrs == null ? "" : ((DateTime)IC.Equipment.Service100Hrs).ToShortDateString()
                    , IC.Equipment.Service500Hrs == null ? "" : ((DateTime)IC.Equipment.Service500Hrs).ToShortDateString()
                    , IC.Equipment.Service1000Hrs == null ? "" : ((DateTime)IC.Equipment.Service1000Hrs).ToShortDateString()
                    , IC.Equipment.Service1500Hrs == null ? "" : ((DateTime)IC.Equipment.Service1500Hrs).ToShortDateString()
                    , IC.ComplaintDescription
                    , IC.ICTicketNumber
                    , IC.RequestedDate == null ? "" : ((DateTime)IC.RequestedDate).ToShortDateString()
                    , IC.RestoreDate == null ? "" : ((DateTime)IC.RestoreDate).ToShortDateString()
                    , IC.ServiceType == null ? "" : IC.ServiceType.ServiceType
                    , IC.ServiceMaterialM.Material.MaterialCode
                    , IC.ServiceMaterialM.Material.MaterialDescription
                    , IC.CurrentHMRValue
                    , IC.ServiceMaterialM.TSIR.TsirNumber
                    , IC.Dealer.DealerCode
                    , IC.Dealer.DealerName
                    );
            }
            new BXcel().ExporttoExcel(dt, "IC Ticket Details");
        }


        public List<PDMS_ICTicket> GetEquipmentDT0toClass(DataTable dt)
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            lblEquipmentModelNumber.Visible = false;
            txtEquipmentModelNumber.Visible = true;
            lblEngineModel.Visible = false;
            txtEngineModel.Visible = true;
            lblEngineSerialNumber.Visible = false;
            txtEngineSerialNumber.Visible = true;
            btnEdit.Visible = false;
            //btnUpdate.Visible = true;

        }

        
    }
}