<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Task_Dashboard.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.Task_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .portlet.box.green {
            border: 1px solid #5cd1db;
            border-top: 0;
            padding-bottom: 11px;
            padding-top: 11px;
            margin-left: 1px;
        }

            .portlet.box.green > .portlet-title {
                background-color: mediumturquoise;
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
            margin: 1px 1px 1px 1px;
            border: 2px solid #5cd1db;
        }
            /* The container */
            .TimeAction .container {
                display: block;
                position: relative;
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
    <div class="lead-static">
        <div class="TimeAction">
            <div class="col-md-12">
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">From</label>
                    <asp:TextBox ID="txtTicketFrom" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">To</label>
                    <asp:TextBox ID="txtTicketTo" runat="server" CssClass="TextBox form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">Category</label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="TextBox form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-1 col-sm-12">
                    <label class="modal-label">Subcategory</label>
                    <asp:DropDownList ID="ddlSubcategory" runat="server" CssClass="TextBox form-control"></asp:DropDownList>
                </div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                </div>
            </div>
        </div>




        <div class="portlet box green">
            <div class="portlet-title">
                <div class="caption" style="font-size: 25px; border-bottom-color: #556b2f; border-bottom-width: initial; border-bottom-style: none;">
                    Task Statistics                    
                </div>
            </div>
            <div class="portlet-body" style="padding: 5px;">
                <div id="divEnquiryStat1">
                    <div id="divLeadStatistics" class="row no-margin" style="font-size: medium; text-align: right;">
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                            <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('Created');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblCreated" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Created</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #CC6E2A">
                            <div class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('Open');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                    <asp:Label ID="lblOpen" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnOpen" runat="server" Style="color: white;" OnClick="lbActions_Click">Open</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--<div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 1px; background-color: #CC6E2A;">
                            <div class="dashboard-stat dashboard-stat-v2 red-soft" href="javascript:void(0);" onclick="VisitMyEnquiries('Approved');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblApproved" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnApproved" runat="server" Style="color: white;" OnClick="lbActions_Click">Approved</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #bf9020;">
                            <div class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('Assigned');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblAssigned" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnAssigned" runat="server" Style="color: white;" OnClick="lbActions_Click">Assigned</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #3598dc;">
                            <div class="dashboard-stat dashboard-stat-v2 green-jungle" href="javascript:void(0);" onclick="VisitMyEnquiries('InProgress');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblInProgress" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnInProgress" runat="server" Style="color: white;" OnClick="lbActions_Click">In Progress</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #009DA1;">
                            <div class="dashboard-stat dashboard-stat-v2 red-thunderbird" href="javascript:void(0);" onclick="VisitMyEnquiries('Resolved');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblResolved" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnResolved" runat="server" Style="color: white;" OnClick="lbActions_Click">Resolved</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 1px; background-color: #d91e18;">
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
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 1px; background-color: #26c281;">
                            <div class="dashboard-stat dashboard-stat-v2 red-soft" href="javascript:void(0);" onclick="VisitMyEnquiries('ForceClose');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblForceClose" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnForceClose" runat="server" Style="color: white;" OnClick="lbActions_Click">Force Close</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: darkolivegreen;">
                            <div class="dashboard-stat dashboard-stat-v2 red-thunderbird" href="javascript:void(0);" onclick="VisitMyEnquiries('Closed');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <asp:Label ID="lblClosed" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnClosed" runat="server" Style="color: white;" OnClick="lbActions_Click">Closed</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; background-color: #d05454">
                            <div class="dashboard-stat dashboard-stat-v2 blue" href="javascript:void(0);" onclick="VisitMyEnquiries('WaitingForApproval');">
                                <div class="visual"><i class="fa fa-ticket"></i></div>
                                <div class="details" style="color: white;">
                                    <%--<div class="number"><span data-counter="counterup" data-value="0" class="counter1">0</span></div>--%>
                                    <asp:Label ID="lblWaitingForApproval" runat="server" Text="0"></asp:Label>
                                    <div class="desc">
                                        <asp:LinkButton ID="lbtnWaitingForApproval" runat="server" Style="color: white;" OnClick="lbActions_Click">Approval</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>






        </div>
        <div class="col-md-12">
            <div class="col-md-12 Report">
                <fieldset class="fieldset-border">
                    <legend style="background: none; color: #007bff; font-size: 17px;">Report</legend>
                    <div class="col-md-12 Report">
                        <asp:GridView ID="gvTickets" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-condensed Grid" AllowPaging="true" PageSize="15">
                            <Columns>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Text='<%# DataBinder.Eval(Container.DataItem, "Year")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Text='<%# DataBinder.Eval(Container.DataItem, "Month")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TotalCreated">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTotalCreated" Text='<%# DataBinder.Eval(Container.DataItem, "TotalCreated")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opened">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOpened" Text='<%# DataBinder.Eval(Container.DataItem, "Opened")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Approved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkApproved" Text='<%# DataBinder.Eval(Container.DataItem, "Approved")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Assigned">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAssigned" Text='<%# DataBinder.Eval(Container.DataItem, "Assigned")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="In Progress">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkInProgress" Text='<%# DataBinder.Eval(Container.DataItem, "InProgress")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resolved">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkResolved" Text='<%# DataBinder.Eval(Container.DataItem, "Resolved")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Cancelled">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkCancelled" Text='<%# DataBinder.Eval(Container.DataItem, "Cancelled")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ForceClose">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkForceClose" Text='<%# DataBinder.Eval(Container.DataItem, "ForceClose")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Closed">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkClosed" Text='<%# DataBinder.Eval(Container.DataItem, "Closed")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkWaitingForApproval" Text='<%# DataBinder.Eval(Container.DataItem, "WaitingForApproval")%>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending">
                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lnkPending" Text='<%# DataBinder.Eval(Container.DataItem, "Pending")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#ffffff" />
                            <FooterStyle ForeColor="White" />
                            <HeaderStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#fbfcfd" ForeColor="Black" HorizontalAlign="Left" />
                        </asp:GridView>
                    </div>
                </fieldset>
            </div>
        </div>

        <div class="col-md-12">
            <div id="divTaskChartStatistics"></div>
        </div>

        <br />

    </div>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript"> 
        function TaskStatusChart() {
            var param = {
                Category: $('#MainContent_ddlCategory').val()
                , Subcategory: $('#MainContent_ddlSubcategory').val()
                , DateFrom: $('#MainContent_txtTicketFrom').val()
                , DateTo: $('#MainContent_txtTicketTo').val()
            }
            $.ajax({
                type: "POST",
                url: 'Task_Dashboard.aspx/TaskStatusChart',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {

                    var data1 = google.visualization.arrayToDataTable(data.d);
                    var view = new google.visualization.DataView(data1);
                    view.setColumns([0, 1,
                        //{
                        //    calc: "stringify",
                        //    sourceColumn: 1,
                        //    type: "string",
                        //    role: "annotation"
                        //},
                        2, 3
                    ]);
                    var options = {
                        //width: '80%',
                        title: 'Task Statistics',
                        height: 500
                        //legend: { position: 'top', maxLines: 5 },
                        //bar: { groupWidth: '80%' },
                        //isStacked: true,
                        //is3D: true
                        //trendlines: {
                        //    0: { type: 'exponential', color: '#333', opacity: 2 }
                        //}
                    };

                    //var chart = new google.charts.Bar(document.getElementById('divTaskChartStatistics'));
                    //chart.draw(data, google.charts.Bar.convertOptions(options));

                    var chart = new google.visualization.ColumnChart(document.getElementById("divTaskChartStatistics"));
                    chart.draw(view, options);
                },
                failure: function (r) {
                    alert(r);
                },
                error: function (r) {
                    alert(r);
                }
            });
        }
    </script>
    
</asp:Content>
