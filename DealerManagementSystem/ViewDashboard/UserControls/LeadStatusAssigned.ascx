<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusAssigned.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusAssigned" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <asp:Label ID="lblAssigned" runat="server" Text="0"></asp:Label>
            <div class="desc">
                <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Assigned</asp:LinkButton>
            </div>
        </div>
    </div>
</div>