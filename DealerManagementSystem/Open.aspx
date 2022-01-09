<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Open.aspx.cs" Inherits="DealerManagementSystem.Open" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript"> 
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart1);
        function drawChart1() {
            var country = $("#DrpMonth option:selected").val();
            var options = { title: country + ' Distribution', is3D: false };
            $.ajax({
                type: "POST",
                url: '/Open.aspx/GetChartData',
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
</head>
<body>
    <form id="form1" runat="server">

             <asp:DropDownList ID="DrpMonth" runat="server" AutoPostBack="true">
        <asp:ListItem Text="Finland" Value="Finland" Selected="True"></asp:ListItem>
        <asp:ListItem Text="Brazil" Value="Brazil"></asp:ListItem>
        <asp:ListItem Text="USA" Value="USA"></asp:ListItem>
        <asp:ListItem Text="Italy" Value="Italy"></asp:ListItem>
        <asp:ListItem Text="Germany" Value="Germany"></asp:ListItem>
    </asp:DropDownList>
                
    
     <div id="chart" style="width: 100%; height: 500px;"></div> 
          <table>

                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="API Test"></asp:Label></td>
                    <td>
                        <asp:Button ID="btnAPITest" runat="server" Text="API Test" OnClick="btnAPITest_Click" />
                    </td>
                    <td>
                        <asp:Button ID="BtnMaterial" runat="server" Text="Material" OnClick="BtnMaterial_Click"/>
                    </td>
                    <td>
                        <asp:Label ID="lblAPITest" runat="server"></asp:Label></td>
                </tr>
              </table>
    </form>
</body>
</html>
