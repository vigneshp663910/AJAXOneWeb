<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddFsrSignature.ascx.cs" Inherits="DealerManagementSystem.ViewService.UserControls.AddFsrSignature" %>

<style>
    .col-md-3   modalV
    {
        margin-top: -16px;
        margin-bottom: -14px;
    }
    input[type=checkbox], input[type=radio] {
    height: initial;
    margin: 0px;
}
</style>

<asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
<%--  <meta charset="utf-8"> 
        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="label" Width="100%" Font-Bold="true" Font-Size="24px" />
        <fieldset class="fieldset-border" id="Fieldset1" runat="server">
            <div class="col-md-12">
             
                <div id="divWeb" style="display: none">
                    <div class="col-md-6 col-sm-12">

                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                            <label class="modal-label">
                                Live Camera
                    <input type="button" id="btnCapture" value="Capture" />
                            </label>
                            <div id="webcam"></div>
                        </div>
                    </div>
                </div>
                <div id="divCapture" style="display: none">
                    <div class="col-md-12 col-sm-12">
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                            <img id="imgCapture" width="320" height="240" />
                            <input type="hidden" id="hfCapture" runat="server" />
                        </div>
                    </div>
                </div>
                <div id="divSign" style="display: none">
                    <div class="col-md-12 col-sm-12">
                        <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                            <label class="modal-label">
                                Name Of Signature
                        <asp:TextBox ID="txtNameOfSignature" runat="server"></asp:TextBox>
                            </label>
                            <div id="signature-pad" class="m-signature-pad">
                                <div class="m-signature-pad--body" style="background-color: #8080801f;">
                                    <canvas width="320" height="320"></canvas>
                                </div>
                                <input type="hidden" id="hfSign" runat="server" />
                            </div>
                        </div>
                    </div> 

                </div>
                <div class="col-md-12 text-center">
                    
                    <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="btn Save"  OnClick="btnSave_Click" />
                 

                     <input type="button" id="btnUpload" value="Upload" style="visibility: hidden" />
                   
                </div>
            </div>
        </fieldset>

        <div class="container-fluid">
        </div> 
--%>
<fieldset class="fieldset-border" id="Fieldset1" runat="server">
    <div class="col-md-12">
         <div class="col-md-3">
            <label class="modal-label"> Engineer Photo : 
            <asp:CheckBox ID="chVEngineerPhoto" runat="server" CssClass="LabelValue" Enabled ="false"></asp:CheckBox>  </label>
        </div>
         <div class="col-md-3">
            <label class="modal-label">Engineer Sign  : 
            <asp:CheckBox ID="chVEngineerSign" runat="server" CssClass="LabelValue" Enabled ="false"></asp:CheckBox></label>  
        </div>
         <div class="col-md-3">
            <label class="modal-label"> Customer Photo :  
            <asp:CheckBox ID="chVCustomerPhoto" runat="server" CssClass="LabelValue" Enabled ="false"></asp:CheckBox>  </label>
        </div>
         <div class="col-md-3">
            <label class="modal-label"> Customer Sign :  
            <asp:CheckBox ID="chVCustomerSign" runat="server" CssClass="LabelValue" Enabled ="false"></asp:CheckBox>  </label>
        </div> 
        <div class="col-md-12">
            <label></label>
            <asp:Label ID="lblProcessName" runat="server" CssClass="LabelValue"></asp:Label>
            <div style="display: none">
                <asp:Label ID="lblProcessID" runat="server"></asp:Label>
            </div>
        </div>
        <div id="divWeb" style="display: none">
            <div class="col-md-6 col-sm-12">
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                    <label class="modal-label">
                        Live Camera : 
                    <input type="button" id="btnCapture" value="Capture" cssclass="btn Save" />
                    </label>
                    <div id="webcam"></div>
                    <input type="hidden" id="hfTPhotoCapture" runat="server" />
                    <input type="hidden" id="hfCPhotoCapture" runat="server" />
                </div>
            </div>
        </div>
        <%-- <div id="divCapture" style="display: none">
            <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Captured Photo"></asp:Label>
                <img id="imgCapture" width="320" height="240" />
                <input type="hidden" id="hfCapture" runat="server" />
            </div>
        </div>--%>
        <div id="divTSign" style="display: none">
            <div class="col-md-6 col-sm-12">
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                    <%-- <label class="modal-label">
                        Name Of Signature
                        <asp:TextBox ID="txtNameOfSignature" runat="server"></asp:TextBox>
                    </label>--%>
                    <input type="button" id="btnTSign" value="Capture" cssclass="btn Save" />
                    <div id="signature-pad" class="m-signature-pad">
                        <div class="m-signature-pad--body" style="background-color: #8080801f;">
                            <canvas width="320" height="320"></canvas>
                        </div>
                    </div>
                    <input type="hidden" id="hfSign" runat="server" />
                    <input type="hidden" id="hfTSign" runat="server" />
                    <input type="hidden" id="hfCSign" runat="server" />
                </div>
            </div>
        </div>
        <div id="divCSign" style="display: none">
            <div class="col-md-6 col-sm-12">
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                    <input type="button" id="btnCSign" value="Capture" cssclass="btn Save" />
                    <div id="signature1-pad" class="m-signature-pad">
                        <div class="m-signature-pad--body" style="background-color: #8080801f;">
                            <canvas width="320" height="320"></canvas>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divNameOfSignature" style="display: none">
            <div class="col-md-6 col-sm-12">
                <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="max-width: 350px;">
                    <label class="modal-label">
                        <asp:TextBox ID="txtNameOfSignatureC" runat="server"></asp:TextBox>
                    </label>
                </div>
            </div>
        </div>
    </div>
