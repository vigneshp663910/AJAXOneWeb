<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusAssigned.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusAssigned" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label2" Text="Lead Status Assigned" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Assigned</asp:LinkButton>--%>
            </div>
            
            <div class="details-position"> 
                <asp:LinkButton ID="lbtnNewlyCreated" runat="server"   OnClick="lbActions_Click">
                    <asp:Label ID="lblAssigned" runat="server" Text="0"  CssClass="sapMNCValueScr"></asp:Label>
                    </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
 
