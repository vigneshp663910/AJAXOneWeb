<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DealerManagementSystem.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        #div1 {
            height: 91.70vh;
            display: flex;
            flex-direction: column;
            overflow: hidden;
            margin-left: 1px;
            background: skyblue;
            background: linear-gradient(to right, #4e97d5, #30526f );
            /* background-image:url('https://localhost:44343/Ajax/Images/bg01.jpg');*/
            /* background-image:url('https://localhost:44343/Ajax/Images/bg05q.png');*/
            /* background-image:url('https://localhost:44343/Ajax/Images/bg04r.png');*/
            background-image: url('https://ajaxapps.ajax-engg.com:1444/Ajax/Images/bg05qr.png');
            /* scroll*/
        }

        .home-search-panel {
            padding: 15px;
        }

            .home-search-panel .tbl-col-right {
                margin-bottom: 10px;
            }

        .tbl-col-right .textBox.form-control {
            height: 35px;
            padding: 0px 7px;
        }

        .tbl-col-right input.form-control {
            height: 35px;
            padding: 0px 10px;
        }

        .home-search-bar {
            position: absolute;
            top: 0;
            right: 0;
            background: #2f516e;
        }

            .home-search-bar:hover {
                background: #336699;
            }

            .home-search-bar a {
                padding: 5px;
            }

        .home-history-body {
            position: relative;
            width: 100%;
            overflow: hidden;
        }

        .navbar-home-content {
            width: 300px;
        }

        .home-search-main {
            transition: width .4s;
            /*width: 300px;*/
            float: right;
            margin-top: 21px;
            margin-bottom: 20px;
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



    <div id="div1">
        <div class="col2">
            <div class="rf-p " id="txnHistory:j_idt1289">
                <div class="rf-p-b home-history-body" id="txnHistory:j_idt1289_body">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Visible="false" Font-Bold="true" Font-Size="24px" />

                    <div class="ErrorRed" id="divError" runat="server" visible="false">
                        <span id="errorMessage" runat="server"></span>
                        <img alt="" src="images/error_red.gif" />
                    </div>
                    <div class="success" id="divSuccess" runat="server" visible="false">
                        <span id="successMessage" runat="server"></span>
                        <img alt="" src="Images/sucess_green.png" />
                    </div>
                    <div id="homeSearchBar" class="home-search-bar">
                        <a class="navbar-home-search" href="javascript:void(0)" onclick="w3_closeHomeSearch()" style="color: #FFFFFF;"><i class="fa fa-fw fa-search font-white" style="color: lightgray"></i></a>
                    </div>
                    <div id="homeSearchMain" class="home-search-main" style="width: 0px;">
                        <asp:Panel ID="pnlFilter" runat="server" CssClass="navbar-home-content">
                            <table id="txnHistory1:panelGridid" style="height: 100%; width: 100%" class="home-history">
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlFilterContent" runat="server" CssClass="home-search-panel">
                                            <div class="rf-p " id="txnHistory:inputFiltersPanel">
                                                <div class="rf-p-b" id="txnHistory:inputFiltersPanel_body">
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlModel" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlApplication" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">
                                                            <asp:DropDownList ID="ddlFiscalYear" runat="server" CssClass="textBox form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">

                                                        <div class="tbl-col-right">
                                                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="hasDatepicker input form-control" AutoComplete="Off"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtDateFrom" WatermarkText="Date From"></asp:TextBoxWatermarkExtender>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="tbl-col-right">

                                                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="hasDatepicker input form-control" AutoComplete="Off"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></asp:CalendarExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtDateTo" WatermarkText="Date To"></asp:TextBoxWatermarkExtender>

                                                        </div>

                                                    </div>
                                                    <div class="col-md-12">

                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="InputButton btn Save" UseSubmitBehavior="true" OnClick="btnSearch_Click" OnClientClick="return dateValidation();" />

                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
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
        <%--<div style="text-align: center; margin-top: -200px; margin-right: 130px">
            <img id="ImgLogo" src="Ajax/Images/AjaxOneB.png" alt="rr" style="height: 45px; width: 130px;"><br />
        </div>--%>
    </div>

</asp:Content>
