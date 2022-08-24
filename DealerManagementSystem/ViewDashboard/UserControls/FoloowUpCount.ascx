<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FoloowUpCount.ascx.cs" Inherits="DealerManagementSystem.ViewDashboard.UserControls.FoloowUpCount" %>
<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label1" Text="Today's Follow Up" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" OnClick="lbActions_Click">Open</asp:LinkButton>--%>
            </div>
            <div class="details-position"> 
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lbActions_Click">
                    <asp:Label ID="lblTodaysFollowUpCount" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label3" Text="Tomorrow's Follow Up" runat="server" />
                <%--  <asp:LinkButton ID="lbtnNewlyCreated" runat="server" OnClick="lbActions_Click">Open</asp:LinkButton>--%>
            </div>
            <div class="details-position"> 
                <asp:LinkButton ID="lnkBtnTomorrowsFollowUpCount" runat="server" OnClick="lbActions_Click">
                    <asp:Label ID="lblTomorrowsFollowUpCount" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>

<div class="tile-size-one grid-item">
    <div class="content">
        <div class="details">
            <div class="desc">
                <asp:Label ID="Label2" Text="Future 7 days Follow Up" runat="server" />
                <%--<asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">Quotation</asp:LinkButton>--%>
            </div> 
            <div class="details-position"> 
                <asp:LinkButton ID="lbtnNewlyCreated" runat="server" Style="color: white;" OnClick="lbActions_Click">
                    <asp:Label ID="lblFuture7DaysFollowUpCount" runat="server" Text="0" CssClass="sapMNCValueScr"></asp:Label>
                </asp:LinkButton>
            </div>
        </div>
    </div>
</div>
