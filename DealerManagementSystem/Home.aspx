<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DealerManagementSystem.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b " id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" Font-Bold="true" Font-Size="24px" />

                    <div class="ErrorRed" id="divError" runat="server" visible="false">
                        <span id="errorMessage" runat="server"></span>
                        <img alt="" src="images/error_red.gif" />
                    </div>
                    <div class="success" id="divSuccess" runat="server" visible="false">
                        <span id="successMessage" runat="server"></span>
                        <img alt="" src="Images/sucess_green.png" />
                    </div>
                    <asp:Panel ID="pnlFilter" runat="server">

                        <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%">
                            <tr>
                                <td>
                                    <div class="boxHead">
                                        <div class="logheading">Filter </div>
                                        <div style="float: right; padding-top: 0px">
                                            <a href="javascript:collapseExpand();">
                                                <img id="imageID" runat="server" alt="Click to show/hide orders" border="0" src="Images/grid_collapse.png" height="22" width="22" /></a>
                                        </div>
                                    </div>
                                    <asp:Panel ID="pnlFilterContent" runat="server">
                                        <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                            <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                                <table class="labeltxt fullWidth">
                                                    <tr>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Dealer"></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Date From "></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">
                                                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDateFrom" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-left">
                                                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text=" Date To"></asp:Label>
                                                                </div>
                                                                <div class="tbl-col-right">

                                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateTo" WatermarkText="DD/MM/YYYY"></asp:TextBoxWatermarkExtender>

                                                                </div>
                                                            </div>

                                                        </td>


                                                        <td>
                                                            <%--  <div class="tbl-btn excelBtn">--%>
                                                            <%--    <div class="tbl-col-btn">--%>
                                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />
                                                            <%--  </div>
                                                            <div class="tbl-col-btn">
                                                            </div>--%>
                                                            <%-- </div>--%>
                                                        </td>
                                                </table>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>


                    <asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick">
                    </asp:Timer>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"
                        ViewStateMode="Enabled">
                        <ContentTemplate>
                            <!-- Placeholder for dashboard -->
                            <div runat="server" id="tblDashboard" class="container">
                                <div class="tblcontrols12">
                                    <div class="cell" id="Div1">
                                        <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div2">
                                        <asp:PlaceHolder ID="ph_usercontrols_2" runat="server"></asp:PlaceHolder>
                                    </div>

                                    <div class="cell" id="Div3">
                                        <asp:PlaceHolder ID="ph_usercontrols_3" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div4">
                                        <asp:PlaceHolder ID="ph_usercontrols_4" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div5">
                                        <asp:PlaceHolder ID="ph_usercontrols_5" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div6">
                                        <asp:PlaceHolder ID="ph_usercontrols_6" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div7">
                                        <asp:PlaceHolder ID="ph_usercontrols_7" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div8">
                                        <asp:PlaceHolder ID="ph_usercontrols_8" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div9">
                                        <asp:PlaceHolder ID="ph_usercontrols_9" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div10">
                                        <asp:PlaceHolder ID="ph_usercontrols_10" runat="server"></asp:PlaceHolder>
                                    </div>
                                    <div class="cell" id="Div11">
                                        <asp:PlaceHolder ID="ph_usercontrols_11" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
