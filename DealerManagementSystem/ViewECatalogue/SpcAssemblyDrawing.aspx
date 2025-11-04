<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcAssemblyDrawing.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcAssemblyDrawing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* horizontal panel*/
        .panel-container {
            display: flex;
            flex-direction: row;
            overflow: hidden;
            height: 96%;
        }

        .panel-left {
            flex: 0 0 auto; /* only manually resize */
            width: 20%;
            max-width: 40%;
            min-height: 200px;
            min-width: 15px;
            white-space: nowrap;
            background: #838383;
            color: white;
        }

        .splitter {
            flex: 0 0 auto;
            width: 10px;
            background: url('/Images/vsizegrip.png') center center no-repeat #838383;
            min-height: 200px;
            cursor: col-resize;
        }

        .panel-right {
            flex: 1 1 auto; /* resizable */
            width: 100%;
            min-height: 200px;
            min-width: 200px !important; /* NOTE: This won't be respected! Explicit size of left forces the size to be fixed */
            background: #eee;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
    <div class="panel-container">
        <div class="panel-left">
            <%--<div id="scrollDiv1" style="height: 88vh; border: 2px solid #333; padding: 10px; overflow: auto;">--%>
                <iframe id="iframe_Model" src="SpcModelTree" style="width: 100%; height: 100%; border: none"></iframe>
            <%--</div>--%>
        </div>

        <div class="splitter" style="height: 88vh;">
        </div>

        <div class="panel-right" style="padding: 0;">
            <iframe id="iframe_Drawing" src="SpcAssemblyDrawingView" style="width: 100%; height: 100%; border: none"></iframe>
        </div>

    </div>
     
    <script>
        function RefreshIframe(as_Criteria) { 
            var iframe1 = document.getElementById('iframe_Drawing');
            iframe1.src = 'SpcAssemblyDrawingView?SpcAssemblyID=' + as_Criteria;

        }
    </script>


    <script src="jquery-resizable.js"></script>
    <script>
        $(".panel-left").resizable({
            handleSelector: ".splitter",
            resizeHeight: false
        });

        $(".panel-top").resizable({
            handleSelector: ".splitter-horizontal",
            resizeWidth: false
        });


    </script>




</asp:Content>
