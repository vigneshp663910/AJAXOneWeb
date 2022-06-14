<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusOpen.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusOpen" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Lead Status Open" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" OnClick="lbActions_Click">Open</asp:LinkButton>--%>
            </div>
            <div class="details-position"> 
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">
                    <asp:Label ID="lblOpen" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>



