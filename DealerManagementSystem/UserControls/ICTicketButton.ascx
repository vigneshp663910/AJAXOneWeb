<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ICTicketButton.ascx.cs" Inherits="DealerManagementSystem.UserControls.ICTicketButton" %>
<asp:Label ID="lblICTicketID" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Button ID="Button1" runat="server" Text="IC Ticket Manage" CssClass="InputButton" OnClick="Button1_Click"/>
<asp:Button ID="btnTechnicianAssign" runat="server" Text="Technician Assign" CssClass="InputButton" OnClick="btnTechnicianAssign_Click" />
<asp:Button ID="btnServiceConfirmation" runat="server" Text="Service Confirmation" CssClass="InputButton" OnClick="btnServiceConfirmation_Click" />
<asp:Button ID="btnNote" runat="server" Text="Note" CssClass="InputButton" OnClick="btnNote_Click" />
<asp:Button ID="btnServiceCharge" runat="server" Text="Service Charge" CssClass="InputButton" OnClick="btnServiceCharge_Click" />
<asp:Button ID="btnMaterialCharge" runat="server" Text="Material Charge" CssClass="InputButton" OnClick="btnMaterialCharge_Click" />

