<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DealerManagementSystem.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        #div1 {
            height: 91.9vh;
            display: flex;
            flex-direction: column;
            overflow: hidden;
            margin-left: 1px;
            background: skyblue;
            background: linear-gradient(to right, #4e97d5, #30526f );
            scroll
        }

        @media screen and (min-device-width: 320px) and (max-device-width: 720px) {

            #div1 {
                height: 93.2vh;
                /* margin-left: 0px;*/
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
                                    <asp:Panel ID="pnlFilterContent" runat="server">
                                        <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                            <div class="rf-p-b " id="txnHistory:inputFiltersPanel_body">
                                                <table class="labeltxt fullWidth">
                                                    <tr>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlZone" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>

                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlApplication" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>

                                                        <td>
                                                            <div class="tbl-row-left">
                                                                <div class="tbl-col-right">
                                                                    <asp:DropDownList ID="ddlFiscalYear" runat="server" CssClass="TextBox" Width="250px" />
                                                                </div>
                                                            </div>
                                                        </td>

                                                     
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="tbl-row-left">

                                                                <div class="tbl-col-right">
                                                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp:TextBoxWatermarkExtender>

                                                                </div>
                                                            </div>
                                                        </td>

                                                        <td>
                                                            <div class="tbl-row-left">

                                                                <div class="tbl-col-right">

                                                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="hasDatepicker input" AutoComplete="Off"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp:TextBoxWatermarkExtender>

                                                                </div>
                                                            </div>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>

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
                    </asp:Panel>

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

                </div>
            </div>
        </div>
    </div>
    <div id="div1">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Ajax/Images/bg01.jpg" Height="100%" />
    </div>

</asp:Content>
