<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="LeadChart.aspx.cs" Inherits="DealerManagementSystem.ViewDashboard.LeadChart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript"> 
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        google.charts.setOnLoadCallback(drawChart1);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['Year', 'Sales', 'Expenses'],
                ['2013', 1000, 400],
                ['2014', 1170, 460],
                ['2015', 660, 1120],
                ['2016', 1030, 540]
            ]);

            var options = {
                title: 'Company Performance',
                hAxis: { title: 'Year', titleTextStyle: { color: '#333' } },
                vAxis: { minValue: 0 }
            };

            var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
            chart.draw(data, options);

        }


        function drawChart1() {
            var country = $("#MainContent_DrpMonth option:selected").val();
            var options = { title: country + ' Distribution', is3D: false };
            $.ajax({
                type: "POST",
                url: 'LeadChart.aspx/GetChartData',
                data: "{country: '" + country + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    var data1 = google.visualization.arrayToDataTable(data.d);
                    var chart = new google.visualization.AreaChart($("#chart")[0]);
                    chart.draw(data1, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            }); 
        } 
    </script>

    <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="true">
        <asp:ListItem Text="Finland" Value="Finland" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Brazil" Value="Brazil"></asp:ListItem>
        <asp:ListItem Text="USA" Value="USA"></asp:ListItem>
        <asp:ListItem Text="Italy" Value="Italy"></asp:ListItem>
        <asp:ListItem Text="Germany" Value="Germany"></asp:ListItem>
    </asp:DropDownList>

     <div id="chart_div" style="width: 100%; height: 500px;"></div> 
     <div id="chart" style="width: 100%; height: 500px;"></div> 

    
 <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
  <script type="text/javascript">
      google.charts.load("current", { packages: ['corechart'] });
      google.charts.setOnLoadCallback(drawChart);
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
                  //var options = {
                  //    isStacked: true,
                  //    height: 300,
                  //    legend: { position: 'top', maxLines: 5 },
                  //    vAxis: { minValue: 0 }
                  //};

                  var options = {
                      //width: '80%',
                       height: 400,
                      legend: { position: 'top', maxLines: 5},
                      bar: { groupWidth: '80%' },
                      isStacked: true,
                        is3D: true
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
           
          //var data = google.visualization.arrayToDataTable([
          //    ['Genre', 'Fantasy & Sci Fi', 'Romance', 'Mystery Crime', 'General', 'Western', 'Literature', { role: 'annotation' }],
          //    ['2010', 10, 24, 20, 32, 18, 5, ''],
          //    ['2020', 16, 22, 23, 30, 16, 9, ''],
          //    ['2030', 28, 19, 29, 30, 12, 13, '']
          //]);
          
          //var data = google.visualization.arrayToDataTable(markers);
       
          //var data = google.visualization.arrayToDataTable([
          //    ['Genre', 'Fantasy & Sci Fi', 'Romance', 'Mystery Crime', 'General', 'Western', 'Literature'],
          //    ['2010', 10, 24, 20, 32, 18, 5],
          //    ['2020', 16, 22, 23, 30, 16, 9],
          //    ['2030', 28, 19, 29, 30, 12, 13]
          //]);

          
           
      }
  </script>
<div id="columnchart_values"  ></div>
   
</asp:Content>
