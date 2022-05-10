<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SignOut.aspx.cs" Inherits="DealerManagementSystem.Account.SignOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group" style="margin-top: 100px">
        <div class="col-sm-12 text-center">
            <h5 style="color: maroon; font-size: 20px;">Are you sure you want to logout ?
            </h5>
        </div>
    </div>
    <div class="form-group">

        <div class="col-sm-12 text-center">
            <%-- <a id="btnYes" title="Click Yes to Logout!" class="btn btn-primary" href="/Account/SignOut.htm">Yes</a>
            <a id="btnNo"  title="Click No if you don't want to Logout!" class="btn btn-danger" href="../Home.aspx">No</a> --%>
            <asp:Button ID="btnYes" runat="server" CssClass="btn Save" Text="Yes" title="Click Yes to Logout!" OnClick="btnLogOutYes_Click"></asp:Button>
            <asp:Button ID="btnNo" runat="server" CssClass="btn Reject" Text="No" title="Click No if you don't want to Logout!" OnClick="btnLogOutNo_Click"></asp:Button>
        </div>

        <div class="col-sm-12 text-center" style="margin-top: 60px">
            <!--<i class="fa fa-fw fa-comment-o" style="color: lightgray"></i>-->
            <img src="/Ajax/images/feedback.png" height="30" width="30"><br />
            <a href="https://ajaxone.ajax-engg.com">Your Feedback Matters</a>
        </div>

    </div>
    
</asp:Content>
