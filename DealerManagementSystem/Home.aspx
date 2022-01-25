<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DealerManagementSystem.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
     
        #div1 {
            height: 91.9vh;        
            display: flex;
            flex-direction: column;
            overflow: hidden;
            margin-left: 1px;
            background: skyblue;
            background: linear-gradient(to right, #4e97d5, #30526f );
            scroll
        }

        @media screen and (min-device-width: 320px) and (max-device-width: 720px) {

            #div1 {
                height: 93.2vh;
                /* margin-left: 0px;*/
            }
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="div1">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Ajax/Images/bg01.jpg" Height="100%" />
    </div>

</asp:Content>
