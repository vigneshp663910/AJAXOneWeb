<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketTSIRMailToVendorReport.aspx.cs" Inherits="DealerManagementSystem.ServiceView.ICTicketTSIRMailToVendorReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>
    <script src="YDMS/YDMS_Scripts.js"></script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table id="txnHistory4:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead" style="height: 10px; background-color: #fbfbfb;"></div>
            </td>
        </tr>
    </table>

    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter : IC Ticket TSIR Mail Send Vendor</div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpand();">
                            <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="pnlFilterContent" runat="server">
                    <%-- <div class="rf-p " id="txnHistory:inputFiltersPanel">--%>
                    <%--    <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">--%>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" Text="TSIR No"></asp:Label>
                                <asp:TextBox ID="txtTSIRNo" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label7" runat="server" Text="TSIR Date From "></asp:Label>
                                <asp:TextBox ID="txtTSIRDateFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTSIRDateFrom" PopupButtonID="txtTSIRDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtTSIRDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label8" runat="server" Text="TSIR Date To"></asp:Label>
                                <asp:TextBox ID="txtTSIRDateTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtTSIRDateTo" PopupButtonID="txtTSIRDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtTSIRDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>

                            <%--  </div>
                                    <div class="row">--%>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblPlant" runat="server" Text="IC Ticket "></asp:Label>
                                <asp:TextBox ID="txtICTicketNumber" runat="server" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label11" runat="server" Text="Machine Serial Number"></asp:Label>
                                <asp:TextBox ID="txtMachineSerialNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 28px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                &nbsp;
                                                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                            </div>
                        </div>
                    </div>
                    <%-- </div>--%>
                    <%--</div>--%>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <span id="txnHistory1:refreshDataGroup">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
                                        <td>IC Ticket TSIR Mail Send Vendor</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="InputButtonRight-contain">
                    </div>
                    <div style="background-color: white" class="TableGrid">
                        <asp:GridView ID="gvICTickets" runat="server" CssClass="TableGrid" AllowPaging="true" PageSize="40" OnPageIndexChanging="gvICTickets_PageIndexChanging" />
                    </div>
                </span>
            </td>
        </tr>
    </table>

</asp:Content>