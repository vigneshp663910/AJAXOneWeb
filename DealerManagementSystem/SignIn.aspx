<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="DealerManagementSystem.SignIn" %>


<!DOCTYPE html>

<html>
<head>
    <link rel="icon" href="../Ajax/Images/dms4.jpg" type="image/x-icon">
    <title>AJAX-DMS | Sign in</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="../CSS/bootstrap.min.4.5.2.css" />


    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
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
            border: solid 1px #cacaca;
            background-color: rgb(245 248 255);
            border-radius: 4px;
            margin: 5px 0;
            opacity: 0.85;
            display: inline-block;
            font-size: 17px;
            line-height: 20px;
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

        /* Two-column layout */
        /*.col {
            float: left;
            width: 50%;
            margin: auto;
            padding: 0 50px;
            margin-top: 6px;
        }*/

        /* Clear floats after the columns */
        /*.row:after {
            content: "";
            display: table;
            clear: both;
        }*/

        /* vertical line */
        /*.vl {
            position: absolute;
            left: 50%;
            transform: translate(-50%);
            border: 2px solid #ddd;
            height: 175px;
        }*/

        /* text inside the vertical line */
        /*.vl-innertext {
            position: absolute;
            top: 50%;
            transform: translate(-50%, -50%);
            background-color: #f1f1f1;
            border: 1px solid #ccc;
            border-radius: 50%;
            padding: 8px 10px;
        }*/

        /* hide some text on medium and large screens */
        /*.hide-md-lg {
            display: none;
        }*/

        /* bottom container */
        /*.bottom-container {
            text-align: center;
            background-color: #666;
            border-radius: 0px 0px 4px 4px;
        }*/

        /* Responsive layout - when the screen is less than 650px wide, make the two columns stack on top of each other instead of next to each other */
        /*@media screen and (max-width: 650px) {
            .col {
                width: 100%;
                margin-top: 0;
            }*/
        /* hide the vertical line */
        /*.vl {
                display: none;
            }*/
        /* show the hidden text on small screens */
        /*.hide-md-lg {
                display: block;
                text-align: center;
            }
        }*/

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

        @media screen and (min-device-width: 320px) and (max-device-width: 768px) {
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
        function OTP() {
            var timeLeft = 15;
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




        <%--function startTimer() {
            var presentTime = document.getElementById('timer').innerHTML;
            var timeArray = presentTime.split(/[:]+/);
            var m = timeArray[0];
            var s = checkSecond((timeArray[1] - 1));
            if (s == 0 && m == 0) {
                $('#<%=BtnSendOTP.ClientID %>').val="Resend OTP";
                $('#<%=BtnSendOTP.ClientID %>').prop("disabled", "enabled");
            }
            if (s == 59) { m = m - 1 }
            if (m < 0) {
                
                return
            }

            document.getElementById("timer").innerHTML = m + ":" + s;
            console.log(m)
            setTimeout(startTimer, 1000);

        }

        function checkSecond(sec) {
            if (sec < 10 && sec >= 0) { sec = "0" + sec }; // add zero in front of numbers < 10
            if (sec < 0) { sec = "59" };
            return sec;
        }--%>



    </script>

    <script>
        /* function geoFindMe() { */

        //const status = document.querySelector('#status');
        //const mapLink = document.querySelector('#map-link');

        //mapLink.href = '';
        //mapLink.textContent = '';

        function success(position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;
            document.getElementById('hfLatitude').value = latitude;
            document.getElementById('hfLongitude').value = longitude;
            status.textContent = '';
            //  mapLink.href = `https://www.openstreetmap.org/#map=18/${latitude}/${longitude}`;
            //  mapLink.textContent = `Latitude: ${latitude} °, Longitude: ${longitude} °`;
            //alert(latitude);
            //alert(longitude);
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

        /*  } */
     //   document.querySelector('#find-me').addEventListener('click', geoFindMe);
    </script>
</head>
<body>
    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="Images/PageLoader.gif" alt="" />
    </div>
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
            background-color: White;
            z-index: 999;
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
            <asp:HiddenField ID="hfLatitude" runat="server" />
            <asp:HiddenField ID="hfLongitude" runat="server" />
            <div class="row">
                <div id="LoginLeft" class="left split">
                    <div class="split left">
                        <asp:Image ID="Image1" runat="server" Width="100%" Height="100%" ImageUrl="~/Ajax/Images/bg01.jpg" />

                        <div class="vertical-center" style="padding: 198px; text-align: center">
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Ajax/Images/AJAXtLogo.png" Height="35" Width="48" />
                            <%-- <asp:Image ID="ImageCompanyLogo1" runat="server" ImageUrl="~/Ajax/Images/ajax_logow.png" Height="70" Width="150" />--%>
                            <asp:Image ID="ImageCompanyLogo" runat="server" ImageUrl="~/Ajax/Images/AJAXOneW.png" Height="50" Width="150" />
                            <h3 style="font-family: Calibri; color: white; margin-top: 5px">DEALER MANAGEMENT SYSTEM</h3>

                        </div>

                        <div class="vertical-center" style="padding: 198px; padding-left: 230px; padding-bottom: 10px; text-align: center; margin-top: 200px; font-family: sans-serif;">
                            <h5 class="care  text-white" style="margin-top: 200px; font-family: Proxima Nova;">NEED HELP ? &nbsp;LET US KNOW</h5>
                            <li class="fa fa-phone text-white">
                                <a class="care text-white" href="tel:+91 08067200014">+91 08067200014</a></li>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <li class="fa fa-envelope text-white">
                                <a class="care text-white" href="mailto:support@ajax-engg.com"><span>support@ajax-engg.com</span></a></li>
                        </div>

                    </div>
                    <%--<div class="split bottom" style="padding: 128px; background: linear-gradient(180deg, #b7babf, #f0f4fd,#b7babf);">
                        <h2>Ajax-XXXXXXXXXXXX</h2>
                    </div>--%>
                </div>
                <div id="LoginRight" class="right split" style="margin: 0 auto">
                    <div class="col-md-12 vertical-center" style="width: 80%">
                        <div id="ImageCompanyLogoRight" style="text-align: center;">
                            <%--<asp:Image ID="Image7" runat="server" ImageUrl="~/Ajax/Images/AJAXtLogo.png" Height="30" Width="40" />--%>
                            <%-- <asp:Image ID="ImageCompanyLogo2" runat="server" ImageUrl="~/Ajax/Images/Ajax-New-Logo.png" Height="40" Width="150" />--%>
                            <asp:Image ID="ImageCompanyLogo2" runat="server" ImageUrl="~/Ajax/Images/AJAXOneB.png" Height="40" Width="150" />
                        </div>
                        <br />
                        <div style="text-align: center;">
                            <asp:Image ID="ImageAppLogo" runat="server" ImageUrl="~/Ajax/Images/dms4.jpg" Width="50" Height="50" />
                        </div>

                        <%--<div style="text-align: center;background-color:#2e516d">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Ajax/Images/dms6.jpg" Width="130" Height="60" />
                        </div>--%>

                        <fieldset class="fieldset-border" id="FldSignin" runat="server">

                            <legend style="background: none; color: #007bff; font-size: 20px;">Sign in</legend>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>UserID / Email / Mobile</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtUsername" runat="server" ToolTip="Enter User Id / Email Id / Mobile No..." PlaceHolder="UserID / Email / Mobile" CausesValidation="false"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtUsername" ToolTip="<%$ Resources:Resource,ttpUsername %>" ForeColor="Red"><img src="images/error_info.png" alt="info" />

                                    </asp:RequiredFieldValidator>--%>
                                    <%-- <input type="text" name="username" id="txtusername" runat="server" placeholder="Username" required>--%>
                                </div>
                                <div>
                                    <%--<br />--%>
                                    <label><b>Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtPassword" runat="server" ToolTip="Enter Password..." PlaceHolder="Password" TextMode="Password" CausesValidation="false"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>--%>

                                    <%--<input type="password" name="password" id="txtpassword" runat="server" placeholder="Password" required>--%>
                                </div>
                                <%--  <br />--%>
                                <%--  <input type="submit" value="Login">--%>
                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                            </div>
                            <div class="col-md-12">
                                <br />
                                <asp:LinkButton ID="LnkForgotPassword" runat="server" OnClick="lForgetPassword_Click" CausesValidation="false">Forgot password?</asp:LinkButton>
                                <%--<br />
                                <br />
                                <label>Don't have ajax account?</label>
                                <input type="submit" value="Create an account" class="btn-danger">--%>
                            </div>
                        </fieldset>

                        <fieldset class="fieldset-border" id="FldResetPassword" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 20px;">Reset Password</legend>
                            <div class="col-md-12">
                                <div>
                                    <br />
                                    <label><b>OTP</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtOTP" runat="server" ToolTip="Type Six digit OTP" PlaceHolder="OTP" autocomplete="off" TextMode="Number" Width="130px"></asp:TextBox><asp:Button ID="BtnSendOTP" runat="server" Text="Send OTP" Width="130px" OnClick="BtnSendOTP_Click" CausesValidation="false" /><div id="some_div"></div>

                                    <%--<asp:LinkButton ID="BtnSendOTP" runat="server" OnClick="BtnSendOTP_Click" Text="Send OTP">LinkButton</asp:LinkButton>--%>
                                </div>
                                <div>
                                    <br />
                                    <label><b>New Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRNewPassword" runat="server" ToolTip="Enter New Password..." PlaceHolder="New Password" autocomplete="off" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRNewPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <br />
                                    <label><b>Retype Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtRRetypePassword" runat="server" ToolTip="Enter Retype Password..." PlaceHolder="Retype Password" autocomplete="off" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRRetypePassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                                </div>
                                <asp:Button ID="BtnReset" runat="server" Text="Reset" OnClick="BtnReset_Click" />
                            </div>
                        </fieldset>

                        <fieldset class="fieldset-border" id="FldChangePassword" runat="server" visible="false">
                            <legend style="background: none; color: #007bff; font-size: 20px;">Change Password</legend>
                            <div class="col-md-12">
                                <div>
                                    <%--<br />--%>
                                    <label><b>Old Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtOldPassword" runat="server" ToolTip="Enter Old Password..." PlaceHolder="Old Password" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOldPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <br />
                                    <label><b>New Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtCNewPassword" runat="server" ToolTip="Enter New Password..." PlaceHolder="New Password" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCNewPassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <br />
                                    <label><b>Retype Password</b></label>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtCRetypePassword" runat="server" ToolTip="Enter Retype Password..." PlaceHolder="Retype Password" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCRetypePassword" ToolTip="<%$ Resources:Resource,ttpPassword %>" ForeColor="Red"><img src="images/error_info.png" alt="info" /></asp:RequiredFieldValidator>
                                </div>
                                <asp:Button ID="BtnChange" runat="server" Text="Change" OnClick="BtnChange_Click" />
                            </div>
                        </fieldset>
                        <div style="text-align: center">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>
                        <%--  </div>--%>

                        <div id="Footer1">
                            <div style="text-align: center; margin: 10px 0;">
                                <p><font size="2px">Powered by AJAXOne&nbsp;&copy; <%: DateTime.Now.Year %> </font></p>

                                <div id="Footer">
                                    <p><font size="2px"><a href="#" data-toggle="modal" data-target="#myModal">Download Our Mobile App </a></font></p>
                                    <p>
                                        <span>
                                            <a href="#" data-toggle="modal" data-target="#myModal">
                                                <img src="../Images/apple.png" border="0" id="" alt="">
                                            </a>
                                        </span>
                                        <span>
                                            <%--<a href="https://play.google.com/store/apps/details?id=com.ajaxengg.hr_app">
                                                <img src="../Images/Playstore.png" border="0" id="" alt="">
                                            </a>--%>

                                            <a href="#" data-toggle="modal" data-target="#myModal">
                                                <img src="../Images/Playstore.png" border="0" id="" alt="">
                                            </a>


                                        </span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>


    <div id="myModal" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header" style="color: #999999; height: 50px;">
                    <h5 class="modal-title" style="color: #0000FF"><b>AJAX Mobile Apps Library</b></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table id="tab_mobile" border="1" style="font-family: Calibri; font-size: medium">
                        <tr style="background-color: black; color: white">
                            <th>SN
                            </th>
                            <th style="width: 60px; text-align: center">Ico
                            </th>
                            <th style="width: 150px; text-align: center">App
                            </th>
                            <th style="width: 80px; text-align: center">Mode
                            </th>
                            <th style="width: 100px; text-align: center">Features
                            </th>
                            <th style="width: 60px">
                                <img src="../Images/Playstore.png" border="0" id="" alt="" width="110Px">
                            </th>
                            <th style="width: 60px">
                                <img src="../Images/apple.png" border="0" id="" alt="" width="110Px">
                            </th>
                            <th style="width: 620px; text-align: center">Remarks
                            </th>
                        </tr>
                        <tr>
                            <td style="text-align: right">1</td>
                            <td style="text-align: center">
                                <asp:Image ID="ImgjxOne" runat="server" ImageUrl="~/Ajax/Images/dms4.jpg" Width="30" Height="30" /></td>
                            <td>AJAX One</td>
                            <td>Online</td>
                            <td>All</td>
                            <td style="text-align: center"><a href="https://play.google.com/store/apps/details?id=com.ajaxengg.sales_webview">Install</a></td>
                            <td style="text-align: center"><a href="https://apps.apple.com/app/ajaxone/id1619091173">Install</a></td>
                            <td style="width: 550px; text-align: left">Includes both Pre-Sales & Service</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">2</td>
                            <td style="text-align: center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Ajax/Images/Pre-Sales5.jpg" Width="30" Height="30" /></td>
                            <td>Pre-Sales</td>
                            <td>Offline</td>
                            <td>RoleBased</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 550px; text-align: left">Customer, Lead, Activity & Quotation</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">3</td>
                            <td style="text-align: center">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Ajax/Images/Service5.jpg" Width="50" Height="30" /></td>
                            <td>Service</td>
                            <td>Offline</td>
                            <td>RoleBased</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 550px; text-align: left">IC Tickets, Customer Feedback</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">4</td>
                            <td style="text-align: center">
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Ajax/Images/User5.jpg" Width="30" Height="30" /></td>
                            <td>Customer</td>
                            <td>Online</td>
                            <td>Standard</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 550px; text-align: left">To Develop in Phase-2</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">5</td>
                            <td style="text-align: center">
                                <asp:Image ID="Image5" runat="server" ImageUrl="~/Ajax/Images/Operator5.png" Width="30" Height="30" /></td>
                            <td>Operator</td>
                            <td>Online</td>
                            <td>Standard</td>
                            <td style="text-align: center">Install</td>
                            <td style="text-align: center">Install</td>
                            <td style="width: 550px; text-align: left">For M/C Operators & Customers Only</td>
                        </tr>


                    </table>

                </div>

            </div>
        </div>
    </div>

    <%--<script>

        function openModal() {
            $('#myModal').modal('show')
        };
    </script>--%>
</body>
</html>
