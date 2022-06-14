<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSatisfactionInAfterSalesSupport.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.CustomerSatisfactionInAfterSalesSupport" %>


<style>
    .sapMNCValueScr {
        overflow: hidden;
        color: #e78c07;
        font-size: 2.25rem;
        float: left;
        margin-top: 43px;
    }
</style>
<div class="tile-size-two grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Mean Time to Respond (MTTR-1) < 8 Hrs" runat="server" />
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
                <asp:Label ID="Label12" Text="Mean Time to Restore (MTTR-2) < 48 Hrs" runat="server" />
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
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/DMS_DashFirstTimeRightForWarrantyService.aspx">
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
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/DMS_DashWarrantyMaterialAnalysis.aspx">
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
        debugger;
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
<%--<div class="modbox">
    <div class="modboxin">
        <div class="portlet-body" style="padding: 5px;">
            <div id="divLeadStatistics2" class="row no-margin" style="font-size: medium; text-align: right;">
                <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                    <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                        <div class="visual"><i class="fa fa-ticket"></i></div>
                        <div class="details" style="color: white;">
                            <asp:Label ID="lblMTTR2" runat="server" Font-Size="20px" />
                            <asp:Label ID="Label7" runat="server" Text="%" Font-Size="20px" />
                            <div class="desc">
                                <asp:Label ID="Label2" Text="Mean Time to Restore (MTTR-2) < 48 Hrs" runat="server" Font-Size="20px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="modbox">
    <div class="modboxin">
        <div class="portlet-body" style="padding: 5px;">
            <div id="divLeadStatistics3" class="row no-margin" style="font-size: medium; text-align: right;">
                <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                    <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                        <div class="visual"><i class="fa fa-ticket"></i></div>
                        <div class="details" style="color: white;">
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DMS_DashFirstTimeRightForWarrantyService.aspx">
                                <asp:Label ID="lblFTRWS" runat="server" Font-Size="20px" />
                            </asp:HyperLink>
                            <asp:Label ID="Label5" runat="server" Text="%" Font-Size="20px" />
                            <div class="desc">
                                <asp:Label ID="Label4" Text="First Time Right for Warranty Service" runat="server" Font-Size="20px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="modbox">
    <div class="modboxin">
        <div class="portlet-body" style="padding: 5px;">
            <div id="divLeadStatistics4" class="row no-margin" style="font-size: medium; text-align: right;">
                <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                    <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                        <div class="visual"><i class="fa fa-ticket"></i></div>
                        <div class="details" style="color: white;">
                             <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DMS_DashWarrantyMaterialAnalysis.aspx">
                                <asp:Label ID="lblWPAD" runat="server" Font-Size="20px" /> 
                            </asp:HyperLink> 
                            <asp:Label ID="Label3" runat="server" Text="%" Font-Size="20px" />
                            <div class="desc">
                                 <asp:Label ID="Label6" Text="Warranty Parts Availability With Dealers" runat="server" Font-Size="20px" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>--%> 