</fieldset>

<%-- <div class="col-xl-2 col-lg-2 col-md-4 col-sm-6 col-12" style="text-align: right; vertical-align: bottom; padding-top: 28px;">
                                          
                                        <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="button save" data-action="save" OnClick="Save" style="visibility: hidden" />
                                        <input type="button" id="btnUpload" value="Upload" style="visibility: hidden" />
                                       
                                    </div>--%>


<script src="../../SignJS/signature_pad.js"></script>
<script src="../../SignJS/app.js"></script>

<script type="text/javascript" src="../../SignJS/1.8.3/jquery.min.js"></script>
<%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
<meta name="description" content="Signature Pad - HTML5 canvas based smooth signature drawing using variable width spline interpolation.">
<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">

<link rel="stylesheet" href="../../SignJS/signature-pad.css">
<script src="../../SignJS/WebCam.js" type="text/javascript"></script>
<script type="text/javascript"> 
    $(function () {
        debugger;
        //Webcam.set({
        //    width: 320,
        //    height: 240,
        //    image_format: 'jpeg',
        //    jpeg_quality: 90
        //});
        /*Webcam.attach('#webcam');*/
        $("#btnCapture").click(function () {
            Webcam.snap(function (data_uri) {
                //  $("#imgCapture")[0].src = data_uri;
                // document.getElementById("divCapture").style.display = "block";

                debugger;
                var lblProcessID = document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").innerHTML;
                if (lblProcessID == 1) {
                    document.getElementById("divWeb").style.display = "None";
                    document.getElementById("divTSign").style.display = "block";
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_hfTPhotoCapture").value = data_uri;
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").innerHTML = "2";
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessName").innerHTML = "Signature Of Engineer";
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_chVEngineerPhoto").checked  = true;
                }
                else if (lblProcessID == 3) {
                    document.getElementById("divWeb").style.display = "None";
                    document.getElementById("divCSign").style.display = "block";
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_hfCPhotoCapture").value = data_uri;
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").innerHTML = "4";
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessName").innerHTML = "Signature Of Customer";
                    document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_chVCustomerPhoto").checked = true;
                }
            });
        });
        $("#btnTSign").click(function () {
            document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").innerHTML = "3";
            document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessName").innerHTML = "Photo Of Customer";
            document.getElementById("divWeb").style.display = "block";
            document.getElementById("divTSign").style.display = "None";
            document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_chVEngineerSign").checked = true;
        });
        $("#btnCSign").click(function () {

            document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessID").innerHTML = "5";
            document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_lblProcessName").innerHTML = "Name Of Customer Signature";

            document.getElementById("divWeb").style.display = "None";
            document.getElementById("divCSign").style.display = "None";
            document.getElementById("divNameOfSignature").style.display = "block";
            document.getElementById("MainContent_UC_ICTicketView_UC_FsrSignature_chVCustomerSign").checked = true;
        });
    });

</script>




<asp:HiddenField ID="hfLatitude" runat="server" />
<asp:HiddenField ID="hfLongitude" runat="server" />
<script> 
    function success(position) {
        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;
        document.getElementById('MainContent_UC_ICTicketView_UC_FsrSignature_hfLatitude').value = latitude;
        document.getElementById('MainContent_UC_ICTicketView_UC_FsrSignature_hfLongitude').value = longitude;
        status.textContent = '';
    }
    function error() {
        status.textContent = 'Unable to retrieve your location';
    }

    if (!navigator.geolocation) {
        status.textContent = 'Geolocation is not supported by your browser';

    } else {
        status.textContent = 'Locating…';
        navigator.geolocation.getCurrentPosition(success, error);
    }
</script>
