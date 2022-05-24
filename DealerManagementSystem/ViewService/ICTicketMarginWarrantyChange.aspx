<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketMarginWarrantyChange.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketMarginWarrantyChange" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Filter : Ticket Margin Warranty Change</div>
                                    <div style="float: right; padding-top: 0px">
                                    </div>
                                </div>
                                <asp:Panel ID="pnlFilterContent" runat="server">
                                    <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                            <table class="labeltxt">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="IC Ticket"></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtICTicket" runat="server" CssClass="input"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <UC:UC_BasicInformation ID="UC_BasicInformation" runat="server"></UC:UC_BasicInformation>
                    <table id="txnHistory2:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Filter : Ticket Margin Warranty Change</div>
                                    <div style="float: right; padding-top: 0px">
                                    </div>
                                </div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="rf-p " id="txnHistory2:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body">
                                            <table class="labeltxt">
                                               <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label19" runat="server" CssClass="label" Text="Is Margin Warranty"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:CheckBox ID="cbIsMarginWarranty" runat="server" Enabled="false" />
                                                            </div>
                                                        </div>
                                                    </td>
                                                 
                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="tbl-col">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="lblMarginRemark" runat="server" CssClass="label" Text="Margin Remark"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtMarginRemark" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox>
                                                              </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSave_Click" OnClientClick="return dateValidation();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
