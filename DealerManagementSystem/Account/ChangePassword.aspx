<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DealerManagementSystem.Account.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Admin</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <asp:ImageButton ID="ibtnPhoto" ImageUrl="~/Images/ChangePw.jpg" runat="server" Width="60px" Height="55px" />
                    </div>
                    <div class="col-md-9"></div>

                    <div class="col-md-3 text-right">
                        <label>Current Password</label>
                    </div>
                    <div class="col-md-2">                
                         <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" Width="100%" TextMode="Password"/>
                    </div>
                    <div class="col-md-7">
                    </div>

                    <div class="col-md-3 text-right">
                        <label>New Password</label>
                    </div>
                    <div class="col-md-2">           
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" Width="100%" TextMode="Password"/>
                    </div>
                    <div class="col-md-7">
                    </div>

                    <div class="col-md-3 text-right">
                        <label>Re-Type New Password</label>
                    </div>
                    <div class="col-md-2">                     
                        <asp:TextBox ID="txtReTypeNewPassword" runat="server" CssClass="form-control" Width="100%" TextMode="Password" />
                    </div>
                    <div class="col-md-7">
                        <div class="col-md-2 text-right ">
                            <asp:Button ID="btnChangePw" runat="server" CssClass="btn Search" Text="Change" ></asp:Button>
                        </div>
                    </div>

                </div>
            </fieldset>

        </div>
    </div>
</asp:Content>
