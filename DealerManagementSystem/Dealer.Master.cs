using Business;
using Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealerManagementSystem
{
    public partial class Dealer : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblPageName.Text = (string)Session["PageName"];
            Session["PageName"] = "";
            if (!IsPostBack)
            {
                lblQuality.Text = ConfigurationManager.AppSettings["IsQuality"];
                if (PSession.User == null)
                {
                    Response.Redirect(UIHelper.SessionFailureRedirectionPage);
                }
                lblWelcome.Text = PSession.User.ContactName;


                string MenuCon = "<ul id='topnav'>";
                //   string MenuReportCon = ""; 
                if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                {
                    MenuCon = MenuCon + "<li><a title='Services' href='CreateTicket.aspx'>New support ticket</a></li>";
                    MenuCon = MenuCon + "<li><a title='Enquiries' href='ManageTickets.aspx'>Check ticket status </a></li>";
                    if (new BFeedback().CheckPendingFeedback(PSession.User.UserID))
                    {
                        MenuCon = MenuCon + "<li><a title='Feedback' href='Feedback.aspx'>Feed back</a></li>";
                    }
                    //divbluemenu.Visible = false;
                }
                else
                {
                    // divbluemenu.Visible = true;
                }
                MenuCon = MenuCon + "<li style='float: right; margin-top: 0px;'><ul style='list-style-type: none;'>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='Home.aspx' style='white-space: pre;'><img src='Ajax/HomeLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='ChangePassword.aspx' style='white-space: pre;'><img src='Ajax/ChangePasswordLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "<li class='right-boarder'><a href='Login.aspx' style='white-space: pre;'><img src='Ajax/SignOutLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + " <li><a href='DMS_ContactUs.aspx' style='white-space: pre;'><img src='Ajax/ContactUsLogo.png'  width='17px' /></a></li>";
                MenuCon = MenuCon + "</ul></li></ul>";
                //if (PSession.User.SystemCategoryID == (short)SystemCategory.Dealer && PSession.User.UserTypeID != (short)UserTypes.Manager)
                //{
                //    MenuCon = "<a href=  style='border-right: 1px solid #e3e3e3; width:130px' ><label class='manulable'></label></a>";
                //    MenuCon = MenuCon + "<a href=Home.aspx  style='border-right: 1px solid #e3e3e3; width:170px' ><label class='manulable'>Home</label></a>";
                //    MenuCon = MenuCon + "<a href=CreateTicket.aspx  style='border-right: 1px solid #e3e3e3; width:170px' ><label class='manulable'>New support ticket</label></a>";
                //    MenuCon = MenuCon + "<a href=ManageTickets.aspx  style='border-right: 1px solid #e3e3e3; width:170px' ><label class='manulable'>Check ticket status</label></a>";

                //    if (new BFeedback().CheckPendingFeedback(PSession.User.UserID))
                //    {
                //        MenuCon = MenuCon + "<a href=Feedback.aspx  style='border-right: 1px solid #e3e3e3; width:170px'><label class='manulable'>Feed back</label></a>";
                //    }
                //}
                //else
                //{
                //    MenuReportCon = "<li class='active'><a href='#'><span>" + "DMS Report" + "</span></a><ul>";
                //    MenuReportCon = MenuReportCon + "<li><a href=" + "DMS_MTTR_Report.aspx" + "><span>" + "MTTR Report" + "</span></a></li>";
                //    MenuReportCon = MenuReportCon + "</ul></li> ";
                //}
                DivMenu.InnerHtml = MenuCon;
                //  MenuReport.InnerHtml = MenuReportCon;
                menu();
            }
        }
        void menu()
        {
            List<PModuleAccess> user = PSession.User.DMSModules;

            string MenuDMS = "<nav id='main-nav'><ul id='main-menu' class='sm sm-blue'>";


            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Dashboard).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href='#'>Dashboard</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Dashboard).SubModuleAccess;
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DashboardICTicketService).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_DashboardICTicketService.aspx'>Service</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DashboardICTicketClaim).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_DashboardICTicketClaim.aspx'>Claim</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }

            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Admin).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href='#'>Admin</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Admin).SubModuleAccess;
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.UserManagement).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_UserManagement.aspx'>User Management</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.MobileUserApprove).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/RegisterUserMobileApprove.aspx'>Mobile User Approve</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DMSUserManagement).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/ChangeDealerEmployeeName.aspx'>Dealer Employee</a></li>";

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsQuality"]))
                {
                    MenuDMS = MenuDMS + "<li><a href='/DMS_AddressStateManage.aspx'>State Manage</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_AddressDistrictManage.aspx'>District Manage</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_AddressTehsilManage.aspx'>Tehsil Manage</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_EquipmentCreate.aspx'>Equipment Create</a></li>";


                    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketPDICheckList1Verify.aspx'>PDI Check List1 Verify</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketPDICheckList1Approve.aspx'>PDI Check List1 Approve</a></li>";
                }

                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((string)Session["LoginID"] == "admin")
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href='#'>User</a>";
                MenuDMS = MenuDMS + "<ul>";
                MenuDMS = MenuDMS + "<li><a href='/DMS_UserList.aspx'>User List</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }

            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Dealer).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href='#'>Dealer</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Dealer).SubModuleAccess;
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeCreate || m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeManage
                    || m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeApproval
                    || m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeRoleAssign || m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeLeaving).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Dealer Manpower Management</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeCreate).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DealerEmployeeCreate.aspx'>Employee Create</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeManage).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DealerEmployeeManage.aspx'>Employee Manage</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeApproval).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DealerEmployeeApproval.aspx'>Employee Approval</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeRoleAssign).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DealerEmployeeAssigningRole.aspx'>Employee  Role Assign</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DealerEmployeeLeaving).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DealerEmployeeLeaving.aspx'>Employee  Leaving</a></li>";
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";

            }

            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Master).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href='#'>Master</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Master).SubModuleAccess;

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Material).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_MaterialReport.aspx'>Material</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Customer).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_CustomerList.aspx'>Customer</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Equipment).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Equipment</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.BIN).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='DMS_MaterialBin.aspx'>BIN</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Price).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_MaterialPrice.aspx'>Price</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Supercede).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_MaterialSuperede.aspx'>Supersede</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ROQ_DOQ).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_MaterialRoqDoq.aspx'>ROQ/DOQ</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.GeographyManage || m.SubModuleMasterID == (short)DMS_MenuSub.TehsilTransfer
                  || m.SubModuleMasterID == (short)DMS_MenuSub.VillageTransfer).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Geography</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.GeographyManage).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Master/GeographicalMaster.aspx'>Geography Manage</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.TehsilTransfer).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Master/TehsilTransfer.aspx'>Tehsil Transfer</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.VillageTransfer).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Master/VillageTransfer.aspx'>Village Transfer</a></li>";
                    }
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Procurement).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Procurement</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Procurement).SubModuleAccess;

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.POList).Count() != 0))
                {
                    //MenuDMS = MenuDMS + "<li><a href='/DMS_PurchaseOrder.aspx'>PO List</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_PurchaseOrderReport.aspx'>PO List </li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_PurchaseOrderASNReport.aspx'> PO ASN List</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_PurchaseOrderInvoiceReport.aspx'>PO Invoice List</a></li>";
                }
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ASNList).Count() != 0))
                //    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>ASN List</a></li>";
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.GRList).Count() != 0))
                //    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>GR List</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.POReturn).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>PO Return</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PendingSAPPO).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Pending SAP PO</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PendingASN).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Pending ASN</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PendingGR).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Pending GR</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.POPerfomance).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_PurchaseOrderPerformance.aspx'>PO Perfomance</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Sales).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Sales</i></a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Sales).SubModuleAccess;

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationCreate || m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationApprove
                   || m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Web Quotation</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationCreate).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WebQuotationCreate.aspx'>Web Quotation Create</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationApprove).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WebQuotationApproval.aspx'>Web Quotation Approve</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WebQuotationReport.aspx'>Web Quotation Report</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WebQuotationSendToSAP).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WebQuotationSendToSAP.aspx'>Web Quotation Send To SAP</a></li>";
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }



                if ((sub.Where(m => m.SubModuleMasterID == 1).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Quotation</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderItem).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_SalesOrderItems.aspx'>Sale Order</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Delivery).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Delivery</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderInvoicePartsReport || m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderInvoiceMcReport
                    || m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderInvoiceWarrantyReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Invoice</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderInvoicePartsReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_SaleOrderInvoicePartsReport.aspx'>Parts Invoice Report</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderInvoiceMcReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_SaleOrderInvoiceMcReport.aspx'>Mc Invoice Report</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SaleOrderInvoiceWarrantyReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_SaleOrderInvoiceWarrantyReport.aspx'>Warranty Invoice Report</a></li>";
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                // MenuDMS = MenuDMS + "<li><a href='/DMS_SalesInvoice.aspx'>Invoice</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SalesReturn).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Sales Return</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PendingDelivery).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Pending Delivery</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PendingInvoice).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Pending Invoice</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.SalesPerfomance).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_SalesOrderPerformance.aspx'>Sales Perfomance</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Service</i></a>";
                MenuDMS = MenuDMS + "<ul>";

                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Service).SubModuleAccess;

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketManage || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketStatusReport
                   || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketListDUMP || m.SubModuleMasterID == (short)DMS_MenuSub.ApproveDeclinedICTicket
                    || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketRequestedDateChange || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketMarginWarrantyChange
 || m.SubModuleMasterID == (short)DMS_MenuSub.TicketTracking).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>IC Ticket</i></a>";
                    MenuDMS = MenuDMS + "<ul>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketManage).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketManage.aspx'>ICTicket Manage</a></li>";
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketCommissionMailToReport.aspx'>Commission Mail To Report</a></li>";
                    }

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketStatusReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketStatusReport.aspx'>ICTicket Status Report</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketListDUMP).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/ZDMS_ICTicketListDUMP.aspx'>ICTicket List DUMP</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ApproveDeclinedICTicket).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ApproveDeclinedICTicket.aspx'>Approve Declined ICTicket</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketRequestedDateChange).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketReachedDateChange.aspx'>IC Ticket Requested Date Change</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketMarginWarrantyChange).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketGoodWillWarrantyChange.aspx'>IC Ticket Margin Warranty Change</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.TicketTracking).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_TicketTracking.aspx'>Ticket Tracking</a></li>";


                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketManage).Count() != 0))
                //{
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketManage.aspx'>ICTicket Manage</a></li>"; 
                //}
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketStatusReport).Count() != 0))
                //{
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketStatusReport.aspx'>ICTicket Status Report</a></li>";
                //}
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketFSRManage).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketFSRManage.aspx'>FSR Report</a></li>";
                    //  MenuDMS = MenuDMS + "<li><a href='DMS_ICTicketReport.aspx'>ICTicket Report</a></li>";
                }
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTsirManage || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTsirReport
                   || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApproveLevel1 || m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApprove).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>TSIR</i></a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTsirManage).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketTSIRManage.aspx'>TSIR Report</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTsirReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketTSIRReport.aspx'>TSIR Report - Details</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApproveLevel1).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketTSIRApprove.aspx'>TSIR Sales Approve</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRSalesApprove).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketTSIRApprove.aspx'>TSIR Sales Approve</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTSIRMessage).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketTSIRMessage.aspx'>TSIR Message</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketTsirMailSendVendorReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketTSIRMailToVendorReport.aspx'>TSIR Mail Send Vendor Report</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.EquipmentHistory).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Equipment</i></a>";
                    MenuDMS = MenuDMS + "<ul>";

                    MenuDMS = MenuDMS + "<li><a href='/DMS_EquipmentHistory.aspx'>Equipment History</a></li>";
                    MenuDMS = MenuDMS + "<li><a href='/DMS_EquipmentPopulationReport.aspx'>Equipment Population Report</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.EquipmentPopulationReportForAE).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_EquipmentPopulationReportForAE.aspx'>Equipment Population Report For AE</a></li>";
                    }

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ServiceEngineerUtilisationReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketServiceEngineerUtilisationReport.aspx'>ServiceEngineerUtilisationReport</a></li>";
                }
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketListDUMP).Count() != 0))
                //{
                //    MenuDMS = MenuDMS + "<li><a href='/ZDMS_ICTicketListDUMP.aspx'>ICTicket List DUMP</a></li>";
                //}



                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ApproveDeclinedICTicket).Count() != 0))
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_ApproveDeclinedICTicket.aspx'>Approve Declined ICTicket</a></li>";

                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketRequestedDateChange).Count() != 0))
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketReachedDateChange.aspx'>IC Ticket Requested Date Change</a></li>";
                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ICTicketMarginWarrantyChange).Count() != 0))
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_ICTicketGoodWillWarrantyChange.aspx'>IC Ticket Margin Warranty Change</a></li>";


                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.MTTR || m.SubModuleMasterID == (short)DMS_MenuSub.MTTR_New).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>MTTR</a>";
                    MenuDMS = MenuDMS + "<ul>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.MTTR).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_MTTR_Report.aspx'>MTTR</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.MTTR_New).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_MTTR_New.aspx'>MTTR New </a></li>";
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.TicketTracking).Count() != 0))
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_TicketTracking.aspx'>Ticket Tracking</a></li>";


                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ClaimPrint).Count() != 0))
                //    MenuDMS = MenuDMS + "<li><a href='/DMS_ClaimPrint.aspx'>Claim Print</a></li>";


                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaim || m.SubModuleMasterID == (short)DMS_MenuSub.ApprovalStatus
                    || m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimInvoiceReport
                    || m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimInvoiceCreate || m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimAnnexureReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Warranty</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaim).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaim1.aspx'>Claim Report</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyDeliveryReport).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DeliveryReport.aspx'>Delivery Report(less than 50K)</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyReqForClaimApproval).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimApprovalRequest.aspx'>Req for Claim Approval(Above 50K)</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ApprovalStatus).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_ClaimApprovalList1.aspx'>Claim Approval</a></li>";
                    }

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimAnnexureCreate).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimAnnexureCreate.aspx'>Claim Annexure Create(NEPI & Warr<50K) </a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimAnnexureReport).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimAnnexureReport.aspx'>Claim Annexure Report(NEPI & Warr<50K)</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimInvoiceCreate).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimInvoiceCreate.aspx'>Final Invoice Create(NEPI & Warr<50K)</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFinalInvoiceCreateAbove50K).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimInvoiceCreate5k.aspx'>Final Invoice Create(Above 50K)</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyClaimInvoiceReport).Count() != 0))
                    {
                        //MenuDMS = MenuDMS + "<li><a href='DMS_ClaimInvoice.aspx'>Final Invoice Report</a></li>";
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimInvoiceReport.aspx'>Final Invoice Report</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyDebitNoteCreate).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimDebitNoteCreate.aspx'>Debit Note Create</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyDebitNoteAcknowledge).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimDebitNoteAcknowledge.aspx'>Debit Note Acknowledge</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyDebitNoteReport).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyClaimDebitNoteReport.aspx'>Debit Note Report</a></li>";
                    }
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialReturn || m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialDCCreation
                    || m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialDCReport || m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialDCGateEntry).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Failed Material</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialReturn).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyFailureMaterialReport.aspx'>Material Report</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialDCCreation).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyFailureMaterialDCTemplateCreation.aspx'>DC Draft</a></li>";
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyFailureMaterialDCCreation.aspx'>DC Creation</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialDCReport).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyFailureMaterialDCReport.aspx'>DC Report</a></li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyFailedMaterialDCGateEntry).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyFailureMaterialGateEntry.aspx'>DC Gate Entry</a></li>";
                    }
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                    //  MenuDMS = MenuDMS + "<li><a href='DMS_WarrantyFailureMaterialReturn.aspx'>Failed Material Return</a></li>";
                }
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidService || m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceQuotation
                   || m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceProformaInvoice || m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceInvoice).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Paid Service</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidService).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceReportNew.aspx'>Paid Service Report</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceQuotation).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceQuotation.aspx'>Paid Service Quotation</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceProformaInvoice).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceProformaInvoice.aspx'>Paid Service Proforma Invoice</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceInvoice).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceInvoice.aspx'>Paid Service Invoice</a></li>";
                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                if (!string.IsNullOrEmpty(lblQuality.Text))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>ASC</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidService).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceReportNew.aspx'>Create ASC Quotation</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceQuotation).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceQuotation.aspx'>ASC Quotatio Report</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceProformaInvoice).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_PaidServiceProformaInvoice.aspx'>ASC Invoice Report Invoice</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";

                }

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.CampignCreateOrUpdate || m.SubModuleMasterID == (short)DMS_MenuSub.CampignReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Campaign</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidService).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_CampignTicketCreate.aspx'>Campaign Ticket Create</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.PaidServiceQuotation).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_CampignReport.aspx'>Campaign Report</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }

                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ServicePerfomance).Count() != 0))
                //    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Service Perfomance</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.WarrantyPartsAvailabilityReport).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_WarrantyPartsAvailabilityReport.aspx'>Warranty Parts Availability Report</a></li>";

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedICTicketRequestForApproval || m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedICTicketApprove
                    || m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedCliamRequestForApproval || m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedCliamApprove).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Deviated</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedICTicketRequestForApproval).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DeviatedICTicketRequestForApproval.aspx'>Deviated ICTicket Request For Approval</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedICTicketApprove).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DeviatedICTicketApprove.aspx'>Deviated ICTicket Approve</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedCliamRequestForApproval).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DeviatedCliamRequestForApproval.aspx'>Deviated Cliam Request For Approval</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DeviatedCliamApprove).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_DeviatedCliamApprove.aspx'>Deviated Cliam Approve</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.EInvoiceRequest || m.SubModuleMasterID == (short)DMS_MenuSub.EDebitNoteRequest).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>E-Invoice</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.EInvoiceRequest).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_EInvoiceRequest.aspx'>E-Invoice Request</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.EDebitNoteRequest).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_EDebitNoteRequest.aspx'>E-Debit Note Request</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.UpdateCommissioningDate).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_UpdateCommissioningDate.aspx'>Update Commissioning Date</a></li>";

                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Stock).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Stock</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Stock).SubModuleAccess;

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ConvertedStock).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Converted Stock</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.StockOverview).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Stock Overview</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.StockSummary).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Stock Summary</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.DMS_AFAccount).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>DMS-AF Account</a>";
                MenuDMS = MenuDMS + "</li>";
            }
            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.MonthlySummary).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Monthly Summary</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.MonthlySummary).SubModuleAccess;
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Procurement).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/DMS_MonthlyProcurement.aspx'>Procurement</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Sales).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Sales</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Service).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Service</a></li>";
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.Stock).Count() != 0))
                    MenuDMS = MenuDMS + "<li style='background: #e7e0e0;'><a href='#'>Stock</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }

            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.Marketing).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Marketing</a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.Marketing).SubModuleAccess;

                //if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityInfoMaster).Count() != 0))
                //    MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ActivityInfoM.aspx'>Activity Info Master</a></li>";


                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityInfoMater || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityPlan || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityActual
                    || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalMarketingLevel1 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalMarketingLevel2
                    || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalServiceLevel1 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalServiceLevel2
                  || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSparesLevel2 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityInvoiceReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Activity</a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityInfoMater).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ActivityInfoM.aspx'>Activity Info Master</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityPlan || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityActual
                   || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalLevel1 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalLevel2).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li>";
                        MenuDMS = MenuDMS + "<a href=''#'>Field Activity</a>";
                        MenuDMS = MenuDMS + "<ul>";

                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityPlan).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ActivityPlan.aspx'>Activity Plan</a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityActual).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ActivityActual.aspx'>Activity Actual</a></li>";

                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalLevel1).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=1&FA=0'>Activity Claim Approval - Level 1 </a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalLevel2).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=2&FA=0'>Activity Claim Approval - Level 2</a></li>";
                        MenuDMS = MenuDMS + "</ul>";
                        MenuDMS = MenuDMS + "</li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DirectClaimEntry
                   || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalMarketingLevel1 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalMarketingLevel2
                   || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalServiceLevel1 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalServiceLevel2
                    || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSparesLevel2 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSalesLevel1
                     || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSalesLevel2 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalTrainingLevel1
                 || m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalTrainingLevel2).Count() != 0))
                    {
                        MenuDMS = MenuDMS + "<li>";
                        MenuDMS = MenuDMS + "<a href=''#'>Invoice Activity</a>";
                        MenuDMS = MenuDMS + "<ul>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.DirectClaimEntry).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimEntry.aspx'>Direct Claim Entry</a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalMarketingLevel1).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=1&FA=1'>Activity Claim Approval Marketing - Level 1 </a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalMarketingLevel2).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=2&FA=1'>Activity Claim Approval Marketing - Level 2 </a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalServiceLevel1).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=1&FA=2'>Activity Claim Approval Service - Level 1</a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalServiceLevel2).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=2&FA=2'>Activity Claim Approval Service - Level 2</a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSparesLevel2).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=2&FA=3'>Activity Claim Approval Spares - Level 2</a></li>";


                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSalesLevel1).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=1&FA=63'>Activity Claim Approval Sales - Level 1</a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalSalesLevel2).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=2&FA=63'>Activity Claim Approval Sales - Level 2</a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalTrainingLevel1).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=1&FA=64'>Activity Claim Approval Training - Level 1 </a></li>";
                        if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityClaimApprovalTrainingLevel2).Count() != 0))
                            MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ClaimApproval.aspx?PAGE=2&FA=4'>Activity Claim Approval Training - Level 2</a></li>";



                        MenuDMS = MenuDMS + "</ul>";
                        MenuDMS = MenuDMS + "</li>";
                    }
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ActivityInvoiceReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_ActivityInvReports.aspx'>Activity Invoice Report</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ABPPlanModelWise || m.SubModuleMasterID == (short)DMS_MenuSub.MonthlyPlanModelWise
                    || m.SubModuleMasterID == (short)DMS_MenuSub.ABPSparePart || m.SubModuleMasterID == (short)DMS_MenuSub.ABPSparePartRetail).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Planning</a>";
                    MenuDMS = MenuDMS + "<ul>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ABPPlanModelWise).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Planning/ABPModelWise.aspx'>Model Wise ABP</a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.MonthlyPlanModelWise).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Planning/RollingPlanModelWise.aspx'>Model Wise Rolling Plan /a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ABPSparePart).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Planning/ABPSparePart.aspx'>Spare Part ABP Wholesale </a></li>";
                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.ABPSparePartRetail).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/YDMS/Planning/ABPSparePart_Retail.aspx'>Spare Part ABP Retail </a></li>";


                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.LostCustomerData).Count() != 0))
                    MenuDMS = MenuDMS + "<li><a href='/YDMS/YDMS_LostCustomerData.aspx'>Lost Customer Data </a></li>";

                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }

            if ((user.Where(m => m.ModuleMasterID == (short)DMS_MenuMain.BankDepositClearing).Count() != 0))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Account</i></a>";
                MenuDMS = MenuDMS + "<ul>";
                List<PSubModuleAccess> sub = user.Find(m => m.ModuleMasterID == (short)DMS_MenuMain.BankDepositClearing).SubModuleAccess;

                if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingCreate || m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingEditAndConfirm
                   || m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingPostingInSAP || m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingReport).Count() != 0))
                {
                    MenuDMS = MenuDMS + "<li>";
                    MenuDMS = MenuDMS + "<a href=''#'>Bank Deposit Clearing</i></a>";
                    MenuDMS = MenuDMS + "<ul>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingCreate).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_BankDepositClearingCreate.aspx'>Bank Statement Upload</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingEditAndConfirm).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_BankDepositClearingEditAndConfirm.aspx'>Unconfirmed Receipts</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingPostingInSAP).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_BankDepositClearingPostingInSAP.aspx'>Pending for SAP FI Posting</a></li>";

                    if ((sub.Where(m => m.SubModuleMasterID == (short)DMS_MenuSub.BankDepositClearingReport).Count() != 0))
                        MenuDMS = MenuDMS + "<li><a href='/DMS_BankDepositClearingReport.aspx'> Bank Deposit Clearing Report</a></li>";

                    MenuDMS = MenuDMS + "</ul>";
                    MenuDMS = MenuDMS + "</li>";
                }
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";
            }



            if (PSession.User.UserName.ToLower().Contains("it.officer"))
            {
                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>IT</i></a>";
                MenuDMS = MenuDMS + "<ul>";
                MenuDMS = MenuDMS + "<li><a href='/DMS_EquipmentEditForHMR.aspx'>IC Ticket HMR update</a></li>";
                MenuDMS = MenuDMS + "<li><a href='/DMS_DealerBankDetailsList.aspx'>Dealer Bank Details update</a></li>";
                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";


                MenuDMS = MenuDMS + "<li>";
                MenuDMS = MenuDMS + "<a href=''#'>Dealer Support Ticket</i></a>";
                MenuDMS = MenuDMS + "<ul>";
                MenuDMS = MenuDMS + "<li><a href=OpenTicket.aspx><span>Open Tickets</span></a></li>";
                MenuDMS = MenuDMS + "<li><a href=AssignedTickets.aspx><span>Assigned Tickets</span></a></li>";
                MenuDMS = MenuDMS + "<li><a href=InProgress.aspx><span>In Progress</span></a></li>";
                MenuDMS = MenuDMS + "<li><a href=ClosedTickets.aspx><span>Close Tickets</span></a></li>";
                MenuDMS = MenuDMS + "<li><a href=UATDetailsUpdate.aspx><span>UAT</span></a></li>";
                MenuDMS = MenuDMS + "<li><a href=TicketApproval.aspx><span>Tickets Approve</span></a></li>";
                MenuDMS = MenuDMS + "<li><a href=ManageTickets.aspx><span>Tickets Report</span></a></li>";

                MenuDMS = MenuDMS + "</ul>";
                MenuDMS = MenuDMS + "</li>";


            }

            MenuDMS = MenuDMS + "<li> <a href=''#'>Help</a> <ul>";
            MenuDMS = MenuDMS + "<li><a href='/DMS_UserManual.aspx?Manual=1'>DMS Report User Manual</a></li>";
            //MenuDMS = MenuDMS + "<li><a href='DMS_UserManual.aspx?Manual=2'>DMS Dealer User Manual</a></li>";
            MenuDMS = MenuDMS + "<li><a href='http://ajaxapps.ajax-engg.com:8087/SAPBOK/HelpDocContents?Category=SS'>Service Parts Topic [SPT]</a></li>";

            MenuDMS = MenuDMS + "<li><a href='/DMS_ActivityManagementUserManual.aspx?Manual=1'>Activity Managment User Manual</a></li>";

            MenuDMS = MenuDMS + "</ul> </li>";
            MenuDMS = MenuDMS + "<li><a href='/DMS_Message.aspx'>Message</a></li>";
            MenuDMS = MenuDMS + "<li><a href='http://ajaxapps.ajax-engg.com:8087/eCatalogue/SignIn'>e-SPC-Catalogue</a> </li>";

            MenuDMS = MenuDMS + "</ul></nav>";
            bluemenu.InnerHtml = MenuDMS;
        }
    }
}