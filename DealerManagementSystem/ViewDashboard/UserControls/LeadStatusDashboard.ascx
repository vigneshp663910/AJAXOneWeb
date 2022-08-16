<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadStatusDashboard.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.LeadStatusDashboard" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Lead Status Open" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" OnClick="lbActions_Click">Open</asp:LinkButton>--%>
            </div>
            <div class="details-position"> 
                <asp:LinkButton ID="lbtnLeadStatusOpen" runat="server" OnClick="lbActions_Click">
                    <asp:Label ID="lblOpen" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label2" Text="Lead Status Assigned" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Assigned</asp:LinkButton>--%>
            </div>
            
            <div class="details-position"> 
                <asp:LinkButton ID="lbtnLeadStatusAssigned" runat="server"   OnClick="lbActions_Click">
                    <asp:Label ID="lblAssigned" runat="server" Text="0"  CssClass="sapMNCValueScr"></asp:Label>
                    </asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label3" Text="Lead Status Quotation" runat="server" />
                <%--<asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Quotation</asp:LinkButton>--%>
            </div>

            <div class="details-position"> 
                <asp:LinkButton ID="lbtnLeadStatusQuotation" runat="server" Style="color: white;" OnClick="lbActions_Click">
                    <asp:Label ID="lblQuotation" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
