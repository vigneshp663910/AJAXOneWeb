<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="Pre_Sales_Dashboard.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Pre_Sales_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .portlet.box.green {
         /*   border: 1px solid #5cd1db;*/
            border: 1px solid #483D8B;
            border-top: 0;
        }

            .portlet.box.green > .portlet-title {
               /* background-color: #32c5d2;*/
                background-color: #40E0D0;
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
            min-block-size:30px;
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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    </head>


    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server" /> </asp:ScriptManager>--%>
    <body>

        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption" style="font-size:25px">
                    Lead Activity Statistics
                   
                
                <div class="actions" style="float:right;">
                    <div class="btn-group btn-group-devided" data-toggle="buttons">
                        <label class="btn red btn-outline btn-circle btn-sm active" style="padding: 2px 5px 2px 5px; font-size: 11px;">
                            <input name="leadstatistics" class="toggle" type="radio" value="Today" onchange="ShowEnquiryStatistics('Today');">Today
                           
                        </label>
                        <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                            <input name="leadstatistics" class="toggle" type="radio" value="Week" onchange="ShowEnquiryStatistics('Week');">Week
                           
                        </label>
                        <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                            <input name="leadstatistics" class="toggle" type="radio" value="Month" onchange="ShowEnquiryStatistics('Month');">Month
                           
                        </label>
                        <label class="btn red btn-outline btn-circle btn-sm" style="padding: 2px 5px; font-size: 11px;">
                            <input name="leadstatistics" class="toggle" type="radio" value="Year" onchange="ShowEnquiryStatistics('Year');">Year
                           
                        </label>
                    </div>
                </div>
                    </div>
            </div>
            <div class="portlet-body" style="padding: 5px;">
                <div id="divEnquiryStat">
                    <div id="divLeadStatistics" class="row no-margin">
                        <div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px;">
                            <a class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details">
                                    <div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>
                                    <div class="desc">Newly Created</div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px;">
                            <a class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('Assigned');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details">
                                    <div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>
                                    <div class="desc">Assigned</div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-2  thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px;">
                            <a class="dashboard-stat dashboard-stat-v2 green" href="javascript:void(0);" onclick="VisitMyEnquiries('Prospect');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details">
                                    <div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>
                                    <div class="desc">Prospect</div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-2  thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px;">
                            <a class="dashboard-stat dashboard-stat-v2 green-jungle" href="javascript:void(0);" onclick="VisitMyEnquiries('Won');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details">
                                    <div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>
                                    <div class="desc">Won</div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px;">
                            <a class="dashboard-stat dashboard-stat-v2 red-thunderbird" href="javascript:void(0);" onclick="VisitMyEnquiries('Lost');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details">
                                    <div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>
                                    <div class="desc">Lost</div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-2 thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px;">
                            <a class="dashboard-stat dashboard-stat-v2 red-soft" href="javascript:void(0);" onclick="VisitMyEnquiries('Cancelled');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details">
                                    <div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>
                                    <div class="desc">Cancelled</div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </body>
    </html>
</asp:Content>
