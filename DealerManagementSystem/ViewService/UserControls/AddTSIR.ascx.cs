using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem.ViewService.UserControls
{
    public partial class AddTSIR : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessageTSIR.Visible = false;
        }
        public void FillMaster(PDMS_ICTicket SDMS_ICTicket)
        {
           
            FillSROCoder();
         //   FillTSIRDetails();
        }
        public PDMS_ICTicket SDMS_ICTicket
        {
            get;
            set;
        }
        public PDMS_ICTicketTSIR ICTicketTSIR
        {
            get
            {
                if (ViewState["PDMS_ICTicketTSIR"] == null)
                {
                    ViewState["PDMS_ICTicketTSIR"] = new PDMS_ICTicketTSIR();
                }
                return (PDMS_ICTicketTSIR)ViewState["PDMS_ICTicketTSIR"];
            }
            set
            {
                ViewState["PDMS_ICTicketTSIR"] = value;
            }
        }
        public List<PDMS_ICTicketTSIR> ICTicketTSIRs
        {
            get
            {
                if (Session["PDMS_ICTicketTSIRs"] == null)
                {
                    Session["PDMS_ICTicketTSIRs"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["PDMS_ICTicketTSIRs"];
            }
            set
            {
                Session["PDMS_ICTicketTSIRs"] = value;
            }
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
         
        //public void FillTSIRDetails()
        //{
        //    ICTicketTSIRs = new BDMS_ICTicketTSIR().GetICTicketTSIRBasicDetails(SDMS_ICTicket.ICTicketID);
        //    gvTSIR.DataSource = ICTicketTSIRs;
        //    gvTSIR.DataBind();
        //    string[] TsirCancel = ConfigurationManager.AppSettings["TsirCancel"].Split(',');
        //    for (int i = 0; i < gvTSIR.Rows.Count; i++)
        //    {
        //        LinkButton lblCancelTSIR = (LinkButton)gvTSIR.Rows[i].FindControl("lblCancelTSIR");
        //        lblCancelTSIR.Visible = false;
        //        if (TsirCancel.Contains(PSession.User.UserID.ToString()))
        //        {
        //            lblCancelTSIR.Visible = true;
        //        }
        //    }
        //}
        //protected void cbCheck_CheckedChanged(object sender, EventArgs e)
        //{
        //    GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        //    CheckBox cbCheck = (CheckBox)gvTSIR.Rows[gvRow.RowIndex].FindControl("cbCheck");
        //    if (cbCheck.Checked)
        //    {

        //        for (int i = 0; i < gvTSIR.Rows.Count; i++)
        //        {
        //            if (i != gvRow.RowIndex)
        //            {
        //                CheckBox cbChecki = (CheckBox)gvTSIR.Rows[i].FindControl("cbCheck");
        //                cbChecki.Checked = false;
        //            }
        //        }
        //        long TsirID = Convert.ToInt64(gvTSIR.DataKeys[gvRow.RowIndex].Value);
        //        PDMS_ICTicketTSIR TSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
        //        if (!((TSIR.Status.StatusID == (short)TSIRStatus.Requested) || (TSIR.Status.StatusID == (short)TSIRStatus.SendBack) || (TSIR.Status.StatusID == (short)TSIRStatus.Rerequested)))
        //        {
        //            cbCheck.Checked = false;
        //            lblMessageTSIR.Text = "You cannot edit this TSIR. It may be Checked or Approved or Rejected";
        //            lblMessageTSIR.Visible = true;
        //            lblMessageTSIR.ForeColor = Color.Red;
        //            ClearTSIR();
        //            return;
        //        }
        //        ICTicketTSIR = new PDMS_ICTicketTSIR();
        //        ICTicketTSIR.ICTicket = new PDMS_ICTicket { ICTicketID = SDMS_ICTicket.ICTicketID };
        //        ICTicketTSIR.TsirID = TsirID;

        //        txtNatureOfFailures.Text = TSIR.NatureOfFailures;
        //        txtProblemNoticedBy.Text = TSIR.ProblemNoticedBy;
        //        txtUnderWhatConditionFailureTaken.Text = TSIR.UnderWhatConditionFailureTaken;
        //        txtFailureDetails.Text = TSIR.FailureDetails;
        //        txtPointsChecked.Text = TSIR.PointsChecked;
        //        txtPossibleRootCauses.Text = TSIR.PossibleRootCauses;
        //        txtSpecificPointsNoticed.Text = TSIR.SpecificPointsNoticed;
        //        txtPartsInvoiceNumber.Text = TSIR.PartsInvoiceNumber;
        //        ViewState["TsirID"] = TSIR.TsirID;
             
        //    }
        //    else
        //    {
        //        ClearTSIR();

        //    } 
        //}

        public PDMS_ICTicketTSIR Read()
        {
            lblMessageTSIR.Visible = true;
            PDMS_ICTicketTSIR ICTicketTSIR = new PDMS_ICTicketTSIR();
            if (!Validation())
            {
                return ICTicketTSIR;
            }
           
            ICTicketTSIR.TsirID = ViewState["TsirID"] == null ? 0 : (long)ViewState["TsirID"];
            long ServiceChargeID = Convert.ToInt64(ddlServiceChargeID.SelectedValue);
            if (ICTicketTSIR.TsirID == 0)
            {
                foreach (PDMS_ICTicketTSIR TSIR in ICTicketTSIRs)
                {
                    if ((TSIR.ServiceCharge.Material.MaterialCode == ddlServiceChargeID.SelectedItem.Text) && (TSIR.Status.StatusID != (short)TSIRStatus.Canceled))
                    {
                        lblMessageTSIR.Text = "TSIR already Created for " + ddlServiceChargeID.SelectedItem.Text + " Service Code";
                        lblMessageTSIR.ForeColor = Color.Red;
                        return ICTicketTSIR;
                    }
                }
            }
            ICTicketTSIR.ICTicket = new PDMS_ICTicket();
            //ICTicketTSIR.ICTicket.ICTicketID = SDMS_ICTicket.ICTicketID;
            ICTicketTSIR.ServiceCharge = new PDMS_ServiceCharge();
            ICTicketTSIR.ServiceCharge.ServiceChargeID = ServiceChargeID;
            ICTicketTSIR.NatureOfFailures = txtNatureOfFailures.Text.Trim();
            ICTicketTSIR.ProblemNoticedBy = txtProblemNoticedBy.Text.Trim();
            ICTicketTSIR.UnderWhatConditionFailureTaken = txtUnderWhatConditionFailureTaken.Text.Trim();
            ICTicketTSIR.FailureDetails = txtFailureDetails.Text.Trim();
            ICTicketTSIR.PointsChecked = txtPointsChecked.Text.Trim();
            ICTicketTSIR.PossibleRootCauses = txtPossibleRootCauses.Text.Trim();
            ICTicketTSIR.SpecificPointsNoticed = txtSpecificPointsNoticed.Text.Trim();
            ICTicketTSIR.PartsInvoiceNumber = txtPartsInvoiceNumber.Text.Trim();
            return ICTicketTSIR;
        }

        public void FillSROCoder()
        {
            var productCodes = (from p1 in SS_ServiceCharge select new { p1.ServiceChargeID, p1.Material.MaterialCode, p1.Material.IsMainServiceMaterial, p1.Material.MaterialGroup }).Where(m => m.IsMainServiceMaterial == false && m.MaterialGroup != "891").Distinct();

            ddlServiceChargeID.DataSource = productCodes;
            ddlServiceChargeID.DataBind();
        }
            

        void ClearTSIR()
        { 
            txtNatureOfFailures.Text = ""; 
            txtProblemNoticedBy.Text = "";
            txtUnderWhatConditionFailureTaken.Text = "";
            txtFailureDetails.Text = "";
            txtPointsChecked.Text = "";
            txtPossibleRootCauses.Text = "";
            txtSpecificPointsNoticed.Text = "";
            txtPartsInvoiceNumber.Text = ""; 
            ViewState["TsirID"] = null; 
            List<PDMS_TSIRAttachedFile> UploadedFile = new List<PDMS_TSIRAttachedFile>();
             
        }

        Boolean Validation()
        {
            lblMessageTSIR.Visible = true;
            string Message = "";
            Boolean Ret = true;
            

            if (string.IsNullOrEmpty(txtNatureOfFailures.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the NatureOfFailures";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtProblemNoticedBy.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the How Was Problem Noticed / Who  / When";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtUnderWhatConditionFailureTaken.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Under What Condition Failure Taken";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtFailureDetails.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Failure Details";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtPointsChecked.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Points Checked";
                Ret = false;
            }

            if (string.IsNullOrEmpty(txtPossibleRootCauses.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the Possible Root Causes";
                Ret = false;
            }
            if (string.IsNullOrEmpty(txtSpecificPointsNoticed.Text.Trim()))
            {
                Message = Message + "<br/>Please Enter the SpecificPoints Noticed";
                Ret = false;
            } 
            lblMessageTSIR.Text = Message;
            return Ret;
        }
      
       
    }
}