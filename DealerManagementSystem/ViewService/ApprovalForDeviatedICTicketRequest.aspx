<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ApprovalForDeviatedICTicketRequest.aspx.cs" Inherits="DealerManagementSystem.ViewService.ApprovalForDeviatedICTicketRequest" %>


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
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Deviated ICTicket Request</div>
                    <div style="float: right; padding-top: 0px">
                        <%--  <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>--%>
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label1" runat="server" Text="Dealer Code" />
                                <asp:DropDownList ID="ddlDealerCode" runat="server" />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label3" runat="server" Text="IC Ticket "></asp:Label>
                                <asp:TextBox ID="txtICTicketNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label6" runat="server" Text="IC Ticket Deviated For" />
                                <asp:DropDownList ID="ddlICTicketDeviationType" runat="server">
                                    <asp:ListItem Value="1">BaseOn60</asp:ListItem>
                                    <asp:ListItem Value="2">Commissioning</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnRequest" runat="server" Text="Request" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnRequest_Click" OnClientClick="return dateValidation();" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table id="txnHistory2:panelGridid" style="height: 100%; width: 100%">
        <tr>
            <td>
                <div class="boxHead">
                    <div class="logheading">Filter : Deviated ICTicket Report</div>
                    <div style="float: right; padding-top: 0px">
                        <%--  <a href="javascript:collapseExpand();">
                            <img id="Img1" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>--%>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label2" runat="server" Text="Dealer Code"></asp:Label>
                                <asp:DropDownList ID="ddlDealerF" runat="server"  />
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="lblPlant" runat="server" Text="IC Ticket "></asp:Label>
                                <asp:TextBox ID="txtICTicketNumberF" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label4" runat="server" Text="Requested Date From"></asp:Label>
                                <asp:TextBox ID="txtRequestedDateFrom" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRequestedDateFrom" PopupButtonID="txtRequestedDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtRequestedDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12">
                                <asp:Label ID="Label5" runat="server" Text="Requested Date To"></asp:Label>
                                <asp:TextBox ID="txtRequestedDateTo" runat="server" AutoComplete="Off"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRequestedDateTo" PopupButtonID="txtRequestedDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRequestedDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 10px;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                    <tr>
                        <td>
                            <span id="txnHistory1:refreshDataGroup">
                                <div class="boxHead">
                                    <div class="logheading">
                                        <div style="float: left">
                                            <table>
                                                <tr>
                                                    <td>Deviated ICTicket Report</td>
                                                    <td>
                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="label"></asp:Label></td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnArrowLeft" runat="server" ImageUrl="~/Images/ArrowLeft.png" Width="15px" OnClick="ibtnArrowLeft_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ibtnArrowRight" runat="server" ImageUrl="~/Images/ArrowRight.png" Width="15px" OnClick="ibtnArrowRight_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                    <asp:GridView ID="gvICTickets" runat="server" CssClass="TableGrid" AllowPaging="true" PageSize="20" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                                    </asp:GridView>
                                </div>
                            </span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
