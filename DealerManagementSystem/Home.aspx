<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DealerManagementSystem.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        #grad1 {
           /* height: 900px;*/
           /* background-color: red;  For browsers that do not support gradients 
             background-image: conic-gradient(red, yellow, green, blue, black);
             background-image: conic-gradient(#4682B4, #4682B4, white, #4682B4, #008080);
             background-image: conic-gradient( #4682B4,  #48D1CC, white,  #4682B4, #008080);*/
             /* background-image: conic-gradient( #4682B4,  #2F4F4F, white,  #4682B4, #2F4F4F);*/
           /* background-image: conic-gradient( #4682B4, #2F4F4F, white, #4682B4, #2F4F4F);*/
           /*background-image: linear-gradient(to right, #30526f , #4e97d5);*/
           /* background-image: radial-gradient(  white,#30526f);*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%--<div id="grad1" style="width: 100%; height: 200%; background-repeat: repeat-y; background-color: #1C8DFF;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Ajax/Images/bg05q.png" Width="100%" Height="200%"/>     
    </div>--%>

    <div id="grad1" style="width: 100%; height: 100%; background-repeat: repeat-y;">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Ajax/Images/bg01.jpg" Width="100%" Height="200%"/>     
    </div>

    <%--<div id="grad1">
    </div>--%>
</asp:Content>
