<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSatisfactionInAfterSalesSupport.ascx.cs" Inherits="DealerManagementSystem.Dashboard.CustomerSatisfactionInAfterSalesSupport" %>
<div class="modbox">
    <div class="modtitle">
        <asp:Literal ID="ucTitle" runat="server" Text="Customer Satisfaction in After Sales Support"></asp:Literal>
    </div>
    <div class="modboxin">
        <div class="dashboardGrid">
            <table>
                <tr style="height: 40px">
                    <td>
                        <asp:Label ID="Label1" Text="Mean Time to Respond (MTTR-1) < 6 Hrs" runat="server" Font-Size="20px" /></td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:Label ID="lblMTTR1" runat="server" Font-Size="20px" /></td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="%" Font-Size="20px" /></td>
                </tr>
                <tr style="height: 40px">
                    <td>
                        <asp:Label ID="Label2" Text="Mean Time to Restore (MTTR-2) < 24 Hrs" runat="server" Font-Size="20px" /></td>
                    <td></td>
                    <td>
                        <asp:Label ID="lblMTTR2" runat="server" Font-Size="20px" /></td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="%" Font-Size="20px" /></td>
                </tr>
                <tr style="height: 40px">
                    <td>
                        <asp:Label ID="Label4" Text="First Time Right for Warranty Service" runat="server" Font-Size="20px" /></td>
                    <td></td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DMS_DashFirstTimeRightForWarrantyService.aspx">
                            <asp:Label ID="lblFTRWS" runat="server" Font-Size="20px" />
                        </asp:HyperLink></td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="%" Font-Size="20px" /></td>
                </tr>
                <tr style="height: 40px">
                    <td>
                        <asp:Label ID="Label6" Text="Warranty Parts Availability With Dealers" runat="server" Font-Size="20px" /></td>
                    <td></td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DMS_DashWarrantyMaterialAnalysis.aspx">
                            <asp:Label ID="lblWPAD" runat="server" Font-Size="20px" />

                        </asp:HyperLink></td>

                    <td>
                        <asp:Label ID="Label3" runat="server" Text="%" Font-Size="20px" /></td>
                </tr>
            </table>
        </div>
    </div>
</div>