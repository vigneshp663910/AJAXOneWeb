<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSatisfactionInAfterSalesSupport.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.CustomerSatisfactionInAfterSalesSupport" %>



<div class="tile-size-two grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Mean Time to Respond (MTTR-1) < 6 Hrs" runat="server" />
            </div>
            <div class="details-position">
                <%--<div id="donut_single" style="width: 100px; height: 100px;"></div>--%>
                <asp:Label ID="lblMTTR1" runat="server" CssClass="sapMNCValueScr" />
            </div>

        </div>
    </div>
</div>
<div class="tile-size-two grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label12" Text="Mean Time to Restore (MTTR-2) < 24 Hrs" runat="server" />
            </div>

            <div class="details-position">
                <%--<div id="donut_single" style="width: 100px; height: 100px;"></div>--%>
                <asp:Label ID="lblMTTR2" runat="server" CssClass="sapMNCValueScr" />
            </div>
        </div>
    </div>
</div>
<div class="tile-size-two grid-item">
    <div class="content">
        <div class="details">

            <div class="desc">
                <asp:Label ID="Label14" Text="First Time Right for Warranty Service" runat="server" />
            </div>
            <div class="details-position">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Dashboard/FirstTimeRightForWarrantyService.aspx">
                    <asp:Label ID="lblFTRWS" runat="server" CssClass="sapMNCValueScr" />
                </asp:HyperLink>
            </div>
        </div>
    </div>
</div>
<div class="tile-size-two grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label16" Text="Warranty Parts Availability With Dealers" runat="server" />
            </div>
            <div class="details-position">
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Dashboard/WarrantyMaterialAnalysis.aspx">
                    <asp:Label ID="lblWPAD" runat="server" CssClass="sapMNCValueScr" />
                </asp:HyperLink>
            </div> 
        </div>
    </div>
</div>

<asp:HiddenField ID="hfMTTR1" runat="server" />
<asp:HiddenField ID="hfLongitude" runat="server" />
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
<script type="text/javascript">
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {
        
        var hfMTTR1 = document.getElementById('MainContent_ucCustomerSatisfactionInAfterSalesSupport_hfMTTR1');
        var data = google.visualization.arrayToDataTable([
            ['Effort', 'Amount given'],
            ['My all', hfMTTR1.value],
            ['My ', 100 - hfMTTR1.value],
        ]);
        var options = {
            pieHole: 0.5,
            pieSliceTextStyle: {
                color: 'black',
            },
            legend: 'none'
        };
        var chart = new google.visualization.PieChart(document.getElementById('donut_single'));
        chart.draw(data, options);
    }
</script>
 