using Business;
using Microsoft.Reporting.WebForms;
using Properties;
using SapIntegration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.UserControls
{
    public partial class ICTicketRestore : System.Web.UI.UserControl
    {
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
        public PDMS_ICTicketFSR ICTicketFSR
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCustomerSatisfactionLevel();
                FillCallInformation();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // btnSave.Focus();
            if (!ValidatetionRestore())
            {
                return;
            }
            int CustomerSatisfactionLevel = Convert.ToInt32(ddlCustomerSatisfactionLevel.SelectedValue);

            DateTime? RestoreDate = string.IsNullOrEmpty(txtRestoreDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtRestoreDate.Text.Trim() + " " + ddlRestoreHH.SelectedValue + ":" + ddlRestoreMM.SelectedValue);
            DateTime? ArrivalBack = string.IsNullOrEmpty(txtArrivalBackDate.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtArrivalBackDate.Text.Trim() + " " + ddlArrivalBackHH.SelectedValue + ":" + ddlArrivalBackMM.SelectedValue);

            if (SDMS_ICTicket.ReachedDate > RestoreDate)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
                lblMessage.Text = "Restore date should not be less than Reached date.";
                return;
            }
            if (new BDMS_ICTicket().InsertOrUpdateICTicketRestore(SDMS_ICTicket.ICTicketID, RestoreDate, ArrivalBack, CustomerSatisfactionLevel, txtCustomerRemarks.Text.Trim(), ddlComplaintStatus.SelectedValue, PSession.User.UserID))
            {
                lblMessage.Text = "ICTicket is updated successfully";
                lblMessage.ForeColor = Color.Green;

                //  long ICTicketID = Convert.ToInt64(Request.QueryString["TicketID"]);
                //     SDMS_ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketID);

                if (SDMS_ICTicket.ServiceType != null)
                    HttpContext.Current.Session["ServiceTypeID"] = SDMS_ICTicket.ServiceType.ServiceTypeID;

                if ((short)DMS_ServiceType.Commission == SDMS_ICTicket.ServiceType.ServiceTypeID)
                {
                    if (ddlComplaintStatus.SelectedValue == "Close")
                    {
                        if (SDMS_ICTicket.Equipment.EngineSerialNo.Length >= 2)
                        {
                            if (SDMS_ICTicket.RestoreDate == null)
                            {
                                DataTable DT = new BDMS_ICTicket().GetCommissionMailTo(SDMS_ICTicket.Address.Region.RegionID, SDMS_ICTicket.Equipment.EngineSerialNo.Substring(0, 2));
                                if (DT.Rows.Count != 0)
                                {
                                    string Message = Body();
                                    List<string> MailToS = new List<string>();
                                    string[] MailIDs = Convert.ToString(DT.Rows[0]["MailID"]).Split(',');
                                    foreach (string MailID in MailIDs)
                                    {
                                        MailToS.Add(MailID);
                                    }
                                    Byte[] MyByte = SendPDF_PRF();
                                    Boolean Success = new EmailManager().MailSendPRF(MailToS, "", Message, MyByte, SDMS_ICTicket.Equipment.EquipmentSerialNo + "_PRF.PDF");
                                    new BDMS_ICTicket().InsertCommissionMailTo(SDMS_ICTicket.ICTicketID, Convert.ToString(DT.Rows[0]["MailID"]), PSession.User.UserID, Success, Message);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "ICTicket is not updated successfully";
            }
        }
        private void FillCallInformation()
        {
            txtRestoreDate.Text = SDMS_ICTicket.RestoreDate == null ? "" : ((DateTime)SDMS_ICTicket.RestoreDate).ToShortDateString();
            ddlRestoreHH.SelectedValue = SDMS_ICTicket.RestoreDate == null ? "-1" : ((DateTime)SDMS_ICTicket.RestoreDate).Hour.ToString();
            if (SDMS_ICTicket.RestoreDate != null)
            {
                int RestoreMMMinute = ((DateTime)SDMS_ICTicket.RestoreDate).Minute;
                int adjustment = RestoreMMMinute % 5;
                if (adjustment != 0)
                {
                    RestoreMMMinute = (RestoreMMMinute - adjustment) + 5;
                }
                ddlRestoreMM.SelectedValue = RestoreMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlRestoreMM.SelectedValue = "0";
            }
            txtArrivalBackDate.Text = SDMS_ICTicket.ArrivalBack == null ? "" : ((DateTime)SDMS_ICTicket.ArrivalBack).ToShortDateString();
            ddlArrivalBackHH.SelectedValue = SDMS_ICTicket.ArrivalBack == null ? "-1" : ((DateTime)SDMS_ICTicket.ArrivalBack).Hour.ToString();
            if (SDMS_ICTicket.ArrivalBack != null)
            {
                int ArrivalBackMMMinute = ((DateTime)SDMS_ICTicket.ArrivalBack).Minute;
                int adjustment = ArrivalBackMMMinute % 5;
                if (adjustment != 0)
                {
                    ArrivalBackMMMinute = (ArrivalBackMMMinute - adjustment) + 5;
                }
                ddlArrivalBackMM.SelectedValue = ArrivalBackMMMinute.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlArrivalBackMM.SelectedValue = "0";
            }

            if (SDMS_ICTicket.CustomerSatisfactionLevel != null)
                ddlCustomerSatisfactionLevel.SelectedValue = SDMS_ICTicket.CustomerSatisfactionLevel.CustomerSatisfactionLevelID.ToString();

            txtCustomerRemarks.Text = ICTicketFSR.CustomerRemarks;

            if (SDMS_ICTicket.CustomerSatisfactionLevel != null)
                ddlComplaintStatus.SelectedValue = ICTicketFSR.ComplaintStatus;
        }
        private void FillCustomerSatisfactionLevel()
        {
            ddlCustomerSatisfactionLevel.DataTextField = "CustomerSatisfactionLevel";
            ddlCustomerSatisfactionLevel.DataValueField = "CustomerSatisfactionLevelID";
            ddlCustomerSatisfactionLevel.DataSource = new BDMS_Service().GetCustomerSatisfactionLevel(null, null);
            ddlCustomerSatisfactionLevel.DataBind();
            ddlCustomerSatisfactionLevel.Items.Insert(0, new ListItem("Select", "0"));
        }
        Boolean ValidatetionRestore()
        {
            ddlCustomerSatisfactionLevel.BorderColor = Color.Silver;
            lblMessage.Visible = true;
            string Message = "";
            Boolean Ret = true;

            if ((!string.IsNullOrEmpty(txtRestoreDate.Text.Trim())) || (ddlComplaintStatus.SelectedValue == "Close"))
            {

                //if (SDMS_ICTicket.Category1 == null)
                //{
                //    Message = Message + "<br/>Please select the Category1";
                //    Ret = false;
                //}
                //else if (SDMS_ICTicket.Category2 == null)
                //{
                //    Message = Message + "<br/>Please select the Category2";
                //    Ret = false;
                //}
                //else if (SDMS_ICTicket.Category3 == null)
                //{
                //    Message = Message + "<br/>Please select the Category3";
                //    Ret = false;
                //}
                //else if (SDMS_ICTicket.Category4 == null)
                //{
                //    Message = Message + "<br/>Please select the Category4";
                //    Ret = false;
                //}

                if (SDMS_ICTicket.MainApplication == null)
                {
                    Message = Message + "<br/>Please select the Main Application";
                    Ret = false;
                }
                else if (SDMS_ICTicket.SubApplication == null)
                {
                    Message = Message + "<br/>Please select the Sub Application";
                    Ret = false;
                }
                if (ddlCustomerSatisfactionLevel.SelectedValue == "0")
                {
                    Message = Message + "<br/>Please select the Customer Satisfaction Level";
                    Ret = false;
                    ddlCustomerSatisfactionLevel.BorderColor = Color.Red;
                }
                if (string.IsNullOrEmpty(txtRestoreDate.Text.Trim()))
                {
                    Message = Message + "<br/>Please Enter the Restore Date";
                    Ret = false;
                }
                if (ddlRestoreHH.SelectedValue == "-1")
                {
                    Message = Message + "<br/>Please select the Restored Hour";
                    Ret = false;
                    ddlRestoreHH.BorderColor = Color.Red;
                }

                if (ddlRestoreMM.SelectedValue == "-1")
                {
                    Message = Message + "<br/>Please select the Restored Minute";
                    Ret = false;
                    ddlRestoreMM.BorderColor = Color.Red;
                }

                if (string.IsNullOrEmpty(txtArrivalBackDate.Text.Trim()))
                {
                    lblMessage.Text = " Please Enter the Arrival Back Date";
                    return false;
                }
                if (ddlArrivalBackHH.SelectedValue == "-1")
                {
                    lblMessage.Text = " Please Enter the Arrival Back Hour";
                    return false;
                }
                if (ddlArrivalBackMM.SelectedValue == "0")
                {
                    lblMessage.Text = " Please Enter the Arrival Back Minute";
                    return false;
                }

                DateTime ArrivalBackDate = Convert.ToDateTime(txtArrivalBackDate.Text.Trim() + " " + ddlArrivalBackHH.SelectedValue + ":" + ddlArrivalBackMM.SelectedValue);
                if (SDMS_ICTicket.ReachedDate > ArrivalBackDate)
                {
                    lblMessage.Text = "Arrival Back Date should be grater then Reached Date";
                    return false;
                }
            }
            lblMessage.Text = Message;

            return Ret;
        }
        public void EnableOrDesableBasedOnServiceCharges()
        {
            if (SS_ServiceCharge.Count != 0)
            {
                txtRestoreDate.Enabled = true;
                ddlRestoreHH.Enabled = true;
                ddlRestoreMM.Enabled = true;
            }
            else
            {
                txtRestoreDate.Enabled = false;
                ddlRestoreHH.Enabled = false;
                ddlRestoreMM.Enabled = false;
            }
        }

        protected void ddlComplaintStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlComplaintStatus.SelectedValue == "Close")
            {
                txtRestoreDate.Enabled = true;
                ddlRestoreHH.Enabled = true;
                ddlRestoreMM.Enabled = true;
            }
            else
            {
                txtRestoreDate.Enabled = false;
                ddlRestoreHH.Enabled = false;
                ddlRestoreMM.Enabled = false;
                txtRestoreDate.Text = "";
            }
        }

        private string Body()
        {
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);
            string Message = "";
            string Top = "<!DOCTYPE html><html><head><title></title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head>"
                         + "<body><div style=\"max-width: 1500px; margin:auto\"><form><div><p><span>Good Morning!</span></p><p>@@Description<br /></p>";
            Top = Top.Replace("@@Description", "");

            string Header = "<table style=\"border: 1px solid lightblue;  border-collapse: collapse; width: 700px;\" ><thead><tr >"
              + "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">Details</th> "
              + "<th style=\"background-color: lightblue;color: white; padding: 10px; text-align:center\">PRF</th></tr></thead><tbody>";

            //string RowTD = "<tr><td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowDetails</td> "
            //    + "<td style=\"background-color: #eee;color: black;padding: 3px; text-align:center;border: 1px solid lightblue\">@@RowPRF</td></tr>";


            string RowTD = "<tr><td style=\"background-color: #eee;color: black;padding: 3px;  border: 1px solid lightblue\">@@RowDetails</td> "
                + "<td style=\"background-color: #eee;color: black;padding: 3px;  border: 1px solid lightblue\">@@RowPRF</td></tr>";


            string Row = RowTD.Replace("@@RowDetails", "OEM Name").Replace("@@RowPRF", "AJAX ENGINEERING PRIVATE LIMITED")
                + RowTD.Replace("@@RowDetails", "Engine Serial No").Replace("@@RowPRF", SDMS_ICTicket.Equipment.EngineSerialNo)
                + RowTD.Replace("@@RowDetails", "Equipment Type & Model").Replace("@@RowPRF", SDMS_ICTicket.Equipment.EquipmentModel.Model)
                + RowTD.Replace("@@RowDetails", "Equipment Number").Replace("@@RowPRF", SDMS_ICTicket.Equipment.EquipmentSerialNo)
                + RowTD.Replace("@@RowDetails", "Actual Date of Commissioning (dd/mm/yyyy)").Replace("@@RowPRF", SDMS_ICTicket.Equipment.EquipmentSerialNo)

                + RowTD.Replace("@@RowDetails", "Customer Name (Person/Organisation)").Replace("@@RowPRF", Customer.CustomerName)
                + RowTD.Replace("@@RowDetails", "Customer Contact Number").Replace("@@RowPRF", Customer.Mobile)
                + RowTD.Replace("@@RowDetails", "End user / Customer Mail ID").Replace("@@RowPRF", Customer.Email)
                + RowTD.Replace("@@RowDetails", "Office Address").Replace("@@RowPRF", Customer.Address12)
                + RowTD.Replace("@@RowDetails", "Office Address City").Replace("@@RowPRF", Customer.City)
                // + RowTD.Replace("@@RowDetails", "Office Address District").Replace("@@RowPRF", Customer.District.District)
                + RowTD.Replace("@@RowDetails", "Office Address State").Replace("@@RowPRF", Customer.State.State)
                + RowTD.Replace("@@RowDetails", "Office Address Pin Code").Replace("@@RowPRF", Customer.Pincode)

                + RowTD.Replace("@@RowDetails", "Site Person Name").Replace("@@RowPRF", SDMS_ICTicket.SiteContactPersonName)
                + RowTD.Replace("@@RowDetails", "Site Person Contact number").Replace("@@RowPRF", SDMS_ICTicket.SiteContactPersonNumber)
                // + RowTD.Replace("@@RowDetails", "Site Address").Replace("@@RowPRF", )
                //+ RowTD.Replace("@@RowDetails", "Site Address City").Replace("@@RowPRF", )
                + RowTD.Replace("@@RowDetails", "Site Address District").Replace("@@RowPRF", SDMS_ICTicket.Address.District.District)
                + RowTD.Replace("@@RowDetails", "Site Address State").Replace("@@RowPRF", SDMS_ICTicket.Address.State.State);
            //   + RowTD.Replace("@@RowDetails", "Site Address Pin Code").Replace("@@RowPRF", )


            string Bottom = "</tbody></table><p>Thank You !</p></div></form></div></body></html>";
            Message = Top + Header + Row + Bottom;
            return Message;
        }
        private Byte[] SendPDF_PRF()
        {
            PDMS_Customer Customer = new SCustomer().getCustomerAddress(SDMS_ICTicket.Customer.CustomerCode);
            try
            {
                string contentType = string.Empty;
                contentType = "application/pdf";
                var CC = CultureInfo.CurrentCulture;
                string FileName = "File_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                string extension;
                string encoding;
                string mimeType;
                string[] streams;
                Warning[] warnings;
                LocalReport report = new LocalReport();
                report.EnableExternalImages = true;

                ReportParameter[] P = new ReportParameter[19];

                P[0] = new ReportParameter("EngineSerialNo", SDMS_ICTicket.Equipment.EngineSerialNo, false);
                P[1] = new ReportParameter("EquipmentModel", SDMS_ICTicket.Equipment.EquipmentModel.Model, false);

                P[2] = new ReportParameter("EquipmentSerialNo", SDMS_ICTicket.Equipment.EquipmentSerialNo, false);
                P[3] = new ReportParameter("CommissioningDate", txtRestoreDate.Text, false);
                P[4] = new ReportParameter("CustomerName", Customer.CustomerName, false);

                P[5] = new ReportParameter("Mobile", Customer.Mobile, false);
                P[6] = new ReportParameter("Email", Customer.Email, false);


                P[7] = new ReportParameter("OfficeAddress", Customer.Address12, false);

                P[8] = new ReportParameter("OfficeAddressCity", Customer.City, false);
                P[9] = new ReportParameter("OfficeAddressDistrict", "", false);
                P[10] = new ReportParameter("OfficeAddressState", Customer.State.State, false);
                P[11] = new ReportParameter("OfficeAddressPinCode", Customer.Pincode, false);


                P[12] = new ReportParameter("SitePersonName", SDMS_ICTicket.SiteContactPersonName, false);
                P[13] = new ReportParameter("SitePersonContactNumber", SDMS_ICTicket.SiteContactPersonNumber, false);
                P[14] = new ReportParameter("SiteAddress", "", false);
                P[15] = new ReportParameter("SiteAddressCity", "", false);
                P[16] = new ReportParameter("SiteAddressDistrict", SDMS_ICTicket.Address.District.District, false);
                P[17] = new ReportParameter("SiteAddressState", SDMS_ICTicket.Address.State.State, false);
                P[18] = new ReportParameter("SiteAddressPinCode", "", false);

                report.ReportPath = Server.MapPath("~/Print/DMS_PRF.rdlc");
                report.SetParameters(P);

                DataTable AvailabilityOfOtherMachineDT = new DataTable();
                AvailabilityOfOtherMachineDT.Columns.Add("TypeOfMachine");
                AvailabilityOfOtherMachineDT.Columns.Add("Qty");
                AvailabilityOfOtherMachineDT.Columns.Add("Mack");

                ReportDataSource rds1 = new ReportDataSource();
                rds1.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
                rds1.Value = AvailabilityOfOtherMachineDT;
                report.DataSources.Add(rds1);


                Byte[] mybytes = report.Render("PDF", null, out extension, out encoding, out mimeType, out streams, out warnings); //for exporting to PDF  
                return mybytes;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Please Contact Administrator. " + ex.Message;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            return null;
        }
    }
}