<%@ Page Title="" Language="C#" MasterPageFile="~/ViewSalesTouchPoint/SalesTouchPointMaster.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="MyProfile.aspx.cs" Inherits="DealerManagementSystem.ViewSalesTouchPoint.MyProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function IsNumbericOnlyCheck(name) {
            var regEx = /^\d+$/;
            if (name.value.match(regEx)) {
                return true;
            }
            else {
                name.value = '';
                $('#MainContent_lblMessage').text('Invalid Number Format');
                return false;
            }
        }
        function MobileOTP() {
            var timeLeft = 30;
            var timerId = setInterval(countdown, 1000);

            function countdown() {
                if (timeLeft == -1) {
                    clearTimeout(timerId);
                    doSomething();
                }
                else {
                    document.getElementById("<%=BtnSendOTP.ClientID%>").style.display = "none";
                    document.getElementById("<%=some_div.ClientID%>").innerHTML = timeLeft + ' Seconds Left';
                    timeLeft--;
                }
            }

            function doSomething() {
                document.getElementById("<%=BtnSendOTP.ClientID%>").value = "Resend OTP";
                document.getElementById("<%=BtnSendOTP.ClientID%>").style.display = "inline";
            }
        }

        function EmailOTP() {
            var timeLeft = 30;
            var timerId = setInterval(countdown, 1000);

            function countdown() {
                if (timeLeft == -1) {
                    clearTimeout(timerId);
                    doSomething();
                } else {
                    document.getElementById("<%=BtnSendOTP.ClientID%>").style.display = "none";
                    document.getElementById("<%=some_div.ClientID%>").innerHTML = timeLeft + ' Seconds Left';
                    timeLeft--;
                }
            }

            function doSomething() {
                document.getElementById("<%=BtnSendOTP.ClientID%>").value = "Resend OTP";
                document.getElementById("<%=BtnSendOTP.ClientID%>").style.display = "inline";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message"/>
    <%--<script> 
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
    </script>--%>
    <asp:HiddenField ID="hfLatitude" runat="server" Visible="false" />
    <asp:HiddenField ID="hfLongitude" runat="server" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border" id="FldViewProfile" runat="server">
                <legend style="background: none; color: #007bff; font-size: 17px;">Info</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <asp:ImageButton ID="ibtnPhoto" ImageUrl="~/Images/User.jpg" runat="server" Width="60px" Height="55px" />
                    </div>
                    <div class="col-md-9"></div>
                    <div class="col-md-3 text-right">
                        <label>Aadhaar Number</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblAadhaarNumber" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Address1</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblAddress1" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Name</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblFullName" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Address2</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblAddress2" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>(SMS) Contact No</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblContactNo" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>State</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblState" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>Email</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblEmail" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-3 text-right">
                        <label>District</label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblDistrict" runat="server" CssClass="label"></asp:Label>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnEditProfile" runat="server" Text="Change" CssClass="btn Save" OnClick="btnEditProfile_Click" />
                    </div>
                </div>
            </fieldset>
            <fieldset class="fieldset-border" id="FldUpdateProfile" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Aadhaar Number</label>
                        <asp:TextBox ID="txtAadhaarNumber" runat="server" CssClass="form-control" ToolTip="Enter Name..." PlaceHolder="Name" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ToolTip="Enter Name..." PlaceHolder="Name" AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Mobile Number</label>
                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="10" onchange="return IsNumbericOnlyCheck(this);" placeholder="Mobile Number" AutoCompleteType="Disabled" OnTextChanged="txtMobileNumber_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-2 col-sm-12">
                        <label class="modal-label">
                            <br />
                        </label>
                        <asp:LinkButton ID="btnMobileVerify" runat="server" OnClick="btnMobileVerify_Click">Verify Mobile?</asp:LinkButton>
                    </div>
                    <div class="col-md-3 col-sm-12">
                        <label class="modal-label">
                            <br />
                        </label>
                        <asp:Image ID="VerifyMobileOTP" runat="server" ImageUrl="~/Images/NotVerified.jpg" Width="30px" Height="30px"/>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Email ID</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email ID" AutoCompleteType="Disabled" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <%--<div class="col-md-2 col-sm-12">
                        <label class="modal-label">
                            <br />
                        </label>
                        <asp:LinkButton ID="btnEmailVerify" runat="server" OnClick="btnEmailVerify_Click">Verify Email?</asp:LinkButton>
                    </div>
                    <div class="col-md-3 col-sm-12">
                        <label class="modal-label">
                            <br />
                        </label>
                        <asp:Image ID="VerifyEmailOTP" runat="server" ImageUrl="~/Images/NotVerified.jpg" Width="30px" Height="30px"/>
                    </div>--%>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address1</label>
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" ToolTip="Address1..." PlaceHolder="Address1" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">Address2</label>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" ToolTip="Address2..." PlaceHolder="Address2" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">State</label>
                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12">
                        <label class="modal-label">District</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnUpdateProfile" runat="server" Text="Save" CssClass="btn Save" OnClick="btnUpdateProfile_Click" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn Save" OnClick="btnBack_Click" />
                    </div>
                </div>
            </fieldset>
            <fieldset class="fieldset-border" id="FldResetOTP" runat="server" visible="false">
                <div class="col-md-12">
                    <div class="col-md-12 col-sm-12">
                        <asp:Button ID="BtnSendOTP" runat="server" Text="Send OTP" CssClass="btn Save" Width="100px" OnClick="BtnSendOTP_Click" />
                        <div id="some_div" runat="server"></div>
                    </div>
                    <div class="col-md-1 col-sm-12">
                        <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" ToolTip="Type Six digit OTP" MaxLength="6" onchange="return IsNumbericOnlyCheck(this);" Width="100px" AutoCompleteType="Disabled" PlaceHolder="OTP"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-sm-12">
                        <asp:Button ID="BtnVerifyOTP" runat="server" Text="Verify" CssClass="btn Save" Width="130px" Visible="false" OnClick="BtnVerifyOTP_Click" />
                    </div>
                    <div class="col-md-1 col-sm-12">
                        <asp:Button ID="BtnOTPBack" runat="server" Text="Back" CssClass="btn Save" Width="130px" OnClick="BtnOTPBack_Click" />
                    </div>
                </div>
            </fieldset>
        </div>
    </div>

</asp:Content>
