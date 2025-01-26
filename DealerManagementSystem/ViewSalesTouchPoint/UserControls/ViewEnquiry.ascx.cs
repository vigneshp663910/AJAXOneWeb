using Business;
using Newtonsoft.Json;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewSalesTouchPoint.UserControls
{
    public partial class ViewEnquiry : System.Web.UI.UserControl
    {
        public PSalesTouchPointEnquiry SalesTouchPointEnquiry
        {
            get
            {
                if (ViewState["EnquiryView"] == null)
                {
                    ViewState["EnquiryView"] = new PEnquiry();
                }
                return (PSalesTouchPointEnquiry)ViewState["EnquiryView"];
            }
            set
            {
                ViewState["EnquiryView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            lblAddEnquiryMessage.Text = "";
        }
        public void fillViewEnquiry(long SalesTouchPointEnquiryID)
        {
            // ViewState["EnquiryID"] = EnquiryID;
            SalesTouchPointEnquiry = new BSalesTouchPoint().GetSalesTouchPointEnquiryByID(SalesTouchPointEnquiryID);
            lblSalesTouchPointEnquiryNumber.Text = SalesTouchPointEnquiry.SalesTouchPointEnquiryNumber;
            lblCustomerName.Text = SalesTouchPointEnquiry.CustomerName;
            lblPersonName.Text = SalesTouchPointEnquiry.PersonName;
            lblEnquiryDate.Text = SalesTouchPointEnquiry.SalesTouchPointEnquiryDate.ToString("dd/MM/yyyy HH:mm:ss");
            lblCountry.Text = SalesTouchPointEnquiry.Country.Country.ToString();
            lblState.Text = SalesTouchPointEnquiry.State.State.ToString();
            lblDistrict.Text = SalesTouchPointEnquiry.District.District.ToString();
            lblAddress.Text = SalesTouchPointEnquiry.Address.ToString();
            lblAddress2.Text = SalesTouchPointEnquiry.Address2.ToString();
            lblAddress3.Text = SalesTouchPointEnquiry.Address3.ToString();
            lblPincode.Text = SalesTouchPointEnquiry.Pincode.ToString();
            lblMobile.Text = "<a href='tel:" + SalesTouchPointEnquiry.Mobile + "'>" + SalesTouchPointEnquiry.Mobile + "</a>";
            lblMail.Text = "<a href='mailto:" + SalesTouchPointEnquiry.Mail + "'>" + SalesTouchPointEnquiry.Mail + "</a>";
            lblRemarks.Text = SalesTouchPointEnquiry.Remarks;
            lblStatus.Text = SalesTouchPointEnquiry.Status.Status;
            

            //fillEnquiryStatusHistory();
            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Edit Enquiry")
            {
                MPE_Enquiry.Show();
                UC_AddEnquiry.FillMaster();
                UC_AddEnquiry.Write(SalesTouchPointEnquiry);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_Enquiry.Show();
                string Message = UC_AddEnquiry.Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblAddEnquiryMessage.Text = Message;
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                    return;
                }
                PSalesTouchPointEnquiry_Insert enquiryAdd = UC_AddEnquiry.Read();

                enquiryAdd.SalesTouchPointEnquiryID = SalesTouchPointEnquiry.SalesTouchPointEnquiryID;

                PApiResult Result = JsonConvert.DeserializeObject<PApiResult>(new BAPI().ApiPut("SalesTouchPointEnquiry", enquiryAdd));
                if (Result.Status == PApplication.Failure)
                {
                    lblAddEnquiryMessage.Text = Result.Message;
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                    return;
                }
                int SalesTouchPointEnquiryID = Convert.ToInt32(Result.Data);

                if (SalesTouchPointEnquiryID != 0)
                {
                    lblMessage.Text = "SalesTouchPoint Enquiry is updated successfully";
                    lblMessage.ForeColor = Color.Green;                    
                    fillViewEnquiry(SalesTouchPointEnquiry.SalesTouchPointEnquiryID);
                    MPE_Enquiry.Hide();
                }
                else
                {
                    lblAddEnquiryMessage.Text = "SalesTouchPoint Enquiry is not updated successfully";
                    lblAddEnquiryMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblAddEnquiryMessage.Text = ex.Message.ToString();
                lblAddEnquiryMessage.ForeColor = Color.Red;
            }
        }
        void ActionControlMange()
        {
            lbEditEnquiry.Visible = true;
            int StatusID = SalesTouchPointEnquiry.Status.StatusID;
            if (StatusID != (short)SalesTouchPointEnquiryStatus.Created)
            {
                lbEditEnquiry.Visible = false;
            }
        }
    }
}