<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketEscalationOnBreakdownCount.ascx.cs" Inherits="DealerManagementSystem.Dashboard.ICTicketEscalationOnBreakdownCount" %>
<div class="modbox">
    <div class="modtitle">
        <asp:Literal ID="ucTitle" runat="server" Text="Escalation report on Break Down"></asp:Literal>
    </div>
    <div class="modboxin">
        <div class="dashboardGrid">
            <table>
                <tr style="height: 40px">
                    <td>
                        <asp:Label ID="Label1" Text="Escalation report on Break Down More than 8 Hrs" runat="server" Font-Size="20px" />
                    </td>
                    <td style="width: 20px"></td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DMS_ICTicketEscalationOnBreakdown.aspx">
                            <asp:Label ID="lblBreakDown8" runat="server" Font-Size="20px" />
                        </asp:HyperLink></td>
                </tr>
                <tr style="height: 40px">
                    <td>

                        <asp:Label ID="Label2" Text="Escalation report on Break Down more than 24 Hrs" runat="server" Font-Size="20px" />


                    </td>
                    <td></td>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DMS_ICTicketEscalationOnBreakdown.aspx">
                            <asp:Label ID="lblBreakDown24" runat="server" Font-Size="20px" />
                        </asp:HyperLink></td>
                </tr>
                <tr style="height: 40px">
                    <td>

                        <asp:Label ID="Label4" Text="Escalation report on Break Down More than 48 Hrs" runat="server" Font-Size="20px" />

                    </td>
                    <td></td>
                    <td>
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/DMS_ICTicketEscalationOnBreakdown.aspx">
                            <asp:Label ID="lblBreakDown48" runat="server" Font-Size="20px" />
                        </asp:HyperLink></td>
                </tr>
                <tr style="height: 40px">
                    <td>

                        <asp:Label ID="Label6" Text="Escalation report on Break Down More than 72 Hrs" runat="server" Font-Size="20px" />

                    </td>
                    <td></td>
                    <td>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/DMS_ICTicketEscalationOnBreakdown.aspx">
                            <asp:Label ID="lblBreakDown72" runat="server" Font-Size="20px" />
                        </asp:HyperLink></td>
                </tr>
            </table>
        </div>
    </div>
</div>
