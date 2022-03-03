<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="MTTR_Report.aspx.cs" Inherits="DealerManagementSystem.ViewService.MTTR_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function collapseExpand(obj) {
            var gvObject = document.getElementById("MainContent_pnlFilterContent");
            var imageID = document.getElementById("MainContent_imageID");

            if (gvObject.style.display == "none") {
                gvObject.style.display = "inline";
                imageID.src = "Images/grid_collapse.png";
            }
            else {
                gvObject.style.display = "none";
                imageID.src = "Images/grid_expand.png";
            }
        }
        $(document).ready(function () {
            var tablefixedWidthID = document.getElementById('tablefixedWidthID');
            var $width = $(window).width() - 28;
            tablefixedWidthID.style.width = $width + "px";
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid" class="report-container" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Filter : MTTR Report </div>
                                    <div style="float: right; padding-top: 0px">
                                        <a href="javascript:collapseExpand();">
                                            <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlFilterContent" CssClass="report-panel" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                            <div class="row">
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer Code"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="TextBox form-control" />
                                                    </div>

                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer Code"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="input form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="lblPlant" runat="server" CssClass="label" Text="IC Service Ticket :"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtICServiceTicket" runat="server" CssClass="input form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="IC Login Date From :"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="hasDatepicker input form-control" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                    </div>

                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="IC Login Date To"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">

                                                        <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="hasDatepicker input form-control" AutoComplete="Off"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Status2" Visible="false"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlStatus2" runat="server" CssClass="TextBox form-control" Visible="false">
                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                                            <asp:ListItem Value="Srv. Restored">Srv. Restored</asp:ListItem>
                                                            <asp:ListItem Value="Rejected By Dea">Rejected By Dea</asp:ListItem>
                                                            <asp:ListItem Value="Forced Close">Forced Close</asp:ListItem>
                                                            <asp:ListItem Value="Srv. Eng. Reached">Srv. Eng. Reached</asp:ListItem>
                                                            <asp:ListItem Value="Rejected By Dealer">Rejected By Dealer</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="PSR Status"></asp:Label>
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:DropDownList ID="ddlPsrStatus" runat="server" CssClass="TextBox form-control">
                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="OPEN">Open</asp:ListItem>
                                                            <asp:ListItem Value="INPROCESS">In Process</asp:ListItem>
                                                            <asp:ListItem Value="COMPLETED">Completed</asp:ListItem>
                                                            <asp:ListItem Value="DECLINED">Declined</asp:ListItem>
                                                            <asp:ListItem Value="CLOSED">Closed</asp:ListItem>
                                                            <asp:ListItem Value="FORCED_CLOSED">Forced Closed</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4 col-sm-6">
                                                    <div class="tbl-col-left">
                                                        <asp:RadioButton ID="rbWithOutText" runat="server" Text="With Out Text" GroupName="s" OnCheckedChanged="rbWithOutText_CheckedChanged" AutoPostBack="true" Checked="true" />
                                                    </div>
                                                    <div class="tbl-col-right">
                                                        <asp:RadioButton ID="rbWithText" runat="server" Text="With Text" GroupName="s" OnCheckedChanged="rbWithOutText_CheckedChanged" AutoPostBack="true" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="tbl-btn excelBtn">
                                                        <div class="tbl-col-btn">
                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                        </div>
                                                        <div class="tbl-col-btn">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <span id="txnHistory1:refreshDataGroup">
                                    <div class="boxHead">
                                        <div class="logheading">
                                            <div style="float: left">
                                                <table>
                                                    <tr>
                                                        <td>MTTR Report</td>
                                                        <td>
                                                            <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" /></td>
                                                        <td>
                                                            <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="InputButtonRight-contain">
                                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                                    </div>
                                    <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">

                                        <asp:GridView ID="gvICTickets" runat="server" AutoGenerateColumns="false" CssClass="TableGrid" Width="4000px" AllowPaging="true" PageSize="20"
                                            OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="IC Tkt No." HeaderStyle-Width="62px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "f_ic_ticket_id")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Login Dt(IC)" HeaderStyle-Width="92px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "f_call_login_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ser. Req. Date" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ser_req_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SE Reached Dt">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "ser_rec_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SE Restore Dt" HeaderStyle-Width="76px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "ser_res_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Cust.Conf.Dt">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="MTTR1-Act Resp(Days)" HeaderStyle-Width="77px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "MTTR1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MTTR2-Actual Restored(Day)" HeaderStyle-Width="79px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "MTTR2")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status1 (Op. Based)" HeaderStyle-Width="147px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "status1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PSR Status" HeaderStyle-Width="147px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPsrStatus" Text='<%# DataBinder.Eval(Container.DataItem, "PsrStatus")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Status2 (Dt Based)" HeaderStyle-Width="147px">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblstatus2" Text='<%# DataBinder.Eval(Container.DataItem, "status2")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="IC Tkt Dt">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblf_call_login_date" Text='<%# DataBinder.Eval(Container.DataItem, "f_call_login_date")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Cust. ID" HeaderStyle-Width="100px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_cust_id" Text='<%# DataBinder.Eval(Container.DataItem, "f_cust_id")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbld_customer_name" Text='<%# DataBinder.Eval(Container.DataItem, "d_customer_name")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Code">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldealer_code" Text='<%# DataBinder.Eval(Container.DataItem, "dealer_code")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldealer_name" Text='<%# DataBinder.Eval(Container.DataItem, "dealer_name")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Present M/C City">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_city" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_city")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M/C Loc Dist(IC)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_dist" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_dist")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M/C Loc State(IC)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_state" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_state")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M/C Loc Region(IC)" HeaderStyle-Width="156px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_region" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_region")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Problem Reported">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproblem_reported" Text='<%# DataBinder.Eval(Container.DataItem, "problem_reported")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model Des">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmc_desc" Text='<%# DataBinder.Eval(Container.DataItem, "mc_desc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial No.">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmc_slno" Text='<%# DataBinder.Eval(Container.DataItem, "mc_slno")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Ser Engg Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_name" Text='<%# DataBinder.Eval(Container.DataItem, "ser_name")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service charge Code">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcode" Text='<%# DataBinder.Eval(Container.DataItem, "code")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service charge des">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_part_id" Text='<%# DataBinder.Eval(Container.DataItem, "f_part_id")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldescription1" Text='<%# DataBinder.Eval(Container.DataItem, "description1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HMR">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_hmr_desc" Text='<%# DataBinder.Eval(Container.DataItem, "r_counter_end")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prior Desc. (IC)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_priority_class_desc" Text='<%# DataBinder.Eval(Container.DataItem, "r_priority_class_desc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prior Desc. (Srv. Order)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_priority_desc" Text='<%# DataBinder.Eval(Container.DataItem, "r_priority_desc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--      <asp:TemplateField HeaderText="Breakdown Reason">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "breakdown_reason")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Breakdown Details">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_comments" Text='<%# DataBinder.Eval(Container.DataItem, "r_comments")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField HeaderText="Internal Notes1">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Cust Sat Level">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="Total Mandays">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ser Ord No." HeaderStyle-Width="100px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_ord_no" Text='<%# DataBinder.Eval(Container.DataItem, "ser_ord_no")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--   <asp:TemplateField HeaderText="Ser. Conf. No.">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField HeaderText="Bill Doc No.">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Application">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_application" Text='<%# DataBinder.Eval(Container.DataItem, "r_application")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="contatc no.">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "contact_person_pre")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact person">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcontact_person" Text='<%# DataBinder.Eval(Container.DataItem, "contact_person")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PSR No">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblp_sr_id" Text='<%# DataBinder.Eval(Container.DataItem, "p_sr_id")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TSIR No">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_tsir_no" Text='<%# DataBinder.Eval(Container.DataItem, "r_tsir_no")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:GridView ID="gvICTicketsWithText" runat="server" DataKeyNames="f_ic_ticket_id" AutoGenerateColumns="false" CssClass="TableGrid" Width="4000px" AllowPaging="true" PageSize="20" OnRowDataBound="gvICTicketsWithText_RowDataBound" OnPageIndexChanging="gvICTickets_PageIndexChanging" Visible="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="IC Tkt No." HeaderStyle-Width="62px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_ic_ticket_id" Text='<%# DataBinder.Eval(Container.DataItem, "f_ic_ticket_id")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Call Login Dt(IC)" HeaderStyle-Width="92px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_call_login_date1" Text='<%# DataBinder.Eval(Container.DataItem, "f_call_login_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ser. Req. Date" HeaderStyle-Width="75px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_req_date" Text='<%# DataBinder.Eval(Container.DataItem, "ser_req_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SE Reached Dt">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_rec_date" Text='<%# DataBinder.Eval(Container.DataItem, "ser_rec_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SE Restore Dt" HeaderStyle-Width="76px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_res_date" Text='<%# DataBinder.Eval(Container.DataItem, "ser_res_date","{0:d}")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Cust.Conf.Dt">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="MTTR1-Act Resp(Days)" HeaderStyle-Width="77px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR1" Text='<%# DataBinder.Eval(Container.DataItem, "MTTR1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MTTR2-Actual Restored(Day)" HeaderStyle-Width="79px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMTTR2" Text='<%# DataBinder.Eval(Container.DataItem, "MTTR2")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status1 (Op. Based)" HeaderStyle-Width="147px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus1" Text='<%# DataBinder.Eval(Container.DataItem, "status1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PSR Status" HeaderStyle-Width="147px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPsrStatus" Text='<%# DataBinder.Eval(Container.DataItem, "PsrStatus")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Status2 (Dt Based)" HeaderStyle-Width="147px">
                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblstatus2" Text='<%# DataBinder.Eval(Container.DataItem, "status2")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                                                <%-- <asp:TemplateField HeaderText="IC Tkt Dt">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblf_call_login_date" Text='<%# DataBinder.Eval(Container.DataItem, "f_call_login_date")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Cust. ID" HeaderStyle-Width="100px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_cust_id" Text='<%# DataBinder.Eval(Container.DataItem, "f_cust_id")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbld_customer_name" Text='<%# DataBinder.Eval(Container.DataItem, "d_customer_name")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Code">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldealer_code" Text='<%# DataBinder.Eval(Container.DataItem, "dealer_code")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dealer Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldealer_name" Text='<%# DataBinder.Eval(Container.DataItem, "dealer_name")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Present M/C City">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_city" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_city")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M/C Loc Dist(IC)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_dist" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_dist")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M/C Loc State(IC)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_state" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_state")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="M/C Loc Region(IC)" HeaderStyle-Width="156px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpresent_mc_region" Text='<%# DataBinder.Eval(Container.DataItem, "present_mc_region")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Problem Reported">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblproblem_reported" Text='<%# DataBinder.Eval(Container.DataItem, "problem_reported")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model Des">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmc_desc" Text='<%# DataBinder.Eval(Container.DataItem, "mc_desc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Serial No.">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmc_slno" Text='<%# DataBinder.Eval(Container.DataItem, "mc_slno")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ser Engg Name">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_name" Text='<%# DataBinder.Eval(Container.DataItem, "ser_name")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service charge Code">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcode" Text='<%# DataBinder.Eval(Container.DataItem, "code")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service charge des">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblf_part_id" Text='<%# DataBinder.Eval(Container.DataItem, "f_part_id")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldescription1" Text='<%# DataBinder.Eval(Container.DataItem, "description1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HMR">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_hmr_desc" Text='<%# DataBinder.Eval(Container.DataItem, "r_counter_end")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prior Desc. (IC)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_priority_class_desc" Text='<%# DataBinder.Eval(Container.DataItem, "r_priority_class_desc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prior Desc. (Srv. Order)">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_priority_desc" Text='<%# DataBinder.Eval(Container.DataItem, "r_priority_desc")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField HeaderText="Breakdown Reason">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "breakdown_reason")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Breakdown Details">
                                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblr_comments" Text='<%# DataBinder.Eval(Container.DataItem, "r_comments")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField HeaderText="Internal Notes1">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Cust Sat Level">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                                <%-- 
                                                <asp:TemplateField HeaderText="Ser Ord No." HeaderStyle-Width="100px">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblser_ord_no" Text='<%# DataBinder.Eval(Container.DataItem, "ser_ord_no")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField> --%>

                                                <%--   <asp:TemplateField HeaderText="Ser. Conf. No.">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField HeaderText="Bill Doc No.">
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Application">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_application" Text='<%# DataBinder.Eval(Container.DataItem, "r_application")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="contatc no.">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedOn" Text='<%# DataBinder.Eval(Container.DataItem, "contact_person_pre")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact person">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcontact_person" Text='<%# DataBinder.Eval(Container.DataItem, "contact_person")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PSR No">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblp_sr_id" Text='<%# DataBinder.Eval(Container.DataItem, "p_sr_id")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TSIR No">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblr_tsir_no" Text='<%# DataBinder.Eval(Container.DataItem, "r_tsir_no")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cust Sat Level">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustomerSatisfactionLevel" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerSatisfactionLevel")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Mandays">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalMandays" Text='<%# DataBinder.Eval(Container.DataItem, "TotalMandays")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Breakdown">
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:GridView ID="gvBreakdown" runat="server" AutoGenerateColumns="false" ShowHeader="False" BorderStyle="None">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemStyle BorderStyle="None" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBreakdownNoteType" Text='<%# DataBinder.Eval(Container.DataItem, "BreakdownNoteType")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle BorderStyle="None" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBreakdownReason" Text='<%# DataBinder.Eval(Container.DataItem, "BreakdownReason")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemStyle BorderStyle="None" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBreakdownDetails" Text='<%# DataBinder.Eval(Container.DataItem, "BreakdownDetails")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
