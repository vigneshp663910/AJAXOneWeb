<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SignOut.aspx.cs" Inherits="DealerManagementSystem.Account.SignOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <div class="form-group">
        <div class="col-sm-12 text-center">
            <h5 style="color: maroon; font-size: 20px;">Are you sure you want to logout ?
                        </h5>
        </div>
    </div>
    <div class="form-group">
        <br>
   
        <div class="col-sm-12 text-center">       
           <%-- <a id="btnYes" title="Click Yes to Logout!" class="btn btn-primary" href="/Account/SignOut.htm">Yes</a>
            <a id="btnNo"  title="Click No if you don't want to Logout!" class="btn btn-danger" href="../Home.aspx">No</a> --%>
            <asp:Button ID="btnYes" runat="server" CssClass="btn Save" Text="Yes" title="Click Yes to Logout!" OnClick="btnLogOutYes_Click"></asp:Button>
            <asp:Button ID="btnNo" runat="server" CssClass="btn Reject" Text="No" title="Click No if you don't want to Logout!"  OnClick="btnLogOutNo_Click"></asp:Button>       
        </div>
    </div>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
</asp:Content>
