<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LeadChart.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.LeadChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #divRegionEast {
            position: relative;
            width: 550px;
            height: 400px;
        }

        #divRegionNorth {
            position: relative;
            width: 550px;
            height: 400px;
        }

        #divRegionSouth {
            position: relative;
            width: 550px;
            height: 400px;
        }

        #divRegionWest {
            position: relative;
            width: 550px;
            height: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />

    <asp1:TabContainer ID="tbpCust" runat="server" Font-Bold="True" Font-Size="Medium">
        <asp1:TabPanel ID="tpnlSalesEngineer" runat="server" HeaderText="YTD" Font-Bold="True" ToolTip="" Enabled="false">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12" id="div1" runat="server">
                        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-left">
                                    <label>Dealer</label>
                                    <asp:DropDownList ID="ddlYDealer" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2 text-left">
                                    <label>Country</label>
                                    <asp:DropDownList ID="ddlYCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlYCountry_SelectedIndexChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label>Region</label>
                                    <asp:DropDownList ID="ddlYRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                    </div>
                </div>
            </ContentTemplate>
        </asp1:TabPanel>
        <asp1:TabPanel ID="tpnlFollowUp" runat="server" HeaderText="Month">
            <ContentTemplate>
                <div class="col-md-12">
                    <div class="col-md-12" id="divList" runat="server">
                        <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                            <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                            <div class="col-md-12">
                                <div class="col-md-2 text-left">
                                    <label>Dealer</label>
                                    <asp:DropDownList ID="ddlMDealer" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-2 text-left">
                                    <label>Country</label>
                                    <asp:DropDownList ID="ddlMCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlMCountry_SelectedIndexChanged" AutoPostBack="true" />
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label>Region</label>
                                    <asp:DropDownList ID="ddlMRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Date From</label>
                                    <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label class="modal-label">Date To</label>
                                    <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" BorderColor="Silver" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-sm-12">
                                    <label>ProductType</label>
                                    <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-md-12 Report">
                    <div class="table-responsive">
                    </div>
                </div>
                <asp1:TabContainer ID="TabContainer1" runat="server" Font-Bold="True" Font-Size="Medium">
                    <asp1:TabPanel ID="TabPanel1" runat="server" HeaderText="Region Wise" Font-Bold="True" ToolTip="">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div id="divRegionEast"></div>
                                </div>
                                <div class="col-md-6">
                                    <div id="divRegionNorth"></div>
                                </div>
                                <div class="col-md-6">
                                    <div id="divRegionSouth"></div>
                                </div>
                                <div class="col-md-6">
                                    <div id="divRegionWest"></div>
                                </div>
                            </div>




                            <div id="columnchart_values"></div>
                            <div id="divRegionWiseLeadStatus"></div>
                        </ContentTemplate>
                    </asp1:TabPanel>
                    <asp1:TabPanel ID="TabPanel2" runat="server" HeaderText="Month">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp1:TabPanel>

                </asp1:TabContainer>

                <div id="curve_chart" style="width: 900px; height: 500px"></div>
            </ContentTemplate>
        </asp1:TabPanel>

    </asp1:TabContainer>


    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript"> 
        function RegionEastChart() {
            var Region = $('#MainContent_tbpCust_tpnlFollowUp_ddlMRegion').val();
            var param = {
                Dealer: $('#MainContent_tbpCust_tpnlFollowUp_ddlMDealer').val()
                , LeadDateFrom: $('#MainContent_tbpCust_tpnlFollowUp_txtDateFrom').val()
                , LeadDateTo: $('#MainContent_tbpCust_tpnlFollowUp_txtDateTo').val()
                , Country: $('#MainContent_tbpCust_tpnlFollowUp_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_tbpCust_tpnlFollowUp_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/RegionEastChart',
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
                        2, 3, 4, 5
                    ]);
                    var options = {
                        title: 'East Region',
                        //width: '80%',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true,
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        }
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("divRegionEast"));
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
        function RegionNorthChart() {
            var Region = $('#MainContent_tbpCust_tpnlFollowUp_ddlMRegion').val();
            var param = {
                Dealer: $('#MainContent_tbpCust_tpnlFollowUp_ddlMDealer').val()
                , LeadDateFrom: $('#MainContent_tbpCust_tpnlFollowUp_txtDateFrom').val()
                , LeadDateTo: $('#MainContent_tbpCust_tpnlFollowUp_txtDateTo').val()
                , Country: $('#MainContent_tbpCust_tpnlFollowUp_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_tbpCust_tpnlFollowUp_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/RegionNorthChart',
                //data: "{country: '  country '}",
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
                        2, 3, 4, 5
                    ]);
                    var options = {
                        title: 'North Region',
                        //width: '80%',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true,
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        }
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("divRegionNorth"));
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
        function RegionSouthChart() {
            var Region = $('#MainContent_tbpCust_tpnlFollowUp_ddlMRegion').val();
            var param = {
                Dealer: $('#MainContent_tbpCust_tpnlFollowUp_ddlMDealer').val()
                , LeadDateFrom: $('#MainContent_tbpCust_tpnlFollowUp_txtDateFrom').val()
                , LeadDateTo: $('#MainContent_tbpCust_tpnlFollowUp_txtDateTo').val()
                , Country: $('#MainContent_tbpCust_tpnlFollowUp_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_tbpCust_tpnlFollowUp_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/RegionSouthChart',
                //data: "{country: '  country '}",
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
                        2, 3, 4, 5
                    ]);
                    var options = {
                        //width: '80%',
                        title: 'South Region',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true,
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        }
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("divRegionSouth"));
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
        function RegionWestChart() {
            var Region = $('#MainContent_tbpCust_tpnlFollowUp_ddlMRegion').val();
            var param = {
                Dealer: $('#MainContent_tbpCust_tpnlFollowUp_ddlMDealer').val()
                , LeadDateFrom: $('#MainContent_tbpCust_tpnlFollowUp_txtDateFrom').val()
                , LeadDateTo: $('#MainContent_tbpCust_tpnlFollowUp_txtDateTo').val()
                , Country: $('#MainContent_tbpCust_tpnlFollowUp_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_tbpCust_tpnlFollowUp_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/RegionWestChart',
                //data: "{country: '  country '}",
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
                        2, 3, 4, 5
                    ]);
                    var options = {
                        //width: '80%',
                        title: 'West Region',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true,
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        }
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("divRegionWest"));
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
        function drawChart() {
            var Region = $('#MainContent_tbpCust_tpnlFollowUp_ddlMRegion').val();
            var param = {
                Dealer: $('#MainContent_tbpCust_tpnlFollowUp_ddlMDealer').val()
                , LeadDateFrom: $('#MainContent_tbpCust_tpnlFollowUp_txtDateFrom').val()
                , LeadDateTo: $('#MainContent_tbpCust_tpnlFollowUp_txtDateTo').val()
                , Country: $('#MainContent_tbpCust_tpnlFollowUp_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_tbpCust_tpnlFollowUp_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/GetChartData2',
                //data: "{country: '  country '}",
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
                        2, 3, 4, 5
                    ]);
                    var options = {
                        //width: '80%',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true,
                        trendlines: {
                            0: { type: 'exponential', color: '#333', opacity: 2 }
                        }
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
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

        function RegionWiseLeadStatusChart() {
            var Region = $('#MainContent_tbpCust_tpnlFollowUp_ddlMRegion').val();
            var param = {
                Dealer: $('#MainContent_tbpCust_tpnlFollowUp_ddlMDealer').val()
                , LeadDateFrom: $('#MainContent_tbpCust_tpnlFollowUp_txtDateFrom').val()
                , LeadDateTo: $('#MainContent_tbpCust_tpnlFollowUp_txtDateTo').val()
                , Country: $('#MainContent_tbpCust_tpnlFollowUp_ddlMCountry').val()
                , Region: Region
                , ProductType: $('#MainContent_tbpCust_tpnlFollowUp_ddlProductType').val()
            }
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/GetRegionWiseLeadStatus',
                //data: "{country: '  country '}",
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
                        2, 3, 4, 5
                    ]);
                    var options = {
                        //width: '80%',
                        height: 400,
                        legend: { position: 'top', maxLines: 5 },
                        bar: { groupWidth: '80%' },
                        isStacked: true,
                        is3D: true
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("divRegionWiseLeadStatus"));
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


    <%--    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart1);

        function drawChart1() {
            //var data = google.visualization.arrayToDataTable([
            //    ['Year', '2019', '2020', '2021'],
            //    ['1', 1000, 400, 400],
            //    ['2', 0, 460, 400],
            //    ['3', 660, 1120, 400],
            //    ['4', 1030, 0, 400],
            //    ['5', 1000, 400, 400],
            //    ['6', 0, 460, 420],
            //    ['7', 660, 1120, 400],
            //    ['8', 1030, 0, 200],
            //    ['9', 1000, 400, 400],
            //    ['10', 0, 460, 400],
            //    ['11', 660, 1120, 78],
            //    ['12', 1030, 0, 400]
            //]);

            var data = google.visualization.arrayToDataTable([
                ['Year', 'Visit %'],
                ['1', 30],
                ['2', 50],
                ['3', 20],
                ['4', 30],
                ['5', 50],
                ['6', 20],
            ]);

            var options = {

                title: 'Company Performance',
                curveType: 'function',
                legend: { position: 'bottom' },
                displayAnnotations: true
            };


            var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

            chart.draw(data, options);
        }
    </script>--%>
</asp:Content>
