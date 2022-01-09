<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Pre_Sales_DashboardVignesh.aspx.cs" Inherits="DealerManagementSystem.ViewPreSale.Pre_Sales_DashboardVignesh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="//www.google.com/jsapi"></script>--%>
    <script src="../Menubar.js" type="text/javascript"></script>
  
<script type="text/javascript">
           

    //function drawChart() {
        //    var options = {
        //        title: 'State vs Count',
        //        width: 1000,
        //        height: 600,
        //        bar: { groupWidth: "95%" },
        //        legend: { position: "none" },
        //        isStacked: true,
        //    };
        //    $.ajax({
        //        type: "POST",
        //        url: "Pre_Sales_DashboardVignesh.aspx/GetData",
        //        data: '{}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (r) {
        //            var data = google.visualization.arrayToDataTable(r.d);
        //            data.addColumn('string', 'State');
        //            data.addColumn('number', 'Count');
        //            var chart = new google.visualization.LineChart($("#visualization")[0]);
        //            chart.draw(data, options);
        //        },
        //        failure: function (r) {
        //            alert(r.d);
        //        },
        //        error: function (r) {
        //            alert(r.d);
        //        }
        //    });
        //}
</script>

        
    
    <style>
        .card {
            color: white;
        }

        .crd1 {
            background-color: #8775a7;
        }

        .crd2 {
            background-color: #3598dc;
        }

        .crd3 {
            background-color: #32c5d2;
        }

        .crd4 {
            background-color: #26c281;
        }

        .crd5 {
            background-color: #d91e18;
        }
    </style>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['bar'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['Year', 'Sales', 'Expenses', 'Profit'],
                ['2015', 1000, 400, 200],
                ['2016', 1170, 460, 250],
                ['2017', 660, 1120, 300],
                ['2018', 1030, 540, 350],
                ['2019', 1170, 460, 250],
                ['2020', 1000, 400, 200],
                ['2022', 660, 1120, 300]
            ]);

            var options = {
                chart: {
                    title: 'Lead Analysis',
                    subtitle: 'Sales, Expenses, and Profit: 2015-2022',
                },
                bars: 'vertical' // Required for Material Bar Charts.
            };

            var chart = new google.charts.Bar(document.getElementById('barchart_material'));

            chart.draw(data, google.charts.Bar.convertOptions(options));
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-xl-3 col-md-6">
            <div class="card crd1">
                <label>card</label>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd1" style="color: black">
                <div class="card-header">
                    <label>caesfergfvrsfvr  rd</label>
                </div>
                <div class="card-body">
                    <label>cardrfgvergve</label>
                    <label>carwg  fet54gt5e6rd</label>
                    <label>carregb be5g6e5hd</label>
                    <label>caesfergfvrsfvr  rd</label>
                    <label>cardrfgvergve</label>
                    <label>carwg  fet54gt5e6rd</label>
                    <label>carregb be5g6e5hd</label>
                    <label>caesfergfvrsfvr  rd</label>
                    <label>cardrfgvergve</label>
                    <label>carwg  fet54gt5e6rd</label>
                </div>
                <div class="card-footer">
                    <label>carregb be5g6e5hd</label>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd2">
                <label>cardchart</label>
            </div>
            <div class="card-body">
                <div id="visualization">
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd3">
                <label>card</label>
            </div>
        </div>
        <div class="col-xl-6 col-md-12">
            <div class="card crd4">
                <div class="card-header">
                    <label>card</label>
                </div>
                <div class="card-body">
                    <div id="barchart_material" style="height: 200px;"></div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd5">
                <label>card</label>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd1">
                <label>card</label>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd1">
                <label>card</label>
            </div>
        </div>
        <div class="col-xl-6 col-md-12">
            <div class="card crd1">
                <label>card</label>
            </div>
        </div>
        <div class="col-xl-3 col-md-6">
            <div class="card crd1">
                <label>card</label>
            </div>
        </div>
    </div>
</asp:Content>
