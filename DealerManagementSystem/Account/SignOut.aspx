<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SignOut.aspx.cs" Inherits="DealerManagementSystem.Account.SignOut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="LnkComment"
        BackgroundCssClass="Background">
    </cc1:ModalPopupExtender>

    <div class="form-group" style="margin-top: 100px">
        <div class="col-sm-12 text-center">
            <h5 style="color: maroon; font-size: 20px;">Are you sure you want to logout ?
            </h5>
        </div>
    </div>
    <div class="form-group">

        <div class="col-sm-12 text-center">

            <asp:Button ID="btnYes" runat="server" CssClass="btn Save" Text="Yes" title="Click Yes to Logout!" OnClick="btnLogOutYes_Click"></asp:Button>
            <asp:Button ID="btnNo" runat="server" CssClass="btn Reject" Text="No" title="Click No if you don't want to Logout!" OnClick="btnLogOutNo_Click"></asp:Button>
        </div>

        <div class="col-sm-12 text-center" style="margin-top: 60px">
            <%-- <img src="/Ajax/images/feedback.png" height="30" width="30">--%><br />
            <h5><a href=""><i id="LnkComment" class="fa fa-fw fa-comment fa-3x" style="color: darkorange" runat="server"></i>
                <br />
                </a>Your Feedback Matters !</h5>
        </div>

    </div>



</asp:Content>

