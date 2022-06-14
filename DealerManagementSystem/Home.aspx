<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DealerManagementSystem.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script src="https://unpkg.com/masonry-layout@4/dist/masonry.pkgd.min.js"></script>
    <style>
        #div1 {
            /*height: 91.9vh;*/
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
            position: absolute;
            right: 0;
            z-index: 9;
            border: 1px solid #3C4C5B;
        }
        .details {
            position: relative;
        }
        .details .desc {
            clear:both;
        }
        .details #donut_single {
            float: right;
        }
        .details-position {
            position: absolute;
    width: 100%;
    /* bottom: 0; */
    top: 47px;
        }
        /*Home page tiles*/
        /*.container .tblcontrols12 {
            position: relative;
        }
        .container .tblcontrols12 div.cell {

        }
        .container .tblcontrols12 #Div1 {
            float:left;
            width:48%;
        }
        .container .tblcontrols12 #Div1 .modbox, .container .tblcontrols12 #Div2 .modbox  {
            margin-right:11px;
            margin-bottom:15px;
        }
        .container .tblcontrols12 #Div3 .modbox {
            margin-right:11px;
        }
        .container .tblcontrols12 #Div2 {
            float:right;
            width:52%;
        }
        .container .tblcontrols12 #Div2 .modbox, .container .tblcontrols12 #Div5 .modbox, .container .tblcontrols12 #Div6 .modbox, 
        .container .tblcontrols12 #Div7 .modbox {
            margin-left:22px;
        }
        .container .tblcontrols12 #Div1 .dashboardGrid, .container .tblcontrols12 #Div2 .dashboardGrid {
            padding:15px;
            background:#d8d8d8;
        }
        .container .tblcontrols12 #Div3 {
            width:70%;
            float:left;
        }
        .container .tblcontrols12 #Div3 .dashboardGrid img  {
        }
        .container .tblcontrols12 #Div4 {
            clear:both;
        }
        .container .tblcontrols12 #Div4, .container .tblcontrols12 #Div5,.container .tblcontrols12 #Div6 {
            width:33.33%;
            float:left;
        }*/
        /* .container .tblcontrols12 #Div6 {
            width:30%;
            float:right;
        }
            
        .container .tblcontrols12 #Div7 {
            width:30%;
            float:right;
        }*/
        /*.container .tblcontrols12 div.cell table {
            width:100%;
        }
        .container .tblcontrols12 div.cell table tr td {
            
        }
        .modbox .row {
            margin: 0;
        }
        .modbox .wide_thumbnail {
            margin: 0!important;
            padding-bottom: 10px;
        }
        .modbox .portlet-body {
            padding: 0!important;
        }
        .modbox .portlet-body .details span {
            font-size:100px;
        }
        .container .tblcontrols12 div.cell {
            margin-bottom: 15px;
        }
        .cell .modbox .modtitle {
            font-size: 18px;
            text-shadow: 1px 2px 3px #bac4cf;
            font-weight: 500;
        }
        @media screen and (max-width: 767px) {
            .container .tblcontrols12 div.cell {
                width:100%!important;
                float:none!important;
            }
            .container .tblcontrols12 #Div1 .modbox, .container .tblcontrols12 #Div3 .modbox, .container .tblcontrols12 #Div2 .modbox, .container .tblcontrols12 #Div4 .modbox, .container .tblcontrols12 #Div5 .modbox, .container .tblcontrols12 #Div6 .modbox, 
            .container .tblcontrols12 #Div7 .modbox {
                margin:0;
            }
        }
        @media screen and (min-device-width: 320px) and (max-device-width: 720px) {

            #div1 {
                /*height: 93.2vh;
                 margin-left: 0px;*/
        /* }
        }*/
    </style>
    <style>
        .grid-item {
            background: #fff;
            box-shadow: 0 2px 4px rgb(51 51 51 / 20%);
            color: #666666;
            border-radius: 0.25rem 0.25rem 0.25rem 0.25rem;
            margin: 10px;
            padding: 15px;
            width:160px;
            float: left;
        }

            .grid-item:hover {
                background-color: #F0F0F0;
            }

        .tile-size-one {
            width: 160px;
            height: 169px;
        }

        .tile-size-two {
            width: 340px;
            height: 169px;
        }

        .tile-size-three {
            width: 340px;
            height: 360px;
        }

        .tile-size-four {
            width: 640px;
            height: 180px;
        }

        .dashboardGrid img {
            width: 270px !important;
            height: 100px !important;
        }

        #MainContent_tblDashboard .grid {
            /*  display: flex;
    flex-wrap: wrap;*/
            /*-webkit-column-width: 19em;
    -webkit-column-gap: 1rem;*/
        }
    </style>
    <script>
        $(document).ready(function () {
            $('.grid').masonry({
                // options
                itemSelector: '.grid-item',
                columnWidth: 10
            });
        });
    </script>
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
                        <div class="grid">
                            <asp:PlaceHolder ID="ph_usercontrols_1" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_2" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_3" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_4" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_5" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_6" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_7" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_8" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_9" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_10" runat="server"></asp:PlaceHolder>

                            <asp:PlaceHolder ID="ph_usercontrols_11" runat="server"></asp:PlaceHolder>
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

