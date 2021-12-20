using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Properties;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace DealerManagementSystem.ViewService
{
    public partial class ICTicketTSIRApprove : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Session["previousUrl"] = "DMS_ICTicketTSIRApprove.aspx";
            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            this.Page.MasterPageFile = "~/Dealer.master";
        }
        public List<PDMS_ICTicketTSIR> ICTicket
        {
            get
            {
                if (Session["DMS_ICTicketTSIRApprove"] == null)
                {
                    Session["DMS_ICTicketTSIRApprove"] = new List<PDMS_ICTicketTSIR>();
                }
                return (List<PDMS_ICTicketTSIR>)Session["DMS_ICTicketTSIRApprove"];
            }
            set
            {
                Session["DMS_ICTicketTSIRApprove"] = value;
            }
        }
        public PDMS_ICTicketTSIR ICTicketTSIR
        {
            get
            {
                if (Session["ICTicketTSIR"] == null)
                {
                    Session["ICTicketTSIR"] = new PDMS_ICTicketTSIR();
                }
                return (PDMS_ICTicketTSIR)Session["ICTicketTSIR"];
            }
            set
            {
                Session["ICTicketTSIR"] = value;
            }
        }
        public List<PDMS_ICTicketTSIRStatus> ICTicketStatus
        {
            get
            {
                if (Session["DMS_ICTicketStatus"] == null)
                {
                    Session["DMS_ICTicketStatus"] = new List<PDMS_ICTicketTSIRStatus>();
                }
                return (List<PDMS_ICTicketTSIRStatus>)Session["DMS_ICTicketStatus"];
            }
            set
            {
                Session["DMS_ICTicketStatus"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            if (PSession.User == null)
            {
                Response.Redirect(UIHelper.SessionFailureRedirectionPage);
            }
            if (!IsPostBack)
            {
                txtTSIRDateFrom.Text = "01/" + DateTime.Now.Month.ToString("0#") + "/" + DateTime.Now.Year;
                txtTSIRDateTo.Text = DateTime.Now.ToShortDateString();

                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    ddlDealerCode.Items.Add(new ListItem(PSession.User.ExternalReferenceID));
                    ddlDealerCode.Enabled = false;
                }
                else
                {
                    ddlDealerCode.Enabled = true;
                    fillDealer();
                }

                lblRowCount.Visible = false;
                ibtnArrowLeft.Visible = false;
                ibtnArrowRight.Visible = false;

                new BDMS_Model().GetTypeOfWarrantyDDL(ddlModelID, null, null);
                ICTicketStatus = new BDMS_ICTicketTSIR().GetTSIRStatus(null, null);

                ViewState["TsirStatusID"] = 0;
                ViewState["ServiceTypeID"] = 0;
                List<PModuleAccess> user = PSession.User.DMSModules;
                if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).Count() != 0))
                {
                    List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApproveLevel1).Count() != 0))
                    {
                        ViewState["TsirStatusID"] = (short)TSIRStatus.Rejected;
                        ViewState["ServiceTypeID"] = (short)DMS_ServiceType.GoodwillWarranty;
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApprove).Count() != 0))
                    {
                        ViewState["TsirStatusID"] = (short)TSIRStatus.SalesApprovedLevel1;
                        ViewState["ServiceTypeID"] = (short)DMS_ServiceType.GoodwillWarranty;
                    }
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                fillICTicket();
            }
            catch (Exception e1)
            {
                lblMessage.Text = e1.ToString();
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
        }
        void fillICTicket()
        {
            try
            {
                TraceLogger.Log(DateTime.Now);
                int? DealerID = ddlDealerCode.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDealerCode.SelectedValue);
                DateTime? ICTicketDateF = string.IsNullOrEmpty(txtICLoginDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateFrom.Text.Trim());
                DateTime? ICTicketDateT = string.IsNullOrEmpty(txtICLoginDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtICLoginDateTo.Text.Trim());

                DateTime? TSIRDateF = string.IsNullOrEmpty(txtTSIRDateFrom.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateFrom.Text.Trim());
                DateTime? TSIRDateT = string.IsNullOrEmpty(txtTSIRDateTo.Text.Trim()) ? (DateTime?)null : Convert.ToDateTime(txtTSIRDateTo.Text.Trim());
                List<PDMS_ICTicketTSIR> SOIs = null;

                int? ModelID = ddlModelID.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlModelID.SelectedValue);
                int? TsirStatusID = (short)ViewState["TsirStatusID"];
                int? ServiceTypeID = (short)ViewState["ServiceTypeID"];

                SOIs = new BDMS_ICTicketTSIR().GetICTicketTSIRForApprove(DealerID, txtCustomerCode.Text.Trim(), txtTSIRNo.Text.Trim(), TSIRDateF, TSIRDateT
                   , txtICTicketNumber.Text.Trim(), ICTicketDateF, ICTicketDateT, txtSroCode.Text.Trim(), ModelID, TsirStatusID, ServiceTypeID);

                if (ddlDealerCode.SelectedValue == "0")
                {
                    var SOIs1 = (from S in SOIs join D in PSession.User.Dealer on S.ICTicket.Dealer.DealerCode equals D.UserName select new { S }).ToList();
                    SOIs.Clear();
                    foreach (var w in SOIs1)
                    {
                        SOIs.Add(w.S);
                    }
                }

                //List<PDMS_Division> Division = new BDMS_ICTicketTSIR().GetICTicketTSIRUserDivisionList(PSession.User.UserID);
                //if (Division.Count != 0)
                //{
                //    var SOIs2 = (from S in SOIs join D in Division on S.ICTicket.Equipment.EquipmentModel.Division.DivisionID equals D.DivisionID select new { S }).ToList();
                //    SOIs.Clear();
                //    foreach (var w in SOIs2)
                //    {
                //        SOIs.Add(w.S);
                //    }
                //}


                ICTicket = SOIs;
                gvICTickets.PageIndex = 0;
                gvICTickets.DataSource = SOIs;
                gvICTickets.DataBind();
                if (SOIs.Count == 0)
                {
                    lblRowCount.Visible = false;
                    ibtnArrowLeft.Visible = false;
                    ibtnArrowRight.Visible = false;
                }
                else
                {
                    lblRowCount.Visible = true;
                    ibtnArrowLeft.Visible = true;
                    ibtnArrowRight.Visible = true;
                    lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
                }
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID == (short)UserTypes.Dealer)
                {
                    gvICTickets.Columns[16].Visible = false;
                }
                string[] MailToSupplier = ConfigurationManager.AppSettings["MailToSupplier"].Split(',');
                if (MailToSupplier.Contains(PSession.User.UserID.ToString()))
                {
                    gvICTickets.Columns[19].Visible = true;
                }
                TraceLogger.Log(DateTime.Now);
            }
            catch (Exception e1)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRManage", "fillClaim", e1);
                throw e1;
            }
        }
        protected void ibtnArrowLeft_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageIndex > 0)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex - 1;

                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void ibtnArrowRight_Click(object sender, ImageClickEventArgs e)
        {
            if (gvICTickets.PageCount > gvICTickets.PageIndex)
            {
                gvICTickets.DataSource = ICTicket;
                gvICTickets.PageIndex = gvICTickets.PageIndex + 1;
                gvICTickets.DataBind();
                lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;
            }
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSIR");
            dt.Columns.Add("TSIR Date");
            dt.Columns.Add("FSR");
            dt.Columns.Add("FSR Date");

            dt.Columns.Add("Commissioning Date");
            dt.Columns.Add("M/C Dispatch Date");
            dt.Columns.Add("Type Of Warranty");

            dt.Columns.Add("Nature of Failure");
            dt.Columns.Add("Failure Details");
            dt.Columns.Add("Points checked");
            dt.Columns.Add("Possible Root Causes / Specific Points Noticed");

            dt.Columns.Add("IC Ticket");
            dt.Columns.Add("IC Ticket Date");
            dt.Columns.Add("Cust. Code");
            dt.Columns.Add("Cust. Name");
            dt.Columns.Add("Dealer Code");
            dt.Columns.Add("Dealer Name");

            dt.Columns.Add("HMR");
            dt.Columns.Add("Application");
            dt.Columns.Add("Machine Serial Number");
            dt.Columns.Add("Machine Model");

            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("Location");
            dt.Columns.Add("TSIR Status");
            foreach (PDMS_ICTicketTSIR IC in ICTicket)
            {
                dt.Rows.Add(
                      IC.TsirNumber
                      , IC.TsirDate.ToShortDateString()
                      , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRNumber
                     , IC.ICTicket.FSR == null ? "" : IC.ICTicket.FSR.FSRDate.ToShortDateString()
                    , IC.ICTicket.Equipment.CommissioningOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.CommissioningOn).ToShortDateString()
                     , IC.ICTicket.Equipment.DispatchedOn == null ? "" : ((DateTime)IC.ICTicket.Equipment.DispatchedOn).ToShortDateString()
                     , IC.ICTicket.TypeOfWarranty == null ? "" : IC.ICTicket.TypeOfWarranty.TypeOfWarranty
                     , IC.NatureOfFailures
                     , IC.FailureDetails
                     , IC.PointsChecked
                     , IC.PossibleRootCauses
                    , IC.ICTicket.ICTicketNumber
                    , IC.ICTicket.ICTicketDate.ToShortDateString()
                   , IC.ICTicket.Customer.CustomerCode
                    , IC.ICTicket.Customer.CustomerName
                    , IC.ICTicket.Dealer.DealerCode
                    , IC.ICTicket.Dealer.DealerName
                     , IC.ICTicket.CurrentHMRValue
                     , IC.ICTicket.MainApplication.MainApplication
                    , IC.ICTicket.Equipment.EquipmentSerialNo
                    , IC.ICTicket.Equipment.EquipmentModel.Model
                   , IC.ICTicket.Address.State.State
                    , IC.ICTicket.Address.District.District
                    , IC.ICTicket.Location
                    , IC.Status.Status
                    );
            }
            new BXcel().ExporttoExcel(dt, "TSIR Details");
        }
        void fillDealer()
        {
            ddlDealerCode.DataTextField = "CodeWithName";
            ddlDealerCode.DataValueField = "UserName";
            ddlDealerCode.DataSource = PSession.User.Dealer;
            ddlDealerCode.DataBind();

            ddlDealerCode.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void gvICTickets_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblStatusID = (Label)e.Row.FindControl("lblStatusID");
                    if (lblStatusID.Text == "1")
                    {
                        TextBox txtStatusRemarks = (TextBox)e.Row.FindControl("txtStatusRemarks");
                        Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                        Button btnReject = (Button)e.Row.FindControl("btnReject");
                        txtStatusRemarks.Visible = true;
                        btnApprove.Visible = true;
                        btnReject.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                new FileLogger().LogMessage("DMS_ICTicketTSIRManage", "gvICTickets_RowDataBound", ex);
                throw ex;
            }
        }
        protected void gvICTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvICTickets.PageIndex = e.NewPageIndex;
            gvICTickets.DataSource = ICTicket;
            gvICTickets.DataBind();
            lblRowCount.Text = (((gvICTickets.PageIndex) * gvICTickets.PageSize) + 1) + " - " + (((gvICTickets.PageIndex) * gvICTickets.PageSize) + gvICTickets.Rows.Count) + " of " + ICTicket.Count;

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            pnlTSIRManage.Visible = false;
            pnlTSIRView.Visible = true;

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            long TsirID = Convert.ToInt64(gvICTickets.DataKeys[gvRow.RowIndex].Value);
            FillTsirDetails(TsirID);
        }
        void FillTsirDetails(long TsirID)
        {
            txtNatureOfFailures.Enabled = false;
            txtProblemNoticedBy.Enabled = false;
            txtUnderWhatConditionFailureTaken.Enabled = false;
            txtFailureDetails.Enabled = false;
            txtPointsChecked.Enabled = false;
            txtPossibleRootCauses.Enabled = false;
            txtSpecificPointsNoticed.Enabled = false;
            txtPartsInvoiceNumber.Enabled = false;
            //  btnSave.Visible = false;

            ICTicketTSIR = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(TsirID, null);
            txtTsirNumber.Text = ICTicketTSIR.TsirNumber;
            txtTsirStatus.Text = ICTicketTSIR.Status.Status;
            txtServiceCharge.Text = ICTicketTSIR.ServiceCharge.Material.MaterialCode + " - " + ICTicketTSIR.ServiceCharge.Material.MaterialDescription;

            txtNatureOfFailures.Text = ICTicketTSIR.NatureOfFailures;
            txtProblemNoticedBy.Text = ICTicketTSIR.ProblemNoticedBy;
            txtUnderWhatConditionFailureTaken.Text = ICTicketTSIR.UnderWhatConditionFailureTaken;
            txtFailureDetails.Text = ICTicketTSIR.FailureDetails;
            txtPointsChecked.Text = ICTicketTSIR.PointsChecked;
            txtPossibleRootCauses.Text = ICTicketTSIR.PossibleRootCauses;
            txtSpecificPointsNoticed.Text = ICTicketTSIR.SpecificPointsNoticed;
            txtPartsInvoiceNumber.Text = ICTicketTSIR.PartsInvoiceNumber;

            txtSales1ApproveAmount.Text = Convert.ToString(ICTicketTSIR.Sales1ApproveAmount);
            txtSales2ApproveAmount.Text = Convert.ToString(ICTicketTSIR.Sales2ApproveAmount);

            ViewState["TsirID"] = ICTicketTSIR.TsirID;

            FillMessage(TsirID);

            PDMS_ICTicket ICTicket = new BDMS_ICTicket().GetICTicketByICTIcketID(ICTicketTSIR.ICTicket.ICTicketID);
            UC_BasicInformation.SDMS_ICTicket = ICTicket;
            UC_BasicInformation.FillBasicInformation();

            gvMaterial.DataSource = new BDMS_Service().GetServiceMaterials(ICTicket.ICTicketID, null, TsirID, "", false, "");
            gvMaterial.DataBind();

            btnSalesApprove1.Visible = false;
            btnSalesApprove2.Visible = false;
            btnSalesReject.Visible = false;

            List<PModuleAccess> user = PSession.User.DMSModules;

            List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;
            if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApproveLevel1).Count() != 0))
            {
                btnSalesReject.Visible = true;
                btnSalesApprove1.Visible = true;
            }
            if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApprove).Count() != 0))
            {
                btnSalesReject.Visible = true;
                btnSalesApprove2.Visible = true;
            }
        }
        void FillMessage(long TsirID)
        {
            Boolean? DisplayToDealer = null;

            if (PSession.User.UserTypeID == (short)UserTypes.Dealer)
            {
                DisplayToDealer = true;
                gvTSIRMessage.Columns[1].Visible = false;
            }


            List<PDMS_ICTicketTSIRMessage> TSIRMessage = new BDMS_ICTicketTSIR().GetICTicketTSIRMessage(null, TsirID, DisplayToDealer);
            if (TSIRMessage.Count == 0)
            {
                PDMS_ICTicketTSIRMessage N = new PDMS_ICTicketTSIRMessage();
                TSIRMessage.Add(N);
            }
            gvTSIRMessage.DataSource = TSIRMessage;
            gvTSIRMessage.DataBind();
        }
        protected void lblTSIRMessageAdd_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;
            TextBox txtTSIRMessage = (TextBox)gvTSIRMessage.FooterRow.FindControl("txtTSIRMessage");
            CheckBox cbDisplayToDealer = (CheckBox)gvTSIRMessage.FooterRow.FindControl("cbDisplayToDealer");

            if (string.IsNullOrEmpty(txtTSIRMessage.Text.Trim()))
            {
                lblMessage.Text = "Please enter the Message";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            long TsirID = Convert.ToInt64(ViewState["TsirID"]);
            Boolean DisplayToDealer = cbDisplayToDealer.Checked;
            if (PSession.User.UserTypeID == (short)UserTypes.Dealer)
            {
                DisplayToDealer = true;
            }
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRMessage(TsirID, txtTSIRMessage.Text.Trim(), DisplayToDealer, PSession.User.UserID))
            {
                lblMessage.Text = "New Message is added for this TSIR";
                lblMessage.ForeColor = Color.Green;
                FillMessage(TsirID);
                txtTSIRMessage.Text = "";
                cbDisplayToDealer.Checked = false;
            }
            else
            {
                lblMessage.Text = "New Message is not added for this TSIR";
                lblMessage.ForeColor = Color.Red;
            }
        }

        //protected void btnChecked_Click(object sender, EventArgs e)
        //{
        //    int StatusID = 1;
        //    if (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Requested)
        //    {
        //        StatusID = (short)TSIRStatus.Checked;
        //        lblMessage.Text = "TSIR Status changed to Checked";
        //    }
        //    else if (ICTicketTSIR.Status.StatusID == (short)TSIRStatus.Checked)
        //    {

        //        StatusID = (short)TSIRStatus.Approved;
        //        lblMessage.Text = "TSIR Status changed to Approved";
        //    }

        //    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, StatusID, PSession.User.UserID))
        //    {

        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR Status is not changed";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //    lblMessage.Visible = true;
        //}

        ////protected void btnSendBack_Click(object sender, EventArgs e)
        ////{
        ////    if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.SendBack, PSession.User.UserID))
        ////    {
        ////        lblMessage.Text = "TSIR Status changed to Send Back";
        ////        lblMessage.ForeColor = Color.Green;
        ////        FillTsirDetails(ICTicketTSIR.TsirID);
        ////    }
        ////    else
        ////    {
        ////        lblMessage.Text = "TSIR Status is not changed";
        ////        lblMessage.ForeColor = Color.Red;
        ////    }
        ////    lblMessage.Visible = true;
        ////}





        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (!Validation())
        //    {
        //        return;
        //    }
        //    ICTicketTSIR.TsirID = ViewState["TsirID"] == null ? 0 : (long)ViewState["TsirID"];

        //    ICTicketTSIR.ServiceCharge = new PDMS_ServiceCharge();
        //    ICTicketTSIR.ServiceCharge.ServiceChargeID = new BDMS_ICTicketTSIR().GetICTicketTSIRByTsirID(ICTicketTSIR.TsirID, null).ServiceCharge.ServiceChargeID;
        //    ICTicketTSIR.NatureOfFailures = txtNatureOfFailures.Text.Trim();
        //    ICTicketTSIR.ProblemNoticedBy = txtProblemNoticedBy.Text.Trim();
        //    ICTicketTSIR.UnderWhatConditionFailureTaken = txtUnderWhatConditionFailureTaken.Text.Trim();
        //    ICTicketTSIR.FailureDetails = txtFailureDetails.Text.Trim();
        //    ICTicketTSIR.PointsChecked = txtPointsChecked.Text.Trim();
        //    ICTicketTSIR.PossibleRootCauses = txtPossibleRootCauses.Text.Trim();
        //    ICTicketTSIR.SpecificPointsNoticed = txtSpecificPointsNoticed.Text.Trim();
        //    ICTicketTSIR.PartsInvoiceNumber = txtPartsInvoiceNumber.Text.Trim();
        //    long DealerEmployeeID = new BDMS_ICTicketTSIR().InsertOrUpdateICTicketTSIR(ICTicketTSIR, PSession.User.UserID);
        //    if (DealerEmployeeID != 0)
        //    {
        //        lblMessage.Text = "TSIR is updated successfully";
        //        lblMessage.ForeColor = Color.Green;
        //        FillTsirDetails(ICTicketTSIR.TsirID);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "TSIR is not updated successfully";
        //        lblMessage.ForeColor = Color.Red;
        //    }
        //}
        Boolean Validation()
        {
            lblMessage.Visible = true;
            string Message = "";
            Boolean Ret = true;
            //if (string.IsNullOrEmpty(txtFailureRepeats.Text.Trim()))
            //{
            //    Message = Message + "<br/>Please Enter the Failure Repeats";
            //    Ret = false;
            //}

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
            lblMessage.Text = Message;
            return Ret;
        }

        protected void btnSalesApprove1_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;

            if (string.IsNullOrEmpty(txtSales1ApproveAmount.Text.Trim()))
            {
                lblMessage.Text = "Please check the Sales 1 Approve Amount";
                lblMessage.ForeColor = Color.Red;
                return;
            }
            Decimal ApproveAmount = Convert.ToDecimal(txtSales1ApproveAmount.Text.Trim());
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.SalesApprovedLevel1, PSession.User.UserID, ApproveAmount))
            {
                lblMessage.Text = "TSIR Status changed to Sales Approved";
                lblMessage.ForeColor = Color.Green;
                FillTsirDetails(ICTicketTSIR.TsirID);
                btnSalesApprove1.Visible = false;
            }
            else
            {
                lblMessage.Text = "TSIR Status is not changed";
                lblMessage.ForeColor = Color.Red;
            }

        }
        protected void btnSalesApprove2_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = true;

            if (string.IsNullOrEmpty(txtSales2ApproveAmount.Text.Trim()))
            {
                lblMessage.Text = "Please check the Sales 2 Approve Amount";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            Decimal ApproveAmount = Convert.ToDecimal(txtSales2ApproveAmount.Text.Trim());
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.SalesApproved, PSession.User.UserID, ApproveAmount))
            {
                lblMessage.Text = "TSIR Status changed to Sales Approved";
                lblMessage.ForeColor = Color.Green;
                FillTsirDetails(ICTicketTSIR.TsirID);
            }
            else
            {
                lblMessage.Text = "TSIR Status is not changed";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnSalesReject_Click(object sender, EventArgs e)
        {
            if (new BDMS_ICTicketTSIR().UpdateICTicketTSIRStatus(ICTicketTSIR.TsirID, (short)TSIRStatus.SalesRejected, PSession.User.UserID, 0))
            {
                lblMessage.Text = "TSIR Status changed to Rejected";
                lblMessage.ForeColor = Color.Green;
                FillTsirDetails(ICTicketTSIR.TsirID);
            }
            else
            {
                lblMessage.Text = "TSIR Status is not changed";
                lblMessage.ForeColor = Color.Red;
            }
            lblMessage.Visible = true;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlTSIRManage.Visible = true;
            pnlTSIRView.Visible = false;
        }
    }
}