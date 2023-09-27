<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketEscalationOnBreakdownCount.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.ICTicketEscalationOnBreakdownCount" %>

<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Escalation report on Break Down More than 8 Hrs" runat="server" />
            </div>
            <div class="details-position">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ViewService/ICTicketEscalationOnBreakdown.aspx">
                    <asp:Label ID="lblBreakDown8" runat="server" CssClass="sapMNCValueScr" />
                </asp:HyperLink>
            </div>
        </div>

    </div>
</div>
<div class="tile-size-two grid-item">
    <div class="content">
        <%--<div class="dashboard-stat dashboard-stat-v2 purple-intense" href="javascript:void(0);" onclick="VisitMyEnquiries('');">--%>
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label2" Text="Escalation report on Break Down more than 24 Hrs" runat="server" />
            </div>
            <div class="details-position">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/ViewService/ICTicketEscalationOnBreakdown.aspx">
                    <asp:Label ID="lblBreakDown24" runat="server" CssClass="sapMNCValueScr" />
                </asp:HyperLink>
            </div>
        </div>
    </div>
</div>
<div class="tile-size-two grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label4" Text="Escalation report on Break Down More than 48 Hrs" runat="server" />
            </div>
            <div class="details-position">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/ViewService/ICTicketEscalationOnBreakdown.aspx">
                    <asp:Label ID="lblBreakDown48" runat="server" CssClass="sapMNCValueScr" />
                </asp:HyperLink>
            </div>
        </div>
    </div>
</div>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label6" Text="Escalation report on Break Down More than 72 Hrs" runat="server" />
            </div>
            <div class="details-position">
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/ViewService/ICTicketEscalationOnBreakdown.aspx">
                    <asp:Label ID="lblBreakDown72" runat="server"  CssClass="sapMNCValueScr" />
                </asp:HyperLink> 
            </div>
        </div>
    </div>
</div>



