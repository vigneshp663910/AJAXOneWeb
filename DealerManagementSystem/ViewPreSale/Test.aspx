<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript"> 
        google.charts.load('current', { 'packages': ['corechart'] }); 
        google.charts.setOnLoadCallback(drawChart1); 
        function drawChart1() {
            var country = $("#MainContent_DrpMonth option:selected").val();
            var options = { title: country + ' Distribution', is3D: false };
            $.ajax({
                type: "POST",
                url: 'Open.aspx/GetChartData',
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="true">
        <asp:ListItem Text="Finland" Value="Finland" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Brazil" Value="Brazil"></asp:ListItem>
        <asp:ListItem Text="USA" Value="USA"></asp:ListItem>
        <asp:ListItem Text="Italy" Value="Italy"></asp:ListItem>
        <asp:ListItem Text="Germany" Value="Germany"></asp:ListItem>
    </asp:DropDownList>
                
    
     <div id="chart" style="width: 100%; height: 500px;"></div> 

</asp:Content>
