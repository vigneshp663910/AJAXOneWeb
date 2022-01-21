<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="HomeWithTiles.aspx.cs" Inherits="DealerManagementSystem.HomeWithTiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <style>

         /*.html{

             background-color:black;
         }*/

         #div1 {
            height: 89.5vh;
            display: flex;
            flex-direction: column;
            background: skyblue;
            overflow:hidden;
           
        }

        #div2 {
            flex-grow: 1;
            background: #cddbe7;
           /* height:10vh;*/
           font-size:medium;
        }
       
        .container {
            height: 100%;       
            position: relative;
            top: 20px;
            /* left: 10px;*/
            float: left;
            border: thick;
            border-color: red;
        }

        .first {
            float: left;
            width: 20%;
            height: 30%;
            background-color: #de5716;
        }

        .second {
            float: left;
            width: 20%;
            height: 40%;
            background-color: #02bde1;
        }

        .third {
            float: right;
            width: 80%;
            height: 50%;
            background-color: #db9b21;
        }

        .fourth {
            float: right;
            width: 40%;
            height: 20%;
            background-color: #024d92;
              color: lightgray;
        }

        .last {
            float: right;
            width: 40%;
            height: 20%;
            background-color: #60b912;
        }

        .first, .second, .third, .fourth, .last {           
            border: solid;
            border-color: skyblue;
        }
   
    </style>

    <div id="div1">

        <div class="container">
            <div class="first">
                <p>first first </p>

            </div>

            <div class="third">
                <p>third</p>

            </div>

            <div class="second">
                <p>second</p>

            </div>

            <div class="last">
                <p>last</p>

            </div>

            <div class="fourth">
                <p>fourth</p>

            </div>

        </div>
    </div>
    <div id="div2">Ready...</div>
</asp:Content>
