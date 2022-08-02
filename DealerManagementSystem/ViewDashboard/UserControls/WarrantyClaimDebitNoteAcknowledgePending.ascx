<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WarrantyClaimDebitNoteAcknowledgePending.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.WarrantyClaimDebitNoteAcknowledgePending" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Debit Note Acknowledge Pending" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" OnClick="lbActions_Click">Open</asp:LinkButton>--%>
            </div>
            <div class="details-position"> 
                <asp:Label ID="lblOpen" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
               <%-- <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">
                    
                </asp:LinkButton>--%>
            </div>
        </div>
    </div>
</div>