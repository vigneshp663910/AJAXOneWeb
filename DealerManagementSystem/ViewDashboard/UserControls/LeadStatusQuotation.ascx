<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusQuotation.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusQuotation" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <asp:Label ID="lblQuotation" runat="server" Text="0"></asp:Label>
            <div class="desc">
                <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Quotation</asp:LinkButton>
            </div>
        </div>
    </div>
</div>