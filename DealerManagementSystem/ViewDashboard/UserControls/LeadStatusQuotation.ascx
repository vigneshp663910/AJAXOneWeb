<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusQuotation.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusQuotation" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Lead Status Quotation" runat="server" />
                <%--<asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Quotation</asp:LinkButton>--%>
            </div>

            <div class="details-position"> 
                <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">
                    <asp:Label ID="lblQuotation" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
