<%@ Page Title="" Language="C#" MasterPageFile="../Dealer.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DealerManagementSystem.Account.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:HiddenField ID="hfLatitude" runat="server" />
            <asp:HiddenField ID="hfLongitude" runat="server" /> 
    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="message" Visible="false" />
    <div class="col-md-12">
        <div class="col-md-12">
            <fieldset class="fieldset-border">
                <legend style="background: none; color: #007bff; font-size: 17px;">Change Password</legend>
                <div class="col-md-12">
                    <div class="col-md-3 text-right">
                        <asp:ImageButton ID="ibtnPhoto" ImageUrl="~/Images/ChangePw.jpg" runat="server" Width="60px" Height="55px" />
                    </div>
                    <div class="col-md-9"></div>

                    <div class="col-md-3 text-right">
                        <label>Current Password</label>
                    </div>
                    <div class="col-md-2">                
                         <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="form-control" TextMode="Password"/>
                    </div>
                    <div class="col-md-7">
                    </div>

                    <div class="col-md-3 text-right">
                        <label>New Password</label>
                    </div>
                    <div class="col-md-2">           
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"/>
                    </div>
                    <div class="col-md-7">
                    </div>

                    <div class="col-md-3 text-right">
                        <label>Re-Type New Password</label>
                    </div>
                    <div class="col-md-2">                     
                        <asp:TextBox ID="txtReTypeNewPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    </div>
                    <div class="col-md-7">
                        <div class="col-md-2 text-right ">
                            <asp:Button ID="btnChangePw" runat="server" CssClass="btn Search" Text="Change" OnClick="btnChangePw_Click"></asp:Button>
                        </div>
                    </div>

                </div>
            </fieldset>

        </div>
    </div>
</asp:Content>
