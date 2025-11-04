<%@ Page Title="" Language="C#" MasterPageFile="~/Dealer.Master" AutoEventWireup="true" CodeBehind="SpcAssemblyParts.aspx.cs" Inherits="DealerManagementSystem.ViewECatalogue.SpcAssemblyParts" %>

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
            max-width: 70%;
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
    <style>
        .tooltip-box {
            position: absolute;
            background-color: white;
            color: #fff;
            padding: 5px 10px;
            border-radius: 4px;
            white-space: nowrap;
            pointer-events: none;
            opacity: 0;
            transition: opacity 0.2s;
            font-size: 13px;
            z-index: 9999;
        }

            .tooltip-box .testMatDesc {
                font-weight: bold;
                font-size: medium;
                color: blue;
                font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
            }

            .tooltip-box .Test {
                font-size: medium;
                color: black;
                font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
            }

            .tooltip-box .Value {
                font-size: medium;
                color: blue;
                font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
            }

            .tooltip-box .PriceTest {
                font-size: medium;
                color: black;
                font-family: "boschsans", "Helvetica Neue", Helvetica, Arial, sans-serif;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Product Group</label>
            <asp:DropDownList ID="ddlProductGroup" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProductGroup_SelectedIndexChanged" AutoPostBack="true" />
        </div>
        <div class="col-md-2 col-sm-12">
            <label class="modal-label">Model / PM Code</label>
            <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" />
        </div>
        <div class="col-md-2 col-sm-12">
            <label>Assembly</label>
            <asp:DropDownList ID="ddlAssembly" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" />
        </div>

    </div>
    <div class="panel-container">

        <div class="panel-left"  style="background: white">

            <%--<div id="scrollDiv1" style="height: 88vh; border: 2px solid #333; padding: 10px; overflow: auto;">--%>
            <iframe id="iframe_PartsList" runat="server" style="width: 100%; height: 100%; border: none"></iframe>
            <%--</div>--%>
        </div>

        <div class="splitter" style="height: 77vh;">
        </div>

        <div class="panel-right" style="padding: 0;background: white">

            <div class="col-md-12">
                <div class="model-scroll">
                    <div id="container">
                        <div id="tooltip" class="tooltip-box" style="border: 2px solid green;">Dynamic Tooltip</div>
                        <asp:Image ID="imgAssemblyImage" runat="server" onmouseout="clearCoor()" onclick="javascript:ClickOnImage();" Height="510" Width="900" GFG="250" />

                        <img id="CallOut" src="" alt="" height="50" width="275" style="visibility: hidden" />
                        <p id="copno" style="font-family: Tahoma; font-size: xx-small; color: #0000FF"></p>
                        <p id="copdesc" style="font-family: Tahoma; font-size: xx-small; color: #0000FF; background-color: #D6D6D6;"></p>
                    </div>
                </div>
                <script type="text/javascript"> 
                    var tooltip = document.getElementById('tooltip');
                    var target = document.getElementById('<%= imgAssemblyImage.ClientID %>');

                    target.addEventListener('mousemove', function (e) {
                        mousemove(e);
                    });

                    function mousemove(e) {
                        var rect = target.getBoundingClientRect();

                        var x = Math.round(event.clientX - rect.left);
                        var y = Math.round(event.clientY - rect.top);
                        let tooltip1 = document.querySelector(".tooltip-box"); // select your tooltip
                        let rect1 = tooltip1.getBoundingClientRect();

                        if (x < 500) {
                            //alert(tooltip.style.left);
                            tooltip.style.left = (x + 15) + 'px';
                            //  alert(tooltip.style.left);
                        }
                        else {
                            tooltip.style.left = (x - 15 - rect1.width) + 'px';
                        }

                        if (y < 300) {
                            tooltip.style.top = (y + 15) + 'px';
                        }
                        else {
                            tooltip.style.top = (y - 15 - rect1.height) + 'px';
                        }
                        tooltip.style.opacity = 1;

                        var iframe = document.getElementById("MainContent_iframe_PartsList");
                        var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;


                        //var grid = document.getElementById('gvParts');
                        //var rows = grid.getElementsByTagName('tr');

                        var grid = iframeDoc.getElementById("gvParts");
                        var rows = grid.getElementsByTagName("tr");

                        var rowCount = rows.length - 1;
                        var TootText = '';
                         var xyBulkUpdate = iframe.contentWindow.xyBulkUpdate;

                        if (xyBulkUpdate == 0) {
                            for (var i = 0; i < rowCount; i++) {
                                var lblX_CoOrdinate = iframeDoc.getElementById('gvParts_lblX_CoOrdinate_' + i);
                                var lblY_CoOrdinate = iframeDoc.getElementById('gvParts_lblY_CoOrdinate_' + i);
                                var X_CoOrdinate = parseInt(lblX_CoOrdinate.innerText);
                                var Y_CoOrdinate = parseInt(lblY_CoOrdinate.innerText);
                                if (X_CoOrdinate >= x - 10 && X_CoOrdinate <= x + 10 && Y_CoOrdinate >= y - 10 && Y_CoOrdinate <= y + 10) {

                                    var lblNumber = iframeDoc.getElementById('gvParts_lblNumber_' + i);
                                    var lblFlag = iframeDoc.getElementById('gvParts_lblFlag_' + i);
                                    var lblMaterial = iframeDoc.getElementById('gvParts_lblMaterial_' + i);
                                    var lblMaterialDescription = iframeDoc.getElementById('gvParts_lblMaterialDescription_' + i);
                                    var lblQty = iframeDoc.getElementById('gvParts_lblQty_' + i);
                                    TootText = "<br /><span class='testMatDesc'>" + lblMaterialDescription.innerText + "</span> <br />"
                                        + "<br /> <span class='Test'> SP Number : </span><span class='Value'>" + lblMaterial.innerText + "</span>"
                                        + "<br /> <span class='Test' >Position :</span><span class='Value'>" + lblNumber.innerText + "</span>"
                                        + "<br /> <span class='Test'> Alt :</span> <span class='Value'>" + lblFlag.innerText + "</span>"

                                        + "<br /> <span class='PriceTest'> Qty :</span><span class='Value'>" + lblQty.innerText;
                                    break;
                                }
                            }
                            tooltip.innerHTML = TootText
                                + "<br /><span class='Test'>e.pageX: </span><span class='Value'>" + e.pageX + "</span>"
                                + "<br /><span class='Test'>e.pageX: </span><span class='Value'>" + e.pageX + "</span>"
                                + "<br /><span class='Test'>X: </span><span class='Value'>" + x + "</span>"
                                + "<br /><span class='Test'>Y: </span><span class='Value'>" + y + "</span>";
                        }
                        else {
                            for (var i = 0; i < rowCount; i++) {
                                var rbParts = iframeDoc.getElementById('gvParts_rbParts_' + i);
                                if (rbParts.checked == false) {
                                    var lblNumber = iframeDoc.getElementById('gvParts_lblNumber_' + i);
                                    var lblFlag = iframeDoc.getElementById('gvParts_lblFlag_' + i);
                                    var lblMaterial = iframeDoc.getElementById('gvParts_lblMaterial_' + i);
                                    var lblMaterialDescription = iframeDoc.getElementById('gvParts_lblMaterialDescription_' + i);
                                    var lblQty = iframeDoc.getElementById('gvParts_lblQty_' + i);
                                    TootText = "<br /> <span class='Test' >Position :</span><span  style='font-size: 50px; color: blue;'>" + lblNumber.innerText + "</span>"
                                        + "<br /><span class='testMatDesc'>" + lblMaterialDescription.innerText + "</span> <br />"
                                        + "<br /> <span class='Test'> SP Number : </span><span class='Value'>" + lblMaterial.innerText + "</span>"
                                        + "<br /> <span class='Test'> Alt :</span> <span class='Value'>" + lblFlag.innerText + "</span>"

                                        //  + "<br />Mat Des : " + lblMaterialDescription.innerText

                                        + "<br /> <span class='PriceTest'> Qty :</span><span class='Value'>" + lblQty.innerText
                                        ;

                                    tooltip.innerHTML = TootText
                                        + "<br /><span class='Test'>X: </span><span class='Value'>" + x + "</span>"
                                        + "<br /><span class='Test'>Y: </span><span class='Value'>" + y + "</span>";
                                    break;
                                }
                            }
                        }

                    };

                    target.addEventListener('mouseleave', function () {
                        tooltip.style.opacity = 0;
                    });

                    function clearCoor() {
                        // document.getElementById("UC_SpcAssemblyDView_lblXY").innerHTML = "";
                    }

                    //CallOut(120, 80, "Part No: 0740.000040\nDescription: CIRCLIP EXTERNAL");
                    function CallOut(x, y, ls_pno, ls_pdesc) {
                        var imgCallOut = document.getElementById("CallOut");
                        imgCallOut.style.visibility = 'visible';
                        imgCallOut.src = '../Images/PopsPartDetails.png';

                        var rect = target.getBoundingClientRect();
                        //x = Math.round(x - rect.left);
                        //y = Math.round(y - rect.top);

                        //xCoor = x;
                        //yCoor = y + 18;
                        xCoor = x - 10;
                        yCoor = y + 15;

                        var newImg1 = $(imgCallOut);
                        newImg1.css({ position: "absolute", left: xCoor, top: yCoor });

                        copono = document.getElementById("copno");
                        copono.innerHTML = ls_pno;

                        copodesc = document.getElementById("copdesc");
                        copodesc.innerHTML = ls_pdesc;

                        var newTxtP = $(copono);
                        newTxtP.css({ position: "absolute", left: xCoor + 90, top: yCoor + 15 });
                        $("#container").append(newTxtP);

                        var newTxtPd = $(copodesc);
                        newTxtPd.css({ position: "absolute", left: xCoor + 90, top: yCoor + 30 });
                        $("#container").append(newTxtPd);

                    }
                </script>
            </div>
        </div>
    </div>

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

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        function ClickOnImage() {

            debugger;
            var img = document.getElementById("MainContent_imgAssemblyImage");
            var rect = img.getBoundingClientRect();
            var x = Math.round(event.clientX - rect.left);
            var y = Math.round(event.clientY - rect.top);

            var iframe = document.getElementById("MainContent_iframe_PartsList");
            var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;

            var grid = iframeDoc.getElementById("gvParts");
            var rows = grid.getElementsByTagName("tr");

            var xyUpdate = iframe.contentWindow.xyUpdate;
            var xyBulkUpdate = iframe.contentWindow.xyBulkUpdate;
          

            //var grid = document.getElementById('UC_SpcAssemblyDView_gvParts');
            //var rows = grid.getElementsByTagName('tr');
            var hdnUpdatedIDs = iframeDoc.getElementById("hdnUpdatedIDs");
            var hdnX = iframeDoc.getElementById("hdnX");
            var hdnY = iframeDoc.getElementById("hdnY");

            var rowCount = rows.length - 1;
            if (xyUpdate == 1) {

                for (var i = 0; i < rowCount; i++) {
                    var rbParts = iframeDoc.getElementById('gvParts_rbParts_' + i);
                    if (rbParts.checked) {

                        rbParts.disabled = true;
                        var lblX_CoOrdinate = iframeDoc.getElementById('gvParts_lblX_CoOrdinate_' + i);
                        var lblY_CoOrdinate = iframeDoc.getElementById('gvParts_lblY_CoOrdinate_' + i);
                        var lblID = iframeDoc.getElementById('gvParts_lblSpcAssemblyPartsCoOrdinateID_' + i);


                       

              <%--   var hdnUpdatedIDs = document.getElementById('<%= hdnUpdatedIDs.ClientID %>');
                     var hdnX = document.getElementById('<%= hdnX.ClientID %>');
                     var hdnY = document.getElementById('<%= hdnY.ClientID %>');    --%>

                        hdnUpdatedIDs.value = hdnUpdatedIDs.value + ',' + lblID.innerText;
                        hdnX.value = hdnX.value + ',' + x;
                        hdnY.value = hdnY.value + ',' + y;

                        lblX_CoOrdinate.innerText = x;
                        lblY_CoOrdinate.innerText = y;
                    }
                }
            }
            else if (xyBulkUpdate == 1) {
            <%-- var hdnUpdatedIDs = document.getElementById('<%= hdnUpdatedIDs.ClientID %>');
             var hdnX = document.getElementById('<%= hdnX.ClientID %>');
             var hdnY = document.getElementById('<%= hdnY.ClientID %>');--%>
                
            

                if (hdnX.value !== "") {
                    debugger;
                    let lastX = hdnX.value.split(",").pop();
                    let lastY = hdnY.value.split(",").pop();
                    if (lastX >= x - 5 && lastX <= x + 5 && lastY >= y - 5 && lastY <= y + 5) {
                        alert("Kindly verify whether the last XY coordinate aligns with the current point.");
                        return;
                    }
                }
                for (var i = 0; i < rowCount; i++) {
                    var rbParts = iframeDoc.getElementById('gvParts_rbParts_' + i);
                    if (rbParts.checked == false) {
                        rbParts.checked = true;
                        var lblX_CoOrdinate = iframeDoc.getElementById('gvParts_lblX_CoOrdinate_' + i);
                        var lblY_CoOrdinate = iframeDoc.getElementById('gvParts_lblY_CoOrdinate_' + i);
                        var lblID = iframeDoc.getElementById('gvParts_lblSpcAssemblyPartsCoOrdinateID_' + i);

                        hdnUpdatedIDs.value = hdnUpdatedIDs.value + ',' + lblID.innerText;
                        hdnX.value = hdnX.value + ',' + x;
                        hdnY.value = hdnY.value + ',' + y;

                        lblX_CoOrdinate.innerText = x;
                        lblY_CoOrdinate.innerText = y;
                        mousemove(event);
                        break;
                    }
                }
            }
            else {
                for (var i = 0; i < rowCount; i++) {
                    var lblX_CoOrdinate = iframeDoc.getElementById('gvParts_lblX_CoOrdinate_' + i);
                    var lblY_CoOrdinate = iframeDoc.getElementById('gvParts_lblY_CoOrdinate_' + i);
                    var X_CoOrdinate = parseInt(lblX_CoOrdinate.innerText);
                    var Y_CoOrdinate = parseInt(lblY_CoOrdinate.innerText);

                    if (X_CoOrdinate >= x - 10 && X_CoOrdinate <= x + 10 && Y_CoOrdinate >= y - 10 && Y_CoOrdinate <= y + 10) {
                        var cbPart = iframeDoc.getElementById('gvParts_cbParts_' + i);
                        cbPart.checked = true;
                        return;
                    }
                }
            }
        }
    </script>
</asp:Content>
