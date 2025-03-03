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

namespace DealerManagementSystem.ViewPreSale.UserControls
{
    public partial class ViewSalesTouchPointEnquiry : System.Web.UI.UserControl
    {
        public PSalesTouchPointEnquiry SalesTouchPointEnquiry
        {
            get
            {
                if (ViewState["SalesTouchPointEnquiryView"] == null)
                {
                    ViewState["SalesTouchPointEnquiryView"] = new PEnquiry();
                }
                return (PSalesTouchPointEnquiry)ViewState["SalesTouchPointEnquiryView"];
            }
            set
            {
                ViewState["SalesTouchPointEnquiryView"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }
        public void fillViewEnquiry(long SalesTouchPointEnquiryID)
        {
            // ViewState["EnquiryID"] = EnquiryID;
            SalesTouchPointEnquiry = new BSalesTouchPoint().GetSalesTouchPointEnquiryByID(SalesTouchPointEnquiryID);
            lblSalesTouchPointEnquiryNumber.Text = SalesTouchPointEnquiry.SalesTouchPointEnquiryNumber;
            lblCustomerName.Text = SalesTouchPointEnquiry.CustomerName;
            lblPersonName.Text = SalesTouchPointEnquiry.PersonName;
            lblSalesTouchPointEnquiryDate.Text = SalesTouchPointEnquiry.SalesTouchPointEnquiryDate.ToString("dd/MM/yyyy HH:mm:ss");
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
            lblEnquiryNumber.Text = SalesTouchPointEnquiry.EnquiryNumber;
            lblStatus.Text = SalesTouchPointEnquiry.Status.Status;
            ActionControlMange();
        }
        protected void lbActions_Click(object sender, EventArgs e)
        {
            LinkButton lbActions = ((LinkButton)sender);
            if (lbActions.Text == "Reject")
            {
                MPE_EnquiryReject.Show();
            }
            if (lbActions.Text == "Convert To Enquiry")
            {
                MPE_Enquiry.Show();
                UC_AddEnquiry.Write(ConvertPresaleEnquiry(SalesTouchPointEnquiry));
                //UC_AddEnquiry.WriteSalesTouchPointEnquiry(SalesTouchPointEnquiry);
            }
        }
        PEnquiry ConvertPresaleEnquiry(PSalesTouchPointEnquiry Enq)
        {
            PEnquiry PEnq = new PEnquiry();
            PEnq.CustomerName = Enq.CustomerName;
            PEnq.PersonName = Enq.PersonName;
            PEnq.Mobile = Enq.Mobile;
            PEnq.Mail = Enq.Mail;
            PEnq.Country = Enq.Country;
            PEnq.State = Enq.State;
            PEnq.District = Enq.District;
            PEnq.Address = Enq.Address;
            PEnq.Address2 = Enq.Address2;
            PEnq.Address3 = Enq.Address3;
            PEnq.Remarks = Enq.Remarks;
            return PEnq;
        }
        protected void btnEnquiryReject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnquiryRejectReason.Text))
            {
                MPE_EnquiryReject.Show();
                lblMessageResponsible.ForeColor = Color.Red;
                lblMessageResponsible.Text = "Please enter remark";
                return;
            }
            PApiResult Result = new BSalesTouchPoint().UpdateSalesTouchPointEnquiryReject(SalesTouchPointEnquiry.SalesTouchPointEnquiryID, txtEnquiryRejectReason.Text.Trim());
            if (Result.Status == PApplication.Failure)
            {
                MPE_EnquiryReject.Show();
                lblMessageResponsible.ForeColor = Color.Red;
                lblMessageResponsible.Text = Result.Message;
                return;
            }
            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = Result.Message;
            fillViewEnquiry(SalesTouchPointEnquiry.SalesTouchPointEnquiryID);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MPE_Enquiry.Show();
                string Message = UC_AddEnquiry.Validation();
                if (!string.IsNullOrEmpty(Message))
                {
                    lblMessage.Text = Message;
                    return;
                }
                PEnquiry enquiryAdd = UC_AddEnquiry.Read();

                long EnquiryID = new BEnquiry().InsertOrUpdateEnquiry(enquiryAdd, PSession.User.UserID);
                if (EnquiryID != 0)
                {
                    PApiResult Result = new BSalesTouchPoint().UpdateSalesTouchPointEnquiryFromPreSalesEnquiry(SalesTouchPointEnquiry.SalesTouchPointEnquiryID, EnquiryID);
                    if (Result.Status == PApplication.Failure)
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = Result.Message;
                        return;
                    }
                    lblMessage.Text = "Sales Touch Point Enquiry is Converted To Enquiry successfully...";
                    lblMessage.ForeColor = Color.Green;
                    fillViewEnquiry(SalesTouchPointEnquiry.SalesTouchPointEnquiryID);
                    MPE_Enquiry.Hide();
                }
                else
                {
                    lblAddEnquiryMessage.Text = "Sales Touch Point Enquiry is not Converted To Enquiry successfully...!";
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
            lbReject.Visible = true;
            lbConvertToEnquiry.Visible = true;
            int StatusID = SalesTouchPointEnquiry.Status.StatusID;
            List<PSubModuleChild> SubModuleChild = PSession.User.SubModuleChild;
            if (StatusID == (short)SalesTouchPointEnquiryStatus.Converted)
            {
                lbReject.Visible = false;
                lbConvertToEnquiry.Visible = false;
            }
            if (StatusID == (short)SalesTouchPointEnquiryStatus.Rejected)
            {
                lbReject.Visible = false;
                lbConvertToEnquiry.Visible = false;
            }
            if (SubModuleChild.Where(A => A.SubModuleChildID == (short)SubModuleChildMaster.EnquiryRejectOrConvert).Count() == 0)
            {
                lbReject.Visible = false;
                lbConvertToEnquiry.Visible = false;
            }
        }
    }
}