<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LeadChart.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.LeadChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12" id="divList" runat="server">
            <fieldset class="fieldset-border" id="Fieldset2" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Specify Criteria</legend>
                <div class="col-md-12">
                    <div class="col-md-2 text-left">
                        <label>Dealer</label>
                        <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-2 text-left">
                        <label>Country</label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label>Region</label>
                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <%--    <div class="col-md-2 text-left">
                        <label>Year</label>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                    </div> 
                     <div class="col-md-2 text-left">
                        <label>Month</label>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" />
                    </div> --%>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn Search" Text="Retrieve" OnClick="BtnSearch_Click"></asp:Button>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">        
        function drawChart() {
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/GetChartData2',
                data: "{country: '  country '}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) {
                    return data;
                },
                success: function (data) {
                    debugger;
                    var data1 = google.visualization.arrayToDataTable(data.d);
                    var view = new google.visualization.DataView(data1);
                    view.setColumns([0, 1,
                        //{
                        //    calc: "stringify",
                        //    sourceColumn: 1,
                        //    type: "string",
                        //    role: "annotation"
                        //},
                        2, 3, 4
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
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/GetRegionWiseLeadStatus',
                data: "{country: '  country '}",
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
                        2, 3, 4
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

    <div id="columnchart_values"></div>
    <div id="divRegionWiseLeadStatus"></div>

    <div id="curve_chart" style="width: 900px; height: 500px"></div>
      <script type="text/javascript">
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
                  ['Year', 'Visit %' ],
                  ['1', 30 ],
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
      </script>
</asp:Content>
