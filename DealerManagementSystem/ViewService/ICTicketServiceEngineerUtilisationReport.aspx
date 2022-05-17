<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketServiceEngineerUtilisationReport.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketServiceEngineerUtilisationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <%--<script src="Scripts/jquery-latest.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css">
    <link href="YDMS/YDMSStyles.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <asp:Label ID="Label1" runat="server" Text="Dealer" />
                        <asp:DropDownList ID="ddlDealerCode" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Employee Code"></asp:Label>
                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control" BorderColor="Silver"></asp:TextBox>
                    </div>

                    <div class="col-md-2 col-sm-12">
                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Date From "></asp:Label>
                        <asp:TextBox ID="txtICLoginDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtICLoginDateFrom" PopupButtonID="txtICLoginDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtICLoginDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Date To"></asp:Label>
                        <asp:TextBox ID="txtICLoginDateTo" runat="server" CssClass="form-control" BorderColor="Silver" AutoComplete="Off"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtICLoginDateTo" PopupButtonID="txtICLoginDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtICLoginDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <asp:Label ID="Label6" runat="server" Text="Status" />
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <asp:Label ID="Label2" runat="server" Text="Details" />
                        <asp:CheckBox ID="cbDetails" runat="server" CssClass="form-control" BorderColor="Silver" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn Search" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                        <asp:Button ID="btnExportExcel" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="btn Back" UseSubmitBehavior="true" OnClick="btnExportExcel_Click" width="100px"/>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-md-12 Report">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                <div class="col-md-12 Report">
                    <div class="boxHead">
                        <div class="logheading">
                            <div style="float: left">
                                <table>
                                    <tr>
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


                    <asp:GridView ID="gvICTickets" runat="server" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="30" OnPageIndexChanging="gvICTickets_PageIndexChanging">
                        <AlternatingRowStyle BackColor="#ffffff" />
                        <FooterStyle ForeColor="White" />
                        <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                    </asp:GridView>


                </div>
            </fieldset>
        </div>





    </div>












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
                    <div class="logheading">Filter</div>
                    <div style="float: right; padding-top: 0px">
                        <a href="javascript:collapseExpand();">
                            <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                    </div>
                </div>
                <asp:Panel ID="Panel2" runat="server">
                    <div class="container-fluid">
                        <div class="row">
                        </div>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="col2">
        <div class="rf-p " id="txnHistory:j_idt1289">
            <div class="rf-p-b " id="txnHistory:j_idt1289_body">

                <div id="divICTicketManage" runat="server">

                    <table id="txnHistory:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <span id="txnHistory1:refreshDataGroup">
                                    <div class="boxHead">
                                        <div class="logheading">
                                            <div style="float: left">
                                                <table>
                                                    <tr>
                                                        <td>Report</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="InputButtonRight-contain">
                                    </div>
                                    <div style="background-color: white" class="tablefixedWidth" id="tablefixedWidthID">
                                    </div>
                                </span>
                            </td>
                        </tr>
                    </table>

                    <div runat="server" id="divUtilisationReportDetails" visible="false">
                        <table id="txnHistory11:panelGridid" style="height: 100%; width: 100%">
                            <tr>
                                <td>
                                    <span id="txnHistory12:refreshDataGroup">
                                        <div style="background-color: white" class="tablefixedWidth">

                                            <asp:Label ID="lblURD1Codunt" runat="server" Text="" CssClass="label" Width="100%" />
                                            <asp:Button ID="btnExportURD1" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportURD1_Click" />
                                            <asp:GridView ID="gvURD1" runat="server" CssClass="TableGrid" AllowPaging="true" PageSize="30" OnPageIndexChanging="gvURD1_PageIndexChanging">
                                            </asp:GridView>
                                            <asp:Label ID="lblURD2Codunt" runat="server" Text="" CssClass="label" Width="100%" />
                                            <asp:Button ID="btnExportURD2" runat="server" Text="<%$ Resources:Resource, btnExportExcel %>" CssClass="InputButtonRight" UseSubmitBehavior="true" OnClick="btnExportURD2_Click" />
                                            <asp:GridView ID="gvURD2" runat="server" CssClass="TableGrid" AllowPaging="true" PageSize="30" OnPageIndexChanging="gvURD2_PageIndexChanging">
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
    </div>
</asp:Content>