<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusOpen.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusOpen" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <asp:Label ID="lblOpen" runat="server" Text="0"></asp:Label>
            <div class="desc">
                <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Open</asp:LinkButton>
            </div>
        </div>
    </div>
</div>
