<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="Pre_Sales_Dashboard.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Pre_Sales_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*.thumbnail {

            padding-bottom: 11px;
            padding-top:11px;
        }

        .wide_thumbnail
        {
             padding-left: 8px;
             padding-right: 8px;

        }*/
        .portlet.box.green {
            border: 1px solid #5cd1db;
            /*   border: 1px solid #483D8B;*/
            border-top: 0;
            padding-bottom: 11px;
            padding-top: 11px;
            margin-left: 1px;
        }

            .portlet.box.green > .portlet-title {
                /* background-color: #1679dd;*/
                /*  background-color:burlywood;*/
                /*   background-color:cadetblue;*/
                /*background-color:darkcyan;*/
                /*     background-color:darkgray;*/
                /* background-color:lightsteelblue;*/
                /* background-color:mediumseagreen;*/

                background-color: mediumturquoise;
                /* background-color: #32c5d2;*/
                /*    background-color: #00CED1;*/
                /* background-color: #0000cc;*/
                /*      background-color: #000099;*/
            }

        .portlet.box > .portlet-title {
            border-bottom: 0;
            padding: 0 10px;
            margin-bottom: 0;
            color: #fff;
        }

        .portlet > .portlet-title > .actions > .btn, .portlet > .portlet-title > .actions > .btn.btn-sm, .portlet > .portlet-title > .actions > .btn-group > .btn, .portlet > .portlet-title > .actions > .btn-group > .btn.btn-sm {
            padding: 4px 10px;
            font-size: 13px;
            line-height: 1.5;
        }


        .btn:not(.md-skip):not(.bs-select-all):not(.bs-deselect-all).btn-sm {
            font-size: 11px;
            /*padding: 6px 18px 6px 18px;*/
            /* padding: 3px 9px 3px 9px;*/
        }


        .btn.btn-outline.red:hover, .btn.btn-outline.red:active, .btn.btn-outline.red:active:hover, .btn.btn-outline.red:active:focus, .btn.btn-outline.red:focus, .btn.btn-outline.red.active {
            border-color: #e7505a;
            color: #fff;
            background-color: #e7505a;
        }


        .btn-group.btn-group-devided > .btn {
            margin-right: 5px;
        }



        .btn.btn-outline.red {
            border-color: #e7505a;
            color: #e7505a;
            background: none;
        }


        .btn .caret, .btn-group > .btn:first-child {
            margin-left: 0;
        }



        .btn-group-vertical > .btn.active, .btn-group-vertical > .btn:active, .btn-group-vertical > .btn:focus, .btn-group-vertical > .btn:hover, .btn-group > .btn.active, .btn-group > .btn:active, .btn-group > .btn:focus, .btn-group > .btn:hover {
            z-index: 2;
        }


        .btn.active, .btn:active {
            outline: 0;
            -webkit-box-shadow: inset 0 3px 5px rgb(0 0 0 / 13%);
            box-shadow: inset 0 3px 5px rgb(0 0 0 / 13%);
        }



        .btn, .btn-danger.active, .btn-danger:active, .btn-default.active, .btn-default:active, .btn-info.active, .btn-info:active, .btn-primary.active, .btn-primary:active, .btn-warning.active, .btn-warning:active, .btn.active, .btn:active, .dropdown-menu > .disabled > a:focus, .dropdown-menu > .disabled > a:hover, .form-control, .navbar-toggle, .open > .btn-danger.dropdown-toggle, .open > .btn-default.dropdown-toggle, .open > .btn-info.dropdown-toggle, .open > .btn-primary.dropdown-toggle, .open > .btn-warning.dropdown-toggle {
            background-image: none;
        }

        .btn-circle {
            border-radius: 25px !important;
            overflow: hidden;
        }

        .btn {
            outline: none !important;
            min-block-size: 30px;
        }

        .btn-circle {
            border-radius: 25px !important;
            overflow: hidden;
        }

        btn-group-sm > .btn, .btn-sm {
            padding: 5px 1px;
            font-size: 12px;
            line-height: 1.5;
            border-radius: 3px;
        }



        .btn {
            display: inline-block;
            margin-bottom: 0;
            font-weight: 400;
            text-align: center;
            vertical-align: middle;
            touch-action: manipulation;
            cursor: pointer;
            border: 1px solid transparent;
            white-space: nowrap;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857;
            border-radius: 4px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }


        label {
            font-weight: normal;
        }

        label {
            font-weight: normal;
        }

        /* Chart style*/
        .funnel-chart {
            padding-top: 15px;
        }

            .funnel-chart .chart-label {
                font-size: 15px;
            }

        .visual > i {
            margin-left: -35px;
            font-size: 100px;
            line-height: 100px;
            color: #fff;
            opacity: .1;
            width: 80px;
            height: 50px;
            display: block;
            float: left;
            padding-top: 10px;
            padding-left: 15px;
            margin-bottom: 15px;
        }
    </style>
    <style>
        #funnel-container {
            color: #fff;
            width: 380px !important;
            height: 600px !important;
            margin: 50px auto;
        }

        h1 {
            margin: 150px auto 30px auto;
            text-align: center;
        }

        #funnel {
            width: auto !important;
            height: 600px !important;
            margin: 0 auto;
            text-align: center;
        }

        .funnel-label {
            left: auto !important;
        }

        @media screen and (max-width: 540px) {
            #funnel-container {
                width: 380px !important;
            }
        }
    </style>


    <style>
        .TimeAction {
            width: 100%;
            overflow: hidden;
            margin: 15px 15px 0;
        }
            /* The container */
            .TimeAction .container {
                display: block;
                position: relative;
                /* padding-left: 35px; */
                /* margin-bottom: 12px; */
                cursor: pointer;
                font-size: 15px;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                float: left;
                width: 117px;
                border: 1px solid red;
                margin: 0 15px 10px;
                padding: 3px 5px 5px 40px;
                border-radius: 50px;
            }

                .TimeAction .container:hover {
                    background-color: #d91e18;
                    color: white;
                }
                /* Hide the browser's default radio button */
                .TimeAction .container input {
                    position: absolute;
                    opacity: 0;
                    cursor: pointer;
                }

                /* Create a custom radio button */
                .TimeAction .container .checkmark {
                    position: absolute;
                    top: 5px;
                    left: 8px;
                    height: 20px;
                    width: 20px;
                    background-color: #eee;
                    border-radius: 50%;
                }

                /* On mouse-over, add a grey background color */
                .TimeAction .container:hover input ~ .checkmark {
                    background-color: #ccc;
                }

                /* When the radio button is checked, add a blue background */
                .TimeAction .container input:checked ~ .checkmark {
                    background-color: #2196F3;
                }

                /* Create the indicator (the dot/circle - hidden when not checked) */
                .TimeAction .container .checkmark:after {
                    content: "";
                    position: absolute;
                    display: none;
                }

                /* Show the indicator (dot/circle) when checked */
                .TimeAction .container input:checked ~ .checkmark:after {
                    display: block;
                }

                /* Style the indicator (dot/circle) */
                .TimeAction .container .checkmark:after {
                    top: 7px;
                    left: 6px;
                    width: 8px;
                    height: 8px;
                    border-radius: 50%;
                    background: white;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html>
    <head>
        <title></title>
        <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    </head>


    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" /> </asp:ScriptManager>--%>
    <body>
        <div class="lead-static">
            <div class="TimeAction">
                <%--  <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px 2px 5px; font-size: 11px;">
                    
                    <asp:RadioButton ID="rbEnquiryToday" runat="server" GroupName="E" OnCheckedChanged="rbStatusE_CheckedChanged" AutoPostBack="true" Checked="true" />
                    <span>Today</span>
                </label>
                <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                  
                    <asp:RadioButton ID="rbEnquiryWeek" runat="server" GroupName="E" OnCheckedChanged="rbStatusE_CheckedChanged" AutoPostBack="true" />
                    <span>Week</span>
                </label>
                <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                    
                    <asp:RadioButton ID="rbEnquiryMonth" runat="server" GroupName="E" OnCheckedChanged="rbStatusE_CheckedChanged" AutoPostBack="true" />
                    <span>Month</span>
                </label>
                <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                   
                    <asp:RadioButton ID="rbEnquiryYear" runat="server" GroupName="E" OnCheckedChanged="rbStatusE_CheckedChanged" AutoPostBack="true" />
                    <span>Year</span>
                </label> --%>
                <label class="container">
                    Today
                    <asp:RadioButton ID="rbToday" runat="server" GroupName="s" OnCheckedChanged="rbStatus_CheckedChanged" AutoPostBack="true" />
                    <span class="checkmark"></span>
                </label>
                <label class="container">
                    Week
                    <asp:RadioButton ID="rbWeek" runat="server" GroupName="s" OnCheckedChanged="rbStatus_CheckedChanged" AutoPostBack="true" />
                    <span class="checkmark"></span>
                </label>
                <label class="container">
                    Month
                    <asp:RadioButton ID="rbMonth" runat="server" GroupName="s" OnCheckedChanged="rbStatus_CheckedChanged" AutoPostBack="true"  Checked="true"  />
                    <span class="checkmark"></span>
                </label>
                <label class="container">
                    Year
                   <asp:RadioButton ID="rbYear" runat="server" GroupName="s" OnCheckedChanged="rbStatus_CheckedChanged" AutoPostBack="true" />
                    <span class="checkmark"></span>
                </label>

                <div class="col-md-12">
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">Dealer Employee</label>
                        <asp:DropDownList ID="ddlDealerEmployee" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDealerEmployee_SelectedIndexChanged" />
                    </div>
                </div>
            </div>


            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption" style="font-size: 25px;">
                        Enquiry Statistics     
                       
                    </div>
                </div>

                <div class="portlet-body" style="padding: 5px;">
                    <div id="divEnquiryStat1">
                        <div id="divEnquiryStatistics" class="row no-margin" style="font-size: medium; text-align: right;">
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                                <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <asp:Label ID="lblEnquiryOpen" runat="server" Text="0"></asp:Label>
                                        <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                        <div class="desc">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Style="color: white;" OnClick="lbEnquiryActions_Click">Open</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: darkolivegreen">
                                <div class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('Assigned');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                        <asp:Label ID="lblEnquiryConvertedToLead" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Style="color: white;" OnClick="lbEnquiryActions_Click">Converted To Lead</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #d91e18;">
                                <div class="dashboard-stat dashboard-stat-v2 red-thunderbird" href="javascript:void(0);" onclick="VisitMyEnquiries('Lost');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <asp:Label ID="lblEnquiryRejected" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="LinkButton5" runat="server" Style="color: white;" OnClick="lbEnquiryActions_Click">Rejected</asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption" style="font-size: 25px;">
                        Lead Statistics             
                    
                    </div>
                </div>

                <div class="portlet-body" style="padding: 5px;">
                    <div id="divEnquiryStat">
                        <div id="divLeadStatistics" class="row no-margin" style="font-size: medium; text-align: right;">
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                                <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <asp:Label ID="lblOpen" runat="server" Text="0"></asp:Label>
                                        <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                        <div class="desc">
                                            <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Open</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #3598dc;">
                                <div class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('Assigned');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                        <asp:Label ID="lblAssigned" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="lbtnAssigned" runat="server" Style="color: white;" OnClick="lbActions_Click">Assigned</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #32c5d2;">
                                <div class="dashboard-stat dashboard-stat-v2 green" href="javascript:void(0);" onclick="VisitMyEnquiries('Prospect');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>

                                        <asp:Label ID="lblQuotation" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="lbtnProspect" runat="server" Style="color: white;" OnClick="lbActions_Click">Quotation</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #26c281;">
                                <div class="dashboard-stat dashboard-stat-v2 green-jungle" href="javascript:void(0);" onclick="VisitMyEnquiries('Won');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                        <asp:Label ID="lblWon" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="lbtnWon" runat="server" Style="color: white;" OnClick="lbActions_Click">Won</asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #d91e18;">
                                <div class="dashboard-stat dashboard-stat-v2 red-thunderbird" href="javascript:void(0);" onclick="VisitMyEnquiries('Lost');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <asp:Label ID="lblLost" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="lbtnLost" runat="server" Style="color: white;" OnClick="lbActions_Click">Lost</asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 1px; background-color: #d05454;">
                                <div class="dashboard-stat dashboard-stat-v2 red-soft" href="javascript:void(0);" onclick="VisitMyEnquiries('Cancelled');">
                                    <div class="visual"><i class="fa fa-ticket"></i></div>
                                    <div class="details" style="color: white;">
                                        <asp:Label ID="lblCancelled" runat="server" Text="0"></asp:Label>
                                        <div class="desc">
                                            <asp:LinkButton ID="lbtnCancelled" runat="server" Style="color: white;" OnClick="lbActions_Click">Cancelled</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- <div class="portlet box green" style="display:none">--%>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption" style="font-size: 25px;">
                        Pre-Sales Statistics Funnel   
                   <%-- <div class="actions" style="float: right;">
                        <div class="btn-group btn-group-devided">
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                                <asp:RadioButton ID="rbWeekF" runat="server" GroupName="Funnel" AutoPostBack="true" Checked="true"></asp:RadioButton>
                                Week 
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                                <asp:RadioButton ID="rbMonthF" runat="server" GroupName="Funnel" AutoPostBack="true" />

                                Month
                           
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                                <asp:RadioButton ID="rbYearF" runat="server" GroupName="Funnel" AutoPostBack="true"></asp:RadioButton>
                                Year 
                            </label>
                        </div>
                    </div>--%>
                    </div>
                </div>
                <div class="portlet-body" style="padding: 5px; text-align: center;">
                    <%--<div id="divLeadFunnel" style="width: 300px; height: 300px; margin: 0 auto;" class="row no-margin">
                        <svg id="d3-funnel-chart-0" width="300" height="300">
                            <defs>
                                <linearGradient id="d3-funnel-chart-0-gradient-0">
                                    <stop offset="0%" style="stop-color: #6c5e86"></stop>
                                    <stop offset="40%" style="stop-color: #8775A7"></stop>
                                    <stop offset="60%" style="stop-color: #8775A7"></stop>
                                    <stop offset="100%" style="stop-color: #6c5e86"></stop>
                                </linearGradient>
                                <linearGradient id="d3-funnel-chart-0-gradient-1">
                                    <stop offset="0%" style="stop-color: #289ea8"></stop>
                                    <stop offset="40%" style="stop-color: #32C5D2"></stop>
                                    <stop offset="60%" style="stop-color: #32C5D2"></stop>
                                    <stop offset="100%" style="stop-color: #289ea8"></stop>
                                </linearGradient>
                                <linearGradient id="d3-funnel-chart-0-gradient-2">
                                    <stop offset="0%" style="stop-color: #1e9b67"></stop>
                                    <stop offset="40%" style="stop-color: #26C281"></stop>
                                    <stop offset="60%" style="stop-color: #26C281"></stop>
                                    <stop offset="100%" style="stop-color: #1e9b67"></stop>
                                </linearGradient>
                            </defs><path fill="#514664" d="M0,10 Q150,30 300,10 M300,10 Q150,0 0,10"></path><g><path d="M0,10 Q150,20 300,10 L250,103.33333333333333 M250,103.33333333333333 Q150,123.33333333333333 50,103.33333333333333 L0,10" fill="url(#d3-funnel-chart-0-gradient-0)"></path>
                                <text x="150" y="66.66666666666666" fill="#fff" font-size="14px" text-anchor="middle" dominant-baseline="middle" pointer-events="none">
                                    <tspan x="150" dy="-10">Newly Created</tspan>
                                    <tspan x="150" dy="20">16</tspan>
                                </text>
                            </g><g><path d="M50,103.33333333333333 Q150,113.33333333333333 250,103.33333333333333 L200,196.66666666666666 M200,196.66666666666666 Q150,216.66666666666666 100,196.66666666666666 L50,103.33333333333333" fill="url(#d3-funnel-chart-0-gradient-1)"></path>
                                <text x="150" y="160" fill="#fff" font-size="14px" text-anchor="middle" dominant-baseline="middle" pointer-events="none">
                                    <tspan x="150" dy="-10">Convert To Prospect</tspan>
                                    <tspan x="150" dy="20">2</tspan>
                                </text>
                            </g><g><path d="M100,196.66666666666666 Q150,206.66666666666666 200,196.66666666666666 L200,290 M200,290 Q150,310 100,290 L100,196.66666666666666" fill="url(#d3-funnel-chart-0-gradient-2)"></path>
                                <text x="150" y="253.33333333333331" fill="#fff" font-size="14px" text-anchor="middle" dominant-baseline="middle" pointer-events="none">
                                    <tspan x="150" dy="-10">Won</tspan>
                                    <tspan x="150" dy="20">0</tspan>
                                </text>
                            </g></svg>
                    </div>--%>

                    <%--<div id="chartdiv"></div>--%>
                    <div id="funnel-container" style="display: none"></div>
                    <div class="funnel-chart">
                        <svg width="350" height="300">
                            <defs></defs>
                            <g cursor="default" font-family="-apple-system,BlinkMacSystemFont,'Segoe UI','Helvetica Neue',Arial,sans-serif" font-size="12px" font-weight="400">
                                <g cursor="default">
                                    <g>
                                        <rect width="350" height="300" fill="rgba(0,0,0,0)"></rect>
                                        <g transform="matrix(6.123233995736766e-17,1,-1,6.123233995736766e-17,341,8)">



                                            <g>
                                                <path d="M174.18788825728643,236.87035240905414A4.849922911848526,70.87035240905416,0,0,0,174.18788825728643,95.12964759094581A4.849922911848526,70.87035240905416,0,0,0,174.18788825728643,236.87035240905414A483.16965914395865,593.7128277905049,0,0,1,284,221.33333333333326A3.786666666666663,55.333333333333286,0,0,0,284,110.66666666666669A483.16965914395865,593.7128277905049,0,0,1,174.18788825728643,95.12964759094581" fill="rgb(104, 193, 130)" stroke="#FFFFFF"></path>
                                                <g transform="matrix(-1.8369701987210297e-16,-1,1,-1.8369701987210297e-16,232.88061079530988,165.99999999999997)" pointer-events="none">
                                                    <text dominant-baseline="middle" class="chart-label" fill="rgb(255, 255, 255)" text-anchor="middle" runat="server" id="lblWonF">Won: 0</text>
                                                </g>
                                            </g>


                                            <g role="img" aria-label="Convert To Prospect; Value: 2">
                                                <path d="M62.285652635312545,287.531735203591A8.316870553691528,121.53173520359101,0,0,0,62.285652635312545,44.46826479640896A8.316870553691528,121.53173520359101,0,0,0,62.285652635312545,287.531735203591A483.16965914395865,593.7128277905049,0,0,1,174.18788825728643,236.87035240905414A4.849922911848526,70.87035240905416,0,0,0,174.18788825728643,95.12964759094581A483.16965914395865,593.7128277905049,0,0,1,62.285652635312545,44.46826479640896" fill="rgb(50 197 210)" stroke="#FFFFFF"></path>
                                                <g transform="matrix(-1.8369701987210297e-16,-1,1,-1.8369701987210297e-16,123.08669335814801,165.99999999999997)" pointer-events="none">
                                                    <text dominant-baseline="middle" class="chart-label" fill="rgb(255, 255, 255)" text-anchor="middle"> Convert To Quotation:</text>

                                                </g>
                                                <g transform="matrix(-1.8369701987210297e-16,-1,1,-1.8369701987210297e-16,145.08669335814801,165.99999999999997)" pointer-events="none">
                                                    <text dominant-baseline="middle" class="chart-label" fill="rgb(255, 255, 255)" text-anchor="middle" runat="server" id="lblConvertToProspectF">0</text>
                                                </g>
                                            </g>


                                            <g role="img" aria-label="Newly Created; Value: 16" id="_dvtActiveElement180589916">
                                                <path d="M4.934393756608301,330.37507614415694A11.248800391551946,164.37507614415696,0,0,0,4.934393756608301,1.624923855843008A11.248800391551946,164.37507614415696,0,0,0,4.934393756608301,330.37507614415694A483.16965914395865,593.7128277905049,0,0,1,62.285652635312545,287.531735203591A8.316870553691528,121.53173520359101,0,0,0,62.285652635312545,44.46826479640896A483.16965914395865,593.7128277905049,0,0,1,4.934393756608301,1.624923855843008" fill="rgb(135, 117, 167)" stroke="#FFFFFF"></path>
                                                <g transform="matrix(-1.8369701987210297e-16,-1,1,-1.8369701987210297e-16,41.92689374965195,165.99999999999997)" pointer-events="none">
                                                    <text dominant-baseline="middle" class="chart-label" fill="rgb(255, 255, 255)" text-anchor="middle" runat="server" id="lblNewlyCreatedF">Newly Created: 0</text>
                                                </g>
                                            </g>
                                        </g>
                                    </g>
                                </g>
                            </g></svg>
                    </div>


                </div>
            </div>
            <br />
            <%--<div class="row">
        
                    <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        SMS Summary
                    </div>
                </div>
                <div class="portlet-body" style="padding:5px;">
                    <div id="divSMSBalanceStatisctics" class="row no-margin"><div class="col-sm-4" style="padding:0;margin-left:5px;border-right:1px solid #F3F3F4;">
    <div class="ibox-title">
        <h5>SMS Balance</h5>
    </div>
    <div class="ibox-content">
        <div class="row">
            <div class="col-sm-12" style="padding-bottom:10px;">
                <h2 class="no-margins" style="margin-top:5px"><i class="fa fa-envelope"></i>&nbsp;<span id="smsBalance">461</span></h2>
            </div>
        </div>
    </div>
</div></div>
                </div>
            </div>
                                            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Enquiry Activity Statistics
                    </div>
                    <div class="actions">
                        <div class="btn-group btn-group-devided" data-toggle="buttons">
                            <label class="btn red btn-outline btn-circle btn-sm active" style="padding:2px 5px;font-size:11px;">
                                <input name="leadstatistics" class="toggle" type="radio" value="Today" onchange="ShowEnquiryStatistics('Today');">Today
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding:2px 5px;font-size:11px;">
                                <input name="leadstatistics" class="toggle" type="radio" value="Week" onchange="ShowEnquiryStatistics('Week');">Week
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding:2px 5px;font-size:11px;">
                                <input name="leadstatistics" class="toggle" type="radio" value="Month" onchange="ShowEnquiryStatistics('Month');">Month
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding:2px 5px;font-size:11px;">
                                <input name="leadstatistics" class="toggle" type="radio" value="Year" onchange="ShowEnquiryStatistics('Year');">Year
                            </label>
                        </div>
                    </div>
                </div>
                <div class="portlet-body" style="padding:5px;">
                    <div id="divEnquiryStat"><div id="divLeadStatistics" class="row no-margin"><div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom:2px;padding-left:10px;padding-right:10px;"><a class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');"><div class="visual"><i class="fa fa-ticket"></i></div><div class="details"><div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div><div class="desc">Newly Created</div></div></a></div><div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom:2px;padding-left:10px;padding-right:10px;"><a class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('Assigned');"><div class="visual"><i class="fa fa-ticket"></i></div><div class="details"><div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div><div class="desc">Assigned</div></div></a></div><div class="col-md-2  thumbnail wide_thumbnail" style="margin-bottom:2px;padding-left:10px;padding-right:10px;"><a class="dashboard-stat dashboard-stat-v2 green" href="javascript:void(0);" onclick="VisitMyEnquiries('Prospect');"><div class="visual"><i class="fa fa-ticket"></i></div><div class="details"><div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div><div class="desc">Prospect</div></div></a></div><div class="col-md-2  thumbnail wide_thumbnail" style="margin-bottom:2px;padding-left:10px;padding-right:10px;"><a class="dashboard-stat dashboard-stat-v2 green-jungle" href="javascript:void(0);" onclick="VisitMyEnquiries('Won');"><div class="visual"><i class="fa fa-ticket"></i></div><div class="details"><div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div><div class="desc">Won</div></div></a></div><div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom:2px;padding-left:10px;padding-right:10px;"><a class="dashboard-stat dashboard-stat-v2 red-thunderbird" href="javascript:void(0);" onclick="VisitMyEnquiries('Lost');"><div class="visual"><i class="fa fa-ticket"></i></div><div class="details"><div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div><div class="desc">Lost</div></div></a></div><div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom:2px;padding-left:10px;padding-right:10px;"><a class="dashboard-stat dashboard-stat-v2 red-soft" href="javascript:void(0);" onclick="VisitMyEnquiries('Cancelled');"><div class="visual"><i class="fa fa-ticket"></i></div><div class="details"><div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div><div class="desc">Cancelled</div></div></a></div></div></div>
                </div>
            </div>
                    <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Enquiry Statistics Funnel
                    </div>
                    <div class="actions">
                        <div class="btn-group btn-group-devided" data-toggle="buttons">
                            <label class="btn red btn-outline btn-circle btn-sm active" style="padding:2px 5px;font-size:11px;">
                                <input name="leadFunnel" class="toggle" type="radio" value="Week" onchange="ShowLeadFunnel('Week');">Week
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding:2px 5px;font-size:11px;">
                                <input name="leadFunnel" class="toggle" type="radio" value="Month" onchange="ShowLeadFunnel('Month');">Month
                            </label>
                            <label class="btn red btn-outline btn-circle btn-sm" style="padding:2px 5px;font-size:11px;">
                                <input name="leadFunnel" class="toggle" type="radio" value="Year" onchange="ShowLeadFunnel('Year');">Year
                            </label>
                        </div>
                    </div>
                </div>
                <div class="portlet-body" style="padding:5px;text-align:center;">
                    <div id="divLeadFunnel" style="width: 300px; height: 300px; margin: 0 auto;" class="row no-margin"><svg id="d3-funnel-chart-0" width="300" height="300"><defs><linearGradient id="d3-funnel-chart-0-gradient-0"><stop offset="0%" style="stop-color: #6c5e86"></stop><stop offset="40%" style="stop-color: #8775A7"></stop><stop offset="60%" style="stop-color: #8775A7"></stop><stop offset="100%" style="stop-color: #6c5e86"></stop></linearGradient><linearGradient id="d3-funnel-chart-0-gradient-1"><stop offset="0%" style="stop-color: #289ea8"></stop><stop offset="40%" style="stop-color: #32C5D2"></stop><stop offset="60%" style="stop-color: #32C5D2"></stop><stop offset="100%" style="stop-color: #289ea8"></stop></linearGradient><linearGradient id="d3-funnel-chart-0-gradient-2"><stop offset="0%" style="stop-color: #1e9b67"></stop><stop offset="40%" style="stop-color: #26C281"></stop><stop offset="60%" style="stop-color: #26C281"></stop><stop offset="100%" style="stop-color: #1e9b67"></stop></linearGradient></defs><path fill="#514664" d="M0,10 Q150,30 300,10 M300,10 Q150,0 0,10"></path><g><path d="M0,10 Q150,20 300,10 L250,103.33333333333333 M250,103.33333333333333 Q150,123.33333333333333 50,103.33333333333333 L0,10" fill="url(#d3-funnel-chart-0-gradient-0)"></path><text x="150" y="66.66666666666666" fill="#fff" font-size="14px" text-anchor="middle" dominant-baseline="middle" pointer-events="none"><tspan x="150" dy="-10">Newly Created</tspan><tspan x="150" dy="20">16</tspan></text></g><g><path d="M50,103.33333333333333 Q150,113.33333333333333 250,103.33333333333333 L200,196.66666666666666 M200,196.66666666666666 Q150,216.66666666666666 100,196.66666666666666 L50,103.33333333333333" fill="url(#d3-funnel-chart-0-gradient-1)"></path><text x="150" y="160" fill="#fff" font-size="14px" text-anchor="middle" dominant-baseline="middle" pointer-events="none"><tspan x="150" dy="-10">Convert To Prospect</tspan><tspan x="150" dy="20">2</tspan></text></g><g><path d="M100,196.66666666666666 Q150,206.66666666666666 200,196.66666666666666 L200,290 M200,290 Q150,310 100,290 L100,196.66666666666666" fill="url(#d3-funnel-chart-0-gradient-2)"></path><text x="150" y="253.33333333333331" fill="#fff" font-size="14px" text-anchor="middle" dominant-baseline="middle" pointer-events="none"><tspan x="150" dy="-10">Won</tspan><tspan x="150" dy="20">0</tspan></text></g></svg></div>
                </div>
            </div>
                    <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Assigned Pending Enquiries [Recent 3]
                    </div>
                </div>
                <div class="portlet-body" style="padding:5px;">
                    <div class="row no-margin">
                        <div class="col-md-12" style="margin-top:10px;padding:0;">
                            <div id="tblEnquiry_wrapper" class="dataTables_wrapper no-footer"><div class="row"><div class="col-md-6 col-sm-6"></div><div class="col-md-6 col-sm-6"><div id="tblEnquiry_filter" class="dataTables_filter" style="display: none;"><label>Search:<input type="search" class="form-control input-sm input-small input-inline" placeholder="" aria-controls="tblEnquiry"></label></div></div></div><div class="table-scrollable"><table id="tblEnquiry" class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" style="border-bottom: none; width: 1649px;" role="grid">
                                <thead>
                                    <tr role="row"><th style="width: 39px; text-align: center;" class="all sorting_disabled" rowspan="1" colspan="1">#</th><th class="all sorting_disabled" style="width: 72px;" rowspan="1" colspan="1">Enquiry No.</th><th class="all sorting_disabled" style="width: 318px;" rowspan="1" colspan="1">Customer</th><th class="all sorting_disabled" style="width: 285px;" rowspan="1" colspan="1">Product</th><th class="all sorting_disabled" style="width: 318px;" rowspan="1" colspan="1">Source</th><th class="all sorting_disabled" style="width: 105px;" rowspan="1" colspan="1">Enquiry Date</th><th class="all sorting_disabled" style="width: 154px;" rowspan="1" colspan="1">Type</th><th class="all sorting_disabled" style="width: 154px;" rowspan="1" colspan="1">Status</th><th class="all sorting_disabled" style="width: 106px; text-align: center;" rowspan="1" colspan="1">Action</th></tr>
                                </thead>
                                <tbody><tr class="odd"><td valign="top" colspan="9" class="dataTables_empty" style="display: none;">No data available in table</td></tr></tbody>
                            </table></div><div class="row"><div class="col-md-5 col-sm-5"></div><div class="col-md-7 col-sm-7"></div></div></div>
                        </div>
                        <div id="divViewMoreEnquiries" class="btn-arrow-link pull-right" style="display:none;">
                            <a href="javascript:void(0);" onclick="VisitPageWithParam('Enquiry', 'ManageEnquiries','statisticsType=Week&amp;status=Assigned');">Show More <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                        <div id="divNoRecords" class="col-sm-12" style="">
                            <span class="text-danger">No records found</span>
                        </div>

                    </div>
                </div>
            </div>

                    <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Today's Follow-ups
                    </div>
                </div>
                <div class="portlet-body" style="padding:5px;">
                    <div id="divTodaysTask" class="row no-margin">
<div class="col-md-12" style="padding:0 10px 0 10px;">
        <span class="text-danger">No records found</span>
</div>
<script type="text/javascript">
    function CopyFollowUp(n){ShowLoader();$.ajax({url:"/Task/CopyFollowUp",type:"POST",data:{followUpId:n},success:function(n){ValidateAjaxRequest(n)?($("#myModalContent").html(n.renderedView),$("#myParitialModalForm").modal({keyboard:!0},"show")):DisplayErrorMessage(n.message);HideLoader()}})}function CopyFollowUp(n){ShowLoader();$.ajax({url:"/Task/CopyFollowUp",type:"POST",data:{followUpId:n},success:function(n){HideLoader();ValidateAjaxRequest(n,!1)?($("div.modal-backdrop").remove(),$("#divImageModalWindow").html(n.renderedView),$("#addFollowUpModalForm").modal({keyboard:!0},"show")):DisplayErrorMessage(n.message)}})}function UpdateFollowUpStatus(n,t,i){ShowLoader();$.ajax({url:"/Task/UpdateFollowUpStatus",type:"POST",data:{followUpId:n,status:t,comment:i},success:function(n){HideLoader();ValidateAjaxRequest(n,!1)?(ShowFollowUps(),DisplaySuccessMessage(n.message)):DisplayErrorMessage(n.message)}})}function CancelFollowUp(n){$("html, body").css("overflow-y","visible");swal({title:"Are you sure to cancel this follow-up?",type:"input",showCancelButton:!0,confirmButtonText:"Yes",confirmButtonColor:"#3085d6",cancelButtonText:"No",closeOnConfirm:!1,animation:"pop",inputPlaceholder:"Comment to cancel follow-up",showLoaderOnConfirm:!0},function(t){if(t===!1)return!1;UpdateFollowUpStatus(n,"Cancelled",t);swal.close()});CustomizeSweetAlert()}function CompleteFollowUp(n){$("html, body").css("overflow-y","visible");swal({title:"Are you sure to complete this follow-up?",type:"input",showCancelButton:!0,confirmButtonText:"Yes",confirmButtonColor:"#3085d6",cancelButtonText:"No",closeOnConfirm:!1,animation:"pop",inputPlaceholder:"Comment to complete follow-up",showLoaderOnConfirm:!0},function(t){if(t===!1)return!1;UpdateFollowUpStatus(n,"Completed",t);swal.close()});CustomizeSweetAlert()}function EditFollowUp(n){ShowLoader();$.ajax({url:"/Task/EditFollowUp",type:"POST",data:{followUpId:n},success:function(n){HideLoader();ValidateAjaxRequest(n,!1)?($("div.modal-backdrop").remove(),$("#divImageModalWindow").html(n.renderedView),$("#addFollowUpModalForm").modal({keyboard:!0},"show")):DisplayErrorMessage(n.message)}})}
</script></div>
                </div>
            </div>
        <div class="col-md-12" style="padding:0;">
                <div class="col-md-6" style="padding:2px">
                    <div class="portlet box green">
                        <div class="portlet-title">
                            <div class="caption">
                                Today's Birthdays
                            </div>
                            <div class="pull-right">
                                <button type="button" class="btn btn-sm btn-primary" style="margin-top:5px;" onclick="ShowTodaysBirthdays();">Show Birthdays</button>
                            </div>
                        </div>
                        <div class="portlet-body" style="padding:5px;">
                            <div id="divTodaysBirthdays" class="row no-margin">
                            </div>
                        </div>
                    </div>
                </div>
                            <div class="col-md-6" style="padding:2px">
                    <div class="portlet box green">
                        <div class="portlet-title">
                            <div class="caption">
                                Today's Anniversaries
                            </div>
                            <div class="pull-right">
                                <button type="button" class="btn btn-sm btn-primary" style="margin-top:5px;" onclick="ShowTodaysAnniversaries();">Show Anniversaries</button>
                            </div>
                        </div>
                        <div class="portlet-body" style="padding:5px;">
                            <div id="divTodaysAnniversaries" class="row no-margin">
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </div>--%>
        </div>
        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB5plfGdJPhLvXriCfqIplJKBzbJVC8GlI"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="../Scripts/jquery.funnel.js"></script>
        <script>
            var funnelData = [
                {
                    value: 5000,
                    color: "#ED5565",
                    label: 'Newly Created: 14'
                },
                {
                    value: 4000,
                    color: "#FC6D58",
                    label: 'Convert To Quotation: 7'
                },
                {
                    value: 3000,
                    color: "#46CFB0",
                    label: 'Won: 0'
                },
                {
                    value: 2000,
                    color: "#9FD477",
                    label: 'Value 4'
                },

            ];

          <%--  var funnelData = JSON.parse('<%=funnelData() %>');--%>
            $('#funnel-container').drawFunnel(funnelData, {
                width: 380, // Container height, i.e. height of #funnel-container
                height: $(this).height(),  // Container width, i.e. width of #funnel-container
                padding: 1, // Padding between segments, in pixels
                half: false,  // Render only a half funnel
                minSegmentSize: 150  // Width of a segment can't be smaller than this, in pixels
                // label: function () { return "Label!"; }  // A label generation function 
            });
        </script>





    </body>
    </html>
</asp:Content>
