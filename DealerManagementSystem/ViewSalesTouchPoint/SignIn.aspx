<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="SignIn.aspx.cs" Inherits="DealerManagementSystem.ViewSalesTouchPoint.SignIn" %>

<!DOCTYPE html>

<html>
<head>
    <link rel="icon" href="../Ajax/Images/dms4.jpg" type="image/x-icon">
    <title>AJAX-DMS | Sign in</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="../CSS/bootstrap.min.4.5.2.css" />
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>

    <style>
        #FldRegister {
            line-height: 5px;
            padding: 10px;
        }

            #FldRegister> div> div> input
            {
                padding: 5px;
            }
            #FldRegister> div> div> .btn
            {
                padding: 12px;
            }
            #FldRegister> div> div> .btn
            {
                font-size: 15px;
            }

        body {
            font-family: Arial, Helvetica, sans-serif;
            line-height: 15px;
        }

        * {
            box-sizing: border-box;
        }

        .fieldset-border {
            border: solid 1px #cacaca;
            margin: 15px 0;
            border-radius: 5px;
            padding: 10px;
        }

            .fieldset-border legend {
                width: auto;
                background: #fff;
                padding: 0 4px;
                color: #313131;
                width: 100%;
                height: 100%;
                font-size: 14px;
                margin-bottom: 5px;
                border: none;
                font-weight: 500;
            }

        fieldset {
            padding: 5px;
        }

            fieldset legend {
                font-weight: bold;
            }
        /* style the container */
        .container {
            position: relative;
            border-radius: 5px;
            background-color: #f2f2f2;
            padding: 20px 0 30px 0;
        }

        /* style inputs and link buttons */
        input,
        .btn {
            width: 100%;
            padding: 12px;
            /*padding: 5px;*/
            border: solid 1px #cacaca;
            background-color: rgb(245 248 255);
            border-radius: 4px;
            margin: 5px 0;
            opacity: 0.85;
            display: inline-block;
            /*font-size: 17px;*/
            font-size: 12px;
            /*line-height: 20px;*/
            line-height: 5px;
            text-decoration: none; /* remove underline from anchors */
        }

            input:hover,
            .btn:hover {
                opacity: 1;
            }

        /* add appropriate colors to fb, twitter and google buttons */
        .fb {
            background-color: #3B5998;
            color: white;
        }

        .twitter {
            background-color: #55ACEE;
            color: white;
        }

        .google {
            background-color: #dd4b39;
            color: white;
        }

        /* style the submit button */
        input[type=submit] {
            background-color: #04AA6D;
            color: white;
            cursor: pointer;
        }

            input[type=submit]:hover {
                background-color: #45a049;
            }



        .modal-lc {
            max-width: 20%;
        }

        /*.modal-lg {
            width:991px;
        }*/

        /* Split the screen in half */
        .split {
            height: 100%;
            position: fixed;
            z-index: 1;
            top: 0;
            /*overflow-x: hidden;*/
            padding-top: 20px;
        }

        /* Control the left side */
        .left {
            left: 0;
            width: 50%;
            padding: initial;
            background-color: #fff;
        }

        /* Control the right side */
        .right {
            right: 0;
            width: 50%;
            padding: initial;
        }
        /* Control the top side */
        .top {
            top: 0;
            height: 50%;
            width: 50%;
            padding: initial;
            background-color: #fff;
        }

        /* Control the bottom side */
        .bottom {
            top: 50%;
            bottom: 0;
            height: 50%;
            width: 50%;
            padding: initial;
        }

        /* If you want the content centered horizontally and vertically */
        .centered {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            text-align: center;
        }

            /* Style the image inside the centered container, if needed */
            .centered img {
                width: 150px;
                border-radius: 50%;
            }

        .vertical-center {
            margin: 0;
            padding: 80px;
            position: absolute;
            top: 50%;
            left: 10%;
            -ms-transform: translateY(-50%);
            transform: translateY(-50%);
        }

        #ImageCompanyLogoRight {
            display: none;
        }

        label {
            margin-bottom: initial;
        }

        .form-control {
            padding: 5px;
            font-size: 12px;
            line-height: 5px;
            margin: 5px 0;
            /*height: initial;*/
        }

        @media (min-width: 250px) and (max-width: 1000px) {
            #LoginLeft {
                display: none;
            }

            #Footer {
                display: none;
            }

            #ImageCompanyLogoRight {
                display: block;
            }

            #LoginRight {
                width: 100%;
                overflow: auto;
            }


            .vertical-center {
                margin: 0;
                padding: initial;
                position: absolute;
                top: 40%;
                left: 10%;
                -ms-transform: translateY(-50%);
                transform: translateY(-50%);
            }
        }

        .care {
            font-family: Proxima Nova;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
        function IsNumbericOnlyCheck(name) {
            var regEx = /^\d+$/;
            if (name.value.match(regEx)) {                
                return true;
            }
            else {
                name.value = '';
                $('#lblMessage').text('Invalid Number Format');
                return false;
            }
        }
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
                if ((iKeyCode > 95 && iKeyCode < 106)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            return true;
        }        
    </script>
    <script type="text/javascript">
        function OTP() {
            var timeLeft = 30;
            var elem = document.getElementById("some_div");
            var timerId = setInterval(countdown, 1000);

            function countdown() {
                if (timeLeft == -1) {
                    clearTimeout(timerId);
                    doSomething();
                } else {
                    document.getElementById("<%=BtnSendOTP.ClientID%>").style.display = "none";
                    elem.innerHTML = timeLeft + ' Seconds Left';
                    timeLeft--;
                }
            }

            function doSomething() {
                document.getElementById("<%=BtnSendOTP.ClientID%>").value = "Resend OTP";
                document.getElementById("<%=BtnSendOTP.ClientID%>").style.display = "inline";
            }
        }
    </script>
    <script type="text/javascript">
        function MobileOTP() {
            var timeLeft = 30;
            var elem = document.getElementById("Mobilesome_div");
            var timerId = setInterval(countdown, 1000);

            function countdown() {
                if (timeLeft == -1) {
                    clearTimeout(timerId);
                    doSomething();
                }
                else {
                    document.getElementById("<%=BtnSendMobileOTP.ClientID%>").style.display = "none";
                    elem.innerHTML = timeLeft + ' Seconds Left';
                    timeLeft--;
                }
            }

            function doSomething() {
                document.getElementById("<%=BtnSendMobileOTP.ClientID%>").value = "Resend OTP";
                document.getElementById("<%=BtnSendMobileOTP.ClientID%>").style.display = "inline";
            }
        }
        function EmailOTP() {
            var timeLeft = 30;
            var elem = document.getElementById("Emailsome_div");
            var timerId = setInterval(countdown, 1000);

            function countdown() {
                if (timeLeft == -1) {
                    clearTimeout(timerId);
                    doSomething();
                }
                else {
                    document.getElementById("<%=BtnSendEmailOTP.ClientID%>").style.display = "none";
                    elem.innerHTML = timeLeft + ' Seconds Left';
                    timeLeft--;
                }
            }

            function doSomething() {
                document.getElementById("<%=BtnSendEmailOTP.ClientID%>").value = "Resend OTP";
                document.getElementById("<%=BtnSendEmailOTP.ClientID%>").style.display = "inline";
            }
        }
    </script>


    <script> 
        function success(position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            document.getElementById('hfLatitude').value = latitude;
            document.getElementById('hfLongitude').value = longitude;
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
</head>
<body>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            display: none;
            position: fixed;
            background-color: black;
            z-index: 999;
            width: 100%;
            height: 100%;
            opacity: 80%;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <div>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <asp:HiddenField ID="hfLatitude" runat="server" />
            <asp:HiddenField ID="hfLongitude" runat="server" />
            <div class="row">
                <div id="LoginLeft" class="left split">
                    <div class="split left">
                        <asp:Image ID="Image1" runat="server" Width="100%" Height="100%" ImageUrl="../Ajax/Images/bg01.jpg" />

                        <div class="vertical-center" style="padding: 198px; text-align: center">
                            <asp:Image ID="Image6" runat="server" ImageUrl="../Ajax/Images/AJAXtLogo.png" Height="35" Width="48" />
                            <asp:Image ID="ImageCompanyLogo" runat="server" ImageUrl="../Ajax/Images/AJAXOneW.png" Height="50" Width="150" />
                            <h3 style="font-family: Calibri; color: white; margin-top: 5px">DEALER MANAGEMENT SYSTEM</h3>

                        </div>

                        <div class="vertical-center" style="padding: 198px; padding-left: 230px; padding-bottom: 10px; text-align: center; /*margin-top: 200px; */ font-family: sans-serif;">
                            <h5 class="care  text-white" style="margin-top: 200px; font-family: Proxima Nova;">NEED HELP ? &nbsp;LET US KNOW</h5>
                            <li class="fa fa-phone text-white">
                                <a class="care text-white" href="tel:+91 08067200014">+91 08067200014</a></li>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <li class="fa fa-envelope text-white">
                                <a class="care text-white" href="mailto:support@ajax-engg.com"><span>support@ajax-engg.com</span></a></li>
                        </div>

                    </div>
                </div>
                <div id="LoginRight" class="right split" style="margin: 0 auto">
                    <div class="col-md-12 vertical-center" style="width: 90%">
                        <div id="ImageCompanyLogoRight" style="text-align: center;">
                            <asp:Image ID="ImageCompanyLogo2" runat="server" ImageUrl="~/Ajax/Images/AJAXOneB.png" Height="40" Width="150" />
                        </div>
                        <br />
                        <div style="text-align: center">
                            <asp:Label ID="lblMessage" runat="server" Visible="true"></asp:Label>
                        </div>
                        <fieldset class="fieldset-border" id="FldSignin" runat="server" visible="true">
                            <legend style="background: none; color: #007bff; font-size: 20px; width: auto">Sign in</legend>
                            <div style="text-align: center;">
                                <asp:Image ID="ImageAppLogo" runat="server" ImageUrl="~/Ajax/Images/dms4.jpg" Width="50" Height="50" />
                            </div>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>Mobile Number</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtUsername" runat="server" ToolTip="Enter UserName..." PlaceHolder="UserName" CssClass="form-control" onchange="return IsNumbericOnlyCheck(this);"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtPassword" runat="server" ToolTip="Enter Password..." PlaceHolder="Password" type="password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" Width="120px" OnClick="btnLogin_Click" /><asp:Button ID="btnLogRegister" runat="server" Text="Register" OnClick="btnLogRegister_Click" Width="120px" />
                                </div>
                            </div>
                            <div class="col-md-12">
                                <br />
                                <asp:LinkButton ID="LnkForgotPassword" runat="server" OnClick="LnkForgotPassword_Click">Forgot password?</asp:LinkButton>
                                <dev style="float: right; padding-right: 1em; font-size: medium; color: white; background-color: darkgray; margin-right: 4px;">
                                    <asp:Label ID="lblServer" runat="server"></asp:Label></dev>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border" id="FldResetPassword" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 20px; width: auto">Reset Password</legend>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>OTP</b></label>
                                </div>
                                <div>
                                    <asp:Button ID="BtnSendOTP" runat="server" Text="Send OTP" Width="130px" />
                                    <div id="some_div"></div>
                                    <asp:TextBox ID="txtOTP" runat="server" ToolTip="Type Six digit OTP" PlaceHolder="OTP" autocomplete="off" Width="140px"></asp:TextBox>
                                    <asp:Button ID="BtnVerifyMobileorEmailOTP" runat="server" Text="Verify Email/Mobile" Width="150px" Visible="false" OnClick="BtnVerifyMobileorEmailOTP_Click" />
                                    <asp:Image ID="VerifyMobileorEmailOTP" runat="server" ImageUrl="~/Images/NotVerified.jpg" Width="30px" Height="30px" Visible="false" />
                                </div>
                                <div>
                                    <br />
                                    <label><b>New Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRNewPassword" runat="server" ToolTip="Enter New Password..." PlaceHolder="New Password" autocomplete="off" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <br />
                                    <label><b>Retype Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRRetypePassword" runat="server" ToolTip="Enter Retype Password..." PlaceHolder="Retype Password" autocomplete="off" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border" id="FldChangePassword" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 20px; width: auto">Change Password</legend>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>Old Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtOldPassword" runat="server" ToolTip="Enter Old Password..." PlaceHolder="Old Password" autocomplete="off" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <br />
                                    <label><b>New Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtNewPassword" runat="server" ToolTip="Enter New Password..." PlaceHolder="New Password" autocomplete="off" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <br />
                                    <label><b>Retype Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRetypePassword" runat="server" ToolTip="Enter Retype Password..." PlaceHolder="Retype Password" autocomplete="off" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Button ID="BtnChange" runat="server" Text="Reset Password" OnClick="BtnChange_Click" />
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border" id="FldRegister" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 20px; width: auto">Register</legend>
                            <div class="col-md-12">
                                <div>
                                    <label><b>Aadhaar Number</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtAadharNo" runat="server" ToolTip="Enter AadharNo..." PlaceHolder="AadharNo" AutoCompleteType="Disabled" MaxLength="12" onchange="return IsNumbericOnlyCheck(this);"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>Name</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtName" runat="server" ToolTip="Enter Name..." PlaceHolder="Name" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>Mobile Number</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtMobileNumber" runat="server" MaxLength="10" onchange="return IsNumbericOnlyCheck(this);" placeholder="Mobile Number" Width="205px" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:LinkButton ID="btnMobileVerify" runat="server" OnClick="btnMobileVerify_Click">Verify Mobile?</asp:LinkButton>
                                    <asp:Image ID="VerifyMobileOTP" runat="server" ImageUrl="~/Images/NotVerified.jpg" Width="30px" Height="30px"/>
                                </div>

                                <div>
                                    <label><b>Email ID</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email ID" Width="205px" AutoCompleteType="Disabled"></asp:TextBox>
                                    <%--<asp:LinkButton ID="btnEmailVerify" runat="server" OnClick="btnEmailVerify_Click">Verify Email?</asp:LinkButton>
                                    <asp:Image ID="VerifyEmailOTP" runat="server" ImageUrl="~/Images/NotVerified.jpg" Width="30px" Height="30px"/>--%>
                                </div>
                                <div>
                                    <label><b>Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRegPassword" runat="server" ToolTip="Enter Password..." PlaceHolder="Password" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>Conform Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRegConfirmPassword" runat="server" ToolTip="Enter Password..." PlaceHolder="Password" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>Address1</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtAddress1" runat="server" ToolTip="Address1..." PlaceHolder="Address1" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>Address2</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtAddress2" runat="server" ToolTip="Address2..." PlaceHolder="Address2" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <div>
                                    <label><b>State</b></label>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" />
                                </div>
                                <div>
                                    <label><b>District</b></label>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" />
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnRegister" runat="server" Text="Register" Width="120px" OnClick="btnRegister_Click" CssClass="btn"/><asp:Button ID="btnRegSignIn" runat="server" Text="SignIn" OnClick="btnRegSignIn_Click" Width="120px" CssClass="btn"/>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border" id="FldVerifyMobileOTP" runat="server" visible="false">
                            <legend id="lVerifyOtp" runat="server" style="background: none; color: #007bff; font-size: 20px; width: auto">Verify Mobile OTP</legend>
                            <div class="col-md-12">
                                <asp:Button ID="BtnSendMobileOTP" runat="server" Text="Send OTP" Width="100px" OnClick="BtnSendMobileOTP_Click" />
                                <div id="Mobilesome_div" runat="server"></div>
                                <asp:TextBox ID="txtMobileOTP" runat="server" ToolTip="Type Six digit OTP" MaxLength="6" onkeydown="return isNumber(event);" Width="100px" AutoCompleteType="Disabled" PlaceHolder="OTP" Visible="false"></asp:TextBox>
                                <asp:Button ID="BtnVerifyMobileOTP" runat="server" Text="Verify" Width="130px" Visible="false" OnClick="BtnVerifyMobileOTP_Click" />
                                <asp:Button ID="BtnMobileOTPBack" runat="server" Text="Back" Width="130px" OnClick="BtnMobileOTPBack_Click" />
                            </div>
                        </fieldset>
                        <fieldset class="fieldset-border" id="FldVerifyEmailOTP" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 20px; width: auto">Verify Email OTP</legend>
                            <div class="col-md-12">
                                <asp:Button ID="BtnSendEmailOTP" runat="server" Text="To Send OTP" Width="120px" OnClick="BtnSendEmailOTP_Click" />
                                <div id="Emailsome_div" runat="server"></div>
                                <asp:TextBox ID="txtEmailOTP" runat="server" ToolTip="Type Six digit OTP" MaxLength="6" onkeydown="return isNumber(event);" Width="100px" AutoCompleteType="Disabled" PlaceHolder="OTP" Visible="false"></asp:TextBox>
                                <asp:Button ID="BtnVerifyEmailOTP" runat="server" Text="Verify" Width="130px" Visible="false" OnClick="BtnVerifyEmailOTP_Click" />
                                <asp:Button ID="BtnEmailOTPBack" runat="server" Text="Back" Width="130px" OnClick="BtnEmailOTPBack_Click" />
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
