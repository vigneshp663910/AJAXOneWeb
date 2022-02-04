<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="ICTicketReachedDateChange.aspx.cs" Inherits="DealerManagementSystem.ViewService.ICTicketReachedDateChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/DMS_ICTicketBasicInformation.ascx" TagPrefix="UC" TagName="UC_BasicInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" />
                    <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                        <tr>
                            <td>
                                <div class="boxHead">
                                    <div class="logheading">Filter : Ticket Reached Date Change</div>
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
                                    <div class="logheading">Filter : Ticket Reached Date Change</div>
                                    <div style="float: right; padding-top: 0px">
                                    </div>
                                </div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="rf-p " id="txnHistory2:inputFiltersPanel">
                                        <div class="rf-p-b " id="txnHistory2:inputFiltersPanel_body">
                                            <table class="labeltxt">
                                                <tr>
                                                    <td>
                                                        <div class="tbl-row-right">
                                                            <div class="tbl-col-left">
                                                                <asp:Label ID="Label10" runat="server" CssClass="label" Text="Requested Date"></asp:Label>
                                                            </div>
                                                            <div class="tbl-col-right">
                                                                <asp:TextBox ID="txtRequestedDate" runat="server" CssClass="input" AutoComplete="SP" onkeyup="return removeText('MainContent_txtRequestedDate');" Enabled="false"></asp:TextBox>
                                                                <asp:CalendarExtender ID="ceRequestedDate" runat="server" TargetControlID="txtRequestedDate" PopupButtonID="txtRequestedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtRequestedDate" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                                <asp:DropDownList ID="ddlRequestedHH" runat="server" CssClass="TextBox" Width="60px">
                                                                    <asp:ListItem Value="-1">HH</asp:ListItem>
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlRequestedMM" runat="server" CssClass="TextBox" Width="65px">
                                                                    <asp:ListItem Value="0">MM</asp:ListItem>
                                                                    <asp:ListItem>00</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>35</asp:ListItem>
                                                                    <asp:ListItem>40</asp:ListItem>
                                                                    <asp:ListItem>45</asp:ListItem>
                                                                    <asp:ListItem>50</asp:ListItem>
                                                                    <asp:ListItem>55</asp:ListItem>
                                                                </asp:DropDownList>
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
