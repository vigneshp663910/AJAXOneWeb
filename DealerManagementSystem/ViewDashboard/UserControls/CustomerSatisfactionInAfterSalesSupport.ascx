<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSatisfactionInAfterSalesSupport.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.CustomerSatisfactionInAfterSalesSupport" %>

<div class="modbox">
    <div class="modboxin">
        <div class="portlet-body" style="padding: 5px;">
            <div id="divLeadStatistics1" class="row no-margin" style="font-size: medium; text-align: right;">
                <div class="thumbnail wide_thumbnail" style="margin-bottom: 2px; padding-left: 10px; padding-right: 10px; margin-right: 5px; margin-left: 15px; background-color: #8775a7;">
                    <div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">
                        <div class="visual"><i class="fa fa-ticket"></i></div>
                        <div class="details" style="color: white;">
                            <asp:Label ID="lblMTTR1" runat="server" Font-Size="20px" />
                            <asp:Label ID="Label8" runat="server" Text="%" Font-Size="20px" />
                            <div class="desc">
                                <asp:Label ID="Label1" Text="Mean Time to Respond (MTTR-1) < 8 Hrs" runat="server" Font-Size="20px" />
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
</div> 
